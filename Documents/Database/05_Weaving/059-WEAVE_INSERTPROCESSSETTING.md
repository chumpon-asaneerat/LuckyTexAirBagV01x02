# WEAVE_INSERTPROCESSSETTING

**Procedure Number**: 059 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new loom setup with beam and parameters |
| **Operation** | INSERT |
| **Tables** | tblWeavingSettingHead |
| **Called From** | WeavingDataService.cs:1413 → WEAVE_INSERTPROCESSSETTING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_MC` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_ITMWEAVE` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_REEDNO2` | VARCHAR2(50) | ✅ | Reed number 2 |
| `P_TEMPLE` | VARCHAR2(50) | ✅ | Temple setting |
| `P_BARNO` | VARCHAR2(50) | ✅ | Bar number |
| `P_PRODUCTTYPE` | VARCHAR2(50) | ✅ | Product type |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |
| `P_REEDNO` | VARCHAR2(50) | ❌ | Reed number 1 |
| `P_REEDTYPE` | VARCHAR2(50) | ❌ | Reed type |
| `P_HEALDNO` | VARCHAR2(50) | ❌ | Heald number |
| `P_HEALDCOLOR` | VARCHAR2(50) | ❌ | Heald color |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |
| `SETUP_ID` | NUMBER | Generated setup ID |

---

## Business Logic

Creates loom setup record when operator loads beam onto loom. Records all loom parameters (reed, temple, bar, etc.) and generates setup ID for tracking.

**Purpose**: Initialize weaving production - creates header record before starting fabric production.

---

**File**: 59/296 | **Progress**: 19.9%
