# FINISHING_UPDATECOATING

**Procedure Number**: 094 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update existing coating process record with current values and status |
| **Operation** | UPDATE |
| **Tables** | tblFinishingLot, tblFinishingCoating |
| **Called From** | CoatingDataService.cs:782 → FINISHING_UPDATECOATING() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number (primary key) |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag (START/FINISH/PAUSE) |
| `P_SATURATOR_PV` | NUMBER | ⬜ | Saturator process value |
| `P_SATURATOR_SP` | NUMBER | ⬜ | Saturator setpoint |
| `P_WASHING1_PV` | NUMBER | ⬜ | Washing zone 1 process value |
| `P_WASHING1_SP` | NUMBER | ⬜ | Washing zone 1 setpoint |
| `P_WASHING2_PV` | NUMBER | ⬜ | Washing zone 2 process value |
| `P_WASHING2_SP` | NUMBER | ⬜ | Washing zone 2 setpoint |
| `P_HOTFLUE_PV` | NUMBER | ⬜ | Hot flue process value |
| `P_HOTFLUE_SP` | NUMBER | ⬜ | Hot flue setpoint |
| `P_TEMP1_PV` | NUMBER | ⬜ | Temperature zone 1 process value (°C) |
| `P_TEMP1_SP` | NUMBER | ⬜ | Temperature zone 1 setpoint (°C) |
| `P_TEMP2_PV` | NUMBER | ⬜ | Temperature zone 2 process value (°C) |
| `P_TEMP2_SP` | NUMBER | ⬜ | Temperature zone 2 setpoint (°C) |
| `P_TEMP3_PV` | NUMBER | ⬜ | Temperature zone 3 process value (°C) |
| `P_TEMP3_SP` | NUMBER | ⬜ | Temperature zone 3 setpoint (°C) |
| `P_TEMP4_PV` | NUMBER | ⬜ | Temperature zone 4 process value (°C) |
| `P_TEMP4_SP` | NUMBER | ⬜ | Temperature zone 4 setpoint (°C) |
| `P_TEMP5_PV` | NUMBER | ⬜ | Temperature zone 5 process value (°C) |
| `P_TEMP5_SP` | NUMBER | ⬜ | Temperature zone 5 setpoint (°C) |
| `P_TEMP6_PV` | NUMBER | ⬜ | Temperature zone 6 process value (°C) |
| `P_TEMP6_SP` | NUMBER | ⬜ | Temperature zone 6 setpoint (°C) |
| `P_TEMP7_PV` | NUMBER | ⬜ | Temperature zone 7 process value (°C) |
| `P_TEMP7_SP` | NUMBER | ⬜ | Temperature zone 7 setpoint (°C) |
| `P_TEMP8_PV` | NUMBER | ⬜ | Temperature zone 8 process value (°C) |
| `P_TEMP8_SP` | NUMBER | ⬜ | Temperature zone 8 setpoint (°C) |
| `P_TEMP9_PV` | NUMBER | ⬜ | Temperature zone 9 process value (°C) |
| `P_TEMP9_SP` | NUMBER | ⬜ | Temperature zone 9 setpoint (°C) |
| `P_TEMP10_PV` | NUMBER | ⬜ | Temperature zone 10 process value (°C) |
| `P_TEMP10_SP` | NUMBER | ⬜ | Temperature zone 10 setpoint (°C) |
| `P_SPEED_PV` | NUMBER | ⬜ | Machine speed process value (m/min) |
| `P_SPEED_SP` | NUMBER | ⬜ | Machine speed setpoint (m/min) |
| `P_LENGTH1` | NUMBER | ⬜ | Length measurement checkpoint 1 (m) |
| `P_LENGTH2` | NUMBER | ⬜ | Length measurement checkpoint 2 (m) |
| `P_LENGTH3` | NUMBER | ⬜ | Length measurement checkpoint 3 (m) |
| `P_LENGTH4` | NUMBER | ⬜ | Length measurement checkpoint 4 (m) |
| `P_LENGTH5` | NUMBER | ⬜ | Length measurement checkpoint 5 (m) |
| `P_LENGTH6` | NUMBER | ⬜ | Length measurement checkpoint 6 (m) |
| `P_LENGTH7` | NUMBER | ⬜ | Length measurement checkpoint 7 (m) |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code |
| `P_WEAVINGLOT` | VARCHAR2(50) | ⬜ | Weaving lot number |
| `P_CUSTOMER` | VARCHAR2(50) | ⬜ | Customer code |
| `P_STARTDATE` | DATE | ⬜ | Start date/time |
| `P_BECOATWIDTH` | NUMBER | ⬜ | Before coating width (mm) |
| `P_FANRPM` | NUMBER | ⬜ | Fan RPM |
| `P_EXFAN_FRONT_BACK` | NUMBER | ⬜ | Exhaust fan front/back |
| `P_EXFAN_MIDDLE` | NUMBER | ⬜ | Exhaust fan middle |
| `P_ANGLEKNIFE` | NUMBER | ⬜ | Knife angle |
| `P_BLADENO` | VARCHAR2(50) | ⬜ | Blade number |
| `P_BLADEDIRECTION` | VARCHAR2(50) | ⬜ | Blade direction |
| `P_TENSIONUP` | NUMBER | ⬜ | Tension up |
| `P_TENSIONDOWN` | NUMBER | ⬜ | Tension down |
| `P_FORN` | NUMBER | ⬜ | Frame width furnace |
| `P_TENTER` | NUMBER | ⬜ | Frame width tenter |
| `P_PATHLINE` | NUMBER | ⬜ | Path line |
| `P_FEEDIN` | NUMBER | ⬜ | Feed-in percentage |
| `P_OVERFEED` | NUMBER | ⬜ | Overfeed percentage |
| `P_WIDTHCOAT` | NUMBER | ⬜ | Coating width |
| `P_WIDTHCOATALL` | NUMBER | ⬜ | Total coating width |
| `P_SILICONEA` | VARCHAR2(50) | ⬜ | Silicone A type/batch |
| `P_SILICONEB` | VARCHAR2(50) | ⬜ | Silicone B type/batch |
| `P_CWL` | NUMBER | ⬜ | Coating weight left side (g/m²) |
| `P_CWC` | NUMBER | ⬜ | Coating weight center (g/m²) |
| `P_CWR` | NUMBER | ⬜ | Coating weight right side (g/m²) |
| `P_CONDITIONBY` | VARCHAR2(50) | ⬜ | Condition set by operator |
| `P_FINISHBY` | VARCHAR2(50) | ⬜ | Finished by operator |
| `P_ENDDATE` | DATE | ⬜ | End date/time |
| `P_CONDITONDATE` | DATE | ⬜ | Condition date |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Process remarks |
| `P_HUMID_BF` | NUMBER | ⬜ | Humidity before |
| `P_HUMID_AF` | NUMBER | ⬜ | Humidity after |
| `P_GROUP` | VARCHAR2(50) | ⬜ | Operator group/shift |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(100) | Success/failure message |

