# LuckyTex AirBag MES - Workflow Documentation

## Main Production Flow

```mermaid
graph TD
    A[RAW MATERIALS] --> B[01-WAREHOUSE<br/>Yarn Receiving/Storage]
    B --> C[02-WARPING<br/>Warp beam preparation]
    C --> D[03-BEAMING<br/>Combine warp beams]
    D --> E[04-DRAWING<br/>Thread through heddles]
    E --> F[05-WEAVING<br/>Fabric production]
    F --> G[06-FINISHING<br/>Coating Process]
    G --> H[08-INSPECTION<br/>Quality Inspection]
    H --> I[11-CUT & PRINT<br/>Cutting and Printing]
    I --> J[12-G3<br/>Fabric/Silicone Receiving]
    J --> K[13-PACKING<br/>Packing Operations]
    K --> L[FINISHED GOODS]

    style A fill:#e1f5ff
    style L fill:#e1f5ff
    style B fill:#fff4e1
    style C fill:#fff4e1
    style D fill:#fff4e1
    style E fill:#fff4e1
    style F fill:#ffe1e1
    style G fill:#ffe1e1
    style H fill:#e1ffe1
    style I fill:#e1ffe1
    style J fill:#e1ffe1
    style K fill:#e1ffe1
```

---

## Module 01 - Warehouse

### Yarn Receiving Workflow

**Business Logic Sequence**:

```mermaid
sequenceDiagram
    participant UI as User Interface
    participant BL as Business Logic
    participant DB as Database
    participant Printer as Label Printer

    UI->>BL: Scan yarn lot barcode
    BL->>BL: Validate barcode format
    BL->>DB: sp_LuckyTex_Yarn_GetByBarcode

    alt PO exists and is open
        DB-->>BL: PO details (supplier, yarn type, qty)
        BL-->>UI: Display PO information

        UI->>BL: Enter actual quantity received
        BL->>BL: Compare received qty vs PO qty

        alt Quantity variance acceptable
            UI->>BL: Confirm receipt
            BL->>BL: Generate internal lot number
            BL->>DB: sp_LuckyTex_Yarn_Receive
            BL->>DB: sp_LuckyTex_Inventory_Update
            DB-->>BL: Receipt confirmed
            BL->>Printer: Print barcode label
            Printer-->>BL: Label printed
            BL-->>UI: Receipt complete
        else Quantity variance too large
            BL-->>UI: Warning: Quantity mismatch
            UI->>BL: User override or reject
        end
    else PO not found or closed
        DB-->>BL: No data / PO closed
        BL-->>UI: Error: Invalid PO
    end
```

**Database Operations**:
- `sp_LuckyTex_Yarn_GetByBarcode` - Lookup yarn lot
- `sp_LuckyTex_Yarn_Receive` - Insert receipt record
- `sp_LuckyTex_Inventory_Update` - Update stock levels

**Output**: Yarn lot stored in warehouse with tracking barcode

---

## Module 02 - Warping

### Warping Production Workflow

```mermaid
graph TD
    A[Start] --> B[Scan Warp Beam Barcode]
    B --> C{Beam Exists?}
    C -->|No| D[Create New Beam Record]
    C -->|Yes| E[Load Beam Details]
    D --> E
    E --> F[Load Creel - Scan Yarn Lots]
    F --> G{All Positions<br/>Loaded?}
    G -->|No| F
    G -->|Yes| H[Select Machine]
    H --> I[Select Operator]
    I --> J[Start Production]
    J --> K[PLC Starts Warping]
    K --> L{Monitor Status}
    L -->|Running| M[Update Meter Counter]
    M --> L
    L -->|Complete| N[Enter Final Length]
    N --> O[Calculate Yarn Consumption]
    O --> P[Update Inventory]
    P --> Q[Print Beam Label]
    Q --> R[End]

    L -->|Error/Break| S[Record Defect]
    S --> T{Continue?}
    T -->|Yes| L
    T -->|No| U[Mark Incomplete]
    U --> R
```

### Creel Loading Details

**Business Logic Sequence**:

