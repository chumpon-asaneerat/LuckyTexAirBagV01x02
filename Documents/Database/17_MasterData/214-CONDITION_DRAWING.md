# CONDITION_DRAWING

**Procedure Number**: 214 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Save drawing-in process conditions for item preparation |
| **Operation** | INSERT/UPDATE |
| **Tables** | Drawing conditions master table |
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
| `P_NOYARN` | NUMBER | ⬜ | Number of yarns |
| `P_REEDTYPE` | NUMBER | ⬜ | Reed type specification |
| `P_NODENT` | NUMBER | ⬜ | Number of dents |
| `P_PITCH` | NUMBER | ⬜ | Reed pitch (dents per cm) |
| `P_AIRSPACE` | NUMBER | ⬜ | Air space specification |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves standard drawing-in process conditions. Drawing-in is the process of threading warp yarns through heddles and reed before mounting beam on loom. This procedure stores specifications for reed type, dent count, pitch, and yarn threading pattern.

The procedure:
1. Takes preparation item code and reed specifications
2. Saves or updates condition record
3. Stores reed type, number of dents, pitch (dents per cm)
4. Stores yarn count and air space specifications
5. Used during drawing-in to ensure correct threading pattern

These specifications ensure operators use the correct reed and thread yarns properly for fabric construction.

---

## Related Procedures

**Similar**:
- [212-CONDITION_WARPING.md](./212-CONDITION_WARPING.md) - Warping conditions
- [213-CONDITION_BEAMING.md](./213-CONDITION_BEAMING.md) - Beaming conditions

**Used By**: M04 Drawing module for process setup

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\ProcessConditionDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CONDITION_DRAWING(CONDITION_DRAWINGParameter para)`
**Lines**: 30781-30818

---

**File**: 214/296 | **Progress**: 72.3%
