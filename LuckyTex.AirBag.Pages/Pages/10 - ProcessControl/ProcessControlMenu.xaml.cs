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
    /// Interaction logic for ProcessControlMenu.xaml
    /// </summary>
    public partial class ProcessControlMenu : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ProcessControlMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Button Handlers

        #region cmdEditInspectionReport_Click
        private void cmdEditInspectionReport_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();

            if (logInfo != null)
            {
                int processId = 12; // for inspection
                List<LogInResult> operators = new List<LogInResult>();
                string OperatorId = string.Empty;
                try
                {
                    operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);


                    if (null == operators || operators.Count <= 0)
                    {
                        "Cannot find operator".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        OperatorId = operators[0].OperatorId;
                    }
                }
                catch
                {
                    "Cannot find operator".ShowMessageBox(true);
                    return;
                }

                if (OperatorId != "")
                {
                    ProcessControlPage page = new ProcessControlPage();
                    page.Setup(OperatorId);

                    PageManager.Instance.Current = page;
                   
                }
            }
        }
        #endregion

        #region cmdManualInspection
        private void cmdManualInspection_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();

            if (logInfo != null)
            {
                int processId = 12; // for inspection
                List<LogInResult> operators = new List<LogInResult>();
                string OperatorId = string.Empty;
                try
                {
                    operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);


                    if (null == operators || operators.Count <= 0)
                    {
                        "Cannot find operator".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        OperatorId = operators[0].OperatorId;
                    }
                }
                catch
                {
                    "Cannot find operator".ShowMessageBox(true);
                    return;
                }

                if (OperatorId != "")
                {
                    ManualInspectionPage page = new ManualInspectionPage();
                    page.Setup(OperatorId);

                    PageManager.Instance.Current = page;
                }
            }
        }
        #endregion

        #region cmdSendAS400_Click
        private void cmdSendAS400_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();

            if (logInfo != null)
            {
                int processId = 12; // for inspection
                List<LogInResult> operators = new List<LogInResult>();
                string OperatorId = string.Empty;
                try
                {
                    operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);


                    if (null == operators || operators.Count <= 0)
                    {
                        "Cannot find operator".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        OperatorId = operators[0].OperatorId;
                    }
                }
                catch
                {
                    "Cannot find operator".ShowMessageBox(true);
                    return;
                }

                if (OperatorId != "")
                {
                    SendAS400Page page = new SendAS400Page();
                    page.Setup(OperatorId);

                    PageManager.Instance.Current = page;
                }
            }
        }
        #endregion

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #endregion
    }
}
