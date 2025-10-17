# PACK_PALLETSHEET

**Procedure Number**: 292 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Generate packing pallet sheet report data |
| **Operation** | SELECT |
| **Tables** | tblPackingPallet, tblPackingPalletDetail, tblInspection (joined) |
| **Called From** | PackingDataService.cs → PACK_PALLETSHEET() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLET` | VARCHAR2(50) | ✅ | Packing pallet number for report |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PALLETNO` | VARCHAR2(50) | Packing pallet number |
| `ITEMCODE` | VARCHAR2(50) | Product item code |
| `CUSTOMERTYPE` | VARCHAR2(50) | Customer type classification |
| `INSPECTIONLOT` | VARCHAR2(50) | Inspection lot number |
| `GRADE` | VARCHAR2(10) | Quality grade (A, B, C, etc.) |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg) |
| `NETLENGTH` | NUMBER | Net length (meters) |
| `NETWEIGHT` | NUMBER | Net weight (kg) |
| `PACKINGDATE` | DATE | Packing date |
| `PACKINGBY` | VARCHAR2(50) | Operator who packed |
| `CHECKBY` | VARCHAR2(50) | Supervisor who checked |
| `CHECKINGDATE` | DATE | Checking date |
| `LOADINGTYPE` | VARCHAR2(10) | Loading type for shipping |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item reference |
| `YARNCODE` | VARCHAR2(50) | Yarn material code |
| `ORDERNO` | NUMBER | Order sequence in pallet |

---

## Business Logic (What it does and why)

Retrieves complete packing pallet information formatted for packing list report/pallet sheet document. Used to print official packing documentation for shipping.

**Workflow**:
1. Receives packing pallet number
2. Joins pallet header and detail tables
3. Retrieves all inspection lots with:
   - Product information (item codes, grades)
   - Measurements (weight, length)
   - Operator information (packing and checking)
   - Dates (packing and checking)
   - Shipping information (loading type, customer)
   - Material traceability (weaving item, yarn code)
4. Returns data ordered by ORDERNO (packing sequence)
5. Formatted for report generation

**Business Rules**:
- Returns multiple rows (one per inspection lot in pallet)
- Ordered by ORDERNO to show packing sequence
- Includes operator accountability (packed by, checked by)
- Shows material traceability (weaving lot, yarn code)
- Used for official shipping documentation

**Report Usage**:
- **Packing List**: Detailed list of all rolls in pallet
- **Shipping Document**: Accompanies pallet to customer
- **QC Record**: Shows who packed and checked
- **Traceability**: Links to source materials (weaving, yarn)

**Data Completeness**:
- PACKINGBY: Operator who assembled pallet
- CHECKBY: Supervisor who verified pallet
- PACKINGDATE: When pallet was created
- CHECKINGDATE: When pallet was verified
- All fields required for shipping documentation

---

## Related Procedures

**Upstream**: [290-PACK_INSERTPACKINGPALLET.md](./290-PACK_INSERTPACKINGPALLET.md) - Creates pallet
**Upstream**: [291-PACK_INSPACKINGPALLETDETAIL.md](./291-PACK_INSPACKINGPALLETDETAIL.md) - Adds lots to pallet
**Related**: [289-PACK_GETPALLETDETAIL.md](./289-PACK_GETPALLETDETAIL.md) - Similar query for editing
**Used With**: Report viewer for printing packing sheets

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_PALLETSHEET()`
**Lines**: Likely in reporting section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_PALLETSHEET(PACK_PALLETSHEETParameter para)`
**Lines**: 2211-2238

**Return Structure**:
```csharp
public class PACK_PALLETSHEETResult
{
    // Pallet identification
    public string PALLETNO { get; set; }
    public string ITEMCODE { get; set; }
    public string CUSTOMERTYPE { get; set; }

    // Lot details
    public string INSPECTIONLOT { get; set; }
    public string GRADE { get; set; }
    public decimal? ORDERNO { get; set; }

    // Measurements
    public decimal? GROSSWEIGHT { get; set; }
    public decimal? NETLENGTH { get; set; }
    public decimal? NETWEIGHT { get; set; }

    // Accountability
    public string PACKINGBY { get; set; }
    public DateTime? PACKINGDATE { get; set; }
    public string CHECKBY { get; set; }
    public DateTime? CHECKINGDATE { get; set; }

    // Shipping & Traceability
    public string LOADINGTYPE { get; set; }
    public string ITM_WEAVING { get; set; }
    public string YARNCODE { get; set; }
}
```

**Typical Query Join**:
```sql
SELECT ph.PALLETNO, pd.INSPECTIONLOT, pd.ITEMCODE, pd.GRADE,
       pd.NETLENGTH, pd.NETWEIGHT, pd.GROSSWEIGHT, pd.ORDERNO,
       ph.PACKINGBY, ph.PACKINGDATE, ph.CHECKBY, ph.CHECKINGDATE,
       pd.LOADINGTYPE, pd.CUSTOMERTYPE,
       i.ITM_WEAVING, i.YARNCODE
FROM tblPackingPallet ph
JOIN tblPackingPalletDetail pd ON ph.PALLETNO = pd.PALLETNO
LEFT JOIN tblInspection i ON pd.INSPECTIONLOT = i.INSPECTIONLOT
WHERE ph.PALLETNO = :P_PALLET
ORDER BY pd.ORDERNO
```

---

**File**: 292/296 | **Progress**: 98.6%
