# LAB_SEARCHLABMASSPRO

**Procedure Number**: 313 | **Module**: M14 - LAB | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search mass production (finished fabric) laboratory test records |
| **Operation** | SELECT |
| **Tables** | Lab mass production test tables |
| **Called From** | LABDataService.cs → LAB_SEARCHLABMASSPRO() |
| **Frequency** | High (daily search before customer shipment) |
| **Performance** | Fast (16 columns, indexed searches) |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVELOT` | VARCHAR2(50) | ⬜ | Weaving lot number filter |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code filter (finished product) |
| `P_FABRICTPE` | VARCHAR2(20) | ⬜ | Fabric type filter (PAB/DAB/SAB) |
| `P_SENDDATE` | VARCHAR2(20) | ⬜ | Send date filter (YYYY-MM-DD) |
| `P_TESTRESULT` | VARCHAR2(20) | ⬜ | Test result filter (PASS/FAIL/PENDING) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code (finished product) |
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number |
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot number |
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
| `FABRICTYPE` | VARCHAR2(20) | Fabric type (PAB/DAB/SAB/etc) |

---

## Business Logic (What it does and why)

Searches mass production finished fabric test records - the FINAL quality gate before customer shipment. Unlike greige testing (unfinished), this validates that all finishing processes completed correctly and final product meets customer specifications.

**Business Purpose**:
- **Shipment Gate**: NO fabric ships without passed mass production test
- **Customer Specifications**: Validates against customer-specific requirements
- **Certificate of Analysis**: Test data used for customer C of A documents
- **Quality Assurance**: Final verification after coating, scouring, drying, etc.

**Workflow**:
1. Finishing operator cuts finished fabric sample
2. Sample sent to lab with finishing lot info (SENDDATE)
3. Lab receives sample (RECEIVEDATE, RECEIVEBY, STATUS=RECEIVED)
4. Sample conditioned 24 hours (CONDITIONINGTIME, STATUS=CONDITIONING)
   - Standard: 23°C ± 2°C, 50% ± 5% RH
5. Lab performs comprehensive test suite (TESTDATE, TESTBY, STATUS=TESTING)
   - Dimensional, weight, tensile, tear, air permeability
   - Flammability, edge comb, stiffness, flex abrasion
   - All tests per customer specification
6. Results entered (TESTRESULT = PASS/FAIL/PENDING, STATUS=COMPLETED)
7. Supervisor approves (APPROVESTATUS, APPROVEBY, APPROVEDATE)
8. **Shipment Decision**: PASS + APPROVED = ship, else HOLD

**Search Use Cases**:
- Find all tests for specific weaving lot (traceability)
- Find failed tests for item code (quality investigation)
- Find all PAB fabric tests sent today (daily workflow)
- Find pending approvals (supervisor queue for shipment release)

**Key Business Rules**:
- All parameters optional for flexible searches
- WEAVINGLOT + FINISHINGLOT provide complete traceability
- TESTRESULT separate from APPROVESTATUS (test can pass, still need approval)
- FABRICTYPE indicates product category (PAB=Passenger, DAB=Driver, SAB=Side airbags)
- Failed test = automatic shipment hold
- Links to detailed 110+ column test data (LAB_SEARCHLABENTRYPRODUCTION)

---

## Related Procedures

**Sample Receipt**: [298-LAB_CHECKRECEIVESAMPLING.md](./298-LAB_CHECKRECEIVESAMPLING.md) - Records sample receipt
**Data Entry**: [308-LAB_SAVELABMASSPRORESULT.md](./308-LAB_SAVELABMASSPRORESULT.md) - Saves test results
**Detailed Data**: [311-LAB_SEARCHLABENTRYPRODUCTION.md](./311-LAB_SEARCHLABENTRYPRODUCTION.md) - Full 110+ column data
**Status Query**: [306-LAB_MASSPROSTOCKSTATUS.md](./306-LAB_MASSPROSTOCKSTATUS.md) - Status summary
**Update Status**: [315-LAB_UPDATEMASSPROSTOCK.md](./315-LAB_UPDATEMASSPROSTOCK.md) - Updates workflow status
**Approval Search**: [310-LAB_SEARCHAPPROVELAB.md](./310-LAB_SEARCHAPPROVELAB.md) - Similar approval workflow
**Greige Tests**: [312-LAB_SEARCHLABGREIGE.md](./312-LAB_SEARCHLABGREIGE.md) - Unfinished fabric tests
**Specifications**: [301-LAB_GETITEMTESTSPECIFICATION.md](./301-LAB_GETITEMTESTSPECIFICATION.md) - Gets test specs

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_SEARCHLABMASSPRO()`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_SEARCHLABMASSPRO(LAB_SEARCHLABMASSPROParameter para)`
**Lines**: 17785-17850
**Parameter Class**: Lines 2679-2710

---

**File**: 313/296 | **Progress**: 65.2%
