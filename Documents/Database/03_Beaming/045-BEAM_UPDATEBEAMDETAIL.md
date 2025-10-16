# BEAM_UPDATEBEAMDETAIL

**Procedure Number**: 045 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update beam production data during or after production |
| **Operation** | UPDATE |
| **Called From** | BeamingDataService.cs:1300 → BEAM_UPDATEBEAMDETAIL() (3 overloads) |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN) - Overload 1 (Full Update with Doff)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number (barcode) |
| `P_LENGTH` | NUMBER | ⬜ | Actual beam length (meters) |
| `P_ENDDATE` | DATE | ⬜ | Doff timestamp |
| `P_SPEED` | NUMBER | ⬜ | Average machine speed (m/min) |
| `P_HARDL` | NUMBER | ⬜ | Hardness - left side |
| `P_HARDN` | NUMBER | ⬜ | Hardness - center |
| `P_HARDR` | NUMBER | ⬜ | Hardness - right side |
| `P_STANDTENSION` | NUMBER | ⬜ | Beam stand tension |
| `P_WINDTENSION` | NUMBER | ⬜ | Winding tension |
| `P_INSIDE` | NUMBER | ⬜ | Inside beam width (mm) |
| `P_OUTSIDE` | NUMBER | ⬜ | Outside beam width (mm) |
| `P_FULL` | NUMBER | ⬜ | Full beam width (mm) |
| `P_DOFFBY` | VARCHAR2(50) | ⬜ | Operator who doffed |
| `P_TENSION_ST1` | NUMBER | ⬜ | Station 1 tension measurement |
| `P_TENSION_ST2` | NUMBER | ⬜ | Station 2 tension measurement |
| `P_TENSION_ST3` | NUMBER | ⬜ | Station 3 tension measurement |
| `P_TENSION_ST4` | NUMBER | ⬜ | Station 4 tension measurement |
| `P_TENSION_ST5` | NUMBER | ⬜ | Station 5 tension measurement |
| `P_TENSION_ST6` | NUMBER | ⬜ | Station 6 tension measurement |
| `P_TENSION_ST7` | NUMBER | ⬜ | Station 7 tension measurement |
| `P_TENSION_ST8` | NUMBER | ⬜ | Station 8 tension measurement |
| `P_TENSION_ST9` | NUMBER | ⬜ | Station 9 tension measurement |
| `P_TENSION_ST10` | NUMBER | ⬜ | Station 10 tension measurement |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator making the update |

### Input (IN) - Overload 2 (Partial Update without Doff)

Same as Overload 1 but **without** `P_ENDDATE` and `P_DOFFBY` parameters

### Input (IN) - Overload 3 (Remark Only)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number |
| `P_REMARK` | VARCHAR2(200) | ⬜ | Production remarks/notes |

### Output (OUT)

None (Returns boolean)

### Returns

| Type | Description |
|------|-------------|
| `Boolean` | True if update successful, False if failed |

---

## Business Logic (What it does and why)

Updates beam production data at various stages: during production (partial), when doffing (full), or for remark changes.

**Purpose**: As beam production progresses, this procedure updates the production record with:
1. **During Production**: Intermediate measurements (tension, hardness, width)
2. **When Doffing**: Final measurements + end time + operator
3. **Anytime**: Remarks/notes (separate overload)

**When Used**:
- **Doff Beam**: Operator completes beam and enters final measurements (Overload 1)
- **Mid-Production**: Operator adjusts parameters and records measurements (Overload 2)
- **Add Notes**: Supervisor adds remarks to beam record (Overload 3)
- **Quality Check**: Operator records hardness/width measurements

**Business Rules**:
- All overloads require BEAMERNO + BEAMLOT (primary key)
- Empty/null BEAMERNO causes failure (returns false)
- **Overload 1** (with ENDDATE/DOFFBY): Used for final doff operation
- **Overload 2** (without ENDDATE/DOFFBY): Used for mid-production updates
- **Overload 3** (remark only): Quick remark update without changing measurements
- All numeric parameters optional (can update subset of fields)
- 10 beam stand tension values (for multi-stand beamers)
- FLAG typically updated to 'D' (Doffed) when P_ENDDATE provided

**Usage Patterns**:

**Pattern 1: Doff Beam (Complete Production)**
```csharp
// Operator clicks "Doff" button after beam complete
bool success = BeamingDataService.Instance.BEAM_UPDATEBEAMDETAIL(
    beamerNo,
    beamLot,
    finalLength,         // Measured length
    DateTime.Now,        // Doff time
    avgSpeed,           // Calculated speed
    hardnessL,          // Measured hardness (left)
    hardnessN,          // Measured hardness (center)
    hardnessR,          // Measured hardness (right)
    standTension,       // Final tension
    windTension,        // Final tension
    insideWidth,        // Measured width
    outsideWidth,       // Measured width
    fullWidth,          // Measured width
    currentOperatorID,  // Doff operator
    tension1, ... tension10, // 10 stand tensions
    currentOperatorID   // Update operator
);
```

**Pattern 2: Mid-Production Update**
```csharp
// Operator updates measurements during production
bool success = BeamingDataService.Instance.BEAM_UPDATEBEAMDETAIL(
    beamerNo,
    beamLot,
    currentLength,      // Current length (not final)
    avgSpeed,           // Current speed
    hardnessL, hardnessN, hardnessR,
    standTension, windTension,
    insideWidth, outsideWidth, fullWidth,
    tension1, ... tension10
    // No ENDDATE, no DOFFBY - beam still in process
);
```

**Pattern 3: Add Remark**
```csharp
// Supervisor adds note to beam record
bool success = BeamingDataService.Instance.BEAM_UPDATEBEAMDETAIL_REMARK(
    beamerNo,
    beamLot,
    "Tension adjusted at 500m due to yarn break"
);
```

---

## Related Procedures

**Upstream**:
- [040-BEAM_INSERTBEAMINGDETAIL.md](./040-BEAM_INSERTBEAMINGDETAIL.md) - Creates record that this updates

**Downstream**:
- [033-BEAM_GETBEAMROLLDETAIL.md](./033-BEAM_GETBEAMROLLDETAIL.md) - Retrieves updated data
- [044-BEAM_TRANFERSLIP.md](./044-BEAM_TRANFERSLIP.md) - Uses updated data for transfer slip

**Similar**:
- [025-WARP_UPDATEWARPINGPROCESS.md](../02_Warping/025-WARP_UPDATEWARPINGPROCESS.md) - Warping update equivalent
- Also used in **DrawingDataService** (cross-module usage noted in TODO)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Methods**:
- `BEAM_UPDATEBEAMDETAIL()` (Overload 1 - with doff) - Line 1298-1360
- `BEAM_UPDATEBEAMDETAIL()` (Overload 2 - without doff) - Line 1364-1423
- `BEAM_UPDATEBEAMDETAIL_REMARK()` (Overload 3 - remark only) - Line 1465-1498

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_UPDATEBEAMDETAILParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 45/296 | **Progress**: 15.2%
