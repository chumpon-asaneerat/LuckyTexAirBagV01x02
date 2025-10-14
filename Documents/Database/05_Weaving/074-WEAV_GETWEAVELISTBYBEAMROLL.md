# WEAV_GETWEAVELISTBYBEAMROLL

**Procedure Number**: 074 | **Module**: M05 - Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieves weaving lot history for a specific beam roll and loom |
| **Operation** | SELECT |
| **Called From** | WeavingDataService.cs:851 → WEAV_GETWEAVELISTBYBEAMROLL() |
| **Frequency** | Medium |
| **Performance** | Fast |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_BEAMROLL` | VARCHAR2 | ✅ | Beam roll number to query weaving history |
| `P_LOOM` | VARCHAR2 | ✅ | Loom machine number |

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `WEAVINGLOT` | VARCHAR2 | Weaving lot number (production identifier) |
| `ITM_WEAVING` | VARCHAR2 | Weaving item code |
| `LENGTH` | NUMBER | Fabric length produced (meters) |
| `LOOMNO` | VARCHAR2 | Loom machine number |
| `WEAVINGDATE` | DATE | Weaving production date |
| `SHIFT` | VARCHAR2 | Production shift |
| `REMARK` | VARCHAR2 | Additional remarks |
| `CREATEDATE` | DATE | Record creation date |
| `WIDTH` | NUMBER | Fabric width (cm) |
| `PREPAREBY` | VARCHAR2 | Operator who prepared the setup |
| `WEAVINGNO` | VARCHAR2 | Sequential weaving number |
| `BEAMLOT` | VARCHAR2 | Beam lot number |
| `DOFFNO` | VARCHAR2 | Doff number (fabric roll identifier) |
| `TENSION` | NUMBER | Warp tension setting |
| `STARTDATE` | DATE | Production start date/time |
| `DOFFBY` | VARCHAR2 | Operator who performed doffing |
| `SPEED` | NUMBER | Loom speed (RPM) |
| `WASTE` | NUMBER | Waste amount (meters) |
| `DENSITY_WARP` | NUMBER | Warp density (ends/cm) |
| `DENSITY_WEFT` | NUMBER | Weft density (picks/cm) |
| `DELETEFLAG` | VARCHAR2 | Deletion status (0=Deleted, 1=Active) |
| `DELETEBY` | VARCHAR2 | User who deleted the record |
| `DELETEDATE` | DATE | Deletion timestamp |

---

## Business Logic

Retrieves complete weaving production history for a specific beam roll and loom combination. Used when operators need to:

1. **Review Production History**: View all weaving lots produced from a specific beam roll on a particular loom
2. **Track Beam Usage**: Monitor how many fabric rolls (doffs) were produced from one beam
3. **Audit Production**: Investigate production parameters (speed, tension, density) used in previous runs
4. **Deleted Records**: Include deleted records in results for audit trail (DELETEFLAG indicates status)

**Business Rules**:
- Returns multiple rows if the beam roll was used multiple times on the same loom
- Includes deleted records (DELETEFLAG = "0") for historical tracking
- Result set is numbered sequentially (RowNo) in C# code for grid display
- DeleteHistory flag is reversed in C# logic: DELETEFLAG "1" = "No" (active), DELETEFLAG "0" = "Yes" (deleted)

**Typical Usage Scenario**: When operator scans a beam roll barcode at the loom, the system loads all previous weaving history to show what was produced before, helping with production planning and quality tracking.

---

## Related Procedures

**Downstream**:
- [063-WEAV_DEFECTLIST.md](./063-WEAV_DEFECTLIST.md) - Get defect records for weaving lots
- [073-WEAV_GETSAMPLINGDATA.md](./073-WEAV_GETSAMPLINGDATA.md) - Get sampling data for weaving lots
- [071-WEAV_GETMCSTOPBYLOT.md](./071-WEAV_GETMCSTOPBYLOT.md) - Get machine stop history for lots

**Similar**:
- [068-WEAV_GETINPROCESSBYBEAMROLL.md](./068-WEAV_GETINPROCESSBYBEAMROLL.md) - Get in-process weaving data by beam roll

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETWEAVELISTBYBEAMROLL()`
**Lines**: 851-920

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETWEAVELISTBYBEAMROLL(WEAV_GETWEAVELISTBYBEAMROLLParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 074/296 | **Progress**: 25.0%
