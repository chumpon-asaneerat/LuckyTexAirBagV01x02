# WEAV_GETMCSTOPLISTBYDOFFNO

**Procedure Number**: 072 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get machine stops for specific doff/fabric roll |
| **Operation** | SELECT |
| **Tables** | tblWeavingMachineStop, tblDefectCode (joined) |
| **Called From** | WeavingDataService.cs:1160 → WEAV_GETMCSTOPLISTBYDOFFNO() |
| **Frequency** | Medium - Quality review per fabric roll |
| **Performance** | Fast - Indexed by composite key |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_DOFFNO` | NUMBER | ✅ | Doff/fabric roll number |
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll number |
| `P_WEAVELOT` | VARCHAR2(50) | ✅ | Weaving lot number |

**Note**: All 4 parameters required to uniquely identify the doff

### Output (OUT)

None

### Returns (Cursor - Multiple Records)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `DEFECTCODE` | VARCHAR2 | Defect code causing stop |
| `DEFECTPOSITION` | NUMBER | Meter position where defect occurred |
| `CREATEBY` | VARCHAR2 | Operator who recorded stop |
| `CREATEDATE` | DATE | When stop was recorded |
| `REMARK` | VARCHAR2 | Additional notes |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `BEAMERROLL` | VARCHAR2 | Beam roll number |
| `DOFFNO` | NUMBER | Doff number |
| `DEFECTLENGTH` | NUMBER | Length of defective section (meters) |
| `STOPDATE` | DATE | When machine stopped |
| `DESCRIPTION` | VARCHAR2 | Defect description (from master) |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWeavingMachineStop` - SELECT - Machine stop events
- `tblDefectCode` - SELECT - Defect descriptions (JOIN)

**Query Type**: Filtered by composite key (LOOM + DOFF + BEAM + LOT)

**Transaction**: No (Read-only operation)

### Likely Query Structure

```sql
-- Assumed stored procedure logic
SELECT
    s.WEAVINGLOT,
    s.DEFECTCODE,
    s.DEFECTPOSITION,
    s.CREATEBY,
    s.CREATEDATE,
    s.REMARK,
    s.LOOMNO,
    s.BEAMERROLL,
    s.DOFFNO,
    s.DEFECTLENGTH,
    s.STOPDATE,
    d.DESCRIPTION
FROM tblWeavingMachineStop s
LEFT JOIN tblDefectCode d ON s.DEFECTCODE = d.DEFECTCODE
WHERE s.LOOMNO = P_LOOMNO
  AND s.DOFFNO = P_DOFFNO
  AND s.BEAMERROLL = P_BEAMROLL
  AND s.WEAVINGLOT = P_WEAVELOT
ORDER BY s.STOPDATE, s.DEFECTPOSITION;
```

---

## Business Logic (What it does and why)

**Purpose**: Retrieves machine stop history for a specific fabric roll (doff), enabling quality tracking and defect mapping for that exact roll.

**Business Context**:
- **Doff**: A "doff" is one complete fabric roll produced on a loom
- Each doff may have multiple stops during production
- After doffing (removing fabric roll from loom), quality must be checked
- Stop history helps identify defect locations on the specific roll
- Critical for:
  - Roll-level quality inspection
  - Defect marking on finished roll
  - Roll grading decisions (A, B, C grade)
  - Customer quality documentation

**Doff-Specific Tracking**:
- Unlike WEAV_GETMCSTOPBYLOT (which gets ALL stops for entire lot)
- This gets stops for ONE specific fabric roll
- More granular for per-roll quality control
- Used when roll is being inspected or packaged

**Usage Scenarios**:

**Scenario 1: Post-Doff Quality Inspection**
1. Operator completes doff #5 from lot WV-2024-001
2. Inspector takes roll to inspection station
3. Scans doff barcode → System loads stop history
4. System calls:
   ```
   WEAV_GETMCSTOPLISTBYDOFFNO(
       P_LOOMNO: "L-001",
       P_DOFFNO: 5,
       P_BEAMROLL: "BM-2024-100",
       P_WEAVELOT: "WV-2024-001"
   )
   ```
5. Returns 3 stops:
   - 120m: D005 Weft Break
   - 235m: D010 Warp Break
   - 340m: D005 Weft Break
6. Inspector marks these positions on roll for detailed inspection

**Scenario 2: Roll Grading**
1. After inspection, grade roll based on defects
2. Review stop history for defect count and severity
3. Grading rules:
   - Grade A: 0-1 stops, no major defects
   - Grade B: 2-3 stops, minor defects only
   - Grade C: 4+ stops or major defects
4. This roll: 3 stops, all minor → Grade B
5. Update roll record with grade

