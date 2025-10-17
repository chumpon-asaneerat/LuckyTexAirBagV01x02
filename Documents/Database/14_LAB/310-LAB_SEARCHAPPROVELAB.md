# LAB_SEARCHAPPROVELAB

**Procedure Name**: `LAB_SEARCHAPPROVELAB`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Searches for lab test records pending approval or approved tests for review
**Return Type**: Result Set (Multiple Rows)

---

## Description

Retrieves comprehensive laboratory test data for approval workflow. This procedure returns complete test results including all physical, mechanical, and functional test measurements for finished fabric samples. Used primarily by quality supervisors and managers to review test data before approving for customer shipment, or to review previously approved tests for audit and quality control purposes.

The result set includes over 100 columns covering all airbag fabric test properties: dimensional measurements, weight analysis, tensile strength, tear resistance, air permeability, flammability, edge comb, stiffness, flex abrasion, and more. This is one of the most comprehensive data retrieval procedures in the LAB module.

---

## Parameters

### Input Parameters

| Parameter Name | Data Type | Required | Description |
|---------------|-----------|----------|-------------|
| P_ITMCODE | VARCHAR2 | Optional | Item code filter (finished product code) |
| P_ENTRYSTARTDATE | VARCHAR2 | Optional | Entry start date filter (format: YYYY-MM-DD) |
| P_ENTRYENDDATE | VARCHAR2 | Optional | Entry end date filter (format: YYYY-MM-DD) |
| P_LOOM | VARCHAR2 | Optional | Loom machine number filter |
| P_FINISHPROCESS | VARCHAR2 | Optional | Finishing process type filter |

**Notes**:
- All parameters are optional filters
- Typical usage: date range to find tests within a specific period
- Can combine filters (e.g., item code + date range)
- Empty/null parameters return broader results

---

## Result Set

### Output Columns (100+ total)

