# MASTER_CUSTOMERTYPELIST

**Procedure Number**: 197 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get list of all customer types for master data management |
| **Operation** | SELECT |
| **Tables** | Customer type master table |
| **Called From** | CustomerAndLoadingTypeDataService.cs |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

None - Returns all customer types

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CUSTOMERTYPE` | VARCHAR2(50) | Customer type code/name |

---

## Business Logic (What it does and why)

Retrieves complete list of customer types configured in the system. Used for populating dropdown lists in master data management screens where administrators configure customer-specific settings like loading types, item specifications, or quality standards.

The procedure:
1. Queries all active customer types
2. Returns simple list of customer type codes
3. Used for UI dropdown population
4. No filtering - returns all types

---

## Related Procedures

**Downstream**:
- [198-MASTER_GETLOADINGBYCUSTYPE.md](./198-MASTER_GETLOADINGBYCUSTYPE.md) - Get loading types for specific customer
- [199-MASTER_EDITCUSLOADTYPE.md](./199-MASTER_EDITCUSLOADTYPE.md) - Edit customer loading type mapping

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CustomerAndLoadingTypeDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `MASTER_CUSTOMERTYPELIST(MASTER_CUSTOMERTYPELISTParameter para)`
**Lines**: 17522-17560

---

**File**: 197/296 | **Progress**: 66.6%
