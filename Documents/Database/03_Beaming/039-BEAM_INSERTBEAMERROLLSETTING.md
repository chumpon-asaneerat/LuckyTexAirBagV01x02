# BEAM_INSERTBEAMERROLLSETTING

**Procedure Number**: 039 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create warp-to-beam mapping for traceability |
| **Operation** | INSERT |
| **Called From** | BeamingDataService.cs:1504 → BEAM_INSERTBEAMERROLLSETTING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |
| `P_WARPERHEADNO` | VARCHAR2(50) | ✅ | Warp setup/batch number (from M02-Warping) |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Individual warp roll barcode |

### Output (OUT)

None (Returns boolean via procedure result)

### Returns

| Type | Description |
|------|-------------|
| `Boolean` | True if insert successful, False if failed |

---

## Business Logic (What it does and why)

Creates **traceability mapping** between warp rolls (from M02-Warping) and beaming setup, enabling forward/backward material tracking.

**Purpose**: When operator creates a new beaming setup and selects which warp rolls to use, this procedure records each warp roll mapping. This creates the critical traceability link:

```
Warp Roll (WARPERLOT) → Beaming Setup (BEAMERNO) → Beam Rolls (BEAMLOT) → Fabric (DOFFNO)
```

**When Used**:
- **New Beaming Setup**: After operator selects warp rolls, system calls this procedure for EACH selected warp roll
- **Batch Operation**: Called multiple times (typically 2-10 warp rolls per beaming setup)
- **Setup Wizard**: Part of beaming setup creation workflow

**Business Rules**:
- Called once per warp roll in the beaming setup
- Three parameters required (no nulls allowed)
- If any parameter is empty/null, operation fails (returns false)
- Should be wrapped in transaction with other setup creation steps
- Creates permanent traceability record (typically not deleted)

**Data Flow (Typical Setup with 5 Warp Rolls)**:
1. Operator creates beaming setup (BEAM_INSERTBEAMNO)
2. Operator selects 5 warp rolls from list
3. System calls this procedure 5 times:
   - Insert mapping: BEAMERNO + WARPHEADNO1 + WARPERLOT1
   - Insert mapping: BEAMERNO + WARPHEADNO1 + WARPERLOT2
   - Insert mapping: BEAMERNO + WARPHEADNO1 + WARPERLOT3
   - Insert mapping: BEAMERNO + WARPHEADNO2 + WARPERLOT4
   - Insert mapping: BEAMERNO + WARPHEADNO2 + WARPERLOT5
4. All mappings committed in single transaction
5. Later: BEAM_GETWARPROLLBYBEAMERNO retrieves all 5 mappings

---

## Related Procedures

**Upstream**:
- [037-BEAM_GETWARPNOBYITEMPREPARE.md](./037-BEAM_GETWARPNOBYITEMPREPARE.md) - Gets warp setups for selection
- [WARP_GETWARPERLOTBYHEADNO.md](./WARP_GETWARPERLOTBYHEADNO.md) - Gets individual warp rolls
- [BEAM_INSERTBEAMNO.md](./BEAM_INSERTBEAMNO.md) - Creates beaming setup first

**Downstream**:
- [038-BEAM_GETWARPROLLBYBEAMERNO.md](./038-BEAM_GETWARPROLLBYBEAMERNO.md) - Retrieves the mappings created here
- Traceability reports use this data extensively

**Similar**:
- No exact equivalent (beaming-specific traceability operation)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_INSERTBEAMERROLLSETTING()`
**Line**: 1502-1539

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_INSERTBEAMERROLLSETTINGParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 39/296 | **Progress**: 13.2%
