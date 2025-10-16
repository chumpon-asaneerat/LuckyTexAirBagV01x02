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
| `STARTDATE` | DATE | Start timestamp |
| `ENDDATE` | DATE | Completion timestamp |
| `LENGTH` | NUMBER | Beam length (meters) |
| `SPEED` | NUMBER | Average speed |
| `BEAMSTANDTENSION` | NUMBER | Stand tension |
| `WINDINGTENSION` | NUMBER | Winding tension |
| `HARDNESS_L` | NUMBER | Hardness left side |
| `HARDNESS_N` | NUMBER | Hardness center |
| `HARDNESS_R` | NUMBER | Hardness right side |
| `INSIDE_WIDTH` | NUMBER | Inside width |
| `OUTSIDE_WIDTH` | NUMBER | Outside width |
| `FULL_WIDTH` | NUMBER | Full width |
| `STARTBY` | VARCHAR2(50) | Start operator |
| `DOFFBY` | VARCHAR2(50) | Doff operator |
| `BEAMMC` | VARCHAR2(50) | Machine number |
| `FLAG` | VARCHAR2(10) | Status flag |
| `REMARK` | VARCHAR2(500) | Notes |
| `TENSION_ST1` | NUMBER | Station 1 tension measurement |
| `TENSION_ST2` | NUMBER | Station 2 tension measurement |
| `TENSION_ST3` | NUMBER | Station 3 tension measurement |
| `TENSION_ST4` | NUMBER | Station 4 tension measurement |
| `TENSION_ST5` | NUMBER | Station 5 tension measurement |
| `TENSION_ST6` | NUMBER | Station 6 tension measurement |
| `TENSION_ST7` | NUMBER | Station 7 tension measurement |
| `TENSION_ST8` | NUMBER | Station 8 tension measurement |
| `TENSION_ST9` | NUMBER | Station 9 tension measurement |
| `TENSION_ST10` | NUMBER | Station 10 tension measurement |
| `EDITBY` | VARCHAR2(50) | Last editor |
| `OLDBEAMNO` | VARCHAR2(50) | Old beam number |
| `EDITDATE` | DATE | Last edit date |
| `KEBA` | NUMBER | Keba count |
| `MISSYARN` | NUMBER | Missing yarn count |
| `OTHER` | NUMBER | Other info |

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
