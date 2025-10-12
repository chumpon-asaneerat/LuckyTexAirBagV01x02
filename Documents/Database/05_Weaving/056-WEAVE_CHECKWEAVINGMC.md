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

| Column | Type | Description |
|--------|------|-------------|
| `BEAMLOT` | VARCHAR2 | Current beam lot on loom |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `STATUS` | VARCHAR2 | Setup status |
| `CREATE_DATE` | DATE | Setup start date |
| `OPERATOR` | VARCHAR2 | Operator name |

---

## Business Logic

Checks if loom is available or currently has an active setup. Prevents double-booking of looms.

---

**File**: 56/296 | **Progress**: 18.9%
