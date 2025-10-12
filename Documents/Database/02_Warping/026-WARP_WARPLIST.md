# WARP_WARPLIST

**Procedure Number**: 026 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search and list completed warp beams with production details |
| **Operation** | SELECT |
| **Tables** | tblWarpingProcess, tblWarpingCreelSetup |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - SELECT - Completed warp beam production records
- `tblWarpingCreelSetup` - SELECT (JOIN) - Product and yarn information

**Transaction**: No (read-only query)

### Indexes (if relevant)

```sql
-- Expected indexes for search performance
CREATE INDEX idx_warpingprocess_headno ON tblWarpingProcess(WARPHEADNO);
CREATE INDEX idx_warpingprocess_warpmc ON tblWarpingProcess(WARPMC);
CREATE INDEX idx_warpingprocess_dates ON tblWarpingProcess(STARTDATE, ENDDATE);
CREATE INDEX idx_warpingprocess_composite ON tblWarpingProcess(WARPMC, STARTDATE);
CREATE INDEX idx_creelsetup_headno ON tblWarpingCreelSetup(WARPHEADNO);
```

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

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_WARPLIST()`
**Line**: 1302-1364

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<WARP_WARPLIST> WARP_WARPLIST(string P_WARPHEADNO, string P_WARPMC,
    string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
{
    List<WARP_WARPLIST> results = null;

    if (!HasConnection())
        return results;

    // Prepare parameters
    WARP_WARPLISTParameter dbPara = new WARP_WARPLISTParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPMC = P_WARPMC;
    dbPara.P_ITMPREPARE = P_ITMPREPARE;
    dbPara.P_STARTDATE = P_STARTDATE;
    dbPara.P_ENDDATE = P_ENDDATE;

    List<WARP_WARPLISTResult> dbResults = null;

    try
    {
        // Call Oracle stored procedure
        dbResults = DatabaseManager.Instance.WARP_WARPLIST(dbPara);
        if (null != dbResults)
        {
            results = new List<WARP_WARPLIST>();
            foreach (WARP_WARPLISTResult dbResult in dbResults)
            {
                WARP_WARPLIST inst = new WARP_WARPLIST();

                inst.WARPHEADNO = dbResult.WARPHEADNO;
                inst.WARPERLOT = dbResult.WARPERLOT;
                inst.BEAMNO = dbResult.BEAMNO;
                inst.SIDE = dbResult.SIDE;
                inst.STARTDATE = dbResult.STARTDATE;
                inst.ENDDATE = dbResult.ENDDATE;
                inst.LENGTH = dbResult.LENGTH;
                inst.SPEED = dbResult.SPEED;
                inst.HARDNESS_L = dbResult.HARDNESS_L;
                inst.HARDNESS_N = dbResult.HARDNESS_N;
                inst.HARDNESS_R = dbResult.HARDNESS_R;
                inst.TENSION = dbResult.TENSION;
                inst.STARTBY = dbResult.STARTBY;
                inst.DOFFBY = dbResult.DOFFBY;
                inst.FLAG = dbResult.FLAG;
                inst.WARPMC = dbResult.WARPMC;
                inst.REMARK = dbResult.REMARK;
                inst.TENSION_IT = dbResult.TENSION_IT;
                inst.TENSION_TAKEUP = dbResult.TENSION_TAKEUP;
                inst.MC_COUNT_L = dbResult.MC_COUNT_L;
                inst.MC_COUNT_S = dbResult.MC_COUNT_S;
                inst.EDITDATE = dbResult.EDITDATE;
                inst.EDITBY = dbResult.EDITBY;

                // Product and yarn info from creel setup
                inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                inst.ITM_YARN = dbResult.ITM_YARN;

                results.Add(inst);
            }
        }
    }
    catch (Exception ex)
    {
        ex.Err();
    }

    return results;
}
```

**Expected Oracle Stored Procedure Logic**:
```sql
-- Estimated stored procedure structure
PROCEDURE WARP_WARPLIST(
    P_WARPHEADNO IN VARCHAR2,
    P_WARPMC IN VARCHAR2,
    P_ITMPREPARE IN VARCHAR2,
    P_STARTDATE IN VARCHAR2,
    P_ENDDATE IN VARCHAR2,
    CUR_RESULT OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN CUR_RESULT FOR
    SELECT
        wp.WARPHEADNO,
        wp.WARPERLOT,
        wp.BEAMNO,
        wp.SIDE,
        wp.STARTDATE,
        wp.ENDDATE,
        wp.LENGTH,
        wp.SPEED,
        wp.HARDNESS_L,
        wp.HARDNESS_N,
        wp.HARDNESS_R,
        wp.TENSION,
        wp.STARTBY,
        wp.DOFFBY,
        wp.FLAG,
        wp.WARPMC,
        wp.REMARK,
        wp.TENSION_IT,
        wp.TENSION_TAKEUP,
        wp.MC_COUNT_L,
        wp.MC_COUNT_S,
        wp.EDITDATE,
        wp.EDITBY,
        wcs.ITM_PREPARE,
        wcs.ITM_YARN
    FROM tblWarpingProcess wp
    INNER JOIN tblWarpingCreelSetup wcs
        ON wp.WARPHEADNO = wcs.WARPHEADNO
    WHERE (P_WARPHEADNO IS NULL OR wp.WARPHEADNO LIKE '%' || P_WARPHEADNO || '%')
      AND (P_WARPMC IS NULL OR wp.WARPMC = P_WARPMC)
      AND (P_ITMPREPARE IS NULL OR wcs.ITM_PREPARE LIKE '%' || P_ITMPREPARE || '%')
      AND (P_STARTDATE IS NULL OR TRUNC(wp.STARTDATE) >= TO_DATE(P_STARTDATE, 'YYYY-MM-DD'))
      AND (P_ENDDATE IS NULL OR TRUNC(wp.STARTDATE) <= TO_DATE(P_ENDDATE, 'YYYY-MM-DD'))
      AND wp.FLAG IN ('C', 'T')  -- Only completed/transferred beams
    ORDER BY wp.STARTDATE DESC, wp.WARPMC, wp.SIDE;
END;
```

**Report Columns Typically Displayed**:
- Beam Barcode (WARPERLOT)
- Product (ITM_PREPARE)
- Machine (WARPMC)
- Start Date/Time
- End Date/Time
- Duration (calculated from dates)
- Length (meters)
- Speed (m/min)
- Hardness (L/N/R)
- Tension
- Operator (STARTBY/DOFFBY)
- Status (FLAG)

---

**File**: 026/296 | **Progress**: 8.8%
