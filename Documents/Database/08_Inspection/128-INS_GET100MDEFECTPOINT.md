# INS_GET100MDEFECTPOINT

**Procedure Number**: 128 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Calculate defect points within current 100-meter section for quality grading |
| **Operation** | SELECT (calculation) |
| **Tables** | tblINSDefect (assumed) |
| **Called From** | DataServicecs.cs:3125 → INS_GET100MDEFECTPOINT() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number |
| `P_DEFECTID` | VARCHAR2(50) | ✅ | Current defect ID being processed |
| `P_LENGTH1` | NUMBER | ⬜ | Start length position (for long defects) or defect position (for point defects) |
| `P_LENGTH2` | NUMBER | ⬜ | End length position (for long defects only, null for point defects) |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_POINT` | NUMBER | Total defect points in current 100M section |

### Returns

Returns single decimal value: total defect points accumulated in the current 100-meter section.

---

## Business Logic (What it does and why)

**Purpose**: Calculate total defect points within the current 100-meter section to determine if quality threshold is exceeded.

**When Used**: **Real-time during inspection** - called immediately after operator enters each defect to check if the 100M section has accumulated too many defect points (quality alert system).

**Business Rules**:
1. Requires valid INSLOT and DEFECTID (both cannot be null/empty)
2. Calculates defect points for current 100-meter section only
3. **Critical Quality Rule**: If defect points exceed 10 points in any 100M section, system alerts operator
4. Works with two defect types:
   - **Point defects**: Single position (LENGTH2 is null)
   - **Long defects**: Start and end position (both LENGTH1 and LENGTH2 provided)
5. Called in real-time as defects are recorded during inspection
6. Used for immediate quality feedback to operator

**100-Meter Quality System**:
- Fabric is evaluated in 100-meter sections
- Each defect type has point value (based on severity)
- Total points per 100M section determines grade
- **> 10 points in any 100M section** = Quality alert (system warns operator)
- Helps catch quality issues during inspection, not after completion

**Workflow**:
1. **Point Defect Entry**:
   - Operator enters defect code and position at current counter length
   - System calls INS_GET100MDEFECTPOINT with current length (LENGTH1 only)
   - Stored procedure calculates points in current 100M section
   - If > 10 points: "Defect in 100 m Over 10 Point" alert displayed

2. **Long Defect Entry** (defects spanning multiple meters):
   - Operator marks defect start position
   - Operator marks defect end position when fabric advances
   - System calls INS_GET100MDEFECTPOINT with start (LENGTH1) and end (LENGTH2)
   - Stored procedure calculates points for the defect span
   - If > 10 points: Alert displayed

3. **Quality Monitoring**:
   - Continuous monitoring during inspection process
   - Immediate feedback if section quality deteriorates
   - Allows operator to notify production team in real-time
   - Prevents shipping fabric with excessive defects

**Why Real-Time Calculation**:
- Early warning system for quality issues
- Allows stopping production if patterns emerge
- Better than discovering issues after inspection complete
- Helps maintain customer quality standards

---

## Related Procedures

**Related**: INS_GETDEFECTLISTREPORT - Get defect list for reporting (not documented yet)
**Related**: GETGRADE - Get final grade based on total defect points (not documented yet)
**Related**: INS_GETTOTALDEFECTBYINSLOT - Get total defects for entire lot (not documented yet)
**Downstream**: Quality grading procedures use accumulated defect points

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `INS_GET100MDEFECTPOINT(string P_INSLOT, string P_DEFECTID, decimal? P_LENGTH1, decimal? P_LENGTH2)`
**Lines**: 3125-3168

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GET100MDEFECTPOINT(INS_GET100MDEFECTPOINTParameter para)`
**Lines**: 22952-22997 (estimated based on pattern)

**Parameter Class**: `INS_GET100MDEFECTPOINTParameter`
**Lines**: 5746-5752 (4 parameters)

**Result Class**: `INS_GET100MDEFECTPOINTResult`
**Lines**: 5758-5761 (returns R_POINT)

**Usage Locations**:
1. `LuckyTex.AirBag.Core\Models\Inspections.cs`
   - Line 3230: INS_GET100MDEFECTPOINT method (point defects)
   - Line 3262: INS_GET100MDEFECTPOINTLongDefect method (long defects)

2. `LuckyTex.AirBag.Pages\Pages\08 - Inspection\InspectionModulePage.xaml.cs`
   - Line 2188: Point defect entry (after operator enters defect code)
   - Line 1927: Long defect completion (after operator marks end position)

**Context**: Called immediately after defect entry to check 100M section quality threshold

---

**File**: 128/296 | **Progress**: 43.2%
