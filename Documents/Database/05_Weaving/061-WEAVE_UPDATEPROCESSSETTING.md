# WEAVE_UPDATEPROCESSSETTING

**Procedure Number**: 061 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update loom setup parameters |
| **Operation** | UPDATE |
| **Tables** | tblWeavingSettingHead |
| **Called From** | WeavingDataService.cs:1471 → WEAVE_UPDATEPROCESSSETTING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

8+ parameters: P_BEAMLOT, P_REEDNO2, P_TEMPLE, P_BARNO, P_PRODUCTTYPE, P_OPERATOR, etc.

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic

Updates loom setup parameters when operator adjusts machine settings during production (reed change, temple adjustment, etc.).

---

**File**: 61/296 | **Progress**: 20.6%
