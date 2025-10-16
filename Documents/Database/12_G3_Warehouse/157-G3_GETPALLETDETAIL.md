# G3_GETPALLETDETAIL

**Procedure Number**: 157 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete pallet details by pallet number for yarn issuing |
| **Operation** | SELECT |
| **Tables** | tblG3Stock, tblG3Receiving |
| **Called From** | G3DataService.cs:819 → G3_GETPALLETDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Pallet number to retrieve details |

### Output (OUT)

None

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ENTRYDATE` | DATE | Pallet entry/receiving date |
| `ITM_YARN` | VARCHAR2(50) | Yarn item code (MES item code) |
| `PALLETNO` | VARCHAR2(50) | Pallet number |
| `YARNTYPE` | VARCHAR2(50) | Yarn type classification |
| `WEIGHTQTY` | NUMBER | Weight quantity (kg) |
| `CONECH` | NUMBER | Cone/cheese count |
| `VERIFY` | VARCHAR2(50) | Verification status |
| `REMAINQTY` | NUMBER | Remaining quantity available (kg) |
| `RECEIVEBY` | VARCHAR2(50) | Operator who received pallet |
| `RECEIVEDATE` | DATE | Receiving date/time |
| `FINISHFLAG` | VARCHAR2(10) | Status flag (ACTIVE/FINISHED) |
| `UPDATEDATE` | DATE | Last update timestamp |
| `PALLETTYPE` | VARCHAR2(50) | Pallet type classification |
| `ITM400` | VARCHAR2(50) | AS400 item code |
| `UM` | VARCHAR2(10) | Unit of measure |
| `PACKAING` | VARCHAR2(50) | Packaging inspection result |
| `CLEAN` | VARCHAR2(50) | Cleanliness inspection result |
| `TEARING` | VARCHAR2(50) | Tearing inspection result |
| `FALLDOWN` | VARCHAR2(50) | Fall down inspection result |
| `CERTIFICATION` | VARCHAR2(50) | Certification document status |
| `INVOICE` | VARCHAR2(50) | Invoice document status |
| `IDENTIFYAREA` | VARCHAR2(50) | Identification area code |
| `AMOUNTPALLET` | VARCHAR2(50) | Amount of pallets |
| `OTHER` | VARCHAR2(500) | Other notes/remarks |
| `ACTION` | VARCHAR2(50) | Last action performed |
| `MOVEMENTDATE` | DATE | Last movement date |
| `LOTNO` | VARCHAR2(50) | Lot number |
| `TRACENO` | VARCHAR2(50) | Trace number |
| `KGPERCH` | NUMBER | Kilograms per cone/cheese |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves comprehensive pallet information for yarn issuing operations. When operator scans or enters a pallet number for issuing to production, this procedure returns all details needed to create the issue transaction.

**When Used**:
- Operator issues yarn to production line
- Scanning pallet barcode for issuing
- Creating issue request (issue slip)
- Verifying pallet availability before issuing
- Viewing pallet details for stock inquiry

**Business Rules**:
1. **Single Pallet Query**: Returns all details for one specific pallet
2. **Available Stock Only**: Should return pallets with REMAINQTY > 0
3. **Quality Information**: Includes all inspection results (packaging, clean, tearing, falldown)
4. **Document Status**: Shows certification and invoice document completeness
5. **Traceability Data**: Includes lot number, trace number for full traceability

**Workflow**:

**Scenario 1: Operator Issues Yarn to Production**
1. Production requests yarn for weaving machine WV-001
2. Warehouse operator creates issue request:
   - Request No: REQ-2024-0015
   - Issue To: WV-001
   - Issue By: Operator ID
3. Operator scans pallet barcode "PLT-2024-001234"
4. System calls G3_GETPALLETDETAIL(P_PALLETNO = "PLT-2024-001234")
5. Returns complete pallet details:
   - ITM_YARN = "YARN-PA66-001"
   - WEIGHTQTY = 500 kg
   - REMAINQTY = 500 kg (full pallet available)
   - CONECH = 50 cones
   - LOTNO = "LOT-2024-0015"
   - Quality inspection results (all pass)
6. System displays pallet details on screen
7. Operator confirms and completes issue
8. Issue transaction created with all pallet data

**Scenario 2: Partial Pallet Already Issued**
1. Operator scans pallet "PLT-2024-000999"
2. Procedure returns:
   - WEIGHTQTY = 500 kg (original weight)
   - REMAINQTY = 250 kg (50% already issued)
   - FINISHFLAG = "ACTIVE" (still has stock)
3. System shows: "Partial pallet - 250 kg available"
4. Operator can issue remaining quantity

**Scenario 3: Pallet Fully Issued**
1. Operator scans pallet "PLT-2024-000888"
2. Procedure returns:
   - REMAINQTY = 0 kg
   - FINISHFLAG = "FINISHED"
3. System shows: "Pallet fully issued - no stock available"
4. Operator must select different pallet

**Scenario 4: Quality Document Verification**
1. Customer requires certification documents
2. Operator scans pallet "PLT-2024-001500"
3. Returns quality inspection data:
   - PACKAING = "PASS"
   - CLEAN = "PASS"
   - TEARING = "PASS"
   - FALLDOWN = "PASS"
   - CERTIFICATION = "YES" (certificate available)
   - INVOICE = "YES" (invoice available)
4. Operator verifies all required documents present
5. Issues pallet with confidence

**Data Transformation in DataService**:
- Procedure returns `G3_GETPALLETDETAILResult` objects
- DataService transforms to `G3_GETREQUESTNODETAIL` format
- Adds calculated fields: RowNo, current ISSUEDATE, REQUESTNO, ISSUEBY, ISSUETO
- Prepares data for issue slip grid display

**Why This Matters**:
- **Accurate Issuing**: Complete pallet information prevents wrong material issues
- **Stock Availability**: REMAINQTY shows actual available quantity
- **Quality Assurance**: Inspection results ensure only good materials issued
- **Document Compliance**: Certificate/invoice status ensures regulatory compliance
- **Traceability**: Lot/trace numbers maintain material genealogy
- **Efficiency**: Single query returns all data needed for issue transaction

---

## Related Procedures

**Upstream**:
- G3_RECEIVEYARN (creates pallets that are later queried)
- G3_GETDATAAS400 (AS400 receives create pallets)
- G3_GETDATAD365 (D365 receives create pallets)

**Downstream**:
- G3_INSERTUPDATEISSUEYARN (uses pallet details to create issue)
- G3_GETREQUESTNODETAIL (similar - gets issue request details)
- G3_UPDATEYARN (updates REMAINQTY after issuing)

**Similar**:
- G3_SEARCHYARNSTOCK (searches multiple pallets)
- WARP_GETREMAINPALLET (warping module - similar pallet inquiry)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_GETPALLETDETAIL(string REQUESTNO, string PALLETNO, string ISSUEBY, string ISSUETO)`
**Lines**: 819-874

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_GETPALLETDETAIL(G3_GETPALLETDETAILParameter para)`
**Lines**: 6978-7023 (Parameter: 6978-6981, Result: 6987-7023)

---

**File**: 157/296 | **Progress**: 53.0%
