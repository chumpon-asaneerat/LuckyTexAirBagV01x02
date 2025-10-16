# G3_GETDATAAS400

**Procedure Number**: 155 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert/update yarn/material receiving data from AS400 ERP system |
| **Operation** | INSERT/UPDATE |
| **Tables** | tblG3Stock, tblG3Receiving (integration with AS400) |
| **Called From** | G3DataService.cs:564 → G3_GETDATAAS400() |
| **Frequency** | High (AS400 integration) |
| **Performance** | Medium |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `DTTRA` | DATE | ⚠️ | Transaction date from AS400 |
| `DTINP` | DATE | ⚠️ | Input/entry date from AS400 |
| `CDCON` | VARCHAR2(50) | ✅ | Container/Pallet number (AS400 field) |
| `BLELE` | NUMBER | ⚠️ | Balance/element quantity |
| `CDUM0` | VARCHAR2(50) | ⚠️ | Unit of measure code (AS400 field) |
| `CDKE1` | VARCHAR2(50) | ✅ | Item code/Key 1 from AS400 (ITM400) |
| `CDLOT` | VARCHAR2(50) | ⚠️ | Lot number from AS400 |
| `CDQUA` | VARCHAR2(50) | ⚠️ | Quality code from AS400 |
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

**Purpose**: Receives yarn/material data from AS400 ERP system and inserts/updates in G3 warehouse stock. This is a critical integration procedure that synchronizes material receiving between AS400 and MES.

**When Used**:
- AS400 ERP records material receipt
- Automatic data sync from AS400 to MES G3 warehouse
- Material receiving integration workflow
- Scheduled batch synchronization jobs
- Real-time receiving updates from AS400

**Business Rules**:
1. **Required AS400 Fields**: CDCON (Container/Pallet) and CDKE1 (Item Code) must be provided
2. **AS400 Field Naming**: Uses AS400 standard field codes (DT=Date, CD=Code, BL=Balance, TE=Text)
3. **Error Handling**: Returns descriptive error message if insert fails
4. **Integration Pattern**: MES is slave system - AS400 is master for receiving data
5. **Custom Fields**: TECU1-TECU6 allow flexible data capture from AS400

**Workflow**:

**Scenario 1: Automatic AS400 Material Receipt Sync**
1. Warehouse receives yarn shipment
2. Receiving clerk enters in AS400 system:
   - CDCON = "PLT-AS400-001234" (Pallet number)
   - CDKE1 = "YARN-PA66-001" (Item code in AS400)
   - DTTRA = 2024-01-15 (Transaction date)
   - BLELE = 500.00 (Quantity in kg)
   - CDUM0 = "KG" (Unit of measure)
   - CDLOT = "LOT-2024-0015" (Supplier lot)
   - TECU1-TECU5 = Additional measurements
3. AS400 integration service calls G3_GETDATAAS400
4. Procedure inserts/updates in G3 warehouse stock
5. Returns R_RESULT = "SUCCESS" or error message
6. Material now available in MES for production

**Scenario 2: Batch Synchronization**
1. Scheduled job runs every 15 minutes
2. Retrieves all new AS400 receiving transactions
3. For each transaction, calls G3_GETDATAAS400
4. AS400 data parameters passed:
   - All mandatory fields (CDCON, CDKE1)
   - Optional fields (dates, quantities, lot info)
5. Procedure processes each record
6. Returns success/failure for each
7. Integration log records sync status

**Scenario 3: Failed Insert - Duplicate Entry**
1. AS400 sends pallet data already in MES
2. CDCON = "PLT-AS400-001234" (existing pallet)
3. CDKE1 = "YARN-PA66-001"
4. Procedure attempts insert/update
5. Error occurs (duplicate or validation failure)
6. Returns: "Can't Insert PALLETNO = PLT-AS400-001234 , ITM400 = YARN-PA66-001"
7. Integration service logs error
8. Administrator investigates mismatch

**AS400 Field Code Meanings**:
- **DT*** = Date fields (DTTRA=Transaction, DTINP=Input)
- **CD*** = Code fields (CDCON=Container, CDKE1=Key1/Item, CDLOT=Lot, CDQUA=Quality, CDUM0=UoM)
- **BL*** = Balance/Quantity fields (BLELE=Element balance)
- **TE*** = Text/Custom fields (TECU1-6=Custom fields 1-6)

**Why This Matters**:
- **ERP Integration**: Seamless data flow from AS400 to MES without manual entry
- **Data Consistency**: Single source of truth (AS400) prevents discrepancies
- **Real-Time Availability**: Received materials immediately available for production planning
- **Traceability**: Maintains link between AS400 receipt and MES stock
- **Audit Trail**: Complete record of AS400-originated material receipts

---

## Related Procedures

**Upstream**:
- AS400 receiving transactions (external system)
- AS400 integration service/scheduler

**Downstream**:
- G3_SEARCHYARNSTOCK (checks received stock)
- G3_GETPALLETDETAIL (views pallet details)
- G3_INSERTUPDATEISSUEYARN (issues received materials)

**Similar**:
- G3_GETDATAD365 (D365 ERP integration - same pattern)
- G3_RECEIVEYARN (manual receiving entry)
- G3_INSERTYARN (manual insert)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_GETDATAAS400(DateTime? DTTRA, DateTime? DTINP, string CDCON, decimal? BLELE, string CDUM0, string CDKE1, string CDLOT, string CDQUA, decimal? TECU1, decimal? TECU2, decimal? TECU3, decimal? TECU4, decimal? TECU5, string TECU6)`
**Lines**: 564-614

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_GETDATAAS400(G3_GETDATAAS400Parameter para)`
**Lines**: 7057-7084 (Parameter: 7057-7073, Result: 7079-7082)

---

**File**: 155/296 | **Progress**: 52.4%
