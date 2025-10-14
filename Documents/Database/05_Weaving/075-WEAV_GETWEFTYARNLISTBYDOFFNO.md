# WEAV_GETWEFTYARNLISTBYDOFFNO

**Procedure Number**: 075 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieves weft yarn pallet list used for a specific beam roll and doff number |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:803 → WEAV_GETWEFTYARNLISTBYDOFFNO() |
| **Frequency** | Medium |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2 | ✅ | Beam roll number |
| `P_DOFFNO` | NUMBER | ✅ | Doff number (fabric roll identifier) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMLOT` | VARCHAR2 | Beam lot number |
| `DOFFNO` | NUMBER | Doff number |
| `PALLETNO` | VARCHAR2 | Weft yarn pallet number |
| `CHLOTNO` | VARCHAR2 | China lot number (yarn lot from supplier) |
| `ADDDATE` | DATE | Date when weft yarn was added/registered |
| `ADDBY` | VARCHAR2 | Operator who added the weft yarn |
| `USETYPE` | VARCHAR2 | Usage type classification |
| `REMARK` | VARCHAR2 | Additional remarks |

---

## Business Logic

Retrieves the list of weft yarn pallets used in a specific weaving production run (identified by beam roll and doff number). This is critical for:

1. **Material Traceability**: Track which weft yarn lots were consumed to produce a specific fabric roll
2. **Quality Investigation**: If fabric defects are found, trace back to specific yarn batches
3. **Inventory Management**: Record which weft yarn pallets were consumed during production
4. **Production Documentation**: Maintain complete material usage records for each doff

**Business Rules**:
- One doff (fabric roll) can use multiple weft yarn pallets if yarn runs out during production
- Each pallet record includes the China lot number (supplier's batch identifier) for full traceability
- USETYPE may indicate whether yarn was used normally or for special purposes (testing, samples)
- Records who added the weft yarn (ADDBY) for accountability

**Typical Usage Scenario**: During weaving production, when a doff is completed, the system displays all weft yarn pallets that were consumed. If quality issues arise later, QA can trace back to specific yarn lots.

---

## Related Procedures

**Upstream**:
- [060-WEAVE_INSERTUPDATEWEFTYARN.md](./060-WEAVE_INSERTUPDATEWEFTYARN.md) - Insert/update weft yarn pallet assignments
- [057-WEAVE_DELETEWEFTYARN.md](./057-WEAVE_DELETEWEFTYARN.md) - Delete weft yarn records

**Downstream**:
- Quality inspection procedures - Use weft yarn data for defect analysis

**Similar**:
- [058-WEAVE_GETBEAMLOTDETAIL.md](./058-WEAVE_GETBEAMLOTDETAIL.md) - Get warp beam details (warp yarn traceability)

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETWEFTYARNLISTBYDOFFNO()`
**Lines**: 803-845

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETWEFTYARNLISTBYDOFFNO(WEAV_GETWEFTYARNLISTBYDOFFNOParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 075/296 | **Progress**: 25.3%
