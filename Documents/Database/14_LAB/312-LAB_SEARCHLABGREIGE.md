# LAB_SEARCHLABGREIGE

**Procedure Name**: `LAB_SEARCHLABGREIGE`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Searches for greige (unfinished/grey) fabric laboratory test records
**Return Type**: Result Set (Multiple Rows)

---

## Description

Retrieves laboratory test records specifically for greige (grey/unfinished) fabric samples that are tested before the finishing process. This procedure is used to search and track test samples from weaving to laboratory testing, including sample receipt, conditioning, testing, and approval workflow for greige fabric quality validation.

Greige fabric testing is critical for identifying quality issues early in the production process, before expensive finishing operations are applied. This allows for early detection of yarn defects, weaving problems, or specification deviations that could affect final product quality.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_BEAMERROLL | VARCHAR2 | Optional | Beam roll number filter |
| P_LOOM | VARCHAR2 | Optional | Loom machine number filter |
| P_ITMWEAVE | VARCHAR2 | Optional | Weaving item code filter |
| P_SETTINGDATE | VARCHAR2 | Optional | Setting date filter (format: YYYY-MM-DD) |
| P_SENDDATE | VARCHAR2 | Optional | Send date filter (format: YYYY-MM-DD) |
| P_TESTRESULT | VARCHAR2 | Optional | Test result filter (PASS/FAIL/PENDING) |

**Notes**:
- All parameters are optional filters
- Can search by beam roll, loom, or item code
- Date filters allow searching for specific test periods
- Test result filter for quality status tracking
- Empty/null parameters return broader results

---

## Result Set

### Output Columns (17 total)

| Column Name | Data Type | Description |
|------------|-----------|-------------|
| BEAMERROLL | VARCHAR2 | Beam roll number/identifier |
| LOOMNO | VARCHAR2 | Loom machine number |
| ITM_WEAVING | VARCHAR2 | Weaving item code |
| TESTNO | NUMBER | Test sequence/serial number |
| RECEIVEDATE | DATE | Sample receive date/time at lab |
| RECEIVEBY | VARCHAR2 | Employee ID who received sample |
| STATUS | VARCHAR2 | Sample status (RECEIVED/CONDITIONING/TESTING/COMPLETED) |
| CONDITIONINGTIME | DATE | Conditioning start time (environmental stabilization) |
| TESTDATE | DATE | Actual test date/time |
| TESTRESULT | VARCHAR2 | Test result (PASS/FAIL/PENDING) |
| REMARK | VARCHAR2 | Test remarks or notes |
| TESTBY | VARCHAR2 | Employee ID who performed test |
| APPROVESTATUS | VARCHAR2 | Approval status (PENDING/APPROVED/REJECTED) |
| APPROVEBY | VARCHAR2 | Employee ID who approved/rejected |
| SENDDATE | DATE | Date sample sent to lab |
| APPROVEDATE | DATE | Approval date/time |
| SETTINGDATE | DATE | Loom setting date (production date) |

**Notes**:
- TESTNO provides unique test sequence tracking
- STATUS tracks sample through lab workflow
- CONDITIONINGTIME critical for fabric equilibration (typically 24 hours)
- TESTRESULT indicates pass/fail against specifications
- APPROVESTATUS separate from TESTRESULT (test can pass but need supervisor approval)

---

## Business Logic

### Greige Fabric Testing Workflow

1. **Sample Preparation**: Weaving operator cuts greige sample from loom
2. **Sample送**: Sample sent to laboratory with beam roll information
3. **Lab Receipt**: Lab technician receives sample (RECEIVEDATE, RECEIVEBY)
4. **Conditioning**: Sample conditioned in controlled environment (CONDITIONINGTIME)
   - Typically 24 hours at standard temperature/humidity
   - Allows fabric to reach equilibrium moisture content
5. **Testing**: Lab technician performs tests (TESTDATE, TESTBY)
   - Visual inspection
   - Dimensional measurements
   - Thread count
   - Specific tests per specification
6. **Results Entry**: Test results entered into system (TESTRESULT)
7. **Supervisor Approval**: Quality supervisor approves results (APPROVESTATUS)
8. **Action**: Based on results, fabric proceeds to finishing or is reworked

### Test Result Interpretation

- **PASS**: Greige fabric meets specifications, proceed to finishing
- **FAIL**: Fabric has defects, may require rework or scrapping
- **PENDING**: Test incomplete or awaiting additional analysis

### Approval Status Flow

- **PENDING**: Awaiting supervisor review
- **APPROVED**: Supervisor approved test results
- **REJECTED**: Supervisor rejected test results (requires re-test)

---

## Usage Example

### C# Method Call

