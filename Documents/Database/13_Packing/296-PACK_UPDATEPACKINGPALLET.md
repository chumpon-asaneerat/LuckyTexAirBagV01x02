# PACK_UPDATEPACKINGPALLET

**Procedure Number**: 296 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update packing pallet header status and verification information |
| **Operation** | UPDATE |
| **Tables** | tblPackingPallet (assumed) |
| **Called From** | PackingDataService.cs → PACK_UPDATEPACKINGPALLET() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLET` | VARCHAR2(50) | ✅ | Packing pallet number to update |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Checking operator ID |
| `P_CHECKDATE` | DATE | ⬜ | Checking date/time |
| `P_LAB` | VARCHAR2(10) | ⬜ | Lab test complete flag (Y/N) |
| `P_AS400` | VARCHAR2(10) | ⬜ | AS400 transfer flag (Y/N) |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Remarks/notes |
| `P_FLAG` | VARCHAR2(10) | ⬜ | Status flag (NEW/CHECKED/SHIPPED) |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - UPDATE operation only

---

## Business Logic (What it does and why)

Updates packing pallet header with verification information and status. Used to finalize pallet after adding all lots, or to update status during shipping workflow.

**Workflow**:
1. Receives pallet number and update parameters
2. Updates pallet header record:
   - Checking operator (supervisor who verified)
   - Checking date (when verified)
   - Lab completion status
   - AS400/ERP transfer status
   - Status flag (workflow state)
   - Additional remarks
3. May trigger downstream processes (printing, ERP sync)

**Business Rules**:
- Pallet number is mandatory
- Other parameters optional (only update what's provided)
- Checking operator and date typically set together
- FLAG controls pallet workflow state
- LAB and AS400 flags control shipping readiness

**Common Update Scenarios**:

1. **Supervisor Verification** (after packing complete):
   2. **Lab Test Completion**:
   3. **ERP Synchronization**:
   4. **Mark as Shipped**:
   **Status Flag Transitions**:
```
NEW → CHECKED → SHIPPED
 ↓        ↓        ↓
(any state) → CANCELLED
```

**Shipping Readiness Check**:
**Workflow Integration**:
1. PACK_INSERTPACKINGPALLET → Creates pallet (FLAG = 'NEW')
2. PACK_INSPACKINGPALLETDETAIL → Add lots (multiple calls)
3. **PACK_UPDATEPACKINGPALLET** → Supervisor checks (FLAG = 'CHECKED')
4. Lab system → Updates lab flag (COMPLETELAB = 'Y')
5. ERP sync → Updates AS400 flag (TRANSFERAS400 = 'Y')
6. **PACK_UPDATEPACKINGPALLET** → Mark shipped (FLAG = 'SHIPPED')

---

## Related Procedures

**Upstream**: [290-PACK_INSERTPACKINGPALLET.md](./290-PACK_INSERTPACKINGPALLET.md) - Creates pallet to update
**Upstream**: [291-PACK_INSPACKINGPALLETDETAIL.md](./291-PACK_INSPACKINGPALLETDETAIL.md) - Adds lots before update
**Related**: [295-PACK_SEARCHPALLETLIST.md](./295-PACK_SEARCHPALLETLIST.md) - Finds pallets to update
**Related**: [286-PACK_CANCELPALLET.md](./286-PACK_CANCELPALLET.md) - Alternative to updating

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_UPDATEPACKINGPALLET()`
**Lines**: Likely in update section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_UPDATEPACKINGPALLET(PACK_UPDATEPACKINGPALLETParameter para)`
**Lines**: 2065-2082

**Implementation Note**:
**Validation Rules**:
- Cannot update SHIPPED pallet to NEW
- Cannot update CANCELLED pallet
- CHECKBY and CHECKDATE typically set together
- FLAG = 'SHIPPED' requires LAB = 'Y' and AS400 = 'Y'

---

**File**: 296/296 | **Progress**: 100.0%
