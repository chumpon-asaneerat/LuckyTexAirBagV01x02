# WARP_PALLETLISTBYITMYARN

**Procedure Number**: 019 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get list of yarn pallets by item code for creel setup |
| **Operation** | SELECT |
| **Tables** | tblWarpingPallets (estimated), tblYarnStock |
| **Called From** | WarpingDataService.cs:307 → WARP_PALLETLISTBYITMYARN() |
| **Frequency** | High (every creel setup operation) |
| **Performance** | Medium (filters by item and calculates remaining stock) |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEM_YARN` | VARCHAR2(50) | ✅ | Yarn item code to filter pallets |
| `P_WARPHEADNO` | VARCHAR2(50) | ⬜ | Current warping head number (to exclude already used pallets) |

### Output (OUT)

N/A - Returns result set

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_YARN` | VARCHAR2(50) | Yarn item code |
| `RECEIVEDATE` | DATE | Date pallet was received |
| `PALLETNO` | VARCHAR2(50) | Pallet barcode/number |
| `RECEIVEWEIGHT` | NUMBER | Total received weight (kg) |
| `RECEIVECH` | NUMBER | Total received cheese count |
| `USEDWEIGHT` | NUMBER | Total used weight (kg) |
| `USEDCH` | NUMBER | Total used cheese count |
| `VERIFY` | VARCHAR2(10) | Verification status (Y/N) |
| `REJECTID` | VARCHAR2(50) | Reject reason code if any |
| `FINISHFLAG` | VARCHAR2(10) | Pallet fully used flag (Y/N) |
| `RETURNFLAG` | VARCHAR2(10) | Return to supplier flag (Y/N) |
| `REJECTCH` | NUMBER | Rejected cheese count |
| `CREATEDATE` | DATE | Pallet creation timestamp |
| `CREATEBY` | VARCHAR2(50) | Operator who created pallet |
| `CLEARBY` | VARCHAR2(50) | Operator who cleared pallet |
| `REMARK` | VARCHAR2(500) | Notes/remarks |
| `CLEARDATE` | DATE | Date pallet was cleared |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingPallets` - SELECT - Yarn pallet inventory with usage tracking
- `tblYarnStock` - SELECT - Master yarn stock data

**Transaction**: No (read-only query)

### Indexes (if relevant)

```sql
-- Expected indexes for performance
CREATE INDEX idx_pallet_itemyarn ON tblWarpingPallets(ITM_YARN, FINISHFLAG);
CREATE INDEX idx_pallet_receivedate ON tblWarpingPallets(RECEIVEDATE);
CREATE INDEX idx_pallet_warpheadno ON tblWarpingPallets(WARPHEADNO);
```

---

## Business Logic (What it does and why)

Retrieves available yarn pallets for a specific yarn item code during warping creel setup. When an operator is setting up yarn on the creel, they need to select which pallets to use. This procedure shows all pallets for the selected yarn type with remaining quantities.

**Workflow**:
1. Operator selects yarn item code on creel setup screen
2. System queries all pallets matching that yarn code
3. Calculates remaining cheese count for each pallet (RECEIVECH - USEDCH - REJECTCH)
4. Excludes pallets already allocated to current setup (via P_WARPHEADNO)
5. Returns sorted list (typically FIFO - oldest first)
6. Operator selects which pallets to use

**Business Rules**:
- Only shows pallets with remaining stock (FINISHFLAG != 'Y')
- Excludes returned pallets (RETURNFLAG = 'Y')
- Shows only verified pallets (VERIFY = 'Y') in production mode
- FIFO principle: oldest pallets (RECEIVEDATE) used first
- Each pallet tracks cheese count, not just weight
- Calculates available quantity dynamically

**Data Calculations (in C# code)**:
- `NoCH = RECEIVECH - USEDCH - REJECTCH` (available cheese count)
- `Use = NoCH` (default quantity to use - can be modified by operator)
- `Reject = 0` (default reject count - can be modified by operator)
- `Remain = NoCH - Use - Reject` (remaining after current allocation)

---

## Related Procedures

**Upstream**: [009-WARP_GETSPECBYCHOPNOANDMC.md](./009-WARP_GETSPECBYCHOPNOANDMC.md) - Gets item specs that determine which yarn to load
**Downstream**: [015-WARP_INSERTSETTINGDETAIL.md](./015-WARP_INSERTSETTINGDETAIL.md) - Saves selected pallets to creel setup
**Similar**: [008-WARP_GETREMAINPALLET.md](./008-WARP_GETREMAINPALLET.md) - Similar pallet query without warp head filter

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `WarpingDataService.cs`
**Method**: `WARP_PALLETLISTBYITMYARN()`
**Line**: 307-398

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public List<WARP_PALLETLISTBYITMYARN> WARP_PALLETLISTBYITMYARN(string P_ITEM_YARN, string P_WARPHEADNO)
{
    List<WARP_PALLETLISTBYITMYARN> results = null;

    if (!HasConnection())
        return results;

    // Prepare parameters
    WARP_PALLETLISTBYITMYARNParameter dbPara = new WARP_PALLETLISTBYITMYARNParameter();
    dbPara.P_ITEM_YARN = P_ITEM_YARN;
    dbPara.P_WARPHEADNO = P_WARPHEADNO;

    List<WARP_PALLETLISTBYITMYARNResult> dbResults = null;

    try
    {
        // Call Oracle stored procedure
        dbResults = DatabaseManager.Instance.WARP_PALLETLISTBYITMYARN(dbPara);
        if (null != dbResults)
        {
            results = new List<WARP_PALLETLISTBYITMYARN>();

            decimal RECEIVECH = 0;
            decimal USEDCH = 0;
            decimal REJECTCH = 0;

            foreach (WARP_PALLETLISTBYITMYARNResult dbResult in dbResults)
            {
                WARP_PALLETLISTBYITMYARN inst = new WARP_PALLETLISTBYITMYARN();

                inst.IsSelect = false; // UI checkbox state
                inst.ITM_YARN = dbResult.ITM_YARN;
                inst.RECEIVEDATE = dbResult.RECEIVEDATE;
                inst.PALLETNO = dbResult.PALLETNO;
                inst.RECEIVEWEIGHT = dbResult.RECEIVEWEIGHT;
                inst.USEDWEIGHT = dbResult.USEDWEIGHT;
                inst.VERIFY = dbResult.VERIFY;
                inst.REJECTID = dbResult.REJECTID;
                inst.FINISHFLAG = dbResult.FINISHFLAG;
                inst.RETURNFLAG = dbResult.RETURNFLAG;
                inst.CREATEDATE = dbResult.CREATEDATE;
                inst.CREATEBY = dbResult.CREATEBY;
                inst.CLEARBY = dbResult.CLEARBY;
                inst.REMARK = dbResult.REMARK;
                inst.CLEARDATE = dbResult.CLEARDATE;

                // Handle nullable decimals
                RECEIVECH = dbResult.RECEIVECH ?? 0;
                USEDCH = dbResult.USEDCH ?? 0;
                REJECTCH = dbResult.REJECTCH ?? 0;

                inst.RECEIVECH = RECEIVECH;
                inst.USEDCH = USEDCH;
                inst.REJECTCH = REJECTCH;

                // Calculate remaining quantity
                inst.NoCH = (RECEIVECH - USEDCH - REJECTCH);
                inst.Use = inst.NoCH; // Default: use all remaining
                inst.Reject = 0;      // Default: no rejects
                inst.Remain = (inst.NoCH - inst.Use - inst.Reject);

                results.Add(inst);
            }
        }
    }
    catch (Exception ex)
    {
        ex.Err();
    }

    return results;
}
```

---

**File**: 019/296 | **Progress**: 6.4%
