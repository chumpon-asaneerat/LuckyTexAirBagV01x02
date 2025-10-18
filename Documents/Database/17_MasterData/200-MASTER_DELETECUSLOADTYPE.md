# MASTER_DELETECUSLOADTYPE

**Procedure Number**: 200 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete customer-loading type mapping |
| **Operation** | DELETE |
| **Tables** | Customer-loading type mapping table |
| **Called From** | CustomerAndLoadingTypeDataService.cs |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_CUSTYPE` | VARCHAR2(50) | ✅ | Customer type code |
| `P_LOADTYPE` | VARCHAR2(50) | ✅ | Loading type code to remove |

### Output (OUT)

None (void procedure)

---

## Business Logic (What it does and why)

Removes mapping between customer type and loading type. When a loading method is no longer valid for a customer type (discontinued packing method, customer requirement change), this procedure deletes the relationship.

The procedure:
1. Validates customer type and loading type exist
2. Checks mapping exists
3. Verifies no active orders/shipments use this mapping (referential integrity)
4. Deletes the mapping record
5. May fail if mapping is in use by existing data

---

## Related Procedures

**Upstream**:
- [198-MASTER_GETLOADINGBYCUSTYPE.md](./198-MASTER_GETLOADINGBYCUSTYPE.md) - View mappings to delete

**Related**:
- [199-MASTER_EDITCUSLOADTYPE.md](./199-MASTER_EDITCUSLOADTYPE.md) - Create/edit mapping

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CustomerAndLoadingTypeDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `MASTER_DELETECUSLOADTYPE(MASTER_DELETECUSLOADTYPEParameter para)`
**Lines**: 17490-17517

---

**File**: 200/296 | **Progress**: 67.6%
