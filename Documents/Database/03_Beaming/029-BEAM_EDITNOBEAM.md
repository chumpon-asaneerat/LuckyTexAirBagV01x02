# BEAM_EDITNOBEAM

**Procedure Number**: 029 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit/change beam number for a completed beam roll |
| **Operation** | UPDATE |
| **Tables** | tblBeamingProcess |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingProcess` - UPDATE - Changes beam number

**Transaction**: Yes

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

**File**: `BeamingDataService.cs`
**Method**: `BEAM_EDITNOBEAM()`
**Line**: 1545-1577

```csharp
public string BEAM_EDITNOBEAM(string P_BEAMROLL, string P_OLDNO,
    string P_NEWNO, string P_OPERATOR)
{
    string result = string.Empty;

    if (string.IsNullOrWhiteSpace(P_BEAMROLL))
        return result;

    if (!HasConnection())
        return result;

    BEAM_EDITNOBEAMParameter dbPara = new BEAM_EDITNOBEAMParameter();
    dbPara.P_BEAMROLL = P_BEAMROLL;
    dbPara.P_OLDNO = P_OLDNO;
    dbPara.P_NEWNO = P_NEWNO;
    dbPara.P_OPERATOR = P_OPERATOR;

    BEAM_EDITNOBEAMResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.BEAM_EDITNOBEAM(dbPara);
        result = dbResult.R_RESULT;
    }
    catch (Exception ex)
    {
        ex.Err();
        result = string.Empty;
    }

    return result;
}
```

---

**File**: 029/296 | **Progress**: 9.8%