```csharp
// From AirbagSPs.cs (lines 17854-17922)
public List<LAB_SEARCHLABGREIGEResult> LAB_SEARCHLABGREIGE(
    LAB_SEARCHLABGREIGEParameter para)
{
    List<LAB_SEARCHLABGREIGEResult> results = new List<LAB_SEARCHLABGREIGEResult>();
    if (!HasConnection())
        return results;

    string[] paraNames = new string[]
    {
        "P_BEAMERROLL",
        "P_LOOM",
        "P_ITMWEAVE",
        "P_SETTINGDATE",
        "P_SENDDATE",
        "P_TESTRESULT"
    };
    object[] paraValues = new object[]
    {
        para.P_BEAMERROLL,
        para.P_LOOM,
        para.P_ITMWEAVE,
        para.P_SETTINGDATE,
        para.P_SENDDATE,
        para.P_TESTRESULT
    };

    ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
        "LAB_SEARCHLABGREIGE",
        paraNames, paraValues);
    // ... result mapping for 17 columns ...
}
```

### Common Usage Patterns

```csharp
// Search all tests for specific beam roll
var param = new LAB_SEARCHLABGREIGEParameter
{
    P_BEAMERROLL = "BR-20231015-001",
    P_LOOM = null,
    P_ITMWEAVE = null,
    P_SETTINGDATE = null,
    P_SENDDATE = null,
    P_TESTRESULT = null
};
var results = DatabaseManager.Instance.LAB_SEARCHLABGREIGE(param);

// Search failed tests for specific loom
var param = new LAB_SEARCHLABGREIGEParameter
{
    P_BEAMERROLL = null,
    P_LOOM = "LOOM-05",
    P_ITMWEAVE = null,
    P_SETTINGDATE = null,
    P_SENDDATE = null,
    P_TESTRESULT = "FAIL"
};
var results = DatabaseManager.Instance.LAB_SEARCHLABGREIGE(param);

// Search tests sent on specific date
var param = new LAB_SEARCHLABGREIGEParameter
{
    P_BEAMERROLL = null,
    P_LOOM = null,
    P_ITMWEAVE = null,
    P_SETTINGDATE = null,
    P_SENDDATE = "2023-10-17",
    P_TESTRESULT = null
};
var results = DatabaseManager.Instance.LAB_SEARCHLABGREIGE(param);

// Search pending approvals for specific item
var param = new LAB_SEARCHLABGREIGEParameter
{
    P_BEAMERROLL = null,
    P_LOOM = null,
    P_ITMWEAVE = "ITM-WEAVE-001",
    P_SETTINGDATE = null,
    P_SENDDATE = null,
    P_TESTRESULT = "PASS"
};
var results = DatabaseManager.Instance.LAB_SEARCHLABGREIGE(param);
```

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_CHECKRECEIVEGREIGESAMPLING | Sample receipt | Records greige sample receipt at lab |
| LAB_SAVELABGREIGERESULT | Data entry | Saves greige test results |
| LAB_GREIGESTOCKSTATUS | Status query | Gets greige sample status summary |
| LAB_GETWEAVINGSAMPLING | Sample source | Gets weaving samples for testing |
| LAB_SEARCHLABMASSPRO | Finished tests | Searches finished (mass production) test records |

---

## UI Integration

### Primary Pages

- **Greige Lab Test Search Page**: Main search interface
- **Greige Sample Tracking Page**: Track samples through workflow
- **Greige Test Entry Page**: Enter and review test results
- **Greige Quality Dashboard**: Monitor overall greige quality status

### Typical Display

```
Greige Fabric Lab Test Search
------------------------------
Beam Roll       Loom     Item Code      Test Date   Result  Status     Action
BR-20231015-001 LOOM-05  ITM-WEAVE-001  2023-10-16  PASS    APPROVED   [View]
BR-20231015-002 LOOM-05  ITM-WEAVE-001  2023-10-16  FAIL    PENDING    [Review]
BR-20231015-003 LOOM-08  ITM-WEAVE-002  2023-10-17  PENDING -          [Enter]

Filters:
[Beam Roll: ________] [Loom: ______] [Item: __________]
[Send Date: From ____ To ____] [Result: ▼ All]
[Search]

[View] → Display detailed test results
[Review] → Supervisor approval interface
[Enter] → Test data entry form
```

---

## Notes

- **Early Detection**: Greige testing catches problems before expensive finishing
- **Cost Savings**: Failed greige fabric costs less than failed finished fabric
- **Conditioning Critical**: Must allow fabric to equilibrate before testing (24 hrs standard)
- **Workflow Tracking**: Complete audit trail from sample send to approval
- **Quality Gate**: Acts as quality gate before finishing process
- **Loom Performance**: Failed tests may indicate loom problems requiring maintenance
- **Yarn Quality**: Greige test failures often trace back to yarn quality issues
- **Production Planning**: Test results influence production scheduling decisions
- **Traceability**: Links test results back to specific beam rolls and looms
- **Statistical Analysis**: Data supports loom-by-loom quality trending

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 2716-2749, 17854-17922)
