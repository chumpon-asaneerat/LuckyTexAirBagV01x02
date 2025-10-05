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
    /// Interaction logic for StaticAirPage.xaml
    /// </summary>
    public partial class StaticAirPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public StaticAirPage()
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

        int? staticFlage = 0;

        string itemCode = string.Empty;
        string productionlot = string.Empty;

        decimal? static_air1 = null;
        decimal? static_air2 = null;
        decimal? static_air3 = null;

        decimal? static_airN1 = null;
        decimal? static_airN2 = null;
        decimal? static_airN3 = null;
        decimal? static_airN1_2 = null;
        decimal? static_airN2_2 = null;
        decimal? static_airN3_2 = null;
        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (opera != "")
                txtOperator.Text = opera;

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.AirStaticLabConfig;

            ClearControl();

            if (AirStaticLabModbusManager.Instance.Reset_AIR() == false)
            {
                "Can't Connect PLC Please check config or PLC".ShowMessageBox();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            AirStaticLabModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<AirStaticLab>(Instance_ReadCompleted);
            AirStaticLabModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<AirStaticLab> e)
        {
            if (null == e.Value)
            {
                return;
            }

            if (e.Value.AIRFLAG == 1)
            {
                //if (e.Value.AIR != null)
                //    txtStatic_Air.Text = MathEx.Round(e.Value.AIR.Value, 3).ToString("#,##0.###");
                //else
                //    txtStatic_Air.Text = "0.000";

                // เปลี่ยนใหม่ ดึงข้อมูลมาตรงๆ ไม่มีการปัดเศษ
                if (e.Value.AIR != null)
                    txtStatic_Air.Text = e.Value.AIR.Value.ToString("#,##0.###");
                else
                    txtStatic_Air.Text = "0.000";

                staticFlage = 1;
            }
            else 
            {
                //txtStatic_Air.Text = "0.000";
                staticFlage = 0;
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
            if (AirStaticLabModbusManager.Instance.Reset_AIR() == false)
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
                    if (!string.IsNullOrEmpty(txtStatic_AirN1.Text) || !string.IsNullOrEmpty(txtStatic_AirN2.Text) || !string.IsNullOrEmpty(txtStatic_AirN3.Text)
                        || !string.IsNullOrEmpty(txtStatic_AirN1_2.Text) || !string.IsNullOrEmpty(txtStatic_AirN2_2.Text) || !string.IsNullOrEmpty(txtStatic_AirN3_2.Text)
                        || !string.IsNullOrEmpty(txtStatic_AirTotal1.Text) || !string.IsNullOrEmpty(txtStatic_AirTotal2.Text) || !string.IsNullOrEmpty(txtStatic_AirTotal3.Text))
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
                    if (string.IsNullOrEmpty(txtStatic_AirN1.Text) && string.IsNullOrEmpty(txtStatic_AirN2.Text) && string.IsNullOrEmpty(txtStatic_AirN3.Text)
                        && string.IsNullOrEmpty(txtStatic_AirN1_2.Text) && string.IsNullOrEmpty(txtStatic_AirN2_2.Text) && string.IsNullOrEmpty(txtStatic_AirN3_2.Text)
                        && string.IsNullOrEmpty(txtStatic_AirTotal1.Text) && string.IsNullOrEmpty(txtStatic_AirTotal2.Text) && string.IsNullOrEmpty(txtStatic_AirTotal3.Text))
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
                txtStatic_AirN1.Focus();
                txtStatic_AirN1.SelectAll();
                e.Handled = true;
            }
        }
        #endregion


        #region txtStatic_AirN1_KeyDown
        private void txtStatic_AirN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_AirN1_2.Focus();
                txtStatic_AirN1_2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_AirN1_2_KeyDown
        private void txtStatic_AirN1_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_AirN2.Focus();
                txtStatic_AirN2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_AirN2_KeyDown
        private void txtStatic_AirN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_AirN2_2.Focus();
                txtStatic_AirN2_2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_AirN2_2_KeyDown
        private void txtStatic_AirN2_2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_AirN3.Focus();
                txtStatic_AirN3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_AirN3_KeyDown
        private void txtStatic_AirN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStatic_AirN3_2.Focus();
                txtStatic_AirN3_2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtStatic_AirN3_2_KeyDown
        private void txtStatic_AirN3_2_KeyDown(object sender, KeyEventArgs e)
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

        #region txtStatic_AirN1_LostFocus
        private void txtStatic_AirN1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_AirN1.Text))
                static_airN1 = decimal.Parse(txtStatic_AirN1.Text);
            else
                static_airN1 = null;

            calAir1();
        }
        #endregion

        #region txtStatic_AirN1_2_LostFocus
        private void txtStatic_AirN1_2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_AirN1_2.Text))
                static_airN1_2 = decimal.Parse(txtStatic_AirN1_2.Text);
            else
                static_airN1_2 = null;

            calAir1();
        }
        #endregion

        #region calAir1
        private void calAir1()
        {
            try
            {
                if (static_airN1 != null && static_airN1_2 != null)
                {
                    static_air1 = MathEx.Round((static_airN1.Value - static_airN1_2.Value),3);
                    txtStatic_AirTotal1.Text = static_air1.Value.ToString("#,###.###");
                }
                else
                {
                    static_air1 = null;
                    txtStatic_AirTotal1.Text = "0.000";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region txtStatic_AirN2_LostFocus
        private void txtStatic_AirN2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_AirN2.Text))
                static_airN2 = decimal.Parse(txtStatic_AirN2.Text);
            else
                static_airN2 = null;

            calAir2();
        }
        #endregion

        #region txtStatic_AirN2_2_LostFocus
        private void txtStatic_AirN2_2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_AirN2_2.Text))
                static_airN2_2 = decimal.Parse(txtStatic_AirN2_2.Text);
            else
                static_airN2_2 = null;

            calAir2();
        }
        #endregion

        #region calAir2
        private void calAir2()
        {
            try
            {
                if (static_airN2 != null && static_airN2_2 != null)
                {
                    static_air2 = MathEx.Round((static_airN2.Value - static_airN2_2.Value),3);
                    txtStatic_AirTotal2.Text = static_air2.Value.ToString("#,###.###");
                }
                else
                {
                    static_air2 = null;
                    txtStatic_AirTotal2.Text = "0.000";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region txtStatic_AirN3_LostFocus
        private void txtStatic_AirN3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_AirN3.Text))
                static_airN3 = decimal.Parse(txtStatic_AirN3.Text);
            else
                static_airN3 = null;

            calAir3();
        }
        #endregion

        #region txtStatic_AirN3_2_LostFocus
        private void txtStatic_AirN3_2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStatic_AirN3_2.Text))
                static_airN3_2 = decimal.Parse(txtStatic_AirN3_2.Text);
            else
                static_airN3_2 = null;

            calAir3();
        }
        #endregion

        #region calAir3
        private void calAir3()
        {
            try
            {
                if (static_airN3 != null && static_airN3_2 != null)
                {
                    static_air3 = MathEx.Round((static_airN3.Value - static_airN3_2.Value),3);
                    txtStatic_AirTotal3.Text = static_air3.Value.ToString("#,###.###");
                }
                else
                {
                    static_air3 = null;
                    txtStatic_AirTotal3.Text = "0.000";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
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

            rbStatic_AirN1.IsChecked = true;
        }

        private void chkRetest_Unchecked(object sender, RoutedEventArgs e)
        {
            rbStatic_AirN1.Visibility = Visibility.Collapsed;
            rbStatic_AirN2.Visibility = Visibility.Collapsed;
            rbStatic_AirN3.Visibility = Visibility.Collapsed;
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

            static_airN1 = null;
            static_airN2 = null;
            static_airN3 = null;

            static_airN1_2 = null;
            static_airN2_2 = null;
            static_airN3_2 = null;

            txtITMCODE.Text = string.Empty;
            txtPRODUCTIONLOT.Text = string.Empty;

            txtStatic_Air.Text = string.Empty;

            txtStatic_AirN1.Text = string.Empty;
            txtStatic_AirN1_2.Text = string.Empty;
            txtStatic_AirTotal1.Text = string.Empty;
            txtStatic_AirN2.Text = string.Empty;
            txtStatic_AirN2_2.Text = string.Empty;
            txtStatic_AirTotal2.Text = string.Empty;
            txtStatic_AirN3.Text = string.Empty;
            txtStatic_AirN3_2.Text = string.Empty;
            txtStatic_AirTotal3.Text = string.Empty;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = false;

            rbStatic_AirN1.Visibility = Visibility.Collapsed;
            rbStatic_AirN2.Visibility = Visibility.Collapsed;
            rbStatic_AirN3.Visibility = Visibility.Collapsed;

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

            static_airN1 = null;
            static_airN2 = null;
            static_airN3 = null;

            static_airN1_2 = null;
            static_airN2_2 = null;
            static_airN3_2 = null;

            txtStatic_Air.Text = string.Empty;

            txtStatic_AirN1.Text = string.Empty;
            txtStatic_AirN1_2.Text = string.Empty;
            txtStatic_AirTotal1.Text = string.Empty;
            txtStatic_AirN2.Text = string.Empty;
            txtStatic_AirN2_2.Text = string.Empty;
            txtStatic_AirTotal2.Text = string.Empty;
            txtStatic_AirN3.Text = string.Empty;
            txtStatic_AirN3_2.Text = string.Empty;
            txtStatic_AirTotal3.Text = string.Empty;

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

            txtStatic_AirN1.IsEnabled = chkManual;
            txtStatic_AirN1_2.IsEnabled = chkManual;
            txtStatic_AirN2.IsEnabled = chkManual;
            txtStatic_AirN2_2.IsEnabled = chkManual;
            txtStatic_AirN3.IsEnabled = chkManual;
            txtStatic_AirN3_2.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    AirStaticLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<AirStaticLab>(Instance_ReadCompleted);
                    AirStaticLabModbusManager.Instance.Start();

                    //if(AirStaticLabModbusManager.Instance.Reset_STATIC_AIR() == false)
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
                AirStaticLabModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<AirStaticLab>(Instance_ReadCompleted);
                AirStaticLabModbusManager.Instance.Shutdown();

                txtStatic_Air.Text = "0.000";

                staticFlage = 0;
            }
        }
        #endregion

        #region CheckDataNull
        private bool CheckDataNull()
        {
            bool chkData = false;

            try
            {

                if (!string.IsNullOrEmpty(txtStatic_AirN3.Text) || !string.IsNullOrEmpty(txtStatic_AirN2.Text) || !string.IsNullOrEmpty(txtStatic_AirN1.Text)
                    || !string.IsNullOrEmpty(txtStatic_AirN3_2.Text) || !string.IsNullOrEmpty(txtStatic_AirN2_2.Text) || !string.IsNullOrEmpty(txtStatic_AirN1_2.Text)
                    || !string.IsNullOrEmpty(txtStatic_AirTotal3.Text) || !string.IsNullOrEmpty(txtStatic_AirTotal2.Text) || !string.IsNullOrEmpty(txtStatic_AirTotal1.Text))
                {
                    chkData = true;
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

                if (!string.IsNullOrEmpty(txtStatic_AirTotal1.Text))
                    static_air1 = decimal.Parse(txtStatic_AirTotal1.Text);
                else
                    static_air1 = null;

                if (!string.IsNullOrEmpty(txtStatic_AirTotal2.Text))
                    static_air2 = decimal.Parse(txtStatic_AirTotal2.Text);
                else
                    static_air2 = null;

                if (!string.IsNullOrEmpty(txtStatic_AirTotal3.Text))
                    static_air3 = decimal.Parse(txtStatic_AirTotal3.Text);
                else
                    static_air3 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_STATIC_AIR1 = static_air1;
                P_STATIC_AIR2 = static_air2;
                P_STATIC_AIR3 = static_air3;

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

                if (!string.IsNullOrEmpty(txtStatic_AirTotal1.Text))
                    static_air1 = decimal.Parse(txtStatic_AirTotal1.Text);
                else
                    static_air1 = null;

                if (!string.IsNullOrEmpty(txtStatic_AirTotal2.Text))
                    static_air2 = decimal.Parse(txtStatic_AirTotal2.Text);
                else
                    static_air2 = null;

                if (!string.IsNullOrEmpty(txtStatic_AirTotal3.Text))
                    static_air3 = decimal.Parse(txtStatic_AirTotal3.Text);
                else
                    static_air3 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_STATIC_AIR1 = static_air1;
                P_STATIC_AIR2 = static_air2;
                P_STATIC_AIR3 = static_air3;

                P_STATICAIRDATE = DateTime.Now;
                P_STATICAIRBY = opera;

                string results = LabDataPDFDataService.Instance.LAB_SAVEREPLCSTATICAIR(P_ITMCODE, P_PRODUCTIONLOT,
                                 P_STATIC_AIR1, P_STATIC_AIR2, P_STATIC_AIR3, P_STATIC_AIR4, P_STATIC_AIR5, P_STATIC_AIR6,
                                 P_STATICAIRDATE, P_STATICAIRBY);

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
                if (staticFlage == 1)
                    AirStaticLabModbusManager.Instance.Reset_AIR();

                if (!string.IsNullOrEmpty(txtStatic_Air.Text))
                {
                    if (string.IsNullOrEmpty(txtStatic_AirN1.Text))
                    {
                        txtStatic_AirN1.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN1.Text))
                            static_airN1 = decimal.Parse(txtStatic_AirN1.Text);
                        else
                            static_airN1 = null;

                        calAir1();
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_AirN1.Text) && string.IsNullOrEmpty(txtStatic_AirN1_2.Text))
                    {
                        txtStatic_AirN1_2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN1_2.Text))
                            static_airN1_2 = decimal.Parse(txtStatic_AirN1_2.Text);
                        else
                            static_airN1_2 = null;

                        calAir1();
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_AirN1.Text) && !string.IsNullOrEmpty(txtStatic_AirN1_2.Text) 
                        && string.IsNullOrEmpty(txtStatic_AirN2.Text))
                    {
                        txtStatic_AirN2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN2.Text))
                            static_airN2 = decimal.Parse(txtStatic_AirN2.Text);
                        else
                            static_airN2 = null;

                        calAir2();
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_AirN1.Text) && !string.IsNullOrEmpty(txtStatic_AirN1_2.Text)
                        && !string.IsNullOrEmpty(txtStatic_AirN2.Text) && string.IsNullOrEmpty(txtStatic_AirN2_2.Text))
                    {
                        txtStatic_AirN2_2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN2_2.Text))
                            static_airN2_2 = decimal.Parse(txtStatic_AirN2_2.Text);
                        else
                            static_airN2_2 = null;

                        calAir2();
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_AirN1.Text) && !string.IsNullOrEmpty(txtStatic_AirN1_2.Text)
                        && !string.IsNullOrEmpty(txtStatic_AirN2.Text) && !string.IsNullOrEmpty(txtStatic_AirN2_2.Text)
                        && string.IsNullOrEmpty(txtStatic_AirN3.Text))
                    {
                        txtStatic_AirN3.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN3.Text))
                            static_airN3 = decimal.Parse(txtStatic_AirN3.Text);
                        else
                            static_airN3 = null;

                        calAir3();
                    }
                    else if (!string.IsNullOrEmpty(txtStatic_AirN1.Text) && !string.IsNullOrEmpty(txtStatic_AirN1_2.Text)
                         && !string.IsNullOrEmpty(txtStatic_AirN2.Text) && !string.IsNullOrEmpty(txtStatic_AirN2_2.Text)
                         && !string.IsNullOrEmpty(txtStatic_AirN3.Text) && string.IsNullOrEmpty(txtStatic_AirN3_2.Text))
                    {
                        txtStatic_AirN3_2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN3_2.Text))
                            static_airN3_2 = decimal.Parse(txtStatic_AirN3_2.Text);
                        else
                            static_airN3_2 = null;

                        calAir3();
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
                if (staticFlage == 1)
                    AirStaticLabModbusManager.Instance.Reset_AIR();

                if (!string.IsNullOrEmpty(txtStatic_Air.Text))
                {
                    if (rbStatic_AirN1.IsChecked == true && string.IsNullOrEmpty(txtStatic_AirN1.Text))
                    {
                        txtStatic_AirN1.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN1.Text))
                            static_airN1 = decimal.Parse(txtStatic_AirN1.Text);
                        else
                            static_airN1 = null;

                        calAir1();
                    }
                    else if (rbStatic_AirN1.IsChecked == true && !string.IsNullOrEmpty(txtStatic_AirN1.Text) && string.IsNullOrEmpty(txtStatic_AirN1_2.Text))
                    {
                        txtStatic_AirN1_2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN1_2.Text))
                            static_airN1_2 = decimal.Parse(txtStatic_AirN1_2.Text);
                        else
                            static_airN1_2 = null;

                        calAir1();
                    }
                    else if (rbStatic_AirN2.IsChecked == true && string.IsNullOrEmpty(txtStatic_AirN2.Text))
                    {
                        txtStatic_AirN2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN2.Text))
                            static_airN2 = decimal.Parse(txtStatic_AirN2.Text);
                        else
                            static_airN2 = null;

                        calAir2();
                    }
                    else if (rbStatic_AirN2.IsChecked == true && !string.IsNullOrEmpty(txtStatic_AirN2.Text) && string.IsNullOrEmpty(txtStatic_AirN2_2.Text))
                    {
                        txtStatic_AirN2_2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN2_2.Text))
                            static_airN2_2 = decimal.Parse(txtStatic_AirN2_2.Text);
                        else
                            static_airN2_2 = null;

                        calAir2();
                    }
                    else if (rbStatic_AirN3.IsChecked == true && string.IsNullOrEmpty(txtStatic_AirN3.Text))
                    {
                        txtStatic_AirN3.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN3.Text))
                            static_airN3 = decimal.Parse(txtStatic_AirN3.Text);
                        else
                            static_airN3 = null;

                        calAir3();
                    }
                    else if (rbStatic_AirN3.IsChecked == true && !string.IsNullOrEmpty(txtStatic_AirN3.Text) && string.IsNullOrEmpty(txtStatic_AirN3_2.Text))
                    {
                        txtStatic_AirN3_2.Text = txtStatic_Air.Text;

                        if (!string.IsNullOrEmpty(txtStatic_AirN3_2.Text))
                            static_airN3_2 = decimal.Parse(txtStatic_AirN3_2.Text);
                        else
                            static_airN3_2 = null;

                        calAir3();
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

