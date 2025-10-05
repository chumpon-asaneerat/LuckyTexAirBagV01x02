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
    /// Interaction logic for BeamingSpecificMCStopWindow.xaml
    /// </summary>
    public partial class BeamingSpecificMCStopWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public BeamingSpecificMCStopWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string BEAMERNO = string.Empty;
        private string BEAMLOTText = string.Empty;
        private string OperatorText = string.Empty;
        private bool chkSave = false;
        private string ResultText = string.Empty;

        #endregion

        #region Window_Loaded
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadReason();

            if (BEAMLOTText != "")
                txtBEAMLOT.Text = BEAMLOTText;

            //if (OperatorText != "")
            //    txtOperator.Text = OperatorText;

            ClearControl();

            if (!string.IsNullOrEmpty(BEAMERNO) && (!string.IsNullOrEmpty(BEAMLOTText)))
                LoadStopReason();

            txtLength.Focus();
        }
        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLength.Text) && !string.IsNullOrEmpty(txtOperator.Text))
            {
                string reason = string.Empty;
                decimal? length = 0;
                string other = string.Empty;
                string result = string.Empty;
                
                if (cbReason.Text != "")
                    reason = cbReason.Text;
                else
                    reason = txtOther.Text;

                if (!string.IsNullOrEmpty(reason))
                {
                    try
                    {
                        length = decimal.Parse(txtLength.Text);
                    }
                    catch
                    {
                        length = 0;
                    }

                    if (!string.IsNullOrEmpty(txtOther.Text))
                    {
                        other = "1";
                    }
                    else
                    {
                        other = "2";
                    }

                    result = BeamingDataService.Instance.BEAM_INSERTBEAMMCSTOP(BEAMERNO, BEAMLOTText, reason, length, other, txtOperator.Text);

                    if (string.IsNullOrEmpty(result))
                    {
                        ClearControl();

                        if (!string.IsNullOrEmpty(BEAMERNO) && (!string.IsNullOrEmpty(BEAMLOTText)))
                            LoadStopReason();

                        //chkSave = true;
                        //this.DialogResult = true;
                    }
                }
                else
                {
                    MessageBox.Show("Reason or Other isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtLength.Text))
                {
                    MessageBox.Show("Length isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    txtLength.SelectAll();
                    txtLength.Focus();
                    txtLength.Focus();
                }
                else if (string.IsNullOrEmpty(txtOperator.Text))
                {
                    MessageBox.Show("Operator isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    txtOperator.SelectAll();
                    txtOperator.Focus();
                    txtOperator.Focus();
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            chkSave = false;
            this.DialogResult = false;
        }

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtLength_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (cbReason.Text == "")
                {
                    txtOther.SelectAll();
                    txtOther.Focus();
                    txtOther.Focus();
                }
                else
                {
                    txtOperator.SelectAll();
                    txtOperator.Focus();
                    txtOperator.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtOther_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtOperator.SelectAll();
                txtOperator.Focus();
                txtOperator.Focus();

                e.Handled = true;
            }
        }

        private void txtOperator_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region LoadReason

        private void LoadReason()
        {
            if (cbReason.ItemsSource == null)
            {
                string[] str = new string[] { null, "Keba", "Missing Yarn" };

                cbReason.ItemsSource = str;
                cbReason.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadStopReason
        private void LoadStopReason()
        {
            List<BEAM_GETSTOPREASONBYBEAMLOT> lots = new List<BEAM_GETSTOPREASONBYBEAMLOT>();

            lots = BeamingDataService.Instance.BEAM_GETSTOPREASONBYBEAMLOT(BEAMERNO, BEAMLOTText);

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

        #region ClearControl
        private void ClearControl()
        {
            txtLength.Text = string.Empty;
            txtOther.Text = string.Empty;
            cbReason.SelectedIndex = 0;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarp.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarp.SelectedItems.Clear();
            else
                this.gridWarp.SelectedItem = null;

            gridWarp.ItemsSource = null;

            txtLength.SelectAll();
            txtLength.Focus();
        }
        #endregion

        #region Public Properties

        public bool ChkStatus { get { return chkSave; } }
        public string Result { get { return ResultText; } }

       
        public void Setup(string beamerNo, string beamLot, string operatorText)
        {
            BEAMERNO = beamerNo;
            BEAMLOTText = beamLot;
            OperatorText = operatorText;
        }

        #endregion

    }
}
