# BEAM_GETBEAMERMCSTATUS

**Procedure Number**: 030 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beaming machine status and current setup information |
| **Operation** | SELECT |
| **Called From** | BeamingDataService.cs:586 → BEAM_GETBEAMERMCSTATUS() |
| **Frequency** | High (real-time machine monitoring) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2(50) | ⬜ | Machine number (empty = all machines) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMERNO` | VARCHAR2(50) | Beamer setup number |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code |
| `TOTALYARN` | NUMBER | Total yarn count |
| `TOTALKEBA` | NUMBER | Total keba count |
| `STARTDATE` | DATE | Setup start date |
| `ENDDATE` | DATE | Setup completion date |
| `CREATEBY` | VARCHAR2(50) | Operator who created setup |
| `CREATEDATE` | DATE | Setup creation date |
| `STATUS` | VARCHAR2(10) | Current status |
| `FINISHFLAG` | VARCHAR2(10) | Finish flag (Y/N) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `WARPHEADNO` | VARCHAR2(50) | Warping head number (traceability) |
| `PRODUCTTYPEID` | VARCHAR2(20) | Product type ID |
| `ADJUSTKEBA` | NUMBER | Adjusted keba count |
| `REMARK` | VARCHAR2(500) | Remarks/notes |
| `NOWARPBEAM` | NUMBER | Number of warp beams |
| `TOTALBEAM` | NUMBER | Total beam count |

---

## Business Logic (What it does and why)

Retrieves current status of beaming machines for production monitoring dashboard. Shows what each machine is currently working on, progress, and setup details.

**Workflow**:
1. Dashboard screen loads
2. System queries machine status
3. Shows real-time view of all beaming machines
4. Displays current setup, product, and progress
5. Operators monitor production status

**Business Rules**:
- If P_MCNO empty, returns all machines
- If P_MCNO specified, returns that machine only
- Shows current active setups (not completed)
- Includes traceability to warping via WARPHEADNO
- Shows material quantities (yarn, keba, beam counts)

**Use Cases**:
- Production monitoring dashboard
- Machine status display
- Capacity planning
- Progress tracking
- Supervisor overview

---

## Related Procedures

**Similar**: [012-WARP_GETWARPERMCSTATUS.md](../02_Warping/012-WARP_GETWARPERMCSTATUS.md)

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_GETBEAMERMCSTATUS()`
**Line**: 586-638

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_GETBEAMERMCSTATUSParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 030/296 | **Progress**: 10.1%
