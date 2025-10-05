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
    /// Interaction logic for FinishingTestPage.xaml
    /// </summary>
    public partial class FinishingTestPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingTestPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods
        
        private void ClearInputs()
        {
            txtBatchNo.Text = string.Empty;
            txtItemGrey.Text = string.Empty;
            txtLot.Text = string.Empty;
            txtLength.Text = string.Empty;

            txtBatchNo.Focus();
            txtBatchNo.SelectAll();
        }

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Clear inputs and set focus to first control.
            ClearInputs();
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

        #region Controls Handlers

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            // Save data.
            string itemCode = txtItemGrey.Text;
            string LotNo = txtLot.Text;
            decimal length = Convert.ToDecimal(txtLength.Text);
            //string partNo = string.Empty;
            string partNo = txtBatchNo.Text;

            if (!FinishingDataService.Instance.SaveFinishingData(
                itemCode, LotNo, length, partNo))
            {
                // Some input is invalid.
            }

            ClearInputs();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtItemGrey.Focus();
                txtItemGrey.SelectAll();
                e.Handled = true;
            }
        }

        private void txtItemGrey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLot.Focus();
                txtLot.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength.Focus();
                txtLength.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLength_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBatchNo.Focus();
                txtBatchNo.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        
    }
}
