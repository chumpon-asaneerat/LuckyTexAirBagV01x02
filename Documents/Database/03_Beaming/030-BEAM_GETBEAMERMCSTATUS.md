# BEAM_GETBEAMERMCSTATUS

**Procedure Number**: 030 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beaming machine status and current setup information |
| **Operation** | SELECT |
| **Tables** | tblBeamingSetup |
| **Called From** | BeamingDataService.cs:586 → BEAM_GETBEAMERMCSTATUS() |
| **Frequency** | High (real-time machine monitoring) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ⬜ | Machine number (empty = all machines) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2(50) | Beamer setup number |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code |
| `TOTALYARN` | NUMBER | Total yarn count |
| `TOTALKEBA` | NUMBER | Total keba count |
| `STARTDATE` | DATE | Setup start date |
| `ENDDATE` | DATE | Setup completion date |
| `CREATEBY` | VARCHAR2(50) | Operator who created setup |
| `CREATEDATE` | DATE | Setup creation date |
| `STATUS` | VARCHAR2(10) | Current status |
| `FINISHFLAG` | VARCHAR2(10) | Finish flag (Y/N) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `WARPHEADNO` | VARCHAR2(50) | Warping head number (traceability) |
| `PRODUCTTYPEID` | VARCHAR2(20) | Product type ID |
| `ADJUSTKEBA` | NUMBER | Adjusted keba count |
| `REMARK` | VARCHAR2(500) | Remarks/notes |
| `NOWARPBEAM` | NUMBER | Number of warp beams |
| `TOTALBEAM` | NUMBER | Total beam count |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingSetup` - SELECT - Current machine setup status

**Transaction**: No (read-only)

### Indexes (if relevant)

```sql
CREATE INDEX idx_beamingsetup_mcno ON tblBeamingSetup(MCNO, STATUS);
```

---

## Business Logic (What it does and why)

Retrieves current status of beaming machines for production monitoring dashboard. Shows what each machine is currently working on, progress, and setup details.

**Workflow**:
1. Dashboard screen loads
2. System queries machine status
3. Shows real-time view of all beaming machines
4. Displays current setup, product, and progress
5. Operators monitor production status

**Business Rules**:
- If P_MCNO empty, returns all machines
- If P_MCNO specified, returns that machine only
- Shows current active setups (not completed)
- Includes traceability to warping via WARPHEADNO
- Shows material quantities (yarn, keba, beam counts)

**Use Cases**:
- Production monitoring dashboard
- Machine status display
- Capacity planning
- Progress tracking
- Supervisor overview

---

## Related Procedures

**Similar**: [012-WARP_GETWARPERMCSTATUS.md](../02_Warping/012-WARP_GETWARPERMCSTATUS.md)

---

## Query/Code Location

**File**: `BeamingDataService.cs`
**Method**: `BEAM_GETBEAMERMCSTATUS()`
**Line**: 586-638

```csharp
public List<BEAM_GETBEAMERMCSTATUS> BEAM_GETBEAMERMCSTATUS(string P_MCNO)
{
    List<BEAM_GETBEAMERMCSTATUS> results = null;

    if (!HasConnection())
        return results;

    BEAM_GETBEAMERMCSTATUSParameter dbPara = new BEAM_GETBEAMERMCSTATUSParameter();
    dbPara.P_MCNO = P_MCNO;

    List<BEAM_GETBEAMERMCSTATUSResult> dbResults = null;

    try
    {
        dbResults = DatabaseManager.Instance.BEAM_GETBEAMERMCSTATUS(dbPara);
        if (null != dbResults)
        {
            results = new List<BEAM_GETBEAMERMCSTATUS>();

            foreach (BEAM_GETBEAMERMCSTATUSResult dbResult in dbResults)
            {
                BEAM_GETBEAMERMCSTATUS inst = new BEAM_GETBEAMERMCSTATUS();

                inst.BEAMERNO = dbResult.BEAMERNO;
                inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                inst.TOTALYARN = dbResult.TOTALYARN;
                inst.TOTALKEBA = dbResult.TOTALKEBA;
                inst.STARTDATE = dbResult.STARTDATE;
                inst.ENDDATE = dbResult.ENDDATE;
                inst.CREATEBY = dbResult.CREATEBY;
                inst.CREATEDATE = dbResult.CREATEDATE;
                inst.STATUS = dbResult.STATUS;
                inst.FINISHFLAG = dbResult.FINISHFLAG;
                inst.MCNO = dbResult.MCNO;
                inst.WARPHEADNO = dbResult.WARPHEADNO;
                inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                inst.ADJUSTKEBA = dbResult.ADJUSTKEBA;
                inst.REMARK = dbResult.REMARK;
                inst.NOWARPBEAM = dbResult.NOWARPBEAM;
                inst.TOTALBEAM = dbResult.TOTALBEAM;

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

**File**: 030/296 | **Progress**: 10.1%
