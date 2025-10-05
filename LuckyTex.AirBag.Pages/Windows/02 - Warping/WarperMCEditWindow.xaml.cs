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
    /// Interaction logic for WarperMCEditWindow.xaml
    /// </summary>
    public partial class WarperMCEditWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public WarperMCEditWindow()
        {
            InitializeComponent();
            LoadWarperMC();
        }

        #endregion

        #region Internal Variables

        private string WARPHEADNO = string.Empty;
        private string ITM_PREPARE = string.Empty;
        private string WARPMC = string.Empty;
        private string SIDE = string.Empty;
        private string OperatorText = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (!string.IsNullOrEmpty(WARPHEADNO))
                txtWARPHEADNO.Text = WARPHEADNO;

            if (!string.IsNullOrEmpty(ITM_PREPARE))
                txtITM_PREPARE.Text = ITM_PREPARE;

            if (!string.IsNullOrEmpty(WARPMC))
                txtWARPMC.Text = WARPMC;

            if (!string.IsNullOrEmpty(SIDE))
                txtSIDE.Text = SIDE;

            if (!string.IsNullOrEmpty(OperatorText))
                txtOperator.Text = OperatorText;
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (chkChangrWarperMC.IsChecked == true)
            {
                string mc = string.Empty;

                if (cbWarperMC.SelectedValue != null)
                { 
                    string wmc = cbWarperMC.SelectedValue.ToString();

                    if (wmc == "Warper1")
                        mc = "1";
                    else if (wmc == "Warper2")
                        mc = "2";
                    else if (wmc == "Warper3")
                        mc = "3";
                    else if (wmc == "Warper4")
                        mc = "4";

                    if (WARPMC != mc)
                    {
                        if (Save(mc) == true)
                            this.DialogResult = true;
                    }
                    else
                    {
                        "Please Select MC Before Save".ShowMessageBox();
                    }
                }
            }
        }

        private void cmdCancelCreel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to Cancel this Creel Setup", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if(CancelCreel() == true)
                    this.DialogResult = true;
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region chkChangrWarperMC_Checked

        private void chkChangrWarperMC_Checked(object sender, RoutedEventArgs e)
        {
            cbWarperMC.IsEnabled = true;
        }

        #endregion

        #region chkChangrWarperMC_Unchecked

        private void chkChangrWarperMC_Unchecked(object sender, RoutedEventArgs e)
        {
            cbWarperMC.SelectedValue = null;
            cbWarperMC.SelectedIndex = -1;
            cbWarperMC.IsEnabled = false;
        }

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtWARPHEADNO.Text = string.Empty;
            txtITM_PREPARE.Text = string.Empty;
            txtWARPMC.Text = string.Empty;
            txtSIDE.Text = string.Empty;
            txtOperator.Text = string.Empty;

            cbWarperMC.SelectedValue = null;
            cbWarperMC.SelectedIndex = -1;
            cbWarperMC.IsEnabled = false;

            chkChangrWarperMC.IsChecked = false;

            cbWarperMC.Focus();
        }

        #endregion

        #region Private Properties

        #region LoadWarperMC

        private void LoadWarperMC()
        {
            if (cbWarperMC.ItemsSource == null)
            {
                string[] str = new string[] { "Warper1", "Warper2", "Warper3", "Warper4" };

                cbWarperMC.ItemsSource = str;
                cbWarperMC.SelectedIndex = 0;
            }
        }

        #endregion

        #region Save

        private bool Save(string mc)
        {
            try
            {
                string P_WARPHEADNO = string.Empty;
                string P_WARPMC = string.Empty;
                string P_SIDE = string.Empty;
                string P_NEWWARPMC = string.Empty;
                string P_OPERATOR = string.Empty;
                string RESULT = string.Empty;

                P_WARPHEADNO = txtWARPHEADNO.Text;
                P_WARPMC = txtWARPMC.Text;
                P_SIDE = txtSIDE.Text;
                P_NEWWARPMC = mc;
                P_OPERATOR = txtOperator.Text;

                RESULT = WarpingDataService.Instance.WARP_EDITWARPERMCSETUP(P_WARPHEADNO, P_WARPMC, P_SIDE, P_NEWWARPMC, P_OPERATOR);

                if (!string.IsNullOrEmpty(RESULT))
                {
                    RESULT.ShowMessageBox(true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }

        #endregion

        #region CancelCreel

        private bool CancelCreel()
        {
            try
            {
                string P_WARPHEADNO = string.Empty;
                string P_WARPMC = string.Empty;
                string P_SIDE = string.Empty;
                string P_OPERATOR = string.Empty;
                string RESULT = string.Empty;

                P_WARPHEADNO = txtWARPHEADNO.Text;
                P_WARPMC = txtWARPMC.Text;
                P_SIDE = txtSIDE.Text;
                P_OPERATOR = txtOperator.Text;

                RESULT = WarpingDataService.Instance.WARP_CANCELCREELSETUP(P_WARPHEADNO, P_WARPMC, P_SIDE, P_OPERATOR);

                if (!string.IsNullOrEmpty(RESULT))
                {
                    RESULT.ShowMessageBox(true);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }

        #endregion

        #endregion

        #region Public Properties

        public void Setup(string P_WARPHEADNO, string P_ITM_PREPARE, string P_WARPMC, string P_SIDE
            , string P_OPERATORID)
        {
            WARPHEADNO = P_WARPHEADNO;
            ITM_PREPARE = P_ITM_PREPARE;
            WARPMC = P_WARPMC;
            SIDE = P_SIDE;
            OperatorText = P_OPERATORID;
        }

        #endregion

    }
}
