# WARP_CANCELCREELSETUP

**Procedure Number**: 001 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Cancel creel setup for warping machine |
| **Operation** | UPDATE / DELETE |
| **Called From** | WarpingDataService.cs:2017 → WARP_CANCELCREELSETUP() |
| **Frequency** | Low (only when operator needs to cancel setup) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number (creel setup ID) |
| `P_WARPMC` | VARCHAR2(50) | ⬜ | Warping machine number |
| `P_SIDE` | VARCHAR2(10) | ⬜ | Side identifier (A or B) |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who cancels the setup |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2 | Return message (success/error status) |

### Returns (if cursor)

N/A - Returns single string result

---

## Business Logic (What it does and why)

Cancels a creel setup that was previously configured for warping production. When an operator starts setting up yarn pallets on the creel but needs to abort the setup (wrong item, machine issue, etc.), this procedure removes the setup records and releases any allocated pallets back to available inventory.

**Workflow**:
1. Operator initiates cancel operation from warping setup screen
2. System validates the warping head number exists
3. Deletes or marks as cancelled the creel setup record
4. Removes pallet allocation records associated with this setup
5. Returns success/error message to UI

**Business Rules**:
- Can only cancel setups that haven't started production
- Must release all allocated pallets back to inventory
- Requires operator authorization
- Maintains audit trail of who cancelled and when

---

## Related Procedures

**Upstream**: [002-WARP_INSERTSETTINGHEAD.md](./015-WARP_INSERTSETTINGHEAD.md) - Creates the setup being cancelled
**Downstream**: None - Terminal operation
**Similar**: [027-BEAM_CANCELLOOMSETUP.md](../03_Beaming/027-BEAM_CANCELLOOMSETUP.md) - Similar cancel pattern

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_CANCELCREELSETUP()`
**Lines**: 2017-2049

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_CANCELCREELSETUP(WARP_CANCELCREELSETUPParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 001/296 | **Progress**: 0.3%
