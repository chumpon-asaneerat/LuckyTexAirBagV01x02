# INSERTUPDATEWEAVINGDATA

**Procedure Number**: 085 | **Module**: M08 - Inspection | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert or update weaving production data with new/corrected lot information |
| **Operation** | INSERT/UPDATE |
| **Called From** | WeavingDataService.cs:1328 → INSERTUPDATEWEAVINGDATA() |
| **Frequency** | Medium (Data corrections) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOTNEW` | VARCHAR2 | ✅ | New weaving lot number (validated in C#) |
| `P_WEAVINGLOTOLD` | VARCHAR2 | ✅ | Old weaving lot number to update (validated in C#) |
| `P_ITEMWEAVING` | VARCHAR2 | ✅ | Weaving item code (validated in C#) |
| `P_LENGHT` | NUMBER | ⬜ | Fabric length (meters) - note typo "LENGHT" |
| `P_WEAVINGDATE` | DATE | ⬜ | Weaving production date |
| `P_LOOM` | VARCHAR2 | ⬜ | Loom machine number |
| `P_SHIFT` | VARCHAR2 | ⬜ | Production shift |
| `P_WIDTH` | NUMBER | ⬜ | Fabric width (cm) |
| `P_REMARK` | VARCHAR2 | ⬜ | Remarks |
| `P_OPERATOR` | VARCHAR2 | ⬜ | Operator performing the update |

### Output

**Returns**: Boolean (true = success, false = error)

---

## Business Logic

Inserts new weaving production record or updates existing record, primarily used for data corrections and lot number changes. This procedure is critical for:

1. **Lot Number Correction**: Change weaving lot number if original was incorrect
2. **Data Correction**: Update production data (length, width, date, shift)
3. **Inspection Integration**: Used by inspection module to correct weaving data after quality checks
4. **Audit Trail**: Records who made the changes (P_OPERATOR)
5. **Traceability**: Maintains link between old and new lot numbers

**Business Rules**:
- **Three mandatory parameters** validated in C#:
  - P_WEAVINGLOTNEW - must not be blank
  - P_WEAVINGLOTOLD - must not be blank
  - P_ITEMWEAVING - must not be blank
- If OLD lot exists → UPDATE record with NEW lot number and other data
- If OLD lot doesn't exist → INSERT new record with NEW lot number
- Other parameters are optional (only provided values are updated/inserted)
- Returns false if validation fails or database error occurs

**Validation in C#**:
```csharp
if (string.IsNullOrWhiteSpace(P_WEAVINGLOTNEW))
    return result;  // false if new lot not specified
if (string.IsNullOrWhiteSpace(P_WEAVINGLOTOLD))
    return result;  // false if old lot not specified
if (string.IsNullOrWhiteSpace(P_ITEMWEAVING))
    return result;  // false if item code not specified
```

**Typical Usage Scenarios**:

1. **Lot Number Correction After Inspection**:
   - Inspector finds weaving lot mislabeled
   - System calls INSERTUPDATEWEAVINGDATA with correct lot number
   - OLD lot: "WV-2024-001-WRONG"
   - NEW lot: "WV-2024-001-CORRECT"
   - Updates all production data under new lot number

2. **Data Correction**:
   - Operator discovers incorrect length or width entered
   - Supervisor authorizes correction
   - Procedure updates record with corrected values

3. **Consolidation**:
   - Multiple partial weaving records need to be consolidated
   - System merges data under single correct lot number

**Note**: The OLD and NEW lot parameter names suggest this is primarily for renaming/correcting lot numbers while preserving all other production data.

---

## Related Procedures

**Related**:
- [062-WEAVE_WEAVINGPROCESS.md](../05_Weaving/062-WEAVE_WEAVINGPROCESS.md) - Initial weaving lot creation
- [081-WEAV_UPDATEWEAVINGLOT.md](../05_Weaving/081-WEAV_UPDATEWEAVINGLOT.md) - Update weaving lot data
- [065-WEAV_DELETEWEAVINGLOT.md](../05_Weaving/065-WEAV_DELETEWEAVINGLOT.md) - Delete weaving lot

**Upstream**:
- Inspection procedures - Call this to correct weaving data after inspection
- Quality assurance workflows - Data corrections after QA review

**Similar**:
- INSERTUPDATEFINISHINGDATA (FinishingDataService) - Similar pattern for finishing module

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `INSERTUPDATEWEAVINGDATA()`
**Lines**: 1328-1372

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INSERTUPDATEWEAVINGDATA(INSERTUPDATEWEAVINGDATAParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 085/296 | **Progress**: 28.7%
