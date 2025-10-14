# WEAV_WEAVINGMCSTATUS

**Procedure Number**: 084 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieves current weaving machine status and setup configuration |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:748 → WEAV_WEAVINGMCSTATUS() |
| **Frequency** | High (Real-time monitoring) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_MC` | VARCHAR2 | ✅ | Machine/loom number to query status |

### Returns (Single Record)

**Note**: Returns single object (not list) - uses first record from cursor

| Column | Type | Description |
|--------|------|-------------|
| `BEAMLOT` | VARCHAR2 | Current beam lot on machine |
| `MC` | VARCHAR2 | Machine/loom number |
| `REEDNO2` | VARCHAR2 | Reed number (2nd reed) |
| `WEFTYARN` | VARCHAR2 | Weft yarn specification |
| `TEMPLETYPE` | VARCHAR2 | Temple type installed |
| `BARNO` | VARCHAR2 | Bar number |
| `STARTDATE` | DATE | Production start date/time |
| `FINISHDATE` | DATE | Production finish date/time |
| `FINISHFLAG` | VARCHAR2 | Completion status flag |
| `SETTINGBY` | VARCHAR2 | Operator who set up machine |
| `EDITDATE` | DATE | Last edit timestamp |
| `EDITBY` | VARCHAR2 | Last editor |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type identifier |
| `WIDTH` | NUMBER | Fabric width (cm) |
| `BEAMLENGTH` | NUMBER | Beam length (meters) |
| `REEDNO` | VARCHAR2 | Primary reed number |
| `HEALDCOLOR` | VARCHAR2 | Heald frame color coding |
| `SPEED` | NUMBER | Loom speed setting (RPM) |

---

## Business Logic

Retrieves the current status and complete setup configuration for a specific weaving machine/loom. Critical for:

1. **Real-Time Monitoring**: Display current machine status on production dashboard
2. **Operator Guidance**: Show what setup is currently configured on the loom
3. **Production Control**: Verify correct machine setup before starting production
4. **Status Board**: Update electronic status boards showing what each loom is producing
5. **Quick Reference**: Operators can check machine configuration without manual inspection

**Business Rules**:
- Returns **single record only** (C# uses `dbResults[0]`)
- If no data or error, returns empty WEAV_WEAVINGMCSTATUS object
- Shows most recent or active setup for specified machine
- FINISHFLAG indicates if production is complete or in-progress
- Includes mechanical configuration (reed, temple, heald) and process parameters (speed, width)

**Machine Status Interpretation**:
- **FINISHFLAG = empty/0**: Machine is currently in production
- **FINISHFLAG = 1**: Last job finished, machine available/idle
- **STARTDATE/FINISHDATE**: Calculate production duration or downtime

**Return Behavior**:
```csharp
// Returns single object, not list
WEAV_WEAVINGMCSTATUS results = new WEAV_WEAVINGMCSTATUS();
// Uses first record only
if (null != dbResults && dbResults.Count > 0) {
    results.BEAMLOT = dbResults[0].BEAMLOT;
    // ... populate from first record
}
```

**Typical Usage Scenarios**:
1. **Production Dashboard**: Display status of all looms in real-time
2. **Operator Screen**: When operator selects a loom, show current configuration
3. **Setup Verification**: Before starting, verify correct reed/temple installed
4. **Handover**: Shift supervisor checks what each machine is producing

**Important Configuration Fields**:
- **REEDNO/REEDNO2**: Reed selection affects fabric density
- **TEMPLETYPE**: Temple type must match fabric width
- **HEALDCOLOR**: Color coding for heald frames (helps operators verify setup)
- **SPEED**: Loom RPM setting

---

## Related Procedures

**Similar**:
- [012-WARP_GETWARPERMCSTATUS.md](../02_Warping/012-WARP_GETWARPERMCSTATUS.md) - Warper machine status
- [030-BEAM_GETBEAMERMCSTATUS.md](../03_Beaming/030-BEAM_GETBEAMERMCSTATUS.md) - Beamer machine status

**Related**:
- [083-WEAV_WEAVINGINPROCESSLIST.md](./083-WEAV_WEAVINGINPROCESSLIST.md) - List all in-process setups (multiple machines)
- [056-WEAVE_CHECKWEAVINGMC.md](./056-WEAVE_CHECKWEAVINGMC.md) - Check machine availability

**Downstream**:
- Production dashboard UI - Display machine status grid
- Mobile apps - Show machine status to supervisors

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_WEAVINGMCSTATUS()`
**Lines**: 748-793

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_WEAVINGMCSTATUS(WEAV_WEAVINGMCSTATUSParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 084/296 | **Progress**: 28.4%
