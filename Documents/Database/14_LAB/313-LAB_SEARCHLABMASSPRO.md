# LAB_SEARCHLABMASSPRO

**Procedure Name**: `LAB_SEARCHLABMASSPRO`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Searches for mass production (finished fabric) laboratory test records
**Return Type**: Result Set (Multiple Rows)

---

## Description

Retrieves laboratory test records for mass production finished fabric samples that have been tested after the finishing process. This procedure is used to search and track test samples from finished fabric production through laboratory testing, including sample receipt, conditioning, testing, and approval workflow for final product quality validation.

Mass production testing is the final quality gate before fabric can be shipped to customers. Unlike greige testing (which tests unfinished fabric), mass production testing validates that all finishing processes (coating, scouring, drying, etc.) have been completed correctly and that the final product meets customer specifications.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_WEAVELOT | VARCHAR2 | Optional | Weaving lot number filter |
| P_ITMCODE | VARCHAR2 | Optional | Item code filter (finished product code) |
| P_FABRICTPE | VARCHAR2 | Optional | Fabric type filter |
| P_SENDDATE | VARCHAR2 | Optional | Send date filter (format: YYYY-MM-DD) |
| P_TESTRESULT | VARCHAR2 | Optional | Test result filter (PASS/FAIL/PENDING) |

**Notes**:
- All parameters are optional filters
- Can search by weaving lot, item code, or fabric type
- Date filter allows searching for specific test periods
- Test result filter for quality status tracking
- Empty/null parameters return broader results

---

## Result Set

### Output Columns (16 total)

| Column Name | Data Type | Description |
|------------|-----------|-------------|
| ITM_CODE | VARCHAR2 | Item code (finished product code) |
| WEAVINGLOT | VARCHAR2 | Weaving lot number |
| FINISHINGLOT | VARCHAR2 | Finishing lot number |
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
| FABRICTYPE | VARCHAR2 | Fabric type classification |

**Notes**:
- WEAVINGLOT and FINISHINGLOT provide complete traceability
- STATUS tracks sample through lab workflow
- CONDITIONINGTIME critical for fabric equilibration (typically 24 hours)
- TESTRESULT indicates pass/fail against customer specifications
- APPROVESTATUS separate from TESTRESULT (test can pass but need supervisor approval)
- FABRICTYPE indicates product category (PAB, DAB, SAB, etc.)

---

## Business Logic

### Mass Production Testing Workflow

1. **Sample Preparation**: Finishing operator cuts finished fabric sample
2. **Sample Send**: Sample sent to laboratory with finishing lot information
3. **Lab Receipt**: Lab technician receives sample (RECEIVEDATE, RECEIVEBY)
4. **Conditioning**: Sample conditioned in controlled environment (CONDITIONINGTIME)
   - Typically 24 hours at standard temperature/humidity (23°C ± 2°C, 50% ± 5% RH)
   - Allows fabric to reach equilibrium moisture content
5. **Comprehensive Testing**: Lab technician performs full test suite (TESTDATE, TESTBY)
   - Dimensional measurements (width, thickness, bow, skew)
   - Weight measurements (total, uncoated, coating)
   - Thread count (warp and filling)
   - Tensile strength and elongation
   - Tear resistance
   - Air permeability (static and dynamic)
   - Flammability
   - Edge comb resistance
   - Stiffness and bending
   - Flex abrasion
   - Dimensional stability
6. **Results Entry**: Test results entered into system (TESTRESULT)
7. **Supervisor Approval**: Quality supervisor approves results (APPROVESTATUS)
8. **Shipment Decision**: Based on results, fabric either ships to customer or is held/reworked

### Test Result Interpretation

- **PASS**: Finished fabric meets all customer specifications, approved for shipment
- **FAIL**: Fabric does not meet specifications, cannot ship (may rework or scrap)
- **PENDING**: Test incomplete or awaiting additional analysis

### Approval Status Flow

- **PENDING**: Awaiting supervisor review and approval
- **APPROVED**: Supervisor approved test results, fabric cleared for shipment
- **REJECTED**: Supervisor rejected test results (requires re-test or investigation)

---

## Usage Example

### C# Method Call

```csharp
// From AirbagSPs.cs (lines 17785-17850)
public List<LAB_SEARCHLABMASSPROResult> LAB_SEARCHLABMASSPRO(
    LAB_SEARCHLABMASSPROParameter para)
{
    List<LAB_SEARCHLABMASSPROResult> results = new List<LAB_SEARCHLABMASSPROResult>();
    if (!HasConnection())
        return results;

    string[] paraNames = new string[]
    {
        "P_WEAVELOT",
        "P_ITMCODE",
        "P_FABRICTPE",
        "P_SENDDATE",
        "P_TESTRESULT"
    };
    object[] paraValues = new object[]
    {
        para.P_WEAVELOT,
        para.P_ITMCODE,
        para.P_FABRICTPE,
        para.P_SENDDATE,
        para.P_TESTRESULT
    };

    ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
        "LAB_SEARCHLABMASSPRO",
        paraNames, paraValues);
    // ... result mapping for 16 columns ...
}
```

