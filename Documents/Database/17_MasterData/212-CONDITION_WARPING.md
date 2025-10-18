# CONDITION_WARPING

**Procedure Number**: 212 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Save warping process conditions for item-machine combination |
| **Operation** | INSERT/UPDATE |
| **Tables** | Warping conditions master table |
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
| `P_MCNO` | VARCHAR2(20) | ✅ | Warping machine number |
| `P_ITMYARN` | VARCHAR2(50) | ✅ | Yarn item code |
| `P_WARPERENDS` | NUMBER | ⬜ | Number of warp ends |
| `P_MAXLENGTH` | NUMBER | ⬜ | Maximum beam length (meters) |
| `P_MINLENGTH` | NUMBER | ⬜ | Minimum beam length (meters) |
| `P_WAXING` | VARCHAR2(10) | ⬜ | Waxing setting (Y/N) |
| `P_COMBTYPE` | VARCHAR2(50) | ⬜ | Comb type specification |
| `P_COMBPITCH` | NUMBER | ⬜ | Comb pitch (mm) |
| `P_KEBAYARN` | NUMBER | ⬜ | Number of yarns per keba |
| `P_NOWARPBEAM` | NUMBER | ⬜ | Number of warp beams to produce |
| `P_HARDNESS_MAX` | NUMBER | ⬜ | Maximum hardness specification |
| `P_HARDNESS_MIN` | NUMBER | ⬜ | Minimum hardness specification |
| `P_SPEED` | NUMBER | ⬜ | Target machine speed (m/min) |
| `P_SPEED_MARGIN` | NUMBER | ⬜ | Speed tolerance margin |
| `P_YARNTENSION` | NUMBER | ⬜ | Yarn tension setting |
| `P_YARNTENSION_MARGIN` | NUMBER | ⬜ | Yarn tension tolerance |
| `P_WINDTENSION` | NUMBER | ⬜ | Wind tension setting |
| `P_WINDTENSION_MARGIN` | NUMBER | ⬜ | Wind tension tolerance |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves standard warping process conditions for specific item-machine combinations. When producing a particular item on a specific warping machine, operators must configure machine parameters (speed, tension, hardness, etc.). This procedure stores the proven settings so operators can quickly set up machines correctly without trial-and-error.

The procedure:
1. Takes item code, machine number, and process parameters
2. Saves or updates condition record for this item-machine combination
3. Stores target values and acceptable tolerance margins
4. Used during machine setup to recall correct settings
5. Ensures consistent quality across production runs

These conditions are critical for first-time-right production and operator training.

---

## Related Procedures

**Similar**:
- [213-CONDITION_BEAMING.md](./213-CONDITION_BEAMING.md) - Beaming conditions
- [214-CONDITION_DRAWING.md](./214-CONDITION_DRAWING.md) - Drawing conditions

**Used By**: M02 Warping module for machine setup

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ProcessConditionDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CONDITION_WARPING(CONDITION_WARPINGParameter para)`
**Lines**: 30443-30506

---

**File**: 212/296 | **Progress**: 71.6%
