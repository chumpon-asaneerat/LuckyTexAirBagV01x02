# CUT_GETCONDITIONBYITEMCODE

**Procedure Number**: 146 | **Module**: Cut & Print (M11) | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve cutting/printing condition specifications by item code |
| **Operation** | SELECT |
| **Tables** | Item cutting condition master table |
| **Called From** | CutPrintDataService.cs:255 → CUT_GETCONDITIONBYITEMCODE() |
| **Frequency** | Medium |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ⬜ | Item code |

### Output (OUT)

None - returns result set via cursor

### Returns

Returns cutting/printing condition specifications:

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Item code |
| `WIDTHBARCODE_MIN` | NUMBER | Barcode width minimum (mm) |
| `WIDTHBARCODE_MAX` | NUMBER | Barcode width maximum (mm) |
| `DISTANTBARCODE_MIN` | NUMBER | Barcode distance minimum (mm) |
| `DISTANTBARCODE_MAX` | NUMBER | Barcode distance maximum (mm) |
| `DISTANTLINE_MIN` | NUMBER | Cut line distance minimum (mm) |
| `DISTANTLINE_MAX` | NUMBER | Cut line distance maximum (mm) |
| `DENSITYWARP_MIN` | NUMBER | Warp density minimum (ends/inch) |
| `DENSITYWARP_MAX` | NUMBER | Warp density maximum (ends/inch) |
| `DENSITYWEFT_MIN` | NUMBER | Weft density minimum (picks/inch) |
| `DENSITYWEFT_MAX` | NUMBER | Weft density maximum (picks/inch) |
| `SPEED` | NUMBER | Cutting/printing speed |
| `SPEED_MARGIN` | NUMBER | Speed tolerance margin |
| `AFTER_WIDTH` | NUMBER | Width after cutting |
| `SHOWSELVAGE` | VARCHAR2(10) | Show selvage flag |

---

## Business Logic (What it does and why)

**Purpose**: Load cutting and printing machine specifications for specific item code.

**When Used**:
- Operator starts cutting/printing operation
- System loads item-specific conditions
- Machine setup and validation
- Quality control parameters

**Business Rules**:
1. P_ITMCODE optional - may return all or default
2. Returns tolerance ranges for quality control:
   - Barcode dimensions (width, distance)
   - Cut line spacing
   - Density ranges
   - Speed settings
3. Used to configure cutting/printing machine
4. Validates actual measurements against specifications

**Cutting/Printing Specifications**:

**1. Barcode Specifications**:
- WIDTHBARCODE: Barcode width tolerance
- DISTANTBARCODE: Distance between barcodes
- Ensures barcode readability and customer requirements

**2. Cut Line Specifications**:
- DISTANTLINE: Distance between cut lines
- Determines final product length
- Customer-specific cutting requirements

**3. Density Specifications**:
- DENSITYWARP/WEFT: Thread density ranges
- Quality verification during cutting
- Ensures fabric meets specifications

**4. Machine Settings**:
- SPEED: Recommended cutting/printing speed
- SPEED_MARGIN: Allowable speed variation
- AFTER_WIDTH: Final product width after cutting
- SHOWSELVAGE: Whether to show fabric selvage edge

**Usage in Process**:
1. Operator scans item code or finishing lot
2. System calls CUT_GETCONDITIONBYITEMCODE
3. Loads all specifications for item
4. Displays tolerance ranges to operator
5. Machine configured with speed/width settings
6. During operation: validates measurements against ranges
7. Alerts if out of tolerance

**Quality Control**:
- Min/Max ranges for all parameters
- Real-time validation during cutting
- Prevents out-of-spec production
- Customer-specific requirements

---

## Related Procedures

**Related**: CUT_GETFINISHINGDATA - Get finishing lot data for cutting
**Related**: CUT_INSERTDATA - Record cutting operation
**Used in**: Cut & Print operation setup and validation

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CutPrintDataService.cs`
**Method**: `CUT_GETCONDITIONBYITEMCODE(string P_ITMCODE)`
**Lines**: 255-355
**Comment**: "เพิ่มใหม่ CUT_GETCONDITIONBYITEMCODE ใช้ในการ Load CONDITIONBYITEMCODE"
*Translation*: "Added new CUT_GETCONDITIONBYITEMCODE used for loading condition by item code"

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `CUT_GETCONDITIONBYITEMCODE(CUT_GETCONDITIONBYITEMCODEParameter para)`

**Parameter Class**: 1 parameter (P_ITMCODE)
**Result Class**: 15 columns (comprehensive cutting conditions)

**Features**: C# code formats min/max values into range strings (e.g., "50.0 - 55.0")

---

**File**: 146/296 | **Progress**: 49.3%
