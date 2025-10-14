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
**Method**: `WEAV_GETSAMPLINGDATA()`
**Lines**: 1100-1154

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETSAMPLINGDATA(WEAV_GETSAMPLINGDATAParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 073/296 | **Progress**: 24.7%
