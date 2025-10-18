# ITM_GETITEMCODELIST

**Procedure Number**: 202 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get complete list of all item codes in system |
| **Operation** | SELECT |
| **Tables** | Item master table |
| **Called From** | Multiple services (5+ modules) |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

None - Returns all item codes

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code |

---

## Business Logic (What it does and why)

Retrieves complete list of all item codes configured in the system. This is one of the most frequently used procedures across the application, used for populating dropdown lists throughout the MES wherever operators need to select an item/product code.

The procedure:
1. Queries all active item codes from master table
2. Returns simple list of item codes
3. May include filtering for active/inactive status
4. Used extensively for UI dropdown population across all modules

**Note**: This procedure is used in at least 5 different modules, making it a critical shared component.

---

## Related Procedures

**Similar**:
- [201-ITM_GETITEMBYITEMCODEANDCUSID.md](./201-ITM_GETITEMBYITEMCODEANDCUSID.md) - Get item with customer filter
- [203-ITM_GETITEMPREPARELIST.md](./203-ITM_GETITEMPREPARELIST.md) - Get items with prepare specs
- [204-ITM_GETITEMYARNLIST.md](./204-ITM_GETITEMYARNLIST.md) - Get items with yarn details

**Downstream**: Used by nearly every production module for item selection

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: Multiple services use this procedure
**Method**: Various methods across modules
**Lines**: Multiple locations

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_GETITEMCODELIST(ITM_GETITEMCODELISTParameter para)`
**Lines**: 22208-22246

---

**File**: 202/296 | **Progress**: 68.2%
