# LuckyTex AirBag MES - UI Navigation and Screen Flows

**Purpose**: Document screen-to-screen navigation and UI workflows for all 21 modules

---

## Overall Application Navigation

### Main Dashboard Navigation

```mermaid
graph TD
    START[Application Start] --> LOGIN[Login Screen]
    LOGIN --> MAIN[M00 - Main Dashboard]

    MAIN --> M01[M01 - Warehouse Dashboard]
    MAIN --> M02[M02 - Warping Dashboard]
    MAIN --> M03[M03 - Beaming Dashboard]
    MAIN --> M04[M04 - Drawing Dashboard]
    MAIN --> M05[M05 - Weaving Dashboard]
    MAIN --> M06[M06 - Finishing Dashboard]
    MAIN --> M08[M08 - Inspection Dashboard]
    MAIN --> M11[M11 - Cut & Print Dashboard]
    MAIN --> M13[M13 - Packing Dashboard]
    MAIN --> M14[M14 - Shipping Dashboard]
    MAIN --> M17[M17 - Master Data Dashboard]
    MAIN --> M20[M20 - User Management Dashboard]

    style MAIN fill:#e1f5ff
    style M01 fill:#fff4e1
    style M02 fill:#fff4e1
    style M05 fill:#ffe1e1
    style M08 fill:#e1ffe1
```

---

## Module 01 - Warehouse (12 Pages)

### Warehouse Dashboard Navigation

```mermaid
graph TD
    WH_DASH[Warehouse Dashboard] --> WH_RECEIVE[Yarn Receiving]
    WH_DASH --> WH_LIST[Yarn Lot List]
    WH_DASH --> WH_STOCK[Stock Report]
    WH_DASH --> WH_ISSUE[Yarn Issuing]
    WH_DASH --> WH_TRACE[Traceability]
    WH_DASH --> WH_TRANSFER[Transfer]
    WH_DASH --> WH_ADJUST[Adjustment]
    WH_DASH --> WH_SUPPLIER[Supplier Mgmt]
    WH_DASH --> WH_PO[PO Management]
    WH_DASH --> WH_CONFIG[Configuration]
    WH_DASH --> WH_INV_RPT[Inventory Report]

    style WH_DASH fill:#e1f5ff
    style WH_RECEIVE fill:#fff4e1
    style WH_LIST fill:#fff4e1
    style WH_STOCK fill:#e1ffe1
```

### Yarn Receiving Flow

```mermaid
sequenceDiagram
    participant User
    participant Screen as Yarn Receiving Screen
    participant Service as Warehouse Service
    participant DB as Database

    User->>Screen: Open Yarn Receiving page
    Screen->>Screen: Focus on Barcode TextBox

    User->>Screen: Scan/Enter Barcode + Press Enter
    Screen->>Service: GetYarnLotByBarcode(barcode)
    Service->>DB: sp_LuckyTex_Yarn_GetByBarcode

    alt Barcode exists
        DB-->>Service: PO Details
        Service-->>Screen: Display PO Info
        Screen->>Screen: Show supplier, yarn type, color, PO qty

        User->>Screen: Enter actual quantity received
        User->>Screen: Click Confirm
        Screen->>Service: ReceiveYarnLot(barcode, quantity, poNumber)
        Service->>DB: sp_LuckyTex_Yarn_Receive
        Service->>DB: sp_LuckyTex_Inventory_Update
        DB-->>Service: Success
        Service-->>Screen: Receipt confirmed
        Screen->>Screen: Display success message
        Screen->>Screen: Generate & print barcode label
        Screen->>Screen: Clear form, refocus barcode
    else Barcode not found
        DB-->>Service: No data
        Service-->>Screen: Error: Barcode not found
        Screen->>Screen: Show error message
        Screen->>Screen: Clear barcode, refocus
    end
```

### Yarn Lot List CRUD Flow

```mermaid
sequenceDiagram
    participant User
    participant Screen as Yarn Lot List Screen
    participant Service as Warehouse Service
    participant DB as Database

    User->>Screen: Open Yarn Lot List
    Screen->>Service: GetYarnLots(filter)
    Service->>DB: sp_LuckyTex_Yarn_GetList
    DB-->>Service: Yarn lots data
    Service-->>Screen: List of yarn lots
    Screen->>Screen: Populate DataGrid

    alt Add New Lot
        User->>Screen: Click Add button
        Screen->>Screen: Open Add dialog
        User->>Screen: Enter lot details
        User->>Screen: Click Save
        Screen->>Service: InsertYarnLot(lot)
        Service->>DB: sp_LuckyTex_Yarn_Insert
        DB-->>Service: Success
        Service-->>Screen: Lot added
        Screen->>Screen: Refresh DataGrid
    else Edit Lot
        User->>Screen: Select row + Click Edit
        Screen->>Screen: Open Edit dialog with data
        User->>Screen: Modify details
        User->>Screen: Click Save
        Screen->>Service: UpdateYarnLot(lot)
        Service->>DB: sp_LuckyTex_Yarn_Update
        DB-->>Service: Success
        Service-->>Screen: Lot updated
        Screen->>Screen: Refresh DataGrid
    else Delete Lot
        User->>Screen: Select row + Click Delete
        Screen->>Screen: Show confirmation dialog
        User->>Screen: Click Yes
        Screen->>Service: DeleteYarnLot(lotNumber)
        Service->>DB: sp_LuckyTex_Yarn_Delete
        DB-->>Service: Success
        Service-->>Screen: Lot deleted
        Screen->>Screen: Refresh DataGrid
    end
```

