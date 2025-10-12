# WARP_EDITWARPERMCSETUP

**Procedure Number**: 004 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit/change warping machine assignment for creel setup |
| **Operation** | UPDATE |
| **Tables** | tblWarpingCreelSetup, tblWarpingHead |
| **Called From** | WarpingDataService.cs:1968 → WARP_EDITWARPERMCSETUP() |
| **Frequency** | Low (only when changing machine assignment) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number (creel setup ID) |
| `P_WARPMC` | VARCHAR2(50) | ⬜ | Current/old warping machine number |
| `P_SIDE` | VARCHAR2(10) | ⬜ | Side identifier (A or B) |
| `P_NEWWARPMC` | VARCHAR2(50) | ✅ | New warping machine number to assign |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who makes the change |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2 | Return message (success/error status) |

### Returns (if cursor)

N/A - Returns single string result

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingHead` - UPDATE - Change machine assignment (WARPMC field)
- `tblWarpingCreelSetup` - UPDATE - Update machine reference in setup records

**Transaction**: Yes (should maintain referential integrity)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpinghead_headno ON tblWarpingHead(WARPHEADNO);
CREATE INDEX idx_warpinghead_warpmc ON tblWarpingHead(WARPMC);
CREATE INDEX idx_creelsetup_headno ON tblWarpingCreelSetup(WARPHEADNO, SIDE);
```

---

## Business Logic (What it does and why)

Changes the warping machine assignment for an existing creel setup. Used when a creel setup needs to be moved to a different warping machine due to machine breakdown, scheduling changes, or operational requirements. Updates all related records to reflect the new machine assignment.

**Workflow**:
1. Supervisor identifies need to change machine assignment (machine breakdown, schedule adjustment)
2. Selects creel setup (head number) and specifies new target machine
3. System validates new machine is available and compatible
4. Updates warping head record with new machine number
5. Updates all associated creel setup records
6. Records operator who made the change for audit trail
7. Returns success/error message

**Business Rules**:
- Warping head number required (identifies which setup to move)
- New machine number required (target machine)
- New machine must be available (not in use)
- New machine must be compatible with setup specifications
- Can only edit setups not yet in production
- Requires supervisor authorization

---

## Related Procedures

**Upstream**: [006-WARP_GETCREELSETUPSTATUS.md](./006-WARP_GETCREELSETUPSTATUS.md) - Check if setup can be moved
**Downstream**: [005-WARP_GETCREELSETUPDETAIL.md](./005-WARP_GETCREELSETUPDETAIL.md) - Verify machine change
**Similar**: [BEAM_EDITBEAMERMC.md](../03_Beaming/BEAM_EDITBEAMERMC.md) - Similar machine change in beaming

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_EDITWARPERMCSETUP()`
**Line**: 1968-2004

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public string WARP_EDITWARPERMCSETUP(string P_WARPHEADNO, string P_WARPMC, string P_SIDE, string P_NEWWARPMC, string P_OPERATOR)
{
    string result = string.Empty;

    // Validation: warping head number required
    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    // Validation: new machine number required
    if (string.IsNullOrWhiteSpace(P_NEWWARPMC))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_EDITWARPERMCSETUPParameter dbPara = new WARP_EDITWARPERMCSETUPParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPMC = P_WARPMC;
    dbPara.P_SIDE = P_SIDE;
    dbPara.P_NEWWARPMC = P_NEWWARPMC;
    dbPara.P_OPERATOR = P_OPERATOR;

    WARP_EDITWARPERMCSETUPResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_EDITWARPERMCSETUP(dbPara);
        result = dbResult.RESULT;
    }
    catch (Exception ex)
    {
        ex.Err();
        result = string.Empty;
    }

    return result;
}
```

---

**File**: 004/296 | **Progress**: 1.4%
