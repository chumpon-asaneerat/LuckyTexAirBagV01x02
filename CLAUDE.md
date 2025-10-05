# LuckyTex AirBag MES - Developer Guide

## Project Overview

**LuckyTex AirBag Manufacturing Execution System (MES)**

A comprehensive production management system for automotive airbag fabric manufacturing, covering the complete production cycle from raw material receipt to finished goods delivery.

**Technology Stack:**
- .NET Framework 4.7.2 (IMPORTANT: No .NET Core - must stay on .NET Framework)
- C# 7.3 (latest officially supported for .NET Framework 4.7.2)
- WPF (Windows Presentation Foundation) with XAML
- Oracle Database (via System.Data.OracleClient)
- SQL Server (for D365 integration)
- NLib Framework (proprietary framework, .NET Framework only)
- Visual Studio 2010 solution format

**Project Scale:**
- 21 production modules
- ~180 XAML pages
- 21 data service classes
- Multiple PLC integrations (Modbus TCP/RTU)
- Extensive Oracle stored procedures

---

## Build Commands

### Build Solution
```bash
# Using MSBuild (Visual Studio 2010 format)
msbuild LuckyTexAirBagV01x02.sln /p:Configuration=Release /p:Platform="Any CPU"

# Or open in Visual Studio and build
# File -> Open -> Project/Solution -> LuckyTexAirBagV01x02.sln
# Build -> Build Solution (Ctrl+Shift+B)
```

### Clean Build
```bash
msbuild LuckyTexAirBagV01x02.sln /t:Clean /p:Configuration=Release
msbuild LuckyTexAirBagV01x02.sln /t:Build /p:Configuration=Release
```

### Run Application
```bash
# After build, executable is in bin/Release/
cd bin/Release
LuckyTexAirBagV01x02.exe
```

---

## Architecture Overview

### High-Level Architecture

The system follows a **layered architecture** with WPF presentation layer, business logic in code-behind, and data access through dedicated service classes.

**Key Architectural Patterns:**

1. **Module-Based Structure**
   - Each production module (01-21) has its own project/namespace
   - Modules represent physical production stages (Warehouse, Warping, Weaving, etc.)
   - Each module contains: XAML pages, data services, and business logic

2. **Data Access Layer**
   - Dedicated service classes (e.g., `WarehouseDataService.cs`)
   - All database operations use Oracle stored procedures
   - Singleton pattern extensively used for service instances
   - SQL Server integration for D365 ERP synchronization

3. **PLC Integration**
   - Serial communication (RS-232) for legacy equipment
   - Modbus TCP/RTU for modern PLCs
   - Real-time data collection from production equipment
   - Custom protocol handling in each module

4. **Report Generation**
   - RDLC report templates (Microsoft Report Viewer)
   - Reports embedded in XAML pages
   - Data binding to Oracle stored procedure results

5. **Navigation Pattern**
   - Main dashboard (M00Dashboard) acts as central hub
   - Module-specific sub-dashboards
   - Hierarchical page navigation within modules

### Critical Dependencies

**Understanding these requires reading multiple files:**

1. **Production Flow Dependencies**
   - Each module depends on upstream data (e.g., Weaving needs Warping data)
   - Traceability chain links all modules: Yarn Lot -> Warp Beam -> Fabric Roll -> Final Product
   - Files to read: `M02Warping/`, `M05Weaving/`, `M06Finishing/` pages

2. **Common Data Entities**
   - `tblMachine`: Equipment master data (used across all modules)
   - `tblEmployee`: Operator/user data (authentication and tracking)
   - `tblShift`: Production shift management (affects all production modules)
   - These tables are referenced in every module's data service

3. **Shared Business Logic**
   - Barcode generation/scanning (common across modules)
   - Quality inspection workflows (multiple modules use same pattern)
   - Material traceability (forward and backward tracking)
   - Files: Search for "GenerateBarcode", "ScanBarcode" patterns across solution

4. **PLC Communication Framework**
   - Common Modbus library (used in modules 2, 5, 6, 8)
   - Serial port handling (legacy modules 3, 4)
   - Real-time data polling and event handling
   - Files: Look for `ModbusClient`, `SerialPort` usage

---

## Modernization Project

[!] **IMPORTANT**: This codebase is undergoing a modernization effort.

### Modernization Goals
- Decouple tightly-coupled code
- Create reusable class libraries
- Optimize performance and maintainability
- **NO UI changes** (XAML and RDLC files remain unchanged)
- **NO code deletion** (create new classes alongside existing code)

### Technology Constraints

**MUST use .NET Framework 4.7.2** (NOT .NET Core, NOT .NET 5+)
- Cannot use .NET Core-specific features (e.g., Span<T>, System.Text.Json, etc.)
- Maximum C# language version: 7.3
- Dependency Injection: Use compatible libraries (Simple Injector, Unity, Autofac)

### .NET Framework 4.7.2 Compatible Patterns

**Dependency Injection Options:**
- Simple Injector (recommended, lightweight)
- Unity Container
- Autofac

