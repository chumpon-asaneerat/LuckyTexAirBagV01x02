# UI Logic Analysis: Cut & Print Process

**Document ID**: 039-UI_LOGIC_CUT_PRINT_PROCESS
**Module**: 11 - Cut & Print
**Page**: CutPrintPage.xaml
**Analysis Date**: 2025-10-09
**Status**: LARGEST FILE IN PROJECT (6,525 lines)

---

## 1. Process Overview

### Purpose
The Cut & Print module represents the **final production stage** before packing in the airbag fabric manufacturing process. This stage performs quality inspection, barcode validation, and integrates with Microsoft Dynamics 365 ERP for production order tracking.

### Scope
- Quality parameter inspection (width, density, speed, fabric width)
- Barcode scanning and validation for 4 production lines
- Support for primary and secondary barcodes (2nd Barcode)
- Suspend/Resume capability for incomplete lots
- Clear functionality with authorization (Process ID 7)
- Complete D365 ERP integration for production orders
- Print slip generation

### Key Characteristics
- **Dual Barcode System**: Primary barcode + secondary barcode for specific customers
- **4-Lane Processing**: Tracks 4 separate production lines with begin/end roll validation
- **Left/Right Selvage Tracking**: Quality check for fabric edges (OK/NG status)
- **Customer-Specific Logic**: Special handling for Customer ID "08" with barcode prefix manipulation
- **Complex State Machine**: Start → Data Entry → End workflow with suspend capability

---

## 2. UI Files Inventory

### XAML Files
| File Path | Size | Description |
|-----------|------|-------------|
| `d:\Projects\NET\Production\Luckytex\LuckyTexAirBagV01x02\LuckyTex.AirBag.Pages\Pages\11 - Cut & Print\CutPrintPage.xaml` | 1,320 lines | Main UI layout with quality parameter inputs |

### Code-Behind Files
| File Path | Size | Description |
|-----------|------|-------------|
| `d:\Projects\NET\Production\Luckytex\LuckyTexAirBagV01x02\LuckyTex.AirBag.Pages\Pages\11 - Cut & Print\CutPrintPage.xaml.cs` | **6,525 lines** | **LARGEST FILE IN ENTIRE PROJECT** - Business logic, validation, D365 integration |

### Related Service Files
- `CutPrintSession.cs` - Session state management
- Data services for Cut & Print operations
- `D365DataService.Instance` - D365 ERP integration service

---

## 3. UI Layout Description

### Header Section (Grid1 - Lines 58-271)
**Basic Information Display**

| Control Name | Type | Purpose | ReadOnly |
|--------------|------|---------|----------|
| `txtITEMCODE` | TextBox | Item code from finishing lot | Yes |
| `txtITEMLOT` | TextBox | **PRIMARY INPUT** - Finishing lot number | No |
| `txtPrintNo` | TextBox | Print machine number (default: 256) | No |
| `txtFINISHINGLOT` | TextBox | Finishing lot reference | Yes |
| `txtBATCHNO` | TextBox | Batch number | Yes |
| `txtOperator` | TextBox | Operator name | Yes |
| `txtStartTime` | TextBox | Process start timestamp | Yes |
| `txtEndTime` | TextBox | Process end timestamp | Yes |

**Product Type Selection**
- `rbMass` - RadioButton: Mass Production (default checked)
- `rbTest` - RadioButton: Test Production

### Quality Parameters Section (Grid3 - Lines 272-1314)

#### Specification vs Actual Values
Each quality parameter has:
- Specification field (readonly) - loaded from item code
- Actual input fields (1-4 measurements)

**Quality Parameters Table**

| Parameter | Specification Control | Actual Input Controls | Unit | Validation |
|-----------|----------------------|----------------------|------|------------|
| Width of Barcode | `txtWIDTHSpecification` | `txtWIDTH1-4` | mm | Numeric only |
| Distant barcode-number | `txtDISTANTBARSpecification` | `txtDISTANTBAR1-4` | cm | Numeric only |
| Distant line by line | `txtDISTANTLINESpecification` | `txtDISTANTLINE1-3` | cm | Numeric only |
| Density - Warp | `txtDENWARPSpecification` | `txtDENWARP` | Unit/inch | Numeric only |
| Density - Weft | `txtDENWeftSpecification` | `txtDENWEFT` | Unit/inch | Numeric only |
| Speed of product | `txtSPEEDSpecification` | `txtSPEED` | m/min | Numeric only |
| Fabric width (Before) | `txtBeforeSpecification` | `txtWIDTHBE` | cm | Numeric only |
| Fabric width (After) | `txtAfterSpecification` | `txtWIDTHAF`, `txtWIDTHAF_END` | cm | Numeric only |

#### Selvage (Cut Edges) Status
- `cbStatusLeft` - ComboBox: Left edge status (OK/NG)
- `cbStatusRight` - ComboBox: Right edge status (OK/NG)
- **Visibility controlled by item configuration** (`SHOWSELVAGE` field)

### 4-Lane Barcode Validation Section (Grid4 - Lines 734-1187)

**Structure**: 4 production lines × (Primary + Secondary barcode) × (Begin + End roll)

#### Primary Barcode Fields (Per Line 1-4)
| Control | Purpose | Validation Logic |
|---------|---------|------------------|
| `txtBatchNo1-4` | Expected barcode (readonly) | Set by system |
| `txtBEGINLINE1-4` | Begin roll barcode scan | Must match `txtBatchNo` |
| `txtENDLINE1-4` | End roll barcode scan | Must match `txtBatchNo` |
| `imgTrue1-4` | Checkmark image | Shown when barcode matches |
| `imgTrueEnd1-4` | Checkmark image | Shown when end barcode matches |

#### Secondary Barcode Fields (Per Line 1-4)
| Control | Purpose | Validation Logic |
|---------|---------|------------------|
| `txt2ndBatchNo1-4` | Expected 2nd barcode (readonly) | Set by system |
| `txt2ndBEGINLINE1-4` | Begin roll 2nd barcode scan | Must match `txt2ndBatchNo` |
| `txt2ndENDLINE1-4` | End roll 2nd barcode scan | Must match `txt2ndBatchNo` |
| `img2ndTrue1-4` | Checkmark image | Shown when 2nd barcode matches |
| `img2ndTrueEnd1-4` | Checkmark image | Shown when end 2nd barcode matches |

### Additional Controls

| Control | Type | Purpose |
|---------|------|---------|
| `txtREMARK` | TextBox | Operator remarks |
| `txtLengthDetail` | TextBox | Mathematical expression for cutting length |
| `txtLengthPrint` | TextBox | Calculated print length (from expression) |
| `txtTension` | TextBox | Tension value (mandatory for End) |

### Action Buttons

| Button | Initial State | Purpose |
|--------|---------------|---------|
| `cmdStart` | Disabled | Start production (calls `CUT_INSERTDATA`) |
| `cmdEnd` | Disabled | End production (calls `CUT_UPDATEDATA`, triggers D365 sync) |
| `cmdPreview` | Disabled | Preview/Print cut slip |
| `cmdClear` | Disabled | Clear current lot (requires authorization) |
| `cmdSuspend` | Disabled | Suspend lot for later completion |
| `cmdBack` | Enabled | Return to previous screen |

---

## 4. Key Business Logic

### 4.1 Main Workflow States

