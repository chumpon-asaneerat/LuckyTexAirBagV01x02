# WARP_GETCREELSETUPSTATUS

**Procedure Number**: 006 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get creel setup status for machine and side |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:842 → WARP_GETCREELSETUPSTATUS() |
| **Frequency** | High (monitors machine status) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ⬜ | Machine number (filter) |
| `P_SIDE` | VARCHAR2(10) | ⬜ | Side identifier (A or B) |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `ITM_PREPARE` | VARCHAR2(50) | Item preparation code |
| `PRODUCTTYPEID` | VARCHAR2(50) | Product type ID |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `SIDE` | VARCHAR2(10) | Side (A or B) |
| `ACTUALCH` | NUMBER | Actual cheese count |
| `WTYPE` | VARCHAR2(20) | Warping type |
| `STARTDATE` | DATE | Setup start date |
| `CREATEBY` | VARCHAR2(50) | Created by user |
| `CONDITIONSTART` | DATE | Condition start date |
| `CONDITIONBY` | VARCHAR2(50) | Condition set by user |
| `ENDDATE` | DATE | Setup end date |
| `STATUS` | VARCHAR2(20) | Setup status |
| `FINISHBY` | VARCHAR2(50) | Finished by user |
| `FINISHFLAG` | VARCHAR2(1) | Finished flag (Y/N) |
| `REEDNO` | VARCHAR2(50) | Reed number |
| `EDITBY` | VARCHAR2(50) | Last edited by |
| `EDITDATE` | DATE | Last edit date |

---

## Business Logic (What it does and why)

Retrieves current creel setup status for specified machine and side. Used for monitoring machine status, displaying active setups, and checking if machine is available. Returns complete setup information including item being processed, start/end times, status flags, and responsible operators.

**Workflow**:
1. UI requests status for specific machine/side or all machines
2. Procedure queries active/recent setups
3. Returns setup details with current status
4. UI displays machine status dashboard or checks availability
5. Operators can see what's running on each machine

**Business Rules**:
- If machine number provided, filters by that machine
- If side provided, filters by that side
- Shows all active and recent setups
- Includes audit trail (created by, edited by, finished by)
- Status indicates if setup is active, finished, or cancelled

---

## Related Procedures

**Upstream**: [015-WARP_INSERTSETTINGHEAD.md](./015-WARP_INSERTSETTINGHEAD.md) - Creates setup
**Downstream**: [004-WARP_EDITWARPERMCSETUP.md](./004-WARP_EDITWARPERMCSETUP.md) - Can edit if status allows
**Similar**: [BEAM_GETBEAMERMCSTATUS.md](../03_Beaming/BEAM_GETBEAMERMCSTATUS.md) - Similar status check

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETCREELSETUPSTATUS()`
**Lines**: 842-894

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_GETCREELSETUPSTATUS(WARP_GETCREELSETUPSTATUSParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 006/296 | **Progress**: 2.0%
