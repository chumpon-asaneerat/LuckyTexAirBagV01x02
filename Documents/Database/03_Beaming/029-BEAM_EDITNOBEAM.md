# BEAM_EDITNOBEAM

**Procedure Number**: 029 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit/change beam number for a completed beam roll |
| **Operation** | UPDATE |
| **Called From** | BeamingDataService.cs:1545 → BEAM_EDITNOBEAM() |
| **Frequency** | Low (corrections only) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll/lot number |
| `P_OLDNO` | VARCHAR2(50) | ⬜ | Old beam number (for validation) |
| `P_NEWNO` | VARCHAR2(50) | ⬜ | New beam number |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator making the change |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic (What it does and why)

Changes beam number when incorrectly assigned or needs correction for tracking/labeling purposes.

**Workflow**:
1. Beam completed with number
2. Error discovered in beam numbering
3. Operator initiates beam number change
4. System validates beam roll exists
5. Updates beam number
6. Records edit audit trail

**Business Rules**:
- Used for error corrections
- Maintains old beam number for reference
- Records who made the change and when
- Updates OLDBEAMNO field with previous number

---

## Related Procedures

**Similar**: [004-WARP_EDITWARPERMCSETUP.md](../02_Warping/004-WARP_EDITWARPERMCSETUP.md)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_EDITNOBEAM()`
**Line**: 1545-1577

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_EDITNOBEAMParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 029/296 | **Progress**: 9.8%