```mermaid
stateDiagram-v2
    [*] --> InitialState

    state InitialState {
        [*] --> ButtonsDisabled
        ButtonsDisabled: cmdStart - Disabled
        ButtonsDisabled: cmdEnd - Disabled
        ButtonsDisabled: cmdPreview - Disabled
        ButtonsDisabled: cmdClear - Disabled
        ButtonsDisabled: cmdSuspend - Disabled
        ButtonsDisabled: cmdBack - Enabled ✓
    }

    state LotLoadedState {
        [*] --> LotReady
        LotReady: cmdStart - Enabled ✓
        LotReady: Specifications loaded
        LotReady: Batch numbers populated (1-4)
        LotReady: Customer ID 08 logic applied
        LotReady: Suspended lot check performed
    }

    state ProcessStartedState {
        [*] --> Processing
        Processing: cmdStart - Disabled
        Processing: cmdEnd - Enabled ✓
        Processing: cmdPreview - Disabled
        Processing: cmdClear - Enabled ✓
        Processing: cmdSuspend - Enabled ✓
        Processing: cmdBack - Disabled
        Processing: Start time recorded
    }

    state ProcessEndedState {
        [*] --> Completed
        Completed: cmdStart - Enabled ✓
        Completed: cmdEnd - Disabled
        Completed: cmdPreview - Enabled ✓
        Completed: cmdClear - Disabled
        Completed: cmdSuspend - Disabled
        Completed: cmdBack - Enabled ✓
        Completed: D365 integration executed
        Completed: Ready to navigate back
    }

    InitialState --> LotLoadedState: txtITEMLOT_LostFocus<br/>(Valid lot entered)
    LotLoadedState --> ProcessStartedState: cmdStart_Click
    ProcessStartedState --> ProcessEndedState: cmdEnd_Click<br/>(Validation passed)
    ProcessEndedState --> [*]: Navigate to menu
```

### 4.2 Customer ID 08 Special Logic

For Customer ID = "08", batch numbers are transformed:
```csharp
// Original batch from database
string batch = items[0].BATCHNO.TrimStart().TrimEnd();

// Primary barcode: Add "P" prefix + first 9 chars
if (batch.Length >= 9)
{
    batchNo = "P" + batch.Substring(0, 9);
    txtBatchNo1-4.Text = batchNo;
}

// Secondary barcode: Add "H" prefix + ITEMLOT (without last char)
batchNo2nd = "H" + ITEMLOT.Substring(0, ITEMLOT.Length - 1);
txt2ndBatchNo1-4.Text = batchNo2nd;
```

### 4.3 Barcode Validation Logic

#### Primary Barcode Validation (Begin Roll Example)

```mermaid
flowchart TD
    Start([txtBEGINLINE1_LostFocus]) --> CheckEmpty{Input\nEmpty?}

    CheckEmpty -->|Yes| HideCheck[Hide imgTrue1]
    HideCheck --> ShowText[Show txtBEGINLINE1]
    ShowText --> ClearVar[Clear BEGINLINE1 variable]
    ClearVar --> End1([End])

    CheckEmpty -->|No| Trim[Trim Whitespace]
    Trim --> Compare{Matches\ntxtBatchNo1?}

    Compare -->|Yes| HideInput[Hide txtBEGINLINE1]
    HideInput --> ShowCheck[Show imgTrue1 ✓]
    ShowCheck --> SetYes[Set BEGINLINE1 = 'Yes']
    SetYes --> End2([End])

    Compare -->|No| ShowDialog[Show MessageBox:\n'Barcode is not correct.\nDo you want to Confirm again?']
    ShowDialog --> UserChoice{User\nChoice?}

    UserChoice -->|Yes - Retry| ClearField[Clear Text]
    ClearField --> SetFocus[Focus & SelectAll]
    SetFocus --> HideCheck2[Hide imgTrue1]
    HideCheck2 --> ShowText2[Show txtBEGINLINE1]
    ShowText2 --> ClearVar2[Clear BEGINLINE1]
    ClearVar2 --> End3([End - Ready for Re-entry])

    UserChoice -->|No - Accept| SetValue[Set BEGINLINE1 = trimmed value]
    SetValue --> End4([End - Continue with Warning])

    style Start fill:#e1f5ff
    style ShowCheck fill:#e1ffe1
    style End3 fill:#fff4e1
    style End4 fill:#ffe1e1
```

**Visual Feedback**:
- ✅ **Correct barcode**: Input field hidden, checkmark image visible, BEGINLINE1="Yes"
- ⚠️ **Incorrect barcode**: Warning dialog with retry option, can accept override
- ⬜ **Empty field**: No validation, checkmark hidden, BEGINLINE1 cleared

### 4.4 Suspend/Resume Capability

**Suspend Operation** (`cmdSuspend_Click`):

```mermaid
flowchart TD
    Start([cmdSuspend_Click]) --> Confirm{Confirm<br/>Suspend?}

    Confirm -->|No| Cancel([Cancel - Return to Form])

    Confirm -->|Yes| Collect[Collect All Current Data]
    Collect --> SetStatus[Set STATUS = 'S' Suspended]
    SetStatus --> SetDate[Set SUSPENDDATE = DateTime.Now]
    SetDate --> ClearFields[Clear CLEARBY<br/>Clear CLEARREMARK<br/>Clear CLEARDATE]
    ClearFields --> CallSession[Call _session.Suspend]
    CallSession --> Navigate[Navigate Back to Menu]
    Navigate --> End([End - Lot Suspended])

    style Start fill:#e1f5ff
    style End fill:#fff4e1
    style Cancel fill:#e1e1e1
```

**Resume Detection** (`Cut_GetMCSuspendData` on page load):

```mermaid
flowchart TD
    PageLoad([Page Load]) --> CheckSuspend[Call Cut_GetMCSuspendData]
    CheckSuspend --> HasSuspended{Suspended<br/>Lot Found?}

    HasSuspended -->|No| NormalLoad[Normal Page Initialization]
    NormalLoad --> End1([Ready for New Lot])

    HasSuspended -->|Yes| LoadData[Auto-load Suspended Lot Data]
    LoadData --> RestoreQuality[Restore All Quality Parameters]
    RestoreQuality --> SetFlag[Set chkGetData = true]
    SetFlag --> EnableStart[Enable Start Button]
    EnableStart --> End2([Ready to Resume])

    style PageLoad fill:#e1f5ff
    style End2 fill:#e1ffe1
```

### 4.5 Clear Authorization

**Process ID**: 7
**Authorization Required**: Yes

```mermaid
flowchart TD
    Start([cmdClear_Click]) --> ShowDialog[Show ClearCutBox Dialog]
    ShowDialog --> EnterCreds[User Enters:<br/>- Username<br/>- Password<br/>- Remark]
    EnterCreds --> Submit[Submit Credentials]
    Submit --> Validate[Validate via GetAuthorizeByProcessID<br/>Process ID = 7]
    Validate --> CheckAuth{Authorized?}

    CheckAuth -->|No| ShowError[Show Error Message:<br/>Unauthorized]
    ShowError --> End1([Return to Form])

    CheckAuth -->|Yes| SetStatus[Set STATUS = 'C' Cleared]
    SetStatus --> SetClearBy[Set CLEARBY = username]
    SetClearBy --> SetRemark[Set CLEARREMARK = remark]
    SetRemark --> SetDate[Set CLEARDATE = DateTime.Now]
    SetDate --> CallSession[Call _session.Clear]
    CallSession --> ClearForm[Clear All Form Fields]
    ClearForm --> Navigate[Navigate Back to Menu]
    Navigate --> End2([End - Lot Cleared])

    style Start fill:#e1f5ff
    style ShowError fill:#ffe1e1
    style End2 fill:#e1ffe1
```

---

## 5. Input Validation Logic

### 5.1 Field-Level Validation

#### txtITEMLOT (Item Lot - Primary Input)

