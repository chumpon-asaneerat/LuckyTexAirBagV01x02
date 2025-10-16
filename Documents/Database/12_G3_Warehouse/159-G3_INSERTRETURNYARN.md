# G3_INSERTRETURNYARN

**Procedure Number**: 159 | **Module**: M12 - G3 Warehouse | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Record returned yarn from production back to warehouse stock |
| **Operation** | INSERT |
| **Tables** | tblG3ReturnYarn, tblG3Stock |
| **Called From** | G3DataService.cs:934 → G3_INSERTRETURNYARN() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_TRACENO` | VARCHAR2(50) | ✅ | Original trace number (from issued pallet) |
| `P_NEWTRACENO` | VARCHAR2(50) | ✅ | New trace number for returned yarn |
| `P_CH` | NUMBER | ⚠️ | Cone/cheese count returned |
| `P_WEIGHT` | NUMBER | ⚠️ | Weight quantity returned (kg) |
| `P_RECEIVEDATE` | DATE | ⚠️ | Return receiving date/time |
| `P_OPERATOR` | VARCHAR2(50) | ✅ | Operator who processed return |
| `P_ITEMYARN` | VARCHAR2(50) | ⚠️ | Yarn item code |
| `P_YARNTYPE` | VARCHAR2(50) | ⚠️ | Yarn type classification |
| `P_RETURNBY` | VARCHAR2(50) | ⚠️ | Production operator who returned yarn |

### Output (OUT)

None - Returns success/failure result

### Returns (if cursor)

None - Procedure executes INSERT operation only

---

## Business Logic (What it does and why)

**Purpose**: Records unused yarn returned from production back to warehouse inventory. When production line receives more yarn than needed, excess is returned to warehouse. This maintains accurate inventory and enables material reuse.

**When Used**:
- Production line has excess yarn after completing job
- Machine breakdown - unused yarn returned
- Job cancellation - all yarn returned
- Material swap - wrong yarn returned, correct yarn issued
- End of shift - partial pallet returned

**Business Rules**:
1. **New Trace Number**: Returned yarn gets new trace number (separate from original issue)
2. **Original Reference**: P_TRACENO links return to original issue
3. **Required Fields**: TRACENO, NEWTRACENO, and OPERATOR must be provided
4. **Stock Addition**: Increases available warehouse stock
5. **Return Tracking**: Maintains who returned (production) and who received (warehouse)

**Workflow**:

**Scenario 1: Excess Yarn Return After Job Completion**
1. Weaving machine WV-001 completes order
2. Original issue: 500kg (full pallet PLT-001234, TRACENO = "TRACE-001234")
3. Used: 420kg
4. Remaining: 80kg (16 cones)
5. Production supervisor returns excess:
   - P_TRACENO = "TRACE-001234" (original issue trace)
   - P_NEWTRACENO = "TRACE-RET-001234" (new return trace)
   - P_CH = 16 (cones returned)
   - P_WEIGHT = 80 (kg)
   - P_RECEIVEDATE = Current timestamp
   - P_OPERATOR = "WH-OP-125" (warehouse operator receiving)
   - P_ITEMYARN = "YARN-PA66-001"
   - P_YARNTYPE = "PA66-40D"
   - P_RETURNBY = "PROD-OP-088" (production operator)
6. System inserts return record
7. Stock increased by 80kg
8. Returned yarn available for next issue

**Scenario 2: Machine Breakdown - Full Return**
1. Machine WV-003 breaks down after receiving yarn
2. Maintenance estimates 2-day repair
3. Warehouse recalls issued pallet:
   - Original: 500kg, 50 cones (TRACE-005678)
   - Return: Full pallet unused
   - P_TRACENO = "TRACE-005678"
   - P_NEWTRACENO = "TRACE-RET-005678"
   - P_CH = 50 (all cones)
   - P_WEIGHT = 500 (full weight)
   - P_RETURNBY = "MAINT-SUPER-05"
4. System records full return
5. Pallet can be issued to another machine

**Scenario 3: Wrong Material Issued - Return and Reissue**
1. Operator mistakenly issues wrong yarn type:
   - Issued: YARN-PA66-001 (500kg, TRACE-009999)
   - Required: YARN-NYLON-001
2. Production immediately returns wrong yarn:
   - P_TRACENO = "TRACE-009999"
   - P_NEWTRACENO = "TRACE-RET-009999"
   - P_CH = 50 (unused)
   - P_WEIGHT = 500 (full return)
   - P_ITEMYARN = "YARN-PA66-001"
   - P_RETURNBY = "PROD-LEAD-12"
3. System records return
4. Warehouse issues correct material

**Scenario 4: End of Shift Partial Return**
1. Night shift ends, machine stops mid-job
2. Partial pallet on machine:
   - Original: 500kg issued (TRACE-012345)
   - Used: 180kg
   - Remaining: 320kg, 32 cones
3. Shift supervisor returns remaining:
   - P_NEWTRACENO = "TRACE-RET-012345"
   - P_WEIGHT = 320
   - P_CH = 32
   - P_RECEIVEDATE = Shift end time
   - P_RETURNBY = "SHIFT-SUP-NIGHT"
4. Day shift can use returned yarn or new pallet

**Scenario 5: Quality Issue - Return for Inspection**
1. Production notices yarn quality problem
2. Returns entire pallet for inspection:
   - P_NEWTRACENO = "TRACE-RET-QC-001"
   - Mark as quality hold
   - P_RETURNBY indicates production operator
3. QC team inspects returned yarn
4. If pass: returned yarn back to available stock
5. If fail: yarn quarantined/scrapped

**Two Trace Numbers - Why?**
- **P_TRACENO**: Original issue trace (links to when/where it was issued)
- **P_NEWTRACENO**: New return trace (tracks return transaction separately)
- Enables: Full traceability of issue → usage → return cycle

**Why This Matters**:
- **Inventory Accuracy**: Returns increase stock, preventing understatement
- **Material Reuse**: Unused yarn can be issued to other jobs
- **Cost Control**: Tracks actual material consumption vs. issued
- **Traceability**: Links returns to original issues for complete material history
- **Production Efficiency**: Quick returns enable rapid material reallocation
- **Accountability**: Tracks both production (who returned) and warehouse (who received)

---

## Related Procedures

**Upstream**:
- G3_INSERTUPDATEISSUEYARN (original yarn issue)
- G3_GETREQUESTNODETAIL (identifies what was issued)

**Downstream**:
- G3_UPDATEYARN (updates stock quantity after return)
- G3_SEARCHYARNSTOCK (shows updated stock including returns)
- G3_GETPALLETDETAIL (returned yarn available for new issues)

**Similar**:
- G3_RECEIVEYARN (receiving from supplier - similar concept, external source)
- G3_INSERTRETURNYARN (this procedure - internal return from production)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\G3DataService.cs`
**Method**: `G3_INSERTRETURNYARN(string P_TRACENO, string P_NEWTRACENO, decimal? P_CH, decimal? P_WEIGHT, DateTime? P_RECEIVEDATE, string P_OPERATOR, string P_ITEMYARN, string P_YARNTYPE, string P_RETURNBY)`
**Lines**: 934-982

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `G3_INSERTRETURNYARN(G3_INSERTRETURNYARNParameter para)`
**Lines**: 6868-6889 (Parameter: 6868-6879, Result: 6885-6887)

---

**File**: 159/296 | **Progress**: 53.7%
