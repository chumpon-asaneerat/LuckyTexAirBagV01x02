# LAB_SAVELABMASSPRORESULT

**Procedure Name**: `LAB_SAVELABMASSPRORESULT`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Saves or updates mass production (finished fabric) test result approval status
**Return Type**: Success/Failure indicator (empty result on success)

---

## Description

Updates the test result approval status for mass production (finished/coated fabric) samples after laboratory testing is complete. This procedure records the final test result (PASS/FAIL) for a specific finished product sample identified by weaving lot, item code, and finishing lot. Typically called after all individual tests are completed and a technician or supervisor makes the final approval decision for customer shipment.

This is a lightweight status update procedure - it does not store detailed test measurements (those are saved separately via individual test procedures). It only updates the overall test result flag for the sample, which is critical for production release and customer shipment approval.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_WEAVELOT | VARCHAR2 | Yes | Weaving lot number (source greige fabric lot) |
| P_ITMCODE | VARCHAR2 | Yes | Finished product item code |
| P_FINISHINGLOT | VARCHAR2 | Yes | Finishing lot number (after coating/finishing process) |
| P_TESTRESULT | VARCHAR2 | Yes | Overall test result (PASS/FAIL/PENDING) |

**Notes**:
- All parameters are required (P_WEAVELOT validated as mandatory in DataService)
- P_WEAVELOT + P_ITMCODE + P_FINISHINGLOT form unique identifier for finished sample
- P_TESTRESULT typical values: "PASS", "FAIL", "PENDING"
- Unlike greige testing, no separate test number parameter (retests create new samples)

---

## Result Set

### Output Columns

No output columns returned. Success indicated by non-null result object; failure indicated by null result or exception.

**Notes**:
- Empty result class (LAB_SAVELABMASSPRORESULTResult has no properties)
- Check for null return value to determine success/failure
- Exceptions thrown for database errors or constraint violations

---

## Business Logic

### Test Result Workflow

1. **Sample Received**: Finished fabric sample arrives in lab from finishing process
2. **Conditioning Period**: 24-hour stabilization (typically required)
3. **Individual Tests Performed**: Technician runs all customer-specified tests
4. **Results Entered**: Each test result saved via specific test procedures
5. **Final Approval**: Supervisor reviews all tests and calls this procedure with overall result
6. **Production Release**: PASS result allows shipment; FAIL result triggers rework/scrap decision

### Quality Gate Importance

This is the final quality gate before customer shipment:
- **PASS**: Lot approved for packing and shipping to customer
- **FAIL**: Lot must be reworked, downgraded, or scrapped
- **PENDING**: Additional testing or customer approval required

### Mass Production vs Greige Testing

This procedure is specifically for finished fabric (mass production):
- **Mass Production**: After coating/finishing - Uses WEAVINGLOT + FINISHINGLOT + ITMCODE
- **Greige**: Raw woven fabric - Uses BEAMERROLL + LOOM + ITMWEAVE (see LAB_SAVELABGREIGERESULT)

---

## Usage Example

### C# Method Call

### Common Usage Patterns

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_MASSPROSTOCKSTATUS | Status query | Gets current status of mass production samples |
| LAB_GETFINISHINGSAMPLING | Detail retrieval | Gets finished sample details for testing |
| LAB_GETITEMTESTSPECIFICATION | Specification | Gets customer test specifications |
| LAB_SAVELABGREIGERESULT | Similar for greige | Saves test results for raw woven fabric |
| LAB_CHECKRECEIVESAMPLING | Upstream | Receives finished samples into lab |

---

## UI Integration

### Primary Pages

- **Mass Production Test Approval Page**: Final approval after all tests complete
- **Sample Status Dashboard**: Monitor test results and shipment approval
- **Quality Review Page**: Supervisor approval workflow

### Typical Workflow in UI

```
1. Technician completes all customer-specified tests
2. Test data validated against customer specifications
3. Supervisor reviews all test results and trend charts
4. Supervisor makes final approval decision
5. UI calls LAB_SAVELABMASSPRORESULT with PASS/FAIL/PENDING
6. If PASS: Sample approved for packing and shipment
7. If FAIL: UI prompts for disposition (rework/scrap/downgrade)
8. Production status updated in real-time across system
```

---

## Database Schema

### Primary Table(s)
- `LAB_MASSPRO_TEST_RESULT` or `LAB_MASSPRO_SAMPLE` (mass production test results)
- `LAB_SAMPLE_APPROVAL` (approval workflow tracking)

### Key Relationships
- Links to `WEAVING_LOT_MASTER` via WEAVINGLOT
- Links to `FINISHING_LOT_MASTER` via FINISHINGLOT
- Links to `ITEM_MASTER` via ITMCODE
- May trigger updates in `INSPECTION_STATUS` or `PACKING_APPROVAL` tables

### Typical UPDATE Statement
```sql
UPDATE LAB_MASSPRO_TEST_RESULT
SET TESTRESULT = P_TESTRESULT,
    APPROVEDATE = SYSDATE,
    UPDATEDATE = SYSDATE
WHERE WEAVINGLOT = P_WEAVELOT
  AND ITM_CODE = P_ITMCODE
  AND FINISHINGLOT = P_FINISHINGLOT;
```

---

## Notes

- **Critical Quality Gate**: This is the final approval before customer shipment
- **Customer Specifications**: Tests must meet exact customer requirements (tolerance, min/max values)
- **Traceability**: Test results linked to specific production lots for customer audit trail
- **Certification**: Many customers require lab test certificates with each shipment
- **SPC Integration**: Test results may feed Statistical Process Control (SPC) charts
- **Shipment Blocking**: FAIL or PENDING status blocks lot from packing and shipping
- **Quality Costs**: Failed lots incur significant costs (rework, scrap, customer penalties)
- **Audit Trail**: All test approvals logged for ISO/TS quality system compliance

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 3286-3304)
**Implementation File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs` (lines 926-955)