---

## Module 02 - Warping (15 Pages)

### Warping Dashboard Navigation

```mermaid
graph TD
    WP_DASH[Warping Dashboard] --> WP_PROD[Production Entry]
    WP_DASH --> WP_CREEL[Creel Loading]
    WP_DASH --> WP_BEAM[Beam List]
    WP_DASH --> WP_MONITOR[PLC Monitor]
    WP_DASH --> WP_DEFECT[Defect Entry]
    WP_DASH --> WP_COMPLETE[Complete Beam]
    WP_DASH --> WP_REPORT[Production Report]
    WP_DASH --> WP_EFFICIENCY[Efficiency Report]
    WP_DASH --> WP_MACHINE[Machine Status]
    WP_DASH --> WP_SPEC[Beam Specification]
    WP_DASH --> WP_HISTORY[Production History]
    WP_DASH --> WP_TRACE[Beam Traceability]
    WP_DASH --> WP_QC[Quality Check]
    WP_DASH --> WP_MATERIAL[Material Consumption]
    WP_DASH --> WP_CONFIG[Configuration]

    style WP_DASH fill:#e1f5ff
    style WP_PROD fill:#fff4e1
    style WP_CREEL fill:#fff4e1
    style WP_MONITOR fill:#ffe1e1
```

### Warping Production Entry Flow

```mermaid
sequenceDiagram
    participant User
    participant Screen as Production Entry Screen
    participant Service as Warping Service
    participant PLC as PLC Controller
    participant DB as Database

    User->>Screen: Open Production Entry
    Screen->>Service: GetMachines()
    Screen->>Service: GetOperators()
    Service->>DB: Load machine & operator lists
    DB-->>Service: Lists
    Service-->>Screen: Populate dropdowns

    User->>Screen: Scan/Enter Beam Barcode
    Screen->>Service: GetBeamByBarcode(barcode)
    Service->>DB: sp_LuckyTex_Warping_GetBeam

    alt New Beam
        DB-->>Service: Not found
        Service-->>Screen: New beam
        Screen->>Screen: Enable creel loading
        User->>Screen: Navigate to Creel Loading
        Note over User,DB: See Creel Loading sequence
        User->>Screen: Return to Production Entry
    else Existing Beam
        DB-->>Service: Beam details
        Service-->>Screen: Display beam info
        Screen->>Screen: Show creel configuration
    end

    User->>Screen: Select Machine
    User->>Screen: Select Operator
    User->>Screen: Click Start Production

    Screen->>Service: StartProduction(beamId, machineId, operatorId)
    Service->>DB: sp_LuckyTex_Warping_StartProduction
    DB-->>Service: Production record created
    Service-->>Screen: Production started

    Screen->>PLC: Send Start command (Modbus)
    PLC-->>Screen: Machine started

    loop Every 2 seconds
        Screen->>PLC: Read status registers
        PLC-->>Screen: Meter count, speed, status
        Screen->>Screen: Update UI display
    end

    alt Break/Defect Occurs
        User->>Screen: Click Record Defect
        Screen->>Service: RecordDefect(details)
        Service->>DB: sp_LuckyTex_Warping_RecordDefect
        Screen->>Screen: Continue monitoring
    else Complete Production
        User->>Screen: Click Complete
        Screen->>PLC: Send Stop command
        PLC-->>Screen: Machine stopped
        Screen->>Service: GetFinalMeterReading()
        Screen->>Screen: Show completion dialog
        User->>Screen: Confirm final length
        Screen->>Service: CompleteProduction(finalLength)
        Service->>DB: sp_LuckyTex_Warping_Complete
        Service->>DB: sp_LuckyTex_Inventory_Update
        DB-->>Service: Success
        Service-->>Screen: Production completed
        Screen->>Screen: Print beam label
        Screen->>Screen: Reset form
    end
```

### Creel Loading Flow

