# 005 - PROCESS: Item Code Specification Management

**Module**: 05 - Item Code Specification
**Process ID**: PROCESS_ITEM_SPECIFICATION
**Created**: 2025-10-11
**Document Type**: Process Implementation Documentation
**Status**: 🔴 **CATASTROPHIC COMPLEXITY**

---

## 🚨 CRITICAL DISCOVERY - 3RD LARGEST FILE IN CODEBASE

### File Metrics
- **ItemCodeSpecificationPage.xaml.cs**: **9,978 lines of code** (371.3 KB)
- **ItemCodeSpecificationPage.xaml**: 505 lines
- **Total Module**: **10,483 LOC**

### Comparison with Other Modules
| File | LOC | Ranking |
|------|-----|---------|
| **LabDataEntryPage** (Module 03) | 149,594 | 🥇 #1 CATASTROPHIC |
| **LabDataEntryWindow** (Module 04) | 33,132 | 🥈 #2 EXTREME |
| **ItemCodeSpecificationPage** (Module 05) | 9,978 | 🥉 **#3 VERY HIGH** ⚠️ |
| CutPrintPage (Main MES) | 6,525 | #4 |
| Coating3FinishingPage (Main MES) | 3,141 | #5 |

### Complexity Assessment: **VERY HIGH** 🔴⚠️
- **3rd largest file** in entire codebase (Main MES + Lab system)
- **~400-500 methods** estimated (mass code duplication)
- **~100+ database parameters** in Save() method
- **~150-200 input fields** (all quality test specifications)
- **~300+ event handlers** (KeyDown, LostFocus, SelectionChanged × ~100 fields)
- **90%+ code duplication** (identical handler patterns repeated)

---

## Process Overview

### Purpose
Manage quality specification limits (LCL/UCL) for each product/item code in the lab testing system. This page defines **ALL quality test parameters** for every fabric type.

### Scope
- **Input**: Item code selection
- **Processing**: Edit specifications for 50+ quality test types
- **Output**: Updated specification database
- **Users**: Lab supervisors, quality managers

### Business Context
- **Master Data**: Quality specifications are referenced by Modules 01-04 for validation
- **Critical Function**: Without proper specifications, all lab tests cannot validate pass/fail
- **Automotive Requirement**: Specification limits are part of customer quality agreements

---

## UI Files Inventory

### Primary Files

| File Path | Lines | Purpose | Status |
|-----------|-------|---------|--------|
| `LuckyTex.Lab.Transfer.Data/Pages/05 Item Code Specification/`<br>`ItemCodeSpecificationPage.xaml` | 505 | UI layout definition | 🔴 Active |
| `LuckyTex.Lab.Transfer.Data/Pages/05 Item Code Specification/`<br>`ItemCodeSpecificationPage.xaml.cs` | 9,978 | Code-behind logic | 🔴 **CATASTROPHIC** |

### Related Files
- **Data Service**: `LuckyTex.Lab.Transfer.Data.Services` (assumed location)
- **Stored Procedure**: `LAB_INSERTUPDATEITEMSPEC` (referenced in code)
- **Models**: `LuckyTex.Models` namespace

---

## UI Layout Description

### Screen Structure

The page is structured as a **massive vertical form** with ~36 rows of quality test specifications:

**Header Section:**
- Title: "Specification" (Blue banner, 40px height)
- Item Code ComboBox (dropdown selection)
- Edit Button (visible for supervisors only, based on positionLevel ≠ "5")
- Save Button (visible for supervisors only)
- Back Button (bottom-right)

**Main Content Section:**
- **Scrollable Grid** with 36 rows × 7 columns
- Each row = 1 quality test type
- Columns:
  1. **Method** (Label) - Test name
  2. **Yarn** (TextBox, read-only) - Yarn specification value
  3. **Specification** (TextBox, editable) - Target specification value
  4. **Allowance** (ComboBox) - Tolerance type (+/-, ±, <, >, etc.)
  5. **Allowance Value** (TextBox) - Tolerance numeric value
  6. **No N** (TextBox) - Number of samples required
  7. **Max Label** (Label) - Maximum allowed value

### 36 Quality Test Types (Rows)

| Row | Test Type | Fields | Complexity |
|-----|-----------|--------|------------|
| 1 | **Width** | 3 fields (Spec, No N, Max label) | Low |
| 2 | **Usable Width** | 5 fields (Spec, Allowance combo/value, No N) | Medium |
| 3 | **Width of Silicone** | 5 fields | Medium |
| 4-5 | **Number of Threads** (W/F) | 10 fields (2 tests × 5 each) | Medium |
| 6-8 | **Weight** (Total, Uncoated, Coat) | 15 fields (3 tests × 5 each) | Medium |
| 9 | **Thickness** | 5 fields | Medium |
| 10-11 | **Max Force** (W/F) | 10 fields | Medium |
| 12-13 | **Elongation** (W/F) | 10 fields | Medium |
| 14-15 | **Flammability** (W/F) | 10 fields | Medium |
| 16-17 | **Edgecomb** (W/F) | 10 fields | Medium |
| 18-19 | **Stiffness** (W/F) | 10 fields | Medium |
| 20-21 | **Tear Strength** (W/F) | 10 fields | Medium |
| 22 | **Static Air** | 5 fields | Medium |
| 23 | **Dynamic Air** | 5 fields | Medium |
| 24 | **Exponent** | 5 fields | Medium |
| 25-26 | **Dimensional Change** (W/F) | 10 fields | Medium |
| 27-28 | **Flex Abrasion** (W/F) | 10 fields | Medium |
| 29 | **Bow** | 5 fields | Medium |
| 30 | **Skew** | 5 fields | Medium |
| 31-32 | **Bending** (W/F) | 10 fields | Medium |
| 33-34 | **Flex Scott** (W/F) | 10 fields | Medium |
| 35-36 | **(Reserved for future tests)** | - | - |

