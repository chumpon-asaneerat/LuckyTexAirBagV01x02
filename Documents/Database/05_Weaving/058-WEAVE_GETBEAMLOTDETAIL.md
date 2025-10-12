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

| Column | Type | Description |
|--------|------|-------------|
| `BEAMNO` | VARCHAR2 | Physical beam number |
| `BEAMLOT` | VARCHAR2 | Beam lot barcode |
| `LENGTH` | NUMBER | Beam length (meters) |
| `TOTALYARN` | NUMBER | Total yarn count |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type ID |
| `DRAWINGTYPE` | VARCHAR2 | Drawing type |
| `REEDNO` | VARCHAR2 | Reed number |
| `HEALDNO` | VARCHAR2 | Heald number |
| `HEALDCOLOR` | VARCHAR2 | Heald color code |
| `REEDTYPE` | VARCHAR2 | Reed type |

---

## Business Logic

Retrieves complete beam information including drawing details when operator scans beam to set up loom. Validates beam is ready for weaving.

---

**File**: 58/296 | **Progress**: 19.6%
