# LAB_SAVELABRESULT

**Procedure Name**: `LAB_SAVELABRESULT`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Saves generic lab test result for inspection lot
**Return Type**: Success/Failure indicator (empty result on success)

---

## Description

Saves a generic test result for an inspection lot. This appears to be a simplified or legacy procedure for saving basic pass/fail results without the detailed sample identification required by the greige and mass production procedures. The procedure uses an inspection lot identifier (INSLOT) rather than the specific lot identifiers (weaving lot, finishing lot, beam roll, etc.) used in other test result procedures.

This may be used for quick quality checks, incoming material inspection, or simplified testing scenarios that don't require the full traceability chain of the main production testing procedures.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_INSLOT | VARCHAR2 | Yes | Inspection lot identifier or sample ID |
| P_RESULT | VARCHAR2 | Yes | Test result (PASS/FAIL or other status codes) |

**Notes**:
- Both parameters are required
- P_INSLOT is a generic identifier - could be inspection lot, sample barcode, or other reference
- P_RESULT format depends on usage context (PASS/FAIL, OK/NG, or numeric values)
- Simpler parameter set compared to LAB_SAVELABGREIGERESULT or LAB_SAVELABMASSPRORESULT

---

## Result Set

### Output Columns

No output columns returned. Success indicated by non-null result object; failure indicated by null result or exception.

**Notes**:
- Empty result class (LAB_SAVELABRESULTResult has no properties)
- Check for null return value to determine success/failure
- Exceptions thrown for database errors or constraint violations

---

## Business Logic

### Generic Test Result Recording

This procedure provides a simplified interface for recording test results:
- **Less Specific**: Uses generic inspection lot ID instead of detailed lot traceability
- **Flexible**: Can be used for various inspection scenarios
- **Lightweight**: Minimal parameters for quick data entry

### Possible Use Cases

1. **Incoming Material Inspection**: Quick accept/reject for received materials
2. **In-Process Checks**: Spot checks during production
3. **Visual Inspection**: Simple pass/fail without detailed measurements
4. **Legacy System**: May be older interface before detailed test procedures were implemented

---

## Usage Example

### C# Method Call

```csharp
// From AirbagSPs.cs (lines 18643-18671)
public LAB_SAVELABRESULTResult LAB_SAVELABRESULT(LAB_SAVELABRESULTParameter para)
{
    LAB_SAVELABRESULTResult result = null;
    if (!HasConnection())
        return result;

    string[] paraNames = new string[]
    {
        "P_INSLOT",
        "P_RESULT"
    };
    object[] paraValues = new object[]
    {
        para.P_INSLOT,
        para.P_RESULT
    };

    ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
        "LAB_SAVELABRESULT",
        paraNames, paraValues);
    if (null != ret && !ret.HasException)
    {
        result = new LAB_SAVELABRESULTResult();
    }

    return result;
}
```

### Common Usage Patterns

```csharp
// Save inspection result for an inspection lot
var param = new LAB_SAVELABRESULTParameter
{
    P_INSLOT = "INS-20231015-001",
    P_RESULT = "PASS"
};
var result = DatabaseManager.Instance.LAB_SAVELABRESULT(param);
bool success = (result != null);

// Save failed inspection
var param = new LAB_SAVELABRESULTParameter
{
    P_INSLOT = "INS-20231015-002",
    P_RESULT = "FAIL"
};
var result = DatabaseManager.Instance.LAB_SAVELABRESULT(param);

// Save numeric result
var param = new LAB_SAVELABRESULTParameter
{
    P_INSLOT = "SAMPLE-12345",
    P_RESULT = "95.5"  // Could be percentage or measurement
};
var result = DatabaseManager.Instance.LAB_SAVELABRESULT(param);
```

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_SAVELABGREIGERESULT | Specific greige | Saves greige fabric test results with detailed identifiers |
| LAB_SAVELABMASSPRORESULT | Specific mass pro | Saves finished fabric test results with detailed identifiers |
| LAB_SEARCHLABENTRYPRODUCTION | Data retrieval | Searches lab test entry records |
| LAB_SEARCHAPPROVELAB | Approval search | Searches lab approval records |

---

## UI Integration

### Potential Pages

- **Generic Inspection Entry Page**: Simple pass/fail data entry
- **Incoming Material Inspection**: Quick accept/reject interface
- **Mobile Inspection App**: Simplified interface for handheld devices

### Typical Workflow in UI

```
1. Inspector scans or enters inspection lot barcode
2. Inspector performs visual or simple test
3. Inspector selects PASS or FAIL
4. UI calls LAB_SAVELABRESULT with lot ID and result
5. Result recorded immediately in database
6. Inspector moves to next inspection item
```

---

## Database Schema

### Primary Table(s)
- `LAB_INSPECTION_RESULT` or `LAB_GENERIC_TEST` (generic test results)
- `INSPECTION_LOT` (inspection lot master)

### Key Relationships
- Links to `INSPECTION_LOT` table via INSLOT
- May link to various source tables depending on inspection type

### Typical UPDATE or INSERT Statement
```sql
-- Update existing inspection result
UPDATE LAB_INSPECTION_RESULT
SET RESULT = P_RESULT,
    UPDATEDATE = SYSDATE
WHERE INSLOT = P_INSLOT;

-- Or insert new result if not exists
INSERT INTO LAB_INSPECTION_RESULT
(INSLOT, RESULT, CREATEDATE)
VALUES (P_INSLOT, P_RESULT, SYSDATE);
```

---

## Notes

- **Simplified Interface**: Much simpler than detailed greige/mass production procedures
- **Generic Usage**: Flexible enough for various inspection scenarios
- **Limited Traceability**: Does not capture detailed lot lineage like other procedures
- **Legacy Consideration**: May be older interface maintained for backward compatibility
- **Inspection Lot Format**: P_INSLOT format depends on inspection type and business rules
- **Result Values**: P_RESULT interpretation depends on context (PASS/FAIL, OK/NG, numeric, etc.)
- **Usage Frequency**: Likely used less frequently than specific test result procedures
- **No DataService Wrapper**: Unlike other LAB procedures, no dedicated method found in LABDataService.cs (may be called directly or through base class)

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 3268-3284)
**Implementation File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 18643-18671)
