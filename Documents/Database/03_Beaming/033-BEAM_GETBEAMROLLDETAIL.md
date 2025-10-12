# BEAM_GETBEAMROLLDETAIL

**Procedure Number**: 033 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get detailed beaming production data by beam roll barcode |
| **Operation** | SELECT |
| **Tables** | tblBeamingDetail |
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
| `BEAMMC` | VARCHAR2 | Beaming machine code |
| `FLAG` | VARCHAR2 | Production status flag (P/D) |
| `REMARK` | VARCHAR2 | Production remarks/notes |
| `TENSION_ST1` to `TENSION_ST10` | NUMBER | Tension values for 10 beam stands |
| `EDITBY` | VARCHAR2 | User who last edited the record |
| `OLDBEAMNO` | VARCHAR2 | Previous beam number (if edited) |
| `EDITDATE` | DATE | Last edit timestamp |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingDetail` - SELECT - Retrieve beam roll production record

**Transaction**: No (Read-only query)

### Indexes (if relevant)

```sql
-- Recommended index for fast barcode lookup
CREATE INDEX idx_beamingdetail_beamlot ON tblBeamingDetail(BEAMLOT);
```

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

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_GETBEAMROLLDETAIL()`
**Line**: 889-961

**Query Type**: SELECT via DatabaseManager wrapper

```csharp
// Method signature
public List<BEAM_GETBEAMROLLDETAIL> BEAM_GETBEAMROLLDETAIL(string P_BEAMROLL)
{
    // Input validation
    if (!HasConnection())
        return results;

    // Parameter setup
    BEAM_GETBEAMROLLDETAILParameter dbPara = new BEAM_GETBEAMROLLDETAILParameter();
    dbPara.P_BEAMROLL = P_BEAMROLL;

    // Execute via DatabaseManager
    dbResults = DatabaseManager.Instance.BEAM_GETBEAMROLLDETAIL(dbPara);

    // Map 30+ fields from result to model object
    // Returns List<BEAM_GETBEAMROLLDETAIL> with complete beam production data
}
```

---

**File**: 33/296 | **Progress**: 11.1%
