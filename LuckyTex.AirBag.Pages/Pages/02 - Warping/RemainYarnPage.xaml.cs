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
    /// Interaction logic for RemainYarnPage.xaml
    /// </summary>
    public partial class RemainYarnPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RemainYarnPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private WarpingSession _session = new WarpingSession();

        string palletNo = string.Empty;
        DateTime? receiveDate = null;
        string opera = string.Empty;
       
        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItemGood();

            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            if (chkAll.IsChecked == false)
            {
                if (cbItemCode.SelectedItem != null)
                    WARP_GETREMAINPALLET(cbItemCode.Text);
            }
            else
            {
                WARP_GETREMAINPALLET("");
            }
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(palletNo) && receiveDate != null && !string.IsNullOrEmpty(opera))
            {
                ClearPallet cp = this.ShowClearPalletBox(palletNo, receiveDate, opera);

                if (cp != null)
                {
                    if (cp.ChkStatus == true)
                    {
                        if (chkAll.IsChecked == false)
                        {
                            if (cbItemCode.SelectedItem != null)
                                WARP_GETREMAINPALLET(cbItemCode.Text);
                        }
                        else
                        {
                            WARP_GETREMAINPALLET("");
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region gridLAB_SelectedCellsChanged

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
                            palletNo = string.Empty;
                            receiveDate = null;

                            if (((LuckyTex.Models.WARP_GETREMAINPALLET)(gridWarping.CurrentCell.Item)).PALLETNO != null)
                            {
                                palletNo = ((LuckyTex.Models.WARP_GETREMAINPALLET)(gridWarping.CurrentCell.Item)).PALLETNO;
                            }

                            if (((LuckyTex.Models.WARP_GETREMAINPALLET)(gridWarping.CurrentCell.Item)).RECEIVEDATE != null)
                            {
                                receiveDate = ((LuckyTex.Models.WARP_GETREMAINPALLET)(gridWarping.CurrentCell.Item)).RECEIVEDATE;
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

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            cbItemCode.SelectedValue = null;
            chkAll.IsChecked = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarping.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarping.SelectedItems.Clear();
            else
                this.gridWarping.SelectedItem = null;

            gridWarping.ItemsSource = null;
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMYARNLIST> items = _session.GetItemCodeYarnData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_YARN";
                this.cbItemCode.SelectedValuePath = "ITM_YARN";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region WARP_GETREMAINPALLET

        private void WARP_GETREMAINPALLET(string P_ITEM_YARN)
        {
            List<WARP_GETREMAINPALLET> lots = new List<WARP_GETREMAINPALLET>();

            lots = WarpingDataService.Instance.WARP_GETREMAINPALLET(P_ITEM_YARN);

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

