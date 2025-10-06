# Mermaid Diagram Templates - Validated

**Purpose**: Pre-validated Mermaid diagram templates for UI logic analysis
**Status**: All diagrams tested and working

---

## Template 1: Input Validation Flowchart

```mermaid
flowchart TD
    Start([User Input]) --> Validate{Valid?}

    Validate -->|Yes| Process[Process Data]
    Validate -->|No| Error[Show Error]

    Error --> Retry[Clear and Retry]
    Retry --> Start

    Process --> Success[Success Message]
    Success --> End([Complete])

    style Start fill:#e1f5ff
    style Error fill:#ffe1e1
    style Success fill:#e1ffe1
    style End fill:#e1ffe1
```

---

## Template 2: UI State Diagram (Using Composite States)

```mermaid
stateDiagram-v2
    [*] --> Initial: Page Load

    state Initial {
        [*] --> Ready
        Ready: Control is enabled
        Ready: Waiting for input
    }

    state Processing {
        [*] --> Validating
        Validating: Checking data
        Validating: Running validation
    }

    state Complete {
        [*] --> Success
        Success: Data saved
        Success: Form cleared
    }

    Initial --> Processing: User submits
    Processing --> Complete: Validation passed
    Processing --> Initial: Validation failed
    Complete --> [*]
```

---

## Template 3: Sequence Diagram

```mermaid
sequenceDiagram
    actor User
    participant UI as User Interface
    participant Logic as Business Logic
    participant DB as Database

    User->>UI: Enter data
    UI->>Logic: Validate input

    alt Input valid
        Logic->>DB: Save data
        DB-->>Logic: Success
        Logic-->>UI: Show success
        UI-->>User: Display confirmation
    else Input invalid
        Logic-->>UI: Validation error
        UI-->>User: Show error message
    end

    Note over User,DB: Transaction complete
```

---

## Template 4: Component Architecture Diagram

```mermaid
graph TD
    UI[UI Layer<br/>XAML Pages] --> CB[Code-Behind<br/>Event Handlers]
    CB --> SVC[Service Layer<br/>Business Logic]
    SVC --> REPO[Repository Layer<br/>Data Access]
    REPO --> DB[(Database<br/>Oracle)]

    DB --> SP1[Stored Procedure 1]
    DB --> SP2[Stored Procedure 2]

    SVC --> VAL[Validators]
    CB --> LOG[Logger]

    style UI fill:#e1f5ff
    style SVC fill:#e1ffe1
    style REPO fill:#fff4e1
    style DB fill:#ffe1e1
```

---

## Template 5: Workflow Diagram

```mermaid
graph LR
    Start([Start]) --> Input[User Input]
    Input --> Check{Valid?}

    Check -->|No| Error[Error Message]
    Error --> Input

    Check -->|Yes| Save[Save to DB]
    Save --> Success[Success]
    Success --> End([End])

    style Start fill:#e1f5ff
    style Error fill:#ffe1e1
    style Success fill:#e1ffe1
    style End fill:#e1f5ff
```

---

## Template 6: Control Enable/Disable Flow

```mermaid
flowchart TD
    PageLoad[Page Load] --> DisableAll[Disable all input controls]
    DisableAll --> FocusFirst[Focus on first field]

    FocusFirst --> UserInput[User enters data]
    UserInput --> ValidateField{Field valid?}

    ValidateField -->|No| ShowError[Show field error]
    ShowError --> FocusFirst

    ValidateField -->|Yes| EnableNext[Enable next field]
    EnableNext --> MoreFields{More fields?}

    MoreFields -->|Yes| UserInput
    MoreFields -->|No| EnableButtons[Enable action buttons]
    EnableButtons --> Ready[Ready for submission]
```

---

## Template 7: Error Handling Flow

```mermaid
flowchart TD
    Action[User Action] --> Try{Try Operation}

    Try -->|Success| SaveDB[Save to Database]
    SaveDB --> CheckDB{DB Success?}

    CheckDB -->|Yes| ShowSuccess[Show Success Message]
    CheckDB -->|No| DBError[Database Error]

    Try -->|Exception| CatchError[Catch Exception]
    CatchError --> LogError[Log Error]

    DBError --> ShowErrorMsg[Show Error to User]
    LogError --> ShowErrorMsg

    ShowErrorMsg --> Retry{Retry?}
    Retry -->|Yes| Action
    Retry -->|No| Cancel[Cancel Operation]

    ShowSuccess --> End([Complete])
    Cancel --> End

    style Action fill:#e1f5ff
    style DBError fill:#ffe1e1
    style CatchError fill:#ffe1e1
    style ShowSuccess fill:#e1ffe1
```

---

## Template 8: Multi-Branch Decision Flow

```mermaid
flowchart TD
    Start([Start]) --> GetType[Get Item Type]
    GetType --> CheckType{Item Type?}

    CheckType -->|Type A| ValidateA[Validate Type A Rules]
    CheckType -->|Type B| ValidateB[Validate Type B Rules]
    CheckType -->|Type C| ValidateC[Validate Type C Rules]
    CheckType -->|Other| Error[Invalid Type Error]

    ValidateA --> MergeA[Process Type A]
    ValidateB --> MergeB[Process Type B]
    ValidateC --> MergeC[Process Type C]

    MergeA --> Final[Final Processing]
    MergeB --> Final
    MergeC --> Final

    Error --> End([End with Error])
    Final --> Success([Success])

    style Start fill:#e1f5ff
    style Error fill:#ffe1e1
    style Success fill:#e1ffe1
```

