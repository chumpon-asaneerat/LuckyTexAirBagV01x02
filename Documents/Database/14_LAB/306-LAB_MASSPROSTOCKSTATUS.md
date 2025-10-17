# LAB_MASSPROSTOCKSTATUS

**Procedure Name**: `LAB_MASSPROSTOCKSTATUS`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Gets mass production sample stock status and test results for finished fabric
**Return Type**: Result Set (Multiple Rows)

---

## Description

Retrieves the current status of mass production (finished fabric) samples in the laboratory testing system. This procedure tracks samples from receipt through testing, approval, and final disposition. It provides comprehensive information about sample location, test status, results, and approval workflow. Used primarily for monitoring samples awaiting testing, samples currently being tested, and completed tests pending approval.

Similar to `LAB_GREIGESTOCKSTATUS` but for finished fabric (after coating/finishing) rather than greige (raw woven) fabric. Mass production samples come from the finishing module with a weaving lot and finishing lot identifier.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_WEAVELOT | VARCHAR2 | Optional | Weaving lot number filter (partial match supported) |
| P_RECEIVEDATE | VARCHAR2 | Optional | Sample receive date filter (format: YYYY-MM-DD or date range) |

**Notes**:
- Both parameters are optional filters
- If P_WEAVELOT is null/empty, returns all weaving lots
- If P_RECEIVEDATE is null/empty, returns all receive dates
- Commonly used with one or both filters to narrow search results

---

## Result Set

### Output Columns

| Column Name | Data Type | Nullable | Description |
|------------|-----------|----------|-------------|
| ITM_CODE | VARCHAR2 | Yes | Item code (finished product code) |
| WEAVINGLOT | VARCHAR2 | Yes | Weaving lot number (source greige fabric) |
| FINISHINGLOT | VARCHAR2 | Yes | Finishing lot number (coated/finished fabric) |
| RECEIVEDATE | DATE | Yes | Date/time when sample was received in lab |
| RECEIVEBY | VARCHAR2 | Yes | Employee ID who received the sample |
| STATUS | VARCHAR2 | Yes | Current sample status (RECEIVED/TESTING/TESTED) |
| CONDITIONINGTIME | VARCHAR2 | Yes | Sample conditioning time (hours:minutes or "Ready" when complete) |
| TESTDATE | DATE | Yes | Date/time when testing was performed |
| TESTRESULT | VARCHAR2 | Yes | Overall test result (PASS/FAIL/PENDING) |
| REMARK | VARCHAR2 | Yes | Test remarks or additional notes |
| TESTBY | VARCHAR2 | Yes | Employee ID who performed the tests |
| APPROVESTATUS | VARCHAR2 | Yes | Approval status (PENDING/APPROVED/REJECTED) |
| APPROVEBY | VARCHAR2 | Yes | Employee ID who approved/rejected the test |
| SENDDATE | DATE | Yes | Date/time when test results were sent for approval |
| APPROVEDATE | DATE | Yes | Date/time when test was approved/rejected |
| LABFORM | VARCHAR2 | Yes | Lab test form identifier or document number |

**Notes**:
- STATUS workflow: RECEIVED → TESTING → TESTED
- APPROVESTATUS workflow: PENDING → APPROVED or REJECTED
- CONDITIONINGTIME typically shows elapsed time until "Ready" status
- All columns nullable to support various workflow stages

---

## Business Logic

### Sample Status Workflow

1. **RECEIVED**: Sample arrived in lab, awaiting conditioning period
2. **TESTING**: Technician actively performing tests
3. **TESTED**: All tests complete, awaiting approval
4. **APPROVED/REJECTED**: Final disposition after quality review

### Conditioning Requirements

Mass production samples typically require a stabilization period (often 24 hours) before testing to ensure accurate results. The CONDITIONINGTIME field tracks this:
- Format: "HH:MM" showing remaining time
- When ready: Shows "Ready" or "Complete"
- Testing cannot begin until conditioning complete

### Test Result Approval

After testing completes:
1. Test results sent for review (SENDDATE recorded)
2. Quality supervisor reviews data
3. Approver makes PASS/FAIL decision
4. APPROVEDATE and APPROVEBY recorded
5. Sample disposition determined based on approval

---

## Usage Example

