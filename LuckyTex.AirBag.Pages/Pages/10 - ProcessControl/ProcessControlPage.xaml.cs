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

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ProcessControlPage.xaml
    /// </summary>
    public partial class ProcessControlPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ProcessControlPage()
        {
            InitializeComponent();

            LoadGroup();
        }

        #endregion

        #region Internal Variables

        private InspectionSession _session = new InspectionSession();

        string operatorId = string.Empty;
        //string producttypeID = string.Empty;
        string DEFECTID = string.Empty;
        string TESTRECORDID = string.Empty;

        decimal? GROSSLENGTH = null;
        decimal? NETLENGTH = null;
        string weightMachine = string.Empty;

        bool chkSave = false;

        string cusID = string.Empty;
        string defID = string.Empty;

        
        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            EnabledCon(true);
            txtNW.IsEnabled = false;
            txtGW.IsEnabled = false;

            ClearData();

            LoadGrade();

            ConfigManager.Instance.LoadWeightConfigs();
            weightMachine = ConfigManager.Instance.WeightConfig;

            chkManual.IsChecked = true;

            txtInsLotNo.Focus();
            txtInsLotNo.SelectAll();

            // Inspection Weight
            //InspectionWeightModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            //InspectionWeightModbusManager.Instance.Start();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            InspectionWeightModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<InspectionWeight> e)
        {
            if (null == e.Value)
            {
                return;
            }
            if (chkManual.IsChecked.HasValue && !chkManual.IsChecked.Value)
            {
                if (weightMachine == "W1")
                {
                    txtGW.Text = e.Value.W1.ToString("n2").Replace(",", string.Empty);
                }
                else if (weightMachine == "W2")
                {
                    txtGW.Text = e.Value.W2.ToString("n2").Replace(",", string.Empty);
                }

                if (txtInsLotNo.Text != "" && txtGW.Text != "")
                {
                    LoadNW();
                }
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
            #region Check(s)

            string insLotNo = txtInsLotNo.Text;
            if (string.IsNullOrWhiteSpace(insLotNo))
            {
                txtInsLotNo.SelectAll();
                txtInsLotNo.Focus();
                return;
            }

            #endregion

            if (txtCuttingLength.Text != "")
            {
                if (txtGW.Text != "")
                {
                    if (txtRemark.Text != "")
                    {
                        if (!string.IsNullOrWhiteSpace(insLotNo))
                        {
                            if (chkSave == true)
                            {
                                Preview(insLotNo);
                            }
                            else if (chkSave == false)
                            {
                                Save();
                                Preview(insLotNo);

                                ClearData();

                                if (null != _session)
                                {
                                    if (!string.IsNullOrEmpty(insLotNo))
                                    {
                                        txtInsLotNo.Text = insLotNo;

                                        if (_session.InspecionLotNo == insLotNo)
                                        {
                                            return;
                                        }

                                        LoadInspectionData(txtInsLotNo.Text);

                                        txtCuttingLength.Focus();
                                        txtCuttingLength.SelectAll();
                                    }
                                }
                            }
                        }
                        else
                        {
                            "Item Lot No. isn't Null".ShowMessageBox(true);
                        }
                    }
                    else
                    {
                        "Remark isn't Null".ShowMessageBox(true);
                    }
                }
                else
                {
                    "G.W. isn't Null".ShowMessageBox(true);
                }
            }
            else
            {
                "Cutting Length isn't Null".ShowMessageBox(true);
            }
        }
        #endregion

        #region cmdSaveNewLot
        private void cmdSaveNewLot_Click(object sender, RoutedEventArgs e)
        {
            SaveNewInsLot();
        }
        #endregion

        #endregion

        #region TextBox Handlers

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtInsLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCuttingLength.Focus();
                txtCuttingLength.SelectAll();
                e.Handled = true;
            }
        }

        private void txtInsLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            string insLot = txtInsLotNo.Text;
            if (null != _session)
            {
                if (txtInsLotNo.Text != "")
                {
                    if (_session.InspecionLotNo == insLot)
                    {
                        return;
                    }

                    LoadInspectionData(txtInsLotNo.Text);
                }
            }
        }

        private void txtCuttingLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtGW.Focus();
                txtGW.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCuttingLength_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtCuttingLength.Text != "")
            {
                decimal newCutting = 0;

                try
                {
                    newCutting = decimal.Parse(txtCuttingLength.Text);
                }
                catch
                {
                    newCutting = 0;
                    txtCuttingLength.Text = "0";
                }

                if (newCutting != 0)
                {
                    GetGrade();
                }
                else
                {
                    txtNewGrade.Text = txtGRADE.Text;
                }
            }
            else
            {
                txtNewGrade.Text = txtGRADE.Text;
            }

            chkSave = false;
        }

        private void txtGW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRemark.Focus();
                txtRemark.SelectAll();
                e.Handled = true;
            }
        }

        private void txtGW_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtInsLotNo.Text != "" && txtGW.Text != "")
            {
                LoadNW();
            }
        }

        private void txtNW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region CheckBox

        #region chkReWarehouse

        private void chkReWarehouse_Checked(object sender, RoutedEventArgs e)
        {
            cmdSaveNewLot.IsEnabled = true;

            if (null != _session)
            {
                if (!string.IsNullOrEmpty(_session.FinishingLotNo))
                    getCurrentInsData();
            }
        }

        private void chkReWarehouse_Unchecked(object sender, RoutedEventArgs e)
        {
            cmdSaveNewLot.IsEnabled = false;
            txtNewInsLotNo.Text = "";
        }

        #endregion

        #region chkManual

        private void chkManual_Checked(object sender, RoutedEventArgs e)
        {
            txtGW.IsEnabled = true;

            InspectionWeightModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Shutdown();
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            txtGW.IsEnabled = false;

            // Inspection Weight
            InspectionWeightModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Start();
        }

        #endregion

        #region chkManualGrade

        private void chkManualGrade_Checked(object sender, RoutedEventArgs e)
        {
            if (chkManualGrade.IsChecked == true)
                cbGrade.IsEnabled = true;
        }

        private void chkManualGrade_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkManualGrade.IsChecked == false)
            {
                cbGrade.IsEnabled = false;

                cbGrade.SelectedIndex = -1;
                cbGrade.SelectedValue = null;
            }
        }

        #endregion

        #endregion

        #region cbGroup_LostFocus

        private void cbGroup_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbGroup.SelectedValue != null)
                    _session.OPERATOR_GROUP = cbGroup.SelectedValue.ToString();
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string insLotNo)
        {
            try
            {
                Windows.ShiftRemarkWindow window = new Windows.ShiftRemarkWindow();
                window.Setup(insLotNo, _session.StartDate);

                if (window.ShowDialog() == true)
                {
                    if (window.useRemark == true)
                    {
                        ConmonReportService.Instance.ReportName = "InspectionRemark";

                        if (window.useShiftRemark == true)
                        {
                            ConmonReportService.Instance.UseShiftRemark = true;
                        }
                        else
                        {
                            ConmonReportService.Instance.UseShiftRemark = false;
                        }
                    }
                    else
                    {
                        ConmonReportService.Instance.ReportName = "Inspection";
                    }
                }
                else
                {
                    ConmonReportService.Instance.ReportName = "Inspection";
                }

                //if (MessageBox.Show("Show Remark on report?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    ConmonReportService.Instance.ReportName = "InspectionRemark";
                //}
                //else
                //{
                //    ConmonReportService.Instance.ReportName = "Inspection";
                //}

                string CmdString = string.Empty;
                ConmonReportService.Instance.CmdString = insLotNo;

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

        private void Preview(string insLotNo)
        {
            try
            {
                Windows.ShiftRemarkWindow window = new Windows.ShiftRemarkWindow();
                window.Setup(insLotNo, _session.StartDate);

                if (window.ShowDialog() == true)
                {
                    if (window.useRemark == true)
                    {
                        ConmonReportService.Instance.ReportName = "InspectionRemark";

                        if (window.useShiftRemark == true)
                        {
                            ConmonReportService.Instance.UseShiftRemark = true;
                        }
                        else
                        {
                            ConmonReportService.Instance.UseShiftRemark = false;
                        }
                    }
                    else
                    {
                        ConmonReportService.Instance.ReportName = "Inspection";
                    }
                }
                else
                {
                    ConmonReportService.Instance.ReportName = "Inspection";
                }

                //if (MessageBox.Show("Show Remark on report?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    ConmonReportService.Instance.ReportName = "InspectionRemark";
                //}
                //else
                //{
                //    ConmonReportService.Instance.ReportName = "Inspection";
                //}

                // ConmonReportService
                ConmonReportService.Instance.CmdString = insLotNo;

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

        #region LoadGrade

        private void LoadGrade()
        {
            if (cbGrade.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C", "T", "X" };

                cbGrade.ItemsSource = str;
                cbGrade.SelectedIndex = -1;
                cbGrade.SelectedValue = null;
            }
        }

        #endregion

        #region LoadGroup

        private void LoadGroup()
        {
            if (cbGroup.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C" };

                cbGroup.ItemsSource = str;
                cbGroup.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadInspectionData

        private void LoadInspectionData(string insLotNo)
        {
            chkSave = false;

            List<InspectionLotData> lots = new List<InspectionLotData>();
            lots = InspectionDataService.Instance.GetInspectionData(insLotNo);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {

                _session.InspecionLotNo = lots[0].INSPECTIONLOT;

                // เพิ่มใหม่ 13/05/16
                if (lots[0].STARTDATE != null)
                    _session.StartDate = lots[0].STARTDATE.Value;

                _session.LOADTYPE = lots[0].LOADTYPE;
                txtLoadingType.Text = _session.LOADTYPE;

                // เพิ่มใหม่
                _session.FinishingLotNo = lots[0].FINISHINGLOT;
                TESTRECORDID = lots[0].TESTRECORDID;
                //

                DEFECTID = lots[0].DEFECTID;

                cusID = lots[0].CUSTOMERID;
                defID = lots[0].DEFECTID;

                #region ITEMCODE
                if (lots[0].ITEMCODE != "")
                {
                    _session.ItemCode = lots[0].ITEMCODE;
                    txtItemCode.Text = lots[0].ITEMCODE;
                }
                else
                {
                    _session.ItemCode = "";
                    txtItemCode.Text = "";
                }
                #endregion

                #region STARTDATE
                if (lots[0].STARTDATE != null)
                {
                    _session.StartDate = lots[0].STARTDATE.Value;
                    txtInspectionDate.Text = lots[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");
                    LoadINS_GETCUTSAMPLELIST(_session.InspecionLotNo, lots[0].STARTDATE);
                }
                else
                {
                    _session.InspecionLotNo = "";
                    txtInspectionDate.Text = "";
                }
                #endregion

                #region CUSTOMERTYPE
                if (lots[0].CUSTOMERTYPE != "")
                {
                    _session.CustomerType = lots[0].CUSTOMERTYPE;
                    txtCustomerType.Text = lots[0].CUSTOMERTYPE;
                }
                else
                {
                    _session.CustomerType = "";
                    txtCustomerType.Text = "";
                }
                #endregion

                #region GROSSLENGTH

                if (lots[0].GROSSLENGTH != null)
                {
                    GROSSLENGTH = lots[0].GROSSLENGTH;
                    txtGROSSLENGTH.Text = GROSSLENGTH.Value.ToString("n2").Replace(",", string.Empty);
                }
                else
                {
                    txtGROSSLENGTH.Text = "";
                    GROSSLENGTH = null;
                }

                #endregion

                #region NETLENGTH

                if (lots[0].NETLENGTH != null)
                {
                    NETLENGTH = lots[0].NETLENGTH;
                }
                else
                {
                    NETLENGTH = null;
                }

                #endregion

                #region NETWEIGHT

                if (lots[0].NETWEIGHT != null)
                {
                    txtNETWEIGHT.Text = lots[0].NETWEIGHT.Value.ToString("n2").Replace(",", string.Empty);
                }
                else
                {
                    txtNETWEIGHT.Text = "";
                }

                #endregion

                #region INSPECTEDBY
                if (lots[0].INSPECTEDBY != "")
                {
                    txtINSPECTEDBY.Text = lots[0].INSPECTEDBY;
                }
                else
                {
                    txtINSPECTEDBY.Text = "";
                }
                #endregion

                #region GRADE
                if (lots[0].GRADE != "")
                {
                    txtGRADE.Text = lots[0].GRADE;
                }
                else
                {
                    txtGRADE.Text = "";
                }
                #endregion

                txtGW.Text = (lots[0].GROSSWEIGHT.HasValue) ?
                           lots[0].GROSSWEIGHT.Value.ToString() : "0.00";
                txtNW.Text = (lots[0].NETWEIGHT.HasValue) ?
                    lots[0].NETWEIGHT.Value.ToString() : "0.00";

                #region OPERATOR_GROUP
                if (!string.IsNullOrEmpty(lots[0].OPERATOR_GROUP))
                {
                    cbGroup.SelectedValue = lots[0].OPERATOR_GROUP;
                }
                else
                {
                    cbGroup.SelectedIndex = 0;
                    cbGroup.SelectedValue = "A";
                }
                #endregion

                txtCuttingLength.Focus();
                txtCuttingLength.SelectAll();

                EnabledCon(true);
            }
            else
            {
                "No Data Found".ShowMessageBox(false);
                ClearData();
            }
        }

        #endregion

        #region LoadINS_GETCUTSAMPLELIST

        private void LoadINS_GETCUTSAMPLELIST(string INS_LOT, DateTime? STARTDATE)
        {
            try
            {
                List<INS_CUTSAMPLELIST> lots = new List<INS_CUTSAMPLELIST>();

                lots = InspectionDataService.Instance.GetINS_GETCUTSAMPLELIST(INS_LOT, STARTDATE);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridCutsamplelist.ItemsSource = lots;
                }
                else
                {
                    gridCutsamplelist.ItemsSource = null;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadNW

        private void LoadNW()
        {
            try
            {
                string itemCode = txtItemCode.Text;
                if (string.IsNullOrWhiteSpace(itemCode))
                {
                    txtItemCode.SelectAll();
                    txtItemCode.Focus();
                    return;
                }

                List<InspectionItemCodeData> lots =
                    InspectionDataService.Instance.GetNW(itemCode);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    InspectionItemCodeData lot = lots[0];
                    decimal cw = decimal.Zero;
                    decimal gw = decimal.Zero;

                    if (lot.Coreweight.HasValue == true)
                    {
                        cw = lot.Coreweight.Value;
                    }

                    if (txtGW.Text != "")
                    {
                        try
                        {
                            gw = Convert.ToDecimal(txtGW.Text);
                        }
                        catch
                        {
                            gw = 0;
                        }
                    }

                    if (gw > 0)
                        txtNW.Text = MathEx.Round((gw - cw), 2).ToString();
                    else
                        txtNW.Text = "0";
                }
                else
                {
                    // Not found.
                    string msg = "item Code. {0} data not found.".args(itemCode);
                    msg.ShowMessageBox(true);

                    ClearData();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region ClearData

        private void ClearData()
        {
            try
            {
                txtInsLotNo.Text = "";
                txtItemCode.Text = "";
                txtInspectionDate.Text = "";
                txtCustomerType.Text = "";
                txtLoadingType.Text = "";
                txtGROSSLENGTH.Text = "";
                txtNETWEIGHT.Text = "";
                txtINSPECTEDBY.Text = "";
                txtGRADE.Text = "";

                txtCuttingLength.Text = "";
                txtRemark.Text = "";

                txtNewGrade.Text = "";
                chkManual.IsChecked = true;
                txtGW.Text = "0.00";
                txtNW.Text = "0.00";

                chkManualGrade.IsChecked = false;
                cbGrade.IsEnabled = false;
                cbGrade.SelectedIndex = -1;
                cbGrade.SelectedValue = null;

                gridCutsamplelist.ItemsSource = null;

                _session.InspecionLotNo = "";

                cbGroup.SelectedIndex = 0;

                cusID = string.Empty;
                defID = string.Empty;

                txtNewInsLotNo.Text = "";
                chkReWarehouse.IsChecked = false;
                cmdSaveNewLot.IsEnabled = false;

                // เพิ่มใหม่ 
                DEFECTID = string.Empty;
                TESTRECORDID = string.Empty;

                GROSSLENGTH = null;
                NETLENGTH = null;
                //

                txtInsLotNo.Focus();
                txtInsLotNo.SelectAll();

                chkSave = false;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region EnabledCon
        private void EnabledCon(bool chkManual)
        {
            txtCuttingLength.IsEnabled = chkManual;
            txtRemark.IsEnabled = chkManual;
        }
        #endregion

        #region GetGrade

        private void GetGrade()
        {
            try
            {
                decimal? CuttingLength = null;
                decimal? grossLength = null;

                if (txtCuttingLength.Text != "")
                    CuttingLength = Convert.ToDecimal(txtCuttingLength.Text);

                if (GROSSLENGTH != null)
                    grossLength = GROSSLENGTH.Value;

                if (grossLength != null && CuttingLength != null)
                {
                    txtNewGrade.Text = _session.GetGrade((grossLength.Value - CuttingLength.Value));
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region getCurrentInsData

        private void getCurrentInsData()
        {
            if (null != _session)
            {
                txtNewInsLotNo.Text = _session.GenNewInspectionLotNo();
            }
        }

        #endregion

        #region Save

        private void Save()
        {
            #region Check(s)

            string insLotNo = txtInsLotNo.Text;
            if (string.IsNullOrWhiteSpace(insLotNo))
            {
                txtInsLotNo.SelectAll();
                txtInsLotNo.Focus();
                return;
            }

            string loadingType = txtLoadingType.Text;
            string grade = string.Empty;
            string Remark = string.Empty;

            string P_GROUP = string.Empty;

            decimal? CuttingLength = null;
            decimal? grossLength = null;
            decimal? glenght = null;
            decimal? nLength = null;
            decimal? gweight = null;
            decimal? nweight = null;

            decimal? netLength = null;

            string _itemCode = string.Empty;

            if (txtCuttingLength.Text != "")
                CuttingLength = decimal.Parse(txtCuttingLength.Text);

            if (GROSSLENGTH != null)
                grossLength = GROSSLENGTH.Value;

            if (txtGW.Text != "")
                gweight = decimal.Parse(txtGW.Text);

            if (txtNW.Text != "")
                nweight = decimal.Parse(txtNW.Text);

            glenght = (grossLength-CuttingLength);

            nLength = (NETLENGTH - CuttingLength);

            if (chkManualGrade.IsChecked == false)
            {
                grade = txtGRADE.Text;
                Remark = txtRemark.Text;
            }
            else
            {
                grade = cbGrade.Text;
                Remark = txtRemark.Text + " " + "Manual Grade From (" + txtGRADE.Text + ") to (" + grade + ")";

                if (txtGRADE.Text == "A")
                {
                    if (grade == "C" || grade == "T")
                    {
                        nLength = glenght;
                    }
                }
                else if (txtGRADE.Text == "B")
                {
                    if (grade == "C" || grade == "T")
                    {
                        nLength = glenght;
                    }
                }
                else if (txtGRADE.Text == "X")
                {
                    if (grade == "C" || grade == "T")
                    {
                        nLength = glenght;
                    }
                }
                else if (txtGRADE.Text == "C")
                {
                    if (grade == "A" || grade == "B" || grade == "X")
                    {
                        _itemCode = txtItemCode.Text;

                        netLength = InspectionDataService.Instance.GetNetLength(
                                    cusID, _itemCode, glenght, grade, defID);

                        if (netLength != null)
                            nLength = netLength;
                        //else
                        //    nLength = 0;
                    }
                }
                else if (txtGRADE.Text == "T")
                {
                    if (grade == "A" || grade == "B" || grade == "X")
                    {
                        _itemCode = txtItemCode.Text;

                        netLength = InspectionDataService.Instance.GetNetLength(
                                    cusID, _itemCode, glenght, grade, defID);

                        if (netLength != null)
                            nLength = netLength;
                        //else
                        //    nLength = 0;
                    }
                }

            }

            if (cbGroup.SelectedValue != null)
                P_GROUP = cbGroup.SelectedValue.ToString();

            #endregion

            try
            {
                if (DEFECTID != "")
                {
                    InspectionDataService.Instance.INS_DELETEDEFECTBYLENGTH(DEFECTID, glenght, operatorId);
                }

                if (insLotNo != "")
                {
                    InspectionDataService.Instance.EditInspectionProcess(insLotNo, _session.StartDate, _session.CustomerType
                        , glenght.Value, nLength.Value, gweight.Value, nweight.Value, grade, loadingType, P_GROUP);

                    InspectionDataService.Instance.INS_CUTSAMPLE(insLotNo, _session.StartDate, CuttingLength.Value, Remark, operatorId);
                }

                chkSave = true;
                EnabledCon(false);
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                chkSave = false;
            }
        }

        #endregion

        #region SaveNewInsLot

        private void SaveNewInsLot()
        {
            string insLotNo = txtInsLotNo.Text;
            string P_INSNEW = txtNewInsLotNo.Text;

            if (!string.IsNullOrEmpty(insLotNo) && !string.IsNullOrEmpty(P_INSNEW))
            {
                if (_session.INS_REWAREHOUSE(insLotNo, DEFECTID, TESTRECORDID, P_INSNEW) == true)
                {
                    string msg = string.Empty;

                    msg = "Inspection Lot " + P_INSNEW + " has been created.";
                    msg.ShowMessageBox();
                    ClearData();
                }
            }
        }

        #endregion

        #endregion

        #region Public Methods

        public void Setup(string userID)
        {
            operatorId = userID;

            if (operatorId != "")
                txtOperator.Text = operatorId;
        }

        #endregion

    }
}
