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
    /// Interaction logic for PLC_TestPage.xaml
    /// </summary>
    public partial class PLC_TestPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PLC_TestPage()
        {
            InitializeComponent();
            ConmonReportService.Instance.OperatorID = string.Empty;
        }

        #endregion

        #region Internal Variables

        private bool mcStatus = true;
        private bool mcAir = true;

        int? weightFlage = 0;
        int? highFlage = 0;
        int? airFlage = 0;
        int? dynamicFlage = 0;
        int? staticFlage = 0;

        #endregion

        #region Private Methods

        #region ClearInputs

        private void ClearInputs()
        {
            txtWeight.Text = "0.000";
            txtHigh.Text = "0.00";
            txtAir.Text = "0.00";
            txtDynamicAir.Text = "0.00";
            txtExponent.Text = "0.00";
            txtStaticAir.Text = "0.00";
        }

        #endregion

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtWeight.IsEnabled = false;
            txtHigh.IsEnabled = false;
            txtAir.IsEnabled = false;
            txtDynamicAir.IsEnabled = false;
            txtExponent.IsEnabled = false;
            txtStaticAir.IsEnabled = false;

            ClearInputs();
            ConfigManager.Instance.LoadMachineStatusConfig();
            mcStatus = ConfigManager.Instance.ScouringLabMachineConfig;

            if (mcStatus == true)
            {
                ScouringLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
                ScouringLabModbusManager.Instance.Start();
            }

            mcAir = ConfigManager.Instance.AirStaticLabConfig;

            if (mcAir == true)
            {
                AirStaticLabModbusManager.Instance.ReadCompleted += new NLib.Devices.Modbus.ModbusReadEventHandler<AirStaticLab>(AirStaticLab_ReadCompleted);
                AirStaticLabModbusManager.Instance.Start();
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ScouringLabModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<ScouringLab>(Instance_ReadCompleted);
            ScouringLabModbusManager.Instance.Shutdown();

            AirStaticLabModbusManager.Instance.ReadCompleted -= new NLib.Devices.Modbus.ModbusReadEventHandler<AirStaticLab>(AirStaticLab_ReadCompleted);
            AirStaticLabModbusManager.Instance.Shutdown();
        }

        void Instance_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<ScouringLab> e)
        {
            if (null == e.Value)
            {
                return;
            }

            if (e.Value.WEIGHTFLAG == 1)
            {
                if (e.Value.WEIGHT != null)
                    txtWeight.Text = MathEx.Round(e.Value.WEIGHT.Value, 3).ToString("#,##0.###");
                else
                    txtWeight.Text = "0.000";

                //txtWeight.Text = e.Value.WEIGHT.Value.ToString("n2").Replace(",", string.Empty);

                weightFlage = 1;
            }
            else
            {
                txtWeight.Text = "0.00";
                weightFlage = 0;
            }

            if (e.Value.HIGHPRESSFLAG == 1)
            {
                if (e.Value.HIGHPRESS != null)
                    txtHigh.Text = MathEx.Round(e.Value.HIGHPRESS.Value, 2).ToString("#,##0.##");
                else
                    txtHigh.Text = "0.00";

                //txtHigh.Text = e.Value.HIGHPRESS.Value.ToString("n2").Replace(",", string.Empty);

                highFlage = 1;
            }
            else
            {
                txtHigh.Text = "0.00";
                highFlage = 0;
            }

            if (e.Value.AIRFLAG == 1)
            {
                if (e.Value.AIR != null)
                    txtAir.Text = MathEx.Round(e.Value.AIR.Value, 2).ToString("#,##0.##");
                else
                    txtAir.Text = "0.00";

                //txtAir.Text = e.Value.AIR.Value.ToString("n2").Replace(",", string.Empty);

                airFlage = 1;
            }
            else
            {
                txtAir.Text = "0.00";
                airFlage = 0;
            }

            if (e.Value.DYNAMICFLAG == 1)
            {
                if (e.Value.DYNAMIC != null)
                    txtDynamicAir.Text = MathEx.Round(e.Value.DYNAMIC.Value, 2).ToString("#,##0.##");
                else
                    txtDynamicAir.Text = "0.00";

                if (e.Value.EXPONENT != null)
                    txtExponent.Text = MathEx.Round(e.Value.EXPONENT.Value, 2).ToString("#,##0.##");
                else
                    txtExponent.Text = "0.00";

                //txtDynamicAir.Text = e.Value.DYNAMIC.Value.ToString("n2").Replace(",", string.Empty);
                //txtExponent.Text = e.Value.EXPONENT.Value.ToString("n2").Replace(",", string.Empty);

                dynamicFlage = 1;
            }
            else
            {
                txtDynamicAir.Text = "0.00";
                txtExponent.Text = "0.00";
                dynamicFlage = 0;
            }


            if (e.Value.STATICFLAG == 1)
            {
                if (e.Value.STATIC != null)
                    txtStaticAir.Text = MathEx.Round(e.Value.STATIC.Value, 2).ToString("#,##0.##");
                else
                    txtStaticAir.Text = "0.00";

                staticFlage = 1;
            }
            else
            {
                txtStaticAir.Text = "0.00";
                staticFlage = 0;
            }
        }

        void AirStaticLab_ReadCompleted(object sender, NLib.Devices.Modbus.ModbusReadEventArgs<AirStaticLab> e)
        {
            if (null == e.Value)
            {
                return;
            }

            if (e.Value.AIRFLAG == 1)
            {
                if (e.Value.AIR != null)
                    txtStaticAir.Text = MathEx.Round(e.Value.AIR.Value, 2).ToString("#,##0.##");
                else
                    txtStaticAir.Text = "0.00";

                staticFlage = 1;
            }
            else
            {
                txtStaticAir.Text = "0.00";
                staticFlage = 0;
            }
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdReset_Weight_Click(object sender, RoutedEventArgs e)
        {
            if (weightFlage == 1)
                ScouringLabModbusManager.Instance.Reset_WEIGHT();
        }

        private void cmdReset_High_Click(object sender, RoutedEventArgs e)
        {
            if (highFlage == 1)
                ScouringLabModbusManager.Instance.Reset_HIGHPRESS();
        }

        private void cmdReset_Air_Click(object sender, RoutedEventArgs e)
        {
            if (airFlage == 1)
                ScouringLabModbusManager.Instance.Reset_AIR();
        }

        private void cmdDynamic_Air_Click(object sender, RoutedEventArgs e)
        {
            if (dynamicFlage == 1)
                ScouringLabModbusManager.Instance.Reset_DYNAMIC();
        }

        private void cmdReset_StaticAir_Click(object sender, RoutedEventArgs e)
        {
            if (staticFlage == 1)
                AirStaticLabModbusManager.Instance.Reset_AIR();
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region TextBox Handlers

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHigh.Focus();
                txtHigh.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHigh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtAir.Focus();
                txtAir.SelectAll();
                e.Handled = true;
            }
        }

        private void txtAir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDynamicAir.Focus();
                txtDynamicAir.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDynamicAir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtExponent.Focus();
                txtExponent.SelectAll();
                e.Handled = true;
            }
        }

        private void txtExponent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStaticAir.Focus();
                txtStaticAir.SelectAll();
                e.Handled = true;
            }
        }

        private void txtStaticAir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdBack.Focus();
                e.Handled = true;
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
