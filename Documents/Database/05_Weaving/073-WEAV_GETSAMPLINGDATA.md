# WEAV_GETSAMPLINGDATA

**Procedure Number**: 073 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get sampling data for specific beam and loom |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:1100 → WEAV_GETSAMPLINGDATA() |
| **Frequency** | Medium - When loading weaving lot for sampling |
| **Performance** | Fast - Indexed by beam roll and loom |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll number |
| `P_LOOM` | VARCHAR2(50) | ✅ | Loom machine number |

**Note**: Both parameters required to identify specific weaving lot on loom

### Output (OUT)

None

### Returns (Cursor - Single/Multiple Records)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERROLL` | VARCHAR2 | Beam roll number |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `ITM_WEAVING` | VARCHAR2 | Item code for weaving |
| `SETTINGDATE` | DATE | When loom setup was completed |
| `BARNO` | VARCHAR2 | Bar/spiral number for sample tracking |
| `SPIRAL_L` | NUMBER | Left spiral measurement (meters) |
| `SPIRAL_R` | NUMBER | Right spiral measurement (meters) |
| `STSAMPLING` | NUMBER | Start sampling meter position |
| `RECUTSAMPLING` | NUMBER | Recut sampling meter position |
| `STSAMPLINGBY` | VARCHAR2 | Operator who took start sample |
| `RECUTBY` | VARCHAR2 | Operator who took recut sample |
| `STDATE` | DATE | Start sampling timestamp |
| `RECUTDATE` | DATE | Recut sampling timestamp |
| `REMARK` | VARCHAR2 | Additional notes |
| `BEAMMC` | VARCHAR2 | Beaming machine number (upstream) |
| `WARPMC` | VARCHAR2 | Warping machine number (upstream) |
| `BEAMERNO` | VARCHAR2 | Beaming operator ID |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves comprehensive sampling data for a specific weaving lot, including upstream traceability (beam and warp sources) and sampling positions for quality control.

**Business Context**:
- **Sampling**: Fabric samples are cut at specific meter positions during production
- Each weaving lot requires at least 2 samples:
  - **Start Sample (STSAMPLING)**: Taken at beginning of production (typically 50-100 meters)
  - **Recut Sample (RECUTSAMPLING)**: Taken later if start sample fails lab tests
- Samples sent to lab for testing (tensile strength, tear resistance, air permeability, etc.)
- Sample tracking ensures lab results can be traced back to exact production conditions
- Spiral measurements (SPIRAL_L, SPIRAL_R) track fabric warping/skewing at sample positions

**Critical for**:
- Lab sample traceability
- Quality investigation when defects found
- Upstream traceability (beam → warp → yarn)
- Sample collection planning
- Quality documentation for customers

**Sampling Workflow**:
1. Operator starts weaving lot on loom
2. When meter counter reaches STSAMPLING position (e.g., 50m):
   - Operator marks fabric with bar code (BARNO)
   - Records spiral measurements (left and right edges)
   - Cuts sample (typically 1-2 meters)
   - Records operator ID and timestamp
3. Sample sent to lab for testing
4. If sample fails lab test:
   - Take RECUTSAMPLING at different position
   - Retest to confirm issue

**Usage Scenarios**:

**Scenario 1: Lab Sample Traceability**
1. Lab receives fabric sample with bar code "BAR-2024-0015"
2. Lab technician scans bar code
3. System calls:
   ```
   WEAV_GETSAMPLINGDATA(
       P_BEAMROLL: "BM-2024-100",
       P_LOOM: "L-005"
   )
   ```
4. Returns sampling data:
   - Sample taken at 50 meters
   - By operator "EMP-001" on 2024-10-10 10:30
   - Spiral measurements: Left=48.5cm, Right=49.0cm (0.5cm difference)
   - Source beam: BM-2024-100 (Beaming machine BM-01)
   - Source warp: WP-2024-050 (Warping machine WP-03)
5. Lab records test results linked to this sample
6. If results fail, can trace back to yarn lot, machine conditions, operators

**Scenario 2: Recut Sample After Failure**
1. Initial sample (50m position) fails tensile test
2. Quality supervisor reviews:
   ```
   var data = service.WEAV_GETSAMPLINGDATA("BM-2024-100", "L-005");
   ```
3. Finds: STSAMPLING = 50m, RECUTSAMPLING = null
4. Decides to take recut sample at 150 meters
5. Operator records:
   - RECUTSAMPLING = 150
   - RECUTBY = "EMP-005"
   - RECUTDATE = 2024-10-10 14:00
6. New sample sent to lab
7. If recut passes → issue was localized defect at 50m
8. If recut fails → systemic problem (yarn, machine, settings)

