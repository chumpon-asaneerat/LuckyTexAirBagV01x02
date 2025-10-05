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
    /// Interaction logic for BeamingEditWindow.xaml
    /// </summary>
    public partial class BeamingEditWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public BeamingEditWindow()
        {
            InitializeComponent();        
        }

        #endregion

        #region Internal Variables

        private string BEAMERNO = string.Empty;
        private string BEAMROLL = string.Empty;
        private string OperatorText = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            BEAM_GETBEAMROLLDETAIL(BEAMROLL);
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBEAMERNO.Text) && !string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                if (BEAM_UPDATEBEAMDETAIL() == true)
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
                txtLENGTH.Focus();
                txtLENGTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLENGTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBEAMSTANDTENSION.Focus();
                txtBEAMSTANDTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBEAMSTANDTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWINDINGTENSION.Focus();
                txtWINDINGTENSION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWINDINGTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtINSIDE_WIDTH.Focus();
                txtINSIDE_WIDTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtINSIDE_WIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtOUTSIDE_WIDTH.Focus();
                txtOUTSIDE_WIDTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtOUTSIDE_WIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFULL_WIDTH.Focus();
                txtFULL_WIDTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFULL_WIDTH_KeyDown(object sender, KeyEventArgs e)
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
                txtTENSION_ST1.Focus();
                txtTENSION_ST1.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST2.Focus();
                txtTENSION_ST2.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST3.Focus();
                txtTENSION_ST3.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST4.Focus();
                txtTENSION_ST4.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST5.Focus();
                txtTENSION_ST5.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST6.Focus();
                txtTENSION_ST6.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST7.Focus();
                txtTENSION_ST7.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST8.Focus();
                txtTENSION_ST8.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST9.Focus();
                txtTENSION_ST9.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST10.Focus();
                txtTENSION_ST10.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTENSION_ST10_KeyDown(object sender, KeyEventArgs e)
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
            txtBEAMERNO.Text = string.Empty;
            txtBEAMLOT.Text = string.Empty;
            txtBEAMNO.Text = string.Empty;

            txtSPEED.Text = string.Empty;
            txtLENGTH.Text = string.Empty;
            txtBEAMSTANDTENSION.Text = string.Empty;
            txtWINDINGTENSION.Text = string.Empty;

            txtINSIDE_WIDTH.Text = string.Empty;
            txtOUTSIDE_WIDTH.Text = string.Empty;
            txtFULL_WIDTH.Text = string.Empty;

            txtHARDNESS_L.Text = string.Empty;
            txtHARDNESS_N.Text = string.Empty;
            txtHARDNESS_R.Text = string.Empty;

            txtTENSION_ST1.Text = string.Empty;
            txtTENSION_ST2.Text = string.Empty;
            txtTENSION_ST3.Text = string.Empty;
            txtTENSION_ST4.Text = string.Empty;
            txtTENSION_ST5.Text = string.Empty;
            txtTENSION_ST6.Text = string.Empty;
            txtTENSION_ST7.Text = string.Empty;
            txtTENSION_ST8.Text = string.Empty;
            txtTENSION_ST9.Text = string.Empty;
            txtTENSION_ST10.Text = string.Empty;

            txtOperator.Text = string.Empty;

            txtSPEED.SelectAll();
            txtSPEED.Focus();
        }

        #endregion

        private void BEAM_GETBEAMROLLDETAIL(string P_BEAMROLL)
        {
            try
            {
                List<BEAM_GETBEAMROLLDETAIL> Result = new List<Models.BEAM_GETBEAMROLLDETAIL>();

                Result = BeamingDataService.Instance.BEAM_GETBEAMROLLDETAIL(P_BEAMROLL);

                if (Result != null && Result.Count > 0)
                {
                    txtBEAMERNO.Text = Result[0].BEAMERNO;
                    txtBEAMLOT.Text = Result[0].BEAMLOT;
                    txtBEAMNO.Text = Result[0].BEAMNO;

                    if (Result[0].SPEED != null)
                        txtSPEED.Text = Result[0].SPEED.Value.ToString("#,##0.##");
                    if (Result[0].LENGTH != null)
                        txtLENGTH.Text = Result[0].LENGTH.Value.ToString("#,##0.##");
                    if (Result[0].BEAMSTANDTENSION != null)
                        txtBEAMSTANDTENSION.Text = Result[0].BEAMSTANDTENSION.Value.ToString("#,##0.##");
                    if (Result[0].WINDINGTENSION != null)
                        txtWINDINGTENSION.Text = Result[0].WINDINGTENSION.Value.ToString("#,##0.##");
                    if (Result[0].INSIDE_WIDTH != null)
                        txtINSIDE_WIDTH.Text = Result[0].INSIDE_WIDTH.Value.ToString("#,##0.##");
                    if (Result[0].OUTSIDE_WIDTH != null)
                        txtOUTSIDE_WIDTH.Text = Result[0].OUTSIDE_WIDTH.Value.ToString("#,##0.##");
                    if (Result[0].FULL_WIDTH != null)
                        txtFULL_WIDTH.Text = Result[0].FULL_WIDTH.Value.ToString("#,##0.##");
                    if (Result[0].HARDNESS_L != null)
                        txtHARDNESS_L.Text = Result[0].HARDNESS_L.Value.ToString("#,##0.##");
                    if (Result[0].HARDNESS_N != null)
                        txtHARDNESS_N.Text = Result[0].HARDNESS_N.Value.ToString("#,##0.##");
                    if (Result[0].HARDNESS_R != null)
                        txtHARDNESS_R.Text = Result[0].HARDNESS_R.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST1 != null)
                        txtTENSION_ST1.Text = Result[0].TENSION_ST1.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST2 != null)
                        txtTENSION_ST2.Text = Result[0].TENSION_ST2.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST3 != null)
                        txtTENSION_ST3.Text = Result[0].TENSION_ST3.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST4 != null)
                        txtTENSION_ST4.Text = Result[0].TENSION_ST4.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST5 != null)
                        txtTENSION_ST5.Text = Result[0].TENSION_ST5.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST6 != null)
                        txtTENSION_ST6.Text = Result[0].TENSION_ST6.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST7 != null)
                        txtTENSION_ST7.Text = Result[0].TENSION_ST7.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST8 != null)
                        txtTENSION_ST8.Text = Result[0].TENSION_ST8.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST9 != null)
                        txtTENSION_ST9.Text = Result[0].TENSION_ST9.Value.ToString("#,##0.##");
                    if (Result[0].TENSION_ST10 != null)
                        txtTENSION_ST10.Text = Result[0].TENSION_ST10.Value.ToString("#,##0.##");

                    txtOperator.Text = OperatorText;
                }
                else
                {
                    txtBEAMERNO.Text = BEAMERNO;
                    txtBEAMLOT.Text = BEAMROLL;

                    txtBEAMNO.Text = string.Empty;
                    txtSPEED.Text = string.Empty;
                    txtLENGTH.Text = string.Empty;
                    txtBEAMSTANDTENSION.Text = string.Empty;
                    txtWINDINGTENSION.Text = string.Empty;

                    txtINSIDE_WIDTH.Text = string.Empty;
                    txtOUTSIDE_WIDTH.Text = string.Empty;
                    txtFULL_WIDTH.Text = string.Empty;

                    txtHARDNESS_L.Text = string.Empty;
                    txtHARDNESS_N.Text = string.Empty;
                    txtHARDNESS_R.Text = string.Empty;

                    txtTENSION_ST1.Text = string.Empty;
                    txtTENSION_ST2.Text = string.Empty;
                    txtTENSION_ST3.Text = string.Empty;
                    txtTENSION_ST4.Text = string.Empty;
                    txtTENSION_ST5.Text = string.Empty;
                    txtTENSION_ST6.Text = string.Empty;
                    txtTENSION_ST7.Text = string.Empty;
                    txtTENSION_ST8.Text = string.Empty;
                    txtTENSION_ST9.Text = string.Empty;
                    txtTENSION_ST10.Text = string.Empty;

                    txtOperator.Text = OperatorText;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private bool BEAM_UPDATEBEAMDETAIL()
        {
            bool result = false;
            try
            {
                string P_BEAMERNO = string.Empty;
                string P_BEAMLOT = string.Empty;
                decimal? P_LENGTH = null;
                decimal? P_SPEED = null;
                decimal? P_HARDL = null;
                decimal? P_HARDN = null;
                decimal? P_HARDR = null;
                decimal? P_STANDTENSION = null;
                decimal? P_WINDTENSION = null;
                decimal? P_INSIDE = null;
                decimal? P_OUTSIDE = null;
                decimal? P_FULL = null;
                decimal? P_TENSION_ST1 = null;
                decimal? P_TENSION_ST2 = null;
                decimal? P_TENSION_ST3 = null;
                decimal? P_TENSION_ST4 = null;
                decimal? P_TENSION_ST5 = null;
                decimal? P_TENSION_ST6 = null;
                decimal? P_TENSION_ST7 = null;
                decimal? P_TENSION_ST8 = null;
                decimal? P_TENSION_ST9 = null;
                decimal? P_TENSION_ST10 = null;

                if (!string.IsNullOrEmpty(txtBEAMERNO.Text))
                    P_BEAMERNO = txtBEAMERNO.Text;

                if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
                    P_BEAMLOT = txtBEAMLOT.Text;

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

                #region P_STANDTENSION

                try
                {
                    if (!string.IsNullOrEmpty(txtBEAMSTANDTENSION.Text))
                        P_STANDTENSION = decimal.Parse(txtBEAMSTANDTENSION.Text);
                }
                catch
                {
                    P_STANDTENSION = null;
                }

                #endregion

                #region P_WINDTENSION

                try
                {
                    if (!string.IsNullOrEmpty(txtWINDINGTENSION.Text))
                        P_WINDTENSION = decimal.Parse(txtWINDINGTENSION.Text);
                }
                catch
                {
                    P_WINDTENSION = null;
                }

                #endregion

                #region P_INSIDE

                try
                {
                    if (!string.IsNullOrEmpty(txtINSIDE_WIDTH.Text))
                        P_INSIDE = decimal.Parse(txtINSIDE_WIDTH.Text);
                }
                catch
                {
                    P_INSIDE = null;
                }

                #endregion

                #region P_OUTSIDE

                try
                {
                    if (!string.IsNullOrEmpty(txtOUTSIDE_WIDTH.Text))
                        P_OUTSIDE = decimal.Parse(txtOUTSIDE_WIDTH.Text);
                }
                catch
                {
                    P_OUTSIDE = null;
                }

                #endregion

                #region P_FULL

                try
                {
                    if (!string.IsNullOrEmpty(txtFULL_WIDTH.Text))
                        P_FULL = decimal.Parse(txtFULL_WIDTH.Text);
                }
                catch
                {
                    P_FULL = null;
                }

                #endregion

                #region P_TENSION_ST1

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST1.Text))
                        P_TENSION_ST1 = decimal.Parse(txtTENSION_ST1.Text);
                }
                catch
                {
                    P_TENSION_ST1 = null;
                }

                #endregion

                #region P_TENSION_ST2

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST2.Text))
                        P_TENSION_ST2 = decimal.Parse(txtTENSION_ST2.Text);
                }
                catch
                {
                    P_TENSION_ST2 = null;
                }

                #endregion

                #region P_TENSION_ST3

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST3.Text))
                        P_TENSION_ST3 = decimal.Parse(txtTENSION_ST3.Text);
                }
                catch
                {
                    P_TENSION_ST3 = null;
                }

                #endregion

                #region P_TENSION_ST4

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST4.Text))
                        P_TENSION_ST4 = decimal.Parse(txtTENSION_ST4.Text);
                }
                catch
                {
                    P_TENSION_ST4 = null;
                }

                #endregion

                #region P_TENSION_ST5

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST5.Text))
                        P_TENSION_ST5 = decimal.Parse(txtTENSION_ST5.Text);
                }
                catch
                {
                    P_TENSION_ST5 = null;
                }

                #endregion

                #region P_TENSION_ST6

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST6.Text))
                        P_TENSION_ST6 = decimal.Parse(txtTENSION_ST6.Text);
                }
                catch
                {
                    P_TENSION_ST6 = null;
                }

                #endregion

                #region P_TENSION_ST7

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST7.Text))
                        P_TENSION_ST7 = decimal.Parse(txtTENSION_ST7.Text);
                }
                catch
                {
                    P_TENSION_ST7 = null;
                }

                #endregion

                #region P_TENSION_ST8

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST8.Text))
                        P_TENSION_ST8 = decimal.Parse(txtTENSION_ST8.Text);
                }
                catch
                {
                    P_TENSION_ST8 = null;
                }

                #endregion

                #region P_TENSION_ST9

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST9.Text))
                        P_TENSION_ST9 = decimal.Parse(txtTENSION_ST9.Text);
                }
                catch
                {
                    P_TENSION_ST9 = null;
                }

                #endregion

                #region P_TENSION_ST10

                try
                {
                    if (!string.IsNullOrEmpty(txtTENSION_ST10.Text))
                        P_TENSION_ST10 = decimal.Parse(txtTENSION_ST10.Text);
                }
                catch
                {
                    P_TENSION_ST10 = null;
                }

                #endregion

                result = BeamingDataService.Instance.BEAM_UPDATEBEAMDETAIL(P_BEAMERNO, P_BEAMLOT
                    , P_LENGTH, P_SPEED, P_HARDL, P_HARDN, P_HARDR
                    , P_STANDTENSION, P_WINDTENSION, P_INSIDE, P_OUTSIDE, P_FULL
                    , P_TENSION_ST1, P_TENSION_ST2, P_TENSION_ST3, P_TENSION_ST4, P_TENSION_ST5
                    , P_TENSION_ST6, P_TENSION_ST7, P_TENSION_ST8, P_TENSION_ST9, P_TENSION_ST10);

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

        public void Setup(string P_BEAMERNO, string P_BEAMROLL, string P_OPERATORID)
        {
            BEAMERNO = P_BEAMERNO;
            BEAMROLL = P_BEAMROLL;
            OperatorText = P_OPERATORID;
        }

        #endregion

    }
}
