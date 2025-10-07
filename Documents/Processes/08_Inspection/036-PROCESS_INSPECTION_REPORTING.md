# Process: Inspection Reporting

**Process ID**: INS-002
**Module**: 08 - Inspection
**Priority**: P3 (Production Module)
**Created**: 2025-10-06

---

## 1. Process Overview

### Purpose
Generate inspection reports for quality analysis, defect trending, inspector performance, and compliance documentation. Provide management with visibility into quality metrics and continuous improvement opportunities.

### Scope
- Daily inspection summary report
- Defect analysis report (by type, frequency, trend)
- Quality grade distribution report
- Inspector performance report
- Rejection analysis report
- Inspection certificate generation

### Module(s) Involved
- **Primary**: M08 - Inspection
- **Data Sources**: tblInspection, tblInspectionDefect, tblFabricRoll, tblFinishedRoll, tblEmployee

---

## 2. UI Files Inventory

### XAML Files
| File Path | Description | Purpose |
|-----------|-------------|---------|
| `LuckyTex.AirBag.Pages/Pages/08 - Inspection/Reports/InspectionSummaryReport.xaml` | Daily summary report | RDLC viewer |
| `LuckyTex.AirBag.Pages/Pages/08 - Inspection/Reports/DefectAnalysisReport.xaml` | Defect analysis | RDLC viewer |
| `LuckyTex.AirBag.Pages/Pages/08 - Inspection/Reports/GradeDistributionReport.xaml` | Grade distribution | RDLC viewer |
| `LuckyTex.AirBag.Pages/Pages/08 - Inspection/Reports/RejectionAnalysisReport.xaml` | Rejection analysis | RDLC viewer |
| `LuckyTex.AirBag.Pages/Pages/08 - Inspection/InspectionMenuPage.xaml` | Module menu | Navigation |

### Report Template Files (RDLC)
| File Path | Description |
|-----------|-------------|
| `LuckyTex.AirBag.Pages/Reports/Inspection/InspectionSummary.rdlc` | Summary template |
| `LuckyTex.AirBag.Pages/Reports/Inspection/DefectAnalysis.rdlc` | Defect analysis template |
| `LuckyTex.AirBag.Pages/Reports/Inspection/GradeDistribution.rdlc` | Grade distribution template |
| `LuckyTex.AirBag.Pages/Reports/Inspection/RejectionAnalysis.rdlc` | Rejection analysis template |
| `LuckyTex.AirBag.Pages/Reports/Inspection/InspectionCertificate.rdlc` | Individual certificate template |

### Service Files
| File Path | Description |
|-----------|-------------|
| *(To be created)* `LuckyTex.AirBag.Core/Repositories/InspectionRepository.cs` | Repository |
| *(To be created)* `LuckyTex.AirBag.Core/Services/InspectionService.cs` | Service layer |
| *(To be created)* `LuckyTex.AirBag.Core/Services/ReportingService.cs` | Report data aggregation |

---

## 3. UI Layout Description

### InspectionSummaryReport.xaml

**Parameter Section**:
- Date range (From/To)
- Roll type filter (All, Grey, Finished)
- Product filter (optional)
- Inspector filter (optional)
- `cmdGenerate`

**Report Viewer**:
- Summary totals (rolls inspected, approved, rejected)
- Grade distribution (A/B/C/Reject counts and %)
- Average defect points per roll
- Inspection breakdown by product
- Inspection breakdown by inspector

### DefectAnalysisReport.xaml

**Parameter Section**:
- Date range
- Defect type filter (All or specific type)
- Severity filter (All, Minor, Major, Critical)
- `cmdGenerate`

**Report Viewer**:
- Total defects by type
- Defect frequency (defects per 100 meters)
- Defect trend chart (over time)
- Top 10 defect types
- Defects by product

### GradeDistributionReport.xaml

**Parameter Section**:
- Date range
- Product filter
- `cmdGenerate`

