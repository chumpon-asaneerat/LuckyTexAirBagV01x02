# FINISHING_INSERTSCOURING

**Procedure Number**: 099 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new scouring process record with initial PLC parameters |
| **Operation** | INSERT |
| **Tables** | tblFinishingLot, tblFinishingScouring |
| **Called From** | CoatingDataService.cs:1840 → FINISHING_INSERTSCOURING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2(50) | ✅ | Weaving lot number (greige fabric lot) |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Finished goods item code |
| `P_FINISHCUSTOMER` | VARCHAR2(50) | ✅ | Customer code |
| `P_PRODUCTTYPEID` | VARCHAR2(50) | ✅ | Product type identifier |
| `P_OPERATORID` | VARCHAR2(50) | ✅ | Operator ID who started process |
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number (scouring machine ID) |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag (START/FINISH) |
| `P_SATURATOR_PV` | NUMBER | ⚠️ | Chemical saturator process value (°C) |
| `P_SATURATOR_SP` | NUMBER | ⚠️ | Chemical saturator setpoint (°C) |
| `P_WASHING1_PV` | NUMBER | ⚠️ | Washing zone 1 process value (°C) |
| `P_WASHING1_SP` | NUMBER | ⚠️ | Washing zone 1 setpoint (°C) |
| `P_WASHING2_PV` | NUMBER | ⚠️ | Washing zone 2 process value (°C) |
| `P_WASHING2_SP` | NUMBER | ⚠️ | Washing zone 2 setpoint (°C) |
| `P_HOTFLUE_PV` | NUMBER | ⚠️ | Hot flue temperature process value (°C) |
| `P_HOTFLUE_SP` | NUMBER | ⚠️ | Hot flue temperature setpoint (°C) |
| `P_TEMP1_PV` | NUMBER | ⚠️ | Temperature zone 1 process value (°C) |
| `P_TEMP1_SP` | NUMBER | ⚠️ | Temperature zone 1 setpoint (°C) |
| `P_TEMP2_PV` | NUMBER | ⚠️ | Temperature zone 2 process value (°C) |
| `P_TEMP2_SP` | NUMBER | ⚠️ | Temperature zone 2 setpoint (°C) |
| `P_TEMP3_PV` | NUMBER | ⚠️ | Temperature zone 3 process value (°C) |
| `P_TEMP3_SP` | NUMBER | ⚠️ | Temperature zone 3 setpoint (°C) |
| `P_TEMP4_PV` | NUMBER | ⚠️ | Temperature zone 4 process value (°C) |
| `P_TEMP4_SP` | NUMBER | ⚠️ | Temperature zone 4 setpoint (°C) |
| `P_TEMP5_PV` | NUMBER | ⚠️ | Temperature zone 5 process value (°C) |
| `P_TEMP5_SP` | NUMBER | ⚠️ | Temperature zone 5 setpoint (°C) |
| `P_TEMP6_PV` | NUMBER | ⚠️ | Temperature zone 6 process value (°C) |
| `P_TEMP6_SP` | NUMBER | ⚠️ | Temperature zone 6 setpoint (°C) |
| `P_TEMP7_PV` | NUMBER | ⚠️ | Temperature zone 7 process value (°C) |
| `P_TEMP7_SP` | NUMBER | ⚠️ | Temperature zone 7 setpoint (°C) |
| `P_TEMP8_PV` | NUMBER | ⚠️ | Temperature zone 8 process value (°C) |
| `P_TEMP8_SP` | NUMBER | ⚠️ | Temperature zone 8 setpoint (°C) |
| `P_TEMP9_PV` | NUMBER | ⚠️ | Temperature zone 9 process value (°C) |
| `P_TEMP9_SP` | NUMBER | ⚠️ | Temperature zone 9 setpoint (°C) |
| `P_TEMP10_PV` | NUMBER | ⚠️ | Temperature zone 10 process value (°C) |
| `P_TEMP10_SP` | NUMBER | ⚠️ | Temperature zone 10 setpoint (°C) |
| `P_SPEED_PV` | NUMBER | ⚠️ | Machine speed process value (m/min) |
| `P_SPEED_SP` | NUMBER | ⚠️ | Machine speed setpoint (m/min) |
| `P_MAINFRAMEWIDTH` | NUMBER | ⚠️ | Main frame width (mm) |
| `P_WIDTH_BE` | NUMBER | ⚠️ | Width before scouring (mm) |
| `P_WIDTH_AF` | NUMBER | ⚠️ | Width after scouring (mm) |
| `P_PIN2PIN` | NUMBER | ⚠️ | Pin to pin distance (mm) |
| `P_HUMID_BF` | NUMBER | ⚠️ | Humidity before (%) |
| `P_HUMID_AF` | NUMBER | ⚠️ | Humidity after (%) |
| `P_REPROCESS` | VARCHAR2(10) | ⚠️ | Reprocess flag (Y/N) |
| `P_WEAVLENGTH` | NUMBER | ⚠️ | Original weaving length (m) |
| `P_GROUP` | VARCHAR2(50) | ⚠️ | Operator group/shift |

