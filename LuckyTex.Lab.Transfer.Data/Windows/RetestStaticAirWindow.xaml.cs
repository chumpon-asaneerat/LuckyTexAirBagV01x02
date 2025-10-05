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
    /// Interaction logic for RetestStaticAirWindow.xaml
    /// </summary>
    public partial class RetestStaticAirWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RetestStaticAirWindow()
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
            LAB_GETSTATICAIRDATA(txtITMCODE.Text, txtWEAVINGLOG.Text);

            txtSaveLabSTATIC_AIRN1.Focus();
            txtSaveLabSTATIC_AIRN1.SelectAll();
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
                if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN1.Text) && txtSaveLabSTATIC_AIRN1.IsVisible == true)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN2.Text) && txtSaveLabSTATIC_AIRN2.IsVisible == true)
                {
                    txtSaveLabSTATIC_AIRN2.Focus();
                    txtSaveLabSTATIC_AIRN2.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN3.Text) && txtSaveLabSTATIC_AIRN3.IsVisible == true)
                {
                    txtSaveLabSTATIC_AIRN3.Focus();
                    txtSaveLabSTATIC_AIRN3.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN4.Text) && txtSaveLabSTATIC_AIRN4.IsVisible == true)
                {
                    txtSaveLabSTATIC_AIRN4.Focus();
                    txtSaveLabSTATIC_AIRN4.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN5.Text) && txtSaveLabSTATIC_AIRN5.IsVisible == true)
                {
                    txtSaveLabSTATIC_AIRN5.Focus();
                    txtSaveLabSTATIC_AIRN5.SelectAll();
                }
                else if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN6.Text) && txtSaveLabSTATIC_AIRN6.IsVisible == true)
                {
                    txtSaveLabSTATIC_AIRN6.Focus();
                    txtSaveLabSTATIC_AIRN6.SelectAll();
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
                if (txtSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSTATIC_AIRN1.Focus();
                    txtSTATIC_AIRN1.SelectAll();
                }

                e.Handled = true;
            }
        }
        #endregion

        #region Item Property

        #region KeyDown

        #region STATIC_AIR
        private void txtSTATIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTATIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtSTATIC_AIRN2.Focus();
                    txtSTATIC_AIRN2.SelectAll();
                }
                else if (txtRetest1STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN1.Focus();
                    txtRetest1STATIC_AIRN1.SelectAll();
                }
               
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTATIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtSTATIC_AIRN3.Focus();
                    txtSTATIC_AIRN3.SelectAll();
                }
                else if (txtRetest1STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN1.Focus();
                    txtRetest1STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTATIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtSTATIC_AIRN4.Focus();
                    txtSTATIC_AIRN4.SelectAll();
                }
                else if (txtRetest1STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN1.Focus();
                    txtRetest1STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTATIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtSTATIC_AIRN5.Focus();
                    txtSTATIC_AIRN5.SelectAll();
                }
                else if (txtRetest1STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN1.Focus();
                    txtRetest1STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTATIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtSTATIC_AIRN6.Focus();
                    txtSTATIC_AIRN6.SelectAll();
                }
                else if (txtRetest1STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN1.Focus();
                    txtRetest1STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN1.Focus();
                    txtRetest1STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest1 STATIC_AIR
        private void txtRetest1STATIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STATIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN2.Focus();
                    txtRetest1STATIC_AIRN2.SelectAll();
                }
                else if (txtRetest2STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN1.Focus();
                    txtRetest2STATIC_AIRN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest1STATIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STATIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN3.Focus();
                    txtRetest1STATIC_AIRN3.SelectAll();
                }
                else if (txtRetest2STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN1.Focus();
                    txtRetest2STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STATIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STATIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN4.Focus();
                    txtRetest1STATIC_AIRN4.SelectAll();
                }
                else if (txtRetest2STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN1.Focus();
                    txtRetest2STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STATIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STATIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN5.Focus();
                    txtRetest1STATIC_AIRN5.SelectAll();
                }
                else if (txtRetest2STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN1.Focus();
                    txtRetest2STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STATIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest1STATIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtRetest1STATIC_AIRN6.Focus();
                    txtRetest1STATIC_AIRN6.SelectAll();
                }
                else if (txtRetest2STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN1.Focus();
                    txtRetest2STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest1STATIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN1.Focus();
                    txtRetest2STATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region Retest2 STATIC_AIR
        private void txtRetest2STATIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STATIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN2.Focus();
                    txtRetest2STATIC_AIRN2.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }

                e.Handled = true;
            }
        }

        private void txtRetest2STATIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STATIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN3.Focus();
                    txtRetest2STATIC_AIRN3.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STATIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STATIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN4.Focus();
                    txtRetest2STATIC_AIRN4.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STATIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STATIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN5.Focus();
                    txtRetest2STATIC_AIRN5.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STATIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtRetest2STATIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtRetest2STATIC_AIRN6.Focus();
                    txtRetest2STATIC_AIRN6.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtRetest2STATIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN1.Focus();
                    txtSaveLabSTATIC_AIRN1.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region SaveLab STATIC_AIR
        private void txtSaveLabSTATIC_AIRN1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN2.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN2.Focus();
                    txtSaveLabSTATIC_AIRN2.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtSaveLabSTATIC_AIRN2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN3.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN3.Focus();
                    txtSaveLabSTATIC_AIRN3.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTATIC_AIRN3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN4.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN4.Focus();
                    txtSaveLabSTATIC_AIRN4.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTATIC_AIRN4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN5.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN5.Focus();
                    txtSaveLabSTATIC_AIRN5.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTATIC_AIRN5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN6.Visibility == Visibility.Visible)
                {
                    txtSaveLabSTATIC_AIRN6.Focus();
                    txtSaveLabSTATIC_AIRN6.SelectAll();
                }
                else if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
                {
                    cmdSelect.Focus();
                }
                e.Handled = true;
            }
        }

        private void txtSaveLabSTATIC_AIRN6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSaveLabSTATIC_AIRN1.Visibility == Visibility.Visible)
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

        #region STATIC_AIR_LostFocus
        private void STATIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            STATIC_AIR(); 
        }
        #endregion

        #region Retest1STATIC_AIR_LostFocus
        private void Retest1STATIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest1STATIC_AIR();
        }
        #endregion

        #region Retest2STATIC_AIR_LostFocus
        private void Retest2STATIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            Retest2STATIC_AIR();
        }
        #endregion

        #region SaveLabSTATIC_AIR_LostFocus
        private void SaveLabSTATIC_AIR_LostFocus(object sender, RoutedEventArgs e)
        {
            SaveLabSTATIC_AIR();
        }
        #endregion

        #region STATIC_AIR
        private void STATIC_AIR()
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

                if (!string.IsNullOrEmpty(txtSTATIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSTATIC_AIRN1.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTATIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSTATIC_AIRN2.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTATIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSTATIC_AIRN3.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTATIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtSTATIC_AIRN4.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTATIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtSTATIC_AIRN5.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSTATIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtSTATIC_AIRN6.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRN6.Text = string.Empty;
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
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 3);

                    txtSTATIC_AIRAve.Text = Avg.Value.ToString("#,##0.###");
                }
                else
                {
                    txtSTATIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave1)
                        {
                            txtSTATIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtSTATIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtSTATIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave2)
                            txtSTATIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtSTATIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtSTATIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave3)
                            txtSTATIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtSTATIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtSTATIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave4)
                            txtSTATIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtSTATIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtSTATIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave5)
                            txtSTATIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtSTATIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtSTATIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave6)
                            txtSTATIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtSTATIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtSTATIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtSTATIC_AIRSpecification.Text))
                {
                    string temp = txtSTATIC_AIRSpecification.Text;

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
                                        txtSTATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSTATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSTATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSTATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSTATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSTATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSTATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSTATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSTATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSTATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSTATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSTATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtSTATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtSTATIC_AIRN6.Background = Brushes.White;
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
                                        //txtSTATIC_AIRN1.Background = Brushes.White;
                                    }
                                    else
                                        txtSTATIC_AIRN1.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSTATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        //txtSTATIC_AIRN2.Background = Brushes.White;
                                    }
                                    else
                                        txtSTATIC_AIRN2.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSTATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        //txtSTATIC_AIRN3.Background = Brushes.White;
                                    }
                                    else
                                        txtSTATIC_AIRN3.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSTATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        //txtSTATIC_AIRN4.Background = Brushes.White;
                                    }
                                    else
                                        txtSTATIC_AIRN4.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSTATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        //txtSTATIC_AIRN5.Background = Brushes.White;
                                    }
                                    else
                                        txtSTATIC_AIRN5.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSTATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        //txtSTATIC_AIRN6.Background = Brushes.White;
                                    }
                                    else
                                        txtSTATIC_AIRN6.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSTATIC_AIRN6.Background = Brushes.White;
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
                                //txtSTATIC_AIRN1.Background = Brushes.White;
                            }
                            else
                                txtSTATIC_AIRN1.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSTATIC_AIRN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                //txtSTATIC_AIRN2.Background = Brushes.White;
                            }
                            else
                                txtSTATIC_AIRN2.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSTATIC_AIRN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                //txtSTATIC_AIRN3.Background = Brushes.White;
                            }
                            else
                                txtSTATIC_AIRN3.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSTATIC_AIRN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                //txtSTATIC_AIRN4.Background = Brushes.White;
                            }
                            else
                                txtSTATIC_AIRN4.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSTATIC_AIRN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                //txtSTATIC_AIRN5.Background = Brushes.White;
                            }
                            else
                                txtSTATIC_AIRN5.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSTATIC_AIRN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                //txtSTATIC_AIRN6.Background = Brushes.White;
                            }
                            else
                                txtSTATIC_AIRN6.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSTATIC_AIRN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtSTATIC_AIRN1.Background = Brushes.White;
                    txtSTATIC_AIRN2.Background = Brushes.White;
                    txtSTATIC_AIRN3.Background = Brushes.White;
                    txtSTATIC_AIRN4.Background = Brushes.White;
                    txtSTATIC_AIRN5.Background = Brushes.White;
                    txtSTATIC_AIRN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest1STATIC_AIR
        private void Retest1STATIC_AIR()
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

                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtRetest1STATIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest1STATIC_AIRN1.Text);
                    }
                    else
                    {
                        txtRetest1STATIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtRetest1STATIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest1STATIC_AIRN2.Text);
                    }
                    else
                    {
                        txtRetest1STATIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtRetest1STATIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest1STATIC_AIRN3.Text);
                    }
                    else
                    {
                        txtRetest1STATIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtRetest1STATIC_AIRN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest1STATIC_AIRN4.Text);
                    }
                    else
                    {
                        txtRetest1STATIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtRetest1STATIC_AIRN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest1STATIC_AIRN5.Text);
                    }
                    else
                    {
                        txtRetest1STATIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtRetest1STATIC_AIRN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest1STATIC_AIRN6.Text);
                    }
                    else
                    {
                        txtRetest1STATIC_AIRN6.Text = string.Empty;
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
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 3);

                    txtRetest1STATIC_AIRAve.Text = Avg.Value.ToString("#,##0.###");
                }
                else
                {
                    txtRetest1STATIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave1)
                        {
                            txtRetest1STATIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtRetest1STATIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtRetest1STATIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave2)
                            txtRetest1STATIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1STATIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtRetest1STATIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave3)
                            txtRetest1STATIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1STATIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtRetest1STATIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave4)
                            txtRetest1STATIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1STATIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtRetest1STATIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave5)
                            txtRetest1STATIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1STATIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtRetest1STATIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave6)
                            txtRetest1STATIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtRetest1STATIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtRetest1STATIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest1STATIC_AIRSpecification.Text))
                {
                    string temp = txtRetest1STATIC_AIRSpecification.Text;

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
                                        txtRetest1STATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest1STATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest1STATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest1STATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest1STATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest1STATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest1STATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest1STATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest1STATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest1STATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest1STATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest1STATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest1STATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN6.Background = Brushes.White;
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
                                        //txtRetest1STATIC_AIRN1.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STATIC_AIRN1.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        //txtRetest1STATIC_AIRN2.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STATIC_AIRN2.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        //txtRetest1STATIC_AIRN3.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STATIC_AIRN3.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        //txtRetest1STATIC_AIRN4.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STATIC_AIRN4.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        //txtRetest1STATIC_AIRN5.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STATIC_AIRN5.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        //txtRetest1STATIC_AIRN6.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest1STATIC_AIRN6.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest1STATIC_AIRN6.Background = Brushes.White;
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
                                //txtRetest1STATIC_AIRN1.Background = Brushes.White;
                            }
                            else
                                txtRetest1STATIC_AIRN1.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest1STATIC_AIRN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                //txtRetest1STATIC_AIRN2.Background = Brushes.White;
                            }
                            else
                                txtRetest1STATIC_AIRN2.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest1STATIC_AIRN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                //txtRetest1STATIC_AIRN3.Background = Brushes.White;
                            }
                            else
                                txtRetest1STATIC_AIRN3.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest1STATIC_AIRN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                //txtRetest1STATIC_AIRN4.Background = Brushes.White;
                            }
                            else
                                txtRetest1STATIC_AIRN4.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest1STATIC_AIRN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                //txtRetest1STATIC_AIRN5.Background = Brushes.White;
                            }
                            else
                                txtRetest1STATIC_AIRN5.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest1STATIC_AIRN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                //txtRetest1STATIC_AIRN6.Background = Brushes.White;
                            }
                            else
                                txtRetest1STATIC_AIRN6.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest1STATIC_AIRN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest1STATIC_AIRN1.Background = Brushes.White;
                    txtRetest1STATIC_AIRN2.Background = Brushes.White;
                    txtRetest1STATIC_AIRN3.Background = Brushes.White;
                    txtRetest1STATIC_AIRN4.Background = Brushes.White;
                    txtRetest1STATIC_AIRN5.Background = Brushes.White;
                    txtRetest1STATIC_AIRN6.Background = Brushes.White;
                }
                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region Retest2STATIC_AIR
        private void Retest2STATIC_AIR()
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

                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtRetest2STATIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtRetest2STATIC_AIRN1.Text);
                    }
                    else
                    {
                        txtRetest2STATIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtRetest2STATIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtRetest2STATIC_AIRN2.Text);
                    }
                    else
                    {
                        txtRetest2STATIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtRetest2STATIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtRetest2STATIC_AIRN3.Text);
                    }
                    else
                    {
                        txtRetest2STATIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtRetest2STATIC_AIRN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtRetest2STATIC_AIRN4.Text);
                    }
                    else
                    {
                        txtRetest2STATIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtRetest2STATIC_AIRN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtRetest2STATIC_AIRN5.Text);
                    }
                    else
                    {
                        txtRetest2STATIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtRetest2STATIC_AIRN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtRetest2STATIC_AIRN6.Text);
                    }
                    else
                    {
                        txtRetest2STATIC_AIRN6.Text = string.Empty;
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
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 3);

                    txtRetest2STATIC_AIRAve.Text = Avg.Value.ToString("#,##0.###");
                }
                else
                {
                    txtRetest2STATIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave1)
                        {
                            txtRetest2STATIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtRetest2STATIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtRetest2STATIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave2)
                            txtRetest2STATIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2STATIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtRetest2STATIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave3)
                            txtRetest2STATIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2STATIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtRetest2STATIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave4)
                            txtRetest2STATIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2STATIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtRetest2STATIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave5)
                            txtRetest2STATIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2STATIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtRetest2STATIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave6)
                            txtRetest2STATIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtRetest2STATIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtRetest2STATIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtRetest2STATIC_AIRSpecification.Text))
                {
                    string temp = txtRetest2STATIC_AIRSpecification.Text;

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
                                        txtRetest2STATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtRetest2STATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtRetest2STATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtRetest2STATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtRetest2STATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtRetest2STATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtRetest2STATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtRetest2STATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtRetest2STATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtRetest2STATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtRetest2STATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtRetest2STATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtRetest2STATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN6.Background = Brushes.White;
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
                                        //txtRetest2STATIC_AIRN1.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STATIC_AIRN1.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        //txtRetest2STATIC_AIRN2.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STATIC_AIRN2.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        //txtRetest2STATIC_AIRN3.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STATIC_AIRN3.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        //txtRetest2STATIC_AIRN4.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STATIC_AIRN4.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        //txtRetest2STATIC_AIRN5.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STATIC_AIRN5.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        //txtRetest2STATIC_AIRN6.Background = Brushes.White;
                                    }
                                    else
                                        txtRetest2STATIC_AIRN6.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtRetest2STATIC_AIRN6.Background = Brushes.White;
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
                                //txtRetest2STATIC_AIRN1.Background = Brushes.White;
                            }
                            else
                                txtRetest2STATIC_AIRN1.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest2STATIC_AIRN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                //txtRetest2STATIC_AIRN2.Background = Brushes.White;
                            }
                            else
                                txtRetest2STATIC_AIRN2.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest2STATIC_AIRN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                //txtRetest2STATIC_AIRN3.Background = Brushes.White;
                            }
                            else
                                txtRetest2STATIC_AIRN3.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest2STATIC_AIRN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                //txtRetest2STATIC_AIRN4.Background = Brushes.White;
                            }
                            else
                                txtRetest2STATIC_AIRN4.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest2STATIC_AIRN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                //txtRetest2STATIC_AIRN5.Background = Brushes.White;
                            }
                            else
                                txtRetest2STATIC_AIRN5.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest2STATIC_AIRN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                //txtRetest2STATIC_AIRN6.Background = Brushes.White;
                            }
                            else
                                txtRetest2STATIC_AIRN6.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtRetest2STATIC_AIRN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtRetest2STATIC_AIRN1.Background = Brushes.White;
                    txtRetest2STATIC_AIRN2.Background = Brushes.White;
                    txtRetest2STATIC_AIRN3.Background = Brushes.White;
                    txtRetest2STATIC_AIRN4.Background = Brushes.White;
                    txtRetest2STATIC_AIRN5.Background = Brushes.White;
                    txtRetest2STATIC_AIRN6.Background = Brushes.White;
                }
                #endregion

            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region SaveLabSTATIC_AIR
        private void SaveLabSTATIC_AIR()
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

                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN1.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTATIC_AIRN1.Text, out value))
                    {
                        ave1 = decimal.Parse(txtSaveLabSTATIC_AIRN1.Text);
                    }
                    else
                    {
                        txtSaveLabSTATIC_AIRN1.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN2.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTATIC_AIRN2.Text, out value))
                    {
                        ave2 = decimal.Parse(txtSaveLabSTATIC_AIRN2.Text);
                    }
                    else
                    {
                        txtSaveLabSTATIC_AIRN2.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN3.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTATIC_AIRN3.Text, out value))
                    {
                        ave3 = decimal.Parse(txtSaveLabSTATIC_AIRN3.Text);
                    }
                    else
                    {
                        txtSaveLabSTATIC_AIRN3.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN4.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTATIC_AIRN4.Text, out value))
                    {
                        ave4 = decimal.Parse(txtSaveLabSTATIC_AIRN4.Text);
                    }
                    else
                    {
                        txtSaveLabSTATIC_AIRN4.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN5.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTATIC_AIRN5.Text, out value))
                    {
                        ave5 = decimal.Parse(txtSaveLabSTATIC_AIRN5.Text);
                    }
                    else
                    {
                        txtSaveLabSTATIC_AIRN5.Text = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN6.Text))
                {
                    if (Decimal.TryParse(txtSaveLabSTATIC_AIRN6.Text, out value))
                    {
                        ave6 = decimal.Parse(txtSaveLabSTATIC_AIRN6.Text);
                    }
                    else
                    {
                        txtSaveLabSTATIC_AIRN6.Text = string.Empty;
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
                    Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 3);

                    txtSaveLabSTATIC_AIRAve.Text = Avg.Value.ToString("#,##0.###");
                }
                else
                {
                    txtSaveLabSTATIC_AIRAve.Text = "";
                }

                #region Background Out of control
                if (_item_LCL_UCL != null)
                {
                    if (ave1 != null && ave1 != 0)
                    {
                        if (ave1 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave1)
                        {
                            txtSaveLabSTATIC_AIRN1.Background = Brushes.LemonChiffon;
                        }
                        else
                            txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
                    }
                    else
                        txtSaveLabSTATIC_AIRN1.Background = Brushes.White;

                    if (ave2 != null && ave2 != 0)
                    {
                        if (ave2 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave2)
                            txtSaveLabSTATIC_AIRN2.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
                    }
                    else
                        txtSaveLabSTATIC_AIRN2.Background = Brushes.White;

                    if (ave3 != null && ave3 != 0)
                    {
                        if (ave3 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave3)
                            txtSaveLabSTATIC_AIRN3.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
                    }
                    else
                        txtSaveLabSTATIC_AIRN3.Background = Brushes.White;

                    if (ave4 != null && ave4 != 0)
                    {
                        if (ave4 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave4)
                            txtSaveLabSTATIC_AIRN4.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
                    }
                    else
                        txtSaveLabSTATIC_AIRN4.Background = Brushes.White;

                    if (ave5 != null && ave5 != 0)
                    {
                        if (ave5 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave5)
                            txtSaveLabSTATIC_AIRN5.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
                    }
                    else
                        txtSaveLabSTATIC_AIRN5.Background = Brushes.White;

                    if (ave6 != null && ave6 != 0)
                    {
                        if (ave6 < _item_LCL_UCL.STATIC_AIR_LCL || _item_LCL_UCL.STATIC_AIR_UCL < ave6)
                            txtSaveLabSTATIC_AIRN6.Background = Brushes.LemonChiffon;
                        else
                            txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                    }
                    else
                        txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                }
                #endregion

                #region Background Over
                if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRSpecification.Text))
                {
                    string temp = txtSaveLabSTATIC_AIRSpecification.Text;

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
                                        txtSaveLabSTATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num < ave2)
                                        txtSaveLabSTATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num < ave3)
                                        txtSaveLabSTATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num < ave4)
                                        txtSaveLabSTATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num < ave5)
                                        txtSaveLabSTATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num < ave6)
                                        txtSaveLabSTATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                            }
                            else if (temp.Contains("MIN"))
                            {
                                if (Decimal.TryParse(myArr[1].ToString().Trim(), out value))
                                    num = decimal.Parse(myArr[1].ToString().Trim());

                                if (ave1 != null && ave1 != 0)
                                {
                                    if (num > ave1)
                                        txtSaveLabSTATIC_AIRN1.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (num > ave2)
                                        txtSaveLabSTATIC_AIRN2.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (num > ave3)
                                        txtSaveLabSTATIC_AIRN3.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (num > ave4)
                                        txtSaveLabSTATIC_AIRN4.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (num > ave5)
                                        txtSaveLabSTATIC_AIRN5.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (num > ave6)
                                        txtSaveLabSTATIC_AIRN6.Background = Brushes.Salmon;
                                    //else
                                    //    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
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
                                        //txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTATIC_AIRN1.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;

                                if (ave2 != null && ave2 != 0)
                                {
                                    if (lower <= ave2 && ave2 <= upper)
                                    {
                                        //txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTATIC_AIRN2.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;

                                if (ave3 != null && ave3 != 0)
                                {
                                    if (lower <= ave3 && ave3 <= upper)
                                    {
                                        //txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTATIC_AIRN3.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;

                                if (ave4 != null && ave4 != 0)
                                {
                                    if (lower <= ave4 && ave4 <= upper)
                                    {
                                        //txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTATIC_AIRN4.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;

                                if (ave5 != null && ave5 != 0)
                                {
                                    if (lower <= ave5 && ave5 <= upper)
                                    {
                                        //txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTATIC_AIRN5.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;

                                if (ave6 != null && ave6 != 0)
                                {
                                    if (lower <= ave6 && ave6 <= upper)
                                    {
                                        //txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                                    }
                                    else
                                        txtSaveLabSTATIC_AIRN6.Background = Brushes.Salmon;
                                }
                                //else
                                //    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
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
                                //txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTATIC_AIRN1.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;

                        if (ave2 != null && ave2 != 0)
                        {
                            if (ave2 == num3)
                            {
                                //txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTATIC_AIRN2.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;

                        if (ave3 != null && ave3 != 0)
                        {
                            if (ave3 == num3)
                            {
                                //txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTATIC_AIRN3.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;

                        if (ave4 != null && ave4 != 0)
                        {
                            if (ave4 == num3)
                            {
                                //txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTATIC_AIRN4.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;

                        if (ave5 != null && ave5 != 0)
                        {
                            if (ave5 == num3)
                            {
                                //txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTATIC_AIRN5.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;

                        if (ave6 != null && ave6 != 0)
                        {
                            if (ave6 == num3)
                            {
                                //txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
                            }
                            else
                                txtSaveLabSTATIC_AIRN6.Background = Brushes.Salmon;
                        }
                        //else
                        //    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;

                    }
                }
                else
                {
                    txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
                    txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
                    txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
                    txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
                    txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
                    txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
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
            setDefBackground();

            txtITMCODE.Text = string.Empty;
            txtWEAVINGLOG.Text = string.Empty;
            txtFINISHINGLOT.Text = string.Empty;

            spRetest1.Visibility = Visibility.Collapsed;
            spRetest2.Visibility = Visibility.Collapsed;

            N_Head(null);
           
            N_STATIC_AIR(null);
            Retest1STATIC_AIR(null);
            Retest2STATIC_AIR(null);

            SaveLabSTATIC_AIR(null);

            _item_LCL_UCL = new LAB_GETITEM_LCL_UCL();

            txtSaveLabSTATIC_AIRN1.Focus();
            txtSaveLabSTATIC_AIRN1.SelectAll();
        }

        #endregion

        #region setDefBackground
        private void setDefBackground()
        {

            txtSTATIC_AIRN1.Background = Brushes.White;
            txtSTATIC_AIRN2.Background = Brushes.White;
            txtSTATIC_AIRN3.Background = Brushes.White;
            txtSTATIC_AIRN4.Background = Brushes.White;
            txtSTATIC_AIRN5.Background = Brushes.White;
            txtSTATIC_AIRN6.Background = Brushes.White;

            txtRetest1STATIC_AIRN1.Background = Brushes.White;
            txtRetest1STATIC_AIRN2.Background = Brushes.White;
            txtRetest1STATIC_AIRN3.Background = Brushes.White;
            txtRetest1STATIC_AIRN4.Background = Brushes.White;
            txtRetest1STATIC_AIRN5.Background = Brushes.White;
            txtRetest1STATIC_AIRN6.Background = Brushes.White;

            txtRetest2STATIC_AIRN1.Background = Brushes.White;
            txtRetest2STATIC_AIRN2.Background = Brushes.White;
            txtRetest2STATIC_AIRN3.Background = Brushes.White;
            txtRetest2STATIC_AIRN4.Background = Brushes.White;
            txtRetest2STATIC_AIRN5.Background = Brushes.White;
            txtRetest2STATIC_AIRN6.Background = Brushes.White;

            txtSaveLabSTATIC_AIRN1.Background = Brushes.White;
            txtSaveLabSTATIC_AIRN2.Background = Brushes.White;
            txtSaveLabSTATIC_AIRN3.Background = Brushes.White;
            txtSaveLabSTATIC_AIRN4.Background = Brushes.White;
            txtSaveLabSTATIC_AIRN5.Background = Brushes.White;
            txtSaveLabSTATIC_AIRN6.Background = Brushes.White;
        }
        #endregion

        #region LAB_GETLABDETAIL
        private bool LAB_GETSTATICAIRDATA(string P_ITMCODE, string P_WEAVINGLOG)
        {
            bool chkLoad = true;

            try
            {
                spRetest1.Visibility = Visibility.Collapsed;
                spRetest2.Visibility = Visibility.Collapsed;

                List<LAB_GETSTATICAIRDATA> results = LabDataPDFDataService.Instance.LAB_GETSTATICAIRDATA(P_ITMCODE, P_WEAVINGLOG);
                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        decimal? STATIC_AIR1 = null;
                        decimal? STATIC_AIR2 = null;
                        decimal? STATIC_AIR3 = null;
                        decimal? STATIC_AIR4 = null;
                        decimal? STATIC_AIR5 = null;
                        decimal? STATIC_AIR6 = null;

                        #region Get Data
 
                        for (int page = 0; page <= results.Count - 1; page++)
                        {
                            if (page == 0)
                            {
                                if (results[page].STATIC_AIR1 != null)
                                {
                                    STATIC_AIR1 = results[page].STATIC_AIR1;
                                    txtSTATIC_AIRN1.Text = results[page].STATIC_AIR1.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR2 != null)
                                {
                                    STATIC_AIR2 = results[page].STATIC_AIR2;
                                    txtSTATIC_AIRN2.Text = results[page].STATIC_AIR2.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR3 != null)
                                {
                                    STATIC_AIR3 = results[page].STATIC_AIR3;
                                    txtSTATIC_AIRN3.Text = results[page].STATIC_AIR3.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR4 != null)
                                {
                                    STATIC_AIR4 = results[page].STATIC_AIR4;
                                    txtSTATIC_AIRN4.Text = results[page].STATIC_AIR4.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR5 != null)
                                {
                                    STATIC_AIR5 = results[page].STATIC_AIR5;
                                    txtSTATIC_AIRN5.Text = results[page].STATIC_AIR5.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR6 != null)
                                {
                                    STATIC_AIR6 = results[page].STATIC_AIR6;
                                    txtSTATIC_AIRN6.Text = results[page].STATIC_AIR6.Value.ToString("#,##0.###");
                                }
                            }
                            else if (page == 1)
                            {
                                spRetest1.Visibility = Visibility.Visible;

                                if (results[page].STATIC_AIR1 != null)
                                {
                                    STATIC_AIR1 = results[page].STATIC_AIR1;
                                    txtRetest1STATIC_AIRN1.Text = results[page].STATIC_AIR1.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR2 != null)
                                {
                                    STATIC_AIR2 = results[page].STATIC_AIR2;
                                    txtRetest1STATIC_AIRN2.Text = results[page].STATIC_AIR2.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR3 != null)
                                {
                                    STATIC_AIR3 = results[page].STATIC_AIR3;
                                    txtRetest1STATIC_AIRN3.Text = results[page].STATIC_AIR3.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR4 != null)
                                {
                                    STATIC_AIR4 = results[page].STATIC_AIR4;
                                    txtRetest1STATIC_AIRN4.Text = results[page].STATIC_AIR4.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR5 != null)
                                {
                                    STATIC_AIR5 = results[page].STATIC_AIR5;
                                    txtRetest1STATIC_AIRN5.Text = results[page].STATIC_AIR5.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR6 != null)
                                {
                                    STATIC_AIR6 = results[page].STATIC_AIR6;
                                    txtRetest1STATIC_AIRN6.Text = results[page].STATIC_AIR6.Value.ToString("#,##0.###");
                                }
                            }
                            else if (page == 2)
                            {
                                spRetest2.Visibility = Visibility.Visible;

                                if (results[page].STATIC_AIR1 != null)
                                {
                                    STATIC_AIR1 = results[page].STATIC_AIR1;
                                    txtRetest2STATIC_AIRN1.Text = results[page].STATIC_AIR1.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR2 != null)
                                {
                                    STATIC_AIR2 = results[page].STATIC_AIR2;
                                    txtRetest2STATIC_AIRN2.Text = results[page].STATIC_AIR2.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR3 != null)
                                {
                                    STATIC_AIR3 = results[page].STATIC_AIR3;
                                    txtRetest2STATIC_AIRN3.Text = results[page].STATIC_AIR3.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR4 != null)
                                {
                                    STATIC_AIR4 = results[page].STATIC_AIR4;
                                    txtRetest2STATIC_AIRN4.Text = results[page].STATIC_AIR4.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR5 != null)
                                {
                                    STATIC_AIR5 = results[page].STATIC_AIR5;
                                    txtRetest2STATIC_AIRN5.Text = results[page].STATIC_AIR5.Value.ToString("#,##0.###");
                                }
                                if (results[page].STATIC_AIR6 != null)
                                {
                                    STATIC_AIR6 = results[page].STATIC_AIR6;
                                    txtRetest2STATIC_AIRN6.Text = results[page].STATIC_AIR6.Value.ToString("#,##0.###");
                                }
                            }
                        }

                        #endregion

                        if (STATIC_AIR1 != null) { txtSaveLabSTATIC_AIRN1.Text = STATIC_AIR1.Value.ToString("#,##0.###"); }
                        if (STATIC_AIR2 != null) { txtSaveLabSTATIC_AIRN2.Text = STATIC_AIR2.Value.ToString("#,##0.###"); }
                        if (STATIC_AIR3 != null) { txtSaveLabSTATIC_AIRN3.Text = STATIC_AIR3.Value.ToString("#,##0.###"); }
                        if (STATIC_AIR4 != null) { txtSaveLabSTATIC_AIRN4.Text = STATIC_AIR4.Value.ToString("#,##0.###"); }
                        if (STATIC_AIR5 != null) { txtSaveLabSTATIC_AIRN5.Text = STATIC_AIR5.Value.ToString("#,##0.###"); }
                        if (STATIC_AIR6 != null) { txtSaveLabSTATIC_AIRN6.Text = STATIC_AIR6.Value.ToString("#,##0.###"); }

                        STATIC_AIR();
                        Retest1STATIC_AIR();
                        Retest2STATIC_AIR();
                        SaveLabSTATIC_AIR();
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
                        N_Head(results[0].STATIC_AIR);

                        N_STATIC_AIR(results[0].STATIC_AIR);
                        Retest1STATIC_AIR(results[0].STATIC_AIR);
                        Retest2STATIC_AIR(results[0].STATIC_AIR);
                        SaveLabSTATIC_AIR(results[0].STATIC_AIR);
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
                txtSTATIC_AIRSpecification.Text = string.Empty;
                txtRetest1STATIC_AIRSpecification.Text = string.Empty;
                txtRetest2STATIC_AIRSpecification.Text = string.Empty;
                txtSaveLabSTATIC_AIRSpecification.Text = string.Empty;

                _item_LCL_UCL = new LAB_GETITEM_LCL_UCL();

                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        txtSTATIC_AIRSpecification.Text = results[0].STATIC_AIR_Spe;

                        txtRetest1STATIC_AIRSpecification.Text = results[0].STATIC_AIR_Spe;
                        txtRetest2STATIC_AIRSpecification.Text = results[0].STATIC_AIR_Spe;
                        txtSaveLabSTATIC_AIRSpecification.Text = results[0].STATIC_AIR_Spe;

                        _item_LCL_UCL.STATIC_AIR_LCL = results[0].STATIC_AIR_LCL;
                        _item_LCL_UCL.STATIC_AIR_UCL = results[0].STATIC_AIR_UCL;
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
                    if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN1.Text))
                        saveLabSTATIC_AIR1 = decimal.Parse(txtSaveLabSTATIC_AIRN1.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN2.Text))
                        saveLabSTATIC_AIR2 = decimal.Parse(txtSaveLabSTATIC_AIRN2.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN3.Text))
                        saveLabSTATIC_AIR3 = decimal.Parse(txtSaveLabSTATIC_AIRN3.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN4.Text))
                        saveLabSTATIC_AIR4 = decimal.Parse(txtSaveLabSTATIC_AIRN4.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN5.Text))
                        saveLabSTATIC_AIR5 = decimal.Parse(txtSaveLabSTATIC_AIRN5.Text);

                    if (!string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN6.Text))
                        saveLabSTATIC_AIR6 = decimal.Parse(txtSaveLabSTATIC_AIRN6.Text);

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

                    if (txtSaveLabSTATIC_AIRN1.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN1.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTATIC_AIRN2.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN2.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTATIC_AIRN3.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN3.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTATIC_AIRN4.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN4.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTATIC_AIRN5.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN5.Text))
                        {
                            chkSave = false;
                            break;
                        }
                    }
                    if (txtSaveLabSTATIC_AIRN6.IsVisible == true)
                    {
                        if (string.IsNullOrEmpty(txtSaveLabSTATIC_AIRN6.Text))
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

        #region N_STATIC_AIR
        private void N_STATIC_AIR(decimal? n)
        {
            decimal? STATIC_AIR = n;

            txtSTATIC_AIRSpecification.Text = string.Empty;
            txtSTATIC_AIRN1.Text = string.Empty;
            txtSTATIC_AIRN2.Text = string.Empty;
            txtSTATIC_AIRN3.Text = string.Empty;
            txtSTATIC_AIRN4.Text = string.Empty;
            txtSTATIC_AIRN5.Text = string.Empty;
            txtSTATIC_AIRN6.Text = string.Empty;
            txtSTATIC_AIRAve.Text = string.Empty;

            if (STATIC_AIR != null && STATIC_AIR > 0)
            {

                if (STATIC_AIR == 6)
                {
                    txtSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN4.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN5.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (STATIC_AIR == 5)
                {
                    txtSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN4.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN5.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 4)
                {
                    txtSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN4.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 3)
                {
                    txtSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 2)
                {
                    txtSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 1)
                {
                    txtSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSTATIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSTATIC_AIRN1.Visibility = Visibility.Collapsed;
                txtSTATIC_AIRN2.Visibility = Visibility.Collapsed;
                txtSTATIC_AIRN3.Visibility = Visibility.Collapsed;
                txtSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                txtSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                txtSTATIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest1STATIC_AIR
        private void Retest1STATIC_AIR(decimal? n)
        {
            decimal? STATIC_AIR = n;

            txtRetest1STATIC_AIRSpecification.Text = string.Empty;
            txtRetest1STATIC_AIRN1.Text = string.Empty;
            txtRetest1STATIC_AIRN2.Text = string.Empty;
            txtRetest1STATIC_AIRN3.Text = string.Empty;
            txtRetest1STATIC_AIRN4.Text = string.Empty;
            txtRetest1STATIC_AIRN5.Text = string.Empty;
            txtRetest1STATIC_AIRN6.Text = string.Empty;
            txtRetest1STATIC_AIRAve.Text = string.Empty;

            if (STATIC_AIR != null && STATIC_AIR > 0)
            {

                if (STATIC_AIR == 6)
                {
                    txtRetest1STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (STATIC_AIR == 5)
                {
                    txtRetest1STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 4)
                {
                    txtRetest1STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 3)
                {
                    txtRetest1STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 2)
                {
                    txtRetest1STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 1)
                {
                    txtRetest1STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest1STATIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest1STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest1STATIC_AIRN1.Visibility = Visibility.Collapsed;
                txtRetest1STATIC_AIRN2.Visibility = Visibility.Collapsed;
                txtRetest1STATIC_AIRN3.Visibility = Visibility.Collapsed;
                txtRetest1STATIC_AIRN4.Visibility = Visibility.Collapsed;
                txtRetest1STATIC_AIRN5.Visibility = Visibility.Collapsed;
                txtRetest1STATIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Retest2STATIC_AIR
        private void Retest2STATIC_AIR(decimal? n)
        {
            decimal? STATIC_AIR = n;

            txtRetest2STATIC_AIRSpecification.Text = string.Empty;
            txtRetest2STATIC_AIRN1.Text = string.Empty;
            txtRetest2STATIC_AIRN2.Text = string.Empty;
            txtRetest2STATIC_AIRN3.Text = string.Empty;
            txtRetest2STATIC_AIRN4.Text = string.Empty;
            txtRetest2STATIC_AIRN5.Text = string.Empty;
            txtRetest2STATIC_AIRN6.Text = string.Empty;
            txtRetest2STATIC_AIRAve.Text = string.Empty;

            if (STATIC_AIR != null && STATIC_AIR > 0)
            {

                if (STATIC_AIR == 6)
                {
                    txtRetest2STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (STATIC_AIR == 5)
                {
                    txtRetest2STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN5.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 4)
                {
                    txtRetest2STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN4.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 3)
                {
                    txtRetest2STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN3.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 2)
                {
                    txtRetest2STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN2.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 1)
                {
                    txtRetest2STATIC_AIRN1.Visibility = Visibility.Visible;
                    txtRetest2STATIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtRetest2STATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtRetest2STATIC_AIRN1.Visibility = Visibility.Collapsed;
                txtRetest2STATIC_AIRN2.Visibility = Visibility.Collapsed;
                txtRetest2STATIC_AIRN3.Visibility = Visibility.Collapsed;
                txtRetest2STATIC_AIRN4.Visibility = Visibility.Collapsed;
                txtRetest2STATIC_AIRN5.Visibility = Visibility.Collapsed;
                txtRetest2STATIC_AIRN6.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region SaveLabSTATIC_AIR
        private void SaveLabSTATIC_AIR(decimal? n)
        {
            decimal? STATIC_AIR = n;

            txtSaveLabSTATIC_AIRSpecification.Text = string.Empty;
            txtSaveLabSTATIC_AIRN1.Text = string.Empty;
            txtSaveLabSTATIC_AIRN2.Text = string.Empty;
            txtSaveLabSTATIC_AIRN3.Text = string.Empty;
            txtSaveLabSTATIC_AIRN4.Text = string.Empty;
            txtSaveLabSTATIC_AIRN5.Text = string.Empty;
            txtSaveLabSTATIC_AIRN6.Text = string.Empty;
            txtSaveLabSTATIC_AIRAve.Text = string.Empty;

            if (STATIC_AIR != null && STATIC_AIR > 0)
            {

                if (STATIC_AIR == 6)
                {
                    txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Visible;
                }
                else if (STATIC_AIR == 5)
                {
                    txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 4)
                {
                    txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 3)
                {
                    txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 2)
                {
                    txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
                else if (STATIC_AIR == 1)
                {
                    txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Visible;
                    txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                    txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                txtSaveLabSTATIC_AIRN1.Visibility = Visibility.Collapsed;
                txtSaveLabSTATIC_AIRN2.Visibility = Visibility.Collapsed;
                txtSaveLabSTATIC_AIRN3.Visibility = Visibility.Collapsed;
                txtSaveLabSTATIC_AIRN4.Visibility = Visibility.Collapsed;
                txtSaveLabSTATIC_AIRN5.Visibility = Visibility.Collapsed;
                txtSaveLabSTATIC_AIRN6.Visibility = Visibility.Collapsed;
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
            txtSTATIC_AIRN1.IsReadOnly = true;
            txtSTATIC_AIRN2.IsReadOnly = true;
            txtSTATIC_AIRN3.IsReadOnly = true;
            txtSTATIC_AIRN4.IsReadOnly = true;
            txtSTATIC_AIRN5.IsReadOnly = true;
            txtSTATIC_AIRN6.IsReadOnly = true;

            txtRetest1STATIC_AIRN1.IsReadOnly = true;
            txtRetest1STATIC_AIRN2.IsReadOnly = true;
            txtRetest1STATIC_AIRN3.IsReadOnly = true;
            txtRetest1STATIC_AIRN4.IsReadOnly = true;
            txtRetest1STATIC_AIRN5.IsReadOnly = true;
            txtRetest1STATIC_AIRN6.IsReadOnly = true;

            txtRetest2STATIC_AIRN1.IsReadOnly = true;
            txtRetest2STATIC_AIRN2.IsReadOnly = true;
            txtRetest2STATIC_AIRN3.IsReadOnly = true;
            txtRetest2STATIC_AIRN4.IsReadOnly = true;
            txtRetest2STATIC_AIRN5.IsReadOnly = true;
            txtRetest2STATIC_AIRN6.IsReadOnly = true;

            //txtSaveLabSTATIC_AIRN1.IsReadOnly = true;
            //txtSaveLabSTATIC_AIRN2.IsReadOnly = true;
            //txtSaveLabSTATIC_AIRN3.IsReadOnly = true;
            //txtSaveLabSTATIC_AIRN4.IsReadOnly = true;
            //txtSaveLabSTATIC_AIRN5.IsReadOnly = true;
            //txtSaveLabSTATIC_AIRN6.IsReadOnly = true;
        }
        #endregion

        #endregion

        #region Public Methods

        #region STATIC_AIR

        decimal? saveLabSTATIC_AIR1 = null;
        decimal? saveLabSTATIC_AIR2 = null;
        decimal? saveLabSTATIC_AIR3 = null;
        decimal? saveLabSTATIC_AIR4 = null;
        decimal? saveLabSTATIC_AIR5 = null;
        decimal? saveLabSTATIC_AIR6 = null;

        public decimal? STATIC_AIR1
        {
            get { return saveLabSTATIC_AIR1; }
            set { saveLabSTATIC_AIR1 = value; }
        }

        public decimal? STATIC_AIR2
        {
            get { return saveLabSTATIC_AIR2; }
            set { saveLabSTATIC_AIR2 = value; }
        }

        public decimal? STATIC_AIR3
        {
            get { return saveLabSTATIC_AIR3; }
            set { saveLabSTATIC_AIR3 = value; }
        }

        public decimal? STATIC_AIR4
        {
            get { return saveLabSTATIC_AIR4; }
            set { saveLabSTATIC_AIR4 = value; }
        }

        public decimal? STATIC_AIR5
        {
            get { return saveLabSTATIC_AIR5; }
            set { saveLabSTATIC_AIR5 = value; }
        }

        public decimal? STATIC_AIR6
        {
            get { return saveLabSTATIC_AIR6; }
            set { saveLabSTATIC_AIR6 = value; }
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

