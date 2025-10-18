# ITM_SEARCHITEMCODE

**Procedure Number**: 207 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search item codes with flexible filtering criteria |
| **Operation** | SELECT |
| **Tables** | Item master table |
| **Called From** | ItemCodeService.cs |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2(50) | ⬜ | Item code filter (partial match) |
| `P_ITEMWEAV` | VARCHAR2(50) | ⬜ | Weaving item code filter |
| `P_ITEMPREPARE` | VARCHAR2(50) | ⬜ | Preparation item code filter |
| `P_ITEMYARN` | VARCHAR2(50) | ⬜ | Yarn item code filter |
| `P_YARNCODE` | VARCHAR2(50) | ⬜ | Yarn code filter |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `ITM_WIDTH` | NUMBER | Item width |
| `ITM_PROC1` | VARCHAR2(50) | Process 1 specification |
| `ITM_PROC2` | VARCHAR2(50) | Process 2 specification |
| `ITM_PROC3` | VARCHAR2(50) | Process 3 specification |
| `ITM_PROC4` | VARCHAR2(50) | Process 4 specification |
| `ITM_PROC5` | VARCHAR2(50) | Process 5 specification |
| `ITM_PROC6` | VARCHAR2(50) | Process 6 specification |
| `CREATEDATE` | DATE | Record creation date |
| `CREATEBY` | VARCHAR2(50) | Created by operator |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2(50) | Last edited by operator |
| `ITM_PREPARE` | VARCHAR2(50) | Preparation item code |
| `COREWEIGHT` | NUMBER | Core weight specification |
| `FULLWEIGHT` | NUMBER | Full weight specification |
| `ITM_GROUP` | VARCHAR2(50) | Item group/category |
| `YARNCODE` | VARCHAR2(50) | Yarn code |
| `WIDTHCODE` | VARCHAR2(50) | Width code |
| `WIDTHWEAVING` | NUMBER | Weaving width |
| `LABFORM` | VARCHAR2(50) | Laboratory form type |
| `WEAVE_TYPE` | VARCHAR2(50) | Weave type |

---

## Business Logic (What it does and why)

Searches item master data with flexible filtering across multiple criteria. Allows administrators to find items by any combination of item code, weaving code, preparation code, yarn code, or yarn type. All search parameters are optional, enabling broad or narrow searches depending on needs.

The procedure:
1. Accepts multiple optional search criteria
2. Performs partial/wildcard matching on provided filters
3. Returns complete item records with all specifications
4. Used for lookup before editing or viewing item details
5. Returns audit trail information (create/edit dates and operators)

Commonly used in master data management screens for searching items before editing.

---

## Related Procedures

**Upstream**: Used before editing operations

**Downstream**:
- [206-ITM_INSERTUPDATEITEMCODE.md](./206-ITM_INSERTUPDATEITEMCODE.md) - Update found items

**Similar**:
- [202-ITM_GETITEMCODELIST.md](./202-ITM_GETITEMCODELIST.md) - Get all items (no filtering)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ItemCodeService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_SEARCHITEMCODE(ITM_SEARCHITEMCODEParameter para)`
**Lines**: 21937-22004

---

**File**: 207/296 | **Progress**: 69.9%
