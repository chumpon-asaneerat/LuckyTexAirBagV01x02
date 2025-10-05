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
using System.Windows.Threading;

using NLib;
using LuckyTex.Configs;
using LuckyTex.Pages;
using LuckyTex.Services;

using System.Globalization;
using System.Threading;

#endregion

namespace LuckyTex
{
    /// <summary>
    /// The LuckyTex Air bag application main window.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            //ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            //Thread.CurrentThread.CurrentCulture = ci;
            //IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Private Methods

        #region Update Time

        private void UpdateTime()
        {
            // update current date time.
            txtDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss",
                System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        #endregion

        #region Start/Shutdown Services

        private void StartServices()
        {
            // Load config
            ConfigManager.Instance.LoadConfigs();
            // Start Inspection PLC Service.
            InspectionPLCService.Instance.Start();

            ConfigManager.Instance.LoadConfigs_D365();
        }

        private void ShutdownServices()
        {
            InspectionPLCService.Instance.Stop();

            InspectionWeightModbusManager.Instance.Shutdown();

            // เพิ่ม Shutdown Modbus
            Coating1ModbusManager.Instance.Shutdown();
            Coating2ModbusManager.Instance.Shutdown();
            Scouring1ModbusManager.Instance.Shutdown();
            Scouring2ModbusManager.Instance.Shutdown();
        }

        #endregion

        #endregion

        #region Load/Unload

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set Last Update string
            string lastUpdate = ApplicationManager.Instance.Environments.Options
                .AppInfo.LastUpdate.ToString("yyyy-MM-dd HH:mm",
                    System.Globalization.DateTimeFormatInfo.InvariantInfo);
            // create version
            string version = "v{0}.{1} build {2} ({3})".args(
                ApplicationManager.Instance.Environments.Options.AppInfo.Version,
                ApplicationManager.Instance.Environments.Options.AppInfo.Minor,
                ApplicationManager.Instance.Environments.Options.AppInfo.Build,
                lastUpdate);
            txtVersion.Text = version;

            // Write App Log
            string msg = string.Empty;
            msg += "=====================================================================================" + Environment.NewLine;
            msg += "| " + ApplicationManager.Instance.Environments.Options.AppInfo.ProductName + " " + version + Environment.NewLine;
            msg += "=====================================================================================" + Environment.NewLine;
            msg.Info(); // Show application name and version in log

            // Start Services
            PageManager.Instance.ContentChanged += new EventHandler(Instance_ContentChanged);
            PageManager.Instance.StatusUpdated += new StatusMessageEventHandler(Instance_StatusUpdated);
            PageManager.Instance.OnTick += new EventHandler(Instance_OnTick);
            PageManager.Instance.Start();
            // Init Main Menu
            PageManager.Instance.Current = new MainMenuPage();

            StartServices();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                "Main Window Closing....Services begin to shutdown.".Info();
                //e.Cancel = false;

                ShutdownServices();

                // Shutdown Services
                PageManager.Instance.Shutdown();
                PageManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
                PageManager.Instance.StatusUpdated -= new StatusMessageEventHandler(Instance_StatusUpdated);
                PageManager.Instance.ContentChanged -= new EventHandler(Instance_ContentChanged);
                // Shutdown Database Manager.
                DatabaseManager.Instance.Shutdown();

                "Main Window Closing....Services shutdown finished.".Info();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ex.Message.ToString().Err();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
                System.Windows.Forms.Application.Exit();
                this.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                ex.Message.ToString().Err();
            }
        }

        #endregion

        #region Page Manager Handlers

        void Instance_OnTick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        void Instance_StatusUpdated(object sender, StatusMessageEventArgs e)
        {
            txtStatus.Text = e.Message;
        }

        void Instance_ContentChanged(object sender, EventArgs e)
        {
            this.container.Content = PageManager.Instance.Current;
        }

        #endregion

    }
}
