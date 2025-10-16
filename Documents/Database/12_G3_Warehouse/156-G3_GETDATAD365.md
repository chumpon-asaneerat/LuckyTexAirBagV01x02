# G3_GETDATAD365

**Procedure Number**: 156 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert/update yarn/material receiving data from D365 ERP system |
| **Operation** | INSERT/UPDATE |
| **Tables** | tblG3Stock, tblG3Receiving (integration with D365) |
| **Called From** | G3DataService.cs:637 → G3_GETDATAD365() |
| **Frequency** | High (D365 integration) |
| **Performance** | Medium |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `DTTRA` | DATE | ⚠️ | Transaction date from D365 |
| `DTINP` | DATE | ⚠️ | Input/entry date from D365 |
| `CDCON` | VARCHAR2(50) | ✅ | Container/Pallet number (D365 field) |
| `BLELE` | NUMBER | ⚠️ | Balance/element quantity |
| `CDUM0` | VARCHAR2(50) | ⚠️ | Unit of measure code (D365 field) |
| `CDKE1` | VARCHAR2(50) | ✅ | Item code/Key 1 from D365 (ITMD365) |
| `CDKE2` | VARCHAR2(50) | ⚠️ | Item code/Key 2 from D365 (additional key) |
| `CDLOT` | VARCHAR2(50) | ⚠️ | Lot number from D365 |
| `CDQUA` | VARCHAR2(50) | ⚠️ | Quality code from D365 |
| `TECU1` | NUMBER | ⚠️ | Text/custom field 1 (numeric) |
| `TECU2` | NUMBER | ⚠️ | Text/custom field 2 (numeric) |
| `TECU3` | NUMBER | ⚠️ | Text/custom field 3 (numeric) |
| `TECU4` | NUMBER | ⚠️ | Text/custom field 4 (numeric) |
| `TECU5` | NUMBER | ⚠️ | Text/custom field 5 (numeric) |
| `TECU6` | VARCHAR2(50) | ⚠️ | Text/custom field 6 (text) |

### Output (OUT)

None

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `R_RESULT` | VARCHAR2(50) | Success/failure message or error description |

---

## Business Logic (What it does and why)

**Purpose**: Receives yarn/material data from Microsoft D365 (Dynamics 365) ERP system and inserts/updates in G3 warehouse stock. This is the modern ERP integration replacing AS400, synchronizing material receiving between D365 and MES.

**When Used**:
- D365 ERP records material receipt
- Automatic data sync from D365 to MES G3 warehouse
- Material receiving integration workflow
- Scheduled batch synchronization jobs
- Real-time receiving updates from D365
- Modern ERP integration (replacement for AS400)

**Business Rules**:
1. **Required D365 Fields**: CDCON (Container/Pallet) and CDKE1 (Item Code) must be provided
2. **D365 Field Naming**: Uses D365 standard field codes (same pattern as AS400)
3. **CDKE2 Addition**: D365 supports additional item key (CDKE2) not in AS400 version
4. **Error Handling**: Returns descriptive error message if insert fails
5. **Integration Pattern**: MES is slave system - D365 is master for receiving data
6. **Custom Fields**: TECU1-TECU6 allow flexible data capture from D365

**Workflow**:

**Scenario 1: Automatic D365 Material Receipt Sync**
1. Warehouse receives yarn shipment
2. Receiving clerk enters in D365 system:
   - CDCON = "PLT-D365-001234" (Pallet number)
   - CDKE1 = "YARN-PA66-001" (Primary item code in D365)
   - CDKE2 = "YARN-SUPPLIER-001" (Supplier item code)
   - DTTRA = 2024-01-15 (Transaction date)
   - BLELE = 500.00 (Quantity in kg)
   - CDUM0 = "KG" (Unit of measure)
   - CDLOT = "LOT-2024-0015" (Supplier lot)
   - TECU1-TECU5 = Additional measurements
3. D365 integration service calls G3_GETDATAD365
4. Procedure inserts/updates in G3 warehouse stock
5. Returns R_RESULT = "SUCCESS" or error message
6. Material now available in MES for production

**Scenario 2: D365 Scheduled Batch Sync**
1. Scheduled job runs every 10 minutes (more frequent than AS400)
2. Retrieves all new D365 receiving transactions via API
3. For each transaction, calls G3_GETDATAD365
4. D365 data parameters passed:
   - All mandatory fields (CDCON, CDKE1)
   - Optional CDKE2 for cross-reference
   - Optional fields (dates, quantities, lot info)
5. Procedure processes each record
6. Returns success/failure for each
7. Integration dashboard shows D365 sync status

**Scenario 3: Failed Insert - Validation Error**
1. D365 sends pallet data with missing required mapping
2. CDCON = "PLT-D365-001234"
3. CDKE1 = "YARN-UNKNOWN" (item not in MES master data)
4. Procedure attempts insert/update
5. Error occurs (item validation failure)
6. Returns: "Can't Insert PALLETNO = PLT-D365-001234 , ITMD365 = YARN-UNKNOWN"
7. Integration service logs error
8. Administrator maps D365 item to MES item

**D365 vs AS400 Differences**:
- **CDKE2**: D365 has additional key field (AS400 has only CDKE1)
- **Integration Frequency**: D365 typically syncs more frequently (10 min vs 15 min)
- **Error Messages**: Uses "ITMD365" instead of "ITM400" in error messages
- **Field Structure**: Otherwise identical to AS400 integration

**D365 Field Code Meanings**:
- **DT*** = Date fields (DTTRA=Transaction, DTINP=Input)
- **CD*** = Code fields (CDCON=Container, CDKE1/2=Keys, CDLOT=Lot, CDQUA=Quality, CDUM0=UoM)
- **BL*** = Balance/Quantity fields (BLELE=Element balance)
- **TE*** = Text/Custom fields (TECU1-6=Custom fields 1-6)

**Why This Matters**:
- **Modern ERP**: D365 is Microsoft's modern ERP platform replacing legacy AS400
- **Dual Integration**: System supports both AS400 (legacy) and D365 (modern) simultaneously
- **Data Consistency**: Single source of truth (D365) prevents discrepancies
- **Real-Time Availability**: Received materials immediately available for production planning
- **Cross-Reference**: CDKE2 enables supplier/customer item code mapping
- **Future-Proof**: D365 integration positions system for long-term ERP strategy

---

## Related Procedures

**Upstream**:
- D365 receiving transactions (external system)
- D365 integration service/API
- D365 batch scheduler

**Downstream**:
- G3_SEARCHYARNSTOCK (checks received stock)
- G3_GETPALLETDETAIL (views pallet details)
- G3_INSERTUPDATEISSUEYARN (issues received materials)

**Similar**:
- G3_GETDATAAS400 (AS400 ERP integration - identical pattern, different ERP)
- G3_RECEIVEYARN (manual receiving entry)
- G3_INSERTYARN (manual insert)

**D365 Integration Module**:
- D365_* procedures (M19 module - comprehensive D365 integration)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_GETDATAD365(DateTime? DTTRA, DateTime? DTINP, string CDCON, decimal? BLELE, string CDUM0, string CDKE1, string CDKE2, string CDLOT, string CDQUA, decimal? TECU1, decimal? TECU2, decimal? TECU3, decimal? TECU4, decimal? TECU5, string TECU6)`
**Lines**: 637-687

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_GETDATAD365(G3_GETDATAD365Parameter para)`
**Lines**: 7024-7051 (Parameter: 7024-7041, Result: 7047-7050)

---

**File**: 156/296 | **Progress**: 52.7%
