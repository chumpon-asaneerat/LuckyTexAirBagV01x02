# WARP_UPDATEWARPINGPROCESS

**Procedure Number**: 025 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update warping production parameters during or after beam production |
| **Operation** | UPDATE |
| **Tables** | tblWarpingProcess |
| **Called From** | WarpingDataService.cs:1719 → WARP_UPDATEWARPINGPROCESS() (doff completion)<br>WarpingDataService.cs:1786 → WARP_UPDATEWARPINGPROCESS() (in-process edit)<br>WarpingDataService.cs:1923 → WARP_UPDATEWARPINGPROCESS_REMARK() (remark only) |
| **Frequency** | High (beam completion and in-process adjustments) |
| **Performance** | Fast (single record update) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN) - Version 1 (Doff Completion)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number (beam barcode) |
| `P_LENGTH` | NUMBER | ⬜ | Final beam length in meters |
| `P_ENDDATE` | DATE | ⬜ | Doff completion timestamp |
| `P_SPEED` | NUMBER | ⬜ | Average warping speed |
| `P_HARDL` | NUMBER | ⬜ | Beam hardness left side |
| `P_HARDN` | NUMBER | ⬜ | Beam hardness center |
| `P_HARDR` | NUMBER | ⬜ | Beam hardness right side |
| `P_TENSION` | NUMBER | ⬜ | Average yarn tension |
| `P_DOFFBY` | VARCHAR2(50) | ⬜ | Operator who doffed beam |
| `P_TENSION_IT` | NUMBER | ⬜ | IT tension parameter |
| `P_TENSION_TAKE` | NUMBER | ⬜ | Take-up tension parameter |
| `P_MCL` | NUMBER | ⬜ | Machine counter L value |
| `P_MCS` | NUMBER | ⬜ | Machine counter S value |

### Input (IN) - Version 2 (In-Process Edit)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number |
| `P_LENGTH` | NUMBER | ⬜ | Current/updated length |
| `P_SPEED` | NUMBER | ⬜ | Updated speed |
| `P_HARDL` | NUMBER | ⬜ | Updated hardness L |
| `P_HARDN` | NUMBER | ⬜ | Updated hardness N |
| `P_HARDR` | NUMBER | ⬜ | Updated hardness R |
| `P_TENSION` | NUMBER | ⬜ | Updated tension |
| `P_TENSION_IT` | NUMBER | ⬜ | Updated IT tension |
| `P_TENSION_TAKE` | NUMBER | ⬜ | Updated take-up tension |
| `P_MCL` | NUMBER | ⬜ | Updated counter L |
| `P_MCS` | NUMBER | ⬜ | Updated counter S |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator making edit |
| `P_BEAMNO` | VARCHAR2(50) | ⬜ | Beam number (if changed) |

### Input (IN) - Version 3 (Remark Only)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Notes/remarks about production |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - UPDATE - Updates production parameters and completion status

