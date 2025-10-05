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

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using System.Reflection;
using iTextSharp.text;

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

using System.Configuration;
using System.Data;
using System.Data.OleDb;
//using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;

using NPOI.XSSF.UserModel;
using NPOI.XSSF.Model; 
#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for StiffnessPage.xaml
    /// </summary>
    public partial class StiffnessPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StiffnessPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        private bool mcStatus = true;

        int? highpressFlage = 0;

        string itemCode = string.Empty;
        string productionlot = string.Empty;

        decimal? warp1 = null;
        decimal? warp2 = null;
        decimal? warp3 = null;
        decimal? weft1 = null;
        decimal? weft2 = null;
        decimal? weft3 = null;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (opera != "")
                txtOperator.Text = opera;

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.ScouringLabMachineConfig;

            ClearControl();

            if (ScouringLabModbusManager.Instance.Reset_HIGHPRESS() == false)
            {
                "Can't Connect PLC Please check config or PLC".ShowMessageBox();
            }

            //if (mcStatus == true)
            //{
            //    ScouringLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
            //    ScouringLabModbusManager.Instance.Start();
            //}
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ScouringLabModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
            ScouringLabModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<ScouringLab> e)
        {
            if (null == e.Value)
            {
                return;
            }

            if (e.Value.HIGHPRESSFLAG == 1)
            {
                //txtWeight.Text = e.Value.HIGHPRESS.Value.ToString("n2").Replace(",", string.Empty);

                if (e.Value.HIGHPRESS != null)
                    txtWeight.Text = MathEx.Round(e.Value.HIGHPRESS.Value, 2).ToString("#,##0.##");
                else
                    txtWeight.Text = "0.00";

                highpressFlage = 1;
            }
            else
            {
                //txtWeight.Text = "0.00";
                highpressFlage = 0;
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

        #region cmdSelect_Click
        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            if (chkRetest.IsChecked == true)
            {
                SelectPLC_ReTest();
            }
            else
            {
                SelectPLC();
            }
        }
        #endregion

        #region cmdSelect_KeyDown
        private void cmdSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (chkRetest.IsChecked == true)
                {
                    SelectPLC_ReTest();
                }
                else
                {
                    SelectPLC();
                }
            }
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (!string.IsNullOrEmpty(txtPRODUCTIONLOT.Text) && !string.IsNullOrEmpty(txtITMCODE.Text))
            {
                if (chkRetest.IsChecked == true)
                {
                    if (!string.IsNullOrEmpty(txtWarp1.Text) || !string.IsNullOrEmpty(txtWarp2.Text) || !string.IsNullOrEmpty(txtWarp3.Text)
                       || !string.IsNullOrEmpty(txtWeft1.Text) || !string.IsNullOrEmpty(txtWeft2.Text) || !string.IsNullOrEmpty(txtWeft3.Text))
                    {
                        if (Lab_SaveREPLCSTIFFNESS() == true)
                        {
                            if ("ต้องการ Re-Test Lot นี้ อีกครั้งหรือไม่".ShowMessageYesNo() == true)
                            {
                                ClearControlReTest();
                            }
                            else
                            {
                                ClearControl();
                            }
                        }
                        else
                        {
                            "Can't Save Please check data".ShowMessageBox();
                        }
                    }
                    else
                    {
                        "Please Select PLC Data Before Save".ShowMessageBox();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtWarp1.Text) && string.IsNullOrEmpty(txtWarp2.Text) && string.IsNullOrEmpty(txtWarp3.Text)
                        && string.IsNullOrEmpty(txtWeft1.Text) && string.IsNullOrEmpty(txtWeft2.Text) && string.IsNullOrEmpty(txtWeft3.Text))
                    {
                        "Please Select PLC Data Before Save".ShowMessageBox();
                    }
                    else
                    {
                        if (CheckDataNull() == true)
                        {
                            if (LAB_SAVEPLCDATA() == true)
                            {
                                "Save Data Complete".ShowMessageBox();

                                ClearControl();
                            }
                            else
                            {
                                "Can't Save Please check data".ShowMessageBox();
                            }
                        }
                        else
                        {
                            "Please Select PLC Data".ShowMessageBox();
                        }
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtPRODUCTIONLOT.Text) && !string.IsNullOrEmpty(txtITMCODE.Text))
                    "Please Select Production Lot".ShowMessageBox();
                else if (string.IsNullOrEmpty(txtITMCODE.Text) && !string.IsNullOrEmpty(txtPRODUCTIONLOT.Text))
                    "Please Select Item Code".ShowMessageBox();
                else
                    "Please check Data".ShowMessageBox();
            }

            buttonEnabled(true);
        }
        #endregion

        #endregion

        #region TextBox Handlers

        #region NumberValidationTextBox

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            string onlyNumeric = @"^([0-9]+(.[0-9]+)?)$";
            Regex regex = new Regex(onlyNumeric);
            e.Handled = !regex.IsMatch(e.Text);
        }

        #endregion

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region General

        #region KeyDown

        #region txtPRODUCTIONLOT_KeyDown
        private void txtPRODUCTIONLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtITMCODE.Focus();
                txtITMCODE.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtITMCODE_KeyDown
        private void txtITMCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (chkManual.IsChecked == true)
                {
                    txtWeight.Focus();
                    txtWeight.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region txtWeight_KeyDown
        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSelect.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWarp1_KeyDown
        private void txtWarp1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWarp2.Focus();
                txtWarp2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWarp2_KeyDown
        private void txtWarp2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWarp3.Focus();
                txtWarp3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWarp3_KeyDown
        private void txtWarp3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeft1.Focus();
                txtWeft1.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeft1_KeyDown
        private void txtWeft1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeft2.Focus();
                txtWeft2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeft2_KeyDown
        private void txtWeft2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeft3.Focus();
                txtWeft3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeft3_KeyDown
        private void txtWeft3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region LostFocus

        #region txtITMCODE_LostFocus
        private void txtITMCODE_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtITMCODE.Text))
                itemCode = txtITMCODE.Text;
            else
                itemCode = string.Empty;
        }
        #endregion

        #region txtPRODUCTIONLOT_LostFocus
        private void txtPRODUCTIONLOT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPRODUCTIONLOT.Text))
            {
                string temp = txtPRODUCTIONLOT.Text.Substring(txtPRODUCTIONLOT.Text.Length - 1);

                if (temp == "0")
                {
                    productionlot = txtPRODUCTIONLOT.Text.Substring(0, txtPRODUCTIONLOT.Text.Length - 1) + "1";
                }
                else
                {
                    productionlot = txtPRODUCTIONLOT.Text.Substring(0, txtPRODUCTIONLOT.Text.Length - 1) + "R";
                }

                txtPRODUCTIONLOT.Text = productionlot;
            }
            else
                productionlot = string.Empty;

            //if (!string.IsNullOrEmpty(txtPRODUCTIONLOT.Text))
            //    productionlot = txtPRODUCTIONLOT.Text;
            //else
            //    productionlot = string.Empty;
        }
        #endregion

        #region txtWarp1_LostFocus
        private void txtWarp1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWarp1.Text))
                warp1 = decimal.Parse(txtWarp1.Text);
            else
                warp1 = null;
        }
        #endregion

        #region txtWarp2_LostFocus
        private void txtWarp2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWarp2.Text))
                warp2 = decimal.Parse(txtWarp2.Text);
            else
                warp2 = null;
        }
        #endregion

        #region txtWarp3_LostFocus
        private void txtWarp3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWarp3.Text))
                warp3 = decimal.Parse(txtWarp3.Text);
            else
                warp3 = null;
        }
        #endregion

        #region txtWeft1_LostFocus
        private void txtWeft1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeft1.Text))
                weft1 = decimal.Parse(txtWeft1.Text);
            else
                weft1 = null;
        }
        #endregion

        #region txtWeft2_LostFocus
        private void txtWeft2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeft2.Text))
                weft2 = decimal.Parse(txtWeft2.Text);
            else
                weft2 = null;
        }
        #endregion

        #region txtWeft3_LostFocus
        private void txtWeft3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeft3.Text))
                weft3 = decimal.Parse(txtWeft3.Text);
            else
                weft3 = null;
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region CheckBox

        private void chkManual_Checked(object sender, RoutedEventArgs e)
        {
            EnabledCon(true);
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            EnabledCon(false);
        }

        private void chkRetest_Checked(object sender, RoutedEventArgs e)
        {
            rbWarpN1.Visibility = Visibility.Visible;
            rbWarpN2.Visibility = Visibility.Visible;
            rbWarpN3.Visibility = Visibility.Visible;

            rbWeftN1.Visibility = Visibility.Visible;
            rbWeftN2.Visibility = Visibility.Visible;
            rbWeftN3.Visibility = Visibility.Visible;

            rbWarpN1.IsChecked = true;
            rbWeftN1.IsChecked = true;
        }

        private void chkRetest_Unchecked(object sender, RoutedEventArgs e)
        {
            rbWarpN1.Visibility = Visibility.Collapsed;
            rbWarpN2.Visibility = Visibility.Collapsed;
            rbWarpN3.Visibility = Visibility.Collapsed;

            rbWeftN1.Visibility = Visibility.Collapsed;
            rbWeftN2.Visibility = Visibility.Collapsed;
            rbWeftN3.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            itemCode = string.Empty;
            productionlot = string.Empty;
            warp1 = null;
            warp2 = null;
            warp3 = null;
            weft1 = null;
            weft2 = null;
            weft3 = null;

            txtITMCODE.Text = string.Empty;
            txtPRODUCTIONLOT.Text = string.Empty;

            txtWeight.Text = string.Empty;
            txtWarp1.Text = string.Empty;
            txtWarp2.Text = string.Empty;
            txtWarp3.Text = string.Empty;
            txtWeft1.Text = string.Empty;
            txtWeft2.Text = string.Empty;
            txtWeft3.Text = string.Empty;

            rbWarp.IsChecked = true;
            rbWeft.IsChecked = false;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = false;

            rbWarpN1.Visibility = Visibility.Collapsed;
            rbWarpN2.Visibility = Visibility.Collapsed;
            rbWarpN3.Visibility = Visibility.Collapsed;

            rbWeftN1.Visibility = Visibility.Collapsed;
            rbWeftN2.Visibility = Visibility.Collapsed;
            rbWeftN3.Visibility = Visibility.Collapsed;

            rbWarpN1.IsChecked = true;
            rbWeftN1.IsChecked = true;

            EnabledCon(false);

            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region ClearControlReTest

        private void ClearControlReTest()
        {
            warp1 = null;
            warp2 = null;
            warp3 = null;
            weft1 = null;
            weft2 = null;
            weft3 = null;

            txtWeight.Text = string.Empty;
            txtWarp1.Text = string.Empty;
            txtWarp2.Text = string.Empty;
            txtWarp3.Text = string.Empty;
            txtWeft1.Text = string.Empty;
            txtWeft2.Text = string.Empty;
            txtWeft3.Text = string.Empty;

            rbWarp.IsChecked = true;
            rbWeft.IsChecked = false;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = true;

            rbWarpN1.IsChecked = true;
            rbWeftN1.IsChecked = true;

            EnabledCon(false);

            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region EnabledCon
        private void EnabledCon(bool chkManual)
        {
            txtWeight.IsEnabled = chkManual;
            txtWarp1.IsEnabled = chkManual;
            txtWarp2.IsEnabled = chkManual;
            txtWarp3.IsEnabled = chkManual;
            txtWeft1.IsEnabled = chkManual;
            txtWeft2.IsEnabled = chkManual;
            txtWeft3.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    ScouringLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
                    ScouringLabModbusManager.Instance.Start();

                    //if (ScouringLabModbusManager.Instance.Reset_HIGHPRESS() == false)
                    //{
                    //    "Can't Connect PLC Please check config or PLC".ShowMessageBox();
                    //}
                }
                else
                {
                    "Machine Status = false Please check config".ShowMessageBox();
                }
            }
            else
            {
                ScouringLabModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
                ScouringLabModbusManager.Instance.Shutdown();

                txtWeight.Text = "0.00";
                highpressFlage = 0;
            }
        }
        #endregion

        #region CheckDataNull
        private bool CheckDataNull()
        {
            bool chkData = false;
            bool chkWeft = false;
            bool chkWarp = false;

            try
            {
                if (!string.IsNullOrEmpty(txtWeft3.Text) && !string.IsNullOrEmpty(txtWeft2.Text) && !string.IsNullOrEmpty(txtWeft1.Text))
                {
                    chkWeft = true;
                }
                else if (string.IsNullOrEmpty(txtWeft3.Text) && !string.IsNullOrEmpty(txtWeft2.Text) && !string.IsNullOrEmpty(txtWeft1.Text))
                {
                    chkWeft = false;
                }
                else if (string.IsNullOrEmpty(txtWeft3.Text) && string.IsNullOrEmpty(txtWeft2.Text) && !string.IsNullOrEmpty(txtWeft1.Text))
                {
                    chkWeft = false;
                }
                else if (string.IsNullOrEmpty(txtWeft3.Text) && string.IsNullOrEmpty(txtWeft2.Text) && string.IsNullOrEmpty(txtWeft1.Text))
                {
                    chkWeft = true;
                }
                else
                {
                    chkWeft = false;
                }


                if (!string.IsNullOrEmpty(txtWarp3.Text) && !string.IsNullOrEmpty(txtWarp2.Text) && !string.IsNullOrEmpty(txtWarp1.Text))
                {
                    chkWarp = true;
                }
                else if (string.IsNullOrEmpty(txtWarp3.Text) && !string.IsNullOrEmpty(txtWarp2.Text) && !string.IsNullOrEmpty(txtWarp1.Text))
                {
                    chkWarp = false;
                }
                else if (string.IsNullOrEmpty(txtWarp3.Text) && string.IsNullOrEmpty(txtWarp2.Text) && !string.IsNullOrEmpty(txtWarp1.Text))
                {
                    chkWarp = false;
                }
                else if (string.IsNullOrEmpty(txtWarp3.Text) && string.IsNullOrEmpty(txtWarp2.Text) && string.IsNullOrEmpty(txtWarp1.Text))
                {
                    chkWarp = true;
                }
                else
                {
                    chkWarp = false;
                }


                if (chkWeft == true && chkWarp == true)
                    chkData = true;
                else
                    chkData = false;
            }
            catch
            {
                chkData = false;
            }

            return chkData;
        }
        #endregion

        #region LAB_SAVEPLCDATA
        private bool LAB_SAVEPLCDATA()
        {
            bool chkLoad = true;

            try
            {
                #region default

                string P_ITMCODE = string.Empty;
                string P_PRODUCTIONLOT = string.Empty;
                decimal? P_TOTALWEIGHT1 = null;
                decimal? P_TOTALWEIGHT2 = null;
                decimal? P_TOTALWEIGHT3 = null;
                decimal? P_TOTALWEIGHT4 = null;
                decimal? P_TOTALWEIGHT5 = null;
                decimal? P_TOTALWEIGHT6 = null;
                decimal? P_UNCOATEDWEIGHT1 = null;
                decimal? P_UNCOATEDWEIGHT2 = null;
                decimal? P_UNCOATEDWEIGHT3 = null;
                decimal? P_UNCOATEDWEIGHT4 = null;
                decimal? P_UNCOATEDWEIGHT5 = null;
                decimal? P_UNCOATEDWEIGHT6 = null;
                decimal? P_COATWEIGHT1 = null;
                decimal? P_COATWEIGHT2 = null;
                decimal? P_COATWEIGHT3 = null;
                decimal? P_COATWEIGHT4 = null;
                decimal? P_COATWEIGHT5 = null;
                decimal? P_COATWEIGHT6 = null;
                decimal? P_STIFFNESS_W1 = null;
                decimal? P_STIFFNESS_W2 = null;
                decimal? P_STIFFNESS_W3 = null;
                decimal? P_STIFFNESS_F1 = null;
                decimal? P_STIFFNESS_F2 = null;
                decimal? P_STIFFNESS_F3 = null;
                decimal? P_STATIC_AIR1 = null;
                decimal? P_STATIC_AIR2 = null;
                decimal? P_STATIC_AIR3 = null;
                decimal? P_STATIC_AIR4 = null;
                decimal? P_STATIC_AIR5 = null;
                decimal? P_STATIC_AIR6 = null;
                DateTime? P_WEIGHTDATE = null;
                string P_WEIGHTBY = string.Empty;
                DateTime? P_STIFFNESSDATE = null;
                string P_STIFFNESSBY = string.Empty;
                DateTime? P_STATICAIRDATE = null;
                string P_STATICAIRBY = string.Empty;

                //เพิ่ม 25/11/18
                decimal? P_EXPONENT1 = null;
                decimal? P_EXPONENT2 = null;
                decimal? P_EXPONENT3 = null;
                decimal? P_DYNAMIC_AIR1 = null;
                decimal? P_DYNAMIC_AIR2 = null;
                decimal? P_DYNAMIC_AIR3 = null;
                DateTime? P_DYNAMICDATE = null;
                string P_DYNAMICBY = string.Empty;

                #endregion

                #region Data

                if (!string.IsNullOrEmpty(txtPRODUCTIONLOT.Text))
                    productionlot = txtPRODUCTIONLOT.Text;
                else
                    productionlot = string.Empty;

                if (!string.IsNullOrEmpty(txtITMCODE.Text))
                    itemCode = txtITMCODE.Text;
                else
                    itemCode = string.Empty;

                if (!string.IsNullOrEmpty(txtWarp1.Text))
                    warp1 = decimal.Parse(txtWarp1.Text);
                else
                    warp1 = null;

                if (!string.IsNullOrEmpty(txtWarp2.Text))
                    warp2 = decimal.Parse(txtWarp2.Text);
                else
                    warp2 = null;

                if (!string.IsNullOrEmpty(txtWarp3.Text))
                    warp3 = decimal.Parse(txtWarp3.Text);
                else
                    warp3 = null;

                if (!string.IsNullOrEmpty(txtWeft1.Text))
                    weft1 = decimal.Parse(txtWeft1.Text);
                else
                    weft1 = null;

                if (!string.IsNullOrEmpty(txtWeft2.Text))
                    weft2 = decimal.Parse(txtWeft2.Text);
                else
                    weft2 = null;

                if (!string.IsNullOrEmpty(txtWeft3.Text))
                    weft3 = decimal.Parse(txtWeft3.Text);
                else
                    weft3 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_STIFFNESS_W1 = warp1;
                P_STIFFNESS_W2 = warp2;
                P_STIFFNESS_W3 = warp3;
                P_STIFFNESS_F1 = weft1;
                P_STIFFNESS_F2 = weft2;
                P_STIFFNESS_F3 = weft3;

                P_STIFFNESSDATE = DateTime.Now;
                P_STIFFNESSBY = opera;


                string results = LabDataPDFDataService.Instance.LAB_SAVEPLCDATA(P_ITMCODE, P_PRODUCTIONLOT,
                                 P_TOTALWEIGHT1, P_TOTALWEIGHT2, P_TOTALWEIGHT3, P_TOTALWEIGHT4, P_TOTALWEIGHT5, P_TOTALWEIGHT6,
                                 P_UNCOATEDWEIGHT1, P_UNCOATEDWEIGHT2, P_UNCOATEDWEIGHT3, P_UNCOATEDWEIGHT4, P_UNCOATEDWEIGHT5, P_UNCOATEDWEIGHT6,
                                 P_COATWEIGHT1, P_COATWEIGHT2, P_COATWEIGHT3, P_COATWEIGHT4, P_COATWEIGHT5, P_COATWEIGHT6,
                                 P_STIFFNESS_W1, P_STIFFNESS_W2, P_STIFFNESS_W3, P_STIFFNESS_F1, P_STIFFNESS_F2, P_STIFFNESS_F3,
                                 P_STATIC_AIR1, P_STATIC_AIR2, P_STATIC_AIR3, P_STATIC_AIR4, P_STATIC_AIR5, P_STATIC_AIR6,
                                 P_DYNAMIC_AIR1, P_DYNAMIC_AIR2, P_DYNAMIC_AIR3, P_EXPONENT1, P_EXPONENT2, P_EXPONENT3,
                                 P_WEIGHTDATE, P_WEIGHTBY, P_STIFFNESSDATE, P_STIFFNESSBY, P_STATICAIRDATE, P_STATICAIRBY,
                                 P_DYNAMICDATE, P_DYNAMICBY);

                if (results != null)
                {
                    if (results == "1")
                    {
                        chkLoad = true;
                    }
                    else
                    {
                        chkLoad = false;
                    }
                }
                else
                {
                    chkLoad = false;
                }

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region Lab_SaveREPLCSTIFFNESS
        private bool Lab_SaveREPLCSTIFFNESS()
        {
            bool chkLoad = true;

            try
            {
                #region default

                string P_ITMCODE = string.Empty;
                string P_PRODUCTIONLOT = string.Empty;
                
                decimal? P_STIFFNESS_W1 = null;
                decimal? P_STIFFNESS_W2 = null;
                decimal? P_STIFFNESS_W3 = null;
                decimal? P_STIFFNESS_F1 = null;
                decimal? P_STIFFNESS_F2 = null;
                decimal? P_STIFFNESS_F3 = null;
               
                DateTime? P_STIFFNESSDATE = null;
                string P_STIFFNESSBY = string.Empty;
               
                #endregion

                #region Data

                if (!string.IsNullOrEmpty(txtPRODUCTIONLOT.Text))
                    productionlot = txtPRODUCTIONLOT.Text;
                else
                    productionlot = string.Empty;

                if (!string.IsNullOrEmpty(txtITMCODE.Text))
                    itemCode = txtITMCODE.Text;
                else
                    itemCode = string.Empty;

                if (!string.IsNullOrEmpty(txtWarp1.Text))
                    warp1 = decimal.Parse(txtWarp1.Text);
                else
                    warp1 = null;

                if (!string.IsNullOrEmpty(txtWarp2.Text))
                    warp2 = decimal.Parse(txtWarp2.Text);
                else
                    warp2 = null;

                if (!string.IsNullOrEmpty(txtWarp3.Text))
                    warp3 = decimal.Parse(txtWarp3.Text);
                else
                    warp3 = null;

                if (!string.IsNullOrEmpty(txtWeft1.Text))
                    weft1 = decimal.Parse(txtWeft1.Text);
                else
                    weft1 = null;

                if (!string.IsNullOrEmpty(txtWeft2.Text))
                    weft2 = decimal.Parse(txtWeft2.Text);
                else
                    weft2 = null;

                if (!string.IsNullOrEmpty(txtWeft3.Text))
                    weft3 = decimal.Parse(txtWeft3.Text);
                else
                    weft3 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_STIFFNESS_W1 = warp1;
                P_STIFFNESS_W2 = warp2;
                P_STIFFNESS_W3 = warp3;
                P_STIFFNESS_F1 = weft1;
                P_STIFFNESS_F2 = weft2;
                P_STIFFNESS_F3 = weft3;

                P_STIFFNESSDATE = DateTime.Now;
                P_STIFFNESSBY = opera;


                string results = LabDataPDFDataService.Instance.LAB_SAVEREPLCSTIFFNESS(P_ITMCODE, P_PRODUCTIONLOT,
                                 P_STIFFNESS_W1, P_STIFFNESS_W2, P_STIFFNESS_W3, P_STIFFNESS_F1, P_STIFFNESS_F2, P_STIFFNESS_F3,
                                 P_STIFFNESSDATE, P_STIFFNESSBY);

                if (results != null)
                {
                    if (results == "1")
                    {
                        chkLoad = true;
                    }
                    else
                    {
                        chkLoad = false;
                    }
                }
                else
                {
                    chkLoad = false;
                }

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdClear.IsEnabled = enabled;
            cmdSelect.IsEnabled = enabled;
            cmdSave.IsEnabled = enabled;
        }
        #endregion

        #region SelectPLC
        private void SelectPLC()
        {
            try
            {
                if (highpressFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_HIGHPRESS();

                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    if (rbWarp.IsChecked == true && rbWeft.IsChecked == false)
                    {
                        if (string.IsNullOrEmpty(txtWarp1.Text))
                        {
                            txtWarp1.Text = txtWeight.Text;
                        }
                        else if (!string.IsNullOrEmpty(txtWarp1.Text) && string.IsNullOrEmpty(txtWarp2.Text))
                        {
                            txtWarp2.Text = txtWeight.Text;
                        }
                        else if (!string.IsNullOrEmpty(txtWarp1.Text) && !string.IsNullOrEmpty(txtWarp2.Text) && string.IsNullOrEmpty(txtWarp3.Text))
                        {
                            txtWarp3.Text = txtWeight.Text;
                        }
                    }
                    else if (rbWarp.IsChecked == false && rbWeft.IsChecked == true)
                    {
                        if (string.IsNullOrEmpty(txtWeft1.Text))
                        {
                            txtWeft1.Text = txtWeight.Text;
                        }
                        else if (!string.IsNullOrEmpty(txtWeft1.Text)
                            && string.IsNullOrEmpty(txtWeft2.Text))
                        {
                            txtWeft2.Text = txtWeight.Text;
                        }
                        else if (!string.IsNullOrEmpty(txtWeft1.Text)
                            && !string.IsNullOrEmpty(txtWeft2.Text) && string.IsNullOrEmpty(txtWeft3.Text))
                        {
                            txtWeft3.Text = txtWeight.Text;
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

        #region SelectPLC_ReTest
        private void SelectPLC_ReTest()
        {
            try
            {
                if (highpressFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_HIGHPRESS();

                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    if (rbWarp.IsChecked == true && rbWeft.IsChecked == false)
                    {
                        if (rbWarpN1.IsChecked == true)
                        {
                            txtWarp1.Text = txtWeight.Text;
                        }
                        else if (rbWarpN2.IsChecked == true)
                        {
                            txtWarp2.Text = txtWeight.Text;
                        }
                        else if (rbWarpN3.IsChecked == true)
                        {
                            txtWarp3.Text = txtWeight.Text;
                        }
                    }
                    else if (rbWarp.IsChecked == false && rbWeft.IsChecked == true)
                    {
                        if (rbWeftN1.IsChecked == true)
                        {
                            txtWeft1.Text = txtWeight.Text;
                        }
                        else if (rbWeftN2.IsChecked == true)
                        {
                            txtWeft2.Text = txtWeight.Text;
                        }
                        else if (rbWeftN3.IsChecked == true)
                        {
                            txtWeft3.Text = txtWeight.Text;
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

