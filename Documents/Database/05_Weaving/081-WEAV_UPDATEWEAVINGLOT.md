# WEAV_UPDATEWEAVINGLOT

**Procedure Number**: 081 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Updates weaving lot production data (length, shift, density, tension, waste) |
| **Operation** | UPDATE |
| **Called From** | WeavingDataService.cs:1955 → WEAV_UPDATEWEAVINGLOT() |
| **Frequency** | High (Production updates) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2 | ✅ | Weaving lot number to update |
| `P_LENGHT` | NUMBER | ⬜ | Fabric length (meters) - note typo "LENGHT" |
| `P_SHIFT` | VARCHAR2 | ⬜ | Production shift (A/B/C) |
| `P_DENSITYWARP` | NUMBER | ⬜ | Warp density (ends/cm) |
| `P_DENSITYWEFT` | NUMBER | ⬜ | Weft density (picks/cm) |
| `P_TENSION` | NUMBER | ⬜ | Warp tension setting |
| `P_WASTE` | NUMBER | ⬜ | Waste amount (meters) |

### Output

**Returns**: Boolean (true = success, false = error)

---

## Business Logic

Updates production data for an existing weaving lot. This procedure allows operators or supervisors to:

1. **Correct Production Data**: Fix errors in length, shift, or other parameters
2. **Update Density Values**: Record actual warp/weft density measurements
3. **Adjust Tension**: Document tension adjustments made during production
4. **Record Waste**: Update waste quantities after quality inspection
5. **Shift Changes**: Correct shift assignments if production spans multiple shifts

**Business Rules**:
- Weaving lot (P_WEAVINGLOT) must exist in database
- All data parameters are optional - only provided values are updated
- Returns boolean: true if update successful, false if error/not found
- C# validates database connection before attempting update

**Common Update Scenarios**:

1. **Length Correction**: Operator initially estimates length, then updates with actual measured length
2. **Density Adjustment**: After quality inspection, actual density measurements are recorded
3. **Waste Recording**: After cutting out defective sections, total waste is updated
4. **Shift Update**: If doffing happens after shift change, shift assignment is corrected

**Note**: Parameter name "P_LENGHT" appears to be a typo (should be "LENGTH") but retained for database compatibility.

**Typical Usage Flow**:
1. Production completes or operator needs to make corrections
2. Operator enters corrected values in UI
3. System calls WEAV_UPDATEWEAVINGLOT with new values
4. Database updates the weaving lot record
5. Boolean result indicates success/failure to UI

---

## Related Procedures

**Upstream**:
- [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Initial weaving lot creation

**Related**:
- [065-WEAV_DELETEWEAVINGLOT.md](./065-WEAV_DELETEWEAVINGLOT.md) - Delete weaving lot (soft delete)
- [074-WEAV_GETWEAVELISTBYBEAMROLL.md](./074-WEAV_GETWEAVELISTBYBEAMROLL.md) - View weaving lot data

**Similar**:
- WARP_UPDATEWARPINGPROCESS - Update warping production data
- BEAM_UPDATEBEAMDETAIL - Update beaming production data

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_UPDATEWEAVINGLOT()`
**Lines**: 1955-1988

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_UPDATEWEAVINGLOT(WEAV_UPDATEWEAVINGLOTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 081/296 | **Progress**: 27.4%
