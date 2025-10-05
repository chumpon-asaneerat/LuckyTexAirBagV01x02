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

using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using DataControl.ClassData;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for FinishingEditLengthWindow.xaml
    /// </summary>
    public partial class FinishingEditLengthWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public FinishingEditLengthWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string weavLot = string.Empty;
        private string finishLot = string.Empty;
        private string Process = string.Empty;
   
        private string OperatorText = string.Empty;

        decimal? length1 = null;
        decimal? length2 = null;
        decimal? length3 = null;
        decimal? length4 = null;
        decimal? length5 = null;
        decimal? length6 = null;
        decimal? length7 = null;
        decimal? totalLength = null;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (length1 != null)
                txtLENGTH1.Text = length1.Value.ToString("#,##0.##");

            if (length2 != null)
                txtLENGTH2.Text = length2.Value.ToString("#,##0.##");

            if (length3 != null)
                txtLENGTH3.Text = length3.Value.ToString("#,##0.##");

            if (length4 != null)
                txtLENGTH4.Text = length4.Value.ToString("#,##0.##");

            if (length5 != null)
                txtLENGTH5.Text = length5.Value.ToString("#,##0.##");

            if (length6 != null)
                txtLENGTH6.Text = length6.Value.ToString("#,##0.##");

            if (length7 != null)
                txtLENGTH7.Text = length7.Value.ToString("#,##0.##");

            if (totalLength != null)
                txtTOTALLENGTH.Text = totalLength.Value.ToString("#,##0.##");
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            decimal? LENGTH1 = null;
            decimal? LENGTH2 = null;
            decimal? LENGTH3 = null;
            decimal? LENGTH4 = null;
            decimal? LENGTH5 = null;
            decimal? LENGTH6 = null;
            decimal? LENGTH7 = null;
            decimal? TOTALLENGTH = null;

            #region LENGTH1
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH1.Text))
                    LENGTH1 = decimal.Parse(txtLENGTH1.Text);
            }
            catch
            {
                LENGTH1 = 0;
            }
            #endregion

            #region LENGTH2
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH2.Text))
                    LENGTH2 = decimal.Parse(txtLENGTH2.Text);
            }
            catch
            {
                LENGTH2 = 0;
            }
            #endregion

            #region LENGTH3
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH3.Text))
                    LENGTH3 = decimal.Parse(txtLENGTH3.Text);
            }
            catch
            {
                LENGTH3 = 0;
            }
            #endregion

            #region LENGTH4
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH4.Text))
                    LENGTH4 = decimal.Parse(txtLENGTH4.Text);
            }
            catch
            {
                LENGTH4 = 0;
            }
            #endregion

            #region LENGTH5
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH5.Text))
                    LENGTH5 = decimal.Parse(txtLENGTH5.Text);
            }
            catch
            {
                LENGTH5 = 0;
            }
            #endregion

            #region LENGTH6
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH6.Text))
                    LENGTH6 = decimal.Parse(txtLENGTH6.Text);
            }
            catch
            {
                LENGTH6 = 0;
            }
            #endregion

            #region LENGTH7
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH7.Text))
                    LENGTH7 = decimal.Parse(txtLENGTH7.Text);
            }
            catch
            {
                LENGTH7 = 0;
            }
            #endregion

            #region TOTALLENGTH
            try
            {
                if (!string.IsNullOrEmpty(txtTOTALLENGTH.Text))
                    TOTALLENGTH = decimal.Parse(txtTOTALLENGTH.Text);
            }
            catch
            {
                TOTALLENGTH = 0;
            }
            #endregion

            if (LENGTH1 != null)
            {
                if (SaveFinishingEditLength(LENGTH1, LENGTH2, LENGTH3, LENGTH4, LENGTH5, LENGTH6, LENGTH7, TOTALLENGTH) == true)
                {
                    this.DialogResult = true;
                }
                else
                {
                    "Can't Save data".ShowMessageBox(true);
                }
            }
            else
            {
                if (LENGTH1 == null)
                {
                    "Length 1 isn't Null".ShowMessageBox(true);
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
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

        #region txtLENGTH1_KeyDown

        private void txtLENGTH1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH2.Focus();
                txtLENGTH2.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLENGTH2_KeyDown

        private void txtLENGTH2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH3.Focus();
                txtLENGTH3.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLENGTH3_KeyDown

        private void txtLENGTH3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH4.Focus();
                txtLENGTH4.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLENGTH4_KeyDown

        private void txtLENGTH4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH5.Focus();
                txtLENGTH5.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLENGTH5_KeyDown

        private void txtLENGTH5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH6.Focus();
                txtLENGTH6.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLENGTH6_KeyDown

        private void txtLENGTH6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH7.Focus();
                txtLENGTH7.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLENGTH7_KeyDown

        private void txtLENGTH7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        private void txtLENGTH1_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtLENGTH2_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtLENGTH3_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtLENGTH4_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtLENGTH5_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtLENGTH6_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtLENGTH7_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        private void txtTOTALLENGTH_LostFocus(object sender, RoutedEventArgs e)
        {
            CalTotalLength();
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtLENGTH1.Text = string.Empty;
            txtLENGTH2.Text = string.Empty;
            txtLENGTH3.Text = string.Empty;
            txtLENGTH4.Text = string.Empty;
            txtLENGTH5.Text = string.Empty;
            txtLENGTH6.Text = string.Empty;
            txtLENGTH7.Text = string.Empty;
            txtTOTALLENGTH.Text = string.Empty;

            txtLENGTH1.SelectAll();
            txtLENGTH1.Focus();
        }

        #endregion

        #region Private Properties

        #region SaveFinishingEditLength

        private bool SaveFinishingEditLength(decimal? LENGTH1, decimal? LENGTH2, decimal? LENGTH3, decimal? LENGTH4, decimal? LENGTH5, decimal? LENGTH6, decimal? LENGTH7, decimal? TOTALLENGTH)
        {
            try
            {
                if (CoatingDataService.Instance.FINISHING_EDITLENGTH(weavLot, finishLot, Process, OperatorText,  LENGTH1 , LENGTH2 ,LENGTH3 ,LENGTH4 , LENGTH5 , LENGTH6 , LENGTH7 , TOTALLENGTH) == true)
                {
                    return true;
                }
                else
                {
                    "Can't Save FINISHING_SAMPLINGDATA".ShowMessageBox(true);

                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);

                return false;
            }
        }

        #endregion

        #region CalTotalLength

        private void CalTotalLength()
        {
            decimal? LENGTH1 = 0;
            decimal? LENGTH2 = 0;
            decimal? LENGTH3 = 0;
            decimal? LENGTH4 = 0;
            decimal? LENGTH5 = 0;
            decimal? LENGTH6 = 0;
            decimal? LENGTH7 = 0;
            decimal? TOTALLENGTH = 0;

            #region LENGTH1
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH1.Text))
                    LENGTH1 = decimal.Parse(txtLENGTH1.Text);
            }
            catch
            {
                LENGTH1 = 0;
            }
            #endregion

            #region LENGTH2
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH2.Text))
                    LENGTH2 = decimal.Parse(txtLENGTH2.Text);
            }
            catch
            {
                LENGTH2 = 0;
            }
            #endregion

            #region LENGTH3
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH3.Text))
                    LENGTH3 = decimal.Parse(txtLENGTH3.Text);
            }
            catch
            {
                LENGTH3 = 0;
            }
            #endregion

            #region LENGTH4
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH4.Text))
                    LENGTH4 = decimal.Parse(txtLENGTH4.Text);
            }
            catch
            {
                LENGTH4 = 0;
            }
            #endregion

            #region LENGTH5
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH5.Text))
                    LENGTH5 = decimal.Parse(txtLENGTH5.Text);
            }
            catch
            {
                LENGTH5 = 0;
            }
            #endregion

            #region LENGTH6
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH6.Text))
                    LENGTH6 = decimal.Parse(txtLENGTH6.Text);
            }
            catch
            {
                LENGTH6 = 0;
            }
            #endregion

            #region LENGTH7
            try
            {
                if (!string.IsNullOrEmpty(txtLENGTH7.Text))
                    LENGTH7 = decimal.Parse(txtLENGTH7.Text);
            }
            catch
            {
                LENGTH7 = 0;
            }
            #endregion

            #region TOTALLENGTH

            TOTALLENGTH = LENGTH1 + LENGTH2 + LENGTH3 + LENGTH4 + LENGTH5 + LENGTH6 + LENGTH7;

            if (TOTALLENGTH != null)
                txtTOTALLENGTH.Text = TOTALLENGTH.Value.ToString();

            #endregion
        }

        #endregion

        #endregion

        #region Public Properties

        public void Setup(string P_WEAVLOT, string P_FINISHLOT, string P_PROCESS
            , string P_OPERATORID, decimal? LENGTH1, decimal? LENGTH2, decimal? LENGTH3, decimal? LENGTH4, decimal? LENGTH5, decimal? LENGTH6, decimal? LENGTH7, decimal? TOTALLENGTH)
        {
            weavLot = P_WEAVLOT;
            finishLot = P_FINISHLOT;
            Process = P_PROCESS;
            OperatorText = P_OPERATORID;
            length1 = LENGTH1;
            length2 = LENGTH2;
            length3 = LENGTH3;
            length4 = LENGTH4;
            length5 = LENGTH5;
            length6 = LENGTH6;
            length7 = LENGTH7;
            totalLength = TOTALLENGTH;
        }

        #endregion

    }
}
