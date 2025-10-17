# PACK_INSERTPACKINGPALLET

**Procedure Number**: 290 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new packing pallet and generate pallet number |
| **Operation** | INSERT |
| **Tables** | tblPackingPallet (assumed) |
| **Called From** | PackingDataService.cs:314 → PACK_INSERTPACKINGPALLET() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID creating the pallet |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_PALLETNO` | VARCHAR2(50) | Generated packing pallet number |

### Returns (if cursor)

No cursor return - Returns output parameter

---

## Business Logic (What it does and why)

Creates a new empty packing pallet and generates a unique pallet number. This is the first step in the packing workflow before adding inspection lots.

**Workflow**:
1. Receives operator ID who is creating the pallet
2. Generates new unique packing pallet number (auto-increment or sequence)
3. Creates pallet header record with:
   - Generated pallet number
   - Operator ID
   - Creation timestamp
   - Initial status (empty/in-progress)
4. Returns the generated pallet number for subsequent operations

**Business Rules**:
- Operator ID is mandatory
- Pallet number must be unique (auto-generated)
- Pallet created in empty state
- Operator logged for traceability
- Timestamp recorded for audit trail

**Pallet Number Format** (typical):
- Pattern: `PK-YYMMDD-NNNN` or similar
- Auto-incremented sequence number
- Date-based for easy tracking
- Unique across all pallets

**Workflow Integration**:
1. **PACK_INSERTPACKINGPALLET** → Creates empty pallet, returns pallet number
2. **PACK_INSPACKINGPALLETDETAIL** → Adds inspection lots to pallet (multiple calls)
3. **PACK_PRINTLABEL** → Prints packing labels
4. **PACK_UPDATEPACKINGPALLET** → Finalizes pallet for shipping

**Return Value**:
```csharp
string palletNo = PACK_INSERTPACKINGPALLET(operatorId);
if (!string.IsNullOrEmpty(palletNo))
{
    // Pallet created successfully
    // Use palletNo for adding lots: PACK_INSPACKINGPALLETDETAIL(palletNo, ...)
}
else
{
    // Creation failed
}
```

---

## Related Procedures

**Downstream**: PACK_INSPACKINGPALLETDETAIL - Adds inspection lots to created pallet
**Downstream**: [289-PACK_GETPALLETDETAIL.md](./289-PACK_GETPALLETDETAIL.md) - Gets pallet details
**Related**: [286-PACK_CANCELPALLET.md](./286-PACK_CANCELPALLET.md) - Cancels created pallet

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_INSERTPACKINGPALLET()`
**Lines**: 300-323

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_INSERTPACKINGPALLET(PACK_INSERTPACKINGPALLETParameter para)`
**Lines**: 2272-2285

**Implementation**:
```csharp
public string PACK_INSERTPACKINGPALLET(string OPERATOR)
{
    if (string.IsNullOrWhiteSpace(OPERATOR))
        return string.Empty;

    if (!HasConnection())
        return string.Empty;

    PACK_INSERTPACKINGPALLETParameter dbPara = new PACK_INSERTPACKINGPALLETParameter();
    dbPara.P_OPERATOR = OPERATOR;

    PACK_INSERTPACKINGPALLETResult dbResult = null;
    try
    {
        dbResult = DatabaseManager.Instance.PACK_INSERTPACKINGPALLET(dbPara);
        return dbResult.R_PALLETNO;  // Returns generated pallet number
    }
    catch (Exception ex)
    {
        ex.Err();
        return string.Empty;  // Empty = creation failed
    }
}
```

**Database Side** (Oracle stored procedure):
```sql
-- Typical implementation:
CREATE OR REPLACE PROCEDURE PACK_INSERTPACKINGPALLET(
    P_OPERATOR IN VARCHAR2,
    R_PALLETNO OUT VARCHAR2
)
IS
BEGIN
    -- Generate unique pallet number
    SELECT 'PK-' || TO_CHAR(SYSDATE,'YYMMDD') || '-' || LPAD(SEQ_PALLET.NEXTVAL,4,'0')
    INTO R_PALLETNO
    FROM DUAL;

    -- Insert pallet header
    INSERT INTO tblPackingPallet (
        PALLETNO, CREATEBY, CREATEDATE, STATUS
    ) VALUES (
        R_PALLETNO, P_OPERATOR, SYSDATE, 'NEW'
    );

    COMMIT;
END;
```

---

**File**: 290/296 | **Progress**: 98.0%
