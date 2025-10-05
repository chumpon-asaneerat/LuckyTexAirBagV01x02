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
    /// Interaction logic for CheckingReportDataPage.xaml
    /// </summary>
    public partial class CheckingReportDataPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CheckingReportDataPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadCustomer();
            LoadItemGood();
            LoadResult();
        }

        #endregion

        #region Internal Variables

        private QualityAssuranceSession _session = new QualityAssuranceSession();
        
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

        #region cmdClear_Click
        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }
        #endregion

        #region cmdSearch_Click
        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }
        #endregion

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        #endregion

        #endregion

        #region gridCheckingData

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

        private void gridCheckingData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridCheckingData.ItemsSource != null)
                {
                    string StrColors = string.Empty;
                    string StrDrawingType = string.Empty;

                    var row_list = GetDataGridRows(gridCheckingData);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                           
                        }
                    }
                }
                else
                {
                    
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

        private void Print(string P_CUSID, string P_DATE, string P_LABITMCODE, string P_RESULT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "CheckingAirbag";

                ConmonReportService.Instance.P_CUSID = P_CUSID;
                ConmonReportService.Instance.P_DATE = P_DATE;
                ConmonReportService.Instance.P_LABITMCODE = P_LABITMCODE;
                ConmonReportService.Instance.P_RESULT = P_RESULT;

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

        private void Preview(string P_CUSID ,string P_DATE , string P_LABITMCODE,string P_RESULT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "CheckingAirbag";

                ConmonReportService.Instance.P_CUSID = P_CUSID;
                ConmonReportService.Instance.P_DATE = P_DATE;
                ConmonReportService.Instance.P_LABITMCODE = P_LABITMCODE;
                ConmonReportService.Instance.P_RESULT = P_RESULT;


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

        #region Load Combo

        #region LoadCustomer

        private void LoadCustomer()
        {
            try
            {
                List<CUS_GETLISTData> items = _session.GetCustomerList();

                this.cbCustomer.ItemsSource = items;
                this.cbCustomer.DisplayMemberPath = "CUSTOMERNAME";
                this.cbCustomer.SelectedValuePath = "CUSTOMERID";
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
                List<ITM_GETITEMCODELIST> items = _session.GetItemCodeData();

                this.cbItemGoods.ItemsSource = items;
                this.cbItemGoods.DisplayMemberPath = "ITM_CODE";
                this.cbItemGoods.SelectedValuePath = "ITM_CODE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadResult

        private void LoadResult()
        {
            if (cbResult.ItemsSource == null)
            {
                string[] str = new string[] { "All", "Success", "Fail" };

                cbResult.ItemsSource = str;
                cbResult.SelectedIndex = 0;
            }
        }

        #endregion

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            cbCustomer.SelectedValue = null;
            cbItemGoods.SelectedValue = null;
            cbResult.SelectedIndex = 0;
            dteCheckingDate.SelectedDate = DateTime.Now;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridCheckingData.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridCheckingData.SelectedItems.Clear();
            else
                this.gridCheckingData.SelectedItem = null;

            gridCheckingData.ItemsSource = null;

            cbCustomer.Focus();
        }

        #endregion

        #region Search
        private void Search()
        {
            try
            {
                string _customer = string.Empty;
                string _item = string.Empty;
                string _date = string.Empty;
                string _result = string.Empty;

                if (cbCustomer.SelectedValue != null)
                {
                    _customer = cbCustomer.SelectedValue.ToString();
                }
                if (cbItemGoods.SelectedValue != null)
                {
                    _item = cbItemGoods.SelectedValue.ToString();
                }

                if (dteCheckingDate.SelectedDate != null)
                {
                    _date = dteCheckingDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                }

                if (cbResult.SelectedValue != null)
                {
                    if (cbResult.SelectedValue.ToString() == "Success")
                    {
                        _result = "Y";
                    }
                    else if (cbResult.SelectedValue.ToString() == "Fail")
                    {
                        _result = "N";
                    }
                    else
                    {
                        _result = string.Empty;
                    }
                }

                List<QA_SEARCHCHECKINGDATA> lots = new List<QA_SEARCHCHECKINGDATA>();

                lots = QualityAssuranceService.Instance.QA_SEARCHCHECKINGDATA(_customer, _date, _item, _result);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridCheckingData.ItemsSource = lots;
                }
                else
                {
                    gridCheckingData.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region PrintReport
        private void PrintReport()
        {
            try
            {
                string _customer = string.Empty;
                string _item = string.Empty;
                string _date = string.Empty;
                string _result = string.Empty;

                ConmonReportService.Instance.P_CUSID = string.Empty;
                ConmonReportService.Instance.P_DATE = string.Empty;
                ConmonReportService.Instance.P_LABITMCODE = string.Empty;
                ConmonReportService.Instance.P_RESULT = string.Empty;

                if (cbCustomer.SelectedValue != null)
                {
                    _customer = cbCustomer.SelectedValue.ToString();
                }
                if (cbItemGoods.SelectedValue != null)
                {
                    _item = cbItemGoods.SelectedValue.ToString();
                }

                if (dteCheckingDate.SelectedDate != null)
                {
                    _date = dteCheckingDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                }

                if (cbResult.SelectedValue != null)
                {
                    if (cbResult.SelectedValue.ToString() == "Success")
                    {
                        _result = "Y";
                    }
                    else if (cbResult.SelectedValue.ToString() == "Fail")
                    {
                        _result = "N";
                    }
                    else
                    {
                        _result = string.Empty;
                    }
                }

                Preview(_customer, _date, _item, _result);
            }
            catch (Exception ex)
            {
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

