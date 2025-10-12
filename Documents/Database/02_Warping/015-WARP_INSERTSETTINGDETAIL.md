# WARP_INSERTSETTINGDETAIL

**Procedure Number**: 015 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert pallet allocation detail for creel setup |
| **Operation** | INSERT |
| **Tables** | tblWarpingCreelSetupDetail, tblYarnPallet |
| **Called From** | WarpingDataService.cs:1535 → WARP_INSERTSETTINGDETAIL() |
| **Frequency** | High (allocating pallets to setup) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_PALLETNO` | VARCHAR2(50) | ⬜ | Pallet barcode to allocate |
| `P_USED` | NUMBER | ⬜ | Used amount (cheese count) |
| `P_REJECTCH` | NUMBER | ⬜ | Rejected cheese count |

### Output (OUT)

None - Returns boolean success/failure

### Returns (if cursor)

N/A - Returns execution result (success = true, failure = false)

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingCreelSetupDetail` - INSERT - Pallet allocation record
- `tblYarnPallet` - UPDATE - Update used quantities

**Transaction**: Yes (should maintain data integrity between allocation and pallet update)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_creelsetup_headno_pallet ON tblWarpingCreelSetupDetail(WARPHEADNO, PALLETNO);
CREATE INDEX idx_yarnpallet_palletno ON tblYarnPallet(PALLETNO);
```

---

## Business Logic (What it does and why)

Allocates yarn pallet to creel setup by creating detail record linking pallet to setup. When operator selects pallets for warping setup, this procedure records which pallet is assigned to which creel position, updates pallet usage quantities, and tracks reject counts. Critical for material tracking and traceability.

**Workflow**:
1. Operator selects yarn pallet for creel setup
2. Specifies initial used amount and reject count (if any from previous use)
3. System calls this procedure to allocate pallet
4. Creates detail record:
   - Links pallet to warping head number
   - Records initial used and reject quantities
5. Updates yarn pallet table:
   - Increments used weight/cheese count
   - Records allocation
6. Returns success/failure
7. Operator continues selecting pallets until all positions filled

**Business Rules**:
- Warping head number required
- Multiple pallets can be allocated to same head
- Tracks used and rejected quantities
- Updates pallet inventory in real-time
- Maintains traceability from pallet to warp beam

---

## Related Procedures

**Upstream**: [002-WARP_CHECKPALLET.md](./002-WARP_CHECKPALLET.md) - Verify pallet before allocating
**Downstream**: [005-WARP_GETCREELSETUPDETAIL.md](./005-WARP_GETCREELSETUPDETAIL.md) - View allocated pallets
**Similar**: None - Unique allocation operation

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_INSERTSETTINGDETAIL()`
**Line**: 1535-1567

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public bool WARP_INSERTSETTINGDETAIL(string P_WARPHEADNO, string P_PALLETNO, decimal? P_USED, decimal? P_REJECTCH)
{
    bool result = false;

    // Validation: warping head number required
    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_INSERTSETTINGDETAILParameter dbPara = new WARP_INSERTSETTINGDETAILParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_PALLETNO = P_PALLETNO;
    dbPara.P_USED = P_USED; // Initial used amount
    dbPara.P_REJECTCH = P_REJECTCH; // Reject count

    WARP_INSERTSETTINGDETAILResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_INSERTSETTINGDETAIL(dbPara);

        // Success if result is not null
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

---

**File**: 015/296 | **Progress**: 5.1%
