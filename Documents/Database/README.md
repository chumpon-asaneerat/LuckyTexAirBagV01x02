# Database Documentation

**Last Updated**: 2025-10-12

This directory contains database-related documentation for the LuckyTex AirBag MES system.

## Essential Files (3 Files Only)

### 1. `.DATABASE_ANALYSIS_INSTRUCTIONS.md` ⭐ **START HERE**

**Purpose**: Complete workflow instructions for documenting stored procedures

**Contains**:
- Sequential work order (M02 → M03 → ... → Lab)
- Step-by-step procedure documentation workflow
- Incremental tracking rules (update after EACH document)
- Session resumption instructions
- Quick reference commands
- Common mistakes to avoid

**When to Read**: At the start of EVERY documentation session

---

### 2. `.DATABASE_STORED_PROCEDURES_TODO.md` (Progress Tracker)

**Purpose**: Master checklist tracking all 296 stored procedures

**Contains**:
- Summary statistics and current progress (X/296, Y.Y%)
- All procedures organized by module (M00-M21 + Lab System)
- Checkbox status for each procedure [ ] or [x]
- DataService file references

**When to Read**:
- At session start (to find next procedure)
- After each document (to update progress)

**Current Progress**: 26/296 (8.8%) - Updated incrementally after each document

---

### 3. `TEMPLATE_INDIVIDUAL_PROCEDURE.md` (Documentation Format)

**Purpose**: Standardized template for procedure documentation

