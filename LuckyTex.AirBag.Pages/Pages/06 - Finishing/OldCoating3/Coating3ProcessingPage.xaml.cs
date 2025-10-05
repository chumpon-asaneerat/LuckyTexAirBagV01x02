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

using DataControl.ClassData;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for Coating3ProcessingPage.xaml
    /// </summary>
    public partial class Coating3ProcessingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating3ProcessingPage()
        {
            InitializeComponent();

            LoadShift();

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Coating3MachineConfig;

            ImageL.Visibility = System.Windows.Visibility.Collapsed;
            ImageR.Visibility = System.Windows.Visibility.Collapsed;
            ImageC.Visibility = System.Windows.Visibility.Collapsed;

            rbMassProduction.Visibility = System.Windows.Visibility.Collapsed;
            rbTest.Visibility = System.Windows.Visibility.Collapsed;
            rbGuide.Visibility = System.Windows.Visibility.Collapsed;
            txtItemWeaving.IsEnabled = false;
            txtLot.IsEnabled = false;
            txtLength.IsEnabled = false;
            txtFINISHINGLOT.IsEnabled = false;
            txtStartTime.IsEnabled = false;

            txtCustomer.IsEnabled = false;
            txtItemGoods.IsEnabled = false;

            cbShift.IsEnabled = false;

            txtClothHumidity.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITYBFSpecification.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITY_BF.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Internal Variables

        private FinishingSession _session = new FinishingSession();
        private bool chkLoad = true;
        private bool mcStatus = true;

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
            if (chkLoad == true)
                EnabledCon(false);
            else
                PageManager.Instance.Back();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Coating3ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Coating3>(Instance_ReadCompleted);
            Coating3ModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<Coating3> e)
        {
            if (null == e.Value)
            {
                return;
            }
            if (chkManual.IsChecked.HasValue && !chkManual.IsChecked.Value)
            {
                txtTEMP1PV.Text = e.Value.TEMP1PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP2PV.Text = e.Value.TEMP2PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP3PV.Text = e.Value.TEMP3PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP4PV.Text = e.Value.TEMP4PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP5PV.Text = e.Value.TEMP5PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP6PV.Text = e.Value.TEMP6PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP7PV.Text = e.Value.TEMP7PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP8PV.Text = e.Value.TEMP8PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP9PV.Text = e.Value.TEMP9PV.ToString("n2").Replace(",", string.Empty);
                txtTEMP10PV.Text = e.Value.TEMP10PV.ToString("n2").Replace(",", string.Empty);

                txtSPEED.Text = e.Value.SPEED.Value.ToString("n2").Replace(",", string.Empty);
                txtHOTPV.Text = e.Value.HOTPV.ToString("n2").Replace(",", string.Empty);
                txtSATPV.Text = e.Value.SATPV.ToString("n2").Replace(",", string.Empty);
                //txtSEN1.Text = e.Value.SEN1.ToString("n2").Replace(",", string.Empty);
                //txtSEN2.Text = e.Value.SEN2.ToString("n2").Replace(",", string.Empty);

                txtWASH1PV.Text = e.Value.WASH1PV.ToString("n2").Replace(",", string.Empty);
                txtWASH2PV.Text = e.Value.WASH2PV.ToString("n2").Replace(",", string.Empty);

                //txtTENDOWN.Text = e.Value.TENDOWN.ToString("n2").Replace(",", string.Empty);
                //txtTENUP.Text = e.Value.TENUP.ToString("n2").Replace(",", string.Empty);

                _session.SATURATOR_PV = e.Value.SATPV;
                _session.SATURATOR_SP = e.Value.SATSP;

                _session.WASHING1_PV = e.Value.WASH1PV;
                _session.WASHING1_SP = e.Value.WASH1SP;
                _session.WASHING2_PV = e.Value.WASH2PV;
                _session.WASHING2_SP = e.Value.WASH2SP;
                _session.HOTFLUE_PV = e.Value.HOTPV;
                _session.HOTFLUE_SP = e.Value.HOTSP;

                _session.TEMP1_PV = e.Value.TEMP1PV;
                _session.TEMP1_SP = e.Value.TEMP1SP;
                _session.TEMP2_PV = e.Value.TEMP2PV;
                _session.TEMP2_SP = e.Value.TEMP2SP;
                _session.TEMP3_PV = e.Value.TEMP3PV;
                _session.TEMP3_SP = e.Value.TEMP3SP;
                _session.TEMP4_PV = e.Value.TEMP4PV;
                _session.TEMP4_SP = e.Value.TEMP4SP;
                _session.TEMP5_PV = e.Value.TEMP5PV;
                _session.TEMP5_SP = e.Value.TEMP5SP;
                _session.TEMP6_PV = e.Value.TEMP6PV;
                _session.TEMP6_SP = e.Value.TEMP6SP;
                _session.TEMP7_PV = e.Value.TEMP7PV;
                _session.TEMP7_SP = e.Value.TEMP7SP;
                _session.TEMP8_PV = e.Value.TEMP8PV;
                _session.TEMP8_SP = e.Value.TEMP8SP;
                _session.TEMP9_PV = e.Value.TEMP9PV;
                _session.TEMP9_SP = e.Value.TEMP9SP;
                _session.TEMP10_PV = e.Value.TEMP10PV;
                _session.TEMP10_SP = e.Value.TEMP10SP;

                if (e.Value.SPEED != null)
                {
                    _session.SPEED_PV = (decimal)(MathEx.Round(e.Value.SPEED.Value, 2));

                    _session.SPEED_SP = _session.SPEED_PV;
                }
            }
        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdSaveCondition_Click

        private void cmdSaveCondition_Click(object sender, RoutedEventArgs e)
        {
            if (txtOperator.Text != "" && txtFINISHINGLOT.Text != "")
            {
                if (CheckNull() == true)
                    SaveCondition();
                else
                    "Can't Save Condition Please check data isn't null".ShowMessageBox(true);
            }
        }

        #endregion

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

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtItemWeaving_KeyDown

        private void txtItemWeaving_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLot.Focus();
                txtLot.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLot_KeyDown

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
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
                txtBE_COATWIDTHActual.Focus();
                txtBE_COATWIDTHActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFINISHINGLOT_KeyDown
        private void txtFINISHINGLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStartTime.Focus();
                txtStartTime.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStartTime_KeyDown
        private void txtStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBE_COATWIDTHActual.Focus();
                txtBE_COATWIDTHActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtBE_COATWIDTHActual_KeyDown
        private void txtBE_COATWIDTHActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFan110Actual.Focus();
                txtFan110Actual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFan110Actual_KeyDown
        private void txtFan110Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN1_6Actual.Focus();
                txtEXFAN1_6Actual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtEXFAN1_6Actual_KeyDown
        private void txtEXFAN1_6Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN2_5Actual.Focus();
                txtEXFAN2_5Actual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtEXFAN2_5Actual_KeyDown
        private void txtEXFAN2_5Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtANGLEKNIFEActual.Focus();
                txtANGLEKNIFEActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtANGLEKNIFEActual_KeyDown
        private void txtANGLEKNIFEActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBLADENOActual.Focus();
                txtBLADENOActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtBLADENOActual_KeyDown
        private void txtBLADENOActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_UPActual.Focus();
                txtTENSION_UPActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_UPActual_KeyDown
        private void txtTENSION_UPActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_DOWNActual.Focus();
                txtTENSION_DOWNActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_DOWNActual_KeyDown
        private void txtTENSION_DOWNActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFRAMEWIDTH_FORNActual.Focus();
                txtFRAMEWIDTH_FORNActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFRAMEWIDTH_FORNActual_KeyDown
        private void txtFRAMEWIDTH_FORNActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFRAMEWIDTH_TENTERActual.Focus();
                txtFRAMEWIDTH_TENTERActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFRAMEWIDTH_TENTERActual_KeyDown
        private void txtFRAMEWIDTH_TENTERActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSPEED.IsEnabled == true)
                {
                    txtSPEED.Focus();
                    txtSPEED.SelectAll();
                    e.Handled = true;
                }
                else
                {
                    txtWIDTHCOATActual.Focus();
                    txtWIDTHCOATActual.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtSPEED_KeyDown
        private void txtSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHCOATActual.Focus();
                txtWIDTHCOATActual.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

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
                if (txtSATPV.IsEnabled == true)
                {
                    txtSATPV.Focus();
                    txtSATPV.SelectAll();
                    e.Handled = true;
                }
                else
                {
                    txtSiliconeA.Focus();
                    txtSiliconeA.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtSATPV_KeyDown
        private void txtSATPV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWASH1PV.Focus();
                txtWASH1PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWASH1PV_KeyDown
        private void txtWASH1PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWASH2PV.Focus();
                txtWASH2PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWASH2PV_KeyDown
        private void txtWASH2PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHOTPV.Focus();
                txtHOTPV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtHOTPV_KeyDown
        private void txtHOTPV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP1PV.Focus();
                txtTEMP1PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP1PV_KeyDown
        private void txtTEMP1PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP2PV.Focus();
                txtTEMP2PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP2PV_KeyDown
        private void txtTEMP2PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP3PV.Focus();
                txtTEMP3PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP3PV_KeyDown
        private void txtTEMP3PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP4PV.Focus();
                txtTEMP4PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP4PV_KeyDown
        private void txtTEMP4PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP5PV.Focus();
                txtTEMP5PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP5PV_KeyDown
        private void txtTEMP5PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP6PV.Focus();
                txtTEMP6PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP6PV_KeyDown
        private void txtTEMP6PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP7PV.Focus();
                txtTEMP7PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP7PV_KeyDown
        private void txtTEMP7PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP8PV.Focus();
                txtTEMP8PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP8PV_KeyDown
        private void txtTEMP8PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP9PV.Focus();
                txtTEMP9PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP9PV_KeyDown
        private void txtTEMP9PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEMP10PV.Focus();
                txtTEMP10PV.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTEMP10PV_KeyDown
        private void txtTEMP10PV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSiliconeA.Focus();
                txtSiliconeA.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtSiliconeA
        private void txtSiliconeA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSiliconeB.Focus();
                txtSiliconeB.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtSiliconeB
        private void txtSiliconeB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCWL.Focus();
                txtCWL.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtCWL
        private void txtCWL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCWC.Focus();
                txtCWC.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtCWC
        private void txtCWC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCWR.Focus();
                txtCWR.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtCWR
        private void txtCWR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSaveCondition.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region CheckBox

        private void chkManual_Checked(object sender, RoutedEventArgs e)
        {
            EnabledCon(true);
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            EnabledCon(false);
        }

        #endregion

        #region private Methods

        #region Load Combo

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

                if (_session.Customer != "")
                    _session = new FinishingSession();
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

            txtSATPV.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    Coating3ModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<Coating3>(Instance_ReadCompleted);
                    Coating3ModbusManager.Instance.Start();
                }
                else
                {
                    "Machine Status = false Please check config".ShowMessageBox();
                }
            }
            else
            {
                Coating3ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Coating3>(Instance_ReadCompleted);
                Coating3ModbusManager.Instance.Shutdown();
            }
        }
        #endregion

        #region CheckNull
        private bool CheckNull()
        {
            bool chkSave = true;

            if (txtCustomer.Text == "")
            {
                return false;
            }
            if (txtItemGoods.Text == "")
            {
                return false;
            }

            if (txtItemWeaving.Text == "")
            {
                return false;
            }
            if (txtLot.Text == "")
            {
                return false;
            }
            if (txtLength.Text == "")
            {
                return false;
            }


            //if (txtBE_COATWIDTHActual.Text == "")
            //{
            //    return false;
            //}
            //if (txtFan110Actual.Text == "")
            //{
            //    return false;
            //}
            //if (txtEXFAN1_6Actual.Text == "")
            //{
            //    return false;
            //}
            //if (txtEXFAN2_5Actual.Text == "")
            //{
            //    return false;
            //}
            //if (txtANGLEKNIFEActual.Text == "")
            //{
            //    return false;
            //}
            //if (txtBLADENOActual.Text == "")
            //{
            //    return false;
            //}

            //if (txtTENSION_UPActual.Text == "")
            //{
            //    return false;
            //}
            //if (txtTENSION_DOWNActual.Text == "")
            //{
            //    return false;
            //}
            //if (txtFRAMEWIDTH_FORNActual.Text == "")
            //{
            //    return false;
            //}
            //if (txtFRAMEWIDTH_TENTERActual.Text == "")
            //{
            //    return false;
            //}
            if (txtSPEED.Text == "")
            {
                return false;
            }
            //if (txtWIDTHCOATActual.Text == "")
            //{
            //    return false;
            //}
            //if (txtWIDTHCOATALLActual.Text == "")
            //{
            //    return false;
            //}

            if (txtSATPV.Text == "")
            {
                return false;
            }
            if (txtWASH1PV.Text == "")
            {
                return false;
            }
            if (txtWASH2PV.Text == "")
            {
                return false;
            }
            if (txtHOTPV.Text == "")
            {
                return false;
            }

            if (txtTEMP1PV.Text == "")
            {
                return false;
            }
            if (txtTEMP2PV.Text == "")
            {
                return false;
            }
            if (txtTEMP3PV.Text == "")
            {
                return false;
            }
            if (txtTEMP4PV.Text == "")
            {
                return false;
            }
            if (txtTEMP5PV.Text == "")
            {
                return false;
            }
            if (txtTEMP6PV.Text == "")
            {
                return false;
            }
            if (txtTEMP7PV.Text == "")
            {
                return false;
            }
            if (txtTEMP8PV.Text == "")
            {
                return false;
            }
            if (txtTEMP9PV.Text == "")
            {
                return false;
            }
            if (txtTEMP10PV.Text == "")
            {
                return false;
            }

            //if (txtSiliconeA.Text == "")
            //{
            //    return false;
            //}
            //if (txtSiliconeB.Text == "")
            //{
            //    return false;
            //}
            //if (txtCWL.Text == "")
            //{
            //    return false;
            //}
            //if (txtCWR.Text == "")
            //{
            //    return false;
            //}
            //if (txtCWC.Text == "")
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
                        }
                        else if (items[0].BLADEDIRECTION == "R")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Visible;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else if (items[0].BLADEDIRECTION == "C")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;
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

                    //if (items[0].HUMIDITY_MIN != null && items[0].HUMIDITY_MAX != null)
                    //{
                    //    string HUMIDITY = (items[0].HUMIDITY_MIN.Value.ToString("#,##0.##") + " - " + items[0].HUMIDITY_MAX.Value.ToString("#,##0.##"));
                    //    txtHUMIDITYAFSpecification.Text = HUMIDITY;
                    //    txtHUMIDITYBFSpecification.Text = HUMIDITY;
                    //}
                    //else
                    //{
                    //    txtHUMIDITYAFSpecification.Text = "";
                    //    txtHUMIDITYBFSpecification.Text = "";
                    //}
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

        #region LoadFinishing_GetCoating

        private void LoadFinishing_GetCoating(string mcno, string flag)
        {
            try
            {
                List<FINISHING_GETCOATINGDATA> items = _session.GetFINISHING_GETCOATINGCONDITIONDATA(mcno, flag);

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

                    txtStartTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");
                    _session.STARTDATE = items[0].STARTDATE;

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
                            cbBLADEDIRECTIONActual.SelectedValue = "L";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/L.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "R")
                        {
                            cbBLADEDIRECTIONActual.SelectedValue = "R";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/R.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "C")
                        {
                            cbBLADEDIRECTIONActual.SelectedValue = "C";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/C.png", UriKind.Relative));
                        }
                    }
                    else
                    {
                        cbBLADEDIRECTIONActual.SelectedValue = null;
                        img.Source = new BitmapImage(new Uri("", UriKind.Relative));
                    }

                    #endregion

                    txtTENSION_DOWNActual.Text = items[0].OPOLE_TENSIONDOWN.ToString();
                    txtTENSION_UPActual.Text = items[0].CYLINDER_TENSIONUP.ToString();
                    txtFRAMEWIDTH_FORNActual.Text = items[0].FRAMEWIDTH_FORN.ToString();
                    txtFRAMEWIDTH_TENTERActual.Text = items[0].FRAMEWIDTH_TENTER.ToString();
                    txtWIDTHCOATActual.Text = items[0].WIDTHCOAT.ToString();
                    txtWIDTHCOATALLActual.Text = items[0].WIDTHCOATALL.ToString();
                    txtSiliconeA.Text = items[0].SILICONE_A;
                    txtSiliconeB.Text = items[0].SILICONE_B;
                    txtCWL.Text = items[0].COATINGWEIGTH_L.ToString();
                    txtCWR.Text = items[0].COATINGWEIGTH_R.ToString();
                    txtCWC.Text = items[0].COATINGWEIGTH_C.ToString();

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

                    _session.BE_COATWIDTH = items[0].BE_COATWIDTH;
                    _session.FANRPM = items[0].FANRPM;
                    _session.EXFAN_FRONT_BACK = items[0].EXFAN_FRONT_BACK;
                    _session.EXFAN_MIDDLE = items[0].EXFAN_MIDDLE;
                    _session.ANGLEKNIFE = items[0].ANGLEKNIFE;
                    _session.BLADENO = items[0].BLADENO;
                    _session.BLADEDIRECTION = items[0].BLADEDIRECTION;

                    _session.TENSIONDOWN = items[0].OPOLE_TENSIONDOWN;
                    _session.TENSIONUP = items[0].CYLINDER_TENSIONUP;
                    _session.FORN = items[0].FRAMEWIDTH_FORN;
                    _session.TENTER = items[0].FRAMEWIDTH_TENTER;
                    _session.WIDTHCOAT = items[0].WIDTHCOAT;
                    _session.WIDTHCOATALL = items[0].WIDTHCOATALL;
                    _session.SILICONE_A = items[0].SILICONE_A;
                    _session.SILICONE_B = items[0].SILICONE_B;
                    _session.CWL = items[0].COATINGWEIGTH_L;
                    _session.CWR = items[0].COATINGWEIGTH_R;
                    _session.CWC = items[0].COATINGWEIGTH_C;

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

                    _session.HUMIDITY_AF = items[0].HUMIDITY_AF;
                    _session.HUMIDITY_BF = items[0].HUMIDITY_BF;
                    _session.OPERATOR_GROUP = items[0].OPERATOR_GROUP;

                    chkLoad = true;
                }
                else
                {
                    string msg = "Can't Load Coating3";

                    msg.ShowMessageBox(false);

                    chkLoad = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region SaveCondition

        private void SaveCondition()
        {
            try
            {
                string FINISHLOT = txtFINISHINGLOT.Text;
                string operatorid = txtOperator.Text;
                string flag = "P";

                if (FINISHLOT != "" && operatorid != "")
                {
                    _session.FINISHLOT = FINISHLOT;
                    _session.Operator = operatorid;
                    _session.Flag = flag;

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

                    #region TENSIONDOWN
                    if (txtTENSION_DOWNActual.Text != "")
                    {
                        _session.TENSIONDOWN = decimal.Parse(txtTENSION_DOWNActual.Text);
                    }
                    #endregion

                    #region TENSION_UP
                    if (txtTENSION_UPActual.Text != "")
                    {
                        _session.TENSIONUP = decimal.Parse(txtTENSION_UPActual.Text);
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

                    #region WIDTHCOAT
                    if (txtWIDTHCOATActual.Text != "")
                    {
                        _session.WIDTHCOAT = decimal.Parse(txtWIDTHCOATActual.Text);
                    }
                    #endregion

                    #region WIDTHCOATALL
                    if (txtWIDTHCOATALLActual.Text != "")
                    {
                        _session.WIDTHCOATALL = decimal.Parse(txtWIDTHCOATALLActual.Text);
                    }
                    #endregion

                    _session.SILICONE_A = txtSiliconeA.Text;
                    _session.SILICONE_B = txtSiliconeB.Text;

                    #region CWL
                    if (txtCWL.Text != "")
                    {
                        _session.CWL = decimal.Parse(txtCWL.Text);
                    }
                    #endregion

                    #region CWC
                    if (txtCWC.Text != "")
                    {
                        _session.CWC = decimal.Parse(txtCWC.Text);
                    }
                    #endregion

                    #region CWR
                    if (txtCWR.Text != "")
                    {
                        _session.CWR = decimal.Parse(txtCWR.Text);
                    }
                    #endregion

                    #region chkManual

                    if (chkManual.IsChecked == true)
                    {
                        #region TEMP1

                        if (txtTEMP1PV.Text != "")
                        {
                            try
                            {
                                decimal temp1 = decimal.Parse(txtTEMP1PV.Text);
                                _session.TEMP1_PV = temp1;
                                _session.TEMP1_SP = temp1;
                            }
                            catch
                            {
                                _session.TEMP1_PV = 0;
                                _session.TEMP1_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP2

                        if (txtTEMP2PV.Text != "")
                        {
                            try
                            {
                                decimal temp2 = decimal.Parse(txtTEMP2PV.Text);
                                _session.TEMP2_PV = temp2;
                                _session.TEMP2_SP = temp2;
                            }
                            catch
                            {
                                _session.TEMP2_PV = 0;
                                _session.TEMP2_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP3

                        if (txtTEMP3PV.Text != "")
                        {
                            try
                            {
                                decimal temp3 = decimal.Parse(txtTEMP3PV.Text);
                                _session.TEMP3_PV = temp3;
                                _session.TEMP3_SP = temp3;
                            }
                            catch
                            {
                                _session.TEMP3_PV = 0;
                                _session.TEMP3_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP4

                        if (txtTEMP4PV.Text != "")
                        {
                            try
                            {
                                decimal temp4 = decimal.Parse(txtTEMP4PV.Text);
                                _session.TEMP4_PV = temp4;
                                _session.TEMP4_SP = temp4;
                            }
                            catch
                            {
                                _session.TEMP4_PV = 0;
                                _session.TEMP4_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP5

                        if (txtTEMP5PV.Text != "")
                        {
                            try
                            {
                                decimal temp5 = decimal.Parse(txtTEMP5PV.Text);
                                _session.TEMP5_PV = temp5;
                                _session.TEMP5_SP = temp5;
                            }
                            catch
                            {
                                _session.TEMP5_PV = 0;
                                _session.TEMP5_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP6

                        if (txtTEMP6PV.Text != "")
                        {
                            try
                            {
                                decimal temp6 = decimal.Parse(txtTEMP6PV.Text);
                                _session.TEMP6_PV = temp6;
                                _session.TEMP6_SP = temp6;
                            }
                            catch
                            {
                                _session.TEMP6_PV = 0;
                                _session.TEMP6_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP7

                        if (txtTEMP7PV.Text != "")
                        {
                            try
                            {
                                decimal temp7 = decimal.Parse(txtTEMP7PV.Text);
                                _session.TEMP7_PV = temp7;
                                _session.TEMP7_SP = temp7;
                            }
                            catch
                            {
                                _session.TEMP7_PV = 0;
                                _session.TEMP7_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP8

                        if (txtTEMP8PV.Text != "")
                        {
                            try
                            {
                                decimal temp8 = decimal.Parse(txtTEMP8PV.Text);
                                _session.TEMP8_PV = temp8;
                                _session.TEMP8_SP = temp8;
                            }
                            catch
                            {
                                _session.TEMP8_PV = 0;
                                _session.TEMP8_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP9

                        if (txtTEMP9PV.Text != "")
                        {
                            try
                            {
                                decimal temp9 = decimal.Parse(txtTEMP9PV.Text);
                                _session.TEMP9_PV = temp9;
                                _session.TEMP9_SP = temp9;
                            }
                            catch
                            {
                                _session.TEMP9_PV = 0;
                                _session.TEMP9_SP = 0;
                            }
                        }

                        #endregion

                        #region TEMP10

                        if (txtTEMP10PV.Text != "")
                        {
                            try
                            {
                                decimal temp10 = decimal.Parse(txtTEMP10PV.Text);
                                _session.TEMP10_PV = temp10;
                                _session.TEMP10_SP = temp10;
                            }
                            catch
                            {
                                _session.TEMP10_PV = 0;
                                _session.TEMP10_SP = 0;
                            }
                        }

                        #endregion

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
                    }

                    #endregion

                    #region HUMIDITY_AF
                    if (txtHUMIDITY_AF.Text != "")
                    {
                        _session.HUMIDITY_AF = decimal.Parse(txtHUMIDITY_AF.Text);
                    }
                    #endregion

                    #region HUMIDITY_BF
                    if (txtHUMIDITY_BF.Text != "")
                    {
                        _session.HUMIDITY_BF = decimal.Parse(txtHUMIDITY_BF.Text);
                    }
                    #endregion

                    if (cbShift.SelectedValue != null)
                    {
                        _session.OPERATOR_GROUP = cbShift.SelectedValue.ToString();
                    }

                    string result = _session.FINISHING_UPDATECOATINGProcessing();

                    if (string.IsNullOrEmpty(result) == true)
                    {
                        PageManager.Instance.Back();
                    }
                    else
                    {
                        result.ShowMessageBox(true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
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

                if (mcID != "")
                    LoadFinishing_GetCoating(mcID, "S");
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
