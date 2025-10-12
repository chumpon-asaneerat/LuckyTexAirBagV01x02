# WARP_UPDATESETTINGHEAD

**Procedure Number**: 024 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update warping creel setup status and completion information |
| **Operation** | UPDATE |
| **Tables** | tblWarpingCreelSetup |
| **Called From** | WarpingDataService.cs:1573 → WARP_UPDATESETTINGHEAD_MCStatus()<br>WarpingDataService.cs:1612 → WARP_UPDATESETTINGHEAD() |
| **Frequency** | High (status changes during warping lifecycle) |
| **Performance** | Fast (single record update) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN) - Version 1 (MCStatus Update)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_STARTDATE` | DATE | ⬜ | Conditioning start date/time |
| `P_CONDITONBY` | VARCHAR2(50) | ⬜ | Operator who started conditioning |
| `P_STATUS` | VARCHAR2(10) | ⬜ | Status code (S=Processing, C=Conditioning) |

### Input (IN) - Version 2 (Completion Update)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_ENDDATE` | DATE | ⬜ | Setup completion date/time |
| `P_STATUS` | VARCHAR2(10) | ⬜ | Status code (F=Finished) |
| `P_FINISHBY` | VARCHAR2(50) | ⬜ | Operator who finished setup |

### Output (OUT)

N/A - Returns success/failure status

### Returns (if cursor)

N/A - Returns boolean in C# (true = success, false = failure)

**Note**: This procedure is also used in BeamingDataService for similar status updates.

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingCreelSetup` - UPDATE - Updates status and timestamps for creel setup lifecycle

**Transaction**: Yes (single update operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE UNIQUE INDEX idx_creelsetup_pk ON tblWarpingCreelSetup(WARPHEADNO);
CREATE INDEX idx_creelsetup_status ON tblWarpingCreelSetup(STATUS);
CREATE INDEX idx_creelsetup_machine ON tblWarpingCreelSetup(WARPMC, SIDE);
```

---

## Business Logic (What it does and why)

Updates the status and completion information of a warping creel setup as it progresses through different stages. This procedure handles two main scenarios: conditioning phase start and setup completion.

**Workflow - Scenario 1: Start Conditioning**
1. Operator completes creel setup (all pallets loaded)
2. Operator starts conditioning process (yarn tension stabilization)
3. System calls WARP_UPDATESETTINGHEAD_MCStatus():
   - P_STARTDATE = current timestamp
   - P_CONDITONBY = operator ID
   - P_STATUS = 'C' (Conditioning)
4. Machine status shows "Conditioning" instead of "Setup"
5. Conditioning typically runs 1-4 hours

**Workflow - Scenario 2: Complete Setup**
1. Conditioning period completes
2. Operator confirms setup ready for production
3. System calls WARP_UPDATESETTINGHEAD():
   - P_ENDDATE = current timestamp
   - P_STATUS = 'F' (Finished)
   - P_FINISHBY = operator ID
4. Machine status shows "Ready" or moves to production
5. Setup can now start production runs

**Business Rules**:
- **Status Progression**: Setup → Conditioning (C) → Finished (F) → Processing (S)
- **Conditioning Phase**:
  - Required for proper yarn tension stabilization
  - Typically 1-4 hours depending on product
  - Operator monitors hardness and tension
  - No production during conditioning
- **Timestamps Track**:
  - STARTDATE: When creel setup created
  - CONDITIONSTART: When conditioning began
  - ENDDATE: When setup fully complete
- **Operator Accountability**:
  - CREATEBY: Who created setup
  - CONDITIONBY: Who started conditioning
  - FINISHBY: Who completed setup

**Status Codes**:
- **S** = Processing (warping in progress)
- **C** = Conditioning (yarn stabilization)
- **F** = Finished (setup complete, ready for production)

---

## Related Procedures

**Upstream**: [016-WARP_INSERTSETTINGHEAD.md](./016-WARP_INSERTSETTINGHEAD.md) - Creates initial setup record
**Downstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Starts production after setup finished
**Cross-Module**: BEAM_UPDATESETTINGHEAD - Beaming uses same procedure pattern

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Methods**:
- `WARP_UPDATESETTINGHEAD_MCStatus()` - Line 1573-1606
- `WARP_UPDATESETTINGHEAD()` - Line 1612-1645

