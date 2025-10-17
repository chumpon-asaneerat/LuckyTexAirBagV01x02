# LAB_GETWEAVINGSAMPLING

**Procedure Number**: 304 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get greige (weaving) sample details for lab testing |
| **Operation** | SELECT |
| **Tables** | tblLabGreigeSampling (assumed) |
| **Called From** | LABDataService.cs → LAB_GETWEAVINGSAMPLING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ✅ | Beam roll number from beaming |
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERROLL` | VARCHAR2(50) | Beam roll number |
| `LOOMNO` | VARCHAR2(50) | Loom machine number |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `SETTINGDATE` | DATE | Loom setup date |
| `BARNO` | VARCHAR2(50) | Bar/reed number used |
| `SPIRAL_L` | NUMBER | Left spiral measurement |
| `SPIRAL_R` | NUMBER | Right spiral measurement |
| `STSAMPLING` | NUMBER | Start sampling position (meters) |
| `RECUTSAMPLING` | NUMBER | Recut sampling position (meters) |
| `STSAMPLINGBY` | VARCHAR2(50) | Operator who took start sample |
| `RECUTBY` | VARCHAR2(50) | Operator who took recut sample |
| `STDATE` | DATE | Start sampling date |
| `RECUTDATE` | DATE | Recut sampling date |
| `REMARK` | VARCHAR2(500) | Sampling remarks/notes |

---

## Business Logic (What it does and why)

Retrieves greige fabric (unfinished weaving) sample details for laboratory testing. Used to load sample information when performing tests on raw woven fabric before finishing process.

**Workflow**:
1. Receives beam roll number and loom number (unique identifier)
2. Queries greige sampling database
3. Returns complete sample information:
   - Weaving setup details (loom, beam roll, bar number)
   - Spiral measurements (fabric tension indicators)
   - Sampling positions (where samples were cut)
   - Operator and date tracking
4. Data used to populate greige test entry screens

**Business Rules**:
- Beam roll + loom number combination identifies unique greige sample
- Sample can be taken at start of weaving (ST) and recut later
- Spiral measurements indicate fabric warp tension uniformity
- Sampling position critical for identifying sample location on fabric roll

**Greige Testing Context**:

**When Greige Samples Taken**:
1. **Start Sampling (STSAMPLING)**:
   - Taken at beginning of weaving lot
   - Position recorded in meters from start
   - Used for initial quality assessment

2. **Recut Sampling (RECUTSAMPLING)**:
   - Taken during or after weaving
   - Position recorded for traceability
   - May be taken if quality issue suspected

**Spiral Measurements**:
- **SPIRAL_L (Left)**: Warp tension on left edge
- **SPIRAL_R (Right)**: Warp tension on right edge
- Difference indicates tension imbalance
- Critical for airbag fabric quality (must be uniform)

**Typical Usage**:
```csharp
var sample = LAB_GETWEAVINGSAMPLING(beamRoll, loomNo);

if (sample != null)
{
    // Load sample into greige test screen
    txtBeamRoll.Text = sample.BEAMERROLL;
    txtLoomNo.Text = sample.LOOMNO;
    txtItemCode.Text = sample.ITM_WEAVING;
    txtBarNo.Text = sample.BARNO;

    // Display spiral measurements
    txtSpiralLeft.Text = sample.SPIRAL_L.ToString();
    txtSpiralRight.Text = sample.SPIRAL_R.ToString();

    // Check spiral balance
    decimal spiralDiff = Math.Abs(sample.SPIRAL_L.Value - sample.SPIRAL_R.Value);
    if (spiralDiff > 5) // Tolerance check
    {
        lblWarning.Text = "WARNING: Spiral imbalance detected!";
        lblWarning.ForeColor = Color.Red;
    }

    // Show sampling positions
    txtStartPosition.Text = $"{sample.STSAMPLING}m from start";
    if (sample.RECUTSAMPLING.HasValue)
    {
        txtRecutPosition.Text = $"{sample.RECUTSAMPLING}m from start";
    }

    // Proceed with greige test entry
    EnableGreigeTestInputs();
}
```

**Greige Test Workflow**:
1. **Weaving**: Fabric woven on loom
2. **Sample Cut**: Quality team cuts sample at specific position
3. **Record Sampling**: Save sample details (this data)
4. **Check if Received**: LAB_CHECKRECEIVEGREIGESAMPLING
5. **Get Sample Details**: LAB_GETWEAVINGSAMPLING (this procedure)
6. **Perform Tests**:
   - Thread count (warp/weft)
   - Weight per square meter
   - Width measurement
   - Strength tests
7. **Save Results**: LAB_SAVELABGREIGERESULT
8. **Approve**: LAB_APPROVELABDATA

**Data Relationship**:
- Similar to [299-LAB_GETFINISHINGSAMPLING.md](./299-LAB_GETFINISHINGSAMPLING.md) but for greige
- GETFINISHINGSAMPLING: For finished fabric (after coating)
- GETWEAVINGSAMPLING: For greige fabric (raw weaving)

---

## Related Procedures

**Similar**: [299-LAB_GETFINISHINGSAMPLING.md](./299-LAB_GETFINISHINGSAMPLING.md) - Gets finished fabric sample
**Upstream**: [297-LAB_CHECKRECEIVEGREIGESAMPLING.md](./297-LAB_CHECKRECEIVEGREIGESAMPLING.md) - Checks if sample received
**Downstream**: LAB_SAVELABGREIGERESULT - Saves greige test results
**Related**: [305-LAB_GREIGESTOCKSTATUS.md](./305-LAB_GREIGESTOCKSTATUS.md) - View greige sample status

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GETWEAVINGSAMPLING()`
**Lines**: Likely in greige sample section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GETWEAVINGSAMPLING(LAB_GETWEAVINGSAMPLINGParameter para)`
**Lines**: 4026-4052

**Return Structure** (14 columns):
```csharp
public class LAB_GETWEAVINGSAMPLINGResult
{
    // Weaving identification
    public string BEAMERROLL { get; set; }
    public string LOOMNO { get; set; }
    public string ITM_WEAVING { get; set; }
    public DateTime? SETTINGDATE { get; set; }
    public string BARNO { get; set; }

    // Spiral measurements (tension)
    public decimal? SPIRAL_L { get; set; }
    public decimal? SPIRAL_R { get; set; }

    // Sampling positions
    public decimal? STSAMPLING { get; set; }      // Start position
    public decimal? RECUTSAMPLING { get; set; }   // Recut position

    // Operator tracking
    public string STSAMPLINGBY { get; set; }
    public string RECUTBY { get; set; }
    public DateTime? STDATE { get; set; }
    public DateTime? RECUTDATE { get; set; }

    // Notes
    public string REMARK { get; set; }
}
```

**Greige vs Finishing Sample Comparison**:
| Aspect | Greige Sample | Finishing Sample |
|--------|---------------|------------------|
| **Identifier** | Beam roll + Loom | Weaving lot + Item code |
| **Stage** | After weaving | After coating/finishing |
| **Measurements** | Spiral, thread count | Weight, air permeability |
| **Purpose** | Raw fabric quality | Final product quality |

---

**File**: 304/296 | **Progress**: 102.7%
