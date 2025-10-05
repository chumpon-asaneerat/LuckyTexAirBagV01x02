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

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for RetestUncoatedWeightWindow.xaml
    /// </summary>
    public partial class RetestUncoatedWeightWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetestUncoatedWeightWindow()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            cmdRetest.Visibility = Visibility.Collapsed;

            ReadOnly();
            ClearControl();
        }

        #endregion

        #region Internal Variables

        string ITM_CODE = string.Empty;
        string WEAVINGLOT = string.Empty;
        string FINISHINGLOT = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            txtITMCODE.Text = ITM_CODE;
            txtWEAVINGLOG.Text = WEAVINGLOT;
            txtFINISHINGLOT.Text = FINISHINGLOT;
          
            LoadItemTestProperty(txtITMCODE.Text);
            LoadItemTestSpecification(txtITMCODE.Text);
            LAB_GETWEIGHTDATA(txtITMCODE.Text, txtWEAVINGLOG.Text, "UW");

            txtSaveLabUNCOATEDWEIGHTN1.Focus();
            txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #endregion

        #region cmdSelect_Click
        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            if (Select() == true)
            {
                this.DialogResult = true;
            }
            else
            {
                if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN1.Text) && txtSaveLabUNCOATEDWEIGHTN1.IsVisible == true)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN2.Text) && txtSaveLabUNCOATEDWEIGHTN2.IsVisible == true)
                {
                    txtSaveLabUNCOATEDWEIGHTN2.Focus();
                    txtSaveLabUNCOATEDWEIGHTN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN3.Text) && txtSaveLabUNCOATEDWEIGHTN3.IsVisible == true)
                {
                    txtSaveLabUNCOATEDWEIGHTN3.Focus();
                    txtSaveLabUNCOATEDWEIGHTN3.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN4.Text) && txtSaveLabUNCOATEDWEIGHTN4.IsVisible == true)
                {
                    txtSaveLabUNCOATEDWEIGHTN4.Focus();
                    txtSaveLabUNCOATEDWEIGHTN4.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN5.Text) && txtSaveLabUNCOATEDWEIGHTN5.IsVisible == true)
                {
                    txtSaveLabUNCOATEDWEIGHTN5.Focus();
                    txtSaveLabUNCOATEDWEIGHTN5.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN6.Text) && txtSaveLabUNCOATEDWEIGHTN6.IsVisible == true)
                {
                    txtSaveLabUNCOATEDWEIGHTN6.Focus();
                    txtSaveLabUNCOATEDWEIGHTN6.SelectAll();
                }
            }
        }
        #endregion

        #endregion

        #region TextBox Handlers

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region General

        #region txtITMCODE_KeyDown
        private void txtITMCODE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWEAVINGLOG.Focus();
                txtWEAVINGLOG.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWEAVINGLOG_KeyDown
        private void txtWEAVINGLOG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFINISHINGLOT.Focus();
                txtFINISHINGLOT.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFINISHINGLOT_KeyDown
        private void txtFINISHINGLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUNCOATEDWEIGHTN1.IsEnabled == true)
                {
                    txtUNCOATEDWEIGHTN1.Focus();
                    txtUNCOATEDWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region Item Property

        #region KeyDown

        #region UNCOATEDWEIGHT
        private void txtUNCOATEDWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUNCOATEDWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtUNCOATEDWEIGHTN2.Focus();
                    txtUNCOATEDWEIGHTN2.SelectAll();
                }
                else if (txtRetest1UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Focus();
                    txtRetest1UNCOATEDWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUNCOATEDWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtUNCOATEDWEIGHTN3.Focus();
                    txtUNCOATEDWEIGHTN3.SelectAll();
                }
                else if (txtRetest1UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Focus();
                    txtRetest1UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUNCOATEDWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtUNCOATEDWEIGHTN4.Focus();
                    txtUNCOATEDWEIGHTN4.SelectAll();
                }
                else if (txtRetest1UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Focus();
                    txtRetest1UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUNCOATEDWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtUNCOATEDWEIGHTN5.Focus();
                    txtUNCOATEDWEIGHTN5.SelectAll();
                }
                else if (txtRetest1UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Focus();
                    txtRetest1UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUNCOATEDWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtUNCOATEDWEIGHTN6.Focus();
                    txtUNCOATEDWEIGHTN6.SelectAll();
                }
                else if (txtRetest1UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Focus();
                    txtRetest1UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Focus();
                    txtRetest1UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 UNCOATEDWEIGHT
        private void txtRetest1UNCOATEDWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1UNCOATEDWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN2.Focus();
                    txtRetest1UNCOATEDWEIGHTN2.SelectAll();
                }
                else if (txtRetest2UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Focus();
                    txtRetest2UNCOATEDWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1UNCOATEDWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1UNCOATEDWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN3.Focus();
                    txtRetest1UNCOATEDWEIGHTN3.SelectAll();
                }
                else if (txtRetest2UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Focus();
                    txtRetest2UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1UNCOATEDWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1UNCOATEDWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN4.Focus();
                    txtRetest1UNCOATEDWEIGHTN4.SelectAll();
                }
                else if (txtRetest2UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Focus();
                    txtRetest2UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1UNCOATEDWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1UNCOATEDWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN5.Focus();
                    txtRetest1UNCOATEDWEIGHTN5.SelectAll();
                }
                else if (txtRetest2UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Focus();
                    txtRetest2UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1UNCOATEDWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1UNCOATEDWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtRetest1UNCOATEDWEIGHTN6.Focus();
                    txtRetest1UNCOATEDWEIGHTN6.SelectAll();
                }
                else if (txtRetest2UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Focus();
                    txtRetest2UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1UNCOATEDWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2UNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Focus();
                    txtRetest2UNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 UNCOATEDWEIGHT
        private void txtRetest2UNCOATEDWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2UNCOATEDWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN2.Focus();
                    txtRetest2UNCOATEDWEIGHTN2.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2UNCOATEDWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2UNCOATEDWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN3.Focus();
                    txtRetest2UNCOATEDWEIGHTN3.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2UNCOATEDWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2UNCOATEDWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN4.Focus();
                    txtRetest2UNCOATEDWEIGHTN4.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2UNCOATEDWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2UNCOATEDWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN5.Focus();
                    txtRetest2UNCOATEDWEIGHTN5.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2UNCOATEDWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2UNCOATEDWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtRetest2UNCOATEDWEIGHTN6.Focus();
                    txtRetest2UNCOATEDWEIGHTN6.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2UNCOATEDWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Focus();
                    txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab UNCOATEDWEIGHT
        private void txtSaveLabUNCOATEDWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN2.Focus();
                    txtSaveLabUNCOATEDWEIGHTN2.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabUNCOATEDWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN3.Focus();
                    txtSaveLabUNCOATEDWEIGHTN3.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabUNCOATEDWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN4.Focus();
                    txtSaveLabUNCOATEDWEIGHTN4.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabUNCOATEDWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN5.Focus();
                    txtSaveLabUNCOATEDWEIGHTN5.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabUNCOATEDWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabUNCOATEDWEIGHTN6.Focus();
                    txtSaveLabUNCOATEDWEIGHTN6.SelectAll();
                }
                else if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabUNCOATEDWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabUNCOATEDWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        // Lost Focus
        #region LostFocus

        #region UNCOATEDWEIGHT_LostFocus
        private void UNCOATEDWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            UNCOATEDWEIGHT();
        }
        #endregion

        #region Retest1UNCOATEDWEIGHT_LostFocus
        private void Retest1UNCOATEDWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1UNCOATEDWEIGHT();
        }
        #endregion

        #region Retest2UNCOATEDWEIGHT_LostFocus
        private void Retest2UNCOATEDWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2UNCOATEDWEIGHT();
        }
        #endregion

        #region SaveLabUNCOATEDWEIGHT_LostFocus
        private void SaveLabUNCOATEDWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabUNCOATEDWEIGHT();
        }
        #endregion

        #region UNCOATEDWEIGHT
        private void UNCOATEDWEIGHT()
        {
            try
            {
                int? i = 0;
                decimal value;

                decimal? ave1 = null;
                decimal? ave2 = null;
                decimal? ave3 = null;
                decimal? ave4 = null;
                decimal? ave5 = null;
                decimal? ave6 = null;

                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtUNCOATEDWEIGHTN1.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtUNCOATEDWEIGHTN2.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtUNCOATEDWEIGHTN3.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtUNCOATEDWEIGHTN4.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtUNCOATEDWEIGHTN5.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtUNCOATEDWEIGHTN6.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTN6.Text = string.Empty;
                    }
                }

                decimal? Avg = 0;

                #region New

                if (ave1 == null)
                    ave1 = 0;
                else
                    i++;

                if (ave2 == null)
                    ave2 = 0;
                else
                    i++;

                if (ave3 == null)
                    ave3 = 0;
                else
                    i++;

                if (ave4 == null)
                    ave4 = 0;
                else
                    i++;

                if (ave5 == null)
                    ave5 = 0;
                else
                    i++;

                if (ave6 == null)
                    ave6 = 0;
                else
                    i++;

                #endregion

                if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                {
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);

                    txtUNCOATEDWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtUNCOATEDWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTSpecification.Text))
                {
                    string temp = txtUNCOATEDWEIGHTSpecification.Text;

                    decimal? num = null;
                    decimal? num2 = null;
                    decimal? num3 = null;

                    decimal? lower = null;
                    decimal? upper = null;

                    String strString = temp.Substring(0, temp.Length).Trim();
                    strString = strString.Replace(" ", "&").TrimEnd();
                    String[] myArr = strString.Split('&');

                    if (myArr.Length > 1)
                    {
                        if (myArr[1] != null)
                        {
                            if (temp.Contains("MAX"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num < ave1)
                                        txtUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("+/-"))
                            {
                                if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[0].ToString().Trim());

                                if (Decimal.TryParse(myArr[2].ToString().Trim(), out value))
                                    num2 = decimal.Parse(myArr[2].ToString().Trim());

                                lower = num - num2;
                                upper = num + num2;

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (lower <= ave1 && ave1 <= upper)
                                        txtUNCOATEDWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtUNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtUNCOATEDWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtUNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtUNCOATEDWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtUNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtUNCOATEDWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtUNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtUNCOATEDWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtUNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                        }
                    }
                    else
                    {
                        if (myArr.Length == 1)
                        {
                            if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                num3 = decimal.Parse(myArr[0].ToString().Trim());
                        }

                        if (ave1 != null && ave1 != 0)
                        {
                            if (ave1 == num3)
                                txtUNCOATEDWEIGHTN1.Background = Brushes.White;
                            else
                                txtUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtUNCOATEDWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtUNCOATEDWEIGHTN2.Background = Brushes.White;
                            else
                                txtUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtUNCOATEDWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtUNCOATEDWEIGHTN3.Background = Brushes.White;
                            else
                                txtUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtUNCOATEDWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtUNCOATEDWEIGHTN4.Background = Brushes.White;
                            else
                                txtUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtUNCOATEDWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtUNCOATEDWEIGHTN5.Background = Brushes.White;
                            else
                                txtUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtUNCOATEDWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                            else
                                txtUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtUNCOATEDWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtUNCOATEDWEIGHTN1.Background = Brushes.White;
                    txtUNCOATEDWEIGHTN2.Background = Brushes.White;
                    txtUNCOATEDWEIGHTN3.Background = Brushes.White;
                    txtUNCOATEDWEIGHTN4.Background = Brushes.White;
                    txtUNCOATEDWEIGHTN5.Background = Brushes.White;
                    txtUNCOATEDWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1UNCOATEDWEIGHT
        private void Retest1UNCOATEDWEIGHT()
        {
            try
            {
                int? i = 0;
                decimal value;

                decimal? ave1 = null;
                decimal? ave2 = null;
                decimal? ave3 = null;
                decimal? ave4 = null;
                decimal? ave5 = null;
                decimal? ave6 = null;

                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1UNCOATEDWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1UNCOATEDWEIGHTN1.Text);
                    }
                    else
                    {
                        txtRetest1UNCOATEDWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1UNCOATEDWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1UNCOATEDWEIGHTN2.Text);
                    }
                    else
                    {
                        txtRetest1UNCOATEDWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1UNCOATEDWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1UNCOATEDWEIGHTN3.Text);
                    }
                    else
                    {
                        txtRetest1UNCOATEDWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1UNCOATEDWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest1UNCOATEDWEIGHTN4.Text);
                    }
                    else
                    {
                        txtRetest1UNCOATEDWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1UNCOATEDWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest1UNCOATEDWEIGHTN5.Text);
                    }
                    else
                    {
                        txtRetest1UNCOATEDWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1UNCOATEDWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest1UNCOATEDWEIGHTN6.Text);
                    }
                    else
                    {
                        txtRetest1UNCOATEDWEIGHTN6.Text = string.Empty;
                    }
                }

                decimal? Avg = 0;

                #region New

                if (ave1 == null)
                    ave1 = 0;
                else
                    i++;

                if (ave2 == null)
                    ave2 = 0;
                else
                    i++;

                if (ave3 == null)
                    ave3 = 0;
                else
                    i++;

                if (ave4 == null)
                    ave4 = 0;
                else
                    i++;

                if (ave5 == null)
                    ave5 = 0;
                else
                    i++;

                if (ave6 == null)
                    ave6 = 0;
                else
                    i++;

                #endregion

                if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                {
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);

                    txtRetest1UNCOATEDWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1UNCOATEDWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1UNCOATEDWEIGHTSpecification.Text))
                {
                    string temp = txtRetest1UNCOATEDWEIGHTSpecification.Text;

                    decimal? num = null;
                    decimal? num2 = null;
                    decimal? num3 = null;

                    decimal? lower = null;
                    decimal? upper = null;

                    String strString = temp.Substring(0, temp.Length).Trim();
                    strString = strString.Replace(" ", "&").TrimEnd();
                    String[] myArr = strString.Split('&');

                    if (myArr.Length > 1)
                    {
                        if (myArr[1] != null)
                        {
                            if (temp.Contains("MAX"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num < ave1)
                                        txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("+/-"))
                            {
                                if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[0].ToString().Trim());

                                if (Decimal.TryParse(myArr[2].ToString().Trim(), out value))
                                    num2 = decimal.Parse(myArr[2].ToString().Trim());

                                lower = num - num2;
                                upper = num + num2;

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (lower <= ave1 && ave1 <= upper)
                                        txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                        }
                    }
                    else
                    {
                        if (myArr.Length == 1)
                        {
                            if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                num3 = decimal.Parse(myArr[0].ToString().Trim());
                        }

                        if (ave1 != null && ave1 != 0)
                        {
                            if (ave1 == num3)
                                txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;
                            else
                                txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;
                            else
                                txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;
                            else
                                txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;
                            else
                                txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;
                            else
                                txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                            else
                                txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;
                    txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;
                    txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;
                    txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;
                    txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;
                    txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2UNCOATEDWEIGHT
        private void Retest2UNCOATEDWEIGHT()
        {
            try
            {
                int? i = 0;
                decimal value;

                decimal? ave1 = null;
                decimal? ave2 = null;
                decimal? ave3 = null;
                decimal? ave4 = null;
                decimal? ave5 = null;
                decimal? ave6 = null;

                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2UNCOATEDWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2UNCOATEDWEIGHTN1.Text);
                    }
                    else
                    {
                        txtRetest2UNCOATEDWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2UNCOATEDWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2UNCOATEDWEIGHTN2.Text);
                    }
                    else
                    {
                        txtRetest2UNCOATEDWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2UNCOATEDWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2UNCOATEDWEIGHTN3.Text);
                    }
                    else
                    {
                        txtRetest2UNCOATEDWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2UNCOATEDWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest2UNCOATEDWEIGHTN4.Text);
                    }
                    else
                    {
                        txtRetest2UNCOATEDWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2UNCOATEDWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest2UNCOATEDWEIGHTN5.Text);
                    }
                    else
                    {
                        txtRetest2UNCOATEDWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2UNCOATEDWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest2UNCOATEDWEIGHTN6.Text);
                    }
                    else
                    {
                        txtRetest2UNCOATEDWEIGHTN6.Text = string.Empty;
                    }
                }

                decimal? Avg = 0;

                #region New

                if (ave1 == null)
                    ave1 = 0;
                else
                    i++;

                if (ave2 == null)
                    ave2 = 0;
                else
                    i++;

                if (ave3 == null)
                    ave3 = 0;
                else
                    i++;

                if (ave4 == null)
                    ave4 = 0;
                else
                    i++;

                if (ave5 == null)
                    ave5 = 0;
                else
                    i++;

                if (ave6 == null)
                    ave6 = 0;
                else
                    i++;

                #endregion

                if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                {
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);

                    txtRetest2UNCOATEDWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2UNCOATEDWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2UNCOATEDWEIGHTSpecification.Text))
                {
                    string temp = txtRetest2UNCOATEDWEIGHTSpecification.Text;

                    decimal? num = null;
                    decimal? num2 = null;
                    decimal? num3 = null;

                    decimal? lower = null;
                    decimal? upper = null;

                    String strString = temp.Substring(0, temp.Length).Trim();
                    strString = strString.Replace(" ", "&").TrimEnd();
                    String[] myArr = strString.Split('&');

                    if (myArr.Length > 1)
                    {
                        if (myArr[1] != null)
                        {
                            if (temp.Contains("MAX"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num < ave1)
                                        txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("+/-"))
                            {
                                if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[0].ToString().Trim());

                                if (Decimal.TryParse(myArr[2].ToString().Trim(), out value))
                                    num2 = decimal.Parse(myArr[2].ToString().Trim());

                                lower = num - num2;
                                upper = num + num2;

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (lower <= ave1 && ave1 <= upper)
                                        txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                        }
                    }
                    else
                    {
                        if (myArr.Length == 1)
                        {
                            if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                num3 = decimal.Parse(myArr[0].ToString().Trim());
                        }

                        if (ave1 != null && ave1 != 0)
                        {
                            if (ave1 == num3)
                                txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;
                            else
                                txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;
                            else
                                txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;
                            else
                                txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;
                            else
                                txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;
                            else
                                txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                            else
                                txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;
                    txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;
                    txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;
                    txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;
                    txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;
                    txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLabUNCOATEDWEIGHT
        private void SaveLabUNCOATEDWEIGHT()
        {
            try
            {
                int? i = 0;
                decimal value;

                decimal? ave1 = null;
                decimal? ave2 = null;
                decimal? ave3 = null;
                decimal? ave4 = null;
                decimal? ave5 = null;
                decimal? ave6 = null;

                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabUNCOATEDWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN1.Text);
                    }
                    else
                    {
                        txtSaveLabUNCOATEDWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabUNCOATEDWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN2.Text);
                    }
                    else
                    {
                        txtSaveLabUNCOATEDWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabUNCOATEDWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN3.Text);
                    }
                    else
                    {
                        txtSaveLabUNCOATEDWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabUNCOATEDWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN4.Text);
                    }
                    else
                    {
                        txtSaveLabUNCOATEDWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabUNCOATEDWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN5.Text);
                    }
                    else
                    {
                        txtSaveLabUNCOATEDWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabUNCOATEDWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN6.Text);
                    }
                    else
                    {
                        txtSaveLabUNCOATEDWEIGHTN6.Text = string.Empty;
                    }
                }

                decimal? Avg = 0;

                #region New

                if (ave1 == null)
                    ave1 = 0;
                else
                    i++;

                if (ave2 == null)
                    ave2 = 0;
                else
                    i++;

                if (ave3 == null)
                    ave3 = 0;
                else
                    i++;

                if (ave4 == null)
                    ave4 = 0;
                else
                    i++;

                if (ave5 == null)
                    ave5 = 0;
                else
                    i++;

                if (ave6 == null)
                    ave6 = 0;
                else
                    i++;

                #endregion

                if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                {
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);

                    txtSaveLabUNCOATEDWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabUNCOATEDWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTSpecification.Text))
                {
                    string temp = txtSaveLabUNCOATEDWEIGHTSpecification.Text;

                    decimal? num = null;
                    decimal? num2 = null;
                    decimal? num3 = null;

                    decimal? lower = null;
                    decimal? upper = null;

                    String strString = temp.Substring(0, temp.Length).Trim();
                    strString = strString.Replace(" ", "&").TrimEnd();
                    String[] myArr = strString.Split('&');

                    if (myArr.Length > 1)
                    {
                        if (myArr[1] != null)
                        {
                            if (temp.Contains("MAX"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num < ave1)
                                        txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("+/-"))
                            {
                                if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[0].ToString().Trim());

                                if (Decimal.TryParse(myArr[2].ToString().Trim(), out value))
                                    num2 = decimal.Parse(myArr[2].ToString().Trim());

                                lower = num - num2;
                                upper = num + num2;

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (lower <= ave1 && ave1 <= upper)
                                        txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                            }
                        }
                    }
                    else
                    {
                        if (myArr.Length == 1)
                        {
                            if (Decimal.TryParse(myArr[0].ToString().Trim(), out value))
                                num3 = decimal.Parse(myArr[0].ToString().Trim());
                        }

                        if (ave1 != null && ave1 != 0)
                        {
                            if (ave1 == num3)
                                txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;
                            else
                                txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;
                            else
                                txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;
                            else
                                txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;
                            else
                                txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;
                            else
                                txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                            else
                                txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;
                    txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;
                    txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;
                    txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;
                    txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;
                    txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #endregion

        #endregion

        #endregion

        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            setDefForeground();

            txtITMCODE.Text = string.Empty;
            txtWEAVINGLOG.Text = string.Empty;
            txtFINISHINGLOT.Text = string.Empty;

            spRetest1.Visibility = Visibility.Collapsed;
            spRetest2.Visibility = Visibility.Collapsed;

            N_Head(null);
            N_UNCOATEDWEIGHT(null);
            Retest1UNCOATEDWEIGHT(null);
            Retest2UNCOATEDWEIGHT(null);
            SaveLabUNCOATEDWEIGHT(null);

            txtSaveLabUNCOATEDWEIGHTN1.Focus();
            txtSaveLabUNCOATEDWEIGHTN1.SelectAll();
        }

        #endregion

        #region setDefForeground
        private void setDefForeground()
        {
            txtUNCOATEDWEIGHTN1.Background = Brushes.White;
            txtUNCOATEDWEIGHTN2.Background = Brushes.White;
            txtUNCOATEDWEIGHTN3.Background = Brushes.White;
            txtUNCOATEDWEIGHTN4.Background = Brushes.White;
            txtUNCOATEDWEIGHTN5.Background = Brushes.White;
            txtUNCOATEDWEIGHTN6.Background = Brushes.White;

            txtRetest1UNCOATEDWEIGHTN1.Background = Brushes.White;
            txtRetest1UNCOATEDWEIGHTN2.Background = Brushes.White;
            txtRetest1UNCOATEDWEIGHTN3.Background = Brushes.White;
            txtRetest1UNCOATEDWEIGHTN4.Background = Brushes.White;
            txtRetest1UNCOATEDWEIGHTN5.Background = Brushes.White;
            txtRetest1UNCOATEDWEIGHTN6.Background = Brushes.White;

            txtRetest2UNCOATEDWEIGHTN1.Background = Brushes.White;
            txtRetest2UNCOATEDWEIGHTN2.Background = Brushes.White;
            txtRetest2UNCOATEDWEIGHTN3.Background = Brushes.White;
            txtRetest2UNCOATEDWEIGHTN4.Background = Brushes.White;
            txtRetest2UNCOATEDWEIGHTN5.Background = Brushes.White;
            txtRetest2UNCOATEDWEIGHTN6.Background = Brushes.White;

            txtSaveLabUNCOATEDWEIGHTN1.Background = Brushes.White;
            txtSaveLabUNCOATEDWEIGHTN2.Background = Brushes.White;
            txtSaveLabUNCOATEDWEIGHTN3.Background = Brushes.White;
            txtSaveLabUNCOATEDWEIGHTN4.Background = Brushes.White;
            txtSaveLabUNCOATEDWEIGHTN5.Background = Brushes.White;
            txtSaveLabUNCOATEDWEIGHTN6.Background = Brushes.White;
        }
        #endregion

        #region LAB_GETLABDETAIL
        private bool LAB_GETWEIGHTDATA(string P_ITMCODE, string P_WEAVINGLOG, string P_TYPE)
        {
            bool chkLoad = true;

            try
            {
                spRetest1.Visibility = Visibility.Collapsed;
                spRetest2.Visibility = Visibility.Collapsed;

                List<LAB_GETWEIGHTDATA> results = LabDataPDFDataService.Instance.LAB_GETWEIGHTDATA(P_ITMCODE, P_WEAVINGLOG, P_TYPE);
                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        decimal? WEIGHT1 = null;
                        decimal? WEIGHT2 = null;
                        decimal? WEIGHT3 = null;
                        decimal? WEIGHT4 = null;
                        decimal? WEIGHT5 = null;
                        decimal? WEIGHT6 = null;

                        #region Get Data

                        for (int page = 0; page <= results.Count - 1; page++)
                        {
                            if (page == 0)
                            {
                                if (results[page].WEIGHT1 != null)
                                {
                                    WEIGHT1 = results[page].WEIGHT1;
                                    txtUNCOATEDWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    WEIGHT2 = results[page].WEIGHT2;
                                    txtUNCOATEDWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    WEIGHT3 = results[page].WEIGHT3;
                                    txtUNCOATEDWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    WEIGHT4 = results[page].WEIGHT4;
                                    txtUNCOATEDWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    WEIGHT5 = results[page].WEIGHT5;
                                    txtUNCOATEDWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    WEIGHT6 = results[page].WEIGHT6;
                                    txtUNCOATEDWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.##");
                                }
                            }
                            else if (page == 1)
                            {
                                spRetest1.Visibility = Visibility.Visible;

                                if (results[page].WEIGHT1 != null)
                                {
                                    WEIGHT1 = results[page].WEIGHT1;
                                    txtRetest1UNCOATEDWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    WEIGHT2 = results[page].WEIGHT2;
                                    txtRetest1UNCOATEDWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    WEIGHT3 = results[page].WEIGHT3;
                                    txtRetest1UNCOATEDWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    WEIGHT4 = results[page].WEIGHT4;
                                    txtRetest1UNCOATEDWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    WEIGHT5 = results[page].WEIGHT5;
                                    txtRetest1UNCOATEDWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    WEIGHT6 = results[page].WEIGHT6;
                                    txtRetest1UNCOATEDWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.###");
                                }
                            }
                            else if (page == 2)
                            {
                                spRetest2.Visibility = Visibility.Visible;

                                if (results[page].WEIGHT1 != null)
                                {
                                    WEIGHT1 = results[page].WEIGHT1;
                                    txtRetest2UNCOATEDWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    WEIGHT2 = results[page].WEIGHT2;
                                    txtRetest2UNCOATEDWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    WEIGHT3 = results[page].WEIGHT3;
                                    txtRetest2UNCOATEDWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    WEIGHT4 = results[page].WEIGHT4;
                                    txtRetest2UNCOATEDWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    WEIGHT5 = results[page].WEIGHT5;
                                    txtRetest2UNCOATEDWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    WEIGHT6 = results[page].WEIGHT6;
                                    txtRetest2UNCOATEDWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.###");
                                }
                            }

                        }

                        #endregion

                        if (WEIGHT1 != null) { txtSaveLabUNCOATEDWEIGHTN1.Text = WEIGHT1.Value.ToString("#,##0.##"); }
                        if (WEIGHT2 != null) { txtSaveLabUNCOATEDWEIGHTN2.Text = WEIGHT2.Value.ToString("#,##0.##"); }
                        if (WEIGHT3 != null) { txtSaveLabUNCOATEDWEIGHTN3.Text = WEIGHT3.Value.ToString("#,##0.##"); }
                        if (WEIGHT4 != null) { txtSaveLabUNCOATEDWEIGHTN4.Text = WEIGHT4.Value.ToString("#,##0.##"); }
                        if (WEIGHT5 != null) { txtSaveLabUNCOATEDWEIGHTN5.Text = WEIGHT5.Value.ToString("#,##0.##"); }
                        if (WEIGHT6 != null) { txtSaveLabUNCOATEDWEIGHTN6.Text = WEIGHT6.Value.ToString("#,##0.##"); }

                        UNCOATEDWEIGHT();
                        Retest1UNCOATEDWEIGHT();
                        Retest2UNCOATEDWEIGHT();
                        SaveLabUNCOATEDWEIGHT();
                    }
                    else
                    {
                        chkLoad = false;
                    }
                }
                else
                {
                    chkLoad = false;
                }
                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region LoadItemTestProperty
        private bool LoadItemTestProperty(string P_ITMCODE)
        {
            bool chkLoad = true;

            try
            {
                List<LAB_GETITEMTESTPROPERTY> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTPROPERTY(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        N_Head(results[0].UNCOATEDWEIGHT);

                        N_UNCOATEDWEIGHT(results[0].UNCOATEDWEIGHT);
                        Retest1UNCOATEDWEIGHT(results[0].UNCOATEDWEIGHT);
                        Retest2UNCOATEDWEIGHT(results[0].UNCOATEDWEIGHT);
                        SaveLabUNCOATEDWEIGHT(results[0].UNCOATEDWEIGHT);
                    }
                }
                else
                {
                    chkLoad = false;
                }

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region LoadItemTestSpecification
        private bool LoadItemTestSpecification(string P_ITMCODE)
        {
            bool chkLoad = true;

            try
            {

                txtUNCOATEDWEIGHTSpecification.Text = string.Empty;
                txtRetest1UNCOATEDWEIGHTSpecification.Text = string.Empty;
                txtRetest2UNCOATEDWEIGHTSpecification.Text = string.Empty;
                txtSaveLabUNCOATEDWEIGHTSpecification.Text = string.Empty;

                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        txtUNCOATEDWEIGHTSpecification.Text = results[0].UNCOATEDWEIGHT_Spe;
                        txtRetest1UNCOATEDWEIGHTSpecification.Text = results[0].UNCOATEDWEIGHT_Spe;
                        txtRetest2UNCOATEDWEIGHTSpecification.Text = results[0].UNCOATEDWEIGHT_Spe;
                        txtSaveLabUNCOATEDWEIGHTSpecification.Text = results[0].UNCOATEDWEIGHT_Spe;
                    }
                }
                else
                {
                    chkLoad = false;
                }

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region Select

        private bool Select()
        {
            try
            {
                if (chkDataOnForm() == false)
                {
                    "Please Fill In All Test Result Data".ShowMessageBox();

                    return false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN1.Text))
                        saveLabUNCOATEDWEIGHT1 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN2.Text))
                        saveLabUNCOATEDWEIGHT2 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN3.Text))
                        saveLabUNCOATEDWEIGHT3 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN4.Text))
                        saveLabUNCOATEDWEIGHT4 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN4.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN5.Text))
                        saveLabUNCOATEDWEIGHT5 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN5.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN6.Text))
                        saveLabUNCOATEDWEIGHT6 = decimal.Parse(txtSaveLabUNCOATEDWEIGHTN6.Text);

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region chkDataOnForm
        private bool chkDataOnForm()
        {
            try
            {
                bool chkSave = true;

                #region check data

                for (int i = 1; i == 1; i++)
                {
                    //if (string.IsNullOrEmpty(txtITMCODE.Text))
                    //{
                    //    chkSave = false;
                    //    break;
                    //}
                    //if (string.IsNullOrEmpty(txtWEAVINGLOG.Text))
                    //{
                    //    chkSave = false;
                    //    break;
                    //}
                    //if (string.IsNullOrEmpty(txtFINISHINGLOT.Text))
                    //{
                    //    chkSave = false;
                    //    break;
                    //}

                    if (txtSaveLabUNCOATEDWEIGHTN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabUNCOATEDWEIGHTN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabUNCOATEDWEIGHTN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabUNCOATEDWEIGHTN4.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN4.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabUNCOATEDWEIGHTN5.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN5.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabUNCOATEDWEIGHTN6.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabUNCOATEDWEIGHTN6.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                }
                #endregion

                return chkSave;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Visibility

        #region N_UNCOATEDWEIGHT
        private void N_UNCOATEDWEIGHT(decimal? n)
        {
            decimal? UNCOATEDWEIGHT = n;

            txtUNCOATEDWEIGHTSpecification.Text = string.Empty;
            txtUNCOATEDWEIGHTN1.Text = string.Empty;
            txtUNCOATEDWEIGHTN2.Text = string.Empty;
            txtUNCOATEDWEIGHTN3.Text = string.Empty;
            txtUNCOATEDWEIGHTN4.Text = string.Empty;
            txtUNCOATEDWEIGHTN5.Text = string.Empty;
            txtUNCOATEDWEIGHTN6.Text = string.Empty;
            txtUNCOATEDWEIGHTAve.Text = string.Empty;

            if (UNCOATEDWEIGHT != null && UNCOATEDWEIGHT > 0)
            {

                if (UNCOATEDWEIGHT == 6)
                {
                    txtUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (UNCOATEDWEIGHT == 5)
                {
                    txtUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 4)
                {
                    txtUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 3)
                {
                    txtUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 2)
                {
                    txtUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 1)
                {
                    txtUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtUNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtUNCOATEDWEIGHTN1.Visibility = Visibility.Collapsed;
                txtUNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                txtUNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                txtUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                txtUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                txtUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1UNCOATEDWEIGHT
        private void Retest1UNCOATEDWEIGHT(decimal? n)
        {
            decimal? UNCOATEDWEIGHT = n;

            txtRetest1UNCOATEDWEIGHTSpecification.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTN1.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTN2.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTN3.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTN4.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTN5.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTN6.Text = string.Empty;
            txtRetest1UNCOATEDWEIGHTAve.Text = string.Empty;

            if (UNCOATEDWEIGHT != null && UNCOATEDWEIGHT > 0)
            {

                if (UNCOATEDWEIGHT == 6)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (UNCOATEDWEIGHT == 5)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 4)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 3)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 2)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 1)
                {
                    txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1UNCOATEDWEIGHTN1.Visibility = Visibility.Collapsed;
                txtRetest1UNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                txtRetest1UNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                txtRetest1UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                txtRetest1UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                txtRetest1UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2UNCOATEDWEIGHT
        private void Retest2UNCOATEDWEIGHT(decimal? n)
        {
            decimal? UNCOATEDWEIGHT = n;

            txtRetest2UNCOATEDWEIGHTSpecification.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTN1.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTN2.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTN3.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTN4.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTN5.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTN6.Text = string.Empty;
            txtRetest2UNCOATEDWEIGHTAve.Text = string.Empty;

            if (UNCOATEDWEIGHT != null && UNCOATEDWEIGHT > 0)
            {

                if (UNCOATEDWEIGHT == 6)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (UNCOATEDWEIGHT == 5)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 4)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 3)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 2)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 1)
                {
                    txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2UNCOATEDWEIGHTN1.Visibility = Visibility.Collapsed;
                txtRetest2UNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                txtRetest2UNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                txtRetest2UNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                txtRetest2UNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                txtRetest2UNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLabUNCOATEDWEIGHT
        private void SaveLabUNCOATEDWEIGHT(decimal? n)
        {
            decimal? UNCOATEDWEIGHT = n;

            txtSaveLabUNCOATEDWEIGHTSpecification.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTN1.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTN2.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTN3.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTN4.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTN5.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTN6.Text = string.Empty;
            txtSaveLabUNCOATEDWEIGHTAve.Text = string.Empty;

            if (UNCOATEDWEIGHT != null && UNCOATEDWEIGHT > 0)
            {

                if (UNCOATEDWEIGHT == 6)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (UNCOATEDWEIGHT == 5)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 4)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 3)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 2)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (UNCOATEDWEIGHT == 1)
                {
                    txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabUNCOATEDWEIGHTN1.Visibility = Visibility.Collapsed;
                txtSaveLabUNCOATEDWEIGHTN2.Visibility = Visibility.Collapsed;
                txtSaveLabUNCOATEDWEIGHTN3.Visibility = Visibility.Collapsed;
                txtSaveLabUNCOATEDWEIGHTN4.Visibility = Visibility.Collapsed;
                txtSaveLabUNCOATEDWEIGHTN5.Visibility = Visibility.Collapsed;
                txtSaveLabUNCOATEDWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region N_Head
        private void N_Head(decimal? n)
        {
            decimal? Head = n;


            if (Head != null && Head > 0)
            {

                if (Head == 6)
                {
                    labN1PDF.Visibility = Visibility.Visible;
                    labN2PDF.Visibility = Visibility.Visible;
                    labN3PDF.Visibility = Visibility.Visible;
                    labN4PDF.Visibility = Visibility.Visible;
                    labN5PDF.Visibility = Visibility.Visible;
                    labN6PDF.Visibility = Visibility.Visible;
                }
                else if (Head == 5)
                {
                    labN1PDF.Visibility = Visibility.Visible;
                    labN2PDF.Visibility = Visibility.Visible;
                    labN3PDF.Visibility = Visibility.Visible;
                    labN4PDF.Visibility = Visibility.Visible;
                    labN5PDF.Visibility = Visibility.Visible;
                    labN6PDF.Visibility = Visibility.Collapsed;
                }
                else if (Head == 4)
                {
                    labN1PDF.Visibility = Visibility.Visible;
                    labN2PDF.Visibility = Visibility.Visible;
                    labN3PDF.Visibility = Visibility.Visible;
                    labN4PDF.Visibility = Visibility.Visible;
                    labN5PDF.Visibility = Visibility.Collapsed;
                    labN6PDF.Visibility = Visibility.Collapsed;
                }
                else if (Head == 3)
                {
                    labN1PDF.Visibility = Visibility.Visible;
                    labN2PDF.Visibility = Visibility.Visible;
                    labN3PDF.Visibility = Visibility.Visible;
                    labN4PDF.Visibility = Visibility.Collapsed;
                    labN5PDF.Visibility = Visibility.Collapsed;
                    labN6PDF.Visibility = Visibility.Collapsed;
                }
                else if (Head == 2)
                {
                    labN1PDF.Visibility = Visibility.Visible;
                    labN2PDF.Visibility = Visibility.Visible;
                    labN3PDF.Visibility = Visibility.Collapsed;
                    labN4PDF.Visibility = Visibility.Collapsed;
                    labN5PDF.Visibility = Visibility.Collapsed;
                    labN6PDF.Visibility = Visibility.Collapsed;
                }
                else if (Head == 1)
                {
                    labN1PDF.Visibility = Visibility.Visible;
                    labN2PDF.Visibility = Visibility.Collapsed;
                    labN3PDF.Visibility = Visibility.Collapsed;
                    labN4PDF.Visibility = Visibility.Collapsed;
                    labN5PDF.Visibility = Visibility.Collapsed;
                    labN6PDF.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                labN1PDF.Visibility = Visibility.Collapsed;
                labN2PDF.Visibility = Visibility.Collapsed;
                labN3PDF.Visibility = Visibility.Collapsed;
                labN4PDF.Visibility = Visibility.Collapsed;
                labN5PDF.Visibility = Visibility.Collapsed;
                labN6PDF.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #endregion

        #region ReadOnly
        private void ReadOnly()
        {
            txtUNCOATEDWEIGHTN1.IsReadOnly = true;
            txtUNCOATEDWEIGHTN2.IsReadOnly = true;
            txtUNCOATEDWEIGHTN3.IsReadOnly = true;
            txtUNCOATEDWEIGHTN4.IsReadOnly = true;
            txtUNCOATEDWEIGHTN5.IsReadOnly = true;
            txtUNCOATEDWEIGHTN6.IsReadOnly = true;

            txtRetest1UNCOATEDWEIGHTN1.IsReadOnly = true;
            txtRetest1UNCOATEDWEIGHTN2.IsReadOnly = true;
            txtRetest1UNCOATEDWEIGHTN3.IsReadOnly = true;
            txtRetest1UNCOATEDWEIGHTN4.IsReadOnly = true;
            txtRetest1UNCOATEDWEIGHTN5.IsReadOnly = true;
            txtRetest1UNCOATEDWEIGHTN6.IsReadOnly = true;

            txtRetest2UNCOATEDWEIGHTN1.IsReadOnly = true;
            txtRetest2UNCOATEDWEIGHTN2.IsReadOnly = true;
            txtRetest2UNCOATEDWEIGHTN3.IsReadOnly = true;
            txtRetest2UNCOATEDWEIGHTN4.IsReadOnly = true;
            txtRetest2UNCOATEDWEIGHTN5.IsReadOnly = true;
            txtRetest2UNCOATEDWEIGHTN6.IsReadOnly = true;

            //txtSaveLabUNCOATEDWEIGHTN1.IsReadOnly = true;
            //txtSaveLabUNCOATEDWEIGHTN2.IsReadOnly = true;
            //txtSaveLabUNCOATEDWEIGHTN3.IsReadOnly = true;
            //txtSaveLabUNCOATEDWEIGHTN4.IsReadOnly = true;
            //txtSaveLabUNCOATEDWEIGHTN5.IsReadOnly = true;
            //txtSaveLabUNCOATEDWEIGHTN6.IsReadOnly = true;
        }
        #endregion

        #endregion

        #region Public Methods

        #region UNCOATEDWEIGHT

        decimal? saveLabUNCOATEDWEIGHT1 = null;
        decimal? saveLabUNCOATEDWEIGHT2 = null;
        decimal? saveLabUNCOATEDWEIGHT3 = null;
        decimal? saveLabUNCOATEDWEIGHT4 = null;
        decimal? saveLabUNCOATEDWEIGHT5 = null;
        decimal? saveLabUNCOATEDWEIGHT6 = null;

        public decimal? UNCOATEDWEIGHT1
        {
            get { return saveLabUNCOATEDWEIGHT1; }
            set { saveLabUNCOATEDWEIGHT1 = value; }
        }

        public decimal? UNCOATEDWEIGHT2
        {
            get { return saveLabUNCOATEDWEIGHT2; }
            set { saveLabUNCOATEDWEIGHT2 = value; }
        }

        public decimal? UNCOATEDWEIGHT3
        {
            get { return saveLabUNCOATEDWEIGHT3; }
            set { saveLabUNCOATEDWEIGHT3 = value; }
        }

        public decimal? UNCOATEDWEIGHT4
        {
            get { return saveLabUNCOATEDWEIGHT4; }
            set { saveLabUNCOATEDWEIGHT4 = value; }
        }

        public decimal? UNCOATEDWEIGHT5
        {
            get { return saveLabUNCOATEDWEIGHT5; }
            set { saveLabUNCOATEDWEIGHT5 = value; }
        }

        public decimal? UNCOATEDWEIGHT6
        {
            get { return saveLabUNCOATEDWEIGHT6; }
            set { saveLabUNCOATEDWEIGHT6 = value; }
        }

        #endregion

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string itemCode, string weavingLot, string finishingLot)
        {
            if (ITM_CODE != null)
            {
                ITM_CODE = itemCode;
            }

            if (WEAVINGLOT != null)
            {
                WEAVINGLOT = weavingLot;
            }

            if (FINISHINGLOT != null)
            {
                FINISHINGLOT = finishingLot;
            }
        }

        #endregion

    }
}

