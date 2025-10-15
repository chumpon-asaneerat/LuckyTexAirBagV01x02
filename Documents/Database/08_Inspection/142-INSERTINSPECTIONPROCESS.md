# INSERTINSPECTIONPROCESS

**Procedure Number**: 142 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert new inspection lot record to start inspection process |
| **Operation** | INSERT |
| **Tables** | tblINSLot |
| **Called From** | DataServicecs.cs:716 → InsertInspectionProcess() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code |
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number (generated) |
| `P_MCNO` | VARCHAR2(50) | ✅ | Inspection machine number |
| `P_PRODUCTTYPEID` | VARCHAR2(50) | ⬜ | Product type identifier |
| `P_CUSTOMERID` | VARCHAR2(50) | ⬜ | Customer ID |
| `P_CUSTOMERTYPE` | VARCHAR2(50) | ⬜ | Customer type |
| `P_PEINSPECTIONLOT` | VARCHAR2(50) | ⬜ | Previous/PE inspection lot (for re-inspection) |
| `P_STARTDATE` | DATE | ⬜ | Inspection start date/time |
| `P_INSPECTEDBY` | VARCHAR2(50) | ✅ | Inspector operator ID |
| `P_RETEST` | VARCHAR2(10) | ⬜ | Retest flag (Y/N) |
| `P_LOADTYPE` | VARCHAR2(50) | ⬜ | Loading type |
| `P_GROUP` | VARCHAR2(50) | ⬜ | Operator group |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_INSID` | VARCHAR2(50) | Generated inspection ID (primary key) |

### Returns

Returns inspection ID string if successful, empty string on failure.

---

## Business Logic (What it does and why)

**Purpose**: Create new inspection lot record when operator starts real-time inspection process.

**When Used**:
- Operator starts new inspection on inspection machine
- After finishing process completes
- Beginning of quality inspection workflow
- Creates inspection lot linked to finishing lot

**Business Rules**:
1. Required: P_FINISHLOT, P_ITMCODE, P_INSLOT, P_MCNO, P_INSPECTEDBY
2. Generates and returns R_INSID (inspection primary key)
3. Links to finishing lot for traceability
4. P_PEINSPECTIONLOT links previous inspection (if re-inspection)
5. P_RETEST flag indicates if this is retest/re-inspection
6. Different from INS_INSERTMANUALINSPECTDATA (this is real-time start, not complete data entry)

**Real-Time Inspection Start**:
- Operator scans finishing lot barcode
- System loads item/customer information
- Generates new inspection lot number
- Calls INSERTINSPECTIONPROCESS with start parameters
- Returns inspection ID
- Inspection session begins (defects/tests collected progressively)
- Later finalized with UPDATEINSPECTIONPROCESS

**vs Manual Entry**:
- **INSERTINSPECTIONPROCESS**: Start inspection → collect data → finish (real-time)
- **INS_INSERTMANUALINSPECTDATA**: Enter complete lot data at once (backdating)

---

## Related Procedures

**Related**: [137-INS_INSERTMANUALINSPECTDATA.md](./137-INS_INSERTMANUALINSPECTDATA.md) - Manual/backdated entry
**Downstream**: UPDATEINSPECTIONPROCESS - Finalize inspection with measurements/grade
**Downstream**: [135-INS_GETTOTALDEFECTBYINSLOT.md](./135-INS_GETTOTALDEFECTBYINSLOT.md) - Count defects
**Downstream**: INSTINSPECTIONLOTDEFECT - Add defects during inspection

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `InsertInspectionProcess(string finLot, string itemCode, string insLot, string mcNo, string producttypeId, string customerId, string customerType, string peInsLot, DateTime insDate, string insBy, string reTest, string LOADTYPE, string GROUP)`
**Lines**: 716-774
**Comment**: "Insert Inspection record when Start button press"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INSERTINSPECTIONPROCESS(INSERTINSPECTIONPROCESSParameter para)`

**Parameter Class**: 13 parameters
**Result Class**: Returns R_INSID

---

**File**: 142/296 | **Progress**: 48.0%
