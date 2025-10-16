# FINISHING_GETSCOURINGREPORT

**Procedure Number**: 098 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete scouring data with specifications for reporting |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingScouring, tblFinishingScouringCondition |
| **Called From** | CoatingDataService.cs:2292 → FINISHING_GETSCOURINGREPORTList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number (greige fabric lot) |
| `P_FINLOT` | VARCHAR2(50) | ✅ | Finishing lot number (scouring lot) |

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
| `INPUTLENGTH` | NUMBER | Input length from weaving (m) |
| `OPERATOR_GROUP` | VARCHAR2(50) | Operator group/shift |
| `PARTNO` | VARCHAR2(50) | Part number |
| `FINISHLENGTH` | NUMBER | Final finished length (m) |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves comprehensive scouring process data combining actual process values, PLC setpoints, and specification ranges for complete quality documentation and reporting.

**When Used**:
- Generating official scouring process reports
- Quality documentation for customer requirements
- ISO/automotive certification compliance reports
- Process capability analysis (Cp/Cpk calculations using MIN/MAX ranges)
- Historical record keeping and traceability

**Business Rules**:
1. **Complete Data Package**: Returns process data + specifications in single query
2. **Dual Identification**: Can query by weaving lot OR finishing lot
3. **Quality Traceability**: Links greige fabric to finished product with full process history
4. **Specification Compliance**: Includes MIN/MAX ranges for automated compliance checking

**Workflow**:
1. User requests scouring process report (from Reports menu or finishing page)
2. User enters either weaving lot number or finishing lot number
3. System calls procedure with both parameters (one may be empty)
4. Procedure returns complete dataset:
   - Process identification (lots, item codes, customer)
   - Time tracking (start/end dates, operators)
   - PLC actual values (PV - Process Values)
   - PLC setpoints (SP - Set Points)
   - Specification ranges (MIN/MAX limits)
   - Mechanical settings (width, speed, frame)
   - Quality data (humidity, lengths)
5. Report generator uses data to create formatted document
6. Report shows actual vs. specification compliance

**Data Sources Combined**:
- **tblFinishingScouring**: Actual process data (PV/SP values, lengths, times)
- **tblFinishingScouringCondition**: Specification ranges (MIN/MAX limits)
- **tblFinishingLot**: Lot identification and linking

**Example Scenario**:
- Customer requests quality documentation for Finishing Lot "FS-2024-001"
- Quality engineer generates report using this procedure
- Report shows:
  - All 10 temperature zones with actual readings
  - MIN/MAX specification limits for each parameter
  - Compliance status: TEMP5_PV=172°C within TEMP5_MIN=168°C to TEMP5_MAX=178°C ✓
  - Complete operator tracking and timestamp history
  - Used for customer audits and certification documentation

**Why This Matters**:
- **Customer Requirements**: Automotive industry requires complete process documentation
- **ISO Certification**: Quality management system compliance
- **Process Capability**: MIN/MAX data enables Cp/Cpk statistical analysis
- **Audit Trail**: Complete traceability from raw fabric to finished product
- **Problem Solving**: Historical data for troubleshooting quality issues

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTSCOURING (creates initial record)
- FINISHING_UPDATESCOURING (updates process values)
- FINISHING_UPDATESCOURINGDATA (updates specification ranges)

**Downstream**:
- Used by report generation systems (RDLC reports)
- Quality documentation exports
- Customer portal data feeds

**Similar**:
- FINISHING_GETCOATINGREPORT (same pattern for coating reports)
- FINISHING_GETDRYERREPORT (same pattern for dryer reports)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETSCOURINGREPORTList(string WEAVINGLOT, string FINLOT)`
**Lines**: 2292-2436

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_GETSCOURINGREPORT(FINISHING_GETSCOURINGREPORTParameter para)`
**Lines**: 7916-8018 (Parameter: 7916-7920, Result: 7926-8018)

---

**File**: 98/296 | **Progress**: 33.1%
