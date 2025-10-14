# WEAV_GETITEMWEAVINGLIST

**Procedure Number**: 069 | **Module**: M05 Weaving (also used in M02 Warping) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve list of weaving items with type, width, and yarn information |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:80, 123, 167 (3 overloads)<br>WarpingDataService (also uses this) |
| **Frequency** | High - Item lookup for production setup |
| **Performance** | Fast - Master data lookup with optional filtering |
| **Issues** | 🟡 1 Low - Multiple C# overloads share same SP (filtering done in C#) |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVETYPE` | VARCHAR2(50) | ⬜ | Optional weave type filter (OPW, CPW, etc.) |

**Note**: Procedure accepts optional parameter. Can be called with no parameters (returns all) or with weave type filter.

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_WEAVING` | VARCHAR2 | Weaving item/product code |
| `WIDTHWEAVING` | NUMBER | Fabric width in cm |
| `WEAVE_TYPE` | VARCHAR2 | Weave type classification (OPW/CPW/etc.) |
| `ITM_YARN` | VARCHAR2 | Associated yarn item code |

---

## Business Logic (What it does and why)

**Purpose**: Provides comprehensive list of weaving items with their specifications (width, type, yarn) for production planning, setup, and item selection across weaving operations.

**Business Context**:
- Weaving items are classified by WEAVE_TYPE (different weaving techniques)
- Each item has standard width specification
- Items are linked to specific yarn types for BOM and material planning
- Same procedure used in both Weaving and Warping modules
- Supports filtered and unfiltered queries for different UI scenarios

**Weave Types**:
- **OPW** - One-Piece Weaving (single fabric piece)
- **CPW** - Cut-Piece Weaving (cut from larger fabric)
- Other types as per production requirements

**Usage Scenarios**:

**Scenario 1: Item Selection Dropdown (No Filter)**
1. Operator opens production setup screen
2. Needs to select weaving item
3. System calls WEAV_GETITEMWEAVINGLIST() without parameters
4. Returns all active items
5. Populates dropdown with item codes
6. Shows width alongside item code for reference

**Scenario 2: Filtered Item List by Weave Type**
1. Production planning for OPW items only
2. Calls WEAV_GETITEMWEAVINGLIST("OPW")
3. Returns only OPW-type items
4. Operator sees relevant items only
5. Reduces selection errors and confusion

**Scenario 3: Yarn Requirements Lookup**
1. Material planning needs to know yarn for specific item
2. Calls procedure to get item list with yarn info
3. Service layer filters by ITM_WEAVING in C# (line 167-208)
4. Returns associated yarn code (ITM_YARN)
5. Used for material requirement calculation

**Scenario 4: Width Validation**
1. Operator enters item code manually
2. System calls procedure to get all items
3. Service layer searches for matching item (line 310-344)
4. Extracts WIDTHWEAVING value
5. Validates operator input against standard width
6. Returns decimal? WIDTHWEAVING for comparison

**C# Overload Patterns**:

The service layer has **3 overloaded methods** that all call the same stored procedure:

**Overload 1** (Line 80): `WEAV_GETITEMWEAVINGLIST()`
- No parameters
- Returns all items (only ITM_WEAVING field)
- Simple item code list

**Overload 2** (Line 123): `WEAV_GETITEMWEAVINGLIST(string P_WEAVETYPE)`
- Filters by weave type
- Passes parameter to stored procedure
- Returns filtered list (only ITM_WEAVING field)

**Overload 3** (Line 167): `WEAV_GETITEMWEAVINGLIST(string ITM_WEAVING, string type)`
- Gets yarn for specific item and type
- Calls procedure without parameters (gets all records)
- **Filters in C# code** (inefficient - should filter in SQL)
- Returns ITM_YARN field
- Breaks on first match

**Overload 4** (Line 310 - GetItemWidth method): Uses same procedure
- Gets width for specific item
- Calls procedure without parameters
- **Filters in C# code** to find matching item
- Returns WIDTHWEAVING as decimal?

**Performance Note**: Overloads 3 and 4 fetch all records then filter in C#, which is inefficient. Should pass item code as parameter to stored procedure.

---

## Related Procedures

**Similar Item Queries**:
- [WEAV_GETALLITEMWEAVING](./WEAV_GETALLITEMWEAVING.md) - Simpler version (item + width only)
- May have item master procedures in Module 17 (Master Data)

**Used By Modules**:
- **M05 Weaving**: Primary usage for item selection
- **M02 Warping**: Upstream module also needs item list for planning

**Item Specification Lookup**:
- Additional procedures for detailed item specs (GSM, construction, etc.)
- Customer item code mapping procedures

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Methods**: Multiple overloads at lines 80-113, 123-157, 167-208, and GetItemWidth() at 310-344

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETITEMWEAVINGLIST(WEAV_GETITEMWEAVINGLISTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 069/296 | **Progress**: 23.3%
