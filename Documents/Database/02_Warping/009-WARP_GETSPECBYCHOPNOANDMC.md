# WARP_GETSPECBYCHOPNOANDMC

**Procedure Number**: 009 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get warping specifications by item prepare and machine |
| **Operation** | SELECT |
| **Tables** | tblWarpingSpec, tblItemPrepare, tblMachine |
| **Called From** | WarpingDataService.cs:243 → WARP_GETSPECBYCHOPNOANDMC() |
| **Frequency** | High (loading production specs for setup) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMPREPARE` | VARCHAR2(50) | ✅ | Item preparation code (item+specification combination) |
| `P_MCNO` | VARCHAR2(50) | ✅ | Warping machine number |

### Output (OUT)

None - Returns result set via cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `CHOPNO` | VARCHAR2(50) | Chop number / specification ID |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `WARPERENDS` | NUMBER | Number of warper ends |
| `MAXLENGTH` | NUMBER | Maximum beam length (meters) |
| `MINLENGTH` | NUMBER | Minimum beam length (meters) |
| `WAXING` | VARCHAR2(10) | Waxing requirement (Y/N) |
| `COMBTYPE` | VARCHAR2(20) | Comb type |
| `COMBPITCH` | NUMBER | Comb pitch |
| `KEBAYARN` | NUMBER | Keba yarn count |
| `NOWARPBEAM` | NUMBER | Number of warp beams |
| `MAXHARDNESS` | NUMBER | Maximum hardness (1-10) |
| `MINHARDNESS` | NUMBER | Minimum hardness (1-10) |
| `MCNO` | VARCHAR2(50) | Machine number |
| `SPEED` | NUMBER | Production speed (m/min) |
| `SPEED_MARGIN` | NUMBER | Speed tolerance (±) |
| `YARN_TENSION` | NUMBER | Yarn tension setting |
| `YARN_TENSION_MARGIN` | NUMBER | Yarn tension tolerance (±) |
| `WINDING_TENSION` | NUMBER | Winding tension setting |
| `WINDING_TENSION_MARGIN` | NUMBER | Winding tension tolerance (±) |
| `NOCH` | NUMBER | Number of cheese |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingSpec` - SELECT - Warping specifications by item and machine
- `tblItemPrepare` - SELECT JOIN - Item preparation details
- `tblMachine` - SELECT JOIN - Machine-specific parameters

**Transaction**: No (read-only operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpingspec_itm_mc ON tblWarpingSpec(ITM_PREPARE, MCNO);
CREATE INDEX idx_itemprepare_code ON tblItemPrepare(ITM_PREPARE);
```

---

## Business Logic (What it does and why)

Retrieves complete warping specifications for a specific item-machine combination. When operator starts new warping setup, system loads all technical parameters required for production including speed, tension, hardness ranges, and material requirements. These specs guide operator in machine setup and provide quality control limits.

**Workflow**:
1. Operator selects item preparation code and target machine
2. System queries warping specifications for this combination
3. Procedure retrieves all technical parameters:
   - Physical specs (warper ends, length range, comb details)
   - Process parameters (speed, tensions with margins)
   - Quality limits (hardness range)
   - Material requirements (yarn type, waxing, cheese count)
4. Returns complete specification set
5. UI displays specs to operator for machine setup
6. System uses specs for validation during production

**Business Rules**:
- Specifications are item+machine specific
- Includes tolerance margins for critical parameters
- Speed and tensions have acceptable ranges (±margin)
- Hardness must be within min/max limits
- Operator must configure machine to match specs
- System validates actual values against specs during production

---

## Related Procedures

**Upstream**: None - Entry point for setup
**Downstream**: [015-WARP_INSERTSETTINGHEAD.md](./015-WARP_INSERTSETTINGHEAD.md) - Creates setup with these specs
**Similar**: [BEAM_GETSPECBYCHOPNO.md](../03_Beaming/BEAM_GETSPECBYCHOPNO.md) - Similar spec retrieval for beaming

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_GETSPECBYCHOPNOANDMC()`
**Line**: 243-297

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<WARP_GETSPECBYCHOPNOANDMC> WARP_GETSPECBYCHOPNOANDMC(string P_ITMPREPARE, string P_MCNO)
{
    List<WARP_GETSPECBYCHOPNOANDMC> results = null;

    if (!HasConnection())
        return results;

    WARP_GETSPECBYCHOPNOANDMCParameter dbPara = new WARP_GETSPECBYCHOPNOANDMCParameter();
    dbPara.P_ITMPREPARE = P_ITMPREPARE;
    dbPara.P_MCNO = P_MCNO;

    try
    {
        dbResults = DatabaseManager.Instance.WARP_GETSPECBYCHOPNOANDMC(dbPara);
        if (null != dbResults)
        {
            results = new List<WARP_GETSPECBYCHOPNOANDMC>();
            foreach (var dbResult in dbResults)
            {
                // Map 19 columns - all critical production parameters
                inst.CHOPNO = dbResult.CHOPNO;
                inst.ITM_YARN = dbResult.ITM_YARN;
                inst.WARPERENDS = dbResult.WARPERENDS;
                inst.MAXLENGTH = dbResult.MAXLENGTH;
                inst.MINLENGTH = dbResult.MINLENGTH;
                inst.WAXING = dbResult.WAXING;
                inst.COMBTYPE = dbResult.COMBTYPE;
                inst.COMBPITCH = dbResult.COMBPITCH;
                inst.KEBAYARN = dbResult.KEBAYARN;
                inst.NOWARPBEAM = dbResult.NOWARPBEAM;
                inst.MAXHARDNESS = dbResult.MAXHARDNESS;
                inst.MINHARDNESS = dbResult.MINHARDNESS;
                inst.MCNO = dbResult.MCNO;
                inst.SPEED = dbResult.SPEED;
                inst.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                inst.YARN_TENSION = dbResult.YARN_TENSION;
                inst.YARN_TENSION_MARGIN = dbResult.YARN_TENSION_MARGIN;
                inst.WINDING_TENSION = dbResult.WINDING_TENSION;
                inst.WINDING_TENSION_MARGIN = dbResult.WINDING_TENSION_MARGIN;
                inst.NOCH = dbResult.NOCH;

                results.Add(inst);
            }
        }
    }
    catch (Exception ex) { ex.Err(); }

    return results;
}
```

---

**File**: 009/296 | **Progress**: 3.0%
