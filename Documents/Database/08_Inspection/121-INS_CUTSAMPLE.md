# INS_CUTSAMPLE

**Procedure Number**: 121 | **Module**: M08 - Inspection | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Record sample cut from inspection lot for quality testing |
| **Operation** | INSERT |
| **Tables** | tblInspectionCutSample (inferred) |
| **Called From** | DataServicecs.cs:1587 → INS_CUTSAMPLE() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2 | ✅ | Inspection lot number |
| `P_STARTDATE` | DATE | ⬜ | Sample cut date/time |
| `P_CUTLENGTH` | NUMBER | ⬜ | Length of sample cut (meters) |
| `P_REMARK` | VARCHAR2 | ⬜ | Remarks/notes about sample |
| `P_CUTBY` | VARCHAR2 | ⬜ | Operator who cut sample |

### Output (OUT)

None (returns empty result object for success/failure indication)

---

## Business Logic

**Purpose**: Record when a sample is cut from a fabric roll during inspection for quality testing purposes. This creates traceability between the inspection lot and samples sent for laboratory testing.

**When Used**:

**Scenario 1: Standard Quality Sample Cut**
1. Inspector inspecting lot INS-2025-001 (50 meters roll)
2. At 10-meter mark, inspector cuts 2-meter sample for lab testing
3. System calls INS_CUTSAMPLE:
   - `P_INSLOT` = INS-2025-001
   - `P_STARTDATE` = 2025-01-15 10:30:00
   - `P_CUTLENGTH` = 2.0 meters
   - `P_CUTBY` = INSPECTOR-01
   - `P_REMARK` = "Standard lab sample"
4. Sample tagged and sent to lab
5. Lab test results linked back via inspection lot number

**Scenario 2: Defect Investigation Sample**
1. Inspector finds unusual defect at 25-meter mark
2. Supervisor requests sample for detailed analysis
3. Inspector cuts 1-meter sample around defect area
4. System calls INS_CUTSAMPLE:
   - `P_CUTLENGTH` = 1.0 meter
   - `P_REMARK` = "Defect investigation - unusual coating pattern"
5. Sample sent to lab for microscopic analysis
6. Lab report linked to sample record for root cause analysis

**Scenario 3: Customer Witness Sample**
1. Customer representative present during inspection
2. Customer requests sample for their own lab testing
3. Inspector cuts customer-requested sample
4. System records:
   - `P_REMARK` = "Customer witness sample - TOYOTA QC"
   - `P_CUTBY` = INSPECTOR-05
5. Sample provided to customer with documentation
6. Record maintained for audit trail

**Scenario 4: Multiple Samples from Same Lot**
1. Long inspection lot (200 meters)
2. Quality protocol requires samples every 50 meters
3. Inspector cuts 4 samples during inspection:
   - Sample 1 at 50m: INS_CUTSAMPLE(INS-2025-050, 50m, 2.0m, "Sample 1/4")
   - Sample 2 at 100m: INS_CUTSAMPLE(INS-2025-050, 100m, 2.0m, "Sample 2/4")
   - Sample 3 at 150m: INS_CUTSAMPLE(INS-2025-050, 150m, 2.0m, "Sample 3/4")
   - Sample 4 at 200m: INS_CUTSAMPLE(INS-2025-050, 200m, 2.0m, "Sample 4/4")
4. All samples tracked with position information
5. Lab tests performed on each sample for consistency analysis

**Business Rules**:
- **Inspection lot required**: Must specify which lot sample came from
- **Sample tracking**: Each cut creates permanent record
- **Length recording**: CUTLENGTH tracks actual sample size
  - Important for calculating remaining fabric length
  - Affects final inspection length calculations
  - Used for material accountability
- **Operator accountability**: CUTBY tracks who cut sample
- **Multiple samples**: Same inspection lot can have multiple cut records
- **Timestamp tracking**: STARTDATE records when sample cut
- **Traceability**: Links inspection → sample → lab test results

**Sample Length Considerations**:
- **Standard samples**: 1-3 meters typical
- **Lab test samples**: Size depends on test requirements
  - Tensile testing: 0.5-1 meter
  - Tear testing: 0.5 meter
  - Comprehensive testing: 2-3 meters
- **Customer samples**: Per customer requirements
- **Defect samples**: Minimal length around defect area

**Integration with Lab System**:
1. Sample cut recorded via INS_CUTSAMPLE
2. Sample sent to lab with inspection lot number
3. Lab system uses inspection lot to link results
4. Lab procedures reference sample:
   - LAB_INSERTSAMPLEDATA (Lab System)
   - LAB_GETPDFDATA (Lab System - includes sample info)
5. Quality reports show: Inspection Lot → Samples → Lab Results

**Material Accountability**:
- Cut samples reduce inspected length:
  - Roll length: 100 meters
  - Sample 1: -2 meters
  - Sample 2: -2 meters
  - **Net inspected length**: 96 meters
- INS_GETNETLENGTH (122) calculates net length after samples
- Critical for accurate inventory and customer invoicing

---

## Related Procedures

**Upstream**:
- INSERTINSPECTIONPROCESS (FinishingDataService) - Creates inspection lot
- INS_INSERTMANUALINSPECTDATA - Starts inspection

**Downstream**:
- [122-INS_GETCUTSAMPLELIST.md](./122-INS_GETCUTSAMPLELIST.md) - Retrieve cut sample records
- [123-INS_GETNETLENGTH.md](./123-INS_GETNETLENGTH.md) - Calculate net length after samples
- LAB_INSERTSAMPLEDATA (Lab System) - Lab receives sample
- LAB_GETPDFDATA (Lab System) - Lab report includes sample data

**Similar Procedures**:
- [112-FINISHING_SAMPLINGDATA.md](../06_Finishing/112-FINISHING_SAMPLINGDATA.md) - Finishing sample cutting
- WEAV_SAMPLING (M05) - Weaving sample cutting

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `INS_CUTSAMPLE(string insLotNo, DateTime startDate, decimal CUTLENGTH, string REMARK, string CUTBY)`
**Lines**: 1587-1614

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `INS_CUTSAMPLEParameter`
**Lines**: 5916-5923

**Result Class**: `INS_CUTSAMPLEResult`
**Lines**: 5929-5931 (empty)

**Method**: `INS_CUTSAMPLE(INS_CUTSAMPLEParameter para)`
**Lines**: 23406-23440 (estimated)

---

**File**: 121/296 | **Progress**: 40.9%
