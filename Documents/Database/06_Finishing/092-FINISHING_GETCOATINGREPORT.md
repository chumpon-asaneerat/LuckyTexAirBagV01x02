# FINISHING_GETCOATINGREPORT

**Procedure Number**: 092 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete coating process data for report generation |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingCoating |
| **Called From** | CoatingDataService.cs:1200 → FINISHING_GETCOATINGREPORTList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number |
| `P_FINLOT` | VARCHAR2(50) | ✅ | Finishing lot number |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot number |
| `ITM_CODE` | VARCHAR2(50) | Finished goods item code |
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number |
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
| `STATUSFLAG` | VARCHAR2(10) | Status (START/FINISH) |
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
| `FANRPM` | NUMBER | Fan RPM |
| `EXFAN_FRONT_BACK` | NUMBER | Exhaust fan front/back |
| `EXFAN_MIDDLE` | NUMBER | Exhaust fan middle |
| `ANGLEKNIFE` | NUMBER | Knife angle (degrees) |
| `BLADENO` | VARCHAR2(50) | Blade number |
| `BLADEDIRECTION` | VARCHAR2(50) | Blade direction |
| `CYLINDER_TENSIONUP` | NUMBER | Cylinder tension up |
| `OPOLE_TENSIONDOWN` | NUMBER | O-pole tension down |
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
| `SAMPLINGID` | VARCHAR2(50) | Sampling ID |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `REMARK` | VARCHAR2(500) | Process remarks |
| `HUMIDITY_BF` | NUMBER | Humidity before (%) |
| `HUMIDITY_AF` | NUMBER | Humidity after (%) |
| `REPROCESS` | VARCHAR2(10) | Reprocess flag (Y/N) |
| `INPUTLENGTH` | NUMBER | Input length (m) |
| `OPERATOR_GROUP` | VARCHAR2(50) | Operator group/shift |
| `TEMP1_MIN` | NUMBER | Temperature zone 1 minimum limit (°C) |
| `TEMP1_MAX` | NUMBER | Temperature zone 1 maximum limit (°C) |
| `TEMP2_MIN` | NUMBER | Temperature zone 2 minimum limit (°C) |
| `TEMP2_MAX` | NUMBER | Temperature zone 2 maximum limit (°C) |
| `TEMP3_MIN` | NUMBER | Temperature zone 3 minimum limit (°C) |
| `TEMP3_MAX` | NUMBER | Temperature zone 3 maximum limit (°C) |
| `TEMP4_MIN` | NUMBER | Temperature zone 4 minimum limit (°C) |
| `TEMP4_MAX` | NUMBER | Temperature zone 4 maximum limit (°C) |
| `TEMP5_MIN` | NUMBER | Temperature zone 5 minimum limit (°C) |
| `TEMP5_MAX` | NUMBER | Temperature zone 5 maximum limit (°C) |
| `TEMP6_MIN` | NUMBER | Temperature zone 6 minimum limit (°C) |
| `TEMP6_MAX` | NUMBER | Temperature zone 6 maximum limit (°C) |
| `TEMP7_MIN` | NUMBER | Temperature zone 7 minimum limit (°C) |
| `TEMP7_MAX` | NUMBER | Temperature zone 7 maximum limit (°C) |
| `TEMP8_MIN` | NUMBER | Temperature zone 8 minimum limit (°C) |
| `TEMP8_MAX` | NUMBER | Temperature zone 8 maximum limit (°C) |
| `TEMP9_MIN` | NUMBER | Temperature zone 9 minimum limit (°C) |
| `TEMP9_MAX` | NUMBER | Temperature zone 9 maximum limit (°C) |
| `TEMP10_MIN` | NUMBER | Temperature zone 10 minimum limit (°C) |
| `TEMP10_MAX` | NUMBER | Temperature zone 10 maximum limit (°C) |
| `SAT_CHEM_MIN` | NUMBER | Saturator chemical minimum limit |
| `SAT_CHEM_MAX` | NUMBER | Saturator chemical maximum limit |
| `HOTFLUE_MIN` | NUMBER | Hot flue minimum limit |
| `HOTFLUE_MAX` | NUMBER | Hot flue maximum limit |
| `WASH1_MIN` | NUMBER | Washing zone 1 minimum limit |
| `WASH1_MAX` | NUMBER | Washing zone 1 maximum limit |
| `WASH2_MIN` | NUMBER | Washing zone 2 minimum limit |
| `WASH2_MAX` | NUMBER | Washing zone 2 maximum limit |
| `TENUP_MIN` | NUMBER | Tension up minimum limit |
| `TENUP_MAX` | NUMBER | Tension up maximum limit |
| `TENDOWN_MIN` | NUMBER | Tension down minimum limit |
| `TENDOWN_MAX` | NUMBER | Tension down maximum limit |
| `SPEED_MIN` | NUMBER | Speed minimum limit (m/min) |
| `SPEED_MAX` | NUMBER | Speed maximum limit (m/min) |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves comprehensive coating process data for a specific lot combination for report generation and documentation.

**When Used**:
- Generating coating process reports
- Quality documentation
- Customer shipment documentation
- Process traceability records
- Audit trail generation

**Business Rules**:
1. **Lot Combination**: Requires both weaving lot and finishing lot for exact record identification
2. **Complete Data**: Returns all process parameters, machine settings, and PLC values
3. **Report Ready**: Data formatted for direct use in report templates
4. **Traceability**: Links weaving fabric to finished coating process

**Workflow**:
1. User requests report for finished lot
2. System calls procedure with weaving lot and finishing lot numbers
3. Procedure retrieves complete process record
4. Returns all parameters recorded during coating
5. Report generator uses data to populate report template

**Why This Matters**:
- Customer quality documentation requirements
- ISO/automotive certification compliance
- Process traceability for defect investigation
- Production record keeping
- Quality assurance validation

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTCOATING (creates records)
- FINISHING_UPDATECOATING (updates records)

**Downstream**:
- Used by report generation systems
- Quality documentation systems

**Similar**:
- FINISHING_GETSCOURINGREPORT (scouring reports)
- FINISHING_GETDRYERREPORT (dryer reports)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETCOATINGREPORTList(string WEAVINGLOT, string FINLOT)`
**Lines**: 1200-1350

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_GETCOATINGREPORT(FINISHING_GETCOATINGREPORTParameter para)`
**Lines**: 28XXX (estimated)

---

**File**: 92/296 | **Progress**: 31.1%
