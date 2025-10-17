# LAB_SEARCHLABGREIGE

**Procedure Number**: 312 | **Module**: M14 - LAB | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search greige (unfinished) fabric laboratory test records |
| **Operation** | SELECT |
| **Tables** | Lab greige test tables |
| **Called From** | LABDataService.cs → LAB_SEARCHLABGREIGE() |
| **Frequency** | Medium (daily search by lab and production staff) |
| **Performance** | Fast (17 columns, indexed searches) |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ⬜ | Beam roll number filter |
| `P_LOOM` | VARCHAR2(50) | ⬜ | Loom machine number filter |
| `P_ITMWEAVE` | VARCHAR2(50) | ⬜ | Weaving item code filter |
| `P_SETTINGDATE` | VARCHAR2(20) | ⬜ | Setting date filter (YYYY-MM-DD) |
| `P_SENDDATE` | VARCHAR2(20) | ⬜ | Send date filter (YYYY-MM-DD) |
| `P_TESTRESULT` | VARCHAR2(20) | ⬜ | Test result filter (PASS/FAIL/PENDING) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERROLL` | VARCHAR2(50) | Beam roll number/identifier |
| `LOOMNO` | VARCHAR2(50) | Loom machine number |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `TESTNO` | NUMBER | Test sequence/serial number |
| `RECEIVEDATE` | DATE | Sample receive date/time at lab |
| `RECEIVEBY` | VARCHAR2(50) | Employee ID who received sample |
| `STATUS` | VARCHAR2(20) | Sample status (RECEIVED/CONDITIONING/TESTING/COMPLETED) |
| `CONDITIONINGTIME` | DATE | Conditioning start time |
| `TESTDATE` | DATE | Actual test date/time |
| `TESTRESULT` | VARCHAR2(20) | Test result (PASS/FAIL/PENDING) |
| `REMARK` | VARCHAR2(500) | Test remarks or notes |
| `TESTBY` | VARCHAR2(50) | Employee ID who performed test |
| `APPROVESTATUS` | VARCHAR2(20) | Approval status (PENDING/APPROVED/REJECTED) |
| `APPROVEBY` | VARCHAR2(50) | Employee ID who approved/rejected |
| `SENDDATE` | DATE | Date sample sent to lab |
| `APPROVEDATE` | DATE | Approval date/time |
| `SETTINGDATE` | DATE | Loom setting date (production date) |

---

## Business Logic (What it does and why)

Searches greige (grey/unfinished) fabric test records to track samples from weaving through laboratory testing workflow. Used for early detection of quality issues before expensive finishing operations.

**Business Purpose**:
- **Early Quality Gate**: Catch defects before finishing process (cost savings)
- **Loom Performance**: Track which looms produce quality issues
- **Yarn Quality**: Identify yarn-related defects early
- **Production Planning**: Failed greige tests affect finishing schedule

**Workflow**:
1. Weaving operator cuts greige sample from loom
2. Sample sent to lab with beam roll information (SENDDATE)
3. Lab receives sample (RECEIVEDATE, RECEIVEBY, STATUS=RECEIVED)
4. Sample conditioned 24 hours (CONDITIONINGTIME, STATUS=CONDITIONING)
5. Lab performs tests (TESTDATE, TESTBY, STATUS=TESTING)
6. Results entered (TESTRESULT = PASS/FAIL/PENDING, STATUS=COMPLETED)
7. Supervisor approves (APPROVESTATUS, APPROVEBY, APPROVEDATE)

**Search Use Cases**:
- Find all tests for specific beam roll (track sample)
- Find failed tests for loom (identify loom problems)
- Find tests sent on specific date (daily workflow management)
- Find pending approvals (supervisor queue)

**Key Business Rules**:
- All parameters optional for flexible searches
- TESTRESULT separate from APPROVESTATUS (test can pass but need approval)
- CONDITIONINGTIME critical (24hr standard for environmental equilibration)
- STATUS tracks sample through 4-stage workflow
- Failed greige = hold fabric from finishing

---

## Related Procedures

**Sample Receipt**: [297-LAB_CHECKRECEIVEGREIGESAMPLING.md](./297-LAB_CHECKRECEIVEGREIGESAMPLING.md) - Records sample receipt
**Data Entry**: [307-LAB_SAVELABGREIGERESULT.md](./307-LAB_SAVELABGREIGERESULT.md) - Saves greige test results
**Status Query**: [305-LAB_GREIGESTOCKSTATUS.md](./305-LAB_GREIGESTOCKSTATUS.md) - Gets status summary
**Update Status**: [314-LAB_UPDATEGREIGESTOCK.md](./314-LAB_UPDATEGREIGESTOCK.md) - Updates workflow status
**Sample Source**: [304-LAB_GETWEAVINGSAMPLING.md](./304-LAB_GETWEAVINGSAMPLING.md) - Gets weaving samples
**Finished Tests**: [313-LAB_SEARCHLABMASSPRO.md](./313-LAB_SEARCHLABMASSPRO.md) - Searches finished fabric tests

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_SEARCHLABGREIGE()`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_SEARCHLABGREIGE(LAB_SEARCHLABGREIGEParameter para)`
**Lines**: 17854-17922
**Parameter Class**: Lines 2716-2749

---

**File**: 312/296 | **Progress**: 64.9%
