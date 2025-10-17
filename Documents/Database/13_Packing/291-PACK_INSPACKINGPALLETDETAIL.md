# PACK_INSPACKINGPALLETDETAIL

**Procedure Number**: 291 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Add inspection lot to packing pallet |
| **Operation** | INSERT |
| **Tables** | tblPackingPalletDetail (assumed) |
| **Called From** | PackingDataService.cs → PACK_INSPACKINGPALLETDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Packing pallet number |
| `P_ORDERNO` | NUMBER | ⬜ | Order sequence in pallet |
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number to add |
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Product item code |
| `P_GRADE` | VARCHAR2(10) | ⬜ | Quality grade (A, B, C, etc.) |
| `P_NETLENGTH` | NUMBER | ⬜ | Net length (meters) |
| `P_GROSSLENGTH` | NUMBER | ⬜ | Gross length (meters) |
| `P_NETWEIGHT` | NUMBER | ⬜ | Net weight (kg) |
| `P_GROSSWEIGHT` | NUMBER | ⬜ | Gross weight (kg) |
| `P_CUSTYPE` | VARCHAR2(50) | ⬜ | Customer type |
| `P_INSPECTDATE` | DATE | ⬜ | Inspection completion date |
| `P_LOADTYPE` | VARCHAR2(10) | ⬜ | Loading type for shipping |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - INSERT operation only

---

## Business Logic (What it does and why)

Adds an inspection lot to a packing pallet. Called multiple times to build up pallet contents after creating pallet with PACK_INSERTPACKINGPALLET.

**Workflow**:
1. Receives pallet number and inspection lot to add
2. Validates inspection lot is available (not already packed)
3. Inserts pallet detail record with:
   - Packing sequence (order number)
   - Inspection lot reference
   - Product information (item code, grade)
   - Measurements (net/gross length and weight)
   - Customer requirements (type, loading type)
   - Inspection date for traceability
4. Marks inspection lot as "packed" in inspection table
5. Updates pallet totals (total length/weight)

**Business Rules**:
- Pallet number and inspection lot are mandatory
- Inspection lot must be in completed status
- Inspection lot cannot be already packed in another pallet
- Order number determines packing sequence
- All lots in pallet must match:
  - Same customer type
  - Same item code (or compatible items)
  - Same loading type
- Grade can vary (A/B/C grades can mix if allowed)

**Data Integrity**:
- Inspection lot marked as packed (ISPACKED = 'Y')
- Stock status updated (not available for other pallets)
- Pallet header totals updated
- Transaction logged for audit trail

---

## Related Procedures

**Upstream**: [290-PACK_INSERTPACKINGPALLET.md](./290-PACK_INSERTPACKINGPALLET.md) - Creates pallet before adding details
**Upstream**: [293-PACK_SEARCHINSPECTIONDATA.md](./293-PACK_SEARCHINSPECTIONDATA.md) - Searches available lots to pack
**Related**: [288-PACK_EDITPACKINGPALLETDETAIL.md](./288-PACK_EDITPACKINGPALLETDETAIL.md) - Edits detail after insertion
**Downstream**: [296-PACK_UPDATEPACKINGPALLET.md](./296-PACK_UPDATEPACKINGPALLET.md) - Finalizes pallet

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_INSPACKINGPALLETDETAIL()`
**Lines**: Approximately 326-400 (after PACK_INSERTPACKINGPALLET region)

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_INSPACKINGPALLETDETAIL(PACK_INSPACKINGPALLETDETAILParameter para)`
**Lines**: 2244-2267

**Implementation Notes**:
- 12 parameters for complete lot information
- Same structure as PACK_EDITPACKINGPALLETDETAIL but for INSERT
- Called multiple times per pallet (one call per inspection lot)
- Critical for building pallet composition

---

**File**: 291/296 | **Progress**: 98.3%
