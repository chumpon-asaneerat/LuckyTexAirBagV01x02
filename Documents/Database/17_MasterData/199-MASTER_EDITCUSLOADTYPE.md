# MASTER_EDITCUSLOADTYPE

**Procedure Number**: 199 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert or update customer-loading type mapping |
| **Operation** | INSERT/UPDATE |
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
| `P_LOADTYPE` | VARCHAR2(50) | ✅ | Loading type code to map |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Creates or updates mapping between customer type and loading type. When administrator configures which loading methods are valid for a customer type (e.g., "Automotive customers use pallet type A"), this procedure saves the mapping.

The procedure:
1. Validates customer type exists
2. Validates loading type exists
3. Checks if mapping already exists (UPDATE) or new (INSERT)
4. Saves the mapping relationship
5. Returns success or error status

---

## Related Procedures

**Upstream**:
- [197-MASTER_CUSTOMERTYPELIST.md](./197-MASTER_CUSTOMERTYPELIST.md) - Get customer types
- [198-MASTER_GETLOADINGBYCUSTYPE.md](./198-MASTER_GETLOADINGBYCUSTYPE.md) - View existing mappings

**Related**:
- [200-MASTER_DELETECUSLOADTYPE.md](./200-MASTER_DELETECUSLOADTYPE.md) - Delete mapping

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CustomerAndLoadingTypeDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `MASTER_EDITCUSLOADTYPE(MASTER_EDITCUSLOADTYPEParameter para)`
**Lines**: 17454-17484

---

**File**: 199/296 | **Progress**: 67.2%
