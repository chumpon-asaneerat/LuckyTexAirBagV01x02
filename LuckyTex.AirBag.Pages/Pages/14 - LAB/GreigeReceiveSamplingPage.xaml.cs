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
    /// Interaction logic for GreigeReceiveSamplingPage.xaml
    /// </summary>
    public partial class GreigeReceiveSamplingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GreigeReceiveSamplingPage()
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

        string gridBEAMERROLL = string.Empty;
        string gridLOOMNO = string.Empty;
        string gridITM_WEAVING = string.Empty;

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

        #region txtBEAMERROLL_KeyDown

        private void txtBEAMERROLL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtBEAMERROLL.Text))
                {
                    txtLOOMNO.SelectAll();
                    txtLOOMNO.Focus();
                }

                e.Handled = true;
            }
        }

        #endregion

        #region txtLOOMNO_KeyDown

        private void txtLOOMNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtLOOMNO.Text) && !string.IsNullOrEmpty(txtBEAMERROLL.Text) && dteReceiveDate.SelectedDate != null)
                {
                    LAB_CHECKRECEIVEGREIGESAMPLING(txtBEAMERROLL.Text, txtLOOMNO.Text);
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
                    if (RowNo != 0 && !string.IsNullOrEmpty(gridBEAMERROLL) && !string.IsNullOrEmpty(gridLOOMNO) && !string.IsNullOrEmpty(gridITM_WEAVING))
                        Remove(RowNo, gridBEAMERROLL, gridLOOMNO, gridITM_WEAVING);
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
                            //if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).RowNo != 0)
                            //{
                            RowNo = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).RowNo;
                            //}

                            if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).BEAMERROLL != null)
                            {
                                gridBEAMERROLL = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).BEAMERROLL;
                            }

                            if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).LOOMNO != null)
                            {
                                gridLOOMNO = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).LOOMNO;
                            }

                            if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).ITM_WEAVING != null)
                            {
                                gridITM_WEAVING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(gridLAB.CurrentCell.Item)).ITM_WEAVING;
                            }
                        }
                    }
                }
                else
                {
                    gridBEAMERROLL = string.Empty;
                    gridLOOMNO = string.Empty;
                    gridITM_WEAVING = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                gridBEAMERROLL = string.Empty;
                gridLOOMNO = string.Empty;
                gridITM_WEAVING = string.Empty;
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
            txtBEAMERROLL.Text = "";
            txtLOOMNO.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLAB.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLAB.SelectedItems.Clear();
            else
                this.gridLAB.SelectedItem = null;

            gridLAB.ItemsSource = null;

            gridBEAMERROLL = string.Empty;
            gridLOOMNO = string.Empty;
            gridITM_WEAVING = string.Empty;

            txtBEAMERROLL.SelectAll();
            txtBEAMERROLL.Focus();
        }

        #endregion

        #region LAB_CHECKRECEIVEGREIGESAMPLING

        private void LAB_CHECKRECEIVEGREIGESAMPLING(string P_BEAMERROLL, string P_LOOMNO)
        {
            string results = string.Empty;
            results = LABDataService.Instance.LAB_CHECKRECEIVEGREIGESAMPLING(P_BEAMERROLL, P_LOOMNO);

            if (!string.IsNullOrEmpty(results))
            {
                results.ShowMessageBox(true);
                txtBEAMERROLL.Text = "";
                txtLOOMNO.Text = "";

                txtBEAMERROLL.SelectAll();
                txtBEAMERROLL.Focus();
            }
            else
            {
                if (ChkBEAMERROLL(P_BEAMERROLL, P_LOOMNO) == true)
                {
                    AddWEAVINGSAMPLING(P_BEAMERROLL, P_LOOMNO);
                }
                else
                {
                    "Receive Sampling had in Data".ShowMessageBox(false);
                }

                txtBEAMERROLL.Text = "";
                txtLOOMNO.Text = "";

                txtBEAMERROLL.SelectAll();
                txtBEAMERROLL.Focus();
            }
        }

        #endregion

        #region AddWEAVINGSAMPLING

        private void AddWEAVINGSAMPLING(string P_BEAMERROLL, string P_LOOMNO)
        {
            List<LAB_GETWEAVINGSAMPLING> dbResults = new List<LAB_GETWEAVINGSAMPLING>();

            dbResults = LABDataService.Instance.LAB_GETWEAVINGSAMPLING(P_BEAMERROLL, P_LOOMNO);

            if (dbResults != null && dbResults.Count > 0)
            {
                string BEAMERROLL = string.Empty;
                string LOOMNO = string.Empty;
                decimal? RECUTSAMPLING = null;
                string ITM_WEAVING = string.Empty;
                string P_OPERATOR = txtOperator.Text;
                DateTime? ReceiveDate = null;

                if (dteReceiveDate.SelectedDate != null)
                    ReceiveDate = dteReceiveDate.SelectedDate;

                 int i = 0;

                 foreach (var row in dbResults)
                 {
                     BEAMERROLL = string.Empty;
                     LOOMNO = string.Empty;
                     RECUTSAMPLING = null;
                     ITM_WEAVING = string.Empty;

                     BEAMERROLL = dbResults[i].BEAMERROLL;
                     LOOMNO = dbResults[i].LOOMNO;
                     RECUTSAMPLING = dbResults[i].RECUTSAMPLING;
                     ITM_WEAVING = dbResults[i].ITM_WEAVING;

                     if (!string.IsNullOrEmpty(BEAMERROLL) && !string.IsNullOrEmpty(LOOMNO))
                         AddLabSamplingDetail(BEAMERROLL, LOOMNO, RECUTSAMPLING, ITM_WEAVING, ReceiveDate, P_OPERATOR);

                     i++;
                 }
            }
        }

        #endregion

        #region AddLabSamplingDetail

        private bool AddLabSamplingDetail(string BEAMERROLL, string LOOMNO, decimal? RECUTSAMPLING,string ITM_WEAVING, DateTime? ReceiveDate, string P_OPERATOR)
        {
            try
            {
                List<LuckyTex.Models.LAB_GETWEAVINGSAMPLING> dataList = new List<LuckyTex.Models.LAB_GETWEAVINGSAMPLING>();
                int o = 0;
                foreach (var row in gridLAB.Items)
                {
                    LuckyTex.Models.LAB_GETWEAVINGSAMPLING dataItem = new LuckyTex.Models.LAB_GETWEAVINGSAMPLING();

                    dataItem.RowNo = o + 1;

                    dataItem.BEAMERROLL = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).BEAMERROLL;
                    dataItem.LOOMNO = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).LOOMNO;
                    dataItem.RECUTSAMPLING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).RECUTSAMPLING;
                    dataItem.ITM_WEAVING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ITM_WEAVING;
                    dataItem.ReceiveDate = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ReceiveDate;

                    o++;

                    dataList.Add(dataItem);
                }

                LuckyTex.Models.LAB_GETWEAVINGSAMPLING dataItemNew = new LuckyTex.Models.LAB_GETWEAVINGSAMPLING();

                dataItemNew.RowNo = gridLAB.Items.Count + 1;

                dataItemNew.BEAMERROLL = BEAMERROLL;
                dataItemNew.LOOMNO = LOOMNO;
                dataItemNew.RECUTSAMPLING = RECUTSAMPLING;
                dataItemNew.ITM_WEAVING = ITM_WEAVING;
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
        private void Remove(int RowNo, string BEAMERROLL, string LOOMNO, string ITM_WEAVING)
        {
            if (gridLAB.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {

                        List<LuckyTex.Models.LAB_GETWEAVINGSAMPLING> dataList = new List<LuckyTex.Models.LAB_GETWEAVINGSAMPLING>();
                        int o = 0;
                        int i = 0;
                        foreach (var row in gridLAB.Items)
                        {
                            LuckyTex.Models.LAB_GETWEAVINGSAMPLING dataItem = new LuckyTex.Models.LAB_GETWEAVINGSAMPLING();

                            if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).RowNo == RowNo
                                && ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).BEAMERROLL == BEAMERROLL
                                && ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).LOOMNO == LOOMNO
                                && ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ITM_WEAVING == ITM_WEAVING)
                            {

                                dataItem.BEAMERROLL = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).BEAMERROLL;
                                dataItem.LOOMNO = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).LOOMNO;
                                dataItem.RECUTSAMPLING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).RECUTSAMPLING;
                                dataItem.ITM_WEAVING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ITM_WEAVING;
                                dataItem.ReceiveDate = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ReceiveDate;

                                dataList.Remove(dataItem);

                            }
                            else
                            {
                                dataItem.RowNo = i + 1;

                                dataItem.BEAMERROLL = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).BEAMERROLL;
                                dataItem.LOOMNO = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).LOOMNO;
                                dataItem.RECUTSAMPLING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).RECUTSAMPLING;
                                dataItem.ITM_WEAVING = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ITM_WEAVING;
                                dataItem.ReceiveDate = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).ReceiveDate;

                                dataList.Add(dataItem);
                                i++;
                            }
                            o++;
                        }

                        this.gridLAB.ItemsSource = dataList;

                        RowNo = 0;
                        gridBEAMERROLL = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region ChkBEAMERROLL

        private bool ChkBEAMERROLL(string P_BEAMERROLL, string P_LOOMNO)
        {
            try
            {
                bool chkWL = true;

                int o = 0;
                foreach (var row in gridLAB.Items)
                {
                    if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).BEAMERROLL == P_BEAMERROLL
                        && ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)((gridLAB.Items)[o])).LOOMNO == P_LOOMNO)
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

                    string P_BEAMERROLL = string.Empty;
                    string P_LOOMNO = string.Empty;
                    decimal? P_TESTNO = null;
                    DateTime? P_RECEIVEDATE = null;
                    string P_RECEIVEBY = string.Empty;
                    string P_STATUS = "C";

                    P_RECEIVEBY = txtOperator.Text;

                    foreach (object obj in gridLAB.Items)
                    {
                         P_BEAMERROLL = string.Empty;
                         P_LOOMNO = string.Empty;
                         P_TESTNO = null;
                         P_RECEIVEDATE = null;

                        if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).BEAMERROLL != null)
                        {
                            P_BEAMERROLL = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).BEAMERROLL;
                        }

                        if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).LOOMNO != null)
                        {
                            P_LOOMNO = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).LOOMNO;
                        }

                        if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).RECUTSAMPLING != null)
                        {
                            P_TESTNO =  2;
                        }
                        else
                        {
                            P_TESTNO = 1;
                        }

                        if (((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).ReceiveDate != null)
                        {
                            P_RECEIVEDATE = ((LuckyTex.Models.LAB_GETWEAVINGSAMPLING)(obj)).ReceiveDate;
                        }

                        if (!string.IsNullOrEmpty(P_BEAMERROLL) && !string.IsNullOrEmpty(P_LOOMNO) && P_RECEIVEDATE != null)
                        {
                            LABDataService.Instance.LAB_GreigeReceiveSampling(P_BEAMERROLL, P_LOOMNO, P_TESTNO, P_RECEIVEDATE, P_RECEIVEBY, P_STATUS);
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

