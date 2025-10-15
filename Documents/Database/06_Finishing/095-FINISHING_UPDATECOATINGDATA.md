# FINISHING_UPDATECOATINGDATA

**Procedure Number**: 095 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update coating process with current PLC values and min/max ranges |
| **Operation** | UPDATE |
| **Tables** | tblFinishingCoating, tblFinishingCoatingCondition |
| **Called From** | CoatingDataService.cs:1030 → FINISHING_UPDATECOATINGDATA() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag |
| `P_SAT` | NUMBER | ⬜ | Saturator actual value |
| `P_SAT_MIN` | NUMBER | ⬜ | Saturator minimum limit |
| `P_SAT_MAX` | NUMBER | ⬜ | Saturator maximum limit |
| `P_WASHING1` | NUMBER | ⬜ | Washing 1 actual value |
| `P_WASHING1_MIN/MAX` | NUMBER | ⬜ | Washing 1 limits |
| `P_WASHING2` | NUMBER | ⬜ | Washing 2 actual value |
| `P_WASHING2_MIN/MAX` | NUMBER | ⬜ | Washing 2 limits |
| `P_HOTFLUE` | NUMBER | ⬜ | Hot flue actual value |
| `P_HOTFLUE_MIN/MAX` | NUMBER | ⬜ | Hot flue limits |
| `P_TEMP1-10` | NUMBER | ⬜ | Temperature zones 1-10 actual values |
| `P_TEMP1-10_MIN/MAX` | NUMBER | ⬜ | Temperature zones 1-10 limits |
| `P_SPEED` | NUMBER | ⬜ | Machine speed actual value |
| `P_SPEED_MIN/MAX` | NUMBER | ⬜ | Machine speed limits |
| `P_TENSIONUP` | NUMBER | ⬜ | Tension up actual value |
| `P_TENSIONUP_MIN/MAX` | NUMBER | ⬜ | Tension up limits |
| `P_TENSIONDOWN` | NUMBER | ⬜ | Tension down actual value |
| `P_TENSIONDOWN_MIN/MAX` | NUMBER | ⬜ | Tension down limits |
| `P_LENGTH1-7` | NUMBER | ⬜ | Length measurements (m) |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code |
| `P_WEAVINGLOT` | VARCHAR2(50) | ⬜ | Weaving lot |
| `P_CUSTOMER` | VARCHAR2(50) | ⬜ | Customer |
| `P_STARTDATE` | DATE | ⬜ | Start date |
| `P_BECOATWIDTH` | NUMBER | ⬜ | Before coating width |
| `P_FANRPM` | NUMBER | ⬜ | Fan RPM |
| `P_EXFAN_FRONT_BACK` | NUMBER | ⬜ | Exhaust fan front/back |
| `P_EXFAN_MIDDLE` | NUMBER | ⬜ | Exhaust fan middle |
| `P_ANGLEKNIFE` | NUMBER | ⬜ | Knife angle |
| `P_BLADENO` | VARCHAR2(50) | ⬜ | Blade number |
| `P_BLADEDIRECTION` | VARCHAR2(50) | ⬜ | Blade direction |
| `P_FORN` | NUMBER | ⬜ | Frame width furnace |
| `P_TENTER` | NUMBER | ⬜ | Frame width tenter |
| `P_PATHLINE` | NUMBER | ⬜ | Path line |
| `P_FEEDIN` | NUMBER | ⬜ | Feed-in |
| `P_OVERFEED` | NUMBER | ⬜ | Overfeed |
| `P_WIDTHCOAT` | NUMBER | ⬜ | Coating width |
| `P_WIDTHCOATALL` | NUMBER | ⬜ | Total coating width |
| `P_SILICONEA/B` | VARCHAR2(50) | ⬜ | Silicone types |
| `P_CWL/C/R` | NUMBER | ⬜ | Coating weights |
| `P_CONDITIONBY` | VARCHAR2(50) | ⬜ | Condition set by |
| `P_FINISHBY` | VARCHAR2(50) | ⬜ | Finished by |
| `P_ENDDATE` | DATE | ⬜ | End date |
| `P_CONDITONDATE` | DATE | ⬜ | Condition date |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Remarks |
| `P_HUMID_BF/AF` | NUMBER | ⬜ | Humidity before/after |
| `P_GROUP` | VARCHAR2(50) | ⬜ | Operator group |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(100) | Success/failure message |

### Returns (if cursor)

N/A - Returns single OUT parameter

---

## Business Logic (What it does and why)

**Purpose**: Updates coating record with complete PLC data including actual values, minimum limits, and maximum limits for all process parameters. Used for quality tracking and real-time monitoring.

**When Used**:
- PLC data collection saves current readings and acceptable ranges
- Quality monitoring systems track if values stay within limits
- Process completion saves final min/max values observed
- Statistical process control analysis

**Business Rules**:
1. **Actual + Limits**: Saves current value plus min/max observed during process
2. **Quality Validation**: MIN/MAX values show process variation range
3. **Out-of-Spec Detection**: Compares actual vs specification limits
4. **Complete Record**: All 10 temperature zones + other parameters tracked
5. **Historical Analysis**: Enables process capability studies

**Workflow**:
1. PLC continuously monitors all parameters
2. System tracks minimum and maximum values observed
3. Periodically calls UPDATECOATINGDATA with:
   - Current actual values
   - Lowest value seen (MIN)
   - Highest value seen (MAX)
4. Quality system checks if MIN/MAX stayed within specifications
5. Final record shows process variation throughout coating

**Why This Matters**:
- Quality assurance requires proof parameters stayed in range
- Process capability analysis needs min/max data
- Automotive certification requires statistical process control
- Defect investigation needs to know if process drifted out of spec
- Continuous improvement uses variation data to optimize process

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTCOATING (creates initial record)
- FINISHING_COATINGPLCDATA (provides specification limits)

**Downstream**:
- Used by quality analysis systems
- Process monitoring dashboards
- Statistical process control reports

**Similar**:
- FINISHING_UPDATESCOURINGDATA (same pattern for scouring)
- FINISHING_UPDATEDRYERDATA (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_UPDATECOATINGDATA(string finishlot, string flag, ...60+ parameters)`
**Lines**: 1030-1190

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_UPDATECOATINGDATA(FINISHING_UPDATECOATINGDATAParameter para)`
**Lines**: 27XXX (estimated)

---

**File**: 95/296 | **Progress**: 32.1%
