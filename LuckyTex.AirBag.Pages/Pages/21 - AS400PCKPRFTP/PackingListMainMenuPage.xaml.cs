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
    /// Interaction logic for PackingListMainMenuPage.xaml
    /// </summary>
    public partial class PackingListMainMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PackingListMainMenuPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        private void cmdManualInserData_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 11; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    ManualPackingListPage page = new ManualPackingListPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }

            //ManualPackingListPage page = new ManualPackingListPage();
            //PageManager.Instance.Current = page;
        }

        private void cmdScanBarcode_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 11; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    PackingListScanBarcodePage page = new PackingListScanBarcodePage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdScanBarcodeToyota_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 11; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    PackingListScanBarcodeToyotaPage page = new PackingListScanBarcodeToyotaPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        #endregion

    }
}
