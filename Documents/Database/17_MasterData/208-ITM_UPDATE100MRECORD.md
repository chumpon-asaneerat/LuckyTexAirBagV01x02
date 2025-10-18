# ITM_UPDATE100MRECORD

**Procedure Number**: 208 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Update 100-meter inspection quality specifications for item |
| **Operation** | UPDATE |
| **Tables** | Item 100M quality specification table |
| **Called From** | HundredMDataService.cs |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Item code to update |
| `P_DENW` | VARCHAR2(20) | ⬜ | Warp density specification |
| `P_DENF` | VARCHAR2(20) | ⬜ | Weft density specification |
| `P_WIDTHALL` | VARCHAR2(20) | ⬜ | Total width specification |
| `P_WIDTHPIN` | VARCHAR2(20) | ⬜ | Width at pin specification |
| `P_WIDTHCOAT` | VARCHAR2(20) | ⬜ | Width after coating specification |
| `P_WIDTHSELVAGEL` | VARCHAR2(20) | ⬜ | Left selvage width |
| `P_WIDTHSELVAGER` | VARCHAR2(20) | ⬜ | Right selvage width |
| `P_TRIML` | VARCHAR2(20) | ⬜ | Left trim specification |
| `P_TRIMR` | VARCHAR2(20) | ⬜ | Right trim specification |
| `P_FLOPPYL` | VARCHAR2(20) | ⬜ | Left edge floppy specification |
| `P_FLOPPYR` | VARCHAR2(20) | ⬜ | Right edge floppy specification |
| `P_UNWINDER` | VARCHAR2(50) | ⬜ | Unwinder setting |
| `P_WINDER` | VARCHAR2(50) | ⬜ | Winder setting |
| `P_HARDNESSL` | VARCHAR2(20) | ⬜ | Left edge hardness specification |
| `P_HARDNESSC` | VARCHAR2(20) | ⬜ | Center hardness specification |
| `P_HARDNESSR` | VARCHAR2(20) | ⬜ | Right edge hardness specification |

### Output (OUT)

None (void procedure)

---

## Business Logic (What it does and why)

Updates quality inspection specifications for 100-meter testing. The 100M inspection is a detailed quality check performed every 100 meters during inspection process. This procedure stores the standard specifications against which actual measurements are compared to determine if fabric quality is acceptable.

The procedure:
1. Takes item code and quality specifications
2. Updates or creates 100M inspection specification record
3. Stores dimensional specifications (width, trim, selvage)
4. Stores hardness/floppy specifications (left, center, right)
5. Stores density specifications (warp and weft)
6. Used by quality inspection module to validate fabric quality

These specifications are critical for automated pass/fail decisions during inspection.

---

## Related Procedures

**Similar**:
- [201-ITM_GETITEMBYITEMCODEANDCUSID.md](./201-ITM_GETITEMBYITEMCODEANDCUSID.md) - Get item with quality specs

**Downstream**: Used by M08 Inspection module for quality checks

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\HundredMDataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `ITM_UPDATE100MRECORD(ITM_UPDATE100MRECORDParameter para)`
**Lines**: 21874-21931

---

**File**: 208/296 | **Progress**: 70.3%
