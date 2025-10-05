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
    /// Interaction logic for OldScouring1PreparingPage.xaml
    /// </summary>
    public partial class OldScouring1PreparingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public OldScouring1PreparingPage()
        {
            InitializeComponent();

            LoadShift();
            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Scouring1MachineConfig;

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
            Scouring1ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Scouring1>(Instance_ReadCompleted);
            Scouring1ModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<Scouring1> e)
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
                    cmdStart.Focus();
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
                cmdStart.Focus();
                e.Handled = true;
            }
        }

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
                string[] str = new string[] { "A", "B", "C"};

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
                cbCustomer.SelectedValue = null;
                cbItemGoods.SelectedValue = null;
                rbMassProduction.IsChecked = true;
                rbTest.IsChecked = false;
                rbGuide.IsChecked = false;
                txtItemWeaving.Text = "";
                txtLot.Text = "";
                txtLength.Text = "";
                OldLength = "";

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

                cbShift.SelectedIndex = 0;

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

            txtSATPV.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    Scouring1ModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<Scouring1>(Instance_ReadCompleted);
                    Scouring1ModbusManager.Instance.Start();
                }
                else
                {
                    "Machine Status = false Please check config".ShowMessageBox();
                }
            }
            else
            {
                Scouring1ModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<Scouring1>(Instance_ReadCompleted);
                Scouring1ModbusManager.Instance.Shutdown();
            }
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
                        //txtWIDTH_AFSpecification.Text = (items[0].WIDTH_AF.Value.ToString("#,##0.##") + " ± " + items[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##"));
                        txtWIDTH_AFSpecification.Text = ("( " + items[0].WIDTH_AF.Value.ToString("#,##0.##") + " )");
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

                    string result = _session.FINISHING_INSERTSCOURING();

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
