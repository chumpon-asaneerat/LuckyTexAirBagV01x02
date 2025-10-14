# INSERTUPDATEFINISHINGDATA

**Procedure Number**: 086 | **Module**: M08 - Inspection | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert or update finishing production data with lot and length information |
| **Operation** | INSERT/UPDATE |
| **Called From** | FinishingDataService.cs:640 → SaveFinishingData() |
| **Frequency** | High (Production recording) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2 | ✅ | Item grey code (validated in C# - trimmed) |
| `P_FINISHINGLOT` | VARCHAR2 | ✅ | Finishing lot number (validated in C# - trimmed) |
| `P_LENGHT` | NUMBER | ✅ | Fabric length in meters - note typo "LENGHT" |
| `P_PARTNO` | VARCHAR2 | ⬜ | Part number (nullable, trimmed if provided) |
| `P_CUSID` | VARCHAR2 | ⬜ | Customer ID (currently set to NULL in C#) |

### Output

**Returns**: Boolean (true = success, false = error)

---

## Business Logic

Saves finishing production data, either inserting a new record or updating an existing one based on finishing lot number. Used after finishing operations (coating, scouring, drying) to record production output. This procedure is critical for:

1. **Production Recording**: Record completed finishing lots with length
2. **Inventory Management**: Track finished goods quantities
3. **Inspection Integration**: Called by inspection module to record finished fabric data
4. **Traceability**: Link finished lots to original grey fabric items
5. **Customer Orders**: Track production against customer orders (via CUSID)

**Business Rules**:
- **Two mandatory parameters** validated in C#:
  - P_ITEMCODE - item grey code must not be blank
  - P_FINISHINGLOT - finishing lot number must not be blank
- All string parameters are **trimmed** before passing to database
- P_PARTNO is nullable - set to NULL if empty/whitespace
- **P_CUSID currently always NULL** (comment in C# indicates this is temporary)
- If finishing lot exists → UPDATE record
- If finishing lot doesn't exist → INSERT new record
- Returns boolean: true if operation succeeds, false if validation fails or database error

**Validation in C#**:
```csharp
if (string.IsNullOrWhiteSpace(itemCode))
    return result;  // false if item code not specified
if (string.IsNullOrWhiteSpace(finishingLotNo))
    return result;  // false if finishing lot not specified
```

**Data Preparation**:
```csharp
dbPara.P_ITEMCODE = itemCode.Trim();
dbPara.P_FINISHINGLOT = finishingLotNo.Trim();
dbPara.P_PARTNO = (string.IsNullOrWhiteSpace(partNo)) ? null : partNo.Trim();
dbPara.P_CUSID = null;  // Set NULL for now (comment in Thai suggests this is temporary)
```

**Typical Usage Scenarios**:

1. **After Finishing Process Completion**:
   - Fabric completes coating/scouring/drying cycle
   - Operator measures final length
   - System calls INSERTUPDATEFINISHINGDATA to record output
   - Item: "GREY-AB100" → Finishing Lot: "FN-2024-001", Length: 450m

2. **Inspection Recording**:
   - Inspector verifies finished fabric quality
   - Records actual usable length (may differ from production length due to defects)
   - Updates finishing record with corrected length

3. **Data Correction**:
   - Supervisor discovers incorrect length or item code
   - Calls procedure again with corrected data
   - Existing record is updated

**Note**: Comment in C# (line 659) says "ส่งค่า Null เข้าไปก่อนตอนนี้" which means "Send NULL value for now" - suggesting P_CUSID functionality may be added in future.

---

## Related Procedures

**Related**:
- FINISHING_INSERTCOATING - Insert coating data
- FINISHING_INSERTSCOURING - Insert scouring data
- FINISHING_INSERTDRYER - Insert dryer data
- INSERTINSPECTIONPROCESS (FinishingDataService) - Insert inspection records

**Similar**:
- [085-INSERTUPDATEWEAVINGDATA.md](./085-INSERTUPDATEWEAVINGDATA.md) - Similar pattern for weaving module

**Upstream**:
- Finishing module procedures (coating/scouring/dryer) - Create data that feeds into this
- Weaving module - Source of grey fabric (P_ITEMCODE)

**Downstream**:
- Inspection procedures - Use finishing data for quality checks
- Packing procedures - Use finishing data for shipment preparation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\FinishingDataService.cs`
**Method**: `SaveFinishingData()`
**Lines**: 640-677

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INSERTUPDATEFINISHINGDATA(INSERTUPDATEFINISHINGDATAParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 086/296 | **Progress**: 29.1%
