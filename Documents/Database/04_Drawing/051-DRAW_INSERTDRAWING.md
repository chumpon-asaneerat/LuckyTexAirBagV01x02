# DRAW_INSERTDRAWING

**Procedure Number**: 051 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new drawing production record |
| **Operation** | INSERT |
| **Called From** | DrawingDataService.cs:427 → DRAW_INSERTDRAWING() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number (barcode) |
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code |
| `P_PRODUCTID` | VARCHAR2(50) | ✅ | Product type ID |
| `P_DRAWINGTYPE` | VARCHAR2(50) | ✅ | Drawing type (manual/auto) |
| `P_REEDNO` | VARCHAR2(50) | ✅ | Reed number used |
| `P_HEALDCOLOR` | VARCHAR2(50) | ✅ | Heald (heddle) color |
| `P_HEALDNO` | NUMBER | ⬜ | Heald (heddle) number |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator performing drawing |
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

Creates new drawing record when operator starts drawing a beam through heddles and reed. Records which reed, heald color, drawing type used, and which operator/group performed the work.

**Purpose**: Start drawing operation - log beam entering drawing process with setup parameters.

**Workflow**: Operator scans beam → Selects drawing parameters → System calls this to create record → Drawing begins

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\DrawingDataService.cs`
**Method**: `DRAW_INSERTDRAWING()`
**Line**: 427

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `DRAW_INSERTDRAWING(DRAW_INSERTDRAWINGParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 51/296 | **Progress**: 17.2%
