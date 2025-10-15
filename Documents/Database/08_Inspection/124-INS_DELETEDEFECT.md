# INS_DELETEDEFECT

**Procedure Number**: 124 | **Module**: M08 - Inspection | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete defect record from inspection lot with audit trail |
| **Operation** | DELETE (with audit logging) |
| **Tables** | tblInspectionDefect (inferred) |
| **Called From** | DataServicecs.cs:2055 → DeleteInspectionLotDefect() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2 | ✅ | Defect record ID to delete |
| `P_DEFECTCODE` | VARCHAR2 | ⬜ | Defect type code (for reference) |
| `P_LENGTH1` | NUMBER | ⬜ | Defect position/length (meters) |
| `P_DELETEBY` | VARCHAR2 | ⬜ | Operator who authorized deletion |
| `P_DELETEREMARK` | VARCHAR2 | ⬜ | Reason for deletion |

### Output (OUT)

None (returns empty result object for success/failure indication)

---

## Business Logic

**Purpose**: Remove a defect record from an inspection lot when the defect marking is determined to be incorrect, invalid, or needs correction. Maintains full audit trail for quality control and regulatory compliance.

**Defect Record Context**:
During fabric inspection:
- Inspectors mark defects as fabric passes through inspection machine
- Each defect recorded with: type, position, severity, size
- Defects affect quality grading and customer acceptance
- Incorrect defect records can cause unfair rejection or customer complaints

**When Used**:

**Scenario 1: False Defect Identification**
1. Inspector marks defect at 50.5 meters during inspection
2. Records defect: CODE="HOLE", LENGTH1=50.5m
3. Supervisor reviews, determines it's not a defect but normal fabric characteristic
4. Authorizes deletion:
   - `P_DEFECTID` = "DEF-2025-001-023"
   - `P_DEFECTCODE` = "HOLE"
   - `P_LENGTH1` = 50.5
   - `P_DELETEBY` = "SUPERVISOR-03"
   - `P_DELETEREMARK` = "Not a defect - normal weave pattern"
5. Defect removed, inspection grade improves
6. Fabric section no longer marked as defective

**Scenario 2: Duplicate Defect Entry**
1. Inspector marks same defect twice accidentally
2. System creates two records for same physical defect:
   - Record 1: DEF-2025-001-045 at 75.2m
   - Record 2: DEF-2025-001-046 at 75.2m (duplicate)
3. Supervisor identifies duplicate during review
4. Deletes duplicate:
   - `P_DELETEREMARK` = "Duplicate entry - same defect recorded twice"
5. Keeps original, removes duplicate
6. Prevents double-counting in defect statistics

**Scenario 3: Wrong Defect Code**
1. Inspector marks defect as "YARN_BREAK" (minor)
2. Should have been marked as "OIL_STAIN" (different severity)
3. Supervisor decides to delete and re-enter correctly:
   - `P_DEFECTCODE` = "YARN_BREAK"
   - `P_DELETEREMARK` = "Wrong defect code - should be OIL_STAIN, will re-enter"
4. Record deleted
5. New defect record created with correct code
6. Proper grading and customer notification

**Scenario 4: Measurement Error**
1. Defect recorded at 125.8 meters
2. Inspector realizes position was misread from machine counter
3. Actual position: 152.8 meters (30cm off)
4. Supervisor authorizes deletion:
   - `P_LENGTH1` = 125.8
   - `P_DELETEREMARK` = "Incorrect position recorded - will re-enter at correct location"
5. Correct defect record created with accurate position
6. Important for customer repair/cutting instructions

**Scenario 5: Customer Challenge Resolution**
1. Customer disputes defect marking from inspection
2. Customer claims marked area is acceptable quality
3. Joint review with customer representative
4. Agreement reached: Defect marking invalid
5. Delete defect with customer approval:
   - `P_DELETEBY` = "QC-MANAGER-01"
   - `P_DELETEREMARK` = "Customer challenge - defect not per customer standard, approved by [Customer Name] QC rep"
6. Lot grade recalculated without disputed defect
7. Customer accepts lot

**Scenario 6: Machine Malfunction False Positives**
1. Automatic defect detection system malfunctions
2. Marks 50 false defects during 10-minute period
3. Supervisor identifies system error
4. Batch deletion of false positives with same reason:
   - `P_DELETEREMARK` = "Auto-detection system malfunction 2025-01-15 14:00-14:10 - all false positives"
5. Prevents unfair lot rejection

**Business Rules**:
- **Defect ID required**: Must specify which defect to remove
- **Authorization mandatory**: DELETEBY tracks accountability
- **Reason required**: DELETEREMARK explains deletion
- **Audit trail**: Maintains complete history of deletions
- **Quality impact**: Deletion may change inspection grade
- **Supervisor approval**: Should require supervisor authorization
- **Position tracking**: LENGTH1 helps identify defect location
- **Irreversible**: Deleted defects cannot be recovered

**When to Use DELETE vs EDIT**:
- **Use DELETE**:
  - False defect (not really a defect)
  - Duplicate entry
  - Wrong defect entirely
  - System malfunction false positive
- **Use EDIT** (INS_EDITDEFECT):
  - Correct defect, wrong code
  - Correct defect, wrong position
  - Correct defect, wrong severity
  - Minor corrections

**Impact on Quality Grading**:
Deleting defects affects:
- **Grade calculation**: Fewer defects = better grade
- **Defect density**: Defects per meter calculation
- **Customer acceptance**: Grade determines acceptance/rejection
- **Pricing**: Grade affects selling price
- **Statistics**: Quality metrics and trends

**Audit Trail Importance**:
- **Customer disputes**: Explain defect history
- **Quality audits**: Document deletion reasons
- **Regulatory compliance**: Complete traceability required
- **Process improvement**: Analyze deletion patterns
- **Training needs**: Identify common marking errors
- **Dispute resolution**: Evidence for customer complaints

---

## Related Procedures

**Alternative Operations**:
- [125-INS_EDITDEFECT.md](./125-INS_EDITDEFECT.md) - Edit defect (vs delete)
- [126-INS_DELETEDEFECTBYLENGTH.md](./126-INS_DELETEDEFECTBYLENGTH.md) - Delete by position

**Defect Management Chain**:
1. INSTINSPECTIONLOTDEFECT - Insert defect record
2. **INS_DELETEDEFECT** - Delete defect (this procedure)
3. INS_EDITDEFECT - Edit defect details
4. INS_GETDEFECTLISTREPORT - Retrieve defects for reporting

**Quality Impact**:
- INS_GETTOTALDEFECTBYINSLOT - Recalculates defect totals after deletion
- INS_REPORTSUMDEFECT - Updated defect summary
- INST_INSPECTIONGRADEHISTORY - Grade may change after deletion

**Similar Deletion Procedures**:
- [123-INS_DELETE100MRECORD.md](./123-INS_DELETE100MRECORD.md) - Delete 100M test record
- [115-FINISHING_CANCEL.md](../06_Finishing/115-FINISHING_CANCEL.md) - Cancel finishing operation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `DeleteInspectionLotDefect(string defectID, string defectCode, decimal? len1, string operatorID, string deleteremark)`
**Lines**: 2055-2089

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `INS_DELETEDEFECTParameter`
**Lines**: 5874-5881

**Result Class**: `INS_DELETEDEFECTResult`
**Lines**: 5887-5889 (empty)

**Method**: `INS_DELETEDEFECT(INS_DELETEDEFECTParameter para)`
**Lines**: 23330-23362 (estimated)

---

**File**: 124/296 | **Progress**: 41.9%
