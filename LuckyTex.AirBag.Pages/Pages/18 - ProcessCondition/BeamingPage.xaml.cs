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
    /// Interaction logic for BeamingPage.xaml
    /// </summary>
    public partial class BeamingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingPage()
        {
            InitializeComponent();

            LoadCOMBTYPE();
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
                EnabledCon(true);

                ClearData();

                txtNOWARPBEAM.Focus();
                txtNOWARPBEAM.SelectAll();
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

                txtNOWARPBEAM.Focus();
                txtNOWARPBEAM.SelectAll();
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
                if (!string.IsNullOrEmpty(txtNOWARPBEAM.Text))
                    {
                        if (Save() == true)
                        {
                            "Save complete".ShowMessageBox(false);
                            EnabledCon(false);
                            ClearData();

                            cbItemGoods.Text = "";
                            cbItemGoods.SelectedValue = null;
                            cbItemGoods.SelectedIndex = -1;

                            cbItemGoods.Focus();
                        }
                    }
                    else
                    {
                        "No.of warper beam isn't Null".ShowMessageBox();
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
            #region LoadFinishing
            try
            {
                if (cbItemGoods.SelectedValue != null)
                {
                    string itm_code = cbItemGoods.SelectedValue.ToString();

                    if (itm_code != "")
                    {
                        LoadDrawing(itm_code);
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

        private void txtNOWARPBEAM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTOTALYARN.Focus();
                txtTOTALYARN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTOTALYARN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTOTALKEBA.Focus();
                txtTOTALKEBA.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTOTALKEBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBEAMLENGTH.Focus();
                txtBEAMLENGTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBEAMLENGTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTOTALBEAM.Focus();
                txtTOTALBEAM.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTOTALBEAM_KeyDown(object sender, KeyEventArgs e)
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
                txtMAXSPEED.Focus();
                txtMAXSPEED.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMINSPEED.Focus();
                txtMINSPEED.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMINSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXBEAMWIDTH.Focus();
                txtMAXBEAMWIDTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXBEAMWIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMINBEAMWIDTH.Focus();
                txtMINBEAMWIDTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMINBEAMWIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXYARNTENSION.Focus();
                txtMAXYARNTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXYARNTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMINYARNTENSION.Focus();
                txtMINYARNTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMINYARNTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXWINDTENSION.Focus();
                txtMAXWINDTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXWINDTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMINWINDTENSION.Focus();
                txtMINWINDTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMINWINDTENSION_KeyDown(object sender, KeyEventArgs e)
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

        #region LoadCOMBTYPE

        private void LoadCOMBTYPE()
        {
            if (cbCOMBTYPE.ItemsSource == null)
            {
                string[] str = new string[] { "Straight", "Zigzag" };

                cbCOMBTYPE.ItemsSource = str;
                cbCOMBTYPE.SelectedIndex = 0;
            }
        }

        #endregion

        #endregion

        #region ClearData

        private void ClearData()
        {
            try
            {
                txtNOWARPBEAM.Text = "";
                txtTOTALYARN.Text = "";
                txtTOTALKEBA.Text = "";
                txtBEAMLENGTH.Text = "";
                txtTOTALBEAM.Text = "";
                txtCOMBPITCH.Text = "";

                cbCOMBTYPE.SelectedValue = 0;
                cbCOMBTYPE.Text = "Straight";

                txtMAXHARDNESS.Text = "";
                txtMINHARDNESS.Text = "";
                txtMAXSPEED.Text = "";
                txtMINSPEED.Text = "";
                txtMAXBEAMWIDTH.Text = "";
                txtMINBEAMWIDTH.Text = "";
                txtMAXYARNTENSION.Text = "";
                txtMINYARNTENSION.Text = "";
                txtMAXWINDTENSION.Text = "";
                txtMINWINDTENSION.Text = "";
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
            txtNOWARPBEAM.IsEnabled = chkManual;
            txtTOTALYARN.IsEnabled = chkManual;
            txtTOTALKEBA.IsEnabled = chkManual;
            txtBEAMLENGTH.IsEnabled = chkManual;
            txtTOTALBEAM.IsEnabled = chkManual;
            txtCOMBPITCH.IsEnabled = chkManual;

            txtMAXHARDNESS.IsEnabled = chkManual;
            txtMINHARDNESS.IsEnabled = chkManual;
            txtMAXSPEED.IsEnabled = chkManual;
            txtMINSPEED.IsEnabled = chkManual;
            txtMAXBEAMWIDTH.IsEnabled = chkManual;
            txtMINBEAMWIDTH.IsEnabled = chkManual;
            txtMAXYARNTENSION.IsEnabled = chkManual;
            txtMINYARNTENSION.IsEnabled = chkManual;
            txtMAXWINDTENSION.IsEnabled = chkManual;
            txtMINWINDTENSION.IsEnabled = chkManual;
        }
        #endregion

        #region LoadDrawing

        private void LoadDrawing(string itm_code)
        {
            try
            {
                List<BEAM_GETSPECBYCHOPNO> results = BeamingDataService.Instance.BEAM_GETSPECBYCHOPNO(itm_code);

                if (results != null && results.Count > 0)
                {
                    txtNOWARPBEAM.Text = results[0].NOWARPBEAM.Value.ToString("#,##0.##");
                    txtTOTALYARN.Text = results[0].TOTALYARN.Value.ToString("#,##0.##");
                    txtTOTALKEBA.Text = results[0].TOTALKEBA.Value.ToString("#,##0.##");
                    txtBEAMLENGTH.Text = results[0].BEAMLENGTH.Value.ToString("#,##0.##");
                    txtTOTALBEAM.Text = results[0].TOTALBEAM.Value.ToString("#,##0.##");
                    txtCOMBPITCH.Text = results[0].COMBPITCH.Value.ToString("#,##0.##");

                    if (results[0].COMBTYPE != null)
                    {
                       cbCOMBTYPE.SelectedValue = results[0].COMBTYPE;
                    }

                    txtMAXHARDNESS.Text = results[0].MAXHARDNESS.Value.ToString("#,##0.##");
                    txtMINHARDNESS.Text = results[0].MINHARDNESS.Value.ToString("#,##0.##");

                    txtMAXSPEED.Text = results[0].MAXSPEED.Value.ToString("#,##0.##");
                    txtMINSPEED.Text = results[0].MINSPEED.Value.ToString("#,##0.##");

                    txtMAXBEAMWIDTH.Text = results[0].MAXBEAMWIDTH.Value.ToString("#,##0.##");
                    txtMINBEAMWIDTH.Text = results[0].MINBEAMWIDTH.Value.ToString("#,##0.##");

                    txtMAXYARNTENSION.Text = results[0].MAXYARNTENSION.Value.ToString("#,##0.##");
                    txtMINYARNTENSION.Text = results[0].MINYARNTENSION.Value.ToString("#,##0.##");

                    txtMAXWINDTENSION.Text = results[0].MAXWINDTENSION.Value.ToString("#,##0.##");
                    txtMINWINDTENSION.Text = results[0].MINWINDTENSION.Value.ToString("#,##0.##");
                }
                else
                {
                    string msg = "No Beaming Spec. found for selected item";

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
            decimal? P_NOWARPBEAM = null; 
            decimal? P_TOTALYARN = null; 
            decimal? P_TOTALKEBA = null; 
            decimal? P_BEAMLENGTH = null;
            decimal? P_HARDNESS_MAX = null; 
            decimal? P_HARDNESS_MIN = null; 
            decimal? P_BEAMWIDTH_MAX = null; 
            decimal? P_BEAMWIDTH_MIN = null; 
            decimal? P_SPEED_MAX = null;
            decimal? P_SPEED_MIN = null; 
            decimal? P_YARNTENSION_MAX = null; 
            decimal? P_YARNTENSION_MIN = null; 
            decimal? P_WINDTENSION_MAX = null; 
            decimal? P_WINDTENSION_MIN = null;
            string P_COMBTYPE = string.Empty;
            decimal? P_COMBPITCH = null; 
            decimal? P_TOTALBEAM = null;
            string P_OPERATOR = string.Empty;

            try
            {
                string RESULT = string.Empty;

                if (cbItemGoods.SelectedValue != null)
                    P_ITMPREPARE = cbItemGoods.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtNOWARPBEAM.Text))
                    P_NOWARPBEAM = decimal.Parse(txtNOWARPBEAM.Text);

                if (!string.IsNullOrEmpty(txtTOTALYARN.Text))
                    P_TOTALYARN = decimal.Parse(txtTOTALYARN.Text);

                if (!string.IsNullOrEmpty(txtTOTALKEBA.Text))
                    P_TOTALKEBA = decimal.Parse(txtTOTALKEBA.Text);

                if (!string.IsNullOrEmpty(txtBEAMLENGTH.Text))
                    P_BEAMLENGTH = decimal.Parse(txtBEAMLENGTH.Text);

                if (!string.IsNullOrEmpty(txtMAXHARDNESS.Text))
                    P_HARDNESS_MAX = decimal.Parse(txtMAXHARDNESS.Text);

                if (!string.IsNullOrEmpty(txtMINHARDNESS.Text))
                    P_HARDNESS_MIN = decimal.Parse(txtMINHARDNESS.Text);


                if (!string.IsNullOrEmpty(txtMAXBEAMWIDTH.Text))
                    P_BEAMWIDTH_MAX = decimal.Parse(txtMAXBEAMWIDTH.Text);

                if (!string.IsNullOrEmpty(txtMINBEAMWIDTH.Text))
                    P_BEAMWIDTH_MIN = decimal.Parse(txtMINBEAMWIDTH.Text);

                if (!string.IsNullOrEmpty(txtMAXSPEED.Text))
                    P_SPEED_MAX = decimal.Parse(txtMAXSPEED.Text);

                if (!string.IsNullOrEmpty(txtMINSPEED.Text))
                    P_SPEED_MIN = decimal.Parse(txtMINSPEED.Text);

                if (!string.IsNullOrEmpty(txtMAXYARNTENSION.Text))
                    P_YARNTENSION_MAX = decimal.Parse(txtMAXYARNTENSION.Text);

                if (!string.IsNullOrEmpty(txtMINYARNTENSION.Text))
                    P_YARNTENSION_MIN = decimal.Parse(txtMINYARNTENSION.Text);

                if (!string.IsNullOrEmpty(txtMAXWINDTENSION.Text))
                    P_WINDTENSION_MAX = decimal.Parse(txtMAXWINDTENSION.Text);

                if (!string.IsNullOrEmpty(txtMINWINDTENSION.Text))
                    P_WINDTENSION_MIN = decimal.Parse(txtMINWINDTENSION.Text);

                if (!string.IsNullOrEmpty(cbCOMBTYPE.Text))
                    P_COMBTYPE = cbCOMBTYPE.Text;

                if (!string.IsNullOrEmpty(txtCOMBPITCH.Text))
                    P_COMBPITCH = decimal.Parse(txtCOMBPITCH.Text);

                if (!string.IsNullOrEmpty(txtTOTALBEAM.Text))
                    P_TOTALBEAM = decimal.Parse(txtTOTALBEAM.Text);

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;


                RESULT = ProcessConditionDataService.Instance.CONDITION_BEAMING(P_ITMPREPARE, P_NOWARPBEAM, P_TOTALYARN, P_TOTALKEBA, P_BEAMLENGTH,
                                                 P_HARDNESS_MAX, P_HARDNESS_MIN, P_BEAMWIDTH_MAX, P_BEAMWIDTH_MIN, P_SPEED_MAX,
                                                 P_SPEED_MIN, P_YARNTENSION_MAX, P_YARNTENSION_MIN, P_WINDTENSION_MAX, P_WINDTENSION_MIN,
                                                 P_COMBTYPE, P_COMBPITCH, P_TOTALBEAM, P_OPERATOR);

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
