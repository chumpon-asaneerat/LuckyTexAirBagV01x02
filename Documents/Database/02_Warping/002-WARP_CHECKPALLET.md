# WARP_CHECKPALLET

**Procedure Number**: 002 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Check pallet details and availability for warping |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:1236 → WARP_CHECKPALLET() |
| **Frequency** | High (called every time operator scans pallet barcode) |
| **Performance** | Fast (single pallet lookup) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet barcode number |

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
| `REJECTID` | VARCHAR2(50) | Reject reason ID (if any) |
| `FINISHFLAG` | VARCHAR2(1) | Pallet finished flag (Y/N) |
| `RETURNFLAG` | VARCHAR2(1) | Pallet returned flag (Y/N) |
| `REJECTCH` | NUMBER | Rejected cheese count |
| `CREATEDATE` | DATE | Record creation date |
| `CREATEBY` | VARCHAR2(50) | Created by user |
| `CLEARBY` | VARCHAR2(50) | Cleared by user |
| `REMARK` | VARCHAR2(200) | Remarks |
| `CLEARDATE` | DATE | Clear date |
| `KGPERCH` | NUMBER | Kg per cheese |

---

## Business Logic (What it does and why)

Validates and retrieves pallet information when operator scans a pallet barcode during warping creel setup. This procedure checks if the pallet exists, is available for use, and returns all relevant pallet details including yarn type, weight, cheese count, and usage status.

**Workflow**:
1. Operator scans pallet barcode using barcode scanner
2. System calls this procedure to lookup pallet details
3. Procedure retrieves pallet record from yarn pallet table
4. Returns complete pallet information including:
   - Yarn item code (to verify correct yarn type)
   - Remaining weight and cheese count
   - Usage status (finished, returned, rejected)
   - Verification status
5. UI displays pallet details or error if pallet not found/unavailable

**Business Rules**:
- Pallet must exist in system
- Shows remaining weight after previous usage
- Indicates if pallet is finished (fully used)
- Shows return or reject status
- Displays kg per cheese for quantity calculation

---

## Related Procedures

**Upstream**: None - Entry point when scanning pallet
**Downstream**: [015-WARP_INSERTSETTINGDETAIL.md](./015-WARP_INSERTSETTINGDETAIL.md) - Allocates checked pallet to creel
**Similar**: [G3_SEARCHBYPALLETNO.md](../12_G3_Warehouse/G3_SEARCHBYPALLETNO.md) - Similar pallet lookup in G3

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_CHECKPALLET()`
**Lines**: 1236-1287

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_CHECKPALLET(WARP_CHECKPALLETParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 002/296 | **Progress**: 0.7%
