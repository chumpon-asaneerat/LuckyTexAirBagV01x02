# BEAM_GETWARPNOBYITEMPREPARE

**Procedure Number**: 037 | **Module**: M03 - Beaming | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get available warp head setups for beaming by item prepare |
| **Operation** | SELECT |
| **Tables** | tblWarpingHead, tblWarpingDetail |
| **Called From** | BeamingDataService.cs:213 → BEAM_GETWARPNOBYITEMPREPARE() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item prepare code (product specification) |

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WARPHEADNO` | VARCHAR2 | Warp setup/batch number (from M02-Warping) |
| `STARTDATE` | DATE | Warping start date |
| `WARPMC` | VARCHAR2 | Warping machine code |
| `ACTUALCH` | NUMBER | Actual channel/ends count |
| `TOTALBEAM` | NUMBER | Total warp beams produced in this setup |

**Note**: Model includes additional field `IsSelect` (Boolean) - added in C# code for UI selection

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingHead` - SELECT - Warping setup header data
- `tblWarpingDetail` - SELECT - Warping production details (to count beams)

**Transaction**: No (Read-only query)

### Indexes (if relevant)

```sql
-- Recommended composite index
CREATE INDEX idx_warpinghead_itmprepare_status ON tblWarpingHead(ITMPREPARE, STATUS);
```

---

## Business Logic (What it does and why)

Retrieves list of available warp setups that can be used for beaming when operator starts a new beaming batch.

**Purpose**: Beaming combines multiple warp beams (from M02-Warping) into a single larger beam. When operator starts beaming:
1. Selects the item prepare code (product)
2. This procedure shows all warp setups for that product
3. Operator selects which warp setup(s) to use for beaming
4. System validates that warp beams are available for beaming

**When Used**:
- **New Beaming Setup**: Operator selects warp beams to combine
- **Warp Selection Screen**: Displays available warp setups as dropdown/list
- **Production Planning**: Shows which warp batches are ready for beaming
- **Traceability**: Links beaming production back to specific warp setups

**Business Rules**:
- Only shows warp setups with matching item prepare code
- TOTALBEAM shows how many warp beams were produced in that setup
- System may filter by warp status (completed, not yet used for beaming)
- Multiple warp setups can exist for the same item prepare
- Operator can select warp beams from different WARPHEADNO values

**Data Flow**:
1. Operator selects item prepare code
2. This procedure retrieves all warp setups for that item
3. UI displays list with checkbox selection (IsSelect field)
4. Operator checks which warp setup(s) to use
5. System calls BEAM_GETWARPERLOTBYHEADNO to get individual warp beams
6. Operator proceeds to create beaming setup

---

## Related Procedures

**Upstream**:
- [035-BEAM_GETSPECBYCHOPNO.md](./035-BEAM_GETSPECBYCHOPNO.md) - Gets beaming specs for the item prepare
- [016-WARP_INSERTSETTINGHEAD.md](../02_Warping/016-WARP_INSERTSETTINGHEAD.md) - Created the warp setup (upstream process)

**Downstream**:
- [WARP_GETWARPERLOTBYHEADNO.md](./WARP_GETWARPERLOTBYHEADNO.md) - Gets individual warp beams from selected setup(s)
- [BEAM_INSERTBEAMNO.md](./BEAM_INSERTBEAMNO.md) - Creates beaming setup using selected warp beams
- [BEAM_INSERTBEAMERROLLSETTING.md](./BEAM_INSERTBEAMERROLLSETTING.md) - Links warp beams to beaming setup

**Similar**:
- No direct equivalent (beaming-specific cross-module query)

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `BeamingDataService.cs`
**Method**: `BEAM_GETWARPNOBYITEMPREPARE()`
**Line**: 207-254

**Query Type**: SELECT via DatabaseManager wrapper

```csharp
// Method signature
public List<BEAM_GETWARPNOBYITEMPREPARE> BEAM_GETWARPNOBYITEMPREPARE(string P_ITMPREPARE)
{
    // Connection validation
    if (!HasConnection())
        return results;

    // Parameter setup
    BEAM_GETWARPNOBYITEMPREPAREParameter dbPara = new BEAM_GETWARPNOBYITEMPREPAREParameter();
    dbPara.P_ITMPREPARE = P_ITMPREPARE;

    // Execute via DatabaseManager
    dbResults = DatabaseManager.Instance.BEAM_GETWARPNOBYITEMPREPARE(dbPara);

    // Maps to model and adds UI field
    foreach (BEAM_GETWARPNOBYITEMPREPAREResult dbResult in dbResults)
    {
        inst.IsSelect = false; // UI checkbox state (not from database)
        inst.WARPHEADNO = dbResult.WARPHEADNO;
        inst.STARTDATE = dbResult.STARTDATE;
        inst.WARPMC = dbResult.WARPMC;
        inst.ACTUALCH = dbResult.ACTUALCH;
        inst.TOTALBEAM = dbResult.TOTALBEAM;
    }

    // Returns List<BEAM_GETWARPNOBYITEMPREPARE> for selection UI
}
```

---

**File**: 37/296 | **Progress**: 12.5%