**Basic Information (9 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| ITM_CODE | VARCHAR2 | Item code (finished product code) |
| WEAVINGLOT | VARCHAR2 | Weaving lot number |
| FINISHINGLOT | VARCHAR2 | Finishing lot number |
| ENTRYDATE | DATE | Test entry date/time |
| ENTEYBY | VARCHAR2 | Employee ID who entered test data |
| STATUS | VARCHAR2 | Approval status (PENDING/APPROVED/REJECTED) |
| REMARK | VARCHAR2 | Test remarks or notes |
| APPROVEBY | VARCHAR2 | Employee ID who approved/rejected |
| APPROVEDATE | DATE | Approval date/time |
| CREATEDATE | DATE | Record creation date/time |

**Dimensional Measurements (15 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| WIDTH | NUMBER | Fabric width measurement |
| USABLE_WIDTH1 | NUMBER | Usable width measurement #1 |
| USABLE_WIDTH2 | NUMBER | Usable width measurement #2 |
| USABLE_WIDTH3 | NUMBER | Usable width measurement #3 |
| WIDTH_SILICONE1 | NUMBER | Silicone-coated width #1 |
| WIDTH_SILICONE2 | NUMBER | Silicone-coated width #2 |
| WIDTH_SILICONE3 | NUMBER | Silicone-coated width #3 |
| BOW1 | NUMBER | Bow distortion measurement #1 |
| BOW2 | NUMBER | Bow distortion measurement #2 |
| BOW3 | NUMBER | Bow distortion measurement #3 |
| SKEW1 | NUMBER | Skew distortion measurement #1 |
| SKEW2 | NUMBER | Skew distortion measurement #2 |
| SKEW3 | NUMBER | Skew distortion measurement #3 |
| THICKNESS1 | NUMBER | Fabric thickness #1 |
| THICKNESS2 | NUMBER | Fabric thickness #2 |
| THICKNESS3 | NUMBER | Fabric thickness #3 |

**Thread Count (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| NUMTHREADS_W1 | NUMBER | Warp thread count #1 |
| NUMTHREADS_W2 | NUMBER | Warp thread count #2 |
| NUMTHREADS_W3 | NUMBER | Warp thread count #3 |
| NUMTHREADS_F1 | NUMBER | Filling thread count #1 |
| NUMTHREADS_F2 | NUMBER | Filling thread count #2 |
| NUMTHREADS_F3 | NUMBER | Filling thread count #3 |

**Weight Measurements (18 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| TOTALWEIGHT1 | NUMBER | Total weight measurement #1 |
| TOTALWEIGHT2 | NUMBER | Total weight measurement #2 |
| TOTALWEIGHT3 | NUMBER | Total weight measurement #3 |
| TOTALWEIGHT4 | NUMBER | Total weight measurement #4 |
| TOTALWEIGHT5 | NUMBER | Total weight measurement #5 |
| TOTALWEIGHT6 | NUMBER | Total weight measurement #6 |
| UNCOATEDWEIGHT1 | NUMBER | Uncoated fabric weight #1 |
| UNCOATEDWEIGHT2 | NUMBER | Uncoated fabric weight #2 |
| UNCOATEDWEIGHT3 | NUMBER | Uncoated fabric weight #3 |
| UNCOATEDWEIGHT4 | NUMBER | Uncoated fabric weight #4 |
| UNCOATEDWEIGHT5 | NUMBER | Uncoated fabric weight #5 |
| UNCOATEDWEIGHT6 | NUMBER | Uncoated fabric weight #6 |
| COATINGWEIGHT1 | NUMBER | Coating weight #1 |
| COATINGWEIGHT2 | NUMBER | Coating weight #2 |
| COATINGWEIGHT3 | NUMBER | Coating weight #3 |
| COATINGWEIGHT4 | NUMBER | Coating weight #4 |
| COATINGWEIGHT5 | NUMBER | Coating weight #5 |
| COATINGWEIGHT6 | NUMBER | Coating weight #6 |

**Tensile Strength (12 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| MAXFORCE_W1 | NUMBER | Maximum tensile force - warp #1 |
| MAXFORCE_W2 | NUMBER | Maximum tensile force - warp #2 |
| MAXFORCE_W3 | NUMBER | Maximum tensile force - warp #3 |
| MAXFORCE_F1 | NUMBER | Maximum tensile force - filling #1 |
| MAXFORCE_F2 | NUMBER | Maximum tensile force - filling #2 |
| MAXFORCE_F3 | NUMBER | Maximum tensile force - filling #3 |
| ELONGATIONFORCE_W1 | NUMBER | Elongation at break - warp #1 |
| ELONGATIONFORCE_W2 | NUMBER | Elongation at break - warp #2 |
| ELONGATIONFORCE_W3 | NUMBER | Elongation at break - warp #3 |
| ELONGATIONFORCE_F1 | NUMBER | Elongation at break - filling #1 |
| ELONGATIONFORCE_F2 | NUMBER | Elongation at break - filling #2 |
| ELONGATIONFORCE_F3 | NUMBER | Elongation at break - filling #3 |

**Tear Resistance (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| TEAR_W1 | NUMBER | Tear strength - warp #1 |
| TEAR_W2 | NUMBER | Tear strength - warp #2 |
| TEAR_W3 | NUMBER | Tear strength - warp #3 |
| TEAR_F1 | NUMBER | Tear strength - filling #1 |
| TEAR_F2 | NUMBER | Tear strength - filling #2 |
| TEAR_F3 | NUMBER | Tear strength - filling #3 |

**Air Permeability (9 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| STATIC_AIR1 | NUMBER | Static air permeability #1 |
| STATIC_AIR2 | NUMBER | Static air permeability #2 |
| STATIC_AIR3 | NUMBER | Static air permeability #3 |
| DYNAMIC_AIR1 | NUMBER | Dynamic air permeability #1 |
| DYNAMIC_AIR2 | NUMBER | Dynamic air permeability #2 |
| DYNAMIC_AIR3 | NUMBER | Dynamic air permeability #3 |
| EXPONENT1 | NUMBER | Permeability exponent #1 |
| EXPONENT2 | NUMBER | Permeability exponent #2 |
| EXPONENT3 | NUMBER | Permeability exponent #3 |

**Stiffness (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| STIFFNESS_W1 | NUMBER | Stiffness - warp #1 |
| STIFFNESS_W2 | NUMBER | Stiffness - warp #2 |
| STIFFNESS_W3 | NUMBER | Stiffness - warp #3 |
| STIFFNESS_F1 | NUMBER | Stiffness - filling #1 |
| STIFFNESS_F2 | NUMBER | Stiffness - filling #2 |
| STIFFNESS_F3 | NUMBER | Stiffness - filling #3 |

**Bending (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| BENDING_W1 | NUMBER | Bending stiffness - warp #1 |
| BENDING_W2 | NUMBER | Bending stiffness - warp #2 |
| BENDING_W3 | NUMBER | Bending stiffness - warp #3 |
| BENDING_F1 | NUMBER | Bending stiffness - filling #1 |
| BENDING_F2 | NUMBER | Bending stiffness - filling #2 |
| BENDING_F3 | NUMBER | Bending stiffness - filling #3 |

**Edge Comb (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| EDGECOMB_W1 | NUMBER | Edge comb resistance - warp #1 |
| EDGECOMB_W2 | NUMBER | Edge comb resistance - warp #2 |
| EDGECOMB_W3 | NUMBER | Edge comb resistance - warp #3 |
| EDGECOMB_F1 | NUMBER | Edge comb resistance - filling #1 |
| EDGECOMB_F2 | NUMBER | Edge comb resistance - filling #2 |
| EDGECOMB_F3 | NUMBER | Edge comb resistance - filling #3 |

**Flammability (2 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| FLAMMABILITY_W | NUMBER | Flammability test - warp direction |
| FLAMMABILITY_F | NUMBER | Flammability test - filling direction |

**Dimensional Stability (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| DIMENSCHANGE_W1 | NUMBER | Dimensional change - warp #1 |
| DIMENSCHANGE_W2 | NUMBER | Dimensional change - warp #2 |
| DIMENSCHANGE_W3 | NUMBER | Dimensional change - warp #3 |
| DIMENSCHANGE_F1 | NUMBER | Dimensional change - filling #1 |
| DIMENSCHANGE_F2 | NUMBER | Dimensional change - filling #2 |
| DIMENSCHANGE_F3 | NUMBER | Dimensional change - filling #3 |

**Flex Abrasion (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| FLEXABRASION_W1 | NUMBER | Flex abrasion cycles - warp #1 |
| FLEXABRASION_W2 | NUMBER | Flex abrasion cycles - warp #2 |
| FLEXABRASION_W3 | NUMBER | Flex abrasion cycles - warp #3 |
| FLEXABRASION_F1 | NUMBER | Flex abrasion cycles - filling #1 |
| FLEXABRASION_F2 | NUMBER | Flex abrasion cycles - filling #2 |
| FLEXABRASION_F3 | NUMBER | Flex abrasion cycles - filling #3 |

**Notes**:
- Total: 100+ columns covering all airbag fabric test properties
- All measurement columns are nullable (may not apply to all products)
- Triplicateto test pattern (1, 2, 3) ensures statistical validity
- W = Warp direction, F = Filling direction

---

## Business Logic

### Approval Workflow

1. **Test Entry**: Technician completes all tests and enters data
2. **Pending Review**: Tests appear in approval queue (STATUS = 'PENDING')
3. **Supervisor Review**: Quality supervisor uses this procedure to retrieve pending tests
4. **Data Validation**: Supervisor checks all measurements against specifications
5. **Approval Decision**: Supervisor approves or rejects
6. **Status Update**: STATUS changes to 'APPROVED' or 'REJECTED'

### Data Completeness

Not all 100+ columns are populated for every test:
- Some tests only apply to specific product types
- Customer specifications determine which tests are required
- Missing values (NULL) indicate test not performed or not applicable

---
## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_SEARCHLABENTRYPRODUCTION | Similar search | Searches lab test entries (similar structure) |
| LAB_SAVELABMASSPRORESULT | Approval action | Saves approval decision after review |
| LAB_GETITEMTESTSPECIFICATION | Specifications | Gets test specs for validation |
| LAB_MASSPROSTOCKSTATUS | Status query | Gets simpler status view |

---

## UI Integration

### Primary Pages

- **Lab Approval Dashboard**: Main approval workflow interface
- **Test Review Page**: Detailed review of all test measurements
- **Quality Audit Page**: Historical approval review

---

## Notes

- **Comprehensive Data**: Returns complete test dataset for thorough review
- **Performance**: Large result set (100+ columns) - use filters to limit results
- **Approval Critical**: Final quality gate before customer shipment
- **Audit Trail**: APPROVEBY and APPROVEDATE provide complete audit history
- **Statistical Analysis**: Triplicate measurements enable statistical validation
- **Customer Requirements**: Different customers may require different test subsets
- **Quality Metrics**: Data used for SPC charts and quality trend analysis

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 2955-3080)
**Implementation File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 18182-18390)
