# BEAM_EDITBEAMERMC

**Procedure Number**: 028 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Change beaming machine assignment for a beam setup |
| **Operation** | UPDATE |
| **Called From** | BeamingDataService.cs:1583 → BEAM_EDITBEAMERMC() |
| **Frequency** | Low (only when machine needs to be changed) |
| **Performance** | Fast (single record update) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer number (current setup ID) |
| `P_BEAMMC` | VARCHAR2(50) | ✅ | Current beaming machine number |
| `P_NEWBEAMMC` | VARCHAR2(50) | ✅ | New beaming machine number |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator ID who made the change |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2 | Return message (success/error status) |

### Returns (if cursor)

N/A - Returns single string result

---

## Business Logic (What it does and why)

Allows changing the beaming machine assignment when a beam setup needs to be moved to a different machine. This is necessary when the originally assigned machine has issues, is occupied, or needs maintenance.

**Workflow**:
1. Beam setup created and assigned to Machine A
2. Machine A has problem or is occupied
3. Supervisor decides to move setup to Machine B
4. System validates new machine is available
5. Updates beam setup with new machine number
6. Records operator who made the change
7. Returns success/error message

**Business Rules**:
- Can only edit setup before production starts
- New machine must be available (not running other setup)
- Requires supervisor/operator authorization
- Maintains audit trail (who changed, when changed)
- Both BEAMERNO and current BEAMMC required for validation
- Validates setup exists before updating

**Use Cases**:
- **Machine Breakdown**: Original machine breaks, move to backup
- **Priority Change**: Higher priority job needs the machine
- **Maintenance**: Scheduled maintenance requires machine change
- **Efficiency**: Move to faster/better performing machine
- **Load Balancing**: Distribute work across machines

**Validation Logic**:
```
1. Check P_BEAMERNO not empty (required)
2. Check P_BEAMMC not empty (required for validation)
3. Verify setup exists with BEAMERNO + BEAMMC combination
4. Check P_NEWBEAMMC is available
5. Update if all validations pass
6. Return success/error message
```

---

## Related Procedures

**Upstream**: BEAM_INSERTBEAMNO - Creates initial setup with machine assignment
**Similar**: [004-WARP_EDITWARPERMCSETUP.md](../02_Warping/004-WARP_EDITWARPERMCSETUP.md) - Warping machine change (same pattern)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_EDITBEAMERMC()`
**Line**: 1583-1618

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_EDITBEAMERMCParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 028/296 | **Progress**: 9.5%
