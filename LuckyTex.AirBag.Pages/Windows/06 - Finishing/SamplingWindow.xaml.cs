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
using System.Runtime.InteropServices;
using System.Threading;

using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using DataControl.ClassData;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for SamplingWindow.xaml
    /// </summary>
    public partial class SamplingWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public SamplingWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string weavLot = string.Empty;
        private string finishLot = string.Empty;
        private string itmCode = string.Empty;
        private string finishCustomer = string.Empty;
        private string productType = string.Empty;
        private string OperatorText = string.Empty;

        private decimal? SAMPLING_WIDTH = null;
        private decimal? SAMPLING_LENGTH = null;
        private string REMARK = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (SAMPLING_WIDTH != null)
                txtWidth.Text = SAMPLING_WIDTH.Value.ToString("#,##0.##");

            if (SAMPLING_LENGTH != null)
                txtLength.Text = SAMPLING_LENGTH.Value.ToString("#,##0.##");

            if (!string.IsNullOrEmpty(REMARK))
                txtRemark.Text = REMARK;
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            decimal? P_WIDTH = null;
            decimal? P_LENGTH = null;
            string P_REMARK = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(txtWidth.Text))
                    P_WIDTH = decimal.Parse(txtWidth.Text);
            }
            catch
            {
                P_WIDTH = 0;
            }

            try
            {
                if (!string.IsNullOrEmpty(txtLength.Text))
                    P_LENGTH = decimal.Parse(txtLength.Text);
            }
            catch
            {
                P_LENGTH = 0;
            }

            P_REMARK = txtRemark.Text;

            if (P_WIDTH != null && P_LENGTH != null)
            {
                if (SaveSampling(P_WIDTH, P_LENGTH, P_REMARK) == true)
                {
                    this.DialogResult = true;
                }
                else
                {
                    "Can't Save data".ShowMessageBox(true);
                }
            }
            else
            {
                if (P_WIDTH == null)
                {
                    "กรุณาใส่ค่า หน้ากว้างผ้า (Width)".ShowMessageBox(true);
                }
                else if (P_LENGTH == null)
                {
                    "กรุณาใส่ค่า จำนวนที่สุ่ม (Length)".ShowMessageBox(true);
                } 
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtWidth_KeyDown

        private void txtWidth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength.Focus();
                txtLength.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength_KeyDown

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRemark.Focus();
                txtRemark.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtRemark_KeyDown

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtRemark.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtLength.Text = string.Empty;

            txtWidth.SelectAll();
            txtWidth.Focus();
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string P_WEAVINGLOT, string P_FINLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "Sampling";
                ConmonReportService.Instance.WEAVINGLOT = P_WEAVINGLOT;
                ConmonReportService.Instance.FinishingLot = P_FINLOT;

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

        private void Preview(string P_WEAVINGLOT, string P_FINLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "Sampling";
                ConmonReportService.Instance.WEAVINGLOT = P_WEAVINGLOT;
                ConmonReportService.Instance.FinishingLot = P_FINLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Properties

        private void GetSamplingSheet(string weavLot, string finishLot)
        {
            List<FINISHING_GETSAMPLINGSHEET> results = null;

            results = CoatingDataService.Instance.FINISHING_GETSAMPLINGSHEETList(weavLot, finishLot);

            if (results != null && results.Count > 0)
            {
                SAMPLING_WIDTH = null;
                SAMPLING_LENGTH = null;
                REMARK = string.Empty;

                SAMPLING_WIDTH = results[0].SAMPLING_WIDTH;
                SAMPLING_LENGTH = results[0].SAMPLING_LENGTH;
                REMARK = results[0].REMARK;
            }
        }


        #region SaveSampling

        private bool SaveSampling(decimal? P_WIDTH, decimal? P_LENGTH, string P_REMARK )
        {
            try
            {
                if (CoatingDataService.Instance.FINISHING_SAMPLINGDATA(weavLot, finishLot, itmCode, finishCustomer, productType, OperatorText, P_WIDTH, P_LENGTH, P_REMARK) == true)
                {
                    Print(weavLot, finishLot);

                    return true;
                }
                else
                {
                    "Can't Save FINISHING_SAMPLINGDATA".ShowMessageBox(true);

                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);

                return false;
            }
        }

        #endregion

        #endregion

        #region Public Properties

        public void Setup(string P_WEAVLOT, string P_FINISHLOT, string P_ITMCODE, string P_FINISHCUSTOMER, string P_PRODUCTTYPEID
            , string P_OPERATORID)
        {
            weavLot = P_WEAVLOT;
            finishLot = P_FINISHLOT;
            itmCode = P_ITMCODE;
            finishCustomer = P_FINISHCUSTOMER;
            productType = P_PRODUCTTYPEID;
            OperatorText = P_OPERATORID;

            if (!string.IsNullOrEmpty(weavLot) && !string.IsNullOrEmpty(finishLot))
            {
                GetSamplingSheet(weavLot, finishLot);
            }
        }

        public void SetupFinishingSearch(string P_WEAVLOT, string P_FINISHLOT, string P_ITMCODE, string P_FINISHCUSTOMER, string P_PRODUCTTYPEID
           , string P_OPERATORID, decimal? P_SAMPLING_WIDTH, decimal? P_SAMPLING_LENGTH, string P_REMARK)
        {
            weavLot = P_WEAVLOT;
            finishLot = P_FINISHLOT;
            itmCode = P_ITMCODE;
            finishCustomer = P_FINISHCUSTOMER;
            productType = P_PRODUCTTYPEID;
            OperatorText = P_OPERATORID;

            if (!string.IsNullOrEmpty(weavLot) && !string.IsNullOrEmpty(finishLot))
            {
                GetSamplingSheet(weavLot, finishLot);
            }

            //SAMPLING_WIDTH = P_SAMPLING_WIDTH;
            //SAMPLING_LENGTH = P_SAMPLING_LENGTH;
            //REMARK = P_REMARK;
        }

        #endregion

    }
}
