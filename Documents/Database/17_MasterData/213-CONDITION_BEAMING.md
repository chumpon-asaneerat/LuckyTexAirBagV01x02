# CONDITION_BEAMING

**Procedure Number**: 213 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Save beaming process conditions for item preparation |
| **Operation** | INSERT/UPDATE |
| **Tables** | Beaming conditions master table |
| **Called From** | ProcessConditionDataService.cs |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Preparation item code |
| `P_NOWARPBEAM` | NUMBER | ⬜ | Number of warp beams to combine |
| `P_TOTALYARN` | NUMBER | ⬜ | Total yarn count |
| `P_TOTALKEBA` | NUMBER | ⬜ | Total keba count |
| `P_BEAMLENGTH` | NUMBER | ⬜ | Target beam length (meters) |
| `P_HARDNESS_MAX` | NUMBER | ⬜ | Maximum hardness specification |
| `P_HARDNESS_MIN` | NUMBER | ⬜ | Minimum hardness specification |
| `P_BEAMWIDTH_MAX` | NUMBER | ⬜ | Maximum beam width |
| `P_BEAMWIDTH_MIN` | NUMBER | ⬜ | Minimum beam width |
| `P_SPEED_MAX` | NUMBER | ⬜ | Maximum machine speed (m/min) |
| `P_SPEED_MIN` | NUMBER | ⬜ | Minimum machine speed (m/min) |
| `P_YARNTENSION_MAX` | NUMBER | ⬜ | Maximum yarn tension |
| `P_YARNTENSION_MIN` | NUMBER | ⬜ | Minimum yarn tension |
| `P_WINDTENSION_MAX` | NUMBER | ⬜ | Maximum wind tension |
| `P_WINDTENSION_MIN` | NUMBER | ⬜ | Minimum wind tension |
| `P_COMBTYPE` | VARCHAR2(50) | ⬜ | Comb type specification |
| `P_COMBPITCH` | NUMBER | ⬜ | Comb pitch (mm) |
| `P_TOTALBEAM` | NUMBER | ⬜ | Total number of beams |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves standard beaming process conditions for combining multiple warp beams into a single larger beam. Beaming takes several small warp beams from warping machines and combines them into one wide beam ready for loom mounting. This procedure stores the proven settings for speed, tension, width, and beam configuration.

The procedure:
1. Takes preparation item code and process parameters
2. Saves or updates condition record for this item
3. Stores target ranges (min/max) for critical parameters
4. Used during beaming setup to recall correct settings
5. Ensures correct beam dimensions and quality

These conditions prevent beam defects like uneven tension or incorrect width.

---

## Related Procedures

**Similar**:
- [212-CONDITION_WARPING.md](./212-CONDITION_WARPING.md) - Warping conditions
- [214-CONDITION_DRAWING.md](./214-CONDITION_DRAWING.md) - Drawing conditions

**Used By**: M03 Beaming module for machine setup

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ProcessConditionDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CONDITION_BEAMING(CONDITION_BEAMINGParameter para)`
**Lines**: 30825-30887

---

**File**: 213/296 | **Progress**: 72.0%
