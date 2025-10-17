# LAB_GETWEAVINGLOTLIST

**Procedure Number**: 303 | **Module**: M14 - LAB (MES Module) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get list of weaving lots for finishing lot and item code |
| **Operation** | SELECT |
| **Tables** | tblFinishing, tblWeaving (joined - assumed) |
| **Called From** | LABDataService.cs → LAB_GETWEAVINGLOTLIST() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINISHINGLOT` | VARCHAR2(50) | ✅ | Finishing lot number |
| `P_ITEMCODE` | VARCHAR2(50) | ✅ | Product item code |
| `P_PROCESS` | VARCHAR2(50) | ✅ | Finishing process type (COATING/SCOURING/DRYER) |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2(50) | Weaving lot number |

---

## Business Logic (What it does and why)

Retrieves list of weaving (greige) lots that were used to create a specific finishing lot. Essential for lab sample traceability and material tracking.

**Workflow**:
1. Receives finishing lot number, item code, and process type
2. Queries finishing process records to find source weaving lots
3. Returns list of all weaving lots in the finishing lot
4. May return multiple weaving lots (one finishing lot can combine multiple greige rolls)

**Business Rules**:
- One finishing lot can contain multiple weaving lots
- All weaving lots in finishing lot must be same item code
- Process type identifies which finishing step (coating, scouring, dryer)
- Used for complete material traceability in lab testing

**Traceability Chain**:
**Lab Testing Context**:

When lab receives finishing sample, they need to trace back to source:
1. **Finishing Lot**: FN-2025-001
2. **Query weaving lots**: LAB_GETWEAVINGLOTLIST('FN-2025-001', 'X12345', 'COATING')
3. **Results**: WV-2025-101, WV-2025-102, WV-2025-103
4. **Lab tracking**: Sample from finishing lot contains 3 weaving lots

**Why Multiple Weaving Lots?**
- Coating/scouring machines process continuous fabric
- Multiple greige rolls (weaving lots) fed into finishing process
- All get coated together → one finishing lot
- Lab needs to know all source lots for quality traceability

**Process Type Significance**:
- **COATING**: After silicone coating applied
- **SCOURING**: After washing/scouring process
- **DRYER**: After drying process

Different processes may have different weaving lot tracking:
**Quality Investigation Use Case**:
1. Lab test on finishing lot fails
2. Get source weaving lots (this procedure)
3. Check if issue existed in greige fabric
4. Check greige test results for those weaving lots
5. Determine if issue from weaving or finishing process
6. Take corrective action at correct process stage

**Data Relationship**:
---

## Related Procedures

**Related**: [299-LAB_GETFINISHINGSAMPLING.md](./299-LAB_GETFINISHINGSAMPLING.md) - Gets finishing sample details
**Related**: LAB_GETWEAVINGSAMPLING - Gets greige sample details
**Upstream**: Finishing process creates finishing lots from weaving lots
**Used For**: Lab sample traceability and root cause analysis

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\LABDataService.cs`
**Method**: `LAB_GETWEAVINGLOTLIST()`
**Lines**: Likely in traceability section

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `LAB_GETWEAVINGLOTLIST(LAB_GETWEAVINGLOTLISTParameter para)`
**Lines**: 4060-4075

**Implementation Notes**:
- Returns list of weaving lot numbers only (no other details)
- Simple traceability lookup
- May return 1 to many weaving lots per finishing lot
- Essential for quality traceability in lab testing

---

**File**: 303/296 | **Progress**: 102.4%
