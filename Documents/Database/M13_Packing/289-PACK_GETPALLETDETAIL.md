# PACK_GETPALLETDETAIL

**Procedure Number**: 289 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get packing pallet details for display or editing |
| **Operation** | SELECT |
| **Tables** | tblPackingPallet, tblPackingPalletDetail (assumed) |
| **Called From** | PackingDataService.cs:448 → PACK_GETPALLETDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLET` | VARCHAR2(50) | ✅ | Packing pallet number to retrieve |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PALLETNO` | VARCHAR2(50) | Packing pallet number |
| `INSPECTIONLOT` | VARCHAR2(50) | Inspection lot number in pallet |
| `ITEMCODE` | VARCHAR2(50) | Product item code |
| `NETLENGTH` | NUMBER | Net length (meters) |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg) |
| `NETWEIGHT` | NUMBER | Net weight (kg) |
| `GRADE` | VARCHAR2(10) | Quality grade (A, B, C, etc.) |
| `CUSTOMERTYPE` | VARCHAR2(50) | Customer type classification |
| `ISLAB` | VARCHAR2(10) | Lab test required flag (Y/N) |
| `INSPECTIONDATE` | DATE | Inspection completion date |
| `FLAG` | VARCHAR2(10) | Status flag |
| `LOADINGTYPE` | VARCHAR2(10) | Loading type for shipping |
| `STOCK` | VARCHAR2(10) | Stock status |
| `GROSSLENGTH` | NUMBER | Gross length (meters) |
| `ORDERNO` | NUMBER | Order sequence in pallet |
| `ITM_GROUP` | VARCHAR2(50) | Item group classification |

---

## Business Logic (What it does and why)

Retrieves all details of a packing pallet including all inspection lots packed in it. Used for display, editing, and validation before shipping.

**Workflow**:
1. Receives packing pallet number
2. Queries pallet header and detail information
3. Returns all inspection lots in the pallet with:
   - Product information (item code, group, grade)
   - Measurements (length and weight - net/gross)
   - Customer information (type, loading type)
   - Quality information (lab flag, inspection date)
   - Packing sequence (order number)
4. Results ordered by ORDERNO for display

**Business Rules**:
- Pallet number must exist
- Returns multiple rows (one per inspection lot in pallet)
- Each lot has its own measurements and specifications
- ORDERNO indicates packing sequence
- FLAG indicates current status (packed, shipped, etc.)

**Usage Scenarios**:
- Display pallet contents on screen
- Edit pallet before shipping
- Validate pallet totals
- Print packing list report
- Check pallet composition

**Data Mapping**:
```csharp
public List<PACK_GETPALLETDETAIL> PACK_GETPALLETDETAIL(string PalletNo)
{
    // Returns list of all lots in pallet
    // Each lot includes:
    // - Inspection lot reference
    // - Item code and specifications
    // - Net/Gross length and weight
    // - Customer and loading type
    // - Order sequence
}
```

---

## Related Procedures

**Related**: [288-PACK_EDITPACKINGPALLETDETAIL.md](./288-PACK_EDITPACKINGPALLETDETAIL.md) - Edits the returned details
**Related**: PACK_INSPACKINGPALLETDETAIL - Adds lots to pallet
**Downstream**: Used for packing list reports and shipping validation

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_GETPALLETDETAIL()`
**Lines**: 434-487

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_GETPALLETDETAIL(PACK_GETPALLETDETAILParameter para)`
**Lines**: 2290-2317

**Implementation**:
```csharp
public List<PACK_GETPALLETDETAIL> PACK_GETPALLETDETAIL(string PalletNo)
{
    List<PACK_GETPALLETDETAIL> results = null;

    if (string.IsNullOrWhiteSpace(PalletNo)) return results;
    if (!HasConnection()) return results;

    PACK_GETPALLETDETAILParameter dbPara = new PACK_GETPALLETDETAILParameter();
    dbPara.P_PALLET = PalletNo;

    List<PACK_GETPALLETDETAILResult> dbResults = null;
    dbResults = DatabaseManager.Instance.PACK_GETPALLETDETAIL(dbPara);

    if (null != dbResults && dbResults.Count > 0)
    {
        results = new List<PACK_GETPALLETDETAIL>();
        foreach (PACK_GETPALLETDETAILResult dbResult in dbResults)
        {
            // Map all 16 columns to result object
            inst.PALLETNO = dbResult.PALLETNO;
            inst.INSPECTIONLOT = dbResult.INSPECTIONLOT;
            inst.ITEMCODE = dbResult.ITEMCODE;
            // ... (all 16 fields)
            results.Add(inst);
        }
    }

    return results;  // Returns list of all lots in pallet
}
```

---

**File**: 289/296 | **Progress**: 97.6%
