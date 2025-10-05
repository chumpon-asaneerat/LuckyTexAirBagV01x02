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
    /// Interaction logic for FinishingDryerPage.xaml
    /// </summary>
    public partial class FinishingDryerPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingDryerPage()
        {
            InitializeComponent();
            LoadItemGood();
            LoadDryer();
        }

        #endregion

        #region Internal Variables

        private ProcessConditionSession _session = new ProcessConditionSession();

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            EnabledCon(false);
            ClearData();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemGoods.SelectedValue != null)
            {
                EnabledCon(true);

                ClearData();

                txtWIDTH_BE_HEATSpecification.Focus();
                txtWIDTH_BE_HEATSpecification.SelectAll();
            }
            else
            {
                "Item Code isn't Null".ShowMessageBox();
            }
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemGoods.SelectedValue != null)
            {
                EnabledCon(true);

                txtWIDTH_BE_HEATSpecification.Focus();
                txtWIDTH_BE_HEATSpecification.SelectAll();
            }
            else
            {
                "Item Code isn't Null".ShowMessageBox();
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemGoods.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(txtWIDTH_BE_HEATActual.Text))
                    {
                        if (Save() == true)
                        {
                            "Save complete".ShowMessageBox(false);
                            EnabledCon(false);
                            
                            cbItemGoods.Text = "";
                            cbItemGoods.SelectedValue = null;
                            cbItemGoods.SelectedIndex = -1;

                            ClearData();
                        }
                    }
                    else
                    {
                        "Chemical Temp isn't Null".ShowMessageBox();
                    }
            }
            else
            {
                "Item Code isn't Null".ShowMessageBox();
            }
        }

        #endregion

        #region ComboBox

        #region cbDryer_SelectionChanged

        private void cbDryer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadFinishing
            try
            {
                if (chkAll.IsChecked == false)
                {
                    if (cbDryer.SelectedValue != null && cbItemGoods.SelectedValue != null)
                    {
                        string ScouringNo = string.Empty;

                        if (cbDryer.SelectedValue.ToString() == "Scouring Dryer")
                            ScouringNo = "272";
                        else if (cbDryer.SelectedValue.ToString() == "Coating1 Dryer")
                            ScouringNo = "273";

                        string itm_code = cbItemGoods.SelectedValue.ToString();

                        if (!string.IsNullOrEmpty(itm_code) && !string.IsNullOrEmpty(ScouringNo))
                        {
                            LoadFinishing(itm_code, ScouringNo);
                        }
                        else
                        {
                            ClearData();
                        }
                    }
                    else
                    {
                        ClearData();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                ClearData();
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
                if (chkAll.IsChecked == false)
                {
                    if (cbItemGoods.SelectedValue != null && cbDryer.SelectedValue != null)
                    {
                        string itm_code = cbItemGoods.SelectedValue.ToString();

                        string ScouringNo = string.Empty;
                        if (cbDryer.SelectedValue.ToString() == "Scouring Dryer")
                            ScouringNo = "272";
                        else if (cbDryer.SelectedValue.ToString() == "Coating1 Dryer")
                            ScouringNo = "273";

                        if (!string.IsNullOrEmpty(itm_code) && !string.IsNullOrEmpty(ScouringNo))
                        {
                            LoadFinishing(itm_code, ScouringNo);
                        }
                        else
                        {
                            ClearData();
                        }
                    }
                    else
                    {
                        ClearData();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                ClearData();
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

        private void txtWIDTH_BE_HEATSpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtACCPRESURESpecification.Focus();
                txtACCPRESURESpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtACCPRESURESpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtASSTENSIONSpecification.Focus();
                txtASSTENSIONSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtASSTENSIONSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtACCARIDENSERSpecification.Focus();
                txtACCARIDENSERSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtACCARIDENSERSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCHIFROTSpecification.Focus();
                txtCHIFROTSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCHIFROTSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCHIREARSpecification.Focus();
                txtCHIREARSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCHIREARSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDRYERTEMP1Specification.Focus();
                txtDRYERTEMP1Specification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDRYERTEMP1Specification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDRYERTEMP1_MARGIN.Focus();
                txtDRYERTEMP1_MARGIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDRYERTEMP1_MARGIN_KeyDown(object sender, KeyEventArgs e)
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
                txtSPEED_MARGIN.Focus();
                txtSPEED_MARGIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSPEED_MARGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTEAMPRESSURESpecification.Focus();
                txtSTEAMPRESSURESpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTEAMPRESSURESpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDRYERUPCIRCUFANSpecification.Focus();
                txtDRYERUPCIRCUFANSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDRYERUPCIRCUFANSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXHAUSTFANSpecification.Focus();
                txtEXHAUSTFANSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXHAUSTFANSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH_AF_HEATSpecification.Focus();
                txtWIDTH_AF_HEATSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH_AF_HEATSpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtHUMIDITYMAX.Focus();
                txtHUMIDITYMAX.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHUMIDITYMAX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHUMIDITYMIN.Focus();
                txtHUMIDITYMIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHUMIDITYMIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region private Methods

        #region Load Combo

        #region LoadDryer

        private void LoadDryer()
        {
            if (cbDryer.ItemsSource == null)
            {
                string[] str = new string[] { "Scouring Dryer", "Coating1 Dryer" };

                cbDryer.ItemsSource = str;
                cbDryer.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMCODELIST> items = _session.GetItemCodeData();

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
                //cbItemGoods.SelectedValue = null;
                //cbDryer.SelectedValue = 0;
                //cbDryer.Text = "Scouring Dryer";
                chkAll.IsChecked = false;

                txtWIDTH_BE_HEATSpecification.Text = "";
                txtACCPRESURESpecification.Text = "";
                txtASSTENSIONSpecification.Text = "";
                txtACCARIDENSERSpecification.Text = "";
                txtCHIFROTSpecification.Text = "";
                txtCHIREARSpecification.Text = "";
                txtDRYERTEMP1Specification.Text = "";
                txtSPEED.Text = "";

                txtSTEAMPRESSURESpecification.Text = "";
                txtDRYERUPCIRCUFANSpecification.Text = "";

                txtEXHAUSTFANSpecification.Text = "";
                txtWIDTH_AF_HEATSpecification.Text = "";

                txtHUMIDITYMAX.Text = "";
                txtWIDTH_BE_HEATActual.Text = "";
                txtDRYERTEMP1_MARGIN.Text = "";

                txtSPEED_MARGIN.Text = "";
                txtWIDTH_AF_HEATActual.Text = "";

                txtHUMIDITYMIN.Text = "";
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
            txtWIDTH_BE_HEATSpecification.IsEnabled = chkManual;
            txtACCPRESURESpecification.IsEnabled = chkManual;
            txtASSTENSIONSpecification.IsEnabled = chkManual;
            txtACCARIDENSERSpecification.IsEnabled = chkManual;
            txtCHIFROTSpecification.IsEnabled = chkManual;
            txtCHIREARSpecification.IsEnabled = chkManual;
            txtDRYERTEMP1Specification.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            txtSTEAMPRESSURESpecification.IsEnabled = chkManual;
            txtDRYERUPCIRCUFANSpecification.IsEnabled = chkManual;

            txtEXHAUSTFANSpecification.IsEnabled = chkManual;
            txtWIDTH_AF_HEATSpecification.IsEnabled = chkManual;

            txtHUMIDITYMAX.IsEnabled = chkManual;
            txtWIDTH_BE_HEATActual.IsEnabled = chkManual;
            txtDRYERTEMP1_MARGIN.IsEnabled = chkManual;

            txtSPEED_MARGIN.IsEnabled = chkManual;
            txtWIDTH_AF_HEATActual.IsEnabled = chkManual;
            txtHUMIDITYMIN.IsEnabled = chkManual;
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
                    #region WIDTH_BE_HEAT_MAX

                    if (items[0].WIDTH_BE_HEAT_MAX != null)
                    {
                        txtWIDTH_BE_HEATSpecification.Text = items[0].WIDTH_BE_HEAT_MAX.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_BE_HEATSpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_BE_HEAT_MIN

                    if (items[0].WIDTH_BE_HEAT_MIN != null)
                    {
                        txtWIDTH_BE_HEATActual.Text = items[0].WIDTH_BE_HEAT_MIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_BE_HEATActual.Text = "";
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

                    if (items[0].DRYERTEMP1 != null)
                    {
                        txtDRYERTEMP1Specification.Text = items[0].DRYERTEMP1.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtDRYERTEMP1Specification.Text = "";
                    }

                    #endregion

                    #region DRYERTEMP1_MARGIN

                    if (items[0].DRYERTEMP1_MARGIN != null)
                    {
                        txtDRYERTEMP1_MARGIN.Text = items[0].DRYERTEMP1_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtDRYERTEMP1_MARGIN.Text = "";
                    }

                    #endregion

                    #region SPEED

                    if (items[0].SPEED != null)
                    {
                        txtSPEED.Text = items[0].SPEED.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSPEED.Text = "";
                    }

                    #endregion

                    #region SPEED_MARGIN

                    if (items[0].SPEED_MARGIN != null)
                    {
                        txtSPEED_MARGIN.Text = items[0].SPEED_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSPEED_MARGIN.Text = "";
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

                    if (items[0].WIDTH_AF_HEAT != null )
                    {
                        txtWIDTH_AF_HEATSpecification.Text = items[0].WIDTH_AF_HEAT.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_AF_HEATSpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_AF_HEAT_MARGIN

                    if (items[0].WIDTH_AF_HEAT_MARGIN != null)
                    {
                        txtWIDTH_AF_HEATActual.Text = items[0].WIDTH_AF_HEAT_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_AF_HEATActual.Text = "";
                    }

                    #endregion

                    #region HUMIDITY_MAX

                    if (items[0].HUMIDITY_MAX != null)
                    {
                        txtHUMIDITYMAX.Text = items[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtHUMIDITYMAX.Text = "";
                    }

                    #endregion

                    #region HUMIDITY_MIN

                    if (items[0].HUMIDITY_MIN != null)
                    {
                        txtHUMIDITYMIN.Text = items[0].HUMIDITY_MIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtHUMIDITYMIN.Text = "";
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
                    string msg = "No Dryer Condition found for selected item";

                    msg.ShowMessageBox(false);

                    EnabledCon(false);
                    ClearData();

                    //cbItemGoods.Text = "";
                    //cbItemGoods.SelectedValue = null;
                    //cbItemGoods.SelectedIndex = -1;

                    //cbItemGoods.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        private bool Save()
        {
            string P_ITEMCODE = string.Empty;
            decimal? P_WIDTH_BE_HEAT_MAX = null;
            decimal? P_WIDTH_BE_HEAT_MIN = null;
            decimal? P_ACCPRESURE = null;
            decimal? P_ASSTENSION = null;
            decimal? P_ACCARIDENSER = null;
            decimal? P_CHIFROT = null;
            decimal? P_CHIREAR = null;
            decimal? P_DRYERTEMP = null;
            decimal? P_DRYERTEMP_MARGIN = null;
            decimal? P_SPEED = null;
            decimal? P_SPEED_MARGIN = null;
            decimal? P_STEAMPRESURE = null;
            decimal? P_DRYERUPCIRCUFAN = null;
            decimal? P_EXHAUSTFAN = null;
            decimal? P_WIDTH_AF_HEAT = null;
            decimal? P_WIDTH_AF_HEAT_MARGIN = null;
            decimal? P_HUMIDITYMAX = null;
            decimal? P_HUMIDITYMIN = null;
            string P_OPERATOR = string.Empty;

            string P_MC = string.Empty;

            try
            {
                string RESULT = string.Empty;

                if (cbItemGoods.SelectedValue != null)
                    P_ITEMCODE = cbItemGoods.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtWIDTH_BE_HEATSpecification.Text))
                    P_WIDTH_BE_HEAT_MAX = decimal.Parse(txtWIDTH_BE_HEATSpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_BE_HEATActual.Text))
                    P_WIDTH_BE_HEAT_MIN = decimal.Parse(txtWIDTH_BE_HEATActual.Text);

                if (!string.IsNullOrEmpty(txtACCPRESURESpecification.Text))
                    P_ACCPRESURE = decimal.Parse(txtACCPRESURESpecification.Text);

                if (!string.IsNullOrEmpty(txtASSTENSIONSpecification.Text))
                    P_ASSTENSION = decimal.Parse(txtASSTENSIONSpecification.Text);

                if (!string.IsNullOrEmpty(txtACCARIDENSERSpecification.Text))
                    P_ACCARIDENSER = decimal.Parse(txtACCARIDENSERSpecification.Text);

                if (!string.IsNullOrEmpty(txtCHIFROTSpecification.Text))
                    P_CHIFROT = decimal.Parse(txtCHIFROTSpecification.Text);

                if (!string.IsNullOrEmpty(txtCHIREARSpecification.Text))
                    P_CHIREAR = decimal.Parse(txtCHIREARSpecification.Text);

                if (!string.IsNullOrEmpty(txtDRYERTEMP1Specification.Text))
                    P_DRYERTEMP = decimal.Parse(txtDRYERTEMP1Specification.Text);

                if (!string.IsNullOrEmpty(txtDRYERTEMP1_MARGIN.Text))
                    P_DRYERTEMP_MARGIN = decimal.Parse(txtDRYERTEMP1_MARGIN.Text);

                if (!string.IsNullOrEmpty(txtSPEED.Text))
                    P_SPEED = decimal.Parse(txtSPEED.Text);

                if (!string.IsNullOrEmpty(txtSPEED_MARGIN.Text))
                    P_SPEED_MARGIN = decimal.Parse(txtSPEED_MARGIN.Text);

                if (!string.IsNullOrEmpty(txtSTEAMPRESSURESpecification.Text))
                    P_STEAMPRESURE = decimal.Parse(txtSTEAMPRESSURESpecification.Text);

                if (!string.IsNullOrEmpty(txtDRYERUPCIRCUFANSpecification.Text))
                    P_DRYERUPCIRCUFAN = decimal.Parse(txtDRYERUPCIRCUFANSpecification.Text);

                if (!string.IsNullOrEmpty(txtEXHAUSTFANSpecification.Text))
                    P_EXHAUSTFAN = decimal.Parse(txtEXHAUSTFANSpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_AF_HEATSpecification.Text))
                    P_WIDTH_AF_HEAT = decimal.Parse(txtWIDTH_AF_HEATSpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_AF_HEATActual.Text))
                    P_WIDTH_AF_HEAT_MARGIN = decimal.Parse(txtWIDTH_AF_HEATActual.Text);

                if (!string.IsNullOrEmpty(txtHUMIDITYMAX.Text))
                    P_HUMIDITYMAX = decimal.Parse(txtHUMIDITYMAX.Text);

                if (!string.IsNullOrEmpty(txtHUMIDITYMIN.Text))
                    P_HUMIDITYMIN = decimal.Parse(txtHUMIDITYMIN.Text);

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;

                if (cbDryer.SelectedValue != null)
                {
                    if (cbDryer.SelectedValue.ToString() == "Scouring Dryer")
                        P_MC = "272";
                    else if (cbDryer.SelectedValue.ToString() == "Coating1 Dryer")
                        P_MC = "273";
                }

                RESULT = ProcessConditionDataService.Instance.CONDITION_FINISHINGDRYER(P_ITEMCODE, P_WIDTH_BE_HEAT_MAX, P_WIDTH_BE_HEAT_MIN,
                         P_ACCPRESURE, P_ASSTENSION, P_ACCARIDENSER, P_CHIFROT, P_CHIREAR, P_DRYERTEMP,
                         P_DRYERTEMP_MARGIN, P_SPEED, P_SPEED_MARGIN, P_STEAMPRESURE, P_DRYERUPCIRCUFAN, P_EXHAUSTFAN,
                         P_WIDTH_AF_HEAT, P_WIDTH_AF_HEAT_MARGIN, P_HUMIDITYMAX, P_HUMIDITYMIN, P_OPERATOR, P_MC);

                if (!string.IsNullOrEmpty(RESULT))
                {
                    RESULT.ShowMessageBox(true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                return false;
            }
        }

        #endregion

        #region Public Methods

        #region Setup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="opera"></param>
        public void Setup(string opera)
        {
            if (null != opera)
            {
                txtOperator.Text = opera;
            }
            else
            {
                txtOperator.Text = "-";
            }
        }

        #endregion

        #endregion

    }
}
