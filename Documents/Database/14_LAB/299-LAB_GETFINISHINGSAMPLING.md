# LAB_GETFINISHINGSAMPLING

**Procedure Number**: 299 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get finishing sample details for lab testing |
| **Operation** | SELECT |
| **Tables** | tblLabSampling, tblLabMassPro (assumed) |
| **Called From** | LABDataService.cs → LAB_GETFINISHINGSAMPLING() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2(50) | ✅ | Weaving lot number |
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Product item code |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number |
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot number |
| `ITM_CODE` | VARCHAR2(50) | Product item code |
| `CREATEDATE` | DATE | Sample creation date |
| `CREATEBY` | VARCHAR2(50) | Operator who created sample |
| `PRODUCTID` | VARCHAR2(50) | Product identification |
| `SAMPLING_WIDTH` | NUMBER | Sample width (mm) |
| `SAMPLING_LENGTH` | NUMBER | Sample length (meters) |
| `PROCESS` | VARCHAR2(50) | Finishing process type |
| `REMARK` | VARCHAR2(500) | Sample remarks/notes |
| `FABRICTYPE` | VARCHAR2(50) | Fabric type classification |
| `RETESTFLAG` | VARCHAR2(10) | Retest indicator (Y/N) |

---

## Business Logic (What it does and why)

Retrieves detailed information about a finishing sample that has been received in the laboratory. Used to load sample data for testing entry and tracking.

**Workflow**:
1. Receives weaving lot number and item code
2. Queries lab sampling database for matching sample
3. Returns complete sample information:
   - Lot traceability (weaving lot, finishing lot)
   - Product identification
   - Sample dimensions (width, length)
   - Sample metadata (create date, operator, remarks)
   - Test status (retest flag)
4. Data used to populate lab testing screens

**Business Rules**:
- Sample must have been received first (via LAB_CHECKRECEIVESAMPLING)
- Returns single sample record per weaving lot/item code combination
- RETESTFLAG indicates if this is a retest sample
- Sample dimensions used for test calculations (e.g., force per width)

**Sample Information Usage**:

**Traceability**:
- WEAVINGLOT: Links to weaving process
- FINISHINGLOT: Links to finishing process
- ITM_CODE: Product specification reference

**Physical Dimensions**:
- SAMPLING_WIDTH: Width of test sample (mm)
- SAMPLING_LENGTH: Length of test sample (meters)
- Used for test result calculations (e.g., N/mm for tensile)

**Test Management**:
- RETESTFLAG = 'Y': This is a retest sample (original failed)
- RETESTFLAG = 'N': This is original sample
- PROCESS: Finishing process type (COATING, SCOURING, DRYER)

**Testing Workflow**:
1. **Receive Sample**: Lab receives finished fabric sample
2. **Check if Received**: LAB_CHECKRECEIVESAMPLING
3. **Get Sample Details**: LAB_GETFINISHINGSAMPLING (this procedure)
4. **Perform Tests**:
   - Tensile test (LAB_INSERTUPDATETENSILE)
   - Tear test (LAB_INSERTUPDATETEAR)
   - Air permeability
   - Edge comb
   - etc.
5. **Save Results**: LAB_SAVELABMASSPRORESULT
6. **Approve**: LAB_APPROVELABDATA

---

## Related Procedures

**Upstream**: [298-LAB_CHECKRECEIVESAMPLING.md](./298-LAB_CHECKRECEIVESAMPLING.md) - Checks if sample received
**Related**: LAB_GETWEAVINGSAMPLING - Gets weaving (greige) sample details
**Downstream**: Test entry procedures (tensile, tear, air, etc.)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GETFINISHINGSAMPLING()`
**Lines**: Likely in sample retrieval section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GETFINISHINGSAMPLING(LAB_GETFINISHINGSAMPLINGParameter para)`
**Lines**: 4678-4702

**Return Structure** (12 columns):
---

**File**: 299/296 | **Progress**: 101.0%