```mermaid
sequenceDiagram
    participant User
    participant Screen as Creel Loading Screen
    participant Service as Warping Service
    participant DB as Database

    User->>Screen: Open Creel Loading (beamId)
    Screen->>Service: GetBeamSpec(beamId)
    Service->>DB: sp_LuckyTex_Warping_GetBeamSpec
    DB-->>Service: Beam spec (total positions, yarn type)
    Service-->>Screen: Display spec

    loop For each position (1-800)
        User->>Screen: Enter position number
        Screen->>Screen: Focus on barcode textbox

        User->>Screen: Scan yarn lot barcode
        Screen->>Service: ValidateYarnForCreel(beamId, barcode, position)
        Service->>DB: sp_LuckyTex_Warping_ValidateYarn

        alt Valid Yarn
            DB-->>Service: Yarn compatible + sufficient qty
            Service-->>Screen: Validation OK
            Screen->>Screen: Display yarn details (type, color, supplier, qty)
            Screen->>Service: LoadCreelPosition(beamId, position, barcode)
            Service->>DB: sp_LuckyTex_Warping_LoadCreel
            Service->>DB: sp_LuckyTex_Inventory_Reserve
            DB-->>Service: Position loaded
            Service-->>Screen: Success
            Screen->>Screen: Mark position as loaded (green)
            Screen->>Screen: Move to next position
        else Invalid Yarn
            DB-->>Service: Error (wrong type/color/insufficient qty)
            Service-->>Screen: Validation failed
            Screen->>Screen: Show error message
            Screen->>Screen: Clear barcode, stay on position
        end
    end

    Screen->>Screen: Check all positions loaded
    Screen->>Service: CompleteCreelLoading(beamId)
    Service->>DB: sp_LuckyTex_Warping_UpdateBeamStatus
    DB-->>Service: Beam ready for production
    Service-->>Screen: Creel loading complete
    Screen->>Screen: Close screen
```

---

## Module 05 - Weaving (18 Pages)

### Weaving Dashboard Navigation

```mermaid
graph TD
    WV_DASH[Weaving Dashboard] --> WV_PROD[Production Entry]
    WV_DASH --> WV_LOOM[Loom Setup]
    WV_DASH --> WV_ROLL[Roll Generation]
    WV_DASH --> WV_LIST[Fabric Roll List]
    WV_DASH --> WV_MONITOR[Loom Monitor]
    WV_DASH --> WV_BREAK[Break Recording]
    WV_DASH --> WV_WEFT[Weft Change]
    WV_DASH --> WV_COMPLETE[Complete Weaving]
    WV_DASH --> WV_REPORT[Production Report]
    WV_DASH --> WV_EFFICIENCY[Efficiency Dashboard]
    WV_DASH --> WV_DEFECT[Defect Analysis]
    WV_DASH --> WV_TRACE[Fabric Traceability]
    WV_DASH --> WV_QC[Quality Check]
    WV_DASH --> WV_PICK[Pick Counter]
    WV_DASH --> WV_MATERIAL[Material Usage]
    WV_DASH --> WV_SCHEDULE[Production Schedule]
    WV_DASH --> WV_MAINTENANCE[Maintenance Log]
    WV_DASH --> WV_CONFIG[Configuration]

    style WV_DASH fill:#e1f5ff
    style WV_PROD fill:#ffe1e1
    style WV_MONITOR fill:#ffe1e1
    style WV_ROLL fill:#fff4e1
```

### Weaving Production Flow

