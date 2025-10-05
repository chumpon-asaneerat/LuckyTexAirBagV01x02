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
    public partial class MainMenuPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MainMenuPage()
        {
            InitializeComponent();
            EnabledButton();

            cmdFinishingTest.Visibility = System.Windows.Visibility.Collapsed;
            cmdReport2.Visibility = System.Windows.Visibility.Collapsed;
            cmdSilicone.Visibility = System.Windows.Visibility.Collapsed;
            cmdUserManagement.Visibility = System.Windows.Visibility.Collapsed;
            cmdOptional.Visibility = System.Windows.Visibility.Collapsed;
            cmdApprove.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Button Events

        private void cmdWarehouse_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new G3MenuPage();
        }

        private void cmdWarping_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new WarpingMenuPage();
        }

        private void cmdBeaming_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new BeamingMenuPage();
        }

        private void cmdDrawing_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new DrawingMenuPage();
        }

        private void cmdWeaving_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new WeavingMenuPage();
        }

        private void cmdProcessControl_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new ProcessControlMenu();
        }

        private void cmdFinishingTest_Click(object sender, RoutedEventArgs e)
        {
            //PageManager.Instance.Current = new FinishingModulePage();
            PageManager.Instance.Current = new FinishingTestPage(); // Temp page
        }

        private void cmdFinishing_Click(object sender, RoutedEventArgs e)
        {
            //PageManager.Instance.Current = new FinishingModulePage();
            PageManager.Instance.Current = new FinishingMCMenu();
        }

        private void cmdCutAndPrint_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new CutPrintMCMenu();
        }

        private void cmdInspection_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new InspectionMCMenu();
        }

        private void cmdPacking_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new PackingMCMenu();
            //"Packing Menu is still on development".ShowMessageBox(true);
        }

        private void cmdLAB_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new LABMenuPage();
        }

        private void cmdApprove_Click(object sender, RoutedEventArgs e)
        {
            //PageManager.Instance.Current = new ApproveMenuPage();
        }

        private void cmdCustomerAndLoadingType_Click(object sender, RoutedEventArgs e)
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
                    CustomerLoadingPage page = new CustomerLoadingPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdSilicone_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmdProcessCondition_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new ProcessConditionMenu();
        }

        private void cmdQualityAssurance_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new QualityAssuranceMenuPage();
        }

        private void cmd100MRecord_Click(object sender, RoutedEventArgs e)
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
                    HundredMRecordPage page = new HundredMRecordPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdDefectCode_Click(object sender, RoutedEventArgs e)
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
                    DefectCodePage page = new DefectCodePage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdItemCode_Click(object sender, RoutedEventArgs e)
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
                    ItemCodePage page = new ItemCodePage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdOperator_Click(object sender, RoutedEventArgs e)
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

                     OperatorPage page = new OperatorPage();
                     page.Setup(logInfo.UserName);

                     PageManager.Instance.Current = page;
                 }
             }
        }

        private void cmdUserManagement_Click(object sender, RoutedEventArgs e)
        {
            "User Management Menu is still on development".ShowMessageBox(true);
        }

        #endregion

        #region EnabledButton

        private void EnabledButton()
        {
            cmdWarehouse.IsEnabled = true;
            cmdWarping.IsEnabled = true;
            cmdBeaming.IsEnabled = true;
            cmdDrawing.IsEnabled = true;
            cmdWeaving.IsEnabled = true;

            cmdFinishingTest.Visibility = System.Windows.Visibility.Collapsed;
            cmdFinishingTest.IsEnabled = false;

            cmdFinishing.IsEnabled = true;
            cmdCutAndPrint.IsEnabled = true;
            cmdInspection.IsEnabled = true;
            cmdPacking.IsEnabled = true;
            cmdLAB.IsEnabled = true;
            cmdApprove.IsEnabled = false;
            cmdReport2.IsEnabled = true;
            cmdReport2.Visibility = System.Windows.Visibility.Hidden;
            cmdProcessControl.IsEnabled = true;

            cmdCustomerAndLoadingType.IsEnabled = true;
            cmdSilicone.IsEnabled = false;

            cmdProcessCondition.IsEnabled = true;
            cmdDefectCode.IsEnabled = true;
            cmdOperator.IsEnabled = true;
            cmdUserManagement.IsEnabled = false;
            cmdOptional.IsEnabled = false;

            cmdItemCode.IsEnabled = true;
        }

        #endregion
    }
}
