# WEAVE_DELETEWEFTYARN

**Procedure Number**: 057 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete weft yarn pallet from weaving setup |
| **Operation** | DELETE |
| **Called From** | WeavingDataService.cs:1620 → WEAVE_DELETEWEFTYARN() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll) |
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Weft yarn pallet number |
| `P_CHLOTNO` | VARCHAR2(50) | ✅ | China lot number |

### Returns

Boolean - true if deleted successfully

---

## Business Logic

Removes weft yarn pallet from production tracking when pallet needs to be replaced or was added in error.

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_DELETEWEFTYARN()`
**Line**: 1620

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WEAVE_DELETEWEFTYARN(WEAVE_DELETEWEFTYARNParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 57/296 | **Progress**: 19.3%