```mermaid
sequenceDiagram
    participant Operator
    participant UI as Creel Loading UI
    participant BL as Business Logic
    participant DB as Database

    Operator->>UI: Open Creel Loading (beam ID)
    UI->>BL: Get beam specification
    BL->>DB: sp_LuckyTex_Warping_GetBeamSpec
    DB-->>BL: Beam spec (yarn type, total positions: 800)
    BL-->>UI: Display specification

    loop For each position (1-800)
        Operator->>UI: Enter position number
        Operator->>UI: Scan yarn lot barcode

        UI->>BL: Validate yarn for creel
        BL->>DB: sp_LuckyTex_Warping_ValidateYarn(beamId, barcode, position)

        alt Yarn valid (type matches, sufficient qty, no duplicate position)
            DB-->>BL: Yarn details + validation OK
            BL-->>UI: Display yarn info (type, color, supplier, qty available)

            Operator->>UI: Confirm load position
            UI->>BL: Load creel position
            BL->>DB: sp_LuckyTex_Warping_LoadCreel(beamId, position, barcode)
            BL->>DB: sp_LuckyTex_Inventory_Reserve(lotNumber, required qty)
            DB-->>BL: Position loaded
            BL-->>UI: Position marked as loaded (green)
            UI->>UI: Move to next position
        else Invalid yarn type
            DB-->>BL: Error: Yarn type mismatch
            BL-->>UI: Error: Wrong yarn type for this beam
        else Insufficient quantity
            DB-->>BL: Error: Insufficient quantity
            BL-->>UI: Error: Not enough yarn in lot
        else Duplicate position
            DB-->>BL: Error: Position already loaded
            BL-->>UI: Error: Position already used
        end
    end

    UI->>BL: Check all positions loaded
    BL->>BL: Verify 800/800 positions complete
    BL->>DB: sp_LuckyTex_Warping_UpdateBeamStatus(READY_FOR_PRODUCTION)
    DB-->>BL: Beam status updated
    BL-->>UI: Creel loading complete
```

**Database Operations**:
- `sp_LuckyTex_Warping_GetBeamSpec` - Get beam specifications
- `sp_LuckyTex_Warping_ValidateYarn` - Validate yarn compatibility
- `sp_LuckyTex_Warping_LoadCreel` - Insert creel loading record
- `sp_LuckyTex_Inventory_Reserve` - Reserve yarn quantity

### PLC Integration (Warping)

```mermaid
graph LR
    A[WPF Application] -->|Modbus TCP| B[PLC Controller]
    B -->|Read Registers| C[Machine Status]
    B -->|Read Registers| D[Meter Counter]
    B -->|Read Registers| E[Speed RPM]
    B -->|Read Registers| F[Alarm Status]

    A -->|Write Register| G[Start Command]
    A -->|Write Register| H[Stop Command]
    A -->|Write Register| I[Reset Command]

    C --> J[Update UI Status]
    D --> J
    E --> J
    F --> J
```

**PLC Data Points**:
- Register 1000: Machine status (0=Stopped, 1=Running, 2=Error)
- Register 1001: Meter counter (accumulated length)
- Register 1002: Current speed (RPM)
- Register 1010-1019: Alarm codes
- Register 2000: Control command (0=Stop, 1=Start, 2=Reset)

**Polling Interval**: 2 seconds

---

## Module 03 - Beaming

### Beaming Workflow

**Business Logic Sequence**:

```mermaid
sequenceDiagram
    participant Operator
    participant UI as Beaming UI
    participant BL as Business Logic
    participant DB as Database
    participant Printer

    Operator->>UI: Open Beaming page
    UI->>UI: Clear beam list

    loop Scan source beams (12-24 beams typical)
        Operator->>UI: Scan warp beam barcode
        UI->>BL: Get beam details
        BL->>DB: sp_LuckyTex_Beaming_GetBeam
        DB-->>BL: Beam info (yarn type, count, length)
        BL-->>UI: Display beam in list

        UI->>BL: Validate against first beam
        alt First beam
            BL-->>UI: Set as reference beam
        else Subsequent beams
            BL->>BL: Check yarn type matches
            BL->>BL: Check length within ±5%

            alt Compatible
                BL-->>UI: Add to beam list (green)
            else Incompatible
                BL-->>UI: Show error (red), remove from list
            end
        end
    end

    Operator->>UI: Generate combined beam ID
    Operator->>UI: Select machine
    Operator->>UI: Click Start Beaming

    UI->>BL: Validate all beams
    BL->>DB: sp_LuckyTex_Beaming_ValidateBeams(beamList)

    alt All beams valid
        DB-->>BL: Validation passed
        BL->>BL: Calculate total length = sum(beam lengths)
        BL->>DB: sp_LuckyTex_Beaming_Create(combinedBeamId, sourceBeams, totalLength)
        BL->>DB: sp_LuckyTex_Beaming_ConsumeSource(mark source beams as used)
        DB-->>BL: Combined beam created
        BL->>Printer: Print combined beam label
        Printer-->>BL: Label printed
        BL-->>UI: Beaming complete
        UI->>UI: Display success message
    else Validation failed
        DB-->>BL: Error: Incompatible beams
        BL-->>UI: Show error, cannot proceed
    end
```

