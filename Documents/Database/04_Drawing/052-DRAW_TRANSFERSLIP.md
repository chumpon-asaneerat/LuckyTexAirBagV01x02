# DRAW_TRANSFERSLIP

**Procedure Number**: 052 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get drawing data for transfer slip to weaving (M04→M05) |
| **Operation** | SELECT |
| **Called From** | DrawingDataService.cs:370 → DRAW_TRANSFERSLIP() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ✅ | Beam lot barcode (BEAMLOT) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
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
| `USEFLAG` | VARCHAR2 | Usage flag |
| `HEALDNO` | NUMBER | Heald (heddle) number |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/team |
| `TOTALYARN` | NUMBER | Total yarn ends count (from beam) |
| `BEAMNO` | VARCHAR2 | Physical beam number (from beam) |
| `LENGTH` | NUMBER | Beam length in meters (from beam) |
| `BEAMERNO` | VARCHAR2 | Beamer setup number (from beam) |

---

## Business Logic

Retrieves complete drawing and beam data for printing transfer slip when beam moves from drawing (M04) to weaving (M05). Similar to BEAM_TRANFERSLIP but for drawing-to-weaving transfer.

**Purpose**: Generate transfer document showing drawing operation completed, beam ready for weaving process.

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\DrawingDataService.cs`
**Method**: `DRAW_TRANSFERSLIP()`
**Line**: 370

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `DRAW_TRANSFERSLIP(DRAW_TRANSFERSLIPParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 52/296 | **Progress**: 17.6%