### C# Method Call

```csharp
// From LABDataService.cs (lines 530-546)
public List<LAB_MASSPROSTOCKSTATUS> LAB_MASSPROSTOCKSTATUS(
    string P_WEAVELOT,
    string P_RECEIVEDATE)
{
    List<LAB_MASSPROSTOCKSTATUS> results = null;

    if (!HasConnection())
        return results;

    LAB_MASSPROSTOCKSTATUSParameter dbPara = new LAB_MASSPROSTOCKSTATUSParameter();
    dbPara.P_WEAVELOT = P_WEAVELOT;
    dbPara.P_RECEIVEDATE = P_RECEIVEDATE;

    List<LAB_MASSPROSTOCKSTATUSResult> dbResults = null;

    try
    {
        dbResults = DatabaseManager.Instance.LAB_MASSPROSTOCKSTATUS(dbPara);
        if (null != dbResults)
        {
            results = new List<LAB_MASSPROSTOCKSTATUS>();
            foreach (LAB_MASSPROSTOCKSTATUSResult dbResult in dbResults)
            {
                // Map to business entity...
            }
        }
    }
    catch (Exception ex)
    {
        // Error handling...
    }

    return results;
}
```

### Common Usage Patterns

```csharp
// Get all samples for a specific weaving lot
var samples = labService.LAB_MASSPROSTOCKSTATUS("WL-20231015-001", null);

// Get all samples received on specific date
var todaySamples = labService.LAB_MASSPROSTOCKSTATUS(null, "2023-10-15");

// Get all pending samples (no filters)
var allSamples = labService.LAB_MASSPROSTOCKSTATUS(null, null);

// Get samples for lot received within date range
var rangedSamples = labService.LAB_MASSPROSTOCKSTATUS(
    "WL-20231015",
    "2023-10-01 TO 2023-10-31");
```

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_GREIGESTOCKSTATUS | Similar for greige | Gets greige sample status (pre-finishing) |
| LAB_CHECKRECEIVESAMPLING | Upstream | Receives mass production samples into lab |
| LAB_GETFINISHINGSAMPLING | Detail retrieval | Gets detailed sample information for testing |
| LAB_SAVELABMASSPRORESULT | Downstream | Saves test results for mass production samples |
| LAB_GETITEMTESTSPECIFICATION | Specification | Gets test specifications for validation |

---

## UI Integration

### Primary Pages

- **Mass Production Sample Status Page**: Main dashboard for lab technicians
- **Sample Tracking Page**: Monitor samples through workflow stages
- **Approval Queue Page**: Quality supervisor review interface

### Typical Display

```
Mass Production Sample Status
------------------------------
Weaving Lot    Finishing Lot    Item Code    Receive Date    Status      Test Result    Approve Status
WL-20231015-01 FL-20231020-001  AB-1234-PAB  2023-10-20 08:00 TESTING    PENDING        PENDING
WL-20231015-02 FL-20231020-002  AB-1234-PAB  2023-10-20 09:30 TESTED     PASS           PENDING
WL-20231015-03 FL-20231020-003  AB-1234-DAB  2023-10-20 10:15 RECEIVED   -              -
```

---

## Database Schema

### Primary Table(s)
- `LAB_MASSPRO_SAMPLE` or `LAB_SAMPLE_MASSPRO` (mass production sample master)
- `LAB_MASSPRO_TEST_RESULT` (test result details)
- `LAB_SAMPLE_APPROVAL` (approval workflow tracking)

### Key Relationships
- Links to `WEAVING_LOT_MASTER` via WEAVINGLOT
- Links to `FINISHING_LOT_MASTER` via FINISHINGLOT
- Links to `ITEM_MASTER` via ITM_CODE
- Links to `EMPLOYEE` via RECEIVEBY, TESTBY, APPROVEBY

---

## Notes

- Mass production samples are from finished fabric (after coating/finishing process)
- Distinguished from greige samples which test raw woven fabric quality
- Critical for customer shipment approval - must pass all tests
- Test results often tied to customer specifications and certifications
- LABFORM may reference specific customer test report templates
- Some customers require specific conditioning times (12h, 24h, 48h)
- Approval status critical for production release decisions

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 3548-3580)
**Implementation File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs` (lines 530-546)
