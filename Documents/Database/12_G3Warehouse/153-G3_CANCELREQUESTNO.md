# G3_CANCELREQUESTNO

**Procedure Number**: 153 | **Module**: G3 Warehouse (M12) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Cancel yarn/material issue request to remove pallet from request list |
| **Operation** | UPDATE |
| **Tables** | tblG3 (material tracking) |
| **Called From** | G3DataService.cs:993 → G3_CANCELREQUESTNO() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_REQUESTNO` | VARCHAR2(50) | ✅ | Request number to cancel |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID performing cancellation |

### Output (OUT)

None (empty result indicates success)

### Returns

Boolean: true if cancellation successful, false on failure

---

## Business Logic (What it does and why)

**Purpose**: Cancel material issue request to remove pallets from a pending request before it's processed.

**When Used**:
- Operator created wrong issue request
- Material requirements changed before issuing
- Need to reorganize pallet assignments
- Correct request number before issuing materials
- Remove pallets from request that shouldn't be issued

**Business Rules**:
1. Both P_REQUESTNO and P_OPERATOR required
2. Request must exist in system
3. Cancels entire request (all pallets)
4. Records who cancelled for audit trail
5. Only pending/not-yet-issued requests can be cancelled

**Warehouse Request Workflow**:

**Normal Flow** (without cancellation):
1. Operator creates issue request
2. Assigns pallets to request
3. Request submitted for processing
4. Materials physically issued
5. Request completed

**Cancellation Flow**:
1. Operator reviews pending request
2. Identifies error or change needed
3. Clicks "Cancel Request" button
4. System calls G3_CANCELREQUESTNO
5. Request and pallet assignments removed
6. Can create new request if needed

**Why Cancellation Needed**:
- **Wrong Request Number**: Typo in request number
- **Wrong Material**: Selected incorrect yarn/material type
- **Wrong Quantity**: Need to adjust pallet count
- **Order Change**: Production plan changed before issuing
- **Data Entry Error**: Need to re-enter correctly

**Audit Trail**:
- System records P_OPERATOR who cancelled
- Maintains history of cancelled requests
- Prevents unauthorized cancellations
- Tracks why and when cancellation occurred

**Example Scenario**:
1. Operator creates request REQ-001 for 10 pallets
2. Assigns pallets P001-P010 to request
3. Realizes should be 8 pallets, not 10
4. Cancels REQ-001 via G3_CANCELREQUESTNO
5. Creates new request REQ-002 with correct 8 pallets
6. Processes new request normally

---

## Related Procedures

**Related**: [154-G3_Delete.md](./154-G3_Delete.md) - Delete individual pallet (different from cancel request)
**Related**: G3_GETREQUESTNODETAIL (not yet documented) - Get request details
**Used With**: G3_INSERTUPDATEISSUEYARN (not yet documented) - Create issue requests

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_CANCELREQUESTNO(string P_REQUESTNO, string P_OPERATOR)`
**Lines**: 993-1028
**Comment**: "เพิ่ม G3_CANCELREQUESTNO" (Translation: "Added G3_CANCELREQUESTNO")

**Validation Logic**:
- Line 997: Validates P_REQUESTNO not null/empty
- Line 1000: Validates P_OPERATOR not null/empty
- Line 1003: Checks database connection

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_CANCELREQUESTNO(G3_CANCELREQUESTNOParameter para)`
**Lines**: 25518-25544

**Parameter Class**: Lines 7088-7092 (2 parameters)
**Result Class**: Lines 7098-7100 (empty result class)

**Usage**:
- EditIssueRawMaterialPage.xaml.cs:191 - Cancel request button click
- Context: G3 warehouse module for material issuing

**Table Operations**:
- **UPDATE**: tblG3 - Cancel request, clear request number from pallets
- **Condition**: WHERE REQUESTNO = P_REQUESTNO
- **Effect**: Removes pallet assignments from request

---

**File**: 153/296 | **Progress**: 51.7%
