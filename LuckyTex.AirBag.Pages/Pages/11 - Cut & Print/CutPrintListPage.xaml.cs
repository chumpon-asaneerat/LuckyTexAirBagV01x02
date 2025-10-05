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
using System.Windows.Navigation;
using System.Windows.Shapes;

using NLib;
using LuckyTex.Services;
using LuckyTex.Models;

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for CutPrintListPage.xaml
    /// </summary>
    public partial class CutPrintListPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public CutPrintListPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string gidITEMLOT = string.Empty;

        CutPrintSession _session = new CutPrintSession();

        string ITEMLOT = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCutPrintMCItem();

            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadCUT_SERACHLIST();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdPrint_Click

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gidITEMLOT))
            {
                Preview();
            }
        }

        #endregion

        #region cmdCutPrintRecord_Click
        private void cmdCutPrintRecord_Click(object sender, RoutedEventArgs e)
        {
            if (dteSTARTDATE.SelectedDate != null)
            {
                if (cbMCItem.SelectedValue != null)
                {
                    if (checkCUT() == true)
                    {
                        try
                        {
                            ConmonReportService.Instance.ReportName = "CUT_SERACHLIST";
                            ConmonReportService.Instance.P_DATE = dteSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                            ConmonReportService.Instance.P_MC = cbMCItem.SelectedValue.ToString();

                            var newWindow = new RepMasterForm();
                            newWindow.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
                        }          
                    }
                    else
                    {
                        "ไม่พบข้อมูลการผลิต".ShowMessageBox();
                    }
                }
                else
                {
                    "Please select Printer".ShowMessageBox();
                }
            }
            else
            {
                "Please select Start Date".ShowMessageBox();
            }
        }
        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ITEMLOT))
            {
                if (D365_CP_BPO() == true)
                {
                    if (PRODID != null)
                    {
                        if (D365_CP_ISH(PRODID) == true)
                        {
                            if (HEADERID != null)
                            {
                                if (D365_CP_ISL(HEADERID) == true)
                                {
                                    if (D365_CP_OPH(PRODID) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_CP_OPL(HEADERID) == true)
                                            {
                                                if (D365_CP_OUH(PRODID) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_CP_OUL(HEADERID) == true)
                                                        {
                                                            "Send D365 complete".ShowMessageBox();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        "HEADERID is null".Info();
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            "HEADERID is null".Info();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                "HEADERID is null".Info();
                            }
                        }
                    }
                    else
                    {
                        "PRODID is null".Info();
                    }
                }
            }
            else
            {
                "Item Lot is null".ShowMessageBox();
            }
        }
        #endregion

        #endregion

        #region gridOperator_SelectedCellsChanged

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void gridCutPrint_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridCutPrint.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridCutPrint);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            #region gidITEMLOT

                            if (((LuckyTex.Models.CUT_SERACHLIST)(gridCutPrint.CurrentCell.Item)).ITEMLOT != null)
                            {
                                gidITEMLOT = ((LuckyTex.Models.CUT_SERACHLIST)(gridCutPrint.CurrentCell.Item)).ITEMLOT;

                                if (!string.IsNullOrEmpty(gidITEMLOT))
                                {
                                    ITEMLOT = gidITEMLOT;
                                }
                                else
                                {
                                    gidITEMLOT = null;
                                    ITEMLOT = null;
                                }
                            }
                            else
                            {
                                gidITEMLOT = "";
                                ITEMLOT = null;
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    gidITEMLOT = string.Empty;

                    ITEMLOT = string.Empty;
                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadCutPrintMCItem

        private void LoadCutPrintMCItem()
        {
            try
            {
                List<CutPrintMCItem> cpMCItem = CutPrintDataService.Instance.GetCutPrintMCItem();

                if (cpMCItem != null)
                {
                    cbMCItem.ItemsSource = cpMCItem;
                    this.cbMCItem.DisplayMemberPath = "DisplayName";
                    this.cbMCItem.SelectedValuePath = "MCId";

                    cbMCItem.SelectedIndex = 0;
                }
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            dteSTARTDATE.SelectedDate = null;
            dteSTARTDATE.Text = "";
            cbMCItem.SelectedValue = null;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridCutPrint.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridCutPrint.SelectedItems.Clear();
            else
                this.gridCutPrint.SelectedItem = null;

            gridCutPrint.ItemsSource = null;

            gidITEMLOT = string.Empty;

            ITEMLOT = string.Empty;
            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
        }

        #endregion

        #region LoadCUT_SERACHLIST
        private void LoadCUT_SERACHLIST()
        {
            string _StartDate = string.Empty;
            string _MC = string.Empty;

            if (dteSTARTDATE.SelectedDate != null)
                _StartDate = dteSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            if (cbMCItem.SelectedValue != null)
                _MC = cbMCItem.SelectedValue.ToString();

            #region Old
            //List<CUT_SERACHLIST> lots = new List<CUT_SERACHLIST>();

            //lots = _session.GetCUT_SERACHLIST(_StartDate, _MC);

            //if (null != lots && lots.Count > 0 && null != lots[0])
            //{
            //    gridCutPrint.ItemsSource = lots;
            //}
            //else
            //{
            //    gridCutPrint.ItemsSource = null;

            //    gidITEMLOT = string.Empty;
            //}
            #endregion

            try
            {

                List<CUT_SERACHLIST> results = null;

                LuckyTex.Domains.CUT_SERACHLISTParameter dbPara = new LuckyTex.Domains.CUT_SERACHLISTParameter();
                dbPara.P_STARTDATE = _StartDate;
                dbPara.P_MC = _MC;

                List<LuckyTex.Domains.CUT_SERACHLISTResult> dbResults = null;


                dbResults = DatabaseManager.Instance.CUT_SERACHLIST(dbPara);

                if (null != dbResults)
                {
                    int? i = 0;
                    decimal? totalLength = 0;

                    results = new List<CUT_SERACHLIST>();
                    foreach (LuckyTex.Domains.CUT_SERACHLISTResult dbResult in dbResults)
                    {
                        CUT_SERACHLIST inst = new CUT_SERACHLIST();

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

                        if (inst.LENGTHPRINT != null)
                            totalLength += inst.LENGTHPRINT;

                        inst.STATUS = dbResult.STATUS;
                        inst.SUSPENDDATE = dbResult.SUSPENDDATE;
                        inst.SUSPENDBY = dbResult.SUSPENDBY;
                        inst.CLEARDATE = dbResult.CLEARDATE;
                        inst.CLEARREMARK = dbResult.CLEARREMARK;
                        inst.CLEARBY = dbResult.CLEARBY;
                        inst.SUSPENDSTARTDATE = dbResult.SUSPENDSTARTDATE;

                        //เพิ่มใหม่ 25/08/17
                        inst.LENGTHDETAIL = dbResult.LENGTHDETAIL;

                        results.Add(inst);

                        i++;
                    }

                    gridCutPrint.ItemsSource = results;

                    if (i != null)
                        txtTotalLot.Text = i.Value.ToString("#,##0");

                    if (totalLength != null)
                        txtTotalLeng.Text = totalLength.Value.ToString("#,##0.##");
                }
                else
                {
                    gridCutPrint.ItemsSource = null;
                    gidITEMLOT = string.Empty;

                    ITEMLOT = string.Empty;
                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;

                    txtTotalLot.Text = "0";
                    txtTotalLeng.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        #endregion

        #region checkCUT
        private bool checkCUT()
        {
            try
            {
                string _StartDate = string.Empty;
                string _MC = string.Empty;

                if (dteSTARTDATE.SelectedDate != null)
                    _StartDate = dteSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                if (cbMCItem.SelectedValue != null)
                    _MC = cbMCItem.SelectedValue.ToString();

                List<CUT_SERACHLIST> lots = new List<CUT_SERACHLIST>();

                lots = _session.GetCUT_SERACHLIST(_StartDate, _MC);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region D365_CP_BPO
        private bool D365_CP_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_BPOData> results = new List<ListD365_CP_BPOData>();

                results = D365DataService.Instance.D365_CP_BPO(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                            PRODID = Convert.ToInt64(results[i].PRODID);
                        else
                            PRODID = null;

                        if (!string.IsNullOrEmpty(results[i].LOTNO))
                            P_LOTNO = results[i].LOTNO;
                        else
                            P_LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            P_ITEMID = results[i].ITEMID;
                        else
                            P_ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

                        if (PRODID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_BPO Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_ISH
        private bool D365_CP_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_CP_ISHData> results = new List<D365_CP_ISHData>();

                results = D365DataService.Instance.D365_CP_ISH(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_ISH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_ISL
        private bool D365_CP_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_ISLData> results = new List<ListD365_CP_ISLData>();

                results = D365DataService.Instance.D365_CP_ISL(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string issDate = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].ISSUEDATE != null)
                            issDate = results[i].ISSUEDATE.Value.ToString("yyyy-MM-dd");
                        else
                            issDate = string.Empty;

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, "N", 0, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_ISL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OPH
        private bool D365_CP_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_CP_OPHData> results = new List<D365_CP_OPHData>();

                results = D365DataService.Instance.D365_CP_OPH(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OPH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OPL
        private bool D365_CP_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_OPLData> results = new List<ListD365_CP_OPLData>();

                results = D365DataService.Instance.D365_CP_OPL(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OPL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OUH
        private bool D365_CP_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_CP_OUHData> results = new List<D365_CP_OUHData>();

                results = D365DataService.Instance.D365_CP_OUH(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OUH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_CP_OUL
        private bool D365_CP_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_CP_OULData> results = new List<ListD365_CP_OULData>();

                results = D365DataService.Instance.D365_CP_OUL(ITEMLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string outputDate = string.Empty;
                    int? finish = null;

                    foreach (var row in results)
                    {
                        if (results[i].OUTPUTDATE != null)
                            outputDate = results[i].OUTPUTDATE.Value.ToString("yyyy-MM-dd");
                        else
                            outputDate = string.Empty;

                        if (results[i].FINISH != null)
                            finish = Convert.ToInt32(results[i].FINISH);
                        else
                            finish = 0;

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);


                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_CP_OUL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print()
        {
            try
            {
                ConmonReportService.Instance.ReportName = "CUT_GETSLIP";
                ConmonReportService.Instance.ITEMLOT = gidITEMLOT;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview()
        {
            try
            {
                ConmonReportService.Instance.ReportName = "CUT_GETSLIP";
                ConmonReportService.Instance.ITEMLOT = gidITEMLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user)
        {
            if (opera != null)
            {
                opera = user;
            }
        }

        #endregion

    }
}
