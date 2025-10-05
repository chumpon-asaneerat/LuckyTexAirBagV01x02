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
using LuckyTex.Pages;
#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for WeightWindow.xaml
    /// </summary>
    public partial class WeightMeasurementWindow : Window
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeightMeasurementWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables
        private string _lotNo = null;

        private DateTime _StartDate = DateTime.MinValue;
        private string _customerType = string.Empty;
        private string _loadType = string.Empty;
        private string _producttypeID = string.Empty;

        string weightMachine = string.Empty;

        #endregion

        #region Load/Unload

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtLotNo.IsEnabled = false;
            txtNW.IsEnabled = false;
            txtGW.IsEnabled = false;

            LoadInsLotData();
            ConfigManager.Instance.LoadWeightConfigs();
            weightMachine = ConfigManager.Instance.WeightConfig;

            // Inspection Weight
            InspectionWeightModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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

                if (txtLotNo.Text != "" && txtGW.Text != "")
                {
                    LoadNW();
                }
            }
        }

        #endregion

        #region ClearInputs

        private void ClearInputs()
        {
            txtLotNo.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtLength.Text = "0.00";
            txtGrade.Text = "-";

            txtGW.Text = "0.00";
            txtNW.Text = "0.00";

            _StartDate = DateTime.MinValue;
            // เพิ่มใหม่
            _customerType = string.Empty;
            _loadType = string.Empty;
            _producttypeID = string.Empty;
        }

        #endregion

        #region LoadInsLotData

        private void LoadInsLotData()
        {
            try
            {

                string insLotNo = _lotNo;

                    txtLotNo.Text = insLotNo;
               
                // Get Inspection Lot Data
                List<InspectionLotData> lots =
                    InspectionDataService.Instance.GetInspectionData(insLotNo);
                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    InspectionLotData lot = lots[0];
                    if (null != lot)
                    {
                        // Update GUI
                        txtItem.Text = (!string.IsNullOrWhiteSpace(lot.ITEMCODE)) ?
                            lot.ITEMCODE.Trim().ToUpper() : string.Empty;
                        txtLength.Text = (lot.GROSSLENGTH.HasValue) ?
                            lot.GROSSLENGTH.Value.ToString("n2") : "0.00";

                        txtGrade.Text = (!string.IsNullOrWhiteSpace(lot.GRADE)) ?
                            lot.GRADE.Trim().ToUpper() : "-";

                        txtGW.Text = (lot.GROSSWEIGHT.HasValue) ?
                            lot.GROSSWEIGHT.Value.ToString() : "0.00";
                        txtNW.Text = (lot.NETWEIGHT.HasValue) ?
                            lot.NETWEIGHT.Value.ToString() : "0.00";

                        _StartDate = (lot.STARTDATE.HasValue) ?
                            lot.STARTDATE.Value : DateTime.MinValue;

                        _loadType = lot.LOADTYPE;

                        // เพิ่มใหม่
                        _customerType = lot.CUSTOMERTYPE;

                        //_customerType = (!string.IsNullOrWhiteSpace(lot.CUSTOMERTYPE)) ?
                        //    lot.CUSTOMERTYPE.Trim().ToUpper() : "-";

                        _producttypeID = lot.PRODUCTTYPEID;

                        txtGW.Focus();
                        txtGW.SelectAll();
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

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="items">The item source.</param>
        public void Setup(string lotNo)
        {
            _lotNo = lotNo;
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
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

            #endregion

            if (!string.IsNullOrWhiteSpace(insLotNo))
            {
                // Save GW, NW
                InspectionDataService.Instance.UpdateWeights(insLotNo, _StartDate, _producttypeID, _customerType, _loadType, dGW, dNW);

                // Test Print
                Print(insLotNo);

                this.DialogResult = true;
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

            #endregion

            if (!string.IsNullOrWhiteSpace(insLotNo))
            {
                // Save GW, NW
                InspectionDataService.Instance.UpdateWeights(insLotNo, _StartDate, _producttypeID, _customerType, _loadType, dGW, dNW);

                Preview(insLotNo);

                this.DialogResult = false;
            }
        }

        #endregion

        #region TextBox Handlers

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtGW.Focus();
                txtGW.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            // Gets related information from Lot No. (Inspection lot).
            if (txtLotNo.Text != "")
                LoadInsLotData();
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
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            txtGW.IsEnabled = false;
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

    }
}
