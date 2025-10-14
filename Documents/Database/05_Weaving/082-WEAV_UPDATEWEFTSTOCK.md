# WEAV_UPDATEWEFTSTOCK

**Procedure Number**: 082 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Updates weft yarn stock consumption during weaving production |
| **Operation** | UPDATE |
| **Called From** | WeavingDataService.cs:1995 → WEAV_UPDATEWEFTSTOCK() |
| **Frequency** | High (Production events) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMLOT` | VARCHAR2 | ✅ | Beam lot number (validated in C#) |
| `P_WEAVINGLOT` | VARCHAR2 | ✅ | Weaving lot number (validated in C#) |
| `P_DOFFNO` | NUMBER | ⬜ | Doff number (fabric roll identifier) |
| `P_LOOMNO` | VARCHAR2 | ⬜ | Loom machine number |
| `P_ITMYARN` | VARCHAR2 | ⬜ | Weft yarn item code |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Operation result message (success/error) |

---

## Business Logic

Updates weft yarn stock inventory based on actual consumption during weaving production. This procedure is critical for:

1. **Inventory Management**: Deduct consumed weft yarn from warehouse stock
2. **Material Traceability**: Link weft yarn consumption to specific production lots
3. **Cost Accounting**: Track material usage for production costing
4. **Stock Replenishment**: Trigger reorder when yarn stock falls below minimum
5. **Production Records**: Maintain accurate material usage history

**Business Rules**:
- **P_BEAMLOT and P_WEAVINGLOT are mandatory** - C# validates both parameters
- Updates yarn stock quantity based on production consumption
- May calculate consumption based on fabric length, width, and weft density
- Links consumption to specific doff for traceability
- Returns result message indicating success or specific error

**Validation in C#**:
```csharp
if (string.IsNullOrWhiteSpace(P_BEAMLOT))
    return results;  // Empty if beam lot not specified
if (string.IsNullOrWhiteSpace(P_WEAVINGLOT))
    return results;  // Empty if weaving lot not specified
```

**Stock Update Logic** (likely in stored procedure):
- Calculate yarn consumption: fabric_length × fabric_width × weft_density
- Deduct calculated amount from yarn pallet stock
- Update stock transaction history
- Check if stock falls below reorder point
- Return success message or error if insufficient stock

**Typical Usage Scenario**:
1. Operator completes a doff (fabric roll)
2. System records production data (length, width)
3. System calls WEAV_UPDATEWEFTSTOCK to update inventory
4. Procedure calculates weft yarn consumed based on production data
5. Yarn pallet stock quantity is reduced
6. If pallet is depleted, status may be updated to "empty"
7. Material tracking record is created linking yarn to fabric roll

**Important for**:
- **Just-in-Time Inventory**: Accurate real-time stock levels
- **Traceability**: If fabric defect found, can trace back to specific yarn pallet
- **ERP Integration**: Stock updates may sync to D365 or AS400 systems

---

## Related Procedures

**Upstream**:
- [060-WEAVE_INSERTUPDATEWEFTYARN.md](./060-WEAVE_INSERTUPDATEWEFTYARN.md) - Assign weft yarn pallets to production
- [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Main weaving process

**Related**:
- [075-WEAV_GETWEFTYARNLISTBYDOFFNO.md](./075-WEAV_GETWEFTYARNLISTBYDOFFNO.md) - Query weft yarn usage by doff
- G3_SEARCHYARNSTOCK (M12-G3) - Query yarn stock levels in warehouse

**Downstream**:
- D365 Integration procedures - Sync inventory updates to ERP
- Inventory reports - Show yarn consumption by item/period

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_UPDATEWEFTSTOCK()`
**Lines**: 1995-2032

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_UPDATEWEFTSTOCK(WEAV_UPDATEWEFTSTOCKParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 082/296 | **Progress**: 27.7%
