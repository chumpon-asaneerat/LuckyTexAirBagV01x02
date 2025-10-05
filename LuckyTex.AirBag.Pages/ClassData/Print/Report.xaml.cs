#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using System.IO;

using System.Reflection;
using NLib;
using NLib.Data;
using NLib.Components;

using LuckyTex.Services;
using LuckyTex.Models;

using System.Diagnostics;
using System.Printing;
using System.Drawing.Printing;

using System.Globalization;
using System.Threading;
#endregion


namespace DataControl.ClassData
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        #region Report
        public Report(bool bprint = false)
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);


            Title = "";
            PriReport(ConmonReportService.Instance.ReportName);
        }
        #endregion

        #region SetSize

        private LocalReportPageSettings _pageSetting = new LocalReportPageSettings();

        private void SetSize(string PaperSize , bool Landscape)
        {
            _pageSetting = new LocalReportPageSettings();

            if (Landscape == false)
            {
                _pageSetting.PageLandscape = false;

                if (PaperSize == "A4")
                {
                    #region A4

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0.38;

                    _pageSetting.PageHeight = 11.69;
                    _pageSetting.PageWidth = 8.27;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                    _pageSetting.PaperName = "A4";
                    _pageSetting.Height = 1169;
                    _pageSetting.Width = 827;

                    #endregion
                }
                else if (PaperSize == "Postcard")
                {
                    #region Postcard

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0;

                    _pageSetting.PageHeight = 3.93701;
                    _pageSetting.PageWidth = 3.93701;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;

                    #endregion
                }
                else if (PaperSize == "A6")
                {
                    #region A6

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0;

                    _pageSetting.PageHeight = 5.83;
                    _pageSetting.PageWidth = 4.13;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.A6;
                    _pageSetting.PaperName = "A6";
                    _pageSetting.Height = 583;
                    _pageSetting.Width = 413;

                    #endregion
                }
                else if (PaperSize == "2D")
                {
                    #region 2D

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0;

                    _pageSetting.PageHeight = 1.771;
                    _pageSetting.PageWidth = 3.937;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;

                    #endregion
                }
                else if (PaperSize == "2DBig")
                {
                    #region 2DBig

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0;

                    //_pageSetting.PageHeight = 4.13386;
                    //_pageSetting.PageWidth = 1.49606;

                    _pageSetting.PageHeight = 3.5433;
                    _pageSetting.PageWidth = 8.2677;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;

                    #endregion
                }
            }
            else if (Landscape == true)
            {
                _pageSetting.PageLandscape = true;

                if (PaperSize == "A4")
                {
                    #region A4

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0.4;

                    _pageSetting.PageHeight = 8.27;
                    _pageSetting.PageWidth = 11.69;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                    _pageSetting.PaperName = "A4";
                    _pageSetting.Height = 1169;
                    _pageSetting.Width = 827;

                    #endregion
                }
                else if (PaperSize == "2DBig")
                {
                    #region 2DBig

                    _pageSetting.MarginLeft = 0;
                    _pageSetting.MarginRight = 0;
                    _pageSetting.MarginTop = 0;
                    _pageSetting.MarginBottom = 0;

                    //_pageSetting.PageHeight = 1.49606;
                    //_pageSetting.PageWidth = 4.13386;

                    _pageSetting.PageHeight = 8.2677;
                    _pageSetting.PageWidth = 3.5433;

                    _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.Custom;

                    #endregion
                }
            }
        }

        #endregion

        #region PrintByPrinter

        public void PrintByPrinter(string printerName)
        {
            LocalReport lr = new LocalReport();
            lr = this._reportViewer.LocalReport;

            StickerPrintService.Instance.Print(lr, printerName, _pageSetting);
        }

        #endregion

        #region PriReport
        private void PriReport(string repName)
        {
            if (repName == "Inspection")
            {
                RepInspection(ConmonReportService.Instance.CmdString);
                Title = "Inspection";
            }
            else if (repName == "RePrint_Inspection")
            {
                RepInspection(ConmonReportService.Instance.CmdString);
                Title = "Inspection";
            }
            else if (repName == "TestInspection")
            {
                RepTestInspection(ConmonReportService.Instance.CmdString, ConmonReportService.Instance.GRADE, ConmonReportService.Instance.NetLenge, ConmonReportService.Instance.GrossLength);
                Title = "Inspection";
            }
            if (repName == "InspectionRemark")
            {
                RepInspectionRemark(ConmonReportService.Instance.CmdString, ConmonReportService.Instance.UseShiftRemark);
                Title = "Inspection Remark";
            }
            else if (repName == "TestInspectionRemark")
            {
                RepTestInspectionRemark(ConmonReportService.Instance.CmdString, ConmonReportService.Instance.GRADE, ConmonReportService.Instance.NetLenge, ConmonReportService.Instance.GrossLength, ConmonReportService.Instance.UseShiftRemark);
                Title = "Inspection Remark";
            }
            else if (repName == "Scouring1")
            {
                RepScouring1(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Scouring1";
            }
            else if (repName == "Scouring2")
            {
                RepScouring2(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Scouring2";
            }
            else if (repName == "ScouringDryer")
            {
                RepScouringDryer(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "ScouringDryer";
            }
            else if (repName == "Coating1")
            {
                RepCoating1(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating 1";
            }
            else if (repName == "Coating2")
            {
                RepCoating2(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating 2";
            }
            else if (repName == "Coating3")
            {
                RepCoating3(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating 3";
            }
            else if (repName == "Coating1Dryer")
            {
                RepCoating1Dryer(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating1 Dryer";
            }
            else if (repName == "Coating12Step")
            {
                RepCoating12Step(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating1 2Step";
            }
            else if (repName == "Coating3Scouring")
            {
                RepCoating3Scouring(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating3Scouring";
            }
            else if (repName == "ScouringCoat1")
            {
                RepScouringCoat1(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Scouring Coat 1";
            }
            else if (repName == "ScouringCoat2")
            {
                RepScouringCoat2(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Scouring Coat 2";
            }
            else if (repName == "Coating1Scouring")
            {
                RepCoating1Scouring(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Coating1 Scouring";
            }
            else if (repName == "Scouring2ScouringDry")
            {
                RepScouring2ScouringDry(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code
                    , ConmonReportService.Instance.ScouringNo, ConmonReportService.Instance.FinishingLot);
                Title = "Scouring2 Scouring Dry";
            }
            else if (repName == "BarcodePart")
            {
                RepBarcodePart(ConmonReportService.Instance.CmdString);
                Title = "BarcodePart";
            }
            else if (repName == "CUT_GETSLIP")
            {
                RepCUT_GETSLIP(ConmonReportService.Instance.ITEMLOT);
                Title = "CUT_GETSLIP";
            }
            else if (repName == "PalletList")
            {
                if (ConmonReportService.Instance.GRADE != "C" && ConmonReportService.Instance.GRADE != "X")
                {
                    RepPalletListGrade_AB(ConmonReportService.Instance.PALLETNO);
                }
                else
                {
                    RepPalletListGrade_C(ConmonReportService.Instance.PALLETNO);
                }

                Title = "PalletList";
            }
            else if (repName == "Weaving")
            {
                RepWeaving(ConmonReportService.Instance.WEAVINGLOT);
                Title = "Weaving";
            }
            else if (repName == "WeavingSampling")
            {
                RepWeavingSampling(ConmonReportService.Instance.BEAMLOT, ConmonReportService.Instance.LOOM);
                Title = "WeavingSampling";
            }
            else if (repName == "PackingLabel")
            {
                RepPackingLabel(ConmonReportService.Instance.INSLOT);
                Title = "PackingLabel";
            }
            else if (repName == "GreyRollDaily")
            {
                RepGreyRollDaily(ConmonReportService.Instance.WEAVINGDATE, ConmonReportService.Instance.CHINA);
                Title = "GreyRollDaily";
            }
            else if (repName == "TransferSlip")
            {
                RepTransferSlip(ConmonReportService.Instance.WARPHEADNO, ConmonReportService.Instance.WARPLOT);
                Title = "Transfer Slip";
            }
            else if (repName == "BeamTransferSlip")
            {
                RepBeamTransferSlip(ConmonReportService.Instance.BEAMLOT);
                Title = "Beam Transfer Slip";
            }
            else if (repName == "Sampling")
            {
                RepSampling(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.FinishingLot);
                Title = "Sampling";
            }
            else if (repName == "FM-QC-18-06")
            {
                RepFMQC1806(ConmonReportService.Instance.WEAVINGLOT,ConmonReportService.Instance.ITM_Code);
                Title = "FM-QC-18-06";
            }
            else if (repName == "EM-QC-19-06")
            {
                RepEMQC1906(ConmonReportService.Instance.WEAVINGLOT, ConmonReportService.Instance.ITM_Code);
                Title = "EM-QC-19-06";
            }
            else if (repName == "WarpingRecord")
            {
                RepWarpingRecord(ConmonReportService.Instance.WARPHEADNO, ConmonReportService.Instance.SIDE, ConmonReportService.Instance.MCNO, ConmonReportService.Instance.ITMPREPARE,
                     ConmonReportService.Instance.WTYPE, ConmonReportService.Instance.CONDITIONBY, ConmonReportService.Instance.REEDNO, ConmonReportService.Instance.CONDITIONSTART);
                Title = "WarpingRecord";
            }
            else if (repName == "BeamingRecord")
            {
                RepBeamingRecord(ConmonReportService.Instance.BEAMERNO, ConmonReportService.Instance.ITMPREPARE, ConmonReportService.Instance.MCNO, ConmonReportService.Instance.TOTALYARN,
                     ConmonReportService.Instance.TOTALKEBA, ConmonReportService.Instance.ADJUSTKEBA, ConmonReportService.Instance.STARTDATE, ConmonReportService.Instance.ENDDATE, ConmonReportService.Instance.REMARK);
                Title = "BeamingRecord";
            }
            else if (repName == "FinishRecord")
            {
                RepFinishRecord(ConmonReportService.Instance.CmdStringDate, ConmonReportService.Instance.MCNO, ConmonReportService.Instance.CmdString);
                Title = "Finish Record";
            }
            else if (repName == "LabFinishRecord")
            {
                RepLabFinishRecord(ConmonReportService.Instance.CmdStringDate, ConmonReportService.Instance.MCNO, ConmonReportService.Instance.ITM_Code, ConmonReportService.Instance.CmdString);
                Title = "Finish Record";
            }
            else if (repName == "WeavingHistory")
            {
                RepWeavingHistory(ConmonReportService.Instance.LOOM, ConmonReportService.Instance.BEAMLOT, ConmonReportService.Instance.BEAMERNO, ConmonReportService.Instance.BARNO
                    , ConmonReportService.Instance.ITM_Code, ConmonReportService.Instance.CmdStringDate
                    , ConmonReportService.Instance.CmdStringStartDate, ConmonReportService.Instance.WEFTYARN
                    , ConmonReportService.Instance.WIDTH, ConmonReportService.Instance.BEAMLENGTH, ConmonReportService.Instance.SPEED);
                Title = "Weaving History";
            }
            else if (repName == "WeavingRecord")
            {
                RepWeavingRecord(ConmonReportService.Instance.LOOM, ConmonReportService.Instance.BEAMLOT, ConmonReportService.Instance.BEAMERNO, ConmonReportService.Instance.BARNO
                    , ConmonReportService.Instance.ITM_Code, ConmonReportService.Instance.CmdStringDate
                    , ConmonReportService.Instance.CmdStringStartDate, ConmonReportService.Instance.WEFTYARN
                    , ConmonReportService.Instance.WIDTH, ConmonReportService.Instance.BEAMLENGTH, ConmonReportService.Instance.SPEED, ConmonReportService.Instance.WEAVINGLOT);
                Title = "Weaving Record";
            }
            else if (repName == "G3_GoLabel")
            {
                RepG3_GoLabel(ConmonReportService.Instance.PALLETNO, ConmonReportService.Instance.TRACENO,
                       ConmonReportService.Instance.LOTNO, ConmonReportService.Instance.ITM_YARN,
                       ConmonReportService.Instance.CONECH, ConmonReportService.Instance.ENTRYDATE,
                       ConmonReportService.Instance.YARNTYPE, ConmonReportService.Instance.WEIGHTQTY);
                Title = "G3_GoLabel";
            }
            else if (repName == "IssueRawMaterial")
            {
                RepIssueRawMaterial(ConmonReportService.Instance.REQUESTNO);
                Title = "IssueRawMaterial";
            }
            else if (repName == "CheckingAirbag")
            {
                RepCheckingAirbag(ConmonReportService.Instance.P_CUSID, ConmonReportService.Instance.P_DATE, ConmonReportService.Instance.P_LABITMCODE, ConmonReportService.Instance.P_RESULT);
                Title = "CheckingAirbag";
            }
            else if (repName == "DRAW_DAILYREPORT")
            {
                RepDRAW_DAILYREPORT(ConmonReportService.Instance.P_DATE);
                Title = "DRAW_DAILYREPORT";
            }
            else if (repName == "DRAW_TRANSFERSLIP")
            {
                RepDRAW_TRANSFERSLIP(ConmonReportService.Instance.P_BEAMERROLL);
                Title = "DRAW_TRANSFERSLIP";
            }
            else if (repName == "WEAV_SHIPMENTREPORT")
            {
                RepWEAV_SHIPMENTREPORT(ConmonReportService.Instance.P_BEGINDATE, ConmonReportService.Instance.P_ENDDATE);
                Title = "WEAV_SHIPMENTREPORT";
            }
            else if (repName == "CUT_SERACHLIST")
            {
                RepCUT_SERACHLIST(ConmonReportService.Instance.P_DATE, ConmonReportService.Instance.P_MC);
                Title = "CUT_SERACHLIST";
            }
            else if (repName == "WarpingList")
            {
                RepWarpingList(ConmonReportService.Instance.WARPHEADNO, ConmonReportService.Instance.P_MC, ConmonReportService.Instance.ITMPREPARE
                    , ConmonReportService.Instance.P_DATE, ConmonReportService.Instance.P_ENDDATE);
                Title = "WarpingList";
            }
            else if (repName == "BeamingList")
            {
                RepBeamingList(ConmonReportService.Instance.BEAMERNO, ConmonReportService.Instance.P_MC, ConmonReportService.Instance.ITMPREPARE
                    , ConmonReportService.Instance.P_DATE, ConmonReportService.Instance.P_ENDDATE);
                Title = "BeamingList";
            }
            else if (repName == "Sample4746P25R")
            {
                RepSample4746P25R();
                Title = "Sample 4746P25R";
            }
            else if (repName == "Sample4755ATW")
            {
                RepSample4755ATW();
                Title = "Sample 4755ATW";
            }
            else if (repName == "Sample4L50B25R")
            {
                RepSample4L50B25R();
                Title = "Sample 4L50B25R";
            }
            else if (repName == "PackingLabel2D")
            {
                RepPackingLabel2D(ConmonReportService.Instance.INSLOT);
                Title = "PackingLabel2D";
            }
            else if (repName == "PackingLabel2DBig")
            {
                RepPackingLabel2DBig(ConmonReportService.Instance.INSLOT);
                Title = "PackingLabel2DBig";
            }
            else if (repName == "Production")
            {
                RepLabTestingResult(ConmonReportService.Instance.ITM_Code, ConmonReportService.Instance.ENTRYSTARTDATE, ConmonReportService.Instance.ENTRYENDDATE,
               ConmonReportService.Instance.LOOM, ConmonReportService.Instance.FINISHPROCESS, ConmonReportService.Instance.WEAVINGLOT,
               ConmonReportService.Instance.FinishingLot, ConmonReportService.Instance.ENTRYDATE);
                Title = "Production";
            }
        }
        #endregion

        #region RepInspection
        private void RepInspection(string insLotNo)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

            try
            {
                List<InspectionReportData> lots =
                       InspectionDataService.Instance.GetInspectionReportData(insLotNo);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    #region DataSet1

                    string _testId = string.Empty;
                    string _defectId = string.Empty;
                    string _itemCode = string.Empty;
                    decimal? sumDefectLength = 0;

                    InspectionReportData lot = lots[0];

                    List<DataControl.ClassData.tInspectionClassData.ListInspection> results = new List<DataControl.ClassData.tInspectionClassData.ListInspection>();

                    DataControl.ClassData.tInspectionClassData.ListInspection inst = new DataControl.ClassData.tInspectionClassData.ListInspection();
                    inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                    inst.ITEMCODE = (!string.IsNullOrWhiteSpace(lot.ITEMCODE)) ?
                       lot.ITEMCODE.Trim().ToUpper() : string.Empty;

                    _itemCode = inst.ITEMCODE;

                    inst.FINISHINGLOT = lot.FINISHINGLOT;
                    inst.STARTDATE = lot.STARTDATE;
                    inst.ENDDATE = lot.ENDDATE;

                    if (lot.GROSSLENGTH != null)
                        inst.GROSSLENGTH = MathEx.TruncateDecimal(lot.GROSSLENGTH.Value, 1);

                    if (lot.NETLENGTH != null)
                        inst.NETLENGTH = MathEx.TruncateDecimal(lot.NETLENGTH.Value, 1);

                    inst.CUSTOMERID = lot.CUSTOMERID;
                    inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                    inst.LOADINGTYPE = lot.LOADINGTYPE;

                    if (lot.CUSTOMERID != null)
                    {
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "09")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "06")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "11")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "12")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else
                        {
                            inst.GrossNetLength = inst.GROSSLENGTH;
                        }
                    }
                    else
                    {
                        inst.GrossNetLength = inst.GROSSLENGTH;
                    }

                    inst.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                    inst.GRADE = (!string.IsNullOrWhiteSpace(lot.GRADE)) ?
                             lot.GRADE.Trim().ToUpper() : "-";
                    inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                    inst.NETWEIGHT = lot.NETWEIGHT;
                    inst.PEINSPECTIONLOT = lot.PEINSPECTIONLOT;
                    inst.DEFECTID = lot.DEFECTID;

                    _defectId = inst.DEFECTID;
                    _testId = lot.TESTRECORDID;

                    inst.SHIFT_ID = lot.SHIFT_ID;
                    inst.SHIFT_REMARK = lot.SHIFT_REMARK;
                    inst.REMARK = lot.REMARK;

                    inst.ATTACHID = lot.ATTACHID;
                    inst.TESTRECORDID = lot.TESTRECORDID;
                    inst.INSPECTEDBY = lot.INSPECTEDBY;
                    inst.MCNO = lot.MCNO;
                    inst.FINISHFLAG = lot.FINISHFLAG;
                    inst.SUSPENDDATE = lot.SUSPENDDATE;
                    inst.INSPECTIONID = lot.INSPECTIONID;
                    inst.RETEST = lot.RETEST;
                    inst.PREITEMCODE = lot.PREITEMCODE;

                    inst.PRODUCTNAME = lot.PRODUCTNAME;
                    inst.MCNAME = lot.MCNAME;
                    inst.CUSTOMERNAME = lot.CUSTOMERNAME;
                    inst.DEFECTFILENAME = lot.DEFECTFILENAME;
                    inst.PARTNO = lot.PARTNO;

                    //New 23/8/22
                    inst.CONFIRMSTARTLENGTH = lot.CONFIRMSTARTLENGTH;
                    inst.CONFIRMSTDLENGTH = lot.CONFIRMSTDLENGTH;

                    //เอาออกก่อนยังไม่ให้ใช้
                    //New 18/10/22
                    //inst.RESETSTARTLENGTH = lot.RESETSTARTLENGTH;

                    results.Add(inst);

                    #endregion

                    if (results.Count > 0)
                    {
                        bool ChkDefect100 = false;

                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                        Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds4 = new Microsoft.Reporting.WinForms.ReportDataSource();

                        #region InspectionTestHistoryItem

                        List<InspectionTestHistoryItem> lots2 =
                            InspectionDataService.Instance.GetTestHistoryList(insLotNo, _testId);

                        List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList> resultsCordList = new List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList>();

                        if (null != lots2 && lots2.Count > 0)
                        {
                            for (int i = 0; i < lots2.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInstestreCordList instCordList = new DataControl.ClassData.tInspectionClassData.ListInstestreCordList();

                                // Old
                                //instCordList.STDLength = lots2[i].STDLength;
                                //instCordList.ActualLength = lots2[i].ActualLength;

                                if (lots2[i].STDLength != "")
                                    instCordList.STDLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].STDLength), 1).ToString();

                                if (lots2[i].ActualLength != "")
                                    instCordList.ActualLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].ActualLength), 1).ToString();

                                instCordList.DensityW = lots2[i].DensityW;
                                instCordList.DensityF = lots2[i].DensityF;
                                instCordList.WidthAll = lots2[i].WidthAll;
                                instCordList.WidthPin = lots2[i].WidthPin;
                                instCordList.WidthCoat = lots2[i].WidthCoat;
                                instCordList.TrimL = lots2[i].TrimL;
                                instCordList.TrimR = lots2[i].TrimR;
                                instCordList.FloppyL = lots2[i].FloppyL;
                                instCordList.FloppyR = lots2[i].FloppyR;
                                instCordList.UnwinderSet = lots2[i].UnwinderSet;
                                instCordList.UnwinderActual = lots2[i].UnwinderActual;
                                instCordList.WinderSet = lots2[i].WinderSet;
                                instCordList.WinderActual = lots2[i].WinderActual;

                                resultsCordList.Add(instCordList);
                            }
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }
                        else
                        {
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }

                        #endregion

                        #region InspectionDefectItem

                        List<InspectionDefectItem> lots3 =
                            InspectionDataService.Instance.ins_GetDefectListReport(insLotNo, _defectId);

                        List<DataControl.ClassData.tInspectionClassData.ListInsdefectList> resultsInsdefectList = new List<DataControl.ClassData.tInspectionClassData.ListInsdefectList>();

                        if (null != lots3 && lots3.Count > 0)
                        {

                            for (int i = 0; i < lots3.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInsdefectList instInsdefectList = new DataControl.ClassData.tInspectionClassData.ListInsdefectList();

                                instInsdefectList.No = lots3[i].No;

                                if (lots3[i].Length != "")
                                    instInsdefectList.Length = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length), 1).ToString("#,##0.0");

                                if (lots3[i].Length2 != "")
                                    instInsdefectList.Length2 = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length2), 1).ToString("#,##0.0");

                                if (lots3[i].DefectLength != "")
                                {
                                    sumDefectLength += MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1);
                                    instInsdefectList.DefectLength = MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1).ToString("#,##0.0");
                                }

                                instInsdefectList.DefectCode = lots3[i].DefectCode;
                                instInsdefectList.Description = lots3[i].Description;
                                instInsdefectList.Position = lots3[i].Position;

                                instInsdefectList.DEFECTPOINT100 = lots3[i].DEFECTPOINT100;

                                if (instInsdefectList.DEFECTPOINT100 != null)
                                    ChkDefect100 = true;

                                resultsInsdefectList.Add(instInsdefectList);
                            }

                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }
                        else
                        {
                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }

                        #endregion

                        #region InspectionSumDefectItem

                        List<INS_ReportSumDefectData> lots4 =
                            InspectionDataService.Instance.GetINS_ReportSumDefect(_defectId, _itemCode);

                        List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList> resultsInsSumDefectList = new List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList>();

                        if (null != lots4 && lots4.Count > 0)
                        {
                            for (int i = 0; i < lots4.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList instInsSumDefectList = new DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList();

                                instInsSumDefectList.TotalPoint = lots4[i].TOTALPOINT;
                                instInsSumDefectList.ShortDefect = lots4[i].SHORTDEFECT;
                                instInsSumDefectList.LongDefect = lots4[i].LONGDEFECT;

                                if (sumDefectLength > 0)
                                    instInsSumDefectList.ComLongDefect = sumDefectLength;
                                else
                                    instInsSumDefectList.ComLongDefect = lots4[i].COMLONGDEFECT;

                                instInsSumDefectList.ComShortDefect = lots4[i].COMSHORTDEFECT;

                                resultsInsSumDefectList.Add(instInsSumDefectList);
                            }

                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }
                        else
                        {
                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }

                        #endregion

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.DataSources.Add(rds2);
                        this._reportViewer.LocalReport.DataSources.Add(rds3);
                        this._reportViewer.LocalReport.DataSources.Add(rds4);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepInspection.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;
                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        if (ChkDefect100 == true)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", "+"));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", ""));

                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepTestInspection
        private void RepTestInspection(string insLotNo, string grade, decimal? netLenge, decimal? grossLength)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

            try
            {
                List<InspectionReportData> lots =
                       InspectionDataService.Instance.GetInspectionReportData(insLotNo);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    #region DataSet1

                    string _testId = string.Empty;
                    string _defectId = string.Empty;
                    string _itemCode = string.Empty;
                    decimal? sumDefectLength = 0;

                    InspectionReportData lot = lots[0];

                    List<DataControl.ClassData.tInspectionClassData.ListInspection> results = new List<DataControl.ClassData.tInspectionClassData.ListInspection>();

                    DataControl.ClassData.tInspectionClassData.ListInspection inst = new DataControl.ClassData.tInspectionClassData.ListInspection();
                    inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                    inst.ITEMCODE = (!string.IsNullOrWhiteSpace(lot.ITEMCODE)) ?
                       lot.ITEMCODE.Trim().ToUpper() : string.Empty;

                    _itemCode = inst.ITEMCODE;

                    inst.FINISHINGLOT = lot.FINISHINGLOT;
                    inst.STARTDATE = lot.STARTDATE;
                    inst.ENDDATE = lot.ENDDATE;

                    if (grossLength != null)
                        inst.GROSSLENGTH = MathEx.TruncateDecimal(grossLength.Value, 1);

                    if (netLenge != null)
                        inst.NETLENGTH = MathEx.TruncateDecimal(netLenge.Value, 1);

                    inst.CUSTOMERID = lot.CUSTOMERID;
                    inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                    inst.LOADINGTYPE = lot.LOADINGTYPE;

                    if (lot.CUSTOMERID != null)
                    {
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "09")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "06")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "11")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "12")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else
                        {
                            inst.GrossNetLength = inst.GROSSLENGTH;
                        }
                    }
                    else
                    {
                        inst.GrossNetLength = inst.GROSSLENGTH;
                    }

                    inst.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                    inst.GRADE = grade;
                    inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                    inst.NETWEIGHT = lot.NETWEIGHT;
                    inst.PEINSPECTIONLOT = lot.PEINSPECTIONLOT;
                    inst.DEFECTID = lot.DEFECTID;

                    _defectId = inst.DEFECTID;
                    _testId = lot.TESTRECORDID;

                    inst.SHIFT_ID = lot.SHIFT_ID;
                    inst.SHIFT_REMARK = lot.SHIFT_REMARK;
                    inst.REMARK = lot.REMARK;

                    inst.ATTACHID = lot.ATTACHID;
                    inst.TESTRECORDID = lot.TESTRECORDID;
                    inst.INSPECTEDBY = lot.INSPECTEDBY;
                    inst.MCNO = lot.MCNO;
                    inst.FINISHFLAG = lot.FINISHFLAG;
                    inst.SUSPENDDATE = lot.SUSPENDDATE;
                    inst.INSPECTIONID = lot.INSPECTIONID;
                    inst.RETEST = lot.RETEST;
                    inst.PREITEMCODE = lot.PREITEMCODE;

                    inst.PRODUCTNAME = lot.PRODUCTNAME;
                    inst.MCNAME = lot.MCNAME;
                    inst.CUSTOMERNAME = lot.CUSTOMERNAME;
                    inst.DEFECTFILENAME = lot.DEFECTFILENAME;
                    inst.PARTNO = lot.PARTNO;

                    //New 23/8/22
                    inst.CONFIRMSTARTLENGTH = lot.CONFIRMSTARTLENGTH;
                    inst.CONFIRMSTDLENGTH = lot.CONFIRMSTDLENGTH;

                    //เอาออกก่อนยังไม่ให้ใช้
                    //New 18/10/22
                    //inst.RESETSTARTLENGTH = lot.RESETSTARTLENGTH;

                    results.Add(inst);

                    #endregion

                    if (results.Count > 0)
                    {
                        bool ChkDefect100 = false;

                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                        Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds4 = new Microsoft.Reporting.WinForms.ReportDataSource();

                        #region InspectionTestHistoryItem

                        List<InspectionTestHistoryItem> lots2 =
                            InspectionDataService.Instance.GetTestHistoryList(insLotNo, _testId);

                        List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList> resultsCordList = new List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList>();

                        if (null != lots2 && lots2.Count > 0)
                        {
                            for (int i = 0; i < lots2.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInstestreCordList instCordList = new DataControl.ClassData.tInspectionClassData.ListInstestreCordList();

                                // Old
                                //instCordList.STDLength = lots2[i].STDLength;
                                //instCordList.ActualLength = lots2[i].ActualLength;

                                if (lots2[i].STDLength != "")
                                    instCordList.STDLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].STDLength), 1).ToString();

                                if (lots2[i].ActualLength != "")
                                    instCordList.ActualLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].ActualLength), 1).ToString();

                                instCordList.DensityW = lots2[i].DensityW;
                                instCordList.DensityF = lots2[i].DensityF;
                                instCordList.WidthAll = lots2[i].WidthAll;
                                instCordList.WidthPin = lots2[i].WidthPin;
                                instCordList.WidthCoat = lots2[i].WidthCoat;
                                instCordList.TrimL = lots2[i].TrimL;
                                instCordList.TrimR = lots2[i].TrimR;
                                instCordList.FloppyL = lots2[i].FloppyL;
                                instCordList.FloppyR = lots2[i].FloppyR;
                                instCordList.UnwinderSet = lots2[i].UnwinderSet;
                                instCordList.UnwinderActual = lots2[i].UnwinderActual;
                                instCordList.WinderSet = lots2[i].WinderSet;
                                instCordList.WinderActual = lots2[i].WinderActual;

                                resultsCordList.Add(instCordList);
                            }
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }
                        else
                        {
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }

                        #endregion

                        #region InspectionDefectItem

                        List<InspectionDefectItem> lots3 =
                            InspectionDataService.Instance.ins_GetDefectListReport(insLotNo, _defectId);

                        List<DataControl.ClassData.tInspectionClassData.ListInsdefectList> resultsInsdefectList = new List<DataControl.ClassData.tInspectionClassData.ListInsdefectList>();

                        if (null != lots3 && lots3.Count > 0)
                        {

                            for (int i = 0; i < lots3.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInsdefectList instInsdefectList = new DataControl.ClassData.tInspectionClassData.ListInsdefectList();

                                instInsdefectList.No = lots3[i].No;

                                if (lots3[i].Length != "")
                                    instInsdefectList.Length = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length), 1).ToString("#,##0.0");

                                if (lots3[i].Length2 != "")
                                    instInsdefectList.Length2 = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length2), 1).ToString("#,##0.0");

                                if (lots3[i].DefectLength != "")
                                {
                                    sumDefectLength += MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1);
                                    instInsdefectList.DefectLength = MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1).ToString("#,##0.0");
                                }

                                instInsdefectList.DefectCode = lots3[i].DefectCode;
                                instInsdefectList.Description = lots3[i].Description;
                                instInsdefectList.Position = lots3[i].Position;
                                instInsdefectList.DEFECTPOINT100 = lots3[i].DEFECTPOINT100;

                                if (instInsdefectList.DEFECTPOINT100 != null)
                                    ChkDefect100 = true;

                                resultsInsdefectList.Add(instInsdefectList);
                            }

                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }
                        else
                        {
                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }

                        #endregion

                        #region InspectionSumDefectItem

                        List<INS_ReportSumDefectData> lots4 =
                            InspectionDataService.Instance.GetINS_ReportSumDefect(_defectId, _itemCode);

                        List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList> resultsInsSumDefectList = new List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList>();

                        if (null != lots4 && lots4.Count > 0)
                        {
                            for (int i = 0; i < lots4.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList instInsSumDefectList = new DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList();

                                instInsSumDefectList.TotalPoint = lots4[i].TOTALPOINT;
                                instInsSumDefectList.ShortDefect = lots4[i].SHORTDEFECT;
                                instInsSumDefectList.LongDefect = lots4[i].LONGDEFECT;

                                if (sumDefectLength > 0)
                                    instInsSumDefectList.ComLongDefect = sumDefectLength;
                                else
                                    instInsSumDefectList.ComLongDefect = lots4[i].COMLONGDEFECT;

                                instInsSumDefectList.ComShortDefect = lots4[i].COMSHORTDEFECT;

                                resultsInsSumDefectList.Add(instInsSumDefectList);
                            }

                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }
                        else
                        {
                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }

                        #endregion

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.DataSources.Add(rds2);
                        this._reportViewer.LocalReport.DataSources.Add(rds3);
                        this._reportViewer.LocalReport.DataSources.Add(rds4);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepInspection.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;
                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        if (ChkDefect100 == true)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", "+"));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", ""));

                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepInspectionRemark
        private void RepInspectionRemark(string insLotNo, bool UseShiftRemark)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

            try
            {
                List<InspectionReportData> lots =
                       InspectionDataService.Instance.GetInspectionReportData(insLotNo);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    #region DataSet1

                    string _testId = string.Empty;
                    string _defectId = string.Empty;
                    string _itemCode = string.Empty;
                    decimal? sumDefectLength = 0;

                    InspectionReportData lot = lots[0];

                    List<DataControl.ClassData.tInspectionClassData.ListInspection> results = new List<DataControl.ClassData.tInspectionClassData.ListInspection>();

                    DataControl.ClassData.tInspectionClassData.ListInspection inst = new DataControl.ClassData.tInspectionClassData.ListInspection();
                    inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                    inst.ITEMCODE = (!string.IsNullOrWhiteSpace(lot.ITEMCODE)) ?
                       lot.ITEMCODE.Trim().ToUpper() : string.Empty;

                    _itemCode = inst.ITEMCODE;

                    inst.FINISHINGLOT = lot.FINISHINGLOT;
                    inst.STARTDATE = lot.STARTDATE;
                    inst.ENDDATE = lot.ENDDATE;

                    if (lot.GROSSLENGTH != null)
                        inst.GROSSLENGTH = MathEx.TruncateDecimal(lot.GROSSLENGTH.Value, 1);

                    if (lot.NETLENGTH != null)
                        inst.NETLENGTH = MathEx.TruncateDecimal(lot.NETLENGTH.Value, 1);

                    inst.CUSTOMERID = lot.CUSTOMERID;
                    inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                    inst.LOADINGTYPE = lot.LOADINGTYPE;

                    if (lot.CUSTOMERID != null)
                    {
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "09")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "06")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "11")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "12")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else
                        {
                            inst.GrossNetLength = inst.GROSSLENGTH;
                        }
                    }
                    else
                    {
                        inst.GrossNetLength = inst.GROSSLENGTH;
                    }

                    inst.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                    inst.GRADE = (!string.IsNullOrWhiteSpace(lot.GRADE)) ?
                             lot.GRADE.Trim().ToUpper() : "-";
                    inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                    inst.NETWEIGHT = lot.NETWEIGHT;
                    inst.PEINSPECTIONLOT = lot.PEINSPECTIONLOT;
                    inst.DEFECTID = lot.DEFECTID;

                    _defectId = inst.DEFECTID;
                    _testId = lot.TESTRECORDID;

                    if (UseShiftRemark == true)
                    {
                        inst.SHIFT_ID = lot.SHIFT_ID;
                        inst.SHIFT_REMARK = lot.SHIFT_REMARK;
                        inst.REMARK = lot.SHIFT_REMARK;
                    }
                    else
                    {
                        inst.REMARK = lot.REMARK;
                    }

                    inst.ATTACHID = lot.ATTACHID;
                    inst.TESTRECORDID = lot.TESTRECORDID;
                    inst.INSPECTEDBY = lot.INSPECTEDBY;
                    inst.MCNO = lot.MCNO;
                    inst.FINISHFLAG = lot.FINISHFLAG;
                    inst.SUSPENDDATE = lot.SUSPENDDATE;
                    inst.INSPECTIONID = lot.INSPECTIONID;
                    inst.RETEST = lot.RETEST;
                    inst.PREITEMCODE = lot.PREITEMCODE;

                    inst.PRODUCTNAME = lot.PRODUCTNAME;
                    inst.MCNAME = lot.MCNAME;
                    inst.CUSTOMERNAME = lot.CUSTOMERNAME;
                    inst.DEFECTFILENAME = lot.DEFECTFILENAME;
                    inst.PARTNO = lot.PARTNO;

                    //New 23/8/22
                    inst.CONFIRMSTARTLENGTH = lot.CONFIRMSTARTLENGTH;
                    inst.CONFIRMSTDLENGTH = lot.CONFIRMSTDLENGTH;

                    //เอาออกก่อนยังไม่ให้ใช้
                    //New 18/10/22
                    //inst.RESETSTARTLENGTH = lot.RESETSTARTLENGTH;

                    results.Add(inst);

                    #endregion

                    if (results.Count > 0)
                    {
                        bool ChkDefect100 = false;

                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                        Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds4 = new Microsoft.Reporting.WinForms.ReportDataSource();

                        #region InspectionTestHistoryItem

                        List<InspectionTestHistoryItem> lots2 =
                            InspectionDataService.Instance.GetTestHistoryList(insLotNo, _testId);

                        List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList> resultsCordList = new List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList>();

                        if (null != lots2 && lots2.Count > 0)
                        {
                            for (int i = 0; i < lots2.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInstestreCordList instCordList = new DataControl.ClassData.tInspectionClassData.ListInstestreCordList();

                                // Old
                                //instCordList.STDLength = lots2[i].STDLength;
                                //instCordList.ActualLength = lots2[i].ActualLength;

                                if (lots2[i].STDLength != "")
                                    instCordList.STDLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].STDLength), 1).ToString();

                                if (lots2[i].ActualLength != "")
                                    instCordList.ActualLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].ActualLength), 1).ToString();

                                instCordList.DensityW = lots2[i].DensityW;
                                instCordList.DensityF = lots2[i].DensityF;
                                instCordList.WidthAll = lots2[i].WidthAll;
                                instCordList.WidthPin = lots2[i].WidthPin;
                                instCordList.WidthCoat = lots2[i].WidthCoat;
                                instCordList.TrimL = lots2[i].TrimL;
                                instCordList.TrimR = lots2[i].TrimR;
                                instCordList.FloppyL = lots2[i].FloppyL;
                                instCordList.FloppyR = lots2[i].FloppyR;
                                instCordList.UnwinderSet = lots2[i].UnwinderSet;
                                instCordList.UnwinderActual = lots2[i].UnwinderActual;
                                instCordList.WinderSet = lots2[i].WinderSet;
                                instCordList.WinderActual = lots2[i].WinderActual;

                                resultsCordList.Add(instCordList);
                            }
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }
                        else
                        {
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }

                        #endregion

                        #region InspectionDefectItem

                        List<InspectionDefectItem> lots3 =
                            InspectionDataService.Instance.ins_GetDefectListReport(insLotNo, _defectId);

                        List<DataControl.ClassData.tInspectionClassData.ListInsdefectList> resultsInsdefectList = new List<DataControl.ClassData.tInspectionClassData.ListInsdefectList>();

                        if (null != lots3 && lots3.Count > 0)
                        {

                            for (int i = 0; i < lots3.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInsdefectList instInsdefectList = new DataControl.ClassData.tInspectionClassData.ListInsdefectList();

                                instInsdefectList.No = lots3[i].No;

                                if (lots3[i].Length != "")
                                    instInsdefectList.Length = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length), 1).ToString("#,##0.0");

                                if (lots3[i].Length2 != "")
                                    instInsdefectList.Length2 = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length2), 1).ToString("#,##0.0");

                                if (lots3[i].DefectLength != "")
                                {
                                    sumDefectLength += MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1);
                                    instInsdefectList.DefectLength = MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1).ToString("#,##0.0");
                                }

                                instInsdefectList.DefectCode = lots3[i].DefectCode;
                                instInsdefectList.Description = lots3[i].Description;
                                instInsdefectList.Position = lots3[i].Position;
                                instInsdefectList.DEFECTPOINT100 = lots3[i].DEFECTPOINT100;

                                if (instInsdefectList.DEFECTPOINT100 != null)
                                    ChkDefect100 = true;

                                resultsInsdefectList.Add(instInsdefectList);
                            }

                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }
                        else
                        {
                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }

                        #endregion

                        #region InspectionSumDefectItem

                        List<INS_ReportSumDefectData> lots4 =
                            InspectionDataService.Instance.GetINS_ReportSumDefect(_defectId, _itemCode);

                        List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList> resultsInsSumDefectList = new List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList>();

                        if (null != lots4 && lots4.Count > 0)
                        {
                            for (int i = 0; i < lots4.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList instInsSumDefectList = new DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList();

                                instInsSumDefectList.TotalPoint = lots4[i].TOTALPOINT;
                                instInsSumDefectList.ShortDefect = lots4[i].SHORTDEFECT;
                                instInsSumDefectList.LongDefect = lots4[i].LONGDEFECT;

                                if (sumDefectLength > 0)
                                    instInsSumDefectList.ComLongDefect = sumDefectLength;
                                else
                                    instInsSumDefectList.ComLongDefect = lots4[i].COMLONGDEFECT;

                                instInsSumDefectList.ComShortDefect = lots4[i].COMSHORTDEFECT;

                                resultsInsSumDefectList.Add(instInsSumDefectList);
                            }

                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }
                        else
                        {
                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }

                        #endregion

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.DataSources.Add(rds2);
                        this._reportViewer.LocalReport.DataSources.Add(rds3);
                        this._reportViewer.LocalReport.DataSources.Add(rds4);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepInspectionRemark.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;
                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        if (ChkDefect100 == true)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", "+"));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", ""));

                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepTestInspectionRemark
        private void RepTestInspectionRemark(string insLotNo, string grade, decimal? netLenge, decimal? grossLength, bool UseShiftRemark)
        {
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

            try
            {
                List<InspectionReportData> lots =
                       InspectionDataService.Instance.GetInspectionReportData(insLotNo);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    #region DataSet1

                    string _testId = string.Empty;
                    string _defectId = string.Empty;
                    string _itemCode = string.Empty;
                    decimal? sumDefectLength = 0;

                    InspectionReportData lot = lots[0];

                    List<DataControl.ClassData.tInspectionClassData.ListInspection> results = new List<DataControl.ClassData.tInspectionClassData.ListInspection>();

                    DataControl.ClassData.tInspectionClassData.ListInspection inst = new DataControl.ClassData.tInspectionClassData.ListInspection();
                    inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                    inst.ITEMCODE = (!string.IsNullOrWhiteSpace(lot.ITEMCODE)) ?
                       lot.ITEMCODE.Trim().ToUpper() : string.Empty;

                    _itemCode = inst.ITEMCODE;

                    inst.FINISHINGLOT = lot.FINISHINGLOT;
                    inst.STARTDATE = lot.STARTDATE;
                    inst.ENDDATE = lot.ENDDATE;

                    if (grossLength != null)
                        inst.GROSSLENGTH = MathEx.TruncateDecimal(grossLength.Value, 1);

                    if (netLenge != null)
                        inst.NETLENGTH = MathEx.TruncateDecimal(netLenge.Value, 1);

                    inst.CUSTOMERID = lot.CUSTOMERID;
                    inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                    inst.LOADINGTYPE = lot.LOADINGTYPE;

                    if (lot.CUSTOMERID != null)
                    {
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "09")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "06")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "11")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else if (lot.CUSTOMERID == "12")
                        {
                            inst.GrossNetLength = inst.NETLENGTH;
                        }
                        else
                        {
                            inst.GrossNetLength = inst.GROSSLENGTH;
                        }
                    }
                    else
                    {
                        inst.GrossNetLength = inst.GROSSLENGTH;
                    }

                    inst.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                    inst.GRADE = grade;
                    inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                    inst.NETWEIGHT = lot.NETWEIGHT;
                    inst.PEINSPECTIONLOT = lot.PEINSPECTIONLOT;
                    inst.DEFECTID = lot.DEFECTID;

                    _defectId = inst.DEFECTID;
                    _testId = lot.TESTRECORDID;

                    if (UseShiftRemark == true)
                    {
                        inst.SHIFT_ID = lot.SHIFT_ID;
                        inst.SHIFT_REMARK = lot.SHIFT_REMARK;
                        inst.REMARK = lot.SHIFT_REMARK;
                    }
                    else
                    {
                        inst.REMARK = lot.REMARK;
                    }

                    inst.ATTACHID = lot.ATTACHID;
                    inst.TESTRECORDID = lot.TESTRECORDID;
                    inst.INSPECTEDBY = lot.INSPECTEDBY;
                    inst.MCNO = lot.MCNO;
                    inst.FINISHFLAG = lot.FINISHFLAG;
                    inst.SUSPENDDATE = lot.SUSPENDDATE;
                    inst.INSPECTIONID = lot.INSPECTIONID;
                    inst.RETEST = lot.RETEST;
                    inst.PREITEMCODE = lot.PREITEMCODE;

                    inst.PRODUCTNAME = lot.PRODUCTNAME;
                    inst.MCNAME = lot.MCNAME;
                    inst.CUSTOMERNAME = lot.CUSTOMERNAME;
                    inst.DEFECTFILENAME = lot.DEFECTFILENAME;
                    inst.PARTNO = lot.PARTNO;

                    //New 23/8/22
                    inst.CONFIRMSTARTLENGTH = lot.CONFIRMSTARTLENGTH;
                    inst.CONFIRMSTDLENGTH = lot.CONFIRMSTDLENGTH;

                    //เอาออกก่อนยังไม่ให้ใช้
                    //New 18/10/22
                    //inst.RESETSTARTLENGTH = lot.RESETSTARTLENGTH;

                    results.Add(inst);

                    #endregion

                    if (results.Count > 0)
                    {
                        bool ChkDefect100 = false;

                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                        Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds3 = new Microsoft.Reporting.WinForms.ReportDataSource();
                        Microsoft.Reporting.WinForms.ReportDataSource rds4 = new Microsoft.Reporting.WinForms.ReportDataSource();

                        #region InspectionTestHistoryItem

                        List<InspectionTestHistoryItem> lots2 =
                            InspectionDataService.Instance.GetTestHistoryList(insLotNo, _testId);

                        List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList> resultsCordList = new List<DataControl.ClassData.tInspectionClassData.ListInstestreCordList>();

                        if (null != lots2 && lots2.Count > 0)
                        {
                            for (int i = 0; i < lots2.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInstestreCordList instCordList = new DataControl.ClassData.tInspectionClassData.ListInstestreCordList();

                                // Old
                                //instCordList.STDLength = lots2[i].STDLength;
                                //instCordList.ActualLength = lots2[i].ActualLength;

                                if (lots2[i].STDLength != "")
                                    instCordList.STDLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].STDLength), 1).ToString();

                                if (lots2[i].ActualLength != "")
                                    instCordList.ActualLength = MathEx.TruncateDecimal(decimal.Parse(lots2[i].ActualLength), 1).ToString();

                                instCordList.DensityW = lots2[i].DensityW;
                                instCordList.DensityF = lots2[i].DensityF;
                                instCordList.WidthAll = lots2[i].WidthAll;
                                instCordList.WidthPin = lots2[i].WidthPin;
                                instCordList.WidthCoat = lots2[i].WidthCoat;
                                instCordList.TrimL = lots2[i].TrimL;
                                instCordList.TrimR = lots2[i].TrimR;
                                instCordList.FloppyL = lots2[i].FloppyL;
                                instCordList.FloppyR = lots2[i].FloppyR;
                                instCordList.UnwinderSet = lots2[i].UnwinderSet;
                                instCordList.UnwinderActual = lots2[i].UnwinderActual;
                                instCordList.WinderSet = lots2[i].WinderSet;
                                instCordList.WinderActual = lots2[i].WinderActual;

                                resultsCordList.Add(instCordList);
                            }
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }
                        else
                        {
                            rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCordList);
                        }

                        #endregion

                        #region InspectionDefectItem

                        List<InspectionDefectItem> lots3 =
                            InspectionDataService.Instance.ins_GetDefectListReport(insLotNo, _defectId);

                        List<DataControl.ClassData.tInspectionClassData.ListInsdefectList> resultsInsdefectList = new List<DataControl.ClassData.tInspectionClassData.ListInsdefectList>();

                        if (null != lots3 && lots3.Count > 0)
                        {

                            for (int i = 0; i < lots3.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListInsdefectList instInsdefectList = new DataControl.ClassData.tInspectionClassData.ListInsdefectList();

                                instInsdefectList.No = lots3[i].No;

                                if (lots3[i].Length != "")
                                    instInsdefectList.Length = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length), 1).ToString("#,##0.0");

                                if (lots3[i].Length2 != "")
                                    instInsdefectList.Length2 = MathEx.TruncateDecimal(decimal.Parse(lots3[i].Length2), 1).ToString("#,##0.0");

                                if (lots3[i].DefectLength != "")
                                {
                                    sumDefectLength += MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1);
                                    instInsdefectList.DefectLength = MathEx.TruncateDecimal(decimal.Parse(lots3[i].DefectLength), 1).ToString("#,##0.0");
                                }

                                instInsdefectList.DefectCode = lots3[i].DefectCode;
                                instInsdefectList.Description = lots3[i].Description;
                                instInsdefectList.Position = lots3[i].Position;
                                instInsdefectList.DEFECTPOINT100 = lots3[i].DEFECTPOINT100;

                                if (instInsdefectList.DEFECTPOINT100 != null)
                                    ChkDefect100 = true;

                                resultsInsdefectList.Add(instInsdefectList);
                            }

                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }
                        else
                        {
                            rds3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsInsdefectList);
                        }

                        #endregion

                        #region InspectionSumDefectItem

                        List<INS_ReportSumDefectData> lots4 =
                            InspectionDataService.Instance.GetINS_ReportSumDefect(_defectId, _itemCode);

                        List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList> resultsInsSumDefectList = new List<DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList>();

                        if (null != lots4 && lots4.Count > 0)
                        {
                            for (int i = 0; i < lots4.Count; i++)
                            {
                                DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList instInsSumDefectList = new DataControl.ClassData.tInspectionClassData.ListINS_ReportSumDefectList();

                                instInsSumDefectList.TotalPoint = lots4[i].TOTALPOINT;
                                instInsSumDefectList.ShortDefect = lots4[i].SHORTDEFECT;
                                instInsSumDefectList.LongDefect = lots4[i].LONGDEFECT;

                                if (sumDefectLength > 0)
                                    instInsSumDefectList.ComLongDefect = sumDefectLength;
                                else
                                    instInsSumDefectList.ComLongDefect = lots4[i].COMLONGDEFECT;

                                instInsSumDefectList.ComShortDefect = lots4[i].COMSHORTDEFECT;

                                resultsInsSumDefectList.Add(instInsSumDefectList);
                            }

                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }
                        else
                        {
                            rds4 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsInsSumDefectList);
                        }

                        #endregion

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.DataSources.Add(rds2);
                        this._reportViewer.LocalReport.DataSources.Add(rds3);
                        this._reportViewer.LocalReport.DataSources.Add(rds4);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepInspectionRemark.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;
                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        if (ChkDefect100 == true)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", "+"));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ChkDefect100", ""));

                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepScouring1
        private void RepScouring1(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETSCOURINGREPORTDATA> lots = CoatingDataService.Instance
                .FINISHING_GETSCOURINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListScouring> results = new List<DataControl.ClassData.FinishingClassData.ListScouring>();

                        DataControl.ClassData.FinishingClassData.ListScouring inst = new DataControl.ClassData.FinishingClassData.ListScouring();

                        #region ListScouring

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.MAINFRAMEWIDTH = lots[0].MAINFRAMEWIDTH;
                        inst.WIDTH_BE = lots[0].WIDTH_BE;
                        inst.WIDTH_AF = lots[0].WIDTH_AF;
                        inst.PIN2PIN = lots[0].PIN2PIN;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        #endregion

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETSCOURINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListScouringCondition> resultsScouringCondition = new List<DataControl.ClassData.FinishingClassData.ListScouringCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListScouringCondition instScouringCondition = new DataControl.ClassData.FinishingClassData.ListScouringCondition();

                                #region Old

                                //instScouringCondition.ITM_CODE = lots2[0].ITM_CODE;
                                //instScouringCondition.SATURATOR_CHEM = lots2[0].SATURATOR_CHEM;
                                //instScouringCondition.SATURATOR_CHEM_MARGIN = lots2[0].SATURATOR_CHEM_MARGIN;
                                //instScouringCondition.WASHING1 = lots2[0].WASHING1;
                                //instScouringCondition.WASHING1_MARGIN = lots2[0].WASHING1_MARGIN;
                                //instScouringCondition.WASHING2 = lots2[0].WASHING2;
                                //instScouringCondition.WASHING2_MARGIN = lots2[0].WASHING2_MARGIN;
                                //instScouringCondition.HOTFLUE = lots2[0].HOTFLUE;
                                //instScouringCondition.HOTFLUE_MARGIN = lots2[0].HOTFLUE_MARGIN;
                                //instScouringCondition.ROOMTEMP = lots2[0].ROOMTEMP;
                                //instScouringCondition.ROOMTEMP_MARGIN = lots2[0].ROOMTEMP_MARGIN;
                                //instScouringCondition.SPEED = lots2[0].SPEED;
                                //instScouringCondition.SPEED_MARGIN = lots2[0].SPEED_MARGIN;
                                //instScouringCondition.MAINFRAMEWIDTH = lots2[0].MAINFRAMEWIDTH;
                                //instScouringCondition.MAINFRAMEWIDTH_MARGIN = lots2[0].MAINFRAMEWIDTH_MARGIN;
                                //instScouringCondition.WIDTH_BE = lots2[0].WIDTH_BE;
                                //instScouringCondition.WIDTH_BE_MARGIN = lots2[0].WIDTH_BE_MARGIN;
                                //instScouringCondition.WIDTH_AF = lots2[0].WIDTH_AF;
                                //instScouringCondition.WIDTH_AF_MARGIN = lots2[0].WIDTH_AF_MARGIN;
                                //instScouringCondition.DENSITY_AF = lots2[0].DENSITY_AF;
                                //instScouringCondition.DENSITY_MARGIN = lots2[0].DENSITY_MARGIN;
                                //instScouringCondition.SCOURINGNO = lots2[0].SCOURINGNO;
                                //instScouringCondition.NIPCHEMICAL = lots2[0].NIPCHEMICAL;
                                //instScouringCondition.NIPROLLWASHER1 = lots2[0].NIPROLLWASHER1;
                                //instScouringCondition.NIPROLLWASHER2 = lots2[0].NIPROLLWASHER2;
                                //instScouringCondition.PIN2PIN = lots2[0].PIN2PIN;
                                //instScouringCondition.PIN2PIN_MARGIN = lots2[0].PIN2PIN_MARGIN;

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instScouringCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instScouringCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instScouringCondition.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instScouringCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region MAINFRAMEWIDTH

                                if (lots2[0].MAINFRAMEWIDTH != null && lots2[0].MAINFRAMEWIDTH_MARGIN != null)
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = (lots2[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##") + " ± " + lots2[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = "";
                                }

                                #endregion

                                #region WIDTH_BE

                                if (lots2[0].WIDTH_BE != null && lots2[0].WIDTH_BE_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_BESpecification = (lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_BESpecification = ("( " + lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_BESpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF

                                if (lots2[0].WIDTH_AF != null && lots2[0].WIDTH_AF_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_AFSpecification = (lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_AFSpecification = ("( " + lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_AFSpecification = "";
                                }

                                #endregion

                                #region PIN2PIN

                                if (lots2[0].PIN2PIN != null && lots2[0].PIN2PIN_MARGIN != null)
                                {
                                    instScouringCondition.PIN2PINSpecification = (lots2[0].PIN2PIN.Value.ToString("#,##0.##") + " ± " + lots2[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.PIN2PINSpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instScouringCondition.StrROOMTEMP = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.StrROOMTEMP = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instScouringCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instScouringCondition.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instScouringCondition.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instScouringCondition.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsScouringCondition.Add(instScouringCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepScouring1.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepScouring2
        private void RepScouring2(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETSCOURINGREPORTDATA> lots = CoatingDataService.Instance
              .FINISHING_GETSCOURINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListScouring> results = new List<DataControl.ClassData.FinishingClassData.ListScouring>();

                        DataControl.ClassData.FinishingClassData.ListScouring inst = new DataControl.ClassData.FinishingClassData.ListScouring();

                        #region ListScouring

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.MAINFRAMEWIDTH = lots[0].MAINFRAMEWIDTH;
                        inst.WIDTH_BE = lots[0].WIDTH_BE;
                        inst.WIDTH_AF = lots[0].WIDTH_AF;
                        inst.PIN2PIN = lots[0].PIN2PIN;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        #endregion

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETSCOURINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListScouringCondition> resultsScouringCondition = new List<DataControl.ClassData.FinishingClassData.ListScouringCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListScouringCondition instScouringCondition = new DataControl.ClassData.FinishingClassData.ListScouringCondition();

                                #region Old

                                //instScouringCondition.ITM_CODE = lots2[0].ITM_CODE;
                                //instScouringCondition.SATURATOR_CHEM = lots2[0].SATURATOR_CHEM;
                                //instScouringCondition.SATURATOR_CHEM_MARGIN = lots2[0].SATURATOR_CHEM_MARGIN;
                                //instScouringCondition.WASHING1 = lots2[0].WASHING1;
                                //instScouringCondition.WASHING1_MARGIN = lots2[0].WASHING1_MARGIN;
                                //instScouringCondition.WASHING2 = lots2[0].WASHING2;
                                //instScouringCondition.WASHING2_MARGIN = lots2[0].WASHING2_MARGIN;
                                //instScouringCondition.HOTFLUE = lots2[0].HOTFLUE;
                                //instScouringCondition.HOTFLUE_MARGIN = lots2[0].HOTFLUE_MARGIN;
                                //instScouringCondition.ROOMTEMP = lots2[0].ROOMTEMP;
                                //instScouringCondition.ROOMTEMP_MARGIN = lots2[0].ROOMTEMP_MARGIN;
                                //instScouringCondition.SPEED = lots2[0].SPEED;
                                //instScouringCondition.SPEED_MARGIN = lots2[0].SPEED_MARGIN;
                                //instScouringCondition.MAINFRAMEWIDTH = lots2[0].MAINFRAMEWIDTH;
                                //instScouringCondition.MAINFRAMEWIDTH_MARGIN = lots2[0].MAINFRAMEWIDTH_MARGIN;
                                //instScouringCondition.WIDTH_BE = lots2[0].WIDTH_BE;
                                //instScouringCondition.WIDTH_BE_MARGIN = lots2[0].WIDTH_BE_MARGIN;
                                //instScouringCondition.WIDTH_AF = lots2[0].WIDTH_AF;
                                //instScouringCondition.WIDTH_AF_MARGIN = lots2[0].WIDTH_AF_MARGIN;
                                //instScouringCondition.DENSITY_AF = lots2[0].DENSITY_AF;
                                //instScouringCondition.DENSITY_MARGIN = lots2[0].DENSITY_MARGIN;
                                //instScouringCondition.SCOURINGNO = lots2[0].SCOURINGNO;
                                //instScouringCondition.NIPCHEMICAL = lots2[0].NIPCHEMICAL;
                                //instScouringCondition.NIPROLLWASHER1 = lots2[0].NIPROLLWASHER1;
                                //instScouringCondition.NIPROLLWASHER2 = lots2[0].NIPROLLWASHER2;
                                //instScouringCondition.PIN2PIN = lots2[0].PIN2PIN;
                                //instScouringCondition.PIN2PIN_MARGIN = lots2[0].PIN2PIN_MARGIN;

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instScouringCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instScouringCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instScouringCondition.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instScouringCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region MAINFRAMEWIDTH

                                if (lots2[0].MAINFRAMEWIDTH != null && lots2[0].MAINFRAMEWIDTH_MARGIN != null)
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = (lots2[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##") + " ± " + lots2[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = "";
                                }

                                #endregion

                                #region WIDTH_BE

                                if (lots2[0].WIDTH_BE != null && lots2[0].WIDTH_BE_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_BESpecification = (lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_BESpecification = ("( "+lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_BESpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF

                                if (lots2[0].WIDTH_AF != null && lots2[0].WIDTH_AF_MARGIN != null)
                                {
                                    instScouringCondition.WIDTH_AFSpecification = (lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_AFSpecification = "";
                                }

                                #endregion

                                #region PIN2PIN

                                if (lots2[0].PIN2PIN != null && lots2[0].PIN2PIN_MARGIN != null)
                                {
                                    instScouringCondition.PIN2PINSpecification = (lots2[0].PIN2PIN.Value.ToString("#,##0.##") + " ± " + lots2[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.PIN2PINSpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instScouringCondition.StrROOMTEMP = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.StrROOMTEMP = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instScouringCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instScouringCondition.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instScouringCondition.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instScouringCondition.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsScouringCondition.Add(instScouringCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepScouring2.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepScouringDryer
        private void RepScouringDryer(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETDRYERREPORTDATA> lots = CoatingDataService.Instance
              .FINISHING_GETDRYERREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListDryer> results = new List<DataControl.ClassData.FinishingClassData.ListDryer>();

                        DataControl.ClassData.FinishingClassData.ListDryer inst = new DataControl.ClassData.FinishingClassData.ListDryer();

                        #region ListDryer

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;

                        inst.WIDTH_BE_HEAT = lots[0].WIDTH_BE_HEAT;
                        inst.ACCPRESURE = lots[0].ACCPRESURE;
                        inst.ASSTENSION = lots[0].ASSTENSION;
                        inst.ACCARIDENSER = lots[0].ACCARIDENSER;
                        inst.CHIFROT = lots[0].CHIFROT;
                        inst.CHIREAR = lots[0].CHIREAR;
                        inst.DRYERTEMP1_PV = lots[0].DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = lots[0].DRYERTEMP1_SP;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.SPEED_SP = lots[0].SPEED_SP;
                        inst.STEAMPRESSURE = lots[0].STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = lots[0].DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = lots[0].EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = lots[0].WIDTH_AF_HEAT;

                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        #endregion

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETDRYERCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETDRYERCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListDryerCondition> resultsDryerCondition = new List<DataControl.ClassData.FinishingClassData.ListDryerCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListDryerCondition instDryerCondition = new DataControl.ClassData.FinishingClassData.ListDryerCondition();

                                #region WIDTH_BE_HEAT

                                if (lots2[0].WIDTH_BE_HEAT_MAX != null && lots2[0].WIDTH_BE_HEAT_MIN != null)
                                {
                                    //instDryerCondition.WIDTH_BE_HEATSpecification = (lots2[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##"));
                                    instDryerCondition.WIDTH_BE_HEATSpecification = ("( " + lots2[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = "";
                                }

                                #endregion

                                #region ACCPRESURE

                                if (lots2[0].ACCPRESURE != null)
                                {
                                    instDryerCondition.ACCPRESURESpecification = lots2[0].ACCPRESURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCPRESURESpecification = "";
                                }

                                #endregion

                                #region ASSTENSION

                                if (lots2[0].ASSTENSION != null)
                                {
                                    instDryerCondition.ASSTENSIONSpecification = lots2[0].ASSTENSION.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ASSTENSIONSpecification = "";
                                }

                                #endregion

                                #region ACCARIDENSER

                                if (lots2[0].ACCARIDENSER != null)
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = lots2[0].ACCARIDENSER.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = "";
                                }

                                #endregion

                                #region CHIFROT

                                if (lots2[0].CHIFROT != null)
                                {
                                    instDryerCondition.CHIFROTSpecification = lots2[0].CHIFROT.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIFROTSpecification = "";
                                }

                                #endregion

                                #region CHIREAR

                                if (lots2[0].CHIREAR != null)
                                {
                                    instDryerCondition.CHIREARSpecification = lots2[0].CHIREAR.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIREARSpecification = "";
                                }

                                #endregion

                                #region DRYERTEMP1

                                if (lots2[0].DRYERTEMP1 != null && lots2[0].DRYERTEMP1_MARGIN != null)
                                {
                                    instDryerCondition.DRYERTEMP1Specification = (lots2[0].DRYERTEMP1.Value.ToString("#,##0.##") + " ± " + lots2[0].DRYERTEMP1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.DRYERTEMP1Specification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instDryerCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region STEAMPRESSURE

                                if (lots2[0].STEAMPRESSURE != null)
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = "> " + lots2[0].STEAMPRESSURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = "";
                                }

                                #endregion

                                #region DRYERUPCIRCUFAN

                                if (lots2[0].DRYERUPCIRCUFAN != null)
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = lots2[0].DRYERUPCIRCUFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = "";
                                }

                                #endregion

                                #region EXHAUSTFAN

                                if (lots2[0].EXHAUSTFAN != null)
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = lots2[0].EXHAUSTFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF_HEAT

                                if (lots2[0].WIDTH_AF_HEAT != null && lots2[0].WIDTH_AF_HEAT_MARGIN != null)
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = (lots2[0].WIDTH_AF_HEAT.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_HEAT_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instDryerCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instDryerCondition.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instDryerCondition.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsDryerCondition.Add(instDryerCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepScouringDryer.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating1
        private void RepCoating1(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETCOATINGREPORTDATA> lots = CoatingDataService.Instance
                .FINISHING_GETCOATINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListCoating> results = new List<DataControl.ClassData.FinishingClassData.ListCoating>();

                        DataControl.ClassData.FinishingClassData.ListCoating inst = new DataControl.ClassData.FinishingClassData.ListCoating();

                        #region ListCoating

                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = lots[0].FINISHINGCUSTOMER;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.PRODUCTTYPEID = lots[0].PRODUCTTYPEID;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.MCNO = lots[0].MCNO;
                        inst.STATUSFLAG = lots[0].STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.BE_COATWIDTH = lots[0].BE_COATWIDTH;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.TEMP9_PV = lots[0].TEMP9_PV;
                        inst.TEMP10_PV = lots[0].TEMP10_PV;
                        inst.FANRPM = lots[0].FANRPM;
                        inst.EXFAN_FRONT_BACK = lots[0].EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = lots[0].EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = lots[0].ANGLEKNIFE;
                        inst.BLADENO = lots[0].BLADENO;
                        inst.BLADEDIRECTION = lots[0].BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = lots[0].CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = lots[0].OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = lots[0].FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = lots[0].FRAMEWIDTH_TENTER;
                        inst.PATHLINE = lots[0].PATHLINE;
                        inst.FEEDIN = lots[0].FEEDIN;
                        inst.OVERFEED = lots[0].OVERFEED;
                        inst.SPEED_PV = lots[0].SPEED_PV;

                        inst.WIDTHCOAT = lots[0].WIDTHCOAT;
                        inst.WIDTHCOATALL = lots[0].WIDTHCOATALL;
                        inst.SILICONE_A = lots[0].SILICONE_A;
                        inst.SILICONE_B = lots[0].SILICONE_B;
                        inst.COATINGWEIGTH_L = lots[0].COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = lots[0].COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = lots[0].COATINGWEIGTH_R;

                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.SAMPLINGID = lots[0].SAMPLINGID;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.PARTNO = lots[0].PARTNO;
                        inst.FINISHLENGTH = lots[0].FINISHLENGTH;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.TEMP9_MIN = lots[0].TEMP9_MIN;
                        inst.TEMP9_MAX = lots[0].TEMP9_MAX;
                        inst.TEMP10_MIN = lots[0].TEMP10_MIN;
                        inst.TEMP10_MAX = lots[0].TEMP10_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;
                        inst.TENSIONUP_MIN = lots[0].TENSIONUP_MIN;
                        inst.TENSIONUP_MAX = lots[0].TENSIONUP_MAX;
                        inst.TENSIONDOWN_MIN = lots[0].TENSIONDOWN_MIN;
                        inst.TENSIONDOWN_MAX = lots[0].TENSIONDOWN_MAX;

                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETCOATINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETCOATINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListCoatingCondition> resultsCoating = new List<DataControl.ClassData.FinishingClassData.ListCoatingCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListCoatingCondition instCoating = new DataControl.ClassData.FinishingClassData.ListCoatingCondition();

                                #region BE_COATWIDTH

                                if (lots2[0].BE_COATWIDTHMAX != null && lots2[0].BE_COATWIDTHMIN != null)
                                {
                                    //instCoating.BE_COATWIDTHSpecification = (lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " - " + lots2[0].BE_COATWIDTHMIN.Value.ToString("#,##0.##"));
                                    instCoating.BE_COATWIDTHSpecification = ("( " + lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.BE_COATWIDTHSpecification = "";
                                }

                                #endregion

                                #region FANRPM

                                if (lots2[0].FANRPM != null && lots2[0].FANRPM_MARGIN != null)
                                {
                                    instCoating.Fan110Specification = (lots2[0].FANRPM.Value.ToString("#,##0.##") + " ± " + lots2[0].FANRPM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.Fan110Specification = "";
                                }

                                #endregion

                                #region EXFAN_FRONT_BACK

                                if (lots2[0].EXFAN_FRONT_BACK != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN15Specification = (lots2[0].EXFAN_FRONT_BACK.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN15Specification = "";
                                }

                                #endregion

                                #region EXFAN_MIDDLE

                                if (lots2[0].EXFAN_MIDDLE != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN234Specification = (lots2[0].EXFAN_MIDDLE.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN234Specification = "";
                                }

                                #endregion

                                #region ANGLEKNIFE

                                if (lots2[0].ANGLEKNIFE != null)
                                    instCoating.ANGLEKNIFESpecification = lots2[0].ANGLEKNIFE.Value.ToString("#,##0.##");
                                else
                                    instCoating.ANGLEKNIFESpecification = "";

                                #endregion

                                instCoating.BLADENO = lots2[0].BLADENO;
                                instCoating.BLADEDIRECTION = lots2[0].BLADEDIRECTION;

                                instCoating.PATHLINE = lots2[0].PATHLINE;

                                #region FEEDIN_MAX

                                if (lots2[0].FEEDIN_MAX != null && lots2[0].FEEDIN_MIN != null)
                                {
                                    //instCoating.FeedInSpecification = (lots2[0].FEEDIN_MAX.Value.ToString("#,##0.##") + " - " + lots2[0].FEEDIN_MIN.Value.ToString("#,##0.##"));
                                    //instCoating.FeedInSpecification = (lots2[0].FEEDIN_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].FEEDIN_MAX.Value.ToString("#,##0.##"));
                                    instCoating.FeedInSpecification = ("( " + lots2[0].FEEDIN_MAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.FeedInSpecification = "";
                                }

                                #endregion

                                #region TENSION_DOWN

                                if (lots2[0].TENSION_DOWN != null && lots2[0].TENSION_DOWN_MARGIN != null)
                                {
                                    //instCoating.TENSION_DOWNSpecification = (lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " +/- " + lots2[0].TENSION_DOWN_MARGIN.Value.ToString("#,##0.##"));
                                    instCoating.TENSION_DOWNSpecification = ("( "+lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " )"); 
                                }
                                else
                                {
                                    instCoating.TENSION_DOWNSpecification = "";
                                }

                                #endregion

                                #region TENSION_UP

                                if (lots2[0].TENSION_UP != null && lots2[0].TENSION_UP_MARGIN != null)
                                {
                                    instCoating.TENSION_UPSpecification = (lots2[0].TENSION_UP.Value.ToString("#,##0.##") + " +/- " + lots2[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.TENSION_UPSpecification = "";
                                }

                                if (lots2[0].TENSION_UP != null)
                                    instCoating.TENSION_UP = lots2[0].TENSION_UP;

                                #endregion

                                #region FRAMEWIDTH_FORN

                                if (lots2[0].FRAMEWIDTH_FORN != null)
                                    instCoating.FRAMEWIDTH_FORN = lots2[0].FRAMEWIDTH_FORN;

                                #endregion

                                #region FRAMEWIDTH_TENTER

                                if (lots2[0].FRAMEWIDTH_TENTER != null)
                                    instCoating.FRAMEWIDTH_TENTER = lots2[0].FRAMEWIDTH_TENTER;

                                #endregion

                                instCoating.OVERFEED = lots2[0].OVERFEED;

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instCoating.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SPEEDSpecification = "";
                                }

                                #endregion

                                #region WIDTHCOAT

                                if (lots2[0].WIDTHCOAT != null)
                                    instCoating.WIDTHCOATSpecification = "> " + lots2[0].WIDTHCOAT.Value.ToString("#,##0.##");
                                else
                                    instCoating.WIDTHCOATSpecification = "";

                                #endregion

                                #region WIDTHCOATALL

                                if (lots2[0].WIDTHCOATALL_MAX != null && lots2[0].WIDTHCOATALL_MIN != null)
                                {
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##"));
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##"));
                                    instCoating.WIDTHCOATALLSpecification = ("( " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.WIDTHCOATALLSpecification = "";
                                }

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instCoating.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instCoating.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instCoating.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instCoating.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instCoating.ROOMTEMPSpecification = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.ROOMTEMPSpecification = "";
                                }

                                #endregion

                                instCoating.RATIOSILICONE = lots2[0].RATIOSILICONE;

                                #region COATINGWEIGTH_MIN

                                if (lots2[0].COATINGWEIGTH_MIN != null && lots2[0].COATINGWEIGTH_MAX != null)
                                {
                                    //instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##"));
                                    instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##") + " +/- " + lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.COATINGWEIGTHSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN

                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instCoating.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instCoating.HUMIDSpecification = "";
                                }

                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                    //instCoating.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instCoating.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsCoating.Add(instCoating);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating1.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating2
        private void RepCoating2(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETCOATINGREPORTDATA> lots = CoatingDataService.Instance
                .FINISHING_GETCOATINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListCoating> results = new List<DataControl.ClassData.FinishingClassData.ListCoating>();

                        DataControl.ClassData.FinishingClassData.ListCoating inst = new DataControl.ClassData.FinishingClassData.ListCoating();

                        #region ListCoating

                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = lots[0].FINISHINGCUSTOMER;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.PRODUCTTYPEID = lots[0].PRODUCTTYPEID;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.MCNO = lots[0].MCNO;
                        inst.STATUSFLAG = lots[0].STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.BE_COATWIDTH = lots[0].BE_COATWIDTH;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.TEMP9_PV = lots[0].TEMP9_PV;
                        inst.TEMP10_PV = lots[0].TEMP10_PV;
                        inst.FANRPM = lots[0].FANRPM;
                        inst.EXFAN_FRONT_BACK = lots[0].EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = lots[0].EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = lots[0].ANGLEKNIFE;
                        inst.BLADENO = lots[0].BLADENO;
                        inst.BLADEDIRECTION = lots[0].BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = lots[0].CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = lots[0].OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = lots[0].FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = lots[0].FRAMEWIDTH_TENTER;
                        inst.PATHLINE = lots[0].PATHLINE;
                        inst.FEEDIN = lots[0].FEEDIN;
                        inst.OVERFEED = lots[0].OVERFEED;
                        inst.SPEED_PV = lots[0].SPEED_PV;

                        inst.WIDTHCOAT = lots[0].WIDTHCOAT;
                        inst.WIDTHCOATALL = lots[0].WIDTHCOATALL;
                        inst.SILICONE_A = lots[0].SILICONE_A;
                        inst.SILICONE_B = lots[0].SILICONE_B;
                        inst.COATINGWEIGTH_L = lots[0].COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = lots[0].COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = lots[0].COATINGWEIGTH_R;

                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.SAMPLINGID = lots[0].SAMPLINGID;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.PARTNO = lots[0].PARTNO;
                        inst.FINISHLENGTH = lots[0].FINISHLENGTH;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.TEMP9_MIN = lots[0].TEMP9_MIN;
                        inst.TEMP9_MAX = lots[0].TEMP9_MAX;
                        inst.TEMP10_MIN = lots[0].TEMP10_MIN;
                        inst.TEMP10_MAX = lots[0].TEMP10_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;
                        inst.TENSIONUP_MIN = lots[0].TENSIONUP_MIN;
                        inst.TENSIONUP_MAX = lots[0].TENSIONUP_MAX;
                        inst.TENSIONDOWN_MIN = lots[0].TENSIONDOWN_MIN;
                        inst.TENSIONDOWN_MAX = lots[0].TENSIONDOWN_MAX;

                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETCOATINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETCOATINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListCoatingCondition> resultsCoating = new List<DataControl.ClassData.FinishingClassData.ListCoatingCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListCoatingCondition instCoating = new DataControl.ClassData.FinishingClassData.ListCoatingCondition();

                                #region BE_COATWIDTH

                                if (lots2[0].BE_COATWIDTHMAX != null && lots2[0].BE_COATWIDTHMIN != null)
                                {
                                    //instCoating.BE_COATWIDTHSpecification = (lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " - " + lots2[0].BE_COATWIDTHMIN.Value.ToString("#,##0.##"));
                                    instCoating.BE_COATWIDTHSpecification = ("( " + lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.BE_COATWIDTHSpecification = "";
                                }

                                #endregion

                                #region FANRPM

                                if (lots2[0].FANRPM != null && lots2[0].FANRPM_MARGIN != null)
                                {
                                    instCoating.Fan110Specification = (lots2[0].FANRPM.Value.ToString("#,##0.##") + " ± " + lots2[0].FANRPM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.Fan110Specification = "";
                                }

                                #endregion

                                #region EXFAN_FRONT_BACK

                                if (lots2[0].EXFAN_FRONT_BACK != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN1_6Specification = (lots2[0].EXFAN_FRONT_BACK.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN1_6Specification = "";
                                }

                                #endregion

                                #region EXFAN_MIDDLE

                                if (lots2[0].EXFAN_MIDDLE != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN2_5Specification = (lots2[0].EXFAN_MIDDLE.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN2_5Specification = "";
                                }

                                #endregion

                                #region ANGLEKNIFE

                                if (lots2[0].ANGLEKNIFE != null)
                                    instCoating.ANGLEKNIFESpecification = lots2[0].ANGLEKNIFE.Value.ToString("#,##0.##");
                                else
                                    instCoating.ANGLEKNIFESpecification = "";

                                #endregion

                                instCoating.BLADENO = lots2[0].BLADENO;

                                instCoating.BLADEDIRECTION = lots2[0].BLADEDIRECTION;


                                #region TENSION_UP

                                if (lots2[0].TENSION_UP != null && lots2[0].TENSION_UP_MARGIN != null)
                                {
                                    //instCoating.TENSION_UPSpecification = (lots2[0].TENSION_UP.Value.ToString("#,##0.##") + " ± " + lots2[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                                    instCoating.TENSION_UPSpecification = (lots2[0].TENSION_UP.Value.ToString("#,##0.##") + " +/- " + lots2[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.TENSION_UPSpecification = "";
                                }

                                #endregion

                                #region TENSION_DOWN

                                if (lots2[0].TENSION_DOWN != null && lots2[0].TENSION_DOWN_MARGIN != null)
                                {
                                    //instCoating.TENSION_DOWNSpecification = (lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " ± " + lots2[0].TENSION_DOWN_MARGIN.Value.ToString("#,##0.##"));
                                    instCoating.TENSION_DOWNSpecification = ("( " + lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " )"); 
                                }
                                else
                                {
                                    instCoating.TENSION_DOWNSpecification = "";
                                }

                                #endregion

                                #region FRAMEWIDTH_FORN

                                if (lots2[0].FRAMEWIDTH_FORN != null)
                                    instCoating.FRAMEWIDTH_FORN = lots2[0].FRAMEWIDTH_FORN;
                                else
                                    instCoating.FRAMEWIDTH_FORN = 0;

                                #endregion

                                #region FRAMEWIDTH_TENTER

                                if (lots2[0].FRAMEWIDTH_TENTER != null)
                                    instCoating.FRAMEWIDTH_TENTER = lots2[0].FRAMEWIDTH_TENTER;
                                else
                                    instCoating.FRAMEWIDTH_TENTER = 0;

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instCoating.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SPEEDSpecification = "";
                                }

                                #endregion

                                #region WIDTHCOAT

                                if (lots2[0].WIDTHCOAT != null)
                                    instCoating.WIDTHCOATSpecification = "> " + lots2[0].WIDTHCOAT.Value.ToString("#,##0.##");
                                else
                                    instCoating.WIDTHCOATSpecification = "";

                                #endregion

                                #region WIDTHCOATALL

                                if (lots2[0].WIDTHCOATALL_MAX != null && lots2[0].WIDTHCOATALL_MIN != null)
                                {
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##"));
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##"));
                                    instCoating.WIDTHCOATALLSpecification = ("( " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.WIDTHCOATALLSpecification = "";
                                }

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instCoating.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instCoating.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instCoating.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instCoating.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instCoating.ROOMTEMPSpecification = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.ROOMTEMPSpecification = "";
                                }

                                #endregion

                                instCoating.RATIOSILICONE = lots2[0].RATIOSILICONE;

                                #region COATINGWEIGTH_MIN

                                if (lots2[0].COATINGWEIGTH_MIN != null && lots2[0].COATINGWEIGTH_MAX != null)
                                {
                                    //instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##"));
                                    instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##") + " +/- " + lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.COATINGWEIGTHSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instCoating.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instCoating.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instCoating.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instCoating.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsCoating.Add(instCoating);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating2.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating3
        private void RepCoating3(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETCOATINGREPORTDATA> lots = CoatingDataService.Instance
                .FINISHING_GETCOATINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListCoating> results = new List<DataControl.ClassData.FinishingClassData.ListCoating>();

                        DataControl.ClassData.FinishingClassData.ListCoating inst = new DataControl.ClassData.FinishingClassData.ListCoating();

                        #region ListCoating

                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = lots[0].FINISHINGCUSTOMER;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.PRODUCTTYPEID = lots[0].PRODUCTTYPEID;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.MCNO = lots[0].MCNO;
                        inst.STATUSFLAG = lots[0].STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.BE_COATWIDTH = lots[0].BE_COATWIDTH;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.TEMP9_PV = lots[0].TEMP9_PV;
                        inst.TEMP10_PV = lots[0].TEMP10_PV;
                        inst.FANRPM = lots[0].FANRPM;
                        inst.EXFAN_FRONT_BACK = lots[0].EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = lots[0].EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = lots[0].ANGLEKNIFE;
                        inst.BLADENO = lots[0].BLADENO;
                        inst.BLADEDIRECTION = lots[0].BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = lots[0].CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = lots[0].OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = lots[0].FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = lots[0].FRAMEWIDTH_TENTER;
                        inst.PATHLINE = lots[0].PATHLINE;
                        inst.FEEDIN = lots[0].FEEDIN;
                        inst.OVERFEED = lots[0].OVERFEED;
                        inst.SPEED_PV = lots[0].SPEED_PV;

                        inst.WIDTHCOAT = lots[0].WIDTHCOAT;
                        inst.WIDTHCOATALL = lots[0].WIDTHCOATALL;
                        inst.SILICONE_A = lots[0].SILICONE_A;
                        inst.SILICONE_B = lots[0].SILICONE_B;
                        inst.COATINGWEIGTH_L = lots[0].COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = lots[0].COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = lots[0].COATINGWEIGTH_R;

                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.SAMPLINGID = lots[0].SAMPLINGID;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.PARTNO = lots[0].PARTNO;
                        inst.FINISHLENGTH = lots[0].FINISHLENGTH;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.TEMP9_MIN = lots[0].TEMP9_MIN;
                        inst.TEMP9_MAX = lots[0].TEMP9_MAX;
                        inst.TEMP10_MIN = lots[0].TEMP10_MIN;
                        inst.TEMP10_MAX = lots[0].TEMP10_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;
                        inst.TENSIONUP_MIN = lots[0].TENSIONUP_MIN;
                        inst.TENSIONUP_MAX = lots[0].TENSIONUP_MAX;
                        inst.TENSIONDOWN_MIN = lots[0].TENSIONDOWN_MIN;
                        inst.TENSIONDOWN_MAX = lots[0].TENSIONDOWN_MAX;

                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETCOATINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETCOATINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListCoatingCondition> resultsCoating = new List<DataControl.ClassData.FinishingClassData.ListCoatingCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListCoatingCondition instCoating = new DataControl.ClassData.FinishingClassData.ListCoatingCondition();

                                #region BE_COATWIDTH

                                if (lots2[0].BE_COATWIDTHMAX != null && lots2[0].BE_COATWIDTHMIN != null)
                                {
                                    //instCoating.BE_COATWIDTHSpecification = (lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " - " + lots2[0].BE_COATWIDTHMIN.Value.ToString("#,##0.##"));
                                    instCoating.BE_COATWIDTHSpecification = ("( " + lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.BE_COATWIDTHSpecification = "";
                                }

                                #endregion

                                #region FANRPM

                                if (lots2[0].FANRPM != null && lots2[0].FANRPM_MARGIN != null)
                                {
                                    instCoating.Fan110Specification = (lots2[0].FANRPM.Value.ToString("#,##0.##") + " ± " + lots2[0].FANRPM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.Fan110Specification = "";
                                }

                                #endregion

                                #region EXFAN_FRONT_BACK

                                if (lots2[0].EXFAN_FRONT_BACK != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN1_6Specification = (lots2[0].EXFAN_FRONT_BACK.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN1_6Specification = "";
                                }

                                #endregion

                                #region EXFAN_MIDDLE

                                if (lots2[0].EXFAN_MIDDLE != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN2_5Specification = (lots2[0].EXFAN_MIDDLE.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN2_5Specification = "";
                                }

                                #endregion

                                #region ANGLEKNIFE

                                if (lots2[0].ANGLEKNIFE != null)
                                    instCoating.ANGLEKNIFESpecification = lots2[0].ANGLEKNIFE.Value.ToString("#,##0.##");
                                else
                                    instCoating.ANGLEKNIFESpecification = "";

                                #endregion

                                instCoating.BLADENO = lots2[0].BLADENO;

                                instCoating.BLADEDIRECTION = lots2[0].BLADEDIRECTION;


                                #region TENSION_UP

                                if (lots2[0].TENSION_UP != null && lots2[0].TENSION_UP_MARGIN != null)
                                {
                                    //instCoating.TENSION_UPSpecification = (lots2[0].TENSION_UP.Value.ToString("#,##0.##") + " ± " + lots2[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                                    instCoating.TENSION_UPSpecification = (lots2[0].TENSION_UP.Value.ToString("#,##0.##") + " +/- " + lots2[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.TENSION_UPSpecification = "";
                                }

                                #endregion

                                #region TENSION_DOWN

                                if (lots2[0].TENSION_DOWN != null && lots2[0].TENSION_DOWN_MARGIN != null)
                                {
                                    //instCoating.TENSION_DOWNSpecification = (lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " ± " + lots2[0].TENSION_DOWN_MARGIN.Value.ToString("#,##0.##"));
                                    instCoating.TENSION_DOWNSpecification = ("( " + lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.TENSION_DOWNSpecification = "";
                                }

                                #endregion

                                #region FRAMEWIDTH_FORN

                                if (lots2[0].FRAMEWIDTH_FORN != null)
                                    instCoating.FRAMEWIDTH_FORN = lots2[0].FRAMEWIDTH_FORN;
                                else
                                    instCoating.FRAMEWIDTH_FORN = 0;

                                #endregion

                                #region FRAMEWIDTH_TENTER

                                if (lots2[0].FRAMEWIDTH_TENTER != null)
                                    instCoating.FRAMEWIDTH_TENTER = lots2[0].FRAMEWIDTH_TENTER;
                                else
                                    instCoating.FRAMEWIDTH_TENTER = 0;

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instCoating.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SPEEDSpecification = "";
                                }

                                #endregion

                                #region WIDTHCOAT

                                if (lots2[0].WIDTHCOAT != null)
                                    instCoating.WIDTHCOATSpecification = "> " + lots2[0].WIDTHCOAT.Value.ToString("#,##0.##");
                                else
                                    instCoating.WIDTHCOATSpecification = "";

                                #endregion

                                #region WIDTHCOATALL

                                if (lots2[0].WIDTHCOATALL_MAX != null && lots2[0].WIDTHCOATALL_MIN != null)
                                {
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##"));
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##"));
                                    instCoating.WIDTHCOATALLSpecification = ("( " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.WIDTHCOATALLSpecification = "";
                                }

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instCoating.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instCoating.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instCoating.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instCoating.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instCoating.ROOMTEMPSpecification = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.ROOMTEMPSpecification = "";
                                }

                                #endregion

                                instCoating.RATIOSILICONE = lots2[0].RATIOSILICONE;

                                #region COATINGWEIGTH_MIN

                                if (lots2[0].COATINGWEIGTH_MIN != null && lots2[0].COATINGWEIGTH_MAX != null)
                                {
                                    //instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##"));
                                    instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##") + " +/- " + lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.COATINGWEIGTHSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instCoating.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instCoating.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instCoating.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instCoating.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsCoating.Add(instCoating);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating3.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating1Dryer
        private void RepCoating1Dryer(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETDRYERREPORTDATA> lots = CoatingDataService.Instance.FINISHING_GETDRYERREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListDryer> results = new List<DataControl.ClassData.FinishingClassData.ListDryer>();

                        DataControl.ClassData.FinishingClassData.ListDryer inst = new DataControl.ClassData.FinishingClassData.ListDryer();

                        #region ListDryer

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;

                        inst.WIDTH_BE_HEAT = lots[0].WIDTH_BE_HEAT;
                        inst.ACCPRESURE = lots[0].ACCPRESURE;
                        inst.ASSTENSION = lots[0].ASSTENSION;
                        inst.ACCARIDENSER = lots[0].ACCARIDENSER;
                        inst.CHIFROT = lots[0].CHIFROT;
                        inst.CHIREAR = lots[0].CHIREAR;
                        inst.DRYERTEMP1_PV = lots[0].DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = lots[0].DRYERTEMP1_SP;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.SPEED_SP = lots[0].SPEED_SP;
                        inst.STEAMPRESSURE = lots[0].STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = lots[0].DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = lots[0].EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = lots[0].WIDTH_AF_HEAT;

                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;
                        
                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETDRYERCONDITIONData> lots2 = CoatingDataService.Instance.GetFINISHING_GETDRYERCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListDryerCondition> resultsDryerCondition = new List<DataControl.ClassData.FinishingClassData.ListDryerCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListDryerCondition instDryerCondition = new DataControl.ClassData.FinishingClassData.ListDryerCondition();

                                #region WIDTH_BE_HEAT

                                if (lots2[0].WIDTH_BE_HEAT_MAX != null && lots2[0].WIDTH_BE_HEAT_MIN != null)
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = (lots2[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = "";
                                }

                                #endregion

                                #region ACCPRESURE

                                if (lots2[0].ACCPRESURE != null)
                                {
                                    instDryerCondition.ACCPRESURESpecification = lots2[0].ACCPRESURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCPRESURESpecification = "";
                                }

                                #endregion

                                #region ASSTENSION

                                if (lots2[0].ASSTENSION != null)
                                {
                                    instDryerCondition.ASSTENSIONSpecification = lots2[0].ASSTENSION.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ASSTENSIONSpecification = "";
                                }

                                #endregion

                                #region ACCARIDENSER

                                if (lots2[0].ACCARIDENSER != null)
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = lots2[0].ACCARIDENSER.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = "";
                                }

                                #endregion

                                #region CHIFROT

                                if (lots2[0].CHIFROT != null)
                                {
                                    instDryerCondition.CHIFROTSpecification = lots2[0].CHIFROT.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIFROTSpecification = "";
                                }

                                #endregion

                                #region CHIREAR

                                if (lots2[0].CHIREAR != null)
                                {
                                    instDryerCondition.CHIREARSpecification = lots2[0].CHIREAR.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIREARSpecification = "";
                                }

                                #endregion

                                #region DRYERTEMP1

                                if (lots2[0].DRYERTEMP1 != null && lots2[0].DRYERTEMP1_MARGIN != null)
                                {
                                    instDryerCondition.DRYERTEMP1Specification = (lots2[0].DRYERTEMP1.Value.ToString("#,##0.##") + " ± " + lots2[0].DRYERTEMP1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.DRYERTEMP1Specification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instDryerCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region STEAMPRESSURE

                                if (lots2[0].STEAMPRESSURE != null)
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = lots2[0].STEAMPRESSURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = "";
                                }

                                #endregion

                                #region DRYERUPCIRCUFAN

                                if (lots2[0].DRYERUPCIRCUFAN != null)
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = lots2[0].DRYERUPCIRCUFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = "";
                                }

                                #endregion

                                #region EXHAUSTFAN

                                if (lots2[0].EXHAUSTFAN != null)
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = lots2[0].EXHAUSTFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF_HEAT

                                if (lots2[0].WIDTH_AF_HEAT != null && lots2[0].WIDTH_AF_HEAT_MARGIN != null)
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = (lots2[0].WIDTH_AF_HEAT.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_HEAT_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instDryerCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.HUMIDSpecification = "";
                                }

                                #endregion

                                resultsDryerCondition.Add(instDryerCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating1Dryer.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating12Step
        private void RepCoating12Step(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETCOATINGREPORTDATA> lots = CoatingDataService.Instance
                .FINISHING_GETCOATINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListCoating> results = new List<DataControl.ClassData.FinishingClassData.ListCoating>();

                        DataControl.ClassData.FinishingClassData.ListCoating inst = new DataControl.ClassData.FinishingClassData.ListCoating();

                        #region ListCoating

                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;
                        inst.FINISHINGCUSTOMER = lots[0].FINISHINGCUSTOMER;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.PRODUCTTYPEID = lots[0].PRODUCTTYPEID;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.MCNO = lots[0].MCNO;
                        inst.STATUSFLAG = lots[0].STATUSFLAG;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.BE_COATWIDTH = lots[0].BE_COATWIDTH;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.TEMP9_PV = lots[0].TEMP9_PV;
                        inst.TEMP10_PV = lots[0].TEMP10_PV;
                        inst.FANRPM = lots[0].FANRPM;
                        inst.EXFAN_FRONT_BACK = lots[0].EXFAN_FRONT_BACK;
                        inst.EXFAN_MIDDLE = lots[0].EXFAN_MIDDLE;
                        inst.ANGLEKNIFE = lots[0].ANGLEKNIFE;
                        inst.BLADENO = lots[0].BLADENO;
                        inst.BLADEDIRECTION = lots[0].BLADEDIRECTION;
                        inst.CYLINDER_TENSIONUP = lots[0].CYLINDER_TENSIONUP;
                        inst.OPOLE_TENSIONDOWN = lots[0].OPOLE_TENSIONDOWN;
                        inst.FRAMEWIDTH_FORN = lots[0].FRAMEWIDTH_FORN;
                        inst.FRAMEWIDTH_TENTER = lots[0].FRAMEWIDTH_TENTER;
                        inst.PATHLINE = lots[0].PATHLINE;
                        inst.FEEDIN = lots[0].FEEDIN;
                        inst.OVERFEED = lots[0].OVERFEED;
                        inst.SPEED_PV = lots[0].SPEED_PV;

                        inst.WIDTHCOAT = lots[0].WIDTHCOAT;
                        inst.WIDTHCOATALL = lots[0].WIDTHCOATALL;
                        inst.SILICONE_A = lots[0].SILICONE_A;
                        inst.SILICONE_B = lots[0].SILICONE_B;
                        inst.COATINGWEIGTH_L = lots[0].COATINGWEIGTH_L;
                        inst.COATINGWEIGTH_C = lots[0].COATINGWEIGTH_C;
                        inst.COATINGWEIGTH_R = lots[0].COATINGWEIGTH_R;

                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.SAMPLINGID = lots[0].SAMPLINGID;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.PARTNO = lots[0].PARTNO;
                        inst.FINISHLENGTH = lots[0].FINISHLENGTH;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.TEMP9_MIN = lots[0].TEMP9_MIN;
                        inst.TEMP9_MAX = lots[0].TEMP9_MAX;
                        inst.TEMP10_MIN = lots[0].TEMP10_MIN;
                        inst.TEMP10_MAX = lots[0].TEMP10_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;
                        inst.TENSIONUP_MIN = lots[0].TENSIONUP_MIN;
                        inst.TENSIONUP_MAX = lots[0].TENSIONUP_MAX;
                        inst.TENSIONDOWN_MIN = lots[0].TENSIONDOWN_MIN;
                        inst.TENSIONDOWN_MAX = lots[0].TENSIONDOWN_MAX;

                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETCOATINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETCOATINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListCoatingCondition> resultsCoating = new List<DataControl.ClassData.FinishingClassData.ListCoatingCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListCoatingCondition instCoating = new DataControl.ClassData.FinishingClassData.ListCoatingCondition();

                                #region BE_COATWIDTH

                                if (lots2[0].BE_COATWIDTHMAX != null && lots2[0].BE_COATWIDTHMIN != null)
                                {
                                    //instCoating.BE_COATWIDTHSpecification = (lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " - " + lots2[0].BE_COATWIDTHMIN.Value.ToString("#,##0.##"));
                                    instCoating.BE_COATWIDTHSpecification = ("( " + lots2[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.BE_COATWIDTHSpecification = "";
                                }

                                #endregion

                                #region FANRPM

                                if (lots2[0].FANRPM != null && lots2[0].FANRPM_MARGIN != null)
                                {
                                    instCoating.Fan110Specification = (lots2[0].FANRPM.Value.ToString("#,##0.##") + " - " + lots2[0].FANRPM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.Fan110Specification = "";
                                }

                                #endregion

                                #region EXFAN_FRONT_BACK

                                if (lots2[0].EXFAN_FRONT_BACK != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN15Specification = (lots2[0].EXFAN_FRONT_BACK.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN15Specification = "";
                                }

                                #endregion

                                #region EXFAN_MIDDLE

                                if (lots2[0].EXFAN_MIDDLE != null && lots2[0].EXFAN_MARGIN != null)
                                {
                                    instCoating.EXFAN234Specification = (lots2[0].EXFAN_MIDDLE.Value.ToString("#,##0.##") + " ± " + lots2[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.EXFAN234Specification = "";
                                }

                                #endregion

                                #region ANGLEKNIFE

                                if (lots2[0].ANGLEKNIFE != null)
                                    instCoating.ANGLEKNIFESpecification = lots2[0].ANGLEKNIFE.Value.ToString("#,##0.##");
                                else
                                    instCoating.ANGLEKNIFESpecification = "";

                                #endregion

                                instCoating.BLADENO = lots2[0].BLADENO;
                                instCoating.BLADEDIRECTION = lots2[0].BLADEDIRECTION;

                                instCoating.PATHLINE = lots2[0].PATHLINE;

                                #region FEEDIN_MAX

                                if (lots2[0].FEEDIN_MAX != null && lots2[0].FEEDIN_MIN != null)
                                {
                                    //instCoating.FeedInSpecification = (lots2[0].FEEDIN_MAX.Value.ToString("#,##0.##") + " - " + lots2[0].FEEDIN_MIN.Value.ToString("#,##0.##"));
                                    instCoating.FeedInSpecification = (lots2[0].FEEDIN_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].FEEDIN_MAX.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.FeedInSpecification = "";
                                }

                                #endregion

                                #region TENSION_DOWN

                                if (lots2[0].TENSION_DOWN != null && lots2[0].TENSION_DOWN_MARGIN != null)
                                {
                                    instCoating.TENSION_DOWNSpecification = (lots2[0].TENSION_DOWN.Value.ToString("#,##0.##") + " ± " + lots2[0].TENSION_DOWN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.TENSION_DOWNSpecification = "";
                                }

                                #endregion

                                #region TENSION_UP

                                if (lots2[0].TENSION_UP != null && lots2[0].TENSION_UP_MARGIN != null)
                                {
                                    instCoating.TENSION_UPSpecification = (lots2[0].TENSION_UP.Value.ToString("#,##0.##") + " +/- " + lots2[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.TENSION_UPSpecification = "";
                                }

                                if (lots2[0].TENSION_UP != null)
                                    instCoating.TENSION_UP = lots2[0].TENSION_UP;

                                #endregion

                                #region FRAMEWIDTH_FORN

                                if (lots2[0].FRAMEWIDTH_FORN != null)
                                    instCoating.FRAMEWIDTH_FORN = lots2[0].FRAMEWIDTH_FORN;

                                #endregion

                                #region FRAMEWIDTH_TENTER

                                if (lots2[0].FRAMEWIDTH_TENTER != null)
                                    instCoating.FRAMEWIDTH_TENTER = lots2[0].FRAMEWIDTH_TENTER;

                                #endregion

                                instCoating.OVERFEED = lots2[0].OVERFEED;

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instCoating.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SPEEDSpecification = "";
                                }

                                #endregion

                                #region WIDTHCOAT

                                if (lots2[0].WIDTHCOAT != null)
                                    instCoating.WIDTHCOATSpecification = "> " + lots2[0].WIDTHCOAT.Value.ToString("#,##0.##");
                                else
                                    instCoating.WIDTHCOATSpecification = "";

                                #endregion

                                #region WIDTHCOATALL

                                if (lots2[0].WIDTHCOATALL_MAX != null && lots2[0].WIDTHCOATALL_MIN != null)
                                {
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##"));
                                    //instCoating.WIDTHCOATALLSpecification = (lots2[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##"));
                                    instCoating.WIDTHCOATALLSpecification = ("( " + lots2[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instCoating.WIDTHCOATALLSpecification = "";
                                }

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instCoating.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instCoating.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instCoating.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instCoating.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instCoating.ROOMTEMPSpecification = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.ROOMTEMPSpecification = "";
                                }

                                #endregion

                                instCoating.RATIOSILICONE = lots2[0].RATIOSILICONE;

                                #region COATINGWEIGTH_MIN

                                if (lots2[0].COATINGWEIGTH_MIN != null && lots2[0].COATINGWEIGTH_MAX != null)
                                {
                                    //instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##"));
                                    instCoating.COATINGWEIGTHSpecification = (lots2[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##") + " +/- " + lots2[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instCoating.COATINGWEIGTHSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN

                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instCoating.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instCoating.HUMIDSpecification = "";
                                }

                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //instCoating.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instCoating.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsCoating.Add(instCoating);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsCoating);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating12Step.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating3Scouring
        private void RepCoating3Scouring(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
                    string ITM_CODE = string.Empty;

                    List<FINISHING_GETSCOURINGREPORTDATA> lots = CoatingDataService.Instance
              .FINISHING_GETSCOURINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListScouring> results = new List<DataControl.ClassData.FinishingClassData.ListScouring>();

                        DataControl.ClassData.FinishingClassData.ListScouring inst = new DataControl.ClassData.FinishingClassData.ListScouring();

                        #region ListScouring

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        ITM_CODE = inst.ITM_CODE;

                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;

                        inst.TEMP9_PV = lots[0].TEMP9_PV;
                        inst.TEMP10_PV = lots[0].TEMP10_PV;

                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.MAINFRAMEWIDTH = lots[0].MAINFRAMEWIDTH;
                        inst.WIDTH_BE = lots[0].WIDTH_BE;
                        inst.WIDTH_AF = lots[0].WIDTH_AF;
                        inst.PIN2PIN = lots[0].PIN2PIN;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;

                        inst.TEMP9_MIN = lots[0].TEMP9_MIN;
                        inst.TEMP9_MAX = lots[0].TEMP9_MAX;
                        inst.TEMP10_MIN = lots[0].TEMP10_MIN;
                        inst.TEMP10_MAX = lots[0].TEMP10_MAX;

                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        #endregion

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETSCOURINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListScouringCondition> resultsScouringCondition = new List<DataControl.ClassData.FinishingClassData.ListScouringCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListScouringCondition instScouringCondition = new DataControl.ClassData.FinishingClassData.ListScouringCondition();

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instScouringCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instScouringCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instScouringCondition.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instScouringCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region MAINFRAMEWIDTH

                                if (lots2[0].MAINFRAMEWIDTH != null && lots2[0].MAINFRAMEWIDTH_MARGIN != null)
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = (lots2[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##") + " ± " + lots2[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = "";
                                }

                                #endregion

                                #region WIDTH_BE

                                if (lots2[0].WIDTH_BE != null && lots2[0].WIDTH_BE_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_BESpecification = (lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_BESpecification = ("( " + lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_BESpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF

                                if (lots2[0].WIDTH_AF != null && lots2[0].WIDTH_AF_MARGIN != null)
                                {
                                    instScouringCondition.WIDTH_AFSpecification = (lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_AFSpecification = "";
                                }

                                #endregion

                                #region PIN2PIN

                                if (lots2[0].PIN2PIN != null && lots2[0].PIN2PIN_MARGIN != null)
                                {
                                    instScouringCondition.PIN2PINSpecification = (lots2[0].PIN2PIN.Value.ToString("#,##0.##") + " ± " + lots2[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.PIN2PINSpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instScouringCondition.StrROOMTEMP = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.StrROOMTEMP = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instScouringCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instScouringCondition.HUMIDSpecification = "";
                                }

                                #endregion

                                resultsScouringCondition.Add(instScouringCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);

                            if (ITM_CODE == "4755ATW" || ITM_CODE == "4755AT" || ITM_CODE == "4753JWAT")
                            {
                                this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating3Scouring2.rdlc";
                            }
                            else
                            {
                                this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating3Scouring.rdlc";
                            }

                            this._reportViewer.LocalReport.EnableExternalImages = true;
                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepScouringCoat1
        private void RepScouringCoat1(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETSCOURINGREPORTDATA> lots = CoatingDataService.Instance
                .FINISHING_GETSCOURINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListScouring> results = new List<DataControl.ClassData.FinishingClassData.ListScouring>();

                        DataControl.ClassData.FinishingClassData.ListScouring inst = new DataControl.ClassData.FinishingClassData.ListScouring();

                        #region ListScouring

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.MAINFRAMEWIDTH = lots[0].MAINFRAMEWIDTH;
                        inst.WIDTH_BE = lots[0].WIDTH_BE;
                        inst.WIDTH_AF = lots[0].WIDTH_AF;
                        inst.PIN2PIN = lots[0].PIN2PIN;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;
                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        #endregion

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETSCOURINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListScouringCondition> resultsScouringCondition = new List<DataControl.ClassData.FinishingClassData.ListScouringCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListScouringCondition instScouringCondition = new DataControl.ClassData.FinishingClassData.ListScouringCondition();

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instScouringCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instScouringCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instScouringCondition.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instScouringCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region MAINFRAMEWIDTH

                                if (lots2[0].MAINFRAMEWIDTH != null && lots2[0].MAINFRAMEWIDTH_MARGIN != null)
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = (lots2[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##") + " ± " + lots2[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = "";
                                }

                                #endregion

                                #region WIDTH_BE

                                if (lots2[0].WIDTH_BE != null && lots2[0].WIDTH_BE_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_BESpecification = (lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_BESpecification = ("( " + lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_BESpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF

                                if (lots2[0].WIDTH_AF != null && lots2[0].WIDTH_AF_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_AFSpecification = (lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_AFSpecification = ("( " + lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_AFSpecification = "";
                                }

                                #endregion

                                #region PIN2PIN

                                if (lots2[0].PIN2PIN != null && lots2[0].PIN2PIN_MARGIN != null)
                                {
                                    instScouringCondition.PIN2PINSpecification = (lots2[0].PIN2PIN.Value.ToString("#,##0.##") + " ± " + lots2[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.PIN2PINSpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instScouringCondition.StrROOMTEMP = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.StrROOMTEMP = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instScouringCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instScouringCondition.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instScouringCondition.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instScouringCondition.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsScouringCondition.Add(instScouringCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepScouringCoat1.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepScouringCoat2
        private void RepScouringCoat2(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETSCOURINGREPORTDATA> lots = CoatingDataService.Instance
              .FINISHING_GETSCOURINGREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListScouring> results = new List<DataControl.ClassData.FinishingClassData.ListScouring>();

                        DataControl.ClassData.FinishingClassData.ListScouring inst = new DataControl.ClassData.FinishingClassData.ListScouring();

                        #region ListScouring

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;
                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_CHEM_PV;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.HOTFLUE_PV = lots[0].HOTFLUE_PV;
                        inst.TEMP1_PV = lots[0].TEMP1_PV;
                        inst.TEMP2_PV = lots[0].TEMP2_PV;
                        inst.TEMP3_PV = lots[0].TEMP3_PV;
                        inst.TEMP4_PV = lots[0].TEMP4_PV;
                        inst.TEMP5_PV = lots[0].TEMP5_PV;
                        inst.TEMP6_PV = lots[0].TEMP6_PV;
                        inst.TEMP7_PV = lots[0].TEMP7_PV;
                        inst.TEMP8_PV = lots[0].TEMP8_PV;

                        inst.TEMP9_PV = lots[0].TEMP9_PV;
                        inst.TEMP10_PV = lots[0].TEMP10_PV;

                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.MAINFRAMEWIDTH = lots[0].MAINFRAMEWIDTH;
                        inst.WIDTH_BE = lots[0].WIDTH_BE;
                        inst.WIDTH_AF = lots[0].WIDTH_AF;
                        inst.PIN2PIN = lots[0].PIN2PIN;
                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.REPROCESS = lots[0].REPROCESS;
                        inst.TEMP1_MIN = lots[0].TEMP1_MIN;
                        inst.TEMP1_MAX = lots[0].TEMP1_MAX;
                        inst.TEMP2_MIN = lots[0].TEMP2_MIN;
                        inst.TEMP2_MAX = lots[0].TEMP2_MAX;
                        inst.TEMP3_MIN = lots[0].TEMP3_MIN;
                        inst.TEMP3_MAX = lots[0].TEMP3_MAX;
                        inst.TEMP4_MIN = lots[0].TEMP4_MIN;
                        inst.TEMP4_MAX = lots[0].TEMP4_MAX;
                        inst.TEMP5_MIN = lots[0].TEMP5_MIN;
                        inst.TEMP5_MAX = lots[0].TEMP5_MAX;
                        inst.TEMP6_MIN = lots[0].TEMP6_MIN;
                        inst.TEMP6_MAX = lots[0].TEMP6_MAX;
                        inst.TEMP7_MIN = lots[0].TEMP7_MIN;
                        inst.TEMP7_MAX = lots[0].TEMP7_MAX;
                        inst.TEMP8_MIN = lots[0].TEMP8_MIN;
                        inst.TEMP8_MAX = lots[0].TEMP8_MAX;

                        inst.TEMP9_MIN = lots[0].TEMP9_MIN;
                        inst.TEMP9_MAX = lots[0].TEMP9_MAX;
                        inst.TEMP10_MIN = lots[0].TEMP10_MIN;
                        inst.TEMP10_MAX = lots[0].TEMP10_MAX;

                        inst.SAT_CHEM_MIN = lots[0].SAT_CHEM_MIN;
                        inst.SAT_CHEM_MAX = lots[0].SAT_CHEM_MAX;
                        inst.WASHING1_MIN = lots[0].WASHING1_MIN;
                        inst.WASHING1_MAX = lots[0].WASHING1_MAX;
                        inst.WASHING2_MIN = lots[0].WASHING2_MIN;
                        inst.WASHING2_MAX = lots[0].WASHING2_MAX;
                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        #endregion

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETSCOURINGCONDITIONData> lots2 = CoatingDataService.Instance
                                    .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListScouringCondition> resultsScouringCondition = new List<DataControl.ClassData.FinishingClassData.ListScouringCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListScouringCondition instScouringCondition = new DataControl.ClassData.FinishingClassData.ListScouringCondition();

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instScouringCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instScouringCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WASHING2Specification = "";
                                }

                                #endregion

                                #region HOTFLUE

                                if (lots2[0].HOTFLUE != null && lots2[0].HOTFLUE_MARGIN != null)
                                {
                                    instScouringCondition.HOTFLUESpecification = (lots2[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + lots2[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.HOTFLUESpecification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instScouringCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region MAINFRAMEWIDTH

                                if (lots2[0].MAINFRAMEWIDTH != null && lots2[0].MAINFRAMEWIDTH_MARGIN != null)
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = (lots2[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##") + " ± " + lots2[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.MAINFRAMEWIDTHSpecification = "";
                                }

                                #endregion

                                #region WIDTH_BE

                                if (lots2[0].WIDTH_BE != null && lots2[0].WIDTH_BE_MARGIN != null)
                                {
                                    //instScouringCondition.WIDTH_BESpecification = (lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##"));
                                    instScouringCondition.WIDTH_BESpecification = ("( " + lots2[0].WIDTH_BE.Value.ToString("#,##0.##") + " )");
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_BESpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF

                                if (lots2[0].WIDTH_AF != null && lots2[0].WIDTH_AF_MARGIN != null)
                                {
                                    instScouringCondition.WIDTH_AFSpecification = (lots2[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.WIDTH_AFSpecification = "";
                                }

                                #endregion

                                #region PIN2PIN

                                if (lots2[0].PIN2PIN != null && lots2[0].PIN2PIN_MARGIN != null)
                                {
                                    instScouringCondition.PIN2PINSpecification = (lots2[0].PIN2PIN.Value.ToString("#,##0.##") + " ± " + lots2[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.PIN2PINSpecification = "";
                                }

                                #endregion

                                #region ROOMTEMP

                                if (lots2[0].ROOMTEMP != null && lots2[0].ROOMTEMP_MARGIN != null)
                                {
                                    instScouringCondition.StrROOMTEMP = (lots2[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + lots2[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instScouringCondition.StrROOMTEMP = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instScouringCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instScouringCondition.HUMIDSpecification = "";
                                }
                                //if (lots2[0].HUMIDITY_MIN != null && lots2[0].HUMIDITY_MAX != null)
                                //{
                                //    instScouringCondition.HUMIDSpecification = (lots2[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                                //}
                                //else
                                //{
                                //    instScouringCondition.HUMIDSpecification = "";
                                //}

                                #endregion

                                resultsScouringCondition.Add(instScouringCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsScouringCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepScouringCoat2.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCoating1Scouring
        private void RepCoating1Scouring(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETDRYERREPORTDATA> lots = CoatingDataService.Instance.FINISHING_GETDRYERREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListDryer> results = new List<DataControl.ClassData.FinishingClassData.ListDryer>();

                        DataControl.ClassData.FinishingClassData.ListDryer inst = new DataControl.ClassData.FinishingClassData.ListDryer();

                        #region ListDryer

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;

                        inst.WIDTH_BE_HEAT = lots[0].WIDTH_BE_HEAT;
                        inst.ACCPRESURE = lots[0].ACCPRESURE;
                        inst.ASSTENSION = lots[0].ASSTENSION;
                        inst.ACCARIDENSER = lots[0].ACCARIDENSER;
                        inst.CHIFROT = lots[0].CHIFROT;
                        inst.CHIREAR = lots[0].CHIREAR;
                        inst.DRYERTEMP1_PV = lots[0].DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = lots[0].DRYERTEMP1_SP;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.SPEED_SP = lots[0].SPEED_SP;
                        inst.STEAMPRESSURE = lots[0].STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = lots[0].DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = lots[0].EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = lots[0].WIDTH_AF_HEAT;

                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_PV;
                        inst.SATURATOR_CHEM_SP = lots[0].SATURATOR_SP;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING1_SP = lots[0].WASHING1_SP;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.WASHING2_SP = lots[0].WASHING2_SP;

                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETDRYERCONDITIONData> lots2 = CoatingDataService.Instance.GetFINISHING_GETDRYERCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListDryerCondition> resultsDryerCondition = new List<DataControl.ClassData.FinishingClassData.ListDryerCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListDryerCondition instDryerCondition = new DataControl.ClassData.FinishingClassData.ListDryerCondition();

                                #region WIDTH_BE_HEAT

                                if (lots2[0].WIDTH_BE_HEAT_MAX != null && lots2[0].WIDTH_BE_HEAT_MIN != null)
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = (lots2[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = "";
                                }

                                #endregion

                                #region ACCPRESURE

                                if (lots2[0].ACCPRESURE != null)
                                {
                                    instDryerCondition.ACCPRESURESpecification = lots2[0].ACCPRESURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCPRESURESpecification = "";
                                }

                                #endregion

                                #region ASSTENSION

                                if (lots2[0].ASSTENSION != null)
                                {
                                    instDryerCondition.ASSTENSIONSpecification = lots2[0].ASSTENSION.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ASSTENSIONSpecification = "";
                                }

                                #endregion

                                #region ACCARIDENSER

                                if (lots2[0].ACCARIDENSER != null)
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = lots2[0].ACCARIDENSER.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = "";
                                }

                                #endregion

                                #region CHIFROT

                                if (lots2[0].CHIFROT != null)
                                {
                                    instDryerCondition.CHIFROTSpecification = lots2[0].CHIFROT.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIFROTSpecification = "";
                                }

                                #endregion

                                #region CHIREAR

                                if (lots2[0].CHIREAR != null)
                                {
                                    instDryerCondition.CHIREARSpecification = lots2[0].CHIREAR.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIREARSpecification = "";
                                }

                                #endregion

                                #region DRYERTEMP1

                                if (lots2[0].DRYERTEMP1 != null && lots2[0].DRYERTEMP1_MARGIN != null)
                                {
                                    instDryerCondition.DRYERTEMP1Specification = (lots2[0].DRYERTEMP1.Value.ToString("#,##0.##") + " ± " + lots2[0].DRYERTEMP1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.DRYERTEMP1Specification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instDryerCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region STEAMPRESSURE

                                if (lots2[0].STEAMPRESSURE != null)
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = lots2[0].STEAMPRESSURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = "";
                                }

                                #endregion

                                #region DRYERUPCIRCUFAN

                                if (lots2[0].DRYERUPCIRCUFAN != null)
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = lots2[0].DRYERUPCIRCUFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = "";
                                }

                                #endregion

                                #region EXHAUSTFAN

                                if (lots2[0].EXHAUSTFAN != null)
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = lots2[0].EXHAUSTFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF_HEAT

                                if (lots2[0].WIDTH_AF_HEAT != null && lots2[0].WIDTH_AF_HEAT_MARGIN != null)
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = (lots2[0].WIDTH_AF_HEAT.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_HEAT_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instDryerCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.HUMIDSpecification = "";
                                }

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instDryerCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instDryerCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instDryerCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WASHING2Specification = "";
                                }

                                #endregion

                                resultsDryerCondition.Add(instDryerCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCoating1Scouring.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepScouring2ScouringDry
        private void RepScouring2ScouringDry(string WEAVINGLOT, string itm_code, string ScouringNo, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETDRYERREPORTDATA> lots = CoatingDataService.Instance.FINISHING_GETDRYERREPORTList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {

                        List<DataControl.ClassData.FinishingClassData.ListDryer> results = new List<DataControl.ClassData.FinishingClassData.ListDryer>();

                        DataControl.ClassData.FinishingClassData.ListDryer inst = new DataControl.ClassData.FinishingClassData.ListDryer();

                        #region ListDryer

                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;

                        //ปรับใช้แทน STARTDATE
                        inst.STARTDATE = lots[0].CONDITIONDATE;
                        //inst.STARTDATE = lots[0].STARTDATE;

                        inst.ENDDATE = lots[0].ENDDATE;

                        inst.WIDTH_BE_HEAT = lots[0].WIDTH_BE_HEAT;
                        inst.ACCPRESURE = lots[0].ACCPRESURE;
                        inst.ASSTENSION = lots[0].ASSTENSION;
                        inst.ACCARIDENSER = lots[0].ACCARIDENSER;
                        inst.CHIFROT = lots[0].CHIFROT;
                        inst.CHIREAR = lots[0].CHIREAR;
                        inst.DRYERTEMP1_PV = lots[0].DRYERTEMP1_PV;
                        inst.DRYERTEMP1_SP = lots[0].DRYERTEMP1_SP;
                        inst.SPEED_PV = lots[0].SPEED_PV;
                        inst.SPEED_SP = lots[0].SPEED_SP;
                        inst.STEAMPRESSURE = lots[0].STEAMPRESSURE;
                        inst.DRYERCIRCUFAN = lots[0].DRYERCIRCUFAN;
                        inst.EXHAUSTFAN = lots[0].EXHAUSTFAN;
                        inst.WIDTH_AF_HEAT = lots[0].WIDTH_AF_HEAT;

                        inst.ITM_WEAVING = lots[0].ITM_WEAVING;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.LENGTH1 = lots[0].LENGTH1;
                        inst.LENGTH2 = lots[0].LENGTH2;
                        inst.LENGTH3 = lots[0].LENGTH3;
                        inst.LENGTH4 = lots[0].LENGTH4;
                        inst.LENGTH5 = lots[0].LENGTH5;
                        inst.LENGTH6 = lots[0].LENGTH6;
                        inst.LENGTH7 = lots[0].LENGTH7;
                        inst.INPUTLENGTH = lots[0].INPUTLENGTH;
                        inst.FINISHBY = lots[0].FINISHBY;
                        inst.PARTNO = lots[0].PARTNO;

                        inst.CONDITIONBY = lots[0].CONDITIONBY;
                        inst.STARTBY = lots[0].STARTBY;
                        inst.REMARK = lots[0].REMARK;

                        inst.HUMID_AF = lots[0].HUMIDITY_AF;
                        inst.HUMID_BF = lots[0].HUMIDITY_BF;

                        inst.OPERATOR_GROUP = lots[0].OPERATOR_GROUP;

                        inst.HOTFLUE_MIN = lots[0].HOTFLUE_MIN;
                        inst.HOTFLUE_MAX = lots[0].HOTFLUE_MAX;
                        inst.SPEED_MIN = lots[0].SPEED_MIN;
                        inst.SPEED_MAX = lots[0].SPEED_MAX;

                        inst.SATURATOR_CHEM_PV = lots[0].SATURATOR_PV;
                        inst.SATURATOR_CHEM_SP = lots[0].SATURATOR_SP;
                        inst.WASHING1_PV = lots[0].WASHING1_PV;
                        inst.WASHING1_SP = lots[0].WASHING1_SP;
                        inst.WASHING2_PV = lots[0].WASHING2_PV;
                        inst.WASHING2_SP = lots[0].WASHING2_SP;

                        results.Add(inst);

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rds2 = new Microsoft.Reporting.WinForms.ReportDataSource();

                            #region FINISHING_GETSCOURINGCONDITIONData

                            List<FINISHING_GETDRYERCONDITIONData> lots2 = CoatingDataService.Instance.GetFINISHING_GETDRYERCONDITIONDataList(itm_code, ScouringNo);

                            List<DataControl.ClassData.FinishingClassData.ListDryerCondition> resultsDryerCondition = new List<DataControl.ClassData.FinishingClassData.ListDryerCondition>();

                            if (null != lots2 && lots2.Count > 0)
                            {

                                DataControl.ClassData.FinishingClassData.ListDryerCondition instDryerCondition = new DataControl.ClassData.FinishingClassData.ListDryerCondition();

                                #region WIDTH_BE_HEAT

                                if (lots2[0].WIDTH_BE_HEAT_MAX != null && lots2[0].WIDTH_BE_HEAT_MIN != null)
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = (lots2[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##") + " - " + lots2[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_BE_HEATSpecification = "";
                                }

                                #endregion

                                #region ACCPRESURE

                                if (lots2[0].ACCPRESURE != null)
                                {
                                    instDryerCondition.ACCPRESURESpecification = lots2[0].ACCPRESURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCPRESURESpecification = "";
                                }

                                #endregion

                                #region ASSTENSION

                                if (lots2[0].ASSTENSION != null)
                                {
                                    instDryerCondition.ASSTENSIONSpecification = lots2[0].ASSTENSION.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ASSTENSIONSpecification = "";
                                }

                                #endregion

                                #region ACCARIDENSER

                                if (lots2[0].ACCARIDENSER != null)
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = lots2[0].ACCARIDENSER.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.ACCARIDENSERSpecification = "";
                                }

                                #endregion

                                #region CHIFROT

                                if (lots2[0].CHIFROT != null)
                                {
                                    instDryerCondition.CHIFROTSpecification = lots2[0].CHIFROT.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIFROTSpecification = "";
                                }

                                #endregion

                                #region CHIREAR

                                if (lots2[0].CHIREAR != null)
                                {
                                    instDryerCondition.CHIREARSpecification = lots2[0].CHIREAR.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.CHIREARSpecification = "";
                                }

                                #endregion

                                #region DRYERTEMP1

                                if (lots2[0].DRYERTEMP1 != null && lots2[0].DRYERTEMP1_MARGIN != null)
                                {
                                    instDryerCondition.DRYERTEMP1Specification = (lots2[0].DRYERTEMP1.Value.ToString("#,##0.##") + " ± " + lots2[0].DRYERTEMP1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.DRYERTEMP1Specification = "";
                                }

                                #endregion

                                #region SPEED

                                if (lots2[0].SPEED != null && lots2[0].SPEED_MARGIN != null)
                                {
                                    instDryerCondition.SPEEDSpecification = (lots2[0].SPEED.Value.ToString("#,##0.##") + " ± " + lots2[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.SPEEDSpecification = "";
                                }

                                #endregion

                                #region STEAMPRESSURE

                                if (lots2[0].STEAMPRESSURE != null)
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = lots2[0].STEAMPRESSURE.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.STEAMPRESSURESpecification = "";
                                }

                                #endregion

                                #region DRYERUPCIRCUFAN

                                if (lots2[0].DRYERUPCIRCUFAN != null)
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = lots2[0].DRYERUPCIRCUFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.DRYERUPCIRCUFANSpecification = "";
                                }

                                #endregion

                                #region EXHAUSTFAN

                                if (lots2[0].EXHAUSTFAN != null)
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = lots2[0].EXHAUSTFAN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.EXHAUSTFANSpecification = "";
                                }

                                #endregion

                                #region WIDTH_AF_HEAT

                                if (lots2[0].WIDTH_AF_HEAT != null && lots2[0].WIDTH_AF_HEAT_MARGIN != null)
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = (lots2[0].WIDTH_AF_HEAT.Value.ToString("#,##0.##") + " ± " + lots2[0].WIDTH_AF_HEAT_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WIDTH_AF_HEATSpecification = "";
                                }

                                #endregion

                                #region HUMIDITY_MIN
                                if (lots2[0].HUMIDITY_MAX != null)
                                {
                                    instDryerCondition.HUMIDSpecification = "< " + lots2[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    instDryerCondition.HUMIDSpecification = "";
                                }

                                #endregion

                                #region SATURATOR_CHEM

                                if (lots2[0].SATURATOR_CHEM != null && lots2[0].SATURATOR_CHEM_MARGIN != null)
                                {
                                    instDryerCondition.SATURATOR_CHEMSpecification = (lots2[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + lots2[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.SATURATOR_CHEMSpecification = "";
                                }

                                #endregion

                                #region WASHING1

                                if (lots2[0].WASHING1 != null && lots2[0].WASHING1_MARGIN != null)
                                {
                                    instDryerCondition.WASHING1Specification = (lots2[0].WASHING1.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WASHING1Specification = "";
                                }

                                #endregion

                                #region WASHING2

                                if (lots2[0].WASHING2 != null && lots2[0].WASHING2_MARGIN != null)
                                {
                                    instDryerCondition.WASHING2Specification = (lots2[0].WASHING2.Value.ToString("#,##0.##") + " ± " + lots2[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                                }
                                else
                                {
                                    instDryerCondition.WASHING2Specification = "";
                                }

                                #endregion

                                resultsDryerCondition.Add(instDryerCondition);

                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }
                            else
                            {
                                rds2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsDryerCondition);
                            }

                            #endregion

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rds2);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepScouring2ScouringDry.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepBarcodePart
        private void RepBarcodePart(string CmdString)
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(CmdString))
                //{
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<DataControl.ClassData.FinishingClassData.ListBarcodePart> results = new List<DataControl.ClassData.FinishingClassData.ListBarcodePart>();

                    DataControl.ClassData.FinishingClassData.ListBarcodePart inst = new DataControl.ClassData.FinishingClassData.ListBarcodePart();
                    inst.CustomerPartNo = "604121024A";
                    inst.Quantity = 454;
                    inst.Description = "PA 66 AIRBAG FABRICCOATED 25 g/m2";
                    inst.SupplierCode = "53612";
                    inst.StrDate = "P20150112";
                    inst.PartRev = "A";
                    inst.NW = 486;
                    inst.GW = 490;
                    inst.Serial = "C12151122";
                    inst.BatchNo = "31215011321";


                    results.Add(inst);

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepBarcodePart.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        //parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("Postcard", false);
                    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCUT_GETSLIP
        private void RepCUT_GETSLIP(string ITEMLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ITEMLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    string showSelvage = string.Empty;
                    string snd_Barcode = string.Empty;
                    string cmID = string.Empty;

                    string batch = string.Empty;
                    string batchNo = string.Empty;
                    string batchNo2nd = string.Empty;
                    string itemCode = string.Empty;
                    bool chk2ndNull = false;

                    List<CUT_GETSLIPReport> lots =
                           CutPrintDataService.Instance.CUT_GETSLIPReportData(ITEMLOT);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        CUT_GETSLIPReport lot = lots[0];

                        List<DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETSLIP> results = new List<DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETSLIP>();

                        DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETSLIP inst = new DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETSLIP();

                        inst.ITEMLOT = lot.ITEMLOT;
                        inst.STARTDATE = lot.STARTDATE;
                        inst.ENDDATE = lot.ENDDATE;
                        inst.WIDTHBARCODE1 = lot.WIDTHBARCODE1;
                        inst.WIDTHBARCODE2 = lot.WIDTHBARCODE2;
                        inst.WIDTHBARCODE3 = lot.WIDTHBARCODE3;
                        inst.WIDTHBARCODE4 = lot.WIDTHBARCODE4;
                        inst.DISTANTBARCODE1 = lot.DISTANTBARCODE1;
                        inst.DISTANTBARCODE2 = lot.DISTANTBARCODE2;
                        inst.DISTANTBARCODE3 = lot.DISTANTBARCODE3;
                        inst.DISTANTBARCODE4 = lot.DISTANTBARCODE4;
                        inst.DISTANTLINE1 = lot.DISTANTLINE1;
                        inst.DISTANTLINE2 = lot.DISTANTLINE2;
                        inst.DISTANTLINE3 = lot.DISTANTLINE3;
                        inst.DENSITYWARP = lot.DENSITYWARP;
                        inst.DENSITYWEFT = lot.DENSITYWEFT;
                        inst.SPEED = lot.SPEED;
                        inst.BEFORE_WIDTH = lot.BEFORE_WIDTH;
                        inst.AFTER_WIDTH = lot.AFTER_WIDTH;

                        inst.PARTNO = lot.PARTNO;
                        inst.PARTNO2 = inst.PARTNO;

                        itemCode = lot.ITEMCODE;

                        batch = inst.PARTNO;
                        batchNo = string.Empty;
                        batchNo2nd = string.Empty;

                        if (!string.IsNullOrEmpty(batch))
                        {
                            if (batch.Length > 0)
                            {
                                if (batch.Length >= 6)
                                {
                                    batchNo = batch.Substring(0, 5);
                                }

                                if (batch.Length >= 10)
                                {
                                    batchNo2nd = batch.Substring(9, batch.Length - 9);
                                }
                            }
                        }

                        cmID = lot.CUSTOMERID;
                        inst.CUSTOMERID = cmID;

                        if (!string.IsNullOrEmpty(cmID))
                        {
                            if (cmID == "08")
                            {
                                #region itemCode = 09300
                                //if (itemCode == "09300")
                                //{
                                //    #region ITEMCODE == 09300

                                //    #region BEGINROLL

                                //    if (lot.BEGINROLL_LINE1 == "Yes")
                                //        inst.BEGINROLL_LINE1 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE1))
                                //            inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1.Trim();
                                //        else
                                //            inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1;
                                //    }

                                //    if (lot.BEGINROLL_LINE2 == "Yes")
                                //        inst.BEGINROLL_LINE2 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE2))
                                //            inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2.Trim();
                                //        else
                                //            inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2;
                                //    }

                                //    if (lot.BEGINROLL_LINE3 == "Yes")
                                //        inst.BEGINROLL_LINE3 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE3))
                                //            inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3.Trim();
                                //        else
                                //            inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3;
                                //    }

                                //    if (lot.BEGINROLL_LINE4 == "Yes")
                                //        inst.BEGINROLL_LINE4 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE4))
                                //            inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4.Trim();
                                //        else
                                //            inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4;
                                //    }

                                //    if (lot.ENDROLL_LINE1 == "Yes")
                                //        inst.ENDROLL_LINE1 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE1))
                                //            inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1.Trim();
                                //        else
                                //            inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1;
                                //    }

                                //    if (lot.ENDROLL_LINE2 == "Yes")
                                //        inst.ENDROLL_LINE2 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE2))
                                //            inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2.Trim();
                                //        else
                                //            inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2;
                                //    }

                                //    if (lot.ENDROLL_LINE3 == "Yes")
                                //        inst.ENDROLL_LINE3 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE3))
                                //            inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3.Trim();
                                //        else
                                //            inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3;
                                //    }

                                //    if (lot.ENDROLL_LINE4 == "Yes")
                                //        inst.ENDROLL_LINE4 = inst.PARTNO;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE4))
                                //            inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4.Trim();
                                //        else
                                //            inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4;
                                //    }
                                //    #endregion

                                //    #region BEGINROLL2

                                //    if (!string.IsNullOrEmpty(lot.SND_BARCODE))
                                //        snd_Barcode = lot.SND_BARCODE.Trim();

                                //    if (!string.IsNullOrEmpty(snd_Barcode))
                                //        inst.SND_BARCODE = snd_Barcode;

                                //    if (lot.BEGINROLL2_LINE1 == "Yes")
                                //        inst.BEGINROLL2_LINE1 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE1))
                                //            inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1.Trim();
                                //        else
                                //            inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1;
                                //    }

                                //    if (lot.BEGINROLL2_LINE2 == "Yes")
                                //        inst.BEGINROLL2_LINE2 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE2))
                                //            inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2.Trim();
                                //        else
                                //            inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2;
                                //    }

                                //    if (lot.BEGINROLL2_LINE3 == "Yes")
                                //        inst.BEGINROLL2_LINE3 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE3))
                                //            inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3.Trim();
                                //        else
                                //            inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3;
                                //    }

                                //    if (lot.BEGINROLL2_LINE4 == "Yes")
                                //        inst.BEGINROLL2_LINE4 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE4))
                                //            inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4.Trim();
                                //        else
                                //            inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4;
                                //    }

                                //    if (lot.ENDROLL2_LINE1 == "Yes")
                                //        inst.ENDROLL2_LINE1 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE1))
                                //            inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1.Trim();
                                //        else
                                //            inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1;
                                //    }

                                //    if (lot.ENDROLL2_LINE2 == "Yes")
                                //        inst.ENDROLL2_LINE2 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE2))
                                //            inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2.Trim();
                                //        else
                                //            inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2;
                                //    }

                                //    if (lot.ENDROLL2_LINE3 == "Yes")
                                //        inst.ENDROLL2_LINE3 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE3))
                                //            inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3.Trim();
                                //        else
                                //            inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3;
                                //    }

                                //    if (lot.ENDROLL2_LINE4 == "Yes")
                                //        inst.ENDROLL2_LINE4 = snd_Barcode;
                                //    else
                                //    {
                                //        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE4))
                                //            inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4.Trim();
                                //        else
                                //            inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4;
                                //    }
                                //    #endregion

                                //    #endregion
                                //}
                                //else
                                //{
                                #endregion

                                if (!string.IsNullOrEmpty(batch))
                                    {
                                        if (batch.Length >= 9)
                                        batchNo = "P" + batch.Substring(0, 9);
                                    }

                                    if (!string.IsNullOrEmpty(ITEMLOT))
                                    {
                                        batchNo2nd = "H" + ITEMLOT.Substring(0, ITEMLOT.Length - 1);
                                    }

                                    #region cmID = "08"

                                    if (!string.IsNullOrEmpty(batchNo))
                                        inst.PARTNO2 = batchNo.Trim();

                                    if (!string.IsNullOrEmpty(batchNo2nd))
                                        inst.SND_BARCODE = batchNo2nd.Trim();

                                    #region BEGINROLL

                                    if (lot.BEGINROLL_LINE1 == "Yes")
                                        inst.BEGINROLL_LINE1 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE1))
                                            inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1.Trim();
                                        else
                                            inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1;
                                    }

                                    if (lot.BEGINROLL_LINE2 == "Yes")
                                        inst.BEGINROLL_LINE2 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE2))
                                            inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2.Trim();
                                        else
                                            inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2;
                                    }

                                    if (lot.BEGINROLL_LINE3 == "Yes")
                                        inst.BEGINROLL_LINE3 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE3))
                                            inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3.Trim();
                                        else
                                            inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3;
                                    }

                                    if (lot.BEGINROLL_LINE4 == "Yes")
                                        inst.BEGINROLL_LINE4 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE4))
                                            inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4.Trim();
                                        else
                                            inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4;
                                    }

                                    if (lot.ENDROLL_LINE1 == "Yes")
                                        inst.ENDROLL_LINE1 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE1))
                                            inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1.Trim();
                                        else
                                            inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1;
                                    }

                                    if (lot.ENDROLL_LINE2 == "Yes")
                                        inst.ENDROLL_LINE2 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE2))
                                            inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2.Trim();
                                        else
                                            inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2;
                                    }

                                    if (lot.ENDROLL_LINE3 == "Yes")
                                        inst.ENDROLL_LINE3 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE3))
                                            inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3.Trim();
                                        else
                                            inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3;
                                    }

                                    if (lot.ENDROLL_LINE4 == "Yes")
                                        inst.ENDROLL_LINE4 = batchNo;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL_LINE4))
                                            inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4.Trim();
                                        else
                                            inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4;
                                    }

                                    #endregion

                                    #region BEGINROLL2

                                    //snd_Barcode = lot.SND_BARCODE;

                                    //if (!string.IsNullOrEmpty(snd_Barcode))
                                    //    inst.SND_BARCODE = snd_Barcode;

                                    if (lot.BEGINROLL2_LINE1 == "Yes")
                                        inst.BEGINROLL2_LINE1 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE1))
                                            inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1.Trim();
                                        else
                                            inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1;
                                    }

                                    if (lot.BEGINROLL2_LINE2 == "Yes")
                                        inst.BEGINROLL2_LINE2 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE2))
                                            inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2.Trim();
                                        else
                                            inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2;
                                    }

                                    if (lot.BEGINROLL2_LINE3 == "Yes")
                                        inst.BEGINROLL2_LINE3 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE3))
                                            inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3.Trim();
                                        else
                                            inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3;
                                    }

                                    if (lot.BEGINROLL2_LINE4 == "Yes")
                                        inst.BEGINROLL2_LINE4 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE4))
                                            inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4.Trim();
                                        else
                                            inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4;
                                    }

                                    if (lot.ENDROLL2_LINE1 == "Yes")
                                        inst.ENDROLL2_LINE1 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE1))
                                            inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1.Trim();
                                        else
                                            inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1;
                                    }

                                    if (lot.ENDROLL2_LINE2 == "Yes")
                                        inst.ENDROLL2_LINE2 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE2))
                                            inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2.Trim();
                                        else
                                            inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2;
                                    }

                                    if (lot.ENDROLL2_LINE3 == "Yes")
                                        inst.ENDROLL2_LINE3 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE3))
                                            inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3.Trim();
                                        else
                                            inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3;
                                    }

                                    if (lot.ENDROLL2_LINE4 == "Yes")
                                        inst.ENDROLL2_LINE4 = batchNo2nd;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE4))
                                            inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4.Trim();
                                        else
                                            inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4;
                                    }

                                    #endregion

                                    if (string.IsNullOrEmpty(inst.BEGINROLL2_LINE1) && string.IsNullOrEmpty(inst.BEGINROLL2_LINE2) && string.IsNullOrEmpty(inst.BEGINROLL2_LINE3) && string.IsNullOrEmpty(inst.BEGINROLL2_LINE4)
                                        && string.IsNullOrEmpty(inst.ENDROLL2_LINE1) && string.IsNullOrEmpty(inst.ENDROLL2_LINE2) && string.IsNullOrEmpty(inst.ENDROLL2_LINE3) && string.IsNullOrEmpty(inst.ENDROLL2_LINE4))
                                    {
                                        chk2ndNull = true;

                                        if (string.IsNullOrEmpty(batchNo))
                                        {
                                            if (!string.IsNullOrEmpty(inst.PARTNO))
                                                inst.PARTNO2 = inst.PARTNO.Trim();

                                            #region BEGINROLL

                                            if (lot.BEGINROLL_LINE1 == "Yes")
                                                inst.BEGINROLL_LINE1 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE1))
                                                    inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1.Trim();
                                                else
                                                    inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1;
                                            }

                                            if (lot.BEGINROLL_LINE2 == "Yes")
                                                inst.BEGINROLL_LINE2 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE2))
                                                    inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2.Trim();
                                                else
                                                    inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2;
                                            }

                                            if (lot.BEGINROLL_LINE3 == "Yes")
                                                inst.BEGINROLL_LINE3 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE3))
                                                    inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3.Trim();
                                                else
                                                    inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3;
                                            }

                                            if (lot.BEGINROLL_LINE4 == "Yes")
                                                inst.BEGINROLL_LINE4 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE4))
                                                    inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4.Trim();
                                                else
                                                    inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4;
                                            }

                                            if (lot.ENDROLL_LINE1 == "Yes")
                                                inst.ENDROLL_LINE1 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE1))
                                                    inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1.Trim();
                                                else
                                                    inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1;
                                            }

                                            if (lot.ENDROLL_LINE2 == "Yes")
                                                inst.ENDROLL_LINE2 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE2))
                                                    inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2.Trim();
                                                else
                                                    inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2;
                                            }

                                            if (lot.ENDROLL_LINE3 == "Yes")
                                                inst.ENDROLL_LINE3 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE3))
                                                    inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3.Trim();
                                                else
                                                    inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3;
                                            }

                                            if (lot.ENDROLL_LINE4 == "Yes")
                                                inst.ENDROLL_LINE4 = inst.PARTNO2;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE4))
                                                    inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4.Trim();
                                                else
                                                    inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4;
                                            }

                                            #endregion
                                        }

                                        if (string.IsNullOrEmpty(batchNo2nd))
                                        {
                                            if (!string.IsNullOrEmpty(lot.SND_BARCODE))
                                                snd_Barcode = lot.SND_BARCODE.Trim();

                                            if (!string.IsNullOrEmpty(snd_Barcode))
                                                inst.SND_BARCODE = snd_Barcode;

                                            #region BEGINROLL2

                                            if (lot.BEGINROLL2_LINE1 == "Yes")
                                                inst.BEGINROLL2_LINE1 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE1))
                                                    inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1.Trim();
                                                else
                                                    inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1;
                                            }

                                            if (lot.BEGINROLL2_LINE2 == "Yes")
                                                inst.BEGINROLL2_LINE2 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE2))
                                                    inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2.Trim();
                                                else
                                                    inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2;
                                            }

                                            if (lot.BEGINROLL2_LINE3 == "Yes")
                                                inst.BEGINROLL2_LINE3 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE3))
                                                    inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3.Trim();
                                                else
                                                    inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3;
                                            }

                                            if (lot.BEGINROLL2_LINE4 == "Yes")
                                                inst.BEGINROLL2_LINE4 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE4))
                                                    inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4.Trim();
                                                else
                                                    inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4;
                                            }

                                            if (lot.ENDROLL2_LINE1 == "Yes")
                                                inst.ENDROLL2_LINE1 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE1))
                                                    inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1.Trim();
                                                else
                                                    inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1;
                                            }

                                            if (lot.ENDROLL2_LINE2 == "Yes")
                                                inst.ENDROLL2_LINE2 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE2))
                                                    inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2.Trim();
                                                else
                                                    inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2;
                                            }

                                            if (lot.ENDROLL2_LINE3 == "Yes")
                                                inst.ENDROLL2_LINE3 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE3))
                                                    inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3.Trim();
                                                else
                                                    inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3;
                                            }

                                            if (lot.ENDROLL2_LINE4 == "Yes")
                                                inst.ENDROLL2_LINE4 = snd_Barcode;
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE4))
                                                    inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4.Trim();
                                                else
                                                    inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4;
                                            }

                                            #endregion
                                        }
                                    }

                                    #endregion
                                //}
                            }
                            else
                            {
                                #region BEGINROLL

                                if (lot.BEGINROLL_LINE1 == "Yes")
                                    inst.BEGINROLL_LINE1 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE1))
                                        inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1.Trim();
                                    else
                                        inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1;
                                }

                                if (lot.BEGINROLL_LINE2 == "Yes")
                                    inst.BEGINROLL_LINE2 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE2))
                                        inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2.Trim();
                                    else
                                        inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2;
                                }

                                if (lot.BEGINROLL_LINE3 == "Yes")
                                    inst.BEGINROLL_LINE3 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE3))
                                        inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3.Trim();
                                    else
                                        inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3;
                                }

                                if (lot.BEGINROLL_LINE4 == "Yes")
                                    inst.BEGINROLL_LINE4 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE4))
                                        inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4.Trim();
                                    else
                                        inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4;
                                }

                                if (lot.ENDROLL_LINE1 == "Yes")
                                    inst.ENDROLL_LINE1 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL_LINE1))
                                        inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1.Trim();
                                    else
                                        inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1;
                                }

                                if (lot.ENDROLL_LINE2 == "Yes")
                                    inst.ENDROLL_LINE2 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL_LINE2))
                                        inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2.Trim();
                                    else
                                        inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2;
                                }

                                if (lot.ENDROLL_LINE3 == "Yes")
                                    inst.ENDROLL_LINE3 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL_LINE3))
                                        inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3.Trim();
                                    else
                                        inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3;
                                }

                                if (lot.ENDROLL_LINE4 == "Yes")
                                    inst.ENDROLL_LINE4 = inst.PARTNO;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL_LINE4))
                                        inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4.Trim();
                                    else
                                        inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4;
                                }
                                #endregion

                                #region BEGINROLL2

                                if (!string.IsNullOrEmpty(lot.SND_BARCODE))
                                    snd_Barcode = lot.SND_BARCODE.Trim();

                                if (!string.IsNullOrEmpty(snd_Barcode))
                                    inst.SND_BARCODE = snd_Barcode;

                                if (lot.BEGINROLL2_LINE1 == "Yes")
                                    inst.BEGINROLL2_LINE1 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE1))
                                        inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1.Trim();
                                    else
                                        inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1;
                                }

                                if (lot.BEGINROLL2_LINE2 == "Yes")
                                    inst.BEGINROLL2_LINE2 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE2))
                                        inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2.Trim();
                                    else
                                        inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2;
                                }

                                if (lot.BEGINROLL2_LINE3 == "Yes")
                                    inst.BEGINROLL2_LINE3 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE3))
                                        inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3.Trim();
                                    else
                                        inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3;
                                }

                                if (lot.BEGINROLL2_LINE4 == "Yes")
                                    inst.BEGINROLL2_LINE4 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE4))
                                        inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4.Trim();
                                    else
                                        inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4;
                                }

                                if (lot.ENDROLL2_LINE1 == "Yes")
                                    inst.ENDROLL2_LINE1 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE1))
                                        inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1.Trim();
                                    else
                                        inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1;
                                }

                                if (lot.ENDROLL2_LINE2 == "Yes")
                                    inst.ENDROLL2_LINE2 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE2))
                                        inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2.Trim();
                                    else
                                        inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2;
                                }

                                if (lot.ENDROLL2_LINE3 == "Yes")
                                    inst.ENDROLL2_LINE3 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE3))
                                        inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3.Trim();
                                    else
                                        inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3;
                                }

                                if (lot.ENDROLL2_LINE4 == "Yes")
                                    inst.ENDROLL2_LINE4 = snd_Barcode;
                                else
                                {
                                    if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE4))
                                        inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4.Trim();
                                    else
                                        inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4;
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region BEGINROLL

                            if (lot.BEGINROLL_LINE1 == "Yes")
                                inst.BEGINROLL_LINE1 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE1))
                                    inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1.Trim();
                                else
                                    inst.BEGINROLL_LINE1 = lot.BEGINROLL_LINE1;
                            }

                            if (lot.BEGINROLL_LINE2 == "Yes")
                                inst.BEGINROLL_LINE2 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE2))
                                    inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2.Trim();
                                else
                                    inst.BEGINROLL_LINE2 = lot.BEGINROLL_LINE2;
                            }

                            if (lot.BEGINROLL_LINE3 == "Yes")
                                inst.BEGINROLL_LINE3 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE3))
                                    inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3.Trim();
                                else
                                    inst.BEGINROLL_LINE3 = lot.BEGINROLL_LINE3;
                            }

                            if (lot.BEGINROLL_LINE4 == "Yes")
                                inst.BEGINROLL_LINE4 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL_LINE4))
                                    inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4.Trim();
                                else
                                    inst.BEGINROLL_LINE4 = lot.BEGINROLL_LINE4;
                            }

                            if (lot.ENDROLL_LINE1 == "Yes")
                                inst.ENDROLL_LINE1 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE1))
                                    inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1.Trim();
                                else
                                    inst.ENDROLL_LINE1 = lot.ENDROLL_LINE1;
                            }

                            if (lot.ENDROLL_LINE2 == "Yes")
                                inst.ENDROLL_LINE2 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE2))
                                    inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2.Trim();
                                else
                                    inst.ENDROLL_LINE2 = lot.ENDROLL_LINE2;
                            }

                            if (lot.ENDROLL_LINE3 == "Yes")
                                inst.ENDROLL_LINE3 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE3))
                                    inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3.Trim();
                                else
                                    inst.ENDROLL_LINE3 = lot.ENDROLL_LINE3;
                            }

                            if (lot.ENDROLL_LINE4 == "Yes")
                                inst.ENDROLL_LINE4 = inst.PARTNO;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL_LINE4))
                                    inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4.Trim();
                                else
                                    inst.ENDROLL_LINE4 = lot.ENDROLL_LINE4;
                            }
                            #endregion

                            #region BEGINROLL2

                            if (!string.IsNullOrEmpty(lot.SND_BARCODE))
                                snd_Barcode = lot.SND_BARCODE.Trim();

                            if (!string.IsNullOrEmpty(snd_Barcode))
                                inst.SND_BARCODE = snd_Barcode;

                            if (lot.BEGINROLL2_LINE1 == "Yes")
                                inst.BEGINROLL2_LINE1 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE1))
                                    inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1.Trim();
                                else
                                    inst.BEGINROLL2_LINE1 = lot.BEGINROLL2_LINE1;
                            }

                            if (lot.BEGINROLL2_LINE2 == "Yes")
                                inst.BEGINROLL2_LINE2 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE2))
                                    inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2.Trim();
                                else
                                    inst.BEGINROLL2_LINE2 = lot.BEGINROLL2_LINE2;
                            }

                            if (lot.BEGINROLL2_LINE3 == "Yes")
                                inst.BEGINROLL2_LINE3 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE3))
                                    inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3.Trim();
                                else
                                    inst.BEGINROLL2_LINE3 = lot.BEGINROLL2_LINE3;
                            }

                            if (lot.BEGINROLL2_LINE4 == "Yes")
                                inst.BEGINROLL2_LINE4 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.BEGINROLL2_LINE4))
                                    inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4.Trim();
                                else
                                    inst.BEGINROLL2_LINE4 = lot.BEGINROLL2_LINE4;
                            }

                            if (lot.ENDROLL2_LINE1 == "Yes")
                                inst.ENDROLL2_LINE1 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE1))
                                    inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1.Trim();
                                else
                                    inst.ENDROLL2_LINE1 = lot.ENDROLL2_LINE1;
                            }

                            if (lot.ENDROLL2_LINE2 == "Yes")
                                inst.ENDROLL2_LINE2 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE2))
                                    inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2.Trim();
                                else
                                    inst.ENDROLL2_LINE2 = lot.ENDROLL2_LINE2;
                            }

                            if (lot.ENDROLL2_LINE3 == "Yes")
                                inst.ENDROLL2_LINE3 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE3))
                                    inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3.Trim();
                                else
                                    inst.ENDROLL2_LINE3 = lot.ENDROLL2_LINE3;
                            }

                            if (lot.ENDROLL2_LINE4 == "Yes")
                                inst.ENDROLL2_LINE4 = snd_Barcode;
                            else
                            {
                                if (!string.IsNullOrEmpty(lot.ENDROLL2_LINE4))
                                    inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4.Trim();
                                else
                                    inst.ENDROLL2_LINE4 = lot.ENDROLL2_LINE4;
                            }
                            #endregion
                        }

                        inst.OPERATORID = lot.OPERATORID;

                        inst.FINISHINGPROCESS = lot.FINISHINGPROCESS;

                        if (inst.FINISHINGPROCESS == "Scouring")
                        {
                            inst.SELVAGE_LEFT = string.Empty;
                            inst.SELVAGE_RIGHT = string.Empty;
                        }
                        else
                        {
                            inst.SELVAGE_LEFT = lot.SELVAGE_LEFT;
                            inst.SELVAGE_RIGHT = lot.SELVAGE_RIGHT;
                        }

                        inst.REMARK = lot.REMARK;
                        inst.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                        inst.MCNO = lot.MCNO;
                        inst.FINISHLOT = lot.FINISHLOT;
                        inst.ITEMCODE = lot.ITEMCODE;
                        inst.ITEMLOT1 = lot.ITEMLOT1;
                        
                        inst.MCNAME = lot.MCNAME;

                        inst.STATUS = lot.STATUS;
                        inst.SUSPENDDATE = lot.SUSPENDDATE;
                        inst.SUSPENDBY = lot.SUSPENDBY;
                        inst.CLEARDATE = lot.CLEARDATE;
                        inst.CLEARREMARK = lot.CLEARREMARK;
                        inst.CLEARBY = lot.CLEARBY;
                        inst.LENGTHPRINT = lot.LENGTHPRINT;
                        inst.SUSPENDSTARTDATE = lot.SUSPENDSTARTDATE;

                        showSelvage = lot.SHOWSELVAGE;
                        inst.SHOWSELVAGE = showSelvage;

                        inst.TENSION = lot.TENSION;

                        //เพิ่มใหม่ 13/07/17
                        inst.FINISHLENGTH = lot.FINISHLENGTH;

                        //เพิ่มใหม่ 28/09/17
                        inst.LENGTHDETAIL = lot.LENGTHDETAIL;

                        //เพิ่มใหม่ 04/10/17
                        inst.AFTER_WIDTH_END = lot.AFTER_WIDTH_END;

                        results.Add(inst);

                        #region CUT_GETCONDITIONBYITEMCODE

                        List<DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETCONDITIONBYITEMCODE> resultConditions = new List<DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETCONDITIONBYITEMCODE>();

                        DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETCONDITIONBYITEMCODE instCondition = new DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_GETCONDITIONBYITEMCODE();

                        if (!string.IsNullOrEmpty(inst.ITEMCODE))
                        {
                            List<CUT_GETCONDITIONBYITEMCODE> dbResults = CutPrintDataService.Instance.CUT_GETCONDITIONBYITEMCODE(inst.ITEMCODE);

                            if (null != dbResults && dbResults.Count > 0 && null != dbResults[0])
                            {
                                CUT_GETCONDITIONBYITEMCODE dbResult = dbResults[0];

                                instCondition.ITM_CODE = dbResult.ITM_CODE;
                                instCondition.WIDTHBARCODE_MIN = dbResult.WIDTHBARCODE_MIN;
                                instCondition.WIDTHBARCODE_MAX = dbResult.WIDTHBARCODE_MAX;
                                instCondition.DISTANTBARCODE_MIN = dbResult.DISTANTBARCODE_MIN;
                                instCondition.DISTANTBARCODE_MAX = dbResult.DISTANTBARCODE_MAX;
                                instCondition.DISTANTLINE_MIN = dbResult.DISTANTLINE_MIN;
                                instCondition.DISTANTLINE_MAX = dbResult.DISTANTLINE_MAX;
                                instCondition.DENSITYWARP_MIN = dbResult.DENSITYWARP_MIN;
                                instCondition.DENSITYWARP_MAX = dbResult.DENSITYWARP_MAX;
                                instCondition.DENSITYWEFT_MIN = dbResult.DENSITYWEFT_MIN;
                                instCondition.DENSITYWEFT_MAX = dbResult.DENSITYWEFT_MAX;
                                instCondition.SPEED = dbResult.SPEED;
                                instCondition.SPEED_MARGIN = dbResult.SPEED_MARGIN;
                                instCondition.AFTER_WIDTH = dbResult.AFTER_WIDTH;

                                instCondition.strWIDTHBARCODE = dbResult.strWIDTHBARCODE;
                                instCondition.strDISTANTBARCODE = dbResult.strDISTANTBARCODE;
                                instCondition.strDISTANTLINE = dbResult.strDISTANTLINE;
                                instCondition.strDENSITYWARP = dbResult.strDENSITYWARP;
                                instCondition.strDENSITYWEFT = dbResult.strDENSITYWEFT;
                                instCondition.strSPEED = dbResult.strSPEED;
                                instCondition.strAFTER = dbResult.strAFTER;

                                resultConditions.Add(instCondition);
                            }
                        }

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            Microsoft.Reporting.WinForms.ReportDataSource rdConditions = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultConditions);

                            rds.Value = results;
                            rdConditions.Value = resultConditions;

                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rdConditions);

                            if (!string.IsNullOrEmpty(cmID))
                            {
                                if (cmID == "08")
                                {
                                    #region itemCode = 09300
                                    //if (itemCode == "09300")
                                    //{
                                    //    #region ITEMCODE == 09300
                                    //    if (showSelvage == "N")
                                    //    {
                                    //        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N.rdlc";
                                    //    }
                                    //    else
                                    //    {
                                    //        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP.rdlc";
                                    //    }
                                    //    #endregion
                                    //}
                                    //else
                                    //{
                                    #endregion

                                    #region chk2ndNull
                                    if (chk2ndNull == false)
                                    {
                                        if (showSelvage == "N")
                                        {
                                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N2.rdlc";
                                        }
                                        else
                                        {
                                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP2.rdlc";
                                        }
                                    }
                                    else
                                    {
                                        if (showSelvage == "N")
                                        {
                                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N.rdlc";
                                        }
                                        else
                                        {
                                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP.rdlc";
                                        }
                                    }
                                    #endregion
                                    //}
                                }
                                else
                                {
                                    if (showSelvage == "N")
                                    {
                                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N.rdlc";
                                    }
                                    else
                                    {
                                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP.rdlc";
                                    }
                                }
                            }
                            else
                            {
                                if (showSelvage == "N")
                                {
                                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N.rdlc";
                                }
                                else
                                {
                                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP.rdlc";
                                }
                            }

                            #region Old
                            //if (string.IsNullOrEmpty(snd_Barcode))
                            //{
                            //    if (showSelvage == "N")
                            //    {
                            //        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N.rdlc";
                            //    }
                            //    else
                            //    {
                            //        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP.rdlc";
                            //    }
                            //}
                            //else
                            //{
                            //    if (showSelvage == "N")
                            //    {
                            //        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP_N2.rdlc";
                            //    }
                            //    else
                            //    {
                            //        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_GETSLIP2.rdlc";
                            //    }
                            //}
                            #endregion

                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant1", "Distant between barcode and number (ระยะห่างระหว่างบาร์โค้ดกับตัวเลข) cm."));
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant2", "Distant between line by line (ระยะห่างระหว่างแถวของบาร์โค้ด) cm."));

                            #region Old ไม่ได้ใช้
                            //if (cmID != "08")
                            //{
                            //    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant1","Distant between barcode and number (ระยะห่างระหว่างบาร์โค้ดกับตัวเลข) cm."));
                            //    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant2", "Distant between line by line (ระยะห่างระหว่างแถวของบาร์โค้ด) cm."));
                            //}
                            //if (cmID == "08")
                            //{
                            //    if (itemCode == "09300")
                            //    {
                            //        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant1", "Distant between barcode and number (ระยะห่างระหว่างบาร์โค้ดกับตัวเลข) cm."));
                            //        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant2", "Distant between line by line (ระยะห่างระหว่างแถวของบาร์โค้ด) cm."));
                            //    }
                            //    else
                            //    {
                            //        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant1", "Distant Between Set to Set (ระยะห่างระหว่างบาร์โค้ด) mm."));
                            //        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("labDistant2", "Distant Between Edge L Side (ระยะห่างจากริมซ้าย) mm."));
                            //    }
                            //}
                            #endregion

                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepPalletListGrade_AB
        private void RepPalletListGrade_AB(string PALLETNO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(PALLETNO))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<PACK_PALLETSHEET> lots = PackingDataService.Instance.PACK_PALLETSHEET(PALLETNO);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.PackingClassData.ListPack_PalletSheet> results = new List<DataControl.ClassData.PackingClassData.ListPack_PalletSheet>();

                        for (int i = 0; i < lots.Count; i++)
                        {
                            PACK_PALLETSHEET lot = lots[i];

                            #region GRADE A , B

                            DataControl.ClassData.PackingClassData.ListPack_PalletSheet inst = new DataControl.ClassData.PackingClassData.ListPack_PalletSheet();

                            inst.RowNo = i + 1;

                            inst.PALLETNO = lot.PALLETNO;
                            inst.ITEMCODE = lot.ITEMCODE;
                            inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                            inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                            inst.GRADE = lot.GRADE;

                            inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                            inst.NETLENGTH = lot.NETLENGTH;
                            inst.NETWEIGHT = lot.NETWEIGHT;
                            inst.PACKINGDATE = lot.PACKINGDATE;
                            inst.PACKINGBY = lot.PACKINGBY;
                            inst.CHECKBY = lot.CHECKBY;
                            inst.CHECKINGDATE = lot.CHECKINGDATE;
                            inst.LOADINGTYPE = lot.LOADINGTYPE;

                            inst.ITM_WEAVING = lot.ITM_WEAVING;
                            inst.YARNCODE = lot.YARNCODE;

                            results.Add(inst);

                            #endregion
                        }

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;

                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPalletList.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();
                            this._reportViewer.Refresh();

                           SetSize("A4", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepPalletListGrade_C
        private void RepPalletListGrade_C(string PALLETNO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(PALLETNO))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<PACK_PALLETSHEET> lots = PackingDataService.Instance.PACK_PALLETSHEET(PALLETNO);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.PackingClassData.ListPack_PalletSheet> results = new List<DataControl.ClassData.PackingClassData.ListPack_PalletSheet>();

                        string itemCode = string.Empty;

                        int newI = 0;
                        int OldI = 0;

                        if (ConmonReportService.Instance.RowLast - 1 > 0)
                            OldI = ConmonReportService.Instance.RowLast - 1;

                        for (int i = 0; i < lots.Count; i++)
                        {
                            PACK_PALLETSHEET lot = lots[i];

                            if (itemCode == "")
                            {
                                #region itemCode == ""

                                DataControl.ClassData.PackingClassData.ListPack_PalletSheet inst = new DataControl.ClassData.PackingClassData.ListPack_PalletSheet();

                                inst.RowNo = newI + 1;

                                inst.PALLETNO = lot.PALLETNO;

                                itemCode = lot.ITEMCODE;

                                inst.ITEMCODE = itemCode;
                                inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                                inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                                inst.GRADE = lot.GRADE;
                                inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                                inst.NETLENGTH = lot.NETLENGTH;
                                inst.NETWEIGHT = lot.NETWEIGHT;
                                inst.PACKINGDATE = lot.PACKINGDATE;
                                inst.PACKINGBY = lot.PACKINGBY;
                                inst.CHECKBY = lot.CHECKBY;
                                inst.CHECKINGDATE = lot.CHECKINGDATE;
                                inst.LOADINGTYPE = lot.LOADINGTYPE;

                                inst.ITM_WEAVING = lot.ITM_WEAVING;
                                inst.YARNCODE = lot.YARNCODE;


                                results.Add(inst);

                                newI++;
                                
                                #endregion
                            }
                            else
                            {
                                if (lot.ITEMCODE == itemCode)
                                {
                                    DataControl.ClassData.PackingClassData.ListPack_PalletSheet inst = new DataControl.ClassData.PackingClassData.ListPack_PalletSheet();

                                    inst.RowNo = newI + 1;

                                    inst.PALLETNO = lot.PALLETNO;
                                    itemCode = lot.ITEMCODE;
                                    inst.ITEMCODE = itemCode;

                                    inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                                    inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                                    inst.GRADE = lot.GRADE;
                                    inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                                    inst.NETLENGTH = lot.NETLENGTH;
                                    inst.NETWEIGHT = lot.NETWEIGHT;
                                    inst.PACKINGDATE = lot.PACKINGDATE;
                                    inst.PACKINGBY = lot.PACKINGBY;
                                    inst.CHECKBY = lot.CHECKBY;
                                    inst.CHECKINGDATE = lot.CHECKINGDATE;
                                    inst.LOADINGTYPE = lot.LOADINGTYPE;

                                    inst.ITM_WEAVING = lot.ITM_WEAVING;
                                    inst.YARNCODE = lot.YARNCODE;

                                    results.Add(inst);
                                    newI++;
                                }
                                else
                                {
                                    if (results.Count > 0)
                                    {
                                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                                        rds.Value = results;

                                        this._reportViewer.LocalReport.DataSources.Clear();
                                        this._reportViewer.LocalReport.DataSources.Add(rds);
                                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPalletList.rdlc";
                                        this._reportViewer.LocalReport.EnableExternalImages = true;

                                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                                        this._reportViewer.LocalReport.SetParameters(parameters);

                                        this._reportViewer.RefreshReport();
                                        this._reportViewer.Refresh();

                                        SetSize("A4", true);
                                        PrintByPrinter(ConmonReportService.Instance.Printername);

                                        results.Clear();

                                        newI = 1;
                                    }

                                    DataControl.ClassData.PackingClassData.ListPack_PalletSheet inst = new DataControl.ClassData.PackingClassData.ListPack_PalletSheet();

                                    inst.RowNo = newI + 1;

                                    inst.PALLETNO = lot.PALLETNO;
                                    itemCode = lot.ITEMCODE;
                                    inst.ITEMCODE = itemCode;

                                    inst.CUSTOMERTYPE = lot.CUSTOMERTYPE;
                                    inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                                    inst.GRADE = lot.GRADE;
                                    inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                                    inst.NETLENGTH = lot.NETLENGTH;
                                    inst.NETWEIGHT = lot.NETWEIGHT;
                                    inst.PACKINGDATE = lot.PACKINGDATE;
                                    inst.PACKINGBY = lot.PACKINGBY;
                                    inst.CHECKBY = lot.CHECKBY;
                                    inst.CHECKINGDATE = lot.CHECKINGDATE;
                                    inst.LOADINGTYPE = lot.LOADINGTYPE;

                                    inst.ITM_WEAVING = lot.ITM_WEAVING;
                                    inst.YARNCODE = lot.YARNCODE;

                                    results.Add(inst);
                                    newI++;
                                }
                            }
                        }     

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;

                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPalletList.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();
                            this._reportViewer.Refresh();

                            SetSize("A4", true);
                            PrintByPrinter(ConmonReportService.Instance.Printername);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWeaving
        private void RepWeaving(string WEAVINGLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<GETWEAVINGINGDATA> lots =
                           WeavingDataService.Instance.GetWeavingingDataList(WEAVINGLOT);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA> results = new List<DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA>();

                        for (int i = 0; i < lots.Count; i++)
                        {
                            GETWEAVINGINGDATA lot = lots[i];

                            DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA inst = new DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA();

                            inst.RowNo = i + 1;
                            inst.WEAVINGLOT = lot.WEAVINGLOT;
                            inst.ITM_WEAVING = lot.ITM_WEAVING;
                            inst.LENGTH = lot.LENGTH;
                            inst.LOOMNO = lot.LOOMNO;
                            inst.WEAVINGDATE = lot.WEAVINGDATE;
                            inst.SHIFT = lot.SHIFT;
                            inst.REMARK = lot.REMARK;
                            inst.CREATEDATE = lot.CREATEDATE;
                            inst.WIDTH = lot.WIDTH;
                            inst.PREPAREBY = lot.PREPAREBY;
                            inst.WEAVINGNO = lot.WEAVINGNO;

                            inst.BEAMLOT = lot.BEAMLOT;
                            inst.DOFFNO = lot.DOFFNO;
                            inst.DENSITY_WARP = lot.DENSITY_WARP;
                            inst.TENSION = lot.TENSION;
                            inst.STARTDATE = lot.STARTDATE;
                            inst.DOFFBY = lot.DOFFBY;
                            inst.SPEED = lot.SPEED;
                            inst.WASTE = lot.WASTE;
                            inst.DENSITY_WEFT = lot.DENSITY_WEFT;
                            inst.DELETEFLAG = lot.DELETEFLAG;
                            inst.DELETEBY = lot.DELETEBY;
                            inst.DELETEDATE = lot.DELETEDATE;

                            results.Add(inst);
                        }

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWeaving.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWeavingSampling
        private void RepWeavingSampling(string P_BEAMROLL, string P_LOOM)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(P_BEAMROLL) && !string.IsNullOrWhiteSpace(P_LOOM))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<WEAV_GETSAMPLINGDATA> lots =
                           WeavingDataService.Instance.WEAV_GETSAMPLINGDATA(P_BEAMROLL, P_LOOM);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.WeavingClassData.ListWeavingSampling> results = new List<DataControl.ClassData.WeavingClassData.ListWeavingSampling>();

                        for (int i = 0; i < lots.Count; i++)
                        {
                            WEAV_GETSAMPLINGDATA lot = lots[i];

                            DataControl.ClassData.WeavingClassData.ListWeavingSampling inst = new DataControl.ClassData.WeavingClassData.ListWeavingSampling();

                            inst.BEAMERROLL = lot.BEAMERROLL;
                            inst.LOOMNO = lot.LOOMNO;
                            inst.ITM_WEAVING = lot.ITM_WEAVING;

                            inst.SETTINGDATE = lot.SETTINGDATE;
                            inst.BARNO = lot.BARNO;
                            inst.SPIRAL_L = lot.SPIRAL_L;
                            inst.SPIRAL_R = lot.SPIRAL_R;
                            inst.STSAMPLING = lot.STSAMPLING;
                            inst.RECUTSAMPLING = lot.RECUTSAMPLING;
                            inst.STSAMPLINGBY = lot.STSAMPLINGBY;
                            inst.RECUTBY = lot.RECUTBY;
                            inst.STDATE = lot.STDATE;
                            inst.RECUTDATE = lot.RECUTDATE;
                            inst.REMARK = lot.REMARK;

                            inst.BEAMMC = lot.BEAMMC;
                            inst.WARPMC = lot.WARPMC;
                            inst.BEAMERNO = lot.BEAMERNO;

                            results.Add(inst);
                        }

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWeavingSampling.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepPackingLabel
        private void RepPackingLabel(string INSLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(INSLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    List<PACK_PRINTLABEL> lots =
                           PackingDataService.Instance.PACK_PRINTLABEL(INSLOT);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        //int? countDescription = 0;

                        PACK_PRINTLABEL lot = lots[0];

                        List<DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL> results = new List<DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL>();

                        DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL inst = new DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL();

                        //ย้ายไปข้างล้างตรวจเพิ่ม S
                        //inst.INSPECTIONLOT = lot.INSPECTIONLOT;

                        //inst.QUANTITY = lot.QUANTITY;

                        if (lot.QUANTITY != null)
                            inst.QUANTITY = MathEx.TruncateDecimal(lot.QUANTITY.Value, 1);
                        else
                            inst.QUANTITY = 0;

                        inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                        inst.NETWEIGHT = lot.NETWEIGHT;
                        inst.GRADE = lot.GRADE;
                        inst.ITEMCODE = lot.ITEMCODE;

                        //if (inst.ITEMCODE == "4746P25R" || inst.ITEMCODE == "09300" || inst.ITEMCODE == "09460" || inst.ITEMCODE == "09461" || inst.ITEMCODE == "09462"
                        //    || inst.ITEMCODE == "2L72C18R")
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.CUSTOMERPARTNO = "P" + lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = "P" + lot.CUSTOMERPARTNO;

                            inst.BATCHNO = "S" + lot.BATCHNO;
                            inst.BARCODEBACTHNO = "S" + lot.BATCHNO;

                            inst.StrQuantity = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = "H" + lot.INSPECTIONLOT;
                        }
                        else
                        {
                            inst.CUSTOMERPARTNO = lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = lot.BarcodeCMPARTNO;

                            inst.BATCHNO = lot.BATCHNO;
                            inst.BARCODEBACTHNO = lot.BARCODEBACTHNO;

                            inst.StrQuantity = inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                        }

                        //if (inst.ITEMCODE == "4746P25R" || inst.ITEMCODE == "2L72C18R")
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.FINISHINGPROCESS = lot.FINISHINGPROCESS;
                        }
                        else
                        {
                            inst.FINISHINGPROCESS = "";
                        }

                        inst.DESCRIPTION = lot.DESCRIPTION;
                        inst.SUPPLIERCODE = lot.SUPPLIERCODE;

                        inst.PDATE = lot.PDATE;
                        inst.CUSTOMERID = lot.CUSTOMERID;

                        //if (!string.IsNullOrEmpty(inst.DESCRIPTION))
                        //    countDescription = inst.DESCRIPTION.Length;

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPackingLabel.rdlc";

                            //if (countDescription <= 30)
                            //    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPackingLabel.rdlc";
                            //else
                            //    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPackingLabel2.rdlc";

                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            this._reportViewer.RefreshReport();

                            SetSize("Postcard", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepGreyRollDaily
        private void RepGreyRollDaily(string WEAVINGDATE, string CHINA)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(WEAVINGDATE))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<WEAV_GREYROLLDAILYREPORT> lots =
                           WeavingDataService.Instance.WEAV_GREYROLLDAILYREPORT(WEAVINGDATE, CHINA);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA> results = new List<DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA>();

                        for (int i = 0; i < lots.Count; i++)
                        {
                            WEAV_GREYROLLDAILYREPORT lot = lots[i];

                            DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA inst = new DataControl.ClassData.WeavingClassData.ListWEAVINGINGDATA();

                            inst.RowNo = i + 1;
                            inst.WEAVINGLOT = lot.WEAVINGLOT;
                            inst.ITM_WEAVING = lot.ITM_WEAVING;
                            inst.LENGTH = lot.LENGTH;
                            inst.LOOMNO = lot.LOOMNO;
                            inst.WEAVINGDATE = lot.WEAVINGDATE;
                            inst.SHIFT = lot.SHIFT;
                            inst.REMARK = lot.REMARK;
                            inst.CREATEDATE = lot.CREATEDATE;
                            inst.WIDTH = lot.WIDTH;
                            inst.PREPAREBY = lot.PREPAREBY;
                            inst.WEAVINGNO = lot.WEAVINGNO;

                            results.Add(inst);
                        }

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepGreyRollDaily.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepTransferSlip
        private void RepTransferSlip(string WARPHEADNO, string WARPLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(WARPHEADNO) && !string.IsNullOrWhiteSpace(WARPLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<WARP_TRANFERSLIP> lots =
                           WarpingDataService.Instance.WARP_TRANFERSLIP(WARPHEADNO, WARPLOT);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.WarpingClassData.ListWARP_TRANFERSLIP> results = new List<DataControl.ClassData.WarpingClassData.ListWARP_TRANFERSLIP>();

                        for (int i = 0; i < lots.Count; i++)
                        {
                            WARP_TRANFERSLIP lot = lots[i];

                            DataControl.ClassData.WarpingClassData.ListWARP_TRANFERSLIP inst = new DataControl.ClassData.WarpingClassData.ListWARP_TRANFERSLIP();

                            inst.WARPHEADNO = lot.WARPHEADNO;
                            inst.WARPERLOT = lot.WARPERLOT;
                            inst.BEAMNO = lot.BEAMNO;
                            inst.SIDE = lot.SIDE;
                            inst.STARTDATE = lot.STARTDATE;
                            inst.ENDDATE = lot.ENDDATE;
                            inst.LENGTH = lot.LENGTH;
                            inst.SPEED = lot.SPEED;
                            inst.HARDNESS_L = lot.HARDNESS_L;
                            inst.HARDNESS_N = lot.HARDNESS_N;
                            inst.HARDNESS_R = lot.HARDNESS_R;
                            inst.TENSION = lot.TENSION;
                            inst.STARTBY = lot.STARTBY;
                            inst.DOFFBY = lot.DOFFBY;
                            inst.FLAG = lot.FLAG;
                            inst.WARPMC = lot.WARPMC;

                            inst.ITM_PREPARE = lot.ITM_PREPARE;
                            inst.ITM_YARN = lot.ITM_YARN;

                            inst.REMARK = lot.REMARK;
                            inst.TENSION_IT = lot.TENSION_IT;
                            inst.TENSION_TAKEUP = lot.TENSION_TAKEUP;
                            inst.MC_COUNT_L = lot.MC_COUNT_L;
                            inst.MC_COUNT_S = lot.MC_COUNT_S;

                            results.Add(inst);
                        }

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepTransferSlip.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepBeamTransferSlip
        private void RepBeamTransferSlip(string BEAMLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(BEAMLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy HH:mm");

                    List<BEAM_TRANFERSLIP> lots =
                           BeamingDataService.Instance.BEAM_TRANFERSLIP(BEAMLOT);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.BeamingClassData.ListBEAM_TRANFERSLIP> results = new List<DataControl.ClassData.BeamingClassData.ListBEAM_TRANFERSLIP>();

                        for (int i = 0; i < lots.Count; i++)
                        {
                            BEAM_TRANFERSLIP lot = lots[i];

                            DataControl.ClassData.BeamingClassData.ListBEAM_TRANFERSLIP inst = new DataControl.ClassData.BeamingClassData.ListBEAM_TRANFERSLIP();

                            inst.BEAMERNO = lot.BEAMERNO;
                            inst.BEAMLOT = lot.BEAMLOT;
                            inst.BEAMNO = lot.BEAMNO;
                            inst.STARTDATE = lot.STARTDATE;
                            inst.ENDDATE = lot.ENDDATE;
                            inst.LENGTH = lot.LENGTH;
                            inst.SPEED = lot.SPEED;
                            inst.BEAMSTANDTENSION = lot.BEAMSTANDTENSION;
                            inst.WINDINGTENSION = lot.WINDINGTENSION;
                            inst.HARDNESS_L = lot.HARDNESS_L;
                            inst.HARDNESS_N = lot.HARDNESS_N;
                            inst.HARDNESS_R = lot.HARDNESS_R;
                            inst.INSIDE_WIDTH = lot.INSIDE_WIDTH;
                            inst.OUTSIDE_WIDTH = lot.OUTSIDE_WIDTH;
                            inst.FULL_WIDTH = lot.FULL_WIDTH;
                            inst.STARTBY = lot.STARTBY;
                            inst.DOFFBY = lot.DOFFBY;
                            inst.BEAMMC = lot.BEAMMC;
                            inst.FLAG = lot.FLAG;
                            inst.REMARK = lot.REMARK;
                            inst.ITM_PREPARE = lot.ITM_PREPARE;

                            results.Add(inst);
                        }

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepBeamTransferSlip.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepSampling
        private void RepSampling(string WEAVINGLOT, string finishinglot)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<FINISHING_GETSAMPLINGSHEET> lots = CoatingDataService.Instance.FINISHING_GETSAMPLINGSHEETList(WEAVINGLOT, finishinglot);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.FinishingClassData.ListSampling> results = new List<DataControl.ClassData.FinishingClassData.ListSampling>();

                        DataControl.ClassData.FinishingClassData.ListSampling inst = new DataControl.ClassData.FinishingClassData.ListSampling();

                        inst.WEAVINGLOT = lots[0].WEAVINGLOT;
                        inst.FINISHINGLOT = lots[0].FINISHINGLOT;
                        inst.ITM_CODE = lots[0].ITM_CODE;
                        inst.CREATEDATE = lots[0].CREATEDATE;
                        inst.CREATEBY = lots[0].CREATEBY;
                        inst.PRODUCTID = lots[0].PRODUCTID;
                        inst.SAMPLING_WIDTH = lots[0].SAMPLING_WIDTH;
                        inst.SAMPLING_LENGTH = lots[0].SAMPLING_LENGTH;
                        inst.PROCESS = lots[0].PROCESS;
                        inst.REMARK = lots[0].REMARK;
                        inst.FABRICTYPE = lots[0].FABRICTYPE;
                        inst.PRODUCTNAME = lots[0].PRODUCTNAME;

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepSampling.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepFMQC1806
        private void RepFMQC1806(string WEAVINGLOT,string ITM_CODE)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    this._reportViewer.LocalReport.DataSources.Clear();

                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFMQC1806.rdlc";
                    this._reportViewer.LocalReport.EnableExternalImages = true;

                    IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("WEAVINGLOT", WEAVINGLOT));
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ITM_CODE", ITM_CODE));
                    this._reportViewer.LocalReport.SetParameters(parameters);

                    this._reportViewer.RefreshReport();

                    SetSize("A4", false);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepEMQC1906
        private void RepEMQC1906(string WEAVINGLOT,string ITM_CODE)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    this._reportViewer.LocalReport.DataSources.Clear();

                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepEMQC1906.rdlc";
                    this._reportViewer.LocalReport.EnableExternalImages = true;

                    IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("WEAVINGLOT", WEAVINGLOT));
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ITM_CODE", ITM_CODE));
                    this._reportViewer.LocalReport.SetParameters(parameters);

                    this._reportViewer.RefreshReport();

                    SetSize("A4", false);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWarpingRecord
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_WARPHEADNO"></param>
        /// <param name="P_SIDE"></param>
        /// <param name="P_WARPMC"></param>
        /// <param name="P_ITM_PREPARE"></param>
        /// <param name="P_WTYPE"></param>
        /// <param name="P_CONDITIONBY"></param>
        /// <param name="P_REEDNO"></param>
        /// <param name="P_CONDITIONSTART"></param>
        private void RepWarpingRecord(string P_WARPHEADNO, string P_SIDE, string P_WARPMC, string P_ITM_PREPARE,
            string P_WTYPE, string P_CONDITIONBY, string P_REEDNO, DateTime? P_CONDITIONSTART)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(P_WARPHEADNO))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    WarpingRecordHead lots = WarpingDataService.Instance.WarpingRecordHead(P_WARPHEADNO, P_SIDE, P_WARPMC, P_ITM_PREPARE,
                          P_WTYPE, P_CONDITIONBY, P_REEDNO, P_CONDITIONSTART);

                    if (null != lots)
                    {
                        string remark = string.Empty;

                        #region WarpingRecordHead

                        List<DataControl.ClassData.WarpingClassData.ListWarpingRecordHead> results = new List<DataControl.ClassData.WarpingClassData.ListWarpingRecordHead>();

                        DataControl.ClassData.WarpingClassData.ListWarpingRecordHead inst = new DataControl.ClassData.WarpingClassData.ListWarpingRecordHead();

                        inst.WARPHEADNO = lots.WARPHEADNO;
                        inst.SIDE = lots.SIDE;
                        inst.WARPMC = lots.WARPMC;
                        inst.ITM_PREPARE = lots.ITM_PREPARE;

                        inst.WTYPE = lots.WTYPE;
                        inst.CONDITIONBY = lots.CONDITIONBY;
                        inst.REEDNO = lots.REEDNO;
                        inst.CONDITIONSTART = lots.CONDITIONSTART;

                        results.Add(inst);

                        #endregion

                        #region WARP_GETCREELSETUPDETAIL

                        List<WARP_GETCREELSETUPDETAIL> dataCREELSETUPDETAIL = WarpingDataService.Instance.WARP_GETCREELSETUPDETAIL(P_WARPHEADNO);
                        List<DataControl.ClassData.WarpingClassData.ListWARP_GETCREELSETUPDETAIL> resultsGETCREELSETUPDETAIL = new List<DataControl.ClassData.WarpingClassData.ListWARP_GETCREELSETUPDETAIL>();

                        if (dataCREELSETUPDETAIL.Count > 0 && dataCREELSETUPDETAIL != null)
                        {
                            for (int i = 0; i < dataCREELSETUPDETAIL.Count; i++)
                            {
                                WARP_GETCREELSETUPDETAIL lot = dataCREELSETUPDETAIL[i];

                                DataControl.ClassData.WarpingClassData.ListWARP_GETCREELSETUPDETAIL instGETCREELSETUPDETAIL = new DataControl.ClassData.WarpingClassData.ListWARP_GETCREELSETUPDETAIL();

                                instGETCREELSETUPDETAIL.PALLETNO = lot.PALLETNO;
                                instGETCREELSETUPDETAIL.RECEIVECH = lot.RECEIVECH;
                                instGETCREELSETUPDETAIL.USEDCH = lot.USEDCH;
                                instGETCREELSETUPDETAIL.REJECTCH = lot.REJECTCH;
                                instGETCREELSETUPDETAIL.PREJECT = lot.PREJECT;
                                instGETCREELSETUPDETAIL.ITM_YARN = lot.ITM_YARN;
                                instGETCREELSETUPDETAIL.RECEIVEDATE = lot.RECEIVEDATE;
                                instGETCREELSETUPDETAIL.PUSED = lot.PUSED;

                                resultsGETCREELSETUPDETAIL.Add(instGETCREELSETUPDETAIL);
                            }
                        }

                        #endregion

                        #region WARP_GETWARPERLOTBYHEADNO

                        List<WARP_GETWARPERLOTBYHEADNO> dataWARPERLOTBYHEADNO = WarpingDataService.Instance.WARP_GETWARPERLOTBYHEADNO(P_WARPHEADNO);
                        List<DataControl.ClassData.WarpingClassData.ListWARP_GETWARPERLOTBYHEADNO> resultsGETWARPERLOTBYHEADNO = new List<DataControl.ClassData.WarpingClassData.ListWARP_GETWARPERLOTBYHEADNO>();

                        if (dataWARPERLOTBYHEADNO.Count > 0 && dataWARPERLOTBYHEADNO != null)
                        {
                            for (int i = 0; i < dataWARPERLOTBYHEADNO.Count; i++)
                            {
                                WARP_GETWARPERLOTBYHEADNO lot = dataWARPERLOTBYHEADNO[i];

                                DataControl.ClassData.WarpingClassData.ListWARP_GETWARPERLOTBYHEADNO instGETWARPERLOTBYHEADNO = new DataControl.ClassData.WarpingClassData.ListWARP_GETWARPERLOTBYHEADNO();

                                instGETWARPERLOTBYHEADNO.WARPHEADNO = lot.WARPHEADNO;
                                instGETWARPERLOTBYHEADNO.WARPERLOT = lot.WARPERLOT;
                                instGETWARPERLOTBYHEADNO.BEAMNO = lot.BEAMNO;
                                instGETWARPERLOTBYHEADNO.SIDE = lot.SIDE;
                                instGETWARPERLOTBYHEADNO.STARTDATE = lot.STARTDATE;
                                instGETWARPERLOTBYHEADNO.ENDDATE = lot.ENDDATE;
                                instGETWARPERLOTBYHEADNO.LENGTH = lot.LENGTH;
                                instGETWARPERLOTBYHEADNO.SPEED = lot.SPEED;
                                instGETWARPERLOTBYHEADNO.HARDNESS_L = lot.HARDNESS_L;
                                instGETWARPERLOTBYHEADNO.HARDNESS_N = lot.HARDNESS_N;
                                instGETWARPERLOTBYHEADNO.HARDNESS_R = lot.HARDNESS_R;
                                instGETWARPERLOTBYHEADNO.TENSION = lot.TENSION;
                                instGETWARPERLOTBYHEADNO.STARTBY = lot.STARTBY;
                                instGETWARPERLOTBYHEADNO.DOFFBY = lot.DOFFBY;
                                instGETWARPERLOTBYHEADNO.FLAG = lot.FLAG;
                                instGETWARPERLOTBYHEADNO.WARPMC = lot.WARPMC;
                                instGETWARPERLOTBYHEADNO.REMARK = lot.REMARK;
                                instGETWARPERLOTBYHEADNO.TENSION_IT = lot.TENSION_IT;
                                instGETWARPERLOTBYHEADNO.TENSION_TAKEUP = lot.TENSION_TAKEUP;
                                instGETWARPERLOTBYHEADNO.MC_COUNT_L = lot.MC_COUNT_L;
                                instGETWARPERLOTBYHEADNO.MC_COUNT_S = lot.MC_COUNT_S;

                                instGETWARPERLOTBYHEADNO.EDITDATE = lot.EDITDATE;
                                instGETWARPERLOTBYHEADNO.EDITBY = lot.EDITBY;

                                if (lot.KEBA != null)
                                    instGETWARPERLOTBYHEADNO.KEBA = lot.KEBA;
                                else
                                    instGETWARPERLOTBYHEADNO.KEBA = 0;

                                if (lot.TIGHTEND != null)
                                    instGETWARPERLOTBYHEADNO.TIGHTEND = lot.TIGHTEND;
                                else
                                    instGETWARPERLOTBYHEADNO.TIGHTEND = 0;

                                if (lot.MISSYARN != null)
                                    instGETWARPERLOTBYHEADNO.MISSYARN = lot.MISSYARN;
                                else
                                    instGETWARPERLOTBYHEADNO.MISSYARN = 0;

                                if (lot.OTHER != null)
                                    instGETWARPERLOTBYHEADNO.OTHER = lot.OTHER;
                                else
                                    instGETWARPERLOTBYHEADNO.OTHER = 0;

                                if (!string.IsNullOrEmpty(lot.REMARK))
                                {
                                    remark += lot.WARPERLOT +"-"+lot.REMARK+"\r\n";
                                }

                                resultsGETWARPERLOTBYHEADNO.Add(instGETWARPERLOTBYHEADNO);
                            }
                        }

                        #endregion

                        #region WARP_GETSPECBYCHOPNOANDMC

                        List<WARP_GETSPECBYCHOPNOANDMC> dataSPECBYCHOPNOANDMC = WarpingDataService.Instance.WARP_GETSPECBYCHOPNOANDMC(P_ITM_PREPARE, P_WARPMC);

                        List<DataControl.ClassData.WarpingClassData.ListWARP_GETSPECBYCHOPNOANDMC> resultsSPECBYCHOPNOANDMC = new List<DataControl.ClassData.WarpingClassData.ListWARP_GETSPECBYCHOPNOANDMC>();

                        if (dataSPECBYCHOPNOANDMC.Count > 0 && dataSPECBYCHOPNOANDMC != null)
                        {

                            for (int i = 0; i < dataSPECBYCHOPNOANDMC.Count; i++)
                            {
                                WARP_GETSPECBYCHOPNOANDMC lot = dataSPECBYCHOPNOANDMC[i];

                                DataControl.ClassData.WarpingClassData.ListWARP_GETSPECBYCHOPNOANDMC instSPECBYCHOPNOANDMC = new DataControl.ClassData.WarpingClassData.ListWARP_GETSPECBYCHOPNOANDMC();

                                instSPECBYCHOPNOANDMC.CHOPNO = lot.CHOPNO;

                                instSPECBYCHOPNOANDMC.ITM_YARN = lot.ITM_YARN;
                                instSPECBYCHOPNOANDMC.WARPERENDS = lot.WARPERENDS;

                                instSPECBYCHOPNOANDMC.MAXLENGTH = lot.MAXLENGTH;
                                instSPECBYCHOPNOANDMC.MINLENGTH = lot.MINLENGTH;

                                instSPECBYCHOPNOANDMC.WAXING = lot.WAXING;
                                instSPECBYCHOPNOANDMC.COMBTYPE = lot.COMBTYPE;
                                instSPECBYCHOPNOANDMC.COMBPITCH = lot.COMBPITCH;
                                instSPECBYCHOPNOANDMC.KEBAYARN = lot.KEBAYARN;
                                instSPECBYCHOPNOANDMC.NOWARPBEAM = lot.NOWARPBEAM;

                                instSPECBYCHOPNOANDMC.MAXHARDNESS = lot.MAXHARDNESS;
                                instSPECBYCHOPNOANDMC.MINHARDNESS = lot.MINHARDNESS;

                                instSPECBYCHOPNOANDMC.MCNO = lot.MCNO;

                                instSPECBYCHOPNOANDMC.SPEED = lot.SPEED;
                                instSPECBYCHOPNOANDMC.SPEED_MARGIN = lot.SPEED_MARGIN;

                                instSPECBYCHOPNOANDMC.YARN_TENSION = lot.YARN_TENSION;
                                instSPECBYCHOPNOANDMC.YARN_TENSION_MARGIN = lot.YARN_TENSION_MARGIN;

                                instSPECBYCHOPNOANDMC.WINDING_TENSION = lot.WINDING_TENSION;
                                instSPECBYCHOPNOANDMC.WINDING_TENSION_MARGIN = lot.WINDING_TENSION_MARGIN;

                                instSPECBYCHOPNOANDMC.NOCH = lot.NOCH;

                                resultsSPECBYCHOPNOANDMC.Add(instSPECBYCHOPNOANDMC);
                            }
                        }

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            rds.Value = results;

                            Microsoft.Reporting.WinForms.ReportDataSource rdsGETCREELSETUPDETAIL = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsGETCREELSETUPDETAIL);
                            rdsGETCREELSETUPDETAIL.Value = resultsGETCREELSETUPDETAIL;

                            Microsoft.Reporting.WinForms.ReportDataSource rdsGETWARPERLOTBYHEADNO = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsGETWARPERLOTBYHEADNO);
                            rdsGETWARPERLOTBYHEADNO.Value = resultsGETWARPERLOTBYHEADNO;

                            Microsoft.Reporting.WinForms.ReportDataSource rdsSPECBYCHOPNOANDMC = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsSPECBYCHOPNOANDMC);
                            rdsSPECBYCHOPNOANDMC.Value = resultsSPECBYCHOPNOANDMC;

                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rdsGETCREELSETUPDETAIL);
                            this._reportViewer.LocalReport.DataSources.Add(rdsGETWARPERLOTBYHEADNO);
                            this._reportViewer.LocalReport.DataSources.Add(rdsSPECBYCHOPNOANDMC);

                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWarpingRecord.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("Remark", remark));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region RepBeamingRecord
        private void RepBeamingRecord(string P_BEAMERNO, string P_ITM_PREPARE, string P_MCNO, decimal? P_TOTALYARN, decimal? P_TOTALKEBA, decimal? P_ADJUSTKEBA
                , DateTime? P_STARTDATE, DateTime? P_ENDDATE, string P_REMARK)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(P_BEAMERNO))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    BeamingRecordHead lots = BeamingDataService.Instance.BeamingRecordHead( P_BEAMERNO, P_ITM_PREPARE, P_MCNO, P_TOTALYARN, P_TOTALKEBA, P_ADJUSTKEBA , P_STARTDATE, P_ENDDATE, P_REMARK);

                    if (null != lots)
                    {
                        string remark = string.Empty;

                        #region BeamingRecordHead

                        List<DataControl.ClassData.BeamingClassData.ListBeamingRecordHead> results = new List<DataControl.ClassData.BeamingClassData.ListBeamingRecordHead>();

                        DataControl.ClassData.BeamingClassData.ListBeamingRecordHead inst = new DataControl.ClassData.BeamingClassData.ListBeamingRecordHead();

                        inst.BEAMERNO = lots.BEAMERNO;
                        inst.ITM_PREPARE = lots.ITM_PREPARE;
                        inst.MCNO = lots.MCNO;
                        inst.TOTALYARN = lots.TOTALYARN;
                        inst.TOTALKEBA = lots.TOTALKEBA;
                        inst.ADJUSTKEBA = lots.ADJUSTKEBA;
                        inst.STARTDATE = lots.STARTDATE;
                        inst.ENDDATE = lots.ENDDATE;
                        inst.REMARK = lots.REMARK;

                        results.Add(inst);

                        #endregion

                        #region BEAM_GETWARPROLLBYBEAMERNO

                        List<BEAM_GETWARPROLLBYBEAMERNO> dataWARPROLLBYBEAMERNO = BeamingDataService.Instance.BEAM_GETWARPROLLBYBEAMERNO(P_BEAMERNO);
                        List<DataControl.ClassData.BeamingClassData.ListBEAM_GETWARPROLLBYBEAMERNO> resultsWARPROLLBYBEAMERNO = new List<DataControl.ClassData.BeamingClassData.ListBEAM_GETWARPROLLBYBEAMERNO>();

                        if (dataWARPROLLBYBEAMERNO.Count > 0 && dataWARPROLLBYBEAMERNO != null)
                        {
                            for (int i = 0; i < dataWARPROLLBYBEAMERNO.Count; i++)
                            {
                                BEAM_GETWARPROLLBYBEAMERNO lot = dataWARPROLLBYBEAMERNO[i];

                                DataControl.ClassData.BeamingClassData.ListBEAM_GETWARPROLLBYBEAMERNO instWARPROLLBYBEAMERNO = new DataControl.ClassData.BeamingClassData.ListBEAM_GETWARPROLLBYBEAMERNO();

                                instWARPROLLBYBEAMERNO.BEAMERNO = lot.BEAMERNO;
                                instWARPROLLBYBEAMERNO.WARPHEADNO = lot.WARPHEADNO;
                                instWARPROLLBYBEAMERNO.WARPERLOT = lot.WARPERLOT;
             
                                resultsWARPROLLBYBEAMERNO.Add(instWARPROLLBYBEAMERNO);
                            }
                        }

                        #endregion

                        #region BEAM_GETBEAMLOTBYBEAMERNO

                        List<BEAM_GETBEAMLOTBYBEAMERNO> dataBEAMLOTBYBEAMERNO = BeamingDataService.Instance.BEAM_GETBEAMLOTBYBEAMERNO(P_BEAMERNO);
                        List<DataControl.ClassData.BeamingClassData.ListBEAM_GETBEAMLOTBYBEAMERNO> resultsBEAMLOTBYBEAMERNO = new List<DataControl.ClassData.BeamingClassData.ListBEAM_GETBEAMLOTBYBEAMERNO>();

                        if (dataBEAMLOTBYBEAMERNO.Count > 0 && dataBEAMLOTBYBEAMERNO != null)
                        {
                            
                            for (int i = 0; i < dataBEAMLOTBYBEAMERNO.Count; i++)
                            {
                                BEAM_GETBEAMLOTBYBEAMERNO lot = dataBEAMLOTBYBEAMERNO[i];

                                DataControl.ClassData.BeamingClassData.ListBEAM_GETBEAMLOTBYBEAMERNO instBEAMLOTBYBEAMERNO = new DataControl.ClassData.BeamingClassData.ListBEAM_GETBEAMLOTBYBEAMERNO();

                                instBEAMLOTBYBEAMERNO.BEAMERNO = lot.BEAMERNO;
                                instBEAMLOTBYBEAMERNO.BEAMLOT = lot.BEAMLOT;
                                instBEAMLOTBYBEAMERNO.BEAMNO = lot.BEAMNO;

                                instBEAMLOTBYBEAMERNO.STARTDATE = lot.STARTDATE;
                                instBEAMLOTBYBEAMERNO.ENDDATE = lot.ENDDATE;
                                instBEAMLOTBYBEAMERNO.LENGTH = lot.LENGTH;
                                instBEAMLOTBYBEAMERNO.SPEED = lot.SPEED;
                                instBEAMLOTBYBEAMERNO.HARDNESS_L = lot.HARDNESS_L;
                                instBEAMLOTBYBEAMERNO.HARDNESS_N = lot.HARDNESS_N;
                                instBEAMLOTBYBEAMERNO.HARDNESS_R = lot.HARDNESS_R;
                                instBEAMLOTBYBEAMERNO.BEAMSTANDTENSION = lot.BEAMSTANDTENSION;

                                instBEAMLOTBYBEAMERNO.BEAMSTANDTENSION = lot.BEAMSTANDTENSION;
                                instBEAMLOTBYBEAMERNO.WINDINGTENSION = lot.WINDINGTENSION;
                                instBEAMLOTBYBEAMERNO.INSIDE_WIDTH = lot.INSIDE_WIDTH;
                                instBEAMLOTBYBEAMERNO.OUTSIDE_WIDTH = lot.OUTSIDE_WIDTH;
                                instBEAMLOTBYBEAMERNO.FULL_WIDTH = lot.FULL_WIDTH;

                                instBEAMLOTBYBEAMERNO.STARTBY = lot.STARTBY;
                                instBEAMLOTBYBEAMERNO.DOFFBY = lot.DOFFBY;
                                instBEAMLOTBYBEAMERNO.FLAG = lot.FLAG;
                                instBEAMLOTBYBEAMERNO.BEAMMC = lot.BEAMMC;

                                instBEAMLOTBYBEAMERNO.REMARK = lot.REMARK;
                                instBEAMLOTBYBEAMERNO.TENSION_ST1 = lot.TENSION_ST1;
                                instBEAMLOTBYBEAMERNO.TENSION_ST2 = lot.TENSION_ST2;
                                instBEAMLOTBYBEAMERNO.TENSION_ST3 = lot.TENSION_ST3;
                                instBEAMLOTBYBEAMERNO.TENSION_ST4 = lot.TENSION_ST4;
                                instBEAMLOTBYBEAMERNO.TENSION_ST5 = lot.TENSION_ST5;
                                instBEAMLOTBYBEAMERNO.TENSION_ST6 = lot.TENSION_ST6;
                                instBEAMLOTBYBEAMERNO.TENSION_ST7 = lot.TENSION_ST7;
                                instBEAMLOTBYBEAMERNO.TENSION_ST8 = lot.TENSION_ST8;
                                instBEAMLOTBYBEAMERNO.TENSION_ST9 = lot.TENSION_ST9;
                                instBEAMLOTBYBEAMERNO.TENSION_ST10 = lot.TENSION_ST10;

                                instBEAMLOTBYBEAMERNO.EDITDATE = lot.EDITDATE;
                                instBEAMLOTBYBEAMERNO.EDITBY = lot.EDITBY;
                                instBEAMLOTBYBEAMERNO.OLDBEAMNO = lot.OLDBEAMNO;

                                if (lot.KEBA != null)
                                    instBEAMLOTBYBEAMERNO.KEBA = lot.KEBA;
                                else
                                    instBEAMLOTBYBEAMERNO.KEBA = 0;

                                if (lot.MISSYARN != null)
                                    instBEAMLOTBYBEAMERNO.MISSYARN = lot.MISSYARN;
                                else
                                    instBEAMLOTBYBEAMERNO.MISSYARN = 0;

                                if (lot.OTHER != null)
                                    instBEAMLOTBYBEAMERNO.OTHER = lot.OTHER;
                                else
                                    instBEAMLOTBYBEAMERNO.OTHER = 0;

                                if (!string.IsNullOrEmpty(lot.REMARK))
                                {
                                    remark += lot.BEAMLOT + "-" + lot.REMARK + "\r\n";
                                }

                                resultsBEAMLOTBYBEAMERNO.Add(instBEAMLOTBYBEAMERNO);
                            }
                        }

                        #endregion

                        #region BEAM_GETSPECBYCHOPNO

                        List<BEAM_GETSPECBYCHOPNO> dataSPECBYCHOPNO = BeamingDataService.Instance.BEAM_GETSPECBYCHOPNO(P_ITM_PREPARE);
                        List<DataControl.ClassData.BeamingClassData.ListBEAM_GETSPECBYCHOPNO> resultsSPECBYCHOPNO = new List<DataControl.ClassData.BeamingClassData.ListBEAM_GETSPECBYCHOPNO>();

                        if (dataSPECBYCHOPNO.Count > 0 && dataSPECBYCHOPNO != null)
                        {
                            for (int i = 0; i < dataSPECBYCHOPNO.Count; i++)
                            {
                                BEAM_GETSPECBYCHOPNO lot = dataSPECBYCHOPNO[i];

                                DataControl.ClassData.BeamingClassData.ListBEAM_GETSPECBYCHOPNO instSPECBYCHOPNO = new DataControl.ClassData.BeamingClassData.ListBEAM_GETSPECBYCHOPNO();

                                instSPECBYCHOPNO.CHOPNO = lot.CHOPNO;
                                instSPECBYCHOPNO.NOWARPBEAM = lot.NOWARPBEAM;
                                instSPECBYCHOPNO.TOTALYARN = lot.TOTALYARN;
                                instSPECBYCHOPNO.TOTALKEBA = lot.TOTALKEBA;
                                instSPECBYCHOPNO.BEAMLENGTH = lot.BEAMLENGTH;
                                instSPECBYCHOPNO.MAXHARDNESS = lot.MAXHARDNESS;
                                instSPECBYCHOPNO.MINHARDNESS = lot.MINHARDNESS;
                                instSPECBYCHOPNO.MAXBEAMWIDTH = lot.MAXBEAMWIDTH;
                                instSPECBYCHOPNO.MINBEAMWIDTH = lot.MINBEAMWIDTH;
                                instSPECBYCHOPNO.MAXSPEED = lot.MAXSPEED;
                                instSPECBYCHOPNO.MINSPEED = lot.MINSPEED;
                                instSPECBYCHOPNO.MAXYARNTENSION = lot.MAXYARNTENSION;
                                instSPECBYCHOPNO.MINYARNTENSION = lot.MINYARNTENSION;
                                instSPECBYCHOPNO.MAXWINDTENSION = lot.MAXWINDTENSION;
                                instSPECBYCHOPNO.MINWINDTENSION = lot.MINWINDTENSION;
                                instSPECBYCHOPNO.COMBTYPE = lot.COMBTYPE;
                                instSPECBYCHOPNO.COMBPITCH = lot.COMBPITCH;
                                instSPECBYCHOPNO.TOTALBEAM = lot.TOTALBEAM;

                                resultsSPECBYCHOPNO.Add(instSPECBYCHOPNO);
                            }
                        }

                        #endregion

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                            rds.Value = results;

                            Microsoft.Reporting.WinForms.ReportDataSource rdsWARPROLLBYBEAMERNO = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultsWARPROLLBYBEAMERNO);
                            rdsWARPROLLBYBEAMERNO.Value = resultsWARPROLLBYBEAMERNO;

                            Microsoft.Reporting.WinForms.ReportDataSource rdsBEAMLOTBYBEAMERNO = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", resultsBEAMLOTBYBEAMERNO);
                            rdsBEAMLOTBYBEAMERNO.Value = resultsBEAMLOTBYBEAMERNO;

                            Microsoft.Reporting.WinForms.ReportDataSource rdsSPECBYCHOPNO = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", resultsSPECBYCHOPNO);
                            rdsSPECBYCHOPNO.Value = resultsSPECBYCHOPNO;

                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.DataSources.Add(rdsWARPROLLBYBEAMERNO);
                            this._reportViewer.LocalReport.DataSources.Add(rdsBEAMLOTBYBEAMERNO);
                            this._reportViewer.LocalReport.DataSources.Add(rdsSPECBYCHOPNO);

                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepBeamingRecord.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("Remark", remark));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", true);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepFinishRecord
        private void RepFinishRecord(string P_DATE, string P_MCNO,string MCName)
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
                string FinishingDate = P_DATE;

                List<FINISHING_SEARCHFINISHRECORD> lots =
                       FinishingDataService.Instance.FINISHING_SEARCHFINISHRECORD( P_DATE,  P_MCNO);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.FinishingClassData.ListFINISHRECORD> results = new List<DataControl.ClassData.FinishingClassData.ListFINISHRECORD>();

                    for (int i = 0; i < lots.Count; i++)
                    {
                        FINISHING_SEARCHFINISHRECORD lot = lots[i];

                        DataControl.ClassData.FinishingClassData.ListFINISHRECORD inst = new DataControl.ClassData.FinishingClassData.ListFINISHRECORD();

                        inst.RowNo = lot.RowNo;
                        inst.WEAVINGLOT = lot.WEAVINGLOT;
                        inst.FINISHINGLOT = lot.FINISHINGLOT;
                        inst.ITM_CODE = lot.ITM_CODE;
                        inst.STARTDATE = lot.STARTDATE;
                        inst.ENDDATE = lot.ENDDATE;
                        inst.FINISHBY = lot.FINISHBY;
                        inst.STARTBY = lot.STARTBY;
                        inst.CONDITIONBY = lot.CONDITIONBY;
                        inst.MCNO = lot.MCNO;
                        inst.MC = lot.MC;
                        inst.WEAVLENGTH = lot.WEAVLENGTH;
                        inst.WIDTH_BE = lot.WIDTH_BE;
                        inst.WIDTH_AF = lot.WIDTH_AF;
                        inst.OPERATOR_GROUP = lot.OPERATOR_GROUP;
                        inst.LENGTH1 = lot.LENGTH1;
                        inst.LENGTH2 = lot.LENGTH2;
                        inst.LENGTH3 = lot.LENGTH3;
                        inst.LENGTH4 = lot.LENGTH4;
                        inst.LENGTH5 = lot.LENGTH5;
                        inst.LENGTH6 = lot.LENGTH6;
                        inst.LENGTH7 = lot.LENGTH7;
                        inst.TOTALLENGTH = lot.TOTALLENGTH;
                        inst.FinishingLength = lot.FinishingLength;

                        //New 17/07/19
                        inst.PRODUCTIONTYPE = lot.PRODUCTIONTYPE;

                        results.Add(inst);
                    }

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFinishRecord.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("FinishingDate", FinishingDate));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("MCName", MCName));
                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepLabFinishRecord
        private void RepLabFinishRecord(string P_DATE, string P_MCNO, string ITM_Code, string MCName)
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
                string FinishingDate = P_DATE;

                List<FINISHING_SEARCHFINISHRECORD> lots =
                       FinishingDataService.Instance.FINISHING_SEARCHFINISHRECORD(P_DATE, P_MCNO, ITM_Code);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.FinishingClassData.ListFINISHRECORD> results = new List<DataControl.ClassData.FinishingClassData.ListFINISHRECORD>();

                    for (int i = 0; i < lots.Count; i++)
                    {
                        FINISHING_SEARCHFINISHRECORD lot = lots[i];

                        DataControl.ClassData.FinishingClassData.ListFINISHRECORD inst = new DataControl.ClassData.FinishingClassData.ListFINISHRECORD();

                        inst.RowNo = lot.RowNo;
                        inst.WEAVINGLOT = lot.WEAVINGLOT;
                        inst.FINISHINGLOT = lot.FINISHINGLOT;
                        inst.ITM_CODE = lot.ITM_CODE;
                        inst.STARTDATE = lot.STARTDATE;
                        inst.ENDDATE = lot.ENDDATE;
                        inst.FINISHBY = lot.FINISHBY;
                        inst.STARTBY = lot.STARTBY;
                        inst.CONDITIONBY = lot.CONDITIONBY;
                        inst.MCNO = lot.MCNO;
                        inst.MC = lot.MC;
                        inst.WEAVLENGTH = lot.WEAVLENGTH;
                        inst.WIDTH_BE = lot.WIDTH_BE;
                        inst.WIDTH_AF = lot.WIDTH_AF;
                        inst.OPERATOR_GROUP = lot.OPERATOR_GROUP;
                        inst.LENGTH1 = lot.LENGTH1;
                        inst.LENGTH2 = lot.LENGTH2;
                        inst.LENGTH3 = lot.LENGTH3;
                        inst.LENGTH4 = lot.LENGTH4;
                        inst.LENGTH5 = lot.LENGTH5;
                        inst.LENGTH6 = lot.LENGTH6;
                        inst.LENGTH7 = lot.LENGTH7;
                        inst.TOTALLENGTH = lot.TOTALLENGTH;
                        inst.FinishingLength = lot.FinishingLength;

                        //New 17/07/19
                        inst.PRODUCTIONTYPE = lot.PRODUCTIONTYPE;

                        results.Add(inst);
                    }

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFinishRecord.rdlc";
                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("FinishingDate", FinishingDate));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("MCName", MCName));
                        this._reportViewer.LocalReport.SetParameters(parameters);

                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWeavingHistory
        private void RepWeavingHistory(string P_LOOMNO, string P_BEAMERROLL, string P_BEAMERNO, string P_BARNO
            , string P_ITMWEAVING, string P_FINISHDATE, string P_STARTDATE, string P_WEFTYARN
            , decimal? P_WIDTH, decimal? P_BEAMLENGTH, decimal? P_SPEED)
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                List<DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION> results = new List<DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION>();

                DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION instList = new DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION();

                instList.BEAMLOT = P_BEAMERROLL;
                instList.MC = P_LOOMNO;
                //instList.REEDNO2 = lot.REEDNO2;
                instList.WEFTYARN = P_WEFTYARN;
                //instList.TEMPLETYPE = lot.TEMPLETYPE;
                instList.BARNO = P_BARNO;

                if (!string.IsNullOrEmpty(P_STARTDATE))
                    instList.STARTDATE = DateTime.Parse(P_STARTDATE);
                if (!string.IsNullOrEmpty(P_FINISHDATE))
                    instList.FINISHDATE = DateTime.Parse(P_FINISHDATE);

                //instList.FINISHFLAG = lot.FINISHFLAG;
                //instList.SETTINGBY = lot.SETTINGBY;
                //instList.EDITDATE = lot.EDITDATE;
                //instList.EDITBY = lot.EDITBY;
                instList.ITM_WEAVING = P_ITMWEAVING;
                //instList.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                instList.WIDTH = P_WIDTH;
                instList.BEAMLENGTH = P_BEAMLENGTH;
                instList.SPEED = P_SPEED;

                instList.BEAMERNO = P_BEAMERNO;

                results.Add(instList);


                List<WEAV_GETWEAVELISTBYBEAMROLL> lotByBeamRolls = WeavingDataService.Instance.WEAV_GETWEAVELISTBYBEAMROLL(P_BEAMERROLL, P_LOOMNO);
                List<DataControl.ClassData.WeavingClassData.ListWEAV_GETWEAVELISTBYBEAMROLL> resultByBeamRolls = new List<DataControl.ClassData.WeavingClassData.ListWEAV_GETWEAVELISTBYBEAMROLL>();

                if (null != lotByBeamRolls && lotByBeamRolls.Count > 0 && null != lotByBeamRolls[0])
                {
                    for (int i = 0; i < lotByBeamRolls.Count; i++)
                    {
                        WEAV_GETWEAVELISTBYBEAMROLL dbResult = lotByBeamRolls[i];

                        DataControl.ClassData.WeavingClassData.ListWEAV_GETWEAVELISTBYBEAMROLL inst = new DataControl.ClassData.WeavingClassData.ListWEAV_GETWEAVELISTBYBEAMROLL();

                        inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.WEAVINGDATE = dbResult.WEAVINGDATE;
                        inst.SHIFT = dbResult.SHIFT;
                        inst.REMARK = dbResult.REMARK;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.PREPAREBY = dbResult.PREPAREBY;
                        inst.WEAVINGNO = dbResult.WEAVINGNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.DOFFNO = dbResult.DOFFNO;

                        inst.TENSION = dbResult.TENSION;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.SPEED = dbResult.SPEED;
                        inst.WASTE = dbResult.WASTE;

                        inst.DENSITY_WARP = dbResult.DENSITY_WARP;
                        inst.DENSITY_WEFT = dbResult.DENSITY_WEFT;

                        inst.DELETEFLAG = dbResult.DELETEFLAG;
                        inst.DELETEBY = dbResult.DELETEBY;
                        inst.DELETEDATE = dbResult.DELETEDATE;

                        if (!string.IsNullOrEmpty(inst.DELETEFLAG))
                        {
                            if (inst.DELETEFLAG == "1")
                                inst.DeleteHistory = "No";
                            else if (inst.DELETEFLAG == "0")
                                inst.DeleteHistory = "Yes";
                        }

                        resultByBeamRolls.Add(inst);
                    }
                }

                if (results.Count > 0 || resultByBeamRolls.Count > 0)
                {
                    Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                    Microsoft.Reporting.WinForms.ReportDataSource rdByBeamRolls = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultByBeamRolls);

                    rds.Value = results;
                    rdByBeamRolls.Value = resultByBeamRolls;

                    this._reportViewer.LocalReport.DataSources.Clear();
                    this._reportViewer.LocalReport.DataSources.Add(rds);
                    this._reportViewer.LocalReport.DataSources.Add(rdByBeamRolls);
                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWeavingHistory.rdlc";
                    this._reportViewer.LocalReport.EnableExternalImages = true;

                    IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                    this._reportViewer.LocalReport.SetParameters(parameters);

                    this._reportViewer.RefreshReport();

                    SetSize("A4", true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWeavingRecord
        private void RepWeavingRecord(string P_LOOMNO, string P_BEAMERROLL, string P_BEAMERNO, string P_BARNO
            , string P_ITMWEAVING, string P_FINISHDATE, string P_STARTDATE, string P_WEFTYARN
            , decimal? P_WIDTH, decimal? P_BEAMLENGTH, decimal? P_SPEED, string P_WEAVINGLOT)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                List<DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION> results = new List<DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION>();

                DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION instList = new DataControl.ClassData.WeavingClassData.ListWEAV_SEARCHPRODUCTION();

                instList.BEAMLOT = P_BEAMERROLL;
                instList.MC = P_LOOMNO;
                //instList.REEDNO2 = lot.REEDNO2;
                instList.WEFTYARN = P_WEFTYARN;
                //instList.TEMPLETYPE = lot.TEMPLETYPE;
                instList.BARNO = P_BARNO;

                if (!string.IsNullOrEmpty(P_STARTDATE))
                    instList.STARTDATE = DateTime.Parse(P_STARTDATE);
                if (!string.IsNullOrEmpty(P_FINISHDATE))
                    instList.FINISHDATE = DateTime.Parse(P_FINISHDATE);

                //instList.FINISHFLAG = lot.FINISHFLAG;
                //instList.SETTINGBY = lot.SETTINGBY;
                //instList.EDITDATE = lot.EDITDATE;
                //instList.EDITBY = lot.EDITBY;
                instList.ITM_WEAVING = P_ITMWEAVING;
                //instList.PRODUCTTYPEID = lot.PRODUCTTYPEID;
                instList.WIDTH = P_WIDTH;
                instList.BEAMLENGTH = P_BEAMLENGTH;
                instList.SPEED = P_SPEED;

                instList.BEAMERNO = P_BEAMERNO;

                results.Add(instList);

                List<WEAV_GETMCSTOPBYLOT> lotByBeamRolls = WeavingDataService.Instance.WEAV_GETMCSTOPBYLOT(P_WEAVINGLOT);
                List<DataControl.ClassData.WeavingClassData.ListWEAV_GETMCSTOPBYLOT> resultByBeamRolls = new List<DataControl.ClassData.WeavingClassData.ListWEAV_GETMCSTOPBYLOT>();

                if (null != lotByBeamRolls && lotByBeamRolls.Count > 0 && null != lotByBeamRolls[0])
                {
                    for (int i = 0; i < lotByBeamRolls.Count; i++)
                    {
                        WEAV_GETMCSTOPBYLOT dbResult = lotByBeamRolls[i];

                        DataControl.ClassData.WeavingClassData.ListWEAV_GETMCSTOPBYLOT inst = new DataControl.ClassData.WeavingClassData.ListWEAV_GETMCSTOPBYLOT();

                        if (!string.IsNullOrEmpty(dbResult.WEAVINGLOT))
                            inst.WEAVINGLOT = dbResult.WEAVINGLOT;
                        else
                            inst.WEAVINGLOT = P_WEAVINGLOT;

                        inst.DEFECTCODE = dbResult.DEFECTCODE;
                        inst.DEFECTPOSITION = dbResult.DEFECTPOSITION;
                        inst.CREATEBY = dbResult.CREATEBY;
                        inst.CREATEDATE = dbResult.CREATEDATE;
                        inst.REMARK = dbResult.REMARK;
                        inst.LOOMNO = dbResult.LOOMNO;
                        inst.BEAMERROLL = dbResult.BEAMERROLL;
                        inst.DOFFNO = dbResult.DOFFNO;
                        inst.DEFECTLENGTH = dbResult.DEFECTLENGTH;
                        inst.DESCRIPTION = dbResult.DESCRIPTION;
                        inst.WEAVSTARTDATE = dbResult.WEAVSTARTDATE;
                        inst.WEAVFINISHDATE = dbResult.WEAVFINISHDATE;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.WIDTH = dbResult.WIDTH;
                        inst.LENGTH = dbResult.LENGTH;

                        resultByBeamRolls.Add(inst);
                    }
                }

                if (results.Count > 0 || resultByBeamRolls.Count > 0)
                {
                    Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);
                    Microsoft.Reporting.WinForms.ReportDataSource rdByBeamRolls = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", resultByBeamRolls);

                    rds.Value = results;
                    rdByBeamRolls.Value = resultByBeamRolls;

                    this._reportViewer.LocalReport.DataSources.Clear();
                    this._reportViewer.LocalReport.DataSources.Add(rds);
                    this._reportViewer.LocalReport.DataSources.Add(rdByBeamRolls);
                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWeavingRecord.rdlc";
                    this._reportViewer.LocalReport.EnableExternalImages = true;

                    IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                    this._reportViewer.LocalReport.SetParameters(parameters);

                    this._reportViewer.RefreshReport();

                    SetSize("A4", true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepG3_GoLabel
        private void RepG3_GoLabel(string PALLETNO, string TRACENO, string LOTNO,
        string ITM_YARN, decimal? CONECH, DateTime? ENTRYDATE, string YARNTYPE, decimal? WEIGHTQTY)
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                List<DataControl.ClassData.G3ClassData.ListG3_GOLABEL> results = new List<DataControl.ClassData.G3ClassData.ListG3_GOLABEL>();

                DataControl.ClassData.G3ClassData.ListG3_GOLABEL inst = new DataControl.ClassData.G3ClassData.ListG3_GOLABEL();

                inst.PALLETNO = PALLETNO;
                inst.TRACENO = TRACENO;
                inst.LOTNO = LOTNO;
                inst.ITM_YARN = ITM_YARN;
                inst.CONECH = CONECH;
                inst.ENTRYDATE = ENTRYDATE;
                inst.YARNTYPE = YARNTYPE;
                inst.WEIGHTQTY = WEIGHTQTY;

                if (!string.IsNullOrEmpty(inst.ITM_YARN))
                {
                    if (inst.ITM_YARN == "470-140-749 INVISTA" || inst.ITM_YARN == "470-68-743 INVISTA")
                    {
                        inst.From = "INV";
                    }
                    else if (inst.ITM_YARN == "470T-72-1781-JJ" || inst.ITM_YARN == "470T-72-1781-O273" || inst.ITM_YARN == "470T-72-1781-O274"
                        || inst.ITM_YARN == "470T-72-1781-O275" || inst.ITM_YARN == "470T-72-1781-O569")
                    {
                        inst.From = "OKA";
                    }
                    else
                    {
                        if (inst.ITM_YARN.Length >= 2)
                        {
                            string itemName = string.Empty;

                            itemName = inst.ITM_YARN.Substring(inst.ITM_YARN.Length - 2, 2);

                            if (itemName == "JJ")
                            {
                                inst.From = "JJ";
                            }
                            else
                            {
                                inst.From = "TTS";
                            }

                        }
                        else
                        {
                            inst.From = "TTS";
                        }
                    }
                }
                else
                {
                    inst.From = "TTS";
                }

                results.Add(inst);


                if (results.Count > 0)
                {
                    Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                    rds.Value = results;
                    this._reportViewer.LocalReport.DataSources.Clear();
                    this._reportViewer.LocalReport.DataSources.Add(rds);
                    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepG3_GoLabel.rdlc";
                    this._reportViewer.LocalReport.EnableExternalImages = true;

                    IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                    parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                    this._reportViewer.LocalReport.SetParameters(parameters);
                    this._reportViewer.RefreshReport();
                    SetSize("A6", false);

                    //----------------------------------------------------------------------------//

                    //System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                    //pg.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                    //pg.PaperSize = new System.Drawing.Printing.PaperSize("A6", 413, 583); // 4.13 in x 5.83 in
                    //pg.PaperSize.RawKind = (int)System.Drawing.Printing.PaperKind.A6;
                    
                    //this._reportViewer.SetPageSettings(pg);
                    //this._reportViewer.RefreshReport();
 
                    //----------------------------------------------------------------------------//
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepIssueRawMaterial
        private void RepIssueRawMaterial(string REQUESTNO)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(REQUESTNO))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
                    string CountPallet = string.Empty;
                    string SumWeight = string.Empty;

                    List<G3_GETREQUESTNODETAIL> lots = G3DataService.Instance.G3_GETREQUESTNODETAIL(REQUESTNO);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                       
                        List<DataControl.ClassData.G3ClassData.ListG3_GETREQUESTNODETAIL> results = new List<DataControl.ClassData.G3ClassData.ListG3_GETREQUESTNODETAIL>();

                        int rowCount = 0;
                        decimal weight = 0;

                        foreach (G3_GETREQUESTNODETAIL dbResult in lots)
                        {
                            DataControl.ClassData.G3ClassData.ListG3_GETREQUESTNODETAIL inst = new DataControl.ClassData.G3ClassData.ListG3_GETREQUESTNODETAIL();

                            inst.ISSUEDATE = dbResult.ISSUEDATE;
                            inst.PALLETNO = dbResult.PALLETNO;
                            inst.TRACENO = dbResult.TRACENO;
                            inst.WEIGHT = dbResult.WEIGHT;
                            inst.CH = dbResult.CH;
                            inst.ISSUEBY = dbResult.ISSUEBY;
                            inst.ISSUETO = dbResult.ISSUETO;
                            inst.REQUESTNO = dbResult.REQUESTNO;
                            inst.PALLETTYPE = dbResult.PALLETTYPE;
                            inst.ITM_YARN = dbResult.ITM_YARN;
                            inst.LOTNO = dbResult.LOTNO;
                            inst.YARNTYPE = dbResult.YARNTYPE;
                            inst.ITM400 = dbResult.ITM400;
                            inst.ENTRYDATE = dbResult.ENTRYDATE;
                            inst.PACKAING = dbResult.PACKAING;
                            inst.CLEAN = dbResult.CLEAN;
                            inst.FALLDOWN = dbResult.FALLDOWN;
                            inst.TEARING = dbResult.TEARING;

                            #region ChkPACKAING
                            if (!string.IsNullOrEmpty(inst.PACKAING))
                            {
                                if (inst.PACKAING == "0")
                                    inst.ChkPACKAING = true;
                                else if (inst.PACKAING == "1")
                                    inst.ChkPACKAING = false;
                            }
                            else
                            {
                                inst.ChkPACKAING = false;
                            }
                            #endregion

                            #region ChkCLEAN
                            if (!string.IsNullOrEmpty(inst.CLEAN))
                            {
                                if (inst.CLEAN == "0")
                                    inst.ChkCLEAN = true;
                                else if (inst.CLEAN == "1")
                                    inst.ChkCLEAN = false;
                            }
                            else
                            {
                                inst.ChkCLEAN = false;
                            }
                            #endregion

                            #region ChkFALLDOWN
                            if (!string.IsNullOrEmpty(inst.FALLDOWN))
                            {
                                if (inst.FALLDOWN == "0")
                                    inst.ChkFALLDOWN = true;
                                else if (inst.FALLDOWN == "1")
                                    inst.ChkFALLDOWN = false;
                            }
                            else
                            {
                                inst.ChkFALLDOWN = false;
                            }
                            #endregion

                            #region ChkTEARING
                            if (!string.IsNullOrEmpty(inst.TEARING))
                            {
                                if (inst.TEARING == "0")
                                    inst.ChkTEARING = true;
                                else if (inst.TEARING == "1")
                                    inst.ChkTEARING = false;
                            }
                            else
                            {
                                inst.ChkTEARING = false;
                            }
                            #endregion

                            if (inst.WEIGHT != null)
                                weight += inst.WEIGHT.Value;

                            rowCount++;

                            results.Add(inst);
                        }

                        CountPallet = rowCount.ToString("#,##0");
                        SumWeight = weight.ToString("#,##0.##");

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepIssueRawMaterial.rdlc";
                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("RequestNo", REQUESTNO));
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("CountPallet", CountPallet));
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("SumWeight", SumWeight));

                            this._reportViewer.LocalReport.SetParameters(parameters);
                            this._reportViewer.RefreshReport();

                            SetSize("A4", true);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCheckingAirbag
        private void RepCheckingAirbag(string P_CUSID, string P_DATE, string P_LABITMCODE, string P_RESULT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(P_CUSID))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<QA_SEARCHCHECKINGDATA> lots = QualityAssuranceService.Instance.QA_SEARCHCHECKINGDATA(P_CUSID, P_DATE, P_LABITMCODE, P_RESULT);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        List<DataControl.ClassData.QualityAssuranceClassData.ListQA_SEARCHCHECKINGDATA> results = new List<DataControl.ClassData.QualityAssuranceClassData.ListQA_SEARCHCHECKINGDATA>();

                        foreach (QA_SEARCHCHECKINGDATA dbResult in lots)
                        {
                            DataControl.ClassData.QualityAssuranceClassData.ListQA_SEARCHCHECKINGDATA inst = new DataControl.ClassData.QualityAssuranceClassData.ListQA_SEARCHCHECKINGDATA();

                            inst.RowNo = dbResult.RowNo;
                            inst.CUSTOMERID = dbResult.CUSTOMERID;
                            inst.LAB_ITMCODE = dbResult.LAB_ITMCODE;
                            inst.LAB_LOT = dbResult.LAB_LOT;
                            inst.LAB_BATCHNO = dbResult.LAB_BATCHNO;
                            inst.INS_ITMCODE = dbResult.INS_ITMCODE;
                            inst.INS_LOT = dbResult.INS_LOT;
                            inst.INS_BATCHNO = dbResult.INS_BATCHNO;
                            inst.CUS_CODE = dbResult.CUS_CODE;
                            inst.CHECK_RESULT = dbResult.CHECK_RESULT;
                            inst.CHECKDATE = dbResult.CHECKDATE;
                            inst.CHECKEDBY = dbResult.CHECKEDBY;
                            inst.CUSTOMERNAME = dbResult.CUSTOMERNAME;

                            inst.DELETEFLAG = dbResult.DELETEFLAG;
                            inst.DELETEBY = dbResult.DELETEBY;
                            inst.DELETEDATE = dbResult.DELETEDATE;
                            inst.SHIFT = dbResult.SHIFT;
                            inst.REMARK = dbResult.REMARK;

                            results.Add(inst);
                        }


                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCheckingAirbag.rdlc";

                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                            this._reportViewer.LocalReport.SetParameters(parameters);
                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepDRAW_DAILYREPORT
        private void RepDRAW_DAILYREPORT(string P_DATE)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                List<DRAW_DAILYREPORT> lots = DrawingDataService.Instance.DRAW_DAILYREPORT(P_DATE);

                int CountRow = 0;
                decimal? TotalLength = 0;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.DrawingClassData.ListDRAW_DAILYREPORT> results = new List<DataControl.ClassData.DrawingClassData.ListDRAW_DAILYREPORT>();

                    foreach (DRAW_DAILYREPORT dbResult in lots)
                    {
                        DataControl.ClassData.DrawingClassData.ListDRAW_DAILYREPORT inst = new DataControl.ClassData.DrawingClassData.ListDRAW_DAILYREPORT();

                        inst.No = dbResult.No;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.DRAWINGTYPE = dbResult.DRAWINGTYPE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDATE = dbResult.ENDATE;
                        inst.REEDNO = dbResult.REEDNO;
                        inst.HEALDCOLOR = dbResult.HEALDCOLOR;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.FINISHBY = dbResult.FINISHBY;
                        inst.USEFLAG = dbResult.USEFLAG;
                        inst.HEALDNO = dbResult.HEALDNO;
                        inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.BEAMERNO = dbResult.BEAMERNO;

                        if (inst.LENGTH != null)
                            TotalLength += inst.LENGTH;

                        CountRow++;

                        results.Add(inst);
                    }


                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepDRAW_DAILYREPORT.rdlc";

                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", P_DATE));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("CountRow", CountRow.ToString("#,##0")));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", TotalLength.Value.ToString("#,##0.##")+"  m."));

                        this._reportViewer.LocalReport.SetParameters(parameters);
                        this._reportViewer.RefreshReport();

                        SetSize("A4", true);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepDRAW_TRANSFERSLIP
        private void RepDRAW_TRANSFERSLIP(string P_BEAMERROLL)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                List<DRAW_TRANSFERSLIP> lots = DrawingDataService.Instance.DRAW_TRANSFERSLIP(P_BEAMERROLL);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.DrawingClassData.ListDRAW_TRANSFERSLIP> results = new List<DataControl.ClassData.DrawingClassData.ListDRAW_TRANSFERSLIP>();

                    DataControl.ClassData.DrawingClassData.ListDRAW_TRANSFERSLIP inst = new DataControl.ClassData.DrawingClassData.ListDRAW_TRANSFERSLIP();

                    DRAW_TRANSFERSLIP dbResult = lots[0];
                                      
                    inst.BEAMLOT = dbResult.BEAMLOT;
                    inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                    inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                    inst.DRAWINGTYPE = dbResult.DRAWINGTYPE;
                    inst.STARTDATE = dbResult.STARTDATE;
                    inst.ENDATE = dbResult.ENDATE;
                    inst.REEDNO = dbResult.REEDNO;
                    inst.HEALDCOLOR = dbResult.HEALDCOLOR;
                    inst.STARTBY = dbResult.STARTBY;
                    inst.FINISHBY = dbResult.FINISHBY;
                    inst.USEFLAG = dbResult.USEFLAG;
                    inst.HEALDNO = dbResult.HEALDNO;
                    inst.OPERATOR_GROUP = dbResult.OPERATOR_GROUP;
                    inst.TOTALYARN = dbResult.TOTALYARN;
                    inst.BEAMNO = dbResult.BEAMNO;
                    inst.LENGTH = dbResult.LENGTH;
                    inst.BEAMERNO = dbResult.BEAMERNO;

                    results.Add(inst);

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepDRAW_TRANSFERSLIP.rdlc";

                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        this._reportViewer.LocalReport.SetParameters(parameters);
                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWEAV_SHIPMENTREPORT
        private void RepWEAV_SHIPMENTREPORT(string P_BEGINDATE, string P_ENDDATE)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                string DatePrint = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                List<WEAV_SHIPMENTREPORT> lots = WeavingDataService.Instance.WEAV_SHIPMENTREPORT(P_BEGINDATE, P_ENDDATE);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.WeavingClassData.ListWEAV_SHIPMENTREPORT> results = new List<DataControl.ClassData.WeavingClassData.ListWEAV_SHIPMENTREPORT>();

                    foreach (WEAV_SHIPMENTREPORT dbResult in lots)
                    {
                        DataControl.ClassData.WeavingClassData.ListWEAV_SHIPMENTREPORT inst = new DataControl.ClassData.WeavingClassData.ListWEAV_SHIPMENTREPORT();

                        inst.No = dbResult.No;
                        inst.ITM_WEAVING = dbResult.ITM_WEAVING;
                        inst.PIECES = dbResult.PIECES;
                        inst.METERS = dbResult.METERS;
                        
                        results.Add(inst);
                    }

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWEAV_SHIPMENTREPORT.rdlc";

                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("SearchDate", "( " + P_BEGINDATE + " - " + P_ENDDATE + " )"));

                        this._reportViewer.LocalReport.SetParameters(parameters);
                        this._reportViewer.RefreshReport();

                        SetSize("A4", false);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepCUT_SERACHLIST
        private void RepCUT_SERACHLIST(string P_DATE, string P_MC)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string DatePrint = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                List<CUT_SERACHLIST> lots = CutPrintDataService.Instance.CUT_SERACHLIST(P_DATE, P_MC);
                string mcName = string.Empty;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_SERACHLIST> results = new List<DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_SERACHLIST>();

                    int i = 0;
                    decimal? TotalLength = 0;

                    foreach (CUT_SERACHLIST dbResult in lots)
                    {
                        DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_SERACHLIST inst = new DataControl.ClassData.CUT_GETSLIPClassData.ListCUT_SERACHLIST();

                        inst.No = i + 1;
                        inst.ITEMLOT = dbResult.ITEMLOT;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.WIDTHBARCODE1 = dbResult.WIDTHBARCODE1;
                        inst.WIDTHBARCODE2 = dbResult.WIDTHBARCODE2;
                        inst.WIDTHBARCODE3 = dbResult.WIDTHBARCODE3;
                        inst.WIDTHBARCODE4 = dbResult.WIDTHBARCODE4;
                        inst.DISTANTBARCODE1 = dbResult.DISTANTBARCODE1;
                        inst.DISTANTBARCODE2 = dbResult.DISTANTBARCODE2;
                        inst.DISTANTBARCODE3 = dbResult.DISTANTBARCODE3;
                        inst.DISTANTBARCODE4 = dbResult.DISTANTBARCODE4;
                        inst.DISTANTLINE1 = dbResult.DISTANTLINE1;
                        inst.DISTANTLINE2 = dbResult.DISTANTLINE2;
                        inst.DISTANTLINE3 = dbResult.DISTANTLINE3;
                        inst.DENSITYWARP = dbResult.DENSITYWARP;
                        inst.DENSITYWEFT = dbResult.DENSITYWEFT;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEFORE_WIDTH = dbResult.BEFORE_WIDTH;
                        inst.AFTER_WIDTH = dbResult.AFTER_WIDTH;
                        inst.BEGINROLL_LINE1 = dbResult.BEGINROLL_LINE1;
                        inst.BEGINROLL_LINE2 = dbResult.BEGINROLL_LINE2;
                        inst.BEGINROLL_LINE3 = dbResult.BEGINROLL_LINE3;
                        inst.BEGINROLL_LINE4 = dbResult.BEGINROLL_LINE4;
                        inst.ENDROLL_LINE1 = dbResult.ENDROLL_LINE1;
                        inst.ENDROLL_LINE2 = dbResult.ENDROLL_LINE2;
                        inst.ENDROLL_LINE3 = dbResult.ENDROLL_LINE3;
                        inst.ENDROLL_LINE4 = dbResult.ENDROLL_LINE4;
                        inst.OPERATORID = dbResult.OPERATORID;
                        inst.SELVAGE_LEFT = dbResult.SELVAGE_LEFT;
                        inst.SELVAGE_RIGHT = dbResult.SELVAGE_RIGHT;
                        inst.REMARK = dbResult.REMARK;
                        inst.PRODUCTTYPEID = dbResult.PRODUCTTYPEID;
                        inst.MCNO = dbResult.MCNO;
                        inst.ITEMCODE = dbResult.ITEMCODE;
                        inst.PARTNO = dbResult.PARTNO;
                        inst.MCNAME = dbResult.MCNAME;
                        mcName = inst.MCNAME;

                        // เพิ่มใหม่ 28/06/16
                        inst.SND_BARCODE = dbResult.SND_BARCODE;

                        inst.BEGINROLL2_LINE1 = dbResult.BEGINROLL2_LINE1;
                        inst.BEGINROLL2_LINE2 = dbResult.BEGINROLL2_LINE2;
                        inst.BEGINROLL2_LINE3 = dbResult.BEGINROLL2_LINE3;
                        inst.BEGINROLL2_LINE4 = dbResult.BEGINROLL2_LINE4;
                        inst.ENDROLL2_LINE1 = dbResult.ENDROLL2_LINE1;
                        inst.ENDROLL2_LINE2 = dbResult.ENDROLL2_LINE2;
                        inst.ENDROLL2_LINE3 = dbResult.ENDROLL2_LINE3;
                        inst.ENDROLL2_LINE4 = dbResult.ENDROLL2_LINE4;

                        //เพิ่มใหม่ 28/06/17
                        inst.TENSION = dbResult.TENSION;
                        inst.FINISHLOT = dbResult.FINISHLOT;
                        inst.FINISHLENGTH = dbResult.FINISHLENGTH;
                        inst.LENGTHPRINT = dbResult.LENGTHPRINT;

                        inst.STATUS = dbResult.STATUS;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.SUSPENDSTARTDATE = dbResult.SUSPENDSTARTDATE;

                        //เพิ่มใหม่ 25/08/17
                        inst.LENGTHDETAIL = dbResult.LENGTHDETAIL;
                        inst.FINISHLENGTH1 = dbResult.FINISHLENGTH1;

                        if (inst.LENGTHPRINT != null)
                            TotalLength += inst.LENGTHPRINT;

                        results.Add(inst);
                        i++;
                    }

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepCUT_SERACHLIST.rdlc";

                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_DATE));
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("MCName", mcName));

                        if (TotalLength != null)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", TotalLength.Value.ToString("#,##0.##")));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", "0"));

                        this._reportViewer.LocalReport.SetParameters(parameters);
                        this._reportViewer.RefreshReport();

                        SetSize("A4", true);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepWarpingList
        private void RepWarpingList(string P_WARPHEADNO, string P_WARPMC, string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string DatePrint = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                List<WARP_WARPLIST> lots = WarpingDataService.Instance.WARP_WARPLIST(P_WARPHEADNO,  P_WARPMC,  P_ITMPREPARE,  P_STARTDATE,  P_ENDDATE);
                string mcName = string.Empty;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.WarpingClassData.ListWARP_WARPLIST> results = new List<DataControl.ClassData.WarpingClassData.ListWARP_WARPLIST>();

                    decimal? TotalLength = 0;

                    foreach (WARP_WARPLIST dbResult in lots)
                    {
                        DataControl.ClassData.WarpingClassData.ListWARP_WARPLIST inst = new DataControl.ClassData.WarpingClassData.ListWARP_WARPLIST();

                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.WARPERLOT = dbResult.WARPERLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.SIDE = dbResult.SIDE;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.TENSION = dbResult.TENSION;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.FLAG = dbResult.FLAG;
                        inst.WARPMC = dbResult.WARPMC;
                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_IT = dbResult.TENSION_IT;
                        inst.TENSION_TAKEUP = dbResult.TENSION_TAKEUP;
                        inst.MC_COUNT_L = dbResult.MC_COUNT_L;
                        inst.MC_COUNT_S = dbResult.MC_COUNT_S;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.ITM_YARN = dbResult.ITM_YARN;

                        if (inst.LENGTH != null)
                            TotalLength += inst.LENGTH;

                        results.Add(inst);
                    }

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepWarpingList.rdlc";

                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        if (!string.IsNullOrEmpty(P_STARTDATE) && !string.IsNullOrEmpty(P_ENDDATE))
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_STARTDATE + " - " + P_ENDDATE));
                        else if (!string.IsNullOrEmpty(P_STARTDATE) && string.IsNullOrEmpty(P_ENDDATE))
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_STARTDATE));
                        else if (string.IsNullOrEmpty(P_STARTDATE) && !string.IsNullOrEmpty(P_ENDDATE))
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_ENDDATE));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", "-"));
                       
                        if (TotalLength != null)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", TotalLength.Value.ToString("#,##0.##")));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", "0"));

                        this._reportViewer.LocalReport.SetParameters(parameters);
                        this._reportViewer.RefreshReport();

                        SetSize("A4", true);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepBeamingList
        private void RepBeamingList(string P_BEAMERNO, string P_WARPMC, string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
        {
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string DatePrint = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                List<BEAM_BEAMLIST> lots = BeamingDataService.Instance.BEAM_BEAMLIST(P_BEAMERNO, P_WARPMC, P_ITMPREPARE, P_STARTDATE, P_ENDDATE);
                string mcName = string.Empty;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    List<DataControl.ClassData.BeamingClassData.ListBEAM_BEAMLIST> results = new List<DataControl.ClassData.BeamingClassData.ListBEAM_BEAMLIST>();

                    decimal? TotalLength = 0;

                    foreach (BEAM_BEAMLIST dbResult in lots)
                    {
                        DataControl.ClassData.BeamingClassData.ListBEAM_BEAMLIST inst = new DataControl.ClassData.BeamingClassData.ListBEAM_BEAMLIST();

                        inst.BEAMERNO = dbResult.BEAMERNO;
                        inst.BEAMLOT = dbResult.BEAMLOT;
                        inst.BEAMNO = dbResult.BEAMNO;
                        inst.STARTDATE = dbResult.STARTDATE;
                        inst.ENDDATE = dbResult.ENDDATE;
                        inst.LENGTH = dbResult.LENGTH;
                        inst.SPEED = dbResult.SPEED;
                        inst.BEAMSTANDTENSION = dbResult.BEAMSTANDTENSION;
                        inst.WINDINGTENSION = dbResult.WINDINGTENSION;
                        inst.HARDNESS_L = dbResult.HARDNESS_L;
                        inst.HARDNESS_N = dbResult.HARDNESS_N;
                        inst.HARDNESS_R = dbResult.HARDNESS_R;
                        inst.INSIDE_WIDTH = dbResult.INSIDE_WIDTH;
                        inst.OUTSIDE_WIDTH = dbResult.OUTSIDE_WIDTH;
                        inst.FULL_WIDTH = dbResult.FULL_WIDTH;
                        inst.STARTBY = dbResult.STARTBY;
                        inst.DOFFBY = dbResult.DOFFBY;
                        inst.BEAMMC = dbResult.BEAMMC;
                        inst.FLAG = dbResult.FLAG;
                        inst.REMARK = dbResult.REMARK;
                        inst.TENSION_ST1 = dbResult.TENSION_ST1;
                        inst.TENSION_ST2 = dbResult.TENSION_ST2;
                        inst.TENSION_ST3 = dbResult.TENSION_ST3;
                        inst.TENSION_ST4 = dbResult.TENSION_ST4;
                        inst.TENSION_ST5 = dbResult.TENSION_ST5;
                        inst.TENSION_ST6 = dbResult.TENSION_ST6;
                        inst.TENSION_ST7 = dbResult.TENSION_ST7;
                        inst.TENSION_ST8 = dbResult.TENSION_ST8;
                        inst.TENSION_ST9 = dbResult.TENSION_ST9;
                        inst.TENSION_ST10 = dbResult.TENSION_ST10;
                        inst.EDITBY = dbResult.EDITBY;
                        inst.OLDBEAMNO = dbResult.OLDBEAMNO;
                        inst.EDITDATE = dbResult.EDITDATE;
                        inst.ITM_PREPARE = dbResult.ITM_PREPARE;
                        inst.WARPHEADNO = dbResult.WARPHEADNO;
                        inst.TOTALYARN = dbResult.TOTALYARN;
                        inst.TOTALKEBA = dbResult.TOTALKEBA;

                        if (inst.LENGTH != null)
                            TotalLength += inst.LENGTH;

                        results.Add(inst);
                    }

                    if (results.Count > 0)
                    {
                        Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                        rds.Value = results;
                        this._reportViewer.LocalReport.DataSources.Clear();
                        this._reportViewer.LocalReport.DataSources.Add(rds);
                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepBeamingList.rdlc";

                        this._reportViewer.LocalReport.EnableExternalImages = true;

                        IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                        parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));

                        if (!string.IsNullOrEmpty(P_STARTDATE) && !string.IsNullOrEmpty(P_ENDDATE))
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_STARTDATE + " - " + P_ENDDATE));
                        else if (!string.IsNullOrEmpty(P_STARTDATE) && string.IsNullOrEmpty(P_ENDDATE))
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_STARTDATE));
                        else if (string.IsNullOrEmpty(P_STARTDATE) && !string.IsNullOrEmpty(P_ENDDATE))
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", P_ENDDATE));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ProductionDate", "-"));

                        if (TotalLength != null)
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", TotalLength.Value.ToString("#,##0.##")));
                        else
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("TotalLength", "0"));

                        this._reportViewer.LocalReport.SetParameters(parameters);
                        this._reportViewer.RefreshReport();

                        SetSize("A4", true);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepSample4746P25R
        private void RepSample4746P25R()
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                this._reportViewer.LocalReport.DataSources.Clear();

                this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepSample4746P25R.rdlc";
                this._reportViewer.LocalReport.EnableExternalImages = true;

                IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
    
                this._reportViewer.LocalReport.SetParameters(parameters);

                this._reportViewer.RefreshReport();

                SetSize("A4", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepSample4755ATW
        private void RepSample4755ATW()
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                this._reportViewer.LocalReport.DataSources.Clear();

                this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepSample4755ATW.rdlc";
                this._reportViewer.LocalReport.EnableExternalImages = true;

                IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();

                this._reportViewer.LocalReport.SetParameters(parameters);

                this._reportViewer.RefreshReport();

                SetSize("A4", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepSample4L50B25R
        private void RepSample4L50B25R()
        {
            try
            {

                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                this._reportViewer.LocalReport.DataSources.Clear();

                this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepSample4L50B25R.rdlc";
                this._reportViewer.LocalReport.EnableExternalImages = true;

                IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();

                this._reportViewer.LocalReport.SetParameters(parameters);

                this._reportViewer.RefreshReport();

                SetSize("A4", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepPackingLabel2D
        private void RepPackingLabel2D(string INSLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(INSLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    List<PACK_PRINTLABEL> lots =
                           PackingDataService.Instance.PACK_PRINTLABEL(INSLOT);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        PACK_PRINTLABEL lot = lots[0];

                        List<DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D> results = new List<DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D>();

                        DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D inst = new DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D();

                        if (lot.QUANTITY != null)
                            inst.QUANTITY = MathEx.TruncateDecimal(lot.QUANTITY.Value, 1);
                        else
                            inst.QUANTITY = 0;

                        inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                        inst.NETWEIGHT = lot.NETWEIGHT;
                        inst.GRADE = lot.GRADE;
                        inst.ITEMCODE = lot.ITEMCODE;

                        #region Set CM
                        //if (inst.ITEMCODE == "4746P25R" || inst.ITEMCODE == "09300" || inst.ITEMCODE == "09460" || inst.ITEMCODE == "09461" || inst.ITEMCODE == "09462"
                        //    || inst.ITEMCODE == "2L72C18R")
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.CUSTOMERPARTNO = "P" + lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = "P" + lot.CUSTOMERPARTNO;

                            inst.CUSTOMERPARTNO09 = "P" + lot.CUSTOMERPARTNO;

                            inst.SUPPLIERCODE = lot.SUPPLIERCODE;
                            inst.SUPPLIERCODE09 = lot.SUPPLIERCODE;

                            inst.BATCHNO = "S" + lot.BATCHNO;
                            inst.BATCHNO09 = "S" + lot.BATCHNO;
                            inst.BARCODEBACTHNO = "S" + lot.BATCHNO;

                            inst.StrQuantity = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");
                            inst.StrQuantity09 = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = "H" + lot.INSPECTIONLOT;
                            inst.INSPECTIONLOT09 = "H" + lot.INSPECTIONLOT;
                        }
                        else if (lot.CUSTOMERID == "09")
                        {
                            inst.CUSTOMERPARTNO = "P" + lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = lot.CUSPARTNO2D;

                            inst.CUSTOMERPARTNO09 = lot.CUSTOMERPARTNO;

                            inst.SUPPLIERCODE = "V" + lot.SUPPLIERCODE;
                            inst.SUPPLIERCODE09 = lot.SUPPLIERCODE;

                            inst.BATCHNO = "H" + lot.BATCHNO;
                            inst.BATCHNO09 = lot.BATCHNO;
                            inst.BARCODEBACTHNO = "H" + lot.BATCHNO;

                            inst.StrQuantity = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");
                            inst.StrQuantity09 = inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = "S" + lot.INSPECTIONLOT;
                            inst.INSPECTIONLOT09 = lot.INSPECTIONLOT;
                        }
                        else
                        {
                            inst.CUSTOMERPARTNO = lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = lot.BarcodeCMPARTNO;
                            inst.CUSTOMERPARTNO09 = lot.CUSTOMERPARTNO;

                            inst.SUPPLIERCODE = lot.SUPPLIERCODE;
                            inst.SUPPLIERCODE09 = lot.SUPPLIERCODE;

                            inst.BATCHNO = lot.BATCHNO;
                            inst.BATCHNO09 = lot.BATCHNO;
                            inst.BARCODEBACTHNO = lot.BARCODEBACTHNO;

                            inst.StrQuantity = inst.QUANTITY.Value.ToString("#,##0.0");
                            inst.StrQuantity09 = inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                            inst.INSPECTIONLOT09 = lot.INSPECTIONLOT;
                        }
                        #endregion

                        //if (inst.ITEMCODE == "4746P25R" || inst.ITEMCODE == "2L72C18R")
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.FINISHINGPROCESS = lot.FINISHINGPROCESS;
                        }
                        else
                        {
                            inst.FINISHINGPROCESS = "";
                        }

                        inst.DESCRIPTION = lot.DESCRIPTION;

                        inst.PDATE = lot.PDATE;
                        inst.CUSTOMERID = lot.CUSTOMERID;

                        #region Old
                        if (!string.IsNullOrEmpty(lot.CUSTOMERPARTNO) && inst.QUANTITY != null && !string.IsNullOrEmpty(lot.INSPECTIONLOT)
                            && !string.IsNullOrEmpty(lot.BATCHNO))
                        {
                            string dtetemp = string.Empty;
                            //dtetemp = "5D" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
                            dtetemp = "5D" + lot.BDate;

                            /*
                                (ISO 646/ASCII char)
                                [   decimal(91)  hex(5B)
                                )   decimal(41)  hex(29)
                                >   decimal(62)  hex(3)
                                RS  decimal(30)  hex(1E)
                                GS  decimal(29)  hex(1D)
                                EOT decimal(04)  hex(04)
                             */

                            char rs = Convert.ToChar(30);
                            char gs = Convert.ToChar(29);
                            char eot = Convert.ToChar(4);

                            string inputData = "[)>" + rs + "06" + gs
                               + "12SA" + gs
                               + "16S1" + gs
                               + "V" + lot.SUPPLIERCODE + gs
                               + "S" + lot.INSPECTIONLOT + gs
                               + lot.CUSPARTNO2D + gs
                               + "Q" + inst.QUANTITY.Value.ToString("#,##0.0") + gs
                               + "3QMTR" + gs
                               + "H" + lot.BATCHNO + gs
                               + dtetemp
                               + "094" + rs + eot;

                            inst.DBARCODE = inputData;

                            //inst.DBARCODE = temp + "Q" + inst.QUANTITY.Value.ToString("#,##0.0") + "S" + lot.INSPECTIONLOT + "H" + lot.BATCHNO;
                        }
                        #endregion

                        //inst.DBARCODE = lot.DBARCODE;

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPackingLabel2D.rdlc";

                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            this._reportViewer.RefreshReport();

                            SetSize("2D", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepPackingLabel2DBig
        private void RepPackingLabel2DBig(string INSLOT)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(INSLOT))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    List<PACK_PRINTLABEL> lots =
                           PackingDataService.Instance.PACK_PRINTLABEL(INSLOT);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        PACK_PRINTLABEL lot = lots[0];

                        List<DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D> results = new List<DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D>();

                        DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D inst = new DataControl.ClassData.PackingClassData.ListPACK_PRINTLABEL2D();

                        if (lot.QUANTITY != null)
                            inst.QUANTITY = MathEx.TruncateDecimal(lot.QUANTITY.Value, 1);
                        else
                            inst.QUANTITY = 0;

                        inst.GROSSWEIGHT = lot.GROSSWEIGHT;
                        inst.NETWEIGHT = lot.NETWEIGHT;
                        inst.GRADE = lot.GRADE;
                        inst.ITEMCODE = lot.ITEMCODE;

                        #region Set CM
                        //if (inst.ITEMCODE == "4746P25R" || inst.ITEMCODE == "09300" || inst.ITEMCODE == "09460" || inst.ITEMCODE == "09461" || inst.ITEMCODE == "09462"
                        //    || inst.ITEMCODE == "2L72C18R")
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.CUSTOMERPARTNO = "P" + lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = "P" + lot.CUSTOMERPARTNO;

                            inst.CUSTOMERPARTNO09 = "P" + lot.CUSTOMERPARTNO;

                            inst.SUPPLIERCODE = lot.SUPPLIERCODE;
                            inst.SUPPLIERCODE09 = lot.SUPPLIERCODE;

                            inst.BATCHNO = "S" + lot.BATCHNO;
                            inst.BATCHNO09 = "S" + lot.BATCHNO;
                            inst.BARCODEBACTHNO = "S" + lot.BATCHNO;

                            inst.StrQuantity = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");
                            inst.StrQuantity09 = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = "H" + lot.INSPECTIONLOT;
                            inst.INSPECTIONLOT09 = "H" + lot.INSPECTIONLOT;
                        }
                        else if (lot.CUSTOMERID == "09")
                        {
                            inst.CUSTOMERPARTNO = "P" + lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = lot.CUSPARTNO2D;

                            inst.CUSTOMERPARTNO09 = lot.CUSTOMERPARTNO;

                            inst.SUPPLIERCODE = "V" + lot.SUPPLIERCODE;
                            inst.SUPPLIERCODE09 = lot.SUPPLIERCODE;

                            inst.BATCHNO = "H" + lot.BATCHNO;
                            inst.BATCHNO09 = lot.BATCHNO;
                            inst.BARCODEBACTHNO = "H" + lot.BATCHNO;

                            inst.StrQuantity = "Q" + inst.QUANTITY.Value.ToString("#,##0.0");
                            inst.StrQuantity09 = inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = "S" + lot.INSPECTIONLOT;
                            inst.INSPECTIONLOT09 = lot.INSPECTIONLOT;
                        }
                        else
                        {
                            inst.CUSTOMERPARTNO = lot.CUSTOMERPARTNO;
                            inst.BarcodeCMPARTNO = lot.BarcodeCMPARTNO;
                            inst.CUSTOMERPARTNO09 = lot.CUSTOMERPARTNO;

                            inst.SUPPLIERCODE = lot.SUPPLIERCODE;
                            inst.SUPPLIERCODE09 = lot.SUPPLIERCODE;


                            inst.BATCHNO = lot.BATCHNO;
                            inst.BATCHNO09 = lot.BATCHNO;
                            inst.BARCODEBACTHNO = lot.BARCODEBACTHNO;

                            inst.StrQuantity = inst.QUANTITY.Value.ToString("#,##0.0");
                            inst.StrQuantity09 = inst.QUANTITY.Value.ToString("#,##0.0");

                            //เพิ่มใหม่ 24/08/60
                            inst.INSPECTIONLOT = lot.INSPECTIONLOT;
                            inst.INSPECTIONLOT09 = lot.INSPECTIONLOT;
                        }
                        #endregion

                        //if (inst.ITEMCODE == "4746P25R" || inst.ITEMCODE == "2L72C18R")
                        if (lot.CUSTOMERID == "08")
                        {
                            inst.FINISHINGPROCESS = lot.FINISHINGPROCESS;
                        }
                        else
                        {
                            inst.FINISHINGPROCESS = "";
                        }

                        inst.DESCRIPTION = lot.DESCRIPTION;

                        inst.PDATE = lot.PDATE;
                        inst.CUSTOMERID = lot.CUSTOMERID;

                        #region Old
                        if (!string.IsNullOrEmpty(lot.CUSTOMERPARTNO) && inst.QUANTITY != null && !string.IsNullOrEmpty(lot.INSPECTIONLOT)
                            && !string.IsNullOrEmpty(lot.BATCHNO))
                        {
                            string dtetemp = string.Empty;
                            //dtetemp = "5D" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
                            dtetemp = "5D" + lot.BDate;

                            char rs = Convert.ToChar(30);
                            char gs = Convert.ToChar(29);
                            char eot = Convert.ToChar(4);

                            string inputData = "[)>" + rs + "06" + gs
                               + "12SA" + gs
                               + "16S1" + gs
                               + "V" + lot.SUPPLIERCODE + gs
                               + "S" + lot.INSPECTIONLOT + gs
                               + lot.CUSPARTNO2D + gs
                               + "Q" + inst.QUANTITY.Value.ToString("#,##0.0") + gs
                               + "3QMTR" + gs
                               + "H" + lot.BATCHNO + gs
                               + dtetemp
                               + "094" + rs + eot;

                            inst.DBARCODE = inputData;

                            //inst.DBARCODE = temp + "Q" + inst.QUANTITY.Value.ToString("#,##0.0") + "S" + lot.INSPECTIONLOT + "H" + lot.BATCHNO;
                        }
                        #endregion

                        //inst.DBARCODE = lot.DBARCODE;

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);
                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepPackingLabel2DBig.rdlc";

                            this._reportViewer.LocalReport.EnableExternalImages = true;

                            this._reportViewer.RefreshReport();

                            SetSize("2DBig", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region RepLabTestingResult
        private void RepLabTestingResult(string ITM_Code, string P_ENTRYSTARTDATE
                    , string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS
                    , string P_WEAVINGLOG, string P_FINISHINGLOT, DateTime? ENTRYDATE)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ITM_Code))
                {
                    string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                    string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                    List<LAB_SEARCHLABENTRYPRODUCTION_Rep> lots =
                           LABDataService.Instance.LAB_SEARCHLABENTRYPRODUCTION(ITM_Code, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS, P_WEAVINGLOG, P_FINISHINGLOT, ENTRYDATE);
                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        string REPORT_ID = string.Empty;
                        string CM_ID = string.Empty;

                        LAB_SEARCHLABENTRYPRODUCTION_Rep lot = lots[0];

                        List<DataControl.ClassData.LabClassData.ListLAB_SEARCHLABENTRYPRODUCTION> results = new List<DataControl.ClassData.LabClassData.ListLAB_SEARCHLABENTRYPRODUCTION>();

                        DataControl.ClassData.LabClassData.ListLAB_SEARCHLABENTRYPRODUCTION inst = new DataControl.ClassData.LabClassData.ListLAB_SEARCHLABENTRYPRODUCTION();

                        #region LAB_SEARCHLABENTRYPRODUCTION
                        inst.PARTNO = lot.PARTNO;

                        inst.ITM_CODE = lot.ITM_CODE;
                        inst.ITM_CODE_B = lot.ITM_CODE_B;
                        inst.ITM_CODE_H = lot.ITM_CODE_H;

                        inst.WEAVINGLOT = lot.WEAVINGLOT;
                        inst.FINISHINGLOT = lot.FINISHINGLOT;
                        inst.CUSTOMERID = lot.CUSTOMERID;

                        if (!string.IsNullOrEmpty(inst.CUSTOMERID))
                        {
                            CM_ID = inst.CUSTOMERID;

                            if (inst.CUSTOMERID == "01" || inst.CUSTOMERID == "05")
                            {
                                inst.ChkCM = true;
                            }
                            else
                            {
                                inst.ChkCM = false;
                            }
                        }
                        else
                        {
                            inst.ChkCM = false;
                        }

                        inst.FINISHINGPROCESS = lot.FINISHINGPROCESS;
                        inst.ITEMLOT = lot.ITEMLOT;
                        inst.LOOMNO = lot.LOOMNO;
                        inst.FINISHINGMC = lot.FINISHINGMC;
                        inst.ENTRYDATE = lot.ENTRYDATE;
                        inst.BATCHNO = lot.BATCHNO;

                        #region Chk2C Old
                        //if (!string.IsNullOrEmpty(inst.ITM_CODE_B) && !string.IsNullOrEmpty(inst.FINISHINGMC))
                        //{
                        //    if ((inst.ITM_CODE_B == "4L46B25R" || inst.ITM_CODE_B == "4L50B25R" || inst.ITM_CODE_B == "7041B25R") && inst.FINISHINGMC == "Coating2")
                        //    {
                        //        inst.Chk2C = true;
                        //    }
                        //    else
                        //    {
                        //        inst.Chk2C = false;
                        //    }
                        //}
                        #endregion

                        #region Chk2C
                        if (!string.IsNullOrEmpty(inst.CUSTOMERID) && !string.IsNullOrEmpty(inst.FINISHINGMC))
                        {
                            if (inst.CUSTOMERID == "09" && inst.FINISHINGMC == "Coating2")
                            {
                                inst.Chk2C = true;
                            }
                            else
                            {
                                inst.Chk2C = false;
                            }
                        }
                        else
                        {
                            inst.Chk2C = false;
                        }
                        #endregion

                        inst.NUMTHREADS_W_AVG = lot.NUMTHREADS_W_AVG;
                        inst.NUMTHREADS_W_JUD = lot.NUMTHREADS_W_JUD;

                        inst.NUMTHREADS_F_AVG = lot.NUMTHREADS_F_AVG;
                        inst.NUMTHREADS_F_JUD = lot.NUMTHREADS_F_JUD;

                        if (lot.USABLE_WIDTH_AVG != null)
                        {
                            inst.USABLE_WIDTH_AVG = lot.USABLE_WIDTH_AVG;
                            inst.USABLE_WIDTH_JUD = lot.USABLE_WIDTH_JUD;
                        }
                        else if (lot.WIDTH_SILICONE_AVG != null)
                        {
                            inst.WIDTH_SILICONE_AVG = lot.WIDTH_SILICONE_AVG;
                            inst.WIDTH_SILICONE_JUD = lot.WIDTH_SILICONE_JUD;
                        }
                        else
                        {
                            inst.USABLE_WIDTH_AVG = null;
                            inst.USABLE_WIDTH_JUD = null;
                            inst.WIDTH_SILICONE_AVG = null;
                            inst.WIDTH_SILICONE_JUD = null;
                        }

                        inst.TOTALWEIGHT_AVG = lot.TOTALWEIGHT_AVG;
                        inst.TOTALWEIGHT_JUD = lot.TOTALWEIGHT_JUD;

                        inst.UNCOATEDWEIGHT_AVG = lot.UNCOATEDWEIGHT_AVG;
                        inst.UNCOATEDWEIGHT_JUD = lot.UNCOATEDWEIGHT_JUD;

                        inst.COATINGWEIGHT_AVG = lot.COATINGWEIGHT_AVG;
                        inst.COATINGWEIGHT_JUD = lot.COATINGWEIGHT_JUD;

                        #region ChkHide

                        if (inst.TOTALWEIGHT_AVG != null && inst.UNCOATEDWEIGHT_AVG != null && inst.COATINGWEIGHT_AVG != null)
                        {
                            if (inst.CUSTOMERID == "01" || inst.CUSTOMERID == "08")
                            {
                                inst.ChkHide3 = false;
                                inst.ChkHide2 = true;
                                inst.ChkHide1 = false;
                            }
                            else
                            {
                                inst.ChkHide3 = true;
                                inst.ChkHide2 = false;
                                inst.ChkHide1 = false;
                            }
                        }
                        else if (inst.TOTALWEIGHT_AVG != null && inst.UNCOATEDWEIGHT_AVG == null && inst.COATINGWEIGHT_AVG == null)
                        {
                            inst.ChkHide3 = false;
                            inst.ChkHide2 = false;
                            inst.ChkHide1 = true;
                        }
                        else if (inst.TOTALWEIGHT_AVG != null && inst.UNCOATEDWEIGHT_AVG == null && inst.COATINGWEIGHT_AVG != null)
                        {
                            if (inst.CUSTOMERID == "01" || inst.CUSTOMERID == "08")
                            {
                                inst.ChkHide3 = false;
                                inst.ChkHide2 = true;
                                inst.ChkHide1 = false;
                            }
                            else
                            {
                                inst.ChkHide3 = false;
                                inst.ChkHide2 = false;
                                inst.ChkHide1 = true;
                            }
                        }
                        else if (inst.TOTALWEIGHT_AVG == null && inst.UNCOATEDWEIGHT_AVG == null && inst.COATINGWEIGHT_AVG == null)
                        {
                            inst.ChkHide3 = false;
                            inst.ChkHide2 = false;
                            inst.ChkHide1 = false;
                        }
                        else
                        {
                            inst.ChkHide3 = false;
                            inst.ChkHide2 = false;
                            inst.ChkHide1 = false;
                        }

                        #endregion

                        inst.MAXFORCE_W_AVG = lot.MAXFORCE_W_AVG;
                        inst.MAXFORCE_W_JUD = lot.MAXFORCE_W_JUD;

                        inst.MAXFORCE_F_AVG = lot.MAXFORCE_F_AVG;
                        inst.MAXFORCE_F_JUD = lot.MAXFORCE_F_JUD;

                        inst.ELONGATIONFORCE_W_AVG = lot.ELONGATIONFORCE_W_AVG;
                        inst.ELONGATIONFORCE_W_JUD = lot.ELONGATIONFORCE_W_JUD;

                        inst.ELONGATIONFORCE_F_AVG = lot.ELONGATIONFORCE_F_AVG;
                        inst.ELONGATIONFORCE_F_JUD = lot.ELONGATIONFORCE_F_JUD;

                        inst.FLAMMABILITY_W = lot.FLAMMABILITY_W;
                        inst.FLAMMABILITY_W_JUD = lot.FLAMMABILITY_W_JUD;

                        inst.FLAMMABILITY_F = lot.FLAMMABILITY_F;
                        inst.FLAMMABILITY_F_JUD = lot.FLAMMABILITY_F_JUD;

                        inst.EDGECOMB_W_AVG = lot.EDGECOMB_W_AVG;
                        inst.EDGECOMB_W_JUD = lot.EDGECOMB_W_JUD;

                        inst.EDGECOMB_F_AVG = lot.EDGECOMB_F_AVG;
                        inst.EDGECOMB_F_JUD = lot.EDGECOMB_F_JUD;

                        inst.STIFFNESS_W_AVG = lot.STIFFNESS_W_AVG;
                        inst.STIFFNESS_W_JUD = lot.STIFFNESS_W_JUD;

                        inst.STIFFNESS_F_AVG = lot.STIFFNESS_F_AVG;
                        inst.STIFFNESS_F_JUD = lot.STIFFNESS_F_JUD;

                        inst.TEAR_W_AVG = lot.TEAR_W_AVG;
                        inst.TEAR_W_JUD = lot.TEAR_W_JUD;

                        inst.TEAR_F_AVG = lot.TEAR_F_AVG;
                        inst.TEAR_F_JUD = lot.TEAR_F_JUD;

                        inst.STATIC_AIR_AVG = lot.STATIC_AIR_AVG;
                        inst.STATIC_AIR_JUD = lot.STATIC_AIR_JUD;

                        inst.FLEXABRASION_W_AVG = lot.FLEXABRASION_W_AVG;
                        inst.FLEXABRASION_F_AVG = lot.FLEXABRASION_F_AVG;

                        #region FLEXABRASION_W_JUD
                        if (!string.IsNullOrEmpty(inst.CUSTOMERID))
                        {
                            if (inst.CUSTOMERID == "08")
                            {
                                if (lot.FLEXABRASION_W_JUD == "PASS")
                                {
                                    inst.FLEXABRASION_W_JUD = "OK";  
                                }
                                else if (lot.FLEXABRASION_W_JUD == "FAIL")
                                {
                                    inst.FLEXABRASION_W_JUD = "NG";
                                }
                            }
                            else
                            {
                                inst.FLEXABRASION_W_JUD = lot.FLEXABRASION_W_JUD;
                            }
                        }
                        else
                        {
                            inst.FLEXABRASION_W_JUD = lot.FLEXABRASION_W_JUD;
                        }
                        #endregion

                        #region FLEXABRASION_F_JUD
                        if (!string.IsNullOrEmpty(inst.CUSTOMERID))
                        {
                            if (inst.CUSTOMERID == "08")
                            {
                                if (lot.FLEXABRASION_F_JUD == "PASS")
                                {
                                    inst.FLEXABRASION_F_JUD = "OK";
                                }
                                else if (lot.FLEXABRASION_F_JUD == "FAIL")
                                {
                                    inst.FLEXABRASION_F_JUD = "NG";
                                }
                            }
                            else
                            {
                                inst.FLEXABRASION_F_JUD = lot.FLEXABRASION_F_JUD;
                            }
                        }
                        else
                        {
                            inst.FLEXABRASION_F_JUD = lot.FLEXABRASION_F_JUD;
                        }
                        #endregion

                        inst.DIMENSCHANGE_W_AVG = lot.DIMENSCHANGE_W_AVG;
                        inst.DIMENSCHANGE_W_JUD = lot.DIMENSCHANGE_W_JUD;

                        inst.DIMENSCHANGE_F_AVG = lot.DIMENSCHANGE_F_AVG;
                        inst.DIMENSCHANGE_F_JUD = lot.DIMENSCHANGE_F_JUD;

                        inst.JUDGEMENT = lot.JUDGEMENT;

                        // 28/10/20
                        inst.THICKNESS_AVG = lot.THICKNESS_AVG;
                        inst.THICKNESS_JUD = lot.THICKNESS_JUD;

                        inst.BENDING_W_AVG = lot.BENDING_W_AVG;
                        inst.BENDING_W_JUD = lot.BENDING_W_JUD;

                        inst.BENDING_F_AVG = lot.BENDING_F_AVG;
                        inst.BENDING_F_JUD = lot.BENDING_F_JUD;

                        inst.DYNAMIC_AIR_AVG = lot.DYNAMIC_AIR_AVG;
                        inst.DYNAMIC_AIR_JUD = lot.DYNAMIC_AIR_JUD;

                        inst.EXPONENT_AVG = lot.EXPONENT_AVG;
                        inst.EXPONENT_JUD = lot.EXPONENT_JUD;

                        if (inst.THICKNESS_AVG != null)
                        {
                            //if (inst.CUSTOMERID == "09" || inst.ITM_CODE == "4L46B25K")
                            if (inst.CUSTOMERID == "09" || inst.CUSTOMERID == "12" || inst.ITM_CODE == "4L46B25K")
                            {
                                inst.ChkTHICKNESS = false;
                            }
                            else
                            {
                                inst.ChkTHICKNESS = true;

                                //if (inst.ITM_CODE == "4L46B25K")
                                //{
                                //    inst.ChkTHICKNESS = false;
                                //}
                                //else
                                //{
                                //    inst.ChkTHICKNESS = true;
                                //}
                            }
                        }
                        else
                        {
                            inst.ChkTHICKNESS = false;
                        }

                        if (inst.USABLE_WIDTH_AVG != null || inst.WIDTH_SILICONE_AVG != null)
                        {
                            if (inst.CUSTOMERID == "08" || inst.CUSTOMERID == "01" || inst.ITM_CODE == "4L46B25K")
                            {
                                inst.strUSABLE_WIDTH = "(mm)";
                            }
                            else
                            {
                                inst.strUSABLE_WIDTH = "(cm)";
                            }
                        }
                        else
                        {
                            inst.strUSABLE_WIDTH = "(cm)";
                        }

                        #endregion

                        #region LAB_GETITEMTESTSPECIFICATION

                        if (!string.IsNullOrEmpty(lot.NUMTHREADS_W_Spe))
                            inst.NUMTHREADS_W_Spe = lot.NUMTHREADS_W_Spe;
                        else
                            inst.NUMTHREADS_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.NUMTHREADS_F_Spe))
                            inst.NUMTHREADS_F_Spe = lot.NUMTHREADS_F_Spe;
                        else
                            inst.NUMTHREADS_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.USABLE_Spe))
                            inst.USABLE_Spe = lot.USABLE_Spe;
                        else
                            inst.USABLE_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.WIDTH_SILICONE_Spe))
                            inst.WIDTH_SILICONE_Spe = lot.WIDTH_SILICONE_Spe;
                        else
                            inst.WIDTH_SILICONE_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.TOTALWEIGHT_Spe))
                            inst.TOTALWEIGHT_Spe = lot.TOTALWEIGHT_Spe;
                        else
                            inst.TOTALWEIGHT_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.UNCOATEDWEIGHT_Spe))
                            inst.UNCOATEDWEIGHT_Spe = lot.UNCOATEDWEIGHT_Spe;
                        else
                            inst.UNCOATEDWEIGHT_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.COATINGWEIGHT_Spe))
                            inst.COATINGWEIGHT_Spe = lot.COATINGWEIGHT_Spe;
                        else
                            inst.COATINGWEIGHT_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.THICKNESS_Spe))
                            inst.THICKNESS_Spe = lot.THICKNESS_Spe;
                        else
                            inst.THICKNESS_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.MAXFORCE_W_Spe))
                            inst.MAXFORCE_W_Spe = lot.MAXFORCE_W_Spe;
                        else
                            inst.MAXFORCE_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.MAXFORCE_F_Spe))
                            inst.MAXFORCE_F_Spe = lot.MAXFORCE_F_Spe;
                        else
                            inst.MAXFORCE_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.ELONGATIONFORCE_W_Spe))
                            inst.ELONGATIONFORCE_W_Spe = lot.ELONGATIONFORCE_W_Spe;
                        else
                            inst.ELONGATIONFORCE_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.ELONGATIONFORCE_F_Spe))
                            inst.ELONGATIONFORCE_F_Spe = lot.ELONGATIONFORCE_F_Spe;
                        else
                            inst.ELONGATIONFORCE_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.FLAMMABILITY_W_Spe))
                            inst.FLAMMABILITY_W_Spe = lot.FLAMMABILITY_W_Spe;
                        else
                            inst.FLAMMABILITY_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.FLAMMABILITY_F_Spe))
                            inst.FLAMMABILITY_F_Spe = lot.FLAMMABILITY_F_Spe;
                        else
                            inst.FLAMMABILITY_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.EDGECOMB_W_Spe))
                            inst.EDGECOMB_W_Spe = lot.EDGECOMB_W_Spe;
                        else
                            inst.EDGECOMB_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.EDGECOMB_F_Spe))
                            inst.EDGECOMB_F_Spe = lot.EDGECOMB_F_Spe;
                        else
                            inst.EDGECOMB_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.STIFFNESS_W_Spe))
                            inst.STIFFNESS_W_Spe = lot.STIFFNESS_W_Spe;
                        else
                            inst.STIFFNESS_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.STIFFNESS_F_Spe))
                            inst.STIFFNESS_F_Spe = lot.STIFFNESS_F_Spe;
                        else
                            inst.STIFFNESS_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.TEAR_W_Spe))
                            inst.TEAR_W_Spe = lot.TEAR_W_Spe;
                        else
                            inst.TEAR_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.TEAR_F_Spe))
                            inst.TEAR_F_Spe = lot.TEAR_F_Spe;
                        else
                            inst.TEAR_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.STATIC_AIR_Spe))
                            inst.STATIC_AIR_Spe = lot.STATIC_AIR_Spe;
                        else
                            inst.STATIC_AIR_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.FLEXABRASION_W_Spe))
                            inst.FLEXABRASION_W_Spe = lot.FLEXABRASION_W_Spe;
                        else
                            inst.FLEXABRASION_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.FLEXABRASION_F_Spe))
                            inst.FLEXABRASION_F_Spe = lot.FLEXABRASION_F_Spe;
                        else
                            inst.FLEXABRASION_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.DIMENSCHANGE_W_Spe))
                            inst.DIMENSCHANGE_W_Spe = lot.DIMENSCHANGE_W_Spe;
                        else
                            inst.DIMENSCHANGE_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.DIMENSCHANGE_F_Spe))
                            inst.DIMENSCHANGE_F_Spe = lot.DIMENSCHANGE_F_Spe;
                        else
                            inst.DIMENSCHANGE_F_Spe = "-";

                        //Update 28/10/20
                        if (!string.IsNullOrEmpty(lot.THICKNESS_Spe))
                            inst.THICKNESS_Spe = lot.THICKNESS_Spe;
                        else
                            inst.THICKNESS_Spe = "-";
 
                        if (!string.IsNullOrEmpty(lot.BENDING_W_Spe))
                            inst.BENDING_W_Spe = lot.BENDING_W_Spe;
                        else
                            inst.BENDING_W_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.BENDING_F_Spe))
                            inst.BENDING_F_Spe = lot.BENDING_F_Spe;
                        else
                            inst.BENDING_F_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.DYNAMIC_AIR_Spe))
                            inst.DYNAMIC_AIR_Spe = lot.DYNAMIC_AIR_Spe;
                        else
                            inst.DYNAMIC_AIR_Spe = "-";

                        if (!string.IsNullOrEmpty(lot.EXPONENT_Spe))
                            inst.EXPONENT_Spe = lot.EXPONENT_Spe;
                        else
                            inst.EXPONENT_Spe = "-";

                        #endregion

                        #region LAB_GETREPORTINFO
                        //LAB_GETREPORTINFO
                        inst.REPORT_ID = lot.REPORT_ID;
                        REPORT_ID = inst.REPORT_ID;

                        //inst.REVESION = lot.REVESION;
                        if (!string.IsNullOrEmpty(lot.REVESION))
                            inst.REVESION = "(Specification issue " + lot.REVESION + ")";
                        else
                            inst.REVESION = string.Empty;

                        inst.USABLE_WIDTH = lot.USABLE_WIDTH;
                        inst.NUMTHREADS = lot.NUMTHREADS;
                        inst.WEIGHT = lot.WEIGHT;
                        
                        inst.FLAMMABILITY = lot.FLAMMABILITY;
                        inst.EDGECOMB = lot.EDGECOMB;
                        inst.STIFFNESS = lot.STIFFNESS;
                        inst.TEAR = lot.TEAR;
                        inst.STATIC_AIR = lot.STATIC_AIR;
                        inst.FLEXABRASION = lot.FLEXABRASION;


                        if (inst.CUSTOMERID == "01")
                        {
                            //inst.DIMENSCHANGE = "150°C X 30 min.";
                            inst.DIMENSCHANGE = "JIS -L-1042 (based upon) 150°C X 30 min.";
                        }
                        else
                        {
                            inst.DIMENSCHANGE = lot.DIMENSCHANGE;
                        }


                        inst.EFFECTIVE_DATE = lot.EFFECTIVE_DATE;

                        // ปรับเพิ่ม
                        inst.YARNTYPE = lot.YARNTYPE;
                        inst.COATWEIGHT = lot.COATWEIGHT;
                        inst.THICKNESS = lot.THICKNESS;
                        inst.MAXFORCE = lot.MAXFORCE;
                        inst.ELONGATIONFORCE = lot.ELONGATIONFORCE;
                        inst.DYNAMIC_AIR = lot.DYNAMIC_AIR;
                        inst.EXPONENT = lot.EXPONENT;
                        inst.BENDING = lot.BENDING;

                        inst.REPORT_NAME = lot.REPORT_NAME;

                        inst.BOW = lot.BOW;
                        inst.SKEW = lot.SKEW;
                        inst.FLEX_SCOTT = lot.FLEX_SCOTT;

                        #endregion

                        #region New 31/08/21

                        inst.BOW_AVG = lot.BOW_AVG;
                        inst.BOW_JUD = lot.BOW_JUD;
                        if (!string.IsNullOrEmpty(lot.BOW_Spe))
                        {
                            if (inst.CUSTOMERID == "01")
                            {
                                if (lot.BOW_Spe.Contains("MAX") && lot.BOW_Max != null)
                                    inst.BOW_Spe = "Less Than " + lot.BOW_Max.Value.ToString("#,##0.##");
                                else if (lot.BOW_Spe.Contains("MIN") && lot.BOW_Min != null)
                                    inst.BOW_Spe = "Great Than " + lot.BOW_Min.Value.ToString("#,##0.##");
                                else
                                    inst.BOW_Spe = lot.BOW_Spe;
                            }
                            else
                            {
                                inst.BOW_Spe = lot.BOW_Spe;
                            }
                        }
                        else
                            inst.BOW_Spe = "-";

                        inst.SKEW_AVG = lot.SKEW_AVG;
                        inst.SKEW_JUD = lot.SKEW_JUD;
                        if (!string.IsNullOrEmpty(lot.SKEW_Spe))
                        {
                            if (inst.CUSTOMERID == "01")
                            {
                                if (lot.SKEW_Spe.Contains("MAX") && lot.SKEW_Max != null)
                                    inst.SKEW_Spe = "Less Than " + lot.SKEW_Max.Value.ToString("#,##0.##");
                                else if (lot.SKEW_Spe.Contains("MIN") && lot.SKEW_Min != null)
                                    inst.SKEW_Spe = "Great Than " + lot.SKEW_Min.Value.ToString("#,##0.##");
                                else
                                    inst.SKEW_Spe = lot.SKEW_Spe;
                            }
                            else
                            {
                                inst.SKEW_Spe = lot.SKEW_Spe;
                            }     
                        }
                        else
                            inst.SKEW_Spe = "-";

                        inst.FLEX_SCOTT_F_AVG = lot.FLEX_SCOTT_F_AVG;
                        inst.FLEX_SCOTT_F_JUD = lot.FLEX_SCOTT_F_JUD;
                        inst.FLEX_SCOTT_F_JUD2 = lot.FLEX_SCOTT_F_JUD2;
                        if (!string.IsNullOrEmpty(lot.FLEX_SCOTT_F_Spe))
                            inst.FLEX_SCOTT_F_Spe = lot.FLEX_SCOTT_F_Spe;
                        else
                            inst.FLEX_SCOTT_F_Spe = "-";

                        inst.FLEX_SCOTT_W_AVG = lot.FLEX_SCOTT_W_AVG;
                        inst.FLEX_SCOTT_W_JUD = lot.FLEX_SCOTT_W_JUD;
                        inst.FLEX_SCOTT_W_JUD2 = lot.FLEX_SCOTT_W_JUD2;
                        if (!string.IsNullOrEmpty(lot.FLEX_SCOTT_W_Spe))
                            inst.FLEX_SCOTT_W_Spe = lot.FLEX_SCOTT_W_Spe;
                        else
                            inst.FLEX_SCOTT_W_Spe = "-";

                        #region Hide Item Code 4L53AS , 3L60AS

                        inst.ChkSTIFFNESS = false;
                        inst.ChkDYNAMIC_AIR = false;
                        inst.ChkEXPONENT = false;
                        inst.ChkDIMENSCHANGE = false;

                        if (inst.ITM_CODE == "4L53AS" || inst.ITM_CODE == "3L60AS")
                        {
                            inst.ChkSTIFFNESS = true;
                            inst.ChkDYNAMIC_AIR = true;
                            inst.ChkEXPONENT = true;
                            inst.ChkDIMENSCHANGE = true;
                        }
                        else
                        {
                            if (inst.STIFFNESS_W_AVG == null && inst.STIFFNESS_F_AVG == null)
                            {
                                inst.ChkSTIFFNESS = true;
                            }

                            if (inst.DYNAMIC_AIR_AVG == null)
                            {
                                inst.ChkDYNAMIC_AIR = true;
                            }

                            if (inst.EXPONENT_AVG == null)
                            {
                                inst.ChkEXPONENT = true;
                            }

                            if (inst.DIMENSCHANGE_W_AVG == null && inst.DIMENSCHANGE_F_AVG == null)
                            {
                                inst.ChkDIMENSCHANGE = true;
                            }
                        }

                        #endregion

                        #region Check CUSTOMERID == "01"
                        inst.ChkFLEX_SCOTT = false;
                        inst.ChkBOW = false;
                        inst.ChkSKEW = false;

                        if (inst.CUSTOMERID == "01")
                        {
                            if (inst.FLEX_SCOTT_W_AVG != null && inst.FLEX_SCOTT_F_AVG != null)
                            {
                                inst.ChkFLEX_SCOTT = true;
                            }

                            if (inst.BOW_AVG != null)
                            {
                                inst.ChkBOW = true;
                            }

                            if (inst.SKEW_AVG != null)
                            {
                                inst.ChkSKEW = true;
                            }
                        }
                        #endregion

                        #endregion

                        if (inst.ChkCM == true)
                        {
                            List<LAB_GETWEAVINGLOTLIST> lots2 = LABDataService.Instance.LAB_GETWEAVINGLOTLIST(inst.FINISHINGLOT, inst.ITM_CODE, inst.FINISHINGPROCESS);

                            if (null != lots2 && lots2.Count > 0 && null != lots2[0])
                            {
                                string wList = string.Empty;
                                int? i = 1;
                                int? c = lots2.Count;

                                foreach (LAB_GETWEAVINGLOTLIST dbResult in lots2)
                                {
                                    if (i != c)
                                        wList += dbResult.WEAVINGLOT + "     " + "\t";
                                    else
                                        wList += dbResult.WEAVINGLOT;

                                    i++;
                                }

                                inst.WEAVINGLOTLIST = wList;
                            }
                        }

                        results.Add(inst);

                        if (results.Count > 0)
                        {
                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                            rds.Value = results;
                            this._reportViewer.LocalReport.DataSources.Clear();
                            this._reportViewer.LocalReport.DataSources.Add(rds);


                            if (!string.IsNullOrEmpty(REPORT_ID))
                            {
                                if (REPORT_ID == "F1")
                                {
                                    if (CM_ID == "01" || CM_ID == "06")
                                    {
                                        if (CM_ID == "01")
                                        {
                                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFM-QC-26-05_CM01.rdlc";
                                        }
                                        else if (CM_ID == "06")
                                        {
                                            this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFM-QC-26-05_CM010506.rdlc";
                                        }
                                    }
                                    else
                                    {
                                        this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFM-QC-26-05_CM0105.rdlc";
                                    }
                                    //if (inst.ChkCM == true)
                                    //{
                                        //this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFM-QC-26-05_CM0105.rdlc";
                                    //}
                                    //else
                                    //{
                                    //    this._reportViewer.LocalReport.ReportPath = path + @"\Report\RepFM-QC-26-05.rdlc";
                                    //}
                                }
                            }

                            this._reportViewer.LocalReport.EnableExternalImages = true;
                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                            this._reportViewer.LocalReport.SetParameters(parameters);

                            this._reportViewer.RefreshReport();

                            SetSize("A4", false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
