# WARP_INSERTWARPINGPROCESS

**Procedure Number**: 017 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Start new warping production lot (beam) |
| **Operation** | INSERT |
| **Tables** | tblWarpingProcess |
| **Called From** | WarpingDataService.cs:1660 → WARP_INSERTWARPINGPROCESS() |
| **Frequency** | Medium (starting new beam production) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number (setup ID) |
| `P_WARPMC` | VARCHAR2(50) | ⬜ | Warping machine number |
| `P_BEAMNO` | VARCHAR2(50) | ⬜ | Beam number (barcode) |
| `P_SIDE` | VARCHAR2(10) | ⬜ | Side (A or B) |
| `P_STARTDATE` | DATE | ⬜ | Production start date/time |
| `P_STARTBY` | VARCHAR2(50) | ⬜ | Operator who starts production |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_WRAPLOT` | VARCHAR2(50) | Generated warper lot number |
| `RESULT` | VARCHAR2(50) | Result code (success/error) |

### Returns (if cursor)

N/A - Returns object with lot number and result code

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingProcess` - INSERT - New production lot record

**Transaction**: Yes (atomic operation for lot creation)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE UNIQUE INDEX uk_warpingprocess_lot ON tblWarpingProcess(WARPERLOT);
CREATE INDEX idx_warpingprocess_headno ON tblWarpingProcess(WARPHEADNO);
CREATE INDEX idx_warpingprocess_beamno ON tblWarpingProcess(BEAMNO);
```

---

## Business Logic (What it does and why)

Initiates new warping production run by creating production lot record. When operator starts producing a beam from creel setup, this creates the lot/beam record that will track the entire production process. Generates unique warper lot number and links to setup header. Critical for production tracking and traceability.

**Workflow**:
1. Creel setup complete (header + pallets allocated)
2. Operator ready to start beam production
3. Scans or enters beam number
4. Presses "Start Production" button
5. System calls this procedure to create lot:
   - Generates unique warper lot number
   - Creates production record linked to setup (WARPHEADNO)
   - Records beam number (barcode for finished beam)
   - Records machine, side, start time, operator
   - Sets initial status (IN_PROCESS)
6. Returns generated lot number to UI
7. Lot number used for all production updates (hardness, speed, length, etc.)
8. Production metrics recorded against this lot number

**Business Rules**:
- Warping head number required (which setup)
- Generates unique warper lot number
- One lot = one beam being produced
- Multiple lots can be produced from same setup
- Start timestamp recorded for production tracking
- Operator accountability (who started)
- Links to beam barcode for downstream traceability

---

## Related Procedures

**Upstream**: [016-WARP_INSERTSETTINGHEAD.md](./016-WARP_INSERTSETTINGHEAD.md) - Setup must exist first
**Downstream**: [025-WARP_UPDATEWARPINGPROCESS.md](./025-WARP_UPDATEWARPINGPROCESS.md) - Update production metrics
**Similar**: [WEAVE_WEAVINGPROCESS.md](../05_Weaving/WEAVE_WEAVINGPROCESS.md) - Similar production start

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_INSERTWARPINGPROCESS()`
**Line**: 1660-1700

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public WARP_INSERTWARPINGPROCESS WARP_INSERTWARPINGPROCESS(
    string P_WARPHEADNO, string P_WARPMC, string P_BEAMNO,
    string P_SIDE, DateTime? P_STARTDATE, string P_STARTBY)
{
    WARP_INSERTWARPINGPROCESS results = null;

    // Validation: warping head number required
    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return results;

    if (!HasConnection())
        return results;

    // Prepare parameters
    WARP_INSERTWARPINGPROCESSParameter dbPara = new WARP_INSERTWARPINGPROCESSParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPMC = P_WARPMC;
    dbPara.P_BEAMNO = P_BEAMNO;
    dbPara.P_SIDE = P_SIDE;
    dbPara.P_STARTDATE = P_STARTDATE;
    dbPara.P_STARTBY = P_STARTBY;

    WARP_INSERTWARPINGPROCESSResult dbResults = null;

    try
    {
        // Call Oracle stored procedure
        dbResults = DatabaseManager.Instance.WARP_INSERTWARPINGPROCESS(dbPara);

        results = new WARP_INSERTWARPINGPROCESS();

        if (null != dbResults)
        {
            // Return generated lot number and result code
            results.R_WRAPLOT = dbResults.R_WRAPLOT; // Generated lot number
            results.RESULT = dbResults.RESULT; // Success/error code
        }
    }
    catch (Exception ex) { ex.Err(); }

    return results;
}
```

---

**File**: 017/296 | **Progress**: 5.7%
