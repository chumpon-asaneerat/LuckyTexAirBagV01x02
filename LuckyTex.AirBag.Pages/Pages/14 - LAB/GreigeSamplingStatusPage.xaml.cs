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
    /// Interaction logic for GreigeSamplingStatusPage.xaml
    /// </summary>
    public partial class GreigeSamplingStatusPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GreigeSamplingStatusPage()
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

        string gridLOOMNO = string.Empty;
        string gridBEAMERROLL = string.Empty;
        decimal? gridCONDITIONINGTIME = null;
        decimal? TESTNO = null;
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
            string BEAMERROLL = string.Empty;

            if (!string.IsNullOrEmpty(txtBEAMERROLL.Text))
                BEAMERROLL = txtBEAMERROLL.Text;

            if (dteReceiveDate.SelectedDate != null)
                ReceiveDate = dteReceiveDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            LAB_GREIGESTOCKSTATUS(BEAMERROLL, null, ReceiveDate);

        }

        #endregion

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Status))
                {
                    if (Status == "Conditioning")
                    {
                        if (LABDataService.Instance.LAB_GreigeSamplingStatus(gridBEAMERROLL, gridLOOMNO, TESTNO, receiveDate, "T", txtOperator.Text) == true)
                        {
                            string msg = "Start Testing on " + gridBEAMERROLL;
                            msg.ShowMessageBox();

                            string BEAMERROLL = string.Empty;
                            string ReceiveDate = string.Empty;
                           
                            if (!string.IsNullOrEmpty(txtBEAMERROLL.Text))
                                BEAMERROLL = txtBEAMERROLL.Text;

                            if (dteReceiveDate.SelectedDate != null)
                                ReceiveDate = dteReceiveDate.SelectedDate.Value.ToString("dd/MM/yyyy");

                            LAB_GREIGESTOCKSTATUS(BEAMERROLL, null, ReceiveDate);
                        }
                        else
                        {
                            "Cannot Save".ShowMessageBox();
                        }
                    }
                    else if (Status == "Sending")
                    {
                        "Cannot Print , Sampling has not received in LAB".ShowMessageBox();
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
                           
                            gridBEAMERROLL = string.Empty;
                            gridLOOMNO = string.Empty;
                            receiveDate = null;
                            Status = string.Empty;
                            TESTNO = null;
                            
                            if (((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).BEAMERROLL != null)
                            {
                                gridBEAMERROLL = ((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).BEAMERROLL;
                            }
                            if (((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).LOOMNO != null)
                            {
                                gridLOOMNO = ((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).LOOMNO;
                            }
                            if (((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).RECEIVEDATE != null)
                            {
                                receiveDate = ((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).RECEIVEDATE;
                            }

                            if (((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).CONDITIONINGTIME != null)
                            {
                                gridCONDITIONINGTIME = ((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).CONDITIONINGTIME;
                            }
                            if (((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).STATUS != null)
                            {
                                Status = ((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).STATUS;
                            }

                            if (((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).TESTNO != null)
                            {
                                TESTNO = ((LuckyTex.Models.LAB_GREIGESTOCKSTATUS)(gridMASSPROSTOCKSTATUS.CurrentCell.Item)).TESTNO;
                            }
                        }
                    }
                }
                else
                {
                    gridBEAMERROLL = string.Empty;
                    gridLOOMNO = string.Empty;
                    receiveDate = null;
                    gridCONDITIONINGTIME = null;
                    Status = string.Empty;
                    TESTNO = null;
                            
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                gridBEAMERROLL = string.Empty;
                gridLOOMNO = string.Empty;
                receiveDate = null;
                gridCONDITIONINGTIME = null;
                Status = string.Empty;
                TESTNO = null;
                            
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
            txtBEAMERROLL.Text = "";
            dteReceiveDate.SelectedDate = null;
            dteReceiveDate.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridMASSPROSTOCKSTATUS.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridMASSPROSTOCKSTATUS.SelectedItems.Clear();
            else
                this.gridMASSPROSTOCKSTATUS.SelectedItem = null;

            gridMASSPROSTOCKSTATUS.ItemsSource = null;


            gridBEAMERROLL = string.Empty;
            gridLOOMNO = string.Empty;
            receiveDate = null;
            gridCONDITIONINGTIME = null;                     
            Status = string.Empty;
            TESTNO = null; 

            txtBEAMERROLL.SelectAll();
            txtBEAMERROLL.Focus();
        }

        #endregion

        #region LAB_GREIGESTOCKSTATUS

        private void LAB_GREIGESTOCKSTATUS(string P_WEAVELOT, string P_LOOMNO, string P_RECEIVEDATE)
        {
            List<LAB_GREIGESTOCKSTATUS> lots = new List<LAB_GREIGESTOCKSTATUS>();

            lots = LABDataService.Instance.LAB_GREIGESTOCKSTATUS(P_WEAVELOT, P_LOOMNO, P_RECEIVEDATE);

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

