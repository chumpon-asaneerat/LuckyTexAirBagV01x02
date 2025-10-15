# FINISHING_SCOURINGDATABYLOT

**Procedure Number**: 100 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve existing scouring process data for a lot being resumed/edited |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingScouring, tblFinishingScouringCondition |
| **Called From** | CoatingDataService.cs:1617 → GetFINISHING_SCOURINGDATABYLOT() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number (scouring machine ID) |
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number (greige fabric lot) |

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
| `LENGTH1-7` | NUMBER | Length measurements at various checkpoints (m) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `STATUSFLAG` | VARCHAR2(10) | Status flag (START/FINISH/CANCEL) |
| `SATURATOR_CHEM_PV/SP` | NUMBER | Chemical saturator PV/SP values (°C) |
| `WASHING1_PV/SP` | NUMBER | Washing zone 1 PV/SP values (°C) |
| `WASHING2_PV/SP` | NUMBER | Washing zone 2 PV/SP values (°C) |
| `HOTFLUE_PV/SP` | NUMBER | Hot flue temperature PV/SP values (°C) |
| `TEMP1-10_PV/SP` | NUMBER | Temperature zone 1-10 PV/SP values (°C) |
| `TEMP1-8_MIN/MAX` | NUMBER | Temperature zone min/max specification limits (°C) |
| `SPEED_PV/SP` | NUMBER | Machine speed PV/SP (m/min) |
| `SPEED_MIN/MAX` | NUMBER | Speed specification limits (m/min) |
| `MAINFRAMEWIDTH` | NUMBER | Main frame width (mm) |
| `WIDTH_BE` | NUMBER | Width before scouring (mm) |
| `WIDTH_AF` | NUMBER | Width after scouring (mm) |
| `PIN2PIN` | NUMBER | Pin to pin distance (mm) |
| `SAT_CHEM_MIN/MAX` | NUMBER | Saturator temperature specification limits (°C) |
| `WASHING1_MIN/MAX` | NUMBER | Washing 1 temperature specification limits (°C) |
| `WASHING2_MIN/MAX` | NUMBER | Washing 2 temperature specification limits (°C) |
| `HOTFLUE_MIN/MAX` | NUMBER | Hot flue temperature specification limits (°C) |
| `CONDITIONBY` | VARCHAR2(50) | Condition set by operator |
| `CONDITIONDATE` | DATE | Condition set date |
| `FINISHBY` | VARCHAR2(50) | Finished by operator |
| `SAMPLINGID` | VARCHAR2(50) | Sampling ID reference |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `REMARK` | VARCHAR2(500) | Process remarks/notes |
| `HUMIDITY_BF` | NUMBER | Humidity before (%) |
| `HUMIDITY_AF` | NUMBER | Humidity after (%) |
| `REPROCESS` | VARCHAR2(10) | Reprocess flag (Y/N) |
| `WEAVLENGTH` | NUMBER | Original weaving length (m) |
| `OPERATOR_GROUP` | VARCHAR2(50) | Operator group/shift |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |

---

## Business Logic (What it does and why)

**Purpose**: Loads previously saved scouring process data when an operator returns to continue or edit a scouring operation that was started earlier. Includes both process values and specification ranges for quality monitoring.

**When Used**:
- When operator scans a weaving lot that already has scouring data
- On the Scouring Finishing page after entering machine and lot
- Used to populate UI with existing process parameters for continuation or editing
- Supports pause-and-resume workflow for long scouring operations

**Business Rules**:
1. **Process Continuity**: Operators can pause scouring operations and resume later with same settings
2. **Data Recovery**: All PLC parameters, machine settings, and process conditions are saved and recoverable
3. **Edit Mode**: Allows editing of in-progress or completed scouring lots
4. **Machine-Lot Matching**: Retrieves data only for specific machine-lot combination
5. **Quality Monitoring**: Returns MIN/MAX specification ranges for real-time compliance checking

**Workflow**:
1. Operator enters scouring machine number (P_MCNO)
2. Operator scans weaving lot barcode (P_WEAVINGLOT)
3. System calls procedure to check if scouring data exists for this combination
4. If found → Loads all process parameters and specifications into UI form fields:
   - PLC setpoints and actual values (temperatures, speeds)
   - Machine settings (width, frame, pin-to-pin)
   - Quality parameters (humidity, lengths)
   - MIN/MAX specification limits for compliance display
   - Status and operator tracking
5. If not found → Blank form for new scouring operation
6. Operator can continue from where left off or edit existing data
7. UI displays actual values vs. spec ranges with color-coded status

**Example Scenario**:
- Operator on Scouring Machine SC1 is scouring Weaving Lot "WV-2024-001"
- After 400m, shift ends → Process paused (status = START)
- Next shift operator enters SC1 and scans WV-2024-001
- Procedure returns saved data:
  - FINISHINGLOT = "FS-2024-0125"
  - LENGTH1 = 400m (already processed)
  - All 10 temperature zone PV/SP values preserved
  - TEMP1_MIN/MAX ranges for compliance checking
  - STATUSFLAG = "START" (indicates in-progress)
- Operator continues from 400m with same settings
- UI shows green indicators for temps within spec, red if out of range

**Why This Matters**:
- Long scouring runs can span multiple shifts (6-10 hours)
- PLC settings must remain consistent throughout the lot
- Quality tracking requires continuous parameter monitoring with spec compliance
- MIN/MAX ranges enable real-time deviation alerts
- Allows correction of data entry errors after process completion
- Supports quality documentation with specification tracking

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item compatibility first)
- FINISHING_INSERTSCOURING (creates initial scouring record)

**Downstream**:
- FINISHING_UPDATESCOURING (updates record with new values)
- FINISHING_SCOURINGPLCDATA (updates real-time PLC data)

**Similar**:
- FINISHING_COATINGDATABYLOT (same pattern for coating process)
- FINISHING_DRYERDATABYLOT (same pattern for dryer process)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `GetFINISHING_SCOURINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 1617-1758

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_SCOURINGDATABYLOT(FINISHING_SCOURINGDATABYLOTParameter para)`
**Lines**: 7568-7668 (Parameter: 7568-7572, Result: 7578-7668)

---

**File**: 100/296 | **Progress**: 33.8%