```mermaid
sequenceDiagram
    participant User
    participant Screen as Weaving Production Screen
    participant Service as Weaving Service
    participant PLC as Loom PLC
    participant DB as Database

    User->>Screen: Open Weaving Production
    Screen->>Service: GetLooms()
    Screen->>Service: GetOperators()
    Service->>DB: Load lists
    DB-->>Service: Data
    Service-->>Screen: Populate UI

    User->>Screen: Scan beam barcode
    Screen->>Service: GetBeamForWeaving(barcode)
    Service->>DB: sp_LuckyTex_Weaving_GetBeam

    alt Beam not ready
        DB-->>Service: Status != Ready for Weaving
        Service-->>Screen: Error: Beam not ready
        Screen->>Screen: Show error, clear
    else Beam ready
        DB-->>Service: Beam details + yarn composition
        Service-->>Screen: Display beam info

        User->>Screen: Select loom
        User->>Screen: Scan weft yarn lot barcode
        Screen->>Service: ValidateWeftYarn(beamId, weftBarcode)
        Service->>DB: sp_LuckyTex_Weaving_ValidateWeft
        DB-->>Service: Validation result
        Service-->>Screen: Weft validated

        User->>Screen: Enter target fabric length
        User->>Screen: Select operator
        User->>Screen: Click Start Weaving

        Screen->>Service: StartWeaving(beamId, loomId, weftLot, targetLength, operatorId)
        Service->>DB: sp_LuckyTex_Weaving_StartProduction
        DB-->>Service: Production record created
        Service-->>Screen: Weaving started

        Screen->>PLC: Send Start command
        PLC-->>Screen: Loom started

        loop Real-time monitoring (every 2 sec)
            Screen->>PLC: Read pick counter, speed, status
            PLC-->>Screen: Current data
            Screen->>Screen: Calculate fabric length = picks / picks-per-cm
            Screen->>Screen: Update progress display

            alt Weft break detected
                PLC-->>Screen: Weft break alarm
                Screen->>Screen: Show alarm notification
                PLC->>PLC: Auto-stop loom
                User->>Screen: Acknowledge alarm
                Screen->>Service: RecordDefect(WEFT_BREAK, position)
                Service->>DB: sp_LuckyTex_Weaving_RecordDefect
                User->>Screen: Fix weft, Click Resume
                Screen->>PLC: Clear alarm, restart
            else Warp break detected
                PLC-->>Screen: Warp break alarm (critical)
                Screen->>Screen: Show critical alarm
                PLC->>PLC: Emergency stop
                User->>Screen: Click Record Critical Defect
                Screen->>Service: RecordCriticalDefect(WARP_BREAK, position)
                Service->>DB: sp_LuckyTex_Weaving_RecordDefect
                Screen->>Screen: Mark production for inspection
            end
        end

        alt Stop Mark (Generate Roll)
            User->>Screen: Click Stop Mark
            Screen->>PLC: Read current meter
            PLC-->>Screen: Current position
            Screen->>Service: GenerateFabricRoll(beamId, startMeter, endMeter)
            Service->>DB: sp_LuckyTex_Weaving_CreateRoll
            DB-->>Service: Roll barcode generated
            Service-->>Screen: Roll created
            Screen->>Screen: Print roll label
            Screen->>Screen: Continue weaving
        else Complete Weaving
            User->>Screen: Click Complete
            Screen->>PLC: Send Stop command
            PLC-->>Screen: Loom stopped
            Screen->>Service: GetFinalMeters()
            Screen->>Screen: Show completion dialog
            User->>Screen: Confirm final data
            Screen->>Service: CompleteWeaving(finalMeters, totalPicks)
            Service->>DB: sp_LuckyTex_Weaving_Complete
            Service->>DB: sp_LuckyTex_Weaving_CalculateConsumption
            Service->>DB: sp_LuckyTex_Inventory_Update
            DB-->>Service: Success
            Service-->>Screen: Weaving completed
            Screen->>Screen: Display summary (rolls produced, efficiency)
            Screen->>Screen: Print final report
            Screen->>Screen: Reset form
        end
    end
```

### Fabric Roll Generation Flow

```mermaid
sequenceDiagram
    participant User
    participant Screen as Production Screen
    participant Service as Weaving Service
    participant Printer as Label Printer
    participant DB as Database

    Note over User,DB: During active weaving production

    User->>Screen: Click "Stop Mark" button
    Screen->>Screen: Capture current timestamp
    Screen->>Screen: Read PLC meter counter

    Screen->>Service: GenerateRoll(beamId, productionId, meterStart, meterEnd)
    Service->>Service: Calculate roll length = meterEnd - meterStart
    Service->>Service: Validate length (50-100m range)

    alt Length valid
        Service->>DB: sp_LuckyTex_Weaving_GenerateRollBarcode
        DB-->>Service: Unique roll barcode (auto-increment)

        Service->>DB: sp_LuckyTex_Weaving_CreateRoll
        Note over DB: Insert into tblFabricRoll:<br/>RollBarcode, BeamId, Length,<br/>LoomNumber, DateTime, Operator
        DB-->>Service: Roll created

        Service->>DB: sp_LuckyTex_Traceability_LinkRollToBeam
        DB-->>Service: Traceability linked

        Service-->>Screen: Roll data
        Screen->>Printer: Print label with:<br/>- Roll barcode<br/>- Beam number<br/>- Length (m)<br/>- Loom number<br/>- Date/Time<br/>- Operator name
        Printer-->>Screen: Label printed

        Screen->>Screen: Show success message
        Screen->>Screen: Reset "last stop mark" to current meter
        Screen->>Screen: Increment roll counter
        Screen->>Screen: Continue production
    else Length invalid
        Service-->>Screen: Error: Invalid length
        Screen->>Screen: Show error, cancel operation
    end
```

---

## Module 08 - Inspection (14 Pages)

### Inspection Dashboard Navigation

```mermaid
graph TD
    INS_DASH[Inspection Dashboard] --> INS_ENTRY[Inspection Entry]
    INS_DASH --> INS_LIST[Inspection List]
    INS_DASH --> INS_DEFECT[Defect Recording]
    INS_DASH --> INS_GRADE[Grade Calculation]
    INS_DASH --> INS_APPROVE[Approve/Reject]
    INS_DASH --> INS_REPORT[Inspection Report]
    INS_DASH --> INS_DEFECT_RPT[Defect Analysis]
    INS_DASH --> INS_TREND[Quality Trend]
    INS_DASH --> INS_CERT[Certificate Print]
    INS_DASH --> INS_TRACE[Roll Traceability]
    INS_DASH --> INS_REWORK[Rework Queue]
    INS_DASH --> INS_STANDARD[Standard Setup]
    INS_DASH --> INS_INSPECTOR[Inspector Performance]
    INS_DASH --> INS_CONFIG[Configuration]

    style INS_DASH fill:#e1f5ff
    style INS_ENTRY fill:#e1ffe1
    style INS_DEFECT fill:#e1ffe1
    style INS_GRADE fill:#e1ffe1
```

