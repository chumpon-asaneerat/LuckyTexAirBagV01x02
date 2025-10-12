# Warping Module - Database Analysis

**Module**: M02 - Warping
**DataService File**: WarpingDataService.cs
**Created**: 2025-10-12
**Status**: ✅ EXAMPLE - This is what will be generated

---

## Module Overview

**Purpose**: Warping production tracking - beam production from yarn pallets
**Total Stored Procedures**: 26 procedures
**Database Calls**: ~150+ calls across service
**Complexity**: Very High

---

## Stored Procedures Inventory

### WARP_* Procedures (26 total)

#### ✅ WARP_GETSPECBYCHOPNOANDMC
**Status**: Analyzed
**Purpose**: Get warping specifications by item code and machine number
**Operation Type**: SELECT
**Called From**: WarpingDataService.cs:258

**Parameters** (IN):
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| P_ITMPREPARE | VARCHAR2 | Yes | Item preparation code |
| P_MCNO | VARCHAR2 | Yes | Machine number |

**Returns**: Result Set (List)
| Column | Type | Description |
|--------|------|-------------|
| CHOPNO | VARCHAR2 | Item code |
| ITM_YARN | VARCHAR2 | Yarn item code |
| WARPERENDS | NUMBER | Number of warper ends |
| MAXLENGTH | NUMBER | Maximum beam length |
| MINLENGTH | NUMBER | Minimum beam length |
| WAXING | VARCHAR2 | Waxing type |
| COMBTYPE | VARCHAR2 | Comb type |
| COMBPITCH | NUMBER | Comb pitch |
| KEBAYARN | VARCHAR2 | Keba yarn |
| NOWARPBEAM | NUMBER | Number of warp beams |
| MAXHARDNESS | NUMBER | Maximum hardness |
| MINHARDNESS | NUMBER | Minimum hardness |
| MCNO | VARCHAR2 | Machine number |
| SPEED | NUMBER | Speed specification |
| SPEED_MARGIN | NUMBER | Speed tolerance margin |
| YARN_TENSION | NUMBER | Yarn tension specification |
| YARN_TENSION_MARGIN | NUMBER | Yarn tension margin |
| WINDING_TENSION | NUMBER | Winding tension specification |
| WINDING_TENSION_MARGIN | NUMBER | Winding tension margin |
| NOCH | NUMBER | Number of channels |

**Tables Accessed**:
- `tblWarpingSpec` (SELECT)
- `tblItemMaster` (SELECT - JOIN)

**Business Logic**:
- Retrieves warping production specifications for machine setup
- Joins item master data with warping specifications
- Filters by item preparation code and machine number
- Used during warping setup phase

**Performance**: Fast (indexed on ITMPREPARE, MCNO)

**Usage Count**: Called 15+ times in WarpingProcessPage.xaml.cs

---

#### ✅ WARP_INSERTPALLETS
**Status**: Analyzed
**Purpose**: Insert yarn pallet allocation for warping
**Operation Type**: INSERT
**Called From**: WarpingDataService.cs:445

**Parameters** (IN):
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| P_HEADNO | VARCHAR2 | Yes | Warp head number |
| P_SIDE | VARCHAR2 | Yes | Side (A or B) |
| P_SEQ | NUMBER | Yes | Sequence number |
| P_PALLETNO | VARCHAR2 | Yes | Pallet barcode |
| P_USE | NUMBER | Yes | Use quantity (kg) |
| P_REJECT | NUMBER | Yes | Reject quantity (kg) |
| P_REMAIN | NUMBER | Yes | Remaining quantity (kg) |
| P_WARPINGUSEBY | VARCHAR2 | Yes | User ID |
| P_WARPINGUSEDATE | DATE | Yes | Use date/time |

**Returns**: None (ExecuteNonQuery)

**Tables Accessed**:
- `tblWarpingPallets` (INSERT)
- `tblYarnStock` (UPDATE - decrement stock)

**Business Logic**:
- Inserts pallet usage record
- Updates yarn stock quantities (REMAIN = REMAIN - USE)
- Validates pallet exists in stock before insert
- Tracks which operator used the pallet

**Transaction**: Yes (INSERT + UPDATE must be atomic)

