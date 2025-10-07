# Process: ERP Synchronization (D365 Integration)

**Process ID**: D365-001
**Module**: 19 - D365 Integration
**Priority**: P5 (Support Module)
**Created**: 2025-10-06

---

## 1. Process Overview

### Purpose
Synchronize production data between LuckyTex AirBag MES and Microsoft Dynamics 365 Finance & Operations ERP system. Enable bidirectional data exchange for customer orders, inventory levels, shipments, and production status to maintain data consistency across systems.

### Scope
- **Inbound (D365 → MES)**:
  - Customer order download
  - Product master data sync
  - Customer master data sync
- **Outbound (MES → D365)**:
  - Shipment confirmations
  - Inventory level updates
  - Production completion reports
- **Synchronization Management**:
  - Sync queue monitoring
  - Error handling and retry logic
  - Sync history tracking

### Module(s) Involved
- **Primary**: M19 - D365 Integration
- **Data Sources**: All production modules (especially Shipping, Warehouse)
- **Target System**: Microsoft Dynamics 365 F&O via SQL Server integration database

---

## 2. UI Files Inventory

### XAML Files
| File Path | Description | Purpose |
|-----------|-------------|---------|
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/SyncDashboardPage.xaml` | Sync monitoring dashboard | Monitor sync status |
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/SyncQueuePage.xaml` | Sync queue viewer | View pending syncs |
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/SyncHistoryPage.xaml` | Sync history | View completed syncs |
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/D365MenuPage.xaml` | Module menu | Navigation |

### Code-Behind Files
| File Path | Description |
|-----------|-------------|
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/SyncDashboardPage.xaml.cs` | Dashboard logic |
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/SyncQueuePage.xaml.cs` | Queue display |
| `LuckyTex.AirBag.Pages/Pages/19 - D365Integration/SyncHistoryPage.xaml.cs` | History display |

### Service Files
| File Path | Description |
|-----------|-------------|
| *(To be created)* `LuckyTex.AirBag.Core/Repositories/D365Repository.cs` | Repository (SQL Server integration DB) |
| *(To be created)* `LuckyTex.AirBag.Core/Services/D365IntegrationService.cs` | Service layer |
| *(To be created)* `LuckyTex.AirBag.Core/Services/SyncSchedulerService.cs` | Background sync scheduler |

---

## 3. UI Layout Description

### SyncDashboardPage.xaml

**Screen Title**: "D365 Integration Dashboard"

**Summary Cards**:
- Syncs pending (count)
- Syncs today (count)
- Sync errors (count)
- Last successful sync (timestamp)

**Sync Status Section**:
- **Inbound Syncs**:
  - Customer orders: Last sync time, Status (OK/Error)
  - Product master: Last sync, Status
  - Customer master: Last sync, Status
- **Outbound Syncs**:
  - Shipments: Last sync, Status
  - Inventory: Last sync, Status
  - Production: Last sync, Status

**Recent Activity DataGrid**:
- Columns: Sync Type, Direction (In/Out), Status, Record Count, Timestamp, Error Message
- Row click: View details

**Action Buttons**:
- `cmdSyncNow` - Manual sync trigger (dropdown: select sync type)
- `cmdViewQueue` - View sync queue
- `cmdViewHistory` - View sync history
- `cmdRefresh` - Refresh dashboard

### SyncQueuePage.xaml

**Screen Title**: "Sync Queue"

**Queue DataGrid**:
- Columns: Queue ID, Sync Type, Direction, Data Payload (summary), Status (Pending/Processing/Error), Created Date, Retry Count
- Row actions: View, Retry, Delete

**Action Buttons**:
- `cmdRetryAll` - Retry all failed syncs
- `cmdClearErrors` - Clear error records
- `cmdExport` - Export queue to Excel

### SyncHistoryPage.xaml

**Screen Title**: "Sync History"

**Filter Section**:
- Date range
- Sync type filter
- Status filter (All, Success, Error)
- `cmdSearch`

**History DataGrid**:
- Columns: Sync ID, Type, Direction, Records Processed, Status, Start Time, End Time, Duration (sec), Error Message

---

## 4. Component Architecture Diagram

