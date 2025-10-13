# WARP_INSERTWARPINGPROCESS

**Procedure Number**: 017 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Start new warping production lot (beam) |
| **Operation** | INSERT |
| **Called From** | WarpingDataService.cs:1660 → WARP_INSERTWARPINGPROCESS() |
| **Frequency** | Medium (starting new beam production) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number (setup ID) |
| `P_WARPMC` | VARCHAR2(50) | ⬜ | Warping machine number |
| `P_BEAMNO` | VARCHAR2(50) | ⬜ | Beam number (barcode) |
| `P_SIDE` | VARCHAR2(10) | ⬜ | Side (A or B) |
| `P_STARTDATE` | DATE | ⬜ | Production start date/time |
| `P_STARTBY` | VARCHAR2(50) | ⬜ | Operator who starts production |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_WRAPLOT` | VARCHAR2(50) | Generated warper lot number |
| `RESULT` | VARCHAR2(50) | Result code (success/error) |

### Returns (if cursor)

N/A - Returns object with lot number and result code

---

## Business Logic (What it does and why)

Initiates new warping production run by creating production lot record. When operator starts producing a beam from creel setup, this creates the lot/beam record that will track the entire production process. Generates unique warper lot number and links to setup header. Critical for production tracking and traceability.

**Workflow**:
1. Creel setup complete (header + pallets allocated)
2. Operator ready to start beam production
3. Scans or enters beam number
4. Presses "Start Production" button
5. System calls this procedure to create lot:
   - Generates unique warper lot number
   - Creates production record linked to setup (WARPHEADNO)
   - Records beam number (barcode for finished beam)
   - Records machine, side, start time, operator
   - Sets initial status (IN_PROCESS)
6. Returns generated lot number to UI
7. Lot number used for all production updates (hardness, speed, length, etc.)
8. Production metrics recorded against this lot number

**Business Rules**:
- Warping head number required (which setup)
- Generates unique warper lot number
- One lot = one beam being produced
- Multiple lots can be produced from same setup
- Start timestamp recorded for production tracking
- Operator accountability (who started)
- Links to beam barcode for downstream traceability

---

## Related Procedures

**Upstream**: [016-WARP_INSERTSETTINGHEAD.md](./016-WARP_INSERTSETTINGHEAD.md) - Setup must exist first
**Downstream**: [025-WARP_UPDATEWARPINGPROCESS.md](./025-WARP_UPDATEWARPINGPROCESS.md) - Update production metrics
**Similar**: [WEAVE_WEAVINGPROCESS.md](../05_Weaving/WEAVE_WEAVINGPROCESS.md) - Similar production start

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_INSERTWARPINGPROCESS()`
**Line**: 1660-1700

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: `WARP_INSERTWARPINGPROCESS(WARP_INSERTWARPINGPROCESSParameter)`
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 017/296 | **Progress**: 5.7%
