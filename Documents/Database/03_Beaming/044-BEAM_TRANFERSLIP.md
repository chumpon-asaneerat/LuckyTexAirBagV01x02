# BEAM_TRANFERSLIP

**Procedure Number**: 044 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beam data for transfer slip document generation |
| **Operation** | SELECT |
| **Tables** | tblBeamingDetail, tblBeamingHead |
| **Called From** | BeamingDataService.cs:520 → BEAM_TRANFERSLIP() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number (barcode) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2 | Beamer setup/batch number |
| `BEAMLOT` | VARCHAR2 | Beam roll lot number (barcode) |
| `BEAMNO` | VARCHAR2 | Physical beam roll number |
| `STARTDATE` | DATE | Production start timestamp |
| `ENDDATE` | DATE | Production end timestamp |
| `LENGTH` | NUMBER | Beam length (meters) |
| `SPEED` | NUMBER | Average machine speed (m/min) |
| `BEAMSTANDTENSION` | NUMBER | Beam stand tension |
| `WINDINGTENSION` | NUMBER | Winding tension |
| `HARDNESS_L` | NUMBER | Beam hardness - left |
| `HARDNESS_N` | NUMBER | Beam hardness - center |
| `HARDNESS_R` | NUMBER | Beam hardness - right |
| `INSIDE_WIDTH` | NUMBER | Inside beam width (mm) |
| `OUTSIDE_WIDTH` | NUMBER | Outside beam width (mm) |
| `FULL_WIDTH` | NUMBER | Full beam width (mm) |
| `STARTBY` | VARCHAR2 | Operator who started |
| `DOFFBY` | VARCHAR2 | Operator who doffed |
| `BEAMMC` | VARCHAR2 | Beaming machine code |
| `FLAG` | VARCHAR2 | Production status |
| `REMARK` | VARCHAR2 | Production remarks |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code (from header) |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingDetail` - SELECT - Beam production data
- `tblBeamingHead` - SELECT (JOIN) - Setup header data

**Transaction**: No (Read-only query)

### Indexes (if relevant)

```sql
-- Recommended index for fast barcode lookup
CREATE INDEX idx_beamingdetail_beamlot ON tblBeamingDetail(BEAMLOT);
```

---

## Business Logic (What it does and why)

Retrieves complete beam production data for generating a **transfer slip document** when beam moves to next process (M04-Drawing).

**Purpose**: When beam production is complete and beam is ready to transfer to drawing process, operator scans beam barcode. This procedure retrieves all production data needed to print a transfer slip/document that:
- Identifies the beam (barcode, physical number)
- Shows production details (dates, length, parameters)
- Documents quality metrics (hardness, width, tension)
- Tracks operators responsible (start/doff)
- Links to product specification (item prepare)

**When Used**:
- **Transfer to Drawing**: Beam moving from M03-Beaming to M04-Drawing
- **Transfer Slip Printing**: Generate official transfer document
- **Quality Documentation**: Record production parameters on transfer slip
- **Traceability**: Document beam movement between processes

**Business Rules**:
- Retrieves single beam record by BEAMLOT barcode
- Joins header table to include item prepare (product info)
- Returns complete production history for the beam
- Used for both screen display and printed document
- Typically called for completed beams (FLAG='D' for Doffed)

**Document Fields** (Transfer Slip typically shows):
- **Identification**: BEAMLOT barcode, BEAMNO, BEAMERNO
- **Product**: ITM_PREPARE (item code)
- **Dimensions**: LENGTH, INSIDE_WIDTH, OUTSIDE_WIDTH, FULL_WIDTH
- **Quality**: HARDNESS_L/N/R, BEAMSTANDTENSION, WINDINGTENSION
- **Process**: SPEED, STARTDATE, ENDDATE
- **People**: STARTBY, DOFFBY, BEAMMC (machine)
- **Notes**: REMARK

---

## Related Procedures

**Upstream**:
- [040-BEAM_INSERTBEAMINGDETAIL.md](./040-BEAM_INSERTBEAMINGDETAIL.md) - Created the beam record
- [BEAM_UPDATEBEAMDETAIL.md](./BEAM_UPDATEBEAMDETAIL.md) - Completed beam production

**Downstream**:
- [DRAW_INSERTDRAWING.md](../04_Drawing/DRAW_INSERTDRAWING.md) - Next process receives the beam
- Print routines (RDLC reports) - Generate transfer slip PDF

**Similar**:
- [022-WARP_TRANFERSLIP.md](../02_Warping/022-WARP_TRANFERSLIP.md) - Warping transfer slip (M02 to M03)
- [DRAW_TRANSFERSLIP.md](../04_Drawing/DRAW_TRANSFERSLIP.md) - Drawing transfer slip (M04 to M05)

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_TRANFERSLIP()`
**Line**: 514-576

**Query Type**: SELECT via DatabaseManager wrapper

```csharp
// Method signature
public List<BEAM_TRANFERSLIP> BEAM_TRANFERSLIP(string P_BEAMLOT)
{
    List<BEAM_TRANFERSLIP> results = null;

    if (!HasConnection())
        return results;

    // Parameter setup
    BEAM_TRANFERSLIPParameter dbPara = new BEAM_TRANFERSLIPParameter();
    dbPara.P_BEAMLOT = P_BEAMLOT;

    // Execute query - joins detail + header tables
    dbResults = DatabaseManager.Instance.BEAM_TRANFERSLIP(dbPara);

    // Returns List<BEAM_TRANFERSLIP> with 21 fields
    // Includes both beam detail and setup header data
    foreach (BEAM_TRANFERSLIPResult dbResult in dbResults)
    {
        inst.BEAMERNO = dbResult.BEAMERNO;
        inst.BEAMLOT = dbResult.BEAMLOT;
        inst.BEAMNO = dbResult.BEAMNO;
        // ... (21 total fields)
        inst.ITM_PREPARE = dbResult.ITM_PREPARE; // From header table
    }

    return results;
}
```

**Usage Pattern**:
```csharp
// In Transfer Slip Screen (BeamingTransferPage.xaml.cs)
private void btnPrintTransferSlip_Click(object sender, RoutedEventArgs e)
{
    string beamLot = txtBeamLot.Text.Trim(); // Scanned barcode

    var data = BeamingDataService.Instance.BEAM_TRANFERSLIP(beamLot);

    if (data != null && data.Count > 0)
    {
        // Generate transfer slip report (RDLC)
        var reportData = data[0];
        var report = new BeamingTransferSlipReport();
        report.SetDataSource(reportData);
        report.Print(); // Print to default printer

        // Or display on screen
        reportViewer.Report = report;
        reportViewer.RefreshReport();
    }
    else
    {
        MessageBox.Show("Beam lot not found");
    }
}
```

---

**File**: 44/296 | **Progress**: 14.9%
