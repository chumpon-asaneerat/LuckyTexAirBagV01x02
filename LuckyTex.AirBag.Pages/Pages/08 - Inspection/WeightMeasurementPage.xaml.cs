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
    /// Interaction logic for WeightMeasurementPage.xaml
    /// </summary>
    public partial class WeightMeasurementPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeightMeasurementPage()
        {
            InitializeComponent();
            ConmonReportService.Instance.OperatorID = string.Empty;

            txtCustomerType.IsEnabled = false;
        }

        #endregion

        #region Internal Variables

        private DateTime _StartDate = DateTime.MinValue;
        private string _customerType = string.Empty;
        private string _loadType = string.Empty;

        private string _OldcustomerType = string.Empty;
        private string _OldloadType = string.Empty;
        decimal? _OldGW = null;
        decimal? _OldNW = null;

        string weightMachine = string.Empty;

        #endregion

        #region Private Methods

        #region ClearInputs

        private void ClearInputs()
        {
            txtItemLotNo.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtLength.Text = "0.00";
            txtGrade.Text = "-";
            txtLoadingType.Text = string.Empty;
            txtGW.Text = "0.00";
            txtNW.Text = "0.00";

            _StartDate = DateTime.MinValue;
            // เพิ่มใหม่
            _customerType = string.Empty;
            _loadType = string.Empty;

            _OldcustomerType = string.Empty;
            _OldloadType = string.Empty;

            _OldGW = null;
            _OldNW = null;

            txtCustomerType.Text = "";
            rbMass.IsChecked = true;
            rbTest.IsChecked = false;    
        }

        #endregion

        #region LoadFinLotData

        private void LoadFinLotData()
        {
            try
            {
                string insItemLotNo = txtItemLotNo.Text;
                if (string.IsNullOrWhiteSpace(insItemLotNo))
                {
                    txtItemLotNo.SelectAll();
                    txtItemLotNo.Focus();
                    return;
                }


                List<INS_GetFinishinslotData> lots = GetINS_GetFinishinsLotData(insItemLotNo);

                if (lots.Count > 0)
                {
                    Windows.InspectionLotSelectionWindow win = new Windows.InspectionLotSelectionWindow();

                    win.Setup(lots);

                    if (win.ShowDialog() == false)
                    {
                        ClearInputs();
                        return;
                    }

                    if (null != win.LotNoText)
                    {
                        txtLotNo.Text = win.LotNoText;

                        // ยังไม่ใช้ Auto Load InsLotData
                        //LoadInsLotData();
                    }
                }
                else
                {
                    "No Inspection Lot Data".ShowMessageBox(true);
                    ClearInputs();
                }
            }
            catch
            {
                "No Inspection Lot Data".ShowMessageBox(true);
                ClearInputs();
            }
        }

        #endregion

        #region LoadInsLotData

        private void LoadInsLotData()
        {
            try
            {
                string insLotNo = txtLotNo.Text;
                if (string.IsNullOrWhiteSpace(insLotNo))
                {
                    txtLotNo.SelectAll();
                    txtLotNo.Focus();
                    return;
                }


                // Get Inspection Lot Data
                List<InspectionLotData> lots =
                    InspectionDataService.Instance.GetInspectionData(insLotNo);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    InspectionLotData lot = lots[0];
                    if (null != lot)
                    {
                        _OldloadType = string.Empty;
                        _OldcustomerType = string.Empty;

                        _OldGW = null;
                        _OldNW = null;

                        // Update GUI
                        txtItem.Text = (!string.IsNullOrWhiteSpace(lot.ITEMCODE)) ?
                            lot.ITEMCODE.Trim().ToUpper() : string.Empty;

                        txtLength.Text = (lot.GROSSLENGTH.HasValue) ?
                            lot.GROSSLENGTH.Value.ToString("n2") : "0.00";

                        txtGrade.Text = (!string.IsNullOrWhiteSpace(lot.GRADE)) ?
                            lot.GRADE.Trim().ToUpper() : "-";

                        // เพิ่มใหม่
                        _loadType = lot.LOADTYPE;
                        _OldloadType = _loadType;

                        txtLoadingType.Text = _loadType;



                        if (lot.GROSSWEIGHT != null)
                        {
                            txtGW.Text = (lot.GROSSWEIGHT.HasValue) ?
                           lot.GROSSWEIGHT.Value.ToString() : "0.00";

                            _OldGW = lot.GROSSWEIGHT;
                        }
                        if (lot.NETWEIGHT != null)
                        {
                            txtNW.Text = (lot.NETWEIGHT.HasValue) ?
                           lot.NETWEIGHT.Value.ToString() : "0.00";

                            _OldNW = lot.NETWEIGHT;
                        }


                        _StartDate = (lot.STARTDATE.HasValue) ?
                            lot.STARTDATE.Value : DateTime.MinValue;

                        // เพิ่มใหม่
                        _customerType = lot.CUSTOMERTYPE;
                        _OldcustomerType = _customerType;

                        txtCustomerType.Text = _customerType;

                        if (lot.PRODUCTTYPEID == "1")
                        {
                            rbMass.IsChecked = true;
                            rbTest.IsChecked = false;
                        }
                        else if (lot.PRODUCTTYPEID == "2")
                        {
                            rbMass.IsChecked = false;
                            rbTest.IsChecked = true;
                        }

                    }
                }
                else
                {
                    // Not found.
                    string msg = "Lot No. {0} data not found.".args(insLotNo);
                    msg.ShowMessageBox(true);

                    ClearInputs();
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
                string itemCode = txtItem.Text;
                if (string.IsNullOrWhiteSpace(itemCode))
                {
                    txtItem.SelectAll();
                    txtItem.Focus();
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

                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        // เพิ่มใหม่ใช้ในการ Load ข้อมูล INS_GetFinishinsLotData
        #region GetINS_GetFinishinsLotData

        /// <summary>
        /// Gets FinishinsLot List.
        /// </summary>
        /// <returns>Returns FinishinsLot list.</returns>
        public List<INS_GetFinishinslotData> GetINS_GetFinishinsLotData(string itemLotNo)
        {
            List<INS_GetFinishinslotData> results = InspectionDataService.Instance
                .GetINS_GetFinishinslotDataList(itemLotNo);

            return results;
        }

        #endregion

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtNW.IsEnabled = false;
            txtGW.IsEnabled = false;
            txtLotNo.Focus();

            ClearInputs();
            ConfigManager.Instance.LoadWeightConfigs();
            weightMachine = ConfigManager.Instance.WeightConfig;

            chkManual.IsChecked = true;

            //// Inspection Weight
            //InspectionWeightModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            //InspectionWeightModbusManager.Instance.Start();
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

                if (txtLotNo.Text != "" && txtGW.Text != "")
                {
                    LoadNW();
                }
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            InspectionWeightModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Shutdown();
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdReprintReport_Click(object sender, RoutedEventArgs e)
        {
            // Re-Printing

            // Clear inputs
            ClearInputs();
        }

        private void cmdPrintReport_Click(object sender, RoutedEventArgs e)
        {
            #region Check(s)
            
            string insLotNo = txtLotNo.Text;
            if (string.IsNullOrWhiteSpace(insLotNo))
            {
                txtLotNo.SelectAll();
                txtLotNo.Focus();
                return;
            }
            
            string sGW = txtGW.Text;
            decimal dGW = decimal.Zero;
            if (string.IsNullOrWhiteSpace(sGW) ||
                !decimal.TryParse(sGW.Replace(",", string.Empty), out dGW))
            {
                txtGW.SelectAll();
                txtGW.Focus();
                return;
            }

            string sNW = txtNW.Text;
            decimal dNW = decimal.Zero;

            if (string.IsNullOrWhiteSpace(sNW) ||
                !decimal.TryParse(sNW.Replace(",", string.Empty), out dNW))
            {
                txtNW.SelectAll();
                txtNW.Focus();
                return;
            }

            string producttypeID = string.Empty;

            if (rbMass.IsChecked == true && rbTest.IsChecked == false)
            {
                producttypeID = "1";
            }
            else if (rbMass.IsChecked == false && rbTest.IsChecked == true)
            {
                producttypeID = "2";
            }

            if (!string.IsNullOrEmpty(txtCustomerType.Text))
            {
                if (_customerType != txtCustomerType.Text)
                    _customerType = txtCustomerType.Text;
            }

            if (!string.IsNullOrEmpty(txtLoadingType.Text))
            {
                if (_loadType != txtLoadingType.Text)
                    _loadType = txtLoadingType.Text;
            }

            #endregion

            if (!string.IsNullOrWhiteSpace(insLotNo))
            {
                // Save GW, NW
                if (InspectionDataService.Instance.UpdateWeights(insLotNo, _StartDate, producttypeID, _customerType, _loadType, dGW, dNW) == true)
                {
                    ConmonReportService.Instance.INSLOT = insLotNo;

                    SaveEdit_Log(dGW, dNW);

                    // Test Print
                    Print(insLotNo);

                    // Clear inputs
                    ClearInputs();
                }
                else
                {
                    "Can't Update Weight".ShowMessageBox(true);
                }
            }
        }

        private void cmdPreview_Click(object sender, RoutedEventArgs e)
        {
            #region Check(s)

            string insLotNo = txtLotNo.Text;
            if (string.IsNullOrWhiteSpace(insLotNo))
            {
                txtLotNo.SelectAll();
                txtLotNo.Focus();
                return;
            }

            string sGW = txtGW.Text;
            decimal dGW = decimal.Zero;
            if (string.IsNullOrWhiteSpace(sGW) ||
                !decimal.TryParse(sGW.Replace(",", string.Empty), out dGW))
            {
                txtGW.SelectAll();
                txtGW.Focus();
                return;
            }

            string sNW = txtNW.Text;
            decimal dNW = decimal.Zero;

            if (string.IsNullOrWhiteSpace(sNW) ||
                !decimal.TryParse(sNW.Replace(",", string.Empty), out dNW))
            {
                txtNW.SelectAll();
                txtNW.Focus();
                return;
            }

            string producttypeID = string.Empty;

            if (rbMass.IsChecked == true && rbTest.IsChecked == false)
            {
                producttypeID = "1";
            }
            else if (rbMass.IsChecked == false && rbTest.IsChecked == true)
            {
                producttypeID = "2";
            }

            if (!string.IsNullOrEmpty(txtCustomerType.Text))
            {
                if (_customerType != txtCustomerType.Text)
                    _customerType = txtCustomerType.Text;
            }

            if (!string.IsNullOrEmpty(txtLoadingType.Text))
            {
                if (_loadType != txtLoadingType.Text)
                    _loadType = txtLoadingType.Text;
            }

            #endregion

            if (!string.IsNullOrWhiteSpace(insLotNo))
            {
                // Save GW, NW
                InspectionDataService.Instance.UpdateWeights(insLotNo, _StartDate, producttypeID, _customerType, _loadType, dGW, dNW);
                
                ConmonReportService.Instance.INSLOT = insLotNo;

                SaveEdit_Log(dGW, dNW);

                Preview(insLotNo);
            }
        }

        #endregion

        #region TextBox Handlers

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtItemLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLotNo.Focus();
                txtLotNo.SelectAll();
                e.Handled = true;
            }
        }

        private void txtItemLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtItemLotNo.Text != "")
                LoadFinLotData();
        }

        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCustomerType.Focus();
                txtCustomerType.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            // Gets related information from Lot No. (Inspection lot).
            if (txtLotNo.Text != "")
                LoadInsLotData();
        }

        private void txtCustomerType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLoadingType.Focus();
                txtLoadingType.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCustomerType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerType.Text) && txtCustomerType.Text != _customerType)
                _customerType = txtCustomerType.Text;
        }

        private void txtLoadingType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtGW.Focus();
                txtGW.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLoadingType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLoadingType.Text) && txtLoadingType.Text != _loadType)
                _loadType = txtLoadingType.Text;
        }

        private void txtGW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                //cmdPrintReport.Focus();
                e.Handled = true;
            }
        }

        private void txtGW_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLotNo.Text != "")
            {
                LoadNW();
            }
        }

        private void txtNW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                //cmdPrintReport.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region CheckBox

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
                window.Setup(insLotNo, _StartDate);

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
                        ConmonReportService.Instance.ReportName = "RePrint_Inspection";
                    }
                }
                else
                {
                    ConmonReportService.Instance.ReportName = "RePrint_Inspection";
                }

                //if (MessageBox.Show("Show Remark on report?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    ConmonReportService.Instance.ReportName = "InspectionRemark";
                //}
                //else
                //{
                //    ConmonReportService.Instance.ReportName = "RePrint_Inspection";
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

                if (ConmonReportService.Instance.ReportName == "RePrint_Inspection")
                {
                    if (!string.IsNullOrEmpty(ConmonReportService.Instance.INSLOT) && !string.IsNullOrEmpty(ConmonReportService.Instance.OperatorID))
                    {
                        string P_COMPUTORNAME = string.Empty;
                        string OPERATOR = string.Empty;
                        string P_LOT = string.Empty;
                        string P_OPERATION = string.Empty;
                        string P_OPERATORID = string.Empty;
                        string P_PROCESSID = string.Empty;

                        P_PROCESSID = "8";
                        P_OPERATION = "Re-Print Report";
                        P_LOT = ConmonReportService.Instance.INSLOT;
                        P_OPERATORID = ConmonReportService.Instance.OperatorID;
                        P_COMPUTORNAME = System.Environment.MachineName;

                        if (PackingDataService.Instance.LOG_INSERT(P_COMPUTORNAME, OPERATOR, P_LOT, P_OPERATION, P_OPERATORID, P_PROCESSID) == false)
                        {
                            MessageBox.Show("Can't LOG_INSERT", "Can't Save Data", MessageBoxButton.OK, MessageBoxImage.Error);
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

        #region Preview

        private void Preview(string insLotNo)
        {
            try
            {
                Windows.ShiftRemarkWindow window = new Windows.ShiftRemarkWindow();
                window.Setup(insLotNo, _StartDate);

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
                        ConmonReportService.Instance.ReportName = "RePrint_Inspection";
                    }
                }
                else
                {
                    ConmonReportService.Instance.ReportName = "RePrint_Inspection";
                }
                //if (MessageBox.Show("Show Remark on report?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    ConmonReportService.Instance.ReportName = "InspectionRemark";
                //}
                //else
                //{
                //    ConmonReportService.Instance.ReportName = "RePrint_Inspection";
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

        #region SaveEdit_Log
        private void SaveEdit_Log(decimal? dGW, decimal dNW)
        {
            if (!string.IsNullOrEmpty(ConmonReportService.Instance.INSLOT) && !string.IsNullOrEmpty(ConmonReportService.Instance.OperatorID))
            {
                string editType = string.Empty;

                if (_customerType != _OldcustomerType)
                    editType = "Customer Type";

                if (_loadType != _OldloadType)
                {
                    if (!string.IsNullOrEmpty(editType))
                        editType += " , Loading Type";
                    else
                        editType = "Loading Type";
                }

                if (_OldGW != dGW)
                {
                    if (!string.IsNullOrEmpty(editType))
                        editType += " , G.W.";
                    else
                        editType = "G.W.";
                }

                if (_OldNW != dNW)
                {
                    if (!string.IsNullOrEmpty(editType))
                        editType += " , N.W.";
                    else
                        editType = "N.W.";
                }

                if (!string.IsNullOrEmpty(editType))
                {
                    string P_COMPUTORNAME = string.Empty;
                    string OPERATOR = string.Empty;
                    string P_LOT = string.Empty;
                    string P_OPERATION = string.Empty;
                    string P_OPERATORID = string.Empty;
                    string P_PROCESSID = string.Empty;

                    P_PROCESSID = "8";
                    P_OPERATION = "Edit " + editType;
                    P_LOT = ConmonReportService.Instance.INSLOT;
                    P_OPERATORID = ConmonReportService.Instance.OperatorID;
                    P_COMPUTORNAME = System.Environment.MachineName;

                    if (PackingDataService.Instance.LOG_INSERT(P_COMPUTORNAME, OPERATOR, P_LOT, P_OPERATION, P_OPERATORID, P_PROCESSID) == false)
                    {
                        MessageBox.Show("Can't LOG_INSERT", "Can't Save Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Setup
        public void Setup(string UserID)
        {
            ConmonReportService.Instance.OperatorID = UserID;
        }
        #endregion
    }
}
