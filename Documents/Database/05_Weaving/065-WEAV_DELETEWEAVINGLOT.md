# WEAV_DELETEWEAVINGLOT

**Procedure Number**: 065 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete entire weaving production lot (cancelled/rejected lot) |
| **Operation** | DELETE (CASCADE - multiple related tables) |
| **Tables** | tblWeavingLot, tblWeavingDetail, tblWeavingMachineStop, tblWeftYarn (assumed) |
| **Called From** | WeavingDataService.cs:1882 → WEAV_DELETEWEAVINGLOT() |
| **Frequency** | Low - Critical operation for cancelled production lots only |
| **Performance** | Medium - Cascade deletion affects multiple tables |
| **Issues** | 🔴 1 High - Requires audit trail and authorization control |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number to delete |
| `P_REMARK` | VARCHAR2(500) | ✅ | Reason for deletion (audit) |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | User performing deletion (audit) |

### Output (OUT)

None - Success determined by non-null result object

### Returns

Empty result object indicating success/failure status

---

## Database Operations

### Tables

**Primary Tables** (Cascade deletion):
- `tblWeavingLot` - DELETE - Main weaving lot header
- `tblWeavingDetail` - DELETE - Weaving production details (length, density, etc.)
- `tblWeavingMachineStop` - DELETE - All machine stop records for this lot
- `tblWeftYarn` - DELETE/UPDATE - Weft yarn consumption records
- `tblBeamUsage` - UPDATE - May need to reverse beam consumption
- `tblInventory` - UPDATE - May need to reverse inventory movements

**Audit Table** (Should be logged):
- `tblDeletionAudit` or similar - INSERT - Log deletion with reason and operator

**Transaction**: **MUST use transaction** - Multiple table operations require atomicity

### Data Integrity

```sql
-- Critical: Must use transaction for cascade deletion
BEGIN TRANSACTION;

-- Log deletion first (audit trail)
INSERT INTO tblDeletionAudit (LotNumber, DeletedBy, DeletedDate, Reason, LotType)
VALUES (P_WEAVINGLOT, P_OPERATOR, SYSDATE, P_REMARK, 'WEAVING');

-- Delete related records (order matters!)
DELETE FROM tblWeavingMachineStop WHERE WEAVINGLOT = P_WEAVINGLOT;
DELETE FROM tblWeavingDetail WHERE WEAVINGLOT = P_WEAVINGLOT;
DELETE FROM tblWeftYarn WHERE WEAVINGLOT = P_WEAVINGLOT;
DELETE FROM tblWeavingLot WHERE WEAVINGLOT = P_WEAVINGLOT;

-- Reverse inventory impact (if lot was in WIP)
UPDATE tblBeamUsage SET STATUS = 'AVAILABLE' WHERE WEAVINGLOT = P_WEAVINGLOT;

COMMIT;
```

---

## Business Logic (What it does and why)

**Purpose**: Completely removes a weaving production lot and all related data from the system, typically for cancelled or rejected lots that should not appear in production history.

**Business Context**:
- Weaving lots can be cancelled due to quality issues, setup errors, or production changes
- Complete deletion (vs. status change) is needed when lot should be completely erased from records
- This is a **critical operation** requiring supervisor/manager authorization
- Deletion affects traceability - all downstream processes must also be cancelled
- Audit trail is essential for compliance and quality management

**Usage Scenarios**:

**Scenario 1: Setup Error**
1. Operator starts weaving lot with wrong beam or item code
2. Mistake discovered immediately (no significant production yet)
3. Supervisor decides to delete lot completely and restart with correct setup
4. Enters reason: "Wrong beam loaded - setup error"
5. System deletes lot and frees up the beam for correct use

**Scenario 2: Quality Rejection**
1. Quality inspector finds critical defect affecting entire lot
2. Lot is rejected and cannot be used in any form
3. Manager authorizes complete deletion
4. Enters reason: "Critical quality defect - lot rejected by QA"
5. System removes lot from production records

**Scenario 3: Production Cancellation**
1. Customer cancels order, lot is only partially complete
2. Manager decides to cancel in-progress lot
3. Enters reason: "Customer order cancellation - SO#12345"
4. System deletes lot and returns beams to available inventory

**Business Rules**:
- ⚠️ **High-risk operation** - Should require supervisor/manager permission level
- Must validate lot status before deletion (should not delete completed/shipped lots)
- Must check for downstream processes (inspection, finishing) - cannot delete if used downstream
- Remark field is **mandatory** - must document why deletion occurred
- Operator name is **mandatory** - must track who authorized/performed deletion
- Should log to audit table **before** deletion (in case of rollback)

**Impact on Related Data**:
- Beam consumption reversed (beam becomes available again)
- Weft yarn usage records deleted
- Machine stop records deleted (affects OEE calculations - should recalculate)
- Production reports must exclude deleted lots
- Traceability chain is broken (beam → lot → fabric)