**Scenario 3: Quality Investigation**
1. Customer complains about fabric quality
2. Customer provides lot number and approximate meter position
3. QA engineer retrieves sampling data:
   ```
   var sampling = service.WEAV_GETSAMPLINGDATA("BM-2024-100", "L-005");
   ```
4. Compares customer complaint position with sample positions
5. Retrieves lab test results for those samples
6. Traces upstream:
   - BEAMMC: "BM-01" → Check beaming conditions
   - WARPMC: "WP-03" → Check warping parameters
   - Reviews spiral measurements (fabric skew issues?)
7. Determines root cause: Warp tension problem on WP-03

**Scenario 4: Sample Collection Planning**
1. Sampling team prepares daily collection route
2. For each active loom, checks if sample is due:
   ```csharp
   private bool IsSampleDue(string beamRoll, string loomNo, decimal currentMeter)
   {
       var sampling = service.WEAV_GETSAMPLINGDATA(beamRoll, loomNo);

       if (sampling == null || sampling.Count == 0)
           return true; // No sample taken yet - urgent!

       var data = sampling[0];

       // Check if start sample taken
       if (!data.STSAMPLING.HasValue)
           return true; // Start sample due

       // Check if recut sample needed but not taken
       if (data.RECUTSAMPLING.HasValue && data.RECUTDATE == null)
           return true; // Recut sample marked but not collected

       return false; // All samples collected
   }
   ```
5. System generates collection list with priorities

**Scenario 5: Spiral Measurement Analysis**
1. Quality team analyzes spiral drift across production
2. Query all sampling data for last month
3. Calculate average spiral difference (left vs right)
4. Identify looms with consistent spiral problems:
   ```csharp
   var allSamples = GetAllSamplesLastMonth();

   var loomAnalysis = allSamples
       .Where(s => s.SPIRAL_L.HasValue && s.SPIRAL_R.HasValue)
       .GroupBy(s => s.LOOMNO)
       .Select(g => new {
           Loom = g.Key,
           AvgDiff = g.Average(s => Math.Abs(s.SPIRAL_L.Value - s.SPIRAL_R.Value)),
           Samples = g.Count()
       })
       .Where(x => x.AvgDiff > 1.0m) // > 1cm difference is problem
       .OrderByDescending(x => x.AvgDiff);

   // Result: L-003 has 2.5cm average difference → Needs alignment
   ```

**Data Enrichment**:
- Joins 3 tables (Weaving → Beaming → Warping)
- Provides complete upstream traceability
- Enables root cause analysis across entire production chain
- BEAMMC, WARPMC, BEAMERNO allow investigation of upstream issues

---

## Related Procedures

**Sampling Management**:
- [WEAV_SAMPLING](./WEAV_SAMPLING.md) - Record/update sampling information
- [LAB_GETWEAVINGSAMPLING](../14_LAB/LAB_GETWEAVINGSAMPLING.md) - Lab system sample retrieval

**Production Tracking**:
- [WEAV_GETINPROCESSBYBEAMROLL](./068-WEAV_GETINPROCESSBYBEAMROLL.md) - Current production on loom
- [WEAVE_WEAVINGPROCESS](./062-WEAVE_WEAVINGPROCESS.md) - Record weaving production

**Upstream Traceability**:
- [BEAM_GETBEAMROLLDETAIL](../03_Beaming/BEAM_GETBEAMROLLDETAIL.md) - Beam details
- [WARP_GETWARPERROLLDETAIL](../02_Warping/WARP_GETWARPERROLLDETAIL.md) - Warp details

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETSAMPLINGDATA(string P_BEAMROLL, string P_LOOM)`
**Lines**: 1100-1154

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETSAMPLINGDATA(WEAV_GETSAMPLINGDATAParameter para)`
**Lines**: 13270-13328

**Stored Procedure Call**:
```csharp
// Two required parameters - beam and loom
string[] paraNames = new string[]
{
    "P_BEAMROLL",   // Beam roll ID
    "P_LOOM"        // Loom machine ID
};

object[] paraValues = new object[]
{
    para.P_BEAMROLL,
    para.P_LOOM
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_GETSAMPLINGDATA",
    paraNames, paraValues);
```

