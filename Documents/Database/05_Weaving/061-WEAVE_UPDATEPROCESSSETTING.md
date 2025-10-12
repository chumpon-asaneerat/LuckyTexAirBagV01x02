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

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_REEDNO2` | VARCHAR2(50) | ✅ | Reed number 2 |
| `P_TEMPLE` | VARCHAR2(50) | ✅ | Temple setting |
| `P_BARNO` | VARCHAR2(50) | ✅ | Bar number |
| `P_PRODUCTTYPE` | VARCHAR2(50) | ✅ | Product type |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |
| `P_REEDNO` | VARCHAR2(50) | ❌ | Reed number 1 |
| `P_REEDTYPE` | VARCHAR2(50) | ❌ | Reed type |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic

Updates loom setup parameters when operator adjusts machine settings during production (reed change, temple adjustment, etc.).

---

**File**: 61/296 | **Progress**: 20.6%
