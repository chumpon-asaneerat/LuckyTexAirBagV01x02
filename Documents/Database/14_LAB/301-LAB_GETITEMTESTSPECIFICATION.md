# LAB_GETITEMTESTSPECIFICATION

**Procedure Number**: 301 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get complete test specifications and tolerances for item code |
| **Operation** | SELECT |
| **Tables** | tblLabItemSpecification (assumed) |
| **Called From** | LABDataService.cs → LAB_GETITEMTESTSPECIFICATION() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Product item code |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

**Note**: This procedure returns **100+ columns** with all test specifications, tolerances, and control limits for a product. Listed below are key column groups:

#### Product Identification
| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Product item code |
| `CUSTOMERID` | VARCHAR2(50) | Customer identification |

#### Physical Dimensions Specifications
| Column | Type | Description |
|--------|------|-------------|
| `WIDTH` | NUMBER | Fabric width specification (mm) |
| `USABLE_WIDTH` | NUMBER | Usable width specification (mm) |
| `USABLE_WIDTH_TOR` | VARCHAR2(10) | Usable width tolerance (±) |
| `THICKNESS` | NUMBER | Fabric thickness (mm) |
| `THICKNESS_TOR` | NUMBER | Thickness tolerance |

#### Weight Specifications
| Column | Type | Description |
|--------|------|-------------|
| `TOTALWEIGHT` | NUMBER | Total weight specification (g/m²) |
| `TOTALWEIGHT_TOR` | NUMBER | Total weight tolerance |
| `UNCOATEDWEIGHT` | NUMBER | Uncoated fabric weight (g/m²) |
| `UNCOATEDWEIGHT_TOR` | NUMBER | Uncoated weight tolerance |
| `COATINGWEIGHT` | NUMBER | Coating weight (g/m²) |
| `COATINGWEIGHT_TOR` | NUMBER | Coating weight tolerance |

#### Thread Count Specifications
| Column | Type | Description |
|--------|------|-------------|
| `NUMTHREADS_W` | NUMBER | Warp thread count (threads/cm) |
| `NUMTHREADS_W_TOR` | NUMBER | Warp thread tolerance |
| `NUMTHREADS_F` | NUMBER | Weft thread count (threads/cm) |
| `NUMTHREADS_F_TOR` | NUMBER | Weft thread tolerance |

#### Coating Specifications
| Column | Type | Description |
|--------|------|-------------|
| `WIDTH_SILICONE` | NUMBER | Silicone coating width (mm) |
| `WIDTH_SILICONE_TOR` | VARCHAR2(10) | Silicone width tolerance |

#### Tensile Strength Specifications (Warp/Weft)
| Column | Type | Description |
|--------|------|-------------|
| `MAXFORCE_W` | NUMBER | Max tensile force - Warp (N/mm) |
| `MAXFORCE_W_TOR` | VARCHAR2(10) | Warp force tolerance |
| `MAXFORCE_F` | NUMBER | Max tensile force - Weft (N/mm) |
| `MAXFORCE_F_TOR` | VARCHAR2(10) | Weft force tolerance |
| `ELONGATIONFORCE_W` | NUMBER | Elongation at break - Warp (%) |
| `ELONGATIONFORCE_W_TOR` | VARCHAR2(10) | Warp elongation tolerance |
| `ELONGATIONFORCE_F` | NUMBER | Elongation at break - Weft (%) |
| `ELONGATIONFORCE_F_TOR` | VARCHAR2(10) | Weft elongation tolerance |

#### Tear Strength Specifications
| Column | Type | Description |
|--------|------|-------------|
| `TEAR_W` | NUMBER | Tear strength - Warp (N) |
| `TEAR_W_TOR` | VARCHAR2(10) | Warp tear tolerance |
| `TEAR_F` | NUMBER | Tear strength - Weft (N) |
| `TEAR_F_TOR` | VARCHAR2(10) | Weft tear tolerance |

#### Air Permeability Specifications
| Column | Type | Description |
|--------|------|-------------|
| `STATIC_AIR` | NUMBER | Static air permeability (L/dm²/min) |
| `STATIC_AIR_TOR` | VARCHAR2(10) | Static air tolerance |
| `DYNAMIC_AIR` | NUMBER | Dynamic air permeability |
| `DYNAMIC_AIR_TOR` | NUMBER | Dynamic air tolerance |
| `EXPONENT` | NUMBER | Air permeability exponent |
| `EXPONENT_TOR` | NUMBER | Exponent tolerance |