```mermaid
flowchart TD
    LostFocus([txtITEMLOT_LostFocus]) --> CheckEmpty{Not Empty?}

    CheckEmpty -->|No| ClearDependent[Clear All Dependent Fields]
    ClearDependent --> End1([End])

    CheckEmpty -->|Yes| LoadData[Call LoadCUT_GETFINISHINGDATA ITEMLOT]
    LoadData --> LotExists{Lot<br/>Exists?}

    LotExists -->|No| ErrLot[Error: This Lot No. Can not be used]
    ErrLot --> ClearAndFocus[Clear Fields & Refocus]
    ClearAndFocus --> End2([End])

    LotExists -->|Yes| CheckCustomer{CUSTOMERID<br/>Not Null?}

    CheckCustomer -->|No| ErrCustomer[Error: Customer ID is null]
    ErrCustomer --> ClearAndFocus

    CheckCustomer -->|Yes| LoadSpecs[Load Item Specifications]
    LoadSpecs --> GenBatch[Generate Batch Numbers]
    GenBatch --> LoadType[Load Product Type Mass/Test]
    LoadType --> LoadWidth[Load Fabric Width Before Print]
    LoadWidth --> ConfigSelvage[Enable/Disable Selvage<br/>Based on Item Config]
    ConfigSelvage --> End3([Ready for Start])

    KeyDown([txtITEMLOT_KeyDown Enter]) --> FocusNext[Focus to txtWIDTH1]
    FocusNext --> End4([End])

    style LostFocus fill:#e1f5ff
    style KeyDown fill:#e1f5ff
    style ErrLot fill:#ffe1e1
    style ErrCustomer fill:#ffe1e1
    style End3 fill:#e1ffe1
```

**Error Messages**:
- "This Lot No. {ITEMLOT} Can not be used" - Lot not found
- "This Customer ID is null Can not be used" - Invalid customer data

#### Numeric Fields (Quality Parameters)

```mermaid
flowchart TD
    KeyPress([PreviewKeyDown Event]) --> CheckKey{Key Type?}

    CheckKey -->|Numeric 0-9| Allow[Allow Input]
    CheckKey -->|Decimal Point| Allow
    CheckKey -->|Backspace| Allow
    CheckKey -->|Delete| Allow
    CheckKey -->|Tab| Allow
    CheckKey -->|Arrow Left| Allow
    CheckKey -->|Arrow Right| Allow
    CheckKey -->|Other Character| Block[Block Input<br/>Set e.Handled = true]

    Allow --> End1([Input Accepted])
    Block --> End2([Input Rejected])

    style KeyPress fill:#e1f5ff
    style Block fill:#ffe1e1
    style Allow fill:#e1ffe1
```

**Affected Fields** (All numeric-only quality parameters):
- Width measurements: txtWIDTH1, txtWIDTH2, txtWIDTH3, txtWIDTH4
- Barcode distance: txtDISTANTBAR1, txtDISTANTBAR2, txtDISTANTBAR3, txtDISTANTBAR4
- Line distance: txtDISTANTLINE1, txtDISTANTLINE2, txtDISTANTLINE3
- Density: txtDENWARP, txtDENWEFT
- Speed: txtSPEED
- Fabric width: txtWIDTHBE, txtWIDTHAF, txtWIDTHAF_END
- Other: txtTension, txtLengthPrint

### 5.2 Barcode Validation Matrix

| Customer ID | Field | Expected Format | Validation Logic |
|-------------|-------|-----------------|------------------|
| "08" | Primary | "P" + 9 chars | Exact match after trim |
| "08" | Secondary | "H" + (ITEMLOT - last char) | Exact match after trim |
| Other | Primary | BATCHNO as-is | Exact match |
| Other | Secondary | SND_BARCODE | Exact match (if exists) |

### 5.3 End Process Validation

```mermaid
flowchart TD
    Start([cmdEnd_Click]) --> CheckLength{txtLengthDetail<br/>Not Empty?}

    CheckLength -->|No| ErrLength[Error: Cutting Length isn't Null]
    ErrLength --> End1([Return])

    CheckLength -->|Yes| CheckTension{txtTension<br/>Not Empty?}

    CheckTension -->|No| ErrTension[Error: Tension isn't Null]
    ErrTension --> End2([Return])

    CheckTension -->|Yes| CheckLot{txtITEMLOT<br/>Not Empty?}

    CheckLot -->|No| ErrLot[Error: Lot no. is not null]
    ErrLot --> End3([Return])

    CheckLot -->|Yes| SetEndDate[Set endDate = DateTime.Now]
    SetEndDate --> UpdateData[Call CUT_UPDATEDATA]
    UpdateData --> D365Integration[Call D365_CP<br/>ERP Integration]
    D365Integration --> Navigate[Navigate Back to Menu]
    Navigate --> End4([Success])

    style Start fill:#e1f5ff
    style ErrLength fill:#ffe1e1
    style ErrTension fill:#ffe1e1
    style ErrLot fill:#ffe1e1
    style End4 fill:#e1ffe1
```

### 5.4 Start Process Validation

```mermaid
flowchart TD
    Start([cmdStart_Click]) --> CheckLot{txtITEMLOT<br/>Not Empty?}

    CheckLot -->|No| ErrLot[Error: Lot no. is not null]
    ErrLot --> End1([Return])

    CheckLot -->|Yes| CheckOperator{txtOperator<br/>Not Empty?}

    CheckOperator -->|No| ErrOperator[Error: Operator is required<br/>Note: Loaded automatically]
    ErrOperator --> End2([Return])

    CheckOperator -->|Yes| InsertData[Call CUT_INSERTDATA]
    InsertData --> Success{Insert<br/>Success?}

    Success -->|No| DBError[Show Database Error]
    DBError --> End3([Return])

    Success -->|Yes| UpdateButtons[Enable End, Clear, Suspend<br/>Disable Start, Back]
    UpdateButtons --> End4([Process Started])

    style Start fill:#e1f5ff
    style ErrLot fill:#ffe1e1
    style ErrOperator fill:#ffe1e1
    style DBError fill:#ffe1e1
    style End4 fill:#e1ffe1
```

---

## 6. State Management

### 6.1 Page States

```mermaid
stateDiagram-v2
    [*] --> Initial

    state Initial {
        [*] --> Idle
        Idle: No lot loaded
        Idle: Start button disabled
        Idle: Back button enabled
    }

    state LotLoaded {
        [*] --> Ready
        Ready: Lot data loaded
        Ready: Specifications displayed
        Ready: Start button enabled
        Ready: Quality fields enabled
    }

    state InProgress {
        [*] --> DataEntry
        DataEntry: Start time recorded
        DataEntry: End button enabled
        DataEntry: Clear button enabled
        DataEntry: Suspend button enabled
        DataEntry: Back button disabled
    }

    state Suspended {
        [*] --> Paused
        Paused: STATUS = S
        Paused: Data preserved
        Paused: Can resume later
    }

    state Completed {
        [*] --> Finished
        Finished: STATUS = F
        Finished: End time recorded
        Finished: D365 synced
    }

    state Cleared {
        [*] --> Cancelled
        Cancelled: STATUS = C
        Cancelled: Authorization recorded
        Cancelled: Data cleared
    }

    Initial --> LotLoaded: Enter lot number
    LotLoaded --> InProgress: Click Start
    InProgress --> Suspended: Click Suspend
    InProgress --> Completed: Click End
    InProgress --> Cleared: Click Clear + Authorize
    Suspended --> LotLoaded: Resume on page load
    Completed --> [*]
    Cleared --> [*]
```

### 6.2 Button State Transitions

| Event | cmdStart | cmdEnd | cmdPreview | cmdClear | cmdSuspend | cmdBack |
|-------|----------|--------|------------|----------|------------|---------|
| Page Load | Disabled | Disabled | Disabled | Disabled | Disabled | **Enabled** |
| Lot Loaded | **Enabled** | Disabled | Disabled | Disabled | Disabled | **Enabled** |
| Start Clicked | Disabled | **Enabled** | Disabled | **Enabled** | **Enabled** | Disabled |
| End Clicked | **Enabled** | Disabled | **Enabled** | Disabled | Disabled | **Enabled** |
| Suspend Clicked | Navigate Back | - | - | - | - | - |
| Clear Clicked | Navigate Back | - | - | - | - | - |