**Compatibility Rules**:
- All beams must have same yarn type and count
- All beams must have similar length (±5%)
- Calculate total length of combined beam
- Source beams marked as consumed

**Database Operations**:
- `sp_LuckyTex_Beaming_GetBeam` - Get beam details
- `sp_LuckyTex_Beaming_ValidateBeams` - Check compatibility
- `sp_LuckyTex_Beaming_Create` - Create combined beam record
- `sp_LuckyTex_Beaming_ConsumeSource` - Mark source beams as used

---

## Module 04 - Drawing

### Drawing Workflow

**Business Logic Sequence**:

```mermaid
sequenceDiagram
    participant Operator
    participant UI as Drawing UI
    participant BL as Business Logic
    participant DB as Database

    Operator->>UI: Scan combined beam barcode
    UI->>BL: Get beam for drawing
    BL->>DB: sp_LuckyTex_Drawing_GetBeam

    alt Beam is from beaming and not drawn
        DB-->>BL: Beam details (yarn count, width, etc.)
        BL-->>UI: Display beam specifications

        Operator->>UI: Select loom number
        UI->>BL: Get threading pattern for loom
        BL->>DB: sp_LuckyTex_Drawing_GetPattern(loomNumber)
        DB-->>BL: Threading pattern (heddles/reeds configuration)
        BL-->>UI: Display pattern instructions

        Note over Operator: Manual work: Thread yarn<br/>through heddles and reeds<br/>following pattern

        Operator->>UI: Click Complete Drawing
        UI->>BL: Confirm drawing completion
        BL->>DB: sp_LuckyTex_Drawing_Complete(beamId, loomNumber, operator)
        BL->>DB: sp_LuckyTex_Drawing_AssignLoom(beamId, loomNumber)
        BL->>DB: Update beam status to READY_FOR_WEAVING
        DB-->>BL: Drawing complete
        BL-->>UI: Success - beam ready for weaving
        UI->>UI: Display next beam prompt
    else Invalid beam
        DB-->>BL: Error: Beam not from beaming or already drawn
        BL-->>UI: Show error message
    end
```

**Process Notes**:
- Validate beam is from beaming (not warping)
- Loom number determines threading pattern
- Manual operation - operator threads yarn through equipment
- Record operator and time (critical for traceability)

**Database Operations**:
- `sp_LuckyTex_Drawing_GetBeam` - Get beam details
- `sp_LuckyTex_Drawing_GetPattern` - Get threading pattern for loom
- `sp_LuckyTex_Drawing_Complete` - Update beam status
- `sp_LuckyTex_Drawing_AssignLoom` - Link beam to loom

---

## Module 05 - Weaving

### Weaving Production Workflow

```mermaid
graph TD
    A[Start] --> B[Scan Beam Barcode]
    B --> C{Beam Status = <br/>Ready for Weaving?}
    C -->|No| D[Error: Invalid Beam]
    D --> Z[End]
    C -->|Yes| E[Select Loom]
    E --> F[Load Beam on Loom]
    F --> G[Scan Weft Yarn Lot]
    G --> H[Enter Target Length]
    H --> I[Select Operator]
    I --> J[Start Weaving]
    J --> K[PLC Monitors Loom]
    K --> L{Status?}
    L -->|Running| M[Update Pick Counter]
    M --> N[Calculate Fabric Length]
    N --> L
    L -->|Stop Mark| O[Generate Fabric Roll]
    O --> P{Continue<br/>Weaving?}
    P -->|Yes| O
    P -->|No| Q[Complete Production]
    Q --> R[Calculate Totals]
    R --> S[Update Inventory]
    S --> T[Print Roll Labels]
    T --> Z

    L -->|Weft Break| U[Record Defect]
    U --> V[Operator Intervention]
    V --> L

    L -->|Warp Break| W[Critical Stop]
    W --> X[Inspection Required]
    X --> Y{Repairable?}
    Y -->|Yes| L
    Y -->|No| Q
```

### Fabric Roll Generation

