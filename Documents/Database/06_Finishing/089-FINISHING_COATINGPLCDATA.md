# FINISHING_COATINGPLCDATA

**Procedure Number**: 089 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve PLC process parameters and specification ranges for coating |
| **Operation** | SELECT |
| **Tables** | tblFinishingCoatingCondition (process specification data) |
| **Called From** | CoatingDataService.cs:560 → FINISHING_COATINGPLCDATA() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number (coating machine ID) |
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number (greige fabric lot) |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `STARTDATE` | DATE | Condition setup timestamp |
| `TEMP1-10_MIN` | NUMBER | Temperature zone 1-10 minimum limit (°C) |
| `TEMP1-10_MAX` | NUMBER | Temperature zone 1-10 maximum limit (°C) |
| `TEMP1-10` | NUMBER | Temperature zone 1-10 actual/target value (°C) |
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
| `TENUP_MIN` | NUMBER | Tension up minimum limit |
| `TENUP_MAX` | NUMBER | Tension up maximum limit |
| `TENUP` | NUMBER | Tension up actual/target value |
| `TENDOWN_MIN` | NUMBER | Tension down minimum limit |
| `TENDOWN_MAX` | NUMBER | Tension down maximum limit |
| `TENDOWN` | NUMBER | Tension down actual/target value |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves the process specification ranges (min/max limits) and target values for all PLC-controlled parameters during coating operations. Used to validate process conditions and display acceptable ranges to operators.

**When Used**:
- After operator enters machine number and weaving lot
- Loads specification ranges for quality control
- Displays on coating finishing page alongside actual PLC values
- Used for real-time monitoring: shows if parameters are within spec

**Business Rules**:
1. **Quality Specifications**: Each item-machine combination has specific temperature/speed ranges
2. **Min/Max Validation**: PLC values must stay within MIN/MAX ranges for quality compliance
3. **Process Consistency**: Target values guide operator to maintain consistent coating quality
4. **Real-time Monitoring**: MIN/MAX ranges enable automated alarm systems when values drift

**Workflow**:
1. Operator scans weaving lot on coating machine
2. System calls FINISHING_COATINGDATABYLOT first (gets process data)
3. Then calls this procedure to get specification ranges
4. UI displays:
   - Target values (from specifications)
   - Actual PLC values (from real-time data)
   - MIN/MAX ranges (from this procedure)
   - Color-coded warnings if actual is out of range
5. Operator adjusts machine to keep all parameters within spec

**Parameter Categories**:

**Temperature Zones (TEMP1-10)**:
- 10 heating zones in coating oven
- Each zone has min/max allowable range
- Critical for proper silicone curing
- Example: TEMP1_MIN=160°C, TEMP1_MAX=180°C, TEMP1=170°C (target)

**Process Temperatures**:
- SAT: Saturator chemical bath temperature
- HOTF: Hot flue air temperature for drying
- WASH1/WASH2: Washing zone temperatures for cleaning

**Mechanical Parameters**:
- SPEED: Line speed (fabric meters per minute)
- TENUP/TENDOWN: Fabric tension control (prevents wrinkles/stretching)

**Example Scenario**:
- Item: Coated airbag fabric requiring precise temperature profile
- Coating Machine C1, Weaving Lot WV-001
- Procedure returns: TEMP5_MIN=165, TEMP5_MAX=175, TEMP5=170
- If PLC shows actual temp = 178°C → Out of spec, triggers alarm
- Operator adjusts to bring back within 165-175 range

**Why This Matters**:
- Coating quality is extremely sensitive to temperature/speed variations
- Out-of-spec parameters = defective product (wrong coating thickness/cure)
- Real-time monitoring prevents scrap and rework
- Maintains consistency across shifts and operators

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item first)
- FINISHING_GETCOATINGCONDITION (retrieves full condition setup)

**Downstream**:
- Used alongside real-time PLC data for monitoring
- Feeds into quality control dashboards

**Similar**:
- FINISHING_SCOURINGPLCDATA (same pattern for scouring)
- FINISHING_DRYERPLCDATA (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_COATINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 560-681

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_COATINGPLCDATA(FINISHING_COATINGPLCDATAParameter para)`
**Lines**: 28418-28546 (estimated)

---

**File**: 89/296 | **Progress**: 30.1%
