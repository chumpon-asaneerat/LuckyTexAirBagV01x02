# WEAV_SAMPLING

**Procedure Number**: 078 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Records fabric sampling data including spiral measurements and recut information |
| **Operation** | INSERT/UPDATE |
| **Called From** | WeavingDataService.cs:1782 → WEAV_SAMPLING() |
| **Frequency** | Medium (Each sampling event) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2 | ✅ | Beam roll number (validated in C# - must not be empty) |
| `P_LOOM` | VARCHAR2 | ✅ | Loom machine number |
| `P_ITMWEAVE` | VARCHAR2 | ✅ | Weaving item code |
| `P_SETTINGDATE` | DATE | ⬜ | Setup/setting date |
| `P_BARNO` | VARCHAR2 | ⬜ | Bar number identifier |
| `P_SPIRIAL_L` | NUMBER | ⬜ | Spiral measurement - Left side (cm or mm) |
| `P_SPIRIAL_R` | NUMBER | ⬜ | Spiral measurement - Right side (cm or mm) |
| `P_SAMPLING` | NUMBER | ⬜ | Sampling length or position |
| `P_SAMPLINGBY` | VARCHAR2 | ⬜ | Operator who performed sampling |
| `P_RECUT` | NUMBER | ⬜ | Recut length or position |
| `P_RECUTBY` | VARCHAR2 | ⬜ | Operator who performed recut |
| `P_RECUTDATE` | DATE | ⬜ | Recut date/time |
| `P_REMARK` | VARCHAR2 | ⬜ | Additional remarks |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `RESULT` | VARCHAR2 | Operation result message (success/error) |

---

## Business Logic

Records quality sampling data during weaving production. This procedure captures:

1. **Spiral Measurements**: Measures fabric edge distortion (left and right sides)
   - Spiral is a common fabric defect where edges curve instead of being straight
   - Critical for airbag fabric quality (must be within specification)

2. **Sampling Information**:
   - Records when and where sampling was performed
   - Tracks which operator did the sampling
   - Documents sampling position/length

3. **Recut Operations**:
   - If fabric fails quality check, operator may recut (trim) defective portion
   - Records recut length, operator, and timestamp
   - Maintains audit trail of corrective actions

**Business Rules**:
- **P_BEAMERROLL is mandatory** - C# validation returns empty string if blank
- Spiral measurements (left/right) indicate fabric straightness quality
- May be called during initial setup sampling and periodic production sampling
- Recut data is optional (only filled if recutting was necessary)
- Links sampling data to specific beam roll and loom for traceability

**Validation in C#**:
```csharp
if (string.IsNullOrWhiteSpace(P_BEAMERROLL))
    return results;  // Empty string if beam roll not specified
```

**Typical Usage Scenario**:
1. Operator starts weaving with new beam roll
2. After initial meters, operator takes sampling measurement
3. Measures spiral on left and right edges using template/gauge
4. If spiral exceeds tolerance, may need to adjust loom settings
5. If severe defect, operator cuts out bad section (recut)
6. System records all sampling and recut data for QA audit

**Quality Context**: Airbag fabric must be extremely precise. Spiral measurements ensure fabric edges are straight, which is critical for downstream cutting operations and final airbag assembly.

---

## Related Procedures

**Related**:
- [073-WEAV_GETSAMPLINGDATA.md](./073-WEAV_GETSAMPLINGDATA.md) - Retrieve sampling history data
- [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Process settings that affect quality
- [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Main weaving process

**Downstream**:
- Quality inspection procedures - Use sampling data for quality reports
- LAB module procedures - Laboratory may test sampled fabric pieces

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_SAMPLING()`
**Lines**: 1782-1828

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_SAMPLING(WEAV_SAMPLINGParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 078/296 | **Progress**: 26.4%
