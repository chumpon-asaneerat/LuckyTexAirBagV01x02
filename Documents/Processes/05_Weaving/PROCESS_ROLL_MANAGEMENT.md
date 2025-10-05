# Process: Fabric Roll Management

**Process ID**: WV-003
**Module**: 05 - Weaving
**Priority**: P2 (Core Production Module)
**Created**: 2025-10-06

---

## 1. Process Overview

### Purpose
Manage fabric roll inventory from creation during weaving to transfer to downstream processes (Finishing/Inspection). Track roll location, status, quality attributes, and provide traceability to source materials.

### Scope
- View and search fabric roll inventory
- Update roll status and location
- Transfer rolls to Finishing/Inspection departments
- Track roll consumption history
- Maintain traceability to beam and weft sources
- Handle roll adjustments (rework, scrap)

### Module(s) Involved
- **Primary**: M05 - Weaving
- **Upstream**: Weaving Production (roll creation)
- **Downstream**: M06 - Finishing, M08 - Inspection (roll consumption)

---

## 2. UI Files Inventory

### XAML Files
| File Path | Description | Purpose |
|-----------|-------------|---------|
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/RollInventoryPage.xaml` | Roll inventory dashboard | View and search rolls |
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/RollTransferPage.xaml` | Roll transfer interface | Transfer to downstream |
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/RollTraceabilityPage.xaml` | Traceability viewer | View roll source materials |
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/WeavingMenuPage.xaml` | Module dashboard | Navigation |

### Code-Behind Files
| File Path | Description |
|-----------|-------------|
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/RollInventoryPage.xaml.cs` | Inventory logic |
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/RollTransferPage.xaml.cs` | Transfer logic |
| `LuckyTex.AirBag.Pages/Pages/05 - Weaving/RollTraceabilityPage.xaml.cs` | Traceability display |

### Service Files
| File Path | Description |
|-----------|-------------|
| *(To be created)* `LuckyTex.AirBag.Core/Repositories/WeavingRepository.cs` | Repository |
| *(To be created)* `LuckyTex.AirBag.Core/Services/WeavingService.cs` | Service layer |
| *(To be created)* `LuckyTex.AirBag.Core/Validators/RollTransferValidator.cs` | FluentValidation |

---

## 3. UI Layout Description

### RollInventoryPage.xaml

**Screen Title**: "Fabric Roll Inventory"

**Key UI Controls**:
- **Search Section**:
  - Roll barcode search textbox
  - Date range filter (From/To)
  - Product filter dropdown
  - Status filter (All, Available, In Process, Consumed, Scrapped)
  - `cmdSearch` - Execute search

- **Inventory DataGrid**:
  - Columns: Barcode, Product, Length (m), Loom, Production Date, Status, Location, Age (days)
  - Row double-click: Open detail view

- **Summary Cards**:
  - Total rolls available
  - Total meters available
  - Rolls pending inspection
  - Rolls in finishing

- **Action Buttons**:
  - `cmdTransfer` - Transfer selected roll
  - `cmdViewTrace` - View traceability
  - `cmdPrint` - Print roll label
  - `cmdExport` - Export to Excel

### RollTransferPage.xaml

**Screen Title**: "Fabric Roll Transfer"

**Key UI Controls**:
- Roll barcode scan textbox
- Display roll details (read-only)
- Destination dropdown (Finishing, Inspection, Warehouse)
- Target location textbox
- Transfer remarks
- Transfer list DataGrid
- `cmdAdd` - Add to transfer list
- `cmdRemove` - Remove from list
- `cmdSave` - Confirm transfer
- `cmdPrint` - Print transfer document

### RollTraceabilityPage.xaml

**Screen Title**: "Fabric Roll Traceability"

**Key UI Controls**:
- Roll barcode input
- **Source Materials Section**:
  - Beam barcode
  - Beam source yarn lots (DataGrid)
  - Weft yarn lot
  - Weft supplier info
- **Production Info Section**:
  - Loom ID, Operator, Date
  - Production session details
- **Downstream Usage Section**:
  - Finishing records
  - Inspection results
  - Cut pieces (if consumed)

---

## 4. Component Architecture Diagram

