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
    /// Interaction logic for RetestStiffnessWindow.xaml
    /// </summary>
    public partial class RetestStiffnessWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetestStiffnessWindow()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            cmdRetest.Visibility = Visibility.Collapsed;

            //ReadOnly();
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
            LAB_GETSTIFFNESSDATA(txtITMCODE.Text, txtWEAVINGLOG.Text);

            txtSaveLabSTIFFNES_WN1.Focus();
            txtSaveLabSTIFFNES_WN1.SelectAll();
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
                if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN1.Text) && txtSaveLabSTIFFNES_WN1.IsVisible == true)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN2.Text) && txtSaveLabSTIFFNES_WN2.IsVisible == true)
                {
                    txtSaveLabSTIFFNES_WN2.Focus();
                    txtSaveLabSTIFFNES_WN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN3.Text) && txtSaveLabSTIFFNES_WN3.IsVisible == true)
                {
                    txtSaveLabSTIFFNES_WN3.Focus();
                    txtSaveLabSTIFFNES_WN3.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN1.Text) && txtSaveLabSTIFFNES_FN1.IsVisible == true)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN2.Text) && txtSaveLabSTIFFNES_FN2.IsVisible == true)
                {
                    txtSaveLabSTIFFNES_FN2.Focus();
                    txtSaveLabSTIFFNES_FN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN3.Text) && txtSaveLabSTIFFNES_FN3.IsVisible == true)
                {
                    txtSaveLabSTIFFNES_FN3.Focus();
                    txtSaveLabSTIFFNES_FN3.SelectAll();
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
                if (txtSTIFFNES_WN1.IsEnabled == true)
                {
                    txtSTIFFNES_WN1.Focus();
                    txtSTIFFNES_WN1.SelectAll();
                }
                else
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region Item Property

        #region KeyDown

        #region STIFFNES_W
        private void txtSTIFFNES_WN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_WN2.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_WN2.Focus();
                    txtSTIFFNES_WN2.SelectAll();
                }
                else if (txtSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
            
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_WN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_WN3.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_WN3.Focus();
                    txtSTIFFNES_WN3.SelectAll();
                }
                else if (txtSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_WN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_WN4.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_WN4.Focus();
                    txtSTIFFNES_WN4.SelectAll();
                }
                else if (txtSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_WN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_WN5.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_WN5.Focus();
                    txtSTIFFNES_WN5.SelectAll();
                }
                else if (txtSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_WN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_WN6.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_WN6.Focus();
                    txtSTIFFNES_WN6.SelectAll();
                }
                else if (txtSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_WN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN1.Focus();
                    txtSTIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region STIFFNES_F
        private void txtSTIFFNES_FN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_FN2.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN2.Focus();
                    txtSTIFFNES_FN2.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_FN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_FN3.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN3.Focus();
                    txtSTIFFNES_FN3.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_FN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_FN4.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN4.Focus();
                    txtSTIFFNES_FN4.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_FN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_FN5.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN5.Focus();
                    txtSTIFFNES_FN5.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_FN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNES_FN6.Visibility == Visibility.Visible)
                {
                    txtSTIFFNES_FN6.Focus();
                    txtSTIFFNES_FN6.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_FN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 STIFFNES_W
        private void txtRetest1STIFFNES_WN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_WN2.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN2.Focus();
                    txtRetest1STIFFNES_WN2.SelectAll();
                }
                else if (txtRetest1STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN1.Focus();
                    txtRetest1STIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_WN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_WN3.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN3.Focus();
                    txtRetest1STIFFNES_WN3.SelectAll();
                }
                else if (txtRetest1STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN1.Focus();
                    txtRetest1STIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_WN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_WN4.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN4.Focus();
                    txtRetest1STIFFNES_WN4.SelectAll();
                }
                else if (txtRetest1STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN1.Focus();
                    txtRetest1STIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_WN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_WN5.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN5.Focus();
                    txtRetest1STIFFNES_WN5.SelectAll();
                }
                else if (txtRetest1STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN1.Focus();
                    txtRetest1STIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_WN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_WN6.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN6.Focus();
                    txtRetest1STIFFNES_WN6.SelectAll();
                }
                else if (txtRetest1STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN1.Focus();
                    txtRetest1STIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_WN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN1.Focus();
                    txtRetest1STIFFNES_FN1.SelectAll();
                }
                else if (txtRetest1STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_WN1.Focus();
                    txtRetest1STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 STIFFNES_F
        private void txtRetest1STIFFNES_FN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_FN2.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN2.Focus();
                    txtRetest1STIFFNES_FN2.SelectAll();
                }
                else if (txtRetest2STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN1.Focus();
                    txtRetest2STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_FN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_FN3.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN3.Focus();
                    txtRetest1STIFFNES_FN3.SelectAll();
                }
                else if (txtRetest2STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN1.Focus();
                    txtRetest2STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_FN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_FN4.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN4.Focus();
                    txtRetest1STIFFNES_FN4.SelectAll();
                }
                else if (txtRetest2STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN1.Focus();
                    txtRetest2STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_FN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_FN5.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN5.Focus();
                    txtRetest1STIFFNES_FN5.SelectAll();
                }
                else if (txtRetest2STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN1.Focus();
                    txtRetest2STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_FN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STIFFNES_FN6.Visibility == Visibility.Visible)
                {
                    txtRetest1STIFFNES_FN6.Focus();
                    txtRetest1STIFFNES_FN6.SelectAll();
                }
                else if (txtRetest2STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN1.Focus();
                    txtRetest2STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STIFFNES_FN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN1.Focus();
                    txtRetest2STIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 STIFFNES_W
        private void txtRetest2STIFFNES_WN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_WN2.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN2.Focus();
                    txtRetest2STIFFNES_WN2.SelectAll();
                }
                else if (txtRetest2STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN1.Focus();
                    txtRetest2STIFFNES_FN1.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_WN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_WN3.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN3.Focus();
                    txtRetest2STIFFNES_WN3.SelectAll();
                }
                else if (txtRetest2STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN1.Focus();
                    txtRetest2STIFFNES_FN1.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_WN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_WN4.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN4.Focus();
                    txtRetest2STIFFNES_WN4.SelectAll();
                }
                else if (txtRetest2STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN1.Focus();
                    txtRetest2STIFFNES_FN1.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_WN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_WN5.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN5.Focus();
                    txtRetest2STIFFNES_WN5.SelectAll();
                }
                else if (txtRetest2STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN1.Focus();
                    txtRetest2STIFFNES_FN1.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_WN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_WN6.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_WN6.Focus();
                    txtRetest2STIFFNES_WN6.SelectAll();
                }
                else if (txtRetest2STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN1.Focus();
                    txtRetest2STIFFNES_FN1.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_WN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN1.Focus();
                    txtRetest2STIFFNES_FN1.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 STIFFNES_F
        private void txtRetest2STIFFNES_FN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_FN2.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN2.Focus();
                    txtRetest2STIFFNES_FN2.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_FN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_FN3.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN3.Focus();
                    txtRetest2STIFFNES_FN3.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_FN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_FN4.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN4.Focus();
                    txtRetest2STIFFNES_FN4.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_FN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_FN5.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN5.Focus();
                    txtRetest2STIFFNES_FN5.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_FN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STIFFNES_FN6.Visibility == Visibility.Visible)
                {
                    txtRetest2STIFFNES_FN6.Focus();
                    txtRetest2STIFFNES_FN6.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STIFFNES_FN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_WN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN1.Focus();
                    txtSaveLabSTIFFNES_WN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab STIFFNES_W
        private void txtSaveLabSTIFFNES_WN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_WN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN2.Focus();
                    txtSaveLabSTIFFNES_WN2.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_WN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_WN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN3.Focus();
                    txtSaveLabSTIFFNES_WN3.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_WN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_WN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN4.Focus();
                    txtSaveLabSTIFFNES_WN4.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_WN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_WN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN5.Focus();
                    txtSaveLabSTIFFNES_WN5.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_WN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_WN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_WN6.Focus();
                    txtSaveLabSTIFFNES_WN6.SelectAll();
                }
                else if (txtSaveLabSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_WN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_FN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN1.Focus();
                    txtSaveLabSTIFFNES_FN1.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab STIFFNES_F
        private void txtSaveLabSTIFFNES_FN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_FN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN2.Focus();
                    txtSaveLabSTIFFNES_FN2.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_FN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_FN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN3.Focus();
                    txtSaveLabSTIFFNES_FN3.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_FN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_FN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN4.Focus();
                    txtSaveLabSTIFFNES_FN4.SelectAll();
                }
                else 
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_FN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_FN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN5.Focus();
                    txtSaveLabSTIFFNES_FN5.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_FN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTIFFNES_FN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTIFFNES_FN6.Focus();
                    txtSaveLabSTIFFNES_FN6.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTIFFNES_FN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSelect.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        // Lost Focus
        #region LostFocus

        #region STIFFNES_F_LostFocus
        private void STIFFNES_F_LostFocus(object sender, RoutedEventArgs e)
        {
            STIFFNES_F();
        }
        #endregion

        #region STIFFNES_W_LostFocus
        private void STIFFNES_W_LostFocus(object sender, RoutedEventArgs e)
        {
            STIFFNES_W();
        }
        #endregion

        #region Retest1STIFFNES_F_LostFocus
        private void Retest1STIFFNES_F_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1STIFFNES_F();
        }
        #endregion

        #region Retest1STIFFNES_W_LostFocus
        private void Retest1STIFFNES_W_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1STIFFNES_W();
        }
        #endregion

        #region Retest2STIFFNES_F_LostFocus
        private void Retest2STIFFNES_F_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2STIFFNES_F();
        }
        #endregion

        #region Retest2STIFFNES_W_LostFocus
        private void Retest2STIFFNES_W_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2STIFFNES_W();
        }
        #endregion

        #region SaveLabSTIFFNES_F_LostFocus
        private void SaveLabSTIFFNES_F_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabSTIFFNES_F();
        }
        #endregion

        #region SaveLabSTIFFNES_W_LostFocus
        private void SaveLabSTIFFNES_W_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabSTIFFNES_W();
        }
        #endregion

        #region STIFFNES_F
        private void STIFFNES_F()
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

                if (!string.IsNullOrEmpty(txtSTIFFNES_FN1.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSTIFFNES_FN1.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_FN2.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSTIFFNES_FN2.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_FN3.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSTIFFNES_FN3.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_FN4.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtSTIFFNES_FN4.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_FN5.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtSTIFFNES_FN5.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_FN6.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtSTIFFNES_FN6.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FN6.Text = string.Empty;
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

                    txtSTIFFNES_FAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSTIFFNES_FAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSTIFFNES_FSpecification.Text))
                {
                    string temp = txtSTIFFNES_FSpecification.Text;

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
                                        txtSTIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSTIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSTIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSTIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSTIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSTIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSTIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSTIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSTIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSTIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSTIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSTIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_FN6.Background = Brushes.White;

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
                                    {
                                        txtSTIFFNES_FN1.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_FN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtSTIFFNES_FN2.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_FN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_FN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtSTIFFNES_FN3.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_FN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtSTIFFNES_FN4.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_FN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtSTIFFNES_FN5.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_FN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtSTIFFNES_FN6.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_FN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_FN6.Background = Brushes.White;

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
                            {
                                txtSTIFFNES_FN1.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_FN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_FN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtSTIFFNES_FN2.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_FN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_FN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtSTIFFNES_FN3.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_FN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_FN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtSTIFFNES_FN4.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_FN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_FN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtSTIFFNES_FN5.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_FN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_FN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtSTIFFNES_FN6.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_FN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_FN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtSTIFFNES_FN1.Background = Brushes.White;
                    txtSTIFFNES_FN2.Background = Brushes.White;
                    txtSTIFFNES_FN3.Background = Brushes.White;
                    txtSTIFFNES_FN4.Background = Brushes.White;
                    txtSTIFFNES_FN5.Background = Brushes.White;
                    txtSTIFFNES_FN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region STIFFNES_W
        private void STIFFNES_W()
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

                if (!string.IsNullOrEmpty(txtSTIFFNES_WN1.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSTIFFNES_WN1.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_WN2.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSTIFFNES_WN2.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_WN3.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSTIFFNES_WN3.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_WN4.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtSTIFFNES_WN4.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_WN5.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtSTIFFNES_WN5.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNES_WN6.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtSTIFFNES_WN6.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WN6.Text = string.Empty;
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

                    txtSTIFFNES_WAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSTIFFNES_WAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSTIFFNES_WSpecification.Text))
                {
                    string temp = txtSTIFFNES_WSpecification.Text;

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
                                        txtSTIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSTIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSTIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSTIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSTIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSTIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSTIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSTIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSTIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSTIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSTIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSTIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtSTIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtSTIFFNES_WN6.Background = Brushes.White;

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
                                    {
                                        txtSTIFFNES_WN1.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_WN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtSTIFFNES_WN2.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_WN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_WN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtSTIFFNES_WN3.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_WN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtSTIFFNES_WN4.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_WN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtSTIFFNES_WN5.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_WN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtSTIFFNES_WN6.Background = Brushes.White;
                                    }
                                    else
                                        txtSTIFFNES_WN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSTIFFNES_WN6.Background = Brushes.White;

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
                            {
                                txtSTIFFNES_WN1.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_WN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_WN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtSTIFFNES_WN2.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_WN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_WN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtSTIFFNES_WN3.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_WN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_WN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtSTIFFNES_WN4.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_WN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_WN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtSTIFFNES_WN5.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_WN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_WN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtSTIFFNES_WN6.Background = Brushes.White;
                            }
                            else
                                txtSTIFFNES_WN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSTIFFNES_WN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtSTIFFNES_WN1.Background = Brushes.White;
                    txtSTIFFNES_WN2.Background = Brushes.White;
                    txtSTIFFNES_WN3.Background = Brushes.White;
                    txtSTIFFNES_WN4.Background = Brushes.White;
                    txtSTIFFNES_WN5.Background = Brushes.White;
                    txtSTIFFNES_WN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1 STIFFNES_F
        private void Retest1STIFFNES_F()
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

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_FN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1STIFFNES_FN1.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_FN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_FN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1STIFFNES_FN2.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_FN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_FN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1STIFFNES_FN3.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_FN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_FN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest1STIFFNES_FN4.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_FN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_FN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest1STIFFNES_FN5.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_FN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_FN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest1STIFFNES_FN6.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_FN6.Text = string.Empty;
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

                    txtRetest1STIFFNES_FAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1STIFFNES_FAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_FSpecification.Text))
                {
                    string temp = txtRetest1STIFFNES_FSpecification.Text;

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
                                        txtRetest1STIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1STIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1STIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1STIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1STIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1STIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1STIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1STIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1STIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1STIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1STIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1STIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_FN6.Background = Brushes.White;

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
                                    {
                                        txtRetest1STIFFNES_FN1.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_FN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtRetest1STIFFNES_FN2.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_FN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_FN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtRetest1STIFFNES_FN3.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_FN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtRetest1STIFFNES_FN4.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_FN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtRetest1STIFFNES_FN5.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_FN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtRetest1STIFFNES_FN6.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_FN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_FN6.Background = Brushes.White;

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
                            {
                                txtRetest1STIFFNES_FN1.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_FN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_FN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtRetest1STIFFNES_FN2.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_FN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_FN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtRetest1STIFFNES_FN3.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_FN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_FN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtRetest1STIFFNES_FN4.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_FN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_FN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtRetest1STIFFNES_FN5.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_FN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_FN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtRetest1STIFFNES_FN6.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_FN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_FN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtRetest1STIFFNES_FN1.Background = Brushes.White;
                    txtRetest1STIFFNES_FN2.Background = Brushes.White;
                    txtRetest1STIFFNES_FN3.Background = Brushes.White;
                    txtRetest1STIFFNES_FN4.Background = Brushes.White;
                    txtRetest1STIFFNES_FN5.Background = Brushes.White;
                    txtRetest1STIFFNES_FN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1 STIFFNES_W
        private void Retest1STIFFNES_W()
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

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_WN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1STIFFNES_WN1.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_WN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_WN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1STIFFNES_WN2.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_WN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_WN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1STIFFNES_WN3.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_WN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_WN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest1STIFFNES_WN4.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_WN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_WN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest1STIFFNES_WN5.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_WN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1STIFFNES_WN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest1STIFFNES_WN6.Text);
                    }
                    else
                    {
                        txtRetest1STIFFNES_WN6.Text = string.Empty;
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

                    txtRetest1STIFFNES_WAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1STIFFNES_WAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1STIFFNES_WSpecification.Text))
                {
                    string temp = txtRetest1STIFFNES_WSpecification.Text;

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
                                        txtRetest1STIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1STIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1STIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1STIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1STIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1STIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1STIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1STIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1STIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1STIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1STIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1STIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1STIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1STIFFNES_WN6.Background = Brushes.White;

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
                                    {
                                        txtRetest1STIFFNES_WN1.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_WN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtRetest1STIFFNES_WN2.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_WN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_WN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtRetest1STIFFNES_WN3.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_WN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtRetest1STIFFNES_WN4.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_WN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtRetest1STIFFNES_WN5.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_WN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtRetest1STIFFNES_WN6.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STIFFNES_WN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1STIFFNES_WN6.Background = Brushes.White;

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
                            {
                                txtRetest1STIFFNES_WN1.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_WN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_WN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtRetest1STIFFNES_WN2.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_WN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_WN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtRetest1STIFFNES_WN3.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_WN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_WN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtRetest1STIFFNES_WN4.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_WN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_WN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtRetest1STIFFNES_WN5.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_WN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_WN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtRetest1STIFFNES_WN6.Background = Brushes.White;
                            }
                            else
                                txtRetest1STIFFNES_WN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1STIFFNES_WN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtRetest1STIFFNES_WN1.Background = Brushes.White;
                    txtRetest1STIFFNES_WN2.Background = Brushes.White;
                    txtRetest1STIFFNES_WN3.Background = Brushes.White;
                    txtRetest1STIFFNES_WN4.Background = Brushes.White;
                    txtRetest1STIFFNES_WN5.Background = Brushes.White;
                    txtRetest1STIFFNES_WN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2 STIFFNES_F
        private void Retest2STIFFNES_F()
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

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_FN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2STIFFNES_FN1.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_FN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_FN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2STIFFNES_FN2.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_FN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_FN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2STIFFNES_FN3.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_FN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_FN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest2STIFFNES_FN4.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_FN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_FN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest2STIFFNES_FN5.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_FN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_FN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest2STIFFNES_FN6.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_FN6.Text = string.Empty;
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

                    txtRetest2STIFFNES_FAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2STIFFNES_FAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_FSpecification.Text))
                {
                    string temp = txtRetest2STIFFNES_FSpecification.Text;

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
                                        txtRetest2STIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2STIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2STIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2STIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2STIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2STIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2STIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2STIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2STIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2STIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2STIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2STIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_FN6.Background = Brushes.White;

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
                                    {
                                        txtRetest2STIFFNES_FN1.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_FN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtRetest2STIFFNES_FN2.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_FN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_FN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtRetest2STIFFNES_FN3.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_FN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtRetest2STIFFNES_FN4.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_FN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtRetest2STIFFNES_FN5.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_FN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtRetest2STIFFNES_FN6.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_FN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_FN6.Background = Brushes.White;

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
                            {
                                txtRetest2STIFFNES_FN1.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_FN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_FN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtRetest2STIFFNES_FN2.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_FN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_FN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtRetest2STIFFNES_FN3.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_FN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_FN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtRetest2STIFFNES_FN4.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_FN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_FN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtRetest2STIFFNES_FN5.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_FN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_FN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtRetest2STIFFNES_FN6.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_FN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_FN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtRetest2STIFFNES_FN1.Background = Brushes.White;
                    txtRetest2STIFFNES_FN2.Background = Brushes.White;
                    txtRetest2STIFFNES_FN3.Background = Brushes.White;
                    txtRetest2STIFFNES_FN4.Background = Brushes.White;
                    txtRetest2STIFFNES_FN5.Background = Brushes.White;
                    txtRetest2STIFFNES_FN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2 STIFFNES_W
        private void Retest2STIFFNES_W()
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

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_WN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2STIFFNES_WN1.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_WN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_WN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2STIFFNES_WN2.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_WN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_WN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2STIFFNES_WN3.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_WN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_WN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest2STIFFNES_WN4.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_WN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_WN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest2STIFFNES_WN5.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_WN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2STIFFNES_WN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest2STIFFNES_WN6.Text);
                    }
                    else
                    {
                        txtRetest2STIFFNES_WN6.Text = string.Empty;
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

                    txtRetest2STIFFNES_WAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2STIFFNES_WAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2STIFFNES_WSpecification.Text))
                {
                    string temp = txtRetest2STIFFNES_WSpecification.Text;

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
                                        txtRetest2STIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2STIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2STIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2STIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2STIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2STIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2STIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2STIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2STIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2STIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2STIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2STIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2STIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2STIFFNES_WN6.Background = Brushes.White;

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
                                    {
                                        txtRetest2STIFFNES_WN1.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_WN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtRetest2STIFFNES_WN2.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_WN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_WN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtRetest2STIFFNES_WN3.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_WN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtRetest2STIFFNES_WN4.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_WN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtRetest2STIFFNES_WN5.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_WN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtRetest2STIFFNES_WN6.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STIFFNES_WN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2STIFFNES_WN6.Background = Brushes.White;

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
                            {
                                txtRetest2STIFFNES_WN1.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_WN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_WN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtRetest2STIFFNES_WN2.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_WN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_WN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtRetest2STIFFNES_WN3.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_WN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_WN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtRetest2STIFFNES_WN4.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_WN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_WN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtRetest2STIFFNES_WN5.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_WN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_WN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtRetest2STIFFNES_WN6.Background = Brushes.White;
                            }
                            else
                                txtRetest2STIFFNES_WN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2STIFFNES_WN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtRetest2STIFFNES_WN1.Background = Brushes.White;
                    txtRetest2STIFFNES_WN2.Background = Brushes.White;
                    txtRetest2STIFFNES_WN3.Background = Brushes.White;
                    txtRetest2STIFFNES_WN4.Background = Brushes.White;
                    txtRetest2STIFFNES_WN5.Background = Brushes.White;
                    txtRetest2STIFFNES_WN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLab STIFFNES_F
        private void SaveLabSTIFFNES_F()
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

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_FN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabSTIFFNES_FN1.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_FN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_FN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabSTIFFNES_FN2.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_FN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_FN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabSTIFFNES_FN3.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_FN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_FN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtSaveLabSTIFFNES_FN4.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_FN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_FN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtSaveLabSTIFFNES_FN5.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_FN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_FN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtSaveLabSTIFFNES_FN6.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_FN6.Text = string.Empty;
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

                    txtSaveLabSTIFFNES_FAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabSTIFFNES_FAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FSpecification.Text))
                {
                    string temp = txtSaveLabSTIFFNES_FSpecification.Text;

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
                                        txtSaveLabSTIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabSTIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabSTIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                txtSaveLabSTIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabSTIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabSTIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabSTIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabSTIFFNES_FN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabSTIFFNES_FN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabSTIFFNES_FN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabSTIFFNES_FN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabSTIFFNES_FN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabSTIFFNES_FN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN6.Background = Brushes.White;

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
                                    {
                                        txtSaveLabSTIFFNES_FN1.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_FN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_FN2.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_FN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_FN3.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_FN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_FN4.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_FN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_FN5.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_FN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_FN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_FN6.Background = Brushes.White;

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
                            {
                                txtSaveLabSTIFFNES_FN1.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_FN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_FN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtSaveLabSTIFFNES_FN2.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_FN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_FN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtSaveLabSTIFFNES_FN3.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_FN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_FN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtSaveLabSTIFFNES_FN4.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_FN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_FN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtSaveLabSTIFFNES_FN5.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_FN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_FN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_FN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtSaveLabSTIFFNES_FN1.Background = Brushes.White;
                    txtSaveLabSTIFFNES_FN2.Background = Brushes.White;
                    txtSaveLabSTIFFNES_FN3.Background = Brushes.White;
                    txtSaveLabSTIFFNES_FN4.Background = Brushes.White;
                    txtSaveLabSTIFFNES_FN5.Background = Brushes.White;
                    txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLab STIFFNES_W
        private void SaveLabSTIFFNES_W()
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

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_WN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabSTIFFNES_WN1.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_WN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_WN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabSTIFFNES_WN2.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_WN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_WN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabSTIFFNES_WN3.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_WN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_WN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtSaveLabSTIFFNES_WN4.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_WN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_WN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtSaveLabSTIFFNES_WN5.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_WN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTIFFNES_WN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtSaveLabSTIFFNES_WN6.Text);
                    }
                    else
                    {
                        txtSaveLabSTIFFNES_WN6.Text = string.Empty;
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

                    txtSaveLabSTIFFNES_WAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabSTIFFNES_WAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WSpecification.Text))
                {
                    string temp = txtSaveLabSTIFFNES_WSpecification.Text;

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
                                        txtSaveLabSTIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN1.Background = Brushes.White;


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabSTIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabSTIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabSTIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabSTIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN5.Background = Brushes.White;


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabSTIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabSTIFFNES_WN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabSTIFFNES_WN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN2.Background = Brushes.White;


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabSTIFFNES_WN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN3.Background = Brushes.White;


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabSTIFFNES_WN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN4.Background = Brushes.White;


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabSTIFFNES_WN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabSTIFFNES_WN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN6.Background = Brushes.White;

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
                                    {
                                        txtSaveLabSTIFFNES_WN1.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_WN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_WN2.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_WN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_WN3.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_WN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_WN4.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_WN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_WN5.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_WN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTIFFNES_WN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabSTIFFNES_WN6.Background = Brushes.White;

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
                            {
                                txtSaveLabSTIFFNES_WN1.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_WN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_WN1.Background = Brushes.White;


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                txtSaveLabSTIFFNES_WN2.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_WN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_WN2.Background = Brushes.White;


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                txtSaveLabSTIFFNES_WN3.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_WN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_WN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                txtSaveLabSTIFFNES_WN4.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_WN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_WN4.Background = Brushes.White;


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                txtSaveLabSTIFFNES_WN5.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_WN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_WN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTIFFNES_WN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
                    }
                }
                else
                {
                    txtSaveLabSTIFFNES_WN1.Background = Brushes.White;
                    txtSaveLabSTIFFNES_WN2.Background = Brushes.White;
                    txtSaveLabSTIFFNES_WN3.Background = Brushes.White;
                    txtSaveLabSTIFFNES_WN4.Background = Brushes.White;
                    txtSaveLabSTIFFNES_WN5.Background = Brushes.White;
                    txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
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

            N_STIFFNES_F(null);
            N_STIFFNES_W(null);

            Retest1STIFFNES_F(null);
            Retest1STIFFNES_W(null);
            Retest2STIFFNES_F(null);
            Retest2STIFFNES_W(null);

            SaveLabSTIFFNES_F(null);
            SaveLabSTIFFNES_W(null);

            txtSaveLabSTIFFNES_WN1.Focus();
            txtSaveLabSTIFFNES_WN1.SelectAll();
        }

        #endregion

        #region setDefForeground
        private void setDefForeground()
        {
            txtSTIFFNES_FN1.Background = Brushes.White;
            txtSTIFFNES_FN2.Background = Brushes.White;
            txtSTIFFNES_FN3.Background = Brushes.White;
            txtSTIFFNES_FN4.Background = Brushes.White;
            txtSTIFFNES_FN5.Background = Brushes.White;
            txtSTIFFNES_FN6.Background = Brushes.White;
            txtSTIFFNES_WN1.Background = Brushes.White;
            txtSTIFFNES_WN2.Background = Brushes.White;
            txtSTIFFNES_WN3.Background = Brushes.White;
            txtSTIFFNES_WN4.Background = Brushes.White;
            txtSTIFFNES_WN5.Background = Brushes.White;
            txtSTIFFNES_WN6.Background = Brushes.White;

            txtRetest1STIFFNES_FN1.Background = Brushes.White;
            txtRetest1STIFFNES_FN2.Background = Brushes.White;
            txtRetest1STIFFNES_FN3.Background = Brushes.White;
            txtRetest1STIFFNES_FN4.Background = Brushes.White;
            txtRetest1STIFFNES_FN5.Background = Brushes.White;
            txtRetest1STIFFNES_FN6.Background = Brushes.White;
            txtRetest1STIFFNES_WN1.Background = Brushes.White;
            txtRetest1STIFFNES_WN2.Background = Brushes.White;
            txtRetest1STIFFNES_WN3.Background = Brushes.White;
            txtRetest1STIFFNES_WN4.Background = Brushes.White;
            txtRetest1STIFFNES_WN5.Background = Brushes.White;
            txtRetest1STIFFNES_WN6.Background = Brushes.White;

            txtRetest2STIFFNES_FN1.Background = Brushes.White;
            txtRetest2STIFFNES_FN2.Background = Brushes.White;
            txtRetest2STIFFNES_FN3.Background = Brushes.White;
            txtRetest2STIFFNES_FN4.Background = Brushes.White;
            txtRetest2STIFFNES_FN5.Background = Brushes.White;
            txtRetest2STIFFNES_FN6.Background = Brushes.White;
            txtRetest2STIFFNES_WN1.Background = Brushes.White;
            txtRetest2STIFFNES_WN2.Background = Brushes.White;
            txtRetest2STIFFNES_WN3.Background = Brushes.White;
            txtRetest2STIFFNES_WN4.Background = Brushes.White;
            txtRetest2STIFFNES_WN5.Background = Brushes.White;
            txtRetest2STIFFNES_WN6.Background = Brushes.White;

            txtSaveLabSTIFFNES_FN1.Background = Brushes.White;
            txtSaveLabSTIFFNES_FN2.Background = Brushes.White;
            txtSaveLabSTIFFNES_FN3.Background = Brushes.White;
            txtSaveLabSTIFFNES_FN4.Background = Brushes.White;
            txtSaveLabSTIFFNES_FN5.Background = Brushes.White;
            txtSaveLabSTIFFNES_FN6.Background = Brushes.White;
            txtSaveLabSTIFFNES_WN1.Background = Brushes.White;
            txtSaveLabSTIFFNES_WN2.Background = Brushes.White;
            txtSaveLabSTIFFNES_WN3.Background = Brushes.White;
            txtSaveLabSTIFFNES_WN4.Background = Brushes.White;
            txtSaveLabSTIFFNES_WN5.Background = Brushes.White;
            txtSaveLabSTIFFNES_WN6.Background = Brushes.White;
        }
        #endregion

        #region LAB_GETSTIFFNESSDATA
        private bool LAB_GETSTIFFNESSDATA(string P_ITMCODE, string P_WEAVINGLOG)
        {
            bool chkLoad = true;

            try
            {
                spRetest1.Visibility = Visibility.Collapsed;
                spRetest2.Visibility = Visibility.Collapsed;

                List<LAB_GETSTIFFNESSDATA> results = LabDataPDFDataService.Instance.LAB_GETSTIFFNESSDATA(P_ITMCODE, P_WEAVINGLOG);
                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        decimal? STIFFNESS_W1 = null;
                        decimal? STIFFNESS_W2 = null;
                        decimal? STIFFNESS_W3 = null;

                        decimal? STIFFNESS_F1 = null;
                        decimal? STIFFNESS_F2 = null;
                        decimal? STIFFNESS_F3 = null;

                        #region Get Data

                        for (int page = 0; page <= results.Count - 1; page++)
                        {
                            if (page == 0)
                            {
                                if (results[page].STIFFNESS_W1 != null)
                                {
                                    STIFFNESS_W1 = results[page].STIFFNESS_W1;
                                    txtSTIFFNES_WN1.Text = results[page].STIFFNESS_W1.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_W2 != null)
                                {
                                    STIFFNESS_W2 = results[page].STIFFNESS_W2;
                                    txtSTIFFNES_WN2.Text = results[page].STIFFNESS_W2.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_W3 != null)
                                {
                                    STIFFNESS_W3 = results[page].STIFFNESS_W3;
                                    txtSTIFFNES_WN3.Text = results[page].STIFFNESS_W3.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F1 != null)
                                {
                                    STIFFNESS_F1 = results[page].STIFFNESS_F1;
                                    txtSTIFFNES_FN1.Text = results[page].STIFFNESS_F1.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F2 != null)
                                {
                                    STIFFNESS_F2 = results[page].STIFFNESS_F2;
                                    txtSTIFFNES_FN2.Text = results[page].STIFFNESS_F2.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F3 != null)
                                {
                                    STIFFNESS_F3 = results[page].STIFFNESS_F3;
                                    txtSTIFFNES_FN3.Text = results[page].STIFFNESS_F3.Value.ToString("#,##0.##");
                                }
                            }
                            else if (page == 1)
                            {
                                spRetest1.Visibility = Visibility.Visible;

                                if (results[page].STIFFNESS_W1 != null)
                                {
                                    STIFFNESS_W1 = results[page].STIFFNESS_W1;
                                    txtRetest1STIFFNES_WN1.Text = results[page].STIFFNESS_W1.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_W2 != null)
                                {
                                    STIFFNESS_W2 = results[page].STIFFNESS_W2;
                                    txtRetest1STIFFNES_WN2.Text = results[page].STIFFNESS_W2.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_W3 != null)
                                {
                                    STIFFNESS_W3 = results[page].STIFFNESS_W3;
                                    txtRetest1STIFFNES_WN3.Text = results[page].STIFFNESS_W3.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F1 != null)
                                {
                                    STIFFNESS_F1 = results[page].STIFFNESS_F1;
                                    txtRetest1STIFFNES_FN1.Text = results[page].STIFFNESS_F1.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F2 != null)
                                {
                                    STIFFNESS_F2 = results[page].STIFFNESS_F2;
                                    txtRetest1STIFFNES_FN2.Text = results[page].STIFFNESS_F2.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F3 != null)
                                {
                                    STIFFNESS_F3 = results[page].STIFFNESS_F3;
                                    txtRetest1STIFFNES_FN3.Text = results[page].STIFFNESS_F3.Value.ToString("#,##0.##");
                                }
                            }
                            else if (page == 2)
                            {
                                spRetest2.Visibility = Visibility.Visible;

                                if (results[page].STIFFNESS_W1 != null)
                                {
                                    STIFFNESS_W1 = results[page].STIFFNESS_W1;
                                    txtRetest2STIFFNES_WN1.Text = results[page].STIFFNESS_W1.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_W2 != null)
                                {
                                    STIFFNESS_W2 = results[page].STIFFNESS_W2;
                                    txtRetest2STIFFNES_WN2.Text = results[page].STIFFNESS_W2.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_W3 != null)
                                {
                                    STIFFNESS_W3 = results[page].STIFFNESS_W3;
                                    txtRetest2STIFFNES_WN3.Text = results[page].STIFFNESS_W3.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F1 != null)
                                {
                                    STIFFNESS_F1 = results[page].STIFFNESS_F1;
                                    txtRetest2STIFFNES_FN1.Text = results[page].STIFFNESS_F1.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F2 != null)
                                {
                                    STIFFNESS_F2 = results[page].STIFFNESS_F2;
                                    txtRetest2STIFFNES_FN2.Text = results[page].STIFFNESS_F2.Value.ToString("#,##0.##");
                                }
                                if (results[page].STIFFNESS_F3 != null)
                                {
                                    STIFFNESS_F3 = results[page].STIFFNESS_F3;
                                    txtRetest2STIFFNES_FN3.Text = results[page].STIFFNESS_F3.Value.ToString("#,##0.##");
                                }
                            }
                        }

                        #endregion

                        if (STIFFNESS_W1 != null) { txtSaveLabSTIFFNES_WN1.Text = STIFFNESS_W1.Value.ToString("#,##0.##"); }
                        if (STIFFNESS_W2 != null) { txtSaveLabSTIFFNES_WN2.Text = STIFFNESS_W2.Value.ToString("#,##0.##"); }
                        if (STIFFNESS_W3 != null) { txtSaveLabSTIFFNES_WN3.Text = STIFFNESS_W3.Value.ToString("#,##0.##"); }

                        if (STIFFNESS_F1 != null) { txtSaveLabSTIFFNES_FN1.Text = STIFFNESS_F1.Value.ToString("#,##0.##"); }
                        if (STIFFNESS_F2 != null) { txtSaveLabSTIFFNES_FN2.Text = STIFFNESS_F2.Value.ToString("#,##0.##"); }
                        if (STIFFNESS_F3 != null) { txtSaveLabSTIFFNES_FN3.Text = STIFFNESS_F3.Value.ToString("#,##0.##"); }

                        STIFFNES_F();
                        STIFFNES_W();
                        Retest1STIFFNES_W();
                        Retest1STIFFNES_F();
                        Retest2STIFFNES_W();
                        Retest2STIFFNES_F();
                        SaveLabSTIFFNES_W();
                        SaveLabSTIFFNES_F();
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
                        N_Head(results[0].STIFFNESS_W);
                        
                        N_STIFFNES_F(results[0].STIFFNESS_F);
                        N_STIFFNES_W(results[0].STIFFNESS_W);

                        Retest1STIFFNES_F(results[0].STIFFNESS_F);
                        Retest1STIFFNES_W(results[0].STIFFNESS_W);
                        Retest2STIFFNES_F(results[0].STIFFNESS_F);
                        Retest2STIFFNES_W(results[0].STIFFNESS_W);

                        SaveLabSTIFFNES_F(results[0].STIFFNESS_F);
                        SaveLabSTIFFNES_W(results[0].STIFFNESS_W);
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

                txtSTIFFNES_FSpecification.Text = string.Empty;
                txtSTIFFNES_WSpecification.Text = string.Empty;

                txtRetest1STIFFNES_FSpecification.Text = string.Empty;
                txtRetest1STIFFNES_WSpecification.Text = string.Empty;

                txtRetest2STIFFNES_FSpecification.Text = string.Empty;
                txtRetest2STIFFNES_WSpecification.Text = string.Empty;

                txtSaveLabSTIFFNES_FSpecification.Text = string.Empty;
                txtSaveLabSTIFFNES_WSpecification.Text = string.Empty;


                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        txtSTIFFNES_FSpecification.Text = results[0].STIFFNESS_F_Spe;
                        txtSTIFFNES_WSpecification.Text = results[0].STIFFNESS_W_Spe;

                        txtRetest1STIFFNES_FSpecification.Text = results[0].STIFFNESS_F_Spe;
                        txtRetest1STIFFNES_WSpecification.Text = results[0].STIFFNESS_W_Spe;

                        txtRetest2STIFFNES_FSpecification.Text = results[0].STIFFNESS_F_Spe;
                        txtRetest2STIFFNES_WSpecification.Text = results[0].STIFFNESS_W_Spe;

                        txtSaveLabSTIFFNES_FSpecification.Text = results[0].STIFFNESS_F_Spe;
                        txtSaveLabSTIFFNES_WSpecification.Text = results[0].STIFFNESS_W_Spe;
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
                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN1.Text))
                        saveLabSTIFFNES_WN1 = decimal.Parse(txtSaveLabSTIFFNES_WN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN2.Text))
                        saveLabSTIFFNES_WN2 = decimal.Parse(txtSaveLabSTIFFNES_WN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN3.Text))
                        saveLabSTIFFNES_WN3 = decimal.Parse(txtSaveLabSTIFFNES_WN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN4.Text))
                        saveLabSTIFFNES_WN4 = decimal.Parse(txtSaveLabSTIFFNES_WN4.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN5.Text))
                        saveLabSTIFFNES_WN5 = decimal.Parse(txtSaveLabSTIFFNES_WN5.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN6.Text))
                        saveLabSTIFFNES_WN6 = decimal.Parse(txtSaveLabSTIFFNES_WN6.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN1.Text))
                        saveLabSTIFFNES_FN1 = decimal.Parse(txtSaveLabSTIFFNES_FN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN2.Text))
                        saveLabSTIFFNES_FN2 = decimal.Parse(txtSaveLabSTIFFNES_FN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN3.Text))
                        saveLabSTIFFNES_FN3 = decimal.Parse(txtSaveLabSTIFFNES_FN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN4.Text))
                        saveLabSTIFFNES_FN4 = decimal.Parse(txtSaveLabSTIFFNES_FN4.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN5.Text))
                        saveLabSTIFFNES_FN5 = decimal.Parse(txtSaveLabSTIFFNES_FN5.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN6.Text))
                        saveLabSTIFFNES_FN6 = decimal.Parse(txtSaveLabSTIFFNES_FN6.Text);

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

                    if (txtSaveLabSTIFFNES_WN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTIFFNES_WN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTIFFNES_WN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_WN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTIFFNES_FN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTIFFNES_FN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTIFFNES_FN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTIFFNES_FN3.Text))
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

        #region N_STIFFNES_W
        private void N_STIFFNES_W(decimal? n)
        {
            decimal? STIFFNES_W = n;

            txtSTIFFNES_WSpecification.Text = string.Empty;
            txtSTIFFNES_WN1.Text = string.Empty;
            txtSTIFFNES_WN2.Text = string.Empty;
            txtSTIFFNES_WN3.Text = string.Empty;
            txtSTIFFNES_WN4.Text = string.Empty;
            txtSTIFFNES_WN5.Text = string.Empty;
            txtSTIFFNES_WN6.Text = string.Empty;
            txtSTIFFNES_WAve.Text = string.Empty;

            if (STIFFNES_W != null && STIFFNES_W > 0)
            {

                if (STIFFNES_W == 6)
                {
                    txtSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN4.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN5.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_W == 5)
                {
                    txtSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN4.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN5.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 4)
                {
                    txtSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN4.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 3)
                {
                    txtSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 2)
                {
                    txtSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 1)
                {
                    txtSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_WN2.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSTIFFNES_WN1.Visibility = Visibility.Collapsed;
                txtSTIFFNES_WN2.Visibility = Visibility.Collapsed;
                txtSTIFFNES_WN3.Visibility = Visibility.Collapsed;
                txtSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                txtSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                txtSTIFFNES_WN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region N_STIFFNES_F
        private void N_STIFFNES_F(decimal? n)
        {
            decimal? STIFFNES_F = n;

            txtSTIFFNES_FSpecification.Text = string.Empty;
            txtSTIFFNES_FN1.Text = string.Empty;
            txtSTIFFNES_FN2.Text = string.Empty;
            txtSTIFFNES_FN3.Text = string.Empty;
            txtSTIFFNES_FN4.Text = string.Empty;
            txtSTIFFNES_FN5.Text = string.Empty;
            txtSTIFFNES_FN6.Text = string.Empty;
            txtSTIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_F != null && STIFFNES_F > 0)
            {

                if (STIFFNES_F == 6)
                {
                    txtSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN4.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN5.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_F == 5)
                {
                    txtSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN4.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN5.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 4)
                {
                    txtSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN4.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 3)
                {
                    txtSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 2)
                {
                    txtSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 1)
                {
                    txtSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSTIFFNES_FN2.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSTIFFNES_FN1.Visibility = Visibility.Collapsed;
                txtSTIFFNES_FN2.Visibility = Visibility.Collapsed;
                txtSTIFFNES_FN3.Visibility = Visibility.Collapsed;
                txtSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                txtSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                txtSTIFFNES_FN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1 N_STIFFNES_W
        private void Retest1STIFFNES_W(decimal? n)
        {
            decimal? STIFFNES_W = n;

            txtRetest1STIFFNES_WSpecification.Text = string.Empty;
            txtRetest1STIFFNES_WN1.Text = string.Empty;
            txtRetest1STIFFNES_WN2.Text = string.Empty;
            txtRetest1STIFFNES_WN3.Text = string.Empty;
            txtRetest1STIFFNES_WN4.Text = string.Empty;
            txtRetest1STIFFNES_WN5.Text = string.Empty;
            txtRetest1STIFFNES_WN6.Text = string.Empty;
            txtRetest1STIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_W != null && STIFFNES_W > 0)
            {

                if (STIFFNES_W == 6)
                {
                    txtRetest1STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN4.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN5.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_W == 5)
                {
                    txtRetest1STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN4.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN5.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 4)
                {
                    txtRetest1STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN4.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 3)
                {
                    txtRetest1STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 2)
                {
                    txtRetest1STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 1)
                {
                    txtRetest1STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_WN2.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1STIFFNES_WN1.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_WN2.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_WN3.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_WN4.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_WN5.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_WN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1 N_STIFFNES_F
        private void Retest1STIFFNES_F(decimal? n)
        {
            decimal? STIFFNES_F = n;

            txtRetest1STIFFNES_FSpecification.Text = string.Empty;
            txtRetest1STIFFNES_FN1.Text = string.Empty;
            txtRetest1STIFFNES_FN2.Text = string.Empty;
            txtRetest1STIFFNES_FN3.Text = string.Empty;
            txtRetest1STIFFNES_FN4.Text = string.Empty;
            txtRetest1STIFFNES_FN5.Text = string.Empty;
            txtRetest1STIFFNES_FN6.Text = string.Empty;
            txtRetest1STIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_F != null && STIFFNES_F > 0)
            {

                if (STIFFNES_F == 6)
                {
                    txtRetest1STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN4.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN5.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_F == 5)
                {
                    txtRetest1STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN4.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN5.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 4)
                {
                    txtRetest1STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN4.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 3)
                {
                    txtRetest1STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 2)
                {
                    txtRetest1STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 1)
                {
                    txtRetest1STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest1STIFFNES_FN2.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest1STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1STIFFNES_FN1.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_FN2.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_FN3.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_FN4.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_FN5.Visibility = Visibility.Collapsed;
                txtRetest1STIFFNES_FN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2 N_STIFFNES_W
        private void Retest2STIFFNES_W(decimal? n)
        {
            decimal? STIFFNES_W = n;

            txtRetest2STIFFNES_WSpecification.Text = string.Empty;
            txtRetest2STIFFNES_WN1.Text = string.Empty;
            txtRetest2STIFFNES_WN2.Text = string.Empty;
            txtRetest2STIFFNES_WN3.Text = string.Empty;
            txtRetest2STIFFNES_WN4.Text = string.Empty;
            txtRetest2STIFFNES_WN5.Text = string.Empty;
            txtRetest2STIFFNES_WN6.Text = string.Empty;
            txtRetest2STIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_W != null && STIFFNES_W > 0)
            {

                if (STIFFNES_W == 6)
                {
                    txtRetest2STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN4.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN5.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_W == 5)
                {
                    txtRetest2STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN4.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN5.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 4)
                {
                    txtRetest2STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN4.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 3)
                {
                    txtRetest2STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 2)
                {
                    txtRetest2STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 1)
                {
                    txtRetest2STIFFNES_WN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_WN2.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2STIFFNES_WN1.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_WN2.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_WN3.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_WN4.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_WN5.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_WN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2 N_STIFFNES_F
        private void Retest2STIFFNES_F(decimal? n)
        {
            decimal? STIFFNES_F = n;

            txtRetest2STIFFNES_FSpecification.Text = string.Empty;
            txtRetest2STIFFNES_FN1.Text = string.Empty;
            txtRetest2STIFFNES_FN2.Text = string.Empty;
            txtRetest2STIFFNES_FN3.Text = string.Empty;
            txtRetest2STIFFNES_FN4.Text = string.Empty;
            txtRetest2STIFFNES_FN5.Text = string.Empty;
            txtRetest2STIFFNES_FN6.Text = string.Empty;
            txtRetest2STIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_F != null && STIFFNES_F > 0)
            {

                if (STIFFNES_F == 6)
                {
                    txtRetest2STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN4.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN5.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_F == 5)
                {
                    txtRetest2STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN4.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN5.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 4)
                {
                    txtRetest2STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN4.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 3)
                {
                    txtRetest2STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN3.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 2)
                {
                    txtRetest2STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN2.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 1)
                {
                    txtRetest2STIFFNES_FN1.Visibility = Visibility.Visible;
                    txtRetest2STIFFNES_FN2.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtRetest2STIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2STIFFNES_FN1.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_FN2.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_FN3.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_FN4.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_FN5.Visibility = Visibility.Collapsed;
                txtRetest2STIFFNES_FN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLab N_STIFFNES_W
        private void SaveLabSTIFFNES_W(decimal? n)
        {
            decimal? STIFFNES_W = n;

            txtSaveLabSTIFFNES_WSpecification.Text = string.Empty;
            txtSaveLabSTIFFNES_WN1.Text = string.Empty;
            txtSaveLabSTIFFNES_WN2.Text = string.Empty;
            txtSaveLabSTIFFNES_WN3.Text = string.Empty;
            txtSaveLabSTIFFNES_WN4.Text = string.Empty;
            txtSaveLabSTIFFNES_WN5.Text = string.Empty;
            txtSaveLabSTIFFNES_WN6.Text = string.Empty;
            txtSaveLabSTIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_W != null && STIFFNES_W > 0)
            {

                if (STIFFNES_W == 6)
                {
                    txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_W == 5)
                {
                    txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 4)
                {
                    txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 3)
                {
                    txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 2)
                {
                    txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_W == 1)
                {
                    txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabSTIFFNES_WN1.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_WN2.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_WN3.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_WN4.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_WN5.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_WN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLab N_STIFFNES_F
        private void SaveLabSTIFFNES_F(decimal? n)
        {
            decimal? STIFFNES_F = n;

            txtSaveLabSTIFFNES_FSpecification.Text = string.Empty;
            txtSaveLabSTIFFNES_FN1.Text = string.Empty;
            txtSaveLabSTIFFNES_FN2.Text = string.Empty;
            txtSaveLabSTIFFNES_FN3.Text = string.Empty;
            txtSaveLabSTIFFNES_FN4.Text = string.Empty;
            txtSaveLabSTIFFNES_FN5.Text = string.Empty;
            txtSaveLabSTIFFNES_FN6.Text = string.Empty;
            txtSaveLabSTIFFNES_FAve.Text = string.Empty;

            if (STIFFNES_F != null && STIFFNES_F > 0)
            {

                if (STIFFNES_F == 6)
                {
                    txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Visible;
                }
                else if (STIFFNES_F == 5)
                {
                    txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 4)
                {
                    txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 3)
                {
                    txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 2)
                {
                    txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
                else if (STIFFNES_F == 1)
                {
                    txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Visible;
                    txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabSTIFFNES_FN1.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_FN2.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_FN3.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_FN4.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_FN5.Visibility = Visibility.Collapsed;
                txtSaveLabSTIFFNES_FN6.Visibility = Visibility.Collapsed;
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
            #region STIFFNES_W
            txtSTIFFNES_WN1.IsReadOnly = true;
            txtSTIFFNES_WN2.IsReadOnly = true;
            txtSTIFFNES_WN3.IsReadOnly = true;
            txtSTIFFNES_WN4.IsReadOnly = true;
            txtSTIFFNES_WN5.IsReadOnly = true;
            txtSTIFFNES_WN6.IsReadOnly = true;

            txtRetest1STIFFNES_WN1.IsReadOnly = true;
            txtRetest1STIFFNES_WN2.IsReadOnly = true;
            txtRetest1STIFFNES_WN3.IsReadOnly = true;
            txtRetest1STIFFNES_WN4.IsReadOnly = true;
            txtRetest1STIFFNES_WN5.IsReadOnly = true;
            txtRetest1STIFFNES_WN6.IsReadOnly = true;

            txtRetest2STIFFNES_WN1.IsReadOnly = true;
            txtRetest2STIFFNES_WN2.IsReadOnly = true;
            txtRetest2STIFFNES_WN3.IsReadOnly = true;
            txtRetest2STIFFNES_WN4.IsReadOnly = true;
            txtRetest2STIFFNES_WN5.IsReadOnly = true;
            txtRetest2STIFFNES_WN6.IsReadOnly = true;

            //txtSaveLabSTIFFNES_WN1.IsReadOnly = true;
            //txtSaveLabSTIFFNES_WN2.IsReadOnly = true;
            //txtSaveLabSTIFFNES_WN3.IsReadOnly = true;
            //txtSaveLabSTIFFNES_WN4.IsReadOnly = true;
            //txtSaveLabSTIFFNES_WN5.IsReadOnly = true;
            //txtSaveLabSTIFFNES_WN6.IsReadOnly = true;
            #endregion

            #region STIFFNES_F
            txtSTIFFNES_FN1.IsReadOnly = true;
            txtSTIFFNES_FN2.IsReadOnly = true;
            txtSTIFFNES_FN3.IsReadOnly = true;
            txtSTIFFNES_FN4.IsReadOnly = true;
            txtSTIFFNES_FN5.IsReadOnly = true;
            txtSTIFFNES_FN6.IsReadOnly = true;

            txtRetest1STIFFNES_FN1.IsReadOnly = true;
            txtRetest1STIFFNES_FN2.IsReadOnly = true;
            txtRetest1STIFFNES_FN3.IsReadOnly = true;
            txtRetest1STIFFNES_FN4.IsReadOnly = true;
            txtRetest1STIFFNES_FN5.IsReadOnly = true;
            txtRetest1STIFFNES_FN6.IsReadOnly = true;

            txtRetest2STIFFNES_FN1.IsReadOnly = true;
            txtRetest2STIFFNES_FN2.IsReadOnly = true;
            txtRetest2STIFFNES_FN3.IsReadOnly = true;
            txtRetest2STIFFNES_FN4.IsReadOnly = true;
            txtRetest2STIFFNES_FN5.IsReadOnly = true;
            txtRetest2STIFFNES_FN6.IsReadOnly = true;

            //txtSaveLabSTIFFNES_FN1.IsReadOnly = true;
            //txtSaveLabSTIFFNES_FN2.IsReadOnly = true;
            //txtSaveLabSTIFFNES_FN3.IsReadOnly = true;
            //txtSaveLabSTIFFNES_FN4.IsReadOnly = true;
            //txtSaveLabSTIFFNES_FN5.IsReadOnly = true;
            //txtSaveLabSTIFFNES_FN6.IsReadOnly = true;
            #endregion
        }
        #endregion

        #endregion

        #region Public Methods

        #region STIFFNES_WN

        decimal? saveLabSTIFFNES_WN1 = null;
        decimal? saveLabSTIFFNES_WN2 = null;
        decimal? saveLabSTIFFNES_WN3 = null;
        decimal? saveLabSTIFFNES_WN4 = null;
        decimal? saveLabSTIFFNES_WN5 = null;
        decimal? saveLabSTIFFNES_WN6 = null;

        public decimal? STIFFNES_WN1
        {
            get { return saveLabSTIFFNES_WN1; }
            set { saveLabSTIFFNES_WN1 = value; }
        }
        
        public decimal? STIFFNES_WN2
        {
            get { return saveLabSTIFFNES_WN2; }
            set { saveLabSTIFFNES_WN2 = value; }
        }

        public decimal? STIFFNES_WN3
        {
            get { return saveLabSTIFFNES_WN3; }
            set { saveLabSTIFFNES_WN3 = value; }
        }

        public decimal? STIFFNES_WN4
        {
            get { return saveLabSTIFFNES_WN4; }
            set { saveLabSTIFFNES_WN4 = value; }
        }

        public decimal? STIFFNES_WN5
        {
            get { return saveLabSTIFFNES_WN5; }
            set { saveLabSTIFFNES_WN5 = value; }
        }

        public decimal? STIFFNES_WN6
        {
            get { return saveLabSTIFFNES_WN6; }
            set { saveLabSTIFFNES_WN6 = value; }
        }

        #endregion

        #region STIFFNES_FN

        decimal? saveLabSTIFFNES_FN1 = null;
        decimal? saveLabSTIFFNES_FN2 = null;
        decimal? saveLabSTIFFNES_FN3 = null;
        decimal? saveLabSTIFFNES_FN4 = null;
        decimal? saveLabSTIFFNES_FN5 = null;
        decimal? saveLabSTIFFNES_FN6 = null;

        public decimal? STIFFNES_FN1
        {
            get { return saveLabSTIFFNES_FN1; }
            set { saveLabSTIFFNES_FN1 = value; }
        }

        public decimal? STIFFNES_FN2
        {
            get { return saveLabSTIFFNES_FN2; }
            set { saveLabSTIFFNES_FN2 = value; }
        }

        public decimal? STIFFNES_FN3
        {
            get { return saveLabSTIFFNES_FN3; }
            set { saveLabSTIFFNES_FN3 = value; }
        }

        public decimal? STIFFNES_FN4
        {
            get { return saveLabSTIFFNES_FN4; }
            set { saveLabSTIFFNES_FN4 = value; }
        }

        public decimal? STIFFNES_FN5
        {
            get { return saveLabSTIFFNES_FN5; }
            set { saveLabSTIFFNES_FN5 = value; }
        }

        public decimal? STIFFNES_FN6
        {
            get { return saveLabSTIFFNES_FN6; }
            set { saveLabSTIFFNES_FN6 = value; }
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

