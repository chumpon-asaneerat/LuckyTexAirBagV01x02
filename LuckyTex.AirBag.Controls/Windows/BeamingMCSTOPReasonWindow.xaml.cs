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
    /// Interaction logic for BeamingMCSTOPReasonWindow.xaml
    /// </summary>
    public partial class BeamingMCSTOPReasonWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public BeamingMCSTOPReasonWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables
        private string BEAMERNO = string.Empty;
        private string BEAMLOT = string.Empty;

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (BEAMLOT != "")
                txtBEAMLOT.Text = BEAMLOT;

            ClearControl();

            if (!string.IsNullOrEmpty(BEAMERNO) && (!string.IsNullOrEmpty(BEAMLOT)))
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
            List<BEAM_GETSTOPREASONBYBEAMLOT> lots = new List<BEAM_GETSTOPREASONBYBEAMLOT>();

            lots = BeamingDataService.Instance.BEAM_GETSTOPREASONBYBEAMLOT(BEAMERNO, BEAMLOT);

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

        public void Setup(string beamerNo, string beamLot)
        {
            BEAMERNO = beamerNo;
            BEAMLOT = beamLot;
        }

        #endregion

    }
}
