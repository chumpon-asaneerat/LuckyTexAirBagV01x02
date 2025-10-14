# WEAV_SEARCHPRODUCTION

**Procedure Number**: 079 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Search weaving production setup records by loom, beam roll, item, or date |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:1216 → WEAV_SEARCHPRODUCTION() |
| **Frequency** | Medium |
| **Performance** | Medium |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_LOOMNO` | VARCHAR2 | ⬜ | Loom machine number (search filter) |
| `P_BEAMERROLL` | VARCHAR2 | ⬜ | Beam roll number (search filter) |
| `P_ITMWEAVING` | VARCHAR2 | ⬜ | Weaving item code (search filter) |
| `P_SETDATE` | VARCHAR2 | ⬜ | Setup date (search filter, likely formatted string) |

**Note**: All parameters are optional - can be used in combination for flexible search

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `MC` | VARCHAR2 | Machine/loom number |
| `REEDNO2` | VARCHAR2 | Reed number (2nd reed) |
| `WEFTYARN` | VARCHAR2 | Weft yarn specification |
| `TEMPLETYPE` | VARCHAR2 | Temple type used in weaving |
| `BEAMLOT` | VARCHAR2 | Beam lot number |
| `BARNO` | VARCHAR2 | Bar number |
| `STARTDATE` | DATE | Production start date |
| `FINISHDATE` | DATE | Production finish date |
| `FINISHFLAG` | VARCHAR2 | Finish status flag |
| `SETTINGBY` | VARCHAR2 | Operator who performed setup |
| `EDITDATE` | DATE | Last edit date |
| `EDITBY` | VARCHAR2 | Last editor |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `PRODUCTTYPEID` | VARCHAR2 | Product type identifier |
| `WIDTH` | NUMBER | Fabric width (cm) |
| `BEAMLENGTH` | NUMBER | Beam length (meters) |
| `SPEED` | NUMBER | Weaving speed (RPM) |
| `BEAMERNO` | VARCHAR2 | Beamer machine number |

---

## Business Logic

Provides flexible search functionality for weaving production setup records. Used to:

1. **Find Production History**: Search past production runs by various criteria
2. **Review Setup Details**: Retrieve complete setup information (reed, temple, weft yarn)
3. **Audit Production**: Track who set up machines and when production occurred
4. **Production Analysis**: Analyze production patterns by machine, item, or date range

**Search Flexibility**:
- All parameters are optional (can search by any combination)
- Empty/null parameters are ignored in search criteria
- Can search by:
  - Single loom (P_LOOMNO)
  - Specific beam roll (P_BEAMERROLL)
  - Item code (P_ITMWEAVING)
  - Setup date (P_SETDATE)
  - Any combination of above

**Business Rules**:
- Returns setup details including mechanical configuration (reed, temple type)
- Shows production timeline (start/finish dates)
- Tracks completion status (FINISHFLAG)
- Includes audit trail (SETTINGBY, EDITBY, EDITDATE)
- May return multiple rows if search criteria match multiple setups

**Typical Usage Scenarios**:
1. **Operator Search**: "Show me all setups for Loom #10 this week"
2. **Material Tracking**: "Find all production using beam roll BR-12345"
3. **Item History**: "List all setups for item code AB-100"
4. **Date Range**: "Show production setups from last month"

---

## Related Procedures

**Related**:
- [059-WEAVE_INSERTPROCESSSETTING.md](./059-WEAVE_INSERTPROCESSSETTING.md) - Insert process settings (creates records searched here)
- [061-WEAVE_UPDATEPROCESSSETTING.md](./061-WEAVE_UPDATEPROCESSSETTING.md) - Update process settings
- [055-WEAVE_CHECKITEMPREPARE.md](./055-WEAVE_CHECKITEMPREPARE.md) - Check item preparation before setup

**Similar**:
- [021-WARP_SEARCHWARPRECORD.md](../02_Warping/021-WARP_SEARCHWARPRECORD.md) - Search warping records
- [043-BEAM_SEARCHBEAMRECORD.md](../03_Beaming/043-BEAM_SEARCHBEAMRECORD.md) - Search beaming records

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_SEARCHPRODUCTION()`
**Lines**: 1216-1272

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_SEARCHPRODUCTION(WEAV_SEARCHPRODUCTIONParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 079/296 | **Progress**: 26.7%
