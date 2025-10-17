# LAB_CHECKRECEIVESAMPLING

**Procedure Number**: 298 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Check if finished fabric sample already received in lab |
| **Operation** | SELECT |
| **Tables** | tblLabSampling, tblLabMassPro (assumed) |
| **Called From** | LABDataService.cs → LAB_CHECKRECEIVESAMPLING() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WEAVLOT` | VARCHAR2(50) | ✅ | Weaving lot number |
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Product item code |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `RESULT` | VARCHAR2(10) | Check result (Y=received, N=not received) |

---

## Business Logic (What it does and why)

Checks if a finished fabric sample from a specific weaving lot has already been received in the laboratory for mass production testing. Prevents duplicate sample entries.

**Workflow**:
1. Receives weaving lot number and item code
2. Queries lab sampling records for finished (mass production) fabric
3. Returns 'Y' if sample already received
4. Returns 'N' if sample not yet received

**Business Rules**:
- Weaving lot and item code combination is unique identifier
- Finished fabric = completed fabric after finishing process (coating, drying)
- Used before receiving sample to prevent duplicates
- Part of mass production lab sample workflow

**Comparison with LAB_CHECKRECEIVEGREIGESAMPLING**:
| Aspect | GREIGE Sampling | Mass Pro Sampling (This) |
|--------|-----------------|--------------------------|
| **Fabric Stage** | Unfinished (greige) | Finished (coated/dried) |
| **Identifier** | Beam roll + Loom | Weaving lot + Item code |
| **Test Type** | Early quality check | Final product testing |
| **Process Stage** | After weaving | After finishing |

**Usage Context**:
- **Mass Production Testing**: Final quality tests on finished fabric
- **Customer Compliance**: Meet customer specifications
- **Sample Management**: One sample per weaving lot

**Typical Usage**:
**Related Workflow**:
1. Finishing process completes (coating, scouring, drying)
2. Quality team takes sample from finishing lot
3. **Check if already received** (this procedure)
4. If not received → Receive sample into lab
5. Perform comprehensive tests:
   - Tensile strength
   - Tear resistance
   - Air permeability
   - Weight/thickness
   - Edge comb
6. Generate test report
7. Approve or reject lot based on results

---

## Related Procedures

**Similar**: [297-LAB_CHECKRECEIVEGREIGESAMPLING.md](./297-LAB_CHECKRECEIVEGREIGESAMPLING.md) - Checks for greige fabric samples
**Downstream**: [299-LAB_GETFINISHINGSAMPLING.md](./299-LAB_GETFINISHINGSAMPLING.md) - Retrieves received samples
**Downstream**: Sample testing procedures (tensile, tear, air permeability, etc.)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_CHECKRECEIVESAMPLING()`
**Lines**: Likely in sample receiving section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_CHECKRECEIVESAMPLING(LAB_CHECKRECEIVESAMPLINGParameter para)`
**Lines**: 4708-4721

**Implementation**:
**Typical Query Logic**:
```sql
SELECT CASE
    WHEN COUNT(*) > 0 THEN 'Y'
    ELSE 'N'
END AS RESULT
FROM tblLabMassProSampling
WHERE WEAVINGLOT = :P_WEAVLOT
  AND ITM_CODE = :P_ITEMCODE
  AND SAMPLETYPE = 'FINISHING'  -- Mass production sample
```

---

**File**: 298/296 | **Progress**: 100.7%