**Performance**: Fast (single row insert)

**Usage Count**: Called 50+ times per warping setup (one per pallet)

**Critical Notes**:
- ⚠️ **MUST use transaction** - Stock update failure = data inconsistency
- ⚠️ **No rollback if UPDATE fails** - Risk of orphaned insert record

---

#### ✅ WARP_UPDATEBEAMQUALITY
**Status**: Analyzed
**Purpose**: Update quality metrics for warp beam during production
**Operation Type**: UPDATE
**Called From**: WarpingDataService.cs:789

**Parameters** (IN):
| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| P_BEAMLOT | VARCHAR2 | Yes | Beam lot number (barcode) |
| P_SPEED | NUMBER | No | Actual speed (m/min) |
| P_YARN_TENSION | NUMBER | No | Actual yarn tension |
| P_WINDING_TENSION | NUMBER | No | Actual winding tension |
| P_HARDNESS_L | NUMBER | No | Hardness left side |
| P_HARDNESS_M | NUMBER | No | Hardness middle |
| P_HARDNESS_R | NUMBER | No | Hardness right side |
| P_MCL | NUMBER | No | Machine condition L |
| P_MCS | NUMBER | No | Machine condition S |
| P_LENGTH | NUMBER | No | Actual beam length |
| P_UPDATEBY | VARCHAR2 | Yes | User ID |
| P_UPDATEDATE | DATE | Yes | Update timestamp |

**Returns**: None (ExecuteNonQuery)

**Tables Accessed**:
- `tblWarpingBeam` (UPDATE)

**Business Logic**:
- Updates quality metrics during production
- Only updates non-null parameters (allows partial updates)
- Validates beam exists before update
- Tracks who updated and when

**Transaction**: No (single UPDATE)

**Performance**: Fast (UPDATE by primary key BEAMLOT)

**Usage Count**: Called every time operator enters quality data (~200+ times/day)

**Validation Rules** (in procedure):
```sql
-- Speed must be within ±10% of specification
IF P_SPEED IS NOT NULL THEN
  IF P_SPEED < (SPEC_SPEED * 0.9) OR P_SPEED > (SPEC_SPEED * 1.1) THEN
    RAISE_APPLICATION_ERROR(-20001, 'Speed out of specification range');
  END IF;
END IF;

-- Hardness values must be 1-10
IF P_HARDNESS_L IS NOT NULL AND (P_HARDNESS_L < 1 OR P_HARDNESS_L > 10) THEN
  RAISE_APPLICATION_ERROR(-20002, 'Hardness value must be 1-10');
END IF;
```

**Critical Notes**:
- ✅ **Good**: Validates against specifications
- ⚠️ **Issue**: No async/await - UI freezes during update
- ⚠️ **Issue**: Error messages in English only (should be localized)

---

#### [ ] WARP_GETWARPERLOTBYHEADNO
**Status**: Pending analysis
**Purpose**: TBD
**Called From**: WarpingDataService.cs:279

---

#### [ ] WARP_INSERTBEAMSTART
**Status**: Pending analysis
**Purpose**: TBD
**Called From**: WarpingDataService.cs:512

---

#### [ ] WARP_UPDATEBEAMDOFFING
**Status**: Pending analysis
**Purpose**: TBD
**Called From**: WarpingDataService.cs:623

---

... (Continue for all 26 WARP_* procedures)

---

## Data Access Patterns

### CRUD Operations Breakdown

| Operation | Count | Percentage |
|-----------|-------|------------|
| SELECT (Read) | 12 procedures | 46% |
| INSERT (Create) | 6 procedures | 23% |
| UPDATE (Update) | 7 procedures | 27% |
| DELETE (Delete) | 1 procedure | 4% |

### Transaction Usage

| Transaction Type | Count | Notes |
|-----------------|-------|-------|
| Explicit Transactions | 3 procedures | WARP_INSERTPALLETS, WARP_DOFFBEAM, WARP_FINISHBEAM |
| No Transactions | 23 procedures | Single operation only |

⚠️ **CONCERN**: Multi-step operations without transactions detected:
- `WARP_INSERTPALLETS` updates 2 tables but no rollback on failure
- `WARP_DOFFBEAM` inserts 3 tables sequentially (risk of partial commit)