**Scenario 3: Customer Quality Documentation**
1. Customer requires defect map for each roll
2. System generates report using stop data
3. Document shows:
   - Roll ID: Doff #5
   - Total length: 450m
   - Defects: 3 locations mapped
   - Position 120m: Weft break (repaired)
   - Position 235m: Warp break (repaired)
   - Position 340m: Weft break (repaired)
4. Attached to roll for customer review

**Scenario 4: Reject Analysis**
1. Customer rejects roll due to defects
2. Retrieve stop history to investigate
3. Find 5 stops between 200-250m → Abnormal clustering
4. Root cause: Yarn quality issue in specific pallet
5. Trace back to yarn lot
6. Quality claim to yarn supplier

**Scenario 5: Operator Performance**
1. Evaluate operator efficiency on doff
2. Check stop frequency and response time
3. Calculate:
   - Stops per 100 meters
   - Average stop duration
   - Defect types (recurring issues?)
4. Use for training and performance review

**Comparison with Related Procedures**:
- **WEAV_GETMCSTOPBYLOT**: All stops for entire weaving lot (multiple doffs)
- **WEAV_GETMCSTOPLISTBYDOFFNO**: Stops for ONE specific doff (this procedure)
- More granular = better for per-roll quality control

---

## Related Procedures

**Machine Stop Tracking**:
- [WEAV_GETMCSTOPBYLOT](./WEAV_GETMCSTOPBYLOT.md) - All stops for lot (broader view)
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Record machine stop
- [WEAV_DELETEMCSTOP](./WEAV_DELETEMCSTOP.md) - Delete stop record

**Doff Management**:
- Doff recording procedures
- Doff inspection procedures
- Roll grading procedures

**Quality Control**:
- Inspection data recording
- Defect marking procedures
- Roll acceptance/rejection workflows

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETMCSTOPLISTBYDOFFNO(string P_LOOMNO, decimal? P_DOFFNO, string P_BEAMROLL, string P_WEAVELOT)`
**Lines**: 1160-1210

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETMCSTOPLISTBYDOFFNO(WEAV_GETMCSTOPLISTBYDOFFNOParameter para)`
**Lines**: 13334-13389

**Stored Procedure Call**:
```csharp
// All 4 parameters required - composite key
string[] paraNames = new string[]
{
    "P_LOOMNO",     // Machine number
    "P_DOFFNO",     // Doff/roll number
    "P_BEAMROLL",   // Source beam
    "P_WEAVELOT"    // Lot number
};

object[] paraValues = new object[]
{
    para.P_LOOMNO,
    para.P_DOFFNO,
    para.P_BEAMROLL,
    para.P_WEAVELOT
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_GETMCSTOPLISTBYDOFFNO",
    paraNames, paraValues);
```

**Usage Example 1: Load Doff Stop History**
```csharp
// After completing doff, load stop history for inspection
private void LoadDoffStops()
{
    string loomNo = "L-001";
    decimal doffNo = 5;
    string beamRoll = "BM-2024-100";
    string weaveLot = "WV-2024-001";

    WeavingDataService service = WeavingDataService.Instance;
    var stops = service.WEAV_GETMCSTOPLISTBYDOFFNO(
        loomNo, doffNo, beamRoll, weaveLot);

    if (stops == null || stops.Count == 0)
    {
        lblQuality.Text = "No stops - Clean production";
        lblQuality.ForeColor = Color.Green;
        return;
    }

    // Display stop summary
    dgStops.DataSource = stops;
    lblStopCount.Text = $"Stops: {stops.Count}";

    // Calculate quality metrics
    int majorDefects = stops.Count(s => IsMajorDefect(s.DEFECTCODE));
    int minorDefects = stops.Count - majorDefects;

    lblQuality.Text = $"Major: {majorDefects} | Minor: {minorDefects}";
    lblQuality.ForeColor = majorDefects > 0 ? Color.Red : Color.Orange;
}
```

**Usage Example 2: Generate Defect Map**
```csharp
// Create visual defect map for roll
private void GenerateDefectMap(string loomNo, decimal doffNo,
                               string beamRoll, string weaveLot)
{
    var stops = service.WEAV_GETMCSTOPLISTBYDOFFNO(
        loomNo, doffNo, beamRoll, weaveLot);

    // Get roll length (from first stop record or production data)
    decimal rollLength = 450m; // Example

    // Create defect map visualization
    var map = new StringBuilder();
    map.AppendLine($"DEFECT MAP - Doff #{doffNo}");
    map.AppendLine(new string('=', 70));
    map.AppendLine($"Roll Length: {rollLength} meters");
    map.AppendLine($"Total Defects: {stops.Count}");
    map.AppendLine(new string('=', 70));
    map.AppendLine();

    // Visual representation
    map.AppendLine("Position Map:");
    map.AppendLine("0m" + new string('-', 60) + $"{rollLength}m");

    foreach (var stop in stops.OrderBy(s => s.DEFECTPOSITION))
    {
        if (!stop.DEFECTPOSITION.HasValue) continue;

        decimal position = stop.DEFECTPOSITION.Value;
        int charPos = (int)((position / rollLength) * 60);

        map.AppendLine(new string(' ', charPos) + "X");
        map.AppendLine($"  ^-- {position}m: {stop.DEFECTCODE} ({stop.DESCRIPTION})");
    }

    txtDefectMap.Text = map.ToString();
}
```

