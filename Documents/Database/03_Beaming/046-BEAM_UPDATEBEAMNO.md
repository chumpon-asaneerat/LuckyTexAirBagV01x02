# BEAM_UPDATEBEAMNO

**Procedure Number**: 046 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update beaming setup status and completion |
| **Operation** | UPDATE |
| **Tables** | tblBeamingHead |
| **Called From** | BeamingDataService.cs:1221 → BEAM_UPDATEBEAMNO() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMNO` | VARCHAR2(50) | ✅ | Beamer setup number (BEAMERNO) |
| `P_ENDDATE` | DATE | ✅ | Setup completion timestamp |
| `P_STATUS` | VARCHAR2(1) | ✅ | Setup status (S/P/F) |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message (success/error) |

### Returns

| Type | Description |
|------|-------------|
| `String` | Result message from database |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingHead` - UPDATE - Updates setup header status

**Transaction**: Yes (should be final step in setup completion)

### Indexes (if relevant)

```sql
-- Recommended index on primary key
CREATE UNIQUE INDEX idx_beaminghead_beamerno ON tblBeamingHead(BEAMERNO);
```

---

## Business Logic (What it does and why)

Updates beaming setup header status and end date when setup is completed or status changes.

**Purpose**: Manages the lifecycle of a beaming setup at the **header level** (not individual beams). When:
1. **Starting Production**: Changes STATUS from 'S' (Setting) to 'P' (Processing)
2. **Completing Setup**: Changes STATUS to 'F' (Finished) and records end date
3. **Status Changes**: Any status transition requiring header update

**When Used**:
- **Start First Beam**: When first beam in setup starts, change STATUS='S' → 'P'
- **Complete Setup**: When all beams produced, change STATUS='P' → 'F' + set ENDDATE
- **Cancel Setup**: Change status to indicate cancellation
- **Status Management**: Any header-level status change

**Business Rules**:
- All 3 parameters required (BEAMNO, ENDDATE, STATUS)
- Empty/null P_BEAMNO causes failure (returns empty string)
- P_BEAMNO is the BEAMERNO (confusing parameter name - historical naming)
- STATUS values:
  - **'S'** = Setting (setup created, not yet producing)
  - **'P'** = Processing (actively producing beams)
  - **'F'** = Finished (all beams completed)
- ENDDATE typically provided when setting STATUS='F'
- FINISHFLAG also updated based on status

**Status Lifecycle**:
```
1. BEAM_INSERTBEAMNO → Creates setup with STATUS='S' (Setting)
                                    ↓
2. BEAM_UPDATEBEAMNO(STATUS='P') → First beam starts (Processing)
                                    ↓
3. BEAM_INSERTBEAMINGDETAIL → Produce multiple beams...
   BEAM_UPDATEBEAMDETAIL → ...multiple times
                                    ↓
4. BEAM_UPDATEBEAMNO(STATUS='F') → All beams complete (Finished)
```

---

## Related Procedures

**Upstream**:
- [042-BEAM_INSERTBEAMNO.md](./042-BEAM_INSERTBEAMNO.md) - Creates setup with initial STATUS='S'
- [040-BEAM_INSERTBEAMINGDETAIL.md](./040-BEAM_INSERTBEAMINGDETAIL.md) - Triggers status change to 'P'

**Downstream**:
- [030-BEAM_GETBEAMERMCSTATUS.md](./030-BEAM_GETBEAMERMCSTATUS.md) - Reads updated status
- [043-BEAM_SEARCHBEAMRECORD.md](./043-BEAM_SEARCHBEAMRECORD.md) - Filters by status

**Similar**:
- [WARP_UPDATESETTINGHEAD.md](../02_Warping/WARP_UPDATESETTINGHEAD.md) - Warping setup status update

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_UPDATEBEAMNO()`
**Line**: 1219-1254

**Query Type**: UPDATE via DatabaseManager wrapper

```csharp
// Method signature
public string BEAM_UPDATEBEAMNO(
    string P_BEAMNO,      // Actually BEAMERNO (confusing naming)
    DateTime P_ENDDATE,
    string P_STATUS)
{
    string result = string.Empty;

    // Input validation - BEAMNO required
    if (string.IsNullOrWhiteSpace(P_BEAMNO))
        return result; // Returns empty string

    if (!HasConnection())
        return result;

    // Parameter setup
    BEAM_UPDATEBEAMNOParameter dbPara = new BEAM_UPDATEBEAMNOParameter();
    dbPara.P_BEAMNO = P_BEAMNO;     // BEAMERNO value
    dbPara.P_ENDDATE = P_ENDDATE;
    dbPara.P_STATUS = P_STATUS;     // S/P/F

    // Execute update
    dbResult = DatabaseManager.Instance.BEAM_UPDATEBEAMNO(dbPara);

    result = dbResult.R_RESULT; // Returns success/error message

    return result;
}
```

**Usage Patterns**:

**Pattern 1: Start Production (S → P)**
```csharp
// When first beam starts
string result = BeamingDataService.Instance.BEAM_UPDATEBEAMNO(
    beamerNo,
    DateTime.Now,
    "P"  // Processing
);
```

**Pattern 2: Complete Setup (P → F)**
```csharp
// When all beams finished
string result = BeamingDataService.Instance.BEAM_UPDATEBEAMNO(
    beamerNo,
    DateTime.Now,
    "F"  // Finished
);

if (!string.IsNullOrEmpty(result))
{
    MessageBox.Show("Setup completed successfully");
    UpdateMachineStatus(); // Refresh machine display
}
```

**Pattern 3: Cancel Setup**
```csharp
// If setup needs to be cancelled
string result = BeamingDataService.Instance.BEAM_UPDATEBEAMNO(
    beamerNo,
    DateTime.Now,
    "C"  // Cancelled (if supported)
);
```

---

**File**: 46/296 | **Progress**: 15.5%

---

## Module M03-Beaming Complete! ✅

**M03-Beaming**: 20/20 procedures documented (100% complete)
- All beaming operations documented
- Setup creation, production tracking, status management
- Cross-module traceability (M02→M03→M04)
- Transfer slip generation
- Quality parameter tracking