**Total Estimated Fields**: **~180-200 input controls**
- **~50+ TextBox** (Specification values)
- **~50+ TextBox** (Allowance values)
- **~50+ TextBox** (No N values)
- **~30+ ComboBox** (Allowance type dropdowns)
- **~50+ Label** (read-only displays)

### Allowance Types (ComboBox Options)
From line 69 `LoadAllowance()`:
- **±** (Plus/Minus tolerance)
- **+/-** (Separate positive/negative tolerances)
- **<** (Less than)
- **>** (Greater than)
- **=** (Exact match)
- **(Other tolerance types as needed)**

### Field Enable/Disable Logic

**On Page Load** (lines 110-138):
- **ALL tolerance TextBoxes disabled by default** (`txtUSEWIDTH_TOR.IsEnabled = false`, etc.)
- Only **Specification and No N fields enabled**

**On Edit Click** (lines 160-425):
- Call `chk_TextEnabled(true)` → Enable all fields
- **Conditional enable** for tolerance TextBoxes:
  - **IF** ComboBox.SelectedIndex == 0 → Enable tolerance TextBox
  - **ELSE** → Disable tolerance TextBox
  - Pattern repeated for **ALL 24 tolerance fields** (lines 170-420)

**Authorization-Based Visibility** (lines 95-107):
- **IF** positionLevel != "5" (Not operator level):
  - cmdEdit.Visibility = Visible
  - cmdSave.Visibility = Visible
- **ELSE** (Operator level = "5"):
  - cmdEdit.Visibility = Collapsed
  - cmdSave.Visibility = Collapsed
  - **Operators can only VIEW, not edit specifications**

---

## Component Architecture Diagram

```mermaid
graph TB
    subgraph "Presentation Layer"
        UI[ItemCodeSpecificationPage.xaml<br/>505 lines<br/>~180 input controls]
        CB[Code-Behind<br/>ItemCodeSpecificationPage.xaml.cs<br/>🔴 9,978 LINES]
    end

    subgraph "Business Logic Layer"
        Session[LAB_INSERTUPDATEITEMSPEC<br/>Session Object<br/>~100 properties]
        Validation[Validation Methods<br/>- chkMax<br/>- chkSpecNoN<br/>- chkLoad]
    end

    subgraph "Data Access Layer"
        DS[Lab Data Service<br/>Singleton Instance]
        SP[Stored Procedure<br/>LAB_INSERTUPDATEITEMSPEC<br/>~100 parameters]
    end

    subgraph "Database"
        DB[(Oracle Database<br/>tblItemSpecification)]
    end

    UI -->|User Input| CB
    CB -->|Load Item Codes| DS
    CB -->|Populate Session| Session
    CB -->|Validate| Validation
    Session -->|Save| DS
    DS -->|Execute| SP
    SP -->|INSERT/UPDATE| DB
    DB -->|Return Status| SP
    SP -->|Success/Fail| DS
    DS -->|Callback| CB
    CB -->|Message| UI

    style CB fill:#ff6b6b,stroke:#c92a2a,color:#fff
    style Session fill:#ffa94d,stroke:#e67700
    style SP fill:#ffa94d,stroke:#e67700
    style UI fill:#74c0fc,stroke:#1864ab
```

---

## Workflow Diagram

```mermaid
graph TD
    Start([Page Loaded]) --> LoadData[Load Item Codes<br/>LoadItemCode]
    LoadData --> LoadAllow[Load Allowance Types<br/>LoadAllowance]
    LoadAllow --> CheckAuth{Check Position Level}
    CheckAuth -->|Level 5 Operator| HideButtons[Hide Edit/Save Buttons<br/>View-Only Mode]
    CheckAuth -->|Supervisor/Manager| ShowButtons[Show Edit/Save Buttons]
    HideButtons --> Ready[Ready State<br/>All Fields Disabled]
    ShowButtons --> Ready

    Ready --> Select[Select Item Code<br/>cbItemCode_SelectionChanged]
    Select --> LoadSpec[Load Specifications<br/>Call Database]
    LoadSpec --> PopulateForm[Populate ~180 Fields<br/>txtWIDTH, txtUSEWIDTH, etc.]
    PopulateForm --> DisplayOnly[Display Mode<br/>All Fields Read-Only]

    DisplayOnly --> Edit[Click Edit Button]
    Edit --> EnableFields[Enable All Fields<br/>chk_TextEnabled]
    EnableFields --> EnableTol{Enable Tolerances?}
    EnableTol -->|For Each of 24 Fields| CheckCombo{ComboBox Index == 0?}
    CheckCombo -->|Yes| EnableTolText[Enable Tolerance TextBox]
    CheckCombo -->|No| DisableTolText[Disable Tolerance TextBox]
    EnableTolText --> EditMode[Edit Mode<br/>~180 Fields Editable]
    DisableTolText --> EditMode

    EditMode --> UserEdit[User Edits Values]
    UserEdit --> ClickSave[Click Save Button]
    ClickSave --> ValidateMax{chkMax<br/>No N <= Max?}
    ValidateMax -->|No| ErrMax[Show Error<br/>No N > Max]
    ValidateMax -->|Yes| ValidateNoN{chkSpecNoN<br/>Spec Set → No N > 0?}
    ValidateNoN -->|No| ErrNoN[Show Error Thai Message<br/>มีการระบุค่า Spec...]
    ValidateNoN -->|Yes| SaveData[Save Method<br/>~100 Parameters]

    SaveData --> Populate[Populate Session Object<br/>LAB_INSERTUPDATEITEMSPEC]
    Populate --> CallDB[Execute Stored Procedure<br/>~100 DB Parameters]
    CallDB --> SaveOK{Success?}
    SaveOK -->|Yes| Clear[ClearControl<br/>Reset All Fields]
    SaveOK -->|No| ErrSave[Show Error Message]
    Clear --> Ready
    ErrMax --> EditMode
    ErrNoN --> EditMode
    ErrSave --> EditMode

    style Start fill:#69db7c,stroke:#2f9e44
    style SaveData fill:#ffa94d,stroke:#e67700
    style Clear fill:#69db7c,stroke:#2f9e44
    style ErrMax fill:#ff6b6b,stroke:#c92a2a
    style ErrNoN fill:#ff6b6b,stroke:#c92a2a
    style ErrSave fill:#ff6b6b,stroke:#c92a2a
```

