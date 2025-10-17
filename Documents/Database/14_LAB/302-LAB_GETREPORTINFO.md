# LAB_GETREPORTINFO

**Procedure Number**: 302 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get lab test report template information for item/customer |
| **Operation** | SELECT |
| **Tables** | tblLabReportTemplate (assumed) |
| **Called From** | LABDataService.cs → LAB_GETREPORTINFO() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Product item code |
| `P_CUSTOMERID` | VARCHAR2(50) | ✅ | Customer identification |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Product item code |
| `REPORT_ID` | VARCHAR2(50) | Report template ID |
| `REVESION` | VARCHAR2(50) | Report revision number |
| `CUSTOMERID` | VARCHAR2(50) | Customer identification |
| `YARNTYPE` | VARCHAR2(500) | Yarn type description |
| `WEIGHT` | VARCHAR2(500) | Weight specification text |
| `COATWEIGHT` | VARCHAR2(500) | Coating weight specification |
| `NUMTHREADS` | VARCHAR2(500) | Thread count specification |
| `USABLE_WIDTH` | VARCHAR2(500) | Usable width specification |
| `THICKNESS` | VARCHAR2(500) | Thickness specification |
| `MAXFORCE` | VARCHAR2(500) | Tensile force specification |
| `ELONGATIONFORCE` | VARCHAR2(500) | Elongation specification |
| `FLAMMABILITY` | VARCHAR2(500) | Flammability specification |
| `EDGECOMB` | VARCHAR2(500) | Edge comb specification |
| `STIFFNESS` | VARCHAR2(500) | Stiffness specification |
| `TEAR` | VARCHAR2(500) | Tear strength specification |
| `STATIC_AIR` | VARCHAR2(500) | Static air permeability spec |
| `DYNAMIC_AIR` | VARCHAR2(500) | Dynamic air permeability spec |
| `EXPONENT` | VARCHAR2(500) | Air exponent specification |
| `DIMENSCHANGE` | VARCHAR2(500) | Dimensional change spec |
| `FLEXABRASION` | VARCHAR2(500) | Flex abrasion specification |
| `BENDING` | VARCHAR2(500) | Bending specification |
| `FLEX_SCOTT` | VARCHAR2(500) | Flex scott specification |
| `BOW` | VARCHAR2(500) | Bow specification |
| `SKEW` | VARCHAR2(500) | Skew specification |
| `EFFECTIVE_DATE` | DATE | Report template effective date |
| `REPORT_NAME` | VARCHAR2(500) | Report template name |

---

## Business Logic (What it does and why)

Retrieves lab test report template information for a specific item code and customer combination. Used to generate customer-specific test reports with correct formatting and specifications.

**Workflow**:
1. Receives item code and customer ID
2. Queries report template master for matching combination
3. Returns report configuration including:
   - Report identification (ID, revision, name)
   - Specification text for all test properties
   - Effective date of template
4. Used to format lab test reports for customer

**Business Rules**:
- Each item/customer combination has specific report template
- Different customers may require different report formats
- Specifications stored as formatted text (not just numbers)
- Report revision tracked for template version control
- Effective date indicates when template became active

**Report Template Usage**:

**Customer-Specific Formatting**:
Different customers require different report formats:
- **Customer A**: May want metric units (N/mm, g/m²)
- **Customer B**: May want imperial units (lbs/inch, oz/yd²)
- **Customer C**: May want specific test methods referenced

**Specification Text Format**:
Values stored as formatted strings (not just numbers):
```
MAXFORCE = "120 ± 5 N/mm (ASTM D5034)"
WEIGHT = "280 ± 10 g/m² (ISO 3801)"
STATIC_AIR = "≤ 5 L/dm²/min at 20 kPa (ISO 9237)"
```

**Report Components**:
1. **Header**: Report ID, Revision, Name, Date
2. **Material Info**: Yarn type, item code
3. **Specifications**: All test requirements (26 properties)
4. **Test Results**: Actual measured values (from test procedures)
5. **Pass/Fail**: Comparison of results vs specifications
6. **Approval**: Lab approval signature and date

**Version Control**:
- REVESION: Template version (e.g., "Rev A", "Rev 1.2")
- EFFECTIVE_DATE: When this version became active
- Allows tracking of specification changes over time

---

## Related Procedures

**Related**: [301-LAB_GETITEMTESTSPECIFICATION.md](./301-LAB_GETITEMTESTSPECIFICATION.md) - Gets numeric test specifications
**Used With**: LAB_GETPDFDATA - Gets actual test data for report
**Downstream**: PDF report generation
**Related**: LAB_INSERTUPDATEREPORTINFO - Updates report templates

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GETREPORTINFO()`
**Lines**: Likely in report generation section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GETREPORTINFO(LAB_GETREPORTINFOParameter para)`
**Lines**: 4198-4237

**Return Structure** (27 columns):
**Key Difference from LAB_GETITEMTESTSPECIFICATION**:
- GETITEMTESTSPECIFICATION: Returns **numeric** values for testing/validation
- GETREPORTINFO: Returns **formatted text** for report presentation

---

**File**: 302/296 | **Progress**: 102.0%
