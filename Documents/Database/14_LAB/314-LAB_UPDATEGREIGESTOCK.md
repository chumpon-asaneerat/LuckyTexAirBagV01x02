# LAB_UPDATEGREIGESTOCK

**Procedure Name**: `LAB_UPDATEGREIGESTOCK`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Updates greige fabric sample test stock/status information
**Return Type**: Single Result (Status Code)

---

## Description

Updates the status and testing information for greige (unfinished/grey) fabric samples in the laboratory stock/tracking system. This procedure is used to record changes in sample status as it progresses through the laboratory workflow, including receipt, conditioning, testing, approval, and final disposition.

This procedure provides workflow tracking for greige fabric samples from the moment they arrive at the laboratory until final approval or rejection. It enables real-time status updates that allow production and quality teams to monitor testing progress and identify bottlenecks in the testing process.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_BEAMERROLL | VARCHAR2 | Yes | Beam roll number/identifier |
| P_LOOMNO | VARCHAR2 | Yes | Loom machine number |
| P_TESTNO | NUMBER | Optional | Test sequence/serial number |
| P_RECEIVEDATE | DATE | Optional | Sample receive date/time at lab |
| P_RECEIVEBY | VARCHAR2 | Optional | Employee ID who received sample |
| P_STATUS | VARCHAR2 | Optional | Sample status (RECEIVED/CONDITIONING/TESTING/COMPLETED) |
| P_CONDITONTIME | DATE | Optional | Conditioning start time |
| P_TESTBY | VARCHAR2 | Optional | Employee ID who performed test |
| P_APPROVESTATUS | VARCHAR2 | Optional | Approval status (PENDING/APPROVED/REJECTED) |
| P_APPROVEBY | VARCHAR2 | Optional | Employee ID who approved/rejected |
| P_APPROVEDATE | DATE | Optional | Approval date/time |
| P_REMARK | VARCHAR2 | Optional | Test remarks or notes |

**Notes**:
- P_BEAMERROLL and P_LOOMNO are required to identify the sample
- Other parameters optional - only provided values are updated
- Status values: RECEIVED, CONDITIONING, TESTING, COMPLETED
- Approval status values: PENDING, APPROVED, REJECTED
- Typo in parameter name: P_CONDITONTIME (should be CONDITIONING)

---

## Result Set

### Output Parameters

| Parameter Name | Data Type | Description |
|---------------|-----------|-------------|
| RESULT | VARCHAR2 | Operation result code/message |

**Typical Return Values**:
- `"SUCCESS"` - Update completed successfully
- `"ERROR: [message]"` - Update failed with error description
- `"NOT FOUND"` - Sample record not found
- `"INVALID STATUS"` - Invalid status value provided

---

## Business Logic

### Update Workflow Tracking

This procedure updates samples through the following workflow stages:

1. **Receipt Stage**
   - Status: RECEIVED
   - Records: P_RECEIVEDATE, P_RECEIVEBY
   - Sample logged into laboratory system

2. **Conditioning Stage**
   - Status: CONDITIONING
   - Records: P_CONDITONTIME
   - Sample placed in environmental chamber (24 hours typical)

3. **Testing Stage**
   - Status: TESTING
   - Records: P_TESTBY
   - Sample undergoing physical testing

4. **Completion Stage**
   - Status: COMPLETED
   - Testing finished, awaiting approval

5. **Approval Stage**
   - ApproveStatus: PENDING → APPROVED/REJECTED
   - Records: P_APPROVEBY, P_APPROVEDATE
   - Supervisor reviews and approves/rejects results

### Partial Updates

- Procedure allows partial updates (only provided parameters updated)
- Can update status independently of approval status
- Can add remarks at any workflow stage
- Test number may be assigned during receipt or testing

### Validation Rules

- Sample must exist (identified by BEAMERROLL + LOOMNO)
- Status must be valid workflow status
- Approval status must be valid approval value
- Dates must be chronologically logical
- Employee IDs must be valid system users

---

## Usage Example

### C# Method Call

```csharp
// From AirbagSPs.cs (lines 17731-17781)
public LAB_UPDATEGREIGESTOCKResult LAB_UPDATEGREIGESTOCK(
    LAB_UPDATEGREIGESTOCKParameter para)
{
    LAB_UPDATEGREIGESTOCKResult result = null;
    if (!HasConnection())
        return result;

    string[] paraNames = new string[]
    {
        "P_BEAMERROLL",
        "P_LOOMNO",
        "P_TESTNO",
        "P_RECEIVEDATE",
        "P_RECEIVEBY",
        "P_STATUS",
        "P_CONDITONTIME",
        "P_TESTBY",
        "P_APPROVESTATUS",
        "P_APPROVEBY",
        "P_APPROVEDATE",
        "P_REMARK"
    };
    object[] paraValues = new object[]
    {
        para.P_BEAMERROLL,
        para.P_LOOMNO,
        para.P_TESTNO,
        para.P_RECEIVEDATE,
        para.P_RECEIVEBY,
        para.P_STATUS,
        para.P_CONDITONTIME,
        para.P_TESTBY,
        para.P_APPROVESTATUS,
        para.P_APPROVEBY,
        para.P_APPROVEDATE,
        para.P_REMARK
    };

    ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
        "LAB_UPDATEGREIGESTOCK",
        paraNames, paraValues);
    if (null != ret && !ret.HasException)
    {
        result = new LAB_UPDATEGREIGESTOCKResult();
        if (ret.Result.OutParameters["RESULT"] != DBNull.Value)
            result.RESULT = (System.String)ret.Result.OutParameters["RESULT"];
    }

    return result;
}
```