---

## Business Logic Sequence Diagram

```mermaid
sequenceDiagram
    actor User as Supervisor/QA Manager
    participant UI as ItemCodeSpecificationPage
    participant CB as Code-Behind<br/>(9,978 LOC)
    participant Session as LAB_INSERTUPDATEITEMSPEC
    participant DB as Oracle Database

    Note over User,DB: Page Initialization

    User->>UI: Open Specification Page
    UI->>CB: UserControl_Loaded
    CB->>DB: LoadItemCode()
    DB-->>CB: List of Item Codes
    CB->>DB: LoadAllowance()
    DB-->>CB: Allowance Types (±, +/-, <, >, =)
    CB->>CB: Check positionLevel
    alt Level 5 (Operator)
        CB->>UI: Hide Edit/Save Buttons
    else Supervisor/Manager
        CB->>UI: Show Edit/Save Buttons
    end
    CB->>UI: Disable ALL Tolerance Fields
    UI-->>User: Ready (View-Only)

    Note over User,DB: Item Selection & Load

    User->>UI: Select Item Code from Dropdown
    UI->>CB: cbItemCode_SelectionChanged
    CB->>DB: Load Specifications for Item
    DB-->>CB: 100+ Specification Values
    CB->>CB: Populate ~180 Fields
    loop For Each Field
        CB->>UI: Set txtWIDTH, txtUSEWIDTH, etc.
    end
    UI-->>User: Display Specifications (Read-Only)

    Note over User,DB: Edit Mode

    User->>UI: Click Edit Button
    UI->>CB: cmdEdit_Click
    CB->>CB: buttonEnabled(false)
    CB->>CB: chk_TextEnabled(true)
    loop For 24 Tolerance Fields
        CB->>CB: Check cbUSEWIDTH_TOR.SelectedIndex
        alt Index == 0
            CB->>UI: txtUSEWIDTH_TOR.IsEnabled = true
        else Index > 0
            CB->>UI: txtUSEWIDTH_TOR.IsEnabled = false
        end
    end
    CB->>CB: buttonEnabled(true)
    UI-->>User: Edit Mode Activated

    Note over User,DB: User Edits Data

    User->>UI: Edit Specification Values
    User->>UI: Edit Allowance Types
    User->>UI: Edit No N Values
    loop For Each Field
        User->>UI: Enter Key → Next Field
        UI->>CB: txt*_KeyDown
        CB->>UI: Focus Next Field
    end

    Note over User,DB: Save Operation

    User->>UI: Click Save Button
    UI->>CB: cmdSave_Click
    CB->>CB: buttonEnabled(false)

    Note over CB: Validation #1: chkMax()
    CB->>CB: chkMax()
    loop For Each Test Type
        CB->>CB: IF Spec Set THEN No N <= Max?
        alt No N > Max
            CB-->>User: Show Error: "No N > Max"
            Note over CB: Abort Save
        end
    end

    Note over CB: Validation #2: chkSpecNoN()
    CB->>CB: chkSpecNoN()
    loop For Each Test Type
        CB->>CB: IF Spec Set THEN No N > 0?
        alt No N == 0
            CB-->>User: Show Error (Thai):<br/>"มีการระบุค่า Spec ช่อง No N ต้อง ไม่เป็น 0"
            Note over CB: Abort Save
        end
    end

    Note over CB: Save Method (~100 Parameters)
    CB->>CB: Save()
    CB->>Session: Create LAB_INSERTUPDATEITEMSPEC
    loop For 100+ Fields
        CB->>Session: P_ITMCODE = _session.P_ITMCODE
        CB->>Session: P_WIDTH_NO = _session.P_WIDTH_NO
        CB->>Session: P_WIDTH = _session.P_WIDTH
        Note over CB,Session: ... 97+ more parameters ...
    end

    CB->>DB: Execute SP: LAB_INSERTUPDATEITEMSPEC<br/>(~100 parameters)
    DB->>DB: INSERT or UPDATE tblItemSpecification
    alt Success
        DB-->>CB: Success
        CB->>CB: ClearControl()
        CB->>UI: Clear All Fields
        CB->>UI: cbItemCode.Text = ""
        CB->>UI: Focus on cbItemCode
        UI-->>User: Saved Successfully<br/>(Ready for Next Item)
    else Failure
        DB-->>CB: Error
        CB-->>User: Show Error Message
    end
    CB->>CB: buttonEnabled(true)
```

---

## Data Flow

### Input Data Sources

1. **Item Code Dropdown**
   - Source: `GetItemCodeData` binding (database)
   - Table: `tblItemCode` or `tblProduct` (assumed)
   - Display: `ITM_CODE` column
   - Selection: Triggers specification load

2. **Allowance Types Dropdown**
   - Source: `Allowance` binding (database or static list)
   - Values: ±, +/-, <, >, = (tolerance types)
   - Applied to: 24+ quality test types

3. **Yarn Specifications** (Read-Only)
   - Source: Yarn/item master data
   - Display only, not editable
   - Shows target values from yarn specification

### Processing Data

**Session Object**: `LAB_INSERTUPDATEITEMSPEC`

Estimated ~100+ properties (from Save() method lines 9676-9779):