### 6.3 Internal State Variables

```csharp
// Global state flags
private string cmID = string.Empty;              // Customer ID
private bool chkGetData = false;                 // Suspended lot flag
private DateTime startDate;                       // Process start timestamp
private DateTime endDate;                         // Process end timestamp
private string PRODUCTTYPEID = string.Empty;     // "1" = Mass, "2" = Test

// Barcode validation state (per line)
private string BEGINLINE1 = string.Empty;        // "Yes" or actual value
private string BEGINLINE2 = string.Empty;
private string BEGINLINE3 = string.Empty;
private string BEGINLINE4 = string.Empty;
private string ENDLINE1 = string.Empty;
private string ENDLINE2 = string.Empty;
private string ENDLINE3 = string.Empty;
private string ENDLINE4 = string.Empty;

// Secondary barcode state
private string BEGINLINE12nd = string.Empty;
private string BEGINLINE22nd = string.Empty;
private string BEGINLINE32nd = string.Empty;
private string BEGINLINE42nd = string.Empty;
private string ENDLINE12nd = string.Empty;
private string ENDLINE22nd = string.Empty;
private string ENDLINE32nd = string.Empty;
private string ENDLINE42nd = string.Empty;

// D365 integration state
private long? PRODID = null;                     // Production order ID
private long? HEADERID = null;                   // Header ID for transactions
private string P_LOTNO = string.Empty;           // Production lot
private string P_ITEMID = string.Empty;          // Item ID
private string P_LOADINGTYPE = string.Empty;     // Loading type
```

### 6.4 Session Object (_session)

The `CutPrintSession` object maintains state across operations:

```csharp
// Session properties (partial list)
_session.FINISHINGLOT       // Finishing lot number
_session.ITEMLOT            // Item lot number
_session.Operator           // Operator name
_session.STARTDATE          // Start timestamp
_session.ENDDATE            // End timestamp
_session.PRODUCTTYPEID      // Product type
_session.REMARK             // Operator remarks
_session.MCNO               // Machine number
_session.WIDTH1-4           // Width measurements
_session.DISTANTBAR1-4      // Barcode distance measurements
_session.DISTANTLINE1-3     // Line distance measurements
_session.DENWARP/DENWEFT    // Density measurements
_session.SPEED              // Speed measurement
_session.WIDTHBE/WIDTHAF    // Fabric width measurements
_session.WIDTHAF_END        // End fabric width
_session.BEGINLINE1-4       // Begin barcodes
_session.ENDLINE1-4         // End barcodes
_session.P_2BEGINLINE1-4    // 2nd begin barcodes
_session.P_2ENDLINE1-4      // 2nd end barcodes
_session.SELVAGELEFT        // Left selvage status
_session.SELVAGERIGHT       // Right selvage status
_session.P_TENSION          // Tension value
_session.LENGTHDETAIL       // Length calculation expression
_session.LENGTHPRINT        // Calculated print length
_session.STATUS             // "I"=InProgress, "F"=Finished, "S"=Suspended, "C"=Cleared
_session.CLEARBY            // Authorization user
_session.CLEARREMARK        // Clear reason
_session.CLEARDATE          // Clear timestamp
_session.SUSPENDDATE        // Suspend timestamp
_session.FINISHINGPROCESS   // "Scouring" disables selvage controls
```

---

## 7. Database Operations

### 7.1 Stored Procedures Used

#### Data Retrieval
| Procedure | Purpose | Called By | Parameters |
|-----------|---------|-----------|------------|
| `CUT_GETFINISHINGDATA` | Load finishing lot details | `LoadCUT_GETFINISHINGDATA()` | `ITEMLOT` |
| `CUT_GETCONDITIONBYITEMCODE` | Load quality specifications | `LoadCUT_GETCONDITIONBYITEMCODE()` | `P_ITMCODE` |
| `Cut_GetMCSuspendData` | Load suspended lot | `Cut_GetMCSuspendData()` | (None - loads all suspended) |
| `GetAuthorizeByProcessID` | Validate clear authorization | `GetAuthorizeByProcessID()` | `PROCESSID=7, USER, PASS` |

#### Data Modification
| Procedure | Purpose | Called By | Operation |
|-----------|---------|-----------|-----------|
| `CUT_INSERTDATA` | Start production process | `CUT_INSERTDATA()` | INSERT |
| `CUT_UPDATEDATA` | End production process | `CUT_UPDATEDATA()` | UPDATE |
| `Suspend` | Suspend current lot | `Suspend()` | UPDATE (STATUS='S') |
| `Clear` | Clear lot with authorization | `GetAuthorizeByProcessID()` | UPDATE (STATUS='C') |

### 7.2 Key Data Structures

#### CUT_GETFINISHINGDATA Response
```csharp
{
    CUSTOMERID       // Customer identifier (drives barcode logic)
    ITEMCODE         // Item code
    BATCHNO          // Batch number (source for barcode generation)
    FINISHINGLOT     // Finishing lot reference
    SND_BARCODE      // Secondary barcode (optional)
    PRODUCTTYPEID    // "1" = Mass, "2" = Test
    BEFORE_WIDTH     // Fabric width before print
    FINISHINGPROCESS // "Scouring" = hide selvage controls
}
```

#### CUT_GETCONDITIONBYITEMCODE Response
```csharp
{
    strWIDTHBARCODE     // Width specification (e.g., "7-9")
    strDISTANTBARCODE   // Barcode distance spec (e.g., "1.0-1.5")
    strDISTANTLINE      // Line distance spec (e.g., "23.5-24.5")
    strDENSITYWARP      // Warp density spec (e.g., "52-56")
    strDENSITYWEFT      // Weft density spec (e.g., "52-56")
    strSPEED            // Speed spec (e.g., "15-20")
    strAFTER            // After width spec (e.g., "161-165")
    SHOWSELVAGE         // "Y" = Show selvage, "N" = Hide
}
```

### 7.3 Transaction Flow

```mermaid
sequenceDiagram
    participant Operator
    participant UI as CutPrintPage
    participant Session as CutPrintSession
    participant DB as Oracle Database

    Operator->>UI: Enter ITEMLOT
    UI->>Session: GetCUT_GETFINISHINGDATA(ITEMLOT)
    Session->>DB: Execute CUT_GETFINISHINGDATA
    DB-->>Session: Return lot details
    Session-->>UI: Lot data + specifications
    UI->>UI: Populate fields, generate barcodes
    UI-->>Operator: Display lot info

    Operator->>UI: Enter quality data
    Operator->>UI: Click Start
    UI->>Session: Set all quality parameters
    Session->>DB: Execute CUT_INSERTDATA
    DB-->>Session: Insert success
    Session-->>UI: Enable End/Clear/Suspend
    UI-->>Operator: Process started

    Operator->>UI: Scan barcodes (4 lines x 2 barcodes)
    UI->>UI: Validate each barcode
    UI-->>Operator: Visual confirmation (checkmarks)

    Operator->>UI: Enter Tension + Length
    Operator->>UI: Click End
    UI->>Session: Set end date + final data
    Session->>DB: Execute CUT_UPDATEDATA
    DB-->>Session: Update success (STATUS='F')
    Session-->>UI: Process complete

    UI->>UI: Call D365_CP()
    Note over UI: D365 Integration Flow
    UI-->>Operator: Navigate to menu
```

---

## 8. D365 Integration

### 8.1 Integration Overview

The D365 integration is triggered **ONLY on End** (`cmdEnd_Click`) and consists of **7 sequential steps**:

