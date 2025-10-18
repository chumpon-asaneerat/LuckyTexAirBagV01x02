# MASTER_AIRBAGPROCESSLIST

**Procedure Number**: 219 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get list of all production processes in the airbag manufacturing system |
| **Operation** | SELECT |
| **Tables** | Process master table |
| **Called From** | DefectCodeService.cs, DataService.cs |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

None - Returns all processes

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PROCESSID` | VARCHAR2(50) | Process ID code |
| `PROCESSDESCRIPTION` | VARCHAR2(200) | Process name/description |

---

## Business Logic (What it does and why)

Retrieves complete list of production processes in the airbag fabric manufacturing system. Each process represents a stage in the production flow (Warping, Beaming, Drawing, Weaving, Finishing, Inspection, etc.). Used for populating dropdown lists when configuring process-specific settings like defect codes, machine assignments, or quality parameters.

The procedure:
1. Queries all production process records
2. Returns process ID and description
3. Used for UI dropdown population across multiple modules
4. No filtering - returns all processes

Typical processes include: WARP, BEAM, DRAW, WEAVE, COAT, SCOUR, DRY, INSPECT, CUT, PACK

---

## Related Procedures

**Used By**:
- Defect code configuration (linking defects to processes)
- Authorization management (process-specific permissions)
- Machine master data (assigning machines to processes)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DefectCodeService.cs` and `DataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `MASTER_AIRBAGPROCESSLIST(MASTER_AIRBAGPROCESSLISTParameter para)`
**Lines**: 17566-17602

---

**File**: 219/296 | **Progress**: 74.0%
