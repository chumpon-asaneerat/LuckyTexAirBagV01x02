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
    /// Interaction logic for RetestDynamicAirWindow.xaml
    /// </summary>
    public partial class RetestDynamicAirWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetestDynamicAirWindow()
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

        LAB_GETITEM_LCL_UCL _item_LCL_UCL = new LAB_GETITEM_LCL_UCL();

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtITMCODE.Text = ITM_CODE;
            txtWEAVINGLOG.Text = WEAVINGLOT;
            txtFINISHINGLOT.Text = FINISHINGLOT;

            LoadItemTestProperty(txtITMCODE.Text);
            LoadItemTestSpecification(txtITMCODE.Text);
            LAB_GETDYNAMICAIRDATA(txtITMCODE.Text, txtWEAVINGLOG.Text);

            txtSaveLabDYNAMIC_AIRN1.Focus();
            txtSaveLabDYNAMIC_AIRN1.SelectAll();
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
                if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN1.Text) && txtSaveLabDYNAMIC_AIRN1.IsVisible == true)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN2.Text) && txtSaveLabDYNAMIC_AIRN2.IsVisible == true)
                {
                    txtSaveLabDYNAMIC_AIRN2.Focus();
                    txtSaveLabDYNAMIC_AIRN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN3.Text) && txtSaveLabDYNAMIC_AIRN3.IsVisible == true)
                {
                    txtSaveLabDYNAMIC_AIRN3.Focus();
                    txtSaveLabDYNAMIC_AIRN3.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabEXPONENTN1.Text) && txtSaveLabEXPONENTN1.IsVisible == true)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabEXPONENTN2.Text) && txtSaveLabEXPONENTN2.IsVisible == true)
                {
                    txtSaveLabEXPONENTN2.Focus();
                    txtSaveLabEXPONENTN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabEXPONENTN3.Text) && txtSaveLabEXPONENTN3.IsVisible == true)
                {
                    txtSaveLabEXPONENTN3.Focus();
                    txtSaveLabEXPONENTN3.SelectAll();
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
                if (txtDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtDYNAMIC_AIRN1.Focus();
                    txtDYNAMIC_AIRN1.SelectAll();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region Item Property

        #region KeyDown

        #region DYNAMIC_AIR
        private void txtDYNAMIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDYNAMIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtDYNAMIC_AIRN2.Focus();
                    txtDYNAMIC_AIRN2.SelectAll();
                }
                else if (txtEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN1.Focus();
                    txtEXPONENTN1.SelectAll();
                }
               
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDYNAMIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtDYNAMIC_AIRN3.Focus();
                    txtDYNAMIC_AIRN3.SelectAll();
                }
                else if (txtEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN1.Focus();
                    txtEXPONENTN1.SelectAll();
                }
                
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDYNAMIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtDYNAMIC_AIRN4.Focus();
                    txtDYNAMIC_AIRN4.SelectAll();
                }
                else if (txtEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN1.Focus();
                    txtEXPONENTN1.SelectAll();
                }
                
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDYNAMIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtDYNAMIC_AIRN5.Focus();
                    txtDYNAMIC_AIRN5.SelectAll();
                }
                else if (txtEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN1.Focus();
                    txtEXPONENTN1.SelectAll();
                }
                
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDYNAMIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtDYNAMIC_AIRN6.Focus();
                    txtDYNAMIC_AIRN6.SelectAll();
                }
                else if (txtEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN1.Focus();
                    txtEXPONENTN1.SelectAll();
                }
                
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN1.Focus();
                    txtEXPONENTN1.SelectAll();
                }
               
                e.Handled = true;
            }
        }
        #endregion

        #region EXPONENT
        private void txtEXPONENTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEXPONENTN2.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN2.Focus();
                    txtEXPONENTN2.SelectAll();
                }
                else if (txtRetest1DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN1.Focus();
                    txtRetest1DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEXPONENTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEXPONENTN3.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN3.Focus();
                    txtEXPONENTN3.SelectAll();
                }
                else if (txtRetest1DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN1.Focus();
                    txtRetest1DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEXPONENTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEXPONENTN4.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN4.Focus();
                    txtEXPONENTN4.SelectAll();
                }
                else if (txtRetest1DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN1.Focus();
                    txtRetest1DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEXPONENTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEXPONENTN5.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN5.Focus();
                    txtEXPONENTN5.SelectAll();
                }
                else if (txtRetest1DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN1.Focus();
                    txtRetest1DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEXPONENTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEXPONENTN6.Visibility == Visibility.Visible)
                {
                    txtEXPONENTN6.Focus();
                    txtEXPONENTN6.SelectAll();
                }
                else if (txtRetest1DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN1.Focus();
                    txtRetest1DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEXPONENTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN1.Focus();
                    txtRetest1DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 DYNAMIC_AIR
        private void txtRetest1DYNAMIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1DYNAMIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN2.Focus();
                    txtRetest1DYNAMIC_AIRN2.SelectAll();
                }
                else if (txtRetest1EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN1.Focus();
                    txtRetest1EXPONENTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1DYNAMIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1DYNAMIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN3.Focus();
                    txtRetest1DYNAMIC_AIRN3.SelectAll();
                }
                else if (txtRetest1EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN1.Focus();
                    txtRetest1EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1DYNAMIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1DYNAMIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN4.Focus();
                    txtRetest1DYNAMIC_AIRN4.SelectAll();
                }
                else if (txtRetest1EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN1.Focus();
                    txtRetest1EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1DYNAMIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1DYNAMIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN5.Focus();
                    txtRetest1DYNAMIC_AIRN5.SelectAll();
                }
                else if (txtRetest1EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN1.Focus();
                    txtRetest1EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1DYNAMIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1DYNAMIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtRetest1DYNAMIC_AIRN6.Focus();
                    txtRetest1DYNAMIC_AIRN6.SelectAll();
                }
                else if (txtRetest1EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN1.Focus();
                    txtRetest1EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1DYNAMIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN1.Focus();
                    txtRetest1EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 EXPONENTN
        private void txtRetest1EXPONENTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1EXPONENTN2.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN2.Focus();
                    txtRetest1EXPONENTN2.SelectAll();
                }
                else if (txtRetest2DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN1.Focus();
                    txtRetest2DYNAMIC_AIRN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1EXPONENTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1EXPONENTN3.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN3.Focus();
                    txtRetest1EXPONENTN3.SelectAll();
                }
                else if (txtRetest2DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN1.Focus();
                    txtRetest2DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1EXPONENTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1EXPONENTN4.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN4.Focus();
                    txtRetest1EXPONENTN4.SelectAll();
                }
                else if (txtRetest2DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN1.Focus();
                    txtRetest2DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1EXPONENTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1EXPONENTN5.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN5.Focus();
                    txtRetest1EXPONENTN5.SelectAll();
                }
                else if (txtRetest2DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN1.Focus();
                    txtRetest2DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1EXPONENTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1EXPONENTN6.Visibility == Visibility.Visible)
                {
                    txtRetest1EXPONENTN6.Focus();
                    txtRetest1EXPONENTN6.SelectAll();
                }
                else if (txtRetest2DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN1.Focus();
                    txtRetest2DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1EXPONENTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2DYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN1.Focus();
                    txtRetest2DYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 DYNAMIC_AIR
        private void txtRetest2DYNAMIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2DYNAMIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN2.Focus();
                    txtRetest2DYNAMIC_AIRN2.SelectAll();
                }
                else if (txtRetest2EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN1.Focus();
                    txtRetest2EXPONENTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2DYNAMIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2DYNAMIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN3.Focus();
                    txtRetest2DYNAMIC_AIRN3.SelectAll();
                }
                else if (txtRetest2EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN1.Focus();
                    txtRetest2EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2DYNAMIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2DYNAMIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN4.Focus();
                    txtRetest2DYNAMIC_AIRN4.SelectAll();
                }
                else if (txtRetest2EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN1.Focus();
                    txtRetest2EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2DYNAMIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2DYNAMIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN5.Focus();
                    txtRetest2DYNAMIC_AIRN5.SelectAll();
                }
                else if (txtRetest2EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN1.Focus();
                    txtRetest2EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2DYNAMIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2DYNAMIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtRetest2DYNAMIC_AIRN6.Focus();
                    txtRetest2DYNAMIC_AIRN6.SelectAll();
                }
                else if (txtRetest2EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN1.Focus();
                    txtRetest2EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2DYNAMIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2EXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN1.Focus();
                    txtRetest2EXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 EXPONENTN
        private void txtRetest2EXPONENTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2EXPONENTN2.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN2.Focus();
                    txtRetest2EXPONENTN2.SelectAll();
                }
                else if (txtSaveLabDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2EXPONENTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2EXPONENTN3.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN3.Focus();
                    txtRetest2EXPONENTN3.SelectAll();
                }
                else if (txtSaveLabDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2EXPONENTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2EXPONENTN4.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN4.Focus();
                    txtRetest2EXPONENTN4.SelectAll();
                }
                else if (txtSaveLabDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2EXPONENTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2EXPONENTN5.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN5.Focus();
                    txtRetest2EXPONENTN5.SelectAll();
                }
                else if (txtSaveLabDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2EXPONENTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2EXPONENTN6.Visibility == Visibility.Visible)
                {
                    txtRetest2EXPONENTN6.Focus();
                    txtRetest2EXPONENTN6.SelectAll();
                }
                else if (txtSaveLabDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2EXPONENTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabDYNAMIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN1.Focus();
                    txtSaveLabDYNAMIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab DYNAMIC_AIR
        private void txtSaveLabDYNAMIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabDYNAMIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN2.Focus();
                    txtSaveLabDYNAMIC_AIRN2.SelectAll();
                }
                else if (txtSaveLabEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabDYNAMIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabDYNAMIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN3.Focus();
                    txtSaveLabDYNAMIC_AIRN3.SelectAll();
                }
                else if (txtSaveLabEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabDYNAMIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabDYNAMIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN4.Focus();
                    txtSaveLabDYNAMIC_AIRN4.SelectAll();
                }
                else if (txtSaveLabEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabDYNAMIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabDYNAMIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN5.Focus();
                    txtSaveLabDYNAMIC_AIRN5.SelectAll();
                }
                else if (txtSaveLabEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabDYNAMIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabDYNAMIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabDYNAMIC_AIRN6.Focus();
                    txtSaveLabDYNAMIC_AIRN6.SelectAll();
                }
                else if (txtSaveLabEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabDYNAMIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabEXPONENTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN1.Focus();
                    txtSaveLabEXPONENTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab EXPONENTN
        private void txtSaveLabEXPONENTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabEXPONENTN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN2.Focus();
                    txtSaveLabEXPONENTN2.SelectAll();
                }
                else 
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabEXPONENTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabEXPONENTN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN3.Focus();
                    txtSaveLabEXPONENTN3.SelectAll();
                }
                else 
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabEXPONENTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabEXPONENTN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN4.Focus();
                    txtSaveLabEXPONENTN4.SelectAll();
                }
                else 
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabEXPONENTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabEXPONENTN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN5.Focus();
                    txtSaveLabEXPONENTN5.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabEXPONENTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabEXPONENTN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabEXPONENTN6.Focus();
                    txtSaveLabEXPONENTN6.SelectAll();
                }
                else
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabEXPONENTN6_KeyDown(object sender, KeyEventArgs e)
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

        #region DYNAMIC_AIR_LostFocus
        private void DYNAMIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            DYNAMIC_AIR();
        }
        #endregion

        #region Retest1DYNAMIC_AIR_LostFocus
        private void Retest1DYNAMIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1DYNAMIC_AIR();
        }
        #endregion

        #region Retest2DYNAMIC_AIR_LostFocus
        private void Retest2DYNAMIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2DYNAMIC_AIR();
        }
        #endregion

        #region SaveLabDYNAMIC_AIR_LostFocus
        private void SaveLabDYNAMIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabDYNAMIC_AIR();
        }
        #endregion

        #region EXPONENT_LostFocus
        private void EXPONENT_LostFocus(object sender, RoutedEventArgs e)
        {
            EXPONENT();
        }
        #endregion

        #region Retest1EXPONENT_LostFocus
        private void Retest1EXPONENT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1EXPONENT();
        }
        #endregion

        #region Retest2EXPONENT_LostFocus
        private void Retest2EXPONENT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2EXPONENT();
        }
        #endregion

        #region SaveLabEXPONENT_LostFocus
        private void SaveLabEXPONENT_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabEXPONENT();
        }
        #endregion

        #region DYNAMIC_AIR
        private void DYNAMIC_AIR() 
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

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtDYNAMIC_AIRN1.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtDYNAMIC_AIRN2.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtDYNAMIC_AIRN3.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtDYNAMIC_AIRN4.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtDYNAMIC_AIRN5.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtDYNAMIC_AIRN6.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRN6.Text = string.Empty;
                    }
                }

                decimal? Avg = 0;

                #region Old

                //if (ave1 != null && ave1 != 0)
                //    i++;
                //else
                //    ave1 = 0;

                //if (ave2 != null && ave2 != 0)
                //    i++;
                //else
                //    ave2 = 0;

                //if (ave3 != null && ave3 != 0)
                //    i++;
                //else
                //    ave3 = 0;

                //if (ave4 != null && ave4 != 0)
                //    i++;
                //else
                //    ave4 = 0;

                //if (ave5 != null && ave5 != 0)
                //    i++;
                //else
                //    ave5 = 0;

                //if (ave6 != null && ave6 != 0)
                //    i++;
                //else
                //    ave6 = 0;

                #endregion

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

                    txtDYNAMIC_AIRAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtDYNAMIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave1)
                        {
                            txtDYNAMIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtDYNAMIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtDYNAMIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave2)
                            txtDYNAMIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtDYNAMIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtDYNAMIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave3)
                            txtDYNAMIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtDYNAMIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtDYNAMIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave4)
                            txtDYNAMIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtDYNAMIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtDYNAMIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave5)
                            txtDYNAMIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtDYNAMIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtDYNAMIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave6)
                            txtDYNAMIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtDYNAMIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtDYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRSpecification.Text))
                {
                    string temp = txtDYNAMIC_AIRSpecification.Text;

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
                                        txtDYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtDYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtDYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtDYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtDYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtDYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtDYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtDYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtDYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtDYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtDYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtDYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtDYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    { 
                                    }
                                    else
                                        txtDYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    { 
                                    }
                                    else
                                        txtDYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    { 
                                    }
                                    else
                                        txtDYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    { 
                                    }
                                    else
                                        txtDYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    { 
                                    }
                                    else
                                        txtDYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtDYNAMIC_AIRN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            { 
                            }
                            else
                                txtDYNAMIC_AIRN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            { 
                            }
                            else
                                txtDYNAMIC_AIRN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            { 
                            }
                            else
                                txtDYNAMIC_AIRN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            { 
                            }
                            else
                                txtDYNAMIC_AIRN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            { 
                            }
                            else
                                txtDYNAMIC_AIRN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtDYNAMIC_AIRN1.Background = Brushes.White;
                    txtDYNAMIC_AIRN2.Background = Brushes.White;
                    txtDYNAMIC_AIRN3.Background = Brushes.White;
                    txtDYNAMIC_AIRN4.Background = Brushes.White;
                    txtDYNAMIC_AIRN5.Background = Brushes.White;
                    txtDYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1DYNAMIC_AIR
        private void Retest1DYNAMIC_AIR()
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

                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1DYNAMIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1DYNAMIC_AIRN1.Text);
                    }
                    else
                    {
                        txtRetest1DYNAMIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1DYNAMIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1DYNAMIC_AIRN2.Text);
                    }
                    else
                    {
                        txtRetest1DYNAMIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1DYNAMIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1DYNAMIC_AIRN3.Text);
                    }
                    else
                    {
                        txtRetest1DYNAMIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1DYNAMIC_AIRN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest1DYNAMIC_AIRN4.Text);
                    }
                    else
                    {
                        txtRetest1DYNAMIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1DYNAMIC_AIRN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest1DYNAMIC_AIRN5.Text);
                    }
                    else
                    {
                        txtRetest1DYNAMIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1DYNAMIC_AIRN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest1DYNAMIC_AIRN6.Text);
                    }
                    else
                    {
                        txtRetest1DYNAMIC_AIRN6.Text = string.Empty;
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

                    txtRetest1DYNAMIC_AIRAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1DYNAMIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave1)
                        {
                            txtRetest1DYNAMIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtRetest1DYNAMIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtRetest1DYNAMIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave2)
                            txtRetest1DYNAMIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1DYNAMIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtRetest1DYNAMIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave3)
                            txtRetest1DYNAMIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1DYNAMIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtRetest1DYNAMIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave4)
                            txtRetest1DYNAMIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1DYNAMIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtRetest1DYNAMIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave5)
                            txtRetest1DYNAMIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1DYNAMIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtRetest1DYNAMIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave6)
                            txtRetest1DYNAMIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1DYNAMIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtRetest1DYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1DYNAMIC_AIRSpecification.Text))
                {
                    string temp = txtRetest1DYNAMIC_AIRSpecification.Text;

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
                                        txtRetest1DYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1DYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1DYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1DYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1DYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1DYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1DYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1DYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1DYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1DYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1DYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1DYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtRetest1DYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1DYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }
         

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1DYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1DYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1DYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1DYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtRetest1DYNAMIC_AIRN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtRetest1DYNAMIC_AIRN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtRetest1DYNAMIC_AIRN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtRetest1DYNAMIC_AIRN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtRetest1DYNAMIC_AIRN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtRetest1DYNAMIC_AIRN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtRetest1DYNAMIC_AIRN1.Background = Brushes.White;
                    txtRetest1DYNAMIC_AIRN2.Background = Brushes.White;
                    txtRetest1DYNAMIC_AIRN3.Background = Brushes.White;
                    txtRetest1DYNAMIC_AIRN4.Background = Brushes.White;
                    txtRetest1DYNAMIC_AIRN5.Background = Brushes.White;
                    txtRetest1DYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2DYNAMIC_AIR
        private void Retest2DYNAMIC_AIR()
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

                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2DYNAMIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2DYNAMIC_AIRN1.Text);
                    }
                    else
                    {
                        txtRetest2DYNAMIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2DYNAMIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2DYNAMIC_AIRN2.Text);
                    }
                    else
                    {
                        txtRetest2DYNAMIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2DYNAMIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2DYNAMIC_AIRN3.Text);
                    }
                    else
                    {
                        txtRetest2DYNAMIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2DYNAMIC_AIRN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest2DYNAMIC_AIRN4.Text);
                    }
                    else
                    {
                        txtRetest2DYNAMIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2DYNAMIC_AIRN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest2DYNAMIC_AIRN5.Text);
                    }
                    else
                    {
                        txtRetest2DYNAMIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2DYNAMIC_AIRN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest2DYNAMIC_AIRN6.Text);
                    }
                    else
                    {
                        txtRetest2DYNAMIC_AIRN6.Text = string.Empty;
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

                    txtRetest2DYNAMIC_AIRAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2DYNAMIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave1)
                        {
                            txtRetest2DYNAMIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtRetest2DYNAMIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtRetest2DYNAMIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave2)
                            txtRetest2DYNAMIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2DYNAMIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtRetest2DYNAMIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave3)
                            txtRetest2DYNAMIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2DYNAMIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtRetest2DYNAMIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave4)
                            txtRetest2DYNAMIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2DYNAMIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtRetest2DYNAMIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave5)
                            txtRetest2DYNAMIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2DYNAMIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtRetest2DYNAMIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave6)
                            txtRetest2DYNAMIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2DYNAMIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtRetest2DYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2DYNAMIC_AIRSpecification.Text))
                {
                    string temp = txtRetest2DYNAMIC_AIRSpecification.Text;

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
                                        txtRetest2DYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2DYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2DYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2DYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2DYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2DYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2DYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2DYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2DYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2DYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2DYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2DYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtRetest2DYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2DYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2DYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2DYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2DYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2DYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtRetest2DYNAMIC_AIRN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtRetest2DYNAMIC_AIRN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtRetest2DYNAMIC_AIRN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtRetest2DYNAMIC_AIRN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtRetest2DYNAMIC_AIRN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtRetest2DYNAMIC_AIRN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtRetest2DYNAMIC_AIRN1.Background = Brushes.White;
                    txtRetest2DYNAMIC_AIRN2.Background = Brushes.White;
                    txtRetest2DYNAMIC_AIRN3.Background = Brushes.White;
                    txtRetest2DYNAMIC_AIRN4.Background = Brushes.White;
                    txtRetest2DYNAMIC_AIRN5.Background = Brushes.White;
                    txtRetest2DYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLabDYNAMIC_AIR
        private void SaveLabDYNAMIC_AIR()
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

                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabDYNAMIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabDYNAMIC_AIRN1.Text);
                    }
                    else
                    {
                        txtSaveLabDYNAMIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabDYNAMIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabDYNAMIC_AIRN2.Text);
                    }
                    else
                    {
                        txtSaveLabDYNAMIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabDYNAMIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabDYNAMIC_AIRN3.Text);
                    }
                    else
                    {
                        txtSaveLabDYNAMIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabDYNAMIC_AIRN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtSaveLabDYNAMIC_AIRN4.Text);
                    }
                    else
                    {
                        txtSaveLabDYNAMIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabDYNAMIC_AIRN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtSaveLabDYNAMIC_AIRN5.Text);
                    }
                    else
                    {
                        txtSaveLabDYNAMIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabDYNAMIC_AIRN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtSaveLabDYNAMIC_AIRN6.Text);
                    }
                    else
                    {
                        txtSaveLabDYNAMIC_AIRN6.Text = string.Empty;
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

                    txtSaveLabDYNAMIC_AIRAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabDYNAMIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave1)
                        {
                            txtSaveLabDYNAMIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtSaveLabDYNAMIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtSaveLabDYNAMIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave2)
                            txtSaveLabDYNAMIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabDYNAMIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtSaveLabDYNAMIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave3)
                            txtSaveLabDYNAMIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabDYNAMIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtSaveLabDYNAMIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave4)
                            txtSaveLabDYNAMIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabDYNAMIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtSaveLabDYNAMIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave5)
                            txtSaveLabDYNAMIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabDYNAMIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtSaveLabDYNAMIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.DYNAMIC_AIR_LCL || _item_LCL_UCL.DYNAMIC_AIR_UCL < ave6)
                            txtSaveLabDYNAMIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabDYNAMIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtSaveLabDYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRSpecification.Text))
                {
                    string temp = txtSaveLabDYNAMIC_AIRSpecification.Text;

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
                                        txtSaveLabDYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabDYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabDYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabDYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabDYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabDYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabDYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabDYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabDYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabDYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabDYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabDYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtSaveLabDYNAMIC_AIRN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabDYNAMIC_AIRN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabDYNAMIC_AIRN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabDYNAMIC_AIRN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabDYNAMIC_AIRN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabDYNAMIC_AIRN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtSaveLabDYNAMIC_AIRN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtSaveLabDYNAMIC_AIRN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtSaveLabDYNAMIC_AIRN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtSaveLabDYNAMIC_AIRN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtSaveLabDYNAMIC_AIRN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtSaveLabDYNAMIC_AIRN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtSaveLabDYNAMIC_AIRN1.Background = Brushes.White;
                    txtSaveLabDYNAMIC_AIRN2.Background = Brushes.White;
                    txtSaveLabDYNAMIC_AIRN3.Background = Brushes.White;
                    txtSaveLabDYNAMIC_AIRN4.Background = Brushes.White;
                    txtSaveLabDYNAMIC_AIRN5.Background = Brushes.White;
                    txtSaveLabDYNAMIC_AIRN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region EXPONENT
        private void EXPONENT() 
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

                if (!string.IsNullOrEmpty(txtEXPONENTN1.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtEXPONENTN1.Text);
                    }
                    else
                    {
                        txtEXPONENTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtEXPONENTN2.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtEXPONENTN2.Text);
                    }
                    else
                    {
                        txtEXPONENTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtEXPONENTN3.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtEXPONENTN3.Text);
                    }
                    else
                    {
                        txtEXPONENTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtEXPONENTN4.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtEXPONENTN4.Text);
                    }
                    else
                    {
                        txtEXPONENTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtEXPONENTN5.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtEXPONENTN5.Text);
                    }
                    else
                    {
                        txtEXPONENTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtEXPONENTN6.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtEXPONENTN6.Text);
                    }
                    else
                    {
                        txtEXPONENTN6.Text = string.Empty;
                    }
                }

                decimal? Avg = 0;

                #region Old

                //if (ave1 != null && ave1 != 0)
                //    i++;
                //else
                //    ave1 = 0;

                //if (ave2 != null && ave2 != 0)
                //    i++;
                //else
                //    ave2 = 0;

                //if (ave3 != null && ave3 != 0)
                //    i++;
                //else
                //    ave3 = 0;

                //if (ave4 != null && ave4 != 0)
                //    i++;
                //else
                //    ave4 = 0;

                //if (ave5 != null && ave5 != 0)
                //    i++;
                //else
                //    ave5 = 0;

                //if (ave6 != null && ave6 != 0)
                //    i++;
                //else
                //    ave6 = 0;
                #endregion

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

                    txtEXPONENTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtEXPONENTAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave1)
                        {
                            txtEXPONENTN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtEXPONENTN1.Background = Brushes.White;
                    }
                    else
                        txtEXPONENTN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave2)
                            txtEXPONENTN2.Background = Brushes.LemonChiffon;
                        else
                            txtEXPONENTN2.Background = Brushes.White;
                    }
                    else
                        txtEXPONENTN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave3)
                            txtEXPONENTN3.Background = Brushes.LemonChiffon;
                        else
                            txtEXPONENTN3.Background = Brushes.White;
                    }
                    else
                        txtEXPONENTN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave4)
                            txtEXPONENTN4.Background = Brushes.LemonChiffon;
                        else
                            txtEXPONENTN4.Background = Brushes.White;
                    }
                    else
                        txtEXPONENTN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave5)
                            txtEXPONENTN5.Background = Brushes.LemonChiffon;
                        else
                            txtEXPONENTN5.Background = Brushes.White;
                    }
                    else
                        txtEXPONENTN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave6)
                            txtEXPONENTN6.Background = Brushes.LemonChiffon;
                        else
                            txtEXPONENTN6.Background = Brushes.White;
                    }
                    else
                        txtEXPONENTN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtEXPONENTSpecification.Text))
                {
                    string temp = txtEXPONENTSpecification.Text;

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
                                        txtEXPONENTN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtEXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtEXPONENTN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtEXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtEXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtEXPONENTN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtEXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtEXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtEXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtEXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtEXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtEXPONENTN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtEXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtEXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtEXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtEXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtEXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtEXPONENTN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtEXPONENTN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtEXPONENTN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtEXPONENTN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtEXPONENTN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtEXPONENTN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtEXPONENTN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtEXPONENTN1.Background = Brushes.White;
                    txtEXPONENTN2.Background = Brushes.White;
                    txtEXPONENTN3.Background = Brushes.White;
                    txtEXPONENTN4.Background = Brushes.White;
                    txtEXPONENTN5.Background = Brushes.White;
                    txtEXPONENTN6.Background = Brushes.White;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1EXPONENT
        private void Retest1EXPONENT()
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

                if (!string.IsNullOrEmpty(txtRetest1EXPONENTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1EXPONENTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1EXPONENTN1.Text);
                    }
                    else
                    {
                        txtRetest1EXPONENTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1EXPONENTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1EXPONENTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1EXPONENTN2.Text);
                    }
                    else
                    {
                        txtRetest1EXPONENTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1EXPONENTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1EXPONENTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1EXPONENTN3.Text);
                    }
                    else
                    {
                        txtRetest1EXPONENTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1EXPONENTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1EXPONENTN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest1EXPONENTN4.Text);
                    }
                    else
                    {
                        txtRetest1EXPONENTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1EXPONENTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1EXPONENTN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest1EXPONENTN5.Text);
                    }
                    else
                    {
                        txtRetest1EXPONENTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1EXPONENTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1EXPONENTN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest1EXPONENTN6.Text);
                    }
                    else
                    {
                        txtRetest1EXPONENTN6.Text = string.Empty;
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

                    txtRetest1EXPONENTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1EXPONENTAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave1)
                        {
                            txtRetest1EXPONENTN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtRetest1EXPONENTN1.Background = Brushes.White;
                    }
                    else
                        txtRetest1EXPONENTN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave2)
                            txtRetest1EXPONENTN2.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1EXPONENTN2.Background = Brushes.White;
                    }
                    else
                        txtRetest1EXPONENTN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave3)
                            txtRetest1EXPONENTN3.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1EXPONENTN3.Background = Brushes.White;
                    }
                    else
                        txtRetest1EXPONENTN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave4)
                            txtRetest1EXPONENTN4.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1EXPONENTN4.Background = Brushes.White;
                    }
                    else
                        txtRetest1EXPONENTN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave5)
                            txtRetest1EXPONENTN5.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1EXPONENTN5.Background = Brushes.White;
                    }
                    else
                        txtRetest1EXPONENTN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave6)
                            txtRetest1EXPONENTN6.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1EXPONENTN6.Background = Brushes.White;
                    }
                    else
                        txtRetest1EXPONENTN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1EXPONENTSpecification.Text))
                {
                    string temp = txtRetest1EXPONENTSpecification.Text;

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
                                        txtRetest1EXPONENTN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1EXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1EXPONENTN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1EXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1EXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1EXPONENTN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1EXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1EXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1EXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1EXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1EXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1EXPONENTN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtRetest1EXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1EXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1EXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1EXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1EXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest1EXPONENTN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtRetest1EXPONENTN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtRetest1EXPONENTN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtRetest1EXPONENTN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtRetest1EXPONENTN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtRetest1EXPONENTN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtRetest1EXPONENTN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtRetest1EXPONENTN1.Background = Brushes.White;
                    txtRetest1EXPONENTN2.Background = Brushes.White;
                    txtRetest1EXPONENTN3.Background = Brushes.White;
                    txtRetest1EXPONENTN4.Background = Brushes.White;
                    txtRetest1EXPONENTN5.Background = Brushes.White;
                    txtRetest1EXPONENTN6.Background = Brushes.White;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2EXPONENT
        private void Retest2EXPONENT()
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

                if (!string.IsNullOrEmpty(txtRetest2EXPONENTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2EXPONENTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2EXPONENTN1.Text);
                    }
                    else
                    {
                        txtRetest2EXPONENTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2EXPONENTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2EXPONENTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2EXPONENTN2.Text);
                    }
                    else
                    {
                        txtRetest2EXPONENTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2EXPONENTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2EXPONENTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2EXPONENTN3.Text);
                    }
                    else
                    {
                        txtRetest2EXPONENTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2EXPONENTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2EXPONENTN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtRetest2EXPONENTN4.Text);
                    }
                    else
                    {
                        txtRetest2EXPONENTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2EXPONENTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2EXPONENTN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtRetest2EXPONENTN5.Text);
                    }
                    else
                    {
                        txtRetest2EXPONENTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2EXPONENTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2EXPONENTN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtRetest2EXPONENTN6.Text);
                    }
                    else
                    {
                        txtRetest2EXPONENTN6.Text = string.Empty;
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

                    txtRetest2EXPONENTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2EXPONENTAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave1)
                        {
                            txtRetest2EXPONENTN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtRetest2EXPONENTN1.Background = Brushes.White;
                    }
                    else
                        txtRetest2EXPONENTN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave2)
                            txtRetest2EXPONENTN2.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2EXPONENTN2.Background = Brushes.White;
                    }
                    else
                        txtRetest2EXPONENTN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave3)
                            txtRetest2EXPONENTN3.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2EXPONENTN3.Background = Brushes.White;
                    }
                    else
                        txtRetest2EXPONENTN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave4)
                            txtRetest2EXPONENTN4.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2EXPONENTN4.Background = Brushes.White;
                    }
                    else
                        txtRetest2EXPONENTN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave5)
                            txtRetest2EXPONENTN5.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2EXPONENTN5.Background = Brushes.White;
                    }
                    else
                        txtRetest2EXPONENTN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave6)
                            txtRetest2EXPONENTN6.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2EXPONENTN6.Background = Brushes.White;
                    }
                    else
                        txtRetest2EXPONENTN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2EXPONENTSpecification.Text))
                {
                    string temp = txtRetest2EXPONENTSpecification.Text;

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
                                        txtRetest2EXPONENTN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2EXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2EXPONENTN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2EXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2EXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2EXPONENTN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2EXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2EXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2EXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2EXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2EXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2EXPONENTN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtRetest2EXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2EXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2EXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2EXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2EXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtRetest2EXPONENTN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtRetest2EXPONENTN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtRetest2EXPONENTN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtRetest2EXPONENTN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtRetest2EXPONENTN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtRetest2EXPONENTN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtRetest2EXPONENTN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtRetest2EXPONENTN1.Background = Brushes.White;
                    txtRetest2EXPONENTN2.Background = Brushes.White;
                    txtRetest2EXPONENTN3.Background = Brushes.White;
                    txtRetest2EXPONENTN4.Background = Brushes.White;
                    txtRetest2EXPONENTN5.Background = Brushes.White;
                    txtRetest2EXPONENTN6.Background = Brushes.White;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLabEXPONENT
        private void SaveLabEXPONENT()
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

                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabEXPONENTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabEXPONENTN1.Text);
                    }
                    else
                    {
                        txtSaveLabEXPONENTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabEXPONENTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabEXPONENTN2.Text);
                    }
                    else
                    {
                        txtSaveLabEXPONENTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabEXPONENTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabEXPONENTN3.Text);
                    }
                    else
                    {
                        txtSaveLabEXPONENTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabEXPONENTN4.Text, out value))
                    {
                        //ave4 = decimal.Parse(txtSaveLabEXPONENTN4.Text);
                    }
                    else
                    {
                        txtSaveLabEXPONENTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabEXPONENTN5.Text, out value))
                    {
                        //ave5 = decimal.Parse(txtSaveLabEXPONENTN5.Text);
                    }
                    else
                    {
                        txtSaveLabEXPONENTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabEXPONENTN6.Text, out value))
                    {
                        //ave6 = decimal.Parse(txtSaveLabEXPONENTN6.Text);
                    }
                    else
                    {
                        txtSaveLabEXPONENTN6.Text = string.Empty;
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

                    txtSaveLabEXPONENTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabEXPONENTAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave1)
                        {
                            txtSaveLabEXPONENTN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtSaveLabEXPONENTN1.Background = Brushes.White;
                    }
                    else
                        txtSaveLabEXPONENTN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave2)
                            txtSaveLabEXPONENTN2.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabEXPONENTN2.Background = Brushes.White;
                    }
                    else
                        txtSaveLabEXPONENTN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave3)
                            txtSaveLabEXPONENTN3.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabEXPONENTN3.Background = Brushes.White;
                    }
                    else
                        txtSaveLabEXPONENTN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave4)
                            txtSaveLabEXPONENTN4.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabEXPONENTN4.Background = Brushes.White;
                    }
                    else
                        txtSaveLabEXPONENTN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave5)
                            txtSaveLabEXPONENTN5.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabEXPONENTN5.Background = Brushes.White;
                    }
                    else
                        txtSaveLabEXPONENTN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.EXPONENT_LCL || _item_LCL_UCL.EXPONENT_UCL < ave6)
                            txtSaveLabEXPONENTN6.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabEXPONENTN6.Background = Brushes.White;
                    }
                    else
                        txtSaveLabEXPONENTN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabEXPONENTSpecification.Text))
                {
                    string temp = txtSaveLabEXPONENTSpecification.Text;

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
                                        txtSaveLabEXPONENTN1.Background = Brushes.Salmon;
                                }


                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabEXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabEXPONENTN3.Background = Brushes.Salmon;
                                }

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabEXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabEXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabEXPONENTN6.Background = Brushes.Salmon;
                                }

                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabEXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabEXPONENTN2.Background = Brushes.Salmon;
                                }


                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabEXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabEXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabEXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabEXPONENTN6.Background = Brushes.Salmon;
                                }

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
                                    }
                                    else
                                        txtSaveLabEXPONENTN1.Background = Brushes.Salmon;
                                }

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabEXPONENTN2.Background = Brushes.Salmon;
                                }

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabEXPONENTN3.Background = Brushes.Salmon;
                                }


                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabEXPONENTN4.Background = Brushes.Salmon;
                                }


                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabEXPONENTN5.Background = Brushes.Salmon;
                                }


                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                    }
                                    else
                                        txtSaveLabEXPONENTN6.Background = Brushes.Salmon;
                                }

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
                            }
                            else
                                txtSaveLabEXPONENTN1.Background = Brushes.Salmon;
                        }


                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                            }
                            else
                                txtSaveLabEXPONENTN2.Background = Brushes.Salmon;
                        }


                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                            }
                            else
                                txtSaveLabEXPONENTN3.Background = Brushes.Salmon;
                        }

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                            }
                            else
                                txtSaveLabEXPONENTN4.Background = Brushes.Salmon;
                        }


                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                            }
                            else
                                txtSaveLabEXPONENTN5.Background = Brushes.Salmon;
                        }


                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                            }
                            else
                                txtSaveLabEXPONENTN6.Background = Brushes.Salmon;
                        }

                    }
                }
                else
                {
                    txtSaveLabEXPONENTN1.Background = Brushes.White;
                    txtSaveLabEXPONENTN2.Background = Brushes.White;
                    txtSaveLabEXPONENTN3.Background = Brushes.White;
                    txtSaveLabEXPONENTN4.Background = Brushes.White;
                    txtSaveLabEXPONENTN5.Background = Brushes.White;
                    txtSaveLabEXPONENTN6.Background = Brushes.White;
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

            N_DYNAMIC_AIR(null);
            Retest1DYNAMIC_AIR(null);
            Retest2DYNAMIC_AIR(null);
            SaveLabDYNAMIC_AIR(null);

            N_EXPONENT(null);
            Retest1EXPONENT(null);
            Retest2EXPONENT(null);
            SaveLabEXPONENT(null);

            _item_LCL_UCL = new LAB_GETITEM_LCL_UCL();

            txtSaveLabDYNAMIC_AIRN1.Focus();
            txtSaveLabDYNAMIC_AIRN1.SelectAll();
        }

        #endregion

        #region setDefForeground
        private void setDefForeground()
        {
            
            txtDYNAMIC_AIRN1.Background = Brushes.White;
            txtDYNAMIC_AIRN2.Background = Brushes.White;
            txtDYNAMIC_AIRN3.Background = Brushes.White;
            txtDYNAMIC_AIRN4.Background = Brushes.White;
            txtDYNAMIC_AIRN5.Background = Brushes.White;
            txtDYNAMIC_AIRN6.Background = Brushes.White;

            txtRetest1DYNAMIC_AIRN1.Background = Brushes.White;
            txtRetest1DYNAMIC_AIRN2.Background = Brushes.White;
            txtRetest1DYNAMIC_AIRN3.Background = Brushes.White;
            txtRetest1DYNAMIC_AIRN4.Background = Brushes.White;
            txtRetest1DYNAMIC_AIRN5.Background = Brushes.White;
            txtRetest1DYNAMIC_AIRN6.Background = Brushes.White;

            txtRetest2DYNAMIC_AIRN1.Background = Brushes.White;
            txtRetest2DYNAMIC_AIRN2.Background = Brushes.White;
            txtRetest2DYNAMIC_AIRN3.Background = Brushes.White;
            txtRetest2DYNAMIC_AIRN4.Background = Brushes.White;
            txtRetest2DYNAMIC_AIRN5.Background = Brushes.White;
            txtRetest2DYNAMIC_AIRN6.Background = Brushes.White;

            txtSaveLabDYNAMIC_AIRN1.Background = Brushes.White;
            txtSaveLabDYNAMIC_AIRN2.Background = Brushes.White;
            txtSaveLabDYNAMIC_AIRN3.Background = Brushes.White;
            txtSaveLabDYNAMIC_AIRN4.Background = Brushes.White;
            txtSaveLabDYNAMIC_AIRN5.Background = Brushes.White;
            txtSaveLabDYNAMIC_AIRN6.Background = Brushes.White;

            txtEXPONENTN1.Background = Brushes.White;
            txtEXPONENTN2.Background = Brushes.White;
            txtEXPONENTN3.Background = Brushes.White;
            txtEXPONENTN4.Background = Brushes.White;
            txtEXPONENTN5.Background = Brushes.White;
            txtEXPONENTN6.Background = Brushes.White;

            txtRetest1EXPONENTN1.Background = Brushes.White;
            txtRetest1EXPONENTN2.Background = Brushes.White;
            txtRetest1EXPONENTN3.Background = Brushes.White;
            txtRetest1EXPONENTN4.Background = Brushes.White;
            txtRetest1EXPONENTN5.Background = Brushes.White;
            txtRetest1EXPONENTN6.Background = Brushes.White;

            txtRetest2EXPONENTN1.Background = Brushes.White;
            txtRetest2EXPONENTN2.Background = Brushes.White;
            txtRetest2EXPONENTN3.Background = Brushes.White;
            txtRetest2EXPONENTN4.Background = Brushes.White;
            txtRetest2EXPONENTN5.Background = Brushes.White;
            txtRetest2EXPONENTN6.Background = Brushes.White;

            txtSaveLabEXPONENTN1.Background = Brushes.White;
            txtSaveLabEXPONENTN2.Background = Brushes.White;
            txtSaveLabEXPONENTN3.Background = Brushes.White;
            txtSaveLabEXPONENTN4.Background = Brushes.White;
            txtSaveLabEXPONENTN5.Background = Brushes.White;
            txtSaveLabEXPONENTN6.Background = Brushes.White;
        }
        #endregion

        #region LAB_GETDYNAMICAIRDATA
        private bool LAB_GETDYNAMICAIRDATA(string P_ITMCODE, string P_WEAVINGLOG)
        {
            bool chkLoad = true;

            try
            {
                spRetest1.Visibility = Visibility.Collapsed;
                spRetest2.Visibility = Visibility.Collapsed;

                List<LAB_GETDYNAMICAIRDATA> results = LabDataPDFDataService.Instance.LAB_GETDYNAMICAIRDATA(P_ITMCODE, P_WEAVINGLOG);
                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        decimal? DYNAMIC_AIR1 = null;
                        decimal? DYNAMIC_AIR2 = null;
                        decimal? DYNAMIC_AIR3 = null;

                        decimal? EXPONENT1 = null;
                        decimal? EXPONENT2 = null;
                        decimal? EXPONENT3 = null;

                        #region Get Data
                        
                        for (int page = 0; page <= results.Count - 1; page++)
                        {

                            if (page == 0)
                            {
                                if (results[page].DYNAMIC_AIR1 != null)
                                {
                                    DYNAMIC_AIR1 = results[page].DYNAMIC_AIR1;
                                    txtDYNAMIC_AIRN1.Text = results[page].DYNAMIC_AIR1.Value.ToString("#,##0.##");
                                }
                                if (results[page].DYNAMIC_AIR2 != null)
                                {
                                    DYNAMIC_AIR2 = results[page].DYNAMIC_AIR2;
                                    txtDYNAMIC_AIRN2.Text = results[page].DYNAMIC_AIR2.Value.ToString("#,##0.##");
                                }
                                if (results[page].DYNAMIC_AIR3 != null)
                                {
                                    DYNAMIC_AIR3 = results[page].DYNAMIC_AIR3;
                                    txtDYNAMIC_AIRN3.Text = results[page].DYNAMIC_AIR3.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT1 != null)
                                {
                                    EXPONENT1 = results[page].EXPONENT1;
                                    txtEXPONENTN1.Text = results[page].EXPONENT1.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT2 != null)
                                {
                                    EXPONENT2 = results[page].EXPONENT2;
                                    txtEXPONENTN2.Text = results[page].EXPONENT2.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT3 != null)
                                {
                                    EXPONENT3 = results[page].EXPONENT3;
                                    txtEXPONENTN3.Text = results[page].EXPONENT3.Value.ToString("#,##0.##");
                                }
                            }
                            else if (page == 1)
                            {
                                spRetest1.Visibility = Visibility.Visible;

                                if (results[page].DYNAMIC_AIR1 != null)
                                {
                                    DYNAMIC_AIR1 = results[page].DYNAMIC_AIR1;
                                    txtRetest1DYNAMIC_AIRN1.Text = results[page].DYNAMIC_AIR1.Value.ToString("#,##0.##");
                                }
                                if (results[page].DYNAMIC_AIR2 != null)
                                {
                                    DYNAMIC_AIR2 = results[page].DYNAMIC_AIR2;
                                    txtRetest1DYNAMIC_AIRN2.Text = results[page].DYNAMIC_AIR2.Value.ToString("#,##0.##");
                                }
                                if (results[page].DYNAMIC_AIR3 != null)
                                {
                                    DYNAMIC_AIR3 = results[page].DYNAMIC_AIR3;
                                    txtRetest1DYNAMIC_AIRN3.Text = results[page].DYNAMIC_AIR3.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT1 != null)
                                {
                                    EXPONENT1 = results[page].EXPONENT1;
                                    txtRetest1EXPONENTN1.Text = results[page].EXPONENT1.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT2 != null)
                                {
                                    EXPONENT2 = results[page].EXPONENT2;
                                    txtRetest1EXPONENTN2.Text = results[page].EXPONENT2.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT3 != null)
                                {
                                    EXPONENT3 = results[page].EXPONENT3;
                                    txtRetest1EXPONENTN3.Text = results[page].EXPONENT3.Value.ToString("#,##0.##");
                                }
                            }
                            else if (page == 2)
                            {
                                spRetest2.Visibility = Visibility.Visible;

                                if (results[page].DYNAMIC_AIR1 != null)
                                {
                                    DYNAMIC_AIR1 = results[page].DYNAMIC_AIR1;
                                    txtRetest2DYNAMIC_AIRN1.Text = results[page].DYNAMIC_AIR1.Value.ToString("#,##0.##");
                                }
                                if (results[page].DYNAMIC_AIR2 != null)
                                {
                                    DYNAMIC_AIR2 = results[page].DYNAMIC_AIR2;
                                    txtRetest2DYNAMIC_AIRN2.Text = results[page].DYNAMIC_AIR2.Value.ToString("#,##0.##");
                                }
                                if (results[page].DYNAMIC_AIR3 != null)
                                {
                                    DYNAMIC_AIR3 = results[page].DYNAMIC_AIR3;
                                    txtRetest2DYNAMIC_AIRN3.Text = results[page].DYNAMIC_AIR3.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT1 != null)
                                {
                                    EXPONENT1 = results[page].EXPONENT1;
                                    txtRetest2EXPONENTN1.Text = results[page].EXPONENT1.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT2 != null)
                                {
                                    EXPONENT2 = results[page].EXPONENT2;
                                    txtRetest2EXPONENTN2.Text = results[page].EXPONENT2.Value.ToString("#,##0.##");
                                }
                                if (results[page].EXPONENT3 != null)
                                {
                                    EXPONENT3 = results[page].EXPONENT3;
                                    txtRetest2EXPONENTN3.Text = results[page].EXPONENT3.Value.ToString("#,##0.##");
                                }
                            }
                        }

                        #endregion

                        if (DYNAMIC_AIR1 != null) { txtSaveLabDYNAMIC_AIRN1.Text = DYNAMIC_AIR1.Value.ToString("#,##0.##"); }
                        if (DYNAMIC_AIR2 != null) { txtSaveLabDYNAMIC_AIRN2.Text = DYNAMIC_AIR2.Value.ToString("#,##0.##"); }
                        if (DYNAMIC_AIR3 != null) { txtSaveLabDYNAMIC_AIRN3.Text = DYNAMIC_AIR3.Value.ToString("#,##0.##"); }
                        if (EXPONENT1 != null) { txtSaveLabEXPONENTN1.Text = EXPONENT1.Value.ToString("#,##0.##"); }
                        if (EXPONENT2 != null) { txtSaveLabEXPONENTN2.Text = EXPONENT2.Value.ToString("#,##0.##"); }
                        if (EXPONENT3 != null) { txtSaveLabEXPONENTN3.Text = EXPONENT3.Value.ToString("#,##0.##"); }

                        DYNAMIC_AIR();
                        Retest1DYNAMIC_AIR();
                        Retest2DYNAMIC_AIR();
                        SaveLabDYNAMIC_AIR();

                        EXPONENT();
                        Retest1EXPONENT();
                        Retest2EXPONENT();
                        SaveLabEXPONENT();
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
                        N_Head(results[0].DYNAMIC_AIR);

                        N_DYNAMIC_AIR(results[0].DYNAMIC_AIR);
                        Retest1DYNAMIC_AIR(results[0].DYNAMIC_AIR);
                        Retest2DYNAMIC_AIR(results[0].DYNAMIC_AIR);
                        SaveLabDYNAMIC_AIR(results[0].DYNAMIC_AIR);

                        N_EXPONENT(results[0].EXPONENT);
                        Retest1EXPONENT(results[0].EXPONENT);
                        Retest2EXPONENT(results[0].EXPONENT);
                        SaveLabEXPONENT(results[0].EXPONENT);
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
                
                txtDYNAMIC_AIRSpecification.Text = string.Empty;
                txtRetest1DYNAMIC_AIRSpecification.Text = string.Empty;
                txtRetest2DYNAMIC_AIRSpecification.Text = string.Empty;
                txtSaveLabDYNAMIC_AIRSpecification.Text = string.Empty;

                txtEXPONENTSpecification.Text = string.Empty;
                txtRetest1EXPONENTSpecification.Text = string.Empty;
                txtRetest2EXPONENTSpecification.Text = string.Empty;
                txtSaveLabEXPONENTSpecification.Text = string.Empty;

                _item_LCL_UCL = new LAB_GETITEM_LCL_UCL();

                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        txtDYNAMIC_AIRSpecification.Text = results[0].DYNAMIC_AIR_Spe;
                        txtRetest1DYNAMIC_AIRSpecification.Text = results[0].DYNAMIC_AIR_Spe;
                        txtRetest2DYNAMIC_AIRSpecification.Text = results[0].DYNAMIC_AIR_Spe;
                        txtSaveLabDYNAMIC_AIRSpecification.Text = results[0].DYNAMIC_AIR_Spe;

                        txtEXPONENTSpecification.Text = results[0].EXPONENT_Spe;
                        txtRetest1EXPONENTSpecification.Text = results[0].EXPONENT_Spe;
                        txtRetest2EXPONENTSpecification.Text = results[0].EXPONENT_Spe;
                        txtSaveLabEXPONENTSpecification.Text = results[0].EXPONENT_Spe;

                        _item_LCL_UCL.DYNAMIC_AIR_LCL = results[0].DYNAMIC_AIR_LCL;
                        _item_LCL_UCL.DYNAMIC_AIR_UCL = results[0].DYNAMIC_AIR_UCL;

                        _item_LCL_UCL.EXPONENT_LCL = results[0].EXPONENT_LCL;
                        _item_LCL_UCL.EXPONENT_UCL = results[0].EXPONENT_UCL;
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
                    if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN1.Text))
                        saveLabDYNAMIC_AIR1 = decimal.Parse(txtSaveLabDYNAMIC_AIRN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN2.Text))
                        saveLabDYNAMIC_AIR2 = decimal.Parse(txtSaveLabDYNAMIC_AIRN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN3.Text))
                        saveLabDYNAMIC_AIR3 = decimal.Parse(txtSaveLabDYNAMIC_AIRN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN1.Text))
                        saveLabEXPONENT1 = decimal.Parse(txtSaveLabEXPONENTN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN2.Text))
                        saveLabEXPONENT2 = decimal.Parse(txtSaveLabEXPONENTN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabEXPONENTN3.Text))
                        saveLabEXPONENT3 = decimal.Parse(txtSaveLabEXPONENTN3.Text);

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

                    if (txtSaveLabDYNAMIC_AIRN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabDYNAMIC_AIRN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabDYNAMIC_AIRN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabDYNAMIC_AIRN4.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN4.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabDYNAMIC_AIRN5.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN5.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabDYNAMIC_AIRN6.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabDYNAMIC_AIRN6.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabEXPONENTN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabEXPONENTN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabEXPONENTN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabEXPONENTN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabEXPONENTN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabEXPONENTN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabEXPONENTN4.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabEXPONENTN4.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabEXPONENTN5.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabEXPONENTN5.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabEXPONENTN6.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabEXPONENTN6.Text))
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

        #region N_DYNAMIC_AIR
        private void N_DYNAMIC_AIR(decimal? n)
        {
            decimal? DYNAMIC_AIR = n;

            txtDYNAMIC_AIRSpecification.Text = string.Empty;
            txtDYNAMIC_AIRN1.Text = string.Empty;
            txtDYNAMIC_AIRN2.Text = string.Empty;
            txtDYNAMIC_AIRN3.Text = string.Empty;
            txtDYNAMIC_AIRN4.Text = string.Empty;
            txtDYNAMIC_AIRN5.Text = string.Empty;
            txtDYNAMIC_AIRN6.Text = string.Empty;
            txtDYNAMIC_AIRAve.Text = string.Empty;

            if (DYNAMIC_AIR != null && DYNAMIC_AIR > 0)
            {

                if (DYNAMIC_AIR == 6)
                {
                    txtDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (DYNAMIC_AIR == 5)
                {
                    txtDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (DYNAMIC_AIR == 4)
                {
                    txtDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (DYNAMIC_AIR == 3)
                {
                    txtDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (DYNAMIC_AIR == 2)
                {
                    txtDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (DYNAMIC_AIR == 1)
                {
                    txtDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtDYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtDYNAMIC_AIRN1.Visibility = Visibility.Collapsed;
                txtDYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                txtDYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                txtDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                txtDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                txtDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1DYNAMIC_AIR
        private void Retest1DYNAMIC_AIR(decimal? n)
        {
            decimal? Retest1DYNAMIC_AIR = n;

            txtRetest1DYNAMIC_AIRSpecification.Text = string.Empty;
            txtRetest1DYNAMIC_AIRN1.Text = string.Empty;
            txtRetest1DYNAMIC_AIRN2.Text = string.Empty;
            txtRetest1DYNAMIC_AIRN3.Text = string.Empty;
            txtRetest1DYNAMIC_AIRN4.Text = string.Empty;
            txtRetest1DYNAMIC_AIRN5.Text = string.Empty;
            txtRetest1DYNAMIC_AIRN6.Text = string.Empty;
            txtRetest1DYNAMIC_AIRAve.Text = string.Empty;

            if (Retest1DYNAMIC_AIR != null && Retest1DYNAMIC_AIR > 0)
            {

                if (Retest1DYNAMIC_AIR == 6)
                {
                    txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (Retest1DYNAMIC_AIR == 5)
                {
                    txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest1DYNAMIC_AIR == 4)
                {
                    txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest1DYNAMIC_AIR == 3)
                {
                    txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest1DYNAMIC_AIR == 2)
                {
                    txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest1DYNAMIC_AIR == 1)
                {
                    txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1DYNAMIC_AIRN1.Visibility = Visibility.Collapsed;
                txtRetest1DYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                txtRetest1DYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                txtRetest1DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                txtRetest1DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                txtRetest1DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2DYNAMIC_AIR
        private void Retest2DYNAMIC_AIR(decimal? n)
        {
            decimal? Retest2DYNAMIC_AIR = n;

            txtRetest2DYNAMIC_AIRSpecification.Text = string.Empty;
            txtRetest2DYNAMIC_AIRN1.Text = string.Empty;
            txtRetest2DYNAMIC_AIRN2.Text = string.Empty;
            txtRetest2DYNAMIC_AIRN3.Text = string.Empty;
            txtRetest2DYNAMIC_AIRN4.Text = string.Empty;
            txtRetest2DYNAMIC_AIRN5.Text = string.Empty;
            txtRetest2DYNAMIC_AIRN6.Text = string.Empty;
            txtRetest2DYNAMIC_AIRAve.Text = string.Empty;

            if (Retest2DYNAMIC_AIR != null && Retest2DYNAMIC_AIR > 0)
            {

                if (Retest2DYNAMIC_AIR == 6)
                {
                    txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (Retest2DYNAMIC_AIR == 5)
                {
                    txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest2DYNAMIC_AIR == 4)
                {
                    txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest2DYNAMIC_AIR == 3)
                {
                    txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest2DYNAMIC_AIR == 2)
                {
                    txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (Retest2DYNAMIC_AIR == 1)
                {
                    txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2DYNAMIC_AIRN1.Visibility = Visibility.Collapsed;
                txtRetest2DYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                txtRetest2DYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                txtRetest2DYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                txtRetest2DYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                txtRetest2DYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLabDYNAMIC_AIR
        private void SaveLabDYNAMIC_AIR(decimal? n)
        {
            decimal? SaveLabDYNAMIC_AIR = n;

            txtSaveLabDYNAMIC_AIRSpecification.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRN1.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRN2.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRN3.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRN4.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRN5.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRN6.Text = string.Empty;
            txtSaveLabDYNAMIC_AIRAve.Text = string.Empty;

            if (SaveLabDYNAMIC_AIR != null && SaveLabDYNAMIC_AIR > 0)
            {

                if (SaveLabDYNAMIC_AIR == 6)
                {
                    txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (SaveLabDYNAMIC_AIR == 5)
                {
                    txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (SaveLabDYNAMIC_AIR == 4)
                {
                    txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (SaveLabDYNAMIC_AIR == 3)
                {
                    txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (SaveLabDYNAMIC_AIR == 2)
                {
                    txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (SaveLabDYNAMIC_AIR == 1)
                {
                    txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabDYNAMIC_AIRN1.Visibility = Visibility.Collapsed;
                txtSaveLabDYNAMIC_AIRN2.Visibility = Visibility.Collapsed;
                txtSaveLabDYNAMIC_AIRN3.Visibility = Visibility.Collapsed;
                txtSaveLabDYNAMIC_AIRN4.Visibility = Visibility.Collapsed;
                txtSaveLabDYNAMIC_AIRN5.Visibility = Visibility.Collapsed;
                txtSaveLabDYNAMIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region N_EXPONENT
        private void N_EXPONENT(decimal? n)
        {
            decimal? EXPONENT = n;

            txtEXPONENTSpecification.Text = string.Empty;
            txtEXPONENTN1.Text = string.Empty;
            txtEXPONENTN2.Text = string.Empty;
            txtEXPONENTN3.Text = string.Empty;
            txtEXPONENTN4.Text = string.Empty;
            txtEXPONENTN5.Text = string.Empty;
            txtEXPONENTN6.Text = string.Empty;
            txtEXPONENTAve.Text = string.Empty;

            if (EXPONENT != null && EXPONENT > 0)
            {

                if (EXPONENT == 6)
                {
                    txtEXPONENTN1.Visibility = Visibility.Visible;
                    txtEXPONENTN2.Visibility = Visibility.Visible;
                    txtEXPONENTN3.Visibility = Visibility.Visible;
                    txtEXPONENTN4.Visibility = Visibility.Visible;
                    txtEXPONENTN5.Visibility = Visibility.Visible;
                    txtEXPONENTN6.Visibility = Visibility.Visible;
                }
                else if (EXPONENT == 5)
                {
                    txtEXPONENTN1.Visibility = Visibility.Visible;
                    txtEXPONENTN2.Visibility = Visibility.Visible;
                    txtEXPONENTN3.Visibility = Visibility.Visible;
                    txtEXPONENTN4.Visibility = Visibility.Visible;
                    txtEXPONENTN5.Visibility = Visibility.Visible;
                    txtEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 4)
                {
                    txtEXPONENTN1.Visibility = Visibility.Visible;
                    txtEXPONENTN2.Visibility = Visibility.Visible;
                    txtEXPONENTN3.Visibility = Visibility.Visible;
                    txtEXPONENTN4.Visibility = Visibility.Visible;
                    txtEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 3)
                {
                    txtEXPONENTN1.Visibility = Visibility.Visible;
                    txtEXPONENTN2.Visibility = Visibility.Visible;
                    txtEXPONENTN3.Visibility = Visibility.Visible;
                    txtEXPONENTN4.Visibility = Visibility.Collapsed;
                    txtEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 2)
                {
                    txtEXPONENTN1.Visibility = Visibility.Visible;
                    txtEXPONENTN2.Visibility = Visibility.Visible;
                    txtEXPONENTN3.Visibility = Visibility.Collapsed;
                    txtEXPONENTN4.Visibility = Visibility.Collapsed;
                    txtEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 1)
                {
                    txtEXPONENTN1.Visibility = Visibility.Visible;
                    txtEXPONENTN2.Visibility = Visibility.Collapsed;
                    txtEXPONENTN3.Visibility = Visibility.Collapsed;
                    txtEXPONENTN4.Visibility = Visibility.Collapsed;
                    txtEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtEXPONENTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtEXPONENTN1.Visibility = Visibility.Collapsed;
                txtEXPONENTN2.Visibility = Visibility.Collapsed;
                txtEXPONENTN3.Visibility = Visibility.Collapsed;
                txtEXPONENTN4.Visibility = Visibility.Collapsed;
                txtEXPONENTN5.Visibility = Visibility.Collapsed;
                txtEXPONENTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1EXPONENT
        private void Retest1EXPONENT(decimal? n)
        {
            decimal? EXPONENT = n;

            txtRetest1EXPONENTSpecification.Text = string.Empty;
            txtRetest1EXPONENTN1.Text = string.Empty;
            txtRetest1EXPONENTN2.Text = string.Empty;
            txtRetest1EXPONENTN3.Text = string.Empty;
            txtRetest1EXPONENTN4.Text = string.Empty;
            txtRetest1EXPONENTN5.Text = string.Empty;
            txtRetest1EXPONENTN6.Text = string.Empty;
            txtRetest1EXPONENTAve.Text = string.Empty;

            if (EXPONENT != null && EXPONENT > 0)
            {

                if (EXPONENT == 6)
                {
                    txtRetest1EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN4.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN5.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN6.Visibility = Visibility.Visible;
                }
                else if (EXPONENT == 5)
                {
                    txtRetest1EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN4.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN5.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 4)
                {
                    txtRetest1EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN4.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 3)
                {
                    txtRetest1EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN4.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 2)
                {
                    txtRetest1EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN3.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN4.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 1)
                {
                    txtRetest1EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest1EXPONENTN2.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN3.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN4.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest1EXPONENTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1EXPONENTN1.Visibility = Visibility.Collapsed;
                txtRetest1EXPONENTN2.Visibility = Visibility.Collapsed;
                txtRetest1EXPONENTN3.Visibility = Visibility.Collapsed;
                txtRetest1EXPONENTN4.Visibility = Visibility.Collapsed;
                txtRetest1EXPONENTN5.Visibility = Visibility.Collapsed;
                txtRetest1EXPONENTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2EXPONENT
        private void Retest2EXPONENT(decimal? n)
        {
            decimal? EXPONENT = n;

            txtRetest2EXPONENTSpecification.Text = string.Empty;
            txtRetest2EXPONENTN1.Text = string.Empty;
            txtRetest2EXPONENTN2.Text = string.Empty;
            txtRetest2EXPONENTN3.Text = string.Empty;
            txtRetest2EXPONENTN4.Text = string.Empty;
            txtRetest2EXPONENTN5.Text = string.Empty;
            txtRetest2EXPONENTN6.Text = string.Empty;
            txtRetest2EXPONENTAve.Text = string.Empty;

            if (EXPONENT != null && EXPONENT > 0)
            {

                if (EXPONENT == 6)
                {
                    txtRetest2EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN4.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN5.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN6.Visibility = Visibility.Visible;
                }
                else if (EXPONENT == 5)
                {
                    txtRetest2EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN4.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN5.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 4)
                {
                    txtRetest2EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN4.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 3)
                {
                    txtRetest2EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN3.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN4.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 2)
                {
                    txtRetest2EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN2.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN3.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN4.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 1)
                {
                    txtRetest2EXPONENTN1.Visibility = Visibility.Visible;
                    txtRetest2EXPONENTN2.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN3.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN4.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN5.Visibility = Visibility.Collapsed;
                    txtRetest2EXPONENTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2EXPONENTN1.Visibility = Visibility.Collapsed;
                txtRetest2EXPONENTN2.Visibility = Visibility.Collapsed;
                txtRetest2EXPONENTN3.Visibility = Visibility.Collapsed;
                txtRetest2EXPONENTN4.Visibility = Visibility.Collapsed;
                txtRetest2EXPONENTN5.Visibility = Visibility.Collapsed;
                txtRetest2EXPONENTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLabEXPONENT
        private void SaveLabEXPONENT(decimal? n)
        {
            decimal? EXPONENT = n;

            txtSaveLabEXPONENTSpecification.Text = string.Empty;
            txtSaveLabEXPONENTN1.Text = string.Empty;
            txtSaveLabEXPONENTN2.Text = string.Empty;
            txtSaveLabEXPONENTN3.Text = string.Empty;
            txtSaveLabEXPONENTN4.Text = string.Empty;
            txtSaveLabEXPONENTN5.Text = string.Empty;
            txtSaveLabEXPONENTN6.Text = string.Empty;
            txtSaveLabEXPONENTAve.Text = string.Empty;

            if (EXPONENT != null && EXPONENT > 0)
            {

                if (EXPONENT == 6)
                {
                    txtSaveLabEXPONENTN1.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN2.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN3.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN4.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN5.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN6.Visibility = Visibility.Visible;
                }
                else if (EXPONENT == 5)
                {
                    txtSaveLabEXPONENTN1.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN2.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN3.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN4.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN5.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 4)
                {
                    txtSaveLabEXPONENTN1.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN2.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN3.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN4.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 3)
                {
                    txtSaveLabEXPONENTN1.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN2.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN3.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 2)
                {
                    txtSaveLabEXPONENTN1.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN2.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN6.Visibility = Visibility.Collapsed;
                }
                else if (EXPONENT == 1)
                {
                    txtSaveLabEXPONENTN1.Visibility = Visibility.Visible;
                    txtSaveLabEXPONENTN2.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabEXPONENTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabEXPONENTN1.Visibility = Visibility.Collapsed;
                txtSaveLabEXPONENTN2.Visibility = Visibility.Collapsed;
                txtSaveLabEXPONENTN3.Visibility = Visibility.Collapsed;
                txtSaveLabEXPONENTN4.Visibility = Visibility.Collapsed;
                txtSaveLabEXPONENTN5.Visibility = Visibility.Collapsed;
                txtSaveLabEXPONENTN6.Visibility = Visibility.Collapsed;
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
            #region DYNAMIC_AIR
            txtDYNAMIC_AIRN1.IsReadOnly = true;
            txtDYNAMIC_AIRN2.IsReadOnly = true;
            txtDYNAMIC_AIRN3.IsReadOnly = true;
            txtDYNAMIC_AIRN4.IsReadOnly = true;
            txtDYNAMIC_AIRN5.IsReadOnly = true;
            txtDYNAMIC_AIRN6.IsReadOnly = true;

            txtRetest1DYNAMIC_AIRN1.IsReadOnly = true;
            txtRetest1DYNAMIC_AIRN2.IsReadOnly = true;
            txtRetest1DYNAMIC_AIRN3.IsReadOnly = true;
            txtRetest1DYNAMIC_AIRN4.IsReadOnly = true;
            txtRetest1DYNAMIC_AIRN5.IsReadOnly = true;
            txtRetest1DYNAMIC_AIRN6.IsReadOnly = true;

            txtRetest2DYNAMIC_AIRN1.IsReadOnly = true;
            txtRetest2DYNAMIC_AIRN2.IsReadOnly = true;
            txtRetest2DYNAMIC_AIRN3.IsReadOnly = true;
            txtRetest2DYNAMIC_AIRN4.IsReadOnly = true;
            txtRetest2DYNAMIC_AIRN5.IsReadOnly = true;
            txtRetest2DYNAMIC_AIRN6.IsReadOnly = true;

            //txtSaveLabDYNAMIC_AIRN1.IsReadOnly = true;
            //txtSaveLabDYNAMIC_AIRN2.IsReadOnly = true;
            //txtSaveLabDYNAMIC_AIRN3.IsReadOnly = true;
            //txtSaveLabDYNAMIC_AIRN4.IsReadOnly = true;
            //txtSaveLabDYNAMIC_AIRN5.IsReadOnly = true;
            //txtSaveLabDYNAMIC_AIRN6.IsReadOnly = true;
            #endregion

            #region EXPONENT
            txtEXPONENTN1.IsReadOnly = true;
            txtEXPONENTN2.IsReadOnly = true;
            txtEXPONENTN3.IsReadOnly = true;
            txtEXPONENTN4.IsReadOnly = true;
            txtEXPONENTN5.IsReadOnly = true;
            txtEXPONENTN6.IsReadOnly = true;

            txtRetest1EXPONENTN1.IsReadOnly = true;
            txtRetest1EXPONENTN2.IsReadOnly = true;
            txtRetest1EXPONENTN3.IsReadOnly = true;
            txtRetest1EXPONENTN4.IsReadOnly = true;
            txtRetest1EXPONENTN5.IsReadOnly = true;
            txtRetest1EXPONENTN6.IsReadOnly = true;

            txtRetest2EXPONENTN1.IsReadOnly = true;
            txtRetest2EXPONENTN2.IsReadOnly = true;
            txtRetest2EXPONENTN3.IsReadOnly = true;
            txtRetest2EXPONENTN4.IsReadOnly = true;
            txtRetest2EXPONENTN5.IsReadOnly = true;
            txtRetest2EXPONENTN6.IsReadOnly = true;

            //txtSaveLabEXPONENTN1.IsReadOnly = true;
            //txtSaveLabEXPONENTN2.IsReadOnly = true;
            //txtSaveLabEXPONENTN3.IsReadOnly = true;
            //txtSaveLabEXPONENTN4.IsReadOnly = true;
            //txtSaveLabEXPONENTN5.IsReadOnly = true;
            //txtSaveLabEXPONENTN6.IsReadOnly = true;
            #endregion
        }
        #endregion

        #endregion

        #region Public Methods

        #region DYNAMIC_AIR

        decimal? saveLabDYNAMIC_AIR1 = null;
        decimal? saveLabDYNAMIC_AIR2 = null;
        decimal? saveLabDYNAMIC_AIR3 = null;

        public decimal? DYNAMIC_AIR1
        {
            get { return saveLabDYNAMIC_AIR1; }
            set { saveLabDYNAMIC_AIR1 = value; }
        }

        public decimal? DYNAMIC_AIR2
        {
            get { return saveLabDYNAMIC_AIR2; }
            set { saveLabDYNAMIC_AIR2 = value; }
        }

        public decimal? DYNAMIC_AIR3
        {
            get { return saveLabDYNAMIC_AIR3; }
            set { saveLabDYNAMIC_AIR3 = value; }
        }

        #endregion

        #region EXPONENT

        decimal? saveLabEXPONENT1 = null;
        decimal? saveLabEXPONENT2 = null;
        decimal? saveLabEXPONENT3 = null;

        public decimal? EXPONENT1
        {
            get { return saveLabEXPONENT1; }
            set { saveLabEXPONENT1 = value; }
        }

        public decimal? EXPONENT2
        {
            get { return saveLabEXPONENT2; }
            set { saveLabEXPONENT2 = value; }
        }

        public decimal? EXPONENT3
        {
            get { return saveLabEXPONENT3; }
            set { saveLabEXPONENT3 = value; }
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

