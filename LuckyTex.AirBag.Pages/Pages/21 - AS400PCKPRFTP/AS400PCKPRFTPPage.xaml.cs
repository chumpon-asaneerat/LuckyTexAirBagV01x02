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
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for AS400PCKPRFTPNPage.xaml
    /// </summary>
    public partial class AS400PCKPRFTPPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AS400PCKPRFTPPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        ListPCKPRFTPData _session = new ListPCKPRFTPData();

        string strConAS400 = string.Empty;
        string operatorid = string.Empty;
       
        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();
            ConAS400();
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

        #region txtCDEL0_KeyDown

        private void txtCDEL0_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                string invno = string.Empty;
                if (!string.IsNullOrEmpty(txtINVNO.Text))
                    invno = txtINVNO.Text;

                string cdel0 = string.Empty;
                if (!string.IsNullOrEmpty(txtCDEL0.Text))
                    cdel0 = txtCDEL0.Text;

                LoadPCKPRFTP(invno, cdel0);
            }
        }

        #endregion

        private void txtINVNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                string invno = string.Empty;
                if (!string.IsNullOrEmpty(txtINVNO.Text))
                    invno = txtINVNO.Text;

                string cdel0 = string.Empty;
                if (!string.IsNullOrEmpty(txtCDEL0.Text))
                    cdel0 = txtCDEL0.Text;

                LoadPCKPRFTP(invno, cdel0);
            }
        }


        #endregion

        #region dgAS400_SelectedCellsChanged

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

        private void dgAS400_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (dgAS400.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(dgAS400);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            #region PalletNo

                            if (((LuckyTex.Models.ListPCKPRFTPData)(dgAS400.CurrentCell.Item)).CDEL0 != null)
                            {
                                _session.CDEL0 = ((LuckyTex.Models.ListPCKPRFTPData)(dgAS400.CurrentCell.Item)).CDEL0;
                            }
                            else
                            {
                                _session.CDEL0 = "";
                            }

                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            string invno = string.Empty;
            if (!string.IsNullOrEmpty(txtINVNO.Text))
                invno = txtINVNO.Text;

            string cdel0 = string.Empty;
            if (!string.IsNullOrEmpty(txtCDEL0.Text))
                cdel0 = txtCDEL0.Text;

            LoadPCKPRFTP(invno, cdel0);
        }

        #endregion

        #region cmdGenNo_Click
        private void cmdGenNo_Click(object sender, RoutedEventArgs e)
        {
            string invno = string.Empty;
            if (!string.IsNullOrEmpty(txtINVNO.Text))
            {
                invno = txtINVNO.Text;
                GenRunNo(invno);
            }
        }
        #endregion

        #region cmdAS400_Click

        private void cmdAS400_Click(object sender, RoutedEventArgs e)
        {
            if (PCKPRFTPDataService.Instance.chkConAS400(strConAS400) == true)
                GetDataAS400();
            else
                labConnect.Content = "Can't Connect";
        }

        #endregion

        #region cmdExcel_Click
        private void cmdExcel_Click(object sender, RoutedEventArgs e)
        {
            ConnDataExcel();
        }
        #endregion

        #endregion

        #region private Methods

        #region ClearControl

        private void ClearControl()
        {
            txtCDEL0.Text = string.Empty;
            txtINVNO.Text = string.Empty;
            labConnect.Content = string.Empty;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.dgAS400.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.dgAS400.SelectedItems.Clear();
            else
                this.dgAS400.SelectedItem = null;

            dgAS400.ItemsSource = null;

            if (this.dgExcel.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.dgExcel.SelectedItems.Clear();
            else
                this.dgExcel.SelectedItem = null;

            dgExcel.ItemsSource = null;


            txtCDEL0.Focus();
        }

        #endregion

        #region LoadPCKPRFTP

        private void LoadPCKPRFTP(string INVNO, string CDEL0)
        {
            List<ListPCKPRFTPData> lots = new List<ListPCKPRFTPData>();

            lots = PCKPRFTPDataService.Instance.PCKPRFTP_GETDATA(INVNO,CDEL0,null);

            if (null != lots)
            {
                dgAS400.ItemsSource = lots;
            }

            //var orderByResult = lots.OrderBy(x => x.INVNO)
            //      .ThenBy(x => x.CDEL0)
            //      .ToList();

            //var orderByResult = lots.OrderByDescending(x => x.INVNO).ThenByDescending(x => x.CDEL0)
            //     .ToList();

            //var orderByResult = from s in lots
            //                    where s.INUSE == 1
            //                    orderby s.INVNO, s.CDEL0
            //                    select new { s.INVNO, s.CDEL0 };

            //if (null != orderByResult)
            //{
            //    dgAS400.ItemsSource = orderByResult;
            //}
        }

        #endregion

        #region GenRunNo
        private bool GenRunNo(string INVNO)
        {
            bool chkSave = true;

            try
            {
                List<ListPCKPRFTPData> lots = new List<ListPCKPRFTPData>();

                lots = PCKPRFTPDataService.Instance.PCKPRFTP_GETDATA(INVNO, string.Empty,null);

                if (null != lots)
                {
                    decimal? runNo = 0;

                    foreach (ListPCKPRFTPData dbResult in lots)
                    {
                        _session = new ListPCKPRFTPData();

                        _session.INVNO = dbResult.INVNO;
                        _session.CDEL0 = dbResult.CDEL0;
                        _session.CUSNO = dbResult.CUSNO;

                        if (_session.CUSNO != null)
                        {
                            runNo = _session.CUSNO;
                        }
                        else
                        {
                            runNo++;
                        }

                        if (!string.IsNullOrEmpty(_session.CDEL0))
                        {
                            if (PCKPRFTPDataService.Instance.PCKPRFTP_UPDATERUNNO(_session.INVNO, _session.CDEL0, runNo, operatorid,1) == false)
                            {
                                chkSave = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkSave = false;
            }

            return chkSave;
        }
        #endregion

        #region Save

        private bool Save()
        {
            bool chkSave = false;

            try
            {
                //_session = new ListPCKPRFTPData();

                //_session.ANNUL = null;
                //_session.CDDIV = "SE";
                //_session.INVTY = "E";
                //_session.INVNO = "200027";
                //_session.CDORD = "782850";
                //_session.RELNO = 1;
                //_session.CUSCD = "2116";
                //_session.CUSNM = "TORAY INTERNATIONAL INC.,";
                //_session.RECTY = "A";
                //_session.CDKE1 = "4L51C15R";
                //_session.CDKE2 = null;
                //_session.CSITM = null;
                //_session.CDCON = "211211010";
                //_session.CDEL0 = "C4121Z061";
                //_session.GRADE = "A";
                //_session.PIELN = 500;
                //_session.NETWH = 225;
                //_session.GRSWH = 229;
                //_session.GRSLN = 500;
                //_session.PALSZ = "ABPL01";
                //_session.DTTRA = 20220107;
                //_session.DTORA = 202203111331;


                //if (!string.IsNullOrEmpty(_session.INVNO) && !string.IsNullOrEmpty(_session.CDEL0))
                //{
                //    PCKPRFTPDataService.Instance.PCKPRFTP_INSERTUPDATE(_session.ANNUL, _session.CDDIV, _session.INVTY, _session.INVNO, _session.CDORD, _session.RELNO, _session.CUSCD,
                //                   _session.CUSNM, _session.RECTY, _session.CDKE1, _session.CDKE2, _session.CSITM, _session.CDCON, _session.CDEL0, _session.GRADE, _session.PIELN, _session.NETWH,
                //                   _session.GRSWH, _session.GRSLN, _session.PALSZ, _session.DTTRA, _session.DTORA, _session.AS400NO, operatorid,1);
                //}

                //_session.CDEL0 = "B4519Z201";
                //_session.CUSNO = 1;

                //if (!string.IsNullOrEmpty(_session.CDEL0))
                //{
                //    PCKPRFTPDataService.Instance.PCKPRFTP_UPDATECUSNO(_session.CDEL0, _session.CUSNO, operatorid);
                //}

                List<ListPCKPRFTPData> result = new List<ListPCKPRFTPData>();

                result = PCKPRFTPDataService.Instance.PCKPRFTP_GETDATA(string.Empty, string.Empty,null);

                var lots = result.OrderBy(x => x.CDDIV)
                       .ThenBy(x => x.INVTY)
                       .ThenBy(x => x.INVNO)
                       .ThenBy(x => x.CDORD)
                       .ThenBy(x => x.RELNO)
                       .ThenBy(x => x.CDKE1)
                       .ThenBy(x => x.CDCON)
                       .ThenBy(x => x.CDEL0)
                       .ToList();

                if (null != lots)
                {
                    int run = 1;
                    string inv = string.Empty;

                    int i = 1;
                    foreach (ListPCKPRFTPData dbResult in lots)
                    {
                        _session = new ListPCKPRFTPData();

                        _session.ANNUL = dbResult.ANNUL;
                        _session.CDDIV = dbResult.CDDIV;
                        _session.INVTY = dbResult.INVTY;
                        _session.INVNO = dbResult.INVNO;
                        _session.CDORD = dbResult.CDORD;
                        _session.RELNO = dbResult.RELNO;
                        _session.CUSCD = dbResult.CUSCD;
                        _session.CUSNM = dbResult.CUSNM;
                        _session.RECTY = dbResult.RECTY;
                        _session.CDKE1 = dbResult.CDKE1;
                        _session.CDKE2 = dbResult.CDKE2;
                        _session.CSITM = dbResult.CSITM;
                        _session.CDCON = dbResult.CDCON;
                        _session.CDEL0 = dbResult.CDEL0;
                        _session.GRADE = dbResult.GRADE;
                        _session.PIELN = dbResult.PIELN;
                        _session.NETWH = dbResult.NETWH;
                        _session.GRSWH = dbResult.GRSWH;
                        _session.GRSLN = dbResult.GRSLN;
                        _session.PALSZ = dbResult.PALSZ;
                        _session.DTTRA = dbResult.DTTRA;
                        _session.DTORA = dbResult.DTORA;

                        #region AS400NO

                        if (!string.IsNullOrEmpty(_session.INVNO))
                        {
                            if (string.IsNullOrEmpty(inv))
                            {
                                inv = _session.INVNO;
                                run = 1;
                            }
                            else
                            {
                                if (inv == _session.INVNO)
                                {
                                    run++;
                                }
                                else
                                {
                                    inv = _session.INVNO;
                                    run = 1;
                                }
                            }
                        }

                        _session.AS400NO = run;

                        #endregion
                        

                        if (!string.IsNullOrEmpty(_session.INVNO) && !string.IsNullOrEmpty(_session.CDEL0))
                        {

                            //if (PCKPRFTPDataService.Instance.PCKPRFTP_UPDATECUSNO(_session.CDEL0, _session.CUSNO,operatorid,0) == false)
                            //    break;

                            if (PCKPRFTPDataService.Instance.PCKPRFTP_INSERTUPDATE(_session.ANNUL, _session.CDDIV, _session.INVTY, _session.INVNO, _session.CDORD, _session.RELNO, _session.CUSCD,
                               _session.CUSNM, _session.RECTY, _session.CDKE1, _session.CDKE2, _session.CSITM, _session.CDCON, _session.CDEL0, _session.GRADE, _session.PIELN, _session.NETWH,
                               _session.GRSWH, _session.GRSLN, _session.PALSZ, _session.DTTRA, _session.DTORA, _session.AS400NO, operatorid,1) == false)
                                break;

                            i++;
                        }

                    }
                }

                chkSave = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkSave = false;
            }

            return chkSave;
        }

        #endregion

        #region AS400

        #region ConAS400

        private void ConAS400()
        {
            ConfigManager.Instance.LoadAS400Configs();
            strConAS400 = ConfigManager.Instance.AS400Config;
        }

        #endregion

        #region GetDataAS400

        private void GetDataAS400()
        {
            string where = string.Empty;
            //where = "Where #RECTY = 'S' ";
            string strErr = string.Empty;
            labConnect.Content = string.Empty;
            try
            {
                List<PCKPRFTPResult> orderByResult = PCKPRFTPDataService.Instance.GetPCKPRFTP(strConAS400, where);

                List<LuckyTex.Models.ListPCKPRFTPData> dataList = new List<LuckyTex.Models.ListPCKPRFTPData>();

                var results = orderByResult.OrderBy(x => x.CDDIV)
                   .ThenBy(x => x.INVTY)
                   .ThenBy(x => x.INVNO)
                   .ThenBy(x => x.CDORD)
                   .ThenBy(x => x.RELNO)
                   .ThenBy(x => x.CDKE1)
                   .ThenBy(x => x.CDCON)
                   .ThenBy(x => x.CDEL0)
                   .ToList();

                if (results != null && results.Count > 0)
                {
                    labConnect.Content = "PCKPRFTP row = " + results.Count.ToString();

                    int i = 0;
                    int run = 1;
                    string inv = string.Empty;

                    #region GetPCKPRFTP

                    foreach (var row in results)
                    {
                        LuckyTex.Models.ListPCKPRFTPData dataItemNew = new LuckyTex.Models.ListPCKPRFTPData();

                        #region ANNUL

                        if (results[i].ANNUL != null)
                        {
                            dataItemNew.ANNUL = results[i].ANNUL;
                        }

                        #endregion

                        #region CDDIV

                        if (results[i].CDDIV != null)
                        {
                            dataItemNew.CDDIV = results[i].CDDIV;
                        }

                        #endregion

                        #region INVTY

                        if (results[i].INVTY != null)
                        {
                            dataItemNew.INVTY = results[i].INVTY;
                        }

                        #endregion

                        #region INVNO

                        if (results[i].INVNO != null)
                        {
                            dataItemNew.INVNO = results[i].INVNO;
                        }

                        #endregion

                        #region CDORD

                        if (results[i].CDORD != null)
                        {
                            dataItemNew.CDORD = results[i].CDORD;
                        }

                        #endregion

                        #region RELNO

                        if (results[i].RELNO != null)
                        {
                            dataItemNew.RELNO = results[i].RELNO;
                        }

                        #endregion

                        #region CUSCD

                        if (results[i].CUSCD != null)
                        {
                            dataItemNew.CUSCD = results[i].CUSCD;
                        }

                        #endregion

                        #region CUSNM

                        if (results[i].CUSNM != null)
                        {
                            dataItemNew.CUSNM = results[i].CUSNM;
                        }

                        #endregion

                        #region RECTY

                        if (results[i].RECTY != null)
                        {
                            dataItemNew.RECTY = results[i].RECTY;
                        }

                        #endregion

                        #region CDKE1

                        if (results[i].CDKE1 != null)
                        {
                            dataItemNew.CDKE1 = results[i].CDKE1;
                        }

                        #endregion

                        #region CDKE2

                        if (results[i].CDKE2 != null)
                        {
                            dataItemNew.CDKE2 = results[i].CDKE2;
                        }

                        #endregion

                        #region CSITM

                        if (results[i].CSITM != null)
                        {
                            dataItemNew.CSITM = results[i].CSITM;
                        }

                        #endregion

                        #region CDCON

                        if (results[i].CDCON != null)
                        {
                            dataItemNew.CDCON = results[i].CDCON;
                        }

                        #endregion

                        #region CDEL0

                        if (results[i].CDEL0 != null)
                        {
                            dataItemNew.CDEL0 = results[i].CDEL0;
                        }

                        #endregion

                        #region GRADE

                        if (results[i].GRADE != null)
                        {
                            dataItemNew.GRADE = results[i].GRADE;
                        }

                        #endregion

                        #region PIELN

                        if (results[i].PIELN != null)
                        {
                            dataItemNew.PIELN = results[i].PIELN;
                        }

                        #endregion

                        #region NETWH

                        if (results[i].NETWH != null)
                        {
                            dataItemNew.NETWH = results[i].NETWH;
                        }

                        #endregion

                        #region GRSWH

                        if (results[i].GRSWH != null)
                        {
                            dataItemNew.GRSWH = results[i].GRSWH;
                        }

                        #endregion

                        #region GRSLN

                        if (results[i].GRSLN != null)
                        {
                            dataItemNew.GRSLN = results[i].GRSLN;
                        }

                        #endregion

                        #region PALSZ

                        if (results[i].PALSZ != null)
                        {
                            dataItemNew.PALSZ = results[i].PALSZ;
                        }

                        #endregion

                        #region DTTRA

                        if (results[i].DTTRA != null)
                        {
                            try
                            {
                                dataItemNew.DTTRA = decimal.Parse(results[i].DTTRA);
                            }
                            catch
                            {
                                "Error DTTRA".ShowMessageBox(true);
                                break;
                            }
                        }

                        #endregion

                        #region DTORA

                        if (results[i].DTORA != null)
                        {
                            try
                            {
                                dataItemNew.DTORA = decimal.Parse(results[i].DTORA);
                            }
                            catch
                            {
                                "Error DTORA".ShowMessageBox(true);
                                break;
                            }
                        }

                        #endregion

                        #region AS400NO

                        if (!string.IsNullOrEmpty(dataItemNew.INVNO))
                        {
                            if (string.IsNullOrEmpty(inv))
                            {
                                inv = dataItemNew.INVNO;
                                run = 1;
                            }
                            else
                            {
                                if (inv == dataItemNew.INVNO)
                                {
                                    run++;
                                }
                                else
                                {
                                    inv = dataItemNew.INVNO;
                                    run = 1;
                                }
                            }
                        }

                        dataItemNew.RUNNO = run;
                        dataItemNew.AS400NO = run;

                        #endregion

                        dataList.Add(dataItemNew);

                        i++;
                    }

                    #endregion

                    dgAS400.ItemsSource = dataList;
                }
                else
                {
                    //labConnect.Content = "Can't Connect";
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox();
            }
        }

        #endregion

        #endregion

        #region load excel

        #region ConnDataExcel
        private bool ConnDataExcel()
        {
            bool chkData = true;

            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "Excel 2007 (*.xlsx)|*.xlsx|Excel (*.xlsm)|*.xlsm|Excel 97-2003 (*.xls)|*.xls";
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    string filename = dlg.FileName;
                    FileInfo fileInfo = new FileInfo(filename);
                    //Create COM Objects.
                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                    List<LuckyTex.Models.PackingListResult> dataList = new List<LuckyTex.Models.PackingListResult>();
                    //
                    //Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Open(filename);

                    List<string> workSheets = new List<string>();
                    Workbook book = excelApp.Workbooks.Open(filename, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    foreach (Worksheet sheet in book.Worksheets)
                    {
                        workSheets.Add(sheet.Name);
                    }

                    if (workSheets.Count > 0)
                    {
                        string fabricNo = string.Empty;
                        string rollNo = string.Empty;
                        decimal? qty = null;
                        decimal? nw = null;
                        decimal? gw = null;
                        decimal? cusRunNo = 0;

                        decimal d;

                        int sheetNorows = workSheets.Count;

                        for (int sheetNo = 1; sheetNo <= sheetNorows; sheetNo++)
                        {
                            Microsoft.Office.Interop.Excel.Worksheet excelSheet = book.Sheets[sheetNo];
                            Microsoft.Office.Interop.Excel.Range excelRange = excelSheet.UsedRange;

                            int rows = excelRange.Rows.Count;

                            try
                            {
                                if (rows > 0)
                                {
                                    fabricNo = string.Empty;

                                    for (int i = 5; i <= rows; i++)
                                    {
                                        if (excelRange.Cells[i, 2].Value2 != null)
                                        {
                                            PackingListResult dataItem = new PackingListResult();

                                            rollNo = string.Empty;
                                            qty = null;
                                            nw = null;
                                            gw = null;
                                            cusRunNo = null;

                                            if (string.IsNullOrEmpty(fabricNo))
                                            {
                                                fabricNo = excelRange.Cells[i, 1].Value2.ToString();
                                            }
                                            //else
                                            //{
                                            //    if (workSheets[sheetNo-1].ToString().Contains(fabricNo))
                                            //    { 

                                            //    }
                                            //}

                                            rollNo = excelRange.Cells[i, 2].Value2.ToString();

                                            if (excelRange.Cells[i, 7].Value2 != null)
                                            {
                                                if (decimal.TryParse(excelRange.Cells[i, 7].Value2.ToString(), out d) == true)
                                                    cusRunNo = decimal.Parse(excelRange.Cells[i, 7].Value2.ToString());
                                            }

                                            if (excelRange.Cells[i, 3].Value2 != null)
                                            {
                                                if (decimal.TryParse(excelRange.Cells[i, 3].Value2.ToString(), out d) == true)
                                                    qty = decimal.Parse(excelRange.Cells[i, 3].Value2.ToString());
                                            }

                                            if (excelRange.Cells[i, 8].Value2 != null)
                                            {
                                                if (decimal.TryParse(excelRange.Cells[i, 8].Value2.ToString(), out d) == true)
                                                    nw = decimal.Parse(excelRange.Cells[i, 8].Value2.ToString());
                                            }

                                            if (excelRange.Cells[i, 9].Value2 != null)
                                            {
                                                if (decimal.TryParse(excelRange.Cells[i, 9].Value2.ToString(), out d) == true)
                                                    gw = decimal.Parse(excelRange.Cells[i, 9].Value2.ToString());
                                            }

                                            dataItem.FabricNo = fabricNo;
                                            dataItem.RollNo = rollNo;
                                            dataItem.Qty = qty;
                                            dataItem.NW = nw;
                                            dataItem.GW = gw;
                                            dataItem.CusRunNo = cusRunNo;

                                            dataList.Add(dataItem);
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                chkData = false;
                            }
                        }
                    }

                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    if (dataList != null && dataList.Count > 0)
                        this.dgExcel.ItemsSource = dataList;
                    else
                        this.dgExcel.ItemsSource = null;

                    return chkData;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
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
        public void Setup(string opera)
        {
            if (null != opera)
            {
                txtOperator.Text = opera;
                operatorid = opera;
            }
            else
            {
                txtOperator.Text = "-";
            }
        }

        #endregion

    }
}
