# G3_GETREQUESTNODETAIL

**Procedure Number**: 158 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve all pallet details for an existing issue request number |
| **Operation** | SELECT |
| **Tables** | tblG3IssueRequest, tblG3IssueDetail |
| **Called From** | G3DataService.cs:757 → G3_GETREQUESTNODETAIL() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_REQUESTNO` | VARCHAR2(50) | ✅ | Issue request number to retrieve |

### Output (OUT)

None

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ISSUEDATE` | DATE | Issue date/time |
| `PALLETNO` | VARCHAR2(50) | Pallet number |
| `TRACENO` | VARCHAR2(50) | Trace number |
| `WEIGHT` | NUMBER | Weight quantity issued (kg) |
| `CH` | NUMBER | Cone/cheese count |
| `ISSUEBY` | VARCHAR2(50) | Operator who issued |
| `ISSUETO` | VARCHAR2(50) | Destination (machine/department) |
| `REQUESTNO` | VARCHAR2(50) | Request number |
| `PALLETTYPE` | VARCHAR2(50) | Pallet type |
| `DELETEFLAG` | VARCHAR2(10) | Deletion flag (Y/N) |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2(50) | Last editor |
| `REMARK` | VARCHAR2(500) | Remarks/notes |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `LOTNO` | VARCHAR2(50) | Lot number |
| `YARNTYPE` | VARCHAR2(50) | Yarn type |
| `ITM400` | VARCHAR2(50) | AS400 item code |
| `ENTRYDATE` | DATE | Entry date |
| `PACKAING` | VARCHAR2(50) | Packaging inspection result |
| `CLEAN` | VARCHAR2(50) | Cleanliness inspection result |
| `FALLDOWN` | VARCHAR2(50) | Fall down inspection result |
| `TEARING` | VARCHAR2(50) | Tearing inspection result |

---

## Business Logic (What it does and why)

**Purpose**: Loads all pallet details for an existing issue request. Used when viewing, editing, or reprinting an issue request slip. This allows operators to see complete history of what materials were issued to production.

**When Used**:
- Viewing existing issue request details
- Editing/modifying issue request before completion
- Reprinting issue slip
- Auditing material issues
- Checking what was issued to specific machine
- Investigating material usage discrepancies

**Business Rules**:
1. **Request-Based Query**: Returns all pallets associated with one request number
2. **Multiple Pallets**: One request can have multiple pallets issued
3. **Edit History**: Includes edit date/by for audit trail
4. **Delete Flag**: Shows if items have been marked for deletion (soft delete)
5. **Quality Data**: Includes all inspection results for traceability

**Workflow**:

**Scenario 1: View Existing Issue Request**
1. Supervisor needs to check yesterday's issues
2. Searches for request "REQ-2024-0015"
3. System calls G3_GETREQUESTNODETAIL(P_REQUESTNO = "REQ-2024-0015")
4. Returns 3 pallets issued under this request:
   - Pallet 1: PLT-001234, 500kg, to WV-001
   - Pallet 2: PLT-001235, 450kg, to WV-001
   - Pallet 3: PLT-001236, 480kg, to WV-001
5. Total: 1430kg issued to weaving machine WV-001
6. Supervisor reviews and confirms correct

**Scenario 2: Edit Issue Request (Before Completion)**
1. Operator creates request REQ-2024-0020
2. Adds 2 pallets to request
3. Realizes wrong pallet scanned
4. Calls G3_GETREQUESTNODETAIL to load current items
5. Shows 2 pallets with edit information:
   - Created by: OP123
   - Created date: 2024-01-15 08:30
   - Edit by: (null - not edited yet)
6. Operator deletes wrong pallet
7. Adds correct pallet
8. Completes request

**Scenario 3: Reprint Issue Slip**
1. Production lost issue slip REQ-2024-0018
2. Warehouse clerk searches for request number
3. System loads complete request details
4. Returns all pallets with:
   - ISSUEDATE = 2024-01-14 14:00
   - ISSUEBY = OP125
   - ISSUETO = WV-005
   - All pallet details, weights, lot numbers
5. System regenerates and reprints issue slip
6. Production receives duplicate slip

**Scenario 4: Audit Material Usage**
1. Production manager investigates yarn consumption
2. Queries all issues for machine WV-001 this week
3. For each request, calls G3_GETREQUESTNODETAIL
4. Returns complete history:
   - Each pallet issued
   - Weights and lot numbers
   - Quality inspection results
   - Edit history (who modified, when)
5. Manager verifies against production records

**Scenario 5: View Deleted Items**
1. Supervisor reviews request REQ-2024-0025
2. Procedure returns 4 pallets
3. One pallet has DELETEFLAG = "Y":
   - Shows it was removed from request
   - Edit date/by shows who removed it
   - Remark explains why removed
4. Only 3 pallets actually issued
5. Complete audit trail maintained

**Data Transformation**:
- Procedure returns `G3_GETREQUESTNODETAILResult` objects
- DataService transforms to `G3_GETREQUESTNODETAIL` model
- Adds RowNo for grid display
- Sets NewData = false (existing records)

**Why This Matters**:
- **Request History**: Complete view of all materials in an issue request
- **Edit Audit Trail**: Tracks who modified request and when
- **Quality Traceability**: Inspection results maintained with issue history
- **Reprinting**: Enables accurate duplicate slip generation
- **Investigation**: Supports material usage analysis and discrepancy resolution
- **Soft Delete Tracking**: Deleted items remain visible for audit

---

## Related Procedures

**Upstream**:
- G3_INSERTUPDATEISSUEYARN (creates issue requests)
- G3_GETPALLETDETAIL (selects pallets to add to request)

**Downstream**:
- G3_UPDATEYARN (updates stock when request finalized)
- G3_SEARCHYARNSTOCK (checks remaining stock)

**Similar**:
- G3_GETPALLETDETAIL (gets available pallets - similar result structure)
- G3_SEARCHBYPALLETNO (searches by pallet instead of request)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_GETREQUESTNODETAIL(string REQUESTNO)`
**Lines**: 757-815

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_GETREQUESTNODETAIL(G3_GETREQUESTNODETAILParameter para)`
**Lines**: 6939-6973 (Parameter: 6939-6942, Result: 6948-6972)

---

**File**: 158/296 | **Progress**: 53.4%
