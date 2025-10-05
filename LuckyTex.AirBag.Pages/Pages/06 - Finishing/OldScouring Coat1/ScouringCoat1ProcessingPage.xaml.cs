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
    /// Interaction logic for ScouringCoat1ProcessingPage.xaml
    /// </summary>
    public partial class ScouringCoat1ProcessingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ScouringCoat1ProcessingPage()
        {
            InitializeComponent();

            LoadShift();

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.ScouringCoat1MachineConfig;

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
            Coating1ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Coating1>(Instance_ReadCompleted);
            Coating1ModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<Coating1> e)
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

                txtSATPV.Text = e.Value.SATPV.ToString("n2").Replace(",", string.Empty);
                txtWASH1PV.Text = e.Value.WASH1PV.ToString("n2").Replace(",", string.Empty);
                txtWASH2PV.Text = e.Value.WASH2PV.ToString("n2").Replace(",", string.Empty);
                txtHOTPV.Text = e.Value.HOTPV.ToString("n2").Replace(",", string.Empty);
                txtSPEED.Text = e.Value.SPEED.Value.ToString("n2").Replace(",", string.Empty);

                //txtSEN1.Text = e.Value.SEN1.ToString("n2").Replace(",", string.Empty);
                //txtSEN2.Text = e.Value.SEN2.ToString("n2").Replace(",", string.Empty);


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

                if (e.Value.SPEED != null)
                {
                    _session.SPEED_PV = (decimal)(MathEx.Round(e.Value.SPEED.Value, 2));
                    _session.SPEED_SP = _session.SPEED_PV;
                }

                //if (e.Value.SPEED2 != null)
                //{
                //    _session.SPEED_SP = (decimal)(MathEx.Round(e.Value.SPEED2.Value, 2));
                //}

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

        private void txtItemWeaving_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLot.Focus();
                txtLot.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength.Focus();
                txtLength.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFINISHINGLOT.Focus();
                txtFINISHINGLOT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFINISHINGLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStartTime.Focus();
                txtStartTime.SelectAll();
                e.Handled = true;
            }
        }

        private void txtStartTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAINFRAMEWIDTHActual.Focus();
                txtMAINFRAMEWIDTHActual.SelectAll();
                e.Handled = true;
            }
        }

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
                txtSPEED.Focus();
                txtSPEED.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAINFRAMEWIDTHActual.Focus();
                txtMAINFRAMEWIDTHActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAINFRAMEWIDTHActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH_BEActual.Focus();
                txtWIDTH_BEActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH_BEActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH_AFActual.Focus();
                txtWIDTH_AFActual.SelectAll();
                e.Handled = true;
            }
        }

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
                if (txtTEMP1PV.IsEnabled == true)
                {
                    txtTEMP1PV.Focus();
                    txtTEMP1PV.SelectAll();
                    e.Handled = true;
                }
                else
                {
                    cmdSaveCondition.Focus();
                    e.Handled = true;
                }
            }
        }
        #endregion

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
                cmdSaveCondition.Focus();
                e.Handled = true;
            }
        }

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

            txtSATPV.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    Coating1ModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<Coating1>(Instance_ReadCompleted);
                    Coating1ModbusManager.Instance.Start();
                }
                else
                {
                    "Machine Status = false Please check config".ShowMessageBox();
                }
            }
            else
            {
                Coating1ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Coating1>(Instance_ReadCompleted);
                Coating1ModbusManager.Instance.Shutdown();
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


            if (txtSPEED.Text == "")
            {
                return false;
            }

            //if (txtMAINFRAMEWIDTHActual.Text == "")
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
                    txtStartTime.Text = items[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");
                    _session.STARTDATE = items[0].STARTDATE;

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
                    string msg = "Can't Load Scouring2";

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

                    //cmdSaveCondition.Focus();
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

                    if (txtMAINFRAMEWIDTHActual.Text != "")
                        _session.MAINFRAMEWIDTH = decimal.Parse(txtMAINFRAMEWIDTHActual.Text);

                    if (txtWIDTH_BEActual.Text != "")
                        _session.WIDTH_BE = decimal.Parse(txtWIDTH_BEActual.Text);

                    if (txtWIDTH_AFActual.Text != "")
                        _session.WIDTH_AF = decimal.Parse(txtWIDTH_AFActual.Text);

                    if (txtPIN2PINActual.Text != "")
                        _session.PIN2PIN = decimal.Parse(txtPIN2PINActual.Text);


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

                    string result = _session.FINISHING_UPDATESCOURINGProcessing();

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
                    LoadFinishing_GetScouring(mcID, "S");
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
