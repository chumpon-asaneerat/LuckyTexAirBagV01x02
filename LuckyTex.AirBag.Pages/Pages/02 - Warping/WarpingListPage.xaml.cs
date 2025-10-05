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
    /// Interaction logic for WarpingListPage.xaml
    /// </summary>
    public partial class WarpingListPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingListPage()
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

            if (!string.IsNullOrEmpty(txtWARPHEADNO.Text) || cbWarpingMC.SelectedValue != null || cbItemCode.SelectedValue != null
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
            if (!string.IsNullOrEmpty(txtWARPHEADNO.Text) || cbWarpingMC.SelectedValue != null || cbItemCode.SelectedValue != null
                || dteStartDate.SelectedDate != null || dteToDate.SelectedDate != null)
            {
                string P_WARPHEADNO = string.Empty;
                string P_WARPMC = string.Empty;
                string P_ITMPREPARE = string.Empty;
                string P_STARTDATE = string.Empty;
                string P_ENDDATE = string.Empty;

                if (!string.IsNullOrEmpty(txtWARPHEADNO.Text))
                    P_WARPHEADNO = txtWARPHEADNO.Text;

                if (cbWarpingMC.SelectedValue != null)
                    P_WARPMC = cbWarpingMC.SelectedValue.ToString();

                if (cbItemCode.SelectedValue != null)
                    P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                if (dteStartDate.SelectedDate != null)
                    P_STARTDATE = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (dteToDate.SelectedDate != null)
                    P_ENDDATE = dteToDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                PrintReport(P_WARPHEADNO, P_WARPMC, P_ITMPREPARE, P_STARTDATE, P_ENDDATE);
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

        private void PrintReport(string P_WARPHEADNO, string P_WARPMC, string P_ITMPREPARE, string P_STARTDATE, string P_ENDDATE)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WarpingList";
                ConmonReportService.Instance.WARPHEADNO = P_WARPHEADNO;
                ConmonReportService.Instance.P_MC = P_WARPMC;
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
            dteToDate.SelectedDate = null;
            dteToDate.Text = "";
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

        }

        #endregion

        #region Search
        private void Search()
        {
            string P_WARPHEADNO = string.Empty;
            string P_WARPMC = string.Empty;
            string P_ITMPREPARE = string.Empty;
            string P_STARTDATE = string.Empty;
            string P_ENDDATE = string.Empty;

            try
            {
                if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarping.SelectedItems.Clear();
                else
                    this.gridWarping.SelectedItem = null;

                gridWarping.ItemsSource = null;


                if (!string.IsNullOrEmpty(txtWARPHEADNO.Text))
                    P_WARPHEADNO = txtWARPHEADNO.Text;

                if (cbWarpingMC.SelectedValue != null)
                    P_WARPMC = cbWarpingMC.SelectedValue.ToString();

                if (cbItemCode.SelectedValue != null)
                    P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                if (dteStartDate.SelectedDate != null)
                    P_STARTDATE = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (dteToDate.SelectedDate != null)
                    P_ENDDATE = dteToDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                List<WARP_WARPLIST> results = new List<WARP_WARPLIST>();

                results = WarpingDataService.Instance.WARP_WARPLIST(P_WARPHEADNO, P_WARPMC, P_ITMPREPARE, P_STARTDATE, P_ENDDATE);

                if (results.Count > 0)
                {
                    List<LuckyTex.Models.WARP_WARPLIST> dataList = new List<LuckyTex.Models.WARP_WARPLIST>();
                    int i = 0;
                    decimal leng = 0;

                    foreach (var row in results)
                    {
                        LuckyTex.Models.WARP_WARPLIST dataItemNew = new LuckyTex.Models.WARP_WARPLIST();

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
                        dataItemNew.EDITDATE = results[i].EDITDATE;
                        dataItemNew.EDITBY = results[i].EDITBY;
                        dataItemNew.ITM_PREPARE = results[i].ITM_PREPARE;
                        dataItemNew.ITM_YARN = results[i].ITM_YARN;

                        if (dataItemNew.LENGTH != null)
                            leng += dataItemNew.LENGTH.Value;

                        dataList.Add(dataItemNew);
                        i++;
                    }

                    this.gridWarping.ItemsSource = dataList;

                    txtTotalLeng.Text = leng.ToString("#,##0.##");
                }
                else
                {
                    // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                    if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridWarping.SelectedItems.Clear();
                    else
                        this.gridWarping.SelectedItem = null;

                    gridWarping.ItemsSource = null;

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
