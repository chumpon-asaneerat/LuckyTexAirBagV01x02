# DEFECT_DELETE

**Procedure Number**: 211 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete defect code from master data |
| **Operation** | DELETE |
| **Tables** | Defect master table |
| **Called From** | DefectCodeService.cs |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2(50) | ✅ | Defect code ID to delete |

### Output (OUT)

None (void procedure)

---

## Business Logic (What it does and why)

Deletes defect code from master data. Used when defect type is no longer applicable or was created in error. Deletion may be restricted if the defect code is referenced by existing inspection records (referential integrity).

The procedure:
1. Validates defect ID exists
2. Checks for existing references in inspection data
3. Prevents deletion if defect code is in use
4. Deletes defect record if not referenced
5. May set delete flag instead of physical delete (soft delete)

**Warning**: Cannot delete defect codes that have been used in historical inspection records.

---

## Related Procedures

**Upstream**:
- [209-DEFECT_SEARCH.md](./209-DEFECT_SEARCH.md) - Search defects to delete

**Related**:
- [210-DEFECT_INSERTUPDATE.md](./210-DEFECT_INSERTUPDATE.md) - Create/update defects

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DefectCodeService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `DEFECT_DELETE(DEFECT_DELETEParameter para)`
**Lines**: 29635-29659

---

**File**: 211/296 | **Progress**: 71.3%