```mermaid
flowchart LR
    Start([D365_CP Called]) --> BPO[Step 1: D365_CP_BPO<br/>Production Order]
    BPO --> ISH[Step 2: D365_CP_ISH<br/>Issue Header]
    ISH --> ISL[Step 3: D365_CP_ISL<br/>Issue Lines]
    ISL --> OPH[Step 4: D365_CP_OPH<br/>Operation Header]
    OPH --> OPL[Step 5: D365_CP_OPL<br/>Operation Lines]
    OPL --> OUH[Step 6: D365_CP_OUH<br/>Output Header]
    OUH --> OUL[Step 7: D365_CP_OUL<br/>Output Lines]
    OUL --> Success([Complete])

    style Start fill:#e1f5ff
    style BPO fill:#fff4e1
    style ISH fill:#fff4e1
    style ISL fill:#fff4e1
    style OPH fill:#fff4e1
    style OPL fill:#fff4e1
    style OUH fill:#fff4e1
    style OUL fill:#fff4e1
    style Success fill:#e1ffe1
```

**⚠️ Critical Issue - Failure Handling**: If ANY step fails, entire integration stops with **NO ROLLBACK**. This can result in partial data corruption in D365 (e.g., if BPO succeeds but ISH fails, orphaned production order exists).

### 8.2 D365 Integration Steps

#### Step 1: D365_CP_BPO (Bill of Production Output)
```
Purpose: Create production order in D365
Database: Call D365_CP_BPO(ITEMLOT)
Insert: D365DataService.Insert_ABBPO()
Parameters:
├─ PRODID (from query)
├─ LOTNO
├─ ITEMID
├─ LOADINGTYPE
├─ QTY
├─ UNIT
└─ OPERATION
Output: Sets PRODID, P_LOTNO, P_ITEMID, P_LOADINGTYPE
```

#### Step 2: D365_CP_ISH (Issue Header)
```
Purpose: Create material issue header
Database: Call D365_CP_ISH(ITEMLOT)
Insert: D365DataService.Insert_ABISH()
Parameters:
├─ HEADERID (from query)
├─ PRODID (from step 1)
├─ TOTALRECORD
├─ P_LOTNO
├─ P_ITEMID
└─ P_LOADINGTYPE
Output: Sets HEADERID
```

#### Step 3: D365_CP_ISL (Issue Lines)
```
Purpose: Create material issue line items
Database: Call D365_CP_ISL(ITEMLOT)
Insert: D365DataService.Insert_ABISL() [Loop through lines]
Parameters:
├─ HEADERID (from step 2)
├─ LINENO
├─ ISSUEDATE (formatted as yyyy-MM-dd)
├─ ITEMID
├─ STYLEID
├─ QTY
├─ UNIT
└─ SERIALID
```

#### Step 4: D365_CP_OPH (Operation Header)
```
Purpose: Create operation tracking header
Database: Call D365_CP_OPH(ITEMLOT)
Insert: D365DataService.Insert_ABOPH()
Parameters:
├─ HEADERID (from query - NEW headerID)
├─ PRODID (from step 1)
├─ TOTALRECORD
├─ P_LOTNO
├─ P_ITEMID
└─ P_LOADINGTYPE
Output: Updates HEADERID
```

#### Step 5: D365_CP_OPL (Operation Lines)
```
Purpose: Record operation details (machine, time, qty)
Database: Call D365_CP_OPL(ITEMLOT)
Insert: D365DataService.Insert_ABOPL() [Loop through lines]
Parameters:
├─ HEADERID (from step 4)
├─ LINENO
├─ PROCQTY (processed quantity)
├─ OPRNO (operation number)
├─ OPRID (operator ID)
├─ MACHINENO
├─ STARTDATETIME
└─ ENDDATETIME
```

#### Step 6: D365_CP_OUH (Output Header)
```
Purpose: Create production output header
Database: Call D365_CP_OUH(ITEMLOT)
Insert: D365DataService.Insert_ABOUH()
Parameters:
├─ HEADERID (from query - NEW headerID)
├─ PRODID (from step 1)
├─ TOTALRECORD
├─ P_LOTNO
├─ P_ITEMID
└─ P_LOADINGTYPE
Output: Updates HEADERID
```

#### Step 7: D365_CP_OUL (Output Lines)
```
Purpose: Record finished goods output
Database: Call D365_CP_OUL(ITEMLOT)
Insert: D365DataService.Insert_ABOUL() [Loop through lines]
Parameters:
├─ HEADERID (from step 6)
├─ LINENO
├─ OUTPUTDATE (formatted as yyyy-MM-dd)
├─ ITEMID
├─ QTY
├─ UNIT
├─ GROSSLENGTH
├─ NETLENGTH
├─ GROSSWEIGHT
├─ NETWEIGHT
├─ PALLETNO
├─ GRADE
├─ SERIALID
├─ LOADINGTYPE
├─ FINISH (int? converted from nullable)
├─ MOVEMENTTRANS
├─ WAREHOUSE
└─ LOCATION
```

### 8.3 D365 Integration Flowchart

```mermaid
flowchart TD
    Start([D365_CP Called]) --> CheckLot{ITEMLOT<br/>not empty?}
    CheckLot -->|No| ErrLot[Error: Item Lot is null]
    ErrLot --> End([End])

    CheckLot -->|Yes| BPO[Step 1: D365_CP_BPO]
    BPO --> CheckProdID{PRODID<br/>not null?}
    CheckProdID -->|No| ErrProdID[Error: PRODID is null]
    ErrProdID --> End

    CheckProdID -->|Yes| ISH[Step 2: D365_CP_ISH]
    ISH --> CheckHeader1{HEADERID<br/>not null?}
    CheckHeader1 -->|No| ErrHeader1[Error: HEADERID is null]
    ErrHeader1 --> End

    CheckHeader1 -->|Yes| ISL[Step 3: D365_CP_ISL]
    ISL --> OPH[Step 4: D365_CP_OPH]
    OPH --> CheckHeader2{HEADERID<br/>not null?}
    CheckHeader2 -->|No| ErrHeader2[Error: HEADERID is null]
    ErrHeader2 --> End

    CheckHeader2 -->|Yes| OPL[Step 5: D365_CP_OPL]
    OPL --> OUH[Step 6: D365_CP_OUH]
    OUH --> CheckHeader3{HEADERID<br/>not null?}
    CheckHeader3 -->|No| ErrHeader3[Error: HEADERID is null]
    ErrHeader3 --> End

    CheckHeader3 -->|Yes| OUL[Step 7: D365_CP_OUL]
    OUL --> Success[Success: Send D365 complete]
    Success --> End

    style Start fill:#e1f5ff
    style Success fill:#e1ffe1
    style ErrLot fill:#ffe1e1
    style ErrProdID fill:#ffe1e1
    style ErrHeader1 fill:#ffe1e1
    style ErrHeader2 fill:#ffe1e1
    style ErrHeader3 fill:#ffe1e1
    style End fill:#e1f5ff
```

### 8.4 D365 Error Handling

```csharp
// Each D365 step returns bool
// If any step returns false, entire flow stops

if (D365_CP_BPO() == true)           // Step 1
{
    if (PRODID != null)
    {
        if (D365_CP_ISH(PRODID) == true)    // Step 2
        {
            if (HEADERID != null)
            {
                if (D365_CP_ISL(HEADERID) == true)  // Step 3
                {
                    if (D365_CP_OPH(PRODID) == true)  // Step 4
                    {
                        // ... continues through step 7
                    }
                }
            }
        }
    }
}
```

**Error Messages**:
- Specific error from database operation (shown via ShowMessageBox)
- Generic info messages for null ID tracking:
  - "PRODID is null"
  - "HEADERID is null"
  - "Item Lot is null"
  - "D365_CP_XXX Row = 0" (no data returned from query)

---

## 9. Mermaid Diagrams

### 9.1 Main Workflow Flowchart

