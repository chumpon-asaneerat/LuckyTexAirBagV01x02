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

#endregion


namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WeavingMenuPage.xaml
    /// </summary>
    public partial class WeavingMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingMenuPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdWeavingProcess_Click(object sender, RoutedEventArgs e)
        {
           PageManager.Instance.Current = new WeavingSettingPage();
        }

        private void cmdDoffing_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 5; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    WeavingDoffingPage page = new WeavingDoffingPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdWeavingData_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 5; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    WeavingPage page = new WeavingPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdWeavingProduction_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 5; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    WeavingProductionPage page = new WeavingProductionPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdShipmentReport_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 5; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    ShipmentReportPage page = new ShipmentReportPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        #endregion
    }
}
