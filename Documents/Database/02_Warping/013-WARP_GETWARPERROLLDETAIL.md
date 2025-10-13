# WARP_GETWARPERROLLDETAIL

**Procedure Number**: 013 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get warper roll/beam details by roll number |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:1170 → WARP_GETWARPERROLLDETAIL() |
| **Frequency** | High (barcode scanning, traceability) |
| **Performance** | Fast (single record lookup) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPERROLL` | VARCHAR2(50) | ✅ | Warper roll/beam barcode number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot number |
| `BEAMNO` | VARCHAR2(50) | Beam number |
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

---

## Business Logic (What it does and why)

Retrieves complete production details for warper beam by scanning beam barcode. Critical for traceability - when beam barcode is scanned in downstream processes (beaming, drawing, weaving), this procedure provides all production history and quality data. Shows how beam was produced, quality metrics, and responsible operators.

**Workflow**:
1. Operator scans warper beam barcode at next process station (beaming, drawing)
2. System calls this procedure with scanned barcode
3. Procedure looks up beam production record
4. Returns complete production details:
   - Traceability (head number, lot, machine, side)
   - Quality metrics (length, hardness, tensions, speed)
   - Accountability (who started, who doffed, when)
   - Status information
5. Downstream process uses data for:
   - Verification (correct beam for order)
   - Quality control (check hardness, tension in spec)
   - Traceability (link to upstream materials)
   - Production history

**Business Rules**:
- Beam barcode must be unique
- Returns single record (one beam)
- Used for forward traceability in production chain
- Quality metrics must match downstream requirements
- Shows complete production provenance

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates beam record
**Downstream**: [BEAM_GETBEAMROLLDETAIL.md](../03_Beaming/BEAM_GETBEAMROLLDETAIL.md) - Next process looks up details
**Similar**: [WEAVE_GETBEAMLOTDETAIL.md](../05_Weaving/WEAVE_GETBEAMLOTDETAIL.md) - Similar traceability lookup

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETWARPERROLLDETAIL()`
**Line**: 1170-1225

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_GETWARPERROLLDETAIL(WARP_GETWARPERROLLDETAILParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 013/296 | **Progress**: 4.4%
