# WEAV_INSERTMCSTOP

**Procedure Number**: 077 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Records machine stop events during weaving production with defect details |
| **Operation** | INSERT |
| **Called From** | WeavingDataService.cs:1834 → WEAV_INSERTMCSTOP() |
| **Frequency** | High (Every machine stop event) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2 | ✅ | Loom machine number where stop occurred |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll identifier) |
| `P_BEAMROLL` | VARCHAR2 | ✅ | Beam roll number in use |
| `P_WEAVINGLOT` | VARCHAR2 | ✅ | Weaving lot number |
| `P_DEFECT` | VARCHAR2 | ✅ | Defect code causing the stop (validated in C# - must not be empty) |
| `P_LENGTH` | NUMBER | ⬜ | Fabric length at stop position (meters) |
| `P_POSITION` | NUMBER | ⬜ | Position indicator |
| `P_REMARK` | VARCHAR2 | ⬜ | Additional remarks about the stop |
| `P_OPERATOR` | VARCHAR2 | ✅ | Operator who recorded the stop |
| `P_DATE` | DATE | ✅ | Date/time of machine stop |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result message (success/error message) |

---

## Business Logic

Records machine stop events during weaving production. Critical for:

1. **Downtime Tracking**: Monitor machine stoppage duration and frequency
2. **Defect Analysis**: Track what types of defects cause production interruptions
3. **OEE Calculation**: Calculate Overall Equipment Effectiveness by analyzing stop data
4. **Root Cause Analysis**: Investigate patterns in machine stops for preventive maintenance
5. **Operator Performance**: Track which operators encounter more stops (training needs)

**Business Rules**:
- **P_DEFECT is mandatory** - C# validation returns empty string if defect code is blank
- Machine stops must be associated with specific production context (loom, doff, beam, lot)
- Records exact length position where stop occurred for defect location tracking
- Timestamp (P_DATE) records when stop happened for duration calculations
- Can record multiple stops per doff as different defects occur

**Validation in C#**:
```csharp
if (string.IsNullOrWhiteSpace(P_DEFECT))
    return results;  // Empty string returned if defect not specified
```

**Typical Usage Scenario**:
1. Loom stops automatically or operator stops it manually
2. Operator inspects fabric and identifies defect type
3. Operator enters defect code, length position, and remarks
4. System records stop event with timestamp
5. Machine can be restarted after issue is resolved
6. Stop data is used for downtime analysis and efficiency reports

---

## Related Procedures

**Related**:
- [064-WEAV_DELETEMCSTOP.md](./064-WEAV_DELETEMCSTOP.md) - Delete machine stop records
- [071-WEAV_GETMCSTOPBYLOT.md](./071-WEAV_GETMCSTOPBYLOT.md) - Retrieve stop history by lot
- [072-WEAV_GETMCSTOPLISTBYDOFFNO.md](./072-WEAV_GETMCSTOPLISTBYDOFFNO.md) - Get stop list by doff number
- [063-WEAV_DEFECTLIST.md](./063-WEAV_DEFECTLIST.md) - Defect code master data

**Similar**:
- WARP_INSERTWARPMCSTOP - Machine stop recording in warping module
- BEAM_INSERTBEAMMCSTOP - Machine stop recording in beaming module

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_INSERTMCSTOP()`
**Lines**: 1834-1876

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_INSERTMCSTOP(WEAV_INSERTMCSTOPParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 077/296 | **Progress**: 26.0%
