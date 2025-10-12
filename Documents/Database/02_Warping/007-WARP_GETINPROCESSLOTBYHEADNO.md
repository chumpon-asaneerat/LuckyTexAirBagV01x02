# WARP_GETINPROCESSLOTBYHEADNO

**Procedure Number**: 007 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get in-process warping lots for specific head number |
| **Operation** | SELECT |
| **Tables** | tblWarpingProcess, tblWarpingHead |
| **Called From** | WarpingDataService.cs:782 → WARP_GETINPROCESSLOTBYHEADNO() |
| **Frequency** | Medium (checks production status) |
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
| `BEAMNO` | VARCHAR2(50) | Beam number being produced |
| `SIDE` | VARCHAR2(10) | Side (A or B) |
| `STARTDATE` | DATE | Production start date/time |
| `ENDDATE` | DATE | Production end date/time |
| `LENGTH` | NUMBER | Beam length produced (meters) |
| `SPEED` | NUMBER | Production speed (m/min) |
| `HARDNESS_L` | NUMBER | Hardness left side (1-10) |
| `HARDNESS_N` | NUMBER | Hardness middle (1-10) |
| `HARDNESS_R` | NUMBER | Hardness right side (1-10) |
| `TENSION` | NUMBER | Yarn tension |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `DOFFBY` | VARCHAR2(50) | Doffed by operator |
| `FLAG` | VARCHAR2(10) | Process flag/status |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `REMARK` | VARCHAR2(200) | Remarks |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - SELECT - Production records
- `tblWarpingHead` - SELECT JOIN - Setup header

**Transaction**: No (read-only operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpingprocess_headno ON tblWarpingProcess(WARPHEADNO);
CREATE INDEX idx_warpingprocess_flag ON tblWarpingProcess(FLAG);
```

---

## Business Logic (What it does and why)

Retrieves all in-process (active) warping lots for a specific creel setup. Shows which beams are currently being produced from this setup, including production metrics (length, speed, hardness, tension) and responsible operators. Used for monitoring active production and displaying current status.

**Workflow**:
1. UI requests in-process lots for specific head number
2. Procedure queries active production records (FLAG != 'FINISHED')
3. Returns list of beams being produced with real-time metrics
4. UI displays production progress, metrics, and operator info
5. Operators can see which beams are in progress

**Business Rules**:
- Shows only in-process lots (not finished)
- Each record represents one beam being produced
- Multiple beams can be produced from same head number
- Includes quality metrics (hardness L/M/R, tension, speed)
- Tracks start/end times and responsible operators

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Starts production
**Downstream**: [025-WARP_UPDATEWARPINGPROCESS.md](./025-WARP_UPDATEWARPINGPROCESS.md) - Updates process
**Similar**: [BEAM_GETINPROCESSLOTBYBEAMNO.md](../03_Beaming/BEAM_GETINPROCESSLOTBYBEAMNO.md) - Similar in beaming

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_GETINPROCESSLOTBYHEADNO()`
**Line**: 782-832

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<WARP_GETINPROCESSLOTBYHEADNO> WARP_GETINPROCESSLOTBYHEADNO(string P_WARPHEADNO)
{
    List<WARP_GETINPROCESSLOTBYHEADNO> results = null;

    if (!HasConnection())
        return results;

    WARP_GETINPROCESSLOTBYHEADNOParameter dbPara = new WARP_GETINPROCESSLOTBYHEADNOParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;

    try
    {
        dbResults = DatabaseManager.Instance.WARP_GETINPROCESSLOTBYHEADNO(dbPara);
        if (null != dbResults)
        {
            results = new List<WARP_GETINPROCESSLOTBYHEADNO>();
            foreach (var dbResult in dbResults)
            {
                // Map 16 columns
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

                results.Add(inst);
            }
        }
    }
    catch (Exception ex) { ex.Err(); }

    return results;
}
```

---

**File**: 007/296 | **Progress**: 2.4%
