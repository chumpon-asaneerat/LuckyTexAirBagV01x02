# INSTINSPECTIONTESTRECORD

**Procedure Number**: 145 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert 100-meter quality test record with comprehensive measurements |
| **Operation** | INSERT |
| **Tables** | tblINSTestRecord |
| **Called From** | DataServicecs.cs:2247 → AddInspectionTestData() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_POPUPDATE` | DATE | ⬜ | Test popup/alert date |
| `P_SAVEDATE` | DATE | ⬜ | Test save date/time |
| `P_ACTUALLENGTH` | NUMBER | ⬜ | Actual counter length (meters) |
| `P_STDLENGTH` | NUMBER | ⬜ | Standard length (100M, 200M, etc.) |
| `P_DENW` | NUMBER | ⬜ | Density warp (ends per inch) |
| `P_DENF` | NUMBER | ⬜ | Density filling/weft (picks per inch) |
| `P_WIDTHALL` | NUMBER | ⬜ | Width overall measurement |
| `P_WIDTHPIN` | NUMBER | ⬜ | Width pin-to-pin measurement |
| `P_WIDTHCOAT` | NUMBER | ⬜ | Width coated area |
| `P_TRIML` | NUMBER | ⬜ | Trim left edge width |
| `P_TRIMR` | NUMBER | ⬜ | Trim right edge width |
| `P_FLOPPYL` | VARCHAR2(10) | ⬜ | Floppy left status (OK/NG) |
| `P_FLOPPYR` | VARCHAR2(10) | ⬜ | Floppy right status (OK/NG) |
| `P_UNWINDERSET` | NUMBER | ⬜ | Unwinder tension set value |
| `P_UNWINDERACT` | NUMBER | ⬜ | Unwinder tension actual value |
| `P_WINDERSET` | NUMBER | ⬜ | Winder tension set value |
| `P_WINDERACT` | NUMBER | ⬜ | Winder tension actual value |
| `P_HARDNESSL` | NUMBER | ⬜ | Hardness left measurement |
| `P_HARDNESSC` | NUMBER | ⬜ | Hardness center measurement |
| `P_HARDNESSR` | NUMBER | ⬜ | Hardness right measurement |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_TESTID` | VARCHAR2(50) | Generated test record ID (primary key) |

### Returns

Returns test ID string if successful, empty string on failure.

---

## Business Logic (What it does and why)

**Purpose**: Record comprehensive quality measurements at regular intervals (every 100 meters) during inspection.

**When Used**:
- System alerts operator every 100M of fabric
- Operator performs standard quality tests
- All measurements entered into test form
- System records complete test data
- Part of 100-meter quality monitoring system

**Business Rules**:
1. P_INSLOT required
2. Records 21 quality parameters
3. Tests performed at standard intervals (100M, 200M, 300M, etc.)
4. Returns R_TESTID for reference
5. Test data used for quality analysis and reporting

**Quality Test Parameters**:

**1. Length Measurements**:
- ACTUALLENGTH: Counter reading when test performed
- STDLENGTH: Standard test position (100M, 200M, etc.)

**2. Density (Thread Count)**:
- DENW: Warp density (ends per inch) - vertical threads
- DENF: Filling/weft density (picks per inch) - horizontal threads
- Critical for fabric strength and quality

**3. Width Measurements** (3 points):
- WIDTHALL: Overall width
- WIDTHPIN: Pin-to-pin width
- WIDTHCOAT: Coated area width
- Ensures dimensional consistency

**4. Trim Measurements**:
- TRIML/TRIMR: Left and right edge trim widths
- Monitors edge waste

**5. Edge Quality**:
- FLOPPYL/FLOPPYR: Left and right edge status (OK/NG)
- Floppy edges indicate quality issues

**6. Tension Settings** (Machine parameters):
- UNWINDERSET/ACT: Unwinder tension (set vs actual)
- WINDERSET/ACT: Winder tension (set vs actual)
- Affects fabric quality and consistency

**7. Hardness** (3 points):
- HARDNESSL/C/R: Left, center, right hardness
- Measures fabric stiffness/rigidity
- Checks uniformity across width

**100M Test Workflow**:
1. Fabric reaches 100M on counter
2. System popup alert: "Perform 100M test"
3. Operator pauses or continues at slow speed
4. Operator measures all parameters
5. Enters data into test form
6. System calls INSTINSPECTIONTESTRECORD with all measurements
7. Returns test ID
8. Test recorded in history
9. Process repeats at 200M, 300M, etc.

**Why Every 100M**:
- Detect quality drift during production
- Early warning of machine issues
- Customer quality requirements
- Statistical process control
- Compliance documentation

---

## Related Procedures

**Related**: [126-INS_EDIT100TESTRECORD.md](./126-INS_EDIT100TESTRECORD.md) - Edit test record
**Related**: [123-INS_DELETE100MRECORD.md](./123-INS_DELETE100MRECORD.md) - Delete test record
**Related**: GETINSTESTRECORDLIST - Retrieve test history
**Used in**: InspectionModulePage, InspectionRecordWindow

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `AddInspectionTestData(string insLotNo, DateTime? popupDate, decimal actualLen, decimal stdLen, InspectionTests test)`
**Lines**: 2247-2306
**Comment**: "Add Inspection Test Data"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INSTINSPECTIONTESTRECORD(INSTINSPECTIONTESTRECORDParameter para)`

**Parameter Class**: 21 parameters (comprehensive test data)
**Result Class**: Returns R_TESTID

**Usage**: InspectionModulePage - 100M test popup and data entry

---

**File**: 145/296 | **Progress**: 49.0%