```mermaid
graph TD
    UI1[SyncDashboardPage.xaml] --> CB1[CodeBehind]
    UI2[SyncQueuePage.xaml] --> CB2[CodeBehind]
    UI3[SyncHistoryPage.xaml] --> CB3[CodeBehind]

    CB1 --> SVC[ID365IntegrationService]
    CB2 --> SVC
    CB3 --> SVC

    SVC --> SCHED[ISyncSchedulerService<br/>Background Service]
    SVC --> REPO_D365[ID365Repository<br/>SQL Server Integration DB]
    SVC --> REPO_MES[MES Repositories<br/>Oracle Database]

    SCHED --> TIMER[Windows Service / Timer]

    REPO_D365 --> SQL[(SQL Server<br/>D365 Integration DB)]
    REPO_MES --> ORACLE[(Oracle Database<br/>MES)]

    SQL --> D365_STAGING[Staging Tables:<br/>- D365_CustomerOrders_In<br/>- D365_Shipments_Out<br/>- D365_Inventory_Out<br/>- D365_SyncQueue<br/>- D365_SyncHistory]

    SQL <--> D365[Microsoft Dynamics 365<br/>F&O via OData/DMF]

    style UI1 fill:#e1f5ff
    style UI2 fill:#e1f5ff
    style UI3 fill:#e1f5ff
    style SVC fill:#e1ffe1
    style REPO_D365 fill:#fff4e1
    style SQL fill:#ffe1e1
    style D365 fill:#ffe1ff
```

---

## 5. Workflow Diagram

```mermaid
graph TD
    START[Start: Background Sync Service] --> TIMER[Timer Trigger - Every 5 Minutes]

    TIMER --> CHECK_QUEUE{Check Sync Queue<br/>Pending Items?}
    CHECK_QUEUE -->|No| WAIT[Wait for Next Cycle]
    WAIT --> TIMER

    CHECK_QUEUE -->|Yes| PROCESS_QUEUE[Process Queue Items]
    PROCESS_QUEUE --> GET_ITEM[Get Next Pending Item]

    GET_ITEM --> CHECK_TYPE{Sync Type?}

    CHECK_TYPE -->|Inbound: Customer Order| PULL_ORDER[Pull Order from D365 Staging Table]
    PULL_ORDER --> MAP_ORDER[Map D365 Fields to MES Format]
    MAP_ORDER --> VALIDATE_ORDER{Data Valid?}
    VALIDATE_ORDER -->|No| LOG_ERROR1[Log Error, Mark Failed]
    LOG_ERROR1 --> MORE_QUEUE{More Items?}

    VALIDATE_ORDER -->|Yes| INSERT_MES[Insert Order into MES Oracle DB]
    INSERT_MES --> UPDATE_STATUS1[Mark Sync Success in Queue]
    UPDATE_STATUS1 --> MORE_QUEUE

    CHECK_TYPE -->|Outbound: Shipment| GET_SHIPMENT[Get Shipment Data from MES]
    GET_SHIPMENT --> MAP_SHIPMENT[Map MES Fields to D365 Format]
    MAP_SHIPMENT --> VALIDATE_SHIP{Data Valid?}
    VALIDATE_SHIP -->|No| LOG_ERROR2[Log Error, Mark Failed]
    LOG_ERROR2 --> MORE_QUEUE

    VALIDATE_SHIP -->|Yes| INSERT_D365[Insert into D365 Staging Table]
    INSERT_D365 --> TRIGGER_D365[Trigger D365 Import Job]
    TRIGGER_D365 --> UPDATE_STATUS2[Mark Sync Success in Queue]
    UPDATE_STATUS2 --> MORE_QUEUE

    CHECK_TYPE -->|Outbound: Inventory| SYNC_INV[Sync Inventory Levels]
    SYNC_INV --> MORE_QUEUE

    MORE_QUEUE -->|Yes| GET_ITEM
    MORE_QUEUE -->|No| LOG_HISTORY[Log Sync Batch to History]
    LOG_HISTORY --> WAIT

    style START fill:#e1f5ff
    style WAIT fill:#e1ffe1
    style LOG_ERROR1 fill:#ffe1e1
    style LOG_ERROR2 fill:#ffe1e1
```

---

## 6. Business Logic Sequence Diagram