### Inspection Entry Flow

```mermaid
sequenceDiagram
    participant User as Inspector
    participant Screen as Inspection Entry Screen
    participant Service as Inspection Service
    participant Printer as Certificate Printer
    participant DB as Database

    User->>Screen: Open Inspection Entry
    Screen->>Service: GetInspectors()
    Service->>DB: sp_LuckyTex_Inspection_GetInspectors
    DB-->>Service: Inspector list
    Service-->>Screen: Populate dropdown

    User->>Screen: Select inspector (self)
    User->>Screen: Scan fabric roll barcode

    Screen->>Service: GetRollForInspection(barcode)
    Service->>DB: sp_LuckyTex_Inspection_GetRoll

    alt Roll not found
        DB-->>Service: No data
        Service-->>Screen: Error: Invalid roll
        Screen->>Screen: Clear, refocus
    else Roll already inspected
        DB-->>Service: Status = Inspected
        Service-->>Screen: Warning: Already inspected
        Screen->>Screen: Show previous inspection data
        Screen->>Screen: Offer re-inspection option
    else Roll ready
        DB-->>Service: Roll details (beam, length, loom, date)
        Service-->>Screen: Display roll information

        Screen->>Service: GetQualityStandard(rollType)
        Service->>DB: sp_LuckyTex_Inspection_GetStandard
        DB-->>Service: Standard criteria (defect types, point values)
        Service-->>Screen: Load defect types

        Screen->>Screen: Clear defect list
        Screen->>Screen: Reset point counter to 0

        loop Inspect fabric (manual review)
            User->>Screen: Identify defect on fabric
            User->>Screen: Enter defect position (meter mark)
            User->>Screen: Select defect type from dropdown
            User->>Screen: Select severity (Minor/Major/Critical)
            User->>Screen: Click Add Defect

            Screen->>Service: ValidateDefect(position, type, severity)
            Service->>Service: Calculate defect points based on severity
            Service-->>Screen: Defect validated

            Screen->>Screen: Add to defect list (DataGrid)
            Screen->>Screen: Update total points
            Screen->>Screen: Clear defect entry form
        end

        User->>Screen: Click Calculate Grade

        Screen->>Service: CalculateGrade(rollId, defectList)
        Service->>Service: Sum total penalty points
        Service->>Service: Apply grading logic:<br/>0-10 points = Grade A<br/>11-20 points = Grade B<br/>21+ points = Grade C<br/>Critical defect = Reject

        Service->>DB: sp_LuckyTex_Inspection_RecordDefects
        Service->>DB: sp_LuckyTex_Inspection_CalculateGrade
        DB-->>Service: Grade calculated
        Service-->>Screen: Grade result

        Screen->>Screen: Display grade (A/B/C/Reject)
        Screen->>Screen: Show defect summary

        alt Grade = Reject
            Screen->>Screen: Highlight as rejected (red)
            Screen->>Screen: Enable Rework/Scrap buttons
            User->>Screen: Select action (Rework/Scrap)
            Screen->>Service: UpdateRollStatus(REJECT, action)
            Service->>DB: sp_LuckyTex_Inspection_UpdateStatus
            DB-->>Service: Status updated
            Service-->>Screen: Roll marked for rework/scrap
        else Grade = A/B/C
            Screen->>Screen: Show as approved (green)
            User->>Screen: Click Approve
            Screen->>Service: ApproveRoll(rollId, grade)
            Service->>DB: sp_LuckyTex_Inspection_Approve
            Service->>DB: sp_LuckyTex_Inventory_UpdateGrade
            DB-->>Service: Roll approved
            Service-->>Screen: Approval confirmed

            Screen->>Printer: Print inspection certificate:<br/>- Roll barcode<br/>- Grade<br/>- Defect summary<br/>- Inspector name<br/>- Date/Time
            Printer-->>Screen: Certificate printed
        end

        Screen->>Screen: Show success message
        Screen->>Screen: Reset form for next roll
    end
```

---

## Module 17 - Master Data (14 Pages)

### Master Data Dashboard Navigation

```mermaid
graph TD
    MD_DASH[Master Data Dashboard] --> MD_MACHINE[Machine Management]
    MD_DASH --> MD_EMP[Employee Management]
    MD_DASH --> MD_SHIFT[Shift Management]
    MD_DASH --> MD_PRODUCT[Product Management]
    MD_DASH --> MD_CUSTOMER[Customer Management]
    MD_DASH --> MD_SUPPLIER[Supplier Management]
    MD_DASH --> MD_DEPT[Department Management]
    MD_DASH --> MD_LOCATION[Location Management]
    MD_DASH --> MD_DEFECT[Defect Type Setup]
    MD_DASH --> MD_IMPORT[Data Import]
    MD_DASH --> MD_EXPORT[Data Export]
    MD_DASH --> MD_SYNC[D365 Sync]
    MD_DASH --> MD_BACKUP[Backup/Restore]
    MD_DASH --> MD_CONFIG[Configuration]

    style MD_DASH fill:#e1f5ff
    style MD_MACHINE fill:#fff4e1
    style MD_EMP fill:#fff4e1
```