**Report Viewer**:
- Grade distribution pie chart (A/B/C/Reject)
- Grade percentages
- Grade trend over time
- Grade comparison by product

### RejectionAnalysisReport.xaml

**Parameter Section**:
- Date range
- Rejection action filter (All, Rework, Scrap)
- `cmdGenerate`

**Report Viewer**:
- Total rejections count
- Rejection rate %
- Rejection reasons (defect types)
- Rejection cost analysis (estimated scrap/rework cost)
- Rejection trend

---

## 4. Component Architecture Diagram

```mermaid
graph TD
    UI1[InspectionSummaryReport.xaml] --> CB1[CodeBehind]
    UI2[DefectAnalysisReport.xaml] --> CB2[CodeBehind]
    UI3[GradeDistributionReport.xaml] --> CB3[CodeBehind]
    UI4[RejectionAnalysisReport.xaml] --> CB4[CodeBehind]

    CB1 --> RPT_SVC[IReportingService]
    CB2 --> RPT_SVC
    CB3 --> RPT_SVC
    CB4 --> RPT_SVC

    RPT_SVC --> INS_SVC[IInspectionService]
    RPT_SVC --> REPO[IInspectionRepository]

    REPO --> DB[(Oracle Database)]

    DB --> SP1[sp_LuckyTex_Report_InspectionSummary]
    DB --> SP2[sp_LuckyTex_Report_DefectAnalysis]
    DB --> SP3[sp_LuckyTex_Report_GradeDistribution]
    DB --> SP4[sp_LuckyTex_Report_RejectionAnalysis]

    CB1 --> RDLC1[InspectionSummary.rdlc]
    CB2 --> RDLC2[DefectAnalysis.rdlc]
    CB3 --> RDLC3[GradeDistribution.rdlc]
    CB4 --> RDLC4[RejectionAnalysis.rdlc]

    style UI1 fill:#e1f5ff
    style UI2 fill:#e1f5ff
    style UI3 fill:#e1f5ff
    style UI4 fill:#e1f5ff
    style RPT_SVC fill:#e1ffe1
    style REPO fill:#fff4e1
    style DB fill:#ffe1e1
```

---

## 5. Workflow Diagram

```mermaid
graph TD
    START[Start: Inspection Reporting] --> SELECT{Select Report Type}

    SELECT -->|Inspection Summary| PARAMS1[Enter Parameters:<br/>Date Range, Roll Type]
    SELECT -->|Defect Analysis| PARAMS2[Enter Parameters:<br/>Date Range, Defect Type]
    SELECT -->|Grade Distribution| PARAMS3[Enter Parameters:<br/>Date Range, Product]
    SELECT -->|Rejection Analysis| PARAMS4[Enter Parameters:<br/>Date Range, Action]

    PARAMS1 --> VALIDATE{Valid?}
    PARAMS2 --> VALIDATE
    PARAMS3 --> VALIDATE
    PARAMS4 --> VALIDATE

    VALIDATE -->|No| ERROR[Error: Invalid Parameters]
    ERROR --> END[End]

    VALIDATE -->|Yes| QUERY[Execute Report Stored Procedure]
    QUERY --> BIND[Bind Data to RDLC Template]
    BIND --> DISPLAY[Display Report in Viewer]

    DISPLAY --> EXPORT{Export?}
    EXPORT -->|PDF| PDF[Generate PDF]
    EXPORT -->|Excel| EXCEL[Generate Excel]
    EXPORT -->|Print| PRINT[Print Report]
    EXPORT -->|Close| END

    PDF --> END
    EXCEL --> END
    PRINT --> END

    style START fill:#e1f5ff
    style END fill:#e1f5ff
    style ERROR fill:#ffe1e1
    style DISPLAY fill:#e1ffe1
```

---

## 6. Business Logic Sequence Diagram

