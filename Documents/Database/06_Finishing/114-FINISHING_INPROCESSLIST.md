# FINISHING_INPROCESSLIST

**Procedure Number**: 114 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve list of all in-process finishing operations for dashboard monitoring |
| **Operation** | SELECT |
| **Tables** | tblFinishingCoating, tblFinishingScouring, tblFinishingDryer (inferred) |
| **Called From** | CoatingDataService.cs:3477 → FINISHING_INPROCESSLIST() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

None (no parameters - returns all in-process operations)

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `MCNAME` | VARCHAR2 | Machine name (coating/scouring/dryer machine) |
| `FINISHINGLOT` | VARCHAR2 | Finishing lot number |
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number (source fabric) |
| `ITM_CODE` | VARCHAR2 | Item code |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/shift |
| `STATUS` | VARCHAR2 | Process status (START/CONDITION/RUNNING) |
| `FINISHINGCUSTOMER` | VARCHAR2 | Customer ID |
| `STARTBY` | VARCHAR2 | Operator who started process |
| `CONDITIONBY` | VARCHAR2 | Operator who set conditions |

---

## Business Logic

**Purpose**: Provide real-time visibility of all active finishing operations across all machines and processes (coating, scouring, dryer). Used primarily for dashboard monitoring and production management.

**When Used**:

**Scenario 1: Production Dashboard Display**
1. Finishing supervisor opens main dashboard
2. System calls FINISHING_INPROCESSLIST (no parameters)
3. Returns all currently active finishing operations
4. Dashboard displays in grid format:
   ```
   Machine    | Lot        | Item      | Status    | Operator | Customer
   COAT-01    | FN-2025-001| AB-45-PA66| START     | OPR-101  | TOYOTA
   DRYER-03   | FN-2025-002| AB-50-PA66| CONDITION | OPR-205  | HONDA
   SCOUR-02   | FN-2025-003| AB-60-PA66| RUNNING   | OPR-150  | NISSAN
   ```
5. Supervisor can quickly identify:
   - Which machines are running
   - Which lots are being processed
   - Current status of each operation
   - Who is responsible for each process

**Scenario 2: Shift Handover Report**
1. Day shift ending, night shift starting
2. Shift supervisor generates handover report
3. System calls FINISHING_INPROCESSLIST
4. Report shows all WIP at shift change:
   - Lots started but not finished
   - Operators who started each lot
   - Current process status
5. Night shift supervisor assigns continuation tasks

**Scenario 3: Machine Availability Check**
1. Production planner needs to schedule new lot
2. Checks which coating machines are available
3. FINISHING_INPROCESSLIST shows:
   - COAT-01: In process (FN-2025-001)
   - COAT-02: Available (no record)
   - COAT-03: Condition setup (FN-2025-005)
4. Planner assigns new lot to COAT-02

**Scenario 4: Production Monitoring - Real-time Status**
1. Operations manager monitors production floor remotely
2. Dashboard auto-refreshes every 30 seconds
3. Each refresh calls FINISHING_INPROCESSLIST
4. Manager sees real-time updates:
   - New lots starting (STATUS='START')
   - Conditions being set (STATUS='CONDITION')
   - Processes running (STATUS='RUNNING')
   - Lots removed when finished (no longer in list)

**Business Rules**:
- **No filters**: Returns ALL in-process operations across all machines
- **Status values**:
  - 'START': Machine started, process running
  - 'CONDITION': Operator setting up machine conditions
  - 'RUNNING': Process actively running (may be same as START)
- **Only active lots**: Completed lots NOT included (STATUS='FINISH' excluded)
- **All process types**: Includes coating, scouring, AND dryer operations
- **Real-time data**: Reflects current state of production floor
- **Operator tracking**: Shows who is responsible for each operation

**Integration Points**:
- **Dashboard**: Main data source for finishing operations overview
- **Production Planning**: Check machine availability
- **Shift Management**: Handover reports and WIP tracking
- **Quality Control**: Identify lots ready for sampling
- **Maintenance**: Identify machines currently in use (cannot service)

**Typical Use Frequency**:
- Dashboard: Every 30-60 seconds (auto-refresh)
- Shift reports: 2-3 times per shift
- Planning checks: 10-20 times per day
- Management monitoring: Continuous during production hours

---

## Related Procedures

**Upstream**:
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Creates coating record
- [099-FINISHING_INSERTSCOURING.md](./099-FINISHING_INSERTSCOURING.md) - Creates scouring record
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates dryer record

**Downstream**:
- Dashboard display components
- Production monitoring screens
- Shift handover reports

**Similar**:
- WEAV_WEAVINGINPROCESSLIST (M05) - Weaving in-process list
- BEAM_GETBEAMERMCSTATUS (M03) - Beaming machine status
- WARP_GETWARPERMCSTATUS (M02) - Warping machine status

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_INPROCESSLIST()`
**Lines**: 3471-3521

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_INPROCESSLISTParameter`
**Lines**: 8050-8052 (empty - no parameters)

**Result Class**: `FINISHING_INPROCESSLISTResult`
**Lines**: 8058-8069

**Method**: `FINISHING_INPROCESSLIST(FINISHING_INPROCESSLISTParameter para)`
**Lines**: 27263-27320 (estimated)

---

**File**: 114/296 | **Progress**: 38.5%