### Master Data CRUD Flow (Generic)

```mermaid
sequenceDiagram
    participant User
    participant Screen as Master Data Screen<br/>(Machine/Employee/etc.)
    participant Service as Master Data Service
    participant DB as Database

    User->>Screen: Open master data screen
    Screen->>Service: GetAll(filter, sortBy)
    Service->>DB: sp_LuckyTex_MasterData_Get[EntityType]
    DB-->>Service: Entity list
    Service-->>Screen: Data
    Screen->>Screen: Populate DataGrid

    alt Create New
        User->>Screen: Click Add button
        Screen->>Screen: Open Add dialog/panel
        Screen->>Screen: Clear all fields

        User->>Screen: Enter entity data (all fields)
        User->>Screen: Click Save

        Screen->>Service: Insert(entity)
        Service->>Service: Validate entity

        alt Validation passed
            Service->>DB: sp_LuckyTex_MasterData_Insert[EntityType]

            alt Insert successful
                DB-->>Service: New entity ID
                Service-->>Screen: Success result
                Screen->>Screen: Show success message
                Screen->>Screen: Close dialog
                Screen->>Service: GetAll (refresh)
                Service->>DB: sp_LuckyTex_MasterData_Get[EntityType]
                DB-->>Service: Updated list
                Service-->>Screen: Refreshed data
                Screen->>Screen: Refresh DataGrid
            else Duplicate/Constraint error
                DB-->>Service: Error (duplicate key, etc.)
                Service-->>Screen: Error message
                Screen->>Screen: Show error, stay in dialog
            end
        else Validation failed
            Service-->>Screen: Validation errors
            Screen->>Screen: Highlight invalid fields
            Screen->>Screen: Show error messages
        end

    else Edit Existing
        User->>Screen: Select row from DataGrid
        User->>Screen: Click Edit button
        Screen->>Screen: Open Edit dialog
        Screen->>Screen: Populate fields with selected entity

        User->>Screen: Modify field values
        User->>Screen: Click Save

        Screen->>Service: Update(entity)
        Service->>Service: Validate entity
        Service->>DB: sp_LuckyTex_MasterData_Update[EntityType]

        alt Update successful
            DB-->>Service: Rows affected = 1
            Service-->>Screen: Success
            Screen->>Screen: Show success message
            Screen->>Screen: Close dialog
            Screen->>Service: GetAll (refresh)
            Service->>DB: Query updated data
            DB-->>Service: Updated list
            Service-->>Screen: Data
            Screen->>Screen: Refresh DataGrid
        else Update failed
            DB-->>Service: Error
            Service-->>Screen: Error message
            Screen->>Screen: Show error
        end

    else Delete
        User->>Screen: Select row
        User->>Screen: Click Delete button
        Screen->>Screen: Show confirmation dialog:<br/>"Delete [EntityName]?"

        alt User confirms
            User->>Screen: Click Yes
            Screen->>Service: Delete(entityId)
            Service->>DB: sp_LuckyTex_MasterData_Delete[EntityType]

            alt Delete successful
                DB-->>Service: Rows affected = 1
                Service-->>Screen: Success
                Screen->>Screen: Show success message
                Screen->>Service: GetAll (refresh)
                Service->>DB: Query data
                DB-->>Service: Updated list
                Service-->>Screen: Data
                Screen->>Screen: Refresh DataGrid
            else Foreign key constraint
                DB-->>Service: Error: Referenced by other records
                Service-->>Screen: Error: Cannot delete
                Screen->>Screen: Show error:<br/>"Cannot delete - in use"
            end
        else User cancels
            User->>Screen: Click No
            Screen->>Screen: Close dialog, no action
        end

    else Search/Filter
        User->>Screen: Enter search text
        User->>Screen: Press Enter or Click Search
        Screen->>Service: GetAll(filter: searchText)
        Service->>DB: sp_LuckyTex_MasterData_Search[EntityType]
        DB-->>Service: Filtered results
        Service-->>Screen: Data
        Screen->>Screen: Refresh DataGrid with filtered data

    else Refresh
        User->>Screen: Click Refresh button
        Screen->>Service: GetAll()
        Service->>DB: sp_LuckyTex_MasterData_Get[EntityType]
        DB-->>Service: Current data
        Service-->>Screen: Data
        Screen->>Screen: Refresh DataGrid
    end
```

---

## Common UI Patterns

### Pattern 1: Barcode Scan Pattern (45+ pages)

