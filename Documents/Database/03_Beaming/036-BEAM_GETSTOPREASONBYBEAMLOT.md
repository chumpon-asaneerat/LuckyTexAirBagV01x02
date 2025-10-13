# BEAM_GETSTOPREASONBYBEAMLOT

**Procedure Number**: 036 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get machine stop reasons for a specific beam lot |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:384 → BEAM_GETSTOPREASONBYBEAMLOT() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number (barcode) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2 | Beamer setup/batch number |
| `BEAMLOT` | VARCHAR2 | Beam roll lot number |
| `REASON` | VARCHAR2 | Stop reason code/description |
| `LENGTH` | NUMBER | Length at which machine stopped (meters) |
| `OPERATOR` | VARCHAR2 | Operator who recorded the stop |
| `OTHERFLAG` | VARCHAR2 | Flag indicating if "Other" reason selected |
| `CREATEDATE` | DATE | Timestamp when stop was recorded |

---

## Business Logic (What it does and why)

Retrieves all machine stop events recorded for a specific beam roll during production.

**Purpose**: When operator or supervisor reviews a beam roll, they need to see:
- Why the machine stopped during production
- How many times it stopped
- Where (at what length) each stop occurred
- Who recorded each stop event

This data is critical for:
- **Production Analysis**: Understanding downtime causes
- **Quality Investigation**: Stops may correlate with defects
- **Performance Metrics**: Calculating machine efficiency
- **Operator Accountability**: Tracking who handled each stop

**When Used**:
- **Production Review**: Supervisor checks stop history for a beam
- **Edit Beam Screen**: Display stop events when editing beam data
- **Quality Analysis**: Investigating quality issues related to stops
- **Performance Reports**: Calculating downtime by stop reason

**Business Rules**:
- Multiple stop events may exist for a single beam lot
- Each stop records the exact length at which it occurred
- OTHERFLAG='Y' indicates operator entered custom stop reason text
- Empty result means the beam had no recorded stops (uninterrupted production)
- Stops are recorded by operator using "Insert Stop" button during production

**Data Flow**:
1. During production, machine stops (breakdown, yarn break, etc.)
2. Operator records stop reason via UI (calls BEAM_INSERTBEAMMCSTOP)
3. Later, when reviewing production, this procedure retrieves all stops
4. UI displays stop history as a list or timeline

---

## Related Procedures

**Upstream**:
- [BEAM_INSERTBEAMMCSTOP.md](./BEAM_INSERTBEAMMCSTOP.md) - Records stop events (writes to tblBeamingMCStop)
- [033-BEAM_GETBEAMROLLDETAIL.md](./033-BEAM_GETBEAMROLLDETAIL.md) - Gets beam details before showing stop history

**Downstream**:
- None (read-only query for display purposes)

**Similar**:
- [010-WARP_GETSTOPREASONBYWARPERLOT.md](../02_Warping/010-WARP_GETSTOPREASONBYWARPERLOT.md) - Warping module equivalent
- [WEAV_GETMCSTOPBYLOT.md](../05_Weaving/WEAV_GETMCSTOPBYLOT.md) - Weaving module equivalent

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_GETSTOPREASONBYBEAMLOT()`
**Line**: 377-425

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_GETSTOPREASONBYBEAMLOTParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 36/296 | **Progress**: 12.2%
