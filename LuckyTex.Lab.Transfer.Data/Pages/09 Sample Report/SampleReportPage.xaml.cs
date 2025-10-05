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

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

#endregion


namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for SampleReportPage.xaml
    /// </summary>
    public partial class SampleReportPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public SampleReportPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdReport4746P25R_Click
        private void cmdReport4746P25R_Click(object sender, RoutedEventArgs e)
        {
            Preview("Sample4746P25R");
        }
        #endregion

        #region cmdReport4755ATW_Click
        private void cmdReport4755ATW_Click(object sender, RoutedEventArgs e)
        {
            Preview("Sample4755ATW");
        }
        #endregion

        #region cmdReport4L50B25R_Click
        private void cmdReport4L50B25R_Click(object sender, RoutedEventArgs e)
        {
            Preview("Sample4L50B25R");
        }
        #endregion

        #endregion

        #region private Methods

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string reportName)
        {
            try
            {
                ConmonReportService.Instance.ReportName = reportName;

                if (!string.IsNullOrEmpty(ConmonReportService.Instance.ReportName))
                {
                    StringBuilder dp = new StringBuilder(256);
                    int size = dp.Capacity;
                    if (GetDefaultPrinter(dp, ref size))
                    {
                        DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                        rep.PrintByPrinter(dp.ToString().Trim());
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

        private void Preview(string reportName)
        {
            try
            {
                ConmonReportService.Instance.ReportName = reportName;

                if (!string.IsNullOrEmpty(ConmonReportService.Instance.ReportName))
                {
                    var newWindow = new RepMasterForm();
                    newWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #endregion

    }
}
