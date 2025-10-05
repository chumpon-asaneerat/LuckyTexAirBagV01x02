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
    /// Interaction logic for WarpingMenuPage.xaml
    /// </summary>
    public partial class WarpingMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingMenuPage()
        {
            InitializeComponent();

            cmdRequestYarn.Visibility = System.Windows.Visibility.Collapsed;
            cmdReturnYarn.Visibility = System.Windows.Visibility.Collapsed;

            //cmdWarpingList.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdRequestYarn_Click(object sender, RoutedEventArgs e)
        {
             //LogInInfo logInfo = this.ShowLogInBox();
             //if (logInfo != null)
             //{
             //    int processId = 2; // for inspection
             //    List<LogInResult> operators = UserDataService.Instance
             //        .GetOperators(logInfo.UserName, logInfo.Password, processId);

             //    if (null == operators || operators.Count <= 0)
             //    {
             //        "This User can not be Use for This Menu".ShowMessageBox(true);
             //        return;
             //    }
             //    else
             //    {

             //        WarpingRequestYarnPage page = new WarpingRequestYarnPage();
             //        page.Setup(logInfo.UserName);

             //        PageManager.Instance.Current = page;
             //    }
               
             //}
        }

        private void cmdReceiveYarn_Click(object sender, RoutedEventArgs e)
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

                    WarpingReceiveYarnPage page = new WarpingReceiveYarnPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }

            }
        }

        private void cmdReturnYarn_Click(object sender, RoutedEventArgs e)
        {
            //WarpingReturnYarnPage page = new WarpingReturnYarnPage();
            //PageManager.Instance.Current = page;
        }

        private void cmdRemainYarn_Click(object sender, RoutedEventArgs e)
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

                    RemainYarnPage page = new RemainYarnPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }

            }
        }

        private void cmdWarpingProcess_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new WarpingMCMenu();
        }

        private void cmdWarpingRecord_Click(object sender, RoutedEventArgs e)
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

                    WarpingRecordPage page = new WarpingRecordPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdWarpingList_Click(object sender, RoutedEventArgs e)
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

                    WarpingListPage page = new WarpingListPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }


        #endregion

    }
}
