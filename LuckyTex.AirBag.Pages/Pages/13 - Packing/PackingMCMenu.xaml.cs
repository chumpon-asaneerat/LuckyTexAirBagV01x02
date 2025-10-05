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
    /// Interaction logic for PackingMCMenu.xaml
    /// </summary>
    public partial class PackingMCMenu : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PackingMCMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<PackingMCItem> instList = null;

        #endregion

        #region Private Methods
        
        private void LoadMacines()
        {
            instList = PackingDataService.Instance.GetMachines();
            lstInstMC.ItemsSource = instList;
        }

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMacines();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region ListBox Handler

        private void lstInstMC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstInstMC.SelectedIndex <= -1 && null != instList)
            {
                return;
            }

            if (lstInstMC.SelectedIndex <= instList.Count - 1)
            {
                if (instList[lstInstMC.SelectedIndex].MCId == "1")
                {
                      LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                      if (logInfo != null)
                      {
                          int processId = 10; // for inspection
                          List<LogInResult> operators = UserDataService.Instance
                              .GetOperators(logInfo.UserName, logInfo.Password, processId);

                          if (null == operators || operators.Count <= 0)
                          {
                              "Cannot find operator".ShowMessageBox(true);
                              lstInstMC.SelectedIndex = -1; // reset index.
                              return;
                          }
                          else
                          {
                              string OperatorId = operators[0].OperatorId;

                              PackingLabelPage page = new PackingLabelPage();
                              page.Setup(OperatorId);

                              PageManager.Instance.Current = page;
                          }
                      }
                }
                else if (instList[lstInstMC.SelectedIndex].MCId == "2" || instList[lstInstMC.SelectedIndex].MCId == "3")
                {
                    LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                    if (logInfo != null)
                    {
                        string position = logInfo.Position;

                        int processId = 10;

                        List<LogInResult> operators = UserDataService.Instance
                            .GetOperators(logInfo.UserName, logInfo.Password, processId);

                        if (null == operators || operators.Count <= 0)
                        {
                            "Cannot find operator".ShowMessageBox(true);
                            lstInstMC.SelectedIndex = -1; // reset index.
                            return;
                        }

                        string OperatorId = operators[0].OperatorId;

                        if (OperatorId != "")
                        {
                            PackingSession session = PackingDataService.Instance.GetSession(operators[0]);
                            if (instList[lstInstMC.SelectedIndex].MCId == "2")
                            {
                                PalletSetupPage page = new PalletSetupPage();
                                page.Setup(OperatorId);

                                PageManager.Instance.Current = page;
                            }
                            else if (instList[lstInstMC.SelectedIndex].MCId == "3")
                            {
                                PalletListPage page = new PalletListPage();
                                page.Setup(OperatorId);

                                PageManager.Instance.Current = page;
                            }
                        }
                    }
                }
                else if (instList[lstInstMC.SelectedIndex].MCId == "4")
                {
                    LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                    if (logInfo != null)
                    {
                        string position = logInfo.Position;

                        int processId = 10;

                        string operators = UserDataService.Instance
                            .GETAUTHORIZEBYPROCESSID(logInfo.UserName, logInfo.Password, processId);

                        if (string.IsNullOrEmpty(operators))
                        {
                            "Cannot find operator".ShowMessageBox(true);
                            lstInstMC.SelectedIndex = -1; // reset index.
                            return;
                        }

                        if (!string.IsNullOrEmpty(operators))
                        {
                            PalletEditPage page = new PalletEditPage();
                            page.Setup(operators);

                            PageManager.Instance.Current = page;
                        }
                    }
                }
                else if (instList[lstInstMC.SelectedIndex].MCId == "5")
                {
                    LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                    if (logInfo != null)
                    {
                        string position = logInfo.Position;

                        int processId = 10;

                        string operators = UserDataService.Instance
                            .GETAUTHORIZEBYPROCESSID(logInfo.UserName, logInfo.Password, processId);

                        if (string.IsNullOrEmpty(operators))
                        {
                            "User No Authorize Using this Menu".ShowMessageBox(true);
                            lstInstMC.SelectedIndex = -1; // reset index.
                            return;
                        }

                        if (!string.IsNullOrEmpty(operators))
                        {
                            RePrintLabelPage page = new RePrintLabelPage();
                            page.Setup(operators);

                            PageManager.Instance.Current = page;
                        }
                    }
                }
                else if (instList[lstInstMC.SelectedIndex].MCId == "6")
                {
                    LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                    if (logInfo != null)
                    {
                        int processId = 10; // for inspection
                        List<LogInResult> operators = UserDataService.Instance
                            .GetOperators(logInfo.UserName, logInfo.Password, processId);

                        if (null == operators || operators.Count <= 0)
                        {
                            "Cannot find operator".ShowMessageBox(true);
                            lstInstMC.SelectedIndex = -1; // reset index.
                            return;
                        }
                        else
                        {
                            string OperatorId = operators[0].OperatorId;

                            AutolivLabelPage page = new AutolivLabelPage();
                            page.Setup(OperatorId);

                            PageManager.Instance.Current = page;
                        }
                    }
                }
            }
            else
            {
                //lstInstMC.SelectedIndex = 0;
                this.lstInstMC.SelectedItem = null;
            }
        }

        #endregion
    }
}
