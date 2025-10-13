# WARP_GETWARPERMCSTATUS

**Procedure Number**: 012 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get warping machine status by machine and side |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:422 → Warp_GetWarperMCStatusSideA/B() |
| **Frequency** | High (machine monitoring dashboard) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ⬜ | Machine number (optional filter) |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `SIDE` | VARCHAR2(10) | Side (A or B) |
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `ITM_PREPARE` | VARCHAR2(50) | Item preparation code |
| `PRODUCTTYPEID` | VARCHAR2(50) | Product type ID |
| `WARPMC` | VARCHAR2(50) | Machine number |
| `ACTUALCH` | NUMBER | Actual cheese count |
| `WTYPE` | VARCHAR2(20) | Warping type |
| `STARTDATE` | DATE | Setup start date |
| `CREATEBY` | VARCHAR2(50) | Created by user |
| `CONDITIONSTART` | DATE | Condition start date |
| `CONDITIONBY` | VARCHAR2(50) | Condition set by |
| `ENDDATE` | DATE | Setup end date |
| `STATUS` | VARCHAR2(20) | Current status |
| `FINISHBY` | VARCHAR2(50) | Finished by user |
| `CONDITIONING` | VARCHAR2(20) | Conditioning status |

---

## Business Logic (What it does and why)

Retrieves real-time machine status for warping machines. Shows which setups are currently active on each machine side (A or B). Used for production monitoring dashboard, displays what each machine is producing, current status, and responsible operators. Critical for production planning and machine utilization monitoring.

**Workflow**:
1. Dashboard requests machine status (all machines or specific machine)
2. Procedure queries active setups by machine and side
3. Returns setup details for each side showing:
   - What item is being produced
   - Current production status
   - Who started and who's responsible
   - Setup and condition timing
4. C# code filters result by side:
   - `Warp_GetWarperMCStatusSideA()` - Returns only Side A records
   - `Warp_GetWarperMCStatusSideB()` - Returns only Side B records
5. UI displays machine status dashboard with real-time info

**Business Rules**:
- Each machine has two sides (A and B)
- Each side can have independent setup
- Shows current status (running, stopped, finished)
- Tracks conditioning status
- Displays responsible operators
- Used for real-time production monitoring

---

## Related Procedures

**Upstream**: [015-WARP_INSERTSETTINGHEAD.md](./015-WARP_INSERTSETTINGHEAD.md) - Creates setup
**Downstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Production records
**Similar**: [WEAV_WEAVINGMCSTATUS.md](../05_Weaving/WEAV_WEAVINGMCSTATUS.md) - Similar machine monitoring

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `Warp_GetWarperMCStatusSideA()` and `Warp_GetWarperMCStatusSideB()`
**Line**: 408-461, 471-524

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_GETWARPERMCSTATUS(WARP_GETWARPERMCSTATUSParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 012/296 | **Progress**: 4.1%
