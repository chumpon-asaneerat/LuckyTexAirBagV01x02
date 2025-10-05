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
    /// Interaction logic for DrawingPage.xaml
    /// </summary>
    public partial class DrawingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingPage()
        {
            InitializeComponent();
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

                txtNOYARN.Focus();
                txtNOYARN.SelectAll();
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

                txtNOYARN.Focus();
                txtNOYARN.SelectAll();
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
                if (!string.IsNullOrEmpty(txtNOYARN.Text))
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
                        "Heald No isn't Null".ShowMessageBox();
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

        private void txtNOYARN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtREEDTYPE.Focus();
                txtREEDTYPE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtREEDTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNODENT.Focus();
                txtNODENT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNODENT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPITCH.Focus();
                txtPITCH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtPITCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtAIRSPACE.Focus();
                txtAIRSPACE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtAIRSPACE_KeyDown(object sender, KeyEventArgs e)
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

        #endregion

        #region ClearData

        private void ClearData()
        {
            try
            {
                txtNOYARN.Text = "";
                txtREEDTYPE.Text = "";
                txtNODENT.Text = "";
                txtPITCH.Text = "";
                txtAIRSPACE.Text = "";
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
            txtNOYARN.IsEnabled = chkManual;
            txtREEDTYPE.IsEnabled = chkManual;
            txtNODENT.IsEnabled = chkManual;
            txtPITCH.IsEnabled = chkManual;
            txtAIRSPACE.IsEnabled = chkManual;
        }
        #endregion

        #region LoadDrawing

        private void LoadDrawing(string itm_code)
        {
            try
            {
                List<DRAW_GETSPECBYCHOPNO> results = DrawingDataService.Instance.ITM_GETITEMPREPARELIST(itm_code);

                if (results != null && results.Count > 0)
                {
                    txtNOYARN.Text = results[0].NOYARN.Value.ToString("#,##0.##");
                    txtREEDTYPE.Text = results[0].REEDTYPE.Value.ToString("#,##0.##");
                    txtNODENT.Text = results[0].NODENT.Value.ToString("#,##0.##");
                    txtPITCH.Text = results[0].PITCH.Value.ToString("#,##0.##");
                    txtAIRSPACE.Text = results[0].AIRSPACE.Value.ToString("#,##0.##");
                }
                else
                {
                    string msg = "No Drawing Spec. found for selected item";

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
            decimal? P_NOYARN = null;
            decimal? P_REEDTYPE = null;
            decimal? P_NODENT = null;
            decimal? P_PITCH = null;
            decimal? P_AIRSPACE = null;
            string P_OPERATOR = string.Empty;

            try
            {
                string RESULT = string.Empty;

                if (cbItemGoods.SelectedValue != null)
                    P_ITMPREPARE = cbItemGoods.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtNOYARN.Text))
                    P_NOYARN = decimal.Parse(txtNOYARN.Text);

                if (!string.IsNullOrEmpty(txtREEDTYPE.Text))
                    P_REEDTYPE = decimal.Parse(txtREEDTYPE.Text);

                if (!string.IsNullOrEmpty(txtNODENT.Text))
                    P_NODENT = decimal.Parse(txtNODENT.Text);

                if (!string.IsNullOrEmpty(txtPITCH.Text))
                    P_PITCH = decimal.Parse(txtPITCH.Text);

                if (!string.IsNullOrEmpty(txtAIRSPACE.Text))
                    P_AIRSPACE = decimal.Parse(txtAIRSPACE.Text);

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;

                RESULT = ProcessConditionDataService.Instance.CONDITION_DRAWING(P_ITMPREPARE, P_NOYARN, P_REEDTYPE, P_NODENT, P_PITCH, P_AIRSPACE, P_OPERATOR);
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
