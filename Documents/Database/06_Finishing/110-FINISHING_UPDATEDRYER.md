# FINISHING_UPDATEDRYER

**Procedure Number**: 110 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update existing dryer processing record (start, condition, or finish operations) |
| **Operation** | UPDATE |
| **Tables** | tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:2988, 3084 → FINISHING_UPDATEDRYERProcessing(), FINISHING_UPDATEDRYERFinishing() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2 | ✅ | Finishing lot number to update |
| `P_FLAG` | VARCHAR2 | ✅ | Operation flag (START/CONDITION/FINISH) |
| `P_ITMCODE` | VARCHAR2 | ⬜ | Item code |
| `P_WEAVINGLOT` | VARCHAR2 | ⬜ | Weaving lot number |
| `P_CUSTOMER` | VARCHAR2 | ⬜ | Customer ID |
| `P_STARTDATE` | DATE | ⬜ | Process start date/time |
| `P_ENDDATE` | DATE | ⬜ | Process end date/time |
| `P_CONDITONDATE` | DATE | ⬜ | Condition setup date/time |
| `P_CONDITIONBY` | VARCHAR2 | ⬜ | Operator who set conditions |
| `P_FINISHBY` | VARCHAR2 | ⬜ | Operator who finished |
| `P_HOTFLUE_PV` | NUMBER | ⬜ | Hot flue temperature - Process Value (°C) |
| `P_HOTFLUE_SP` | NUMBER | ⬜ | Hot flue temperature - Set Point (°C) |
| `P_SPEED_PV` | NUMBER | ⬜ | Machine speed - Process Value (m/min) |
| `P_SPEED_SP` | NUMBER | ⬜ | Machine speed - Set Point (m/min) |
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
| `P_SATURATOR_PV` | NUMBER | ⬜ | Saturator chemical - Process Value (%) |
| `P_SATURATOR_SP` | NUMBER | ⬜ | Saturator chemical - Set Point (%) |
| `P_WASHING1_PV` | NUMBER | ⬜ | Washing zone 1 - Process Value |
| `P_WASHING1_SP` | NUMBER | ⬜ | Washing zone 1 - Set Point |
| `P_WASHING2_PV` | NUMBER | ⬜ | Washing zone 2 - Process Value |
| `P_WASHING2_SP` | NUMBER | ⬜ | Washing zone 2 - Set Point |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2 | Return status (SUCCESS/ERROR message) |

---

## Business Logic

**Purpose**: Update existing dryer processing record at different stages of the operation. Unlike INSERT which creates new record, this updates existing record identified by finishing lot number.

**When Used**: Three main update scenarios based on P_FLAG:

**1. Update CONDITION (FLAG = 'CONDITION')**:
- Operator modifies dryer machine conditions after initial setup
- Updates target values (SP - Set Points)
- Records who updated and when
- Used when conditions need adjustment before or during processing

**2. Update START (FLAG = 'START')**:
- Operator starts dryer machine (updating status from setup to in-process)
- Updates actual process values (PV - Process Values)
- Records start timestamp and operator
- May update machine parameters that were adjusted at startup

**3. Update FINISH (FLAG = 'FINISH')**:
- Operator completes dryer run
- Updates final measurements (widths after heating, final lengths)
- Records finish timestamp and operator
- Updates humidity after process, remarks
- Changes status to completed

**Workflow Examples**:

**Scenario 1: Condition Adjustment**
1. Operator sets initial conditions using INSERT (procedure 109)
2. Quality team reviews and requests temperature adjustment
3. Operator modifies temperature settings
4. System calls UPDATE with FLAG='CONDITION'
5. Condition record updated with new values and timestamp

**Scenario 2: Process Continuation**
1. Dryer record created earlier (INSERT)
2. Operator starts machine
3. System calls UPDATE with FLAG='START' and captures start time
4. Process runs for several hours
5. Operator completes run
6. System calls UPDATE with FLAG='FINISH' with final measurements

**Scenario 3: Data Correction**
1. Operator realizes width measurement was recorded incorrectly
2. Supervisor authorizes correction
3. System calls UPDATE with corrected width value
4. Record updated with audit trail

**Business Rules**:
- `P_FINISHLOT` must exist (cannot update non-existent lot)
- FLAG determines which fields are updatable:
  - 'CONDITION': Machine settings, condition by/date
  - 'START': Start date, start by, PV values
  - 'FINISH': End date, finish by, final measurements, remarks
- Operator tracking maintained for all updates
- Multiple updates allowed (e.g., start then finish)
- All original data preserved unless explicitly updated
- Audit trail maintained (who updated what and when)

**Comparison**:
- `INSERTDRYER` (109): Creates NEW dryer record
- `UPDATEDRYER` (110): Modifies EXISTING dryer record by finishing lot

---

## Related Procedures

**Upstream**:
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates initial record

**Downstream**:
- [111-FINISHING_UPDATEDRYERDATA.md](./111-FINISHING_UPDATEDRYERDATA.md) - Update specific data fields

**Similar**:
- [094-FINISHING_UPDATECOATING.md](./094-FINISHING_UPDATECOATING.md) - Update coating record
- [102-FINISHING_UPDATESCOURING.md](./102-FINISHING_UPDATESCOURING.md) - Update scouring record

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Methods**:
- `FINISHING_UPDATEDRYERProcessing(...)` - Lines 2921-3002 (update during processing)
- `FINISHING_UPDATEDRYERFinishing(...)` - Lines 3005-3098 (update at finish)

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_UPDATEDRYERParameter`
**Lines**: 7330-7375

**Result Class**: `FINISHING_UPDATEDRYERResult`
**Lines**: 7379-7384

**Method**: `FINISHING_UPDATEDRYER(FINISHING_UPDATEDRYERParameter para)`
**Lines**: 25982-26090

---

**File**: 110/296 | **Progress**: 37.2%
