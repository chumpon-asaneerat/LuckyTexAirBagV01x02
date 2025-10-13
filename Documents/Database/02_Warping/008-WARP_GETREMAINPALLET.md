# WARP_GETREMAINPALLET

**Procedure Number**: 008 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get remaining available pallets for specific yarn item |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:669 → WARP_GETREMAINPALLET() |
| **Frequency** | High (selecting pallets for setup) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEM_YARN` | VARCHAR2(50) | ✅ | Yarn item code |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `RECEIVEDATE` | DATE | Pallet receive date |
| `PALLETNO` | VARCHAR2(50) | Pallet barcode |
| `RECEIVEWEIGHT` | NUMBER | Original receive weight (kg) |
| `RECEIVECH` | NUMBER | Original receive cheese count |
| `USEDWEIGHT` | NUMBER | Used weight (kg) |
| `USEDCH` | NUMBER | Used cheese count |
| `VERIFY` | VARCHAR2(10) | Verification status |
| `REJECTID` | VARCHAR2(50) | Reject reason ID |
| `FINISHFLAG` | VARCHAR2(1) | Finished flag (Y/N) |
| `RETURNFLAG` | VARCHAR2(1) | Returned flag (Y/N) |
| `REJECTCH` | NUMBER | Rejected cheese count |
| `CREATEDATE` | DATE | Create date |
| `CREATEBY` | VARCHAR2(50) | Created by user |
| `CLEARBY` | VARCHAR2(50) | Cleared by user |
| `REMARK` | VARCHAR2(200) | Remarks |
| `CLEARDATE` | DATE | Clear date |
| `REMAINCH` | NUMBER | Remaining cheese (calculated) |

---

## Business Logic (What it does and why)

Retrieves list of available pallets for specific yarn item during creel setup. Filters pallets to show only those with remaining quantity (not finished, not returned). Displays remaining cheese count to help operator select appropriate pallets for production. Essential for pallet selection during warping setup.

**Workflow**:
1. Operator starts creel setup for specific item/yarn combination
2. System requests available pallets for that yarn item
3. Procedure queries pallets matching item code
4. Filters out finished pallets (FINISHFLAG='Y')
5. Filters out returned pallets (RETURNFLAG='Y')
6. Calculates remaining cheese for each pallet
7. Returns sorted list (usually by receive date - FIFO)
8. Operator selects pallets from available list

**Business Rules**:
- Shows only available pallets (not finished, not returned)
- Must match specified yarn item code
- Displays remaining quantity for each pallet
- Usually sorted by receive date (FIFO inventory management)
- Operator selects pallets with sufficient remaining quantity

---

## Related Procedures

**Upstream**: None - Entry point for pallet selection
**Downstream**: [002-WARP_CHECKPALLET.md](./002-WARP_CHECKPALLET.md) - Verify selected pallet details
**Similar**: [G3_SEARCHYARNSTOCK.md](../12_G3_Warehouse/G3_SEARCHYARNSTOCK.md) - Similar stock search

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETREMAINPALLET()`
**Lines**: 669-720

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_GETREMAINPALLET(WARP_GETREMAINPALLETParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 008/296 | **Progress**: 2.7%
