# INS_GETMCSUSPENDDATA

**Procedure Number**: 132 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve suspended inspection lots for specific machine to resume inspection |
| **Operation** | SELECT |
| **Tables** | tblINSLot (WHERE SUSPENDDATE IS NOT NULL) |
| **Called From** | DataServicecs.cs:1411 → GetSuspendInspectionProcess() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSMC` | VARCHAR2(50) | ✅ | Inspection machine number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns list of suspended inspection lots (30+ columns) including all inspection lot fields filtered by machine and suspension status.

---

## Business Logic (What it does and why)

**Purpose**: Retrieve all suspended (paused) inspection lots for a specific inspection machine so operator can resume inspection work.

**When Used**:
- Machine menu pages (InspectionMCMenu, FinishingMCMenu, CutPrintMCMenu)
- When operator selects machine and system needs to show paused lots
- Allows resuming interrupted inspection work

**Business Rules**:
1. Requires valid P_INSMC (inspection machine number)
2. Returns only suspended lots (SUSPENDDATE IS NOT NULL, FINISHFLAG != complete)
3. Filtered by machine number
4. Used across multiple modules: Inspection (M08), Finishing (M06), Cut & Print (M11)
5. Shows who suspended and when (SUSPENDBY, SUSPENDDATE)

**Suspension Workflow**:
1. Operator working on inspection lot
2. Needs to pause (machine issue, shift change, emergency)
3. System calls SuspendInspectionProcess → sets SUSPENDDATE, SUSPENDBY
4. Later, operator opens machine menu
5. System calls INS_GETMCSUSPENDDATA to show paused lots
6. Operator selects lot to resume
7. System clears suspension and continues inspection

---

## Related Procedures

**Related**: UPDATEINSPECTIONPROCESS - Updates inspection lot with suspension data
**Opposite**: Resume inspection (clears SUSPENDDATE)
**Used in**: Multiple modules (M06, M08, M11)

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `GetSuspendInspectionProcess(string machineId)`
**Lines**: 1411-1431

**Also used in**:
- `FinishingDataService.cs`
- `CutPrintDataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETMCSUSPENDDATA(INS_GETMCSUSPENDDATAParameter para)`
**Lines**: 22765-22845

**Parameter Class**: Lines 5541-5544
**Result Class**: Lines 5550-5593

**Usage Locations**:
- `InspectionMCMenu.xaml.cs`
- `FinishingMCMenu.xaml.cs`
- `CutPrintMCMenu.xaml.cs`

---

**File**: 132/296 | **Progress**: 44.6%
