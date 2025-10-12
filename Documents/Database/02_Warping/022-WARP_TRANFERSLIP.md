# WARP_TRANFERSLIP

**Procedure Number**: 022 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get transfer slip data for completed warp beam (for printing/reporting) |
| **Operation** | SELECT |
| **Tables** | tblWarpingProcess, tblWarpingCreelSetup |
| **Called From** | WarpingDataService.cs:606 → WARP_TRANFERSLIP() |
| **Frequency** | Medium (every warp beam completion for transfer documentation) |
| **Performance** | Fast (single beam lookup) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number (beam barcode) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot number (beam barcode) |
| `BEAMNO` | VARCHAR2(50) | Beam number |
| `SIDE` | VARCHAR2(10) | Creel side (A or B) |
| `STARTDATE` | DATE | Warping start date/time |
| `ENDDATE` | DATE | Warping completion date/time |
| `LENGTH` | NUMBER | Beam length in meters |
| `SPEED` | NUMBER | Average warping speed |
| `HARDNESS_L` | NUMBER | Beam hardness left side |
| `HARDNESS_N` | NUMBER | Beam hardness center |
| `HARDNESS_R` | NUMBER | Beam hardness right side |
| `TENSION` | NUMBER | Yarn tension during warping |
| `STARTBY` | VARCHAR2(50) | Operator who started warping |
| `DOFFBY` | VARCHAR2(50) | Operator who completed beam |
| `FLAG` | VARCHAR2(10) | Process flag/status |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code (product) |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code used |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - SELECT - Completed warp beam production data
- `tblWarpingCreelSetup` - SELECT (JOIN) - Product and yarn information

**Transaction**: No (read-only query)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpingprocess_headno ON tblWarpingProcess(WARPHEADNO);
CREATE INDEX idx_warpingprocess_warplot ON tblWarpingProcess(WARPERLOT);
CREATE INDEX idx_warpingprocess_composite ON tblWarpingProcess(WARPHEADNO, WARPERLOT);
```

---

## Business Logic (What it does and why)

Retrieves complete production data for a warp beam to generate transfer documentation (transfer slip). When a warp beam is completed and ready to move to the next production stage (beaming), this slip documents all production parameters and provides traceability.

**Workflow**:
1. Operator completes warp beam production (doff operation)
2. System prompts to print transfer slip
3. Operator clicks print transfer slip button
4. System calls this procedure with WARPHEADNO and WARPERLOT
5. Retrieves all production data and item information
6. Generates transfer slip report showing:
   - Beam identification (barcode, beam number)
   - Product information (item prepare, yarn type)
   - Production parameters (length, speed, tension, hardness)
   - Operators (who started, who doffed)
   - Timestamps (start/end dates)
7. Slip printed as barcode label or paper document
8. Beam physically transferred to beaming department with slip

**Business Rules**:
- Transfer slip required for material traceability
- Documents quality parameters (hardness L/N/R, tension, speed)
- Links yarn input (ITM_YARN) to warp beam output (WARPERLOT)
- Provides operator accountability (STARTBY, DOFFBY)
- Slip stays with beam through production process
- May be scanned at next operation to verify beam identity

**Use Cases**:
- Print transfer slip after beam completion
- Verify beam identity at beaming operation
- Quality issue investigation (trace back to warping parameters)
- Production reporting and analysis
- Operator performance tracking

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates the process record being queried
**Downstream**: BEAM_* procedures - Beaming operations that receive this beam
**Similar**: BEAM_TRANFERSLIP - Transfer slip for beaming to drawing

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_TRANFERSLIP()`
**Line**: 606-659

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<WARP_TRANFERSLIP> WARP_TRANFERSLIP(string P_WARPHEADNO, string P_WARPLOT)
{
    List<WARP_TRANFERSLIP> results = null;

    if (!HasConnection())
        return results;

    // Prepare parameters
    WARP_TRANFERSLIPParameter dbPara = new WARP_TRANFERSLIPParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPLOT = P_WARPLOT;

    List<WARP_TRANFERSLIPResult> dbResults = null;

    try
    {
        // Call Oracle stored procedure
        dbResults = DatabaseManager.Instance.WARP_TRANFERSLIP(dbPara);
        if (null != dbResults)
        {
            results = new List<WARP_TRANFERSLIP>();
            foreach (WARP_TRANFERSLIPResult dbResult in dbResults)
            {
                WARP_TRANFERSLIP inst = new WARP_TRANFERSLIP();

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

                // Item information from creel setup
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
PROCEDURE WARP_TRANFERSLIP(
    P_WARPHEADNO IN VARCHAR2,
    P_WARPLOT IN VARCHAR2,
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
        wcs.ITM_PREPARE,
        wcs.ITM_YARN
    FROM tblWarpingProcess wp
    INNER JOIN tblWarpingCreelSetup wcs
        ON wp.WARPHEADNO = wcs.WARPHEADNO
    WHERE wp.WARPHEADNO = P_WARPHEADNO
      AND wp.WARPERLOT = P_WARPLOT;
END;
```

**Report Usage**:
This data is typically used to generate a transfer slip report/label that includes:
- Barcode: WARPERLOT (for scanning at next operation)
- Product: ITM_PREPARE
- Yarn: ITM_YARN
- Beam No: BEAMNO
- Length: LENGTH meters
- Machine: WARPMC
- Date: ENDDATE
- Operator: DOFFBY
- QC Params: HARDNESS_L/N/R, TENSION, SPEED

---

**File**: 022/296 | **Progress**: 7.4%
