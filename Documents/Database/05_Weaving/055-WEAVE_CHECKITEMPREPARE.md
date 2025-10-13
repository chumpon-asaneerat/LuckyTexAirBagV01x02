# WEAVE_CHECKITEMPREPARE

**Procedure Number**: 055 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Validate weaving item matches item prepare and get specifications |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:562 → WEAVE_CHECKITEMPREPARE() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMWEAVING` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code (from beam) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2 | Item code |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `ITM_YARN` | VARCHAR2 | Yarn item code |
| `ITM_WIDTH` | NUMBER | Fabric width |
| `ITM_PROC1` | VARCHAR2 | Process parameter 1 |
| `ITM_PROC2` | VARCHAR2 | Process parameter 2 |
| `ITM_PROC3` | VARCHAR2 | Process parameter 3 |
| `ITM_PROC4` | VARCHAR2 | Process parameter 4 |
| `ITM_PROC5` | VARCHAR2 | Process parameter 5 |
| `ITM_PROC6` | VARCHAR2 | Process parameter 6 |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |
| `COREWEIGHT` | NUMBER | Core weight (kg) |
| `FULLWEIGHT` | NUMBER | Full weight (kg) |
| `ITM_GROUP` | VARCHAR2 | Item group |
| `YARNCODE` | VARCHAR2 | Yarn code |
| `WIDTHCODE` | VARCHAR2 | Width code |
| `WIDTHWEAVING` | NUMBER | Weaving width |
| `LABFORM` | VARCHAR2 | Lab form code |
| `WEAVE_TYPE` | VARCHAR2 | Weaving type |
| `CREATE_BY` | VARCHAR2 | Created by user |
| `CREATE_DATE` | DATE | Creation date |
| `UPDATE_BY` | VARCHAR2 | Updated by user |
| `UPDATE_DATE` | DATE | Update date |

---

## Business Logic

Validates that selected weaving item is compatible with item prepare on beam. Returns complete item specifications for loom setup.

**Purpose**: Ensure correct product specifications before starting weaving to prevent mismatched production.

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_CHECKITEMPREPARE()`
**Line**: 562

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WEAVE_CHECKITEMPREPARE(WEAVE_CHECKITEMPREPAREParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 55/296 | **Progress**: 18.6%
