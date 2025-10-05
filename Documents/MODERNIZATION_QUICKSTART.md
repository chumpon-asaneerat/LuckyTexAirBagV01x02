# LuckyTex AirBag MES - Quick Start Guide

**Estimated reading time: 10 minutes**

---

## What Is This Project?

A **modernization/refactoring project** for a large WPF manufacturing execution system:

- **Technology**: .NET Framework 4.7.2 (WPF + Oracle + PLC integration)
- **Scale**: 21 modules, ~180 pages, extensive business logic
- **Goal**: Decouple code, eliminate duplication, improve testability
- **Constraint**: NO UI changes, NO code deletion, .NET Framework 4.7.2 ONLY

---

## Critical Constraints

### [!] .NET Framework 4.7.2 ONLY

- **NO .NET Core** - Must stay on .NET Framework
- **NO .NET 5+** features
- **C# 7.3 maximum**
- See `DOTNET_FRAMEWORK_4.7.2_NOTES.md` for full compatibility list

### [!] Non-Destructive Refactoring

- Create **new classes alongside existing code**
- **Do not delete** old code until new code is proven
- **Do not modify** XAML files (UI stays the same)
- **Do not modify** RDLC files (reports stay the same)
- **Do not modify** database schema or stored procedures

---

## Current Problems

### High Code Duplication (60-80%)
- Same barcode scanning logic in 45+ pages
- Same CRUD operations in 85+ pages
- Same production entry logic in 35+ pages
- Same report generation in 40+ pages
- Same PLC communication in 12+ pages

### Zero Testability
- All data services use singleton pattern
- Business logic mixed with UI code
- No dependency injection
- Cannot mock dependencies

### Tight Coupling
- Code-behind calls DataService.Instance directly
- Business logic scattered across UI layer
- No separation of concerns

---

## Solution Approach

### Layered Architecture

**Current** (2 layers):
```
[UI + Business Logic] → [Data Services (Singleton)] → [Database]
```

**Target** (4 layers):
```
[UI (thin)] → [Service Layer (business)] → [Repository Layer (data)] → [Database]
```

### Key Patterns to Implement

1. **Dependency Injection** - Simple Injector container
2. **Repository Pattern** - Abstract data access
3. **Service Layer** - Business logic separation
4. **Base Page Classes** - Eliminate UI duplication
5. **Validation Framework** - FluentValidation
6. **Logging** - NLog or Serilog

---

## 8-Phase Strategy (24 weeks)

### Phase 1: Foundation (Week 1-2)
- Create core library project
- Set up DI container
- Add logging framework
- Define folder structure

### Phase 2: Data Access (Week 3-6)
- Create entity models (replace DataTable)
- Create repository interfaces
- Implement repository classes
- Start with low-risk modules (M01, M17, M20)

### Phase 3: Service Layer (Week 7-12)
- Create service interfaces
- Implement validation framework
- Extract business logic from code-behind
- Migrate module by module

### Phase 4: Base Pages (Week 13-15)
- Create base page classes (Crud, Production, Barcode, Report, PLC)
- Refactor pages to inherit from base classes
- Reduce code duplication by 60-70%

### Phase 5-8: PLC/Reports/Testing/Deployment (Week 16-24)
- Standardize PLC communication
- Standardize report generation
- Write comprehensive tests
- Deploy gradually (low to high risk)

---

## Module Refactoring Priority

### Start Here (Low Risk):
1. **M01 - Warehouse** (simple CRUD)
2. **M17 - Master Data** (reference data)
3. **M20 - User Management** (isolated)

### Then (Medium Risk):
4. M03 - Beaming
5. M04 - Drawing
6. M02 - Warping (has PLC)

### Later (High Risk):
7. M05 - Weaving (complex + production-critical)
8. M06 - Finishing (complex + production-critical)
9. M08 - Inspection (complex business rules)

### Last (System-Wide):
10. M00 - Dashboard (depends on all modules)
11. M21 - System Config (system-wide impact)

---

## DO and DON'T

