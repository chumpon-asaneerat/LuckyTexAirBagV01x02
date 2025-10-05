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

using System.Configuration;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ReceiveReturnMaterialPage.xaml
    /// </summary>
    public partial class ReceiveReturnMaterialPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveReturnMaterialPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadReturnBy();
            LoadYarnType();
            LoadItemYarn();
            LoadGrade();
        }

        #endregion

        #region Internal Variables

        private List<ItemYarnItem> instList = null;

        string strConAS400 = string.Empty;
        bool chkStatusAS400 = true;

        string opera = string.Empty;

        int? RowNo = null;

        string P_PALLETNO = string.Empty;
        string P_ITM400 = string.Empty;
        string P_TRACENO = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ConfigManager.Instance.LoadAS400Configs();
            //strConAS400 = ConfigManager.Instance.AS400Config;

            ClearControl();

            //chkStatusAS400 = chkConAS400();

            if (opera != "")
                txtOperator.Text = opera;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region TextBox

        #region Common_PreviewKeyDown

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtTraceNo_LostFocus
        private void txtTraceNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTraceNo.Text))
            {
                G3_SEARCHBYTRACENO(txtTraceNo.Text);
            }
        }
        #endregion

        #region txtTraceNo_KeyDown
        private void txtTraceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtTraceNo.Text))
                {
                    txtWeight.Focus();
                    txtWeight.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtWeight_KeyDown
        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    txtCH.Focus();
                    txtCH.SelectAll();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtCH_KeyDown
        private void txtCH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtCH.Text))
                {
                    cmdReceive.Focus();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region txtLotNo_KeyDown
        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtLotNo.Text))
                {
                    cmdReceive.Focus();
                    e.Handled = true;
                }
            }
        }
        #endregion

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdReceive_Click
        private void cmdReceive_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTraceNo.Text) && !string.IsNullOrEmpty(txtWeight.Text) && !string.IsNullOrEmpty(txtCH.Text))
            {
                if (cbItemYarn.SelectedValue != null && cbYarnType.SelectedValue != null)
                {
                    if (CheckTRACENO(txtTraceNo.Text) == true)
                        Receive();
                    else
                    {
                        "Trace No had in DataGrid".ShowMessageBox();
                        ClearControl();
                    }
                }
                else
                {
                    if (cbItemYarn.SelectedValue == null)
                    {
                        "Item Yarn is not null".ShowMessageBox();
                    }
                    else if (cbYarnType.SelectedValue == null)
                    {
                        "Yarn Type is not null".ShowMessageBox();
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtTraceNo.Text))
                {
                    "Trace No is not null".ShowMessageBox();
                }
                else if (string.IsNullOrEmpty(txtWeight.Text))
                {
                    "Weight is not null".ShowMessageBox();
                }
                else if (string.IsNullOrEmpty(txtCH.Text))
                {
                    "CH is not null".ShowMessageBox();
                }
            }
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPalletDetail.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPalletDetail.SelectedItems.Clear();
            else
                this.gridPalletDetail.SelectedItem = null;

            gridPalletDetail.ItemsSource = null;

            txtTotalPallet.Text = "0";
            txtSumWeight.Text = "0";
            txtSumCH.Text = "0";

            txtTraceNo.SelectAll();
            txtTraceNo.Focus();
        }
        #endregion

        #region cmdReturnAS400_Click
        private void cmdReturnAS400_Click(object sender, RoutedEventArgs e)
        {
            ExportExcel();
            //ReturnAS400();
        }
        #endregion

        #endregion

        #region gridPalletDetail_SelectedCellsChanged

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

        private void gridPalletDetail_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridPalletDetail.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridPalletDetail);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.G3_INSERTRETURNYARN)(gridPalletDetail.CurrentCell.Item)).RowNo != 0)
                            {
                                RowNo = ((LuckyTex.Models.G3_INSERTRETURNYARN)(gridPalletDetail.CurrentCell.Item)).RowNo;
                            }
                            else
                            {
                                RowNo = 0;
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

        private void gridPalletDetail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                Type type = this.GetType();
                try
                {
                    if (RowNo != 0)
                        Remove(RowNo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region private Methods

        #region LoadReturnBy

        private void LoadReturnBy()
        {
            if (cbReturnBy.ItemsSource == null)
            {
                string[] str = new string[] { "Warping", "Weaving" };

                cbReturnBy.ItemsSource = str;
                cbReturnBy.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadYarnType

        private void LoadYarnType()
        {
            try
            {
                if (cbYarnType.ItemsSource == null)
                {
                    string[] str = new string[] { "Warp", "Weft" };

                    cbYarnType.ItemsSource = str;
                    cbYarnType.SelectedIndex = 0;
                }
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
                instList = G3DataService.Instance.GetItemYarnData();

                this.cbItemYarn.ItemsSource = instList;
                this.cbItemYarn.DisplayMemberPath = "ITM_YARN";
                this.cbItemYarn.SelectedValuePath = "ITM_YARN";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadGrade

        private void LoadGrade()
        {
            if (cbGrade.ItemsSource == null)
            {
                string[] str = new string[] { "A", "C" };

                cbGrade.ItemsSource = str;
                cbGrade.SelectedIndex = 0;
            }
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtTraceNo.Text = string.Empty;

            dteReceiveDate.SelectedDate = DateTime.Now;
            dteReceiveDate.Text = DateTime.Now.ToString("dd/MM/yy");
            cbReturnBy.SelectedIndex = 0;
            cbGrade.SelectedIndex = 0;

            txtWeight.Text = string.Empty;
            txtCH.Text = string.Empty;
            cbItemYarn.SelectedValue = null;
            cbYarnType.SelectedIndex = 0;
            txtLotNo.Text = string.Empty;

            cbItemYarn.IsEnabled = false;
            cbYarnType.IsEnabled = false;
            cbGrade.IsEnabled = false;

            P_PALLETNO = string.Empty;
            P_ITM400 = string.Empty;
            P_TRACENO = string.Empty;

            txtTraceNo.SelectAll();
            txtTraceNo.Focus();
        }

        #endregion

        #region G3_SEARCHBYTRACENO

        private void G3_SEARCHBYTRACENO(string _traceNo)
        {
            if (_traceNo != "")
            {
                G3_SEARCHBYPALLETNOSearchData lots = new G3_SEARCHBYPALLETNOSearchData();

                lots = G3DataService.Instance.G3_SearchByTRACENO(_traceNo);

                if (null != lots && !string.IsNullOrEmpty(lots.TRACENO))
                {
                    if (!string.IsNullOrEmpty(lots.LOTNO))
                    {
                        txtLotNo.Text = lots.LOTNO;
                    }
                    else
                    {
                        DateTime dt = DateTime.Now;
                        txtLotNo.Text = "L" + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy") + dt.ToString("HH") + dt.ToString("mm");
                    }

                    cbItemYarn.SelectedValue = lots.ITM_YARN;
                    cbYarnType.SelectedValue = lots.YARNTYPE;
                    cbGrade.SelectedIndex = 0;


                    P_PALLETNO = lots.PALLETNO;
                    P_ITM400 = lots.ITM400;

                    cbItemYarn.IsEnabled = false;
                    cbYarnType.IsEnabled = false;

                    cbGrade.IsEnabled = true;
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    txtLotNo.Text = "L" + dt.ToString("dd") + dt.ToString("MM") + dt.ToString("yyyy") + dt.ToString("HH") + dt.ToString("mm");

                    cbItemYarn.SelectedValue = null;
                    cbYarnType.SelectedIndex = 0;
                    cbGrade.SelectedIndex = 0;

                    cbItemYarn.IsEnabled = true;
                    cbYarnType.IsEnabled = true;
                    cbGrade.IsEnabled = true;

                    P_PALLETNO = string.Empty;
                    P_ITM400 = string.Empty;
                }
            }
        }

        #endregion

        #region Receive
        private void Receive()
        {
            try
            {
                List<LuckyTex.Models.G3_INSERTRETURNYARN> dataList = new List<LuckyTex.Models.G3_INSERTRETURNYARN>();
                int o = 0;
                foreach (var row in gridPalletDetail.Items)
                {
                    LuckyTex.Models.G3_INSERTRETURNYARN dataItem = new LuckyTex.Models.G3_INSERTRETURNYARN();

                    dataItem.RowNo = o + 1;

                    dataItem.TRACENO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).TRACENO;
                    dataItem.LOTNO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).LOTNO;
                    dataItem.ITEMYARN = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITEMYARN;
                    dataItem.PALLETNO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).PALLETNO;
                    dataItem.ITM400 = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITM400;
                    dataItem.YARNTYPE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).YARNTYPE;
                    dataItem.WEIGHT = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT;
                    dataItem.CH = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH;
                    dataItem.NEWTRACENO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).NEWTRACENO;
                    dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RECEIVEDATE;
                    dataItem.RETURNBY = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RETURNBY;
                    dataItem.OPERATOR = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).OPERATOR;

                    dataItem.GRADE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).GRADE;

                    o++;

                    dataList.Add(dataItem);
                }

                LuckyTex.Models.G3_INSERTRETURNYARN dataItemNew = new LuckyTex.Models.G3_INSERTRETURNYARN();

                dataItemNew.RowNo = gridPalletDetail.Items.Count + 1;

                if (!string.IsNullOrEmpty(txtTraceNo.Text))
                {
                    dataItemNew.TRACENO = txtTraceNo.Text;

                    if (dataItemNew.TRACENO.Length < 10)
                    {
                        dataItemNew.NEWTRACENO = "R" + dataItemNew.TRACENO;

                        if (dataItemNew.NEWTRACENO.Length != 10)
                        {
                            dataItemNew.NEWTRACENO = "R" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("mm") + dataItemNew.RowNo.ToString();
                        }
                    }
                    else
                    {
                        try
                        {
                            dataItemNew.NEWTRACENO = "R" + dataItemNew.TRACENO.Substring(dataItemNew.TRACENO.Length - (dataItemNew.TRACENO.Length - 1), (dataItemNew.TRACENO.Length - 1));

                            if (dataItemNew.NEWTRACENO.Length != 10)
                            {
                                dataItemNew.NEWTRACENO = "R" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("mm") + dataItemNew.RowNo.ToString();
                            }
                        }
                        catch
                        {
                            dataItemNew.NEWTRACENO = "R" + DateTime.Now.ToString("dd") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("yy") + DateTime.Now.ToString("mm") + dataItemNew.RowNo.ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(txtLotNo.Text))
                    dataItemNew.LOTNO = txtLotNo.Text;

                if (cbItemYarn.SelectedValue != null)
                    dataItemNew.ITEMYARN = cbItemYarn.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(P_PALLETNO))
                    dataItemNew.PALLETNO = P_PALLETNO;

                if (!string.IsNullOrEmpty(P_ITM400))
                    dataItemNew.ITM400 = P_ITM400;
                else
                {
                    if (!string.IsNullOrEmpty(dataItemNew.ITEMYARN))
                    {
                        dataItemNew.ITM400 = G3DataService.Instance.ITM_GETITEMYARN400(dataItemNew.ITEMYARN);
                    }
                }

                if (cbYarnType.SelectedValue != null)
                    dataItemNew.YARNTYPE = cbYarnType.SelectedValue.ToString();

                try
                {
                    if (!string.IsNullOrEmpty(txtWeight.Text))
                        dataItemNew.WEIGHT = decimal.Parse(txtWeight.Text);
                }
                catch
                {
                    dataItemNew.WEIGHT = 0;
                }

                try
                {
                    if (!string.IsNullOrEmpty(txtCH.Text))
                        dataItemNew.CH = decimal.Parse(txtCH.Text);
                }
                catch
                {
                    dataItemNew.CH = 0;
                }

                if (dteReceiveDate.SelectedDate != null)
                    dataItemNew.RECEIVEDATE = dteReceiveDate.SelectedDate;

                if (cbReturnBy.SelectedValue != null)
                    dataItemNew.RETURNBY = cbReturnBy.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    dataItemNew.OPERATOR = txtOperator.Text;

                if (cbGrade.SelectedValue != null)
                    dataItemNew.GRADE = cbGrade.SelectedValue.ToString();

                dataList.Add(dataItemNew);


                this.gridPalletDetail.ItemsSource = dataList;

                CalTotal();
                ClearControl();
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region Remove
        private void Remove(int? RowNo)
        {
            if (gridPalletDetail.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.G3_INSERTRETURNYARN> dataList = new List<LuckyTex.Models.G3_INSERTRETURNYARN>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridPalletDetail.Items)
                        {
                            LuckyTex.Models.G3_INSERTRETURNYARN dataItem = new LuckyTex.Models.G3_INSERTRETURNYARN();

                            if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RowNo == RowNo)
                            {
                                dataItem.TRACENO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).TRACENO;
                                dataItem.LOTNO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).LOTNO;
                                dataItem.ITEMYARN = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITEMYARN;
                                dataItem.PALLETNO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).PALLETNO;
                                dataItem.ITM400 = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITM400;
                                dataItem.YARNTYPE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).YARNTYPE;
                                dataItem.WEIGHT = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT;
                                dataItem.CH = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH;
                                dataItem.NEWTRACENO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).NEWTRACENO;
                                dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RECEIVEDATE;
                                dataItem.RETURNBY = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RETURNBY;
                                dataItem.OPERATOR = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).OPERATOR;
                                dataItem.GRADE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).GRADE;

                                dataList.Remove(dataItem);
                            }
                            else
                            {
                                dataItem.RowNo = i + 1;

                                dataItem.TRACENO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).TRACENO;
                                dataItem.LOTNO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).LOTNO;
                                dataItem.ITEMYARN = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITEMYARN;
                                dataItem.PALLETNO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).PALLETNO;
                                dataItem.ITM400 = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITM400;
                                dataItem.YARNTYPE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).YARNTYPE;
                                dataItem.WEIGHT = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT;
                                dataItem.CH = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH;
                                dataItem.NEWTRACENO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).NEWTRACENO;
                                dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RECEIVEDATE;
                                dataItem.RETURNBY = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RETURNBY;
                                dataItem.OPERATOR = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).OPERATOR;
                                dataItem.GRADE = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).GRADE;

                                dataList.Add(dataItem);
                                i++;
                            }
                            o++;
                        }

                        this.gridPalletDetail.ItemsSource = dataList;

                        RowNo = 0;

                        CalTotal();
                        ClearControl();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region CheckTRACENO
        private bool CheckTRACENO(string TRACENO)
        {
            try
            {
                int o = 0;
                bool chkStatus = true;

                foreach (var row in gridPalletDetail.Items)
                {
                    if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).TRACENO != null)
                    {
                        if (TRACENO == ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).TRACENO)
                        {
                            chkStatus = false;
                            break;
                        }
                    }
                    o++;
                }

                return chkStatus;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region CalTotal

        private void CalTotal()
        {
            try
            {
                int o = 0;
                decimal pallet = 0;
                decimal weight = 0;
                decimal conech = 0;

                foreach (var row in gridPalletDetail.Items)
                {
                    if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).PALLETNO != null)
                    {
                        pallet++;
                    }

                    if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT != null)
                    {
                        weight += ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT.Value;
                    }

                    if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH != null)
                    {
                        conech += ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH.Value;
                    }

                    o++;
                }

                if (pallet > 0)
                {
                    txtTotalPallet.Text = pallet.ToString("#,##0");
                    txtSumWeight.Text = weight.ToString("#,##0.##");
                    txtSumCH.Text = conech.ToString("#,##0.##");
                }
                else
                {
                    txtTotalPallet.Text = "0";
                    txtSumWeight.Text = "0";
                    txtSumCH.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region ReturnAS400
        private void ReturnAS400()
        {
            try
            {
                int o = 0;
                bool chkSave = true;
                
                foreach (var row in gridPalletDetail.Items)
                {
                    if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RowNo != 0)
                    {


                        if (G3DataService.Instance.G3_INSERTRETURNYARN(((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).TRACENO
                            ,((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).NEWTRACENO
                            , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH
                            ,((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT
                            ,((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RECEIVEDATE
                            ,((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).OPERATOR
                            ,((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITEMYARN
                            ,((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).YARNTYPE
                            , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RETURNBY) == true)
                        {
                            if (chkStatusAS400 == true)
                            {
                                if (SendAS400(((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).RECEIVEDATE
                                    , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).PALLETNO
                                    , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).ITM400
                                    , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).LOTNO
                                    , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).NEWTRACENO
                                    , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).WEIGHT
                                    , ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[o])).CH) == false)
                                {
                                    chkSave = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            chkSave = false;
                            break;
                        }
                    }

                    o++;
                }

                if (chkSave == true)
                {
                    "Data Return to AS400 Complete".ShowMessageBox();

                    ClearControl();
                    // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                    if (this.gridPalletDetail.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridPalletDetail.SelectedItems.Clear();
                    else
                        this.gridPalletDetail.SelectedItem = null;

                    gridPalletDetail.ItemsSource = null;

                    txtTotalPallet.Text = "0";
                    txtSumWeight.Text = "0";
                    txtSumCH.Text = "0";

                    txtTraceNo.SelectAll();
                    txtTraceNo.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region ExportExcel
        private void ExportExcel()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                FileInfo fileInfo = new FileInfo(path + @"\ReturnAS400.xls");
                string excel = string.Empty;

                if (fileInfo.Extension == ".xlsx")
                    excel = "Excel (*.xlsx)|*.xlsx";
                else if (fileInfo.Extension == ".xlsm")
                    excel = "Excel (*.xlsm)|*.xlsm";
                else
                    excel = "Excel (*.xls)|*.xls";

                saveFileDialog1.Filter = excel;
                saveFileDialog1.FilterIndex = 1;

                Nullable<bool> result = saveFileDialog1.ShowDialog();

                if (result == true)
                {
                    string newFileName = saveFileDialog1.FileName;

                    try
                    {
                        if (File.Exists(newFileName))
                        {
                            FileInfo fileCheck = new FileInfo(newFileName);
                            fileCheck.Delete();
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Err();
                    }

                    if (CreateSheet(newFileName)== true)
                    {
                        MessageBox.Show("Excel : " + newFileName, "Save Complete", MessageBoxButton.OK);
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }
        #endregion

        #region ExportToExcel
        public System.Data.DataTable ExportToExcel()
        {
            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("NO", typeof(int));
            table.Columns.Add("TruckNo", typeof(string));
            table.Columns.Add("Desc", typeof(string));
            table.Columns.Add("TraceNo", typeof(string));
            table.Columns.Add("Cone", typeof(decimal));
            table.Columns.Add("Qty", typeof(decimal));
            table.Columns.Add("Lot", typeof(string));
            table.Columns.Add("Item", typeof(string));
            table.Columns.Add("RecDt", typeof(DateTime));
            table.Columns.Add("Um", typeof(string));
            table.Columns.Add("Grade", typeof(string));
            table.Columns.Add("Trans", typeof(string));

            long? NO = null;
            string TruckNo = string.Empty;
            string Desc = string.Empty;
            string TraceNo = string.Empty;
            decimal? Cone = null;
            decimal? Qty = null;
            string Lot = string.Empty;
            string Item = string.Empty;
            DateTime? RecDt = null;
            string Um = "KG";
            string Grade = string.Empty;
            string Trans = "GR";

             int i = 0;
             foreach (var row in gridPalletDetail.Items)
             {
                 if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).RowNo != 0)
                 {
                     NO = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).RowNo;

                     TraceNo = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).NEWTRACENO;

                     if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).CH != null)
                         Cone = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).CH;

                     if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).WEIGHT != null)
                         Qty = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).WEIGHT;

                     Lot = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).LOTNO;

                     Item = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).ITM400;

                     if (((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).RECEIVEDATE != null)
                         RecDt = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).RECEIVEDATE;

                     Grade = ((LuckyTex.Models.G3_INSERTRETURNYARN)((gridPalletDetail.Items)[i])).GRADE;

                     table.Rows.Add(NO, TruckNo, Desc, TraceNo, Cone, Qty, Lot, Item, RecDt, Um, Grade, Trans);
                 }

                 i++;
             }

            return table;
        }
        #endregion

        #region CreateSheet
        private bool CreateSheet(string newFileName)
        {

            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook worKbooK;
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            Microsoft.Office.Interop.Excel.Range celLrangE;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                worKbooK = excel.Workbooks.Add(Type.Missing);

                worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                worKsheeT.Name = "RETURNYARN";

                //worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[1, 1]].Merge();
                //worKsheeT.Cells[1, 1] = "NO";
                //worKsheeT.Cells[1, 2] = "TruckNo";
                //worKsheeT.Cells.Font.Size = 15;

                System.Data.DataTable export = ExportToExcel();
                int rowcount = 1;

                foreach (DataRow datarow in export.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= export.Columns.Count; i++)
                    {
                        if (rowcount == 2)
                        {
                            worKsheeT.Cells[1, i] = export.Columns[i - 1].ColumnName;
                            worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;
                        }

                        worKsheeT.Cells[rowcount, i] = datarow[i - 1].ToString();

                        if (rowcount > 2)
                        {
                            if (i == export.Columns.Count)
                            {
                                if (rowcount % 1 == 0)
                                {
                                    celLrangE = worKsheeT.Range[worKsheeT.Cells[rowcount, 1], worKsheeT.Cells[rowcount, export.Columns.Count]];
                                }

                            }
                        }
                    }
                }

                celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[rowcount, export.Columns.Count]];
                celLrangE.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = celLrangE.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;

                celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[2, export.Columns.Count]];

                worKbooK.SaveAs(newFileName); ;
                worKbooK.Close();
                excel.Quit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ex.Message.Err();
                return false;
            }
            finally
            {
                worKsheeT = null;
                celLrangE = null;
                worKbooK = null;
            }  

        }
        #endregion

        #region chkConAS400

        private bool chkConAS400()
        {
            try
            {
                return BCSPRFTPDataService.Instance.chkConAS400(strConAS400);
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }

        #endregion

        #region SendAS400

        private bool SendAS400(DateTime? ISSUEDATE, string PALLETNO, string ITM400, string LOTNO, string TRACENO, decimal? WEIGHT, decimal? CH)
        {
            try
            {
                bool chkErr = true;

                #region string

                string ConStr = string.Empty;
                string ANNUL = string.Empty;
                string FLAGS = string.Empty;
                string RECTY = string.Empty;
                string CDSTO = string.Empty;
                string USRNM = string.Empty;
                int? DTTRA = null;
                int? DTINP = null;
                string CDEL0 = string.Empty;
                string CDCON = string.Empty;
                decimal? BLELE = null;
                string CDUM0 = string.Empty;
                string CDKE1 = string.Empty;
                string CDKE2 = string.Empty;
                string CDKE3 = string.Empty;
                string CDKE4 = string.Empty;
                string CDKE5 = string.Empty;
                string CDLOT = string.Empty;
                string CDTRA = string.Empty;
                string REFER = string.Empty;
                string LOCAT = string.Empty;
                string CDQUA = string.Empty;
                string QUACA = string.Empty;
                decimal? TECU1 = null;
                decimal? TECU2 = null;
                decimal? TECU3 = null;
                decimal? TECU4 = null;
                string TECU5 = string.Empty;
                string TECU6 = string.Empty;
                string COMM0 = string.Empty;
                Int64? DTORA = null;

                string dSend = ISSUEDATE.Value.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + ISSUEDATE.Value.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + ISSUEDATE.Value.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));

                DateTime? sendDate = null;
                string sDate = string.Empty;

                sendDate = DateTime.Now;

                if (sendDate != null)
                {
                    sDate = sendDate.Value.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("dd", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("HH", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("mm", CultureInfo.CreateSpecificCulture("en-US"));
                }

                #endregion

                ConStr = strConAS400;

                FLAGS = "R";
                RECTY = "S";
                CDSTO = "3N";
                USRNM = "PGMR";

                #region DTTRA

                try
                {
                    DTTRA = Convert.ToInt32(dSend);
                }
                catch
                {
                    DTTRA = null;
                }

                #endregion

                CDEL0 = PALLETNO;
                CDCON = PALLETNO;
                BLELE = WEIGHT;
                CDUM0 = "KG";
                CDKE1 = ITM400;
                CDLOT = LOTNO;
                CDTRA = "GR";

                TECU1 = WEIGHT;
                TECU2 = WEIGHT;
                TECU3 = CH;
                TECU4 = WEIGHT;
                TECU6 = TRACENO;

                #region DTORA

                if (!string.IsNullOrEmpty(sDate))
                {
                    try
                    {
                        DTORA = Convert.ToInt64(sDate);
                    }
                    catch
                    {
                        DTORA = null;
                    }
                }

                #endregion

                if (BCSPRFTPDataService.Instance.InsertDataBCSPRFTP(ConStr,
                                               ANNUL, FLAGS, RECTY, CDSTO, USRNM, DTTRA, DTINP,
                                               CDEL0, CDCON, BLELE, CDUM0, CDKE1, CDKE2, CDKE3, CDKE4, CDKE5, CDLOT, CDTRA,
                                               REFER, LOCAT, CDQUA, QUACA, TECU1, TECU2, TECU3, TECU4, TECU5, TECU6, COMM0, DTORA) == false)
                {
                    "Can't Insert Data SA400".ShowMessageBox(true);
                    chkErr = false;
                }

                return chkErr;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string logIn)
        {
            opera = logIn;
        }

        #endregion

    }
}