### Common Usage Patterns

```csharp
// Search all tests for specific weaving lot
var param = new LAB_SEARCHLABMASSPROParameter
{
    P_WEAVELOT = "WL-20231015-001",
    P_ITMCODE = null,
    P_FABRICTPE = null,
    P_SENDDATE = null,
    P_TESTRESULT = null
};
var results = DatabaseManager.Instance.LAB_SEARCHLABMASSPRO(param);

// Search failed tests for specific item code
var param = new LAB_SEARCHLABMASSPROParameter
{
    P_WEAVELOT = null,
    P_ITMCODE = "AB-1234-PAB",
    P_FABRICTPE = null,
    P_SENDDATE = null,
    P_TESTRESULT = "FAIL"
};
var results = DatabaseManager.Instance.LAB_SEARCHLABMASSPRO(param);

// Search all PAB fabric tests sent on specific date
var param = new LAB_SEARCHLABMASSPROParameter
{
    P_WEAVELOT = null,
    P_ITMCODE = null,
    P_FABRICTPE = "PAB",
    P_SENDDATE = "2023-10-17",
    P_TESTRESULT = null
};
var results = DatabaseManager.Instance.LAB_SEARCHLABMASSPRO(param);

// Search pending approvals (passed tests awaiting approval)
var param = new LAB_SEARCHLABMASSPROParameter
{
    P_WEAVELOT = null,
    P_ITMCODE = null,
    P_FABRICTPE = null,
    P_SENDDATE = null,
    P_TESTRESULT = "PASS"
};
var results = DatabaseManager.Instance.LAB_SEARCHLABMASSPRO(param);
```

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_CHECKRECEIVESAMPLING | Sample receipt | Records finished sample receipt at lab |
| LAB_SAVELABMASSPRORESULT | Data entry | Saves mass production test results |
| LAB_MASSPROSTOCKSTATUS | Status query | Gets mass production sample status summary |
| LAB_SEARCHLABENTRYPRODUCTION | Detailed tests | Searches with full 100+ test measurements |
| LAB_SEARCHAPPROVELAB | Approval workflow | Searches for approval queue (similar purpose) |
| LAB_SEARCHLABGREIGE | Greige tests | Searches greige (unfinished) test records |
| LAB_GETITEMTESTSPECIFICATION | Specifications | Gets test specs for comparison |

---

## UI Integration

### Primary Pages

- **Mass Production Lab Test Search Page**: Main search interface
- **Finished Sample Tracking Page**: Track finished samples through workflow
- **Mass Production Test Entry Page**: Enter and review test results
- **Shipment Approval Dashboard**: Monitor tests pending shipment approval

### Typical Display

```
Mass Production Lab Test Search
--------------------------------
Item Code    Weaving Lot    Finishing Lot   Send Date   Test Date   Result  Status     Action
AB-1234-PAB  WL-20231015-01 FL-20231020-001 2023-10-20  2023-10-21  PASS    APPROVED   [View]
AB-1234-DAB  WL-20231015-02 FL-20231020-002 2023-10-20  2023-10-21  FAIL    PENDING    [Review]
AB-1234-SAB  WL-20231015-03 FL-20231020-003 2023-10-21  -           PENDING -          [Enter]

Filters:
[Weaving Lot: ________] [Item Code: __________] [Fabric Type: ▼ All]
[Send Date: From ____ To ____] [Result: ▼ All]
[Search]

Status Legend:
• RECEIVED - Sample received, awaiting conditioning
• CONDITIONING - Sample in environmental chamber
• TESTING - Tests in progress
• COMPLETED - All tests complete, awaiting approval

[View] → Display detailed test results (links to LAB_SEARCHLABENTRYPRODUCTION)
[Review] → Supervisor approval interface
[Enter] → Test data entry form
```

---

## Notes

- **Final Quality Gate**: Last inspection before customer shipment
- **Critical for Shipment**: No fabric ships without passed mass production test
- **Customer Specifications**: Tests must meet customer-specific requirements
- **Conditioning Critical**: Must allow 24 hours equilibration before testing
- **Workflow Tracking**: Complete audit trail from sample send to approval
- **Traceability**: Links finished product back to weaving and finishing lots
- **Multiple Tests**: Single lot may have multiple samples tested for validation
- **Statistical Analysis**: Data supports quality trending and SPC analysis
- **Shipment Hold**: Failed tests automatically hold fabric from shipment
- **Re-test Protocol**: Failed tests may require investigation and re-testing
- **Certificate of Analysis**: Test data used to generate C of A for customers
- **Fabric Type Classification**: PAB (Passenger Airbag), DAB (Driver Airbag), SAB (Side Airbag), etc.

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 2679-2710, 17785-17850)
