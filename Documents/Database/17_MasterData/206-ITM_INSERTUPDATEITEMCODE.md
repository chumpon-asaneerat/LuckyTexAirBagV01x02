# ITM_INSERTUPDATEITEMCODE

**Procedure Number**: 206 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create or update item code master data with production specifications |
| **Operation** | INSERT/UPDATE |
| **Tables** | Item master table with multiple specification links |
| **Called From** | ItemCodeService.cs |
| **Frequency** | Low |
| **Performance** | Medium |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Item code (finished product) |
| `P_ITEMWEAV` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_ITEMPREPARE` | VARCHAR2(50) | ✅ | Preparation item code (warping/beaming/drawing) |
| `P_ITEMYARN` | VARCHAR2(50) | ✅ | Yarn item code |
| `P_WIDTH` | NUMBER | ⬜ | Item width specification |
| `P_WEAVEWIDTH` | NUMBER | ⬜ | Weaving width specification |
| `P_COREWEIGHT` | NUMBER | ⬜ | Core weight specification |
| `P_YARNCODE` | VARCHAR2(50) | ⬜ | Alternative yarn code |
| `P_PROC1` | VARCHAR2(50) | ⬜ | Process 1 specification |
| `P_PROC2` | VARCHAR2(50) | ⬜ | Process 2 specification |
| `P_PROC3` | VARCHAR2(50) | ⬜ | Process 3 specification |
| `P_PROC4` | VARCHAR2(50) | ⬜ | Process 4 specification |
| `P_PROC5` | VARCHAR2(50) | ⬜ | Process 5 specification |
| `P_PROC6` | VARCHAR2(50) | ⬜ | Process 6 specification |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID performing the update |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Creates or updates item code master data with complete production specifications linking all stages of the manufacturing process. Each finished item requires definitions for yarn composition, preparation parameters (warping/beaming/drawing), weaving specifications, and optional finishing processes. This procedure maintains the hierarchical relationship between item codes at different production stages.

The procedure:
1. Validates all linked item codes exist (yarn, prepare, weaving)
2. Checks if item code exists (UPDATE) or new (INSERT)
3. Saves item master record with all process links
4. Updates process specifications (PROC1-6) if provided
5. Records operator and timestamp for audit trail
6. Returns success or validation error

**Critical**: This establishes the foundational data for entire production workflow.

---

## Related Procedures

**Upstream**:
- [202-ITM_GETITEMCODELIST.md](./202-ITM_GETITEMCODELIST.md) - View existing items
- [207-ITM_SEARCHITEMCODE.md](./207-ITM_SEARCHITEMCODE.md) - Search items before editing

**Downstream**: All production modules depend on this master data

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ItemCodeService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_INSERTUPDATEITEMCODE(ITM_INSERTUPDATEITEMCODEParameter para)`
**Lines**: 22013-22067

---

**File**: 206/296 | **Progress**: 69.6%
