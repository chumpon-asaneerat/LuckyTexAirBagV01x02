# WEAVE_INSERTUPDATEWEFTYARN

**Procedure Number**: 060 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Add or update weft yarn pallet for weaving |
| **Operation** | INSERT/UPDATE |
| **Called From** | WeavingDataService.cs:1572 → WEAVE_INSERTUPDATEWEFTYARN() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll) |
| `P_PALLETNO` | VARCHAR2(50) | ✅ | Weft yarn pallet number |
| `P_CHLOTNO` | VARCHAR2(50) | ✅ | China lot number (weft yarn) |
| `P_POSITION` | VARCHAR2(50) | ✅ | Pallet position on loom |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic

Records weft yarn pallets used in weaving for traceability. Operator scans weft yarn pallet barcode, system records which pallet supplies which fabric roll (doff).

**Workflow**:
1. Operator loads weft yarn pallet onto loom shuttle/creel position
2. Operator scans pallet barcode
3. System links pallet to current beam lot and doff number
4. System records pallet position on loom
5. If pallet already recorded, updates quantity or position
6. If new pallet, inserts new record
7. Returns success/error message

**Business Rules**:
- Weft yarn pallet must exist in inventory
- Pallet is linked to specific beam lot and doff (fabric roll)
- Position tracking enables multiple weft yarns per fabric (striped patterns)
- China lot number (P_CHLOTNO) provides traceability to yarn supplier batch
- Operator recorded for accountability
- Enables forward tracing: Yarn Pallet → Fabric Roll → Final Product
- Enables backward tracing: Fabric Roll → Yarn Pallets used

---

## Related Procedures

**Upstream**: [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Setup before weft tracking
**Downstream**: [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Uses weft data in production
**Similar**: [016-WARP_INSERTSETTINGDETAIL.md](../02_Warping/015-WARP_INSERTSETTINGDETAIL.md) - Warp yarn tracking

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_INSERTUPDATEWEFTYARN()`
**Lines**: 1572-1610

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAVE_INSERTUPDATEWEFTYARN(WEAVE_INSERTUPDATEWEFTYARNParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 60/296 | **Progress**: 20.3%