```mermaid
sequenceDiagram
    participant User
    participant UI as Report Page
    participant RPT_SVC as ReportingService
    participant REPO as InspectionRepository
    participant DB as Oracle Database
    participant RDLC as RDLC Engine

    User->>UI: Select report type
    User->>UI: Enter parameters (date range, filters)
    User->>UI: Click Generate

    UI->>RPT_SVC: GetReportData(reportType, parameters)
    RPT_SVC->>RPT_SVC: Validate parameters

    alt Inspection Summary Report
        RPT_SVC->>REPO: GetInspectionSummary(dateFrom, dateTo, rollType, product, inspector)
        REPO->>DB: sp_LuckyTex_Report_InspectionSummary
        Note over DB: SELECT inspections<br/>JOIN with rolls, employees<br/>GROUP BY product, inspector, grade<br/>CALCULATE totals, percentages
        DB-->>REPO: Summary dataset
        REPO-->>RPT_SVC: Report data
        RPT_SVC-->>UI: Dataset

        UI->>RDLC: Load InspectionSummary.rdlc
        UI->>RDLC: Bind data
        RDLC-->>UI: Rendered report

    else Defect Analysis Report
        RPT_SVC->>REPO: GetDefectAnalysis(dateFrom, dateTo, defectType, severity)
        REPO->>DB: sp_LuckyTex_Report_DefectAnalysis
        Note over DB: SELECT defects<br/>GROUP BY type, severity<br/>CALCULATE frequency, trends
        DB-->>REPO: Defect dataset
        REPO-->>RPT_SVC: Report data
        RPT_SVC-->>UI: Dataset

        UI->>RDLC: Load DefectAnalysis.rdlc
        UI->>RDLC: Bind data
        RDLC-->>UI: Rendered report

    else Grade Distribution Report
        RPT_SVC->>REPO: GetGradeDistribution(dateFrom, dateTo, product)
        REPO->>DB: sp_LuckyTex_Report_GradeDistribution
        Note over DB: SELECT inspections<br/>GROUP BY grade<br/>CALCULATE counts, percentages
        DB-->>REPO: Grade dataset
        REPO-->>RPT_SVC: Report data
        RPT_SVC-->>UI: Dataset

        UI->>RDLC: Load GradeDistribution.rdlc
        UI->>RDLC: Bind data with pie chart
        RDLC-->>UI: Rendered report

    else Rejection Analysis Report
        RPT_SVC->>REPO: GetRejectionAnalysis(dateFrom, dateTo, action)
        REPO->>DB: sp_LuckyTex_Report_RejectionAnalysis
        Note over DB: SELECT rejected inspections<br/>JOIN with defects<br/>GROUP BY action, defect type<br/>CALCULATE rejection rate, costs
        DB-->>REPO: Rejection dataset
        REPO-->>RPT_SVC: Report data
        RPT_SVC-->>UI: Dataset

        UI->>RDLC: Load RejectionAnalysis.rdlc
        UI->>RDLC: Bind data
        RDLC-->>UI: Rendered report
    end

    UI->>User: Display report

    alt Export
        User->>UI: Click Export PDF
        UI->>RDLC: Export to PDF
        RDLC-->>UI: PDF file
        UI->>User: Download PDF
    end
```

---

## 7. Data Flow

### Input Data

| Data Element | Source | Format | Validation |
|--------------|--------|--------|------------|
| Date From | User input | DateTime | <= Date To |
| Date To | User input | DateTime | >= Date From |
| Roll Type Filter | Dropdown | Enum (All, Grey, Finished) | Valid value |
| Product Filter | Dropdown | String | Valid product or "All" |
| Inspector Filter | Dropdown | String | Valid employee or "All" |
| Defect Type Filter | Dropdown | String | Valid defect type or "All" |
| Severity Filter | Dropdown | Enum (All, Minor, Major, Critical) | Valid value |
| Rejection Action Filter | Dropdown | Enum (All, Rework, Scrap) | Valid value |

### Output Data

