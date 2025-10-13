# WEAV_GETALLITEMWEAVING

**Procedure Number**: 066 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete list of weaving item codes with fabric widths |
| **Operation** | SELECT |
| **Tables** | tblItemWeaving or tblProduct (assumed) |
| **Called From** | WeavingDataService.cs:218 → WEAV_GETALLITEMWEAVING() |
| **Frequency** | Medium - Page initialization for item selection dropdowns |
| **Performance** | Fast - Small master data table |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

None - Returns all active weaving items

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code (product code for fabric) |
| `WIDTHWEAVING` | NUMBER | Standard fabric width in cm or inches |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblItemWeaving` - SELECT - Weaving item master data (assumed)
- OR `tblProduct` with filter for weaving items

**Transaction**: No (Read-only operation)

### Likely Query Structure

```sql
-- Assumed stored procedure logic
SELECT
    ITM_WEAVING,
    WIDTHWEAVING
FROM tblItemWeaving
WHERE ISACTIVE = 'Y'  -- Only active items
  AND ITEMTYPE = 'WEAVING'  -- Weaving products only
ORDER BY ITM_WEAVING;
```

---

## Business Logic (What it does and why)

**Purpose**: Provides a master list of all weaving item codes (fabric products) with their standard widths, used for item selection throughout the weaving module.

**Business Context**:
- Each fabric product has a unique item code (ITM_WEAVING)
- Each item has a standard fabric width specification
- Operators need to select item codes when setting up looms or recording production
- Width is critical specification for loom setup and quality control
- Item list populates dropdown lists across multiple weaving pages

**Usage Scenarios**:

**Scenario 1: Loom Setup**
1. Operator preparing to start production on a loom
2. Opens loom setup screen
3. System loads all available weaving items via WEAV_GETALLITEMWEAVING
4. Operator selects item code from dropdown (e.g., "FAB-AB-6070")
5. System auto-fills width specification (e.g., 60 cm)
6. Operator configures loom according to width and item spec

**Scenario 2: Production Recording**
1. Operator recording completed fabric roll
2. Selects item code from predefined list
3. Width validates against actual measured width
4. Production data includes both item code and width for traceability

**Scenario 3: Item Search/Filter**
1. User searching production history by item
2. System provides autocomplete suggestions from item list
3. User selects item to filter production records

**Business Rules**:
- Returns only active items (not discontinued products)
- Returns only items classified as weaving products (not yarn or other materials)
- Width is standard specification (not actual measured width)
- Item codes are unique across the system
- Width is used for validation and quality control

**Data Usage**:
- **ITM_WEAVING**: Displayed in dropdowns, used as key for other lookups
- **WIDTHWEAVING**: Displayed alongside item code, used for validation

**Related Master Data**:
- Item specifications (GSM, construction, composition)
- Customer item mapping (customer's item code → internal code)
- Production standards (speed, tension, efficiency targets)

---

## Related Procedures

**Similar Master Data Queries**:
- [WEAV_GETITEMWEAVINGLIST](./WEAV_GETITEMWEAVINGLIST.md) - More detailed item list with filters
- Similar item list procedures in other modules (WARP_, BEAM_)

**Item Specification Lookup**:
- May have related procedures for detailed item specs
- Customer-specific item code mapping

**Used By** (Procedures that reference items):
- [WEAVE_INSERTPROCESSSETTING](./WEAVE_INSERTPROCESSSETTING.md) - Validate item during setup
- [WEAV_WEAVINGPROCESS](./WEAV_WEAVINGPROCESS.md) - Record production by item
- Most other WEAV_ procedures that accept ITM_WEAVING parameter

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETALLITEMWEAVING()`
**Lines**: 218-252
**Comment**: `เพิ่มใหม่ WEAV_GETALLITEMWEAVING ใช้ในการ Load ItemGood` (Thai: "Added for loading good items")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETALLITEMWEAVING(WEAV_GETALLITEMWEAVINGParameter para)`
**Lines**: 13656-13695

**Stored Procedure Call**:
```csharp
// No input parameters - returns all weaving items
string[] paraNames = new string[] { };
object[] paraValues = new object[] { };

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_GETALLITEMWEAVING",
    paraNames, paraValues);
```

**Return Structure** (mapped to C# model):
```csharp
public class WEAV_GETALLITEMWEAVING
{
    public string ITM_WEAVING { get; set; }    // Item code
    public decimal? WIDTHWEAVING { get; set; }  // Fabric width
}
```

**Usage Example**:
```csharp
// Load item list at page initialization
WeavingDataService service = WeavingDataService.Instance;
List<WEAV_GETALLITEMWEAVING> itemList = service.WEAV_GETALLITEMWEAVING();

// Bind to ComboBox for item selection
cmbItemCode.ItemsSource = itemList;
cmbItemCode.DisplayMemberPath = "ITM_WEAVING";
cmbItemCode.SelectedValuePath = "ITM_WEAVING";

// Display width when item selected
private void cmbItemCode_SelectionChanged(object sender, EventArgs e)
{
    if (cmbItemCode.SelectedItem is WEAV_GETALLITEMWEAVING item)
    {
        txtWidth.Text = item.WIDTHWEAVING?.ToString() ?? "N/A";
        // Load additional item specifications...
    }
}
```

**Alternative Usage - Autocomplete**:
```csharp
// Use for search/filter with autocomplete
public void SetupItemSearch()
{
    List<WEAV_GETALLITEMWEAVING> allItems = service.WEAV_GETALLITEMWEAVING();

    // Setup autocomplete source
    txtItemSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    txtItemSearch.AutoCompleteSource = AutoCompleteSource.CustomSource;

    AutoCompleteStringCollection itemCodes = new AutoCompleteStringCollection();
    itemCodes.AddRange(allItems.Select(i => i.ITM_WEAVING).ToArray());
    txtItemSearch.AutoCompleteCustomSource = itemCodes;
}
```

**Display with Width**:
```csharp
// Create display string with item and width
foreach (var item in itemList)
{
    string display = $"{item.ITM_WEAVING} (Width: {item.WIDTHWEAVING} cm)";
    cmbItemCode.Items.Add(new { Display = display, Value = item.ITM_WEAVING });
}
```

---

**File**: 066/296 | **Progress**: 22.3%
