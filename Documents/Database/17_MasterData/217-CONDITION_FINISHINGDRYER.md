# CONDITION_FINISHINGDRYER

**Procedure Number**: 217 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Save dryer process conditions for item-machine combination |
| **Operation** | INSERT/UPDATE |
| **Tables** | Dryer conditions master table |
| **Called From** | ProcessConditionDataService.cs |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Item code |
| `P_MC` | VARCHAR2(20) | ✅ | Dryer machine number |
| `P_WIDTH_BE_HEAT_MAX` | NUMBER | ⬜ | Maximum width before heating (cm) |
| `P_WIDTH_BE_HEAT_MIN` | NUMBER | ⬜ | Minimum width before heating (cm) |
| `P_ACCPRESURE` | NUMBER | ⬜ | Accumulator pressure |
| `P_ASSTENSION` | NUMBER | ⬜ | Assist tension setting |
| `P_ACCARIDENSER` | NUMBER | ⬜ | Accumulator air denser |
| `P_CHIFROT` | NUMBER | ⬜ | Chi front setting |
| `P_CHIREAR` | NUMBER | ⬜ | Chi rear setting |
| `P_DRYERTEMP` | NUMBER | ⬜ | Dryer temperature (°C) |
| `P_DRYERTEMP_MARGIN` | NUMBER | ⬜ | Dryer temperature tolerance |
| `P_SPEED` | NUMBER | ⬜ | Machine speed (m/min) |
| `P_SPEED_MARGIN` | NUMBER | ⬜ | Speed tolerance |
| `P_STEAMPRESURE` | NUMBER | ⬜ | Steam pressure (bar) |
| `P_DRYERUPCIRCUFAN` | NUMBER | ⬜ | Dryer upper circulation fan (RPM) |
| `P_EXHAUSTFAN` | NUMBER | ⬜ | Exhaust fan setting |
| `P_WIDTH_AF_HEAT` | NUMBER | ⬜ | Width after heating (cm) |
| `P_WIDTH_AF_HEAT_MARGIN` | NUMBER | ⬜ | Width after heating tolerance |
| `P_HUMIDITYMAX` | NUMBER | ⬜ | Maximum humidity (%) |
| `P_HUMIDITYMIN` | NUMBER | ⬜ | Minimum humidity (%) |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves dryer process conditions. The dryer is the final finishing step that heat-sets the coated fabric, stabilizes dimensions, and ensures proper moisture content. Uses steam heating and forced air circulation. Critical parameters include temperature, speed, width control, steam pressure, and fan settings.

The procedure:
1. Takes item code, machine number, and process parameters
2. Saves or updates condition record
3. Stores target values and tolerances
4. Controls width stabilization (fabric dimensions after heating)
5. Manages heat distribution (steam pressure, fan speeds)

Proper drying is critical for dimensional stability and coating cure.

---

## Related Procedures

**Similar**:
- [215-CONDITION_FINISHINGCOATING.md](./215-CONDITION_FINISHINGCOATING.md) - Coating conditions
- [216-CONDITION_FINISHINGSCOURING.md](./216-CONDITION_FINISHINGSCOURING.md) - Scouring conditions

**Used By**: M06 Finishing module (Dryer) for machine setup

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ProcessConditionDataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CONDITION_FINISHINGDRYER(CONDITION_FINISHINGDRYERParameter para)`
**Lines**: 30593-30658

---

**File**: 217/296 | **Progress**: 73.3%
