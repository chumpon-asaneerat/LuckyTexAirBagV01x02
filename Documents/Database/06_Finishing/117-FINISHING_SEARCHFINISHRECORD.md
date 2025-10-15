# FINISHING_SEARCHFINISHRECORD

**Procedure Number**: 117 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search and retrieve finished production records by date, machine, and/or item code |
| **Operation** | SELECT |
| **Tables** | tblFinishingCoating, tblFinishingScouring, tblFinishingDryer (inferred - UNION query) |
| **Called From** | FinishingDataService.cs:294 → FINISHING_SEARCHFINISHRECORD(date, mcno)<br>FinishingDataService.cs:463 → FINISHING_SEARCHFINISHRECORD(date, mcno, itmcode) |
| **Frequency** | High |
| **Performance** | Medium (date range queries) |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DATE` | VARCHAR2 | ⬜ | Production date to search (format: YYYYMMDD or date string) |
| `P_MCNO` | VARCHAR2 | ⬜ | Machine number to filter |
| `P_ITMCODE` | VARCHAR2 | ⬜ | Item code to filter |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number (source fabric) |
| `FINISHINGLOT` | VARCHAR2 | Finishing lot number |
| `ITM_CODE` | VARCHAR2 | Item code |
| `STARTDATE` | DATE | Process start date/time |
| `ENDDATE` | DATE | Process end date/time |
| `FINISHBY` | VARCHAR2 | Operator who completed process |
| `STARTBY` | VARCHAR2 | Operator who started process |
| `CONDITIONBY` | VARCHAR2 | Operator who set conditions |
| `MCNO` | VARCHAR2 | Machine number |
| `MC` | VARCHAR2 | Machine name |
| `WEAVLENGTH` | NUMBER | Weaving length (input length) |
| `WIDTH_BE` | NUMBER | Width before processing (cm) |
| `WIDTH_AF` | NUMBER | Width after processing (cm) |
| `OPERATOR_GROUP` | VARCHAR2 | Operator group/shift |
| `LENGTH1` | NUMBER | Finished length 1 (meters) |
| `LENGTH2` | NUMBER | Finished length 2 (meters) |
| `LENGTH3` | NUMBER | Finished length 3 (meters) |
| `LENGTH4` | NUMBER | Finished length 4 (meters) |
| `LENGTH5` | NUMBER | Finished length 5 (meters) |
| `LENGTH6` | NUMBER | Finished length 6 (meters) |
| `LENGTH7` | NUMBER | Finished length 7 (meters) |
| `PRODUCTIONTYPE` | VARCHAR2 | Production type (NORMAL/REPROCESS/TRIAL) |

---

## Business Logic

**Purpose**: Search for completed finishing operations across all three finishing processes (coating, scouring, dryer). This is the primary search interface for production history, reporting, and analysis.

**When Used**:

**Scenario 1: Daily Production Report**
1. Production supervisor generates end-of-day report
2. Calls FINISHING_SEARCHFINISHRECORD:
   - `P_DATE` = "20250115" (today)
   - `P_MCNO` = null (all machines)
   - `P_ITMCODE` = null (all items)
3. Returns all finishing operations completed today
4. Report shows:
   - Total lots processed: 45 lots
   - By machine: COAT-01 (8), DRYER-01 (15), SCOUR-02 (12), etc.
   - By item: AB-45-PA66 (20), AB-50-PA66 (15), etc.
   - Total lengths produced
   - Operator efficiency

**Scenario 2: Machine Performance Analysis**
1. Maintenance manager analyzes DRYER-01 performance
2. Calls FINISHING_SEARCHFINISHRECORD:
   - `P_DATE` = "202501" (January 2025 - month range)
   - `P_MCNO` = "DRYER-01"
   - `P_ITMCODE` = null
3. Returns all DRYER-01 operations in January
4. Analysis includes:
   - Total lots processed: 450
   - Average cycle time: (ENDDATE - STARTDATE)
   - Utilization rate
   - Width change analysis (shrinkage/expansion)
   - Length yield (output/input)

**Scenario 3: Item Production History**
1. Customer requests production traceability for item AB-45-PA66
2. Calls FINISHING_SEARCHFINISHRECORD:
   - `P_DATE` = "2025" (full year)
   - `P_MCNO` = null
   - `P_ITMCODE` = "AB-45-PA66"
3. Returns all AB-45-PA66 finishing operations
4. Provides:
   - Complete production history
   - Which machines used
   - Operators involved
   - Length variations
   - Width consistency
   - Traceability: Finishing lot → Weaving lot → upstream

**Scenario 4: Operator Performance Review**
1. HR manager reviews operator OPR-101 performance
2. Calls FINISHING_SEARCHFINISHRECORD:
   - `P_DATE` = "202412" (December 2024)
   - `P_MCNO` = null
3. Filter results by FINISHBY = "OPR-101" in application
4. Review:
   - Lots completed: 120
   - Quality consistency (width/length accuracy)
   - Shift patterns (from OPERATOR_GROUP)
   - Training needs

**Scenario 5: Customer Complaint Investigation**
1. Customer reports quality issue with shipment
2. QC traces back to finishing lots: FN-2025-100 to FN-2025-105
3. Calls FINISHING_SEARCHFINISHRECORD with date range covering those lots
4. Investigates:
   - Which machines processed the lots
   - Process parameters (from linked detail records)
   - Operators involved
   - Width/length variations
   - Identifies common factors (same machine, same shift, etc.)

**Business Rules**:
- **Flexible filtering**: All parameters optional for broad searches
- **Date flexibility**: Can search by specific date, date range, or month
- **Multiple processes**: UNION query returns coating + scouring + dryer results
- **Completed only**: Only returns finished operations (STATUS='FINISH')
- **Comprehensive data**: Includes all key metrics (lengths, widths, operators)
- **Traceability link**: WEAVINGLOT provides upstream traceability
- **Multiple lengths**: Up to 7 length measurements per lot (segmented rolls)

**Search Flexibility**:
- `P_DATE` only: All lots on specific date(s)
- `P_MCNO` only: All lots for specific machine
- `P_ITMCODE` only: All lots for specific item
- `P_DATE + P_MCNO`: Specific machine on specific date
- `P_DATE + P_ITMCODE`: Specific item on specific date
- `P_MCNO + P_ITMCODE`: Specific item on specific machine
- All three: Most specific search
- No parameters: All finishing records (use with caution - large dataset)

**Performance Considerations**:
- Date indexing critical (P_DATE most common filter)
- Machine index for machine-specific queries
- Item code index for product analysis
- Consider date range limits for large datasets

---

## Related Procedures

**Upstream**:
- [094-FINISHING_UPDATECOATING.md](./094-FINISHING_UPDATECOATING.md) - Completes coating
- [102-FINISHING_UPDATESCOURING.md](./102-FINISHING_UPDATESCOURING.md) - Completes scouring
- [110-FINISHING_UPDATEDRYER.md](./110-FINISHING_UPDATEDRYER.md) - Completes dryer

**Similar**:
- [118-FINISHING_SEARCHFINISHDATA.md](./118-FINISHING_SEARCHFINISHDATA.md) - Similar search (different columns)
- WEAV_SEARCHFINISHWEAVLOT (M05) - Search weaving records
- INS_SEARCHINSPECTIONRECORD (M08) - Search inspection records

**Downstream**:
- Production reports (daily, weekly, monthly)
- Machine performance dashboards
- Quality analysis tools
- Customer traceability reports

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\FinishingDataService.cs`

**Method 1** (2 parameters): `FINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO)`
**Lines**: 294-458

**Method 2** (3 parameters): `FINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO, string P_ITMCODE)`
**Lines**: 463-626

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_SEARCHFINISHRECORDParameter`
**Lines**: 7587-7592

**Result Class**: `FINISHING_SEARCHFINISHRECORDResult`
**Lines**: 7598-7622

**Method**: `FINISHING_SEARCHFINISHRECORD(FINISHING_SEARCHFINISHRECORDParameter para)`
**Lines**: 26482-26600 (estimated)

---

**File**: 117/296 | **Progress**: 39.5%
