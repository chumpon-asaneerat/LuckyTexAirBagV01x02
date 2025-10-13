# DRAW_GETSPECBYCHOPNO

**Procedure Number**: 050 | **Module**: M04 - Drawing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get drawing specifications by item prepare code |
| **Operation** | SELECT |
| **Called From** | DrawingDataService.cs:124 → ITM_GETITEMPREPARELIST() (method name mismatch) |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 Method name doesn't match procedure (ITM_GETITEMPREPARELIST calls DRAW_GETSPECBYCHOPNO) |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CHOPNO` | VARCHAR2 | Item code |
| `NOYARN` | NUMBER | Number of yarn ends |
| `REEDTYPE` | VARCHAR2 | Reed type specification |
| `LENGTH` | NUMBER | Standard length |
| `NODENT` | NUMBER | Number of dents |
| `PITCH` | NUMBER | Pitch measurement |
| `AIRSPACE` | NUMBER | Air space specification |

---

## Business Logic

Retrieves drawing process specifications when operator selects product to draw. Shows reed type, dent count, pitch, etc. needed for proper drawing setup.

**Purpose**: Display drawing specifications for operator to set up reed, heald, and threading correctly.

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\DrawingDataService.cs`
**Method**: `ITM_GETITEMPREPARELIST()`
**Line**: 124

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `DRAW_GETSPECBYCHOPNO(DRAW_GETSPECBYCHOPNOParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 50/296 | **Progress**: 16.9%