### Connection Management

**Pattern Used**: Singleton DatabaseManager
```csharp
dbResults = DatabaseManager.Instance.WARP_GETSPECBYCHOPNOANDMC(dbPara);
```

**Connection Lifecycle**:
- Connection opened per call
- No explicit using statements
- Relies on NLib framework for connection pooling

⚠️ **CONCERN**: No explicit Dispose pattern - potential connection leaks

---

## Database Tables Used

### Primary Tables (Write Operations)

| Table | Operations | Procedures Using |
|-------|------------|------------------|
| `tblWarpingBeam` | INSERT, UPDATE, DELETE | 15 procedures |
| `tblWarpingPallets` | INSERT, UPDATE | 6 procedures |
| `tblWarpingHead` | INSERT, UPDATE | 4 procedures |
| `tblWarpingQuality` | INSERT, UPDATE | 3 procedures |

### Reference Tables (Read Only)

| Table | Purpose | Procedures Using |
|-------|---------|------------------|
| `tblWarpingSpec` | Specifications | 8 procedures |
| `tblItemMaster` | Item codes | 12 procedures |
| `tblMachine` | Machine master | 5 procedures |
| `tblEmployee` | Operators | 8 procedures |
| `tblYarnStock` | Yarn inventory | 6 procedures |

### Shared Tables (Used by Multiple Modules)

| Table | Used By | Notes |
|-------|---------|-------|
| `tblYarnStock` | Warping, Warehouse, G3 | Shared inventory |
| `tblItemMaster` | All modules | Master data |
| `tblEmployee` | All modules | Authentication |
| `tblMachine` | All production modules | Equipment master |

---

## Performance Analysis

### Slow Query Candidates

| Procedure | Estimated Time | Reason | Priority |
|-----------|---------------|--------|----------|
| `WARP_SEARCHBEAMHISTORY` | 2-5 seconds | Full table scan, no date index | 🔴 HIGH |
| `WARP_GETPALLETUSAGE` | 1-3 seconds | Complex JOIN (5 tables) | 🟠 MEDIUM |
| `WARP_REPORTDAILY` | 5-10 seconds | Aggregation on large dataset | 🟡 LOW |

### Missing Indexes (Suspected)

```sql
-- Suggested indexes
CREATE INDEX idx_warpingbeam_date ON tblWarpingBeam(STARTDATE);
CREATE INDEX idx_warpingpallets_headno ON tblWarpingPallets(HEADNO, SIDE);
CREATE INDEX idx_warpingquality_beamlot ON tblWarpingQuality(BEAMLOT);
```

### Connection Leak Risks

⚠️ **HIGH RISK**: WarpingDataService.cs
- No explicit connection disposal in 18 methods
- Relies on GC for connection cleanup
- Under load: potential connection pool exhaustion

---

## Integration Points

### Upstream Modules

| Module | Integration Point | Shared Data |
|--------|------------------|-------------|
| M01 Warehouse | Yarn stock | `tblYarnStock` |
| M12 G3 | Yarn pallets | `tblYarnPallets` |

### Downstream Modules

| Module | Integration Point | Shared Data |
|--------|------------------|-------------|
| M03 Beaming | Warp beams | `tblWarpingBeam` |
| M04 Drawing | Beam allocation | `tblWarpingBeam` |

### D365 ERP Integration

**Procedures Involved**: None directly
**Integration Method**: Via D365DataService.cs after beam completion

---

## Critical Issues Identified

### 🔴 HIGH Priority

1. **Transaction Management** (3 procedures)
   - `WARP_INSERTPALLETS`: Updates 2 tables without transaction
   - `WARP_DOFFBEAM`: Inserts 3 tables sequentially, no rollback
   - **Impact**: Data inconsistency risk
   - **Fix**: Wrap multi-table operations in explicit transactions

2. **Connection Leaks** (18 methods)
   - No explicit using statements
   - Relies on GC for cleanup
   - **Impact**: Connection pool exhaustion under load
   - **Fix**: Implement IDisposable pattern

3. **Performance** (1 procedure)
   - `WARP_SEARCHBEAMHISTORY`: Full table scan
   - **Impact**: 5-10 second queries during reports
   - **Fix**: Add index on STARTDATE column

