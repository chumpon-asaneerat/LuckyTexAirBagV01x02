# WARP_INSERTSETTINGHEAD

**Procedure Number**: 016 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new warping creel setup header record |
| **Operation** | INSERT |
| **Called From** | WarpingDataService.cs:1492 → WARP_INSERTSETTINGHEAD() |
| **Frequency** | Medium (starting new warping setup) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item preparation code |
| `P_PRODUCTID` | VARCHAR2(50) | ⬜ | Product type ID |
| `P_MCNO` | VARCHAR2(50) | ⬜ | Machine number |
| `P_SIDE` | VARCHAR2(10) | ⬜ | Side (A or B) |
| `P_ACTUALCH` | NUMBER | ⬜ | Actual cheese count |
| `P_WTYPE` | VARCHAR2(20) | ⬜ | Warping type |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator who creates setup |
| `P_WARPERHEADNO` | VARCHAR2(50) | ⬜ | Warper head number (if predefined) |
| `P_REEDNO` | VARCHAR2(50) | ⬜ | Reed number |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Generated warping head number or error message |

### Returns (if cursor)

N/A - Returns single string (new head number or error)

---

## Business Logic (What it does and why)

Creates master header record for new warping creel setup. This is the first step when operator begins configuring creel for production - establishes the setup record that will link all pallets, specifications, and production data. Generates unique head number that identifies this specific setup throughout its lifecycle.

**Workflow**:
1. Operator starts new warping setup
2. Selects item preparation code, machine, and side
3. Specifies setup parameters (cheese count, type, reed)
4. System calls this procedure to create header
5. Procedure:
   - Generates unique warping head number (or uses provided)
   - Creates header record with all parameters
   - Records operator who created setup
   - Sets initial status
   - Returns new head number to UI
6. Operator then uses head number to allocate pallets (INSERTSETTINGDETAIL)
7. Head number links all related records (setup → pallets → production)

**Business Rules**:
- Item preparation code required (what to produce)
- Generates unique head number if not provided
- One setup per machine/side combination at a time
- Records creation timestamp and operator
- Status initially "SETUP" or "PENDING"
- Head number used for all subsequent operations

---

## Related Procedures

**Upstream**: [009-WARP_GETSPECBYCHOPNOANDMC.md](./009-WARP_GETSPECBYCHOPNOANDMC.md) - Load specs before creating setup
**Downstream**: [015-WARP_INSERTSETTINGDETAIL.md](./015-WARP_INSERTSETTINGDETAIL.md) - Allocate pallets to this setup
**Similar**: [BEAM_INSERTBEAMNO.md](../03_Beaming/BEAM_INSERTBEAMNO.md) - Similar header creation

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_INSERTSETTINGHEAD()`
**Line**: 1492-1529

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_INSERTSETTINGHEAD(WARP_INSERTSETTINGHEADParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 016/296 | **Progress**: 5.4%
