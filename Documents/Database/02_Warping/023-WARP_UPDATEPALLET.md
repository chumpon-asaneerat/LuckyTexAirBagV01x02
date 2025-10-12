# WARP_UPDATEPALLET

**Procedure Number**: 023 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update pallet usage quantities during creel setup allocation |
| **Operation** | UPDATE |
| **Tables** | tblWarpingPallets |
| **Called From** | WarpingDataService.cs:1451 → WARP_UPDATEPALLET() |
| **Frequency** | High (every creel setup when allocating pallets) |
| **Performance** | Fast (single pallet update) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_RECEIVEDATE` | DATE | ⬜ | Pallet receive date (part of composite key) |
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet barcode/number |
| `P_USEDCH` | NUMBER | ⬜ | Total used cheese count (cumulative) |
| `P_REJECTCH` | NUMBER | ⬜ | Total reject cheese count (cumulative) |
| `P_REMAINCH` | NUMBER | ⬜ | Remaining cheese count |
| `P_WARPHEADNO` | VARCHAR2(50) | ⬜ | Current warp head number using this pallet |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingPallets` - UPDATE - Updates usage counters for pallet allocation

**Transaction**: Yes (single update with potential trigger for FINISHFLAG)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE UNIQUE INDEX idx_pallet_pk ON tblWarpingPallets(PALLETNO, RECEIVEDATE);
CREATE INDEX idx_pallet_warpheadno ON tblWarpingPallets(WARPHEADNO);
```

---

## Business Logic (What it does and why)

Updates yarn pallet usage quantities when operator allocates cheese from pallets during creel setup. This maintains accurate inventory tracking and links pallets to specific warping operations.

**Workflow**:
1. Operator creates creel setup and selects pallets
2. For each pallet, operator specifies:
   - How many cheese to use (USE)
   - How many cheese to reject (REJECT)
3. System calculates remaining: REMAIN = (RECEIVECH - USEDCH - REJECTCH) - USE - REJECT
4. System calls this procedure to update pallet record:
   - USEDCH += USE (cumulative used count)
   - REJECTCH += REJECT (cumulative reject count)
   - Links pallet to WARPHEADNO
5. If REMAIN = 0, sets FINISHFLAG = 'Y' (pallet fully consumed)
6. Pallet inventory updated for tracking and allocation

**Business Rules**:
- Updates are cumulative (adds to existing USEDCH and REJECTCH)
- Must not allow negative remaining quantity
- FINISHFLAG automatically set when pallet depleted:
  - `FINISHFLAG = 'Y' when (RECEIVECH - USEDCH - REJECTCH) = 0`
- Links pallet to specific warping operation via WARPHEADNO
- May update USEDWEIGHT based on USEDCH and KGPERCH ratio
- Validates that updated quantities don't exceed RECEIVECH

**Inventory Tracking**:
- **Before**: Pallet shows available cheese count
- **After**: Pallet shows reduced available count
- **If depleted**: FINISHFLAG = 'Y', pallet no longer appears in selection lists

**Related Business Logic**:
```
RECEIVECH = 100 (total received)
USEDCH = 40 (previously used)
REJECTCH = 5 (previously rejected)
Available = 55

Operator allocates: USE = 30, REJECT = 2

New values:
USEDCH = 40 + 30 = 70
REJECTCH = 5 + 2 = 7
Remaining = 100 - 70 - 7 = 23
FINISHFLAG = 'N' (still has stock)
```

---

## Related Procedures

**Upstream**: [019-WARP_PALLETLISTBYITMYARN.md](./019-WARP_PALLETLISTBYITMYARN.md) - Lists pallets to allocate
**Downstream**: [005-WARP_GETCREELSETUPDETAIL.md](./005-WARP_GETCREELSETUPDETAIL.md) - Shows allocated pallets with updated quantities
**Similar**: [003-WARP_CLEARPALLET.md](./003-WARP_CLEARPALLET.md) - Clears finished pallet

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_UPDATEPALLET()`
**Line**: 1451-1486

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public bool WARP_UPDATEPALLET(DateTime? P_RECEIVEDATE, string P_PALLETNO,
    decimal? P_USEDCH, decimal? P_REJECTCH, decimal? P_REMAINCH, string P_WARPHEADNO)
{
    bool result = false;

    // Validation: pallet number required
    if (string.IsNullOrWhiteSpace(P_PALLETNO))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_UPDATEPALLETParameter dbPara = new WARP_UPDATEPALLETParameter();
    dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
    dbPara.P_PALLETNO = P_PALLETNO;
    dbPara.P_USEDCH = P_USEDCH;
    dbPara.P_REJECTCH = P_REJECTCH;
    dbPara.P_REMAINCH = P_REMAINCH;
    dbPara.P_WARPHEADNO = P_WARPHEADNO;

    WARP_UPDATEPALLETResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_UPDATEPALLET(dbPara);

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
PROCEDURE WARP_UPDATEPALLET(
    P_RECEIVEDATE IN DATE,
    P_PALLETNO IN VARCHAR2,
    P_USEDCH IN NUMBER,
    P_REJECTCH IN NUMBER,
    P_REMAINCH IN NUMBER,
    P_WARPHEADNO IN VARCHAR2
)
IS
    v_receivech NUMBER;
    v_kgperch NUMBER;
BEGIN
    -- Get pallet info
    SELECT RECEIVECH, KGPERCH
    INTO v_receivech, v_kgperch
    FROM tblWarpingPallets
    WHERE PALLETNO = P_PALLETNO
      AND RECEIVEDATE = P_RECEIVEDATE;

    -- Update pallet usage
    UPDATE tblWarpingPallets
    SET USEDCH = NVL(P_USEDCH, USEDCH),
        REJECTCH = NVL(P_REJECTCH, REJECTCH),
        USEDWEIGHT = NVL(P_USEDCH, USEDCH) * v_kgperch,
        WARPHEADNO = P_WARPHEADNO,
        FINISHFLAG = CASE
            WHEN (v_receivech - NVL(P_USEDCH, USEDCH) - NVL(P_REJECTCH, REJECTCH)) <= 0
            THEN 'Y'
            ELSE 'N'
        END,
        EDITDATE = SYSDATE
    WHERE PALLETNO = P_PALLETNO
      AND RECEIVEDATE = P_RECEIVEDATE;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
```

---

**File**: 023/296 | **Progress**: 7.8%
