# CUT_GETSLIP

**Procedure Number**: 149 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve cutting operation data for slip/report printing |
| **Operation** | SELECT |
| **Tables** | tblCutPrint |
| **Called From** | CutPrintDataService.cs:1155 → CUT_GETSLIPReportData() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMLOT` | VARCHAR2(50) | ✅ | Item lot number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns complete cutting operation record for report generation with **46 columns** (same structure as CUT_GETMCSUSPENDDATA):

| Column | Type | Description |
|--------|------|-------------|
| `ITEMLOT` | VARCHAR2(50) | Item lot number |
| `STARTDATE` | DATE | Operation start date |
| `ENDDATE` | DATE | Operation end date |
| `WIDTHBARCODE1` | NUMBER | Barcode 1 width (mm) |
| `WIDTHBARCODE2` | NUMBER | Barcode 2 width (mm) |
| `WIDTHBARCODE3` | NUMBER | Barcode 3 width (mm) |
| `WIDTHBARCODE4` | NUMBER | Barcode 4 width (mm) |
| `DISTANTBARCODE1` | NUMBER | Barcode 1 distance (mm) |
| `DISTANTBARCODE2` | NUMBER | Barcode 2 distance (mm) |
| `DISTANTBARCODE3` | NUMBER | Barcode 3 distance (mm) |
| `DISTANTBARCODE4` | NUMBER | Barcode 4 distance (mm) |
| `DISTANTLINE1` | NUMBER | Cut line 1 distance (mm) |
| `DISTANTLINE2` | NUMBER | Cut line 2 distance (mm) |
| `DISTANTLINE3` | NUMBER | Cut line 3 distance (mm) |
| `DENSITYWARP` | NUMBER | Warp density (ends/inch) |
| `DENSITYWEFT` | NUMBER | Weft density (picks/inch) |
| `SPEED` | NUMBER | Cutting/printing speed |
| `BEFORE_WIDTH` | NUMBER | Width before cutting |
| `AFTER_WIDTH` | NUMBER | Width after cutting |
| `BEGINROLL_LINE1` | VARCHAR2(50) | Begin roll number line 1 |
| `BEGINROLL_LINE2` | VARCHAR2(50) | Begin roll number line 2 |
| `BEGINROLL_LINE3` | VARCHAR2(50) | Begin roll number line 3 |
| `BEGINROLL_LINE4` | VARCHAR2(50) | Begin roll number line 4 |
| `ENDROLL_LINE1` | VARCHAR2(50) | End roll number line 1 |
| `ENDROLL_LINE2` | VARCHAR2(50) | End roll number line 2 |
| `ENDROLL_LINE3` | VARCHAR2(50) | End roll number line 3 |
| `ENDROLL_LINE4` | VARCHAR2(50) | End roll number line 4 |
| `OPERATORID` | VARCHAR2(50) | Operator ID |
| `SELVAGE_LEFT` | VARCHAR2(50) | Left selvage measurement |
| `SELVAGE_RIGHT` | VARCHAR2(50) | Right selvage measurement |
| `REMARK` | VARCHAR2(500) | Remarks |
| `PRODUCTTYPEID` | VARCHAR2(50) | Product type |
| `MCNO` | VARCHAR2(50) | Machine number |
| `STATUS` | VARCHAR2(10) | Operation status |
| `SUSPENDDATE` | DATE | Date suspended |
| `SUSPENDBY` | VARCHAR2(50) | Operator who suspended |
| `CLEARDATE` | DATE | Date cleared |
| `CLEARREMARK` | VARCHAR2(500) | Clearance remarks |
| `CLEARBY` | VARCHAR2(50) | Operator who cleared |
| `LENGTHPRINT` | NUMBER | Length printed |
| `SUSPENDSTARTDATE` | DATE | Original start date when suspended |
| `BEGINROLL2_LINE1` | VARCHAR2(50) | Second begin roll line 1 |
| `BEGINROLL2_LINE2` | VARCHAR2(50) | Second begin roll line 2 |
| `BEGINROLL2_LINE3` | VARCHAR2(50) | Second begin roll line 3 |
| `BEGINROLL2_LINE4` | VARCHAR2(50) | Second begin roll line 4 |
| `ENDROLL2_LINE1` | VARCHAR2(50) | Second end roll line 1 |
| `ENDROLL2_LINE2` | VARCHAR2(50) | Second end roll line 2 |
| `ENDROLL2_LINE3` | VARCHAR2(50) | Second end roll line 3 |
| `ENDROLL2_LINE4` | VARCHAR2(50) | Second end roll line 4 |
| `TENSION` | NUMBER | Tension setting |
| `LENGTHDETAIL` | VARCHAR2(500) | Detailed length information |
| `AFTER_WIDTH_END` | NUMBER | Width at end |

---

## Business Logic (What it does and why)

**Purpose**: Retrieve cutting operation data for printing cutting slip/report.

**When Used**:
- After cutting operation completes
- Generate cutting slip for documentation
- Print operation summary
- Quality documentation
- Customer delivery documentation

**Business Rules**:
1. P_ITEMLOT required
2. Returns complete cutting record
3. Used for report generation (RDLC)
4. Shows all cutting parameters and measurements
5. Part of production traceability

**Report Contents**:
- **Identification**: Item lot, dates, operator
- **Barcode Specs**: Width and distance for multiple barcodes (1-4)
- **Cut Specifications**: Line distances, densities
- **Quality Measurements**: Widths before/after, selvage
- **Machine Parameters**: Speed, tension
- **Roll Information**: Begin/end positions for multiple lines
- **Length Details**: Detailed length information

**Workflow**:
1. Cutting operation completes
2. Operator initiates report print
3. System calls CUT_GETSLIP with item lot
4. Retrieves all cutting data
5. Generates RDLC report
6. Prints cutting slip
7. Attached to finished product or documentation

---

## Related Procedures

**Related**: [147-CUT_GETFINISHINGDATA.md](./147-CUT_GETFINISHINGDATA.md) - Get source finishing data
**Related**: CUT_INSERTDATA - Records data that this retrieves
**Used in**: Report generation for cutting documentation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `CUT_GETSLIPReportData(string ITEMLOT)`
**Lines**: 1155-1245
**Comment**: "เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUT_GETSLIP Report"
*Translation*: "Added new for use with CUT_GETSLIP Report"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_GETSLIP(CUT_GETSLIPParameter para)`

**Parameter Class**: 1 parameter (P_ITEMLOT)
**Result Class**: 40+ columns (complete cutting record)

**Usage**: Report generation pages - prints cutting slip/summary

---

**File**: 149/296 | **Progress**: 50.3%
