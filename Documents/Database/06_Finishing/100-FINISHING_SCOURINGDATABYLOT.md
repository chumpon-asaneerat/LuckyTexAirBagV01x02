# FINISHING_SCOURINGDATABYLOT

**Procedure Number**: 100 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve existing scouring process data for a lot being resumed/edited |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingScouring, tblFinishingScouringCondition |
| **Called From** | CoatingDataService.cs:1617 → GetFINISHING_SCOURINGDATABYLOT() |
| **Frequency** | Medium |
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
| `SATURATOR_CHEM_PV` | NUMBER | Chemical saturator process value (°C) |
| `SATURATOR_CHEM_SP` | NUMBER | Chemical saturator setpoint (°C) |
| `WASHING1_PV` | NUMBER | Washing zone 1 process value (°C) |
| `WASHING1_SP` | NUMBER | Washing zone 1 setpoint (°C) |
| `WASHING2_PV` | NUMBER | Washing zone 2 process value (°C) |
| `WASHING2_SP` | NUMBER | Washing zone 2 setpoint (°C) |
| `HOTFLUE_PV` | NUMBER | Hot flue temperature process value (°C) |
| `HOTFLUE_SP` | NUMBER | Hot flue temperature setpoint (°C) |
| `TEMP1_PV` | NUMBER | Temperature zone 1 process value (°C) |
| `TEMP1_SP` | NUMBER | Temperature zone 1 setpoint (°C) |
| `TEMP2_PV` | NUMBER | Temperature zone 2 process value (°C) |
| `TEMP2_SP` | NUMBER | Temperature zone 2 setpoint (°C) |
| `TEMP3_PV` | NUMBER | Temperature zone 3 process value (°C) |
| `TEMP3_SP` | NUMBER | Temperature zone 3 setpoint (°C) |
| `TEMP4_PV` | NUMBER | Temperature zone 4 process value (°C) |
| `TEMP4_SP` | NUMBER | Temperature zone 4 setpoint (°C) |
| `TEMP5_PV` | NUMBER | Temperature zone 5 process value (°C) |
| `TEMP5_SP` | NUMBER | Temperature zone 5 setpoint (°C) |
| `TEMP6_PV` | NUMBER | Temperature zone 6 process value (°C) |
| `TEMP6_SP` | NUMBER | Temperature zone 6 setpoint (°C) |
| `TEMP7_PV` | NUMBER | Temperature zone 7 process value (°C) |
| `TEMP7_SP` | NUMBER | Temperature zone 7 setpoint (°C) |
| `TEMP8_PV` | NUMBER | Temperature zone 8 process value (°C) |
| `TEMP8_SP` | NUMBER | Temperature zone 8 setpoint (°C) |
| `TEMP9_PV` | NUMBER | Temperature zone 9 process value (°C) |
| `TEMP9_SP` | NUMBER | Temperature zone 9 setpoint (°C) |
| `TEMP10_PV` | NUMBER | Temperature zone 10 process value (°C) |
| `TEMP10_SP` | NUMBER | Temperature zone 10 setpoint (°C) |
| `TEMP1_MIN` | NUMBER | Temperature zone 1 minimum specification limit (°C) |
| `TEMP1_MAX` | NUMBER | Temperature zone 1 maximum specification limit (°C) |
| `TEMP2_MIN` | NUMBER | Temperature zone 2 minimum specification limit (°C) |
| `TEMP2_MAX` | NUMBER | Temperature zone 2 maximum specification limit (°C) |
| `TEMP3_MIN` | NUMBER | Temperature zone 3 minimum specification limit (°C) |
| `TEMP3_MAX` | NUMBER | Temperature zone 3 maximum specification limit (°C) |
| `TEMP4_MIN` | NUMBER | Temperature zone 4 minimum specification limit (°C) |
| `TEMP4_MAX` | NUMBER | Temperature zone 4 maximum specification limit (°C) |
| `TEMP5_MIN` | NUMBER | Temperature zone 5 minimum specification limit (°C) |
| `TEMP5_MAX` | NUMBER | Temperature zone 5 maximum specification limit (°C) |
| `TEMP6_MIN` | NUMBER | Temperature zone 6 minimum specification limit (°C) |
| `TEMP6_MAX` | NUMBER | Temperature zone 6 maximum specification limit (°C) |
| `TEMP7_MIN` | NUMBER | Temperature zone 7 minimum specification limit (°C) |
| `TEMP7_MAX` | NUMBER | Temperature zone 7 maximum specification limit (°C) |
| `TEMP8_MIN` | NUMBER | Temperature zone 8 minimum specification limit (°C) |
| `TEMP8_MAX` | NUMBER | Temperature zone 8 maximum specification limit (°C) |
| `SPEED_PV` | NUMBER | Machine speed process value (m/min) |
| `SPEED_SP` | NUMBER | Machine speed setpoint (m/min) |
| `SPEED_MIN` | NUMBER | Speed minimum specification limit (m/min) |
| `SPEED_MAX` | NUMBER | Speed maximum specification limit (m/min) |
| `MAINFRAMEWIDTH` | NUMBER | Main frame width (mm) |
| `WIDTH_BE` | NUMBER | Width before scouring (mm) |
| `WIDTH_AF` | NUMBER | Width after scouring (mm) |
| `PIN2PIN` | NUMBER | Pin to pin distance (mm) |
| `SAT_CHEM_MIN` | NUMBER | Saturator temperature minimum specification limit (°C) |
| `SAT_CHEM_MAX` | NUMBER | Saturator temperature maximum specification limit (°C) |
| `WASHING1_MIN` | NUMBER | Washing 1 temperature minimum specification limit (°C) |
| `WASHING1_MAX` | NUMBER | Washing 1 temperature maximum specification limit (°C) |
| `WASHING2_MIN` | NUMBER | Washing 2 temperature minimum specification limit (°C) |
| `WASHING2_MAX` | NUMBER | Washing 2 temperature maximum specification limit (°C) |
| `HOTFLUE_MIN` | NUMBER | Hot flue temperature minimum specification limit (°C) |
| `HOTFLUE_MAX` | NUMBER | Hot flue temperature maximum specification limit (°C) |
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
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |

