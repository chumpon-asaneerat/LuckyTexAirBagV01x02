# WEAVE_WEAVINGPROCESS

**Procedure Number**: 062 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Start new fabric roll (doff) production |
| **Operation** | INSERT |
| **Called From** | WeavingDataService.cs:1519 → WEAVE_WEAVINGPROCESS() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll sequence) |
| `P_ITEMWEAVING` | VARCHAR2(50) | ✅ | Weaving item code |
| `P_LOOM` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_STARTDATE` | DATE | ✅ | Production start date |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |
| `P_SHIFT` | VARCHAR2(10) | ✅ | Production shift |
| `P_REEDNO` | VARCHAR2(50) | ❌ | Reed number |
| `P_TEMPLE` | VARCHAR2(50) | ❌ | Temple setting |
| `P_BARNO` | VARCHAR2(50) | ❌ | Bar number |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Generated WEAVINGLOT barcode or error |

---

## Business Logic

Starts new fabric roll (doff) production on loom. Generates unique WEAVINGLOT barcode for the fabric roll. Creates production tracking record.

**Workflow**:
1. Loom setup is complete (beam loaded, weft yarn ready)
2. Operator initiates new doff (fabric roll) production
3. System validates beam lot exists and is active
4. System validates loom is available
5. System generates unique WEAVINGLOT barcode (format: date + sequential)
6. System creates production record in tblWeavingDetail
7. System records start date, operator, shift, loom parameters
8. Returns WEAVINGLOT barcode to UI for operator confirmation
9. Operator prints/attaches barcode label to fabric roll

**Business Rules**:
- Each fabric roll (doff) has unique WEAVINGLOT barcode
- DOFFNO is sequential number (1, 2, 3...) per beam lot
- First doff starts at 1, increments for each roll from same beam
- WEAVINGLOT enables forward tracing: Beam → Fabric Rolls → Final Products
- Start date and operator recorded for production tracking
- Shift recorded for productivity analysis
- Reed, temple, bar settings recorded at doff start (may differ from setup)
- Item weaving code determines product specifications

---

## Related Procedures

**Upstream**: [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Creates loom setup
**Downstream**: Production monitoring procedures (WEAV_* series)
**Similar**: [017-WARP_INSERTWARPINGPROCESS.md](../02_Warping/017-WARP_INSERTWARPINGPROCESS.md) - Warp production start

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_WEAVINGPROCESS()`
**Lines**: 1519-1570

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAVE_WEAVINGPROCESS(WEAVE_WEAVINGPROCESSParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 62/296 | **Progress**: 20.9%
