# CONDITION_FINISHINGSCOURING

**Procedure Number**: 216 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Save scouring process conditions for item-machine combination |
| **Operation** | INSERT/UPDATE |
| **Tables** | Scouring conditions master table |
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
| `P_SCOURINGNO` | VARCHAR2(20) | ✅ | Scouring machine number |
| `P_CHEM` | NUMBER | ⬜ | Chemical bath temperature (°C) |
| `P_CHEM_MARGIN` | NUMBER | ⬜ | Chemical temperature tolerance |
| `P_WASH1` | NUMBER | ⬜ | Wash 1 temperature (°C) |
| `P_WASH1_MARGIN` | NUMBER | ⬜ | Wash 1 tolerance |
| `P_WASH2` | NUMBER | ⬜ | Wash 2 temperature (°C) |
| `P_WASH2_MARGIN` | NUMBER | ⬜ | Wash 2 tolerance |
| `P_HOTFLUE` | NUMBER | ⬜ | Hot flue temperature (°C) |
| `P_HOTFLUE_MARGIN` | NUMBER | ⬜ | Hot flue tolerance |
| `P_ROOMTEMP` | NUMBER | ⬜ | Room temperature (°C) |
| `P_ROOMTEMP_MARGIN` | NUMBER | ⬜ | Room temperature tolerance |
| `P_SPEED` | NUMBER | ⬜ | Machine speed (m/min) |
| `P_SPEED_MARGIN` | NUMBER | ⬜ | Speed tolerance |
| `P_MAINFRAME` | NUMBER | ⬜ | Main frame setting |
| `P_MAINFRAME_MARGIN` | NUMBER | ⬜ | Main frame tolerance |
| `P_WIDTHBE` | NUMBER | ⬜ | Width before scouring (cm) |
| `P_WIDTHBE_MARGIN` | NUMBER | ⬜ | Width before tolerance |
| `P_WIDTHAF` | NUMBER | ⬜ | Width after scouring (cm) |
| `P_WIDTHAF_MARGIN` | NUMBER | ⬜ | Width after tolerance |
| `P_PIN2PIN` | NUMBER | ⬜ | Pin-to-pin distance (cm) |
| `P_PIN2PIN_MARGIN` | NUMBER | ⬜ | Pin-to-pin tolerance |
| `P_HUMIDITYMAX` | NUMBER | ⬜ | Maximum humidity (%) |
| `P_HUMIDITYMIN` | NUMBER | ⬜ | Minimum humidity (%) |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves scouring process conditions. Scouring is a cleaning/washing process that removes sizing, oils, and contaminants from greige fabric before coating. The process uses chemical baths, washing stages, and hot air drying. Critical parameters include temperatures, speed, width control, and environmental conditions.

The procedure:
1. Takes item code, machine number, and process parameters
2. Saves or updates condition record
3. Stores target values and tolerances for all critical parameters
4. Ensures fabric is properly cleaned without damage
5. Controls width changes (fabric may shrink/relax during scouring)

Proper scouring is essential for coating adhesion and fabric quality.

---

## Related Procedures

**Similar**:
- [215-CONDITION_FINISHINGCOATING.md](./215-CONDITION_FINISHINGCOATING.md) - Coating conditions
- [217-CONDITION_FINISHINGDRYER.md](./217-CONDITION_FINISHINGDRYER.md) - Dryer conditions

**Used By**: M06 Finishing module (Scouring) for machine setup

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ProcessConditionDataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CONDITION_FINISHINGSCOURING(CONDITION_FINISHINGSCOURINGParameter para)`
**Lines**: 30513-30586

---

**File**: 216/296 | **Progress**: 73.0%
