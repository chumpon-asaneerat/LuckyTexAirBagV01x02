# INS_INSERTMANUALINSPECTDATA

**Procedure Number**: 137 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert inspection lot record manually (offline/backdated inspection entry) |
| **Operation** | INSERT |
| **Tables** | tblINSLot |
| **Called From** | DataServicecs.cs:2929 → INS_INSERTMANUALINSPECTDATA() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code |
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number |
| `P_STARTDATE` | DATE | ⬜ | Inspection start date/time |
| `P_ENDDATE` | DATE | ⬜ | Inspection end date/time |
| `P_CUSTOMERID` | VARCHAR2(50) | ⬜ | Customer ID |
| `P_PRODUCTTYPEID` | VARCHAR2(50) | ⬜ | Product type |
| `P_INSPECTEDBY` | VARCHAR2(50) | ⬜ | Inspector operator ID |
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number |
| `P_CUSTOMERTYPE` | VARCHAR2(50) | ⬜ | Customer type |
| `P_LOADTYPE` | VARCHAR2(50) | ⬜ | Loading type |
| `P_GLENGHT` | NUMBER | ⬜ | Gross length (meters) |
| `P_NLENGTH` | NUMBER | ⬜ | Net length (meters) |
| `P_GRADE` | VARCHAR2(10) | ⬜ | Quality grade |
| `P_GWEIGHT` | NUMBER | ⬜ | Gross weight (kg) |
| `P_NWEIGHT` | NUMBER | ⬜ | Net weight (kg) |
| `P_REMARK` | VARCHAR2(200) | ⬜ | Remarks |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator who enters data |
| `P_GROUP` | VARCHAR2(50) | ⬜ | Operator group |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_INSID` | VARCHAR2(50) | Generated inspection ID (primary key) |

### Returns

Returns inspection ID string if successful, empty string on failure.

---

## Business Logic (What it does and why)

**Purpose**: Insert complete inspection lot record manually for offline/backdated inspection entry.

**When Used**:
- Backdating inspection records
- Manual entry of inspection data not captured during real-time inspection
- Data recovery or correction scenarios
- Offline inspection documentation

**Business Rules**:
1. Required fields: P_INSLOT, P_ITMCODE, P_FINISHLOT, P_MCNO, P_OPERATOR
2. Accepts complete inspection data (all measurements, dates, grades)
3. Returns generated inspection ID (R_INSID) as primary key
4. Different from normal inspection start (this enters complete lot data at once)

**Manual Entry vs Real-Time Inspection**:
- **Real-time**: Start inspection → collect data progressively → finish
- **Manual**: Enter all data at once (start date, end date, measurements, grade, etc.)
- Used when inspection was done but not recorded in system
- Backdating capability for data reconciliation

**Usage Scenarios**:
1. System downtime - inspection completed offline
2. Data entry from paper records
3. Migrating historical inspection data
4. Correcting incomplete records

---

## Related Procedures

**Opposite**: Normal inspection flow (INSERTINSPECTIONPROCESS)
**Related**: UPDATEINSPECTIONPROCESS - Update existing inspection lot
**Related**: INS_SEARCHINSPECTIONDATA - Search existing records

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `INS_INSERTMANUALINSPECTDATA(string P_INSLOT, string P_ITMCODE, string P_FINISHLOT, DateTime? P_STARTDATE, DateTime? P_ENDDATE, string P_CUSTOMERID, string P_PRODUCTTYPEID, string P_INSPECTEDBY, string P_MCNO, string P_CUSTOMERTYPE, string P_LOADTYPE, Decimal? P_GLENGHT, Decimal? P_NLENGTH, string P_GRADE, Decimal? P_GWEIGHT, Decimal? P_NWEIGHT, string P_REMARK, string P_OPERATOR, string GROUP)`
**Lines**: 2929-2992
**Comment**: "Insert Inspection record when Start button press" (for manual entry)

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_INSERTMANUALINSPECTDATA(INS_INSERTMANUALINSPECTDATAParameter para)`

**Parameter Class**: 19 parameters
**Result Class**: Returns R_INSID

---

**File**: 137/296 | **Progress**: 46.3%
