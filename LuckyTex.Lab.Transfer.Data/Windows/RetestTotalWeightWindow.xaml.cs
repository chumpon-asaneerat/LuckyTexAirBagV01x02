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
    /// Interaction logic for RetestTotalWeightWindow.xaml
    /// </summary>
    public partial class RetestTotalWeightWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetestTotalWeightWindow()
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
            LAB_GETWEIGHTDATA(txtITMCODE.Text, txtWEAVINGLOG.Text, "TW");

            txtSaveLabTOTALWEIGHTN1.Focus();
            txtSaveLabTOTALWEIGHTN1.SelectAll();
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
                if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN1.Text) && txtSaveLabTOTALWEIGHTN1.IsVisible == true)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN2.Text) && txtSaveLabTOTALWEIGHTN2.IsVisible == true)
                {
                    txtSaveLabTOTALWEIGHTN2.Focus();
                    txtSaveLabTOTALWEIGHTN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN3.Text) && txtSaveLabTOTALWEIGHTN3.IsVisible == true)
                {
                    txtSaveLabTOTALWEIGHTN3.Focus();
                    txtSaveLabTOTALWEIGHTN3.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN4.Text) && txtSaveLabTOTALWEIGHTN4.IsVisible == true)
                {
                    txtSaveLabTOTALWEIGHTN4.Focus();
                    txtSaveLabTOTALWEIGHTN4.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN5.Text) && txtSaveLabTOTALWEIGHTN5.IsVisible == true)
                {
                    txtSaveLabTOTALWEIGHTN5.Focus();
                    txtSaveLabTOTALWEIGHTN5.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN6.Text) && txtSaveLabTOTALWEIGHTN6.IsVisible == true)
                {
                    txtSaveLabTOTALWEIGHTN6.Focus();
                    txtSaveLabTOTALWEIGHTN6.SelectAll();
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
                if (txtTOTALWEIGHTN1.IsEnabled == true)
                {
                    txtTOTALWEIGHTN1.Focus();
                    txtTOTALWEIGHTN1.SelectAll();
                }
                
                e.Handled = true;
            }
        }
        #endregion

        #region Item Property

        #region KeyDown

        #region TOTALWEIGHT
        private void txtTOTALWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTOTALWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtTOTALWEIGHTN2.Focus();
                    txtTOTALWEIGHTN2.SelectAll();
                }
                else if (txtRetest1TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN1.Focus();
                    txtRetest1TOTALWEIGHTN1.SelectAll();
                }
            
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTOTALWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtTOTALWEIGHTN3.Focus();
                    txtTOTALWEIGHTN3.SelectAll();
                }
                else if (txtRetest1TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN1.Focus();
                    txtRetest1TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTOTALWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtTOTALWEIGHTN4.Focus();
                    txtTOTALWEIGHTN4.SelectAll();
                }
                else if (txtRetest1TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN1.Focus();
                    txtRetest1TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTOTALWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtTOTALWEIGHTN5.Focus();
                    txtTOTALWEIGHTN5.SelectAll();
                }
                else if (txtRetest1TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN1.Focus();
                    txtRetest1TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTOTALWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtTOTALWEIGHTN6.Focus();
                    txtTOTALWEIGHTN6.SelectAll();
                }
                else if (txtRetest1TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN1.Focus();
                    txtRetest1TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN1.Focus();
                    txtRetest1TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 TOTALWEIGHT
        private void txtRetest1TOTALWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1TOTALWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN2.Focus();
                    txtRetest1TOTALWEIGHTN2.SelectAll();
                }
                else if (txtRetest2TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN1.Focus();
                    txtRetest2TOTALWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1TOTALWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1TOTALWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN3.Focus();
                    txtRetest1TOTALWEIGHTN3.SelectAll();
                }
                else if (txtRetest2TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN1.Focus();
                    txtRetest2TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1TOTALWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1TOTALWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN4.Focus();
                    txtRetest1TOTALWEIGHTN4.SelectAll();
                }
                else if (txtRetest2TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN1.Focus();
                    txtRetest2TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1TOTALWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1TOTALWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN5.Focus();
                    txtRetest1TOTALWEIGHTN5.SelectAll();
                }
                else if (txtRetest2TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN1.Focus();
                    txtRetest2TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1TOTALWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1TOTALWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtRetest1TOTALWEIGHTN6.Focus();
                    txtRetest1TOTALWEIGHTN6.SelectAll();
                }
                else if (txtRetest2TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN1.Focus();
                    txtRetest2TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1TOTALWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2TOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN1.Focus();
                    txtRetest2TOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 TOTALWEIGHT
        private void txtRetest2TOTALWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2TOTALWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN2.Focus();
                    txtRetest2TOTALWEIGHTN2.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2TOTALWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2TOTALWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN3.Focus();
                    txtRetest2TOTALWEIGHTN3.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2TOTALWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2TOTALWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN4.Focus();
                    txtRetest2TOTALWEIGHTN4.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2TOTALWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2TOTALWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN5.Focus();
                    txtRetest2TOTALWEIGHTN5.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2TOTALWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2TOTALWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtRetest2TOTALWEIGHTN6.Focus();
                    txtRetest2TOTALWEIGHTN6.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2TOTALWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN1.Focus();
                    txtSaveLabTOTALWEIGHTN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab TOTALWEIGHT
        private void txtSaveLabTOTALWEIGHTN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN2.Focus();
                    txtSaveLabTOTALWEIGHTN2.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabTOTALWEIGHTN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN3.Focus();
                    txtSaveLabTOTALWEIGHTN3.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabTOTALWEIGHTN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN4.Focus();
                    txtSaveLabTOTALWEIGHTN4.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabTOTALWEIGHTN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN5.Focus();
                    txtSaveLabTOTALWEIGHTN5.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabTOTALWEIGHTN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabTOTALWEIGHTN6.Focus();
                    txtSaveLabTOTALWEIGHTN6.SelectAll();
                }
                else if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabTOTALWEIGHTN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabTOTALWEIGHTN1.Visibility == Visibility.Visible)
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

        #region TOTALWEIGHT_LostFocus
        private void TOTALWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            TOTALWEIGHT();
        }
        #endregion

        #region Retest1TOTALWEIGHT_LostFocus
        private void Retest1TOTALWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1TOTALWEIGHT();
        }
        #endregion

        #region Retest2TOTALWEIGHT_LostFocus
        private void Retest2TOTALWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2TOTALWEIGHT();
        }
        #endregion

        #region SaveLabTOTALWEIGHT_LostFocus
        private void SaveLabTOTALWEIGHT_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabTOTALWEIGHT();
        }
        #endregion

        #region TOTALWEIGHT
        private void TOTALWEIGHT()
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

                if (!string.IsNullOrEmpty(txtTOTALWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtTOTALWEIGHTN1.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtTOTALWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtTOTALWEIGHTN2.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtTOTALWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtTOTALWEIGHTN3.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtTOTALWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtTOTALWEIGHTN4.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtTOTALWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtTOTALWEIGHTN5.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtTOTALWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtTOTALWEIGHTN6.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTN6.Text = string.Empty;
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

                    txtTOTALWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtTOTALWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtTOTALWEIGHTSpecification.Text))
                {
                    string temp = txtTOTALWEIGHTSpecification.Text;

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
                                        txtTOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtTOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtTOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtTOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtTOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtTOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtTOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtTOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtTOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtTOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtTOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtTOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtTOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtTOTALWEIGHTN6.Background = Brushes.White;
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
                                        txtTOTALWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtTOTALWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtTOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtTOTALWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtTOTALWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtTOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtTOTALWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtTOTALWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtTOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtTOTALWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtTOTALWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtTOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtTOTALWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtTOTALWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtTOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtTOTALWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtTOTALWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtTOTALWEIGHTN6.Background = Brushes.White;
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
                                txtTOTALWEIGHTN1.Background = Brushes.White;
                            else
                                txtTOTALWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtTOTALWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtTOTALWEIGHTN2.Background = Brushes.White;
                            else
                                txtTOTALWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtTOTALWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtTOTALWEIGHTN3.Background = Brushes.White;
                            else
                                txtTOTALWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtTOTALWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtTOTALWEIGHTN4.Background = Brushes.White;
                            else
                                txtTOTALWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtTOTALWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtTOTALWEIGHTN5.Background = Brushes.White;
                            else
                                txtTOTALWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtTOTALWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtTOTALWEIGHTN6.Background = Brushes.White;
                            else
                                txtTOTALWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtTOTALWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtTOTALWEIGHTN1.Background = Brushes.White;
                    txtTOTALWEIGHTN2.Background = Brushes.White;
                    txtTOTALWEIGHTN3.Background = Brushes.White;
                    txtTOTALWEIGHTN4.Background = Brushes.White;
                    txtTOTALWEIGHTN5.Background = Brushes.White;
                    txtTOTALWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1TOTALWEIGHT
        private void Retest1TOTALWEIGHT()
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

                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1TOTALWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1TOTALWEIGHTN1.Text);
                    }
                    else
                    {
                        txtRetest1TOTALWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1TOTALWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1TOTALWEIGHTN2.Text);
                    }
                    else
                    {
                        txtRetest1TOTALWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1TOTALWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1TOTALWEIGHTN3.Text);
                    }
                    else
                    {
                        txtRetest1TOTALWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1TOTALWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest1TOTALWEIGHTN4.Text);
                    }
                    else
                    {
                        txtRetest1TOTALWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1TOTALWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest1TOTALWEIGHTN5.Text);
                    }
                    else
                    {
                        txtRetest1TOTALWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1TOTALWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest1TOTALWEIGHTN6.Text);
                    }
                    else
                    {
                        txtRetest1TOTALWEIGHTN6.Text = string.Empty;
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

                    txtRetest1TOTALWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest1TOTALWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1TOTALWEIGHTSpecification.Text))
                {
                    string temp = txtRetest1TOTALWEIGHTSpecification.Text;

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
                                        txtRetest1TOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1TOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1TOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1TOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1TOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1TOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1TOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1TOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1TOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1TOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1TOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1TOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
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
                                        txtRetest1TOTALWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtRetest1TOTALWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtRetest1TOTALWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtRetest1TOTALWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtRetest1TOTALWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtRetest1TOTALWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtRetest1TOTALWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtRetest1TOTALWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtRetest1TOTALWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtRetest1TOTALWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtRetest1TOTALWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
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
                                txtRetest1TOTALWEIGHTN1.Background = Brushes.White;
                            else
                                txtRetest1TOTALWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1TOTALWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtRetest1TOTALWEIGHTN2.Background = Brushes.White;
                            else
                                txtRetest1TOTALWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1TOTALWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtRetest1TOTALWEIGHTN3.Background = Brushes.White;
                            else
                                txtRetest1TOTALWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1TOTALWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtRetest1TOTALWEIGHTN4.Background = Brushes.White;
                            else
                                txtRetest1TOTALWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1TOTALWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtRetest1TOTALWEIGHTN5.Background = Brushes.White;
                            else
                                txtRetest1TOTALWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1TOTALWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
                            else
                                txtRetest1TOTALWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest1TOTALWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest1TOTALWEIGHTN1.Background = Brushes.White;
                    txtRetest1TOTALWEIGHTN2.Background = Brushes.White;
                    txtRetest1TOTALWEIGHTN3.Background = Brushes.White;
                    txtRetest1TOTALWEIGHTN4.Background = Brushes.White;
                    txtRetest1TOTALWEIGHTN5.Background = Brushes.White;
                    txtRetest1TOTALWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2TOTALWEIGHT
        private void Retest2TOTALWEIGHT()
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

                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2TOTALWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2TOTALWEIGHTN1.Text);
                    }
                    else
                    {
                        txtRetest2TOTALWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2TOTALWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2TOTALWEIGHTN2.Text);
                    }
                    else
                    {
                        txtRetest2TOTALWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2TOTALWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2TOTALWEIGHTN3.Text);
                    }
                    else
                    {
                        txtRetest2TOTALWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2TOTALWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest2TOTALWEIGHTN4.Text);
                    }
                    else
                    {
                        txtRetest2TOTALWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2TOTALWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest2TOTALWEIGHTN5.Text);
                    }
                    else
                    {
                        txtRetest2TOTALWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2TOTALWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest2TOTALWEIGHTN6.Text);
                    }
                    else
                    {
                        txtRetest2TOTALWEIGHTN6.Text = string.Empty;
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

                    txtRetest2TOTALWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtRetest2TOTALWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2TOTALWEIGHTSpecification.Text))
                {
                    string temp = txtRetest2TOTALWEIGHTSpecification.Text;

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
                                        txtRetest2TOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2TOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2TOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2TOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2TOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2TOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2TOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2TOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2TOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2TOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2TOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2TOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
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
                                        txtRetest2TOTALWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtRetest2TOTALWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtRetest2TOTALWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtRetest2TOTALWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtRetest2TOTALWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtRetest2TOTALWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtRetest2TOTALWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtRetest2TOTALWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtRetest2TOTALWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtRetest2TOTALWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtRetest2TOTALWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
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
                                txtRetest2TOTALWEIGHTN1.Background = Brushes.White;
                            else
                                txtRetest2TOTALWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2TOTALWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtRetest2TOTALWEIGHTN2.Background = Brushes.White;
                            else
                                txtRetest2TOTALWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2TOTALWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtRetest2TOTALWEIGHTN3.Background = Brushes.White;
                            else
                                txtRetest2TOTALWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2TOTALWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtRetest2TOTALWEIGHTN4.Background = Brushes.White;
                            else
                                txtRetest2TOTALWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2TOTALWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtRetest2TOTALWEIGHTN5.Background = Brushes.White;
                            else
                                txtRetest2TOTALWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2TOTALWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
                            else
                                txtRetest2TOTALWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtRetest2TOTALWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest2TOTALWEIGHTN1.Background = Brushes.White;
                    txtRetest2TOTALWEIGHTN2.Background = Brushes.White;
                    txtRetest2TOTALWEIGHTN3.Background = Brushes.White;
                    txtRetest2TOTALWEIGHTN4.Background = Brushes.White;
                    txtRetest2TOTALWEIGHTN5.Background = Brushes.White;
                    txtRetest2TOTALWEIGHTN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLabTOTALWEIGHT
        private void SaveLabTOTALWEIGHT()
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

                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabTOTALWEIGHTN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabTOTALWEIGHTN1.Text);
                    }
                    else
                    {
                        txtSaveLabTOTALWEIGHTN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabTOTALWEIGHTN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabTOTALWEIGHTN2.Text);
                    }
                    else
                    {
                        txtSaveLabTOTALWEIGHTN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabTOTALWEIGHTN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabTOTALWEIGHTN3.Text);
                    }
                    else
                    {
                        txtSaveLabTOTALWEIGHTN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabTOTALWEIGHTN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtSaveLabTOTALWEIGHTN4.Text);
                    }
                    else
                    {
                        txtSaveLabTOTALWEIGHTN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabTOTALWEIGHTN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtSaveLabTOTALWEIGHTN5.Text);
                    }
                    else
                    {
                        txtSaveLabTOTALWEIGHTN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabTOTALWEIGHTN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtSaveLabTOTALWEIGHTN6.Text);
                    }
                    else
                    {
                        txtSaveLabTOTALWEIGHTN6.Text = string.Empty;
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

                    txtSaveLabTOTALWEIGHTAve.Text = Avg.Value.ToString("#,##0.##");
                }
                else
                {
                    txtSaveLabTOTALWEIGHTAve.Text = "";
                }

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTSpecification.Text))
                {
                    string temp = txtSaveLabTOTALWEIGHTSpecification.Text;

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
                                        txtSaveLabTOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabTOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabTOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabTOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabTOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabTOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabTOTALWEIGHTN1.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabTOTALWEIGHTN2.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabTOTALWEIGHTN3.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabTOTALWEIGHTN4.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabTOTALWEIGHTN5.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabTOTALWEIGHTN6.Background = Brushes.Salmon;
                                    else
                                        txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
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
                                        txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;
                                    else
                                        txtSaveLabTOTALWEIGHTN1.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                        txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;
                                    else
                                        txtSaveLabTOTALWEIGHTN2.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                        txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;
                                    else
                                        txtSaveLabTOTALWEIGHTN3.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                        txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;
                                    else
                                        txtSaveLabTOTALWEIGHTN4.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                        txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;
                                    else
                                        txtSaveLabTOTALWEIGHTN5.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                        txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
                                    else
                                        txtSaveLabTOTALWEIGHTN6.Background = Brushes.Salmon;
                                }
                                else
                                    txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
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
                                txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;
                            else
                                txtSaveLabTOTALWEIGHTN1.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                                txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;
                            else
                                txtSaveLabTOTALWEIGHTN2.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                                txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;
                            else
                                txtSaveLabTOTALWEIGHTN3.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                                txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;
                            else
                                txtSaveLabTOTALWEIGHTN4.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                                txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;
                            else
                                txtSaveLabTOTALWEIGHTN5.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                                txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
                            else
                                txtSaveLabTOTALWEIGHTN6.Background = Brushes.Salmon;
                        }
                        else
                            txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;
                    txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;
                    txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;
                    txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;
                    txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;
                    txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
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

            N_TOTALWEIGHT(null);
            Retest1TOTALWEIGHT(null);
            Retest2TOTALWEIGHT(null);
            SaveLabTOTALWEIGHT(null);

            txtSaveLabTOTALWEIGHTN1.Focus();
            txtSaveLabTOTALWEIGHTN1.SelectAll();
        }

        #endregion

        #region setDefForeground
        private void setDefForeground()
        {
            txtTOTALWEIGHTN1.Background = Brushes.White;
            txtTOTALWEIGHTN2.Background = Brushes.White;
            txtTOTALWEIGHTN3.Background = Brushes.White;
            txtTOTALWEIGHTN4.Background = Brushes.White;
            txtTOTALWEIGHTN5.Background = Brushes.White;
            txtTOTALWEIGHTN6.Background = Brushes.White;

            txtRetest1TOTALWEIGHTN1.Background = Brushes.White;
            txtRetest1TOTALWEIGHTN2.Background = Brushes.White;
            txtRetest1TOTALWEIGHTN3.Background = Brushes.White;
            txtRetest1TOTALWEIGHTN4.Background = Brushes.White;
            txtRetest1TOTALWEIGHTN5.Background = Brushes.White;
            txtRetest1TOTALWEIGHTN6.Background = Brushes.White;

            txtRetest2TOTALWEIGHTN1.Background = Brushes.White;
            txtRetest2TOTALWEIGHTN2.Background = Brushes.White;
            txtRetest2TOTALWEIGHTN3.Background = Brushes.White;
            txtRetest2TOTALWEIGHTN4.Background = Brushes.White;
            txtRetest2TOTALWEIGHTN5.Background = Brushes.White;
            txtRetest2TOTALWEIGHTN6.Background = Brushes.White;

            txtSaveLabTOTALWEIGHTN1.Background = Brushes.White;
            txtSaveLabTOTALWEIGHTN2.Background = Brushes.White;
            txtSaveLabTOTALWEIGHTN3.Background = Brushes.White;
            txtSaveLabTOTALWEIGHTN4.Background = Brushes.White;
            txtSaveLabTOTALWEIGHTN5.Background = Brushes.White;
            txtSaveLabTOTALWEIGHTN6.Background = Brushes.White;
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
                        decimal? weight1 = null;
                        decimal? weight2 = null;
                        decimal? weight3 = null;
                        decimal? weight4 = null;
                        decimal? weight5 = null;
                        decimal? weight6 = null;

                        #region Get Data

                        for (int page = 0; page <= results.Count - 1; page++)
                        {
                            if (page == 0)
                            {
                                if (results[page].WEIGHT1 != null) 
                                {
                                    weight1 = results[page].WEIGHT1;
                                    txtTOTALWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.##"); 
                                }
                                if (results[page].WEIGHT2 != null) 
                                {
                                    weight2 = results[page].WEIGHT2;
                                    txtTOTALWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.##"); 
                                }
                                if (results[page].WEIGHT3 != null) 
                                {
                                    weight3 = results[page].WEIGHT3;
                                    txtTOTALWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.##"); 
                                }
                                if (results[page].WEIGHT4 != null) 
                                {
                                    weight4 = results[page].WEIGHT4;
                                    txtTOTALWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.##"); 
                                }
                                if (results[page].WEIGHT5 != null) 
                                {
                                    weight5 = results[page].WEIGHT5;
                                    txtTOTALWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.##"); 
                                }
                                if (results[page].WEIGHT6 != null) 
                                {
                                    weight6 = results[page].WEIGHT6;
                                    txtTOTALWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.##"); 
                                }
                            }
                            else if (page == 1)
                            {
                                spRetest1.Visibility = Visibility.Visible;

                                if (results[page].WEIGHT1 != null)
                                {
                                    weight1 = results[page].WEIGHT1;
                                    txtRetest1TOTALWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    weight2 = results[page].WEIGHT2;
                                    txtRetest1TOTALWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    weight3 = results[page].WEIGHT3;
                                    txtRetest1TOTALWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    weight4 = results[page].WEIGHT4;
                                    txtRetest1TOTALWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    weight5 = results[page].WEIGHT5;
                                    txtRetest1TOTALWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    weight6 = results[page].WEIGHT6;
                                    txtRetest1TOTALWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.###");
                                }
                            }
                            else if (page == 2)
                            {
                                spRetest2.Visibility = Visibility.Visible;

                                if (results[page].WEIGHT1 != null)
                                {
                                    weight1 = results[page].WEIGHT1;
                                    txtRetest2TOTALWEIGHTN1.Text = results[page].WEIGHT1.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT2 != null)
                                {
                                    weight2 = results[page].WEIGHT2;
                                    txtRetest2TOTALWEIGHTN2.Text = results[page].WEIGHT2.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT3 != null)
                                {
                                    weight3 = results[page].WEIGHT3;
                                    txtRetest2TOTALWEIGHTN3.Text = results[page].WEIGHT3.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT4 != null)
                                {
                                    weight4 = results[page].WEIGHT4;
                                    txtRetest2TOTALWEIGHTN4.Text = results[page].WEIGHT4.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT5 != null)
                                {
                                    weight5 = results[page].WEIGHT5;
                                    txtRetest2TOTALWEIGHTN5.Text = results[page].WEIGHT5.Value.ToString("#,##0.###");
                                }
                                if (results[page].WEIGHT6 != null)
                                {
                                    weight6 = results[page].WEIGHT6;
                                    txtRetest2TOTALWEIGHTN6.Text = results[page].WEIGHT6.Value.ToString("#,##0.###");
                                }
                            }
                            
                        }

                        #endregion

                        if (weight1 != null)
                        { 
                            txtSaveLabTOTALWEIGHTN1.Text = weight1.Value.ToString("#,##0.##"); 
                        }
                        if (weight2 != null) 
                        {
                            txtSaveLabTOTALWEIGHTN2.Text = weight2.Value.ToString("#,##0.##"); 
                        }
                        if (weight3 != null)
                        { 
                            txtSaveLabTOTALWEIGHTN3.Text = weight3.Value.ToString("#,##0.##"); 
                        }
                        if (weight4 != null) 
                        {
                            txtSaveLabTOTALWEIGHTN4.Text = weight4.Value.ToString("#,##0.##");
                        }
                        if (weight5 != null) 
                        {
                            txtSaveLabTOTALWEIGHTN5.Text = weight5.Value.ToString("#,##0.##");
                        }
                        if (weight6 != null) 
                        {
                            txtSaveLabTOTALWEIGHTN6.Text = weight6.Value.ToString("#,##0.##");
                        }

                        TOTALWEIGHT();
                        Retest1TOTALWEIGHT();
                        Retest2TOTALWEIGHT();
                        SaveLabTOTALWEIGHT();
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
                        N_Head(results[0].TOTALWEIGHT);

                        N_TOTALWEIGHT(results[0].TOTALWEIGHT);
                        Retest1TOTALWEIGHT(results[0].TOTALWEIGHT);
                        Retest2TOTALWEIGHT(results[0].TOTALWEIGHT);
                        SaveLabTOTALWEIGHT(results[0].TOTALWEIGHT);
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
                
                txtTOTALWEIGHTSpecification.Text = string.Empty;
                txtRetest1TOTALWEIGHTSpecification.Text = string.Empty;
                txtRetest2TOTALWEIGHTSpecification.Text = string.Empty;
                txtSaveLabTOTALWEIGHTSpecification.Text = string.Empty;

                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        txtTOTALWEIGHTSpecification.Text = results[0].TOTALWEIGHT_Spe;
                        txtRetest1TOTALWEIGHTSpecification.Text = results[0].TOTALWEIGHT_Spe;
                        txtRetest2TOTALWEIGHTSpecification.Text = results[0].TOTALWEIGHT_Spe;
                        txtSaveLabTOTALWEIGHTSpecification.Text = results[0].TOTALWEIGHT_Spe;
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
                    if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN1.Text))
                        saveLabTOTALWEIGHT1 = decimal.Parse(txtSaveLabTOTALWEIGHTN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN2.Text))
                        saveLabTOTALWEIGHT2 = decimal.Parse(txtSaveLabTOTALWEIGHTN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN3.Text))
                        saveLabTOTALWEIGHT3 = decimal.Parse(txtSaveLabTOTALWEIGHTN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN4.Text))
                        saveLabTOTALWEIGHT4 = decimal.Parse(txtSaveLabTOTALWEIGHTN4.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN5.Text))
                        saveLabTOTALWEIGHT5 = decimal.Parse(txtSaveLabTOTALWEIGHTN5.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN6.Text))
                        saveLabTOTALWEIGHT6 = decimal.Parse(txtSaveLabTOTALWEIGHTN6.Text);

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

                    if (txtSaveLabTOTALWEIGHTN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabTOTALWEIGHTN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabTOTALWEIGHTN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabTOTALWEIGHTN4.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN4.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabTOTALWEIGHTN5.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN5.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabTOTALWEIGHTN6.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabTOTALWEIGHTN6.Text))
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

        #region N_TOTALWEIGHT
        private void N_TOTALWEIGHT(decimal? n)
        {
            decimal? TOTALWEIGHT = n;

            txtTOTALWEIGHTSpecification.Text = string.Empty;
            txtTOTALWEIGHTN1.Text = string.Empty;
            txtTOTALWEIGHTN2.Text = string.Empty;
            txtTOTALWEIGHTN3.Text = string.Empty;
            txtTOTALWEIGHTN4.Text = string.Empty;
            txtTOTALWEIGHTN5.Text = string.Empty;
            txtTOTALWEIGHTN6.Text = string.Empty;
            txtTOTALWEIGHTAve.Text = string.Empty;

            if (TOTALWEIGHT != null && TOTALWEIGHT > 0)
            {

                if (TOTALWEIGHT == 6)
                {
                    txtTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (TOTALWEIGHT == 5)
                {
                    txtTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 4)
                {
                    txtTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 3)
                {
                    txtTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 2)
                {
                    txtTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 1)
                {
                    txtTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtTOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtTOTALWEIGHTN1.Visibility = Visibility.Collapsed;
                txtTOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                txtTOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                txtTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                txtTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                txtTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1TOTALWEIGHT
        private void Retest1TOTALWEIGHT(decimal? n)
        {
            decimal? TOTALWEIGHT = n;

            txtRetest1TOTALWEIGHTSpecification.Text = string.Empty;
            txtRetest1TOTALWEIGHTN1.Text = string.Empty;
            txtRetest1TOTALWEIGHTN2.Text = string.Empty;
            txtRetest1TOTALWEIGHTN3.Text = string.Empty;
            txtRetest1TOTALWEIGHTN4.Text = string.Empty;
            txtRetest1TOTALWEIGHTN5.Text = string.Empty;
            txtRetest1TOTALWEIGHTN6.Text = string.Empty;
            txtRetest1TOTALWEIGHTAve.Text = string.Empty;

            if (TOTALWEIGHT != null && TOTALWEIGHT > 0)
            {

                if (TOTALWEIGHT == 6)
                {
                    txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (TOTALWEIGHT == 5)
                {
                    txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 4)
                {
                    txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 3)
                {
                    txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 2)
                {
                    txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 1)
                {
                    txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1TOTALWEIGHTN1.Visibility = Visibility.Collapsed;
                txtRetest1TOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                txtRetest1TOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                txtRetest1TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                txtRetest1TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                txtRetest1TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2TOTALWEIGHT
        private void Retest2TOTALWEIGHT(decimal? n)
        {
            decimal? TOTALWEIGHT = n;

            txtRetest2TOTALWEIGHTSpecification.Text = string.Empty;
            txtRetest2TOTALWEIGHTN1.Text = string.Empty;
            txtRetest2TOTALWEIGHTN2.Text = string.Empty;
            txtRetest2TOTALWEIGHTN3.Text = string.Empty;
            txtRetest2TOTALWEIGHTN4.Text = string.Empty;
            txtRetest2TOTALWEIGHTN5.Text = string.Empty;
            txtRetest2TOTALWEIGHTN6.Text = string.Empty;
            txtRetest2TOTALWEIGHTAve.Text = string.Empty;

            if (TOTALWEIGHT != null && TOTALWEIGHT > 0)
            {

                if (TOTALWEIGHT == 6)
                {
                    txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (TOTALWEIGHT == 5)
                {
                    txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 4)
                {
                    txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 3)
                {
                    txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 2)
                {
                    txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 1)
                {
                    txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2TOTALWEIGHTN1.Visibility = Visibility.Collapsed;
                txtRetest2TOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                txtRetest2TOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                txtRetest2TOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                txtRetest2TOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                txtRetest2TOTALWEIGHTN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLabTOTALWEIGHT
        private void SaveLabTOTALWEIGHT(decimal? n)
        {
            decimal? TOTALWEIGHT = n;

            txtSaveLabTOTALWEIGHTSpecification.Text = string.Empty;
            txtSaveLabTOTALWEIGHTN1.Text = string.Empty;
            txtSaveLabTOTALWEIGHTN2.Text = string.Empty;
            txtSaveLabTOTALWEIGHTN3.Text = string.Empty;
            txtSaveLabTOTALWEIGHTN4.Text = string.Empty;
            txtSaveLabTOTALWEIGHTN5.Text = string.Empty;
            txtSaveLabTOTALWEIGHTN6.Text = string.Empty;
            txtSaveLabTOTALWEIGHTAve.Text = string.Empty;

            if (TOTALWEIGHT != null && TOTALWEIGHT > 0)
            {

                if (TOTALWEIGHT == 6)
                {
                    txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Visible;
                }
                else if (TOTALWEIGHT == 5)
                {
                    txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 4)
                {
                    txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 3)
                {
                    txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 2)
                {
                    txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
                else if (TOTALWEIGHT == 1)
                {
                    txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Visible;
                    txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                    txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabTOTALWEIGHTN1.Visibility = Visibility.Collapsed;
                txtSaveLabTOTALWEIGHTN2.Visibility = Visibility.Collapsed;
                txtSaveLabTOTALWEIGHTN3.Visibility = Visibility.Collapsed;
                txtSaveLabTOTALWEIGHTN4.Visibility = Visibility.Collapsed;
                txtSaveLabTOTALWEIGHTN5.Visibility = Visibility.Collapsed;
                txtSaveLabTOTALWEIGHTN6.Visibility = Visibility.Collapsed;
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
            txtTOTALWEIGHTN1.IsReadOnly = true;
            txtTOTALWEIGHTN2.IsReadOnly = true;
            txtTOTALWEIGHTN3.IsReadOnly = true;
            txtTOTALWEIGHTN4.IsReadOnly = true;
            txtTOTALWEIGHTN5.IsReadOnly = true;
            txtTOTALWEIGHTN6.IsReadOnly = true;

            txtRetest1TOTALWEIGHTN1.IsReadOnly = true;
            txtRetest1TOTALWEIGHTN2.IsReadOnly = true;
            txtRetest1TOTALWEIGHTN3.IsReadOnly = true;
            txtRetest1TOTALWEIGHTN4.IsReadOnly = true;
            txtRetest1TOTALWEIGHTN5.IsReadOnly = true;
            txtRetest1TOTALWEIGHTN6.IsReadOnly = true;

            txtRetest2TOTALWEIGHTN1.IsReadOnly = true;
            txtRetest2TOTALWEIGHTN2.IsReadOnly = true;
            txtRetest2TOTALWEIGHTN3.IsReadOnly = true;
            txtRetest2TOTALWEIGHTN4.IsReadOnly = true;
            txtRetest2TOTALWEIGHTN5.IsReadOnly = true;
            txtRetest2TOTALWEIGHTN6.IsReadOnly = true;

            //txtSaveLabTOTALWEIGHTN1.IsReadOnly = true;
            //txtSaveLabTOTALWEIGHTN2.IsReadOnly = true;
            //txtSaveLabTOTALWEIGHTN3.IsReadOnly = true;
            //txtSaveLabTOTALWEIGHTN4.IsReadOnly = true;
            //txtSaveLabTOTALWEIGHTN5.IsReadOnly = true;
            //txtSaveLabTOTALWEIGHTN6.IsReadOnly = true;
        }
        #endregion

        #endregion

        #region Public Methods

        #region TOTALWEIGHT

        decimal? saveLabTOTALWEIGHT1 = null;
        decimal? saveLabTOTALWEIGHT2 = null;
        decimal? saveLabTOTALWEIGHT3 = null;
        decimal? saveLabTOTALWEIGHT4 = null;
        decimal? saveLabTOTALWEIGHT5 = null;
        decimal? saveLabTOTALWEIGHT6 = null;

        public decimal? TOTALWEIGHT1
        {
            get { return saveLabTOTALWEIGHT1; }
            set { saveLabTOTALWEIGHT1 = value; }
        }

        public decimal? TOTALWEIGHT2
        {
            get { return saveLabTOTALWEIGHT2; }
            set { saveLabTOTALWEIGHT2 = value; }
        }

        public decimal? TOTALWEIGHT3
        {
            get { return saveLabTOTALWEIGHT3; }
            set { saveLabTOTALWEIGHT3 = value; }
        }

        public decimal? TOTALWEIGHT4
        {
            get { return saveLabTOTALWEIGHT4; }
            set { saveLabTOTALWEIGHT4 = value; }
        }

        public decimal? TOTALWEIGHT5
        {
            get { return saveLabTOTALWEIGHT5; }
            set { saveLabTOTALWEIGHT5 = value; }
        }

        public decimal? TOTALWEIGHT6
        {
            get { return saveLabTOTALWEIGHT6; }
            set { saveLabTOTALWEIGHT6 = value; }
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

