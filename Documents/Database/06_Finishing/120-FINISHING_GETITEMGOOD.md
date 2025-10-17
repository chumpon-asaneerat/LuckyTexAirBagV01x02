# FINISHING_GETITEMGOOD

**Procedure Number**: 120 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve items (products) for specific customer with full specifications |
| **Operation** | SELECT |
| **Tables** | tblItemSpecification, tblProduct (inferred - JOIN query) |
| **Called From** | DataServicecs.cs:3412 → GetFINISHING_GETITEMGOODDataList() |
| **Frequency** | Medium (per customer selection) |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_CUSTOMER` | VARCHAR2 | ⬜ | Customer ID to filter items |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CUSTOMERID` | VARCHAR2 | Customer ID |
| `ITM_CODE` | VARCHAR2 | Item code |
| `PARTNO` | VARCHAR2 | Part number (customer part number) |
| `FABRIC` | VARCHAR2 | Fabric type/specification |
| `LENGTH` | NUMBER | Standard length (meters) |
| `DENSITY_W` | VARCHAR2 | Warp density (ends/cm) |
| `DENSITY_F` | VARCHAR2 | Filling density (picks/cm) |
| `WIDTH_ALL` | VARCHAR2 | Total width specification (cm) |
| `WIDTH_PIN` | VARCHAR2 | Pin-to-pin width (cm) |
| `WIDTH_COAT` | VARCHAR2 | Coating width (cm) |
| `TRIM_L` | VARCHAR2 | Left trim specification (cm) |
| `TRIM_R` | VARCHAR2 | Right trim specification (cm) |
| `FLOPPY_L` | VARCHAR2 | Left floppy specification |
| `FLOPPY_R` | VARCHAR2 | Right floppy specification |
| `HARDNESS_L` | VARCHAR2 | Left hardness specification |
| `HARDNESS_C` | VARCHAR2 | Center hardness specification |
| `HARDNESS_R` | VARCHAR2 | Right hardness specification |

---

## Business Logic

**Purpose**: Retrieve complete item specifications for a specific customer to populate dropdown lists and provide process setup parameters. This procedure is typically called after customer selection to show available products for that customer.

**When Used**:

**Scenario 1: Customer-Item Selection (Typical UI Flow)**
1. Operator opens coating preparation page
2. Selects customer: "TOYOTA"
3. System calls FINISHING_GETITEMGOOD(P_CUSTOMER='TOYOTA')
4. Returns all TOYOTA items: AB-45-PA66, AB-50-PA66, AB-60-PA66
5. Item dropdown populated with TOYOTA items only
6. Operator selects item for processing
7. System displays item specifications:
   - WIDTH_COAT: 160 cm
   - DENSITY_W: 55 ends/cm
   - HARDNESS target values
8. Operator uses specs to set up machine

**Scenario 2: Process Parameter Reference**
1. Operator setting up dryer for item AB-45-PA66
2. System retrieved item specs from FINISHING_GETITEMGOOD
3. Displays key parameters for reference:
   - WIDTH_PIN: 156 cm (expected output width)
   - TRIM_L: 2 cm, TRIM_R: 2 cm (trim allowances)
   - HARDNESS_L/C/R: Target values for quality
4. Operator adjusts machine to match specifications

**Scenario 3: Quality Control Verification**
1. QC inspector checks finished fabric against specs
2. Retrieves item specifications via FINISHING_GETITEMGOOD
3. Compares actual measurements vs specifications:
   - Measured width: 155.8 cm vs WIDTH_PIN: 156 cm ✓
   - Measured hardness: Within HARDNESS_L/C/R range ✓
   - Trim: Within TRIM_L/R specifications ✓
4. Passes quality check

**Scenario 4: New Product Setup**
1. Process engineer sets up new item for customer
2. Calls FINISHING_GETITEMGOOD for customer
3. Reviews existing similar items for reference
4. Sets process parameters based on:
   - FABRIC type (coating requirements)
   - WIDTH specifications (machine settings)
   - DENSITY (processing speed considerations)

**Business Rules**:
- **Customer-specific**: Returns only items for specified customer
- **NULL customer**: If P_CUSTOMER is null, may return all items
- **Complete specifications**: Includes all parameters needed for finishing
- **Reference data**: Specifications used for setup and quality control
- **Part number traceability**: PARTNO is customer's part number (not internal)

**Key Specifications Explained**:
- **PARTNO**: Customer's part number (for orders/invoicing)
- **FABRIC**: Fabric construction (e.g., "PA66 420D 55x55")
- **DENSITY_W/F**: Warp and filling density (process speed impact)
- **WIDTH_ALL**: Total fabric width before trim
- **WIDTH_PIN**: Final pin-to-pin width (quality target)
- **WIDTH_COAT**: Width to be coated (coating machine setup)
- **TRIM_L/R**: Trim allowances on left/right edges
- **FLOPPY_L/R**: Fabric hand/feel specifications
- **HARDNESS_L/C/R**: Coating hardness at left/center/right positions

---

## Related Procedures

**Upstream Lookup Chain**:
1. [119-FINISHING_GETCUTOMERLIST.md](./119-FINISHING_GETCUTOMERLIST.md) - Get customer list
2. User selects customer
3. **FINISHING_GETITEMGOOD**(customer) - Get items for customer
4. User selects item
5. Process begins

**Similar Procedures**:
- WEAV_GETITEMBYCUSTOMER (M05) - Weaving item lookup
- INS_GETITEMBYCUSTOMER (M08) - Inspection item lookup
- COMMON_GETITEMSPECIFICATION (Common) - General item specs

**Usage in Process Setup**:
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Uses item specs for coating
- [099-FINISHING_INSERTSCOURING.md](./099-FINISHING_INSERTSCOURING.md) - Uses item specs for scouring
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Uses item specs for dryer

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `GetFINISHING_GETITEMGOODDataList(string cusID)`
**Lines**: 3406-3449

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_GETITEMGOODParameter`
**Lines**: 8340-8343

**Result Class**: `FINISHING_GETITEMGOODResult`
**Lines**: 8349-8367 (partial listing above, full class has more columns)

**Method**: `FINISHING_GETITEMGOOD(FINISHING_GETITEMGOODParameter para)`
**Lines**: 27696-27800 (estimated)

---

**File**: 120/296 | **Progress**: 40.5%
