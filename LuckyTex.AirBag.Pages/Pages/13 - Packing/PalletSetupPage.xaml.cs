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
    /// Interaction logic for PalletSetupPage.xaml
    /// </summary>
    public partial class PalletSetupPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PalletSetupPage()
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

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdCreatePallet_Click

        private void cmdCreatePallet_Click(object sender, RoutedEventArgs e)
        {
            CreatePallet();
        }

        #endregion

        #endregion

        #region txtInspectionLot_KeyDown

        private void txtInspectionLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtInspectionLot.Text != "")
                {
                    LoadPackingINSLOT();
                    e.Handled = true;
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

        #region gridPallet_SelectedCellsChanged

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
                            if (((LuckyTex.Models.PalletData)(gridPallet.CurrentCell.Item)).RowNo != 0)
                            {
                                RowNo = ((LuckyTex.Models.PalletData)(gridPallet.CurrentCell.Item)).RowNo;
                            }

                            if (((LuckyTex.Models.PalletData)(gridPallet.CurrentCell.Item)).INSPECTIONLOT != null)
                            {
                                INSPECTIONLOT = ((LuckyTex.Models.PalletData)(gridPallet.CurrentCell.Item)).INSPECTIONLOT;
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

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            txtInspectionLot.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPallet.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPallet.SelectedItems.Clear();
            else
                this.gridPallet.SelectedItem = null;

            gridPallet.ItemsSource = null;

            _palletNO = string.Empty;
            PRODID = null;
            HEADERID = null;

            _LOTNO = string.Empty;
            _ITEMID = string.Empty;
            _LOADINGTYPE = string.Empty;

            txtInspectionLot.SelectAll();
            txtInspectionLot.Focus();
        }

        #endregion

        #region LoadPackingINSLOT
        private void LoadPackingINSLOT()
        {
            string _INSLOT = string.Empty;
            string _InspectionDate = string.Empty;
            string _ItemCode = string.Empty;
            string _Grade = string.Empty;


            if (!string.IsNullOrEmpty(txtInspectionLot.Text))
            {
                if (txtInspectionLot.Text.Length <= 9)
                    _INSLOT = txtInspectionLot.Text;
                else
                    _INSLOT = txtInspectionLot.Text.Substring(txtInspectionLot.Text.Length - 9, 9);
            }

            List<PACK_SEARCHINSPECTIONDATA> lots = new List<PACK_SEARCHINSPECTIONDATA>();

            lots = PackingDataService.Instance.Pack_SearchDataList(_InspectionDate, _Grade, _ItemCode, _INSLOT);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                DateTime? _STARTDATE = DateTime.Now;
                string _INSPECTIONLOT = string.Empty;
                string _ITEMCODE = string.Empty;
                string _ITM_GROUP = string.Empty;
                string _CUSTOMERTYPE = string.Empty;
                string _GRADE = string.Empty;
                string _LOADINGTYPE = string.Empty;
                decimal? _NETWEIGHT = 0;
                decimal? _GROSSWEIGHT = 0;
                decimal? _GROSSLENGTH = 0;
                decimal? _NETLENGTH = 0;

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
                    if (AddPalletDetail(_STARTDATE, _INSPECTIONLOT, _ITEMCODE, _CUSTOMERTYPE, _GRADE,
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

        }
        #endregion

        #region AddPalletDetail
        private bool AddPalletDetail(DateTime? _STARTDATE, string _INSPECTIONLOT, string _ITEMCODE, string _CUSTOMERTYPE, string _GRADE,
                 decimal? _NETWEIGHT, decimal? _GROSSWEIGHT, decimal? _NETLENGTH, string _ITM_GROUP, string _LOADINGTYPE, decimal? _GROSSLENGTH)
        {
            bool chkAddData = true;
            bool chkInsLot = true;
            string FirstGRADE = string.Empty;
            string FirstITM_GROUP = string.Empty;
            string FirstCUSTOMERTYPE = string.Empty;
            string FirstLOADINGTYPE = string.Empty;

            List<LuckyTex.Models.PalletData> dataList = new List<LuckyTex.Models.PalletData>();
            int o = 0;
            foreach (var row in gridPallet.Items)
            {
                LuckyTex.Models.PalletData dataItem = new LuckyTex.Models.PalletData();

                dataItem.RowNo = o + 1;
                dataItem.INSPECTIONLOT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).INSPECTIONLOT;

                if (dataItem.INSPECTIONLOT == _INSPECTIONLOT)
                    chkInsLot = false;

                dataItem.STARTDATE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).STARTDATE;
                dataItem.ITEMCODE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITEMCODE;
                dataItem.ITM_GROUP = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITM_GROUP;
                dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).CUSTOMERTYPE;
                dataItem.GRADE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GRADE;
                dataItem.NETLENGTH = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).NETLENGTH;
                dataItem.GROSSWEIGHT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GROSSWEIGHT;
                dataItem.GROSSLENGTH = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GROSSLENGTH;
                dataItem.NETWEIGHT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).NETWEIGHT;
                dataItem.LOADINGTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).LOADINGTYPE;

                if (o == 0)
                {
                    FirstGRADE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GRADE;
                    FirstITM_GROUP = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITM_GROUP;
                    FirstCUSTOMERTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).CUSTOMERTYPE;
                    FirstLOADINGTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).LOADINGTYPE;
                }

                o++;
                dataList.Add(dataItem);
            }

            if (chkInsLot == true)
            {

                if (FirstGRADE == "" && FirstITM_GROUP == "" && FirstLOADINGTYPE == "")
                {
                    LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                    dataItemNew.RowNo = gridPallet.Items.Count + 1;

                    dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                    dataItemNew.STARTDATE = _STARTDATE;
                    dataItemNew.ITEMCODE = _ITEMCODE;
                    dataItemNew.ITM_GROUP = _ITM_GROUP;
                    dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                    dataItemNew.GRADE = _GRADE;
                    dataItemNew.NETWEIGHT = _NETWEIGHT;
                    dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                    dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                    dataItemNew.NETLENGTH = _NETLENGTH;
                    dataItemNew.LOADINGTYPE = _LOADINGTYPE;

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
                                        LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                        dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                        dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                        dataItemNew.STARTDATE = _STARTDATE;
                                        dataItemNew.ITEMCODE = _ITEMCODE;
                                        dataItemNew.ITM_GROUP = _ITM_GROUP;
                                        dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                        dataItemNew.GRADE = _GRADE;
                                        dataItemNew.NETWEIGHT = _NETWEIGHT;
                                        dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                        dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                        dataItemNew.NETLENGTH = _NETLENGTH;
                                        dataItemNew.LOADINGTYPE = _LOADINGTYPE;

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
                                        LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                        dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                        dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                        dataItemNew.STARTDATE = _STARTDATE;
                                        dataItemNew.ITEMCODE = _ITEMCODE;
                                        dataItemNew.ITM_GROUP = _ITM_GROUP;
                                        dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                        dataItemNew.GRADE = _GRADE;
                                        dataItemNew.NETWEIGHT = _NETWEIGHT;
                                        dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                        dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                        dataItemNew.NETLENGTH = _NETLENGTH;
                                        dataItemNew.LOADINGTYPE = _LOADINGTYPE;

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
                        else if (FirstGRADE == "T")
                        {
                            #region FirstGRADE = T

                            if (_GRADE == FirstGRADE)
                            {
                                if (_ITM_GROUP == FirstITM_GROUP)
                                {
                                    if (_CUSTOMERTYPE == FirstCUSTOMERTYPE)
                                    {
                                        LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                        dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                        dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                        dataItemNew.STARTDATE = _STARTDATE;
                                        dataItemNew.ITEMCODE = _ITEMCODE;
                                        dataItemNew.ITM_GROUP = _ITM_GROUP;
                                        dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                        dataItemNew.GRADE = _GRADE;
                                        dataItemNew.NETWEIGHT = _NETWEIGHT;
                                        dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                        dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                        dataItemNew.NETLENGTH = _NETLENGTH;
                                        dataItemNew.LOADINGTYPE = _LOADINGTYPE;

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
                                    LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                    dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                    dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                    dataItemNew.STARTDATE = _STARTDATE;
                                    dataItemNew.ITEMCODE = _ITEMCODE;
                                    dataItemNew.ITM_GROUP = _ITM_GROUP;
                                    dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                    dataItemNew.GRADE = _GRADE;
                                    dataItemNew.NETWEIGHT = _NETWEIGHT;
                                    dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                    dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                    dataItemNew.NETLENGTH = _NETLENGTH;
                                    dataItemNew.LOADINGTYPE = _LOADINGTYPE;

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
                        else if (FirstGRADE == "X")
                        {
                            #region FirstGRADE = X

                            if (_GRADE == FirstGRADE)
                            {
                                LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                dataItemNew.STARTDATE = _STARTDATE;
                                dataItemNew.ITEMCODE = _ITEMCODE;
                                dataItemNew.ITM_GROUP = _ITM_GROUP;
                                dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                dataItemNew.GRADE = _GRADE;
                                dataItemNew.NETWEIGHT = _NETWEIGHT;
                                dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                dataItemNew.NETLENGTH = _NETLENGTH;
                                dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                                dataList.Add(dataItemNew);
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
                                LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                dataItemNew.STARTDATE = _STARTDATE;
                                dataItemNew.ITEMCODE = _ITEMCODE;
                                dataItemNew.ITM_GROUP = _ITM_GROUP;
                                dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                dataItemNew.GRADE = _GRADE;
                                dataItemNew.NETWEIGHT = _NETWEIGHT;
                                dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                dataItemNew.NETLENGTH = _NETLENGTH;
                                dataItemNew.LOADINGTYPE = _LOADINGTYPE;

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
                        else if (FirstGRADE == "X")
                        {
                            #region FirstGRADE = X

                            if (_GRADE == FirstGRADE)
                            {
                                LuckyTex.Models.PalletData dataItemNew = new LuckyTex.Models.PalletData();

                                dataItemNew.RowNo = gridPallet.Items.Count + 1;

                                dataItemNew.INSPECTIONLOT = _INSPECTIONLOT;
                                dataItemNew.STARTDATE = _STARTDATE;
                                dataItemNew.ITEMCODE = _ITEMCODE;
                                dataItemNew.ITM_GROUP = _ITM_GROUP;
                                dataItemNew.CUSTOMERTYPE = _CUSTOMERTYPE;
                                dataItemNew.GRADE = _GRADE;
                                dataItemNew.NETWEIGHT = _NETWEIGHT;
                                dataItemNew.GROSSWEIGHT = _GROSSWEIGHT;
                                dataItemNew.GROSSLENGTH = _GROSSLENGTH;
                                dataItemNew.NETLENGTH = _NETLENGTH;
                                dataItemNew.LOADINGTYPE = _LOADINGTYPE;

                                dataList.Add(dataItemNew);
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

        #region Remove
        private void Remove(int RowNo, string INSPECTIONLOT)
        {
            if (gridPallet.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.PalletData> dataList = new List<LuckyTex.Models.PalletData>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridPallet.Items)
                        {
                            LuckyTex.Models.PalletData dataItem = new LuckyTex.Models.PalletData();

                            if (((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).RowNo == RowNo
                                && ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).INSPECTIONLOT == INSPECTIONLOT)
                            {

                                dataItem.INSPECTIONLOT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).INSPECTIONLOT;
                                dataItem.STARTDATE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).STARTDATE;
                                dataItem.ITEMCODE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITEMCODE;
                                dataItem.ITM_GROUP = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITM_GROUP;
                                dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).CUSTOMERTYPE;
                                dataItem.GRADE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GRADE;
                                dataItem.NETLENGTH = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).NETLENGTH;
                                dataItem.GROSSWEIGHT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GROSSWEIGHT;
                                dataItem.GROSSLENGTH = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GROSSLENGTH;
                                dataItem.NETWEIGHT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).NETWEIGHT;
                                dataItem.LOADINGTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).LOADINGTYPE;

                                dataList.Remove(dataItem);
                            }
                            else
                            {
                                dataItem.RowNo = i + 1;

                                dataItem.INSPECTIONLOT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).INSPECTIONLOT;
                                dataItem.STARTDATE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).STARTDATE;
                                dataItem.ITEMCODE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITEMCODE;
                                dataItem.ITM_GROUP = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).ITM_GROUP;
                                dataItem.CUSTOMERTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).CUSTOMERTYPE;
                                dataItem.GRADE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GRADE;
                                dataItem.NETLENGTH = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).NETLENGTH;
                                dataItem.GROSSWEIGHT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GROSSWEIGHT;
                                dataItem.GROSSLENGTH = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).GROSSLENGTH;
                                dataItem.NETWEIGHT = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).NETWEIGHT;
                                dataItem.LOADINGTYPE = ((LuckyTex.Models.PalletData)((gridPallet.Items)[o])).LOADINGTYPE;

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

        #region CreatePallet

        private void CreatePallet()
        {
            try
            {
                DateTime? STARTDATE = null;
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

                #region foreach gridPallet
                foreach (object obj in gridPallet.ItemsSource)
                {
                    if (((LuckyTex.Models.PalletData)(obj)).INSPECTIONLOT != null)
                    {
                        STARTDATE = null;
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

                        INSPECTIONLOT = ((LuckyTex.Models.PalletData)(obj)).INSPECTIONLOT;
                        STARTDATE = ((LuckyTex.Models.PalletData)(obj)).STARTDATE;
                        ITEMCODE = ((LuckyTex.Models.PalletData)(obj)).ITEMCODE;
                        CUSTOMERTYPE = ((LuckyTex.Models.PalletData)(obj)).CUSTOMERTYPE;
                        GRADE = ((LuckyTex.Models.PalletData)(obj)).GRADE;

                        NETLENGTH = ((LuckyTex.Models.PalletData)(obj)).NETLENGTH;
                        NETWEIGHT = ((LuckyTex.Models.PalletData)(obj)).NETWEIGHT;
                        GROSSLENGTH = ((LuckyTex.Models.PalletData)(obj)).GROSSLENGTH;
                        GROSSWEIGHT = ((LuckyTex.Models.PalletData)(obj)).GROSSWEIGHT;

                        LOADTYPE = ((LuckyTex.Models.PalletData)(obj)).LOADINGTYPE;

                        ORDERNO = ((LuckyTex.Models.PalletData)(obj)).RowNo;

                        if (UpdateInspectionProcess(INSPECTIONLOT, STARTDATE) == false)
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
                                if (PACK_INSPACKINGPALLETDETAIL(PALLETNO, ORDERNO, INSPECTIONLOT, ITEMCODE, GRADE, NETLENGTH, NETWEIGHT, GROSSWEIGHT, CUSTOMERTYPE, STARTDATE, LOADTYPE, GROSSLENGTH) == true)
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
                #endregion

                if (!string.IsNullOrEmpty(PALLETNO))
                {
                    D365_PK(PALLETNO);
                }

                //if (MsgPALLETNO != "")
                //    MsgPALLETNO.ShowMessageBox(false);

                if (MsgPALLETNO != "")
                {
                    if (MessageBox.Show(MsgPALLETNO + "\r\n" + "Do you want to Print", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Print(PALLETNO);
                    }
                }

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
        private bool PACK_INSPACKINGPALLETDETAIL(string PALLETNO,decimal? ORDERNO, string insLotNo, string ITMCODE, string GRADE
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

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string PALLETNO)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PalletList";
                ConmonReportService.Instance.PALLETNO = PALLETNO;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();

                    if (ConmonReportService.Instance.GRADE == "C" || ConmonReportService.Instance.GRADE == "X")
                    {
                        DataControl.ClassData.Report rep = new DataControl.ClassData.Report();

                        ConmonReportService.Instance.RowLast = 0;
                    }
                    else
                    {
                        DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                        rep.PrintByPrinter(dp.ToString().Trim());

                        ConmonReportService.Instance.RowLast = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void D365_PK(string PALLETNO)
        {
            try
            {
                _palletNO = PALLETNO;

                if (!string.IsNullOrEmpty(_palletNO))
                {
                    if (D365_PK_TOTALHEADER() == true)
                    {
                        "Send D365 complete".Info();
                    }
                }
                else
                {
                    "Pallet No is null".Info();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }

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

                            if (D365_PK_BPO(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                            {
                                if (PRODID != null)
                                {
                                    if (D365_PK_ISH(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_PK_ISL(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                            {
                                                if (D365_PK_OUH(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
                                                {
                                                    if (D365_PK_OUL(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE) == true)
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

        #region D365_PK_BPO
        private bool D365_PK_BPO(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_BPOData> results = new List<ListD365_PK_BPOData>();

                results = D365DataService.Instance.D365_PK_BPO(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

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
                    "D365_PK_BPO Row = 0".Info();
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

        #region D365_PK_ISL
        private bool D365_PK_ISL(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_ISLData> results = new List<ListD365_PK_ISLData>();

                results = D365DataService.Instance.D365_PK_ISL(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

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

        #region D365_PK_OUH
        private bool D365_PK_OUH(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<D365_PK_OUHData> results = new List<D365_PK_OUHData>();

                results = D365DataService.Instance.D365_PK_OUH(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

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
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, _LOTNO, _ITEMID, _LOADINGTYPE);

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
                    "D365_PK_OUH Row = 0".Info();
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

        #region D365_PK_OUL
        private bool D365_PK_OUL(string P_PALLETNO, string P_ITEMCODE, string P_LOADINGTYPE)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_PK_OULData> results = new List<ListD365_PK_OULData>();

                results = D365DataService.Instance.D365_PK_OUL(P_PALLETNO, P_ITEMCODE, P_LOADINGTYPE);

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
                    "D365_PK_OUL Row = 0".Info();
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
