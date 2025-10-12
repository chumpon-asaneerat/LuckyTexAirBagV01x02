# BEAM_GETINPROCESSLOTBYBEAMNO

**Procedure Number**: 034 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get in-process beam lots for a beamer machine setup |
| **Operation** | SELECT |
| **Tables** | tblBeamingDetail |
| **Called From** | BeamingDataService.cs:717 → BEAM_GETINPROCESSLOTBYBEAMNO() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |

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
| `FLAG` | VARCHAR2 | Production status flag |
| `REMARK` | VARCHAR2 | Production remarks/notes |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingDetail` - SELECT - Retrieve in-process beam records

**Transaction**: No (Read-only query)

### Indexes (if relevant)

```sql
-- Recommended composite index
CREATE INDEX idx_beamingdetail_beamerno_flag ON tblBeamingDetail(BEAMERNO, FLAG);
```

---

## Business Logic (What it does and why)

Retrieves all in-process (not yet doffed) beam rolls for a specific beamer machine setup.

**Purpose**: When operator selects a beamer setup (BEAMERNO), this procedure lists all beam rolls currently in production. This allows operators to:
- See which beams are still running
- Select a beam to continue production
- Review current production status before doffing
- Monitor multiple beams in a setup

**When Used**:
- **Beaming Dashboard**: Display active beams for a machine setup
- **Continue Production**: Operator selects which beam to work on
- **Production Monitoring**: Supervisors check in-process beams
- **Doff Beam**: Operator selects beam to complete

**Business Rules**:
- Returns only beams with FLAG = 'P' (In-Process)
- Empty result means all beams in the setup are completed
- Multiple beams may be in-process for same BEAMERNO (sequential beaming)
- Input validation: Returns null if P_BEAMERNO is empty/null

**Data Flow**:
1. Operator opens beaming machine screen
2. System calls BEAM_GETBEAMERMCSTATUS to get BEAMERNO
3. This procedure retrieves all in-process beams for that setup
4. UI displays list of active beams for operator selection

---

## Related Procedures

**Upstream**:
- [030-BEAM_GETBEAMERMCSTATUS.md](./030-BEAM_GETBEAMERMCSTATUS.md) - Get machine status to obtain BEAMERNO
- [BEAM_INSERTBEAMINGDETAIL.md](./BEAM_INSERTBEAMINGDETAIL.md) - Creates new beam lots (sets FLAG='P')

**Downstream**:
- [033-BEAM_GETBEAMROLLDETAIL.md](./033-BEAM_GETBEAMROLLDETAIL.md) - Get full details after selecting a beam
- [BEAM_UPDATEBEAMDETAIL.md](./BEAM_UPDATEBEAMDETAIL.md) - Update beam (including FLAG to 'D' when doffing)

**Similar**:
- [008-WARP_GETINPROCESSLOTBYHEADNO.md](../02_Warping/008-WARP_GETINPROCESSLOTBYHEADNO.md) - Same pattern for warping module

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_GETINPROCESSLOTBYBEAMNO()`
**Line**: 710-775

**Query Type**: SELECT via DatabaseManager wrapper

```csharp
// Method signature
public List<BEAM_GETINPROCESSLOTBYBEAMNO> BEAM_GETINPROCESSLOTBYBEAMNO(string P_BEAMERNO)
{
    // Input validation
    if (string.IsNullOrWhiteSpace(P_BEAMERNO))
        return results; // Return null if empty

    if (!HasConnection())
        return results;

    // Parameter setup
    BEAM_GETINPROCESSLOTBYBEAMNOParameter dbPara = new BEAM_GETINPROCESSLOTBYBEAMNOParameter();
    dbPara.P_BEAMERNO = P_BEAMERNO;

    // Execute via DatabaseManager
    dbResults = DatabaseManager.Instance.BEAM_GETINPROCESSLOTBYBEAMNO(dbPara);

    // Returns List<BEAM_GETINPROCESSLOTBYBEAMNO> with in-process beams only
}
```

---

**File**: 34/296 | **Progress**: 11.5%