#### Edge Comb Specifications
| Column | Type | Description |
|--------|------|-------------|
| `EDGECOMB_W` | NUMBER | Edge comb resistance - Warp |
| `EDGECOMB_W_TOR` | VARCHAR2(10) | Warp edge comb tolerance |
| `EDGECOMB_F` | NUMBER | Edge comb resistance - Weft |
| `EDGECOMB_F_TOR` | VARCHAR2(10) | Weft edge comb tolerance |

#### Stiffness Specifications
| Column | Type | Description |
|--------|------|-------------|
| `STIFFNESS_W` | NUMBER | Stiffness - Warp (mg) |
| `STIFFNESS_W_TOR` | VARCHAR2(10) | Warp stiffness tolerance |
| `STIFFNESS_F` | NUMBER | Stiffness - Weft (mg) |
| `STIFFNESS_F_TOR` | VARCHAR2(10) | Weft stiffness tolerance |

#### Flammability Specifications
| Column | Type | Description |
|--------|------|-------------|
| `FLAMMABILITY_W` | NUMBER | Flammability - Warp |
| `FLAMMABILITY_W_TOR` | VARCHAR2(10) | Warp flammability tolerance |
| `FLAMMABILITY_F` | NUMBER | Flammability - Weft |
| `FLAMMABILITY_F_TOR` | VARCHAR2(10) | Weft flammability tolerance |

#### Flex Abrasion Specifications
| Column | Type | Description |
|--------|------|-------------|
| `FLEXABRASION_W` | NUMBER | Flex abrasion - Warp (cycles) |
| `FLEXABRASION_W_TOR` | VARCHAR2(10) | Warp abrasion tolerance |
| `FLEXABRASION_F` | NUMBER | Flex abrasion - Weft (cycles) |
| `FLEXABRASION_F_TOR` | VARCHAR2(10) | Weft abrasion tolerance |

#### Dimensional Change Specifications
| Column | Type | Description |
|--------|------|-------------|
| `DIMENSCHANGE_W` | NUMBER | Dimensional change - Warp (%) |
| `DIMENSCHANGE_W_TOR` | VARCHAR2(10) | Warp dimension tolerance |
| `DIMENSCHANGE_F` | NUMBER | Dimensional change - Weft (%) |
| `DIMENSCHANGE_F_TOR` | VARCHAR2(10) | Weft dimension tolerance |

#### Fabric Straightness Specifications
| Column | Type | Description |
|--------|------|-------------|
| `BOW` | NUMBER | Bow specification (%) |
| `BOW_TOR` | VARCHAR2(10) | Bow tolerance |
| `SKEW` | NUMBER | Skew specification (%) |
| `SKEW_TOR` | VARCHAR2(10) | Skew tolerance |

#### Bending Specifications
| Column | Type | Description |
|--------|------|-------------|
| `BENDING_W` | NUMBER | Bending rigidity - Warp |
| `BENDING_W_TOR` | VARCHAR2(10) | Warp bending tolerance |
| `BENDING_F` | NUMBER | Bending rigidity - Weft |
| `BENDING_F_TOR` | VARCHAR2(10) | Weft bending tolerance |

#### Flex Scott Specifications
| Column | Type | Description |
|--------|------|-------------|
| `FLEX_SCOTT_W` | NUMBER | Flex scott - Warp |
| `FLEX_SCOTT_W_TOR` | VARCHAR2(10) | Warp flex scott tolerance |
| `FLEX_SCOTT_F` | NUMBER | Flex scott - Weft |
| `FLEX_SCOTT_F_TOR` | VARCHAR2(10) | Weft flex scott tolerance |

#### Statistical Control Limits (UCL/LCL)
**Note**: Upper Control Limit (UCL) and Lower Control Limit (LCL) for SPC (Statistical Process Control)

