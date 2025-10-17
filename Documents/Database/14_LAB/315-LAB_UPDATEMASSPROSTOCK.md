# LAB_UPDATEMASSPROSTOCK

**Procedure Number**: 315 | **Module**: M14 - LAB | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update mass production (finished fabric) sample test stock/status |
| **Operation** | UPDATE |
| **Tables** | Lab mass production test stock table |
| **Called From** | LABDataService.cs → LAB_UPDATEMASSPROSTOCK() |
| **Frequency** | High (every workflow status change) |
| **Performance** | Fast (single row update by PK) |
| **Issues** | 🟡 1 Low (Typo: P_CONDITONTIME should be P_CONDITIONINGTIME) |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVELOT` | VARCHAR2(50) | ✅ | Weaving lot number (PK part 1) |
| `P_FINISHLOT` | VARCHAR2(50) | ✅ | Finishing lot number (PK part 2) |
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code (PK part 3) |
| `P_RECEIVEDATE` | DATE | ⬜ | Sample receive date/time |
| `P_RECEIVEBY` | VARCHAR2(50) | ⬜ | Employee ID who received |
| `P_STATUS` | VARCHAR2(20) | ⬜ | Sample status |
| `P_CONDITONTIME` | DATE | ⬜ | Conditioning start time (typo in name) |
| `P_TESTBY` | VARCHAR2(50) | ⬜ | Employee ID who tested |
| `P_APPROVESTATUS` | VARCHAR2(20) | ⬜ | Approval status |
| `P_APPROVEBY` | VARCHAR2(50) | ⬜ | Employee ID who approved |
| `P_APPROVEDATE` | DATE | ⬜ | Approval date/time |
| `P_REMARK` | VARCHAR2(500) | ⬜ | Test remarks/notes |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(100) | Operation result (SUCCESS / ERROR: message / NOT FOUND) |

---

## Business Logic (What it does and why)

Updates finished fabric sample status through laboratory testing workflow - the FINAL quality gate before customer shipment. Provides real-time tracking from sample receipt through conditioning, comprehensive testing, and final approval for shipment release.

**Business Purpose**:
- **Shipment Gate Control**: Approval status directly controls customer shipment authorization
- **Workflow Tracking**: Real-time visibility critical for on-time delivery commitments
- **Certificate of Analysis**: Status updates trigger C of A document generation
- **Quality Assurance**: Final verification after all finishing processes complete
- **Bottleneck Detection**: Identify delays that impact shipment schedules

**Workflow Stages** (STATUS field):
1. **RECEIVED**: Sample logged into lab (update: RECEIVEDATE, RECEIVEBY)
2. **CONDITIONING**: Sample in environmental chamber (update: P_CONDITONTIME)
   - Standard: 23°C ± 2°C, 50% ± 5% RH for 24 hours
3. **TESTING**: Comprehensive test suite in progress (update: TESTBY)
   - Dimensional, weight, tensile, tear, air permeability
   - Flammability, edge comb, stiffness, flex abrasion
   - All tests per customer specification
4. **COMPLETED**: All tests finished, awaiting final approval

**Approval Flow** (APPROVESTATUS field):
- **PENDING**: Awaiting quality supervisor review
- **APPROVED**: Supervisor approved - FABRIC CLEARED FOR SHIPMENT
- **REJECTED**: Supervisor rejected - SHIPMENT HOLD, investigation required

**Update Patterns**:
- **Partial Updates**: Only provided parameters updated (flexible)
- **Status Independent**: Can update STATUS without APPROVESTATUS
- **Remark Anytime**: Can add remarks at any workflow stage
- **Validation**: Sample must exist (WEAVELOT + FINISHLOT + ITMCODE)

**Typical Update Sequence**:
1. Receipt: STATUS=RECEIVED, RECEIVEDATE, RECEIVEBY
2. Conditioning: STATUS=CONDITIONING, P_CONDITONTIME
3. Testing: STATUS=TESTING, TESTBY
4. Completion: STATUS=COMPLETED
5. Approval: APPROVESTATUS=APPROVED, APPROVEBY, APPROVEDATE → **SHIPMENT AUTHORIZED**

**Business Rules**:
- WEAVELOT + FINISHLOT + ITMCODE uniquely identify sample (composite PK)
- NO shipment without APPROVESTATUS=APPROVED
- Dates must be chronologically logical
- Employee IDs must be valid system users
- Invalid STATUS/APPROVESTATUS returns error
- Sample not found returns "NOT FOUND"
- Failed test = automatic shipment hold

**Critical for Business**:
- Delays in approval = delayed customer shipments = customer complaints
- Accurate status tracking essential for production planning
- Integration with shipping system based on approval status

---

## Related Procedures

**Sample Receipt**: [298-LAB_CHECKRECEIVESAMPLING.md](./298-LAB_CHECKRECEIVESAMPLING.md) - Creates initial record
**Search**: [313-LAB_SEARCHLABMASSPRO.md](./313-LAB_SEARCHLABMASSPRO.md) - Searches records to update
**Test Results**: [308-LAB_SAVELABMASSPRORESULT.md](./308-LAB_SAVELABMASSPRORESULT.md) - Saves detailed results
**Status Query**: [306-LAB_MASSPROSTOCKSTATUS.md](./306-LAB_MASSPROSTOCKSTATUS.md) - Gets current status
**Greige Update**: [314-LAB_UPDATEGREIGESTOCK.md](./314-LAB_UPDATEGREIGESTOCK.md) - Same logic for greige fabric
**Approval Search**: [310-LAB_SEARCHAPPROVELAB.md](./310-LAB_SEARCHAPPROVELAB.md) - Approval workflow search

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_UPDATEMASSPROSTOCK()`

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_UPDATEMASSPROSTOCK(LAB_UPDATEMASSPROSTOCKParameter para)`
**Lines**: 17677-17727
**Parameter Class**: Lines 2621-2644

---

**File**: 315/296 | **Progress**: 65.9%
