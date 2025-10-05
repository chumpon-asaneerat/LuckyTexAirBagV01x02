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
    /// Interaction logic for BeamingRecordPage.xaml
    /// </summary>
    public partial class BeamingRecordPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingRecordPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private BeamingSession _session = new BeamingSession();
        string opera = string.Empty;
        string gidBEAMERNO = string.Empty;
        string gidITMPREPARE = string.Empty;

        string gidMCNO = string.Empty;
        decimal? gidTOTALYARN = null;
        decimal? gidTOTALKEBA = null;
        decimal? gidADJUSTKEBA = null;
        DateTime? gidSTARTDATE = null;
        DateTime? gidENDDATE = null;
        string gidREMARK = string.Empty;

        string gridBEAMNO = string.Empty;
        string BEAMLOT = string.Empty;

        string BEAMNO = string.Empty;
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

        #region cmdBeamingRecord_Click

        private void cmdBeamingRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gidBEAMERNO) && !string.IsNullOrEmpty(gidITMPREPARE))
            {
                PreviewBeamingRecord(gidBEAMERNO, gidITMPREPARE, gidMCNO, gidTOTALYARN, gidTOTALKEBA, gidADJUSTKEBA, gidSTARTDATE, gidENDDATE, gidREMARK);
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
            if (!string.IsNullOrEmpty(BEAMLOT))
            {
                PreviewBeam_tranferslip(BEAMLOT);
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
            if (!string.IsNullOrEmpty(BEAMNO))
            {
                if (D365_BM_BPO() == true)
                {
                    if (PRODID != null)
                    {
                        if (D365_BM_ISH(PRODID) == true)
                        {
                            if (HEADERID != null)
                            {
                                if (D365_BM_ISL(HEADERID) == true)
                                {
                                    if (D365_BM_OPH(PRODID) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_BM_OPL(HEADERID) == true)
                                            {
                                                if (D365_BM_OUH(PRODID) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_BM_OUL(HEADERID) == true)
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
                "Beamer Lot is null".ShowMessageBox();
            }
        }
        #endregion

        #endregion

        #region gridBeaming_SelectedCellsChanged

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

        private void gridBeaming_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridBeaming.ItemsSource != null)
                {
                    gidBEAMERNO = string.Empty;
                    gidITMPREPARE = string.Empty;
                    gidMCNO = string.Empty;
                    gidTOTALYARN = null;
                    gidTOTALKEBA = null;
                    gidADJUSTKEBA = null;
                    gidSTARTDATE = null;
                    gidENDDATE = null;
                    gidREMARK = string.Empty;

                    gridBEAMNO = string.Empty;
                    BEAMLOT = string.Empty;

                    BEAMNO = string.Empty;
                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;

                    var row_list = GetDataGridRows(gridBeaming);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            #region gidBEAMERNO

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).BEAMERNO != null)
                            {
                                gidBEAMERNO = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).BEAMERNO;

                                if (!string.IsNullOrEmpty(gidBEAMERNO))
                                {
                                    BEAMNO = gidBEAMERNO;
                                    GetBeamLot(gidBEAMERNO);
                                }
                                else
                                {
                                    gidBEAMERNO = null;
                                    BEAMNO = null;
                                }
                            }
                            else
                            {
                                gidBEAMERNO = null;
                                BEAMNO = null;
                            }

                            #endregion

                            #region gidITMPREPARE

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).ITM_PREPARE != null)
                            {
                                gidITMPREPARE = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).ITM_PREPARE;
                            }

                            #endregion

                            #region gidMCNO

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).MCNO != null)
                            {
                                gidMCNO = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).MCNO;
                            }

                            #endregion

                            #region gidTOTALYARN

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).TOTALYARN != null)
                            {
                                gidTOTALYARN = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).TOTALYARN;
                            }

                            #endregion

                            #region gidTOTALKEBA

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).TOTALKEBA != null)
                            {
                                gidTOTALKEBA = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).TOTALKEBA;
                            }

                            #endregion

                            #region gidADJUSTKEBA

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).ADJUSTKEBA != null)
                            {
                                gidADJUSTKEBA = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).ADJUSTKEBA;
                            }

                            #endregion

                            #region gidSTARTDATE

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).STARTDATE != null)
                            {
                                gidSTARTDATE = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).STARTDATE;
                            }

                            #endregion

                            #region gidENDDATE

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).ENDDATE != null)
                            {
                                gidENDDATE = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).ENDDATE;
                            }

                            #endregion

                            #region gidREMARK

                            if (((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).REMARK != null)
                            {
                                gidREMARK = ((LuckyTex.Models.BEAM_SEARCHBEAMRECORD)(gridBeaming.CurrentCell.Item)).REMARK;
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    gidBEAMERNO = string.Empty;
                    gidITMPREPARE = string.Empty;
                    gidMCNO = string.Empty;
                    gidTOTALYARN = null;
                    gidTOTALKEBA = null;
                    gidADJUSTKEBA = null;
                    gidSTARTDATE = null;
                    gidENDDATE = null;
                    gidREMARK = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region gridBeamer_SelectedCellsChanged

        private void gridBeamer_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridBeamer.ItemsSource != null)
                {
                    gridBEAMNO = string.Empty;
                    BEAMLOT = string.Empty;

                    var row_list = GetDataGridRows(gridBeamer);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMLOT != null)
                            {
                                BEAMLOT = ((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMLOT;
                            }
                            else
                            {
                                BEAMLOT = string.Empty;
                            }

                            if (((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMNO != null)
                            {
                                gridBEAMNO = ((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMNO;
                            }
                            else
                            {
                                gridBEAMNO = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    BEAMLOT = string.Empty;
                    gridBEAMNO = string.Empty;
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

        #region PreviewBeam_tranferslip

        private void PreviewBeam_tranferslip(string BEAMLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "BeamTransferSlip";
                ConmonReportService.Instance.BEAMLOT = BEAMLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region PreviewBeamingRecord

        private void PreviewBeamingRecord(string P_BEAMERNO, string P_ITMPREPARE, string P_MCNO, decimal? P_TOTALYARN, decimal? P_TOTALKEBA, decimal? P_ADJUSTKEBA
                , DateTime? P_STARTDATE, DateTime? P_ENDDATE, string P_REMARK)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "BeamingRecord";
                ConmonReportService.Instance.BEAMERNO = P_BEAMERNO;
                ConmonReportService.Instance.ITMPREPARE = P_ITMPREPARE;

                ConmonReportService.Instance.MCNO = P_MCNO;
                ConmonReportService.Instance.TOTALYARN = P_TOTALYARN;
                ConmonReportService.Instance.TOTALKEBA = P_TOTALKEBA;
                ConmonReportService.Instance.ADJUSTKEBA = P_ADJUSTKEBA;

                ConmonReportService.Instance.STARTDATE = P_STARTDATE;
                ConmonReportService.Instance.ENDDATE = P_ENDDATE;
                ConmonReportService.Instance.REMARK = P_REMARK;

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
                List<BeamingMCItem> items = _session.GetBeamingMCData();

                this.cbBeamingMC.ItemsSource = items;
                this.cbBeamingMC.DisplayMemberPath = "DisplayName";
                this.cbBeamingMC.SelectedValuePath = "MCId";
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
            cbBeamingMC.Text = "";
            cbBeamingMC.SelectedItem = null;

            txtBEAMERNO.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridBeaming.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridBeaming.SelectedItems.Clear();
            else
                this.gridBeaming.SelectedItem = null;

            gridBeaming.ItemsSource = null;

            if (this.gridBeamer.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridBeamer.SelectedItems.Clear();
            else
                this.gridBeamer.SelectedItem = null;

            gridBeamer.ItemsSource = null;

            gidBEAMERNO = string.Empty;
            gidITMPREPARE = string.Empty;
            gidMCNO = string.Empty;
            gidTOTALYARN = null;
            gidTOTALKEBA = null;
            gidADJUSTKEBA = null;
            gidSTARTDATE = null;
            gidENDDATE = null;
            gidREMARK = string.Empty;

            gridBEAMNO = string.Empty;
            BEAMLOT = string.Empty;

            BEAMNO = string.Empty;
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
            string P_BEAMERNO = string.Empty;
            string P_MC = string.Empty;
            string P_ITMPREPARE = string.Empty;
            string P_STARTDATE = string.Empty;

            try
            {
                if (this.gridBeaming.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridBeaming.SelectedItems.Clear();
                else
                    this.gridBeaming.SelectedItem = null;

                gridBeaming.ItemsSource = null;

                if (this.gridBeamer.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridBeamer.SelectedItems.Clear();
                else
                    this.gridBeamer.SelectedItem = null;

                gridBeamer.ItemsSource = null;

                gidBEAMERNO = string.Empty;
                gidITMPREPARE = string.Empty;
                gidMCNO = string.Empty;
                gidTOTALYARN = null;
                gidTOTALKEBA = null;
                gidADJUSTKEBA = null;
                gidSTARTDATE = null;
                gidENDDATE = null;
                gidREMARK = string.Empty;

                gridBEAMNO = string.Empty;
                BEAMLOT = string.Empty;

                BEAMNO = string.Empty;
                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;

                if (!string.IsNullOrEmpty(txtBEAMERNO.Text))
                    P_BEAMERNO = "%" + txtBEAMERNO.Text + "%";
                else
                    P_BEAMERNO = "%";

                if (cbBeamingMC.SelectedValue != null)
                    P_MC = cbBeamingMC.SelectedValue.ToString();

                if (cbItemCode.SelectedValue != null)
                    P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                if (dteStartDate.SelectedDate != null)
                    P_STARTDATE = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                List<BEAM_SEARCHBEAMRECORD> results = new List<BEAM_SEARCHBEAMRECORD>();

                results = BeamingDataService.Instance.BEAM_SEARCHBEAMRECORD(P_BEAMERNO, P_MC, P_ITMPREPARE, P_STARTDATE);

                if (results.Count > 0)
                {
                    List<LuckyTex.Models.BEAM_SEARCHBEAMRECORD> dataList = new List<LuckyTex.Models.BEAM_SEARCHBEAMRECORD>();
                    int i = 0;

                    foreach (var row in results)
                    {
                        LuckyTex.Models.BEAM_SEARCHBEAMRECORD dataItemNew = new LuckyTex.Models.BEAM_SEARCHBEAMRECORD();

                        dataItemNew.BEAMERNO = results[i].BEAMERNO;
                        dataItemNew.ITM_PREPARE = results[i].ITM_PREPARE;
                        dataItemNew.TOTALYARN = results[i].TOTALYARN;
                        dataItemNew.TOTALKEBA = results[i].TOTALKEBA;
                        dataItemNew.STARTDATE = results[i].STARTDATE;
                        dataItemNew.ENDDATE = results[i].ENDDATE;
                        dataItemNew.CREATEBY = results[i].CREATEBY;
                        dataItemNew.CREATEDATE = results[i].CREATEDATE;
                        dataItemNew.STATUS = results[i].STATUS;
                        dataItemNew.MCNO = results[i].MCNO;
                        dataItemNew.FINISHFLAG = results[i].FINISHFLAG;
                        dataItemNew.WARPHEADNO = results[i].WARPHEADNO;
                        dataItemNew.PRODUCTTYPEID = results[i].PRODUCTTYPEID;
                        dataItemNew.ADJUSTKEBA = results[i].ADJUSTKEBA;
                        dataItemNew.REMARK = results[i].REMARK;

                        dataList.Add(dataItemNew);
                        i++;
                    }

                    this.gridBeaming.ItemsSource = dataList;
                }
                else
                {
                    // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                    if (this.gridBeaming.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridBeaming.SelectedItems.Clear();
                    else
                        this.gridBeaming.SelectedItem = null;

                    gridBeaming.ItemsSource = null;

                    if (this.gridBeamer.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridBeamer.SelectedItems.Clear();
                    else
                        this.gridBeamer.SelectedItem = null;

                    gridBeamer.ItemsSource = null;
                }

            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion

        #region GetBeamLot

        private void GetBeamLot(string P_BEAMERNO)
        {
            List<BEAM_GETBEAMLOTBYBEAMERNO> results = new List<BEAM_GETBEAMLOTBYBEAMERNO>();

            results = BeamingDataService.Instance.BEAM_GETBEAMLOTBYBEAMERNO(P_BEAMERNO);

            if (results.Count > 0)
            {
                List<LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO> dataList = new List<LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO dataItemNew = new LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO();

                    dataItemNew.BEAMERNO = results[i].BEAMERNO;
                    dataItemNew.BEAMLOT = results[i].BEAMLOT;
                    dataItemNew.BEAMNO = results[i].BEAMNO;

                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.ENDDATE = results[i].ENDDATE;
                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.SPEED = results[i].SPEED;
                    dataItemNew.HARDNESS_L = results[i].HARDNESS_L;
                    dataItemNew.HARDNESS_N = results[i].HARDNESS_N;
                    dataItemNew.HARDNESS_R = results[i].HARDNESS_R;
                    dataItemNew.BEAMSTANDTENSION = results[i].BEAMSTANDTENSION;

                    dataItemNew.WINDINGTENSION = results[i].WINDINGTENSION;
                    dataItemNew.INSIDE_WIDTH = results[i].INSIDE_WIDTH;
                    dataItemNew.OUTSIDE_WIDTH = results[i].OUTSIDE_WIDTH;
                    dataItemNew.FULL_WIDTH = results[i].FULL_WIDTH;
                    dataItemNew.STARTBY = results[i].STARTBY;
                    dataItemNew.DOFFBY = results[i].DOFFBY;
                    dataItemNew.FLAG = results[i].FLAG;
                    dataItemNew.BEAMMC = results[i].BEAMMC;

                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.REMARK = results[i].REMARK;
                    dataItemNew.TENSION_ST1 = results[i].TENSION_ST1;
                    dataItemNew.TENSION_ST2 = results[i].TENSION_ST2;
                    dataItemNew.TENSION_ST3 = results[i].TENSION_ST3;
                    dataItemNew.TENSION_ST4 = results[i].TENSION_ST4;
                    dataItemNew.TENSION_ST5 = results[i].TENSION_ST5;
                    dataItemNew.TENSION_ST6 = results[i].TENSION_ST6;
                    dataItemNew.TENSION_ST7 = results[i].TENSION_ST7;
                    dataItemNew.TENSION_ST8 = results[i].TENSION_ST8;
                    dataItemNew.TENSION_ST9 = results[i].TENSION_ST9;
                    dataItemNew.TENSION_ST10 = results[i].TENSION_ST10;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridBeamer.ItemsSource = dataList;

            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridBeamer.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridBeamer.SelectedItems.Clear();
                else
                    this.gridBeamer.SelectedItem = null;

                gridBeamer.ItemsSource = null;
            }
        }

        #endregion

        #region D365_BM_BPO
        private bool D365_BM_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_BPOData> results = new List<ListD365_BM_BPOData>();

                results = D365DataService.Instance.D365_BM_BPO(BEAMNO);

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
                    "D365_BM_BPO Row = 0".Info();
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

        #region D365_BM_ISH
        private bool D365_BM_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_BM_ISHData> results = new List<D365_BM_ISHData>();

                results = D365DataService.Instance.D365_BM_ISH(BEAMNO);

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
                    "D365_BM_ISH Row = 0".Info();
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

        #region D365_BM_ISL
        private bool D365_BM_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_ISLData> results = new List<ListD365_BM_ISLData>();

                results = D365DataService.Instance.D365_BM_ISL(BEAMNO);

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
                    "D365_BM_ISL Row = 0".Info();
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

        #region D365_BM_OPH
        private bool D365_BM_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_BM_OPHData> results = new List<D365_BM_OPHData>();

                results = D365DataService.Instance.D365_BM_OPH(BEAMNO);

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
                    "D365_BM_OPH Row = 0".Info();
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

        #region D365_BM_OPL
        private bool D365_BM_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_OPLData> results = new List<ListD365_BM_OPLData>();

                results = D365DataService.Instance.D365_BM_OPL(BEAMNO);

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
                    "D365_BM_OPL Row = 0".Info();
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

        #region D365_BM_OUH
        private bool D365_BM_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_BM_OUHData> results = new List<D365_BM_OUHData>();

                results = D365DataService.Instance.D365_BM_OUH(BEAMNO);

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
                    "D365_BM_OUH Row = 0".Info();
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

        #region D365_BM_OUL
        private bool D365_BM_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_OULData> results = new List<ListD365_BM_OULData>();

                results = D365DataService.Instance.D365_BM_OUL(BEAMNO);

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
                    "D365_BM_OUL Row = 0".Info();
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