**When NOT to Use**:
- If lot has been inspected → Use status change instead
- If lot has been shipped → Cannot delete (integrity violation)
- If lot is referenced in downstream processes → Must cancel downstream first
- For simple data corrections → Use UPDATE procedures instead

---

## Related Procedures

**Before Deletion (Validation)**:
- [WEAV_GETWEAVELISTBYBEAMROLL](./WEAV_GETWEAVELISTBYBEAMROLL.md) - Check lot status
- [WEAV_WEAVINGINPROCESSLIST](./WEAV_WEAVINGINPROCESSLIST.md) - Verify lot is still in-process
- Should check: Finishing, Inspection, Packing status

**Related Deletion Operations**:
- [WEAV_DELETEMCSTOP](./WEAV_DELETEMCSTOP.md) - Delete individual machine stop (less severe)
- Similar DELETE procedures in other modules (BEAM_, WARP_, etc.)

**Audit/Logging**:
- Should call shared audit logging procedure
- May need to notify reporting system to recalculate metrics

**Inventory Impact**:
- May need to call inventory adjustment procedures
- Beam status update procedures

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`

**Current Implementation (3 parameters)**:
- **Method**: `WEAV_DELETEWEAVINGLOT(string P_WEAVINGLOT, string P_REMARK, string P_OPERATOR)`
- **Lines**: 1882-1913
- **Note**: This is the active version with audit parameters

**Legacy Implementation (1 parameter)** - ⚠️ Deprecated:
- **Method**: `WEAV_DELETEWEAVINGLOT(string P_WEAVINGLOT)`
- **Lines**: 1378-1407
- **Note**: Old version without audit trail - should not be used

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_DELETEWEAVINGLOT(WEAV_DELETEWEAVINGLOTParameter para)`
**Lines**: 13701-13729

**Stored Procedure Call**:
```csharp
// Current version with audit parameters
string[] paraNames = new string[]
{
    "P_WEAVINGLOT",  // Lot to delete
    "P_REMARK",      // Deletion reason (audit)
    "P_OPERATOR"     // User performing deletion (audit)
};

object[] paraValues = new object[]
{
    para.P_WEAVINGLOT,
    para.P_REMARK,
    para.P_OPERATOR
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_DELETEWEAVINGLOT",
    paraNames, paraValues);

// Success if no exception and result object created
result = new WEAV_DELETEWEAVINGLOTResult(); // Empty result = success
```

**Return Handling**:
```csharp
// Service layer returns bool
public bool WEAV_DELETEWEAVINGLOT(string P_WEAVINGLOT, string P_REMARK, string P_OPERATOR)
{
    bool result = false;

    // Validate required field
    if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
        return result;

    WEAV_DELETEWEAVINGLOTParameter dbPara = new WEAV_DELETEWEAVINGLOTParameter();
    dbPara.P_WEAVINGLOT = P_WEAVINGLOT;
    dbPara.P_REMARK = P_REMARK;
    dbPara.P_OPERATOR = P_OPERATOR;

    WEAV_DELETEWEAVINGLOTResult dbResult =
        DatabaseManager.Instance.WEAV_DELETEWEAVINGLOT(dbPara);

    result = (null != dbResult); // True if deletion succeeded

    return result;
}
```

**Usage Example**:
```csharp
// Delete weaving lot with audit information
WeavingDataService service = WeavingDataService.Instance;

// Get operator info
string operatorName = CurrentUser.EmployeeCode;

// Show confirmation dialog
DialogResult confirm = MessageBox.Show(
    "Are you sure you want to delete lot " + weavingLot + "?\n" +
    "This will delete all related data and cannot be undone!",
    "Confirm Deletion",
    MessageBoxButtons.YesNo,
    MessageBoxIcon.Warning
);

if (confirm == DialogResult.Yes)
{
    // Get deletion reason from user
    string remark = PromptForDeletionReason();

    if (!string.IsNullOrEmpty(remark))
    {
        bool success = service.WEAV_DELETEWEAVINGLOT(
            P_WEAVINGLOT: "WV-2024-001",
            P_REMARK: remark,
            P_OPERATOR: operatorName
        );

        if (success)
        {
            MessageBox.Show("Weaving lot deleted successfully.");
            RefreshProductionList();
            RecalculateOEE(); // Recalculate metrics
        }
        else
        {
            MessageBox.Show("Failed to delete weaving lot. It may be used in downstream processes.");
        }
    }
}
```

**Recommended UI Flow**:
```csharp
// Validation before allowing deletion
private bool CanDeleteWeavingLot(string weavingLot)
{
    // Check if lot exists and is deletable
    // 1. Verify lot status is IN_PROCESS (not COMPLETED/SHIPPED)
    // 2. Check no downstream processes (finishing, inspection, packing)
    // 3. Verify user has supervisor permission

    return isInProcess && !hasDownstream && hasSupervisorPermission;
}
```

---

**File**: 065/296 | **Progress**: 22.0%
