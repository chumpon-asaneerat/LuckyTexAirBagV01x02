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
    /// Interaction logic for DynamicAirPermeabilityPage.xaml
    /// </summary>
    public partial class DynamicAirPermeabilityPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DynamicAirPermeabilityPage()
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

        int? dynamicFlage = 0;

        string itemCode = string.Empty;
        string productionlot = string.Empty;

        decimal? dynamic_air1 = null;
        decimal? dynamic_air2 = null;
        decimal? dynamic_air3 = null;
        decimal? exponent1 = null;
        decimal? exponent2 = null;
        decimal? exponent3 = null;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (opera != "")
                txtOperator.Text = opera;

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.ScouringLabMachineConfig;

            ClearControl();

            if (ScouringLabModbusManager.Instance.Reset_DYNAMIC() == false)
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

            if (e.Value.DYNAMICFLAG == 1)
            {
                if (e.Value.DYNAMIC != null)
                    txtDynamic_Air.Text = MathEx.Round(e.Value.DYNAMIC.Value, 2).ToString("#,##0.##");
                else
                    txtDynamic_Air.Text = "0.00";

                if (e.Value.EXPONENT != null)
                    txtExponent.Text = MathEx.Round(e.Value.EXPONENT.Value, 2).ToString("#,##0.##");
                else
                    txtExponent.Text = "0.00";

                dynamicFlage = 1;
            }
            else 
            {
                //txtDynamic_Air.Text = "0.00";
                dynamicFlage = 0;
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

        #region cmdReset_Click
        private void cmdReset_Click(object sender, RoutedEventArgs e)
        {
            if (ScouringLabModbusManager.Instance.Reset_DYNAMIC() == false)
            {
                "Can't Connect PLC Please check config or PLC".ShowMessageBox();
            }
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
                    if (!string.IsNullOrEmpty(txtDynamic_Air1.Text) || !string.IsNullOrEmpty(txtDynamic_Air2.Text) || !string.IsNullOrEmpty(txtDynamic_Air3.Text)
                        || !string.IsNullOrEmpty(txtExponent1.Text) || !string.IsNullOrEmpty(txtExponent2.Text) || !string.IsNullOrEmpty(txtExponent3.Text))
                    {
                        if (Lab_SaveREPLCDYNAMICAIR() == true)
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
                    if (string.IsNullOrEmpty(txtDynamic_Air1.Text) && string.IsNullOrEmpty(txtDynamic_Air2.Text) && string.IsNullOrEmpty(txtDynamic_Air3.Text)
                        && string.IsNullOrEmpty(txtExponent1.Text) && string.IsNullOrEmpty(txtExponent2.Text) && string.IsNullOrEmpty(txtExponent3.Text))
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

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region NumberValidationTextBox

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            string onlyNumeric = @"^([0-9]+(.[0-9]+)?)$";
            Regex regex = new Regex(onlyNumeric);
            e.Handled = !regex.IsMatch(e.Text);
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
                    txtDynamic_Air.Focus();
                    txtDynamic_Air.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtDynamic_Air_KeyDown
        private void txtDynamic_Air_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtExponent.Focus();
                txtExponent.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtDynamic_Air1_KeyDown
        private void txtDynamic_Air1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDynamic_Air2.Focus();
                txtDynamic_Air2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtDynamic_Air2_KeyDown
        private void txtDynamic_Air2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDynamic_Air3.Focus();
                txtDynamic_Air3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtDynamic_Air3_KeyDown
        private void txtDynamic_Air3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtExponent1.Focus();
                txtExponent1.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtExponent_KeyDown
        private void txtExponent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSelect.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region txtExponent1_KeyDown
        private void txtExponent1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtExponent2.Focus();
                txtExponent2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtExponent2_KeyDown
        private void txtExponent2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtExponent3.Focus();
                txtExponent3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtExponent3_KeyDown
        private void txtExponent3_KeyDown(object sender, KeyEventArgs e)
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
                    productionlot = txtPRODUCTIONLOT.Text.Substring(0, txtPRODUCTIONLOT.Text.Length - 1)+"1";
                }
                else
                {
                    productionlot = txtPRODUCTIONLOT.Text.Substring(0, txtPRODUCTIONLOT.Text.Length - 1) + "R";
                }

                txtPRODUCTIONLOT.Text = productionlot;
            }
            else
                productionlot = string.Empty;
        }
        #endregion

        #region txtDynamic_Air1_LostFocus
        private void txtDynamic_Air1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDynamic_Air1.Text))
                dynamic_air1 = decimal.Parse(txtDynamic_Air1.Text);
            else
                dynamic_air1 = null;
        }
        #endregion

        #region txtDynamic_Air2_LostFocus
        private void txtDynamic_Air2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDynamic_Air2.Text))
                dynamic_air2 = decimal.Parse(txtDynamic_Air2.Text);
            else
                dynamic_air2 = null;
        }
        #endregion

        #region txtDynamic_Air3_LostFocus
        private void txtDynamic_Air3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDynamic_Air3.Text))
                dynamic_air3 = decimal.Parse(txtDynamic_Air3.Text);
            else
                dynamic_air3 = null;
        }
        #endregion

        #region txtExponent1_LostFocus
        private void txtExponent1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExponent1.Text))
                exponent1 = decimal.Parse(txtExponent1.Text);
            else
                exponent1 = null;
        }
        #endregion

        #region txtExponent2_LostFocus
        private void txtExponent2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExponent2.Text))
                exponent2 = decimal.Parse(txtExponent2.Text);
            else
                exponent2 = null;
        }
        #endregion

        #region txtExponent3_LostFocus
        private void txtExponent3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExponent3.Text))
                exponent3 = decimal.Parse(txtExponent3.Text);
            else
                exponent3 = null;
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
            rbDynamic_AirN1.Visibility = Visibility.Visible;
            rbDynamic_AirN2.Visibility = Visibility.Visible;
            rbDynamic_AirN3.Visibility = Visibility.Visible;

            rbExponentN1.Visibility = Visibility.Visible;
            rbExponentN2.Visibility = Visibility.Visible;
            rbExponentN3.Visibility = Visibility.Visible;

            rbDynamic_AirN1.IsChecked = true;
            rbExponentN1.IsChecked = true;
        }

        private void chkRetest_Unchecked(object sender, RoutedEventArgs e)
        {
            rbDynamic_AirN1.Visibility = Visibility.Collapsed;
            rbDynamic_AirN2.Visibility = Visibility.Collapsed;
            rbDynamic_AirN3.Visibility = Visibility.Collapsed;

            rbExponentN1.Visibility = Visibility.Collapsed;
            rbExponentN2.Visibility = Visibility.Collapsed;
            rbExponentN3.Visibility = Visibility.Collapsed;
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
            dynamic_air1 = null;
            dynamic_air2 = null;
            dynamic_air3 = null;
            exponent1 = null;
            exponent2 = null;
            exponent3 = null;

            txtITMCODE.Text = string.Empty;
            txtPRODUCTIONLOT.Text = string.Empty;

            txtDynamic_Air.Text = string.Empty;
            txtExponent.Text = string.Empty;

            txtDynamic_Air1.Text = string.Empty;
            txtDynamic_Air2.Text = string.Empty;
            txtDynamic_Air3.Text = string.Empty;

            txtExponent1.Text = string.Empty;
            txtExponent2.Text = string.Empty;
            txtExponent3.Text = string.Empty;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = false;

            rbDynamic_AirN1.Visibility = Visibility.Collapsed;
            rbDynamic_AirN2.Visibility = Visibility.Collapsed;
            rbDynamic_AirN3.Visibility = Visibility.Collapsed;

            rbExponentN1.Visibility = Visibility.Collapsed;
            rbExponentN2.Visibility = Visibility.Collapsed;
            rbExponentN3.Visibility = Visibility.Collapsed;

            rbDynamic_AirN1.IsChecked = true;
            rbExponentN1.IsChecked = true;

            EnabledCon(false);

            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region Clear Control

        private void ClearControlReTest()
        {
            dynamic_air1 = null;
            dynamic_air2 = null;
            dynamic_air3 = null;
            exponent1 = null;
            exponent2 = null;
            exponent3 = null;

            txtDynamic_Air.Text = string.Empty;
            txtExponent.Text = string.Empty;

            txtDynamic_Air1.Text = string.Empty;
            txtDynamic_Air2.Text = string.Empty;
            txtDynamic_Air3.Text = string.Empty;

            txtExponent1.Text = string.Empty;
            txtExponent2.Text = string.Empty;
            txtExponent3.Text = string.Empty;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = true;

            rbDynamic_AirN1.IsChecked = true;
            rbExponentN1.IsChecked = true;

            EnabledCon(false);

            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region EnabledCon
        private void EnabledCon(bool chkManual)
        {
            txtDynamic_Air.IsEnabled = chkManual;
            txtExponent.IsEnabled = chkManual;

            txtDynamic_Air1.IsEnabled = chkManual;
            txtDynamic_Air2.IsEnabled = chkManual;
            txtDynamic_Air3.IsEnabled = chkManual;
            txtExponent1.IsEnabled = chkManual;
            txtExponent2.IsEnabled = chkManual;
            txtExponent3.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    ScouringLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
                    ScouringLabModbusManager.Instance.Start();

                    //if(ScouringLabModbusManager.Instance.Reset_DYNAMIC() == false)
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

                txtDynamic_Air.Text = "0.00";
                txtExponent.Text = "0.00";

                dynamicFlage = 0;
            }
        }
        #endregion

        #region CheckDataNull
        private bool CheckDataNull()
        {
            bool chkData = false;

            try
            {

                if (!string.IsNullOrEmpty(txtExponent3.Text) && !string.IsNullOrEmpty(txtExponent2.Text) && !string.IsNullOrEmpty(txtExponent1.Text) && !string.IsNullOrEmpty(txtDynamic_Air3.Text)
                        && !string.IsNullOrEmpty(txtDynamic_Air2.Text) && !string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtExponent3.Text) && !string.IsNullOrEmpty(txtExponent2.Text) && !string.IsNullOrEmpty(txtExponent1.Text) && !string.IsNullOrEmpty(txtDynamic_Air3.Text)
                        && !string.IsNullOrEmpty(txtDynamic_Air2.Text) && !string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtExponent3.Text) && string.IsNullOrEmpty(txtExponent2.Text) && !string.IsNullOrEmpty(txtExponent1.Text) && !string.IsNullOrEmpty(txtDynamic_Air3.Text)
                   && !string.IsNullOrEmpty(txtDynamic_Air2.Text) && !string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtExponent3.Text) && string.IsNullOrEmpty(txtExponent2.Text) && string.IsNullOrEmpty(txtExponent1.Text) && !string.IsNullOrEmpty(txtDynamic_Air3.Text)
                       && !string.IsNullOrEmpty(txtDynamic_Air2.Text) && !string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtExponent3.Text) && string.IsNullOrEmpty(txtExponent2.Text) && string.IsNullOrEmpty(txtExponent1.Text) && string.IsNullOrEmpty(txtDynamic_Air3.Text)
                       && !string.IsNullOrEmpty(txtDynamic_Air2.Text) && !string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtExponent3.Text) && string.IsNullOrEmpty(txtExponent2.Text) && string.IsNullOrEmpty(txtExponent1.Text) && string.IsNullOrEmpty(txtDynamic_Air3.Text)
                     && string.IsNullOrEmpty(txtDynamic_Air2.Text) && !string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtExponent3.Text) && string.IsNullOrEmpty(txtExponent2.Text) && string.IsNullOrEmpty(txtExponent1.Text) && string.IsNullOrEmpty(txtDynamic_Air3.Text)
                     && string.IsNullOrEmpty(txtDynamic_Air2.Text) && string.IsNullOrEmpty(txtDynamic_Air1.Text))
                {
                    chkData = false;
                }
                else
                {
                    chkData = false;
                }
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

                if (!string.IsNullOrEmpty(txtDynamic_Air1.Text))
                    dynamic_air1 = decimal.Parse(txtDynamic_Air1.Text);
                else
                    dynamic_air1 = null;

                if (!string.IsNullOrEmpty(txtDynamic_Air2.Text))
                    dynamic_air2 = decimal.Parse(txtDynamic_Air2.Text);
                else
                    dynamic_air2 = null;

                if (!string.IsNullOrEmpty(txtDynamic_Air3.Text))
                    dynamic_air3 = decimal.Parse(txtDynamic_Air3.Text);
                else
                    dynamic_air3 = null;

                if (!string.IsNullOrEmpty(txtExponent1.Text))
                    exponent1 = decimal.Parse(txtExponent1.Text);
                else
                    exponent1 = null;

                if (!string.IsNullOrEmpty(txtExponent2.Text))
                    exponent2 = decimal.Parse(txtExponent2.Text);
                else
                    exponent2 = null;

                if (!string.IsNullOrEmpty(txtExponent3.Text))
                    exponent3 = decimal.Parse(txtExponent3.Text);
                else
                    exponent3 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_DYNAMIC_AIR1 = dynamic_air1;
                P_DYNAMIC_AIR2 = dynamic_air2;
                P_DYNAMIC_AIR3 = dynamic_air3;
                P_EXPONENT1 = exponent1;
                P_EXPONENT2 = exponent2;
                P_EXPONENT3 = exponent3;

                P_DYNAMICDATE = DateTime.Now;
                P_DYNAMICBY = opera;


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

        #region Lab_SaveREPLCDYNAMICAIR
        private bool Lab_SaveREPLCDYNAMICAIR()
        {
            bool chkLoad = true;

            try
            {
                #region default

                string P_ITMCODE = string.Empty;
                string P_PRODUCTIONLOT = string.Empty;

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

                if (!string.IsNullOrEmpty(txtDynamic_Air1.Text))
                    dynamic_air1 = decimal.Parse(txtDynamic_Air1.Text);
                else
                    dynamic_air1 = null;

                if (!string.IsNullOrEmpty(txtDynamic_Air2.Text))
                    dynamic_air2 = decimal.Parse(txtDynamic_Air2.Text);
                else
                    dynamic_air2 = null;

                if (!string.IsNullOrEmpty(txtDynamic_Air3.Text))
                    dynamic_air3 = decimal.Parse(txtDynamic_Air3.Text);
                else
                    dynamic_air3 = null;

                if (!string.IsNullOrEmpty(txtExponent1.Text))
                    exponent1 = decimal.Parse(txtExponent1.Text);
                else
                    exponent1 = null;

                if (!string.IsNullOrEmpty(txtExponent2.Text))
                    exponent2 = decimal.Parse(txtExponent2.Text);
                else
                    exponent2 = null;

                if (!string.IsNullOrEmpty(txtExponent3.Text))
                    exponent3 = decimal.Parse(txtExponent3.Text);
                else
                    exponent3 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_DYNAMIC_AIR1 = dynamic_air1;
                P_DYNAMIC_AIR2 = dynamic_air2;
                P_DYNAMIC_AIR3 = dynamic_air3;
                P_EXPONENT1 = exponent1;
                P_EXPONENT2 = exponent2;
                P_EXPONENT3 = exponent3;

                P_DYNAMICDATE = DateTime.Now;
                P_DYNAMICBY = opera;

                string results = LabDataPDFDataService.Instance.LAB_SAVEREPLCDYNAMICAIR(P_ITMCODE, P_PRODUCTIONLOT,
                                 P_DYNAMIC_AIR1, P_DYNAMIC_AIR2, P_DYNAMIC_AIR3, P_EXPONENT1, P_EXPONENT2, P_EXPONENT3,
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
                if (dynamicFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_DYNAMIC();

                if (!string.IsNullOrEmpty(txtDynamic_Air.Text))
                {
                    if (string.IsNullOrEmpty(txtDynamic_Air1.Text))
                    {
                        txtDynamic_Air1.Text = txtDynamic_Air.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtDynamic_Air1.Text) && string.IsNullOrEmpty(txtDynamic_Air2.Text))
                    {
                        txtDynamic_Air2.Text = txtDynamic_Air.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtDynamic_Air1.Text) && !string.IsNullOrEmpty(txtDynamic_Air2.Text) && string.IsNullOrEmpty(txtDynamic_Air3.Text))
                    {
                        txtDynamic_Air3.Text = txtDynamic_Air.Text;
                    }
                    

                    if (string.IsNullOrEmpty(txtExponent1.Text))
                    {
                        txtExponent1.Text = txtExponent.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtExponent1.Text)
                        && string.IsNullOrEmpty(txtExponent2.Text))
                    {
                        txtExponent2.Text = txtExponent.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtExponent1.Text)
                        && !string.IsNullOrEmpty(txtExponent2.Text) && string.IsNullOrEmpty(txtExponent3.Text))
                    {
                        txtExponent3.Text = txtExponent.Text;
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
                if (dynamicFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_DYNAMIC();

                if (!string.IsNullOrEmpty(txtDynamic_Air.Text))
                {
                    if (rbDynamic_AirN1.IsChecked == true)
                    {
                        txtDynamic_Air1.Text = txtDynamic_Air.Text;
                    }
                    else if (rbDynamic_AirN2.IsChecked == true)
                    {
                        txtDynamic_Air2.Text = txtDynamic_Air.Text;
                    }
                    else if (rbDynamic_AirN3.IsChecked == true)
                    {
                        txtDynamic_Air3.Text = txtDynamic_Air.Text;
                    }


                    if (rbExponentN1.IsChecked == true)
                    {
                        txtExponent1.Text = txtExponent.Text;
                    }
                    else if (rbExponentN2.IsChecked == true)
                    {
                        txtExponent2.Text = txtExponent.Text;
                    }
                    else if (rbExponentN3.IsChecked == true)
                    {
                        txtExponent3.Text = txtExponent.Text;
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

