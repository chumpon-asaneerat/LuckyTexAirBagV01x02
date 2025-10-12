# WEAVE_WEAVINGPROCESS

**Procedure Number**: 062 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Start new fabric roll (doff) production |
| **Operation** | INSERT |
| **Tables** | tblWeavingDetail |
| **Called From** | WeavingDataService.cs:1519 → WEAVE_WEAVINGPROCESS() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll sequence) |
| `P_ITEMWEAVING` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_LOOM` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_STARTDATE` | DATE | ✅ | Production start date |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |
| `P_SHIFT` | VARCHAR2(10) | ✅ | Production shift |
| `P_REEDNO` | VARCHAR2(50) | ❌ | Reed number |
| `P_TEMPLE` | VARCHAR2(50) | ❌ | Temple setting |
| `P_BARNO` | VARCHAR2(50) | ❌ | Bar number |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Generated WEAVINGLOT barcode or error |

---

## Business Logic

Starts new fabric roll (doff) production on loom. Generates unique WEAVINGLOT barcode for the fabric roll. Creates production tracking record.

**Purpose**: Begin fabric production - generates barcode for finished fabric roll traceability.

**Workflow**: Loom setup complete → Operator starts weaving → System generates WEAVINGLOT → Production begins

---

**File**: 62/296 | **Progress**: 20.9%

---

## WEAVE_ Procedures Complete! (9/9)

All 9 WEAVE_ setup/configuration procedures documented.
Next: WEAV_ production/monitoring procedures (24 remaining)
