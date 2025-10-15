# INST_INSPECTIONGRADEHISTORY

**Procedure Number**: 143 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Record grade change history with authentication for audit trail |
| **Operation** | INSERT |
| **Tables** | tblINSGradeHistory (grade change audit table) |
| **Called From** | DataServicecs.cs:2772 → InsertInspectionGradeHistory() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_OLDGRADE` | VARCHAR2(10) | ✅ | Original grade before change |
| `P_NEWGRADE` | VARCHAR2(10) | ✅ | New grade after change |
| `P_REMARK` | VARCHAR2(200) | ⬜ | Reason for grade change |
| `P_USER` | VARCHAR2(50) | ✅ | Username authorizing change |
| `P_PASS` | VARCHAR2(50) | ✅ | Password for authentication |
| `P_STARTDATE` | DATE | ⬜ | Timestamp of change |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2(10) | Result: "Y" = success, other = failure |

### Returns

Returns "Y" if authentication successful and grade history inserted, otherwise failure indicator.

---

## Business Logic (What it does and why)

**Purpose**: Record grade change history with user authentication for quality audit trail.

**When Used**:
- Supervisor/manager changes inspection grade
- Grade correction after review
- Quality override scenarios
- Requires authentication (username + password)

**Business Rules**:
1. All parameters required except P_REMARK
2. **Authentication required**: P_USER and P_PASS validated
3. Returns "Y" only if authentication successful
4. Records: old grade, new grade, who changed, when, why
5. Creates permanent audit trail for compliance
6. Cannot be deleted (audit requirement)

**Grade Change Workflow**:
1. Inspector assigns initial grade (e.g., "B")
2. Supervisor reviews and disagrees
3. Supervisor initiates grade change
4. System prompts for username/password
5. Supervisor enters credentials + reason for change
6. System validates credentials
7. If valid: calls INST_INSPECTIONGRADEHISTORY
8. Grade changed in inspection lot
9. History record created with old/new grades
10. Audit trail maintained for compliance

**Why Authentication Required**:
- Grade changes affect product value
- Customer billing may depend on grade
- Quality compliance requirements
- Prevent unauthorized grade manipulation
- Audit trail for quality reviews

**Audit Trail Fields**:
- INSLOT: Which lot was changed
- OLDGRADE: Original grade
- NEWGRADE: Corrected grade
- REMARK: Justification
- USER: Who authorized change
- STARTDATE: When changed

---

## Related Procedures

**Related**: UPDATEINSPECTIONPROCESS - Updates grade in inspection lot
**Related**: GETGRADE - Calculate grade based on defects
**Used for**: Quality audit compliance

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `InsertInspectionGradeHistory(string insLot, string oldGrade, string newGrade, string remark, string userName, string password)`
**Lines**: 2772-2826

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INST_INSPECTIONGRADEHISTORY(INST_INSPECTIONGRADEHISTORYParameter para)`

**Parameter Class**: 7 parameters (including authentication)
**Result Class**: Returns R_RESULT ("Y" or error)

**Context**: Grade change requires supervisor authentication and creates permanent audit record

---

**File**: 143/296 | **Progress**: 48.3%