```mermaid
graph TD
    UI1[RollInventoryPage.xaml] --> CB1[RollInventoryPage.xaml.cs]
    UI2[RollTransferPage.xaml] --> CB2[RollTransferPage.xaml.cs]
    UI3[RollTraceabilityPage.xaml] --> CB3[RollTraceabilityPage.xaml.cs]

    CB1 --> SVC[IWeavingService]
    CB2 --> SVC
    CB3 --> SVC

    SVC --> VAL[RollTransferValidator]
    SVC --> REPO[IWeavingRepository]

    REPO --> DB[(Oracle Database)]

    DB --> SP1[sp_LuckyTex_Roll_GetInventory]
    DB --> SP2[sp_LuckyTex_Roll_GetByBarcode]
    DB --> SP3[sp_LuckyTex_Roll_Transfer]
    DB --> SP4[sp_LuckyTex_Roll_UpdateStatus]
    DB --> SP5[sp_LuckyTex_Roll_GetTraceability]

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
    START[Start: Fabric Roll Management] --> OPTION{Action?}

    OPTION -->|View Inventory| SEARCH[Search Rolls with Filters]
    SEARCH --> DISPLAY[Display Results in DataGrid]
    DISPLAY --> END[End]

    OPTION -->|Transfer Roll| SCAN[Scan Roll Barcode]
    SCAN --> VALIDATE{Roll Status<br/>= Available?}
    VALIDATE -->|No| ERROR1[Error: Roll Not Available]
    ERROR1 --> END

    VALIDATE -->|Yes| SELECT_DEST[Select Destination]
    SELECT_DEST --> ADD_LIST[Add to Transfer List]
    ADD_LIST --> MORE{Transfer More?}
    MORE -->|Yes| SCAN
    MORE -->|No| SAVE_TRANSFER[Save Transfer Records]
    SAVE_TRANSFER --> UPDATE_STATUS[Update Roll Status = In Process]
    UPDATE_STATUS --> PRINT[Print Transfer Document]
    PRINT --> SUCCESS[Success]
    SUCCESS --> END

    OPTION -->|View Traceability| SCAN_TRACE[Scan Roll Barcode]
    SCAN_TRACE --> LOAD_TRACE[Load Traceability Data:<br/>Beam, Weft, Production]
    LOAD_TRACE --> DISPLAY_TRACE[Display Full Chain]
    DISPLAY_TRACE --> END

    style START fill:#e1f5ff
    style SUCCESS fill:#e1ffe1
    style END fill:#e1f5ff
    style ERROR1 fill:#ffe1e1
```

---

## 6. Business Logic Sequence Diagram

```mermaid
sequenceDiagram
    participant Operator
    participant UI as RollTransferPage
    participant BL as WeavingService
    participant VAL as RollTransferValidator
    participant REPO as WeavingRepository
    participant DB as Oracle Database
    participant Printer

    Operator->>UI: Scan roll barcode
    UI->>BL: GetRollDetails(barcode)
    BL->>REPO: GetRollByBarcode(barcode)
    REPO->>DB: sp_LuckyTex_Roll_GetByBarcode
    DB-->>REPO: Roll details + status
    REPO-->>BL: Roll entity

    alt Roll exists and available
        BL-->>UI: Display roll info
        Operator->>UI: Select destination
        Operator->>UI: Click Add

        UI->>BL: ValidateTransfer(barcode, destination)
        BL->>VAL: Validate transfer request
        VAL->>VAL: Check roll status = Available
        VAL->>VAL: Check destination valid

        alt Validation passed
            VAL-->>BL: OK
            BL-->>UI: Add to transfer list
            UI->>UI: Update DataGrid

            Operator->>UI: Click Save
            UI->>BL: TransferRolls(transferRequest)
            BL->>REPO: BeginTransaction()

            loop For each roll
                BL->>REPO: InsertTransferRecord(transfer)
                REPO->>DB: sp_LuckyTex_Roll_Transfer
                DB-->>REPO: Transfer ID

                BL->>REPO: UpdateRollStatus(barcode, 'In Process')
                REPO->>DB: sp_LuckyTex_Roll_UpdateStatus
                DB-->>REPO: Status updated
            end

            BL->>REPO: CommitTransaction()
            BL-->>UI: Transfer complete

            UI->>Printer: Print transfer document
            Printer-->>UI: Document printed

            UI->>UI: Success, clear form

        else Validation failed
            VAL-->>BL: Error
            BL-->>UI: Show error
        end

    else Roll not available
        BL-->>UI: Error: Roll not found or not available
    end
```

