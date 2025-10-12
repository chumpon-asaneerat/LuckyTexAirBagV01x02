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

12+ parameters including: P_BEAMLOT, P_MC (loom), P_ITMWEAVE, P_REEDNO2, P_TEMPLE, P_BARNO, P_PRODUCTTYPE, P_OPERATOR, etc.

### Output (OUT)

Returns WEAVE_INSERTPROCESSSETTING object with R_RESULT and generated setup ID

---

## Business Logic

Creates loom setup record when operator loads beam onto loom. Records all loom parameters (reed, temple, bar, etc.) and generates setup ID for tracking.

**Purpose**: Initialize weaving production - creates header record before starting fabric production.

---

**File**: 59/296 | **Progress**: 19.9%
