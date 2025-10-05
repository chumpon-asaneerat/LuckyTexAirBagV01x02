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
    /// Interaction logic for BeamingMenuPage.xaml
    /// </summary>
    public partial class BeamingMenuPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingMenuPage()
        {
            InitializeComponent();

            //cmdBeamingList.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdBeamingProcess_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Current = new BeamingMCMenu();
        }

        private void cmdBeamingRecord_Click(object sender, RoutedEventArgs e)
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

                    BeamingRecordPage page = new BeamingRecordPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        private void cmdBeamingList_Click(object sender, RoutedEventArgs e)
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

                    BeamingListPage page = new BeamingListPage();
                    page.Setup(logInfo.UserName);

                    PageManager.Instance.Current = page;
                }
            }
        }

        #endregion
    }
}
