# [PROCEDURE_NAME]

**Procedure Number**: [NNN] | **Module**: [Module Name] | **Status**: ✅ ANALYZED

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Purpose** | Brief one-line description |
| **Operation** | SELECT / INSERT / UPDATE / DELETE |
| **Tables** | table1, table2, table3 |
| **Called From** | ServiceFile.cs:line → MethodName() |
| **Frequency** | Low / Medium / High |
| **Performance** | Fast / Medium / Slow |
| **Issues** | 🔴 N High / 🟠 N Medium / 🟡 N Low |

---

## Parameters

### Input (IN)

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| `P_PARAM1` | VARCHAR2(50) | ✅ | Description |
| `P_PARAM2` | NUMBER | ⬜ | Description |

### Output (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `O_RESULT` | NUMBER | Return code (0=success, -1=error) |

### Returns (if cursor)

| Column | Type | Description |
|--------|------|-------------|
| `COLUMN1` | VARCHAR2(50) | Description |
| `COLUMN2` | NUMBER | Description |

---

## Database Operations

### Tables

**Primary Tables**:
- `tblTableName` - INSERT/UPDATE - Main operation table
- `tblTableName2` - SELECT - Lookup/validation

**Transaction**: Yes / No

### Indexes (if relevant)

```sql
-- Missing or existing indexes
CREATE INDEX idx_name ON tblTableName(column1, column2);
```

---

## Business Logic (What it does and why)

Brief description of the business purpose and workflow:
- What business problem does this solve?
- When is it used in the production process?
- What business rules are enforced?
- What happens step-by-step?

**Example**: "Gets warping specifications for machine setup. When operator selects an item code and machine, this validates the item exists, checks machine is active, and returns production parameters (speed, tension, hardness) so operator can configure the warping machine correctly."

---

## Related Procedures

**Upstream**: [NNN-PROCEDURE_NAME.md](./NNN-PROCEDURE_NAME.md) - Called before this
**Downstream**: [NNN-PROCEDURE_NAME.md](./NNN-PROCEDURE_NAME.md) - Called after this
**Similar**: [NNN-PROCEDURE_NAME.md](../Module/NNN-PROCEDURE_NAME.md) - Similar logic

---

## Query/Code Location

**Note**: This project does NOT use stored procedures in the database. Queries are hardcoded in C# DataService classes.

**File**: `ServiceName.cs`
**Method**: `MethodName()`
**Line**: NNN-NNN

**Query Type**: SELECT / INSERT / UPDATE / DELETE / ExecuteNonQuery

```csharp
// Code snippet showing the query
string query = @"
    SELECT column1, column2, column3
    FROM tblTableName
    WHERE condition = @param";

// Or if using stored procedure call:
// OracleCommand cmd = new OracleCommand("PROCEDURE_NAME", conn);
```

---

**File**: NNN/296 | **Progress**: X.X%
