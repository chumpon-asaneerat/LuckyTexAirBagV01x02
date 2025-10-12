# BEAM_EDITBEAMERMC

**Procedure Number**: 028 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Change beaming machine assignment for a beam setup |
| **Operation** | UPDATE |
| **Tables** | tblBeamingSetup |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingSetup` - UPDATE - Changes machine assignment for beam setup

**Transaction**: Yes (single update operation with validation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_beamingsetup_beamerno ON tblBeamingSetup(BEAMERNO);
CREATE INDEX idx_beamingsetup_beammc ON tblBeamingSetup(BEAMMC);
```

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

**File**: `BeamingDataService.cs`
**Method**: `BEAM_EDITBEAMERMC()`
**Line**: 1583-1618

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public string BEAM_EDITBEAMERMC(string P_BEAMERNO, string P_BEAMMC,
    string P_NEWBEAMMC, string P_OPERATOR)
{
    string result = string.Empty;

    // Validation: beamer number and current machine required
    if (string.IsNullOrWhiteSpace(P_BEAMERNO))
        return result;

    if (string.IsNullOrWhiteSpace(P_BEAMMC))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    BEAM_EDITBEAMERMCParameter dbPara = new BEAM_EDITBEAMERMCParameter();
    dbPara.P_BEAMERNO = P_BEAMERNO;
    dbPara.P_BEAMMC = P_BEAMMC;
    dbPara.P_NEWBEAMMC = P_NEWBEAMMC;
    dbPara.P_OPERATOR = P_OPERATOR;

    BEAM_EDITBEAMERMCResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.BEAM_EDITBEAMERMC(dbPara);

        result = dbResult.RESULT;
    }
    catch (Exception ex)
    {
        ex.Err();
        result = string.Empty;
    }

    return result;
}
```

**Expected Oracle Stored Procedure Logic**:
```sql
-- Estimated stored procedure structure
PROCEDURE BEAM_EDITBEAMERMC(
    P_BEAMERNO IN VARCHAR2,
    P_BEAMMC IN VARCHAR2,
    P_NEWBEAMMC IN VARCHAR2,
    P_OPERATOR IN VARCHAR2,
    O_RESULT OUT VARCHAR2
)
IS
    v_count NUMBER;
    v_new_mc_busy NUMBER;
BEGIN
    -- Validate setup exists with current machine
    SELECT COUNT(*) INTO v_count
    FROM tblBeamingSetup
    WHERE BEAMERNO = P_BEAMERNO
      AND BEAMMC = P_BEAMMC
      AND STATUS NOT IN ('C', 'F'); -- Not completed/finished

    IF v_count = 0 THEN
        O_RESULT := 'ERROR: Setup not found or already completed';
        RETURN;
    END IF;

    -- Check if new machine is available
    SELECT COUNT(*) INTO v_new_mc_busy
    FROM tblBeamingSetup
    WHERE BEAMMC = P_NEWBEAMMC
      AND STATUS IN ('S', 'P'); -- Processing status

    IF v_new_mc_busy > 0 THEN
        O_RESULT := 'ERROR: New machine is busy';
        RETURN;
    END IF;

    -- Update machine assignment
    UPDATE tblBeamingSetup
    SET BEAMMC = P_NEWBEAMMC,
        EDITBY = P_OPERATOR,
        EDITDATE = SYSDATE
    WHERE BEAMERNO = P_BEAMERNO
      AND BEAMMC = P_BEAMMC;

    COMMIT;

    O_RESULT := 'SUCCESS: Machine changed to ' || P_NEWBEAMMC;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        O_RESULT := 'ERROR: ' || SQLERRM;
END;
```

---

**File**: 028/296 | **Progress**: 9.5%
