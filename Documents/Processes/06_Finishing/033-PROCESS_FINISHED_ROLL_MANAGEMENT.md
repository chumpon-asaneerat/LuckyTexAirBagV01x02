# Process: Finished Roll Management

**Process ID**: FN-002
**Module**: 06 - Finishing
**Priority**: P3 (Production Module)
**Created**: 2025-10-06

---

## 1. Process Overview

### Purpose
Manage finished fabric roll inventory from creation during finishing processes to transfer to Inspection department. Track finished roll location, status, process parameters, and maintain traceability to source grey rolls and original materials.

### Scope
- View and search finished roll inventory
- Transfer rolls to Inspection department
- Track roll quality parameters (process deviations, coating quality)
- Maintain traceability to grey rolls and source materials
- Handle roll rework/scrap (if quality issues detected)
- Generate finished roll reports

### Module(s) Involved
- **Primary**: M06 - Finishing
- **Upstream**: Finishing Process (roll creation)
- **Downstream**: M08 - Inspection (finished roll inspection)

---

## 2. UI Files Inventory

### XAML Files
| File Path | Description | Purpose |
|-----------|-------------|---------|
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishedRollInventoryPage.xaml` | Inventory dashboard | View finished rolls |
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishedRollTransferPage.xaml` | Transfer interface | Transfer to Inspection |
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishedRollTraceabilityPage.xaml` | Traceability viewer | View process history |
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishingMenuPage.xaml` | Module menu | Navigation |

### Code-Behind Files
| File Path | Description |
|-----------|-------------|
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishedRollInventoryPage.xaml.cs` | Inventory logic |
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishedRollTransferPage.xaml.cs` | Transfer logic |
| `LuckyTex.AirBag.Pages/Pages/06 - Finishing/FinishedRollTraceabilityPage.xaml.cs` | Traceability display |

### Service Files
| File Path | Description |
|-----------|-------------|
| *(To be created)* `LuckyTex.AirBag.Core/Repositories/FinishingRepository.cs` | Repository |
| *(To be created)* `LuckyTex.AirBag.Core/Services/FinishingService.cs` | Service layer |
| *(To be created)* `LuckyTex.AirBag.Core/Validators/FinishedRollTransferValidator.cs` | FluentValidation |

---

## 3. UI Layout Description

### FinishedRollInventoryPage.xaml

**Screen Title**: "Finished Fabric Roll Inventory"

**Search Section**:
- Finished roll barcode textbox
- Date range filter (From/To)
- Process type filter (All, Coating, Heat-Setting, Both)
- Status filter (All, Available, In Inspection, Approved, Rejected, Scrapped)
- `cmdSearch` - Execute search

**Inventory DataGrid**:
- Columns: Barcode, Product, Length (m), Process Type, Process Date, Status, Deviation Count, Location, Age (days)
- Row click: Show details

**Summary Cards**:
- Total finished rolls available
- Pending inspection
- Approved rolls
- Rolls with process deviations

**Action Buttons**:
- `cmdTransfer` - Transfer to Inspection
- `cmdViewProcess` - View process details
- `cmdViewTrace` - View traceability
- `cmdPrint` - Print roll label
- `cmdExport` - Export to Excel

### FinishedRollTransferPage.xaml

**Screen Title**: "Finished Roll Transfer to Inspection"

**Transfer Section**:
- Finished roll barcode scan textbox
- Display roll details (read-only):
  - Product, Length
  - Process parameters (temperature, speed, coating)
  - Deviation count
- Inspection department/location textbox
- Transfer remarks
- Transfer list DataGrid
- `cmdAdd` - Add to transfer list
- `cmdRemove` - Remove
- `cmdSave` - Confirm transfer
- `cmdPrint` - Print transfer document

### FinishedRollTraceabilityPage.xaml

**Screen Title**: "Finished Roll Traceability & Process History"

**Roll Information Section**:
- Finished roll barcode input
- Display finished roll details

**Process History Section** (read-only):
- Process type (Coating/Heat-Setting/Both)
- Machine ID, Operator, Date
- **Target Parameters**:
  - Temperature (°C)
  - Speed (m/min)
  - Coating thickness (mm)
- **Actual Average Parameters** (from parameter log)
- **Process Deviations** (DataGrid):
  - Columns: Timestamp, Parameter Name, Target, Actual, Deviation, Status

**Source Material Section**:
- Grey roll barcode (link to weaving)
- Click grey roll → Navigate to weaving traceability
  - Beam barcode
  - Weft yarn lot
  - Yarn suppliers

