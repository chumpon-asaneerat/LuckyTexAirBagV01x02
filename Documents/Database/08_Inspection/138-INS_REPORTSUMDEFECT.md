# INS_REPORTSUMDEFECT

**Procedure Number**: 138 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Generate defect summary statistics grouped by defect type for reporting |
| **Operation** | SELECT (with GROUP BY) |
| **Tables** | tblINSDefect (joined with defect code master) |
| **Called From** | DataServicecs.cs:1853 → GetINS_ReportSumDefect() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2(50) | ✅ | Defect ID reference |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns defect summary statistics grouped by defect type:

| Column | Type | Description |
|--------|------|-------------|
| `TOTALPOINT` | NUMBER | Total defect points for this defect type |
| `SHORTDEFECT` | NUMBER | Count of short defects |
| `LONGDEFECT` | NUMBER | Count of long defects |
| `COMSHORTDEFECT` | NUMBER | Compensated short defect count |

---

## Business Logic (What it does and why)

**Purpose**: Summarize defects by type with point totals and counts for inspection reporting.

**When Used**: Generating inspection reports that show defect breakdown by defect type (used in Report.xaml.cs).

**Business Rules**:
1. Both P_DEFECTID and P_ITMCODE required
2. Groups defects by defect code/type
3. Calculates:
   - Total points per defect type
   - Short defect counts (single position defects)
   - Long defect counts (spanning multiple meters)
   - Compensated defect counts
4. Used for quality analysis and reporting

**Defect Type Analysis**:
- **TOTALPOINT**: Sum of all defect points for each defect type
- **SHORTDEFECT**: Count of point defects (single location)
- **LONGDEFECT**: Count of continuous defects (start to end position)
- **COMSHORTDEFECT**: Short defects with compensation applied

**Report Usage**:
- Shows which defect types are most prevalent
- Helps identify quality issues by defect category
- Provides statistical analysis for quality improvement
- Customer reporting and quality documentation

---

## Related Procedures

**Related**: [129-INS_GETDEFECTLISTREPORT.md](./129-INS_GETDEFECTLISTREPORT.md) - Detailed defect list
**Related**: [135-INS_GETTOTALDEFECTBYINSLOT.md](./135-INS_GETTOTALDEFECTBYINSLOT.md) - Total defect count
**Used in**: Report generation for quality analysis

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `GetINS_ReportSumDefect(string defectId, string itmCode)`
**Lines**: 1853-1957

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_REPORTSUMDEFECT(INS_REPORTSUMDEFECTParameter para)`

**Parameter Class**: 2 parameters
**Result Class**: 4 columns (TOTALPOINT, SHORTDEFECT, LONGDEFECT, COMSHORTDEFECT)

**Usage**: `LuckyTex.AirBag.Pages\ClassData\Print\Report.xaml.cs` - Inspection report generation

---

**File**: 138/296 | **Progress**: 46.6%
