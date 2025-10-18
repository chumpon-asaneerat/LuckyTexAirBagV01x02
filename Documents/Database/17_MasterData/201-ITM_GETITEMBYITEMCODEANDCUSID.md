# ITM_GETITEMBYITEMCODEANDCUSID

**Procedure Number**: 201 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get item details by item code and customer ID |
| **Operation** | SELECT |
| **Tables** | Item master table with customer linkage |
| **Called From** | QualityAssuranceDataService.cs |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_CUSTOMERID` | VARCHAR2(50) | ✅ | Customer ID |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CUSTOMERID` | VARCHAR2(50) | Customer ID |
| `ITM_CODE` | VARCHAR2(50) | Item code |
| `PARTNO` | VARCHAR2(50) | Customer part number |
| `FABRIC` | VARCHAR2(100) | Fabric type/description |
| `LENGTH` | NUMBER | Standard length specification |
| `DENSITY_W` | VARCHAR2(20) | Warp density specification |
| `DENSITY_F` | VARCHAR2(20) | Weft density specification |
| `WIDTH_ALL` | VARCHAR2(20) | Total width |
| `WIDTH_PIN` | VARCHAR2(20) | Width at pin (before coating) |
| `WIDTH_COAT` | VARCHAR2(20) | Width after coating |
| `TRIM_L` | VARCHAR2(20) | Left trim specification |
| `TRIM_R` | VARCHAR2(20) | Right trim specification |
| `FLOPPY_L` | VARCHAR2(20) | Left edge floppy specification |
| `FLOPPY_R` | VARCHAR2(20) | Right edge floppy specification |
| `HARDNESS_L` | VARCHAR2(20) | Left edge hardness specification |
| `HARDNESS_C` | VARCHAR2(20) | Center hardness specification |
| `HARDNESS_R` | VARCHAR2(20) | Right edge hardness specification |
| `UNWINDER` | VARCHAR2(50) | Unwinder setting/type |
| `WINDER` | VARCHAR2(50) | Winder setting/type |
| `FINISHINGCUSTOMER` | VARCHAR2(100) | Finishing customer name |
| `DESCRIPTION` | VARCHAR2(500) | Item description |
| `SUPPLIERCODE` | VARCHAR2(50) | Supplier code |
| `WIDTH` | VARCHAR2(20) | Width specification |
| `WIDTH_SELVAGEL` | VARCHAR2(20) | Left selvage width |
| `WIDTH_SELVAGER` | VARCHAR2(20) | Right selvage width |
| `RESETSTARTLENGTH` | NUMBER | Reset start length for inspection |

---

## Business Logic (What it does and why)

Retrieves item information based on both item code and customer ID. Different customers may have different specifications or quality requirements for the same base item code. This procedure validates that a specific item-customer combination exists and is valid for production or quality testing.

The procedure:
1. Takes item code and customer ID
2. Looks up item master with customer linkage
3. Validates item exists and is configured for this customer
4. Returns item-customer combination
5. Used in quality assurance to ensure tests use correct customer specs

---

## Related Procedures

**Similar**:
- [202-ITM_GETITEMCODELIST.md](./202-ITM_GETITEMCODELIST.md) - Get all item codes
- [203-ITM_GETITEMPREPARELIST.md](./203-ITM_GETITEMPREPARELIST.md) - Get items with prepare status

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\QualityAssuranceDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_GETITEMBYITEMCODEANDCUSID(ITM_GETITEMBYITEMCODEANDCUSIDParameter para)`
**Lines**: 22252-22292

---

**File**: 201/296 | **Progress**: 67.9%
