# WEAVE_UPDATEPROCESSSETTING

**Procedure Number**: 061 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update loom setup parameters |
| **Operation** | UPDATE |
| **Called From** | WeavingDataService.cs:1471 → WEAVE_UPDATEPROCESSSETTING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2(50) | ✅ | Beam lot barcode |
| `P_REEDNO2` | VARCHAR2(50) | ✅ | Reed number 2 |
| `P_TEMPLE` | VARCHAR2(50) | ✅ | Temple setting |
| `P_BARNO` | VARCHAR2(50) | ✅ | Bar number |
| `P_PRODUCTTYPE` | VARCHAR2(50) | ✅ | Product type |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator name |
| `P_REEDNO` | VARCHAR2(50) | ❌ | Reed number 1 |
| `P_REEDTYPE` | VARCHAR2(50) | ❌ | Reed type |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message |

---

## Business Logic

Updates loom setup parameters when operator adjusts machine settings during production (reed change, temple adjustment, etc.).

**Workflow**:
1. Operator identifies need to change loom parameter (quality issue, specification change)
2. Operator opens edit setup screen
3. Operator modifies reed, temple, bar, or product type settings
4. System validates new parameters
5. System updates existing setup record in tblWeavingSettingHead
6. System records operator who made the change
7. Returns success/error message

**Business Rules**:
- Can only update setup for active production (beam lot must exist)
- Changes affect all subsequent fabric production from this setup
- Operator change is recorded for audit trail
- Temple and reed changes may affect fabric width and quality
- Product type changes affect downstream processing and quality standards
- Cannot change beam lot (must cancel and create new setup instead)

---

## Related Procedures

**Upstream**: [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Initial setup creation
**Downstream**: [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Uses updated settings
**Similar**: [024-WARP_UPDATESETTINGHEAD.md](../02_Warping/024-WARP_UPDATESETTINGHEAD.md) - Similar update pattern

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAVE_UPDATEPROCESSSETTING()`
**Lines**: 1471-1510

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAVE_UPDATEPROCESSSETTING(WEAVE_UPDATEPROCESSSETTINGParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 61/296 | **Progress**: 20.6%