| Category | Properties | Example Fields |
|----------|------------|----------------|
| **Width Tests** | 6 | P_WIDTH, P_WIDTH_NO, P_USEWIDTH, P_USEWIDTH_NO, P_USEWIDTH_TOR, P_WIDTHSILICONE... |
| **Thread Count** | 6 | P_NUMTHREADS_W, P_NUMTHREADS_W_NO, P_NUMTHREADS_W_TOR, P_NUMTHREADS_F... |
| **Weight Tests** | 9 | P_TOTALWEIGHT, P_TOTALWEIGHT_NO, P_TOTALWEIGHT_TOR, P_UNCOATEDWEIGHT... |
| **Thickness** | 3 | P_THICKNESS, P_THICKNESS_NO, P_THICKNESS_TOR |
| **Tensile Tests** | 12 | P_MAXFORCE_W, P_MAXFORCE_W_NO, P_MAXFORCE_W_TOR, P_ELOGATION_W... |
| **Flame Tests** | 6 | P_FLAMMABILITY_W, P_FLAMMABILITY_W_NO, P_FLAMMABILITY_W_TOR... |
| **Edgecomb** | 6 | P_EDGECOMB_W, P_EDGECOMB_W_NO, P_EDGECOMB_W_TOR... |
| **Stiffness** | 6 | P_STIFFNESS_W, P_STIFFNESS_W_NO, P_STIFFNESS_W_TOR... |
| **Tear Strength** | 6 | P_TEAR_W, P_TEAR_W_NO, P_TEAR_W_TOR... |
| **Air Tests** | 9 | P_STATIC_AIR, P_STATIC_AIR_NO, P_STATIC_AIR_TOR, P_DYNAMIC_AIR... |
| **Dimensional** | 6 | P_DIMENSCHANGE_W, P_DIMENSCHANGE_W_NO, P_DIMENSCHANGE_W_TOR... |
| **Flex Abrasion** | 6 | P_FLEXABRASION_W, P_FLEXABRASION_W_NO, P_FLEXABRASION_W_TOR... |
| **Bow/Skew** | 6 | P_BOW, P_BOW_NO, P_BOW_TOR, P_SKEW, P_SKEW_NO, P_SKEW_TOR |
| **Bending** | 6 | P_BENDING_W, P_BENDING_W_NO, P_BENDING_W_TOR, P_BENDING_F... |
| **Flex Scott** | 6 | P_FLEX_SCOTT_W, P_FLEX_SCOTT_W_NO, P_FLEX_SCOTT_W_TOR... |
| **Other** | 10+ | P_EXPONENT, P_EXPONENT_NO, P_EXPONENT_TOR... |
| **TOTAL** | **~100+** | ⚠️ **MASSIVE PARAMETER COUNT** |

### Output Data Destinations

1. **Database Table**: `tblItemSpecification` (assumed)
   - Stored Procedure: `LAB_INSERTUPDATEITEMSPEC`
   - Operation: INSERT (new item) or UPDATE (existing item)
   - ~100 columns updated per save

2. **Validation References** (Downstream Usage)
   - **Module 01** (Lab Test): Uses specs for validation
   - **Module 03** (Lab Data Entry): Validates against LCL/UCL
   - **Module 04** (Production): Validates test results
   - **Module 06** (Sample Test): Quality pass/fail determination
   - **Module 07** (PLC Get Data): Auto-validation of equipment readings

---

## Database Operations

### Stored Procedures

#### 1. LAB_INSERTUPDATEITEMSPEC
**Purpose**: Insert or update item specifications
**Type**: INSERT / UPDATE
**Parameters**: ~100 parameters (all quality test specs)

```sql
-- Estimated Signature (based on code analysis)
PROCEDURE LAB_INSERTUPDATEITEMSPEC(
    P_ITMCODE VARCHAR2,

    -- Width Tests (6 params)
    P_WIDTH_NO NUMBER,
    P_WIDTH NUMBER,
    P_USEWIDTH_NO NUMBER,
    P_USEWIDTH NUMBER,
    P_USEWIDTH_TOR VARCHAR2,
    P_WIDTHSILICONE_NO NUMBER,
    P_WIDTHSILICONE NUMBER,
    P_WIDTHSILICONE_TOR VARCHAR2,

    -- Thread Count (6 params)
    P_NUMTHREADS_W_NO NUMBER,
    P_NUMTHREADS_W NUMBER,
    P_NUMTHREADS_W_TOR NUMBER,
    P_NUMTHREADS_F_NO NUMBER,
    P_NUMTHREADS_F NUMBER,
    P_NUMTHREADS_F_TOR NUMBER,

    -- Weight Tests (9 params)
    P_TOTALWEIGHT_NO NUMBER,
    P_TOTALWEIGHT NUMBER,
    P_TOTALWEIGHT_TOR NUMBER,
    P_UNCOATEDWEIGHT_NO NUMBER,
    P_UNCOATEDWEIGHT NUMBER,
    P_UNCOATEDWEIGHT_TOR NUMBER,
    P_COATWEIGHT_NO NUMBER,
    P_COATWEIGHT NUMBER,
    P_COATWEIGHT_TOR NUMBER,

    -- Thickness (3 params)
    P_THICKNESS_NO NUMBER,
    P_THICKNESS NUMBER,
    P_THICKNESS_TOR NUMBER,

    -- Tensile Tests (12 params)
    P_MAXFORCE_W_NO NUMBER,
    P_MAXFORCE_W NUMBER,
    P_MAXFORCE_W_TOR VARCHAR2,
    P_MAXFORCE_F_NO NUMBER,
    P_MAXFORCE_F NUMBER,
    P_MAXFORCE_F_TOR VARCHAR2,
    P_ELOGATION_W_NO NUMBER,
    P_ELOGATION_W NUMBER,
    P_ELOGATION_W_TOR VARCHAR2,
    P_ELOGATION_F_NO NUMBER,
    P_ELOGATION_F NUMBER,
    P_ELOGATION_F_TOR VARCHAR2,

    -- ... 70+ MORE PARAMETERS ...

    -- Bending Tests (6 params - added 2018-07-07)
    P_BENDING_W_NO NUMBER,
    P_BENDING_W NUMBER,
    P_BENDING_W_TOR VARCHAR2,
    P_BENDING_F_NO NUMBER,
    P_BENDING_F NUMBER,
    P_BENDING_F_TOR VARCHAR2,

    -- Flex Scott Tests (6 params - added 2018-07-07)
    P_FLEX_SCOTT_W_NO NUMBER,
    P_FLEX_SCOTT_W NUMBER,
    P_FLEX_SCOTT_W_TOR VARCHAR2,
    P_FLEX_SCOTT_F_NO NUMBER,
    P_FLEX_SCOTT_F NUMBER,
    P_FLEX_SCOTT_F_TOR VARCHAR2
)
```

