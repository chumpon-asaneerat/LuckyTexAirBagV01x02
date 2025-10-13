# WARP_WARPLIST

**Procedure Number**: 026 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search and list completed warp beams with production details |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:1302 → WARP_WARPLIST() |
| **Frequency** | Medium (production reporting and beam tracking) |
| **Performance** | Medium (joins with multiple filters and date range) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ⬜ | Warping head number (supports wildcard) |
| `P_WARPMC` | VARCHAR2(50) | ⬜ | Warping machine number |
| `P_ITMPREPARE` | VARCHAR2(50) | ⬜ | Item prepare code (product) |
| `P_STARTDATE` | VARCHAR2(20) | ⬜ | Start date filter (YYYY-MM-DD) |
| `P_ENDDATE` | VARCHAR2(20) | ⬜ | End date filter (YYYY-MM-DD) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot/beam barcode |
| `BEAMNO` | VARCHAR2(50) | Beam number |
| `SIDE` | VARCHAR2(10) | Creel side (A or B) |
| `STARTDATE` | DATE | Warping start timestamp |
| `ENDDATE` | DATE | Warping completion timestamp |
| `LENGTH` | NUMBER | Beam length in meters |
| `SPEED` | NUMBER | Average warping speed |
| `HARDNESS_L` | NUMBER | Beam hardness left side |
| `HARDNESS_N` | NUMBER | Beam hardness center |
| `HARDNESS_R` | NUMBER | Beam hardness right side |
| `TENSION` | NUMBER | Average yarn tension |
| `STARTBY` | VARCHAR2(50) | Operator who started production |
| `DOFFBY` | VARCHAR2(50) | Operator who doffed beam |
| `FLAG` | VARCHAR2(10) | Process flag (C=Complete, T=Transferred) |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `REMARK` | VARCHAR2(500) | Production notes/remarks |
| `TENSION_IT` | NUMBER | IT tension parameter |
| `TENSION_TAKEUP` | NUMBER | Take-up tension parameter |
| `MC_COUNT_L` | NUMBER | Machine counter L value |
| `MC_COUNT_S` | NUMBER | Machine counter S value |
| `EDITDATE` | DATE | Last edit timestamp |
| `EDITBY` | VARCHAR2(50) | Last operator to edit |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code (product) |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code used |

---

## Business Logic (What it does and why)

Provides comprehensive search and reporting for completed warp beams. Used for production analysis, quality tracking, operator performance monitoring, and material traceability throughout the production history.

**Workflow**:
1. User opens warp beam list/report screen
2. Enters search criteria (any combination):
   - Warp head number (partial match allowed)
   - Machine number (exact match)
   - Product code (partial match allowed)
   - Date range (from/to dates)
3. System queries all completed warping records
4. Returns detailed production data joined with product/yarn info
5. Results displayed in grid with sortable columns
6. User can:
   - Drill down for more details
   - Export to Excel for analysis
   - Generate production reports
   - Investigate quality issues
   - Track operator performance

**Business Rules**:
- All parameters are optional (empty = no filter)
- Date range filters on STARTDATE field (production start)
- Supports partial text matching with wildcards
- Returns only completed beams (FLAG = 'C' or 'T')
- Ordered by date (newest first, typically)
- Joins with creel setup to get ITM_PREPARE and ITM_YARN

**Use Cases**:

**Production Reporting**:
- Daily/weekly/monthly production summary
- Machine utilization analysis
- Speed and efficiency calculations
- Length totals by product

**Quality Analysis**:
- Hardness distribution (L/N/R values)
- Tension consistency tracking
- Speed vs. quality correlation
- Defect pattern investigation

**Operator Performance**:
- Beams per operator (STARTBY, DOFFBY)
- Average production time
- Quality parameter consistency
- Setup efficiency tracking

**Material Traceability**:
- Yarn lot to warp beam tracking
- Forward traceability to weaving
- Backward traceability to yarn pallets
- Quality issue root cause analysis

**Typical Reports Generated**:
- Daily production summary by machine
- Operator efficiency report
- Quality parameter trending
- Machine performance comparison
- Product-specific production history

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates records being queried
**Downstream**: [022-WARP_TRANFERSLIP.md](./022-WARP_TRANFERSLIP.md) - Prints details for selected beam
**Similar**: [021-WARP_SEARCHWARPRECORD.md](./021-WARP_SEARCHWARPRECORD.md) - Searches creel setups (upstream)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_WARPLIST()`
**Line**: 1302-1364

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: WARP_WARPLISTParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 026/296 | **Progress**: 8.8%
