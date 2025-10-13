# WEAV_DEFECTLIST

**Procedure Number**: 063 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve master list of defect codes for weaving operations |
| **Operation** | SELECT |
| **Tables** | tblDefectCode (assumed master table) |
| **Called From** | WeavingDataService.cs:262 → WEAV_DEFECTLIST() |
| **Frequency** | Medium - Loaded at page initialization for defect entry screens |
| **Performance** | Fast - Small master data lookup |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

None - Returns all defect codes

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `DEFECTCODE` | VARCHAR2 | Unique defect code identifier |
| `DEFECTTYPE` | VARCHAR2 | Type/category of defect |
| `DESCRIPTION` | VARCHAR2 | Human-readable defect description |
| `YARN` | VARCHAR2 | Related yarn type or position (if applicable) |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblDefectCode` - SELECT - Master defect code table (assumed)

**Transaction**: No (Read-only operation)

### Indexes

```sql
-- Recommended indexes (if not exists)
CREATE INDEX idx_defectcode_type ON tblDefectCode(DEFECTTYPE);
CREATE INDEX idx_defectcode_active ON tblDefectCode(ISACTIVE); -- if has active flag
```

---

## Business Logic (What it does and why)

**Purpose**: Provides a master list of standardized defect codes used throughout the weaving process for quality tracking and defect recording.

**Business Context**:
- When operators inspect fabric during or after weaving, they record defects using standardized codes
- This procedure loads all available defect codes to populate dropdown lists or selection controls
- Defect codes are categorized by DEFECTTYPE for easier filtering and selection
- YARN field indicates if defect is related to specific yarn positions (warp/weft)

**Usage Scenario**:
1. Operator opens defect entry screen (weaving inspection, machine stop recording)
2. System calls WEAV_DEFECTLIST to populate defect code dropdown
3. Operator selects appropriate defect code from standardized list
4. Selected defect code is recorded with production lot for traceability

**Business Rules**:
- Returns all defect codes (no filtering by active/inactive status)
- Codes are standardized across all weaving machines and operators
- Defect codes must be predefined in master data (Module 17)
- Each defect code has a type classification for reporting and analysis

**Data Transformation**:
- Service layer concatenates DEFECTCODE + DESCRIPTION into `DefectCodeName` property for display purposes
- Example: "D001 Broken End" for easier operator selection

---

## Related Procedures

**Master Data Management**:
- [DEFECT_SEARCH](../17_Master_Data/DEFECT_SEARCH.md) - Search/filter defect codes
- [DEFECT_INSERTUPDATE](../17_Master_Data/DEFECT_INSERTUPDATE.md) - Add/modify defect codes

**Usage in Inspection**:
- [INS_INSERTMANUALINSPECTDATA](../08_Inspection/INS_INSERTMANUALINSPECTDATA.md) - Record defects during inspection
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Record machine stops with defect codes

**Similar**:
- [DEFECT_SEARCH](../17_Master_Data/DEFECT_SEARCH.md) - More advanced search with filtering

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_DEFECTLIST()`
**Lines**: 262-300

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_DEFECTLIST(WEAV_DEFECTLISTParameter para)`
**Lines**: 13773-13813

**Stored Procedure Call**:
```csharp
// No input parameters - returns all defect codes
string[] paraNames = new string[] { };
object[] paraValues = new object[] { };

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_DEFECTLIST",
    paraNames, paraValues);
```

**Return Structure** (mapped to C# model):
```csharp
public class WEAV_DEFECTLIST
{
    public string DEFECTCODE { get; set; }    // Primary key
    public string DEFECTTYPE { get; set; }    // Category
    public string DESCRIPTION { get; set; }   // Display text
    public string YARN { get; set; }          // Yarn position
    public string DefectCodeName { get; set; } // Computed: CODE + " " + DESCRIPTION
}
```

**Usage Example**:
```csharp
// Called from UI page initialization
WeavingDataService service = WeavingDataService.Instance;
List<WEAV_DEFECTLIST> defectList = service.WEAV_DEFECTLIST();

// Bind to ComboBox for operator selection
cmbDefectCode.ItemsSource = defectList;
cmbDefectCode.DisplayMemberPath = "DefectCodeName"; // Shows "D001 Broken End"
cmbDefectCode.SelectedValuePath = "DEFECTCODE";     // Stores "D001"
```

---

**File**: 063/296 | **Progress**: 21.3%
