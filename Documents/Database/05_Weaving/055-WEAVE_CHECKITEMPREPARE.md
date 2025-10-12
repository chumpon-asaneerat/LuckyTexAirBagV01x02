# WEAVE_CHECKITEMPREPARE

**Procedure Number**: 055 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Validate weaving item matches item prepare and get specifications |
| **Operation** | SELECT |
| **Tables** | tblItemCode, tblItemPrepare |
| **Called From** | WeavingDataService.cs:562 → WEAVE_CHECKITEMPREPARE() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMWEAVING` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code (from beam) |

### Returns (Cursor)

Returns 23 fields including: ITM_CODE, ITM_WEAVING, ITM_YARN, ITM_WIDTH, ITM_PROC1-6, ITM_PREPARE, COREWEIGHT, FULLWEIGHT, ITM_GROUP, YARNCODE, WIDTHCODE, WIDTHWEAVING, LABFORM, WEAVE_TYPE, and audit fields

---

## Business Logic

Validates that selected weaving item is compatible with item prepare on beam. Returns complete item specifications for loom setup.

**Purpose**: Ensure correct product specifications before starting weaving to prevent mismatched production.

---

**File**: 55/296 | **Progress**: 18.6%
