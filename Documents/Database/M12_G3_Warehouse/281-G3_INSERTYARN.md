# G3_INSERTYARN

**Procedure Number**: 281 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert new yarn/material pallet receiving record |
| **Operation** | INSERT |
| **Tables** | tblYarnStock (assumed) |
| **Called From** | G3DataService.cs:94 → G3_InsertYarn() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟠 1 Medium - Method commented out, parameters not passed |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_TRUCKNO` | VARCHAR2(50) | ✅ | Truck delivery number (primary key) |
| `P_DESC` | VARCHAR2(500) | ✅ | Description/remarks (primary key) |
| `P_PATTETNO` | VARCHAR2(50) | ⬜ | Pallet number for yarn/material |
| `P_CH` | NUMBER | ⬜ | Cone/cheese quantity |
| `P_WEIGHT` | NUMBER | ⬜ | Weight (kg) |
| `P_LOTORDERNO` | VARCHAR2(50) | ⬜ | Lot/order number from supplier |
| `P_ITMORDER` | VARCHAR2(50) | ⬜ | Item code from order |
| `P_RECEIVEDATE` | VARCHAR2(20) | ⬜ | Receiving date (string format) |
| `P_UM` | VARCHAR2(10) | ⬜ | Unit of measure |
| `P_ITMYARN` | VARCHAR2(50) | ⬜ | Yarn/material item code |
| `P_TYPE` | VARCHAR2(10) | ⬜ | Material type (YARN/SILICONE/etc) |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - INSERT operation only

---

## Business Logic (What it does and why)

Records initial yarn/material pallet receiving into warehouse system. When a delivery truck arrives with yarn or silicone materials, this procedure creates the first receiving record before QC inspection.

**Workflow**:
1. Receives truck delivery information (truck number, description)
2. Creates pallet record with material details (item code, weight, cone count)
3. Records receiving date and lot/order reference
4. Stores material type for inventory classification

**Business Rules**:
- Truck number and description are mandatory (primary key)
- Called before G3_RECEIVEYARN (which performs QC inspection)
- Does not update stock quantities (done in G3_RECEIVEYARN after QC approval)

**Note**: Current implementation has all parameters commented out in C# code (lines 96-109), suggesting this procedure may be deprecated or under revision. Only connection validation is active.

---

## Related Procedures

**Downstream**: [282-G3_RECEIVEYARN.md](./282-G3_RECEIVEYARN.md) - Called after this to perform QC inspection and approve receiving
**Similar**: G3_INSERTUPDATEISSUEYARN - Similar insert logic for yarn issuing

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_InsertYarn()`
**Lines**: 88-127

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_INSERTYARN(G3_INSERTYARNParameter para)`
**Lines**: 6816-6838

**Implementation Issue**: All parameter assignments are commented out (lines 96-109). Method only validates connection and calls stored procedure with empty parameters.

```csharp
// Current implementation (all parameters commented):
G3_INSERTYARNParameter dbPara = new G3_INSERTYARNParameter();
// All assignments commented out
dbResult = DatabaseManager.Instance.G3_INSERTYARN(dbPara);
```

---

**File**: 281/296 | **Progress**: 94.9%
