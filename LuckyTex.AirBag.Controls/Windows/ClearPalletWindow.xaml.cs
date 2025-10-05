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
    /// Interaction logic for ClearPalletWindow.xaml
    /// </summary>
    public partial class ClearPalletWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public ClearPalletWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string PALLETNO = string.Empty;
        private DateTime? RECEIVEDATE = null;
        private string OperatorText = string.Empty;

        private bool chkSave = false;

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (PALLETNO != "")
                txtPALLETNO.Text = PALLETNO;


            ClearControl();

            txtRemark.Focus();
        }

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPALLETNO.Text) && !string.IsNullOrEmpty(txtRemark.Text))
            {
                if (WarpingDataService.Instance.WARP_CLEARPALLET(PALLETNO, RECEIVEDATE, OperatorText, txtRemark.Text) == true)
                {
                    chkSave = true;
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Can't Clear Pallet", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtRemark.Text))
                {
                    MessageBox.Show("Remark isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    txtRemark.SelectAll();
                    txtRemark.Focus();
                    txtRemark.Focus();
                }
                else if (string.IsNullOrEmpty(txtPALLETNO.Text))
                {
                    MessageBox.Show("Pallet No isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    txtPALLETNO.SelectAll();
                    txtPALLETNO.Focus();
                    txtPALLETNO.Focus();
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            chkSave = false;
            this.DialogResult = false;
        }

        #endregion

        private void ClearControl()
        {
            txtRemark.Text = string.Empty;

            txtRemark.SelectAll();
            txtRemark.Focus();
        }

        #region Public Properties

        public bool ChkStatus { get { return chkSave; } }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="warpheadNo"></param>
        /// <param name="WarperLot"></param>
        /// <param name="operatorText"></param>
        public void Setup(string palletNo ,DateTime? receiveDate  , string operatorText)
        {
            RECEIVEDATE = receiveDate;
            PALLETNO = palletNo;
            OperatorText = operatorText;
        }

        #endregion

    }
}