### DO:
- [x] Create new classes in separate library project
- [x] Use dependency injection for all new code
- [x] Write unit tests for new services and repositories
- [x] Keep original XAML files unchanged
- [x] Keep original RDLC templates unchanged
- [x] Add comprehensive logging
- [x] Follow module priority order (low to high risk)
- [x] Test thoroughly before moving to next module
- [x] Update `MODERNIZATION_SESSION_TRACKER.md` after every session
- [x] Commit frequently to git

### DON'T:
- [ ] Delete or modify existing working code
- [ ] Use .NET Core-specific features
- [ ] Modify database schema
- [ ] Change stored procedures
- [ ] Modify XAML layouts or controls
- [ ] Modify RDLC report definitions
- [ ] Skip testing
- [ ] Rewrite everything at once
- [ ] Work on high-risk modules first

---

## Key Concepts

### 1. Repository Pattern

**Purpose**: Abstract data access, make it testable

**Structure**:
```
IWarehouseRepository (interface)
  ↓ implements
WarehouseRepository (concrete class)
  ↓ uses
Oracle Stored Procedures
```

**Benefits**:
- Testable (can mock IWarehouseRepository)
- Consistent data access patterns
- Single responsibility

### 2. Service Layer

**Purpose**: Extract business logic from UI

**Structure**:
```
IWarehouseService (interface)
  ↓ implements
WarehouseService (concrete class)
  ↓ uses
IWarehouseRepository + Validators + Logging
```

**Benefits**:
- Business logic testable
- Reusable across multiple pages
- Clear separation from UI

### 3. Base Page Classes

**Purpose**: Eliminate code duplication in UI

**Classes**:
- `BasePage` - Common functionality for all pages
- `CrudPageBase<T>` - For 85+ CRUD pages
- `ProductionEntryPageBase<T>` - For 35+ production pages
- `BarcodePageBase` - For 45+ barcode scanning pages
- `ReportPageBase` - For 40+ report pages
- `PLCMonitorPageBase` - For 12+ PLC monitoring pages

**Benefits**:
- Reduces duplication by 60-70%
- Consistent behavior across pages
- Easier maintenance

### 4. Dependency Injection

**Purpose**: Decouple dependencies, enable testing

**Pattern**:
```
Page constructor:
  _service = App.GetInstance<IWarehouseService>();

App.xaml.cs ConfigureServices:
  container.Register<IWarehouseService, WarehouseService>();
  container.Register<IWarehouseRepository, WarehouseRepository>();
```

**Benefits**:
- Loose coupling
- Testable (inject mocks)
- Centralized configuration

---

## Common Duplication Patterns

### Pattern A: Barcode Scanning (45+ pages)

**Current Code** (duplicated in each page):
- TextBox KeyDown event handler
- Check for Enter key
- Trim and validate barcode
- Call service.GetByBarcode
- Display results or error
- Clear and refocus textbox

**Solution**: `BarcodePageBase` class handles all common logic

### Pattern B: CRUD Operations (85+ pages)

**Current Code** (duplicated in each page):
- Add button click → validation → insert → refresh
- Edit button click → check selection → update → refresh
- Delete button click → confirm → delete → refresh
- Refresh button click → reload data

**Solution**: `CrudPageBase<T>` class handles all button events

### Pattern C: Production Entry (35+ pages)

**Current Code** (duplicated in each page):
- Select machine combo box
- Select operator combo box
- Start button → insert production record
- Stop button → update status
- Complete button → calculate quantities

**Solution**: `ProductionEntryPageBase<T>` class handles workflow

---

## Document Navigation

### Quick Reference (This document)
**When to use**: First session, quick refresher

### Complete Workflow Understanding
**Document**: `MODERNIZATION_WORKFLOW.md`
**When to use**: Need to understand business logic for a specific module

### Pattern Analysis
**Document**: `MODERNIZATION_ANALYSIS.md`
**When to use**: Need to understand current code patterns and duplication

