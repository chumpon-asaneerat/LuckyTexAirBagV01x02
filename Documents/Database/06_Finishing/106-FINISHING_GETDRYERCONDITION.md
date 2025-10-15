# FINISHING_GETDRYERCONDITION

**Procedure Number**: 106 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve dryer machine condition parameters (specifications) for an item on a specific machine |
| **Operation** | SELECT |
| **Tables** | tblFinishingDryerCondition, tblItemCondition (inferred) |
| **Called From** | CoatingDataService.cs:2521 → GetFINISHING_GETDRYERCONDITIONDataList() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2 | ✅ | Item code to get dryer specifications for |
| `P_MCNO` | VARCHAR2 | ✅ | Machine number (dryer machine ID) |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2 | Item code |
| `MCNO` | VARCHAR2 | Machine number |
| `WIDTH_BE_HEAT_MAX` | NUMBER | Fabric width before heat - maximum spec (cm) |
| `WIDTH_BE_HEAT_MIN` | NUMBER | Fabric width before heat - minimum spec (cm) |
| `ACCPRESURE` | NUMBER | Accumulator pressure setting (bar) |
| `ASSTENSION` | NUMBER | Assist tension setting |
| `ACCARIDENSER` | NUMBER | Accumulator air density setting |
| `CHIFROT` | NUMBER | Chi front setting |
| `CHIREAR` | NUMBER | Chi rear setting |
| `DRYERTEMP1` | NUMBER | Dryer temperature zone 1 target (°C) |
| `DRYERTEMP1_MARGIN` | NUMBER | Temperature tolerance margin (±°C) |
| `SPEED` | NUMBER | Machine speed target (m/min) |
| `SPEED_MARGIN` | NUMBER | Speed tolerance margin (±m/min) |
| `STEAMPRESSURE` | NUMBER | Steam pressure setting (bar) |
| `DRYERUPCIRCUFAN` | NUMBER | Dryer upper circulation fan speed |
| `DRYERDOWNCIRCUFAN` | NUMBER | Dryer lower circulation fan speed |
| `EXHAUSTFAN` | NUMBER | Exhaust fan speed |
| `WIDTH_AF_HEAT` | NUMBER | Fabric width after heat target (cm) |
| `WIDTH_AF_HEAT_MARGIN` | NUMBER | Width after heat tolerance (±cm) |
| `HUMIDITY_MAX` | NUMBER | Maximum humidity spec (%) |
| `HUMIDITY_MIN` | NUMBER | Minimum humidity spec (%) |
| `SATURATOR_CHEM` | NUMBER | Saturator chemical concentration (%) |
| `SATURATOR_CHEM_MARGIN` | NUMBER | Chemical concentration tolerance (±%) |
| `WASHING1` | NUMBER | Washing zone 1 setting |
| `WASHING1_MARGIN` | NUMBER | Washing 1 tolerance |
| `WASHING2` | NUMBER | Washing zone 2 setting |
| `WASHING2_MARGIN` | NUMBER | Washing 2 tolerance |

---

## Business Logic

**Purpose**: Get dryer machine condition parameters (target values and tolerances) for a specific item on a specific machine. Used for machine setup and process control.

**When Used**: When operator sets up dryer machine for a new production lot, this procedure provides the standard operating conditions for the item-machine combination.

**Workflow**:
1. Operator selects item code and dryer machine for new production run
2. System calls this procedure with item code and machine number
3. Returns all dryer condition parameters including:
   - Temperature and speed targets with tolerance margins
   - Fabric width specifications (before and after heating)
   - Pressure, tension, and fan speed settings
   - Humidity control limits
   - Chemical concentration (if applicable)
   - Washing zone settings (if applicable)
4. UI populates machine setup screen with standard values
5. Operator can adjust within tolerance margins if needed
6. System validates actual readings against specs during production

**Business Rules**:
- Each item+machine combination has unique dryer conditions
- Margins define acceptable variation from target (e.g., SPEED ± SPEED_MARGIN)
- Different items require different drying parameters based on:
  - Fabric thickness
  - Fiber type
  - Customer requirements
  - Coating type (if coated)
- Conditions ensure:
  - Proper moisture removal
  - Dimensional stability (width control)
  - No damage from excessive heat
  - Consistent quality

**Key Parameters Explained**:
- **WIDTH_BE_HEAT**: Input width specification
- **WIDTH_AF_HEAT**: Output width specification (accounts for shrinkage/stretching)
- **DRYERTEMP1**: Main zone temperature (may have multiple zones)
- **SPEED**: Fabric speed through dryer (affects dwell time and drying effectiveness)
- **CIRCUFAN**: Circulation fans distribute hot air evenly
- **EXHAUSTFAN**: Removes moisture-laden air
- **HUMIDITY**: Final moisture content target

---

## Related Procedures

**Upstream**:
- None (master data reference)

**Downstream**:
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Uses conditions for new dryer lot
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - Validates against conditions

**Similar**:
- [090-FINISHING_GETCOATINGCONDITION.md](./090-FINISHING_GETCOATINGCONDITION.md) - Get coating conditions
- [096-FINISHING_GETSCOURINGCONDITION.md](./096-FINISHING_GETSCOURINGCONDITION.md) - Get scouring conditions

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `GetFINISHING_GETDRYERCONDITIONDataList(string itm_code, string mcNo)`
**Lines**: 2500-2577

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_GETDRYERCONDITIONParameter`
**Lines**: 8519-8525

**Result Class**: `FINISHING_GETDRYERCONDITIONResult`
**Lines**: 8529-8560

**Method**: `FINISHING_GETDRYERCONDITION(FINISHING_GETDRYERCONDITIONParameter para)`
**Lines**: 27959-28030

---

**File**: 106/296 | **Progress**: 35.8%
