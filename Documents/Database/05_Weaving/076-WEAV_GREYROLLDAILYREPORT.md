# WEAV_GREYROLLDAILYREPORT

**Procedure Number**: 076 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Generates daily production report for greige (grey) fabric by date and China lot |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:459 → WEAV_GREYROLLDAILYREPORT() |
| **Frequency** | High (Daily reporting) |
| **Performance** | Medium |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_DATE` | VARCHAR2 | ✅ | Production date for report (typically formatted as string) |
| `P_CHINA` | VARCHAR2 | ✅ | China lot filter (may use wildcard '%' for all) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number |
| `LENGTH` | NUMBER | Fabric length produced (meters) |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `WEAVINGDATE` | DATE | Weaving production date |
| `SHIFT` | VARCHAR2 | Production shift (A/B/C) |
| `REMARK` | VARCHAR2 | Production remarks |
| `CREATEDATE` | DATE | Record creation timestamp |
| `WIDTH` | NUMBER | Fabric width (cm) |
| `PREPAREBY` | VARCHAR2 | Operator who prepared the setup |
| `WEAVINGNO` | VARCHAR2 | Sequential weaving number |

---

## Business Logic

Generates a daily production report for greige (unfinished) fabric production, filtered by production date and optionally by China lot number. This report is used for:

1. **Daily Production Tracking**: Monitor total fabric production output per day
2. **Shift Performance Analysis**: Compare production across different shifts
3. **Customer-Specific Reporting**: Filter by China lot to report production for specific customers/orders
4. **Production Planning**: Review daily capacity utilization across all looms
5. **Management Reporting**: Provide summary data for production KPIs

**Business Rules**:
- P_CHINA can accept '%' wildcard to include all China lots
- Returns all weaving lots completed on the specified date
- Groups data by loom, shift, and item code for analysis
- Includes operator information (PREPAREBY) for accountability
- WEAVINGDATE is the primary filter criterion

**Typical Usage Scenario**: Production supervisor runs this report at end of day to:
- Calculate total meters produced
- Identify which looms were most productive
- Report completion status to planning department
- Prepare shift handover documentation

**Report Purpose**: "Greige Roll Daily Report" - Shows all grey (unfinished) fabric rolls produced in a single day before they go to finishing operations (coating, scouring, drying).

---

## Related Procedures

**Similar**:
- [074-WEAV_GETWEAVELISTBYBEAMROLL.md](./074-WEAV_GETWEAVELISTBYBEAMROLL.md) - Get weaving history by beam roll
- [069-WEAV_GETITEMWEAVINGLIST.md](./069-WEAV_GETITEMWEAVINGLIST.md) - Get weaving production list by item

**Downstream**:
- Finishing module procedures - Greige fabric moves to finishing after this stage
- Production reporting dashboards - Aggregate daily production data

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GREYROLLDAILYREPORT()`
**Lines**: 459-505

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GREYROLLDAILYREPORT(WEAV_GREYROLLDAILYREPORTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 076/296 | **Progress**: 25.7%