### Returns (if cursor)

N/A - Returns single OUT parameter

---

## Business Logic (What it does and why)

**Purpose**: Updates coating process record during or after processing. Used to save current PLC values, update lengths, change status, add remarks, and finalize completed lots.

**When Used**:
- During coating: Periodic updates of PLC values and lengths
- Process completion: Final lengths, end date, status = 'FINISH'
- Process pause: Save current state, status = 'PAUSE'
- Parameter adjustment: Update machine settings mid-process
- Quality notes: Add remarks about process conditions

**Business Rules**:
1. **Identified by FINISHLOT**: Updates specific lot by finishing lot number
2. **Partial Updates**: Only updates fields provided (nulls ignored)
3. **Status Changes**: P_FLAG controls process state (START/FINISH/PAUSE)
4. **Complete Flexibility**: Can update any combination of parameters
5. **Audit Trail**: Tracks who finished (FINISHBY) and when (ENDDATE)

**Workflow**:

**Scenario 1: Process Completion**
1. Operator clicks "Finish" button
2. System calls UPDATE with: P_FLAG='FINISH', P_ENDDATE=now, P_FINISHBY=operator
3. Updates final lengths (LENGTH1-7)
4. Saves final PLC values
5. Record status changes to completed

**Scenario 2: Mid-Process Update**
1. PLC continuously collects data
2. System periodically calls UPDATE with current PLC values
3. Updates temperature/speed readings
4. Saves current length
5. Keeps status = 'START'

**Scenario 3: Remark Addition**
1. Operator notes unusual condition
2. Adds remark in UI
3. System calls UPDATE with just FINISHLOT and REMARK
4. Other fields unchanged

**Why This Matters**:
- Preserves process data during long coating operations
- Enables process resume after interruption
- Records final state for quality documentation
- Tracks process progression through multiple shifts
- Supports real-time monitoring and historical analysis

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTCOATING (creates initial record to update)
- FINISHING_COATINGDATABYLOT (retrieves current values before update)

**Downstream**:
- FINISHING_GETCOATINGREPORT (retrieves updated data for reports)
- Used by process monitoring systems

**Similar**:
- FINISHING_UPDATESCOURING (same pattern for scouring)
- FINISHING_UPDATEDRYER (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_UPDATECOATING(string finishlot, string flag, ...40+ parameters)`
**Lines**: 782-900

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_UPDATECOATING(FINISHING_UPDATECOATINGParameter para)`
**Lines**: 27XXX (estimated)

---

**File**: 94/296 | **Progress**: 31.8%
