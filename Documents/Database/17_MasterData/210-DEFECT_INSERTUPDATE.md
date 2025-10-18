# DEFECT_INSERTUPDATE

**Procedure Number**: 210 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create or update defect code master data |
| **Operation** | INSERT/UPDATE |
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
| `P_DEFECTID` | VARCHAR2(50) | ✅ | Defect code ID |
| `P_PROCESSID` | VARCHAR2(50) | ✅ | Process ID where defect applies |
| `P_THAIDESC` | VARCHAR2(200) | ✅ | Thai description of defect |
| `P_ENGDESC` | VARCHAR2(200) | ✅ | English description of defect |
| `P_POINT` | NUMBER | ✅ | Defect penalty points |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Creates or updates defect code master data with bilingual descriptions and point values. Defect codes are essential for quality inspection - each type of fabric defect (hole, stain, streak, yarn break, etc.) has a code, description, and point value. Point values determine quality grade and customer acceptability.

The procedure:
1. Validates defect ID and process ID
2. Checks if defect code exists (UPDATE) or new (INSERT)
3. Saves defect code with Thai and English descriptions
4. Sets point value for quality grading calculations
5. Links defect to specific process (weaving, finishing, etc.)
6. Returns success or error status

Higher point values indicate more severe defects. Total points across fabric roll determine quality grade (A, B, C, or reject).

---

## Related Procedures

**Upstream**:
- [209-DEFECT_SEARCH.md](./209-DEFECT_SEARCH.md) - Search defects before editing

**Related**:
- [211-DEFECT_DELETE.md](./211-DEFECT_DELETE.md) - Delete defect codes

**Used By**: M08 Inspection module for defect recording and grading

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DefectCodeService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `DEFECT_INSERTUPDATE(DEFECT_INSERTUPDATEParameter para)`
**Lines**: 29595-29629

---

**File**: 210/296 | **Progress**: 70.9%
