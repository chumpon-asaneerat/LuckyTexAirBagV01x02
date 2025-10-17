# G3_SEARCHBYPALLETNO

**Procedure Number**: 283 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search yarn pallet by pallet number or trace number |
| **Operation** | SELECT |
| **Tables** | tblYarnStock (assumed) |
| **Called From** | G3DataService.cs:191 → G3_SearchByPalletNoDataList(), G3_SearchByTRACENO() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet number OR trace number to search |

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

Searches and retrieves complete pallet information by pallet number or trace number. Used for pallet lookup during issuing, stock inquiry, and editing operations.

**Workflow**:
1. Accepts pallet number OR trace number as search key
2. Retrieves all pallet details including:
   - Material information (item code, type, weight, cone count)
   - QC inspection results (all quality checks)
   - Receiving information (operator, dates, verification status)
   - Stock information (remaining quantity, movement date)
   - Documentation status (certification, invoice, etc.)
3. Returns single pallet record or list of matching records

**Business Rules**:
- Can search by either pallet number or trace number
- Returns complete pallet history including QC results
- Used in two contexts:
  - `G3_SearchByPalletNoDataList()`: Returns list of pallets (line 191)
  - `G3_SearchByTRACENO()`: Returns single pallet by trace number (line 260)
- Critical for material traceability

**Usage Scenarios**:
- Material issuing: Scan pallet to get available quantity
- Stock inquiry: Check pallet status and location
- Edit operations: Load pallet data for modification
- Traceability: Track material from receiving to consumption

---

## Related Procedures

**Used With**: [282-G3_RECEIVEYARN.md](./282-G3_RECEIVEYARN.md) - After receiving, search to verify pallet
**Used With**: [284-G3_SEARCHYARNSTOCK.md](./284-G3_SEARCHYARNSTOCK.md) - Alternative search by item code/date
**Downstream**: G3_INSERTUPDATEISSUEYARN - Uses search results for issuing

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method 1**: `G3_SearchByPalletNoDataList()` - Returns list of pallets
**Lines**: 177-238
**Method 2**: `G3_SearchByTRACENO()` - Returns single pallet by trace number
**Lines**: 248-320

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_SEARCHBYPALLETNO(G3_SEARCHBYPALLETNOParameter para)`
**Lines**: 6735-6775

**Implementation Notes**:
- Same procedure used for both pallet number and trace number searches
- Returns 28 columns with complete pallet history
- Maps database results to `G3_SEARCHBYPALLETNOSearchData` objects
- Adds `KGPERCH` (kg per cone) calculated field

---

**File**: 283/296 | **Progress**: 95.6%
