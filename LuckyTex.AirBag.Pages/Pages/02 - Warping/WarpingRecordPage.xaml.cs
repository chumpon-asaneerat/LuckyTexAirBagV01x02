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
    /// Interaction logic for WarpingRecordPage.xaml
    /// </summary>
    public partial class WarpingRecordPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingRecordPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private WarpingSession _session = new WarpingSession();
        string opera = string.Empty;
        string gidWARPHEADNO = string.Empty;
        string gidSIDE = string.Empty;
        string gidMCNO = string.Empty;
        string gidITMPREPARE = string.Empty;

        string gidWTYPE = string.Empty;
        string gidCONDITIONBY = string.Empty;
        string gidREEDNO = string.Empty;
        DateTime? gidCONDITIONSTART = null;

        string gridWARPHEADNO = string.Empty;

        string WARPERLOT = string.Empty;

        string WARPHEADNO = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMachines();
            LoadItemGood();
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
            Search();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdWarpingRecord_Click

        private void cmdWarpingRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gidWARPHEADNO) && !string.IsNullOrEmpty(gidSIDE) && !string.IsNullOrEmpty(gidMCNO) && !string.IsNullOrEmpty(gidITMPREPARE))
            {
                PreviewWarpingRecord(gidWARPHEADNO, gidSIDE, gidMCNO, gidITMPREPARE, gidWTYPE, gidCONDITIONBY, gidREEDNO, gidCONDITIONSTART);
            }
            else
            {
                "Please select data in grid".ShowMessageBox();
            }
        }

        #endregion

        #region cmdRePrintTransferSlip_Click

        private void cmdRePrintTransferSlip_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gridWARPHEADNO) && !string.IsNullOrEmpty(WARPERLOT))
            {
                PreviewWarp_tranferSlip(gridWARPHEADNO, WARPERLOT);
            }
            else
            {
                "Please select data in grid".ShowMessageBox();
            }
        }

        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(WARPHEADNO))
            {
                if (D365_WP_BPO() == true)
                {
                    if (PRODID != null)
                    {
                        if (D365_WP_ISH(PRODID) == true)
                        {
                            if (HEADERID != null)
                            {
                                if (D365_WP_ISL(HEADERID) == true)
                                {
                                    if (D365_WP_OPH(PRODID) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_WP_OPL(HEADERID) == true)
                                            {
                                                if (D365_WP_OUH(PRODID) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_WP_OUL(HEADERID) == true)
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
                "Warper Lot is null".ShowMessageBox();
            }
        }
        #endregion

        #endregion

        #region gridWarping_SelectedCellsChanged

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

        private void gridWarping_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWarping.ItemsSource != null)
                {
                    gidWARPHEADNO = string.Empty;
                    gidSIDE = string.Empty;
                    gidMCNO = string.Empty;
                    gidITMPREPARE = string.Empty;

                    gidWTYPE = string.Empty;
                    gidCONDITIONBY = string.Empty;
                    gidREEDNO = string.Empty;
                    gidCONDITIONSTART = null;

                    gridWARPHEADNO = string.Empty;
                    WARPERLOT = string.Empty;

                    WARPHEADNO = string.Empty;
                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;

                    var row_list = GetDataGridRows(gridWarping);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            #region gidWARPHEADNO

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).WARPHEADNO != null)
                            {
                                gidWARPHEADNO = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).WARPHEADNO;

                                if (!string.IsNullOrEmpty(gidWARPHEADNO))
                                {
                                    WARPHEADNO = gidWARPHEADNO;
                                    GetWarperLot(gidWARPHEADNO);
                                }
                                else
                                {
                                    gidWARPHEADNO = null;
                                    WARPHEADNO = null;
                                }
                            }
                            else
                            {
                                gidWARPHEADNO = null;
                                WARPHEADNO = null;
                            }

                            #endregion

                            #region gidSIDE

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).SIDE != null)
                            {
                                gidSIDE = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).SIDE;
                            }
                            else
                            {
                                gidSIDE = null;
                            }

                            #endregion

                            #region gidMCNO

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).WARPMC != null)
                            {
                                gidMCNO = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).WARPMC;
                            }
                            else
                            {
                                gidMCNO = null;
                            }

                            #endregion

                            #region gidITMPREPARE

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).ITM_PREPARE != null)
                            {
                                gidITMPREPARE = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).ITM_PREPARE;
                            }
                            else
                            {
                                gidITMPREPARE = null;
                            }

                            #endregion

                            #region gidWTYPE

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).WTYPE != null)
                            {
                                gidWTYPE = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).WTYPE;
                            }
                            else
                            {
                                gidWTYPE = null;
                            }

                            #endregion

                            #region gidCONDITIONBY

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).CONDITIONBY != null)
                            {
                                gidCONDITIONBY = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).CONDITIONBY;
                            }
                            else
                            {
                                gidCONDITIONBY = null;
                            }

                            #endregion

                            #region gidREEDNO

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).REEDNO != null)
                            {
                                gidREEDNO = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).REEDNO;
                            }
                            else
                            {
                                gidREEDNO = null;
                            }

                            #endregion

                            #region gidCONDITIONSTART

                            if (((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).CONDITIONSTART != null)
                            {
                                gidCONDITIONSTART = ((LuckyTex.Models.WARP_SEARCHWARPRECORD)(gridWarping.CurrentCell.Item)).CONDITIONSTART;
                            }
                            else
                            {
                                gidCONDITIONSTART = null;
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    gidWARPHEADNO = string.Empty;
                    gidSIDE = string.Empty;
                    gidMCNO = string.Empty;
                    gidITMPREPARE = string.Empty;
                    gidWTYPE = string.Empty;
                    gidCONDITIONBY = string.Empty;
                    gidREEDNO = string.Empty;
                    gidCONDITIONSTART = null;

                    WARPHEADNO = string.Empty;
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

        #region gridWarp_getwarperlotbyheadno_SelectedCellsChanged

        private void gridWarp_getwarperlotbyheadno_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWarp_getwarperlotbyheadno.ItemsSource != null)
                {
                    gridWARPHEADNO = string.Empty;
                    WARPERLOT = string.Empty;
                   
                    var row_list = GetDataGridRows(gridWarp_getwarperlotbyheadno);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPHEADNO != null)
                            {
                                gridWARPHEADNO = ((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPHEADNO;
                            }
                            else
                            {
                                gridWARPHEADNO = string.Empty;
                            }

                            if (((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPERLOT != null)
                            {
                                WARPERLOT = ((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPERLOT;
                            }
                            else
                            {
                                WARPERLOT = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    gridWARPHEADNO = string.Empty;
                    WARPERLOT = string.Empty;
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

        #region PreviewWarp_tranferSlip

        private void PreviewWarp_tranferSlip(string WARPHEADNO, string WARPLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "TransferSlip";
                ConmonReportService.Instance.WARPHEADNO = WARPHEADNO;
                ConmonReportService.Instance.WARPLOT = WARPLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region PreviewWarpingRecord

        private void PreviewWarpingRecord(string P_WARPHEADNO, string P_SIDE, string P_WARPMC, string P_ITMPREPARE ,
           string P_WTYPE , string P_CONDITIONBY,string P_REEDNO ,DateTime? P_CONDITIONSTART )
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WarpingRecord";

                ConmonReportService.Instance.WARPHEADNO = P_WARPHEADNO;
                ConmonReportService.Instance.SIDE = P_SIDE;
                ConmonReportService.Instance.MCNO = P_WARPMC;
                ConmonReportService.Instance.ITMPREPARE = P_ITMPREPARE;


                ConmonReportService.Instance.WTYPE = P_WTYPE;
                ConmonReportService.Instance.CONDITIONBY = P_CONDITIONBY;
                ConmonReportService.Instance.REEDNO = P_REEDNO;
                ConmonReportService.Instance.CONDITIONSTART = P_CONDITIONSTART;

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

        #region LoadMachines

        private void LoadMachines()
        {
            try
            {
                List<WarpingMCItem> items = _session.GetMachines();

                this.cbWarpingMC.ItemsSource = items;
                this.cbWarpingMC.DisplayMemberPath = "DisplayName";
                this.cbWarpingMC.SelectedValuePath = "MCId";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMPREPARELIST> items = _session.GetItemCodeData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_PREPARE";
                this.cbItemCode.SelectedValuePath = "ITM_PREPARE";
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
            dteStartDate.SelectedDate = null;
            dteStartDate.Text = "";
            cbItemCode.Text = "";
            cbItemCode.SelectedItem = null;
            cbWarpingMC.Text = "";
            cbWarpingMC.SelectedItem = null;

            txtWARPHEADNO.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarping.SelectedItems.Clear();
            else
                this.gridWarping.SelectedItem = null;

            gridWarping.ItemsSource = null;

            if (this.gridWarp_getwarperlotbyheadno.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarp_getwarperlotbyheadno.SelectedItems.Clear();
            else
                this.gridWarp_getwarperlotbyheadno.SelectedItem = null;

            gridWarp_getwarperlotbyheadno.ItemsSource = null;

            gidWARPHEADNO = string.Empty;
            gidSIDE = string.Empty;
            gidMCNO = string.Empty;
            gidITMPREPARE = string.Empty;
            gidWTYPE = string.Empty;
            gidCONDITIONBY = string.Empty;
            gidREEDNO = string.Empty;
            gidCONDITIONSTART = null;

            gridWARPHEADNO = string.Empty;
            WARPERLOT = string.Empty;

            WARPHEADNO = string.Empty;
            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
        }

        #endregion

        #region Search
        private void Search()
        {
            string P_WARPHEADNO = string.Empty;
            string P_WARPMC = string.Empty;
            string P_ITMPREPARE = string.Empty;
            string P_STARTDATE = string.Empty;


            try
            {
                if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarping.SelectedItems.Clear();
                else
                    this.gridWarping.SelectedItem = null;

                gridWarping.ItemsSource = null;

                if (this.gridWarp_getwarperlotbyheadno.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarp_getwarperlotbyheadno.SelectedItems.Clear();
                else
                    this.gridWarp_getwarperlotbyheadno.SelectedItem = null;

                gridWarp_getwarperlotbyheadno.ItemsSource = null;

                gidWARPHEADNO = string.Empty;
                gidSIDE = string.Empty;
                gidMCNO = string.Empty;
                gidITMPREPARE = string.Empty;
                gidWTYPE = string.Empty;
                gidCONDITIONBY = string.Empty;
                gidREEDNO = string.Empty;
                gidCONDITIONSTART = null;

                gridWARPHEADNO = string.Empty;
                WARPERLOT = string.Empty;

                WARPHEADNO = string.Empty;
                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;

                if (!string.IsNullOrEmpty(txtWARPHEADNO.Text))
                    P_WARPHEADNO = txtWARPHEADNO.Text;
                else
                    P_WARPHEADNO = "%";

                if (cbWarpingMC.SelectedValue != null)
                    P_WARPMC = cbWarpingMC.SelectedValue.ToString();

                if (cbItemCode.SelectedValue != null)
                    P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                if (dteStartDate.SelectedDate != null)
                    P_STARTDATE = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                List<WARP_SEARCHWARPRECORD> results = new List<WARP_SEARCHWARPRECORD>();

                results = WarpingDataService.Instance.WARP_SEARCHWARPRECORD(P_WARPHEADNO, P_WARPMC, P_ITMPREPARE, P_STARTDATE);

                if (results.Count > 0)
                {
                    List<LuckyTex.Models.WARP_SEARCHWARPRECORD> dataList = new List<LuckyTex.Models.WARP_SEARCHWARPRECORD>();
                    int i = 0;

                    foreach (var row in results)
                    {
                        LuckyTex.Models.WARP_SEARCHWARPRECORD dataItemNew = new LuckyTex.Models.WARP_SEARCHWARPRECORD();

                        dataItemNew.WARPHEADNO = results[i].WARPHEADNO;
                        dataItemNew.ITM_PREPARE = results[i].ITM_PREPARE;
                        dataItemNew.PRODUCTTYPEID = results[i].PRODUCTTYPEID;
                        dataItemNew.WARPMC = results[i].WARPMC;
                        dataItemNew.SIDE = results[i].SIDE;
                        dataItemNew.ACTUALCH = results[i].ACTUALCH;
                        dataItemNew.WTYPE = results[i].WTYPE;
                        dataItemNew.STARTDATE = results[i].STARTDATE;
                        dataItemNew.CREATEBY = results[i].CREATEBY;
                        dataItemNew.CONDITIONSTART = results[i].CONDITIONSTART;
                        dataItemNew.CONDITIONBY = results[i].CONDITIONBY;
                        dataItemNew.ENDDATE = results[i].ENDDATE;
                        dataItemNew.STATUS = results[i].STATUS;
                        dataItemNew.FINISHBY = results[i].FINISHBY;
                        dataItemNew.FINISHFLAG = results[i].FINISHFLAG;
                        dataItemNew.REEDNO = results[i].REEDNO;
                        dataItemNew.EDITBY = results[i].EDITBY;
                        dataItemNew.EDITDATE = results[i].EDITDATE;

                        dataList.Add(dataItemNew);
                        i++;
                    }

                    this.gridWarping.ItemsSource = dataList;
                }
                else
                {
                    // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                    if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridWarping.SelectedItems.Clear();
                    else
                        this.gridWarping.SelectedItem = null;

                    gridWarping.ItemsSource = null;

                    if (this.gridWarp_getwarperlotbyheadno.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridWarp_getwarperlotbyheadno.SelectedItems.Clear();
                    else
                        this.gridWarp_getwarperlotbyheadno.SelectedItem = null;

                    gridWarp_getwarperlotbyheadno.ItemsSource = null;
                }

            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion

        #region GetWarperLot
        private void GetWarperLot(string P_WARPHEADNO)
        {
            List<WARP_GETWARPERLOTBYHEADNO> results = new List<WARP_GETWARPERLOTBYHEADNO>();

            results = WarpingDataService.Instance.WARP_GETWARPERLOTBYHEADNO(P_WARPHEADNO);

            if (results.Count > 0)
            {
                List<LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO> dataList = new List<LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO dataItemNew = new LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO();

                    dataItemNew.WARPHEADNO = results[i].WARPHEADNO;
                    dataItemNew.WARPERLOT = results[i].WARPERLOT;
                    dataItemNew.BEAMNO = results[i].BEAMNO;
                    dataItemNew.SIDE = results[i].SIDE;
                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.ENDDATE = results[i].ENDDATE;
                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.SPEED = results[i].SPEED;
                    dataItemNew.HARDNESS_L = results[i].HARDNESS_L;
                    dataItemNew.HARDNESS_N = results[i].HARDNESS_N;
                    dataItemNew.HARDNESS_R = results[i].HARDNESS_R;
                    dataItemNew.TENSION = results[i].TENSION;
                    dataItemNew.STARTBY = results[i].STARTBY;
                    dataItemNew.DOFFBY = results[i].DOFFBY;
                    dataItemNew.FLAG = results[i].FLAG;
                    dataItemNew.WARPMC = results[i].WARPMC;

                    dataItemNew.REMARK = results[i].REMARK;
                    dataItemNew.TENSION_IT = results[i].TENSION_IT;
                    dataItemNew.TENSION_TAKEUP = results[i].TENSION_TAKEUP;
                    dataItemNew.MC_COUNT_L = results[i].MC_COUNT_L;
                    dataItemNew.MC_COUNT_S = results[i].MC_COUNT_S;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridWarp_getwarperlotbyheadno.ItemsSource = dataList;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWarp_getwarperlotbyheadno.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarp_getwarperlotbyheadno.SelectedItems.Clear();
                else
                    this.gridWarp_getwarperlotbyheadno.SelectedItem = null;

                gridWarp_getwarperlotbyheadno.ItemsSource = null;
            }
        }
        #endregion

        #region D365_WP_BPO
        private bool D365_WP_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_BPOData> results = new List<ListD365_WP_BPOData>();

                results = D365DataService.Instance.D365_WP_BPO(WARPHEADNO);

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
                    "D365_WP_BPO Row = 0".Info();
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

        #region D365_WP_ISH
        private bool D365_WP_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_WP_ISHData> results = new List<D365_WP_ISHData>();

                results = D365DataService.Instance.D365_WP_ISH(WARPHEADNO);

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
                    "D365_WP_ISH Row = 0".Info();
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

        #region D365_WP_ISL
        private bool D365_WP_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_ISLData> results = new List<ListD365_WP_ISLData>();

                results = D365DataService.Instance.D365_WP_ISL(WARPHEADNO);

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
                    "D365_WP_ISL Row = 0".Info();
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

        #region D365_WP_OPH
        private bool D365_WP_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_WP_OPHData> results = new List<D365_WP_OPHData>();

                results = D365DataService.Instance.D365_WP_OPH(WARPHEADNO);

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
                    "D365_WP_OPH Row = 0".Info();
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

        #region D365_WP_OPL
        private bool D365_WP_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_OPLData> results = new List<ListD365_WP_OPLData>();

                results = D365DataService.Instance.D365_WP_OPL(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME,results[i].ENDDATETIME);

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
                    "D365_WP_OPL Row = 0".Info();
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

        #region D365_WP_OUH
        private bool D365_WP_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_WP_OUHData> results = new List<D365_WP_OUHData>();

                results = D365DataService.Instance.D365_WP_OUH(WARPHEADNO);

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
                    "D365_WP_OUH Row = 0".Info();
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

        #region D365_WP_OUL
        private bool D365_WP_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_OULData> results = new List<ListD365_WP_OULData>();

                results = D365DataService.Instance.D365_WP_OUL(WARPHEADNO);

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
                    "D365_WP_OUL Row = 0".Info();
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
