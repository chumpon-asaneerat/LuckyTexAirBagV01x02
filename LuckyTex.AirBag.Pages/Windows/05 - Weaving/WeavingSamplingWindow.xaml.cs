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
    /// Interaction logic for WeavingSamplingWindow.xaml
    /// </summary>
    public partial class WeavingSamplingWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public WeavingSamplingWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables
        private string BEAMLOT = string.Empty;
        private string LOOM = string.Empty;
        private string ITM_WEAVING = string.Empty;
        private string BARNO = string.Empty;
        private DateTime? stDate = null;
        private string OperatorText = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            WEAV_GETSAMPLINGDATA(BEAMLOT, LOOM);
        }

        #endregion

        #region Button Events

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSAMPLING.Text))
            {
                WEAV_SAMPLING();
            }
            else
            {
                "1st sampling isn't null".ShowMessageBox();
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

        private void txtSPIRAL_L_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSPIRAL_R.Focus();
                txtSPIRAL_R.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSPIRAL_R_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSAMPLING.Focus();
                txtSAMPLING.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSAMPLING_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSAMPLINGBY.Focus();
                txtSAMPLINGBY.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSAMPLINGBY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtREMARK.Focus();
                txtREMARK.SelectAll();
                e.Handled = true;
            }
        }

        private void txtREMARK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRECUT.Focus();
                txtRECUT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtRECUT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRECUTBY.Focus();
                txtRECUTBY.SelectAll();
                e.Handled = true;
            }
        }

        private void txtRECUTBY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdPrint.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string P_BEAMROLL, string P_LOOM)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WeavingSampling";
                ConmonReportService.Instance.BEAMLOT = P_BEAMROLL;
                ConmonReportService.Instance.LOOM = P_LOOM;

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

        private void Preview(string P_BEAMROLL, string P_LOOM)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WeavingSampling";
                ConmonReportService.Instance.BEAMLOT = P_BEAMROLL;
                ConmonReportService.Instance.LOOM = P_LOOM;

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

        #region ClearControl

        private void ClearControl()
        {
            txtBEAMLOT.Text = string.Empty;
            txtITM_WEAVING.Text = string.Empty;
            txtStartDate.Text = string.Empty;

            txtLOOMNO.Text = string.Empty;
            txtBARNO.Text = string.Empty;
            txtSPIRAL_L.Text = string.Empty;
            txtSPIRAL_R.Text = string.Empty;
            txtSAMPLING.Text = string.Empty;
            txtSAMPLINGBY.Text = string.Empty;

            txtREMARK.Text = string.Empty;
            txtRECUT.Text = string.Empty;
            txtRECUTBY.Text = string.Empty;
            txtOperator.Text = string.Empty;

            txtSPIRAL_L.SelectAll();
            txtSPIRAL_L.Focus();
        }

        #endregion

        private void WEAV_GETSAMPLINGDATA(string beamroll, string loom)
        {
            try
            {
                List<WEAV_GETSAMPLINGDATA> Result = new List<Models.WEAV_GETSAMPLINGDATA>();

                Result = WeavingDataService.Instance.WEAV_GETSAMPLINGDATA(beamroll, loom);

                if (Result != null && Result.Count > 0)
                {
                    txtBEAMLOT.Text = Result[0].BEAMERROLL;
                    txtITM_WEAVING.Text = Result[0].ITM_WEAVING;
                    txtLOOMNO.Text = Result[0].LOOMNO;
                    txtBARNO.Text = Result[0].BARNO;

                    if (Result[0].SPIRAL_L != null)
                        txtSPIRAL_L.Text = Result[0].SPIRAL_L.Value.ToString("#,##0.##");

                    if (Result[0].SPIRAL_R != null)
                        txtSPIRAL_R.Text = Result[0].SPIRAL_R.Value.ToString("#,##0.##");

                    if (Result[0].STSAMPLING != null)
                        txtSAMPLING.Text = Result[0].STSAMPLING.Value.ToString("#,##0.##");

                    txtSAMPLINGBY.Text = Result[0].STSAMPLINGBY;
                    txtREMARK.Text = Result[0].REMARK;

                    if (Result[0].RECUTSAMPLING != null)
                        txtRECUT.Text = Result[0].RECUTSAMPLING.Value.ToString("#,##0.##");

                    txtRECUTBY.Text = Result[0].RECUTBY;

                    txtOperator.Text = OperatorText;

                    if (stDate != null)
                        txtStartDate.Text = stDate.Value.ToString("dd/MM/yy HH:mm");
                }
                else
                {
                    txtBEAMLOT.Text = BEAMLOT;
                    txtITM_WEAVING.Text = ITM_WEAVING;
                    txtLOOMNO.Text = LOOM;
                    txtBARNO.Text = BARNO;

                    txtSAMPLINGBY.Text = OperatorText;
                    txtOperator.Text = OperatorText;

                    if (stDate != null)
                        txtStartDate.Text = stDate.Value.ToString("dd/MM/yy HH:mm");
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void WEAV_SAMPLING()
        {
            try
            {
                string P_BEAMERROLL = string.Empty;
                string P_LOOM = string.Empty;
                string P_ITMWEAVE = string.Empty;
                DateTime? P_SETTINGDATE = null;
                string P_BARNO = string.Empty;
                decimal? P_SPIRIAL_L = null;
                decimal? P_SPIRIAL_R = null;
                decimal? P_SAMPLING = null;
                string P_SAMPLINGBY = string.Empty;
                decimal? P_RECUT = null;
                string P_RECUTBY = string.Empty;
                DateTime? P_RECUTDATE = DateTime.Now;
                string P_REMARK = string.Empty;

                string RESULT = string.Empty;

                P_BEAMERROLL = txtBEAMLOT.Text;
                P_LOOM = txtLOOMNO.Text;
                P_ITMWEAVE = txtITM_WEAVING.Text;
                P_SETTINGDATE = stDate;
                P_BARNO = txtBARNO.Text;

                if (!string.IsNullOrEmpty(txtSPIRAL_L.Text))
                    P_SPIRIAL_L = Convert.ToDecimal(txtSPIRAL_L.Text);

                if (!string.IsNullOrEmpty(txtSPIRAL_R.Text))
                    P_SPIRIAL_R = Convert.ToDecimal(txtSPIRAL_R.Text);

                if (!string.IsNullOrEmpty(txtSAMPLING.Text))
                    P_SAMPLING = Convert.ToDecimal(txtSAMPLING.Text);

                P_SAMPLINGBY = txtSAMPLINGBY.Text;

                if (!string.IsNullOrEmpty(txtRECUT.Text))
                    P_RECUT = Convert.ToDecimal(txtRECUT.Text);

                P_RECUTBY = txtRECUTBY.Text;

                P_REMARK = txtREMARK.Text;

                RESULT = WeavingDataService.Instance.WEAV_SAMPLING(P_BEAMERROLL,
                            P_LOOM, P_ITMWEAVE, P_SETTINGDATE,
                            P_BARNO, P_SPIRIAL_L, P_SPIRIAL_R, P_SAMPLING,
                            P_SAMPLINGBY, P_RECUT, P_RECUTBY, P_RECUTDATE, P_REMARK);

                if (string.IsNullOrEmpty(RESULT))
                {
                    Preview(P_BEAMERROLL, P_LOOM);
                }
                else
                {
                    RESULT.ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region Public Properties

        public void Setup(string P_BEAMLOT, string P_LOOM, string P_ITM_WEAVING, string P_BARNO
            , string P_OPERATORID, DateTime? StartDate)
        {
            BEAMLOT = P_BEAMLOT;
            LOOM = P_LOOM;
            ITM_WEAVING = P_ITM_WEAVING;
            BARNO = P_BARNO;
            OperatorText = P_OPERATORID;
            stDate = StartDate;
        }

        #endregion

    }
}
