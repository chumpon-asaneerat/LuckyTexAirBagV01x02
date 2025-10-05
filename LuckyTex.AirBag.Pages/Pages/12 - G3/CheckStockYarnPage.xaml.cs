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
using System.Data;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for CheckStockYarnPage.xaml
    /// </summary>
    public partial class CheckStockYarnPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public CheckStockYarnPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadYarnType();
            LoadItemYarn();
        }

        #endregion

        #region Internal Variables

        private List<ItemYarnItem> instList = null;
        string PALLETNO = string.Empty;
        string TRACENO = string.Empty;
        string LOTNO = string.Empty;
        string ITM_YARN = string.Empty;
        decimal? CONECH = null;
        DateTime? ENTRYDATE = null;
        string YARNTYPE = string.Empty;
        decimal? WEIGHTQTY = null;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
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

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadStockYarnData();
        }

        #endregion

        #region cmdPrintGoLabel_Click
        private void cmdPrintGoLabel_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelect() == true)
            {
                AutoPrint();
            }
            else
            {
                if (!string.IsNullOrEmpty(PALLETNO))
                {
                    ConmonReportService.Instance.PALLETNO = string.Empty;
                    ConmonReportService.Instance.TRACENO = string.Empty;
                    ConmonReportService.Instance.LOTNO = string.Empty;
                    ConmonReportService.Instance.ITM_YARN = string.Empty;
                    ConmonReportService.Instance.CONECH = null;
                    ConmonReportService.Instance.ENTRYDATE = null;
                    ConmonReportService.Instance.YARNTYPE = string.Empty;
                    ConmonReportService.Instance.WEIGHTQTY = null;

                    Preview();
                }
            }
        }
        #endregion

        #region cmdDelete_Click
        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int o = 0;
                foreach (var row in gridStockYarn.Items)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).SelectData == true)
                    {
                        G3DataService.Instance.G3_Del(((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETNO, ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).TRACENO, ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ITM_YARN, ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).LOTNO);
                    }

                    o++;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #endregion

        #region Check Box

        #region chkCheckAll_Checked
        private void chkCheckAll_Checked(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }
        #endregion

        #region chkCheckAll_Unchecked
        private void chkCheckAll_Unchecked(object sender, RoutedEventArgs e)
        {
            UnSelectAll();
        }
        #endregion

        #endregion

        #region gridStockYarn_SelectedCellsChanged

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

        private void gridStockYarn_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridStockYarn.ItemsSource != null)
                {
                    PALLETNO = string.Empty;
                    TRACENO = string.Empty;
                    LOTNO = string.Empty;
                    ITM_YARN = string.Empty;
                    CONECH = null;
                    ENTRYDATE = null;
                    YARNTYPE = string.Empty;
                    WEIGHTQTY = null;

                    var row_list = GetDataGridRows(gridStockYarn);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).PALLETNO != null)
                            {
                                PALLETNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).PALLETNO;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).TRACENO != null)
                            {
                                TRACENO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).TRACENO;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).LOTNO != null)
                            {
                                LOTNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).LOTNO;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).ITM_YARN != null)
                            {
                                ITM_YARN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).ITM_YARN;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).CONECH != null)
                            {
                                CONECH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).CONECH;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).ENTRYDATE != null)
                            {
                                ENTRYDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).ENTRYDATE;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).YARNTYPE != null)
                            {
                                YARNTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).YARNTYPE;
                            }

                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).WEIGHTQTY != null)
                            {
                                WEIGHTQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(gridStockYarn.CurrentCell.Item)).WEIGHTQTY;
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

        #region private Methods

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

        #region LoadYarnType

        private void LoadYarnType()
        {
            if (cbYarnType.ItemsSource == null)
            {
                string[] str = new string[] { "All", "Warp", "Weft" };

                cbYarnType.ItemsSource = str;
                cbYarnType.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadStockYarnData

        private void LoadStockYarnData()
        {
            string _date = string.Empty;
            string _yarnType = string.Empty;
            string _itemyarn = string.Empty;

            if (dteRecDate.SelectedDate != null)
            {
                _date = dteRecDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (chkAll.IsChecked == false)
            {
                if (cbItemYarn.SelectedValue != null)
                {
                    if (cbItemYarn.SelectedValue != null)
                    {
                        _itemyarn = cbItemYarn.SelectedValue.ToString();
                    }
                }

                if (cbYarnType.SelectedValue != null)
                {
                    if (cbYarnType.SelectedValue.ToString() != "All")
                        _yarnType = cbYarnType.SelectedValue.ToString();
                }
            }

            ClearControl();

            List<G3_SEARCHYARNSTOCKData> lots = new List<G3_SEARCHYARNSTOCKData>();

            lots = G3DataService.Instance.GetG3_SEARCHYARNSTOCKData(_date,_itemyarn, _yarnType);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridStockYarn.ItemsSource = lots;

                CalTotal();
            }
            else
            {
                gridStockYarn.ItemsSource = null;

                txtTotalPallet.Text = "0";
                txtSumWeight.Text = "0";
                txtSumCH.Text = "0";
            }
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            PALLETNO = string.Empty;
            TRACENO = string.Empty;
            LOTNO = string.Empty;
            ITM_YARN = string.Empty;
            CONECH = null;
            ENTRYDATE = null;
            YARNTYPE = string.Empty;
            WEIGHTQTY = null;

            ConmonReportService.Instance.PALLETNO = string.Empty;
            ConmonReportService.Instance.TRACENO = string.Empty;
            ConmonReportService.Instance.LOTNO = string.Empty;
            ConmonReportService.Instance.ITM_YARN = string.Empty;
            ConmonReportService.Instance.CONECH = null;
            ConmonReportService.Instance.ENTRYDATE = null;
            ConmonReportService.Instance.YARNTYPE = string.Empty;
            ConmonReportService.Instance.WEIGHTQTY = null;

            chkCheckAll.IsChecked = false;

            txtTotalPallet.Text = "0";
            txtSumWeight.Text = "0";
            txtSumCH.Text = "0";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridStockYarn.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridStockYarn.SelectedItems.Clear();
            else
                this.gridStockYarn.SelectedItem = null;

            gridStockYarn.ItemsSource = null;

        }

        #endregion

        #region SelectAll

        private void SelectAll()
        {
            try
            {
                List<LuckyTex.Models.G3_SEARCHYARNSTOCKData> dataList = new List<LuckyTex.Models.G3_SEARCHYARNSTOCKData>();
                int o = 0;
                foreach (var row in gridStockYarn.Items)
                {
                    LuckyTex.Models.G3_SEARCHYARNSTOCKData dataItem = new LuckyTex.Models.G3_SEARCHYARNSTOCKData();

                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).SelectData == false)
                    {
                        dataItem.SelectData = true;
                    }
                    else
                    {
                        dataItem.SelectData = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).SelectData;
                    }

                    dataItem.ENTRYDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ENTRYDATE;
                    dataItem.ITM_YARN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ITM_YARN;
                    dataItem.PALLETNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETNO;
                    dataItem.YARNTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).YARNTYPE;
                    dataItem.WEIGHTQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).WEIGHTQTY;
                    dataItem.CONECH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CONECH;
                    dataItem.VERIFY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).VERIFY;
                    dataItem.REMAINQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).REMAINQTY;
                    dataItem.RECEIVEBY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).RECEIVEBY;
                    dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).RECEIVEDATE;
                    dataItem.FINISHFLAG = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).FINISHFLAG;
                    dataItem.UPDATEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).UPDATEDATE;
                    dataItem.PALLETTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETTYPE;
                    dataItem.ITM400 = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ITM400;
                    dataItem.UM = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).UM;
                    dataItem.PACKAING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PACKAING;
                    dataItem.CLEAN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CLEAN;
                    dataItem.TEARING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).TEARING;
                    dataItem.FALLDOWN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).FALLDOWN;
                    dataItem.CERTIFICATION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CERTIFICATION;
                    dataItem.INVOICE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).INVOICE;
                    dataItem.IDENTIFYAREA = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).IDENTIFYAREA;
                    dataItem.AMOUNTPALLET = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).AMOUNTPALLET;
                    dataItem.OTHER = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).OTHER;
                    dataItem.ACTION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ACTION;
                    dataItem.MOVEMENTDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).MOVEMENTDATE;
                    dataItem.LOTNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).LOTNO;
                    dataItem.TRACENO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).TRACENO;

                    o++;

                    dataList.Add(dataItem);
                }

                this.gridStockYarn.ItemsSource = dataList;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region UnSelectAll

        private void UnSelectAll()
        {
            try
            {
                List<LuckyTex.Models.G3_SEARCHYARNSTOCKData> dataList = new List<LuckyTex.Models.G3_SEARCHYARNSTOCKData>();
                int o = 0;
                foreach (var row in gridStockYarn.Items)
                {
                    LuckyTex.Models.G3_SEARCHYARNSTOCKData dataItem = new LuckyTex.Models.G3_SEARCHYARNSTOCKData();

                    dataItem.SelectData = false;

                    dataItem.ENTRYDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ENTRYDATE;
                    dataItem.ITM_YARN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ITM_YARN;
                    dataItem.PALLETNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETNO;
                    dataItem.YARNTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).YARNTYPE;
                    dataItem.WEIGHTQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).WEIGHTQTY;
                    dataItem.CONECH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CONECH;
                    dataItem.VERIFY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).VERIFY;
                    dataItem.REMAINQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).REMAINQTY;
                    dataItem.RECEIVEBY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).RECEIVEBY;
                    dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).RECEIVEDATE;
                    dataItem.FINISHFLAG = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).FINISHFLAG;
                    dataItem.UPDATEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).UPDATEDATE;
                    dataItem.PALLETTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETTYPE;
                    dataItem.ITM400 = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ITM400;
                    dataItem.UM = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).UM;
                    dataItem.PACKAING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PACKAING;
                    dataItem.CLEAN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CLEAN;
                    dataItem.TEARING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).TEARING;
                    dataItem.FALLDOWN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).FALLDOWN;
                    dataItem.CERTIFICATION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CERTIFICATION;
                    dataItem.INVOICE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).INVOICE;
                    dataItem.IDENTIFYAREA = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).IDENTIFYAREA;
                    dataItem.AMOUNTPALLET = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).AMOUNTPALLET;
                    dataItem.OTHER = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).OTHER;
                    dataItem.ACTION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ACTION;
                    dataItem.MOVEMENTDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).MOVEMENTDATE;
                    dataItem.LOTNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).LOTNO;
                    dataItem.TRACENO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).TRACENO;

                    o++;

                    dataList.Add(dataItem);
                }

                this.gridStockYarn.ItemsSource = dataList;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region CheckSelect
        private bool CheckSelect()
        {
            try
            {
                bool status = false;

                int o = 0;
                foreach (var row in gridStockYarn.Items)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).SelectData == true)
                    {
                        status = true;
                        break;
                    }
                    o++;
                }

                return status;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region AutoPrint
        private void AutoPrint()
        {
            try
            {
                int o = 0;
                foreach (var row in gridStockYarn.Items)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).SelectData == true)
                    {
                        ConmonReportService.Instance.PALLETNO = string.Empty;
                        ConmonReportService.Instance.TRACENO = string.Empty;
                        ConmonReportService.Instance.LOTNO = string.Empty;
                        ConmonReportService.Instance.ITM_YARN = string.Empty;
                        ConmonReportService.Instance.CONECH = null;
                        ConmonReportService.Instance.ENTRYDATE = null;
                        ConmonReportService.Instance.YARNTYPE = string.Empty;
                        ConmonReportService.Instance.WEIGHTQTY = null;

                      Print(((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETNO
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).TRACENO
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).LOTNO
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ITM_YARN
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CONECH
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).ENTRYDATE
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).YARNTYPE
                          , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).WEIGHTQTY);
                    }

                    o++;
                }
            }
            catch (Exception ex)
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

                foreach (var row in gridStockYarn.Items)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).PALLETNO != null)
                    {
                        pallet++;
                    }

                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).WEIGHTQTY != null)
                    {
                        weight += ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).WEIGHTQTY.Value;
                    }

                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CONECH != null)
                    {
                        conech += ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridStockYarn.Items)[o])).CONECH.Value;
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

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string PALLETNO ,string TRACENO ,string LOTNO ,
        string ITM_YARN, decimal? CONECH, DateTime? ENTRYDATE, string YARNTYPE, decimal? WEIGHTQTY)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "G3_GoLabel";

                ConmonReportService.Instance.PALLETNO = PALLETNO;
                ConmonReportService.Instance.TRACENO = TRACENO;
                ConmonReportService.Instance.LOTNO = LOTNO;
                ConmonReportService.Instance.ITM_YARN = ITM_YARN;
                ConmonReportService.Instance.CONECH = CONECH;
                ConmonReportService.Instance.ENTRYDATE = ENTRYDATE;
                ConmonReportService.Instance.YARNTYPE = YARNTYPE;
                ConmonReportService.Instance.WEIGHTQTY = WEIGHTQTY;

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

        private void Preview()
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "G3_GoLabel";

                ConmonReportService.Instance.PALLETNO = PALLETNO;
                ConmonReportService.Instance.TRACENO = TRACENO;
                ConmonReportService.Instance.LOTNO = LOTNO;
                ConmonReportService.Instance.ITM_YARN = ITM_YARN;
                ConmonReportService.Instance.CONECH = CONECH;
                ConmonReportService.Instance.ENTRYDATE = ENTRYDATE;
                ConmonReportService.Instance.YARNTYPE = YARNTYPE;
                ConmonReportService.Instance.WEIGHTQTY = WEIGHTQTY;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