**Required Sections**:
- Procedure header (number, module, status)
- Quick Reference table
- Parameters (IN/OUT/Returns)
- Database Operations (tables, indexes)
- Business Logic (what and WHY)
- Related Procedures (upstream/downstream)
- Code Location (C# + estimated SQL)

**When to Use**: As reference when creating each new procedure document

## Procedure Categories

### Production Modules (Core Operations)

1. **BEAM_** (20 procedures) - Beaming operations
2. **WARP_** (26 procedures) - Warping operations  
3. **WEAV_** (24 procedures) - Weaving operations
4. **WEAVE_** (9 procedures) - Weaving setup/process
5. **DRAW_** (7 procedures) - Drawing operations
6. **FINISHING_** (37 procedures) - Finishing operations (Coating/Scouring/Dryer)

### ERP Integration

7. **D365_BM_** (7 procedures) - Beaming D365 integration
8. **D365_CP_** (7 procedures) - Cutting/Printing D365 integration
9. **D365_DT_** (7 procedures) - Drawing D365 integration
10. **D365_FN_** (7 procedures) - Finishing D365 integration
11. **D365_GR_** (7 procedures) - Greige/Weaving D365 integration
12. **D365_IN_** (8 procedures) - Inspection D365 integration
13. **D365_PK_** (10 procedures) - Packing D365 integration
14. **D365_WP_** (7 procedures) - Warping D365 integration

### Quality & Inspection

15. **INS_** (29 procedures) - Inspection operations
16. **LAB_** (22 procedures) - Laboratory/testing operations
17. **QA_** (3 procedures) - Quality assurance operations
18. **DEFECT_** (3 procedures) - Defect management

### Warehouse & Materials

19. **G3_** (12 procedures) - G3 warehouse operations
20. **ITM_** (8 procedures) - Item/material master

### Packaging & Shipping

21. **PACK_** (11 procedures) - Packing operations
22. **PCKPRFTP_** (7 procedures) - AS400 FTP integration
23. **PCKPRFTP_D365_** (7 procedures) - D365 FTP integration
24. **CUT_** (7 procedures) - Cutting & printing
25. **FG_** (2 procedures) - Finished goods

### Master Data & Common

26. **CONDITION_** (6 procedures) - Process conditions
27. **MASTER_** (6 procedures) - Master data management
28. **Shared/Common** (14+ procedures) - Shared operations

## D365 Integration Patterns

The D365_ procedures follow a consistent pattern:

- **BPO** - Backflush Production Order
- **ISH** - Issue Header
- **ISL** - Issue Line
- **OPH** - Output Header (planned)
- **OPL** - Output Line (planned)
- **OUH** - Output Header (actual)
- **OUL** - Output Line (actual)

Each production module has this set of 7 procedures for ERP integration.

## DataService Files

The procedures are implemented across 21 DataService files:

1. BeamingDataService.cs (20+ procedures)
2. WarpingDataService.cs (26+ procedures)
3. WeavingDataService.cs (33+ procedures)
4. CoatingDataService.cs (37+ procedures)
5. DrawingDataService.cs (7+ procedures)
6. FinishingDataService.cs (6+ procedures)
7. D365DataService.cs (57+ procedures)
8. G3DataService.cs (13+ procedures)
9. LABDataService.cs (22+ procedures)
10. PackingDataService.cs (11+ procedures)
11. PCKPRFTPDataService.cs (14+ procedures)
12. CutPrintDataService.cs (7+ procedures)
13. ProcessConditionDataService.cs (6+ procedures)
14. HundredMDataService.cs (2+ procedures)
15. DefectCodeService.cs (3+ procedures)
16. CustomerAndLoadingTypeDataService.cs (4+ procedures)
17. UserDataService.cs (2+ procedures)
18. ItemCodeService.cs (2+ procedures)
19. QualityAssuranceDataService.cs (3+ procedures)
20. BCSPRFTPDataService.cs (2+ procedures)
21. DataService.cs (29+ legacy procedures)

## Documentation Workflow

### Phase 1: High-Priority Production Procedures
- WARP_* (Warping - 26 procedures)
- BEAM_* (Beaming - 20 procedures)
- WEAV_* + WEAVE_* (Weaving - 33 procedures)
- FINISHING_* (Finishing - 37 procedures)

### Phase 2: D365 Integration
- D365_* (All D365 procedures - 60+ procedures)

### Phase 3: Quality & Inspection
- INS_* (Inspection - 29 procedures)
- LAB_* (Laboratory - 22 procedures)
- QA_* (Quality Assurance - 3 procedures)

### Phase 4: Warehouse & Materials
- G3_* (Warehouse - 12 procedures)
- ITM_* (Item Master - 8 procedures)

### Phase 5: Packing & Shipping
- PACK_* (Packing - 11 procedures)
- PCKPRFTP_* (FTP Integration - 14 procedures)

### Phase 6: Master Data & Shared
- MASTER_* (Master Data - 6 procedures)
- CONDITION_* (Conditions - 6 procedures)
- Shared procedures (14+ procedures)

## How to Use - Quick Start

### For Documentation Sessions

**ALWAYS start by reading these 2 files**:
```
1. Read .DATABASE_ANALYSIS_INSTRUCTIONS.md (complete workflow)
2. Read .DATABASE_STORED_PROCEDURES_TODO.md (find next procedure)
```

### Work Cycle (One Procedure at a Time)

**For EACH procedure**:
1. ✅ **Generate** documentation file (use TEMPLATE_INDIVIDUAL_PROCEDURE.md)
2. ✅ **Update** tracker checkbox `[ ]` → `[x]`
3. ✅ **Update** progress counter `X/296` → `(X+1)/296`
4. ✅ **Move** to next procedure

**CRITICAL**: Update tracker after EACH document, never batch!

### Why Incremental Updates?

- ✅ Session interruptions don't lose progress
- ✅ Always know exact completion status
- ✅ Seamless continuation across sessions
- ✅ Real-time progress visibility

See `.DATABASE_ANALYSIS_INSTRUCTIONS.md` for complete details.

## Documentation Template

For each procedure, document:

1. Procedure Name
2. Module/Category
3. Purpose (business function)
4. Parameters (input/output)
5. Returns (result set structure)
6. Business Logic (key operations)
7. Called By (DataService methods)
8. Database Tables (accessed)
9. Transaction Scope
10. Error Handling
11. Performance Notes
12. Dependencies

## Notes

- Some procedures are called from multiple DataService files (cross-module operations)
- The legacy DataService.cs file contains many procedures that should be refactored
- D365 integration procedures follow a consistent naming pattern
- FINISHING_ procedures are split across CoatingDataService and FinishingDataService

---

**Status**: Initial checklist created
**Next Action**: Begin Phase 1 documentation (production procedures)
