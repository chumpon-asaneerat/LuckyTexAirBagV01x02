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
    /// Interaction logic for Coating1ScouringPreparingPage.xaml
    /// </summary>
    public partial class Coating1ScouringPreparingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating1ScouringPreparingPage()
        {
            InitializeComponent();

            LoadShift();

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.Coating1ScouringMachineConfig;

            txtLength.IsEnabled = false;
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
            txtLot.Focus();
            txtLot.SelectAll();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<Coating1Scouring> e)
        {
            if (null == e.Value)
            {
                return;
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
                if (txtLength.IsEnabled == true)
                {
                    txtLength.Focus();
                    txtLength.SelectAll();
                }
                else
                {
                    cmdStart.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
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

                cbShift.SelectedIndex = 0;

                if (_session.Customer != "")
                    _session = new FinishingSession();

                chkReporcess.IsChecked = false;
                txtLength.IsEnabled = false;

                if (!string.IsNullOrEmpty(txtScouringNo.Text))
                    LoadFinishing_GetScouring(txtScouringNo.Text, "S");

                txtLot.SelectAll();
                txtLot.Focus();
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

        #region LoadFinishing_GetScouring

        private void LoadFinishing_GetScouring(string mcno, string flag)
        {
            try
            {
                List<FINISHING_GETDRYERDATA> items = _session.GetFINISHING_GETDRYERDATAList(flag, mcno);

                if (null != items && items.Count > 0 && null != items[0])
                {
                    gridProcess.ItemsSource = items;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
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
                        ClearData();
                        //PageManager.Instance.Back();
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
