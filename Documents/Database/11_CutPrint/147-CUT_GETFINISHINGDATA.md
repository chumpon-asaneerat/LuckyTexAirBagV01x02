# CUT_GETFINISHINGDATA

**Procedure Number**: 147 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve finishing lot data to start cutting/printing operation |
| **Operation** | SELECT |
| **Tables** | tblFinishing (finishing lot master) |
| **Called From** | CutPrintDataService.cs:195 → GetCUT_GETFINISHINGDATAList() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITEMLOT` | VARCHAR2(50) | ⬜ | Item lot number (finishing lot or inspection lot) |

### Output (OUT)

None - returns result set via cursor

### Returns

| Column | Type | Description |
|--------|------|-------------|
| `ITEMCODE` | VARCHAR2(50) | Item/product code |
| `ITEMLOT` | VARCHAR2(50) | Item lot number |
| `BATCHNO` | VARCHAR2(50) | Batch number |
| `FINISHINGLOT` | VARCHAR2(50) | Finishing lot number |
| `PRODUCTTYPEID` | VARCHAR2(50) | Product type |
| `FINISHINGPROCESS` | VARCHAR2(50) | Finishing process type (added 2016-01-20) |
| `SND_BARCODE` | VARCHAR2(50) | Second barcode (added 2016-06-27) |
| `CUSTOMERID` | VARCHAR2(50) | Customer ID |
| `BEFORE_WIDTH` | NUMBER | Width before cutting (added 2017-10-04) |

---

## Business Logic (What it does and why)

**Purpose**: Load finishing lot information when operator starts cutting/printing operation.

**When Used**:
- Operator scans finishing lot barcode
- System loads lot details for cutting setup
- Retrieves item specifications and customer information
- Prepares data for cutting operation start

**Business Rules**:
1. P_ITEMLOT can be finishing lot or item lot number
2. Returns lot information with customer and product details
3. Includes finishing process type (Scouring, Coating, etc.)
4. BEFORE_WIDTH used for cutting width validation
5. Links finishing operation to cutting operation

**Workflow**:
1. Operator at cutting machine
2. Scans finishing lot barcode
3. System calls CUT_GETFINISHINGDATA
4. Retrieves:
   - Item code and product type
   - Batch and finishing lot numbers
   - Customer information
   - Finishing process (affects cutting parameters)
   - Width before cutting (for validation)
5. Displays lot information
6. Operator confirms and starts cutting operation
7. System loads cutting conditions using item code

**Enhancement History**:
- **2016-01-20**: Added FINISHINGPROCESS field
- **2016-06-27**: Added SND_BARCODE (second barcode support)
- **2017-10-04**: Added BEFORE_WIDTH field

---

## Related Procedures

**Related**: [146-CUT_GETCONDITIONBYITEMCODE.md](./146-CUT_GETCONDITIONBYITEMCODE.md) - Get cutting specifications
**Related**: CUT_INSERTDATA - Record cutting operation
**Upstream**: Finishing module provides finishing lots
**Downstream**: Cutting operation starts with this data

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `GetCUT_GETFINISHINGDATAList(string ITEMLOT)`
**Lines**: 195-244
**Comment**: "เพิ่มใหม่ CUT_GETFINISHINGDATA ใช้ในการ Load Cut FINISHING"
*Translation*: "Added new CUT_GETFINISHINGDATA used for loading Cut FINISHING"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_GETFINISHINGDATA(CUT_GETFINISHINGDATAParameter para)`

**Parameter Class**: 1 parameter (P_ITEMLOT)
**Result Class**: 9 columns

---

**File**: 147/296 | **Progress**: 49.7%