### Output (OUT)

None - Returns result value

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `RESULT` | VARCHAR2(50) | Generated finishing lot number or error message |

---

## Business Logic (What it does and why)

**Purpose**: Creates initial scouring process record when operator starts new lot on scouring machine. Generates finishing lot number and captures initial PLC parameters.

**When Used**:
- Operator scans weaving lot barcode on scouring machine
- Selects item code and customer
- Sets up initial machine parameters from PLC
- Clicks "Start Process" button
- System generates new finishing lot number and saves record

**Business Rules**:
1. **Lot Number Generation**: Auto-generates finishing lot number (format likely: FS-YYYYMMDD-NNN)
2. **Initial Status**: P_FLAG typically 'START' for new process
3. **Optional Parameters**: Most PLC values optional - can start with minimal data and update later
4. **Traceability Link**: Creates link between weaving lot (greige) and finishing lot (scoured)
5. **Timestamp Capture**: Records start date/time automatically

**Workflow**:
1. **Operator Setup**:
   - Arrives at scouring machine (e.g., SC1)
   - Scans weaving lot barcode "WV-2024-001"
   - System validates lot exists and hasn't been processed
   - Selects finished item code and customer

2. **Initial Parameters** (from PLC or manual entry):
   - Temperature zones 1-10 setpoints
   - Saturator, washing, hot flue temperatures
   - Machine speed
   - Frame width and fabric widths
   - Pin-to-pin distance
   - Humidity readings

3. **Process Start**:
   - Operator clicks "Start Scouring"
   - System calls INSERT procedure
   - Generates finishing lot: "FS-2024-0125"
   - Creates record in tblFinishingScouring
   - Links to tblFinishingLot for traceability
   - Status = START

4. **Return Value**:
   - Success: Returns generated finishing lot number
   - Failure: Returns error message (e.g., "Lot already exists")

**Example Scenario**:
```
Input:
- P_WEAVLOT = "WV-2024-001"
- P_ITMCODE = "ITM-COATED-FABRIC-001"
- P_FINISHCUSTOMER = "CUSTOMER-A"
- P_MCNO = "SC1"
- P_OPERATORID = "OP123"
- P_FLAG = "START"
- P_TEMP1_SP = 170, P_TEMP2_SP = 175, ... (initial setpoints)
- P_WIDTH_BE = 1850 mm

Output:
- RESULT = "FS-2024-0125" (new finishing lot number)

Operator sees: "Scouring lot FS-2024-0125 created successfully"
Process continues → Machine runs → Operator updates with actual values later
```

**Why This Matters**:
- **Production Start Tracking**: Captures exact moment scouring begins
- **Lot Number Management**: System-generated lot numbers prevent duplicates
- **PLC Data Capture**: Preserves initial setpoints for quality documentation
- **Shift Continuity**: Creates record for later continuation/editing
- **Traceability Foundation**: Links raw fabric to finished product

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item compatibility first)
- FINISHING_GETSCOURINGCONDITION (loads standard conditions)

**Downstream**:
- FINISHING_UPDATESCOURING (updates process values during/after run)
- FINISHING_SCOURINGDATABYLOT (retrieves data for editing)
- FINISHING_GETSCOURINGDATA (lists lots for selection)

**Similar**:
- FINISHING_INSERTCOATING (same pattern for coating process)
- FINISHING_INSERTDRYER (same pattern for dryer process)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_INSERTSCOURING(string weavlot, string itmCode, ... 20+ parameters)`
**Lines**: 1840-1945

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_INSERTSCOURING(FINISHING_INSERTSCOURINGParameter para)`
**Lines**: 7699-7756 (Parameter: 7699-7747, Result: 7753-7756)

---

**File**: 99/296 | **Progress**: 33.4%