**Downstream Usage Section** (if consumed):
- Inspection result (grade, defects)
- Cut pieces (if further processed)

---

## 4. Component Architecture Diagram

```mermaid
graph TD
    UI1[FinishedRollInventoryPage.xaml] --> CB1[CodeBehind]
    UI2[FinishedRollTransferPage.xaml] --> CB2[CodeBehind]
    UI3[FinishedRollTraceabilityPage.xaml] --> CB3[CodeBehind]

    CB1 --> SVC[IFinishingService]
    CB2 --> SVC
    CB3 --> SVC

    SVC --> VAL[FinishedRollTransferValidator]
    SVC --> REPO[IFinishingRepository]

    REPO --> DB[(Oracle Database)]

    DB --> SP1[sp_LuckyTex_FinishedRoll_GetInventory]
    DB --> SP2[sp_LuckyTex_FinishedRoll_GetByBarcode]
    DB --> SP3[sp_LuckyTex_FinishedRoll_Transfer]
    DB --> SP4[sp_LuckyTex_FinishedRoll_UpdateStatus]
    DB --> SP5[sp_LuckyTex_FinishedRoll_GetTraceability]
    DB --> SP6[sp_LuckyTex_FinishedRoll_GetProcessHistory]

    SVC --> PRINT[Printer]

    style UI1 fill:#e1f5ff
    style UI2 fill:#e1f5ff
    style UI3 fill:#e1f5ff
    style SVC fill:#e1ffe1
    style REPO fill:#fff4e1
    style DB fill:#ffe1e1
```

---

## 5. Workflow Diagram

```mermaid
graph TD
    START[Start: Finished Roll Management] --> OPTION{Action?}

    OPTION -->|View Inventory| SEARCH[Search Finished Rolls]
    SEARCH --> DISPLAY[Display Results with Process Info]
    DISPLAY --> END[End]

    OPTION -->|Transfer to Inspection| SCAN[Scan Finished Roll Barcode]
    SCAN --> VALIDATE{Roll Status<br/>= Available?}
    VALIDATE -->|No| ERROR1[Error: Roll Already Transferred]
    ERROR1 --> END

    VALIDATE -->|Yes| CHECK_DEV{Check Deviation<br/>Count}
    CHECK_DEV -->|High Deviations| WARN[Warning: High Deviations<br/>Review Process Parameters]
    CHECK_DEV -->|OK| ADD_LIST[Add to Transfer List]
    WARN --> CONFIRM{Proceed?}
    CONFIRM -->|Yes| ADD_LIST
    CONFIRM -->|No| END

    ADD_LIST --> MORE{Transfer More?}
    MORE -->|Yes| SCAN
    MORE -->|No| SAVE_TRANSFER[Save Transfer Records]

    SAVE_TRANSFER --> UPDATE_STATUS[Update Roll Status = In Inspection]
    UPDATE_STATUS --> PRINT[Print Transfer Document]
    PRINT --> SUCCESS[Success]
    SUCCESS --> END

    OPTION -->|View Traceability| SCAN_TRACE[Scan Finished Roll Barcode]
    SCAN_TRACE --> LOAD_PROCESS[Load Process History:<br/>Parameters, Deviations]
    LOAD_PROCESS --> LOAD_SOURCE[Load Source Materials:<br/>Grey Roll → Beam → Yarn]
    LOAD_SOURCE --> DISPLAY_FULL[Display Full Chain]
    DISPLAY_FULL --> END

    style START fill:#e1f5ff
    style SUCCESS fill:#e1ffe1
    style END fill:#e1f5ff
    style ERROR1 fill:#ffe1e1
    style WARN fill:#fff4e1
```

---

## 6. Business Logic Sequence Diagram

