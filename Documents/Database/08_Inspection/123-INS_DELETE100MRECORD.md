# INS_DELETE100MRECORD

**Procedure Number**: 123 | **Module**: M08 - Inspection | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete 100-meter test record (measurement removal with audit trail) |
| **Operation** | DELETE (with audit logging) |
| **Tables** | tblInspection100MTest (inferred) |
| **Called From** | DataServicecs.cs:2138 → Ins_Delete100MRecord() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_TESTID` | VARCHAR2 | ✅ | Test record ID to delete |
| `P_STDLENGTH` | VARCHAR2 | ⬜ | Standard length reference (100M) |
| `P_ACTLENGTH` | NUMBER | ⬜ | Actual measured length |
| `P_DELETEBY` | VARCHAR2 | ⬜ | Operator who authorized deletion |
| `P_DELETEREMARK` | VARCHAR2 | ⬜ | Reason for deletion |

### Output (OUT)

None (returns empty result object for success/failure indication)

---

## Business Logic

**Purpose**: Delete a 100-meter test measurement record with full audit trail. This procedure is used when a measurement is determined to be invalid, incorrect, or needs to be removed for quality control reasons.

**100-Meter Test Context**:
The 100-meter test is a standard quality control procedure in fabric manufacturing where:
- A 100-meter section of fabric is measured precisely
- Multiple parameters tested: width, density, hardness, trim, etc.
- Tests occur at regular intervals during inspection
- Test records accumulate throughout the inspection process

**When Used**:

**Scenario 1: Measurement Error Discovery**
1. Inspector performs 100M test on section starting at 100 meters
2. Records measurements in system
3. Later discovers measurement device was miscalibrated
4. Supervisor reviews and authorizes deletion
5. System calls INS_DELETE100MRECORD:
   - `P_TESTID` = "TEST-2025-001-001"
   - `P_STDLENGTH` = "100M"
   - `P_ACTLENGTH` = 98.5 (incorrect measurement)
   - `P_DELETEBY` = "SUPERVISOR-01"
   - `P_DELETEREMARK` = "Device miscalibration - remeasurement required"
6. Record deleted, inspector remeasures with calibrated device
7. New correct measurement recorded

**Scenario 2: Wrong Inspection Lot Recorded**
1. Inspector accidentally records 100M test against wrong inspection lot
2. Realizes error after saving
3. Supervisor authorizes correction
4. Delete incorrect record:
   - `P_DELETEREMARK` = "Recorded to wrong inspection lot - INS-2025-100 instead of INS-2025-101"
5. Correct record created for proper lot

**Scenario 3: Duplicate Entry Removal**
1. System glitch or operator error creates duplicate test record
2. Same test recorded twice with identical data
3. QC supervisor identifies duplicate during review
4. Deletes duplicate:
   - `P_DELETEREMARK` = "Duplicate entry - keeping original record TEST-2025-001-001"
5. Maintains data integrity by removing duplicate

**Scenario 4: Test Invalidation After Review**
1. 100M test performed, results recorded
2. QC manager reviews test results
3. Determines test procedure was not followed correctly
4. Test results deemed invalid
5. Authorizes deletion:
   - `P_DELETEBY` = "QC-MANAGER-02"
   - `P_DELETEREMARK` = "Test procedure not followed - fabric not conditioned before testing"
6. New test scheduled with proper procedure

**Scenario 5: Customer Request for Retest**
1. Customer questions original 100M test results
2. Agreement to retest section
3. Original test results deleted to avoid confusion:
   - `P_DELETEREMARK` = "Customer requested retest - original results disputed"
4. New test performed with customer witness
5. New results recorded

**Business Rules**:
- **Test ID required**: Must specify which test to delete
- **Authorization mandatory**: DELETEBY must be recorded
- **Reason required**: DELETEREMARK documents why deletion needed
- **Audit trail**: Deletion logged with operator and reason
- **Irreversible**: Deleted records cannot be recovered
- **Quality impact**: Deletion may affect inspection status
- **Supervisor approval**: Should require supervisor authorization
- **Measurement reference**: STDLENGTH and ACTLENGTH preserved in audit

**Data Integrity Considerations**:
- **Cascade effects**: Check if test results used in reports
- **Statistics impact**: Deletion affects inspection averages
- **Quality metrics**: May change pass/fail status
- **Traceability**: Audit log maintains history
- **Replacement test**: Usually followed by new test recording

**When to Use vs Edit**:
- **Use DELETE**: When measurement fundamentally invalid
- **Use EDIT**: When measurement needs correction (see INS_EDIT100TESTRECORD)
- **Examples**:
  - Wrong lot → DELETE
  - Miscalibration → DELETE and remeasure
  - Data entry typo → EDIT (correct the value)
  - Wrong decimal → EDIT (fix the number)

**Audit Trail Importance**:
The DELETEBY and DELETEREMARK parameters are critical for:
- **Quality audits**: Document why data removed
- **Customer complaints**: Explain test history
- **Regulatory compliance**: Maintain complete record
- **Process improvement**: Analyze deletion patterns
- **Training needs**: Identify common errors

---

## Related Procedures

**Alternative Operations**:
- [124-INS_EDIT100TESTRECORD.md](./124-INS_EDIT100TESTRECORD.md) - Edit test record (vs delete)
- INST_INSPECTIONTESTRECORD - Insert new test record

**Upstream**:
- INST_INSPECTIONTESTRECORD - Creates 100M test records (to be deleted)

**Similar Deletion Procedures**:
- [125-INS_DELETEDEFECT.md](./125-INS_DELETEDEFECT.md) - Delete defect record
- [126-INS_DELETEDEFECTBYLENGTH.md](./126-INS_DELETEDEFECTBYLENGTH.md) - Delete defect by position
- [115-FINISHING_CANCEL.md](../06_Finishing/115-FINISHING_CANCEL.md) - Cancel finishing operation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `Ins_Delete100MRecord(string testID, string stdLength, decimal? actLength, string deleteBy, string deleteRemark)`
**Lines**: 2138-2171

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `INS_DELETE100MRECORDParameter`
**Lines**: 5895-5902

**Result Class**: `INS_DELETE100MRECORDResult`
**Lines**: 5908-5910 (empty)

**Method**: `INS_DELETE100MRECORD(INS_DELETE100MRECORDParameter para)`
**Lines**: 23368-23400 (estimated)

---

**File**: 123/296 | **Progress**: 41.6%
