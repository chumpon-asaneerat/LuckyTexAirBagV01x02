# CONDITION_FINISHINGCOATING

**Procedure Number**: 215 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Save coating process conditions for item-machine combination |
| **Operation** | INSERT/UPDATE |
| **Tables** | Coating conditions master table |
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
| `P_COATNO` | VARCHAR2(20) | ✅ | Coating machine number |
| `P_CHEM` | NUMBER | ⬜ | Chemical bath temperature (°C) |
| `P_CHEM_MARGIN` | NUMBER | ⬜ | Chemical temperature tolerance |
| `P_WASH1` | NUMBER | ⬜ | Wash 1 temperature (°C) |
| `P_WASH1_MARGIN` | NUMBER | ⬜ | Wash 1 tolerance |
| `P_WASH2` | NUMBER | ⬜ | Wash 2 temperature (°C) |
| `P_WASH2_MARGIN` | NUMBER | ⬜ | Wash 2 tolerance |
| `P_HOTFLUE` | NUMBER | ⬜ | Hot flue temperature (°C) |
| `P_HOTFLUE_MARGIN` | NUMBER | ⬜ | Hot flue tolerance |
| `P_BE_COAT_MAX` | NUMBER | ⬜ | Maximum width before coating (cm) |
| `P_BE_COAT_MIN` | NUMBER | ⬜ | Minimum width before coating (cm) |
| `P_ROOMTEMP` | NUMBER | ⬜ | Room temperature (°C) |
| `P_ROOMTEMP_MARGIN` | NUMBER | ⬜ | Room temperature tolerance |
| `P_FANRPM` | NUMBER | ⬜ | Fan RPM setting |
| `P_FANRPM_MARGIN` | NUMBER | ⬜ | Fan RPM tolerance |
| `P_EXFAN_FRONT_BACK` | NUMBER | ⬜ | Exhaust fan front/back setting |
| `P_EXFAN_MARGIN` | NUMBER | ⬜ | Exhaust fan tolerance |
| `P_EXFAN_MIDDLE` | NUMBER | ⬜ | Exhaust fan middle setting |
| `P_ANGLEKNIFE` | NUMBER | ⬜ | Knife angle (degrees) |
| `P_BLADENO` | VARCHAR2(20) | ⬜ | Blade number |
| `P_BLADEDIRECTION` | VARCHAR2(20) | ⬜ | Blade direction |
| `P_PATHLINE` | VARCHAR2(50) | ⬜ | Path line configuration |
| `P_FEEDIN_MAX` | NUMBER | ⬜ | Maximum feed-in speed |
| `P_FEEDIN_MIN` | NUMBER | ⬜ | Minimum feed-in speed |
| `P_TENSION_UP` | NUMBER | ⬜ | Upper tension setting |
| `P_TENSION_UP_MARGIN` | NUMBER | ⬜ | Upper tension tolerance |
| `P_TENSION_DOWN` | NUMBER | ⬜ | Lower tension setting |
| `P_TENSION_DOWN_MARGIN` | NUMBER | ⬜ | Lower tension tolerance |
| `P_FRAME_FORN` | NUMBER | ⬜ | Frame forn setting |
| `P_FRAME_TENTER` | NUMBER | ⬜ | Frame tenter setting |
| `P_OVERFEED` | VARCHAR2(20) | ⬜ | Overfeed percentage |
| `P_SPEED` | NUMBER | ⬜ | Machine speed (m/min) |
| `P_SPEED_MARGIN` | NUMBER | ⬜ | Speed tolerance |
| `P_WIDTHCOAT` | NUMBER | ⬜ | Coating width (cm) |
| `P_WIDTHCOATALL_MAX` | NUMBER | ⬜ | Maximum total coated width |
| `P_WIDTHCOATALL_MIN` | NUMBER | ⬜ | Minimum total coated width |
| `P_COATINGWEIGTH_MAX` | NUMBER | ⬜ | Maximum coating weight (g/m²) |
| `P_COATINGWEIGTH_MIN` | NUMBER | ⬜ | Minimum coating weight (g/m²) |
| `P_RATIONSILICONE` | VARCHAR2(50) | ⬜ | Silicone ratio |
| `P_HUMIDITYMAX` | NUMBER | ⬜ | Maximum humidity (%) |
| `P_HUMIDITYMIN` | NUMBER | ⬜ | Minimum humidity (%) |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves comprehensive coating process conditions. Coating applies silicone to fabric surface for airbag deployment performance. This is the most complex finishing process with many critical parameters: temperatures (chemical bath, wash stages, hot flue), speeds, tensions, widths, coating weight, and environmental controls.

The procedure stores proven settings to ensure consistent coating quality and prevent defects.

---

## Related Procedures

**Similar**:
- [216-CONDITION_FINISHINGSCOURING.md](./216-CONDITION_FINISHINGSCOURING.md) - Scouring conditions
- [217-CONDITION_FINISHINGDRYER.md](./217-CONDITION_FINISHINGDRYER.md) - Dryer conditions

**Used By**: M06 Finishing module (Coating) for machine setup

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ProcessConditionDataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CONDITION_FINISHINGCOATING(CONDITION_FINISHINGCOATINGParameter para)`
**Lines**: 30665-30774

---

**File**: 215/296 | **Progress**: 72.6%
