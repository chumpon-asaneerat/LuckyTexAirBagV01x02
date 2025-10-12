# [PROCEDURE_NAME]

**Procedure Number**: [NNN]
**Module**: [Module Name]
**Status**: ✅ ANALYZED
**Last Updated**: [Date]

---

## Quick Reference

| Attribute | Value |
|-----------|-------|
| **Operation Type** | SELECT / INSERT / UPDATE / DELETE |
| **Complexity** | Low / Medium / High / Very High |
| **Performance** | Fast / Medium / Slow |
| **Transaction** | Yes / No |
| **Tables Accessed** | N tables (table1, table2, ...) |
| **Called From** | ServiceFile.cs:line |
| **Usage Frequency** | Low / Medium / High |

---

## Purpose

Brief description of what this procedure does and why it exists.

---

## Parameters

### Input Parameters (IN)

| Parameter | Type | Required | Description | Example |
|-----------|------|----------|-------------|---------|
| `P_PARAM1` | VARCHAR2(50) | ✅ Yes | Description | "VALUE1" |
| `P_PARAM2` | NUMBER | ⬜ No | Description | 123 |

### Output Parameters (OUT)

| Parameter | Type | Description |
|-----------|------|-------------|
| `O_RESULT` | NUMBER | Return code (0=success, -1=error) |

---

## Return Value

**Type**: Result Set / Number / None
**Format**: List of records / Single value

### Return Columns (if applicable)

| Column | Type | Description |
|--------|------|-------------|
| `COLUMN1` | VARCHAR2(50) | Description |
| `COLUMN2` | NUMBER | Description |

---

## Database Operations

### Tables Accessed

#### Primary Tables
- **`tblTableName`**
  - Operation: SELECT / INSERT / UPDATE / DELETE
  - Columns: column1, column2, column3
  - Filter: WHERE condition
  - Join: (if applicable)

#### Reference Tables
- **`tblRefTable`**
  - Operation: SELECT (JOIN)
  - Columns: ref_col1, ref_col2
  - Purpose: Lookup / Validation

### Indexes Used

```sql
-- Existing or recommended indexes
CREATE INDEX idx_name ON tblTableName(column1, column2);
```

---

## Business Logic

### Workflow

1. Step 1 description
2. Step 2 description
3. Step 3 description
4. ...

### Validation Rules

- Rule 1
- Rule 2
- Rule 3

### Error Handling

```sql
-- Error handling logic
-- RAISE_APPLICATION_ERROR codes
-- Exception handling
```

---

## Performance Analysis

### Query Performance

| Metric | Value |
|--------|-------|
| Estimated Execution Time | < 100ms / 1-3 sec / > 5 sec |
| Rows Scanned | Estimated count |
| Index Usage | Yes / No |
| Network Roundtrips | Count |

### Optimization Notes

✅ **Good**: What works well
⚠️ **Issue**: What needs improvement
🔴 **Critical**: Critical performance problems

---

## Usage Patterns

### Called From

**File**: `ServiceFile.cs`
**Line**: NNN
**Method**: `MethodName(parameters)`

### Call Frequency

- **Per Operation**: N calls
- **Per Shift**: N calls
- **Daily Average**: N calls

### Typical Workflow

```
User action 1
    ↓
System process
    ↓
Calls this procedure
    ↓
Result handling
```

---

## C# Integration

### DataService Method

```csharp
public ReturnType ProcedureName(parameters)
{
    // Method implementation
    // Parameter setup
    // Database call
    // Result mapping
    // Return
}
```

### UI Usage

**Page**: `PageName.xaml.cs`
**Method**: `MethodName()`

```csharp
private void MethodName()
{
    // UI logic
    // Call DataService
    // Handle result
    // Update UI
}
```

---

## Issues Identified

### Current Issues

#### ⚠️ Issue Title
**Severity**: 🔴 HIGH / 🟠 MEDIUM / 🟡 LOW
**Issue**: Description of the problem
**Impact**: What this affects
**Affected Code**: File.cs:line

#### ⚠️ Another Issue
**Severity**: Priority level
**Issue**: Description
**Impact**: Impact description
**Affected Code**: Location

---

## Related Procedures

### Upstream (Called Before)
- [NNN-PROCEDURE_NAME.md](./NNN-PROCEDURE_NAME.md) - Description

### Downstream (Called After)
- [NNN-PROCEDURE_NAME.md](./NNN-PROCEDURE_NAME.md) - Description

### Similar Procedures
- [NNN-PROCEDURE_NAME.md](../OtherModule/NNN-PROCEDURE_NAME.md) - Description

---

## Change History

| Date | Version | Changes | Author |
|------|---------|---------|--------|
| YYYY-MM-DD | 1.0 | Initial analysis | Database Analysis Team |

---

## SQL Definition

```sql
CREATE OR REPLACE PROCEDURE PROCEDURE_NAME(
    P_PARAM1 IN VARCHAR2,
    P_PARAM2 IN NUMBER,
    result_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    -- Procedure implementation
    OPEN result_cursor FOR
        SELECT ...
        FROM ...
        WHERE ...;
END PROCEDURE_NAME;
/
```

---

**Analysis Status**: ✅ Complete
**File Number**: NNN/296
**Progress**: X.X% of total database analysis