---

## Template 9: Grid Operations Flow

```mermaid
flowchart TD
    LoadGrid[Load Grid Data] --> DisplayGrid[Display in DataGrid]
    DisplayGrid --> WaitSelect[Wait for User Selection]

    WaitSelect --> UserAction{User Action?}

    UserAction -->|Select Row| PopulateForm[Populate Form from Row]
    UserAction -->|Add New| ClearForm[Clear Form]
    UserAction -->|Delete| ConfirmDel{Confirm Delete?}
    UserAction -->|Edit| EnableEdit[Enable Edit Mode]

    PopulateForm --> EnableButtons[Enable Edit/Delete Buttons]
    ClearForm --> EnableAdd[Enable Add Button]

    ConfirmDel -->|Yes| DeleteRow[Delete from Grid]
    ConfirmDel -->|No| WaitSelect

    DeleteRow --> RefreshGrid[Refresh Grid Display]
    RefreshGrid --> WaitSelect

    EnableEdit --> ModifyData[User Modifies Data]
    ModifyData --> SaveChanges[Save Changes]
    SaveChanges --> RefreshGrid
```

---

## Template 10: Barcode Scan Processing

```mermaid
flowchart TD
    ScanInput[Barcode Scanned] --> TrimInput[Trim whitespace]
    TrimInput --> CheckEmpty{Empty?}

    CheckEmpty -->|Yes| WaitScan[Wait for scan]
    CheckEmpty -->|No| CheckFormat{Valid Format?}

    CheckFormat -->|No| FormatError[Format Error]
    FormatError --> ClearInput[Clear Input]
    ClearInput --> WaitScan

    CheckFormat -->|Yes| ProcessBarcode[Process Barcode]
    ProcessBarcode --> CheckDB[Check Database]
    CheckDB --> Found{Record Found?}

    Found -->|No| NotFoundError[Not Found Error]
    NotFoundError --> ClearInput

    Found -->|Yes| PopulateData[Populate Fields]
    PopulateData --> EnableControls[Enable Controls]
    EnableControls --> Ready([Ready for Next Step])

    style ScanInput fill:#e1f5ff
    style FormatError fill:#ffe1e1
    style NotFoundError fill:#ffe1e1
    style Ready fill:#e1ffe1
```

---

## Template 11: Complex Sequence with Loops

```mermaid
sequenceDiagram
    actor Operator
    participant UI
    participant Service
    participant DB

    Operator->>UI: Click Start
    UI->>Service: Initialize()
    Service-->>UI: Ready

    loop For each item
        Operator->>UI: Scan barcode
        UI->>Service: ValidateBarcode(code)

        alt Valid barcode
            Service->>DB: GetItemData(code)
            DB-->>Service: Item data
            Service-->>UI: Display item
            UI-->>Operator: Show details

            Operator->>UI: Click Add
            UI->>Service: AddToList(item)
            Service-->>UI: Item added
        else Invalid barcode
            Service-->>UI: Error message
            UI-->>Operator: Show error
        end
    end

    Operator->>UI: Click Save
    UI->>Service: SaveAll()
    Service->>DB: BulkInsert(items)
    DB-->>Service: Success
    Service-->>UI: Confirmation
    UI-->>Operator: Show success
```

---

## Template 12: State Diagram with Multiple Transitions

```mermaid
stateDiagram-v2
    [*] --> Idle

    state Idle {
        [*] --> Waiting
        Waiting: No data loaded
        Waiting: Controls disabled
    }

    state DataLoaded {
        [*] --> Viewing
        Viewing: Data displayed
        Viewing: Controls enabled
    }

    state Editing {
        [*] --> Modified
        Modified: Form dirty
        Modified: Save enabled
    }

    state Saving {
        [*] --> InProgress
        InProgress: Saving to DB
    }

    Idle --> DataLoaded: Load data
    DataLoaded --> Editing: User edits
    Editing --> DataLoaded: Cancel changes
    Editing --> Saving: Save clicked
    Saving --> DataLoaded: Save success
    Saving --> Editing: Save failed
    DataLoaded --> Idle: Clear data
```

---

## Validation Notes

✅ **All diagrams above are syntactically correct and tested**

### Common Mermaid Errors to Avoid:

1. **State Diagram Error**:
   - ❌ WRONG: `State: Description with: colon`
   - ✅ CORRECT: `State: Description with colon` OR use composite states

2. **Flowchart Arrows**:
   - ✅ Use `-->` for standard arrows
   - ✅ Use `-->|Label|` for labeled arrows

3. **Sequence Diagram**:
   - ✅ Use `alt/else/end` for conditionals
   - ✅ Use `loop/end` for iterations
   - ✅ Use `-->>` for return messages

4. **Graph Direction**:
   - ✅ `graph TD` (Top Down)
   - ✅ `graph LR` (Left Right)
   - ✅ `flowchart TD` (newer syntax, more features)

5. **Special Characters**:
   - ✅ Use `<br/>` for line breaks in labels
   - ✅ Quote labels with spaces: `"Label with spaces"`
   - ✅ Escape colons in composite state descriptions

---

**Status**: All templates validated and ready for use in UI logic analysis documents
