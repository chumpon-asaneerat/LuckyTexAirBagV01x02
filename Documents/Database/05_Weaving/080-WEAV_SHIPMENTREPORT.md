# WEAV_SHIPMENTREPORT

**Procedure Number**: 080 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Generates shipment report showing total pieces and meters by item code for date range |
| **Operation** | SELECT (Aggregation) |
| **Called From** | WeavingDataService.cs:1282 → WEAV_SHIPMENTREPORT() |
| **Frequency** | Medium (Reporting) |
| **Performance** | Medium |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEGINDATE` | VARCHAR2 | ✅ | Report start date (formatted as string) |
| `P_ENDDATE` | VARCHAR2 | ✅ | Report end date (formatted as string) |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `PIECES` | NUMBER | Total number of pieces/rolls produced |
| `METERS` | NUMBER | Total meters of fabric produced |

**Note**: C# code adds row numbering (`No` field) starting from 1 for display purposes

---

## Business Logic

Generates a summarized shipment report for weaving production, aggregating data by item code over a specified date range. This report is used for:

1. **Shipment Planning**: Calculate total production available for shipment
2. **Customer Orders**: Verify if sufficient quantity exists to fulfill orders
3. **Production Summary**: Show total output per item code
4. **Inventory Management**: Track fabric quantities ready for finishing/packing
5. **Management Reporting**: Provide high-level production metrics

**Business Rules**:
- Aggregates data by item code (ITM_WEAVING)
- Returns summary counts: total pieces and total meters per item
- Date range filter allows flexible reporting periods (daily, weekly, monthly)
- Results are numbered sequentially (1, 2, 3...) in C# for display
- Likely filters completed/approved production only (not work-in-process)

**Report Output**:
- Each row = one item code with totals
- PIECES = count of individual fabric rolls/doffs
- METERS = sum of all fabric lengths for that item

**Typical Usage Scenarios**:
1. **Daily Shipment Report**: P_BEGINDATE = today, P_ENDDATE = today
2. **Weekly Production**: P_BEGINDATE = Monday, P_ENDDATE = Friday
3. **Monthly Summary**: P_BEGINDATE = 1st of month, P_ENDDATE = last day of month
4. **Custom Range**: Any date range for ad-hoc analysis

**Example Report Output**:
```
No  ITM_WEAVING    PIECES    METERS
1   AB-100         15        4,500
2   AB-200         8         2,400
3   CD-150         22        6,600
```

---

## Related Procedures

**Related**:
- [076-WEAV_GREYROLLDAILYREPORT.md](./076-WEAV_GREYROLLDAILYREPORT.md) - Daily detailed production report (more granular)
- [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Main weaving process that generates data

**Downstream**:
- Packing module - Use shipment data to plan packing operations
- Inventory module - Update stock quantities based on shipment
- D365 Integration - Export shipment data to ERP system

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_SHIPMENTREPORT()`
**Lines**: 1282-1322

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_SHIPMENTREPORT(WEAV_SHIPMENTREPORTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 080/296 | **Progress**: 27.0%
