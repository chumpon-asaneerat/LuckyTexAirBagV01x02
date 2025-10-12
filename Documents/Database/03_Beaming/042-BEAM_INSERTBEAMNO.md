# BEAM_INSERTBEAMNO

**Procedure Number**: 042 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new beaming setup/batch (header record) |
| **Operation** | INSERT |
| **Tables** | tblBeamingHead |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamingHead` - INSERT - Creates beaming setup header record

**Transaction**: Yes (should be wrapped with warp roll mappings)

### Indexes (if relevant)

```sql
-- Recommended unique index on generated setup number
CREATE UNIQUE INDEX idx_beaminghead_beamerno ON tblBeamingHead(BEAMERNO);

-- Also useful for machine status queries
CREATE INDEX idx_beaminghead_mcno_status ON tblBeamingHead(MCNO, STATUS);
CREATE INDEX idx_beaminghead_itmprepare ON tblBeamingHead(ITMPREPARE);
```

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

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_INSERTBEAMNO()`
**Line**: 1119-1162

**Query Type**: INSERT via DatabaseManager wrapper

```csharp
// Method signature
public string BEAM_INSERTBEAMNO(
    string P_BEAMNO,
    string P_WARPERHEADNO,
    string P_ITMPREPARE,
    string P_PRODUCTID,
    string P_MCNO,
    decimal? P_TOTALYARN,
    decimal? P_TOTALKEBA,
    string P_OPERATOR,
    decimal? P_ADJUSTKEBA,
    string P_REMARK)
{
    string result = string.Empty;

    // Input validation - P_BEAMNO required
    if (string.IsNullOrWhiteSpace(P_BEAMNO))
        return result; // Returns empty string

    if (!HasConnection())
        return result;

    // Parameter setup (10 parameters)
    BEAM_INSERTBEAMNOParameter dbPara = new BEAM_INSERTBEAMNOParameter();
    dbPara.P_BEAMNO = P_BEAMNO;
    dbPara.P_WARPERHEADNO = P_WARPERHEADNO;
    dbPara.P_ITMPREPARE = P_ITMPREPARE;
    dbPara.P_PRODUCTID = P_PRODUCTID;
    dbPara.P_MCNO = P_MCNO;
    dbPara.P_TOTALYARN = P_TOTALYARN;
    dbPara.P_TOTALKEBA = P_TOTALKEBA;
    dbPara.P_OPERATOR = P_OPERATOR;
    dbPara.P_ADJUSTKEBA = P_ADJUSTKEBA;
    dbPara.P_REMARK = P_REMARK;

    // Execute insert - generates BEAMERNO
    dbResult = DatabaseManager.Instance.BEAM_INSERTBEAMNO(dbPara);

    result = dbResult.R_RESULT; // Returns generated BEAMERNO

    return result;
}
```

**Usage Pattern**:
```csharp
// In UI Setup Wizard (BeamingSetupPage.xaml.cs)
// Step 1: Create setup header
string beamerNo = BeamingDataService.Instance.BEAM_INSERTBEAMNO(
    beamNo,              // User-entered or auto-generated
    warpHeadNo,          // Selected warp setup
    itemPrepare,         // Selected product
    productTypeId,       // Product type
    machineCode,         // Selected machine
    totalYarn,           // From specs
    totalKeba,           // From specs
    currentOperatorID,   // Logged-in operator
    adjustedKeba,        // Optional adjustment
    remarks              // Optional notes
);

if (!string.IsNullOrEmpty(beamerNo))
{
    // Step 2: Link warp rolls
    foreach (var warpRoll in selectedWarpRolls)
    {
        BeamingDataService.Instance.BEAM_INSERTBEAMERROLLSETTING(
            beamerNo, warpRoll.WARPHEADNO, warpRoll.WARPERLOT
        );
    }

    MessageBox.Show($"Setup created: {beamerNo}");
    NavigateToProductionScreen(beamerNo);
}
```

---

**File**: 42/296 | **Progress**: 14.2%