### 🟠 MEDIUM Priority

4. **No Async Operations** (All 26 procedures)
   - All database calls synchronous
   - **Impact**: UI freezes during long operations
   - **Fix**: Implement async/await pattern

5. **Hardcoded Error Messages** (12 procedures)
   - Error messages in English only
   - **Impact**: Poor user experience for Thai operators
   - **Fix**: Implement localization

### 🟡 LOW Priority

6. **Code Duplication** (8 procedures)
   - Parameter validation duplicated across procedures
   - **Impact**: Maintenance overhead
   - **Fix**: Extract validation to shared methods

---

## Refactoring Recommendations

### Phase 1: Critical Fixes (1-2 weeks)

```csharp
// 1. Add Transaction Support
public class WarpingRepository : IWarpingRepository
{
    public async Task<bool> InsertPalletAsync(WarpingPallet pallet)
    {
        using (var transaction = await _db.BeginTransactionAsync())
        {
            try
            {
                // Insert pallet record
                await _db.ExecuteAsync("WARP_INSERTPALLETS", pallet);

                // Update stock quantity
                await _db.ExecuteAsync("YARN_UPDATESTOCK", new {
                    PalletNo = pallet.PalletNo,
                    Quantity = -pallet.Use
                });

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

// 2. Implement Async/Await
public async Task<List<WarpSpec>> GetSpecificationAsync(string itemCode, string mcNo)
{
    var parameters = new { P_ITMPREPARE = itemCode, P_MCNO = mcNo };
    return await _db.QueryAsync<WarpSpec>("WARP_GETSPECBYCHOPNOANDMC", parameters);
}

// 3. Add Connection Disposal
public class WarpingRepository : IWarpingRepository, IDisposable
{
    private readonly IDbConnection _connection;

    public void Dispose()
    {
        _connection?.Dispose();
    }
}
```

### Phase 2: Performance Optimization (1 week)

```sql
-- Add missing indexes
CREATE INDEX idx_warpingbeam_startdate ON tblWarpingBeam(STARTDATE);
CREATE INDEX idx_warpingbeam_mcno ON tblWarpingBeam(MCNO);
CREATE INDEX idx_warpingpallets_composite ON tblWarpingPallets(HEADNO, SIDE, PALLETNO);

-- Optimize slow query
CREATE OR REPLACE PROCEDURE WARP_SEARCHBEAMHISTORY(
    P_STARTDATE IN DATE,
    P_ENDDATE IN DATE,
    P_MCNO IN VARCHAR2 DEFAULT NULL
) AS
BEGIN
    -- Use index hint for date range
    OPEN result_cursor FOR
        SELECT /*+ INDEX(tblWarpingBeam idx_warpingbeam_startdate) */
            *
        FROM tblWarpingBeam
        WHERE STARTDATE BETWEEN P_STARTDATE AND P_ENDDATE
          AND (P_MCNO IS NULL OR MCNO = P_MCNO);
END;
```

### Phase 3: Architecture Refactoring (2-3 weeks)

```
Current: WarpingProcessPage → WarpingDataService → DatabaseManager → Oracle

Refactored:
WarpingProcessPage (ViewModel)
    ↓
IWarpingService (Interface)
    ↓
WarpingService (Business Logic)
    ↓
IWarpingRepository (Interface)
    ↓
WarpingRepository (Data Access)
    ↓
Oracle Database
```

**Benefits**:
- ✅ Testable (mock repository for unit tests)
- ✅ Transactional (repository controls transactions)
- ✅ Async (all methods async/await)
- ✅ Disposable (proper resource management)
- ✅ Injectable (dependency injection ready)

---

## Testing Recommendations

### Unit Tests Required

