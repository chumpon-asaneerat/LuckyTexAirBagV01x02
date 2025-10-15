# INS_GETNETLENGTH

**Procedure Number**: 133 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Calculate net fabric length after deducting defect compensation lengths |
| **Operation** | SELECT (calculation) |
| **Tables** | tblINSDefect, tblDefectCode (calculate compensation) |
| **Called From** | DataServicecs.cs:2670 → GetNetLength() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_CUSID` | VARCHAR2(50) | ⬜ | Customer ID |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code |
| `P_LENGTH` | NUMBER | ⬜ | Gross length (meters) |
| `P_GRADE` | VARCHAR2(10) | ✅ | Quality grade |
| `P_DEFECTID` | VARCHAR2(50) | ⬜ | Defect ID reference |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `NETLENGTH` | NUMBER | Calculated net length after defect compensation |

### Returns

Returns single decimal value: net length = gross length - total compensation length for all defects.

---

## Business Logic (What it does and why)

**Purpose**: Calculate the final net fabric length by deducting compensation lengths for defects from gross length.

**When Used**: During inspection process to determine the final sellable fabric length after accounting for defects that require fabric cuts or compensation.

**Business Rules**:
1. P_GRADE is required (cannot be null/empty)
2. Calculates total compensation length from all defects in the inspection lot
3. Formula: Net Length = Gross Length - Total Defect Compensation Length
4. Compensation lengths vary by defect type (defined in defect code master)
5. Used to determine final billable/sellable fabric length

**Defect Compensation System**:
- Some defects require cutting fabric sections
- Each defect type has compensation length (e.g., holes = cut 1m, tears = cut 0.5m)
- System sums all compensation lengths for the lot
- Subtracts from gross length to get net length
- Net length = what customer receives/pays for

---

## Related Procedures

**Related**: INS_GETTOTALDEFECTBYINSLOT - Count total defects
**Related**: GETDEFECTCODEDETAIL - Get defect compensation rules
**Downstream**: Used in grade calculation and final lot processing

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `GetNetLength(string cusid, string itemCode, decimal? length, string grade, string defectId)`
**Lines**: 2670-2703

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETNETLENGTH(INS_GETNETLENGTHParameter para)`
**Lines**: 22719-22759

**Parameter Class**: Lines 5519-5526
**Result Class**: Lines 5532-5535

---

**File**: 133/296 | **Progress**: 44.9%
