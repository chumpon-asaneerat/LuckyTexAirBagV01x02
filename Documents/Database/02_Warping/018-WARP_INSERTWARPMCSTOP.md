# WARP_INSERTWARPMCSTOP

**Procedure Number**: 018 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Record machine stop incident during warping |
| **Operation** | INSERT |
| **Called From** | WarpingDataService.cs:1838 → WARP_INSERTWARPMCSTOP() |
| **Frequency** | Medium (whenever machine stops) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ⬜ | Warper lot number |
| `P_REASON` | VARCHAR2(100) | ⬜ | Stop reason code/description |
| `P_LENGTH` | NUMBER | ⬜ | Length (meters) when stopped |
| `P_OTHER` | VARCHAR2(200) | ⬜ | Other notes/details |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator who records stop |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result code (success/error) |

### Returns (if cursor)

N/A - Returns single string result

---

## Business Logic (What it does and why)

Records machine stop incidents during warping production for downtime tracking and quality analysis. When machine stops (yarn break, mechanical issue, operator intervention), operator records reason and details. Critical for analyzing production efficiency, identifying quality issues, and maintenance needs.

**Workflow**:
1. Machine stops during production
2. Operator identifies stop reason from predefined list
3. Records length/position where stop occurred
4. Adds additional notes if needed (P_OTHER)
5. System calls this procedure to log stop:
   - Creates stop record with timestamp
   - Links to warping head and lot
   - Records reason and position
   - Records who logged the stop
6. Returns success/error code
7. Stop data used for:
   - Downtime reports
   - Quality problem analysis
   - Maintenance scheduling
   - Operator performance tracking

**Business Rules**:
- Warping head number required
- Lot number links stop to specific beam
- Stop reason from predefined codes (yarn break, tension issue, mechanical, etc.)
- Length indicates where in production stop occurred
- Timestamp auto-recorded
- Operator accountability
- Used for efficiency calculations (downtime vs. uptime)

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Production lot must exist
**Downstream**: [010-WARP_GETSTOPREASONBYWARPERLOT.md](./010-WARP_GETSTOPREASONBYWARPERLOT.md) - Retrieve stop history
**Similar**: [BEAM_INSERTBEAMMCSTOP.md](../03_Beaming/BEAM_INSERTBEAMMCSTOP.md) - Similar stop recording

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_INSERTWARPMCSTOP()`
**Line**: 1838-1872

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_INSERTWARPMCSTOP(WARP_INSERTWARPMCSTOPParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 018/296 | **Progress**: 6.1%
