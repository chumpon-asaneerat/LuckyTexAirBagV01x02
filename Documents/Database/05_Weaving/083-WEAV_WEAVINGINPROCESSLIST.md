# WEAV_WEAVINGINPROCESSLIST

**Procedure Number**: 083 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieves in-process weaving setup details for a specific beam roll and doff number |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:689 → WEAV_WEAVINGINPROCESSLIST() |
| **Frequency** | High (Production monitoring) |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2 | ⬜ | Beam roll number filter |
| `P_DOFFNO` | NUMBER | ⬜ | Doff number filter |

**Note**: Parameters are optional - likely support flexible search criteria

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `BEAMLOT` | VARCHAR2 | Beam lot number |
| `MC` | VARCHAR2 | Machine/loom number |
| `REEDNO2` | VARCHAR2 | Reed number (2nd reed) |
| `WEFTYARN` | VARCHAR2 | Weft yarn specification |
| `TEMPLETYPE` | VARCHAR2 | Temple type used |
| `BARNO` | VARCHAR2 | Bar number |
| `STARTDATE` | DATE | Production start date/time |
| `FINISHDATE` | DATE | Production finish date/time |
| `FINISHFLAG` | VARCHAR2 | Completion flag |
| `SETTINGBY` | VARCHAR2 | Operator who performed setup |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2 | Last editor |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type identifier |
| `WIDTH` | NUMBER | Fabric width (cm) |

---

## Business Logic

Retrieves in-process weaving production details, showing active or recent weaving setups with their machine configurations. Used for:

1. **Production Monitoring**: Track current weaving jobs in progress
2. **Setup Verification**: Verify machine configuration (reed, temple, weft yarn) before starting
3. **Status Checking**: Determine if production is still in progress or completed (FINISHFLAG)
4. **Load Context**: When operator scans beam roll/doff, load existing setup details
5. **Continuation**: Resume weaving with correct settings if production was interrupted

**Business Rules**:
- Returns setup records that are currently in-process or recently completed
- FINISHFLAG indicates completion status
- May filter by beam roll, doff number, or both
- Shows complete machine setup configuration for verification
- Includes audit trail (SETTINGBY, EDITBY, dates)

**FINISHFLAG Values** (likely):
- Empty or "0" = In process
- "1" = Completed/Finished

**Typical Usage Scenarios**:

1. **Start Production**: Operator scans beam roll → system loads in-process setup details
2. **Resume After Break**: Operator returns to loom → view what was being produced
3. **Quality Check**: QA needs to verify what setup was used for specific doff
4. **Production Dashboard**: Display all in-process weaving jobs across all looms

**Important Fields**:
- **REEDNO2**: Reed configuration affects fabric density
- **TEMPLETYPE**: Temple holds fabric edges during weaving
- **WEFTYARN**: Weft yarn type being used
- **STARTDATE/FINISHDATE**: Calculate production duration

---

## Related Procedures

**Related**:
- [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Create process settings (creates records queried here)
- [061-WEAVE_UPDATEPROCESSSETTING.md](./061-WEAVE_UPDATEPROCESSSETTING.md) - Update process settings
- [079-WEAV_SEARCHPRODUCTION.md](./079-WEAV_SEARCHPRODUCTION.md) - Broader production search (similar purpose)

**Downstream**:
- [062-WEAVE_WEAVINGPROCESS.md](./062-WEAVE_WEAVINGPROCESS.md) - Main weaving process uses this setup data

**Similar**:
- [068-WEAV_GETINPROCESSBYBEAMROLL.md](./068-WEAV_GETINPROCESSBYBEAMROLL.md) - Get in-process data (similar name, different purpose)

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_WEAVINGINPROCESSLIST()`
**Lines**: 689-740

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_WEAVINGINPROCESSLIST(WEAV_WEAVINGINPROCESSLISTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 083/296 | **Progress**: 28.0%
