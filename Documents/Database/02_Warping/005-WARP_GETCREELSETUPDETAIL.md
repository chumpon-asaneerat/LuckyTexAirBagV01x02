# WARP_GETCREELSETUPDETAIL

**Procedure Number**: 005 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get detailed pallet list for creel setup |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:973 → WARP_GETCREELSETUPDETAIL() |
| **Frequency** | High (displayed on creel setup screen) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PALLETNO` | VARCHAR2(50) | Pallet barcode |
| `RECEIVECH` | NUMBER | Received cheese count |
| `USEDCH` | NUMBER | Used cheese count |
| `REJECTCH` | NUMBER | Rejected cheese count |
| `PREJECT` | NUMBER | Previous reject count |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `RECEIVEDATE` | DATE | Pallet receive date |
| `PUSED` | NUMBER | Previous used amount |
| `NoCH` | NUMBER | Remaining cheese (calculated) |
| `Use` | NUMBER | Net usage (calculated) |

---

## Business Logic (What it does and why)

Retrieves complete list of pallets allocated to a creel setup with detailed usage statistics. Displays to operator all pallets assigned to the setup showing how much has been used, rejected, and remaining for each pallet. Includes calculated fields for net usage and remaining quantity.

**Workflow**:
1. UI requests creel setup details by head number
2. Procedure retrieves all pallet records for this setup
3. Calculates remaining cheese: RECEIVECH - USEDCH - REJECTCH
4. Calculates net usage: USEDCH - PREJECT or PUSED if available
5. Returns list sorted by pallet position

**Business Rules**:
- Shows all pallets for setup (multiple records)
- Includes calculated fields (NoCH, Use)
- Handles null values in numeric fields
- Used for operator verification before production

---

## Related Procedures

**Upstream**: [015-WARP_INSERTSETTINGDETAIL.md](./015-WARP_INSERTSETTINGDETAIL.md) - Creates setup details
**Downstream**: None - Display only
**Similar**: [BEAM_GETBEAMROLLDETAIL.md](../03_Beaming/BEAM_GETBEAMROLLDETAIL.md) - Similar detail retrieval

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETCREELSETUPDETAIL()`
**Lines**: 973-1050

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_GETCREELSETUPDETAIL(WARP_GETCREELSETUPDETAILParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 005/296 | **Progress**: 1.7%
