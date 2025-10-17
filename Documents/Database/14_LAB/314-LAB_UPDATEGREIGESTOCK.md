# LAB_UPDATEGREIGESTOCK

**Procedure Number**: 314 | **Module**: M14 - LAB | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update greige fabric sample test stock/status information |
| **Operation** | UPDATE |
| **Tables** | Lab greige test stock table |
| **Called From** | LABDataService.cs → LAB_UPDATEGREIGESTOCK() |
| **Frequency** | High (every workflow status change) |
| **Performance** | Fast (single row update by PK) |
| **Issues** | 🟡 1 Low (Typo: P_CONDITONTIME should be P_CONDITIONINGTIME) |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ✅ | Beam roll number (PK part 1) |
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number (PK part 2) |
| `P_TESTNO` | NUMBER | ⬜ | Test sequence/serial number |
| `P_RECEIVEDATE` | DATE | ⬜ | Sample receive date/time |
| `P_RECEIVEBY` | VARCHAR2(50) | ⬜ | Employee ID who received |
| `P_STATUS` | VARCHAR2(20) | ⬜ | Sample status |
| `P_CONDITONTIME` | DATE | ⬜ | Conditioning start time (typo in name) |
| `P_TESTBY` | VARCHAR2(50) | ⬜ | Employee ID who tested |
| `P_APPROVESTATUS` | VARCHAR2(20) | ⬜ | Approval status |
| `P_APPROVEBY` | VARCHAR2(50) | ⬜ | Employee ID who approved |
| `P_APPROVEDATE` | DATE | ⬜ | Approval date/time |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Test remarks/notes |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(100) | Operation result (SUCCESS / ERROR: message / NOT FOUND) |

---

## Business Logic (What it does and why)

Updates greige fabric sample status as it progresses through laboratory testing workflow. Provides real-time tracking from sample receipt through conditioning, testing, and final approval.

**Business Purpose**:
- **Workflow Tracking**: Real-time visibility of sample status through 4-stage workflow
- **Bottleneck Detection**: Identify delays in lab testing process
- **Resource Planning**: Lab can plan testing resources based on sample queue
- **Quality Gate Control**: Approval status controls fabric release to finishing
- **Audit Trail**: All updates create history of sample progress

**Workflow Stages** (STATUS field):
1. **RECEIVED**: Sample logged into lab system (update: RECEIVEDATE, RECEIVEBY)
2. **CONDITIONING**: Sample in environmental chamber (update: P_CONDITONTIME)
   - Standard 24 hours for moisture equilibration
3. **TESTING**: Sample undergoing physical tests (update: TESTBY)
4. **COMPLETED**: Testing finished, awaiting approval

**Approval Flow** (APPROVESTATUS field):
- **PENDING**: Awaiting supervisor review
- **APPROVED**: Supervisor approved, release to finishing
- **REJECTED**: Supervisor rejected, requires re-test

**Update Patterns**:
- **Partial Updates**: Only provided parameters are updated (flexible)
- **Status Independent**: Can update STATUS without APPROVESTATUS
- **Remark Anytime**: Can add remarks at any workflow stage
- **Validation**: Sample must exist (BEAMERROLL + LOOMNO)

**Typical Update Sequence**:
1. Receipt: STATUS=RECEIVED, RECEIVEDATE, RECEIVEBY
2. Conditioning: STATUS=CONDITIONING, P_CONDITONTIME
3. Testing: STATUS=TESTING, TESTBY
4. Completion: STATUS=COMPLETED
5. Approval: APPROVESTATUS=APPROVED, APPROVEBY, APPROVEDATE

**Business Rules**:
- BEAMERROLL + LOOMNO uniquely identify sample (composite PK)
- Dates must be chronologically logical
- Employee IDs must be valid system users
- Invalid STATUS/APPROVESTATUS returns error
- Sample not found returns "NOT FOUND"

---

## Related Procedures

**Initial Receipt**: [297-LAB_CHECKRECEIVEGREIGESAMPLING.md](./297-LAB_CHECKRECEIVEGREIGESAMPLING.md) - Creates initial record
**Search**: [312-LAB_SEARCHLABGREIGE.md](./312-LAB_SEARCHLABGREIGE.md) - Searches records to update
**Test Results**: [307-LAB_SAVELABGREIGERESULT.md](./307-LAB_SAVELABGREIGERESULT.md) - Saves detailed results
**Status Query**: [305-LAB_GREIGESTOCKSTATUS.md](./305-LAB_GREIGESTOCKSTATUS.md) - Gets current status
**Finished Update**: [315-LAB_UPDATEMASSPROSTOCK.md](./315-LAB_UPDATEMASSPROSTOCK.md) - Same logic for finished fabric

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_UPDATEGREIGESTOCK()`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_UPDATEGREIGESTOCK(LAB_UPDATEGREIGESTOCKParameter para)`
**Lines**: 17731-17781
**Parameter Class**: Lines 2650-2673

---

**File**: 314/296 | **Progress**: 65.5%
