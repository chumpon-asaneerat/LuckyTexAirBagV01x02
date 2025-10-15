# FINISHING_GETSCOURINGCONDITION

**Procedure Number**: 096 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve scouring process condition setup and specifications for item |
| **Operation** | SELECT |
| **Tables** | tblItemScouringCondition |
| **Called From** | CoatingDataService.cs:1431 → GetFINISHING_GETSCOURINGCONDITIONDataList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Item code |
| `P_SCOURINGNO` | VARCHAR2(50) | ✅ | Scouring number/type (S1, S2, etc.) |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code |
| `SATURATOR_CHEM` | NUMBER | Saturator chemical temperature target (°C) |
| `SATURATOR_CHEM_MARGIN` | NUMBER | Saturator tolerance (±°C) |
| `WASHING1` | NUMBER | Washing zone 1 temperature target (°C) |
| `WASHING1_MARGIN` | NUMBER | Washing 1 tolerance (±°C) |
| `WASHING2` | NUMBER | Washing zone 2 temperature target (°C) |
| `WASHING2_MARGIN` | NUMBER | Washing 2 tolerance (±°C) |
| `HOTFLUE` | NUMBER | Hot flue temperature target (°C) |
| `HOTFLUE_MARGIN` | NUMBER | Hot flue tolerance (±°C) |
| `ROOMTEMP` | NUMBER | Room temperature target (°C) |
| `ROOMTEMP_MARGIN` | NUMBER | Room temperature tolerance (±°C) |
| `SPEED` | NUMBER | Machine speed target (m/min) |
| `SPEED_MARGIN` | NUMBER | Speed tolerance (±m/min) |
| `MAINFRAMEWIDTH` | NUMBER | Main frame width (mm) |
| `MAINFRAMEWIDTH_MARGIN` | NUMBER | Frame width tolerance (±mm) |
| `WIDTH_BE` | NUMBER | Width before scouring (mm) |
| `WIDTH_BE_MARGIN` | NUMBER | Width before tolerance (±mm) |
| `WIDTH_AF` | NUMBER | Width after scouring (mm) |
| `WIDTH_AF_MARGIN` | NUMBER | Width after tolerance (±mm) |
| `DENSITY_AF` | VARCHAR2(50) | Density after scouring |
| `DENSITY_MARGIN` | NUMBER | Density tolerance |
| `SCOURINGNO` | VARCHAR2(50) | Scouring number |
| `NIPCHEMICAL` | NUMBER | Nip chemical setting |
| `NIPROLLWASHER1` | NUMBER | Nip roll washer 1 setting |
| `NIPROLLWASHER2` | NUMBER | Nip roll washer 2 setting |
| `PIN2PIN` | NUMBER | Pin to pin distance (mm) |
| `PIN2PIN_MARGIN` | NUMBER | Pin to pin tolerance (±mm) |
| `HUMIDITY_MAX` | NUMBER | Maximum humidity (%) |
| `HUMIDITY_MIN` | NUMBER | Minimum humidity (%) |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves master process conditions for scouring operation. Used for reports, documentation, and displaying standard specifications to operators.

**When Used**:
- Generating scouring process reports
- Printing process condition sheets
- Quality documentation
- Master data reference

**Business Rules**:
1. **Item-Specific Conditions**: Each item has unique scouring requirements
2. **Scouring Type**: Different scouring steps (S1, S2) have different conditions
3. **Target + Tolerance**: Specifications include target value and acceptable tolerance range
4. **Quality Standards**: Defines acceptable process parameter ranges

**Workflow**:
1. User generates scouring report for item
2. System calls procedure with item code and scouring number
3. Returns all standard conditions from master data
4. Report displays approved specifications
5. Used for operator guidance and quality documentation

**Why This Matters**:
- Customer quality documentation requirements
- ISO/automotive certification compliance
- Process standardization across shifts
- Training reference for operators
- Engineering baseline for process optimization

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item)
- ITM_GETITEMCODELIST (item master data)

**Downstream**:
- Used in report generation systems
- Quality documentation systems

**Similar**:
- FINISHING_GETCOATINGCONDITION (same pattern for coating)
- FINISHING_GETDRYERCONDITION (same pattern for dryer)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `GetFINISHING_GETSCOURINGCONDITIONDataList(string itm_code, string ScouringNo)`
**Lines**: 1431-1498

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_GETSCOURINGCONDITION(FINISHING_GETSCOURINGCONDITIONParameter para)`
**Lines**: 28XXX (estimated)

---

**File**: 96/296 | **Progress**: 32.4%
