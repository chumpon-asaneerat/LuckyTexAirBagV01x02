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

using System.Diagnostics;
using System.Printing;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WeavingProductionPage.xaml
    /// </summary>
    public partial class WeavingProductionPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingProductionPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemGood();
        }

        #endregion

        #region Internal Variables

        private WeavingSession _session = new WeavingSession();
        string opera = string.Empty;
        string gridBEAMROLL = string.Empty;
        string gridBEAMERNO = string.Empty;
        string gridBARNO = string.Empty;
        string gridLOOM = string.Empty;
        string gridITM_WEAVING = string.Empty;
        string gridWEAVINGLOT = string.Empty;
        DateTime? gridSTARTDATE = null;
        DateTime? gridFINISHDATE = null;
        string gridWEFTYARN = string.Empty;
        decimal? gridWIDTH = null;
        decimal? gridBEAMLENGTH = null;
        decimal? gridSPEED = null;

        string P_BEAMLOT = string.Empty;
        string P_WEAVINGLOT = string.Empty;
        decimal? P_DOFFNO = null;
        string P_LOOMNO = string.Empty;

        long? PRODID = null;
        long? HEADERID = null;


        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;
        decimal? P_TOTALRECORD = null;

        bool chkISHRow0 = false;

        #endregion

        #region UserControl_Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearInputs();

            if (!string.IsNullOrEmpty(opera))
                txtOperator.Text = opera;
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region TextBox

        #region txtLoomNo_KeyDown

        private void txtLoomNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBEAMLOT.SelectAll();
                txtBEAMLOT.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region txtBEAMLOT_KeyDown

        private void txtBEAMLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                dteSettingDate.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region Button Handlers

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            WEAV_SEARCHPRODUCTION();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        #endregion

        #region cmdWeavingRecord_Click

        private void cmdWeavingRecord_Click(object sender, RoutedEventArgs e)
        {
            WeavingRecord();
        }

        #endregion

        #region cmdWeavingHistory_Click

        private void cmdWeavingHistory_Click(object sender, RoutedEventArgs e)
        {
            WeavingHistory();
        }

        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_BEAMLOT) && !string.IsNullOrEmpty(P_WEAVINGLOT))
            {
                D365SelectInfo d365SelectInfo = new D365SelectInfo();

                d365SelectInfo = this.ShowD365SelectBox();

                if (d365SelectInfo != null)
                {
                    string P_action = string.Empty;
                    decimal? P_resent = null;

                    P_action = d365SelectInfo.Action;
                    P_resent = d365SelectInfo.Resent;

                    #region D365

                    if (D365_GR_BPO(P_action, P_resent) == true)
                    {
                        if (PRODID != null)
                        {
                            if (D365_GR_ISH(PRODID, P_action, P_resent) == true)
                            {
                                if (chkISHRow0 == false)
                                {
                                    if (HEADERID != null)
                                    {
                                        if (P_TOTALRECORD != 0)
                                        {
                                            #region D365_GR_ISL
                                            if (D365_GR_ISL(HEADERID, P_action, P_resent) == true)
                                            {
                                                if (D365_GR_OPH(PRODID, P_action, P_resent) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_GR_OPL(HEADERID, P_action, P_resent) == true)
                                                        {
                                                            if (D365_GR_OUH(PRODID, P_action, P_resent) == true)
                                                            {
                                                                if (HEADERID != null)
                                                                {
                                                                    if (D365_GR_OUL(HEADERID, P_action, P_resent) == true)
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
                                            #endregion
                                        }
                                        else
                                        {
                                            //ข้าม D365_GR_ISL
                                            #region D365_GR_OPH
                                            if (D365_GR_OPH(PRODID, P_action, P_resent) == true)
                                            {
                                                if (HEADERID != null)
                                                {
                                                    if (D365_GR_OPL(HEADERID, P_action, P_resent) == true)
                                                    {
                                                        if (D365_GR_OUH(PRODID, P_action, P_resent) == true)
                                                        {
                                                            if (HEADERID != null)
                                                            {
                                                                if (D365_GR_OUL(HEADERID, P_action, P_resent) == true)
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
                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        "HEADERID is null".Info();
                                    }
                                }
                                else
                                {
                                    //ข้าม D365_GR_ISL
                                    #region D365_GR_OPH
                                    if (D365_GR_OPH(PRODID, P_action, P_resent) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_GR_OPL(HEADERID, P_action, P_resent) == true)
                                            {
                                                if (D365_GR_OUH(PRODID, P_action, P_resent) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_GR_OUL(HEADERID, P_action, P_resent) == true)
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
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }

                    #endregion
                }
            }
            else
            {
                "Beamer Roll is null".ShowMessageBox();
            }
        }
        #endregion

        #endregion

        #region gridWeaving_SelectedCellsChanged

        #region GetDataGridRows

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

        #endregion

        private void gridWeaving_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWeaving.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWeaving);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            gridBEAMROLL = string.Empty;
                            gridBEAMERNO = string.Empty;
                            gridBARNO = string.Empty;
                            gridLOOM = string.Empty;
                            gridITM_WEAVING = string.Empty;
                            gridWEAVINGLOT = string.Empty;
                            gridSTARTDATE = null;
                            gridFINISHDATE = null;
                            gridWEFTYARN = string.Empty;
                            gridWIDTH = null;
                            gridBEAMLENGTH = null;
                            gridSPEED = null;

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BEAMLOT != null)
                            {
                                gridBEAMROLL = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BEAMLOT;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BEAMERNO != null)
                            {
                                gridBEAMERNO = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BEAMERNO;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).MC != null)
                            {
                                gridLOOM = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).MC;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).ITM_WEAVING != null)
                            {
                                gridITM_WEAVING = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).ITM_WEAVING;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BARNO != null)
                            {
                                gridBARNO = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BARNO;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).STARTDATE != null)
                            {
                                gridSTARTDATE = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).STARTDATE;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).FINISHDATE != null)
                            {
                                gridFINISHDATE = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).FINISHDATE;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).WEFTYARN != null)
                            {
                                gridWEFTYARN = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).WEFTYARN;
                            }

                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).WIDTH != null)
                            {
                                gridWIDTH = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).WIDTH;
                            }
                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BEAMLENGTH != null)
                            {
                                gridBEAMLENGTH = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).BEAMLENGTH;
                            }
                            if (((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).SPEED != null)
                            {
                                gridSPEED = ((LuckyTex.Models.WEAV_SEARCHPRODUCTION)(gridWeaving.CurrentCell.Item)).SPEED;
                            }

                            if (!string.IsNullOrEmpty(gridBEAMROLL) && !string.IsNullOrEmpty(gridLOOM))
                            {
                                WEAV_GETWEAVELISTBYBEAMROLL(gridBEAMROLL, gridLOOM);
                            }
                            else
                            {
                                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                                if (this.gridWeavingListByBeamRoll.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                    this.gridWeavingListByBeamRoll.SelectedItems.Clear();
                                else
                                    this.gridWeavingListByBeamRoll.SelectedItem = null;

                                gridWeavingListByBeamRoll.ItemsSource = null;
                            }
                        }
                    }
                }
                else
                {
                    gridBEAMROLL = string.Empty;
                    gridBEAMERNO = string.Empty;
                    gridBARNO = string.Empty;
                    gridLOOM = string.Empty;
                    gridITM_WEAVING = string.Empty;
                    gridWEAVINGLOT = string.Empty;
                    gridSTARTDATE = null;
                    gridFINISHDATE = null;
                    gridWEFTYARN = string.Empty;
                    gridWIDTH = null;
                    gridBEAMLENGTH = null;
                    gridSPEED = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region gridWeavingListByBeamRoll_SelectedCellsChanged

        private void gridWeavingListByBeamRoll_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWeavingListByBeamRoll.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWeavingListByBeamRoll);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            gridWEAVINGLOT = string.Empty;

                            P_BEAMLOT = string.Empty;
                            P_WEAVINGLOT = string.Empty;
                            P_DOFFNO = null;
                            P_LOOMNO = string.Empty;

                            PRODID = null;
                            HEADERID = null;

                            P_LOTNO = string.Empty;
                            P_ITEMID = string.Empty;
                            P_LOADINGTYPE = string.Empty;
                            P_TOTALRECORD = null;

                            chkISHRow0 = false;

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).BEAMLOT != null)
                            {
                                P_BEAMLOT = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).BEAMLOT;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                gridWEAVINGLOT = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).WEAVINGLOT;

                                if (!string.IsNullOrEmpty(gridWEAVINGLOT))
                                    P_WEAVINGLOT = gridWEAVINGLOT;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).DOFFNO != null)
                            {
                                P_DOFFNO = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).DOFFNO;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).LOOMNO != null)
                            {
                                P_LOOMNO = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeavingListByBeamRoll.CurrentCell.Item)).LOOMNO;
                            }
                        }
                    }
                }
                else
                {
                    gridWEAVINGLOT = string.Empty;

                    P_BEAMLOT = string.Empty;
                    P_WEAVINGLOT = string.Empty;
                    P_DOFFNO = null;
                    P_LOOMNO = string.Empty;

                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;
                    P_TOTALRECORD = null;

                    chkISHRow0 = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region ClearInputs

        private void ClearInputs()
        {
            txtLoomNo.Text = "";
            txtBEAMLOT.Text = "";
            dteSettingDate.SelectedDate = null;
            cbItemCode.SelectedValue = null;

            if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single)
                this.gridWeaving.SelectedItems.Clear();
            else
                this.gridWeaving.SelectedItem = null;

            gridWeaving.ItemsSource = null;

            if (this.gridWeavingListByBeamRoll.SelectionMode != DataGridSelectionMode.Single)
                this.gridWeavingListByBeamRoll.SelectedItems.Clear();
            else
                this.gridWeavingListByBeamRoll.SelectedItem = null;

            gridWeavingListByBeamRoll.ItemsSource = null;

            gridBEAMROLL = string.Empty;
            gridBEAMERNO = string.Empty;
            gridBARNO = string.Empty;
            gridLOOM = string.Empty;
            gridITM_WEAVING = string.Empty;
            gridWEAVINGLOT = string.Empty;

            P_BEAMLOT = string.Empty;
            P_WEAVINGLOT = string.Empty;
            P_DOFFNO = null;
            P_LOOMNO = string.Empty;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
            P_TOTALRECORD = null;

            chkISHRow0 = false;

            gridSTARTDATE = null;
            gridFINISHDATE = null;
            gridWEFTYARN = string.Empty;
            gridWIDTH = null;
            gridBEAMLENGTH = null;
            gridSPEED = null;

            ConmonReportService.Instance.LOOM = string.Empty;
            ConmonReportService.Instance.BEAMLOT = string.Empty;
            ConmonReportService.Instance.BEAMERNO = string.Empty;
            ConmonReportService.Instance.BARNO = string.Empty;
            ConmonReportService.Instance.ITM_Code = string.Empty;
            ConmonReportService.Instance.CmdStringDate = string.Empty;
            ConmonReportService.Instance.CmdStringStartDate = string.Empty;
            ConmonReportService.Instance.WEFTYARN = string.Empty;
            ConmonReportService.Instance.WIDTH = null;
            ConmonReportService.Instance.BEAMLENGTH = null;
            ConmonReportService.Instance.SPEED = null;
            ConmonReportService.Instance.WEAVINGLOT = string.Empty;
            ConmonReportService.Instance.ReportName = string.Empty;
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<WEAV_GETALLITEMWEAVING> items = _session.Weav_getAllItemWeaving();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_WEAVING";
                this.cbItemCode.SelectedValuePath = "ITM_WEAVING";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region WEAV_SEARCHPRODUCTION

        private void WEAV_SEARCHPRODUCTION()
        {
            string P_LOOMNO = string.Empty;
            string P_BEAMERROLL = string.Empty;
            string P_ITMWEAVING = string.Empty;
            string P_SETDATE = string.Empty;

            if (!string.IsNullOrEmpty(txtLoomNo.Text))
            {
                P_LOOMNO = txtLoomNo.Text;
            }

            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                P_BEAMERROLL = txtBEAMLOT.Text;
            }

            if (cbItemCode.SelectedValue != null)
            {
                P_ITMWEAVING = cbItemCode.SelectedValue.ToString();
            }

            if (dteSettingDate.SelectedDate != null)
            {
                P_SETDATE = dteSettingDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            }

            List<WEAV_SEARCHPRODUCTION> results = WeavingDataService.Instance.WEAV_SEARCHPRODUCTION(P_LOOMNO, P_BEAMERROLL, P_ITMWEAVING, P_SETDATE);

            if (results != null && results.Count > 0)
            {
                this.gridWeaving.ItemsSource = results;
            }
            else
            {
                if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWeaving.SelectedItems.Clear();
                else
                    this.gridWeaving.SelectedItem = null;

                gridWeaving.ItemsSource = null;
            }

            if (this.gridWeavingListByBeamRoll.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWeavingListByBeamRoll.SelectedItems.Clear();
            else
                this.gridWeavingListByBeamRoll.SelectedItem = null;

            gridWeavingListByBeamRoll.ItemsSource = null;
        }

        #endregion

        #region WEAV_GETWEAVELISTBYBEAMROLL

        private void WEAV_GETWEAVELISTBYBEAMROLL(string P_BEAMROLL, string P_LOOM)
        {
            List<WEAV_GETWEAVELISTBYBEAMROLL> results = WeavingDataService.Instance.WEAV_GETWEAVELISTBYBEAMROLL(P_BEAMROLL, P_LOOM);

            if (results != null && results.Count > 0)
            {
                this.gridWeavingListByBeamRoll.ItemsSource = results;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWeavingListByBeamRoll.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWeavingListByBeamRoll.SelectedItems.Clear();
                else
                    this.gridWeavingListByBeamRoll.SelectedItem = null;

                gridWeavingListByBeamRoll.ItemsSource = null;
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region WeavingHistory

        private void WeavingHistory()
        {
            try
            {
                if (!string.IsNullOrEmpty(gridLOOM) && !string.IsNullOrEmpty(gridBEAMROLL) && !string.IsNullOrEmpty(gridITM_WEAVING))
                {
                   
                    // ConmonReportService
                    ConmonReportService.Instance.LOOM = gridLOOM;
                    ConmonReportService.Instance.BEAMLOT = gridBEAMROLL;
                    ConmonReportService.Instance.BEAMERNO = gridBEAMERNO;
                    ConmonReportService.Instance.BARNO = gridBARNO;
                    ConmonReportService.Instance.ITM_Code = gridITM_WEAVING;
                    ConmonReportService.Instance.CmdStringDate = gridFINISHDATE.ToString();
                    ConmonReportService.Instance.CmdStringStartDate = gridSTARTDATE.ToString();
                    ConmonReportService.Instance.WEFTYARN = gridWEFTYARN;
                    ConmonReportService.Instance.WIDTH = gridWIDTH;
                    ConmonReportService.Instance.BEAMLENGTH = gridBEAMLENGTH;
                    ConmonReportService.Instance.SPEED = gridSPEED;

                    ConmonReportService.Instance.ReportName = "WeavingHistory";

                    var newWindow = new RepMasterForm();
                    newWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region WeavingRecord

        private void WeavingRecord()
        {
            try
            {
                if (!string.IsNullOrEmpty(gridWEAVINGLOT))
                {
                    ConmonReportService.Instance.LOOM = gridLOOM;
                    ConmonReportService.Instance.BEAMLOT = gridBEAMROLL;
                    ConmonReportService.Instance.BEAMERNO = gridBEAMERNO;
                    ConmonReportService.Instance.BARNO = gridBARNO;
                    ConmonReportService.Instance.ITM_Code = gridITM_WEAVING;
                    ConmonReportService.Instance.CmdStringDate = gridFINISHDATE.ToString();
                    ConmonReportService.Instance.CmdStringStartDate = gridSTARTDATE.ToString();
                    ConmonReportService.Instance.WEFTYARN = gridWEFTYARN;
                    ConmonReportService.Instance.WIDTH = gridWIDTH;
                    ConmonReportService.Instance.BEAMLENGTH = gridBEAMLENGTH;
                    ConmonReportService.Instance.SPEED = gridSPEED;


                    ConmonReportService.Instance.WEAVINGLOT = gridWEAVINGLOT;
                    ConmonReportService.Instance.ReportName = "WeavingRecord";
                   
                    var newWindow = new RepMasterForm();
                    newWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region D365_GR_BPO
        private bool D365_GR_BPO(string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_BPOData> results = new List<ListD365_GR_BPOData>();

                results = D365DataService.Instance.D365_GR_BPO(P_BEAMLOT, P_LOOMNO, P_DOFFNO);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        int i = 0;
                        string chkError = string.Empty;

                        string temp = string.Empty;
                        string temp2 = string.Empty;
                        string temp3 = string.Empty;
                        string temp4 = string.Empty;
                        string temp5 = string.Empty;

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
                                temp = "PRODID = " + PRODID.ToString();
                            else
                                temp = "PRODID = null";

                            if (!string.IsNullOrEmpty(P_LOTNO))
                                temp2 = "LOTNO = " + P_LOTNO;
                            else
                                temp2 = "LOTNO = null";

                            if (!string.IsNullOrEmpty(P_ITEMID))
                                temp3 = "ITEMID = " + P_ITEMID;
                            else
                                temp3 = "ITEMID = null";

                            if (!string.IsNullOrEmpty(P_LOADINGTYPE))
                                temp4 = "LOADINGTYPE = " + P_LOADINGTYPE;
                            else
                                temp4 = "LOADINGTYPE = null";

                            if (P_DOFFNO != null)
                                temp5 = "DOFFNO = " + P_DOFFNO.ToString();
                            else
                                temp5 = "DOFFNO = null";

                            temp.Info();
                            temp2.Info();
                            temp3.Info();
                            temp4.Info();
                            temp5.Info();

                            if (PRODID != null && P_DOFFNO == 1)
                            {
                                "Insert_ABBPO".Info();

                                chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, P_RESENT, P_ACTION, results[i].QTY, results[i].UNIT, results[i].OPERATION, results[i].MACHINESTART);

                                if (!string.IsNullOrEmpty(chkError))
                                {
                                    chkError.Err();
                                    chkError.ShowMessageBox();
                                    chkD365 = false;
                                    break;
                                }
                            }
                            else
                            {
                                "PRODID = null Or DOFFNO != 1 can't Insert_ABBPO".Err();
                            }

                            i++;
                        }
                    }
                    else
                    {
                        "D365_GR_BPO Row = 0".Info();
                    }
                }
                else
                {
                    "D365_GR_BPO return null".Err();
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

        #region D365_GR_ISH
        private bool D365_GR_ISH(long? PRODID, string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<D365_GR_ISHData> results = new List<D365_GR_ISHData>();

                results = D365DataService.Instance.D365_GR_ISH(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO);

                if (results.Count > 0)
                {
                    chkISHRow0 = false;

                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (results[i].TOTALRECORD != null)
                            P_TOTALRECORD = results[i].TOTALRECORD;
                        else
                            P_TOTALRECORD = null;
                       
                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, P_ACTION, P_RESENT, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);

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
                    "D365_GR_ISH Row = 0".Info();
                    chkISHRow0 = true;
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

        #region D365_GR_ISL
        private bool D365_GR_ISL(long? HEADERID, string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_ISLData> results = new List<ListD365_GR_ISLData>();

                results = D365DataService.Instance.D365_GR_ISL(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO,P_LOOMNO);

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

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, P_ACTION, P_RESENT, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

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
                    "D365_GR_ISL Row = 0".Info();
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

        #region D365_GR_OPH
        private bool D365_GR_OPH(long? PRODID, string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<D365_GR_OPHData> results = new List<D365_GR_OPHData>();

                results = D365DataService.Instance.D365_GR_OPH(P_WEAVINGLOT);

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
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, P_ACTION, P_RESENT, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);

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
                    "D365_GR_OPH Row = 0".Info();
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

        #region D365_GR_OPL
        private bool D365_GR_OPL(long? HEADERID, string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_OPLData> results = new List<ListD365_GR_OPLData>();

                results = D365DataService.Instance.D365_GR_OPL(P_WEAVINGLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, P_ACTION, P_RESENT, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

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
                    "D365_GR_OPL Row = 0".Info();
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

        #region D365_GR_OUH
        private bool D365_GR_OUH(long? PRODID, string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<D365_GR_OUHData> results = new List<D365_GR_OUHData>();

                results = D365DataService.Instance.D365_GR_OUH(P_WEAVINGLOT);

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
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, P_ACTION, P_RESENT, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);

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
                    "D365_GR_OUH Row = 0".Info();
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

        #region D365_GR_OUL
        private bool D365_GR_OUL(long? HEADERID, string P_ACTION, decimal? P_RESENT)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_OULData> results = new List<ListD365_GR_OULData>();

                results = D365DataService.Instance.D365_GR_OUL(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO, P_LOOMNO);

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

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, P_ACTION, P_RESENT, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
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
                    "D365_GR_OUL Row = 0".Info();
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
