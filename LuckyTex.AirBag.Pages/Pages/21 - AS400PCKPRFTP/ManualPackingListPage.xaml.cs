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
    /// Interaction logic for ManualPackingListPage.xaml
    /// </summary>
    public partial class ManualPackingListPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ManualPackingListPage()
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

        #region txtINVNO_KeyDown
        private void txtINVNO_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter || e.Key == Key.Return)
            //{
            //    string invno = string.Empty;
            //    if (!string.IsNullOrEmpty(txtINVNO.Text))
            //        invno = txtINVNO.Text;

            //    string cdel0 = string.Empty;
            //    if (!string.IsNullOrEmpty(txtCDEL0.Text))
            //        cdel0 = txtCDEL0.Text;

            //    LoadPCKPRFTP(invno, cdel0);
            //}

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCDEL0.SelectAll();
                txtCDEL0.Focus();
                e.Handled = true;
            }
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

                if (!string.IsNullOrEmpty(invno) || !string.IsNullOrEmpty(cdel0))
                {
                    LoadPCKPRFTP(invno, cdel0);
                }
                else
                {
                    string dte = string.Empty;
                    dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    listBox1.Items.Add(new MyListBoxItem(dte, "Please insert DO NO or ROLL NO", ""));
                }
            }
        }

        #endregion

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
                            #region CDEL0

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

            if (!string.IsNullOrEmpty(invno) || !string.IsNullOrEmpty(cdel0))
            {
                LoadPCKPRFTP(invno, cdel0);
            }
            else
            {
                "Please insert DO NO or ROLL NO".ShowMessageBox();
                if (string.IsNullOrEmpty(invno))
                {
                    txtINVNO.Focus();
                    txtINVNO.SelectAll();
                }
                else if (string.IsNullOrEmpty(cdel0))
                {
                    txtCDEL0.Focus();
                    txtCDEL0.SelectAll();
                }
            }
        }

        #endregion

        #region cmdDelete_Click

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtINVNO.Text))
            {
                Delete(txtINVNO.Text, txtCDEL0.Text);
            }
            else
            {
                string dte = string.Empty;
                dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                listBox1.Items.Add(new MyListBoxItem(dte, "DO NO. isn't null ", ""));
                txtINVNO.Focus();
                txtINVNO.SelectAll();
            }
        }

        #endregion

        #region cmdAS400_Click

        private void cmdAS400_Click(object sender, RoutedEventArgs e)
        {
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            if (PCKPRFTPDataService.Instance.chkConAS400(strConAS400) == true)
                AS400InsertData();
            else
                listBox1.Items.Add(new MyListBoxItem(dte, "Can't Connect AS400", ""));
        }

        #endregion

        #region cmdAS400GetData_Click
        private void cmdAS400GetData_Click(object sender, RoutedEventArgs e)
        {
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            if (PCKPRFTPDataService.Instance.chkConAS400(strConAS400) == true)
                GetDataAS400();
            else
                listBox1.Items.Add(new MyListBoxItem(dte, "Can't Connect AS400", ""));
        }
        #endregion

        #region cmdExcel_Click
        private void cmdExcel_Click(object sender, RoutedEventArgs e)
        {
            ConnDataExcel();
        }
        #endregion

        #region cmdUpdateCUSNo_Click
        private void cmdUpdateCUSNo_Click(object sender, RoutedEventArgs e)
        {
            if (dgExcel.Items.Count > 0)
            {
                if (UpdateCUSNo() == true)
                {
                    List<PackingListINVNOResult> lots = new List<PackingListINVNOResult>();

                    lots = PCKPRFTPDataService.Instance.PCKPRFTP_GETINVNO();

                    if (null != lots && lots.Count > 0)
                    {
                        foreach (PackingListINVNOResult dbResult in lots)
                        {
                            if (!string.IsNullOrEmpty(dbResult.INVNO))
                            {
                                GenRunNo(dbResult.INVNO);
                            }
                        }
                    }
                }
            }
            else
            {
                "โปรดเลือก file excel".ShowMessageBox();
            }
        }
        #endregion


        private void cmdD365Insert_Click(object sender, RoutedEventArgs e)
        {
            D365InsertData();
        }

        private void cmdD365Get_Click(object sender, RoutedEventArgs e)
        {
            GetDataD365();
        }

        #endregion

        #region private Methods

        #region ClearControl

        private void ClearControl()
        {
            txtCDEL0.Text = string.Empty;
            txtINVNO.Text = string.Empty;

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

            DelDataInList();

            txtINVNO.Focus();
        }

        #endregion

        #region LoadPCKPRFTP

        private void LoadPCKPRFTP(string INVNO, string CDEL0)
        {
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            try
            {
                List<ListPCKPRFTPData> lots = new List<ListPCKPRFTPData>();

                lots = PCKPRFTPDataService.Instance.PCKPRFTP_GETDATA(INVNO, CDEL0,null);

                if (null != lots && lots.Count > 0)
                {
                    dgAS400.ItemsSource = lots;
                }
                else
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "PCKPRFTP is null", ""));
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
            }
        }

        #endregion

        #region GenRunNo
        private bool GenRunNo(string INVNO)
        {
            bool chkSave = true;
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                List<ListPCKPRFTPData> lots = new List<ListPCKPRFTPData>();

                lots = PCKPRFTPDataService.Instance.PCKPRFTP_GETDATA(INVNO, string.Empty,null);

                if (null != lots && lots.Count > 0)
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "PCKPRFTP row = " + lots.Count.ToString(), ""));

                    decimal? runNo = 0;
                    int? i = 0;
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
                                listBox1.Items.Add(new MyListBoxItem(dte, "Can't gen run No. , ROLL NO = " + _session.CDEL0, "(Error)"));
                                chkSave = false;
                                break;
                            }
                            else
                            {
                                i++;
                                //listBox1.Items.Add(new MyListBoxItem(dte, "ROLL NO = " + _session.CDEL0 +", Run No = " + runNo.ToString(), ""));
                            }
                        }
                    }

                    listBox1.Items.Add(new MyListBoxItem(dte, "จำนวน Run No ที่ update = " + i.ToString() , ""));
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
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

        #region AS400InsertData

        private void AS400InsertData()
        {
            DelDataInList();

            string where = string.Empty;

            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                listBox1.Items.Add(new MyListBoxItem(dte, "Connect AS400", ""));

                List<PCKPRFTPResult> orderByResult = PCKPRFTPDataService.Instance.GetPCKPRFTP(strConAS400, where);

                if (orderByResult != null && orderByResult.Count > 0)
                {
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
                        listBox1.Items.Add(new MyListBoxItem(dte, "AS400 table PCKPRFTP row = " + results.Count.ToString(), ""));

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
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTTRA", "(Error)"));
                                    break;
                                }
                            }

                            #endregion

                            #region DTORA

                            if (!string.IsNullOrEmpty(results[i].DTORA ))
                            {
                                try
                                {
                                    dataItemNew.DTORA = Convert.ToDecimal(results[i].DTORA);
                                }
                                catch
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTORA", "(Error)"));
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
                            dataItemNew.AS400NO = run;

                            #endregion

                            if (!string.IsNullOrEmpty(dataItemNew.INVNO) && !string.IsNullOrEmpty(dataItemNew.CDEL0))
                            {
                                if (PCKPRFTPDataService.Instance.PCKPRFTP_INSERTUPDATE(dataItemNew.ANNUL, dataItemNew.CDDIV, dataItemNew.INVTY, dataItemNew.INVNO, dataItemNew.CDORD, dataItemNew.RELNO, dataItemNew.CUSCD,
                                                dataItemNew.CUSNM, dataItemNew.RECTY, dataItemNew.CDKE1, dataItemNew.CDKE2, dataItemNew.CSITM, dataItemNew.CDCON, dataItemNew.CDEL0, dataItemNew.GRADE, dataItemNew.PIELN, dataItemNew.NETWH,
                                                dataItemNew.GRSWH, dataItemNew.GRSLN, dataItemNew.PALSZ, dataItemNew.DTTRA, dataItemNew.DTORA, dataItemNew.AS400NO, operatorid, 1) == true)
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "AS400 insert data ROLL NO. = " + dataItemNew.CDEL0, ""));

                                    if (PCKPRFTPDataService.Instance.DeletePCKPRFTP(strConAS400, dataItemNew.INVNO, dataItemNew.CDEL0) == true)
                                    {
                                        listBox1.Items.Add(new MyListBoxItem(dte, "AS400 Delete ROLL NO. = " + dataItemNew.CDEL0, ""));
                                    }
                                    else
                                    {
                                        listBox1.Items.Add(new MyListBoxItem(dte, "Can't Delete ROLL NO. = " + dataItemNew.CDEL0, "(Error)"));
                                        break;
                                    }
                                }
                                else
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Can't insert data ROLL NO. = " + dataItemNew.CDEL0, "(Error)"));
                                    break;
                                }
                            }

                            i++;
                        }

                        #endregion

                        listBox1.Items.Add(new MyListBoxItem(dte, "AS400 insert data PCKPRFTP row = " + i.ToString(), ""));
                    }
                    else
                    {
                        listBox1.Items.Add(new MyListBoxItem(dte, "AS400 PCKPRFTP row is null", ""));
                    }
                }
                else
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "AS400 PCKPRFTP row is null", ""));
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
            }
        }

        #endregion

        #region GetDataAS400

        private void GetDataAS400()
        {
            string where = string.Empty;
            //where = "Where #RECTY = 'S' ";
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                List<PCKPRFTPResult> orderByResult = PCKPRFTPDataService.Instance.GetPCKPRFTP(strConAS400, where);

                if (orderByResult != null && orderByResult.Count > 0)
                {
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
                        listBox1.Items.Add(new MyListBoxItem(dte, "AS400 table PCKPRFTP row = " + results.Count.ToString(), ""));

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
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTTRA", "(Error)"));
                                    break;
                                }
                            }

                            #endregion

                            #region DTORA

                            if (!string.IsNullOrEmpty(results[i].DTORA))
                            {
                                try
                                {
                                    dataItemNew.DTORA = Convert.ToDecimal(results[i].DTORA);
                                }
                                catch
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTORA", "(Error)"));
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
                        listBox1.Items.Add(new MyListBoxItem(dte, "AS400 PCKPRFTP row is null", ""));
                    }
                }
                else
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "AS400 PCKPRFTP row is null", ""));
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
            }
        }

        #endregion

        #endregion

        #region D365

        #region D365InsertData

        private void D365InsertData()
        {
            DelDataInList();

            string where = string.Empty;

            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                List<PCKPRFTPD365Result> orderByResult = D365DataService.Instance.GetPCKPRFTP();

                if (orderByResult != null && orderByResult.Count > 0)
                {
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
                        listBox1.Items.Add(new MyListBoxItem(dte, "D365 table PCKPRFTP row = " + results.Count.ToString(), ""));

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
                                    dataItemNew.DTTRA = results[i].DTTRA;
                                }
                                catch
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTTRA", "(Error)"));
                                    break;
                                }
                            }

                            #endregion

                            #region DTORA

                            if (results[i].DTORA != null)
                            {
                                try
                                {
                                    dataItemNew.DTORA = results[i].DTORA;
                                }
                                catch
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTORA", "(Error)"));
                                    break;
                                }
                            }

                            #endregion

                            #region D365NO

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
                            dataItemNew.AS400NO = run;

                            #endregion

                            if (!string.IsNullOrEmpty(dataItemNew.INVNO) && !string.IsNullOrEmpty(dataItemNew.CDEL0))
                            {
                                if (PCKPRFTPDataService.Instance.PCKPRFTP_D365_INSERTUPDATE(dataItemNew.ANNUL, dataItemNew.CDDIV, dataItemNew.INVTY, dataItemNew.INVNO, dataItemNew.CDORD, dataItemNew.RELNO, dataItemNew.CUSCD,
                                                dataItemNew.CUSNM, dataItemNew.RECTY, dataItemNew.CDKE1, dataItemNew.CDKE2, dataItemNew.CSITM, dataItemNew.CDCON, dataItemNew.CDEL0, dataItemNew.GRADE, dataItemNew.PIELN, dataItemNew.NETWH,
                                                dataItemNew.GRSWH, dataItemNew.GRSLN, dataItemNew.PALSZ, dataItemNew.DTTRA, dataItemNew.DTORA, dataItemNew.AS400NO, operatorid, 1) == true)
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "D365 insert data ROLL NO. = " + dataItemNew.CDEL0, ""));

                                    if (string.IsNullOrEmpty(D365DataService.Instance.DelPCKPRFTP(dataItemNew.INVNO, dataItemNew.CDEL0)))
                                    {
                                        listBox1.Items.Add(new MyListBoxItem(dte, "D365 Delete ROLL NO. = " + dataItemNew.CDEL0, ""));
                                    }
                                    else
                                    {
                                        listBox1.Items.Add(new MyListBoxItem(dte, "Can't Delete ROLL NO. = " + dataItemNew.CDEL0, "(Error)"));
                                        break;
                                    }
                                }
                                else
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Can't insert data ROLL NO. = " + dataItemNew.CDEL0, "(Error)"));
                                    break;
                                }
                            }

                            i++;
                        }

                        #endregion

                        listBox1.Items.Add(new MyListBoxItem(dte, "D365 insert data PCKPRFTP row = " + i.ToString(), ""));
                    }
                    else
                    {
                        listBox1.Items.Add(new MyListBoxItem(dte, "D365 PCKPRFTP row is null", ""));
                    }
                }
                else
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "D365 PCKPRFTP row is null", ""));
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
            }
        }

        #endregion

        #region GetDataD365
        /// <summary>
        /// 
        /// </summary>
        private void GetDataD365()
        {
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                List<PCKPRFTPD365Result> orderByResult = D365DataService.Instance.GetPCKPRFTP();

                if (orderByResult != null && orderByResult.Count > 0)
                {
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
                        listBox1.Items.Add(new MyListBoxItem(dte, "D365 table PCKPRFTP row = " + results.Count.ToString(), ""));

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
                                    dataItemNew.DTTRA = results[i].DTTRA;
                                }
                                catch
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTTRA", "(Error)"));
                                    break;
                                }
                            }

                            #endregion

                            #region DTORA

                            if (results[i].DTORA != null)
                            {
                                try
                                {
                                    dataItemNew.DTORA = results[i].DTORA;
                                }
                                catch
                                {
                                    listBox1.Items.Add(new MyListBoxItem(dte, "Error DTORA", "(Error)"));
                                    break;
                                }
                            }

                            #endregion

                            #region D365NO

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
                        listBox1.Items.Add(new MyListBoxItem(dte, "D365 PCKPRFTP row is null", ""));
                    }
                }
                else
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "D365 PCKPRFTP = Null", ""));
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
            }
        }

        #endregion

        #endregion

        #region load excel

        #region ConnDataExcel
        private bool ConnDataExcel()
        {
            bool chkData = true;
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

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
                                ex.Message.ToString().Err();
                                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
                                chkData = false;
                            }
                        }
                    }
                   
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                    if (dataList != null && dataList.Count > 0)
                    {
                        this.dgExcel.ItemsSource = dataList;
                        listBox1.Items.Add(new MyListBoxItem(dte, "Excel file row = " + dataList.Count.ToString(), ""));
                    }
                    else
                    {
                        this.dgExcel.ItemsSource = null;
                        listBox1.Items.Add(new MyListBoxItem(dte, "Excel file data null", "(Error)"));
                    }
                        

                    return chkData;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
                return false;
            }
        }
        #endregion

        #endregion

        #region UpdateCUSNo
        private bool UpdateCUSNo()
        {
            bool chkData = true;
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                if (dgExcel.Items.Count > 0)
                {
                    int o = 0;
                    foreach (var row in dgExcel.Items)
                    {
                        PackingListResult dataItem = new PackingListResult();

                        dataItem.RollNo = ((PackingListResult)(dgExcel).Items[o]).RollNo;
                        dataItem.CusRunNo = ((PackingListResult)(dgExcel).Items[o]).CusRunNo;

                        if (!string.IsNullOrEmpty(dataItem.RollNo))
                        {
                            PCKPRFTPDataService.Instance.PCKPRFTP_UPDATECUSNO(dataItem.RollNo, dataItem.CusRunNo, operatorid,0);
                        }
                        else
                        {
                            listBox1.Items.Add(new MyListBoxItem(dte, "Can't update CUS No. , Roll No. = " + dataItem.RollNo, "(Error)"));
                            break;
                        }

                        o++;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
                chkData = false;
            }

            return chkData;
        }
        #endregion

        #region Delete
        private bool Delete(string invNo, string cdel0)
        {
            bool chkData = true;
            string dte = string.Empty;
            dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

            try
            {
                List<ListPCKPRFTPData> lots = new List<ListPCKPRFTPData>();

                lots = PCKPRFTPDataService.Instance.PCKPRFTP_GETDATA(invNo, cdel0, null);

                if (null != lots && lots.Count > 0)
                {
                    if (PCKPRFTPDataService.Instance.PCKPRFTP_UPDATEINUSE(invNo, cdel0, operatorid, 3) == true)
                    {
                        listBox1.Items.Add(new MyListBoxItem(dte, "Delete DO NO. = " + invNo, ""));
                    }
                    else
                    {
                        ("Can't delete DO NO. = " + invNo).Err();
                        listBox1.Items.Add(new MyListBoxItem(dte, "Can't delete DO NO. = " + invNo, "(Error)"));
                    }
                }
                else
                {
                    listBox1.Items.Add(new MyListBoxItem(dte, "Data is null", ""));
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
                chkData = false;
            }

            return chkData;
        }
        #endregion

        #region MyListBoxItem
        public class MyListBoxItem
        {
            public MyListBoxItem(string dte, string mes, string sta)
            {
                Date = dte;
                Message = mes;
                Status = sta;
            }
            public string Date { get; set; }
            public string Message { get; set; }
            public string Status { get; set; }
        }
        #endregion

        #region DelDataInList
        private void DelDataInList()
        {
            try
            {
                int count = listBox1.Items.Count;

                if (count >= 50)
                {
                    listBox1.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                string dte = string.Empty;
                dte = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                listBox1.Items.Add(new MyListBoxItem(dte, ex.Message.ToString(), "(Error)"));
            }
        }
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
