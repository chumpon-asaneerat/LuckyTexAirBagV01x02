# LAB_UPLOADREPORT

**Procedure Number**: 195 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Upload laboratory test report file for a specific lot |
| **Operation** | INSERT/UPDATE |
| **Tables** | Lab report tracking table |
| **Called From** | LABDataService.cs |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code being tested |
| `P_WEAVINGLOG` | VARCHAR2(50) | ⬜ | Weaving lot number (for greige tests) |
| `P_FINISHINGLOT` | VARCHAR2(50) | ⬜ | Finishing lot number (for finished fabric tests) |
| `P_ENTRYDATE` | DATE | ✅ | Test entry date |
| `P_FILENAME` | VARCHAR2(500) | ✅ | Uploaded PDF report filename |
| `P_UPLOADDATE` | DATE | ✅ | Upload timestamp |
| `P_UPLOADBY` | VARCHAR2(50) | ✅ | Operator ID who uploaded the report |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `P_RETURN` | VARCHAR2(50) | Return code ('SUCCESS' or error message) |

---

## Business Logic (What it does and why)

Uploads laboratory test report PDF file to the system and links it to the tested lot. When lab technician completes physical testing and generates a PDF report, this procedure records the upload information so reports can be retrieved later for quality audits, customer requests, or production analysis.

The procedure:
1. Validates item code exists
2. Verifies lot number (weaving or finishing) is valid
3. Records the uploaded filename with timestamp
4. Links the report to the specific test entry
5. Updates upload tracking for audit trail
6. Returns success or error status

Either weaving lot OR finishing lot must be provided (not both, depending on test stage).

---

## Related Procedures

**Upstream**:
- [316-LAB_SEARCHLABENTRYPRODUCTION.md](./316-LAB_SEARCHLABENTRYPRODUCTION.md) - Search test entries
- [319-LAB_SAVELABRESULT.md](./319-LAB_SAVELABRESULT.md) - Save test results

**Downstream**:
- [314-LAB_GETREPORTINFO.md](./314-LAB_GETREPORTINFO.md) - Retrieve report information

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: Method name to be confirmed from DataService file
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_UPLOADREPORT(LAB_UPLOADREPORTParameter para)`
**Lines**: 17930-17967

---

**File**: 195/296 | **Progress**: 65.9%
