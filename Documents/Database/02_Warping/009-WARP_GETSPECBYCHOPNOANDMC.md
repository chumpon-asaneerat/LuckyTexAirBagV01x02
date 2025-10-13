# WARP_GETSPECBYCHOPNOANDMC

**Procedure Number**: 009 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get warping specifications by item prepare and machine |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:243 → WARP_GETSPECBYCHOPNOANDMC() |
| **Frequency** | High (loading production specs for setup) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item preparation code (item+specification combination) |
| `P_MCNO` | VARCHAR2(50) | ✅ | Warping machine number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CHOPNO` | VARCHAR2(50) | Chop number / specification ID |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `WARPERENDS` | NUMBER | Number of warper ends |
| `MAXLENGTH` | NUMBER | Maximum beam length (meters) |
| `MINLENGTH` | NUMBER | Minimum beam length (meters) |
| `WAXING` | VARCHAR2(10) | Waxing requirement (Y/N) |
| `COMBTYPE` | VARCHAR2(20) | Comb type |
| `COMBPITCH` | NUMBER | Comb pitch |
| `KEBAYARN` | NUMBER | Keba yarn count |
| `NOWARPBEAM` | NUMBER | Number of warp beams |
| `MAXHARDNESS` | NUMBER | Maximum hardness (1-10) |
| `MINHARDNESS` | NUMBER | Minimum hardness (1-10) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `SPEED` | NUMBER | Production speed (m/min) |
| `SPEED_MARGIN` | NUMBER | Speed tolerance (±) |
| `YARN_TENSION` | NUMBER | Yarn tension setting |
| `YARN_TENSION_MARGIN` | NUMBER | Yarn tension tolerance (±) |
| `WINDING_TENSION` | NUMBER | Winding tension setting |
| `WINDING_TENSION_MARGIN` | NUMBER | Winding tension tolerance (±) |
| `NOCH` | NUMBER | Number of cheese |

---

## Business Logic (What it does and why)

Retrieves complete warping specifications for a specific item-machine combination. When operator starts new warping setup, system loads all technical parameters required for production including speed, tension, hardness ranges, and material requirements. These specs guide operator in machine setup and provide quality control limits.

**Workflow**:
1. Operator selects item preparation code and target machine
2. System queries warping specifications for this combination
3. Procedure retrieves all technical parameters:
   - Physical specs (warper ends, length range, comb details)
   - Process parameters (speed, tensions with margins)
   - Quality limits (hardness range)
   - Material requirements (yarn type, waxing, cheese count)
4. Returns complete specification set
5. UI displays specs to operator for machine setup
6. System uses specs for validation during production

**Business Rules**:
- Specifications are item+machine specific
- Includes tolerance margins for critical parameters
- Speed and tensions have acceptable ranges (±margin)
- Hardness must be within min/max limits
- Operator must configure machine to match specs
- System validates actual values against specs during production

---

## Related Procedures

**Upstream**: None - Entry point for setup
**Downstream**: [015-WARP_INSERTSETTINGHEAD.md](./015-WARP_INSERTSETTINGHEAD.md) - Creates setup with these specs
**Similar**: [BEAM_GETSPECBYCHOPNO.md](../03_Beaming/BEAM_GETSPECBYCHOPNO.md) - Similar spec retrieval for beaming

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_GETSPECBYCHOPNOANDMC()`
**Lines**: 243-297

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WARP_GETSPECBYCHOPNOANDMC(WARP_GETSPECBYCHOPNOANDMCParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 009/296 | **Progress**: 3.0%
