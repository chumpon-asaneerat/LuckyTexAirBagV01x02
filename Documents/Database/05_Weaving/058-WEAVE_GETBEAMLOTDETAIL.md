# WEAVE_GETBEAMLOTDETAIL

**Procedure Number**: 058 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beam details for weaving setup |
| **Operation** | SELECT |
| **Tables** | tblBeamingDetail, tblBeamingHead, tblDrawing |
| **Called From** | WeavingDataService.cs:629 → WEAVE_GETBEAMLOTDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |

### Returns (Cursor)

Returns beam details: BEAMNO, BEAMLOT, LENGTH, TOTALYARN, ITM_PREPARE, PRODUCTTYPEID, DRAWINGTYPE, REEDNO, HEALDNO, HEALDCOLOR, REEDTYPE

---

## Business Logic

Retrieves complete beam information including drawing details when operator scans beam to set up loom. Validates beam is ready for weaving.

---

**File**: 58/296 | **Progress**: 19.6%