**Usage Example 3: Auto Grade Roll**
```csharp
// Automatically grade roll based on stop count and severity
private string GradeRoll(string loomNo, decimal doffNo,
                        string beamRoll, string weaveLot)
{
    var stops = service.WEAV_GETMCSTOPLISTBYDOFFNO(
        loomNo, doffNo, beamRoll, weaveLot);

    // Grading logic
    if (stops == null || stops.Count == 0)
    {
        return "A"; // Perfect - no stops
    }

    int majorDefects = stops.Count(s => IsMajorDefect(s.DEFECTCODE));
    int totalStops = stops.Count;

    // Major defect codes (examples)
    bool IsMajorDefect(string code) =>
        code == "D015" || // Major hole
        code == "D020" || // Contamination
        code == "D025";   // Wrong yarn

    // Grading criteria
    if (majorDefects > 0)
        return "C"; // Any major defect = Grade C

    if (totalStops >= 5)
        return "C"; // Too many stops = Grade C

    if (totalStops >= 2)
        return "B"; // 2-4 minor stops = Grade B

    return "A"; // 0-1 minor stops = Grade A
}

// Usage
string grade = GradeRoll("L-001", 5, "BM-2024-100", "WV-2024-001");
lblGrade.Text = $"Roll Grade: {grade}";
lblGrade.BackColor = grade == "A" ? Color.Green :
                     grade == "B" ? Color.Yellow :
                     Color.Red;
```

**Usage Example 4: Export Customer Report**
```csharp
// Generate customer quality report for specific doff
private void ExportCustomerReport(string loomNo, decimal doffNo,
                                  string beamRoll, string weaveLot)
{
    var stops = service.WEAV_GETMCSTOPLISTBYDOFFNO(
        loomNo, doffNo, beamRoll, weaveLot);

    string filename = $"QualityReport_Doff{doffNo}_{DateTime.Now:yyyyMMdd}.txt";

    using (StreamWriter writer = new StreamWriter(filename))
    {
        writer.WriteLine("FABRIC ROLL QUALITY REPORT");
        writer.WriteLine(new string('=', 80));
        writer.WriteLine($"Roll Identification:");
        writer.WriteLine($"  Weaving Lot: {weaveLot}");
        writer.WriteLine($"  Doff Number: {doffNo}");
        writer.WriteLine($"  Loom: {loomNo}");
        writer.WriteLine($"  Beam: {beamRoll}");
        writer.WriteLine($"  Report Date: {DateTime.Now:yyyy-MM-dd HH:mm}");
        writer.WriteLine(new string('=', 80));
        writer.WriteLine();

        if (stops == null || stops.Count == 0)
        {
            writer.WriteLine("QUALITY STATUS: EXCELLENT");
            writer.WriteLine("No defects or stops recorded during production.");
        }
        else
        {
            writer.WriteLine($"DEFECT SUMMARY: {stops.Count} issues recorded");
            writer.WriteLine();
            writer.WriteLine($"{"Position",-12} {"Defect",-12} {"Description",-30} {"Recorded"}");
            writer.WriteLine(new string('-', 80));

            foreach (var stop in stops.OrderBy(s => s.DEFECTPOSITION))
            {
                writer.WriteLine(
                    $"{stop.DEFECTPOSITION + "m",-12} " +
                    $"{stop.DEFECTCODE,-12} " +
                    $"{stop.DESCRIPTION,-30} " +
                    $"{stop.STOPDATE:yyyy-MM-dd HH:mm}");

                if (!string.IsNullOrEmpty(stop.REMARK))
                {
                    writer.WriteLine($"  Note: {stop.REMARK}");
                }
            }
        }

        writer.WriteLine();
        writer.WriteLine(new string('=', 80));
        writer.WriteLine("All defects have been repaired and verified.");
        writer.WriteLine("Roll is suitable for customer delivery.");
    }

    MessageBox.Show($"Customer report generated: {filename}");
}
```

---

**File**: 072/296 | **Progress**: 24.3%
