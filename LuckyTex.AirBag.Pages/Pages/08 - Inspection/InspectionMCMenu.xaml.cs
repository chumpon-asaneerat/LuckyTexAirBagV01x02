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
    /// Interaction logic for InspectionMCMenu.xaml
    /// </summary>
    public partial class InspectionMCMenu : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public InspectionMCMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<InspectionMCItem> instList = null;

        #endregion

        #region Private Methods
        
        private void LoadMacines()
        {
            instList = InspectionDataService.Instance.GetMachines();
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
            //Console.WriteLine("Unload INST M/C Selection page.");
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
            try
            {
                if (lstInstMC.SelectedIndex <= -1 && null != instList)
                {
                    return;
                }

                LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                if (logInfo != null)
                {
                    int processId = 8; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "Cannot find operator".ShowMessageBox(true);
                        lstInstMC.SelectedIndex = -1; // reset index.
                        return;
                    }

                    if (lstInstMC.SelectedIndex < instList.Count - 2)
                    {
                        List<Domains.INS_GETMCSUSPENDDATAResult> results =
                            InspectionDataService.Instance.GetSuspendInspectionProcess(
                            instList[lstInstMC.SelectedIndex].MCId);

                        Domains.INS_GETMCSUSPENDDATAResult result = null;

                        InspectionSession session = InspectionDataService.Instance.GetSession(
                            instList[lstInstMC.SelectedIndex], operators[0]);

                        if (null != results && results.Count > 0)
                        {
                            result = results[0];
                        }

                        InspectionModulePage page = new InspectionModulePage();
                        page.Setup(session, result);

                        PageManager.Instance.Current = page;
                    }
                    else if (lstInstMC.SelectedIndex == instList.Count - 2)
                    {
                        SearchInspectionDataPage page = new SearchInspectionDataPage();
                        PageManager.Instance.Current = page;
                    }
                    else if (lstInstMC.SelectedIndex == instList.Count - 1)
                    {
                        //DataControl.ClassData.ConmonReportService.Instance.OperatorID = string.Empty;

                        WeightMeasurementPage page = new WeightMeasurementPage();
                        page.Setup(logInfo.UserName);
                        PageManager.Instance.Current = page;
                    }
                }
                else
                {
                    //lstInstMC.SelectedIndex = 0;
                    this.lstInstMC.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion
    }
}
