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
    /// Interaction logic for EditWeavingLotWindow.xaml
    /// </summary>
    public partial class EditWeavingLotWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public EditWeavingLotWindow()
        {
            InitializeComponent();
            LoadShift();
        }

        #endregion

        #region Internal Variables

        private string WEAVINGLOT = string.Empty;
        private string SHIFT = string.Empty;
        private decimal? DENSITY_WARP = null;
        private decimal? DENSITY_WEFT = null;
        private decimal? TENSION = null;
        private decimal? WASTE = null;
        private decimal? LENGTH = null;
        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (!string.IsNullOrEmpty(WEAVINGLOT))
                txtWEAVINGLOT.Text = WEAVINGLOT;

            if (!string.IsNullOrEmpty(SHIFT))
                cbShift.SelectedValue = SHIFT;

            if (DENSITY_WARP != null)
                txtDENSITY_WARP.Text = DENSITY_WARP.Value.ToString("#,##0.##");

            if (DENSITY_WEFT != null)
                txtDENSITY_WEFT.Text = DENSITY_WEFT.Value.ToString("#,##0.##");

            if (TENSION != null)
                txtTENSION.Text = TENSION.Value.ToString("#,##0.##");

            if (WASTE != null)
                txtWASTE.Text = WASTE.Value.ToString("#,##0.##");
        }

        #endregion

        #region Button Events

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowDeleteRemarkBox();
            if (logInfo != null)
            {
                int processId = 5; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    return;
                }
                else
                {
                    if (WeavingDataService.Instance.WEAV_DELETEWEAVINGLOT(WEAVINGLOT, logInfo.Remark, logInfo.UserName) == true)
                    {
                        this.DialogResult = true;
                    }
                    else
                    { 
                    
                    }
                }
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (Save() == true)
                this.DialogResult = true;
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

        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtWEAVINGLOT.Text = string.Empty;
            txtDENSITY_WARP.Text = string.Empty;
            txtDENSITY_WEFT.Text = string.Empty;
            txtTENSION.Text = string.Empty;
            txtWASTE.Text = string.Empty;

            cbShift.SelectedValue = null;
            cbShift.Focus();
        }

        #endregion

        #region Private Properties

        #region LoadShift

        private void LoadShift()
        {
            if (cbShift.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C", "D" };

                cbShift.ItemsSource = str;
                cbShift.SelectedIndex = 0;
            }
        }

        #endregion

        #region Save

        private bool Save()
        {
            try
            {
                string strSHIFT = string.Empty;
                decimal? p_DENSITY_WARP = null;
                decimal? p_DENSITY_WEFT = null;
                decimal? p_TENSION = null;
                decimal? p_WASTE = null;

                if (!string.IsNullOrEmpty(txtDENSITY_WARP.Text))
                {
                    p_DENSITY_WARP = decimal.Parse(txtDENSITY_WARP.Text);
                }

                if (!string.IsNullOrEmpty(txtDENSITY_WEFT.Text))
                {
                    p_DENSITY_WEFT = decimal.Parse(txtDENSITY_WEFT.Text);
                }

                if (!string.IsNullOrEmpty(txtTENSION.Text))
                {
                    p_TENSION = decimal.Parse(txtTENSION.Text);
                }

                if (!string.IsNullOrEmpty(txtWASTE.Text))
                {
                    p_WASTE = decimal.Parse(txtWASTE.Text);
                }

                strSHIFT = cbShift.SelectedValue.ToString();

                if (WeavingDataService.Instance.WEAV_UPDATEWEAVINGLOT(WEAVINGLOT, LENGTH, strSHIFT, p_DENSITY_WARP, p_DENSITY_WEFT, p_TENSION, p_WASTE) == true)
                {
                    return true;
                }
                else
                {
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

        #endregion

        #region Public Properties

        public void Setup(string P_WEAVINGLOT, string P_SHIFT, decimal? P_DENSITY_WARP, decimal? P_DENSITY_WEFT, decimal? P_TENSION, decimal? P_WASTE, decimal? P_LENGTH)
        {
            WEAVINGLOT = P_WEAVINGLOT;
            SHIFT = P_SHIFT;
            DENSITY_WARP = P_DENSITY_WARP;
            DENSITY_WEFT = P_DENSITY_WEFT;
            TENSION = P_TENSION;
            WASTE = P_WASTE;
            LENGTH = P_LENGTH;
        }

        #endregion

    }
}
