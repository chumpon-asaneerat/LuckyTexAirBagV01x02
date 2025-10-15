# FINISHING_UPDATEDRYERDATA

**Procedure Number**: 111 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update specific dryer data fields with tolerance ranges (min/max values) |
| **Operation** | UPDATE |
| **Tables** | tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:3103 → FINISHING_UPDATEDRYERDATA() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2 | ✅ | Finishing lot number to update |
| `P_FLAG` | VARCHAR2 | ✅ | Operation flag (START/CONDITION/FINISH) |
| `P_ITMCODE` | VARCHAR2 | ✅ | Item code |
| `P_WEAVINGLOT` | VARCHAR2 | ✅ | Weaving lot number |
| `P_CUSTOMER` | VARCHAR2 | ⬜ | Customer ID |
| `P_STARTDATE` | DATE | ⬜ | Process start date/time |
| `P_ENDDATE` | DATE | ⬜ | Process end date/time |
| `P_CONDITONDATE` | DATE | ⬜ | Condition setup date/time |
| `P_CONDITIONBY` | VARCHAR2 | ⬜ | Operator who set conditions |
| `P_FINISHBY` | VARCHAR2 | ⬜ | Operator who finished |
| `P_HOTFLUE` | NUMBER | ⬜ | Hot flue temperature value (°C) |
| `P_HOTFLUE_MIN` | NUMBER | ⬜ | Hot flue minimum acceptable temperature (°C) |
| `P_HOTFLUE_MAX` | NUMBER | ⬜ | Hot flue maximum acceptable temperature (°C) |
| `P_SPEED` | NUMBER | ⬜ | Machine speed value (m/min) |
| `P_SPEED_MIN` | NUMBER | ⬜ | Speed minimum acceptable value (m/min) |
| `P_SPEED_MAX` | NUMBER | ⬜ | Speed maximum acceptable value (m/min) |
| `P_WIDTHBEHEAT` | NUMBER | ⬜ | Fabric width before heating (cm) |
| `P_ACCPRESURE` | NUMBER | ⬜ | Accumulator pressure (bar) |
| `P_ASSTENSION` | NUMBER | ⬜ | Assist tension |
| `P_ACCARIDENSER` | NUMBER | ⬜ | Accumulator air density |
| `P_CHIFROT` | NUMBER | ⬜ | Chi front setting |
| `P_CHIREAR` | NUMBER | ⬜ | Chi rear setting |
| `P_STEAMPRESURE` | NUMBER | ⬜ | Steam pressure (bar) |
| `P_DRYCIRCUFAN` | NUMBER | ⬜ | Dryer circulation fan speed |
| `P_EXHAUSTFAN` | NUMBER | ⬜ | Exhaust fan speed |
| `P_WIDTHAFHEAT` | NUMBER | ⬜ | Fabric width after heating (cm) |
| `P_LENGTH1` | NUMBER | ⬜ | Length measurement 1 (meters) |
| `P_LENGTH2` | NUMBER | ⬜ | Length measurement 2 (meters) |
| `P_LENGTH3` | NUMBER | ⬜ | Length measurement 3 (meters) |
| `P_LENGTH4` | NUMBER | ⬜ | Length measurement 4 (meters) |
| `P_LENGTH5` | NUMBER | ⬜ | Length measurement 5 (meters) |
| `P_LENGTH6` | NUMBER | ⬜ | Length measurement 6 (meters) |
| `P_LENGTH7` | NUMBER | ⬜ | Length measurement 7 (meters) |
| `P_REMARK` | VARCHAR2 | ⬜ | Additional remarks |
| `P_HUMID_BF` | NUMBER | ⬜ | Humidity before process (%) |
| `P_HUMID_AF` | NUMBER | ⬜ | Humidity after process (%) |
| `P_GROUP` | VARCHAR2 | ⬜ | Operator group/shift |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2 | Return status (SUCCESS/ERROR message) |

---

## Business Logic

**Purpose**: Update existing dryer processing record with specific data fields including tolerance ranges. Unlike UPDATEDRYER (110) which uses PV/SP pattern, this procedure uses single value + MIN/MAX range pattern.

**When Used**: This procedure is called when updating dryer data with quality control tolerance ranges, particularly useful for:
- Updating temperature targets with acceptable min/max ranges
- Updating speed settings with tolerance margins
- Recording actual measurements against specifications

**Key Difference from UPDATEDRYER (110)**:

**UPDATEDRYER (110)**:
- Uses PV (Process Value) and SP (Set Point) pattern
- Example: `P_HOTFLUE_PV`, `P_HOTFLUE_SP`
- Captures actual vs target values

**UPDATEDRYERDATA (111)**:
- Uses single value + MIN/MAX range pattern
- Example: `P_HOTFLUE`, `P_HOTFLUE_MIN`, `P_HOTFLUE_MAX`
- Captures value with tolerance margins
- More focused on quality control specifications

**Workflow Example**:

**Scenario: Update Temperature with QC Tolerances**
1. Operator completes dryer run
2. Quality inspector reviews temperature data
3. System needs to update record with QC-approved ranges
4. Calls UPDATEDRYERDATA with:
   - `P_HOTFLUE` = 180 (target temperature)
   - `P_HOTFLUE_MIN` = 175 (minimum acceptable)
   - `P_HOTFLUE_MAX` = 185 (maximum acceptable)
5. Record updated with tolerance ranges for compliance reporting

**Scenario: Update Speed with Operating Window**
1. Process engineer reviews dryer performance
2. Establishes operating window for item/machine combination
3. System updates record with validated ranges:
   - `P_SPEED` = 25 m/min (optimal speed)
   - `P_SPEED_MIN` = 23 m/min (minimum)
   - `P_SPEED_MAX` = 27 m/min (maximum)
4. Future operations can validate against these ranges

**Business Rules**:
- `P_FINISHLOT` must exist (cannot update non-existent lot)
- `P_WEAVINGLOT` must be provided (required traceability)
- `P_ITMCODE` must be provided (required for validation)
- FLAG determines update context (same as UPDATEDRYER)
- MIN/MAX values define acceptable tolerance ranges
- Used primarily for quality control and compliance documentation
- Tolerance ranges help identify out-of-spec conditions

**Comparison**:
- `UPDATEDRYER` (110): General updates with PV/SP pattern
- `UPDATEDRYERDATA` (111): Specific updates with MIN/MAX tolerance ranges
- Both update same table, different data patterns

---

## Related Procedures

**Upstream**:
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates initial record
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - General updates

**Downstream**:
- [108-FINISHING_GETDRYERREPORT.md](./108-FINISHING_GETDRYERREPORT.md) - Retrieves data for reporting
- [107-FINISHING_GETDRYERDATA.md](./107-FINISHING_GETDRYERDATA.md) - Retrieves updated data

**Similar**:
- [103-FINISHING_UPDATESCOURINGDATA.md](./103-FINISHING_UPDATESCOURINGDATA.md) - Update scouring with tolerances
- [095-FINISHING_UPDATECOATINGDATA.md](./095-FINISHING_UPDATECOATINGDATA.md) - Update coating with tolerances

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_UPDATEDRYERDATA(...)`
**Lines**: 3101-3187

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_UPDATEDRYERDATAParameter`
**Lines**: 7278-7317

**Result Class**: `FINISHING_UPDATEDRYERDATAResult`
**Lines**: 7321-7326

**Method**: `FINISHING_UPDATEDRYERDATA(FINISHING_UPDATEDRYERDATAParameter para)`
**Lines**: 25878-25978

---

**File**: 111/296 | **Progress**: 37.5%
