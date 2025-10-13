# WARP_EDITWARPERMCSETUP

**Procedure Number**: 004 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit/change warping machine assignment for creel setup |
| **Operation** | UPDATE |
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

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_EDITWARPERMCSETUP()`
**Lines**: 1968-2004

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_EDITWARPERMCSETUP(WARP_EDITWARPERMCSETUPParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 004/296 | **Progress**: 1.4%
