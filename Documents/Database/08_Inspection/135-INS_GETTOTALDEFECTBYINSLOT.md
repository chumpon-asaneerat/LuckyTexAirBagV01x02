# INS_GETTOTALDEFECTBYINSLOT

**Procedure Number**: 135 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Count total number of defects for inspection lot |
| **Operation** | SELECT COUNT |
| **Tables** | tblINSDefect |
| **Called From** | DataServicecs.cs:1717 → GetDefectCount() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_DEFECTID` | VARCHAR2(50) | ⬜ | Defect ID filter (optional) |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `TOTAL` | NUMBER | Total count of defects |

### Returns

Returns single numeric value: total number of defect records for the inspection lot.

---

## Business Logic (What it does and why)

**Purpose**: Count total defects in inspection lot for quality assessment and UI display.

**When Used**:
- Displaying defect count in inspection screens
- Quality metrics calculation
- Validation before operations
- UI status displays

**Business Rules**:
1. P_INSLOT is required
2. P_DEFECTID is optional filter
3. Returns count of all defect records (not deleted)
4. Used for quick defect statistics

**Usage Pattern**:
- Called when loading inspection lot to show defect count
- Used in DefectListWindow to display total defects
- Helps operator understand overall lot quality at a glance
- Part of quality dashboard metrics

---

## Related Procedures

**Related**: INS_GET100MDEFECTPOINT - Calculate defect points (different from count)
**Related**: INS_GETDEFECTLISTREPORT - Get detailed defect list
**Related**: GETINSDEFECTLIST - Get defect details (not count)

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `GetDefectCount(string insLotNo, string DEFECTID)`
**Lines**: 1717-1746

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETTOTALDEFECTBYINSLOT(INS_GETTOTALDEFECTBYINSLOTParameter para)`
**Lines**: ~22620-22660

**Parameter Class**: Lines ~5480-5483
**Result Class**: Lines ~5489-5491 (contains TOTAL field)

---

**File**: 135/296 | **Progress**: 45.6%
