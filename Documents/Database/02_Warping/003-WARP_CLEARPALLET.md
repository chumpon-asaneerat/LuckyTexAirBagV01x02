# WARP_CLEARPALLET

**Procedure Number**: 003 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Clear/reset pallet usage data for warping |
| **Operation** | UPDATE |
| **Tables** | tblYarnPallet, tblWarpingPalletUsage |
| **Called From** | WarpingDataService.cs:1885 → WARP_CLEARPALLET() |
| **Frequency** | Low (only when clearing pallet usage records) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet barcode number to clear |
| `P_RECEIVEDATE` | DATE | ⬜ | Receive date for validation |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who clears the pallet |
| `P_REMARK` | VARCHAR2(200) | ⬜ | Remark/reason for clearing |

### Output (OUT)

None - Returns boolean success/failure

### Returns (if cursor)

N/A - Returns execution result (success = true, failure = false)

---

## Database Operations

### Tables

**Primary Tables**:
- `tblYarnPallet` - UPDATE - Reset usage counters (USEDWEIGHT, USEDCH to 0)
- `tblWarpingPalletUsage` - UPDATE or DELETE - Clear usage history records

**Transaction**: Yes (should maintain data integrity when resetting counters)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_yarnpallet_palletno ON tblYarnPallet(PALLETNO);
CREATE INDEX idx_warpingusage_palletno ON tblWarpingPalletUsage(PALLETNO, RECEIVEDATE);
```

---

## Business Logic (What it does and why)

Clears or resets pallet usage data when an error occurred during warping setup or when pallet needs to be re-allocated. This operation resets the pallet's used weight and cheese count back to zero, effectively making the pallet available for reuse as if it was never allocated.

**Workflow**:
1. Supervisor/operator identifies pallet that needs clearing (wrong allocation, data entry error, etc.)
2. Initiates clear operation from UI with pallet barcode and reason
3. System validates pallet exists and matches receive date if provided
4. Resets USEDWEIGHT and USEDCH counters to 0
5. Clears or marks pallet usage records as cancelled
6. Records who cleared and when for audit trail
7. Saves remark explaining why pallet was cleared

**Business Rules**:
- Pallet number is required
- Can only clear pallets not currently in active production
- Requires supervisor authorization (operator ID required)
- Remark should explain reason for clearing
- Audit trail maintained for traceability

---

## Related Procedures

**Upstream**: [002-WARP_CHECKPALLET.md](./002-WARP_CHECKPALLET.md) - Check pallet before clearing
**Downstream**: [002-WARP_CHECKPALLET.md](./002-WARP_CHECKPALLET.md) - Verify pallet cleared successfully
**Similar**: None - Unique operation

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_CLEARPALLET()`
**Line**: 1885-1917

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public bool WARP_CLEARPALLET(string P_PALLETNO, DateTime? P_RECEIVEDATE, string P_OPERATOR, string P_REMARK)
{
    bool result = false;

    // Validation: pallet number required
    if (string.IsNullOrWhiteSpace(P_PALLETNO))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_CLEARPALLETParameter dbPara = new WARP_CLEARPALLETParameter();
    dbPara.P_PALLETNO = P_PALLETNO;
    dbPara.P_RECEIVEDATE = P_RECEIVEDATE;
    dbPara.P_OPERATOR = P_OPERATOR;
    dbPara.P_REMARK = P_REMARK;

    WARP_CLEARPALLETResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_CLEARPALLET(dbPara);

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

**File**: 003/296 | **Progress**: 1.0%
