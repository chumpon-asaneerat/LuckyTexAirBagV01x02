# LAB_CHECKRECEIVEGREIGESAMPLING

**Procedure Number**: 297 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Check if greige (unfinished) fabric sample already received in lab |
| **Operation** | SELECT |
| **Tables** | tblLabSampling, tblLabGreige (assumed) |
| **Called From** | LABDataService.cs → LAB_CHECKRECEIVEGREIGESAMPLING() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMERROLL` | VARCHAR2(50) | ✅ | Beam roll number (from beaming process) |
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `RESULT` | VARCHAR2(10) | Check result (Y=received, N=not received) |

---

## Business Logic (What it does and why)

Checks if a greige fabric sample from a specific beam roll and loom has already been received in the laboratory. Prevents duplicate sample entries.

**Workflow**:
1. Receives beam roll number and loom number
2. Queries lab sampling records for greige fabric
3. Returns 'Y' if sample already received
4. Returns 'N' if sample not yet received

**Business Rules**:
- Beam roll and loom number combination is unique identifier
- Greige fabric = unfinished fabric (before coating/finishing)
- Used before receiving sample to prevent duplicates
- Part of lab sample receiving workflow

**Usage Context**:
- **Greige Testing**: Tests on raw woven fabric before finishing
- **Quality Control**: Early quality assessment
- **Sample Tracking**: Ensures one sample per beam roll/loom combination

**Related Workflow**:
1. Weaving process creates greige fabric on loom
2. Quality team takes sample from beam roll
3. **Check if already received** (this procedure)
4. If not received → Receive sample into lab
5. Perform greige tests (strength, weight, thread count, etc.)
6. Record results for quality assessment

---

## Related Procedures

**Similar**: [298-LAB_CHECKRECEIVESAMPLING.md](./298-LAB_CHECKRECEIVESAMPLING.md) - Checks for finished fabric samples
**Downstream**: Sample receiving procedures
**Related**: LAB_GREIGESTOCKSTATUS - View greige sample inventory

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_CHECKRECEIVEGREIGESAMPLING()`
**Lines**: Likely in sample receiving section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_CHECKRECEIVEGREIGESAMPLING(LAB_CHECKRECEIVEGREIGESAMPLINGParameter para)`
**Lines**: 4727-4740

**Implementation**:
**Typical Query Logic**:
```sql
SELECT CASE
    WHEN COUNT(*) > 0 THEN 'Y'
    ELSE 'N'
END AS RESULT
FROM tblLabGreigeSampling
WHERE BEAMERROLL = :P_BEAMERROLL
  AND LOOMNO = :P_LOOMNO
```

---

**File**: 297/296 | **Progress**: 100.3%
