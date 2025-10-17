# G3_SEARCHYARNSTOCK

**Procedure Number**: 284 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search yarn stock by item code, date, and material type |
| **Operation** | SELECT |
| **Tables** | tblYarnStock (assumed) |
| **Called From** | G3DataService.cs:484 → GetG3_SEARCHYARNSTOCKData() |
| **Frequency** | High |
| **Performance** | Medium |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMYARN` | VARCHAR2(50) | ⬜ | Yarn/material item code filter |
| `P_RECDATE` | VARCHAR2(20) | ⬜ | Receiving date filter (string format) |
| `P_YARNTYPE` | VARCHAR2(10) | ⬜ | Material type filter (YARN/SILICONE/etc) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ENTRYDATE` | DATE | Entry date to warehouse |
| `ITM_YARN` | VARCHAR2(50) | Yarn/material item code |
| `PALLETNO` | VARCHAR2(50) | Pallet number |
| `YARNTYPE` | VARCHAR2(10) | Material type (YARN/SILICONE/etc) |
| `WEIGHTQTY` | NUMBER | Weight quantity (kg) |
| `CONECH` | NUMBER | Cone/cheese quantity |
| `VERIFY` | VARCHAR2(10) | QC verification status (PASS/FAIL) |
| `REMAINQTY` | NUMBER | Remaining quantity in stock |
| `RECEIVEBY` | VARCHAR2(50) | Operator who received the pallet |
| `RECEIVEDATE` | DATE | Receiving date |
| `FINISHFLAG` | VARCHAR2(10) | Completion flag (Y/N) |
| `UPDATEDATE` | DATE | Last update timestamp |
| `PALLETTYPE` | VARCHAR2(10) | Pallet type classification |
| `ITM400` | VARCHAR2(50) | AS400 item code reference |
| `UM` | VARCHAR2(10) | Unit of measure |
| `PACKAING` | VARCHAR2(10) | Packaging QC result (OK/NG) |
| `CLEAN` | VARCHAR2(10) | Cleanliness QC result (OK/NG) |
| `TEARING` | VARCHAR2(10) | Tearing inspection result (OK/NG) |
| `FALLDOWN` | VARCHAR2(10) | Fall down test result (OK/NG) |
| `CERTIFICATION` | VARCHAR2(10) | Certification document status (OK/NG) |
| `INVOICE` | VARCHAR2(10) | Invoice document status (OK/NG) |
| `IDENTIFYAREA` | VARCHAR2(10) | Identification area check (OK/NG) |
| `AMOUNTPALLET` | VARCHAR2(10) | Pallet amount check (OK/NG) |
| `OTHER` | VARCHAR2(500) | Other remarks |
| `ACTION` | VARCHAR2(500) | Corrective action taken |
| `MOVEMENTDATE` | DATE | Last movement date |
| `LOTNO` | VARCHAR2(50) | Supplier lot number |
| `TRACENO` | VARCHAR2(50) | Unique trace number |
| `KGPERCH` | NUMBER | Kilograms per cone/cheese |

---

## Business Logic (What it does and why)

Searches yarn/material stock with flexible filtering by item code, receiving date, and material type. Used for stock inquiry and pallet selection during issuing operations.

**Workflow**:
1. Accepts optional filter criteria:
   - Item code: Filter by specific yarn/material
   - Receiving date: Filter by receiving period
   - Material type: Filter by YARN, SILICONE, or other types
2. Returns all matching pallets with complete information:
   - Stock details (quantity, weight, cone count)
   - QC inspection history
   - Receiving information
   - Documentation status
3. Results are ordered and numbered for display

**Business Rules**:
- All parameters are optional (can search all stock)
- Returns same structure as G3_SEARCHBYPALLETNO
- Used primarily in CheckStockYarnPage.xaml for stock inquiry
- Can filter by any combination of criteria
- Returns only available stock (FINISHFLAG = 'N' or remaining qty > 0)

**Usage Scenarios**:
- Stock inquiry: "Show all PA6 yarn received this month"
- Material selection: "Find available silicone for coating"
- Stock counting: "List all pallets by item code"
- Aging analysis: "Show yarn received before specific date"

**Data Enhancement**:
- Adds row numbers for display (inst.RowNo = i++)
- Adds selection checkbox support (inst.SelectData = false)
- Includes KGPERCH calculated field for unit conversion

---

## Related Procedures

**Alternative**: [283-G3_SEARCHBYPALLETNO.md](./283-G3_SEARCHBYPALLETNO.md) - Search by pallet number
**Used With**: [282-G3_RECEIVEYARN.md](./282-G3_RECEIVEYARN.md) - Searches received stock
**Downstream**: G3_INSERTUPDATEISSUEYARN - Uses search results for issuing

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `GetG3_SEARCHYARNSTOCKData()`
**Lines**: 469-541

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_SEARCHYARNSTOCK(G3_SEARCHYARNSTOCKParameter para)`
**Lines**: 6687-6729

**Implementation Details**:
```csharp
// Flexible filtering:
dbPara.P_RECDATE = _date;      // Optional date filter
dbPara.P_ITMYARN = _ITMYARN;   // Optional item code filter
dbPara.P_YARNTYPE = _YARNTYPE; // Optional type filter

// Enhanced result mapping:
inst.SelectData = false;        // Add checkbox support
inst.RowNo = i++;              // Add row numbering
inst.KGPERCH = dbResult.KGPERCH; // Include unit conversion
```

---

**File**: 284/296 | **Progress**: 95.9%
