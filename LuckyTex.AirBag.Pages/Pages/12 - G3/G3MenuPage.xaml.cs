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
    /// Interaction logic for WarehouseMenuPage.xaml
    /// </summary>
    public partial class G3MenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public G3MenuPage()
        {
            InitializeComponent();

            cmdAS400.Visibility = System.Windows.Visibility.Collapsed;
            cmdSilicone.Visibility = System.Windows.Visibility.Collapsed;
            cmdFabric.Visibility = System.Windows.Visibility.Collapsed;

        }

        #endregion

        #region Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdAS400_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 1; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {

                    AS400Page page = new AS400Page();
                    page.Setup(logInfo.UserName);
                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdYarn_Click(object sender, RoutedEventArgs e)
        {
            if (rbReceive.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 1; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {

                        ReceiveYARNPage page = new ReceiveYARNPage();
                        page.Setup(logInfo.UserName);
                        PageManager.Instance.Current = page;
                    }
                }
            }
            else if (rbDeliver.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 1; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {

                        IssueRawMaterialPage page = new IssueRawMaterialPage();
                        page.Setup(logInfo.UserName);
                        PageManager.Instance.Current = page;
                    }
                }
            }
            else if (rbCheckStock.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 1; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        PageManager.Instance.Current = new CheckStockYarnPage();
                    }
                }
            }
            else if (rbReceiveReturn.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 1; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {

                        ReceiveReturnMaterialPage page = new ReceiveReturnMaterialPage();
                        page.Setup(logInfo.UserName);
                        PageManager.Instance.Current = page;
                    }
                }
            }
            else if (rbEditIssueRawMaterial.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 1; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {

                        EditIssueRawMaterialPage page = new EditIssueRawMaterialPage();
                        page.Setup(logInfo.UserName);
                        PageManager.Instance.Current = page;
                    }
                }
            }
        }

        private void cmdSilicone_Click(object sender, RoutedEventArgs e)
        {
            if (rbReceive.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    PageManager.Instance.Current = new ReceiveSiliconePage();
                }
            }
            else if (rbDeliver.IsChecked == true)
            {
                "Deliver Silicone is on development.".ShowMessageBox();
            }
        }

        private void cmdFabric_Click(object sender, RoutedEventArgs e)
        {
            if (rbReceive.IsChecked == true)
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    PageManager.Instance.Current = new ReceiveFabricPage();
                }
            }
            else if (rbDeliver.IsChecked == true)
            {
                "Deliver Fabric is on development.".ShowMessageBox();
            }
        }

        #endregion
    }
}
