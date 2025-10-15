# INS_GETCUTSAMPLELIST

**Procedure Number**: 122 | **Module**: M08 - Inspection | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve list of samples cut from inspection lot |
| **Operation** | SELECT |
| **Tables** | tblInspectionCutSample (inferred) |
| **Called From** | DataServicecs.cs:646 → GetINS_GETCUTSAMPLELIST() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INS_LOT` | VARCHAR2 | ✅ | Inspection lot number to query |
| `P_STARTDATE` | DATE | ⬜ | Inspection start date (additional filter) |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `INSPECTIONLOT` | VARCHAR2 | Inspection lot number |
| `STARTDATE` | DATE | Inspection start date/time |
| `ORDERNO` | NUMBER | Sample order number (sequence) |
| `CUTLENGTH` | NUMBER | Length of sample cut (meters) |
| `REMARK` | VARCHAR2 | Sample remarks/notes |
| `CUTDATE` | DATE | Date/time sample was cut |
| `CUTBY` | VARCHAR2 | Operator who cut sample |

---

## Business Logic

**Purpose**: Retrieve all samples that have been cut from a specific inspection lot. Used for displaying sample history, calculating remaining fabric length, and providing traceability for laboratory testing.

**When Used**:

**Scenario 1: Inspection Summary Display**
1. Inspector viewing inspection lot INS-2025-001
2. System calls INS_GETCUTSAMPLELIST(P_INS_LOT='INS-2025-001')
3. Returns sample history:
   ```
   Order | Cut Date      | Length | Cut By        | Remark
   ------+---------------+--------+---------------+-------------------
   1     | 2025-01-15 10:30 | 2.0   | INSPECTOR-01  | Standard lab sample
   2     | 2025-01-15 14:15 | 1.0   | INSPECTOR-01  | Defect investigation
   3     | 2025-01-15 16:45 | 2.0   | INSPECTOR-05  | Customer witness

   Total samples: 3
   Total cut length: 5.0 meters
   ```
4. Inspector sees complete sample history for lot

**Scenario 2: Net Length Calculation**
1. Inspection of 100-meter roll complete
2. Need to calculate net inspected length for invoicing
3. System calls INS_GETCUTSAMPLELIST to get all samples:
   - Sample 1: 2.0 meters
   - Sample 2: 1.0 meter
   - Sample 3: 2.0 meters
   - **Total cut**: 5.0 meters
4. Calculate net length:
   - Gross length: 100.0 meters
   - Less samples: -5.0 meters
   - Less defects: -2.5 meters (from INS_GETDEFECTLIST)
   - **Net length**: 92.5 meters (for customer invoice)
5. Used by INS_GETNETLENGTH (123) procedure

**Scenario 3: Lab Sample Traceability Report**
1. Lab technician looking for samples from inspection lot
2. Lab system queries INS_GETCUTSAMPLELIST
3. Returns all samples with cut dates and operators
4. Lab matches physical samples to records:
   - Sample 1 (ORDERNO=1): Standard tensile testing
   - Sample 2 (ORDERNO=2): Microscopic defect analysis
   - Sample 3 (ORDERNO=3): Customer verification sample
5. Lab results linked back via inspection lot + order number

**Scenario 4: Quality Audit**
1. Quality auditor reviewing sampling compliance
2. Calls INS_GETCUTSAMPLELIST for inspection lots in date range
3. Verifies sampling frequency:
   - 100-meter roll: 2 samples (compliant)
   - 200-meter roll: 4 samples (compliant)
   - 50-meter roll: 1 sample (compliant)
4. Checks operator accountability (all samples have CUTBY)
5. Reviews sample remarks for special cases

**Scenario 5: Customer Complaint Investigation**
1. Customer reports issue with shipped fabric
2. QC traces back to inspection lot INS-2024-500
3. Calls INS_GETCUTSAMPLELIST to check if samples retained
4. Finds:
   - Customer witness sample (ORDERNO=3) was provided to customer
   - Lab samples (ORDERNO=1,2) still retained
5. Lab samples re-tested for comparison with customer findings
6. Sample records provide evidence for resolution

**Business Rules**:
- **Inspection lot required**: Must specify lot number
- **Optional date filter**: P_STARTDATE can further filter results
- **Chronological order**: Samples returned in ORDERNO sequence
- **Complete history**: Shows all samples ever cut from lot
- **Order numbering**: ORDERNO sequences samples (1, 2, 3, ...)
- **Length tracking**: CUTLENGTH critical for net length calculation
- **Operator accountability**: CUTBY tracked for all samples
- **Timestamp precision**: CUTDATE records exact cut time

**Data Usage Patterns**:
- **Sample count**: COUNT(*) = number of samples
- **Total cut length**: SUM(CUTLENGTH) = total meters cut
- **Sample frequency**: Verify compliance with quality protocols
- **Operator workload**: Track samples by CUTBY
- **Traceability**: Link inspection lot → samples → lab tests

**Integration Points**:
- **Net length calculation**: INS_GETNETLENGTH uses this data
- **Lab traceability**: Lab system links samples via inspection lot
- **Quality reports**: Sample summary in inspection reports
- **Material accountability**: Cut length affects inventory
- **Customer invoicing**: Net length (after samples) used for billing

---

## Related Procedures

**Upstream**:
- [121-INS_CUTSAMPLE.md](./121-INS_CUTSAMPLE.md) - Creates sample records

**Downstream**:
- [123-INS_GETNETLENGTH.md](./123-INS_GETNETLENGTH.md) - Uses sample data for net calculation
- INS_GETINSPECTIONREPORTDATA - Inspection report includes samples
- LAB_GETSAMPLEDATABYMETHOD (Lab System) - Lab sample lookup

**Similar Procedures**:
- [113-FINISHING_GETSAMPLINGSHEET.md](../06_Finishing/113-FINISHING_GETSAMPLINGSHEET.md) - Finishing sample retrieval
- WEAV_GETSAMPLINGDATA (M05) - Weaving sample retrieval

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `GetINS_GETCUTSAMPLELIST(string INS_LOT, DateTime? STARTDATE)`
**Lines**: 646-696

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `INS_GETCUTSAMPLELISTParameter`
**Lines**: 5767-5771

**Result Class**: `INS_GETCUTSAMPLELISTResult`
**Lines**: 5777-5786

**Method**: `INS_GETCUTSAMPLELIST(INS_GETCUTSAMPLELISTParameter para)`
**Lines**: 23124-23180 (estimated)

---

**File**: 122/296 | **Progress**: 41.2%
