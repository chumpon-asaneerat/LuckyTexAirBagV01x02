# FINISHING_GETDRYERDATA

**Procedure Number**: 107 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve dryer processing records filtered by machine and status flag |
| **Operation** | SELECT |
| **Tables** | tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:2596 → FINISHING_GETDRYERDATAList() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2 | ✅ | Machine number (dryer machine ID) |
| `P_FLAG` | VARCHAR2 | ✅ | Status flag filter (START/FINISH/ALL/etc) |

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
| `STATUSFLAG` | VARCHAR2 | Current process status |
| `WIDTH_BE_HEAT` | NUMBER | Fabric width before heating (cm) |
| `ACCPRESURE` | NUMBER | Accumulator pressure actual |
| `ASSTENSION` | NUMBER | Assist tension actual |
| `ACCARIDENSER` | NUMBER | Accumulator air density actual |
| `CHIFROT` | NUMBER | Chi front actual |
| `CHIREAR` | NUMBER | Chi rear actual |
| `DRYERTEMP1_PV` | NUMBER | Dryer temperature - Process Value (°C) |
| `DRYERTEMP1_SP` | NUMBER | Dryer temperature - Set Point (°C) |
| `SPEED_PV` | NUMBER | Machine speed - Process Value (m/min) |
| `SPEED_SP` | NUMBER | Machine speed - Set Point (m/min) |
| `STEAMPRESSURE` | NUMBER | Steam pressure (bar) |
| `DRYERCIRCUFAN` | NUMBER | Circulation fan speed |
| `EXHAUSTFAN` | NUMBER | Exhaust fan speed |
| `WIDTH_AF_HEAT` | NUMBER | Fabric width after heating (cm) |
| `CONDITIONBY` | VARCHAR2 | Operator who set conditions |
| `CONDITIONDATE` | DATE | Condition setup date/time |
| `FINISHBY` | VARCHAR2 | Operator who finished process |
| `SAMPLINGID` | VARCHAR2 | Sampling ID |
| `STARTBY` | VARCHAR2 | Operator who started process |
| `REMARK` | VARCHAR2 | Additional remarks |
| `HUMIDITY_BF` | NUMBER | Humidity before process (%) |
| `HUMIDITY_AF` | NUMBER | Humidity after process (%) |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `WEAVINGLENGTH` | NUMBER | Original weaving length (meters) |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/shift |
| `SATURATOR_CHEM_PV` | NUMBER | Saturator chemical - Process Value (%) |
| `SATURATOR_CHEM_SP` | NUMBER | Saturator chemical - Set Point (%) |
| `WASHING1_PV` | NUMBER | Washing zone 1 - Process Value |
| `WASHING1_SP` | NUMBER | Washing zone 1 - Set Point |
| `WASHING2_PV` | NUMBER | Washing zone 2 - Process Value |
| `WASHING2_SP` | NUMBER | Washing zone 2 - Set Point |

---

## Business Logic

**Purpose**: Get dryer processing records for a specific machine filtered by status flag. Used for displaying in-process, completed, or all dryer lots.

**When Used**: Various scenarios:
- Display in-process dryer lots (FLAG = 'START')
- Show completed dryer runs (FLAG = 'FINISH')
- Search all dryer history (FLAG = 'ALL' or empty)
- Monitor machine workload and throughput

**Workflow**:
1. User opens dryer machine monitoring or search screen
2. Selects machine and status filter:
   - "In Process" → FLAG = 'START'
   - "Completed" → FLAG = 'FINISH'
   - "All Records" → FLAG = 'ALL'
3. System calls this procedure with machine number and flag
4. Returns list of dryer records matching criteria
5. UI displays records in grid/list format
6. User can select record to view details or continue processing

**Business Rules**:
- `P_FLAG` filters by STATUSFLAG column:
  - 'START': Lots currently in process (started but not finished)
  - 'FINISH': Completed lots
  - 'ALL' or NULL: All records regardless of status
- Each machine can have multiple lots in various states
- Records include both actual values (PV) and set points (SP) for comparison
- Saturator and washing columns may be NULL if not applicable to the item

**Comparison with Similar Procedures**:
- `DRYERDATABYLOT` (104): Gets ONE specific lot by weaving lot number
- `GETDRYERDATA` (107): Gets MULTIPLE lots filtered by machine and status

---

## Related Procedures

**Upstream**:
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates records returned by this

**Downstream**:
- [104-FINISHING_DRYERDATABYLOT.md](./104-FINISHING_DRYERDATABYLOT.md) - Get details of selected lot
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - Update selected lot

**Similar**:
- [091-FINISHING_GETCOATINGDATA.md](./091-FINISHING_GETCOATINGDATA.md) - Get coating data by machine/flag
- [097-FINISHING_GETSCOURINGDATA.md](./097-FINISHING_GETSCOURINGDATA.md) - Get scouring data by machine/flag

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETDRYERDATAList(string flag, string mcNo)`
**Lines**: 2575-2675

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_GETDRYERDATAParameter`
**Lines**: 8454-8460

**Result Class**: `FINISHING_GETDRYERDATAResult`
**Lines**: 8464-8515

**Method**: `FINISHING_GETDRYERDATA(FINISHING_GETDRYERDATAParameter para)`
**Lines**: 27865-27957

---

**File**: 107/296 | **Progress**: 36.1%
