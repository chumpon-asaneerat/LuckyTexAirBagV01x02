# WARP_SEARCHWARPRECORD

**Procedure Number**: 021 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search warping production records with multiple filter criteria |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:1070 → WARP_SEARCHWARPRECORD() |
| **Frequency** | Medium (used in warping record search screen) |
| **Performance** | Medium (multiple optional filters with wildcards) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ⬜ | Warping head number (supports wildcard) |
| `P_WARPMC` | VARCHAR2(50) | ⬜ | Warping machine number |
| `P_ITMPREPARE` | VARCHAR2(50) | ⬜ | Item prepare code (product code) |
| `P_STARTDATE` | VARCHAR2(20) | ⬜ | Start date filter (format: YYYY-MM-DD or similar) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number (creel setup ID) |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code (product being warped) |
| `PRODUCTTYPEID` | VARCHAR2(20) | Product type identifier |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `SIDE` | VARCHAR2(10) | Creel side (A or B) |
| `ACTUALCH` | NUMBER | Actual cheese count loaded |
| `WTYPE` | VARCHAR2(10) | Waxing type (Y/N - Yes/No Wax) |
| `STARTDATE` | DATE | Setup start date/time |
| `CREATEBY` | VARCHAR2(50) | Operator who created setup |
| `CONDITIONSTART` | DATE | Conditioning start date/time |
| `CONDITIONBY` | VARCHAR2(50) | Operator who started conditioning |
| `ENDDATE` | DATE | Setup completion date/time |
| `STATUS` | VARCHAR2(10) | Setup status (S=Processing, C=Conditioning, F=Finished) |
| `FINISHBY` | VARCHAR2(50) | Operator who finished setup |
| `FINISHFLAG` | VARCHAR2(10) | Finish flag (Y/N) |
| `REEDNO` | VARCHAR2(50) | Reed number used |
| `EDITBY` | VARCHAR2(50) | Last operator to edit |
| `EDITDATE` | DATE | Last edit timestamp |

---

## Business Logic (What it does and why)

Provides flexible search functionality for warping production records. Operators use this to find historical warping setups for analysis, troubleshooting, or reference when setting up similar products.

**Workflow**:
1. Operator opens warping record search screen
2. Enters one or more search criteria:
   - Warp head number (exact or partial match)
   - Machine number (exact match)
   - Item code (exact or partial match)
   - Date range (start date)
3. System queries database with all provided filters (AND logic)
4. Returns matching records sorted by date (newest first, typically)
5. Operator can drill down into specific records for details

**Business Rules**:
- All parameters are optional (empty = no filter on that field)
- Supports partial matching on text fields (LIKE '%value%')
- Date filter typically searches for records on or after P_STARTDATE
- Returns all statuses (S, C, F) unless further filtered in UI
- Used for:
  - Historical production analysis
  - Setup verification
  - Troubleshooting quality issues
  - Reference for similar product setups
  - Production reporting

**Search Patterns**:
- Empty parameters: Returns all records (may need date range limit)
- Single parameter: Filter by that field only
- Multiple parameters: AND logic (all conditions must match)
- Wildcard support: '%' character for partial matches

---

## Related Procedures

**Upstream**: None - This is a search/query operation
**Downstream**: [005-WARP_GETCREELSETUPDETAIL.md](./005-WARP_GETCREELSETUPDETAIL.md) - Get detailed pallet information for selected record
**Similar**: [026-WARP_WARPLIST.md](./026-WARP_WARPLIST.md) - Similar search for completed warp beams

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_SEARCHWARPRECORD()`
**Line**: 1070-1124

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: WARP_SEARCHWARPRECORDParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 021/296 | **Progress**: 7.1%
