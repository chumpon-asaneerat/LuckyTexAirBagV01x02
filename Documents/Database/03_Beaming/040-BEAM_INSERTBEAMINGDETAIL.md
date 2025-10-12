# BEAM_INSERTBEAMINGDETAIL

**Procedure Number**: 040 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Start a new beam roll production and generate beam lot number |
| **Operation** | INSERT |
| **Tables** | tblBeamingDetail |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingDetail` - INSERT - Creates new beam roll production record

**Transaction**: Yes (should be part of production start transaction)

### Indexes (if relevant)

```sql
-- Recommended unique index on generated barcode
CREATE UNIQUE INDEX idx_beamingdetail_beamlot ON tblBeamingDetail(BEAMLOT);

-- Also useful for machine status queries
CREATE INDEX idx_beamingdetail_beamerno_flag ON tblBeamingDetail(BEAMERNO, FLAG);
```

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

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_INSERTBEAMINGDETAIL()`
**Line**: 1166-1215

**Query Type**: INSERT via DatabaseManager wrapper

```csharp
// Method signature
public BEAM_INSERTBEAMINGDETAIL BEAM_INSERTBEAMINGDETAIL(
    string P_BEAMERNO,
    string P_MCNO,
    string P_BEAMNO,
    DateTime? P_STARTDATE,
    string P_STARTBY)
{
    BEAM_INSERTBEAMINGDETAIL results = null;

    // Input validation - BEAMERNO required
    if (string.IsNullOrWhiteSpace(P_BEAMERNO))
        return results; // Returns null

    if (!HasConnection())
        return results;

    // Parameter setup
    BEAM_INSERTBEAMINGDETAILParameter dbPara = new BEAM_INSERTBEAMINGDETAILParameter();
    dbPara.P_BEAMERNO = P_BEAMERNO;
    dbPara.P_MCNO = P_MCNO;
    dbPara.P_BEAMNO = P_BEAMNO;
    dbPara.P_STARTDATE = P_STARTDATE;
    dbPara.P_STARTBY = P_STARTBY;

    // Execute insert - generates BEAMLOT
    dbResults = DatabaseManager.Instance.BEAM_INSERTBEAMINGDETAIL(dbPara);

    // Return object contains generated barcode
    if (null != dbResults)
    {
        results.R_BEAMLOT = dbResults.R_BEAMLOT; // Generated barcode
        results.RESULT = dbResults.RESULT;        // Success/error message
    }

    return results;
}
```

**Usage Pattern**:
```csharp
// In UI code-behind (BeamingProductionPage.xaml.cs)
var result = BeamingDataService.Instance.BEAM_INSERTBEAMINGDETAIL(
    beamerNo,           // From machine status
    machineCode,        // From machine selection
    beamNo,             // Scanned/entered by operator
    DateTime.Now,       // Current timestamp
    currentOperatorID   // Logged-in operator
);

if (result != null && !string.IsNullOrEmpty(result.R_BEAMLOT))
{
    // Display generated barcode to operator
    txtBeamLot.Text = result.R_BEAMLOT;

    // Print barcode label
    PrintBarcodeLabel(result.R_BEAMLOT);

    // Enable production controls
    EnableProductionControls();
}
```

---

**File**: 40/296 | **Progress**: 13.5%