**UI Flow**:
1. During weaving, operator presses "Stop Mark" button
2. System captures current meter reading
3. Generate unique roll barcode
4. Print roll label with:
   - Roll barcode
   - Beam number (traceability)
   - Length
   - Loom number
   - Date/time
   - Operator

**Business Logic**:
- Calculate roll length = Current meter - Last stop mark
- Typical roll length: 50-100 meters
- Each roll inherits beam's yarn traceability
- Weft yarn lot also tracked per roll

**Database Operations**:
- `sp_LuckyTex_Weaving_CreateRoll` - Insert fabric roll record
- `sp_LuckyTex_Weaving_UpdateMeter` - Update production meters
- `sp_LuckyTex_Traceability_Link` - Link roll to beam and yarn lots

### PLC Integration (Weaving)

```mermaid
graph TD
    A[PLC Controller] --> B[Pick Counter]
    A --> C[Speed Sensor]
    A --> D[Weft Break Sensor]
    A --> E[Warp Break Sensor]
    A --> F[Temperature Sensor]

    B --> G[WPF App - Modbus TCP]
    C --> G
    D --> G
    E --> G
    F --> G

    G --> H{Break Detected?}
    H -->|Weft Break| I[Stop Loom<br/>Log Defect]
    H -->|Warp Break| J[Emergency Stop<br/>Alert Operator]
    H -->|No| K[Update Dashboard]

    I --> K
    J --> K
```

---

## Module 06 - Finishing

### Coating/Heat-Setting Workflow

**UI Flow**:
1. Scan fabric roll barcode
2. Select process type (coating/heat-setting/both)
3. Select machine
4. Enter process parameters:
   - Temperature
   - Speed
   - Coating thickness (if coating)
5. Start process
6. Monitor real-time parameters via PLC
7. Complete and generate finished roll

**Business Logic**:
- Validate roll has passed inspection (if required)
- Process parameters must be within specification
- PLC monitors temperature ±2°C tolerance
- Record actual vs target parameters
- Generate new barcode for finished roll

**Database Operations**:
- `sp_LuckyTex_Finishing_ValidateRoll` - Check roll status
- `sp_LuckyTex_Finishing_StartProcess` - Insert process record
- `sp_LuckyTex_Finishing_UpdateParameters` - Log parameters (every 30 sec)
- `sp_LuckyTex_Finishing_Complete` - Generate finished roll

---

## Module 08 - Inspection

### Quality Inspection Workflow

**Business Logic Sequence**:

```mermaid
sequenceDiagram
    participant Inspector
    participant UI as Inspection UI
    participant BL as Business Logic
    participant DB as Database
    participant Printer

    Inspector->>UI: Scan fabric roll barcode
    UI->>BL: Get roll for inspection
    BL->>DB: sp_LuckyTex_Inspection_GetRoll

    alt Roll ready for inspection
        DB-->>BL: Roll details (length, beam, loom, date)
        BL->>DB: sp_LuckyTex_Inspection_GetStandard
        DB-->>BL: Quality standards & defect types
        BL-->>UI: Display roll info + defect entry form

        loop For each defect found
            Inspector->>UI: Enter defect (position, type, severity)
            UI->>BL: Validate defect
            BL->>BL: Calculate defect points<br/>(Minor=1, Major=3, Critical=10)
            BL-->>UI: Add to defect list
            UI->>UI: Update total points display
        end

        Inspector->>UI: Click Calculate Grade
        UI->>BL: Calculate final grade
        BL->>BL: Sum all defect points
        BL->>BL: Check for critical defects

        alt Has critical defects
            BL-->>UI: Grade = REJECT
            UI->>UI: Display as rejected (red)
            Inspector->>UI: Select action (Rework/Scrap)
            UI->>BL: Update roll status
            BL->>DB: sp_LuckyTex_Inspection_RecordDefects
            BL->>DB: sp_LuckyTex_Inspection_UpdateRollStatus(REJECT)
            DB-->>BL: Status updated
            BL-->>UI: Roll marked for rework/scrap
        else Points 0-10
            BL-->>UI: Grade = A
            Inspector->>UI: Approve roll
            UI->>BL: Approve with Grade A
            BL->>DB: sp_LuckyTex_Inspection_RecordDefects
            BL->>DB: sp_LuckyTex_Inspection_CalculateGrade
            BL->>DB: sp_LuckyTex_Inspection_UpdateRollStatus(APPROVED)
            BL->>DB: sp_LuckyTex_Inventory_UpdateGrade(A)
            DB-->>BL: Approved
            BL->>Printer: Print inspection certificate
            Printer-->>BL: Certificate printed
            BL-->>UI: Inspection complete
        else Points 11-20
            BL-->>UI: Grade = B
            Note over BL,Printer: Similar approval process
        else Points 21+
            BL-->>UI: Grade = C
            Note over BL,Printer: Similar approval process
        end

        UI->>UI: Reset form for next roll
    else Roll not found or already inspected
        DB-->>BL: Error / Already inspected
        BL-->>UI: Show error message
    end
```

