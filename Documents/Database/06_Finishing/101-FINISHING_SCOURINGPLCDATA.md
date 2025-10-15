# FINISHING_SCOURINGPLCDATA

**Procedure Number**: 101 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve PLC process parameters and specification ranges for scouring |
| **Operation** | SELECT |
| **Tables** | tblFinishingScouringCondition (process specification data) |
| **Called From** | CoatingDataService.cs:1752 → FINISHING_SCOURINGPLCDATA() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number (scouring machine ID) |
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number (greige fabric lot) |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `STARTDATE` | DATE | Condition setup timestamp |
| `TEMP1-8_MIN` | NUMBER | Temperature zone 1-8 minimum limit (°C) |
| `TEMP1-8_MAX` | NUMBER | Temperature zone 1-8 maximum limit (°C) |
| `TEMP1-8` | NUMBER | Temperature zone 1-8 actual/target value (°C) |
| `SPEED_MIN` | NUMBER | Machine speed minimum limit (m/min) |
| `SPEED_MAX` | NUMBER | Machine speed maximum limit (m/min) |
| `SPEED` | NUMBER | Machine speed actual/target value (m/min) |
| `SAT_MIN` | NUMBER | Saturator temperature minimum limit (°C) |
| `SAT_MAX` | NUMBER | Saturator temperature maximum limit (°C) |
| `SAT` | NUMBER | Saturator temperature actual/target (°C) |
| `HOTF_MIN` | NUMBER | Hot flue temperature minimum limit (°C) |
| `HOTF_MAX` | NUMBER | Hot flue temperature maximum limit (°C) |
| `HOTF` | NUMBER | Hot flue temperature actual/target (°C) |
| `WASH1_MIN` | NUMBER | Washing zone 1 temperature minimum (°C) |
| `WASH1_MAX` | NUMBER | Washing zone 1 temperature maximum (°C) |
| `WASH1` | NUMBER | Washing zone 1 temperature actual/target (°C) |
| `WASH2_MIN` | NUMBER | Washing zone 2 temperature minimum (°C) |
| `WASH2_MAX` | NUMBER | Washing zone 2 temperature maximum (°C) |
| `WASH2` | NUMBER | Washing zone 2 temperature actual/target (°C) |
| `TEMP9` | NUMBER | Temperature zone 9 actual/target (°C) |
| `TEMP10` | NUMBER | Temperature zone 10 actual/target (°C) |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves the process specification ranges (min/max limits) and target values for all PLC-controlled parameters during scouring operations. Used to validate process conditions and display acceptable ranges to operators.

**When Used**:
- After operator enters machine number and weaving lot
- Loads specification ranges for quality control
- Displays on scouring finishing page alongside actual PLC values
- Used for real-time monitoring: shows if parameters are within spec

**Business Rules**:
1. **Quality Specifications**: Each item-machine combination has specific temperature/speed ranges
2. **Min/Max Validation**: PLC values must stay within MIN/MAX ranges for quality compliance
3. **Process Consistency**: Target values guide operator to maintain consistent scouring quality
4. **Real-time Monitoring**: MIN/MAX ranges enable automated alarm systems when values drift

**Workflow**:
1. Operator scans weaving lot on scouring machine
2. System calls FINISHING_SCOURINGDATABYLOT first (gets process data)
3. Then calls this procedure to get specification ranges
4. UI displays:
   - Target values (from specifications)
   - Actual PLC values (from real-time data)
   - MIN/MAX ranges (from this procedure)
   - Color-coded warnings if actual is out of range
5. Operator adjusts machine to keep all parameters within spec

**Parameter Categories**:

**Temperature Zones (TEMP1-10)**:
- 10 heating zones in scouring oven
- Each zone has min/max allowable range (zones 1-8 have full MIN/MAX tracking)
- Critical for proper fabric cleaning and preparation
- Example: TEMP3_MIN=165°C, TEMP3_MAX=175°C, TEMP3=170°C (target)

**Process Temperatures**:
- SAT: Saturator chemical bath temperature (cleaning agent application)
- HOTF: Hot flue air temperature for initial drying
- WASH1/WASH2: Washing zone temperatures for chemical removal

**Mechanical Parameters**:
- SPEED: Line speed (fabric meters per minute)

**Example Scenario**:
- Item: Airbag fabric requiring precise scouring profile
- Scouring Machine SC1, Weaving Lot WV-001
- Procedure returns: TEMP5_MIN=168, TEMP5_MAX=178, TEMP5=173
- If PLC shows actual temp = 182°C → Out of spec, triggers alarm
- Operator adjusts to bring back within 168-178 range

**Why This Matters**:
- Scouring quality is extremely sensitive to temperature variations
- Improper temperatures = incomplete cleaning or fabric damage
- Out-of-spec parameters = defective product (residual chemicals, shrinkage issues)
- Real-time monitoring prevents scrap and rework
- Maintains consistency across shifts and operators

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item first)
- FINISHING_GETSCOURINGCONDITION (retrieves full condition setup)

**Downstream**:
- Used alongside real-time PLC data for monitoring
- Feeds into quality control dashboards

**Similar**:
- FINISHING_COATINGPLCDATA (same pattern for coating)
- FINISHING_DRYERPLCDATA (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_SCOURINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 1752-1826

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_SCOURINGPLCDATA(FINISHING_SCOURINGPLCDATAParameter para)`
**Lines**: 7507-7562 (Parameter: 7507-7511, Result: 7517-7562)

---

**File**: 101/296 | **Progress**: 34.1%