```mermaid
flowchart TD
    Start([Page Load]) --> Init[Initialize Controls]
    Init --> LoadStatus[Load Left/Right Status<br/>ComboBox: OK, NG]
    LoadStatus --> DisableButtons[Disable: Start, Preview,<br/>End, Clear, Suspend]
    DisableButtons --> CheckSuspend[Check for Suspended Lot]

    CheckSuspend --> SuspendFound{Suspended<br/>Lot Found?}
    SuspendFound -->|Yes| LoadSuspended[Load Suspended Data]
    SuspendFound -->|No| WaitLot[Wait for Lot Entry]

    LoadSuspended --> EnableStart[Enable Start Button]
    WaitLot --> EnterLot[Operator Enters ITEMLOT]

    EnterLot --> ValidateLot{Lot Valid?}
    ValidateLot -->|No| ShowError[Show Error Message]
    ShowError --> WaitLot

    ValidateLot -->|Yes| LoadData[Load Finishing Data]
    LoadData --> LoadSpecs[Load Specifications by ITEMCODE]
    LoadSpecs --> GenBarcodes[Generate Batch Numbers]
    GenBarcodes --> CheckCustomer{Customer<br/>ID = 08?}

    CheckCustomer -->|Yes| SpecialBarcode[Apply Special Barcode Logic<br/>P + 9 chars / H + ITEMLOT]
    CheckCustomer -->|No| NormalBarcode[Use BATCHNO as-is]

    SpecialBarcode --> EnableStart
    NormalBarcode --> EnableStart
    EnableStart --> WaitStart[Wait for Start Click]

    WaitStart --> ClickStart[Operator Clicks Start]
    ClickStart --> RecordStart[Record Start Time]
    RecordStart --> InsertData[CUT_INSERTDATA]
    InsertData --> InsertSuccess{Insert<br/>Success?}

    InsertSuccess -->|No| ErrorInsert[Show Error]
    ErrorInsert --> WaitStart

    InsertSuccess -->|Yes| UpdateButtons1[Enable: End, Clear, Suspend<br/>Disable: Start, Back]
    UpdateButtons1 --> DataEntry[Operator Enters Quality Data]

    DataEntry --> ScanBarcodes[Scan Barcodes 4 Lines x 2]
    ScanBarcodes --> ValidateBarcodes{All Barcodes<br/>Valid?}
    ValidateBarcodes -->|No| ShowBarcodeError[Show Validation Error]
    ShowBarcodeError --> ScanBarcodes

    ValidateBarcodes -->|Yes| WaitAction{User Action?}

    WaitAction -->|End| CheckTension{Tension<br/>Entered?}
    CheckTension -->|No| TensionError[Error: Tension isn't Null]
    TensionError --> DataEntry

    CheckTension -->|Yes| CheckLength{Length<br/>Entered?}
    CheckLength -->|No| LengthError[Error: Cutting Length isn't Null]
    LengthError --> DataEntry

    CheckLength -->|Yes| RecordEnd[Record End Time]
    RecordEnd --> UpdateData[CUT_UPDATEDATA]
    UpdateData --> UpdateSuccess{Update<br/>Success?}

    UpdateSuccess -->|No| ErrorUpdate[Show Error]
    ErrorUpdate --> DataEntry

    UpdateSuccess -->|Yes| D365Sync[D365_CP Integration]
    D365Sync --> D365Success{D365<br/>Success?}

    D365Success -->|No| D365Error[Show D365 Error]
    D365Success -->|Yes| Complete[Process Complete]
    Complete --> NavBack1[Navigate Back to Menu]
    NavBack1 --> End([End])

    WaitAction -->|Suspend| ConfirmSuspend{Confirm<br/>Suspend?}
    ConfirmSuspend -->|No| DataEntry
    ConfirmSuspend -->|Yes| SaveSuspend[Save Suspend Data<br/>STATUS='S']
    SaveSuspend --> NavBack2[Navigate Back to Menu]
    NavBack2 --> End

    WaitAction -->|Clear| ShowAuth[Show Authorization Dialog]
    ShowAuth --> ValidateAuth{Valid<br/>Credentials?}
    ValidateAuth -->|No| AuthError[Show Error]
    AuthError --> DataEntry
    ValidateAuth -->|Yes| ClearLot[Clear Lot<br/>STATUS='C']
    ClearLot --> NavBack3[Navigate Back to Menu]
    NavBack3 --> End

    D365Error --> NavBack1

    style Start fill:#e1f5ff
    style ShowError fill:#ffe1e1
    style ErrorInsert fill:#ffe1e1
    style ErrorUpdate fill:#ffe1e1
    style TensionError fill:#ffe1e1
    style LengthError fill:#ffe1e1
    style D365Error fill:#ffe1e1
    style AuthError fill:#ffe1e1
    style Complete fill:#e1ffe1
    style End fill:#e1f5ff
```

### 9.2 Barcode Validation Sequence Diagram

```mermaid
sequenceDiagram
    actor Operator
    participant UI as CutPrintPage
    participant Field as Barcode TextBox
    participant Validation as Validation Logic
    participant Visual as UI Feedback

    Operator->>Field: Scan/Enter Barcode
    Field->>Field: LostFocus Event

    Field->>Validation: Check if empty

    alt Barcode is empty
        Validation->>Visual: Hide checkmark
        Validation->>Visual: Show textbox
        Validation-->>UI: Clear validation state
    else Barcode entered
        Validation->>Validation: Trim whitespace
        Validation->>Validation: Compare with expected batch number

        alt Barcode matches
            Validation->>Visual: Hide textbox
            Validation->>Visual: Show checkmark image
            Validation->>UI: Set validation state = "Yes"
            Visual-->>Operator: Visual confirmation
        else Barcode does not match
            Validation->>UI: Show MessageBox: "Barcode is not correct. Do you want to Confirm again?"
            UI->>Operator: Display dialog

            alt User clicks Yes (Retry)
                Operator->>UI: Click Yes
                UI->>Field: Clear text
                UI->>Field: Focus and SelectAll
                UI->>Visual: Hide checkmark
                UI->>Visual: Show textbox
                UI->>UI: Clear validation state
                Field-->>Operator: Ready for re-entry
            else User clicks No (Accept)
                Operator->>UI: Click No
                UI->>UI: Set validation state = scanned value
                UI-->>Operator: Continue with warning
            end
        end
    end

    Note over Operator,Visual: Process repeats for all 16 barcode fields<br/>(4 lines × 2 barcodes × 2 rolls = 16)
```

### 9.3 UI State Transition Diagram

```mermaid
stateDiagram-v2
    [*] --> PageLoad

    state PageLoad {
        [*] --> Initializing
        Initializing: Disable all action buttons
        Initializing: Load status comboboxes
        Initializing: Check suspended lots
    }

    state WaitingForLot {
        [*] --> Idle
        Idle: Start button disabled
        Idle: Back button enabled
        Idle: Focus on ITEMLOT field
    }

    state LotLoaded {
        [*] --> SpecsLoaded
        SpecsLoaded: Specifications displayed
        SpecsLoaded: Batch numbers generated
        SpecsLoaded: Start button enabled
        SpecsLoaded: Quality fields enabled
    }

    state ProcessActive {
        [*] --> Running
        Running: Start time recorded
        Running: End button enabled
        Running: Clear button enabled
        Running: Suspend button enabled
        Running: Back button disabled
        Running: Data entry in progress
    }

    state SuspendedState {
        [*] --> Paused
        Paused: STATUS = S
        Paused: SUSPENDDATE recorded
        Paused: Can resume on next load
    }

    state CompletedState {
        [*] --> Finished
        Finished: STATUS = F
        Finished: End time recorded
        Finished: D365 integration complete
        Finished: Ready to navigate back
    }

    state ClearedState {
        [*] --> Cancelled
        Cancelled: STATUS = C
        Cancelled: Authorization recorded
        Cancelled: CLEARBY and CLEARDATE set
    }

    PageLoad --> WaitingForLot: Initialize complete
    WaitingForLot --> LotLoaded: Valid lot entered
    WaitingForLot --> WaitingForLot: Invalid lot (retry)
    LotLoaded --> ProcessActive: Click Start
    ProcessActive --> SuspendedState: Click Suspend
    ProcessActive --> CompletedState: Click End (with validation)
    ProcessActive --> ClearedState: Click Clear + Authorize
    SuspendedState --> LotLoaded: Resume on next page load
    CompletedState --> [*]: Navigate back
    ClearedState --> [*]: Navigate back
```

