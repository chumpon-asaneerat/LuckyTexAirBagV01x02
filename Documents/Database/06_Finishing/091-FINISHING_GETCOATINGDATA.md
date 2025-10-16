# FINISHING_GETCOATINGDATA

**Procedure Number**: 091 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve coating lots by machine and status for selection/resumption |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingCoating (coating process records) |
| **Called From** | CoatingDataService.cs:277 → FINISHING_GETCOATINGCONDITIONList() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number (coating machine ID) |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag filter (START/FINISH/ALL) |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

Returns comprehensive coating lot data including process parameters and PLC values (same structure as FINISHING_COATINGDATABYLOT):

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
| `STATUSFLAG` | VARCHAR2(10) | Current status (START/FINISH/CANCEL) |
| `SATURATOR_CHEM_PV` | NUMBER | Chemical saturator process value |
| `SATURATOR_CHEM_SP` | NUMBER | Chemical saturator setpoint |
| `WASHING1_PV` | NUMBER | Washing zone 1 process value |
| `WASHING1_SP` | NUMBER | Washing zone 1 setpoint |
| `WASHING2_PV` | NUMBER | Washing zone 2 process value |
| `WASHING2_SP` | NUMBER | Washing zone 2 setpoint |
| `HOTFLUE_PV` | NUMBER | Hot flue temperature process value |
| `HOTFLUE_SP` | NUMBER | Hot flue temperature setpoint |
| `BE_COATWIDTH` | NUMBER | Before coating width (mm) |
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
| `FANRPM` | NUMBER | Fan RPM setting |
| `EXFAN_FRONT_BACK` | NUMBER | Exhaust fan front/back setting |
| `EXFAN_MIDDLE` | NUMBER | Exhaust fan middle setting |
| `ANGLEKNIFE` | NUMBER | Knife angle (degrees) |
| `BLADENO` | VARCHAR2(50) | Blade number |
| `BLADEDIRECTION` | VARCHAR2(50) | Blade direction |
| `CYLINDER_TENSIONUP` | NUMBER | Cylinder tension up value |
| `OPOLE_TENSIONDOWN` | NUMBER | O-pole tension down value |
| `FRAMEWIDTH_FORN` | NUMBER | Frame width furnace (mm) |
| `FRAMEWIDTH_TENTER` | NUMBER | Frame width tenter (mm) |
| `PATHLINE` | NUMBER | Path line setting |
| `FEEDIN` | NUMBER | Feed-in percentage |
| `OVERFEED` | NUMBER | Overfeed percentage |
| `SPEED_PV` | NUMBER | Machine speed process value (m/min) |
| `SPEED_SP` | NUMBER | Machine speed setpoint (m/min) |
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
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `WEAVINGLENGTH` | NUMBER | Original weaving length (m) |
| `OPERATOR_GROUP` | VARCHAR2(50) | Operator group/shift |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves a list of coating lots on a specific machine filtered by status. Used for lot selection, resumption of in-progress lots, and viewing completed lots.

**When Used**:
- Machine startup: Show all in-progress lots (STATUS = 'START') for this machine
- Lot selection: Operator picks which lot to continue working on
- Review completed work: Show finished lots (STATUS = 'FINISH')
- All lots view: Show all lots on machine (STATUS = 'ALL')

**Business Rules**:
1. **Machine-Specific**: Only shows lots running/completed on specified machine
2. **Status Filtering**: P_FLAG controls which lots are visible
   - 'START': In-progress lots (paused/active coating operations)
   - 'FINISH': Completed lots
   - 'ALL': All lots regardless of status
3. **Multi-Lot Support**: One machine can have multiple lots in different states
4. **Resume Capability**: Operator can select lot from list to resume processing

**Workflow**:

**Scenario 1: Resume In-Progress Lot**
1. Operator arrives at Coating Machine C1
2. System calls procedure: P_MCNO='C1', P_FLAG='START'
3. Procedure returns list of all incomplete lots on C1:
   - LOT-001: Started yesterday, 500m completed, paused
   - LOT-002: Started this morning, 200m completed, active
4. Operator selects LOT-001 from list
5. System loads all parameters for LOT-001
6. Operator continues from 500m

**Scenario 2: View Completed Lots**
1. Supervisor reviews today's completed work on C1
2. System calls: P_MCNO='C1', P_FLAG='FINISH'
3. Returns all finished lots with final parameters
4. Displays summary: Item codes, lengths, completion times

**Scenario 3: Machine Dashboard**
1. Production dashboard shows all active coating machines
2. For each machine, calls: P_FLAG='START'
3. Displays real-time: Which lots are running, current status

**FLAG Parameter Values**:
- **'START'**: Show incomplete lots (most common for operators)
- **'FINISH'**: Show completed lots (for review/reports)
- **'ALL'**: Show everything (for troubleshooting/audits)

**Why This Matters**:
- **Shift Continuity**: New shift sees exactly what previous shift left incomplete
- **Resource Planning**: Shows how many lots are queued on each machine
- **Quality Tracking**: Links process parameters to each lot for traceability
- **Error Recovery**: If system crashes, can resume from saved state

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTCOATING (creates new lots that appear in this list)
- FINISHING_UPDATECOATING (updates lot status)

**Downstream**:
- FINISHING_COATINGDATABYLOT (loads detailed data after lot selection)
- Used for populating lot selection dropdowns/lists

**Similar**:
- FINISHING_GETSCOURINGDATA (same pattern for scouring)
- FINISHING_GETDRYERDATA (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETCOATINGCONDITIONList(string mcno, string flag)`
**Lines**: 277-378

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_GETCOATINGDATA(FINISHING_GETCOATINGDATAParameter para)`
**Lines**: 28XXX (estimated)

---

**File**: 91/296 | **Progress**: 30.7%
