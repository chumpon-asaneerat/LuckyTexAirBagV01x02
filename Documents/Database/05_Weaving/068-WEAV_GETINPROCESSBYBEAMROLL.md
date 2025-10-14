# WEAV_GETINPROCESSBYBEAMROLL

**Procedure Number**: 068 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve in-process weaving lot details by beam roll and loom number |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:999 → WEAV_GETINPROCESSBYBEAMROLL() |
| **Frequency** | High - Called when loading/resuming production on loom |
| **Performance** | Fast - Single record lookup by composite key |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll number currently on the loom |
| `P_LOOM` | VARCHAR2(50) | ✅ | Loom machine number |

### Output (OUT)

None

### Returns (Cursor - Single Record Expected)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number (unique identifier) |
| `ITM_WEAVING` | VARCHAR2 | Item/product code being woven |
| `LENGTH` | NUMBER | Current fabric length produced (meters) |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `WEAVINGDATE` | DATE | Production date |
| `SHIFT` | VARCHAR2 | Production shift |
| `REMARK` | VARCHAR2 | Production remarks/notes |
| `CREATEDATE` | DATE | Record creation timestamp |
| `WIDTH` | NUMBER | Fabric width (cm) |
| `PREPAREBY` | VARCHAR2 | Setup operator name/code |
| `WEAVINGNO` | VARCHAR2 | Sequential weaving number |
| `BEAMLOT` | VARCHAR2 | Beam lot number (source beam) |
| `DOFFNO` | NUMBER | Doff number (fabric roll sequence) |
| `TENSION` | NUMBER | Warp tension setting |
| `STARTDATE` | DATE | Production start date/time |
| `DOFFBY` | VARCHAR2 | Operator who performed doffing |
| `SPEED` | NUMBER | Loom speed (picks per minute) |
| `WASTE` | NUMBER | Waste percentage |
| `DENSITY_WARP` | NUMBER | Warp density (ends/cm) |
| `DENSITY_WEFT` | NUMBER | Weft density (picks/cm) |
| `DELETEFLAG` | VARCHAR2 | Soft delete indicator |
| `DELETEBY` | VARCHAR2 | User who deleted record |
| `DELETEDATE` | DATE | Deletion timestamp |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves complete details of the weaving lot currently in production on a specific loom with a specific beam roll, enabling operators to resume production or view current status.

**Business Context**:
- Looms run continuously, often across multiple shifts
- When operator arrives at a loom, they need to see what's currently being produced
- Each loom can only process one beam roll at a time
- Beam roll + Loom combination uniquely identifies the current production lot
- All production parameters must be visible for quality control and adjustment
- Supports "resume production" scenario after machine stops or shift changes

**Usage Scenarios**:

**Scenario 1: Shift Handover**
1. New shift operator scans/selects loom number "L-001"
2. Scans beam roll barcode "BM-2024-001"
3. System calls WEAV_GETINPROCESSBYBEAMROLL(BM-2024-001, L-001)
4. Returns current production details:
   - Weaving lot: WV-2024-100
   - Item: FAB-AB-6070
   - Current length: 150 meters
   - Target: 500 meters
   - Speed: 250 PPM, Tension: 8.5
5. Operator reviews parameters and resumes production

**Scenario 2: Machine Stop Recovery**
1. Loom stops due to weft yarn break
2. Operator fixes the issue
3. Before restarting, scans beam to verify setup
4. System loads current lot details
5. Operator confirms all parameters correct before restart
6. Production continues from last recorded length

**Scenario 3: Production Status Check**
1. Supervisor walks the floor checking loom status
2. Selects loom on mobile device
3. System shows current beam and lot details
4. Can see progress: 300/500 meters complete (60%)
5. Reviews speed and quality parameters
6. No need to disturb operator

**Scenario 4: Quality Issue Investigation**
1. Quality inspector finds defect on fabric roll
2. Enters defect location: Lot WV-2024-100, position 250m
3. Needs to know production parameters at that time
4. Uses beam roll and loom to retrieve settings
5. Identifies speed was too high → root cause found

**Business Rules**:
- Beam roll + Loom combination should be unique for in-process lots
- Returns only active production (STATUS = 'INPROCESS')
- Excludes soft-deleted records (DELETEFLAG = 'Y')
- Should return exactly 1 record (or 0 if no production)
- If multiple records found → data integrity issue

**Key Production Parameters**:
- **LENGTH**: Current fabric produced (continuously updated)
- **SPEED**: Loom speed in picks per minute (adjustable)
- **TENSION**: Warp tension (critical for quality)
- **DENSITY_WARP/WEFT**: Fabric construction (must match spec)
- **WASTE**: Scrap percentage (monitored for efficiency)
- **WIDTH**: Fabric width (must match target)

**Traceability Data**:
- **BEAMLOT**: Links to upstream beaming operation
- **WEAVINGLOT**: Current lot identifier
- **ITM_WEAVING**: Product specification
- **DOFFNO**: Sequential roll number within lot

---

## Related Procedures

**Production Monitoring**:
- [WEAV_WEAVINGINPROCESSLIST](./WEAV_WEAVINGINPROCESSLIST.md) - List all in-process lots
- [WEAV_WEAVINGMCSTATUS](./WEAV_WEAVINGMCSTATUS.md) - Get loom machine status
- [WEAV_GETWEAVELISTBYBEAMROLL](./WEAV_GETWEAVELISTBYBEAMROLL.md) - History of weaving by beam

**Production Updates**:
- [WEAV_UPDATEWEAVINGLOT](./WEAV_UPDATEWEAVINGLOT.md) - Update production parameters
- [WEAV_WEAVINGPROCESS](./WEAV_WEAVINGPROCESS.md) - Record production progress

**Machine Stop Tracking**:
- [WEAV_GETMCSTOPBYLOT](./WEAV_GETMCSTOPBYLOT.md) - Get machine stops for this lot
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Record machine stop event

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETINPROCESSBYBEAMROLL()`
**Lines**: 999-1047

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETINPROCESSBYBEAMROLL(WEAV_GETINPROCESSBYBEAMROLLParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 068/296 | **Progress**: 23.0%
