# WARP_TRANFERSLIP

**Procedure Number**: 022 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get transfer slip data for completed warp beam (for printing/reporting) |
| **Operation** | SELECT |
| **Called From** | WarpingDataService.cs:606 → WARP_TRANFERSLIP() |
| **Frequency** | Medium (every warp beam completion for transfer documentation) |
| **Performance** | Fast (single beam lookup) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Warping lot number (beam barcode) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2(50) | Warping head number |
| `WARPERLOT` | VARCHAR2(50) | Warper lot number (beam barcode) |
| `BEAMNO` | VARCHAR2(50) | Beam number |
| `SIDE` | VARCHAR2(10) | Creel side (A or B) |
| `STARTDATE` | DATE | Warping start date/time |
| `ENDDATE` | DATE | Warping completion date/time |
| `LENGTH` | NUMBER | Beam length in meters |
| `SPEED` | NUMBER | Average warping speed |
| `HARDNESS_L` | NUMBER | Beam hardness left side |
| `HARDNESS_N` | NUMBER | Beam hardness center |
| `HARDNESS_R` | NUMBER | Beam hardness right side |
| `TENSION` | NUMBER | Yarn tension during warping |
| `STARTBY` | VARCHAR2(50) | Operator who started warping |
| `DOFFBY` | VARCHAR2(50) | Operator who completed beam |
| `FLAG` | VARCHAR2(10) | Process flag/status |
| `WARPMC` | VARCHAR2(50) | Warping machine number |
| `ITM_PREPARE` | VARCHAR2(50) | Item prepare code (product) |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code used |

---

## Business Logic (What it does and why)

Retrieves complete production data for a warp beam to generate transfer documentation (transfer slip). When a warp beam is completed and ready to move to the next production stage (beaming), this slip documents all production parameters and provides traceability.

**Workflow**:
1. Operator completes warp beam production (doff operation)
2. System prompts to print transfer slip
3. Operator clicks print transfer slip button
4. System calls this procedure with WARPHEADNO and WARPERLOT
5. Retrieves all production data and item information
6. Generates transfer slip report showing:
   - Beam identification (barcode, beam number)
   - Product information (item prepare, yarn type)
   - Production parameters (length, speed, tension, hardness)
   - Operators (who started, who doffed)
   - Timestamps (start/end dates)
7. Slip printed as barcode label or paper document
8. Beam physically transferred to beaming department with slip

**Business Rules**:
- Transfer slip required for material traceability
- Documents quality parameters (hardness L/N/R, tension, speed)
- Links yarn input (ITM_YARN) to warp beam output (WARPERLOT)
- Provides operator accountability (STARTBY, DOFFBY)
- Slip stays with beam through production process
- May be scanned at next operation to verify beam identity

**Use Cases**:
- Print transfer slip after beam completion
- Verify beam identity at beaming operation
- Quality issue investigation (trace back to warping parameters)
- Production reporting and analysis
- Operator performance tracking

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Creates the process record being queried
**Downstream**: BEAM_* procedures - Beaming operations that receive this beam
**Similar**: BEAM_TRANFERSLIP - Transfer slip for beaming to drawing

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\WarpingDataService.cs`
**Method**: `WARP_TRANFERSLIP()`
**Line**: 606-659

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: WARP_TRANFERSLIPParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 022/296 | **Progress**: 7.4%
