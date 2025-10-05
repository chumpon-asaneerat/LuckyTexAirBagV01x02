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
    /// Interaction logic for PalletEditPage.xaml
    /// </summary>
    public partial class PalletEditPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PalletEditPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables
        private PackingSession _session = new PackingSession();

        string opera = string.Empty;
        string INSPECTIONLOT = string.Empty;
        int RowNo = 0;

        string gridINSLOT = string.Empty;
        string gridItemCode = string.Empty;
        string gridGrade = string.Empty;

        int CountPacking = 0;

        Queue<int> orderNos = new Queue<int>();
        Queue<string> inspectionLots = new Queue<string>();

        string _palletNO = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string _LOTNO = string.Empty;
        string _ITEMID = string.Empty;
        string _LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrade();
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
            if (CountPacking == 0 && gridPallet.Items.Count == 0)
            {
                PageManager.Instance.Back();
            }
            else
            {
                if (gridPallet.Items.Count != CountPacking)
                {
                    "Close This Page Without Saving Data?".ShowMessageBox();
                    PageManager.Instance.Back();
                }
                else
                {
                    PageManager.Instance.Back();
                }
            }

        }
        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadPacking();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdGetPalletDetail_Click

        private void cmdGetPalletDetail_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtPalletNo.Text))
                PACK_GETPALLETDETAIL(txtPalletNo.Text);
        }

        #endregion

        #region cmdNew_Click

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            New();
        }

        #endregion

        #region cmdDelete_Click

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (RowNo != 0 && !string.IsNullOrEmpty(INSPECTIONLOT))
                Remove(RowNo, INSPECTIONLOT);
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPalletNo.Text))
            {
                if (txtPalletNo.IsEnabled == false)
                {
                    if (Save() == true)
                    {
                        ClearControl();
                        New();
                    }
                }
                else
                {
                    "Please Insert Pallet No".ShowMessageBox();
                }
            }
            else
            {
                "Please Insert Pallet No".ShowMessageBox();
            }
        }

        #endregion

        #region cmdCancelPallet_Click

        private void cmdCancelPallet_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPalletNo.Text))
                CancelPallet(txtPalletNo.Text);
        }

        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_palletNO))
            {
                //if (MessageBox.Show("Do You Want to Cancel this Pallet " + _palletNO, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                //{
                    if (D365_PK_TOTALHEADER() == true)
                    {
                        "Cancel D365 complete".ShowMessageBox();
                    }
                //}
            }
            else
            {
                "Pallet No is null".ShowMessageBox();
            }
        }
        #endregion

        #endregion

        #region TextBox

        #region txtInspectionLot_KeyDown

        private void txtInspectionLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtInspectionLot.Text != "")
                {
                    LoadPackingINSLOT();
                    //cmdMoveToPallet.Focus();
                }

                e.Handled = true;
            }
        }

        #endregion

        #region txtInspectionLot_LostFocus

        private void txtInspectionLot_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (txtInspectionLot.Text != "")
            //{
            //    LoadPackingINSLOT();
            //}
        }

        #endregion

        #region txtPalletNo_KeyDown

        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtPalletNo.Text) && txtPalletNo.IsEnabled == true)
                {
                    PACK_GETPALLETDETAIL(txtPalletNo.Text);
                }

                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region gridPack_SelectedCellsChanged

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

        private void gridPack_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridPack.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridPack);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(gridPack.CurrentCell.Item)).INSPECTIONLOT != null)
                            {
                                gridINSLOT = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(gridPack.CurrentCell.Item)).INSPECTIONLOT;
                            }

                            if (((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(gridPack.CurrentCell.Item)).ITEMCODE != null)
                            {
                                gridItemCode = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(gridPack.CurrentCell.Item)).ITEMCODE;
                            }
                            if (((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(gridPack.CurrentCell.Item)).GRADE != null)
                            {
                                gridGrade = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(gridPack.CurrentCell.Item)).GRADE;
                            }
                        }
                    }
                }
                else
                {
                    gridINSLOT = string.Empty;
                    gridItemCode = string.Empty;
                    gridGrade = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CountPacking != 0 && gridPallet.Items.Count != 0)
            {
                //if (gridPallet.Items.Count >= CountPacking)
                //{
                //    "Data Over Data in DataGrid".ShowMessageBox();
                //}
                //else
                //{
                    List<PACK_SEARCHINSPECTIONDATA> lots = new List<PACK_SEARCHINSPECTIONDATA>();

                    lots = PackingDataService.Instance.Pack_SearchDataList(string.Empty, gridGrade, gridItemCode, gridINSLOT);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        DateTime? _STARTDATE = DateTime.Now;
                        string _INSPECTIONLOT = string.Empty;

                        string _PALLETNO = string.Empty;

                        string _ITEMCODE = string.Empty;
                        string _ITM_GROUP = string.Empty;
                        string _CUSTOMERTYPE = string.Empty;
                        string _GRADE = string.Empty;
                        string _LOADINGTYPE = string.Empty;
                        decimal? _NETWEIGHT = 0;
                        decimal? _GROSSWEIGHT = 0;
                        decimal? _GROSSLENGTH = 0;
                        decimal? _NETLENGTH = 0;

                        if (!string.IsNullOrEmpty(txtPalletNo.Text))
                            _PALLETNO = txtPalletNo.Text;

                        _STARTDATE = lots[0].STARTDATE;
                        _INSPECTIONLOT = lots[0].INSPECTIONLOT;
                        _ITEMCODE = lots[0].ITEMCODE;
                        _CUSTOMERTYPE = lots[0].CUSTOMERTYPE;
                        _GRADE = lots[0].GRADE;
                        _NETWEIGHT = lots[0].NETWEIGHT;
                        _GROSSWEIGHT = lots[0].GROSSWEIGHT;
                        _GROSSLENGTH = lots[0].GROSSLENGTH;
                        _NETLENGTH = lots[0].NETLENGTH;
                        _ITM_GROUP = lots[0].ITM_GROUP;
                        _LOADINGTYPE = lots[0].LOADINGTYPE;

                        if (_INSPECTIONLOT != "")
                        {
                            if (AddPalletDetail(_STARTDATE, _INSPECTIONLOT, _PALLETNO, _ITEMCODE, _CUSTOMERTYPE, _GRADE,
                             _NETWEIGHT, _GROSSWEIGHT, _NETLENGTH, _ITM_GROUP, _LOADINGTYPE, _GROSSLENGTH) == true)
                                txtInspectionLot.Text = "";
                            else
                            {
                                txtInspectionLot.Focus();
                                txtInspectionLot.SelectAll();
                                txtInspectionLot.Text = "";
                            }

                        }
                        else
                        {
                            "INSPECTION LOT is not null".ShowMessageBox(false);

                            txtInspectionLot.Focus();
                            txtInspectionLot.SelectAll();
                        }
                    }
                    else
                    {
                        "Can't Search data".ShowMessageBox(false);

                        txtInspectionLot.Focus();
                        txtInspectionLot.SelectAll();
                    }
                //}
            }
        }

        #endregion

        #region gridPallet_SelectedCellsChanged

        private void gridPallet_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridPallet.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridPallet);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.PACK_GETPALLETDETAIL)(gridPallet.CurrentCell.Item)).RowNo != 0)
                            {
                                RowNo = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(gridPallet.CurrentCell.Item)).RowNo;
                            }

                            if (((LuckyTex.Models.PACK_GETPALLETDETAIL)(gridPallet.CurrentCell.Item)).INSPECTIONLOT != null)
                            {
                                INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(gridPallet.CurrentCell.Item)).INSPECTIONLOT;
                            }

                            if (((LuckyTex.Models.PACK_GETPALLETDETAIL)(gridPallet.CurrentCell.Item)).PALLETNO != null)
                            {
                                _palletNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(gridPallet.CurrentCell.Item)).PALLETNO;
                            }
                            else
                            {
                                _palletNO = string.Empty;
                                PRODID = null;
                                HEADERID = null;

                                _LOTNO = string.Empty;
                                _ITEMID = string.Empty;
                                _LOADINGTYPE = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    _palletNO = string.Empty;
                    PRODID = null;
                    HEADERID = null;

                    _LOTNO = string.Empty;
                    _ITEMID = string.Empty;
                    _LOADINGTYPE = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void gridPallet_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                Type type = this.GetType();
                try
                {
                    if (RowNo != 0 && INSPECTIONLOT != "")
                        Remove(RowNo, INSPECTIONLOT);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #region private Methods

        #region LoadGrade

        private void LoadGrade()
        {
            if (cbGrade.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C" };

                cbGrade.ItemsSource = str;
                cbGrade.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<GetItemCodeData> items = _session.GetItemCodeData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_CODE";
                this.cbItemCode.SelectedValuePath = "ITM_CODE";
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
            dteInspectionDate.SelectedDate = null;
            dteInspectionDate.Text = "";
            cbItemCode.Text = "";
            cbItemCode.SelectedItem = null;
            cbGrade.Text = "";
            chkAll.IsChecked = false;

            txtInspectionLot.Text = "";

            gridINSLOT = string.Empty;
            gridItemCode = string.Empty;
            gridGrade = string.Empty;

            _palletNO = string.Empty;
            PRODID = null;
            HEADERID = null;

            _LOTNO = string.Empty;
            _ITEMID = string.Empty;
            _LOADINGTYPE = string.Empty;

            cmdCancelPallet.IsEnabled = false;
            txtPalletNo.IsEnabled = true;
           
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPack.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPack.SelectedItems.Clear();
            else
                this.gridPack.SelectedItem = null;

            gridPack.ItemsSource = null;
        }

        #endregion

        #region LoadPacking
        private void LoadPacking()
        {
            string _INSLOT = string.Empty;
            string _InspectionDate = string.Empty;
            string _ItemCode = string.Empty;
            string _Grade = string.Empty;

            gridINSLOT = string.Empty;
            gridItemCode = string.Empty;
            gridGrade = string.Empty;

            if (chkAll.IsChecked == false)
            {
                if (cbItemCode.SelectedValue != null)
                    _ItemCode = cbItemCode.SelectedValue.ToString();
            }

            if (dteInspectionDate.SelectedDate != null)
                _InspectionDate = dteInspectionDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            if (cbGrade.SelectedValue != null)
                _Grade = cbGrade.SelectedValue.ToString();

            if (txtInspectionLot.Text != "")
                _INSLOT = txtInspectionLot.Text;

            List<PACK_SEARCHINSPECTIONDATA> lots = new List<PACK_SEARCHINSPECTIONDATA>();

            lots = PackingDataService.Instance.Pack_SearchDataList(_InspectionDate, _Grade, _ItemCode, _INSLOT);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridPack.ItemsSource = lots;
            }
            else
            {
                gridPack.ItemsSource = null;
            }

        }
        #endregion

        #region LoadPackingINSLOT
        private void LoadPackingINSLOT()
        {
            if (CountPacking != 0 && gridPallet.Items.Count != 0)
            {
                //if (gridPallet.Items.Count >= CountPacking)
                //{
                //    "Data Over Data in DataGrid".ShowMessageBox();
                //}
                //else
                //{
                    string _INSLOT = string.Empty;
                    string _InspectionDate = string.Empty;
                    string _ItemCode = string.Empty;
                    string _Grade = string.Empty;

                    if (chkAll.IsChecked == false)
                    {
                        if (cbItemCode.SelectedValue != null)
                            _ItemCode = cbItemCode.SelectedValue.ToString();
                    }

                    if (dteInspectionDate.SelectedDate != null)
                        _InspectionDate = dteInspectionDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                    if (cbGrade.SelectedValue != null)
                        _Grade = cbGrade.SelectedValue.ToString();

                    if (txtInspectionLot.Text != "")
                        _INSLOT = txtInspectionLot.Text;

                    List<PACK_SEARCHINSPECTIONDATA> lots = new List<PACK_SEARCHINSPECTIONDATA>();

                    lots = PackingDataService.Instance.Pack_SearchDataList(_InspectionDate, _Grade, _ItemCode, _INSLOT);

                    if (null != lots && lots.Count > 0 && null != lots[0])
                    {
                        DateTime? _STARTDATE = DateTime.Now;
                        string _INSPECTIONLOT = string.Empty;
                        string _PALLETNO = string.Empty;

                        string _ITEMCODE = string.Empty;
                        string _ITM_GROUP = string.Empty;
                        string _CUSTOMERTYPE = string.Empty;
                        string _GRADE = string.Empty;
                        string _LOADINGTYPE = string.Empty;
                        decimal? _NETWEIGHT = 0;
                        decimal? _GROSSWEIGHT = 0;
                        decimal? _GROSSLENGTH = 0;
                        decimal? _NETLENGTH = 0;

                        if (!string.IsNullOrEmpty(txtPalletNo.Text))
                            _PALLETNO = txtPalletNo.Text;

                        _STARTDATE = lots[0].STARTDATE;
                        _INSPECTIONLOT = lots[0].INSPECTIONLOT;
                        _ITEMCODE = lots[0].ITEMCODE;
                        _CUSTOMERTYPE = lots[0].CUSTOMERTYPE;
                        _GRADE = lots[0].GRADE;
                        _NETWEIGHT = lots[0].NETWEIGHT;
                        _GROSSWEIGHT = lots[0].GROSSWEIGHT;
                        _GROSSLENGTH = lots[0].GROSSLENGTH;
                        _NETLENGTH = lots[0].NETLENGTH;
                        _ITM_GROUP = lots[0].ITM_GROUP;
                        _LOADINGTYPE = lots[0].LOADINGTYPE;

                        if (_INSPECTIONLOT != "")
                        {
                            if (AddPalletDetail(_STARTDATE, _INSPECTIONLOT, _PALLETNO, _ITEMCODE, _CUSTOMERTYPE, _GRADE,
                             _NETWEIGHT, _GROSSWEIGHT, _NETLENGTH, _ITM_GROUP, _LOADINGTYPE, _GROSSLENGTH) == true)
                                txtInspectionLot.Text = "";
                            else
                            {
                                txtInspectionLot.Focus();
                                txtInspectionLot.SelectAll();
                                txtInspectionLot.Text = "";
                            }

                        }
                        else
                        {
                            "INSPECTION LOT is not null".ShowMessageBox(false);

                            txtInspectionLot.Focus();
                            txtInspectionLot.SelectAll();
                        }
                    }
                    else
                    {
                        "Can't Search data".ShowMessageBox(false);

                        txtInspectionLot.Focus();
                        txtInspectionLot.SelectAll();
                    }
                //}
            }
        }
        #endregion

        #region AddPallet

        private void AddPallet()
        {
            if (gridPack.SelectedItems.Count > 0)
            {
                try
                {
                    
                    DateTime? STARTDATE = null;
                    string INSPECTIONLOT = string.Empty;
                    string PALLETNO = string.Empty;

                    string ITEMCODE = string.Empty;
                    string ITM_GROUP = string.Empty;
                    string CUSTOMERTYPE = string.Empty;
                    string GRADE = string.Empty;
                    decimal? NETLENGTH = null;
                    decimal? GROSSWEIGHT = null;
                    decimal? GROSSLENGTH = null;
                    decimal? NETWEIGHT = null;
                    string LOADINGTYPE = string.Empty;

                    if (!string.IsNullOrEmpty(txtPalletNo.Text))
                        PALLETNO = txtPalletNo.Text;

                    #region gridPack

                    foreach (object obj in gridPack.SelectedItems)
                    {
                        if (((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).INSPECTIONLOT != null)
                        {
                            STARTDATE = null;
                            INSPECTIONLOT = string.Empty;

                            ITEMCODE = string.Empty;
                            ITM_GROUP = string.Empty;
                            CUSTOMERTYPE = string.Empty;
                            GRADE = string.Empty;
                            NETLENGTH = null;
                            GROSSWEIGHT = null;
                            GROSSLENGTH = null;
                            NETWEIGHT = null;
                            LOADINGTYPE = string.Empty;

                            INSPECTIONLOT = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).INSPECTIONLOT;
                            STARTDATE = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).STARTDATE;
                            ITEMCODE = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).ITEMCODE;
                            CUSTOMERTYPE = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).CUSTOMERTYPE;
                            GRADE = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).GRADE;
                            NETWEIGHT = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).NETWEIGHT;
                            GROSSWEIGHT = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).GROSSWEIGHT;
                            GROSSLENGTH = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).GROSSLENGTH;
                            NETLENGTH = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).NETLENGTH;
                            ITM_GROUP = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).ITM_GROUP;
                            LOADINGTYPE = ((LuckyTex.Models.PACK_SEARCHINSPECTIONDATA)(obj)).LOADINGTYPE;

                            if (AddPalletDetail(STARTDATE, INSPECTIONLOT,PALLETNO, ITEMCODE, CUSTOMERTYPE, GRADE, NETWEIGHT, GROSSWEIGHT, NETLENGTH, ITM_GROUP, LOADINGTYPE, GROSSLENGTH) == false)
                            {
                                break;
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
        }

        #endregion

        #region AddPalletDetail
        private bool AddPalletDetail(DateTime? _STARTDATE, string _INSPECTIONLOT, string _PALLETNO, string _ITEMCODE, string _CUSTOMERTYPE, string _GRADE,
                 decimal? _NETWEIGHT, decimal? _GROSSWEIGHT, decimal? _NETLENGTH, string _ITM_GROUP, string _LOADINGTYPE, decimal? _GROSSLENGTH)
        {
            bool chkAddData = true;
            bool chkInsLot = true;
            string FirstGRADE = string.Empty;
            string FirstITM_GROUP = string.Empty;
            string FirstCUSTOMERTYPE = string.Empty;
            string FirstLOADINGTYPE = string.Empty;

            List<LuckyTex.Models.PACK_GETPALLETDETAIL> dataList = new List<LuckyTex.Models.PACK_GETPALLETDETAIL>();
            int o = 0;
            foreach (var row in gridPallet.Items)
            {
                LuckyTex.Models.PACK_GETPALLETDETAIL dataItem = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                dataItem.RowNo = o + 1;
                dataItem.INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT;
                dataItem.INSPECTIONLOT_OLD = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT_OLD;

                if (dataItem.INSPECTIONLOT == _INSPECTIONLOT)
                    chkInsLot = false;

                dataItem.INSPECTIONDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONDATE;
                dataItem.PALLETNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO;

                dataItem.ITEMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITEMCODE;
                dataItem.ITM_GROUP = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITM_GROUP;
                dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                dataItem.GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;
                dataItem.NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETLENGTH;
                dataItem.GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSWEIGHT;
                dataItem.GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSLENGTH;
                dataItem.NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETWEIGHT;
                dataItem.LOADINGTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;

                dataItem.ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO;

                if (o == 0)
                {
                    FirstGRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;
                    FirstITM_GROUP = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITM_GROUP;
                    FirstCUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                    FirstLOADINGTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;
                }

                o++;
                dataList.Add(dataItem);
            }

            if (chkInsLot == true)
            {
                if (FirstGRADE == "" && FirstITM_GROUP == "" && FirstLOADINGTYPE == "")
                {
                    LuckyTex.Models.PACK_GETPALLETDETAIL dataItemNew = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                    dataItemNew.RowNo = gridPallet.Items.Count + 1;

                    dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                    dataItemNew.PALLETNO = _PALLETNO;

                    dataItemNew.INSPECTIONDATE = _STARTDATE;
                    dataItemNew.ITEMCODE = _ITEMCODE;
                    dataItemNew.ITM_GROUP = _ITM_GROUP;
                    dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                    dataItemNew.GRADE = _GRADE;
                    dataItemNew.NETWEIGHT = _NETWEIGHT;
                    dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                    dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                    dataItemNew.NETLENGTH = _NETLENGTH;
                    dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                    #region orderNos

                    if (orderNos.Count != 0)
                    {
                        dataItemNew.ORDERNO = orderNos.Dequeue();
                    }
                    else
                    {
                        dataItemNew.ORDERNO = dataItemNew.RowNo;
                    }

                    #endregion

                    #region inspectionLots

                    if (inspectionLots.Count != 0)
                    {
                        dataItemNew.INSPECTIONLOT_OLD = inspectionLots.Dequeue();
                    }
                    else
                    {
                        //dataItemNew.INSPECTIONLOT_OLD = dataItemNew.INSPECTIONLOT;
                    }

                    #endregion

                    dataList.Add(dataItemNew);
                }
                else
                {
                    if (FirstLOADINGTYPE == _LOADINGTYPE)
                    {
                        if (FirstGRADE == "A")
                        {
                            #region FirstGRADE = A

                            if (_GRADE == FirstGRADE)
                            {
                                if (_ITM_GROUP == FirstITM_GROUP)
                                {
                                    if (_CUSTOMERTYPE == FirstCUSTOMERTYPE)
                                    {
                                        LuckyTex.Models.PACK_GETPALLETDETAIL dataItemNew = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                                        dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                        dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                        dataItemNew.INSPECTIONDATE = _STARTDATE;
                                        dataItemNew.ITEMCODE = _ITEMCODE;
                                        dataItemNew.ITM_GROUP = _ITM_GROUP;
                                        dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                        dataItemNew.GRADE = _GRADE;
                                        dataItemNew.NETWEIGHT = _NETWEIGHT;
                                        dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                        dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                        dataItemNew.NETLENGTH = _NETLENGTH;
                                        dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                                        #region orderNos

                                        if (orderNos.Count != 0)
                                        {
                                            dataItemNew.ORDERNO = orderNos.Dequeue();
                                        }
                                        else
                                        {
                                            dataItemNew.ORDERNO = dataItemNew.RowNo;
                                        }

                                        #endregion

                                        #region inspectionLots

                                        if (inspectionLots.Count != 0)
                                        {
                                            dataItemNew.INSPECTIONLOT_OLD = inspectionLots.Dequeue();
                                        }
                                        else
                                        {
                                            //dataItemNew.INSPECTIONLOT_OLD = dataItemNew.INSPECTIONLOT;
                                        }

                                        #endregion

                                        dataList.Add(dataItemNew);
                                    }
                                    else
                                    {
                                        chkAddData = false;
                                    }
                                }
                                else
                                {
                                    chkAddData = false;
                                }
                            }
                            else
                            {
                                chkAddData = false;
                            }

                            #endregion
                        }
                        else if (FirstGRADE == "B")
                        {
                            #region FirstGRADE = B

                            if (_GRADE == FirstGRADE)
                            {
                                if (_ITM_GROUP == FirstITM_GROUP)
                                {
                                    if (_CUSTOMERTYPE == FirstCUSTOMERTYPE)
                                    {
                                        LuckyTex.Models.PACK_GETPALLETDETAIL dataItemNew = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                                        dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                        dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                        dataItemNew.INSPECTIONDATE = _STARTDATE;
                                        dataItemNew.ITEMCODE = _ITEMCODE;
                                        dataItemNew.ITM_GROUP = _ITM_GROUP;
                                        dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                        dataItemNew.GRADE = _GRADE;
                                        dataItemNew.NETWEIGHT = _NETWEIGHT;
                                        dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                        dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                        dataItemNew.NETLENGTH = _NETLENGTH;
                                        dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                                        #region orderNos

                                        if (orderNos.Count != 0)
                                        {
                                            dataItemNew.ORDERNO = orderNos.Dequeue();
                                        }
                                        else
                                        {
                                            dataItemNew.ORDERNO = dataItemNew.RowNo;
                                        }

                                        #endregion

                                        #region inspectionLots

                                        if (inspectionLots.Count != 0)
                                        {
                                            dataItemNew.INSPECTIONLOT_OLD = inspectionLots.Dequeue();
                                        }
                                        else
                                        {
                                            //dataItemNew.INSPECTIONLOT_OLD = dataItemNew.INSPECTIONLOT;
                                        }

                                        #endregion

                                        dataList.Add(dataItemNew);
                                    }
                                    else
                                    {
                                        chkAddData = false;
                                    }
                                }
                                else
                                {
                                    chkAddData = false;
                                }
                            }
                            else
                            {
                                chkAddData = false;
                            }

                            #endregion
                        }
                        else if (FirstGRADE == "C")
                        {
                            #region FirstGRADE = C

                            if (_GRADE == FirstGRADE)
                            {
                                //if (_CUSTOMERTYPE == FirstCUSTOMERTYPE)
                                //{
                                LuckyTex.Models.PACK_GETPALLETDETAIL dataItemNew = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                                    dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                    dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                    dataItemNew.INSPECTIONDATE = _STARTDATE;
                                    dataItemNew.ITEMCODE = _ITEMCODE;
                                    dataItemNew.ITM_GROUP = _ITM_GROUP;
                                    dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                    dataItemNew.GRADE = _GRADE;
                                    dataItemNew.NETWEIGHT = _NETWEIGHT;
                                    dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                    dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                    dataItemNew.NETLENGTH = _NETLENGTH;
                                    dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                                    #region orderNos

                                    if (orderNos.Count != 0)
                                    {
                                        dataItemNew.ORDERNO = orderNos.Dequeue();
                                    }
                                    else
                                    {
                                        dataItemNew.ORDERNO = dataItemNew.RowNo;
                                    }

                                    #endregion

                                    #region inspectionLots

                                    if (inspectionLots.Count != 0)
                                    {
                                        dataItemNew.INSPECTIONLOT_OLD = inspectionLots.Dequeue();
                                    }
                                    else
                                    {
                                        //dataItemNew.INSPECTIONLOT_OLD = dataItemNew.INSPECTIONLOT;
                                    }

                                    #endregion

                                    dataList.Add(dataItemNew);
                                //}
                                //else
                                //{
                                //    chkAddData = false;
                                //}
                            }
                            else
                            {
                                chkAddData = false;
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        if (FirstGRADE == "C")
                        {
                            #region FirstGRADE = C

                            if (_GRADE == FirstGRADE)
                            {
                                //if (_CUSTOMERTYPE == FirstCUSTOMERTYPE)
                                //{
                                LuckyTex.Models.PACK_GETPALLETDETAIL dataItemNew = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                                dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                dataItemNew.INSPECTIONDATE = _STARTDATE;
                                dataItemNew.ITEMCODE = _ITEMCODE;
                                dataItemNew.ITM_GROUP = _ITM_GROUP;
                                dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                dataItemNew.GRADE = _GRADE;
                                dataItemNew.NETWEIGHT = _NETWEIGHT;
                                dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                dataItemNew.NETLENGTH = _NETLENGTH;
                                dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                                #region orderNos

                                if (orderNos.Count != 0)
                                {
                                    dataItemNew.ORDERNO = orderNos.Dequeue();
                                }
                                else
                                {
                                    dataItemNew.ORDERNO = dataItemNew.RowNo;
                                }

                                #endregion

                                #region inspectionLots

                                if (inspectionLots.Count != 0)
                                {
                                    dataItemNew.INSPECTIONLOT_OLD = inspectionLots.Dequeue();
                                }
                                else
                                {
                                    //dataItemNew.INSPECTIONLOT_OLD = dataItemNew.INSPECTIONLOT;
                                }

                                #endregion

                                dataList.Add(dataItemNew);
                                //}
                                //else
                                //{
                                //    chkAddData = false;
                                //}
                            }
                            else
                            {
                                chkAddData = false;
                            }

                            #endregion
                        }
                        else
                        {
                            chkAddData = false;
                        }
                    }
                }
            }

            this.gridPallet.ItemsSource = dataList;

            if (chkAddData == false)
                "This Inspection Lot cannot be added in this Pallet".ShowMessageBox(true);

            return chkAddData;

        }
        #endregion

        #region PACK_GETPALLETDETAIL

        private void PACK_GETPALLETDETAIL(string PalletNo)
        {
            try
            {
                CountPacking = 0;
                orderNos.Clear();
                inspectionLots.Clear();

                List<PACK_GETPALLETDETAIL> lots = new List<PACK_GETPALLETDETAIL>();

                lots = PackingDataService.Instance.PACK_GETPALLETDETAIL(PalletNo);
                CountPacking = lots.Count;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridPallet.ItemsSource = lots;

                    cmdCancelPallet.IsEnabled = true;
                    txtPalletNo.IsEnabled = false;
                }
                else
                {
                    gridPallet.ItemsSource = null;

                    cmdCancelPallet.IsEnabled = false;
                    txtPalletNo.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region Remove
        private void Remove(int RowNo, string INSPECTIONLOT)
        {
            if (gridPallet.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.PACK_GETPALLETDETAIL> dataList = new List<LuckyTex.Models.PACK_GETPALLETDETAIL>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridPallet.Items)
                        {
                            LuckyTex.Models.PACK_GETPALLETDETAIL dataItem = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                            if (((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).RowNo == RowNo
                                && ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT == INSPECTIONLOT)
                            {
                                dataItem.PALLETNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO;

                                dataItem.INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT;
                                dataItem.INSPECTIONLOT_OLD = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT_OLD;

                                dataItem.INSPECTIONDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONDATE;
                                dataItem.ITEMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITEMCODE;
                                dataItem.ITM_GROUP = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITM_GROUP;
                                dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                                dataItem.GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;
                                dataItem.NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETLENGTH;
                                dataItem.GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSWEIGHT;
                                dataItem.GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSLENGTH;

                                dataItem.NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETWEIGHT;
                                dataItem.LOADINGTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;

                                dataItem.ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO;



                                if (dataItem.ORDERNO != null)
                                    orderNos.Enqueue(Convert.ToInt32(dataItem.ORDERNO));

                                if (!string.IsNullOrEmpty(dataItem.INSPECTIONLOT) && !string.IsNullOrEmpty(dataItem.INSPECTIONLOT_OLD))
                                {
                                    if (dataItem.INSPECTIONLOT == dataItem.INSPECTIONLOT_OLD)
                                    {
                                        inspectionLots.Enqueue(dataItem.INSPECTIONLOT);
                                    }
                                    else
                                    {
                                        inspectionLots.Enqueue(dataItem.INSPECTIONLOT_OLD);
                                    }
                                }

                                dataList.Remove(dataItem);
                            }
                            else
                            {
                                dataItem.RowNo = i + 1;
                                dataItem.PALLETNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO;

                                dataItem.INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT;
                                dataItem.INSPECTIONLOT_OLD = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT_OLD;

                                dataItem.INSPECTIONDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONDATE;
                                dataItem.ITEMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITEMCODE;
                                dataItem.ITM_GROUP = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITM_GROUP;
                                dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                                dataItem.GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;
                                dataItem.NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETLENGTH;
                                dataItem.GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSWEIGHT;
                                dataItem.GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSLENGTH;
                                dataItem.NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETWEIGHT;
                                dataItem.LOADINGTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;

                                dataItem.ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO;

                                dataList.Add(dataItem);
                                i++;
                            }
                            o++;
                        }

                        this.gridPallet.ItemsSource = dataList;

                        RowNo = 0;
                        INSPECTIONLOT = "";     
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Remove
        private void Remove(string PALLETNO)
        {
            try
            {

                List<LuckyTex.Models.PACK_GETPALLETDETAIL> dataList = new List<LuckyTex.Models.PACK_GETPALLETDETAIL>();
                int o = 0;
                int i = 0;
                foreach (var row in gridPallet.Items)
                {
                    LuckyTex.Models.PACK_GETPALLETDETAIL dataItem = new LuckyTex.Models.PACK_GETPALLETDETAIL();

                    if (((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO == PALLETNO)
                    {
                        dataItem.PALLETNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO;

                        dataItem.INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT;
                        dataItem.INSPECTIONLOT_OLD = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT_OLD;

                        dataItem.INSPECTIONDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONDATE;
                        dataItem.ITEMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITEMCODE;
                        dataItem.ITM_GROUP = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITM_GROUP;
                        dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                        dataItem.GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;
                        dataItem.NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETLENGTH;
                        dataItem.GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSWEIGHT;
                        dataItem.GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSLENGTH;

                        dataItem.NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETWEIGHT;
                        dataItem.LOADINGTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;

                        dataItem.ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO;

                        if (dataItem.ORDERNO != null)
                            orderNos.Enqueue(Convert.ToInt32(dataItem.ORDERNO));

                        if (!string.IsNullOrEmpty(dataItem.INSPECTIONLOT) && !string.IsNullOrEmpty(dataItem.INSPECTIONLOT_OLD))
                        {
                            if (dataItem.INSPECTIONLOT == dataItem.INSPECTIONLOT_OLD)
                            {
                                inspectionLots.Enqueue(dataItem.INSPECTIONLOT);
                            }
                            else
                            {
                                inspectionLots.Enqueue(dataItem.INSPECTIONLOT_OLD);
                            }
                        }

                        dataList.Remove(dataItem);
                    }
                    else
                    {
                        dataItem.RowNo = i + 1;
                        dataItem.PALLETNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO;

                        dataItem.INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT;
                        dataItem.INSPECTIONLOT_OLD = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT_OLD;

                        dataItem.INSPECTIONDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONDATE;
                        dataItem.ITEMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITEMCODE;
                        dataItem.ITM_GROUP = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITM_GROUP;
                        dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                        dataItem.GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;
                        dataItem.NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETLENGTH;
                        dataItem.GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSWEIGHT;
                        dataItem.GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSLENGTH;
                        dataItem.NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETWEIGHT;
                        dataItem.LOADINGTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;

                        dataItem.ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO;

                        dataList.Add(dataItem);
                        i++;
                    }
                    o++;
                }

                this.gridPallet.ItemsSource = dataList;

                RowNo = 0;
                INSPECTIONLOT = "";

                txtPalletNo.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region CreatePallet

        private void CreatePallet()
        {
            try
            {

                DateTime? INSPECTIONDATE = null;
                string INSPECTIONLOT = string.Empty;
                string ITEMCODE = string.Empty;
                string CUSTOMERTYPE = string.Empty;
                string GRADE = string.Empty;

                decimal? NETLENGTH = null;
                decimal? NETWEIGHT = null;
                decimal? GROSSLENGTH = null;
                decimal? GROSSWEIGHT = null;

                string PALLETNO = string.Empty;
                string LOADTYPE = string.Empty;
                decimal? ORDERNO = null;

                string MsgPALLETNO = string.Empty;

                bool chkError = true;

                #region gridPack

                foreach (object obj in gridPallet.ItemsSource)
                {
                    if (((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).INSPECTIONLOT != null)
                    {

                        INSPECTIONDATE = null;
                        INSPECTIONLOT = string.Empty;
                        ITEMCODE = string.Empty;
                        CUSTOMERTYPE = string.Empty;
                        GRADE = string.Empty;

                        NETLENGTH = null;
                        NETWEIGHT = null;
                        GROSSLENGTH = null;
                        GROSSWEIGHT = null;
                        LOADTYPE = string.Empty;
                        ORDERNO = null;

                        INSPECTIONLOT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).INSPECTIONLOT;
                        INSPECTIONDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).INSPECTIONDATE;
                        ITEMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).ITEMCODE;
                        CUSTOMERTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).CUSTOMERTYPE;
                        GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).GRADE;

                        NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).NETLENGTH;
                        NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).NETWEIGHT;
                        GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).GROSSLENGTH;
                        GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).GROSSWEIGHT;

                        LOADTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).LOADINGTYPE;
                        ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)(obj)).RowNo;

                        if (UpdateInspectionProcess(INSPECTIONLOT, INSPECTIONDATE) == false)
                        {
                            chkError = false;
                            break;
                        }
                        else
                        {

                            if (PALLETNO == "")
                            {
                                if (txtOperator.Text != "")
                                {
                                    PALLETNO = PACK_INSERTPACKINGPALLET(txtOperator.Text);
                                }
                            }

                            if (PALLETNO != "")
                            {
                                if (PACK_INSPACKINGPALLETDETAIL(PALLETNO, ORDERNO, INSPECTIONLOT, ITEMCODE, GRADE, NETLENGTH, NETWEIGHT, GROSSWEIGHT, CUSTOMERTYPE, INSPECTIONDATE, LOADTYPE, GROSSLENGTH) == true)
                                {
                                    if (MsgPALLETNO == "")
                                        MsgPALLETNO = "Pallet No " + PALLETNO + " have been created" + "\r\n";
                                }
                                else
                                {
                                    chkError = false;
                                    break;
                                }
                            }
                            else
                            {
                                chkError = false;
                                break;
                            }
                        }
                    }
                }

                if (MsgPALLETNO != "")
                    MsgPALLETNO.ShowMessageBox(false);

                if (chkError == true)
                {
                    ClearControl();

                    // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                    if (this.gridPallet.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                        this.gridPallet.SelectedItems.Clear();
                    else
                        this.gridPallet.SelectedItem = null;

                    gridPallet.ItemsSource = null;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        private void CancelPallet(string PalletNo)
        {
            if (MessageBox.Show("Do You Want to Cancel this Pallet " + PalletNo, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (PackingDataService.Instance.PACK_CANCELPALLET(PalletNo) == true)
                {
                    Remove(txtPalletNo.Text);
                }
                else
                {
                    "Can't Cancel Pallet".ShowMessageBox(true);
                }
            }
        }

        private void New()
        {
            txtPalletNo.Text = "";

            cmdCancelPallet.IsEnabled = false;
            txtPalletNo.IsEnabled = true;

            CountPacking = 0;
            orderNos.Clear();
            inspectionLots.Clear();

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPallet.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPallet.SelectedItems.Clear();
            else
                this.gridPallet.SelectedItem = null;

            gridPallet.ItemsSource = null;
        }

        private bool Save()
        {
            bool chkSave = true;

            if (CountPacking != 0 && gridPallet.Items.Count != 0)
            {

                string P_PALLETNO = string.Empty;
                decimal? P_ORDERNO = null;
                string P_INSLOT_OLD = string.Empty;
                string P_INSLOT_NEW = string.Empty;
                string P_ITMCODE = string.Empty;
                string P_GRADE = string.Empty;
                decimal? P_NETLENGTH = null;
                decimal? P_GROSSLENGTH = null;
                decimal? P_NETWEIGHT = null;
                decimal? P_GROSSWEIGHT = null;
                string P_CUSTYPE = string.Empty;
                DateTime? P_INSPECTDATE = null;
                string P_LOADTYPE = string.Empty;

                int o = 0;
                foreach (var row in gridPallet.Items)
                {
                    P_PALLETNO = string.Empty;
                    P_ORDERNO = null;
                    P_INSLOT_OLD = string.Empty;
                    P_INSLOT_NEW = string.Empty;
                    P_ITMCODE = string.Empty;
                    P_GRADE = string.Empty;
                    P_NETLENGTH = null;
                    P_GROSSLENGTH = null;
                    P_NETWEIGHT = null;
                    P_GROSSWEIGHT = null;
                    P_CUSTYPE = string.Empty;
                    P_INSPECTDATE = null;
                    P_LOADTYPE = string.Empty;

                    if (!string.IsNullOrEmpty(((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO))
                    {
                        P_PALLETNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).PALLETNO;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(txtPalletNo.Text))
                        {
                            P_PALLETNO = txtPalletNo.Text;
                        }
                    }

                    if (((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO != null)
                    {
                        P_ORDERNO = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ORDERNO;
                    }

                    P_INSLOT_NEW = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT;
                    P_INSLOT_OLD = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONLOT_OLD;

                    P_ITMCODE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).ITEMCODE;
                    P_GRADE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GRADE;

                    P_NETLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETLENGTH;
                    P_GROSSLENGTH = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSLENGTH;
                    P_NETWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).NETWEIGHT;
                    P_GROSSWEIGHT = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).GROSSWEIGHT;

                    P_CUSTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).CUSTOMERTYPE;
                    P_INSPECTDATE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).INSPECTIONDATE;
                    P_LOADTYPE = ((LuckyTex.Models.PACK_GETPALLETDETAIL)((gridPallet.Items)[o])).LOADINGTYPE;

                    if (PackingDataService.Instance.PACK_EDITPACKINGPALLETDETAIL(P_PALLETNO, P_ORDERNO, P_INSLOT_OLD, P_INSLOT_NEW,
                                                             P_ITMCODE, P_GRADE, P_NETLENGTH, P_GROSSLENGTH, P_NETWEIGHT, P_GROSSWEIGHT,
                                                             P_CUSTYPE, P_INSPECTDATE, P_LOADTYPE) == false)
                    {
                        "Can't Edit Pallet".ShowMessageBox();
                        break;
                    }

                    o++;
                }

                if (gridPallet.Items.Count < CountPacking)
                {
                    if (orderNos.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(txtPalletNo.Text))
                            P_PALLETNO = txtPalletNo.Text;

                        for (int i = 0; i < orderNos.Count; i++)
                        {
                            if (PackingDataService.Instance.PACK_EDITPACKINGPALLETDETAIL(P_PALLETNO, orderNos.Dequeue(), inspectionLots.Dequeue(), null, 
                                                           null, null, null, null, null, null,
                                                           null, null, null) == false)
                            {
                                "Can't Edit Pallet".ShowMessageBox();
                                break;
                            }

                        }
                    }
                }

            }
            else
            {
                chkSave = false;
            }

            return chkSave;
        }

        #region UpdateInspectionProcess
        private bool UpdateInspectionProcess(string _inspecionLotNo, DateTime? _startDate)
        {
            try
            {
                PackingDataService.Instance.UpdateInspectionProcess(_inspecionLotNo, _startDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region PACK_INSERTPACKINGPALLET
        private string PACK_INSERTPACKINGPALLET(string Operator)
        {
            string PALLETNO = string.Empty;
            try
            {
                PALLETNO = PackingDataService.Instance.PACK_INSERTPACKINGPALLET(Operator);
                return PALLETNO;
            }
            catch
            {
                return PALLETNO;
            }
        }
        #endregion

        #region PACK_INSPACKINGPALLETDETAIL
        private bool PACK_INSPACKINGPALLETDETAIL(string PALLETNO,decimal? ORDERNO , string insLotNo, string ITMCODE, string GRADE
            , decimal? NETLENGTH, decimal? NETWEIGHT, decimal? GROSSWEIGHT, string CUSTYPE, DateTime? INSPECTDATE, string LOADTYPE, decimal? GROSSLENGTH)
        {
            try
            {
                PackingDataService.Instance.PACK_INSPACKINGPALLETDETAIL(PALLETNO,ORDERNO, insLotNo, ITMCODE, GRADE
            , NETLENGTH, NETWEIGHT, GROSSWEIGHT, CUSTYPE, INSPECTDATE, LOADTYPE, GROSSLENGTH);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region D365

        #region D365_PK_TOTALHEADER
        private bool D365_PK_TOTALHEADER()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_TOTALHEADERData> results = new List<ListD365_PK_TOTALHEADERData>();

                results = D365DataService.Instance.D365_PK_TOTALHEADER(_palletNO);

                if (results.Count > 0)
                {
                    int i = 0;

                    string P_PALLETNO = string.Empty;
                    string P_ITEMCODE = string.Empty;
                    string P_LOADINGTYPE = string.Empty;

                    foreach (var row in results)
                    {
                        if (!string.IsNullOrEmpty(results[i].PALLETNO))
                            P_PALLETNO = results[i].PALLETNO;
                        else
                            P_PALLETNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMCODE))
                            P_ITEMCODE = results[i].ITEMCODE;
                        else
                            P_ITEMCODE = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

                        if (!string.IsNullOrEmpty(P_PALLETNO) && !string.IsNullOrEmpty(P_ITEMCODE) && !string.IsNullOrEmpty(P_LOADINGTYPE))
                        {
                            PRODID = null;
                            HEADERID = null;

                            _LOTNO = string.Empty;
                            _ITEMID = string.Empty;
                            _LOADINGTYPE = string.Empty;

                            if (D365_PK_BPO_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                            {
                                if (PRODID != null)
                                {
                                    if (D365_PK_ISH(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_PK_ISL_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                            {
                                                if (D365_PK_OUH_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                                {
                                                    if (D365_PK_OUL_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                                    {
                                                        chkD365 = true;
                                                    }
                                                    else
                                                    {
                                                        chkD365 = false;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    chkD365 = false;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                chkD365 = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            "HEADERID is null".Info();
                                            chkD365 = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        chkD365 = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    "PRODID is null".Info();
                                    chkD365 = false;
                                    break;
                                }
                            }
                            else
                            {
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_PK_TOTALHEADER Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_PK_BPO_C
        private bool D365_PK_BPO_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_BPO_CData> results = new List<ListD365_PK_BPO_CData>();

                results = D365DataService.Instance.D365_PK_BPO_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                            PRODID = Convert.ToInt64(results[i].PRODID);
                        else
                            PRODID = null;

                        if (!string.IsNullOrEmpty(results[i].LOTNO))
                            _LOTNO = results[i].LOTNO;
                        else
                            _LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            _ITEMID = results[i].ITEMID;
                        else
                            _ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            _LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            _LOADINGTYPE = string.Empty;

                        if (PRODID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_PK_BPO_C Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_PK_ISH
        private bool D365_PK_ISH(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<D365_PK_ISHData> results = new List<D365_PK_ISHData>();

                results = D365DataService.Instance.D365_PK_ISH(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, _LOTNO, _ITEMID, _LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_PK_ISH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_PK_ISL_C
        private bool D365_PK_ISL_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_ISL_CData> results = new List<ListD365_PK_ISL_CData>();

                results = D365DataService.Instance.D365_PK_ISL_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string issDate = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].ISSUEDATE != null)
                            issDate = results[i].ISSUEDATE.Value.ToString("yyyy-MM-dd");
                        else
                            issDate = string.Empty;

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, "N", 0, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_PK_ISL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_PK_OUH_C
        private bool D365_PK_OUH_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<D365_PK_OUH_CData> results = new List<D365_PK_OUH_CData>();

                results = D365DataService.Instance.D365_PK_OUH_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, _LOTNO, results[i].ITEMID, _LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_PK_OUH_C Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_PK_OUL_C
        private bool D365_PK_OUL_C(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_OUL_CData> results = new List<ListD365_PK_OUL_CData>();

                results = D365DataService.Instance.D365_PK_OUL_C(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string outputDate = string.Empty;
                    int? finish = null;

                    foreach (var row in results)
                    {
                        if (results[i].OUTPUTDATE != null)
                            outputDate = results[i].OUTPUTDATE.Value.ToString("yyyy-MM-dd");
                        else
                            outputDate = string.Empty;

                        if (results[i].FINISH != null)
                            finish = Convert.ToInt32(results[i].FINISH);
                        else
                            finish = 0;

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);


                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_PK_OUL_C Row = 0".Info();
                }

                return chkD365;
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
