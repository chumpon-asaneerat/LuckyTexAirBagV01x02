# WARP_UPDATESETTINGHEAD

**Procedure Number**: 024 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update warping creel setup status and completion information |
| **Operation** | UPDATE |
| **Called From** | WarpingDataService.cs:1573 → WARP_UPDATESETTINGHEAD_MCStatus()<br>WarpingDataService.cs:1612 → WARP_UPDATESETTINGHEAD() |
| **Frequency** | High (status changes during warping lifecycle) |
| **Performance** | Fast (single record update) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN) - Version 1 (MCStatus Update)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_STARTDATE` | DATE | ⬜ | Conditioning start date/time |
| `P_CONDITONBY` | VARCHAR2(50) | ⬜ | Operator who started conditioning |
| `P_STATUS` | VARCHAR2(10) | ⬜ | Status code (S=Processing, C=Conditioning) |

### Input (IN) - Version 2 (Completion Update)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_ENDDATE` | DATE | ⬜ | Setup completion date/time |
| `P_STATUS` | VARCHAR2(10) | ⬜ | Status code (F=Finished) |
| `P_FINISHBY` | VARCHAR2(50) | ⬜ | Operator who finished setup |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

**Note**: This procedure is also used in BeamingDataService for similar status updates.

---

## Business Logic (What it does and why)

Updates the status and completion information of a warping creel setup as it progresses through different stages. This procedure handles two main scenarios: conditioning phase start and setup completion.

**Workflow - Scenario 1: Start Conditioning**
1. Operator completes creel setup (all pallets loaded)
2. Operator starts conditioning process (yarn tension stabilization)
3. System calls WARP_UPDATESETTINGHEAD_MCStatus():
   - P_STARTDATE = current timestamp
   - P_CONDITONBY = operator ID
   - P_STATUS = 'C' (Conditioning)
4. Machine status shows "Conditioning" instead of "Setup"
5. Conditioning typically runs 1-4 hours

**Workflow - Scenario 2: Complete Setup**
1. Conditioning period completes
2. Operator confirms setup ready for production
3. System calls WARP_UPDATESETTINGHEAD():
   - P_ENDDATE = current timestamp
   - P_STATUS = 'F' (Finished)
   - P_FINISHBY = operator ID
4. Machine status shows "Ready" or moves to production
5. Setup can now start production runs

**Business Rules**:
- **Status Progression**: Setup → Conditioning (C) → Finished (F) → Processing (S)
- **Conditioning Phase**:
  - Required for proper yarn tension stabilization
  - Typically 1-4 hours depending on product
  - Operator monitors hardness and tension
  - No production during conditioning
- **Timestamps Track**:
  - STARTDATE: When creel setup created
  - CONDITIONSTART: When conditioning began
  - ENDDATE: When setup fully complete
- **Operator Accountability**:
  - CREATEBY: Who created setup
  - CONDITIONBY: Who started conditioning
  - FINISHBY: Who completed setup

**Status Codes**:
- **S** = Processing (warping in progress)
- **C** = Conditioning (yarn stabilization)
- **F** = Finished (setup complete, ready for production)

---

## Related Procedures

**Upstream**: [016-WARP_INSERTSETTINGHEAD.md](./016-WARP_INSERTSETTINGHEAD.md) - Creates initial setup record
**Downstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Starts production after setup finished
**Cross-Module**: BEAM_UPDATESETTINGHEAD - Beaming uses same procedure pattern

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_UPDATESETTINGHEAD_MCStatus()` / `WARP_UPDATESETTINGHEAD()`
**Line**: 1573-1606 / 1612-1645

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: WARP_UPDATESETTINGHEADParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 024/296 | **Progress**: 8.1%
