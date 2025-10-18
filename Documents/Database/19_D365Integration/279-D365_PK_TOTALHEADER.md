# D365_PK_TOTALHEADER

**Procedure Number**: 279 | **Module**: M19 - D365 Integration | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get Packing Total Header summary for D365 ERP pallet aggregation |
| **Operation** | SELECT |
| **Tables** | Packing header aggregation table |
| **Called From** | D365DataService.cs |
| **Frequency** | High (ERP sync) |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PLALLETNO` | VARCHAR2(50) | ✅ | Pallet number (shipping identifier) - **Note**: Parameter has typo "PLALLET" |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PALLETNO` | VARCHAR2(50) | Pallet number (shipping identifier) |
| `ITEMCODE` | VARCHAR2(50) | Item code (packed product identifier) |
| `LOADINGTYPE` | VARCHAR2(50) | Loading type code (customer/order specific) |

---

## Business Logic (What it does and why)

Retrieves Packing Total Header summary data for D365 ERP synchronization. This procedure provides aggregated header information for a pallet, returning the key identifiers (pallet number, item code, loading type) that are used to query detailed packing transactions.

The procedure:
1. Takes pallet number to identify the shipment
2. Returns summary header information for this pallet
3. Provides the three key identifiers needed for subsequent D365 sync queries:
   - PALLETNO: Which pallet
   - ITEMCODE: What product is packed
   - LOADINGTYPE: Which customer/order
4. Used as a lookup/summary procedure to get pallet details before syncing BPO/ISH/ISL/OUH/OUL

**Note**: This is a supporting procedure that provides the parameter values needed for other packing D365 procedures. The parameter name contains a typo (P_PLALLETNO instead of P_PALLETNO).

Common use case: Query this procedure first to get pallet details, then use the returned values to call D365_PK_BPO, D365_PK_ISH, etc.

---

## Related Procedures

**Downstream (uses results from this procedure)**:
- [270-D365_PK_BPO.md](./270-D365_PK_BPO.md) - Uses PALLETNO, ITEMCODE, LOADINGTYPE
- [271-D365_PK_BPO_C.md](./271-D365_PK_BPO_C.md) - Uses PALLETNO, ITEMCODE, LOADINGTYPE
- [272-D365_PK_ISH.md](./272-D365_PK_ISH.md) - Uses PALLETNO, ITEMCODE, LOADINGTYPE
- All other packing procedures

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\\Services\\DataService\\D365DataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\\Domains\\AirbagSPs.cs`
**Method**: `D365_PK_TOTALHEADER(D365_PK_TOTALHEADERParameter para)`
**Lines**: 35739-35780

---

**File**: 279/296 | **Progress**: 94.3%