| Data Element | Destination | Format | Purpose |
|--------------|-------------|--------|---------|
| Inspection Summary Dataset | RDLC Report | Tabular data | Summary metrics |
| Defect Statistics | RDLC Report | Aggregated counts | Defect tracking |
| Grade Distribution Data | RDLC Report | Counts + percentages | Quality visualization |
| Rejection Data | RDLC Report | Rejection details | Cost analysis |
| PDF/Excel Files | File system | Export formats | Distribution |

### Data Transformations

1. **Inspections → Summary Totals**: Aggregate counts by grade, product, inspector
2. **Defects → Defect Rate**: (Defect count / Total meters) × 100
3. **Grade Counts → Percentages**: (Grade count / Total inspections) × 100%
4. **Rejections → Rejection Rate**: (Rejected count / Total inspections) × 100%

---

## 8. Database Operations

### Stored Procedures Used

#### sp_LuckyTex_Report_InspectionSummary
- **Purpose**: Get inspection summary data
- **Parameters**: @DateFrom, @DateTo, @RollType, @ProductCode, @InspectorID
- **Returns**: Inspection totals, grade distribution, breakdowns
- **Tables Read**: tblInspection, tblFabricRoll, tblFinishedRoll, tblEmployee

#### sp_LuckyTex_Report_DefectAnalysis
- **Purpose**: Aggregate defect statistics
- **Parameters**: @DateFrom, @DateTo, @DefectType, @Severity
- **Returns**: Defect counts, frequency, trends
- **Tables Read**: tblInspectionDefect, tblInspection

#### sp_LuckyTex_Report_GradeDistribution
- **Purpose**: Calculate grade distribution
- **Parameters**: @DateFrom, @DateTo, @ProductCode
- **Returns**: Grade counts and percentages
- **Tables Read**: tblInspection

#### sp_LuckyTex_Report_RejectionAnalysis
- **Purpose**: Analyze rejections
- **Parameters**: @DateFrom, @DateTo, @Action
- **Returns**: Rejection counts, reasons, costs
- **Tables Read**: tblInspection, tblInspectionDefect

---

## 9. Implementation Checklist

### Phase 1: Repository Layer
- [ ] Extend `IInspectionRepository`
  - [ ] GetInspectionSummary(params) method
  - [ ] GetDefectAnalysis(params) method
  - [ ] GetGradeDistribution(params) method
  - [ ] GetRejectionAnalysis(params) method
- [ ] Implement in `InspectionRepository`
- [ ] Unit tests

### Phase 2: Service Layer
- [ ] Extend `IReportingService`
  - [ ] GetInspectionSummaryData(params) method
  - [ ] GetDefectAnalysisData(params) method
  - [ ] GetGradeDistributionData(params) method
  - [ ] GetRejectionAnalysisData(params) method
- [ ] Implement in `ReportingService`
- [ ] Unit tests

### Phase 3: RDLC Report Templates
- [ ] Create InspectionSummary.rdlc
  - [ ] Summary section with totals
  - [ ] Grade distribution chart
  - [ ] Breakdown tables
- [ ] Create DefectAnalysis.rdlc
  - [ ] Defect type breakdown
  - [ ] Trend chart
- [ ] Create GradeDistribution.rdlc
  - [ ] Pie chart
  - [ ] Trend line chart
- [ ] Create RejectionAnalysis.rdlc
  - [ ] Rejection breakdown
  - [ ] Cost analysis table
- [ ] Create InspectionCertificate.rdlc
  - [ ] Roll info, grade, defects
  - [ ] Inspector signature

### Phase 4: UI Implementation
- [ ] Implement report pages (4 pages)
  - [ ] Parameter controls
  - [ ] ReportViewer binding
  - [ ] Export handlers
- [ ] Integration testing
  - [ ] Test each report
  - [ ] Verify calculations
  - [ ] Test exports

### Phase 5: Deployment
- [ ] Code review
- [ ] Unit tests passing
- [ ] UAT
- [ ] Production deployment

---

**Document Version**: 1.0
**Last Updated**: 2025-10-06
**Status**: Ready for Implementation
**Estimated Effort**: 3 days
