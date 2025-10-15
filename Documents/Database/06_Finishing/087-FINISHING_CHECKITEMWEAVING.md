# FINISHING_CHECKITEMWEAVING

**Procedure Number**: 087 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Validate weaving item compatibility with finishing item code |
| **Operation** | SELECT |
| **Tables** | tblItemCode (master item data) |
| **Called From** | CoatingDataService.cs:79 → FINISHING_CHECKITEMWEAVINGList() |
| **Frequency** | High |
| **Performance** | Fast |
| **Issues** | 🟢 None |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_ITMCODE` | VARCHAR2(50) | ✅ | Finished goods item code |
| `P_ITMWEAVING` | VARCHAR2(50) | ✅ | Weaving (greige) item code from fabric roll |

### Output (OUT)

None - Returns cursor

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ITM_CODE` | VARCHAR2(50) | Finished goods item code |
| `ITM_WEAVING` | VARCHAR2(50) | Weaving item code |
| `ITM_YARN` | VARCHAR2(50) | Yarn specification code |
| `ITM_WIDTH` | NUMBER | Fabric width (mm) |
| `ITM_PROC1` | VARCHAR2(50) | Process 1 type (e.g., Coating) |
| `ITM_PROC2` | VARCHAR2(50) | Process 2 type (e.g., Scouring) |
| `ITM_PROC3` | VARCHAR2(50) | Process 3 type (e.g., Dryer) |
| `ITM_PROC4` | VARCHAR2(50) | Process 4 type (optional) |
| `ITM_PROC5` | VARCHAR2(50) | Process 5 type (optional) |
| `ITM_PROC6` | VARCHAR2(50) | Process 6 type (optional) |
| `CREATEDATE` | DATE | Record creation timestamp |
| `CREATEBY` | VARCHAR2(50) | Created by user |
| `EDITDATE` | DATE | Last edit timestamp |
| `EDITBY` | VARCHAR2(50) | Last edited by user |
| `ITM_PREPARE` | VARCHAR2(50) | Item preparation code |
| `COREWEIGHT` | NUMBER | Core weight (kg) |
| `FULLWEIGHT` | NUMBER | Full roll weight (kg) |
| `ITM_GROUP` | VARCHAR2(50) | Item group classification |
| `YARNCODE` | VARCHAR2(50) | Yarn specification code |
| `WIDTHCODE` | VARCHAR2(50) | Width specification code |
| `WIDTHWEAVING` | NUMBER | Weaving width (mm) |
| `LABFORM` | VARCHAR2(50) | Laboratory test form reference |
| `WEAVE_TYPE` | VARCHAR2(50) | Weave pattern type (plain, twill, etc.) |

---

## Business Logic (What it does and why)

**Purpose**: Validates that an incoming weaving fabric roll matches the selected finishing item specification before starting finishing operations.

**When Used**:
- At the beginning of any finishing process (Coating, Scouring, Dryer)
- Operator scans/enters both the target finished goods item code and the weaving lot barcode
- System must verify they are compatible before allowing production to start

**Business Rules**:
1. **Compatibility Check**: Finished goods items can only be produced from specific weaving item codes
2. **Process Configuration**: Returns the required finishing process sequence (PROC1-PROC6) for this item combination
3. **Specification Validation**: Ensures width, yarn type, and other specifications match between weaving and finishing items
4. **Traceability**: Links finished goods back to greige fabric specification

**Workflow**:
1. Operator selects/scans target finished goods item code (P_ITMCODE)
2. Operator scans weaving fabric roll barcode containing ITM_WEAVING code
3. System calls this procedure to check if combination is valid
4. If record found → Valid combination, returns process configuration
5. If no record → Shows error: "This Item Weaving does not map with selected item Good"
6. Prevents production of wrong item or using incompatible fabric

**Example Scenario**:
- Finished Item: "F-AIR-001" (coated airbag fabric)
- Weaving Item: "W-GREY-001" (greige fabric roll)
- Procedure checks: Can F-AIR-001 be made from W-GREY-001?
- Returns: YES + process sequence (Coating → Scouring → Dryer) + specifications

---

## Related Procedures

**Upstream**:
- WEAV_WEAVINGINPROCESSLIST (provides weaving lot data)
- ITM_GETITEMCODELIST (item master data)

**Downstream**:
- FINISHING_INSERTCOATING (uses validated item data to start coating)
- FINISHING_INSERTSCOURING (uses validated item data to start scouring)
- FINISHING_INSERTDRYER (uses validated item data to start drying)

**Similar**:
- WEAVE_CHECKITEMPREPARE (validates beam vs weaving item compatibility)
- BEAM_GETSPECBYCHOPNO (validates beam specification)

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\CoatingDataService.cs`
**Method**: `FINISHING_CHECKITEMWEAVINGList(string itm_code, string itm_weaving)`
**Lines**: 79-129

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `FINISHING_CHECKITEMWEAVING(FINISHING_CHECKITEMWEAVINGParameter para)`
**Lines**: 28449-28513

---

**File**: 87/296 | **Progress**: 29.4%
