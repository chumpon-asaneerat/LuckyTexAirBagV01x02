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

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;

using System.Globalization;
using System.Threading;

#endregion


namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ScouringDryerFinishingPage.xaml
    /// </summary>
    public partial class ScouringDryerFinishingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScouringDryerFinishingPage()
        {
            InitializeComponent();

            LoadShift();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Scouring1MachineConfig;

            EnabledCon(false);

            rbMassProduction.Visibility = System.Windows.Visibility.Collapsed;
            rbTest.Visibility = System.Windows.Visibility.Collapsed;
            rbGuide.Visibility = System.Windows.Visibility.Collapsed;
            txtCustomer.IsEnabled = false;
            txtItemGoods.IsEnabled = false;
            txtItemWeaving.IsEnabled = false;

            txtLength.IsEnabled = false;
            txtFINISHINGLOT.IsEnabled = false;
            txtStartTime.IsEnabled = false;
            txtPreparingTime.IsEnabled = false;

            txtClothHumidity.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITYBFSpecification.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITY_BF.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Internal Variables

        private FinishingSession _session = null;
        private bool mcStatus = true;

        string P_FINISHINGLOT = string.Empty;
        string P_WEAVINGLOT = string.Empty;
        string P_PROCESS = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Private Methods

        #region Inspection Session methods

        private void InitSession()
        {
            if (null != _session)
            {
                txtOperator.Text = (null != _session.CurrentUser.OperatorId) ?
                    _session.CurrentUser.OperatorId.ToString() : "-";
                _session.OnStateChanged += new EventHandler(_session_OnStateChanged);
            }
            else
            {
                txtOperator.Text = "-";
            }
        }

        private void ReleaseSession()
        {
            if (null != _session)
            {
                _session.OnStateChanged -= new EventHandler(_session_OnStateChanged);
            }
            _session = null;
        }

        void _session_OnStateChanged(object sender, EventArgs e)
        {
            if (null != _session)
            {

            }
        }

        #endregion

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            chkManual.IsChecked = false;
            chkManualPLC.IsChecked = false;
            CheckManual();

            txtLot.Focus();
            txtLot.SelectAll();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        void ScouringDryerCounter_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<FinishingCounter> e)
        {
            if (null == e.Value)
            {
                return;
            }
            if (chkManual.IsChecked.HasValue && !chkManual.IsChecked.Value)
            {
                #region BatchCounter1

                _session.LENGTH1 = (decimal)(e.Value.BatchCounter1);
                txtLength1.Text = _session.LENGTH1.Value.ToString("#,##0.##");

                #endregion

                #region BatchCounter2

                _session.LENGTH2 = (decimal)(e.Value.BatchCounter2);
                txtLength2.Text = _session.LENGTH2.Value.ToString("#,##0.##");

                #endregion

                #region BatchCounter3

                _session.LENGTH3 = (decimal)(e.Value.BatchCounter3);
                txtLength3.Text = _session.LENGTH3.Value.ToString("#,##0.##");

                #endregion

                #region BatchCounter4

                _session.LENGTH4 = (decimal)(e.Value.BatchCounter4);
                txtLength4.Text = _session.LENGTH4.Value.ToString("#,##0.##");

                #endregion

                #region BatchCounter5

                _session.LENGTH5 = (decimal)(e.Value.BatchCounter5);
                txtLength5.Text = _session.LENGTH5.Value.ToString("#,##0.##");

                #endregion

                #region BatchCounter6

                _session.LENGTH6 = (decimal)(e.Value.BatchCounter6);
                txtLength6.Text = _session.LENGTH6.Value.ToString("#,##0.##");

                #endregion

                #region BatchCounter7

                _session.LENGTH7 = (decimal)(e.Value.BatchCounter7);
                txtLength7.Text = _session.LENGTH7.Value.ToString("#,##0.##");

                #endregion

                if (e.Value.TotalFlag == 0)
                {
                    if (cmdFinish.IsEnabled == true)
                        cmdFinish.IsEnabled = false;
                }
                else if (e.Value.TotalFlag == 1)
                {
                    if (cmdFinish.IsEnabled == false)
                    {
                        CalTotalLength();
                        cmdFinish.IsEnabled = true;
                    }
                }
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

        #region CommonLength_LostFocus
        private void CommonLength_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }
        #endregion

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSampling.Focus();
                e.Handled = true;
            }
        }

        private void txtLot_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScouringNo.Text) && !string.IsNullOrEmpty(txtLot.Text))
            {
                FINISHING_DRYERDATABYLOT(txtScouringNo.Text, txtLot.Text);
            }
        }

        #region txtLength1_KeyDown

        private void txtLength1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength2.Focus();
                txtLength2.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength2_KeyDown

        private void txtLength2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength3.Focus();
                txtLength3.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength3_KeyDown

        private void txtLength3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength4.Focus();
                txtLength4.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength4_KeyDown

        private void txtLength4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength5.Focus();
                txtLength5.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength5_KeyDown

        private void txtLength5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength6.Focus();
                txtLength6.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength6_KeyDown

        private void txtLength6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength7.Focus();
                txtLength7.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLength7_KeyDown

        private void txtLength7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH_AF_HEATActual.Focus();
                txtWIDTH_AF_HEATActual.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        private void txtWIDTH_BE_HEATActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtACCPRESUREActual.Focus();
                txtACCPRESUREActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtACCPRESUREActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtASSTENSIONActual.Focus();
                txtASSTENSIONActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtASSTENSIONActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtACCARIDENSERActual.Focus();
                txtACCARIDENSERActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtACCARIDENSERActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCHIFROTActual.Focus();
                txtCHIFROTActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCHIFROTActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCHIREARActual.Focus();
                txtCHIREARActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCHIREARActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHOTPV.Focus();
                txtHOTPV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHOTPV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSPEED.Focus();
                txtSPEED.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTEAMPRESSUREActual.Focus();
                txtSTEAMPRESSUREActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTEAMPRESSUREActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDRYERUPCIRCUFANActual.Focus();
                txtDRYERUPCIRCUFANActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDRYERUPCIRCUFANActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXHAUSTFANActual.Focus();
                txtEXHAUSTFANActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXHAUSTFANActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH_AF_HEATActual.Focus();
                txtWIDTH_AF_HEATActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH_AF_HEATActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                //txtHUMIDITY_BF.Focus();
                //txtHUMIDITY_BF.SelectAll();
                txtHUMIDITY_AF.Focus();
                txtHUMIDITY_AF.SelectAll();
                e.Handled = true;
            }
        }

        #region txtHUMIDITY_BF_KeyDown
        private void txtHUMIDITY_BF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHUMIDITY_AF.Focus();
                txtHUMIDITY_AF.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtHUMIDITY_AF_KeyDown
        private void txtHUMIDITY_AF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdFinish.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region CheckBox

        private void chkManual_Checked(object sender, RoutedEventArgs e)
        {
            CheckManual();
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckManual();
        }

        private void chkManualPLC_Checked(object sender, RoutedEventArgs e)
        {
            EnabledCon(true);
        }

        private void chkManualPLC_Unchecked(object sender, RoutedEventArgs e)
        {
            EnabledCon(false);
        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdFinish_Click

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            if (txtOperator.Text != "" && txtFINISHINGLOT.Text != "" && txtLength1.Text != "")
            {
                if (CheckNull() == true)
                {
                    if (Finish() == true)
                    {
                        D365_FN(txtFINISHINGLOT.Text, _session.WEAVINGLOT, "Dryer");

                        Print(_session.WEAVINGLOT, _session.ItemCode, txtFINISHINGLOT.Text);
                    }
                }
                else
                    "Can't Finish Please check data isn't null".ShowMessageBox(true);
            }
            else
            {
                if (txtLength1.Text == "")
                    "Length 1 can't null".ShowMessageBox(true);
            }
        }

        #endregion

        #region cmdPreview_Click

        private void cmdPreview_Click(object sender, RoutedEventArgs e)
        {
            #region Check(s)

            string scouringNo = txtScouringNo.Text;
            if (string.IsNullOrWhiteSpace(scouringNo))
            {
                txtScouringNo.SelectAll();
                txtScouringNo.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(_session.WEAVINGLOT))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(_session.ItemCode))
            {
                return;
            }

            #endregion

            if (!string.IsNullOrWhiteSpace(scouringNo))
            {
                if (cmdFinish.IsEnabled == true)
                {
                    if (txtOperator.Text != "" && txtFINISHINGLOT.Text != "" && txtLength1.Text != "")
                    {
                        if (CheckNull() == true)
                        {
                            if (Finish() == true)
                            {
                                Preview(_session.WEAVINGLOT, _session.ItemCode, txtFINISHINGLOT.Text);
                            }
                        }
                        else
                            "Can't Finish Please check data isn't null".ShowMessageBox(true);
                    }
                    else
                    {
                        if (txtLength1.Text == "")
                            "Length 1 can't null".ShowMessageBox(true);
                    }
                }
                else
                {
                    if (txtFINISHINGLOT.Text != "")
                        Preview(_session.WEAVINGLOT, _session.ItemCode, txtFINISHINGLOT.Text);
                }
            }
        }

        #endregion

        #region cmdRemark_Click
        private void cmdRemark_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                string remark = _session.GetDryerRemark("P", txtScouringNo.Text);
                if (null == remark)
                    remark = string.Empty;
                RemarkInfo remarkInfo = this.ShowRemarkBox(remark);
                if (null != remarkInfo)
                {
                    string FINISHLOT = txtFINISHINGLOT.Text;
                    string ItemCode = _session.ItemCode;
                    _session.AddDryerRemark(FINISHLOT, ItemCode, remarkInfo.Remark);
                }
            }
        }
        #endregion

        private void cmdSampling_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                SamplingWindow window = new SamplingWindow();

                string P_WEAVLOT = txtLot.Text;
                string FINISHLOT = txtFINISHINGLOT.Text;
                string P_ITMCODE = txtItemGoods.Text;
                string P_FINISHCUSTOMER = txtCustomer.Text;
                string P_PRODUCTTYPEID = string.Empty;
                string operatorid = txtOperator.Text;

                if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false && rbGuide.IsChecked == false)
                {
                    P_PRODUCTTYPEID = "1";
                }
                else if (rbMassProduction.IsChecked == false && rbTest.IsChecked == true && rbGuide.IsChecked == false)
                {
                    P_PRODUCTTYPEID = "2";
                }
                else if (rbMassProduction.IsChecked == false && rbTest.IsChecked == false && rbGuide.IsChecked == true)
                {
                    P_PRODUCTTYPEID = "3";
                }

                window.Setup(P_WEAVLOT, FINISHLOT, P_ITMCODE, P_FINISHCUSTOMER, P_PRODUCTTYPEID, operatorid);

                if (window.ShowDialog() == true)
                {

                }
            }
        }

        #endregion

        #region private Methods

        #region LoadShift

        private void LoadShift()
        {
            if (cbShift.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C" };

                cbShift.ItemsSource = str;
                cbShift.SelectedIndex = 0;
            }
        }

        #endregion

        #region ShowLength

        private void ShowLength()
        {
            if (txtLength2.Visibility == System.Windows.Visibility.Collapsed
                && txtLength3.Visibility == System.Windows.Visibility.Collapsed
                && txtLength4.Visibility == System.Windows.Visibility.Collapsed
                && txtLength5.Visibility == System.Windows.Visibility.Collapsed
                && txtLength6.Visibility == System.Windows.Visibility.Collapsed
                && txtLength7.Visibility == System.Windows.Visibility.Collapsed)
            {
                labLength2.Visibility = System.Windows.Visibility.Visible;
                txtLength2.Visibility = System.Windows.Visibility.Visible;
                txtLength2.Text = "0";
                txtLength2.Focus();
                txtLength2.SelectAll();
            }
            else if (txtLength2.Visibility == System.Windows.Visibility.Visible
                && txtLength3.Visibility == System.Windows.Visibility.Collapsed
                && txtLength4.Visibility == System.Windows.Visibility.Collapsed
                && txtLength5.Visibility == System.Windows.Visibility.Collapsed
                && txtLength6.Visibility == System.Windows.Visibility.Collapsed
                && txtLength7.Visibility == System.Windows.Visibility.Collapsed)
            {
                labLength3.Visibility = System.Windows.Visibility.Visible;
                txtLength3.Visibility = System.Windows.Visibility.Visible;

                txtLength3.Text = "0";
                txtLength3.Focus();
                txtLength3.SelectAll();
            }
            else if (txtLength2.Visibility == System.Windows.Visibility.Visible
                && txtLength3.Visibility == System.Windows.Visibility.Visible
                && txtLength4.Visibility == System.Windows.Visibility.Collapsed
                && txtLength5.Visibility == System.Windows.Visibility.Collapsed
                && txtLength6.Visibility == System.Windows.Visibility.Collapsed
                && txtLength7.Visibility == System.Windows.Visibility.Collapsed)
            {
                labLength4.Visibility = System.Windows.Visibility.Visible;
                txtLength4.Visibility = System.Windows.Visibility.Visible;

                txtLength4.Text = "0";
                txtLength4.Focus();
                txtLength4.SelectAll();
            }
            else if (txtLength2.Visibility == System.Windows.Visibility.Visible
                && txtLength3.Visibility == System.Windows.Visibility.Visible
                && txtLength4.Visibility == System.Windows.Visibility.Visible
                && txtLength5.Visibility == System.Windows.Visibility.Collapsed
                && txtLength6.Visibility == System.Windows.Visibility.Collapsed
                && txtLength7.Visibility == System.Windows.Visibility.Collapsed)
            {
                labLength5.Visibility = System.Windows.Visibility.Visible;
                txtLength5.Visibility = System.Windows.Visibility.Visible;

                txtLength5.Text = "0";
                txtLength5.Focus();
                txtLength5.SelectAll();
            }
            else if (txtLength2.Visibility == System.Windows.Visibility.Visible
                && txtLength3.Visibility == System.Windows.Visibility.Visible
                && txtLength4.Visibility == System.Windows.Visibility.Visible
                && txtLength5.Visibility == System.Windows.Visibility.Visible
                && txtLength6.Visibility == System.Windows.Visibility.Collapsed
                && txtLength7.Visibility == System.Windows.Visibility.Collapsed)
            {
                labLength6.Visibility = System.Windows.Visibility.Visible;
                txtLength6.Visibility = System.Windows.Visibility.Visible;

                txtLength6.Text = "0";
                txtLength6.Focus();
                txtLength6.SelectAll();
            }
            else if (txtLength2.Visibility == System.Windows.Visibility.Visible
                && txtLength3.Visibility == System.Windows.Visibility.Visible
                && txtLength4.Visibility == System.Windows.Visibility.Visible
                && txtLength5.Visibility == System.Windows.Visibility.Visible
                && txtLength6.Visibility == System.Windows.Visibility.Visible
                && txtLength7.Visibility == System.Windows.Visibility.Collapsed)
            {
                labLength7.Visibility = System.Windows.Visibility.Visible;
                txtLength7.Visibility = System.Windows.Visibility.Visible;

                txtLength7.Text = "0";
                txtLength7.Focus();
                txtLength7.SelectAll();
            }
        }

        #endregion

        #region ClearFinishing

        private void ClearFinishing()
        {
            txtWIDTH_BE_HEATSpecification.Text = "";
            txtACCPRESURESpecification.Text = "";
            txtASSTENSIONSpecification.Text = "";
            txtACCARIDENSERSpecification.Text = "";
            txtCHIFROTSpecification.Text = "";
            txtCHIREARSpecification.Text = "";
            txtDRYERTEMP1Specification.Text = "";
            txtSPEEDSpecification.Text = "";

            txtSTEAMPRESSURESpecification.Text = "";
            txtDRYERUPCIRCUFANSpecification.Text = "";

            txtEXHAUSTFANSpecification.Text = "";
            txtWIDTH_AF_HEATSpecification.Text = "";

            txtHUMIDITYBFSpecification.Text = "";
            txtHUMIDITYAFSpecification.Text = "";

            ConmonReportService.Instance.ReportName = "";
            ConmonReportService.Instance.ScouringNo = "";
            ConmonReportService.Instance.WEAVINGLOT = "";
            ConmonReportService.Instance.ITM_Code = "";
            ConmonReportService.Instance.FinishingLot = "";
        }

        #endregion

        #region ClearData

        private void ClearData()
        {
            try
            {
                txtCustomer.Text = "";
                txtItemGoods.Text = "";
                rbMassProduction.IsChecked = true;
                rbTest.IsChecked = false;
                rbGuide.IsChecked = false;
                txtItemWeaving.Text = "";
                txtLot.Text = "";
                txtLength.Text = "";

                txtWIDTH_BE_HEATActual.Text = "";
                txtACCPRESUREActual.Text = "";
                txtASSTENSIONActual.Text = "";
                txtACCARIDENSERActual.Text = "";
                txtCHIFROTActual.Text = "";
                txtCHIREARActual.Text = "";
                txtHOTPV.Text = "";

                txtSPEED.Text = "";
                txtSTEAMPRESSUREActual.Text = "";
                txtDRYERUPCIRCUFANActual.Text = "";
                txtEXHAUSTFANActual.Text = "";
                txtWIDTH_AF_HEATActual.Text = "";
                txtHUMIDITY_BF.Text = "";
                txtHUMIDITY_AF.Text = "";

                cbShift.SelectedIndex = 0;

                if (_session.Customer != "")
                    _session = new FinishingSession();

                P_FINISHINGLOT = string.Empty;
                P_WEAVINGLOT = string.Empty;
                P_PROCESS = string.Empty;

                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region EnabledCon
        private void EnabledCon(bool chkManual)
        {
            txtHOTPV.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            txtWIDTH_BE_HEATActual.IsEnabled = chkManual;
            txtACCPRESUREActual.IsEnabled = chkManual;
            txtASSTENSIONActual.IsEnabled = chkManual;
            txtACCARIDENSERActual.IsEnabled = chkManual;
            txtCHIFROTActual.IsEnabled = chkManual;
            txtCHIREARActual.IsEnabled = chkManual;

            txtSTEAMPRESSUREActual.IsEnabled = chkManual;
            txtDRYERUPCIRCUFANActual.IsEnabled = chkManual;
            txtEXHAUSTFANActual.IsEnabled = chkManual;
        }
        #endregion

        #region CheckManual
        private void CheckManual()
        {
            if (chkManual.IsChecked == false)
            {
                if (mcStatus == true)
                {
                    txtLength1.IsEnabled = false;
                    txtLength2.IsEnabled = false;
                    txtLength3.IsEnabled = false;
                    txtLength4.IsEnabled = false;
                    txtLength5.IsEnabled = false;
                    txtLength6.IsEnabled = false;
                    txtLength7.IsEnabled = false;
                }
                else
                {
                    "Machine Status = false Please check config".ShowMessageBox();
                }
            }
            else
            {
                txtLength1.IsEnabled = true;
                txtLength2.IsEnabled = true;
                txtLength3.IsEnabled = true;
                txtLength4.IsEnabled = true;
                txtLength5.IsEnabled = true;
                txtLength6.IsEnabled = true;
                txtLength7.IsEnabled = true;

                cmdFinish.IsEnabled = true;

                txtLength1.Focus();
                txtLength1.SelectAll();
            }
        }
        #endregion

        #region CheckNull
        private bool CheckNull()
        {
            bool chkSave = true;

            #region Check Null Processing

            if (txtWIDTH_BE_HEATActual.Text == "")
            {
                return false;
            }
            if (txtACCPRESUREActual.Text == "")
            {
                return false;
            }

            if (txtASSTENSIONActual.Text == "")
            {
                return false;
            }

            if (txtACCARIDENSERActual.Text == "")
            {
                return false;
            }


            if (txtCHIFROTActual.Text == "")
            {
                return false;
            }

            if (txtCHIREARActual.Text == "")
            {
                return false;
            }

            if (txtSTEAMPRESSUREActual.Text == "")
            {
                return false;
            }

            if (txtDRYERUPCIRCUFANActual.Text == "")
            {
                return false;
            }

            if (txtEXHAUSTFANActual.Text == "")
            {
                return false;
            }

            #endregion

            if (txtWIDTH_AF_HEATActual.Text == "")
            {
                return false;
            }
            if (txtHUMIDITY_AF.Text == "")
            {
                return false;
            }
            //if (txtHUMIDITY_BF.Text == "")
            //{
            //    return false;
            //}

            return chkSave;
        }
        #endregion

        #region LoadFinishing

        private void LoadFinishing(string itm_code, string mcNo)
        {
            try
            {
                List<FINISHING_GETDRYERCONDITIONData> items = _session.GetFINISHING_GETDRYERCONDITION(itm_code, mcNo);

                if (items != null && items.Count > 0)
                {
                    #region WIDTH_BE_HEAT

                    if (items[0].WIDTH_BE_HEAT_MAX != null && items[0].WIDTH_BE_HEAT_MIN != null)
                    {
                        //txtWIDTH_BE_HEATSpecification.Text = (items[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##") + " - " + items[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##"));
                        txtWIDTH_BE_HEATSpecification.Text = ("( " + items[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##") + " )");
                    }
                    else
                    {
                        txtWIDTH_BE_HEATSpecification.Text = "";
                    }

                    #endregion

                    #region ACCPRESURE

                    if (items[0].ACCPRESURE != null)
                    {
                        txtACCPRESURESpecification.Text = items[0].ACCPRESURE.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtACCPRESURESpecification.Text = "";
                    }

                    #endregion

                    #region ASSTENSION

                    if (items[0].ASSTENSION != null)
                    {
                        txtASSTENSIONSpecification.Text = items[0].ASSTENSION.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtASSTENSIONSpecification.Text = "";
                    }

                    #endregion

                    #region ACCARIDENSER

                    if (items[0].ACCARIDENSER != null)
                    {
                        txtACCARIDENSERSpecification.Text = items[0].ACCARIDENSER.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtACCARIDENSERSpecification.Text = "";
                    }

                    #endregion

                    #region CHIFROT

                    if (items[0].CHIFROT != null)
                    {
                        txtCHIFROTSpecification.Text = items[0].CHIFROT.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtCHIFROTSpecification.Text = "";
                    }

                    #endregion

                    #region CHIREAR

                    if (items[0].CHIREAR != null)
                    {
                        txtCHIREARSpecification.Text = items[0].CHIREAR.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtCHIREARSpecification.Text = "";
                    }

                    #endregion

                    #region DRYERTEMP1

                    if (items[0].DRYERTEMP1 != null && items[0].DRYERTEMP1_MARGIN != null)
                    {
                        txtDRYERTEMP1Specification.Text = (items[0].DRYERTEMP1.Value.ToString("#,##0.##") + " ± " + items[0].DRYERTEMP1_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtDRYERTEMP1Specification.Text = "";
                    }

                    #endregion

                    #region SPEED

                    if (items[0].SPEED != null && items[0].SPEED_MARGIN != null)
                    {
                        txtSPEEDSpecification.Text = (items[0].SPEED.Value.ToString("#,##0.##") + " ± " + items[0].SPEED_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtSPEEDSpecification.Text = "";
                    }

                    #endregion

                    #region STEAMPRESSURE

                    if (items[0].STEAMPRESSURE != null)
                    {
                        txtSTEAMPRESSURESpecification.Text = "> " + items[0].STEAMPRESSURE.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSTEAMPRESSURESpecification.Text = "";
                    }

                    #endregion

                    #region DRYERUPCIRCUFAN

                    if (items[0].DRYERUPCIRCUFAN != null)
                    {
                        txtDRYERUPCIRCUFANSpecification.Text = items[0].DRYERUPCIRCUFAN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtDRYERUPCIRCUFANSpecification.Text = "";
                    }

                    #endregion

                    #region EXHAUSTFAN

                    if (items[0].EXHAUSTFAN != null)
                    {
                        txtEXHAUSTFANSpecification.Text = items[0].EXHAUSTFAN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtEXHAUSTFANSpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_AF_HEAT

                    if (items[0].WIDTH_AF_HEAT != null && items[0].WIDTH_AF_HEAT_MARGIN != null)
                    {
                        txtWIDTH_AF_HEATSpecification.Text = (items[0].WIDTH_AF_HEAT.Value.ToString("#,##0.##") + " ± " + items[0].WIDTH_AF_HEAT_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtWIDTH_AF_HEATSpecification.Text = "";
                    }

                    #endregion

                    #region HUMIDITY

                    if (items[0].HUMIDITY_MAX != null)
                    {
                        string HUMIDITY = "< " + items[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                        txtHUMIDITYAFSpecification.Text = HUMIDITY;
                        txtHUMIDITYBFSpecification.Text = HUMIDITY;
                    }
                    else
                    {
                        txtHUMIDITYAFSpecification.Text = "";
                        txtHUMIDITYBFSpecification.Text = "";
                    }

                    #endregion

                    #region MCNO

                    //if (!string.IsNullOrEmpty(items[0].MCNO))
                    //{
                    //    txtScouringNo.Text = items[0].MCNO;
                    //}

                    #endregion
                }
                else
                {
                    string msg = "This Item Code " + itm_code + " Can not be used in this MC.";

                    msg.ShowMessageBox(false);
                    ClearFinishing();

                    txtItemGoods.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        private void FINISHING_DRYERDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            try
            {
                List<FINISHING_DRYERDATABYLOT> items = _session.GetFINISHING_DRYERDATABYLOT(P_MCNO, P_WEAVINGLOT);

                if (items != null && items.Count > 0)
                {
                    #region FINISHINGCUSTOMER

                    if (items[0].FINISHINGCUSTOMER != "")
                    {
                        txtCustomer.Text = items[0].FINISHINGCUSTOMER;
                        _session.Customer = items[0].FINISHINGCUSTOMER;

                        txtItemGoods.Text = items[0].ITM_CODE;
                        _session.ItemCode = items[0].ITM_CODE;

                        string ScouringNo = txtScouringNo.Text;

                        if (ScouringNo != "" && items[0].ITM_CODE != "")
                        {
                            LoadFinishing(items[0].ITM_CODE, ScouringNo);
                        }
                        else
                        {
                            ClearFinishing();
                        }
                    }

                    #endregion

                    #region PRODUCTTYPEID

                    if (items[0].PRODUCTTYPEID != "")
                    {
                        if (items[0].PRODUCTTYPEID == "1")
                        {
                            rbMassProduction.Visibility = System.Windows.Visibility.Visible;
                            rbTest.Visibility = System.Windows.Visibility.Collapsed;
                            rbGuide.Visibility = System.Windows.Visibility.Collapsed;

                            rbMassProduction.IsChecked = true;
                            rbTest.IsChecked = false;
                            rbGuide.IsChecked = false;
                        }
                        else if (items[0].PRODUCTTYPEID == "2")
                        {
                            rbMassProduction.Visibility = System.Windows.Visibility.Collapsed;
                            rbTest.Visibility = System.Windows.Visibility.Visible;
                            rbGuide.Visibility = System.Windows.Visibility.Collapsed;

                            rbMassProduction.IsChecked = false;
                            rbTest.IsChecked = true;
                            rbGuide.IsChecked = false;
                        }
                        else if (items[0].PRODUCTTYPEID == "3")
                        {
                            rbMassProduction.Visibility = System.Windows.Visibility.Collapsed;
                            rbTest.Visibility = System.Windows.Visibility.Collapsed;
                            rbGuide.Visibility = System.Windows.Visibility.Visible;

                            rbMassProduction.IsChecked = false;
                            rbTest.IsChecked = false;
                            rbGuide.IsChecked = true;
                        }
                    }

                    #endregion

                    txtItemWeaving.Text = items[0].ITM_WEAVING;
                    txtLot.Text = items[0].WEAVINGLOT;
                    _session.WEAVINGLOT = items[0].WEAVINGLOT;

                    txtLength.Text = items[0].WEAVLENGTH.ToString();
                    txtFINISHINGLOT.Text = items[0].FINISHINGLOT;

                    txtStartTime.Text = items[0].CONDITIONDATE.Value.ToString("dd/MM/yy HH:mm");
                    txtPreparingTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");

                    _session.STARTDATE = items[0].STARTDATE;
                    _session.CONDITONDATE = items[0].CONDITIONDATE;

                    //txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");

                    txtWIDTH_BE_HEATActual.Text = items[0].WIDTH_BE_HEAT.ToString();
                    txtACCPRESUREActual.Text = items[0].ACCPRESURE.ToString();
                    txtASSTENSIONActual.Text = items[0].ASSTENSION.ToString();
                    txtACCARIDENSERActual.Text = items[0].ACCARIDENSER.ToString();
                    txtCHIFROTActual.Text = items[0].CHIFROT.ToString();
                    txtCHIREARActual.Text = items[0].CHIREAR.ToString();
                    txtHOTPV.Text = items[0].DRYERTEMP1_PV.ToString();

                    txtSPEED.Text = items[0].SPEED_PV.ToString();
                    txtSTEAMPRESSUREActual.Text = items[0].STEAMPRESSURE.ToString();
                    txtDRYERUPCIRCUFANActual.Text = items[0].DRYERCIRCUFAN.ToString();
                    txtEXHAUSTFANActual.Text = items[0].EXHAUSTFAN.ToString();
                    txtWIDTH_AF_HEATActual.Text = items[0].WIDTH_AF_HEAT.ToString();

                    txtHUMIDITY_AF.Text = items[0].HUMIDITY_AF.ToString();
                    txtHUMIDITY_BF.Text = items[0].HUMIDITY_BF.ToString();

                    if (items[0].OPERATOR_GROUP != null)
                    {
                        string shift = items[0].OPERATOR_GROUP;

                        if (shift == "A")
                        {
                            cbShift.SelectedIndex = 0;
                        }
                        else if (shift == "B")
                        {
                            cbShift.SelectedIndex = 1;
                        }
                        else if (shift == "C")
                        {
                            cbShift.SelectedIndex = 2;
                        }
                        else if (shift == "D")
                        {
                            cbShift.SelectedIndex = 3;
                        }
                    }

                    _session.WIDTHBEHEAT = items[0].WIDTH_BE_HEAT;
                    _session.ACCPRESURE = items[0].ACCPRESURE;
                    _session.ASSTENSION = items[0].ASSTENSION;
                    _session.ACCARIDENSER = items[0].ACCARIDENSER;
                    _session.CHIFROT = items[0].CHIFROT;
                    _session.CHIREAR = items[0].CHIREAR;
                    _session.HOTFLUE_PV = items[0].DRYERTEMP1_PV;
                    _session.HOTFLUE_SP = items[0].DRYERTEMP1_SP;
                    _session.SPEED_PV = items[0].SPEED_PV;
                    _session.SPEED_SP = items[0].SPEED_SP;

                    _session.STEAMPRESURE = items[0].STEAMPRESSURE;
                    _session.DRYCIRCUFAN = items[0].DRYERCIRCUFAN;
                    _session.EXHAUSTFAN = items[0].EXHAUSTFAN;
                    _session.WIDTHAFHEAT = items[0].WIDTH_AF_HEAT;

                    _session.HUMIDITY_AF = items[0].HUMIDITY_AF;
                    _session.HUMIDITY_BF = items[0].HUMIDITY_BF;
                    _session.OPERATOR_GROUP = items[0].OPERATOR_GROUP;

                    FINISHING_SCOURINGPLCDATA(P_MCNO, P_WEAVINGLOT);
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void FINISHING_SCOURINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            try
            {
                List<FINISHING_SCOURINGPLCDATA> items = _session.GetFINISHING_SCOURINGPLCDATA(P_MCNO, P_WEAVINGLOT);

                if (items != null && items.Count > 0)
                {
                    #region STARTDATE
                    if (items[0].STARTDATE != null)
                    {
                        txtStartTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");
                        _session.STARTDATE = items[0].STARTDATE;
                    }
                    #endregion

                    #region HOTF
                    if (items[0].HOTF != null)
                    {
                        txtHOTPV.Text = items[0].HOTF.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region SPEED
                    if (items[0].SPEED != null)
                    {
                        txtSPEED.Text = items[0].SPEED.Value.ToString("#,##0.##");
                    }
                    #endregion

                    _session.SAT = items[0].SAT;
                    _session.SAT_MIN = items[0].SAT_MIN;
                    _session.SAT_MAX = items[0].SAT_MAX;
                    _session.WASHING1 = items[0].WASH1;
                    _session.WASHING1_MIN = items[0].WASH1_MIN;
                    _session.WASHING1_MAX = items[0].WASH1_MAX;
                    _session.WASHING2 = items[0].WASH2;
                    _session.WASHING2_MIN = items[0].WASH2_MIN;
                    _session.WASHING2_MAX = items[0].WASH2_MAX;
                    _session.HOTFLUE = items[0].HOTF;
                    _session.HOTFLUE_MIN = items[0].HOTF_MIN;
                    _session.HOTFLUE_MAX = items[0].HOTF_MAX;

                    _session.SPEED = items[0].SPEED;
                    _session.SPEED_MIN = items[0].SPEED_MIN;
                    _session.SPEED_MAX = items[0].SPEED_MAX;

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #region Old Finish

        //private bool OldFinish()
        //{
        //    bool chkFinish = false;

        //    try
        //    {
        //        string FINISHLOT = txtFINISHINGLOT.Text;
        //        string operatorid = txtOperator.Text;
        //        string flag = "F";
        //        decimal? length1 = null;
        //        decimal? length2 = null;
        //        decimal? length3 = null;
        //        decimal? length4 = null;
        //        decimal? length5 = null;
        //        decimal? length6 = null;
        //        decimal? length7 = null;

        //        if (txtLength1.Text != "")
        //            length1 = decimal.Parse(txtLength1.Text);

        //        if (txtLength2.Text != "")
        //            length2 = decimal.Parse(txtLength2.Text);

        //        if (txtLength3.Text != "")
        //            length3 = decimal.Parse(txtLength3.Text);

        //        if (txtLength4.Text != "")
        //            length4 = decimal.Parse(txtLength4.Text);

        //        if (txtLength5.Text != "")
        //            length5 = decimal.Parse(txtLength5.Text);

        //        if (txtLength6.Text != "")
        //            length6 = decimal.Parse(txtLength6.Text);

        //        if (txtLength7.Text != "")
        //            length7 = decimal.Parse(txtLength7.Text);

        //        CalTotalLength();

        //        #region HUMIDITY_AF
        //        if (txtHUMIDITY_AF.Text != ""
        //            && txtHUMIDITY_AF.Text != _session.HUMIDITY_AF.ToString())
        //        {
        //            _session.HUMIDITY_AF = decimal.Parse(txtHUMIDITY_AF.Text);
        //        }
        //        #endregion

        //        #region HUMIDITY_BF
        //        if (txtHUMIDITY_BF.Text != ""
        //            && txtHUMIDITY_BF.Text != _session.HUMIDITY_BF.ToString())
        //        {
        //            _session.HUMIDITY_BF = decimal.Parse(txtHUMIDITY_BF.Text);
        //        }
        //        #endregion

        //        if (cbShift.SelectedValue != null)
        //        {
        //            _session.OPERATOR_GROUP = cbShift.SelectedValue.ToString();
        //        }

        //        if (FINISHLOT != "" && operatorid != "" && length1 != null)
        //        {
        //            _session.FINISHLOT = FINISHLOT;
        //            _session.Operator = operatorid;
        //            _session.Flag = flag;

        //            _session.LENGTH1 = length1;
        //            _session.LENGTH2 = length2;
        //            _session.LENGTH3 = length3;
        //            _session.LENGTH4 = length4;
        //            _session.LENGTH5 = length5;
        //            _session.LENGTH6 = length6;
        //            _session.LENGTH7 = length7;

        //            string result = _session.FINISHING_UPDATEDRYERFinishing();

        //            if (string.IsNullOrEmpty(result) == true)
        //            {
        //                txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
        //                cmdFinish.IsEnabled = false;
        //                cmdRemark.IsEnabled = false;

        //                //PageManager.Instance.Back();
        //                chkFinish = true;

        //                //Print(_session.WEAVINGLOT, _session.ItemCode);

        //                ScouringDryerCounterModbusManager.Instance.Reset();
        //            }
        //            else
        //            {
        //                result.ShowMessageBox(true);
        //                chkFinish = false;
        //            }
        //        }

        //        return chkFinish;
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString().ShowMessageBox(true);
        //        return false;
        //    }
        //}

        #endregion

        #region Finish

        private bool Finish()
        {
            bool chkFinish = false;

            try
            {
                string FINISHLOT = txtFINISHINGLOT.Text;
                string operatorid = txtOperator.Text;
                string flag = "F";
                decimal? length1 = null;
                decimal? length2 = null;
                decimal? length3 = null;
                decimal? length4 = null;
                decimal? length5 = null;
                decimal? length6 = null;
                decimal? length7 = null;

                if (txtLength1.Text != "")
                    length1 = decimal.Parse(txtLength1.Text);

                if (txtLength2.Text != "")
                    length2 = decimal.Parse(txtLength2.Text);

                if (txtLength3.Text != "")
                    length3 = decimal.Parse(txtLength3.Text);

                if (txtLength4.Text != "")
                    length4 = decimal.Parse(txtLength4.Text);

                if (txtLength5.Text != "")
                    length5 = decimal.Parse(txtLength5.Text);

                if (txtLength6.Text != "")
                    length6 = decimal.Parse(txtLength6.Text);

                if (txtLength7.Text != "")
                    length7 = decimal.Parse(txtLength7.Text);

                CalTotalLength();

                if (txtWIDTH_BE_HEATActual.Text != "")
                    _session.WIDTHBEHEAT = decimal.Parse(txtWIDTH_BE_HEATActual.Text);

                if (txtACCPRESUREActual.Text != "")
                    _session.ACCPRESURE = decimal.Parse(txtACCPRESUREActual.Text);

                if (txtASSTENSIONActual.Text != "")
                    _session.ASSTENSION = decimal.Parse(txtASSTENSIONActual.Text);

                if (txtACCARIDENSERActual.Text != "")
                    _session.ACCARIDENSER = decimal.Parse(txtACCARIDENSERActual.Text);

                if (txtCHIFROTActual.Text != "")
                    _session.CHIFROT = decimal.Parse(txtCHIFROTActual.Text);

                if (txtCHIREARActual.Text != "")
                    _session.CHIREAR = decimal.Parse(txtCHIREARActual.Text);

                if (txtSTEAMPRESSUREActual.Text != "")
                    _session.STEAMPRESURE = decimal.Parse(txtSTEAMPRESSUREActual.Text);

                if (txtDRYERUPCIRCUFANActual.Text != "")
                    _session.DRYCIRCUFAN = decimal.Parse(txtDRYERUPCIRCUFANActual.Text);

                if (txtEXHAUSTFANActual.Text != "")
                    _session.EXHAUSTFAN = decimal.Parse(txtEXHAUSTFANActual.Text);

                if (txtWIDTH_AF_HEATActual.Text != "")
                    _session.WIDTHAFHEAT = decimal.Parse(txtWIDTH_AF_HEATActual.Text);

                #region HUMIDITY_AF
                if (txtHUMIDITY_AF.Text != ""
                    && txtHUMIDITY_AF.Text != _session.HUMIDITY_AF.ToString())
                {
                    _session.HUMIDITY_AF = decimal.Parse(txtHUMIDITY_AF.Text);
                }
                #endregion

                #region HUMIDITY_BF
                if (txtHUMIDITY_BF.Text != ""
                    && txtHUMIDITY_BF.Text != _session.HUMIDITY_BF.ToString())
                {
                    _session.HUMIDITY_BF = decimal.Parse(txtHUMIDITY_BF.Text);
                }
                #endregion

                #region WEAVLENGTH

                try
                {
                    if (txtLength.Text != "")
                    {
                        _session.WEAVLENGTH = decimal.Parse(txtLength.Text);
                    }
                }
                catch
                {
                    _session.WEAVLENGTH = 0;
                }

                #endregion

                if (cbShift.SelectedValue != null)
                {
                    _session.OPERATOR_GROUP = cbShift.SelectedValue.ToString();
                }

                if (FINISHLOT != "" && operatorid != "" && length1 != null)
                {
                    _session.FINISHLOT = FINISHLOT;
                    _session.Operator = operatorid;
                    _session.Flag = flag;

                    //if (chkManualPLC.IsChecked == true)
                    //{

                    if (!string.IsNullOrEmpty(txtHOTPV.Text))
                    {
                        try
                        {
                            decimal hot = decimal.Parse(txtHOTPV.Text);
                            _session.HOTFLUE = hot;
                            _session.HOTFLUE_PV = hot;
                            _session.HOTFLUE_SP = hot;
                        }
                        catch
                        {
                            _session.HOTFLUE = 0;
                            _session.HOTFLUE_PV = 0;
                            _session.HOTFLUE_SP = 0;
                        }

                        _session.HOTFLUE = decimal.Parse(txtHOTPV.Text);
                    }
                    if (!string.IsNullOrEmpty(txtSPEED.Text))
                    {
                        try
                        {
                            decimal speed = decimal.Parse(txtSPEED.Text);
                            _session.SPEED = speed;
                            _session.SPEED_PV = speed;
                            _session.SPEED_SP = speed;
                        }
                        catch
                        {
                            _session.SPEED = 0;
                            _session.SPEED_PV = 0;
                            _session.SPEED_SP = 0;
                        }
                    }

                    //}

                    _session.ENDDATE = DateTime.Now;
                    _session.CONDITIONBY = operatorid;
                    _session.CONDITONDATE = DateTime.Now;

                    _session.LENGTH1 = length1;
                    _session.LENGTH2 = length2;
                    _session.LENGTH3 = length3;
                    _session.LENGTH4 = length4;
                    _session.LENGTH5 = length5;
                    _session.LENGTH6 = length6;
                    _session.LENGTH7 = length7;

                    string result = _session.FINISHING_UPDATEDRYERDATAFinishing();

                    if (string.IsNullOrEmpty(result) == true)
                    {
                        txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
                        cmdFinish.IsEnabled = false;
                        cmdRemark.IsEnabled = false;

                        //PageManager.Instance.Back();
                        chkFinish = true;

                        //Print(_session.WEAVINGLOT, _session.ItemCode);

                        ScouringDryerCounterModbusManager.Instance.Reset();
                    }
                    else
                    {
                        result.ShowMessageBox(true);
                        chkFinish = false;
                    }
                }

                return chkFinish;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }

        #endregion

        #region CalTotalLength
        private void CalTotalLength()
        {
            try
            {
                decimal? length1 = 0;
                decimal? length2 = 0;
                decimal? length3 = 0;
                decimal? length4 = 0;
                decimal? length5 = 0;
                decimal? length6 = 0;
                decimal? length7 = 0;

                if (txtLength1.Text != "")
                    length1 = decimal.Parse(txtLength1.Text);

                if (txtLength2.Text != "")
                    length2 = decimal.Parse(txtLength2.Text);

                if (txtLength3.Text != "")
                    length3 = decimal.Parse(txtLength3.Text);

                if (txtLength4.Text != "")
                    length4 = decimal.Parse(txtLength4.Text);

                if (txtLength5.Text != "")
                    length5 = decimal.Parse(txtLength5.Text);

                if (txtLength6.Text != "")
                    length6 = decimal.Parse(txtLength6.Text);

                if (txtLength7.Text != "")
                    length7 = decimal.Parse(txtLength7.Text);


                txtTotalLength.Text = MathEx.Round((length1.Value + length2.Value + length3.Value + length4.Value + length5.Value + length6.Value + length7.Value), 2).ToString();
            }
            catch
            {
                txtTotalLength.Text = "0";
            }
        }
        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string WEAVINGLOT, string itm_code, string finishingLot)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "ScouringDryer";

                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = itm_code;
                ConmonReportService.Instance.FinishingLot = finishingLot;


                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string WEAVINGLOT, string itm_code, string finishingLot)
        {
            try
            {
                // ConmonReportService

                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = itm_code;
                ConmonReportService.Instance.FinishingLot = finishingLot;


                ConmonReportService.Instance.ReportName = "ScouringDryer";

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region D365_FN
        private void D365_FN(string _FINISHINGLOT, string _WEAVINGLOT, string _PROCESS)
        {
            try
            {
                P_FINISHINGLOT = _FINISHINGLOT;
                P_WEAVINGLOT = _WEAVINGLOT;
                P_PROCESS = _PROCESS;

                if (!string.IsNullOrEmpty(P_FINISHINGLOT) && !string.IsNullOrEmpty(P_WEAVINGLOT) && !string.IsNullOrEmpty(P_PROCESS))
                {
                    if (D365_FN_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            if (D365_FN_ISH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_FN_ISL(HEADERID) == true)
                                    {
                                        if (D365_FN_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_FN_OPL(HEADERID) == true)
                                                {
                                                    if (D365_FN_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_FN_OUL(HEADERID) == true)
                                                            {
                                                                "Send D365 complete".Info();
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
                    if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                    {
                        "Finishing Lot is null".Info();
                    }
                    else if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                    {
                        "Weaving Lot is null".Info();
                    }
                    else if (!string.IsNullOrEmpty(P_PROCESS))
                    {
                        "Process is null".Info();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }
        #endregion

        #region D365_FN_BPO
        private bool D365_FN_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_BPOData> results = new List<ListD365_FN_BPOData>();

                results = D365DataService.Instance.D365_FN_BPO(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_BPO Row = 0".Info();
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

        #region D365_FN_ISH
        private bool D365_FN_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_FN_ISHData> results = new List<D365_FN_ISHData>();

                results = D365DataService.Instance.D365_FN_ISH(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_ISH Row = 0".Info();
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

        #region D365_FN_ISL
        private bool D365_FN_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_ISLData> results = new List<ListD365_FN_ISLData>();

                results = D365DataService.Instance.D365_FN_ISL(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_ISL Row = 0".Info();
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

        #region D365_FN_OPH
        private bool D365_FN_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_FN_OPHData> results = new List<D365_FN_OPHData>();

                results = D365DataService.Instance.D365_FN_OPH(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OPH Row = 0".Info();
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

        #region D365_FN_OPL
        private bool D365_FN_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_OPLData> results = new List<ListD365_FN_OPLData>();

                results = D365DataService.Instance.D365_FN_OPL(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OPL Row = 0".Info();
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

        #region D365_FN_OUH
        private bool D365_FN_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_FN_OUHData> results = new List<D365_FN_OUHData>();

                results = D365DataService.Instance.D365_FN_OUH(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OUH Row = 0".Info();
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

        #region D365_FN_OUL
        private bool D365_FN_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_OULData> results = new List<ListD365_FN_OULData>();

                results = D365DataService.Instance.D365_FN_OUL(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OUL Row = 0".Info();
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

        #region Setup

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="session">The inspection session.</param>
        /// <param name="suspendData">The suspend data.</param>
        public void Setup(FinishingSession session,
            Domains.INS_GETMCSUSPENDDATAResult suspendData)
        {
            _session = session;
            // Init Session
            InitSession();
            if (null != suspendData && null != _session)
            {
                session.Resume(suspendData.FINISHINGLOT,
                    suspendData.ITEMCODE, suspendData.INSPECTIONLOT,
                    suspendData.CUSTOMERID,
                    suspendData.INSPECTIONID);
            }

            if (null != _session)
            {
                //SetupOperatorAndMC(session.CurrentUser.OperatorId, session.Machine.DisplayName, session.Machine.MCId);
                SetupOperatorAndMC(session.CurrentUser.OperatorId, "Dryer", session.Machine.MCId);
            }
        }

        #endregion

        #region SetupOperatorAndMC

        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        /// <param name="mc"></param>
        public void SetupOperatorAndMC(string opera, string mcName, string mcID)
        {
            if (null != opera)
            {
                txtOperator.Text = opera;
            }
            else
            {
                txtOperator.Text = "-";
            }
            if (null != mcName)
            {
                txtMCName.Text = mcName;
            }
            else
            {
                txtMCName.Text = "-";
            }

            txtScouringNo.Visibility = System.Windows.Visibility.Collapsed;

            if (null != mcID)
            {
                txtScouringNo.Text = mcID;
            }
            else
            {
                txtScouringNo.Text = "-";
            }
        }

        #endregion

        #endregion

    }
}