| Column | Type | Description |
|--------|------|-------------|
| `USABLE_WIDTH_LCL` | NUMBER | Usable width lower control limit |
| `USABLE_WIDTH_UCL` | NUMBER | Usable width upper control limit |
| `TOTALWEIGHT_LCL` | NUMBER | Total weight lower control limit |
| `TOTALWEIGHT_UCL` | NUMBER | Total weight upper control limit |
| `NUMTHREADS_W_LCL` | NUMBER | Warp thread count LCL |
| `NUMTHREADS_W_UCL` | NUMBER | Warp thread count UCL |
| `NUMTHREADS_F_LCL` | NUMBER | Weft thread count LCL |
| `NUMTHREADS_F_UCL` | NUMBER | Weft thread count UCL |
| `MAXFORCE_W_LCL` | NUMBER | Warp tensile force LCL |
| `MAXFORCE_W_UCL` | NUMBER | Warp tensile force UCL |
| `MAXFORCE_F_LCL` | NUMBER | Weft tensile force LCL |
| `MAXFORCE_F_UCL` | NUMBER | Weft tensile force UCL |
| `ELONGATIONFORCE_W_LCL` | NUMBER | Warp elongation LCL |
| `ELONGATIONFORCE_W_UCL` | NUMBER | Warp elongation UCL |
| `ELONGATIONFORCE_F_LCL` | NUMBER | Weft elongation LCL |
| `ELONGATIONFORCE_F_UCL` | NUMBER | Weft elongation UCL |
| `EDGECOMB_W_LCL` | NUMBER | Warp edge comb LCL |
| `EDGECOMB_W_UCL` | NUMBER | Warp edge comb UCL |
| `EDGECOMB_F_LCL` | NUMBER | Weft edge comb LCL |
| `EDGECOMB_F_UCL` | NUMBER | Weft edge comb UCL |
| `TEAR_W_LCL` | NUMBER | Warp tear LCL |
| `TEAR_W_UCL` | NUMBER | Warp tear UCL |
| `TEAR_F_LCL` | NUMBER | Weft tear LCL |
| `TEAR_F_UCL` | NUMBER | Weft tear UCL |
| `STATIC_AIR_LCL` | NUMBER | Static air permeability LCL |
| `STATIC_AIR_UCL` | NUMBER | Static air permeability UCL |
| `DYNAMIC_AIR_LCL` | NUMBER | Dynamic air permeability LCL |
| `DYNAMIC_AIR_UCL` | NUMBER | Dynamic air permeability UCL |
| `EXPONENT_LCL` | NUMBER | Air exponent LCL |
| `EXPONENT_UCL` | NUMBER | Air exponent UCL |

**Total**: ~100 columns covering all airbag fabric specifications

---

## Business Logic (What it does and why)

Retrieves complete test specification master data for a product item code. This is the "specification sheet" that defines all quality requirements and tolerances for lab testing.

**Workflow**:
1. Receives product item code
2. Queries specification master table
3. Returns all test specifications with:
   - Target values (specification)
   - Tolerances (acceptable deviation)
   - Control limits (for SPC charts)
4. Lab uses specifications to:
   - Validate test results
   - Determine pass/fail
   - Plot control charts
   - Generate test reports

**Business Rules**:
- Each item code has ONE specification record
- Specifications defined by customer requirements
- Tolerances indicate acceptable range (±)
- Control limits used for statistical process control
- W = Warp direction, F = Weft direction
- TOR = Tolerance

**Test Result Validation**:
**Control Chart Monitoring**:
**Test Coverage**:
This specification covers ALL automotive airbag fabric tests:
1. **Physical**: Width, thickness, weight, thread count
2. **Mechanical**: Tensile, tear, stiffness, bending
3. **Functional**: Air permeability (static/dynamic)
4. **Durability**: Flex abrasion, edge comb
5. **Safety**: Flammability
6. **Dimensional**: Bow, skew, dimensional change

---

## Related Procedures

**Used By**: All lab test entry procedures
**Used By**: LAB_SAVELABRESULT - Validates results against spec
**Used By**: LAB_APPROVELABDATA - Checks approval criteria
**Related**: LAB_INSERTUPDATEITEMSPEC - Updates specifications

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GETITEMTESTSPECIFICATION()`
**Lines**: Likely in specification retrieval section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GETITEMTESTSPECIFICATION(LAB_GETITEMTESTSPECIFICATIONParameter para)`
**Lines**: 4447-4557 (111 columns!)

**Implementation Note**: This is one of the largest result sets in the system due to comprehensive airbag fabric testing requirements.

---

**File**: 301/296 | **Progress**: 101.7%
