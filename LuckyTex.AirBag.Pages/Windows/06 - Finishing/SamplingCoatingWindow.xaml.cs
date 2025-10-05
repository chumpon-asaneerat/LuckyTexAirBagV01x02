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
    /// Interaction logic for SamplingCoatingWindow.xaml
    /// </summary>
    public partial class SamplingCoatingWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public SamplingCoatingWindow()
        {
            InitializeComponent();

            chkPrintGray.IsChecked = true;
            chkPrintCoated.IsChecked = true;
        }

        #endregion

        #region Internal Variables

        private string weavLot = string.Empty;
        private string finishLot = string.Empty;
        private string itmCode = string.Empty;
        private string finishCustomer = string.Empty;
        private string productType = string.Empty;
        private string OperatorText = string.Empty;

        private decimal? Gray_WIDTH = null;
        private decimal? Gray_LENGTH = null;
        private string Gray_REMARK = string.Empty;

        private decimal? Coat_WIDTH = null;
        private decimal? Coat_LENGTH = null;
        private string Coat_REMARK = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (Gray_WIDTH != null)
                txtWidth.Text = Gray_WIDTH.Value.ToString("#,##0.##");

            if (Gray_LENGTH != null)
                txtLength.Text = Gray_LENGTH.Value.ToString("#,##0.##");

            if (!string.IsNullOrEmpty(Gray_REMARK))
                txtRemark.Text = Gray_REMARK;

            if (Coat_WIDTH != null)
                txtWidthCoat.Text = Coat_WIDTH.Value.ToString("#,##0.##");

            if (Coat_LENGTH != null)
                txtLengthCoat.Text = Coat_LENGTH.Value.ToString("#,##0.##");

            if (!string.IsNullOrEmpty(Coat_REMARK))
                txtRemarkCoat.Text = Coat_REMARK;
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            bool chkSaveGray = true;
            bool chkSaveCoat = true;

            #region Gray

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
                    chkSaveGray = true;
                }
                else
                {
                    "Can't Save data".ShowMessageBox(true);
                    chkSaveGray = false;
                }
            }
            //else
            //{
            //    if (P_WIDTH == null)
            //    {
            //        "กรุณาใส่ค่า หน้ากว้างผ้า (Width)".ShowMessageBox(true);
            //    }
            //    else if (P_LENGTH == null)
            //    {
            //        "กรุณาใส่ค่า จำนวนที่สุ่ม (Length)".ShowMessageBox(true);
            //    } 
            //}

            #endregion

            #region Coat

            decimal? P_WIDTHCoat = null;
            decimal? P_LENGTHCoat = null;
            string P_REMARKCoat = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(txtWidthCoat.Text))
                    P_WIDTHCoat = decimal.Parse(txtWidthCoat.Text);
            }
            catch
            {
                P_WIDTHCoat = 0;
            }

            try
            {
                if (!string.IsNullOrEmpty(txtLengthCoat.Text))
                    P_LENGTHCoat = decimal.Parse(txtLengthCoat.Text);
            }
            catch
            {
                P_LENGTHCoat = 0;
            }

            P_REMARKCoat = txtRemarkCoat.Text;

            if (P_WIDTHCoat != null && P_LENGTHCoat != null)
            {
                if (SaveCoat(P_WIDTHCoat, P_LENGTHCoat, P_REMARKCoat) == true)
                {
                    chkSaveCoat = true;
                }
                else
                {
                    "Can't Save data".ShowMessageBox(true);
                    chkSaveCoat = false;
                }
            }

            #endregion

            if (chkSaveGray == true && chkSaveCoat == true)
            {
                Print(weavLot, finishLot);

                this.DialogResult = true;
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
                txtWidthCoat.Focus();
                txtWidthCoat.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtWidthCoat_KeyDown

        private void txtWidthCoat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLengthCoat.Focus();
                txtLengthCoat.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLengthCoat_KeyDown

        private void txtLengthCoat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRemarkCoat.Focus();
                txtRemarkCoat.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtRemarkCoat_KeyDown

        private void txtRemarkCoat_KeyDown(object sender, KeyEventArgs e)
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

            txtRemarkCoat.Text = string.Empty;
            txtWidthCoat.Text = string.Empty;
            txtLengthCoat.Text = string.Empty;

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
                RepSampling(P_WEAVINGLOT, P_FINLOT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region RepSampling
        private void RepSampling(string WEAVINGLOT, string finishinglot)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(WEAVINGLOT))
                {
                    StringBuilder dp = new StringBuilder(256);
                    int size = dp.Capacity;
                    if (GetDefaultPrinter(dp, ref size))
                    {
                        ConmonReportService.Instance.Printername = dp.ToString().Trim();

                        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                        string DatePrint = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");

                        List<FINISHING_GETSAMPLINGSHEET> lots = CoatingDataService.Instance.FINISHING_GETSAMPLINGSHEETList(WEAVINGLOT, finishinglot);

                        if (null != lots && lots.Count > 0 && null != lots[0])
                        {

                            int i = 0;
                            foreach (var row in lots)
                            {
                                if (!string.IsNullOrEmpty(lots[i].FABRICTYPE) )
                                {
                                    if (lots[i].FABRICTYPE.Trim() == "GRAY" && chkPrintGray.IsChecked == true)
                                    {
                                        #region Print

                                        List<DataControl.ClassData.FinishingClassData.ListSampling> results = new List<DataControl.ClassData.FinishingClassData.ListSampling>();

                                        DataControl.ClassData.FinishingClassData.ListSampling inst = new DataControl.ClassData.FinishingClassData.ListSampling();

                                        inst.WEAVINGLOT = lots[i].WEAVINGLOT;
                                        inst.FINISHINGLOT = lots[i].FINISHINGLOT;
                                        inst.ITM_CODE = lots[i].ITM_CODE;
                                        inst.CREATEDATE = lots[i].CREATEDATE;
                                        inst.CREATEBY = lots[i].CREATEBY;
                                        inst.PRODUCTID = lots[i].PRODUCTID;
                                        inst.SAMPLING_WIDTH = lots[i].SAMPLING_WIDTH;
                                        inst.SAMPLING_LENGTH = lots[i].SAMPLING_LENGTH;
                                        inst.PROCESS = lots[i].PROCESS;
                                        inst.REMARK = lots[i].REMARK;
                                        inst.FABRICTYPE = lots[i].FABRICTYPE;
                                        inst.PRODUCTNAME = lots[i].PRODUCTNAME;

                                        results.Add(inst);

                                        if (results.Count > 0)
                                        {
                                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                                            rds.Value = results;
                                            DataControl.ClassData.Report r = new DataControl.ClassData.Report();

                                            r._reportViewer.LocalReport.DataSources.Clear();
                                            r._reportViewer.LocalReport.DataSources.Add(rds);
                                            r._reportViewer.LocalReport.ReportPath = path + @"\Report\RepSampling.rdlc";
                                            r._reportViewer.LocalReport.EnableExternalImages = true;

                                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                                            r._reportViewer.LocalReport.SetParameters(parameters);

                                            r._reportViewer.RefreshReport();

                                            LocalReportPageSettings _pageSetting = new LocalReportPageSettings();
                                            _pageSetting.PageLandscape = false;
                                            _pageSetting.MarginLeft = 0;
                                            _pageSetting.MarginRight = 0;
                                            _pageSetting.MarginTop = 0;
                                            _pageSetting.MarginBottom = 0.5;

                                            _pageSetting.PageHeight = 11.69;
                                            _pageSetting.PageWidth = 8.27;
                                            
                                            //เพิ่มใหม่ 
                                            //----------------------------------------------------------------//
                                            _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                                            _pageSetting.PaperName = "A4";
                                            _pageSetting.Height = 1169;
                                            _pageSetting.Width = 827;
                                            //----------------------------------------------------------------//

                                            LocalReport lr = new LocalReport();
                                            lr = r._reportViewer.LocalReport;

                                            StickerPrintService.Instance.Print(lr, dp.ToString().Trim(), _pageSetting);
                                        }

                                        #endregion
                                    }
                                    else if (lots[i].FABRICTYPE.Trim() == "COATED" && chkPrintCoated.IsChecked == true)
                                    {
                                        #region Print

                                        List<DataControl.ClassData.FinishingClassData.ListSampling> results = new List<DataControl.ClassData.FinishingClassData.ListSampling>();

                                        DataControl.ClassData.FinishingClassData.ListSampling inst = new DataControl.ClassData.FinishingClassData.ListSampling();

                                        inst.WEAVINGLOT = lots[i].WEAVINGLOT;
                                        inst.FINISHINGLOT = lots[i].FINISHINGLOT;
                                        inst.ITM_CODE = lots[i].ITM_CODE;
                                        inst.CREATEDATE = lots[i].CREATEDATE;
                                        inst.CREATEBY = lots[i].CREATEBY;
                                        inst.PRODUCTID = lots[i].PRODUCTID;
                                        inst.SAMPLING_WIDTH = lots[i].SAMPLING_WIDTH;
                                        inst.SAMPLING_LENGTH = lots[i].SAMPLING_LENGTH;
                                        inst.PROCESS = lots[i].PROCESS;
                                        inst.REMARK = lots[i].REMARK;
                                        inst.FABRICTYPE = lots[i].FABRICTYPE;
                                        inst.PRODUCTNAME = lots[i].PRODUCTNAME;

                                        results.Add(inst);

                                        if (results.Count > 0)
                                        {
                                            Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", results);

                                            rds.Value = results;
                                            DataControl.ClassData.Report r = new DataControl.ClassData.Report();

                                            r._reportViewer.LocalReport.DataSources.Clear();
                                            r._reportViewer.LocalReport.DataSources.Add(rds);
                                            r._reportViewer.LocalReport.ReportPath = path + @"\Report\RepSampling.rdlc";
                                            r._reportViewer.LocalReport.EnableExternalImages = true;

                                            IList<Microsoft.Reporting.WinForms.ReportParameter> parameters = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                                            parameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DatePrint", DatePrint));
                                            r._reportViewer.LocalReport.SetParameters(parameters);

                                            r._reportViewer.RefreshReport();

                                            LocalReportPageSettings _pageSetting = new LocalReportPageSettings();
                                            _pageSetting.PageLandscape = false;
                                            _pageSetting.MarginLeft = 0;
                                            _pageSetting.MarginRight = 0;
                                            _pageSetting.MarginTop = 0;
                                            _pageSetting.MarginBottom = 0.5;

                                            _pageSetting.PageHeight = 11.69;
                                            _pageSetting.PageWidth = 8.27;

                                            //เพิ่มใหม่ 
                                            //----------------------------------------------------------------//
                                            _pageSetting.RawKind = (int)System.Drawing.Printing.PaperKind.A4;
                                            _pageSetting.PaperName = "A4";
                                            _pageSetting.Height = 1169;
                                            _pageSetting.Width = 827;
                                            //----------------------------------------------------------------//

                                            LocalReport lr = new LocalReport();
                                            lr = r._reportViewer.LocalReport;

                                            StickerPrintService.Instance.Print(lr, dp.ToString().Trim(), _pageSetting);
                                        }

                                        #endregion
                                    
                                    }
                                }

                                i++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error Load Data", MessageBoxButton.OK, MessageBoxImage.Error);
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
                Gray_WIDTH = null;
                Gray_LENGTH = null;
                Gray_REMARK = string.Empty;

                Coat_WIDTH = null;
                Coat_LENGTH = null;
                Coat_REMARK = string.Empty;

                int i = 0;
                foreach (var row in results)
                {
                    if (results[i].FABRICTYPE.Trim() == "GRAY")
                    {
                        if (Gray_WIDTH == null)
                        {
                            Gray_WIDTH = results[i].SAMPLING_WIDTH;
                            Gray_LENGTH = results[i].SAMPLING_LENGTH;
                            Gray_REMARK = results[i].REMARK;
                        }
                    }
                    else if (results[i].FABRICTYPE.Trim() == "COATED")
                    {
                        if (Coat_WIDTH == null)
                        {
                            Coat_WIDTH = results[i].SAMPLING_WIDTH;
                            Coat_LENGTH = results[i].SAMPLING_LENGTH;
                            Coat_REMARK = results[i].REMARK;
                        }
                    }

                    i++;
                }
            }
        }

        #region SaveSampling

        private bool SaveSampling(decimal? P_WIDTH, decimal? P_LENGTH, string P_REMARK )
        {
            try
            {
                if (CoatingDataService.Instance.FINISHING_SAMPLINGDATA(weavLot, finishLot, itmCode+"G", finishCustomer, productType, OperatorText, P_WIDTH, P_LENGTH, P_REMARK) == true)
                {
                    //Print(weavLot, finishLot);

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

        #region SaveCoat

        private bool SaveCoat(decimal? P_WIDTH, decimal? P_LENGTH, string P_REMARK)
        {
            try
            {
                if (CoatingDataService.Instance.FINISHING_SAMPLINGDATA(weavLot, finishLot, itmCode, finishCustomer, productType, OperatorText, P_WIDTH, P_LENGTH, P_REMARK) == true)
                {
                    //Print(weavLot, finishLot);

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

        #endregion

    }
}
