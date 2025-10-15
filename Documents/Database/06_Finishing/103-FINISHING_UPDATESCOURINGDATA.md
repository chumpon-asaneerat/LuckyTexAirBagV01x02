# FINISHING_UPDATESCOURINGDATA

**Procedure Number**: 103 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update scouring record with PLC target values AND min/max specification ranges |
| **Operation** | UPDATE |
| **Tables** | tblFinishingScouring, tblFinishingScouringCondition |
| **Called From** | CoatingDataService.cs:2169 → FINISHING_UPDATESCOURINGDATA() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number to update |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag (START/FINISH) |
| `P_SAT` | NUMBER | ⚠️ | Saturator temperature target (°C) |
| `P_SAT_MIN/MAX` | NUMBER | ⚠️ | Saturator min/max specification limits (°C) |
| `P_WASHING1` | NUMBER | ⚠️ | Washing 1 temperature target (°C) |
| `P_WASHING1_MIN/MAX` | NUMBER | ⚠️ | Washing 1 min/max specification limits (°C) |
| `P_WASHING2` | NUMBER | ⚠️ | Washing 2 temperature target (°C) |
| `P_WASHING2_MIN/MAX` | NUMBER | ⚠️ | Washing 2 min/max specification limits (°C) |
| `P_HOTFLUE` | NUMBER | ⚠️ | Hot flue temperature target (°C) |
| `P_HOTFLUE_MIN/MAX` | NUMBER | ⚠️ | Hot flue min/max specification limits (°C) |
| `P_TEMP1-8` | NUMBER | ⚠️ | Temperature zone 1-8 target values (°C) |
| `P_TEMP1-8_MIN/MAX` | NUMBER | ⚠️ | Temperature zone 1-8 min/max limits (°C) |
| `P_SPEED` | NUMBER | ⚠️ | Machine speed target (m/min) |
| `P_SPEED_MIN/MAX` | NUMBER | ⚠️ | Speed min/max specification limits (m/min) |
| `P_MAINFRAMEWIDTH` | NUMBER | ⚠️ | Main frame width (mm) |
| `P_WIDTH_BE` | NUMBER | ⚠️ | Width before scouring (mm) |
| `P_WIDTH_AF` | NUMBER | ⚠️ | Width after scouring (mm) |
| `P_PIN2PIN` | NUMBER | ⚠️ | Pin to pin distance (mm) |
| `P_FINISHBY` | VARCHAR2(50) | ⚠️ | Operator who finished process |
| `P_ENDDATE` | DATE | ⚠️ | Process end timestamp |
| `P_LENGTH1-7` | NUMBER | ⚠️ | Length measurements at various checkpoints (m) |
| `P_ITMCODE` | VARCHAR2(50) | ⚠️ | Item code |
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number (validated) |
| `P_CUSTOMER` | VARCHAR2(50) | ⚠️ | Customer code |
| `P_STARTDATE` | DATE | ⚠️ | Process start timestamp |
| `P_REMARK` | VARCHAR2(500) | ⚠️ | Process remarks/notes |
| `P_HUMID_BF` | NUMBER | ⚠️ | Humidity before (%) |
| `P_HUMID_AF` | NUMBER | ⚠️ | Humidity after (%) |
| `P_GROUP` | VARCHAR2(50) | ⚠️ | Operator group/shift |

### Output (OUT)

None - Returns result value

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `RESULT` | VARCHAR2(50) | Success/failure message |

---

## Business Logic (What it does and why)

**Purpose**: Updates scouring process record with both target values AND min/max specification ranges. This is the comprehensive update that includes quality control limits for process capability analysis (Cp/Cpk calculations).

**When Used**:
- Quality engineer sets or updates specification ranges for the lot
- System loads condition data with quality limits
- Process engineer adjusts acceptable tolerance ranges
- Used when finalizing lot with complete quality documentation

**Business Rules**:
1. **Dual Data Update**: Updates both actual/target values AND specification limits in same transaction
2. **Quality Control Focus**: MIN/MAX ranges define acceptable process variation
3. **Required Validation**: FINISHLOT, WEAVINGLOT, and FLAG must be provided
4. **Process Capability**: MIN/MAX data enables Cp/Cpk statistical analysis
5. **Comprehensive Update**: All parameters optional except required IDs

**Workflow**:

**Scenario 1: Setting Quality Specifications**
1. Quality engineer opens lot "FS-2024-0125" for condition setup
2. Loads item specifications from master data
3. Sets target values and tolerance ranges:
   - P_TEMP5 = 173°C (target)
   - P_TEMP5_MIN = 168°C (lower control limit)
   - P_TEMP5_MAX = 178°C (upper control limit)
   - P_SPEED = 25 m/min (target)
   - P_SPEED_MIN = 23 m/min
   - P_SPEED_MAX = 27 m/min
4. System updates both process data and specification limits
5. PLC monitoring system uses limits for real-time alarms

**Scenario 2: Finalizing with Quality Data**
1. Operator completes scouring of lot
2. Final measurements recorded
3. Updates with complete dataset:
   - All temperature zones (targets + MIN/MAX)
   - Speed (target + MIN/MAX)
   - Final lengths, widths, humidity
   - Operator and timestamp data
4. System calculates process capability:
   - Cp = (UCL - LCL) / (6 × σ)
   - Cpk = min[(UCL - μ) / (3 × σ), (μ - LCL) / (3 × σ)]
5. Quality report shows compliance status

**Difference from FINISHING_UPDATESCOURING**:
- **FINISHING_UPDATESCOURING**: Updates process values (PV/SP) only
- **FINISHING_UPDATESCOURINGDATA**: Updates values + MIN/MAX specification limits (comprehensive quality data)

**Parameter Pattern**:
For each parameter, provides THREE values:
- Target/Actual value (e.g., TEMP1)
- Minimum specification (e.g., TEMP1_MIN)
- Maximum specification (e.g., TEMP1_MAX)

This enables:
- Real-time compliance checking
- Process capability analysis
- Quality documentation
- Automated alarm systems

**Why This Matters**:
- **Statistical Process Control**: MIN/MAX enables Cp/Cpk calculations for automotive certification
- **Quality Compliance**: Customer requirements for process capability documentation
- **Automated Monitoring**: PLC systems trigger alarms when values exceed MIN/MAX
- **Process Optimization**: Historical MIN/MAX data identifies process stability issues
- **Audit Requirements**: ISO/TS quality standards require specification tracking

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTSCOURING (creates initial record)
- FINISHING_GETSCOURINGCONDITION (loads specification ranges from master)
- FINISHING_SCOURINGPLCDATA (retrieves current specs)

**Downstream**:
- FINISHING_GETSCOURINGREPORT (uses MIN/MAX for capability analysis)
- Quality dashboards (process capability charts)

**Similar**:
- FINISHING_UPDATECOATINGDATA (same pattern for coating with MIN/MAX)
- FINISHING_UPDATEDRYERDATA (same pattern for dryer with MIN/MAX)

**Complementary**:
- FINISHING_UPDATESCOURING (updates process values without MIN/MAX)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_UPDATESCOURINGDATA(string P_FINISHLOT, string P_FLAG, ... 60+ parameters)`
**Lines**: 2169-2271

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_UPDATESCOURINGDATA(FINISHING_UPDATESCOURINGDATAParameter para)`
**Lines**: 6968-7041 (Parameter: 6968-7032, Result: 7038-7041)

---

**File**: 103/296 | **Progress**: 34.8%