---

## 7. Data Flow

### Input Data

| Data Element | Source | Format | Validation |
|--------------|--------|--------|------------|
| Roll Barcode | Scan/search | String (30 chars) | Must exist |
| Search Filters | Operator input | Date range, product, status | Valid values |
| Destination | Dropdown | String | Valid department |
| Transfer Location | Operator input | String (50 chars) | Optional |
| Transfer Remarks | Operator input | String (500 chars) | Optional |

### Output Data

| Data Element | Destination | Format | Purpose |
|--------------|-------------|--------|---------|
| Roll Inventory List | UI Display | List of rolls | Search results |
| Transfer Records | tblRollTransfer | Database records | Audit trail |
| Roll Status Updates | tblFabricRoll | Status changes | Inventory tracking |
| Traceability Data | UI Display | Beam + Weft links | Quality investigation |
| Transfer Document | Printer | Printed document | Material movement record |

---

## 8. Database Operations

### Stored Procedures Used

#### sp_LuckyTex_Roll_GetInventory
- **Purpose**: Search fabric roll inventory
- **Parameters**: @ProductCode, @Status, @DateFrom, @DateTo
- **Returns**: List of rolls
- **Tables Read**: tblFabricRoll, tblProduct, tblLoom

#### sp_LuckyTex_Roll_GetByBarcode
- **Purpose**: Get roll details by barcode
- **Parameters**: @Barcode VARCHAR(30)
- **Returns**: Roll details + status
- **Tables Read**: tblFabricRoll

#### sp_LuckyTex_Roll_Transfer
- **Purpose**: Record roll transfer
- **Parameters**: @RollBarcode, @Destination, @Location, @OperatorID, @Remarks
- **Returns**: Transfer ID
- **Tables Written**: tblRollTransfer

#### sp_LuckyTex_Roll_UpdateStatus
- **Purpose**: Update roll status
- **Parameters**: @RollBarcode, @Status
- **Returns**: Success flag
- **Tables Written**: tblFabricRoll

#### sp_LuckyTex_Roll_GetTraceability
- **Purpose**: Get full traceability chain
- **Parameters**: @RollBarcode VARCHAR(30)
- **Returns**: Beam, weft, yarn lots, production details
- **Tables Read**: tblRollTraceability, tblBeam, tblBeamTraceability, tblInventory, tblWeavingProduction

### Transaction Scope

#### Roll Transfer Transaction
```sql
BEGIN TRANSACTION
  FOR EACH roll:
    1. INSERT INTO tblRollTransfer (sp_LuckyTex_Roll_Transfer)
    2. UPDATE tblFabricRoll - set status (sp_LuckyTex_Roll_UpdateStatus)
COMMIT TRANSACTION
```

---

## 9. Implementation Checklist

### Phase 1: Repository Layer
- [ ] Extend `IWeavingRepository`
  - [ ] GetRollInventory(filters) method
  - [ ] GetRollByBarcode(barcode) method
  - [ ] TransferRoll(transfer) method
  - [ ] UpdateRollStatus(barcode, status) method
  - [ ] GetRollTraceability(barcode) method
- [ ] Implement in `WeavingRepository`
- [ ] Unit tests

### Phase 2: Service Layer
- [ ] Extend `IWeavingService`
  - [ ] GetRollInventory(filters) method
  - [ ] GetRollDetails(barcode) method
  - [ ] TransferRolls(request) method
  - [ ] GetRollTraceability(barcode) method
- [ ] Create `RollTransferValidator`
  - [ ] Validate roll status
  - [ ] Validate destination
- [ ] Implement in `WeavingService`
- [ ] Unit tests

### Phase 3: UI Refactoring
- [ ] Update `RollInventoryPage.xaml.cs`
  - [ ] Inject IWeavingService
  - [ ] Search handler
  - [ ] Export to Excel
- [ ] Update `RollTransferPage.xaml.cs`
  - [ ] Transfer workflow
  - [ ] Print document
- [ ] Update `RollTraceabilityPage.xaml.cs`
  - [ ] Display traceability tree

### Phase 4: Integration Testing
- [ ] Test inventory search
- [ ] Test roll transfer
- [ ] Test traceability display
- [ ] Test document printing

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
