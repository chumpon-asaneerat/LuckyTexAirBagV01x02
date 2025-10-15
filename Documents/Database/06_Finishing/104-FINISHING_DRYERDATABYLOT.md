# FINISHING_DRYERDATABYLOT

**Procedure Number**: 104 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve dryer processing data for a specific fabric lot and machine |
| **Operation** | SELECT |
| **Tables** | tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:2696 → GetFINISHING_DRYERDATABYLOT() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2 | ✅ | Machine number (dryer machine ID) |
| `P_WEAVINGLOT` | VARCHAR2 | ✅ | Weaving lot number to load dryer data for |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `FINISHINGLOT` | VARCHAR2 | Finishing lot number |
| `ITM_CODE` | VARCHAR2 | Item code |
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `FINISHINGCUSTOMER` | VARCHAR2 | Customer ID for finishing |
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
| `STATUSFLAG` | VARCHAR2 | Process status (START/FINISH/etc) |
| `WIDTH_BE_HEAT` | NUMBER | Fabric width before heating (cm) |
| `ACCPRESURE` | NUMBER | Accumulator pressure setting |
| `ASSTENSION` | NUMBER | Assist tension setting |
| `ACCARIDENSER` | NUMBER | Accumulator air density setting |
| `CHIFROT` | NUMBER | Chi front setting |
| `CHIREAR` | NUMBER | Chi rear setting |
| `DRYERTEMP1_PV` | NUMBER | Dryer temperature zone 1 - Process Value (°C) |
| `DRYERTEMP1_SP` | NUMBER | Dryer temperature zone 1 - Set Point (°C) |
| `SPEED_PV` | NUMBER | Machine speed - Process Value (m/min) |
| `SPEED_SP` | NUMBER | Machine speed - Set Point (m/min) |
| `STEAMPRESSURE` | NUMBER | Steam pressure (bar) |
| `DRYERCIRCUFAN` | NUMBER | Dryer circulation fan speed |
| `EXHAUSTFAN` | NUMBER | Exhaust fan speed |
| `WIDTH_AF_HEAT` | NUMBER | Fabric width after heating (cm) |
| `CONDITIONBY` | VARCHAR2 | Operator who set conditions |
| `CONDITIONDATE` | DATE | Condition setup date/time |
| `FINISHBY` | VARCHAR2 | Operator who finished process |
| `SAMPLINGID` | VARCHAR2 | Sampling ID for quality control |
| `STARTBY` | VARCHAR2 | Operator who started process |
| `REMARK` | VARCHAR2 | Additional remarks/notes |
| `HUMIDITY_BF` | NUMBER | Humidity before process (%) |
| `HUMIDITY_AF` | NUMBER | Humidity after process (%) |
| `REPROCESS` | VARCHAR2 | Reprocess flag (Y/N) |
| `WEAVLENGTH` | NUMBER | Original weaving length (meters) |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/shift |
| `HOTFLUE_MIN` | NUMBER | Hot flue minimum temperature spec (°C) |
| `HOTFLUE_MAX` | NUMBER | Hot flue maximum temperature spec (°C) |
| `SPEED_MIN` | NUMBER | Speed minimum spec (m/min) |
| `SPEED_MAX` | NUMBER | Speed maximum spec (m/min) |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |

---

## Business Logic

**Purpose**: Load existing dryer processing data for a weaving lot on a specific machine.

**When Used**: When operator selects a weaving lot on the dryer machine screen, this procedure loads all previously saved dryer processing parameters and measurements.

**Workflow**:
1. Operator enters or scans weaving lot number on dryer machine interface
2. System calls this procedure with machine number and weaving lot
3. Returns complete dryer processing record including:
   - Process timing (start/end dates)
   - Machine settings (temperature, speed, pressure, fan speeds)
   - Fabric measurements (widths before/after, multiple length readings)
   - Quality control data (humidity, sampling ID)
   - Operator tracking (who set conditions, who started, who finished)
   - Process status and remarks
4. UI displays data for operator review or modification

**Business Rules**:
- Each weaving lot can have multiple dryer processing records (one per pass)
- Machine number identifies which dryer machine performed the operation
- Temperature and speed have both PV (Process Value - actual) and SP (Set Point - target)
- Multiple length measurements (LENGTH1-LENGTH7) track fabric at different stages
- Width measurements show shrinkage from heating process
- Humidity before/after tracks moisture removal effectiveness
- Reprocess flag indicates if fabric failed initial QC

---

## Related Procedures

**Upstream**:
- [101-FINISHING_SCOURINGPLCDATA.md](./101-FINISHING_SCOURINGPLCDATA.md) - Scouring before dryer
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Coating before dryer

**Downstream**:
- [105-FINISHING_DRYERPLCDATA.md](./105-FINISHING_DRYERPLCDATA.md) - PLC data capture
- [106-FINISHING_GETDRYERCONDITION.md](./106-FINISHING_GETDRYERCONDITION.md) - Get dryer conditions

**Similar**:
- [088-FINISHING_COATINGDATABYLOT.md](./088-FINISHING_COATINGDATABYLOT.md) - Similar data retrieval for coating
- [100-FINISHING_SCOURINGDATABYLOT.md](./100-FINISHING_SCOURINGDATABYLOT.md) - Similar data retrieval for scouring

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `GetFINISHING_DRYERDATABYLOT(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 2675-2785

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_DRYERDATABYLOTParameter`
**Lines**: 8923-8929

**Result Class**: `FINISHING_DRYERDATABYLOTResult`
**Lines**: 8933-8983

**Method**: `FINISHING_DRYERDATABYLOT(FINISHING_DRYERDATABYLOTParameter para)`
**Lines**: 28559-28625

---

**File**: 104/296 | **Progress**: 35.1%
