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
    /// Interaction logic for ReceiveSamplingPage.xaml
    /// </summary>
    public partial class ReceiveSamplingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveSamplingPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            Status = new List<string> { "Pass", "Fail" };
        }

        #endregion

        #region Internal Variables

        private LABSession _session = new LABSession();
        string opera = string.Empty;
        
        // DataGrid List<String> Example
        public List<string> Status { get; set; }

        int RowNo = 0;

        string WEAVINGLOT = string.Empty;
        string FINISHINGLOT = string.Empty;
        string ITM_CODE = string.Empty;

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

        #region cmdReceive_Click

        private void cmdReceive_Click(object sender, RoutedEventArgs e)
        {
            if (Receive() == true)
            {
                "Receive All Sampling and Start Conditioning".ShowMessageBox(false);

                ClearControl();
            }
            else
            {
                "Can't Receive".ShowMessageBox(true);
            }
        }

        #endregion

        #endregion

        #region TextBox

        #region txtWEAVLOT_KeyDown

        private void txtWEAVLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtWEAVLOT.Text))
                {
                    txtITEMCODE.SelectAll();
                    txtITEMCODE.Focus();
                }

                e.Handled = true;
            }
        }

        #endregion

        #region txtITEMCODE_KeyDown

        private void txtITEMCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtITEMCODE.Text) && !string.IsNullOrEmpty(txtWEAVLOT.Text) && dteReceiveDate.SelectedDate != null)
                {
                    LAB_CHECKRECEIVESAMPLING(txtWEAVLOT.Text, txtITEMCODE.Text);
                }

                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region gridLAB_SelectedCellsChanged

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

        private void gridLAB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Delete))
            {
                Type type = this.GetType();
                try
                {
                    if (RowNo != 0 && !string.IsNullOrEmpty(WEAVINGLOT) && !string.IsNullOrEmpty(FINISHINGLOT) && !string.IsNullOrEmpty(ITM_CODE))
                        Remove(RowNo, WEAVINGLOT, FINISHINGLOT, ITM_CODE);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void gridLAB_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridLAB.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridLAB);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            WEAVINGLOT = string.Empty;
                            FINISHINGLOT = string.Empty;
                            ITM_CODE = string.Empty;

                            //if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).RowNo != 0)
                            //{
                                RowNo = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).RowNo;
                            //}

                            if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                WEAVINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).WEAVINGLOT;
                            }

                            if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).FINISHINGLOT != null)
                            {
                                FINISHINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).FINISHINGLOT;
                            }

                            if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).ITM_CODE != null)
                            {
                                ITM_CODE = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(gridLAB.CurrentCell.Item)).ITM_CODE;
                            }
                        }
                    }
                }
                else
                {
                    WEAVINGLOT = string.Empty;
                    FINISHINGLOT = string.Empty;
                    ITM_CODE = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            dteReceiveDate.SelectedDate = DateTime.Now;
            //dteReceiveDate.Text = "";
            txtWEAVLOT.Text = "";
            txtITEMCODE.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLAB.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLAB.SelectedItems.Clear();
            else
                this.gridLAB.SelectedItem = null;

            gridLAB.ItemsSource = null;

            WEAVINGLOT = string.Empty;
            FINISHINGLOT = string.Empty;
            ITM_CODE = string.Empty;

            txtWEAVLOT.SelectAll();
            txtWEAVLOT.Focus();
        }

        #endregion

        #region LAB_CHECKRECEIVESAMPLING

        private void LAB_CHECKRECEIVESAMPLING(string P_WEAVLOT, string P_ITEMCODE)
        {
            string results = string.Empty;

            results = LABDataService.Instance.LAB_CHECKRECEIVESAMPLING(P_WEAVLOT, P_ITEMCODE);

            if (!string.IsNullOrEmpty(results))
            {
                results.ShowMessageBox(true);
                txtWEAVLOT.Text = "";
                txtITEMCODE.Text = "";

                txtWEAVLOT.SelectAll();
                txtWEAVLOT.Focus();
            }
            else
            {
                if (ChkWEAVLOT(P_WEAVLOT, P_ITEMCODE) == true)
                {
                    AddFINISHINGSAMPLING(P_WEAVLOT, P_ITEMCODE);
                }
                else
                {
                    "Receive Sampling had in Data".ShowMessageBox(false);
                }

                txtWEAVLOT.Text = "";
                txtITEMCODE.Text = "";

                txtWEAVLOT.SelectAll();
                txtWEAVLOT.Focus();
            }
        }

        #endregion

        #region AddFINISHINGSAMPLING

        private void AddFINISHINGSAMPLING(string P_WEAVLOT, string P_ITMCODE)
        {
            List<LAB_GETFINISHINGSAMPLING> dbResults = new List<LAB_GETFINISHINGSAMPLING>();

            dbResults = LABDataService.Instance.LAB_GETFINISHINGSAMPLING(P_WEAVLOT, P_ITMCODE);

            if (dbResults != null && dbResults.Count > 0)
            {
                string WEAVINGLOT = string.Empty;
                string FINISHINGLOT = string.Empty;
                string ITM_CODE = string.Empty;
                string P_OPERATOR = txtOperator.Text;
                DateTime? ReceiveDate = null;

                if (dteReceiveDate.SelectedDate != null)
                    ReceiveDate = dteReceiveDate.SelectedDate;

                 int i = 0;

                 foreach (var row in dbResults)
                 {
                     WEAVINGLOT = string.Empty;
                     FINISHINGLOT = string.Empty;
                     ITM_CODE = string.Empty;

                     WEAVINGLOT = dbResults[i].WEAVINGLOT;
                     FINISHINGLOT = dbResults[i].FINISHINGLOT;
                     ITM_CODE = dbResults[i].ITM_CODE;

                     if (!string.IsNullOrEmpty(WEAVINGLOT) && !string.IsNullOrEmpty(FINISHINGLOT) && !string.IsNullOrEmpty(ITM_CODE))
                         AddLabSamplingDetail(WEAVINGLOT, FINISHINGLOT, ITM_CODE, ReceiveDate, P_OPERATOR);

                     i++;
                 }
            }
        }

        #endregion

        #region AddLabSamplingDetail

        private bool AddLabSamplingDetail(string WEAVINGLOT, string FINISHINGLOT, string ITM_CODE, DateTime? ReceiveDate, string P_OPERATOR)
        {
            try
            {
                List<LuckyTex.Models.LAB_GETFINISHINGSAMPLING> dataList = new List<LuckyTex.Models.LAB_GETFINISHINGSAMPLING>();
                int o = 0;
                foreach (var row in gridLAB.Items)
                {
                    LuckyTex.Models.LAB_GETFINISHINGSAMPLING dataItem = new LuckyTex.Models.LAB_GETFINISHINGSAMPLING();

                    dataItem.RowNo = o + 1;

                    dataItem.WEAVINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).WEAVINGLOT;
                    dataItem.FINISHINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).FINISHINGLOT;
                    dataItem.ITM_CODE = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ITM_CODE;
                    dataItem.ReceiveDate = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ReceiveDate;

                    o++;

                    dataList.Add(dataItem);
                }

                LuckyTex.Models.LAB_GETFINISHINGSAMPLING dataItemNew = new LuckyTex.Models.LAB_GETFINISHINGSAMPLING();

                dataItemNew.RowNo = gridLAB.Items.Count + 1;

                dataItemNew.WEAVINGLOT = WEAVINGLOT;
                dataItemNew.FINISHINGLOT = FINISHINGLOT;
                dataItemNew.ITM_CODE = ITM_CODE;
                dataItemNew.ReceiveDate = ReceiveDate;

                dataList.Add(dataItemNew);

                this.gridLAB.ItemsSource = dataList;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Remove
        private void Remove(int RowNo, string WEAVINGLOT, string FINISHINGLOT, string ITM_CODE)
        {
            if (gridLAB.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.LAB_GETFINISHINGSAMPLING> dataList = new List<LuckyTex.Models.LAB_GETFINISHINGSAMPLING>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridLAB.Items)
                        {
                            LuckyTex.Models.LAB_GETFINISHINGSAMPLING dataItem = new LuckyTex.Models.LAB_GETFINISHINGSAMPLING();

                            if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).RowNo == RowNo
                                && ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).WEAVINGLOT == WEAVINGLOT
                                && ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).FINISHINGLOT == FINISHINGLOT
                                && ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ITM_CODE == ITM_CODE)
                            {

                                dataItem.WEAVINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).WEAVINGLOT;
                                dataItem.FINISHINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).FINISHINGLOT;
                                dataItem.ITM_CODE = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ITM_CODE;
                                dataItem.ReceiveDate = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ReceiveDate;

                                dataList.Remove(dataItem);

                            }
                            else
                            {
                                dataItem.RowNo = i + 1;

                                dataItem.WEAVINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).WEAVINGLOT;
                                dataItem.FINISHINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).FINISHINGLOT;
                                dataItem.ITM_CODE = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ITM_CODE;
                                dataItem.ReceiveDate = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ReceiveDate;

                                dataList.Add(dataItem);
                                i++;
                            }
                            o++;
                        }

                        this.gridLAB.ItemsSource = dataList;

                        RowNo = 0;
                        WEAVINGLOT = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region ChkWEAVLOT

        private bool ChkWEAVLOT(string P_WEAVLOT, string P_ITEMCODE)
        {
            try
            {
                bool chkWL = true;

                int o = 0;
                foreach (var row in gridLAB.Items)
                {
                    if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).WEAVINGLOT == P_WEAVLOT
                        && ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)((gridLAB.Items)[o])).ITM_CODE == P_ITEMCODE)
                    {
                        chkWL = false;
                        break;
                    }

                    o++;
                }

                return chkWL;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Receive

        private bool Receive()
        {
            try
            {
                if (gridLAB.Items.Count > 0)
                {

                    string P_WEAVINGLOT = string.Empty;
                    string FINISHINGLOT = string.Empty;
                    string ITM_CODE = string.Empty;

                    string OPERATOR = string.Empty;
                    DateTime? ReceiveDate = null;
                    string P_STATUS = "C";

                    OPERATOR = txtOperator.Text;

                    foreach (object obj in gridLAB.Items)
                    {
                         P_WEAVINGLOT = string.Empty;
                         FINISHINGLOT = string.Empty;
                         ITM_CODE = string.Empty;
                         ReceiveDate = null;

                        if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).WEAVINGLOT != null)
                        {
                            P_WEAVINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).WEAVINGLOT;
                        }

                        if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).FINISHINGLOT != null)
                        {
                            FINISHINGLOT = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).FINISHINGLOT;
                        }

                        if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).ITM_CODE != null)
                        {
                            ITM_CODE = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).ITM_CODE;
                        }

                        if (((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).ReceiveDate != null)
                        {
                            ReceiveDate = ((LuckyTex.Models.LAB_GETFINISHINGSAMPLING)(obj)).ReceiveDate;
                        }

                        if (!string.IsNullOrEmpty(P_WEAVINGLOT) && !string.IsNullOrEmpty(FINISHINGLOT) && ReceiveDate != null)
                        {
                            LABDataService.Instance.LAB_ReceiveSampling(P_WEAVINGLOT, FINISHINGLOT, ITM_CODE, ReceiveDate, OPERATOR, P_STATUS);
                        }
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

