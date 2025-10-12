# WARP_GETWARPERROLLREMARK

**Procedure Number**: 014 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get remark text for warper lot |
| **Operation** | SELECT |
| **Tables** | tblWarpingProcess |
| **Called From** | WarpingDataService.cs:1370 → WARP_GETWARPERROLLREMARK() |
| **Frequency** | Medium (displaying lot details) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warper lot number |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_REMARK` | VARCHAR2(200) | Remark text for the lot |

### Returns (if cursor)

N/A - Returns single string value

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - SELECT - Remark field only

**Transaction**: No (read-only operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpingprocess_lot ON tblWarpingProcess(WARPERLOT);
```

---

## Business Logic (What it does and why)

Retrieves remark/notes text for specific warper lot. Displays operator notes about production issues, quality concerns, or special instructions recorded during warping. Used when viewing lot details to show any important notes left by operators or supervisors.

**Workflow**:
1. UI displays warper lot details
2. Calls this procedure to get remarks separately
3. Returns remark text string
4. UI displays remarks in notes/comments section
5. Shows operator observations:
   - Production issues encountered
   - Quality concerns
   - Special handling notes
   - Troubleshooting steps taken

**Business Rules**:
- Returns empty string if no remark
- Remarks entered by operators during production
- May include troubleshooting notes
- Important for quality issues or special handling

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates lot with remark
**Downstream**: None - Display only
**Similar**: [BEAM_GETBEAMERROLLREMARK.md](../03_Beaming/BEAM_GETBEAMERROLLREMARK.md) - Similar remark retrieval

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_GETWARPERROLLREMARK()`
**Line**: 1370-1400

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public string WARP_GETWARPERROLLREMARK(string P_WARPLOT)
{
    string result = string.Empty;

    // Validation: warper lot required
    if (string.IsNullOrWhiteSpace(P_WARPLOT))
        return result;

    if (!HasConnection())
        return result;

    WARP_GETWARPERROLLREMARKParameter dbPara = new WARP_GETWARPERROLLREMARKParameter();
    dbPara.P_WARPLOT = P_WARPLOT;

    WARP_GETWARPERROLLREMARKResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_GETWARPERROLLREMARK(dbPara);

        // Extract remark string
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

**File**: 014/296 | **Progress**: 4.7%
