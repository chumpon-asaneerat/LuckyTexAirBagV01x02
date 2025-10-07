# File Renaming Script - Add Sequential Order Numbers
# Created: 2025-10-07

$baseDir = "d:\Projects\NET\Production\Luckytex\LuckyTexAirBagV01x02\Documents\Processes"
Set-Location $baseDir

Write-Host "Starting file renaming process..." -ForegroundColor Green
Write-Host "Base directory: $baseDir" -ForegroundColor Cyan
Write-Host ""

# Define all rename operations
$renames = @(
    # Group 1: Warehouse (001-009)
    @{Old="01_Warehouse/PROCESS_YARN_RECEIVING.md"; New="01_Warehouse/001-PROCESS_YARN_RECEIVING.md"},
    @{Old="UI_Logic_Analysis/12_G3/UI_LOGIC_RECEIVE_YARN.md"; New="UI_Logic_Analysis/12_G3/002-UI_LOGIC_RECEIVE_YARN.md"},
    @{Old="01_Warehouse/PROCESS_YARN_ISSUING.md"; New="01_Warehouse/003-PROCESS_YARN_ISSUING.md"},
    @{Old="UI_Logic_Analysis/12_G3/UI_LOGIC_ISSUE_RAW_MATERIAL.md"; New="UI_Logic_Analysis/12_G3/004-UI_LOGIC_ISSUE_RAW_MATERIAL.md"},
    @{Old="01_Warehouse/PROCESS_YARN_TRANSFER.md"; New="01_Warehouse/005-PROCESS_YARN_TRANSFER.md"},
    @{Old="01_Warehouse/PROCESS_YARN_ADJUSTMENT.md"; New="01_Warehouse/006-PROCESS_YARN_ADJUSTMENT.md"},
    @{Old="UI_Logic_Analysis/12_G3/UI_LOGIC_CHECK_STOCK_YARN.md"; New="UI_Logic_Analysis/12_G3/007-UI_LOGIC_CHECK_STOCK_YARN.md"},
    @{Old="UI_Logic_Analysis/12_G3/UI_LOGIC_EDIT_ISSUE_RAW_MATERIAL.md"; New="UI_Logic_Analysis/12_G3/008-UI_LOGIC_EDIT_ISSUE_RAW_MATERIAL.md"},
    @{Old="UI_Logic_Analysis/12_G3/UI_LOGIC_RECEIVE_RETURN_MATERIAL.md"; New="UI_Logic_Analysis/12_G3/009-UI_LOGIC_RECEIVE_RETURN_MATERIAL.md"},

    # Group 2: Warping (010-015)
    @{Old="02_Warping/PROCESS_CREEL_LOADING.md"; New="02_Warping/010-PROCESS_CREEL_LOADING.md"},
    @{Old="UI_Logic_Analysis/02_Warping/UI_LOGIC_WARPING_SETTING.md"; New="UI_Logic_Analysis/02_Warping/011-UI_LOGIC_WARPING_SETTING.md"},
    @{Old="02_Warping/PROCESS_WARPING_PRODUCTION.md"; New="02_Warping/012-PROCESS_WARPING_PRODUCTION.md"},
    @{Old="UI_Logic_Analysis/02_Warping/UI_LOGIC_WARPING_PROCESS.md"; New="UI_Logic_Analysis/02_Warping/013-UI_LOGIC_WARPING_PROCESS.md"},
    @{Old="02_Warping/PROCESS_BEAM_MANAGEMENT.md"; New="02_Warping/014-PROCESS_BEAM_MANAGEMENT.md"},
    @{Old="UI_Logic_Analysis/02_Warping/UI_LOGIC_WARPING_RECEIVE_YARN.md"; New="UI_Logic_Analysis/02_Warping/015-UI_LOGIC_WARPING_RECEIVE_YARN.md"},

    # Group 3: Beaming (016-017)
    @{Old="03_Beaming/PROCESS_BEAMING_PRODUCTION.md"; New="03_Beaming/016-PROCESS_BEAMING_PRODUCTION.md"},
    @{Old="UI_Logic_Analysis/03_Beaming/UI_LOGIC_BEAMING_PROCESS.md"; New="UI_Logic_Analysis/03_Beaming/017-UI_LOGIC_BEAMING_PROCESS.md"},

    # Group 4: Drawing (018-020)
    @{Old="04_Drawing/PROCESS_DRAWING_IN.md"; New="04_Drawing/018-PROCESS_DRAWING_IN.md"},
    @{Old="UI_Logic_Analysis/04_Drawing/UI_LOGIC_DRAWING_START.md"; New="UI_Logic_Analysis/04_Drawing/019-UI_LOGIC_DRAWING_START.md"},
    @{Old="UI_Logic_Analysis/04_Drawing/UI_LOGIC_DRAWING_FINISH.md"; New="UI_Logic_Analysis/04_Drawing/020-UI_LOGIC_DRAWING_FINISH.md"},

    # Group 5: Weaving (021-026)
    @{Old="05_Weaving/PROCESS_LOOM_SETUP.md"; New="05_Weaving/021-PROCESS_LOOM_SETUP.md"},
    @{Old="UI_Logic_Analysis/05_Weaving/UI_LOGIC_WEAVING_MANUAL_ENTRY.md"; New="UI_Logic_Analysis/05_Weaving/022-UI_LOGIC_WEAVING_MANUAL_ENTRY.md"},
    @{Old="05_Weaving/PROCESS_WEAVING_PRODUCTION.md"; New="05_Weaving/023-PROCESS_WEAVING_PRODUCTION.md"},
    @{Old="UI_Logic_Analysis/05_Weaving/UI_LOGIC_WEAVING_PROCESS.md"; New="UI_Logic_Analysis/05_Weaving/024-UI_LOGIC_WEAVING_PROCESS.md"},
    @{Old="05_Weaving/PROCESS_ROLL_MANAGEMENT.md"; New="05_Weaving/025-PROCESS_ROLL_MANAGEMENT.md"},
    @{Old="05_Weaving/PROCESS_PRODUCTION_REPORTING.md"; New="05_Weaving/026-PROCESS_PRODUCTION_REPORTING.md"},

    # Group 6: Finishing (027-033)
    @{Old="06_Finishing/PROCESS_COATING_HEATSETTING.md"; New="06_Finishing/027-PROCESS_COATING_HEATSETTING.md"},
    @{Old="UI_Logic_Analysis/06_Finishing/UI_LOGIC_COATING1_PREPARING.md"; New="UI_Logic_Analysis/06_Finishing/028-UI_LOGIC_COATING1_PREPARING.md"},
    @{Old="UI_Logic_Analysis/06_Finishing/UI_LOGIC_COATING1_FINISHING.md"; New="UI_Logic_Analysis/06_Finishing/029-UI_LOGIC_COATING1_FINISHING.md"},
    @{Old="UI_Logic_Analysis/06_Finishing/UI_LOGIC_COATING3_PREPARING.md"; New="UI_Logic_Analysis/06_Finishing/030-UI_LOGIC_COATING3_PREPARING.md"},
    @{Old="UI_Logic_Analysis/06_Finishing/UI_LOGIC_COATING3_PROCESSING.md"; New="UI_Logic_Analysis/06_Finishing/031-UI_LOGIC_COATING3_PROCESSING.md"},
    @{Old="UI_Logic_Analysis/06_Finishing/UI_LOGIC_COATING3_FINISHING.md"; New="UI_Logic_Analysis/06_Finishing/032-UI_LOGIC_COATING3_FINISHING.md"},
    @{Old="06_Finishing/PROCESS_FINISHED_ROLL_MANAGEMENT.md"; New="06_Finishing/033-PROCESS_FINISHED_ROLL_MANAGEMENT.md"},

    # Group 7: Inspection (034-037)
    @{Old="08_Inspection/PROCESS_QUALITY_INSPECTION.md"; New="08_Inspection/034-PROCESS_QUALITY_INSPECTION.md"},
    @{Old="UI_Logic_Analysis/08_Inspection/UI_LOGIC_INSPECTION_MODULE.md"; New="UI_Logic_Analysis/08_Inspection/035-UI_LOGIC_INSPECTION_MODULE.md"},
    @{Old="08_Inspection/PROCESS_INSPECTION_REPORTING.md"; New="08_Inspection/036-PROCESS_INSPECTION_REPORTING.md"},
    @{Old="UI_Logic_Analysis/08_Inspection/UI_LOGIC_WEIGHT_MEASUREMENT.md"; New="UI_Logic_Analysis/08_Inspection/037-UI_LOGIC_WEIGHT_MEASUREMENT.md"},

    # Group 8: Cut & Print (038-039)
    @{Old="11_CutPrint/PROCESS_CUTTING_OPERATION.md"; New="11_CutPrint/038-PROCESS_CUTTING_OPERATION.md"},
    @{Old="11_CutPrint/PROCESS_CUT_PIECE_MANAGEMENT.md"; New="11_CutPrint/039-PROCESS_CUT_PIECE_MANAGEMENT.md"},

    # Group 9: Packing (040)
    @{Old="13_Packing/PROCESS_ORDER_PACKING.md"; New="13_Packing/040-PROCESS_ORDER_PACKING.md"},

    # Group 10: Shipping (041)
    @{Old="14_Shipping/PROCESS_SHIPMENT_MANAGEMENT.md"; New="14_Shipping/041-PROCESS_SHIPMENT_MANAGEMENT.md"},

    # Group 11: Master Data (042-047)
    @{Old="17_MasterData/PROCESS_MACHINE_MANAGEMENT.md"; New="17_MasterData/042-PROCESS_MACHINE_MANAGEMENT.md"},
    @{Old="17_MasterData/PROCESS_EMPLOYEE_MANAGEMENT.md"; New="17_MasterData/043-PROCESS_EMPLOYEE_MANAGEMENT.md"},
    @{Old="17_MasterData/PROCESS_SHIFT_MANAGEMENT.md"; New="17_MasterData/044-PROCESS_SHIFT_MANAGEMENT.md"},
    @{Old="17_MasterData/PROCESS_PRODUCT_MANAGEMENT.md"; New="17_MasterData/045-PROCESS_PRODUCT_MANAGEMENT.md"},
    @{Old="17_MasterData/PROCESS_CUSTOMER_SUPPLIER_MANAGEMENT.md"; New="17_MasterData/046-PROCESS_CUSTOMER_SUPPLIER_MANAGEMENT.md"},
    @{Old="17_MasterData/PROCESS_REFERENCE_DATA_MANAGEMENT.md"; New="17_MasterData/047-PROCESS_REFERENCE_DATA_MANAGEMENT.md"},

    # Group 12: D365 Integration (048)
    @{Old="19_D365Integration/PROCESS_ERP_SYNCHRONIZATION.md"; New="19_D365Integration/048-PROCESS_ERP_SYNCHRONIZATION.md"},

    # Group 13: User Management (049-050)
    @{Old="20_UserManagement/PROCESS_USER_MANAGEMENT.md"; New="20_UserManagement/049-PROCESS_USER_MANAGEMENT.md"},
    @{Old="20_UserManagement/PROCESS_ROLE_PERMISSION_MANAGEMENT.md"; New="20_UserManagement/050-PROCESS_ROLE_PERMISSION_MANAGEMENT.md"}
)

