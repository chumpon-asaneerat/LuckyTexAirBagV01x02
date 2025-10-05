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

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for BeamingMCMenu.xaml
    /// </summary>
    public partial class BeamingMCMenu : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingMCMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<BeamingMCItem> instList = null;
        private string status = string.Empty;
        private string Beamerno = string.Empty;
        private string itm_prepare = string.Empty;
        private string mcno = string.Empty;
        private string Createby = string.Empty;
         
        #endregion

        #region Private Methods
        
        private void LoadMacines()
        {
            instList = BeamingDataService.Instance.GetMachinesData();
            lstInstMC.ItemsSource = instList;
        }

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMacines();
            GetCreelSetupStatus();
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
                if (!string.IsNullOrEmpty(instList[lstInstMC.SelectedIndex].MCId))
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
                            List<BEAM_GETBEAMERMCSTATUS> results = null;

                            results = BeamingDataService.Instance.BEAM_GETBEAMERMCSTATUS(instList[lstInstMC.SelectedIndex].MCNo.ToString());

                            if (results == null)
                            {
                                BeamingSetupPage page = new BeamingSetupPage();
                                page.Setup(logInfo.UserName, instList[lstInstMC.SelectedIndex].DisplayName, instList[lstInstMC.SelectedIndex].MCNo.ToString());

                                PageManager.Instance.Current = page;
                            }
                            else
                            {
                                if (results.Count == 0)
                                {
                                    BeamingSetupPage page = new BeamingSetupPage();
                                    page.Setup(logInfo.UserName, instList[lstInstMC.SelectedIndex].DisplayName, instList[lstInstMC.SelectedIndex].MCNo.ToString());

                                    PageManager.Instance.Current = page;
                                }
                                else if (results.Count > 0)
                                {
                                    BeamingProcessPage page = new BeamingProcessPage();
                                    page.Setup(logInfo.UserName, instList[lstInstMC.SelectedIndex].DisplayName, instList[lstInstMC.SelectedIndex].MCNo.ToString(), results[0].BEAMERNO, results[0].ITM_PREPARE, 1, results[0].TOTALBEAM, results[0].NOWARPBEAM);

                                    PageManager.Instance.Current = page;
                                }
                            }
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

        #region Button

        #region cmdRefresh_Click

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetCreelSetupStatus();
        }

        #endregion

        #region cmdEditBeamerMC_Click

        private void cmdEditBeamerMC_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "P")
                {
                    "Beamer Lot Already Start Can not Edit".ShowMessageBox();
                }
                else if (status == "S")
                {
                    LogInInfo logInfo = this.ShowLogInBox();
                    if (logInfo != null)
                    {
                        int processId = 13; // for inspection
                        List<LogInResult> operators = UserDataService.Instance
                            .GetOperators(logInfo.UserName, logInfo.Password, processId);

                        if (null == operators || operators.Count <= 0)
                        {
                            "No Authorize for Edit Length".ShowMessageBox(true);
                            return;
                        }
                        else
                        {
                            BeamerMCEditWindow window = new BeamerMCEditWindow();
                            window.Setup(Beamerno, itm_prepare, mcno, logInfo.UserName);

                            if (window.ShowDialog() == true)
                            {
                                GetCreelSetupStatus();
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region gridBeaming_SelectedCellsChanged

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        private void gridBeaming_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridBeaming.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridBeaming);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            status = string.Empty;
                            Beamerno = string.Empty;
                            itm_prepare = string.Empty;
                            mcno = string.Empty;
                            Createby = string.Empty;

                            if (((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).STATUS != null)
                            {
                                status = ((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).STATUS;
                            }

                            if (((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).BEAMERNO != null)
                            {
                                Beamerno = ((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).BEAMERNO;
                            }

                            if (((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).ITM_PREPARE != null)
                            {
                                itm_prepare = ((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).ITM_PREPARE;
                            }

                            if (((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).MCNO != null)
                            {
                                mcno = ((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).MCNO;
                            }

                            if (((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).CREATEBY != null)
                            {
                                Createby = ((LuckyTex.Models.BEAM_GETBEAMERMCSTATUS)(gridBeaming.CurrentCell.Item)).CREATEBY;
                            }
                        }
                    }
                }
                else
                {
                     status = string.Empty;
                     Beamerno = string.Empty;
                     itm_prepare = string.Empty;
                     mcno = string.Empty;
                     Createby = string.Empty;
                }
            }
            catch (Exception ex)
            {
                status = string.Empty;
                Beamerno = string.Empty;
                itm_prepare = string.Empty;
                mcno = string.Empty;
                Createby = string.Empty;
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Methods

        private void GetCreelSetupStatus()
        {
            status = string.Empty;
            Beamerno = string.Empty;
            itm_prepare = string.Empty;
            mcno = string.Empty;
            Createby = string.Empty;

            List<BEAM_GETBEAMERMCSTATUS> lots = new List<BEAM_GETBEAMERMCSTATUS>();
            lots = BeamingDataService.Instance.GETBEAMERMCSTATUS();

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridBeaming.ItemsSource = lots;
            }
            else
            {
                gridBeaming.ItemsSource = null;
            }
        }

        #endregion
    }
}