**Grading Formula**:
- Defect points = Severity × Frequency
- Critical defects = automatic rejection
- Grade A: 0-10 points
- Grade B: 11-20 points
- Grade C: 21+ points
- Reject: Critical defects present

**Database Operations**:
- `sp_LuckyTex_Inspection_GetRoll` - Get roll details
- `sp_LuckyTex_Inspection_GetStandard` - Get quality standards
- `sp_LuckyTex_Inspection_RecordDefect` - Insert defect record
- `sp_LuckyTex_Inspection_CalculateGrade` - Calculate final grade
- `sp_LuckyTex_Inspection_UpdateRollStatus` - Update to Approved/Rejected

---

## Module 11 - Cut & Print

### Cutting Workflow

**UI Flow**:
1. Scan approved fabric roll
2. Enter customer order number
3. Display cutting plan (lengths and quantities)
4. Operator cuts fabric per plan
5. Generate cut piece barcodes
6. Print labels for each cut piece

**Business Logic**:
- Validate roll is Grade A or B
- Cutting plan optimizes material usage
- Each cut piece linked to parent roll (traceability)
- Update roll status to "Partially Used" or "Consumed"

**Database Operations**:
- `sp_LuckyTex_Cut_GetCuttingPlan` - Get order cutting requirements
- `sp_LuckyTex_Cut_GeneratePieces` - Create cut piece records
- `sp_LuckyTex_Cut_UpdateRollBalance` - Update remaining length

---

## Module 13 - Packing

### Packing Workflow

**UI Flow**:
1. Scan customer order number
2. Display required cut pieces
3. Scan each cut piece barcode to confirm
4. System validates against order
5. Enter packing details:
   - Box number
   - Quantity per box
6. Generate packing list
7. Print shipping label

**Business Logic**:
- All pieces must match order specifications
- Verify total quantity matches order
- Generate packing list with full traceability
- Update order status to "Packed"

**Database Operations**:
- `sp_LuckyTex_Packing_GetOrder` - Get order details
- `sp_LuckyTex_Packing_ValidatePiece` - Check piece belongs to order
- `sp_LuckyTex_Packing_Complete` - Create shipment record
- `sp_LuckyTex_Packing_GenerateDocument` - Generate packing list

---

## Data Entity Flow

```mermaid
graph LR
    A[Yarn Lot] --> B[Warp Beam]
    B --> C[Combined Beam]
    C --> D[Fabric Roll - Grey]
    E[Weft Yarn Lot] --> D
    D --> F[Fabric Roll - Finished]
    F --> G[Cut Piece]
    G --> H[Packed Box]
    H --> I[Shipment]

    style A fill:#e1f5ff
    style E fill:#e1f5ff
    style I fill:#e1ffe1
```

---

## Traceability Chain

### Forward Traceability (Yarn to Customer)

```mermaid
graph TD
    A[Yarn Lot Barcode] --> B{Query:<br/>Where Used?}
    B --> C[Warp Beam #12345]
    C --> D{Query:<br/>Beaming Record}
    D --> E[Combined Beam #67890]
    E --> F{Query:<br/>Weaving Record}
    F --> G[Fabric Rolls<br/>#001, #002, #003]
    G --> H{Query:<br/>Inspection & Finishing}
    H --> I[Approved Rolls<br/>#001, #002]
    I --> J{Query:<br/>Cutting Record}
    J --> K[Cut Pieces<br/>CP-001 to CP-010]
    K --> L{Query:<br/>Packing Record}
    L --> M[Customer Order<br/>CO-2025-001]
    M --> N[Shipment<br/>SH-2025-050]
```

### Backward Traceability (Customer to Yarn)

