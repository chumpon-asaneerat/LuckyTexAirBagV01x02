# WARP_INSERTSETTINGHEAD

**Procedure Number**: 016 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new warping creel setup header record |
| **Operation** | INSERT |
| **Tables** | tblWarpingHead |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingHead` - INSERT - New setup header record

**Transaction**: Yes (should be atomic operation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE UNIQUE INDEX uk_warpinghead_headno ON tblWarpingHead(WARPHEADNO);
CREATE INDEX idx_warpinghead_item_mc ON tblWarpingHead(ITM_PREPARE, WARPMC, SIDE);
```

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

**File**: `WarpingDataService.cs`
**Method**: `WARP_INSERTSETTINGHEAD()`
**Line**: 1492-1529

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public string WARP_INSERTSETTINGHEAD(
    string P_ITMPREPARE, string P_PRODUCTID, string P_MCNO, string P_SIDE,
    decimal? P_ACTUALCH, string P_WTYPE, string P_OPERATOR,
    string P_WARPERHEADNO, string P_REEDNO)
{
    string result = string.Empty;

    // Validation: item prepare code required
    if (string.IsNullOrWhiteSpace(P_ITMPREPARE))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_INSERTSETTINGHEADParameter dbPara = new WARP_INSERTSETTINGHEADParameter();
    dbPara.P_ITMPREPARE = P_ITMPREPARE;
    dbPara.P_PRODUCTID = P_PRODUCTID;
    dbPara.P_MCNO = P_MCNO;
    dbPara.P_SIDE = P_SIDE;
    dbPara.P_ACTUALCH = P_ACTUALCH;
    dbPara.P_WTYPE = P_WTYPE;
    dbPara.P_OPERATOR = P_OPERATOR;
    dbPara.P_WARPERHEADNO = P_WARPERHEADNO;
    dbPara.P_REEDNO = P_REEDNO;

    WARP_INSERTSETTINGHEADResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_INSERTSETTINGHEAD(dbPara);

        // Return generated head number or error message
        result = dbResult.R_RESULT;
    }
    catch (Exception ex)
    {
        ex.Err();
        result = string.Empty;
    }

    return result;
}
```

---

**File**: 016/296 | **Progress**: 5.4%
