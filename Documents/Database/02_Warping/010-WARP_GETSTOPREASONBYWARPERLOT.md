# WARP_GETSTOPREASONBYWARPERLOT

**Procedure Number**: 010 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get machine stop reasons for warping lot |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:731 → WARP_GETSTOPREASONBYWARPERLOT() |
| **Frequency** | Medium (reviewing stop history) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warper lot number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot number |
| `REASON` | VARCHAR2(100) | Stop reason code/description |
| `LENGTH` | NUMBER | Length at stop point (meters) |
| `OPERATOR` | VARCHAR2(50) | Operator who recorded stop |
| `OTHERFLAG` | VARCHAR2(1) | Other/miscellaneous flag |
| `CREATEDATE` | DATE | Stop record creation date/time |

---

## Business Logic (What it does and why)

Retrieves all machine stop incidents for specific warping lot. When machine stops during production, operator records reason (yarn break, mechanical issue, etc.). This procedure returns stop history for analysis of downtime, quality issues, and production efficiency. Used for production reports and problem analysis.

**Workflow**:
1. User requests stop history for specific warping lot
2. Procedure queries all stop records for that lot
3. Returns chronological list of stops with:
   - Stop reason/description
   - Length at which stop occurred
   - Who recorded the stop
   - When it was recorded
4. UI displays stop history timeline
5. Used for:
   - Downtime analysis
   - Quality problem tracking
   - Operator performance review
   - Machine maintenance needs

**Business Rules**:
- Each stop must have a reason code
- Records exact length where stop occurred
- Tracks who recorded the stop (accountability)
- Timestamp records when stop was logged
- OTHERFLAG indicates miscellaneous/unplanned stops
- Multiple stops can occur for same lot

---

## Related Procedures

**Upstream**: [018-WARP_INSERTWARPMCSTOP.md](./018-WARP_INSERTWARPMCSTOP.md) - Creates stop records
**Downstream**: None - Display/reporting only
**Similar**: [BEAM_GETSTOPREASONBYBEAMLOT.md](../03_Beaming/BEAM_GETSTOPREASONBYBEAMLOT.md) - Similar in beaming

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETSTOPREASONBYWARPERLOT()`
**Lines**: 731-772

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_GETSTOPREASONBYWARPERLOT(WARP_GETSTOPREASONBYWARPERLOTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 010/296 | **Progress**: 3.4%
