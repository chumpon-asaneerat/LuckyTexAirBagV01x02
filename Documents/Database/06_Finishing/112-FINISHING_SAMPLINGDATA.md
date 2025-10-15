# FINISHING_SAMPLINGDATA

**Procedure Number**: 112 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create sampling record for quality control testing after finishing process |
| **Operation** | INSERT |
| **Tables** | tblFinishingSampling (inferred) |
| **Called From** | CoatingDataService.cs:3365 → FINISHING_SAMPLINGDATA() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2 | ✅ | Weaving lot number |
| `P_FINISHLOT` | VARCHAR2 | ✅ | Finishing lot number |
| `P_ITMCODE` | VARCHAR2 | ✅ | Item code |
| `P_FINISHCUSTOMER` | VARCHAR2 | ⬜ | Customer ID for finishing |
| `P_PRODUCTTYPEID` | VARCHAR2 | ⬜ | Product type identifier |
| `P_OPERATORID` | VARCHAR2 | ⬜ | Operator who created sample |
| `P_WIDTH` | NUMBER | ⬜ | Sample width (cm) |
| `P_LENGTH` | NUMBER | ⬜ | Sample length (meters) |
| `P_REMARK` | VARCHAR2 | ⬜ | Additional remarks/notes |

### Output (OUT)

None (returns empty result object for success/failure indication)

---

## Business Logic

**Purpose**: Create a sampling record when a quality control sample is cut from finished fabric for laboratory testing.

**When Used**: After finishing operations (coating/scouring/dryer) are complete, quality control personnel cut samples from the fabric for laboratory testing. This procedure records:
- Which fabric lot the sample came from
- Sample dimensions (width and length)
- Who created the sample and when
- Customer and product type for test specification reference

**Workflow**:

**Scenario: Standard QC Sampling After Dryer**
1. Dryer operator completes dryer process for finishing lot FN2025-001
2. Fabric moves to quality control area
3. QC technician cuts sample from fabric roll:
   - Width: 10 cm
   - Length: 2 meters
4. Technician scans/enters finishing lot and weaving lot barcodes
5. System calls FINISHING_SAMPLINGDATA:
   - `P_WEAVLOT` = WL2025-456
   - `P_FINISHLOT` = FN2025-001
   - `P_ITMCODE` = AB-45-PA66
   - `P_WIDTH` = 10
   - `P_LENGTH` = 2
   - `P_OPERATORID` = QC-001
6. Sample record created with timestamp
7. Sample sent to lab with reference number
8. Lab uses FINISHING_GETSAMPLINGSHEET to retrieve sample info for testing

**Scenario: Customer-Specific Retest Sample**
1. Lab test fails initial inspection
2. Supervisor authorizes retest with new sample
3. QC technician cuts fresh sample from same fabric
4. System calls FINISHING_SAMPLINGDATA with RETEST flag
5. Links to customer specifications for correct test parameters
6. Lab performs retest using new sample

**Business Rules**:
- Required fields: Weaving lot, finishing lot, item code
- Each sample creates unique record with auto-generated ID
- Sample dimensions recorded for lab reference (test area calculation)
- Operator tracking for accountability
- Links to product type for test specification lookup
- Customer information enables customer-specific test requirements
- Remarks field for special instructions (e.g., "Retest", "Customer witness required")
- Sample record enables traceability: Lab result → Sample → Finishing lot → Weaving lot

**Integration Points**:
- **Upstream**: Finishing operations (coating/scouring/dryer) create the lots
- **Downstream**:
  - FINISHING_GETSAMPLINGSHEET retrieves sample records
  - Laboratory system uses sample data for test entry
  - Inspection system links test results back to samples
  - Quality reports reference sample IDs

**Data Purpose**:
- **Quality Control**: Track which samples were cut for testing
- **Traceability**: Link lab test results back to production lots
- **Compliance**: Document sample creation for customer audits
- **Planning**: Monitor sampling frequency and lab workload

---

## Related Procedures

**Upstream**:
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates finishing lot
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Creates coating lot
- [099-FINISHING_INSERTSCOURING.md](./099-FINISHING_INSERTSCOURING.md) - Creates scouring lot

**Downstream**:
- [113-FINISHING_GETSAMPLINGSHEET.md](./113-FINISHING_GETSAMPLINGSHEET.md) - Retrieves sample records
- LAB_INSERTSAMPLEDATA (M14) - Lab system sample entry
- INS_CUTSAMPLE (M08) - Inspection sample cutting

**Similar**:
- WEAV_SAMPLING (M05) - Weaving sampling (similar pattern)
- INS_CUTSAMPLE (M08) - Inspection sampling

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_SAMPLINGDATA(...)`
**Lines**: 3364-3408

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_SAMPLINGDATAParameter`
**Lines**: 7833-7844

**Result Class**: `FINISHING_SAMPLINGDATAResult`
**Lines**: 7850-7852

**Method**: `FINISHING_SAMPLINGDATA(FINISHING_SAMPLINGDATAParameter para)`
**Lines**: 26845-26885

---

**File**: 112/296 | **Progress**: 37.8%
