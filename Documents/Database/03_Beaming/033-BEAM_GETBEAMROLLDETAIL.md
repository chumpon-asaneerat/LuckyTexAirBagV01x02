# BEAM_GETBEAMROLLDETAIL

**Procedure Number**: 033 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get detailed beaming production data by beam roll barcode |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:895 → BEAM_GETBEAMROLLDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll barcode (BEAMLOT) to retrieve |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2 | Beamer machine setup/batch number |
| `BEAMLOT` | VARCHAR2 | Unique beam roll lot number (barcode) |
| `BEAMNO` | VARCHAR2 | Physical beam roll number |
| `STARTDATE` | DATE | Production start timestamp |
| `ENDDATE` | DATE | Production end timestamp (doff time) |
| `LENGTH` | NUMBER | Actual beam length produced (meters) |
| `SPEED` | NUMBER | Average machine speed (m/min) |
| `BEAMSTANDTENSION` | NUMBER | Beam stand tension setting |
| `WINDINGTENSION` | NUMBER | Winding tension setting |
| `HARDNESS_L` | NUMBER | Beam hardness - left side |
| `HARDNESS_N` | NUMBER | Beam hardness - center |
| `HARDNESS_R` | NUMBER | Beam hardness - right side |
| `INSIDE_WIDTH` | NUMBER | Inside beam width (mm) |
| `OUTSIDE_WIDTH` | NUMBER | Outside beam width (mm) |
| `FULL_WIDTH` | NUMBER | Full beam width (mm) |
| `STARTBY` | VARCHAR2 | Operator who started production |
| `DOFFBY` | VARCHAR2 | Operator who doffed the beam |
| `BEAMMC` | VARCHAR2(50) | Beaming machine code |
| `FLAG` | VARCHAR2(10) | Production status flag (P/D) |
| `REMARK` | VARCHAR2(500) | Production remarks/notes |
| `TENSION_ST1` | NUMBER | Station 1 tension measurement |
| `TENSION_ST2` | NUMBER | Station 2 tension measurement |
| `TENSION_ST3` | NUMBER | Station 3 tension measurement |
| `TENSION_ST4` | NUMBER | Station 4 tension measurement |
| `TENSION_ST5` | NUMBER | Station 5 tension measurement |
| `TENSION_ST6` | NUMBER | Station 6 tension measurement |
| `TENSION_ST7` | NUMBER | Station 7 tension measurement |
| `TENSION_ST8` | NUMBER | Station 8 tension measurement |
| `TENSION_ST9` | NUMBER | Station 9 tension measurement |
| `TENSION_ST10` | NUMBER | Station 10 tension measurement |
| `EDITBY` | VARCHAR2(50) | User who last edited the record |
| `OLDBEAMNO` | VARCHAR2(50) | Previous beam number (if edited) |
| `EDITDATE` | DATE | Last edit timestamp |

---

## Business Logic (What it does and why)

Retrieves complete production details for a specific beam roll when operator scans a beam barcode.

**Purpose**: When operators need to view, edit, or continue production on a specific beam roll, they scan the beam barcode. This procedure returns all production data including process parameters (speed, tension, hardness), measurements (length, width), operator information, and timestamps.

**When Used**:
- **Edit Beam Production**: Operator scans beam to modify production data
- **Continue Production**: Operator scans in-process beam to resume
- **Quality Review**: Supervisor checks production parameters
- **Transfer Slip**: System needs beam details for transfer documentation
- **Drawing Setup**: Next process (M04-Drawing) needs beam details

**Business Rules**:
- Returns single record matching the scanned beam roll barcode
- Includes all 10 beam stand tension values (for multi-stand beamers)
- Returns edit history (EDITBY, EDITDATE, OLDBEAMNO) for audit trail
- FLAG indicates production status: 'P' = In-Process, 'D' = Doffed/Complete

---

## Related Procedures

**Upstream**:
- [027-BEAM_BEAMLIST.md](./027-BEAM_BEAMLIST.md) - Search/list beams before drilling down
- [030-BEAM_GETBEAMERMCSTATUS.md](./030-BEAM_GETBEAMERMCSTATUS.md) - Get machine status to find active beams

**Downstream**:
- [BEAM_UPDATEBEAMDETAIL.md](./BEAM_UPDATEBEAMDETAIL.md) - Update beam data after retrieval
- [BEAM_EDITNOBEAM.md](./029-BEAM_EDITNOBEAM.md) - Edit beam number after retrieval

**Similar**:
- [032-BEAM_GETBEAMERROLLREMARK.md](./032-BEAM_GETBEAMERROLLREMARK.md) - Gets only REMARK field (lighter query)
- [078-BEAM_GETINPROCESSLOTBYBEAMNO.md](./078-BEAM_GETINPROCESSLOTBYBEAMNO.md) - Gets in-process beams by BEAMERNO

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_GETBEAMROLLDETAIL()`
**Line**: 889-961

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_GETBEAMROLLDETAILParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 33/296 | **Progress**: 11.1%