**Transaction**: Yes (single update operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE UNIQUE INDEX idx_warpingprocess_pk ON tblWarpingProcess(WARPHEADNO, WARPERLOT);
CREATE INDEX idx_warpingprocess_beamno ON tblWarpingProcess(BEAMNO);
CREATE INDEX idx_warpingprocess_flag ON tblWarpingProcess(FLAG);
```

---

## Business Logic (What it does and why)

Updates warping production parameters in three main scenarios: beam completion (doff), in-process adjustments, and adding remarks. This maintains accurate production records and quality parameters for traceability.

**Workflow - Scenario 1: Beam Completion (Doff)**
1. Warping machine reaches target length or beam full
2. Operator stops machine and performs doff (beam removal)
3. Operator measures final parameters:
   - Length (actual meters)
   - Hardness at 3 points (L/N/R)
   - Records average speed and tension
4. System calls WARP_UPDATEWARPINGPROCESS() with completion data:
   - P_ENDDATE = current timestamp
   - P_DOFFBY = operator ID
   - All final quality parameters
5. Sets FLAG = 'C' (Complete)
6. Beam ready for transfer to beaming

**Workflow - Scenario 2: In-Process Edit**
1. Production running, operator needs to adjust parameters
2. Operator edits current values (speed, tension, hardness)
3. System updates running process record
4. Used for:
   - Correcting data entry errors
   - Recording mid-production parameter changes
   - Updating beam number if changed

**Workflow - Scenario 3: Add Remarks**
1. Operator needs to note quality issues or special conditions
2. Adds text remark (defects, problems, observations)
3. System updates REMARK field only
4. Preserves all other production data

**Business Rules**:
- **Quality Parameters Required at Doff**:
  - LENGTH: Actual meters produced
  - HARDNESS_L/N/R: Must be within spec range
  - SPEED: Average speed during production
  - TENSION: Average yarn tension
- **FLAG Values**:
  - NULL or 'S': In process
  - 'C': Complete (doffed)
  - 'T': Transferred to next stage
- **Audit Trail**:
  - EDITBY: Last operator to edit
  - EDITDATE: Last edit timestamp
  - DOFFBY: Operator who completed beam
  - ENDDATE: Completion timestamp

**Three Overloaded Methods in C#**:
The same procedure is called with different parameter combinations based on the operation type. The stored procedure uses NVL to only update provided parameters.

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates initial process record
**Downstream**: [022-WARP_TRANFERSLIP.md](./022-WARP_TRANFERSLIP.md) - Prints transfer slip using updated data
**Similar**: BEAM_UPDATEBEAMDETAIL - Beaming process update (similar pattern)

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Methods**:
- `WARP_UPDATEWARPINGPROCESS()` (Doff) - Line 1719-1763
- `WARP_UPDATEWARPINGPROCESS()` (Edit) - Line 1786-1832
- `WARP_UPDATEWARPINGPROCESS_REMARK()` - Line 1923-1954

**Query Type**: Stored Procedure Call (Oracle)

```csharp
// VERSION 1: Doff Completion with End Date
public bool WARP_UPDATEWARPINGPROCESS(string P_WARPHEADNO, string P_WARPLOT,
    decimal? P_LENGTH, DateTime? P_ENDDATE, decimal? P_SPEED,
    decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR, decimal? P_TENSION,
    string P_DOFFBY, decimal? P_TENSION_IT, decimal? P_TENSION_TAKE,
    decimal? P_MCL, decimal? P_MCS)
{
    bool result = false;

    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    WARP_UPDATEWARPINGPROCESSParameter dbPara = new WARP_UPDATEWARPINGPROCESSParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPLOT = P_WARPLOT;
    dbPara.P_LENGTH = P_LENGTH;
    dbPara.P_ENDDATE = P_ENDDATE;       // Completion timestamp
    dbPara.P_SPEED = P_SPEED;
    dbPara.P_HARDL = P_HARDL;
    dbPara.P_HARDN = P_HARDN;
    dbPara.P_HARDR = P_HARDR;
    dbPara.P_TENSION = P_TENSION;
    dbPara.P_DOFFBY = P_DOFFBY;         // Operator who doffed
    dbPara.P_TENSION_IT = P_TENSION_IT;
    dbPara.P_TENSION_TAKE = P_TENSION_TAKE;
    dbPara.P_MCL = P_MCL;
    dbPara.P_MCS = P_MCS;

    WARP_UPDATEWARPINGPROCESSResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.WARP_UPDATEWARPINGPROCESS(dbPara);
        result = (null != dbResult);
    }
    catch (Exception ex)
    {
        ex.Err();
        result = false;
    }

    return result;
}

// VERSION 2: In-Process Edit (no end date, includes operator and beam number)
public bool WARP_UPDATEWARPINGPROCESS(string P_WARPHEADNO, string P_WARPLOT,
    decimal? P_LENGTH, decimal? P_SPEED,
    decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR,
    decimal? P_TENSION, decimal? P_TENSION_IT, decimal? P_TENSION_TAKE,
    decimal? P_MCL, decimal? P_MCS, string P_OPERATOR, string P_BEAMNO)
{
    bool result = false;

    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    WARP_UPDATEWARPINGPROCESSParameter dbPara = new WARP_UPDATEWARPINGPROCESSParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPLOT = P_WARPLOT;
    dbPara.P_LENGTH = P_LENGTH;
    dbPara.P_SPEED = P_SPEED;
    dbPara.P_HARDL = P_HARDL;
    dbPara.P_HARDN = P_HARDN;
    dbPara.P_HARDR = P_HARDR;
    dbPara.P_TENSION = P_TENSION;
    dbPara.P_TENSION_IT = P_TENSION_IT;
    dbPara.P_TENSION_TAKE = P_TENSION_TAKE;
    dbPara.P_MCL = P_MCL;
    dbPara.P_MCS = P_MCS;
    dbPara.P_OPERATOR = P_OPERATOR;     // Edit operator
    dbPara.P_BEAMNO = P_BEAMNO;         // Updated beam number

    WARP_UPDATEWARPINGPROCESSResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.WARP_UPDATEWARPINGPROCESS(dbPara);
        result = (null != dbResult);
    }
    catch (Exception ex)
    {
        ex.Err();
        result = false;
    }

    return result;
}

