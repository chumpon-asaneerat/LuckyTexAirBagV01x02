# WEAV_GETCNTCHINALOT

**Procedure Number**: 067 | **Module**: M05 Weaving (also used in M03 Beaming) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get count of China lots (special lot tracking for Chinese customer orders) |
| **Operation** | SELECT (scalar value) |
| **Called From** | WeavingDataService.cs:354 → WEAV_GETCNTCHINALOT()<br>BeamingDataService.cs:114 → WEAV_GETCNTCHINALOT() |
| **Frequency** | Medium - Used during lot number generation/validation |
| **Performance** | Fast - Single scalar value lookup |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOT` | VARCHAR2(50) | ✅ | Lot number to check/count |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `CNT` | VARCHAR2 | Count or status value (returned as string) |

### Returns

Single OUT parameter value (scalar)

---

## Business Logic (What it does and why)

**Purpose**: Specialized procedure for tracking production lots destined for Chinese customers, which require special handling, documentation, or sequencing.

**Business Context**:
- LuckyTex produces airbag fabric for multiple customers globally
- Chinese customer orders have special requirements (documentation, quality specs, traceability)
- "China lots" need separate tracking and counting mechanisms
- Lot numbering may include country/customer-specific prefixes or sequences
- Count is used for validation, duplicate checking, or sequence generation

**Usage Scenarios**:

**Scenario 1: Lot Number Validation**
1. Operator enters or scans lot number during production setup
2. System calls WEAV_GETCNTCHINALOT to check if it's a valid China lot
3. If CNT = "1", lot is confirmed as China lot → apply special rules
4. If CNT = "0", lot is regular lot → standard processing
5. UI may display special indicators or enable additional fields for China lots

**Scenario 2: China Lot Sequence Generation**
1. Production planning creates new lot for Chinese customer order
2. System needs to generate next China lot number
3. Calls WEAV_GETCNTCHINALOT with date pattern to get current count
4. Increments count to generate new unique lot number
5. Example: CN-2024-10-0001, CN-2024-10-0002, etc.

**Scenario 3: Duplicate Lot Prevention**
1. Before creating new China lot, system validates uniqueness
2. Calls WEAV_GETCNTCHINALOT with proposed lot number
3. If CNT > 0, lot already exists → prevent duplicate
4. If CNT = 0, lot is unique → allow creation

**Scenario 4: Cross-Module Tracking (Beaming Usage)**
1. Beaming module needs to track China lots from upstream warping
2. Calls WEAV_GETCNTCHINALOT to verify if warp beam is for China lot
3. If yes, applies special beaming parameters or documentation
4. Maintains traceability through production chain

**Business Rules**:
- China lots may have specific naming conventions (prefix, format)
- Count is returned as STRING (not integer) for flexibility
- May track across multiple production stages (Warping → Beaming → Weaving)
- Used by both Weaving and Beaming modules (shared procedure)

**Special Handling for China Lots**:
- Enhanced quality documentation
- Specific customer labels and certifications
- Export documentation requirements
- Special packaging or marking
- Traceability reports in specific format

**Why Named "CHINALOT"**:
- "China" refers to customer destination country (China market)
- Not product origin (fabric made in Thailand)
- Specific customer requirements drive special handling

---

## Related Procedures

**Lot Number Generation/Validation**:
- May have similar procedures for other customer types (Japan, USA, Europe)
- General lot number validation procedures

**Cross-Module Usage**:
- Used in **WeavingDataService** (lines 354-380) - Weaving operations
- Used in **BeamingDataService** (lines 114-140) - Beaming operations
- Shows importance of tracking China lots through production chain

**Customer-Specific Logic**:
- Customer order lookup procedures
- Customer specification validation
- Export documentation generation

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File (Weaving)**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETCNTCHINALOT()`
**Lines**: 354-380

**DataService File (Beaming)**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `WEAV_GETCNTCHINALOT()`
**Lines**: 114-140

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETCNTCHINALOT(WEAV_GETCNTCHINALOTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 067/296 | **Progress**: 22.6%