```mermaid
sequenceDiagram
    participant Operator
    participant UI as FinishedRollTransferPage
    participant BL as FinishingService
    participant VAL as FinishedRollTransferValidator
    participant REPO as FinishingRepository
    participant DB as Oracle Database
    participant Printer

    Operator->>UI: Scan finished roll barcode
    UI->>BL: GetFinishedRollDetails(barcode)
    BL->>REPO: GetFinishedRollByBarcode(barcode)
    REPO->>DB: sp_LuckyTex_FinishedRoll_GetByBarcode

    alt Roll exists
        DB-->>REPO: Finished roll details + status + deviation count
        REPO-->>BL: Finished roll entity
        BL-->>UI: Display roll info

        alt Deviation count > 5
            UI->>UI: Display warning: High process deviations detected
            Operator->>UI: Review process parameters
            UI->>BL: GetProcessHistory(barcode)
            BL->>REPO: GetProcessHistory(barcode)
            REPO->>DB: sp_LuckyTex_FinishedRoll_GetProcessHistory
            Note over DB: SELECT parameter logs, deviations<br/>FROM tblFinishingParameterLog<br/>JOIN tblFinishingDeviation
            DB-->>REPO: Process history
            REPO-->>BL: Parameters + deviations
            BL-->>UI: Display deviation details

            Operator->>UI: Confirm proceed with transfer
        end

        UI->>BL: ValidateTransfer(barcode)
        BL->>VAL: Validate transfer request
        VAL->>VAL: Check roll status = Available
        VAL->>VAL: Check not already transferred

        alt Validation passed
            VAL-->>BL: OK
            BL-->>UI: Add to transfer list
            Operator->>UI: Click Save

            UI->>BL: TransferFinishedRolls(transferRequest)
            BL->>REPO: BeginTransaction()

            loop For each roll
                BL->>REPO: InsertTransferRecord(transfer)
                REPO->>DB: sp_LuckyTex_FinishedRoll_Transfer
                Note over DB: INSERT INTO tblFinishedRollTransfer<br/>(rollBarcode, destination='Inspection',<br/>operatorId, date, remarks)
                DB-->>REPO: Transfer ID

                BL->>REPO: UpdateRollStatus(barcode, 'In Inspection')
                REPO->>DB: sp_LuckyTex_FinishedRoll_UpdateStatus
                DB-->>REPO: Status updated
            end

            BL->>REPO: CommitTransaction()
            BL-->>UI: Transfer complete

            UI->>Printer: Print transfer document with:<br/>- Roll barcodes<br/>- Destination<br/>- Process parameters<br/>- Deviation summary
            Printer-->>UI: Document printed

            UI->>UI: Success, clear form

        else Validation failed
            VAL-->>BL: Error: Roll not available
            BL-->>UI: Show error
        end

    else Roll not found
        DB-->>REPO: No data
        REPO-->>BL: Error
        BL-->>UI: Error: Invalid barcode
    end

    Note over Operator,Printer: View Traceability

    Operator->>UI: Navigate to Traceability Page
    Operator->>UI: Scan finished roll barcode

    UI->>BL: GetFullTraceability(finishedBarcode)
    BL->>REPO: GetFinishedRollTraceability(finishedBarcode)
    REPO->>DB: sp_LuckyTex_FinishedRoll_GetTraceability

    Note over DB: SELECT finished roll<br/>JOIN grey roll<br/>JOIN weaving production<br/>JOIN beam, weft<br/>JOIN beam traceability (yarn lots)

    DB-->>REPO: Full traceability chain:
    Note over REPO: - Finished roll details<br/>- Process parameters & deviations<br/>- Grey roll barcode<br/>- Beam barcode<br/>- Weft yarn lot<br/>- Yarn source lots from warping

    REPO-->>BL: Traceability data
    BL-->>UI: Display full chain

    UI->>UI: Render traceability tree:<br/>Finished Roll → Grey Roll → Beam → Yarn Lots<br/>                  ↓<br/>               Weft Lot → Supplier
```

---

## 7. Data Flow

### Input Data

| Data Element | Source | Format | Validation |
|--------------|--------|--------|------------|
| Finished Roll Barcode | Scan | String (30 chars) | Must exist |
| Search Filters | Operator | Date range, process type, status | Valid values |
| Transfer Destination | Input | String | "Inspection" (default) |
| Transfer Remarks | Operator | String (500 chars) | Optional |

### Output Data

| Data Element | Destination | Format | Purpose |
|--------------|-------------|--------|---------|
| Inventory List | UI Display | List of finished rolls | Search results |
| Transfer Records | tblFinishedRollTransfer | Database records | Audit trail |
| Roll Status Updates | tblFinishedRoll | Status changes | Inventory tracking |
| Process History | UI Display | Parameters + deviations | Quality investigation |
| Full Traceability Chain | UI Display | Hierarchical tree | Root cause analysis |
| Transfer Document | Printer | Printed document | Material movement |

---

## 8. Database Operations

### Stored Procedures Used

