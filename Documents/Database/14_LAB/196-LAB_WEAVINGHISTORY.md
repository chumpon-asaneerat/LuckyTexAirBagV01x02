# LAB_WEAVINGHISTORY

**Procedure Number**: 196 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve weaving production history for laboratory test reference |
| **Operation** | SELECT |
| **Tables** | Weaving production records |
| **Called From** | LABDataService.cs |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVINGLOT` | VARCHAR2(50) | ✅ | Weaving lot number to retrieve history |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `LENGTH` | NUMBER | Fabric length produced (meters) |
| `LOOMNO` | VARCHAR2(20) | Loom machine number |
| `WEAVINGDATE` | DATE | Weaving production date |
| `SHIFT` | VARCHAR2(10) | Production shift |
| `REMARK` | VARCHAR2(500) | Production remarks/notes |
| `CREATEDATE` | DATE | Record creation date |
| `WIDTH` | NUMBER | Fabric width (cm) |
| `PREPAREBY` | VARCHAR2(50) | Operator who prepared loom |
| `WEAVINGNO` | VARCHAR2(50) | Weaving sequence number |
| `BEAMLOT` | VARCHAR2(50) | Beam lot number used |
| `DOFFNO` | NUMBER | Doff sequence number |
| `DENSITY_WARP` | NUMBER | Warp density (ends/cm) |
| `TENSION` | NUMBER | Warp tension setting |
| `STARTDATE` | DATE | Production start date/time |
| `DOFFBY` | VARCHAR2(50) | Operator who performed doff |
| `SPEED` | NUMBER | Loom speed (picks/min) |
| `WASTE` | NUMBER | Waste amount (meters) |
| `DENSITY_WEFT` | NUMBER | Weft density (picks/cm) |
| `DELETEFLAG` | VARCHAR2(1) | Deletion flag (Y/N) |
| `DELETEBY` | VARCHAR2(50) | Operator who deleted record |
| `DELETEDATE` | DATE | Deletion timestamp |

---

## Business Logic (What it does and why)

Retrieves weaving production history for a specific weaving lot to provide context for laboratory testing. When lab technician receives a greige fabric sample for testing, this procedure shows the production details (which loom, what length, what item) to help understand test results in context of production conditions.

The procedure:
1. Takes a weaving lot number
2. Looks up production record
3. Returns key production parameters (loom, length, item code)
4. Used for correlating test results with production conditions

This helps identify if quality issues are related to specific looms or production runs.

---

## Related Procedures

**Upstream**:
- [313-LAB_GETWEAVINGLOTLIST.md](./313-LAB_GETWEAVINGLOTLIST.md) - Get available weaving lots

**Downstream**:
- [316-LAB_SEARCHLABENTRYPRODUCTION.md](./316-LAB_SEARCHLABENTRYPRODUCTION.md) - Search lab test entries
- [319-LAB_SAVELABRESULT.md](./319-LAB_SAVELABRESULT.md) - Save test results

**Similar**:
- [312-LAB_GETFINISHINGSAMPLING.md](./312-LAB_GETFINISHINGSAMPLING.md) - Similar history lookup for finishing

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: Method name to be confirmed from DataService file
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_WEAVINGHISTORY(LAB_WEAVINGHISTORYParameter para)`
**Lines**: 17611-17660

---

**File**: 196/296 | **Progress**: 66.2%
