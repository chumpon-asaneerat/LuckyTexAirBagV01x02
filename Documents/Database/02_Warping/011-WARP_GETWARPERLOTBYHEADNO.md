# WARP_GETWARPERLOTBYHEADNO

**Procedure Number**: 011 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get all warper lot records for specific head number |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:534 → WARP_GETWARPERLOTBYHEADNO() |
| **Frequency** | High (production history and reporting) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot number |
| `BEAMNO` | VARCHAR2(50) | Beam number produced |
| `SIDE` | VARCHAR2(10) | Side (A or B) |
| `STARTDATE` | DATE | Production start date/time |
| `ENDDATE` | DATE | Production end date/time |
| `LENGTH` | NUMBER | Beam length (meters) |
| `SPEED` | NUMBER | Production speed (m/min) |
| `HARDNESS_L` | NUMBER | Hardness left (1-10) |
| `HARDNESS_N` | NUMBER | Hardness middle (1-10) |
| `HARDNESS_R` | NUMBER | Hardness right (1-10) |
| `TENSION` | NUMBER | Yarn tension |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `DOFFBY` | VARCHAR2(50) | Doffed by operator |
| `FLAG` | VARCHAR2(10) | Process flag/status |
| `WARPMC` | VARCHAR2(50) | Machine number |
| `REMARK` | VARCHAR2(200) | Remarks |
| `TENSION_IT` | NUMBER | IT tension |
| `TENSION_TAKEUP` | NUMBER | Takeup tension |
| `MC_COUNT_L` | NUMBER | Machine count left |
| `MC_COUNT_S` | NUMBER | Machine count small |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2(50) | Last edited by |
| `KEBA` | NUMBER | Keba count |
| `TIGHTEND` | NUMBER | Tight end count |
| `MISSYARN` | NUMBER | Missing yarn count |
| `OTHER` | NUMBER | Other issue count |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - SELECT - All production lots for specified head

**Transaction**: No (read-only operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpingprocess_headno ON tblWarpingProcess(WARPHEADNO);
CREATE INDEX idx_warpingprocess_lot ON tblWarpingProcess(WARPERLOT);
```

---

## Business Logic (What it does and why)

Retrieves complete production history for specific warping head/creel setup. Shows all beams produced from this setup including production metrics, quality data, and defect counts. Used for production reports, traceability, and quality analysis. Critical for understanding which beams came from which setup and their quality characteristics.

**Workflow**:
1. User requests production history for specific head number
2. Procedure queries all warper lot records for that head
3. Returns complete list of beams produced including:
   - Production dates and times
   - Quality metrics (length, speed, hardness, tension)
   - Operator accountability (who started, who doffed)
   - Defect counts (keba, tight ends, missing yarn)
   - Status flags
4. UI displays production history chronologically
5. Used for:
   - Production reports
   - Beam traceability
   - Quality analysis
   - Operator performance tracking

**Business Rules**:
- One head number can produce multiple beams (lots)
- Each lot represents one complete beam
- Includes quality metrics recorded during production
- Tracks defects for quality control
- Multiple tension readings (yarn, IT, takeup)
- FLAG indicates if lot is finished or in-process
- Edit tracking for audit trail

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates lot records
**Downstream**: [025-WARP_UPDATEWARPINGPROCESS.md](./025-WARP_UPDATEWARPINGPROCESS.md) - Updates lot data
**Similar**: [BEAM_GETBEAMLOTBYBEAMERNO.md](../03_Beaming/BEAM_GETBEAMLOTBYBEAMERNO.md) - Similar history retrieval

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_GETWARPERLOTBYHEADNO()`
**Line**: 534-596

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<WARP_GETWARPERLOTBYHEADNO> WARP_GETWARPERLOTBYHEADNO(string P_WARPHEADNO)
{
    List<WARP_GETWARPERLOTBYHEADNO> results = null;

    if (!HasConnection())
        return results;

    WARP_GETWARPERLOTBYHEADNOParameter dbPara = new WARP_GETWARPERLOTBYHEADNOParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;

    try
    {
        dbResults = DatabaseManager.Instance.WARP_GETWARPERLOTBYHEADNO(dbPara);
        if (null != dbResults)
        {
            results = new List<WARP_GETWARPERLOTBYHEADNO>();
            foreach (var dbResult in dbResults)
            {
                // Map 26 columns - comprehensive production data
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
                inst.KEBA = dbResult.KEBA; // Defect count
                inst.TIGHTEND = dbResult.TIGHTEND; // Defect count
                inst.MISSYARN = dbResult.MISSYARN; // Defect count
                inst.OTHER = dbResult.OTHER; // Defect count

                results.Add(inst);
            }
        }
    }
    catch (Exception ex) { ex.Err(); }

    return results;
}
```

---

**File**: 011/296 | **Progress**: 3.7%
