# INS_INSERTCONFIRMSTARTING

**Procedure Number**: 136 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Record confirmed starting length for inspection lot |
| **Operation** | UPDATE |
| **Tables** | tblINSLot (CONFIRMSTARTLENGTH field) |
| **Called From** | DataServicecs.cs:3083 → INS_INSERTCONFIRMSTARTING() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSID` | VARCHAR2(50) | ✅ | Inspection ID |
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_CONFIRMSTART` | NUMBER | ⬜ | Confirmed starting length (meters) |

### Output (OUT)

None (empty result indicates success)

### Returns

Empty result object. Success determined by non-null return in C# code (returns empty string on success, error message on failure).

---

## Business Logic (What it does and why)

**Purpose**: Record the confirmed starting length when beginning inspection process.

**When Used**:
- At inspection start
- Operator confirms the starting counter length
- System records this value for traceability and length calculations
- Added 2022-08-23 for improved length tracking

**Business Rules**:
1. Both P_INSID and P_INSLOT required
2. Updates CONFIRMSTARTLENGTH field in inspection lot record
3. Used with customer-specific reset start length feature
4. Ensures accurate net length calculations

**Inspection Start Workflow**:
1. Operator starts new inspection lot
2. System may load reset start length (if customer requires it)
3. Operator confirms starting counter value
4. System calls INS_INSERTCONFIRMSTARTING to record value
5. This confirmed start is used for all subsequent length calculations
6. Ensures traceability and accuracy in final length reporting

**Why Confirmation is Important**:
- Counter may not be at zero (continuous inspection)
- Customer-specific starting requirements
- Accurate net length calculation depends on correct start value
- Audit trail for length discrepancies

---

## Related Procedures

**Related**: [134-INS_GETRESETSTARTLENGTH.md](./134-INS_GETRESETSTARTLENGTH.md) - Get customer-specific reset start
**Related**: [133-INS_GETNETLENGTH.md](./133-INS_GETNETLENGTH.md) - Calculate net length (uses confirmed start)
**Upstream**: Called after inspection lot is created
**Downstream**: Confirmed start value used in all length calculations

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `INS_INSERTCONFIRMSTARTING(string P_INSID, string P_INSLOT, decimal? P_CONFIRMSTART)`
**Lines**: 3083-3121
**Added**: Related to CONFIRMSTARTLENGTH feature (2022-08-23)

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_INSERTCONFIRMSTARTING(INS_INSERTCONFIRMSTARTINGParameter para)`
**Lines**: ~22580-22620

**Parameter Class**: Lines ~5467-5471
**Result Class**: Lines ~5477-5479 (empty)

**Usage**: `LuckyTex.AirBag.Core\Models\Inspections.cs`
**Method**: `INS_INSERTCONFIRMSTARTING(decimal? P_CONFIRMSTART)`
**Line**: ~3200-3211
**Context**: InspectionSession model - records confirmed start at inspection begin

---

**File**: 136/296 | **Progress**: 45.9%
