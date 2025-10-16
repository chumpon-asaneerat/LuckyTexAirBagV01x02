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
