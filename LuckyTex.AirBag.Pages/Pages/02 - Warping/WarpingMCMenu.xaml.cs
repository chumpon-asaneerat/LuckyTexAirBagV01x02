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
    /// Interaction logic for WarpingMCMenu.xaml
    /// </summary>
    public partial class WarpingMCMenu : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingMCMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<WarpingMCItem> instList = null;
        private string STATUS = string.Empty;
        private string WarperLot = string.Empty;
        private string ItemPrepare = string.Empty;
        private string WarperMC = string.Empty;
        private string Side = string.Empty;
         
        #endregion

        #region Private Methods
        
        private void LoadMacines()
        {
            instList = WarpingDataService.Instance.GetMachinesData();
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
                    WarpingProcessMenuPage page = new WarpingProcessMenuPage();
                    page.MCName(instList[lstInstMC.SelectedIndex].DisplayName, instList[lstInstMC.SelectedIndex].MCNo.ToString());

                    PageManager.Instance.Current = page;
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

        #region cmdCancel_Click

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(STATUS))
            {
                if (STATUS == "S")
                {
                    "Warper Lot Already Start Can not Edit Creel Set up".ShowMessageBox();
                }
                else if (STATUS == "C")
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
                            WarperMCEditWindow window = new WarperMCEditWindow();
                            window.Setup(WarperLot, ItemPrepare, WarperMC, Side, logInfo.UserName);

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

        #region gridWarping_SelectedCellsChanged

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

        private void gridWarping_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWarping.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWarping);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            STATUS = string.Empty;
                            WarperLot = string.Empty;
                            ItemPrepare = string.Empty;
                            WarperMC = string.Empty;
                            Side = string.Empty;

                            if (((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).STATUS != null)
                            {
                                STATUS = ((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).STATUS;
                            }

                            if (((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).WARPHEADNO != null)
                            {
                                WarperLot = ((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).WARPHEADNO;
                            }

                            if (((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).ITM_PREPARE != null)
                            {
                                ItemPrepare = ((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).ITM_PREPARE;
                            }

                            if (((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).WARPMC != null)
                            {
                                WarperMC = ((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).WARPMC;
                            }

                            if (((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).SIDE != null)
                            {
                                Side = ((LuckyTex.Models.WARP_GETCREELSETUPSTATUS)(gridWarping.CurrentCell.Item)).SIDE;
                            }
                        }
                    } 
                }
                else
                {
                    STATUS = string.Empty;
                    WarperLot = string.Empty;
                    ItemPrepare = string.Empty;
                    WarperMC = string.Empty;
                    Side = string.Empty;
                }
            }
            catch (Exception ex)
            {
                STATUS = string.Empty;
                WarperLot = string.Empty;
                ItemPrepare = string.Empty;
                WarperMC = string.Empty;
                Side = string.Empty;
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Methods

        private void GetCreelSetupStatus()
        {
            STATUS = string.Empty;
            WarperLot = string.Empty;
            ItemPrepare = string.Empty;
            WarperMC = string.Empty;
            Side = string.Empty;

            List<WARP_GETCREELSETUPSTATUS> lots = new List<WARP_GETCREELSETUPSTATUS>();
            lots = WarpingDataService.Instance.GETCREELSETUPSTATUS();

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridWarping.ItemsSource = lots;
            }
            else
            {
                gridWarping.ItemsSource = null;
            }
        }

        #endregion

    }
}
