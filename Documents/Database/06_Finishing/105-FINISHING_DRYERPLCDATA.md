# FINISHING_DRYERPLCDATA

**Procedure Number**: 105 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve PLC-captured dryer process data (hot flue temperature and speed trends) |
| **Operation** | SELECT |
| **Tables** | tblFinishingDryerPLC (inferred) |
| **Called From** | CoatingDataService.cs:2799 → FINISHING_DRYERPLCDATA() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MCNO` | VARCHAR2 | ✅ | Machine number (dryer machine ID) |
| `P_WEAVINGLOT` | VARCHAR2 | ✅ | Weaving lot number to retrieve PLC data for |

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `STARTDATE` | DATE | Data capture timestamp |
| `HOTF_MIN` | NUMBER | Hot flue temperature minimum spec (°C) |
| `HOTF_MAX` | NUMBER | Hot flue temperature maximum spec (°C) |
| `HOTF` | NUMBER | Hot flue temperature actual reading (°C) |
| `SPEED_MIN` | NUMBER | Machine speed minimum spec (m/min) |
| `SPEED_MAX` | NUMBER | Machine speed maximum spec (m/min) |
| `SPEED` | NUMBER | Machine speed actual reading (m/min) |

---

## Business Logic

**Purpose**: Retrieve historical PLC-captured data for dryer process monitoring and trend analysis.

**When Used**: When operator reviews dryer processing history or generates quality reports showing temperature and speed trends over time during the dryer operation.

**Workflow**:
1. Operator selects a weaving lot to review dryer processing history
2. System calls this procedure with machine number and weaving lot
3. Returns time-series data of PLC readings including:
   - Hot flue temperature readings with min/max specification limits
   - Machine speed readings with min/max specification limits
   - Timestamp for each data point
4. UI displays trend charts showing actual values vs. specification limits
5. Quality team can identify process variations or out-of-spec conditions

**Business Rules**:
- PLC captures data at regular intervals (e.g., every minute) during dryer operation
- Each record has both specification limits (MIN/MAX) and actual reading
- Hot flue temperature controls fabric drying rate and final moisture content
- Machine speed affects fabric throughput and drying effectiveness
- Data used for:
  - Process validation (were specs maintained?)
  - Quality investigation (if defects found, was process stable?)
  - Statistical process control (trend analysis)
  - Operator training (showing good vs. bad runs)

**Comparison with DRYERDATABYLOT**:
- `DRYERDATABYLOT` (104): Gets single summary record (overall process data)
- `DRYERPLCDATA` (105): Gets multiple time-series records (trend data during process)

---

## Related Procedures

**Upstream**:
- [104-FINISHING_DRYERDATABYLOT.md](./104-FINISHING_DRYERDATABYLOT.md) - Main dryer process data

**Downstream**:
- [108-FINISHING_GETDRYERREPORT.md](./108-FINISHING_GETDRYERREPORT.md) - Generate dryer reports

**Similar**:
- [089-FINISHING_COATINGPLCDATA.md](./089-FINISHING_COATINGPLCDATA.md) - PLC data for coating process
- [101-FINISHING_SCOURINGPLCDATA.md](./101-FINISHING_SCOURINGPLCDATA.md) - PLC data for scouring process

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_DRYERPLCDATA(string P_MCNO, string P_WEAVINGLOT)`
**Lines**: 2778-2836

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_DRYERPLCDATAParameter`
**Lines**: 8898-8904

**Result Class**: `FINISHING_DRYERPLCDATAResult`
**Lines**: 8908-8919

**Method**: `FINISHING_DRYERPLCDATA(FINISHING_DRYERPLCDATAParameter para)`
**Lines**: 28505-28557

---

**File**: 105/296 | **Progress**: 35.5%
