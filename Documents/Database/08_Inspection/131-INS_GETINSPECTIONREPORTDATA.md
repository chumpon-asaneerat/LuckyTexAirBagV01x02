# INS_GETINSPECTIONREPORTDATA

**Procedure Number**: 131 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete inspection lot data with enriched master data for report generation |
| **Operation** | SELECT (with JOINs) |
| **Tables** | tblINSLot (joined with product, customer, machine masters) |
| **Called From** | DataServicecs.cs:2408 → GetInspectionReportData() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INS_LOT` | VARCHAR2(50) | ✅ | Inspection lot number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns enriched inspection lot record with **38 columns** including master data lookups:

**Core Identification:**
- `INSPECTIONLOT` - Inspection lot number
- `FINISHINGLOT` - Finishing lot number
- `ITEMCODE` - Product item code
- `DEFECTID` - Defect record ID reference
- `TESTRECORDID` - 100M test record ID reference
- `INSPECTIONID` - Inspection process identifier

**Date/Time:**
- `STARTDATE` - Inspection start date/time
- `ENDDATE` - Inspection end date/time
- `STARTDATE1` - Alternative start date
- `SUSPENDDATE` - Suspension date (if suspended)

**Measurements:**
- `GROSSLENGTH` - Gross fabric length (meters)
- `NETLENGTH` - Net length after defects (meters)
- `GROSSWEIGHT` - Gross weight (kg)
- `NETWEIGHT` - Net weight (kg)
- `CONFIRMSTARTLENGTH` - Confirmed starting length

**Quality & Grade:**
- `GRADE` - Final quality grade (A, B, C, etc.)
- `RETEST` - Retest flag

**Customer/Product (WITH JOINS):**
- `CUSTOMERID` - Customer ID
- `CUSTOMERNAME` - **Customer name (from master table join)**
- `CUSTOMERTYPE` - Customer type classification
- `PRODUCTTYPEID` - Product type ID
- `PRODUCTNAME` - **Product name (from master table join)**
- `PARTNO` - **Part number (from item master join)**
- `LOADINGTYPE` - Loading/packaging type

**Machine:**
- `MCNO` - Machine number
- `MCNAME` - **Machine name (from machine master join)**

**Operators/Process:**
- `INSPECTEDBY` - Inspector operator ID
- `SHIFT_ID` - Production shift
- `SHIFT_REMARK` - Shift remark/notes

**Status/Flags:**
- `FINISHFLAG` - Inspection completion flag
- `PREITEMCODE` - Previous item code (if changed)
- `PEINSPECTIONLOT` - Previous inspection lot (if re-inspection)

**Suspension/Clearance:**
- `SUSPENDBY` - Operator who suspended
- `CLEARBY` - Operator who cleared
- `CLEARREMARK` - Clearance remarks

**References:**
- `ATTACHID` - Attachment file ID
- `DEFECTFILENAME` - Defect image filename
- `REMARK` - General remarks

---

## Business Logic (What it does and why)

**Purpose**: Retrieve comprehensive inspection lot data **with enriched master data** for generating printed inspection reports.

**When Used**: During **inspection report generation** (RDLC reports) to provide complete lot information with descriptive names for customer-facing documents.

**Business Rules**:
1. Requires valid P_INS_LOT (inspection lot number)
2. **Performs JOINs with master tables** to enrich data:
   - Joins with customer master → adds CUSTOMERNAME
   - Joins with product/item master → adds PRODUCTNAME, PARTNO
   - Joins with machine master → adds MCNAME
3. Returns single record (first match) for the inspection lot
4. Used exclusively for report generation (not for data entry screens)
5. Comment indicates: "เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Inspection Report"
   - Translation: "Added new for use with Inspection Report"

**Report Generation Workflow**:
1. User requests inspection report print
2. System calls INS_GETINSPECTIONREPORTDATA with inspection lot number
3. Stored procedure:
   - Retrieves inspection lot record
   - Joins with customer master to get customer name
   - Joins with product master to get product name and part number
   - Joins with machine master to get machine name
4. Returns enriched data with descriptive names
5. Report uses this data to populate RDLC report fields
6. Customer sees readable names instead of cryptic codes

**Used in Multiple Report Types** (Report.xaml.cs):
- `RepInspection()` - Standard inspection report
- `RepTestInspection()` - Test inspection report (with grade/length override)
- `RepInspectionRemark()` - Inspection report with shift remarks
- `RepTestInspectionRemark()` - Test inspection report with shift remarks

**Difference from INS_GETFINISHINSLOTDATA**:
- **INS_GETFINISHINSLOTDATA**: Query by finishing lot, returns raw data
- **INS_GETINSPECTIONREPORTDATA**: Query by inspection lot, returns enriched data with JOINed master data
- This procedure is **reporting-focused** with descriptive names
- Other procedure is **data entry-focused** with IDs only

**Enhancement History**:
- Added SHIFT_ID and SHIFT_REMARK: 2016-05-13
- Added CONFIRMSTARTLENGTH: 2022-08-23
- Added RESETSTARTLENGTH: 2022-10-17 (commented in code)

---

## Related Procedures

**Similar**: [130-INS_GETFINISHINSLOTDATA.md](./130-INS_GETFINISHINSLOTDATA.md) - Get by finishing lot (data entry version)
**Related**: [129-INS_GETDEFECTLISTREPORT.md](./129-INS_GETDEFECTLISTREPORT.md) - Get defect list for reports
**Related**: INS_GETCUTSAMPLELIST - Get cut sample data for reports (documented earlier)
**Upstream**: Called after inspection completion for report printing
**Master Data**: Depends on customer, product, machine master tables

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `GetInspectionReportData(string insLotNo)`
**Lines**: 2408-2493
**Comment**: "เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Inspection Report" (Translation: "Added new for use with Inspection Report")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETINSPECTIONREPORTDATA(INS_GETINSPECTIONREPORTDATAParameter para)`
**Lines**: 22851-22934

**Parameter Class**: `INS_GETINSPECTIONREPORTDATAParameter`
**Lines**: 5599-5602 (1 parameter)

**Result Class**: `INS_GETINSPECTIONREPORTDATAResult`
**Lines**: 5608-5651 (38 columns with master data)

**Usage Locations**:
1. `LuckyTex.AirBag.Pages\ClassData\Print\Report.xaml.cs`
   - Line 530: RepInspection() - Standard report
   - Line 824: RepTestInspection() - Test report with overrides
   - Line 1116: RepInspectionRemark() - Report with shift remarks
   - Line 1416: RepTestInspectionRemark() - Test report with shift remarks

2. `LuckyTex.AirBag.Pages\ClassData\Print\RepMasterForm.xaml.cs`
   - Used in master form report generation

3. `LuckyTex.AirBag.Pages\Windows\08 - Inspection\ShiftRemarkWindow.xaml.cs`
   - Used when displaying/editing shift remarks

---

**File**: 131/296 | **Progress**: 44.3%
