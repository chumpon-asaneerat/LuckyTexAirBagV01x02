# DEFECT_SEARCH

**Procedure Number**: 209 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search defect codes with flexible filtering criteria |
| **Operation** | SELECT |
| **Tables** | Defect master table |
| **Called From** | DefectCodeService.cs |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DEFECTID` | VARCHAR2(50) | ⬜ | Defect code filter (partial match) |
| `P_PROCESSID` | VARCHAR2(50) | ⬜ | Process ID filter |
| `P_THAIDESC` | VARCHAR2(200) | ⬜ | Thai description filter (partial match) |
| `P_ENGDESC` | VARCHAR2(200) | ⬜ | English description filter (partial match) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `DEFECTCODE` | VARCHAR2(50) | Defect code |
| `DESCRIPTION_TH` | VARCHAR2(200) | Thai description |
| `DESCRIPTION_EN` | VARCHAR2(200) | English description |
| `PROCESSID` | VARCHAR2(50) | Process ID where defect applies |
| `DEFECTPROCESSCODE` | VARCHAR2(50) | Defect-process combination code |
| `POINT` | NUMBER | Defect penalty points |
| `PROCESSDESCRIPTION` | VARCHAR2(200) | Process description |

---

## Business Logic (What it does and why)

Searches defect master data with flexible filtering across multiple criteria. Defect codes are used throughout quality inspection processes to record fabric defects (holes, stains, streaks, etc.). Each defect has point values that affect overall quality grade. All search parameters are optional for flexible searching.

The procedure:
1. Accepts multiple optional search criteria
2. Searches by defect code, process, or descriptions (Thai/English)
3. Returns defect details with point values and process association
4. Used for lookup before editing defect codes
5. Supports bilingual searching (Thai and English)

Used in quality inspection configuration and defect code management screens.

---

## Related Procedures

**Downstream**:
- [210-DEFECT_INSERTUPDATE.md](./210-DEFECT_INSERTUPDATE.md) - Update found defects
- [211-DEFECT_DELETE.md](./211-DEFECT_DELETE.md) - Delete defects

**Used By**: M08 Inspection module for defect recording

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DefectCodeService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `DEFECT_SEARCH(DEFECT_SEARCHParameter para)`
**Lines**: 29537-29587

---

**File**: 209/296 | **Progress**: 70.6%