// VERSION 3: Remark Only Update
public bool WARP_UPDATEWARPINGPROCESS_REMARK(string P_WARPHEADNO, string P_WARPLOT,
    string P_REMARK)
{
    bool result = false;

    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    WARP_UPDATEWARPINGPROCESSParameter dbPara = new WARP_UPDATEWARPINGPROCESSParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPLOT = P_WARPLOT;
    dbPara.P_REMARK = P_REMARK;         // Notes/remarks only

    WARP_UPDATEWARPINGPROCESSResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.WARP_UPDATEWARPINGPROCESS(dbPara);
        result = (null != dbResult);
    }
    catch (Exception ex)
    {
        ex.Err();
        result = false;
    }

    return result;
}
```

**Expected Oracle Stored Procedure Logic**:
```sql
-- Estimated stored procedure structure (handles all three versions)
PROCEDURE WARP_UPDATEWARPINGPROCESS(
    P_WARPHEADNO IN VARCHAR2,
    P_WARPLOT IN VARCHAR2,
    P_LENGTH IN NUMBER DEFAULT NULL,
    P_ENDDATE IN DATE DEFAULT NULL,
    P_SPEED IN NUMBER DEFAULT NULL,
    P_HARDL IN NUMBER DEFAULT NULL,
    P_HARDN IN NUMBER DEFAULT NULL,
    P_HARDR IN NUMBER DEFAULT NULL,
    P_TENSION IN NUMBER DEFAULT NULL,
    P_DOFFBY IN VARCHAR2 DEFAULT NULL,
    P_TENSION_IT IN NUMBER DEFAULT NULL,
    P_TENSION_TAKE IN NUMBER DEFAULT NULL,
    P_MCL IN NUMBER DEFAULT NULL,
    P_MCS IN NUMBER DEFAULT NULL,
    P_OPERATOR IN VARCHAR2 DEFAULT NULL,
    P_BEAMNO IN VARCHAR2 DEFAULT NULL,
    P_REMARK IN VARCHAR2 DEFAULT NULL
)
IS
BEGIN
    UPDATE tblWarpingProcess
    SET LENGTH = NVL(P_LENGTH, LENGTH),
        ENDDATE = NVL(P_ENDDATE, ENDDATE),
        SPEED = NVL(P_SPEED, SPEED),
        HARDNESS_L = NVL(P_HARDL, HARDNESS_L),
        HARDNESS_N = NVL(P_HARDN, HARDNESS_N),
        HARDNESS_R = NVL(P_HARDR, HARDNESS_R),
        TENSION = NVL(P_TENSION, TENSION),
        DOFFBY = NVL(P_DOFFBY, DOFFBY),
        TENSION_IT = NVL(P_TENSION_IT, TENSION_IT),
        TENSION_TAKEUP = NVL(P_TENSION_TAKE, TENSION_TAKEUP),
        MC_COUNT_L = NVL(P_MCL, MC_COUNT_L),
        MC_COUNT_S = NVL(P_MCS, MC_COUNT_S),
        BEAMNO = NVL(P_BEAMNO, BEAMNO),
        REMARK = NVL(P_REMARK, REMARK),
        EDITBY = NVL(P_OPERATOR, P_DOFFBY),
        EDITDATE = SYSDATE,
        FLAG = CASE
            WHEN P_ENDDATE IS NOT NULL AND P_DOFFBY IS NOT NULL THEN 'C'
            ELSE FLAG
        END
    WHERE WARPHEADNO = P_WARPHEADNO
      AND WARPERLOT = P_WARPLOT;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
```

---

**File**: 025/296 | **Progress**: 8.4%
