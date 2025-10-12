# WARP_RECEIVEPALLET

**Procedure Number**: 020 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Receive and register new yarn pallet into warping inventory |
| **Operation** | INSERT |
| **Tables** | tblWarpingPallets |
| **Called From** | WarpingDataService.cs:1406 → WARP_RECEIVEPALLET() |
| **Frequency** | Medium (yarn receiving operations) |
| **Performance** | Fast (single insert) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMYARN` | VARCHAR2(50) | ✅ | Yarn item code |
| `P_RECEIVEDATE` | DATE | ⬜ | Date pallet was received |
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet barcode/number (unique identifier) |
| `P_WEIGHT` | NUMBER | ⬜ | Total weight in kilograms |
| `P_CH` | NUMBER | ⬜ | Cheese count (number of yarn packages on pallet) |
| `P_VERIFY` | VARCHAR2(10) | ⬜ | Verification status (Y/N) |
| `P_REJECTID` | VARCHAR2(50) | ⬜ | Reject reason code if pallet has quality issues |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who received the pallet |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingPallets` - INSERT - Creates new pallet record with initial inventory

**Transaction**: Yes (single insert operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE UNIQUE INDEX idx_pallet_pk ON tblWarpingPallets(PALLETNO, RECEIVEDATE);
CREATE INDEX idx_pallet_itemyarn ON tblWarpingPallets(ITM_YARN);
CREATE INDEX idx_pallet_createdate ON tblWarpingPallets(CREATEDATE);
```

---

## Business Logic (What it does and why)

Registers a new yarn pallet into the warping inventory system when yarn is received from the warehouse or supplier. This creates the initial inventory record that will be tracked throughout the warping process.

**Workflow**:
1. Warehouse delivers yarn pallet to warping department
2. Operator scans or enters pallet barcode
3. System verifies pallet doesn't already exist
4. Operator confirms yarn item code, weight, and cheese count
5. If pallet has quality issues, selects reject reason
6. System creates new pallet record with initial quantities
7. Pallet becomes available for creel setup operations

**Business Rules**:
- Pallet number must be unique (primary key constraint)
- Initial state: USEDCH = 0, REJECTCH = 0, FINISHFLAG = 'N'
- VERIFY field controls whether pallet can be used in production
- Rejected pallets (REJECTID set) may have restricted usage
- Creates audit trail (CREATEDATE, CREATEBY)
- Pallet weight and cheese count used for yield tracking

**Inventory Tracking**:
- `RECEIVEWEIGHT` = Initial weight received
- `RECEIVECH` = Initial cheese count
- `USEDWEIGHT` = 0 (not yet used)
- `USEDCH` = 0 (not yet used)
- `REJECTCH` = 0 (no rejects recorded yet)
- `FINISHFLAG` = 'N' (pallet still has stock)

---

## Related Procedures

**Upstream**: None - This is typically the first operation for a pallet
**Downstream**: [019-WARP_PALLETLISTBYITMYARN.md](./019-WARP_PALLETLISTBYITMYARN.md) - Lists available pallets including this new one
**Similar**: G3_RECEIVEYARN - Warehouse yarn receiving (different module)

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_RECEIVEPALLET()`
**Line**: 1406-1445

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public bool WARP_RECEIVEPALLET(string P_ITMYARN, DateTime? P_RECEIVEDATE, string P_PALLETNO,
    decimal? P_WEIGHT, decimal? P_CH, string P_VERIFY, string P_REJECTID, string P_OPERATOR)
{
    bool result = false;

    // Validation: pallet number and item yarn required
    if (string.IsNullOrWhiteSpace(P_PALLETNO))
        return result;
    if (string.IsNullOrWhiteSpace(P_ITMYARN))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_RECEIVEPALLETParameter dbPara = new WARP_RECEIVEPALLETParameter();
    dbPara.P_ITMYARN = P_ITMYARN;
    dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
    dbPara.P_PALLETNO = P_PALLETNO;
    dbPara.P_WEIGHT = P_WEIGHT;
    dbPara.P_CH = P_CH;
    dbPara.P_VERIFY = P_VERIFY;
    dbPara.P_REJECTID = P_REJECTID;
    dbPara.P_OPERATOR = P_OPERATOR;

    WARP_RECEIVEPALLETResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_RECEIVEPALLET(dbPara);

        result = (null != dbResult);
    }
    catch (Exception ex)
    {
        ex.Err();
        result = false;
    }

    return result;
}
```

**Expected Oracle Stored Procedure Logic**:
```sql
-- Estimated stored procedure structure
PROCEDURE WARP_RECEIVEPALLET(
    P_ITMYARN IN VARCHAR2,
    P_RECEIVEDATE IN DATE,
    P_PALLETNO IN VARCHAR2,
    P_WEIGHT IN NUMBER,
    P_CH IN NUMBER,
    P_VERIFY IN VARCHAR2,
    P_REJECTID IN VARCHAR2,
    P_OPERATOR IN VARCHAR2
)
IS
BEGIN
    INSERT INTO tblWarpingPallets (
        ITM_YARN,
        RECEIVEDATE,
        PALLETNO,
        RECEIVEWEIGHT,
        RECEIVECH,
        USEDWEIGHT,
        USEDCH,
        REJECTCH,
        VERIFY,
        REJECTID,
        FINISHFLAG,
        RETURNFLAG,
        CREATEDATE,
        CREATEBY
    ) VALUES (
        P_ITMYARN,
        NVL(P_RECEIVEDATE, SYSDATE),
        P_PALLETNO,
        NVL(P_WEIGHT, 0),
        NVL(P_CH, 0),
        0, -- USEDWEIGHT
        0, -- USEDCH
        0, -- REJECTCH
        NVL(P_VERIFY, 'N'),
        P_REJECTID,
        'N', -- FINISHFLAG
        'N', -- RETURNFLAG
        SYSDATE,
        P_OPERATOR
    );

    COMMIT;

EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        ROLLBACK;
        RAISE_APPLICATION_ERROR(-20001, 'Pallet number already exists');
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
```

---

**File**: 020/296 | **Progress**: 6.8%
