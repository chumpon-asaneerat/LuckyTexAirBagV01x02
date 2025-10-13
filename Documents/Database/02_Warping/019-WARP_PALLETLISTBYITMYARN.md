# WARP_PALLETLISTBYITMYARN

**Procedure Number**: 019 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get list of yarn pallets by item code for creel setup |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:307 → WARP_PALLETLISTBYITMYARN() |
| **Frequency** | High (every creel setup operation) |
| **Performance** | Medium (filters by item and calculates remaining stock) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEM_YARN` | VARCHAR2(50) | ✅ | Yarn item code to filter pallets |
| `P_WARPHEADNO` | VARCHAR2(50) | ⬜ | Current warping head number (to exclude already used pallets) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `RECEIVEDATE` | DATE | Date pallet was received |
| `PALLETNO` | VARCHAR2(50) | Pallet barcode/number |
| `RECEIVEWEIGHT` | NUMBER | Total received weight (kg) |
| `RECEIVECH` | NUMBER | Total received cheese count |
| `USEDWEIGHT` | NUMBER | Total used weight (kg) |
| `USEDCH` | NUMBER | Total used cheese count |
| `VERIFY` | VARCHAR2(10) | Verification status (Y/N) |
| `REJECTID` | VARCHAR2(50) | Reject reason code if any |
| `FINISHFLAG` | VARCHAR2(10) | Pallet fully used flag (Y/N) |
| `RETURNFLAG` | VARCHAR2(10) | Return to supplier flag (Y/N) |
| `REJECTCH` | NUMBER | Rejected cheese count |
| `CREATEDATE` | DATE | Pallet creation timestamp |
| `CREATEBY` | VARCHAR2(50) | Operator who created pallet |
| `CLEARBY` | VARCHAR2(50) | Operator who cleared pallet |
| `REMARK` | VARCHAR2(500) | Notes/remarks |
| `CLEARDATE` | DATE | Date pallet was cleared |

---

## Business Logic (What it does and why)

Retrieves available yarn pallets for a specific yarn item code during warping creel setup. When an operator is setting up yarn on the creel, they need to select which pallets to use. This procedure shows all pallets for the selected yarn type with remaining quantities.

**Workflow**:
1. Operator selects yarn item code on creel setup screen
2. System queries all pallets matching that yarn code
3. Calculates remaining cheese count for each pallet (RECEIVECH - USEDCH - REJECTCH)
4. Excludes pallets already allocated to current setup (via P_WARPHEADNO)
5. Returns sorted list (typically FIFO - oldest first)
6. Operator selects which pallets to use

**Business Rules**:
- Only shows pallets with remaining stock (FINISHFLAG != 'Y')
- Excludes returned pallets (RETURNFLAG = 'Y')
- Shows only verified pallets (VERIFY = 'Y') in production mode
- FIFO principle: oldest pallets (RECEIVEDATE) used first
- Each pallet tracks cheese count, not just weight
- Calculates available quantity dynamically

**Data Calculations (in C# code)**:
- `NoCH = RECEIVECH - USEDCH - REJECTCH` (available cheese count)
- `Use = NoCH` (default quantity to use - can be modified by operator)
- `Reject = 0` (default reject count - can be modified by operator)
- `Remain = NoCH - Use - Reject` (remaining after current allocation)

---

## Related Procedures

**Upstream**: [009-WARP_GETSPECBYCHOPNOANDMC.md](./009-WARP_GETSPECBYCHOPNOANDMC.md) - Gets item specs that determine which yarn to load
**Downstream**: [015-WARP_INSERTSETTINGDETAIL.md](./015-WARP_INSERTSETTINGDETAIL.md) - Saves selected pallets to creel setup
**Similar**: [008-WARP_GETREMAINPALLET.md](./008-WARP_GETREMAINPALLET.md) - Similar pallet query without warp head filter

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_PALLETLISTBYITMYARN()`
**Line**: 307-398

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_PALLETLISTBYITMYARN(WARP_PALLETLISTBYITMYARNParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 019/296 | **Progress**: 6.4%
