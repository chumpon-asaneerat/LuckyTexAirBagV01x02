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
    /// Interaction logic for PalletListPage.xaml
    /// </summary>
    public partial class PalletListPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PalletListPage()
        {
            InitializeComponent();

            //CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables
        private PackingSession _session = new PackingSession();

        string opera = string.Empty;
        string PALLETNO = string.Empty;

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
            LoadCheckingStatus();
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

        #region cmdCheckingPallet_Click

        private void cmdCheckingPallet_Click(object sender, RoutedEventArgs e)
        {
            if (PACK_UPDATEPACKINGPALLET() == true)
            {
                "Checking Complete".ShowMessageBox(false);
                LoadPacking();
            }
        }

        #endregion

        #region cmdGetPalletDetail_Click

        private void cmdGetPalletDetail_Click(object sender, RoutedEventArgs e)
        {
            PACK_GETPALLETDETAIL();
        }

        #endregion

        #region cmdPrintPalletSheet_Click

        private void cmdPrintPalletSheet_Click(object sender, RoutedEventArgs e)
        {
            ConmonReportService.Instance.chkRowBreak = true;
            ConmonReportService.Instance.Printername = "";
            PrintPalletList();
        }

        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_palletNO))
            {
                if (D365_PK_TOTALHEADER() == true)
                {
                    "Send D365 complete".ShowMessageBox();
                }
            }
            else
            {
                "Pallet No is null".ShowMessageBox();
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
                    ConmonReportService.Instance.GRADE = "";

                    var row_list = GetDataGridRows(gridPack);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.PACK_SEARCHPALLETLIST)(gridPack.CurrentCell.Item)).PALLETNO != null)
                            {
                                PALLETNO = ((LuckyTex.Models.PACK_SEARCHPALLETLIST)(gridPack.CurrentCell.Item)).PALLETNO;

                                if (!string.IsNullOrEmpty(PALLETNO))
                                {
                                    _palletNO = PALLETNO;
                                    GetGrade(PALLETNO);
                                }
                                else
                                {
                                    _palletNO = string.Empty;
                                    PALLETNO = null;
                                }
                            }
                            else
                            {
                                _palletNO = string.Empty;
                                PALLETNO = null;
                            }
                        }
                    }
                }
                else
                {
                    _palletNO = string.Empty;
                    PALLETNO = null;

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

        #region Preview

        private void Preview(string PALLETNO)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PalletList";
                ConmonReportService.Instance.PALLETNO = PALLETNO;

                if (ConmonReportService.Instance.GRADE == "C" || ConmonReportService.Instance.GRADE == "X")
                {
                    if (ConmonReportService.Instance.RowCount != 0)
                    {
                        //ConmonReportService.Instance.RowLast.ToString().ShowMessageBox();
                        if (ConmonReportService.Instance.chkRowBreak == true)
                        {
                            if (ConmonReportService.Instance.RowCount > ConmonReportService.Instance.RowLast)
                            {
                                var newWindow = new RepMasterForm();
                                newWindow.ShowDialog();
                                Preview(PALLETNO);
                                ConmonReportService.Instance.RowLast = 0;
                            }
                            else if (ConmonReportService.Instance.RowCount == ConmonReportService.Instance.RowLast)
                            {
                                var newWindow = new RepMasterForm();
                                newWindow.ShowDialog();
                                ConmonReportService.Instance.RowLast = 0;
                            }
                            else
                            {
                                ConmonReportService.Instance.RowLast = 0;
                                ConmonReportService.Instance.chkRowBreak = true;
                            }
                        }
                        else
                        {
                            ConmonReportService.Instance.chkRowBreak = true;
                        }
                    }
                }
                else
                {
                    var newWindow = new RepMasterForm();
                    newWindow.ShowDialog();

                    ConmonReportService.Instance.RowLast = 0;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadCheckingStatus

        private void LoadCheckingStatus()
        {
            if (cbCheckingStatus.ItemsSource == null)
            {
                string[] str = new string[] { "All", "Yes", "No" };

                cbCheckingStatus.ItemsSource = str;
                cbCheckingStatus.SelectedIndex = 0;
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            PALLETNO = null;

            _palletNO = string.Empty;
            PRODID = null;
            HEADERID = null;

            _LOTNO = string.Empty;
            _ITEMID = string.Empty;
            _LOADINGTYPE = string.Empty;

            dtePackingDate.SelectedDate = null;
            dtePackingDate.Text = "";
            cbCheckingStatus.Text = "";
            txtPalletNo.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPack.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPack.SelectedItems.Clear();
            else
                this.gridPack.SelectedItem = null;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPallet.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPallet.SelectedItems.Clear();
            else
                this.gridPallet.SelectedItem = null;

            gridPack.ItemsSource = null;

            gridPallet.ItemsSource = null;
            
        }

        #endregion

        #region LoadPacking
        private void LoadPacking()
        {
            string _PalletNo = string.Empty;
            string _PackingDate = string.Empty;
            string _CheckingStatus = string.Empty;

            PALLETNO = null;

            _palletNO = string.Empty;
            PRODID = null;
            HEADERID = null;

            _LOTNO = string.Empty;
            _ITEMID = string.Empty;
            _LOADINGTYPE = string.Empty;

            if (txtPalletNo.Text != "")
                _PalletNo = txtPalletNo.Text;

            if (dtePackingDate.SelectedDate != null)
                _PackingDate = dtePackingDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            if (cbCheckingStatus.SelectedValue != null)
            {
                if (cbCheckingStatus.SelectedValue.ToString() == "Yes")
                    _CheckingStatus = "Y";
                else if (cbCheckingStatus.SelectedValue.ToString() == "No")
                    _CheckingStatus = "N";
            }


            List<PACK_SEARCHPALLETLIST> lots = new List<PACK_SEARCHPALLETLIST>();

            lots = PackingDataService.Instance.Pack_SearchPalletList(_PalletNo, _PackingDate, _CheckingStatus);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridPack.ItemsSource = lots;
            }
            else
            {
                gridPack.ItemsSource = null;
            }

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPallet.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPallet.SelectedItems.Clear();
            else
                this.gridPallet.SelectedItem = null;

            gridPallet.ItemsSource = null;
            
        }
        #endregion

        #region PACK_UPDATEPACKINGPALLET

        private bool PACK_UPDATEPACKINGPALLET()
        {
            try
            {
                //if (gridPack.SelectedItems.Count > 0)
                //{

                    string PALLET = string.Empty;
                    string OPERATOR = string.Empty;
                    DateTime? CHECKDATE = null;
                    string REMARK = string.Empty;

                    OPERATOR = txtOperator.Text;
                    CHECKDATE = DateTime.Now;

                    foreach (object obj in gridPack.ItemsSource)
                    {
                        if (((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO != null)
                        {
                            PALLET = ((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO;

                            REMARK = "";

                            if (((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PalletChecking == true)
                            {
                                REMARK = ((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).REMARK;
                            }

                            if (((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PalletChecking == true && REMARK != "")
                                PackingDataService.Instance.PACK_UPDATEPACKINGPALLET(PALLET, OPERATOR, CHECKDATE, REMARK);
                        }
                    }
                //}

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region PACK_GETPALLETDETAIL

        private void PACK_GETPALLETDETAIL()
        {
            try
            {
                if (gridPack.SelectedItems.Count > 0)
                {

                    string PALLET = string.Empty;
                   

                    foreach (object obj in gridPack.SelectedItems)
                    {
                        if (((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO != null)
                        {
                            PALLET = ((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO;

                            List<PACK_GETPALLETDETAIL> lots = new List<PACK_GETPALLETDETAIL>();

                            lots = PackingDataService.Instance.PACK_GETPALLETDETAIL(PALLET);

                            if (null != lots && lots.Count > 0 && null != lots[0])
                            {
                                gridPallet.ItemsSource = lots;
                            }
                            else
                            {
                                gridPallet.ItemsSource = null;
                            }
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

        #region PrintPalletList

        #region PrintPalletList
        private void PrintPalletList()
        {
            try
            {
                if (gridPack.SelectedItems.Count > 0)
                {
                    foreach (object obj in gridPack.SelectedItems)
                    {
                        if (((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO != null)
                        {
                            //Print(((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO);
                            Preview(((LuckyTex.Models.PACK_SEARCHPALLETLIST)(obj)).PALLETNO);
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

        #region GetGrade
        private void GetGrade(string _PalletNo)
        {
            try
            {
                List<PACK_GETPALLETDETAIL> lots = new List<PACK_GETPALLETDETAIL>();

                lots = PackingDataService.Instance.PACK_GETPALLETDETAIL(_PalletNo);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    //ConmonReportService.Instance.RowCount = lots.Count;
                    ConmonReportService.Instance.GRADE = lots[0].GRADE;
                }
                else
                {
                    //ConmonReportService.Instance.RowCount = 0;
                    ConmonReportService.Instance.GRADE = "";
                }

                ConmonReportService.Instance.RowCount = PackingDataService.Instance.Count_PACK_PALLETSHEET(_PalletNo);
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }

        }
        #endregion

        #endregion

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
