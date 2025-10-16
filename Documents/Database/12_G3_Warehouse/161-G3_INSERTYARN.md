# G3_INSERTYARN

**Procedure Number**: 161 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Insert new yarn/material receiving record into G3 warehouse (manual entry) |
| **Operation** | INSERT |
| **Tables** | tblG3Stock, tblG3Receiving |
| **Called From** | G3DataService.cs:75 → SaveG3_yarn() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | ⚠️ Parameters commented out in code |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_TRUCKNO` | VARCHAR2(50) | ✅ | Truck/delivery number |
| `P_DESC` | VARCHAR2(500) | ✅ | Description/remarks |
| `P_PATTETNO` | VARCHAR2(50) | ✅ | Pallet number |
| `P_CH` | NUMBER | ⚠️ | Cone/cheese count |
| `P_WEIGHT` | NUMBER | ⚠️ | Weight quantity (kg) |
| `P_LOTORDERNO` | VARCHAR2(50) | ⚠️ | Lot/order number |
| `P_ITMORDER` | VARCHAR2(50) | ⚠️ | Item order code |
| `P_RECEIVEDATE` | VARCHAR2(50) | ⚠️ | Receiving date (string format) |
| `P_UM` | VARCHAR2(10) | ⚠️ | Unit of measure |
| `P_ITMYARN` | VARCHAR2(50) | ✅ | Yarn item code |
| `P_TYPE` | VARCHAR2(50) | ✅ | Material type classification |

### Output (OUT)

None - Returns success/failure result

### Returns (if cursor)

None - Procedure executes INSERT operation only

---

## Business Logic (What it does and why)

**Purpose**: Manually insert yarn/material receiving records into G3 warehouse. Used when operator manually enters receiving data (not from ERP integration). This is the alternative to automated receiving from AS400 or D365.

**When Used**:
- Manual yarn receiving entry (no ERP integration)
- Emergency receiving (ERP system down)
- Local supplier deliveries (not in ERP)
- Test/sample materials receiving
- Receiving corrections/adjustments

**Business Rules**:
1. **Manual Entry**: Operator types all information (not scanned/imported)
2. **Required Fields**: TRUCKNO, DESC, ITMYARN, TYPE, PALLETNO must be provided
3. **PK Constraint**: TRUCKNO + DESC cannot be null (commented in code: "PK truckno, desc ห้าม Null")
4. **Stock Addition**: Creates new stock record in warehouse
5. **No Validation**: Unlike ERP integrations, manual entry has less automated validation

**Workflow**:

**Scenario 1: Manual Receiving Entry (Non-ERP Supplier)**
1. Local supplier delivers yarn
2. Not registered in AS400/D365 system
3. Warehouse clerk manually enters:
   - Truck: "LOCAL-TRUCK-001"
   - Description: "PA66 yarn from Local Supplier ABC"
   - Pallet: "PLT-LOCAL-001234"
   - CH: 50 cones
   - Weight: 500 kg
   - Item: "YARN-PA66-001"
   - Type: "PA66"
   - UM: "KG"
   - Receive Date: "2024-01-15"
4. System calls G3_INSERTYARN with all parameters
5. New pallet created in warehouse stock
6. Available for issuing to production

**Scenario 2: Emergency Receiving (ERP Down)**
1. Critical yarn delivery arrives
2. AS400/D365 system is down for maintenance
3. Cannot use automated integration
4. Warehouse supervisor makes manual entry:
   - All required fields entered by hand
   - Temporary truck number assigned
   - Description notes "ERP DOWN - manual entry"
5. Receiving recorded immediately
6. Production can proceed
7. Later: Sync with ERP when system back online

**Scenario 3: Sample/Test Material**
1. R&D department orders yarn sample
2. Small quantity (5kg, 1 cone)
3. Not in standard ERP workflow
4. Lab receives and enters manually:
   - Truck: "SAMPLE-2024-001"
   - Description: "Test yarn for project XYZ"
   - Pallet: "SAMPLE-PLT-001"
   - Weight: 5 kg
   - CH: 1
   - Type: "SAMPLE"
5. Sample tracked separately in warehouse

**Scenario 4: Receiving Correction**
1. Found discrepancy in yesterday's receiving
2. Actual quantity: 520kg (not 500kg as recorded)
3. Supervisor creates correcting entry:
   - New pallet number with adjusted weight
   - Description: "Correction for PLT-001234"
   - Adds missing 20kg to inventory
4. Stock accuracy restored

**Code Issue Note**:
Lines 96-109 show **commented out parameter assignments**:
```csharp
//dbPara.P_TRUCKNO = truckno;
//dbPara.P_DESC = desc;
//dbPara.P_PATTETNO = palletNo;
// ... all other parameters commented
```

This suggests:
- **Potential Issue**: Parameters not being passed to procedure
- **Possible**: Procedure gets empty parameter object
- **Or**: Parameters set elsewhere in code
- **Warning**: This may cause receiving failures or empty inserts

**Comparison with Automated Receiving**:
- **G3_GETDATAAS400**: Automated from AS400 (standardized fields)
- **G3_GETDATAD365**: Automated from D365 (standardized fields)
- **G3_INSERTYARN**: Manual entry (flexible but prone to errors)

**Why This Matters**:
- **Flexibility**: Handles non-standard receiving scenarios
- **Backup**: Ensures operations continue when ERP down
- **Local Suppliers**: Supports suppliers not in ERP system
- **Samples/Tests**: Tracks special materials
- **Corrections**: Enables manual adjustments
- **Risk**: Manual entry more error-prone than automated

---

## Related Procedures

**Upstream**:
- None (manual entry - no upstream automation)
- CheckITMYARN (validates item code before insert)

**Downstream**:
- G3_SEARCHYARNSTOCK (finds manually entered stock)
- G3_GETPALLETDETAIL (retrieves manual entry details)
- G3_INSERTUPDATEISSUEYARN (issues manually received yarn)

**Similar**:
- G3_GETDATAAS400 (automated AS400 receiving)
- G3_GETDATAD365 (automated D365 receiving)
- G3_RECEIVEYARN (receiving with quality inspection data)

**Alternative**:
- G3_RECEIVEYARN (more comprehensive receiving with QC data)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `SaveG3_yarn(string truckno, string desc, string palletNo, decimal? ch, decimal? weight, string lotorderNo, string itmorder, string receiveDate, string um, string itmyarn, string type)`
**Lines**: 75-128

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_INSERTYARN(G3_INSERTYARNParameter para)`
**Lines**: 6816-6838 (Parameter: 6816-6829, Result: 6835-6837)

**⚠️ Important Note**: Lines 96-109 in G3DataService.cs have all parameter assignments commented out. This may indicate a bug or parameters are set elsewhere.

---

**File**: 161/296 | **Progress**: 54.4%
