# INS_GETRESETSTARTLENGTH

**Procedure Number**: 134 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get reset start length specification for specific customer and item |
| **Operation** | SELECT |
| **Tables** | Item specification table (customer-specific settings) |
| **Called From** | DataServicecs.cs:3172 → INS_GETRESETSTARTLENGTH() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Item code |
| `P_CUSTOMERID` | VARCHAR2(50) | ✅ | Customer ID |

### Output (OUT)

None - returns result set via cursor

### Returns

| Column | Type | Description |
|--------|------|-------------|
| `RESETSTARTLENGTH` | NUMBER | Reset starting length value (meters) |

---

## Business Logic (What it does and why)

**Purpose**: Retrieve customer-specific reset start length specification for inspection counter.

**When Used**: Customer-specific inspection requirement (added 2022-10-18) for resetting inspection start length. Used in InspectionSession model.

**Business Rules**:
1. Both P_ITMCODE and P_CUSTOMERID required
2. Returns customer-specific reset length specification
3. Some customers require specific starting points for inspection
4. Used to configure inspection counter initial value

**Special Customer Requirements**:
- Certain customers have specific inspection start requirements
- System checks if customer ID requires reset start length
- If found, inspection counter starts from that value instead of zero
- Allows compliance with customer-specific quality protocols

---

## Related Procedures

**Related**: INS_INSERTCONFIRMSTARTING - Records confirmed start length
**Related**: Item specification procedures

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `INS_GETRESETSTARTLENGTH(string P_ITMCODE, string P_CUSTOMERID)`
**Lines**: 3172-3216
**Added**: 2022-10-18 (comment: "New 18/10/22")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETRESETSTARTLENGTH(INS_GETRESETSTARTLENGTHParameter para)`
**Lines**: ~22680-22718

**Parameter Class**: Lines 5503-5506
**Result Class**: Lines 5510-5512

**Usage**: `LuckyTex.AirBag.Core\Models\Inspections.cs` - InspectionSession model

---

**File**: 134/296 | **Progress**: 45.3%