#### sp_LuckyTex_FinishedRoll_GetInventory
- **Purpose**: Search finished roll inventory
- **Parameters**: @ProcessType, @Status, @DateFrom, @DateTo
- **Returns**: List of finished rolls with process info
- **Tables Read**: tblFinishedRoll, tblFinishingProcess, tblProduct

#### sp_LuckyTex_FinishedRoll_GetByBarcode
- **Purpose**: Get finished roll details
- **Parameters**: @Barcode VARCHAR(30)
- **Returns**: Roll details + status + deviation count
- **Tables Read**: tblFinishedRoll, tblFinishingProcess, tblFinishingDeviation

#### sp_LuckyTex_FinishedRoll_Transfer
- **Purpose**: Record finished roll transfer
- **Parameters**: @RollBarcode, @Destination, @OperatorID, @Remarks
- **Returns**: Transfer ID
- **Tables Written**: tblFinishedRollTransfer

#### sp_LuckyTex_FinishedRoll_UpdateStatus
- **Purpose**: Update finished roll status
- **Parameters**: @RollBarcode, @Status
- **Returns**: Success flag
- **Tables Written**: tblFinishedRoll

#### sp_LuckyTex_FinishedRoll_GetTraceability
- **Purpose**: Get full traceability chain
- **Parameters**: @FinishedRollBarcode VARCHAR(30)
- **Returns**: Finished → Grey → Beam → Yarn lots chain
- **Tables Read**: tblFinishedRoll, tblTraceability, tblFabricRoll, tblRollTraceability, tblBeam, tblBeamTraceability, tblInventory

#### sp_LuckyTex_FinishedRoll_GetProcessHistory
- **Purpose**: Get process parameters and deviations
- **Parameters**: @FinishedRollBarcode VARCHAR(30)
- **Returns**: Process parameters, parameter logs, deviations
- **Tables Read**: tblFinishingProcess, tblFinishingParameterLog, tblFinishingDeviation

### Transaction Scope

#### Finished Roll Transfer Transaction
```sql
BEGIN TRANSACTION
  FOR EACH roll:
    1. INSERT INTO tblFinishedRollTransfer (sp_LuckyTex_FinishedRoll_Transfer)
    2. UPDATE tblFinishedRoll - set status (sp_LuckyTex_FinishedRoll_UpdateStatus)
COMMIT TRANSACTION
```

---

## 9. Implementation Checklist

### Phase 1: Repository Layer
- [ ] Extend `IFinishingRepository`
  - [ ] GetFinishedRollInventory(filters) method
  - [ ] GetFinishedRollByBarcode(barcode) method
  - [ ] TransferFinishedRoll(transfer) method
  - [ ] UpdateFinishedRollStatus(barcode, status) method
  - [ ] GetFinishedRollTraceability(barcode) method
  - [ ] GetProcessHistory(barcode) method
- [ ] Implement in `FinishingRepository`
- [ ] Unit tests

### Phase 2: Service Layer
- [ ] Extend `IFinishingService`
  - [ ] GetFinishedRollInventory(filters) method
  - [ ] GetFinishedRollDetails(barcode) method
  - [ ] TransferFinishedRolls(request) method
  - [ ] GetFullTraceability(barcode) method
  - [ ] GetProcessHistory(barcode) method
- [ ] Create `FinishedRollTransferValidator`
  - [ ] Validate roll status = Available
  - [ ] Validate not already transferred
- [ ] Implement in `FinishingService`
- [ ] Unit tests

### Phase 3: UI Refactoring
- [ ] Update `FinishedRollInventoryPage.xaml.cs`
  - [ ] Inject IFinishingService
  - [ ] Search handler
  - [ ] Summary card calculations
  - [ ] Export to Excel
- [ ] Update `FinishedRollTransferPage.xaml.cs`
  - [ ] Transfer workflow
  - [ ] Deviation warning display
  - [ ] Print transfer document
- [ ] Update `FinishedRollTraceabilityPage.xaml.cs`
  - [ ] Traceability tree display
  - [ ] Process history display
  - [ ] Deviation details

### Phase 4: Integration Testing
- [ ] Test inventory search with filters
- [ ] Test transfer workflow
- [ ] Test traceability display (full chain)
- [ ] Test process history display
- [ ] Test transfer document printing

### Phase 5: Deployment
- [ ] Code review
- [ ] Unit tests passing
- [ ] UAT
- [ ] Production deployment

---

**Document Version**: 1.0
**Last Updated**: 2025-10-06
**Status**: Ready for Implementation
**Estimated Effort**: 2 days
