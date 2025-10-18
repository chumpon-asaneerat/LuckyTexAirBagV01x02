# ITM_GETITEMYARNLIST

**Procedure Number**: 204 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get item codes with yarn composition specifications |
| **Operation** | SELECT |
| **Tables** | Item yarn master table |
| **Called From** | Multiple services (warehouse, warping, production modules) |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

None - Returns all yarn items

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |

---

## Business Logic (What it does and why)

Retrieves list of item codes with their yarn composition and material specifications. This procedure provides items that have complete yarn material definitions, crucial for material issuing, warping setup, and production planning. Only items with properly configured yarn specifications can be produced.

The procedure:
1. Queries items with yarn composition configured
2. Returns item codes with yarn type details
3. Ensures items have complete material specifications
4. Used for validating items before material issuing

Used in warehouse module (G3) for yarn issuing and in warping module (M02) for creel setup validation.

---

## Related Procedures

**Similar**:
- [202-ITM_GETITEMCODELIST.md](./202-ITM_GETITEMCODELIST.md) - Get all item codes
- [203-ITM_GETITEMPREPARELIST.md](./203-ITM_GETITEMPREPARELIST.md) - Get items with prepare specs
- [205-ITM_GETITEMYARN400.md](./205-ITM_GETITEMYARN400.md) - Get items from AS400 integration

**Downstream**: Used for material planning and yarn issuing workflows

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: Multiple services use this procedure
**Method**: To be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_GETITEMYARNLIST(ITM_GETITEMYARNLISTParameter para)`
**Lines**: 22073-22112

---

**File**: 204/296 | **Progress**: 68.9%
