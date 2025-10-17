# LAB_GREIGESTOCKSTATUS

**Procedure Number**: 305 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get greige sample stock status and test results |
| **Operation** | SELECT |
| **Tables** | tblLabGreigeStock, tblLabGreigeTest (joined - assumed) |
| **Called From** | LABDataService.cs → LAB_GREIGESTOCKSTATUS() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ⬜ | Beam roll number filter (optional) |
| `P_LOOMNO` | VARCHAR2(50) | ⬜ | Loom machine number filter (optional) |
| `P_RECEIVEDATE` | VARCHAR2(20) | ⬜ | Receive date filter (optional) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERROLL` | VARCHAR2(50) | Beam roll number |
| `LOOMNO` | VARCHAR2(50) | Loom machine number |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `RECEIVEDATE` | DATE | Lab receive date |
| `RECEIVEBY` | VARCHAR2(50) | Operator who received sample |
| `STATUS` | VARCHAR2(10) | Sample status (RECEIVED/TESTING/TESTED) |
| `CONDITIONINGTIME` | NUMBER | Conditioning time (hours) before testing |
| `TESTDATE` | DATE | Test completion date |
| `TESTRESULT` | VARCHAR2(10) | Test result (PASS/FAIL) |
| `REMARK` | VARCHAR2(500) | Test remarks/notes |
| `TESTBY` | VARCHAR2(50) | Lab technician who tested |
| `APPROVESTATUS` | VARCHAR2(10) | Approval status (APPROVED/REJECTED/PENDING) |
| `APPROVEBY` | VARCHAR2(50) | Lab supervisor who approved |
| `SENDDATE` | DATE | Sample send date to lab |
| `APPROVEDATE` | DATE | Approval date |
| `TESTNO` | NUMBER | Test sequence number |

---

## Business Logic (What it does and why)

Retrieves greige sample inventory status including receive status, test results, and approval workflow. Used for lab workload management and sample tracking.

**Workflow**:
1. Accepts optional filter criteria:
   - Beam roll number
   - Loom number
   - Receive date
2. Queries greige sample stock with complete status
3. Returns sample inventory including:
   - Sample identification
   - Receive and test tracking
   - Test results and approval status
   - Conditioning time (critical for fabric testing)
4. Used for lab management and quality reporting

**Business Rules**:
- All filter parameters optional (can list all greige samples)
- STATUS tracks sample workflow stage
- CONDITIONINGTIME required before testing (fabric must stabilize)
- APPROVESTATUS indicates final disposition

**Sample Status Workflow**:
```
RECEIVED → TESTING → TESTED → APPROVED/REJECTED
   ↓         ↓         ↓           ↓
Receive   Start     Complete   Supervisor
Sample    Test      Test       Decision
```

**Conditioning Time Requirement**:
Greige fabric must be conditioned before testing:
- Standard: 24 hours at controlled temperature/humidity
- Allows fabric to stabilize after weaving
- CONDITIONINGTIME tracked to ensure compliance
- Testing before conditioning = invalid results

**Status Values**:
- **RECEIVED**: Sample in lab, conditioning
- **TESTING**: Currently being tested
- **TESTED**: Test complete, awaiting approval
- **APPROVED/REJECTED**: Final disposition

**Lab Workload Dashboard**:
**Quality Investigation**:
**Approval Workflow**:
---

## Related Procedures

**Related**: [304-LAB_GETWEAVINGSAMPLING.md](./304-LAB_GETWEAVINGSAMPLING.md) - Gets greige sample details
**Related**: LAB_MASSPROSTOCKSTATUS - Similar status for finished fabric samples
**Upstream**: [297-LAB_CHECKRECEIVEGREIGESAMPLING.md](./297-LAB_CHECKRECEIVEGREIGESAMPLING.md) - Checks if sample received
**Downstream**: LAB_UPDATEGREIGESTOCK - Updates sample status

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GREIGESTOCKSTATUS()`
**Lines**: Likely in greige inventory section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GREIGESTOCKSTATUS(LAB_GREIGESTOCKSTATUSParameter para)`
**Lines**: 3991-4020

**Return Structure** (16 columns):
**Typical Query Logic**:
```sql
SELECT g.BEAMERROLL, g.LOOMNO, g.ITM_WEAVING,
       g.RECEIVEDATE, g.RECEIVEBY, g.STATUS,
       g.CONDITIONINGTIME, g.SENDDATE,
       t.TESTDATE, t.TESTRESULT, t.TESTBY, t.TESTNO,
       t.REMARK, t.APPROVESTATUS, t.APPROVEBY, t.APPROVEDATE
FROM tblLabGreigeStock g
LEFT JOIN tblLabGreigeTest t ON g.BEAMERROLL = t.BEAMERROLL
                             AND g.LOOMNO = t.LOOMNO
WHERE (:P_BEAMERROLL IS NULL OR g.BEAMERROLL = :P_BEAMERROLL)
  AND (:P_LOOMNO IS NULL OR g.LOOMNO = :P_LOOMNO)
  AND (:P_RECEIVEDATE IS NULL OR TRUNC(g.RECEIVEDATE) = TO_DATE(:P_RECEIVEDATE, 'YYYY-MM-DD'))
ORDER BY g.RECEIVEDATE DESC, g.BEAMERROLL
```

---

**File**: 305/296 | **Progress**: 103.0%
