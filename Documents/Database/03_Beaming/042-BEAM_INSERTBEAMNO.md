# BEAM_INSERTBEAMNO

**Procedure Number**: 042 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new beaming setup/batch (header record) |
| **Operation** | INSERT |
| **Called From** | BeamingDataService.cs:1121 → BEAM_INSERTBEAMNO() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMNO` | VARCHAR2(50) | ✅ | Beamer setup number (user-entered or auto-generated) |
| `P_WARPERHEADNO` | VARCHAR2(50) | ✅ | Warp setup number (from M02-Warping) |
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code (product specification) |
| `P_PRODUCTID` | VARCHAR2(50) | ✅ | Product type ID |
| `P_MCNO` | VARCHAR2(50) | ✅ | Beaming machine code |
| `P_TOTALYARN` | NUMBER | ⬜ | Total yarn ends count |
| `P_TOTALKEBA` | NUMBER | ⬜ | Total keba (yarn groups) |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator creating the setup |
| `P_ADJUSTKEBA` | NUMBER | ⬜ | Adjusted keba value |
| `P_REMARK` | VARCHAR2(200) | ⬜ | Setup remarks/notes |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Generated BEAMERNO or error message |

### Returns

| Type | Description |
|------|-------------|
| `String` | Generated BEAMERNO if successful, empty if failed |

---

## Business Logic (What it does and why)

Creates a new beaming setup/batch header when operator starts a new beaming operation. This is the **first step** in the beaming production workflow.

**Purpose**: Before producing beam rolls, operator must create a beaming setup that defines:
- Which product to produce (item prepare)
- Which machine to use
- Which warp beams to combine
- Production parameters and specifications

This procedure generates a unique BEAMERNO that identifies this setup batch.

**When Used**:
- **New Beaming Setup Wizard**: First step in creating new beaming batch
- **One Setup → Multiple Beams**: One header record, many detail records
- **Infrequent**: Called only when starting new setup (not per beam)

**Business Rules**:
- All required parameters must be provided (8 required fields)
- Empty/null P_BEAMNO causes failure (returns empty string)
- Auto-generates BEAMERNO (unique identifier for this setup batch)
- Initial STATUS set to 'S' (Setting) - not yet producing
- CREATEDATE auto-captured (timestamp)
- FINISHFLAG initially '0' (not finished)
- NOWARPBEAM starts at 0 (incremented as beams are produced)

**Workflow Sequence**:
1. **BEAM_INSERTBEAMNO** ← Creates setup header (THIS PROCEDURE)
2. BEAM_INSERTBEAMERROLLSETTING ← Links warp rolls to setup (called multiple times)
3. BEAM_INSERTBEAMINGDETAIL ← Starts first beam production (generates BEAMLOT)
4. BEAM_UPDATEBEAMDETAIL ← Updates beam data during/after production
5. (Repeat steps 3-4 for each beam in the setup)
6. BEAM_UPDATEBEAMNO ← Marks setup as complete

---

## Related Procedures

**Upstream**:
- [035-BEAM_GETSPECBYCHOPNO.md](./035-BEAM_GETSPECBYCHOPNO.md) - Gets specifications before creating setup
- [037-BEAM_GETWARPNOBYITEMPREPARE.md](./037-BEAM_GETWARPNOBYITEMPREPARE.md) - Gets warp setups to select from

**Downstream**:
- [039-BEAM_INSERTBEAMERROLLSETTING.md](./039-BEAM_INSERTBEAMERROLLSETTING.md) - Links warp rolls after creating setup
- [040-BEAM_INSERTBEAMINGDETAIL.md](./040-BEAM_INSERTBEAMINGDETAIL.md) - Starts beam production
- [030-BEAM_GETBEAMERMCSTATUS.md](./030-BEAM_GETBEAMERMCSTATUS.md) - Retrieves setup status
- [BEAM_UPDATEBEAMNO.md](./BEAM_UPDATEBEAMNO.md) - Updates setup status

**Similar**:
- [016-WARP_INSERTSETTINGHEAD.md](../02_Warping/016-WARP_INSERTSETTINGHEAD.md) - Warping setup header creation

---

## Query/Code Location

**Note**: This application uses Oracle stored procedures exclusively for all database operations.

### Data Service Layer
**File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `BEAM_INSERTBEAMNO()`
**Line**: 1119-1162

### Database Manager
**File**: `LuckyTex.AirBag.Core\Services\DataService\DatabaseManager.cs`
**Method**: BEAM_INSERTBEAMNOParameter
**Purpose**: Executes Oracle stored procedure and returns result set

---

**File**: 42/296 | **Progress**: 14.2%