---

## Business Logic (What it does and why)

**Purpose**: Loads previously saved scouring process data when an operator returns to continue or edit a scouring operation that was started earlier. Includes both process values and specification ranges for quality monitoring.

**When Used**:
- When operator scans a weaving lot that already has scouring data
- On the Scouring Finishing page after entering machine and lot
- Used to populate UI with existing process parameters for continuation or editing
- Supports pause-and-resume workflow for long scouring operations

**Business Rules**:
1. **Process Continuity**: Operators can pause scouring operations and resume later with same settings
2. **Data Recovery**: All PLC parameters, machine settings, and process conditions are saved and recoverable
3. **Edit Mode**: Allows editing of in-progress or completed scouring lots
4. **Machine-Lot Matching**: Retrieves data only for specific machine-lot combination
5. **Quality Monitoring**: Returns MIN/MAX specification ranges for real-time compliance checking

**Workflow**:
1. Operator enters scouring machine number (P_MCNO)
2. Operator scans weaving lot barcode (P_WEAVINGLOT)
3. System calls procedure to check if scouring data exists for this combination
4. If found → Loads all process parameters and specifications into UI form fields:
   - PLC setpoints and actual values (temperatures, speeds)
   - Machine settings (width, frame, pin-to-pin)
   - Quality parameters (humidity, lengths)
   - MIN/MAX specification limits for compliance display
   - Status and operator tracking
5. If not found → Blank form for new scouring operation
6. Operator can continue from where left off or edit existing data
7. UI displays actual values vs. spec ranges with color-coded status

**Example Scenario**:
- Operator on Scouring Machine SC1 is scouring Weaving Lot "WV-2024-001"
- After 400m, shift ends → Process paused (status = START)
- Next shift operator enters SC1 and scans WV-2024-001
- Procedure returns saved data:
  - FINISHINGLOT = "FS-2024-0125"
  - LENGTH1 = 400m (already processed)
  - All 10 temperature zone PV/SP values preserved
  - TEMP1_MIN/MAX ranges for compliance checking
  - STATUSFLAG = "START" (indicates in-progress)
- Operator continues from 400m with same settings
- UI shows green indicators for temps within spec, red if out of range

**Why This Matters**:
- Long scouring runs can span multiple shifts (6-10 hours)
- PLC settings must remain consistent throughout the lot
- Quality tracking requires continuous parameter monitoring with spec compliance
- MIN/MAX ranges enable real-time deviation alerts
- Allows correction of data entry errors after process completion
- Supports quality documentation with specification tracking

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item compatibility first)
- FINISHING_INSERTSCOURING (creates initial scouring record)

**Downstream**:
- FINISHING_UPDATESCOURING (updates record with new values)
- FINISHING_SCOURINGPLCDATA (updates real-time PLC data)

**Similar**:
- FINISHING_COATINGDATABYLOT (same pattern for coating process)
- FINISHING_DRYERDATABYLOT (same pattern for dryer process)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `GetFINISHING_SCOURINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 1617-1758

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_SCOURINGDATABYLOT(FINISHING_SCOURINGDATABYLOTParameter para)`
**Lines**: 7568-7668 (Parameter: 7568-7572, Result: 7578-7668)

---

**File**: 100/296 | **Progress**: 33.8%
