# BEAM_GETBEAMLOTBYBEAMERNO

**Procedure Number**: 032 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beam lots/rolls produced by a specific beamer setup |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:435 → BEAM_GETBEAMLOTBYBEAMERNO() |
| **Frequency** | High (viewing setup details) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup number |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2(50) | Beamer setup number |
| `BEAMLOT` | VARCHAR2(50) | Beam lot number |
| `BEAMNO` | VARCHAR2(50) | Beam number |
| `STARTDATE` | DateTime? | Start timestamp |
| `ENDDATE` | DateTime? | Completion timestamp |
| `LENGTH` | Decimal? | Beam length (meters) |
| `SPEED` | Decimal? | Average speed |
| `BEAMSTANDTENSION` | Decimal? | Stand tension |
| `WINDINGTENSION` | Decimal? | Winding tension |
| `HARDNESS_L` | Decimal? | Hardness left side |
| `HARDNESS_N` | Decimal? | Hardness center |
| `HARDNESS_R` | Decimal? | Hardness right side |
| `INSIDE_WIDTH` | Decimal? | Inside width |
| `OUTSIDE_WIDTH` | Decimal? | Outside width |
| `FULL_WIDTH` | Decimal? | Full width |
| `STARTBY` | String | Start operator |
| `DOFFBY` | String | Doff operator |
| `BEAMMC` | String | Machine number |
| `FLAG` | String | Status flag |
| `REMARK` | String | Notes |
| `TENSION_ST1` | Decimal? | Station 1 tension measurement |
| `TENSION_ST2` | Decimal? | Station 2 tension measurement |
| `TENSION_ST3` | Decimal? | Station 3 tension measurement |
| `TENSION_ST4` | Decimal? | Station 4 tension measurement |
| `TENSION_ST5` | Decimal? | Station 5 tension measurement |
| `TENSION_ST6` | Decimal? | Station 6 tension measurement |
| `TENSION_ST7` | Decimal? | Station 7 tension measurement |
| `TENSION_ST8` | Decimal? | Station 8 tension measurement |
| `TENSION_ST9` | Decimal? | Station 9 tension measurement |
| `TENSION_ST10` | Decimal? | Station 10 tension measurement |
| `EDITBY` | String | Last editor |
| `OLDBEAMNO` | String | Old beam number |
| `EDITDATE` | DateTime? | Last edit date |
| `KEBA` | Decimal? | Keba count |
| `MISSYARN` | Decimal? | Missing yarn count |
| `OTHER` | Decimal? | Other info |

---

## Business Logic (What it does and why)

Retrieves all beam rolls produced from a specific beamer setup. Shows complete production history and quality parameters for all beams in the setup.

**Workflow**:
1. User views beamer setup details
2. System loads all beams produced
3. Shows list of completed beam rolls
4. User can drill down into individual beams

**Business Rules**:
- Shows all beams from setup (may be multiple)
- Includes quality parameters for each beam
- Shows traceability from setup to individual beams
- Returns beams in production order

**Use Cases**:
- Setup review
- Quality analysis across multiple beams
- Production history
- Traceability reporting

---

## Related Procedures

**Similar**: [011-WARP_GETWARPERLOTBYHEADNO.md](../02_Warping/011-WARP_GETWARPERLOTBYHEADNO.md)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_GETBEAMLOTBYBEAMERNO()`
**Line**: 435-510

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_GETBEAMLOTBYBEAMERNOParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 032/296 | **Progress**: 10.8%
