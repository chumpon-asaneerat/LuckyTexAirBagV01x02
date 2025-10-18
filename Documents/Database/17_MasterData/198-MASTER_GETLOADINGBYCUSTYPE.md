# MASTER_GETLOADINGBYCUSTYPE

**Procedure Number**: 198 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get loading types configured for specific customer type |
| **Operation** | SELECT |
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

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `LOADINGTYPE` | VARCHAR2(50) | Loading type code/name for this customer |

---

## Business Logic (What it does and why)

Retrieves all loading types configured for a specific customer type. Different customers may require different loading/packing methods (pallets, boxes, special containers). This procedure returns which loading options are valid for a selected customer type, used when configuring customer-specific shipping requirements.

The procedure:
1. Takes customer type code as input
2. Looks up customer-loading type mappings
3. Returns all valid loading types for that customer
4. Used for dependent dropdown lists (select customer → see valid loading types)

---

## Related Procedures

**Upstream**:
- [197-MASTER_CUSTOMERTYPELIST.md](./197-MASTER_CUSTOMERTYPELIST.md) - Get customer types list

**Downstream**:
- [199-MASTER_EDITCUSLOADTYPE.md](./199-MASTER_EDITCUSLOADTYPE.md) - Edit mapping
- [200-MASTER_DELETECUSLOADTYPE.md](./200-MASTER_DELETECUSLOADTYPE.md) - Delete mapping

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CustomerAndLoadingTypeDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `MASTER_GETLOADINGBYCUSTYPE(MASTER_GETLOADINGBYCUSTYPEParameter para)`
**Lines**: 17408-17449

---

**File**: 198/296 | **Progress**: 66.9%
