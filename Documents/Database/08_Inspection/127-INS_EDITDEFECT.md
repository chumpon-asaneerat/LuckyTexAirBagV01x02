# INS_EDITDEFECT

**Procedure Number**: 127 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit/update defect record with old and new defect information |
| **Operation** | UPDATE |
| **Tables** | tblINSDefect (assumed) |
| **Called From** | DataServicecs.cs:2092 → EditInspectionLotDefect() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2(50) | ✅ | Defect record unique identifier |
| `P_INSLOT` | VARCHAR2(50) | ⬜ | Inspection lot number |
| `P_DEFECTCODE` | VARCHAR2(50) | ⬜ | Original defect code |
| `P_LENGTH1` | NUMBER | ⬜ | Original start length position of defect |
| `P_LENGTH2` | NUMBER | ⬜ | Original end length position of defect |
| `P_POSITION` | NUMBER | ⬜ | Original position across width (1-9, left to right) |
| `P_NDEFECTCODE` | VARCHAR2(50) | ⬜ | **New** defect code (corrected) |
| `P_NLENGTH1` | NUMBER | ⬜ | **New** start length position |
| `P_NLENGTH2` | NUMBER | ⬜ | **New** end length position |
| `P_NPOSITION` | NUMBER | ⬜ | **New** position across width |

### Output (OUT)

None (returns empty result object indicating success/failure)

### Returns

Empty result object. Success determined by non-null return value in C# code.

---

## Business Logic (What it does and why)

**Purpose**: Update a defect record by providing both old (original) and new (corrected) defect information.

**When Used**: During inspection when operator needs to correct a previously recorded defect. This allows changing:
- Defect type/code (misidentified defect type)
- Length positions (incorrect length measurement)
- Width position (wrong position recorded)

**Business Rules**:
1. Requires valid DEFECTID (cannot be null/empty)
2. Maintains audit trail with both old and new values (P_* vs P_N* parameters)
3. Allows updating defect code, length positions (start/end), and width position
4. Used in DefectListWindow when operator reviews and corrects defect records
5. Both old and new values passed to stored procedure (likely for validation or logging)

**Defect Position System**:
- **LENGTH1/LENGTH2**: Start and end position along fabric length (in meters)
- **POSITION**: Width position numbered 1-9 from left edge to right edge
  - Position 1: Left edge
  - Position 5: Center
  - Position 9: Right edge
  - Helps identify if defects cluster in specific fabric areas

**Workflow**:
1. Operator opens defect list for inspection lot
2. Selects defect record needing correction
3. Opens edit dialog with current defect details
4. Modifies defect code, length positions, or width position
5. System updates defect with new values (old values preserved for audit)
6. Defect list grid refreshes to show updated data
7. Ensures accurate defect tracking for quality reporting

**Why Old and New Values**:
- Audit trail: track what was changed and why
- Validation: stored procedure can verify changes are reasonable
- History: may log changes to defect modification history table
- Quality improvement: analyze common correction patterns to improve operator training

---

## Related Procedures

**Similar**: [124-INS_DELETEDEFECT.md](./124-INS_DELETEDEFECT.md) - Delete defect record
**Similar**: [125-INS_DELETEDEFECTBYLENGTH.md](./125-INS_DELETEDEFECTBYLENGTH.md) - Delete defect by length position
**Related**: GETDEFECTCODEDETAIL - Get defect code master data (not documented yet)
**Downstream**: GetDefectList - Retrieve updated defect list after edit (not documented yet)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `EditInspectionLotDefect(string defectID, string insLot, string defectCode, decimal? len1, decimal? len2, decimal? position, string ndefectCode, decimal? nlen, decimal? nlen2, decimal? nposition)`
**Lines**: 2092-2134

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_EDITDEFECT(INS_EDITDEFECTParameter para)`
**Lines**: 23178-23220

**Parameter Class**: `INS_EDITDEFECTParameter`
**Lines**: 5792-5804 (10 parameters: old and new values)

**Result Class**: `INS_EDITDEFECTResult`
**Lines**: 5810-5812

**Usage Location**: `LuckyTex.AirBag.Controls\Windows\DefectListWindow.xaml.cs`
**Line**: 700-701
**Context**: EditDefect method called when operator modifies defect record in defect list window

---

**File**: 127/296 | **Progress**: 42.9%
