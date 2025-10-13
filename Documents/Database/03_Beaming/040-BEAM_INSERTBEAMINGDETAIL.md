# BEAM_INSERTBEAMINGDETAIL

**Procedure Number**: 040 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Start a new beam roll production and generate beam lot number |
| **Operation** | INSERT |
| **Called From** | BeamingDataService.cs:1176 → BEAM_INSERTBEAMINGDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |
| `P_MCNO` | VARCHAR2(50) | ✅ | Beaming machine code |
| `P_BEAMNO` | VARCHAR2(50) | ✅ | Physical beam roll number |
| `P_STARTDATE` | DATE | ✅ | Production start timestamp |
| `P_STARTBY` | VARCHAR2(50) | ✅ | Operator who started production |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_BEAMLOT` | VARCHAR2 | Generated beam lot barcode |
| `RESULT` | VARCHAR2 | Result message/status |

### Returns

| Type | Description |
|------|-------------|
| `BEAM_INSERTBEAMINGDETAIL` | Custom object with R_BEAMLOT and RESULT fields |

---

## Business Logic (What it does and why)

Creates a new beam roll production record and generates a unique barcode (BEAMLOT) when operator starts beaming a new beam.

**Purpose**: When operator clicks "Start Production" button:
1. System generates unique beam lot barcode (BEAMLOT)
2. Creates production record with initial data (start time, operator, machine)
3. Sets FLAG='P' (In-Process) to indicate beam is currently running
4. Returns the generated BEAMLOT to UI for display/printing

**When Used**:
- **Start New Beam**: Operator starts producing a beam within an existing beaming setup
- **Sequential Production**: Called multiple times per BEAMERNO (one setup produces multiple beams)
- **Production Tracking**: Begins the production lifecycle for a beam roll

**Business Rules**:
- All 5 input parameters required (no nulls allowed)
- Auto-generates BEAMLOT barcode (format likely: BEAMERNO + sequence or timestamp)
- Initial FLAG set to 'P' (In-Process)
- ENDDATE initially null (filled when beam is doffed)
- Empty result on validation failure (null BEAMERNO)
- BEAMLOT is unique identifier for the beam roll throughout its lifecycle

**Data Flow**:
1. Operator selects beaming setup (BEAMERNO) from machine status screen
2. Operator scans/enters physical beam number (BEAMNO)
3. Operator clicks "Start" button
4. System calls this procedure with current timestamp and operator ID
5. Procedure generates BEAMLOT and inserts record
6. System returns BEAMLOT to UI
7. UI displays BEAMLOT and may print barcode label
8. Operator attaches label to physical beam
9. Production continues... (data updated via BEAM_UPDATEBEAMDETAIL)
10. When finished, operator doffs beam (BEAM_UPDATEBEAMDETAIL sets FLAG='D')

---

## Related Procedures

**Upstream**:
- [030-BEAM_GETBEAMERMCSTATUS.md](./030-BEAM_GETBEAMERMCSTATUS.md) - Gets machine status to obtain BEAMERNO
- [BEAM_INSERTBEAMNO.md](./BEAM_INSERTBEAMNO.md) - Creates beaming setup first

**Downstream**:
- [BEAM_UPDATEBEAMDETAIL.md](./BEAM_UPDATEBEAMDETAIL.md) - Updates beam data during/after production
- [034-BEAM_GETINPROCESSLOTBYBEAMNO.md](./034-BEAM_GETINPROCESSLOTBYBEAMNO.md) - Retrieves in-process beams (FLAG='P')
- [033-BEAM_GETBEAMROLLDETAIL.md](./033-BEAM_GETBEAMROLLDETAIL.md) - Gets beam details by BEAMLOT

**Similar**:
- [017-WARP_INSERTWARPINGPROCESS.md](../02_Warping/017-WARP_INSERTWARPINGPROCESS.md) - Warping equivalent (starts warp roll)
- [WEAVE_WEAVINGPROCESS.md](../05_Weaving/WEAVE_WEAVINGPROCESS.md) - Weaving equivalent (starts fabric roll)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_INSERTBEAMINGDETAIL()`
**Line**: 1166-1215

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_INSERTBEAMINGDETAILParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 40/296 | **Progress**: 13.5%
