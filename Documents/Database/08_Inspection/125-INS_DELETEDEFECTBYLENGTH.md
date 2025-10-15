# INS_DELETEDEFECTBYLENGTH

**Procedure Number**: 125 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete defect record by length during inspection lot processing |
| **Operation** | DELETE |
| **Tables** | tblINSDefect (assumed) |
| **Called From** | DataServicecs.cs:1554 → INS_DELETEDEFECTBYLENGTH() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2(50) | ✅ | Defect record unique identifier |
| `P_LENGTH` | NUMBER | ⬜ | Length position where defect occurred |
| `P_DELETEBY` | VARCHAR2(50) | ⬜ | Operator ID who deleted the defect |

### Output (OUT)

None (returns empty result object indicating success/failure)

### Returns

Empty result object. Success determined by non-null return value in C# code.

---

## Business Logic (What it does and why)

**Purpose**: Delete a defect record associated with a specific length position during inspection processing.

**When Used**: During inspection lot processing when operator needs to remove a defect entry. This is typically used in the Process Control page when:
- Operator is editing/finalizing inspection data
- Need to remove incorrectly recorded defects before cutting sample
- Cleaning up defect records at specific length positions

**Business Rules**:
1. Requires valid DEFECTID (cannot be null/empty)
2. Length parameter identifies specific position of defect to delete
3. Records who deleted the defect for audit purposes
4. Different from INS_DELETEDEFECT which requires defect code and additional parameters

**Workflow**:
1. Operator reviews inspection data with defects
2. Identifies defect record to remove at specific length
3. System deletes defect using DEFECTID and length position
4. Audit trail maintained with DELETEBY operator ID
5. Proceeds with cutting sample and finalizing inspection

**Relationship to Process**:
- Part of inspection finalization workflow
- Works with INS_CUTSAMPLE and EditInspectionProcess operations
- Precedes sample cutting operation (removes incorrect defects first)

---

## Related Procedures

**Similar**: [124-INS_DELETEDEFECT.md](./124-INS_DELETEDEFECT.md) - Delete defect with defect code and remark (more detailed deletion)
**Downstream**: INS_CUTSAMPLE - Called after defect cleanup (not documented yet)
**Related**: UPDATEINSPECTIONPROCESS - Updates inspection lot after defect modification (shared procedure)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `INS_DELETEDEFECTBYLENGTH(string DEFECTID, decimal? LENGTH, string DELETEBY)`
**Lines**: 1554-1585

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_DELETEDEFECTBYLENGTH(INS_DELETEDEFECTBYLENGTHParameter para)`
**Lines**: 23296-23324

**Parameter Class**: `INS_DELETEDEFECTBYLENGTHParameter`
**Lines**: 5855-5860

**Result Class**: `INS_DELETEDEFECTBYLENGTHResult`
**Lines**: 5866-5868

**Usage Location**: `LuckyTex.AirBag.Pages\Pages\10 - ProcessControl\ProcessControlPage.xaml.cs`
**Line**: 1064
**Context**: Called before INS_CUTSAMPLE during inspection lot finalization

---

**File**: 125/296 | **Progress**: 42.2%
