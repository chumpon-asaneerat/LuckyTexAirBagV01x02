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
    /// Interaction logic for Coating3FinishingPage.xaml
    /// </summary>
    public partial class Coating3FinishingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating3FinishingPage()
        {
            InitializeComponent();

            LoadShift();
            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Coating3MachineConfig;

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

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

        private FinishingSession _session = new FinishingSession();
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
            CoatingCounter3ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<FinishingCounter>(CoatingCounter3_ReadCompleted);
            CoatingCounter3ModbusManager.Instance.Shutdown();
        }

        void CoatingCounter3_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<FinishingCounter> e)
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
                FINISHING_COATINGDATABYLOT(txtScouringNo.Text, txtLot.Text);
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
                txtWIDTHCOATActual.Focus();
                txtWIDTHCOATActual.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        private void txtBE_COATWIDTHActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFan110Actual.Focus();
                txtFan110Actual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFan110Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN1_6Actual.Focus();
                txtEXFAN1_6Actual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXFAN1_6Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN2_5Actual.Focus();
                txtEXFAN2_5Actual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXFAN2_5Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtANGLEKNIFEActual.Focus();
                txtANGLEKNIFEActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtANGLEKNIFEActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBLADENOActual.Focus();
                txtBLADENOActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBLADENOActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_UPActual.Focus();
                txtTENSION_UPActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_UPActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_DOWNActual.Focus();
                txtTENSION_DOWNActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_DOWNActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFRAMEWIDTH_FORNActual.Focus();
                txtFRAMEWIDTH_FORNActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFRAMEWIDTH_FORNActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFRAMEWIDTH_TENTERActual.Focus();
                txtFRAMEWIDTH_TENTERActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFRAMEWIDTH_TENTERActual_KeyDown(object sender, KeyEventArgs e)
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
                txtWIDTHCOATActual.Focus();
                txtWIDTHCOATActual.SelectAll();
                e.Handled = true;
            }
        }

        #region txtWIDTHCOATActual_KeyDown
        private void txtWIDTHCOATActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHCOATALLActual.Focus();
                txtWIDTHCOATALLActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWIDTHCOATALLActual_KeyDown
        private void txtWIDTHCOATALLActual_KeyDown(object sender, KeyEventArgs e)
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
        #endregion

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
                if (txtSATPV.IsEnabled == false)
                {
                    cmdFinish.Focus();
                    e.Handled = true;
                }
                else
                {
                    txtSATPV.Focus();
                    txtSATPV.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        private void txtSATPV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWASH1PV.Focus();
                txtWASH1PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWASH1PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWASH2PV.Focus();
                txtWASH2PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWASH2PV_KeyDown(object sender, KeyEventArgs e)
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
                txtTEMP1PV.Focus();
                txtTEMP1PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP1PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP2PV.Focus();
                txtTEMP2PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP2PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP3PV.Focus();
                txtTEMP3PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP3PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP4PV.Focus();
                txtTEMP4PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP4PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP5PV.Focus();
                txtTEMP5PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP5PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP6PV.Focus();
                txtTEMP6PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP6PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP7PV.Focus();
                txtTEMP7PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP7PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP8PV.Focus();
                txtTEMP8PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP8PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP9PV.Focus();
                txtTEMP9PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP9PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP10PV.Focus();
                txtTEMP10PV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEMP10PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSiliconeA.Focus();
                txtSiliconeA.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSiliconeA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSiliconeB.Focus();
                txtSiliconeB.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSiliconeB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCWL.Focus();
                txtCWL.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCWL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCWC.Focus();
                txtCWC.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCWC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCWR.Focus();
                txtCWR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCWR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdFinish.Focus();
                e.Handled = true;
            }
        }

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
                        D365_FN(txtFINISHINGLOT.Text, _session.WEAVINGLOT, "Coating");

                        string scouringNo = txtScouringNo.Text;
                        Print(_session.WEAVINGLOT, _session.ItemCode, scouringNo, txtFINISHINGLOT.Text);
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
                string remark = _session.GetCoatingRemark(txtScouringNo.Text, "S");
                if (null == remark)
                    remark = string.Empty;
                RemarkInfo remarkInfo = this.ShowRemarkBox(remark);
                if (null != remarkInfo)
                {
                    string FINISHLOT = txtFINISHINGLOT.Text;
                    string ItemCode = _session.ItemCode;
                    _session.AddCoatingRemark(FINISHLOT, ItemCode, remarkInfo.Remark);

                    if (!string.IsNullOrEmpty(remarkInfo.Remark))
                        _session.REMARK = remarkInfo.Remark;
                }
            }
        }
        #endregion

        private void cmdSampling_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                SamplingCoatingWindow window = new SamplingCoatingWindow();

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

        #region ComboBox

        #region cbBLADEDIRECTIONActual_SelectionChanged

        private void cbBLADEDIRECTIONActual_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbBLADEDIRECTIONActual.SelectedValue != null)
                {
                    if (cbBLADEDIRECTIONActual.SelectedValue.ToString() == "R")
                    {
                        img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/R.png", UriKind.Relative));
                    }
                    else if (cbBLADEDIRECTIONActual.SelectedValue.ToString() == "L")
                    {
                        img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/L.png", UriKind.Relative));
                    }
                    else if (cbBLADEDIRECTIONActual.SelectedValue.ToString() == "C")
                    {
                        img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/C.png", UriKind.Relative));
                    }
                }
            }
            catch
            {
                img.Source = new BitmapImage(new Uri("", UriKind.Relative));
            }
        }

        #endregion

        #endregion

        #region private Methods

        #region LoadShift

        private void LoadShift()
        {
            if (cbShift.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C"};

                cbShift.ItemsSource = str;
                cbShift.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadBladedirection
        private void LoadBladedirection()
        {
            if (cbBLADEDIRECTIONActual.ItemsSource == null)
            {
                string[] str = new string[] { "R", "L", "C" };

                cbBLADEDIRECTIONActual.ItemsSource = str;
                cbBLADEDIRECTIONActual.SelectedValue = null;
            }
        }
        #endregion

        #region ClearFinishing

        private void ClearFinishing()
        {
            txtBE_COATWIDTHSpecification.Text = "";
            txtFan110Specification.Text = "";
            txtEXFAN1_6Specification.Text = "";
            txtEXFAN2_5Specification.Text = "";
            txtANGLEKNIFESpecification.Text = "";
            txtBLADENOSpecification.Text = "";

            ImageL.Visibility = System.Windows.Visibility.Collapsed;
            ImageR.Visibility = System.Windows.Visibility.Collapsed;
            ImageC.Visibility = System.Windows.Visibility.Collapsed;

            txtTENSION_UPSpecification.Text = "";
            txtTENSION_DOWNSpecification.Text = "";
            txtFRAMEWIDTH_FORNSpecification.Text = "";
            txtFRAMEWIDTH_TENTERSpecification.Text = "";
            txtSPEEDSpecification.Text = "";
            txtWIDTHCOATSpecification.Text = "";
            txtWIDTHCOATALLSpecification.Text = "";

            txtSATURATOR_CHEMSpecification.Text = "";
            txtWASHING1Specification.Text = "";
            txtWASHING2Specification.Text = "";

            txtHOTFLUESpecification.Text = "";
            txtROOMTEMP.Text = "";

            txtRATIOSILICONE.Text = "";
            txtCOATINGWEIGTH.Text = "";

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

                txtBE_COATWIDTHActual.Text = "";
                txtFan110Actual.Text = "";
                txtEXFAN1_6Actual.Text = "";
                txtEXFAN2_5Actual.Text = "";
                txtANGLEKNIFEActual.Text = "";
                txtBLADENOActual.Text = "";

                cbBLADEDIRECTIONActual.SelectedValue = null;
                img.Source = new BitmapImage(new Uri("", UriKind.Relative));

                txtTENSION_UPActual.Text = "";
                txtTENSION_DOWNActual.Text = "";
                txtFRAMEWIDTH_FORNActual.Text = "";
                txtFRAMEWIDTH_TENTERActual.Text = "";
                txtSPEED.Text = "";
                txtWIDTHCOATActual.Text = "";
                txtWIDTHCOATALLActual.Text = "";
                txtHUMIDITY_BF.Text = "";
                txtHUMIDITY_AF.Text = "";
                txtSATPV.Text = "";
                txtWASH1PV.Text = "";
                txtWASH2PV.Text = "";
                txtHOTPV.Text = "";

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

                txtSiliconeA.Text = "";
                txtSiliconeB.Text = "";
                txtCWL.Text = "";
                txtCWR.Text = "";
                txtCWC.Text = "";

                cbShift.SelectedIndex = 0;

                chkManualPLC.IsChecked = false;
                EnabledCon(false);

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

                txtLot.Focus();
                txtLot.SelectAll();
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
            txtBE_COATWIDTHActual.IsEnabled = chkManual;
            txtFan110Actual.IsEnabled = chkManual;
            txtEXFAN1_6Actual.IsEnabled = chkManual;
            txtEXFAN2_5Actual.IsEnabled = chkManual;
            txtANGLEKNIFEActual.IsEnabled = chkManual;
            txtBLADENOActual.IsEnabled = chkManual;

            txtTENSION_UPActual.IsEnabled = chkManual;
            txtTENSION_DOWNActual.IsEnabled = chkManual;
            txtFRAMEWIDTH_FORNActual.IsEnabled = chkManual;
            txtFRAMEWIDTH_TENTERActual.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;
            //txtWIDTHCOATActual.IsEnabled = chkManual;
            //txtWIDTHCOATALLActual.IsEnabled = chkManual;

            txtSATPV.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;

            txtTEMP1PV.IsEnabled = chkManual;
            txtTEMP2PV.IsEnabled = chkManual;
            txtTEMP3PV.IsEnabled = chkManual;
            txtTEMP4PV.IsEnabled = chkManual;
            txtTEMP5PV.IsEnabled = chkManual;
            txtTEMP6PV.IsEnabled = chkManual;
            txtTEMP7PV.IsEnabled = chkManual;
            txtTEMP8PV.IsEnabled = chkManual;
            txtTEMP9PV.IsEnabled = chkManual;
            txtTEMP10PV.IsEnabled = chkManual;

            txtSiliconeA.IsEnabled = chkManual;
            txtSiliconeB.IsEnabled = chkManual;
            txtCWL.IsEnabled = chkManual;
            txtCWR.IsEnabled = chkManual;
            txtCWC.IsEnabled = chkManual;

            // เพิ่ม 08/12/15
            cbBLADEDIRECTIONActual.IsEnabled = chkManual;
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

            if (txtWIDTHCOATActual.Text == "")
            {
                return false;
            }
            if (txtWIDTHCOATALLActual.Text == "")
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

        private void LoadFinishing(string itm_code, string CoatNo)
        {
            try
            {
                List<FINISHING_GETCOATINGCONDITIONData> items = _session.GetFINISHING_GETCOATINGCONDITION(itm_code, CoatNo);

                if (items != null && items.Count > 0)
                {
                    #region BE_COATWIDTH

                    if (items[0].BE_COATWIDTHMAX != null && items[0].BE_COATWIDTHMIN != null)
                    {
                        //txtBE_COATWIDTHSpecification.Text = (items[0].BE_COATWIDTHMIN.Value.ToString("#,##0.##") + " - " + items[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##"));
                        txtBE_COATWIDTHSpecification.Text = ("( " + items[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##") + " )");
                    }
                    else
                    {
                        txtBE_COATWIDTHSpecification.Text = "";
                    }

                    #endregion

                    #region FANRPM

                    if (items[0].FANRPM != null && items[0].FANRPM_MARGIN != null)
                    {
                        txtFan110Specification.Text = (items[0].FANRPM.Value.ToString("#,##0.##") + " ± " + items[0].FANRPM_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtFan110Specification.Text = "";
                    }

                    #endregion

                    #region EXFAN_FRONT_BACK

                    if (items[0].EXFAN_FRONT_BACK != null && items[0].EXFAN_MARGIN != null)
                    {
                        txtEXFAN1_6Specification.Text = (items[0].EXFAN_FRONT_BACK.Value.ToString("#,##0.##") + " ± " + items[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtEXFAN1_6Specification.Text = "";
                    }

                    #endregion

                    #region EXFAN_MIDDLE

                    if (items[0].EXFAN_MIDDLE != null && items[0].EXFAN_MARGIN != null)
                    {
                        txtEXFAN2_5Specification.Text = (items[0].EXFAN_MIDDLE.Value.ToString("#,##0.##") + " ± " + items[0].EXFAN_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtEXFAN2_5Specification.Text = "";
                    }

                    #endregion

                    #region ANGLEKNIFE

                    if (items[0].ANGLEKNIFE != null)
                        txtANGLEKNIFESpecification.Text = items[0].ANGLEKNIFE.Value.ToString("#,##0.##");
                    else
                        txtANGLEKNIFESpecification.Text = "";

                    #endregion

                    txtBLADENOSpecification.Text = items[0].BLADENO;

                    #region BLADEDIRECTION

                    if (items[0].BLADEDIRECTION != "")
                    {
                        if (items[0].BLADEDIRECTION == "L")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Visible;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;

                            cbBLADEDIRECTIONActual.SelectedValue = "L";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/L.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "R")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Visible;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;

                            cbBLADEDIRECTIONActual.SelectedValue = "R";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/R.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "C")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Visible;

                            cbBLADEDIRECTIONActual.SelectedValue = "C";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/C.png", UriKind.Relative));
                        }
                        else
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;

                            cbBLADEDIRECTIONActual.SelectedValue = null;
                            img.Source = new BitmapImage(new Uri("", UriKind.Relative));
                        }
                    }

                    #endregion

                    #region TENSION_UP

                    if (items[0].TENSION_UP != null && items[0].TENSION_UP_MARGIN != null)
                    {
                        txtTENSION_UPSpecification.Text = (items[0].TENSION_UP.Value.ToString("#,##0.##") + " ± " + items[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtTENSION_UPSpecification.Text = "";
                    }

                    #endregion

                    #region TENSION_DOWN

                    if (items[0].TENSION_DOWN != null && items[0].TENSION_DOWN_MARGIN != null)
                    {
                        //txtTENSION_DOWNSpecification.Text = (items[0].TENSION_DOWN.Value.ToString("#,##0.##") + " ± " + items[0].TENSION_DOWN_MARGIN.Value.ToString("#,##0.##"));
                        txtTENSION_DOWNSpecification.Text = ("( " + items[0].TENSION_DOWN.Value.ToString("#,##0.##") + " )");
                    }
                    else
                    {
                        txtTENSION_DOWNSpecification.Text = "";
                    }

                    #endregion

                    #region FRAMEWIDTH_FORN

                    if (items[0].FRAMEWIDTH_FORN != null)
                        txtFRAMEWIDTH_FORNSpecification.Text = items[0].FRAMEWIDTH_FORN.Value.ToString("#,##0.##");
                    else
                        txtFRAMEWIDTH_FORNSpecification.Text = "";

                    #endregion

                    #region FRAMEWIDTH_TENTER

                    if (items[0].FRAMEWIDTH_TENTER != null)
                        txtFRAMEWIDTH_TENTERSpecification.Text = items[0].FRAMEWIDTH_TENTER.Value.ToString("#,##0.##");
                    else
                        txtFRAMEWIDTH_TENTERSpecification.Text = "";

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

                    #region WIDTHCOAT

                    if (items[0].WIDTHCOAT != null)
                        txtWIDTHCOATSpecification.Text = "> " + items[0].WIDTHCOAT.Value.ToString("#,##0.##");
                    else
                        txtWIDTHCOATSpecification.Text = "";

                    #endregion

                    #region WIDTHCOATALL

                    if (items[0].WIDTHCOATALL_MAX != null && items[0].WIDTHCOATALL_MIN != null)
                    {
                        //txtWIDTHCOATALLSpecification.Text = (items[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##") + " - " + items[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##"));
                        txtWIDTHCOATALLSpecification.Text = ("( " + items[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##") + " )");
                    }
                    else
                    {
                        txtWIDTHCOATALLSpecification.Text = "";
                    }

                    #endregion

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

                    txtRATIOSILICONE.Text = items[0].RATIOSILICONE;

                    #region COATINGWEIGTH_MIN

                    if (items[0].COATINGWEIGTH_MIN != null && items[0].COATINGWEIGTH_MAX != null)
                    {
                        //txtCOATINGWEIGTH.Text = (items[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##") + " - " + items[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##"));
                        txtCOATINGWEIGTH.Text = (items[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##") + " +/- " + items[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtCOATINGWEIGTH.Text = "";
                    }

                    #endregion

                    #region HUMIDITY

                    if (items[0].HUMIDITY_MAX != null)
                    {
                        //string HUMIDITY = (items[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + items[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
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
                    ClearFinishing();
                    txtItemGoods.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                ClearFinishing();
            }
        }

        #endregion

        private void FINISHING_COATINGDATABYLOT(string P_MCNO, string P_WEAVINGLOT)
        {
            try
            {
                List<FINISHING_COATINGDATABYLOT> items = _session.GetFINISHING_COATINGDATABYLOT(P_MCNO, P_WEAVINGLOT);

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

                    txtBE_COATWIDTHActual.Text = items[0].BE_COATWIDTH.ToString();
                    txtFan110Actual.Text = items[0].FANRPM.ToString();
                    txtEXFAN1_6Actual.Text = items[0].EXFAN_FRONT_BACK.ToString();
                    txtEXFAN2_5Actual.Text = items[0].EXFAN_MIDDLE.ToString();
                    txtANGLEKNIFEActual.Text = items[0].ANGLEKNIFE.ToString();
                    txtBLADENOActual.Text = items[0].BLADENO;

                    #region BLADEDIRECTION

                    if (items[0].BLADEDIRECTION != "")
                    {
                        if (items[0].BLADEDIRECTION == "L")
                        {
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/L.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "R")
                        {
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/R.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "C")
                        {
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/C.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        img.Source = new BitmapImage(new Uri("", UriKind.Relative));
                    }

                    #endregion

                    //txtTENSION_DOWNActual.Text = items[0].OPOLE_TENSIONDOWN.ToString();
                    //txtTENSION_UPActual.Text = items[0].CYLINDER_TENSIONUP.ToString();
                    txtFRAMEWIDTH_FORNActual.Text = items[0].FRAMEWIDTH_FORN.ToString();
                    txtFRAMEWIDTH_TENTERActual.Text = items[0].FRAMEWIDTH_TENTER.ToString();
                    txtWIDTHCOATActual.Text = items[0].WIDTHCOAT.ToString();
                    txtWIDTHCOATALLActual.Text = items[0].WIDTHCOATALL.ToString();
                    txtSiliconeA.Text = items[0].SILICONE_A;
                    txtSiliconeB.Text = items[0].SILICONE_B;
                    txtCWL.Text = items[0].COATINGWEIGTH_L.ToString();
                    txtCWR.Text = items[0].COATINGWEIGTH_R.ToString();
                    txtCWC.Text = items[0].COATINGWEIGTH_C.ToString();

                    //txtSATPV.Text = items[0].SATURATOR_CHEM_PV.ToString();
                    //txtWASH1PV.Text = items[0].WASHING1_PV.ToString();
                    //txtWASH2PV.Text = items[0].WASHING2_PV.ToString();

                    //txtHOTPV.Text = items[0].HOTFLUE_PV.ToString();
                    //txtSPEED.Text = items[0].SPEED_PV.ToString();

                    //txtTEMP1PV.Text = items[0].TEMP1_PV.ToString();
                    //txtTEMP2PV.Text = items[0].TEMP2_PV.ToString();
                    //txtTEMP3PV.Text = items[0].TEMP3_PV.ToString();
                    //txtTEMP4PV.Text = items[0].TEMP4_PV.ToString();
                    //txtTEMP5PV.Text = items[0].TEMP5_PV.ToString();
                    //txtTEMP6PV.Text = items[0].TEMP6_PV.ToString();
                    //txtTEMP7PV.Text = items[0].TEMP7_PV.ToString();
                    //txtTEMP8PV.Text = items[0].TEMP8_PV.ToString();
                    //txtTEMP9PV.Text = items[0].TEMP9_PV.ToString();
                    //txtTEMP10PV.Text = items[0].TEMP10_PV.ToString();

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

                    _session.BE_COATWIDTH = items[0].BE_COATWIDTH;
                    _session.FANRPM = items[0].FANRPM;
                    _session.EXFAN_FRONT_BACK = items[0].EXFAN_FRONT_BACK;
                    _session.EXFAN_MIDDLE = items[0].EXFAN_MIDDLE;
                    _session.ANGLEKNIFE = items[0].ANGLEKNIFE;
                    _session.BLADENO = items[0].BLADENO;
                    _session.BLADEDIRECTION = items[0].BLADEDIRECTION;

                    //_session.TENSIONDOWN = items[0].OPOLE_TENSIONDOWN;
                    //_session.TENSIONUP = items[0].CYLINDER_TENSIONUP;
                    _session.FORN = items[0].FRAMEWIDTH_FORN;
                    _session.TENTER = items[0].FRAMEWIDTH_TENTER;
                    _session.WIDTHCOAT = items[0].WIDTHCOAT;
                    _session.WIDTHCOATALL = items[0].WIDTHCOATALL;
                    _session.SILICONE_A = items[0].SILICONE_A;
                    _session.SILICONE_B = items[0].SILICONE_B;
                    _session.CWL = items[0].COATINGWEIGTH_L;
                    _session.CWR = items[0].COATINGWEIGTH_R;
                    _session.CWC = items[0].COATINGWEIGTH_C;

                    //_session.SATURATOR_PV = items[0].SATURATOR_CHEM_PV;
                    //_session.SATURATOR_SP = items[0].SATURATOR_CHEM_SP;
                    //_session.WASHING1_PV = items[0].WASHING1_PV;
                    //_session.WASHING1_SP = items[0].WASHING1_SP;
                    //_session.WASHING2_PV = items[0].WASHING2_PV;
                    //_session.WASHING2_SP = items[0].WASHING2_SP;
                    //_session.HOTFLUE_PV = items[0].HOTFLUE_PV;
                    //_session.HOTFLUE_SP = items[0].HOTFLUE_SP;
                    //_session.TEMP1_PV = items[0].TEMP1_PV;
                    //_session.TEMP1_SP = items[0].TEMP1_SP;
                    //_session.TEMP2_PV = items[0].TEMP2_PV;
                    //_session.TEMP2_SP = items[0].TEMP2_SP;
                    //_session.TEMP3_PV = items[0].TEMP3_PV;
                    //_session.TEMP3_SP = items[0].TEMP3_SP;
                    //_session.TEMP4_PV = items[0].TEMP4_PV;
                    //_session.TEMP4_SP = items[0].TEMP4_SP;
                    //_session.TEMP5_PV = items[0].TEMP5_PV;
                    //_session.TEMP5_SP = items[0].TEMP5_SP;
                    //_session.TEMP6_PV = items[0].TEMP6_PV;
                    //_session.TEMP6_SP = items[0].TEMP6_SP;
                    //_session.TEMP7_PV = items[0].TEMP7_PV;
                    //_session.TEMP7_SP = items[0].TEMP7_SP;
                    //_session.TEMP8_PV = items[0].TEMP8_PV;
                    //_session.TEMP8_SP = items[0].TEMP8_SP;
                    //_session.TEMP9_PV = items[0].TEMP9_PV;
                    //_session.TEMP9_SP = items[0].TEMP9_SP;
                    //_session.TEMP10_PV = items[0].TEMP10_PV;
                    //_session.TEMP10_SP = items[0].TEMP10_SP;
                    //_session.SPEED_PV = items[0].SPEED_PV;
                    //_session.SPEED_SP = items[0].SPEED_SP;

                    _session.HUMIDITY_AF = items[0].HUMIDITY_AF;
                    _session.HUMIDITY_BF = items[0].HUMIDITY_BF;
                    _session.OPERATOR_GROUP = items[0].OPERATOR_GROUP;

                    FINISHING_COATINGPLCDATA(P_MCNO, P_WEAVINGLOT);
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

        private void FINISHING_COATINGPLCDATA(string P_MCNO, string P_WEAVINGLOT)
        {
            try
            {
                List<FINISHING_COATINGPLCDATA> items = _session.GetFINISHING_COATINGPLCDATA(P_MCNO, P_WEAVINGLOT);

                if (items != null && items.Count > 0)
                {
                    #region STARTDATE
                    if (items[0].STARTDATE != null)
                    {
                        txtStartTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");
                        _session.STARTDATE = items[0].STARTDATE;
                    }
                    #endregion

                    #region TEMP1
                    if (items[0].TEMP1 != null)
                    {
                        txtTEMP1PV.Text = items[0].TEMP1.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP2
                    if (items[0].TEMP2 != null)
                    {
                        txtTEMP2PV.Text = items[0].TEMP2.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP3
                    if (items[0].TEMP3 != null)
                    {
                        txtTEMP3PV.Text = items[0].TEMP3.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP4
                    if (items[0].TEMP4 != null)
                    {
                        txtTEMP4PV.Text = items[0].TEMP4.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP5
                    if (items[0].TEMP5 != null)
                    {
                        txtTEMP5PV.Text = items[0].TEMP5.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP6
                    if (items[0].TEMP6 != null)
                    {
                        txtTEMP6PV.Text = items[0].TEMP6.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP7
                    if (items[0].TEMP7 != null)
                    {
                        txtTEMP7PV.Text = items[0].TEMP7.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP8
                    if (items[0].TEMP8 != null)
                    {
                        txtTEMP8PV.Text = items[0].TEMP8.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP9
                    if (items[0].TEMP9 != null)
                    {
                        txtTEMP9PV.Text = items[0].TEMP9.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TEMP10
                    if (items[0].TEMP10 != null)
                    {
                        txtTEMP10PV.Text = items[0].TEMP10.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region WASH1
                    if (items[0].WASH1 != null)
                    {
                        txtWASH1PV.Text = items[0].WASH1.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region WASH2
                    if (items[0].WASH2 != null)
                    {
                        txtWASH2PV.Text = items[0].WASH2.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region SAT
                    if (items[0].SAT != null)
                    {
                        txtSATPV.Text = items[0].SAT.Value.ToString("#,##0.##");
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

                    #region TENUP
                    if (items[0].TENUP != null)
                    {
                        txtTENSION_UPActual.Text = items[0].TENUP.Value.ToString("#,##0.##");
                    }
                    #endregion

                    #region TENDOWN
                    if (items[0].TENDOWN != null)
                    {
                        txtTENSION_DOWNActual.Text = items[0].TENDOWN.Value.ToString("#,##0.##");
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

                    _session.TEMP1 = items[0].TEMP1;
                    _session.TEMP1_MIN = items[0].TEMP1_MIN;
                    _session.TEMP1_MAX = items[0].TEMP1_MAX;
                    _session.TEMP2 = items[0].TEMP2;
                    _session.TEMP2_MIN = items[0].TEMP2_MIN;
                    _session.TEMP2_MAX = items[0].TEMP2_MAX;
                    _session.TEMP3 = items[0].TEMP3;
                    _session.TEMP3_MIN = items[0].TEMP3_MIN;
                    _session.TEMP3_MAX = items[0].TEMP3_MAX;
                    _session.TEMP4 = items[0].TEMP4;
                    _session.TEMP4_MIN = items[0].TEMP4_MIN;
                    _session.TEMP4_MAX = items[0].TEMP4_MAX;
                    _session.TEMP5 = items[0].TEMP5;
                    _session.TEMP5_MIN = items[0].TEMP5_MIN;
                    _session.TEMP5_MAX = items[0].TEMP5_MAX;
                    _session.TEMP6 = items[0].TEMP6;
                    _session.TEMP6_MIN = items[0].TEMP6_MIN;
                    _session.TEMP6_MAX = items[0].TEMP6_MAX;
                    _session.TEMP7 = items[0].TEMP7;
                    _session.TEMP7_MIN = items[0].TEMP7_MIN;
                    _session.TEMP7_MAX = items[0].TEMP7_MAX;
                    _session.TEMP8 = items[0].TEMP8;
                    _session.TEMP8_MIN = items[0].TEMP8_MIN;
                    _session.TEMP8_MAX = items[0].TEMP8_MAX;

                    _session.TEMP9 = items[0].TEMP9;
                    _session.TEMP9_MIN = items[0].TEMP9_MIN;
                    _session.TEMP9_MAX = items[0].TEMP9_MAX;
                    _session.TEMP10 = items[0].TEMP10;
                    _session.TEMP10_MIN = items[0].TEMP10_MIN;
                    _session.TEMP10_MAX = items[0].TEMP10_MAX;

                    _session.SPEED = items[0].SPEED;
                    _session.SPEED_MIN = items[0].SPEED_MIN;
                    _session.SPEED_MAX = items[0].SPEED_MAX;

                    _session.TENUP_MIN = items[0].TENUP_MIN;
                    _session.TENUP_MAX = items[0].TENUP_MAX;
                    _session.TENSIONUP = items[0].TENUP;

                    _session.TENDOWN_MIN = items[0].TENDOWN_MIN;
                    _session.TENDOWN_MAX = items[0].TENDOWN_MAX;
                    _session.TENSIONDOWN = items[0].TENDOWN;
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

        //        if (txtWIDTHCOATActual.Text != ""
        //            && txtWIDTHCOATActual.Text != _session.WIDTHCOAT.ToString())
        //            _session.WIDTHCOAT = decimal.Parse(txtWIDTHCOATActual.Text);

        //        if (txtWIDTHCOATALLActual.Text != ""
        //            && txtWIDTHCOATALLActual.Text != _session.WIDTHCOAT.ToString())
        //            _session.WIDTHCOATALL = decimal.Parse(txtWIDTHCOATALLActual.Text);

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

        //            string result = _session.FINISHING_UPDATECOATINGFinishing();

        //            if (string.IsNullOrEmpty(result) == true)
        //            {
        //                txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
        //                cmdFinish.IsEnabled = false;
        //                cmdRemark.IsEnabled = false;
                        
        //                //PageManager.Instance.Back();
        //                chkFinish = true;
        //                //string scouringNo = txtScouringNo.Text;
        //                //Print(_session.WEAVINGLOT, _session.ItemCode, scouringNo);
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

                if (txtWIDTHCOATActual.Text != ""
                    && txtWIDTHCOATActual.Text != _session.WIDTHCOAT.ToString())
                    _session.WIDTHCOAT = decimal.Parse(txtWIDTHCOATActual.Text);

                if (txtWIDTHCOATALLActual.Text != ""
                    && txtWIDTHCOATALLActual.Text != _session.WIDTHCOAT.ToString())
                    _session.WIDTHCOATALL = decimal.Parse(txtWIDTHCOATALLActual.Text);

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

                #region Finishing

                #region BE_COATWIDTH
                if (txtBE_COATWIDTHActual.Text != "")
                {
                    _session.BE_COATWIDTH = decimal.Parse(txtBE_COATWIDTHActual.Text);
                }
                #endregion

                #region FANRPM
                if (txtFan110Actual.Text != "")
                {
                    _session.FANRPM = decimal.Parse(txtFan110Actual.Text);
                }
                #endregion

                #region EXFAN_FRONT_BACK
                if (txtEXFAN1_6Actual.Text != "")
                {
                    _session.EXFAN_FRONT_BACK = decimal.Parse(txtEXFAN1_6Actual.Text);
                }
                #endregion

                #region EXFAN_MIDDLE
                if (txtEXFAN2_5Actual.Text != "")
                {
                    _session.EXFAN_MIDDLE = decimal.Parse(txtEXFAN2_5Actual.Text);
                }
                #endregion

                #region ANGLEKNIFE
                if (txtANGLEKNIFEActual.Text != "")
                {
                    _session.ANGLEKNIFE = decimal.Parse(txtANGLEKNIFEActual.Text);
                }
                #endregion

                _session.BLADENO = txtBLADENOActual.Text;

                #region BLADEDIRECTION
                if (cbBLADEDIRECTIONActual.SelectedValue != null)
                {
                    _session.BLADEDIRECTION = cbBLADEDIRECTIONActual.SelectedValue.ToString();
                }
                #endregion

                #region FRAMEWIDTH_FORN
                if (txtFRAMEWIDTH_FORNActual.Text != "")
                {
                    _session.FORN = decimal.Parse(txtFRAMEWIDTH_FORNActual.Text);
                }
                #endregion

                #region FRAMEWIDTH_TENTER
                if (txtFRAMEWIDTH_TENTERActual.Text != "")
                {
                    _session.TENTER = decimal.Parse(txtFRAMEWIDTH_TENTERActual.Text);
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

                        #region TENUP
                        if (!string.IsNullOrEmpty(txtTENSION_UPActual.Text))
                        {
                            _session.TENSIONUP = decimal.Parse(txtTENSION_UPActual.Text);
                        }
                        #endregion

                        #region TENDOWN
                        if (!string.IsNullOrEmpty(txtTENSION_DOWNActual.Text))
                        {
                            _session.TENSIONDOWN = decimal.Parse(txtTENSION_DOWNActual.Text);
                        }
                        #endregion

                        if (!string.IsNullOrEmpty(txtSiliconeA.Text))
                        {
                            _session.SILICONE_A = txtSiliconeA.Text;
                        }
                        if (!string.IsNullOrEmpty(txtSiliconeB.Text))
                        {
                            _session.SILICONE_B = txtSiliconeB.Text;
                        }
                        if (!string.IsNullOrEmpty(txtCWL.Text))
                        {
                            _session.CWL = decimal.Parse(txtCWL.Text);
                        }
                        if (!string.IsNullOrEmpty(txtCWC.Text))
                        {
                            _session.CWC = decimal.Parse(txtCWC.Text);
                        }
                        if (!string.IsNullOrEmpty(txtCWR.Text))
                        {
                            _session.CWR = decimal.Parse(txtCWR.Text);
                        }

                        #region SATURATOR

                        if (txtSATPV.Text != "")
                        {
                            try
                            {
                                decimal satpv = decimal.Parse(txtSATPV.Text);
                                _session.SATURATOR_PV = satpv;
                                _session.SATURATOR_SP = satpv;
                            }
                            catch
                            {
                                _session.SATURATOR_PV = 0;
                                _session.SATURATOR_SP = 0;
                            }
                        }

                        #endregion

                        #region WASHING

                        if (txtWASH1PV.Text != "")
                        {
                            try
                            {
                                decimal wash1pv = decimal.Parse(txtWASH1PV.Text);
                                _session.WASHING1_PV = wash1pv;
                                _session.WASHING1_SP = wash1pv;
                            }
                            catch
                            {
                                _session.WASHING1_PV = 0;
                                _session.WASHING1_SP = 0;
                            }
                        }

                        #endregion

                        #region WASHING 2

                        if (txtWASH2PV.Text != "")
                        {
                            try
                            {
                                decimal wash2pv = decimal.Parse(txtWASH2PV.Text);
                                _session.WASHING2_PV = wash2pv;
                                _session.WASHING2_SP = wash2pv;
                            }
                            catch
                            {
                                _session.WASHING2_PV = 0;
                                _session.WASHING2_SP = 0;
                            }
                        }

                        #endregion

                        #region HOT

                        if (txtHOTPV.Text != "")
                        {
                            try
                            {
                                decimal hot = decimal.Parse(txtHOTPV.Text);
                                _session.HOTFLUE_PV = hot;
                                _session.HOTFLUE_SP = hot;
                            }
                            catch
                            {
                                _session.HOTFLUE_PV = 0;
                                _session.HOTFLUE_SP = 0;
                            }
                        }
                        #endregion

                        #region SPEED
                        if (txtSPEED.Text != "")
                        {
                            try
                            {
                                decimal speed = decimal.Parse(txtSPEED.Text);
                                _session.SPEED_PV = speed;
                                _session.SPEED_SP = speed;
                            }
                            catch
                            {
                                _session.SPEED_PV = 0;
                                _session.SPEED_SP = 0;
                            }
                        }
                        #endregion
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

                    string result = _session.FINISHING_UPDATECOATINGDATAFinishing();

                    if (string.IsNullOrEmpty(result) == true)
                    {
                        txtEndTime.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");
                        cmdFinish.IsEnabled = false;
                        cmdRemark.IsEnabled = false;

                        //PageManager.Instance.Back();

                        chkFinish = true;
                        //string scouringNo = txtScouringNo.Text;
                        //Print(_session.WEAVINGLOT, _session.ItemCode, scouringNo);
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
                ConmonReportService.Instance.ReportName = "Coating3";
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


                ConmonReportService.Instance.ReportName = "Coating3";

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

                try
                {
                    LoadBladedirection();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
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
