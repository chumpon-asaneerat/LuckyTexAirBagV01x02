# D365_PK_OUL_C

**Procedure Number**: 278 | **Module**: M19 - D365 Integration | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get Packing Output Update Line details (Variant C) for D365 ERP output corrections/updates |
| **Operation** | SELECT |
| **Tables** | Packing production output update detail table (variant C data) |
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
| `LINENO` | NUMBER | Update line sequence number |
| `OUTPUTDATE` | DATE | Output/packing date |
| `ITEMID` | VARCHAR2(50) | Item ID (packed product) in D365 |
| `QTY` | NUMBER | Quantity (updated value) |
| `UNIT` | VARCHAR2(50) | Unit of measure |
| `GROSSLENGTH` | NUMBER | Gross length (meters, with waste) |
| `NETLENGTH` | NUMBER | Net length (meters, usable) |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg, with packaging) |
| `NETWEIGHT` | NUMBER | Net weight (kg, product only) |
| `PALLETNO` | VARCHAR2(50) | Pallet number for storage/shipping |
| `GRADE` | VARCHAR2(50) | Quality grade (A, B, C, etc.) |
| `LOADINGTYPE` | VARCHAR2(50) | Loading type code |
| `SERIALID` | VARCHAR2(50) | Serial/batch ID for traceability |
| `FINISH` | NUMBER | Finish status flag |
| `MOVEMENTTRANS` | VARCHAR2(50) | Movement transaction type |
| `WAREHOUSE` | VARCHAR2(50) | Warehouse location code |
| `LOCATION` | VARCHAR2(50) | Specific location within warehouse |

---

## Business Logic (What it does and why)

Retrieves Packing production Output Update Line details (Variant C) for D365 ERP synchronization. This is a variant version of D365_PK_OUL that handles specific packing scenarios or customer requirements (designated as "C" variant). Update Lines contain the corrected/updated information for each packed pallet in the variant C processing path.

The procedure:
1. Takes pallet number, item code, and loading type to identify the packing operation
2. Returns all output update line items for this packing lot (variant C processing)
3. Each line has updated values for quantity, quality grade, weights, lengths
4. Includes warehouse location and movement transaction data
5. Used to post output correction transactions in D365
6. Maintains full traceability through serial ID

Synced after Output Update Header (OUH_C) is posted to D365. Used for variant C packing process (BPO_C). Common scenarios: consolidated shipment adjustments, special customer-specific corrections, multi-order pallet updates.

---

## Related Procedures

**Upstream**:
- [276-D365_PK_OUH_C.md](./276-D365_PK_OUH_C.md) - Output update header variant C (must sync first)

**Similar**:
- [277-D365_PK_OUL.md](./277-D365_PK_OUL.md) - Standard version

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\\Services\\DataService\\D365DataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\\Domains\\AirbagSPs.cs`
**Method**: `D365_PK_OUL_C(D365_PK_OUL_CParameter para)`
**Lines**: 35663-35732

---

**File**: 278/296 | **Progress**: 93.9%