```mermaid
sequenceDiagram
    participant Timer as Background Timer
    participant SCHED as SyncSchedulerService
    participant BL as D365IntegrationService
    participant REPO_D365 as D365Repository (SQL)
    participant REPO_MES as MES Repository (Oracle)
    participant SQL as SQL Server Integration DB
    participant ORACLE as Oracle MES Database
    participant D365 as Dynamics 365 F&O

    Note over Timer,D365: Inbound Sync: Customer Order

    Timer->>SCHED: Timer trigger (every 5 min)
    SCHED->>BL: ProcessSyncQueue()
    BL->>REPO_D365: GetPendingInboundSyncs()
    REPO_D365->>SQL: SELECT * FROM D365_SyncQueue WHERE direction='IN' AND status='Pending'
    SQL-->>REPO_D365: Pending sync items
    REPO_D365-->>BL: Sync queue items

    loop For each inbound sync
        alt Sync Type = Customer Order
            BL->>REPO_D365: GetD365CustomerOrders()
            REPO_D365->>SQL: SELECT * FROM D365_CustomerOrders_In WHERE synced=0
            Note over SQL: D365 populates this table via DMF export
            SQL-->>REPO_D365: Customer order data (D365 format)
            REPO_D365-->>BL: Order entities

            BL->>BL: MapD365OrderToMES(d365Order)
            Note over BL: Map fields:<br/>D365.SalesId → MES.OrderNumber<br/>D365.CustAccount → MES.CustomerCode<br/>D365.DeliveryDate → MES.RequiredDate

            BL->>BL: ValidateOrderData(mesOrder)

            alt Validation passed
                BL->>REPO_MES: InsertCustomerOrder(mesOrder)
                REPO_MES->>ORACLE: INSERT INTO tblCustomerOrder
                ORACLE-->>REPO_MES: Order created
                REPO_MES-->>BL: Success

                BL->>REPO_D365: UpdateSyncStatus(syncId, 'Success')
                REPO_D365->>SQL: UPDATE D365_SyncQueue SET status='Success'
                REPO_D365->>SQL: UPDATE D365_CustomerOrders_In SET synced=1
                SQL-->>REPO_D365: Updated

            else Validation failed
                BL->>BL: Log error details
                BL->>REPO_D365: UpdateSyncStatus(syncId, 'Error', errorMessage)
                REPO_D365->>SQL: UPDATE D365_SyncQueue SET status='Error', errorMsg=@msg
                SQL-->>REPO_D365: Updated
            end
        end
    end

    Note over Timer,D365: Outbound Sync: Shipment Confirmation

    Timer->>SCHED: Timer trigger
    SCHED->>BL: ProcessOutboundShipments()
    BL->>REPO_MES: GetShippedOrdersSinceLastSync(lastSyncDate)
    REPO_MES->>ORACLE: SELECT * FROM tblShipment WHERE shipDate > @lastSync AND d365Synced=0
    ORACLE-->>REPO_MES: Shipment records
    REPO_MES-->>BL: Shipments

    loop For each shipment
        BL->>BL: MapMESShipmentToD365(mesShipment)
        Note over BL: Map fields:<br/>MES.ShipmentNumber → D365.PackingSlipId<br/>MES.OrderNumber → D365.SalesId<br/>MES.TrackingNumber → D365.TrackingNumber

        BL->>REPO_D365: InsertD365ShipmentStaging(d365Shipment)
        REPO_D365->>SQL: INSERT INTO D365_Shipments_Out
        SQL-->>REPO_D365: Staging record created

        BL->>D365: TriggerD365ImportJob(jobName='ShipmentImport')
        Note over D365: D365 DMF job imports from staging table
        D365-->>BL: Import job queued

        BL->>REPO_MES: UpdateShipmentSyncStatus(shipmentId, synced=1)
        REPO_MES->>ORACLE: UPDATE tblShipment SET d365Synced=1
        ORACLE-->>REPO_MES: Updated

        BL->>REPO_D365: LogSyncHistory(syncType='Shipment', direction='OUT', status='Success')
        REPO_D365->>SQL: INSERT INTO D365_SyncHistory
        SQL-->>REPO_D365: History logged
    end

    BL-->>SCHED: Sync batch complete
    SCHED-->>Timer: Wait for next cycle
```

---

## 7. Data Flow

### Inbound Data (D365 → MES)

| D365 Entity | D365 Field | MES Table | MES Field | Transformation |
|-------------|------------|-----------|-----------|----------------|
| Sales Order | SalesId | tblCustomerOrder | OrderNumber | Direct mapping |
| Sales Order | CustAccount | tblCustomerOrder | CustomerCode | Lookup customer ID |
| Sales Order | DeliveryDate | tblCustomerOrder | RequiredDate | Date format conversion |
| Sales Order Line | ItemId | tblOrderLine | ProductCode | Product lookup |
| Sales Order Line | QtyOrdered | tblOrderLine | Quantity | Decimal |
| Customer | AccountNum | tblCustomer | CustomerCode | Direct mapping |
| Customer | Name | tblCustomer | CustomerName | Direct mapping |

### Outbound Data (MES → D365)

| MES Table | MES Field | D365 Entity | D365 Field | Transformation |
|-----------|-----------|-------------|------------|----------------|
| tblShipment | ShipmentNumber | Packing Slip | PackingSlipId | Direct mapping |
| tblShipment | OrderNumber | Sales Order | SalesId | Direct mapping |
| tblShipment | TrackingNumber | Packing Slip | TrackingNumber | Direct mapping |
| tblShipment | ShipDate | Packing Slip | DeliveryDate | Date format |
| tblInventory | YarnLotNumber | Inventory Journal | ItemId | Item mapping |
| tblInventory | QuantityOnHand | Inventory Journal | Qty | Decimal |

