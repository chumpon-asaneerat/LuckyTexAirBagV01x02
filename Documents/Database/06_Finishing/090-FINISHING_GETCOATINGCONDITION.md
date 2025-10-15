# FINISHING_GETCOATINGCONDITION

**Procedure Number**: 090 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve coating process condition setup and specifications for item |
| **Operation** | SELECT |
| **Tables** | tblItemCoatingCondition (coating specifications by item) |
| **Called From** | CoatingDataService.cs:189 → GetFINISHING_GETCOATINGCONDITIONDataList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Item code (finished goods specification) |
| `P_COATNO` | VARCHAR2(50) | ✅ | Coating number/type (C1, C2, C3, etc.) |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code |
| `SATURATOR_CHEM` | NUMBER | Saturator chemical temperature target (°C) |
| `SATURATOR_CHEM_MARGIN` | NUMBER | Saturator temperature tolerance (±°C) |
| `WASHING1` | NUMBER | Washing zone 1 temperature target (°C) |
| `WASHING1_MARGIN` | NUMBER | Washing 1 tolerance (±°C) |
| `WASHING2` | NUMBER | Washing zone 2 temperature target (°C) |
| `WASHING2_MARGIN` | NUMBER | Washing 2 tolerance (±°C) |
| `HOTFLUE` | NUMBER | Hot flue temperature target (°C) |
| `HOTFLUE_MARGIN` | NUMBER | Hot flue tolerance (±°C) |
| `BE_COATWIDTHMAX` | NUMBER | Before coating max width (mm) |
| `BE_COATWIDTHMIN` | NUMBER | Before coating min width (mm) |
| `ROOMTEMP` | NUMBER | Room temperature target (°C) |
| `ROOMTEMP_MARGIN` | NUMBER | Room temperature tolerance (±°C) |
| `FANRPM` | NUMBER | Fan RPM target |
| `FANRPM_MARGIN` | NUMBER | Fan RPM tolerance (±) |
| `EXFAN_FRONT_BACK` | NUMBER | Exhaust fan front/back setting |
| `EXFAN_MARGIN` | NUMBER | Exhaust fan tolerance |
| `ANGLEKNIFE` | NUMBER | Knife angle (degrees) |
| `BLADENO` | VARCHAR2(50) | Blade number specification |
| `BLADEDIRECTION` | VARCHAR2(50) | Blade direction (forward/reverse) |
| `PATHLINE` | VARCHAR2(50) | Path line configuration |
| `FEEDIN_MAX` | NUMBER | Feed-in percentage maximum |
| `FEEDIN_MIN` | NUMBER | Feed-in percentage minimum |
| `TENSION_UP` | NUMBER | Tension up target value |
| `TENSION_UP_MARGIN` | NUMBER | Tension up tolerance |
| `TENSION_DOWN` | NUMBER | Tension down target value |
| `TENSION_DOWN_MARGIN` | NUMBER | Tension down tolerance |
| `FRAMEWIDTH_FORN` | NUMBER | Frame width for furnace (mm) |
| `FRAMEWIDTH_TENTER` | NUMBER | Frame width tenter (mm) |
| `OVERFEED` | VARCHAR2(50) | Overfeed setting |
| `SPEED` | NUMBER | Machine speed target (m/min) |
| `SPEED_MARGIN` | NUMBER | Speed tolerance (±m/min) |
| `WIDTHCOAT` | NUMBER | Coating width target (mm) |
| `WIDTHCOATALL_MAX` | NUMBER | Total coating width maximum (mm) |
| `WIDTHCOATALL_MIN` | NUMBER | Total coating width minimum (mm) |
| `COATINGWEIGTH_MAX` | NUMBER | Coating weight maximum (g/m²) |
| `COATINGWEIGTH_MIN` | NUMBER | Coating weight minimum (g/m²) |
| `EXFAN_MIDDLE` | NUMBER | Exhaust fan middle zone setting |
| `RATIOSILICONE` | VARCHAR2(50) | Silicone A:B ratio |
| `COATNO` | VARCHAR2(50) | Coating number |
| `HUMIDITY_MAX` | NUMBER | Maximum humidity (%) |
| `HUMIDITY_MIN` | NUMBER | Minimum humidity (%) |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves the master process conditions (targets and tolerances) for coating a specific item. Used primarily for reports and documentation to show what the standard specifications are for each item.

**When Used**:
- Generating coating process reports (process sheets, QC documentation)
- Printing process condition sheets for operators
- Quality documentation showing approved parameters
- Master data reference for process engineers

**Business Rules**:
1. **Item-Specific Conditions**: Each finished goods item has unique coating requirements
2. **Coating Type**: Same item may have different conditions for different coating steps (C1, C2, C3)
3. **Target + Tolerance**: Specifications include target value and acceptable tolerance range
4. **Quality Standards**: Defines what "in-spec" means for quality control
5. **Process Documentation**: Official approved parameters for the item

**Workflow**:
1. User generates coating report for a specific item/lot
2. Report needs to show approved process conditions
3. System calls procedure with item code and coating number
4. Procedure returns all standard conditions from master data
5. Report displays: "Standard Conditions for Item XYZ, Coating C1"
6. Shows targets and ranges for all parameters

**Difference from FINISHING_COATINGPLCDATA**:
- **COATINGPLCDATA**: Real-time actual values from specific lot production
- **GETCOATINGCONDITION**: Master specification from item definition
- Example:
  - GETCOATINGCONDITION returns: "Item ABC should have TEMP1=170±5°C"
  - COATINGPLCDATA returns: "Lot 123 actual TEMP1 was 168°C"

**Example Scenario**:
- Item: "AB-COAT-001" (airbag fabric requiring 2-step coating)
- Coating Type: "C1" (first coating pass)
- Procedure returns master conditions:
  - SATURATOR_CHEM: 85°C ± 3°C (range: 82-88°C)
  - SPEED: 12 m/min ± 2 m/min (range: 10-14 m/min)
  - COATINGWEIGTH: 30-35 g/m² target range
  - RATIOSILICONE: "10:1" (A:B ratio)
  - BLADENO: "B-015"
  - BLADEDIRECTION: "Forward"
- Used in process sheet printout for operator reference

**Why This Matters**:
- **Quality Documentation**: Required for customer audits and certifications
- **Process Standardization**: Ensures all shifts use same approved parameters
- **Training**: New operators learn correct settings from process sheets
- **Engineering Reference**: When optimizing process, compare to approved baseline

---

## Related Procedures

**Upstream**:
- FINISHING_CHECKITEMWEAVING (validates item)
- ITM_GETITEMCODELIST (item master data)

**Downstream**:
- Used in report generation (RepMasterForm, Report classes)
- Feeds into quality documentation

**Similar**:
- FINISHING_GETSCOURINGCONDITION (same pattern for scouring)
- FINISHING_GETDRYERCONDITION (same pattern for dryer)
- CONDITION_FINISHINGCOATING (master process condition setup)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `GetFINISHING_GETCOATINGCONDITIONDataList(string itm_code, string CoatNo)`
**Lines**: 189-267

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_GETCOATINGCONDITION(FINISHING_GETCOATINGCONDITIONParameter para)`
**Lines**: 28XXX (estimated)

---

**File**: 90/296 | **Progress**: 30.4%
