# INS_GETDEFECTLISTREPORT

**Procedure Number**: 129 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete defect list for inspection lot reporting with defect codes and points |
| **Operation** | SELECT |
| **Tables** | tblINSDefect (joined with defect code master) |
| **Called From** | DataServicecs.cs:1960 → ins_GetDefectListReport() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2(50) | ⬜ | Defect ID filter (can be null for all defects) |
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns list of defect records with the following columns:

| Column | Type | Description |
|--------|------|-------------|
| `DEFECTID` | VARCHAR2(50) | Defect record unique identifier |
| `INSPECTIONLOT` | VARCHAR2(50) | Inspection lot number |
| `DEFECTCODE` | VARCHAR2(50) | Defect type code (e.g., "HL", "WB", "SL") |
| `LENGTH1` | NUMBER | Start length position (meters) |
| `LENGTH2` | NUMBER | End length position (for long defects) |
| `POSITION` | NUMBER | Width position (1-9, left to right) |
| `FLAG` | VARCHAR2(10) | Defect status flag |
| `DEFECTPOINT` | NUMBER | Point value of this defect |
| `DEFECTPOINT100` | NUMBER | Defect points for 100M grading |
| `COMPENSATELENGTH` | NUMBER | Compensation length for this defect |
| `DELETEBY` | VARCHAR2(50) | Operator who deleted (if deleted) |
| `DELETEREMARK` | VARCHAR2(200) | Deletion reason/remark |
| `DESCRIPTION_EN` | VARCHAR2(200) | English description of defect type |

---

## Business Logic (What it does and why)

**Purpose**: Retrieve comprehensive defect list for inspection lot reporting and printing. This is the **reporting version** of defect list retrieval (replaces older GETINSDEFECTLIST).

**When Used**:
- Generating inspection reports (printed reports)
- Displaying defect summary on inspection documentation
- Quality review and analysis
- Customer quality documentation

**Business Rules**:
1. Requires valid INSLOT (cannot be null/empty)
2. DEFECTID parameter is optional - if null/empty, returns all defects for the lot
3. Returns defect details WITH defect code descriptions (joined with defect master table)
4. Includes both active and deleted defects (DELETEBY field indicates deletion status)
5. Provides both regular defect points and 100M section points
6. Includes compensation length for defects that require fabric cut compensation

**Data Enrichment**:
- Joins defect records with defect code master table
- Provides `DESCRIPTION_EN` (English defect description) for reporting
- Calculates `DEFECTPOINT100` for 100-meter grading system
- Shows `COMPENSATELENGTH` for defects requiring length adjustment

**Report Usage Pattern**:
1. **Inspection Report Generation** (Report.xaml.cs):
   - Called when printing inspection reports
   - Retrieves complete defect list for the lot
   - Formats defect data for RDLC report
   - Shows defect code, position, length, and points
   - Used in multiple report templates (RepMasterForm.xaml.cs)

2. **Defect List Processing**:
   - C# code calculates defect length for long defects (LENGTH2 - LENGTH1)
   - Truncates decimal values for display (1 decimal place)
   - Orders defects by position for report readability

**Difference from Similar Procedures**:
- **vs GETINSDEFECTLIST**: This is the newer reporting version with enhanced fields
- **vs GetDefectList**: That's for editing, this is for reporting/printing
- Includes DESCRIPTION_EN for customer-facing reports
- Added 2016-02-08 to replace older defect list procedures

---

## Related Procedures

**Similar**: GETINSDEFECTLIST - Older defect list procedure (legacy, not documented)
**Related**: GetDefectList - Get defect list for editing in DefectListWindow (not documented)
**Related**: [128-INS_GET100MDEFECTPOINT.md](./128-INS_GET100MDEFECTPOINT.md) - Calculate 100M defect points
**Related**: GETDEFECTCODEDETAIL - Get defect code master data (not documented)
**Upstream**: Used after inspection completion for reporting

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `ins_GetDefectListReport(string insLotNo, string defectId)`
**Lines**: 1960-2038 (includes defect length calculation logic)
**Note**: Comment indicates "ปรับเพิ่มใหม่ใช้แทน GETINSDEFECTLIST ใน report Inspection ( Add date 08/02/16 )"
*Translation*: "Adjusted/added new to replace GETINSDEFECTLIST in inspection report (Added 2016-02-08)"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETDEFECTLISTREPORT(INS_GETDEFECTLISTREPORTParameter para)`
**Lines**: 23026-23080

**Parameter Class**: `INS_GETDEFECTLISTREPORTParameter`
**Lines**: 5715-5719 (2 parameters)

**Result Class**: `INS_GETDEFECTLISTREPORTResult`
**Lines**: 5725-5740 (13 columns)

**Usage Locations**:
1. `LuckyTex.AirBag.Pages\ClassData\Print\Report.xaml.cs`
   - Lines: 700, 993, 1293, 1592 (multiple report generation methods)
   - Context: Binding defect data to RDLC inspection reports

2. `LuckyTex.AirBag.Pages\ClassData\Print\RepMasterForm.xaml.cs`
   - Used in master form report generation

---

**File**: 129/296 | **Progress**: 43.6%
