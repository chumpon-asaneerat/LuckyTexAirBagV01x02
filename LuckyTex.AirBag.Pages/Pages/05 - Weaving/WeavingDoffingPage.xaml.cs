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

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WeavingDoffingPage.xaml
    /// </summary>
    public partial class WeavingDoffingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingDoffingPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private WeavingSession _session = new WeavingSession();
        string opera = string.Empty;
        string strType = string.Empty;
        string MC = string.Empty;
        string PRODUCTTYPEID = string.Empty;

        #endregion

        #region UserControl_Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearInputs();
            WEAV_WEAVINGINPROCESSLIST();

            if (!string.IsNullOrEmpty(opera))
                txtOperator.Text = opera;
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            WEAV_WEAVINGINPROCESSLIST();
        }

        private void cmdDoffing_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MC) && !string.IsNullOrEmpty(strType) && !string.IsNullOrEmpty(PRODUCTTYPEID))
            {
                WeavingProcessPage page = new WeavingProcessPage();
                page.Setup(opera, MC, strType, PRODUCTTYPEID,false,false);

                PageManager.Instance.Current = page;
            }
        }

        #endregion

        #region gridWeaving_SelectedCellsChanged

        #region GetDataGridRows

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

        #endregion

        private void gridWeaving_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWeaving.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWeaving);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            strType = string.Empty;
                            MC = string.Empty;
                            PRODUCTTYPEID = string.Empty;

                            if (((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).MC != null)
                            {
                                MC = ((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).MC;

                                if (MC.Length > 0)
                                    GetstrType(MC.Substring(0, 1));
                            }
                            else
                            {
                                MC = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).PRODUCTTYPEID != null)
                            {
                                PRODUCTTYPEID = ((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).PRODUCTTYPEID;
                            }
                            else
                            {
                                PRODUCTTYPEID = string.Empty;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!string.IsNullOrEmpty(MC) && !string.IsNullOrEmpty(strType) && !string.IsNullOrEmpty(PRODUCTTYPEID))
            {
                WeavingProcessPage page = new WeavingProcessPage();
                page.Setup(opera, MC, strType, PRODUCTTYPEID, false);

                PageManager.Instance.Current = page;
            }
        }

        #region private Methods

        #region ClearInputs

        private void ClearInputs()
        {
            if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWeaving.SelectedItems.Clear();
            else
                this.gridWeaving.SelectedItem = null;

            gridWeaving.ItemsSource = null;
        }

        #endregion

        #region GetstrType

        private void GetstrType(string zone)
        {
            List<WEAV_GETMACHINEZONELIST> results = _session.GetWeav_getMachineZoneList(zone);

            if (results != null)
            {
                strType = results[0].TYPE;
            }
            else
            {
                strType = string.Empty;
            }
        }

        #endregion

        #region WEAV_WEAVINGINPROCESSLIST

        private void WEAV_WEAVINGINPROCESSLIST()
        {
            List<WEAV_WEAVINGINPROCESSLIST> results = WeavingDataService.Instance.WEAV_WEAVINGINPROCESSLIST(null, null);

            if (results != null && results.Count > 0)
            {
                List<LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST> dataList = new List<LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST dataItemNew = new LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST();

                    dataItemNew.BEAMLOT = results[i].BEAMLOT;
                    dataItemNew.MC = results[i].MC;
                    dataItemNew.BARNO = results[i].BARNO;

                    dataItemNew.REEDNO2 = results[i].REEDNO2;
                    dataItemNew.WEFTYARN = results[i].WEFTYARN;
                    dataItemNew.TEMPLETYPE = results[i].TEMPLETYPE;

                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.FINISHDATE = results[i].FINISHDATE;
                    dataItemNew.FINISHFLAG = results[i].FINISHFLAG;
                    dataItemNew.SETTINGBY = results[i].SETTINGBY;
                    dataItemNew.EDITDATE = results[i].EDITDATE;
                    dataItemNew.EDITBY = results[i].EDITBY;
                    dataItemNew.ITM_WEAVING = results[i].ITM_WEAVING;
                    dataItemNew.PRODUCTTYPEID = results[i].PRODUCTTYPEID;
                    dataItemNew.WIDTH = results[i].WIDTH;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridWeaving.ItemsSource = dataList;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWeaving.SelectedItems.Clear();
                else
                    this.gridWeaving.SelectedItem = null;

                gridWeaving.ItemsSource = null;
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user)
        {
            if (opera != null)
            {
                opera = user;
            }

        }

        #endregion 

    }
}