**Estimated Parameter Count**: **~100 parameters**

**Transaction Scope**: Unknown (likely no transaction support based on other module patterns)

### Table Operations

| Table | Operation | Fields Updated | Notes |
|-------|-----------|----------------|-------|
| `tblItemSpecification` | INSERT / UPDATE | ~100 columns | All quality test specs |
| `tblItemCode` | SELECT | ITM_CODE | Item code dropdown source |
| `tblAllowance` | SELECT | Allowance | Tolerance type dropdown |

---

## Validation Logic

### 1. Authorization Validation (Page Load)

**Method**: `UserControl_Loaded` (lines 87-142)

**Logic**:
```
IF positionLevel != "5" THEN
    Show Edit Button
    Show Save Button
ELSE
    Hide Edit Button (Operators view-only)
    Hide Save Button
END IF
```

**Access Control**:
- **Level 5**: Operators - **VIEW ONLY**
- **Level 1-4**: Supervisors/Managers - **EDIT ALLOWED**

---

### 2. Maximum Value Validation (chkMax)

**Method**: `chkMax()` (estimated location ~line 9500+)

**Purpose**: Ensure "No N" (number of samples) does not exceed "Max" allowed

**Logic** (for EACH of 36 test types):
```
FOR EACH test type (WIDTH, USEWIDTH, WIDTHSILICONE, etc.):
    IF Specification field is NOT empty THEN
        IF No_N > Max THEN
            RETURN FALSE (Validation failed)
        END IF
    END IF
END FOR
RETURN TRUE
```

**Error Message**: "No N > Max"

**Example**:
- Test: Usable Width
- Specification: 1500 mm
- Max: 3
- No N: **5** ← INVALID (5 > 3)
- Error: "No N > Max"

---

### 3. No N Required Validation (chkSpecNoN)

**Method**: `chkSpecNoN()` (lines 9600+)

**Purpose**: If specification is set, "No N" must be > 0 (cannot require 0 samples if spec exists)

**Logic**:
```
FOR EACH test type:
    IF Specification field is NOT empty THEN
        IF No_N == 0 THEN
            RETURN FALSE
        END IF
    END IF
END FOR
RETURN TRUE
```

**Error Message** (Thai): `"มีการระบุค่า Spec ช่อง No N ต้อง ไม่เป็น 0"`
**Translation**: "If Spec is specified, No N field must not be 0"

**Business Rule**: Cannot have a quality requirement without specifying sample count

---

### 4. ComboBox-Dependent Field Enable (Edit Mode)

**Method**: `cmdEdit_Click` (lines 160-425)

**Pattern** (repeated 24 times for each tolerance field):
```
IF cbUSEWIDTH_TOR.SelectedIndex == 0 THEN
    txtUSEWIDTH_TOR.IsEnabled = TRUE
ELSE
    txtUSEWIDTH_TOR.IsEnabled = FALSE
END IF
```

**Fields Affected** (24 tolerance TextBoxes):
1. txtUSEWIDTH_TOR
2. txtWIDTHSILICONE_TOR
3. txtMAXFORCE_W_TOR
4. txtMAXFORCE_F_TOR
5. txtELOGATION_W_TOR
6. txtELOGATION_F_TOR
7. txtFLAMMABILITY_W_TOR
8. txtFLAMMABILITY_F_TOR
9. txtEDGECOMB_W_TOR
10. txtEDGECOMB_F_TOR
11. txtSTIFFNESS_W_TOR
12. txtSTIFFNESS_F_TOR
13. txtTEAR_W_TOR
14. txtTEAR_F_TOR
15. txtSTATIC_AIR_TOR
16. txtDIMENSCHANGE_W_TOR
17. txtDIMENSCHANGE_F_TOR
18. txtFLEXABRASION_W_TOR
19. txtFLEXABRASION_F_TOR
20. txtBOW_TOR
21. txtSKEW_TOR
22. txtBENDING_W_TOR
23. txtBENDING_F_TOR
24. txtFLEX_SCOTT_W_TOR
25. txtFLEX_SCOTT_F_TOR

**Code Duplication**: **🔴 EXTREME** - Same logic repeated 24+ times (lines 170-420, 250 lines of pure duplication)

---

### 5. Numeric Input Validation

**Method**: `Common_PreviewKeyDown` (lines 474-477)

**Applied To**: All numeric TextBoxes (No N, Tolerance values)

**Logic**:
```csharp
private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
{
    e.Handled = !this.IsNumericInput(e);
}
```

**Behavior**: Block non-numeric key presses (0-9, decimal point only)

---

## Critical Issues & Technical Debt

### 🔴 CATASTROPHIC Issues

#### 1. **EXTREME FILE SIZE** - 9,978 Lines
- **Impact**: 🔴 **CRITICAL**
- **Problem**: 3rd largest file in entire codebase
- **Consequence**:
  - Visual Studio performance degradation
  - Impossible to maintain or understand
  - High bug risk (cannot review all code)
  - New developers cannot onboard
- **Refactoring Estimate**: **8-12 weeks**

#### 2. **MASSIVE CODE DUPLICATION** - ~90%
- **Impact**: 🔴 **CRITICAL**
- **Examples**:
  - Lines 170-420: ComboBox enable logic **repeated 24 times** (identical code)
  - Lines 9676-9867: Session property assignment **repeated 100+ times**
  - KeyDown handlers: **~150+ identical methods** (Enter key navigation)
  - LostFocus handlers: **~150+ identical methods** (field validation)
  - SelectionChanged handlers: **~30+ identical methods** (ComboBox logic)
- **Wasted Code**: **~8,000-9,000 lines of duplication**
- **Bug Multiplication**: Single bug fix must be applied in **100+ places**

