# FINISHING_INSERTDRYER

**Procedure Number**: 109 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert new dryer processing record when starting or finishing dryer operation |
| **Operation** | INSERT |
| **Tables** | tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:2904 → FINISHING_INSERTDRYER() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2 | ✅ | Weaving lot number being processed |
| `P_ITMCODE` | VARCHAR2 | ✅ | Item code |
| `P_FINISHCUSTOMER` | VARCHAR2 | ✅ | Customer ID for finishing |
| `P_PRODUCTTYPEID` | VARCHAR2 | ✅ | Product type identifier |
| `P_OPERATORID` | VARCHAR2 | ✅ | Operator ID performing the operation |
| `P_MCNO` | VARCHAR2 | ✅ | Machine number (dryer machine ID) |
| `P_FLAG` | VARCHAR2 | ✅ | Operation flag (START/FINISH/CONDITION) |
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
| `P_HUMID_BF` | NUMBER | ⬜ | Humidity before process (%) |
| `P_HUMID_AF` | NUMBER | ⬜ | Humidity after process (%) |
| `P_REPROCESS` | VARCHAR2 | ⬜ | Reprocess flag (Y/N) |
| `P_WEAVLENGTH` | NUMBER | ⬜ | Weaving length (meters) |
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

**Purpose**: Create new dryer processing record or update existing record with operation details. Called at different stages of dryer processing (condition setup, start, finish).

**When Used**: Three main scenarios based on P_FLAG:

**1. CONDITION Setup (FLAG = 'CONDITION')**:
- Operator sets up dryer machine conditions before starting
- Saves all target values (SP - Set Points)
- Records who set conditions and when
- Machine ready but not yet running

**2. Process START (FLAG = 'START')**:
- Operator starts dryer machine
- Records actual process values (PV - Process Values)
- Updates status to in-process
- Captures start time and operator

**3. Process FINISH (FLAG = 'FINISH')**:
- Operator completes dryer run
- Records final measurements (widths, humidity after)
- Updates status to completed
- Captures finish time and operator

**Workflow**:
1. Operator loads weaving lot onto dryer machine
2. System validates weaving lot exists and is ready for drying
3. Operator sets machine conditions:
   - Temperature targets
   - Speed settings
   - Fan speeds
   - Pressure settings
4. System calls INSERT with FLAG='CONDITION'
5. Operator starts machine
6. System calls INSERT with FLAG='START', capturing actual PV values
7. Dryer process runs (minutes to hours depending on fabric length)
8. Operator monitors process, PLC captures real-time data
9. When complete, operator enters final measurements
10. System calls INSERT with FLAG='FINISH'
11. Record now complete with full traceability

**Business Rules**:
- Each weaving lot can be processed multiple times (rework if needed)
- REPROCESS flag indicates if this is a rework operation
- All process parameters compared against item specifications
- Width measurements show shrinkage/stretch during heating
- Humidity measurements verify moisture removal effectiveness
- Saturator and washing parameters only applicable for certain items
- Operator tracking required at each stage (condition, start, finish)
- Machine number identifies which dryer performed the operation

**Data Captured**:
- **Identification**: Weaving lot, item, customer, product type
- **Process Control**: Temperatures, speeds, pressures, fan settings
- **Quality**: Fabric widths (before/after), humidity (before/after)
- **Traceability**: Operators (who set conditions/started/finished), timestamps
- **Status**: FLAG indicates current stage of processing

---

## Related Procedures

**Upstream**:
- [106-FINISHING_GETDRYERCONDITION.md](./106-FINISHING_GETDRYERCONDITION.md) - Gets default conditions for item

**Downstream**:
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - Update existing dryer record
- [104-FINISHING_DRYERDATABYLOT.md](./104-FINISHING_DRYERDATABYLOT.md) - Retrieve created record

**Similar**:
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Insert coating record
- [099-FINISHING_INSERTSCOURING.md](./099-FINISHING_INSERTSCOURING.md) - Insert scouring record

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_INSERTDRYER(...)`
**Lines**: 2830-2932

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_INSERTDRYERParameter`
**Lines**: 7919-7955

**Result Class**: `FINISHING_INSERTDRYERResult`
**Lines**: 7959-7964

**Method**: `FINISHING_INSERTDRYER(FINISHING_INSERTDRYERParameter para)`
**Lines**: 27011-27104

---

**File**: 109/296 | **Progress**: 36.8%
