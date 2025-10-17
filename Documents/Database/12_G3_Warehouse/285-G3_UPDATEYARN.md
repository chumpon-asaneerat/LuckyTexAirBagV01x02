# G3_UPDATEYARN

**Procedure Number**: 285 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update yarn pallet information and QC inspection results |
| **Operation** | UPDATE |
| **Tables** | tblYarnStock (assumed) |
| **Called From** | G3DataService.cs:346 → G3_UPDATEYARN() (COMMENTED OUT) |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🔴 1 High - Entire method is commented out, not in active use |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PATTETNO` | VARCHAR2(50) | ✅ | Pallet number to update |
| `P_LOTORDERNO` | VARCHAR2(50) | ⬜ | Lot/order number from supplier |
| `P_VERIFY` | VARCHAR2(10) | ⬜ | QC verification status (PASS/FAIL) |
| `P_REMAINQTY` | NUMBER | ⬜ | Remaining quantity in stock |
| `P_FLAG` | VARCHAR2(10) | ⬜ | Status flag |
| `P_OPERATORID` | VARCHAR2(50) | ⬜ | Operator performing update |
| `P_RECEIVEDATE` | DATE | ⬜ | Receiving date |
| `P_UPDATEDATE` | DATE | ⬜ | Update timestamp |
| `P_TYPE` | VARCHAR2(10) | ⬜ | Material type |
| `P_PACKAGING` | VARCHAR2(10) | ⬜ | Packaging QC result (OK/NG) |
| `P_CLEAN` | VARCHAR2(10) | ⬜ | Cleanliness QC result (OK/NG) |
| `P_TEARING` | VARCHAR2(10) | ⬜ | Tearing inspection result (OK/NG) |
| `P_FALLDOWN` | VARCHAR2(10) | ⬜ | Fall down test result (OK/NG) |
| `P_CERTIFICATION` | VARCHAR2(10) | ⬜ | Certification document status (OK/NG) |
| `P_INVOICE` | VARCHAR2(10) | ⬜ | Invoice document status (OK/NG) |
| `P_IDENTIFYAREA` | VARCHAR2(10) | ⬜ | Identification area check (OK/NG) |
| `P_AMOUNTPALLET` | VARCHAR2(10) | ⬜ | Pallet amount check (OK/NG) |
| `P_OTHER` | VARCHAR2(500) | ⬜ | Other remarks |
| `P_ACTION` | VARCHAR2(500) | ⬜ | Corrective action taken |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - UPDATE operation only

---

## Business Logic (What it does and why)

**IMPORTANT**: This procedure is currently COMMENTED OUT in the codebase (lines 346-428 in G3DataService.cs).

**Original Intent** (when active):
Updates existing yarn pallet information and QC inspection results. Similar to G3_RECEIVEYARN but used for editing/correcting previously received pallets.

**Intended Workflow**:
1. Search pallet by pallet number
2. Allow modification of:
   - QC inspection results (all quality checks)
   - Verification status (PASS/FAIL)
   - Remaining quantity (stock adjustment)
   - Documentation status
   - Corrective actions
3. Record operator and update timestamp
4. Save changes to database

**Business Rules** (when active):
- Pallet number is mandatory identifier
- Similar to G3_RECEIVEYARN but uses pallet number instead of trace number
- Allows re-inspection or correction of QC results
- Updates audit trail with operator and timestamp

**Current Status**:
- **Entire method is commented out** (lines 346-428)
- Functionality replaced by G3_RECEIVEYARN
- May have been deprecated in favor of trace number-based updates
- Stored procedure definition still exists but not called from C#

**Comparison with G3_RECEIVEYARN**:
- G3_RECEIVEYARN: Uses TRACENO + LOTNO as key (active)
- G3_UPDATEYARN: Uses PATTETNO (pallet number) as key (inactive)
- Same QC parameters and functionality
- G3_RECEIVEYARN is the preferred method

---

## Related Procedures

**Replaced By**: [282-G3_RECEIVEYARN.md](./282-G3_RECEIVEYARN.md) - Active procedure for updating yarn receiving
**Similar**: G3_INSERTYARN - Also commented out, part of deprecated workflow

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_UPDATEYARN()` - **COMMENTED OUT**
**Lines**: 346-428 (entire method commented)

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_UPDATEYARN(G3_UPDATEYARNParameter para)`
**Lines**: 6652-6681

**Code Status**:
**Recommendation**: Consider removing stored procedure definition if permanently deprecated, or document migration path from pallet number to trace number based updates.

---

**File**: 285/296 | **Progress**: 96.3%
