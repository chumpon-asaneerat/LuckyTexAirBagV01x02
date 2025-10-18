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
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator ID saving conditions |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Saves standard drawing-in process conditions. Drawing-in is the process of threading warp yarns through heddles and reed before mounting beam on loom. This is typically a manual or semi-automated process that requires knowing how many yarns to thread and the threading pattern.

The procedure:
1. Takes preparation item code and yarn count
2. Saves or updates condition record
3. Stores yarn count specification
4. Used during drawing-in to ensure correct threading
5. Simpler than warping/beaming (fewer automated parameters)

While simpler than other process conditions, this ensures operators thread the correct number of yarns per heddle/dent.

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
