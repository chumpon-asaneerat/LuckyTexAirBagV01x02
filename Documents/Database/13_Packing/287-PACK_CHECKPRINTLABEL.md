# PACK_CHECKPRINTLABEL

**Procedure Number**: 287 | **Module**: M13 - Packing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Check if packing label has been printed for inspection lot |
| **Operation** | SELECT |
| **Tables** | tblPackingLabel or tblPackingPallet (assumed) |
| **Called From** | PackingDataService.cs:725 → PACK_CHECKPRINTLABEL() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_INSLOT` | VARCHAR2(50) | ✅ | Inspection lot number to check |

### Output (OUT)

No output parameters - returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `PRINTDATE` | DATE | Date/time when label was printed (NULL if not printed) |

---

## Business Logic (What it does and why)

Checks if a packing label has already been printed for an inspection lot. Prevents duplicate label printing and validates packing status.

**Workflow**:
1. Receives inspection lot number
2. Queries label printing history
3. Returns print date if label was printed
4. Returns NULL if label not yet printed

**Business Rules**:
- Inspection lot must exist
- Used to prevent duplicate label printing
- Validates lot is ready for packing
- Part of packing workflow validation

**Usage Scenarios**:
- Before printing new label: Check if already printed
- Packing validation: Verify lot has been labelled
- Reprint prevention: Block duplicate labels
- Audit trail: Track when labels were generated

**Return Value Logic**:
---

## Related Procedures

**Related**: [291-PACK_PRINTLABEL.md](./291-PACK_PRINTLABEL.md) - Actually prints the label
**Upstream**: Inspection process completes lot
**Downstream**: Prevents duplicate printing

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\PackingDataService.cs`
**Method**: `PACK_CHECKPRINTLABEL()`
**Lines**: 709-738

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `PACK_CHECKPRINTLABEL(PACK_CHECKPRINTLABELParameter para)`
**Lines**: 2352-2365

---

**File**: 287/296 | **Progress**: 97.0%
