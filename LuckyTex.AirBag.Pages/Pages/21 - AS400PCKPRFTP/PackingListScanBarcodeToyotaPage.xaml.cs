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
using System.Windows.Threading;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for PackingListScanBarcodeToyotaPage.xaml
    /// </summary>
    public partial class PackingListScanBarcodeToyotaPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PackingListScanBarcodeToyotaPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        List<ListPCKPRFTPData> results = new List<ListPCKPRFTPData>();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        TimeSpan start = new TimeSpan();
        TimeSpan end = new TimeSpan();
        string operatorid = string.Empty;
       
        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();
            TimerSendData();

            if (dispatcherTimer.IsEnabled == true)
            {
                dispatcherTimer.IsEnabled = false;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
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
                if (chkCheckShipping.IsChecked == true)
                {
                    txtShippingMark.Focus();
                    txtShippingMark.SelectAll();
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtCDEL0.Text))
                    {
                        LoadPCKPRFTP(txtCDEL0.Text);
                    }
                }
            }
        }

        #endregion

        #region txtShippingMark_KeyDown

        private void txtShippingMark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (chkCheckShipping.IsChecked == true)
                {
                    if (!string.IsNullOrEmpty(txtCDEL0.Text) && !string.IsNullOrEmpty(txtShippingMark.Text))
                    {
                        LoadPCKPRFTP(txtCDEL0.Text, txtShippingMark.Text);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtCDEL0.Text))
                        {
                            txtCDEL0.Focus();
                            txtCDEL0.SelectAll();
                        }
                        else if (string.IsNullOrEmpty(txtShippingMark.Text))
                        {
                            "Please insert Shipping Mark No".ShowMessageBox();
                            txtShippingMark.Focus();
                            txtShippingMark.SelectAll();
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region CheckBox

        #region chkCheckShipping_Checked
        private void chkCheckShipping_Checked(object sender, RoutedEventArgs e)
        {
            labShippingMark.Visibility = Visibility.Visible;
            txtShippingMark.Visibility = Visibility.Visible;
        }
        #endregion

        #region chkCheckShipping_Unchecked
        private void chkCheckShipping_Unchecked(object sender, RoutedEventArgs e)
        {
            labShippingMark.Visibility = Visibility.Collapsed;
            txtShippingMark.Text = string.Empty;
            txtShippingMark.Visibility = Visibility.Collapsed;
        }
        #endregion

        #endregion

        #region Button Handlers

        #region cmdBack_Click

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #endregion

        #region private Methods

        #region ClearControl

        private void ClearControl()
        {
            txtCDEL0.Text = string.Empty;
            txtRUNNO.Text = string.Empty;
            txtRUNNO.Background = new SolidColorBrush(Colors.White);

            chkCheckShipping.IsChecked = false;
            txtShippingMark.Text = string.Empty;
            txtShippingMark.Visibility = Visibility.Collapsed;
            labShippingMark.Visibility = Visibility.Collapsed;

            imgTruePIELN.Visibility = Visibility.Collapsed;
            imgFalsePIELN.Visibility = Visibility.Collapsed;
            imgTrueNETWH.Visibility = Visibility.Collapsed;
            imgFalseNETWH.Visibility = Visibility.Collapsed;
            imgTrueGRSLN.Visibility = Visibility.Collapsed;
            imgFalseGRSLN.Visibility = Visibility.Collapsed;
            imgTrueGRSWH.Visibility = Visibility.Collapsed;
            imgFalseGRSWH.Visibility = Visibility.Collapsed;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.dgAS400.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.dgAS400.SelectedItems.Clear();
            else
                this.dgAS400.SelectedItem = null;

            dgAS400.ItemsSource = null;

            results = new List<ListPCKPRFTPData>();

            txtCDEL0.Focus();
        }

        #endregion

        #region LoadPCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CDEL0"></param>
        private void LoadPCKPRFTP(string CDEL0)
        {
            try
            {
                ListPCKPRFTPData lots = new  ListPCKPRFTPData();

                lots = PCKPRFTPDataService.Instance.PCKPRFTP_SCANBARCODE(CDEL0);

                imgTruePIELN.Visibility = Visibility.Collapsed;
                imgFalsePIELN.Visibility = Visibility.Collapsed;
                imgTrueNETWH.Visibility = Visibility.Collapsed;
                imgFalseNETWH.Visibility = Visibility.Collapsed;
                imgTrueGRSLN.Visibility = Visibility.Collapsed;
                imgFalseGRSLN.Visibility = Visibility.Collapsed;
                imgTrueGRSWH.Visibility = Visibility.Collapsed;
                imgFalseGRSWH.Visibility = Visibility.Collapsed;

                if (null != lots)
                {
                    start = DateTime.Now.TimeOfDay;
                    end = DateTime.Now.AddMinutes(2).TimeOfDay;

                    if (lots.RUNNO != null)
                    {
                        txtRUNNO.Text = lots.RUNNO.ToString();

                        #region CHKNETLENGTH
                        if (lots.CHKNETLENGTH == 1)
                        {
                            imgTruePIELN.Visibility = Visibility.Visible;
                            imgFalsePIELN.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTruePIELN.Visibility = Visibility.Collapsed;
                            imgFalsePIELN.Visibility = Visibility.Visible;
                        }
                        #endregion

                        #region CHKNETWEIGHT
                        if (lots.CHKNETWEIGHT == 1)
                        {
                            imgTrueNETWH.Visibility = Visibility.Visible;
                            imgFalseNETWH.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTrueNETWH.Visibility = Visibility.Collapsed;
                            imgFalseNETWH.Visibility = Visibility.Visible;
                        }
                        #endregion

                        #region CHKGROSSLENGTH
                        if (lots.CHKGROSSLENGTH == 1)
                        {
                            imgTrueGRSLN.Visibility = Visibility.Visible;
                            imgFalseGRSLN.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTrueGRSLN.Visibility = Visibility.Collapsed;
                            imgFalseGRSLN.Visibility = Visibility.Visible;
                        }
                        #endregion

                        #region CHKGROSSWEIGHT
                        if (lots.CHKGROSSWEIGHT == 1)
                        {
                            imgTrueGRSWH.Visibility = Visibility.Visible;
                            imgFalseGRSWH.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTrueGRSWH.Visibility = Visibility.Collapsed;
                            imgFalseGRSWH.Visibility = Visibility.Visible;
                        }
                        #endregion

                        if (results.Exists(x => x.CDEL0 == lots.CDEL0) == false)
                        {
                            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                            if (this.dgAS400.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                this.dgAS400.SelectedItems.Clear();
                            else
                                this.dgAS400.SelectedItem = null;

                            dgAS400.ItemsSource = null;
                            results.Add(lots);
                            dgAS400.ItemsSource = results;
                        }
                        else
                        {
                            //txtRUNNO.Background = new SolidColorBrush(Colors.LightGreen);

                            //if (Save(lots.INVNO, lots.CDEL0,null) == true)
                            //{
                            //    CheckScan(lots.INVNO, lots.CDEL0);
                            //}
                            //else
                            //{
                            //    txtRUNNO.Text = "Error";
                            //    txtRUNNO.Background = new SolidColorBrush(Colors.Red);
                            //}
                        }
                    }
                    else
                    {
                        txtRUNNO.Text = "NULL";
                    }
                }

                if (!string.IsNullOrEmpty(txtCDEL0.Text))
                {
                    if (dispatcherTimer.IsEnabled == false)
                    {
                        dispatcherTimer.IsEnabled = true;
                    }

                    txtCDEL0.Text = string.Empty;
                    txtCDEL0.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region LoadPCKPRFTP
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CDEL0"></param>
        /// <param name="ShippingMark"></param>
        private void LoadPCKPRFTP(string CDEL0, string ShippingMark)
        {
            try
            {
                ListPCKPRFTPData lots = new ListPCKPRFTPData();

                decimal? shippingNo = null;
                if (!string.IsNullOrEmpty(ShippingMark))
                {
                    shippingNo = decimal.Parse(ShippingMark);
                }

                lots = PCKPRFTPDataService.Instance.PCKPRFTP_SCANBARCODE(string.Empty,CDEL0, shippingNo);

                imgTruePIELN.Visibility = Visibility.Collapsed;
                imgFalsePIELN.Visibility = Visibility.Collapsed;
                imgTrueNETWH.Visibility = Visibility.Collapsed;
                imgFalseNETWH.Visibility = Visibility.Collapsed;
                imgTrueGRSLN.Visibility = Visibility.Collapsed;
                imgFalseGRSLN.Visibility = Visibility.Collapsed;
                imgTrueGRSWH.Visibility = Visibility.Collapsed;
                imgFalseGRSWH.Visibility = Visibility.Collapsed;

                if (null != lots)
                {
                    start = DateTime.Now.TimeOfDay;
                    end = DateTime.Now.AddMinutes(2).TimeOfDay;

                    if (lots.RUNNO != null)
                    {
                        txtRUNNO.Text = lots.RUNNO.ToString();

                        #region CHKNETLENGTH
                        if (lots.CHKNETLENGTH == 1)
                        {
                            imgTruePIELN.Visibility = Visibility.Visible;
                            imgFalsePIELN.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTruePIELN.Visibility = Visibility.Collapsed;
                            imgFalsePIELN.Visibility = Visibility.Visible;
                        }
                        #endregion

                        #region CHKNETWEIGHT
                        if (lots.CHKNETWEIGHT == 1)
                        {
                            imgTrueNETWH.Visibility = Visibility.Visible;
                            imgFalseNETWH.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTrueNETWH.Visibility = Visibility.Collapsed;
                            imgFalseNETWH.Visibility = Visibility.Visible;
                        }
                        #endregion

                        #region CHKGROSSLENGTH
                        if (lots.CHKGROSSLENGTH == 1)
                        {
                            imgTrueGRSLN.Visibility = Visibility.Visible;
                            imgFalseGRSLN.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTrueGRSLN.Visibility = Visibility.Collapsed;
                            imgFalseGRSLN.Visibility = Visibility.Visible;
                        }
                        #endregion

                        #region CHKGROSSWEIGHT
                        if (lots.CHKGROSSWEIGHT == 1)
                        {
                            imgTrueGRSWH.Visibility = Visibility.Visible;
                            imgFalseGRSWH.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            imgTrueGRSWH.Visibility = Visibility.Collapsed;
                            imgFalseGRSWH.Visibility = Visibility.Visible;
                        }
                        #endregion

                        if (txtRUNNO.Text == ShippingMark)
                        {
                            if (results.Exists(x => x.CDEL0 == lots.CDEL0) == false)
                            {
                                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                                if (this.dgAS400.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                    this.dgAS400.SelectedItems.Clear();
                                else
                                    this.dgAS400.SelectedItem = null;

                                dgAS400.ItemsSource = null;
                                results.Add(lots);
                                dgAS400.ItemsSource = results;

                                txtRUNNO.Background = new SolidColorBrush(Colors.LightGreen);

                                if (Save(lots.INVNO, lots.CDEL0, shippingNo) == true)
                                {
                                    CheckScan(lots.INVNO, lots.CDEL0);
                                }
                                else
                                {
                                    txtRUNNO.Text = "Error";
                                    txtRUNNO.Background = new SolidColorBrush(Colors.Red);
                                }
                            }
                            else
                            {
                                txtRUNNO.Background = new SolidColorBrush(Colors.LightGreen);

                                if (Save(lots.INVNO, lots.CDEL0, shippingNo) == true)
                                {
                                    CheckScan(lots.INVNO, lots.CDEL0);
                                }
                                else
                                {
                                    txtRUNNO.Text = "Error";
                                    txtRUNNO.Background = new SolidColorBrush(Colors.Red);
                                }
                            }
                        }
                        else
                        {
                            txtRUNNO.Text = "NO";
                            txtRUNNO.Background = new SolidColorBrush(Colors.Red);
                        }
                    }
                    else
                    {
                        txtRUNNO.Text = "NULL";
                        txtRUNNO.Background = new SolidColorBrush(Colors.Red);
                    }
                }

                if (!string.IsNullOrEmpty(txtCDEL0.Text))
                {
                    if (dispatcherTimer.IsEnabled == false)
                    {
                        dispatcherTimer.IsEnabled = true;
                    }

                    txtShippingMark.Text = string.Empty;
                    txtCDEL0.Text = string.Empty;
                    txtCDEL0.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region Save
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="cdel0"></param>
        /// <returns></returns>
        private bool Save(string invNo, string cdel0, decimal? runno)
        {
            bool chkSave = false;

            try
            {
                if (PCKPRFTPDataService.Instance.PCKPRFTP_UPDATESCAN(invNo, cdel0, operatorid, runno) == false)
                {
                    chkSave = false;
                }
                else
                {
                    chkSave = true;
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

        #region CheckScan

        private void CheckScan(string invNo, string cdel0)
        {
            try
            {
                List<LuckyTex.Models.ListPCKPRFTPData> dataList = new List<LuckyTex.Models.ListPCKPRFTPData>();

                int o = 0;
                foreach (var row in dgAS400.Items)
                {
                    LuckyTex.Models.ListPCKPRFTPData dataItem = new LuckyTex.Models.ListPCKPRFTPData();

                    dataItem.CDDIV = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CDDIV;
                    dataItem.INVTY = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).INVTY;
                    dataItem.INVNO = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).INVNO;
                    dataItem.CDORD = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CDORD;
                    dataItem.RELNO = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).RELNO;
                    dataItem.CUSCD = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CUSCD;
                    dataItem.CUSNM = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CUSNM;
                    dataItem.RECTY = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).RECTY;
                    dataItem.CDKE1 = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CDKE1;
                    dataItem.CDKE2 = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CDKE2;
                    dataItem.CSITM = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CSITM;
                    dataItem.CDCON = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CDCON;
                    dataItem.CDEL0 = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CDEL0;
                    dataItem.GRADE = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).GRADE;
                    dataItem.PIELN = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).PIELN;
                    dataItem.NETWH = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).NETWH;
                    dataItem.GRSWH = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).GRSWH;
                    dataItem.GRSLN = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).GRSLN;
                    dataItem.PALSZ = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).PALSZ;
                    dataItem.DTTRA = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).DTTRA;
                    dataItem.DTORA = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).DTORA;
                    dataItem.RUNNO = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).RUNNO;
                    dataItem.AS400NO = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).AS400NO;
                    dataItem.CUSNO = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CUSNO;

                    dataItem.INSERTBY = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).INSERTBY;
                    dataItem.INSERTDATE = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).INSERTDATE;
                    dataItem.EDITBY = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).EDITBY;
                    dataItem.EDITDATE = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).EDITDATE;
                    dataItem.INUSE = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).INUSE;

                    dataItem.SCANBY = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).SCANBY;
                    dataItem.SCANDATE = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).SCANDATE;

                    if (((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).ImgScanFlag != null)
                    {
                        Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                        BitmapImage source = new BitmapImage(uri);
                        dataItem.ImgScanFlag = source;
                    }
                    else
                    {
                        if (dataItem.INVNO == invNo && dataItem.CDEL0 == cdel0)
                        {
                            Uri uri = new Uri("pack://application:,,,/Luckytex.PackingList;component/Images/Check.png");
                            BitmapImage source = new BitmapImage(uri);
                            dataItem.ImgScanFlag = source;
                        }
                    }

                    #region CHKNETWEIGHT

                    dataItem.CHKNETWEIGHT = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CHKNETWEIGHT;
                    dataItem.ImgChkNetWeight = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).ImgChkNetWeight;

                    #endregion

                    #region CHKGROSSWEIGHT

                    dataItem.CHKGROSSWEIGHT = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CHKGROSSWEIGHT;
                    dataItem.ImgChkGrossWeight = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).ImgChkGrossWeight;

                    #endregion

                    #region CHKNETLENGTH

                    dataItem.CHKNETLENGTH = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CHKNETLENGTH;
                    dataItem.ImgChkNetLength = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).ImgChkNetLength;

                    #endregion

                    #region CHKGROSSLENGTH

                    dataItem.CHKGROSSLENGTH = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).CHKGROSSLENGTH;
                    dataItem.ImgChkGrossLength = ((LuckyTex.Models.ListPCKPRFTPData)((dgAS400.Items)[o])).ImgChkGrossLength;

                    #endregion

                    o++;

                    dataList.Add(dataItem);
                }

                results.Clear();
                results = dataList;

                this.dgAS400.ItemsSource = results;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Timer
        #region dispatcherTimer_Tick
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                TimeSpan now = DateTime.Now.TimeOfDay;

                if (TimeBetween(now, start, end) == true)
                {
                    if (!string.IsNullOrEmpty(txtRUNNO.Text))
                    {
                        txtRUNNO.Text = string.Empty;
                        dispatcherTimer.IsEnabled = false;
                        txtRUNNO.Background = new SolidColorBrush(Colors.White);

                        imgTruePIELN.Visibility = Visibility.Collapsed;
                        imgFalsePIELN.Visibility = Visibility.Collapsed;
                        imgTrueNETWH.Visibility = Visibility.Collapsed;
                        imgFalseNETWH.Visibility = Visibility.Collapsed;
                        imgTrueGRSLN.Visibility = Visibility.Collapsed;
                        imgFalseGRSLN.Visibility = Visibility.Collapsed;
                        imgTrueGRSWH.Visibility = Visibility.Collapsed;
                        imgFalseGRSWH.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                dispatcherTimer.Stop();
            }
        }
        #endregion

        #region TimerSendData
        private void TimerSendData()
        {
            try
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region TimeBetween
        private bool TimeBetween(TimeSpan now, TimeSpan start, TimeSpan end)
        {
            try
            {
                if (start <= now && now <= end)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
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