$count = 0
$success = 0
$failed = 0

foreach ($rename in $renames) {
    $count++
    $oldPath = Join-Path $baseDir $rename.Old
    $newPath = Join-Path $baseDir $rename.New

    Write-Host "[$count/50] Renaming: $($rename.Old)" -ForegroundColor Yellow

    if (Test-Path $oldPath) {
        try {
            # Use git mv for proper version control
            git mv $oldPath $newPath 2>$null

            if ($LASTEXITCODE -eq 0) {
                Write-Host "  ✓ Success: $($rename.New)" -ForegroundColor Green
                $success++
            } else {
                # Fallback to regular rename if git fails
                Move-Item -Path $oldPath -Destination $newPath -Force
                Write-Host "  ✓ Success (non-git): $($rename.New)" -ForegroundColor Green
                $success++
            }
        }
        catch {
            Write-Host "  ✗ Failed: $($_.Exception.Message)" -ForegroundColor Red
            $failed++
        }
    } else {
        Write-Host "  ⚠ File not found: $oldPath" -ForegroundColor Magenta
        $failed++
    }
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Renaming Complete!" -ForegroundColor Green
Write-Host "Total files: $count" -ForegroundColor White
Write-Host "Successful: $success" -ForegroundColor Green
Write-Host "Failed: $failed" -ForegroundColor Red
Write-Host "========================================" -ForegroundColor Cyan
