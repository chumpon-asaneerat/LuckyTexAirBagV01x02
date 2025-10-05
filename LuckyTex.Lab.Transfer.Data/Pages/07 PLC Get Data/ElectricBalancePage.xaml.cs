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
    /// Interaction logic for ElectricBalancePage.xaml
    /// </summary>
    public partial class ElectricBalancePage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElectricBalancePage()
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

        int? weightFlage = 0;

        string itemCode = string.Empty;
        string productionlot = string.Empty;

        int chkProperty = 0;
        bool chkPro = true;

        decimal? weight1 = null;
        decimal? weight2 = null;
        decimal? weight3 = null;
        decimal? weight4 = null;
        decimal? weight5 = null;
        decimal? weight6 = null;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (opera != "")
                txtOperator.Text = opera;

            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.ScouringLabMachineConfig;

            ClearControl();

            if (ScouringLabModbusManager.Instance.Reset_WEIGHT() == false)
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

            if (e.Value.WEIGHTFLAG == 1)
            {
                //txtWeight.Text = e.Value.WEIGHT.Value.ToString("n2").Replace(",", string.Empty);

                if (e.Value.WEIGHT != null)
                    txtWeight.Text = MathEx.Round(e.Value.WEIGHT.Value, 3).ToString("#,##0.###");
                else
                    txtWeight.Text = "0.000";

                weightFlage = 1;
            }
            else 
            {
                //txtWeight.Text = "0.000";
                weightFlage = 0;
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
                    if (!string.IsNullOrEmpty(txtWeight1.Text) || !string.IsNullOrEmpty(txtWeight2.Text) || !string.IsNullOrEmpty(txtWeight3.Text)
                       || !string.IsNullOrEmpty(txtWeight4.Text) || !string.IsNullOrEmpty(txtWeight5.Text) || !string.IsNullOrEmpty(txtWeight6.Text))
                    {
                        if (LAB_SAVEREPLCWEIGHT() == true)
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
                    if (string.IsNullOrEmpty(txtWeight1.Text) && string.IsNullOrEmpty(txtWeight2.Text) && string.IsNullOrEmpty(txtWeight3.Text)
                        && string.IsNullOrEmpty(txtWeight4.Text) && string.IsNullOrEmpty(txtWeight5.Text) && string.IsNullOrEmpty(txtWeight6.Text))
                    {
                        "Please Select PLC Data Before Save".ShowMessageBox();
                    }
                    else
                    {
                        if (CheckDataNull() == true)
                        {
                            #region LAB_SAVEPLCDATA
                            if (LAB_SAVEPLCDATA() == true)
                            {
                                "Save Data Complete".ShowMessageBox();

                                ClearControl();

                                //weight1 = null;
                                //weight2 = null;
                                //weight3 = null;
                                //weight4 = null;
                                //weight5 = null;
                                //weight6 = null;

                                //txtWeight.Text = string.Empty;
                                //txtWeight1.Text = string.Empty;
                                //txtWeight2.Text = string.Empty;
                                //txtWeight3.Text = string.Empty;
                                //txtWeight4.Text = string.Empty;
                                //txtWeight5.Text = string.Empty;
                                //txtWeight6.Text = string.Empty;

                                //rbTotalWeight.IsChecked = true;
                                //rbUncoatedWeight.IsChecked = false;
                                //rbCoatingWeight.IsChecked = false;
                                //chkProperty = 1;
                                //chkPro = true;

                                //txtWeight.Focus();
                            }
                            else
                            {
                                "Can't Save Please check data".ShowMessageBox();
                            }
                            #endregion
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

        #region txtWeight1_KeyDown
        private void txtWeight1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeight2.Focus();
                txtWeight2.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeight2_KeyDown
        private void txtWeight2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeight3.Focus();
                txtWeight3.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeight3_KeyDown
        private void txtWeight3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeight4.Focus();
                txtWeight4.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeight4_KeyDown
        private void txtWeight4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeight5.Focus();
                txtWeight5.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeight5_KeyDown
        private void txtWeight5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeight6.Focus();
                txtWeight6.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWeight6_KeyDown
        private void txtWeight6_KeyDown(object sender, KeyEventArgs e)
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

        #region txtWeight1_LostFocus
        private void txtWeight1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight1.Text))
                weight1 = decimal.Parse(txtWeight1.Text);
            else
                weight1 = null;
        }
        #endregion

        #region txtWeight2_LostFocus
        private void txtWeight2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight2.Text))
                weight2 = decimal.Parse(txtWeight2.Text);
            else
                weight2 = null;
        }
        #endregion

        #region txtWeight3_LostFocus
        private void txtWeight3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight3.Text))
                weight3 = decimal.Parse(txtWeight3.Text);
            else
                weight3 = null;
        }
        #endregion

        #region txtWeight4_LostFocus
        private void txtWeight4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight4.Text))
                weight4 = decimal.Parse(txtWeight4.Text);
            else
                weight4 = null;
        }
        #endregion

        #region txtWeight5_LostFocus
        private void txtWeight5_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight5.Text))
                weight5 = decimal.Parse(txtWeight5.Text);
            else
                weight5 = null;
        }
        #endregion

        #region txtWeight6_LostFocus
        private void txtWeight6_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight6.Text))
                weight6 = decimal.Parse(txtWeight6.Text);
            else
                weight6 = null;
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #region RadioButton

        private void rbTotalWeight_Checked(object sender, RoutedEventArgs e)
        {
            if (chkPro == true)
            {
                if (chkWeight1_6() == false)
                {
                    if ("Please Save Data Before Change Property".ShowMessageOKCancel() == true)
                    {
                        chkPro = false;

                        if (chkProperty == 1)
                            rbTotalWeight.IsChecked = true;
                        else if (chkProperty == 2)
                            rbUncoatedWeight.IsChecked = true;
                        else if (chkProperty == 3)
                            rbCoatingWeight.IsChecked = true;

                        chkPro = true;
                    }
                    else
                    {
                        txtWeight.Text = string.Empty;
                        txtWeight1.Text = string.Empty;
                        txtWeight2.Text = string.Empty;
                        txtWeight3.Text = string.Empty;
                        txtWeight4.Text = string.Empty;
                        txtWeight5.Text = string.Empty;
                        txtWeight6.Text = string.Empty;

                        weight1 = null;
                        weight2 = null;
                        weight3 = null;
                        weight4 = null;
                        weight5 = null;
                        weight6 = null;

                        chkProperty = 1;
                    }
                }
                else
                {
                    chkProperty = 1;
                }
            }
        }

        private void rbUncoatedWeight_Checked(object sender, RoutedEventArgs e)
        {
            if (chkPro == true)
            {
                if (chkWeight1_6() == false)
                {
                    if ("Please Save Data Before Change Property".ShowMessageOKCancel() == true)
                    {
                        chkPro = false;

                        if (chkProperty == 1)
                            rbTotalWeight.IsChecked = true;
                        else if (chkProperty == 2)
                            rbUncoatedWeight.IsChecked = true;
                        else if (chkProperty == 3)
                            rbCoatingWeight.IsChecked = true;

                        chkPro = true;
                    }
                    else
                    {
                        txtWeight.Text = string.Empty;
                        txtWeight1.Text = string.Empty;
                        txtWeight2.Text = string.Empty;
                        txtWeight3.Text = string.Empty;
                        txtWeight4.Text = string.Empty;
                        txtWeight5.Text = string.Empty;
                        txtWeight6.Text = string.Empty;

                        weight1 = null;
                        weight2 = null;
                        weight3 = null;
                        weight4 = null;
                        weight5 = null;
                        weight6 = null;

                        chkProperty = 2;
                    }
                }
                else
                {
                    chkProperty = 2;
                }
            }
        }

        private void rbCoatingWeight_Checked(object sender, RoutedEventArgs e)
        {
            if (chkPro == true)
            {
                if (chkWeight1_6() == false)
                {
                    if ("Please Save Data Before Change Property".ShowMessageOKCancel() == true)
                    {
                        chkPro = false;

                        if (chkProperty == 1)
                            rbTotalWeight.IsChecked = true;
                        else if (chkProperty == 2)
                            rbUncoatedWeight.IsChecked = true;
                        else if (chkProperty == 3)
                            rbCoatingWeight.IsChecked = true;

                        chkPro = true;
                    }
                    else
                    {
                        txtWeight.Text = string.Empty;
                        txtWeight1.Text = string.Empty;
                        txtWeight2.Text = string.Empty;
                        txtWeight3.Text = string.Empty;
                        txtWeight4.Text = string.Empty;
                        txtWeight5.Text = string.Empty;
                        txtWeight6.Text = string.Empty;

                        weight1 = null;
                        weight2 = null;
                        weight3 = null;
                        weight4 = null;
                        weight5 = null;
                        weight6 = null;

                        chkProperty = 3;
                    }
                }
                else
                {
                    chkProperty = 3;
                }
            }
        }

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
            rbN1.Visibility = Visibility.Visible;
            rbN2.Visibility = Visibility.Visible;
            rbN3.Visibility = Visibility.Visible;
            rbN4.Visibility = Visibility.Visible;
            rbN5.Visibility = Visibility.Visible;
            rbN6.Visibility = Visibility.Visible;

            rbN1.IsChecked = true;
        }

        private void chkRetest_Unchecked(object sender, RoutedEventArgs e)
        {
            rbN1.Visibility = Visibility.Collapsed;
            rbN2.Visibility = Visibility.Collapsed;
            rbN3.Visibility = Visibility.Collapsed;
            rbN4.Visibility = Visibility.Collapsed;
            rbN5.Visibility = Visibility.Collapsed;
            rbN6.Visibility = Visibility.Collapsed;
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
            weight1 = null;
            weight2 = null;
            weight3 = null;
            weight4 = null;
            weight5 = null;
            weight6 = null;

            txtITMCODE.Text = string.Empty;
            txtPRODUCTIONLOT.Text = string.Empty;

            txtWeight.Text = string.Empty;
            txtWeight1.Text = string.Empty;
            txtWeight2.Text = string.Empty;
            txtWeight3.Text = string.Empty;
            txtWeight4.Text = string.Empty;
            txtWeight5.Text = string.Empty;
            txtWeight6.Text = string.Empty;

            rbTotalWeight.IsChecked = true;
            rbUncoatedWeight.IsChecked = false;
            rbCoatingWeight.IsChecked = false;
            chkProperty = 1;
            chkPro = true;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = false;

            rbN1.Visibility = Visibility.Collapsed;
            rbN2.Visibility = Visibility.Collapsed;
            rbN3.Visibility = Visibility.Collapsed;
            rbN4.Visibility = Visibility.Collapsed;
            rbN5.Visibility = Visibility.Collapsed;
            rbN6.Visibility = Visibility.Collapsed;

            rbN1.IsChecked = true;

            EnabledCon(false);

            txtPRODUCTIONLOT.Focus();
        }

        #endregion

        #region ClearControlReTest

        private void ClearControlReTest()
        {
            weight1 = null;
            weight2 = null;
            weight3 = null;
            weight4 = null;
            weight5 = null;
            weight6 = null;

            txtWeight.Text = string.Empty;
            txtWeight1.Text = string.Empty;
            txtWeight2.Text = string.Empty;
            txtWeight3.Text = string.Empty;
            txtWeight4.Text = string.Empty;
            txtWeight5.Text = string.Empty;
            txtWeight6.Text = string.Empty;

            rbTotalWeight.IsChecked = true;
            rbUncoatedWeight.IsChecked = false;
            rbCoatingWeight.IsChecked = false;
            chkProperty = 1;
            chkPro = true;

            chkManual.IsChecked = false;
            chkRetest.IsChecked = true;
            rbN1.IsChecked = true;

            EnabledCon(false);

            txtWeight.Focus();
        }

        #endregion

        #region EnabledCon
        private void EnabledCon(bool chkManual)
        {
            txtWeight.IsEnabled = chkManual;
            txtWeight1.IsEnabled = chkManual;
            txtWeight2.IsEnabled = chkManual;
            txtWeight3.IsEnabled = chkManual;
            txtWeight4.IsEnabled = chkManual;
            txtWeight5.IsEnabled = chkManual;
            txtWeight6.IsEnabled = chkManual;

            if (chkManual == false)
            {
                if (mcStatus == true)
                {
                    ScouringLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
                    ScouringLabModbusManager.Instance.Start();

                    //if (ScouringLabModbusManager.Instance.Reset_WEIGHT() == false)
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
                weightFlage = 0;
            }
        }
        #endregion

        #region chkWeight1_6
        private bool chkWeight1_6()
        {
            bool chkWeight = true;

            try
            {
                if (!string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtWeight2.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtWeight3.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtWeight4.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtWeight5.Text))
                {
                    chkWeight = false;
                }
                if (!string.IsNullOrEmpty(txtWeight6.Text))
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

                if (!string.IsNullOrEmpty(txtWeight6.Text) && !string.IsNullOrEmpty(txtWeight5.Text) && !string.IsNullOrEmpty(txtWeight4.Text) && !string.IsNullOrEmpty(txtWeight3.Text)
                        && !string.IsNullOrEmpty(txtWeight2.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtWeight6.Text) && !string.IsNullOrEmpty(txtWeight5.Text) && !string.IsNullOrEmpty(txtWeight4.Text) && !string.IsNullOrEmpty(txtWeight3.Text)
                        && !string.IsNullOrEmpty(txtWeight2.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtWeight6.Text) && string.IsNullOrEmpty(txtWeight5.Text) && !string.IsNullOrEmpty(txtWeight4.Text) && !string.IsNullOrEmpty(txtWeight3.Text)
                   && !string.IsNullOrEmpty(txtWeight2.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtWeight6.Text) && string.IsNullOrEmpty(txtWeight5.Text) && string.IsNullOrEmpty(txtWeight4.Text) && !string.IsNullOrEmpty(txtWeight3.Text)
                       && !string.IsNullOrEmpty(txtWeight2.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtWeight6.Text) && string.IsNullOrEmpty(txtWeight5.Text) && string.IsNullOrEmpty(txtWeight4.Text) && string.IsNullOrEmpty(txtWeight3.Text)
                       && !string.IsNullOrEmpty(txtWeight2.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtWeight6.Text) && string.IsNullOrEmpty(txtWeight5.Text) && string.IsNullOrEmpty(txtWeight4.Text) && string.IsNullOrEmpty(txtWeight3.Text)
                     && string.IsNullOrEmpty(txtWeight2.Text) && !string.IsNullOrEmpty(txtWeight1.Text))
                {
                    chkData = true;
                }
                else if (string.IsNullOrEmpty(txtWeight6.Text) && string.IsNullOrEmpty(txtWeight5.Text) && string.IsNullOrEmpty(txtWeight4.Text) && string.IsNullOrEmpty(txtWeight3.Text)
                     && string.IsNullOrEmpty(txtWeight2.Text) && string.IsNullOrEmpty(txtWeight1.Text))
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

                if (!string.IsNullOrEmpty(txtWeight1.Text))
                    weight1 = decimal.Parse(txtWeight1.Text);
                else
                    weight1 = null;

                if (!string.IsNullOrEmpty(txtWeight2.Text))
                    weight2 = decimal.Parse(txtWeight2.Text);
                else
                    weight2 = null;

                if (!string.IsNullOrEmpty(txtWeight3.Text))
                    weight3 = decimal.Parse(txtWeight3.Text);
                else
                    weight3 = null;

                if (!string.IsNullOrEmpty(txtWeight4.Text))
                    weight4 = decimal.Parse(txtWeight4.Text);
                else
                    weight4 = null;

                if (!string.IsNullOrEmpty(txtWeight5.Text))
                    weight5 = decimal.Parse(txtWeight5.Text);
                else
                    weight5 = null;

                if (!string.IsNullOrEmpty(txtWeight6.Text))
                    weight6 = decimal.Parse(txtWeight6.Text);
                else
                    weight6 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                if (chkProperty == 1)
                {
                    P_TOTALWEIGHT1 = weight1;
                    P_TOTALWEIGHT2 = weight2;
                    P_TOTALWEIGHT3 = weight3;
                    P_TOTALWEIGHT4 = weight4;
                    P_TOTALWEIGHT5 = weight5;
                    P_TOTALWEIGHT6 = weight6;
                }
                else if (chkProperty == 2)
                {
                    P_UNCOATEDWEIGHT1 = weight1;
                    P_UNCOATEDWEIGHT2 = weight2;
                    P_UNCOATEDWEIGHT3 = weight3;
                    P_UNCOATEDWEIGHT4 = weight4;
                    P_UNCOATEDWEIGHT5 = weight5;
                    P_UNCOATEDWEIGHT6 = weight6;
                }
                else if (chkProperty == 3)
                {
                    P_COATWEIGHT1 = weight1;
                    P_COATWEIGHT2 = weight2;
                    P_COATWEIGHT3 = weight3;
                    P_COATWEIGHT4 = weight4;
                    P_COATWEIGHT5 = weight5;
                    P_COATWEIGHT6 = weight6;
                }

                P_WEIGHTDATE = DateTime.Now;
                P_WEIGHTBY = opera;


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

        #region LAB_SAVEREPLCWEIGHT
        private bool LAB_SAVEREPLCWEIGHT()
        {
            bool chkLoad = true;

            try
            {
                #region default

                string P_ITMCODE = string.Empty;
                string P_PRODUCTIONLOT = string.Empty;
                string P_TYPE = string.Empty;

                decimal? P_WEIGHT1 = null;
                decimal? P_WEIGHT2 = null;
                decimal? P_WEIGHT3 = null;
                decimal? P_WEIGHT4 = null;
                decimal? P_WEIGHT5 = null;
                decimal? P_WEIGHT6 = null;
           
                DateTime? P_WEIGHTDATE = null;
                string P_WEIGHTBY = string.Empty;
             
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

                if (!string.IsNullOrEmpty(txtWeight1.Text))
                    weight1 = decimal.Parse(txtWeight1.Text);
                else
                    weight1 = null;

                if (!string.IsNullOrEmpty(txtWeight2.Text))
                    weight2 = decimal.Parse(txtWeight2.Text);
                else
                    weight2 = null;

                if (!string.IsNullOrEmpty(txtWeight3.Text))
                    weight3 = decimal.Parse(txtWeight3.Text);
                else
                    weight3 = null;

                if (!string.IsNullOrEmpty(txtWeight4.Text))
                    weight4 = decimal.Parse(txtWeight4.Text);
                else
                    weight4 = null;

                if (!string.IsNullOrEmpty(txtWeight5.Text))
                    weight5 = decimal.Parse(txtWeight5.Text);
                else
                    weight5 = null;

                if (!string.IsNullOrEmpty(txtWeight6.Text))
                    weight6 = decimal.Parse(txtWeight6.Text);
                else
                    weight6 = null;

                #endregion

                P_ITMCODE = itemCode;
                P_PRODUCTIONLOT = productionlot;

                P_WEIGHT1 = weight1;
                P_WEIGHT2 = weight2;
                P_WEIGHT3 = weight3;
                P_WEIGHT4 = weight4;
                P_WEIGHT5 = weight5;
                P_WEIGHT6 = weight6;

                if (chkProperty == 1)
                {
                    P_TYPE = "TW";
                }
                else if (chkProperty == 2)
                {
                    P_TYPE = "UW";
                }
                else if (chkProperty == 3)
                {
                    P_TYPE = "CW";
                }

                P_WEIGHTDATE = DateTime.Now;
                P_WEIGHTBY = opera;


                string results = LabDataPDFDataService.Instance.LAB_SAVEREPLCWEIGHT(P_ITMCODE, P_PRODUCTIONLOT,P_TYPE,
                                 P_WEIGHT1, P_WEIGHT2, P_WEIGHT3, P_WEIGHT4, P_WEIGHT5, P_WEIGHT6,
                                 P_WEIGHTDATE, P_WEIGHTBY);

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
                if (weightFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_WEIGHT();

                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    decimal? _weight = null;

                    _weight = (decimal.Parse(txtWeight.Text) * 100);

                    if (string.IsNullOrEmpty(txtWeight1.Text))
                    {
                        txtWeight1.Text = _weight.Value.ToString("#,##0.##");
                        //txtWeight1.Text = txtWeight.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtWeight1.Text) && string.IsNullOrEmpty(txtWeight2.Text))
                    {
                        txtWeight2.Text = _weight.Value.ToString("#,##0.##");
                        //txtWeight2.Text = txtWeight.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtWeight1.Text) && !string.IsNullOrEmpty(txtWeight2.Text) && string.IsNullOrEmpty(txtWeight3.Text))
                    {
                        txtWeight3.Text = _weight.Value.ToString("#,##0.##");
                        //txtWeight3.Text = txtWeight.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtWeight1.Text) && !string.IsNullOrEmpty(txtWeight2.Text) &&
                        !string.IsNullOrEmpty(txtWeight3.Text) && string.IsNullOrEmpty(txtWeight4.Text))
                    {
                        txtWeight4.Text = _weight.Value.ToString("#,##0.##");
                        //txtWeight4.Text = txtWeight.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtWeight1.Text) && !string.IsNullOrEmpty(txtWeight2.Text) &&
                        !string.IsNullOrEmpty(txtWeight3.Text) && !string.IsNullOrEmpty(txtWeight4.Text)
                        && string.IsNullOrEmpty(txtWeight5.Text))
                    {
                        txtWeight5.Text = _weight.Value.ToString("#,##0.##");
                        //txtWeight5.Text = txtWeight.Text;
                    }
                    else if (!string.IsNullOrEmpty(txtWeight1.Text) && !string.IsNullOrEmpty(txtWeight2.Text) &&
                        !string.IsNullOrEmpty(txtWeight3.Text) && !string.IsNullOrEmpty(txtWeight4.Text)
                        && !string.IsNullOrEmpty(txtWeight5.Text) && string.IsNullOrEmpty(txtWeight6.Text))
                    {
                        txtWeight6.Text = _weight.Value.ToString("#,##0.##");
                        //txtWeight6.Text = txtWeight.Text;
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
                if (weightFlage == 1)
                    ScouringLabModbusManager.Instance.Reset_WEIGHT();

                if (!string.IsNullOrEmpty(txtWeight.Text))
                {
                    decimal? _weight = null;

                    _weight = (decimal.Parse(txtWeight.Text) * 100);

                    if (rbN1.IsChecked == true)
                    {
                        txtWeight1.Text = _weight.Value.ToString("#,##0.##");
                    }
                    else if (rbN2.IsChecked == true)
                    {
                        txtWeight2.Text = _weight.Value.ToString("#,##0.##");
                    }
                    else if (rbN3.IsChecked == true)
                    {
                        txtWeight3.Text = _weight.Value.ToString("#,##0.##");
                    }
                    else if (rbN4.IsChecked == true)
                    {
                        txtWeight4.Text = _weight.Value.ToString("#,##0.##");
                    }
                    else if (rbN5.IsChecked == true)
                    {
                        txtWeight5.Text = _weight.Value.ToString("#,##0.##");
                    }
                    else if (rbN6.IsChecked == true)
                    {
                        txtWeight6.Text = _weight.Value.ToString("#,##0.##");
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

