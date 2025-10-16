# CUT_UPDATEDATA

**Procedure Number**: 152 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update existing cutting/printing operation record with new measurements or status |
| **Operation** | UPDATE |
| **Tables** | tblCutPrint |
| **Called From** | CutPrintDataService.cs:711, 862, 971 → CUT_UPDATEDATA() (3 overloads) |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

**Note**: This procedure has 3 overload signatures in C# with different parameter sets.

#### Overload 1: Full Update (48 parameters)
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMLOT` | String | ✅ | Item lot number (primary key) |
| `P_REMARK` | String | ⬜ | Remarks |
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
| `P_SELVAGELEFT` | String | ⬜ | Left selvage measurement |
| `P_SELVAGERIGHT` | String | ⬜ | Right selvage measurement |
| `P_STATUS` | String | ⬜ | Operation status |
| `P_LENGTHPRINT` | Decimal? | ⬜ | Length printed |
| `P_CLEARBY` | String | ⬜ | Operator who cleared suspension |
| `P_CLEARREMARK` | String | ⬜ | Clearance remarks |
| `P_CLEARDATE` | DateTime? | ⬜ | Date cleared |
| `P_SUSPENDDATE` | DateTime? | ⬜ | Date suspended |
| `P_2BEGINLINE1` | String | ⬜ | Second begin roll line 1 |
| `P_2BEGINLINE2` | String | ⬜ | Second begin roll line 2 |
| `P_2BEGINLINE3` | String | ⬜ | Second begin roll line 3 |
| `P_2BEGINLINE4` | String | ⬜ | Second begin roll line 4 |
| `P_2ENDLINE1` | String | ⬜ | Second end roll line 1 |
| `P_2ENDLINE2` | String | ⬜ | Second end roll line 2 |
| `P_2ENDLINE3` | String | ⬜ | Second end roll line 3 |
| `P_2ENDLINE4` | String | ⬜ | Second end roll line 4 |
| `P_TENSION` | Decimal? | ⬜ | Tension setting |
| `P_LENGTHDETAIL` | String | ⬜ | Length details |
| `P_WIDTHAF_END` | Decimal? | ⬜ | Width at end |

#### Overload 2: Full Update with End Date (49 parameters)
Same as Overload 1 plus:
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ENDDATE` | DateTime? | ⬜ | Operation end date |

#### Overload 3: Status Update Only (5 parameters)
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMLOT` | String | ✅ | Item lot number (primary key) |
| `P_STATUS` | String | ⬜ | Operation status |
| `P_CLEARBY` | String | ⬜ | Operator who cleared |
| `P_CLEARREMARK` | String | ⬜ | Clearance remarks |
| `P_CLEARDATE` | DateTime? | ⬜ | Date cleared |

### Output (OUT)

None (empty result indicates success)

### Returns

Boolean: true if update successful, false on failure

---

## Business Logic (What it does and why)

**Purpose**: Update cutting/printing operation data with new measurements, status changes, or suspension/clearance information.

**When Used**:
- Edit cutting parameters during or after operation
- Update status (suspend/resume/complete)
- Clear suspended operations
- Update measurement data
- Correct data entry errors

**Business Rules**:
1. P_ITEMLOT required (identifies record to update)
2. Three update scenarios supported:
   - **Full Update**: Edit all cutting parameters and measurements
   - **Full Update + End Date**: Complete operation with end timestamp
   - **Status Update**: Only update status and clearance info (lightweight)
3. Similar pattern to INS_UPDATEDATA (inspection module)
4. Supports partial updates (only specified fields updated)

**Update Scenarios**:

**1. Parameter Update (Overload 1)**:
- During operation: adjust measurements
- Fix incorrect barcode/cut line distances
- Update roll positions
- Modify selvage measurements
- Change tension or speed settings

**2. Complete Operation (Overload 2)**:
- Set end date when operation finishes
- Update all final measurements
- Record final width, length, and quality data
- Complete cutting/printing process

**3. Clear Suspension (Overload 3)**:
- Resume suspended operation
- Update status to active/complete
- Record who cleared and why
- Timestamp clearance

**Workflow Examples**:

**Scenario A: Edit During Operation**
1. Operator notices incorrect barcode width setting
2. Pauses machine
3. System calls CUT_UPDATEDATA (Overload 1)
4. Updates barcode widths and distances
5. Resume operation with corrected settings

**Scenario B: Complete Operation**
1. Cutting operation finishes
2. Operator enters final measurements
3. System calls CUT_UPDATEDATA (Overload 2)
4. Sets ENDDATE and final width/length data
5. Operation marked complete

**Scenario C: Clear Suspension**
1. Maintenance completed
2. Supervisor approves resume
3. System calls CUT_UPDATEDATA (Overload 3)
4. Updates STATUS, CLEARBY, CLEARDATE
5. Operation ready to resume

---

## Related Procedures

**Counterpart**: [150-CUT_INSERTDATA.md](./150-CUT_INSERTDATA.md) - Inserts new cutting record
**Related**: [148-CUT_GETMCSUSPENDDATA.md](./148-CUT_GETMCSUSPENDDATA.md) - Retrieves suspended operations
**Similar**: INS_UPDATEDATA (not yet documented) - Inspection module equivalent
**Used with**: [151-CUT_SERACHLIST.md](./151-CUT_SERACHLIST.md) - Search operations to update

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`

**Method 1** (Full Update): `CUT_UPDATEDATA(48 parameters)` - Lines 711-811
**Method 2** (with End Date): `CUT_UPDATEDATA(49 parameters)` - Lines 862-954
**Method 3** (Status Only): `CUT_UPDATEDATA(5 parameters)` - Lines 971-1006
**Comment**: "เพิ่ม CUT_UPDATEDATA" (Translation: "Added CUT_UPDATEDATA")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_UPDATEDATA(CUT_UPDATEDATAParameter para)`

**Parameter Class**: Lines 9606-9654 (48 parameters)
**Result Class**: Lines 9660+ (empty result class)

**Usage**:
- CutPrint pages for editing operations
- Status update screens
- Suspension/clearance management

---

**File**: 152/296 | **Progress**: 51.4%
