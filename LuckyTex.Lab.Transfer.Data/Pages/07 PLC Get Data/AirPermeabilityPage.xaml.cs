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
    /// Interaction logic for AirPermeabilityPage.xaml
    /// </summary>
    public partial class AirPermeabilityPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AirPermeabilityPage()
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

        int? airFlage = 0;

        string itemCode = string.Empty;
        string productionlot = string.Empty;

        decimal? static_air1 = null;
        decimal? static_air2 = null;
        decimal? static_air3 = null;
        decimal? static_air4 = null;
        decimal? static_air5 = null;
        decimal? static_air6 = null;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (opera != "")
                txtOperator.Text = opera;

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.ScouringLabMachineConfig;

            ClearControl();

            if (ScouringLabModbusManager.Instance.Reset_AIR() == false)
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

            if (e.Value.AIRFLAG == 1)
            {
                //txtStatic_Air.Text = e.Value.AIR.Value.ToString("n2").Replace(",", string.Empty);

                if (e.Value.AIR != null)
                    txtStatic_Air.Text = MathEx.Round(e.Value.AIR.Value, 2).ToString("#,##0.##");
                else
                    txtStatic_Air.Text = "0.00";

                airFlage = 1;
            }
            else 
            {
                //txtStatic_Air.Text = "0.00";
                airFlage = 0;
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
            if (ScouringLabModbusManager.Instance.Reset_AIR() == false)
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
                    if (!string.IsNullOrEmpty(txtStatic_Air1.Text) || !string.IsNullOrEmpty(txtStatic_Air2.Text) || !string.IsNullOrEmpty(txtStatic_Air3.Text)
                        || !string.IsNullOrEmpty(txtStatic_Air4.Text) || !string.IsNullOrEmpty(txtStatic_Air5.Text) || !string.IsNullOrEmpty(txtStatic_Air6.Text))
                    {
                        if (Lab_SaveREPLCSTATICAIR() == true)
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
                    if (string.IsNullOrEmpty(txtStatic_Air1.Text) && string.IsNullOrEmpty(txtStatic_Air2.Text) && string.IsNullOrEmpty(txtStatic_Air3.Text)
                        && string.IsNullOrEmpty(txtStatic_Air4.Text) && string.IsNullOrEmpty(txtStatic_Air5.Text) && string.IsNullOrEmpty(txtStatic_Air6.Text))
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
                    txtStatic_Air.Focus();
                    txtStatic_Air.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air_KeyDown
        private void txtStatic_Air_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSelect.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air1_KeyDown
        private void txtStatic_Air1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_Air2.Focus();
                txtStatic_Air2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air2_KeyDown
        private void txtStatic_Air2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_Air3.Focus();
                txtStatic_Air3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air3_KeyDown
        private void txtStatic_Air3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_Air4.Focus();
                txtStatic_Air4.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air4_KeyDown
        private void txtStatic_Air4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_Air5.Focus();
                txtStatic_Air5.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air5_KeyDown
        private void txtStatic_Air5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_Air6.Focus();
                txtStatic_Air6.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_Air6_KeyDown
        private void txtStatic_Air6_KeyDown(object sender, KeyEventArgs e)
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

        #region txtStatic_Air1_LostFocus
        private void txtStatic_Air1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_Air1.Text))
                static_air1 = decimal.Parse(txtStatic_Air1.Text);
            else
                static_air1 = null;
        }
        #endregion

        #region txtStatic_Air2_LostFocus
        private void txtStatic_Air2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_Air2.Text))
                static_air2 = decimal.Parse(txtStatic_Air2.Text);
            else
                static_air2 = null;
        }
        #endregion

        #region txtStatic_Air3_LostFocus
        private void txtStatic_Air3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_Air3.Text))
                static_air3 = decimal.Parse(txtStatic_Air3.Text);
            else
                static_air3 = null;
        }
        #endregion

        #region txtStatic_Air4_LostFocus
        private void txtStatic_Air4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_Air4.Text))
                static_air4 = decimal.Parse(txtStatic_Air4.Text);
            else
                static_air4 = null;
        }
        #endregion

        #region txtStatic_Air5_LostFocus
        private void txtStatic_Air5_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_Air5.Text))
                static_air5 = decimal.Parse(txtStatic_Air5.Text);
            else
                static_air5 = null;
        }
        #endregion

        #region txtStatic_Air6_LostFocus
        private void txtStatic_Air6_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_Air6.Text))
                static_air6 = decimal.Parse(txtStatic_Air6.Text);
            else
                static_air6 = null;
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
            rbStatic_AirN1.Visibility = Visibility.Visible;
            rbStatic_AirN2.Visibility = Visibility.Visible;
            rbStatic_AirN3.Visibility = Visibility.Visible;
            rbStatic_AirN4.Visibility = Visibility.Visible;
            rbStatic_AirN5.Visibility = Visibility.Visible;
            rbStatic_AirN6.Visibility = Visibility.Visible;

            rbStatic_AirN1.IsChecked = true;
        }

        private void chkRetest_Unchecked(object sender, RoutedEventArgs e)
        {
            rbStatic_AirN1.Visibility = Visibility.Collapsed;
            rbStatic_AirN2.Visibility = Visibility.Collapsed;
            rbStatic_AirN3.Visibility = Visibility.Collapsed;
            rbStatic_AirN4.Visibility = Visibility.Collapsed;
            rbStatic_AirN5.Visibility = Visibility.Collapsed;
            rbStatic_AirN6.Visibility = Visibility.Collapsed;
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
            static_air1 = null;
            static_air2 = null;
            static_air3 = null;
            static_air4 = null;
            static_air5 = null;
            static_air6 = null;

            txtITMCODE.Text = string.Empty;
            txtPRODUCTIONLOT.Text = string.Empty;

            txtStatic_Air.Text = string.Empty;
            txtStatic_Air1.Text = string.Empty;
            txtStatic_Air2.Text = string.Empty;
            txtStatic_Air3.Text = string.Empty;
            txtStatic_Air4.Text = string.Empty;
            txtStatic_Air5.Text = string.Empty;
            txtStatic_Air6.Text = string.Empty;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = false;

            rbStatic_AirN1.Visibility = Visibility.Collapsed;
            rbStatic_AirN2.Visibility = Visibility.Collapsed;
            rbStatic_AirN3.Visibility = Visibility.Collapsed;
            rbStatic_AirN4.Visibility = Visibility.Collapsed;
            rbStatic_AirN5.Visibility = Visibility.Collapsed;
            rbStatic_AirN6.Visibility = Visibility.Collapsed;

            rbStatic_AirN1.IsChecked = true;

            EnabledCon(false);
 
            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region ClearControlReTest

        private void ClearControlReTest()
        {
            static_air1 = null;
            static_air2 = null;
            static_air3 = null;
            static_air4 = null;
            static_air5 = null;
            static_air6 = null;

            txtStatic_Air.Text = string.Empty;
            txtStatic_Air1.Text = string.Empty;
            txtStatic_Air2.Text = string.Empty;
            txtStatic_Air3.Text = string.Empty;
            txtStatic_Air4.Text = string.Empty;
            txtStatic_Air5.Text = string.Empty;
            txtStatic_Air6.Text = string.Empty;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = true;

            rbStatic_AirN1.IsChecked = true;

            EnabledCon(false);

            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region EnabledCon
        private void EnabledCon(bool chkManual)
        {
            txtStatic_Air.IsEnabled = chkManual;
            txtStatic_Air1.IsEnabled = chkManual;
            txtStatic_Air2.IsEnabled = chkManual;
            txtStatic_Air3.IsEnabled = chkManual;
            txtStatic_Air4.IsEnabled = chkManual;
            txtStatic_Air5.IsEnabled = chkManual;
            txtStatic_Air6.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    ScouringLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
                    ScouringLabModbusManager.Instance.Start();

                    //if(ScouringLabModbusManager.Instance.Reset_AIR() == false)
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

                txtStatic_Air.Text = "0.00";
                airFlage = 0;
            }
        }
        #endregion

        #region chkWeight1_6
        private bool chkWeight1_6()
        {
            bool chkWeight = true;

            try
            {
                if (!string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtStatic_Air2.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtStatic_Air3.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtStatic_Air4.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtStatic_Air5.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtStatic_Air6.Text))
                {
                    chkWeight = false;
                }
            }
            catch
            {
                chkWeight = false;
            }

            return chkWeight;
        }

        #endregion

        #region CheckDataNull
        private bool CheckDataNull()
        {
            bool chkData = false;

            try
            {

                if (!string.IsNullOrEmpty(txtStatic_Air6.Text) && !string.IsNullOrEmpty(txtStatic_Air5.Text) && !string.IsNullOrEmpty(txtStatic_Air4.Text) && !string.IsNullOrEmpty(txtStatic_Air3.Text)
                        && !string.IsNullOrEmpty(txtStatic_Air2.Text) && !string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtStatic_Air6.Text) && !string.IsNullOrEmpty(txtStatic_Air5.Text) && !string.IsNullOrEmpty(txtStatic_Air4.Text) && !string.IsNullOrEmpty(txtStatic_Air3.Text)
                        && !string.IsNullOrEmpty(txtStatic_Air2.Text) && !string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtStatic_Air6.Text) && string.IsNullOrEmpty(txtStatic_Air5.Text) && !string.IsNullOrEmpty(txtStatic_Air4.Text) && !string.IsNullOrEmpty(txtStatic_Air3.Text)
                   && !string.IsNullOrEmpty(txtStatic_Air2.Text) && !string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtStatic_Air6.Text) && string.IsNullOrEmpty(txtStatic_Air5.Text) && string.IsNullOrEmpty(txtStatic_Air4.Text) && !string.IsNullOrEmpty(txtStatic_Air3.Text)
                       && !string.IsNullOrEmpty(txtStatic_Air2.Text) && !string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtStatic_Air6.Text) && string.IsNullOrEmpty(txtStatic_Air5.Text) && string.IsNullOrEmpty(txtStatic_Air4.Text) && string.IsNullOrEmpty(txtStatic_Air3.Text)
                       && !string.IsNullOrEmpty(txtStatic_Air2.Text) && !string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtStatic_Air6.Text) && string.IsNullOrEmpty(txtStatic_Air5.Text) && string.IsNullOrEmpty(txtStatic_Air4.Text) && string.IsNullOrEmpty(txtStatic_Air3.Text)
                     && string.IsNullOrEmpty(txtStatic_Air2.Text) && !string.IsNullOrEmpty(txtStatic_Air1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtStatic_Air6.Text) && string.IsNullOrEmpty(txtStatic_Air5.Text) && string.IsNullOrEmpty(txtStatic_Air4.Text) && string.IsNullOrEmpty(txtStatic_Air3.Text)
                     && string.IsNullOrEmpty(txtStatic_Air2.Text) && string.IsNullOrEmpty(txtStatic_Air1.Text))
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

                if (!string.IsNullOrEmpty(txtStatic_Air1.Text))
                    static_air1 = decimal.Parse(txtStatic_Air1.Text);
                else
                    static_air1 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air2.Text))
                    static_air2 = decimal.Parse(txtStatic_Air2.Text);
                else
                    static_air2 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air3.Text))
                    static_air3 = decimal.Parse(txtStatic_Air3.Text);
                else
                    static_air3 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air4.Text))
                    static_air4 = decimal.Parse(txtStatic_Air4.Text);
                else
                    static_air4 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air5.Text))
                    static_air5 = decimal.Parse(txtStatic_Air5.Text);
                else
                    static_air5 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air6.Text))
                    static_air6 = decimal.Parse(txtStatic_Air6.Text);
                else
                    static_air6 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_STATIC_AIR1 = static_air1;
                P_STATIC_AIR2 = static_air2;
                P_STATIC_AIR3 = static_air3;
                P_STATIC_AIR4 = static_air4;
                P_STATIC_AIR5 = static_air5;
                P_STATIC_AIR6 = static_air6;

                P_STATICAIRDATE = DateTime.Now;
                P_STATICAIRBY = opera;


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

        #region Lab_SaveREPLCSTATICAIR

        private bool Lab_SaveREPLCSTATICAIR()
        {
            bool chkLoad = true;

            try
            {
                #region default

                string P_ITMCODE = string.Empty;
                string P_PRODUCTIONLOT = string.Empty;
               
                decimal? P_STATIC_AIR1 = null;
                decimal? P_STATIC_AIR2 = null;
                decimal? P_STATIC_AIR3 = null;
                decimal? P_STATIC_AIR4 = null;
                decimal? P_STATIC_AIR5 = null;
                decimal? P_STATIC_AIR6 = null;

                DateTime? P_STATICAIRDATE = null;
                string P_STATICAIRBY = string.Empty;
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

                if (!string.IsNullOrEmpty(txtStatic_Air1.Text))
                    static_air1 = decimal.Parse(txtStatic_Air1.Text);
                else
                    static_air1 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air2.Text))
                    static_air2 = decimal.Parse(txtStatic_Air2.Text);
                else
                    static_air2 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air3.Text))
                    static_air3 = decimal.Parse(txtStatic_Air3.Text);
                else
                    static_air3 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air4.Text))
                    static_air4 = decimal.Parse(txtStatic_Air4.Text);
                else
                    static_air4 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air5.Text))
                    static_air5 = decimal.Parse(txtStatic_Air5.Text);
                else
                    static_air5 = null;

                if (!string.IsNullOrEmpty(txtStatic_Air6.Text))
                    static_air6 = decimal.Parse(txtStatic_Air6.Text);
                else
                    static_air6 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_STATIC_AIR1 = static_air1;
                P_STATIC_AIR2 = static_air2;
                P_STATIC_AIR3 = static_air3;
                P_STATIC_AIR4 = static_air4;
                P_STATIC_AIR5 = static_air5;
                P_STATIC_AIR6 = static_air6;

                P_STATICAIRDATE = DateTime.Now;
                P_STATICAIRBY = opera;

                string results = LabDataPDFDataService.Instance.LAB_SAVEREPLCSTATICAIR(P_ITMCODE, P_PRODUCTIONLOT,
                                 P_STATIC_AIR1, P_STATIC_AIR2, P_STATIC_AIR3, P_STATIC_AIR4, P_STATIC_AIR5, P_STATIC_AIR6, P_STATICAIRDATE, P_STATICAIRBY);

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
                if (airFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_AIR();

                if (!string.IsNullOrEmpty(txtStatic_Air.Text))
                {
                    if (string.IsNullOrEmpty(txtStatic_Air1.Text))
                    {
                        txtStatic_Air1.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_Air1.Text) && string.IsNullOrEmpty(txtStatic_Air2.Text))
                    {
                        txtStatic_Air2.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_Air1.Text) && !string.IsNullOrEmpty(txtStatic_Air2.Text) && string.IsNullOrEmpty(txtStatic_Air3.Text))
                    {
                        txtStatic_Air3.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_Air1.Text) && !string.IsNullOrEmpty(txtStatic_Air2.Text) &&
                        !string.IsNullOrEmpty(txtStatic_Air3.Text) && string.IsNullOrEmpty(txtStatic_Air4.Text))
                    {
                        txtStatic_Air4.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_Air1.Text) && !string.IsNullOrEmpty(txtStatic_Air2.Text) &&
                        !string.IsNullOrEmpty(txtStatic_Air3.Text) && !string.IsNullOrEmpty(txtStatic_Air4.Text)
                        && string.IsNullOrEmpty(txtStatic_Air5.Text))
                    {
                        txtStatic_Air5.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_Air1.Text) && !string.IsNullOrEmpty(txtStatic_Air2.Text) &&
                        !string.IsNullOrEmpty(txtStatic_Air3.Text) && !string.IsNullOrEmpty(txtStatic_Air4.Text)
                        && !string.IsNullOrEmpty(txtStatic_Air5.Text) && string.IsNullOrEmpty(txtStatic_Air6.Text))
                    {
                        txtStatic_Air6.Text = CalPLC(txtStatic_Air.Text);
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
                if (airFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_AIR();

                if (!string.IsNullOrEmpty(txtStatic_Air.Text))
                {
                    if (rbStatic_AirN1.IsChecked == true)
                    {
                        txtStatic_Air1.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (rbStatic_AirN2.IsChecked == true)
                    {
                        txtStatic_Air2.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (rbStatic_AirN3.IsChecked == true)
                    {
                        txtStatic_Air3.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (rbStatic_AirN4.IsChecked == true)
                    {
                        txtStatic_Air4.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (rbStatic_AirN5.IsChecked == true)
                    {
                        txtStatic_Air5.Text = CalPLC(txtStatic_Air.Text);
                    }
                    else if (rbStatic_AirN6.IsChecked == true)
                    {
                        txtStatic_Air6.Text = CalPLC(txtStatic_Air.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region CalPLC
        private string CalPLC(string static_air)
        {
            string strCal = string.Empty;

            decimal? cal = null;
            decimal? plc_air = null;
            decimal? circle = null;

            try
            {

                if (!string.IsNullOrEmpty(txtDefCircle.Text))
                {
                    circle = decimal.Parse(txtDefCircle.Text);
                }
                if (!string.IsNullOrEmpty(static_air))
                {
                    plc_air = decimal.Parse(static_air);
                }

                if (circle != null && plc_air != null)
                {
                    if (circle != 0)
                        cal = MathEx.Round((plc_air / circle).Value, 3);
                    else
                        cal = 0;
                }

                if (cal != null)
                    strCal = cal.Value.ToString("#,##0.###");

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }

            return strCal;
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

