# WEAVE_CANCELLOOMSETUP

**Procedure Number**: 054 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Cancel loom setup and release machine |
| **Operation** | UPDATE/DELETE |
| **Tables** | tblWeavingSettingHead, tblWeavingSettingDetail |
| **Called From** | WeavingDataService.cs:1743 → WEAVE_CANCELLOOMSETUP() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator cancelling setup |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic

Cancels loom setup when beam cannot be used or setup needs to be redone. Releases loom for other work.

---

**File**: 54/296 | **Progress**: 18.2%
