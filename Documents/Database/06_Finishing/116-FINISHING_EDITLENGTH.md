# FINISHING_EDITLENGTH

**Procedure Number**: 116 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update/correct length measurements for finished fabric after process completion |
| **Operation** | UPDATE |
| **Tables** | tblFinishingCoating, tblFinishingScouring, tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:3566 → FINISHING_EDITLENGTH() |
| **Frequency** | Low-Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2 | ✅ | Weaving lot number |
| `P_FINISHLOT` | VARCHAR2 | ✅ | Finishing lot number to update |
| `P_PROCESS` | VARCHAR2 | ✅ | Process type (COATING/SCOURING/DRYER) |
| `P_OPERATOR` | VARCHAR2 | ⬜ | Operator who made correction |
| `P_LENGTH1` | NUMBER | ⬜ | Length measurement 1 (meters) |
| `P_LENGTH2` | NUMBER | ⬜ | Length measurement 2 (meters) |
| `P_LENGTH3` | NUMBER | ⬜ | Length measurement 3 (meters) |
| `P_LENGTH4` | NUMBER | ⬜ | Length measurement 4 (meters) |
| `P_LENGTH5` | NUMBER | ⬜ | Length measurement 5 (meters) |
| `P_LENGTH6` | NUMBER | ⬜ | Length measurement 6 (meters) |
| `P_LENGTH7` | NUMBER | ⬜ | Length measurement 7 (meters) |
| `P_TOTALLENGTH` | NUMBER | ⬜ | Total length (sum or calculated) |

### Output (OUT)

None (returns empty result object for success/failure indication)

---

## Business Logic

**Purpose**: Correct or update length measurements after finishing process is complete. Length measurements are critical for inventory accuracy, customer invoicing, and quality control. This procedure allows authorized personnel to fix measurement errors without recreating the entire finishing record.

**When Used**:

**Scenario 1: Measurement Entry Error (Immediate Correction)**
1. Operator completes dryer process for lot FN-2025-500
2. Enters final lengths:
   - LENGTH1: 245.5 meters
   - LENGTH2: 243.0 meters (ERROR - should be 343.0)
   - LENGTH3: 244.8 meters
3. Operator notices error immediately after saving
4. Supervisor authorizes correction
5. Calls FINISHING_EDITLENGTH:
   - `P_FINISHLOT` = FN-2025-500
   - `P_PROCESS` = DRYER
   - `P_LENGTH2` = 343.0 (corrected value)
   - `P_OPERATOR` = SUPERVISOR-01
6. Record updated, inventory corrected

**Scenario 2: Remeasurement After Quality Issue**
1. Fabric lot FN-2025-501 processed through coating
2. Original lengths recorded during processing
3. Quality inspector finds issue: fabric appears shorter than recorded
4. QC manager orders remeasurement
5. Operator remeasures entire lot with calibrated equipment:
   - New measurements differ from original (shrinkage discovered)
6. Calls FINISHING_EDITLENGTH with corrected measurements:
   - All 7 length fields updated
   - `P_TOTALLENGTH` recalculated
   - `P_OPERATOR` = QC-MANAGER-01
7. Inventory and invoicing updated with correct values

**Scenario 3: Post-Process Adjustment (Roll Splitting)**
1. Large fabric roll processed: 500 meters total
2. After inspection, roll split into smaller segments:
   - Segment 1: 150 meters
   - Segment 2: 175 meters
   - Segment 3: 175 meters
3. Need to update finishing record to reflect actual segments
4. Calls FINISHING_EDITLENGTH:
   - `P_LENGTH1` = 150
   - `P_LENGTH2` = 175
   - `P_LENGTH3` = 175
   - `P_TOTALLENGTH` = 500
5. Record updated for accurate inventory tracking

**Scenario 4: Customer-Required Remeasurement**
1. Customer inspection reveals length discrepancy
2. Customer requests remeasurement with their own equipment
3. Joint measurement performed (customer + company QC)
4. Agreed measurements recorded
5. FINISHING_EDITLENGTH updates record with agreed values
6. Updated measurements used for final invoicing

**Business Rules**:
- **Process type required**: Must specify COATING/SCOURING/DRYER
  - Determines which table to update
  - Prevents updating wrong process record
- **Authorization**: Should require supervisor approval for corrections
- **Operator tracking**: Record who made correction (audit trail)
- **Both lot numbers**: Weaving lot + finishing lot (verification)
- **Multiple measurements**: Up to 7 length readings supported
  - Different rolls/segments from same finishing run
  - Statistical analysis (variation between readings)
- **Total length**: Can be sum of individual readings or independent measurement
- **When to use**:
  - Data entry errors discovered immediately
  - Remeasurement ordered by QC
  - Post-process adjustments (splitting, combining)
  - Customer-required corrections
  - Calibration corrections
- **When NOT to use**:
  - Process not yet complete (use UPDATE procedures during process)
  - Wrong lot processed (use CANCEL procedure)
  - Before authorization obtained

**Why Multiple Length Measurements?**
- **LENGTH1-LENGTH7**: Different segments/rolls from same finishing run
- **Statistical Quality Control**: Monitor consistency across rolls
- **Inventory Precision**: Track each segment individually
- **Customer Requirements**: Some customers require multiple measurements
- **Process Validation**: Verify uniform processing across entire lot

**Audit Trail Importance**:
- Length corrections affect:
  - **Inventory value** (meters × price)
  - **Customer invoicing** (sold by meter)
  - **Material yields** (efficiency calculations)
  - **Quality metrics** (process consistency)
- Must track who changed what and when

---

## Related Procedures

**Upstream**:
- [094-FINISHING_UPDATECOATING.md](./094-FINISHING_UPDATECOATING.md) - Update coating (includes lengths)
- [102-FINISHING_UPDATESCOURING.md](./102-FINISHING_UPDATESCOURING.md) - Update scouring (includes lengths)
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - Update dryer (includes lengths)

**Similar**:
- INS_EDIT100TESTRECORD (M08) - Edit inspection measurements
- PACK_EDITPACKINGPALLETDETAIL (M13) - Edit packing lengths

**Downstream**:
- Inventory management system (uses updated lengths)
- Customer invoicing (billing based on meters)
- Quality reports (measurement accuracy)

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_EDITLENGTH(...)`
**Lines**: 3565-3614

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_EDITLENGTHParameter`
**Lines**: 8872-8886

**Result Class**: `FINISHING_EDITLENGTHResult`
**Lines**: 8892-8894 (empty)

**Method**: `FINISHING_EDITLENGTH(FINISHING_EDITLENGTHParameter para)`
**Lines**: 28455-28505 (estimated)

---

**File**: 116/296 | **Progress**: 39.2%
