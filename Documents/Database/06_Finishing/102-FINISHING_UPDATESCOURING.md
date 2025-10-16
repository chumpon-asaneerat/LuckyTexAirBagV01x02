# FINISHING_UPDATESCOURING

**Procedure Number**: 102 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update scouring process record with new PLC values and process parameters |
| **Operation** | UPDATE |
| **Tables** | tblFinishingLot, tblFinishingScouring |
| **Called From** | CoatingDataService.cs:1949 → FINISHING_UPDATESCOURINGProcessing(), 2057 → FINISHING_UPDATESCOURINGFinishing() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number to update |
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
| `P_CONDITIONBY` | VARCHAR2(50) | ⚠️ | Operator who set conditions |
| `P_FINISHBY` | VARCHAR2(50) | ⚠️ | Operator who finished process |
| `P_ENDDATE` | DATE | ⚠️ | Process end timestamp |
| `P_CONDITONDATE` | DATE | ⚠️ | Condition set timestamp |
| `P_LENGTH1-7` | NUMBER | ⚠️ | Length measurements at various checkpoints (m) |
| `P_ITMCODE` | VARCHAR2(50) | ⚠️ | Item code |
| `P_WEAVINGLOT` | VARCHAR2(50) | ⚠️ | Weaving lot number |
| `P_CUSTOMER` | VARCHAR2(50) | ⚠️ | Customer code |
| `P_STARTDATE` | DATE | ⚠️ | Process start timestamp |
| `P_REMARK` | VARCHAR2(500) | ⚠️ | Process remarks/notes |
| `P_HUMID_BF` | NUMBER | ⚠️ | Humidity before (%) |
| `P_HUMID_AF` | NUMBER | ⚠️ | Humidity after (%) |
| `P_GROUP` | VARCHAR2(50) | ⚠️ | Operator group/shift |

### Output (OUT)

None - Returns result value

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `RESULT` | VARCHAR2(50) | Success/failure message |

---

## Business Logic (What it does and why)

**Purpose**: Updates existing scouring process record with new PLC values, machine settings, and process parameters. Used during processing and when finishing the lot.

**When Used**:
- **During Processing**: Operator updates actual PLC values while machine is running
- **On Completion**: Operator finalizes the lot with final measurements and timestamp
- **Manual Updates**: Quality engineer corrects data after process completion
- **PLC Integration**: Automated updates from PLC data collection system

**Business Rules**:
1. **Status Transition**: P_FLAG changes from 'START' → 'FINISH' when completing lot
2. **Partial Updates**: Most parameters optional - can update specific fields only
3. **Timestamp Control**: Different timestamps for condition setting vs. process completion
4. **Length Tracking**: Multiple length checkpoints (7 positions) for process monitoring
5. **Operator Tracking**: Records both condition setter and finisher

**Workflow Scenarios**:

**Scenario 1: During Processing (Mid-Process Update)**
1. Scouring machine SC1 running Finishing Lot "FS-2024-0125"
2. Operator checks actual PLC readings after 200m
3. Updates process values:
   - P_FINISHLOT = "FS-2024-0125"
   - P_FLAG = "START" (still in progress)
   - P_TEMP1_PV = 172°C (actual reading)
   - P_SPEED_PV = 25 m/min (actual speed)
   - P_LENGTH1 = 200m (current length)
4. System updates record
5. Process continues

**Scenario 2: Finishing Process (Completion Update)**
1. Operator completes scouring of lot "FS-2024-0125"
2. Final measurements taken:
   - Total length processed
   - Final width measurements (before/after)
   - Final humidity readings
3. Updates with completion data:
   - P_FLAG = "FINISH" (marks completed)
   - P_FINISHBY = "OP123" (operator ID)
   - P_ENDDATE = Current timestamp
   - P_LENGTH7 = 1850m (final length)
   - P_WIDTH_AF = 1845mm (final width)
4. System marks lot complete
5. Lot moves to next process (dryer or inspection)

**Scenario 3: Data Correction**
1. Quality engineer reviews completed lot
2. Finds incorrect temperature entry
3. Edits record:
   - P_FINISHLOT = "FS-2024-0125"
   - P_FLAG = "FINISH" (no status change)
   - P_TEMP5_SP = 175°C (corrected setpoint)
   - P_REMARK = "Corrected TEMP5 value per QA review"
4. System updates record
5. Audit trail maintained

**Two Main Methods in DataService**:
- **FINISHING_UPDATESCOURINGProcessing()**: Called during active processing (status remains START)
- **FINISHING_UPDATESCOURINGFinishing()**: Called when completing lot (status changes to FINISH)

**Why This Matters**:
- **Real-time Tracking**: Captures actual process performance vs. setpoints
- **Quality Documentation**: Complete process history for customer audits
- **Process Improvement**: Data used for optimization and troubleshooting
- **Operator Accountability**: Tracks who set conditions and who finished
- **Status Management**: Clear distinction between in-progress and completed lots

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTSCOURING (creates initial record to update)
- FINISHING_SCOURINGDATABYLOT (loads data for editing)

**Downstream**:
- FINISHING_GETSCOURINGREPORT (uses updated data for reports)
- FINISHING_GETSCOURINGDATA (lists updated lots)

**Similar**:
- FINISHING_UPDATECOATING (same pattern for coating process)
- FINISHING_UPDATEDRYER (same pattern for dryer process)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method 1**: `FINISHING_UPDATESCOURINGProcessing(string FINISHLOT, string operatorid, ... 20+ parameters)`
**Lines**: 1949-2051

**Method 2**: `FINISHING_UPDATESCOURINGFinishing(string FINISHLOT, string operatorid, ... 20+ parameters)`
**Lines**: 2057-2163

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_UPDATESCOURING(FINISHING_UPDATESCOURINGParameter para)`
**Lines**: 7047-7113 (Parameter: 7047-7104, Result: 7110-7113)

---

**File**: 102/296 | **Progress**: 34.5%
