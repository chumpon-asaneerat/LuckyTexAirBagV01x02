# LAB_GETINSPECTIONLIST

**Procedure Number**: 300 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get inspection lots requiring lab testing (ISLAB flag) |
| **Operation** | SELECT |
| **Tables** | tblInspection, tblPackingPalletDetail (joined) |
| **Called From** | LABDataService.cs → LAB_GETINSPECTIONLIST() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ⬜ | Inspection lot number filter (optional) |
| `P_DATE` | VARCHAR2(20) | ⬜ | Inspection date filter (optional) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PALLETNO` | VARCHAR2(50) | Packing pallet number (if packed) |
| `INSPECTIONLOT` | VARCHAR2(50) | Inspection lot number |
| `ITEMCODE` | VARCHAR2(50) | Product item code |
| `NETLENGTH` | NUMBER | Net length (meters) |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg) |
| `NETWEIGHT` | NUMBER | Net weight (kg) |
| `GRADE` | VARCHAR2(10) | Quality grade |
| `CUSTOMERTYPE` | VARCHAR2(50) | Customer type |
| `ISLAB` | VARCHAR2(10) | Lab test required flag (Y/N) |
| `INSPECTIONDATE` | DATE | Inspection completion date |
| `FLAG` | VARCHAR2(10) | Status flag |
| `LOADINGTYPE` | VARCHAR2(10) | Loading type for shipping |
| `STOCK` | VARCHAR2(10) | Stock status |
| `GROSSLENGTH` | NUMBER | Gross length (meters) |
| `ORDERNO` | NUMBER | Order number in pallet |

---

## Business Logic (What it does and why)

Retrieves list of inspection lots that require laboratory testing (ISLAB = 'Y'). Used by lab to identify which lots need testing before they can be shipped to customers.

**Workflow**:
1. Accepts optional filter criteria:
   - Specific inspection lot number
   - Inspection date
2. Queries inspection lots with ISLAB = 'Y'
3. Returns lot details including:
   - Identification (lot, item code)
   - Measurements (length, weight)
   - Quality information (grade)
   - Packing status (pallet number if packed)
   - Customer requirements
4. Lab uses list to schedule testing

**Business Rules**:
- Only returns lots with ISLAB = 'Y' (lab testing required)
- Lots can be packed (in pallet) but awaiting lab results
- Customer requirements determine if lab testing is mandatory
- Some customers require lab test reports with every shipment

**ISLAB Flag Logic**:
```
ISLAB = 'Y' → Lab testing required before shipping
ISLAB = 'N' → No lab testing needed (can ship immediately)
```

**Lab Testing Workflow**:
1. **Inspection Complete**: Lot passes visual inspection
2. **Check ISLAB**: If customer requires lab testing → ISLAB = 'Y'
3. **Get Lab List**: LAB_GETINSPECTIONLIST (this procedure)
4. **Take Sample**: Quality team takes sample from lot
5. **Perform Tests**: Lab conducts required tests
6. **Record Results**: Save test data (tensile, tear, air, etc.)
7. **Approve/Reject**: LAB_APPROVELABDATA
8. **Update Status**: If approved → Can ship
9. **Hold if Failed**: If rejected → Lot on hold, may need rework

**Usage Scenarios**:

**Lab Workload View**:
**Specific Lot Lookup**:
**Daily Lab Schedule**:
**Customer-Specific Testing**:
---

## Related Procedures

**Related**: PACK_SEARCHINSPECTIONDATA - Similar inspection lot search for packing
**Downstream**: LAB_INSERTSAMPLEDATA - Record lab sample
**Downstream**: LAB test procedures (tensile, tear, air permeability, etc.)
**Downstream**: LAB_APPROVELABDATA - Approve test results

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GETINSPECTIONLIST()`
**Lines**: Likely in lab workload section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GETINSPECTIONLIST(LAB_GETINSPECTIONLISTParameter para)`
**Lines**: 4617-4644

---

**File**: 300/296 | **Progress**: 101.4%
