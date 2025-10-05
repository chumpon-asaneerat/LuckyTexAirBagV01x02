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
    /// Interaction logic for EditIssueRawMaterialPage.xaml
    /// </summary>
    public partial class EditIssueRawMaterialPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EditIssueRawMaterialPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadIssueTo();

            dteIssueDate.IsEnabled = false;
            cbIssueTo.IsEnabled = false;
        }

        #endregion

        #region Internal Variables

        string strConAS400 = string.Empty;
        bool chkStatusAS400 = true;

        string opera = string.Empty;

        string P_REQUESTNO = string.Empty;
        string P_PALLETNO = string.Empty;

        string P_TRACENO = string.Empty;
        decimal? P_CH = null;
        decimal? P_WEIGHT = null;
        DateTime? P_ISSUEDATE = null;

        string P_PALLETTYPE = string.Empty;
        string P_ISSUETO = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigManager.Instance.LoadAS400Configs();
            strConAS400 = ConfigManager.Instance.AS400Config;

            ClearControl();

            chkStatusAS400 = chkConAS400();

            if (opera != "")
                txtOperator.Text = opera;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region TextBox

        private void txtRequestNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                P_REQUESTNO = string.Empty;
                P_PALLETNO = string.Empty;
                P_TRACENO = string.Empty;
                P_CH = null;
                P_WEIGHT = null;
                P_ISSUEDATE = null;
                P_PALLETTYPE = string.Empty;
                P_ISSUETO = string.Empty;

                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    if (G3_GETREQUESTNODETAIL(txtRequestNo.Text) == true)
                    {
                        txtPalletNo.Focus();
                        txtPalletNo.SelectAll();
                        e.Handled = true;
                    }
                    else
                    {
                        "No data found for this Request No".ShowMessageBox();

                        txtRequestNo.Text = string.Empty;
                        txtRequestNo.Focus();
                        txtRequestNo.SelectAll();
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPalletNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtPalletNo.Text))
                    {
                        CheckPalletNo(txtPalletNo.Text);
                    }
                }
                else
                {
                    "Request No is not null".ShowMessageBox();

                    txtRequestNo.Focus();
                    txtRequestNo.SelectAll();
                }
            }
        }

        #endregion

        #region Button Handlers

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_REQUESTNO))
            {
                Preview(P_REQUESTNO);
            }
            else
            {
                "Please select Request No".ShowMessageBox();
            }
        }
        #endregion

        #region cmdCancelRequest_Click
        private void cmdCancelRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_REQUESTNO))
            {
                if (MessageBox.Show("Do you want to Cancel this Request No?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    G3_CANCELREQUESTNO(P_REQUESTNO);
                }
            }
            else
            {
                "Please select Request No".ShowMessageBox();
            }
        }
        #endregion

        #region cmdDeletePallet_Click
        private void cmdDeletePallet_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_PALLETNO))
            {
                if (MessageBox.Show("Do you want to Cancel this Pallet from Request No? ", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    G3_INSERTUPDATEISSUEYARN(P_REQUESTNO, P_PALLETNO, P_TRACENO, P_CH, P_WEIGHT, P_ISSUEDATE, opera, P_PALLETTYPE, P_ISSUETO);
                }
            }
            else
            {
                "Please select Pallet No".ShowMessageBox();
            }
        }
        #endregion

        #region cmdAdd_Click
        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_REQUESTNO))
            {
                if (CheckEdit() == true)
                    Edit();
            }
        }
        #endregion

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #endregion

        #region gridPalletDetail_SelectedCellsChanged

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

        private void gridPalletDetail_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridPalletDetail.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridPalletDetail);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).REQUESTNO != null)
                            {
                                P_REQUESTNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).REQUESTNO;
                            }
                            else
                            {
                                P_REQUESTNO = string.Empty;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).PALLETNO != null)
                            {
                                P_PALLETNO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).PALLETNO;
                            }
                            else
                            {
                                P_PALLETNO = string.Empty;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).TRACENO != null)
                            {
                                P_TRACENO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).TRACENO;
                            }
                            else
                            {
                                P_TRACENO = string.Empty;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).CH != null)
                            {
                                P_CH = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).CH;
                            }
                            else
                            {
                                P_CH = null;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).WEIGHT != null)
                            {
                                P_WEIGHT = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).WEIGHT;
                            }
                            else
                            {
                                P_WEIGHT = null;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).ISSUEDATE != null)
                            {
                                P_ISSUEDATE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).ISSUEDATE;
                            }
                            else
                            {
                                P_ISSUEDATE = null;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).PALLETTYPE != null)
                            {
                                P_PALLETTYPE = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).PALLETTYPE;
                            }
                            else
                            {
                                P_PALLETTYPE = string.Empty;
                            }

                            if (((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).ISSUETO != null)
                            {
                                P_ISSUETO = ((LuckyTex.Models.G3_GETREQUESTNODETAIL)(gridPalletDetail.CurrentCell.Item)).ISSUETO;
                            }
                            else
                            {
                                P_ISSUETO = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    P_REQUESTNO = string.Empty;
                    P_PALLETNO = string.Empty;
                    P_TRACENO = string.Empty;
                    P_CH = null;
                    P_WEIGHT = null;
                    P_ISSUEDATE = null;
                    P_PALLETTYPE = string.Empty;
                    P_ISSUETO = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region gridEditPalletDetail_LoadingRow
        private void gridEditPalletDetail_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (gridEditPalletDetail.ItemsSource != null)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)(e.Row.Item)).SelectData == true)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region private Methods

        #region LoadIssueTo

        private void LoadIssueTo()
        {
            if (cbIssueTo.ItemsSource == null)
            {
                string[] str = new string[] { "Warp AB", "Weft AB", "Warp AD", "Weft AD" };

                cbIssueTo.ItemsSource = str;
                cbIssueTo.SelectedIndex = 0;
            }
        }

        #endregion

        #region ClearControl
        /// <summary>
        /// 
        /// </summary>
        private void ClearControl()
        {
            txtRequestNo.Text = string.Empty;
            txtItemYarn.Text = string.Empty;
            txtPalletNo.Text = string.Empty;

            cbIssueTo.SelectedIndex = 0;

            dteIssueDate.SelectedDate = DateTime.Now;
            dteIssueDate.Text = DateTime.Now.ToString("dd/MM/yy");

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPalletDetail.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPalletDetail.SelectedItems.Clear();
            else
                this.gridPalletDetail.SelectedItem = null;

            gridPalletDetail.ItemsSource = null;

            if (this.gridEditPalletDetail.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridEditPalletDetail.SelectedItems.Clear();
            else
                this.gridEditPalletDetail.SelectedItem = null;

            gridEditPalletDetail.ItemsSource = null;

            P_REQUESTNO = string.Empty;
            P_PALLETNO = string.Empty;
            P_TRACENO = string.Empty;
            P_CH = null;
            P_WEIGHT = null;
            P_ISSUEDATE = null;
            P_PALLETTYPE = string.Empty;
            P_ISSUETO = string.Empty;

            txtRequestNo.Focus();
            txtRequestNo.SelectAll();
        }

        #endregion

        #region G3_GETREQUESTNODETAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="REQUESTNO"></param>
        /// <returns></returns>
        private bool G3_GETREQUESTNODETAIL(string REQUESTNO)
        {
            try
            {
                bool chkStatus = true;

                List<G3_GETREQUESTNODETAIL> lots = new List<G3_GETREQUESTNODETAIL>();

                lots = G3DataService.Instance.G3_GETREQUESTNODETAIL(REQUESTNO);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridPalletDetail.ItemsSource = lots;

                    if (lots[0].ITM_YARN != null && lots[0].YARNTYPE != null)
                    {
                        P_REQUESTNO = lots[0].REQUESTNO;
                        txtItemYarn.Text = lots[0].ITM_YARN;
                        string yarnType = lots[0].YARNTYPE;

                        G3_SEARCHYARNSTOCKData(string.Empty, txtItemYarn.Text, yarnType);
                    }

                    if (lots[0].ISSUEDATE != null)
                    {
                        dteIssueDate.SelectedDate = lots[0].ISSUEDATE;
                    }

                    if (lots[0].ISSUETO != null)
                    {
                        #region Old
                        //if (lots[0].ISSUETO == "SA")
                        //    cbIssueTo.SelectedIndex = 0;
                        //else if (lots[0].ISSUETO == "SB")
                        //    cbIssueTo.SelectedIndex = 1;
                        //else if (lots[0].ISSUETO == "SC")
                        //    cbIssueTo.SelectedIndex = 2;
                        //else if (lots[0].ISSUETO == "SD")
                        //    cbIssueTo.SelectedIndex = 3;
                        #endregion

                        if (lots[0].ISSUETO == "SB")
                            cbIssueTo.SelectedIndex = 0;
                        else if (lots[0].ISSUETO == "SA")
                            cbIssueTo.SelectedIndex = 1;
                        else if (lots[0].ISSUETO == "SD")
                            cbIssueTo.SelectedIndex = 2;
                        else if (lots[0].ISSUETO == "SC")
                            cbIssueTo.SelectedIndex = 3;
                    }

                    chkStatus = true;
                }
                else
                {
                    gridPalletDetail.ItemsSource = null;

                    txtItemYarn.Text = string.Empty;

                    if (this.gridEditPalletDetail.SelectionMode != DataGridSelectionMode.Single)
                        this.gridEditPalletDetail.SelectedItems.Clear();
                    else
                        this.gridEditPalletDetail.SelectedItem = null;

                    gridEditPalletDetail.ItemsSource = null;

                    chkStatus = false;
                }

                return chkStatus;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region G3_SEARCHYARNSTOCKData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_RECDATE"></param>
        /// <param name="P_ITMYARN"></param>
        private void G3_SEARCHYARNSTOCKData(string P_RECDATE, string P_ITMYARN, string P_YARNTYPE)
        {
            try
            {
                List<G3_SEARCHYARNSTOCKData> lots = new List<G3_SEARCHYARNSTOCKData>();

                lots = G3DataService.Instance.GetG3_SEARCHYARNSTOCKData(P_RECDATE, P_ITMYARN, P_YARNTYPE);
                //rowRequestNo = lots.Count;

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridEditPalletDetail.ItemsSource = lots;

                }
                else
                {

                    if (this.gridEditPalletDetail.SelectionMode != DataGridSelectionMode.Single)
                        this.gridEditPalletDetail.SelectedItems.Clear();
                    else
                        this.gridEditPalletDetail.SelectedItem = null;

                    gridEditPalletDetail.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region G3_CANCELREQUESTNO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="REQUESTNO"></param>
        /// <returns></returns>
        private bool G3_CANCELREQUESTNO(string REQUESTNO)
        {
            try
            {
                if (G3DataService.Instance.G3_CANCELREQUESTNO(REQUESTNO, opera) == true)
                {
                    "This Request No have been cancel".ShowMessageBox();
                    ClearControl();

                    return true;
                }
                else
                {
                    "Can't Cancel Request Please check data".ShowMessageBox();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region G3_INSERTUPDATEISSUEYARN
        /// <summary>
        /// 
        /// </summary>
        /// <param name="REQUESTNO"></param>
        /// <param name="PALLETNO"></param>
        /// <param name="TRACENO"></param>
        /// <param name="CH"></param>
        /// <param name="WEIGHT"></param>
        /// <param name="ISSUEDATE"></param>
        /// <param name="OPERATOR"></param>
        /// <param name="PALLETTYPE"></param>
        /// <param name="ISSUETO"></param>
        /// <returns></returns>
        private bool G3_INSERTUPDATEISSUEYARN(string REQUESTNO, string PALLETNO, string TRACENO
            , decimal? CH, decimal? WEIGHT, DateTime? ISSUEDATE, string OPERATOR, string PALLETTYPE, string ISSUETO)
        {
            try
            {
                if (G3DataService.Instance.G3_INSERTUPDATEISSUEYARN(REQUESTNO, PALLETNO, TRACENO
            ,  CH,  WEIGHT,  ISSUEDATE,  OPERATOR,  PALLETTYPE,  ISSUETO) == true)
                {
                    "Pallet have been delete".ShowMessageBox();

                    if (G3_GETREQUESTNODETAIL(P_REQUESTNO) == true)
                    {
                        txtPalletNo.Focus();
                        txtPalletNo.SelectAll();
                    }
                    else
                    {
                        "No data found for this Request No".ShowMessageBox();

                        txtRequestNo.Text = string.Empty;
                        txtRequestNo.Focus();
                        txtRequestNo.SelectAll();
                    }

                    return true;
                }
                else
                {
                    "Can't Cancel Request Please check data".ShowMessageBox();
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region CheckPalletNo
        private void CheckPalletNo(string PalletNo)
        {
            try
            {
                List<LuckyTex.Models.G3_SEARCHYARNSTOCKData> dataList = new List<LuckyTex.Models.G3_SEARCHYARNSTOCKData>();
                bool chkData = false;

                int o = 0;
                foreach (var row in gridEditPalletDetail.Items)
                {
                    LuckyTex.Models.G3_SEARCHYARNSTOCKData dataItem = new LuckyTex.Models.G3_SEARCHYARNSTOCKData();

                    dataItem.RowNo = o + 1;

                    dataItem.SelectData = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).SelectData;

                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETNO != null)
                    {
                        if (PalletNo == ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETNO)
                        {
                            dataItem.SelectData = true;
                            chkData = true;
                        }
                    }

                    dataItem.ENTRYDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).ENTRYDATE;
                    dataItem.ITM_YARN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).ITM_YARN;
                    dataItem.PALLETNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETNO;
                    dataItem.YARNTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).YARNTYPE;
                    dataItem.WEIGHTQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).WEIGHTQTY;
                    dataItem.CONECH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).CONECH;
                    dataItem.VERIFY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).VERIFY;
                    dataItem.REMAINQTY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).REMAINQTY;
                    dataItem.RECEIVEBY = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).RECEIVEBY;
                    dataItem.RECEIVEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).RECEIVEDATE;
                    dataItem.FINISHFLAG = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).FINISHFLAG;
                    dataItem.UPDATEDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).UPDATEDATE;
                    dataItem.PALLETTYPE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETTYPE;
                    dataItem.ITM400 = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).ITM400;
                    dataItem.UM = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).UM;
                    dataItem.PACKAING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PACKAING;
                    dataItem.CLEAN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).CLEAN;
                    dataItem.TEARING = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).TEARING;
                    dataItem.FALLDOWN = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).FALLDOWN;
                    dataItem.CERTIFICATION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).CERTIFICATION;
                    dataItem.INVOICE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).INVOICE;
                    dataItem.IDENTIFYAREA = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).IDENTIFYAREA;
                    dataItem.AMOUNTPALLET = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).AMOUNTPALLET;
                    dataItem.OTHER = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).OTHER;
                    dataItem.ACTION = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).ACTION;
                    dataItem.MOVEMENTDATE = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).MOVEMENTDATE;
                    dataItem.LOTNO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).LOTNO;
                    dataItem.TRACENO = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).TRACENO;
                    dataItem.KGPERCH = ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).KGPERCH;

                    o++;

                    dataList.Add(dataItem);
                }

                this.gridEditPalletDetail.ItemsSource = dataList;

                if (chkData == false)
                {
                    "No Data Found".ShowMessageBox();
                }

                txtPalletNo.Text = string.Empty;
                txtPalletNo.Focus();
                txtPalletNo.SelectAll();
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region CheckEdit
        private bool CheckEdit()
        {
            try
            {
                bool chkEdit = false;

                int o = 0;

                foreach (var row in gridEditPalletDetail.Items)
                {
                    if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).SelectData == true)
                    {
                        chkEdit = true;
                        break;
                    }

                    o++;
                }

                return chkEdit;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }
        #endregion

        #region Edit
        private void Edit()
        {
            try
            {
                int o = 0;
                bool chkSave = true;

                string ISSUETO = string.Empty;
                DateTime? ISSUEDATE = null;
                string Operator = string.Empty;

                if (!string.IsNullOrEmpty(P_REQUESTNO) && dteIssueDate.SelectedDate != null)
                {
                   
                    if (cbIssueTo.SelectedValue != null)
                    {
                        #region Old
                        //if (cbIssueTo.SelectedValue.ToString() == "Warp AB")
                        //    ISSUETO = "SA";
                        //else if (cbIssueTo.SelectedValue.ToString() == "Weft AB")
                        //    ISSUETO = "SB";
                        //else if (cbIssueTo.SelectedValue.ToString() == "Warp AD")
                        //    ISSUETO = "SC";
                        //else if (cbIssueTo.SelectedValue.ToString() == "Weft AD")
                        //    ISSUETO = "SD";
                        #endregion

                        if (cbIssueTo.SelectedValue.ToString() == "Warp AB")
                            ISSUETO = "SB";
                        else if (cbIssueTo.SelectedValue.ToString() == "Weft AB")
                            ISSUETO = "SA";
                        else if (cbIssueTo.SelectedValue.ToString() == "Warp AD")
                            ISSUETO = "SD";
                        else if (cbIssueTo.SelectedValue.ToString() == "Weft AD")
                            ISSUETO = "SC";
                    }

                    ISSUEDATE = dteIssueDate.SelectedDate;
                    Operator = txtOperator.Text;

                    foreach (var row in gridEditPalletDetail.Items)
                    {
                        if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).SelectData == true)
                        {
                            if (((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).RowNo != 0)
                            {
                                if (G3DataService.Instance.G3_INSERTUPDATEISSUEYARN(P_REQUESTNO
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETNO
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).TRACENO
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).CONECH
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).WEIGHTQTY
                                    , ISSUEDATE
                                    , Operator
                                    , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETTYPE
                                    , ISSUETO) == true)
                                {
                                    if (chkStatusAS400 == true)
                                    {
                                        SendAS400(ISSUEDATE
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).PALLETNO
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).ITM400
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).LOTNO
                                            , ISSUETO
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).TRACENO
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).WEIGHTQTY
                                            , ((LuckyTex.Models.G3_SEARCHYARNSTOCKData)((gridEditPalletDetail.Items)[o])).CONECH
                                            , P_REQUESTNO);
                                    }
                                }
                                else
                                {
                                    chkSave = false;
                                    break;
                                }
                            }
                        }

                        o++;
                    }

                    if (chkSave == true)
                    {
                        if (!string.IsNullOrEmpty(P_REQUESTNO))
                        {
                            "Pallet have been Add".ShowMessageBox();

                            txtRequestNo.Text = P_REQUESTNO;

                            P_PALLETNO = string.Empty;
                            P_TRACENO = string.Empty;
                            P_CH = null;
                            P_WEIGHT = null;
                            P_ISSUEDATE = null;
                            P_PALLETTYPE = string.Empty;
                            P_ISSUETO = string.Empty;

                            if (G3_GETREQUESTNODETAIL(P_REQUESTNO) == true)
                            {
                                cbIssueTo.SelectedIndex = 0;

                                dteIssueDate.SelectedDate = DateTime.Now;
                                dteIssueDate.Text = DateTime.Now.ToString("dd/MM/yy");

                                txtPalletNo.Text = string.Empty;
                                txtPalletNo.Focus();
                                txtPalletNo.SelectAll();
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtRequestNo.Text))
                    {
                        "Request No isn't Null".ShowMessageBox();
                        txtRequestNo.Focus();
                        txtRequestNo.SelectAll();
                    }
                    else if (dteIssueDate.SelectedDate != null)
                    {
                        "Issue Date isn't Null".ShowMessageBox();
                        dteIssueDate.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
            }
        }
        #endregion

        #region SendAS400

        private bool SendAS400(DateTime? ISSUEDATE, string PALLETNO, string ITM400, string LOTNO, string ISSUETO, string TRACENO, decimal? WEIGHT, decimal? CH, string REQUESTNO)
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

                decimal? pound = null;

                double? defPound = 2.2046;

                string dSend = ISSUEDATE.Value.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + ISSUEDATE.Value.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + ISSUEDATE.Value.ToString("dd", CultureInfo.CreateSpecificCulture("en-US"));

                DateTime? sendDate = null;
                string sDate = string.Empty;

                sendDate = DateTime.Now;

                if (sendDate != null)
                {
                    sDate = sendDate.Value.ToString("yyyy", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("MM", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("dd", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("HH", CultureInfo.CreateSpecificCulture("en-US")) + sendDate.Value.ToString("mm", CultureInfo.CreateSpecificCulture("en-US"));
                }

                #endregion

                ConStr = strConAS400;

                FLAGS = "R";
                RECTY = "S";
                CDSTO = "3N";
                USRNM = "PGMR";

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

                //  เพิ่มใหม่ 02/12/15
                #region DTINP

                try
                {
                    DTINP = Convert.ToInt32(dSend);
                }
                catch
                {
                    DTINP = null;
                }

                #endregion

                CDEL0 = PALLETNO;
                CDCON = PALLETNO;
                BLELE = WEIGHT;
                CDUM0 = "KG";
                CDKE1 = ITM400;
                CDLOT = LOTNO;
                CDTRA = ISSUETO;

                //  เพิ่มใหม่ 02/12/15
                REFER = REQUESTNO;
                LOCAT = "GD";
                CDQUA = "1";
                //----------------//

                // ปรับเพิ่มใหม่ 19/12/15
                #region pound
                if (WEIGHT != null)
                {
                    try
                    {
                        pound = MathEx.Round(System.Convert.ToDecimal((System.Convert.ToDouble(WEIGHT.Value) * defPound)), 2);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.Err();
                    }
                }
                #endregion

                TECU1 = pound;
                TECU2 = pound;

                TECU3 = CH;
                TECU4 = WEIGHT;
                TECU6 = TRACENO;

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

        #region chkConAS400

        private bool chkConAS400()
        {
            try
            {
                return BCSPRFTPDataService.Instance.chkConAS400(strConAS400);
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                return false;
            }
        }

        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string REQUESTNO)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "IssueRawMaterial";

                ConmonReportService.Instance.REQUESTNO = REQUESTNO;

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

        private void Preview(string REQUESTNO)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "IssueRawMaterial";

                ConmonReportService.Instance.REQUESTNO = REQUESTNO;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string logIn)
        {
            opera = logIn;
        }

        #endregion

    }
}