**Usage Example 1: Load Sampling Data for Lab Sample**
```csharp
// When lab receives sample, load production context
private void LoadSampleContext(string barCode)
{
    // Parse bar code to get beam and loom
    // (Barcode format: "BAR-BEAMROLL-LOOMNO-POSITION")
    string[] parts = barCode.Split('-');
    string beamRoll = parts[1];
    string loomNo = parts[2];

    WeavingDataService service = WeavingDataService.Instance;
    var samplingData = service.WEAV_GETSAMPLINGDATA(beamRoll, loomNo);

    if (samplingData == null || samplingData.Count == 0)
    {
        MessageBox.Show("No sampling data found for this sample!");
        return;
    }

    var data = samplingData[0];

    // Display traceability information
    txtBeamRoll.Text = data.BEAMERROLL;
    txtLoom.Text = data.LOOMNO;
    txtItem.Text = data.ITM_WEAVING;
    txtStartSample.Text = $"{data.STSAMPLING}m by {data.STSAMPLINGBY} at {data.STDATE:yyyy-MM-dd HH:mm}";

    if (data.RECUTSAMPLING.HasValue)
    {
        txtRecutSample.Text = $"{data.RECUTSAMPLING}m by {data.RECUTBY} at {data.RECUTDATE:yyyy-MM-dd HH:mm}";
    }

    // Upstream traceability
    txtBeamMachine.Text = data.BEAMMC;
    txtWarpMachine.Text = data.WARPMC;
    txtBeamer.Text = data.BEAMERNO;

    // Spiral measurements
    decimal spiralDiff = Math.Abs((data.SPIRAL_L ?? 0) - (data.SPIRAL_R ?? 0));
    txtSpiralL.Text = $"{data.SPIRAL_L}cm";
    txtSpiralR.Text = $"{data.SPIRAL_R}cm";
    txtSpiralDiff.Text = $"{spiralDiff}cm";

    if (spiralDiff > 1.0m)
    {
        lblSpiralWarning.Text = "⚠ Significant spiral detected!";
        lblSpiralWarning.ForeColor = Color.Red;
    }
}
```

**Usage Example 2: Check Sample Collection Status**
```csharp
// Daily report: Which lots need samples collected?
private void GenerateSampleCollectionList()
{
    // Get all active weaving lots
    var activeLots = GetActiveWeavingLots(); // Returns List<(beamRoll, loomNo, currentMeter)>

    var sampleDueList = new List<SampleDueInfo>();
    WeavingDataService service = WeavingDataService.Instance;

    foreach (var lot in activeLots)
    {
        var sampling = service.WEAV_GETSAMPLINGDATA(lot.beamRoll, lot.loomNo);

        SampleDueInfo info = new SampleDueInfo
        {
            BeamRoll = lot.beamRoll,
            LoomNo = lot.loomNo,
            CurrentMeter = lot.currentMeter
        };

        if (sampling == null || sampling.Count == 0)
        {
            // No sample record at all - start sample overdue!
            info.Status = "⚠ START SAMPLE OVERDUE";
            info.Priority = 1; // Highest priority
            info.ExpectedPosition = 50; // Standard start sample position
            sampleDueList.Add(info);
            continue;
        }

        var data = sampling[0];

        // Check start sample
        if (!data.STSAMPLING.HasValue || !data.STDATE.HasValue)
        {
            info.Status = "⚠ START SAMPLE DUE";
            info.Priority = 1;
            info.ExpectedPosition = 50;
            sampleDueList.Add(info);
        }
        // Check recut sample
        else if (data.RECUTSAMPLING.HasValue && !data.RECUTDATE.HasValue)
        {
            info.Status = "⚠ RECUT SAMPLE DUE";
            info.Priority = 2;
            info.ExpectedPosition = data.RECUTSAMPLING.Value;
            sampleDueList.Add(info);
        }
    }

    // Sort by priority
    sampleDueList = sampleDueList.OrderBy(x => x.Priority).ThenBy(x => x.LoomNo).ToList();

    // Display collection list
    dgSampleCollection.DataSource = sampleDueList;
    lblTotalDue.Text = $"Total samples due: {sampleDueList.Count}";
}

public class SampleDueInfo
{
    public string BeamRoll { get; set; }
    public string LoomNo { get; set; }
    public decimal CurrentMeter { get; set; }
    public string Status { get; set; }
    public int Priority { get; set; }
    public decimal ExpectedPosition { get; set; }
}
```

