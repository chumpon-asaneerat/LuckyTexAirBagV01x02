# BEAM_BEAMLIST

**Procedure Number**: 027 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search and list completed beam production records |
| **Operation** | SELECT |
| **Tables** | tblBeamingProcess, tblBeamingSetup |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingProcess` - SELECT - Completed beam production records
- `tblBeamingSetup` - SELECT (JOIN) - Product and warp beam information

**Transaction**: No (read-only query)

### Indexes (if relevant)

```sql
-- Expected indexes for search performance
CREATE INDEX idx_beamingprocess_beamerno ON tblBeamingProcess(BEAMERNO);
CREATE INDEX idx_beamingprocess_beammc ON tblBeamingProcess(BEAMMC);
CREATE INDEX idx_beamingprocess_dates ON tblBeamingProcess(STARTDATE, ENDDATE);
CREATE INDEX idx_beamingprocess_composite ON tblBeamingProcess(BEAMMC, STARTDATE);
```

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

**File**: `BeamingDataService.cs`
**Method**: `BEAM_BEAMLIST()`
**Line**: 1005-1081

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<BEAM_BEAMLIST> BEAM_BEAMLIST(string P_BEAMERNO, string P_MC,
    string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
{
    List<BEAM_BEAMLIST> results = null;

    if (!HasConnection())
        return results;

    BEAM_BEAMLISTParameter dbPara = new BEAM_BEAMLISTParameter();
    dbPara.P_BEAMERNO = P_BEAMERNO;
    dbPara.P_MC = P_MC;
    dbPara.P_ITMPREPARE = P_ITMPREPARE;
    dbPara.P_STARTDATE = P_STARTDATE;
    dbPara.P_ENDDATE = P_ENDDATE;

    List<BEAM_BEAMLISTResult> dbResults = null;

    try
    {
        dbResults = DatabaseManager.Instance.BEAM_BEAMLIST(dbPara);
        if (null != dbResults)
        {
            results = new List<BEAM_BEAMLIST>();
            foreach (BEAM_BEAMLISTResult dbResult in dbResults)
            {
                BEAM_BEAMLIST inst = new BEAM_BEAMLIST();

                inst.BEAMERNO = dbResult.BEAMERNO;
                inst.BEAMLOT = dbResult.BEAMLOT;
                inst.BEAMNO = dbResult.BEAMNO;
                inst.STARTDATE = dbResult.STARTDATE;
                inst.ENDDATE = dbResult.ENDDATE;
                inst.LENGTH = dbResult.LENGTH;
                inst.SPEED = dbResult.SPEED;
                inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                inst.HARDNESS_L = dbResult.HARDNESS_L;
                inst.HARDNESS_N = dbResult.HARDNESS_N;
                inst.HARDNESS_R = dbResult.HARDNESS_R;
                inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                inst.STARTBY = dbResult.STARTBY;
                inst.DOFFBY = dbResult.DOFFBY;
                inst.BEAMMC = dbResult.BEAMMC;
                inst.FLAG = dbResult.FLAG;
                inst.REMARK = dbResult.REMARK;

                // 10 station tensions
                inst.TENSION_ST1 = dbResult.TENSION_ST1;
                inst.TENSION_ST2 = dbResult.TENSION_ST2;
                inst.TENSION_ST3 = dbResult.TENSION_ST3;
                inst.TENSION_ST4 = dbResult.TENSION_ST4;
                inst.TENSION_ST5 = dbResult.TENSION_ST5;
                inst.TENSION_ST6 = dbResult.TENSION_ST6;
                inst.TENSION_ST7 = dbResult.TENSION_ST7;
                inst.TENSION_ST8 = dbResult.TENSION_ST8;
                inst.TENSION_ST9 = dbResult.TENSION_ST9;
                inst.TENSION_ST10 = dbResult.TENSION_ST10;

                inst.EDITBY = dbResult.EDITBY;
                inst.OLDBEAMNO = dbResult.OLDBEAMNO;
                inst.EDITDATE = dbResult.EDITDATE;
                inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                inst.WARPHEADNO = dbResult.WARPHEADNO;
                inst.TOTALYARN = dbResult.TOTALYARN;
                inst.TOTALKEBA = dbResult.TOTALKEBA;

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

---

**File**: 027/296 | **Progress**: 9.1%
