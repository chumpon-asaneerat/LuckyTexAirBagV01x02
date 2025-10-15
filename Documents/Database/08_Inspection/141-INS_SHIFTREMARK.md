# INS_SHIFTREMARK

**Procedure Number**: 141 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Add or update shift remarks for inspection lot |
| **Operation** | UPDATE |
| **Tables** | tblINSLot (SHIFT_ID, SHIFT_REMARK, SHIFT_REMARK_DATE) |
| **Called From** | DataServicecs.cs:3038 → INS_SHIFTREMARK() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_STARTDATE` | DATE | ⬜ | Inspection start date |
| `P_SHIFTID` | VARCHAR2(50) | ⬜ | Shift identifier |
| `P_SHIFTREMARK` | VARCHAR2(500) | ⬜ | Shift remark text |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2(50) | Result status/message |

### Returns

Returns result string (success/failure indicator).

---

## Business Logic (What it does and why)

**Purpose**: Record shift-specific remarks and notes for inspection lot.

**When Used**:
- During or after inspection to document shift-specific issues
- Recording special circumstances or observations
- Shift handover notes
- Quality issue documentation

**Business Rules**:
1. P_INSLOT is required
2. Updates shift information fields:
   - SHIFT_ID: Current shift identifier
   - SHIFT_REMARK: Remark text
   - SHIFT_REMARK_DATE: Timestamp (set to current date/time)
3. Used in ShiftRemarkWindow for adding remarks
4. Remarks can be viewed in inspection reports

**Shift Remark Usage**:
- **Quality Issues**: Document problems found during shift
- **Machine Issues**: Note machine-related problems affecting inspection
- **Special Instructions**: Handover notes to next shift
- **Customer Requirements**: Document specific customer requests
- **Process Deviations**: Note any process changes or exceptions

**Workflow**:
1. Operator encounters situation requiring documentation
2. Opens shift remark window
3. Enters remark text with shift ID
4. System calls INS_SHIFTREMARK
5. Remark saved with timestamp
6. Remark appears in inspection reports (if UseShiftRemark = true)

**Report Integration**:
- RepInspectionRemark() - Shows remarks on inspection report
- RepTestInspectionRemark() - Shows remarks on test report
- Shift remarks can be enabled/disabled per report

---

## Related Procedures

**Related**: [131-INS_GETINSPECTIONREPORTDATA.md](./131-INS_GETINSPECTIONREPORTDATA.md) - Returns shift remark data
**Related**: UPDATEINSPECTIONPROCESS - Updates inspection lot
**Used in**: ShiftRemarkWindow, inspection report generation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `INS_SHIFTREMARK(string P_INSLOT, DateTime? P_STARTDATE, string P_SHIFTID, string P_SHIFTREMARK)`
**Lines**: 3038-3079

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_SHIFTREMARK(INS_SHIFTREMARKParameter para)`

**Parameter Class**: 4 parameters (P_INSLOT, P_STARTDATE, P_SHIFTID, P_SHIFTREMARK)
**Result Class**: Returns R_RESULT

**Usage Locations**:
1. `LuckyTex.AirBag.Pages\Windows\08 - Inspection\ShiftRemarkWindow.xaml.cs`
   - Shift remark entry window

2. `LuckyTex.AirBag.Pages\ClassData\Print\Report.xaml.cs`
   - RepInspectionRemark() - Line ~1107
   - RepTestInspectionRemark() - Line ~1407
   - Context: Display remarks in printed reports

---

**File**: 141/296 | **Progress**: 47.6%
