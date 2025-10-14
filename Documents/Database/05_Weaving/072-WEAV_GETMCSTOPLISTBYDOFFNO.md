# WEAV_GETMCSTOPLISTBYDOFFNO

**Procedure Number**: 072 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get machine stops for specific doff/fabric roll |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:1160 → WEAV_GETMCSTOPLISTBYDOFFNO() |
| **Frequency** | Medium - Quality review per fabric roll |
| **Performance** | Fast - Indexed by composite key |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2(50) | ✅ | Loom machine number |
| `P_DOFFNO` | NUMBER | ✅ | Doff/fabric roll number |
| `P_BEAMROLL` | VARCHAR2(50) | ✅ | Beam roll number |
| `P_WEAVELOT` | VARCHAR2(50) | ✅ | Weaving lot number |

**Note**: All 4 parameters required to uniquely identify the doff

### Output (OUT)

None

### Returns (Cursor - Multiple Records)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `DEFECTCODE` | VARCHAR2 | Defect code causing stop |
| `DEFECTPOSITION` | NUMBER | Meter position where defect occurred |
| `CREATEBY` | VARCHAR2 | Operator who recorded stop |
| `CREATEDATE` | DATE | When stop was recorded |
| `REMARK` | VARCHAR2 | Additional notes |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `BEAMERROLL` | VARCHAR2 | Beam roll number |
| `DOFFNO` | NUMBER | Doff number |
| `DEFECTLENGTH` | NUMBER | Length of defective section (meters) |
| `STOPDATE` | DATE | When machine stopped |
| `DESCRIPTION` | VARCHAR2 | Defect description (from master) |

---

## Business Logic (What it does and why)

**Purpose**: Retrieves machine stop history for a specific fabric roll (doff), enabling quality tracking and defect mapping for that exact roll.

**Business Context**:
- **Doff**: A "doff" is one complete fabric roll produced on a loom
- Each doff may have multiple stops during production
- After doffing (removing fabric roll from loom), quality must be checked
- Stop history helps identify defect locations on the specific roll
- Critical for:
  - Roll-level quality inspection
  - Defect marking on finished roll
  - Roll grading decisions (A, B, C grade)
  - Customer quality documentation

**Doff-Specific Tracking**:
- Unlike WEAV_GETMCSTOPBYLOT (which gets ALL stops for entire lot)
- This gets stops for ONE specific fabric roll
- More granular for per-roll quality control
- Used when roll is being inspected or packaged

**Usage Scenarios**:

**Scenario 1: Post-Doff Quality Inspection**
1. Operator completes doff #5 from lot WV-2024-001
2. Inspector takes roll to inspection station
3. Scans doff barcode → System loads stop history
4. System calls:
   ```
   WEAV_GETMCSTOPLISTBYDOFFNO(
       P_LOOMNO: "L-001",
       P_DOFFNO: 5,
       P_BEAMROLL: "BM-2024-100",
       P_WEAVELOT: "WV-2024-001"
   )
   ```
5. Returns 3 stops:
   - 120m: D005 Weft Break
   - 235m: D010 Warp Break
   - 340m: D005 Weft Break
6. Inspector marks these positions on roll for detailed inspection

**Scenario 2: Roll Grading**
1. After inspection, grade roll based on defects
2. Review stop history for defect count and severity
3. Grading rules:
   - Grade A: 0-1 stops, no major defects
   - Grade B: 2-3 stops, minor defects only
   - Grade C: 4+ stops or major defects
4. This roll: 3 stops, all minor → Grade B
5. Update roll record with grade

**Scenario 3: Customer Quality Documentation**
1. Customer requires defect map for each roll
2. System generates report using stop data
3. Document shows:
   - Roll ID: Doff #5
   - Total length: 450m
   - Defects: 3 locations mapped
   - Position 120m: Weft break (repaired)
   - Position 235m: Warp break (repaired)
   - Position 340m: Weft break (repaired)
4. Attached to roll for customer review

**Scenario 4: Reject Analysis**
1. Customer rejects roll due to defects
2. Retrieve stop history to investigate
3. Find 5 stops between 200-250m → Abnormal clustering
4. Root cause: Yarn quality issue in specific pallet
5. Trace back to yarn lot
6. Quality claim to yarn supplier

**Scenario 5: Operator Performance**
1. Evaluate operator efficiency on doff
2. Check stop frequency and response time
3. Calculate:
   - Stops per 100 meters
   - Average stop duration
   - Defect types (recurring issues?)
4. Use for training and performance review

**Comparison with Related Procedures**:
- **WEAV_GETMCSTOPBYLOT**: All stops for entire weaving lot (multiple doffs)
- **WEAV_GETMCSTOPLISTBYDOFFNO**: Stops for ONE specific doff (this procedure)
- More granular = better for per-roll quality control

---

## Related Procedures

**Machine Stop Tracking**:
- [WEAV_GETMCSTOPBYLOT](./WEAV_GETMCSTOPBYLOT.md) - All stops for lot (broader view)
- [WEAV_INSERTMCSTOP](./WEAV_INSERTMCSTOP.md) - Record machine stop
- [WEAV_DELETEMCSTOP](./WEAV_DELETEMCSTOP.md) - Delete stop record

**Doff Management**:
- Doff recording procedures
- Doff inspection procedures
- Roll grading procedures

**Quality Control**:
- Inspection data recording
- Defect marking procedures
- Roll acceptance/rejection workflows

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETMCSTOPLISTBYDOFFNO()`
**Lines**: 1160-1210

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETMCSTOPLISTBYDOFFNO(WEAV_GETMCSTOPLISTBYDOFFNOParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 072/296 | **Progress**: 24.3%