```mermaid
graph TD
    A[Customer Complaint<br/>Shipment SH-2025-050] --> B{Query:<br/>Packing Record}
    B --> C[Cut Pieces<br/>CP-001 to CP-010]
    C --> D{Query:<br/>Cutting Record}
    D --> E[Fabric Roll #001]
    E --> F{Query:<br/>Weaving Record}
    F --> G[Beam #67890<br/>+ Weft Lot W-789]
    G --> H{Query:<br/>Beaming Record}
    H --> I[Source Beams<br/>#12345, #12346, ...]
    I --> J{Query:<br/>Warping Record}
    J --> K[Creel Loading<br/>800 Yarn Lots]
    K --> L[Yarn Lot Numbers<br/>Supplier Info<br/>Receipt Dates]
```

---

## PLC Integration Patterns

### Serial Communication (RS-232) - Legacy Equipment

```mermaid
sequenceDiagram
    participant WPF as WPF Application
    participant Serial as SerialPort
    participant PLC as PLC Controller

    WPF->>Serial: Open COM3, 9600 baud
    Serial->>PLC: Connection Established

    loop Every 5 seconds
        WPF->>Serial: Send Read Command
        Serial->>PLC: Read Status
        PLC-->>Serial: Status Data (20 bytes)
        Serial-->>WPF: Parse Data
        WPF->>WPF: Update UI
    end

    WPF->>Serial: Send Start Command
    Serial->>PLC: Write Start Register
    PLC-->>Serial: Acknowledgment
    Serial-->>WPF: Command Confirmed
```

### Modbus TCP Communication - Modern Equipment

```mermaid
sequenceDiagram
    participant WPF as WPF Application
    participant Modbus as ModbusClient
    participant PLC as PLC via Ethernet

    WPF->>Modbus: Connect(192.168.1.100, Port 502)
    Modbus->>PLC: TCP Connection
    PLC-->>Modbus: Connected

    loop Every 2 seconds
        WPF->>Modbus: ReadHoldingRegisters(1000, 20)
        Modbus->>PLC: Modbus Function Code 03
        PLC-->>Modbus: Register Values
        Modbus-->>WPF: int[] Data
        WPF->>WPF: Update Dashboard
    end

    WPF->>Modbus: WriteSingleRegister(2000, 1)
    Modbus->>PLC: Modbus Function Code 06
    PLC-->>Modbus: Success
    Modbus-->>WPF: Write Confirmed
```

---

## Common Workflow Patterns

### Pattern 1: Barcode Scan Workflow

[x] Focus on barcode textbox on page load
[x] User scans barcode (or types + Enter)
[x] Lookup in database via stored procedure
[x] Display results or error message
[x] Clear textbox and refocus for next scan

### Pattern 2: Production Start Workflow

[x] Select machine (dropdown or scan)
[x] Select operator (dropdown or employee card scan)
[x] Scan material barcode(s)
[x] Validate all inputs
[x] Insert production record with status = 'Started'
[x] Enable real-time monitoring (if PLC integrated)

### Pattern 3: Production Complete Workflow

[x] Enter final quantities
[x] Calculate consumption (actual vs theoretical)
[x] Record any defects or issues
[x] Update production record status = 'Completed'
[x] Update inventory (reduce raw material, increase finished goods)
[x] Print labels/documents
[x] Clear form for next production

### Pattern 4: Report Generation Workflow

[x] User selects report parameters (dates, filters, etc.)
[x] Validate parameter ranges
[x] Call stored procedure to get data
[x] Bind data to RDLC report template
[x] Display in ReportViewer control
[x] User can export (PDF, Excel) or print

---

## Cross-Module Dependencies

| Source Module | Target Module | Dependency Type |
|---------------|---------------|-----------------|
| M01 Warehouse | M02 Warping | Yarn lot availability |
| M02 Warping | M03 Beaming | Warp beam availability |
| M03 Beaming | M04 Drawing | Combined beam availability |
| M04 Drawing | M05 Weaving | Drawn beam ready status |
| M05 Weaving | M06 Finishing | Grey fabric roll availability |
| M06 Finishing | M08 Inspection | Finished roll availability |
| M08 Inspection | M11 Cut & Print | Approved roll status |
| M11 Cut & Print | M13 Packing | Cut piece availability |
| M05 Weaving | M02 Warping | Beam consumption (close loop) |
| All Modules | M17 Master Data | Machine, Employee, Shift data |

---

## Next Steps

[*] This document provides business logic understanding
[*] For implementation strategy, see MODERNIZATION_REFACTORING.md
[*] For codebase patterns, see MODERNIZATION_ANALYSIS.md
[*] For .NET Framework constraints, see DOTNET_FRAMEWORK_4.7.2_NOTES.md

---

**Document Version**: 1.0
**Last Updated**: 2025-10-05
**Status**: Complete
