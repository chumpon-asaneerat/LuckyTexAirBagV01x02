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
    /// Interaction logic for SendAS400Page.xaml
    /// </summary>
    public partial class SendAS400Page : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public SendAS400Page()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadStock();
            LoadFactory();
        }

        #endregion

        #region Internal Variables

        private AS400Session _session = new AS400Session();
        string opera = string.Empty;

        string strConAS400 = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;

            ConfigManager.Instance.LoadAS400Configs();
            strConAS400 = ConfigManager.Instance.AS400Config;

            ConfigManager.Instance.LoadDefaultAS400Configs();
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
            SearchDataSendAS400();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdSelectAll_Click

        private void cmdSelectAll_Click(object sender, RoutedEventArgs e)
        {
            SelectAll();
        }

        #endregion

        #region cmdUnselectAll_Click

        private void cmdUnselectAll_Click(object sender, RoutedEventArgs e)
        {
            UnSelectAll();
        }

        #endregion

        #region cmdSend_Click

        private void cmdSend_Click(object sender, RoutedEventArgs e)
        {
            if (SendAS400() == true)
            {
                "Send Data to AS400 Stock +(send stock) + Complete".ShowMessageBox();
                ClearControl();
                SearchDataSendAS400();
            }
        }

        #endregion

        #endregion

        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtPalletNo.Text))
                    SearchDataSendAS400();
            }
        }

        #region gridAS400_SelectedCellsChanged

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

        private void gridAS400_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                //if (gridAS400.ItemsSource != null)
                //{
                //    var row_list = GetDataGridRows(gridAS400);
                //    foreach (DataGridRow single_row in row_list)
                //    {
                //        if (single_row.IsSelected == true)
                //        {
                //            if (!string.IsNullOrEmpty(((LuckyTex.Models.FG_SEARCHDATASEND400)(gridAS400.CurrentCell.Item)).ISLAB)
                //                && string.IsNullOrEmpty(((LuckyTex.Models.FG_SEARCHDATASEND400)(gridAS400.CurrentCell.Item)).STOCK))
                //            {
                //                single_row.IsEnabled = true;
                //            }
                //            else
                //            {
                //                single_row.IsEnabled = false;
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadStock

        private void LoadStock()
        {
            if (cbStock.ItemsSource == null)
            {
                string[] str = new string[] { "No", "Yes" };

                cbStock.ItemsSource = str;
                cbStock.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadFactory

        private void LoadFactory()
        {
            if (cbFactory.ItemsSource == null)
            {
                string[] str = new string[] { "AB", "AD" };

                cbFactory.ItemsSource = str;
                cbFactory.SelectedIndex = 0;
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            dteStartDate.SelectedDate = null;
            dteStartDate.Text = "";
            dteEndDate.SelectedDate = null;
            dteEndDate.Text = "";
            cbStock.Text = "No";
            cbStock.SelectedItem = 1;

            cbFactory.Text = "AB";
            cbFactory.SelectedItem = 1;

            dteSendDate.SelectedDate = DateTime.Now;
            txtPalletNo.Text = "";
            txtTotalCount.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridAS400.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridAS400.SelectedItems.Clear();
            else
                this.gridAS400.SelectedItem = null;

            gridAS400.ItemsSource = null;
        }

        #endregion

        #region SearchDataSendAS400
        private void SearchDataSendAS400()
        {
            string _StartDate = string.Empty;
            string _EndDate = string.Empty;
            string _Stock = string.Empty;
            string _PalletNo = string.Empty;

            if (dteStartDate.SelectedDate != null && dteEndDate.SelectedDate == null)
            {
                _StartDate = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                _EndDate = _StartDate;
            }
            else if (dteStartDate.SelectedDate == null && dteEndDate.SelectedDate != null)
            {
                _EndDate = dteEndDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                _StartDate = _EndDate;
            }
            else if (dteStartDate.SelectedDate != null && dteEndDate.SelectedDate != null)
            {
                _StartDate = dteStartDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
                _EndDate = dteEndDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            }

            if (cbStock.SelectedValue != null)
                _Stock = cbStock.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(txtPalletNo.Text))
                _PalletNo = txtPalletNo.Text;

            try
            {
                if (gridAS400.ItemsSource != null)
                {
                    var dataView = (ListCollectionView)CollectionViewSource.GetDefaultView(gridAS400.ItemsSource);
                    if (dataView.IsEditingItem)
                        dataView.CommitEdit();
                    if (dataView.IsAddingNew)
                        dataView.CommitNew();

                    BindingOperations.ClearAllBindings(gridAS400);
                    gridAS400.ItemsSource = null;
                }

                List<FG_SEARCHDATASEND400> lots = new List<FG_SEARCHDATASEND400>();

                lots = BCSPRFTPDataService.Instance.FG_SEARCHDATASEND400(_StartDate, _EndDate, _Stock, _PalletNo);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridAS400.ItemsSource = lots;
                    txtTotalCount.Text = lots.Count.ToString("#,##0.##");
                }
                else
                {
                    gridAS400.ItemsSource = null;
                    txtTotalCount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region SelectAll

        private void SelectAll()
        {
            try
            {
                List<LuckyTex.Models.FG_SEARCHDATASEND400> dataList = new List<LuckyTex.Models.FG_SEARCHDATASEND400>();
                int o = 0;
                foreach (var row in gridAS400.Items)
                {
                    LuckyTex.Models.FG_SEARCHDATASEND400 dataItem = new LuckyTex.Models.FG_SEARCHDATASEND400();

                    //if (!string.IsNullOrEmpty(((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ISLAB)
                    //    && string.IsNullOrEmpty(((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).STOCK))
                    //{
                        dataItem.SelectData = true;
                    //}
                    //else
                    //{
                    //    dataItem.SelectData = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).SelectData;
                    //}


                    dataItem.PALLETNO = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PALLETNO;
                    dataItem.INSPECTIONLOT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONLOT;
                    dataItem.ITEMCODE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ITEMCODE;
                    dataItem.GROSSLENGTH = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSLENGTH;
                    dataItem.NETLENGTH = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETLENGTH;
                    dataItem.GROSSWEIGHT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSWEIGHT;
                    dataItem.NETWEIGHT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETWEIGHT;
                    dataItem.GRADE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GRADE;
                    dataItem.CUSTOMERTYPE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).CUSTOMERTYPE;
                    dataItem.ISLAB = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ISLAB;
                    dataItem.INSPECTIONDATE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONDATE;
                    dataItem.FLAG = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).FLAG;
                    dataItem.LOADINGTYPE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).LOADINGTYPE;
                    dataItem.STOCK = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).STOCK;
                    dataItem.RETEST = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).RETEST;
                    dataItem.PRODUCTTYPE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PRODUCTTYPE;
                    dataItem.ROLLNO = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ROLLNO;
                    dataItem.CUSTOMERITEM = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).CUSTOMERITEM;

                    dataItem.ORDERNO = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ORDERNO;
                    dataItem.FINISHFLAG = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).FINISHFLAG;

                    o++;

                    dataList.Add(dataItem);
                }

                this.gridAS400.ItemsSource = dataList;
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
                List<LuckyTex.Models.FG_SEARCHDATASEND400> dataList = new List<LuckyTex.Models.FG_SEARCHDATASEND400>();
                int o = 0;
                foreach (var row in gridAS400.Items)
                {
                    LuckyTex.Models.FG_SEARCHDATASEND400 dataItem = new LuckyTex.Models.FG_SEARCHDATASEND400();

                    dataItem.SelectData = false;
                    dataItem.PALLETNO = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PALLETNO;
                    dataItem.INSPECTIONLOT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONLOT;
                    dataItem.ITEMCODE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ITEMCODE;
                    dataItem.GROSSLENGTH = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSLENGTH;
                    dataItem.NETLENGTH = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETLENGTH;
                    dataItem.GROSSWEIGHT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSWEIGHT;
                    dataItem.NETWEIGHT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETWEIGHT;
                    dataItem.GRADE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GRADE;
                    dataItem.CUSTOMERTYPE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).CUSTOMERTYPE;
                    dataItem.ISLAB = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ISLAB;
                    dataItem.INSPECTIONDATE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONDATE;
                    dataItem.FLAG = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).FLAG;
                    dataItem.LOADINGTYPE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).LOADINGTYPE;
                    dataItem.STOCK = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).STOCK;
                    dataItem.RETEST = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).RETEST;
                    dataItem.PRODUCTTYPE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PRODUCTTYPE;
                    dataItem.ROLLNO = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ROLLNO;
                    dataItem.CUSTOMERITEM = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).CUSTOMERITEM;

                    dataItem.ORDERNO = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ORDERNO;
                    dataItem.FINISHFLAG = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).FINISHFLAG;

                    o++;

                    dataList.Add(dataItem);
                }

                this.gridAS400.ItemsSource = dataList;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region SendAS400

        private bool SendAS400()
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

                string dInspe = string.Empty;
                string retest = string.Empty;
                string grade = string.Empty;

                string INSPECTIONLOT = string.Empty;
                DateTime? INSPECTIONDATE = null;

                //decimal? netLength = null;

                // เดิม ใช้ DateTime.Now
                //string dSend = DateTime.Now.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));

                // ใหม่เปลี่ยนมาดึงค่าจาก control datetime
                string dSend = string.Empty;

                DateTime? sendDate = null;

                string sDate = string.Empty;

                #endregion

                //ConStr = "Provider = SQLOLEDB; Server=(local);Database=TESLUCDAT;User Id=sa;Password=k5109484;";
                ConStr = strConAS400;

                //FLAGS = "R";
                //RECTY = "A";
                //USRNM = "PGMR";
                //CDUM0 = "MT";


                if (cbFactory.SelectedValue != null)
                {
                    if (cbFactory.SelectedValue.ToString() == "AB")
                    {
                        CDSTO = "3T";
                        LOCAT = "0000";
                        QUACA = "AB";
                    }
                    else if (cbFactory.SelectedValue.ToString() == "AD")
                    {
                        CDSTO = "3E";
                        LOCAT = "Z000";
                        QUACA = "AD";
                    }
                }

                FLAGS = ConfigManager.Instance.DefaultAS400Config_FLAGS;
                RECTY = ConfigManager.Instance.DefaultAS400Config_RECTY;

                //ไม่ได้ใช้งาน
                //CDSTO = ConfigManager.Instance.DefaultAS400Config_CDSTO;

                USRNM = ConfigManager.Instance.DefaultAS400Config_USRNM;
                CDUM0 = ConfigManager.Instance.DefaultAS400Config_CDUM0;

                if (dteSendDate.SelectedDate != null)
                {
                    dSend = dteSendDate.SelectedDate.Value.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + dteSendDate.SelectedDate.Value.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + dteSendDate.SelectedDate.Value.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));
                }
                else
                {
                    dSend = DateTime.Now.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + DateTime.Now.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));
                }
                
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

                sendDate = DateTime.Now;

                if (sendDate != null)
                {
                    sDate = sendDate.Value.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("dd", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("HH", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("mm", CultureInfo.CreateSpecificCulture("en-US"));
                }

                int o = 0;
                foreach (var row in gridAS400.Items)
                {
                    dInspe = string.Empty;
                    retest = string.Empty;
                    grade = string.Empty;

                    DTINP = null;
                    CDEL0 = string.Empty;
                    CDCON = string.Empty;
                    BLELE = null;
                    CDKE1 = string.Empty;
                    CDKE2 = string.Empty;
                    CDTRA = string.Empty;
                    CDQUA = string.Empty;
                    TECU1 = null;
                    TECU2 = null;
                    TECU4 = null;

                    INSPECTIONLOT = string.Empty;
                    INSPECTIONDATE = null;
                    
                    if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).SelectData == true)
                    {
                        #region DTINP

                        dInspe = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONDATE.Value.ToString("yyyy") + ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONDATE.Value.ToString("MM") + ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONDATE.Value.ToString("dd");

                        try
                        {
                            DTINP = Convert.ToInt32(dInspe);
                        }
                        catch
                        {
                            DTINP = null;
                        }

                        #endregion

                        CDEL0 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ROLLNO;

                        CDCON = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PALLETNO;

                        #region NETLENGTH

                        if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETLENGTH != null)
                        {
                            try
                            {
                                BLELE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETLENGTH;
                            }
                            catch
                            {
                                BLELE = null;
                            }
                        }


                        #endregion

                        #region GROSSLENGTH

                        if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSLENGTH != null)
                        {
                            try
                            {
                                TECU4 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSLENGTH;
                            }
                            catch
                            {
                                TECU4 = null;
                            }
                        }

                        #endregion

                        //CDKE1 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).ITEMCODE;
                        CDKE1 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).CUSTOMERITEM;

                        #region CDKE2

                        if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PRODUCTTYPE != null)
                        {
                            if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PRODUCTTYPE != "-")
                                CDKE2 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).PRODUCTTYPE;
                        }

                        #endregion

                        #region CDTRA

                        if (!string.IsNullOrEmpty(((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).RETEST))
                        {
                            retest = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).RETEST;

                            if (retest == "N")
                                CDTRA = "IA";
                            else if (retest == "Y")
                                CDTRA = "IB";
                        }

                        #endregion

                        #region CDQUA

                        if (!string.IsNullOrEmpty(((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GRADE))
                        {
                            grade = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GRADE;

                            if (grade == "A")
                                CDQUA = "1";
                            else if (grade == "B")
                                CDQUA = "2";
                            else if (grade == "C")
                                CDQUA = "3";
                            else if (grade == "T")
                                CDQUA = "4";
                            else if (grade == "X")
                                CDQUA = "5";
                        }

                        #endregion

                        #region TECU1

                        if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSWEIGHT != null)
                        {
                            try
                            {
                                TECU1 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).GROSSWEIGHT;
                            }
                            catch
                            {
                                TECU1 = null;
                            }
                        }

                        #endregion

                        #region TECU2

                        if (((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETWEIGHT != null)
                        {
                            try
                            {
                                TECU2 = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).NETWEIGHT;
                            }
                            catch
                            {
                                TECU2 = null;
                            }
                        }

                        #endregion

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
                            break;
                        }
                        else
                        {
                            INSPECTIONLOT = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONLOT;
                            INSPECTIONDATE = ((LuckyTex.Models.FG_SEARCHDATASEND400)((gridAS400.Items)[o])).INSPECTIONDATE;

                            if (BCSPRFTPDataService.Instance.FG_UPDATEDATASEND400(INSPECTIONLOT, INSPECTIONDATE, CDCON) == false)
                            {
                                "Can't FG_UPDATEDATASEND400".ShowMessageBox(true);
                                chkErr = false;
                                break;
                            }
                        }
                    }

                    o++;
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
