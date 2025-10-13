# WARP_GETINPROCESSLOTBYHEADNO

**Procedure Number**: 007 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get in-process warping lots for specific head number |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:782 → WARP_GETINPROCESSLOTBYHEADNO() |
| **Frequency** | Medium (checks production status) |
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
| `BEAMNO` | VARCHAR2(50) | Beam number being produced |
| `SIDE` | VARCHAR2(10) | Side (A or B) |
| `STARTDATE` | DATE | Production start date/time |
| `ENDDATE` | DATE | Production end date/time |
| `LENGTH` | NUMBER | Beam length produced (meters) |
| `SPEED` | NUMBER | Production speed (m/min) |
| `HARDNESS_L` | NUMBER | Hardness left side (1-10) |
| `HARDNESS_N` | NUMBER | Hardness middle (1-10) |
| `HARDNESS_R` | NUMBER | Hardness right side (1-10) |
| `TENSION` | NUMBER | Yarn tension |
| `STARTBY` | VARCHAR2(50) | Started by operator |
| `DOFFBY` | VARCHAR2(50) | Doffed by operator |
| `FLAG` | VARCHAR2(10) | Process flag/status |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `REMARK` | VARCHAR2(200) | Remarks |

---

## Business Logic (What it does and why)

Retrieves all in-process (active) warping lots for a specific creel setup. Shows which beams are currently being produced from this setup, including production metrics (length, speed, hardness, tension) and responsible operators. Used for monitoring active production and displaying current status.

**Workflow**:
1. UI requests in-process lots for specific head number
2. Procedure queries active production records (FLAG != 'FINISHED')
3. Returns list of beams being produced with real-time metrics
4. UI displays production progress, metrics, and operator info
5. Operators can see which beams are in progress

**Business Rules**:
- Shows only in-process lots (not finished)
- Each record represents one beam being produced
- Multiple beams can be produced from same head number
- Includes quality metrics (hardness L/M/R, tension, speed)
- Tracks start/end times and responsible operators

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Starts production
**Downstream**: [025-WARP_UPDATEWARPINGPROCESS.md](./025-WARP_UPDATEWARPINGPROCESS.md) - Updates process
**Similar**: [BEAM_GETINPROCESSLOTBYBEAMNO.md](../03_Beaming/BEAM_GETINPROCESSLOTBYBEAMNO.md) - Similar in beaming

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETINPROCESSLOTBYHEADNO()`
**Lines**: 782-832

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_GETINPROCESSLOTBYHEADNO(WARP_GETINPROCESSLOTBYHEADNOParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 007/296 | **Progress**: 2.4%
