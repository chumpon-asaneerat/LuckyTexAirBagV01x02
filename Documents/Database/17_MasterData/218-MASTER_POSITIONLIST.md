# MASTER_POSITIONLIST

**Procedure Number**: 218 | **Module**: M17 - Master Data | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get list of all employee positions/roles for master data |
| **Operation** | SELECT |
| **Tables** | Position master table |
| **Called From** | DataService.cs (Legacy) |
| **Frequency** | Low |
| **Performance** | Fast |
| **Issues** | 🟢 None identified |

---

## Parameters

### Input (IN)

None - Returns all positions

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `POSITIONLEVEL` | VARCHAR2(50) | Position level/rank code |
| `POSITIONNAME` | VARCHAR2(200) | Position name/title |

---

## Business Logic (What it does and why)

Retrieves complete list of employee positions/roles configured in the system. Used for populating dropdown lists in user management and authorization screens where administrators assign positions to employees. Positions determine access levels and permissions in the system.

The procedure:
1. Queries all active position records
2. Returns position level and name
3. Used for UI dropdown population in user management
4. No filtering - returns all positions

Common positions include: Operator, Supervisor, Manager, Administrator, Quality Inspector, Technician, etc.

---

## Related Procedures

**Used By**: M20 User Management module for role assignment

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataService.cs`
**Method**: Method name to be confirmed
**Lines**: To be confirmed

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `MASTER_POSITIONLIST(MASTER_POSITIONLISTParameter para)`
**Lines**: 17363-17400

---

**File**: 218/296 | **Progress**: 73.6%
