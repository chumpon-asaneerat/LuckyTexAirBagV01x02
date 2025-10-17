# PACK_PRINTLABEL

**Procedure Number**: 293 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get inspection lot data for printing packing label |
| **Operation** | SELECT / UPDATE |
| **Tables** | tblInspection, tblPackingLabel (assumed) |
| **Called From** | PackingDataService.cs → PACK_PRINTLABEL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number to print label |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `INSPECTIONLOT` | VARCHAR2(50) | Inspection lot number |
| `QUANTITY` | NUMBER | Quantity (length in meters) |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg) |
| `NETWEIGHT` | NUMBER | Net weight (kg) |
| `GRADE` | VARCHAR2(10) | Quality grade |
| `ITEMCODE` | VARCHAR2(50) | Product item code |
| `DESCRIPTION` | VARCHAR2(500) | Product description |
| `SUPPLIERCODE` | VARCHAR2(50) | Supplier code (LuckyTex) |
| `BARCODEBACTHNO` | VARCHAR2(50) | Barcode batch number |
| `CUSTOMERPARTNO` | VARCHAR2(50) | Customer part number |
| `BATCHNO` | VARCHAR2(50) | Production batch number |
| `PDATE` | VARCHAR2(20) | Production date |
| `CUSTOMERID` | VARCHAR2(50) | Customer identification |
| `FINISHINGPROCESS` | VARCHAR2(50) | Finishing process code |
| `DBARCODE` | VARCHAR2(100) | 2D barcode data |
| `BDate` | VARCHAR2(20) | Batch date |
| `CUSPARTNO2D` | VARCHAR2(50) | Customer part number for 2D barcode |
| `GROSSLENGTH` | NUMBER | Gross length (meters) |

---

## Business Logic (What it does and why)

Retrieves all necessary information to print a packing label for an inspection lot. Also records the print event to prevent duplicate printing.

**Workflow**:
1. Receives inspection lot number
2. Retrieves complete lot information:
   - Product details (item code, description)
   - Measurements (quantity, weights, length)
   - Quality information (grade)
   - Customer information (customer ID, part numbers)
   - Batch information (batch number, production date)
   - Barcode data (1D and 2D barcodes)
   - Process information (finishing process)
3. Records print timestamp in label history table
4. Returns data formatted for label template

**Business Rules**:
- Inspection lot must be completed (inspected)
- Label can be printed multiple times (reprints allowed)
- Print timestamp logged for audit trail
- Barcode data includes:
  - Standard 1D barcode (BARCODEBACTHNO)
  - 2D barcode data (DBARCODE)
  - Customer-specific part numbers

**Label Information**:
- **Identification**: Inspection lot, item code, batch number
- **Customer Data**: Customer ID, part numbers (1D and 2D)
- **Quality**: Grade, finishing process
- **Measurements**: Net/gross weight, net/gross length
- **Traceability**: Production date, batch date, supplier code

**Barcode Standards**:
- BARCODEBACTHNO: 1D barcode for scanning
- DBARCODE: 2D barcode (QR or Data Matrix)
- CUSPARTNO2D: Customer part number encoded in 2D barcode
- All barcode data validated before printing

**Print Event Recording**:
After printing, procedure updates label history:
```sql
UPDATE tblPackingLabel
SET PRINTDATE = SYSDATE,
    PRINTCOUNT = NVL(PRINTCOUNT, 0) + 1
WHERE INSPECTIONLOT = :P_INSLOT;
```

---

## Related Procedures

**Related**: [287-PACK_CHECKPRINTLABEL.md](./287-PACK_CHECKPRINTLABEL.md) - Checks if label was printed
**Upstream**: Inspection process completes lot
**Used With**: Label printer driver/template
**Downstream**: Label attached to fabric roll

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_PRINTLABEL()`
**Lines**: Likely in label printing section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_PRINTLABEL(PACK_PRINTLABELParameter para)`
**Lines**: 2173-2205

**Return Structure** (18 columns):
```csharp
public class PACK_PRINTLABELResult
{
    // Basic identification
    public string INSPECTIONLOT { get; set; }
    public string ITEMCODE { get; set; }
    public string DESCRIPTION { get; set; }

    // Measurements
    public decimal? QUANTITY { get; set; }        // Net length
    public decimal? GROSSLENGTH { get; set; }
    public decimal? NETWEIGHT { get; set; }
    public decimal? GROSSWEIGHT { get; set; }

    // Quality & Process
    public string GRADE { get; set; }
    public string FINISHINGPROCESS { get; set; }

    // Customer information
    public string CUSTOMERID { get; set; }
    public string CUSTOMERPARTNO { get; set; }
    public string CUSPARTNO2D { get; set; }

    // Batch & Traceability
    public string BATCHNO { get; set; }
    public string PDATE { get; set; }
    public string BDate { get; set; }
    public string SUPPLIERCODE { get; set; }

    // Barcode data
    public string BARCODEBACTHNO { get; set; }    // 1D barcode
    public string DBARCODE { get; set; }          // 2D barcode
}
```

---

**File**: 293/296 | **Progress**: 99.0%
