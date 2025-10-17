# LAB_SEARCHLABENTRYPRODUCTION

**Procedure Number**: 311 | **Module**: M14 - LAB | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search laboratory test entry records for production fabric samples |
| **Operation** | SELECT |
| **Tables** | Lab test tables (greige/mass production test tables) |
| **Called From** | LABDataService.cs → LAB_SEARCHLABENTRYPRODUCTION() |
| **Frequency** | High (daily search by lab technicians and quality staff) |
| **Performance** | Medium (100+ columns, filter-dependent) |
| **Issues** | 🟡 1 Low (Large result set - recommend using filters) |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code filter (finished product code) |
| `P_ENTRYSTARTDATE` | VARCHAR2(20) | ⬜ | Entry start date filter (YYYY-MM-DD) |
| `P_ENTRYENDDATE` | VARCHAR2(20) | ⬜ | Entry end date filter (YYYY-MM-DD) |
| `P_LOOM` | VARCHAR2(50) | ⬜ | Loom machine number filter |
| `P_FINISHPROCESS` | VARCHAR2(50) | ⬜ | Finishing process type filter |

### Returns (Cursor)

**Basic Information (9 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code (finished product) |
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number |
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot number |
| `ENTRYDATE` | DATE | Test entry date/time |
| `ENTEYBY` | VARCHAR2(50) | Employee ID who entered test |
| `STATUS` | VARCHAR2(20) | Test status (PENDING/APPROVED/REJECTED) |
| `REMARK` | VARCHAR2(500) | Test remarks |
| `APPROVEBY` | VARCHAR2(50) | Employee ID approver |
| `APPROVEDATE` | DATE | Approval date/time |
| `CREATEDATE` | DATE | Record creation date |

**Dimensional Measurements (15 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `WIDTH` | NUMBER | Fabric width |
| `USABLE_WIDTH1` | NUMBER | Usable width #1 |
| `USABLE_WIDTH2` | NUMBER | Usable width #2 |
| `USABLE_WIDTH3` | NUMBER | Usable width #3 |
| `WIDTH_SILICONE1` | NUMBER | Silicone width #1 |
| `WIDTH_SILICONE2` | NUMBER | Silicone width #2 |
| `WIDTH_SILICONE3` | NUMBER | Silicone width #3 |
| `BOW1` | NUMBER | Bow distortion #1 |
| `BOW2` | NUMBER | Bow distortion #2 |
| `BOW3` | NUMBER | Bow distortion #3 |
| `SKEW1` | NUMBER | Skew distortion #1 |
| `SKEW2` | NUMBER | Skew distortion #2 |
| `SKEW3` | NUMBER | Skew distortion #3 |
| `THICKNESS1` | NUMBER | Fabric thickness #1 |
| `THICKNESS2` | NUMBER | Fabric thickness #2 |
| `THICKNESS3` | NUMBER | Fabric thickness #3 |

**Thread Count (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `NUMTHREADS_W1` | NUMBER | Warp thread count #1 |
| `NUMTHREADS_W2` | NUMBER | Warp thread count #2 |
| `NUMTHREADS_W3` | NUMBER | Warp thread count #3 |
| `NUMTHREADS_F1` | NUMBER | Filling thread count #1 |
| `NUMTHREADS_F2` | NUMBER | Filling thread count #2 |
| `NUMTHREADS_F3` | NUMBER | Filling thread count #3 |

**Weight Measurements (18 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `TOTALWEIGHT1` | NUMBER | Total weight #1 |
| `TOTALWEIGHT2` | NUMBER | Total weight #2 |
| `TOTALWEIGHT3` | NUMBER | Total weight #3 |
| `TOTALWEIGHT4` | NUMBER | Total weight #4 |
| `TOTALWEIGHT5` | NUMBER | Total weight #5 |
| `TOTALWEIGHT6` | NUMBER | Total weight #6 |
| `UNCOATEDWEIGHT1` | NUMBER | Uncoated weight #1 |
| `UNCOATEDWEIGHT2` | NUMBER | Uncoated weight #2 |
| `UNCOATEDWEIGHT3` | NUMBER | Uncoated weight #3 |
| `UNCOATEDWEIGHT4` | NUMBER | Uncoated weight #4 |
| `UNCOATEDWEIGHT5` | NUMBER | Uncoated weight #5 |
| `UNCOATEDWEIGHT6` | NUMBER | Uncoated weight #6 |
| `COATINGWEIGHT1` | NUMBER | Coating weight #1 |
| `COATINGWEIGHT2` | NUMBER | Coating weight #2 |
| `COATINGWEIGHT3` | NUMBER | Coating weight #3 |
| `COATINGWEIGHT4` | NUMBER | Coating weight #4 |
| `COATINGWEIGHT5` | NUMBER | Coating weight #5 |
| `COATINGWEIGHT6` | NUMBER | Coating weight #6 |

**Tensile Strength (24 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `MAXFORCE_W1` | NUMBER | Max tensile force warp #1 |
| `MAXFORCE_W2` | NUMBER | Max tensile force warp #2 |
| `MAXFORCE_W3` | NUMBER | Max tensile force warp #3 |
| `MAXFORCE_W4` | NUMBER | Max tensile force warp #4 |
| `MAXFORCE_W5` | NUMBER | Max tensile force warp #5 |
| `MAXFORCE_W6` | NUMBER | Max tensile force warp #6 |
| `MAXFORCE_F1` | NUMBER | Max tensile force filling #1 |
| `MAXFORCE_F2` | NUMBER | Max tensile force filling #2 |
| `MAXFORCE_F3` | NUMBER | Max tensile force filling #3 |
| `MAXFORCE_F4` | NUMBER | Max tensile force filling #4 |
| `MAXFORCE_F5` | NUMBER | Max tensile force filling #5 |
| `MAXFORCE_F6` | NUMBER | Max tensile force filling #6 |
| `ELONGATIONFORCE_W1` | NUMBER | Elongation at break warp #1 |
| `ELONGATIONFORCE_W2` | NUMBER | Elongation at break warp #2 |
| `ELONGATIONFORCE_W3` | NUMBER | Elongation at break warp #3 |
| `ELONGATIONFORCE_W4` | NUMBER | Elongation at break warp #4 |
| `ELONGATIONFORCE_W5` | NUMBER | Elongation at break warp #5 |
| `ELONGATIONFORCE_W6` | NUMBER | Elongation at break warp #6 |
| `ELONGATIONFORCE_F1` | NUMBER | Elongation at break filling #1 |
| `ELONGATIONFORCE_F2` | NUMBER | Elongation at break filling #2 |
| `ELONGATIONFORCE_F3` | NUMBER | Elongation at break filling #3 |
| `ELONGATIONFORCE_F4` | NUMBER | Elongation at break filling #4 |
| `ELONGATIONFORCE_F5` | NUMBER | Elongation at break filling #5 |
| `ELONGATIONFORCE_F6` | NUMBER | Elongation at break filling #6 |

**Tear Resistance (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `TEAR_W1` | NUMBER | Tear strength warp #1 |
| `TEAR_W2` | NUMBER | Tear strength warp #2 |
| `TEAR_W3` | NUMBER | Tear strength warp #3 |
| `TEAR_F1` | NUMBER | Tear strength filling #1 |
| `TEAR_F2` | NUMBER | Tear strength filling #2 |
| `TEAR_F3` | NUMBER | Tear strength filling #3 |

**Air Permeability (12 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `STATIC_AIR1` | NUMBER | Static air permeability #1 |
| `STATIC_AIR2` | NUMBER | Static air permeability #2 |
| `STATIC_AIR3` | NUMBER | Static air permeability #3 |
| `STATIC_AIR4` | NUMBER | Static air permeability #4 |
| `STATIC_AIR5` | NUMBER | Static air permeability #5 |
| `STATIC_AIR6` | NUMBER | Static air permeability #6 |
| `DYNAMIC_AIR1` | NUMBER | Dynamic air permeability #1 |
| `DYNAMIC_AIR2` | NUMBER | Dynamic air permeability #2 |
| `DYNAMIC_AIR3` | NUMBER | Dynamic air permeability #3 |
| `EXPONENT1` | NUMBER | Permeability exponent #1 |
| `EXPONENT2` | NUMBER | Permeability exponent #2 |
| `EXPONENT3` | NUMBER | Permeability exponent #3 |

**Stiffness (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `STIFFNESS_W1` | NUMBER | Stiffness warp #1 |
| `STIFFNESS_W2` | NUMBER | Stiffness warp #2 |
| `STIFFNESS_W3` | NUMBER | Stiffness warp #3 |
| `STIFFNESS_F1` | NUMBER | Stiffness filling #1 |
| `STIFFNESS_F2` | NUMBER | Stiffness filling #2 |
| `STIFFNESS_F3` | NUMBER | Stiffness filling #3 |

**Bending (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `BENDING_W1` | NUMBER | Bending stiffness warp #1 |
| `BENDING_W2` | NUMBER | Bending stiffness warp #2 |
| `BENDING_W3` | NUMBER | Bending stiffness warp #3 |
| `BENDING_F1` | NUMBER | Bending stiffness filling #1 |
| `BENDING_F2` | NUMBER | Bending stiffness filling #2 |
| `BENDING_F3` | NUMBER | Bending stiffness filling #3 |

**Edge Comb (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `EDGECOMB_W1` | NUMBER | Edge comb warp #1 |
| `EDGECOMB_W2` | NUMBER | Edge comb warp #2 |
| `EDGECOMB_W3` | NUMBER | Edge comb warp #3 |
| `EDGECOMB_F1` | NUMBER | Edge comb filling #1 |
| `EDGECOMB_F2` | NUMBER | Edge comb filling #2 |
| `EDGECOMB_F3` | NUMBER | Edge comb filling #3 |

**Flammability (10 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `FLAMMABILITY_W` | NUMBER | Flammability warp #1 |
| `FLAMMABILITY_W2` | NUMBER | Flammability warp #2 |
| `FLAMMABILITY_W3` | NUMBER | Flammability warp #3 |
| `FLAMMABILITY_W4` | NUMBER | Flammability warp #4 |
| `FLAMMABILITY_W5` | NUMBER | Flammability warp #5 |
| `FLAMMABILITY_F` | NUMBER | Flammability filling #1 |
| `FLAMMABILITY_F2` | NUMBER | Flammability filling #2 |
| `FLAMMABILITY_F3` | NUMBER | Flammability filling #3 |
| `FLAMMABILITY_F4` | NUMBER | Flammability filling #4 |
| `FLAMMABILITY_F5` | NUMBER | Flammability filling #5 |

**Dimensional Stability (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `DIMENSCHANGE_W1` | NUMBER | Dimensional change warp #1 |
| `DIMENSCHANGE_W2` | NUMBER | Dimensional change warp #2 |
| `DIMENSCHANGE_W3` | NUMBER | Dimensional change warp #3 |
| `DIMENSCHANGE_F1` | NUMBER | Dimensional change filling #1 |
| `DIMENSCHANGE_F2` | NUMBER | Dimensional change filling #2 |
| `DIMENSCHANGE_F3` | NUMBER | Dimensional change filling #3 |

**Flex Abrasion (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `FLEXABRASION_W1` | NUMBER | Flex abrasion warp #1 |
| `FLEXABRASION_W2` | NUMBER | Flex abrasion warp #2 |
| `FLEXABRASION_W3` | NUMBER | Flex abrasion warp #3 |
| `FLEXABRASION_F1` | NUMBER | Flex abrasion filling #1 |
| `FLEXABRASION_F2` | NUMBER | Flex abrasion filling #2 |
| `FLEXABRASION_F3` | NUMBER | Flex abrasion filling #3 |

**Flex Scott (6 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `FLEX_SCOTT_W1` | NUMBER | Flex Scott warp #1 |
| `FLEX_SCOTT_W2` | NUMBER | Flex Scott warp #2 |
| `FLEX_SCOTT_W3` | NUMBER | Flex Scott warp #3 |
| `FLEX_SCOTT_F1` | NUMBER | Flex Scott filling #1 |
| `FLEX_SCOTT_F2` | NUMBER | Flex Scott filling #2 |
| `FLEX_SCOTT_F3` | NUMBER | Flex Scott filling #3 |

**Production Information (8 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE_H` | VARCHAR2(50) | Item code (header/alternate) |
| `FINISHLENGTH` | NUMBER | Finished fabric length |
| `FINISHINGPROCESS` | VARCHAR2(50) | Finishing process type |
| `ITEMLOT` | VARCHAR2(50) | Item lot number |
| `LOOMNO` | VARCHAR2(50) | Loom machine number |
| `FINISHINGMC` | VARCHAR2(50) | Finishing machine number |
| `BATCHNO` | VARCHAR2(50) | Production batch number |
| `CUSTOMERID` | VARCHAR2(50) | Customer ID |
| `PARTNO` | VARCHAR2(50) | Customer part number |

**File Upload (3 columns)**
| Column | Type | Description |
|--------|------|-------------|
| `FILENAME` | VARCHAR2(255) | Uploaded file name |
| `UPLOADDATE` | DATE | File upload date/time |
| `UPLOADBY` | VARCHAR2(50) | Employee ID uploader |

**Total**: 110+ columns covering all airbag fabric test properties

---

## Business Logic (What it does and why)

Retrieves comprehensive laboratory test data for production fabric samples for search and analysis purposes. Used by lab technicians, quality supervisors, and quality managers to:

1. **Search Test Records**: Find specific test entries by item code, date range, loom, or finishing process
2. **Review Test History**: Analyze historical test data for quality trending
3. **Data Validation**: Review test measurements against customer specifications
4. **Quality Analysis**: Identify patterns in test failures or quality issues
5. **Customer Inquiries**: Retrieve test data for customer questions about specific lots

**Workflow**:
- Lab technician or quality staff enters search criteria (date range, item code, etc.)
- Procedure retrieves all matching test records with complete 110+ column dataset
- Results displayed in search grid showing key information (item, lots, dates, status)
- User can drill down into specific test record to view all detailed measurements
- Data used for quality reports, customer certificates, and trending analysis

**Key Business Rules**:
- All parameters optional - allows flexible search queries
- Returns complete test dataset (dimensional, mechanical, functional properties)
- Triplicate measurements (1,2,3) ensure statistical validity
- Some tests have 6 measurements for additional validation
- Not all 110+ columns populated for every test (depends on product requirements)
- W = Warp direction, F = Filling direction

---

## Related Procedures

**Similar**: [310-LAB_SEARCHAPPROVELAB.md](./310-LAB_SEARCHAPPROVELAB.md) - Nearly identical, focused on approval workflow
**Data Entry**: [308-LAB_SAVELABMASSPRORESULT.md](./308-LAB_SAVELABMASSPRORESULT.md) - Saves test data that this searches
**Specifications**: [301-LAB_GETITEMTESTSPECIFICATION.md](./301-LAB_GETITEMTESTSPECIFICATION.md) - Gets specs for comparison
**Status Query**: [306-LAB_MASSPROSTOCKSTATUS.md](./306-LAB_MASSPROSTOCKSTATUS.md) - Simpler status view

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_SEARCHLABENTRYPRODUCTION()`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_SEARCHLABENTRYPRODUCTION(LAB_SEARCHLABENTRYPRODUCTIONParameter para)`
**Lines**: 17974-18173
**Parameter Class**: Lines 2782-2929

---

**File**: 311/296 | **Progress**: 64.5%
