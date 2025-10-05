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
    /// Interaction logic for SamplingStatusPage.xaml
    /// </summary>
    public partial class SamplingStatusPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SamplingStatusPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private LABSession _session = new LABSession();

        string ITM_CODE = string.Empty;
        string WEAVINGLOT = string.Empty;
        string FINISHINGLOT = string.Empty;
        string LABFORM = string.Empty;
        string Status = string.Empty;
        DateTime? receiveDate = null;
        string opera = string.Empty;
       
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

            string ReceiveDate = string.Empty;
            string WEAVLOT = string.Empty;

            if (!string.IsNullOrEmpty(txtWEAVLOT.Text))
                WEAVLOT = txtWEAVLOT.Text;

            if (dteReceiveDate.SelectedDate != null)
                ReceiveDate = dteReceiveDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            //ReceiveDate = dteReceiveDate.SelectedDate.Value.ToString("dd/MM/yyyy")+" "+DateTime.Now.ToString("HH:mm");

            //if (!string.IsNullOrEmpty(WEAVLOT) && !string.IsNullOrEmpty(ReceiveDate))

            LAB_MASSPROSTOCKSTATUS(WEAVLOT, ReceiveDate);
        }

        #endregion

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Status))
                {
                    if (Status == "Conditioning")
                    {
                        LABDataService.Instance.LAB_SamplingStatus(WEAVINGLOT, FINISHINGLOT, ITM_CODE, DateTime.Now, "T", txtOperator.Text);
                    }

                    if (Status == "Sending")
                    {
                        "Cannot Print , Sampling has not received in LAB".ShowMessageBox();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(LABFORM))
                        {
                            string WEAVLOT = string.Empty;
                            string ReceiveDate = string.Empty;

                            if (LABFORM == "FM-QC-18")
                            {
                                Print18(WEAVINGLOT);

                                if (!string.IsNullOrEmpty(txtWEAVLOT.Text))
                                    WEAVLOT = txtWEAVLOT.Text;

                                if (dteReceiveDate.SelectedDate != null)
                                    ReceiveDate = dteReceiveDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                                LAB_MASSPROSTOCKSTATUS(WEAVLOT, ReceiveDate);
                            }
                            else if (LABFORM == "FM-QC-19")
                            {
                                Print19(WEAVINGLOT);

                                if (!string.IsNullOrEmpty(txtWEAVLOT.Text))
                                    WEAVLOT = txtWEAVLOT.Text;

                                if (dteReceiveDate.SelectedDate != null)
                                    ReceiveDate = dteReceiveDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                                LAB_MASSPROSTOCKSTATUS(WEAVLOT, ReceiveDate);
                            }
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region gridMASSPROSTOCKSTATUS_SelectedCellsChanged

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

        private void gridMASSPROSTOCKSTATUS_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridMASSPROSTOCKSTATUS.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridMASSPROSTOCKSTATUS);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            ITM_CODE = string.Empty;
                            WEAVINGLOT = string.Empty;

                            receiveDate = null;
                            Status = string.Empty;
                            LABFORM = string.Empty;

                            if (((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).ITM_CODE != null)
                            {
                                ITM_CODE = ((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).ITM_CODE;
                            }

                            if (((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                WEAVINGLOT = ((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).WEAVINGLOT;
                            }

                            if (((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).FINISHINGLOT != null)
                            {
                                FINISHINGLOT = ((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).FINISHINGLOT;
                            }

                            if (((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).LABFORM != null)
                            {
                                LABFORM = ((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).LABFORM;
                            }

                            if (((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).RECEIVEDATE != null)
                            {
                                receiveDate = ((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).RECEIVEDATE;
                            }
                            if (((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).STATUS != null)
                            {
                                Status = ((LuckyTex.Models.LAB_MASSPROSTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).STATUS;
                            }
                        }
                    }
                }
                else
                {
                    ITM_CODE = string.Empty;
                    WEAVINGLOT = string.Empty;
                    FINISHINGLOT = string.Empty;
                    receiveDate = null;
                    Status = string.Empty;
                    LABFORM = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                ITM_CODE = string.Empty;
                WEAVINGLOT = string.Empty;
                FINISHINGLOT = string.Empty;
                receiveDate = null;
                Status = string.Empty;
                LABFORM = string.Empty;
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print18(string WEAVINGLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "FM-QC-18-06";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = ITM_CODE;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Print19(string WEAVINGLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "EM-QC-19-06";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = ITM_CODE;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Preview

        private void Preview18(string WEAVINGLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "FM-QC-18-06";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = ITM_CODE;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Preview19(string WEAVINGLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "EM-QC-19-06";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = ITM_CODE;

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

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            txtWEAVLOT.Text = "";
            dteReceiveDate.SelectedDate = null;
            dteReceiveDate.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridMASSPROSTOCKSTATUS.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridMASSPROSTOCKSTATUS.SelectedItems.Clear();
            else
                this.gridMASSPROSTOCKSTATUS.SelectedItem = null;

            gridMASSPROSTOCKSTATUS.ItemsSource = null;

            ITM_CODE = string.Empty;
            WEAVINGLOT = string.Empty;
            FINISHINGLOT = string.Empty;
            LABFORM = string.Empty;
            receiveDate = null;
            Status = string.Empty;

            txtWEAVLOT.SelectAll();
            txtWEAVLOT.Focus();
        }

        #endregion

        #region LAB_MASSPROSTOCKSTATUS

        private void LAB_MASSPROSTOCKSTATUS(string P_WEAVELOT, string P_RECEIVEDATE)
        {
            List<LAB_MASSPROSTOCKSTATUS> lots = new List<LAB_MASSPROSTOCKSTATUS>();

            lots = LABDataService.Instance.LAB_MASSPROSTOCKSTATUS(P_WEAVELOT, P_RECEIVEDATE);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridMASSPROSTOCKSTATUS.ItemsSource = lots;
            }
            else
            {
                gridMASSPROSTOCKSTATUS.ItemsSource = null;
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

namespace CheckConditioningTime
{
    public class CheckConditioningTime : IValueConverter
    {
        #region Convert

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal? input = 0;

            try
            {
                if (value != null)
                {
                    string dt = value.ToString();
                    int day = int.Parse(dt.Split(':')[0]);
                    TimeSpan ts = TimeSpan.FromDays(day);

                    if (ts.Days != 0)
                        input = decimal.Parse(ts.Days.ToString());
                    else
                        input = 0;

                    //input = decimal.Parse(value.ToString());
                }
            }
            catch
            {
                input = 0;
            }

            if (input >= 1)
            {
                return Brushes.Red;
            }
            else
            {
                return DependencyProperty.UnsetValue;
            }
        }

        #endregion

        #region ConvertBack

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}

