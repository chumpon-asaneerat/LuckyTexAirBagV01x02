# WARP_RECEIVEPALLET

**Procedure Number**: 020 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Receive and register new yarn pallet into warping inventory |
| **Operation** | INSERT |
| **Called From** | WarpingDataService.cs:1406 → WARP_RECEIVEPALLET() |
| **Frequency** | Medium (yarn receiving operations) |
| **Performance** | Fast (single insert) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMYARN` | VARCHAR2(50) | ✅ | Yarn item code |
| `P_RECEIVEDATE` | DATE | ⬜ | Date pallet was received |
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet barcode/number (unique identifier) |
| `P_WEIGHT` | NUMBER | ⬜ | Total weight in kilograms |
| `P_CH` | NUMBER | ⬜ | Cheese count (number of yarn packages on pallet) |
| `P_VERIFY` | VARCHAR2(10) | ⬜ | Verification status (Y/N) |
| `P_REJECTID` | VARCHAR2(50) | ⬜ | Reject reason code if pallet has quality issues |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who received the pallet |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

---

## Business Logic (What it does and why)

Registers a new yarn pallet into the warping inventory system when yarn is received from the warehouse or supplier. This creates the initial inventory record that will be tracked throughout the warping process.

**Workflow**:
1. Warehouse delivers yarn pallet to warping department
2. Operator scans or enters pallet barcode
3. System verifies pallet doesn't already exist
4. Operator confirms yarn item code, weight, and cheese count
5. If pallet has quality issues, selects reject reason
6. System creates new pallet record with initial quantities
7. Pallet becomes available for creel setup operations

**Business Rules**:
- Pallet number must be unique (primary key constraint)
- Initial state: USEDCH = 0, REJECTCH = 0, FINISHFLAG = 'N'
- VERIFY field controls whether pallet can be used in production
- Rejected pallets (REJECTID set) may have restricted usage
- Creates audit trail (CREATEDATE, CREATEBY)
- Pallet weight and cheese count used for yield tracking

**Inventory Tracking**:
- `RECEIVEWEIGHT` = Initial weight received
- `RECEIVECH` = Initial cheese count
- `USEDWEIGHT` = 0 (not yet used)
- `USEDCH` = 0 (not yet used)
- `REJECTCH` = 0 (no rejects recorded yet)
- `FINISHFLAG` = 'N' (pallet still has stock)

---

## Related Procedures

**Upstream**: None - This is typically the first operation for a pallet
**Downstream**: [019-WARP_PALLETLISTBYITMYARN.md](./019-WARP_PALLETLISTBYITMYARN.md) - Lists available pallets including this new one
**Similar**: G3_RECEIVEYARN - Warehouse yarn receiving (different module)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_RECEIVEPALLET()`
**Line**: 1406-1445

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_RECEIVEPALLET(WARP_RECEIVEPALLETParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 020/296 | **Progress**: 6.8%
