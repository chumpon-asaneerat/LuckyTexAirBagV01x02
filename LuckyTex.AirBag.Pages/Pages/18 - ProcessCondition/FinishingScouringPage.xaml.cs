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
    /// Interaction logic for FinishingScouringPage.xaml
    /// </summary>
    public partial class FinishingScouringPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingScouringPage()
        {
            InitializeComponent();
            LoadScouring();
            LoadItemGood();
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
                if (cbScouring.Text != "")
                {
                    EnabledCon(true);

                    ClearData();
                }
                else
                {
                    "Scouring MC isn't Null".ShowMessageBox();
                }
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
                if (cbScouring.Text != "")
                {
                    EnabledCon(true);
                }
                else
                {
                    "Scouring MC isn't Null".ShowMessageBox();
                }
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
                if (cbScouring.Text != "")
                {
                    if (!string.IsNullOrEmpty(txtSATPV.Text))
                    {
                        if (Save() == true)
                        {
                            "Save complete".ShowMessageBox(false);
                            EnabledCon(false);

                            cbScouring.SelectedValue = 0;
                            cbScouring.Text = "Scouring 1";
                            cbItemGoods.SelectedValue = null;

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
                    "Scouring MC isn't Null".ShowMessageBox();
                }
            }
            else
            {
                "Item Code isn't Null".ShowMessageBox();
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

        private void txtSATURATOR_CHEMSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSATPV.Focus();
                txtSATPV.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSATPV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWASHING1Specification.Focus();
                txtWASHING1Specification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWASHING1Specification_KeyDown(object sender, KeyEventArgs e)
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
                txtWASHING2Specification.Focus();
                txtWASHING2Specification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWASHING2Specification_KeyDown(object sender, KeyEventArgs e)
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
                txtHOTFLUESpecification.Focus();
                txtHOTFLUESpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHOTFLUESpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtSPEEDSpecification.Focus();
                txtSPEEDSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSPEEDSpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtMAINFRAMEWIDTHSpecification.Focus();
                txtMAINFRAMEWIDTHSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAINFRAMEWIDTHSpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtWIDTH_BESpecification.Focus();
                txtWIDTH_BESpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH_BESpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtWIDTH_AFSpecification.Focus();
                txtWIDTH_AFSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH_AFSpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtPIN2PINSpecification.Focus();
                txtPIN2PINSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtPIN2PINSpecification_KeyDown(object sender, KeyEventArgs e)
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

        #region txtHUMIDITYMIN_KeyDown
        private void txtHUMIDITYMIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtROOMTEMP.Focus();
                txtROOMTEMP.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        private void txtROOMTEMP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtROOMTEMP_MARGIN.Focus();
                txtROOMTEMP_MARGIN.SelectAll();
                e.Handled = true;
            }
        }

        #region txtROOMTEMP_MARGIN_KeyDown

        private void txtROOMTEMP_MARGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region ComboBox

        #region cbScouring_SelectionChanged

        private void cbScouring_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadFinishing
            try
            {
                if (chkAll.IsChecked == false)
                {
                    if (cbItemGoods.SelectedValue != null && cbScouring.SelectedValue != null)
                    {
                        string ScouringNo = string.Empty;

                        if (cbScouring.SelectedValue.ToString() == "Scouring 1")
                            ScouringNo = "252";
                        else if (cbScouring.SelectedValue.ToString() == "Scouring 2")
                            ScouringNo = "253";

                        string itm_code = cbItemGoods.SelectedValue.ToString();

                        if (ScouringNo != "" && itm_code != "")
                        {
                            LoadFinishing(itm_code, ScouringNo);
                        }
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
                    if (cbItemGoods.SelectedValue != null && cbScouring.SelectedValue != null)
                    {
                        string ScouringNo = string.Empty;

                        if (cbScouring.SelectedValue.ToString() == "Scouring 1")
                            ScouringNo = "252";
                        else if (cbScouring.SelectedValue.ToString() == "Scouring 2")
                            ScouringNo = "253";

                        string itm_code = cbItemGoods.SelectedValue.ToString();

                        if (ScouringNo != "" && itm_code != "")
                        {
                            LoadFinishing(itm_code, ScouringNo);
                        }
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

        #region private Methods

        #region Load Combo

        #region LoadScouring

        private void LoadScouring()
        {
            if (cbScouring.ItemsSource == null)
            {
                string[] str = new string[] { "Scouring 1", "Scouring 2" };

                cbScouring.ItemsSource = str;
                cbScouring.SelectedIndex = 0;
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
                //cbScouring.SelectedValue = 0;
                //cbScouring.Text = "Scouring 1";
                //cbItemGoods.SelectedValue = null;
                chkAll.IsChecked = false;

                txtSATURATOR_CHEMSpecification.Text = "";
                txtSATPV.Text = "";
                txtWASHING1Specification.Text = "";
                txtWASH1PV.Text = "";
                txtWASHING2Specification.Text = "";
                txtWASH2PV.Text = "";
                txtHOTFLUESpecification.Text = "";
                txtHOTPV.Text = "";
               
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
                txtHUMIDITYMIN.Text = "";
                txtHUMIDITYMAX.Text = "";

                txtROOMTEMP.Text = "";
                txtROOMTEMP_MARGIN.Text = "";
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
            txtSATPV.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;

            txtSPEEDSpecification.IsEnabled = chkManual;
            txtMAINFRAMEWIDTHSpecification.IsEnabled = chkManual;
            txtWIDTH_BESpecification.IsEnabled = chkManual;
            txtWIDTH_AFSpecification.IsEnabled = chkManual;
            txtPIN2PINSpecification.IsEnabled = chkManual;

            txtSATURATOR_CHEMSpecification.IsEnabled = chkManual;
            txtWASHING1Specification.IsEnabled = chkManual;
            txtWASHING2Specification.IsEnabled = chkManual;
            txtHOTFLUESpecification.IsEnabled = chkManual;

            txtHUMIDITYMAX.IsEnabled = chkManual;
            txtROOMTEMP_MARGIN.IsEnabled = chkManual;

            txtSATURATOR_CHEMSpecification.IsEnabled = chkManual;
            txtSATPV.IsEnabled = chkManual;
            txtWASHING1Specification.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASHING2Specification.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTFLUESpecification.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;

            txtSPEEDSpecification.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;
            txtMAINFRAMEWIDTHSpecification.IsEnabled = chkManual;
            txtMAINFRAMEWIDTHActual.IsEnabled = chkManual;
            txtWIDTH_BESpecification.IsEnabled = chkManual;
            txtWIDTH_BEActual.IsEnabled = chkManual;
            txtWIDTH_AFSpecification.IsEnabled = chkManual;
            txtWIDTH_AFActual.IsEnabled = chkManual;
            txtPIN2PINSpecification.IsEnabled = chkManual;
            txtPIN2PINActual.IsEnabled = chkManual;
            txtHUMIDITYMIN.IsEnabled = chkManual;

            txtROOMTEMP.IsEnabled = chkManual;
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

                    if (items[0].SATURATOR_CHEM != null)
                    {
                        txtSATURATOR_CHEMSpecification.Text = items[0].SATURATOR_CHEM.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSATURATOR_CHEMSpecification.Text = "";
                    }

                    #endregion

                    #region SATURATOR_CHEM_MARGIN

                    if (items[0].SATURATOR_CHEM_MARGIN != null)
                    {
                        txtSATPV.Text = items[0].SATURATOR_CHEM_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSATPV.Text = "";
                    }

                    #endregion

                    #region WASHING1

                    if (items[0].WASHING1 != null)
                    {
                        txtWASHING1Specification.Text = items[0].WASHING1.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWASHING1Specification.Text = "";
                    }

                    #endregion

                    #region WASHING1_MARGIN

                    if (items[0].WASHING1_MARGIN != null)
                    {
                        txtWASH1PV.Text = items[0].WASHING1_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWASH1PV.Text = "";
                    }

                    #endregion

                    #region WASHING2

                    if (items[0].WASHING2 != null)
                    {
                        txtWASHING2Specification.Text = items[0].WASHING2.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWASHING2Specification.Text = "";
                    }

                    #endregion

                    #region WASHING2_MARGIN

                    if (items[0].WASHING2_MARGIN != null)
                    {
                        txtWASH2PV.Text = items[0].WASHING2_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWASH2PV.Text = "";
                    }

                    #endregion

                    #region HOTFLUE

                    if (items[0].HOTFLUE != null)
                    {
                        txtHOTFLUESpecification.Text = items[0].HOTFLUE.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtHOTFLUESpecification.Text = "";
                    }

                    #endregion

                    #region HOTFLUE_MARGIN

                    if (items[0].HOTFLUE_MARGIN != null)
                    {
                        txtHOTPV.Text = items[0].HOTFLUE_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtHOTPV.Text = "";
                    }

                    #endregion

                    #region SPEED

                    if (items[0].SPEED != null)
                    {
                        txtSPEEDSpecification.Text = items[0].SPEED.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSPEEDSpecification.Text = "";
                    }

                    #endregion

                    #region SPEED_MARGIN

                    if (items[0].SPEED_MARGIN != null)
                    {
                        txtSPEED.Text = items[0].SPEED_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtSPEED.Text = "";
                    }

                    #endregion

                    #region MAINFRAMEWIDTH

                    if (items[0].MAINFRAMEWIDTH != null)
                    {
                        txtMAINFRAMEWIDTHSpecification.Text = items[0].MAINFRAMEWIDTH.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtMAINFRAMEWIDTHSpecification.Text = "";
                    }

                    #endregion

                    #region MAINFRAMEWIDTH_MARGIN

                    if (items[0].MAINFRAMEWIDTH_MARGIN != null)
                    {
                        txtMAINFRAMEWIDTHActual.Text = items[0].MAINFRAMEWIDTH_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtMAINFRAMEWIDTHActual.Text = "";
                    }

                    #endregion

                    #region WIDTH_BE

                    if (items[0].WIDTH_BE != null)
                    {
                        txtWIDTH_BESpecification.Text = items[0].WIDTH_BE.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_BESpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_BE_MARGIN

                    if (items[0].WIDTH_BE_MARGIN != null)
                    {
                        txtWIDTH_BEActual.Text = items[0].WIDTH_BE_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_BEActual.Text = "";
                    }

                    #endregion

                    #region WIDTH_AF

                    if (items[0].WIDTH_AF != null)
                    {
                        txtWIDTH_AFSpecification.Text = items[0].WIDTH_AF.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_AFSpecification.Text = "";
                    }

                    #endregion

                    #region WIDTH_AF_MARGIN

                    if (items[0].WIDTH_AF_MARGIN != null)
                    {
                        txtWIDTH_AFActual.Text =  items[0].WIDTH_AF_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtWIDTH_AFActual.Text = "";
                    }

                    #endregion

                    #region PIN2PIN

                    if (items[0].PIN2PIN != null)
                    {
                        txtPIN2PINSpecification.Text = items[0].PIN2PIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtPIN2PINSpecification.Text = "";
                    }

                    #endregion

                    #region PIN2PIN_MARGIN

                    if (items[0].PIN2PIN_MARGIN != null)
                    {
                        txtPIN2PINActual.Text = items[0].PIN2PIN_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtPIN2PINActual.Text = "";
                    }

                    #endregion

                    #region ROOMTEMP

                    if (items[0].ROOMTEMP != null)
                    {
                        txtROOMTEMP.Text = items[0].ROOMTEMP.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtROOMTEMP.Text = "";
                    }

                    #endregion

                    #region ROOMTEMP_MARGIN

                    if (items[0].ROOMTEMP_MARGIN != null)
                    {
                        txtROOMTEMP_MARGIN.Text = items[0].ROOMTEMP_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtROOMTEMP_MARGIN.Text = "";
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

                }
                else
                {
                    string msg = "No Scouring Condition found for selected item";

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
            string P_SCOURINGNO = string.Empty;
            decimal? P_CHEM = null;
            decimal? P_CHEM_MARGIN = null;
            decimal? P_WASH1 = null;
            decimal? P_WASH1_MARGIN = null;
            decimal? P_WASH2 = null;
            decimal? P_WASH2_MARGIN = null;
            decimal? P_HOTFLUE = null;
            decimal? P_HOTFLUE_MARGIN = null;
            decimal? P_ROOMTEMP = null;
            decimal? P_ROOMTEMP_MARGIN = null;
            decimal? P_SPEED = null;
            decimal? P_SPEED_MARGIN = null;
            decimal? P_MAINFRAME = null;
            decimal? P_MAINFRAME_MARGIN = null;
            decimal? P_WIDTHBE = null;
            decimal? P_WIDTHBE_MARGIN = null;
            decimal? P_WIDTHAF = null;
            decimal? P_WIDTHAF_MARGIN = null;
            decimal? P_PIN2PIN = null;
            decimal? P_PIN2PIN_MARGIN = null;
            decimal? P_HUMIDITYMAX = null;
            decimal? P_HUMIDITYMIN = null;
            string P_OPERATOR = string.Empty;

            try
            {
                string RESULT = string.Empty;

                if (cbItemGoods.SelectedValue != null)
                    P_ITEMCODE = cbItemGoods.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(cbScouring.Text))
                {
                    if (cbScouring.Text == "Scouring 1")
                        P_SCOURINGNO = "252";
                    else if (cbScouring.Text == "Scouring 2")
                        P_SCOURINGNO = "253";
                }

                if (!string.IsNullOrEmpty(txtSATURATOR_CHEMSpecification.Text))
                    P_CHEM = decimal.Parse(txtSATURATOR_CHEMSpecification.Text);

                if (!string.IsNullOrEmpty(txtSATPV.Text))
                    P_CHEM_MARGIN = decimal.Parse(txtSATPV.Text);

                if (!string.IsNullOrEmpty(txtWASHING1Specification.Text))
                    P_WASH1 = decimal.Parse(txtWASHING1Specification.Text);

                if (!string.IsNullOrEmpty(txtWASH1PV.Text))
                    P_WASH1_MARGIN = decimal.Parse(txtWASH1PV.Text);

                if (!string.IsNullOrEmpty(txtWASHING2Specification.Text))
                    P_WASH2 = decimal.Parse(txtWASHING2Specification.Text);

                if (!string.IsNullOrEmpty(txtWASH2PV.Text))
                    P_WASH2_MARGIN = decimal.Parse(txtWASH2PV.Text);

                if (!string.IsNullOrEmpty(txtHOTFLUESpecification.Text))
                    P_HOTFLUE = decimal.Parse(txtHOTFLUESpecification.Text);

                if (!string.IsNullOrEmpty(txtHOTPV.Text))
                    P_HOTFLUE_MARGIN = decimal.Parse(txtHOTPV.Text);

                if (!string.IsNullOrEmpty(txtROOMTEMP.Text))
                    P_ROOMTEMP = decimal.Parse(txtROOMTEMP.Text);

                if (!string.IsNullOrEmpty(txtROOMTEMP_MARGIN.Text))
                    P_ROOMTEMP_MARGIN = decimal.Parse(txtROOMTEMP_MARGIN.Text);

                if (!string.IsNullOrEmpty(txtSPEEDSpecification.Text))
                    P_SPEED = decimal.Parse(txtSPEEDSpecification.Text);

                if (!string.IsNullOrEmpty(txtSPEED.Text))
                    P_SPEED_MARGIN = decimal.Parse(txtSPEED.Text);

                if (!string.IsNullOrEmpty(txtMAINFRAMEWIDTHSpecification.Text))
                    P_MAINFRAME = decimal.Parse(txtMAINFRAMEWIDTHSpecification.Text);

                if (!string.IsNullOrEmpty(txtMAINFRAMEWIDTHActual.Text))
                    P_MAINFRAME_MARGIN = decimal.Parse(txtMAINFRAMEWIDTHActual.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_BESpecification.Text))
                    P_WIDTHBE = decimal.Parse(txtWIDTH_BESpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_BEActual.Text))
                    P_WIDTHBE_MARGIN = decimal.Parse(txtWIDTH_BEActual.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_AFSpecification.Text))
                    P_WIDTHAF = decimal.Parse(txtWIDTH_AFSpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTH_AFActual.Text))
                    P_WIDTHAF_MARGIN = decimal.Parse(txtWIDTH_AFActual.Text);

                if (!string.IsNullOrEmpty(txtPIN2PINSpecification.Text))
                    P_PIN2PIN = decimal.Parse(txtPIN2PINSpecification.Text);

                if (!string.IsNullOrEmpty(txtPIN2PINActual.Text))
                    P_PIN2PIN_MARGIN = decimal.Parse(txtPIN2PINActual.Text);

                if (!string.IsNullOrEmpty(txtHUMIDITYMAX.Text))
                    P_HUMIDITYMAX = decimal.Parse(txtHUMIDITYMAX.Text);

                if (!string.IsNullOrEmpty(txtHUMIDITYMIN.Text))
                    P_HUMIDITYMIN = decimal.Parse(txtHUMIDITYMIN.Text);

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;

                RESULT = ProcessConditionDataService.Instance.CONDITION_FINISHINGSCOURING(P_ITEMCODE, P_SCOURINGNO,
                          P_CHEM, P_CHEM_MARGIN, P_WASH1, P_WASH1_MARGIN,
                          P_WASH2, P_WASH2_MARGIN, P_HOTFLUE, P_HOTFLUE_MARGIN,
                          P_ROOMTEMP, P_ROOMTEMP_MARGIN, P_SPEED, P_SPEED_MARGIN,
                          P_MAINFRAME, P_MAINFRAME_MARGIN, P_WIDTHBE, P_WIDTHBE_MARGIN,
                          P_WIDTHAF, P_WIDTHAF_MARGIN, P_PIN2PIN, P_PIN2PIN_MARGIN,
                          P_HUMIDITYMAX, P_HUMIDITYMIN, P_OPERATOR);

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
