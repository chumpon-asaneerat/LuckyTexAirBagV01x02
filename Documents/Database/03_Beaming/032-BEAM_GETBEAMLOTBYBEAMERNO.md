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
| `HARDNESS_L/N/R` | NUMBER | Hardness 3 points |
| `BEAMSTANDTENSION` | NUMBER | Stand tension |
| `WINDINGTENSION` | NUMBER | Winding tension |
| `INSIDE_WIDTH` | NUMBER | Inside width |
| `OUTSIDE_WIDTH` | NUMBER | Outside width |
| `FULL_WIDTH` | NUMBER | Full width |
| `STARTBY` | VARCHAR2(50) | Start operator |
| `DOFFBY` | VARCHAR2(50) | Doff operator |
| `FLAG` | VARCHAR2(10) | Status flag |
| `BEAMMC` | VARCHAR2(50) | Machine number |
| `REMARK` | VARCHAR2(500) | Notes |
| `TENSION_ST1-ST10` | NUMBER | 10 station tensions |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2(50) | Last editor |
| `OLDBEAMNO` | VARCHAR2(50) | Old beam number |
| `KEBA` | NUMBER | Keba count |
| `MISSYARN` | NUMBER | Missing yarn count |
| `OTHER` | VARCHAR2(200) | Other info |

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
