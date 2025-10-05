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

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

using System.Reflection;
using iTextSharp.text;

using System.Globalization;
using System.Collections;

using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using System.Runtime.InteropServices;

using DataControl.ClassData;
using System.Threading;

using System.Configuration;
using System.Data;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ItemCodeSpecificationPage.xaml
    /// </summary>
    public partial class ItemCodeSpecificationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemCodeSpecificationPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemCode();
            LoadAllowance();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string positionLevel = string.Empty;

        decimal value;

        LAB_INSERTUPDATEITEMSPEC _session = new LAB_INSERTUPDATEITEMSPEC();

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;


            if (positionLevel != "")
            {
                if (positionLevel != "5")
                {
                    cmdEdit.Visibility = System.Windows.Visibility.Visible;
                    cmdSave.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    cmdEdit.Visibility = System.Windows.Visibility.Collapsed;
                    cmdSave.Visibility = System.Windows.Visibility.Collapsed;
                }
            }


            txtUSEWIDTH_TOR.IsEnabled = false;
            txtWIDTHSILICONE_TOR.IsEnabled = false;
            txtMAXFORCE_W_TOR.IsEnabled = false;
            txtMAXFORCE_F_TOR.IsEnabled = false;

            txtELOGATION_W_TOR.IsEnabled = false;
            txtELOGATION_F_TOR.IsEnabled = false;

            txtFLAMMABILITY_F_TOR.IsEnabled = false;
            txtFLAMMABILITY_W_TOR.IsEnabled = false;
            txtEDGECOMB_W_TOR.IsEnabled = false;
            txtEDGECOMB_F_TOR.IsEnabled = false;
            txtSTIFFNESS_F_TOR.IsEnabled = false;
            txtSTIFFNESS_W_TOR.IsEnabled = false;
            txtTEAR_W_TOR.IsEnabled = false;
            txtTEAR_F_TOR.IsEnabled = false;
            txtSTATIC_AIR_TOR.IsEnabled = false;
            txtDIMENSCHANGE_F_TOR.IsEnabled = false;
            txtDIMENSCHANGE_W_TOR.IsEnabled = false;
            txtFLEXABRASION_F_TOR.IsEnabled = false;
            txtFLEXABRASION_W_TOR.IsEnabled = false;
            txtBOW_TOR.IsEnabled = false;
            txtSKEW_TOR.IsEnabled = false;

            txtBENDING_F_TOR.IsEnabled = false;
            txtBENDING_W_TOR.IsEnabled = false;
            txtFLEX_SCOTT_F_TOR.IsEnabled = false;
            txtFLEX_SCOTT_W_TOR.IsEnabled = false;

            cbItemCode.Text = "";
            cbItemCode.SelectedItem = null;
            cbItemCode.Focus();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        #region cmdEdit_Click

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            chk_TextEnabled(true);

            #region Combo

            #region cbUSEWIDTH_TOR
            if (cbUSEWIDTH_TOR.SelectedValue != null)
            {
                if (cbUSEWIDTH_TOR.SelectedIndex == 0)
                    txtUSEWIDTH_TOR.IsEnabled = true;
                else
                    txtUSEWIDTH_TOR.IsEnabled = false;
            }
            #endregion

            #region cbWIDTHSILICONE_TOR
            if (cbWIDTHSILICONE_TOR.SelectedValue != null)
            {
                if (cbWIDTHSILICONE_TOR.SelectedIndex == 0)
                    txtWIDTHSILICONE_TOR.IsEnabled = true;
                else
                    txtWIDTHSILICONE_TOR.IsEnabled = false;
            }
            #endregion

            #region cbMAXFORCE_W_TOR
            if (cbMAXFORCE_W_TOR.SelectedValue != null)
            {
                if (cbMAXFORCE_W_TOR.SelectedIndex == 0)
                    txtMAXFORCE_W_TOR.IsEnabled = true;
                else
                    txtMAXFORCE_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbMAXFORCE_F_TOR
            if (cbMAXFORCE_F_TOR.SelectedValue != null)
            {
                if (cbMAXFORCE_F_TOR.SelectedIndex == 0)
                    txtMAXFORCE_F_TOR.IsEnabled = true;
                else
                    txtMAXFORCE_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbELOGATION_W_TOR
            if (cbELOGATION_W_TOR.SelectedValue != null)
            {
                if (cbELOGATION_W_TOR.SelectedIndex == 0)
                    txtELOGATION_W_TOR.IsEnabled = true;
                else
                    txtELOGATION_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbELOGATION_F_TOR
            if (cbELOGATION_F_TOR.SelectedValue != null)
            {
                if (cbELOGATION_F_TOR.SelectedIndex == 0)
                    txtELOGATION_F_TOR.IsEnabled = true;
                else
                    txtELOGATION_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbFLAMMABILITY_W_TOR
            if (cbFLAMMABILITY_W_TOR.SelectedValue != null)
            {
                if (cbFLAMMABILITY_W_TOR.SelectedIndex == 0)
                    txtFLAMMABILITY_W_TOR.IsEnabled = true;
                else
                    txtFLAMMABILITY_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbFLAMMABILITY_F_TOR
            if (cbFLAMMABILITY_F_TOR.SelectedValue != null)
            {
                if (cbFLAMMABILITY_F_TOR.SelectedIndex == 0)
                    txtFLAMMABILITY_F_TOR.IsEnabled = true;
                else
                    txtFLAMMABILITY_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbEDGECOMB_W_TOR
            if (cbEDGECOMB_W_TOR.SelectedValue != null)
            {
                if (cbEDGECOMB_W_TOR.SelectedIndex == 0)
                    txtEDGECOMB_W_TOR.IsEnabled = true;
                else
                    txtEDGECOMB_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbEDGECOMB_F_TOR
            if (cbEDGECOMB_F_TOR.SelectedValue != null)
            {
                if (cbEDGECOMB_F_TOR.SelectedIndex == 0)
                    txtEDGECOMB_F_TOR.IsEnabled = true;
                else
                    txtEDGECOMB_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbSTIFFNESS_W_TOR
            if (cbSTIFFNESS_W_TOR.SelectedValue != null)
            {
                if (cbSTIFFNESS_W_TOR.SelectedIndex == 0)
                    txtSTIFFNESS_W_TOR.IsEnabled = true;
                else
                    txtSTIFFNESS_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbSTIFFNESS_F_TOR
            if (cbSTIFFNESS_F_TOR.SelectedValue != null)
            {
                if (cbSTIFFNESS_F_TOR.SelectedIndex == 0)
                    txtSTIFFNESS_F_TOR.IsEnabled = true;
                else
                    txtSTIFFNESS_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbTEAR_W_TOR
            if (cbTEAR_W_TOR.SelectedValue != null)
            {
                if (cbTEAR_W_TOR.SelectedIndex == 0)
                    txtTEAR_W_TOR.IsEnabled = true;
                else
                    txtTEAR_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbTEAR_F_TOR
            if (cbTEAR_F_TOR.SelectedValue != null)
            {
                if (cbTEAR_F_TOR.SelectedIndex == 0)
                    txtTEAR_F_TOR.IsEnabled = true;
                else
                    txtTEAR_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbSTATIC_AIR_TOR
            if (cbSTATIC_AIR_TOR.SelectedValue != null)
            {
                if (cbSTATIC_AIR_TOR.SelectedIndex == 0)
                    txtSTATIC_AIR_TOR.IsEnabled = true;
                else
                    txtSTATIC_AIR_TOR.IsEnabled = false;
            }
            #endregion

            #region cbDIMENSCHANGE_W_TOR
            if (cbDIMENSCHANGE_W_TOR.SelectedValue != null)
            {
                if (cbDIMENSCHANGE_W_TOR.SelectedIndex == 0)
                    txtDIMENSCHANGE_W_TOR.IsEnabled = true;
                else
                    txtDIMENSCHANGE_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbDIMENSCHANGE_F_TOR
            if (cbDIMENSCHANGE_F_TOR.SelectedValue != null)
            {
                if (cbDIMENSCHANGE_F_TOR.SelectedIndex == 0)
                    txtDIMENSCHANGE_F_TOR.IsEnabled = true;
                else
                    txtDIMENSCHANGE_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbFLEXABRASION_W_TOR
            if (cbFLEXABRASION_W_TOR.SelectedValue != null)
            {
                if (cbFLEXABRASION_W_TOR.SelectedIndex == 0)
                    txtFLEXABRASION_W_TOR.IsEnabled = true;
                else
                    txtFLEXABRASION_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbFLEXABRASION_F_TOR
            if (cbFLEXABRASION_F_TOR.SelectedValue != null)
            {
                if (cbFLEXABRASION_F_TOR.SelectedIndex == 0)
                    txtFLEXABRASION_F_TOR.IsEnabled = true;
                else
                    txtFLEXABRASION_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbBOW_TOR
            if (cbBOW_TOR.SelectedValue != null)
            {
                if (cbBOW_TOR.SelectedIndex == 0)
                    txtBOW_TOR.IsEnabled = true;
                else
                    txtBOW_TOR.IsEnabled = false;
            }
            #endregion

            #region cbSKEW_TOR
            if (cbSKEW_TOR.SelectedValue != null)
            {
                if (cbSKEW_TOR.SelectedIndex == 0)
                    txtSKEW_TOR.IsEnabled = true;
                else
                    txtSKEW_TOR.IsEnabled = false;
            }
            #endregion

            //Update 07/07/18

            #region cbBENDING_W_TOR
            if (cbBENDING_W_TOR.SelectedValue != null)
            {
                if (cbBENDING_W_TOR.SelectedIndex == 0)
                    txtBENDING_W_TOR.IsEnabled = true;
                else
                    txtBENDING_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbBENDINGN_F_TOR
            if (cbBENDING_F_TOR.SelectedValue != null)
            {
                if (cbBENDING_F_TOR.SelectedIndex == 0)
                    txtBENDING_F_TOR.IsEnabled = true;
                else
                    txtBENDING_F_TOR.IsEnabled = false;
            }
            #endregion

            #region cbFLEX_SCOTT_W_TOR
            if (cbFLEX_SCOTT_W_TOR.SelectedValue != null)
            {
                if (cbFLEX_SCOTT_W_TOR.SelectedIndex == 0)
                    txtFLEX_SCOTT_W_TOR.IsEnabled = true;
                else
                    txtFLEX_SCOTT_W_TOR.IsEnabled = false;
            }
            #endregion

            #region cbFLEX_SCOTT_F_TOR
            if (cbFLEX_SCOTT_F_TOR.SelectedValue != null)
            {
                if (cbFLEX_SCOTT_F_TOR.SelectedIndex == 0)
                    txtFLEX_SCOTT_F_TOR.IsEnabled = true;
                else
                    txtFLEX_SCOTT_F_TOR.IsEnabled = false;
            }
            #endregion

            #endregion

            buttonEnabled(true);
        }

        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);
            string itemCode = string.Empty;

            if (cbItemCode.SelectedValue != null)
                itemCode = cbItemCode.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(itemCode))
            {
                if (chkMax() == true)
                {
                    if (chkSpecNoN() == true)
                    {
                        if (Save() == true)
                        {
                            ClearControl();

                            cbItemCode.Text = "";
                            cbItemCode.SelectedItem = null;
                            cbItemCode.Focus();
                        }
                    }
                    else
                    {
                        "มีการระบุค่า Spec ช่อง No N ต้อง ไม่เป็น 0".ShowMessageBox();
                    }
                }
                else
                {
                    "No N > Max".ShowMessageBox();
                }
            }

            buttonEnabled(true);
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

        #region KeyDown
        private void txtWIDTHSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH_NO.Focus();
                txtWIDTH_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTH_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUSEWIDTHSpecification.Focus();
                txtUSEWIDTHSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUSEWIDTHSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtUSEWIDTH_TOR.IsEnabled == true)
                {
                    txtUSEWIDTH_TOR.Focus();
                    txtUSEWIDTH_TOR.SelectAll();
                }
                else
                {
                    txtUSEWIDTH_NO.Focus();
                    txtUSEWIDTH_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtUSEWIDTH_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUSEWIDTH_NO.Focus();
                txtUSEWIDTH_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUSEWIDTH_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHSILICONESpecification.Focus();
                txtWIDTHSILICONESpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHSILICONESpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtWIDTHSILICONE_TOR.IsEnabled == true)
                {
                    txtWIDTHSILICONE_TOR.Focus();
                    txtWIDTHSILICONE_TOR.SelectAll();
                }
                else
                {
                    txtWIDTHSILICONE_NO.Focus();
                    txtWIDTHSILICONE_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtWIDTHSILICONE_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTHSILICONE_NO.Focus();
                txtWIDTHSILICONE_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWIDTHSILICONE_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNUMTHREADS_WSpecification.Focus();
                txtNUMTHREADS_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNUMTHREADS_W_TOR.Focus();
                txtNUMTHREADS_W_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNUMTHREADS_W_NO.Focus();
                txtNUMTHREADS_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNUMTHREADS_FSpecification.Focus();
                txtNUMTHREADS_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNUMTHREADS_F_TOR.Focus();
                txtNUMTHREADS_F_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNUMTHREADS_F_NO.Focus();
                txtNUMTHREADS_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTOTALWEIGHTSpecification.Focus();
                txtTOTALWEIGHTSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHTSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTOTALWEIGHT_TOR.Focus();
                txtTOTALWEIGHT_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHT_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTOTALWEIGHT_NO.Focus();
                txtTOTALWEIGHT_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTOTALWEIGHT_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUNCOATEDWEIGHTSpecification.Focus();
                txtUNCOATEDWEIGHTSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHTSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUNCOATEDWEIGHT_TOR.Focus();
                txtUNCOATEDWEIGHT_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHT_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUNCOATEDWEIGHT_NO.Focus();
                txtUNCOATEDWEIGHT_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUNCOATEDWEIGHT_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCOATINGWEIGHTSpecification.Focus();
                txtCOATINGWEIGHTSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOATINGWEIGHTSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCOATWEIGHT_TOR.Focus();
                txtCOATWEIGHT_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOATWEIGHT_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCOATWEIGHT_NO.Focus();
                txtCOATWEIGHT_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOATWEIGHT_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTHICKNESSSpecification.Focus();
                txtTHICKNESSSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTHICKNESSSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTHICKNESS_TOR.Focus();
                txtTHICKNESS_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTHICKNESS_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTHICKNESS_NO.Focus();
                txtTHICKNESS_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTHICKNESS_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXFORCE_WSpecification.Focus();
                txtMAXFORCE_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtMAXFORCE_W_TOR.IsEnabled == true)
                {
                    txtMAXFORCE_W_TOR.Focus();
                    txtMAXFORCE_W_TOR.SelectAll();
                }
                else
                {
                    txtMAXFORCE_W_NO.Focus();
                    txtMAXFORCE_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXFORCE_W_NO.Focus();
                txtMAXFORCE_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXFORCE_FSpecification.Focus();
                txtMAXFORCE_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtMAXFORCE_F_TOR.IsEnabled == true)
                {
                    txtMAXFORCE_F_TOR.Focus();
                    txtMAXFORCE_F_TOR.SelectAll();
                }
                else
                {
                    txtMAXFORCE_F_NO.Focus();
                    txtMAXFORCE_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMAXFORCE_F_NO.Focus();
                txtMAXFORCE_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtELOGATION_WSpecification.Focus();
                txtELOGATION_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtELOGATION_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtELOGATION_W_TOR.IsEnabled == true)
                {
                    txtELOGATION_W_TOR.Focus();
                    txtELOGATION_W_TOR.SelectAll();
                }
                else
                {
                    txtELOGATION_W_NO.Focus();
                    txtELOGATION_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtELOGATION_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtELOGATION_W_NO.Focus();
                txtELOGATION_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtELOGATION_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtELOGATION_FSpecification.Focus();
                txtELOGATION_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtELOGATION_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtELOGATION_F_TOR.IsEnabled == true)
                {
                    txtELOGATION_F_TOR.Focus();
                    txtELOGATION_F_TOR.SelectAll();
                }
                else
                {
                    txtELOGATION_F_NO.Focus();
                    txtELOGATION_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtELOGATION_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtELOGATION_F_NO.Focus();
                txtELOGATION_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtELOGATION_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLAMMABILITY_WSpecification.Focus();
                txtFLAMMABILITY_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtFLAMMABILITY_W_TOR.IsEnabled == true)
                {
                    txtFLAMMABILITY_W_TOR.Focus();
                    txtFLAMMABILITY_W_TOR.SelectAll();
                }
                else
                {
                    txtFLAMMABILITY_W_NO.Focus();
                    txtFLAMMABILITY_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLAMMABILITY_W_NO.Focus();
                txtFLAMMABILITY_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLAMMABILITY_FSpecification.Focus();
                txtFLAMMABILITY_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtFLAMMABILITY_F_TOR.IsEnabled == true)
                {
                    txtFLAMMABILITY_F_TOR.Focus();
                    txtFLAMMABILITY_F_TOR.SelectAll();
                }
                else
                {
                    txtFLAMMABILITY_F_NO.Focus();
                    txtFLAMMABILITY_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLAMMABILITY_F_NO.Focus();
                txtFLAMMABILITY_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEDGECOMB_WSpecification.Focus();
                txtEDGECOMB_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEDGECOMB_W_TOR.IsEnabled == true)
                {
                    txtEDGECOMB_W_TOR.Focus();
                    txtEDGECOMB_W_TOR.SelectAll();
                }
                else
                {
                    txtEDGECOMB_W_NO.Focus();
                    txtEDGECOMB_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEDGECOMB_W_NO.Focus();
                txtEDGECOMB_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEDGECOMB_FSpecification.Focus();
                txtEDGECOMB_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtEDGECOMB_F_TOR.IsEnabled == true)
                {
                    txtEDGECOMB_F_TOR.Focus();
                    txtEDGECOMB_F_TOR.SelectAll();
                }
                else
                {
                    txtEDGECOMB_F_NO.Focus();
                    txtEDGECOMB_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEDGECOMB_F_NO.Focus();
                txtEDGECOMB_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTIFFNES_WSpecification.Focus();
                txtSTIFFNES_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNESS_W_TOR.IsEnabled == true)
                {
                    txtSTIFFNESS_W_TOR.Focus();
                    txtSTIFFNESS_W_TOR.SelectAll();
                }
                else
                {
                    txtSTIFFNESS_W_NO.Focus();
                    txtSTIFFNESS_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNESS_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTIFFNESS_W_NO.Focus();
                txtSTIFFNESS_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTIFFNESS_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTIFFNES_FSpecification.Focus();
                txtSTIFFNES_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTIFFNES_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTIFFNESS_F_TOR.IsEnabled == true)
                {
                    txtSTIFFNESS_F_TOR.Focus();
                    txtSTIFFNESS_F_TOR.SelectAll();
                }
                else
                {
                    txtSTIFFNESS_F_NO.Focus();
                    txtSTIFFNESS_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTIFFNESS_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTIFFNESS_F_NO.Focus();
                txtSTIFFNESS_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTIFFNESS_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEAR_WSpecification.Focus();
                txtTEAR_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEAR_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTEAR_W_TOR.IsEnabled == true)
                {
                    txtTEAR_W_TOR.Focus();
                    txtTEAR_W_TOR.SelectAll();
                }
                else
                {
                    txtTEAR_W_NO.Focus();
                    txtTEAR_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtTEAR_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEAR_W_NO.Focus();
                txtTEAR_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEAR_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEAR_FSpecification.Focus();
                txtTEAR_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEAR_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTEAR_F_TOR.IsEnabled == true)
                {
                    txtTEAR_F_TOR.Focus();
                    txtTEAR_F_TOR.SelectAll();
                }
                else
                {
                    txtTEAR_F_NO.Focus();
                    txtTEAR_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtTEAR_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTEAR_F_NO.Focus();
                txtTEAR_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEAR_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTATIC_AIRSpecification.Focus();
                txtSTATIC_AIRSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIRSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSTATIC_AIR_TOR.IsEnabled == true)
                {
                    txtSTATIC_AIR_TOR.Focus();
                    txtSTATIC_AIR_TOR.SelectAll();
                }
                else
                {
                    txtSTATIC_AIR_NO.Focus();
                    txtSTATIC_AIR_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIR_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTATIC_AIR_NO.Focus();
                txtSTATIC_AIR_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIR_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDYNAMIC_AIRSpecification.Focus();
                txtDYNAMIC_AIRSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIRSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDYNAMIC_AIR_TOR.Focus();
                txtDYNAMIC_AIR_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIR_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDYNAMIC_AIR_NO.Focus();
                txtDYNAMIC_AIR_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIR_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXPONENTSpecification.Focus();
                txtEXPONENTSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXPONENTSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXPONENT_TOR.Focus();
                txtEXPONENT_TOR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXPONENT_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEXPONENT_NO.Focus();
                txtEXPONENT_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXPONENT_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDIMENSCHANGE_WSpecification.Focus();
                txtDIMENSCHANGE_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDIMENSCHANGE_W_TOR.IsEnabled == true)
                {
                    txtDIMENSCHANGE_W_TOR.Focus();
                    txtDIMENSCHANGE_W_TOR.SelectAll();
                }
                else
                {
                    txtDIMENSCHANGE_W_NO.Focus();
                    txtDIMENSCHANGE_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDIMENSCHANGE_W_NO.Focus();
                txtDIMENSCHANGE_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDIMENSCHANGE_FSpecification.Focus();
                txtDIMENSCHANGE_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDIMENSCHANGE_F_TOR.IsEnabled == true)
                {
                    txtDIMENSCHANGE_F_TOR.Focus();
                    txtDIMENSCHANGE_F_TOR.SelectAll();
                }
                else
                {
                    txtDIMENSCHANGE_F_NO.Focus();
                    txtDIMENSCHANGE_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDIMENSCHANGE_F_NO.Focus();
                txtDIMENSCHANGE_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEXABRASION_WSpecification.Focus();
                txtFLEXABRASION_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtFLEXABRASION_W_TOR.IsEnabled == true)
                {
                    txtFLEXABRASION_W_TOR.Focus();
                    txtFLEXABRASION_W_TOR.SelectAll();
                }
                else
                {
                    txtFLEXABRASION_W_NO.Focus();
                    txtFLEXABRASION_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEXABRASION_W_NO.Focus();
                txtFLEXABRASION_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEXABRASION_FSpecification.Focus();
                txtFLEXABRASION_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtFLEXABRASION_F_TOR.IsEnabled == true)
                {
                    txtFLEXABRASION_F_TOR.Focus();
                    txtFLEXABRASION_F_TOR.SelectAll();
                }
                else
                {
                    txtFLEXABRASION_F_NO.Focus();
                    txtFLEXABRASION_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEXABRASION_F_NO.Focus();
                txtFLEXABRASION_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBOWSpecification.Focus();
                txtBOWSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBOWSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBOW_TOR.IsEnabled == true)
                {
                    txtBOW_TOR.Focus();
                    txtBOW_TOR.SelectAll();
                }
                else
                {
                    txtBOW_NO.Focus();
                    txtBOW_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtBOW_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBOW_NO.Focus();
                txtBOW_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBOW_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSKEWSpecification.Focus();
                txtSKEWSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSKEWSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtSKEW_TOR.IsEnabled == true)
                {
                    txtSKEW_TOR.Focus();
                    txtSKEW_TOR.SelectAll();
                }
                else
                {
                    txtSKEW_NO.Focus();
                    txtSKEW_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtSKEW_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSKEW_NO.Focus();
                txtSKEW_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSKEW_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBENDING_WSpecification.Focus();
                txtBENDING_WSpecification.SelectAll();
                //cmdSave.Focus();
                e.Handled = true;
            }
        }


        // Update 07/07/18
        private void txtBENDING_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBENDING_W_TOR.IsEnabled == true)
                {
                    txtBENDING_W_TOR.Focus();
                    txtBENDING_W_TOR.SelectAll();
                }
                else
                {
                    txtBENDING_W_NO.Focus();
                    txtBENDING_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtBENDING_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBENDING_W_NO.Focus();
                txtBENDING_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBENDING_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBENDING_FSpecification.Focus();
                txtBENDING_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBENDING_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBENDING_F_TOR.IsEnabled == true)
                {
                    txtBENDING_F_TOR.Focus();
                    txtBENDING_F_TOR.SelectAll();
                }
                else
                {
                    txtBENDING_F_NO.Focus();
                    txtBENDING_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtBENDING_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtBENDING_F_NO.Focus();
                txtBENDING_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBENDING_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEX_SCOTT_WSpecification.Focus();
                txtFLEX_SCOTT_WSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_WSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtFLEX_SCOTT_W_TOR.IsEnabled == true)
                {
                    txtFLEX_SCOTT_W_TOR.Focus();
                    txtFLEX_SCOTT_W_TOR.SelectAll();
                }
                else
                {
                    txtFLEX_SCOTT_W_NO.Focus();
                    txtFLEX_SCOTT_W_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_W_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEX_SCOTT_W_NO.Focus();
                txtFLEX_SCOTT_W_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_W_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEX_SCOTT_FSpecification.Focus();
                txtFLEX_SCOTT_FSpecification.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_FSpecification_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtFLEX_SCOTT_F_TOR.IsEnabled == true)
                {
                    txtFLEX_SCOTT_F_TOR.Focus();
                    txtFLEX_SCOTT_F_TOR.SelectAll();
                }
                else
                {
                    txtFLEX_SCOTT_F_NO.Focus();
                    txtFLEX_SCOTT_F_NO.SelectAll();
                }
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_F_TOR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFLEX_SCOTT_F_NO.Focus();
                txtFLEX_SCOTT_F_NO.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_F_NO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region LostFocus

        private void txtWIDTHSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtWIDTHSpecification.Text))
                {
                    if (Decimal.TryParse(txtWIDTHSpecification.Text, out value))
                    {
                        _session.P_WIDTH = decimal.Parse(txtWIDTHSpecification.Text);
                    }
                    else
                    {
                        txtWIDTHSpecification.Text = string.Empty;
                        _session.P_WIDTH = null;
                    }
                }
                else
                    _session.P_WIDTH = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtWIDTH_TOR_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtWIDTH_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtWIDTH_NO.Text))
                {
                    if (Decimal.TryParse(txtWIDTH_NO.Text, out value))
                    {
                        _session.P_WIDTH_NO = decimal.Parse(txtWIDTH_NO.Text);
                    }
                    else
                    {
                        txtWIDTH_NO.Text = string.Empty;
                        _session.P_WIDTH_NO = null;
                    }
                }
                else
                    _session.P_WIDTH_NO = null;

                #region Foreground

                if (_session.P_WIDTH_NO != null)
                {
                    if (_session.P_WIDTH_NO > 1)
                        txtWIDTH_NO.Foreground = Brushes.Red;
                    else
                        txtWIDTH_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtUSEWIDTHSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUSEWIDTHSpecification.Text))
                {
                    if (Decimal.TryParse(txtUSEWIDTHSpecification.Text, out value))
                    {
                        _session.P_USEWIDTH = decimal.Parse(txtUSEWIDTHSpecification.Text);
                    }
                    else
                    {
                        txtUSEWIDTHSpecification.Text = string.Empty;
                        _session.P_USEWIDTH = null;
                    }
                }
                else
                    _session.P_USEWIDTH = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbUSEWIDTH_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbUSEWIDTH_TOR.SelectedValue != null)
                {
                    if (cbUSEWIDTH_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_USEWIDTH_TOR = "MIN.";
                    }
                    else if (cbUSEWIDTH_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_USEWIDTH_TOR = "MAX.";
                    }
                    else if (cbUSEWIDTH_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtUSEWIDTH_TOR.Text))
                        {
                            _session.P_USEWIDTH_TOR = txtUSEWIDTH_TOR.Text;
                        }
                        else
                            _session.P_USEWIDTH_TOR = string.Empty;
                    }
                }
                else
                    _session.P_USEWIDTH_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtUSEWIDTH_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbUSEWIDTH_TOR.SelectedValue != null)
                {
                    if (cbUSEWIDTH_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_USEWIDTH_TOR = "MIN.";
                    }
                    else if (cbUSEWIDTH_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_USEWIDTH_TOR = "MAX.";
                    }
                    else if (cbUSEWIDTH_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtUSEWIDTH_TOR.Text))
                        {
                            _session.P_USEWIDTH_TOR = txtUSEWIDTH_TOR.Text;
                        }
                        else
                            _session.P_USEWIDTH_TOR = string.Empty;
                    }
                }
                else
                    _session.P_USEWIDTH_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtUSEWIDTH_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUSEWIDTH_NO.Text))
                {
                    if (Decimal.TryParse(txtUSEWIDTH_NO.Text, out value))
                    {
                        _session.P_USEWIDTH_NO = decimal.Parse(txtUSEWIDTH_NO.Text);
                    }
                    else
                    {
                        txtUSEWIDTH_NO.Text = string.Empty;
                        _session.P_USEWIDTH_NO = null;
                    }
                }
                else
                    _session.P_USEWIDTH_NO = null;

                #region Foreground

                if (_session.P_USEWIDTH_NO != null)
                {
                    if (_session.P_USEWIDTH_NO > 3)
                        txtUSEWIDTH_NO.Foreground = Brushes.Red;
                    else
                        txtUSEWIDTH_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtWIDTHSILICONESpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtWIDTHSILICONESpecification.Text))
                {
                    if (Decimal.TryParse(txtWIDTHSILICONESpecification.Text, out value))
                    {
                        _session.P_WIDTHSILICONE = decimal.Parse(txtWIDTHSILICONESpecification.Text);
                    }
                    else
                    {
                        txtWIDTHSILICONESpecification.Text = string.Empty;
                        _session.P_WIDTHSILICONE = null;
                    }
                }
                else
                    _session.P_WIDTHSILICONE = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbWIDTHSILICONE_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbWIDTHSILICONE_TOR.SelectedValue != null)
                {
                    if (cbWIDTHSILICONE_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_WIDTHSILICONE_TOR = "MIN.";
                    }
                    else if (cbWIDTHSILICONE_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_WIDTHSILICONE_TOR = "MAX.";
                    }
                    else if (cbWIDTHSILICONE_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtWIDTHSILICONE_TOR.Text))
                        {
                            _session.P_WIDTHSILICONE_TOR = txtWIDTHSILICONE_TOR.Text;
                        }
                        else
                            _session.P_WIDTHSILICONE_TOR = string.Empty;
                    }
                }
                else
                    _session.P_WIDTHSILICONE_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtWIDTHSILICONE_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbWIDTHSILICONE_TOR.SelectedValue != null)
                {
                    if (cbWIDTHSILICONE_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_WIDTHSILICONE_TOR = "MIN.";
                    }
                    else if (cbWIDTHSILICONE_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_WIDTHSILICONE_TOR = "MAX.";
                    }
                    else if (cbWIDTHSILICONE_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtWIDTHSILICONE_TOR.Text))
                        {
                            _session.P_WIDTHSILICONE_TOR = txtWIDTHSILICONE_TOR.Text;
                        }
                        else
                            _session.P_WIDTHSILICONE_TOR = string.Empty;
                    }
                }
                else
                    _session.P_WIDTHSILICONE_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtWIDTHSILICONE_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtWIDTHSILICONE_NO.Text))
                {
                    if (Decimal.TryParse(txtWIDTHSILICONE_NO.Text, out value))
                    {
                        _session.P_WIDTHSILICONE_NO = decimal.Parse(txtWIDTHSILICONE_NO.Text);
                    }
                    else
                    {
                        txtWIDTHSILICONE_NO.Text = string.Empty;
                        _session.P_WIDTHSILICONE_NO = null;
                    }
                }
                else
                    _session.P_WIDTHSILICONE_NO = null;

                #region Foreground

                if (_session.P_WIDTHSILICONE_NO != null)
                {
                    if (_session.P_WIDTHSILICONE_NO > 3)
                        txtWIDTHSILICONE_NO.Foreground = Brushes.Red;
                    else
                        txtWIDTHSILICONE_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtNUMTHREADS_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNUMTHREADS_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_WSpecification.Text, out value))
                    {
                        _session.P_NUMTHREADS_W = decimal.Parse(txtNUMTHREADS_WSpecification.Text);
                    }
                    else
                    {
                        txtNUMTHREADS_WSpecification.Text = string.Empty;
                        _session.P_NUMTHREADS_W = null;
                    }
                }
                else
                    _session.P_NUMTHREADS_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtNUMTHREADS_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
             try
            {
                if (!string.IsNullOrEmpty(txtNUMTHREADS_W_TOR.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_W_TOR.Text, out value))
                    {
                        _session.P_NUMTHREADS_W_TOR = decimal.Parse(txtNUMTHREADS_W_TOR.Text);
                    }
                    else
                    {
                        txtNUMTHREADS_W_TOR.Text = string.Empty;
                        _session.P_NUMTHREADS_W_TOR = null;
                    }
                }
                else
                    _session.P_NUMTHREADS_W_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtNUMTHREADS_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNUMTHREADS_W_NO.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_W_NO.Text, out value))
                    {
                        _session.P_NUMTHREADS_W_NO = decimal.Parse(txtNUMTHREADS_W_NO.Text);
                    }
                    else
                    {
                        txtNUMTHREADS_W_NO.Text = string.Empty;
                        _session.P_NUMTHREADS_W_NO = null;
                    }
                }
                else
                    _session.P_NUMTHREADS_W_NO = null;

                #region Foreground

                if (_session.P_NUMTHREADS_W_NO != null)
                {
                    if (_session.P_NUMTHREADS_W_NO > 3)
                        txtNUMTHREADS_W_NO.Foreground = Brushes.Red;
                    else
                        txtNUMTHREADS_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtNUMTHREADS_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNUMTHREADS_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_FSpecification.Text, out value))
                    {
                        _session.P_NUMTHREADS_F = decimal.Parse(txtNUMTHREADS_FSpecification.Text);
                    }
                    else
                    {
                        txtNUMTHREADS_FSpecification.Text = string.Empty;
                        _session.P_NUMTHREADS_F = null;
                    }
                }
                else
                    _session.P_NUMTHREADS_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtNUMTHREADS_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNUMTHREADS_F_TOR.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_F_TOR.Text, out value))
                    {
                        _session.P_NUMTHREADS_F_TOR = decimal.Parse(txtNUMTHREADS_F_TOR.Text);
                    }
                    else
                    {
                        txtNUMTHREADS_F_TOR.Text = string.Empty;
                        _session.P_NUMTHREADS_F_TOR = null;
                    }
                }
                else
                    _session.P_NUMTHREADS_F_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtNUMTHREADS_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNUMTHREADS_F_NO.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_F_NO.Text, out value))
                    {
                        _session.P_NUMTHREADS_F_NO = decimal.Parse(txtNUMTHREADS_F_NO.Text);
                    }
                    else
                    {
                        txtNUMTHREADS_F_NO.Text = string.Empty;
                        _session.P_NUMTHREADS_F_NO = null;
                    }
                }
                else
                    _session.P_NUMTHREADS_F_NO = null;

                #region Foreground

                if (_session.P_NUMTHREADS_F_NO != null)
                {
                    if (_session.P_NUMTHREADS_F_NO > 3)
                        txtNUMTHREADS_F_NO.Foreground = Brushes.Red;
                    else
                        txtNUMTHREADS_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTOTALWEIGHTSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTOTALWEIGHTSpecification.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHTSpecification.Text, out value))
                    {
                        _session.P_TOTALWEIGHT = decimal.Parse(txtTOTALWEIGHTSpecification.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHTSpecification.Text = string.Empty;
                        _session.P_TOTALWEIGHT = null;
                    }
                }
                else
                    _session.P_TOTALWEIGHT = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTOTALWEIGHT_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTOTALWEIGHT_TOR.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHT_TOR.Text, out value))
                    {
                        _session.P_TOTALWEIGHT_TOR = decimal.Parse(txtTOTALWEIGHT_TOR.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHT_TOR.Text = string.Empty;
                        _session.P_TOTALWEIGHT_TOR = null;
                    }
                }
                else
                    _session.P_TOTALWEIGHT_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTOTALWEIGHT_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTOTALWEIGHT_NO.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHT_NO.Text, out value))
                    {
                        _session.P_TOTALWEIGHT_NO = decimal.Parse(txtTOTALWEIGHT_NO.Text);
                    }
                    else
                    {
                        txtTOTALWEIGHT_NO.Text = string.Empty;
                        _session.P_TOTALWEIGHT_NO = null;
                    }
                }
                else
                    _session.P_TOTALWEIGHT_NO = null;

                #region Foreground

                if (_session.P_TOTALWEIGHT_NO != null)
                {
                    if (_session.P_TOTALWEIGHT_NO > 6)
                        txtTOTALWEIGHT_NO.Foreground = Brushes.Red;
                    else
                        txtTOTALWEIGHT_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtUNCOATEDWEIGHTSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTSpecification.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHTSpecification.Text, out value))
                    {
                        _session.P_UNCOATEDWEIGHT = decimal.Parse(txtUNCOATEDWEIGHTSpecification.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHTSpecification.Text = string.Empty;
                        _session.P_UNCOATEDWEIGHT = null;
                    }
                }
                else
                    _session.P_UNCOATEDWEIGHT = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtUNCOATEDWEIGHT_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHT_TOR.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHT_TOR.Text, out value))
                    {
                        _session.P_UNCOATEDWEIGHT_TOR = decimal.Parse(txtUNCOATEDWEIGHT_TOR.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHT_TOR.Text = string.Empty;
                        _session.P_UNCOATEDWEIGHT_TOR = null;
                    }
                }
                else
                    _session.P_UNCOATEDWEIGHT_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtUNCOATEDWEIGHT_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHT_NO.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHT_NO.Text, out value))
                    {
                        _session.P_UNCOATEDWEIGHT_NO = decimal.Parse(txtUNCOATEDWEIGHT_NO.Text);
                    }
                    else
                    {
                        txtUNCOATEDWEIGHT_NO.Text = string.Empty;
                        _session.P_UNCOATEDWEIGHT_NO = null;
                    }
                }
                else
                    _session.P_UNCOATEDWEIGHT_NO = null;

                #region Foreground

                if (_session.P_UNCOATEDWEIGHT_NO != null)
                {
                    if (_session.P_UNCOATEDWEIGHT_NO > 6)
                        txtUNCOATEDWEIGHT_NO.Foreground = Brushes.Red;
                    else
                        txtUNCOATEDWEIGHT_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtCOATINGWEIGHTSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCOATINGWEIGHTSpecification.Text))
                {
                    if (Decimal.TryParse(txtCOATINGWEIGHTSpecification.Text, out value))
                    {
                        _session.P_COATWEIGHT = decimal.Parse(txtCOATINGWEIGHTSpecification.Text);
                    }
                    else
                    {
                        txtCOATINGWEIGHTSpecification.Text = string.Empty;
                        _session.P_COATWEIGHT = null;
                    }
                }
                else
                    _session.P_COATWEIGHT = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtCOATWEIGHT_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCOATWEIGHT_TOR.Text))
                {
                    if (Decimal.TryParse(txtCOATWEIGHT_TOR.Text, out value))
                    {
                        _session.P_COATWEIGHT_TOR = decimal.Parse(txtCOATWEIGHT_TOR.Text);
                    }
                    else
                    {
                        txtCOATWEIGHT_TOR.Text = string.Empty;
                        _session.P_COATWEIGHT_TOR = null;
                    }
                }
                else
                    _session.P_COATWEIGHT_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtCOATWEIGHT_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCOATWEIGHT_NO.Text))
                {
                    if (Decimal.TryParse(txtCOATWEIGHT_NO.Text, out value))
                    {
                        _session.P_COATWEIGHT_NO = decimal.Parse(txtCOATWEIGHT_NO.Text);
                    }
                    else
                    {
                        txtCOATWEIGHT_NO.Text = string.Empty;
                        _session.P_COATWEIGHT_NO = null;
                    }
                }
                else
                    _session.P_COATWEIGHT_NO = null;

                #region Foreground

                if (_session.P_COATWEIGHT_NO != null)
                {
                    if (_session.P_COATWEIGHT_NO > 6)
                        txtCOATWEIGHT_NO.Foreground = Brushes.Red;
                    else
                        txtCOATWEIGHT_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTHICKNESSSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTHICKNESSSpecification.Text))
                {
                    if (Decimal.TryParse(txtTHICKNESSSpecification.Text, out value))
                    {
                        _session.P_THICKNESS = decimal.Parse(txtTHICKNESSSpecification.Text);
                    }
                    else
                    {
                        txtTHICKNESSSpecification.Text = string.Empty;
                        _session.P_THICKNESS = null;
                    }
                }
                else
                    _session.P_THICKNESS = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTHICKNESS_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTHICKNESS_TOR.Text))
                {
                    if (Decimal.TryParse(txtTHICKNESS_TOR.Text, out value))
                    {
                        _session.P_THICKNESS_TOR = decimal.Parse(txtTHICKNESS_TOR.Text);
                    }
                    else
                    {
                        txtTHICKNESS_TOR.Text = string.Empty;
                        _session.P_THICKNESS_TOR = null;
                    }
                }
                else
                    _session.P_THICKNESS_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTHICKNESS_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTHICKNESS_NO.Text))
                {
                    if (Decimal.TryParse(txtTHICKNESS_NO.Text, out value))
                    {
                        _session.P_THICKNESS_NO = decimal.Parse(txtTHICKNESS_NO.Text);
                    }
                    else
                    {
                        txtTHICKNESS_NO.Text = string.Empty;
                        _session.P_THICKNESS_NO = null;
                    }
                }
                else
                    _session.P_THICKNESS_NO = null;

                #region Foreground

                if (_session.P_THICKNESS_NO != null)
                {
                    if (_session.P_THICKNESS_NO > 3)
                        txtTHICKNESS_NO.Foreground = Brushes.Red;
                    else
                        txtTHICKNESS_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtMAXFORCE_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMAXFORCE_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtMAXFORCE_WSpecification.Text, out value))
                    {
                        _session.P_MAXFORCE_W = decimal.Parse(txtMAXFORCE_WSpecification.Text);
                    }
                    else
                    {
                        txtMAXFORCE_WSpecification.Text = string.Empty;
                        _session.P_MAXFORCE_W = null;
                    }
                }
                else
                    _session.P_MAXFORCE_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbMAXFORCE_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbMAXFORCE_W_TOR.SelectedValue != null)
                {
                    if (cbMAXFORCE_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_MAXFORCE_W_TOR = "MIN.";
                    }
                    else if (cbMAXFORCE_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_MAXFORCE_W_TOR = "MAX.";
                    }
                    else if (cbMAXFORCE_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtMAXFORCE_W_TOR.Text))
                        {
                            _session.P_MAXFORCE_W_TOR = txtMAXFORCE_W_TOR.Text;
                        }
                        else
                            _session.P_MAXFORCE_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_MAXFORCE_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtMAXFORCE_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbMAXFORCE_W_TOR.SelectedValue != null)
                {
                    if (cbMAXFORCE_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_MAXFORCE_W_TOR = "MIN.";
                    }
                    else if (cbMAXFORCE_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_MAXFORCE_W_TOR = "MAX.";
                    }
                    else if (cbMAXFORCE_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtMAXFORCE_W_TOR.Text))
                        {
                            _session.P_MAXFORCE_W_TOR = txtMAXFORCE_W_TOR.Text;
                        }
                        else
                            _session.P_MAXFORCE_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_MAXFORCE_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtMAXFORCE_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMAXFORCE_W_NO.Text))
                {
                    if (Decimal.TryParse(txtMAXFORCE_W_NO.Text, out value))
                    {
                        _session.P_MAXFORCE_W_NO = decimal.Parse(txtMAXFORCE_W_NO.Text);
                    }
                    else
                    {
                        txtMAXFORCE_W_NO.Text = string.Empty;
                        _session.P_MAXFORCE_W_NO = null;
                    }
                }
                else
                    _session.P_MAXFORCE_W_NO = null;

                #region Foreground

                if (_session.P_MAXFORCE_W_NO != null)
                {
                    if (_session.P_MAXFORCE_W_NO > 6)
                        txtMAXFORCE_W_NO.Foreground = Brushes.Red;
                    else
                        txtMAXFORCE_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtMAXFORCE_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMAXFORCE_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtMAXFORCE_FSpecification.Text, out value))
                    {
                        _session.P_MAXFORCE_F = decimal.Parse(txtMAXFORCE_FSpecification.Text);
                    }
                    else
                    {
                        txtMAXFORCE_FSpecification.Text = string.Empty;
                        _session.P_MAXFORCE_F = null;
                    }
                }
                else
                    _session.P_MAXFORCE_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbMAXFORCE_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbMAXFORCE_F_TOR.SelectedValue != null)
                {
                    if (cbMAXFORCE_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_MAXFORCE_F_TOR = "MIN.";
                    }
                    else if (cbMAXFORCE_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_MAXFORCE_F_TOR = "MAX.";
                    }
                    else if (cbMAXFORCE_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtMAXFORCE_F_TOR.Text))
                        {
                            _session.P_MAXFORCE_F_TOR = txtMAXFORCE_F_TOR.Text;
                        }
                        else
                            _session.P_MAXFORCE_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_MAXFORCE_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtMAXFORCE_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbMAXFORCE_F_TOR.SelectedValue != null)
                {
                    if (cbMAXFORCE_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_MAXFORCE_F_TOR = "MIN.";
                    }
                    else if (cbMAXFORCE_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_MAXFORCE_F_TOR = "MAX.";
                    }
                    else if (cbMAXFORCE_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtMAXFORCE_F_TOR.Text))
                        {
                            _session.P_MAXFORCE_F_TOR = txtMAXFORCE_F_TOR.Text;
                        }
                        else
                            _session.P_MAXFORCE_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_MAXFORCE_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtMAXFORCE_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMAXFORCE_F_NO.Text))
                {
                    if (Decimal.TryParse(txtMAXFORCE_F_NO.Text, out value))
                    {
                        _session.P_MAXFORCE_F_NO = decimal.Parse(txtMAXFORCE_F_NO.Text);
                    }
                    else
                    {
                        txtMAXFORCE_F_NO.Text = string.Empty;
                        _session.P_MAXFORCE_F_NO = null;
                    }
                }
                else
                    _session.P_MAXFORCE_F_NO = null;

                #region Foreground

                if (_session.P_MAXFORCE_F_NO != null)
                {
                    if (_session.P_MAXFORCE_F_NO > 6)
                        txtMAXFORCE_F_NO.Foreground = Brushes.Red;
                    else
                        txtMAXFORCE_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtELOGATION_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtELOGATION_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtELOGATION_WSpecification.Text, out value))
                    {
                        _session.P_ELOGATION_W = decimal.Parse(txtELOGATION_WSpecification.Text);
                    }
                    else
                    {
                        txtELOGATION_WSpecification.Text = string.Empty;
                        _session.P_ELOGATION_W = null;
                    }
                }
                else
                    _session.P_ELOGATION_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbELOGATION_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbELOGATION_W_TOR.SelectedValue != null)
                {
                    if (cbELOGATION_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_ELOGATION_W_TOR = "MIN.";
                    }
                    else if (cbELOGATION_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_ELOGATION_W_TOR = "MAX.";
                    }
                    else if (cbELOGATION_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtELOGATION_W_TOR.Text))
                        {
                            _session.P_ELOGATION_W_TOR = txtELOGATION_W_TOR.Text;
                        }
                        else
                            _session.P_ELOGATION_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_ELOGATION_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtELOGATION_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbELOGATION_W_TOR.SelectedValue != null)
                {
                    if (cbELOGATION_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_ELOGATION_W_TOR = "MIN.";
                    }
                    else if (cbELOGATION_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_ELOGATION_W_TOR = "MAX.";
                    }
                    else if (cbELOGATION_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtELOGATION_W_TOR.Text))
                        {
                            _session.P_ELOGATION_W_TOR = txtELOGATION_W_TOR.Text;
                        }
                        else
                            _session.P_ELOGATION_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_ELOGATION_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtELOGATION_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtELOGATION_W_NO.Text))
                {
                    if (Decimal.TryParse(txtELOGATION_W_NO.Text, out value))
                    {
                        _session.P_ELOGATION_W_NO = decimal.Parse(txtELOGATION_W_NO.Text);
                    }
                    else
                    {
                        txtELOGATION_W_NO.Text = string.Empty;
                        _session.P_ELOGATION_W_NO = null;
                    }
                }
                else
                    _session.P_ELOGATION_W_NO = null;

                #region Foreground

                if (_session.P_ELOGATION_W_NO != null)
                {
                    if (_session.P_ELOGATION_W_NO > 6)
                        txtELOGATION_W_NO.Foreground = Brushes.Red;
                    else
                        txtELOGATION_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtELOGATION_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtELOGATION_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtELOGATION_FSpecification.Text, out value))
                    {
                        _session.P_ELOGATION_F = decimal.Parse(txtELOGATION_FSpecification.Text);
                    }
                    else
                    {
                        txtELOGATION_FSpecification.Text = string.Empty;
                        _session.P_ELOGATION_F = null;
                    }
                }
                else
                    _session.P_ELOGATION_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbELOGATION_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbELOGATION_F_TOR.SelectedValue != null)
                {
                    if (cbELOGATION_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_ELOGATION_F_TOR = "MIN.";
                    }
                    else if (cbELOGATION_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_ELOGATION_F_TOR = "MAX.";
                    }
                    else if (cbELOGATION_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtELOGATION_F_TOR.Text))
                        {
                            _session.P_ELOGATION_F_TOR = txtELOGATION_F_TOR.Text;
                        }
                        else
                            _session.P_ELOGATION_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_ELOGATION_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtELOGATION_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbELOGATION_F_TOR.SelectedValue != null)
                {
                    if (cbELOGATION_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_ELOGATION_F_TOR = "MIN.";
                    }
                    else if (cbELOGATION_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_ELOGATION_F_TOR = "MAX.";
                    }
                    else if (cbELOGATION_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtELOGATION_F_TOR.Text))
                        {
                            _session.P_ELOGATION_F_TOR = txtELOGATION_F_TOR.Text;
                        }
                        else
                            _session.P_ELOGATION_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_ELOGATION_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtELOGATION_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtELOGATION_F_NO.Text))
                {
                    if (Decimal.TryParse(txtELOGATION_F_NO.Text, out value))
                    {
                        _session.P_ELOGATION_F_NO = decimal.Parse(txtELOGATION_F_NO.Text);
                    }
                    else
                    {
                        txtELOGATION_F_NO.Text = string.Empty;
                        _session.P_ELOGATION_F_NO = null;
                    }
                }
                else
                    _session.P_ELOGATION_F_NO = null;

                #region Foreground

                if (_session.P_ELOGATION_F_NO != null)
                {
                    if (_session.P_ELOGATION_F_NO > 6)
                        txtELOGATION_F_NO.Foreground = Brushes.Red;
                    else
                        txtELOGATION_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

         private void txtFLAMMABILITY_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_WSpecification.Text, out value))
                    {
                        _session.P_FLAMMABILITY_W = decimal.Parse(txtFLAMMABILITY_WSpecification.Text);
                    }
                    else
                    {
                        txtFLAMMABILITY_WSpecification.Text = string.Empty;
                        _session.P_FLAMMABILITY_W = null;
                    }
                }
                else
                    _session.P_FLAMMABILITY_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbFLAMMABILITY_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLAMMABILITY_W_TOR.SelectedValue != null)
                {
                    if (cbFLAMMABILITY_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLAMMABILITY_W_TOR = "MIN.";
                    }
                    else if (cbFLAMMABILITY_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLAMMABILITY_W_TOR = "MAX.";
                    }
                    else if (cbFLAMMABILITY_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLAMMABILITY_W_TOR.Text))
                        {
                            _session.P_FLAMMABILITY_W_TOR = txtFLAMMABILITY_W_TOR.Text;
                        }
                        else
                            _session.P_FLAMMABILITY_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLAMMABILITY_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLAMMABILITY_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLAMMABILITY_W_TOR.SelectedValue != null)
                {
                    if (cbFLAMMABILITY_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLAMMABILITY_W_TOR = "MIN.";
                    }
                    else if (cbFLAMMABILITY_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLAMMABILITY_W_TOR = "MAX.";
                    }
                    else if (cbFLAMMABILITY_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLAMMABILITY_W_TOR.Text))
                        {
                            _session.P_FLAMMABILITY_W_TOR = txtFLAMMABILITY_W_TOR.Text;
                        }
                        else
                            _session.P_FLAMMABILITY_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLAMMABILITY_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLAMMABILITY_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_W_NO.Text, out value))
                    {
                        _session.P_FLAMMABILITY_W_NO = decimal.Parse(txtFLAMMABILITY_W_NO.Text);
                    }
                    else
                    {
                        txtFLAMMABILITY_W_NO.Text = string.Empty;
                        _session.P_FLAMMABILITY_W_NO = null;
                    }
                }
                else
                    _session.P_FLAMMABILITY_W_NO = null;

                #region Foreground

                if (_session.P_FLAMMABILITY_W_NO != null)
                {
                    if (_session.P_FLAMMABILITY_W_NO > 5)
                        txtFLAMMABILITY_W_NO.Foreground = Brushes.Red;
                    else
                        txtFLAMMABILITY_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLAMMABILITY_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_FSpecification.Text, out value))
                    {
                        _session.P_FLAMMABILITY_F = decimal.Parse(txtFLAMMABILITY_FSpecification.Text);
                    }
                    else
                    {
                        txtFLAMMABILITY_FSpecification.Text = string.Empty;
                        _session.P_FLAMMABILITY_F = null;
                    }
                }
                else
                    _session.P_FLAMMABILITY_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbFLAMMABILITY_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLAMMABILITY_F_TOR.SelectedValue != null)
                {
                    if (cbFLAMMABILITY_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLAMMABILITY_F_TOR = "MIN.";
                    }
                    else if (cbFLAMMABILITY_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLAMMABILITY_F_TOR = "MAX.";
                    }
                    else if (cbFLAMMABILITY_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLAMMABILITY_F_TOR.Text))
                        {
                            _session.P_FLAMMABILITY_F_TOR = txtFLAMMABILITY_F_TOR.Text;
                        }
                        else
                            _session.P_FLAMMABILITY_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLAMMABILITY_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLAMMABILITY_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLAMMABILITY_F_TOR.SelectedValue != null)
                {
                    if (cbFLAMMABILITY_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLAMMABILITY_F_TOR = "MIN.";
                    }
                    else if (cbFLAMMABILITY_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLAMMABILITY_F_TOR = "MAX.";
                    }
                    else if (cbFLAMMABILITY_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLAMMABILITY_F_TOR.Text))
                        {
                            _session.P_FLAMMABILITY_F_TOR = txtFLAMMABILITY_F_TOR.Text;
                        }
                        else
                            _session.P_FLAMMABILITY_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLAMMABILITY_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLAMMABILITY_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_F_NO.Text, out value))
                    {
                        _session.P_FLAMMABILITY_F_NO = decimal.Parse(txtFLAMMABILITY_F_NO.Text);
                    }
                    else
                    {
                        txtFLAMMABILITY_F_NO.Text = string.Empty;
                        _session.P_FLAMMABILITY_F_NO = null;
                    }
                }
                else
                    _session.P_FLAMMABILITY_F_NO = null;

                #region Foreground

                if (_session.P_FLAMMABILITY_F_NO != null)
                {
                    if (_session.P_FLAMMABILITY_F_NO > 5)
                        txtFLAMMABILITY_F_NO.Foreground = Brushes.Red;
                    else
                        txtFLAMMABILITY_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEDGECOMB_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEDGECOMB_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtEDGECOMB_WSpecification.Text, out value))
                    {
                        _session.P_EDGECOMB_W = decimal.Parse(txtEDGECOMB_WSpecification.Text);
                    }
                    else
                    {
                        txtEDGECOMB_WSpecification.Text = string.Empty;
                        _session.P_EDGECOMB_W = null;
                    }
                }
                else
                    _session.P_EDGECOMB_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbEDGECOMB_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbEDGECOMB_W_TOR.SelectedValue != null)
                {
                    if (cbEDGECOMB_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_EDGECOMB_W_TOR = "MIN.";
                    }
                    else if (cbEDGECOMB_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_EDGECOMB_W_TOR = "MAX.";
                    }
                    else if (cbEDGECOMB_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtEDGECOMB_W_TOR.Text))
                        {
                            _session.P_EDGECOMB_W_TOR = txtEDGECOMB_W_TOR.Text;
                        }
                        else
                            _session.P_EDGECOMB_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_EDGECOMB_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEDGECOMB_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbEDGECOMB_W_TOR.SelectedValue != null)
                {
                    if (cbEDGECOMB_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_EDGECOMB_W_TOR = "MIN.";
                    }
                    else if (cbEDGECOMB_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_EDGECOMB_W_TOR = "MAX.";
                    }
                    else if (cbEDGECOMB_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtEDGECOMB_W_TOR.Text))
                        {
                            _session.P_EDGECOMB_W_TOR = txtEDGECOMB_W_TOR.Text;
                        }
                        else
                            _session.P_EDGECOMB_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_EDGECOMB_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEDGECOMB_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEDGECOMB_W_NO.Text))
                {
                    if (Decimal.TryParse(txtEDGECOMB_W_NO.Text, out value))
                    {
                        _session.P_EDGECOMB_W_NO = decimal.Parse(txtEDGECOMB_W_NO.Text);
                    }
                    else
                    {
                        txtEDGECOMB_W_NO.Text = string.Empty;
                        _session.P_EDGECOMB_W_NO = null;
                    }
                }
                else
                    _session.P_EDGECOMB_W_NO = null;

                #region Foreground

                if (_session.P_EDGECOMB_W_NO != null)
                {
                    if (_session.P_EDGECOMB_W_NO > 3)
                        txtEDGECOMB_W_NO.Foreground = Brushes.Red;
                    else
                        txtEDGECOMB_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEDGECOMB_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEDGECOMB_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtEDGECOMB_FSpecification.Text, out value))
                    {
                        _session.P_EDGECOMB_F = decimal.Parse(txtEDGECOMB_FSpecification.Text);
                    }
                    else
                    {
                        txtEDGECOMB_FSpecification.Text = string.Empty;
                        _session.P_EDGECOMB_F = null;
                    }
                }
                else
                    _session.P_EDGECOMB_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbEDGECOMB_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbEDGECOMB_F_TOR.SelectedValue != null)
                {
                    if (cbEDGECOMB_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_EDGECOMB_F_TOR = "MIN.";
                    }
                    else if (cbEDGECOMB_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_EDGECOMB_F_TOR = "MAX.";
                    }
                    else if (cbEDGECOMB_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtEDGECOMB_F_TOR.Text))
                        {
                            _session.P_EDGECOMB_F_TOR = txtEDGECOMB_F_TOR.Text;
                        }
                        else
                            _session.P_EDGECOMB_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_EDGECOMB_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEDGECOMB_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbEDGECOMB_F_TOR.SelectedValue != null)
                {
                    if (cbEDGECOMB_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_EDGECOMB_F_TOR = "MIN.";
                    }
                    else if (cbEDGECOMB_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_EDGECOMB_F_TOR = "MAX.";
                    }
                    else if (cbEDGECOMB_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtEDGECOMB_F_TOR.Text))
                        {
                            _session.P_EDGECOMB_F_TOR = txtEDGECOMB_F_TOR.Text;
                        }
                        else
                            _session.P_EDGECOMB_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_EDGECOMB_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEDGECOMB_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEDGECOMB_F_NO.Text))
                {
                    if (Decimal.TryParse(txtEDGECOMB_F_NO.Text, out value))
                    {
                        _session.P_EDGECOMB_F_NO = decimal.Parse(txtEDGECOMB_F_NO.Text);
                    }
                    else
                    {
                        txtEDGECOMB_F_NO.Text = string.Empty;
                        _session.P_EDGECOMB_F_NO = null;
                    }
                }
                else
                    _session.P_EDGECOMB_F_NO = null;

                #region Foreground

                if (_session.P_EDGECOMB_F_NO != null)
                {
                    if (_session.P_EDGECOMB_F_NO > 3)
                        txtEDGECOMB_F_NO.Foreground = Brushes.Red;
                    else
                        txtEDGECOMB_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTIFFNES_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSTIFFNES_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_WSpecification.Text, out value))
                    {
                        _session.P_STIFFNESS_W = decimal.Parse(txtSTIFFNES_WSpecification.Text);
                    }
                    else
                    {
                        txtSTIFFNES_WSpecification.Text = string.Empty;
                        _session.P_STIFFNESS_W = null;
                    }
                }
                else
                    _session.P_STIFFNESS_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbSTIFFNESS_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSTIFFNESS_W_TOR.SelectedValue != null)
                {
                    if (cbSTIFFNESS_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_STIFFNESS_W_TOR = "MIN.";
                    }
                    else if (cbSTIFFNESS_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_STIFFNESS_W_TOR = "MAX.";
                    }
                    else if (cbSTIFFNESS_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSTIFFNESS_W_TOR.Text))
                        {
                            _session.P_STIFFNESS_W_TOR = txtSTIFFNESS_W_TOR.Text;
                        }
                        else
                            _session.P_STIFFNESS_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_STIFFNESS_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTIFFNESS_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSTIFFNESS_W_TOR.SelectedValue != null)
                {
                    if (cbSTIFFNESS_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_STIFFNESS_W_TOR = "MIN.";
                    }
                    else if (cbSTIFFNESS_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_STIFFNESS_W_TOR = "MAX.";
                    }
                    else if (cbSTIFFNESS_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSTIFFNESS_W_TOR.Text))
                        {
                            _session.P_STIFFNESS_W_TOR = txtSTIFFNESS_W_TOR.Text;
                        }
                        else
                            _session.P_STIFFNESS_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_STIFFNESS_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTIFFNESS_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSTIFFNESS_W_NO.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNESS_W_NO.Text, out value))
                    {
                        _session.P_STIFFNESS_W_NO = decimal.Parse(txtSTIFFNESS_W_NO.Text);
                    }
                    else
                    {
                        txtSTIFFNESS_W_NO.Text = string.Empty;
                        _session.P_STIFFNESS_W_NO = null;
                    }
                }
                else
                    _session.P_STIFFNESS_W_NO = null;

                #region Foreground

                if (_session.P_STIFFNESS_W_NO != null)
                {
                    if (_session.P_STIFFNESS_W_NO > 3)
                        txtSTIFFNESS_W_NO.Foreground = Brushes.Red;
                    else
                        txtSTIFFNESS_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTIFFNES_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSTIFFNES_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNES_FSpecification.Text, out value))
                    {
                        _session.P_STIFFNESS_F = decimal.Parse(txtSTIFFNES_FSpecification.Text);
                    }
                    else
                    {
                        txtSTIFFNES_FSpecification.Text = string.Empty;
                        _session.P_STIFFNESS_F = null;
                    }
                }
                else
                    _session.P_STIFFNESS_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbSTIFFNESS_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSTIFFNESS_F_TOR.SelectedValue != null)
                {
                    if (cbSTIFFNESS_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_STIFFNESS_F_TOR = "MIN.";
                    }
                    else if (cbSTIFFNESS_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_STIFFNESS_F_TOR = "MAX.";
                    }
                    else if (cbSTIFFNESS_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSTIFFNESS_F_TOR.Text))
                        {
                            _session.P_STIFFNESS_F_TOR = txtSTIFFNESS_F_TOR.Text;
                        }
                        else
                            _session.P_STIFFNESS_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_STIFFNESS_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTIFFNESS_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSTIFFNESS_F_TOR.SelectedValue != null)
                {
                    if (cbSTIFFNESS_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_STIFFNESS_F_TOR = "MIN.";
                    }
                    else if (cbSTIFFNESS_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_STIFFNESS_F_TOR = "MAX.";
                    }
                    else if (cbSTIFFNESS_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSTIFFNESS_F_TOR.Text))
                        {
                            _session.P_STIFFNESS_F_TOR = txtSTIFFNESS_F_TOR.Text;
                        }
                        else
                            _session.P_STIFFNESS_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_STIFFNESS_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTIFFNESS_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSTIFFNESS_F_NO.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNESS_F_NO.Text, out value))
                    {
                        _session.P_STIFFNESS_F_NO = decimal.Parse(txtSTIFFNESS_F_NO.Text);
                    }
                    else
                    {
                        txtSTIFFNESS_F_NO.Text = string.Empty;
                        _session.P_STIFFNESS_F_NO = null;
                    }
                }
                else
                    _session.P_STIFFNESS_F_NO = null;

                #region Foreground

                if (_session.P_STIFFNESS_F_NO != null)
                {
                    if (_session.P_STIFFNESS_F_NO > 3)
                        txtSTIFFNESS_F_NO.Foreground = Brushes.Red;
                    else
                        txtSTIFFNESS_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTEAR_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTEAR_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtTEAR_WSpecification.Text, out value))
                    {
                        _session.P_TEAR_W = decimal.Parse(txtTEAR_WSpecification.Text);
                    }
                    else
                    {
                        txtTEAR_WSpecification.Text = string.Empty;
                        _session.P_TEAR_W = null;
                    }
                }
                else
                    _session.P_TEAR_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbTEAR_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTEAR_W_TOR.SelectedValue != null)
                {
                    if (cbTEAR_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_TEAR_W_TOR = "MIN.";
                    }
                    else if (cbTEAR_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_TEAR_W_TOR = "MAX.";
                    }
                    else if (cbTEAR_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtTEAR_W_TOR.Text))
                        {
                            _session.P_TEAR_W_TOR = txtTEAR_W_TOR.Text;
                        }
                        else
                            _session.P_TEAR_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_TEAR_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTEAR_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTEAR_W_TOR.SelectedValue != null)
                {
                    if (cbTEAR_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_TEAR_W_TOR = "MIN.";
                    }
                    else if (cbTEAR_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_TEAR_W_TOR = "MAX.";
                    }
                    else if (cbTEAR_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtTEAR_W_TOR.Text))
                        {
                            _session.P_TEAR_W_TOR = txtTEAR_W_TOR.Text;
                        }
                        else
                            _session.P_TEAR_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_TEAR_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTEAR_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTEAR_W_NO.Text))
                {
                    if (Decimal.TryParse(txtTEAR_W_NO.Text, out value))
                    {
                        _session.P_TEAR_W_NO = decimal.Parse(txtTEAR_W_NO.Text);
                    }
                    else
                    {
                        txtTEAR_W_NO.Text = string.Empty;
                        _session.P_TEAR_W_NO = null;
                    }
                }
                else
                    _session.P_TEAR_W_NO = null;

                #region Foreground

                if (_session.P_TEAR_W_NO != null)
                {
                    if (_session.P_TEAR_W_NO > 3)
                        txtTEAR_W_NO.Foreground = Brushes.Red;
                    else
                        txtTEAR_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTEAR_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTEAR_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtTEAR_FSpecification.Text, out value))
                    {
                        _session.P_TEAR_F = decimal.Parse(txtTEAR_FSpecification.Text);
                    }
                    else
                    {
                        txtTEAR_FSpecification.Text = string.Empty;
                        _session.P_TEAR_F = null;
                    }
                }
                else
                    _session.P_TEAR_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbTEAR_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTEAR_F_TOR.SelectedValue != null)
                {
                    if (cbTEAR_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_TEAR_F_TOR = "MIN.";
                    }
                    else if (cbTEAR_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_TEAR_F_TOR = "MAX.";
                    }
                    else if (cbTEAR_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtTEAR_F_TOR.Text))
                        {
                            _session.P_TEAR_F_TOR = txtTEAR_F_TOR.Text;
                        }
                        else
                            _session.P_TEAR_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_TEAR_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTEAR_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbTEAR_F_TOR.SelectedValue != null)
                {
                    if (cbTEAR_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_TEAR_F_TOR = "MIN.";
                    }
                    else if (cbTEAR_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_TEAR_F_TOR = "MAX.";
                    }
                    else if (cbTEAR_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtTEAR_F_TOR.Text))
                        {
                            _session.P_TEAR_F_TOR = txtTEAR_F_TOR.Text;
                        }
                        else
                            _session.P_TEAR_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_TEAR_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtTEAR_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTEAR_F_NO.Text))
                {
                    if (Decimal.TryParse(txtTEAR_F_NO.Text, out value))
                    {
                        _session.P_TEAR_F_NO = decimal.Parse(txtTEAR_F_NO.Text);
                    }
                    else
                    {
                        txtTEAR_F_NO.Text = string.Empty;
                        _session.P_TEAR_F_NO = null;
                    }
                }
                else
                    _session.P_TEAR_F_NO = null;

                #region Foreground

                if (_session.P_TEAR_F_NO != null)
                {
                    if (_session.P_TEAR_F_NO > 3)
                        txtTEAR_F_NO.Foreground = Brushes.Red;
                    else
                        txtTEAR_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTATIC_AIRSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSTATIC_AIRSpecification.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIRSpecification.Text, out value))
                    {
                        _session.P_STATIC_AIR = decimal.Parse(txtSTATIC_AIRSpecification.Text);
                    }
                    else
                    {
                        txtSTATIC_AIRSpecification.Text = string.Empty;
                        _session.P_STATIC_AIR = null;
                    }
                }
                else
                    _session.P_STATIC_AIR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbSTATIC_AIR_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSTATIC_AIR_TOR.SelectedValue != null)
                {
                    if (cbSTATIC_AIR_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_STATIC_AIR_TOR = "MIN.";
                    }
                    else if (cbSTATIC_AIR_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_STATIC_AIR_TOR = "MAX.";
                    }
                    else if (cbSTATIC_AIR_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSTATIC_AIR_TOR.Text))
                        {
                            _session.P_STATIC_AIR_TOR = txtSTATIC_AIR_TOR.Text;
                        }
                        else
                            _session.P_STATIC_AIR_TOR = string.Empty;
                    }
                }
                else
                    _session.P_STATIC_AIR_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTATIC_AIR_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSTATIC_AIR_TOR.SelectedValue != null)
                {
                    if (cbSTATIC_AIR_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_STATIC_AIR_TOR = "MIN.";
                    }
                    else if (cbSTATIC_AIR_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_STATIC_AIR_TOR = "MAX.";
                    }
                    else if (cbSTATIC_AIR_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSTATIC_AIR_TOR.Text))
                        {
                            _session.P_STATIC_AIR_TOR = txtSTATIC_AIR_TOR.Text;
                        }
                        else
                            _session.P_STATIC_AIR_TOR = string.Empty;
                    }
                }
                else
                    _session.P_STATIC_AIR_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSTATIC_AIR_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSTATIC_AIR_NO.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIR_NO.Text, out value))
                    {
                        _session.P_STATIC_AIR_NO = decimal.Parse(txtSTATIC_AIR_NO.Text);
                    }
                    else
                    {
                        txtSTATIC_AIR_NO.Text = string.Empty;
                        _session.P_STATIC_AIR_NO = null;
                    }
                }
                else
                    _session.P_STATIC_AIR_NO = null;

                #region Foreground

                if (_session.P_STATIC_AIR_NO != null)
                {
                    if (_session.P_STATIC_AIR_NO > 6)
                        txtSTATIC_AIR_NO.Foreground = Brushes.Red;
                    else
                        txtSTATIC_AIR_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDYNAMIC_AIRSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDYNAMIC_AIRSpecification.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIRSpecification.Text, out value))
                    {
                        _session.P_DYNAMIC_AIR = decimal.Parse(txtDYNAMIC_AIRSpecification.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIRSpecification.Text = string.Empty;
                        _session.P_DYNAMIC_AIR = null;
                    }
                }
                else
                    _session.P_DYNAMIC_AIR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDYNAMIC_AIR_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDYNAMIC_AIR_TOR.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIR_TOR.Text, out value))
                    {
                        _session.P_DYNAMIC_AIR_TOR = decimal.Parse(txtDYNAMIC_AIR_TOR.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIR_TOR.Text = string.Empty;
                        _session.P_DYNAMIC_AIR_TOR = null;
                    }
                }
                else
                    _session.P_DYNAMIC_AIR_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDYNAMIC_AIR_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDYNAMIC_AIR_NO.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIR_NO.Text, out value))
                    {
                        _session.P_DYNAMIC_AIR_NO = decimal.Parse(txtDYNAMIC_AIR_NO.Text);
                    }
                    else
                    {
                        txtDYNAMIC_AIR_NO.Text = string.Empty;
                        _session.P_DYNAMIC_AIR_NO = null;
                    }
                }
                else
                    _session.P_DYNAMIC_AIR_NO = null;

                #region Foreground

                if (_session.P_DYNAMIC_AIR_NO != null)
                {
                    if (_session.P_DYNAMIC_AIR_NO > 3)
                        txtDYNAMIC_AIR_NO.Foreground = Brushes.Red;
                    else
                        txtDYNAMIC_AIR_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEXPONENTSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEXPONENTSpecification.Text))
                {
                    if (Decimal.TryParse(txtEXPONENTSpecification.Text, out value))
                    {
                        _session.P_EXPONENT = decimal.Parse(txtEXPONENTSpecification.Text);
                    }
                    else
                    {
                        txtEXPONENTSpecification.Text = string.Empty;
                        _session.P_EXPONENT = null;
                    }
                }
                else
                    _session.P_EXPONENT = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEXPONENT_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEXPONENT_TOR.Text))
                {
                    if (Decimal.TryParse(txtEXPONENT_TOR.Text, out value))
                    {
                        _session.P_EXPONENT_TOR = decimal.Parse(txtEXPONENT_TOR.Text);
                    }
                    else
                    {
                        txtEXPONENT_TOR.Text = string.Empty;
                        _session.P_EXPONENT_TOR = null;
                    }
                }
                else
                    _session.P_EXPONENT_TOR = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtEXPONENT_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEXPONENT_NO.Text))
                {
                    if (Decimal.TryParse(txtEXPONENT_NO.Text, out value))
                    {
                        _session.P_EXPONENT_NO = decimal.Parse(txtEXPONENT_NO.Text);
                    }
                    else
                    {
                        txtEXPONENT_NO.Text = string.Empty;
                        _session.P_EXPONENT_NO = null;
                    }
                }
                else
                    _session.P_EXPONENT_NO = null;

                #region Foreground

                if (_session.P_EXPONENT_NO != null)
                {
                    if (_session.P_EXPONENT_NO > 3)
                        txtEXPONENT_NO.Foreground = Brushes.Red;
                    else
                        txtEXPONENT_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDIMENSCHANGE_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_WSpecification.Text, out value))
                    {
                        _session.P_DIMENSCHANGE_W = decimal.Parse(txtDIMENSCHANGE_WSpecification.Text);
                    }
                    else
                    {
                        txtDIMENSCHANGE_WSpecification.Text = string.Empty;
                        _session.P_DIMENSCHANGE_W = null;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbDIMENSCHANGE_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbDIMENSCHANGE_W_TOR.SelectedValue != null)
                {
                    if (cbDIMENSCHANGE_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_DIMENSCHANGE_W_TOR = "MIN.";
                    }
                    else if (cbDIMENSCHANGE_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_DIMENSCHANGE_W_TOR = "MAX.";
                    }
                    else if (cbDIMENSCHANGE_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtDIMENSCHANGE_W_TOR.Text))
                        {
                            _session.P_DIMENSCHANGE_W_TOR = txtDIMENSCHANGE_W_TOR.Text;
                        }
                        else
                            _session.P_DIMENSCHANGE_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDIMENSCHANGE_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbDIMENSCHANGE_W_TOR.SelectedValue != null)
                {
                    if (cbDIMENSCHANGE_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_DIMENSCHANGE_W_TOR = "MIN.";
                    }
                    else if (cbDIMENSCHANGE_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_DIMENSCHANGE_W_TOR = "MAX.";
                    }
                    else if (cbDIMENSCHANGE_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtDIMENSCHANGE_W_TOR.Text))
                        {
                            _session.P_DIMENSCHANGE_W_TOR = txtDIMENSCHANGE_W_TOR.Text;
                        }
                        else
                            _session.P_DIMENSCHANGE_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDIMENSCHANGE_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_W_NO.Text))
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_W_NO.Text, out value))
                    {
                        _session.P_DIMENSCHANGE_W_NO = decimal.Parse(txtDIMENSCHANGE_W_NO.Text);
                    }
                    else
                    {
                        txtDIMENSCHANGE_W_NO.Text = string.Empty;
                        _session.P_DIMENSCHANGE_W_NO = null;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_W_NO = null;

                #region Foreground

                if (_session.P_DIMENSCHANGE_W_NO != null)
                {
                    if (_session.P_DIMENSCHANGE_W_NO > 3)
                        txtDIMENSCHANGE_W_NO.Foreground = Brushes.Red;
                    else
                        txtDIMENSCHANGE_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDIMENSCHANGE_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_FSpecification.Text, out value))
                    {
                        _session.P_DIMENSCHANGE_F = decimal.Parse(txtDIMENSCHANGE_FSpecification.Text);
                    }
                    else
                    {
                        txtDIMENSCHANGE_FSpecification.Text = string.Empty;
                        _session.P_DIMENSCHANGE_F = null;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbDIMENSCHANGE_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbDIMENSCHANGE_F_TOR.SelectedValue != null)
                {
                    if (cbDIMENSCHANGE_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_DIMENSCHANGE_F_TOR = "MIN.";
                    }
                    else if (cbDIMENSCHANGE_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_DIMENSCHANGE_F_TOR = "MAX.";
                    }
                    else if (cbDIMENSCHANGE_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtDIMENSCHANGE_F_TOR.Text))
                        {
                            _session.P_DIMENSCHANGE_F_TOR = txtDIMENSCHANGE_F_TOR.Text;
                        }
                        else
                            _session.P_DIMENSCHANGE_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDIMENSCHANGE_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbDIMENSCHANGE_F_TOR.SelectedValue != null)
                {
                    if (cbDIMENSCHANGE_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_DIMENSCHANGE_F_TOR = "MIN.";
                    }
                    else if (cbDIMENSCHANGE_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_DIMENSCHANGE_F_TOR = "MAX.";
                    }
                    else if (cbDIMENSCHANGE_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtDIMENSCHANGE_F_TOR.Text))
                        {
                            _session.P_DIMENSCHANGE_F_TOR = txtDIMENSCHANGE_F_TOR.Text;
                        }
                        else
                            _session.P_DIMENSCHANGE_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtDIMENSCHANGE_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_F_NO.Text))
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_F_NO.Text, out value))
                    {
                        _session.P_DIMENSCHANGE_F_NO = decimal.Parse(txtDIMENSCHANGE_F_NO.Text);
                    }
                    else
                    {
                        txtDIMENSCHANGE_F_NO.Text = string.Empty;
                        _session.P_DIMENSCHANGE_F_NO = null;
                    }
                }
                else
                    _session.P_DIMENSCHANGE_F_NO = null;

                #region Foreground

                if (_session.P_DIMENSCHANGE_F_NO != null)
                {
                    if (_session.P_DIMENSCHANGE_F_NO > 3)
                        txtDIMENSCHANGE_F_NO.Foreground = Brushes.Red;
                    else
                        txtDIMENSCHANGE_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEXABRASION_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEXABRASION_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtFLEXABRASION_WSpecification.Text, out value))
                    {
                        _session.P_FLEXABRASION_W = decimal.Parse(txtFLEXABRASION_WSpecification.Text);
                    }
                    else
                    {
                        txtFLEXABRASION_WSpecification.Text = string.Empty;
                        _session.P_FLEXABRASION_W = null;
                    }
                }
                else
                    _session.P_FLEXABRASION_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbFLEXABRASION_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEXABRASION_W_TOR.SelectedValue != null)
                {
                    if (cbFLEXABRASION_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEXABRASION_W_TOR = "MIN.";
                    }
                    else if (cbFLEXABRASION_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEXABRASION_W_TOR = "MAX.";
                    }
                    else if (cbFLEXABRASION_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEXABRASION_W_TOR.Text))
                        {
                            _session.P_FLEXABRASION_W_TOR = txtFLEXABRASION_W_TOR.Text;
                        }
                        else
                            _session.P_FLEXABRASION_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEXABRASION_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEXABRASION_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEXABRASION_W_TOR.SelectedValue != null)
                {
                    if (cbFLEXABRASION_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEXABRASION_W_TOR = "MIN.";
                    }
                    else if (cbFLEXABRASION_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEXABRASION_W_TOR = "MAX.";
                    }
                    else if (cbFLEXABRASION_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEXABRASION_W_TOR.Text))
                        {
                            _session.P_FLEXABRASION_W_TOR = txtFLEXABRASION_W_TOR.Text;
                        }
                        else
                            _session.P_FLEXABRASION_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEXABRASION_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEXABRASION_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEXABRASION_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEXABRASION_W_NO.Text, out value))
                    {
                        _session.P_FLEXABRASION_W_NO = decimal.Parse(txtFLEXABRASION_W_NO.Text);
                    }
                    else
                    {
                        txtFLEXABRASION_W_NO.Text = string.Empty;
                        _session.P_FLEXABRASION_W_NO = null;
                    }
                }
                else
                    _session.P_FLEXABRASION_W_NO = null;

                #region Foreground

                if (_session.P_FLEXABRASION_W_NO != null)
                {
                    if (_session.P_FLEXABRASION_W_NO > 3)
                        txtFLEXABRASION_W_NO.Foreground = Brushes.Red;
                    else
                        txtFLEXABRASION_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEXABRASION_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEXABRASION_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtFLEXABRASION_FSpecification.Text, out value))
                    {
                        _session.P_FLEXABRASION_F = decimal.Parse(txtFLEXABRASION_FSpecification.Text);
                    }
                    else
                    {
                        txtFLEXABRASION_FSpecification.Text = string.Empty;
                        _session.P_FLEXABRASION_F = null;
                    }
                }
                else
                    _session.P_FLEXABRASION_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbFLEXABRASION_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEXABRASION_F_TOR.SelectedValue != null)
                {
                    if (cbFLEXABRASION_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEXABRASION_F_TOR = "MIN.";
                    }
                    else if (cbFLEXABRASION_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEXABRASION_F_TOR = "MAX.";
                    }
                    else if (cbFLEXABRASION_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEXABRASION_F_TOR.Text))
                        {
                            _session.P_FLEXABRASION_F_TOR = txtFLEXABRASION_F_TOR.Text;
                        }
                        else
                            _session.P_FLEXABRASION_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEXABRASION_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEXABRASION_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEXABRASION_F_TOR.SelectedValue != null)
                {
                    if (cbFLEXABRASION_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEXABRASION_F_TOR = "MIN.";
                    }
                    else if (cbFLEXABRASION_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEXABRASION_F_TOR = "MAX.";
                    }
                    else if (cbFLEXABRASION_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEXABRASION_F_TOR.Text))
                        {
                            _session.P_FLEXABRASION_F_TOR = txtFLEXABRASION_F_TOR.Text;
                        }
                        else
                            _session.P_FLEXABRASION_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEXABRASION_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEXABRASION_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEXABRASION_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEXABRASION_F_NO.Text, out value))
                    {
                        _session.P_FLEXABRASION_F_NO = decimal.Parse(txtFLEXABRASION_F_NO.Text);
                    }
                    else
                    {
                        txtFLEXABRASION_F_NO.Text = string.Empty;
                        _session.P_FLEXABRASION_F_NO = null;
                    }
                }
                else
                    _session.P_FLEXABRASION_F_NO = null;

                #region Foreground

                if (_session.P_FLEXABRASION_F_NO != null)
                {
                    if (_session.P_FLEXABRASION_F_NO > 3)
                        txtFLEXABRASION_F_NO.Foreground = Brushes.Red;
                    else
                        txtFLEXABRASION_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBOWSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBOWSpecification.Text))
                {
                    if (Decimal.TryParse(txtBOWSpecification.Text, out value))
                    {
                        _session.P_BOW = decimal.Parse(txtBOWSpecification.Text);
                    }
                    else
                    {
                        txtBOWSpecification.Text = string.Empty;
                        _session.P_BOW = null;
                    }
                }
                else
                    _session.P_BOW = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbBOW_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBOW_TOR.SelectedValue != null)
                {
                    if (cbBOW_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_BOW_TOR = "MIN.";
                    }
                    else if (cbBOW_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_BOW_TOR = "MAX.";
                    }
                    else if (cbBOW_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtBOW_TOR.Text))
                        {
                            _session.P_BOW_TOR = txtBOW_TOR.Text;
                        }
                        else
                            _session.P_BOW_TOR = string.Empty;
                    }
                }
                else
                    _session.P_BOW_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBOW_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBOW_TOR.SelectedValue != null)
                {
                    if (cbBOW_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_BOW_TOR = "MIN.";
                    }
                    else if (cbBOW_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_BOW_TOR = "MAX.";
                    }
                    else if (cbBOW_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtBOW_TOR.Text))
                        {
                            _session.P_BOW_TOR = txtBOW_TOR.Text;
                        }
                        else
                            _session.P_BOW_TOR = string.Empty;
                    }
                }
                else
                    _session.P_BOW_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBOW_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBOW_NO.Text))
                {
                    if (Decimal.TryParse(txtBOW_NO.Text, out value))
                    {
                        _session.P_BOW_NO = decimal.Parse(txtBOW_NO.Text);
                    }
                    else
                    {
                        txtBOW_NO.Text = string.Empty;
                        _session.P_BOW_NO = null;
                    }
                }
                else
                    _session.P_BOW_NO = null;

                #region Foreground

                if (_session.P_BOW_NO != null)
                {
                    if (_session.P_BOW_NO > 3)
                        txtBOW_NO.Foreground = Brushes.Red;
                    else
                        txtBOW_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSKEWSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSKEWSpecification.Text))
                {
                    if (Decimal.TryParse(txtSKEWSpecification.Text, out value))
                    {
                        _session.P_SKEW = decimal.Parse(txtSKEWSpecification.Text);
                    }
                    else
                    {
                        txtSKEWSpecification.Text = string.Empty;
                        _session.P_SKEW = null;
                    }
                }
                else
                    _session.P_SKEW = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbSKEW_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSKEW_TOR.SelectedValue != null)
                {
                    if (cbSKEW_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_SKEW_TOR = "MIN.";
                    }
                    else if (cbSKEW_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_SKEW_TOR = "MAX.";
                    }
                    else if (cbSKEW_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSKEW_TOR.Text))
                        {
                            _session.P_SKEW_TOR = txtSKEW_TOR.Text;
                        }
                        else
                            _session.P_SKEW_TOR = string.Empty;
                    }
                }
                else
                    _session.P_SKEW_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSKEW_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbSKEW_TOR.SelectedValue != null)
                {
                    if (cbSKEW_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_SKEW_TOR = "MIN.";
                    }
                    else if (cbSKEW_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_SKEW_TOR = "MAX.";
                    }
                    else if (cbSKEW_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtSKEW_TOR.Text))
                        {
                            _session.P_SKEW_TOR = txtSKEW_TOR.Text;
                        }
                        else
                            _session.P_SKEW_TOR = string.Empty;
                    }
                }
                else
                    _session.P_SKEW_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtSKEW_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSKEW_NO.Text))
                {
                    if (Decimal.TryParse(txtSKEW_NO.Text, out value))
                    {
                        _session.P_SKEW_NO = decimal.Parse(txtSKEW_NO.Text);
                    }
                    else
                    {
                        txtSKEW_NO.Text = string.Empty;
                        _session.P_SKEW_NO = null;
                    }
                }
                else
                    _session.P_SKEW_NO = null;

                #region Foreground

                if (_session.P_SKEW_NO != null)
                {
                    if (_session.P_SKEW_NO > 3)
                        txtSKEW_NO.Foreground = Brushes.Red;
                    else
                        txtSKEW_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        //Update 07/07/18

        private void txtBENDING_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBENDING_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtBENDING_WSpecification.Text, out value))
                    {
                        _session.P_BENDING_W = decimal.Parse(txtBENDING_WSpecification.Text);
                    }
                    else
                    {
                        txtBENDING_WSpecification.Text = string.Empty;
                        _session.P_BENDING_W = null;
                    }
                }
                else
                    _session.P_BENDING_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbBENDING_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBENDING_W_TOR.SelectedValue != null)
                {
                    if (cbBENDING_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_BENDING_W_TOR = "MIN.";
                    }
                    else if (cbBENDING_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_BENDING_W_TOR = "MAX.";
                    }
                    else if (cbBENDING_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtBENDING_W_TOR.Text))
                        {
                            _session.P_BENDING_W_TOR = txtBENDING_W_TOR.Text;
                        }
                        else
                            _session.P_BENDING_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_BENDING_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBENDING_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBENDING_W_TOR.SelectedValue != null)
                {
                    if (cbBENDING_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_BENDING_W_TOR = "MIN.";
                    }
                    else if (cbBENDING_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_BENDING_W_TOR = "MAX.";
                    }
                    else if (cbBENDING_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtBENDING_W_TOR.Text))
                        {
                            _session.P_BENDING_W_TOR = txtBENDING_W_TOR.Text;
                        }
                        else
                            _session.P_BENDING_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_BENDING_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBENDING_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBENDING_W_NO.Text))
                {
                    if (Decimal.TryParse(txtBENDING_W_NO.Text, out value))
                    {
                        _session.P_BENDING_W_NO = decimal.Parse(txtBENDING_W_NO.Text);
                    }
                    else
                    {
                        txtBENDING_W_NO.Text = string.Empty;
                        _session.P_BENDING_W_NO = null;
                    }
                }
                else
                    _session.P_BENDING_W_NO = null;

                #region Foreground

                if (_session.P_BENDING_W_NO != null)
                {
                    if (_session.P_BENDING_W_NO > 3)
                        txtBENDING_W_NO.Foreground = Brushes.Red;
                    else
                        txtBENDING_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBENDING_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBENDING_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtBENDING_FSpecification.Text, out value))
                    {
                        _session.P_BENDING_F = decimal.Parse(txtBENDING_FSpecification.Text);
                    }
                    else
                    {
                        txtBENDING_FSpecification.Text = string.Empty;
                        _session.P_BENDING_F = null;
                    }
                }
                else
                    _session.P_BENDING_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbBENDING_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBENDING_F_TOR.SelectedValue != null)
                {
                    if (cbBENDING_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_BENDING_F_TOR = "MIN.";
                    }
                    else if (cbBENDING_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_BENDING_F_TOR = "MAX.";
                    }
                    else if (cbBENDING_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtBENDING_F_TOR.Text))
                        {
                            _session.P_BENDING_F_TOR = txtBENDING_F_TOR.Text;
                        }
                        else
                            _session.P_BENDING_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_BENDING_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBENDING_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbBENDING_F_TOR.SelectedValue != null)
                {
                    if (cbBENDING_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_BENDING_F_TOR = "MIN.";
                    }
                    else if (cbBENDING_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_BENDING_F_TOR = "MAX.";
                    }
                    else if (cbBENDING_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtBENDING_F_TOR.Text))
                        {
                            _session.P_BENDING_F_TOR = txtBENDING_F_TOR.Text;
                        }
                        else
                            _session.P_BENDING_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_BENDING_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtBENDING_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtBENDING_F_NO.Text))
                {
                    if (Decimal.TryParse(txtBENDING_F_NO.Text, out value))
                    {
                        _session.P_BENDING_F_NO = decimal.Parse(txtBENDING_F_NO.Text);
                    }
                    else
                    {
                        txtBENDING_F_NO.Text = string.Empty;
                        _session.P_BENDING_F_NO = null;
                    }
                }
                else
                    _session.P_BENDING_F_NO = null;

                #region Foreground

                if (_session.P_BENDING_F_NO != null)
                {
                    if (_session.P_BENDING_F_NO > 3)
                        txtBENDING_F_NO.Foreground = Brushes.Red;
                    else
                        txtBENDING_F_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEX_SCOTT_WSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_WSpecification.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_WSpecification.Text, out value))
                    {
                        _session.P_FLEX_SCOTT_W = decimal.Parse(txtFLEX_SCOTT_WSpecification.Text);
                    }
                    else
                    {
                        txtFLEX_SCOTT_WSpecification.Text = string.Empty;
                        _session.P_FLEX_SCOTT_W = null;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_W = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbFLEX_SCOTT_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEX_SCOTT_W_TOR.SelectedValue != null)
                {
                    if (cbFLEX_SCOTT_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEX_SCOTT_W_TOR = "MIN.";
                    }
                    else if (cbFLEX_SCOTT_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEX_SCOTT_W_TOR = "MAX.";
                    }
                    else if (cbFLEX_SCOTT_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEX_SCOTT_W_TOR.Text))
                        {
                            _session.P_FLEX_SCOTT_W_TOR = txtFLEX_SCOTT_W_TOR.Text;
                        }
                        else
                            _session.P_FLEX_SCOTT_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEX_SCOTT_W_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEX_SCOTT_W_TOR.SelectedValue != null)
                {
                    if (cbFLEX_SCOTT_W_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEX_SCOTT_W_TOR = "MIN.";
                    }
                    else if (cbFLEX_SCOTT_W_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEX_SCOTT_W_TOR = "MAX.";
                    }
                    else if (cbFLEX_SCOTT_W_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEX_SCOTT_W_TOR.Text))
                        {
                            _session.P_FLEX_SCOTT_W_TOR = txtFLEX_SCOTT_W_TOR.Text;
                        }
                        else
                            _session.P_FLEX_SCOTT_W_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_W_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEX_SCOTT_W_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_W_NO.Text, out value))
                    {
                        _session.P_FLEX_SCOTT_W_NO = decimal.Parse(txtFLEX_SCOTT_W_NO.Text);
                    }
                    else
                    {
                        txtFLEX_SCOTT_W_NO.Text = string.Empty;
                        _session.P_FLEX_SCOTT_W_NO = null;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_W_NO = null;

                #region Foreground

                if (_session.P_FLEX_SCOTT_W_NO != null)
                {
                    if (_session.P_FLEX_SCOTT_W_NO > 3)
                        txtFLEX_SCOTT_W_NO.Foreground = Brushes.Red;
                    else
                        txtFLEX_SCOTT_W_NO.Foreground = Brushes.Black;
                }

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEX_SCOTT_FSpecification_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_FSpecification.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_FSpecification.Text, out value))
                    {
                        _session.P_FLEX_SCOTT_F = decimal.Parse(txtFLEX_SCOTT_FSpecification.Text);
                    }
                    else
                    {
                        txtFLEX_SCOTT_FSpecification.Text = string.Empty;
                        _session.P_FLEX_SCOTT_F = null;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_F = null;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void cbFLEX_SCOTT_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEX_SCOTT_F_TOR.SelectedValue != null)
                {
                    if (cbFLEX_SCOTT_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEX_SCOTT_F_TOR = "MIN.";
                    }
                    else if (cbFLEX_SCOTT_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEX_SCOTT_F_TOR = "MAX.";
                    }
                    else if (cbFLEX_SCOTT_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEX_SCOTT_F_TOR.Text))
                        {
                            _session.P_FLEX_SCOTT_F_TOR = txtFLEX_SCOTT_F_TOR.Text;
                        }
                        else
                            _session.P_FLEX_SCOTT_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEX_SCOTT_F_TOR_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbFLEX_SCOTT_F_TOR.SelectedValue != null)
                {
                    if (cbFLEX_SCOTT_F_TOR.SelectedValue.ToString() == "MIN.")
                    {
                        _session.P_FLEX_SCOTT_F_TOR = "MIN.";
                    }
                    else if (cbFLEX_SCOTT_F_TOR.SelectedValue.ToString() == "MAX.")
                    {
                        _session.P_FLEX_SCOTT_F_TOR = "MAX.";
                    }
                    else if (cbFLEX_SCOTT_F_TOR.SelectedValue.ToString() == "Number")
                    {
                        if (!string.IsNullOrEmpty(txtFLEX_SCOTT_F_TOR.Text))
                        {
                            _session.P_FLEX_SCOTT_F_TOR = txtFLEX_SCOTT_F_TOR.Text;
                        }
                        else
                            _session.P_FLEX_SCOTT_F_TOR = string.Empty;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_F_TOR = string.Empty;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtFLEX_SCOTT_F_NO_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_F_NO.Text, out value))
                    {
                        _session.P_FLEX_SCOTT_F_NO = decimal.Parse(txtFLEX_SCOTT_F_NO.Text);
                    }
                    else
                    {
                        txtFLEX_SCOTT_F_NO.Text = string.Empty;
                        _session.P_FLEX_SCOTT_F_NO = null;
                    }
                }
                else
                    _session.P_FLEX_SCOTT_F_NO = null;

                #region Foreground

                if (_session.P_FLEX_SCOTT_F_NO != null)
                {
                    if (_session.P_FLEX_SCOTT_F_NO > 3)
                        txtFLEX_SCOTT_F_NO.Foreground = Brushes.Red;
                    else
                        txtFLEX_SCOTT_F_NO.Foreground = Brushes.Black;
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

        #region ComboBox

        #region cbItemCode_SelectionChanged
        private void cbItemCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbItemCode.SelectedValue != null)
            {
                SearchData();

                _session.P_ITMCODE = cbItemCode.SelectedValue.ToString();
            }
            else
            {
                _session.P_ITMCODE = string.Empty;
                ClearControl();
            }

            txtUSEWIDTH_TOR.IsEnabled = false;
            txtWIDTHSILICONE_TOR.IsEnabled = false;
            txtMAXFORCE_W_TOR.IsEnabled = false;
            txtMAXFORCE_F_TOR.IsEnabled = false;

            txtELOGATION_W_TOR.IsEnabled = false;
            txtELOGATION_F_TOR.IsEnabled = false;

            txtFLAMMABILITY_F_TOR.IsEnabled = false;
            txtFLAMMABILITY_W_TOR.IsEnabled = false;
            txtEDGECOMB_W_TOR.IsEnabled = false;
            txtEDGECOMB_F_TOR.IsEnabled = false;
            txtSTIFFNESS_F_TOR.IsEnabled = false;
            txtSTIFFNESS_W_TOR.IsEnabled = false;
            txtTEAR_W_TOR.IsEnabled = false;
            txtTEAR_F_TOR.IsEnabled = false;
            txtSTATIC_AIR_TOR.IsEnabled = false;
            txtDIMENSCHANGE_F_TOR.IsEnabled = false;
            txtDIMENSCHANGE_W_TOR.IsEnabled = false;
            txtFLEXABRASION_F_TOR.IsEnabled = false;
            txtFLEXABRASION_W_TOR.IsEnabled = false;
            txtBOW_TOR.IsEnabled = false;
            txtSKEW_TOR.IsEnabled = false;

            chk_TextEnabled(false);
        }
        #endregion

        #region cbUSEWIDTH_TOR_SelectionChanged
        private void cbUSEWIDTH_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbUSEWIDTH_TOR.SelectedValue != null)
            {
                if (cbUSEWIDTH_TOR.SelectedIndex == 0)
                {
                    txtUSEWIDTH_TOR.IsEnabled = true;
                }
                else
                {
                    txtUSEWIDTH_TOR.Text = string.Empty;
                    txtUSEWIDTH_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtUSEWIDTH_TOR.Text = string.Empty;
                txtUSEWIDTH_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbWIDTHSILICONE_TOR_SelectionChanged
        private void cbWIDTHSILICONE_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbWIDTHSILICONE_TOR.SelectedValue != null)
            {
                if (cbWIDTHSILICONE_TOR.SelectedIndex == 0)
                {
                    txtWIDTHSILICONE_TOR.IsEnabled = true;
                }
                else
                {
                    txtWIDTHSILICONE_TOR.Text = string.Empty;
                    txtWIDTHSILICONE_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtWIDTHSILICONE_TOR.Text = string.Empty;
                txtWIDTHSILICONE_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbMAXFORCE_W_TOR_SelectionChanged
        private void cbMAXFORCE_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMAXFORCE_W_TOR.SelectedValue != null)
            {
                if (cbMAXFORCE_W_TOR.SelectedIndex == 0)
                {
                    txtMAXFORCE_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtMAXFORCE_W_TOR.Text = string.Empty;
                    txtMAXFORCE_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtMAXFORCE_W_TOR.Text = string.Empty;
                txtMAXFORCE_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbMAXFORCE_F_TOR_SelectionChanged
        private void cbMAXFORCE_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMAXFORCE_F_TOR.SelectedValue != null)
            {
                if (cbMAXFORCE_F_TOR.SelectedIndex == 0)
                {
                    txtMAXFORCE_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtMAXFORCE_F_TOR.Text = string.Empty;
                    txtMAXFORCE_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtMAXFORCE_F_TOR.Text = string.Empty;
                txtMAXFORCE_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbELOGATION_W_TOR_SelectionChanged
        private void cbELOGATION_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbELOGATION_W_TOR.SelectedValue != null)
            {
                if (cbELOGATION_W_TOR.SelectedIndex == 0)
                {
                    txtELOGATION_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtELOGATION_W_TOR.Text = string.Empty;
                    txtELOGATION_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtELOGATION_W_TOR.Text = string.Empty;
                txtELOGATION_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbELOGATION_F_TOR_SelectionChanged
        private void cbELOGATION_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbELOGATION_F_TOR.SelectedValue != null)
            {
                if (cbELOGATION_F_TOR.SelectedIndex == 0)
                {
                    txtELOGATION_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtELOGATION_F_TOR.Text = string.Empty;
                    txtELOGATION_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtELOGATION_F_TOR.Text = string.Empty;
                txtELOGATION_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbFLAMMABILITY_W_TOR_SelectionChanged
        private void cbFLAMMABILITY_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFLAMMABILITY_W_TOR.SelectedValue != null)
            {
                if (cbFLAMMABILITY_W_TOR.SelectedIndex == 0)
                {
                    txtFLAMMABILITY_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtFLAMMABILITY_W_TOR.Text = string.Empty;
                    txtFLAMMABILITY_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtFLAMMABILITY_W_TOR.Text = string.Empty;
                txtFLAMMABILITY_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbFLAMMABILITY_F_TOR_SelectionChanged
        private void cbFLAMMABILITY_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFLAMMABILITY_F_TOR.SelectedValue != null)
            {
                if (cbFLAMMABILITY_F_TOR.SelectedIndex == 0)
                {
                    txtFLAMMABILITY_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtFLAMMABILITY_F_TOR.Text = string.Empty;
                    txtFLAMMABILITY_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtFLAMMABILITY_F_TOR.Text = string.Empty;
                txtFLAMMABILITY_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbEDGECOMB_W_TOR_SelectionChanged
        private void cbEDGECOMB_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEDGECOMB_W_TOR.SelectedValue != null)
            {
                if (cbEDGECOMB_W_TOR.SelectedIndex == 0)
                {
                    txtEDGECOMB_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtEDGECOMB_W_TOR.Text = string.Empty;
                    txtEDGECOMB_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtEDGECOMB_W_TOR.Text = string.Empty;
                txtEDGECOMB_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbEDGECOMB_F_TOR_SelectionChanged
        private void cbEDGECOMB_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEDGECOMB_F_TOR.SelectedValue != null)
            {
                if (cbEDGECOMB_F_TOR.SelectedIndex == 0)
                {
                    txtEDGECOMB_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtEDGECOMB_F_TOR.Text = string.Empty;
                    txtEDGECOMB_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtEDGECOMB_F_TOR.Text = string.Empty;
                txtEDGECOMB_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbSTIFFNESS_W_TOR_SelectionChanged
        private void cbSTIFFNESS_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSTIFFNESS_W_TOR.SelectedValue != null)
            {
                if (cbSTIFFNESS_W_TOR.SelectedIndex == 0)
                {
                    txtSTIFFNESS_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtSTIFFNESS_W_TOR.Text = string.Empty;
                    txtSTIFFNESS_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtSTIFFNESS_W_TOR.Text = string.Empty;
                txtSTIFFNESS_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbSTIFFNESS_F_TOR_SelectionChanged
        private void cbSTIFFNESS_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSTIFFNESS_F_TOR.SelectedValue != null)
            {
                if (cbSTIFFNESS_F_TOR.SelectedIndex == 0)
                {
                    txtSTIFFNESS_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtSTIFFNESS_F_TOR.Text = string.Empty;
                    txtSTIFFNESS_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtSTIFFNESS_F_TOR.Text = string.Empty;
                txtSTIFFNESS_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbTEAR_W_TOR_SelectionChanged
        private void cbTEAR_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTEAR_W_TOR.SelectedValue != null)
            {
                if (cbTEAR_W_TOR.SelectedIndex == 0)
                {
                    txtTEAR_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtTEAR_W_TOR.Text = string.Empty;
                    txtTEAR_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtTEAR_W_TOR.Text = string.Empty;
                txtTEAR_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbTEAR_F_TOR_SelectionChanged
        private void cbTEAR_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTEAR_F_TOR.SelectedValue != null)
            {
                if (cbTEAR_F_TOR.SelectedIndex == 0)
                {
                    txtTEAR_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtTEAR_F_TOR.Text = string.Empty;
                    txtTEAR_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtTEAR_F_TOR.Text = string.Empty;
                txtTEAR_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbSTATIC_AIR_TOR_SelectionChanged
        private void cbSTATIC_AIR_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSTATIC_AIR_TOR.SelectedValue != null)
            {
                if (cbSTATIC_AIR_TOR.SelectedIndex == 0)
                {
                    txtSTATIC_AIR_TOR.IsEnabled = true;
                }
                else
                {
                    txtSTATIC_AIR_TOR.Text = string.Empty;
                    txtSTATIC_AIR_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtSTATIC_AIR_TOR.Text = string.Empty;
                txtSTATIC_AIR_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbDIMENSCHANGE_W_TOR_SelectionChanged
        private void cbDIMENSCHANGE_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDIMENSCHANGE_W_TOR.SelectedValue != null)
            {
                if (cbDIMENSCHANGE_W_TOR.SelectedIndex == 0)
                {
                    txtDIMENSCHANGE_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtDIMENSCHANGE_W_TOR.Text = string.Empty;
                    txtDIMENSCHANGE_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtDIMENSCHANGE_W_TOR.Text = string.Empty;
                txtDIMENSCHANGE_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbDIMENSCHANGE_F_TOR_SelectionChanged
        private void cbDIMENSCHANGE_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDIMENSCHANGE_F_TOR.SelectedValue != null)
            {
                if (cbDIMENSCHANGE_F_TOR.SelectedIndex == 0)
                {
                    txtDIMENSCHANGE_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtDIMENSCHANGE_F_TOR.Text = string.Empty;
                    txtDIMENSCHANGE_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtDIMENSCHANGE_F_TOR.Text = string.Empty;
                txtDIMENSCHANGE_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbFLEXABRASION_W_TOR_SelectionChanged
        private void cbFLEXABRASION_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFLEXABRASION_W_TOR.SelectedValue != null)
            {
                if (cbFLEXABRASION_W_TOR.SelectedIndex == 0)
                {
                    txtFLEXABRASION_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtFLEXABRASION_W_TOR.Text = string.Empty;
                    txtFLEXABRASION_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtFLEXABRASION_W_TOR.Text = string.Empty;
                txtFLEXABRASION_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbFLEXABRASION_F_TOR_SelectionChanged
        private void cbFLEXABRASION_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFLEXABRASION_F_TOR.SelectedValue != null)
            {
                if (cbFLEXABRASION_F_TOR.SelectedIndex == 0)
                {
                    txtFLEXABRASION_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtFLEXABRASION_F_TOR.Text = string.Empty;
                    txtFLEXABRASION_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtFLEXABRASION_F_TOR.Text = string.Empty;
                txtFLEXABRASION_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbBOW_TOR_SelectionChanged
        private void cbBOW_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBOW_TOR.SelectedValue != null)
            {
                if (cbBOW_TOR.SelectedIndex == 0)
                {
                    txtBOW_TOR.IsEnabled = true;
                }
                else
                {
                    txtBOW_TOR.Text = string.Empty;
                    txtBOW_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtBOW_TOR.Text = string.Empty;
                txtBOW_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbSKEW_TOR_SelectionChanged
        private void cbSKEW_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSKEW_TOR.SelectedValue != null)
            {
                if (cbSKEW_TOR.SelectedIndex == 0)
                {
                    txtSKEW_TOR.IsEnabled = true;
                }
                else
                {
                    txtSKEW_TOR.Text = string.Empty;
                    txtSKEW_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtSKEW_TOR.Text = string.Empty;
                txtSKEW_TOR.IsEnabled = false;
            }
        }
        #endregion

        //Update 07/07/18
        #region cbBENDING_W_TOR_SelectionChanged
        private void cbBENDING_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBENDING_W_TOR.SelectedValue != null)
            {
                if (cbBENDING_W_TOR.SelectedIndex == 0)
                {
                    txtBENDING_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtBENDING_W_TOR.Text = string.Empty;
                    txtBENDING_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtBENDING_W_TOR.Text = string.Empty;
                txtBENDING_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbBENDING_F_TOR_SelectionChanged
        private void cbBENDING_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBENDING_F_TOR.SelectedValue != null)
            {
                if (cbBENDING_F_TOR.SelectedIndex == 0)
                {
                    txtBENDING_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtBENDING_F_TOR.Text = string.Empty;
                    txtBENDING_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtBENDING_F_TOR.Text = string.Empty;
                txtBENDING_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbFLEX_SCOTT_W_TOR_SelectionChanged
        private void cbFLEX_SCOTT_W_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFLEX_SCOTT_W_TOR.SelectedValue != null)
            {
                if (cbFLEX_SCOTT_W_TOR.SelectedIndex == 0)
                {
                    txtFLEX_SCOTT_W_TOR.IsEnabled = true;
                }
                else
                {
                    txtFLEX_SCOTT_W_TOR.Text = string.Empty;
                    txtFLEX_SCOTT_W_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtFLEX_SCOTT_W_TOR.Text = string.Empty;
                txtFLEX_SCOTT_W_TOR.IsEnabled = false;
            }
        }
        #endregion

        #region cbFLEX_SCOTT_F_TOR_SelectionChanged
        private void cbFLEX_SCOTT_F_TOR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFLEX_SCOTT_F_TOR.SelectedValue != null)
            {
                if (cbFLEX_SCOTT_F_TOR.SelectedIndex == 0)
                {
                    txtFLEX_SCOTT_F_TOR.IsEnabled = true;
                }
                else
                {
                    txtFLEX_SCOTT_F_TOR.Text = string.Empty;
                    txtFLEX_SCOTT_F_TOR.IsEnabled = false;
                }
            }
            else
            {
                txtFLEX_SCOTT_F_TOR.Text = string.Empty;
                txtFLEX_SCOTT_F_TOR.IsEnabled = false;
            }
        }
        #endregion

        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            _session = new LAB_INSERTUPDATEITEMSPEC();

            #region NO
            txtWIDTH_NO.Text = string.Empty;
            txtUSEWIDTH_NO.Text = string.Empty;
            txtWIDTHSILICONE_NO.Text = string.Empty;
            txtNUMTHREADS_F_NO.Text = string.Empty;
            txtNUMTHREADS_W_NO.Text = string.Empty;
            txtTOTALWEIGHT_NO.Text = string.Empty;
            txtUNCOATEDWEIGHT_NO.Text = string.Empty;
            txtCOATWEIGHT_NO.Text = string.Empty;
            txtTHICKNESS_NO.Text = string.Empty;
            txtMAXFORCE_W_NO.Text = string.Empty;
            txtMAXFORCE_F_NO.Text = string.Empty;
            txtELOGATION_W_NO.Text = string.Empty;
            txtELOGATION_F_NO.Text = string.Empty;
            txtFLAMMABILITY_F_NO.Text = string.Empty;
            txtFLAMMABILITY_W_NO.Text = string.Empty;

            txtEDGECOMB_F_NO.Text = string.Empty;
            txtEDGECOMB_W_NO.Text = string.Empty;

            txtSTIFFNESS_F_NO.Text = string.Empty;
            txtSTIFFNESS_W_NO.Text = string.Empty;
            txtTEAR_W_NO.Text = string.Empty;
            txtTEAR_F_NO.Text = string.Empty;
            txtSTATIC_AIR_NO.Text = string.Empty;
            txtDYNAMIC_AIR_NO.Text = string.Empty;
            txtEXPONENT_NO.Text = string.Empty;
            txtDIMENSCHANGE_F_NO.Text = string.Empty;
            txtDIMENSCHANGE_W_NO.Text = string.Empty;
            txtFLEXABRASION_F_NO.Text = string.Empty;
            txtFLEXABRASION_W_NO.Text = string.Empty;
            txtBOW_NO.Text = string.Empty;
            txtSKEW_NO.Text = string.Empty;

            txtBENDING_F_NO.Text = string.Empty;
            txtBENDING_W_NO.Text = string.Empty;
            txtFLEX_SCOTT_F_NO.Text = string.Empty;
            txtFLEX_SCOTT_W_NO.Text = string.Empty;
            #endregion

            #region Specification
            txtWIDTHSpecification.Text = string.Empty;
            txtUSEWIDTHSpecification.Text = string.Empty;
            txtWIDTHSILICONESpecification.Text = string.Empty;
            txtNUMTHREADS_FSpecification.Text = string.Empty;
            txtNUMTHREADS_WSpecification.Text = string.Empty;
            txtTOTALWEIGHTSpecification.Text = string.Empty;
            txtUNCOATEDWEIGHTSpecification.Text = string.Empty;
            txtCOATINGWEIGHTSpecification.Text = string.Empty;
            txtTHICKNESSSpecification.Text = string.Empty;
            txtMAXFORCE_WSpecification.Text = string.Empty;
            txtMAXFORCE_FSpecification.Text = string.Empty;
            txtELOGATION_WSpecification.Text = string.Empty;
            txtELOGATION_FSpecification.Text = string.Empty;
            txtFLAMMABILITY_FSpecification.Text = string.Empty;
            txtFLAMMABILITY_WSpecification.Text = string.Empty;
            txtEDGECOMB_WSpecification.Text = string.Empty;
            txtEDGECOMB_FSpecification.Text = string.Empty;
            txtSTIFFNES_FSpecification.Text = string.Empty;
            txtSTIFFNES_WSpecification.Text = string.Empty;
            txtTEAR_FSpecification.Text = string.Empty;
            txtTEAR_WSpecification.Text = string.Empty;
            txtSTATIC_AIRSpecification.Text = string.Empty;
            txtDYNAMIC_AIRSpecification.Text = string.Empty;
            txtEXPONENTSpecification.Text = string.Empty;
            txtDIMENSCHANGE_FSpecification.Text = string.Empty;
            txtDIMENSCHANGE_WSpecification.Text = string.Empty;
            txtFLEXABRASION_FSpecification.Text = string.Empty;
            txtFLEXABRASION_WSpecification.Text = string.Empty;
            txtBOWSpecification.Text = string.Empty;
            txtSKEWSpecification.Text = string.Empty;

            txtBENDING_FSpecification.Text = string.Empty;
            txtBENDING_WSpecification.Text = string.Empty;
            txtFLEX_SCOTT_FSpecification.Text = string.Empty;
            txtFLEX_SCOTT_WSpecification.Text = string.Empty;
            #endregion

            #region TOR

            txtUSEWIDTH_TOR.Text = string.Empty;
            txtWIDTHSILICONE_TOR.Text = string.Empty;
            txtNUMTHREADS_F_TOR.Text = string.Empty;
            txtNUMTHREADS_W_TOR.Text = string.Empty;
            txtTOTALWEIGHT_TOR.Text = string.Empty;
            txtUNCOATEDWEIGHT_TOR.Text = string.Empty;
            txtCOATWEIGHT_TOR.Text = string.Empty;
            txtTHICKNESS_TOR.Text = string.Empty;
            txtMAXFORCE_W_TOR.Text = string.Empty;
            txtMAXFORCE_F_TOR.Text = string.Empty;

            txtELOGATION_W_TOR.Text = string.Empty;
            txtELOGATION_F_TOR.Text = string.Empty;

            txtFLAMMABILITY_F_TOR.Text = string.Empty;
            txtFLAMMABILITY_W_TOR.Text = string.Empty;

            txtEDGECOMB_F_TOR.Text = string.Empty;
            txtEDGECOMB_W_TOR.Text = string.Empty;

            txtSTIFFNESS_F_TOR.Text = string.Empty;
            txtSTIFFNESS_W_TOR.Text = string.Empty;
            txtTEAR_W_TOR.Text = string.Empty;
            txtTEAR_F_TOR.Text = string.Empty;
            txtSTATIC_AIR_TOR.Text = string.Empty;
            txtDYNAMIC_AIR_TOR.Text = string.Empty;
            txtEXPONENT_TOR.Text = string.Empty;
            txtDIMENSCHANGE_F_TOR.Text = string.Empty;
            txtDIMENSCHANGE_W_TOR.Text = string.Empty;
            txtFLEXABRASION_F_TOR.Text = string.Empty;
            txtFLEXABRASION_W_TOR.Text = string.Empty;
            txtBOW_TOR.Text = string.Empty;
            txtSKEW_TOR.Text = string.Empty;

            txtBENDING_F_TOR.Text = string.Empty;
            txtBENDING_W_TOR.Text = string.Empty;
            txtFLEX_SCOTT_F_TOR.Text = string.Empty;
            txtFLEX_SCOTT_W_TOR.Text = string.Empty;

            #endregion

            #region Combo TOR

            this.cbUSEWIDTH_TOR.Text = "";
            cbUSEWIDTH_TOR.SelectedItem = null;

            //if (cbUSEWIDTH_TOR.SelectedValue != null)
            //    cbUSEWIDTH_TOR.SelectedIndex = 0;

            this.cbWIDTHSILICONE_TOR.Text = "";
            cbWIDTHSILICONE_TOR.SelectedItem = null;

            this.cbMAXFORCE_W_TOR.Text = "";
            cbMAXFORCE_W_TOR.SelectedItem = null;

            this.cbMAXFORCE_F_TOR.Text = "";
            cbMAXFORCE_F_TOR.SelectedItem = null;

            this.cbELOGATION_W_TOR.Text = "";
            cbELOGATION_W_TOR.SelectedItem = null;

            this.cbELOGATION_F_TOR.Text = "";
            cbELOGATION_F_TOR.SelectedItem = null;

            this.cbFLAMMABILITY_W_TOR.Text = "";
            cbFLAMMABILITY_W_TOR.SelectedItem = null;

            this.cbFLAMMABILITY_F_TOR.Text = "";
            cbFLAMMABILITY_F_TOR.SelectedItem = null;

            this.cbEDGECOMB_W_TOR.Text = "";
            cbEDGECOMB_W_TOR.SelectedItem = null;

            this.cbEDGECOMB_F_TOR.Text = "";
            cbEDGECOMB_F_TOR.SelectedItem = null;

            this.cbSTIFFNESS_W_TOR.Text = "";
            cbSTIFFNESS_W_TOR.SelectedItem = null;

            this.cbSTIFFNESS_F_TOR.Text = "";
            cbSTIFFNESS_F_TOR.SelectedItem = null;

            this.cbTEAR_W_TOR.Text = "";
            cbTEAR_W_TOR.SelectedItem = null;

            this.cbTEAR_F_TOR.Text = "";
            cbTEAR_F_TOR.SelectedItem = null;

            this.cbSTATIC_AIR_TOR.Text = "";
            cbSTATIC_AIR_TOR.SelectedItem = null;

            this.cbDIMENSCHANGE_W_TOR.Text = "";
            cbDIMENSCHANGE_W_TOR.SelectedItem = null;

            this.cbDIMENSCHANGE_F_TOR.Text = "";
            cbDIMENSCHANGE_F_TOR.SelectedItem = null;

            this.cbFLEXABRASION_W_TOR.Text = "";
            cbFLEXABRASION_W_TOR.SelectedItem = null;

            this.cbFLEXABRASION_F_TOR.Text = "";
            cbFLEXABRASION_F_TOR.SelectedItem = null;

            this.cbBOW_TOR.Text = "";
            cbBOW_TOR.SelectedItem = null;

            this.cbSKEW_TOR.Text = "";
            cbSKEW_TOR.SelectedItem = null;

            this.cbBENDING_F_TOR.Text = "";
            cbBENDING_F_TOR.SelectedItem = null;

            this.cbBENDING_W_TOR.Text = "";
            cbBENDING_W_TOR.SelectedItem = null;

            this.cbFLEX_SCOTT_F_TOR.Text = "";
            cbFLEX_SCOTT_F_TOR.SelectedItem = null;

            this.cbFLEX_SCOTT_W_TOR.Text = "";
            cbFLEX_SCOTT_W_TOR.SelectedItem = null;

            //Update 07/07/18
            this.cbBENDING_F_TOR.Text = "";
            cbBENDING_F_TOR.SelectedItem = null;

            this.cbBENDING_W_TOR.Text = "";
            cbBENDING_W_TOR.SelectedItem = null;

            this.cbFLEX_SCOTT_F_TOR.Text = "";
            cbFLEX_SCOTT_F_TOR.SelectedItem = null;

            this.cbFLEX_SCOTT_W_TOR.Text = "";
            cbFLEX_SCOTT_W_TOR.SelectedItem = null;

            #endregion

            #region NO Foreground
            txtWIDTH_NO.Foreground = Brushes.Black;
            txtUSEWIDTH_NO.Foreground = Brushes.Black;
            txtWIDTHSILICONE_NO.Foreground = Brushes.Black;
            txtNUMTHREADS_F_NO.Foreground = Brushes.Black;
            txtNUMTHREADS_W_NO.Foreground = Brushes.Black;
            txtTOTALWEIGHT_NO.Foreground = Brushes.Black;
            txtUNCOATEDWEIGHT_NO.Foreground = Brushes.Black;
            txtCOATWEIGHT_NO.Foreground = Brushes.Black;
            txtTHICKNESS_NO.Foreground = Brushes.Black;
            txtMAXFORCE_W_NO.Foreground = Brushes.Black;
            txtMAXFORCE_F_NO.Foreground = Brushes.Black;
            txtELOGATION_W_NO.Foreground = Brushes.Black;
            txtELOGATION_F_NO.Foreground = Brushes.Black;
            txtFLAMMABILITY_F_NO.Foreground = Brushes.Black;
            txtFLAMMABILITY_W_NO.Foreground = Brushes.Black;

            txtEDGECOMB_F_NO.Foreground = Brushes.Black;
            txtEDGECOMB_W_NO.Foreground = Brushes.Black;

            txtSTIFFNESS_F_NO.Foreground = Brushes.Black;
            txtSTIFFNESS_W_NO.Foreground = Brushes.Black;
            txtTEAR_W_NO.Foreground = Brushes.Black;
            txtTEAR_F_NO.Foreground = Brushes.Black;
            txtSTATIC_AIR_NO.Foreground = Brushes.Black;
            txtDYNAMIC_AIR_NO.Foreground = Brushes.Black;
            txtEXPONENT_NO.Foreground = Brushes.Black;
            txtDIMENSCHANGE_F_NO.Foreground = Brushes.Black;
            txtDIMENSCHANGE_W_NO.Foreground = Brushes.Black;
            txtFLEXABRASION_F_NO.Foreground = Brushes.Black;
            txtFLEXABRASION_W_NO.Foreground = Brushes.Black;
            txtBOW_NO.Foreground = Brushes.Black;
            txtSKEW_NO.Foreground = Brushes.Black;

            txtBENDING_F_NO.Foreground = Brushes.Black;
            txtBENDING_W_NO.Foreground = Brushes.Black;
            txtFLEX_SCOTT_F_NO.Foreground = Brushes.Black;
            txtFLEX_SCOTT_W_NO.Foreground = Brushes.Black;

            #endregion

            chk_TextEnabled(false);
        }

        #endregion

        #region IsEnabled

        #region chk_TextEnabled
        private void chk_TextEnabled(bool chk)
        {
            #region Specification
            txtWIDTHSpecification.IsEnabled = chk;
            txtUSEWIDTHSpecification.IsEnabled = chk;
            txtWIDTHSILICONESpecification.IsEnabled = chk;
            txtNUMTHREADS_FSpecification.IsEnabled = chk;
            txtNUMTHREADS_WSpecification.IsEnabled = chk;
            txtTOTALWEIGHTSpecification.IsEnabled = chk;
            txtUNCOATEDWEIGHTSpecification.IsEnabled = chk;
            txtCOATINGWEIGHTSpecification.IsEnabled = chk;
            txtTHICKNESSSpecification.IsEnabled = chk;
            txtMAXFORCE_WSpecification.IsEnabled = chk;
            txtMAXFORCE_FSpecification.IsEnabled = chk;
            txtELOGATION_WSpecification.IsEnabled = chk;
            txtELOGATION_FSpecification.IsEnabled = chk;
            txtFLAMMABILITY_FSpecification.IsEnabled = chk;
            txtFLAMMABILITY_WSpecification.IsEnabled = chk;
            txtEDGECOMB_WSpecification.IsEnabled = chk;
            txtEDGECOMB_FSpecification.IsEnabled = chk;
            txtSTIFFNES_FSpecification.IsEnabled = chk;
            txtSTIFFNES_WSpecification.IsEnabled = chk;
            txtTEAR_FSpecification.IsEnabled = chk;
            txtTEAR_WSpecification.IsEnabled = chk;
            txtSTATIC_AIRSpecification.IsEnabled = chk;
            txtDYNAMIC_AIRSpecification.IsEnabled = chk;
            txtEXPONENTSpecification.IsEnabled = chk;
            txtDIMENSCHANGE_FSpecification.IsEnabled = chk;
            txtDIMENSCHANGE_WSpecification.IsEnabled = chk;
            txtFLEXABRASION_FSpecification.IsEnabled = chk;
            txtFLEXABRASION_WSpecification.IsEnabled = chk;
            txtBOWSpecification.IsEnabled = chk;
            txtSKEWSpecification.IsEnabled = chk;

            //Update 07/07/18
            txtBENDING_FSpecification.IsEnabled = chk;
            txtBENDING_WSpecification.IsEnabled = chk;
            txtFLEX_SCOTT_FSpecification.IsEnabled = chk;
            txtFLEX_SCOTT_WSpecification.IsEnabled = chk;

            #endregion

            #region TOR

            //txtUSEWIDTH_TOR.IsEnabled = chk;
            //txtWIDTHSILICONE_TOR.IsEnabled = chk;
            txtNUMTHREADS_F_TOR.IsEnabled = chk;
            txtNUMTHREADS_W_TOR.IsEnabled = chk;
            txtTOTALWEIGHT_TOR.IsEnabled = chk;
            txtUNCOATEDWEIGHT_TOR.IsEnabled = chk;
            txtCOATWEIGHT_TOR.IsEnabled = chk;
            txtTHICKNESS_TOR.IsEnabled = chk;
            //txtMAXFORCE_W_TOR.IsEnabled = chk;
            //txtMAXFORCE_F_TOR.IsEnabled = chk;
            //txtELOGATION_W_TOR.IsEnabled = chk;
            //txtELOGATION_F_TOR.IsEnabled = chk;

            //txtFLAMMABILITY_F_TOR.IsEnabled = chk;
            //txtFLAMMABILITY_W_TOR.IsEnabled = chk;
            //txtEDGECOMB_W_TOR.IsEnabled = chk;
            //txtEDGECOMB_F_TOR.IsEnabled = chk;
            //txtSTIFFNESS_F_TOR.IsEnabled = chk;
            //txtSTIFFNESS_W_TOR.IsEnabled = chk;
            //txtTEAR_W_TOR.IsEnabled = chk;
            //txtTEAR_F_TOR.IsEnabled = chk;
            //txtSTATIC_AIR_TOR.IsEnabled = chk;
            txtDYNAMIC_AIR_TOR.IsEnabled = chk;
            txtEXPONENT_TOR.IsEnabled = chk;
            //txtDIMENSCHANGE_F_TOR.IsEnabled = chk;
            //txtDIMENSCHANGE_W_TOR.IsEnabled = chk;
            //txtFLEXABRASION_F_TOR.IsEnabled = chk;
            //txtFLEXABRASION_W_TOR.IsEnabled = chk;
            //txtBOW_TOR.IsEnabled = chk;
            //txtSKEW_TOR.IsEnabled = chk;

            #endregion

            #region NO
            txtWIDTH_NO.IsEnabled = chk;
            txtUSEWIDTH_NO.IsEnabled = chk;
            txtWIDTHSILICONE_NO.IsEnabled = chk;
            txtNUMTHREADS_F_NO.IsEnabled = chk;
            txtNUMTHREADS_W_NO.IsEnabled = chk;
            txtTOTALWEIGHT_NO.IsEnabled = chk;
            txtUNCOATEDWEIGHT_NO.IsEnabled = chk;
            txtCOATWEIGHT_NO.IsEnabled = chk;
            txtTHICKNESS_NO.IsEnabled = chk;
            txtMAXFORCE_W_NO.IsEnabled = chk;
            txtMAXFORCE_F_NO.IsEnabled = chk;
            txtELOGATION_W_NO.IsEnabled = chk;
            txtELOGATION_F_NO.IsEnabled = chk;
            txtFLAMMABILITY_F_NO.IsEnabled = chk;
            txtFLAMMABILITY_W_NO.IsEnabled = chk;
            txtEDGECOMB_F_NO.IsEnabled = chk;
            txtEDGECOMB_W_NO.IsEnabled = chk;
            txtSTIFFNESS_F_NO.IsEnabled = chk;
            txtSTIFFNESS_W_NO.IsEnabled = chk;
            txtTEAR_W_NO.IsEnabled = chk;
            txtTEAR_F_NO.IsEnabled = chk;
            txtSTATIC_AIR_NO.IsEnabled = chk;
            txtDYNAMIC_AIR_NO.IsEnabled = chk;
            txtEXPONENT_NO.IsEnabled = chk;
            txtDIMENSCHANGE_F_NO.IsEnabled = chk;
            txtDIMENSCHANGE_W_NO.IsEnabled = chk;
            txtFLEXABRASION_F_NO.IsEnabled = chk;
            txtFLEXABRASION_W_NO.IsEnabled = chk;
            txtBOW_NO.IsEnabled = chk;
            txtSKEW_NO.IsEnabled = chk;

            //Update 07/07/18
            txtBENDING_F_NO.IsEnabled = chk;
            txtBENDING_W_NO.IsEnabled = chk;
            txtFLEX_SCOTT_F_NO.IsEnabled = chk;
            txtFLEX_SCOTT_W_NO.IsEnabled = chk;

            #endregion

            #region Combo TOR

            this.cbUSEWIDTH_TOR.IsEnabled = chk;
            this.cbWIDTHSILICONE_TOR.IsEnabled = chk;
            this.cbMAXFORCE_W_TOR.IsEnabled = chk;
            this.cbMAXFORCE_F_TOR.IsEnabled = chk;
            this.cbELOGATION_W_TOR.IsEnabled = chk;
            this.cbELOGATION_F_TOR.IsEnabled = chk;
            this.cbFLAMMABILITY_W_TOR.IsEnabled = chk;
            this.cbFLAMMABILITY_F_TOR.IsEnabled = chk;
            this.cbEDGECOMB_W_TOR.IsEnabled = chk;
            this.cbEDGECOMB_F_TOR.IsEnabled = chk;
            this.cbSTIFFNESS_W_TOR.IsEnabled = chk;
            this.cbSTIFFNESS_F_TOR.IsEnabled = chk;
            this.cbTEAR_W_TOR.IsEnabled = chk;
            this.cbTEAR_F_TOR.IsEnabled = chk;
            this.cbSTATIC_AIR_TOR.IsEnabled = chk;
            this.cbDIMENSCHANGE_W_TOR.IsEnabled = chk;
            this.cbDIMENSCHANGE_F_TOR.IsEnabled = chk;
            this.cbFLEXABRASION_W_TOR.IsEnabled = chk;
            this.cbFLEXABRASION_F_TOR.IsEnabled = chk;
            this.cbBOW_TOR.IsEnabled = chk;
            this.cbSKEW_TOR.IsEnabled = chk;

            //Update 07/07/18
            this.cbBENDING_W_TOR.IsEnabled = chk;
            this.cbBENDING_F_TOR.IsEnabled = chk;
            this.cbFLEX_SCOTT_W_TOR.IsEnabled = chk;
            this.cbFLEX_SCOTT_F_TOR.IsEnabled = chk;

            #endregion

            cmdSave.IsEnabled = chk;
        }
        #endregion

        #endregion

        #region LoadItemCode

        private void LoadItemCode()
        {
            try
            {
                List<ITM_GETITEMCODELIST> items = LabDataPDFDataService.Instance.GETITEMCODELIST();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_CODE";
                this.cbItemCode.SelectedValuePath = "ITM_CODE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadAllowance

        private void LoadAllowance()
        {
            try
            {
                List<LAB_AllowanceProcess> items = LabDataPDFDataService.Instance.LAB_AllowanceProcess();

                this.cbUSEWIDTH_TOR.ItemsSource = items;
                this.cbUSEWIDTH_TOR.DisplayMemberPath = "Allowance";
                this.cbUSEWIDTH_TOR.SelectedValuePath = "Allowance";

                this.cbWIDTHSILICONE_TOR.ItemsSource = items;
                this.cbWIDTHSILICONE_TOR.DisplayMemberPath = "Allowance";
                this.cbWIDTHSILICONE_TOR.SelectedValuePath = "Allowance";

                this.cbMAXFORCE_W_TOR.ItemsSource = items;
                this.cbMAXFORCE_W_TOR.DisplayMemberPath = "Allowance";
                this.cbMAXFORCE_W_TOR.SelectedValuePath = "Allowance";

                this.cbMAXFORCE_F_TOR.ItemsSource = items;
                this.cbMAXFORCE_F_TOR.DisplayMemberPath = "Allowance";
                this.cbMAXFORCE_F_TOR.SelectedValuePath = "Allowance";

                this.cbELOGATION_W_TOR.ItemsSource = items;
                this.cbELOGATION_W_TOR.DisplayMemberPath = "Allowance";
                this.cbELOGATION_W_TOR.SelectedValuePath = "Allowance";

                this.cbELOGATION_F_TOR.ItemsSource = items;
                this.cbELOGATION_F_TOR.DisplayMemberPath = "Allowance";
                this.cbELOGATION_F_TOR.SelectedValuePath = "Allowance";

                this.cbFLAMMABILITY_W_TOR.ItemsSource = items;
                this.cbFLAMMABILITY_W_TOR.DisplayMemberPath = "Allowance";
                this.cbFLAMMABILITY_W_TOR.SelectedValuePath = "Allowance";

                this.cbFLAMMABILITY_F_TOR.ItemsSource = items;
                this.cbFLAMMABILITY_F_TOR.DisplayMemberPath = "Allowance";
                this.cbFLAMMABILITY_F_TOR.SelectedValuePath = "Allowance";

                this.cbEDGECOMB_W_TOR.ItemsSource = items;
                this.cbEDGECOMB_W_TOR.DisplayMemberPath = "Allowance";
                this.cbEDGECOMB_W_TOR.SelectedValuePath = "Allowance";

                this.cbEDGECOMB_F_TOR.ItemsSource = items;
                this.cbEDGECOMB_F_TOR.DisplayMemberPath = "Allowance";
                this.cbEDGECOMB_F_TOR.SelectedValuePath = "Allowance";

                this.cbSTIFFNESS_W_TOR.ItemsSource = items;
                this.cbSTIFFNESS_W_TOR.DisplayMemberPath = "Allowance";
                this.cbSTIFFNESS_W_TOR.SelectedValuePath = "Allowance";

                this.cbSTIFFNESS_F_TOR.ItemsSource = items;
                this.cbSTIFFNESS_F_TOR.DisplayMemberPath = "Allowance";
                this.cbSTIFFNESS_F_TOR.SelectedValuePath = "Allowance";

                this.cbTEAR_W_TOR.ItemsSource = items;
                this.cbTEAR_W_TOR.DisplayMemberPath = "Allowance";
                this.cbTEAR_W_TOR.SelectedValuePath = "Allowance";

                this.cbTEAR_F_TOR.ItemsSource = items;
                this.cbTEAR_F_TOR.DisplayMemberPath = "Allowance";
                this.cbTEAR_F_TOR.SelectedValuePath = "Allowance";

                this.cbSTATIC_AIR_TOR.ItemsSource = items;
                this.cbSTATIC_AIR_TOR.DisplayMemberPath = "Allowance";
                this.cbSTATIC_AIR_TOR.SelectedValuePath = "Allowance";

                this.cbDIMENSCHANGE_W_TOR.ItemsSource = items;
                this.cbDIMENSCHANGE_W_TOR.DisplayMemberPath = "Allowance";
                this.cbDIMENSCHANGE_W_TOR.SelectedValuePath = "Allowance";

                this.cbDIMENSCHANGE_F_TOR.ItemsSource = items;
                this.cbDIMENSCHANGE_F_TOR.DisplayMemberPath = "Allowance";
                this.cbDIMENSCHANGE_F_TOR.SelectedValuePath = "Allowance";

                this.cbFLEXABRASION_W_TOR.ItemsSource = items;
                this.cbFLEXABRASION_W_TOR.DisplayMemberPath = "Allowance";
                this.cbFLEXABRASION_W_TOR.SelectedValuePath = "Allowance";

                this.cbFLEXABRASION_F_TOR.ItemsSource = items;
                this.cbFLEXABRASION_F_TOR.DisplayMemberPath = "Allowance";
                this.cbFLEXABRASION_F_TOR.SelectedValuePath = "Allowance";

                this.cbBOW_TOR.ItemsSource = items;
                this.cbBOW_TOR.DisplayMemberPath = "Allowance";
                this.cbBOW_TOR.SelectedValuePath = "Allowance";

                this.cbSKEW_TOR.ItemsSource = items;
                this.cbSKEW_TOR.DisplayMemberPath = "Allowance";
                this.cbSKEW_TOR.SelectedValuePath = "Allowance";

                //Update 07/07/18
                this.cbBENDING_W_TOR.ItemsSource = items;
                this.cbBENDING_W_TOR.DisplayMemberPath = "Allowance";
                this.cbBENDING_W_TOR.SelectedValuePath = "Allowance";

                this.cbBENDING_F_TOR.ItemsSource = items;
                this.cbBENDING_F_TOR.DisplayMemberPath = "Allowance";
                this.cbBENDING_F_TOR.SelectedValuePath = "Allowance";

                this.cbFLEX_SCOTT_W_TOR.ItemsSource = items;
                this.cbFLEX_SCOTT_W_TOR.DisplayMemberPath = "Allowance";
                this.cbFLEX_SCOTT_W_TOR.SelectedValuePath = "Allowance";

                this.cbFLEX_SCOTT_F_TOR.ItemsSource = items;
                this.cbFLEX_SCOTT_F_TOR.DisplayMemberPath = "Allowance";
                this.cbFLEX_SCOTT_F_TOR.SelectedValuePath = "Allowance";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
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
                        if (results[0].WIDTH != null)
                        {
                            txtWIDTH_NO.Text = results[0].WIDTH.Value.ToString("#,##0.##");
                            _session.P_WIDTH_NO = results[0].WIDTH;

                            #region Foreground

                            if (_session.P_WIDTH_NO != null)
                            {
                                if (_session.P_WIDTH_NO > 1)
                                    txtWIDTH_NO.Foreground = Brushes.Red;
                                else
                                    txtWIDTH_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].USABLE_WIDTH != null)
                        {
                            txtUSEWIDTH_NO.Text = results[0].USABLE_WIDTH.Value.ToString("#,##0.##");
                            _session.P_USEWIDTH_NO = results[0].USABLE_WIDTH;

                            #region Foreground

                            if (_session.P_USEWIDTH_NO != null)
                            {
                                if (_session.P_USEWIDTH_NO > 3)
                                    txtUSEWIDTH_NO.Foreground = Brushes.Red;
                                else
                                    txtUSEWIDTH_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].WIDTH_SILICONE != null)
                        {
                            txtWIDTHSILICONE_NO.Text = results[0].WIDTH_SILICONE.Value.ToString("#,##0.##");
                            _session.P_WIDTHSILICONE_NO = results[0].WIDTH_SILICONE;

                            #region Foreground

                            if (_session.P_WIDTHSILICONE_NO != null)
                            {
                                if (_session.P_WIDTHSILICONE_NO > 3)
                                    txtWIDTHSILICONE_NO.Foreground = Brushes.Red;
                                else
                                    txtWIDTHSILICONE_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].NUMTHREADS_F != null)
                        {
                            txtNUMTHREADS_F_NO.Text = results[0].NUMTHREADS_F.Value.ToString("#,##0.##");
                            _session.P_NUMTHREADS_F_NO = results[0].NUMTHREADS_F;

                            #region Foreground

                            if (_session.P_NUMTHREADS_F_NO != null)
                            {
                                if (_session.P_NUMTHREADS_F_NO > 3)
                                    txtNUMTHREADS_F_NO.Foreground = Brushes.Red;
                                else
                                    txtNUMTHREADS_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].NUMTHREADS_W != null)
                        {
                            txtNUMTHREADS_W_NO.Text = results[0].NUMTHREADS_W.Value.ToString("#,##0.##");
                            _session.P_NUMTHREADS_W_NO = results[0].NUMTHREADS_W;

                            #region Foreground

                            if (_session.P_NUMTHREADS_W_NO != null)
                            {
                                if (_session.P_NUMTHREADS_W_NO > 3)
                                    txtNUMTHREADS_W_NO.Foreground = Brushes.Red;
                                else
                                    txtNUMTHREADS_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].TOTALWEIGHT != null)
                        {
                            txtTOTALWEIGHT_NO.Text = results[0].TOTALWEIGHT.Value.ToString("#,##0.##");
                            _session.P_TOTALWEIGHT_NO = results[0].TOTALWEIGHT;

                            #region Foreground

                            if (_session.P_TOTALWEIGHT_NO != null)
                            {
                                if (_session.P_TOTALWEIGHT_NO > 6)
                                    txtTOTALWEIGHT_NO.Foreground = Brushes.Red;
                                else
                                    txtTOTALWEIGHT_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].UNCOATEDWEIGHT != null)
                        {
                            txtUNCOATEDWEIGHT_NO.Text = results[0].UNCOATEDWEIGHT.Value.ToString("#,##0.##");
                            _session.P_UNCOATEDWEIGHT_NO = results[0].UNCOATEDWEIGHT;

                            #region Foreground

                            if (_session.P_UNCOATEDWEIGHT_NO != null)
                            {
                                if (_session.P_UNCOATEDWEIGHT_NO > 6)
                                    txtUNCOATEDWEIGHT_NO.Foreground = Brushes.Red;
                                else
                                    txtUNCOATEDWEIGHT_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].COATINGWEIGHT != null)
                        {
                            txtCOATWEIGHT_NO.Text = results[0].COATINGWEIGHT.Value.ToString("#,##0.##");
                            _session.P_COATWEIGHT_NO = results[0].COATINGWEIGHT;

                            #region Foreground

                            if (_session.P_COATWEIGHT_NO != null)
                            {
                                if (_session.P_COATWEIGHT_NO > 6)
                                    txtCOATWEIGHT_NO.Foreground = Brushes.Red;
                                else
                                    txtCOATWEIGHT_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].THICKNESS != null)
                        {
                            txtTHICKNESS_NO.Text = results[0].THICKNESS.Value.ToString("#,##0.##");
                            _session.P_THICKNESS_NO = results[0].THICKNESS;

                            #region Foreground

                            if (_session.P_THICKNESS_NO != null)
                            {
                                if (_session.P_THICKNESS_NO > 3)
                                    txtTHICKNESS_NO.Foreground = Brushes.Red;
                                else
                                    txtTHICKNESS_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].MAXFORCE_W != null)
                        {
                            txtMAXFORCE_W_NO.Text = results[0].MAXFORCE_W.Value.ToString("#,##0.##");
                            _session.P_MAXFORCE_W_NO = results[0].MAXFORCE_W;

                            #region Foreground

                            if (_session.P_MAXFORCE_W_NO != null)
                            {
                                if (_session.P_MAXFORCE_W_NO > 6)
                                    txtMAXFORCE_W_NO.Foreground = Brushes.Red;
                                else
                                    txtMAXFORCE_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].MAXFORCE_F != null)
                        {
                            txtMAXFORCE_F_NO.Text = results[0].MAXFORCE_F.Value.ToString("#,##0.##");
                            _session.P_MAXFORCE_F_NO = results[0].MAXFORCE_F;

                            #region Foreground

                            if (_session.P_MAXFORCE_F_NO != null)
                            {
                                if (_session.P_MAXFORCE_F_NO > 6)
                                    txtMAXFORCE_F_NO.Foreground = Brushes.Red;
                                else
                                    txtMAXFORCE_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].ELONGATIONFORCE_W != null)
                        {
                            txtELOGATION_W_NO.Text = results[0].ELONGATIONFORCE_W.Value.ToString("#,##0.##");
                            _session.P_ELOGATION_W_NO = results[0].ELONGATIONFORCE_W;

                            #region Foreground

                            if (_session.P_ELOGATION_W_NO != null)
                            {
                                if (_session.P_ELOGATION_W_NO > 6)
                                    txtELOGATION_W_NO.Foreground = Brushes.Red;
                                else
                                    txtELOGATION_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].ELONGATIONFORCE_F != null)
                        {
                            txtELOGATION_F_NO.Text = results[0].ELONGATIONFORCE_F.Value.ToString("#,##0.##");
                            _session.P_ELOGATION_F_NO = results[0].ELONGATIONFORCE_F;

                            #region Foreground

                            if (_session.P_ELOGATION_F_NO != null)
                            {
                                if (_session.P_ELOGATION_F_NO > 6)
                                    txtELOGATION_F_NO.Foreground = Brushes.Red;
                                else
                                    txtELOGATION_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].FLAMMABILITY_F != null)
                        {
                            txtFLAMMABILITY_F_NO.Text = results[0].FLAMMABILITY_F.Value.ToString("#,##0.##");
                            _session.P_FLAMMABILITY_F_NO = results[0].FLAMMABILITY_F;

                            #region Foreground

                            if (_session.P_FLAMMABILITY_F_NO != null)
                            {
                                if (_session.P_FLAMMABILITY_F_NO > 5)
                                    txtFLAMMABILITY_F_NO.Foreground = Brushes.Red;
                                else
                                    txtFLAMMABILITY_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].FLAMMABILITY_W != null)
                        {
                            txtFLAMMABILITY_W_NO.Text = results[0].FLAMMABILITY_W.Value.ToString("#,##0.##");
                            _session.P_FLAMMABILITY_W_NO = results[0].FLAMMABILITY_W;

                            #region Foreground

                            if (_session.P_FLAMMABILITY_W_NO != null)
                            {
                                if (_session.P_FLAMMABILITY_W_NO > 5)
                                    txtFLAMMABILITY_W_NO.Foreground = Brushes.Red;
                                else
                                    txtFLAMMABILITY_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].EDGECOMB_W != null)
                        {
                            txtEDGECOMB_W_NO.Text = results[0].EDGECOMB_W.Value.ToString("#,##0.##");
                            _session.P_EDGECOMB_W_NO = results[0].EDGECOMB_W;

                            #region Foreground

                            if (_session.P_EDGECOMB_W_NO != null)
                            {
                                if (_session.P_EDGECOMB_W_NO > 3)
                                    txtEDGECOMB_W_NO.Foreground = Brushes.Red;
                                else
                                    txtEDGECOMB_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].EDGECOMB_F != null)
                        {
                            txtEDGECOMB_F_NO.Text = results[0].EDGECOMB_F.Value.ToString("#,##0.##");
                            _session.P_EDGECOMB_F_NO = results[0].EDGECOMB_F;

                            #region Foreground

                            if (_session.P_EDGECOMB_F_NO != null)
                            {
                                if (_session.P_EDGECOMB_F_NO > 3)
                                    txtEDGECOMB_F_NO.Foreground = Brushes.Red;
                                else
                                    txtEDGECOMB_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].STIFFNESS_F != null)
                        {
                            txtSTIFFNESS_F_NO.Text = results[0].STIFFNESS_F.Value.ToString("#,##0.##");
                            _session.P_STIFFNESS_F_NO = results[0].STIFFNESS_F;

                            #region Foreground

                            if (_session.P_STIFFNESS_F_NO != null)
                            {
                                if (_session.P_STIFFNESS_F_NO > 3)
                                    txtSTIFFNESS_F_NO.Foreground = Brushes.Red;
                                else
                                    txtSTIFFNESS_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].STIFFNESS_W != null)
                        {
                            txtSTIFFNESS_W_NO.Text = results[0].STIFFNESS_W.Value.ToString("#,##0.##");
                            _session.P_STIFFNESS_W_NO = results[0].STIFFNESS_W;

                            #region Foreground

                            if (_session.P_STIFFNESS_W_NO != null)
                            {
                                if (_session.P_STIFFNESS_W_NO > 3)
                                    txtSTIFFNESS_W_NO.Foreground = Brushes.Red;
                                else
                                    txtSTIFFNESS_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].TEAR_W != null)
                        {
                            txtTEAR_W_NO.Text = results[0].TEAR_W.Value.ToString("#,##0.##");
                            _session.P_TEAR_W_NO = results[0].TEAR_W;

                            #region Foreground

                            if (_session.P_TEAR_W_NO != null)
                            {
                                if (_session.P_TEAR_W_NO > 3)
                                    txtTEAR_W_NO.Foreground = Brushes.Red;
                                else
                                    txtTEAR_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].TEAR_F != null)
                        {
                            txtTEAR_F_NO.Text = results[0].TEAR_F.Value.ToString("#,##0.##");
                            _session.P_TEAR_F_NO = results[0].TEAR_F;

                            #region Foreground

                            if (_session.P_TEAR_F_NO != null)
                            {
                                if (_session.P_TEAR_F_NO > 3)
                                    txtTEAR_F_NO.Foreground = Brushes.Red;
                                else
                                    txtTEAR_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].STATIC_AIR != null)
                        {
                            txtSTATIC_AIR_NO.Text = results[0].STATIC_AIR.Value.ToString("#,##0.##");
                            _session.P_STATIC_AIR_NO = results[0].STATIC_AIR;

                            #region Foreground

                            if (_session.P_STATIC_AIR_NO != null)
                            {
                                if (_session.P_STATIC_AIR_NO > 6)
                                    txtSTATIC_AIR_NO.Foreground = Brushes.Red;
                                else
                                    txtSTATIC_AIR_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].DYNAMIC_AIR != null)
                        {
                            txtDYNAMIC_AIR_NO.Text = results[0].DYNAMIC_AIR.Value.ToString("#,##0.##");
                            _session.P_DYNAMIC_AIR_NO = results[0].DYNAMIC_AIR;

                            #region Foreground

                            if (_session.P_DYNAMIC_AIR_NO != null)
                            {
                                if (_session.P_DYNAMIC_AIR_NO > 3)
                                    txtDYNAMIC_AIR_NO.Foreground = Brushes.Red;
                                else
                                    txtDYNAMIC_AIR_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].EXPONENT != null)
                        {
                            txtEXPONENT_NO.Text = results[0].EXPONENT.Value.ToString("#,##0.##");
                            _session.P_EXPONENT_NO = results[0].EXPONENT;

                            #region Foreground

                            if (_session.P_EXPONENT_NO != null)
                            {
                                if (_session.P_EXPONENT_NO > 3)
                                    txtEXPONENT_NO.Foreground = Brushes.Red;
                                else
                                    txtEXPONENT_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].DIMENSCHANGE_F != null)
                        {
                            txtDIMENSCHANGE_F_NO.Text = results[0].DIMENSCHANGE_F.Value.ToString("#,##0.##");
                            _session.P_DIMENSCHANGE_F_NO = results[0].DIMENSCHANGE_F;

                            #region Foreground

                            if (_session.P_DIMENSCHANGE_F_NO != null)
                            {
                                if (_session.P_DIMENSCHANGE_F_NO > 3)
                                    txtDIMENSCHANGE_F_NO.Foreground = Brushes.Red;
                                else
                                    txtDIMENSCHANGE_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].DIMENSCHANGE_W != null)
                        {
                            txtDIMENSCHANGE_W_NO.Text = results[0].DIMENSCHANGE_W.Value.ToString("#,##0.##");
                            _session.P_DIMENSCHANGE_W_NO = results[0].DIMENSCHANGE_W;

                            #region Foreground

                            if (_session.P_DIMENSCHANGE_W_NO != null)
                            {
                                if (_session.P_DIMENSCHANGE_W_NO > 3)
                                    txtDIMENSCHANGE_W_NO.Foreground = Brushes.Red;
                                else
                                    txtDIMENSCHANGE_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].FLEXABRASION_F != null)
                        {
                            txtFLEXABRASION_F_NO.Text = results[0].FLEXABRASION_F.Value.ToString("#,##0.##");
                            _session.P_FLEXABRASION_F_NO = results[0].FLEXABRASION_F;

                            #region Foreground

                            if (_session.P_FLEXABRASION_F_NO != null)
                            {
                                if (_session.P_FLEXABRASION_F_NO > 3)
                                    txtFLEXABRASION_F_NO.Foreground = Brushes.Red;
                                else
                                    txtFLEXABRASION_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].FLEXABRASION_W != null)
                        {
                            txtFLEXABRASION_W_NO.Text = results[0].FLEXABRASION_W.Value.ToString("#,##0.##");
                            _session.P_FLEXABRASION_W_NO = results[0].FLEXABRASION_W;

                            #region Foreground

                            if (_session.P_FLEXABRASION_W_NO != null)
                            {
                                if (_session.P_FLEXABRASION_W_NO > 3)
                                    txtFLEXABRASION_W_NO.Foreground = Brushes.Red;
                                else
                                    txtFLEXABRASION_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].BOW != null)
                        {
                            txtBOW_NO.Text = results[0].BOW.Value.ToString("#,##0.##");
                            _session.P_BOW_NO = results[0].BOW;

                            #region Foreground

                            if (_session.P_BOW_NO != null)
                            {
                                if (_session.P_BOW_NO > 3)
                                    txtBOW_NO.Foreground = Brushes.Red;
                                else
                                    txtBOW_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].SKEW != null)
                        {
                            txtSKEW_NO.Text = results[0].SKEW.Value.ToString("#,##0.##");
                            _session.P_SKEW_NO = results[0].SKEW;

                            #region Foreground

                            if (_session.P_SKEW_NO != null)
                            {
                                if (_session.P_SKEW_NO > 3)
                                    txtSKEW_NO.Foreground = Brushes.Red;
                                else
                                    txtSKEW_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        //Update 07/07/18
                        if (results[0].BENDING_F != null)
                        {
                            txtBENDING_F_NO.Text = results[0].BENDING_F.Value.ToString("#,##0.##");
                            _session.P_BENDING_F_NO = results[0].BENDING_F;

                            #region Foreground

                            if (_session.P_BENDING_F_NO != null)
                            {
                                if (_session.P_BENDING_F_NO > 3)
                                    txtBENDING_F_NO.Foreground = Brushes.Red;
                                else
                                    txtBENDING_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].BENDING_W != null)
                        {
                            txtBENDING_W_NO.Text = results[0].BENDING_W.Value.ToString("#,##0.##");
                            _session.P_BENDING_W_NO = results[0].BENDING_W;

                            #region Foreground

                            if (_session.P_BENDING_W_NO != null)
                            {
                                if (_session.P_BENDING_W_NO > 3)
                                    txtBENDING_W_NO.Foreground = Brushes.Red;
                                else
                                    txtBENDING_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].FLEX_SCOTT_F != null)
                        {
                            txtFLEX_SCOTT_F_NO.Text = results[0].FLEX_SCOTT_F.Value.ToString("#,##0.##");
                            _session.P_FLEX_SCOTT_F_NO = results[0].FLEX_SCOTT_F;

                            #region Foreground

                            if (_session.P_FLEX_SCOTT_F_NO != null)
                            {
                                if (_session.P_FLEX_SCOTT_F_NO > 3)
                                    txtFLEX_SCOTT_F_NO.Foreground = Brushes.Red;
                                else
                                    txtFLEX_SCOTT_F_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }

                        if (results[0].FLEX_SCOTT_W != null)
                        {
                            txtFLEX_SCOTT_W_NO.Text = results[0].FLEX_SCOTT_W.Value.ToString("#,##0.##");
                            _session.P_FLEX_SCOTT_W_NO = results[0].FLEX_SCOTT_W;

                            #region Foreground

                            if (_session.P_FLEX_SCOTT_W_NO != null)
                            {
                                if (_session.P_FLEX_SCOTT_W_NO > 3)
                                    txtFLEX_SCOTT_W_NO.Foreground = Brushes.Red;
                                else
                                    txtFLEX_SCOTT_W_NO.Foreground = Brushes.Black;
                            }

                            #endregion
                        }
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
                #region Specification
                txtWIDTHSpecification.Text = string.Empty;
                txtUSEWIDTHSpecification.Text = string.Empty;
                txtWIDTHSILICONESpecification.Text = string.Empty;
                txtNUMTHREADS_FSpecification.Text = string.Empty;
                txtNUMTHREADS_WSpecification.Text = string.Empty;
                txtTOTALWEIGHTSpecification.Text = string.Empty;
                txtUNCOATEDWEIGHTSpecification.Text = string.Empty;
                txtCOATINGWEIGHTSpecification.Text = string.Empty;
                txtTHICKNESSSpecification.Text = string.Empty;

                txtMAXFORCE_WSpecification.Text = string.Empty;
                txtMAXFORCE_FSpecification.Text = string.Empty;
                txtELOGATION_WSpecification.Text = string.Empty;
                txtELOGATION_FSpecification.Text = string.Empty;

                txtFLAMMABILITY_FSpecification.Text = string.Empty;
                txtFLAMMABILITY_WSpecification.Text = string.Empty;

                txtEDGECOMB_WSpecification.Text = string.Empty;
                txtEDGECOMB_FSpecification.Text = string.Empty;

                txtSTIFFNES_FSpecification.Text = string.Empty;
                txtSTIFFNES_WSpecification.Text = string.Empty;

                txtTEAR_FSpecification.Text = string.Empty;
                txtTEAR_WSpecification.Text = string.Empty;

                txtSTATIC_AIRSpecification.Text = string.Empty;
                txtDYNAMIC_AIRSpecification.Text = string.Empty;
                txtEXPONENTSpecification.Text = string.Empty;
                txtDIMENSCHANGE_FSpecification.Text = string.Empty;
                txtDIMENSCHANGE_WSpecification.Text = string.Empty;
                txtFLEXABRASION_FSpecification.Text = string.Empty;
                txtFLEXABRASION_WSpecification.Text = string.Empty;
                txtBOWSpecification.Text = string.Empty;
                txtSKEWSpecification.Text = string.Empty;

                txtBENDING_FSpecification.Text = string.Empty;
                txtBENDING_WSpecification.Text = string.Empty;
                txtFLEX_SCOTT_FSpecification.Text = string.Empty;
                txtFLEX_SCOTT_WSpecification.Text = string.Empty;

                #endregion

                #region TOR

                txtUSEWIDTH_TOR.Text = string.Empty;
                txtWIDTHSILICONE_TOR.Text = string.Empty;
                txtNUMTHREADS_F_TOR.Text = string.Empty;
                txtNUMTHREADS_W_TOR.Text = string.Empty;
                txtTOTALWEIGHT_TOR.Text = string.Empty;
                txtUNCOATEDWEIGHT_TOR.Text = string.Empty;
                txtCOATWEIGHT_TOR.Text = string.Empty;
                txtTHICKNESS_TOR.Text = string.Empty;
                txtMAXFORCE_W_TOR.Text = string.Empty;
                txtMAXFORCE_F_TOR.Text = string.Empty;
                txtELOGATION_W_TOR.Text = string.Empty;
                txtELOGATION_F_TOR.Text = string.Empty;
                txtFLAMMABILITY_F_TOR.Text = string.Empty;
                txtFLAMMABILITY_W_TOR.Text = string.Empty;
                txtEDGECOMB_F_TOR.Text = string.Empty;
                txtEDGECOMB_W_TOR.Text = string.Empty;
                txtSTIFFNESS_F_TOR.Text = string.Empty;
                txtSTIFFNESS_W_TOR.Text = string.Empty;
                txtTEAR_W_TOR.Text = string.Empty;
                txtTEAR_F_TOR.Text = string.Empty;
                txtSTATIC_AIR_TOR.Text = string.Empty;
                txtDYNAMIC_AIR_TOR.Text = string.Empty;
                txtEXPONENT_TOR.Text = string.Empty;
                txtDIMENSCHANGE_F_TOR.Text = string.Empty;
                txtDIMENSCHANGE_W_TOR.Text = string.Empty;
                txtFLEXABRASION_F_TOR.Text = string.Empty;
                txtFLEXABRASION_W_TOR.Text = string.Empty;
                txtBOW_TOR.Text = string.Empty;
                txtSKEW_TOR.Text = string.Empty;

                //Update 07/07/18
                txtBENDING_F_TOR.Text = string.Empty;
                txtBENDING_W_TOR.Text = string.Empty;
                txtFLEX_SCOTT_F_TOR.Text = string.Empty;
                txtFLEX_SCOTT_W_TOR.Text = string.Empty;

                #endregion

                List<LAB_GETITEMTESTSPECIFICATION> results = LabDataPDFDataService.Instance.LAB_GETITEMTESTSPECIFICATION(P_ITMCODE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        #region Specification

                        if (results[0].WIDTH != null)
                        {
                            txtWIDTHSpecification.Text = results[0].WIDTH.Value.ToString("#,##0.##");
                            _session.P_WIDTH = results[0].WIDTH;
                        }

                        if (results[0].USABLE_WIDTH != null)
                        {
                            txtUSEWIDTHSpecification.Text = results[0].USABLE_WIDTH.Value.ToString("#,##0.##");
                            _session.P_USEWIDTH = results[0].USABLE_WIDTH;
                        }

                        if (results[0].WIDTH_SILICONE != null)
                        {
                            txtWIDTHSILICONESpecification.Text = results[0].WIDTH_SILICONE.Value.ToString("#,##0.##");
                            _session.P_WIDTHSILICONE = results[0].WIDTH_SILICONE;
                        }

                        if (results[0].NUMTHREADS_F != null)
                        {
                            txtNUMTHREADS_FSpecification.Text = results[0].NUMTHREADS_F.Value.ToString("#,##0.##");
                            _session.P_NUMTHREADS_F = results[0].NUMTHREADS_F;
                        }

                        if (results[0].NUMTHREADS_W != null)
                        {
                            txtNUMTHREADS_WSpecification.Text = results[0].NUMTHREADS_W.Value.ToString("#,##0.##");
                            _session.P_NUMTHREADS_W = results[0].NUMTHREADS_W;
                        }

                        if (results[0].TOTALWEIGHT != null)
                        {
                            txtTOTALWEIGHTSpecification.Text = results[0].TOTALWEIGHT.Value.ToString("#,##0.##");
                            _session.P_TOTALWEIGHT = results[0].TOTALWEIGHT;
                        }

                        if (results[0].UNCOATEDWEIGHT != null)
                        {
                            txtUNCOATEDWEIGHTSpecification.Text = results[0].UNCOATEDWEIGHT.Value.ToString("#,##0.##");
                            _session.P_UNCOATEDWEIGHT = results[0].UNCOATEDWEIGHT;
                        }

                        if (results[0].COATINGWEIGHT != null)
                        {
                            txtCOATINGWEIGHTSpecification.Text = results[0].COATINGWEIGHT.Value.ToString("#,##0.##");
                            _session.P_COATWEIGHT = results[0].COATINGWEIGHT;
                        }

                        if (results[0].THICKNESS != null)
                        {
                            txtTHICKNESSSpecification.Text = results[0].THICKNESS.Value.ToString("#,##0.##");
                            _session.P_THICKNESS = results[0].THICKNESS;
                        }

                        if (results[0].MAXFORCE_W != null)
                        {
                            txtMAXFORCE_WSpecification.Text = results[0].MAXFORCE_W.Value.ToString("#,##0.##");
                            _session.P_MAXFORCE_W = results[0].MAXFORCE_W;
                        }

                        if (results[0].MAXFORCE_F != null)
                        {
                            txtMAXFORCE_FSpecification.Text = results[0].MAXFORCE_F.Value.ToString("#,##0.##");
                            _session.P_MAXFORCE_F = results[0].MAXFORCE_F;
                        }

                        if (results[0].ELONGATIONFORCE_W != null)
                        {
                            txtELOGATION_WSpecification.Text = results[0].ELONGATIONFORCE_W.Value.ToString("#,##0.##");
                            _session.P_ELOGATION_W = results[0].ELONGATIONFORCE_W;
                        }

                        if (results[0].ELONGATIONFORCE_F != null)
                        {
                            txtELOGATION_FSpecification.Text = results[0].ELONGATIONFORCE_F.Value.ToString("#,##0.##");
                            _session.P_ELOGATION_F = results[0].ELONGATIONFORCE_F;
                        }

                        if (results[0].FLAMMABILITY_F != null)
                        {
                            txtFLAMMABILITY_FSpecification.Text = results[0].FLAMMABILITY_F.Value.ToString("#,##0.##");
                            _session.P_FLAMMABILITY_F = results[0].FLAMMABILITY_F;
                        }

                        if (results[0].FLAMMABILITY_W != null)
                        {
                            txtFLAMMABILITY_WSpecification.Text = results[0].FLAMMABILITY_W.Value.ToString("#,##0.##");
                            _session.P_FLAMMABILITY_W = results[0].FLAMMABILITY_W;
                        }

                        if (results[0].EDGECOMB_W != null)
                        {
                            txtEDGECOMB_WSpecification.Text = results[0].EDGECOMB_W.Value.ToString("#,##0.##");
                            _session.P_EDGECOMB_W = results[0].EDGECOMB_W;
                        }

                        if (results[0].EDGECOMB_F != null)
                        {
                            txtEDGECOMB_FSpecification.Text = results[0].EDGECOMB_F.Value.ToString("#,##0.##");
                            _session.P_EDGECOMB_F = results[0].EDGECOMB_F;
                        }

                        if (results[0].STIFFNESS_F != null)
                        {
                            txtSTIFFNES_FSpecification.Text = results[0].STIFFNESS_F.Value.ToString("#,##0.##");
                            _session.P_STIFFNESS_F = results[0].STIFFNESS_F;
                        }

                        if (results[0].STIFFNESS_W != null)
                        {
                            txtSTIFFNES_WSpecification.Text = results[0].STIFFNESS_W.Value.ToString("#,##0.##");
                            _session.P_STIFFNESS_W = results[0].STIFFNESS_W;
                        }

                        if (results[0].TEAR_W != null)
                        {
                            txtTEAR_WSpecification.Text = results[0].TEAR_W.Value.ToString("#,##0.##");
                            _session.P_TEAR_W = results[0].TEAR_W;
                        }

                        if (results[0].TEAR_F != null)
                        {
                            txtTEAR_FSpecification.Text = results[0].TEAR_F.Value.ToString("#,##0.##");
                            _session.P_TEAR_F = results[0].TEAR_F;
                        }

                        if (results[0].STATIC_AIR != null)
                        {
                            txtSTATIC_AIRSpecification.Text = results[0].STATIC_AIR.Value.ToString("#,##0.##");
                            _session.P_STATIC_AIR = results[0].STATIC_AIR;
                        }

                        if (results[0].DYNAMIC_AIR != null)
                        {
                            txtDYNAMIC_AIRSpecification.Text = results[0].DYNAMIC_AIR.Value.ToString("#,##0.##");
                            _session.P_DYNAMIC_AIR = results[0].DYNAMIC_AIR;
                        }

                        if (results[0].EXPONENT != null)
                        {
                            txtEXPONENTSpecification.Text = results[0].EXPONENT.Value.ToString("#,##0.##");
                            _session.P_EXPONENT = results[0].EXPONENT;
                        }

                        if (results[0].DIMENSCHANGE_F != null)
                        {
                            txtDIMENSCHANGE_FSpecification.Text = results[0].DIMENSCHANGE_F.Value.ToString("#,##0.##");
                            _session.P_DIMENSCHANGE_F = results[0].DIMENSCHANGE_F;
                        }

                        if (results[0].DIMENSCHANGE_W != null)
                        {
                            txtDIMENSCHANGE_WSpecification.Text = results[0].DIMENSCHANGE_W.Value.ToString("#,##0.##");
                            _session.P_DIMENSCHANGE_W = results[0].DIMENSCHANGE_W;
                        }

                        if (results[0].FLEXABRASION_F != null)
                        {
                            txtFLEXABRASION_FSpecification.Text = results[0].FLEXABRASION_F.Value.ToString("#,##0.##");
                            _session.P_FLEXABRASION_F = results[0].FLEXABRASION_F;
                        }

                        if (results[0].FLEXABRASION_W != null)
                        {
                            txtFLEXABRASION_WSpecification.Text = results[0].FLEXABRASION_W.Value.ToString("#,##0.##");
                            _session.P_FLEXABRASION_W = results[0].FLEXABRASION_W;
                        }

                        // Update 07/07/18
                        if (results[0].BOW != null)
                        {
                            txtBOWSpecification.Text = results[0].BOW.Value.ToString("#,##0.##");
                            _session.P_BOW = results[0].BOW;
                        }

                        if (results[0].SKEW != null)
                        {
                            txtSKEWSpecification.Text = results[0].SKEW.Value.ToString("#,##0.##");
                            _session.P_SKEW = results[0].SKEW;
                        }


                        if (results[0].BENDING_W != null)
                        {
                            txtBENDING_WSpecification.Text = results[0].BENDING_W.Value.ToString("#,##0.##");
                            _session.P_BENDING_W = results[0].BENDING_W;
                        }

                        if (results[0].BENDING_F != null)
                        {
                            txtBENDING_FSpecification.Text = results[0].BENDING_F.Value.ToString("#,##0.##");
                            _session.P_BENDING_F = results[0].BENDING_F;
                        }

                        if (results[0].FLEX_SCOTT_W != null)
                        {
                            txtFLEX_SCOTT_WSpecification.Text = results[0].FLEX_SCOTT_W.Value.ToString("#,##0.##");
                            _session.P_FLEX_SCOTT_W = results[0].FLEX_SCOTT_W;
                        }

                        if (results[0].FLEX_SCOTT_F != null)
                        {
                            txtFLEX_SCOTT_FSpecification.Text = results[0].FLEX_SCOTT_F.Value.ToString("#,##0.##");
                            _session.P_FLEX_SCOTT_F = results[0].FLEX_SCOTT_F;
                        }
                        #endregion

                        #region TOR

                        //if (results[0].WIDTH != null)
                        //    txtWIDTH_TOR.Text = results[0].WIDTH.Value.ToString("#,##0.##");

                        if (results[0].USABLE_WIDTH_TOR != null)
                        {
                            if (results[0].USABLE_WIDTH_TOR == "MIN.")
                            {
                                cbUSEWIDTH_TOR.SelectedValue = "MIN.";
                                txtUSEWIDTH_TOR.Text = string.Empty; 
                            }
                            else if (results[0].USABLE_WIDTH_TOR == "MAX.")
                            {
                                cbUSEWIDTH_TOR.SelectedValue = "MAX.";
                                txtUSEWIDTH_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbUSEWIDTH_TOR.SelectedValue = "Number";
                                txtUSEWIDTH_TOR.Text = results[0].USABLE_WIDTH_TOR;
                            }

                            _session.P_USEWIDTH_TOR = results[0].USABLE_WIDTH_TOR;
                        }
                        else
                        {
                            cbUSEWIDTH_TOR.SelectedValue = "";
                        }

                        if (results[0].WIDTH_SILICONE_TOR != null)
                        {
                            if (results[0].WIDTH_SILICONE_TOR == "MIN.")
                            {
                                cbWIDTHSILICONE_TOR.SelectedValue = "MIN.";
                                txtWIDTHSILICONE_TOR.Text = string.Empty;
                            }
                            else if (results[0].WIDTH_SILICONE_TOR == "MAX.")
                            {
                                cbWIDTHSILICONE_TOR.SelectedValue = "MAX.";
                                txtWIDTHSILICONE_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbWIDTHSILICONE_TOR.SelectedValue = "Number";
                                txtWIDTHSILICONE_TOR.Text = results[0].WIDTH_SILICONE_TOR;
                            }

                            _session.P_WIDTHSILICONE_TOR = results[0].WIDTH_SILICONE_TOR;
                        }
                        else
                        {
                            cbWIDTHSILICONE_TOR.SelectedValue = "";
                        }

                        if (results[0].NUMTHREADS_F_TOR != null)
                        {
                            txtNUMTHREADS_F_TOR.Text = results[0].NUMTHREADS_F_TOR.Value.ToString("#,##0.##");
                            _session.P_NUMTHREADS_F_TOR = results[0].NUMTHREADS_F_TOR;
                        }

                        if (results[0].NUMTHREADS_W_TOR != null)
                        {
                            txtNUMTHREADS_W_TOR.Text = results[0].NUMTHREADS_W_TOR.Value.ToString("#,##0.##");
                            _session.P_NUMTHREADS_W_TOR = results[0].NUMTHREADS_W_TOR;
                        }

                        if (results[0].TOTALWEIGHT_TOR != null)
                        {
                            txtTOTALWEIGHT_TOR.Text = results[0].TOTALWEIGHT_TOR.Value.ToString("#,##0.##");
                            _session.P_TOTALWEIGHT_TOR = results[0].TOTALWEIGHT_TOR;
                        }

                        if (results[0].UNCOATEDWEIGHT_TOR != null)
                        {
                            txtUNCOATEDWEIGHT_TOR.Text = results[0].UNCOATEDWEIGHT_TOR.Value.ToString("#,##0.##");
                            _session.P_UNCOATEDWEIGHT_TOR = results[0].UNCOATEDWEIGHT_TOR;
                        }

                        if (results[0].COATINGWEIGHT_TOR != null)
                        {
                            txtCOATWEIGHT_TOR.Text = results[0].COATINGWEIGHT_TOR.Value.ToString("#,##0.##");
                            _session.P_COATWEIGHT_TOR = results[0].COATINGWEIGHT_TOR;
                        }

                        if (results[0].THICKNESS_TOR != null)
                        {
                            txtTHICKNESS_TOR.Text = results[0].THICKNESS_TOR.Value.ToString("#,##0.##");
                            _session.P_THICKNESS_TOR = results[0].THICKNESS_TOR;
                        }

                        if (results[0].MAXFORCE_W_TOR != null)
                        {
                            if (results[0].MAXFORCE_W_TOR == "MIN.")
                            {
                                cbMAXFORCE_W_TOR.SelectedValue = "MIN.";
                                txtMAXFORCE_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].MAXFORCE_W_TOR == "MAX.")
                            {
                                cbMAXFORCE_W_TOR.SelectedValue = "MAX.";
                                txtMAXFORCE_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbMAXFORCE_W_TOR.SelectedValue = "Number";
                                txtMAXFORCE_W_TOR.Text = results[0].MAXFORCE_W_TOR;
                            }

                            _session.P_MAXFORCE_W_TOR = results[0].MAXFORCE_W_TOR;
                        }
                        else
                        {
                            cbMAXFORCE_W_TOR.SelectedValue = "";
                        }

                        if (results[0].MAXFORCE_F_TOR != null)
                        {
                            if (results[0].MAXFORCE_F_TOR == "MIN.")
                            {
                                cbMAXFORCE_F_TOR.SelectedValue = "MIN.";
                                txtMAXFORCE_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].MAXFORCE_F_TOR == "MAX.")
                            {
                                cbMAXFORCE_F_TOR.SelectedValue = "MAX.";
                                txtMAXFORCE_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbMAXFORCE_F_TOR.SelectedValue = "Number";
                                txtMAXFORCE_F_TOR.Text = results[0].MAXFORCE_F_TOR;
                            }

                            _session.P_MAXFORCE_F_TOR = results[0].MAXFORCE_F_TOR;
                        }
                        else
                        {
                            cbMAXFORCE_F_TOR.SelectedValue = "";
                        }

                        if (results[0].ELONGATIONFORCE_W_TOR != null)
                        {
                            if (results[0].ELONGATIONFORCE_W_TOR == "MIN.")
                            {
                                cbELOGATION_W_TOR.SelectedValue = "MIN.";
                                txtELOGATION_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].ELONGATIONFORCE_W_TOR == "MAX.")
                            {
                                cbELOGATION_W_TOR.SelectedValue = "MAX.";
                                txtELOGATION_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbELOGATION_W_TOR.SelectedValue = "Number";
                                txtELOGATION_W_TOR.Text = results[0].ELONGATIONFORCE_W_TOR;
                            }

                            _session.P_ELOGATION_W_TOR = results[0].ELONGATIONFORCE_W_TOR;
                        }
                        else
                        {
                            cbELOGATION_W_TOR.SelectedValue = "";
                        }

                        if (results[0].ELONGATIONFORCE_F_TOR != null)
                        {
                            if (results[0].ELONGATIONFORCE_F_TOR == "MIN.")
                            {
                                cbELOGATION_F_TOR.SelectedValue = "MIN.";
                                txtELOGATION_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].ELONGATIONFORCE_F_TOR == "MAX.")
                            {
                                cbELOGATION_F_TOR.SelectedValue = "MAX.";
                                txtELOGATION_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbELOGATION_F_TOR.SelectedValue = "Number";
                                txtELOGATION_F_TOR.Text = results[0].ELONGATIONFORCE_F_TOR;
                            }

                            _session.P_ELOGATION_F_TOR = results[0].ELONGATIONFORCE_F_TOR;
                        }
                        else
                        {
                            cbELOGATION_F_TOR.SelectedValue = "";
                        }

                        if (results[0].FLAMMABILITY_F_TOR != null)
                        {
                            if (results[0].FLAMMABILITY_F_TOR == "MIN.")
                            {
                                cbFLAMMABILITY_F_TOR.SelectedValue = "MIN.";
                                txtFLAMMABILITY_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].FLAMMABILITY_F_TOR == "MAX.")
                            {
                                cbFLAMMABILITY_F_TOR.SelectedValue = "MAX.";
                                txtFLAMMABILITY_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbFLAMMABILITY_F_TOR.SelectedValue = "Number";
                                txtFLAMMABILITY_F_TOR.Text = results[0].FLAMMABILITY_F_TOR;
                            }

                            _session.P_FLAMMABILITY_F_TOR = results[0].FLAMMABILITY_F_TOR;
                        }
                        else
                        {
                            cbFLAMMABILITY_F_TOR.SelectedValue = "";
                        }

                        if (results[0].FLAMMABILITY_W_TOR != null)
                        {
                            if (results[0].FLAMMABILITY_W_TOR == "MIN.")
                            {
                                cbFLAMMABILITY_W_TOR.SelectedValue = "MIN.";
                                txtFLAMMABILITY_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].FLAMMABILITY_W_TOR == "MAX.")
                            {
                                cbFLAMMABILITY_W_TOR.SelectedValue = "MAX.";
                                txtFLAMMABILITY_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbFLAMMABILITY_W_TOR.SelectedValue = "Number";
                                txtFLAMMABILITY_W_TOR.Text = results[0].FLAMMABILITY_W_TOR;
                            }

                            _session.P_FLAMMABILITY_W_TOR = results[0].FLAMMABILITY_W_TOR;
                        }
                        else
                        {
                            cbFLAMMABILITY_W_TOR.SelectedValue = "";
                        }

                        if (results[0].EDGECOMB_F_TOR != null)
                        {
                            if (results[0].EDGECOMB_F_TOR == "MIN.")
                            {
                                cbEDGECOMB_F_TOR.SelectedValue = "MIN.";
                                txtEDGECOMB_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].EDGECOMB_F_TOR == "MAX.")
                            {
                                cbEDGECOMB_F_TOR.SelectedValue = "MAX.";
                                txtEDGECOMB_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbEDGECOMB_F_TOR.SelectedValue = "Number";
                                txtEDGECOMB_F_TOR.Text = results[0].EDGECOMB_F_TOR;
                            }

                            _session.P_EDGECOMB_F_TOR = results[0].EDGECOMB_F_TOR;
                        }
                        else
                        {
                            cbEDGECOMB_F_TOR.SelectedValue = "";
                        }

                        if (results[0].EDGECOMB_W_TOR != null)
                        {
                            if (results[0].EDGECOMB_W_TOR == "MIN.")
                            {
                                cbEDGECOMB_W_TOR.SelectedValue = "MIN.";
                                txtEDGECOMB_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].EDGECOMB_W_TOR == "MAX.")
                            {
                                cbEDGECOMB_W_TOR.SelectedValue = "MAX.";
                                txtEDGECOMB_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbEDGECOMB_W_TOR.SelectedValue = "Number";
                                txtEDGECOMB_W_TOR.Text = results[0].EDGECOMB_W_TOR;
                            }

                            _session.P_EDGECOMB_W_TOR = results[0].EDGECOMB_W_TOR;
                        }
                        else
                        {
                            cbEDGECOMB_W_TOR.SelectedValue = "";
                        }

                        if (results[0].STIFFNESS_F_TOR != null)
                        {
                            if (results[0].STIFFNESS_F_TOR == "MIN.")
                            {
                                cbSTIFFNESS_F_TOR.SelectedValue = "MIN.";
                                txtSTIFFNESS_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].STIFFNESS_F_TOR == "MAX.")
                            {
                                cbSTIFFNESS_F_TOR.SelectedValue = "MAX.";
                                txtSTIFFNESS_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbSTIFFNESS_F_TOR.SelectedValue = "Number";
                                txtSTIFFNESS_F_TOR.Text = results[0].STIFFNESS_F_TOR;
                            }

                            _session.P_STIFFNESS_F_TOR = results[0].STIFFNESS_F_TOR;
                        }
                        else
                        {
                            cbSTIFFNESS_F_TOR.SelectedValue = "";
                        }

                        if (results[0].STIFFNESS_W_TOR != null)
                        {
                            if (results[0].STIFFNESS_W_TOR == "MIN.")
                            {
                                cbSTIFFNESS_W_TOR.SelectedValue = "MIN.";
                                txtSTIFFNESS_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].STIFFNESS_W_TOR == "MAX.")
                            {
                                cbSTIFFNESS_W_TOR.SelectedValue = "MAX.";
                                txtSTIFFNESS_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbSTIFFNESS_W_TOR.SelectedValue = "Number";
                                txtSTIFFNESS_W_TOR.Text = results[0].STIFFNESS_W_TOR;
                            }

                            _session.P_STIFFNESS_W_TOR = results[0].STIFFNESS_W_TOR;
                        }
                        else
                        {
                            cbSTIFFNESS_W_TOR.SelectedValue = "";
                        }

                        if (results[0].TEAR_W_TOR != null)
                        {
                            if (results[0].TEAR_W_TOR == "MIN.")
                            {
                                cbTEAR_W_TOR.SelectedValue = "MIN.";
                                txtTEAR_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].TEAR_W_TOR == "MAX.")
                            {
                                cbTEAR_W_TOR.SelectedValue = "MAX.";
                                txtTEAR_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbTEAR_W_TOR.SelectedValue = "Number";
                                txtTEAR_W_TOR.Text = results[0].TEAR_W_TOR;
                            }

                            _session.P_TEAR_W_TOR = results[0].TEAR_W_TOR;
                        }
                        else
                        {
                            cbTEAR_W_TOR.SelectedValue = "";
                        }

                        if (results[0].TEAR_F_TOR != null)
                        {
                            if (results[0].TEAR_F_TOR == "MIN.")
                            {
                                cbTEAR_F_TOR.SelectedValue = "MIN.";
                                txtTEAR_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].TEAR_F_TOR == "MAX.")
                            {
                                cbTEAR_F_TOR.SelectedValue = "MAX.";
                                txtTEAR_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbTEAR_F_TOR.SelectedValue = "Number";
                                txtTEAR_F_TOR.Text = results[0].TEAR_F_TOR;
                            }

                            _session.P_TEAR_F_TOR = results[0].TEAR_F_TOR;
                        }
                        else
                        {
                            cbTEAR_F_TOR.SelectedValue = "";
                        }

                        if (results[0].STATIC_AIR_TOR != null)
                        {
                            if (results[0].STATIC_AIR_TOR == "MIN.")
                            {
                                cbSTATIC_AIR_TOR.SelectedValue = "MIN.";
                                txtSTATIC_AIR_TOR.Text = string.Empty;
                            }
                            else if (results[0].STATIC_AIR_TOR == "MAX.")
                            {
                                cbSTATIC_AIR_TOR.SelectedValue = "MAX.";
                                txtSTATIC_AIR_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbSTATIC_AIR_TOR.SelectedValue = "Number";
                                txtSTATIC_AIR_TOR.Text = results[0].STATIC_AIR_TOR;
                            }

                            _session.P_STATIC_AIR_TOR = results[0].STATIC_AIR_TOR;
                        }
                        else
                        {
                            cbSTATIC_AIR_TOR.SelectedValue = "";
                        }

                        if (results[0].DYNAMIC_AIR_TOR != null)
                        {
                            txtDYNAMIC_AIR_TOR.Text = results[0].DYNAMIC_AIR_TOR.Value.ToString("#,##0.##");
                            _session.P_DYNAMIC_AIR_TOR = results[0].DYNAMIC_AIR_TOR;
                        }

                        if (results[0].EXPONENT_TOR != null)
                        {
                            txtEXPONENT_TOR.Text = results[0].EXPONENT_TOR.Value.ToString("#,##0.##");
                            _session.P_EXPONENT_TOR = results[0].EXPONENT_TOR;
                        }

                        if (results[0].DIMENSCHANGE_F_TOR != null)
                        {
                            if (results[0].DIMENSCHANGE_F_TOR == "MIN.")
                            {
                                cbDIMENSCHANGE_F_TOR.SelectedValue = "MIN.";
                                txtDIMENSCHANGE_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].DIMENSCHANGE_F_TOR == "MAX.")
                            {
                                cbDIMENSCHANGE_F_TOR.SelectedValue = "MAX.";
                                txtDIMENSCHANGE_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbDIMENSCHANGE_F_TOR.SelectedValue = "Number";
                                txtDIMENSCHANGE_F_TOR.Text = results[0].DIMENSCHANGE_F_TOR;
                            }

                            _session.P_DIMENSCHANGE_F_TOR = results[0].DIMENSCHANGE_F_TOR;
                        }
                        else
                        {
                            cbDIMENSCHANGE_F_TOR.SelectedValue = "";
                        }

                        if (results[0].DIMENSCHANGE_W_TOR != null)
                        {
                            if (results[0].DIMENSCHANGE_W_TOR == "MIN.")
                            {
                                cbDIMENSCHANGE_W_TOR.SelectedValue = "MIN.";
                                txtDIMENSCHANGE_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].DIMENSCHANGE_W_TOR == "MAX.")
                            {
                                cbDIMENSCHANGE_W_TOR.SelectedValue = "MAX.";
                                txtDIMENSCHANGE_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbDIMENSCHANGE_W_TOR.SelectedValue = "Number";
                                txtDIMENSCHANGE_W_TOR.Text = results[0].DIMENSCHANGE_W_TOR;
                            }

                            _session.P_DIMENSCHANGE_W_TOR = results[0].DIMENSCHANGE_W_TOR;
                        }
                        else
                        {
                            cbDIMENSCHANGE_W_TOR.SelectedValue = "";
                        }

                        if (results[0].FLEXABRASION_F_TOR != null)
                        {
                            if (results[0].FLEXABRASION_F_TOR == "MIN.")
                            {
                                cbFLEXABRASION_F_TOR.SelectedValue = "MIN.";
                                txtFLEXABRASION_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].FLEXABRASION_F_TOR == "MAX.")
                            {
                                cbFLEXABRASION_F_TOR.SelectedValue = "MAX.";
                                txtFLEXABRASION_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbFLEXABRASION_F_TOR.SelectedValue = "Number";
                                txtFLEXABRASION_F_TOR.Text = results[0].FLEXABRASION_F_TOR;
                            }

                            _session.P_FLEXABRASION_F_TOR = results[0].FLEXABRASION_F_TOR;
                        }
                        else
                        {
                            cbFLEXABRASION_F_TOR.SelectedValue = "";
                        }

                        if (results[0].FLEXABRASION_W_TOR != null)
                        {
                            if (results[0].FLEXABRASION_W_TOR == "MIN.")
                            {
                                cbFLEXABRASION_W_TOR.SelectedValue = "MIN.";
                                txtFLEXABRASION_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].FLEXABRASION_W_TOR == "MAX.")
                            {
                                cbFLEXABRASION_W_TOR.SelectedValue = "MAX.";
                                txtFLEXABRASION_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbFLEXABRASION_W_TOR.SelectedValue = "Number";
                                txtFLEXABRASION_W_TOR.Text = results[0].FLEXABRASION_W_TOR;
                            }

                            _session.P_FLEXABRASION_W_TOR = results[0].FLEXABRASION_W_TOR;
                        }
                        else
                        {
                            cbFLEXABRASION_W_TOR.SelectedValue = "";
                        }

                        //Update 07/07/18
                        if (results[0].BOW_TOR != null)
                        {
                            if (results[0].BOW_TOR == "MIN.")
                            {
                                cbBOW_TOR.SelectedValue = "MIN.";
                                txtBOW_TOR.Text = string.Empty;
                            }
                            else if (results[0].BOW_TOR == "MAX.")
                            {
                                cbBOW_TOR.SelectedValue = "MAX.";
                                txtBOW_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbBOW_TOR.SelectedValue = "Number";
                                txtBOW_TOR.Text = results[0].BOW_TOR;
                            }

                            _session.P_BOW_TOR = results[0].BOW_TOR;
                        }
                        else
                        {
                            cbBOW_TOR.SelectedValue = "";
                        }

                        if (results[0].SKEW_TOR != null)
                        {
                            if (results[0].SKEW_TOR == "MIN.")
                            {
                                cbSKEW_TOR.SelectedValue = "MIN.";
                                txtSKEW_TOR.Text = string.Empty;
                            }
                            else if (results[0].SKEW_TOR == "MAX.")
                            {
                                cbSKEW_TOR.SelectedValue = "MAX.";
                                txtSKEW_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbSKEW_TOR.SelectedValue = "Number";
                                txtSKEW_TOR.Text = results[0].SKEW_TOR;
                            }

                            _session.P_SKEW_TOR = results[0].SKEW_TOR;
                        }
                        else
                        {
                            cbSKEW_TOR.SelectedValue = "";
                        }

                        //Update 07/07/18
                        if (results[0].BENDING_F_TOR != null)
                        {
                            if (results[0].BENDING_F_TOR == "MIN.")
                            {
                                cbBENDING_F_TOR.SelectedValue = "MIN.";
                                txtBENDING_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].BENDING_F_TOR == "MAX.")
                            {
                                cbBENDING_F_TOR.SelectedValue = "MAX.";
                                txtBENDING_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbBENDING_F_TOR.SelectedValue = "Number";
                                txtBENDING_F_TOR.Text = results[0].BENDING_F_TOR;
                            }

                            _session.P_BENDING_F_TOR = results[0].BENDING_F_TOR;
                        }
                        else
                        {
                            cbBENDING_F_TOR.SelectedValue = "";
                        }

                        if (results[0].BENDING_W_TOR != null)
                        {
                            if (results[0].BENDING_W_TOR == "MIN.")
                            {
                                cbBENDING_W_TOR.SelectedValue = "MIN.";
                                txtBENDING_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].BENDING_W_TOR == "MAX.")
                            {
                                cbBENDING_W_TOR.SelectedValue = "MAX.";
                                txtBENDING_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbBENDING_W_TOR.SelectedValue = "Number";
                                txtBENDING_W_TOR.Text = results[0].BENDING_W_TOR;
                            }

                            _session.P_BENDING_W_TOR = results[0].BENDING_W_TOR;
                        }
                        else
                        {
                            cbBENDING_W_TOR.SelectedValue = "";
                        }

                        if (results[0].FLEX_SCOTT_F_TOR != null)
                        {
                            if (results[0].FLEX_SCOTT_F_TOR == "MIN.")
                            {
                                cbFLEX_SCOTT_F_TOR.SelectedValue = "MIN.";
                                txtFLEX_SCOTT_F_TOR.Text = string.Empty;
                            }
                            else if (results[0].FLEX_SCOTT_F_TOR == "MAX.")
                            {
                                cbFLEX_SCOTT_F_TOR.SelectedValue = "MAX.";
                                txtFLEX_SCOTT_F_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbFLEX_SCOTT_F_TOR.SelectedValue = "Number";
                                txtFLEX_SCOTT_F_TOR.Text = results[0].FLEX_SCOTT_F_TOR;
                            }

                            _session.P_FLEX_SCOTT_F_TOR = results[0].FLEX_SCOTT_F_TOR;
                        }
                        else
                        {
                            cbFLEX_SCOTT_F_TOR.SelectedValue = "";
                        }

                        if (results[0].FLEX_SCOTT_W_TOR != null)
                        {
                            if (results[0].FLEX_SCOTT_W_TOR == "MIN.")
                            {
                                cbFLEX_SCOTT_W_TOR.SelectedValue = "MIN.";
                                txtFLEX_SCOTT_W_TOR.Text = string.Empty;
                            }
                            else if (results[0].FLEX_SCOTT_W_TOR == "MAX.")
                            {
                                cbFLEX_SCOTT_W_TOR.SelectedValue = "MAX.";
                                txtFLEX_SCOTT_W_TOR.Text = string.Empty;
                            }
                            else
                            {
                                cbFLEX_SCOTT_W_TOR.SelectedValue = "Number";
                                txtFLEX_SCOTT_W_TOR.Text = results[0].FLEX_SCOTT_W_TOR;
                            }

                            _session.P_FLEX_SCOTT_W_TOR = results[0].FLEX_SCOTT_W_TOR;
                        }
                        else
                        {
                            cbFLEX_SCOTT_W_TOR.SelectedValue = "";
                        }
                        #endregion
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

        #region SearchData
        private void SearchData()
        {
            string P_ITMCODE = string.Empty;

            if (cbItemCode.SelectedValue != null)
            {
                P_ITMCODE = cbItemCode.SelectedValue.ToString();

                ClearControl();

                if (LoadItemTestProperty(P_ITMCODE) == false)
                {
                    "No Data loading from Item Test Property".ShowMessageBox();
                }
                else
                {
                    if (LoadItemTestSpecification(P_ITMCODE) == false)
                    {
                        "No Data Specification Found".ShowMessageBox();
                    }
                }
            }
        }
        #endregion

        #region chkMax
        private bool chkMax()
        {
            bool chkLoad = true;

            try
            {
                #region Def

                decimal? WIDTH_NO = null;
                decimal? USEWIDTH_NO = null;
                decimal? WIDTHSILICONE_NO = null;
                decimal? NUMTHREADS_F_NO = null;
                decimal? NUMTHREADS_W_NO = null;
                decimal? TOTALWEIGHT_NO = null;
                decimal? UNCOATEDWEIGHT_NO = null;
                decimal? COATWEIGHT_NO = null;
                decimal? THICKNESS_NO = null;
                decimal? MAXFORCE_W_NO = null;
                decimal? MAXFORCE_F_NO = null;
                decimal? ELOGATION_W_NO = null;
                decimal? ELOGATION_F_NO = null;
                decimal? FLAMMABILITY_F_NO = null;
                decimal? FLAMMABILITY_W_NO = null;
                decimal? EDGECOMB_W_NO = null;
                decimal? EDGECOMB_F_NO = null;
                decimal? STIFFNESS_F_NO = null;
                decimal? STIFFNESS_W_NO = null;
                decimal? TEAR_W_NO = null;
                decimal? TEAR_F_NO = null;
                decimal? STATIC_AIR_NO = null;
                decimal? DYNAMIC_AIR_NO = null;
                decimal? EXPONENT_NO = null;
                decimal? DIMENSCHANGE_F_NO = null;
                decimal? DIMENSCHANGE_W_NO = null;
                decimal? FLEXABRASION_F_NO = null;
                decimal? FLEXABRASION_W_NO = null;
                decimal? BOW_NO = null;
                decimal? SKEW_NO = null;

                decimal? BENDING_W_NO = null;
                decimal? BENDING_F_NO = null;
                decimal? FLEX_SCOTT_W_NO = null;
                decimal? FLEX_SCOTT_F_NO = null;
                
                #endregion

                #region NO

                if (!string.IsNullOrEmpty(txtWIDTH_NO.Text))
                {
                    if (Decimal.TryParse(txtWIDTH_NO.Text, out value))
                    {
                        WIDTH_NO = decimal.Parse(txtWIDTH_NO.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtUSEWIDTH_NO.Text)) 
                {
                    if (Decimal.TryParse(txtUSEWIDTH_NO.Text, out value))
                    {
                        USEWIDTH_NO = decimal.Parse(txtUSEWIDTH_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtWIDTHSILICONE_NO.Text)) 
                {
                    if (Decimal.TryParse(txtWIDTHSILICONE_NO.Text, out value))
                    {
                        WIDTHSILICONE_NO = decimal.Parse(txtWIDTHSILICONE_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtNUMTHREADS_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtNUMTHREADS_F_NO.Text, out value))
                    {
                        NUMTHREADS_F_NO = decimal.Parse(txtNUMTHREADS_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtNUMTHREADS_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtNUMTHREADS_W_NO.Text, out value))
                    {
                        NUMTHREADS_W_NO = decimal.Parse(txtNUMTHREADS_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTOTALWEIGHT_NO.Text)) 
                {
                    if (Decimal.TryParse(txtTOTALWEIGHT_NO.Text, out value))
                    {
                        TOTALWEIGHT_NO = decimal.Parse(txtTOTALWEIGHT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHT_NO.Text)) 
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHT_NO.Text, out value))
                    {
                        UNCOATEDWEIGHT_NO = decimal.Parse(txtUNCOATEDWEIGHT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtCOATWEIGHT_NO.Text)) 
                {
                    if (Decimal.TryParse(txtCOATWEIGHT_NO.Text, out value))
                    {
                        COATWEIGHT_NO = decimal.Parse(txtCOATWEIGHT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTHICKNESS_NO.Text)) 
                {
                    if (Decimal.TryParse(txtTHICKNESS_NO.Text, out value))
                    {
                        THICKNESS_NO = decimal.Parse(txtTHICKNESS_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtMAXFORCE_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtMAXFORCE_W_NO.Text, out value))
                    {
                        MAXFORCE_W_NO = decimal.Parse(txtMAXFORCE_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtMAXFORCE_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtMAXFORCE_F_NO.Text, out value))
                    {
                        MAXFORCE_F_NO = decimal.Parse(txtMAXFORCE_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtELOGATION_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtELOGATION_W_NO.Text, out value))
                    {
                        ELOGATION_W_NO = decimal.Parse(txtELOGATION_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtELOGATION_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtELOGATION_F_NO.Text, out value))
                    {
                        ELOGATION_F_NO = decimal.Parse(txtELOGATION_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_F_NO.Text, out value))
                    {
                        FLAMMABILITY_F_NO = decimal.Parse(txtFLAMMABILITY_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_W_NO.Text, out value))
                    {
                        FLAMMABILITY_W_NO = decimal.Parse(txtFLAMMABILITY_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtEDGECOMB_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtEDGECOMB_F_NO.Text, out value))
                    {
                        EDGECOMB_F_NO = decimal.Parse(txtEDGECOMB_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtEDGECOMB_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtEDGECOMB_W_NO.Text, out value))
                    {
                        EDGECOMB_W_NO = decimal.Parse(txtEDGECOMB_W_NO.Text);
                    }
                }
                
                if (!string.IsNullOrEmpty(txtSTIFFNESS_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtSTIFFNESS_F_NO.Text, out value))
                    {
                        STIFFNESS_F_NO = decimal.Parse(txtSTIFFNESS_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtSTIFFNESS_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtSTIFFNESS_W_NO.Text, out value))
                    {
                        STIFFNESS_W_NO = decimal.Parse(txtSTIFFNESS_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTEAR_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtTEAR_W_NO.Text, out value))
                    {
                        TEAR_W_NO = decimal.Parse(txtTEAR_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTEAR_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtTEAR_F_NO.Text, out value))
                    {
                        TEAR_F_NO = decimal.Parse(txtTEAR_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtSTATIC_AIR_NO.Text)) 
                {
                    if (Decimal.TryParse(txtSTATIC_AIR_NO.Text, out value))
                    {
                        STATIC_AIR_NO = decimal.Parse(txtSTATIC_AIR_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtDYNAMIC_AIR_NO.Text)) 
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIR_NO.Text, out value))
                    {
                        DYNAMIC_AIR_NO = decimal.Parse(txtDYNAMIC_AIR_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtEXPONENT_NO.Text)) 
                {
                    if (Decimal.TryParse(txtEXPONENT_NO.Text, out value))
                    {
                        EXPONENT_NO = decimal.Parse(txtEXPONENT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_F_NO.Text, out value))
                    {
                        DIMENSCHANGE_F_NO = decimal.Parse(txtDIMENSCHANGE_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_W_NO.Text, out value))
                    {
                        DIMENSCHANGE_W_NO = decimal.Parse(txtDIMENSCHANGE_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLEXABRASION_F_NO.Text)) 
                {
                    if (Decimal.TryParse(txtFLEXABRASION_F_NO.Text, out value))
                    {
                        FLEXABRASION_F_NO = decimal.Parse(txtFLEXABRASION_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLEXABRASION_W_NO.Text)) 
                {
                    if (Decimal.TryParse(txtFLEXABRASION_W_NO.Text, out value))
                    {
                        FLEXABRASION_W_NO = decimal.Parse(txtFLEXABRASION_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtBOW_NO.Text)) 
                {
                    if (Decimal.TryParse(txtBOW_NO.Text, out value))
                    {
                        BOW_NO = decimal.Parse(txtBOW_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtSKEW_NO.Text)) 
                {
                    if (Decimal.TryParse(txtSKEW_NO.Text, out value))
                    {
                        SKEW_NO = decimal.Parse(txtSKEW_NO.Text);
                    }
                }

                //Update 07/07/18
                if (!string.IsNullOrEmpty(txtBENDING_F_NO.Text))
                {
                    if (Decimal.TryParse(txtBENDING_F_NO.Text, out value))
                    {
                        BENDING_F_NO = decimal.Parse(txtBENDING_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtBENDING_W_NO.Text))
                {
                    if (Decimal.TryParse(txtBENDING_W_NO.Text, out value))
                    {
                        BENDING_W_NO = decimal.Parse(txtBENDING_W_NO.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_F_NO.Text, out value))
                    {
                        FLEX_SCOTT_F_NO = decimal.Parse(txtFLEX_SCOTT_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_W_NO.Text, out value))
                    {
                        FLEX_SCOTT_W_NO = decimal.Parse(txtFLEX_SCOTT_W_NO.Text);
                    }
                }

                #endregion

                #region Chk

                for (int page = 1; page == 1; page++)
                {
                    if (WIDTH_NO > 1)
                    {
                        chkLoad = false;
                        break;
                    }

                    if (USEWIDTH_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (WIDTHSILICONE_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (NUMTHREADS_F_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (NUMTHREADS_W_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (TOTALWEIGHT_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (UNCOATEDWEIGHT_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (COATWEIGHT_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (THICKNESS_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (MAXFORCE_W_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (MAXFORCE_F_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (ELOGATION_W_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (ELOGATION_F_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (FLAMMABILITY_F_NO > 5) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (FLAMMABILITY_W_NO > 5) 
                    {
                        chkLoad = false;
                        break;
                    }
                    if (EDGECOMB_F_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (EDGECOMB_W_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (STIFFNESS_F_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (STIFFNESS_W_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (TEAR_W_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (TEAR_F_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (STATIC_AIR_NO > 6) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (DYNAMIC_AIR_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (EXPONENT_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (DIMENSCHANGE_F_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (DIMENSCHANGE_W_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (FLEXABRASION_F_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (FLEXABRASION_W_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (BOW_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (SKEW_NO > 3) 
                    {
                        chkLoad = false;
                        break;
                    }

                    if (BENDING_F_NO > 3)
                    {
                        chkLoad = false;
                        break;
                    }

                    if (BENDING_W_NO > 3)
                    {
                        chkLoad = false;
                        break;
                    }

                    if (FLEX_SCOTT_F_NO > 3)
                    {
                        chkLoad = false;
                        break;
                    }

                    if (FLEX_SCOTT_W_NO > 3)
                    {
                        chkLoad = false;
                        break;
                    }
                }
                #endregion

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region chkSpec No N
        private bool chkSpecNoN()
        {
            bool chkLoad = true;

            try
            {
                #region Def

                decimal? WIDTH_NO = null;
                decimal? USEWIDTH_NO = null;
                decimal? WIDTHSILICONE_NO = null;
                decimal? NUMTHREADS_F_NO = null;
                decimal? NUMTHREADS_W_NO = null;
                decimal? TOTALWEIGHT_NO = null;
                decimal? UNCOATEDWEIGHT_NO = null;
                decimal? COATWEIGHT_NO = null;
                decimal? THICKNESS_NO = null;
                decimal? MAXFORCE_W_NO = null;
                decimal? MAXFORCE_F_NO = null;
                decimal? ELOGATION_W_NO = null;
                decimal? ELOGATION_F_NO = null;
                decimal? FLAMMABILITY_F_NO = null;
                decimal? FLAMMABILITY_W_NO = null;

                decimal? EDGECOMB_W_NO = null;
                decimal? EDGECOMB_F_NO = null;

                decimal? STIFFNESS_F_NO = null;
                decimal? STIFFNESS_W_NO = null;
                decimal? TEAR_W_NO = null;
                decimal? TEAR_F_NO = null;
                decimal? STATIC_AIR_NO = null;
                decimal? DYNAMIC_AIR_NO = null;
                decimal? EXPONENT_NO = null;
                decimal? DIMENSCHANGE_F_NO = null;
                decimal? DIMENSCHANGE_W_NO = null;
                decimal? FLEXABRASION_F_NO = null;
                decimal? FLEXABRASION_W_NO = null;
                decimal? BOW_NO = null;
                decimal? SKEW_NO = null;

                decimal? BENDING_W_NO = null;
                decimal? BENDING_F_NO = null;
                decimal? FLEX_SCOTT_W_NO = null;
                decimal? FLEX_SCOTT_F_NO = null;

                #endregion

                #region NO

                if (!string.IsNullOrEmpty(txtWIDTH_NO.Text))
                {
                    if (Decimal.TryParse(txtWIDTH_NO.Text, out value))
                    {
                        WIDTH_NO = decimal.Parse(txtWIDTH_NO.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtUSEWIDTH_NO.Text))
                {
                    if (Decimal.TryParse(txtUSEWIDTH_NO.Text, out value))
                    {
                        USEWIDTH_NO = decimal.Parse(txtUSEWIDTH_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtWIDTHSILICONE_NO.Text))
                {
                    if (Decimal.TryParse(txtWIDTHSILICONE_NO.Text, out value))
                    {
                        WIDTHSILICONE_NO = decimal.Parse(txtWIDTHSILICONE_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtNUMTHREADS_F_NO.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_F_NO.Text, out value))
                    {
                        NUMTHREADS_F_NO = decimal.Parse(txtNUMTHREADS_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtNUMTHREADS_W_NO.Text))
                {
                    if (Decimal.TryParse(txtNUMTHREADS_W_NO.Text, out value))
                    {
                        NUMTHREADS_W_NO = decimal.Parse(txtNUMTHREADS_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTOTALWEIGHT_NO.Text))
                {
                    if (Decimal.TryParse(txtTOTALWEIGHT_NO.Text, out value))
                    {
                        TOTALWEIGHT_NO = decimal.Parse(txtTOTALWEIGHT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHT_NO.Text))
                {
                    if (Decimal.TryParse(txtUNCOATEDWEIGHT_NO.Text, out value))
                    {
                        UNCOATEDWEIGHT_NO = decimal.Parse(txtUNCOATEDWEIGHT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtCOATWEIGHT_NO.Text))
                {
                    if (Decimal.TryParse(txtCOATWEIGHT_NO.Text, out value))
                    {
                        COATWEIGHT_NO = decimal.Parse(txtCOATWEIGHT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTHICKNESS_NO.Text))
                {
                    if (Decimal.TryParse(txtTHICKNESS_NO.Text, out value))
                    {
                        THICKNESS_NO = decimal.Parse(txtTHICKNESS_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtMAXFORCE_W_NO.Text))
                {
                    if (Decimal.TryParse(txtMAXFORCE_W_NO.Text, out value))
                    {
                        MAXFORCE_W_NO = decimal.Parse(txtMAXFORCE_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtMAXFORCE_F_NO.Text))
                {
                    if (Decimal.TryParse(txtMAXFORCE_F_NO.Text, out value))
                    {
                        MAXFORCE_F_NO = decimal.Parse(txtMAXFORCE_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtELOGATION_W_NO.Text))
                {
                    if (Decimal.TryParse(txtELOGATION_W_NO.Text, out value))
                    {
                        ELOGATION_W_NO = decimal.Parse(txtELOGATION_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtELOGATION_F_NO.Text))
                {
                    if (Decimal.TryParse(txtELOGATION_F_NO.Text, out value))
                    {
                        ELOGATION_F_NO = decimal.Parse(txtELOGATION_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_F_NO.Text, out value))
                    {
                        FLAMMABILITY_F_NO = decimal.Parse(txtFLAMMABILITY_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLAMMABILITY_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLAMMABILITY_W_NO.Text, out value))
                    {
                        FLAMMABILITY_W_NO = decimal.Parse(txtFLAMMABILITY_W_NO.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtEDGECOMB_F_NO.Text))
                {
                    if (Decimal.TryParse(txtEDGECOMB_F_NO.Text, out value))
                    {
                        EDGECOMB_F_NO = decimal.Parse(txtEDGECOMB_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtEDGECOMB_W_NO.Text))
                {
                    if (Decimal.TryParse(txtEDGECOMB_W_NO.Text, out value))
                    {
                        EDGECOMB_W_NO = decimal.Parse(txtEDGECOMB_W_NO.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtSTIFFNESS_F_NO.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNESS_F_NO.Text, out value))
                    {
                        STIFFNESS_F_NO = decimal.Parse(txtSTIFFNESS_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtSTIFFNESS_W_NO.Text))
                {
                    if (Decimal.TryParse(txtSTIFFNESS_W_NO.Text, out value))
                    {
                        STIFFNESS_W_NO = decimal.Parse(txtSTIFFNESS_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTEAR_W_NO.Text))
                {
                    if (Decimal.TryParse(txtTEAR_W_NO.Text, out value))
                    {
                        TEAR_W_NO = decimal.Parse(txtTEAR_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtTEAR_F_NO.Text))
                {
                    if (Decimal.TryParse(txtTEAR_F_NO.Text, out value))
                    {
                        TEAR_F_NO = decimal.Parse(txtTEAR_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtSTATIC_AIR_NO.Text))
                {
                    if (Decimal.TryParse(txtSTATIC_AIR_NO.Text, out value))
                    {
                        STATIC_AIR_NO = decimal.Parse(txtSTATIC_AIR_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtDYNAMIC_AIR_NO.Text))
                {
                    if (Decimal.TryParse(txtDYNAMIC_AIR_NO.Text, out value))
                    {
                        DYNAMIC_AIR_NO = decimal.Parse(txtDYNAMIC_AIR_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtEXPONENT_NO.Text))
                {
                    if (Decimal.TryParse(txtEXPONENT_NO.Text, out value))
                    {
                        EXPONENT_NO = decimal.Parse(txtEXPONENT_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_F_NO.Text))
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_F_NO.Text, out value))
                    {
                        DIMENSCHANGE_F_NO = decimal.Parse(txtDIMENSCHANGE_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtDIMENSCHANGE_W_NO.Text))
                {
                    if (Decimal.TryParse(txtDIMENSCHANGE_W_NO.Text, out value))
                    {
                        DIMENSCHANGE_W_NO = decimal.Parse(txtDIMENSCHANGE_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLEXABRASION_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEXABRASION_F_NO.Text, out value))
                    {
                        FLEXABRASION_F_NO = decimal.Parse(txtFLEXABRASION_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLEXABRASION_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEXABRASION_W_NO.Text, out value))
                    {
                        FLEXABRASION_W_NO = decimal.Parse(txtFLEXABRASION_W_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtBOW_NO.Text))
                {
                    if (Decimal.TryParse(txtBOW_NO.Text, out value))
                    {
                        BOW_NO = decimal.Parse(txtBOW_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtSKEW_NO.Text))
                {
                    if (Decimal.TryParse(txtSKEW_NO.Text, out value))
                    {
                        SKEW_NO = decimal.Parse(txtSKEW_NO.Text);
                    }
                }

                //Update 07/07/18
                if (!string.IsNullOrEmpty(txtBENDING_F_NO.Text))
                {
                    if (Decimal.TryParse(txtBENDING_F_NO.Text, out value))
                    {
                        BENDING_F_NO = decimal.Parse(txtBENDING_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtBENDING_W_NO.Text))
                {
                    if (Decimal.TryParse(txtBENDING_W_NO.Text, out value))
                    {
                        BENDING_W_NO = decimal.Parse(txtBENDING_W_NO.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_F_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_F_NO.Text, out value))
                    {
                        FLEX_SCOTT_F_NO = decimal.Parse(txtFLEX_SCOTT_F_NO.Text);
                    }
                }
                if (!string.IsNullOrEmpty(txtFLEX_SCOTT_W_NO.Text))
                {
                    if (Decimal.TryParse(txtFLEX_SCOTT_W_NO.Text, out value))
                    {
                        FLEX_SCOTT_W_NO = decimal.Parse(txtFLEX_SCOTT_W_NO.Text);
                    }
                }

                #endregion

                #region Chk

                for (int page = 1; page == 1; page++)
                {
                    if (!string.IsNullOrEmpty(txtWIDTHSpecification.Text))
                    {
                        if (WIDTH_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtUSEWIDTHSpecification.Text))
                    {
                        if (USEWIDTH_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtWIDTHSILICONESpecification.Text))
                    {
                        if (WIDTHSILICONE_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtNUMTHREADS_FSpecification.Text))
                    {
                        if (NUMTHREADS_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtNUMTHREADS_WSpecification.Text))
                    {
                        if (NUMTHREADS_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtTOTALWEIGHTSpecification.Text))
                    {
                        if (TOTALWEIGHT_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtUNCOATEDWEIGHTSpecification.Text))
                    {
                        if (UNCOATEDWEIGHT_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtCOATINGWEIGHTSpecification.Text))
                    {
                        if (COATWEIGHT_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtTHICKNESSSpecification.Text))
                    {
                        if (THICKNESS_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtMAXFORCE_WSpecification.Text))
                    {
                        if (MAXFORCE_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtMAXFORCE_FSpecification.Text))
                    {
                        if (MAXFORCE_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtELOGATION_WSpecification.Text))
                    {
                        if (ELOGATION_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtELOGATION_FSpecification.Text))
                    {
                        if (ELOGATION_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFLAMMABILITY_FSpecification.Text))
                    {
                        if (FLAMMABILITY_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFLAMMABILITY_WSpecification.Text))
                    {
                        if (FLAMMABILITY_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtELOGATION_FSpecification.Text))
                    {
                        if (ELOGATION_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtELOGATION_WSpecification.Text))
                    {
                        if (ELOGATION_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtSTIFFNES_FSpecification.Text))
                    {
                        if (STIFFNESS_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtSTIFFNES_WSpecification.Text))
                    {
                        if (STIFFNESS_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtTEAR_WSpecification.Text))
                    {
                        if (TEAR_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtTEAR_FSpecification.Text))
                    {
                        if (TEAR_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtSTATIC_AIRSpecification.Text))
                    {
                        if (STATIC_AIR_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtDYNAMIC_AIRSpecification.Text))
                    {
                        if (DYNAMIC_AIR_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtEXPONENTSpecification.Text))
                    {
                        if (EXPONENT_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtDIMENSCHANGE_FSpecification.Text))
                    {
                        if (DIMENSCHANGE_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtDIMENSCHANGE_WSpecification.Text))
                    {
                        if (DIMENSCHANGE_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFLEXABRASION_FSpecification.Text))
                    {
                        if (FLEXABRASION_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFLEXABRASION_WSpecification.Text))
                    {
                        if (FLEXABRASION_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtBOWSpecification.Text))
                    {
                        if (BOW_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtSKEWSpecification.Text))
                    {
                        if (SKEW_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    //Update 07/07/18
                    if (!string.IsNullOrEmpty(txtBENDING_FSpecification.Text))
                    {
                        if (BENDING_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtBENDING_WSpecification.Text))
                    {
                        if (BENDING_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFLEX_SCOTT_FSpecification.Text))
                    {
                        if (FLEX_SCOTT_F_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtFLEX_SCOTT_WSpecification.Text))
                    {
                        if (FLEX_SCOTT_W_NO == 0)
                        {
                            chkLoad = false;
                            break;
                        }
                    }
                }
                #endregion

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region Save
        private bool Save()
        {
            bool chkSave = true;

            try
            {
                #region Def

                string P_ITMCODE = string.Empty;
                decimal? P_WIDTH_NO = null;
                decimal? P_WIDTH = null;
                decimal? P_USEWIDTH_NO = null;
                decimal? P_USEWIDTH = null;
                string P_USEWIDTH_TOR = null;
                decimal? P_WIDTHSILICONE_NO = null;
                decimal? P_WIDTHSILICONE = null;
                string P_WIDTHSILICONE_TOR = string.Empty;
                decimal? P_NUMTHREADS_W_NO = null;
                decimal? P_NUMTHREADS_W = null;
                decimal? P_NUMTHREADS_W_TOR = null;
                decimal? P_NUMTHREADS_F_NO = null;
                decimal? P_NUMTHREADS_F = null;
                decimal? P_NUMTHREADS_F_TOR = null;
                decimal? P_TOTALWEIGHT_NO = null;
                decimal? P_TOTALWEIGHT = null;
                decimal? P_TOTALWEIGHT_TOR = null;
                decimal? P_UNCOATEDWEIGHT_NO = null;
                decimal? P_UNCOATEDWEIGHT = null;
                decimal? P_UNCOATEDWEIGHT_TOR = null;
                decimal? P_COATWEIGHT_NO = null;
                decimal? P_COATWEIGHT = null;
                decimal? P_COATWEIGHT_TOR = null;
                decimal? P_THICKNESS_NO = null;
                decimal? P_THICKNESS = null;
                decimal? P_THICKNESS_TOR = null;
                decimal? P_MAXFORCE_W_NO = null;
                decimal? P_MAXFORCE_W = null;
                string P_MAXFORCE_W_TOR = string.Empty;
                decimal? P_MAXFORCE_F_NO = null;
                decimal? P_MAXFORCE_F = null;
                string P_MAXFORCE_F_TOR = string.Empty;
                decimal? P_ELOGATION_W_NO = null;
                decimal? P_ELOGATION_W = null;
                string P_ELOGATION_W_TOR = null;
                decimal? P_ELOGATION_F_NO = null;
                decimal? P_ELOGATION_F = null;
                string P_ELOGATION_F_TOR = null;
                decimal? P_FLAMMABILITY_W_NO = null;
                decimal? P_FLAMMABILITY_W = null;
                string P_FLAMMABILITY_W_TOR = string.Empty;
                decimal? P_FLAMMABILITY_F_NO = null;
                decimal? P_FLAMMABILITY_F = null;
                string P_FLAMMABILITY_F_TOR = string.Empty;
                decimal? P_EDGECOMB_W_NO = null;
                decimal? P_EDGECOMB_W = null;
                string P_EDGECOMB_W_TOR = string.Empty;
                decimal? P_EDGECOMB_F_NO = null;
                decimal? P_EDGECOMB_F = null;
                string P_EDGECOMB_F_TOR = string.Empty;
                decimal? P_STIFFNESS_W_NO = null;
                decimal? P_STIFFNESS_W = null;
                string P_STIFFNESS_W_TOR = string.Empty;
                decimal? P_STIFFNESS_F_NO = null;
                decimal? P_STIFFNESS_F = null;
                string P_STIFFNESS_F_TOR = string.Empty;
                decimal? P_TEAR_W_NO = null;
                decimal? P_TEAR_W = null;
                string P_TEAR_W_TOR = string.Empty;
                decimal? P_TEAR_F_NO = null;
                decimal? P_TEAR_F = null;
                string P_TEAR_F_TOR = string.Empty;
                decimal? P_STATIC_AIR_NO = null;
                decimal? P_STATIC_AIR = null;
                string P_STATIC_AIR_TOR = string.Empty;
                decimal? P_DYNAMIC_AIR_NO = null;
                decimal? P_DYNAMIC_AIR = null;
                decimal? P_DYNAMIC_AIR_TOR = null;
                decimal? P_EXPONENT_NO = null;
                decimal? P_EXPONENT = null;
                decimal? P_EXPONENT_TOR = null;
                decimal? P_DIMENSCHANGE_W_NO = null;
                decimal? P_DIMENSCHANGE_W = null;
                string P_DIMENSCHANGE_W_TOR = string.Empty;
                decimal? P_DIMENSCHANGE_F_NO = null;
                decimal? P_DIMENSCHANGE_F = null;
                string P_DIMENSCHANGE_F_TOR = string.Empty;
                decimal? P_FLEXABRASION_W_NO = null;
                decimal? P_FLEXABRASION_W = null;
                string P_FLEXABRASION_W_TOR = string.Empty;
                decimal? P_FLEXABRASION_F_NO = null;
                decimal? P_FLEXABRASION_F = null;
                string P_FLEXABRASION_F_TOR = string.Empty;
                decimal? P_BOW_NO = null;
                decimal? P_BOW = null;
                string P_BOW_TOR = string.Empty;
                decimal? P_SKEW_NO = null;
                decimal? P_SKEW = null;
                string P_SKEW_TOR = string.Empty;

                //Update 07/07/18
                decimal? P_BENDING_W_NO = null;
                decimal? P_BENDING_W = null;
                string P_BENDING_W_TOR = string.Empty;
                decimal? P_BENDING_F_NO = null;
                decimal? P_BENDING_F = null;
                string P_BENDING_F_TOR = string.Empty;
                decimal? P_FLEX_SCOTT_W_NO = null;
                decimal? P_FLEX_SCOTT_W = null;
                string P_FLEX_SCOTT_W_TOR = string.Empty;
                decimal? P_FLEX_SCOTT_F_NO = null;
                decimal? P_FLEX_SCOTT_F = null;
                string P_FLEX_SCOTT_F_TOR = string.Empty;
                //if (cbItemCode.SelectedValue != null)
                //    P_ITMCODE = cbItemCode.SelectedValue.ToString();

                P_ITMCODE = _session.P_ITMCODE;
                P_WIDTH_NO = _session.P_WIDTH_NO;
                P_WIDTH = _session.P_WIDTH;
                P_USEWIDTH_NO = _session.P_USEWIDTH_NO;
                P_USEWIDTH = _session.P_USEWIDTH;
                P_USEWIDTH_TOR = _session.P_USEWIDTH_TOR;
                P_WIDTHSILICONE_NO = _session.P_WIDTHSILICONE_NO;
                P_WIDTHSILICONE = _session.P_WIDTHSILICONE;
                P_WIDTHSILICONE_TOR = _session.P_WIDTHSILICONE_TOR;
                P_NUMTHREADS_W_NO = _session.P_NUMTHREADS_W_NO;
                P_NUMTHREADS_W = _session.P_NUMTHREADS_W;
                P_NUMTHREADS_W_TOR = _session.P_NUMTHREADS_W_TOR;
                P_NUMTHREADS_F_NO = _session.P_NUMTHREADS_F_NO;
                P_NUMTHREADS_F = _session.P_NUMTHREADS_F;
                P_NUMTHREADS_F_TOR = _session.P_NUMTHREADS_F_TOR;
                P_TOTALWEIGHT_NO = _session.P_TOTALWEIGHT_NO;
                P_TOTALWEIGHT = _session.P_TOTALWEIGHT;
                P_TOTALWEIGHT_TOR = _session.P_TOTALWEIGHT_TOR;
                P_UNCOATEDWEIGHT_NO = _session.P_UNCOATEDWEIGHT_NO;
                P_UNCOATEDWEIGHT = _session.P_UNCOATEDWEIGHT;
                P_UNCOATEDWEIGHT_TOR = _session.P_UNCOATEDWEIGHT_TOR;
                P_COATWEIGHT_NO = _session.P_COATWEIGHT_NO;
                P_COATWEIGHT = _session.P_COATWEIGHT;
                P_COATWEIGHT_TOR = _session.P_COATWEIGHT_TOR;
                P_THICKNESS_NO = _session.P_THICKNESS_NO;
                P_THICKNESS = _session.P_THICKNESS;
                P_THICKNESS_TOR = _session.P_THICKNESS_TOR;
                P_MAXFORCE_W_NO = _session.P_MAXFORCE_W_NO;
                P_MAXFORCE_W = _session.P_MAXFORCE_W;
                P_MAXFORCE_W_TOR = _session.P_MAXFORCE_W_TOR;
                P_MAXFORCE_F_NO = _session.P_MAXFORCE_F_NO;
                P_MAXFORCE_F = _session.P_MAXFORCE_F;
                P_MAXFORCE_F_TOR = _session.P_MAXFORCE_F_TOR;
                P_ELOGATION_W_NO = _session.P_ELOGATION_W_NO;
                P_ELOGATION_W = _session.P_ELOGATION_W;
                P_ELOGATION_W_TOR = _session.P_ELOGATION_W_TOR;
                P_ELOGATION_F_NO = _session.P_ELOGATION_F_NO;
                P_ELOGATION_F = _session.P_ELOGATION_F;
                P_ELOGATION_F_TOR = _session.P_ELOGATION_F_TOR;
                P_FLAMMABILITY_W_NO = _session.P_FLAMMABILITY_W_NO;
                P_FLAMMABILITY_W = _session.P_FLAMMABILITY_W;
                P_FLAMMABILITY_W_TOR = _session.P_FLAMMABILITY_W_TOR;
                P_FLAMMABILITY_F_NO = _session.P_FLAMMABILITY_F_NO;
                P_FLAMMABILITY_F = _session.P_FLAMMABILITY_F;
                P_FLAMMABILITY_F_TOR = _session.P_FLAMMABILITY_F_TOR;
                P_EDGECOMB_W_NO = _session.P_EDGECOMB_W_NO;
                P_EDGECOMB_W = _session.P_EDGECOMB_W;
                P_EDGECOMB_W_TOR = _session.P_EDGECOMB_W_TOR;
                P_EDGECOMB_F_NO = _session.P_EDGECOMB_F_NO;
                P_EDGECOMB_F = _session.P_EDGECOMB_F;
                P_EDGECOMB_F_TOR = _session.P_EDGECOMB_F_TOR;
                P_STIFFNESS_W_NO = _session.P_STIFFNESS_W_NO;
                P_STIFFNESS_W = _session.P_STIFFNESS_W;
                P_STIFFNESS_W_TOR = _session.P_STIFFNESS_W_TOR;
                P_STIFFNESS_F_NO = _session.P_STIFFNESS_F_NO;
                P_STIFFNESS_F = _session.P_STIFFNESS_F;
                P_STIFFNESS_F_TOR = _session.P_STIFFNESS_F_TOR;
                P_TEAR_W_NO = _session.P_TEAR_W_NO;
                P_TEAR_W = _session.P_TEAR_W;
                P_TEAR_W_TOR = _session.P_TEAR_W_TOR;
                P_TEAR_F_NO = _session.P_TEAR_F_NO;
                P_TEAR_F = _session.P_TEAR_F;
                P_TEAR_F_TOR = _session.P_TEAR_F_TOR;
                P_STATIC_AIR_NO = _session.P_STATIC_AIR_NO;
                P_STATIC_AIR = _session.P_STATIC_AIR;
                P_STATIC_AIR_TOR = _session.P_STATIC_AIR_TOR;
                P_DYNAMIC_AIR_NO = _session.P_DYNAMIC_AIR_NO;
                P_DYNAMIC_AIR = _session.P_DYNAMIC_AIR;
                P_DYNAMIC_AIR_TOR = _session.P_DYNAMIC_AIR_TOR;
                P_EXPONENT_NO = _session.P_EXPONENT_NO;
                P_EXPONENT = _session.P_EXPONENT;
                P_EXPONENT_TOR = _session.P_EXPONENT_TOR;
                P_DIMENSCHANGE_W_NO = _session.P_DIMENSCHANGE_W_NO;
                P_DIMENSCHANGE_W = _session.P_DIMENSCHANGE_W;
                P_DIMENSCHANGE_W_TOR = _session.P_DIMENSCHANGE_W_TOR;
                P_DIMENSCHANGE_F_NO = _session.P_DIMENSCHANGE_F_NO;
                P_DIMENSCHANGE_F = _session.P_DIMENSCHANGE_F;
                P_DIMENSCHANGE_F_TOR = _session.P_DIMENSCHANGE_F_TOR;
                P_FLEXABRASION_W_NO = _session.P_FLEXABRASION_W_NO;
                P_FLEXABRASION_W = _session.P_FLEXABRASION_W;
                P_FLEXABRASION_W_TOR = _session.P_FLEXABRASION_W_TOR;
                P_FLEXABRASION_F_NO = _session.P_FLEXABRASION_F_NO;
                P_FLEXABRASION_F = _session.P_FLEXABRASION_F;
                P_FLEXABRASION_F_TOR = _session.P_FLEXABRASION_F_TOR;
                P_BOW_NO = _session.P_BOW_NO;
                P_BOW = _session.P_BOW;
                P_BOW_TOR = _session.P_BOW_TOR;
                P_SKEW_NO = _session.P_SKEW_NO;
                P_SKEW = _session.P_SKEW;
                P_SKEW_TOR = _session.P_SKEW_TOR;

                //Update 07/07/18
                P_BENDING_W_NO = _session.P_BENDING_W_NO;
                P_BENDING_W = _session.P_BENDING_W;
                P_BENDING_W_TOR = _session.P_BENDING_W_TOR;
                P_BENDING_F_NO = _session.P_BENDING_F_NO;
                P_BENDING_F = _session.P_BENDING_F;
                P_BENDING_F_TOR = _session.P_BENDING_F_TOR;
                P_FLEX_SCOTT_W_NO = _session.P_FLEX_SCOTT_W_NO;
                P_FLEX_SCOTT_W = _session.P_FLEX_SCOTT_W;
                P_FLEX_SCOTT_W_TOR = _session.P_FLEX_SCOTT_W_TOR;
                P_FLEX_SCOTT_F_NO = _session.P_FLEX_SCOTT_F_NO;
                P_FLEX_SCOTT_F = _session.P_FLEX_SCOTT_F;
                P_FLEX_SCOTT_F_TOR = _session.P_FLEX_SCOTT_F_TOR;

                #endregion

                string insert = LabDataPDFDataService.Instance.LAB_INSERTUPDATEITEMSPEC(P_ITMCODE, P_WIDTH_NO, P_WIDTH,
                                 P_USEWIDTH_NO, P_USEWIDTH, P_USEWIDTH_TOR,
                                 P_WIDTHSILICONE_NO, P_WIDTHSILICONE, P_WIDTHSILICONE_TOR,
                                 P_NUMTHREADS_W_NO, P_NUMTHREADS_W, P_NUMTHREADS_W_TOR,
                                 P_NUMTHREADS_F_NO, P_NUMTHREADS_F, P_NUMTHREADS_F_TOR,
                                 P_TOTALWEIGHT_NO, P_TOTALWEIGHT, P_TOTALWEIGHT_TOR,
                                 P_UNCOATEDWEIGHT_NO, P_UNCOATEDWEIGHT, P_UNCOATEDWEIGHT_TOR,
                                 P_COATWEIGHT_NO, P_COATWEIGHT, P_COATWEIGHT_TOR,
                                 P_THICKNESS_NO, P_THICKNESS, P_THICKNESS_TOR,
                                 P_MAXFORCE_W_NO, P_MAXFORCE_W, P_MAXFORCE_W_TOR,
                                 P_MAXFORCE_F_NO, P_MAXFORCE_F, P_MAXFORCE_F_TOR,
                                 P_ELOGATION_W_NO, P_ELOGATION_W, P_ELOGATION_W_TOR,
                                 P_ELOGATION_F_NO, P_ELOGATION_F, P_ELOGATION_F_TOR,
                                 P_FLAMMABILITY_W_NO, P_FLAMMABILITY_W, P_FLAMMABILITY_W_TOR,
                                 P_FLAMMABILITY_F_NO, P_FLAMMABILITY_F, P_FLAMMABILITY_F_TOR,
                                 P_EDGECOMB_W_NO, P_EDGECOMB_W, P_EDGECOMB_W_TOR,
                                 P_EDGECOMB_F_NO, P_EDGECOMB_F, P_EDGECOMB_F_TOR,
                                 P_STIFFNESS_W_NO, P_STIFFNESS_W, P_STIFFNESS_W_TOR,
                                 P_STIFFNESS_F_NO, P_STIFFNESS_F, P_STIFFNESS_F_TOR,
                                 P_TEAR_W_NO, P_TEAR_W, P_TEAR_W_TOR,
                                 P_TEAR_F_NO, P_TEAR_F, P_TEAR_F_TOR,
                                 P_STATIC_AIR_NO, P_STATIC_AIR, P_STATIC_AIR_TOR,
                                 P_DYNAMIC_AIR_NO, P_DYNAMIC_AIR, P_DYNAMIC_AIR_TOR,
                                 P_EXPONENT_NO, P_EXPONENT, P_EXPONENT_TOR,
                                 P_DIMENSCHANGE_W_NO, P_DIMENSCHANGE_W, P_DIMENSCHANGE_W_TOR,
                                 P_DIMENSCHANGE_F_NO, P_DIMENSCHANGE_F, P_DIMENSCHANGE_F_TOR,
                                 P_FLEXABRASION_W_NO, P_FLEXABRASION_W, P_FLEXABRASION_W_TOR,
                                 P_FLEXABRASION_F_NO, P_FLEXABRASION_F, P_FLEXABRASION_F_TOR,
                                 P_BOW_NO, P_BOW, P_BOW_TOR,
                                 P_SKEW_NO, P_SKEW, P_SKEW_TOR,
                                 P_BENDING_W_NO, P_BENDING_W, P_BENDING_W_TOR,
                                 P_BENDING_F_NO, P_BENDING_F, P_BENDING_F_TOR,
                                 P_FLEX_SCOTT_W_NO, P_FLEX_SCOTT_W, P_FLEX_SCOTT_W_TOR,
                                 P_FLEX_SCOTT_F_NO, P_FLEX_SCOTT_F, P_FLEX_SCOTT_F_TOR);

                if (insert == "1")
                {
                    "Save Data Complete".ShowMessageBox();
                    chkSave = true;
                }
                else
                {
                    insert.ShowMessageBox();
                    chkSave = false;
                }

                return chkSave;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdEdit.IsEnabled = enabled;
            cmdSave.IsEnabled = enabled;
        }
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user, string level)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (positionLevel != null)
            {
                positionLevel = level;
            }
        }

        #endregion

    }
}