### Common Usage Patterns

```csharp
// Update sample status to RECEIVED when sample arrives
var param = new LAB_UPDATEGREIGESTOCKParameter
{
    P_BEAMERROLL = "BR-20231015-001",
    P_LOOMNO = "LOOM-05",
    P_TESTNO = 12345,
    P_RECEIVEDATE = DateTime.Now,
    P_RECEIVEBY = "EMP001",
    P_STATUS = "RECEIVED",
    P_CONDITONTIME = null,
    P_TESTBY = null,
    P_APPROVESTATUS = null,
    P_APPROVEBY = null,
    P_APPROVEDATE = null,
    P_REMARK = "Sample received from weaving"
};
var result = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(param);

// Update status to CONDITIONING when placed in chamber
var param = new LAB_UPDATEGREIGESTOCKParameter
{
    P_BEAMERROLL = "BR-20231015-001",
    P_LOOMNO = "LOOM-05",
    P_TESTNO = null,
    P_RECEIVEDATE = null,
    P_RECEIVEBY = null,
    P_STATUS = "CONDITIONING",
    P_CONDITONTIME = DateTime.Now,
    P_TESTBY = null,
    P_APPROVESTATUS = null,
    P_APPROVEBY = null,
    P_APPROVEDATE = null,
    P_REMARK = null
};
var result = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(param);

// Update to TESTING status when test begins
var param = new LAB_UPDATEGREIGESTOCKParameter
{
    P_BEAMERROLL = "BR-20231015-001",
    P_LOOMNO = "LOOM-05",
    P_TESTNO = null,
    P_RECEIVEDATE = null,
    P_RECEIVEBY = null,
    P_STATUS = "TESTING",
    P_CONDITONTIME = null,
    P_TESTBY = "EMP002",
    P_APPROVESTATUS = null,
    P_APPROVEBY = null,
    P_APPROVEDATE = null,
    P_REMARK = null
};
var result = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(param);

// Update approval status when supervisor approves
var param = new LAB_UPDATEGREIGESTOCKParameter
{
    P_BEAMERROLL = "BR-20231015-001",
    P_LOOMNO = "LOOM-05",
    P_TESTNO = null,
    P_RECEIVEDATE = null,
    P_RECEIVEBY = null,
    P_STATUS = "COMPLETED",
    P_CONDITONTIME = null,
    P_TESTBY = null,
    P_APPROVESTATUS = "APPROVED",
    P_APPROVEBY = "EMP003",
    P_APPROVEDATE = DateTime.Now,
    P_REMARK = "Test results approved - proceed to finishing"
};
var result = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(param);

// Add remark to existing sample
var param = new LAB_UPDATEGREIGESTOCKParameter
{
    P_BEAMERROLL = "BR-20231015-001",
    P_LOOMNO = "LOOM-05",
    P_TESTNO = null,
    P_RECEIVEDATE = null,
    P_RECEIVEBY = null,
    P_STATUS = null,
    P_CONDITONTIME = null,
    P_TESTBY = null,
    P_APPROVESTATUS = null,
    P_APPROVEBY = null,
    P_APPROVEDATE = null,
    P_REMARK = "Re-test requested due to abnormal reading"
};
var result = DatabaseManager.Instance.LAB_UPDATEGREIGESTOCK(param);
```

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_CHECKRECEIVEGREIGESAMPLING | Initial receipt | Records initial greige sample receipt |
| LAB_SAVELABGREIGERESULT | Test results | Saves detailed greige test results |
| LAB_SEARCHLABGREIGE | Search | Searches greige test records |
| LAB_GREIGESTOCKSTATUS | Status query | Gets current greige sample status |
| LAB_UPDATEMASSPROSTOCK | Finished update | Updates mass production sample status |

---

## UI Integration

### Primary Pages

- **Greige Sample Receipt Page**: Update status when receiving samples
- **Conditioning Tracking Page**: Update conditioning status
- **Test Entry Page**: Update testing status and test-by information
- **Approval Dashboard**: Update approval status and remarks

### Typical UI Flow

```
Greige Sample Tracking Interface
---------------------------------

Sample: BR-20231015-001 (LOOM-05)
Current Status: CONDITIONING

[Update Status]

Status: ▼ TESTING
Test By: [EMP002      ] [Lookup]
Remarks: [_______________________________]

[Update] [Cancel]

Status History:
2023-10-15 08:30 - RECEIVED by EMP001
2023-10-15 09:00 - CONDITIONING started
2023-10-16 09:15 - TESTING by EMP002 (current)
```

---

## Notes

- **Real-time Tracking**: Enables real-time visibility of sample status
- **Workflow Enforcement**: Ensures samples follow proper testing workflow
- **Partial Updates**: Only updates fields that are provided (flexible)
- **Audit Trail**: All updates create audit trail of sample progress
- **Bottleneck Detection**: Status tracking helps identify testing delays
- **Resource Planning**: Helps lab plan testing resources based on sample queue
- **Quality Gate**: Approval status controls fabric release to finishing
- **Integration Point**: Status updates trigger notifications to production teams
- **Typo Note**: P_CONDITONTIME has typo (should be CONDITIONING) - legacy code
- **Return Value**: Always check RESULT parameter for success/error status
- **Transaction Safety**: Updates should be atomic to prevent data corruption
- **Duplicate Prevention**: BEAMERROLL + LOOMNO uniquely identify sample

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 2650-2673, 17731-17781)
