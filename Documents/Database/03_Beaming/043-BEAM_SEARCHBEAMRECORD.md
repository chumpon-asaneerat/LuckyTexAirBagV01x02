# BEAM_SEARCHBEAMRECORD

**Procedure Number**: 043 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search beaming setup records with multiple filter criteria |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:788 → BEAM_SEARCHBEAMRECORD() |
| **Frequency** | Medium |
| **Performance** | Medium (depends on filters) |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ⬜ | Beamer setup number (partial match) |
| `P_MC` | VARCHAR2(50) | ⬜ | Machine code filter |
| `P_ITMPREPARE` | VARCHAR2(50) | ⬜ | Item prepare code filter |
| `P_STARTDATE` | VARCHAR2(20) | ⬜ | Start date filter (date string) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2 | Beamer setup/batch number |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |
| `TOTALYARN` | NUMBER | Total yarn ends count |
| `TOTALKEBA` | NUMBER | Total keba (yarn groups) |
| `STARTDATE` | DATE | Setup start date |
| `ENDDATE` | DATE | Setup end date |
| `CREATEBY` | VARCHAR2 | Operator who created setup |
| `CREATEDATE` | DATE | Setup creation timestamp |
| `STATUS` | VARCHAR2 | Setup status (S=Setting, P=Processing, F=Finished) |
| `MCNO` | VARCHAR2 | Beaming machine code |
| `FINISHFLAG` | VARCHAR2 | Finish flag (0/1) |
| `WARPHEADNO` | VARCHAR2 | Warp setup number |
| `PRODUCTTYPEID` | VARCHAR2 | Product type ID |
| `ADJUSTKEBA` | NUMBER | Adjusted keba value |
| `REMARK` | VARCHAR2 | Setup remarks |

---

## Business Logic (What it does and why)

Searches beaming setup records with flexible filtering for production history review, reporting, and record lookup.

**Purpose**: Allows operators and supervisors to find past beaming setups using multiple search criteria. Used for:
- **Production History**: Review past setups and parameters
- **Data Analysis**: Find setups by date range, machine, product
- **Record Editing**: Locate setup before making corrections
- **Reporting**: Generate production reports by filters

**When Used**:
- **Search Screen**: Main beaming search/history page
- **Production Reports**: Filter data for report generation
- **Edit History**: Find old records to correct errors
- **Performance Analysis**: Analyze production by machine/product/date

**Business Rules**:
- All 4 parameters are optional (supports partial search)
- Empty parameters ignored (not used in filter)
- BEAMERNO supports partial match (LIKE search)
- Multiple filters combine with AND logic
- Returns all matching setup header records
- Empty result means no matches found

**Search Combinations**:
1. **By Setup Number**: P_BEAMERNO only → Find specific setup(s)
2. **By Machine**: P_MC only → All setups on a machine
3. **By Product**: P_ITMPREPARE only → All setups for a product
4. **By Date**: P_STARTDATE only → All setups on a date
5. **Combined**: All filters → Narrow search

---

## Related Procedures

**Upstream**:
- [042-BEAM_INSERTBEAMNO.md](./042-BEAM_INSERTBEAMNO.md) - Creates records that this procedure searches

**Downstream**:
- [030-BEAM_GETBEAMERMCSTATUS.md](./030-BEAM_GETBEAMERMCSTATUS.md) - Gets detailed status after finding setup
- [027-BEAM_BEAMLIST.md](./027-BEAM_BEAMLIST.md) - Gets beam details for selected setup

**Similar**:
- [021-WARP_SEARCHWARPRECORD.md](../02_Warping/021-WARP_SEARCHWARPRECORD.md) - Warping search equivalent
- [WEAV_SEARCHPRODUCTION.md](../05_Weaving/WEAV_SEARCHPRODUCTION.md) - Weaving search equivalent

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_SEARCHBEAMRECORD()`
**Line**: 788-839

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_SEARCHBEAMRECORDParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 43/296 | **Progress**: 14.5%
