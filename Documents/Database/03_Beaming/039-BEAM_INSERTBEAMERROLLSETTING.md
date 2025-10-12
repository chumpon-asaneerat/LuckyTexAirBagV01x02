# BEAM_INSERTBEAMERROLLSETTING

**Procedure Number**: 039 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create warp-to-beam mapping for traceability |
| **Operation** | INSERT |
| **Tables** | tblBeamerRollSetting |
| **Called From** | BeamingDataService.cs:1504 → BEAM_INSERTBEAMERROLLSETTING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERNO` | VARCHAR2(50) | ✅ | Beamer setup/batch number |
| `P_WARPERHEADNO` | VARCHAR2(50) | ✅ | Warp setup/batch number (from M02-Warping) |
| `P_WARPLOT` | VARCHAR2(50) | ✅ | Individual warp roll barcode |

### Output (OUT)

None (Returns boolean via procedure result)

### Returns

| Type | Description |
|------|-------------|
| `Boolean` | True if insert successful, False if failed |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblBeamerRollSetting` - INSERT - Creates warp-to-beam mapping record

**Transaction**: Should be used (multiple warp rolls mapped per setup)

### Indexes (if relevant)

```sql
-- Recommended composite unique index to prevent duplicates
CREATE UNIQUE INDEX idx_beamerrollsetting_unique
ON tblBeamerRollSetting(BEAMERNO, WARPHEADNO, WARPERLOT);

-- Also useful for traceability queries
CREATE INDEX idx_beamerrollsetting_beamerno ON tblBeamerRollSetting(BEAMERNO);
CREATE INDEX idx_beamerrollsetting_warperlot ON tblBeamerRollSetting(WARPERLOT);
```

---

## Business Logic (What it does and why)

Creates **traceability mapping** between warp rolls (from M02-Warping) and beaming setup, enabling forward/backward material tracking.

**Purpose**: When operator creates a new beaming setup and selects which warp rolls to use, this procedure records each warp roll mapping. This creates the critical traceability link:

```
Warp Roll (WARPERLOT) → Beaming Setup (BEAMERNO) → Beam Rolls (BEAMLOT) → Fabric (DOFFNO)
```

**When Used**:
- **New Beaming Setup**: After operator selects warp rolls, system calls this procedure for EACH selected warp roll
- **Batch Operation**: Called multiple times (typically 2-10 warp rolls per beaming setup)
- **Setup Wizard**: Part of beaming setup creation workflow

**Business Rules**:
- Called once per warp roll in the beaming setup
- Three parameters required (no nulls allowed)
- If any parameter is empty/null, operation fails (returns false)
- Should be wrapped in transaction with other setup creation steps
- Creates permanent traceability record (typically not deleted)

**Data Flow (Typical Setup with 5 Warp Rolls)**:
1. Operator creates beaming setup (BEAM_INSERTBEAMNO)
2. Operator selects 5 warp rolls from list
3. System calls this procedure 5 times:
   - Insert mapping: BEAMERNO + WARPHEADNO1 + WARPERLOT1
   - Insert mapping: BEAMERNO + WARPHEADNO1 + WARPERLOT2
   - Insert mapping: BEAMERNO + WARPHEADNO1 + WARPERLOT3
   - Insert mapping: BEAMERNO + WARPHEADNO2 + WARPERLOT4
   - Insert mapping: BEAMERNO + WARPHEADNO2 + WARPERLOT5
4. All mappings committed in single transaction
5. Later: BEAM_GETWARPROLLBYBEAMERNO retrieves all 5 mappings

---

## Related Procedures

**Upstream**:
- [037-BEAM_GETWARPNOBYITEMPREPARE.md](./037-BEAM_GETWARPNOBYITEMPREPARE.md) - Gets warp setups for selection
- [WARP_GETWARPERLOTBYHEADNO.md](./WARP_GETWARPERLOTBYHEADNO.md) - Gets individual warp rolls
- [BEAM_INSERTBEAMNO.md](./BEAM_INSERTBEAMNO.md) - Creates beaming setup first

**Downstream**:
- [038-BEAM_GETWARPROLLBYBEAMERNO.md](./038-BEAM_GETWARPROLLBYBEAMERNO.md) - Retrieves the mappings created here
- Traceability reports use this data extensively

**Similar**:
- No exact equivalent (beaming-specific traceability operation)

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_INSERTBEAMERROLLSETTING()`
**Line**: 1502-1539

**Query Type**: INSERT via DatabaseManager wrapper

```csharp
// Method signature
public bool BEAM_INSERTBEAMERROLLSETTING(string P_BEAMERNO, string P_WARPERHEADNO, string P_WARPLOT)
{
    bool result = false;

    // Input validation - all 3 parameters required
    if (string.IsNullOrWhiteSpace(P_BEAMERNO))
        return result; // Returns false

    if (string.IsNullOrWhiteSpace(P_WARPERHEADNO))
        return result; // Returns false

    if (!HasConnection())
        return result;

    // Parameter setup
    BEAM_INSERTBEAMERROLLSETTINGParameter dbPara = new BEAM_INSERTBEAMERROLLSETTINGParameter();
    dbPara.P_BEAMERNO = P_BEAMERNO;
    dbPara.P_WARPERHEADNO = P_WARPERHEADNO;
    dbPara.P_WARPLOT = P_WARPLOT;

    // Execute insert
    dbResult = DatabaseManager.Instance.BEAM_INSERTBEAMERROLLSETTING(dbPara);

    result = (null != dbResult); // Returns true if successful

    return result;
}
```

**Usage Pattern** (Called in Loop):
```csharp
// In UI code-behind (BeamingSetupPage.xaml.cs)
foreach (var selectedWarpRoll in selectedWarpRolls)
{
    bool success = BeamingDataService.Instance.BEAM_INSERTBEAMERROLLSETTING(
        beamerNo,
        selectedWarpRoll.WARPHEADNO,
        selectedWarpRoll.WARPERLOT
    );

    if (!success) {
        // Rollback transaction
        // Show error to operator
    }
}
```

---

**File**: 39/296 | **Progress**: 13.2%
