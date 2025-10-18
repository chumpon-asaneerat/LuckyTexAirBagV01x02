# D365_PK_ISL_C

**Procedure Number**: 274 | **Module**: M19 - D365 Integration | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get Packing Issue Line details (Variant C) for D365 ERP material consumption detail sync |
| **Operation** | SELECT |
| **Tables** | Packing material issue detail table (variant C data) |
| **Called From** | D365DataService.cs |
| **Frequency** | High (ERP sync) |
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
| `LINENO` | NUMBER | Issue line sequence number |
| `ISSUEDATE` | DATE | Material issue date/time |
| `ITEMID` | VARCHAR2(50) | Item ID (cut/printed fabric) in D365 |
| `STYLEID` | VARCHAR2(50) | Style/variant ID |
| `QTY` | NUMBER | Quantity issued (consumed for packing) |
| `UNIT` | VARCHAR2(50) | Unit of measure |
| `SERIALID` | VARCHAR2(50) | Serial/batch ID for traceability |

---

## Business Logic (What it does and why)

Retrieves Packing material Issue Line details (Variant C) for D365 ERP synchronization. This is a variant version of D365_PK_ISL that handles specific packing scenarios or customer requirements (designated as "C" variant). Issue Lines are the detail records showing each cut/printed fabric piece consumed for variant C packing operations.

The procedure:
1. Takes pallet number, item code, and loading type to identify the packing operation
2. Returns all issue line items for this packing lot (variant C processing)
3. Each line shows which cut/printed fabric was packed (ITEMID, SERIALID)
4. Includes quantity, unit, and issue date/time
5. Used to post material consumption transactions in D365
6. Enables backward traceability (which fabric pieces went onto this pallet)

Synced after Issue Header (ISH) is posted to D365. Used for variant C packing process (BPO_C).

**Note**: The "_C" suffix indicates this handles material consumption for the variant C packing path, likely with different material tracking or consolidated shipment logic.

---

## Related Procedures

**Upstream**:
- [272-D365_PK_ISH.md](./272-D365_PK_ISH.md) - Issue header (shared, must sync first)

**Related**:
- [271-D365_PK_BPO_C.md](./271-D365_PK_BPO_C.md) - Variant C packing BPO
- [273-D365_PK_ISL.md](./273-D365_PK_ISL.md) - Standard version

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\\Services\\DataService\\D365DataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\\Domains\\AirbagSPs.cs`
**Method**: `D365_PK_ISL_C(D365_PK_ISL_CParameter para)`
**Lines**: 35439-35484

---

**File**: 274/296 | **Progress**: 92.6%
