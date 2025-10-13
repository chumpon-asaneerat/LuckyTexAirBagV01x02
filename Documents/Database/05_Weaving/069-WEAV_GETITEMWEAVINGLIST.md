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

### Weaving Module Usage (Multiple Overloads)

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`

**Overload 1** - No parameters:
- **Method**: `WEAV_GETITEMWEAVINGLIST()`
- **Lines**: 80-113
- **Returns**: List with ITM_WEAVING only

**Overload 2** - With weave type filter:
- **Method**: `WEAV_GETITEMWEAVINGLIST(string P_WEAVETYPE)`
- **Lines**: 123-157
- **Returns**: Filtered list with ITM_WEAVING only

**Overload 3** - Get yarn by item and type:
- **Method**: `WEAV_GETITEMWEAVINGLIST(string ITM_WEAVING, string type)`
- **Lines**: 167-208
- **Returns**: List with ITM_YARN (filtered in C#)

**Overload 4** - Get width (different method name):
- **Method**: `GetItemWidth(string ITM_WEAVING)`
- **Lines**: 310-344
- **Returns**: decimal? WIDTHWEAVING

### Database Manager

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETITEMWEAVINGLIST(WEAV_GETITEMWEAVINGLISTParameter para)`
**Lines**: 13505-13548

**Stored Procedure Call**:
```csharp
// Single optional parameter - weave type filter
string[] paraNames = new string[]
{
    "P_WEAVETYPE"
};

object[] paraValues = new object[]
{
    para.P_WEAVETYPE  // Can be null for all items
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_GETITEMWEAVINGLIST",
    paraNames, paraValues);

// Returns cursor with 4 columns
```

**Return Structure**:
```csharp
public class WEAV_GETITEMWEAVINGLISTResult
{
    public string ITM_WEAVING { get; set; }    // Item code
    public decimal? WIDTHWEAVING { get; set; }  // Fabric width
    public string WEAVE_TYPE { get; set; }      // Type classification
    public string ITM_YARN { get; set; }        // Associated yarn
}
```

**Usage Example 1: Load All Items**
```csharp
// Simple item dropdown - all items
WeavingDataService service = WeavingDataService.Instance;
List<WEAV_GETITEMWEAVINGLIST> allItems = service.WEAV_GETITEMWEAVINGLIST();

cmbItemCode.ItemsSource = allItems;
cmbItemCode.DisplayMemberPath = "ITM_WEAVING";
cmbItemCode.SelectedValuePath = "ITM_WEAVING";
```

**Usage Example 2: Filtered by Type**
```csharp
// Load only OPW (One-Piece Weaving) items
string weaveType = "OPW";
List<WEAV_GETITEMWEAVINGLIST> opwItems =
    service.WEAV_GETITEMWEAVINGLIST(weaveType);

// Display in filtered list
dgItems.ItemsSource = opwItems;
lblItemCount.Text = $"{opwItems.Count} OPW items available";
```

**Usage Example 3: Get Yarn for Item**
```csharp
// Find required yarn for specific item
string itemCode = "FAB-AB-6070";
string weaveType = "OPW";

List<WEAV_GETITEMWEAVINGLIST> yarnInfo =
    service.WEAV_GETITEMWEAVINGLIST(itemCode, weaveType);

if (yarnInfo != null && yarnInfo.Count > 0)
{
    string requiredYarn = yarnInfo[0].ITM_YARN;
    lblYarnRequired.Text = $"Yarn: {requiredYarn}";

    // Check yarn availability
    CheckYarnStock(requiredYarn);
}
```

**Usage Example 4: Get Width for Validation**
```csharp
// Validate width entry
private bool ValidateWidth(string itemCode, decimal enteredWidth)
{
    decimal? standardWidth = service.GetItemWidth(itemCode);

    if (!standardWidth.HasValue)
    {
        MessageBox.Show($"Item {itemCode} not found.", "Error");
        return false;
    }

    decimal tolerance = standardWidth.Value * 0.02m; // 2% tolerance

    if (Math.Abs(enteredWidth - standardWidth.Value) > tolerance)
    {
        MessageBox.Show(
            $"Width out of tolerance!\n" +
            $"Standard: {standardWidth.Value} cm\n" +
            $"Entered: {enteredWidth} cm\n" +
            $"Tolerance: ±{tolerance:F2} cm",
            "Width Validation Failed",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );
        return false;
    }

    return true;
}
```

**Usage Example 5: Complete Item Info Display**
```csharp
// Display complete item information
private void ShowItemDetails(string itemCode)
{
    WeavingDataService service = WeavingDataService.Instance;

    // Call overload 2 to get all data
    var allItems = service.WEAV_GETITEMWEAVINGLIST();

    // Find in C# (ideally should filter in SQL)
    var itemDetails = DatabaseManager.Instance
        .WEAV_GETITEMWEAVINGLIST(new WEAV_GETITEMWEAVINGLISTParameter())
        .FirstOrDefault(i => i.ITM_WEAVING == itemCode);

    if (itemDetails != null)
    {
        txtItemCode.Text = itemDetails.ITM_WEAVING;
        txtWidth.Text = itemDetails.WIDTHWEAVING?.ToString() ?? "N/A";
        txtWeaveType.Text = itemDetails.WEAVE_TYPE ?? "N/A";
        txtYarn.Text = itemDetails.ITM_YARN ?? "N/A";

        // Additional item specifications from other procedures...
    }
}
```

**Performance Improvement Recommendation**:
```csharp
// Current inefficient approach (fetches all, filters in C#):
var allItems = service.WEAV_GETITEMWEAVINGLIST();  // Gets 1000 items
var item = allItems.FirstOrDefault(i => i.ITM_WEAVING == "FAB-001");

// Better approach (should modify stored procedure to accept item code):
// Add P_ITM_WEAVING parameter to stored procedure
// SELECT ... WHERE ITM_WEAVING = P_ITM_WEAVING OR P_ITM_WEAVING IS NULL
```

---

**File**: 069/296 | **Progress**: 23.3%
