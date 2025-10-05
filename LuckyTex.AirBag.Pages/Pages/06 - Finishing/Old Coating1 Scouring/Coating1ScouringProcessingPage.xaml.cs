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
    /// Interaction logic for Coating1ScouringProcessingPage.xaml
    /// </summary>
    public partial class Coating1ScouringProcessingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating1ScouringProcessingPage()
        {
            InitializeComponent();

            LoadShift();

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Coating1ScouringMachineConfig;

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
            Coating1ScouringModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Coating1Scouring>(Instance_ReadCompleted);
            Coating1ScouringModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<Coating1Scouring> e)
        {
            if (null == e.Value)
            {
                return;
            }
            if (chkManual.IsChecked.HasValue && !chkManual.IsChecked.Value)
            {
                txtHOTPV.Text = e.Value.HOTPV.ToString("n2").Replace(",", string.Empty);
                txtSPEED.Text = e.Value.SPEED.Value.ToString("n2").Replace(",", string.Empty);

                txtSATPV.Text = e.Value.SATPV.ToString("n2").Replace(",", string.Empty);
                txtWASH1PV.Text = e.Value.WASH1PV.ToString("n2").Replace(",", string.Empty);
                txtWASH2PV.Text = e.Value.WASH2PV.ToString("n2").Replace(",", string.Empty);

                _session.HOTFLUE_PV = e.Value.HOTPV;
                _session.HOTFLUE_SP = e.Value.HOTSP;

                _session.SATURATOR_PV = e.Value.SATPV;
                _session.SATURATOR_SP = e.Value.SATSP;
                _session.WASHING1_PV = e.Value.WASH1PV;
                _session.WASHING1_SP = e.Value.WASH1SP;
                _session.WASHING2_PV = e.Value.WASH2PV;
                _session.WASHING2_SP = e.Value.WASH2SP;

                if (e.Value.SPEED != null)
                {
                    _session.SPEED_PV = (decimal)(MathEx.Round(e.Value.SPEED.Value, 2));

                    _session.SPEED_SP = _session.SPEED_PV;
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
                txtWIDTH_BE_HEATActual.Focus();
                txtWIDTH_BE_HEATActual.SelectAll();
                e.Handled = true;
            }
        }
    
        private void txtWIDTH_BE_HEATActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtHOTPV.IsEnabled == true)
                {
                    txtHOTPV.Focus();
                    txtHOTPV.SelectAll();
                    e.Handled = true;
                }
                else
                {
                    txtSTEAMPRESSUREActual.Focus();
                    txtSTEAMPRESSUREActual.SelectAll();
                    e.Handled = true;
                }
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
                if (txtSATPV.IsEnabled == true)
                {
                    txtSATPV.Focus();
                    txtSATPV.SelectAll();
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
            txtWIDTH_BE_HEATSpecification.Text = "";

            txtDRYERTEMP1Specification.Text = "";
            txtSPEEDSpecification.Text = "";

            txtSTEAMPRESSURESpecification.Text = "";
            txtDRYERUPCIRCUFANSpecification.Text = "";

            txtEXHAUSTFANSpecification.Text = "";
            txtWIDTH_AF_HEATSpecification.Text = "";

            txtHUMIDITYBFSpecification.Text = "";
            txtHUMIDITYAFSpecification.Text = "";

            txtSATURATOR_CHEMSpecification.Text = "";
            txtWASHING1Specification.Text = "";
            txtWASHING2Specification.Text = "";
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
                txtHOTPV.Text = "";

                txtSPEED.Text = "";
                txtSTEAMPRESSUREActual.Text = "";
                txtDRYERUPCIRCUFANActual.Text = "";
                txtEXHAUSTFANActual.Text = "";
                txtWIDTH_AF_HEATActual.Text = "";
                txtHUMIDITY_BF.Text = "";
                txtHUMIDITY_AF.Text = "";

                txtSATPV.Text = "";
                txtWASH1PV.Text = "";
                txtWASH2PV.Text = "";

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
            txtHOTPV.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            txtSATPV.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    Coating1ScouringModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<Coating1Scouring>(Instance_ReadCompleted);
                    Coating1ScouringModbusManager.Instance.Start();
                }
                else
                {
                    "Machine Status = false Please check config".ShowMessageBox();
                }
            }
            else
            {
                Coating1ScouringModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Coating1Scouring>(Instance_ReadCompleted);
                Coating1ScouringModbusManager.Instance.Shutdown();
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

            //if (txtWIDTH_BE_HEATActual.Text == "")
            //{
            //    return false;
            //}
            if (txtSPEED.Text == "")
            {
                return false;
            }

            //if (txtSTEAMPRESSUREActual.Text == "")
            //{
            //    return false;
            //}

            //if (txtDRYERUPCIRCUFANActual.Text == "")
            //{
            //    return false;
            //}

            //if (txtEXHAUSTFANActual.Text == "")
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

            return chkSave;
        }
        #endregion

        #region LoadFinishing

        private void LoadFinishing(string itm_code,string mcNo)
        {
            try
            {
                List<FINISHING_GETDRYERCONDITIONData> items = _session.GetFINISHING_GETDRYERCONDITION(itm_code, mcNo);

                if (items != null && items.Count > 0)
                {
                    #region WIDTH_BE_HEAT

                    if (items[0].WIDTH_BE_HEAT_MAX != null && items[0].WIDTH_BE_HEAT_MIN != null)
                    {
                        txtWIDTH_BE_HEATSpecification.Text = (items[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##") + " - " + items[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##"));
                    }
                    else
                    {
                        txtWIDTH_BE_HEATSpecification.Text = "";
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
                        txtSTEAMPRESSURESpecification.Text = items[0].STEAMPRESSURE.Value.ToString("#,##0.##");
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

        #region LoadFinishing_GetScouring

        private void LoadFinishing_GetScouring(string flag, string mcNo)
        {
            try
            {
                List<FINISHING_GETDRYERDATA> items = _session.GetFINISHING_GETDRYERDATAList(flag, mcNo);

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
                    
                    txtWIDTH_BE_HEATActual.Text = items[0].WIDTH_BE_HEAT.ToString();
                    txtHOTPV.Text = items[0].DRYERTEMP1_PV.ToString();

                    txtSPEED.Text = items[0].SPEED_PV.ToString();
                    txtSTEAMPRESSUREActual.Text = items[0].STEAMPRESSURE.ToString();
                    txtDRYERUPCIRCUFANActual.Text = items[0].DRYERCIRCUFAN.ToString();
                    txtEXHAUSTFANActual.Text = items[0].EXHAUSTFAN.ToString();
                    txtWIDTH_AF_HEATActual.Text = items[0].WIDTH_AF_HEAT.ToString();

                    txtHUMIDITY_AF.Text = items[0].HUMIDITY_AF.ToString();
                    txtHUMIDITY_BF.Text = items[0].HUMIDITY_BF.ToString();

                    txtSATPV.Text = items[0].SATURATOR_PV.ToString();
                    txtWASH1PV.Text = items[0].WASHING1_PV.ToString();
                    txtWASH2PV.Text = items[0].WASHING2_PV.ToString();

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

                    _session.SATURATOR_PV = items[0].SATURATOR_PV;
                    _session.SATURATOR_SP = items[0].SATURATOR_SP;
                    _session.WASHING1_PV = items[0].WASHING1_PV;
                    _session.WASHING1_SP = items[0].WASHING1_SP;
                    _session.WASHING2_PV = items[0].WASHING2_PV;
                    _session.WASHING2_SP = items[0].WASHING2_SP;

                    chkLoad = true;
                }
                else
                {
                    string msg = "Can't Load Coat #1 Scouring-Dry";

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

                    if (txtWIDTH_BE_HEATActual.Text != "")
                        _session.WIDTHBEHEAT = decimal.Parse(txtWIDTH_BE_HEATActual.Text);

                    if (txtSTEAMPRESSUREActual.Text != "")
                        _session.STEAMPRESURE = decimal.Parse(txtSTEAMPRESSUREActual.Text);

                    if (txtDRYERUPCIRCUFANActual.Text != "")
                        _session.DRYCIRCUFAN = decimal.Parse(txtDRYERUPCIRCUFANActual.Text);

                    if (txtEXHAUSTFANActual.Text != "")
                        _session.EXHAUSTFAN = decimal.Parse(txtEXHAUSTFANActual.Text);

                    if (txtWIDTH_AF_HEATActual.Text != "")
                        _session.WIDTHAFHEAT = decimal.Parse(txtWIDTH_AF_HEATActual.Text);

                    #region chkManual

                    if (chkManual.IsChecked == true)
                    {
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

                    string result = _session.FINISHING_UPDATEDRYERProcessing();

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
                    LoadFinishing_GetScouring("S", mcID);
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
