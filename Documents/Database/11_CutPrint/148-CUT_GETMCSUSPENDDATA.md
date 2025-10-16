# CUT_GETMCSUSPENDDATA

**Procedure Number**: 148 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve suspended cutting operations for specific machine to resume work |
| **Operation** | SELECT |
| **Tables** | tblCutPrint (WHERE suspended/not finished) |
| **Called From** | CutPrintDataService.cs:1050 → Cut_GetMCSuspendData() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_CUTMCNO` | VARCHAR2(50) | ✅ | Cutting machine number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns suspended cutting records with **46 columns**:

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

**Purpose**: Retrieve paused cutting operations for specific machine so operator can resume work.

**When Used**:
- Machine menu display
- Operator selects machine
- System shows suspended operations
- Allows resuming interrupted cutting work

**Business Rules**:
1. P_CUTMCNO required
2. Returns only suspended/unfinished operations
3. Filtered by cutting machine number
4. Shows complete cutting parameters for resume
5. Similar pattern to INS_GETMCSUSPENDDATA (inspection module)

**Suspension Scenarios**:
- Machine malfunction/maintenance
- Shift change
- Material shortage
- Quality issue requiring review
- Emergency stop

**Resume Workflow**:
1. Operator opens cutting machine menu
2. System calls CUT_GETMCSUSPENDDATA
3. Displays list of suspended operations
4. Operator selects operation to resume
5. System loads all saved parameters
6. Cutting continues from saved state

---

## Related Procedures

**Similar**: [132-INS_GETMCSUSPENDDATA.md](../08_Inspection/132-INS_GETMCSUSPENDDATA.md) - Inspection module equivalent
**Related**: [150-CUT_INSERTDATA.md](./150-CUT_INSERTDATA.md) - Records cutting with suspend capability
**Related**: CUT_UPDATEDATA - Updates to clear suspension

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `Cut_GetMCSuspendData(string CUTMCNO)`
**Lines**: 1050-1149

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_GETMCSUSPENDDATA(CUT_GETMCSUSPENDDATAParameter para)`

**Parameter Class**: Lines 9883-9886
**Result Class**: Lines 9892-9946 (46 columns)

**Usage**: CutPrintMCMenu.xaml.cs - Machine menu for selecting suspended operations

---

**File**: 148/296 | **Progress**: 50.0%
