# FINISHING_GETSAMPLINGSHEET

**Procedure Number**: 113 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve sampling records for quality control and laboratory testing |
| **Operation** | SELECT |
| **Tables** | tblFinishingSampling (inferred) |
| **Called From** | CoatingDataService.cs:3417 → FINISHING_GETSAMPLINGSHEETList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2 | ⬜ | Weaving lot number to filter |
| `P_FINLOT` | VARCHAR2 | ⬜ | Finishing lot number to filter |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `FINISHINGLOT` | VARCHAR2 | Finishing lot number |
| `ITM_CODE` | VARCHAR2 | Item code |
| `CREATEDATE` | DATE | Sample creation date/time |
| `CREATEBY` | VARCHAR2 | Operator who created sample |
| `PRODUCTID` | VARCHAR2 | Product identifier |
| `SAMPLING_WIDTH` | NUMBER | Sample width (cm) |
| `SAMPLING_LENGTH` | NUMBER | Sample length (meters) |
| `PROCESS` | VARCHAR2 | Finishing process (COATING/SCOURING/DRYER) |
| `REMARK` | VARCHAR2 | Sample remarks/notes |
| `FABRICTYPE` | VARCHAR2 | Fabric type description |
| `RETESTFLAG` | VARCHAR2 | Retest indicator (Y/N) |
| `PRODUCTNAME` | VARCHAR2 | Product name description |

---

## Business Logic

**Purpose**: Retrieve quality control sampling records for laboratory testing, reporting, and traceability. Provides complete information about samples cut from finished fabric.

**When Used**:

**Scenario 1: Laboratory Test Entry**
1. Lab technician receives fabric sample
2. Scans sample barcode or enters finishing lot number
3. System calls FINISHING_GETSAMPLINGSHEET with finishing lot
4. Displays sample information:
   - Sample dimensions (width, length)
   - Item code for test specification lookup
   - Process type (determines which tests to perform)
   - Retest flag (if this is a repeat test)
5. Lab technician enters test results linked to sample record

**Scenario 2: Quality Report Generation**
1. QC manager generates daily sampling report
2. System calls FINISHING_GETSAMPLINGSHEET (no filters = all samples)
3. Returns all samples created today with:
   - Who created each sample
   - Sample dimensions
   - Which lots sampled
4. Report shows sampling frequency and coverage

**Scenario 3: Customer Audit Traceability**
1. Customer requests test documentation for specific shipment
2. System identifies weaving lots in shipment
3. Calls FINISHING_GETSAMPLINGSHEET with weaving lot filter
4. Returns all samples cut from those lots
5. Links to lab test results for complete traceability:
   - Shipment → Weaving Lots → Samples → Lab Tests → Results

**Scenario 4: Retest Workflow**
1. Lab test result out of specification
2. QC supervisor reviews FINISHINGLOT data
3. Checks RETESTFLAG to see if already a retest
4. If first failure, authorizes new sample cut
5. If second failure (RETESTFLAG='Y'), escalates to production manager

**Business Rules**:
- Can filter by weaving lot OR finishing lot OR both
- No filters returns all sampling records (for reports)
- Each sample record unique (can have multiple samples per lot)
- PROCESS field indicates which finishing step was sampled:
  - COATING: Sample after coating process
  - SCOURING: Sample after scouring process
  - DRYER: Sample after dryer process (most common)
- RETESTFLAG='Y' indicates retest sample (previous test failed)
- Sample dimensions used for calculating test area
- CREATEDATE/CREATEBY provide audit trail

**Data Integration**:
- **Lab System**: Uses sample records to link test results
- **Quality Reports**: Sample data for compliance documentation
- **Inspection**: Links samples to inspection results
- **Customer QC**: Provides traceability for customer audits

**Typical Sample Dimensions**:
- Width: 10-30 cm (enough for multiple tests)
- Length: 1-3 meters (depends on test requirements)
- Larger samples for comprehensive testing (tensile, tear, etc.)
- Smaller samples for quick checks

---

## Related Procedures

**Upstream**:
- [112-FINISHING_SAMPLINGDATA.md](./112-FINISHING_SAMPLINGDATA.md) - Creates sample records

**Downstream**:
- LAB_INSERTSAMPLEDATA (M14) - Lab test entry
- LAB_GETPDFDATA (Lab System) - Lab report generation
- INS_GETINSPECTIONREPORTDATA (M08) - Inspection reporting

**Similar**:
- WEAV_GETSAMPLINGDATA (M05) - Weaving sampling data
- LAB_GETSAMPLEDATABYMETHOD (Lab System) - Lab sample retrieval

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETSAMPLINGSHEETList(string WEAVINGLOT, string FINLOT)`
**Lines**: 3411-3466

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_GETSAMPLINGSHEETParameter`
**Lines**: 8309-8313

**Result Class**: `FINISHING_GETSAMPLINGSHEETResult`
**Lines**: 8319-8334

**Method**: `FINISHING_GETSAMPLINGSHEET(FINISHING_GETSAMPLINGSHEETParameter para)`
**Lines**: 27636-27700 (estimated)

---

**File**: 113/296 | **Progress**: 38.2%
