# WARP_CLEARPALLET

**Procedure Number**: 003 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Clear/reset pallet usage data for warping |
| **Operation** | UPDATE |
| **Called From** | WarpingDataService.cs:1885 → WARP_CLEARPALLET() |
| **Frequency** | Low (only when clearing pallet usage records) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet barcode number to clear |
| `P_RECEIVEDATE` | DATE | ⬜ | Receive date for validation |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who clears the pallet |
| `P_REMARK` | VARCHAR2(200) | ⬜ | Remark/reason for clearing |

### Output (OUT)

None - Returns boolean success/failure

### Returns (if cursor)

N/A - Returns execution result (success = true, failure = false)

---

## Business Logic (What it does and why)

Clears or resets pallet usage data when an error occurred during warping setup or when pallet needs to be re-allocated. This operation resets the pallet's used weight and cheese count back to zero, effectively making the pallet available for reuse as if it was never allocated.

**Workflow**:
1. Supervisor/operator identifies pallet that needs clearing (wrong allocation, data entry error, etc.)
2. Initiates clear operation from UI with pallet barcode and reason
3. System validates pallet exists and matches receive date if provided
4. Resets USEDWEIGHT and USEDCH counters to 0
5. Clears or marks pallet usage records as cancelled
6. Records who cleared and when for audit trail
7. Saves remark explaining why pallet was cleared

**Business Rules**:
- Pallet number is required
- Can only clear pallets not currently in active production
- Requires supervisor authorization (operator ID required)
- Remark should explain reason for clearing
- Audit trail maintained for traceability

---

## Related Procedures

**Upstream**: [002-WARP_CHECKPALLET.md](./002-WARP_CHECKPALLET.md) - Check pallet before clearing
**Downstream**: [002-WARP_CHECKPALLET.md](./002-WARP_CHECKPALLET.md) - Verify pallet cleared successfully
**Similar**: None - Unique operation

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_CLEARPALLET()`
**Lines**: 1885-1917

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_CLEARPALLET(WARP_CLEARPALLETParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 003/296 | **Progress**: 1.0%
