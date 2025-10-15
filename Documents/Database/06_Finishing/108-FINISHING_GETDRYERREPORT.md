# FINISHING_GETDRYERREPORT

**Procedure Number**: 108 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve comprehensive dryer processing data for report generation |
| **Operation** | SELECT |
| **Tables** | tblFinishingDryer, related lookup tables (inferred) |
| **Called From** | CoatingDataService.cs:3210 → FINISHING_GETDRYERREPORTList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2 | ⬜ | Weaving lot number (optional filter) |
| `P_FINLOT` | VARCHAR2 | ⬜ | Finishing lot number (optional filter) |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `FINISHINGLOT` | VARCHAR2 | Finishing lot number |
| `ITM_CODE` | VARCHAR2 | Item code |
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `FINISHINGCUSTOMER` | VARCHAR2 | Customer ID |
| `STARTDATE` | DATE | Process start date/time |
| `ENDDATE` | DATE | Process end date/time |
| `PRODUCTTYPEID` | VARCHAR2 | Product type identifier |
| `LENGTH1` | NUMBER | Length measurement 1 (meters) |
| `LENGTH2` | NUMBER | Length measurement 2 (meters) |
| `LENGTH3` | NUMBER | Length measurement 3 (meters) |
| `LENGTH4` | NUMBER | Length measurement 4 (meters) |
| `LENGTH5` | NUMBER | Length measurement 5 (meters) |
| `LENGTH6` | NUMBER | Length measurement 6 (meters) |
| `LENGTH7` | NUMBER | Length measurement 7 (meters) |
| `MCNO` | VARCHAR2 | Machine number |
| `STATUSFLAG` | VARCHAR2 | Process status |
| `WIDTH_BE_HEAT` | NUMBER | Fabric width before heating (cm) |
| `ACCPRESURE` | NUMBER | Accumulator pressure |
| `ASSTENSION` | NUMBER | Assist tension |
| `ACCARIDENSER` | NUMBER | Accumulator air density |
| `CHIFROT` | NUMBER | Chi front setting |
| `CHIREAR` | NUMBER | Chi rear setting |
| `DRYERTEMP1_PV` | NUMBER | Dryer temperature - Process Value (°C) |
| `DRYERTEMP1_SP` | NUMBER | Dryer temperature - Set Point (°C) |
| `SPEED_PV` | NUMBER | Machine speed - Process Value (m/min) |
| `SPEED_SP` | NUMBER | Machine speed - Set Point (m/min) |
| `HOTFLUE_MIN` | NUMBER | Hot flue temperature minimum spec (°C) |
| `HOTFLUE_MAX` | NUMBER | Hot flue temperature maximum spec (°C) |
| `SPEED_MIN` | NUMBER | Speed minimum spec (m/min) |
| `SPEED_MAX` | NUMBER | Speed maximum spec (m/min) |
| `STEAMPRESSURE` | NUMBER | Steam pressure (bar) |
| `DRYERCIRCUFAN` | NUMBER | Circulation fan speed |
| `EXHAUSTFAN` | NUMBER | Exhaust fan speed |
| `WIDTH_AF_HEAT` | NUMBER | Fabric width after heating (cm) |
| `CONDITIONBY` | VARCHAR2 | Operator who set conditions |
| `CONDITIONDATE` | DATE | Condition setup date/time |
| `FINISHBY` | VARCHAR2 | Operator who finished |
| `SAMPLINGID` | VARCHAR2 | Sampling ID |
| `STARTBY` | VARCHAR2 | Operator who started |
| `REMARK` | VARCHAR2 | Additional remarks |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/shift |
| `HUMIDITY_BF` | NUMBER | Humidity before (%) |
| `HUMIDITY_AF` | NUMBER | Humidity after (%) |
| `PARTNO` | VARCHAR2 | Part number |
| `FINISHLENGTH` | NUMBER | Final length after dryer (meters) |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `INPUTLENGTH` | NUMBER | Input length to dryer (meters) |
| `SATURATOR_CHEM_PV` | NUMBER | Saturator chemical - Process Value (%) |
| `SATURATOR_CHEM_SP` | NUMBER | Saturator chemical - Set Point (%) |
| `WASHING1_PV` | NUMBER | Washing zone 1 - Process Value |
| `WASHING1_SP` | NUMBER | Washing zone 1 - Set Point |
| `WASHING2_PV` | NUMBER | Washing zone 2 - Process Value |
| `WASHING2_SP` | NUMBER | Washing zone 2 - Set Point |

---

## Business Logic

**Purpose**: Retrieve comprehensive dryer processing data formatted for report generation. Includes all process parameters, specifications, and calculated values needed for production reports, quality reports, and certificate of analysis.

**When Used**:
- Generate dryer processing reports for management
- Create quality certificates for customers
- Print process cards or travelers
- Export data for statistical analysis
- Audit trail and compliance documentation

**Workflow**:
1. User requests dryer processing report from report menu
2. User provides search criteria:
   - By weaving lot number (track specific fabric)
   - By finishing lot number (track specific dryer run)
   - Or both for precise filtering
3. System calls this procedure with filter parameters
4. Returns comprehensive data set including:
   - Process identification (lots, item, customer, dates)
   - Machine settings and actual values
   - Specification limits (min/max for temperature and speed)
   - Quality measurements (widths, humidity, lengths)
   - Operator tracking (who did what and when)
   - Additional parameters (saturator, washing if applicable)
5. Report engine formats data into professional report layout
6. Report can be viewed on screen, printed, or exported (PDF, Excel)

**Business Rules**:
- Both parameters are optional - allows flexible filtering:
  - If both NULL: Returns all dryer records (use cautiously - could be large)
  - If WEAVINGLOT provided: Returns all dryer operations for that weaving lot
  - If FINLOT provided: Returns specific finishing lot
  - If both provided: Most specific filter (single record usually)
- Includes spec limits (HOTFLUE_MIN/MAX, SPEED_MIN/MAX) for spec compliance verification
- Shows both actual (PV) and target (SP) values for quality assessment
- FINISHLENGTH vs. INPUTLENGTH shows shrinkage/stretch during drying
- PARTNO links to customer part number for traceability

**Report Types Using This Data**:
- **Process Card**: Shows machine settings and actual values
- **Quality Certificate**: Confirms specs were met, shows measurements
- **Production Report**: Throughput, efficiency, quality metrics
- **Compliance Report**: Audit trail of who, what, when
- **Trend Analysis**: Historical data for statistical process control

---

## Related Procedures

**Upstream**:
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates data used in reports

**Downstream**:
- None (terminal procedure - data goes to reports)

**Similar**:
- [092-FINISHING_GETCOATINGREPORT.md](./092-FINISHING_GETCOATINGREPORT.md) - Coating report data
- [098-FINISHING_GETSCOURINGREPORT.md](./098-FINISHING_GETSCOURINGREPORT.md) - Scouring report data

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETDRYERREPORTList(string WEAVINGLOT, string FINLOT)`
**Lines**: 3189-3294

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_GETDRYERREPORTParameter`
**Lines**: 8383-8389

**Result Class**: `FINISHING_GETDRYERREPORTResult`
**Lines**: 8393-8450

**Method**: `FINISHING_GETDRYERREPORT(FINISHING_GETDRYERREPORTParameter para)`
**Lines**: 27765-27863

---

**File**: 108/296 | **Progress**: 36.5%
