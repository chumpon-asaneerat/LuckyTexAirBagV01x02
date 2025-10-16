# CUT_SERACHLIST

**Procedure Number**: 151 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search cutting/printing operations by date and machine |
| **Operation** | SELECT |
| **Tables** | tblCutPrint |
| **Called From** | CutPrintDataService.cs:405 → CUT_SERACHLIST() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_STARTDATE` | VARCHAR2(50) | ⬜ | Search date |
| `P_MC` | VARCHAR2(50) | ⬜ | Machine number filter |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns list of cutting/printing records matching search criteria with **46 columns** (same structure as CUT_GETMCSUSPENDDATA):

| Column | Type | Description |
|--------|------|-------------|
| `ITEMLOT` | String | Item lot number |
| `STARTDATE` | DateTime? | Operation start date |
| `ENDDATE` | DateTime? | Operation end date |
| `WIDTHBARCODE1` | Decimal? | Barcode 1 width (mm) |
| `WIDTHBARCODE2` | Decimal? | Barcode 2 width (mm) |
| `WIDTHBARCODE3` | Decimal? | Barcode 3 width (mm) |
| `WIDTHBARCODE4` | Decimal? | Barcode 4 width (mm) |
| `DISTANTBARCODE1` | Decimal? | Barcode 1 distance (mm) |
| `DISTANTBARCODE2` | Decimal? | Barcode 2 distance (mm) |
| `DISTANTBARCODE3` | Decimal? | Barcode 3 distance (mm) |
| `DISTANTBARCODE4` | Decimal? | Barcode 4 distance (mm) |
| `DISTANTLINE1` | Decimal? | Cut line 1 distance (mm) |
| `DISTANTLINE2` | Decimal? | Cut line 2 distance (mm) |
| `DISTANTLINE3` | Decimal? | Cut line 3 distance (mm) |
| `DENSITYWARP` | Decimal? | Warp density (ends/inch) |
| `DENSITYWEFT` | Decimal? | Weft density (picks/inch) |
| `SPEED` | Decimal? | Cutting/printing speed |
| `BEFORE_WIDTH` | Decimal? | Width before cutting |
| `AFTER_WIDTH` | Decimal? | Width after cutting |
| `BEGINROLL_LINE1` | String | Begin roll number line 1 |
| `BEGINROLL_LINE2` | String | Begin roll number line 2 |
| `BEGINROLL_LINE3` | String | Begin roll number line 3 |
| `BEGINROLL_LINE4` | String | Begin roll number line 4 |
| `ENDROLL_LINE1` | String | End roll number line 1 |
| `ENDROLL_LINE2` | String | End roll number line 2 |
| `ENDROLL_LINE3` | String | End roll number line 3 |
| `ENDROLL_LINE4` | String | End roll number line 4 |
| `OPERATORID` | String | Operator ID |
| `SELVAGE_LEFT` | String | Left selvage measurement |
| `SELVAGE_RIGHT` | String | Right selvage measurement |
| `REMARK` | String | Remarks |
| `PRODUCTTYPEID` | String | Product type |
| `MCNO` | String | Machine number |
| `STATUS` | String | Operation status |
| `SUSPENDDATE` | DateTime? | Date suspended |
| `SUSPENDBY` | String | Operator who suspended |
| `CLEARDATE` | DateTime? | Date cleared |
| `CLEARREMARK` | String | Clearance remarks |
| `CLEARBY` | String | Operator who cleared |
| `LENGTHPRINT` | Decimal? | Length printed |
| `SUSPENDSTARTDATE` | DateTime? | Original start date when suspended |
| `BEGINROLL2_LINE1` | String | Second begin roll line 1 |
| `BEGINROLL2_LINE2` | String | Second begin roll line 2 |
| `BEGINROLL2_LINE3` | String | Second begin roll line 3 |
| `BEGINROLL2_LINE4` | String | Second begin roll line 4 |
| `ENDROLL2_LINE1` | String | Second end roll line 1 |
| `ENDROLL2_LINE2` | String | Second end roll line 2 |
| `ENDROLL2_LINE3` | String | Second end roll line 3 |
| `ENDROLL2_LINE4` | String | Second end roll line 4 |
| `TENSION` | Decimal? | Tension setting |
| `LENGTHDETAIL` | String | Detailed length information |
| `AFTER_WIDTH_END` | Decimal? | Width at end |

---

## Business Logic (What it does and why)

**Purpose**: Search and retrieve cutting/printing operations by date and/or machine for review, reporting, or data lookup.

**When Used**:
- Cutting data search screens
- Historical operation lookup
- Production tracking and analysis
- Quality review by date/machine

**Business Rules**:
1. Both P_STARTDATE and P_MC are optional filters
2. If both provided: search by date AND machine
3. If only date: return all operations for that date
4. If only machine: return all operations for that machine
5. If neither: return all operations (or default date range)
6. Returns complete cutting records with all parameters

**Search Patterns**:
- **By Date**: Find all cutting operations on specific date
- **By Machine**: Find all operations on specific machine
- **By Both**: Find operations on date and machine combination
- Used for production reports, quality audits, issue investigation

**Data Returned** (46 columns - complete cutting records):
- See Returns section above for full column details
- Includes all measurements, machine parameters, and status information
- Used for comprehensive operation review and reporting

---

## Related Procedures

**Similar**: [140-INS_SEARCHINSPECTIONDATA.md](../08_Inspection/140-INS_SEARCHINSPECTIONDATA.md) - Inspection search equivalent
**Related**: [148-CUT_GETMCSUSPENDDATA.md](./148-CUT_GETMCSUSPENDDATA.md) - Get suspended operations only
**Related**: [149-CUT_GETSLIP.md](./149-CUT_GETSLIP.md) - Get specific operation for report

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `CUT_SERACHLIST(string P_STARTDATE, string P_MC)`
**Lines**: 405-505
**Comment**: "เพิ่มใหม่ CUT_SERACHLIST ใช้ในการ Load CUT_SERACHLIST"
*Translation*: "Added new CUT_SERACHLIST used for loading CUT_SERACHLIST"
**Note**: Typo in procedure name (SERACH instead of SEARCH) - preserved as-is in database

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_SERACHLIST(CUT_SERACHLISTParameter para)`

**Parameter Class**: 2 parameters (P_STARTDATE, P_MC)
**Result Class**: 40+ columns (complete cutting record)

---

**File**: 151/296 | **Progress**: 51.0%