**Language Features Available:**
- C# 7.3 (tuples, pattern matching, ref returns, local functions, etc.)
- async/await (fully supported)
- LINQ (fully supported)
- Expression trees
- Task-based asynchronous pattern (TAP)

**NOT Available (These are .NET Core/.NET 5+ only):**
- Span<T> and Memory<T>
- System.Text.Json (use Newtonsoft.Json instead)
- IAsyncEnumerable
- Record types (C# 9+)
- Init-only setters
- Top-level statements

### Modernization Documentation

All modernization documentation is in the `Documents/` folder:

- **START HERE**: `Documents/README_MODERNIZATION.md` - Project overview and navigation
- **QUICK START**: `Documents/MODERNIZATION_QUICKSTART.md` - 10-minute orientation
- **ANALYSIS**: `Documents/MODERNIZATION_ANALYSIS.md` - Codebase inventory and patterns
- **WORKFLOWS**: `Documents/MODERNIZATION_WORKFLOW.md` - Business logic for all 21 modules
- **REFACTORING GUIDE**: `Documents/MODERNIZATION_REFACTORING.md` - 8-phase implementation strategy
- **SESSION TRACKER**: `Documents/MODERNIZATION_SESSION_TRACKER.md` - Progress tracking
- **.NET FRAMEWORK NOTES**: `Documents/DOTNET_FRAMEWORK_4.7.2_NOTES.md` - Compatibility reference

---

## Key Concepts for Context

When working on this codebase, understanding these concepts helps maintain context across multiple sessions:

### 1. Production Workflow Chain
The 21 modules form a manufacturing pipeline. Changes in one module often affect downstream modules. Always consider traceability requirements when modifying data structures.

### 2. Singleton Services
Most data services use singleton pattern. When refactoring, plan for dependency injection migration to maintain thread safety and testability.

### 3. Stored Procedure Dependency
All database operations use Oracle stored procedures. Never bypass this pattern - it's critical for database security and performance.

### 4. PLC Integration Patterns
Real-time data collection from PLCs requires careful threading and error handling. Module 2 (Warping), Module 5 (Weaving), and Module 6 (Finishing) have the most complex PLC integrations.

### 5. Report Template Binding
RDLC reports are tightly bound to data structures. When refactoring data access layer, ensure report data source compatibility.

---

## Development Workflow

### For New Features
1. Identify which module(s) are affected
2. Review corresponding workflow in `Documents/MODERNIZATION_WORKFLOW.md`
3. Check data service class for existing stored procedures
4. Follow established patterns (singleton services, XAML data binding)
5. Test thoroughly - production system with zero-downtime requirement

### For Refactoring
1. Read `Documents/MODERNIZATION_QUICKSTART.md` first
2. Choose a module to refactor (recommend starting with Module 1 - Warehouse)
3. Follow phase-by-phase strategy in `Documents/MODERNIZATION_REFACTORING.md`
4. Create new classes in parallel - DO NOT modify existing code
5. Update `Documents/MODERNIZATION_SESSION_TRACKER.md` after each session

### For Bug Fixes
1. Identify the module and page
2. Check corresponding data service class
3. Review stored procedure logic (Oracle database)
4. Test in development environment with real production scenarios
5. Consider impact on upstream/downstream modules

---

## Common Pitfalls

1. **Don't assume .NET Core compatibility** - Always verify features are available in .NET Framework 4.7.2
2. **Don't modify XAML layouts** - UI changes are out of scope for modernization
3. **Don't delete old code** - Create new alongside existing until fully tested
4. **Don't bypass stored procedures** - Direct SQL queries are not allowed
5. **Don't ignore traceability** - Every production operation must maintain forward/backward traceability

---

## Quick Reference

### Find a Feature
- **Barcode scanning**: Search for `ScanBarcode` across solution
- **PLC communication**: Look in M02Warping, M05Weaving, M06Finishing modules
- **Quality inspection**: M08Inspection module
- **Material receiving**: M01Warehouse module
- **Production reporting**: Each module has `Reports/` subfolder

### Common File Locations
- **Data Services**: `M##ModuleName/Services/*DataService.cs`
- **XAML Pages**: `M##ModuleName/Pages/*.xaml`
- **Reports**: `M##ModuleName/Reports/*.rdlc`
- **Common utilities**: `Common/` folder (if exists)

### Database Schema
- **Connection strings**: App.config
- **Stored procedures**: Prefix `sp_LuckyTex_*`
- **Main tables**: `tblMachine`, `tblEmployee`, `tblShift`, `tblProduct*`

---

## Session Continuity

For multi-session work (especially modernization):

1. **Always read** `Documents/MODERNIZATION_SESSION_TRACKER.md` first
2. **Update session log** with what you accomplished
3. **Mark completed tasks** in the tracker
4. **Note any blockers** for the next session
5. **Update "NEXT SESSION TASKS"** before ending

This ensures seamless continuation across context resets.

---

## Additional Resources

- Visual Studio 2010+ required for development
- Oracle Client installation required
- .NET Framework 4.7.2 SDK
- Microsoft Report Viewer runtime

For detailed modernization planning, always start with `Documents/README_MODERNIZATION.md`.
