# FINISHING_GETSCOURINGDATA

**Procedure Number**: 097 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve scouring lots by machine and status for selection/resumption |
| **Operation** | SELECT |
| **Tables** | tblFinishingLot, tblFinishingScouring |
| **Called From** | CoatingDataService.cs:1503 → FINISHING_GETSCOURINGDATAList() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ✅ | Machine number (scouring machine ID) |
| `P_FLAG` | VARCHAR2(10) | ✅ | Status flag filter (START/FINISH/ALL) |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

Returns comprehensive scouring lot data (similar structure to coating data with scouring-specific fields)

---

## Business Logic (What it does and why)

**Purpose**: Retrieves list of scouring lots on specific machine filtered by status. Used for lot selection, resumption, and viewing completed lots.

**When Used**:
- Machine startup: Show in-progress lots for operator selection
- Lot resumption: Operator picks which lot to continue
- Review completed work: Show finished lots
- All lots view: Show all lots on machine

**Business Rules**:
1. **Machine-Specific**: Only shows lots for specified machine
2. **Status Filtering**: P_FLAG controls which lots visible
3. **Multi-Lot Support**: One machine can have multiple lots
4. **Resume Capability**: Enables process continuation

**Workflow**:
1. Operator arrives at scouring machine
2. System calls procedure with machine number and status
3. Returns list of lots matching criteria
4. Operator selects lot to resume or reviews completed work
5. System loads selected lot parameters

**Why This Matters**:
- Shift continuity for long-running operations
- Resource planning and queue management
- Quality traceability
- Error recovery capability

---

## Related Procedures

**Upstream**:
- FINISHING_INSERTSCOURING (creates records in list)
- FINISHING_UPDATESCOURING (updates record status)

**Downstream**:
- FINISHING_SCOURINGDATABYLOT (loads detailed data after selection)

**Similar**:
- FINISHING_GETCOATINGDATA (same pattern for coating)
- FINISHING_GETDRYERDATA (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_GETSCOURINGDATAList(string mcno, string flag)`
**Lines**: 1503-1610

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_GETSCOURINGDATA(FINISHING_GETSCOURINGDATAParameter para)`
**Lines**: 28XXX (estimated)

---

**File**: 97/296 | **Progress**: 32.8%
