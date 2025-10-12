# DRAW_UPDATEDRAWING

**Procedure Number**: 053 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update drawing production record and completion status |
| **Operation** | UPDATE |
| **Tables** | tblDrawing |
| **Called From** | DrawingDataService.cs:471 → DRAW_UPDATEDRAWING() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number (barcode) |
| `P_DRAWINGTYPE` | VARCHAR2(50) | ✅ | Drawing type (manual/auto) |
| `P_REEDNO` | VARCHAR2(50) | ✅ | Reed number used |
| `P_HEALDCOLOR` | VARCHAR2(50) | ✅ | Heald (heddle) color |
| `P_HEALDNO` | NUMBER | ⬜ | Heald (heddle) number |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator performing update |
| `P_FLAG` | VARCHAR2(1) | ✅ | Completion flag (0=In-Progress, 1=Complete) |
| `P_GROUP` | VARCHAR2(50) | ✅ | Operator group/team |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message (success/error) |

### Returns

| Type | Description |
|------|-------------|
| `String` | Result message from database |

---

## Business Logic

Updates drawing record with final data and sets completion flag when drawing operation finishes.

**Purpose**:
- Update drawing parameters during operation
- Mark drawing complete (FLAG) when finished
- Record finish operator

**When Used**:
- Operator completes drawing → Updates record with FLAG='1' (complete)
- Operator modifies parameters → Updates reed/heald data

**Also used in WeavingDataService** (cross-module usage)

---

**File**: 53/296 | **Progress**: 17.9%

---

## Module M04-Drawing Complete! ✅

**M04-Drawing**: 7/7 procedures documented (100% complete)
- All drawing operations documented
- Reed/heald threading process tracking
- Transfer slip generation (M04→M05)
- Cross-module integration with M03-Beaming and M05-Weaving
