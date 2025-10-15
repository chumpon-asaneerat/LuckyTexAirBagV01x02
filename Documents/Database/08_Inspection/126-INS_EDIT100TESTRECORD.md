# INS_EDIT100TESTRECORD

**Procedure Number**: 126 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit/update 100-meter test record with comprehensive quality measurements |
| **Operation** | UPDATE |
| **Tables** | tblINSTestRecord (assumed) |
| **Called From** | DataServicecs.cs:2174 → INS_EDIT100TESTRECORD() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_TESTID` | VARCHAR2(50) | ✅ | Test record unique identifier |
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_STDLENGTH` | VARCHAR2(50) | ⬜ | Standard length position (100M, 200M, etc.) |
| `P_ACTLENGTH` | NUMBER | ⬜ | Actual measured length |
| `P_NACTLENGTH` | NUMBER | ⬜ | Net actual length (after corrections) |
| `P_NDENW` | NUMBER | ⬜ | Net density warp (ends per inch) |
| `P_NDENF` | NUMBER | ⬜ | Net density filling/weft (picks per inch) |
| `P_NWIDTHALL` | NUMBER | ⬜ | Net width overall measurement |
| `P_NWIDTHPIN` | NUMBER | ⬜ | Net width pin-to-pin measurement |
| `P_NWIDTHCOAT` | NUMBER | ⬜ | Net width coated area |
| `P_NTRIML` | NUMBER | ⬜ | Net trim left edge width |
| `P_NTRIMR` | NUMBER | ⬜ | Net trim right edge width |
| `P_NFLOPPYL` | VARCHAR2(10) | ⬜ | Net floppy left edge status (OK/NG) |
| `P_NFLOPPYR` | VARCHAR2(10) | ⬜ | Net floppy right edge status (OK/NG) |
| `P_NUNWINDERSET` | NUMBER | ⬜ | Net unwinder tension set value |
| `P_NUNWINDERACT` | NUMBER | ⬜ | Net unwinder tension actual value |
| `P_NWINDERSET` | NUMBER | ⬜ | Net winder tension set value |
| `P_NWINDERACT` | NUMBER | ⬜ | Net winder tension actual value |
| `P_NHARDNESSL` | NUMBER | ⬜ | Net hardness left measurement |
| `P_NHARDNESSC` | NUMBER | ⬜ | Net hardness center measurement |
| `P_NHARDNESSR` | NUMBER | ⬜ | Net hardness right measurement |

### Output (OUT)

None (returns empty result object indicating success/failure)

### Returns

Empty result object. Success determined by non-null return value in C# code.

---

## Business Logic (What it does and why)

**Purpose**: Update comprehensive quality test measurements for a 100-meter inspection test record.

**When Used**: During inspection when operator needs to modify previously recorded 100M test measurements. This is the **100-meter quality check system** that measures critical fabric properties at regular intervals.

**Business Rules**:
1. Requires valid TESTID and INSLOT (both cannot be null/empty)
2. Updates complete set of quality measurements in one transaction
3. Test measurements include:
   - **Length**: Actual vs standard length verification
   - **Density**: Warp and weft thread density (fabric tightness)
   - **Width**: Multiple width measurements (overall, pin-to-pin, coated area)
   - **Trim**: Left and right edge trim widths
   - **Floppy**: Edge quality status (left/right)
   - **Tension**: Unwinder and winder tension settings and actuals
   - **Hardness**: Three-point hardness measurements (left, center, right)
4. All measurements are "Net" values (final values after adjustments/corrections)
5. Used when correcting previously entered test data

**Workflow**:
1. Operator opens Inspection Record Window with existing test data
2. Reviews 100M test record for specific test ID
3. Modifies any of the quality measurements (density, width, hardness, etc.)
4. System updates all test parameters in database
5. Grid refreshes to show updated test history
6. Ensures quality data accuracy for production reporting

**Quality Measurements Explained**:
- **Density (NDENW/NDENF)**: Thread count per inch - critical for fabric strength
- **Width measurements**: Verify fabric meets dimensional specifications
- **Trim width**: Edge waste tracking
- **Floppy edges**: Edge quality assessment (soft/unraveling edges are defects)
- **Tension values**: Machine parameters affecting fabric quality
- **Hardness**: Fabric stiffness/rigidity at three positions (uniformity check)

---

## Related Procedures

**Similar**: [123-INS_DELETE100MRECORD.md](./123-INS_DELETE100MRECORD.md) - Delete 100M test record
**Related**: INSTINSPECTIONTESTRECORD - Insert new 100M test record (not documented yet)
**Related**: GETINSTESTRECORDLIST - Retrieve test history (not documented yet)
**Upstream**: GETINSPECTIONLISTTESTBYITMCODE - Get test specifications by item code (not documented yet)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `INS_EDIT100TESTRECORD(string testID, string insLot, string stdLength, decimal? actLength, decimal? nactLength, decimal? ndenw, decimal? ndenf, decimal? nwidthAll, decimal? nwidthPin, decimal? nwidthCoat, decimal? ntrimL, decimal? ntrimR, string nfloppyL, string nfloppyR, decimal? nunwinderSet, decimal? nunwinderAct, decimal? nwinderSet, decimal? nwinderAct, decimal? nhardnessL, decimal? nhardnessC, decimal? nhardnessR)`
**Lines**: 2174-2233

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_EDIT100TESTRECORD(INS_EDIT100TESTRECORDParameter para)`
**Lines**: 23218-23290

**Parameter Class**: `INS_EDIT100TESTRECORDParameter`
**Lines**: 5818-5841 (21 parameters)

**Result Class**: `INS_EDIT100TESTRECORDResult`
**Lines**: 5847-5849

**Usage Location**: `LuckyTex.AirBag.Controls\Windows\InspectionRecordWindow.xaml.cs`
**Line**: 863-865
**Context**: Edit button click handler in 100M test record editing window

---

**File**: 126/296 | **Progress**: 42.6%
