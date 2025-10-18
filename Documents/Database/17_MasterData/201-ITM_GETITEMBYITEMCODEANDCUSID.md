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
