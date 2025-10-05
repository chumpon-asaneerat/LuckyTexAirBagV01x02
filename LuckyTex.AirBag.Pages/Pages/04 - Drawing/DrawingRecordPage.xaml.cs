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
    /// Interaction logic for DrawingRecordPage.xaml
    /// </summary>
    public partial class DrawingRecordPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingRecordPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private DrawingSession _session = new DrawingSession();

        string opera = string.Empty;
        string mcName = string.Empty;
        string P_BEAMERROLL = string.Empty;

        string BEAMERROLL = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
            if (dteDrawingDate.SelectedDate != null)
            {

                DRAW_DAILYREPORT(dteDrawingDate.SelectedDate.Value.ToString("dd/MM/yyyy"));
            }
            else
            {
                DRAW_DAILYREPORT("");
            }
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdRePrint_Click

        private void cmdRePrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_BEAMERROLL))
            {
                Preview_DRAW_TRANSFERSLIP(P_BEAMERROLL);
            }
        }

        #endregion

        #region cmdPrintReport_Click

        private void cmdPrintReport_Click(object sender, RoutedEventArgs e)
        {
            if (dteDrawingDate.SelectedDate != null)
            {
                Preview(dteDrawingDate.SelectedDate.Value.ToString("dd/MM/yyyy"));
            }
        }

        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BEAMERROLL))
            {
                if (D365_DT_BPO() == true)
                {
                    if (PRODID != null)
                    {
                        if (D365_DT_ISH(PRODID) == true)
                        {
                            if (HEADERID != null)
                            {
                                if (D365_DT_ISL(HEADERID) == true)
                                {
                                    if (D365_DT_OPH(PRODID) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_DT_OPL(HEADERID) == true)
                                            {
                                                if (D365_DT_OUH(PRODID) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_DT_OUL(HEADERID) == true)
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
                "Beamer Roll is null".ShowMessageBox();
            }
        }
        #endregion

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #endregion

        #region gridDrawing

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

        private void gridDrawing_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridDrawing.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridDrawing);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            #region BEAMERROLL
                            if (((LuckyTex.Models.DRAW_DAILYREPORT)(gridDrawing.CurrentCell.Item)).BEAMLOT != null)
                            {
                                P_BEAMERROLL = ((LuckyTex.Models.DRAW_DAILYREPORT)(gridDrawing.CurrentCell.Item)).BEAMLOT;

                                if (!string.IsNullOrEmpty(P_BEAMERROLL))
                                {
                                    BEAMERROLL = P_BEAMERROLL;
                                }
                                else
                                {
                                    P_BEAMERROLL = string.Empty;
                                    BEAMERROLL = string.Empty;
                                }
                            }
                            else
                            {
                                P_BEAMERROLL = string.Empty;
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    P_BEAMERROLL = string.Empty;
                    txtTotal.Text = string.Empty;
                    txtTotalBeam.Text = string.Empty;

                    BEAMERROLL = string.Empty;
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

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string P_DATE)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "DRAW_DAILYREPORT";
                ConmonReportService.Instance.P_DATE = P_DATE;

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

        private void Preview(string P_DATE)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "DRAW_DAILYREPORT";
                ConmonReportService.Instance.P_DATE = P_DATE;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview_DRAW_TRANSFERSLIP

        private void Preview_DRAW_TRANSFERSLIP(string P_BEAMERROLL)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "DRAW_TRANSFERSLIP";
                ConmonReportService.Instance.P_BEAMERROLL = P_BEAMERROLL;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region DRAW_DAILYREPORT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_DATE"></param>
        private void DRAW_DAILYREPORT(string P_DATE)
        {
            try
            {
                List<DRAW_DAILYREPORT> lots = new List<DRAW_DAILYREPORT>();

                lots = DrawingDataService.Instance.DRAW_DAILYREPORT(P_DATE);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridDrawing.ItemsSource = lots;

                    GetTotalLength();
                }
                else
                {
                    gridDrawing.ItemsSource = null;

                    P_BEAMERROLL = string.Empty;
                    txtTotal.Text = string.Empty;
                    txtTotalBeam.Text = string.Empty;

                    BEAMERROLL = string.Empty;
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

        #region Clear Control
        /// <summary>
        /// 
        /// </summary>
        private void ClearControl()
        {
            P_BEAMERROLL = string.Empty;

            BEAMERROLL = string.Empty;
            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;

            dteDrawingDate.SelectedDate = DateTime.Now;
            
            txtTotal.Text = string.Empty;
            txtTotalBeam.Text = string.Empty;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridDrawing.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridDrawing.SelectedItems.Clear();
            else
                this.gridDrawing.SelectedItem = null;

            gridDrawing.ItemsSource = null;

            dteDrawingDate.Focus();
        }

        #endregion

        #region GetTotalLength
        /// <summary>
        /// 
        /// </summary>
        private void GetTotalLength()
        {
            try
            {
                if (gridDrawing.Items.Count > 0)
                {
                    decimal? totalBeam = 0;

                    int o = 0;
                    foreach (var row in gridDrawing.Items)
                    {
                        if (((LuckyTex.Models.DRAW_DAILYREPORT)((gridDrawing.Items)[o])).LENGTH != null)
                            totalBeam += ((LuckyTex.Models.DRAW_DAILYREPORT)((gridDrawing.Items)[o])).LENGTH;

                        o++;
                    }

                    txtTotal.Text = o.ToString("#,##0");
                    txtTotalBeam.Text = totalBeam.Value.ToString("#,##0");
                }
                else
                {
                    txtTotal.Text = "0";
                    txtTotalBeam.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region D365_DT_BPO
        private bool D365_DT_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_BPOData> results = new List<ListD365_DT_BPOData>();

                results = D365DataService.Instance.D365_DT_BPO(BEAMERROLL);

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
                    "D365_DT_BPO Row = 0".Info();
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

        #region D365_DT_ISH
        private bool D365_DT_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_DT_ISHData> results = new List<D365_DT_ISHData>();

                results = D365DataService.Instance.D365_DT_ISH(BEAMERROLL);

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
                    "D365_DT_ISH Row = 0".Info();
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

        #region D365_DT_ISL
        private bool D365_DT_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_ISLData> results = new List<ListD365_DT_ISLData>();

                results = D365DataService.Instance.D365_DT_ISL(BEAMERROLL);

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
                    "D365_DT_ISL Row = 0".Info();
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

        #region D365_DT_OPH
        private bool D365_DT_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_DT_OPHData> results = new List<D365_DT_OPHData>();

                results = D365DataService.Instance.D365_DT_OPH(BEAMERROLL);

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
                    "D365_DT_OPH Row = 0".Info();
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

        #region D365_DT_OPL
        private bool D365_DT_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_OPLData> results = new List<ListD365_DT_OPLData>();

                results = D365DataService.Instance.D365_DT_OPL(BEAMERROLL);

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
                    "D365_DT_OPL Row = 0".Info();
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

        #region D365_DT_OUH
        private bool D365_DT_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_DT_OUHData> results = new List<D365_DT_OUHData>();

                results = D365DataService.Instance.D365_DT_OUH(BEAMERROLL);

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
                    "D365_DT_OUH Row = 0".Info();
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

        #region D365_DT_OUL
        private bool D365_DT_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_OULData> results = new List<ListD365_DT_OULData>();

                results = D365DataService.Instance.D365_DT_OUL(BEAMERROLL);

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
                    "D365_DT_OUL Row = 0".Info();
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

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user, string DisplayName)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (mcName != null)
            {
                mcName = DisplayName;
            }
        }

        #endregion


    }
}

