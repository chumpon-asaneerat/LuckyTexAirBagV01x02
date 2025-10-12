# Database Documentation

**Last Updated**: 2025-10-12

This directory contains database-related documentation for the LuckyTex AirBag MES system.

## Files

### `.DATABASE_STORED_PROCEDURES_TODO.md` (Main TODO Checklist)

Comprehensive checklist of all 269+ unique stored procedures extracted from the codebase.

**Structure**:
- Summary statistics
- Procedures grouped by prefix/module (BEAM_, WARP_, WEAV_, etc.)
- Each procedure listed with its DataService file(s)
- Documentation guidelines
- Phased implementation plan

**Key Metrics**:
- **Total Unique Stored Procedures**: 269
- **Total Checklist Items**: 320+ (some procedures used in multiple services)
- **Total DataService Files**: 21
- **Completion Progress**: 0/269 (0%)

### `.DATABASE_ANALYSIS_TRACKER.md`

Tracking file for database analysis tasks (if exists).

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

## How to Use This Checklist

1. **Open** `.DATABASE_STORED_PROCEDURES_TODO.md`
2. **Select** a procedure to document
3. **Mark as in progress** by changing `[ ]` to `[~]` (optional)
4. **Document** the procedure following the guidelines
5. **Mark as complete** by changing `[ ]` to `[x]`
6. **Update** the completion progress counter

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
