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
    /// Interaction logic for WarpingEditWindow.xaml
    /// </summary>
    public partial class WarpingEditWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public WarpingEditWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string WARPHEADNO = string.Empty;
        private string WARPERLOT = string.Empty;
        private string OperatorText = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            WARP_GETWARPERROLLDETAIL(WARPERLOT);
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWARPHEADNO.Text) && !string.IsNullOrEmpty(txtWARPERLOT.Text))
            {
                if (WARP_UPDATEWARPINGPROCESS() == true)
                {
                    "Edit Data Complete".ShowMessageBox();
                    this.DialogResult = true;
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

        private void txtSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMC_COUNT_L.Focus();
                txtMC_COUNT_L.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMC_COUNT_L_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMC_COUNT_S.Focus();
                txtMC_COUNT_S.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMC_COUNT_S_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH.Focus();
                txtLENGTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLENGTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_IT.Focus();
                txtTENSION_IT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_IT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_TAKEUP.Focus();
                txtTENSION_TAKEUP.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_TAKEUP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION.Focus();
                txtTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHARDNESS_L.Focus();
                txtHARDNESS_L.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHARDNESS_L_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHARDNESS_N.Focus();
                txtHARDNESS_N.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHARDNESS_N_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHARDNESS_R.Focus();
                txtHARDNESS_R.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHARDNESS_R_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region Private Properties

        #region ClearControl

        private void ClearControl()
        {
            txtWARPHEADNO.Text = string.Empty;
            txtWARPERLOT.Text = string.Empty;
            txtBEAMNO.Text = string.Empty;

            txtSPEED.Text = string.Empty;
            txtMC_COUNT_L.Text = string.Empty;
            txtMC_COUNT_S.Text = string.Empty;
            txtLENGTH.Text = string.Empty;
            txtTENSION_IT.Text = string.Empty;
            txtTENSION_TAKEUP.Text = string.Empty;
            txtTENSION.Text = string.Empty;

            txtHARDNESS_L.Text = string.Empty;
            txtHARDNESS_N.Text = string.Empty;
            txtHARDNESS_R.Text = string.Empty;

            txtOperator.Text = string.Empty;

            txtSPEED.SelectAll();
            txtSPEED.Focus();
        }

        #endregion

        private void WARP_GETWARPERROLLDETAIL(string P_WARPHEADNO)
        {
            try
            {
                List<WARP_GETWARPERROLLDETAIL> Result = new List<Models.WARP_GETWARPERROLLDETAIL>();

                Result = WarpingDataService.Instance.WARP_GETWARPERROLLDETAIL(P_WARPHEADNO);

                if (Result != null && Result.Count > 0)
                {
                    txtWARPHEADNO.Text = Result[0].WARPHEADNO;
                    txtWARPERLOT.Text = Result[0].WARPERLOT;
                    txtBEAMNO.Text = Result[0].BEAMNO;

                    if (Result[0].SPEED != null)
                        txtSPEED.Text = Result[0].SPEED.Value.ToString("#,##0.##");
                    if (Result[0].MC_COUNT_L != null)
                        txtMC_COUNT_L.Text = Result[0].MC_COUNT_L.Value.ToString("#,##0.##");
                    if (Result[0].MC_COUNT_S != null)
                        txtMC_COUNT_S.Text = Result[0].MC_COUNT_S.Value.ToString("#,##0.##");
                    if (Result[0].LENGTH != null)
                        txtLENGTH.Text = Result[0].LENGTH.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_IT != null)
                        txtTENSION_IT.Text = Result[0].TENSION_IT.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_TAKEUP != null)
                        txtTENSION_TAKEUP.Text = Result[0].TENSION_TAKEUP.Value.ToString("#,##0.##");
                    if (Result[0].TENSION != null)
                        txtTENSION.Text = Result[0].TENSION.Value.ToString("#,##0.##");
                    if (Result[0].HARDNESS_L != null)
                        txtHARDNESS_L.Text = Result[0].HARDNESS_L.Value.ToString("#,##0.##");
                    if (Result[0].HARDNESS_N != null)
                        txtHARDNESS_N.Text = Result[0].HARDNESS_N.Value.ToString("#,##0.##");
                    if (Result[0].HARDNESS_R != null)
                        txtHARDNESS_R.Text = Result[0].HARDNESS_R.Value.ToString("#,##0.##");

                    txtOperator.Text = OperatorText;
                }
                else
                {
                    txtWARPHEADNO.Text = WARPHEADNO;
                    txtWARPERLOT.Text = WARPERLOT;

                    txtBEAMNO.Text = string.Empty;
                    txtSPEED.Text = string.Empty;
                    txtMC_COUNT_L.Text = string.Empty;
                    txtMC_COUNT_S.Text = string.Empty;
                    txtLENGTH.Text = string.Empty;

                    txtTENSION_IT.Text = string.Empty;
                    txtTENSION_TAKEUP.Text = string.Empty;
                    txtTENSION.Text = string.Empty;

                    txtHARDNESS_L.Text = string.Empty;
                    txtHARDNESS_N.Text = string.Empty;
                    txtHARDNESS_R.Text = string.Empty;

                    txtOperator.Text = OperatorText;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private bool WARP_UPDATEWARPINGPROCESS()
        { 
            bool result = false;
            try
            {
                string P_WARPHEADNO = string.Empty;
                string P_WARPLOT = string.Empty;
                decimal? P_LENGTH = null;
                decimal? P_SPEED = null;
                decimal? P_HARDL = null;
                decimal? P_HARDN = null;
                decimal? P_HARDR = null;
                decimal? P_TENSION = null;
                decimal? P_TENSION_IT = null;
                decimal? P_TENSION_TAKE = null;
                decimal? P_MCL = null;
                decimal? P_MCS = null;
                string P_OPERATOR = string.Empty;
                string P_BEAMNO = string.Empty;

                if (!string.IsNullOrEmpty(txtWARPHEADNO.Text))
                    P_WARPHEADNO = txtWARPHEADNO.Text;

                if (!string.IsNullOrEmpty(txtWARPERLOT.Text))
                    P_WARPLOT = txtWARPERLOT.Text;

                #region P_LENGTH

                try
                {
                    if (!string.IsNullOrEmpty(txtLENGTH.Text))
                        P_LENGTH = decimal.Parse(txtLENGTH.Text);
                }
                catch
                {
                    P_LENGTH = null;
                }

                #endregion

                #region P_SPEED

                try
                {
                    if (!string.IsNullOrEmpty(txtSPEED.Text))
                        P_SPEED = decimal.Parse(txtSPEED.Text);
                }
                catch
                {
                    P_SPEED = null;
                }

                #endregion

                #region P_HARDL

                try
                {
                    if (!string.IsNullOrEmpty(txtHARDNESS_L.Text))
                        P_HARDL = decimal.Parse(txtHARDNESS_L.Text);
                }
                catch
                {
                    P_HARDL = null;
                }

                #endregion

                #region P_HARDN

                try
                {
                    if (!string.IsNullOrEmpty(txtHARDNESS_N.Text))
                        P_HARDN = decimal.Parse(txtHARDNESS_N.Text);
                }
                catch
                {
                    P_HARDN = null;
                }

                #endregion

                #region P_HARDR

                try
                {
                    if (!string.IsNullOrEmpty(txtHARDNESS_R.Text))
                        P_HARDR = decimal.Parse(txtHARDNESS_R.Text);
                }
                catch
                {
                    P_HARDR = null;
                }

                #endregion

                #region P_TENSION

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION.Text))
                        P_TENSION = decimal.Parse(txtTENSION.Text);
                }
                catch
                {
                    P_TENSION = null;
                }

                #endregion

                #region P_TENSION_IT

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_IT.Text))
                        P_TENSION_IT = decimal.Parse(txtTENSION_IT.Text);
                }
                catch
                {
                    P_TENSION_IT = null;
                }

                #endregion

                #region P_TENSION_TAKE

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_TAKEUP.Text))
                        P_TENSION_TAKE = decimal.Parse(txtTENSION_TAKEUP.Text);
                }
                catch
                {
                    P_TENSION_TAKE = null;
                }

                #endregion

                #region P_MCL

                try
                {
                    if (!string.IsNullOrEmpty(txtMC_COUNT_L.Text))
                        P_MCL = decimal.Parse(txtMC_COUNT_L.Text);
                }
                catch
                {
                    P_MCL = null;
                }

                #endregion

                #region P_MCS

                try
                {
                    if (!string.IsNullOrEmpty(txtMC_COUNT_S.Text))
                        P_MCS = decimal.Parse(txtMC_COUNT_S.Text);
                }
                catch
                {
                    P_MCS = null;
                }

                #endregion

                if (!string.IsNullOrEmpty(txtOperator.Text))
                    P_OPERATOR = txtOperator.Text;

                if (!string.IsNullOrEmpty(txtBEAMNO.Text))
                    P_BEAMNO = txtBEAMNO.Text;

                result = WarpingDataService.Instance.WARP_UPDATEWARPINGPROCESS(P_WARPHEADNO, P_WARPLOT
                    , P_LENGTH, P_SPEED, P_HARDL, P_HARDN, P_HARDR
                    , P_TENSION, P_TENSION_IT, P_TENSION_TAKE, P_MCL, P_MCS, P_OPERATOR, P_BEAMNO);

            }
            catch (Exception ex)
            {
                ex.Message.Err();
                ex.Message.ToString().ShowMessageBox(true);
                result = false;
            }

            return result;
        }

        #endregion

        #region Public Properties

        public void Setup(string P_WARPHEADNO, string P_WARPERLOT, string P_OPERATORID)
        {
            WARPHEADNO = P_WARPHEADNO;
            WARPERLOT = P_WARPERLOT;
            OperatorText = P_OPERATORID;
        }

        #endregion

    }
}
