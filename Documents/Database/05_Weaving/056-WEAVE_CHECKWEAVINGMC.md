# WEAVE_CHECKWEAVINGMC

**Procedure Number**: 056 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Check loom machine availability and current status |
| **Operation** | SELECT |
| **Tables** | tblWeavingSettingHead |
| **Called From** | WeavingDataService.cs:1053 → WEAVE_CHECKWEAVINGMC() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number |

### Returns (Cursor)

Returns current setup on loom if exists (BEAMLOT, ITM_WEAVING, status fields)

---

## Business Logic

Checks if loom is available or currently has an active setup. Prevents double-booking of looms.

---

**File**: 56/296 | **Progress**: 18.9%
