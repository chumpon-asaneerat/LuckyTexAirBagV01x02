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
using System.Runtime.InteropServices;
using System.Threading;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for MCSTOPReasonWindow.xaml
    /// </summary>
    public partial class MCSTOPReasonWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public MCSTOPReasonWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables
        private string WARPHEADNO = string.Empty;
        private string WARPERLOT = string.Empty;

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (WARPERLOT != "")
                txtWARPERLOT.Text = WARPERLOT;

            ClearControl();

            if (!string.IsNullOrEmpty(WARPHEADNO) && (!string.IsNullOrEmpty(WARPERLOT)))
                LoadStopReason();
        }

        #region Button Events

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        #endregion

        private void ClearControl()
        {
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarp.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarp.SelectedItems.Clear();
            else
                this.gridWarp.SelectedItem = null;

            gridWarp.ItemsSource = null;
        }

        #region LoadStopReason
        private void LoadStopReason()
        {
            List<WARP_GETSTOPREASONBYWARPERLOT> lots = new List<WARP_GETSTOPREASONBYWARPERLOT>();

            lots = WarpingDataService.Instance.WARP_GETSTOPREASONBYWARPERLOT(WARPHEADNO, WARPERLOT);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridWarp.ItemsSource = lots;
            }
            else
            {
                gridWarp.ItemsSource = null;
            }
        }
        #endregion

        #region Public Properties

        public void Setup(string warpheadNo, string warperLot)
        {
            WARPHEADNO = warpheadNo;
            WARPERLOT = warperLot;
        }

        #endregion

    }
}
