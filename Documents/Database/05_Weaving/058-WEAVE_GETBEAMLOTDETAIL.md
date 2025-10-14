# WEAVE_GETBEAMLOTDETAIL

**Procedure Number**: 058 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get beam details for weaving setup |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:629 → WEAVE_GETBEAMLOTDETAIL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMNO` | VARCHAR2 | Physical beam number |
| `BEAMLOT` | VARCHAR2 | Beam lot barcode |
| `LENGTH` | NUMBER | Beam length (meters) |
| `TOTALYARN` | NUMBER | Total yarn count |
| `ITM_PREPARE` | VARCHAR2 | Item prepare code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type ID |
| `DRAWINGTYPE` | VARCHAR2 | Drawing type |
| `REEDNO` | VARCHAR2 | Reed number |
| `HEALDNO` | VARCHAR2 | Heald number |
| `HEALDCOLOR` | VARCHAR2 | Heald color code |
| `REEDTYPE` | VARCHAR2 | Reed type |

---

## Business Logic

Retrieves complete beam information including drawing details when operator scans beam to set up loom. Validates beam is ready for weaving.

**Workflow**:
1. Operator scans beam barcode during loom setup
2. System retrieves beam details from beaming records
3. Joins with drawing data to get technical specifications
4. Returns complete beam information for loom configuration
5. UI displays beam details for operator verification

**Business Rules**:
- Beam must exist in beaming system
- Drawing type determines loom setup requirements
- Reed and heald specifications must match loom capabilities
- Beam length affects production planning

---

## Related Procedures

**Upstream**: [033-BEAM_GETBEAMROLLDETAIL.md](../03_Beaming/033-BEAM_GETBEAMROLLDETAIL.md) - Creates beam data
**Downstream**: [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Uses beam data for setup
**Similar**: [048-DRAW_GETBEAMLOTDETAIL.md](../04_Drawing/048-DRAW_GETBEAMLOTDETAIL.md) - Similar lookup in drawing

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_GETBEAMLOTDETAIL()`
**Lines**: 629-660

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAVE_GETBEAMLOTDETAIL(WEAVE_GETBEAMLOTDETAILParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 58/296 | **Progress**: 19.6%
