# WARP_INSERTWARPMCSTOP

**Procedure Number**: 018 | **Module**: M02 - Warping | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Record machine stop incident during warping |
| **Operation** | INSERT |
| **Tables** | tblWarpingMachineStop |
| **Called From** | WarpingDataService.cs:1838 → WARP_INSERTWARPMCSTOP() |
| **Frequency** | Medium (whenever machine stops) |
| **Performance** | Fast |
| **Issues** | 🟡 0 High / 🟡 0 Medium / 🟡 0 Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_WARPHEADNO` | VARCHAR2(50) | ✅ | Warping head number |
| `P_WARPLOT` | VARCHAR2(50) | ⬜ | Warper lot number |
| `P_REASON` | VARCHAR2(100) | ⬜ | Stop reason code/description |
| `P_LENGTH` | NUMBER | ⬜ | Length (meters) when stopped |
| `P_OTHER` | VARCHAR2(200) | ⬜ | Other notes/details |
| `P_OPERATOR` | VARCHAR2(50) | ⬜ | Operator who records stop |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `R_RESULT` | VARCHAR2 | Result code (success/error) |

### Returns (if cursor)

N/A - Returns single string result

---

## Database Operations

### Tables

**Primary Tables**:
- `tblWarpingMachineStop` - INSERT - Stop incident record

**Transaction**: No (standalone insert for logging)

### Indexes (if relevant)

```sql
-- Expected indexes
CREATE INDEX idx_warpingstop_headno_lot ON tblWarpingMachineStop(WARPHEADNO, WARPERLOT);
CREATE INDEX idx_warpingstop_date ON tblWarpingMachineStop(CREATEDATE);
```

---

## Business Logic (What it does and why)

Records machine stop incidents during warping production for downtime tracking and quality analysis. When machine stops (yarn break, mechanical issue, operator intervention), operator records reason and details. Critical for analyzing production efficiency, identifying quality issues, and maintenance needs.

**Workflow**:
1. Machine stops during production
2. Operator identifies stop reason from predefined list
3. Records length/position where stop occurred
4. Adds additional notes if needed (P_OTHER)
5. System calls this procedure to log stop:
   - Creates stop record with timestamp
   - Links to warping head and lot
   - Records reason and position
   - Records who logged the stop
6. Returns success/error code
7. Stop data used for:
   - Downtime reports
   - Quality problem analysis
   - Maintenance scheduling
   - Operator performance tracking

**Business Rules**:
- Warping head number required
- Lot number links stop to specific beam
- Stop reason from predefined codes (yarn break, tension issue, mechanical, etc.)
- Length indicates where in production stop occurred
- Timestamp auto-recorded
- Operator accountability
- Used for efficiency calculations (downtime vs. uptime)

---

## Related Procedures

**Upstream**: [017-WARP_INSERTWARPINGPROCESS.md](./017-WARP_INSERTWARPINGPROCESS.md) - Production lot must exist
**Downstream**: [010-WARP_GETSTOPREASONBYWARPERLOT.md](./010-WARP_GETSTOPREASONBYWARPERLOT.md) - Retrieve stop history
**Similar**: [BEAM_INSERTBEAMMCSTOP.md](../03_Beaming/BEAM_INSERTBEAMMCSTOP.md) - Similar stop recording

---

## Query/Code Location

**File**: `WarpingDataService.cs`
**Method**: `WARP_INSERTWARPMCSTOP()`
**Line**: 1838-1872

**Query Type**: Stored Procedure Call (Oracle)

```csharp
public string WARP_INSERTWARPMCSTOP(
    string P_WARPHEADNO, string P_WARPLOT, string P_REASON,
    decimal? P_LENGTH, string P_OTHER, string P_OPERATOR)
{
    string result = string.Empty;

    // Validation: warping head number required
    if (string.IsNullOrWhiteSpace(P_WARPHEADNO))
        return result;

    if (!HasConnection())
        return result;

    // Prepare parameters
    WARP_INSERTWARPMCSTOPParameter dbPara = new WARP_INSERTWARPMCSTOPParameter();
    dbPara.P_WARPHEADNO = P_WARPHEADNO;
    dbPara.P_WARPLOT = P_WARPLOT;
    dbPara.P_REASON = P_REASON; // Stop reason code
    dbPara.P_LENGTH = P_LENGTH; // Position where stopped
    dbPara.P_OTHER = P_OTHER; // Additional notes
    dbPara.P_OPERATOR = P_OPERATOR; // Who recorded

    WARP_INSERTWARPMCSTOPResult dbResult = null;

    try
    {
        // Call Oracle stored procedure
        dbResult = DatabaseManager.Instance.WARP_INSERTWARPMCSTOP(dbPara);

        // Return result code
        result = dbResult.R_RESULT;
    }
    catch (Exception ex)
    {
        ex.Err();
        result = string.Empty;
    }

    return result;
}
```

---

**File**: 018/296 | **Progress**: 6.1%
