# D365_IN_OUL_AUTO

**Procedure Number**: 262 | **Module**: M19 - D365 Integration | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get Inspection Output Update Line details (AUTO) for D365 ERP automatic output corrections |
| **Operation** | SELECT |
| **Tables** | Inspection production output update detail table |
| **Called From** | D365DataService.cs |
| **Frequency** | Medium (ERP sync - automatic corrections) |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHINGLOT` | VARCHAR2(50) | ✅ | Finishing lot number (source fabric identifier) |
| `P_INSPECTIONLOT` | VARCHAR2(50) | ✅ | Inspection lot number (production identifier) |
| `P_FINISH` | NUMBER | ✅ | Finish status flag for filtering |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `LINENO` | NUMBER | Update line sequence number |
| `OUTPUTDATE` | DATE | Output/inspection date |
| `ITEMID` | VARCHAR2(50) | Item ID (inspected fabric) in D365 |
| `QTY` | NUMBER | Quantity (fabric length) |
| `UNIT` | VARCHAR2(50) | Unit of measure (typically 'M' for meters) |
| `GROSSLENGTH` | NUMBER | Gross length (meters, with waste) |
| `NETLENGTH` | NUMBER | Net length (meters, usable) |
| `GROSSWEIGHT` | NUMBER | Gross weight (kg, with packaging) |
| `NETWEIGHT` | NUMBER | Net weight (kg, product only) |
| `PALLETNO` | VARCHAR2(50) | Pallet number for storage/shipping |
| `GRADE` | VARCHAR2(50) | Quality grade (A, B, C, etc.) |
| `LOADINGTYPE` | VARCHAR2(50) | Loading type code |
| `SERIALID` | VARCHAR2(50) | Serial/batch ID for traceability |
| `FINISH` | NUMBER | Finish status flag |
| `MOVEMENTTRANS` | VARCHAR2(50) | Movement transaction type |
| `WAREHOUSE` | VARCHAR2(50) | Warehouse location code |
| `LOCATION` | VARCHAR2(50) | Specific location within warehouse |

---

## Business Logic (What it does and why)

Retrieves Inspection production Output Update Line details for D365 ERP synchronization in AUTOMATED mode. This is a specialized version of D365_IN_OUL that filters by finish status flag, used for automatic batch updates triggered by system events rather than manual corrections.

The procedure:
1. Takes finishing lot, inspection lot, and finish status flag to identify records
2. Returns all output update line items matching the finish status
3. Each line has complete data for quantity, quality grade, weights, lengths
4. Includes warehouse location and movement transaction data
5. Used to post automatic output correction transactions in D365
6. Maintains full traceability through serial ID

**Difference from D365_IN_OUL**: This procedure uses P_FINISH filter instead of P_STARTDATE, allowing automated processes to sync only records in specific finish states (e.g., all completed inspections, all pending re-inspection, etc.).

Synced after Output Update Header (OUH) is posted to D365. Common scenarios: automated sync of all inspections marked as complete, batch update of records flagged for re-grading, system-triggered inventory adjustments.

---

## Related Procedures

**Upstream**:
- [260-D365_IN_OUH.md](./260-D365_IN_OUH.md) - Output update header (must sync first)

**Related**:
- [261-D365_IN_OUL.md](./261-D365_IN_OUL.md) - Manual version (uses P_STARTDATE)
- [258-D365_IN_OPH.md](./258-D365_IN_OPH.md) - Original output header
- [259-D365_IN_OPL.md](./259-D365_IN_OPL.md) - Original output lines

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\\Services\\DataService\\D365DataService.cs`

**Database Manager File**: `LuckyTex.AirBag.Core\\Domains\\AirbagSPs.cs`
**Method**: `D365_IN_OUL_AUTO(D365_IN_OUL_AUTOParameter para)`
**Lines**: 34777-34846

---

**File**: 262/296 | **Progress**: 88.5%
