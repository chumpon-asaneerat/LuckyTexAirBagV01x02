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
    /// Interaction logic for WarpingConditionPage.xaml
    /// </summary>
    public partial class WarpingConditionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingConditionPage()
        {
            InitializeComponent();
            LoadItemGood();
            LoadItemYarn();
            LoadMCNO();
            LoadCombType();
            LoadWaxing();
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

                txtWarperends.Focus();
                txtWarperends.SelectAll();
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

                txtWarperends.Focus();
                txtWarperends.SelectAll();
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
                if (!string.IsNullOrEmpty(txtWarperends.Text))
                    {
                        if (Save() == true)
                        {
                            "Save complete".ShowMessageBox(false);
                            EnabledCon(false);
                            ClearData();

                            cbItemGoods.Text = "";
                            cbItemGoods.SelectedValue = null;
                            cbItemGoods.SelectedIndex = -1;

                            cbMCNO.SelectedValue = 0;
                            cbMCNO.Text = "1";

                            cbItemGoods.Focus();
                        }
                    }
                    else
                    {
                        "Warper Ends isn't Null".ShowMessageBox();
                    }
            }
            else
            {
                "Item Code isn't Null".ShowMessageBox();
            }
        }

        #endregion

        #region ComboBox

        #region cbItemGoods_SelectionChanged

        private void cbItemGoods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadSpecByChopNoAndMC
            try
            {
                if (cbItemGoods.SelectedValue != null && cbMCNO.SelectedValue != null)
                {
                    string itm_code = cbItemGoods.SelectedValue.ToString();
                    string mc = cbMCNO.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(itm_code) && !string.IsNullOrEmpty(mc))
                    {
                        LoadSpecByChopNoAndMC(itm_code,mc);
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
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                ClearData();
            }
            #endregion
        }

        #endregion

        #region cbMCNO_SelectionChanged

        private void cbMCNO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadSpecByChopNoAndMC
            try
            {
                if (cbItemGoods.SelectedValue != null && cbMCNO.SelectedValue != null)
                {
                    string itm_code = cbItemGoods.SelectedValue.ToString();
                    string mc = cbMCNO.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(itm_code) && !string.IsNullOrEmpty(mc))
                    {
                        LoadSpecByChopNoAndMC(itm_code, mc);
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

        private void txtWarperends_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtKebayarn.Focus();
                txtKebayarn.SelectAll();
                e.Handled = true;
            }
        }

        private void txtKebayarn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNowarpbeam.Focus();
                txtNowarpbeam.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNowarpbeam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCOMBPITCH.Focus();
                txtCOMBPITCH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOMBPITCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSpeed.Focus();
                txtSpeed.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSpeed_Margin.Focus();
                txtSpeed_Margin.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSpeed_Margin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtYarnTension.Focus();
                txtYarnTension.SelectAll();
                e.Handled = true;
            }
        }

        private void txtYarnTension_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtYarnTension_Margin.Focus();
                txtYarnTension_Margin.SelectAll();
                e.Handled = true;
            }
        }

        private void txtYarnTension_Margin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWindTension.Focus();
                txtWindTension.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWindTension_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWindTension_Margin.Focus();
                txtWindTension_Margin.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWindTension_Margin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMaxLength.Focus();
                txtMaxLength.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMaxLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMinLength.Focus();
                txtMinLength.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMinLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXHARDNESS.Focus();
                txtMAXHARDNESS.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXHARDNESS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMINHARDNESS.Focus();
                txtMINHARDNESS.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMINHARDNESS_KeyDown(object sender, KeyEventArgs e)
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

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMPREPARELIST> items = _session.ITM_GETITEMPREPARELIST();

                this.cbItemGoods.ItemsSource = items;
                this.cbItemGoods.DisplayMemberPath = "ITM_PREPARE";
                this.cbItemGoods.SelectedValuePath = "ITM_PREPARE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadItemYarn

        private void LoadItemYarn()
        {
            try
            {
                List<ITM_GETITEMYARNLIST> items = _session.ITM_GETITEMYARNLIST();

                this.cbITMYARN.ItemsSource = items;
                this.cbITMYARN.DisplayMemberPath = "ITM_YARN";
                this.cbITMYARN.SelectedValuePath = "ITM_YARN";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadMCNO

        private void LoadMCNO()
        {
            if (cbMCNO.ItemsSource == null)
            {
                string[] str = new string[] { "1", "2", "3", "4" };

                cbMCNO.ItemsSource = str;
                cbMCNO.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadCombType

        private void LoadCombType()
        {
            if (cbCombType.ItemsSource == null)
            {
                string[] str = new string[] { "Straight", "Zigzag" };

                cbCombType.ItemsSource = str;
                cbCombType.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadWaxing

        private void LoadWaxing()
        {
            if (cbWaxing.ItemsSource == null)
            {
                string[] str = new string[] { "Yes", "No" };

                cbWaxing.ItemsSource = str;
                cbWaxing.SelectedIndex = 0;
            }
        }

        #endregion

        #endregion

        #region ClearData

        private void ClearData()
        {
            try
            {
                txtWarperends.Text = "";
                txtKebayarn.Text = "";
                txtNowarpbeam.Text = "";
                txtCOMBPITCH.Text = "";
                txtSpeed.Text = "";
                txtSpeed_Margin.Text = "";
                txtYarnTension.Text = "";
                txtYarnTension_Margin.Text = "";
                txtWindTension.Text = "";
                txtWindTension_Margin.Text = "";
                txtMaxLength.Text = "";
                txtMinLength.Text = "";
                txtMAXHARDNESS.Text = "";
                txtMINHARDNESS.Text = "";

                cbITMYARN.SelectedValue = null;
                cbITMYARN.SelectedIndex = -1;

                cbCombType.SelectedValue = 0;
                cbCombType.Text = "Straight";

                cbWaxing.SelectedValue = 0;
                cbWaxing.Text = "Yes";
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
            txtWarperends.IsEnabled = chkManual;
            txtKebayarn.IsEnabled = chkManual;
            txtNowarpbeam.IsEnabled = chkManual;
            txtCOMBPITCH.IsEnabled = chkManual;
            txtSpeed.IsEnabled = chkManual;
            txtSpeed_Margin.IsEnabled = chkManual;
            txtYarnTension.IsEnabled = chkManual;
            txtYarnTension_Margin.IsEnabled = chkManual;
            txtWindTension.IsEnabled = chkManual;
            txtWindTension_Margin.IsEnabled = chkManual;
            txtMaxLength.IsEnabled = chkManual;
            txtMinLength.IsEnabled = chkManual;
            txtMAXHARDNESS.IsEnabled = chkManual;
            txtMINHARDNESS.IsEnabled = chkManual;

        }
        #endregion

        #region LoadSpecByChopNoAndMC

        private void LoadSpecByChopNoAndMC(string P_ITMPREPARE, string P_MCNO)
        {
            try
            {
                List<WARP_GETSPECBYCHOPNOANDMC> results = WarpingDataService.Instance.WARP_GETSPECBYCHOPNOANDMC(P_ITMPREPARE, P_MCNO);

                if (results != null && results.Count > 0)
                {
                    if (results[0].ITM_YARN != null)
                     cbITMYARN.SelectedValue = results[0].ITM_YARN;

                    if (results[0].WARPERENDS != null)
                        txtWarperends.Text = results[0].WARPERENDS.Value.ToString("#,##0.##");

                    if (results[0].MAXLENGTH != null)
                        txtMaxLength.Text = results[0].MAXLENGTH.Value.ToString("#,##0.##");

                    if (results[0].MINLENGTH != null)
                        txtMinLength.Text = results[0].MINLENGTH.Value.ToString("#,##0.##");

                    if (!string.IsNullOrEmpty(results[0].WAXING))
                    {
                        if (results[0].WAXING == "Y")
                            cbWaxing.SelectedValue = "Yes";
                        else if (results[0].WAXING == "N")
                            cbWaxing.SelectedValue = "No";
                    }

                    if (!string.IsNullOrEmpty(results[0].COMBTYPE))
                    {
                        if (results[0].COMBTYPE == "Straight")
                            cbCombType.SelectedValue = "Straight";
                        else if (results[0].COMBTYPE == "Zigzag")
                            cbCombType.SelectedValue = "Zigzag";
                    }

                    txtCOMBPITCH.Text = results[0].COMBPITCH;

                    if (results[0].KEBAYARN != null)
                        txtKebayarn.Text = results[0].KEBAYARN.Value.ToString("#,##0.##");

                    if (results[0].NOWARPBEAM != null)
                        txtNowarpbeam.Text = results[0].NOWARPBEAM.Value.ToString("#,##0.##");

                    if (results[0].MAXHARDNESS != null)
                        txtMAXHARDNESS.Text = results[0].MAXHARDNESS.Value.ToString("#,##0.##");

                    if (results[0].MINHARDNESS != null)
                        txtMINHARDNESS.Text = results[0].MINHARDNESS.Value.ToString("#,##0.##");

                    if (results[0].SPEED != null)
                        txtSpeed.Text = results[0].SPEED.Value.ToString("#,##0.##");

                    if (results[0].SPEED_MARGIN != null)
                        txtSpeed_Margin.Text = results[0].SPEED_MARGIN.Value.ToString("#,##0.##");

                    if (results[0].YARN_TENSION != null)
                        txtYarnTension.Text = results[0].YARN_TENSION.Value.ToString("#,##0.##");

                    if (results[0].YARN_TENSION_MARGIN != null)
                        txtYarnTension_Margin.Text = results[0].YARN_TENSION_MARGIN.Value.ToString("#,##0.##");

                    if (results[0].WINDING_TENSION != null)
                        txtWindTension.Text = results[0].WINDING_TENSION.Value.ToString("#,##0.##");

                    if (results[0].WINDING_TENSION_MARGIN != null)
                        txtWindTension_Margin.Text = results[0].WINDING_TENSION_MARGIN.Value.ToString("#,##0.##");

                }
                else
                {
                    string msg = "No Warping Spec. found for selected item and Warper MC";

                    msg.ShowMessageBox(false);

                    EnabledCon(false);
                    ClearData();
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
            string P_ITMPREPARE = string.Empty;
            string P_MCNO = string.Empty;
            string P_ITMYARN = string.Empty;
            decimal? P_WARPERENDS = null;
            decimal? P_MAXLENGTH = null;
            decimal? P_MINLENGTH = null;
            string P_WAXING = string.Empty;
            string P_COMBTYPE = string.Empty;
            decimal? P_COMBPITCH = null;
            decimal? P_KEBAYARN = null;
            decimal? P_NOWARPBEAM = null;
            decimal? P_HARDNESS_MAX = null;
            decimal? P_HARDNESS_MIN = null;
            decimal? P_SPEED = null;
            decimal? P_SPEED_MARGIN = null;
            decimal? P_YARNTENSION = null;
            decimal? P_YARNTENSION_MARGIN = null;
            decimal? P_WINDTENSION = null;
            decimal? P_WINDTENSION_MARGIN = null;
            string P_OPERATOR = string.Empty;

            try
            {
                string RESULT = string.Empty;

                if (cbItemGoods.SelectedValue != null)
                    P_ITMPREPARE = cbItemGoods.SelectedValue.ToString();

                if (cbMCNO.SelectedValue != null)
                    P_MCNO = cbMCNO.SelectedValue.ToString();

                if (cbITMYARN.SelectedValue != null)
                    P_ITMYARN = cbITMYARN.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtWarperends.Text))
                    P_WARPERENDS  = decimal.Parse(txtWarperends.Text);

                if (!string.IsNullOrEmpty(txtMaxLength.Text))
                    P_MAXLENGTH = decimal.Parse(txtMaxLength.Text);

                if (!string.IsNullOrEmpty(txtMinLength.Text))
                    P_MINLENGTH = decimal.Parse(txtMinLength.Text);

                if (cbWaxing.SelectedValue != null)
                {
                    if (cbWaxing.SelectedValue.ToString() == "Yes")
                        P_WAXING = "Y";
                    else if (cbWaxing.SelectedValue.ToString() == "No")
                        P_WAXING = "N";
                }

                if (cbCombType.SelectedValue != null)
                {
                    P_COMBTYPE = cbCombType.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(txtCOMBPITCH.Text))
                P_COMBPITCH = decimal.Parse(txtCOMBPITCH.Text);

                if (!string.IsNullOrEmpty(txtKebayarn.Text))
                    P_KEBAYARN = decimal.Parse(txtKebayarn.Text);

                if (!string.IsNullOrEmpty(txtNowarpbeam.Text))
                    P_NOWARPBEAM = decimal.Parse(txtNowarpbeam.Text);

                if (!string.IsNullOrEmpty(txtMAXHARDNESS.Text))
                    P_HARDNESS_MAX = decimal.Parse(txtMAXHARDNESS.Text);

                if (!string.IsNullOrEmpty(txtMINHARDNESS.Text))
                    P_HARDNESS_MIN = decimal.Parse(txtMINHARDNESS.Text);

                if (!string.IsNullOrEmpty(txtSpeed.Text))
                    P_SPEED = decimal.Parse(txtSpeed.Text);

                if (!string.IsNullOrEmpty(txtSpeed_Margin.Text))
                    P_SPEED_MARGIN = decimal.Parse(txtSpeed_Margin.Text);

                if (!string.IsNullOrEmpty(txtYarnTension.Text))
                    P_YARNTENSION = decimal.Parse(txtYarnTension.Text);

                if (!string.IsNullOrEmpty(txtYarnTension_Margin.Text))
                    P_YARNTENSION_MARGIN = decimal.Parse(txtYarnTension_Margin.Text);

                if (!string.IsNullOrEmpty(txtWindTension.Text))
                    P_WINDTENSION = decimal.Parse(txtWindTension.Text);

                if (!string.IsNullOrEmpty(txtWindTension_Margin.Text))
                    P_WINDTENSION_MARGIN = decimal.Parse(txtWindTension_Margin.Text);

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;

                RESULT = ProcessConditionDataService.Instance.CONDITION_WARPING(P_ITMPREPARE, P_MCNO, P_ITMYARN, P_WARPERENDS, P_MAXLENGTH,
                             P_MINLENGTH, P_WAXING, P_COMBTYPE, P_COMBPITCH, P_KEBAYARN, P_NOWARPBEAM,
                             P_HARDNESS_MAX, P_HARDNESS_MIN, P_SPEED, P_SPEED_MARGIN, P_YARNTENSION,
                             P_YARNTENSION_MARGIN, P_WINDTENSION, P_WINDTENSION_MARGIN, P_OPERATOR);

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
