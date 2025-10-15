# FINISHING_SEARCHFINISHDATA

**Procedure Number**: 118 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search finished production records by date and process type with sampling data |
| **Operation** | SELECT |
| **Tables** | tblFinishingCoating, tblFinishingScouring, tblFinishingDryer, tblFinishingSampling (inferred - JOIN query) |
| **Called From** | DataServicecs.cs:2853 → Finishing_SearchDataList() |
| **Frequency** | Medium |
| **Performance** | Medium |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DATE` | VARCHAR2 | ⬜ | Production date to search (format: YYYYMMDD) |
| `P_PROCESS` | VARCHAR2 | ⬜ | Process type filter (COATING/SCOURING/DRYER) |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `FINISHINGLOT` | VARCHAR2 | Finishing lot number |
| `ITEMCODE` | VARCHAR2 | Item code |
| `TOTALLENGTH` | NUMBER | Total finished length (meters) |
| `PROCESS` | VARCHAR2 | Process type (COATING/SCOURING/DRYER) |
| `FINISHINGDATE` | DATE | Completion date |
| `FINISHBY` | VARCHAR2 | Operator who finished |
| `FINISHINGCUSTOMER` | VARCHAR2 | Customer ID |
| `MC` | VARCHAR2 | Machine name |
| `PRODUCTTYPEID` | VARCHAR2 | Product type identifier |
| `LENGTH1` | NUMBER | Length measurement 1 |
| `LENGTH2` | NUMBER | Length measurement 2 |
| `LENGTH3` | NUMBER | Length measurement 3 |
| `LENGTH4` | NUMBER | Length measurement 4 |
| `LENGTH5` | NUMBER | Length measurement 5 |
| `LENGTH6` | NUMBER | Length measurement 6 |
| `LENGTH7` | NUMBER | Length measurement 7 |
| `SAMPLING_WIDTH` | NUMBER | Sample width (cm) - from sampling record |
| `SAMPLING_LENGTH` | NUMBER | Sample length (meters) - from sampling record |
| `REMARK` | VARCHAR2 | Remarks/notes |

---

## Business Logic

**Purpose**: Search for completed finishing operations with integrated sampling data. This procedure differs from FINISHING_SEARCHFINISHRECORD by including QC sampling information and filtering by specific process type.

**Key Difference from FINISHING_SEARCHFINISHRECORD (117)**:

| Feature | SEARCHFINISHRECORD (117) | SEARCHFINISHDATA (118) |
|---------|-------------------------|------------------------|
| **Filters** | Date, Machine, Item Code | Date, Process Type |
| **Sampling Data** | ❌ No | ✅ Yes (SAMPLING_WIDTH, SAMPLING_LENGTH, REMARK) |
| **Process Filter** | Returns all processes | Returns specific process only |
| **Use Case** | General production search | QC/sampling-focused search |
| **Columns** | More operator/machine details | More sampling/QC details |

**When Used**:

**Scenario 1: Quality Control Daily Report**
1. QC manager generates daily sampling report for dryer operations
2. Calls FINISHING_SEARCHFINISHDATA:
   - `P_DATE` = "20250115"
   - `P_PROCESS` = "DRYER"
3. Returns only dryer operations with sampling data
4. Report shows:
   - Which lots were sampled
   - Sample dimensions (width, length)
   - Total production lengths
   - Customer-specific production
5. QC manager reviews sampling coverage (% of lots sampled)

**Scenario 2: Customer-Specific Production Review**
1. Customer TOYOTA requests production summary for January
2. Filter by customer in application layer
3. Calls FINISHING_SEARCHFINISHDATA:
   - `P_DATE` = "202501" (month range)
   - `P_PROCESS` = "COATING" (customer uses coated fabric)
4. Returns all TOYOTA coating operations with sampling data
5. Provides:
   - Total production: 1,500 meters across 15 lots
   - Sampling records for quality assurance
   - Production dates for delivery planning

**Scenario 3: Process-Specific Analysis**
1. Process engineer analyzes scouring operations
2. Calls FINISHING_SEARCHFINISHDATA:
   - `P_DATE` = "2025Q1" (quarterly analysis)
   - `P_PROCESS` = "SCOURING"
3. Returns only scouring operations (not coating or dryer)
4. Analysis includes:
   - Total scouring production by month
   - Sampling frequency for quality control
   - Length consistency across lots
   - Customer distribution

**Scenario 4: Lab Test Planning**
1. Lab supervisor plans next week's testing schedule
2. Calls FINISHING_SEARCHFINISHDATA:
   - `P_DATE` = "last 7 days"
   - `P_PROCESS` = null (all processes)
3. Returns all finished lots with sampling data
4. Lab identifies:
   - Which samples already cut (SAMPLING_WIDTH/LENGTH not null)
   - Which lots need sampling
   - Sample priorities by customer
   - Expected test workload

**Scenario 5: Sampling Audit**
1. Quality auditor reviews sampling compliance
2. Calls FINISHING_SEARCHFINISHDATA for each process
3. Checks sampling coverage:
   - COATING: 95% of lots sampled
   - SCOURING: 98% of lots sampled
   - DRYER: 100% of lots sampled (required)
4. Identifies gaps where sampling was missed
5. Investigates reasons and corrective actions

**Business Rules**:
- **Process-specific**: Filter by single process type (not multi-process like 117)
- **Sampling integration**: Includes sampling data (unlike 117)
- **Date-driven**: Primary filter is production date
- **QC focus**: Designed for quality control and sampling workflows
- **Completed operations**: Only finished lots (STATUS='FINISH')
- **Optional filters**: Both parameters optional for flexibility

**Data Usage**:
- **Quality reports**: Sampling coverage and compliance
- **Lab scheduling**: Which samples need testing
- **Customer reports**: Production summary with QC data
- **Process analysis**: Process-specific production metrics
- **Audit trails**: Sampling compliance documentation

**Integration with Sampling**:
- LEFT JOIN with tblFinishingSampling (inferred)
- Returns finishing records even without sampling data
- SAMPLING_WIDTH/LENGTH null if no sample cut yet
- Enables tracking of lots requiring sampling

---

## Related Procedures

**Comparison**:
- [117-FINISHING_SEARCHFINISHRECORD.md](./117-FINISHING_SEARCHFINISHRECORD.md) - General search (no sampling data)
  - Use 117 for: Machine analysis, operator reports, general production
  - Use 118 for: QC reports, sampling coverage, lab planning

**Upstream**:
- [112-FINISHING_SAMPLINGDATA.md](./112-FINISHING_SAMPLINGDATA.md) - Creates sampling records
- [094-FINISHING_UPDATECOATING.md](./094-FINISHING_UPDATECOATING.md) - Completes coating
- [102-FINISHING_UPDATESCOURING.md](./102-FINISHING_UPDATESCOURING.md) - Completes scouring
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - Completes dryer

**Downstream**:
- Quality control dashboards
- Lab test scheduling
- Customer production reports
- Sampling compliance audits

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `Finishing_SearchDataList(string strDATE, string PROCESS)`
**Lines**: 2847-2910

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_SEARCHFINISHDATAParameter`
**Lines**: 7628-7632

**Result Class**: `FINISHING_SEARCHFINISHDATAResult`
**Lines**: 7638-7660

**Method**: `FINISHING_SEARCHFINISHDATA(FINISHING_SEARCHFINISHDATAParameter para)`
**Lines**: 26600-26750 (estimated)

---

**File**: 118/296 | **Progress**: 39.9%
