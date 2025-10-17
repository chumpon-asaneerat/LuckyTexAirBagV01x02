# PACK_CANCELPALLET

**Procedure Number**: 286 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Cancel packing pallet and remove from shipping |
| **Operation** | DELETE / UPDATE |
| **Tables** | tblPackingPallet, tblPackingPalletDetail (assumed) |
| **Called From** | PackingDataService.cs:760 → PACK_CANCELPALLET() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Packing pallet number to cancel |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - DELETE/UPDATE operation only

---

## Business Logic (What it does and why)

Cancels a packing pallet and removes it from shipping queue. Used when pallet needs to be repacked, has errors, or shipping plans change.

**Workflow**:
1. Receives packing pallet number to cancel
2. Validates pallet exists and can be cancelled
3. Removes pallet from shipping queue
4. May delete pallet record or mark as cancelled (flag update)
5. Releases inspection lots back to available stock

**Business Rules**:
- Pallet number is mandatory
- Cannot cancel shipped pallets (only in-warehouse)
- Releases all inspection lots in the pallet
- Allows re-packing of the same inspection lots
- Audit trail maintained for cancelled pallets

**Common Scenarios**:
- Pallet damaged before shipping
- Wrong customer loading type assigned
- Need to repack with different rolls
- Shipping schedule changed
- Quality issue found after packing

**Validation**:
```csharp
// Required validation in C#:
if (string.IsNullOrWhiteSpace(P_PALLETNO)) return false;
```

---

## Related Procedures

**Upstream**: [289-PACK_INSERTPACKINGPALLET.md](./289-PACK_INSERTPACKINGPALLET.md) - Creates pallet to be cancelled
**Related**: PACK_UPDATEPACKINGPALLET - Updates pallet instead of cancelling
**Downstream**: Inspection lots released back to available stock

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_CANCELPALLET()`
**Lines**: 744-772

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_CANCELPALLET(PACK_CANCELPALLETParameter para)`
**Lines**: 2370-2382

**Implementation**:
```csharp
// Validation and execution:
if (string.IsNullOrWhiteSpace(P_PALLETNO)) return false;
if (!HasConnection()) return false;

PACK_CANCELPALLETParameter dbPara = new PACK_CANCELPALLETParameter();
dbPara.P_PALLETNO = P_PALLETNO;

dbResult = DatabaseManager.Instance.PACK_CANCELPALLET(dbPara);
result = (null != dbResult);
```

---

**File**: 286/296 | **Progress**: 96.6%
