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

using System.Diagnostics;
using System.Printing;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WarpingProcessMenuPage.xaml
    /// </summary>
    public partial class WarpingProcessMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingProcessMenuPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables
        private WarpingSession _session = new WarpingSession();

        string mcName = string.Empty;
        string mcNo = string.Empty;

        #endregion

        #region UserControl_Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtMCNo.Text = mcName;
            cmdWarpingSearch.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdWarpingSetting_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 13; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    WarpingSettingPage page = new WarpingSettingPage();
                    page.Setup(logInfo.UserName, mcNo);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdWarpingProcess_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 13; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    WarperMCStatusPage page = new WarperMCStatusPage();
                    page.Setup(logInfo.UserName, mcName, mcNo);

                    PageManager.Instance.Current = page;
                }
            } 
        }

        private void cmdWarpingSearch_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new WarpingSearchPage();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void MCName(string DisplayName,string MCNo)
        {
            if (mcName != null)
            {
                mcName = DisplayName;
            }

            if (mcNo != null)
            {
                mcNo = MCNo;
            }
        }

        #endregion

    }
}