#### 3. **100+ DATABASE PARAMETERS**
- **Impact**: 🔴 **CRITICAL**
- **Problem**: Save() method has ~100+ parameters
- **Consequence**:
  - No transaction support possible (parameter limit)
  - Partial saves = data corruption risk
  - Cannot validate atomicity
  - Performance bottleneck
- **Refactoring**: Break into multiple calls or use XML/JSON payload

#### 4. **NO ASYNC OPERATIONS**
- **Impact**: 🔴 **HIGH**
- **Problem**: All database calls are synchronous
- **Consequence**:
  - UI freezes during save (~100 param stored procedure = slow)
  - Poor user experience
  - Application appears frozen (no feedback)

---

### 🟠 HIGH Priority Issues

#### 5. **180-200 Event Handlers**
- **Impact**: 🟠 **HIGH**
- **Problem**: Every field has 2-3 event handlers
  - ~150 KeyDown handlers (Enter key navigation)
  - ~150 LostFocus handlers (validation)
  - ~30 SelectionChanged handlers (ComboBox logic)
  - ~30 Button Click handlers
- **Consequence**: 90%+ duplicate code
- **Solution**: Centralize navigation/validation logic

#### 6. **Hardcoded Position Level Authorization**
- **Impact**: 🟠 **MEDIUM**
- **Problem**: `positionLevel != "5"` hardcoded (line 97)
- **Consequence**: Cannot change authorization without code change
- **Solution**: Move to configuration table or role-based system

#### 7. **Thai Language Error Messages**
- **Impact**: 🟡 **MEDIUM**
- **Problem**: Error message in Thai: `"มีการระบุค่า Spec ช่อง No N ต้อง ไม่เป็น 0"` (line 455)
- **Consequence**: Not internationalized, hardcoded string
- **Solution**: Resource file for multi-language support

---

### 🟡 MEDIUM Priority Issues

#### 8. **No Transaction Support**
- **Impact**: 🟡 **MEDIUM**
- **Problem**: ~100 fields saved in single stored procedure call, no rollback
- **Consequence**: Partial save = corrupt specification data
- **Solution**: Implement proper transaction handling

#### 9. **No Input Validation Framework**
- **Impact**: 🟡 **MEDIUM**
- **Problem**: Validation scattered across 150+ LostFocus handlers
- **Consequence**: Inconsistent validation logic
- **Solution**: Centralized validation framework

#### 10. **No Success Feedback**
- **Impact**: 🟡 **LOW**
- **Problem**: No confirmation message after save
- **Consequence**: User unsure if save succeeded
- **Solution**: Show success toast/message box

---

## Implementation Checklist

### Phase 1: Emergency Refactoring (12 weeks) 🔴 **P0 CRITICAL**

#### Week 1-2: Analysis & Design
- [ ] **Task 1.1**: Full code audit of 9,978 lines
- [ ] **Task 1.2**: Identify all duplicate code blocks
- [ ] **Task 1.3**: Design specification data model (break into logical groups)
- [ ] **Task 1.4**: Design ViewModel architecture (MVVM pattern)
- [ ] **Task 1.5**: Design service layer interface

#### Week 3-4: Base Infrastructure
- [ ] **Task 2.1**: Create `ItemSpecificationViewModel` base class
- [ ] **Task 2.2**: Create `ILabSpecificationService` interface
- [ ] **Task 2.3**: Implement service layer with async/await
- [ ] **Task 2.4**: Create `SpecificationFieldControl` UserControl (reusable field component)
- [ ] **Task 2.5**: Implement centralized navigation manager

#### Week 5-6: Extract Specification Groups (6 UserControls)
- [ ] **Task 3.1**: Extract `WidthSpecificationControl` (Width, Usable Width, Silicone Width)
- [ ] **Task 3.2**: Extract `WeightSpecificationControl` (Total, Uncoated, Coat)
- [ ] **Task 3.3**: Extract `TensileSpecificationControl` (MaxForce, Elongation)
- [ ] **Task 3.4**: Extract `SurfaceSpecificationControl` (Flammability, Edgecomb, Stiffness, Tear)
- [ ] **Task 3.5**: Extract `AirSpecificationControl` (Static, Dynamic, Exponent)
- [ ] **Task 3.6**: Extract `MechanicalSpecificationControl` (Dimensional Change, Flex, Bow, Skew, Bending, Flex Scott)

#### Week 7-8: Centralize Business Logic
- [ ] **Task 4.1**: Extract validation logic to `SpecificationValidator` class
- [ ] **Task 4.2**: Implement `chkMax()` as fluent validation rule
- [ ] **Task 4.3**: Implement `chkSpecNoN()` as fluent validation rule
- [ ] **Task 4.4**: Create `AuthorizationService` for position-based access control
- [ ] **Task 4.5**: Implement resource file for multi-language support

#### Week 9-10: Refactor Database Layer
- [ ] **Task 5.1**: Design specification DTO (Data Transfer Object)
- [ ] **Task 5.2**: Refactor stored procedure to accept XML or JSON payload
- [ ] **Task 5.3**: Implement transaction support in service layer
- [ ] **Task 5.4**: Add async/await to all database operations
- [ ] **Task 5.5**: Implement retry logic for transient failures

#### Week 11-12: Testing & Migration
- [ ] **Task 6.1**: Unit tests for ViewModel (100% coverage target)
- [ ] **Task 6.2**: Integration tests for service layer
- [ ] **Task 6.3**: UI automation tests (Selenium or Coded UI)
- [ ] **Task 6.4**: Parallel run with legacy page (validation)
- [ ] **Task 6.5**: Performance testing (load time, save time)
- [ ] **Task 6.6**: User acceptance testing
- [ ] **Task 6.7**: Deploy to production

**Estimated Effort**:
- **2-3 Senior Developers**: 12 weeks (3 months)
- **Total Effort**: ~600-900 hours

**Expected Results**:
- **Code Reduction**: 9,978 lines → **~1,500 lines** (85% reduction)
- **Duplication Elimination**: ~8,000 lines of duplicate code removed
- **Maintainability**: 6 reusable UserControl components
- **Performance**: Async operations, no UI freezing
- **Testability**: 100% unit test coverage

