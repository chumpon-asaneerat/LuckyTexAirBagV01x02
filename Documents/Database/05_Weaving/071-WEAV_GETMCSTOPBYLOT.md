# WEAV_GETMCSTOPBYLOT

**Procedure Number**: 071 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve all machine stop events for a specific weaving lot |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:934 → WEAV_GETMCSTOPBYLOT() |
| **Frequency** | Medium - Viewed when reviewing lot history or troubleshooting |
| **Performance** | Fast - Indexed by WEAVINGLOT |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number to retrieve stops for |

### Output (OUT)

None

### Returns (Cursor - Multiple Records)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `DEFECTCODE` | VARCHAR2 | Defect code that caused the stop |
| `DEFECTPOSITION` | NUMBER | Position/meter where defect occurred |
| `CREATEBY` | VARCHAR2 | Operator who recorded the stop |
| `CREATEDATE` | DATE | When stop was recorded |
| `REMARK` | VARCHAR2 | Additional notes about the stop |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `BEAMERROLL` | VARCHAR2 | Beam roll number |
| `DOFFNO` | NUMBER | Doff/roll number |
| `DEFECTLENGTH` | NUMBER | Length of defective fabric (meters) |
| `STOPDATE` | DATE | When machine actually stopped |
| `DESCRIPTION` | VARCHAR2 | Defect description (from defect master) |
| `WEAVSTARTDATE` | DATE | Lot production start date |
| `WEAVFINISHDATE` | DATE | Lot production finish date |
| `ITM_WEAVING` | VARCHAR2 | Item code being produced |
| `WIDTH` | NUMBER | Fabric width |
| `LENGTH` | NUMBER | Total lot length |

---

## Business Logic (What it does and why)

**Purpose**: Provides complete history of all machine stops for a weaving lot, used for quality analysis, downtime tracking, and root cause investigation.

**Business Context**:
- During weaving, machines stop for various reasons (defects, yarn breaks, maintenance)
- Each stop is recorded with defect code, position, and operator
- Stop history is critical for:
  - OEE (Overall Equipment Effectiveness) calculation
  - Downtime analysis
  - Quality investigation
  - Operator performance tracking
  - Process improvement initiatives

**Machine Stop Categories**:
1. **Quality Defects**: Warp/weft breaks, tension issues, contamination
2. **Material Issues**: Yarn quality, yarn out, beam change
3. **Mechanical**: Machine malfunction, maintenance required
4. **Operational**: Setup, changeover, cleaning

**Usage Scenarios**:

**Scenario 1: Quality Investigation**
1. Quality inspector finds defect on finished roll
2. Enters weaving lot number
3. System calls WEAV_GETMCSTOPBYLOT(lotNumber)
4. Returns all stops during production
5. Inspector reviews stops near defect position
6. Identifies pattern: "Multiple stops at 250-260m → warp break"
7. Root cause: Weak yarn in specific pallet
8. Corrective action: Notify yarn supplier

**Scenario 2: OEE Calculation**
1. Production manager reviewing daily efficiency
2. Selects lot to analyze downtime
3. System loads all machine stops
4. Calculates total stop duration
5. Categorizes stops by defect type
6. Identifies:
   - Planned stops: 30 minutes (changeover)
   - Unplanned stops: 2 hours (yarn breaks)
7. OEE = (Available - Downtime) / Available
8. Generates improvement action items

**Scenario 3: Operator Training**
1. New operator training on defect recognition
2. Supervisor reviews lot with multiple stops
3. Shows stop history with defect positions
4. Explains each defect code and how to identify
5. Uses real production data as training material

**Scenario 4: Process Improvement**
1. Engineering team analyzing chronic issues
2. Reviews stops across multiple lots
3. Identifies common defect codes
4. Statistics show: 40% stops due to "D010 Warp Break"
5. Investigates warp break causes:
   - Tension too high
   - Yarn quality issue
   - Beam preparation problem
6. Implements corrective actions
7. Monitors future lots for improvement

**Scenario 5: Lot Review Before Finishing**
1. Before sending lot to finishing, review quality
2. Load machine stop history
3. Check if any critical defects recorded
4. If serious defects found → may reject entire lot
5. If minor defects → mark positions for inspection
6. Decision: Send to inspection vs. rework vs. scrap