### Implementation Strategy
**Document**: `MODERNIZATION_REFACTORING.md`
**When to use**: Ready to implement, need detailed phase breakdown

### Compatibility Reference
**Document**: `DOTNET_FRAMEWORK_4.7.2_NOTES.md`
**When to use**: Unsure if a feature/library is compatible

### Progress Tracking
**Document**: `MODERNIZATION_SESSION_TRACKER.md`
**When to use**: Every session (track progress, note blockers, plan next session)

### Project Overview
**Document**: `README_MODERNIZATION.md`
**When to use**: Need overall project context, architecture diagrams

---

## Session Workflow

### Starting a New Session

1. Open `MODERNIZATION_SESSION_TRACKER.md`
2. Review "LAST SESSION" notes
3. Check "NEXT SESSION TASKS"
4. Review overall progress table
5. Start working on next task

### During Session

1. Complete tasks one by one
2. Update tracker as you complete tasks
3. Note any blockers or decisions
4. Commit code frequently
5. Run tests before moving to next task

### Ending Session

1. Update "LAST SESSION" section in tracker
2. Update "NEXT SESSION TASKS"
3. Mark completed modules/phases
4. Note any blockers for next session
5. Commit all changes to git

---

## Success Criteria

### Code Quality
- [x] Code duplication reduced to <20%
- [x] Unit test coverage >80%
- [x] All singletons replaced with DI
- [x] All business logic in service layer

### Performance
- [x] Response time equals or better than current
- [x] Memory usage equals or better than current
- [x] PLC communication latency <100ms

### Functionality
- [x] All existing features working
- [x] 100% backward compatibility
- [x] XAML files unchanged
- [x] RDLC files unchanged
- [x] No database schema changes

### Maintainability
- [x] Clear separation of concerns (UI/Business/Data)
- [x] Consistent patterns across all modules
- [x] Comprehensive logging for troubleshooting
- [x] Updated documentation

---

## Common Pitfalls to Avoid

### Pitfall 1: Using .NET Core Features
**Problem**: Features like Span<T>, System.Text.Json, IAsyncEnumerable don't exist in .NET Framework 4.7.2
**Solution**: Always check `DOTNET_FRAMEWORK_4.7.2_NOTES.md` before using new features

### Pitfall 2: Modifying Existing Code
**Problem**: Breaks working functionality, hard to rollback
**Solution**: Always create new classes alongside old code, don't modify existing

### Pitfall 3: Skipping Tests
**Problem**: Can't verify new code works correctly
**Solution**: Write unit tests for all services and repositories before integration

### Pitfall 4: Working on High-Risk Modules First
**Problem**: Production downtime if issues occur
**Solution**: Follow priority order - start with M01, M17, M20 (low risk)

### Pitfall 5: Not Updating Session Tracker
**Problem**: Lose context when session ends, forget what was done
**Solution**: Update tracker throughout session, especially at the end

---

## Technology Stack Summary

### Staying the Same
- .NET Framework 4.7.2 (WPF)
- Oracle Database
- Stored procedures (all data operations)
- XAML UI
- RDLC reports

### Adding for Modernization
- Simple Injector 4.x (DI container)
- FluentValidation (validation)
- NLog or Serilog (logging)
- AutoMapper 10.x (object mapping)
- NUnit or xUnit (testing)

### Not Using (Incompatible)
- .NET Core
- .NET 5+
- System.Text.Json
- Entity Framework Core
- Any .NET Standard 2.1+ exclusive libraries

---

## Next Steps

1. **Finish reading this document** (you're almost done!)
2. **Read `README_MODERNIZATION.md`** for architecture overview
3. **Skim `MODERNIZATION_WORKFLOW.md`** to understand business logic
4. **Read `MODERNIZATION_REFACTORING.md` Phase 1** for implementation details
5. **Open `MODERNIZATION_SESSION_TRACKER.md`** to track your work

**You're now ready to start modernization!**

---

**Document Version**: 1.0
**Last Updated**: 2025-10-05
**Estimated Time to Competency**: 1-2 hours of reading
