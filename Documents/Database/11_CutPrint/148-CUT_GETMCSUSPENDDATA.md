# CUT_GETMCSUSPENDDATA

**Procedure Number**: 148 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve suspended cutting operations for specific machine to resume work |
| **Operation** | SELECT |
| **Tables** | tblCutPrint (WHERE suspended/not finished) |
| **Called From** | CutPrintDataService.cs:1050 → Cut_GetMCSuspendData() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_CUTMCNO` | VARCHAR2(50) | ✅ | Cutting machine number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns suspended cutting records with 40+ columns including all cutting parameters:
- ITEMLOT, dates, barcode widths/distances (1-4)
- Cut line distances (1-3), densities, speed
- Before/after widths, begin/end roll positions (1-4)
- Selvage measurements, tension, length details, etc.

---

## Business Logic (What it does and why)

**Purpose**: Retrieve paused cutting operations for specific machine so operator can resume work.

**When Used**:
- Machine menu display
- Operator selects machine
- System shows suspended operations
- Allows resuming interrupted cutting work

**Business Rules**:
1. P_CUTMCNO required
2. Returns only suspended/unfinished operations
3. Filtered by cutting machine number
4. Shows complete cutting parameters for resume
5. Similar pattern to INS_GETMCSUSPENDDATA (inspection module)

**Suspension Scenarios**:
- Machine malfunction/maintenance
- Shift change
- Material shortage
- Quality issue requiring review
- Emergency stop

**Resume Workflow**:
1. Operator opens cutting machine menu
2. System calls CUT_GETMCSUSPENDDATA
3. Displays list of suspended operations
4. Operator selects operation to resume
5. System loads all saved parameters
6. Cutting continues from saved state

---

## Related Procedures

**Similar**: [132-INS_GETMCSUSPENDDATA.md](../08_Inspection/132-INS_GETMCSUSPENDDATA.md) - Inspection module equivalent
**Related**: CUT_INSERTDATA - Records cutting with suspend capability
**Related**: CUT_UPDATEDATA - Updates to clear suspension

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `Cut_GetMCSuspendData(string CUTMCNO)`
**Lines**: 1050-1149

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_GETMCSUSPENDDATA(CUT_GETMCSUSPENDDATAParameter para)`

**Parameter Class**: 1 parameter
**Result Class**: 40+ columns (complete cutting record)

**Usage**: CutPrintMCMenu.xaml.cs - Machine menu for selecting suspended operations

---

**File**: 148/296 | **Progress**: 50.0%
