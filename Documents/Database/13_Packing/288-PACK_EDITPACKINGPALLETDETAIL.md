# PACK_EDITPACKINGPALLETDETAIL

**Procedure Number**: 288 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Edit packing pallet detail - change inspection lot or update packing information |
| **Operation** | UPDATE |
| **Tables** | tblPackingPalletDetail (assumed) |
| **Called From** | PackingDataService.cs:806 → PACK_EDITPACKINGPALLETDETAIL() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Packing pallet number |
| `P_ORDERNO` | NUMBER | ⬜ | Order sequence number in pallet |
| `P_INSLOT_OLD` | VARCHAR2(50) | ✅ | Old inspection lot to replace |
| `P_INSLOT_NEW` | VARCHAR2(50) | ✅ | New inspection lot to add |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code |
| `P_GRADE` | VARCHAR2(10) | ⬜ | Quality grade |
| `P_NETLENGTH` | NUMBER | ⬜ | Net length (meters) |
| `P_GROSSLENGTH` | NUMBER | ⬜ | Gross length (meters) |
| `P_NETWEIGHT` | NUMBER | ⬜ | Net weight (kg) |
| `P_GROSSWEIGHT` | NUMBER | ⬜ | Gross weight (kg) |
| `P_CUSTYPE` | VARCHAR2(50) | ⬜ | Customer type |
| `P_INSPECTDATE` | DATE | ⬜ | Inspection date |
| `P_LOADTYPE` | VARCHAR2(10) | ⬜ | Loading type |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - UPDATE operation only

---

## Business Logic (What it does and why)

Edits packing pallet details by replacing an inspection lot or updating packing information. Used when operator needs to change rolled fabric in a pallet before shipping.

**Workflow**:
1. Identifies pallet detail by pallet number and order number
2. Replaces old inspection lot with new inspection lot
3. Updates all associated packing information:
   - Item code and grade
   - Length measurements (net/gross)
   - Weight measurements (net/gross)
   - Customer type and loading type
   - Inspection date reference
4. Validates new lot is compatible with pallet

**Business Rules**:
- Pallet number, old lot, and new lot are mandatory
- Old lot must exist in the pallet
- New lot must not already be in the pallet
- New lot must match pallet customer/item requirements
- Updates both lot reference and measurements
- Maintains packing sequence (order number)

**Common Scenarios**:
- Replace damaged roll with good roll
- Correct operator packing mistake
- Swap rolls to meet customer requirements
- Update measurements after re-inspection
- Change loading type for specific lot

**Data Integrity**:
- Old lot released back to available stock
- New lot marked as packed in pallet
- All measurements updated atomically
- Pallet totals recalculated

---

## Related Procedures

**Upstream**: [289-PACK_GETPALLETDETAIL.md](./289-PACK_GETPALLETDETAIL.md) - Gets current pallet details
**Related**: PACK_INSPACKINGPALLETDETAIL - Initial detail insertion
**Related**: [286-PACK_CANCELPALLET.md](./286-PACK_CANCELPALLET.md) - Cancels entire pallet

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_EDITPACKINGPALLETDETAIL()`
**Lines**: 776-820

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_EDITPACKINGPALLETDETAIL(PACK_EDITPACKINGPALLETDETAILParameter para)`
**Lines**: 2323-2347

**Implementation**:
---

**File**: 288/296 | **Progress**: 97.3%
