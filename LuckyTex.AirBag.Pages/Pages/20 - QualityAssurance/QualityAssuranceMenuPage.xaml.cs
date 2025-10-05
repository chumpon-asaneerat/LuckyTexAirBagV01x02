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
    /// Interaction logic for QualityAssuranceMenuPage.xaml
    /// </summary>
    public partial class QualityAssuranceMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public QualityAssuranceMenuPage()
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

        private void cmdCheckingAirbagReport_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 12; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    CheckingAirbagReportPage page = new CheckingAirbagReportPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdCheckingReportData_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 12; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    CheckingReportDataPage page = new CheckingReportDataPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdManagement_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 12; // for inspection
                string operators = UserDataService.Instance
                            .GETAUTHORIZEBYPROCESSID(logInfo.UserName, logInfo.Password, processId);

                if (null == operators)
                {
                    "Cannot find operator".ShowMessageBox(true);
                    return;
                }

                if (operators != "")
                {
                    ManagementPage page = new ManagementPage();
                    page.Setup(operators);

                    PageManager.Instance.Current = page;
                }
            }
        }

        #endregion

    }
}
