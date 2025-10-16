# FINISHING_COATINGDATABYLOT

**Procedure Number**: 088 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve existing coating process data for a lot being resumed/edited |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingCoating (coating process records) |
| **Called From** | CoatingDataService.cs:401 → FINISHING_COATINGDATABYLOT() |
| **Frequency** | Medium |
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
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot number |
| `ITM_CODE` | VARCHAR2(50) | Finished goods item code |
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number (greige fabric) |
| `FINISHINGCUSTOMER` | VARCHAR2(50) | Customer code |
| `STARTDATE` | DATE | Process start timestamp |
| `ENDDATE` | DATE | Process end timestamp |
| `PRODUCTTYPEID` | VARCHAR2(50) | Product type identifier |
| `LENGTH1` | NUMBER | Length measurement checkpoint 1 (m) |
| `LENGTH2` | NUMBER | Length measurement checkpoint 2 (m) |
| `LENGTH3` | NUMBER | Length measurement checkpoint 3 (m) |
| `LENGTH4` | NUMBER | Length measurement checkpoint 4 (m) |
| `LENGTH5` | NUMBER | Length measurement checkpoint 5 (m) |
| `LENGTH6` | NUMBER | Length measurement checkpoint 6 (m) |
| `LENGTH7` | NUMBER | Length measurement checkpoint 7 (m) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `STATUSFLAG` | VARCHAR2(10) | Status flag (START/FINISH/CANCEL) |
| `SATURATOR_CHEM_PV` | NUMBER | Chemical saturator process value (actual) |
| `SATURATOR_CHEM_SP` | NUMBER | Chemical saturator setpoint (target) |
| `WASHING1_PV` | NUMBER | Washing zone 1 process value (actual) |
| `WASHING1_SP` | NUMBER | Washing zone 1 setpoint (target) |
| `WASHING2_PV` | NUMBER | Washing zone 2 process value (actual) |
| `WASHING2_SP` | NUMBER | Washing zone 2 setpoint (target) |
| `HOTFLUE_PV` | NUMBER | Hot flue temperature process value (°C actual) |
| `HOTFLUE_SP` | NUMBER | Hot flue temperature setpoint (°C target) |
| `BE_COATWIDTH` | NUMBER | Before coating width (mm) |
| `TEMP1_PV` | NUMBER | Temperature zone 1 process value (°C actual) |
| `TEMP1_SP` | NUMBER | Temperature zone 1 setpoint (°C target) |
| `TEMP2_PV` | NUMBER | Temperature zone 2 process value (°C actual) |
| `TEMP2_SP` | NUMBER | Temperature zone 2 setpoint (°C target) |
| `TEMP3_PV` | NUMBER | Temperature zone 3 process value (°C actual) |
| `TEMP3_SP` | NUMBER | Temperature zone 3 setpoint (°C target) |
| `TEMP4_PV` | NUMBER | Temperature zone 4 process value (°C actual) |
| `TEMP4_SP` | NUMBER | Temperature zone 4 setpoint (°C target) |
| `TEMP5_PV` | NUMBER | Temperature zone 5 process value (°C actual) |
| `TEMP5_SP` | NUMBER | Temperature zone 5 setpoint (°C target) |
| `TEMP6_PV` | NUMBER | Temperature zone 6 process value (°C actual) |
| `TEMP6_SP` | NUMBER | Temperature zone 6 setpoint (°C target) |
| `TEMP7_PV` | NUMBER | Temperature zone 7 process value (°C actual) |
| `TEMP7_SP` | NUMBER | Temperature zone 7 setpoint (°C target) |
| `TEMP8_PV` | NUMBER | Temperature zone 8 process value (°C actual) |
| `TEMP8_SP` | NUMBER | Temperature zone 8 setpoint (°C target) |
| `TEMP9_PV` | NUMBER | Temperature zone 9 process value (°C actual) |
| `TEMP9_SP` | NUMBER | Temperature zone 9 setpoint (°C target) |
| `TEMP10_PV` | NUMBER | Temperature zone 10 process value (°C actual) |
| `TEMP10_SP` | NUMBER | Temperature zone 10 setpoint (°C target) |
| `FANRPM` | NUMBER | Fan RPM setting |
| `EXFAN_FRONT_BACK` | NUMBER | Exhaust fan front/back setting |
| `EXFAN_MIDDLE` | NUMBER | Exhaust fan middle setting |
| `ANGLEKNIFE` | NUMBER | Knife angle setting (degrees) |
| `BLADENO` | VARCHAR2(50) | Blade number |
| `BLADEDIRECTION` | VARCHAR2(50) | Blade direction |
| `CYLINDER_TENSIONUP` | NUMBER | Cylinder tension up value |
| `OPOLE_TENSIONDOWN` | NUMBER | O-pole tension down value |
| `FRAMEWIDTH_FORN` | NUMBER | Frame width for furnace (mm) |
| `FRAMEWIDTH_TENTER` | NUMBER | Frame width tenter (mm) |
| `PATHLINE` | NUMBER | Path line setting |
| `FEEDIN` | NUMBER | Feed-in percentage |
| `OVERFEED` | NUMBER | Overfeed percentage |
| `SPEED_PV` | NUMBER | Machine speed process value (m/min actual) |
| `SPEED_SP` | NUMBER | Machine speed setpoint (m/min target) |
| `WIDTHCOAT` | NUMBER | Coating width (mm) |
| `WIDTHCOATALL` | NUMBER | Total coating width (mm) |
| `SILICONE_A` | VARCHAR2(50) | Silicone A type/batch |
| `SILICONE_B` | VARCHAR2(50) | Silicone B type/batch |
| `COATINGWEIGTH_L` | NUMBER | Coating weight left side (g/m²) |
| `COATINGWEIGTH_C` | NUMBER | Coating weight center (g/m²) |
| `COATINGWEIGTH_R` | NUMBER | Coating weight right side (g/m²) |
| `CONDITIONBY` | VARCHAR2(50) | Condition set by operator |
| `CONDITIONDATE` | DATE | Condition set date |
| `FINISHBY` | VARCHAR2(50) | Finished by operator |
| `SAMPLINGID` | VARCHAR2(50) | Sampling ID reference |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `REMARK` | VARCHAR2(500) | Process remarks/notes |
| `HUMIDITY_BF` | NUMBER | Humidity before (%) |
| `HUMIDITY_AF` | NUMBER | Humidity after (%) |
| `REPROCESS` | VARCHAR2(10) | Reprocess flag (Y/N) |
| `WEAVLENGTH` | NUMBER | Original weaving length (m) |
| `OPERATOR_GROUP` | VARCHAR2(50) | Operator group/shift |

