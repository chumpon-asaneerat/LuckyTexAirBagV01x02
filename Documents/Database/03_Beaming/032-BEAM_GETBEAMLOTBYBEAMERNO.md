# BEAM_GETBEAMLOTBYBEAMERNO

**Procedure Number**: 032 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beam lots/rolls produced by a specific beamer setup |
| **Operation** | SELECT |
| **Tables** | tblBeamingProcess |
| **Called From** | BeamingDataService.cs:435 → BEAM_GETBEAMLOTBYBEAMERNO() |
| **Frequency** | High (viewing setup details) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup number |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2(50) | Beamer setup number |
| `BEAMLOT` | VARCHAR2(50) | Beam lot number |
| `BEAMNO` | VARCHAR2(50) | Beam number |
| `STARTDATE` | DATE | Start timestamp |
| `ENDDATE` | DATE | Completion timestamp |
| `LENGTH` | NUMBER | Beam length (meters) |
| `SPEED` | NUMBER | Average speed |
| `HARDNESS_L/N/R` | NUMBER | Hardness 3 points |
| `BEAMSTANDTENSION` | NUMBER | Stand tension |
| `WINDINGTENSION` | NUMBER | Winding tension |
| `INSIDE_WIDTH` | NUMBER | Inside width |
| `OUTSIDE_WIDTH` | NUMBER | Outside width |
| `FULL_WIDTH` | NUMBER | Full width |
| `STARTBY` | VARCHAR2(50) | Start operator |
| `DOFFBY` | VARCHAR2(50) | Doff operator |
| `FLAG` | VARCHAR2(10) | Status flag |
| `BEAMMC` | VARCHAR2(50) | Machine number |
| `REMARK` | VARCHAR2(500) | Notes |
| `TENSION_ST1-ST10` | NUMBER | 10 station tensions |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2(50) | Last editor |
| `OLDBEAMNO` | VARCHAR2(50) | Old beam number |
| `KEBA` | NUMBER | Keba count |
| `MISSYARN` | NUMBER | Missing yarn count |
| `OTHER` | VARCHAR2(200) | Other info |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingProcess` - SELECT - All beams produced from this setup

**Transaction**: No (read-only)

---

## Business Logic (What it does and why)

Retrieves all beam rolls produced from a specific beamer setup. Shows complete production history and quality parameters for all beams in the setup.

**Workflow**:
1. User views beamer setup details
2. System loads all beams produced
3. Shows list of completed beam rolls
4. User can drill down into individual beams

**Business Rules**:
- Shows all beams from setup (may be multiple)
- Includes quality parameters for each beam
- Shows traceability from setup to individual beams
- Returns beams in production order

**Use Cases**:
- Setup review
- Quality analysis across multiple beams
- Production history
- Traceability reporting

---

## Related Procedures

**Similar**: [011-WARP_GETWARPERLOTBYHEADNO.md](../02_Warping/011-WARP_GETWARPERLOTBYHEADNO.md)

---

## Query/Code Location

**File**: `BeamingDataService.cs`
**Method**: `BEAM_GETBEAMLOTBYBEAMERNO()`
**Line**: 435-510

```csharp
public List<BEAM_GETBEAMLOTBYBEAMERNO> BEAM_GETBEAMLOTBYBEAMERNO(string P_BEAMERNO)
{
    List<BEAM_GETBEAMLOTBYBEAMERNO> results = null;

    if (!HasConnection())
        return results;

    BEAM_GETBEAMLOTBYBEAMERNOParameter dbPara = new BEAM_GETBEAMLOTBYBEAMERNOParameter();
    dbPara.P_BEAMERNO = P_BEAMERNO;

    List<BEAM_GETBEAMLOTBYBEAMERNOResult> dbResults = null;

    try
    {
        dbResults = DatabaseManager.Instance.BEAM_GETBEAMLOTBYBEAMERNO(dbPara);
        if (null != dbResults)
        {
            results = new List<BEAM_GETBEAMLOTBYBEAMERNO>();
            foreach (BEAM_GETBEAMLOTBYBEAMERNOResult dbResult in dbResults)
            {
                BEAM_GETBEAMLOTBYBEAMERNO inst = new BEAM_GETBEAMLOTBYBEAMERNO();

                inst.BEAMERNO = dbResult.BEAMERNO;
                inst.BEAMLOT = dbResult.BEAMLOT;
                inst.BEAMNO = dbResult.BEAMNO;
                inst.STARTDATE = dbResult.STARTDATE;
                inst.ENDDATE = dbResult.ENDDATE;
                inst.LENGTH = dbResult.LENGTH;
                inst.SPEED = dbResult.SPEED;
                inst.HARDNESS_L = dbResult.HARDNESS_L;
                inst.HARDNESS_N = dbResult.HARDNESS_N;
                inst.HARDNESS_R = dbResult.HARDNESS_R;
                inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                inst.STARTBY = dbResult.STARTBY;
                inst.DOFFBY = dbResult.DOFFBY;
                inst.FLAG = dbResult.FLAG;
                inst.BEAMMC = dbResult.BEAMMC;
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

                inst.EDITDATE = dbResult.EDITDATE;
                inst.EDITBY = dbResult.EDITBY;
                inst.OLDBEAMNO = dbResult.OLDBEAMNO;
                inst.KEBA = dbResult.KEBA;
                inst.MISSYARN = dbResult.MISSYARN;
                inst.OTHER = dbResult.OTHER;

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

**File**: 032/296 | **Progress**: 10.8%