**Usage Example 3: Spiral Drift Analysis**
```csharp
// Analyze spiral measurements across looms to identify alignment issues
private void AnalyzeSpiralDrift()
{
    WeavingDataService service = WeavingDataService.Instance;

    // Get all recent sampling data (requires getting active lots first)
    var activeLots = GetActiveWeavingLots();
    var allSamples = new List<WEAV_GETSAMPLINGDATA>();

    foreach (var lot in activeLots)
    {
        var sampling = service.WEAV_GETSAMPLINGDATA(lot.beamRoll, lot.loomNo);
        if (sampling != null && sampling.Count > 0)
        {
            allSamples.AddRange(sampling);
        }
    }

    // Calculate spiral statistics per loom
    var loomStats = allSamples
        .Where(s => s.SPIRAL_L.HasValue && s.SPIRAL_R.HasValue)
        .GroupBy(s => s.LOOMNO)
        .Select(g => new
        {
            Loom = g.Key,
            AvgLeft = g.Average(s => s.SPIRAL_L.Value),
            AvgRight = g.Average(s => s.SPIRAL_R.Value),
            AvgDiff = g.Average(s => Math.Abs(s.SPIRAL_L.Value - s.SPIRAL_R.Value)),
            MaxDiff = g.Max(s => Math.Abs(s.SPIRAL_L.Value - s.SPIRAL_R.Value)),
            SampleCount = g.Count()
        })
        .OrderByDescending(x => x.AvgDiff)
        .ToList();

    // Display results
    StringBuilder report = new StringBuilder();
    report.AppendLine("SPIRAL DRIFT ANALYSIS");
    report.AppendLine(new string('=', 70));
    report.AppendLine($"{"Loom",-8} {"Samples",-8} {"Avg Left",-10} {"Avg Right",-10} {"Avg Diff",-10} {"Max Diff",-10}");
    report.AppendLine(new string('-', 70));

    foreach (var stat in loomStats)
    {
        string flag = stat.AvgDiff > 1.0m ? " ⚠" : "";
        report.AppendLine(
            $"{stat.Loom,-8} {stat.SampleCount,-8} " +
            $"{stat.AvgLeft,-10:F1} {stat.AvgRight,-10:F1} " +
            $"{stat.AvgDiff,-10:F1} {stat.MaxDiff,-10:F1}{flag}");
    }

    report.AppendLine();
    report.AppendLine("⚠ = Average difference > 1.0cm - Loom alignment required");

    txtReport.Text = report.ToString();

    // Flag looms needing maintenance
    var problemLooms = loomStats.Where(x => x.AvgDiff > 1.0m).ToList();
    if (problemLooms.Any())
    {
        MessageBox.Show(
            $"{problemLooms.Count} loom(s) require alignment adjustment:\n" +
            string.Join(", ", problemLooms.Select(x => x.Loom)),
            "Maintenance Required",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }
}
```

**Usage Example 4: Upstream Traceability Report**
```csharp
// Generate full traceability report from fabric sample back to yarn
private void GenerateTraceabilityReport(string beamRoll, string loomNo)
{
    WeavingDataService service = WeavingDataService.Instance;
    var sampling = service.WEAV_GETSAMPLINGDATA(beamRoll, loomNo);

    if (sampling == null || sampling.Count == 0)
    {
        MessageBox.Show("No sampling data found!");
        return;
    }

    var data = sampling[0];

    string report = $@"
FABRIC SAMPLE TRACEABILITY REPORT
================================================================================

Sample Information:
  Bar Code: {data.BARNO}
  Item Code: {data.ITM_WEAVING}
  Sample Date: {data.STDATE:yyyy-MM-dd HH:mm}
  Sampled By: {data.STSAMPLINGBY}
  Sample Position: {data.STSAMPLING}m

Weaving Stage:
  Loom: {data.LOOMNO}
  Beam Roll: {data.BEAMERROLL}
  Setup Date: {data.SETTINGDATE:yyyy-MM-dd}

Spiral Measurements:
  Left Edge: {data.SPIRAL_L}cm
  Right Edge: {data.SPIRAL_R}cm
  Difference: {Math.Abs((data.SPIRAL_L ?? 0) - (data.SPIRAL_R ?? 0)):F1}cm
  {(Math.Abs((data.SPIRAL_L ?? 0) - (data.SPIRAL_R ?? 0)) > 1.0m ? "  ⚠ WARNING: Excessive spiral drift detected!" : "")}

Upstream Traceability:
  Beaming Machine: {data.BEAMMC}
  Beaming Operator: {data.BEAMERNO}
  Warping Machine: {data.WARPMC}

Recut Sample Information:
{(data.RECUTSAMPLING.HasValue ?
  $@"  Recut Position: {data.RECUTSAMPLING}m
  Recut Date: {data.RECUTDATE:yyyy-MM-dd HH:mm}
  Recut By: {data.RECUTBY}
  Reason: Initial sample required retest" :
  "  No recut sample taken")}

Additional Notes:
{data.REMARK ?? "None"}

================================================================================
Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}
";

    txtTraceability.Text = report;

    // Can extend to query yarn lots from WARPMC using additional procedures
    // LoadYarnLots(data.WARPMC);
}
```

---

**File**: 073/296 | **Progress**: 24.7%
