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
    /// Interaction logic for ReceiveYARNPage.xaml
    /// </summary>
    public partial class ReceiveYARNPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReceiveYARNPage()
        {
            InitializeComponent();

            ConAS400();

            cmdVerify.Visibility = System.Windows.Visibility.Collapsed;

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private ListG3_YarnData _session = new ListG3_YarnData();
        private G3Session _sessionG3 = new G3Session();

        List<LuckyTex.Models.ListG3_YarnData> listG3_Yarn = new List<LuckyTex.Models.ListG3_YarnData>();

        string strConAS400 = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtTotalPallet.Text = "0";
            ClearControl();
            EnabledControl(false);
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

        #region txtPalletNo_KeyDown

        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtPalletNo.Text))
                {
                    string palletNo = CutBarcode(txtPalletNo.Text);

                    if (ChkPalletNoInList(palletNo) == false)
                    {
                        LoadPalletNoData(palletNo);
                    }
                    else
                    {
                        "Pattet No have in use".ShowMessageBox(true);

                        ClearControl();

                        txtPalletNo.Focus();
                        txtPalletNo.SelectAll();
                        e.Handled = true;
                    }
                }
            }
        }

        #endregion

        #region txtPalletNo_LostFocus

        private void txtPalletNo_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtPalletNo.Text))
            //{
            //    if (ChkPalletNoInList(txtPalletNo.Text) == false)
            //    {
            //        LoadPalletNoData();
            //    }
            //    else
            //    {
            //        "Pattet No have in use".ShowMessageBox(true);
            //    }
            //}
        }

        #endregion

        #endregion

        #region CheckBox

        #region rbOK_Checked

        private void rbOK_Checked(object sender, RoutedEventArgs e)
        {
            if (rbOK.IsChecked == true)
            {
                if (txtOther != null && txtAction != null)
                {
                    txtOther.IsEnabled = false;
                    txtOther.Text = "";
                    txtAction.IsEnabled = false;
                    txtAction.Text = "";

                    chkPackaging.IsEnabled = false;
                    chkClean.IsEnabled = false;
                    chkTearing.IsEnabled = false;
                    chkFalldown.IsEnabled = false;
                    chkCertification.IsEnabled = false;
                    chkInvoice.IsEnabled = false;
                    chkIdentifyarea.IsEnabled = false;
                    chkAmountpallet.IsEnabled = false;

                    chkPackaging.IsChecked = false;
                    chkClean.IsChecked = false;
                    chkTearing.IsChecked = false;
                    chkFalldown.IsChecked = false;
                    chkCertification.IsChecked = false;
                    chkInvoice.IsChecked = false;
                    chkIdentifyarea.IsChecked = false;
                    chkAmountpallet.IsChecked = false;
                }
            }
        }

        #endregion

        #region rbNG_Checked

        private void rbNG_Checked(object sender, RoutedEventArgs e)
        {
            if (rbNG.IsChecked == true)
            {
                if (txtOther != null && txtAction != null)
                {
                    txtOther.IsEnabled = true;
                    txtAction.IsEnabled = true;

                    chkPackaging.IsEnabled = true;
                    chkClean.IsEnabled = true;
                    chkTearing.IsEnabled = true;
                    chkFalldown.IsEnabled = true;
                    chkCertification.IsEnabled = true;
                    chkInvoice.IsEnabled = true;
                    chkIdentifyarea.IsEnabled = true;
                    chkAmountpallet.IsEnabled = true;
                }
            }
        }

        #endregion

        #endregion

        #region gridG3_SelectedCellsChanged

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

        private void gridG3_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridG3.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridG3);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            EnabledControl(false);

                            #region PalletNo

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).PalletNo != null)
                            {
                                _session.PalletNo = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).PalletNo;
                            }
                            else
                            {
                                _session.PalletNo = "";
                            }

                            #endregion

                            #region Lotorderno

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).LotorderNo != null)
                            {
                                _session.LotorderNo = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).LotorderNo;
                            }
                            else
                            {
                                _session.LotorderNo = "";
                            }

                            #endregion

                            #region Itm_Yarn

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Itm_Yarn != null)
                            {
                                _session.Itm_Yarn = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Itm_Yarn;
                            }
                            else
                            {
                                _session.Itm_Yarn = "";
                            }

                            #endregion

                            #region WeightQty

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).WeightQty != null)
                            {
                                try
                                {
                                    _session.WeightQty = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).WeightQty;
                                }
                                catch
                                {
                                    _session.WeightQty = 0;
                                }
                            }
                            else
                            {
                                _session.WeightQty = 0;
                            }

                            #endregion

                            #region VerifyOK

                            try
                            {
                                _session.VerifyOK = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).VerifyOK;
                            }
                            catch
                            {
                                _session.VerifyOK = false;
                            }

                            #endregion

                            #region VerifyNG

                            try
                            {
                                _session.VerifyNG = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).VerifyNG;
                            }
                            catch
                            {
                                _session.VerifyNG = false;
                            }

                            #endregion

                            #region Packaging

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Packaging != null)
                            {
                                _session.Packaging = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Packaging;
                            }
                            else
                            {
                                _session.Packaging = "0";
                            }

                            #endregion

                            #region Clean

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Clean != null)
                            {
                                _session.Clean = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Clean;
                            }
                            else
                            {
                                _session.Clean = "0";
                            }

                            #endregion

                            #region Tearing

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Tearing != null)
                            {
                                _session.Tearing = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Tearing;
                            }
                            else
                            {
                                _session.Tearing = "0";
                            }

                            #endregion

                            #region Falldown

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Falldown != null)
                            {
                                _session.Falldown = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Falldown;
                            }
                            else
                            {
                                _session.Falldown = "0";
                            }

                            #endregion

                            #region Certification

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Certification != null)
                            {
                                _session.Certification = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Certification;
                            }
                            else
                            {
                                _session.Certification = "0";
                            }

                            #endregion

                            #region Invoice

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Invoice != null)
                            {
                                _session.Invoice = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Invoice;
                            }
                            else
                            {
                                _session.Invoice = "0";
                            }

                            #endregion

                            #region Identifyarea

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Identifyarea != null)
                            {
                                _session.Identifyarea = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Identifyarea;
                            }
                            else
                            {
                                _session.Identifyarea = "0";
                            }

                            #endregion

                            #region Amountpallet

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Amountpallet != null)
                            {
                                _session.Amountpallet = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Amountpallet;
                            }
                            else
                            {
                                _session.Amountpallet = "0";
                            }

                            #endregion

                            #region Other

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Other != null)
                            {
                                _session.Other = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Other;
                            }
                            else
                            {
                                _session.Other = "";
                            }

                            #endregion

                            #region Action

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Action != null)
                            {
                                _session.Action = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).Action;
                            }
                            else
                            {
                                _session.Action = "";
                            }

                            #endregion

                            #region YarnType

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).YarnType != null)
                            {
                                _session.YarnType = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).YarnType;
                            }
                            else
                            {
                                _session.YarnType = "";
                            }

                            #endregion

                            #region TraceNo

                            if (((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).TraceNo != null)
                            {
                                _session.TraceNo = ((LuckyTex.Models.ListG3_YarnData)(gridG3.CurrentCell.Item)).TraceNo;
                            }
                            else
                            {
                                _session.TraceNo = "";
                            }

                            #endregion

                            if (!string.IsNullOrEmpty(_session.PalletNo))
                            {
                                LoadDataEdit();

                                rbOK.IsEnabled = true;
                                rbNG.IsEnabled = true;
                            }

                            cmdEdit.IsEnabled = true;
                            cmdDelete.IsEnabled = true;

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

        #region cmdReceive_Click

        private void cmdReceive_Click(object sender, RoutedEventArgs e)
        {
            if (txtPalletNo.Text != "")
            {
                if (CheckPalletNo(txtPalletNo.Text) == true)
                {
                    if (AddDataInGrid() == true)
                    {
                        ClearControl();
                        EnabledControl(false);
                    }
                }
                else
                {
                    "Pallet No had in DataGrid".ShowMessageBox();

                    ClearControl();
                    EnabledControl(false);
                }

                CalTotal();
            }
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
            EnabledControl(false);

            gridG3.IsEnabled = true;
            cmdReceive.IsEnabled = true;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridG3.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridG3.SelectedItems.Clear();
            else
                this.gridG3.SelectedItem = null;

            CalTotal();
        }

        #endregion

        #region cmdEdit_Click

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            //EnabledControl(true);
            //LoadDataEdit();

            if (txtPalletNo.Text != "")
            {
                if (gridG3.ItemsSource != null)
                {
                    if (gridG3.Items.Count > 0)
                    {
                        if (DeleteDataInGrid() == true)
                        {
                            if (AddDataInGrid() == true)
                            {
                                ClearControl();
                                EnabledControl(false);

                                gridG3.IsEnabled = true;
                                cmdReceive.IsEnabled = true;

                                txtTotalPallet.Text = "0";

                                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                                if (this.gridG3.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                    this.gridG3.SelectedItems.Clear();
                                else
                                    this.gridG3.SelectedItem = null;
                            }
                        }
                    }
                }

                CalTotal();
            }
        }

        #endregion

        #region cmdVerify_Click

        private void cmdVerify_Click(object sender, RoutedEventArgs e)
        {
            if (txtPalletNo.Text != "")
            {
                if (gridG3.ItemsSource != null)
                {
                    if (gridG3.Items.Count > 0)
                    {
                        if (DeleteDataInGrid() == true)
                        {
                            if (AddDataInGrid() == true)
                            {
                                ClearControl();
                                EnabledControl(false);

                                gridG3.IsEnabled = true;
                                cmdReceive.IsEnabled = true;

                                txtTotalPallet.Text = "0";

                                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                                if (this.gridG3.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                    this.gridG3.SelectedItems.Clear();
                                else
                                    this.gridG3.SelectedItem = null;
                            }
                        }
                    }
                }

                CalTotal();
            }
        }

        #endregion

        #region cmdDelete_Click

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DeleteDataInGrid() == true)
            {
                ClearControl();

                cmdReceive.IsEnabled = true;
                cmdClear.IsEnabled = true;

                cmdEdit.IsEnabled = false;
                cmdDelete.IsEnabled = false;
            }

            CalTotal();
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveG3_UPDATEYARN() == true)
            {
                _session = new ListG3_YarnData();
                _sessionG3 = new G3Session();
                listG3_Yarn = new List<LuckyTex.Models.ListG3_YarnData>();
            }
        }

        #endregion

        #region cmdAS400_Click

        private void cmdAS400_Click(object sender, RoutedEventArgs e)
        {
            GetDataAS400();
        }

        #endregion

        private void cmdD365_Click(object sender, RoutedEventArgs e)
        {
            GetDataD365();
        }

        #endregion

        #region private Methods

        #region ClearControl

        private void ClearControl()
        {
            txtPalletNo.Text = "";
            txtLot.Text = "";
            txtYarnType.Text = "";
            txtWeight.Text = "";
            txtTraceNo.Text = "";
            txtITM_YARN.Text = "";

            rbOK.IsChecked = true;
            rbNG.IsChecked = false;

            chkPackaging.IsEnabled = false;
            chkClean.IsEnabled = false;
            chkTearing.IsEnabled = false;
            chkFalldown.IsEnabled = false;
            chkCertification.IsEnabled = false;
            chkInvoice.IsEnabled = false;
            chkIdentifyarea.IsEnabled = false;
            chkAmountpallet.IsEnabled = false;

            chkPackaging.IsChecked = false;
            chkClean.IsChecked = false;
            chkTearing.IsChecked = false;
            chkFalldown.IsChecked = false;
            chkCertification.IsChecked = false;
            chkInvoice.IsChecked = false;
            chkIdentifyarea.IsChecked = false;
            chkAmountpallet.IsChecked = false;

            txtPalletNo.Focus();
        }

        #endregion

        #region EnabledControl

        private void EnabledControl(bool chkEnabled)
        {
            rbOK.IsEnabled = chkEnabled;
            rbNG.IsEnabled = chkEnabled;

            //chkPackaging.IsEnabled = chkEnabled;
            //chkClean.IsEnabled = chkEnabled;
            //chkTearing.IsEnabled = chkEnabled;
            //chkFalldown.IsEnabled = chkEnabled;
            //chkCertification.IsEnabled = chkEnabled;
            //chkInvoice.IsEnabled = chkEnabled;
            //chkIdentifyarea.IsEnabled = chkEnabled;
            //chkAmountpallet.IsEnabled = chkEnabled;

            cmdEdit.IsEnabled = chkEnabled;
            cmdVerify.IsEnabled = chkEnabled;
            cmdDelete.IsEnabled = chkEnabled;
        }

        #endregion

        #region LoadPalletNoData

        private void LoadPalletNoData(string palletNo)
        {
            string _palletNo = string.Empty;

            if (!string.IsNullOrEmpty(palletNo))
            {
                _palletNo = palletNo;
            }

            if (_palletNo != "")
            {
                List<G3_SEARCHBYPALLETNOSearchData> lots = new List<G3_SEARCHBYPALLETNOSearchData>();

                lots = G3DataService.Instance.G3_SearchByPalletNoDataList(_palletNo);

                if (null != lots )
                {
                    if (lots.Count > 0 && null != lots[0])
                    {
                        //เพิ่มใหม่ PALLETNO
                        txtPalletNo.Text = lots[0].PALLETNO;

                        txtLot.Text = lots[0].LOTNO;
                        txtYarnType.Text = lots[0].YARNTYPE;
                        
                        txtTraceNo.Text = lots[0].TRACENO;
                        txtITM_YARN.Text = lots[0].ITM_YARN;

                        txtWeight.Text = lots[0].WEIGHTQTY.Value.ToString("#,##0.##");

                        rbOK.IsChecked = true;

                        EnabledControl(true);

                        rbOK.Focus();
                    }
                    else
                    {
                        string ErrPalletNo = "Pattet No " + _palletNo + " Not Found in Database";
                        ErrPalletNo.ShowMessageBox(true);

                        ClearControl();
                        EnabledControl(false);

                        txtPalletNo.Focus();
                        txtPalletNo.SelectAll();
                    }
                }
                else
                {
                    string ErrPalletNo = "Pattet No " + _palletNo + " Not Found in Database";
                    ErrPalletNo.ShowMessageBox(false);

                    ClearControl();
                    EnabledControl(true);

                    txtPalletNo.Focus();
                    txtPalletNo.SelectAll();
                }
            }
        }

        #endregion

        #region AddDataInGrid

        private bool AddDataInGrid()
        {
            bool chkAdd = false;

            try
            {
                this.gridG3.ItemsSource = null;

                LuckyTex.Models.ListG3_YarnData dataItem = new ListG3_YarnData();

                #region PalletNo

                if (txtPalletNo.Text != "")
                    dataItem.PalletNo = txtPalletNo.Text;

                #endregion

                #region Lotorderno

                if (txtLot.Text != "")
                    dataItem.LotorderNo = txtLot.Text;

                #endregion

                #region Verify

                if (rbOK.IsChecked == true && rbNG.IsChecked == false)
                {
                    dataItem.Verify = "OK";
                    dataItem.VerifyOK = true;
                }
                else if (rbOK.IsChecked == false && rbNG.IsChecked == true)
                {
                    dataItem.Verify = "NG";
                    dataItem.VerifyNG = true;
                }

                #endregion

                #region Itm_Yarn

                if (txtITM_YARN.Text != "")
                    dataItem.Itm_Yarn = txtITM_YARN.Text;

                #endregion

                #region WeightQty

                if (txtWeight.Text != "")
                {
                    try
                    {
                        dataItem.WeightQty = decimal.Parse(txtWeight.Text);
                    }
                    catch
                    {
                        dataItem.WeightQty = 0;
                    }
                }

                #endregion

                dataItem.Flag = "N";

                #region Packaging

                if (chkPackaging.IsChecked == true)
                {
                    dataItem.Packaging = "1";
                }
                else
                {
                    dataItem.Packaging = "0";
                }

                #endregion

                #region Clean

                if (chkClean.IsChecked == true)
                {
                    dataItem.Clean = "1";
                }
                else
                {
                    dataItem.Clean = "0";
                }

                #endregion

                #region Tearing

                if (chkTearing.IsChecked == true)
                {
                    dataItem.Tearing = "1";
                }
                else
                {
                    dataItem.Tearing = "0";
                }

                #endregion

                #region Falldown

                if (chkFalldown.IsChecked == true)
                {
                    dataItem.Falldown = "1";
                }
                else
                {
                    dataItem.Falldown = "0";
                }

                #endregion

                #region Certification

                if (chkCertification.IsChecked == true)
                {
                    dataItem.Certification = "1";
                }
                else
                {
                    dataItem.Certification = "0";
                }

                #endregion

                #region Invoice

                if (chkInvoice.IsChecked == true)
                {
                    dataItem.Invoice = "1";
                }
                else
                {
                    dataItem.Invoice = "0";
                }

                #endregion

                #region Identifyarea

                if (chkIdentifyarea.IsChecked == true)
                {
                    dataItem.Identifyarea = "1";
                }
                else
                {
                    dataItem.Identifyarea = "0";
                }

                #endregion

                #region Amountpallet

                if (chkAmountpallet.IsChecked == true)
                {
                    dataItem.Amountpallet = "1";
                }
                else
                {
                    dataItem.Amountpallet = "0";
                }

                #endregion

                #region Other

                if (txtOther.Text != "")
                    dataItem.Other = txtOther.Text;

                #endregion

                #region Action

                if (txtAction.Text != "")
                    dataItem.Action = txtAction.Text;

                #endregion

                #region TraceNo

                if (txtTraceNo.Text != "")
                    dataItem.TraceNo = txtTraceNo.Text;

                #endregion

                #region YarnType

                if (txtYarnType.Text != "")
                    dataItem.YarnType = txtYarnType.Text;

                #endregion

                listG3_Yarn.Add(dataItem);

                if (listG3_Yarn.Count > 0)
                {
                    this.gridG3.ItemsSource = listG3_Yarn;
                }

                chkAdd = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkAdd = false;
            }

            return chkAdd;
        }

        #endregion

        #region DeleteDataInGrid

        private bool DeleteDataInGrid()
        {
            bool chkDel = false;

            try
            {
                if (gridG3.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridG3);
                    int i = 0;
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            listG3_Yarn.RemoveAt(i);
                            break;
                        }
                        i++;
                    }
                }

                this.gridG3.ItemsSource = null;

                if (listG3_Yarn.Count > 0)
                {
                    this.gridG3.ItemsSource = listG3_Yarn;
                }

                chkDel = true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkDel = false;
            }

            return chkDel;
        }

        #endregion

        #region LoadDataEdit

        private void LoadDataEdit()
        {
            txtPalletNo.Text = _session.PalletNo;
            txtLot.Text = _session.LotorderNo;
            txtYarnType.Text = _session.YarnType;

            txtTraceNo.Text = _session.TraceNo;
            txtITM_YARN.Text = _session.Itm_Yarn;

            txtWeight.Text = _session.WeightQty.ToString();
            rbOK.IsChecked = _session.VerifyOK;
            rbNG.IsChecked = _session.VerifyNG;

            #region Packaging

            if (_session.Packaging == "1")
                chkPackaging.IsChecked = true;
            else
                chkPackaging.IsChecked = false;

            #endregion

            #region Clean

            if (_session.Clean == "1")
                chkClean.IsChecked = true;
            else
                chkClean.IsChecked = false;

            #endregion

            #region Tearing

            if (_session.Tearing == "1")
                chkTearing.IsChecked = true;
            else
                chkTearing.IsChecked = false;

            #endregion

            #region Falldown

            if (_session.Falldown == "1")
                chkFalldown.IsChecked = true;
            else
                chkFalldown.IsChecked = false;

            #endregion

            #region Certification

            if (_session.Certification == "1")
                chkCertification.IsChecked = true;
            else
                chkCertification.IsChecked = false;

            #endregion

            #region Invoice

            if (_session.Invoice == "1")
                chkInvoice.IsChecked = true;
            else
                chkInvoice.IsChecked = false;

            #endregion

            #region Identifyarea

            if (_session.Identifyarea == "1")
                chkIdentifyarea.IsChecked = true;
            else
                chkIdentifyarea.IsChecked = false;

            #endregion

            #region Amountpallet

            if (_session.Amountpallet == "1")
                chkAmountpallet.IsChecked = true;
            else
                chkAmountpallet.IsChecked = false;

            #endregion

            txtOther.Text = _session.Other;
            txtAction.Text = _session.Action;
            
            //cmdVerify.IsEnabled = true;

            //gridG3.IsEnabled = false;
            cmdReceive.IsEnabled = false;
            //cmdEdit.IsEnabled = false;
            //cmdDelete.IsEnabled = false;
        }

        #endregion

        #region ChkPalletNoInList

        private bool ChkPalletNoInList(string palletNo)
        {
            bool chkPalletNo = false;

            try
            {
                if (listG3_Yarn != null)
                {
                    for (int i = 0; i < listG3_Yarn.Count; i++)
                    {
                        if (listG3_Yarn[i].PalletNo == palletNo)
                        {
                            chkPalletNo = true;
                            break;
                        }
                    }    
                } 
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkPalletNo = false;
            }

            return chkPalletNo;
        }

        #endregion

        #region SaveG3_UPDATEYARN

        private bool SaveG3_UPDATEYARN()
        {
            bool chkSave = false;

            try
            {
                if (listG3_Yarn != null)
                {
                    string palletNo = string.Empty;
                    bool chkError = false;

                    IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
                    //DateTime dt = DateTime.UtcNow;
                    DateTime dt = DateTime.Now;
                
                    for (int i = 0; i < listG3_Yarn.Count; i++)
                    {
                        if (listG3_Yarn[i].PalletNo != null)
                        {
                            _sessionG3.PalletNo = listG3_Yarn[i].PalletNo;
                        }

                        if (listG3_Yarn[i].TraceNo != null)
                        {
                            _sessionG3.TraceNo = listG3_Yarn[i].TraceNo;
                        }

                        _sessionG3.LotorderNo = listG3_Yarn[i].LotorderNo;
                        _sessionG3.Verify = listG3_Yarn[i].Verify;
                        _sessionG3.Weight = listG3_Yarn[i].WeightQty;
                        _sessionG3.Flag = listG3_Yarn[i].Flag;
                        _sessionG3.OperatorID = txtOperator.Text;

                        _sessionG3.Packaging = listG3_Yarn[i].Packaging;
                        _sessionG3.Clean = listG3_Yarn[i].Clean;
                        _sessionG3.Tearing = listG3_Yarn[i].Tearing;
                        _sessionG3.Falldown = listG3_Yarn[i].Falldown;
                        _sessionG3.Certification = listG3_Yarn[i].Certification;
                        _sessionG3.Invoice = listG3_Yarn[i].Invoice;
                        _sessionG3.Identifyarea = listG3_Yarn[i].Identifyarea;
                        _sessionG3.Amountpallet = listG3_Yarn[i].Amountpallet;
                        _sessionG3.Other = listG3_Yarn[i].Other;
                        _sessionG3.Action = listG3_Yarn[i].Action;

                        _sessionG3.YarnType = listG3_Yarn[i].YarnType;

                        _sessionG3.ReceiveG3Date = dt;
                        _sessionG3.Type = "F";

                        if (!string.IsNullOrEmpty(_sessionG3.TraceNo))
                        {
                            if (_sessionG3.G3_RECEIVEYARN() == false)
                            {
                                palletNo += _sessionG3.PalletNo;
                                chkError = true;
                                break;
                            }
                        }
                    }

                    if (chkError == false)
                    {
                        "Save Data Complete".ShowMessageBox(false);

                        BindingOperations.ClearAllBindings(gridG3);
                        this.gridG3.ItemsSource = null;

                        _sessionG3.NewUpdateG3_yarn();
                        ClearControl();
                        EnabledControl(false);

                        chkSave = true;
                    }
                    else
                    {
                        string error = "Error on Save Data please try again: " + "\r\n Pallet No Error \r\n" + palletNo;
                        error.ShowMessageBox(true);
                        chkSave = false;
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
            where = "Where #FLAGS = 'S' and #RECTY = 'S' and #CDSTO = '3N' ";

            //string strConAS400 = "Provider = SQLOLEDB; Server=(local);Database=TESLUCDAT;User Id=sa;Password=kob;";
            //where = "Where FLAGS = 'S' and RECTY = 'S' and CDSTO = '3N' ";

            bool chkErr = true;
            string strErr = string.Empty;
            int? getAS400 = 0;
            try
            {
                List<BCSPRFTPResult> results = BCSPRFTPDataService.Instance.GetBCSPRFTP(strConAS400, where);

                List<LuckyTex.Models.AS400G3> dataList = new List<LuckyTex.Models.AS400G3>();
                int i = 0;

                if (results != null && results.Count > 0)
                {
                    #region GetBCSPRFTP

                    CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
                    ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";

                    string strMD = string.Empty;
                    string strED = string.Empty;

                    string mDate = string.Empty;
                    string eDate = string.Empty;

                    foreach (var row in results)
                    {
                        LuckyTex.Models.AS400G3 dataItemNew = new LuckyTex.Models.AS400G3();

                        strMD = string.Empty;
                        strED = string.Empty;

                        mDate = string.Empty;
                        eDate = string.Empty;

                        #region CDEL0

                        if (results[i].CDEL0 != null)
                        {
                            dataItemNew.CDEL0 = results[i].CDEL0;
                        }

                        #endregion

                        #region MOVEMENTDATE

                        if (results[i].DTTRA != null)
                        {
                            strMD = results[i].DTTRA.ToString();
                            mDate = string.Empty;

                            try
                            {
                                mDate = strMD.Substring(6, 2) + "/" + strMD.Substring(4, 2) + "/" + strMD.Substring(0, 4);

                                dataItemNew.MOVEMENTDATE = DateTime.Parse(mDate, ci);
                            }
                            catch
                            {
                                "Error MOVEMENTDATE".ShowMessageBox(true);
                                break;
                            }
                        }

                        #endregion

                        #region ITM400

                        if (results[i].CDKE1 != null)
                        {
                            dataItemNew.ITM400 = results[i].CDKE1;
                        }

                        #endregion

                        #region ENTRYDATE

                        if (results[i].DTINP != null)
                        {
                            strED = results[i].DTINP.ToString();
                            eDate = string.Empty;

                            try
                            {
                                eDate = strED.Substring(6, 2) + "/" + strED.Substring(4, 2) + "/" + strED.Substring(0, 4);

                                dataItemNew.ENTRYDATE = DateTime.Parse(eDate, ci);
                            }
                            catch
                            {
                                "Error ENTRYDATE".ShowMessageBox(true);
                                break;
                            }
                        }

                        #endregion

                        #region LOTNO

                        if (results[i].CDLOT != null)
                        {
                            dataItemNew.LOTNO = results[i].CDLOT;
                        }

                        #endregion

                        #region PALLETNO

                        if (results[i].CDCON != null)
                        {
                            dataItemNew.PALLETNO = results[i].CDCON;
                        }

                        #endregion

                        #region CONECH

                        if (results[i].TECU3 != null)
                        {
                            dataItemNew.CONECH = results[i].TECU3;
                        }

                        #endregion

                        #region WEIGHTQTY

                        if (results[i].BLELE != null)
                        {
                            dataItemNew.WEIGHTQTY = results[i].BLELE;
                        }

                        #endregion

                        #region TRACENO

                        if (results[i].TECU6 != null)
                        {
                            dataItemNew.TRACENO = results[i].TECU6;
                        }

                        #endregion

                        #region UM

                        if (results[i].CDUM0 != null)
                        {
                            dataItemNew.UM = results[i].CDUM0;
                        }

                        #endregion

                        dataList.Add(dataItemNew);

                        i++;
                    }

                    #endregion

                    if (dataList != null && dataList.Count > 0)
                    {
                        #region G3_GETDATAAS400

                        int a = 0;

                        string CDEL0 = string.Empty;
                        DateTime? DTTRA = null;
                        DateTime? DTINP = null;
                        string CDCON = string.Empty;
                        decimal? BLELE = null;
                        string CDUM0 = string.Empty;
                        string CDKE1 = string.Empty;
                        string CDLOT = string.Empty;
                        string CDQUA = string.Empty;
                        decimal? TECU1 = null;
                        decimal? TECU2 = null;
                        decimal? TECU3 = null;
                        decimal? TECU4 = null;
                        decimal? TECU5 = null;
                        string TECU6 = string.Empty;

                        string chkError = string.Empty;

                        foreach (var row in dataList)
                        {
                            // ตรวจสอบ Error
                            chkError = string.Empty;

                            CDEL0 = string.Empty;
                            DTTRA = null;
                            DTINP = null;
                            CDCON = string.Empty;
                            BLELE = null;
                            CDUM0 = string.Empty;
                            CDKE1 = string.Empty;
                            CDLOT = string.Empty;
                            CDQUA = string.Empty;
                            TECU1 = null;
                            TECU2 = null;
                            TECU3 = null;
                            TECU4 = null;
                            TECU5 = null;
                            TECU6 = null;

                            if (dataList[a].CDEL0 != null)
                                CDEL0 = dataList[a].CDEL0.TrimStart().TrimEnd();

                            if (dataList[a].MOVEMENTDATE != null)
                                DTTRA = dataList[a].MOVEMENTDATE;

                            if (dataList[a].ENTRYDATE != null)
                                DTINP = dataList[a].ENTRYDATE;

                            if (dataList[a].PALLETNO != null)
                                CDCON = dataList[a].PALLETNO.TrimStart().TrimEnd();

                            if (dataList[a].WEIGHTQTY != null)
                                BLELE = dataList[a].WEIGHTQTY;

                            if (dataList[a].UM != null)
                                CDUM0 = dataList[a].UM.TrimStart().TrimEnd();

                            if (dataList[a].ITM400 != null)
                                CDKE1 = dataList[a].ITM400.TrimStart().TrimEnd();

                            if (dataList[a].LOTNO != null)
                                CDLOT = dataList[a].LOTNO.TrimStart().TrimEnd();

                            if (dataList[a].CONECH != null)
                                TECU3 = dataList[a].CONECH;

                            if (dataList[a].TRACENO != null)
                                TECU6 = dataList[a].TRACENO.TrimStart().TrimEnd();

                            if (!string.IsNullOrEmpty(CDCON) && !string.IsNullOrEmpty(CDKE1))
                            {
                                chkError = G3DataService.Instance.G3_GETDATAAS400(DTTRA, DTINP, CDCON, BLELE, CDUM0, CDKE1, CDLOT, CDQUA, TECU1, TECU2, TECU3, TECU4, TECU5, TECU6);

                                if (!string.IsNullOrEmpty(chkError))
                                {
                                    //chkError.ShowMessageBox(true);

                                    strErr += chkError + "\r\n";
                                    chkErr = false;
                                    //break;
                                }
                                else
                                {
                                    getAS400++;

                                    if (BCSPRFTPDataService.Instance.DeleteBCSPRFTP(strConAS400, CDEL0, CDCON, CDKE1) == false)
                                    {
                                        string msg = string.Empty;
                                        msg = "Can't Delete BCSPRFTP please check CDEL0 = " + CDEL0;

                                        //msg.ShowMessageBox(true);
                                        strErr += msg + "\r\n";
                                        chkErr = false;
                                        //break;
                                    }
                                }
                            }

                            a++;
                        }

                        #endregion
                    }
                }
                else
                {
                    if (results == null)
                    {
                        chkErr = false;
                        strErr = "Data AS400 = null";
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Err();
                chkErr = false;
            }

            if (chkErr == true)
            {
                 string count = "Update Data from AS400 Complete " + "\r\n Total AS400 = " + getAS400;

                 count.ShowMessageBox();
            }
            else
            {
                if (!string.IsNullOrEmpty(strErr))
                {
                    strErr.Err();
                    strErr.ShowMessageBox();
                }
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

                if (gridG3.ItemsSource != null)
                {
                    if (gridG3.Items.Count > 0)
                    {
                        foreach (var row in gridG3.Items)
                        {
                            if (((LuckyTex.Models.ListG3_YarnData)((gridG3.Items)[o])).PalletNo != null)
                            {
                                pallet++;
                            }
                            o++;
                        }
                    }
                }

                if (pallet > 0)
                {
                    txtTotalPallet.Text = pallet.ToString("#,##0");
                }
                else
                {
                    txtTotalPallet.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region CheckPalletNo
        private bool CheckPalletNo(string PalletNo)
        {
            bool status = true;

            try
            {
                int o = 0;

                if (gridG3.ItemsSource != null)
                {
                    if (gridG3.Items.Count > 0)
                    {
                        foreach (var row in gridG3.Items)
                        {
                            if (((LuckyTex.Models.ListG3_YarnData)((gridG3.Items)[o])).PalletNo != null)
                            {
                                if (((LuckyTex.Models.ListG3_YarnData)((gridG3.Items)[o])).PalletNo == PalletNo)
                                {
                                    status = false;
                                    break;
                                }
                            }
                            o++;
                        }
                    }
                }
            }
            catch
            {
                status = false;
            }

            return status;
        }
        #endregion

        #region CutBarcode
        private string CutBarcode(string barcode)
        {
            string palletNo = string.Empty;
            string strBarcode = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(barcode))
                {
                    if (barcode.Length > 0)
                    {
                        if (barcode.Length != 10)
                        {
                            string first = string.Empty;

                            first = barcode.Substring(0, 1);

                            int n;
                            bool isNumeric = int.TryParse(first, out n);

                            if (isNumeric == true)
                            {
                                if (barcode.Length > 10)
                                {
                                    palletNo = barcode.Substring(barcode.Length - 10);
                                }
                                else
                                {
                                    palletNo = barcode;
                                }
                            }
                            else
                            {
                                strBarcode = barcode.Substring(0, barcode.Length - 1);

                                if (strBarcode.Length > 8)
                                {
                                    palletNo = strBarcode.Substring(strBarcode.Length - 8);
                                }
                                else
                                {
                                    palletNo = barcode;
                                }
                            }
                        }
                        else
                        {
                            palletNo = barcode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return palletNo;
        }
        #endregion

        //GetDataD365
        #region GetDataD365

        private void GetDataD365()
        {
           
            bool chkErr = true;
            string strErr = string.Empty;
            int? getAS400 = 0;
            try
            {
                string P_FLAGS = string.Empty;
                string P_RECTY = string.Empty;
                string P_CDSTO = string.Empty;

                P_FLAGS= "S";
                P_RECTY = "S";
                P_CDSTO = "3N";

                List<BCSPRFTP_D365Result> results = BCSPRFTPDataService.Instance.GetBCSPRFTP_D365(P_FLAGS, P_RECTY, P_CDSTO);

                List<LuckyTex.Models.AS400G3> dataList = new List<LuckyTex.Models.AS400G3>();
                int i = 0;

                if (results != null && results.Count > 0)
                {
                    #region GetBCSPRFTP

                    CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
                    ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";

                    string strMD = string.Empty;
                    string strED = string.Empty;

                    string mDate = string.Empty;
                    string eDate = string.Empty;

                    foreach (var row in results)
                    {
                        LuckyTex.Models.AS400G3 dataItemNew = new LuckyTex.Models.AS400G3();

                        strMD = string.Empty;
                        strED = string.Empty;

                        mDate = string.Empty;
                        eDate = string.Empty;

                        #region CDEL0

                        if (results[i].CDEL0 != null)
                        {
                            dataItemNew.CDEL0 = results[i].CDEL0;
                        }

                        #endregion

                        #region MOVEMENTDATE

                        if (results[i].DTTRA != null)
                        {
                            strMD = results[i].DTTRA.ToString();
                            mDate = string.Empty;

                            try
                            {
                                mDate = strMD.Substring(6, 2) + "/" + strMD.Substring(4, 2) + "/" + strMD.Substring(0, 4);

                                dataItemNew.MOVEMENTDATE = DateTime.Parse(mDate, ci);
                            }
                            catch
                            {
                                "Error MOVEMENTDATE".ShowMessageBox(true);
                                break;
                            }
                        }

                        #endregion

                        #region ITM400

                        if (results[i].CDKE1 != null)
                        {
                            dataItemNew.ITM400 = results[i].CDKE1;
                        }

                        #endregion

                        #region CDKE2

                        if (results[i].CDKE2 != null)
                        {
                            dataItemNew.CDKE2 = results[i].CDKE2;
                        }

                        #endregion

                        #region ENTRYDATE

                        if (results[i].DTINP != null)
                        {
                            strED = results[i].DTINP.ToString();
                            eDate = string.Empty;

                            try
                            {
                                eDate = strED.Substring(6, 2) + "/" + strED.Substring(4, 2) + "/" + strED.Substring(0, 4);

                                dataItemNew.ENTRYDATE = DateTime.Parse(eDate, ci);
                            }
                            catch
                            {
                                "Error ENTRYDATE".ShowMessageBox(true);
                                break;
                            }
                        }

                        #endregion

                        #region LOTNO

                        if (results[i].CDLOT != null)
                        {
                            dataItemNew.LOTNO = results[i].CDLOT;
                        }

                        #endregion

                        #region PALLETNO

                        if (results[i].CDCON != null)
                        {
                            dataItemNew.PALLETNO = results[i].CDCON;
                        }

                        #endregion

                        #region CONECH

                        if (results[i].TECU3 != null)
                        {
                            dataItemNew.CONECH = results[i].TECU3;
                        }

                        #endregion

                        #region WEIGHTQTY

                        if (results[i].BLELE != null)
                        {
                            dataItemNew.WEIGHTQTY = results[i].BLELE;
                        }

                        #endregion

                        #region TRACENO

                        if (results[i].TECU6 != null)
                        {
                            dataItemNew.TRACENO = results[i].TECU6;
                        }

                        #endregion

                        #region UM

                        if (results[i].CDUM0 != null)
                        {
                            dataItemNew.UM = results[i].CDUM0;
                        }

                        #endregion

                        dataList.Add(dataItemNew);

                        i++;
                    }

                    #endregion

                    if (dataList != null && dataList.Count > 0)
                    {
                        #region G3_GETDATAAS400

                        int a = 0;

                        string CDEL0 = string.Empty;
                        DateTime? DTTRA = null;
                        DateTime? DTINP = null;
                        string CDCON = string.Empty;
                        decimal? BLELE = null;
                        string CDUM0 = string.Empty;
                        string CDKE1 = string.Empty;
                        string CDKE2 = string.Empty;
                        string CDLOT = string.Empty;
                        string CDQUA = string.Empty;
                        decimal? TECU1 = null;
                        decimal? TECU2 = null;
                        decimal? TECU3 = null;
                        decimal? TECU4 = null;
                        decimal? TECU5 = null;
                        string TECU6 = string.Empty;

                        string chkError = string.Empty;

                        foreach (var row in dataList)
                        {
                            // ตรวจสอบ Error
                            chkError = string.Empty;

                            CDEL0 = string.Empty;
                            DTTRA = null;
                            DTINP = null;
                            CDCON = string.Empty;
                            BLELE = null;
                            CDUM0 = string.Empty;
                            CDKE1 = string.Empty;
                            CDKE2 = string.Empty;
                            CDLOT = string.Empty;
                            CDQUA = string.Empty;
                            TECU1 = null;
                            TECU2 = null;
                            TECU3 = null;
                            TECU4 = null;
                            TECU5 = null;
                            TECU6 = null;

                            if (dataList[a].CDEL0 != null)
                                CDEL0 = dataList[a].CDEL0.TrimStart().TrimEnd();

                            if (dataList[a].MOVEMENTDATE != null)
                                DTTRA = dataList[a].MOVEMENTDATE;

                            if (dataList[a].ENTRYDATE != null)
                                DTINP = dataList[a].ENTRYDATE;

                            if (dataList[a].PALLETNO != null)
                                CDCON = dataList[a].PALLETNO.TrimStart().TrimEnd();

                            if (dataList[a].WEIGHTQTY != null)
                                BLELE = dataList[a].WEIGHTQTY;

                            if (dataList[a].UM != null)
                                CDUM0 = dataList[a].UM.TrimStart().TrimEnd();

                            if (dataList[a].ITM400 != null)
                                CDKE1 = dataList[a].ITM400.TrimStart().TrimEnd();

                            if (dataList[a].CDKE2 != null)
                                CDKE2 = dataList[a].CDKE2.TrimStart().TrimEnd();

                            if (dataList[a].LOTNO != null)
                                CDLOT = dataList[a].LOTNO.TrimStart().TrimEnd();

                            if (dataList[a].CONECH != null)
                                TECU3 = dataList[a].CONECH;

                            if (dataList[a].TRACENO != null)
                                TECU6 = dataList[a].TRACENO.TrimStart().TrimEnd();

                            if (!string.IsNullOrEmpty(CDCON) && !string.IsNullOrEmpty(CDKE1))
                            {
                                chkError = G3DataService.Instance.G3_GETDATAD365(DTTRA, DTINP, CDCON, BLELE, CDUM0, CDKE1,CDKE2, CDLOT, CDQUA, TECU1, TECU2, TECU3, TECU4, TECU5, TECU6);

                                if (!string.IsNullOrEmpty(chkError))
                                {
                                    strErr += chkError + "\r\n";
                                    chkErr = false;
                                    //break;
                                }
                                else
                                {
                                    getAS400++;

                                    if (BCSPRFTPDataService.Instance.DeleteBCSPRFTP(CDEL0, CDCON, CDKE1) == false)
                                    {
                                        string msg = string.Empty;
                                        msg = "Can't Delete BCSPRFTP please check CDEL0 = " + CDEL0;

                                        strErr += msg + "\r\n";
                                        chkErr = false;
                                    }

                                }
                            }

                            a++;
                        }

                        #endregion
                    }
                }
                else
                {
                    if (results == null)
                    {
                        chkErr = false;
                        strErr = "Data D365 = null";
                    }
                }

            }
            catch (Exception ex)
            {
                ex.Err();
                chkErr = false;
            }

            if (chkErr == true)
            {
                string count = "Update Data from D365 Complete " + "\r\n Total D365 = " + getAS400;

                count.ShowMessageBox();
            }
            else
            {
                if (!string.IsNullOrEmpty(strErr))
                {
                    strErr.Err();
                    strErr.ShowMessageBox();
                }
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string logIn)
        {
            txtOperator.Text = logIn;
            listG3_Yarn = new List<ListG3_YarnData>();
        }

        #endregion

    }
}
