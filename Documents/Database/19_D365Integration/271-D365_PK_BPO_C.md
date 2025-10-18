# D365_PK_BPO_C

**Procedure Number**: 271 | **Module**: M19 - D365 Integration | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get Packing Batch Production Order data (Variant C) for D365 ERP sync |
| **Operation** | SELECT |
| **Tables** | Packing production order table (variant C data) |
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
| `PRODID` | NUMBER | Production order ID in D365 |
| `LOTNO` | VARCHAR2(50) | Lot number (batch identifier) |
| `ITEMID` | VARCHAR2(50) | Item ID (packed product) in D365 ERP |
| `LOADINGTYPE` | VARCHAR2(50) | Loading type code |
| `QTY` | NUMBER | Quantity packed |
| `UNIT` | VARCHAR2(50) | Unit of measure (e.g., 'ROLL', 'PCS') |
| `OPERATION` | VARCHAR2(50) | Operation code (e.g., 'PACK') |
| `MACHINESTART` | DATE | Packing start timestamp |

---

## Business Logic (What it does and why)

Retrieves Packing Batch Production Order data (Variant C) for D365 ERP synchronization. This is a variant version of D365_PK_BPO that handles specific packing scenarios or customer requirements (designated as "C" variant).

The procedure:
1. Takes pallet number, item code, and loading type to identify the packing operation
2. Returns production order details for D365 sync (variant C processing)
3. Includes item code (packed product type), quantity, operation type
4. Provides packing start time for production tracking
5. Must be synced FIRST before any material consumption (ISH/ISL_C) or output (OUH_C/OUL_C) transactions

**Note**: The "_C" suffix indicates a variant processing path, likely for different packing methods, customer-specific requirements, or consolidated shipments.

Part of the D365 integration pattern: BPO_C → ISH → ISL_C → OUH_C → OUL_C

---

## Related Procedures

**Downstream**:
- [272-D365_PK_ISH.md](./272-D365_PK_ISH.md) - Material consumption header (shared)
- [274-D365_PK_ISL_C.md](./274-D365_PK_ISL_C.md) - Material consumption lines (variant C)

**Similar**:
- [270-D365_PK_BPO.md](./270-D365_PK_BPO.md) - Standard packing BPO

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\\Services\\DataService\\D365DataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\\Domains\\AirbagSPs.cs`
**Method**: `D365_PK_BPO_C(D365_PK_BPO_CParameter para)`
**Lines**: 35285-35332

---

**File**: 271/296 | **Progress**: 91.6%
