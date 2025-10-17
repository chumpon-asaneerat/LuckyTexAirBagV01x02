# LAB_SAVELABGREIGERESULT

**Procedure Name**: `LAB_SAVELABGREIGERESULT`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Saves or updates greige (raw woven fabric) test result approval status
**Return Type**: Success/Failure indicator (empty result on success)

---

## Description

Updates the test result approval status for greige (unfinished woven) fabric samples after laboratory testing is complete. This procedure records the final test result (PASS/FAIL) for a specific greige sample identified by beam roll, loom, and weaving item code. Typically called after all individual tests are completed and a technician or supervisor makes the final approval decision.

This is a lightweight status update procedure - it does not store detailed test measurements (those are saved separately via individual test procedures like LAB_INSERTUPDATETENSILE, etc.). It only updates the overall test result flag for the sample.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_BEAMERROLL | VARCHAR2 | Yes | Beam roll identifier (source beam from beaming process) |
| P_LOOM | VARCHAR2 | Yes | Loom machine number that produced the greige fabric |
| P_ITMWEAVE | VARCHAR2 | Yes | Weaving item code (greige fabric product code) |
| P_TESTRESULT | VARCHAR2 | Yes | Overall test result (PASS/FAIL/PENDING) |
| P_TESTNO | NUMBER | Yes | Test sequence number (for retest tracking) |

**Notes**:
- All parameters are required
- P_BEAMERROLL + P_LOOM + P_ITMWEAVE form unique identifier for greige sample
- P_TESTRESULT typical values: "PASS", "FAIL", "PENDING"
- P_TESTNO increments for retests (1 = first test, 2 = first retest, etc.)

---

## Result Set

### Output Columns

No output columns returned. Success indicated by non-null result object; failure indicated by null result or exception.

**Notes**:
- Empty result class (LAB_SAVELABGREIGERESULTResult has no properties)
- Check for null return value to determine success/failure
- Exceptions thrown for database errors or constraint violations

---

## Business Logic

### Test Result Workflow

1. **Sample Received**: Greige sample arrives in lab from weaving floor
2. **Individual Tests Performed**: Technician runs all required tests (tensile, tear, weight, etc.)
3. **Results Entered**: Each test result saved via specific test procedures
4. **Final Approval**: Supervisor reviews all tests and calls this procedure with overall result
5. **Status Updated**: Sample marked as PASS or FAIL in database

### Retest Handling

The P_TESTNO parameter supports retesting workflow:
- **Test #1**: Initial testing (P_TESTNO = 1)
- **Test #2**: First retest if initial test failed (P_TESTNO = 2)
- **Test #3+**: Additional retests as needed

Each retest creates a new test record with incremented test number.

### Greige vs Mass Production

This procedure is specifically for greige (raw woven) fabric testing:
- **Greige**: Uses BEAMERROLL + LOOM identifier
- **Mass Production**: Uses WEAVINGLOT + FINISHINGLOT identifier (see LAB_SAVELABMASSPRORESULT)

---

## Usage Example

### C# Method Call

### Common Usage Patterns

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_GREIGESTOCKSTATUS | Status query | Gets current status of greige samples |
| LAB_GETWEAVINGSAMPLING | Detail retrieval | Gets greige sample details for testing |
| LAB_INSERTUPDATETENSILE | Test data entry | Saves tensile test measurements |
| LAB_SAVELABMASSPRORESULT | Similar for mass production | Saves test results for finished fabric |
| LAB_CHECKRECEIVEGREIGESAMPLING | Upstream | Receives greige samples into lab |

---

## UI Integration

### Primary Pages

- **Greige Test Approval Page**: Final approval after all tests complete
- **Greige Sample Status Page**: Monitor test results and approval status
- **Retest Management Page**: Handle failed samples requiring retesting

### Typical Workflow in UI

---

## Database Schema

### Primary Table(s)
- `LAB_GREIGE_TEST_RESULT` or `LAB_GREIGE_SAMPLE` (greige sample test results)

### Key Relationships
- Links to `BEAMING_DETAIL` via BEAMERROLL
- Links to `WEAVING_PRODUCTION` via LOOM
- Links to `ITEM_MASTER` via ITMWEAVE
- May update status in `LAB_GREIGE_STOCK` table

### Typical UPDATE Statement
---

## Notes

- Greige testing occurs before coating/finishing process
- Critical quality gate - failed greige cannot proceed to finishing
- Test results affect production planning (rework vs scrap decisions)
- Some customers require greige certification in addition to finished product tests
- Retests may require 24-hour conditioning period to restart
- Test number tracking ensures audit trail for quality compliance

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 3306-3325)
**Implementation File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs` (lines 884-914)
