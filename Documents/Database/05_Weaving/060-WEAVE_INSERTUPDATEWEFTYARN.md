# WEAVE_INSERTUPDATEWEFTYARN

**Procedure Number**: 060 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Add or update weft yarn pallet for weaving |
| **Operation** | INSERT/UPDATE |
| **Tables** | tblWeavingWeftStock |
| **Called From** | WeavingDataService.cs:1572 → WEAVE_INSERTUPDATEWEFTYARN() |
| **Frequency** | High |
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
| `P_CHLOTNO` | VARCHAR2(50) | ✅ | China lot number (weft yarn) |
| `P_POSITION` | VARCHAR2(50) | ✅ | Pallet position on loom |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic

Records weft yarn pallets used in weaving for traceability. Operator scans weft yarn pallet barcode, system records which pallet supplies which fabric roll (doff).

**Purpose**: Track weft yarn consumption and maintain forward/backward traceability from yarn to fabric.

---

**File**: 60/296 | **Progress**: 20.3%
