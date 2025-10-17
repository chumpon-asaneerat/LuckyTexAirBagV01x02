# WARP_GETWARPERLOTBYHEADNO

**Procedure Number**: 011 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get all warper lot records for specific head number |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:534 → WARP_GETWARPERLOTBYHEADNO() |
| **Frequency** | High (production history and reporting) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot number |
| `BEAMNO` | VARCHAR2(50) | Beam number produced |
| `SIDE` | VARCHAR2(10) | Side (A or B) |
| `STARTDATE` | DATE | Production start date/time |
| `ENDDATE` | DATE | Production end date/time |
| `LENGTH` | NUMBER | Beam length (meters) |
| `SPEED` | NUMBER | Production speed (m/min) |
| `HARDNESS_L` | NUMBER | Hardness left (1-10) |
| `HARDNESS_N` | NUMBER | Hardness middle (1-10) |
| `HARDNESS_R` | NUMBER | Hardness right (1-10) |
| `TENSION` | NUMBER | Yarn tension |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `DOFFBY` | VARCHAR2(50) | Doffed by operator |
| `FLAG` | VARCHAR2(10) | Process flag/status |
| `WARPMC` | VARCHAR2(50) | Machine number |
| `REMARK` | VARCHAR2(200) | Remarks |
| `TENSION_IT` | NUMBER | IT tension |
| `TENSION_TAKEUP` | NUMBER | Takeup tension |
| `MC_COUNT_L` | NUMBER | Machine count left |
| `MC_COUNT_S` | NUMBER | Machine count small |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2(50) | Last edited by |
| `KEBA` | NUMBER | Keba count |
| `TIGHTEND` | NUMBER | Tight end count |
| `MISSYARN` | NUMBER | Missing yarn count |
| `OTHER` | NUMBER | Other issue count |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - SELECT - All production lots for specified head

**Transaction**: No (read-only operation)

---

## Business Logic (What it does and why)

Retrieves complete production history for specific warping head/creel setup. Shows all beams produced from this setup including production metrics, quality data, and defect counts. Used for production reports, traceability, and quality analysis. Critical for understanding which beams came from which setup and their quality characteristics.

**Workflow**:
1. User requests production history for specific head number
2. Procedure queries all warper lot records for that head
3. Returns complete list of beams produced including:
   - Production dates and times
   - Quality metrics (length, speed, hardness, tension)
   - Operator accountability (who started, who doffed)
   - Defect counts (keba, tight ends, missing yarn)
   - Status flags
4. UI displays production history chronologically
5. Used for:
   - Production reports
   - Beam traceability
   - Quality analysis
   - Operator performance tracking

**Business Rules**:
- One head number can produce multiple beams (lots)
- Each lot represents one complete beam
- Includes quality metrics recorded during production
- Tracks defects for quality control
- Multiple tension readings (yarn, IT, takeup)
- FLAG indicates if lot is finished or in-process
- Edit tracking for audit trail

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates lot records
**Downstream**: [025-WARP_UPDATEWARPINGPROCESS.md](./025-WARP_UPDATEWARPINGPROCESS.md) - Updates lot data
**Similar**: [BEAM_GETBEAMLOTBYBEAMERNO.md](../03_Beaming/BEAM_GETBEAMLOTBYBEAMERNO.md) - Similar history retrieval

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_GETWARPERLOTBYHEADNO()`
**Line**: 534-596

---

**File**: 011/296 | **Progress**: 3.7%
