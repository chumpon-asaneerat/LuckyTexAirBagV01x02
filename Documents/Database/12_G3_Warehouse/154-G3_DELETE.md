# G3_DELETE

**Procedure Number**: 154 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Delete yarn/material pallet record from G3 warehouse stock |
| **Operation** | DELETE |
| **Tables** | tblG3Stock (or similar G3 warehouse table) |
| **Called From** | G3DataService.cs:1042 → G3_Del() |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `PALLETNO` | VARCHAR2(50) | ✅ | Pallet number (primary key) |
| `TRACENO` | VARCHAR2(50) | ✅ | Trace number (tracking ID) |
| `ITM_YARN` | VARCHAR2(50) | ✅ | Yarn item code |
| `LOTNO` | VARCHAR2(50) | ✅ | Lot number |

### Output (OUT)

None - Returns success/failure result

### Returns (if cursor)

None - Procedure executes DELETE operation only

---

## Business Logic (What it does and why)

**Purpose**: Removes a yarn or material pallet record from the G3 warehouse stock system. Used when correcting erroneous entries or removing cancelled pallets.

**When Used**:
- Warehouse operator deletes incorrect pallet entry
- Cancelling a received pallet (wrong material received)
- Removing duplicate pallet records
- Data correction operations
- System cleanup for test/training data

**Business Rules**:
1. **Four-Field Key**: Requires all four fields to uniquely identify the pallet record (PALLETNO, TRACENO, ITM_YARN, LOTNO)
2. **Validation Required**: All four parameters must be provided (not null/empty)
3. **No Soft Delete**: This is a hard delete operation - record is permanently removed
4. **Stock Impact**: Deleting reduces available stock quantity
5. **Audit Trail**: Deletion should be logged for traceability

**Workflow**:

**Scenario 1: Delete Incorrect Pallet Entry**
1. Warehouse operator receives pallet "PLT-2024-001234"
2. Accidentally enters wrong item code during receiving
3. Realizes mistake immediately
4. Uses delete function with:
   - PALLETNO = "PLT-2024-001234"
   - TRACENO = "TRACE-001"
   - ITM_YARN = "YARN-WRONG" (incorrect item)
   - LOTNO = "LOT-2024-100"
5. System deletes the incorrect record
6. Operator re-enters with correct item code

**Scenario 2: Remove Cancelled Pallet**
1. Pallet received and entered into system
2. Quality inspection fails - pallet rejected
3. Pallet sent back to supplier
4. Supervisor deletes pallet record:
   - All four identification fields provided
5. System removes pallet from stock
6. Stock quantity adjusted automatically

**Scenario 3: Clean Up Duplicate Entry**
1. System detects duplicate pallet entries
2. Investigation shows pallet was scanned twice
3. Supervisor verifies which is duplicate
4. Deletes duplicate record using all four keys
5. Keeps correct record only

**Why This Matters**:
- **Data Accuracy**: Ensures warehouse stock reflects actual physical inventory
- **Stock Integrity**: Prevents overstatement of available materials
- **Traceability**: Maintains clean records for material tracking
- **Error Correction**: Allows fixing data entry mistakes
- **Compliance**: Supports inventory audit requirements

---

## Related Procedures

**Upstream**:
- G3_INSERTYARN (creates records that may need deletion)
- G3_RECEIVEYARN (receiving creates records)
- G3_UPDATEYARN (alternative to delete - updates instead)

**Downstream**:
- G3_SEARCHYARNSTOCK (checks stock after deletion)
- G3_GETPALLETDETAIL (verifies pallet no longer exists)

**Similar**:
- G3_CANCELREQUESTNO (cancels issue requests)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_Del(string _PALLETNO, string _TRACENO, string _ITM_YARN, string _LOTNO)`
**Lines**: 1042-1085

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_Delete(G3_DeleteParameter para)`
**Lines**: 7106-7123 (Parameter: 7114-7121, Result: 7106-7108)

---

**File**: 154/296 | **Progress**: 52.0%
