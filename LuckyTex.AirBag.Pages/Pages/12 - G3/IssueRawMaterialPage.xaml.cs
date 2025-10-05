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
    /// Interaction logic for IssueRawMaterialPage.xaml
    /// </summary>
    public partial class IssueRawMaterialPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public IssueRawMaterialPage()
        {
            InitializeComponent();
            cmdSearch.Visibility = System.Windows.Visibility.Collapsed;
            grid4.Visibility = System.Windows.Visibility.Collapsed;

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadIssueTo();
            LoadYarnType();
            LoadItemYarn();
        }

        #endregion

        #region Internal Variables

        string strConAS400 = string.Empty;
        bool chkStatusAS400 = true;

        string opera = string.Empty;

        string P_PALLETNO = string.Empty;
        int RowNo = 0;
        int rowRequestNo = 0;

        private List<ItemYarnItem> instList = null;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigManager.Instance.LoadAS400Configs();
            strConAS400 = ConfigManager.Instance.AS400Config;

            ClearControl();

            chkStatusAS400 = chkConAS400();

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

        #region txtRequestNo_KeyDown
        private void txtRequestNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    if (CheckRequestNo(txtRequestNo.Text) == true)
                    {
                        txtPalletNo.Focus();
                        txtPalletNo.SelectAll();
                        e.Handled = true;
                    }
                    else
                    {
                        "Request No had in Database".ShowMessageBox();

                        txtRequestNo.Text = string.Empty;
                        txtRequestNo.Focus();
                        txtRequestNo.SelectAll();
                        e.Handled = true;
                    }
                }
            }
        }
        #endregion

        #region txtRequestNo_LostFocus
        private void txtRequestNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRequestNo.Text))
            {
                
                #region ไม่ได้ใช้ตอนนี้
                //if (GETREQUESTNODETAIL(txtRequestNo.Text) == false)
                //{
                //    txtPalletNo.Focus();
                //    txtPalletNo.SelectAll();
                //}
                #endregion
            }
        }
        #endregion

        #region txtPalletNo_KeyDown
        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtPalletNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtRequestNo.Text))
                    {
                        CheckPalletNoYarnStock(txtPalletNo.Text);

                        #region ไม่ได้ใช้ตอนนี้
                        //if (!string.IsNullOrEmpty(txtRequestNo.Text))
                        //{
                        //    if (cbIssueTo.SelectedValue != null)
                        //    {
                        //        if (CheckPalletNo(txtPalletNo.Text) == true)
                        //        {
                        //            AddPalletDetail(txtRequestNo.Text, txtPalletNo.Text, cbIssueTo.SelectedValue.ToString());
                        //        }
                        //        else
                        //        {
                        //            "Pallet No had in DataGrid".ShowMessageBox();

                        //            txtPalletNo.Focus();
                        //            txtPalletNo.SelectAll();
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    "Request No is not null".ShowMessageBox();

                        //    txtRequestNo.Focus();
                        //    txtRequestNo.SelectAll();
                        //}
                        #endregion
                    }
                    else
                    {
                        "Request No is not null".ShowMessageBox();

                        txtRequestNo.Focus();
                        txtRequestNo.SelectAll();
                    }
                }
            }
        }
        #endregion

        #endregion

        #region cbItemYarn_SelectionChanged
        private void cbItemYarn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbItemYarn.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(cbItemYarn.SelectedValue.ToString()))
                {
                    if (cbYarnType.SelectedValue != null)
                    {
                        if (!string.IsNullOrEmpty(cbYarnType.SelectedValue.ToString()))
                        {
                            string YarnType = string.Empty;

                            if (cbYarnType.SelectedValue.ToString() != "All")
                                YarnType = cbYarnType.SelectedValue.ToString();

                            G3_SearchYarnStock(string.Empty, cbItemYarn.SelectedValue.ToString(), YarnType);
                        }
                    }
                }
            }
        }
        #endregion

        #region cbYarnType_SelectionChanged
        private void cbYarnType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbYarnType.SelectedValue != null)
            {
                if (!string.IsNullOrEmpty(cbYarnType.SelectedValue.ToString()))
                {
                    string YarnType = string.Empty;

                    if (cbYarnType.SelectedValue.ToString() != "All")
                        YarnType = cbYarnType.SelectedValue.ToString();

                    if (cbItemYarn.SelectedValue != null)
                    {
                        if (!string.IsNullOrEmpty(cbItemYarn.SelectedValue.ToString()))
                        {
                            G3_SearchYarnStock(string.Empty, cbItemYarn.SelectedValue.ToString(), YarnType);
                        }
                    }
                }
            }
        }
        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRequestNo.Text))
            {
                if (GETREQUESTNODETAIL(txtRequestNo.Text) == false)
                {
                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();
                }
            }
            //else if (string.IsNullOrEmpty(txtRequestNo.Text) && !string.IsNullOrEmpty(txtPalletNo.Text))
            //{
            //    if (!string.IsNullOrEmpty(txtPalletNo.Text))
            //    {
            //        if (cbIssueTo.SelectedValue != null)
            //        {
            //            GETPALLETDETAIL(txtPalletNo.Text, cbIssueTo.SelectedValue.ToString());
            //        }
            //    }
            //}
        }

        #endregion

        #region cmdEditRequestNo_Click
        private void cmdEditRequestNo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRequestNo.Text) && !string.IsNullOrEmpty(txtNewRequestNo.Text))
            {
                if (EditPalletDetail(txtRequestNo.Text, txtNewRequestNo.Text) == true)
                {
                    txtRequestNo.Text = txtNewRequestNo.Text;
                    txtNewRequestNo.Text = string.Empty;
                }
            }
        }
        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbYarnType.SelectedValue != null)
            {
                if (cbYarnType.SelectedValue.ToString() == "All")
                {
                    Save();
                }
                else
                {
                    if (cbIssueTo.SelectedValue != null)
                    {
                        if (cbYarnType.SelectedValue.ToString() == "Warp")
                        {
                            if (cbIssueTo.SelectedValue.ToString() == "Warp AB" || cbIssueTo.SelectedValue.ToString() == "Warp AD")
                            {
                                Save();
                            }
                            else
                            {
                                "Can't Save because Issue To != Warp".ShowMessageBox();
                            }
                        }
                        else if (cbYarnType.SelectedValue.ToString() == "Weft")
                        {
                            if (cbIssueTo.SelectedValue.ToString() == "Weft AB" || cbIssueTo.SelectedValue.ToString() == "Weft AD")
                            {
                                Save();
                            }
                            else
                            {
                                "Can't Save because Issue To != Weft".ShowMessageBox();
                            }
                        }
                    }
                    else
                    {
                        "Issue To isn't Null".ShowMessageBox();
                    }
                }
            }
            else
            {
                "Yarn Type isn't Null".ShowMessageBox();
            }
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
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
                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridPalletDetail.CurrentCell.Item)).RowNo != 0)
                            {
                                RowNo = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridPalletDetail.CurrentCell.Item)).RowNo;
                            }
                            else
                            {
                                RowNo = 0;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridPalletDetail.CurrentCell.Item)).PALLETNO != null)
                            {
                                P_PALLETNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridPalletDetail.CurrentCell.Item)).PALLETNO;
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

        private void gridPalletDetail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                Type type = this.GetType();
                try
                {
                    #region ไม่ได้ใช้ตอนนี้
                    //if (RowNo != 0 && P_PALLETNO != "")
                    //    Remove(RowNo, P_PALLETNO);
                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void gridPalletDetail_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (gridPalletDetail.ItemsSource != null)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(e.Row.Item)).SelectData == true)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
            }
            catch
            {
            }
        }

        #endregion

        #region private Methods

        #region LoadIssueTo

        private void LoadIssueTo()
        {
            if (cbIssueTo.ItemsSource == null)
            {
                string[] str = new string[] { "Warp AB", "Weft AB", "Warp AD", "Weft AD" };

                cbIssueTo.ItemsSource = str;
                cbIssueTo.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadYarnType

        private void LoadYarnType()
        {
            if (cbYarnType.ItemsSource == null)
            {
                string[] str = new string[] { "All", "Warp", "Weft"};

                cbYarnType.ItemsSource = str;
                cbYarnType.SelectedIndex = 0;
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

        #region ClearControl

        private void ClearControl()
        {
            txtRequestNo.Text = string.Empty;
            txtRequestNo.IsEnabled = true;

            txtNewRequestNo.IsEnabled = false;
            cmdEditRequestNo.IsEnabled = false;

            txtPalletNo.Text = string.Empty;
            cbIssueTo.SelectedIndex = 0;
            cbYarnType.SelectedIndex = 0;

            if (cbItemYarn.IsEnabled == false)
                cbItemYarn.IsEnabled = true;

            if (cbYarnType.IsEnabled == false)
                cbYarnType.IsEnabled = true;
           
            dteIssueDate.SelectedDate = DateTime.Now;
            dteIssueDate.Text = DateTime.Now.ToString("dd/MM/yy");

            cbItemYarn.SelectedValue = null;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPalletDetail.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPalletDetail.SelectedItems.Clear();
            else
                this.gridPalletDetail.SelectedItem = null;

            gridPalletDetail.ItemsSource = null;

            txtTotalPallet.Text = "0";
            txtSumWeight.Text = "0";
            txtSumCH.Text = "0";

            P_PALLETNO = string.Empty;
            RowNo = 0;
            rowRequestNo = 0;

            txtRequestNo.Focus();
            txtRequestNo.SelectAll();
        }

        #endregion

        #region GETREQUESTNODETAIL
        private bool GETREQUESTNODETAIL(string REQUESTNO)
        {
            try
            {
                List<G3_GETREQUESTNODETAIL> lots = new List<G3_GETREQUESTNODETAIL>();

                lots = G3DataService.Instance.G3_GETREQUESTNODETAIL(REQUESTNO);
                //rowRequestNo = lots.Count;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridPalletDetail.ItemsSource = lots;

                    CalTotal();
                }
                else
                {
                    gridPalletDetail.ItemsSource = null;

                    txtTotalPallet.Text = "0";
                    txtSumWeight.Text = "0";
                    txtSumCH.Text = "0";
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region G3_SearchYarnStock

        private void G3_SearchYarnStock(string P_RECDATE, string P_ITMYARN, string P_YARNTYPE)
        {
            try
            {
                List<G3_SEARCHYARNSTOCKData> lots = new List<G3_SEARCHYARNSTOCKData>();

                lots = G3DataService.Instance.GetG3_SEARCHYARNSTOCKData(P_RECDATE, P_ITMYARN, P_YARNTYPE);
                //rowRequestNo = lots.Count;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridPalletDetail.ItemsSource = lots;

                    CalTotal();
                }
                else
                {
                    gridPalletDetail.ItemsSource = null;

                    txtTotalPallet.Text = "0";
                    txtSumWeight.Text = "0";
                    txtSumCH.Text = "0";

                    "No Data found".ShowMessageBox();
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString().Err();
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
                    #region ไม่ได้ใช้งาน
                    //if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO != null)
                    //{
                    //    pallet++;
                    //}

                    //if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT != null)
                    //{
                    //    weight += ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT.Value;
                    //}

                    //if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH != null)
                    //{
                    //    conech += ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH.Value;
                    //}

                    #endregion

                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).SelectData == true)
                    {
                        pallet++;

                        if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).WEIGHTQTY != null)
                        {
                            weight += ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).WEIGHTQTY.Value;
                        }

                        if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CONECH != null)
                        {
                            conech += ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CONECH.Value;
                        }
                    }

                    o++;
                }

                #region pallet
                if (pallet > 0)
                {
                    txtTotalPallet.Text = pallet.ToString("#,##0");
                }
                else
                {
                    txtTotalPallet.Text = "0";
                }
                #endregion

                if (pallet > 0)
                {
                    if (cbItemYarn.IsEnabled == true)
                        cbItemYarn.IsEnabled = false;

                    if (cbYarnType.IsEnabled == true)
                        cbYarnType.IsEnabled = false;
                }
                else
                {
                    if (cbItemYarn.IsEnabled == false)
                        cbItemYarn.IsEnabled = true;

                    if (cbYarnType.IsEnabled == false)
                        cbYarnType.IsEnabled = true;
                }

                #region weight
                if (weight > 0)
                {
                    txtSumWeight.Text = weight.ToString("#,##0.##");
                }
                else
                {
                    txtSumWeight.Text = "0";
                }
                #endregion

                #region conech
                if (conech > 0)
                {
                    txtSumCH.Text = conech.ToString("#,##0.##");
                }
                else
                {
                    txtSumCH.Text = "0";
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region AddPalletDetail

        private void AddPalletDetail(string REQUESTNO,string PalletNo, string ISSUETO)
        {
            try
            {
                 List<G3_GETREQUESTNODETAIL> lots = new List<G3_GETREQUESTNODETAIL>();

                lots = G3DataService.Instance.G3_GETPALLETDETAIL(REQUESTNO,PalletNo, opera, ISSUETO);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {

                    List<LuckyTex.Models.G3_GETREQUESTNODETAIL> dataList = new List<LuckyTex.Models.G3_GETREQUESTNODETAIL>();
                    int o = 0;
                    foreach (var row in gridPalletDetail.Items)
                    {
                        LuckyTex.Models.G3_GETREQUESTNODETAIL dataItem = new LuckyTex.Models.G3_GETREQUESTNODETAIL();

                        dataItem.RowNo = o + 1;
                        dataItem.ISSUEDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEDATE;
                        dataItem.PALLETNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO;
                        dataItem.TRACENO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TRACENO;
                        dataItem.WEIGHT = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT;
                        dataItem.CH = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH;
                        dataItem.ISSUEBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEBY;
                        dataItem.ISSUETO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO;
                        dataItem.REQUESTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO;
                        dataItem.PALLETTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETTYPE;
                        dataItem.ITM_YARN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM_YARN;
                        dataItem.LOTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).LOTNO;
                        dataItem.YARNTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).YARNTYPE;
                        dataItem.ITM400 = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM400;
                        dataItem.ENTRYDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ENTRYDATE;
                        dataItem.PACKAING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PACKAING;
                        dataItem.CLEAN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CLEAN;
                        dataItem.FALLDOWN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).FALLDOWN;
                        dataItem.TEARING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TEARING;
                        dataItem.NewData = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).NewData;

                        dataItem.DELETEFLAG = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).DELETEFLAG;
                        dataItem.EDITDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITDATE;
                        dataItem.EDITBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITBY;
                        dataItem.REMARK = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REMARK;

                        o++;

                        dataList.Add(dataItem);
                    }

                    int n = 0;
                    foreach (var row in lots)
                    {
                        LuckyTex.Models.G3_GETREQUESTNODETAIL dataItemNew = new LuckyTex.Models.G3_GETREQUESTNODETAIL();

                        dataItemNew.RowNo = gridPalletDetail.Items.Count + 1;

                        dataItemNew.ISSUEDATE = lots[n].ISSUEDATE;
                        dataItemNew.PALLETNO = lots[n].PALLETNO;
                        dataItemNew.TRACENO = lots[n].TRACENO;
                        dataItemNew.WEIGHT = lots[n].WEIGHT;
                        dataItemNew.CH = lots[n].CH;
                        dataItemNew.ISSUEBY = lots[n].ISSUEBY;
                        dataItemNew.ISSUETO = lots[n].ISSUETO;
                        dataItemNew.REQUESTNO = lots[n].REQUESTNO;
                        dataItemNew.PALLETTYPE = lots[n].PALLETTYPE;
                        dataItemNew.ITM_YARN = lots[n].ITM_YARN;
                        dataItemNew.LOTNO = lots[n].LOTNO;
                        dataItemNew.YARNTYPE = lots[n].YARNTYPE;
                        dataItemNew.ITM400 = lots[n].ITM400;
                        dataItemNew.ENTRYDATE = lots[n].ENTRYDATE;
                        dataItemNew.PACKAING = lots[n].PACKAING;
                        dataItemNew.CLEAN = lots[n].CLEAN;
                        dataItemNew.FALLDOWN = lots[n].FALLDOWN;
                        dataItemNew.TEARING = lots[n].TEARING;

                        dataItemNew.DELETEFLAG = lots[n].DELETEFLAG;
                        dataItemNew.EDITDATE = lots[n].EDITDATE;
                        dataItemNew.EDITBY = lots[n].EDITBY;
                        dataItemNew.REMARK = lots[n].REMARK;

                        dataItemNew.NewData = true;

                        n++;

                        dataList.Add(dataItemNew);
                    }

                    this.gridPalletDetail.ItemsSource = dataList;

                    CalTotal();

                    txtPalletNo.Text = string.Empty;
                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();

                    txtRequestNo.IsEnabled = false;
                    txtNewRequestNo.IsEnabled = true;
                    cmdEditRequestNo.IsEnabled = true;
                }
                else
                {
                    "No Data Found or this Pallet have been used".ShowMessageBox();

                    txtPalletNo.Text = string.Empty;
                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();

                    txtRequestNo.IsEnabled = true;
                    txtNewRequestNo.IsEnabled = false;
                    cmdEditRequestNo.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region Remove
        private void Remove(int RowNo, string P_PALLETNO)
        {
            if (gridPalletDetail.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.G3_GETREQUESTNODETAIL> dataList = new List<LuckyTex.Models.G3_GETREQUESTNODETAIL>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridPalletDetail.Items)
                        {
                            LuckyTex.Models.G3_GETREQUESTNODETAIL dataItem = new LuckyTex.Models.G3_GETREQUESTNODETAIL();

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).RowNo == RowNo
                                && ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO == P_PALLETNO
                                && ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).NewData == true)
                            {
                                dataItem.RowNo = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).RowNo;
                                dataItem.ISSUEDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEDATE;
                                dataItem.PALLETNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO;
                                dataItem.TRACENO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TRACENO;
                                dataItem.WEIGHT = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT;
                                dataItem.CH = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH;
                                dataItem.ISSUEBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEBY;
                                dataItem.ISSUETO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO;
                                dataItem.REQUESTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO;
                                dataItem.PALLETTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETTYPE;
                                dataItem.ITM_YARN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM_YARN;
                                dataItem.LOTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).LOTNO;
                                dataItem.YARNTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).YARNTYPE;
                                dataItem.ITM400 = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM400;
                                dataItem.ENTRYDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ENTRYDATE;
                                dataItem.PACKAING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PACKAING;
                                dataItem.CLEAN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CLEAN;
                                dataItem.FALLDOWN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).FALLDOWN;
                                dataItem.TEARING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TEARING;
                                dataItem.NewData = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).NewData;

                                dataItem.DELETEFLAG = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).DELETEFLAG;
                                dataItem.EDITDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITDATE;
                                dataItem.EDITBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITBY;
                                dataItem.REMARK = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REMARK;

                                dataList.Remove(dataItem);

                            }
                            else
                            {
                                dataItem.RowNo = i + 1;

                                dataItem.ISSUEDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEDATE;
                                dataItem.PALLETNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO;
                                dataItem.TRACENO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TRACENO;
                                dataItem.WEIGHT = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT;
                                dataItem.CH = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH;
                                dataItem.ISSUEBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEBY;
                                dataItem.ISSUETO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO;
                                dataItem.REQUESTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO;
                                dataItem.PALLETTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETTYPE;
                                dataItem.ITM_YARN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM_YARN;
                                dataItem.LOTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).LOTNO;
                                dataItem.YARNTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).YARNTYPE;
                                dataItem.ITM400 = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM400;
                                dataItem.ENTRYDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ENTRYDATE;
                                dataItem.PACKAING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PACKAING;
                                dataItem.CLEAN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CLEAN;
                                dataItem.FALLDOWN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).FALLDOWN;
                                dataItem.TEARING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TEARING;
                                dataItem.NewData = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).NewData;

                                dataItem.DELETEFLAG = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).DELETEFLAG;
                                dataItem.EDITDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITDATE;
                                dataItem.EDITBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITBY;
                                dataItem.REMARK = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REMARK;

                                dataList.Add(dataItem);
                                i++;
                            }
                            o++;
                        }

                        this.gridPalletDetail.ItemsSource = dataList;

                        RowNo = 0;
                        P_PALLETNO = "";

                        CalTotal();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region EditPalletDetail
        private bool EditPalletDetail(string Old_RequestNo, string New_RequestNo)
        {
            if (gridPalletDetail.Items.Count > 0)
            {
                if (MessageBox.Show("Do you want to change Request No", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.G3_GETREQUESTNODETAIL> dataList = new List<LuckyTex.Models.G3_GETREQUESTNODETAIL>();
                        int o = 0;

                        foreach (var row in gridPalletDetail.Items)
                        {
                            LuckyTex.Models.G3_GETREQUESTNODETAIL dataItem = new LuckyTex.Models.G3_GETREQUESTNODETAIL();

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO == Old_RequestNo)
                            {
                                dataItem.RowNo = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).RowNo;
                                dataItem.ISSUEDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEDATE;
                                dataItem.PALLETNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO;
                                dataItem.TRACENO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TRACENO;
                                dataItem.WEIGHT = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT;
                                dataItem.CH = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH;
                                dataItem.ISSUEBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEBY;
                                dataItem.ISSUETO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO;
                                //dataItem.REQUESTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO;
                                dataItem.REQUESTNO = New_RequestNo;
                                dataItem.PALLETTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETTYPE;
                                dataItem.ITM_YARN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM_YARN;
                                dataItem.LOTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).LOTNO;
                                dataItem.YARNTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).YARNTYPE;
                                dataItem.ITM400 = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM400;
                                dataItem.ENTRYDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ENTRYDATE;
                                dataItem.PACKAING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PACKAING;
                                dataItem.CLEAN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CLEAN;
                                dataItem.FALLDOWN = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).FALLDOWN;
                                dataItem.TEARING = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TEARING;
                                dataItem.NewData = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).NewData;

                                dataItem.DELETEFLAG = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).DELETEFLAG;
                                dataItem.EDITDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITDATE;
                                dataItem.EDITBY = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).EDITBY;
                                dataItem.REMARK = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REMARK;

                                dataList.Add(dataItem);
                            }

                            o++;
                        }

                        this.gridPalletDetail.ItemsSource = dataList;

                        RowNo = 0;

                        CalTotal();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region CheckPalletNo
        private bool CheckPalletNo(string PalletNo)
        {
            try
            {
                int o = 0;
                bool chkStatus = true;

                foreach (var row in gridPalletDetail.Items)
                {
                    if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO != null)
                    {
                        if (PalletNo == ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO)
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

        #region CheckPalletNo
        private bool CheckRequestNo(string REQUESTNO)
        {
            try
            {
                bool chkStatus = true;

                 List<G3_GETREQUESTNODETAIL> lots = new List<G3_GETREQUESTNODETAIL>();

                lots = G3DataService.Instance.G3_GETREQUESTNODETAIL(REQUESTNO);
                //rowRequestNo = lots.Count;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    chkStatus = false;
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

        #region CheckPalletNoYarnStock
        private void CheckPalletNoYarnStock(string PalletNo)
        {
            try
            {
                List<LuckyTex.Models.G3_SEARCHYARNSTOCKData> dataList = new List<LuckyTex.Models.G3_SEARCHYARNSTOCKData>();
                bool chkData = false;

                int o = 0;
                foreach (var row in gridPalletDetail.Items)
                {
                    LuckyTex.Models.G3_SEARCHYARNSTOCKData dataItem = new LuckyTex.Models.G3_SEARCHYARNSTOCKData();

                    dataItem.RowNo = o + 1;

                    dataItem.SelectData = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).SelectData;

                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETNO != null)
                    {
                        if (PalletNo == ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETNO)
                        {
                            dataItem.SelectData = true;
                            chkData = true;
                        }
                    }

                    dataItem.ENTRYDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).ENTRYDATE;
                    dataItem.ITM_YARN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).ITM_YARN;
                    dataItem.PALLETNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETNO;
                    dataItem.YARNTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).YARNTYPE;
                    dataItem.WEIGHTQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).WEIGHTQTY;
                    dataItem.CONECH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CONECH;
                    dataItem.VERIFY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).VERIFY;
                    dataItem.REMAINQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).REMAINQTY;
                    dataItem.RECEIVEBY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).RECEIVEBY;
                    dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).RECEIVEDATE;
                    dataItem.FINISHFLAG = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).FINISHFLAG;
                    dataItem.UPDATEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).UPDATEDATE;
                    dataItem.PALLETTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETTYPE;
                    dataItem.ITM400 = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).ITM400;
                    dataItem.UM = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).UM;
                    dataItem.PACKAING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PACKAING;
                    dataItem.CLEAN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CLEAN;
                    dataItem.TEARING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).TEARING;
                    dataItem.FALLDOWN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).FALLDOWN;
                    dataItem.CERTIFICATION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CERTIFICATION;
                    dataItem.INVOICE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).INVOICE;
                    dataItem.IDENTIFYAREA = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).IDENTIFYAREA;
                    dataItem.AMOUNTPALLET = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).AMOUNTPALLET;
                    dataItem.OTHER = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).OTHER;
                    dataItem.ACTION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).ACTION;
                    dataItem.MOVEMENTDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).MOVEMENTDATE;
                    dataItem.LOTNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).LOTNO;
                    dataItem.TRACENO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).TRACENO;
                    dataItem.KGPERCH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).KGPERCH;

                    o++;

                    dataList.Add(dataItem);
                }

                this.gridPalletDetail.ItemsSource = dataList;

                CalTotal();

                txtPalletNo.Text = string.Empty;
                txtPalletNo.Focus();
                txtPalletNo.SelectAll();

                txtRequestNo.IsEnabled = false;
                txtNewRequestNo.IsEnabled = true;
                cmdEditRequestNo.IsEnabled = true;

                if (chkData == false)
                {
                    "No Pallet No. in Stock".ShowMessageBox();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region Save
        private void Save()
        {
            try
            {
                int o = 0;
                bool chkSave = true;
                string REQUESTNO = string.Empty;
                string ISSUETO = string.Empty;
                DateTime? ISSUEDATE = null;
                string Operator = string.Empty;

                if (!string.IsNullOrEmpty(txtRequestNo.Text) && cbItemYarn.SelectedValue != null && dteIssueDate.SelectedDate != null)
                {
                    REQUESTNO = txtRequestNo.Text.Trim();

                    if (cbIssueTo.SelectedValue != null)
                    {
                        #region Old
                        //if (cbIssueTo.SelectedValue.ToString() == "Warp AB")
                        //    ISSUETO = "SA";
                        //else if (cbIssueTo.SelectedValue.ToString() == "Weft AB")
                        //    ISSUETO = "SB";
                        //else if (cbIssueTo.SelectedValue.ToString() == "Warp AD")
                        //    ISSUETO = "SC";
                        //else if (cbIssueTo.SelectedValue.ToString() == "Weft AD")
                        //    ISSUETO = "SD";
                        #endregion

                        if (cbIssueTo.SelectedValue.ToString() == "Warp AB")
                            ISSUETO = "SB";
                        else if (cbIssueTo.SelectedValue.ToString() == "Weft AB")
                            ISSUETO = "SA";
                        else if (cbIssueTo.SelectedValue.ToString() == "Warp AD")
                            ISSUETO = "SD";
                        else if (cbIssueTo.SelectedValue.ToString() == "Weft AD")
                            ISSUETO = "SC";
                    }

                    ISSUEDATE = dteIssueDate.SelectedDate;
                    Operator = txtOperator.Text;

                    foreach (var row in gridPalletDetail.Items)
                    {
                        if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).SelectData == true)
                        {
                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).RowNo != 0)
                            {


                                if (G3DataService.Instance.G3_INSERTUPDATEISSUEYARN(REQUESTNO
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETNO
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).TRACENO
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CONECH
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).WEIGHTQTY
                                    , ISSUEDATE
                                    , Operator
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETTYPE
                                    , ISSUETO) == true)
                                {
                                    if (chkStatusAS400 == true)
                                    {
                                        SendAS400(ISSUEDATE
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).PALLETNO
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).ITM400
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).LOTNO
                                            , ISSUETO
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).TRACENO
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).WEIGHTQTY
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridPalletDetail.Items)[o])).CONECH
                                            , REQUESTNO);
                                    }
                                }
                                else
                                {
                                    chkSave = false;
                                    break;
                                }
                            }
                        }

                        o++;
                    }

                    if (chkSave == true)
                    {
                        if (!string.IsNullOrEmpty(REQUESTNO))
                        {
                            string temp = "Request No " + REQUESTNO + " have been issue.";
                            //temp.ShowMessageBox();
                            
                            if (MessageBox.Show(temp+"\r\n"+"Do you want to Print", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            {
                                Preview(txtRequestNo.Text);
                            }

                            ClearControl();
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtRequestNo.Text))
                    {
                        "Request No isn't Null".ShowMessageBox();
                        txtRequestNo.Focus();
                        txtRequestNo.SelectAll();
                    }
                    else if (cbItemYarn.SelectedValue != null)
                    {
                        "Item Yarn isn't Null".ShowMessageBox();
                        cbItemYarn.Focus();
                    }
                    else if (dteIssueDate.SelectedDate != null)
                    {
                        "Issue Date isn't Null".ShowMessageBox();
                        dteIssueDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region ยังไม่ได้ใช้งาน Save
        //private void Save()
        //{
        //    try
        //    {
        //        int o = 0;
        //        bool chkSave = true;
        //        string REQUESTNO = string.Empty;
        //        string ISSUETO = string.Empty;

        //        foreach (var row in gridPalletDetail.Items)
        //        {
        //            if(0 == 0)
        //            REQUESTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO;

        //            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).RowNo != 0)
        //            {
        //                ISSUETO = string.Empty;

        //                if (!string.IsNullOrEmpty(((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO))
        //                {
        //                    if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO == "Warp AB")
        //                        ISSUETO = "SA";
        //                    else if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO == "Weft AB")
        //                        ISSUETO = "SB";
        //                    else if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO == "Warp AD")
        //                        ISSUETO = "SC";
        //                    else if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO == "Weft AD")
        //                        ISSUETO = "SD";
        //                }

        //                if (G3DataService.Instance.G3_INSERTUPDATEISSUEYARN(((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TRACENO
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEDATE
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEBY
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETTYPE
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUETO
        //                    , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).Old_REQUESTNO) == true)
        //                {
        //                    if (chkStatusAS400 == true)
        //                    {
        //                        SendAS400(((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ISSUEDATE
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).PALLETNO
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).ITM400
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).LOTNO
        //                            , ISSUETO
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).TRACENO
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).WEIGHT
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).CH
        //                            , ((LuckyTex.Models.G3_GETREQUESTNODETAIL)((gridPalletDetail.Items)[o])).REQUESTNO);
        //                    }
        //                }
        //                else
        //                {
        //                    chkSave = false;
        //                    break;
        //                }
        //            }

        //            o++;
        //        }

        //        if (chkSave == true)
        //        {
        //            if (!string.IsNullOrEmpty(REQUESTNO))
        //            {
        //                string temp = "Request No " + REQUESTNO + " have been issue.";
        //                temp.ShowMessageBox();

        //                GETREQUESTNODETAIL(REQUESTNO);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString().Err();
        //    }
        //}
        #endregion

        #region SendAS400

        private bool SendAS400(DateTime? ISSUEDATE, string PALLETNO, string ITM400, string LOTNO, string ISSUETO, string TRACENO, decimal? WEIGHT, decimal? CH, string REQUESTNO)
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

                decimal? pound = null;

                double? defPound = 2.2046;
                
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

                //  เพิ่มใหม่ 02/12/15
                #region DTINP

                try
                {
                    DTINP = Convert.ToInt32(dSend);
                }
                catch
                {
                    DTINP = null;
                }

                #endregion

                CDEL0 = PALLETNO;
                CDCON = PALLETNO;
                BLELE = WEIGHT;
                CDUM0 = "KG";
                CDKE1 = ITM400;
                CDLOT = LOTNO;
                CDTRA = ISSUETO;

                //  เพิ่มใหม่ 02/12/15
                REFER = REQUESTNO;
                LOCAT = "GD";
                CDQUA = "1";
                //----------------//

                // ปรับเพิ่มใหม่ 19/12/15
                #region pound
                if (WEIGHT != null)
                {
                    try
                    {
                        pound = MathEx.Round(System.Convert.ToDecimal((System.Convert.ToDouble(WEIGHT.Value) * defPound)), 2);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.Err();
                    }
                }
                #endregion

                TECU1 = pound;
                TECU2 = pound;

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

        #region CheckSavePrint
        private bool CheckSavePrint()
        {
            if (!string.IsNullOrEmpty(txtRequestNo.Text))
            {
                List<G3_GETREQUESTNODETAIL> lots = new List<G3_GETREQUESTNODETAIL>();

                lots = G3DataService.Instance.G3_GETREQUESTNODETAIL(txtRequestNo.Text);
                rowRequestNo = lots.Count;
            }
            else
            {
                rowRequestNo = 0;
            }

            if (rowRequestNo <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            #region ยังไม่ได้ใช้งาน
            //int gridRow = gridPalletDetail.Items.Count;

            //if (rowRequestNo <= 0)
            //{
            //    if (gridRow > 0)
            //    {
            //        "Check and Save Data first before printing".ShowMessageBox();
            //        return false;
            //    }
            //    else
            //    {
            //        "No Data Found".ShowMessageBox();
            //        return false;
            //    }
            //}
            //else
            //{
            //    if (rowRequestNo == gridPalletDetail.Items.Count)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        "Check and Save Data before printing".ShowMessageBox();
            //        return false;
            //    }
            //}
            #endregion
        }
        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string REQUESTNO)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "IssueRawMaterial";

                ConmonReportService.Instance.REQUESTNO = REQUESTNO;
           
                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();

                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string REQUESTNO)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "IssueRawMaterial";

                ConmonReportService.Instance.REQUESTNO = REQUESTNO;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
