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
    /// Interaction logic for RetestCoatedWeightWindow.xaml
    /// </summary>
    public partial class RetestCoatedWeightWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetestCoatedWeightWindow()
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
            LAB_GETWEIGHTDATA(txtITMCODE.Text, txtWEAVINGLOG.Text, "CW");

            txtSaveLabCOATINGWEIGHTN1.Focus();
            txtSaveLabCOATINGWEIGHTN1.SelectAll();
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
                if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN1.Text) && txtSaveLabCOATINGWEIGHTN1.IsVisible == true)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN2.Text) && txtSaveLabCOATINGWEIGHTN2.IsVisible == true)
                {
                    txtSaveLabCOATINGWEIGHTN2.Focus();
                    txtSaveLabCOATINGWEIGHTN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN3.Text) && txtSaveLabCOATINGWEIGHTN3.IsVisible == true)
                {
                    txtSaveLabCOATINGWEIGHTN3.Focus();
                    txtSaveLabCOATINGWEIGHTN3.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN4.Text) && txtSaveLabCOATINGWEIGHTN4.IsVisible == true)
                {
                    txtSaveLabCOATINGWEIGHTN4.Focus();
                    txtSaveLabCOATINGWEIGHTN4.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN5.Text) && txtSaveLabCOATINGWEIGHTN5.IsVisible == true)
                {
                    txtSaveLabCOATINGWEIGHTN5.Focus();
                    txtSaveLabCOATINGWEIGHTN5.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN6.Text) && txtSaveLabCOATINGWEIGHTN6.IsVisible == true)
                {
                    txtSaveLabCOATINGWEIGHTN6.Focus();
                    txtSaveLabCOATINGWEIGHTN6.SelectAll();
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
                if (txtCOATINGWEIGHTN1.IsEnabled == true)
                {
                    txtCOATINGWEIGHTN1.Focus();
                    txtCOATINGWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region Item Property

        #region KeyDown

        #region COATINGWEIGHT
        private void txtCOATINGWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtCOATINGWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtCOATINGWEIGHTN2.Focus();
                    txtCOATINGWEIGHTN2.SelectAll();
                }
                else if (txtRetest1COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN1.Focus();
                    txtRetest1COATINGWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtCOATINGWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtCOATINGWEIGHTN3.Focus();
                    txtCOATINGWEIGHTN3.SelectAll();
                }
                else if (txtRetest1COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN1.Focus();
                    txtRetest1COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtCOATINGWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtCOATINGWEIGHTN4.Focus();
                    txtCOATINGWEIGHTN4.SelectAll();
                }
                else if (txtRetest1COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN1.Focus();
                    txtRetest1COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtCOATINGWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtCOATINGWEIGHTN5.Focus();
                    txtCOATINGWEIGHTN5.SelectAll();
                }
                else if (txtRetest1COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN1.Focus();
                    txtRetest1COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtCOATINGWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtCOATINGWEIGHTN6.Focus();
                    txtCOATINGWEIGHTN6.SelectAll();
                }
                else if (txtRetest1COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN1.Focus();
                    txtRetest1COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN1.Focus();
                    txtRetest1COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 COATINGWEIGHT
        private void txtRetest1COATINGWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1COATINGWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN2.Focus();
                    txtRetest1COATINGWEIGHTN2.SelectAll();
                }
                else if (txtRetest2COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN1.Focus();
                    txtRetest2COATINGWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1COATINGWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1COATINGWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN3.Focus();
                    txtRetest1COATINGWEIGHTN3.SelectAll();
                }
                else if (txtRetest2COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN1.Focus();
                    txtRetest2COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1COATINGWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1COATINGWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN4.Focus();
                    txtRetest1COATINGWEIGHTN4.SelectAll();
                }
                else if (txtRetest2COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN1.Focus();
                    txtRetest2COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1COATINGWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1COATINGWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN5.Focus();
                    txtRetest1COATINGWEIGHTN5.SelectAll();
                }
                else if (txtRetest2COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN1.Focus();
                    txtRetest2COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1COATINGWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1COATINGWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtRetest1COATINGWEIGHTN6.Focus();
                    txtRetest1COATINGWEIGHTN6.SelectAll();
                }
                else if (txtRetest2COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN1.Focus();
                    txtRetest2COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1COATINGWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2COATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN1.Focus();
                    txtRetest2COATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 COATINGWEIGHT
        private void txtRetest2COATINGWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2COATINGWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN2.Focus();
                    txtRetest2COATINGWEIGHTN2.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2COATINGWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2COATINGWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN3.Focus();
                    txtRetest2COATINGWEIGHTN3.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2COATINGWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2COATINGWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN4.Focus();
                    txtRetest2COATINGWEIGHTN4.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2COATINGWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2COATINGWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN5.Focus();
                    txtRetest2COATINGWEIGHTN5.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2COATINGWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2COATINGWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtRetest2COATINGWEIGHTN6.Focus();
                    txtRetest2COATINGWEIGHTN6.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2COATINGWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN1.Focus();
                    txtSaveLabCOATINGWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab COATINGWEIGHT
        private void txtSaveLabCOATINGWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN2.Focus();
                    txtSaveLabCOATINGWEIGHTN2.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabCOATINGWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN3.Focus();
                    txtSaveLabCOATINGWEIGHTN3.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabCOATINGWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN4.Focus();
                    txtSaveLabCOATINGWEIGHTN4.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabCOATINGWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN5.Focus();
                    txtSaveLabCOATINGWEIGHTN5.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabCOATINGWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabCOATINGWEIGHTN6.Focus();
                    txtSaveLabCOATINGWEIGHTN6.SelectAll();
                }
                else if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabCOATINGWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabCOATINGWEIGHTN1.Visibility == Visibility.Visible)
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

        #region COATINGWEIGHT_LostFocus
        private void COATINGWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            COATINGWEIGHT();
        }
        #endregion

        #region Retest1COATINGWEIGHT_LostFocus
        private void Retest1COATINGWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1COATINGWEIGHT();
        }
        #endregion

        #region Retest2COATINGWEIGHT_LostFocus
        private void Retest2COATINGWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2COATINGWEIGHT();
        }
        #endregion

        #region SaveLabCOATINGWEIGHT_LostFocus
        private void SaveLabCOATINGWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabCOATINGWEIGHT();
        }
        #endregion

        #region COATINGWEIGHT
        private void COATINGWEIGHT()
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

                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtCOATINGWEIGHTN1.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtCOATINGWEIGHTN2.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtCOATINGWEIGHTN3.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtCOATINGWEIGHTN4.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtCOATINGWEIGHTN5.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtCOATINGWEIGHTN6.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTN6.Text = string.Empty;
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

                    txtCOATINGWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtCOATINGWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTSpecification.Text))
                {
                    string temp = txtCOATINGWEIGHTSpecification.Text;

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
                                        txtCOATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtCOATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtCOATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtCOATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtCOATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtCOATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtCOATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtCOATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtCOATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtCOATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtCOATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtCOATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtCOATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtCOATINGWEIGHTN6.Background = Brushes.White;
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
                                        txtCOATINGWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtCOATINGWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtCOATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtCOATINGWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtCOATINGWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtCOATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtCOATINGWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtCOATINGWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtCOATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtCOATINGWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtCOATINGWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtCOATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtCOATINGWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtCOATINGWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtCOATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtCOATINGWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtCOATINGWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtCOATINGWEIGHTN6.Background = Brushes.White;
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
                                txtCOATINGWEIGHTN1.Background = Brushes.White;
                            else
                                txtCOATINGWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtCOATINGWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtCOATINGWEIGHTN2.Background = Brushes.White;
                            else
                                txtCOATINGWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtCOATINGWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtCOATINGWEIGHTN3.Background = Brushes.White;
                            else
                                txtCOATINGWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtCOATINGWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtCOATINGWEIGHTN4.Background = Brushes.White;
                            else
                                txtCOATINGWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtCOATINGWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtCOATINGWEIGHTN5.Background = Brushes.White;
                            else
                                txtCOATINGWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtCOATINGWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtCOATINGWEIGHTN6.Background = Brushes.White;
                            else
                                txtCOATINGWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtCOATINGWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtCOATINGWEIGHTN1.Background = Brushes.White;
                    txtCOATINGWEIGHTN2.Background = Brushes.White;
                    txtCOATINGWEIGHTN3.Background = Brushes.White;
                    txtCOATINGWEIGHTN4.Background = Brushes.White;
                    txtCOATINGWEIGHTN5.Background = Brushes.White;
                    txtCOATINGWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1COATINGWEIGHT
        private void Retest1COATINGWEIGHT()
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

                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1COATINGWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1COATINGWEIGHTN1.Text);
                    }
                    else
                    {
                        txtRetest1COATINGWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1COATINGWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1COATINGWEIGHTN2.Text);
                    }
                    else
                    {
                        txtRetest1COATINGWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1COATINGWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1COATINGWEIGHTN3.Text);
                    }
                    else
                    {
                        txtRetest1COATINGWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1COATINGWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest1COATINGWEIGHTN4.Text);
                    }
                    else
                    {
                        txtRetest1COATINGWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1COATINGWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest1COATINGWEIGHTN5.Text);
                    }
                    else
                    {
                        txtRetest1COATINGWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1COATINGWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest1COATINGWEIGHTN6.Text);
                    }
                    else
                    {
                        txtRetest1COATINGWEIGHTN6.Text = string.Empty;
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

                    txtRetest1COATINGWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1COATINGWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1COATINGWEIGHTSpecification.Text))
                {
                    string temp = txtRetest1COATINGWEIGHTSpecification.Text;

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
                                        txtRetest1COATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1COATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1COATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1COATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1COATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1COATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1COATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1COATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1COATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1COATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1COATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1COATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
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
                                        txtRetest1COATINGWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtRetest1COATINGWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtRetest1COATINGWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtRetest1COATINGWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtRetest1COATINGWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtRetest1COATINGWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtRetest1COATINGWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtRetest1COATINGWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtRetest1COATINGWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtRetest1COATINGWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtRetest1COATINGWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
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
                                txtRetest1COATINGWEIGHTN1.Background = Brushes.White;
                            else
                                txtRetest1COATINGWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1COATINGWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtRetest1COATINGWEIGHTN2.Background = Brushes.White;
                            else
                                txtRetest1COATINGWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1COATINGWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtRetest1COATINGWEIGHTN3.Background = Brushes.White;
                            else
                                txtRetest1COATINGWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1COATINGWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtRetest1COATINGWEIGHTN4.Background = Brushes.White;
                            else
                                txtRetest1COATINGWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1COATINGWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtRetest1COATINGWEIGHTN5.Background = Brushes.White;
                            else
                                txtRetest1COATINGWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1COATINGWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
                            else
                                txtRetest1COATINGWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1COATINGWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest1COATINGWEIGHTN1.Background = Brushes.White;
                    txtRetest1COATINGWEIGHTN2.Background = Brushes.White;
                    txtRetest1COATINGWEIGHTN3.Background = Brushes.White;
                    txtRetest1COATINGWEIGHTN4.Background = Brushes.White;
                    txtRetest1COATINGWEIGHTN5.Background = Brushes.White;
                    txtRetest1COATINGWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2COATINGWEIGHT
        private void Retest2COATINGWEIGHT()
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

                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2COATINGWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2COATINGWEIGHTN1.Text);
                    }
                    else
                    {
                        txtRetest2COATINGWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2COATINGWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2COATINGWEIGHTN2.Text);
                    }
                    else
                    {
                        txtRetest2COATINGWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2COATINGWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2COATINGWEIGHTN3.Text);
                    }
                    else
                    {
                        txtRetest2COATINGWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2COATINGWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest2COATINGWEIGHTN4.Text);
                    }
                    else
                    {
                        txtRetest2COATINGWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2COATINGWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest2COATINGWEIGHTN5.Text);
                    }
                    else
                    {
                        txtRetest2COATINGWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2COATINGWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest2COATINGWEIGHTN6.Text);
                    }
                    else
                    {
                        txtRetest2COATINGWEIGHTN6.Text = string.Empty;
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

                    txtRetest2COATINGWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2COATINGWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2COATINGWEIGHTSpecification.Text))
                {
                    string temp = txtRetest2COATINGWEIGHTSpecification.Text;

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
                                        txtRetest2COATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2COATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2COATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2COATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2COATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2COATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2COATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2COATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2COATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2COATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2COATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2COATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
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
                                        txtRetest2COATINGWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtRetest2COATINGWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtRetest2COATINGWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtRetest2COATINGWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtRetest2COATINGWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtRetest2COATINGWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtRetest2COATINGWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtRetest2COATINGWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtRetest2COATINGWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtRetest2COATINGWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtRetest2COATINGWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
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
                                txtRetest2COATINGWEIGHTN1.Background = Brushes.White;
                            else
                                txtRetest2COATINGWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2COATINGWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtRetest2COATINGWEIGHTN2.Background = Brushes.White;
                            else
                                txtRetest2COATINGWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2COATINGWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtRetest2COATINGWEIGHTN3.Background = Brushes.White;
                            else
                                txtRetest2COATINGWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2COATINGWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtRetest2COATINGWEIGHTN4.Background = Brushes.White;
                            else
                                txtRetest2COATINGWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2COATINGWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtRetest2COATINGWEIGHTN5.Background = Brushes.White;
                            else
                                txtRetest2COATINGWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2COATINGWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
                            else
                                txtRetest2COATINGWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2COATINGWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest2COATINGWEIGHTN1.Background = Brushes.White;
                    txtRetest2COATINGWEIGHTN2.Background = Brushes.White;
                    txtRetest2COATINGWEIGHTN3.Background = Brushes.White;
                    txtRetest2COATINGWEIGHTN4.Background = Brushes.White;
                    txtRetest2COATINGWEIGHTN5.Background = Brushes.White;
                    txtRetest2COATINGWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLabCOATINGWEIGHT
        private void SaveLabCOATINGWEIGHT()
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

                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabCOATINGWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabCOATINGWEIGHTN1.Text);
                    }
                    else
                    {
                        txtSaveLabCOATINGWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabCOATINGWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabCOATINGWEIGHTN2.Text);
                    }
                    else
                    {
                        txtSaveLabCOATINGWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabCOATINGWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabCOATINGWEIGHTN3.Text);
                    }
                    else
                    {
                        txtSaveLabCOATINGWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabCOATINGWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtSaveLabCOATINGWEIGHTN4.Text);
                    }
                    else
                    {
                        txtSaveLabCOATINGWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabCOATINGWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtSaveLabCOATINGWEIGHTN5.Text);
                    }
                    else
                    {
                        txtSaveLabCOATINGWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabCOATINGWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtSaveLabCOATINGWEIGHTN6.Text);
                    }
                    else
                    {
                        txtSaveLabCOATINGWEIGHTN6.Text = string.Empty;
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

                    txtSaveLabCOATINGWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabCOATINGWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTSpecification.Text))
                {
                    string temp = txtSaveLabCOATINGWEIGHTSpecification.Text;

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
                                        txtSaveLabCOATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabCOATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabCOATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabCOATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabCOATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabCOATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabCOATINGWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabCOATINGWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabCOATINGWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabCOATINGWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabCOATINGWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabCOATINGWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
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
                                        txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtSaveLabCOATINGWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtSaveLabCOATINGWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtSaveLabCOATINGWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtSaveLabCOATINGWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtSaveLabCOATINGWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtSaveLabCOATINGWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
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
                                txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;
                            else
                                txtSaveLabCOATINGWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;
                            else
                                txtSaveLabCOATINGWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;
                            else
                                txtSaveLabCOATINGWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;
                            else
                                txtSaveLabCOATINGWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;
                            else
                                txtSaveLabCOATINGWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
                            else
                                txtSaveLabCOATINGWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;
                    txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;
                    txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;
                    txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;
                    txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;
                    txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
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

            N_COATINGWEIGHT(null);
            Retest1COATINGWEIGHT(null);
            Retest2COATINGWEIGHT(null);
            SaveLabCOATINGWEIGHT(null);

            txtSaveLabCOATINGWEIGHTN1.Focus();
            txtSaveLabCOATINGWEIGHTN1.SelectAll();
        }

        #endregion

        #region setDefForeground
        private void setDefForeground()
        {

            txtCOATINGWEIGHTN1.Background = Brushes.White;
            txtCOATINGWEIGHTN2.Background = Brushes.White;
            txtCOATINGWEIGHTN3.Background = Brushes.White;
            txtCOATINGWEIGHTN4.Background = Brushes.White;
            txtCOATINGWEIGHTN5.Background = Brushes.White;
            txtCOATINGWEIGHTN6.Background = Brushes.White;

            txtRetest1COATINGWEIGHTN1.Background = Brushes.White;
            txtRetest1COATINGWEIGHTN2.Background = Brushes.White;
            txtRetest1COATINGWEIGHTN3.Background = Brushes.White;
            txtRetest1COATINGWEIGHTN4.Background = Brushes.White;
            txtRetest1COATINGWEIGHTN5.Background = Brushes.White;
            txtRetest1COATINGWEIGHTN6.Background = Brushes.White;

            txtRetest2COATINGWEIGHTN1.Background = Brushes.White;
            txtRetest2COATINGWEIGHTN2.Background = Brushes.White;
            txtRetest2COATINGWEIGHTN3.Background = Brushes.White;
            txtRetest2COATINGWEIGHTN4.Background = Brushes.White;
            txtRetest2COATINGWEIGHTN5.Background = Brushes.White;
            txtRetest2COATINGWEIGHTN6.Background = Brushes.White;

            txtSaveLabCOATINGWEIGHTN1.Background = Brushes.White;
            txtSaveLabCOATINGWEIGHTN2.Background = Brushes.White;
            txtSaveLabCOATINGWEIGHTN3.Background = Brushes.White;
            txtSaveLabCOATINGWEIGHTN4.Background = Brushes.White;
            txtSaveLabCOATINGWEIGHTN5.Background = Brushes.White;
            txtSaveLabCOATINGWEIGHTN6.Background = Brushes.White;
        }
        #endregion

        #region LAB_GETWEIGHTDATA
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
                                    txtCOATINGWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    WEIGHT2 = results[page].WEIGHT2;
                                    txtCOATINGWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    WEIGHT3 = results[page].WEIGHT3;
                                    txtCOATINGWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    WEIGHT4 = results[page].WEIGHT4;
                                    txtCOATINGWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    WEIGHT5 = results[page].WEIGHT5;
                                    txtCOATINGWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.##");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    WEIGHT6 = results[page].WEIGHT6;
                                    txtCOATINGWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.##");
                                }
                            }
                            else if (page == 1)
                            {
                                spRetest1.Visibility = Visibility.Visible;

                                if (results[page].WEIGHT1 != null)
                                {
                                    WEIGHT1 = results[page].WEIGHT1;
                                    txtRetest1COATINGWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    WEIGHT2 = results[page].WEIGHT2;
                                    txtRetest1COATINGWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    WEIGHT3 = results[page].WEIGHT3;
                                    txtRetest1COATINGWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    WEIGHT4 = results[page].WEIGHT4;
                                    txtRetest1COATINGWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    WEIGHT5 = results[page].WEIGHT5;
                                    txtRetest1COATINGWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    WEIGHT6 = results[page].WEIGHT6;
                                    txtRetest1COATINGWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.###");
                                }
                            }
                            else if (page == 2)
                            {
                                spRetest2.Visibility = Visibility.Visible;

                                if (results[page].WEIGHT1 != null)
                                {
                                    WEIGHT1 = results[page].WEIGHT1;
                                    txtRetest2COATINGWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    WEIGHT2 = results[page].WEIGHT2;
                                    txtRetest2COATINGWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    WEIGHT3 = results[page].WEIGHT3;
                                    txtRetest2COATINGWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    WEIGHT4 = results[page].WEIGHT4;
                                    txtRetest2COATINGWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    WEIGHT5 = results[page].WEIGHT5;
                                    txtRetest2COATINGWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    WEIGHT6 = results[page].WEIGHT6;
                                    txtRetest2COATINGWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.###");
                                }
                            }

                        }

                        #endregion

                        if (WEIGHT1 != null) { txtSaveLabCOATINGWEIGHTN1.Text = WEIGHT1.Value.ToString("#,##0.##"); }
                        if (WEIGHT2 != null) { txtSaveLabCOATINGWEIGHTN2.Text = WEIGHT2.Value.ToString("#,##0.##"); }
                        if (WEIGHT3 != null) { txtSaveLabCOATINGWEIGHTN3.Text = WEIGHT3.Value.ToString("#,##0.##"); }
                        if (WEIGHT4 != null) { txtSaveLabCOATINGWEIGHTN4.Text = WEIGHT4.Value.ToString("#,##0.##"); }
                        if (WEIGHT5 != null) { txtSaveLabCOATINGWEIGHTN5.Text = WEIGHT5.Value.ToString("#,##0.##"); }
                        if (WEIGHT6 != null) { txtSaveLabCOATINGWEIGHTN6.Text = WEIGHT6.Value.ToString("#,##0.##"); }

                        COATINGWEIGHT();
                        Retest1COATINGWEIGHT();
                        Retest2COATINGWEIGHT();
                        SaveLabCOATINGWEIGHT();
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
                        N_Head(results[0].COATINGWEIGHT);

                        N_COATINGWEIGHT(results[0].COATINGWEIGHT);
                        Retest1COATINGWEIGHT(results[0].COATINGWEIGHT);
                        Retest2COATINGWEIGHT(results[0].COATINGWEIGHT);
                        SaveLabCOATINGWEIGHT(results[0].COATINGWEIGHT);
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
                txtCOATINGWEIGHTSpecification.Text = string.Empty;
                txtRetest1COATINGWEIGHTSpecification.Text = string.Empty;
                txtRetest2COATINGWEIGHTSpecification.Text = string.Empty;
                txtSaveLabCOATINGWEIGHTSpecification.Text = string.Empty;

                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        txtCOATINGWEIGHTSpecification.Text = results[0].COATINGWEIGHT_Spe;
                        txtRetest1COATINGWEIGHTSpecification.Text = results[0].COATINGWEIGHT_Spe;
                        txtRetest2COATINGWEIGHTSpecification.Text = results[0].COATINGWEIGHT_Spe;
                        txtSaveLabCOATINGWEIGHTSpecification.Text = results[0].COATINGWEIGHT_Spe;
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
                    if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN1.Text))
                        saveLabCOATWEIGHT1 = decimal.Parse(txtSaveLabCOATINGWEIGHTN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN2.Text))
                        saveLabCOATWEIGHT2 = decimal.Parse(txtSaveLabCOATINGWEIGHTN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN3.Text))
                        saveLabCOATWEIGHT3 = decimal.Parse(txtSaveLabCOATINGWEIGHTN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN4.Text))
                        saveLabCOATWEIGHT4 = decimal.Parse(txtSaveLabCOATINGWEIGHTN4.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN5.Text))
                        saveLabCOATWEIGHT5 = decimal.Parse(txtSaveLabCOATINGWEIGHTN5.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN6.Text))
                        saveLabCOATWEIGHT6 = decimal.Parse(txtSaveLabCOATINGWEIGHTN6.Text);

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

                    if (txtSaveLabCOATINGWEIGHTN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabCOATINGWEIGHTN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabCOATINGWEIGHTN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabCOATINGWEIGHTN4.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN4.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabCOATINGWEIGHTN5.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN5.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabCOATINGWEIGHTN6.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabCOATINGWEIGHTN6.Text))
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

        #region N_COATINGWEIGHT
        private void N_COATINGWEIGHT(decimal? n)
        {
            decimal? COATINGWEIGHT = n;

            txtCOATINGWEIGHTSpecification.Text = string.Empty;
            txtCOATINGWEIGHTN1.Text = string.Empty;
            txtCOATINGWEIGHTN2.Text = string.Empty;
            txtCOATINGWEIGHTN3.Text = string.Empty;
            txtCOATINGWEIGHTN4.Text = string.Empty;
            txtCOATINGWEIGHTN5.Text = string.Empty;
            txtCOATINGWEIGHTN6.Text = string.Empty;
            txtCOATINGWEIGHTAve.Text = string.Empty;

            if (COATINGWEIGHT != null && COATINGWEIGHT > 0)
            {

                if (COATINGWEIGHT == 6)
                {
                    txtCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (COATINGWEIGHT == 5)
                {
                    txtCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 4)
                {
                    txtCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 3)
                {
                    txtCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 2)
                {
                    txtCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 1)
                {
                    txtCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtCOATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtCOATINGWEIGHTN1.Visibility = Visibility.Collapsed;
                txtCOATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                txtCOATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                txtCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                txtCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                txtCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1COATINGWEIGHT
        private void Retest1COATINGWEIGHT(decimal? n)
        {
            decimal? COATINGWEIGHT = n;

            txtRetest1COATINGWEIGHTSpecification.Text = string.Empty;
            txtRetest1COATINGWEIGHTN1.Text = string.Empty;
            txtRetest1COATINGWEIGHTN2.Text = string.Empty;
            txtRetest1COATINGWEIGHTN3.Text = string.Empty;
            txtRetest1COATINGWEIGHTN4.Text = string.Empty;
            txtRetest1COATINGWEIGHTN5.Text = string.Empty;
            txtRetest1COATINGWEIGHTN6.Text = string.Empty;
            txtRetest1COATINGWEIGHTAve.Text = string.Empty;

            if (COATINGWEIGHT != null && COATINGWEIGHT > 0)
            {

                if (COATINGWEIGHT == 6)
                {
                    txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (COATINGWEIGHT == 5)
                {
                    txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 4)
                {
                    txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 3)
                {
                    txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 2)
                {
                    txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 1)
                {
                    txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1COATINGWEIGHTN1.Visibility = Visibility.Collapsed;
                txtRetest1COATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                txtRetest1COATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                txtRetest1COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                txtRetest1COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                txtRetest1COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2COATINGWEIGHT
        private void Retest2COATINGWEIGHT(decimal? n)
        {
            decimal? COATINGWEIGHT = n;

            txtRetest2COATINGWEIGHTSpecification.Text = string.Empty;
            txtRetest2COATINGWEIGHTN1.Text = string.Empty;
            txtRetest2COATINGWEIGHTN2.Text = string.Empty;
            txtRetest2COATINGWEIGHTN3.Text = string.Empty;
            txtRetest2COATINGWEIGHTN4.Text = string.Empty;
            txtRetest2COATINGWEIGHTN5.Text = string.Empty;
            txtRetest2COATINGWEIGHTN6.Text = string.Empty;
            txtRetest2COATINGWEIGHTAve.Text = string.Empty;

            if (COATINGWEIGHT != null && COATINGWEIGHT > 0)
            {

                if (COATINGWEIGHT == 6)
                {
                    txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (COATINGWEIGHT == 5)
                {
                    txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 4)
                {
                    txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 3)
                {
                    txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 2)
                {
                    txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 1)
                {
                    txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2COATINGWEIGHTN1.Visibility = Visibility.Collapsed;
                txtRetest2COATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                txtRetest2COATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                txtRetest2COATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                txtRetest2COATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                txtRetest2COATINGWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLabCOATINGWEIGHT
        private void SaveLabCOATINGWEIGHT(decimal? n)
        {
            decimal? COATINGWEIGHT = n;

            txtSaveLabCOATINGWEIGHTSpecification.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTN1.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTN2.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTN3.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTN4.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTN5.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTN6.Text = string.Empty;
            txtSaveLabCOATINGWEIGHTAve.Text = string.Empty;

            if (COATINGWEIGHT != null && COATINGWEIGHT > 0)
            {

                if (COATINGWEIGHT == 6)
                {
                    txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (COATINGWEIGHT == 5)
                {
                    txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 4)
                {
                    txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 3)
                {
                    txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 2)
                {
                    txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (COATINGWEIGHT == 1)
                {
                    txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabCOATINGWEIGHTN1.Visibility = Visibility.Collapsed;
                txtSaveLabCOATINGWEIGHTN2.Visibility = Visibility.Collapsed;
                txtSaveLabCOATINGWEIGHTN3.Visibility = Visibility.Collapsed;
                txtSaveLabCOATINGWEIGHTN4.Visibility = Visibility.Collapsed;
                txtSaveLabCOATINGWEIGHTN5.Visibility = Visibility.Collapsed;
                txtSaveLabCOATINGWEIGHTN6.Visibility = Visibility.Collapsed;
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
            txtCOATINGWEIGHTN1.IsReadOnly = true;
            txtCOATINGWEIGHTN2.IsReadOnly = true;
            txtCOATINGWEIGHTN3.IsReadOnly = true;
            txtCOATINGWEIGHTN4.IsReadOnly = true;
            txtCOATINGWEIGHTN5.IsReadOnly = true;
            txtCOATINGWEIGHTN6.IsReadOnly = true;

            txtRetest1COATINGWEIGHTN1.IsReadOnly = true;
            txtRetest1COATINGWEIGHTN2.IsReadOnly = true;
            txtRetest1COATINGWEIGHTN3.IsReadOnly = true;
            txtRetest1COATINGWEIGHTN4.IsReadOnly = true;
            txtRetest1COATINGWEIGHTN5.IsReadOnly = true;
            txtRetest1COATINGWEIGHTN6.IsReadOnly = true;

            txtRetest2COATINGWEIGHTN1.IsReadOnly = true;
            txtRetest2COATINGWEIGHTN2.IsReadOnly = true;
            txtRetest2COATINGWEIGHTN3.IsReadOnly = true;
            txtRetest2COATINGWEIGHTN4.IsReadOnly = true;
            txtRetest2COATINGWEIGHTN5.IsReadOnly = true;
            txtRetest2COATINGWEIGHTN6.IsReadOnly = true;

            //txtSaveLabCOATINGWEIGHTN1.IsReadOnly = true;
            //txtSaveLabCOATINGWEIGHTN2.IsReadOnly = true;
            //txtSaveLabCOATINGWEIGHTN3.IsReadOnly = true;
            //txtSaveLabCOATINGWEIGHTN4.IsReadOnly = true;
            //txtSaveLabCOATINGWEIGHTN5.IsReadOnly = true;
            //txtSaveLabCOATINGWEIGHTN6.IsReadOnly = true;
        }
        #endregion

        #endregion

        #region Public Methods

        #region COATINGWEIGHT

        decimal? saveLabCOATWEIGHT1 = null;
        decimal? saveLabCOATWEIGHT2 = null;
        decimal? saveLabCOATWEIGHT3 = null;
        decimal? saveLabCOATWEIGHT4 = null;
        decimal? saveLabCOATWEIGHT5 = null;
        decimal? saveLabCOATWEIGHT6 = null;

        public decimal? COATWEIGHT1
        {
            get { return saveLabCOATWEIGHT1; }
            set { saveLabCOATWEIGHT1 = value; }
        }

        public decimal? COATWEIGHT2
        {
            get { return saveLabCOATWEIGHT2; }
            set { saveLabCOATWEIGHT2 = value; }
        }

        public decimal? COATWEIGHT3
        {
            get { return saveLabCOATWEIGHT3; }
            set { saveLabCOATWEIGHT3 = value; }
        }

        public decimal? COATWEIGHT4
        {
            get { return saveLabCOATWEIGHT4; }
            set { saveLabCOATWEIGHT4 = value; }
        }

        public decimal? COATWEIGHT5
        {
            get { return saveLabCOATWEIGHT5; }
            set { saveLabCOATWEIGHT5 = value; }
        }

        public decimal? COATWEIGHT6
        {
            get { return saveLabCOATWEIGHT6; }
            set { saveLabCOATWEIGHT6 = value; }
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

