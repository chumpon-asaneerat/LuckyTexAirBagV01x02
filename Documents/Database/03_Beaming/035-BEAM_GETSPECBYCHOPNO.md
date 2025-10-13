# BEAM_GETSPECBYCHOPNO

**Procedure Number**: 035 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beaming specifications by item prepare code |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:150 → BEAM_GETSPECBYCHOPNO() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code (product specification) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CHOPNO` | VARCHAR2 | Item code/chop number |
| `NOWARPBEAM` | NUMBER | Number of warp beams required |
| `TOTALYARN` | NUMBER | Total yarn ends count |
| `TOTALKEBA` | NUMBER | Total keba (yarn groups) |
| `BEAMLENGTH` | NUMBER | Target beam length (meters) |
| `MAXHARDNESS` | NUMBER | Maximum hardness specification |
| `MINHARDNESS` | NUMBER | Minimum hardness specification |
| `MAXBEAMWIDTH` | NUMBER | Maximum beam width (mm) |
| `MINBEAMWIDTH` | NUMBER | Minimum beam width (mm) |
| `MAXSPEED` | NUMBER | Maximum machine speed (m/min) |
| `MINSPEED` | NUMBER | Minimum machine speed (m/min) |
| `MAXYARNTENSION` | NUMBER | Maximum yarn tension |
| `MINYARNTENSION` | NUMBER | Minimum yarn tension |
| `MAXWINDTENSION` | NUMBER | Maximum winding tension |
| `MINWINDTENSION` | NUMBER | Minimum winding tension |
| `COMBTYPE` | VARCHAR2 | Comb type specification |
| `COMBPITCH` | NUMBER | Comb pitch measurement |
| `TOTALBEAM` | NUMBER | Total beams to be produced |

---

## Business Logic (What it does and why)

Retrieves complete beaming machine setup specifications when operator selects an item prepare code.

**Purpose**: When starting a new beaming setup, operator selects the product (item prepare code). This procedure returns all technical specifications needed to:
- Set up the beaming machine correctly
- Configure process parameters (speed, tension, hardness)
- Know how many beams to produce
- Validate production against quality specifications

**When Used**:
- **New Beaming Setup**: Operator starts new beaming batch
- **Machine Configuration**: System displays target parameters for operator
- **Quality Validation**: System checks if actual values are within spec ranges
- **Production Planning**: Shows how many beams are needed for the order

**Business Rules**:
- Each item prepare code has one set of beaming specifications
- MIN/MAX values define acceptable quality ranges
- NOWARPBEAM indicates how many warp beams combine into one beam
- TOTALBEAM indicates total production quantity for the order
- Empty result means item prepare code not found or no beaming spec configured

**Data Flow**:
1. Operator selects item prepare code from dropdown
2. This procedure retrieves specifications
3. UI displays target values for operator reference
4. During production, system validates actual values against MIN/MAX specs
5. System warns operator if values go outside specification ranges

---

## Related Procedures

**Upstream**:
- [ITM_GETITEMPREPARELIST.md](../17_Master_Data/ITM_GETITEMPREPARELIST.md) - Gets list of item prepare codes
- [BEAM_INSERTBEAMNO.md](./BEAM_INSERTBEAMNO.md) - Creates setup using these specifications

**Downstream**:
- [BEAM_INSERTBEAMINGDETAIL.md](./BEAM_INSERTBEAMINGDETAIL.md) - Records actual production data
- [BEAM_UPDATEBEAMDETAIL.md](./BEAM_UPDATEBEAMDETAIL.md) - Updates production data with actual values

**Similar**:
- [009-WARP_GETSPECBYCHOPNOANDMC.md](../02_Warping/009-WARP_GETSPECBYCHOPNOANDMC.md) - Warping specifications (includes machine-specific data)
- [DRAW_GETSPECBYCHOPNO.md](../04_Drawing/DRAW_GETSPECBYCHOPNO.md) - Drawing specifications

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_GETSPECBYCHOPNO()`
**Line**: 144-203

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_GETSPECBYCHOPNOParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 35/296 | **Progress**: 11.8%
