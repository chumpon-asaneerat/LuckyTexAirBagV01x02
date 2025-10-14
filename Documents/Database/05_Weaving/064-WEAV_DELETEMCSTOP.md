# WEAV_DELETEMCSTOP

**Procedure Number**: 064 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete machine stop record from weaving production |
| **Operation** | DELETE |
| **Called From** | WeavingDataService.cs:1919 → WEAV_DELETEMCSTOP() |
| **Frequency** | Low - Manual correction when stop record entered incorrectly |
| **Performance** | Fast - Single record deletion by composite key |
| **Issues** | 🟡 1 Low - No transaction handling visible |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom/weaving machine number |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll) |
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll number |
| `P_DEFECT` | VARCHAR2(50) | ✅ | Defect code causing the stop |
| `P_DATE` | DATE | ✅ | Date/time of the machine stop |

### Output (OUT)

None - Success determined by non-null result object

### Returns

Empty result object indicating success/failure status

---

## Business Logic (What it does and why)

**Purpose**: Removes incorrectly recorded machine stop events from weaving production history.

**Business Context**:
- During weaving operations, operators record every machine stop with cause (defect code)
- If a stop is recorded incorrectly (wrong defect code, wrong time, duplicate entry), supervisor can delete it
- Machine stop data is critical for OEE (Overall Equipment Effectiveness) calculations
- Incorrect stop records skew downtime analysis and machine performance metrics

**Usage Scenario**:
1. Operator accidentally records a machine stop with wrong defect code
2. Supervisor reviews stop history and identifies the error
3. Supervisor opens machine stop management screen
4. Selects the incorrect stop record from the list
5. Clicks "Delete" button
6. System calls WEAV_DELETEMCSTOP with all identifying parameters
7. Stop record is removed from database
8. OEE and downtime calculations are recalculated without the erroneous data

**Business Rules**:
- Requires complete composite key (all 5 parameters) to prevent accidental mass deletion
- Should require supervisor authorization (not operator level)
- Should log the deletion action for audit trail
- May need to recalculate production efficiency metrics after deletion

**Data Integrity Concerns**:
- ⚠️ No visible transaction handling in C# code
- Should verify the stop record exists before attempting deletion
- Should check if stop record has been used in reports (may need soft delete instead)
- Should prevent deletion of stops from completed/closed production lots

**Related Data Impact**:
- OEE calculations need recalculation after stop deletion
- Downtime reports may need refresh
- Machine performance metrics affected

---

## Related Procedures

**Upstream (Machine Stop Management)**:
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Create machine stop record
- [WEAV_GETMCSTOPLISTBYDOFFNO](./WEAV_GETMCSTOPLISTBYDOFFNO.md) - List stops to identify record to delete
- [WEAV_GETMCSTOPBYLOT](./WEAV_GETMCSTOPBYLOT.md) - View stop history by lot

**Downstream (After Deletion)**:
- May trigger recalculation of production metrics
- May need to refresh dashboard displays

**Similar Operations**:
- [WEAV_DELETEWEAVINGLOT](./WEAV_DELETEWEAVINGLOT.md) - Delete entire weaving lot (more severe)
- Other module DELETE operations for data correction

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_DELETEMCSTOP()`
**Lines**: 1919-1949

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_DELETEMCSTOP(WEAV_DELETEMCSTOPParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 064/296 | **Progress**: 21.6%
