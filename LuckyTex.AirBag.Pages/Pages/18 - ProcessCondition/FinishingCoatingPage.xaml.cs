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
    /// Interaction logic for FinishingCoatingPage.xaml
    /// </summary>
    public partial class FinishingCoatingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingCoatingPage()
        {
            InitializeComponent();

            LoadBladedirection();
            LoadCoating();
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
                if (cbCoating.Text != "")
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
                if (cbCoating.Text != "")
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
                if (cbCoating.Text != "")
                {
                    if (!string.IsNullOrEmpty(txtSATPV.Text))
                    {
                        if (Save() == true)
                        {
                            "Save complete".ShowMessageBox(false);
                            EnabledCon(false);

                            cbCoating.SelectedValue = 0;
                            cbCoating.Text = "Coating 1";
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

        private void txtBE_COATWIDTHSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBE_COATWIDTHActual.Focus();
                txtBE_COATWIDTHActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBE_COATWIDTHActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFan110Specification.Focus();
                txtFan110Specification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFan110Specification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFan110Actual.Focus();
                txtFan110Actual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFan110Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN15Specification.Focus();
                txtEXFAN15Specification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXFAN15Specification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN15Actual.Focus();
                txtEXFAN15Actual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXFAN15Actual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXFAN234Specification.Focus();
                txtEXFAN234Specification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXFAN234Specification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtANGLEKNIFESpecification.Focus();
                txtANGLEKNIFESpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtANGLEKNIFESpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBLADENOSpecification.Focus();
                txtBLADENOSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBLADENOSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPathLineSpecification.Focus();
                txtPathLineSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtPathLineSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFeedInSpecification.Focus();
                txtFeedInSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFeedInSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFeedInActual.Focus();
                txtFeedInActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFeedInActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_UPSpecification.Focus();
                txtTENSION_UPSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_UPSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_UPActual.Focus();
                txtTENSION_UPActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_UPActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_DOWNSpecification.Focus();
                txtTENSION_DOWNSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_DOWNSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_DOWNActual.Focus();
                txtTENSION_DOWNActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_DOWNActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFRAMEWIDTH_FORNSpecification.Focus();
                txtFRAMEWIDTH_FORNSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFRAMEWIDTH_FORNSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFRAMEWIDTH_TENTERSpecification.Focus();
                txtFRAMEWIDTH_TENTERSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFRAMEWIDTH_TENTERSpecification_KeyDown(object sender, KeyEventArgs e)
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
                txtSATURATOR_CHEMSpecification.Focus();
                txtSATURATOR_CHEMSpecification.SelectAll();
                e.Handled = true;
            }
        }

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
                txtROOMTEMP.Focus();
                txtROOMTEMP.SelectAll();
                e.Handled = true;
            }
        }

        private void txtROOMTEMP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtROOMTEMP_MARGIN.Focus();
                txtROOMTEMP_MARGIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtROOMTEMP_MARGIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRATIOSILICONE.Focus();
                txtRATIOSILICONE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtRATIOSILICONE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCOATINGWEIGTH_MAX.Focus();
                txtCOATINGWEIGTH_MAX.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGTH_MAX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCOATINGWEIGTH_MIN.Focus();
                txtCOATINGWEIGTH_MIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGTH_MIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHCOATSpecification.Focus();
                txtWIDTHCOATSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHCOATSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHCOATALL_MAX.Focus();
                txtWIDTHCOATALL_MAX.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHCOATALL_MAX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHCOATALL_MIN.Focus();
                txtWIDTHCOATALL_MIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHCOATALL_MIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHUMIDITY_MAX.Focus();
                txtHUMIDITY_MAX.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHUMIDITY_MAX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHUMIDITY_MIN.Focus();
                txtHUMIDITY_MIN.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHUMIDITY_MIN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region ComboBox

        #region cbCoating_SelectionChanged

        private void cbCoating_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region LoadFinishing
            try
            {
                if (chkAll.IsChecked == false)
                {
                    if (cbItemGoods.SelectedValue != null && cbCoating.SelectedValue != null)
                    {
                        string CoatingNo = string.Empty;

                        if (cbCoating.SelectedValue.ToString() == "Coating 1")
                            CoatingNo = "254";
                        else if (cbCoating.SelectedValue.ToString() == "Coating 2")
                            CoatingNo = "255";

                        string itm_code = cbItemGoods.SelectedValue.ToString();

                        if (CoatingNo != "" && itm_code != "")
                        {
                            LoadFinishing(itm_code, CoatingNo);
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
                    if (cbItemGoods.SelectedValue != null && cbCoating.SelectedValue != null)
                    {
                        string ScouringNo = string.Empty;

                        if (cbCoating.SelectedValue.ToString() == "Coating 1")
                            ScouringNo = "254";
                        else if (cbCoating.SelectedValue.ToString() == "Coating 2")
                            ScouringNo = "255";

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

        #region private Methods

        #region Load Combo

        #region LoadCoating

        private void LoadCoating()
        {
            if (cbCoating.ItemsSource == null)
            {
                string[] str = new string[] { "Coating 1", "Coating 2" };

                cbCoating.ItemsSource = str;
                cbCoating.SelectedIndex = 0;
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

        #region ClearData

        private void ClearData()
        {
            try
            {
                //cbCoating.SelectedValue = 0;
                //cbCoating.Text = "Coating 1";
                //cbItemGoods.SelectedValue = null;
                chkAll.IsChecked = false;

                cbBLADEDIRECTIONActual.SelectedValue = null;
                img.Source = new BitmapImage(new Uri("", UriKind.Relative));

                ImageL.Visibility = System.Windows.Visibility.Collapsed;
                ImageR.Visibility = System.Windows.Visibility.Collapsed;
                ImageC.Visibility = System.Windows.Visibility.Collapsed;

                txtBE_COATWIDTHSpecification.Text = "";
                txtBE_COATWIDTHActual.Text = "";
                txtFan110Specification.Text = "";
                txtFan110Actual.Text = "";
                txtEXFAN15Specification.Text = "";
                txtEXFAN15Actual.Text = "";
                txtEXFAN234Specification.Text = "";
                txtANGLEKNIFESpecification.Text = "";
                txtBLADENOSpecification.Text = "";
                txtPathLineSpecification.Text = "";
                txtFeedInSpecification.Text = "";
                txtFeedInActual.Text = "";
                txtTENSION_UPSpecification.Text = "";
                txtTENSION_UPActual.Text = "";
                txtTENSION_DOWNSpecification.Text = "";
                txtTENSION_DOWNActual.Text = "";
                txtFRAMEWIDTH_FORNSpecification.Text = "";
                txtFRAMEWIDTH_TENTERSpecification.Text = "";
                txtSPEED.Text = "";
                txtSPEED_MARGIN.Text = "";
                txtSATURATOR_CHEMSpecification.Text = "";
                txtSATPV.Text = "";
                txtWASHING1Specification.Text = "";
                txtWASH1PV.Text = "";
                txtWASHING2Specification.Text = "";
                txtWASH2PV.Text = "";
                txtHOTFLUESpecification.Text = "";
                txtHOTPV.Text = "";
                txtROOMTEMP.Text = "";
                txtROOMTEMP_MARGIN.Text = "";
                txtRATIOSILICONE.Text = "";
                txtCOATINGWEIGTH_MAX.Text = "";
                txtCOATINGWEIGTH_MIN.Text = "";
                txtWIDTHCOATSpecification.Text = "";
                txtWIDTHCOATALL_MAX.Text = "";
                txtWIDTHCOATALL_MIN.Text = "";
                txtHUMIDITY_MAX.Text = "";
                txtHUMIDITY_MIN.Text = "";
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
            txtBE_COATWIDTHSpecification.IsEnabled = chkManual;
            txtBE_COATWIDTHActual.IsEnabled = chkManual;
            txtFan110Specification.IsEnabled = chkManual;
            txtFan110Actual.IsEnabled = chkManual;
            txtEXFAN15Specification.IsEnabled = chkManual;
            txtEXFAN15Actual.IsEnabled = chkManual;
            txtEXFAN234Specification.IsEnabled = chkManual;
            txtANGLEKNIFESpecification.IsEnabled = chkManual;
            txtBLADENOSpecification.IsEnabled = chkManual;
            txtPathLineSpecification.IsEnabled = chkManual;
            txtFeedInSpecification.IsEnabled = chkManual;
            txtFeedInActual.IsEnabled = chkManual;
            txtTENSION_UPSpecification.IsEnabled = chkManual;
            txtTENSION_UPActual.IsEnabled = chkManual;
            txtTENSION_DOWNSpecification.IsEnabled = chkManual;
            txtTENSION_DOWNActual.IsEnabled = chkManual;
            txtFRAMEWIDTH_FORNSpecification.IsEnabled = chkManual;
            txtFRAMEWIDTH_TENTERSpecification.IsEnabled = chkManual;
            txtSPEED.IsEnabled = chkManual;
            txtSPEED_MARGIN.IsEnabled = chkManual;
            txtSATURATOR_CHEMSpecification.IsEnabled = chkManual;
            txtSATPV.IsEnabled = chkManual;
            txtWASHING1Specification.IsEnabled = chkManual;
            txtWASH1PV.IsEnabled = chkManual;
            txtWASHING2Specification.IsEnabled = chkManual;
            txtWASH2PV.IsEnabled = chkManual;
            txtHOTFLUESpecification.IsEnabled = chkManual;
            txtHOTPV.IsEnabled = chkManual;
            txtROOMTEMP.IsEnabled = chkManual;
            txtROOMTEMP_MARGIN.IsEnabled = chkManual;
            txtRATIOSILICONE.IsEnabled = chkManual;
            txtCOATINGWEIGTH_MAX.IsEnabled = chkManual;
            txtCOATINGWEIGTH_MIN.IsEnabled = chkManual;
            txtWIDTHCOATSpecification.IsEnabled = chkManual;
            txtWIDTHCOATALL_MAX.IsEnabled = chkManual;
            txtWIDTHCOATALL_MIN.IsEnabled = chkManual;
            txtHUMIDITY_MAX.IsEnabled = chkManual;
            txtHUMIDITY_MIN.IsEnabled = chkManual;
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
                    #region BE_COATWIDTHMAX

                    if (items[0].BE_COATWIDTHMAX != null)
                    {
                        txtBE_COATWIDTHSpecification.Text = items[0].BE_COATWIDTHMAX.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtBE_COATWIDTHSpecification.Text = "";
                    }

                    #endregion

                    #region BE_COATWIDTHMIN

                    if (items[0].BE_COATWIDTHMIN != null)
                    {
                        txtBE_COATWIDTHActual.Text = items[0].BE_COATWIDTHMIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtBE_COATWIDTHActual.Text = "";
                    }

                    #endregion

                    #region FANRPM

                    if (items[0].FANRPM != null)
                    {
                        txtFan110Specification.Text = items[0].FANRPM.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtFan110Specification.Text = "";
                    }

                    #endregion

                    #region FANRPM_MARGIN

                    if (items[0].FANRPM_MARGIN != null)
                    {
                        txtFan110Actual.Text = items[0].FANRPM_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtFan110Actual.Text = "";
                    }

                    #endregion

                    #region EXFAN_FRONT_BACK

                    if (items[0].EXFAN_FRONT_BACK != null)
                    {
                        txtEXFAN15Specification.Text = items[0].EXFAN_FRONT_BACK.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtEXFAN15Specification.Text = "";
                    }

                    #endregion

                    #region EXFAN_MARGIN

                    if (items[0].EXFAN_MARGIN != null)
                    {
                        txtEXFAN15Actual.Text = items[0].EXFAN_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtEXFAN15Actual.Text = "";
                    }

                    #endregion

                    #region EXFAN_MIDDLE

                    if (items[0].EXFAN_MIDDLE != null)
                    {
                        txtEXFAN234Specification.Text = items[0].EXFAN_MIDDLE.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtEXFAN234Specification.Text = "";
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

                            cbBLADEDIRECTIONActual.SelectedValue = "L";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/L.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "R")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Visible;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;

                            cbBLADEDIRECTIONActual.SelectedValue = "R";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/R.png", UriKind.Relative));
                        }
                        else if (items[0].BLADEDIRECTION == "C")
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Visible;

                            cbBLADEDIRECTIONActual.SelectedValue = "C";
                            img.Source = new BitmapImage(new Uri(@"/LuckyTex.AirBag.Pages;component/ClassData/Print/Image/C.png", UriKind.Relative));
                        }
                        else
                        {
                            ImageL.Visibility = System.Windows.Visibility.Collapsed;
                            ImageR.Visibility = System.Windows.Visibility.Collapsed;
                            ImageC.Visibility = System.Windows.Visibility.Collapsed;

                            cbBLADEDIRECTIONActual.SelectedValue = null;
                            img.Source = new BitmapImage(new Uri("", UriKind.Relative));
                        }
                    }

                    #endregion

                    txtPathLineSpecification.Text = items[0].PATHLINE;

                    #region FEEDIN_MAX

                    if (items[0].FEEDIN_MAX != null)
                    {
                        txtFeedInSpecification.Text = items[0].FEEDIN_MAX.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtFeedInSpecification.Text = "";
                    }

                    #endregion

                    #region FEEDIN_MIN

                    if (items[0].FEEDIN_MIN != null)
                    {
                        txtFeedInActual.Text = items[0].FEEDIN_MIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtFeedInActual.Text = "";
                    }

                    #endregion

                    #region TENSION_UP

                    if (items[0].TENSION_UP != null)
                        txtTENSION_UPSpecification.Text = items[0].TENSION_UP.Value.ToString("#,##0.##");
                    else
                        txtTENSION_UPSpecification.Text = "";

                    #endregion

                    #region TENSION_UP_MARGIN

                    if (items[0].TENSION_UP_MARGIN != null)
                        txtTENSION_UPActual.Text = items[0].TENSION_UP_MARGIN.Value.ToString("#,##0.##");
                    else
                        txtTENSION_UPActual.Text = "";

                    #endregion

                    #region TENSION_DOWN

                    if (items[0].TENSION_DOWN != null)
                    {
                        txtTENSION_DOWNSpecification.Text = items[0].TENSION_DOWN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtTENSION_DOWNSpecification.Text = "";
                    }

                    #endregion

                    #region TENSION_DOWN_MARGIN

                    if (items[0].TENSION_DOWN_MARGIN != null)
                    {
                        txtTENSION_DOWNActual.Text = items[0].TENSION_DOWN_MARGIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtTENSION_DOWNActual.Text = "";
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

                    #region RATIOSILICONE

                    if (items[0].RATIOSILICONE != null)
                    {
                        txtRATIOSILICONE.Text = items[0].RATIOSILICONE;
                    }
                    else
                    {
                        txtRATIOSILICONE.Text = "";
                    }

                    #endregion

                    #region COATINGWEIGTH_MAX

                    if (items[0].COATINGWEIGTH_MAX != null)
                    {
                        txtCOATINGWEIGTH_MAX.Text = items[0].COATINGWEIGTH_MAX.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtCOATINGWEIGTH_MAX.Text = "";
                    }

                    #endregion

                    #region COATINGWEIGTH_MIN

                    if (items[0].COATINGWEIGTH_MIN != null)
                    {
                        txtCOATINGWEIGTH_MIN.Text = items[0].COATINGWEIGTH_MIN.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtCOATINGWEIGTH_MIN.Text = "";
                    }

                    #endregion

                    #region WIDTHCOAT

                    if (items[0].WIDTHCOAT != null)
                        txtWIDTHCOATSpecification.Text = items[0].WIDTHCOAT.Value.ToString("#,##0.##");
                    else
                        txtWIDTHCOATSpecification.Text = "";

                    #endregion

                    #region WIDTHCOATALL_MAX

                    if (items[0].WIDTHCOATALL_MAX != null)
                        txtWIDTHCOATALL_MAX.Text = items[0].WIDTHCOATALL_MAX.Value.ToString("#,##0.##");
                    else
                        txtWIDTHCOATALL_MAX.Text = "";

                    #endregion

                    #region WIDTHCOATALL_MIN

                    if (items[0].WIDTHCOATALL_MIN != null)
                        txtWIDTHCOATALL_MIN.Text = items[0].WIDTHCOATALL_MIN.Value.ToString("#,##0.##");
                    else
                        txtWIDTHCOATALL_MIN.Text = "";

                    #endregion

                    #region HUMIDITY_MAX

                    if (items[0].HUMIDITY_MAX != null)
                        txtHUMIDITY_MAX.Text = items[0].HUMIDITY_MAX.Value.ToString("#,##0.##");
                    else
                        txtHUMIDITY_MAX.Text = "";

                    #endregion

                    #region HUMIDITY_MIN

                    if (items[0].HUMIDITY_MIN != null)
                        txtHUMIDITY_MIN.Text = items[0].HUMIDITY_MIN.Value.ToString("#,##0.##");
                    else
                        txtHUMIDITY_MIN.Text = "";

                    #endregion
                }
                else
                {
                    string msg = "No Coating Condition found for selected item";

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
            string P_COATNO = string.Empty;
            decimal? P_CHEM = null;
            decimal? P_CHEM_MARGIN = null;
            decimal? P_WASH1 = null;
            decimal? P_WASH1_MARGIN = null;
            decimal? P_WASH2 = null;
            decimal? P_WASH2_MARGIN = null;
            decimal? P_HOTFLUE = null;
            decimal? P_HOTFLUE_MARGIN = null;
            decimal? P_BE_COAT_MAX = null;
            decimal? P_BE_COAT_MIN = null;
            decimal? P_ROOMTEMP = null;
            decimal? P_ROOMTEMP_MARGIN = null;
            decimal? P_FANRPM = null;
            decimal? P_FANRPM_MARGIN = null;
            decimal? P_EXFAN_FRONT_BACK = null;
            decimal? P_EXFAN_MARGIN = null;
            decimal? P_EXFAN_MIDDLE = null;
            decimal? P_ANGLEKNIFE = null;
            string P_BLADENO = string.Empty;
            string P_BLADEDIRECTION = string.Empty;
            string P_PATHLINE = string.Empty;
            decimal? P_FEEDIN_MAX = null;
            decimal? P_FEEDIN_MIN = null;
            decimal? P_TENSION_UP = null;
            decimal? P_TENSION_UP_MARGIN = null;
            decimal? P_TENSION_DOWN = null;
            decimal? P_TENSION_DOWN_MARGIN = null;
            decimal? P_FRAME_FORN = null;
            decimal? P_FRAME_TENTER = null;
            string P_OVERFEED = string.Empty;
            decimal? P_SPEED = null;
            decimal? P_SPEED_MARGIN = null;
            decimal? P_WIDTHCOAT = null;
            decimal? P_WIDTHCOATALL_MAX = null;
            decimal? P_WIDTHCOATALL_MIN = null;
            decimal? P_COATINGWEIGTH_MAX = null;
            decimal? P_COATINGWEIGTH_MIN = null;
            string P_RATIONSILICONE = string.Empty;
            decimal? P_HUMIDITYMAX = null;
            decimal? P_HUMIDITYMIN = null;
            string P_OPERATOR = string.Empty;

            try
            {
                string RESULT = string.Empty;

                if (cbItemGoods.SelectedValue != null)
                    P_ITEMCODE = cbItemGoods.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(cbCoating.Text))
                {
                    if (cbCoating.Text == "Coating 1")
                        P_COATNO = "254";
                    else if (cbCoating.Text == "Coating 2")
                        P_COATNO = "255";
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

                if (!string.IsNullOrEmpty(txtBE_COATWIDTHSpecification.Text))
                    P_BE_COAT_MAX = decimal.Parse(txtBE_COATWIDTHSpecification.Text);

                if (!string.IsNullOrEmpty(txtBE_COATWIDTHActual.Text))
                    P_BE_COAT_MIN = decimal.Parse(txtBE_COATWIDTHActual.Text);

                if (!string.IsNullOrEmpty(txtROOMTEMP.Text))
                    P_ROOMTEMP = decimal.Parse(txtROOMTEMP.Text);

                if (!string.IsNullOrEmpty(txtROOMTEMP_MARGIN.Text))
                    P_ROOMTEMP_MARGIN = decimal.Parse(txtROOMTEMP_MARGIN.Text);

                if (!string.IsNullOrEmpty(txtFan110Specification.Text))
                    P_FANRPM = decimal.Parse(txtFan110Specification.Text);

                if (!string.IsNullOrEmpty(txtFan110Actual.Text))
                    P_FANRPM_MARGIN = decimal.Parse(txtFan110Actual.Text);

                if (!string.IsNullOrEmpty(txtEXFAN15Specification.Text))
                    P_EXFAN_FRONT_BACK = decimal.Parse(txtEXFAN15Specification.Text);

                if (!string.IsNullOrEmpty(txtEXFAN15Actual.Text))
                    P_EXFAN_MARGIN = decimal.Parse(txtEXFAN15Actual.Text);

                if (!string.IsNullOrEmpty(txtEXFAN234Specification.Text))
                    P_EXFAN_MIDDLE = decimal.Parse(txtEXFAN234Specification.Text);

                if (!string.IsNullOrEmpty(txtANGLEKNIFESpecification.Text))
                    P_ANGLEKNIFE = decimal.Parse(txtANGLEKNIFESpecification.Text);

                P_BLADENO = txtBLADENOSpecification.Text;

                #region BLADEDIRECTION
                if (cbBLADEDIRECTIONActual.SelectedValue != null)
                {
                    P_BLADEDIRECTION = cbBLADEDIRECTIONActual.SelectedValue.ToString();
                }
                #endregion

                P_PATHLINE = txtPathLineSpecification.Text;

                if (!string.IsNullOrEmpty(txtFeedInSpecification.Text))
                    P_FEEDIN_MAX = decimal.Parse(txtFeedInSpecification.Text);

                if (!string.IsNullOrEmpty(txtFeedInActual.Text))
                    P_FEEDIN_MIN = decimal.Parse(txtFeedInActual.Text);

                if (!string.IsNullOrEmpty(txtTENSION_UPSpecification.Text))
                    P_TENSION_UP = decimal.Parse(txtTENSION_UPSpecification.Text);

                if (!string.IsNullOrEmpty(txtTENSION_UPActual.Text))
                    P_TENSION_UP_MARGIN = decimal.Parse(txtTENSION_UPActual.Text);

                if (!string.IsNullOrEmpty(txtTENSION_DOWNSpecification.Text))
                    P_TENSION_DOWN = decimal.Parse(txtTENSION_DOWNSpecification.Text);

                if (!string.IsNullOrEmpty(txtTENSION_DOWNActual.Text))
                    P_TENSION_DOWN_MARGIN = decimal.Parse(txtTENSION_DOWNActual.Text);

                if (!string.IsNullOrEmpty(txtFRAMEWIDTH_FORNSpecification.Text))
                    P_FRAME_FORN = decimal.Parse(txtFRAMEWIDTH_FORNSpecification.Text);

                if (!string.IsNullOrEmpty(txtFRAMEWIDTH_TENTERSpecification.Text))
                    P_FRAME_TENTER = decimal.Parse(txtFRAMEWIDTH_TENTERSpecification.Text);

                if (!string.IsNullOrEmpty(txtSPEED.Text))
                    P_SPEED = decimal.Parse(txtSPEED.Text);

                if (!string.IsNullOrEmpty(txtSPEED_MARGIN.Text))
                    P_SPEED_MARGIN = decimal.Parse(txtSPEED_MARGIN.Text);

                if (!string.IsNullOrEmpty(txtWIDTHCOATSpecification.Text))
                    P_WIDTHCOAT = decimal.Parse(txtWIDTHCOATSpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTHCOATSpecification.Text))
                    P_WIDTHCOAT = decimal.Parse(txtWIDTHCOATSpecification.Text);

                if (!string.IsNullOrEmpty(txtWIDTHCOATALL_MAX.Text))
                    P_WIDTHCOATALL_MAX = decimal.Parse(txtWIDTHCOATALL_MAX.Text);

                if (!string.IsNullOrEmpty(txtWIDTHCOATALL_MIN.Text))
                    P_WIDTHCOATALL_MIN = decimal.Parse(txtWIDTHCOATALL_MIN.Text);

                if (!string.IsNullOrEmpty(txtCOATINGWEIGTH_MAX.Text))
                    P_COATINGWEIGTH_MAX = decimal.Parse(txtCOATINGWEIGTH_MAX.Text);

                if (!string.IsNullOrEmpty(txtCOATINGWEIGTH_MIN.Text))
                    P_COATINGWEIGTH_MIN = decimal.Parse(txtCOATINGWEIGTH_MIN.Text);

                P_RATIONSILICONE = txtRATIOSILICONE.Text;

                if (!string.IsNullOrEmpty(txtHUMIDITY_MAX.Text))
                    P_HUMIDITYMAX = decimal.Parse(txtHUMIDITY_MAX.Text);

                if (!string.IsNullOrEmpty(txtHUMIDITY_MIN.Text))
                    P_HUMIDITYMIN = decimal.Parse(txtHUMIDITY_MIN.Text);

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;


                RESULT = ProcessConditionDataService.Instance.CONDITION_FINISHINGCOATING(P_ITEMCODE, P_COATNO,
                                 P_CHEM, P_CHEM_MARGIN, P_WASH1, P_WASH1_MARGIN,
                                 P_WASH2, P_WASH2_MARGIN, P_HOTFLUE, P_HOTFLUE_MARGIN,
                                 P_BE_COAT_MAX, P_BE_COAT_MIN, P_ROOMTEMP, P_ROOMTEMP_MARGIN,
                                 P_FANRPM, P_FANRPM_MARGIN, P_EXFAN_FRONT_BACK, P_EXFAN_MARGIN,
                                 P_EXFAN_MIDDLE, P_ANGLEKNIFE, P_BLADENO, P_BLADEDIRECTION,
                                 P_PATHLINE, P_FEEDIN_MAX, P_FEEDIN_MIN, P_TENSION_UP,
                                 P_TENSION_UP_MARGIN, P_TENSION_DOWN, P_TENSION_DOWN_MARGIN, P_FRAME_FORN,
                                 P_FRAME_TENTER, P_OVERFEED, P_SPEED, P_SPEED_MARGIN,
                                 P_WIDTHCOAT, P_WIDTHCOATALL_MAX, P_WIDTHCOATALL_MIN, P_COATINGWEIGTH_MAX,
                                 P_COATINGWEIGTH_MIN, P_RATIONSILICONE, P_HUMIDITYMAX, P_HUMIDITYMIN, P_OPERATOR);

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
