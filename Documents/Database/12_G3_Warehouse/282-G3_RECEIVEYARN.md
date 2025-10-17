# G3_RECEIVEYARN

**Procedure Number**: 282 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update yarn pallet with QC inspection results and approve receiving |
| **Operation** | UPDATE |
| **Tables** | tblYarnStock (assumed) |
| **Called From** | G3DataService.cs:738 → G3_RECEIVEYARN() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_TRACENO` | VARCHAR2(50) | ✅ | Trace number (unique identifier) |
| `P_LOTNO` | VARCHAR2(50) | ✅ | Lot number from supplier |
| `P_VERIFY` | VARCHAR2(10) | ✅ | Verification status (PASS/FAIL) |
| `P_REMAINQTY` | NUMBER | ⬜ | Remaining quantity after inspection |
| `P_FLAG` | VARCHAR2(10) | ⬜ | Status flag |
| `P_OPERATORID` | VARCHAR2(50) | ✅ | QC operator ID performing inspection |
| `P_RECEIVEDATE` | DATE | ⬜ | Actual receiving date |
| `P_UPDATEDATE` | DATE | ⬜ | Last update timestamp |
| `P_TYPE` | VARCHAR2(10) | ⬜ | Material type |
| `P_PACKAGING` | VARCHAR2(10) | ⬜ | Packaging QC result (OK/NG) |
| `P_CLEAN` | VARCHAR2(10) | ⬜ | Cleanliness QC result (OK/NG) |
| `P_TEARING` | VARCHAR2(10) | ⬜ | Tearing inspection result (OK/NG) |
| `P_FALLDOWN` | VARCHAR2(10) | ⬜ | Fall down test result (OK/NG) |
| `P_CERTIFICATION` | VARCHAR2(10) | ⬜ | Certification document check (OK/NG) |
| `P_INVOICE` | VARCHAR2(10) | ⬜ | Invoice document check (OK/NG) |
| `P_IDENTIFYAREA` | VARCHAR2(10) | ⬜ | Identification area check (OK/NG) |
| `P_AMOUNTPALLET` | VARCHAR2(10) | ⬜ | Pallet quantity check (OK/NG) |
| `P_OTHER` | VARCHAR2(500) | ⬜ | Other remarks/issues |
| `P_ACTION` | VARCHAR2(500) | ⬜ | Corrective action taken |

### Output (OUT)

No output parameters (void result)

### Returns (if cursor)

No cursor return - UPDATE operation only

---

## Business Logic (What it does and why)

Performs quality control (QC) inspection on received yarn/material pallets and updates receiving status. This is the second step after G3_INSERTYARN, where QC operator inspects material quality and documentation.

**Workflow**:
1. QC operator scans pallet by trace number or lot number
2. Performs physical inspections:
   - Packaging condition (damage check)
   - Cleanliness (contamination check)
   - Tearing (material integrity)
   - Fall down test (stacking stability)
3. Verifies documentation:
   - Certification documents
   - Invoice/delivery note
   - Identification labels
   - Pallet quantity accuracy
4. Records verification result (PASS/FAIL) and operator ID
5. Updates remaining quantity if any loss/damage found
6. Logs corrective actions if needed

**Business Rules**:
- Trace number, lot number, verification status, and operator ID are mandatory
- Cannot proceed without QC inspection
- Only PASS status allows material to move to available stock
- Failed verification requires action plan documentation
- Updates receiving timestamp for traceability

**Integration**: This procedure is called from ReceiveYARNPage.xaml during incoming QC inspection workflow.

---

## Related Procedures

**Upstream**: [281-G3_INSERTYARN.md](./281-G3_INSERTYARN.md) - Creates initial pallet record before QC
**Downstream**: [283-G3_SEARCHBYPALLETNO.md](./283-G3_SEARCHBYPALLETNO.md) - Searches approved pallets for issuing
**Similar**: G3_UPDATEYARN - Similar update logic with different parameters

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_RECEIVEYARN()`
**Lines**: 693-752

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_RECEIVEYARN(G3_RECEIVEYARNParameter para)`
**Lines**: 6781-6810

**Validation Logic**:
```csharp
// Required validations before calling procedure:
if (string.IsNullOrWhiteSpace(P_TRACENO)) return false;
if (string.IsNullOrWhiteSpace(P_LOTNO)) return false;
if (string.IsNullOrWhiteSpace(P_VERIFY)) return false;
if (string.IsNullOrWhiteSpace(P_OPERATORID)) return false;
```

---

**File**: 282/296 | **Progress**: 95.3%
