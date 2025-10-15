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

### Input (IN) - 45+ parameters

**Core Information**:
- `P_ITEMLOT` - Item lot number ✅
- `P_STARTDATE` - Operation start date
- `P_PRODUCTTYPEID` - Product type ✅
- `P_OPERATORID` - Operator ID ✅
- `P_MCNO` - Machine number ✅
- `P_REMARK` - Remarks

**Barcode Measurements (4 sets)**:
- `P_WIDTHBARCODE1/2/3/4` - Barcode widths
- `P_DISTANTBARCODE1/2/3/4` - Barcode distances

**Cut Line Measurements (3 sets)**:
- `P_DISTANTLINE1/2/3` - Distance between cut lines

**Fabric Quality**:
- `P_DENSITYWARP` - Warp density
- `P_DENSITYWEFT` - Weft density

**Machine Parameters**:
- `P_SPEED` - Cutting/printing speed
- `P_TENSION` - Tension setting (added 2017-06-28)

**Width Measurements**:
- `P_WIDTHBEFORE` - Width before cutting
- `P_WIDTHAFTER` - Width after cutting
- `P_WIDTHAFTER_END` - Width at end (added 2017-10-04)

**Roll Position Tracking (8 sets - 2 lines x 4 positions)**:
- `P_BEGINROLL_LINE1/2/3/4` - Begin roll positions
- `P_ENDROLL_LINE1/2/3/4` - End roll positions
- `P_2BEGINROLL_LINE1/2/3/4` - Second begin positions
- `P_2ENDROLL_LINE1/2/3/4` - Second end positions

**Edge Measurements**:
- `P_SELVAGELEFT` - Left selvage measurement
- `P_SELVAGERIGHT` - Right selvage measurement
- (Only for non-Scouring processes)

**Process Details**:
- `P_FINISHINGPROCESS` - Finishing process type
- `P_SUSPENDSTARTDATE` - Suspend date (if suspended)
- `P_LENGTHDETAIL` - Length details (added 2017-08-25)

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
