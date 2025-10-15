# INS_SEARCHINSPECTIONDATA

**Procedure Number**: 140 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search inspection lot records by date and machine for data retrieval |
| **Operation** | SELECT |
| **Tables** | tblINSLot |
| **Called From** | DataServicecs.cs:1483 → GetINS_SearchinspectionData() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DATE` | VARCHAR2(50) | ⬜ | Search date (format varies) |
| `P_MC` | VARCHAR2(50) | ⬜ | Machine number filter |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns list of inspection lot records matching search criteria (30+ columns including all inspection lot fields).

---

## Business Logic (What it does and why)

**Purpose**: Search and retrieve inspection lots by date and/or machine for review, reporting, or data lookup.

**When Used**:
- Inspection data search screens
- Historical data lookup
- Quality review by date/machine
- Production tracking and analysis

**Business Rules**:
1. Both P_DATE and P_MC are optional filters
2. If both provided: search by date AND machine
3. If only date: return all lots for that date
4. If only machine: return all lots for that machine
5. If neither: return all inspection lots (or date-limited default)
6. Returns complete inspection lot records

**Search Patterns**:
- **By Date**: Find all inspections on specific date
- **By Machine**: Find all inspections on specific machine
- **By Both**: Find inspections on date and machine combination
- Used for quality audits, production reports, issue investigation

**Data Returned**:
- Complete inspection lot information
- Dates, measurements, grades, operators
- Defect and test record references
- Status flags and remarks

---

## Related Procedures

**Related**: [131-INS_GETINSPECTIONREPORTDATA.md](./131-INS_GETINSPECTIONREPORTDATA.md) - Get specific inspection lot
**Related**: [130-INS_GETFINISHINSLOTDATA.md](./130-INS_GETFINISHINSLOTDATA.md) - Get by finishing lot
**Related**: [132-INS_GETMCSUSPENDDATA.md](./132-INS_GETMCSUSPENDDATA.md) - Get suspended lots by machine

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `GetINS_SearchinspectionData(string _date, string _mc)`
**Lines**: 1483-1557
**Comment**: "เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ Search inspection Data" (Translation: "Added new for use with Search inspection Data")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_SEARCHINSPECTIONDATA(INS_SEARCHINSPECTIONDATAParameter para)`

**Parameter Class**: 2 parameters (P_DATE, P_MC)
**Result Class**: 30+ columns (full inspection lot data)

---

**File**: 140/296 | **Progress**: 47.3%
