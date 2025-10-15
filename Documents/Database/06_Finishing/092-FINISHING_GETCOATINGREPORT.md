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
| `LENGTH1-7` | NUMBER | Length measurements (m) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `STATUSFLAG` | VARCHAR2(10) | Status (START/FINISH) |
| `SATURATOR_CHEM_PV/SP` | NUMBER | Chemical saturator values |
| `WASHING1_PV/SP` | NUMBER | Washing zone 1 values |
| `WASHING2_PV/SP` | NUMBER | Washing zone 2 values |
| `HOTFLUE_PV/SP` | NUMBER | Hot flue values |
| `BE_COATWIDTH` | NUMBER | Before coating width (mm) |
| `TEMP1-10_PV/SP` | NUMBER | Temperature zones 1-10 (°C) |
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
| `SPEED_PV/SP` | NUMBER | Machine speed (m/min) |
| `WIDTHCOAT` | NUMBER | Coating width (mm) |
| `WIDTHCOATALL` | NUMBER | Total coating width (mm) |
| `SILICONE_A` | VARCHAR2(50) | Silicone A type/batch |
| `SILICONE_B` | VARCHAR2(50) | Silicone B type/batch |
| `COATINGWEIGTH_L/C/R` | NUMBER | Coating weight Left/Center/Right (g/m²) |
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
