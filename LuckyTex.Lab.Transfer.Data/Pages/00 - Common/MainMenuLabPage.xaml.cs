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
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuLabPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainMenuLabPage()
        {
            InitializeComponent();
            cmdSampleReport.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Button Events

        #region cmdPDFLoading_Click
        private void cmdPDFLoading_Click(object sender, RoutedEventArgs e)
        {
            PDFLoadingMenuPage page = new PDFLoadingMenuPage();
            PageManager.Instance.Current = page;
        }
        #endregion

        #region cmdLabDataEntry_Click

        private void cmdLabDataEntry_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 9; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    LabDataEntryPage page = new LabDataEntryPage();
                    page.Setup(logInfo.UserName, operators[0].PositionLevel);

                    PageManager.Instance.Current = page;
                }
            }
        }

        #endregion

        #region cmdProduction_Click
        private void cmdProduction_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 9; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    ProductionPage page = new ProductionPage();
                    page.Setup(logInfo.UserName, operators[0].PositionLevel);

                    PageManager.Instance.Current = page;
                }
            }
        }
        #endregion

        #region cmdItemCodeSpecification_Click
        private void cmdItemCodeSpecification_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 9; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    ItemCodeSpecificationPage page = new ItemCodeSpecificationPage();
                    page.Setup(logInfo.UserName, operators[0].PositionLevel);

                    PageManager.Instance.Current = page;
                }
            }
        }
        #endregion

        #region cmdSampleTestData_Click
        private void cmdSampleTestData_Click(object sender, RoutedEventArgs e)
        {
            SampleTestDataMenuPage page = new SampleTestDataMenuPage();
            PageManager.Instance.Current = page;
        }
        #endregion

        #region cmdPLCGetData_Click
        private void cmdPLCGetData_Click(object sender, RoutedEventArgs e)
        {
            PLCGetDataMenuPage page = new PLCGetDataMenuPage();
            PageManager.Instance.Current = page;
        }
        #endregion

        #region cmdUploadLABReport_Click
        private void cmdUploadLABReport_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 9; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    UploadLABReportPage page = new UploadLABReportPage();
                    page.Setup(logInfo.UserName, operators[0].PositionLevel);

                    PageManager.Instance.Current = page;
                }
            }
        }
        #endregion

        #region cmdLABReportSetting_Click
        private void cmdLABReportSetting_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 9; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    LABReportSettingPage page = new LABReportSettingPage();
                    page.Setup(logInfo.UserName, operators[0].PositionLevel);

                    PageManager.Instance.Current = page;
                }
            }
        }
        #endregion

        #region cmdSampleReport_Click
        private void cmdSampleReport_Click(object sender, RoutedEventArgs e)
        {
            SampleReportPage page = new SampleReportPage();
            PageManager.Instance.Current = page;
        }
        #endregion

        #endregion

    }
}
