# FINISHING_CANCEL

**Procedure Number**: 115 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Cancel finishing operation (coating/scouring/dryer) and remove record |
| **Operation** | DELETE |
| **Tables** | tblFinishingCoating, tblFinishingScouring, tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:3524 → FINISHING_CANCEL() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2 | ✅ | Weaving lot number |
| `P_FINISHLOT` | VARCHAR2 | ✅ | Finishing lot number to cancel |
| `P_PROCESS` | VARCHAR2 | ✅ | Process type (COATING/SCOURING/DRYER) |
| `P_OPERATOR` | VARCHAR2 | ⬜ | Operator who authorized cancellation |

### Output (OUT)

None (returns empty result object for success/failure indication)

---

## Business Logic

**Purpose**: Cancel a finishing operation that was started in error, or remove a record that cannot be completed due to machine failure, material issues, or other problems. This is a DELETE operation that removes the finishing record entirely.

**When Used**:

**Scenario 1: Wrong Lot Loaded (Operator Error)**
1. Operator loads weaving lot WL-2025-100 onto DRYER-01
2. System creates finishing record FN-2025-500
3. Operator realizes wrong lot (should have been WL-2025-101)
4. Operator stops machine immediately (before process starts)
5. Supervisor authorizes cancellation
6. System calls FINISHING_CANCEL:
   - `P_WEAVLOT` = WL-2025-100
   - `P_FINISHLOT` = FN-2025-500
   - `P_PROCESS` = DRYER
   - `P_OPERATOR` = SUPERVISOR-01
7. Record deleted, operator can reload correct lot
8. New finishing record created for correct lot

**Scenario 2: Machine Failure During Setup**
1. Operator sets conditions on COAT-02 for lot FN-2025-501
2. Machine fails during condition setup (heater malfunction)
3. Cannot proceed with coating process
4. Maintenance required (4+ hours downtime)
5. Supervisor decides to move lot to COAT-03
6. Calls FINISHING_CANCEL to remove failed setup:
   - `P_PROCESS` = COATING
   - `P_FINISHLOT` = FN-2025-501
7. Original record deleted
8. New record created on COAT-03

**Scenario 3: Material Quality Issue Discovered**
1. Weaving lot WL-2025-102 loaded on SCOUR-01
2. Scouring conditions set, record created
3. QC inspector samples fabric, finds defect (wrong yarn used)
4. Lot fails QC before scouring starts
5. Supervisor cancels scouring operation
6. Calls FINISHING_CANCEL:
   - `P_PROCESS` = SCOURING
   - `P_FINISHLOT` = FN-2025-502
7. Record removed, lot sent back to weaving for rework

**Scenario 4: Duplicate Entry (Data Entry Error)**
1. Operator scans lot twice accidentally
2. Two finishing records created for same lot
3. Supervisor identifies duplicate in dashboard
4. Cancels duplicate record (keeps first entry)
5. FINISHING_CANCEL removes duplicate
6. Production continues with correct single record

**Business Rules**:
- **Authorization required**: Only supervisors should cancel operations
- **Operator tracking**: Record who authorized cancellation (audit trail)
- **Process type required**: Must specify COATING/SCOURING/DRYER
  - Ensures deletion from correct table
  - Prevents accidental deletion of wrong process type
- **Both lot numbers required**: Weaving lot AND finishing lot
  - Double verification to prevent wrong cancellation
- **Permanent deletion**: Record is DELETED, not marked as cancelled
- **When to use**:
  - Before process actually starts (condition setup stage)
  - When process cannot be completed (machine failure)
  - Wrong lot loaded (operator error)
  - Material quality issue discovered
- **When NOT to use**:
  - Process already completed (use different procedure for corrections)
  - After production has started significantly (too late to cancel)
  - For quality failures AFTER processing (use rejection workflow instead)

**Safety Checks (Recommended)**:
- Verify process not already finished (STATUS != 'FINISH')
- Check no downstream operations started (inspection, sampling)
- Require supervisor authorization level
- Log cancellation for audit trail

**Alternative Actions**:
- If process already started: Complete process, then reject at inspection
- If only data error: Use UPDATE procedures to correct (don't delete)
- If quality issue after processing: Use quality hold/rejection workflow

---

## Related Procedures

**Upstream**:
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Creates coating record (to cancel)
- [099-FINISHING_INSERTSCOURING.md](./099-FINISHING_INSERTSCOURING.md) - Creates scouring record (to cancel)
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates dryer record (to cancel)

**Similar**:
- WEAV_DELETEWEAVINGLOT (M05) - Cancel weaving operation
- WEAV_DELETEMCSTOP (M05) - Delete machine stop record
- WARP_CANCELCREELSETUP (M02) - Cancel warping setup
- WEAVE_CANCELLOOMSETUP (M05) - Cancel loom setup

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_CANCEL(string WEAVLOT, string FINISHLOT, string PROCESS, string OPERATOR)`
**Lines**: 3523-3563

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_CANCELParameter`
**Lines**: 9231-9237

**Result Class**: `FINISHING_CANCELResult`
**Lines**: 9243-9245 (empty)

**Method**: `FINISHING_CANCEL(FINISHING_CANCELParameter para)`
**Lines**: 28983-29015 (estimated)

---

**File**: 115/296 | **Progress**: 38.9%