```csharp
[TestClass]
public class WarpingRepositoryTests
{
    [TestMethod]
    public async Task InsertPallet_WithTransaction_ShouldUpdateStock()
    {
        // Arrange
        var mockDb = new MockDatabase();
        var repository = new WarpingRepository(mockDb);
        var pallet = new WarpingPallet { PalletNo = "P001", Use = 50 };

        // Act
        var result = await repository.InsertPalletAsync(pallet);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(2, mockDb.ExecutedCommands.Count); // Insert + Update
        Assert.IsTrue(mockDb.TransactionCommitted);
    }

    [TestMethod]
    public async Task InsertPallet_OnFailure_ShouldRollback()
    {
        // Arrange
        var mockDb = new MockDatabaseWithFailure();
        var repository = new WarpingRepository(mockDb);
        var pallet = new WarpingPallet { PalletNo = "P001", Use = 50 };

        // Act & Assert
        await Assert.ThrowsExceptionAsync<Exception>(
            async () => await repository.InsertPalletAsync(pallet)
        );
        Assert.IsTrue(mockDb.TransactionRolledBack);
    }
}
```

### Integration Tests Required

```csharp
[TestClass]
public class WarpingIntegrationTests
{
    [TestMethod]
    public async Task CompleteWarpingWorkflow_ShouldSucceed()
    {
        // Arrange - Create test data
        var itemCode = "TEST001";
        var mcNo = "WP01";

        // Act - Execute full workflow
        var spec = await _service.GetSpecificationAsync(itemCode, mcNo);
        var beamLot = await _service.StartBeamAsync(spec);
        await _service.AllocatePalletsAsync(beamLot, pallets);
        await _service.UpdateQualityAsync(beamLot, qualityData);
        await _service.DoffBeamAsync(beamLot);
        await _service.FinishBeamAsync(beamLot);

        // Assert - Verify data integrity
        var beam = await _repository.GetBeamAsync(beamLot);
        Assert.IsNotNull(beam);
        Assert.AreEqual("FINISHED", beam.Status);

        // Verify stock updated
        var stock = await _yarnRepository.GetStockAsync(pallets[0].PalletNo);
        Assert.AreEqual(expectedRemaining, stock.Quantity);
    }
}
```

---

## Maintenance Notes

### For Future Developers

**Key Patterns**:
1. All procedures follow naming: `WARP_[OPERATION][ENTITY]`
2. All procedures use parameter prefix: `P_`
3. Return cursors for SELECT operations
4. Return affected row count for DML operations

**Common Gotchas**:
- ⚠️ `WARP_INSERTPALLETS` updates stock - must use transaction
- ⚠️ `WARP_UPDATEBEAMQUALITY` allows partial updates (null parameters skipped)
- ⚠️ `WARP_DOFFBEAM` generates sequential doff numbers - not thread-safe
- ⚠️ Error codes -20001 to -20026 reserved for warping module

**Deployment Checklist**:
- [ ] Backup all `tblWarping*` tables before procedure changes
- [ ] Test transaction rollback scenarios
- [ ] Verify indexes exist before deploying optimized queries
- [ ] Update DataService.cs to match procedure signature changes
- [ ] Run integration tests against staging database
- [ ] Document any breaking changes in CHANGELOG.md

---

## Summary Statistics

| Metric | Value |
|--------|-------|
| Total Procedures | 26 |
| Analyzed Procedures | 3 (12%) |
| Pending Procedures | 23 (88%) |
| Critical Issues | 3 |
| Medium Issues | 2 |
| Low Issues | 1 |
| Tables Accessed | 9 |
| Primary Tables | 4 |
| Reference Tables | 5 |
| Shared Tables | 4 |
| Estimated Refactoring Effort | 4-6 weeks |

---

## Document Status

**Version**: 1.0 (Example)
**Created**: 2025-10-12
**Last Updated**: 2025-10-12
**Completion**: 12% (3/26 procedures analyzed)
**Next Session**: Analyze remaining 23 procedures

**Progress Tracker**:
- [x] WARP_GETSPECBYCHOPNOANDMC ✅
- [x] WARP_INSERTPALLETS ✅
- [x] WARP_UPDATEBEAMQUALITY ✅
- [ ] WARP_GETWARPERLOTBYHEADNO
- [ ] WARP_INSERTBEAMSTART
- [ ] WARP_UPDATEBEAMDOFFING
- [ ] ... (20 more procedures)

---

**⚠️ IMPORTANT**: This is an EXAMPLE document showing the format and level of detail that will be generated. Actual analysis will be done procedure by procedure with immediate status updates to prevent data loss during long sessions.
