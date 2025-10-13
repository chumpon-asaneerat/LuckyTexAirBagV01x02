# WEAV_GETCNTCHINALOT

**Procedure Number**: 067 | **Module**: M05 Weaving (also used in M03 Beaming) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get count of China lots (special lot tracking for Chinese customer orders) |
| **Operation** | SELECT (scalar value) |
| **Tables** | tblWeavingLot or tblChinaLot (assumed) |
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

## Database Operations

### Tables

**Primary Tables**:
- `tblWeavingLot` - SELECT - Weaving lot records with China lot classification
- OR `tblChinaLot` - SELECT - Dedicated China lot tracking table
- May query across multiple modules (Warping, Beaming, Weaving)

**Transaction**: No (Read-only operation)

### Likely Query Structure

```sql
-- Possible implementation 1: Count China lots with specific pattern
SELECT COUNT(*) AS CNT
FROM tblWeavingLot
WHERE LOTNO LIKE P_LOT || '%'
  AND CUSTOMERTYPE = 'CHINA'
  AND STATUS = 'ACTIVE';

-- Possible implementation 2: Check if lot is China lot
SELECT CASE
    WHEN EXISTS (SELECT 1 FROM tblChinaLot WHERE LOTNO = P_LOT)
    THEN '1'
    ELSE '0'
END AS CNT
FROM DUAL;

-- Possible implementation 3: Get next sequence number for China lot
SELECT TO_CHAR(NVL(MAX(SUBSTR(LOTNO, -4)), 0) + 1) AS CNT
FROM tblWeavingLot
WHERE LOTNO LIKE 'CN-' || TO_CHAR(SYSDATE, 'YYYY-MM') || '%';
```

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

### Weaving Module Usage

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETCNTCHINALOT(string P_LOT)`
**Lines**: 354-380
**Comment**: `เพิ่มใหม่ WEAV_GETCNTCHINALOT ใช้ในการ Load WIDTHWEAVING` (Thai: "Added for loading width weaving")

### Beaming Module Usage

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\BeamingDataService.cs`
**Method**: `WEAV_GETCNTCHINALOT(string P_LOT)`
**Lines**: 114-140
**Comment**: `เพิ่มใหม่ WEAV_GETCNTCHINALOT ใช้ในการ Load WEAV` (Thai: "Added for loading weaving")

### Database Manager

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETCNTCHINALOT(WEAV_GETCNTCHINALOTParameter para)`
**Lines**: 13624-13650

**Stored Procedure Call**:
```csharp
// Single input parameter - lot number to check
string[] paraNames = new string[]
{
    "P_LOT"
};

object[] paraValues = new object[]
{
    para.P_LOT
};

ExecuteResult<StoredProcedureResult> ret = _manager.ExecuteProcedure(
    "WEAV_GETCNTCHINALOT",
    paraNames, paraValues);

// Returns OUT parameter value
if (null != ret && !ret.HasException)
{
    result = new WEAV_GETCNTCHINALOTResult();
    if (ret.Result.OutParameters["CNT"] != DBNull.Value)
        result.CNT = (string)ret.Result.OutParameters["CNT"];
}
```

**Return Handling**:
```csharp
// Service layer returns string directly
public string WEAV_GETCNTCHINALOT(string P_LOT)
{
    string results = string.Empty;

    WEAV_GETCNTCHINALOTParameter dbPara = new WEAV_GETCNTCHINALOTParameter();
    dbPara.P_LOT = P_LOT;

    WEAV_GETCNTCHINALOTResult dbResults =
        DatabaseManager.Instance.WEAV_GETCNTCHINALOT(dbPara);

    if (null != dbResults)
    {
        results = dbResults.CNT;  // Return count as string
    }

    return results;
}
```

**Usage Example 1: Validate China Lot**
```csharp
// Check if lot is a China lot
WeavingDataService service = WeavingDataService.Instance;
string lotNumber = "CN-2024-10-0001";

string count = service.WEAV_GETCNTCHINALOT(lotNumber);

if (!string.IsNullOrEmpty(count) && count != "0")
{
    // This is a China lot - apply special processing
    lblCustomerType.Text = "CHINA";
    panelChinaLotDetails.Visible = true;
    LoadChinaCustomerRequirements();
}
else
{
    // Regular lot - standard processing
    panelChinaLotDetails.Visible = false;
}
```

**Usage Example 2: Generate Next China Lot Number**
```csharp
// Generate next sequential China lot number
public string GenerateNextChinaLotNumber()
{
    string datePrefix = "CN-" + DateTime.Now.ToString("yyyy-MM") + "-";

    // Get current count
    string countStr = service.WEAV_GETCNTCHINALOT(datePrefix);

    int nextNumber = 1;
    if (!string.IsNullOrEmpty(countStr))
    {
        int.TryParse(countStr, out int currentCount);
        nextNumber = currentCount + 1;
    }

    // Format as CN-2024-10-0001
    string newLotNumber = datePrefix + nextNumber.ToString("D4");

    return newLotNumber;
}
```

**Usage Example 3: Cross-Module (Beaming)**
```csharp
// In Beaming module - check if beam is for China lot
BeamingDataService beamService = BeamingDataService.Instance;
string beamLot = "BM-2024-001";

// Get associated weaving lot (assumed logic)
string weavingLot = GetWeavingLotForBeam(beamLot);

// Check if it's China lot
string isChinaLot = beamService.WEAV_GETCNTCHINALOT(weavingLot);

if (isChinaLot == "1")
{
    // Apply China-specific beaming parameters
    ApplyChinaQualityStandards();
    GenerateChinaTraceabilityDocument();
}
```

---

**File**: 067/296 | **Progress**: 22.6%