---

## 8. Database Operations

### SQL Server Integration Database

#### Staging Tables

**D365_CustomerOrders_In** (Inbound):
- SalesId, CustAccount, DeliveryDate, Status, Synced, CreatedDate

**D365_Shipments_Out** (Outbound):
- PackingSlipId, SalesId, TrackingNumber, ShipDate, Synced, CreatedDate

**D365_Inventory_Out** (Outbound):
- ItemId, Qty, TransDate, Synced, CreatedDate

**D365_SyncQueue**:
- SyncId, SyncType, Direction, Status, DataPayload, CreatedDate, ProcessedDate, RetryCount, ErrorMessage

**D365_SyncHistory**:
- HistoryId, SyncType, Direction, RecordCount, Status, StartTime, EndTime, ErrorMessage

### Stored Procedures

#### sp_D365_EnqueueSync
- **Purpose**: Add sync to queue
- **Parameters**: @SyncType, @Direction, @DataPayload
- **Returns**: Queue ID

#### sp_D365_ProcessQueue
- **Purpose**: Get pending queue items
- **Parameters**: @Direction (IN/OUT), @Limit
- **Returns**: Pending syncs

#### sp_D365_UpdateSyncStatus
- **Purpose**: Update sync status
- **Parameters**: @SyncId, @Status, @ErrorMessage
- **Returns**: Success flag

#### sp_D365_LogSyncHistory
- **Purpose**: Log sync to history
- **Parameters**: @SyncType, @Direction, @RecordCount, @Status, @ErrorMessage
- **Returns**: History ID

---

## 9. Implementation Checklist

### Phase 1: Infrastructure Setup
- [ ] Create SQL Server Integration Database
- [ ] Create staging tables (IN/OUT)
- [ ] Create sync queue and history tables
- [ ] Set up D365 DMF data entities
- [ ] Configure D365 OData endpoints

### Phase 2: Repository Layer
- [ ] Create `ID365Repository` interface (SQL Server)
  - [ ] EnqueueSync(sync) method
  - [ ] GetPendingSyncs(direction) method
  - [ ] UpdateSyncStatus(syncId, status, error) method
  - [ ] GetD365CustomerOrders() method (inbound)
  - [ ] InsertD365Shipment(shipment) method (outbound)
  - [ ] InsertD365Inventory(inventory) method (outbound)
  - [ ] LogSyncHistory(history) method
- [ ] Implement in `D365Repository`
- [ ] Unit tests

### Phase 3: Service Layer
- [ ] Create `ID365IntegrationService` interface
  - [ ] ProcessInboundOrders() method
  - [ ] ProcessOutboundShipments() method
  - [ ] ProcessOutboundInventory() method
  - [ ] EnqueueManualSync(syncType) method
  - [ ] GetSyncQueue() method
  - [ ] GetSyncHistory(filters) method
- [ ] Create `ISyncSchedulerService` interface
  - [ ] StartScheduler() method
  - [ ] StopScheduler() method
  - [ ] ProcessSyncQueue() method (background task)
- [ ] Implement services with field mapping
- [ ] Unit tests

### Phase 4: Background Service
- [ ] Create Windows Service or Timer-based background task
- [ ] Configure polling interval (default: 5 minutes)
- [ ] Error handling and retry logic (max 3 retries)
- [ ] Logging (to file and database)

### Phase 5: UI Refactoring
- [ ] Update `SyncDashboardPage.xaml.cs`
  - [ ] Inject ID365IntegrationService
  - [ ] Display sync status
  - [ ] Manual sync trigger
- [ ] Update `SyncQueuePage.xaml.cs`
  - [ ] Display queue
  - [ ] Retry functionality
- [ ] Update `SyncHistoryPage.xaml.cs`
  - [ ] Display history with filters

### Phase 6: Integration Testing
- [ ] Test inbound order sync (D365 → MES)
- [ ] Test outbound shipment sync (MES → D365)
- [ ] Test outbound inventory sync
- [ ] Test error handling and retry
- [ ] Test D365 DMF job triggering
- [ ] Performance testing (large batches)

### Phase 7: Deployment
- [ ] Deploy SQL Server Integration DB
- [ ] Deploy background sync service
- [ ] Configure D365 integration endpoints
- [ ] UAT with test D365 environment
- [ ] Production deployment
- [ ] Monitor first week of syncs

---

**Document Version**: 1.0
**Last Updated**: 2025-10-06
**Status**: Ready for Implementation
**Estimated Effort**: 5-7 days (complex integration)
