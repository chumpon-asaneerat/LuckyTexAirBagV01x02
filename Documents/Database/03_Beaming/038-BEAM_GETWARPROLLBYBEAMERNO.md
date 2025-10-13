# BEAM_GETWARPROLLBYBEAMERNO

**Procedure Number**: 038 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get warp rolls used in a beaming setup for traceability |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:849 → BEAM_GETWARPROLLBYBEAMERNO() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2 | Beamer setup/batch number |
| `WARPHEADNO` | VARCHAR2 | Warp setup/batch number (from M02-Warping) |
| `WARPERLOT` | VARCHAR2 | Individual warp roll barcode |

---

## Business Logic (What it does and why)

Retrieves the list of warp rolls (from M02-Warping) that were used as input material for a specific beaming setup.

**Purpose**: This is a critical **traceability query** that links beaming production back to its source warp rolls. When supervisor or quality team needs to:
- Track which warp beams went into a specific beaming batch
- Trace back from finished beam to raw warp materials
- Investigate quality issues (which warp rolls may have caused defects)
- Generate production reports showing material consumption

**When Used**:
- **Traceability Reports**: Showing forward/backward material flow
- **Quality Investigation**: Finding which warp rolls were used
- **Edit Beaming Setup**: Display list of warp rolls in the setup
- **Production Analysis**: Understanding material usage patterns
- **Transfer Slip**: Document source materials on transfer documents

**Business Rules**:
- Multiple warp rolls combine into one beaming setup (many-to-one relationship)
- Each warp roll (WARPERLOT) belongs to a warp setup (WARPHEADNO)
- Mapping is created during beaming setup via BEAM_INSERTBEAMERROLLSETTING
- This is historical data - shows what was SET UP, not necessarily what was used
- Empty result means beaming setup has no warp roll mappings (data issue)

**Data Flow**:
1. During beaming setup, operator selects warp rolls
2. System calls BEAM_INSERTBEAMERROLLSETTING to record mappings
3. Later, this procedure retrieves the mappings for display/reports
4. Used in forward traceability: WARPERLOT → BEAMERNO → (next process)

---

## Related Procedures

**Upstream**:
- [BEAM_INSERTBEAMERROLLSETTING.md](./BEAM_INSERTBEAMERROLLSETTING.md) - Creates the warp-to-beam mappings
- [037-BEAM_GETWARPNOBYITEMPREPARE.md](./037-BEAM_GETWARPNOBYITEMPREPARE.md) - Gets warp setups for selection

**Downstream**:
- [WARP_GETWARPERLOTBYHEADNO.md](./WARP_GETWARPERLOTBYHEADNO.md) - Gets details of individual warp rolls
- [011-WARP_GETWARPERLOTBYHEADNO.md](../02_Warping/011-WARP_GETWARPERLOTBYHEADNO.md) - Warping module's view of same data

**Similar**:
- No direct equivalent (beaming-specific traceability query)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_GETWARPROLLBYBEAMERNO()`
**Line**: 843-885

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_GETWARPROLLBYBEAMERNOParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 38/296 | **Progress**: 12.8%
