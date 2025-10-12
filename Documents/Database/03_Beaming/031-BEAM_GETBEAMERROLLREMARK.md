# BEAM_GETBEAMERROLLREMARK

**Procedure Number**: 031 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get remarks/notes for a specific beam lot |
| **Operation** | SELECT |
| **Tables** | tblBeamingProcess |
| **Called From** | BeamingDataService.cs:1085 → BEAM_GETBEAMERROLLREMARK() |
| **Frequency** | Medium (when viewing beam details) |
| **Performance** | Fast (single record lookup) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot number |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_REMARK` | VARCHAR2(500) | Remark text |

### Returns (if cursor)

N/A - Returns single string value

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingProcess` - SELECT - Retrieves REMARK field for beam lot

**Transaction**: No (read-only)

---

## Business Logic (What it does and why)

Retrieves notes/remarks recorded for a specific beam lot. Used to display quality notes, issues, or special instructions associated with the beam.

**Workflow**:
1. User views beam details screen
2. System loads beam information
3. Calls this procedure to get remarks
4. Displays notes in remarks field

**Business Rules**:
- Returns empty string if no remarks
- Returns empty if beam lot not found
- Used for quality tracking
- May contain defect notes, operator observations

---

## Related Procedures

**Similar**: [014-WARP_GETWARPERROLLREMARK.md](../02_Warping/014-WARP_GETWARPERROLLREMARK.md)

---

## Query/Code Location

**File**: `BeamingDataService.cs`
**Method**: `BEAM_GETBEAMERROLLREMARK()`
**Line**: 1085-1115

```csharp
public string BEAM_GETBEAMERROLLREMARK(string P_BEAMLOT)
{
    string result = string.Empty;

    if (string.IsNullOrWhiteSpace(P_BEAMLOT))
        return result;

    if (!HasConnection())
        return result;

    BEAM_GETBEAMERROLLREMARKParameter dbPara = new BEAM_GETBEAMERROLLREMARKParameter();
    dbPara.P_BEAMLOT = P_BEAMLOT;

    BEAM_GETBEAMERROLLREMARKResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.BEAM_GETBEAMERROLLREMARK(dbPara);
        result = dbResult.R_REMARK;
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

**File**: 031/296 | **Progress**: 10.5%
