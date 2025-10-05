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

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

using System.Diagnostics;
using System.Printing;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ItemCodePage.xaml
    /// </summary>
    public partial class ItemCodePage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemCodePage()
        {
            InitializeComponent();
            LoadYARNCODE();
        }

        #endregion

        #region Internal Variables

        private ItemCodeSession _session = new ItemCodeSession();
        string opera = string.Empty;

        string gidITM_CODE = string.Empty;
        string gidITM_WEAVING = string.Empty;
        string gidITM_PREPARE = string.Empty;
        string gidITM_YARN = string.Empty;
        string gidYARNCODE = string.Empty;
        decimal? gidITM_WIDTH = null;
        decimal? gidWIDTHWEAVING = null;
        decimal? gidCOREWEIGHT = null;

        string gidITM_PROC1 = string.Empty;
        string gidITM_PROC2 = string.Empty;
        string gidITM_PROC3 = string.Empty;
        string gidITM_PROC4 = string.Empty;
        string gidITM_PROC5 = string.Empty;
        string gidITM_PROC6 = string.Empty;
       
        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Main Menu Button Handlers
        
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtITEMCODE.Text))
            {
                if (!string.IsNullOrEmpty(txtITEMWEAV.Text))
                {
                    if (!string.IsNullOrEmpty(txtITEMPREPARE.Text))
                    {
                        if (!string.IsNullOrEmpty(txtITEMYARN.Text))
                        {
                            if (!string.IsNullOrEmpty(txtWIDTH.Text))
                            {
                                if (!string.IsNullOrEmpty(txtWEAVEWIDTH.Text))
                                {
                                    if (!string.IsNullOrEmpty(txtCOREWEIGHT.Text))
                                    {
                                        #region Save
                                        string P_ITEMCODE = string.Empty;
                                        string P_ITEMWEAV = string.Empty;
                                        string P_ITEMPREPARE = string.Empty;
                                        string P_ITEMYARN = string.Empty;
                                        decimal? P_WIDTH = null;
                                        decimal? P_WEAVEWIDTH = null;
                                        decimal? P_COREWEIGHT = null;
                                        string P_YARNCODE = string.Empty;
                                        string P_PROC1 = string.Empty;
                                        string P_PROC2 = string.Empty;
                                        string P_PROC3 = string.Empty;
                                        string P_PROC4 = string.Empty;
                                        string P_PROC5 = string.Empty;
                                        string P_PROC6 = string.Empty;
                                        string P_OPERATOR = string.Empty;

                                        if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                                            P_ITEMCODE = txtITEMCODE.Text;

                                        if (!string.IsNullOrEmpty(txtITEMWEAV.Text))
                                            P_ITEMWEAV = txtITEMWEAV.Text;

                                        if (!string.IsNullOrEmpty(txtITEMPREPARE.Text))
                                            P_ITEMPREPARE = txtITEMPREPARE.Text;

                                        if (!string.IsNullOrEmpty(txtITEMYARN.Text))
                                            P_ITEMYARN = txtITEMYARN.Text;

                                        #region P_WIDTH

                                        if (!string.IsNullOrEmpty(txtWIDTH.Text))
                                        {
                                            try
                                            {
                                                P_WIDTH = decimal.Parse(txtWIDTH.Text);
                                            }
                                            catch
                                            {
                                                P_WIDTH = 0;
                                            }
                                        }

                                        #endregion

                                        #region P_WEAVEWIDTH

                                        if (!string.IsNullOrEmpty(txtWEAVEWIDTH.Text))
                                        {
                                            try
                                            {
                                                P_WEAVEWIDTH = decimal.Parse(txtWEAVEWIDTH.Text);
                                            }
                                            catch
                                            {
                                                P_WEAVEWIDTH = 0;
                                            }
                                        }

                                        #endregion

                                        #region P_COREWEIGHT

                                        if (!string.IsNullOrEmpty(txtCOREWEIGHT.Text))
                                        {
                                            try
                                            {
                                                P_COREWEIGHT = decimal.Parse(txtCOREWEIGHT.Text);
                                            }
                                            catch
                                            {
                                                P_COREWEIGHT = 0;
                                            }
                                        }

                                        #endregion

                                        if (cbYARNCODE.SelectedValue != null)
                                        {
                                            P_YARNCODE = cbYARNCODE.SelectedValue.ToString();
                                        }

                                        if (chkPROC1_DRYER.IsChecked == true)
                                            P_PROC1 = "DRYER";

                                        if (chkPROC2_SCOURING.IsChecked == true)
                                            P_PROC2 = "SCOURING";

                                        if (chkPROC3_DRYING.IsChecked == true)
                                            P_PROC3 = "DRYING";

                                        if (chkPROC4_HEATSETTING.IsChecked == true)
                                            P_PROC4 = "HEAT SETTING";

                                        if (chkPROC5_COATING.IsChecked == true)
                                            P_PROC5 = "COATING";

                                        if (chkPROC6_INSPECTION.IsChecked == true)
                                            P_PROC6 = "INSPECTION";

                                        if (!string.IsNullOrEmpty(txtOperator.Text))
                                            P_OPERATOR = txtOperator.Text;

                                        Save(P_ITEMCODE, P_ITEMWEAV, P_ITEMPREPARE, P_ITEMYARN,
                                             P_WIDTH, P_WEAVEWIDTH, P_COREWEIGHT
                                             , P_YARNCODE, P_PROC1, P_PROC2, P_PROC3
                                             , P_PROC4, P_PROC5, P_PROC6, P_OPERATOR);

                                        #endregion
                                    }
                                    else
                                    {
                                        "Core Weight isn't Null".ShowMessageBox();
                                    }
                                }
                                else
                                {
                                    "Weaving Width isn't Null".ShowMessageBox();
                                }
                            }
                            else
                            {
                                "Width isn't Null".ShowMessageBox();
                            }
                        }
                        else
                        {
                            "Item Yarn isn't Null".ShowMessageBox();
                        }
                    }
                    else
                    {
                        "Item Prepare isn't Null".ShowMessageBox();
                    }
                }
                else
                {
                    "Item Weaving isn't Null".ShowMessageBox();
                }
            }
            else
            {
                "Item Code isn't Null".ShowMessageBox();
            }
        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        #endregion

        #region Controls Handlers

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtITEMCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtITEMCODE.Text != "")
                {
                    txtITEMWEAV.SelectAll();
                    txtITEMWEAV.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtITEMWEAV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtITEMWEAV.Text != "")
                {
                    txtITEMPREPARE.SelectAll();
                    txtITEMPREPARE.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtITEMPREPARE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtITEMPREPARE.Text != "")
                {
                    txtITEMYARN.SelectAll();
                    txtITEMYARN.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtITEMYARN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtITEMWEAV.Text != "")
                {
                    txtWIDTH.SelectAll();
                    txtWIDTH.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtWIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtWIDTH.Text != "")
                {
                    txtWEAVEWIDTH.SelectAll();
                    txtWEAVEWIDTH.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtWEAVEWIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtWEAVEWIDTH.Text != "")
                {
                    txtCOREWEIGHT.SelectAll();
                    txtCOREWEIGHT.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtCOREWEIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtWEAVEWIDTH.Text != "")
                {
                    chkPROC1_DRYER.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        #endregion

        #region gridItem_SelectedCellsChanged

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void gridItem_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridItem.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridItem);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            gidITM_CODE = string.Empty;
                            gidITM_WEAVING = string.Empty;
                            gidITM_YARN = string.Empty;
                            gidYARNCODE = string.Empty;
                            gidITM_PREPARE = string.Empty;
                            gidITM_WIDTH = null;
                            gidWIDTHWEAVING = null;
                            gidCOREWEIGHT = null;
                            gidITM_PROC1 = string.Empty;
                            gidITM_PROC2 = string.Empty;
                            gidITM_PROC3 = string.Empty;
                            gidITM_PROC4 = string.Empty;
                            gidITM_PROC5 = string.Empty;
                            gidITM_PROC6 = string.Empty;

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_CODE != null)
                            {
                                gidITM_CODE = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_CODE;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_WEAVING != null)
                            {
                                gidITM_WEAVING = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_WEAVING;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_YARN != null)
                            {
                                gidITM_YARN = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_YARN;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).YARNCODE != null)
                            {
                                gidYARNCODE = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).YARNCODE;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PREPARE != null)
                            {
                                gidITM_PREPARE = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PREPARE;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_WIDTH != null)
                            {
                                gidITM_WIDTH = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_WIDTH;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).WIDTHWEAVING != null)
                            {
                                gidWIDTHWEAVING = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).WIDTHWEAVING;
                            }
                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).COREWEIGHT != null)
                            {
                                gidCOREWEIGHT = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).COREWEIGHT;
                            }

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC1 != null)
                            {
                                gidITM_PROC1 = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC1;
                            }

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC2 != null)
                            {
                                gidITM_PROC2 = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC2;
                            }

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC3 != null)
                            {
                                gidITM_PROC3 = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC3;
                            }

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC4 != null)
                            {
                                gidITM_PROC4 = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC4;
                            }

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC5 != null)
                            {
                                gidITM_PROC5 = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC5;
                            }

                            if (((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC6 != null)
                            {
                                gidITM_PROC6 = ((LuckyTex.Models.ITM_SEARCHITEMCODE)(gridItem.CurrentCell.Item)).ITM_PROC6;
                            }

                            EnabledCon(false);
                        }
                    }
                }
                else
                {
                    gidITM_CODE = string.Empty;
                    gidITM_WEAVING = string.Empty;
                    gidITM_YARN = string.Empty;
                    gidYARNCODE = string.Empty;
                    gidITM_PREPARE = string.Empty;
                    gidITM_WIDTH = null;
                    gidWIDTHWEAVING = null;
                    gidCOREWEIGHT = null;
                    gidITM_PROC1 = string.Empty;
                    gidITM_PROC2 = string.Empty;
                    gidITM_PROC3 = string.Empty;
                    gidITM_PROC4 = string.Empty;
                    gidITM_PROC5 = string.Empty;
                    gidITM_PROC6 = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadYARNCODE

        private void LoadYARNCODE()
        {
            if (cbYARNCODE.ItemsSource == null)
            {
                string[] str = new string[] { "T", "J", "CHN" };

                cbYARNCODE.ItemsSource = str;
                cbYARNCODE.SelectedIndex = 0;
            }
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtITEMCODE.Text = "";
            txtITEMWEAV.Text = "";
            cbYARNCODE.SelectedValue = null;
            txtITEMPREPARE.Text = "";
            txtITEMYARN.Text = "";
            txtWIDTH.Text = "";
            txtWEAVEWIDTH.Text = "";
            txtCOREWEIGHT.Text = "";

            chkPROC1_DRYER.IsChecked = false;
            chkPROC2_SCOURING.IsChecked = false;
            chkPROC3_DRYING.IsChecked = false;
            chkPROC4_HEATSETTING.IsChecked = false;
            chkPROC5_COATING.IsChecked = false;
            chkPROC6_INSPECTION.IsChecked = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridItem.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridItem.SelectedItems.Clear();
            else
                this.gridItem.SelectedItem = null;

            gridItem.ItemsSource = null;

            gidITM_CODE = string.Empty;
            gidITM_WEAVING = string.Empty;
            gidITM_YARN = string.Empty;
            gidYARNCODE = string.Empty;
            gidITM_PREPARE = string.Empty;
            gidITM_WIDTH = null;
            gidWIDTHWEAVING = null;
            gidCOREWEIGHT = null;
            gidITM_PROC1 = string.Empty;
            gidITM_PROC2 = string.Empty;
            gidITM_PROC3 = string.Empty;
            gidITM_PROC4 = string.Empty;
            gidITM_PROC5 = string.Empty;
            gidITM_PROC6 = string.Empty;

            EnabledCon(true);

            txtITEMCODE.SelectAll();
            txtITEMCODE.Focus();
        }

        #endregion

        #region Search

        private void Search()
        {
            string P_ITEMCODE = string.Empty;
            string P_ITEMWEAV = string.Empty;
            string P_ITEMPREPARE = string.Empty;
            string P_ITEMYARN = string.Empty;
            string P_YARNCODE = string.Empty;

            #region Search

            if (!string.IsNullOrEmpty(txtITEMCODE.Text))
            {
                P_ITEMCODE = "%" + txtITEMCODE.Text + "%";
            }
            else
            {
                P_ITEMCODE = null;
            }

            if (!string.IsNullOrEmpty(txtITEMWEAV.Text))
            {
                P_ITEMWEAV = "%" + txtITEMWEAV.Text + "%";
            }
            else
            {
                P_ITEMWEAV = null;
            }

            if (!string.IsNullOrEmpty(txtITEMPREPARE.Text))
            {
                P_ITEMPREPARE = "%" + txtITEMPREPARE.Text + "%";
            }
            else
            {
                P_ITEMPREPARE = null;
            }

            if (!string.IsNullOrEmpty(txtITEMYARN.Text))
            {
                P_ITEMYARN = "%" + txtITEMYARN.Text + "%";
            }
            else
            {
                P_ITEMYARN = null;
            }

            if (cbYARNCODE.SelectedValue != null)
            {
                P_YARNCODE = cbYARNCODE.SelectedValue.ToString();
            }
            else
            {
                P_YARNCODE = null;
            }

            #endregion

            List<ITM_SEARCHITEMCODE> results = null;

            results = ItemCodeService.Instance.ITM_SEARCHITEMCODE(P_ITEMCODE, P_ITEMWEAV, P_ITEMPREPARE, P_ITEMYARN, P_YARNCODE);

            if (results != null && results.Count > 0)
            {
                gridItem.ItemsSource = results;
            }
            else
            {
                gridItem.ItemsSource = null;
            }
        }

        #endregion

        #region Edit

        private void Edit()
        {
            if (!string.IsNullOrEmpty(gidITM_CODE))
            {
                txtITEMCODE.Text = gidITM_CODE;
            }
            if (!string.IsNullOrEmpty(gidITM_WEAVING))
            {
                txtITEMWEAV.Text = gidITM_WEAVING;
            }
            if (!string.IsNullOrEmpty(gidITM_PREPARE))
            {
                txtITEMPREPARE.Text = gidITM_PREPARE;
            }
            if (!string.IsNullOrEmpty(gidITM_YARN))
            {
                txtITEMYARN.Text = gidITM_YARN;
            }
            if (!string.IsNullOrEmpty(gidYARNCODE))
            {
                cbYARNCODE.SelectedValue = gidYARNCODE;
            }

            if (gidITM_WIDTH != null)
            {
                txtWIDTH.Text = gidITM_WIDTH.Value.ToString("#,##0.##");
            }
            if (gidWIDTHWEAVING != null)
            {
                txtWEAVEWIDTH.Text = gidWIDTHWEAVING.Value.ToString("#,##0.##");
            }
            if (gidCOREWEIGHT != null)
            {
                txtCOREWEIGHT.Text = gidCOREWEIGHT.Value.ToString("#,##0.##");
            }

            #region gidITM_PROC1

            if (!string.IsNullOrEmpty(gidITM_PROC1))
            {
                chkPROC1_DRYER.IsChecked = true;
            }
            else
            {
                chkPROC1_DRYER.IsChecked = false;
            }

            #endregion

            #region gidITM_PROC2

            if (!string.IsNullOrEmpty(gidITM_PROC2))
            {
                chkPROC2_SCOURING.IsChecked = true;
            }
            else
            {
                chkPROC2_SCOURING.IsChecked = false;
            }

            #endregion

            #region gidITM_PROC3

            if (!string.IsNullOrEmpty(gidITM_PROC3))
            {
                chkPROC3_DRYING.IsChecked = true;
            }
            else
            {
                chkPROC3_DRYING.IsChecked = false;
            }

            #endregion

            #region gidITM_PROC4

            if (!string.IsNullOrEmpty(gidITM_PROC4))
            {
                chkPROC4_HEATSETTING.IsChecked = true;
            }
            else
            {
                chkPROC4_HEATSETTING.IsChecked = false;
            }

            #endregion

            #region gidITM_PROC5

            if (!string.IsNullOrEmpty(gidITM_PROC5))
            {
                chkPROC5_COATING.IsChecked = true;
            }
            else
            {
                chkPROC5_COATING.IsChecked = false;
            }

            #endregion

            #region gidITM_PROC6

            if (!string.IsNullOrEmpty(gidITM_PROC6))
            {
                chkPROC6_INSPECTION.IsChecked = true;
            }
            else
            {
                chkPROC6_INSPECTION.IsChecked = false;
            }

            #endregion

            EnabledCon(true);

            txtITEMCODE.IsEnabled = false;
            txtITEMWEAV.IsEnabled = false;
        }

        #endregion

        #region EnabledCon

        private void EnabledCon(bool ena)
        {
            txtITEMCODE.IsEnabled = ena;
            txtITEMWEAV.IsEnabled = ena;
            txtITEMPREPARE.IsEnabled = ena;
            txtITEMYARN.IsEnabled = ena;
            cbYARNCODE.IsEnabled = ena;
           
            txtWIDTH.IsEnabled = ena;
            txtWEAVEWIDTH.IsEnabled = ena;
            txtCOREWEIGHT.IsEnabled = ena;

            chkPROC1_DRYER.IsEnabled = ena;
            chkPROC2_SCOURING.IsEnabled = ena;
            chkPROC3_DRYING.IsEnabled = ena;
            chkPROC4_HEATSETTING.IsEnabled = ena;
            chkPROC5_COATING.IsEnabled = ena;
            chkPROC6_INSPECTION.IsEnabled = ena;
        }

        #endregion

        #region Save

        private void Save(string P_ITEMCODE, string P_ITEMWEAV, string P_ITEMPREPARE, string P_ITEMYARN,
        decimal? P_WIDTH, decimal? P_WEAVEWIDTH, decimal? P_COREWEIGHT, string P_YARNCODE, string P_PROC1, string P_PROC2,
        string P_PROC3, string P_PROC4, string P_PROC5, string P_PROC6, string P_OPERATOR)
        {
            string R_RESULT = string.Empty;

            R_RESULT = ItemCodeService.Instance.ITM_INSERTUPDATEITEMCODE(P_ITEMCODE, P_ITEMWEAV, P_ITEMPREPARE, P_ITEMYARN
                , P_WIDTH, P_WEAVEWIDTH, P_COREWEIGHT, P_YARNCODE
                , P_PROC1, P_PROC2, P_PROC3, P_PROC4, P_PROC5, P_PROC6, P_OPERATOR);

            if (string.IsNullOrEmpty(R_RESULT))
            {
                "Save complete".ShowMessageBox();

                ClearControl();
            }
            else
            {
                R_RESULT.ShowMessageBox(true);
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user)
        {
            if (opera != null)
            {
                opera = user;
            }
        }

        #endregion

    }
}
