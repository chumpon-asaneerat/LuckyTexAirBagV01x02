# LAB_SEARCHLABENTRYPRODUCTION

**Procedure Name**: `LAB_SEARCHLABENTRYPRODUCTION`
**Module**: M14 - LAB (Laboratory Testing)
**Purpose**: Searches for laboratory test entry records for production fabric samples
**Return Type**: Result Set (Multiple Rows)

---

## Description

Retrieves comprehensive laboratory test data for production fabric samples that have been entered into the system. This procedure returns complete test results including all physical, mechanical, and functional test measurements for finished fabric samples. Used primarily by lab technicians and quality personnel to search, review, and analyze test data for specific production lots, items, or date ranges.

The result set includes 100+ columns covering all airbag fabric test properties: dimensional measurements, weight analysis, tensile strength, tear resistance, air permeability, flammability, edge comb, stiffness, flex abrasion, and more. This procedure is nearly identical to `LAB_SEARCHAPPROVELAB` but focuses on searching general test entries rather than approval workflow.

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
| STATUS | VARCHAR2 | Test status (PENDING/APPROVED/REJECTED) |
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

**Tensile Strength (18 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| MAXFORCE_W1 | NUMBER | Maximum tensile force - warp #1 |
| MAXFORCE_W2 | NUMBER | Maximum tensile force - warp #2 |
| MAXFORCE_W3 | NUMBER | Maximum tensile force - warp #3 |
| MAXFORCE_W4 | NUMBER | Maximum tensile force - warp #4 |
| MAXFORCE_W5 | NUMBER | Maximum tensile force - warp #5 |
| MAXFORCE_W6 | NUMBER | Maximum tensile force - warp #6 |
| MAXFORCE_F1 | NUMBER | Maximum tensile force - filling #1 |
| MAXFORCE_F2 | NUMBER | Maximum tensile force - filling #2 |
| MAXFORCE_F3 | NUMBER | Maximum tensile force - filling #3 |
| MAXFORCE_F4 | NUMBER | Maximum tensile force - filling #4 |
| MAXFORCE_F5 | NUMBER | Maximum tensile force - filling #5 |
| MAXFORCE_F6 | NUMBER | Maximum tensile force - filling #6 |
| ELONGATIONFORCE_W1 | NUMBER | Elongation at break - warp #1 |
| ELONGATIONFORCE_W2 | NUMBER | Elongation at break - warp #2 |
| ELONGATIONFORCE_W3 | NUMBER | Elongation at break - warp #3 |
| ELONGATIONFORCE_W4 | NUMBER | Elongation at break - warp #4 |
| ELONGATIONFORCE_W5 | NUMBER | Elongation at break - warp #5 |
| ELONGATIONFORCE_W6 | NUMBER | Elongation at break - warp #6 |
| ELONGATIONFORCE_F1 | NUMBER | Elongation at break - filling #1 |
| ELONGATIONFORCE_F2 | NUMBER | Elongation at break - filling #2 |
| ELONGATIONFORCE_F3 | NUMBER | Elongation at break - filling #3 |
| ELONGATIONFORCE_F4 | NUMBER | Elongation at break - filling #4 |
| ELONGATIONFORCE_F5 | NUMBER | Elongation at break - filling #5 |
| ELONGATIONFORCE_F6 | NUMBER | Elongation at break - filling #6 |

**Tear Resistance (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| TEAR_W1 | NUMBER | Tear strength - warp #1 |
| TEAR_W2 | NUMBER | Tear strength - warp #2 |
| TEAR_W3 | NUMBER | Tear strength - warp #3 |
| TEAR_F1 | NUMBER | Tear strength - filling #1 |
| TEAR_F2 | NUMBER | Tear strength - filling #2 |
| TEAR_F3 | NUMBER | Tear strength - filling #3 |

**Air Permeability (12 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| STATIC_AIR1 | NUMBER | Static air permeability #1 |
| STATIC_AIR2 | NUMBER | Static air permeability #2 |
| STATIC_AIR3 | NUMBER | Static air permeability #3 |
| STATIC_AIR4 | NUMBER | Static air permeability #4 |
| STATIC_AIR5 | NUMBER | Static air permeability #5 |
| STATIC_AIR6 | NUMBER | Static air permeability #6 |
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

**Flammability (10 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| FLAMMABILITY_W | NUMBER | Flammability test - warp #1 |
| FLAMMABILITY_W2 | NUMBER | Flammability test - warp #2 |
| FLAMMABILITY_W3 | NUMBER | Flammability test - warp #3 |
| FLAMMABILITY_W4 | NUMBER | Flammability test - warp #4 |
| FLAMMABILITY_W5 | NUMBER | Flammability test - warp #5 |
| FLAMMABILITY_F | NUMBER | Flammability test - filling #1 |
| FLAMMABILITY_F2 | NUMBER | Flammability test - filling #2 |
| FLAMMABILITY_F3 | NUMBER | Flammability test - filling #3 |
| FLAMMABILITY_F4 | NUMBER | Flammability test - filling #4 |
| FLAMMABILITY_F5 | NUMBER | Flammability test - filling #5 |

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

**Flex Scott Test (6 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| FLEX_SCOTT_W1 | NUMBER | Flex Scott test - warp #1 |
| FLEX_SCOTT_W2 | NUMBER | Flex Scott test - warp #2 |
| FLEX_SCOTT_W3 | NUMBER | Flex Scott test - warp #3 |
| FLEX_SCOTT_F1 | NUMBER | Flex Scott test - filling #1 |
| FLEX_SCOTT_F2 | NUMBER | Flex Scott test - filling #2 |
| FLEX_SCOTT_F3 | NUMBER | Flex Scott test - filling #3 |

**Production Information (8 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| ITM_CODE_H | VARCHAR2 | Item code (alternate/header) |
| FINISHLENGTH | NUMBER | Finished fabric length |
| FINISHINGPROCESS | VARCHAR2 | Finishing process type |
| ITEMLOT | VARCHAR2 | Item lot number |
| LOOMNO | VARCHAR2 | Loom machine number |
| FINISHINGMC | VARCHAR2 | Finishing machine number |
| BATCHNO | VARCHAR2 | Production batch number |
| CUSTOMERID | VARCHAR2 | Customer ID |
| PARTNO | VARCHAR2 | Customer part number |

**File Upload (3 columns)**
| Column Name | Data Type | Description |
|------------|-----------|-------------|
| FILENAME | VARCHAR2 | Uploaded file name (if applicable) |
| UPLOADDATE | DATE | File upload date/time |
| UPLOADBY | VARCHAR2 | Employee ID who uploaded file |

**Notes**:
- Total: 110+ columns covering all airbag fabric test properties
- All measurement columns are nullable (may not apply to all products)
- Triplicate test pattern (1, 2, 3) ensures statistical validity
- Some tests expanded to 6 measurements for additional validation
- W = Warp direction, F = Filling direction

---

## Business Logic

### Search and Retrieval Workflow

1. **Search Query**: User enters search criteria (date range, item code, etc.)
2. **Data Retrieval**: Procedure retrieves matching test records from database
3. **Result Display**: All matching tests displayed in grid/list
4. **Data Analysis**: User can analyze test trends and patterns
5. **Status Tracking**: View test status (PENDING/APPROVED/REJECTED)

### Data Usage Patterns

- **Lab Technician**: Search own entries to verify data or make corrections
- **Quality Supervisor**: Review test history for specific products or lots
- **Quality Manager**: Analyze trends across multiple production lots
- **Customer Service**: Retrieve test data for customer inquiries
- **Production Planning**: Review test results to optimize production parameters

---

## Usage Example

### C# Method Call

```csharp
// From AirbagSPs.cs (lines 17974-18173)
public List<LAB_SEARCHLABENTRYPRODUCTIONResult> LAB_SEARCHLABENTRYPRODUCTION(
    LAB_SEARCHLABENTRYPRODUCTIONParameter para)
{
    List<LAB_SEARCHLABENTRYPRODUCTIONResult> results =
        new List<LAB_SEARCHLABENTRYPRODUCTIONResult>();
    if (!HasConnection())
        return results;

    string[] paraNames = new string[]
    {
        "P_ITMCODE",
        "P_ENTRYSTARTDATE",
        "P_ENTRYENDDATE",
        "P_LOOM",
        "P_FINISHPROCESS"
    };
    object[] paraValues = new object[]
    {
        para.P_ITMCODE,
        para.P_ENTRYSTARTDATE,
        para.P_ENTRYENDDATE,
        para.P_LOOM,
        para.P_FINISHPROCESS
    };

    ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
        "LAB_SEARCHLABENTRYPRODUCTION",
        paraNames, paraValues);
    // ... result mapping for 110+ columns ...
}
```

### Common Usage Patterns

```csharp
// Search all tests for today
var param = new LAB_SEARCHLABENTRYPRODUCTIONParameter
{
    P_ITMCODE = null,
    P_ENTRYSTARTDATE = "2023-10-17",
    P_ENTRYENDDATE = "2023-10-17",
    P_LOOM = null,
    P_FINISHPROCESS = null
};
var results = DatabaseManager.Instance.LAB_SEARCHLABENTRYPRODUCTION(param);

// Search specific item code for date range
var param = new LAB_SEARCHLABENTRYPRODUCTIONParameter
{
    P_ITMCODE = "AB-1234-PAB",
    P_ENTRYSTARTDATE = "2023-10-01",
    P_ENTRYENDDATE = "2023-10-31",
    P_LOOM = null,
    P_FINISHPROCESS = null
};
var results = DatabaseManager.Instance.LAB_SEARCHLABENTRYPRODUCTION(param);

// Search by loom and finishing process
var param = new LAB_SEARCHLABENTRYPRODUCTIONParameter
{
    P_ITMCODE = null,
    P_ENTRYSTARTDATE = null,
    P_ENTRYENDDATE = null,
    P_LOOM = "LOOM-05",
    P_FINISHPROCESS = "COATING-A"
};
var results = DatabaseManager.Instance.LAB_SEARCHLABENTRYPRODUCTION(param);
```

---

## Related Procedures

| Procedure Name | Relationship | Description |
|---------------|--------------|-------------|
| LAB_SEARCHAPPROVELAB | Similar search | Searches for approval workflow (nearly identical) |
| LAB_SAVELABMASSPRORESULT | Data entry | Saves test results that this procedure searches |
| LAB_GETITEMTESTSPECIFICATION | Specifications | Gets test specs for comparison |
| LAB_MASSPROSTOCKSTATUS | Status query | Gets simpler status view |
| LAB_UPLOADREPORT | File upload | Uploads PDF reports linked to tests |

---

## UI Integration

### Primary Pages

- **Lab Test Search Page**: Main search and retrieval interface
- **Lab Test Review Page**: Detailed view of individual test records
- **Lab Test History Page**: Historical analysis and trending
- **Lab Test Report Page**: Report generation from search results

### Typical Display

```
Lab Test Search Results
-----------------------
Item Code    Weaving Lot    Finishing Lot   Entry Date    Status     Action
AB-1234-PAB  WL-20231015-01 FL-20231020-001 2023-10-20    APPROVED   [View]
AB-1234-DAB  WL-20231015-02 FL-20231020-002 2023-10-20    PENDING    [View]
AB-1234-SAB  WL-20231015-03 FL-20231020-003 2023-10-21    APPROVED   [View]

[View] button → Opens detailed test data view with all 110+ measurements
```

---

## Notes

- **Comprehensive Data**: Returns complete test dataset for thorough analysis
- **Performance**: Large result set (110+ columns) - use filters to limit results
- **Flexible Search**: Multiple optional parameters for flexible queries
- **Status Tracking**: Includes approval status for workflow visibility
- **Audit Trail**: Complete tracking of entry, approval, and file uploads
- **Statistical Analysis**: Multiple measurements enable statistical validation
- **Customer Requirements**: Different customers may require different test subsets
- **Quality Metrics**: Data used for SPC charts and quality trend analysis
- **Historical Analysis**: Supports long-term quality tracking and trending

---

**Document Generated**: 2025-10-17
**Source File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs` (lines 2782-2929, 17974-18173)