### 9.4 Validation Sequence Diagram

```mermaid
sequenceDiagram
    actor Operator
    participant UI as UI Controls
    participant Logic as Business Logic
    participant Session as CutPrintSession
    participant DB as Database

    Operator->>UI: Enter ITEMLOT
    UI->>Logic: txtITEMLOT_LostFocus
    Logic->>Logic: Check if not empty

    Logic->>Session: GetCUT_GETFINISHINGDATA(ITEMLOT)
    Session->>DB: Execute SP: CUT_GETFINISHINGDATA
    DB-->>Session: Return lot data

    alt Lot found and valid
        Session-->>Logic: Return lot details
        Logic->>Logic: Validate CUSTOMERID not null
        Logic->>Session: GetCUT_GETCONDITIONBYITEMCODE(ITEMCODE)
        Session->>DB: Execute SP: CUT_GETCONDITIONBYITEMCODE
        DB-->>Session: Return specifications
        Session-->>Logic: Return specs

        Logic->>UI: Populate all specification fields
        Logic->>Logic: Generate batch numbers

        alt Customer ID = 08
            Logic->>Logic: Apply special barcode logic
            Note over Logic: Primary: P + 9 chars<br/>Secondary: H + ITEMLOT
        else Other customers
            Logic->>Logic: Use BATCHNO as-is
        end

        Logic->>UI: Set all batch number fields
        Logic->>UI: Enable Start button
        UI-->>Operator: Ready for data entry

    else Lot not found or invalid
        Session-->>Logic: Return empty or invalid data
        Logic->>UI: Show error message
        Logic->>UI: Clear all fields
        Logic->>UI: Focus on ITEMLOT
        UI-->>Operator: Retry entry
    end

    Note over Operator,DB: After Start clicked

    Operator->>UI: Enter quality parameters
    Operator->>UI: Click Start
    UI->>Logic: cmdStart_Click
    Logic->>Logic: Validate ITEMLOT not empty
    Logic->>Logic: Validate Operator not empty

    alt Validation passed
        Logic->>Logic: Record start time
        Logic->>Session: Set all quality parameters
        Session->>DB: Execute SP: CUT_INSERTDATA
        DB-->>Session: Insert success
        Session-->>Logic: Return true
        Logic->>UI: Enable End, Clear, Suspend
        Logic->>UI: Disable Start, Back
        UI-->>Operator: Process started
    else Validation failed
        Logic->>UI: Show validation error
        UI-->>Operator: Fix issues
    end

    Note over Operator,DB: After End clicked

    Operator->>UI: Enter Tension + Length
    Operator->>UI: Click End
    UI->>Logic: cmdEnd_Click
    Logic->>Logic: Validate Tension not empty
    Logic->>Logic: Validate Length not empty

    alt Validation passed
        Logic->>Logic: Record end time
        Logic->>Session: Set end date + final data
        Logic->>Session: Set STATUS = 'F'
        Session->>DB: Execute SP: CUT_UPDATEDATA
        DB-->>Session: Update success
        Session-->>Logic: Return true

        Logic->>Logic: Call D365_CP()

        loop For each D365 step (BPO→ISH→ISL→OPH→OPL→OUH→OUL)
            Logic->>Session: D365 step query
            Session->>DB: Execute D365 query
            DB-->>Session: Return D365 data
            Session->>DB: Insert to D365 staging table
            DB-->>Session: Insert success
        end

        Logic-->>UI: D365 complete
        UI->>UI: Navigate back to menu
    else Validation failed
        Logic->>UI: Show validation error
        UI-->>Operator: Fix issues
    end
```

### 9.5 D365 Integration Sequence

```mermaid
sequenceDiagram
    participant Logic as Business Logic
    participant D365Svc as D365DataService
    participant OracleDB as Oracle Database
    participant D365Staging as D365 Staging Tables

    Logic->>Logic: D365_CP() called
    Logic->>Logic: Validate ITEMLOT not empty

    Note over Logic,D365Staging: Step 1: BPO (Bill of Production Output)
    Logic->>D365Svc: D365_CP_BPO(ITEMLOT)
    D365Svc->>OracleDB: Query production order data
    OracleDB-->>D365Svc: Return PRODID, LOTNO, ITEMID, LOADINGTYPE
    D365Svc->>D365Staging: Insert_ABBPO(data)
    D365Staging-->>D365Svc: Insert success
    D365Svc-->>Logic: Return true + PRODID

    Note over Logic,D365Staging: Step 2: ISH (Issue Header)
    Logic->>D365Svc: D365_CP_ISH(ITEMLOT)
    D365Svc->>OracleDB: Query issue header
    OracleDB-->>D365Svc: Return HEADERID, TOTALRECORD
    D365Svc->>D365Staging: Insert_ABISH(HEADERID, PRODID, ...)
    D365Staging-->>D365Svc: Insert success
    D365Svc-->>Logic: Return true + HEADERID

    Note over Logic,D365Staging: Step 3: ISL (Issue Lines)
    Logic->>D365Svc: D365_CP_ISL(ITEMLOT)
    D365Svc->>OracleDB: Query issue lines
    OracleDB-->>D365Svc: Return line items array

    loop For each issue line
        D365Svc->>D365Staging: Insert_ABISL(HEADERID, line data)
        D365Staging-->>D365Svc: Insert success
    end
    D365Svc-->>Logic: Return true

    Note over Logic,D365Staging: Step 4: OPH (Operation Header)
    Logic->>D365Svc: D365_CP_OPH(ITEMLOT)
    D365Svc->>OracleDB: Query operation header
    OracleDB-->>D365Svc: Return new HEADERID
    D365Svc->>D365Staging: Insert_ABOPH(HEADERID, PRODID, ...)
    D365Staging-->>D365Svc: Insert success
    D365Svc-->>Logic: Return true + new HEADERID

    Note over Logic,D365Staging: Step 5: OPL (Operation Lines)
    Logic->>D365Svc: D365_CP_OPL(ITEMLOT)
    D365Svc->>OracleDB: Query operation details
    OracleDB-->>D365Svc: Return operation lines

    loop For each operation line
        D365Svc->>D365Staging: Insert_ABOPL(HEADERID, operation data)
        D365Staging-->>D365Svc: Insert success
    end
    D365Svc-->>Logic: Return true

    Note over Logic,D365Staging: Step 6: OUH (Output Header)
    Logic->>D365Svc: D365_CP_OUH(ITEMLOT)
    D365Svc->>OracleDB: Query output header
    OracleDB-->>D365Svc: Return new HEADERID
    D365Svc->>D365Staging: Insert_ABOUH(HEADERID, PRODID, ...)
    D365Staging-->>D365Svc: Insert success
    D365Svc-->>Logic: Return true + new HEADERID

    Note over Logic,D365Staging: Step 7: OUL (Output Lines)
    Logic->>D365Svc: D365_CP_OUL(ITEMLOT)
    D365Svc->>OracleDB: Query output lines
    OracleDB-->>D365Svc: Return finished goods data

    loop For each output line
        D365Svc->>D365Staging: Insert_ABOUL(HEADERID, output data)
        D365Staging-->>D365Svc: Insert success
    end
    D365Svc-->>Logic: Return true

    Logic->>Logic: Show success message: "Send D365 complete"
    Logic-->>Logic: Return to caller
```

