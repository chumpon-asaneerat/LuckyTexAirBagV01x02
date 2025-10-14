# WEAVE_INSERTPROCESSSETTING

**Procedure Number**: 059 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Create new loom setup with beam and parameters |
| **Operation** | INSERT |
| **Called From** | WeavingDataService.cs:1413 → WEAVE_INSERTPROCESSSETTING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_MC` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_ITMWEAVE` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_REEDNO2` | VARCHAR2(50) | ✅ | Reed number 2 |
| `P_TEMPLE` | VARCHAR2(50) | ✅ | Temple setting |
| `P_BARNO` | VARCHAR2(50) | ✅ | Bar number |
| `P_PRODUCTTYPE` | VARCHAR2(50) | ✅ | Product type |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |
| `P_REEDNO` | VARCHAR2(50) | ❌ | Reed number 1 |
| `P_REEDTYPE` | VARCHAR2(50) | ❌ | Reed type |
| `P_HEALDNO` | VARCHAR2(50) | ❌ | Heald number |
| `P_HEALDCOLOR` | VARCHAR2(50) | ❌ | Heald color |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |
| `SETUP_ID` | NUMBER | Generated setup ID |

---

## Business Logic

Creates loom setup record when operator loads beam onto loom. Records all loom parameters (reed, temple, bar, etc.) and generates setup ID for tracking.

**Workflow**:
1. Operator scans beam barcode to start loom setup
2. Operator selects loom machine number
3. Operator enters or confirms loom parameters (reed, temple, bar)
4. System validates beam is available and not already in use
5. System creates new setup record in tblWeavingSettingHead
6. System generates unique SETUP_ID for tracking
7. Returns setup ID to UI for production tracking

**Business Rules**:
- Beam must exist and be available (not already in production)
- Loom machine must be available for new setup
- Reed, temple, and bar numbers are required for quality control
- Setup ID links all subsequent weaving operations
- Operator is recorded for accountability
- Product type determines downstream processing requirements

---

## Related Procedures

**Upstream**: [058-WEAVE_GETBEAMLOTDETAIL.md](./058-WEAVE_GETBEAMLOTDETAIL.md) - Retrieves beam info before setup
**Downstream**: [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Uses setup for production
**Similar**: [016-WARP_INSERTSETTINGHEAD.md](../02_Warping/016-WARP_INSERTSETTINGHEAD.md) - Similar setup pattern

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_INSERTPROCESSSETTING()`
**Lines**: 1413-1450

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAVE_INSERTPROCESSSETTING(WEAVE_INSERTPROCESSSETTINGParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 59/296 | **Progress**: 19.9%
