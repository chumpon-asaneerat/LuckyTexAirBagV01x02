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
    /// Interaction logic for BeamingListPage.xaml
    /// </summary>
    public partial class BeamingListPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingListPage()
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
            if (!string.IsNullOrEmpty(txtBEAMERNO.Text) || cbBeamingMC.SelectedValue != null || cbItemCode.SelectedValue != null
                || dteStartDate.SelectedDate != null || dteToDate.SelectedDate != null)
            {
                Search();
            }
            else
            {
                "Please select data".ShowMessageBox(false);
            }
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdPrintReport_Click

        private void cmdPrintReport_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBEAMERNO.Text) || cbBeamingMC.SelectedValue != null || cbItemCode.SelectedValue != null
                || dteStartDate.SelectedDate != null || dteToDate.SelectedDate != null)
            {
                string P_BEAMERNO = string.Empty;
                string P_BEAMMC = string.Empty;
                string P_ITMPREPARE = string.Empty;
                string P_STARTDATE = string.Empty;
                string P_ENDDATE = string.Empty;

                if (!string.IsNullOrEmpty(txtBEAMERNO.Text))
                    P_BEAMERNO = txtBEAMERNO.Text;

                if (cbBeamingMC.SelectedValue != null)
                    P_BEAMMC = cbBeamingMC.SelectedValue.ToString();

                if (cbItemCode.SelectedValue != null)
                    P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                if (dteStartDate.SelectedDate != null)
                    P_STARTDATE = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (dteToDate.SelectedDate != null)
                    P_ENDDATE = dteToDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                PrintReport(P_BEAMERNO, P_BEAMMC, P_ITMPREPARE, P_STARTDATE, P_ENDDATE);
            }
            else
            {
                "Please select data".ShowMessageBox();
            }
        }

        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region PrintReport

        private void PrintReport(string P_BEAMERNO, string P_BEAMMC, string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "BeamingList";
                ConmonReportService.Instance.BEAMERNO = P_BEAMERNO;
                ConmonReportService.Instance.P_MC = P_BEAMMC;
                ConmonReportService.Instance.ITMPREPARE = P_ITMPREPARE;
                ConmonReportService.Instance.P_DATE = P_STARTDATE;
                ConmonReportService.Instance.P_ENDDATE = P_ENDDATE;

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
            dteToDate.SelectedDate = null;
            dteToDate.Text = "";
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

        }

        #endregion

        #region Search
        private void Search()
        {
            string P_BEAMERNO = string.Empty;
            string P_MC = string.Empty;
            string P_ITMPREPARE = string.Empty;
            string P_STARTDATE = string.Empty;
            string P_ENDDATE = string.Empty;

            try
            {
                if (this.gridBeaming.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridBeaming.SelectedItems.Clear();
                else
                    this.gridBeaming.SelectedItem = null;

                gridBeaming.ItemsSource = null;


                if (!string.IsNullOrEmpty(txtBEAMERNO.Text))
                    P_BEAMERNO = txtBEAMERNO.Text;

                if (cbBeamingMC.SelectedValue != null)
                    P_MC = cbBeamingMC.SelectedValue.ToString();

                if (cbItemCode.SelectedValue != null)
                    P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                if (dteStartDate.SelectedDate != null)
                    P_STARTDATE = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (dteToDate.SelectedDate != null)
                    P_ENDDATE = dteToDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                List<BEAM_BEAMLIST> results = new List<BEAM_BEAMLIST>();

                results = BeamingDataService.Instance.BEAM_BEAMLIST(P_BEAMERNO, P_MC, P_ITMPREPARE, P_STARTDATE, P_ENDDATE);

                if (results.Count > 0)
                {
                    List<LuckyTex.Models.BEAM_BEAMLIST> dataList = new List<LuckyTex.Models.BEAM_BEAMLIST>();
                    int i = 0;
                    decimal leng = 0;

                    foreach (var row in results)
                    {
                        LuckyTex.Models.BEAM_BEAMLIST dataItemNew = new LuckyTex.Models.BEAM_BEAMLIST();

                        dataItemNew.BEAMERNO = results[i].BEAMERNO;
                        dataItemNew.BEAMLOT = results[i].BEAMLOT;
                        dataItemNew.BEAMNO = results[i].BEAMNO;
                        dataItemNew.STARTDATE = results[i].STARTDATE;
                        dataItemNew.ENDDATE = results[i].ENDDATE;
                        dataItemNew.LENGTH = results[i].LENGTH;
                        dataItemNew.SPEED = results[i].SPEED;
                        dataItemNew.BEAMSTANDTENSION = results[i].BEAMSTANDTENSION;
                        dataItemNew.WINDINGTENSION = results[i].WINDINGTENSION;
                        dataItemNew.HARDNESS_L = results[i].HARDNESS_L;
                        dataItemNew.HARDNESS_N = results[i].HARDNESS_N;
                        dataItemNew.HARDNESS_R = results[i].HARDNESS_R;
                        dataItemNew.INSIDE_WIDTH = results[i].INSIDE_WIDTH;
                        dataItemNew.OUTSIDE_WIDTH = results[i].OUTSIDE_WIDTH;
                        dataItemNew.FULL_WIDTH = results[i].FULL_WIDTH;
                        dataItemNew.STARTBY = results[i].STARTBY;
                        dataItemNew.DOFFBY = results[i].DOFFBY;
                        dataItemNew.BEAMMC = results[i].BEAMMC;
                        dataItemNew.FLAG = results[i].FLAG;
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
                        dataItemNew.EDITBY = results[i].EDITBY;
                        dataItemNew.OLDBEAMNO = results[i].OLDBEAMNO;
                        dataItemNew.EDITDATE = results[i].EDITDATE;
                        dataItemNew.ITM_PREPARE = results[i].ITM_PREPARE;
                        dataItemNew.WARPHEADNO = results[i].WARPHEADNO;
                        dataItemNew.TOTALYARN = results[i].TOTALYARN;
                        dataItemNew.TOTALKEBA = results[i].TOTALKEBA;

                        if (dataItemNew.LENGTH != null)
                            leng += dataItemNew.LENGTH.Value;

                        dataList.Add(dataItemNew);
                        i++;
                    }

                    this.gridBeaming.ItemsSource = dataList;

                    txtTotalLeng.Text = leng.ToString("#,##0.##");
                }
                else
                {
                    // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                    if (this.gridBeaming.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridBeaming.SelectedItems.Clear();
                    else
                        this.gridBeaming.SelectedItem = null;

                    gridBeaming.ItemsSource = null;

                    txtTotalLeng.Text = "0";

                    "No Data Found".ShowMessageBox(false);
                }

            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
