# FINISHING_INSERTCOATING

**Procedure Number**: 093 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new coating process record with all process parameters |
| **Operation** | INSERT |
| **Tables** | tblFinishingLot, tblFinishingCoating |
| **Called From** | CoatingDataService.cs:655 → FINISHING_INSERTCOATING() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2(50) | ✅ | Weaving lot number |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Finished goods item code |
| `P_FINISHCUSTOMER` | VARCHAR2(50) | ✅ | Customer code |
| `P_PRODUCTTYPEID` | VARCHAR2(50) | ✅ | Product type ID |
| `P_OPERATORID` | VARCHAR2(50) | ✅ | Operator ID |
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag (START/FINISH) |
| `P_SATURATOR_PV/SP` | NUMBER | ⬜ | Saturator process/setpoint values |
| `P_WASHING1_PV/SP` | NUMBER | ⬜ | Washing zone 1 values |
| `P_WASHING2_PV/SP` | NUMBER | ⬜ | Washing zone 2 values |
| `P_HOTFLUE_PV/SP` | NUMBER | ⬜ | Hot flue values |
| `P_TEMP1-10_PV/SP` | NUMBER | ⬜ | Temperature zones 1-10 values (°C) |
| `P_SPEED_PV/SP` | NUMBER | ⬜ | Machine speed values (m/min) |
| `P_BECOATWIDTH` | NUMBER | ⬜ | Before coating width (mm) |
| `P_FANRPM` | NUMBER | ⬜ | Fan RPM |
| `P_EXFAN_FRONT_BACK` | NUMBER | ⬜ | Exhaust fan front/back |
| `P_EXFAN_MIDDLE` | NUMBER | ⬜ | Exhaust fan middle |
| `P_ANGLEKNIFE` | NUMBER | ⬜ | Knife angle (degrees) |
| `P_BLADENO` | VARCHAR2(50) | ⬜ | Blade number |
| `P_BLADEDIRECTION` | VARCHAR2(50) | ⬜ | Blade direction |
| `P_TENSIONUP` | NUMBER | ⬜ | Tension up value |
| `P_TENSIONDOWN` | NUMBER | ⬜ | Tension down value |
| `P_FORN` | NUMBER | ⬜ | Frame width furnace (mm) |
| `P_TENTER` | NUMBER | ⬜ | Frame width tenter (mm) |
| `P_PATHLINE` | NUMBER | ⬜ | Path line setting |
| `P_FEEDIN` | NUMBER | ⬜ | Feed-in percentage |
| `P_OVERFEED` | NUMBER | ⬜ | Overfeed percentage |
| `P_WIDTHCOAT` | NUMBER | ⬜ | Coating width (mm) |
| `P_WIDTHCOATALL` | NUMBER | ⬜ | Total coating width (mm) |
| `P_SILICONEA` | VARCHAR2(50) | ⬜ | Silicone A type/batch |
| `P_SILICONEB` | VARCHAR2(50) | ⬜ | Silicone B type/batch |
| `P_CWL` | NUMBER | ⬜ | Coating weight Left (g/m²) |
| `P_CWC` | NUMBER | ⬜ | Coating weight Center (g/m²) |
| `P_CWR` | NUMBER | ⬜ | Coating weight Right (g/m²) |
| `P_HUMID_BF` | NUMBER | ⬜ | Humidity before (%) |
| `P_HUMID_AF` | NUMBER | ⬜ | Humidity after (%) |
| `P_REPROCESS` | VARCHAR2(10) | ⬜ | Reprocess flag (Y/N) |
| `P_WEAVLENGTH` | NUMBER | ⬜ | Weaving length (m) |
| `P_GROUP` | VARCHAR2(50) | ⬜ | Operator group/shift |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(100) | Success/failure message or generated finishing lot number |

### Returns (if cursor)

N/A - Returns single OUT parameter

---

## Business Logic (What it does and why)

**Purpose**: Creates new coating process record when operator starts coating operation. Saves all PLC parameters, machine settings, and process conditions for traceability.

**When Used**:
- Operator starts new coating lot on machine
- Operator clicks "Start" button on coating preparation page
- System validates item compatibility and creates new process record
- Generates unique finishing lot number

**Business Rules**:
1. **New Lot Creation**: Generates new FINISHINGLOT number (system-generated)
2. **Traceability**: Links weaving lot to finishing lot
3. **Parameter Capture**: Saves all PLC and machine settings at process start
4. **Status Tracking**: Sets initial status via P_FLAG (usually 'START')
5. **Operator Tracking**: Records who started the process
6. **Complete Data**: Captures all 40+ process parameters for quality documentation

**Workflow**:
1. Operator scans weaving lot barcode
2. System validates item code compatibility
3. Operator enters/confirms all process parameters
4. Operator clicks "Start Coating"
5. System calls INSERTCOATING with all parameters
6. Procedure generates new finishing lot number
7. Inserts record into database
8. Returns finishing lot number to UI
9. Process begins with status = 'START'

**Why This Matters**:
- Complete quality traceability required for automotive certification
- All process parameters must be recorded at start
- Enables process resume if interrupted
- Links upstream (weaving) to downstream (finished goods)
- Supports defect investigation and quality analysis

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item before insert)
- FINISHING_GETCOATINGCONDITION (retrieves standard conditions)

**Downstream**:
- FINISHING_UPDATECOATING (updates record during/after processing)
- FINISHING_COATINGDATABYLOT (retrieves created record)

**Similar**:
- FINISHING_INSERTSCOURING (same pattern for scouring)
- FINISHING_INSERTDRYER (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_INSERTCOATING(string weavlot, string itmCode, ...40+ parameters)`
**Lines**: 655-780

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_INSERTCOATING(FINISHING_INSERTCOATINGParameter para)`
**Lines**: 27XXX (estimated)

---

**File**: 93/296 | **Progress**: 31.4%
