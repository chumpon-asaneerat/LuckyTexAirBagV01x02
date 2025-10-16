# CUT_INSERTDATA

**Procedure Number**: 150 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert cutting/printing operation record with comprehensive measurements |
| **Operation** | INSERT |
| **Tables** | tblCutPrint |
| **Called From** | CutPrintDataService.cs:557 → CUT_INSERTDATA() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMLOT` | String | ✅ | Item lot number |
| `P_STARTDATE` | DateTime? | ⬜ | Operation start date |
| `P_PRODUCTTYPEID` | String | ✅ | Product type |
| `P_OPERATORID` | String | ✅ | Operator ID |
| `P_REMARK` | String | ⬜ | Remarks |
| `P_MCNO` | String | ✅ | Machine number |
| `P_WIDTH1` | Decimal? | ⬜ | Barcode 1 width (mm) |
| `P_WIDTH2` | Decimal? | ⬜ | Barcode 2 width (mm) |
| `P_WIDTH3` | Decimal? | ⬜ | Barcode 3 width (mm) |
| `P_WIDTH4` | Decimal? | ⬜ | Barcode 4 width (mm) |
| `P_DISTANTBAR1` | Decimal? | ⬜ | Barcode 1 distance (mm) |
| `P_DISTANTBAR2` | Decimal? | ⬜ | Barcode 2 distance (mm) |
| `P_DISTANTBAR3` | Decimal? | ⬜ | Barcode 3 distance (mm) |
| `P_DISTANTBAR4` | Decimal? | ⬜ | Barcode 4 distance (mm) |
| `P_DISTANTLINE1` | Decimal? | ⬜ | Cut line 1 distance (mm) |
| `P_DISTANTLINE2` | Decimal? | ⬜ | Cut line 2 distance (mm) |
| `P_DISTANTLINE3` | Decimal? | ⬜ | Cut line 3 distance (mm) |
| `P_DENWARP` | Decimal? | ⬜ | Warp density (ends/inch) |
| `P_DENWEFT` | Decimal? | ⬜ | Weft density (picks/inch) |
| `P_SPEED` | Decimal? | ⬜ | Cutting/printing speed |
| `P_WIDTHBE` | Decimal? | ⬜ | Width before cutting |
| `P_WIDTHAF` | Decimal? | ⬜ | Width after cutting |
| `P_BEGINLINE1` | String | ⬜ | Begin roll number line 1 |
| `P_BEGINLINE2` | String | ⬜ | Begin roll number line 2 |
| `P_BEGINLINE3` | String | ⬜ | Begin roll number line 3 |
| `P_BEGINLINE4` | String | ⬜ | Begin roll number line 4 |
| `P_ENDLINE1` | String | ⬜ | End roll number line 1 |
| `P_ENDLINE2` | String | ⬜ | End roll number line 2 |
| `P_ENDLINE3` | String | ⬜ | End roll number line 3 |
| `P_ENDLINE4` | String | ⬜ | End roll number line 4 |
| `P_SELVAGELEFT` | String | ⬜ | Left selvage measurement (excluded for Scouring) |
| `P_SELVAGERIGHT` | String | ⬜ | Right selvage measurement (excluded for Scouring) |
| `P_SUSPENSTARTDATE` | DateTime? | ⬜ | Suspend start date (if suspended) |
| `P_2BEGINLINE1` | String | ⬜ | Second begin roll line 1 |
| `P_2BEGINLINE2` | String | ⬜ | Second begin roll line 2 |
| `P_2BEGINLINE3` | String | ⬜ | Second begin roll line 3 |
| `P_2BEGINLINE4` | String | ⬜ | Second begin roll line 4 |
| `P_2ENDLINE1` | String | ⬜ | Second end roll line 1 |
| `P_2ENDLINE2` | String | ⬜ | Second end roll line 2 |
| `P_2ENDLINE3` | String | ⬜ | Second end roll line 3 |
| `P_2ENDLINE4` | String | ⬜ | Second end roll line 4 |
| `P_TENSION` | Decimal? | ⬜ | Tension setting (added 2017-06-28) |
| `P_LENGTHDETAIL` | String | ⬜ | Length details (added 2017-08-25) |
| `P_WIDTHAF_END` | Decimal? | ⬜ | Width at end (added 2017-10-04) |

### Output (OUT)

None (empty result indicates success)

### Returns

Boolean: true if insert successful, false on failure

---

## Business Logic (What it does and why)

**Purpose**: Record complete cutting/printing operation with all measurements and parameters.

**When Used**: When operator starts or completes cutting/printing operation on fabric roll.

**Business Rules**:
1. Required: ITEMLOT, PRODUCTTYPEID, MCNO, OPERATORID
2. Records 45+ parameters for complete traceability
3. **Selvage excluded for Scouring process** (special rule)
4. Supports suspension (SUSPENDSTARTDATE)
5. Multiple barcode and cut line measurements
6. Tracks begin/end positions for multiple cutting lines

**Cutting/Printing Process**:

**1. Setup Phase**:
- Load finishing lot data
- Configure machine parameters
- Set cutting specifications

**2. Operation Phase**:
- Measure barcode widths and distances (up to 4)
- Set cut line distances (up to 3 lines)
- Monitor fabric density (warp/weft)
- Control speed and tension
- Measure widths before/after cutting

**3. Roll Tracking**:
- Track roll positions for multiple cutting lines (up to 4)
- Begin and end roll numbers for each line
- Second set of positions (2BEGINROLL/2ENDROLL) for continued operations

**4. Quality Control**:
- Selvage measurements (left/right edges)
- Width verification (before/after/end)
- Density validation

**5. Completion/Suspension**:
- If complete: record all final measurements
- If suspended: record SUSPENDSTARTDATE for resume

**Special Handling**:
- **Scouring Process**: No selvage measurements (fabric edge processing)
- **Multiple Lines**: Support for 4 concurrent cutting lines
- **Tension Control**: Added 2017-06-28 for quality improvement
- **End Width**: Added 2017-10-04 for better width tracking

---

## Related Procedures

**Related**: [147-CUT_GETFINISHINGDATA.md](./147-CUT_GETFINISHINGDATA.md) - Load source data
**Related**: [146-CUT_GETCONDITIONBYITEMCODE.md](./146-CUT_GETCONDITIONBYITEMCODE.md) - Get specifications
**Related**: CUT_UPDATEDATA - Update existing record
**Related**: [149-CUT_GETSLIP.md](./149-CUT_GETSLIP.md) - Retrieve for reporting

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `CUT_INSERTDATA(...)` (45+ parameters)
**Lines**: 557-658
**Comment**: "เพิ่ม CUT_INSERTDATA" (Translation: "Added CUT_INSERTDATA")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_INSERTDATA(CUT_INSERTDATAParameter para)`

**Parameter Class**: 45+ parameters
**Result Class**: Empty result

**Enhancement History**:
- **2017-06-28**: Added P_TENSION parameter
- **2017-08-25**: Added P_LENGTHDETAIL parameter
- **2017-10-04**: Added P_WIDTHAFTER_END parameter

---

**File**: 150/296 | **Progress**: 50.7%
