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

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WarpingSettingPage.xaml
    /// </summary>
    public partial class WarpingSettingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingSettingPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private WarpingSession _session = new WarpingSession();
        string opera = string.Empty;
        string mcNo = string.Empty;
        string PALLETNO = string.Empty;

        string OldSide = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItemGood();
            LoadShift();

            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;

            if (mcNo != "")
            {
                txtMCNo.Text = mcNo;

                WARP_GETCREELSETUPSTATUS(mcNo, "A");
            }
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

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (gridWarping.Items.Count > 0)
            {
                if (!string.IsNullOrEmpty(txtWARPERHEADNO.Text))
                {
                    if (!string.IsNullOrEmpty(txtReedNo.Text))
                    {
                        if (txtNoCH.Text != "" && txtTotalCH.Text != "")
                        {
                            decimal noch = 0;
                            decimal total = 0;

                            #region noch
                            try
                            {
                                noch = decimal.Parse(txtNoCH.Text);
                            }
                            catch
                            {
                                noch = 0;
                            }
                            #endregion

                            #region total
                            try
                            {
                                total = decimal.Parse(txtTotalCH.Text);
                            }
                            catch
                            {
                                total = 0;
                            }
                            #endregion

                            if (noch == total)
                            {
                                if (SaveWarping() == true)
                                {
                                    string save = "Wrapper No " + txtMCNo.Text + " , Side " + cbSide.SelectedValue.ToString() + " has been created and start conditioning.";

                                    save.ShowMessageBox(false);

                                    ClearControl();
                                }
                            }
                            else
                            {
                                "Total CH != No CH โปรดเลือก Pallet เพิ่ม".ShowMessageBox(false);
                            }
                        }
                        else
                        {
                            "Can't Save".ShowMessageBox(false);
                        }
                    }
                    else
                    {
                        "Reed No isn't Null".ShowMessageBox(false);
                    }
                }
                else
                {
                    "Warper No isn't Null".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #endregion

        #region cbItemCode_LostFocus

        private void cbItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbItemCode.SelectedValue != null && !string.IsNullOrEmpty(txtMCNo.Text))
                WARP_GETSPECBYCHOPNOANDMC(cbItemCode.SelectedValue.ToString(), txtMCNo.Text);
        }

        #endregion

        #region cbSide_LostFocus

        private void cbSide_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMCNo.Text) && !string.IsNullOrEmpty(cbSide.SelectedValue.ToString()))
            {
                if (OldSide != cbSide.SelectedValue.ToString())
                {
                    WARP_GETCREELSETUPSTATUS(txtMCNo.Text, cbSide.SelectedValue.ToString());
                }
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

        #region txtWARPERHEADNO_KeyDown

        private void txtWARPERHEADNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        private void txtPALLETNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (gridWarping.ItemsSource != null)
                {
                    if (!string.IsNullOrEmpty(txtPALLETNO.Text.Trim()))
                    {
                        string barcode = string.Empty;

                        barcode = txtPALLETNO.Text;

                        SelectPALLETNO(barcode);

                        txtPALLETNO.Text = "";
                        txtPALLETNO.SelectAll();
                        txtPALLETNO.Focus();
                    }
                }
                else
                {
                    "Data isn't null".ShowMessageBox(true);

                    txtPALLETNO.Text = "";
                    txtPALLETNO.SelectAll();
                    txtPALLETNO.Focus();
                }
            }
        }

        #endregion

        #region gridWarping_SelectedCellsChanged

        #region GetDataGridRows

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

        #endregion

        #region gridWarping_SelectedCellsChanged

        private void gridWarping_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWarping.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWarping);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                            if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.CurrentCell.Item)).PALLETNO != null)
                            {
                                PALLETNO = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.CurrentCell.Item)).PALLETNO;
                            }
                            else
                            {
                                PALLETNO = string.Empty;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Selected_Checked

        private void Selected_Checked(object sender, RoutedEventArgs e)
        {
            CalTotal();
        }

        #endregion

        #region Selected_Unchecked

        private void Selected_Unchecked(object sender, RoutedEventArgs e)
        {
            CalTotal();
        }

        #endregion

        #region Use_LostFocus

        private void Use_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).ITM_YARN != ""
                        && ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).PALLETNO != "")
                    {
                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Use != null
                            && ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Reject != null)
                        {
                            EditReceive(((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).PALLETNO, ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).ITM_YARN
                                , ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Use, ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Reject);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region Reject_LostFocus

        private void Reject_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).ITM_YARN != ""
                        && ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).PALLETNO != "")
                    {
                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Use != null
                            && ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Reject != null)
                        {
                            EditReceive(((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).PALLETNO, ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).ITM_YARN
                                , ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Use, ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)(gridWarping.SelectedItem)).Reject);
                        }
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

        #region private Methods

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMPREPARELIST> items = _session.GetItemCodeData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_PREPARE";
                this.cbItemCode.SelectedValuePath = "ITM_PREPARE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadShift

        private void LoadShift()
        {
            if (cbSide.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B" };

                cbSide.ItemsSource = str;
                cbSide.SelectedIndex = 0;
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            cbItemCode.SelectedValue = null;
            cbSide.SelectedValue = null;
            //txtMCNo.Text = "";
            txtNoCH.Text = "";
            txtReedNo.Text = "";
            txtWaxApp.Text = "";

            rbMassProduction.IsChecked = true;
            rbTest.IsChecked = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarping.SelectedItems.Clear();
            else
                this.gridWarping.SelectedItem = null;

            gridWarping.ItemsSource = null;

            txtTotalCH.Text = "0";

            PALLETNO = string.Empty;

            cbSide.SelectedValue = "A";
            OldSide = "A";

            cbItemCode.IsEnabled = true;
            txtWARPERHEADNO.IsEnabled = true;

            txtWARPERHEADNO.Text = string.Empty;

            if (!string.IsNullOrEmpty(txtMCNo.Text) && !string.IsNullOrEmpty(cbSide.SelectedValue.ToString()))
            {
                if (OldSide != cbSide.SelectedValue.ToString())
                {
                    WARP_GETCREELSETUPSTATUS(txtMCNo.Text, cbSide.SelectedValue.ToString());
                }
            }

            txtNoCH.SelectAll();
            txtNoCH.Focus();
        }

        #endregion

        #region EditReceive
        private void EditReceive(string PALLETNO, string ITM_YARN, decimal? Use, decimal? Reject)
        {
            if (gridWarping.SelectedItems.Count > 0)
            {
                try
                {
                    List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN> dataList = new List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN>();

                    int o = 0;
                    foreach (var row in gridWarping.Items)
                    {
                        LuckyTex.Models.WARP_PALLETLISTBYITMYARN dataItem = new LuckyTex.Models.WARP_PALLETLISTBYITMYARN();

                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).PALLETNO == PALLETNO
                            && ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).ITM_YARN == ITM_YARN)
                        {
                            dataItem.IsSelect = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).IsSelect;

                            dataItem.ITM_YARN = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).ITM_YARN;
                            dataItem.RECEIVEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEDATE;
                            dataItem.PALLETNO = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).PALLETNO;
                            dataItem.RECEIVEWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEWEIGHT;

                            dataItem.USEDWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).USEDWEIGHT;

                            dataItem.VERIFY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).VERIFY;
                            dataItem.REJECTID = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).REJECTID;
                            dataItem.FINISHFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).FINISHFLAG;
                            dataItem.RETURNFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RETURNFLAG;

                            dataItem.CREATEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEDATE;
                            dataItem.CREATEBY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEBY;
                            dataItem.NoCH = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).NoCH;
                            dataItem.Use = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Use;
                            dataItem.Reject = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Reject;

                            dataItem.Remain = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).NoCH - Use - Reject;
                            
                            dataList.Add(dataItem);

                        }
                        else
                        {
                            dataItem.IsSelect = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).IsSelect;

                            dataItem.ITM_YARN = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).ITM_YARN;
                            dataItem.RECEIVEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEDATE;
                            dataItem.PALLETNO = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).PALLETNO;
                            dataItem.RECEIVEWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEWEIGHT;

                            dataItem.USEDWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).USEDWEIGHT;

                            dataItem.VERIFY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).VERIFY;
                            dataItem.REJECTID = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).REJECTID;
                            dataItem.FINISHFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).FINISHFLAG;
                            dataItem.RETURNFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RETURNFLAG;

                            dataItem.CREATEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEDATE;
                            dataItem.CREATEBY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEBY;
                            dataItem.NoCH = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).NoCH;
                            dataItem.Use = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Use;
                            dataItem.Reject = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Reject;
                            dataItem.Remain = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Remain;

                            dataList.Add(dataItem);
                        }
                        o++;
                    }

                    this.gridWarping.ItemsSource = dataList;

                    CalTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region SelectPALLETNO
        private void SelectPALLETNO(string barcode)
        {
            try
            {
                bool chkPALLETNO = false;

                List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN> dataList = new List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN>();

                int o = 0;
                foreach (var row in gridWarping.Items)
                {
                    LuckyTex.Models.WARP_PALLETLISTBYITMYARN dataItem = new LuckyTex.Models.WARP_PALLETLISTBYITMYARN();

                    if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).PALLETNO == barcode)
                    {
                        dataItem.IsSelect = true;

                        dataItem.ITM_YARN = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).ITM_YARN;
                        dataItem.RECEIVEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEDATE;
                        dataItem.PALLETNO = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).PALLETNO;
                        dataItem.RECEIVEWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEWEIGHT;

                        dataItem.USEDWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).USEDWEIGHT;

                        dataItem.VERIFY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).VERIFY;
                        dataItem.REJECTID = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).REJECTID;
                        dataItem.FINISHFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).FINISHFLAG;
                        dataItem.RETURNFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RETURNFLAG;

                        dataItem.CREATEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEDATE;
                        dataItem.CREATEBY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEBY;
                        dataItem.NoCH = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).NoCH;
                        dataItem.Use = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Use;
                        dataItem.Reject = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Reject;
                        dataItem.Remain = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Remain;

                        dataList.Add(dataItem);

                        chkPALLETNO = true;
                    }
                    else
                    {
                        dataItem.IsSelect = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).IsSelect;

                        dataItem.ITM_YARN = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).ITM_YARN;
                        dataItem.RECEIVEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEDATE;
                        dataItem.PALLETNO = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).PALLETNO;
                        dataItem.RECEIVEWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RECEIVEWEIGHT;

                        dataItem.USEDWEIGHT = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).USEDWEIGHT;

                        dataItem.VERIFY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).VERIFY;
                        dataItem.REJECTID = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).REJECTID;
                        dataItem.FINISHFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).FINISHFLAG;
                        dataItem.RETURNFLAG = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).RETURNFLAG;

                        dataItem.CREATEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEDATE;
                        dataItem.CREATEBY = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).CREATEBY;
                        dataItem.NoCH = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).NoCH;
                        dataItem.Use = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Use;
                        dataItem.Reject = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Reject;
                        dataItem.Remain = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[o])).Remain;

                        dataList.Add(dataItem);
                    }
                    o++;
                }

                this.gridWarping.ItemsSource = dataList;

                CalTotal();

                if (chkPALLETNO == false)
                    "Pallet No hadn't in Data".ShowMessageBox(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region CalTotal

        private void CalTotal()
        {
            if (gridWarping.Items.Count > 0)
            {
                try
                {
                    decimal use = 0;

                    int i = 0;
                    foreach (var row in gridWarping.Items)
                    {
                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).IsSelect == true)
                        {
                            use += ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Use.Value;
                        }

                        i++;
                    }

                    txtTotalCH.Text = use.ToString("#,##0.##");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                txtTotalCH.Text = "0";
        }

        #endregion

        #region WARP_GETSPECBYCHOPNOANDMC

        private void WARP_GETSPECBYCHOPNOANDMC(string P_ITMPREPARE, string P_MCNO)
        {
            List<WARP_GETSPECBYCHOPNOANDMC> results = WarpingDataService.Instance.WARP_GETSPECBYCHOPNOANDMC(P_ITMPREPARE, P_MCNO);

            if (results.Count > 0)
            {
                txtNoCH.Text = results[0].NOCH.Value.ToString("#,##0.##");
   
                string wax = results[0].WAXING;

                if (wax != string.Empty)
                {
                    if (wax == "Y")
                        txtWaxApp.Text = "Wax";
                    else if (wax == "N")
                        txtWaxApp.Text = "No Wax";
                }

                WARP_PALLETLISTBYITMYARN(results[0].ITM_YARN);

                //WARP_PALLETLISTBYITMYARN(P_ITMPREPARE);
            }
            else
            {
                txtNoCH.Text = "";
                txtWaxApp.Text = "";

                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarping.SelectedItems.Clear();
                else
                    this.gridWarping.SelectedItem = null;

                gridWarping.ItemsSource = null;
            }
        }

        #endregion

        #region WARP_PALLETLISTBYITMYARN

        private void WARP_PALLETLISTBYITMYARN(string ITM_YARN)
        {
            List<WARP_PALLETLISTBYITMYARN> results = WarpingDataService.Instance.WARP_PALLETLISTBYITMYARN(ITM_YARN,null);

            if (results.Count > 0)
            {
                List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN> dataList = new List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN>();
                int i = 0;

                foreach(var row in results)
                {
                    LuckyTex.Models.WARP_PALLETLISTBYITMYARN dataItemNew = new LuckyTex.Models.WARP_PALLETLISTBYITMYARN();

                    dataItemNew.IsSelect = false;
 
                    dataItemNew.ITM_YARN = results[i].ITM_YARN;
                    dataItemNew.RECEIVEDATE = results[i].RECEIVEDATE;
                    dataItemNew.PALLETNO = results[i].PALLETNO;
                    dataItemNew.RECEIVEWEIGHT = results[i].RECEIVEWEIGHT;

                    dataItemNew.USEDWEIGHT = results[i].USEDWEIGHT;

                    dataItemNew.VERIFY = results[i].VERIFY;
                    dataItemNew.REJECTID = results[i].REJECTID;
                    dataItemNew.FINISHFLAG = results[i].FINISHFLAG;
                    dataItemNew.RETURNFLAG = results[i].RETURNFLAG;

                    dataItemNew.CREATEDATE = results[i].CREATEDATE;
                    dataItemNew.CREATEBY = results[i].CREATEBY;
                    dataItemNew.NoCH = results[i].NoCH;
                    dataItemNew.Use = results[i].Use;
                    dataItemNew.Reject = results[i].Reject;
                    dataItemNew.Remain = results[i].Remain;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridWarping.ItemsSource = dataList;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarping.SelectedItems.Clear();
                else
                    this.gridWarping.SelectedItem = null;

                gridWarping.ItemsSource = null;

            }
        }

        #endregion

        #region WARP_GETCREELSETUPSTATUS

        private void WARP_GETCREELSETUPSTATUS(string P_MCNO, string P_SIDE)
        {
            List<WARP_GETCREELSETUPSTATUS> results = WarpingDataService.Instance.WARP_GETCREELSETUPSTATUS(P_MCNO, P_SIDE);

            if (results.Count > 0)
            {
                DateTime dt = DateTime.Now;

                cbItemCode.SelectedValue = results[0].ITM_PREPARE;

                if (results[0].WARPHEADNO != null)
                    txtWARPERHEADNO.Text = results[0].WARPHEADNO + dt.ToString("yy");

                if (results[0].WTYPE == "Y")
                    txtWaxApp.Text = "Wax";
                else if (results[0].WTYPE == "N")
                    txtWaxApp.Text = "No Wax";

                if (!string.IsNullOrEmpty(results[0].SIDE))
                    cbSide.SelectedValue = results[0].SIDE;

                if (!string.IsNullOrEmpty(results[0].PRODUCTTYPEID))
                {
                    if (results[0].PRODUCTTYPEID == "1")
                    {
                        rbMassProduction.IsChecked = true;
                        rbTest.IsChecked = false;
                    }
                    else if (results[0].PRODUCTTYPEID == "2")
                    {
                        rbMassProduction.IsChecked = false;
                        rbTest.IsChecked = true;
                    }
                }

                if (results[0].ACTUALCH != null)
                {
                    txtTotalCH.Text = results[0].ACTUALCH.Value.ToString("#,##0.##");
                }

                txtMCNo.Text = results[0].WARPMC;
                txtReedNo.Text = results[0].REEDNO;

                List<WARP_GETSPECBYCHOPNOANDMC> resultNoCH = WarpingDataService.Instance.WARP_GETSPECBYCHOPNOANDMC(results[0].ITM_PREPARE, P_MCNO);

                if (resultNoCH.Count > 0)
                {
                    txtNoCH.Text = resultNoCH[0].NOCH.Value.ToString("#,##0.##");

                    WARP_GETCREELSETUPDETAIL(results[0].WARPHEADNO, resultNoCH[0].ITM_YARN);
                }

                cbItemCode.IsEnabled = false;
                txtWARPERHEADNO.IsEnabled = false;

                txtPALLETNO.SelectAll();
                txtPALLETNO.Focus();
            }
            else
            {
                cbItemCode.IsEnabled = true;
                txtWARPERHEADNO.IsEnabled = true;

                cbItemCode.SelectedValue = null;
                txtNoCH.Text = "";
                txtReedNo.Text = "";
                txtWaxApp.Text = "";

                rbMassProduction.IsChecked = true;
                rbTest.IsChecked = false;

                if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single)
                    this.gridWarping.SelectedItems.Clear();
                else
                    this.gridWarping.SelectedItem = null;

                gridWarping.ItemsSource = null;

                txtTotalCH.Text = "0";

                PALLETNO = string.Empty;

                txtWARPERHEADNO.Text = string.Empty;

                cbItemCode.Focus();
            }

            OldSide = P_SIDE;
        }

        #endregion

        #region WARP_GETCREELSETUPDETAIL

        private void WARP_GETCREELSETUPDETAIL(string P_WARPHEADNO, string ITM_YARN)
        {
            List<WARP_GETCREELSETUPDETAIL> results = WarpingDataService.Instance.WARP_GETCREELSETUPDETAIL(P_WARPHEADNO);

            if (results.Count > 0)
            {
                List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN> dataList = new List<LuckyTex.Models.WARP_PALLETLISTBYITMYARN>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.WARP_PALLETLISTBYITMYARN dataItemNew = new LuckyTex.Models.WARP_PALLETLISTBYITMYARN();

                    dataItemNew.IsSelect = true;

                    dataItemNew.ITM_YARN = results[i].ITM_YARN;
                    dataItemNew.RECEIVEDATE = results[i].RECEIVEDATE;
                    dataItemNew.PALLETNO = results[i].PALLETNO;
                   
                    dataItemNew.NoCH = results[i].NoCH;
                    dataItemNew.Use = results[i].Use;
                    dataItemNew.Reject = results[i].Reject;
                    dataItemNew.Remain = results[i].Remain;

                    dataList.Add(dataItemNew);
                    i++;
                }

                List<WARP_PALLETLISTBYITMYARN> resultsITM_YARN = WarpingDataService.Instance.WARP_PALLETLISTBYITMYARN(ITM_YARN, P_WARPHEADNO);

                if (resultsITM_YARN.Count > 0)
                {
                    int iITM_YARN = 0;

                    foreach (var row in resultsITM_YARN)
                    {
                        LuckyTex.Models.WARP_PALLETLISTBYITMYARN dataITM_YARN = new LuckyTex.Models.WARP_PALLETLISTBYITMYARN();

                        dataITM_YARN.IsSelect = false;

                        dataITM_YARN.ITM_YARN = resultsITM_YARN[iITM_YARN].ITM_YARN;
                        dataITM_YARN.RECEIVEDATE = resultsITM_YARN[iITM_YARN].RECEIVEDATE;
                        dataITM_YARN.PALLETNO = resultsITM_YARN[iITM_YARN].PALLETNO;

                        dataITM_YARN.NoCH = resultsITM_YARN[iITM_YARN].NoCH;
                        dataITM_YARN.Use = resultsITM_YARN[iITM_YARN].Use;
                        dataITM_YARN.Reject = resultsITM_YARN[iITM_YARN].Reject;
                        dataITM_YARN.Remain = resultsITM_YARN[iITM_YARN].Remain;

                        dataList.Add(dataITM_YARN);
                        iITM_YARN++;
                    }
                }

                this.gridWarping.ItemsSource = dataList;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarping.SelectedItems.Clear();
                else
                    this.gridWarping.SelectedItem = null;

                gridWarping.ItemsSource = null;

            }
        }

        #endregion

        #region SaveWarping

        private bool SaveWarping()
        {
            try
            {
                bool chkSave = true;

                if (gridWarping.Items.Count > 0)
                {
                    bool IsSelect = false;
                    string R_WARPERHEADNO = string.Empty;

                    string PALLETNO = string.Empty;

                    DateTime? P_RECEIVEDATE = DateTime.Now;
                    decimal? P_USED = null;
                    decimal? P_REJECTCH = null;
                    decimal? P_REMAINCH = null;

                    string P_ITMPREPARE = string.Empty;
                    string PRODUCTTYPEID = string.Empty;
                    string P_MCNO = string.Empty;
                    string P_SIDE = string.Empty;
                    decimal? P_ACTUALCH = null;
                    string P_WTYPE = string.Empty;
                    string P_OPERATOR = string.Empty;

                    string USEDCH = string.Empty;
                    string REJECTCH = string.Empty;
                    string WARPERHEADNO = string.Empty;
                    string REEDNO = string.Empty;

                    if (cbItemCode.SelectedValue != null)
                        P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                    if (!string.IsNullOrEmpty(txtMCNo.Text))
                        P_MCNO = txtMCNo.Text;

                    if (cbSide.SelectedValue != null)
                        P_SIDE = cbSide.SelectedValue.ToString();

                    if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false)
                    {
                        PRODUCTTYPEID = "1";
                    }
                    else
                    {
                        PRODUCTTYPEID = "2";
                    }

                    if (!string.IsNullOrEmpty(txtOperator.Text))
                        P_OPERATOR = txtOperator.Text;

                    if (!string.IsNullOrEmpty(txtWaxApp.Text))
                    {
                        if (txtWaxApp.Text == "Wax")
                            P_WTYPE = "Y";
                        else if (txtWaxApp.Text == "No Wax")
                            P_WTYPE = "N";
                    }


                    if (!string.IsNullOrEmpty(txtTotalCH.Text))
                    {
                        try
                        {
                            P_ACTUALCH = decimal.Parse(txtTotalCH.Text);
                        }
                        catch
                        {
                            P_ACTUALCH = 0;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtWARPERHEADNO.Text))
                    {
                        WARPERHEADNO = txtWARPERHEADNO.Text;
                    }

                    if (!string.IsNullOrEmpty(txtReedNo.Text))
                    {
                        REEDNO = txtReedNo.Text;
                    }

                    int i = 0;
                    bool chkSaveHead = false;

                    bool chkFirst = true;

                    foreach (var row in gridWarping.Items)
                    {
                        IsSelect = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).IsSelect;

                        PALLETNO = string.Empty;
                        P_RECEIVEDATE = null;
                        P_USED = null;
                        USEDCH = string.Empty;
                        P_REJECTCH = null;
                        REJECTCH = string.Empty;
                        P_REMAINCH = null;

                        if (!string.IsNullOrEmpty(((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).PALLETNO))
                        {
                            PALLETNO = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).PALLETNO;
                        }

                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).RECEIVEDATE != null)
                        {
                            P_RECEIVEDATE = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).RECEIVEDATE;
                        }

                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Use != null)
                        {
                            P_USED = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Use;
                            //USEDCH = P_USED.Value.ToString("#,##0.##");
                        }

                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Reject != null)
                        {
                            P_REJECTCH = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Reject;
                            //REJECTCH = P_REJECTCH.Value.ToString("#,##0.##");
                        }

                        if (((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Remain != null)
                        {
                            P_REMAINCH = ((LuckyTex.Models.WARP_PALLETLISTBYITMYARN)((gridWarping.Items)[i])).Remain;
                        }

                        if (IsSelect == true && PALLETNO != "")
                        {
                            // ย้ายจากเดิมมาทำก่อน WARP_INSERTSETTINGHEAD
                            if (WarpingDataService.Instance.WARP_UPDATEPALLET(P_RECEIVEDATE, PALLETNO, P_USED, P_REJECTCH, P_REMAINCH, WARPERHEADNO) == false)
                            {
                                chkSave = false;
                                "Can't Update Pallet".ShowMessageBox(true);
                                break;
                            }

                            if (chkFirst == true)
                            {
                                string R_RESULT = WarpingDataService.Instance.WARP_INSERTSETTINGHEAD(P_ITMPREPARE, PRODUCTTYPEID, P_MCNO, P_SIDE, P_ACTUALCH, P_WTYPE, P_OPERATOR, WARPERHEADNO, REEDNO);

                                if (string.IsNullOrEmpty(R_RESULT))
                                {
                                    chkSaveHead = true;

                                    chkFirst = false;
                                }
                                else
                                {
                                    chkSave = false;

                                    R_RESULT.ShowMessageBox(true);
                                    break;
                                }
                            }

                            if (chkSaveHead == true)
                            {
                                if (!string.IsNullOrEmpty(WARPERHEADNO))
                                {
                                    if (WarpingDataService.Instance.WARP_INSERTSETTINGDETAIL(WARPERHEADNO, PALLETNO, P_USED, P_REJECTCH) == false)
                                    {
                                        chkSave = false;
                                        "Can't Insert setting detail".ShowMessageBox(true);
                                        break;
                                    }

                                    // ของเดิมไม่ได้ใช้ ย้ายไปทำก่อน WARP_INSERTSETTINGHEAD
                                    //if (WarpingDataService.Instance.WARP_UPDATEPALLET(P_RECEIVEDATE, PALLETNO, P_USED, P_REJECTCH, P_REMAINCH, WARPERHEADNO) == false)
                                    //{
                                    //    chkSave = false;
                                    //    "Can't Update Pallet".ShowMessageBox(true);
                                    //    break;
                                    //}
                                }
                               
                            }
                        }

                        i++;
                    }
                }

                return chkSave;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user,string MCNo)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (mcNo != null)
            {
                mcNo = MCNo;
            }
        }

        #endregion

    }
}

