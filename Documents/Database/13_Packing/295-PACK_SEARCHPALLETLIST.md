# PACK_SEARCHPALLETLIST

**Procedure Number**: 295 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search packing pallets by number, date, or status |
| **Operation** | SELECT |
| **Tables** | tblPackingPallet (assumed) |
| **Called From** | PackingDataService.cs → PACK_SEARCHPALLETLIST() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLET` | VARCHAR2(50) | ⬜ | Pallet number filter (optional) |
| `P_DATE` | VARCHAR2(20) | ⬜ | Packing date filter (optional) |
| `P_STATUS` | VARCHAR2(10) | ⬜ | Status flag filter (optional) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PALLETNO` | VARCHAR2(50) | Packing pallet number |
| `PACKINGDATE` | DATE | Packing date |
| `PACKINGBY` | VARCHAR2(50) | Operator who packed |
| `CHECKBY` | VARCHAR2(50) | Supervisor who checked |
| `CHECKINGDATE` | DATE | Checking date |
| `REMARK` | VARCHAR2(500) | Packing remarks/notes |
| `COMPLETELAB` | VARCHAR2(10) | Lab testing complete flag (Y/N) |
| `TRANSFERAS400` | VARCHAR2(10) | Transferred to AS400 flag (Y/N) |
| `FLAG` | VARCHAR2(10) | Status flag (NEW/CHECKED/SHIPPED) |

---

## Business Logic (What it does and why)

Searches packing pallet headers with flexible filtering. Used to find pallets for editing, shipping, or status inquiry.

**Workflow**:
1. Accepts optional filter criteria:
   - Specific pallet number
   - Packing date
   - Status flag (new, checked, shipped)
2. Queries pallet header records
3. Returns pallet list with:
   - Identification (pallet number)
   - Operators (packed by, checked by)
   - Dates (packing, checking)
   - Status indicators (lab complete, AS400 transfer, flag)
   - Remarks/notes
4. Ordered by packing date (newest first typically)

**Business Rules**:
- All filter parameters are optional (can list all pallets)
- Returns pallet headers only (not details)
- Use PACK_GETPALLETDETAIL to get pallet contents
- FLAG indicates current status in workflow
- TRANSFERAS400 indicates ERP synchronization status

**Search Scenarios**:
1. **By Pallet Number**: "Find pallet PK-251017-0001"
   - P_PALLET = 'PK-251017-0001', others NULL
2. **By Date**: "Show all pallets packed today"
   - P_DATE = '2025-10-17', others NULL
3. **By Status**: "Find all checked pallets ready to ship"
   - P_STATUS = 'CHECKED', others NULL
4. **Recent Pallets**: "List all recent pallets"
   - All parameters NULL

**Status Flag Values** (typical):
- **NEW**: Just created, being filled with lots
- **CHECKED**: Verified by supervisor, ready to ship
- **SHIPPED**: Already shipped to customer
- **CANCELLED**: Cancelled/deleted

**Workflow Integration**:
```
NEW → CHECKED → SHIPPED
 ↓        ↓
CANCELLED  (from any state)
```

**Lab and AS400 Flags**:
- **COMPLETELAB = 'Y'**: All lots in pallet have lab test results
- **TRANSFERAS400 = 'Y'**: Pallet data sent to ERP system
- Both must be 'Y' before shipping in some workflows

**Common Queries**:
```csharp
// Find pallets ready to ship
var pallets = PACK_SEARCHPALLETLIST(null, null, "CHECKED")
    .Where(p => p.COMPLETELAB == "Y" && p.TRANSFERAS400 == "Y");

// Find today's pallets
var todayPallets = PACK_SEARCHPALLETLIST(null, DateTime.Today.ToString("yyyy-MM-dd"), null);

// Find specific pallet
var pallet = PACK_SEARCHPALLETLIST("PK-251017-0001", null, null).FirstOrDefault();
```

---

## Related Procedures

**Downstream**: [289-PACK_GETPALLETDETAIL.md](./289-PACK_GETPALLETDETAIL.md) - Gets pallet contents after finding
**Related**: [296-PACK_UPDATEPACKINGPALLET.md](./296-PACK_UPDATEPACKINGPALLET.md) - Updates found pallets
**Related**: [286-PACK_CANCELPALLET.md](./286-PACK_CANCELPALLET.md) - Cancels found pallets

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_SEARCHPALLETLIST()`
**Lines**: Likely in search/listing section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_SEARCHPALLETLIST(PACK_SEARCHPALLETLISTParameter para)`
**Lines**: 2088-2110

**Return Structure**:
```csharp
public class PACK_SEARCHPALLETLISTResult
{
    // Identification
    public string PALLETNO { get; set; }
    public string FLAG { get; set; }

    // Operators & Dates
    public string PACKINGBY { get; set; }
    public DateTime? PACKINGDATE { get; set; }
    public string CHECKBY { get; set; }
    public DateTime? CHECKINGDATE { get; set; }

    // Status indicators
    public string COMPLETELAB { get; set; }      // Lab tests complete
    public string TRANSFERAS400 { get; set; }    // ERP synchronized
    public string REMARK { get; set; }           // Notes
}
```

**Typical Query**:
```sql
SELECT PALLETNO, PACKINGDATE, PACKINGBY,
       CHECKBY, CHECKINGDATE, REMARK,
       COMPLETELAB, TRANSFERAS400, FLAG
FROM tblPackingPallet
WHERE (:P_PALLET IS NULL OR PALLETNO = :P_PALLET)
  AND (:P_DATE IS NULL OR TRUNC(PACKINGDATE) = TO_DATE(:P_DATE, 'YYYY-MM-DD'))
  AND (:P_STATUS IS NULL OR FLAG = :P_STATUS)
ORDER BY PACKINGDATE DESC, PALLETNO DESC
```

---

**File**: 295/296 | **Progress**: 99.7%
