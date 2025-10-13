# WEAVE_CANCELLOOMSETUP

**Procedure Number**: 054 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Cancel loom setup and release machine |
| **Operation** | UPDATE/DELETE |
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

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_CANCELLOOMSETUP()`
**Line**: 1743

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WEAVE_CANCELLOOMSETUP(WEAVE_CANCELLOOMSETUPParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 54/296 | **Progress**: 18.2%
