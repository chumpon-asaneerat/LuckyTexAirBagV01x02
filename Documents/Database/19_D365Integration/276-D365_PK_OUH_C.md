# D365_PK_OUH_C

**Procedure Number**: 276 | **Module**: M19 - D365 Integration | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get Packing Output Update Header (Variant C) for D365 ERP output corrections/updates |
| **Operation** | SELECT |
| **Tables** | Packing production output update header table (variant C data) |
| **Called From** | D365DataService.cs |
| **Frequency** | Medium (ERP sync - corrections only) |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet number (shipping identifier) |
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Item code (packed product identifier) |
| `P_LOADINGTYPE` | VARCHAR2(50) | ✅ | Loading type code (customer/order specific) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `HEADERID` | NUMBER | Output update header ID |
| `ITEMID` | VARCHAR2(50) | Item ID in D365 (included in variant C) |
| `TOTALRECORD` | NUMBER | Total number of update line records |
| `MACHINESTART` | DATE | Packing start timestamp |

---

## Business Logic (What it does and why)

Retrieves Packing production Output Update Header data (Variant C) for D365 ERP synchronization. This is a variant version of D365_PK_OUH that handles specific packing scenarios or customer requirements (designated as "C" variant). Update Header represents the summary of all output corrections/changes for variant C packing.

The procedure:
1. Takes pallet number, item code, and loading type to identify the packing operation
2. Returns output update header ID for this packing lot (variant C processing)
3. Provides total count of update line items
4. **Includes ITEMID in results** (unlike standard OUH) for additional item tracking
5. Header must be synced before update lines (OUL_C)

Used for variant C packing process (BPO_C). The additional ITEMID column suggests variant C requires more detailed item-level tracking in the header.

---

## Related Procedures

**Downstream**:
- [278-D365_PK_OUL_C.md](./278-D365_PK_OUL_C.md) - Output update lines (variant C detail records)

**Similar**:
- [275-D365_PK_OUH.md](./275-D365_PK_OUH.md) - Standard version

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\\Services\\DataService\\D365DataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\\Domains\\AirbagSPs.cs`
**Method**: `D365_PK_OUH_C(D365_PK_OUH_CParameter para)`
**Lines**: 35539-35580

---

**File**: 276/296 | **Progress**: 93.2%