---

## Business Logic (What it does and why)

**Purpose**: Loads previously saved coating process data when an operator returns to continue or edit a coating operation that was started earlier.

**When Used**:
- When operator scans a weaving lot that already has coating data
- On the Coating Finishing page (Coating1/Coating2) after entering machine and lot
- Used to populate UI with existing process parameters for continuation or editing
- Supports pause-and-resume workflow for long coating operations

**Business Rules**:
1. **Process Continuity**: Operators can pause coating operations and resume later with same settings
2. **Data Recovery**: All PLC parameters, machine settings, and process conditions are saved and recoverable
3. **Edit Mode**: Allows editing of in-progress or completed coating lots
4. **Machine-Lot Matching**: Retrieves data only for specific machine-lot combination

**Workflow**:
1. Operator enters coating machine number (P_MCNO)
2. Operator scans weaving lot barcode (P_WEAVINGLOT)
3. System calls procedure to check if coating data exists for this combination
4. If found → Loads all process parameters into UI form fields
   - PLC setpoints (temperatures, speeds, pressures)
   - Machine settings (blade, tension, width)
   - Chemical information (silicone types)
   - Quality data (coating weights, humidity)
   - Status and operator tracking
5. If not found → Blank form for new coating operation
6. Operator can continue from where left off or edit existing data

**Example Scenario**:
- Operator on Coating Machine C1 is coating Weaving Lot "WV-2024-001"
- After 500m, shift ends → Process paused (status = START)
- Next shift operator enters C1 and scans WV-2024-001
- Procedure returns saved data: 500m completed, all PLC settings preserved
- Operator continues from 500m with same settings

**Why This Matters**:
- Long coating runs can span multiple shifts (8-12 hours)
- PLC settings must remain consistent throughout the lot
- Quality tracking requires continuous parameter monitoring
- Allows correction of data entry errors after process completion

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item compatibility first)
- FINISHING_INSERTCOATING (creates initial coating record)

**Downstream**:
- FINISHING_UPDATECOATING (updates record with new values)
- FINISHING_COATINGPLCDATA (updates real-time PLC data)

**Similar**:
- FINISHING_SCOURINGDATABYLOT (same pattern for scouring process)
- FINISHING_DRYERDATABYLOT (same pattern for dryer process)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_COATINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 401-462

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_COATINGDATABYLOT(FINISHING_COATINGDATABYLOTParameter para)`
**Lines**: 28289-28417

---

**File**: 88/296 | **Progress**: 29.7%
