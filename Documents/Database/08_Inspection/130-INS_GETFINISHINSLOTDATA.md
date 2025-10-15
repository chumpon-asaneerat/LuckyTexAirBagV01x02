# INS_GETFINISHINSLOTDATA

**Procedure Number**: 130 | **Module**: Inspection (M08) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve complete inspection lot data by finishing lot number for weight measurement |
| **Operation** | SELECT |
| **Tables** | tblINSLot (inspection lot master) |
| **Called From** | DataServicecs.cs:2504 → GetINS_GetFinishinslotDataList() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_FINLOT` | VARCHAR2(50) | ✅ | Finishing lot number (from coating/finishing process) |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns comprehensive inspection lot record(s) with 42 columns (extensive data):

**Core Identification:**
- `INSPECTIONLOT` - Inspection lot number (primary key)
- `FINISHINGLOT` - Finishing lot number
- `ITEMCODE` - Product item code
- `DEFECTID` - Defect record ID reference

**Date/Time:**
- `STARTDATE` - Inspection start date/time
- `ENDDATE` - Inspection end date/time
- `SUSPENDDATE` - Date lot was suspended (if suspended)
- `STARTDATE1` - Alternative start date
- `SHIFT_REMARK_DATE` - Shift remark entry date

**Length/Weight Measurements:**
- `GROSSLENGTH` - Total fabric length before inspection
- `NETLENGTH` - Final net length after defects/cuts
- `GROSSWEIGHT` - Gross weight (kg)
- `NETWEIGHT` - Net weight (kg)
- `CONFIRMSTARTLENGTH` - Confirmed starting length

**Quality Information:**
- `GRADE` - Final quality grade (A, B, C, etc.)
- `DF_CODE` - Defect code reference
- `DF_AMOUNT` - Defect amount
- `DF_POINT` - Total defect points

**Customer/Product:**
- `CUSTOMERID` - Customer identifier
- `CUSTOMERTYPE` - Customer type classification
- `PRODUCTTYPEID` - Product type
- `LOADINGTYPE` - Loading/packaging type

**Status/Flags:**
- `FINISHFLAG` - Inspection completion flag
- `ISPACKED` - Packing status flag
- `RETEST` - Retest indicator
- `RETYPE` - Retype classification

**Operators/Process:**
- `INSPECTEDBY` - Inspector operator ID
- `OPERATOR_GROUP` - Operator group
- `SHIFT_ID` - Production shift
- `SHIFT_REMARK` - Shift remark/notes

**Machine/Location:**
- `MCNO` - Inspection machine number
- `INSPECTIONID` - Inspection process identifier

**References:**
- `PEINSPECTIONLOT` - Previous inspection lot (if re-inspection)
- `PREITEMCODE` - Previous item code (if changed)
- `ATTACHID` - Attachment file ID
- `TESTRECORDID` - 100M test record ID reference
- `DEFECTFILENAME` - Defect image filename

**Suspension/Clearance:**
- `SUSPENDBY` - Operator who suspended lot
- `CLEARBY` - Operator who cleared suspension
- `CLEARREMARK` - Clearance remark/reason

**General:**
- `REMARK` - General remarks

---

## Business Logic (What it does and why)

**Purpose**: Retrieve inspection lot data by finishing lot number for the **Weight Measurement page**.

**When Used**: When operator scans or enters a finishing lot number on the Weight Measurement page to:
1. Validate the lot exists
2. Display current lot information (item code, grade, customer, etc.)
3. Show existing weight measurements (gross/net weight)
4. Load shift and operator information
5. Prepare for weight entry or verification

**Business Rules**:
1. Requires valid P_FINLOT (finishing lot number from coating/finishing process)
2. Returns complete inspection record with all fields
3. Used specifically for weight measurement workflow
4. Finishing lot number links back to inspection lot
5. May return multiple records if finishing lot has multiple inspection entries

**Weight Measurement Workflow**:
1. Operator scans/enters finishing lot barcode on WeightMeasurementPage
2. System calls INS_GETFINISHINSLOTDATA to retrieve lot data
3. Page displays:
   - Item code and product information
   - Current grade
   - Existing gross/net weights (if already measured)
   - Customer and shift information
4. Operator verifies information
5. Enters or confirms weight measurements
6. System updates the record

**Data Usage in WeightMeasurementPage**:
- Displays ITEMCODE, FINISHINGLOT, GRADE, CUSTOMERTYPE
- Shows current GROSSWEIGHT and NETWEIGHT values
- Displays SHIFT_ID and SHIFT_REMARK
- Uses INSPECTIONLOT as key for weight update operations

**Why Query by Finishing Lot**:
- Weight measurement happens after finishing process
- Operators work with finishing lot numbers (printed on fabric)
- Finishing lot is more accessible than inspection lot number
- Links finishing operation to inspection records

---

## Related Procedures

**Similar**: INS_GETINSPECTIONREPORTDATA - Get inspection data for reporting (not documented yet)
**Related**: UPDATEINSPECTIONPROCESS - Update inspection lot with weight data (shared, not documented)
**Related**: FINISHING_* procedures - Source of finishing lot numbers (M06 module)
**Upstream**: Called after finishing process completes
**Downstream**: Weight data updated back to inspection lot record

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `GetINS_GetFinishinslotDataList(string insFINLOT)`
**Lines**: 2504-2557
**Comment**: "เพิ่มใหม่ INS_GetFinishinslotData" (Translation: "Added new INS_GetFinishinslotData")

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `INS_GETFINISHINSLOTDATA(INS_GETFINISHINSLOTDATAParameter para)`
**Lines**: 22940-23020

**Parameter Class**: `INS_GETFINISHINSLOTDATAParameter`
**Lines**: 5657-5660 (1 parameter)

**Result Class**: `INS_GETFINISHINSLOTDATAResult`
**Lines**: 5666-5709 (42 columns - very comprehensive)

**Usage Location**: `LuckyTex.AirBag.Pages\Pages\08 - Inspection\WeightMeasurementPage.xaml.cs`
**Line**: 326
**Method**: `GetINS_GetFinishinsLotData(string itemLotNo)`
**Context**: Loading finishing lot data for weight measurement entry

---

**File**: 130/296 | **Progress**: 43.9%