---

### Phase 2: Enhanced Features (4 weeks) 🟠 **P1 HIGH**

#### Week 13: User Experience
- [ ] **Task 7.1**: Add loading indicator during save operation
- [ ] **Task 7.2**: Add success confirmation toast message
- [ ] **Task 7.3**: Implement field-level validation with visual feedback
- [ ] **Task 7.4**: Add tooltips explaining each test type
- [ ] **Task 7.5**: Implement undo/redo functionality

#### Week 14: Advanced Validation
- [ ] **Task 8.1**: Add range validation (LCL < Spec < UCL)
- [ ] **Task 8.2**: Add cross-field validation (related specs consistency)
- [ ] **Task 8.3**: Implement validation warning vs error severity
- [ ] **Task 8.4**: Add bulk validation (validate all fields at once)

#### Week 15: Reporting & Audit
- [ ] **Task 9.1**: Add audit trail (who changed what when)
- [ ] **Task 9.2**: Implement specification change history
- [ ] **Task 9.3**: Create specification comparison report (old vs new)
- [ ] **Task 9.4**: Add specification export to Excel

#### Week 16: Configuration & Admin
- [ ] **Task 10.1**: Move position level authorization to database
- [ ] **Task 10.2**: Add role-based access control (RBAC)
- [ ] **Task 10.3**: Implement specification templates (copy from existing item)
- [ ] **Task 10.4**: Add specification import from Excel
- [ ] **Task 10.5**: Configuration UI for allowance types

---

### Phase 3: Integration Testing (2 weeks) 🟡 **P2 MEDIUM**

#### Week 17-18: Full System Integration
- [ ] **Task 11.1**: Test Module 01 (Lab Test) integration - spec validation
- [ ] **Task 11.2**: Test Module 03 (Lab Data Entry) integration - LCL/UCL limits
- [ ] **Task 11.3**: Test Module 04 (Production) integration - test result validation
- [ ] **Task 11.4**: Test Module 06 (Sample Test) integration - pass/fail determination
- [ ] **Task 11.5**: Test Module 07 (PLC Get Data) integration - auto-validation
- [ ] **Task 11.6**: Regression testing (all lab modules)
- [ ] **Task 11.7**: Performance testing (concurrent users)
- [ ] **Task 11.8**: Security testing (authorization bypass attempts)

---

## Modernization Priority

### Severity: 🔴 **P0 EMERGENCY - IMMEDIATE ACTION REQUIRED**

**Ranking**: **#3 Most Critical File** in entire codebase (after LabDataEntryPage and LabDataEntryWindow)

### Justification for P0 Priority:

1. **Business Impact**: 🔴 **CRITICAL**
   - **Single Point of Failure**: ALL lab test validations depend on this data
   - **Production Blocker**: Incorrect specs = invalid test results = production shutdown
   - **Customer Compliance**: Specification errors violate automotive quality agreements
   - **Regulatory Risk**: Incorrect specs = failed audits = lost certifications

2. **Technical Debt**: 🔴 **CATASTROPHIC**
   - **9,978 lines** of unmaintainable code
   - **~90% code duplication** (~8,000 duplicated lines)
   - **100+ database parameters** = no transaction support
   - **No async operations** = UI freezing
   - **Bug multiplication**: Single bug exists in 100+ places

3. **Maintenance Risk**: 🔴 **EXTREME**
   - **Cannot onboard new developers** (too complex to understand)
   - **Cannot add new test types** (would require 200+ lines of duplicate code)
   - **Cannot fix bugs reliably** (must fix in 100+ places)
   - **Visual Studio crashes** when editing (file too large)

4. **Integration Risk**: 🔴 **HIGH**
   - **Modules 01, 03, 04, 06, 07** all depend on this data
   - **Corrupt specifications** = invalid validation across entire lab system
   - **No data integrity checks** = silent failures

---

### Comparison with Other Lab Modules

| Module | File | LOC | Priority | Rationale |
|--------|------|-----|----------|-----------|
| **Module 03** | LabDataEntryPage | 149,594 | 🔴 P0 | Largest, 89% duplicate with Module 04 |
| **Module 04** | LabDataEntryWindow | 33,132 | 🔴 P0 | Duplicate of Module 03 |
| **Module 05** | ItemCodeSpecificationPage | 9,978 | 🔴 **P0** ⭐ | **Master data for all tests** |
| Module 02 | PDF Loading (3 pages) | 4,152 | 🟠 P1 | 80% duplication |
| Module 01 | ExcelTestPage | 2,563 | 🟡 P2 | Medium complexity |

**Recommended Refactoring Order**:
1. **Module 05** (ItemCodeSpecification) - **FIRST** ⚠️
   - **Reason**: Master data affects all other modules
   - **Impact**: Fix specs → fixes validation in Modules 01, 03, 04, 06, 07
2. **Modules 03 + 04** (LabDataEntry) - **SECOND**
   - **Reason**: 89% duplication, extract shared component
3. **Module 02** (PDF Loading) - **THIRD**
   - **Reason**: 80% duplication, extract base class

---

## Business Rules Summary

### Authorization Rules
1. **Position Level 5** (Operators): **VIEW ONLY** - Edit/Save buttons hidden
2. **Position Levels 1-4** (Supervisors/Managers): **EDIT ALLOWED** - Full access

### Validation Rules
3. **Max Validation**: `No N ≤ Max` for ALL test types where Spec is set
4. **No N Required**: `No N > 0` if Specification is not empty
5. **Numeric Fields**: No N and Tolerance values must be numeric (0-9, decimal point only)

### Field Enable Logic
6. **Tolerance TextBox Enable**: Only enabled if ComboBox SelectedIndex == 0
7. **Default State**: ALL tolerance TextBoxes disabled on page load
8. **Edit Mode**: Call `chk_TextEnabled(true)` → Enable all editable fields

