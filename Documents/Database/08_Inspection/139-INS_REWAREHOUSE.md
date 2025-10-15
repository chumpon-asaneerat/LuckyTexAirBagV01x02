# INS_REWAREHOUSE

**Procedure Number**: 139 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Transfer inspection data to new inspection lot (re-inspection/rework) |
| **Operation** | UPDATE/INSERT |
| **Tables** | tblINSLot, tblINSDefect, tblINSTestRecord |
| **Called From** | DataServicecs.cs:2997 → INS_REWAREHOUSE() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSOLD` | VARCHAR2(50) | ✅ | Old inspection lot number |
| `P_DEFECTID` | VARCHAR2(50) | ⬜ | Defect ID to transfer |
| `P_TESTID` | VARCHAR2(50) | ⬜ | Test record ID to transfer |
| `P_INSNEW` | VARCHAR2(50) | ✅ | New inspection lot number |

### Output (OUT)

None (empty result indicates success)

### Returns

Empty result object. Success determined by non-null return in C# code.

---

## Business Logic (What it does and why)

**Purpose**: Transfer inspection data from old lot to new lot when fabric is re-inspected or reworked.

**When Used**:
- Fabric fails inspection and needs rework
- After rework, fabric returns to inspection
- Need to link old inspection data to new inspection lot
- Maintain quality traceability across re-inspections

**Business Rules**:
1. Both P_INSOLD and P_INSNEW required
2. Transfers or links:
   - Defect records (if P_DEFECTID provided)
   - Test records (if P_TESTID provided)
   - References old lot in new lot record
3. Maintains quality history and traceability
4. Used in ProcessControlPage for re-warehousing workflow

**Re-Inspection Workflow**:
1. Fabric inspected (old inspection lot)
2. Grade fails or defects found
3. Fabric sent for rework (repair/reprocess)
4. Fabric returns to inspection (new inspection lot)
5. System calls INS_REWAREHOUSE to:
   - Create link between old and new lots
   - Transfer relevant defect/test data
   - Maintain complete quality history
6. New inspection can reference old results

**Why "REWAREHOUSE"**:
- Fabric returns to warehouse/inspection queue
- Not first-time inspection (re-inspection)
- Quality tracking requires linking inspection attempts
- Customer may request complete inspection history

---

## Related Procedures

**Related**: UPDATEINSPECTIONPROCESS - Update inspection status
**Related**: INSERTINSPECTIONPROCESS - Create new inspection lot
**Used with**: ProcessControlPage re-warehousing functionality

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs`
**Method**: `INS_REWAREHOUSE(string P_INSOLD, string P_DEFECTID, string P_TESTID, string P_INSNEW)`
**Lines**: 2997-3034
**Comment**: "เพิ่มใหม่ INS_REWAREHOUSE ใช้ในหน้า ProcessControlPage" (Translation: "Added new INS_REWAREHOUSE used in ProcessControlPage")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_REWAREHOUSE(INS_REWAREHOUSEParameter para)`

**Parameter Class**: 4 parameters
**Result Class**: Empty result

**Usage**: `LuckyTex.AirBag.Core\Models\Inspections.cs`
**Method**: `INS_REWAREHOUSE(string P_INSOLD, string P_DEFECTID, string P_TESTID, string P_INSNEW)`
**Line**: ~3194-3196
**Context**: InspectionSession model - handles re-inspection workflow

---

**File**: 139/296 | **Progress**: 47.0%
