# WEAV_GETMACHINEZONELIST

**Procedure Number**: 070 | **Module**: M05 Weaving | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Get machine count statistics by production zone and machine type |
| **Operation** | SELECT (aggregate/summary query) |
| **Called From** | WeavingDataService.cs:514 → WEAV_GETMACHINEZONELIST() |
| **Frequency** | Low - Dashboard/reporting displays |
| **Performance** | Fast - Aggregated summary data |
| **Issues** | 🟡 1 Low - C# filters by zone after fetching all zones (inefficient) |

---

## Parameters

### Input (IN)

None - Returns statistics for all zones

**Note**: C# service layer accepts zone parameter but doesn't pass it to SP - filters in C# instead

### Output (OUT)

None

### Returns (Cursor)

| Column | Type | Description |
|--------|------|-------------|
| `ZONE` | VARCHAR2 | Production zone identifier (A, B, C, etc.) |
| `TYPE` | VARCHAR2 | Machine type classification |
| `TOTAL` | NUMBER | Count of machines in this zone/type combination |

---

## Business Logic (What it does and why)

**Purpose**: Provides summary statistics of weaving machines grouped by production zone and machine type, used for capacity planning, dashboard displays, and resource allocation.

**Business Context**:
- Weaving factory is divided into production zones (A, B, C, etc.)
- Each zone contains different types of machines (looms, automatic looms, etc.)
- Management needs to know machine count per zone for:
  - Capacity planning
  - Production scheduling
  - Maintenance planning
  - Resource allocation
  - Dashboard KPI displays

**Production Zones**:
- **Zone A**: May be for specific product types (e.g., narrow fabric)
- **Zone B**: May be for different product types (e.g., wide fabric)
- **Zone C**: May be specialized zone (e.g., high-speed looms)
- Zones help organize production floor and assign work orders

**Machine Types**:
- **LOOM**: Standard weaving looms
- **AUTO**: Automatic weaving machines
- **AIRJET**: Air-jet looms (high-speed)
- **RAPIER**: Rapier looms
- Other specialized machine types

**Usage Scenarios**:

**Scenario 1: Dashboard Display**
1. Production dashboard loads on manager's screen
2. Calls WEAV_GETMACHINEZONELIST()
3. Returns counts for all zones
4. Displays summary cards:
   - Zone A: 23 machines (15 LOOM, 8 AUTO)
   - Zone B: 18 machines (12 LOOM, 6 AUTO)
   - Zone C: 20 machines (20 LOOM)
5. Manager sees capacity at a glance

**Scenario 2: Capacity Planning**
1. Planning team evaluating zone capacity
2. Calls procedure to get current machine distribution
3. Service layer filters by specific zone (e.g., "Zone A")
4. Returns TYPE and TOTAL for that zone only
5. Used to determine if zone can handle new work orders

**Scenario 3: Resource Allocation**
1. New order requires 10 looms for 3 days
2. System checks which zone has available capacity
3. Iterates through zones to find sufficient machines
4. Allocates work order to zone with capacity

**Scenario 4: Maintenance Scheduling**
1. Maintenance team planning preventive maintenance
2. Needs to know machine distribution by zone
3. Gets totals per zone to plan maintenance without stopping entire zone
4. Schedules maintenance to maintain minimum production capacity

**C# Implementation Pattern**:

**Inefficiency**: Service layer accepts `zone` parameter but procedure doesn't:
```csharp
// Line 514: Method accepts zone parameter
public List<WEAV_GETMACHINEZONELIST> WEAV_GETMACHINEZONELIST(string zone)
{
    // Calls SP with NO parameters
    dbResults = DatabaseManager.Instance.WEAV_GETMACHINEZONELIST(dbPara);

    // Filters in C# after fetching ALL zones
    foreach (var dbResult in dbResults)
    {
        if (dbResult.ZONE == zone)  // Line 533
        {
            // Only returns matching zone
            results.Add(inst);
            break;
        }
    }
}
```

**Better Approach**: Pass zone to SP and filter in SQL:
```sql
-- Recommended stored procedure modification
WHERE ISACTIVE = 'Y'
  AND (P_ZONE IS NULL OR ZONE = P_ZONE)
GROUP BY ZONE, TYPE
```

---

## Related Procedures

**Machine Management**:
- Machine master data queries
- Machine status procedures (WEAV_WEAVINGMCSTATUS)
- Machine allocation procedures

**Zone Management**:
- Zone configuration procedures
- Zone assignment procedures

**Dashboard/Reporting**:
- Production summary by zone
- Efficiency reports by zone
- Utilization reports by machine type

---

## Query/Code Location

**Note**: This project uses Oracle stored procedures called from C# DataService classes.

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\WeavingDataService.cs`
**Method**: `WEAV_GETMACHINEZONELIST()`
**Lines**: 514-552

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Method**: `WEAV_GETMACHINEZONELIST(WEAV_GETMACHINEZONELISTParameter para)`
**Lines**: (locate in AirbagSPs.cs)

---

**File**: 070/296 | **Progress**: 23.6%
