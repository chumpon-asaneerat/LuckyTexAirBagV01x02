# DRAW_DAILYREPORT

**Procedure Number**: 047 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get daily drawing production report by date |
| **Operation** | SELECT |
| **Tables** | tblDrawing, tblBeamingDetail |
| **Called From** | DrawingDataService.cs:305 → DRAW_DAILYREPORT() |
| **Frequency** | Low |
| **Performance** | Medium (date range query) |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DATE` | VARCHAR2(20) | ✅ | Date filter (date string format) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMLOT` | VARCHAR2 | Beam roll lot number (barcode) |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type ID |
| `DRAWINGTYPE` | VARCHAR2 | Drawing type (manual/auto) |
| `STARTDATE` | DATE | Drawing start timestamp |
| `ENDATE` | DATE | Drawing end timestamp |
| `REEDNO` | VARCHAR2 | Reed number used |
| `HEALDCOLOR` | VARCHAR2 | Heald (heddle) color |
| `STARTBY` | VARCHAR2 | Operator who started |
| `FINISHBY` | VARCHAR2 | Operator who finished |
| `USEFLAG` | VARCHAR2 | Usage flag (0/1) |
| `HEALDNO` | NUMBER | Heald (heddle) number |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/team |
| `TOTALYARN` | NUMBER | Total yarn ends count |
| `BEAMNO` | VARCHAR2 | Physical beam number |
| `LENGTH` | NUMBER | Beam length (meters) |
| `BEAMERNO` | VARCHAR2 | Beamer setup number |

**Note**: Model adds `No` field (row number) in C# code

---

## Database Operations

### Tables

**Primary Tables**:
- `tblDrawing` - SELECT - Drawing production records
- `tblBeamingDetail` - SELECT (JOIN) - Beam data (BEAMNO, LENGTH, BEAMERNO)

**Transaction**: No (Read-only report query)

### Indexes (if relevant)

```sql
-- Recommended index for date range queries
CREATE INDEX idx_drawing_startdate ON tblDrawing(STARTDATE);
CREATE INDEX idx_drawing_endate ON tblDrawing(ENDATE);
```

---

## Business Logic (What it does and why)

Generates daily production report showing all drawing operations completed on a specific date for management review and performance tracking.

**Purpose**: Daily production summary report for drawing department showing:
- Which beams were drawn
- Drawing parameters (reed, heald, type)
- Production times and operators
- Product specifications
- Beam details (length, setup)

**When Used**:
- **Daily Reports**: End-of-day production summary
- **Management Review**: Daily performance tracking
- **Production Planning**: Review daily output
- **Operator Performance**: Track who worked on what

**Business Rules**:
- Date filter required (P_DATE parameter)
- Returns all drawing records for the specified date
- Joins with beam data to show beam details
- Results ordered (likely by STARTDATE)
- Row numbers added in C# (No field = sequence 1, 2, 3...)
- Empty result means no drawing operations on that date

**Report Fields** (typical usage):
- **Identification**: No (sequence), BEAMLOT, BEAMNO
- **Product**: ITM_PREPARE, PRODUCTTYPEID
- **Process**: DRAWINGTYPE, REEDNO, HEALDCOLOR, HEALDNO
- **Time**: STARTDATE, ENDATE (duration calculated)
- **People**: STARTBY, FINISHBY, OPERATOR_GROUP
- **Beam Info**: LENGTH, BEAMERNO, TOTALYARN

---

## Related Procedures

**Upstream**:
- [DRAW_INSERTDRAWING.md](./DRAW_INSERTDRAWING.md) - Creates records that appear in this report
- [DRAW_UPDATEDRAWING.md](./DRAW_UPDATEDRAWING.md) - Updates records that appear in this report

**Downstream**:
- Report viewers (RDLC) - Generate printed daily report

**Similar**:
- WARP_DAILYREPORT - Warping daily report (if exists)
- BEAM_DAILYREPORT - Beaming daily report (if exists)
- WEAV_GREYROLLDAILYREPORT - Weaving daily report

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `DrawingDataService.cs`
**Method**: `DRAW_DAILYREPORT()`
**Line**: 299-361

**Query Type**: SELECT via DatabaseManager wrapper

```csharp
// Method signature
public List<DRAW_DAILYREPORT> DRAW_DAILYREPORT(string P_DATE)
{
    List<DRAW_DAILYREPORT> results = null;

    if (!HasConnection())
        return results;

    // Parameter setup
    DRAW_DAILYREPORTParameter dbPara = new DRAW_DAILYREPORTParameter();
    dbPara.P_DATE = P_DATE; // Date string

    // Execute query
    dbResults = DatabaseManager.Instance.DRAW_DAILYREPORT(dbPara);

    // Add row numbers in code (not from database)
    int i = 0;
    foreach (DRAW_DAILYREPORTResult dbResult in dbResults)
    {
        inst.No = (i + 1); // Sequence number: 1, 2, 3...

        // Map 17 fields from database
        inst.BEAMLOT = dbResult.BEAMLOT;
        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
        // ... (17 total fields)

        i++;
    }

    return results;
}
```

**Usage Pattern**:
```csharp
// In Daily Report Screen (DrawingDailyReportPage.xaml.cs)
private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
{
    string reportDate = dtpDate.Text; // Selected date

    var reportData = DrawingDataService.Instance.DRAW_DAILYREPORT(reportDate);

    if (reportData != null && reportData.Count > 0)
    {
        // Display in grid
        dgReport.ItemsSource = reportData;
        lblTotalRecords.Text = $"Total: {reportData.Count} beams drawn";

        // Or generate PDF report (RDLC)
        var report = new DrawingDailyReport();
        report.SetDataSource(reportData);
        reportViewer.Report = report;
    }
    else
    {
        MessageBox.Show("No drawing operations on this date");
    }
}
```

---

**File**: 47/296 | **Progress**: 15.9%
