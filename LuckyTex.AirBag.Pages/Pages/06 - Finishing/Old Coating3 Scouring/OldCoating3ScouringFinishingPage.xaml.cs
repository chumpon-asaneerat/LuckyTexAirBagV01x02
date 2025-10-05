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
    /// Interaction logic for OldCoating3ScouringFinishingPage.xaml
    /// </summary>
    public partial class OldCoating3ScouringFinishingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public OldCoating3ScouringFinishingPage()
        {
            InitializeComponent();

            LoadShift();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Coating3ScouringMachineConfig;

            EnabledCon(true);

            rbMassProduction.Visibility = System.Windows.Visibility.Collapsed;
            rbTest.Visibility = System.Windows.Visibility.Collapsed;
            rbGuide.Visibility = System.Windows.Visibility.Collapsed;
            txtCustomer.IsEnabled = false;
            txtItemGoods.IsEnabled = false;
            txtItemWeaving.IsEnabled = false;
            txtLot.IsEnabled = false;
            txtLength.IsEnabled = false;
            txtFINISHINGLOT.IsEnabled = false;
            txtStartTime.IsEnabled = false;
            txtPreparingTime.IsEnabled = false;

            // ปรับเพิ่มเป็นห้ามแก้ไข
            txtSPEED.IsEnabled = false;
            txtSATPV.IsEnabled = false;
            txtWASH1PV.IsEnabled = false;
            txtWASH2PV.IsEnabled = false;
            txtHOTPV.IsEnabled = false;

            txtTEMP1PV.IsEnabled = false;
            txtTEMP2PV.IsEnabled = false;
            txtTEMP3PV.IsEnabled = false;
            txtTEMP4PV.IsEnabled = false;
            txtTEMP5PV.IsEnabled = false;
            txtTEMP6PV.IsEnabled = false;
            txtTEMP7PV.IsEnabled = false;
            txtTEMP8PV.IsEnabled = false;
            txtTEMP9PV.IsEnabled = false;
            txtTEMP10PV.IsEnabled = false;

            txtClothHumidity.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITYBFSpecification.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITY_BF.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Internal Variables

        private FinishingSession _session = new FinishingSession();
        private bool chkLoad = true;
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
            if (chkLoad == false)
            {
                PageManager.Instance.Back();
            }
            else
            {
                chkManual.IsChecked = false;
                CheckManual();
                
                txtLength1.Focus();
                txtLength1.SelectAll();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Coating3ScouringCounterModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<FinishingCounter>(Coating3ScouringCounter_ReadCompleted);
            Coating3ScouringCounterModbusManager.Instance.Shutdown();
        }

        void Coating3ScouringCounter_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<FinishingCounter> e)
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
                txtWIDTH_AFActual.Focus();
                txtWIDTH_AFActual.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        private void txtWIDTH_AFActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPIN2PINActual.Focus();
                txtPIN2PINActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtPIN2PINActual_KeyDown(object sender, KeyEventArgs e)
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
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    cmdFinish.Focus();
                    e.Handled = true;
                }
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

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdShowLength_Click

        private void cmdShowLength_Click(object sender, RoutedEventArgs e)
        {
            ShowLength();
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
                        D365_FN(txtFINISHINGLOT.Text, _session.WEAVINGLOT, "Scouring");

                        string scouringNo = txtScouringNo.Text;
                        Print(_session.WEAVINGLOT, _session.ItemCode, scouringNo, txtFINISHINGLOT.Text);
                    }

                    //if (txtMAINFRAMEWIDTHActual.IsEnabled == true)
                    //    EnabledCon(false);
                }
                else
                {
                    "Can't Finish Please check data isn't null".ShowMessageBox(true);

                    //if (txtMAINFRAMEWIDTHActual.IsEnabled == false)
                    //    EnabledCon(true);
                }
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
                                Preview(_session.WEAVINGLOT, _session.ItemCode, scouringNo, txtFINISHINGLOT.Text);
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
                    if (txtFINISHINGLOT.Text != "" && scouringNo != "")
                        Preview(_session.WEAVINGLOT, _session.ItemCode, scouringNo, txtFINISHINGLOT.Text);
                }
            }
        }

        #endregion

        #region cmdRemark_Click
        private void cmdRemark_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                string remark = _session.GetScouringRemark(txtScouringNo.Text, "P");
                if (null == remark)
                    remark = string.Empty;
                RemarkInfo remarkInfo = this.ShowRemarkBox(remark);
                if (null != remarkInfo)
                {
                    string FINISHLOT = txtFINISHINGLOT.Text;
                    string ItemCode = _session.ItemCode;
                    _session.AddScouringRemark(FINISHLOT, ItemCode, remarkInfo.Remark);
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
            txtSPEEDSpecification.Text = "";
            txtMAINFRAMEWIDTHSpecification.Text = "";
            txtWIDTH_BESpecification.Text = "";
            txtWIDTH_AFSpecification.Text = "";
            txtPIN2PINSpecification.Text = "";

            txtSATURATOR_CHEMSpecification.Text = "";
            txtWASHING1Specification.Text = "";
            txtWASHING2Specification.Text = "";
            txtHOTFLUESpecification.Text = "";
            txtROOMTEMP.Text = "";
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
                //rbTest.IsChecked = false;
                txtItemWeaving.Text = "";
                txtLot.Text = "";
                txtLength.Text = "";

                txtSATURATOR_CHEMSpecification.Text = "";
                txtSATPV.Text = "";
                txtWASHING1Specification.Text = "";
                txtWASH1PV.Text = "";
                txtWASHING2Specification.Text = "";
                txtWASH2PV.Text = "";
                txtHOTFLUESpecification.Text = "";
                txtHOTPV.Text = "";
                txtROOMTEMP.Text = "";

                txtSPEEDSpecification.Text = "";
                txtSPEED.Text = "";
                txtMAINFRAMEWIDTHSpecification.Text = "";
                txtMAINFRAMEWIDTHActual.Text = "";
                txtWIDTH_BESpecification.Text = "";
                txtWIDTH_BEActual.Text = "";
                txtWIDTH_AFSpecification.Text = "";
                txtWIDTH_AFActual.Text = "";
                txtPIN2PINSpecification.Text = "";
                txtPIN2PINActual.Text = "";
                txtHUMIDITY_BF.Text = "";
                txtHUMIDITY_AF.Text = "";
                txtTEMP1PV.Text = "";
                txtTEMP2PV.Text = "";
                txtTEMP3PV.Text = "";
                txtTEMP4PV.Text = "";
                txtTEMP5PV.Text = "";
                txtTEMP6PV.Text = "";
                txtTEMP7PV.Text = "";
                txtTEMP8PV.Text = "";
                txtTEMP9PV.Text = "";
                txtTEMP10PV.Text = "";

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
            txtMAINFRAMEWIDTHActual.IsEnabled = chkManual;
            txtWIDTH_BEActual.IsEnabled = chkManual;
            //txtWIDTH_AFActual.IsEnabled = chkManual;
            //txtPIN2PINActual.IsEnabled = chkManual;
        }
        #endregion

        #region CheckManual
        private void CheckManual()
        {
            if (chkManual.IsChecked == false)
            {
                if (mcStatus == true)
                {
                    Coating3ScouringCounterModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<FinishingCounter>(Coating3ScouringCounter_ReadCompleted);
                    Coating3ScouringCounterModbusManager.Instance.Start();

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
                Coating3ScouringCounterModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<FinishingCounter>(Coating3ScouringCounter_ReadCompleted);
                Coating3ScouringCounterModbusManager.Instance.Shutdown();

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

            if (txtMAINFRAMEWIDTHActual.Text == "")
            {
                return false;
            }

            #endregion

            if (txtWIDTH_AFActual.Text == "")
            {
                return false;
            }
            if (txtPIN2PINActual.Text == "")
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

        private void LoadFinishing(string itm_code, string ScouringNo)
        {
            try
            {
                List<FINISHING_GETSCOURINGCONDITIONData> items = _session.GetFINISHING_GETSCOURINGCONDITION(itm_code, ScouringNo);

                if (items != null && items.Count > 0)
                {

                    #region SATURATOR_CHEM

                    if (items[0].SATURATOR_CHEM != null && items[0].SATURATOR_CHEM_MARGIN != null)
                    {
                        txtSATURATOR_CHEMSpecification.Text = (items[0].SATURATOR_CHEM.Value.ToString("#,##0.##") + " ± " + items[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtSATURATOR_CHEMSpecification.Text = "";
                    }

                    #endregion

                    #region WASHING1

                    if (items[0].WASHING1 != null && items[0].WASHING1_MARGIN != null)
                    {
                        txtWASHING1Specification.Text = (items[0].WASHING1.Value.ToString("#,##0.##") + " ± " + items[0].WASHING1_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtWASHING1Specification.Text = "";
                    }

                    #endregion

                    #region WASHING2

                    if (items[0].WASHING2 != null && items[0].WASHING2_MARGIN != null)
                    {
                        txtWASHING2Specification.Text = (items[0].WASHING2.Value.ToString("#,##0.##") + " ± " + items[0].WASHING2_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtWASHING2Specification.Text = "";
                    }

                    #endregion

                    #region HOTFLUE

                    if (items[0].HOTFLUE != null && items[0].HOTFLUE_MARGIN != null)
                    {
                        txtHOTFLUESpecification.Text = (items[0].HOTFLUE.Value.ToString("#,##0.##") + " ± " + items[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtHOTFLUESpecification.Text = "";
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

                    #region MAINFRAMEWIDTH

                    if (items[0].MAINFRAMEWIDTH != null && items[0].MAINFRAMEWIDTH_MARGIN != null)
                    {
                        txtMAINFRAMEWIDTHSpecification.Text = (items[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##") + " ± " + items[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtMAINFRAMEWIDTHSpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_BE

                    if (items[0].WIDTH_BE != null && items[0].WIDTH_BE_MARGIN != null)
                    {
                        //txtWIDTH_BESpecification.Text = (items[0].WIDTH_BE.Value.ToString("#,##0.##") + " ± " + items[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##"));
                        txtWIDTH_BESpecification.Text = ("( " + items[0].WIDTH_BE.Value.ToString("#,##0.##") + " )");
                    }
                    else
                    {
                        txtWIDTH_BESpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_AF

                    if (items[0].WIDTH_AF != null && items[0].WIDTH_AF_MARGIN != null)
                    {
                        txtWIDTH_AFSpecification.Text = (items[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + items[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtWIDTH_AFSpecification.Text = "";
                    }

                    #endregion

                    #region PIN2PIN

                    if (items[0].PIN2PIN != null && items[0].PIN2PIN_MARGIN != null)
                    {
                        txtPIN2PINSpecification.Text = (items[0].PIN2PIN.Value.ToString("#,##0.##") + " ± " + items[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtPIN2PINSpecification.Text = "";
                    }

                    #endregion

                    #region ROOMTEMP

                    if (items[0].ROOMTEMP != null && items[0].ROOMTEMP_MARGIN != null)
                    {
                        txtROOMTEMP.Text = (items[0].ROOMTEMP.Value.ToString("#,##0.##") + " ± " + items[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtROOMTEMP.Text = "";
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
                }
                else
                {
                    string msg = "This Item Code " + itm_code + " Can not be used in this MC.";

                    msg.ShowMessageBox(false);

                    txtItemGoods.Focus();

                    ClearFinishing();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadFinishing_GetScouring

        private void LoadFinishing_GetScouring(string mcno, string flag)
        {
            try
            {
                List<FINISHING_GETSCOURINGDATA> items = _session.GetFINISHING_GETSCOURINGDATA(mcno, flag);

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

                    txtLength.Text = items[0].WEAVINGLENGTH.ToString();
                    
                    txtFINISHINGLOT.Text = items[0].FINISHINGLOT;

                    txtStartTime.Text = items[0].CONDITIONDATE.Value.ToString("dd/MM/yy HH:mm");
                    txtPreparingTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");

                    _session.STARTDATE = items[0].STARTDATE;
                    _session.CONDITONDATE = items[0].CONDITIONDATE;

                    //txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");

                    txtMAINFRAMEWIDTHActual.Text = items[0].MAINFRAMEWIDTH.ToString();
                    txtWIDTH_BEActual.Text = items[0].WIDTH_BE.ToString();
                    txtWIDTH_AFActual.Text = items[0].WIDTH_AF.ToString();
                    txtPIN2PINActual.Text = items[0].PIN2PIN.ToString();

                    txtSATPV.Text = items[0].SATURATOR_CHEM_PV.ToString();
                    txtWASH1PV.Text = items[0].WASHING1_PV.ToString();
                    txtWASH2PV.Text = items[0].WASHING2_PV.ToString();

                    txtHOTPV.Text = items[0].HOTFLUE_PV.ToString();
                    txtSPEED.Text = items[0].SPEED_PV.ToString();

                    txtTEMP1PV.Text = items[0].TEMP1_PV.ToString();
                    txtTEMP2PV.Text = items[0].TEMP2_PV.ToString();
                    txtTEMP3PV.Text = items[0].TEMP3_PV.ToString();
                    txtTEMP4PV.Text = items[0].TEMP4_PV.ToString();
                    txtTEMP5PV.Text = items[0].TEMP5_PV.ToString();
                    txtTEMP6PV.Text = items[0].TEMP6_PV.ToString();
                    txtTEMP7PV.Text = items[0].TEMP7_PV.ToString();
                    txtTEMP8PV.Text = items[0].TEMP8_PV.ToString();
                    txtTEMP9PV.Text = items[0].TEMP9_PV.ToString();
                    txtTEMP10PV.Text = items[0].TEMP10_PV.ToString();

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

                    _session.SATURATOR_PV = items[0].SATURATOR_CHEM_PV;
                    _session.SATURATOR_SP = items[0].SATURATOR_CHEM_SP;
                    _session.WASHING1_PV = items[0].WASHING1_PV;
                    _session.WASHING1_SP = items[0].WASHING1_SP;
                    _session.WASHING2_PV = items[0].WASHING2_PV;
                    _session.WASHING2_SP = items[0].WASHING2_SP;
                    _session.HOTFLUE_PV = items[0].HOTFLUE_PV;
                    _session.HOTFLUE_SP = items[0].HOTFLUE_SP;
                    _session.TEMP1_PV = items[0].TEMP1_PV;
                    _session.TEMP1_SP = items[0].TEMP1_SP;
                    _session.TEMP2_PV = items[0].TEMP2_PV;
                    _session.TEMP2_SP = items[0].TEMP2_SP;
                    _session.TEMP3_PV = items[0].TEMP3_PV;
                    _session.TEMP3_SP = items[0].TEMP3_SP;
                    _session.TEMP4_PV = items[0].TEMP4_PV;
                    _session.TEMP4_SP = items[0].TEMP4_SP;
                    _session.TEMP5_PV = items[0].TEMP5_PV;
                    _session.TEMP5_SP = items[0].TEMP5_SP;
                    _session.TEMP6_PV = items[0].TEMP6_PV;
                    _session.TEMP6_SP = items[0].TEMP6_SP;
                    _session.TEMP7_PV = items[0].TEMP7_PV;
                    _session.TEMP7_SP = items[0].TEMP7_SP;
                    _session.TEMP8_PV = items[0].TEMP8_PV;
                    _session.TEMP8_SP = items[0].TEMP8_SP;

                    _session.TEMP9_PV = items[0].TEMP9_PV;
                    _session.TEMP9_SP = items[0].TEMP9_SP;
                    _session.TEMP10_PV = items[0].TEMP10_PV;
                    _session.TEMP10_SP = items[0].TEMP10_SP;

                    _session.SPEED_PV = items[0].SPEED_PV;
                    _session.SPEED_SP = items[0].SPEED_SP;

                    _session.MAINFRAMEWIDTH = items[0].MAINFRAMEWIDTH;
                    _session.WIDTH_BE = items[0].WIDTH_BE;
                    _session.WIDTH_AF = items[0].WIDTH_AF;
                    _session.PIN2PIN = items[0].PIN2PIN;

                    _session.HUMIDITY_AF = items[0].HUMIDITY_AF;
                    _session.HUMIDITY_BF = items[0].HUMIDITY_BF;
                    _session.OPERATOR_GROUP = items[0].OPERATOR_GROUP;

                    chkLoad = true;
                }
                else
                {
                    string msg = "Can't Load Scouring Coat3";

                    msg.ShowMessageBox(false);

                    chkLoad = false;

                    //txtCustomer.Text = "";
                    //txtItemGoods.Text = "";

                    //rbMassProduction.Visibility = System.Windows.Visibility.Visible;
                    //rbTest.Visibility = System.Windows.Visibility.Collapsed;

                    //rbMassProduction.IsChecked = true;
                    //rbTest.IsChecked = false;

                    //txtItemWeaving.Text = "";
                    //txtLot.Text = "";
                    //txtLength.Text = "";
                    //txtFINISHINGLOT.Text = "";
                    //txtStartTime.Text = "";

                    //txtMAINFRAMEWIDTHActual.Text = "";
                    //txtWIDTH_BEActual.Text = "";
                    //txtWIDTH_AFActual.Text = "";
                    //txtPIN2PINActual.Text = "";

                    //txtSATPV.Text = "";
                    //txtWASH1PV.Text = "";
                    //txtWASH2PV.Text = "";

                    //txtHOTPV.Text = "";
                    //txtSPEED.Text = "";

                    //txtTEMP1PV.Text = "";
                    //txtTEMP2PV.Text = "";
                    //txtTEMP3PV.Text = "";
                    //txtTEMP4PV.Text = "";
                    //txtTEMP5PV.Text = "";
                    //txtTEMP6PV.Text = "";
                    //txtTEMP6PV.Text = "";
                    //txtTEMP7PV.Text = "";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

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

                if (txtWIDTH_AFActual.Text != ""
                    && txtWIDTH_AFActual.Text != _session.WIDTH_AF.ToString())
                    _session.WIDTH_AF = decimal.Parse(txtWIDTH_AFActual.Text);

                if (txtPIN2PINActual.Text != ""
                    && txtPIN2PINActual.Text != _session.PIN2PIN.ToString())
                    _session.PIN2PIN = decimal.Parse(txtPIN2PINActual.Text);

                if (txtMAINFRAMEWIDTHActual.Text != "")
                    _session.MAINFRAMEWIDTH = decimal.Parse(txtMAINFRAMEWIDTHActual.Text);

                if (txtWIDTH_BEActual.Text != "")
                    _session.WIDTH_BE = decimal.Parse(txtWIDTH_BEActual.Text);

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

                if (!string.IsNullOrEmpty(txtSATPV.Text))
                {
                    _session.SAT = decimal.Parse(txtSATPV.Text);
                }
                if (!string.IsNullOrEmpty(txtWASH1PV.Text))
                {
                    _session.WASHING1 = decimal.Parse(txtWASH1PV.Text);
                }
                if (!string.IsNullOrEmpty(txtWASH2PV.Text))
                {
                    _session.WASHING2 = decimal.Parse(txtWASH2PV.Text);
                }
                if (!string.IsNullOrEmpty(txtHOTPV.Text))
                {
                    _session.HOTFLUE = decimal.Parse(txtHOTPV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP1PV.Text))
                {
                    _session.TEMP1 = decimal.Parse(txtTEMP1PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP2PV.Text))
                {
                    _session.TEMP2 = decimal.Parse(txtTEMP2PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP3PV.Text))
                {
                    _session.TEMP3 = decimal.Parse(txtTEMP3PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP4PV.Text))
                {
                    _session.TEMP4 = decimal.Parse(txtTEMP4PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP5PV.Text))
                {
                    _session.TEMP5 = decimal.Parse(txtTEMP5PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP6PV.Text))
                {
                    _session.TEMP6 = decimal.Parse(txtTEMP6PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP7PV.Text))
                {
                    _session.TEMP7 = decimal.Parse(txtTEMP7PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP8PV.Text))
                {
                    _session.TEMP8 = decimal.Parse(txtTEMP8PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP9PV.Text))
                {
                    _session.TEMP9 = decimal.Parse(txtTEMP9PV.Text);
                }
                if (!string.IsNullOrEmpty(txtTEMP10PV.Text))
                {
                    _session.TEMP10 = decimal.Parse(txtTEMP10PV.Text);
                }
                if (!string.IsNullOrEmpty(txtSPEED.Text))
                {
                    _session.SPEED = decimal.Parse(txtSPEED.Text);
                }

                if (cbShift.SelectedValue != null)
                {
                    _session.OPERATOR_GROUP = cbShift.SelectedValue.ToString();
                }

                if (FINISHLOT != "" && operatorid != "" && length1 != null)
                {
                    _session.FINISHLOT = FINISHLOT;
                    _session.Operator = operatorid;
                    _session.Flag = flag;

                    _session.LENGTH1 = length1;
                    _session.LENGTH2 = length2;
                    _session.LENGTH3 = length3;
                    _session.LENGTH4 = length4;
                    _session.LENGTH5 = length5;
                    _session.LENGTH6 = length6;
                    _session.LENGTH7 = length7;

                    string result = _session.FINISHING_UPDATESCOURINGFinishing();

                    if (string.IsNullOrEmpty(result) == true)
                    {
                        txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
                        cmdFinish.IsEnabled = false;
                        cmdRemark.IsEnabled = false;
                        //PageManager.Instance.Back();

                        chkFinish = true;

                        //string scouringNo = txtScouringNo.Text;
                        //Print(_session.WEAVINGLOT, _session.ItemCode, scouringNo);

                        Coating3ScouringCounterModbusManager.Instance.Reset();
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

        private void Print(string WEAVINGLOT, string itm_code, string ScouringNo, string finishingLot)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "Coating3Scouring";
                ConmonReportService.Instance.ScouringNo = ScouringNo;
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

        private void Preview(string WEAVINGLOT, string itm_code, string ScouringNo, string finishingLot)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ScouringNo = ScouringNo;
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = itm_code;
                ConmonReportService.Instance.FinishingLot = finishingLot;


                ConmonReportService.Instance.ReportName = "Coating3Scouring";

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
                SetupOperatorAndMC(session.CurrentUser.OperatorId, session.Machine.DisplayName, session.Machine.MCId);
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

                if (mcID != "")
                    LoadFinishing_GetScouring(mcID, "P");
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
