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
    /// Interaction logic for WeavingHistoryPage.xaml
    /// </summary>
    public partial class WeavingHistoryPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingHistoryPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private LABSession _session = new LABSession();

        string gridWEAVINGLOT = string.Empty;
      
        string opera = string.Empty;
       
        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
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
            string WEAVLOT = string.Empty;

            WEAVLOT = txtWEAVLOT.Text;

            LAB_WEAVINGHISTORY(WEAVLOT);
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #endregion

        #region gridMASSPROSTOCKSTATUS_SelectedCellsChanged

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

        private void gridLAB_WEAVINGHISTORY_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridLAB_WEAVINGHISTORY.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridLAB_WEAVINGHISTORY);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                            gridWEAVINGLOT = string.Empty;

                            if (((LuckyTex.Models.LAB_WEAVINGHISTORY)(gridLAB_WEAVINGHISTORY.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                gridWEAVINGLOT = ((LuckyTex.Models.LAB_WEAVINGHISTORY)(gridLAB_WEAVINGHISTORY.CurrentCell.Item)).WEAVINGLOT;
                            }
                           
                        }
                    }
                }
                else
                {
                    gridWEAVINGLOT = string.Empty;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                gridWEAVINGLOT = string.Empty;
                   
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
            txtWEAVLOT.Text = string.Empty;
          
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLAB_WEAVINGHISTORY.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLAB_WEAVINGHISTORY.SelectedItems.Clear();
            else
                this.gridLAB_WEAVINGHISTORY.SelectedItem = null;

            gridLAB_WEAVINGHISTORY.ItemsSource = null;

            gridWEAVINGLOT = string.Empty;

            txtWEAVLOT.SelectAll();
            txtWEAVLOT.Focus();
        }

        #endregion

        #region LAB_WEAVINGHISTORY

        private void LAB_WEAVINGHISTORY(string P_WEAVINGLOT)
        {
            List<LAB_WEAVINGHISTORY> lots = new List<LAB_WEAVINGHISTORY>();

            lots = _session.LAB_WEAVINGHISTORY(P_WEAVINGLOT);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridLAB_WEAVINGHISTORY.ItemsSource = lots;
            }
            else
            {
                gridLAB_WEAVINGHISTORY.ItemsSource = null;
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

