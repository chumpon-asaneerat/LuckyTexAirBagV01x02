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
**Method**: `WEAV_GETMCSTOPBYLOT()`
**Lines**: 934-989

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETMCSTOPBYLOT(WEAV_GETMCSTOPBYLOTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 071/296 | **Progress**: 24.0%
