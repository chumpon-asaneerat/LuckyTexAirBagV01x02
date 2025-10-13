# WEAV_DELETEMCSTOP

**Procedure Number**: 064 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete machine stop record from weaving production |
| **Operation** | DELETE |
| **Tables** | tblWeavingMachineStop (assumed) |
| **Called From** | WeavingDataService.cs:1919 → WEAV_DELETEMCSTOP() |
| **Frequency** | Low - Manual correction when stop record entered incorrectly |
| **Performance** | Fast - Single record deletion by composite key |
| **Issues** | 🟡 1 Low - No transaction handling visible |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom/weaving machine number |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll) |
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll number |
| `P_DEFECT` | VARCHAR2(50) | ✅ | Defect code causing the stop |
| `P_DATE` | DATE | ✅ | Date/time of the machine stop |

### Output (OUT)

None - Success determined by non-null result object

### Returns

Empty result object indicating success/failure status

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWeavingMachineStop` - DELETE - Machine stop tracking table (assumed)

**Transaction**: Should use transaction (data integrity concern)

### Composite Key

The deletion requires a composite key of 5 fields to uniquely identify the stop record:
- LOOMNO + DOFFNO + BEAMROLL + DEFECT + DATE

This ensures only the specific stop event is deleted, not other stops on the same machine.

---

## Business Logic (What it does and why)

**Purpose**: Removes incorrectly recorded machine stop events from weaving production history.

**Business Context**:
- During weaving operations, operators record every machine stop with cause (defect code)
- If a stop is recorded incorrectly (wrong defect code, wrong time, duplicate entry), supervisor can delete it
- Machine stop data is critical for OEE (Overall Equipment Effectiveness) calculations
- Incorrect stop records skew downtime analysis and machine performance metrics

**Usage Scenario**:
1. Operator accidentally records a machine stop with wrong defect code
2. Supervisor reviews stop history and identifies the error
3. Supervisor opens machine stop management screen
4. Selects the incorrect stop record from the list
5. Clicks "Delete" button
6. System calls WEAV_DELETEMCSTOP with all identifying parameters
7. Stop record is removed from database
8. OEE and downtime calculations are recalculated without the erroneous data

**Business Rules**:
- Requires complete composite key (all 5 parameters) to prevent accidental mass deletion
- Should require supervisor authorization (not operator level)
- Should log the deletion action for audit trail
- May need to recalculate production efficiency metrics after deletion

**Data Integrity Concerns**:
- ⚠️ No visible transaction handling in C# code
- Should verify the stop record exists before attempting deletion
- Should check if stop record has been used in reports (may need soft delete instead)
- Should prevent deletion of stops from completed/closed production lots

**Related Data Impact**:
- OEE calculations need recalculation after stop deletion
- Downtime reports may need refresh
- Machine performance metrics affected

---

## Related Procedures

**Upstream (Machine Stop Management)**:
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Create machine stop record
- [WEAV_GETMCSTOPLISTBYDOFFNO](./WEAV_GETMCSTOPLISTBYDOFFNO.md) - List stops to identify record to delete
- [WEAV_GETMCSTOPBYLOT](./WEAV_GETMCSTOPBYLOT.md) - View stop history by lot

**Downstream (After Deletion)**:
- May trigger recalculation of production metrics
- May need to refresh dashboard displays

**Similar Operations**:
- [WEAV_DELETEWEAVINGLOT](./WEAV_DELETEWEAVINGLOT.md) - Delete entire weaving lot (more severe)
- Other module DELETE operations for data correction

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_DELETEMCSTOP(string P_LOOMNO, decimal? P_DOFFNO, string P_BEAMROLL, string P_DEFECT, DateTime? P_DATE)`
**Lines**: 1919-1949

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_DELETEMCSTOP(WEAV_DELETEMCSTOPParameter para)`
**Lines**: 13735-13767

**Stored Procedure Call**:
```csharp
// All 5 parameters required for composite key identification
string[] paraNames = new string[]
{
    "P_LOOMNO",    // Machine number
    "P_DOFFNO",    // Roll number
    "P_BEAMROLL",  // Beam identifier
    "P_DEFECT",    // Defect code
    "P_DATE"       // Stop timestamp
};

object[] paraValues = new object[]
{
    para.P_LOOMNO,
    para.P_DOFFNO,
    para.P_BEAMROLL,
    para.P_DEFECT,
    para.P_DATE
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_DELETEMCSTOP",
    paraNames, paraValues);

// Success if no exception and result object created
result = new WEAV_DELETEMCSTOPResult(); // Empty result = success
```

**Return Handling**:
```csharp
// Service layer returns bool
public bool WEAV_DELETEMCSTOP(...)
{
    bool result = false;
    WEAV_DELETEMCSTOPResult dbResult = null;

    dbResult = DatabaseManager.Instance.WEAV_DELETEMCSTOP(dbPara);
    result = (null != dbResult); // True if deletion succeeded

    return result;
}
```

**Usage Example**:
```csharp
// Delete specific machine stop record
WeavingDataService service = WeavingDataService.Instance;

bool success = service.WEAV_DELETEMCSTOP(
    P_LOOMNO: "L-001",
    P_DOFFNO: 12345,
    P_BEAMROLL: "BM-2024-001",
    P_DEFECT: "D001",
    P_DATE: DateTime.Parse("2024-10-13 14:30:00")
);

if (success)
{
    MessageBox.Show("Machine stop record deleted successfully.");
    RefreshStopList(); // Reload the grid
    RecalculateOEE();  // Update metrics
}
else
{
    MessageBox.Show("Failed to delete machine stop record.");
}
```

**Assumed SQL Logic** (based on parameters):
```sql
-- Likely stored procedure implementation
DELETE FROM tblWeavingMachineStop
WHERE LOOMNO = P_LOOMNO
  AND DOFFNO = P_DOFFNO
  AND BEAMROLL = P_BEAMROLL
  AND DEFECTCODE = P_DEFECT
  AND STOPDATE = P_DATE;

-- Should include:
-- COMMIT;
-- Audit log entry
-- Error handling if no record found
```

---

**File**: 064/296 | **Progress**: 21.6%