**Query Type**: Stored Procedure Call (Oracle)

```csharp
// VERSION 1: Update to Conditioning Status
public bool WARP_UPDATESETTINGHEAD_MCStatus(string P_WARPHEADNO, DateTime? P_STARTDATE,
    string P_CONDITONBY, string P_STATUS)
{
    bool result = false;

    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    WARP_UPDATESETTINGHEADParameter dbPara = new WARP_UPDATESETTINGHEADParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_STARTDATE = P_STARTDATE;          // CONDITIONSTART timestamp
    dbPara.P_CONDITONBY = P_CONDITONBY;        // Operator who started conditioning
    dbPara.P_STATUS = P_STATUS;                // 'C' for Conditioning

    WARP_UPDATESETTINGHEADResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.WARP_UPDATESETTINGHEAD(dbPara);
        result = (null != dbResult);
    }
    catch (Exception ex)
    {
        ex.Err();
        result = false;
    }

    return result;
}

// VERSION 2: Update to Finished Status
public bool WARP_UPDATESETTINGHEAD(string P_WARPHEADNO, DateTime? P_ENDDATE,
    string P_STATUS, string P_FINISHBY)
{
    bool result = false;

    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    WARP_UPDATESETTINGHEADParameter dbPara = new WARP_UPDATESETTINGHEADParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_ENDDATE = P_ENDDATE;              // Completion timestamp
    dbPara.P_STATUS = P_STATUS;                // 'F' for Finished
    dbPara.P_FINISHBY = P_FINISHBY;            // Operator who finished

    WARP_UPDATESETTINGHEADResult dbResult = null;

    try
    {
        dbResult = DatabaseManager.Instance.WARP_UPDATESETTINGHEAD(dbPara);
        result = (null != dbResult);
    }
    catch (Exception ex)
    {
        ex.Err();
        result = false;
    }

    return result;
}
```

**Expected Oracle Stored Procedure Logic**:
```sql
-- Estimated stored procedure structure (handles both versions)
PROCEDURE WARP_UPDATESETTINGHEAD(
    P_WARPHEADNO IN VARCHAR2,
    P_STARTDATE IN DATE DEFAULT NULL,         -- CONDITIONSTART
    P_CONDITONBY IN VARCHAR2 DEFAULT NULL,    -- Conditioning operator
    P_ENDDATE IN DATE DEFAULT NULL,           -- Completion date
    P_STATUS IN VARCHAR2 DEFAULT NULL,        -- Status code
    P_FINISHBY IN VARCHAR2 DEFAULT NULL       -- Finishing operator
)
IS
BEGIN
    UPDATE tblWarpingCreelSetup
    SET STATUS = NVL(P_STATUS, STATUS),
        CONDITIONSTART = NVL(P_STARTDATE, CONDITIONSTART),
        CONDITIONBY = NVL(P_CONDITONBY, CONDITIONBY),
        ENDDATE = NVL(P_ENDDATE, ENDDATE),
        FINISHBY = NVL(P_FINISHBY, FINISHBY),
        FINISHFLAG = CASE
            WHEN P_STATUS = 'F' THEN 'Y'
            ELSE FINISHFLAG
        END,
        EDITDATE = SYSDATE
    WHERE WARPHEADNO = P_WARPHEADNO;

    COMMIT;

EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE;
END;
```

**Usage in UI Flow**:
1. **Setup Created** → STATUS = NULL, FINISHFLAG = 'N'
2. **Start Conditioning** → Call WARP_UPDATESETTINGHEAD_MCStatus() → STATUS = 'C'
3. **Finish Setup** → Call WARP_UPDATESETTINGHEAD() → STATUS = 'F', FINISHFLAG = 'Y'
4. **Start Production** → STATUS changes to 'S' (via WARP_INSERTWARPINGPROCESS)

---

**File**: 024/296 | **Progress**: 8.1%
