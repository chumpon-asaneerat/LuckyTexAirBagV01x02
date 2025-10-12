# DRAW_GETDRAWINGLISTBYITEM

**Procedure Number**: 049 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get drawing production records by item prepare code |
| **Operation** | SELECT |
| **Tables** | tblDrawing |
| **Called From** | DrawingDataService.cs:245 → DRAW_GETDRAWINGLISTBYITEM() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code filter |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMNO` | VARCHAR2 | Physical beam number |
| `BEAMLOT` | VARCHAR2 | Beam roll lot number (barcode) |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type ID |
| `DRAWINGTYPE` | VARCHAR2 | Drawing type (manual/auto) |
| `STARTDATE` | DATE | Drawing start timestamp |
| `ENDATE` | DATE | Drawing end timestamp |
| `REEDNO` | VARCHAR2 | Reed number used |
| `HEALDCOLOR` | VARCHAR2 | Heald (heddle) color |
| `STARTBY` | VARCHAR2 | Operator who started |
| `FINISHBY` | VARCHAR2 | Operator who finished |
| `USEFLAG` | VARCHAR2 | Usage flag (0/1) |
| `HEALDNO` | NUMBER | Heald (heddle) number |
| `REEDTYPE` | VARCHAR2 | Reed type specification |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/team |

---

## Business Logic

Lists all drawing operations for a specific product (item prepare code) for production tracking and history review.

**When Used**: Search drawing history by product, review past operations, production analysis

---

**File**: 49/296 | **Progress**: 16.6%
