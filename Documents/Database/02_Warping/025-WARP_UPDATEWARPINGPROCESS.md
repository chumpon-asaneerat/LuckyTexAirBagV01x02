# WARP_UPDATEWARPINGPROCESS

**Procedure Number**: 025 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update warping production parameters during or after beam production |
| **Operation** | UPDATE |
| **Called From** | WarpingDataService.cs:1719 → WARP_UPDATEWARPINGPROCESS() (doff completion)<br>WarpingDataService.cs:1786 → WARP_UPDATEWARPINGPROCESS() (in-process edit)<br>WarpingDataService.cs:1923 → WARP_UPDATEWARPINGPROCESS_REMARK() (remark only) |
| **Frequency** | High (beam completion and in-process adjustments) |
| **Performance** | Fast (single record update) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN) - Version 1 (Doff Completion)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number (beam barcode) |
| `P_LENGTH` | NUMBER | ⬜ | Final beam length in meters |
| `P_ENDDATE` | DATE | ⬜ | Doff completion timestamp |
| `P_SPEED` | NUMBER | ⬜ | Average warping speed |
| `P_HARDL` | NUMBER | ⬜ | Beam hardness left side |
| `P_HARDN` | NUMBER | ⬜ | Beam hardness center |
| `P_HARDR` | NUMBER | ⬜ | Beam hardness right side |
| `P_TENSION` | NUMBER | ⬜ | Average yarn tension |
| `P_DOFFBY` | VARCHAR2(50) | ⬜ | Operator who doffed beam |
| `P_TENSION_IT` | NUMBER | ⬜ | IT tension parameter |
| `P_TENSION_TAKE` | NUMBER | ⬜ | Take-up tension parameter |
| `P_MCL` | NUMBER | ⬜ | Machine counter L value |
| `P_MCS` | NUMBER | ⬜ | Machine counter S value |

### Input (IN) - Version 2 (In-Process Edit)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number |
| `P_LENGTH` | NUMBER | ⬜ | Current/updated length |
| `P_SPEED` | NUMBER | ⬜ | Updated speed |
| `P_HARDL` | NUMBER | ⬜ | Updated hardness L |
| `P_HARDN` | NUMBER | ⬜ | Updated hardness N |
| `P_HARDR` | NUMBER | ⬜ | Updated hardness R |
| `P_TENSION` | NUMBER | ⬜ | Updated tension |
| `P_TENSION_IT` | NUMBER | ⬜ | Updated IT tension |
| `P_TENSION_TAKE` | NUMBER | ⬜ | Updated take-up tension |
| `P_MCL` | NUMBER | ⬜ | Updated counter L |
| `P_MCS` | NUMBER | ⬜ | Updated counter S |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator making edit |
| `P_BEAMNO` | VARCHAR2(50) | ⬜ | Beam number (if changed) |

### Input (IN) - Version 3 (Remark Only)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Notes/remarks about production |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

---

## Business Logic (What it does and why)

Updates warping production parameters in three main scenarios: beam completion (doff), in-process adjustments, and adding remarks. This maintains accurate production records and quality parameters for traceability.

**Workflow - Scenario 1: Beam Completion (Doff)**
1. Warping machine reaches target length or beam full
2. Operator stops machine and performs doff (beam removal)
3. Operator measures final parameters:
   - Length (actual meters)
   - Hardness at 3 points (L/N/R)
   - Records average speed and tension
4. System calls WARP_UPDATEWARPINGPROCESS() with completion data:
   - P_ENDDATE = current timestamp
   - P_DOFFBY = operator ID
   - All final quality parameters
5. Sets FLAG = 'C' (Complete)
6. Beam ready for transfer to beaming

**Workflow - Scenario 2: In-Process Edit**
1. Production running, operator needs to adjust parameters
2. Operator edits current values (speed, tension, hardness)
3. System updates running process record
4. Used for:
   - Correcting data entry errors
   - Recording mid-production parameter changes
   - Updating beam number if changed

**Workflow - Scenario 3: Add Remarks**
1. Operator needs to note quality issues or special conditions
2. Adds text remark (defects, problems, observations)
3. System updates REMARK field only
4. Preserves all other production data

**Business Rules**:
- **Quality Parameters Required at Doff**:
  - LENGTH: Actual meters produced
  - HARDNESS_L/N/R: Must be within spec range
  - SPEED: Average speed during production
  - TENSION: Average yarn tension
- **FLAG Values**:
  - NULL or 'S': In process
  - 'C': Complete (doffed)
  - 'T': Transferred to next stage
- **Audit Trail**:
  - EDITBY: Last operator to edit
  - EDITDATE: Last edit timestamp
  - DOFFBY: Operator who completed beam
  - ENDDATE: Completion timestamp

**Three Overloaded Methods in C#**:
The same procedure is called with different parameter combinations based on the operation type. The stored procedure uses NVL to only update provided parameters.

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates initial process record
**Downstream**: [022-WARP_TRANFERSLIP.md](./022-WARP_TRANFERSLIP.md) - Prints transfer slip using updated data
**Similar**: BEAM_UPDATEBEAMDETAIL - Beaming process update (similar pattern)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_UPDATEWARPINGPROCESS()` / `WARP_UPDATEWARPINGPROCESS_REMARK()`
**Line**: 1719-1763 / 1786-1832 / 1923-1954

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: WARP_UPDATEWARPINGPROCESSParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 025/296 | **Progress**: 8.4%
