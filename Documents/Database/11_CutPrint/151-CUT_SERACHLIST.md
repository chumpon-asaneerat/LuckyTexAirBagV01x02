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

**Core Information (8 columns)**:
- `ITEMLOT`, `STARTDATE`, `ENDDATE`, `OPERATORID`, `MCNO`, `PRODUCTTYPEID`, `STATUS`, `REMARK`

**Barcode Measurements (8 columns)**:
- `WIDTHBARCODE1/2/3/4` - Barcode widths (mm)
- `DISTANTBARCODE1/2/3/4` - Barcode distances (mm)

**Cut Line Measurements (3 columns)**:
- `DISTANTLINE1/2/3` - Distance between cut lines (mm)

**Fabric Quality (2 columns)**:
- `DENSITYWARP`, `DENSITYWEFT` - Thread densities

**Machine Parameters (2 columns)**:
- `SPEED`, `TENSION` - Machine settings

**Width Measurements (3 columns)**:
- `BEFORE_WIDTH`, `AFTER_WIDTH`, `AFTER_WIDTH_END`

**Roll Positions (16 columns)**:
- `BEGINROLL_LINE1/2/3/4`, `ENDROLL_LINE1/2/3/4`
- `BEGINROLL2_LINE1/2/3/4`, `ENDROLL2_LINE1/2/3/4`

**Edge Measurements (2 columns)**:
- `SELVAGE_LEFT`, `SELVAGE_RIGHT`

**Suspension/Status Information (5 columns)**:
- `SUSPENDDATE`, `SUSPENDBY`, `SUSPENDSTARTDATE`
- `CLEARDATE`, `CLEARBY`, `CLEARREMARK`

**Additional Details (2 columns)**:
- `LENGTHPRINT`, `LENGTHDETAIL`

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
