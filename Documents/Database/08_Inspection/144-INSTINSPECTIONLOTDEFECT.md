# INSTINSPECTIONLOTDEFECT

**Procedure Number**: 144 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert defect record during real-time inspection |
| **Operation** | INSERT |
| **Tables** | tblINSDefect |
| **Called From** | DataServicecs.cs:1669 → AddInspectionLotDefect() |
| **Frequency** | Very High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTCODE` | VARCHAR2(50) | ✅ | Defect type code (2+ char) |
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_LENGTH1` | NUMBER | ⬜ | Start length position (meters) |
| `P_LENGTH2` | NUMBER | ⬜ | End length position (for long defects) |
| `P_POINT` | NUMBER | ⬜ | Defect point value |
| `P_POSITION` | NUMBER | ⬜ | Width position (1-9, left to right) |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_DEFECTID` | VARCHAR2(50) | Generated defect ID (primary key) |

### Returns

Returns defect ID string if successful, empty string on failure.

---

## Business Logic (What it does and why)

**Purpose**: Record defect during real-time inspection when operator identifies quality issue.

**When Used**: **Very frequently** - Every time operator marks a defect during inspection:
- Operator inspecting fabric in real-time
- Sees defect (hole, stain, tear, etc.)
- Enters defect code + position
- System calls INSTINSPECTIONLOTDEFECT
- Defect recorded immediately

**Business Rules**:
1. P_DEFECTCODE and P_INSLOT required
2. If P_LENGTH2 is null: **Point defect** (single location)
   - Requires P_DEFECTCODE >= 2 characters
3. If P_LENGTH2 provided: **Long defect** (continuous defect)
   - P_LENGTH1 = start position
   - P_LENGTH2 = end position
4. Returns R_DEFECTID for reference
5. P_POSITION indicates width location (1=left edge, 9=right edge)

**Defect Types**:
- **Point Defect**: Single location (hole, stain, etc.)
  - LENGTH1 = defect position
  - LENGTH2 = null
- **Long Defect**: Continuous defect (tear, streak, etc.)
  - LENGTH1 = start position
  - LENGTH2 = end position
  - Defect length = LENGTH2 - LENGTH1

**Real-Time Workflow**:
1. Fabric running through inspection machine
2. Operator sees defect at counter position (e.g., 125.5m)
3. Operator enters defect code (e.g., "HL" for hole)
4. Operator marks width position (e.g., position 3)
5. System calls INSTINSPECTIONLOTDEFECT:
   - P_DEFECTCODE = "HL"
   - P_INSLOT = current lot
   - P_LENGTH1 = 125.5
   - P_POSITION = 3
6. Returns defect ID
7. System checks 100M defect points (INS_GET100MDEFECTPOINT)
8. Alert if > 10 points in 100M section

**For Long Defects**:
1. Operator sees continuous defect starting
2. Marks start position (e.g., 200.0m)
3. Fabric continues, defect continues
4. Operator marks end position (e.g., 202.5m)
5. System records LENGTH1=200.0, LENGTH2=202.5
6. Defect length = 2.5m

---

## Related Procedures

**Related**: [128-INS_GET100MDEFECTPOINT.md](./128-INS_GET100MDEFECTPOINT.md) - Check defect points after insert
**Related**: [127-INS_EDITDEFECT.md](./127-INS_EDITDEFECT.md) - Edit defect record
**Related**: [124-INS_DELETEDEFECT.md](./124-INS_DELETEDEFECT.md) - Delete defect
**Downstream**: Used in quality grading and net length calculation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `AddInspectionLotDefect(string code, decimal? point, decimal? position, decimal? len1, decimal? len2, string insLot)`
**Lines**: 1669-1711
**Comment**: Validates code length if not long defect mode

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INSTINSPECTIONLOTDEFECT(INSTINSPECTIONLOTDEFECTParameter para)`

**Parameter Class**: 6 parameters
**Result Class**: Returns R_DEFECTID

**Usage**: Called during InspectionModulePage real-time defect entry

---

**File**: 144/296 | **Progress**: 48.6%
