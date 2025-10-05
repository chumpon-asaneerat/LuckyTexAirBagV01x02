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
using System.Configuration;
using System.Data;
using System.Data.OleDb;

using System.Printing;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for AS400Page.xaml
    /// </summary>
    public partial class AS400Page : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AS400Page()
        {
            InitializeComponent();
            GetPrintName();
        }

        #endregion

        #region Internal Variables

        string strLogIn = string.Empty;
        string strConAS400 = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigManager.Instance.LoadAS400Configs();
            strConAS400 = ConfigManager.Instance.AS400Config;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdConnectAS400_Click(object sender, RoutedEventArgs e)
        {
            List<BCSPRFTPResult> results = BCSPRFTPDataService.Instance.GetBCSPRFTP(strConAS400,"");

            if (results != null && results.Count > 0)
            {
                gridAS400.ItemsSource = results;

                labConnect.Content = "Can Connect";
            }
            else
            {
                labConnect.Content = "Can't Connect";
            }

            //if (CheckConDB2() == true)
            //{
            //    labConnect.Content = "Can Connect";
            //}
            //else
            //{
            //    labConnect.Content = "Can't Connect";
            //}
        }

        private void cmdPreview_Click(object sender, RoutedEventArgs e)
        {
            Preview("");
        }

        private void cmdTest_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                PrintName(listBox1.SelectedItem.ToString());
            }
        }

        private void cmdTest2_Click(object sender, RoutedEventArgs e)
        {
            PrintName();
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string test)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "BarcodePart";

                string CmdString = string.Empty;
                ConmonReportService.Instance.CmdString = test;

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

        private void PrintName(string test)
        {
            try
            {
                if (test != "")
                {
                    ConmonReportService.Instance.ReportName = "BarcodePart";
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(test);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PrintName()
        {
            try
            {
                ConmonReportService.Instance.ReportName = "BarcodePart";

                var printDialog = new PrintDialog();
                printDialog.PageRangeSelection = PageRangeSelection.AllPages;
                printDialog.UserPageRangeEnabled = true;
                var doPrint = printDialog.ShowDialog();
                if (doPrint == true)
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(printDialog.PrintQueue.FullName.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string test)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.CmdString = test;

                ConmonReportService.Instance.ReportName = "BarcodePart";

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

        #region CheckConDB2
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CmdString"></param>
        /// <returns></returns>
        public bool CheckConDB2()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(strConAS400))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    if (con.State == ConnectionState.Open)
                        con.Close();

                    return true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        #endregion

        private void GetPrintName()
        {
            foreach (string printname in PrinterSettings.InstalledPrinters)
            {
                listBox1.Items.Add(printname);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="logIn"></param>
        public void Setup(string logIn)
        {
            strLogIn = logIn;
        }

        #endregion

    }
}