```mermaid
sequenceDiagram
    participant User
    participant Screen as Any Page with Barcode Input
    participant Service
    participant DB as Database

    Note over Screen: Page loads
    Screen->>Screen: Set focus to barcode TextBox

    loop Until user closes page
        User->>Screen: Scan barcode OR type + Enter

        alt Barcode not empty
            Screen->>Screen: Trim whitespace
            Screen->>Service: GetByBarcode(barcode)
            Service->>DB: sp_[Module]_GetByBarcode

            alt Data found
                DB-->>Service: Entity data
                Service-->>Screen: Entity
                Screen->>Screen: Display entity details in form/grid
                Screen->>Screen: Enable action buttons
            else Data not found
                DB-->>Service: No results
                Service-->>Screen: Null/Empty
                Screen->>Screen: Show "Not found" message
                Screen->>Screen: Disable action buttons
            end

            Screen->>Screen: Clear barcode TextBox
            Screen->>Screen: Set focus back to barcode
        else Barcode empty
            Screen->>Screen: Do nothing, stay focused
        end
    end
```

### Pattern 2: Report Parameter Selection Pattern (40+ pages)

```mermaid
sequenceDiagram
    participant User
    participant Screen as Report Screen
    participant Service
    participant ReportViewer
    participant DB as Database

    User->>Screen: Open report page
    Screen->>Service: GetParameterOptions()
    Service->>DB: Load dropdown data<br/>(machines, employees, products, etc.)
    DB-->>Service: Lookup data
    Service-->>Screen: Options
    Screen->>Screen: Populate parameter controls

    User->>Screen: Select From Date
    User->>Screen: Select To Date
    User->>Screen: Select other parameters<br/>(machine, dept, product, etc.)
    User->>Screen: Click Generate Report

    Screen->>Screen: Validate parameters

    alt Validation passed
        Screen->>Service: GetReportData(fromDate, toDate, params...)
        Service->>DB: sp_[Module]_GetReportData
        DB-->>Service: Report dataset
        Service-->>Screen: DataTable/List

        Screen->>ReportViewer: Set ReportPath = "Reports/[ReportName].rdlc"
        Screen->>ReportViewer: Clear DataSources
        Screen->>ReportViewer: Add DataSource("DataSet1", data)
        Screen->>ReportViewer: RefreshReport()
        ReportViewer->>ReportViewer: Render report
        ReportViewer->>Screen: Display report

        alt Export to PDF
            User->>ReportViewer: Click Export → PDF
            ReportViewer->>ReportViewer: Generate PDF
            ReportViewer->>User: Download/Save PDF file
        else Export to Excel
            User->>ReportViewer: Click Export → Excel
            ReportViewer->>ReportViewer: Generate Excel
            ReportViewer->>User: Download/Save Excel file
        else Print
            User->>ReportViewer: Click Print
            ReportViewer->>ReportViewer: Show print dialog
            User->>ReportViewer: Confirm print
            ReportViewer->>User: Send to printer
        end
    else Validation failed
        Screen->>Screen: Show validation errors
        Screen->>Screen: Highlight invalid fields
    end
```

### Pattern 3: Production Entry Pattern (35+ pages)

```mermaid
graph TD
    START[Open Production Entry Page] --> LOAD_MASTER[Load Master Data<br/>Machines, Operators, Shifts]
    LOAD_MASTER --> SELECT_MACHINE[User Selects Machine]
    SELECT_MACHINE --> SELECT_OPERATOR[User Selects Operator]
    SELECT_OPERATOR --> SCAN_MATERIAL[User Scans Material Barcode]

    SCAN_MATERIAL --> VALIDATE{Validate Material}
    VALIDATE -->|Invalid| ERROR[Show Error Message]
    ERROR --> SCAN_MATERIAL
    VALIDATE -->|Valid| DISPLAY[Display Material Info]

    DISPLAY --> ENTER_PARAMS[User Enters Parameters<br/>Target qty, spec, etc.]
    ENTER_PARAMS --> CLICK_START[User Clicks Start]

    CLICK_START --> INSERT_PROD[Insert Production Record<br/>Status = Started]
    INSERT_PROD --> START_PLC[Send Start to PLC<br/>if applicable]
    START_PLC --> MONITOR[Monitor Production]

    MONITOR --> CHECK_STATUS{Production Status}
    CHECK_STATUS -->|Running| UPDATE_DISPLAY[Update Display<br/>Counter, Speed, etc.]
    UPDATE_DISPLAY --> MONITOR

    CHECK_STATUS -->|User Clicks Stop| STOP_PROD[Stop Production<br/>Status = Stopped]
    STOP_PROD --> MONITOR

    CHECK_STATUS -->|User Clicks Complete| ENTER_FINAL[Enter Final Quantities]
    ENTER_FINAL --> CALC[Calculate Consumption<br/>Efficiency, Waste]
    CALC --> UPDATE_INVENTORY[Update Inventory<br/>Reduce Raw, Add Finished]
    UPDATE_INVENTORY --> PRINT_LABEL[Print Product Labels]
    PRINT_LABEL --> COMPLETE[Update Status = Completed]
    COMPLETE --> RESET[Reset Form]
    RESET --> START

    style START fill:#e1f5ff
    style MONITOR fill:#ffe1e1
    style COMPLETE fill:#e1ffe1
```

