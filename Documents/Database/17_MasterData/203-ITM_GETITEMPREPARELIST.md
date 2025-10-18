# ITM_GETITEMPREPARELIST

**Procedure Number**: 203 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get item codes with preparation specifications (warping/beaming/drawing specs) |
| **Operation** | SELECT |
| **Tables** | Item prepare master table |
| **Called From** | DrawingDataService, ProcessConditionDataService, WarpingDataService |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

None - Returns all preparation items

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_PREPARE` | VARCHAR2(50) | Preparation item code (for warping/beaming/drawing) |
| `ITM_YARN` | VARCHAR2(50) | Associated yarn item code |

---

## Business Logic (What it does and why)

Retrieves list of item codes that have preparation stage specifications configured (warping, beaming, drawing parameters). Unlike ITM_GETITEMCODELIST which returns all items, this procedure filters to only items that have complete preparation specifications, ensuring operators only see items that can actually be processed through warping/beaming/drawing stages.

The procedure:
1. Queries items with configured prepare specs
2. Filters out items without warping/beaming/drawing parameters
3. Returns items ready for preparation processes
4. Prevents selection of items lacking required production parameters

Used in modules M02 (Warping), M03 (Beaming), M04 (Drawing) to populate item selection lists.

---

## Related Procedures

**Similar**:
- [202-ITM_GETITEMCODELIST.md](./202-ITM_GETITEMCODELIST.md) - Get all item codes
- [204-ITM_GETITEMYARNLIST.md](./204-ITM_GETITEMYARNLIST.md) - Get items with yarn composition

**Downstream**:
- CONDITION_WARPING - Get warping conditions for item
- CONDITION_BEAMING - Get beaming conditions for item
- CONDITION_DRAWING - Get drawing conditions for item

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: Multiple services:
- `DrawingDataService.cs`
- `ProcessConditionDataService.cs`
- `WarpingDataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_GETITEMPREPARELIST(ITM_GETITEMPREPARELISTParameter para)`
**Lines**: 22163-22202

---

**File**: 203/296 | **Progress**: 68.6%