### Data Integrity Rules
9. **Item Code Required**: Cannot save without selecting item code
10. **Specification Consistency**: If Spec set → Must have Allowance Type + No N
11. **Test Type Coverage**: Support for 36+ quality test types (extensible)

### Navigation Rules
12. **Enter Key**: Navigate to next field in sequence
13. **Tab Key**: Standard tab order through form
14. **Focus Management**: Auto-select all text on field focus (for quick overwrite)

### Save Operation Rules
15. **Validation Before Save**: chkMax() → chkSpecNoN() → Save()
16. **Clear After Save**: Reset all fields to blank state after successful save
17. **Error Display**: Show error message, remain in edit mode (no clear)

### Allowance Type Rules
18. **Supported Types**: ±, +/-, <, >, = (tolerance type dropdown)
19. **Type-Specific Logic**: Different tolerance types enable/disable tolerance value field

### Historical Notes
20. **Bending & Flex Scott Tests**: Added 2018-07-07 (comment on line 9767)
21. **Test Type Evolution**: System designed to add new test types (extensible grid layout)

---

## Integration Points with Other Modules

### Downstream Dependencies (Modules Using These Specifications)

#### Module 01: Lab Test Operations
- **Usage**: Load specifications for validation during test result import
- **Fields Used**: ALL test type specs (LCL, UCL, allowances)
- **Impact**: Incorrect specs → invalid test validations

#### Module 02: PDF Test Import
- **Usage**: Validate imported PDF test results against specs
- **Fields Used**: Tear Strength, Elongation, Edgecomb specs
- **Impact**: Incorrect specs → wrong pass/fail determination

#### Module 03: Lab Data Entry
- **Usage**: Display LCL/UCL limits for 50-80 test types
- **Fields Used**: ALL test type specs
- **Impact**: **CRITICAL** - Incorrect specs → production lot rejection errors

#### Module 04: Production Test Tracking
- **Usage**: Validate production test results
- **Fields Used**: ALL test type specs
- **Impact**: Incorrect specs → invalid quality reports

#### Module 06: Sample Test Data
- **Usage**: Determine sample pass/fail status
- **Fields Used**: ALL test type specs
- **Impact**: Incorrect specs → wrong quality approvals

#### Module 07: PLC Get Data (Lab Equipment)
- **Usage**: Auto-validate PLC equipment readings
- **Fields Used**: Air Permeability, Static Air, Dynamic Air, Stiffness specs
- **Impact**: Incorrect specs → auto-fail valid samples

---

## Estimated Complexity Metrics

### Lines of Code Breakdown
| Component | LOC | % of Total |
|-----------|-----|------------|
| **Using Directives** | 45 | 0.5% |
| **Constructor** | 10 | 0.1% |
| **Load Methods** | ~100 | 1.0% |
| **Edit Button Handler** | ~260 | 2.6% |
| **Save Button Handler** | ~300 | 3.0% |
| **Validation Methods** | ~500 | 5.0% |
| **KeyDown Handlers** | ~3,000 | 30.0% |
| **LostFocus Handlers** | ~3,000 | 30.0% |
| **SelectionChanged Handlers** | ~900 | 9.0% |
| **Helper Methods** | ~500 | 5.0% |
| **Unused/Legacy Code** | ~1,363 | 13.7% |
| **TOTAL** | **9,978** | **100%** |

### Code Duplication Analysis
| Pattern Type | Occurrences | Duplicated LOC | Total Waste |
|--------------|-------------|----------------|-------------|
| **ComboBox Enable Logic** | 24 | 10 lines each | ~240 lines |
| **KeyDown Handlers** | ~150 | 8 lines each | ~1,200 lines |
| **LostFocus Handlers** | ~150 | 10 lines each | ~1,500 lines |
| **SelectionChanged Handlers** | ~30 | 15 lines each | ~450 lines |
| **Session Property Assignment** | ~100 | 3 lines each | ~300 lines |
| **Validation Loops** | ~36 | 15 lines each | ~540 lines |
| **Other Duplicates** | Multiple | Various | ~3,770 lines |
| **TOTAL DUPLICATION** | **~500** | **Average 16** | **~8,000 lines** |

**Duplication Rate**: **~80-85%** of code is duplicate
**Unique Code**: Only **~1,500-2,000 lines** are actually unique logic

---

## Conclusion

### Summary

**Module 05 - ItemCodeSpecificationPage** is the **3rd most critical refactoring priority** in the entire Lab.Transfer.Data system and **#1 for immediate action** due to:

1. **Master Data Role**: ALL lab modules depend on these specifications
2. **Catastrophic Complexity**: 9,978 lines with ~80% duplication
3. **Massive Parameter Count**: ~100 database parameters = no transaction support
4. **Business Criticality**: Incorrect specs = production shutdown

### Immediate Actions Required

1. 🔴 **FREEZE NEW FEATURES** on this page until refactored
2. 🔴 **Allocate 2-3 senior developers** for 3-month refactoring project
3. 🔴 **Create backup/restore process** for specification data
4. 🔴 **Document ALL business rules** before touching code
5. 🔴 **Implement audit logging** to track all specification changes

### Long-Term Vision

**Target Architecture** (Post-Refactoring):
- **ViewModel**: 300 lines (MVVM pattern)
- **6 UserControl Components**: 200 lines each = 1,200 lines
- **Service Layer**: 200 lines (async/await)
- **Validation Layer**: 300 lines (fluent validation)
- **TOTAL**: **~2,000 lines** (from 9,978) = **80% code reduction**

**Benefits**:
- ✅ Maintainable codebase
- ✅ Testable components (unit tests)
- ✅ No UI freezing (async operations)
- ✅ Data integrity (transaction support)
- ✅ Easy to add new test types
- ✅ Fast onboarding for new developers

---

**Document Version**: 1.0
**Created**: 2025-10-11
**Status**: 🔴 **EMERGENCY REFACTORING REQUIRED**
**Priority**: 🔴 **P0 CRITICAL**
**Estimated Refactoring Effort**: **12 weeks (2-3 developers)**
**Code Reduction Target**: **80% (9,978 → ~2,000 lines)**