---

## Cross-Module Navigation Flows

### Material Traceability Flow (Crosses Multiple Modules)

```mermaid
graph LR
    USER[User in Any Module] --> TRACE_PAGE[Open Traceability Page]
    TRACE_PAGE --> SCAN[Scan Any Barcode<br/>Yarn/Beam/Roll/Product]

    SCAN --> IDENTIFY{Identify<br/>Barcode Type}

    IDENTIFY -->|Yarn Lot| YARN_TRACE[Show Yarn Forward Trace]
    IDENTIFY -->|Warp Beam| BEAM_TRACE[Show Beam Forward Trace]
    IDENTIFY -->|Fabric Roll| ROLL_TRACE[Show Roll Forward/Backward]
    IDENTIFY -->|Final Product| PRODUCT_TRACE[Show Product Backward Trace]

    YARN_TRACE --> DISPLAY_TREE[Display TreeView<br/>Recursive Hierarchy]
    BEAM_TRACE --> DISPLAY_TREE
    ROLL_TRACE --> DISPLAY_TREE
    PRODUCT_TRACE --> DISPLAY_TREE

    DISPLAY_TREE --> ACTIONS{User Action}
    ACTIONS -->|Click Node| SHOW_DETAILS[Show Detail Dialog<br/>for Selected Item]
    ACTIONS -->|Export| EXPORT_EXCEL[Export to Excel]
    ACTIONS -->|Print| PRINT_TRACE[Print Traceability Report]

    style TRACE_PAGE fill:#e1f5ff
    style DISPLAY_TREE fill:#e1ffe1
```

### D365 Integration Flow (M19)

```mermaid
sequenceDiagram
    participant User
    participant Screen as D365 Sync Screen
    participant Service as Integration Service
    participant LocalDB as Local Oracle DB
    participant D365DB as D365 SQL Server

    User->>Screen: Open D365 Integration
    Screen->>Service: GetSyncStatus()
    Service->>LocalDB: Get last sync timestamp
    LocalDB-->>Service: Last sync date/time
    Service-->>Screen: Display sync status

    alt Manual Sync
        User->>Screen: Select entity type<br/>(Products, Customers, Orders)
        User->>Screen: Click Sync button

        Screen->>Service: SyncEntity(entityType)
        Service->>LocalDB: Get changed records since last sync
        LocalDB-->>Service: Changed data

        Service->>D365DB: Send data via SQL connection
        D365DB-->>Service: Acknowledgment

        Service->>D365DB: Get updates from D365
        D365DB-->>Service: D365 changed data

        Service->>LocalDB: Update with D365 data
        LocalDB-->>Service: Update complete

        Service->>LocalDB: Update sync timestamp
        Service-->>Screen: Sync completed
        Screen->>Screen: Show success + record counts

    else Automatic Sync (Timer)
        loop Every 15 minutes
            Service->>Service: Auto-sync triggered
            Service->>LocalDB: Get changes
            Service->>D365DB: Bidirectional sync
            Service->>LocalDB: Update data + timestamp
            Service->>Screen: Update status display
        end
    end

    alt Sync Error
        D365DB-->>Service: Connection error
        Service-->>Screen: Error message
        Screen->>Screen: Show error dialog
        Service->>LocalDB: Log error
        Screen->>Screen: Enable Retry button
    end
```

---

## Summary of UI Navigation Patterns

### Page Type Distribution

| Pattern | Page Count | Modules | Key Features |
|---------|------------|---------|--------------|
| Barcode Scan Entry | 45 | All production modules | Focus on barcode, scan loop, clear & refocus |
| CRUD List | 85 | All modules | DataGrid, Add/Edit/Delete buttons, search |
| Production Entry | 35 | M02, M05, M06, M11 | Machine select, material scan, start/stop/complete |
| Report Generation | 40 | All modules | Parameter selection, generate, export |
| PLC Monitor | 12 | M02, M05, M06 | Real-time polling, status display, alarms |
| Traceability View | 15 | M01, M02, M05, M08, M13 | TreeView, recursive query, export |
| Dashboard/Summary | 18 | All modules | Cards/panels, navigation buttons, KPIs |

### Common Navigation Sequences

1. **Dashboard → List → Detail/Edit → Back to List**
2. **Dashboard → Production Entry → Monitor → Complete → Dashboard**
3. **Dashboard → Scan Entry → Display → Action → Scan Next**
4. **Dashboard → Report → Parameters → Generate → Export**
5. **Any Page → Traceability → TreeView → Detail → Close**

---

**Document Version**: 1.0
**Last Updated**: 2025-10-05
**Total Diagrams**: 22 (15 sequence, 7 graph)
**Coverage**: All 21 modules + common patterns