**Data Enrichment**:
- **DESCRIPTION**: Human-readable defect name (joined from tblDefectCode)
- **Lot Info**: Start/finish dates, item code, dimensions (from tblWeavingLot)
- **Position Tracking**: DEFECTPOSITION and DEFECTLENGTH allow precise location

**Key Metrics Derived**:
- Total stop count per lot
- Total stop duration
- Most common defect codes
- Stops per meter produced
- Mean time between stops
- First pass yield

---

## Related Procedures

**Machine Stop Management**:
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Record new machine stop
- [WEAV_DELETEMCSTOP](./WEAV_DELETEMCSTOP.md) - Delete incorrect stop record
- [WEAV_GETMCSTOPLISTBYDOFFNO](./WEAV_GETMCSTOPLISTBYDOFFNO.md) - Stops by doff number

**Defect Management**:
- [WEAV_DEFECTLIST](./WEAV_DEFECTLIST.md) - Master defect code list
- Defect analysis procedures

**Production Tracking**:
- [WEAV_GETINPROCESSBYBEAMROLL](./WEAV_GETINPROCESSBYBEAMROLL.md) - Current lot status
- [WEAV_WEAVINGINPROCESSLIST](./WEAV_WEAVINGINPROCESSLIST.md) - All active lots

**Reporting**:
- OEE calculation procedures
- Downtime analysis reports
- Quality trending reports

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETMCSTOPBYLOT(string P_WEAVINGLOT)`
**Lines**: 934-989

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETMCSTOPBYLOT(WEAV_GETMCSTOPBYLOTParameter para)`
**Lines**: 13397-13453

**Stored Procedure Call**:
```csharp
// Single parameter - weaving lot number
string[] paraNames = new string[]
{
    "P_WEAVINGLOT"
};

object[] paraValues = new object[]
{
    para.P_WEAVINGLOT
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_GETMCSTOPBYLOT",
    paraNames, paraValues);

// Returns cursor with comprehensive stop details
```

**Return Structure** (17 columns):
```csharp
public class WEAV_GETMCSTOPBYLOT
{
    public string WEAVINGLOT { get; set; }
    public string DEFECTCODE { get; set; }
    public decimal? DEFECTPOSITION { get; set; }
    public string CREATEBY { get; set; }
    public DateTime? CREATEDATE { get; set; }
    public string REMARK { get; set; }
    public string LOOMNO { get; set; }
    public string BEAMERROLL { get; set; }
    public decimal? DOFFNO { get; set; }
    public decimal? DEFECTLENGTH { get; set; }
    public string DESCRIPTION { get; set; }
    public DateTime? WEAVSTARTDATE { get; set; }
    public DateTime? WEAVFINISHDATE { get; set; }
    public string ITM_WEAVING { get; set; }
    public decimal? WIDTH { get; set; }
    public decimal? LENGTH { get; set; }
}
```

**Usage Example 1: Display Stop History**
```csharp
// Load and display all stops for a lot
private void LoadMachineStops(string weavingLot)
{
    WeavingDataService service = WeavingDataService.Instance;
    var stops = service.WEAV_GETMCSTOPBYLOT(weavingLot);

    if (stops == null || stops.Count == 0)
    {
        lblStatus.Text = "No machine stops recorded for this lot.";
        lblStatus.ForeColor = Color.Green;
        return;
    }

    // Bind to DataGridView
    dgStops.DataSource = stops;

    // Summary statistics
    lblTotalStops.Text = $"Total Stops: {stops.Count}";
    lblLotInfo.Text = $"Lot: {weavingLot} | Item: {stops[0].ITM_WEAVING}";
    lblDuration.Text = $"Start: {stops[0].WEAVSTARTDATE:yyyy-MM-dd} | " +
                      $"Finish: {stops[0].WEAVFINISHDATE:yyyy-MM-dd}";
}
```

