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
    /// Interaction logic for WarpingReceiveYarnPage.xaml
    /// </summary>
    public partial class WarpingReceiveYarnPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingReceiveYarnPage()
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
        string P_PALLETNO = string.Empty;
        int RowNo = 0;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItemGood();

            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;
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

        #region cmdAdd_Click

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            string P_ITMYARN = string.Empty;
            DateTime? P_RECEIVEDATE = DateTime.Now;
            string P_PALLETNO = string.Empty;
            string P_OPERATOR = string.Empty;

            if (txtPalletNo.Text != "")
                P_PALLETNO = txtPalletNo.Text;

            if (cbItemCode.SelectedValue != null)
                P_ITMYARN = cbItemCode.SelectedValue.ToString();

            if (dteReceiveDate.SelectedDate != null)
                P_RECEIVEDATE = dteReceiveDate.SelectedDate;

            if (txtOperator.Text != "")
                P_OPERATOR = txtOperator.Text;

            if (ChkPalletNo(txtPalletNo.Text) == true)
            {
                if (!string.IsNullOrEmpty(P_PALLETNO) && P_RECEIVEDATE != null && !string.IsNullOrEmpty(P_ITMYARN) && !string.IsNullOrEmpty(P_OPERATOR))
                    AddWarping();
            }
            else
            {
                "Pallet No had in Data".ShowMessageBox(false);

                txtPalletNo.Text = "";
                txtPalletNo.SelectAll();
                txtPalletNo.Focus();
            }
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (gridWarping.Items.Count > 0)
            {
                if (SaveWarping() == true)
                {
                    "Save complete".ShowMessageBox(false);

                    ClearControl();
                }
            }
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

        #region txtPalletNo_LostFocus

        private void txtPalletNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPalletNo.Text))
            {
                //if (ChkPalletNo(txtPalletNo.Text) == false)
                //    "Pallet No had in Data".ShowMessageBox(false);
            }
        }

        #endregion

        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtPalletNo.Text) && !string.IsNullOrEmpty(txtCH.Text) && !string.IsNullOrEmpty(txtWeight.Text))
                {
                    if (WARP_CHECKPALLET(txtPalletNo.Text) == true)
                    {
                        if (CalWeight() == true)
                        {
                            AddData();
                        }
                    }
                    else
                    {
                        txtPalletNo.Text = "";
                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();
                        e.Handled = true;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtPalletNo.Text))
                    {
                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();
                        e.Handled = true;
                    }
                    else if (string.IsNullOrEmpty(txtCH.Text))
                    {
                        txtCH.SelectAll();
                        txtCH.Focus();
                        e.Handled = true;
                    }
                    else if (string.IsNullOrEmpty(txtWeight.Text))
                    {
                        txtWeight.SelectAll();
                        txtWeight.Focus();
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (CalWeight() == true)
                {
                    if (!string.IsNullOrEmpty(txtPalletNo.Text) && !string.IsNullOrEmpty(txtCH.Text) && !string.IsNullOrEmpty(txtWeight.Text))
                    {
                        if (MessageBox.Show("Do you want to Add data", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            AddData();
                        }
                        else
                        {
                            txtWeight.SelectAll();
                            txtWeight.Focus();
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPalletNo.Text))
                        {
                            txtPalletNo.SelectAll();
                            txtPalletNo.Focus();
                            e.Handled = true;
                        }
                        else if (string.IsNullOrEmpty(txtCH.Text))
                        {
                            txtCH.SelectAll();
                            txtCH.Focus();
                            e.Handled = true;
                        }
                        else if (string.IsNullOrEmpty(txtWeight.Text))
                        {
                            txtWeight.SelectAll();
                            txtWeight.Focus();
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (CalWeight() == true)
                {
                    if (!string.IsNullOrEmpty(txtPalletNo.Text) && !string.IsNullOrEmpty(txtCH.Text) && !string.IsNullOrEmpty(txtWeight.Text))
                    {
                        if (MessageBox.Show("Do you want to Add data", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            AddData();
                        }
                        else
                        {
                            txtPalletNo.SelectAll();
                            txtPalletNo.Focus();
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtPalletNo.Text))
                        {
                            txtPalletNo.SelectAll();
                            txtPalletNo.Focus();
                            e.Handled = true;
                        }
                        else if (string.IsNullOrEmpty(txtCH.Text))
                        {
                            txtCH.SelectAll();
                            txtCH.Focus();
                            e.Handled = true;
                        }
                        else if (string.IsNullOrEmpty(txtWeight.Text))
                        {
                            txtWeight.SelectAll();
                            txtWeight.Focus();
                            e.Handled = true;
                        }
                    }
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
                            if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).RowNo != 0)
                            {
                                RowNo = ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).RowNo;
                            }
                            else
                            {
                                RowNo = 0;
                            }

                            if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).P_PALLETNO != null)
                            {
                                P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).P_PALLETNO;
                            }
                            else
                            {
                                P_PALLETNO = string.Empty;
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

        #region gridWarping_KeyUp

        private void gridWarping_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                Type type = this.GetType();
                try
                {
                    if (RowNo != 0 && P_PALLETNO != "")
                        Remove(RowNo, P_PALLETNO);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region Receive_Checked

        private void Receive_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo != 0
                        && ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO != "")
                    {
                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).Receive == true)
                        {
                            EditReceive(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, "N", "", true, false);
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

        #region Receive_Unchecked

        private void Receive_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo != 0
                       && ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO != "")
                    {
                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).Receive == false)
                        {
                            EditReceive(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, "Y",
                                ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).P_PALLETNO + ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).P_RECEIVEDATE.Value.ToString("ddMMyy"), false, true);
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

        #region Reject_Checked

        private void Reject_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo != 0
                       && ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO != "")
                    {
                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).Reject == true)
                        {
                            EditReceive(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, "Y",
                                ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).P_PALLETNO + ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.CurrentCell.Item)).P_RECEIVEDATE.Value.ToString("ddMMyy"), false, true);
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

        #region Reject_Unchecked

        private void Reject_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo != 0
                       && ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO != "")
                    {
                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).Reject == false)
                        {
                            EditReceive(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, "N", "", true, false);
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

        #region P_CH_LostFocus

        private void P_CH_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo != 0
                       && ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO != "")
                    {
                        decimal? ch = ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_CH;
                        decimal? weigth = ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_WEIGHT;

                        if (weigth != null && ch != null)
                        {
                            decimal? w = GridCalWeight(ch, weigth);

                            if (w != 0)
                                EditCH(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, ch, w);
                        }
                        else
                        {
                            EditCH(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, 0, 0);
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

        #region P_WEIGHT_LostFocus

        private void P_WEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridWarping.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo != 0
                       && ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO != "")
                    {
                        decimal? ch = ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_CH;
                        decimal? weigth = ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_WEIGHT;

                        if (weigth != null && ch != null)
                        {
                            decimal? w = GridCalWeight(ch, weigth);

                            if (w != 0)
                                EditCH(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, ch, w);
                        }
                        else
                        {
                            EditCH(((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).RowNo, ((LuckyTex.Models.WARP_RECEIVEPALLET)(gridWarping.SelectedItem)).P_PALLETNO, 0, 0);
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
                List<ITM_GETITEMYARNLIST> items = _session.GetItemCodeYarnData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_YARN";
                this.cbItemCode.SelectedValuePath = "ITM_YARN";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            cmdAdd.Visibility = System.Windows.Visibility.Collapsed;

            dteReceiveDate.SelectedDate = DateTime.Now;
            dteReceiveDate.Text = DateTime.Now.ToString("dd/MM/yy");
            cbItemCode.SelectedValue = null;
            txtPalletNo.Text = "";

            txtCH.Text = "52";
            txtWeight.Text = "520";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarping.SelectedItems.Clear();
            else
                this.gridWarping.SelectedItem = null;

            gridWarping.ItemsSource = null;

            P_PALLETNO = string.Empty;
            RowNo = 0;

            txtPalletNo.SelectAll();
            txtPalletNo.Focus();
        }

        #endregion

        #region AddData

        private void AddData()
        {
            try
            {
                string P_ITMYARN = string.Empty;
                DateTime? P_RECEIVEDATE = DateTime.Now;
                string P_PALLETNO = string.Empty;
                string P_OPERATOR = string.Empty;
                decimal? P_CH = null;
                decimal? P_WEIGHT = null;

                if (txtPalletNo.Text != "")
                    P_PALLETNO = txtPalletNo.Text;

                if (cbItemCode.SelectedValue != null)
                    P_ITMYARN = cbItemCode.SelectedValue.ToString();

                if (dteReceiveDate.SelectedDate != null)
                    P_RECEIVEDATE = dteReceiveDate.SelectedDate;

                if (txtOperator.Text != "")
                    P_OPERATOR = txtOperator.Text;

                if (txtCH.Text != "")
                    P_CH = decimal.Parse(txtCH.Text);

                if (txtWeight.Text != "")
                    P_WEIGHT = decimal.Parse(txtWeight.Text);

                if (!string.IsNullOrEmpty(P_ITMYARN))
                {
                    if (ChkPalletNo(txtPalletNo.Text) == true)
                    {
                        if (!string.IsNullOrEmpty(P_PALLETNO) && P_RECEIVEDATE != null && !string.IsNullOrEmpty(P_ITMYARN) && !string.IsNullOrEmpty(P_OPERATOR) && P_CH != null && P_WEIGHT != null)
                        {
                            AddWarping();
                        }
                    }
                    else
                    {
                        "Pallet No had in Data".ShowMessageBox(false);

                        txtPalletNo.Text = "";
                        txtCH.Text = "52";
                        txtWeight.Text = "520";

                        txtPalletNo.SelectAll();
                        txtPalletNo.Focus();
                    }
                }
                else
                {
                    "Item Yarn isn't Null".ShowMessageBox(false);
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region CalWeight

        private bool CalWeight()
        {
            bool chkWeight = true;

            try
            {
                if (!string.IsNullOrEmpty(txtCH.Text) && !string.IsNullOrEmpty(txtWeight.Text))
                {
                    decimal? min = null;
                    decimal? max = null;
                    decimal? ch = null;
                    decimal? weight = null;

                    try
                    {
                        ch = decimal.Parse(txtCH.Text);
                    }
                    catch
                    {
                        ch = 0;
                    }

                    try
                    {
                        weight = decimal.Parse(txtWeight.Text);
                    }
                    catch
                    {
                        weight = 0;
                    }

                    min = ch * 1;
                    max = ch * 10;

                    if (weight >= min)
                    {
                        if (weight > max)
                        {
                            txtWeight.Text = min.Value.ToString("#,##0.##");
                            chkWeight = false;
                        }
                        else if (weight <= max)
                        {
                            chkWeight = true;

                            txtPalletNo.SelectAll();
                            txtPalletNo.Focus();
                        }
                    }
                    else
                    {
                        txtWeight.Text = min.Value.ToString("#,##0.##");
                        chkWeight = false;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtCH.Text))
                    {
                        txtCH.SelectAll();
                        txtCH.Focus();
                        chkWeight = false;
                    }
                    else if (string.IsNullOrEmpty(txtWeight.Text))
                    {
                        txtWeight.SelectAll();
                        txtWeight.Focus();
                        chkWeight = false;
                    }
                }

                return chkWeight;
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);

                return false;
            }
        }

        #endregion

        #region GridCalWeight

        private decimal? GridCalWeight(decimal? ch, decimal? weight)
        {
            decimal? chkWeight = 0;

            try
            {
                if (ch != null && weight != null)
                {
                    decimal? min = null;
                    decimal? max = null;

                    min = ch * 1;
                    max = ch * 10;

                    if (weight >= min)
                    {
                        if (weight > max)
                        {
                            chkWeight = min;
                        }
                        else if (weight <= max)
                        {
                            chkWeight = weight;
                        }
                    }
                    else
                    {
                        chkWeight = min;
                    }
                }

                return chkWeight;
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);

                return 0;
            }
        }

        #endregion

        #region AddWarping

        private void AddWarping()
        {
            try
            {
                string P_ITMYARN = string.Empty;
                DateTime? P_RECEIVEDATE = DateTime.Now;
                string P_PALLETNO = string.Empty;
                string P_OPERATOR = string.Empty;
                decimal? P_CH = null;
                decimal? P_WEIGHT = null;

                if (txtPalletNo.Text != "")
                    P_PALLETNO = txtPalletNo.Text;

                if (cbItemCode.SelectedValue != null)
                    P_ITMYARN = cbItemCode.SelectedValue.ToString();

                if (dteReceiveDate.SelectedDate != null)
                    P_RECEIVEDATE = dteReceiveDate.SelectedDate;

                if (txtOperator.Text != "")
                    P_OPERATOR = txtOperator.Text;

                if (txtCH.Text != "")
                    P_CH = decimal.Parse(txtCH.Text);

                if (txtWeight.Text != "")
                    P_WEIGHT = decimal.Parse(txtWeight.Text);

                #region AddWarpingDetail

                if (AddWarpingDetail(P_ITMYARN, P_RECEIVEDATE, P_PALLETNO, P_OPERATOR, P_CH, P_WEIGHT) == false)
                {
                    "Can't Insert Pallet No".ShowMessageBox(false);
                }
                else
                {
                    //dteReceiveDate.SelectedDate = DateTime.Now;
                    //dteReceiveDate.Text = DateTime.Now.ToString("dd/MM/yy");
                    
                    txtPalletNo.Text = "";
                    txtCH.Text = "52";
                    txtWeight.Text = "520";

                    txtPalletNo.SelectAll();
                    txtPalletNo.Focus();
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region AddWarpingDetail

        private bool AddWarpingDetail(string P_ITMYARN, DateTime? P_RECEIVEDATE, string P_PALLETNO, string P_OPERATOR
            , decimal? P_CH, decimal? P_WEIGHT)
        {
            try
            {
                List<LuckyTex.Models.WARP_RECEIVEPALLET> dataList = new List<LuckyTex.Models.WARP_RECEIVEPALLET>();
                int o = 0;
                foreach (var row in gridWarping.Items)
                {
                    LuckyTex.Models.WARP_RECEIVEPALLET dataItem = new LuckyTex.Models.WARP_RECEIVEPALLET();

                    dataItem.RowNo = o + 1;
                    dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                    dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                    dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;

                    dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;

                    dataItem.P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_WEIGHT;
                    dataItem.P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_CH;
                    dataItem.P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_VERIFY;
                    dataItem.P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_REJECTID;
                    dataItem.Receive = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Receive;
                    dataItem.Reject = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Reject;

                    o++;

                    dataList.Add(dataItem);
                }

                LuckyTex.Models.WARP_RECEIVEPALLET dataItemNew = new LuckyTex.Models.WARP_RECEIVEPALLET();

                dataItemNew.RowNo = gridWarping.Items.Count + 1;

                dataItemNew.P_PALLETNO = P_PALLETNO;
                dataItemNew.P_ITMYARN = P_ITMYARN;
                dataItemNew.P_RECEIVEDATE = P_RECEIVEDATE;

                dataItemNew.P_CH = P_CH;
                dataItemNew.P_WEIGHT = P_WEIGHT;
                //dataItemNew.P_WEIGHT = 520;
                
                dataItemNew.P_VERIFY = "Y";
                dataItemNew.P_REJECTID = "";
                dataItemNew.Receive = true;
                dataItemNew.Reject = false;
                dataItemNew.P_OPERATOR = P_OPERATOR;

                dataList.Add(dataItemNew);

                this.gridWarping.ItemsSource = dataList;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Remove
        private void Remove(int RowNo, string P_PALLETNO)
        {
            if (gridWarping.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.WARP_RECEIVEPALLET> dataList = new List<LuckyTex.Models.WARP_RECEIVEPALLET>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridWarping.Items)
                        {
                            LuckyTex.Models.WARP_RECEIVEPALLET dataItem = new LuckyTex.Models.WARP_RECEIVEPALLET();

                            if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo == RowNo
                                && ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO == P_PALLETNO)
                            {

                                dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                                dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                                dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;
                                dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;
                                dataItem.P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_CH;
                                dataItem.P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_WEIGHT;
                                dataItem.P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_VERIFY;
                                dataItem.P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_REJECTID;
                                dataItem.Receive = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Receive;
                                dataItem.Reject = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Reject;

                                dataList.Remove(dataItem);

                            }
                            else
                            {
                                dataItem.RowNo = i + 1;

                                dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                                dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                                dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;
                                dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;
                                dataItem.P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_CH;
                                dataItem.P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_WEIGHT;
                                dataItem.P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_VERIFY;
                                dataItem.P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_REJECTID;
                                dataItem.Receive = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Receive;
                                dataItem.Reject = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Reject;

                                dataList.Add(dataItem);
                                i++;
                            }
                            o++;
                        }

                        this.gridWarping.ItemsSource = dataList;

                        RowNo = 0;
                        P_PALLETNO = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region EditReceive
        private void EditReceive(int RowNo, string P_PALLETNO, string P_VERIFY, string P_REJECTID, bool Receive, bool Reject)
        {
            if (gridWarping.SelectedItems.Count > 0)
            {
                try
                {
                    List<LuckyTex.Models.WARP_RECEIVEPALLET> dataList = new List<LuckyTex.Models.WARP_RECEIVEPALLET>();

                    int o = 0;
                    foreach (var row in gridWarping.Items)
                    {
                        LuckyTex.Models.WARP_RECEIVEPALLET dataItem = new LuckyTex.Models.WARP_RECEIVEPALLET();

                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo == RowNo
                            && ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO == P_PALLETNO)
                        {
                            dataItem.RowNo = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo;
                            dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                            dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                            dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;
                            dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;
                            dataItem.P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_CH;
                            dataItem.P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_WEIGHT;
                            dataItem.P_VERIFY = P_VERIFY;
                            dataItem.P_REJECTID = P_REJECTID;
                            dataItem.Receive = Receive;
                            dataItem.Reject = Reject;

                            dataList.Add(dataItem);
                        }
                        else
                        {
                            dataItem.RowNo = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo;
                            dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                            dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                            dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;
                            dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;
                            dataItem.P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_CH;
                            dataItem.P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_WEIGHT;
                            dataItem.P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_VERIFY;
                            dataItem.P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_REJECTID;
                            dataItem.Receive = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Receive;
                            dataItem.Reject = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Reject;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridWarping.ItemsSource = dataList;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region EditCH
        private void EditCH(int RowNo, string P_PALLETNO, decimal? P_CH, decimal? P_WEIGHT)
        {
            if (gridWarping.SelectedItems.Count > 0)
            {
                try
                {
                    List<LuckyTex.Models.WARP_RECEIVEPALLET> dataList = new List<LuckyTex.Models.WARP_RECEIVEPALLET>();

                    int o = 0;
                    foreach (var row in gridWarping.Items)
                    {
                        LuckyTex.Models.WARP_RECEIVEPALLET dataItem = new LuckyTex.Models.WARP_RECEIVEPALLET();

                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo == RowNo
                            && ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO == P_PALLETNO)
                        {
                            dataItem.RowNo = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo;
                            dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                            dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                            dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;
                            dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;
                            dataItem.P_CH = P_CH;
                            dataItem.P_WEIGHT = P_WEIGHT;
                            dataItem.P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_VERIFY;
                            dataItem.P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_REJECTID;
                            dataItem.Receive = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Receive;
                            dataItem.Reject = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Reject;

                            dataList.Add(dataItem);
                        }
                        else
                        {
                            dataItem.RowNo = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).RowNo;
                            dataItem.P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO;
                            dataItem.P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_RECEIVEDATE;
                            dataItem.P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_ITMYARN;
                            dataItem.P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_OPERATOR;
                            dataItem.P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_CH;
                            dataItem.P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_WEIGHT;
                            dataItem.P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_VERIFY;
                            dataItem.P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_REJECTID;
                            dataItem.Receive = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Receive;
                            dataItem.Reject = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).Reject;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridWarping.ItemsSource = dataList;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        private bool WARP_CHECKPALLET(string P_PALLETNO)
        {
            try
            {
                bool chkPN = true;

                List<WARP_CHECKPALLET> dbResults = WarpingDataService.Instance.WARP_CHECKPALLET(P_PALLETNO);

                if (dbResults != null && dbResults.Count > 0)
                {
                    "Cannot use this Pallet No,  Pallet No already Received".ShowMessageBox();

                    chkPN = false;
                }

                return chkPN;
            }
            catch
            {
                return false;
            }
        }

        #region ChkPalletNo

        private bool ChkPalletNo(string P_PALLETNO)
        {
            try
            {
                bool chkPN = true;

                int o = 0;
                foreach (var row in gridWarping.Items)
                {
                    if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[o])).P_PALLETNO == P_PALLETNO)
                    {
                        chkPN = false;
                        break;
                    }

                    o++;
                }

                return chkPN;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region SaveWarping

        private bool SaveWarping()
        {
            try
            {
                if (gridWarping.Items.Count > 0)
                {
                    string P_ITMYARN = string.Empty;
                    DateTime? P_RECEIVEDATE = DateTime.Now;
                    string P_PALLETNO = string.Empty;
                    decimal? P_CH = null;
                    decimal? P_WEIGHT = null;
                    string P_VERIFY = string.Empty;
                    string P_REJECTID = string.Empty;
                    string P_OPERATOR = string.Empty;

                    int i = 0;
                    foreach (var row in gridWarping.Items)
                    {
                        P_ITMYARN = string.Empty;
                        P_RECEIVEDATE = null;
                        P_PALLETNO = string.Empty;
                        P_CH = null;
                        P_WEIGHT = null;
                        P_VERIFY = string.Empty;
                        P_REJECTID = string.Empty;
                        P_OPERATOR = string.Empty;

                        if (!string.IsNullOrEmpty(((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_ITMYARN))
                        {
                            P_ITMYARN = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_ITMYARN;
                        }

                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_RECEIVEDATE != null)
                        {
                            P_RECEIVEDATE = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_RECEIVEDATE;
                        }
                        if (!string.IsNullOrEmpty(((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_PALLETNO))
                        {
                            P_PALLETNO = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_PALLETNO;
                        }

                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_CH != null)
                        {
                            P_CH = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_CH;
                        }

                        if (((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_WEIGHT != null)
                        {
                            P_WEIGHT = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_WEIGHT;
                        }

                        if (!string.IsNullOrEmpty(((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_VERIFY))
                        {
                            P_VERIFY = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_VERIFY;
                        }
                        if (!string.IsNullOrEmpty(((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_REJECTID))
                        {
                            P_REJECTID = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_REJECTID;
                        }
                        if (!string.IsNullOrEmpty(((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_OPERATOR))
                        {
                            P_OPERATOR = ((LuckyTex.Models.WARP_RECEIVEPALLET)((gridWarping.Items)[i])).P_OPERATOR;
                        }

                        if (P_ITMYARN != "" && P_PALLETNO != "")
                        {
                            WarpingDataService.Instance.WARP_RECEIVEPALLET(P_ITMYARN, P_RECEIVEDATE, P_PALLETNO, P_WEIGHT, P_CH , P_VERIFY, P_REJECTID, P_OPERATOR);
                        }

                        i++;
                    }
                }

                return true;
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