---

## 10. Critical Findings

### 10.1 Code Complexity Metrics

| Metric | Value | Assessment |
|--------|-------|------------|
| **Total Lines** | 6,525 | **CRITICAL** - Largest file in project |
| **Cyclomatic Complexity** | Very High | Extensive nesting, multiple decision points |
| **Method Count** | 100+ | Too many methods in single file |
| **Duplicate Code** | High | Barcode validation repeated 16 times |
| **D365 Integration** | 7 sequential steps | Single point of failure, no rollback |

### 10.2 Bugs and Issues

#### CRITICAL ISSUES

**1. D365 Integration Failure - No Rollback** (Lines 5981-6450)
```
Severity: CRITICAL
Impact: Partial data corruption in D365

If any D365 step fails after BPO succeeds:
├─ Production order created in D365 (ABBPO table)
├─ But issue/operation/output data missing
└─ Manual intervention required to fix D365 state

Recommendation: Implement transaction wrapper or compensating transactions
```

**2. Hardcoded Machine Number** (Line 127)
```xaml
<TextBox Name="txtPrintNo" Text="256" ... />

Severity: HIGH
Impact: Wrong machine tracking if used on different equipment

Recommendation: Load from machine configuration or user selection
```

**3. Decimal Parsing Without TryCatch** (Lines 4594-4644, 4786-4836)
```csharp
if (txtWIDTH1.Text != "")
    WIDTH1 = decimal.Parse(txtWIDTH1.Text);  // No try-catch

Severity: HIGH
Impact: Application crash if invalid decimal format

Recommendation: Use decimal.TryParse() with error handling
```

#### MAJOR ISSUES

**4. Customer ID "08" Hardcoded Logic** (Lines 4136-4303)
```csharp
if (items[0].CUSTOMERID == "08")
{
    // Special barcode manipulation
    batchNo = "P" + batch.Substring(0, 9);
    batchNo2nd = "H" + ITEMLOT.Substring(0, ITEMLOT.Length - 1);
}

Severity: MAJOR
Impact: Business logic tied to hardcoded customer ID

Recommendation: Move to configuration table or customer profile
```

**5. Massive Code Duplication - Barcode Validation** (Lines 1067-3750)
```
Similar logic repeated 16 times:
- txtBEGINLINE1-4_LostFocus
- txt2ndBEGINLINE1-4_LostFocus
- txtENDLINE1-4_LostFocus
- txt2ndENDLINE1-4_LostFocus

Severity: MAJOR
Impact: Maintenance nightmare, bug fixes must be applied 16 times

Recommendation: Extract to shared validation method with parameters
```

**6. String Concatenation for Barcode Generation** (Lines 4250-4300)
```csharp
if (batch.Length >= 9)
{
    batchNo = "P" + batch.Substring(0, 9);  // No length validation
}

Severity: MAJOR
Impact: IndexOutOfRangeException if batch length < 9

Recommendation: Validate length before substring
```

#### MODERATE ISSUES

**7. Magic Strings Throughout** (Lines 4367-4382, 4917, 5135)
```csharp
if (items[0].FINISHINGPROCESS == "Scouring")  // Magic string
_session.STATUS = "F";  // Magic string
_session.STATUS = "S";  // Magic string
_session.STATUS = "C";  // Magic string

Severity: MODERATE
Impact: Typos cause logic errors, hard to maintain

Recommendation: Use enums or constants
```

**8. Complex Navigation Flow** (Multiple KeyDown handlers)
```
54 KeyDown event handlers for tab order navigation
Lines 320-1063 - Manual focus management

Severity: MODERATE
Impact: Brittle, breaks if UI layout changes

Recommendation: Use TabIndex property or focus manager
```

**9. Authorization Process ID Hardcoded** (Line 275)
```csharp
string processId = "7";  // Hardcoded

Severity: MODERATE
Impact: Unclear what Process ID 7 represents

Recommendation: Use named constant: PROCESSID_CUT_PRINT_CLEAR = 7
```

**10. No Input Sanitization** (Lines 4544-4742)
```csharp
_session.REMARK = txtREMARK.Text;  // No sanitization
_session.LENGTHDETAIL = txtLengthDetail.Text;  // User input directly

Severity: MODERATE
Impact: Potential SQL injection if not handled in stored procedure

Recommendation: Sanitize/validate all user inputs
```

### 10.3 Performance Concerns

**1. Sequential D365 Calls** (Lines 5989-6038)
```
D365_CP() executes 7 sequential database calls
Total time: 7 × (network latency + DB query + insert)

Recommendation: Consider batch operations or async processing
```

**2. Multiple ComboBox Bindings** (Lines 3915-3935)
```csharp
cbStatusLeft.ItemsSource = new string[] { "OK", "NG" };
cbStatusRight.ItemsSource = new string[] { "OK", "NG" };

Minor issue: Created every page load
Recommendation: Create once in constructor
```

### 10.4 Security Concerns

**1. Authorization Bypass Potential** (Lines 5183-5220)
```csharp
if (_session.GetAuthorizeByProcessID(PROCESSID, USER, PASS) == true)
{
    // Clear lot
}

Concern: Password transmitted to database (unclear if hashed)
Recommendation: Verify password hashing in stored procedure
```

**2. No Audit Trail for Barcode Mismatches** (Lines 1150-1164)
```csharp
else
{
    BEGINLINE1 = beging;  // Wrong barcode accepted
    txtBEGINLINE1.Text = beging;
}

Concern: Operator can override barcode validation without logging
Recommendation: Log all overrides to audit table
```

### 10.5 Maintainability Issues

**1. File Size** (6,525 lines)
```
This single file is:
- 3× larger than typical page
- Contains business logic + UI logic + D365 integration + validation

Recommendation: Split into:
- CutPrintPage.xaml.cs (UI only)
- CutPrintBusinessLogic.cs (validation, workflow)
- CutPrintD365Integration.cs (D365 specific)
- BarcodeValidator.cs (shared barcode logic)
```

**2. Session God Object** (Lines 104, 4578-4716)
```csharp
_session has 50+ properties

Recommendation: Use DTOs for data transfer instead of session state
```

**3. Commented Code** (Lines 4138-4233, 1075-1136)
```
Extensive blocks of commented-out code

Recommendation: Remove dead code, use version control instead
```

### 10.6 Recommended Refactoring Priority

| Priority | Issue | Estimated Effort | Risk |
|----------|-------|------------------|------|
| 1 | D365 transaction safety | High (2 weeks) | Critical |
| 2 | Extract barcode validation | Medium (1 week) | Medium |
| 3 | Replace decimal.Parse | Low (2 days) | High |
| 4 | File size reduction | High (3 weeks) | Low |
| 5 | Remove hardcoded values | Medium (1 week) | Medium |
| 6 | Add input sanitization | Low (3 days) | Medium |
| 7 | Implement proper enums | Low (2 days) | Low |
| 8 | Remove dead code | Low (1 day) | Low |

---

## Document Metadata

**Generated**: 2025-10-09
**Analysis Version**: 1.0
**Files Analyzed**:
- CutPrintPage.xaml (1,320 lines)
- CutPrintPage.xaml.cs (6,525 lines)

**Total Lines Analyzed**: 7,845 lines

**Key Statistics**:
- Quality Parameters: 11 categories
- Barcode Fields: 16 (4 lines × 2 barcodes × 2 rolls)
- Button Controls: 6 action buttons
- D365 Integration Steps: 7 sequential operations
- Database Procedures: 12+
- State Variables: 30+
- Event Handlers: 80+

---

**END OF DOCUMENT**
