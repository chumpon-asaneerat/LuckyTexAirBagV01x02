# CUT_GETSLIP

**Procedure Number**: 149 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve cutting operation data for slip/report printing |
| **Operation** | SELECT |
| **Tables** | tblCutPrint |
| **Called From** | CutPrintDataService.cs:1155 → CUT_GETSLIPReportData() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMLOT` | VARCHAR2(50) | ✅ | Item lot number |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns complete cutting operation record for report generation (40+ columns):
- Item lot, dates, all barcode measurements
- Cut line distances, densities, speeds
- Width measurements, roll positions
- Selvage, tension, length details
- All parameters recorded during cutting operation

---

## Business Logic (What it does and why)

**Purpose**: Retrieve cutting operation data for printing cutting slip/report.

**When Used**:
- After cutting operation completes
- Generate cutting slip for documentation
- Print operation summary
- Quality documentation
- Customer delivery documentation

**Business Rules**:
1. P_ITEMLOT required
2. Returns complete cutting record
3. Used for report generation (RDLC)
4. Shows all cutting parameters and measurements
5. Part of production traceability

**Report Contents**:
- **Identification**: Item lot, dates, operator
- **Barcode Specs**: Width and distance for multiple barcodes (1-4)
- **Cut Specifications**: Line distances, densities
- **Quality Measurements**: Widths before/after, selvage
- **Machine Parameters**: Speed, tension
- **Roll Information**: Begin/end positions for multiple lines
- **Length Details**: Detailed length information

**Workflow**:
1. Cutting operation completes
2. Operator initiates report print
3. System calls CUT_GETSLIP with item lot
4. Retrieves all cutting data
5. Generates RDLC report
6. Prints cutting slip
7. Attached to finished product or documentation

---

## Related Procedures

**Related**: [147-CUT_GETFINISHINGDATA.md](./147-CUT_GETFINISHINGDATA.md) - Get source finishing data
**Related**: CUT_INSERTDATA - Records data that this retrieves
**Used in**: Report generation for cutting documentation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `CUT_GETSLIPReportData(string ITEMLOT)`
**Lines**: 1155-1245
**Comment**: "เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUT_GETSLIP Report"
*Translation*: "Added new for use with CUT_GETSLIP Report"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_GETSLIP(CUT_GETSLIPParameter para)`

**Parameter Class**: 1 parameter (P_ITEMLOT)
**Result Class**: 40+ columns (complete cutting record)

**Usage**: Report generation pages - prints cutting slip/summary

---

**File**: 149/296 | **Progress**: 50.3%