**Usage Example 2: OEE Analysis**
```csharp
// Calculate downtime from machine stops
private void AnalyzeDowntime(string weavingLot)
{
    var stops = service.WEAV_GETMCSTOPBYLOT(weavingLot);

    if (stops == null || stops.Count == 0) return;

    // Group by defect code to find common issues
    var defectGroups = stops
        .GroupBy(s => new { s.DEFECTCODE, s.DESCRIPTION })
        .Select(g => new
        {
            DefectCode = g.Key.DEFECTCODE,
            Description = g.Key.DESCRIPTION,
            Count = g.Count(),
            Percentage = (g.Count() * 100.0) / stops.Count
        })
        .OrderByDescending(g => g.Count);

    // Display Pareto chart
    txtAnalysis.Clear();
    txtAnalysis.AppendText($"Machine Stop Analysis for {weavingLot}\n");
    txtAnalysis.AppendText($"{'=',-60}\n\n");
    txtAnalysis.AppendText($"Total Stops: {stops.Count}\n\n");

    txtAnalysis.AppendText($"Top Defects (Pareto Analysis):\n");
    foreach (var defect in defectGroups)
    {
        txtAnalysis.AppendText(
            $"  {defect.DefectCode,-10} {defect.Description,-30} " +
            $"{defect.Count,5} ({defect.Percentage:F1}%)\n");
    }

    // Calculate stops per meter
    decimal? totalLength = stops.FirstOrDefault()?.LENGTH;
    if (totalLength.HasValue && totalLength > 0)
    {
        decimal stopsPerMeter = stops.Count / totalLength.Value;
        txtAnalysis.AppendText($"\nStops per Meter: {stopsPerMeter:F4}\n");
    }
}
```

**Usage Example 3: Quality Investigation**
```csharp
// Find stops near specific defect position
private List<WEAV_GETMCSTOPBYLOT> FindStopsNearPosition(
    string weavingLot, decimal targetPosition, decimal tolerance = 5)
{
    var allStops = service.WEAV_GETMCSTOPBYLOT(weavingLot);

    // Find stops within tolerance of target position
    var nearbyStops = allStops
        .Where(s => s.DEFECTPOSITION.HasValue &&
                   Math.Abs(s.DEFECTPOSITION.Value - targetPosition) <= tolerance)
        .OrderBy(s => Math.Abs(s.DEFECTPOSITION.Value - targetPosition))
        .ToList();

    return nearbyStops;
}

// Usage
var stopsNearDefect = FindStopsNearPosition("WV-2024-001", 250m);
if (stopsNearDefect.Count > 0)
{
    MessageBox.Show(
        $"Found {stopsNearDefect.Count} machine stops near position 250m\n" +
        $"Primary defect: {stopsNearDefect[0].DESCRIPTION}",
        "Investigation Results");
}
```

**Usage Example 4: Export to Report**
```csharp
// Generate stop history report
private void ExportStopReport(string weavingLot)
{
    var stops = service.WEAV_GETMCSTOPBYLOT(weavingLot);

    if (stops == null || stops.Count == 0) return;

    var lotInfo = stops[0];

    using (StreamWriter writer = new StreamWriter($"StopReport_{weavingLot}.txt"))
    {
        writer.WriteLine("MACHINE STOP HISTORY REPORT");
        writer.WriteLine(new string('=', 80));
        writer.WriteLine($"Weaving Lot: {weavingLot}");
        writer.WriteLine($"Item Code: {lotInfo.ITM_WEAVING}");
        writer.WriteLine($"Production Period: {lotInfo.WEAVSTARTDATE:yyyy-MM-dd} to " +
                        $"{lotInfo.WEAVFINISHDATE:yyyy-MM-dd}");
        writer.WriteLine($"Total Length: {lotInfo.LENGTH} meters");
        writer.WriteLine($"Total Stops: {stops.Count}");
        writer.WriteLine(new string('=', 80));
        writer.WriteLine();

        writer.WriteLine($"{"#",-5} {"Stop Date",-20} {"Defect",-10} " +
                        $"{"Position",-10} {"Length",-10} {"Operator",-15}");
        writer.WriteLine(new string('-', 80));

        int i = 1;
        foreach (var stop in stops)
        {
            writer.WriteLine(
                $"{i,-5} {stop.STOPDATE,-20:yyyy-MM-dd HH:mm} " +
                $"{stop.DEFECTCODE,-10} {stop.DEFECTPOSITION,-10:F2} " +
                $"{stop.DEFECTLENGTH,-10:F2} {stop.CREATEBY,-15}");
            if (!string.IsNullOrEmpty(stop.REMARK))
            {
                writer.WriteLine($"      Remark: {stop.REMARK}");
            }
            i++;
        }
    }

    MessageBox.Show($"Report exported: StopReport_{weavingLot}.txt");
}
```

---

**File**: 071/296 | **Progress**: 24.0%
