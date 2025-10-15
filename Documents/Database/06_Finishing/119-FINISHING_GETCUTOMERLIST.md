# FINISHING_GETCUTOMERLIST

**Procedure Number**: 119 | **Module**: M06 - Finishing | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Retrieve list of customers who have finishing operations (lookup/dropdown data) |
| **Operation** | SELECT DISTINCT |
| **Tables** | tblFinishingCoating, tblFinishingScouring, tblFinishingDryer (inferred - UNION DISTINCT) |
| **Called From** | DataServicecs.cs:3369 → GetFinishingCustomerDataList() |
| **Frequency** | Low (cached/lookup data) |
| **Performance** | Fast |
| **Issues** | None identified |

---

## Parameters

### Input (IN)

None (no parameters - returns all customers)

### Output (OUT)

None (returns cursor)

### Returns (cursor)

| Column | Type | Description |
|--------|------|-------------|
| `FINISHINGCUSTOMER` | VARCHAR2 | Customer ID (unique list) |

---

## Business Logic

**Purpose**: Provide a distinct list of customers who have finishing operations in the system. This is a lookup/reference procedure used to populate dropdown lists, filters, and selection controls in the UI.

**When Used**:

**Scenario 1: Process Preparation Page Load**
1. Operator opens any finishing preparation page (coating/scouring/dryer)
2. Page initialization calls FINISHING_GETCUTOMERLIST
3. Returns list of customers: ["TOYOTA", "HONDA", "NISSAN", "FORD", ...]
4. Populates customer dropdown/combo box
5. Operator selects customer for the lot being processed

**Scenario 2: Search Filter Setup**
1. User opens finishing search/report page
2. Customer filter dropdown needs to be populated
3. Calls FINISHING_GETCUTOMERLIST
4. Returns available customers
5. User can filter by specific customer

**Scenario 3: Production Planning**
1. Production planner reviews customer workload
2. System needs list of active customers
3. Calls FINISHING_GETCUTOMERLIST
4. Shows all customers with finishing operations
5. Planner checks capacity per customer

**Scenario 4: Customer Selection in New Lot Entry**
1. Operator starts new finishing operation
2. Customer selection required
3. FINISHING_GETCUTOMERLIST provides valid options
4. Only shows customers who actually use finishing services
5. Prevents entry of invalid customer IDs

**Business Rules**:
- **No parameters**: Returns ALL customers (no filtering)
- **DISTINCT list**: Each customer appears only once
- **Active customers**: Only customers with finishing records
- **All processes**: UNION of coating + scouring + dryer customers
- **Lookup data**: Typically cached by application for performance
- **Lightweight**: Returns only customer ID (minimal data)

**Implementation Pattern**:
```sql
-- Inferred query structure
SELECT DISTINCT FINISHINGCUSTOMER
FROM tblFinishingCoating
WHERE FINISHINGCUSTOMER IS NOT NULL

UNION

SELECT DISTINCT FINISHINGCUSTOMER
FROM tblFinishingScouring
WHERE FINISHINGCUSTOMER IS NOT NULL

UNION

SELECT DISTINCT FINISHINGCUSTOMER
FROM tblFinishingDryer
WHERE FINISHINGCUSTOMER IS NOT NULL

ORDER BY FINISHINGCUSTOMER
```

**Performance Optimization**:
- Result set typically small (10-50 customers)
- Application often caches result
- Refresh only when new customer added
- No complex joins needed

**Typical Data**:
```
FINISHINGCUSTOMER
-----------------
FORD
HONDA
NISSAN
TOYOTA
... (more customers)
```

**UI Usage Pattern**:
```csharp
// Page initialization
List<FinishingCustomerData> customers =
    DataService.Instance.GetFinishingCustomerDataList();

// Populate dropdown
cmbCustomer.ItemsSource = customers;
cmbCustomer.DisplayMemberPath = "FINISHINGCUSTOMER";

// User selects customer
string selectedCustomer = cmbCustomer.SelectedValue;
```

---

## Related Procedures

**Similar Lookup Procedures**:
- [120-FINISHING_GETITEMGOOD.md](./120-FINISHING_GETITEMGOOD.md) - Get items for customer
- WEAV_GETCUSTOMERLIST (M05) - Weaving customer list
- INS_GETCUSTOMERLIST (M08) - Inspection customer list
- COMMON_GETCUSTOMERLIST (Common) - System-wide customer list

**Usage Chain**:
1. **FINISHING_GETCUTOMERLIST** → Returns customer list
2. User selects customer
3. **FINISHING_GETITEMGOOD**(customer) → Returns items for that customer
4. User selects item
5. Process begins with customer + item

**Upstream**:
- [093-FINISHING_INSERTCOATING.md](./093-FINISHING_INSERTCOATING.md) - Creates coating with customer
- [099-FINISHING_INSERTSCOURING.md](./099-FINISHING_INSERTSCOURING.md) - Creates scouring with customer
- [109-FINISHING_INSERTDRYER.md](./109-FINISHING_INSERTDRYER.md) - Creates dryer with customer

---

## Query/Code Location

**DataService File**: `LuckyTex.AirBag.Core\Services\DataService\DataServicecs.cs` (Legacy)
**Method**: `GetFinishingCustomerDataList()`
**Lines**: 3365-3404

**Database Manager File**: `LuckyTex.AirBag.Core\Domains\AirbagSPs.cs`
**Parameter Class**: `FINISHING_GETCUTOMERLISTParameter`
**Lines**: 8566-8568 (empty - no parameters)

**Result Class**: `FINISHING_GETCUTOMERLISTResult`
**Lines**: 8574-8577

**Method**: `FINISHING_GETCUTOMERLIST(FINISHING_GETCUTOMERLISTParameter para)`
**Lines**: 28035-28085 (estimated)

---

**File**: 119/296 | **Progress**: 40.2%
