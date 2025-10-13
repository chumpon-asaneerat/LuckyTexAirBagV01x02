# DRAW_GETBEAMLOTDETAIL

**Procedure Number**: 048 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beam details for drawing setup validation |
| **Operation** | SELECT |
| **Called From** | DrawingDataService.cs:182 → CheckBeamLot_ITM_Prepare() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam roll lot number (barcode) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMNO` | VARCHAR2 | Physical beam number |
| `PRODUCTTYPEID` | VARCHAR2 | Product type ID |
| `TOTALYARN` | NUMBER | Total yarn ends count |
| `BEAMLOT` | VARCHAR2 | Beam lot barcode |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |

---

## Business Logic

Retrieves beam details and validates beam lot matches the selected item prepare code before starting drawing operation.

**Purpose**: When operator scans beam barcode to start drawing, this validates:
- Beam exists in system
- Beam matches the selected product (item prepare)
- Returns beam specifications needed for drawing

**Validation Logic** (in C# wrapper):
- If BEAMLOT matches AND ITM_PREPARE matches → Success, return beam data
- If BEAMLOT matches BUT ITM_PREPARE doesn't match → Error: "Beam Lot is not map for this Item Prepare"
- If BEAMLOT doesn't exist → Error: "Beam Lot have no data"

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\DrawingDataService.cs`
**Method**: `CheckBeamLot_ITM_Prepare()`
**Line**: 168-235

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `DRAW_GETBEAMLOTDETAIL(DRAW_GETBEAMLOTDETAILParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 48/296 | **Progress**: 16.2%
