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
    /// Interaction logic for OldCoating1ScouringPreparingPage.xaml
    /// </summary>
    public partial class OldCoating1ScouringPreparingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public OldCoating1ScouringPreparingPage()
        {
            InitializeComponent();

            LoadShift();

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Coating1ScouringMachineConfig;

            txtLength.IsEnabled = false;

            txtClothHumidity.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITYBFSpecification.Visibility = System.Windows.Visibility.Collapsed;
            txtHUMIDITY_BF.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Internal Variables

        private FinishingSession _session = new FinishingSession();
        string OldLength = string.Empty;
        private bool mcStatus = true;

        #endregion

        #region Private Methods

        #region Inspection Session methods

        private void InitSession()
        {
            if (null != _session)
            {
                _session.OnStateChanged += new EventHandler(_session_OnStateChanged);
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
            EnabledCon(false);
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

                if (e.Value.SPEED != null)
                {
                    _session.SPEED_PV = (decimal)(MathEx.Round(e.Value.SPEED.Value, 2));

                    _session.SPEED_SP = _session.SPEED_PV;
                }

                _session.SATURATOR_PV = e.Value.SATPV;
                _session.SATURATOR_SP = e.Value.SATSP;

                _session.WASHING1_PV = e.Value.WASH1PV;
                _session.WASHING1_SP = e.Value.WASH1SP;
                _session.WASHING2_PV = e.Value.WASH2PV;
                _session.WASHING2_SP = e.Value.WASH2SP;
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

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        #endregion

        #region cmdStart_Click

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (cbCustomer.SelectedValue != null && cbItemGoods.SelectedValue != null && txtLot.Text != "" && txtLength.Text != "")
            {
                Start();
            }
            else
            {
                if (cbCustomer.SelectedValue == null)
                {
                    "Customer can't Null".ShowMessageBox(true);
                    return;
                }
                else if (cbItemGoods.SelectedValue == null)
                {
                    "Item Goods can't Null".ShowMessageBox(true);
                    return;
                }
                else if (txtLot.Text == "")
                {
                    "Lot can't Null".ShowMessageBox(true);
                    return;
                }
                else if (txtLength.Text == "")
                {
                    "Length can't Null".ShowMessageBox(true);
                    return;
                }
            }
        }

        #endregion

        #endregion

        #region ComboBox

        #region cbCustomer_SelectionChanged

        private void cbCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadItemGood
            try
            {
                List<CUS_GETITEMGOODBYCUSTOMERData> items = new List<CUS_GETITEMGOODBYCUSTOMERData>();

                if (cbCustomer.SelectedValue != null)
                {
                    string cusID = cbCustomer.SelectedValue.ToString();

                    if (cusID != "")
                    {
                        LoadItemGood(cusID);
                    }
                    else
                    {
                        this.cbItemGoods.ItemsSource = items;
                    }
                }
                else
                {
                    this.cbItemGoods.ItemsSource = items;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
            #endregion
        }

        #endregion

        #region cbItemGoods_SelectionChanged

        private void cbItemGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadFinishing
            try
            {
                if (cbItemGoods.SelectedIndex >= 0)
                {
                    if (cbItemGoods.SelectedValue != null && txtScouringNo.Text != "")
                    {
                        string ScouringNo = txtScouringNo.Text;
                        string itm_code = cbItemGoods.SelectedValue.ToString();

                        if (ScouringNo != "" && itm_code != "")
                        {
                            LoadFinishing(itm_code, ScouringNo);
                        }
                        else
                        {
                            ClearFinishing();
                        }
                    }
                    else
                    {
                        ClearFinishing();
                    }
                }
                else
                {
                    ClearFinishing();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                ClearFinishing();
            }
            #endregion
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
                    cmdStart.Focus();
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
                cmdStart.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region txtItemWeaving_LostFocus

        private void txtItemWeaving_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbItemGoods.SelectedValue != null && txtItemWeaving.Text != "")
            {
                string itm_code = cbItemGoods.SelectedValue.ToString();
                ScanWeavingLot(itm_code, txtItemWeaving.Text);
            }
        }

        #endregion

        #region txtLot_LostFocus

        private void txtLot_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLot.Text != "")
            {
                ScanLot(txtLot.Text);
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

        private void chkReporcess_Checked(object sender, RoutedEventArgs e)
        {
            if (txtLot.Text != "")
            {
                GETCURRENTINSDATA(txtLot.Text);
                txtLength.IsEnabled = true;
            }
            else
            {
                "Lot isn't Null".ShowMessageBox(false);
                chkReporcess.IsChecked = false;
                txtLength.IsEnabled = false;

                chkReporcess.IsChecked = false;
                txtLot.SelectAll();
                txtLot.Focus();
            }
        }

        private void chkReporcess_Unchecked(object sender, RoutedEventArgs e)
        {
            if (OldLength != "")
            {
                txtLength.Text = OldLength;
                txtLength.IsEnabled = false;
            }
        }


        #endregion

        #region private Methods

        #region Load Combo

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

        #region LoadCustomer

        private void LoadCustomer()
        {
            try
            {
                List<FinishingCustomerData> items = _session.Finishing_GetCustomerList();

                this.cbCustomer.ItemsSource = items;
                this.cbCustomer.DisplayMemberPath = "FINISHINGCUSTOMER";
                this.cbCustomer.SelectedValuePath = "FINISHINGCUSTOMER";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood(string cusID)
        {
            try
            {
                List<FINISHING_GETITEMGOODData> items = _session.GetFINISHING_GETITEMGOOD(cusID);

                this.cbItemGoods.ItemsSource = items;
                this.cbItemGoods.DisplayMemberPath = "ITM_CODE";
                this.cbItemGoods.SelectedValuePath = "ITM_CODE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

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
                cbCustomer.SelectedValue = null;
                cbItemGoods.SelectedValue = null;
                rbMassProduction.IsChecked = true;
                rbTest.IsChecked = false;
                rbGuide.IsChecked = false;
                txtItemWeaving.Text = "";
                txtLot.Text = "";
                txtLength.Text = "";
                OldLength = "";

                txtWIDTH_BE_HEATActual.Text = "";
                txtHOTPV.Text = "";

                txtSPEED.Text = "";
                txtSTEAMPRESSUREActual.Text = "";
                txtDRYERUPCIRCUFANActual.Text = "";
                txtEXHAUSTFANActual.Text = "";
                txtWIDTH_AF_HEATActual.Text = "";
                txtHUMIDITY_BF.Text = "";
                txtHUMIDITY_AF.Text = "";

                cbShift.SelectedIndex = 0;

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

                    cbItemGoods.Text = "";
                    cbItemGoods.SelectedValue = null;
                    cbItemGoods.SelectedIndex = -1;

                    cbItemGoods.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region ScanWeavingLot

        private void ScanWeavingLot(string itm_code, string itm_weaving)
        {
            try
            {
                List<FINISHING_CHECKITEMWEAVINGData> items = _session.GetFINISHING_CHECKITEMWEAVING(itm_code, itm_weaving);

                if (items != null && items.Count > 0)
                {

                }
                else
                {
                    string msg = "This Item Weaving does not map with selected item Good";

                    msg.ShowMessageBox(false);

                    txtItemWeaving.Text = "";
                    txtLot.Text = "";
                    txtLength.Text = "";
                    OldLength = "";
                    txtItemWeaving.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region ScanLot

        private void ScanLot(string WEAVINGLOT)
        {
            try
            {
                List<GETWEAVINGINGDATA> items = _session.GetWeavingingDataList(WEAVINGLOT);

                if (items != null && items.Count > 0)
                {
                    txtItemWeaving.Text = items[0].ITM_WEAVING;
                    txtLot.Text = items[0].WEAVINGLOT;
                    OldLength = items[0].LENGTH.Value.ToString("#,##0.##");
                    txtLength.Text = OldLength;

                    if (cbItemGoods.SelectedValue != null && txtItemWeaving.Text != "")
                    {
                        string itm_code = cbItemGoods.SelectedValue.ToString();
                        ScanWeavingLot(itm_code, txtItemWeaving.Text);
                    }
                }
                else
                {
                    string msg = "This Item Weaving does not map with selected item Good";

                    msg.ShowMessageBox(false);

                    txtItemWeaving.Text = "";
                    txtLot.Text = "";
                    txtLength.Text = "";
                    OldLength = "";
                    txtLot.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region GETCURRENTINSDATA

        private void GETCURRENTINSDATA(string FINISHINGLOT)
        {
            if (txtLength.Text != "")
            {
                OldLength = txtLength.Text;
            }

            GETCURRENTINSDATA dbResults = FinishingDataService.Instance.GETCURRENTINSDATA(FINISHINGLOT);

            if (dbResults != null)
            {
                if (dbResults.ACTUALLENGTH != null)
                    txtLength.Text = dbResults.ACTUALLENGTH.Value.ToString("#,##0.##");
            }
        }

        #endregion

        #region Start

        private void Start()
        {
            try
            {
                string weavlot = txtLot.Text;
                string itmCode = cbItemGoods.SelectedValue.ToString();
                string finishcustomer = cbCustomer.SelectedValue.ToString();
                string PRODUCTTYPEID = string.Empty;
                string operatorid = txtOperator.Text;
                string MCNO = txtScouringNo.Text;
                string flag = "S";

                if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false && rbGuide.IsChecked == false)
                {
                    PRODUCTTYPEID = "1";
                }
                else if (rbMassProduction.IsChecked == false && rbTest.IsChecked == true && rbGuide.IsChecked == false)
                {
                    PRODUCTTYPEID = "2";
                }
                else if (rbMassProduction.IsChecked == false && rbTest.IsChecked == false && rbGuide.IsChecked == true)
                {
                    PRODUCTTYPEID = "3";
                }

                if (weavlot != "" && itmCode != "" && finishcustomer != "" && operatorid != "" && MCNO != "")
                {
                    _session.WEAVINGLOT = weavlot;
                    _session.ItemCode = itmCode;
                    _session.Customer = finishcustomer;
                    _session.PRODUCTTYPEID = PRODUCTTYPEID;
                    _session.Operator = operatorid;
                    _session.MCNO = MCNO;
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

                    #region REPROCESS

                    if (chkReporcess.IsChecked == true)
                        _session.REPROCESS = "Y";
                    else
                        _session.REPROCESS = "N";

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

                    string result = _session.FINISHING_INSERTDRYER();

                    if (string.IsNullOrEmpty(result) == true)
                    {
                        //ClearData();
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

            if (null != _session)
            {
                SetupOperatorAndMC(session.CurrentUser.OperatorId, _session.Machine.DisplayName, session.Machine.MCId);
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
                LoadCustomer();
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
