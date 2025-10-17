# PACK_SEARCHINSPECTIONDATA

**Procedure Number**: 294 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search available inspection lots for packing |
| **Operation** | SELECT |
| **Tables** | tblInspection, tblInspectionDefect (joined) |
| **Called From** | PackingDataService.cs → PACK_SEARCHINSPECTIONDATA() |
| **Frequency** | High |
| **Performance** | Medium |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DATE` | VARCHAR2(20) | ⬜ | Inspection date filter (optional) |
| `P_GRADE` | VARCHAR2(10) | ⬜ | Quality grade filter (A/B/C) (optional) |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code filter (optional) |
| `P_INSLOT` | VARCHAR2(50) | ⬜ | Inspection lot filter (optional) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `INSPECTIONLOT` | VARCHAR2(50) | Inspection lot number |
| `ITEMCODE` | VARCHAR2(50) | Product item code |
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot reference |
| `STARTDATE` | DATE | Inspection start date |
| `ENDDATE` | DATE | Inspection end date |
| `GROSSLENGTH` | NUMBER | Gross length (meters) |
| `NETLENGTH` | NUMBER | Net length (meters) |
| `CUSTOMERID` | VARCHAR2(50) | Customer identification |
| `PRODUCTTYPEID` | VARCHAR2(10) | Product type code |
| `GRADE` | VARCHAR2(10) | Quality grade |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg) |
| `NETWEIGHT` | NUMBER | Net weight (kg) |
| `PEINSPECTIONLOT` | VARCHAR2(50) | Previous inspection lot (if retest) |
| `DEFECTID` | VARCHAR2(50) | Defect identifier |
| `REMARK` | VARCHAR2(500) | Inspection remarks |
| `ATTACHID` | VARCHAR2(50) | Attachment file ID |
| `TESTRECORDID` | VARCHAR2(50) | Test record reference |
| `INSPECTEDBY` | VARCHAR2(50) | Inspector operator ID |
| `MCNO` | VARCHAR2(50) | Machine number used |
| `FINISHFLAG` | VARCHAR2(10) | Completion flag |
| `SUSPENDDATE` | DATE | Suspension date (if suspended) |
| `INSPECTIONID` | VARCHAR2(50) | Inspection record ID |
| `RETEST` | VARCHAR2(10) | Retest flag (Y/N) |
| `PREITEMCODE` | VARCHAR2(50) | Previous item code (if rework) |
| `CLEARBY` | VARCHAR2(50) | Operator who cleared suspension |
| `CLEARREMARK` | VARCHAR2(500) | Clear suspension remarks |
| `SUSPENDBY` | VARCHAR2(50) | Operator who suspended |
| `STARTDATE1` | DATE | Alternative start date |
| `CUSTOMERTYPE` | VARCHAR2(50) | Customer type classification |
| `DEFECTFILENAME` | VARCHAR2(500) | Defect image filename |
| `ISPACKED` | VARCHAR2(10) | Packing status (Y/N) |
| `LOADINGTYPE` | VARCHAR2(10) | Loading type for shipping |
| `OPERATOR_GROUP` | VARCHAR2(50) | Operator group/shift |
| `DF_CODE` | VARCHAR2(50) | Defect code |
| `DF_AMOUNT` | NUMBER | Defect amount/count |
| `DF_POINT` | NUMBER | Defect point score |
| `ITM_GROUP` | VARCHAR2(50) | Item group classification |

---

## Business Logic (What it does and why)

Searches for completed inspection lots that are available for packing. Provides flexible filtering to find specific lots or groups of lots ready for customer shipping.

**Workflow**:
1. Accepts optional filter criteria:
   - Inspection date range
   - Quality grade (A, B, or C)
   - Item code
   - Specific inspection lot number
2. Queries inspection records with status:
   - Inspection completed (FINISHFLAG = 'Y')
   - Not yet packed (ISPACKED = 'N' or NULL)
   - Not suspended (or cleared suspension)
3. Joins with defect data for quality information
4. Returns available lots with complete information for packing decisions

**Business Rules**:
- All filter parameters are optional (can search all available)
- Returns only completed inspections
- Excludes already packed lots (ISPACKED = 'Y')
- Includes suspended lots if cleared
- Shows defect information for quality assessment
- Ordered by inspection date (newest first typically)

**Search Scenarios**:
1. **By Date**: "Show all lots inspected today"
   - P_DATE = '2025-10-17', others NULL
2. **By Grade**: "Find all Grade A lots available"
   - P_GRADE = 'A', others NULL
3. **By Item**: "Search item X54321 for customer order"
   - P_ITMCODE = 'X54321', others NULL
4. **Specific Lot**: "Find inspection lot INS-001234"
   - P_INSLOT = 'INS-001234', others NULL
5. **Combined**: "Grade A of item X54321 from today"
   - All parameters specified

**Defect Information**:
- DF_CODE: Defect type code
- DF_AMOUNT: Number of defects found
- DF_POINT: Quality point deduction
- DEFECTFILENAME: Photo/image of defect
- Used to decide packing priority and customer suitability

**Packing Decision Criteria**:
---

## Related Procedures

**Downstream**: [291-PACK_INSPACKINGPALLETDETAIL.md](./291-PACK_INSPACKINGPALLETDETAIL.md) - Packs found lots
**Related**: INS_SEARCHINSPECTIONDATA - Similar inspection search
**Upstream**: Inspection process completes lots

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_SEARCHINSPECTIONDATA()`
**Lines**: Likely in search section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_SEARCHINSPECTIONDATA(PACK_SEARCHINSPECTIONDATAParameter para)`
**Lines**: 2116-2167

**Return Structure** (37 columns - comprehensive inspection data):
**Typical Query Logic**:
```sql
SELECT i.*, d.DF_CODE, d.DF_AMOUNT, d.DF_POINT
FROM tblInspection i
LEFT JOIN tblInspectionDefect d ON i.INSPECTIONLOT = d.INSPECTIONLOT
WHERE i.FINISHFLAG = 'Y'
  AND (i.ISPACKED = 'N' OR i.ISPACKED IS NULL)
  AND (:P_DATE IS NULL OR TRUNC(i.ENDDATE) = TO_DATE(:P_DATE, 'YYYY-MM-DD'))
  AND (:P_GRADE IS NULL OR i.GRADE = :P_GRADE)
  AND (:P_ITMCODE IS NULL OR i.ITEMCODE = :P_ITMCODE)
  AND (:P_INSLOT IS NULL OR i.INSPECTIONLOT = :P_INSLOT)
ORDER BY i.ENDDATE DESC
```

---

**File**: 294/296 | **Progress**: 99.3%
