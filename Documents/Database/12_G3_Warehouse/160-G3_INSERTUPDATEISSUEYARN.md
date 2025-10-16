# G3_INSERTUPDATEISSUEYARN

**Procedure Number**: 160 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert or update yarn issue transaction (issue yarn from warehouse to production) |
| **Operation** | INSERT/UPDATE |
| **Tables** | tblG3IssueRequest, tblG3IssueDetail, tblG3Stock |
| **Called From** | G3DataService.cs:879 → G3_INSERTUPDATEISSUEYARN() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_REQUESTNO` | VARCHAR2(50) | ✅ | Issue request number |
| `P_PATTETNO` | VARCHAR2(50) | ✅ | Pallet number being issued |
| `P_TRACENO` | VARCHAR2(50) | ✅ | Trace number for traceability |
| `P_CH` | NUMBER | ⚠️ | Cone/cheese count issued |
| `P_WEIGHT` | NUMBER | ⚠️ | Weight quantity issued (kg) |
| `P_ISSUEDATE` | DATE | ⚠️ | Issue date/time |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator who issued yarn |
| `P_PALLETTYPE` | VARCHAR2(50) | ⚠️ | Pallet type classification |
| `P_ISSUETO` | VARCHAR2(50) | ⚠️ | Destination (machine/department code) |

### Output (OUT)

None - Returns success/failure result

### Returns (if cursor)

None - Procedure executes INSERT/UPDATE operation only

---

## Business Logic (What it does and why)

**Purpose**: Records yarn issuance from warehouse to production. This is the core transaction that transfers material from warehouse stock to production floor, updating inventory and creating traceability records.

**When Used**:
- Warehouse issues yarn to weaving machine
- Material requisition fulfillment
- Creating/updating issue request slip
- Batch issuing multiple pallets to production
- Issue correction/edit before finalization

**Business Rules**:
1. **Insert or Update**: Creates new issue record or updates existing one (upsert pattern)
2. **Required Fields**: REQUESTNO, PALLET NO, TRACENO, and OPERATOR must be provided
3. **Stock Reduction**: Reduces warehouse available stock (REMAINQTY)
4. **Traceability**: Links pallet/trace to production destination
5. **Request Grouping**: Multiple pallets can belong to one REQUEST NO

**Workflow**:

**Scenario 1: Issue Yarn to Weaving Machine**
1. Production supervisor requests yarn for machine WV-001
2. Warehouse operator creates issue request:
   - P_REQUESTNO = "REQ-2024-0015" (new request)
   - P_ISSUETO = "WV-001" (weaving machine)
   - P_OPERATOR = "WH-OP-125"
3. Operator scans pallet barcode "PLT-2024-001234"
4. System calls G3_INSERTUPDATEISSUEYARN:
   - P_PATTETNO = "PLT-2024-001234"
   - P_TRACENO = "TRACE-001234"
   - P_CH = 50 (cones)
   - P_WEIGHT = 500 (kg)
   - P_ISSUEDATE = Current timestamp
   - P_PALLETTYPE = "FULL"
5. Procedure inserts issue transaction
6. Stock reduced: PLT-2024-001234 REMAINQTY = 0
7. Issue slip printed for production

**Scenario 2: Multi-Pallet Issue (Batch)**
1. Large production order needs 3 pallets
2. One request number for all:
   - Request: REQ-2024-0020
   - Destination: WV-003
3. Operator issues 3 pallets sequentially:
   - Call 1: PLT-001, 500kg → Insert
   - Call 2: PLT-002, 480kg → Insert
   - Call 3: PLT-003, 520kg → Insert
4. All three recorded under same request
5. Total issued: 1500kg to machine WV-003

**Scenario 3: Partial Pallet Issue**
1. Production needs only 200kg
2. Full pallet has 500kg available
3. Operator issues partial quantity:
   - P_PATTETNO = "PLT-2024-005678"
   - P_WEIGHT = 200 (partial)
   - P_CH = 20 (partial cones)
4. Procedure inserts issue for 200kg
5. Stock updated: REMAINQTY = 300kg (remaining)
6. Pallet still available for future issues

**Scenario 4: Edit Issue Before Finalization**
1. Operator realizes wrong weight entered
2. Request REQ-2024-0025 already has 2 pallets
3. Need to correct Pallet PLT-001 weight:
   - P_REQUESTNO = "REQ-2024-0025" (existing)
   - P_PATTETNO = "PLT-001" (existing)
   - P_WEIGHT = 485 (corrected from 500)
4. Procedure **updates** existing record (upsert)
5. Stock adjustment: +15kg back to warehouse
6. Corrected issue slip reprinted

**Scenario 5: Emergency Issue**
1. Machine WV-007 runs out of yarn
2. Urgent issue request created:
   - P_REQUESTNO = "REQ-2024-URGENT-001"
   - P_ISSUETO = "WV-007"
   - Priority flag set
3. Nearest available pallet issued immediately
4. P_ISSUEDATE = Current time (real-time)
5. Production continues without delay

**Insert vs Update Logic (Upsert)**:
- **IF EXISTS** (REQUESTNO + PALLETNO): UPDATE weight/quantity
- **IF NOT EXISTS**: INSERT new issue record
- Enables: Issue correction without creating duplicates

**Stock Impact**:
- **INSERT**: Reduces stock REMAINQTY by P_WEIGHT
- **UPDATE**: Adjusts stock by difference (old weight vs new weight)
- **Ensures**: Inventory accuracy maintained

**Why This Matters**:
- **Inventory Control**: Real-time stock reduction when yarn issued
- **Production Traceability**: Links warehouse stock to production output
- **Request Management**: Groups multiple pallets under one requisition
- **Accountability**: Records operator and exact issue time
- **Partial Issues**: Supports issuing part of a pallet
- **Edit Capability**: Allows corrections before finalization

---

## Related Procedures

**Upstream**:
- G3_GETPALLETDETAIL (selects pallets to issue)
- G3_GETREQUESTNODETAIL (loads existing request for editing)
- G3_SEARCHYARNSTOCK (finds available stock)

**Downstream**:
- G3_UPDATEYARN (updates stock REMAINQTY)
- G3_INSERTRETURNYARN (returns unused yarn)
- WEAV_UPDATEWEFTSTOCK (weaving consumes issued yarn)

**Similar**:
- WARP_RECEIVEPALLET (warping issues material - similar pattern)
- G3_RECEIVEYARN (opposite direction - receiving from supplier)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_INSERTUPDATEISSUEYARN(string P_REQUESTNO, string P_PATTETNO, string P_TRACENO, decimal? P_CH, decimal? P_WEIGHT, DateTime? P_ISSUEDATE, string P_OPERATOR, string P_PALLETTYPE, string P_ISSUETO)`
**Lines**: 879-928

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_INSERTUPDATEISSUEYARN(G3_INSERTUPDATEISSUEYARNParameter para)`
**Lines**: 6843-6863 (Parameter: 6843-6854, Result: 6860-6862)

---

**File**: 160/296 | **Progress**: 54.1%
