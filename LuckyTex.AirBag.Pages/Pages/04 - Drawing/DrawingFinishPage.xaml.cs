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
    /// Interaction logic for DrawingFinishPage.xaml
    /// </summary>
    public partial class DrawingFinishPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingFinishPage()
        {
            InitializeComponent();

            LoadGroup();
        }

        #endregion

        #region Internal Variables

        private DrawingSession _session = new DrawingSession();

        string opera = string.Empty;
        string mcNo = string.Empty;
        string mcName = string.Empty;

        string BEAMERROLL = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItemGood();

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
            if (chkAll.IsChecked == false)
            {
                if (cbItemCodeSearch.SelectedItem != null)
                    DRAW_GETDRAWINGLISTBYITEM(cbItemCodeSearch.Text);
            }
            else
            {
                DRAW_GETDRAWINGLISTBYITEM("");
            }
        }

        #endregion

        #region cmdEnd_Click

        private void cmdEnd_Click(object sender, RoutedEventArgs e)
        {
            End();
        }

        #endregion

        #endregion

        #region cmbColors_LostFocus
        private void cmbColors_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbColors.Text == "Orange")
            {
                cmbColors.Background = Brushes.Orange;
                cmbColors.Foreground = Brushes.Orange;
            }
            else if (cmbColors.Text == "Green")
            {
                cmbColors.Background = Brushes.Green;
                cmbColors.Foreground = Brushes.Green;
            }
            else if (cmbColors.Text == "Blue")
            {
                cmbColors.Background = Brushes.Blue;
                cmbColors.Foreground = Brushes.Blue;
            }
            else if (cmbColors.Text == "Gray")
            {
                cmbColors.Background = Brushes.Gray;
                cmbColors.Foreground = Brushes.Gray;
            }
        }
        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtReedNo_KeyDown

        private void txtReedNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHealdNo.Focus();
                txtHealdNo.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtHealdNo_KeyDown

        private void txtHealdNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBeamLot.Focus();
                txtBeamLot.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region gridDrawing

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

        private void gridDrawing_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridDrawing.ItemsSource != null)
                {
                    string StrColors = string.Empty;
                    string StrDrawingType = string.Empty;

                    var row_list = GetDataGridRows(gridDrawing);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            StrColors = string.Empty;
                            StrDrawingType = string.Empty;

                            #region BEAMNO
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).BEAMNO != null)
                            {
                                txtBEAMNO.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).BEAMNO;
                            }
                            else
                            {
                                txtBEAMNO.Text = "";
                            }
                            #endregion

                            #region BEAMLOT
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).BEAMLOT != null)
                            {
                                txtBeamLot.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).BEAMLOT;
                            }
                            else
                            {
                                txtBeamLot.Text = "";
                            }
                            #endregion

                            #region REEDNO
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).REEDNO != null)
                            {
                                txtReedNo.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).REEDNO;
                            }
                            else
                            {
                                txtReedNo.Text = "";
                            }
                            #endregion

                            #region ITM_PREPARE

                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).ITM_PREPARE != null)
                            {
                                txtItemCode.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).ITM_PREPARE;

                                List<DRAW_GETSPECBYCHOPNO> results = DrawingDataService.Instance.ITM_GETITEMPREPARELIST(((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).ITM_PREPARE);

                                if (results.Count > 0)
                                {
                                    txtNOYARN.Text = results[0].NOYARN.Value.ToString("#,##0.##");
                                }
                                else
                                {
                                    txtNOYARN.Text = "0";
                                }
                            }
                            else
                            {
                                txtItemCode.Text = "";
                                txtNOYARN.Text = "0";
                            }

                            #endregion

                            #region TotalBeam

                            if (!string.IsNullOrEmpty(txtBeamLot.Text) && !string.IsNullOrEmpty(txtItemCode.Text))
                            {
                                DRAW_GETBEAMLOTDETAIL results = new DRAW_GETBEAMLOTDETAIL();

                                results = DrawingDataService.Instance.CheckBeamLot_ITM_Prepare(txtBeamLot.Text, txtItemCode.Text);

                                if (results != null)
                                {
                                    if (results.TOTALYARN != null)
                                    {
                                        txtTotalBeam.Text = results.TOTALYARN.Value.ToString("#,##0.##");
                                    }
                                    else
                                    {
                                        txtTotalBeam.Text = "0";
                                    }
                                }
                            }
                            else
                            {
                                txtTotalBeam.Text = "0";
                            }

                            #endregion

                            #region REEDTYPE
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).REEDTYPE != null)
                            {
                                txtREEDTYPE.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).REEDTYPE.Value.ToString("#,##0.##");
                            }
                            else
                            {
                                txtREEDTYPE.Text = "";
                            }
                            #endregion

                            #region HEALDNO
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).HEALDNO != null)
                            {
                                //txtHealdNo.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).HEALDNO.Value.ToString("#,##0.##");
                                txtHealdNo.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).HEALDNO.Value.ToString();
                            }
                            else
                            {
                                txtHealdNo.Text = "";
                            }
                            #endregion

                            #region HEALDCOLOR

                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).HEALDCOLOR != null)
                            {
                                StrColors = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).HEALDCOLOR;

                                if (StrColors == "Orange")
                                {
                                    cmbColors.Text = "Orange";
                                    cmbColors.Background = Brushes.Orange;
                                    cmbColors.Foreground = Brushes.Orange;
                                }
                                else if (StrColors == "Green")
                                {
                                    cmbColors.Text = "Green";
                                    cmbColors.Background = Brushes.Green;
                                    cmbColors.Foreground = Brushes.Green;
                                }
                                else if (StrColors == "Blue")
                                {
                                    cmbColors.Text = "Blue";
                                    cmbColors.Background = Brushes.Blue;
                                    cmbColors.Foreground = Brushes.Blue;
                                }
                                else if (StrColors == "Gray")
                                {
                                    cmbColors.Text = "Gray";
                                    cmbColors.Background = Brushes.Gray;
                                    cmbColors.Foreground = Brushes.Gray;
                                }
                            }
                            else
                            {
                                cmbColors.Text = "";
                                cmbColors.Background = null;
                                cmbColors.Foreground = null;
                            }

                            #endregion

                            #region STARTBY
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).STARTBY != null)
                            {
                                txtStartBy.Text = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).STARTBY;
                            }
                            else
                            {
                                txtStartBy.Text = "";
                            }
                            #endregion

                            #region DRAWINGTYPE
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).DRAWINGTYPE != null)
                            {
                                StrDrawingType = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).DRAWINGTYPE;

                                if (StrDrawingType == "Tying")
                                {
                                    rbTying.IsChecked = true;
                                    rbDrawing.IsChecked = false;
                                }
                                else if (StrDrawingType == "Drawing")
                                {
                                    rbTying.IsChecked = false;
                                    rbDrawing.IsChecked = true;
                                }
                            }
                            else
                            {
                                rbTying.IsChecked = true;
                                rbDrawing.IsChecked = false;
                            }
                            #endregion

                            #region OPERATOR_GROUP
                            if (((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).OPERATOR_GROUP != null)
                            {
                                cbGroup.SelectedValue = ((LuckyTex.Models.DRAW_GETDRAWINGLISTBYITEM)(gridDrawing.CurrentCell.Item)).OPERATOR_GROUP;
                            }
                            else
                            {
                                cbGroup.SelectedValue = "A";
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    txtBeamLot.Text = "";
                    txtReedNo.Text = "";
                    txtItemCode.Text = null;

                    txtREEDTYPE.Text = "";
                    txtHealdNo.Text = "";
                    cmbColors.Text = "";
                    cmbColors.Background = null;
                    cmbColors.Foreground = null;
                    txtStartBy.Text = "";
                    rbTying.IsChecked = true;
                    rbDrawing.IsChecked = false;

                    txtNOYARN.Text = "";
                    txtTotalBeam.Text = "";

                    cbGroup.SelectedValue = "A";
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

        private void Print(string P_BEAMERROLL)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "DRAW_TRANSFERSLIP";
                ConmonReportService.Instance.P_BEAMERROLL = P_BEAMERROLL;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string P_BEAMERROLL)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "DRAW_TRANSFERSLIP";
                ConmonReportService.Instance.P_BEAMERROLL = P_BEAMERROLL;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMPREPARELIST> items = _session.GetItemCodeData();

                this.cbItemCodeSearch.ItemsSource = items;
                this.cbItemCodeSearch.DisplayMemberPath = "ITM_PREPARE";
                this.cbItemCodeSearch.SelectedValuePath = "ITM_PREPARE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadGroup

        private void LoadGroup()
        {
            if (cbGroup.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C","D" };

                cbGroup.ItemsSource = str;
                cbGroup.SelectedIndex = 0;
            }
        }

        #endregion

        #region DRAW_GETDRAWINGLISTBYITEM

        private void DRAW_GETDRAWINGLISTBYITEM(string P_ITMPREPARE)
        {
            List<DRAW_GETDRAWINGLISTBYITEM> lots = new List<DRAW_GETDRAWINGLISTBYITEM>();

            lots = DrawingDataService.Instance.DRAW_GETDRAWINGLISTBYITEM(P_ITMPREPARE);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridDrawing.ItemsSource = lots;
            }
            else
            {
                gridDrawing.ItemsSource = null;

                txtBeamLot.Text = "";
                txtReedNo.Text = "";
                txtItemCode.Text = "";

                txtREEDTYPE.Text = "";
                txtHealdNo.Text = "";
                cmbColors.Text = "";
                cmbColors.Background = null;
                cmbColors.Foreground = null;
                txtStartBy.Text = "";
                rbTying.IsChecked = true;
                rbDrawing.IsChecked = false;

                txtNOYARN.Text = "";
                txtTotalBeam.Text = "";

                cbGroup.SelectedValue = "A";
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            txtItemCode.Text = "";
            cbItemCodeSearch.SelectedValue = null;

            cmbColors.SelectedValue = null;
            cmbColors.Text = "";
            cmbColors.Background = null;
            cmbColors.Foreground = null;

            txtReedNo.Text = "";
            txtHealdNo.Text = "";
            txtREEDTYPE.Text = "";
            txtBeamLot.Text = "";

            rbTying.IsChecked = true;
            rbDrawing.IsChecked = false;

            chkAll.IsChecked = false;

            txtNOYARN.Text = "";
            txtTotalBeam.Text = "";

            cbGroup.SelectedIndex = 0;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridDrawing.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridDrawing.SelectedItems.Clear();
            else
                this.gridDrawing.SelectedItem = null;

            gridDrawing.ItemsSource = null;

            BEAMERROLL = string.Empty;
            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;

            cbItemCodeSearch.Focus();
        }

        #endregion

        #region End

        private void End()
        {
            string result = string.Empty;

            string P_BEAMLOT = string.Empty;
            string P_DRAWINGTYPE = string.Empty;
            string P_REEDNO = string.Empty;
            string P_HEALDCOLOR = string.Empty;
            decimal? P_HEALDNO = null;
            string P_OPERATOR = string.Empty;
            string P_FLAG = string.Empty;
            string P_GROUP = string.Empty;

            if (!string.IsNullOrEmpty(txtBeamLot.Text))
                P_BEAMLOT = txtBeamLot.Text;

            #region P_DRAWINGTYPE

            if (rbTying.IsChecked == true && rbDrawing.IsChecked == false)
            {
                P_DRAWINGTYPE = "Tying";
            }
            else if (rbTying.IsChecked == false && rbDrawing.IsChecked == true)
            {
                P_DRAWINGTYPE = "Drawing";
            }

            #endregion

            if (!string.IsNullOrEmpty(txtReedNo.Text))
                P_REEDNO = txtReedNo.Text;

            if (!string.IsNullOrEmpty(cmbColors.Text))
                P_HEALDCOLOR = cmbColors.Text;

            if (!string.IsNullOrEmpty(txtHealdNo.Text))
            {
                try
                {
                    P_HEALDNO = decimal.Parse(txtHealdNo.Text);
                }
                catch
                {
                    P_HEALDNO = 0;
                }
            }

            if (!string.IsNullOrEmpty(txtOperator.Text))
                P_OPERATOR = txtOperator.Text;

            P_FLAG = "0";

            if (cbGroup.SelectedValue != null)
                P_GROUP = cbGroup.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(P_BEAMLOT)
                && !string.IsNullOrEmpty(P_DRAWINGTYPE) && !string.IsNullOrEmpty(P_REEDNO) && !string.IsNullOrEmpty(P_HEALDCOLOR)
                && !string.IsNullOrEmpty(P_OPERATOR))
            {
                result = DrawingDataService.Instance.DRAW_UPDATEDRAWING(P_BEAMLOT, P_DRAWINGTYPE
                    , P_REEDNO, P_HEALDCOLOR, P_HEALDNO, P_OPERATOR, P_FLAG, P_GROUP);

                string msg = "Beamer Roll " + P_BEAMLOT + " Drawing Complete";


                if (string.IsNullOrEmpty(result))
                {
                    msg.ShowMessageBox();

                    // เพิ่มใหม่ 21/01/17
                    Preview(P_BEAMLOT);

                    if (DrawingDataService.Instance.BEAM_UPDATEBEAMDETAIL(P_BEAMLOT, P_FLAG) == true)
                    {
                        D365_DT(P_BEAMLOT);

                        PageManager.Instance.Back();
                    }
                    else
                    {
                        "Can't Update Beam Detail".ShowMessageBox(true);
                    }
                }
                else
                    result.ShowMessageBox(true);
            }
            else
            {
                if (string.IsNullOrEmpty(P_BEAMLOT))
                {
                    "Beam Lot isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_DRAWINGTYPE))
                {
                    "Reed Type isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_REEDNO))
                {
                    "Reed No isn't null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(P_HEALDCOLOR))
                {
                    "Heald Color isn't null".ShowMessageBox(true);
                }
            }
        }

        #endregion

        private void D365_DT(string P_BEAMLOT)
        {
            try
            {
                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;

                BEAMERROLL = P_BEAMLOT;

                if (!string.IsNullOrEmpty(BEAMERROLL))
                {
                    if (D365_DT_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            if (D365_DT_ISH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_DT_ISL(HEADERID) == true)
                                    {
                                        if (D365_DT_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_DT_OPL(HEADERID) == true)
                                                {
                                                    if (D365_DT_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_DT_OUL(HEADERID) == true)
                                                            {
                                                                "Send D365 complete".Info();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            "HEADERID is null".Info();
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                "HEADERID is null".Info();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }
                }
                else
                {
                    "Beamer Roll is null".Info();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }

        #region D365_DT_BPO
        private bool D365_DT_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_BPOData> results = new List<ListD365_DT_BPOData>();

                results = D365DataService.Instance.D365_DT_BPO(BEAMERROLL);

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
                            P_LOTNO = results[i].LOTNO;
                        else
                            P_LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            P_ITEMID = results[i].ITEMID;
                        else
                            P_ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

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
                    "D365_DT_BPO Row = 0".Info();
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

        #region D365_DT_ISH
        private bool D365_DT_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_DT_ISHData> results = new List<D365_DT_ISHData>();

                results = D365DataService.Instance.D365_DT_ISH(BEAMERROLL);

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
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

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
                    "D365_DT_ISH Row = 0".Info();
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

        #region D365_DT_ISL
        private bool D365_DT_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_ISLData> results = new List<ListD365_DT_ISLData>();

                results = D365DataService.Instance.D365_DT_ISL(BEAMERROLL);

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
                    "D365_DT_ISL Row = 0".Info();
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

        #region D365_DT_OPH
        private bool D365_DT_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_DT_OPHData> results = new List<D365_DT_OPHData>();

                results = D365DataService.Instance.D365_DT_OPH(BEAMERROLL);

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
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

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
                    "D365_DT_OPH Row = 0".Info();
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

        #region D365_DT_OPL
        private bool D365_DT_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_OPLData> results = new List<ListD365_DT_OPLData>();

                results = D365DataService.Instance.D365_DT_OPL(BEAMERROLL);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

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
                    "D365_DT_OPL Row = 0".Info();
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

        #region D365_DT_OUH
        private bool D365_DT_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_DT_OUHData> results = new List<D365_DT_OUHData>();

                results = D365DataService.Instance.D365_DT_OUH(BEAMERROLL);

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
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

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
                    "D365_DT_OUH Row = 0".Info();
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

        #region D365_DT_OUL
        private bool D365_DT_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_DT_OULData> results = new List<ListD365_DT_OULData>();

                results = D365DataService.Instance.D365_DT_OUL(BEAMERROLL);

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
                    "D365_DT_OUL Row = 0".Info();
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
        public void Setup(string user, string DisplayName)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (mcName != null)
            {
                mcName = DisplayName;
            }
        }

        #endregion

    }
}

