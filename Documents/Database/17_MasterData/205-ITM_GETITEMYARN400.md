# ITM_GETITEMYARN400

**Procedure Number**: 205 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get yarn item codes from AS400 ERP system |
| **Operation** | SELECT |
| **Tables** | AS400 integration table for yarn items |
| **Called From** | G3DataService.cs (Warehouse) |
| **Frequency** | Medium |
| **Performance** | Medium (external system query) |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMYARN` | VARCHAR2(50) | ⬜ | Yarn item code filter (optional) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_YARN` | VARCHAR2(50) | Yarn item code from AS400 |

---

## Business Logic (What it does and why)

Retrieves yarn item codes from AS400 legacy ERP system integration. The company migrated from AS400 to D365, but this procedure maintains compatibility with AS400 data for historical records and transition period. Used in warehouse module (G3) when receiving yarn shipments that were ordered through the old AS400 system.

The procedure:
1. Queries AS400 integration table
2. Filters by yarn item code if provided (or returns all)
3. Returns yarn item codes from legacy system
4. Enables lookup/validation of AS400 item codes during transition

**Note**: This is part of dual-system integration during AS400→D365 migration period.

---

## Related Procedures

**Similar**:
- [204-ITM_GETITEMYARNLIST.md](./204-ITM_GETITEMYARNLIST.md) - Get yarn items from current system
- G3_GETDATAAS400 - Get warehouse data from AS400
- G3_GETDATAD365 - Get warehouse data from D365

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_GETITEMYARN400(ITM_GETITEMYARN400Parameter para)`
**Lines**: 22117-22154

---

**File**: 205/296 | **Progress**: 69.3%
