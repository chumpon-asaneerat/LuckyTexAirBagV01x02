# BEAM_BEAMLIST

**Procedure Number**: 027 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search and list completed beam production records |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:1005 → BEAM_BEAMLIST() |
| **Frequency** | Medium (production reporting and beam tracking) |
| **Performance** | Medium (joins with multiple filters and date range) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ⬜ | Beamer number (machine number) |
| `P_MC` | VARCHAR2(50) | ⬜ | Machine number (alternative parameter) |
| `P_ITMPREPARE` | VARCHAR2(50) | ⬜ | Item prepare code (product) |
| `P_STARTDATE` | VARCHAR2(20) | ⬜ | Start date filter (YYYY-MM-DD) |
| `P_ENDDATE` | VARCHAR2(20) | ⬜ | End date filter (YYYY-MM-DD) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2(50) | Beamer machine number |
| `BEAMLOT` | VARCHAR2(50) | Beam lot number (barcode) |
| `BEAMNO` | VARCHAR2(50) | Beam number |
| `STARTDATE` | DATE | Beaming start timestamp |
| `ENDDATE` | DATE | Beaming completion timestamp |
| `LENGTH` | NUMBER | Beam length in meters |
| `SPEED` | NUMBER | Average beaming speed |
| `BEAMSTANDTENSION` | NUMBER | Beam stand tension |
| `WINDINGTENSION` | NUMBER | Winding tension |
| `HARDNESS_L` | NUMBER | Beam hardness left side |
| `HARDNESS_N` | NUMBER | Beam hardness center |
| `HARDNESS_R` | NUMBER | Beam hardness right side |
| `INSIDE_WIDTH` | NUMBER | Inside width measurement |
| `OUTSIDE_WIDTH` | NUMBER | Outside width measurement |
| `FULL_WIDTH` | NUMBER | Full width measurement |
| `STARTBY` | VARCHAR2(50) | Operator who started beaming |
| `DOFFBY` | VARCHAR2(50) | Operator who completed beam |
| `BEAMMC` | VARCHAR2(50) | Beaming machine number |
| `FLAG` | VARCHAR2(10) | Process flag (C=Complete, T=Transferred) |
| `REMARK` | VARCHAR2(500) | Production notes/remarks |
| `TENSION_ST1` - `TENSION_ST10` | NUMBER | Station tensions (10 measurement points) |
| `EDITBY` | VARCHAR2(50) | Last operator to edit |
| `OLDBEAMNO` | VARCHAR2(50) | Previous beam number (if changed) |
| `EDITDATE` | DATE | Last edit timestamp |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code (product) |
| `WARPHEADNO` | VARCHAR2(50) | Warping head number (upstream traceability) |
| `TOTALYARN` | NUMBER | Total yarn count |
| `TOTALKEBA` | NUMBER | Total keba count |

---

## Business Logic (What it does and why)

Provides comprehensive search and reporting for completed beaming operations. Used for production analysis, quality tracking, operator performance monitoring, and material traceability from warping to drawing operations.

**Workflow**:
1. User opens beam list/report screen
2. Enters search criteria (any combination):
   - Beamer number (machine)
   - Product code
   - Date range (from/to dates)
3. System queries all completed beaming records
4. Returns detailed production data joined with product info
5. Results displayed in grid for analysis

**Business Rules**:
- All parameters optional (empty = no filter)
- Date range filters on STARTDATE field
- Returns only completed beams (FLAG = 'C' or 'T')
- Joins with setup to get ITM_PREPARE and WARPHEADNO
- Shows upstream traceability to warping operation

**Use Cases**:

**Production Reporting**:
- Daily/weekly/monthly production summary
- Machine utilization analysis
- Speed and efficiency calculations
- Length totals by product

**Quality Analysis**:
- Hardness distribution (L/N/R values)
- Tension consistency across 10 stations
- Width measurements (inside/outside/full)
- Speed vs. quality correlation

**Operator Performance**:
- Beams per operator (STARTBY, DOFFBY)
- Average production time
- Quality parameter consistency

**Material Traceability**:
- Warp beams to combined beam tracking
- Forward traceability to drawing/weaving
- Backward traceability to warping via WARPHEADNO
- Quality issue investigation

---

## Related Procedures

**Upstream**: BEAM_INSERTBEAMNO, BEAM_INSERTBEAMINGDETAIL - Create records being queried
**Downstream**: BEAM_TRANFERSLIP - Prints details for selected beam
**Similar**: [026-WARP_WARPLIST.md](../02_Warping/026-WARP_WARPLIST.md) - Similar search for warp beams

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_BEAMLIST()`
**Line**: 1005-1081

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_BEAMLISTParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 027/296 | **Progress**: 9.1%
