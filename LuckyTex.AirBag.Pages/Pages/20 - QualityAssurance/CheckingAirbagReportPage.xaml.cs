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
    /// Interaction logic for CheckingAirbagReportPage.xaml
    /// </summary>
    public partial class CheckingAirbagReportPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CheckingAirbagReportPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            dteCheckingDate.IsEnabled = false;

            LoadShift();
            LoadCustomer();
        }

        #endregion

        #region Internal Variables

        private QualityAssuranceSession _session = new QualityAssuranceSession();
        
        string opera = string.Empty;
        string partNo = string.Empty; 

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

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintReport();
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (QA_InsertCheckingData("Y") == true)
            {
                txtItemCode.Text = string.Empty;
                txtLotNo.Text = string.Empty;
                txtBatchNo.Text = string.Empty;
                txtItemCodeIns.Text = string.Empty;
                txtLotNoIns.Text = string.Empty;
                txtBatchNoIns.Text = string.Empty;

                partNo = string.Empty;

                if (cbCustomer.SelectedValue != null)
                {
                    string _customer = cbCustomer.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(_customer))
                        SearchCheckingData(_customer);
                }

                cmdSave.IsEnabled = false;
                txtRemark.Text = string.Empty;
                cbShift.SelectedIndex = 0;

                txtItemCode.Focus();
                txtItemCode.SelectAll();
            }
            else
            {
                "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
            }
        }
        #endregion

        #endregion

        #region cbCustomer_SelectionChanged
        private void cbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCustomer.SelectedValue != null)
            {
                string _customer = cbCustomer.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(_customer))
                    SearchCheckingData(_customer);
            }
        }
        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtItemCode_KeyDown
        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (cbCustomer.SelectedValue != null)
                {
                    if (!string.IsNullOrEmpty(txtItemCode.Text))
                    {
                        //if (GetItemByCustomer(cbCustomer.SelectedValue.ToString(), txtItemCode.Text) == true)
                        //{
                            txtLotNo.Focus();
                            txtLotNo.SelectAll();
                            e.Handled = true;
                        //}
                        //else
                        //{
                        //    "This Item Code not for Selected Customer".ShowMessageBox();

                        //    txtItemCode.Text = string.Empty;
                        //    txtItemCode.Focus();
                        //    txtItemCode.SelectAll();
                        //    e.Handled = true;
                        //}
                    }
                    else
                    {
                        txtLotNo.Focus();
                        txtLotNo.SelectAll();
                        e.Handled = true;
                    }
                }
                else
                {
                    //"Please Select Customer First".ShowMessageBox();

                    txtItemCode.Text = string.Empty;
                    cbCustomer.Focus();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtItemCode_LostFocus
        private void txtItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbCustomer.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(txtItemCode.Text))
                {
                    if (GetItemByCustomer(cbCustomer.SelectedValue.ToString(), txtItemCode.Text) == true)
                    {
                        txtLotNo.Focus();
                        txtLotNo.SelectAll();
                        e.Handled = true;
                    }
                    else
                    {
                        "This Item Code not for Selected Customer".ShowMessageBox();

                        txtItemCode.Text = string.Empty;
                        txtItemCode.Focus();
                        txtItemCode.SelectAll();
                        e.Handled = true;
                    }
                }
            }
            else
            {
                "Please Select Customer First".ShowMessageBox();
            }
        }
        #endregion

        #region txtLotNo_KeyDown
        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBatchNo.Focus();
                txtBatchNo.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtBatchNo_KeyDown
        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtItemCodeIns.Focus();
                txtItemCodeIns.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtItemCodeIns_KeyDown
        private void txtItemCodeIns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtItemCode.Text))
                {
                    if (!string.IsNullOrEmpty(txtItemCodeIns.Text))
                    {
                        if (txtItemCode.Text != txtItemCodeIns.Text)
                        {
                            //txtItemCode.Text = string.Empty;
                            //txtLotNo.Text = string.Empty;
                            //txtBatchNo.Text = string.Empty;
                            //txtItemCodeIns.Text = string.Empty;
                            //txtLotNoIns.Text = string.Empty;
                            //txtBatchNoIns.Text = string.Empty;

                            //partNo = string.Empty;

                            txtItemCode.Focus();
                            txtItemCode.SelectAll();
                            e.Handled = true;
                        }
                        else
                        {
                            txtLotNoIns.Focus();
                            txtLotNoIns.SelectAll();
                            e.Handled = true;
                        }
                    }
                }
                else
                {
                    //"Please Select Item Code".ShowMessageBox();
                    txtItemCode.Focus();
                    txtItemCode.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtItemCodeIns_LostFocus
        private void txtItemCodeIns_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (!string.IsNullOrEmpty(txtItemCodeIns.Text))
                {
                    if (txtItemCode.Text != txtItemCodeIns.Text)
                    {
                        "Inspection Item Code not match with Testing result Item Code !!".ShowMessageBox();

                        if (QA_InsertCheckingData("C") == true)
                        {
                            txtItemCode.Text = string.Empty;
                            txtLotNo.Text = string.Empty;
                            txtBatchNo.Text = string.Empty;
                            txtItemCodeIns.Text = string.Empty;
                            txtLotNoIns.Text = string.Empty;
                            txtBatchNoIns.Text = string.Empty;

                            partNo = string.Empty;

                            txtItemCode.Focus();
                            txtItemCode.SelectAll();
                            e.Handled = true;
                        }
                        else
                        {
                            "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
                        }
                    }
                }
            }
            else
            {
                "Please Select Item Code".ShowMessageBox();
            }
        }
        #endregion

        #region txtLotNoIns_KeyDown
        private void txtLotNoIns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtLotNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtLotNoIns.Text))
                    {
                        if (CheckLotNo(txtLotNo.Text, txtLotNoIns.Text) == true)
                        {
                            txtBatchNoIns.Focus();
                            txtBatchNoIns.SelectAll();
                            e.Handled = true;
                        }
                        else
                        {
                            //"Inspection Lot not match with Testing result !!".ShowMessageBox();

                            //if (QA_InsertCheckingData("L") == true)
                            //{
                                //txtItemCode.Text = string.Empty;
                                //txtLotNo.Text = string.Empty;
                                //txtBatchNo.Text = string.Empty;
                                //txtItemCodeIns.Text = string.Empty;
                                //txtLotNoIns.Text = string.Empty;
                                //txtBatchNoIns.Text = string.Empty;

                                //partNo = string.Empty;
                               
                                txtItemCode.Focus();
                                txtItemCode.SelectAll();
                                e.Handled = true;
                            //}
                            //else
                            //{
                            //    "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
                            //}
                        }                        
                    }
                    else
                    {
                        txtBatchNoIns.Focus();
                        txtBatchNoIns.SelectAll();
                        e.Handled = true;
                    }
                }
                else
                {
                    //"Please Select Lot No.".ShowMessageBox();
                    txtLotNo.Focus();
                    txtLotNo.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtLotNoIns_LostFocus
        private void txtLotNoIns_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLotNo.Text))
            {
                if (!string.IsNullOrEmpty(txtLotNoIns.Text))
                {
                    if (CheckLotNo(txtLotNo.Text, txtLotNoIns.Text) == false)
                    {
                        "Inspection Lot not match with Testing result !!".ShowMessageBox();

                        if (QA_InsertCheckingData("L") == true)
                        {
                            txtItemCode.Text = string.Empty;
                            txtLotNo.Text = string.Empty;
                            txtBatchNo.Text = string.Empty;
                            txtItemCodeIns.Text = string.Empty;
                            txtLotNoIns.Text = string.Empty;
                            txtBatchNoIns.Text = string.Empty;

                            partNo = string.Empty;

                            txtItemCode.Focus();
                            txtItemCode.SelectAll();
                            e.Handled = true;
                        }
                        else
                        {
                            "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
                        }
                    }
                }
            }
            else
            {
                "Please Select Lot No.".ShowMessageBox();
            }
        }
        #endregion

        #region txtBatchNoIns_KeyDown
        private void txtBatchNoIns_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBatchNo.Text))
            {
                if (!string.IsNullOrEmpty(txtBatchNoIns.Text))
                {
                    //txtItemCode.Text = string.Empty;
                    //txtLotNo.Text = string.Empty;
                    //txtBatchNo.Text = string.Empty;
                    //txtItemCodeIns.Text = string.Empty;
                    //txtLotNoIns.Text = string.Empty;
                    //txtBatchNoIns.Text = string.Empty;

                    //partNo = string.Empty;

                    //if (cbCustomer.SelectedValue != null)
                    //{
                    //    string _customer = cbCustomer.SelectedValue.ToString();

                    //    if (!string.IsNullOrEmpty(_customer))
                    //        SearchCheckingData(_customer);
                    //}

                    txtRemark.Focus();
                    txtRemark.SelectAll();
                    e.Handled = true;

                    #region Old
                    //if (CheckBatchNo(txtBatchNo.Text, txtBatchNoIns.Text) == true)
                    //{
                    //    "Checking Complete !!".ShowMessageBox();

                    //    if (QA_InsertCheckingData("Y") == true)
                    //    {
                    //        txtItemCode.Text = string.Empty;
                    //        txtLotNo.Text = string.Empty;
                    //        txtBatchNo.Text = string.Empty;
                    //        txtItemCodeIns.Text = string.Empty;
                    //        txtLotNoIns.Text = string.Empty;
                    //        txtBatchNoIns.Text = string.Empty;

                    //        partNo = string.Empty;
                   
                    //        if (cbCustomer.SelectedValue != null)
                    //        {
                    //            string _customer = cbCustomer.SelectedValue.ToString();

                    //            if (!string.IsNullOrEmpty(_customer))
                    //                SearchCheckingData(_customer);
                    //        }

                    //        txtItemCode.Focus();
                    //        txtItemCode.SelectAll();
                    //        e.Handled = true;
                    //    }
                    //    else
                    //    {
                    //        "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
                    //    }
                    //}
                    //else
                    //{
                    //    "Inspection Batch No not match with Testing result !!".ShowMessageBox();

                    //    if (QA_InsertCheckingData("B") == true)
                    //    {
                    //        txtItemCode.Text = string.Empty;
                    //        txtLotNo.Text = string.Empty;
                    //        txtBatchNo.Text = string.Empty;
                    //        txtItemCodeIns.Text = string.Empty;
                    //        txtLotNoIns.Text = string.Empty;
                    //        txtBatchNoIns.Text = string.Empty;

                    //        partNo = string.Empty;
                   
                    //        txtItemCode.Focus();
                    //        txtItemCode.SelectAll();
                    //        e.Handled = true;
                    //    }
                    //    else
                    //    {
                    //        "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
                    //    }
                    //}
                    #endregion
                }
            }
            else
            {
                //"Please Select Batch No.".ShowMessageBox();
                txtBatchNo.Focus();
                txtBatchNo.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtBatchNoIns_LostFocus
        private void txtBatchNoIns_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBatchNo.Text))
            {
                if (!string.IsNullOrEmpty(txtBatchNoIns.Text))
                {
                    if (CheckBatchNo(txtBatchNo.Text, txtBatchNoIns.Text) == true)
                    {
                        cmdSave.IsEnabled = true;
                    }
                    else
                    {
                        "Inspection Batch No not match with Testing result !!".ShowMessageBox();

                        if (QA_InsertCheckingData("B") == true)
                        {
                            txtItemCode.Text = string.Empty;
                            txtLotNo.Text = string.Empty;
                            txtBatchNo.Text = string.Empty;
                            txtItemCodeIns.Text = string.Empty;
                            txtLotNoIns.Text = string.Empty;
                            txtBatchNoIns.Text = string.Empty;

                            partNo = string.Empty;

                            txtItemCode.Focus();
                            txtItemCode.SelectAll();
                            e.Handled = true;
                        }
                        else
                        {
                            "Can't Save QA_InsertCheckingData".ShowMessageBox(true);
                        }
                    }
                }
            }
            else
            {
                "Please Select Batch No.".ShowMessageBox();
            }
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

        private void Preview(string P_CUSID, string P_DATE, string P_LABITMCODE, string P_RESULT)
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

        #region LoadShift

        private void LoadShift()
        {
            if (cbShift.ItemsSource == null)
            {
                string[] str = new string[] { "A","B","C","D" };

                cbShift.ItemsSource = str;
                cbShift.SelectedIndex = 0;
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
            dteCheckingDate.SelectedDate = DateTime.Now;
            cbShift.SelectedIndex = 0;

            txtItemCode.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtBatchNo.Text = string.Empty;

            txtItemCodeIns.Text = string.Empty;
            txtLotNoIns.Text = string.Empty;
            txtBatchNoIns.Text = string.Empty;
            txtRemark.Text = string.Empty;

            partNo = string.Empty;
           
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridCheckingData.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridCheckingData.SelectedItems.Clear();
            else
                this.gridCheckingData.SelectedItem = null;

            gridCheckingData.ItemsSource = null;
            txtTotalRoll.Text = "0";

            cmdSave.IsEnabled = false;

            cbCustomer.Focus();
        }

        #endregion

        #region CheckLotNo
        private bool CheckLotNo(string lotNo , string lotNoIns)
        {
            bool chkLot = false;
            try
            {
                string lotNo1 = string.Empty;
                string lotNo2 = string.Empty;

                if (lotNo.Length > 0)
                    lotNo1 = lotNo.Substring(0, lotNo.Length - 1);
                else
                    lotNo1 = lotNo;

                if (lotNoIns.Length > 0)
                    lotNo2 = lotNoIns.Substring(0,lotNoIns.Length - 1);
                else
                    lotNo2 = lotNoIns;

                if (lotNo1 == lotNo2)
                {
                    chkLot = true;
                }
                else
                {
                    chkLot = false;
                }

                return chkLot;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }
        #endregion

        #region CheckBatchNo
        private bool CheckBatchNo(string batchNo, string batchNoIns)
        {
            bool chkBatch = false;
            try
            {
                if (batchNo == batchNoIns)
                {
                    chkBatch = true;
                }
                else
                {
                    chkBatch = false;
                }

                return chkBatch;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }
        #endregion

        #region SearchCheckingData
        private void SearchCheckingData(string _customer)
        {
            try
            {
                string _date = string.Empty;
                string _labitmCode  = string.Empty;
                string _result = "Y";
                
                if (dteCheckingDate.SelectedDate != null)
                {
                    _date = dteCheckingDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                }

                List<QA_SEARCHCHECKINGDATA> lots = new List<QA_SEARCHCHECKINGDATA>();

                lots = QualityAssuranceService.Instance.QA_SEARCHCHECKINGDATA(_customer, _date, _labitmCode, _result);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridCheckingData.ItemsSource = lots;
                    txtTotalRoll.Text = lots.Count.ToString("#,##0.##");
                }
                else
                {
                    gridCheckingData.ItemsSource = null;
                    txtTotalRoll.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region GetItemByCustomer
        private bool GetItemByCustomer(string _customer, string _item)
        {
            bool chkGetItem = false;

            try
            {
                List<ITM_GETITEMBYITEMCODEANDCUSIDDATA> lots = new List<ITM_GETITEMBYITEMCODEANDCUSIDDATA>();

                lots = QualityAssuranceService.Instance.ITM_GETITEMBYITEMCODEANDCUSID(_customer, _item);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    partNo = lots[0].PARTNO;
                    chkGetItem = true;
                }
                else
                {
                    partNo = string.Empty;
                    chkGetItem = false;
                }

                return chkGetItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }
        #endregion

        #region QA_InsertCheckingData
        private bool QA_InsertCheckingData(string P_RESULT)
        {
            bool chkQA = false;

            try
            {
                string P_CUSTOMERID = string.Empty;
                string P_LABITMCODE = string.Empty;
                string P_LABLOT = string.Empty;
                string P_LABBATCHNO = string.Empty;
                string P_INSITMCODE = string.Empty;
                string P_INSLOT = string.Empty;
                string P_INSBATCHNO = string.Empty;
                string P_CUSCODE = string.Empty;

                string P_SHIFT = string.Empty;
                string P_REMARK = string.Empty;
                
                DateTime? P_CHECKDATE = DateTime.Now;
                string P_CHECKEDBY = string.Empty;

                if (cbCustomer.SelectedValue != null)
                    P_CUSTOMERID = cbCustomer.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtItemCode.Text))
                    P_LABITMCODE = txtItemCode.Text;

                if (!string.IsNullOrEmpty(txtLotNo.Text))
                    P_LABLOT = txtLotNo.Text;

                if (!string.IsNullOrEmpty(txtBatchNo.Text))
                    P_LABBATCHNO = txtBatchNo.Text;

                if (!string.IsNullOrEmpty(txtItemCodeIns.Text))
                    P_INSITMCODE = txtItemCodeIns.Text;

                if (!string.IsNullOrEmpty(txtLotNoIns.Text))
                    P_INSLOT = txtLotNoIns.Text;

                if (!string.IsNullOrEmpty(txtBatchNoIns.Text))
                    P_INSBATCHNO = txtBatchNoIns.Text;

                if (!string.IsNullOrEmpty(partNo))
                    P_CUSCODE = partNo;

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_CHECKEDBY = txtOperator.Text;

                if (cbShift.SelectedValue != null)
                    P_SHIFT = cbShift.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtRemark.Text))
                    P_REMARK = txtRemark.Text;


                if (QualityAssuranceService.Instance.QA_INSERTCHECKINGDATA(P_CUSTOMERID, P_LABITMCODE, P_LABLOT, P_LABBATCHNO
                    , P_INSITMCODE, P_INSLOT, P_INSBATCHNO, P_CUSCODE, P_RESULT, P_CHECKDATE, P_CHECKEDBY, P_SHIFT, P_REMARK) == true)
                {
                    chkQA = true;
                }
                else
                {
                    chkQA = false;
                }

                return chkQA;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
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
                string _result = "Y";

                ConmonReportService.Instance.P_CUSID = string.Empty;
                ConmonReportService.Instance.P_DATE = string.Empty;
                ConmonReportService.Instance.P_LABITMCODE = string.Empty;
                ConmonReportService.Instance.P_RESULT = string.Empty;

                if (cbCustomer.SelectedValue != null)
                {
                    _customer = cbCustomer.SelectedValue.ToString();
                }

                if (dteCheckingDate.SelectedDate != null)
                {
                    _date = dteCheckingDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
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

