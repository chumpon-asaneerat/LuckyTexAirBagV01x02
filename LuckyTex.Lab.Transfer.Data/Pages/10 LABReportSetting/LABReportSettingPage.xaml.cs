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
    /// Interaction logic for LABReportSettingPage.xaml
    /// </summary>
    public partial class LABReportSettingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LABReportSettingPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemCode();
            LoadCUS_GETLIST();
            LoadLAB_GETREPORTINFO();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string positionLevel = string.Empty;

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
            EnabledControl(true);

            cmdEdit.IsEnabled = false;
            cmdSave.IsEnabled = true;
        }

        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            string itemCode = string.Empty;
            string cmID = string.Empty;
            string repID = string.Empty;

            if (cbItemCode.SelectedValue != null)
                itemCode = cbItemCode.SelectedValue.ToString();

            if (cbCUSTOMERID.SelectedValue != null)
                cmID = cbCUSTOMERID.SelectedValue.ToString();

            if (cbREPORT_ID.SelectedValue != null)
                repID = cbREPORT_ID.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(cmID) && !string.IsNullOrEmpty(repID))
            {
                if (Save() == true)
                {
                    ClearControl();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(itemCode))
                    "Item Code isn't Null".ShowMessageBox();
                else if (string.IsNullOrEmpty(cmID))
                    "Customer isn't Null".ShowMessageBox();
                else if (string.IsNullOrEmpty(cmID))
                    "Report ID isn't Null".ShowMessageBox();
            }
        }
        #endregion

        #region cmdClear_Click
        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
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
        private void txtREVERSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtYARNTYPE.Focus();
                txtYARNTYPE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtYARNTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtWEIGHT.Focus();
                txtWEIGHT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWEIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtCOATWEIGHT.Focus();
                txtCOATWEIGHT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCOATWEIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtNUMTHREADS.Focus();
                txtNUMTHREADS.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNUMTHREADS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtUSEWIDTH.Focus();
                txtUSEWIDTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUSEWIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtTHICKNESS.Focus();
                txtTHICKNESS.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTHICKNESS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtMAXFORCE.Focus();
                txtMAXFORCE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtMAXFORCE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtELOGATION.Focus();
                txtELOGATION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtELOGATION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtFLAMMABILITY.Focus();
                txtFLAMMABILITY.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLAMMABILITY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtEDGECOMB.Focus();
                txtEDGECOMB.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEDGECOMB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtSTIFFNESS.Focus();
                txtSTIFFNESS.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTIFFNESS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtTEAR.Focus();
                txtTEAR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTEAR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtSTATIC_AIR.Focus();
                txtSTATIC_AIR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSTATIC_AIR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtDYNAMIC_AIR.Focus();
                txtDYNAMIC_AIR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDYNAMIC_AIR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtEXPONENT.Focus();
                txtEXPONENT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEXPONENT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtDIMENSCHANGE.Focus();
                txtDIMENSCHANGE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDIMENSCHANGE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtFLEXABRASION.Focus();
                txtFLEXABRASION.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEXABRASION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtFLEX_SCOTT.Focus();
                txtFLEX_SCOTT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFLEX_SCOTT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtBOW.Focus();
                txtBOW.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBOW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtSKEW.Focus();
                txtSKEW.SelectAll();
                e.Handled = true;
            }
        }

        private void txtSKEW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                txtBENDING.Focus();
                txtBENDING.SelectAll();
                e.Handled = true;
            }
        }

        private void txtBENDING_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.RightShift || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
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
                if (cbCUSTOMERID.SelectedValue != null)
                {
                    if (cbREPORT_ID.IsEnabled == false)
                    {
                        string iCode = cbItemCode.SelectedValue.ToString();
                        string cmID = cbCUSTOMERID.SelectedValue.ToString();

                        SearchData(iCode, cmID);
                    }
                }
            }
        }
        #endregion

        #region cbCUSTOMERID_SelectionChanged
        private void cbCUSTOMERID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCUSTOMERID.SelectedValue != null)
            {
                if (cbItemCode.SelectedValue != null)
                {
                    if (cbREPORT_ID.IsEnabled == false)
                    {
                        string iCode = cbItemCode.SelectedValue.ToString();
                        string cmID = cbCUSTOMERID.SelectedValue.ToString();

                        SearchData(iCode, cmID);
                    }
                }
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
            #region Combo

            cbItemCode.SelectedValue = null;
            cbCUSTOMERID.SelectedValue = null;
            cbREPORT_ID.SelectedValue = null;

            #endregion

            txtREVERSION.Text = string.Empty;
            txtYARNTYPE.Text = string.Empty;
            txtWEIGHT.Text = string.Empty;
            txtCOATWEIGHT.Text = string.Empty;
            txtNUMTHREADS.Text = string.Empty;
            txtUSEWIDTH.Text = string.Empty;
            txtTHICKNESS.Text = string.Empty;
            txtMAXFORCE.Text = string.Empty;
            txtELOGATION.Text = string.Empty;
            txtFLAMMABILITY.Text = string.Empty;
            txtEDGECOMB.Text = string.Empty;
            txtSTIFFNESS.Text = string.Empty;
            txtTEAR.Text = string.Empty;
            txtSTATIC_AIR.Text = string.Empty;
            txtDYNAMIC_AIR.Text = string.Empty;
            txtEXPONENT.Text = string.Empty;
            txtDIMENSCHANGE.Text = string.Empty;
            txtFLEXABRASION.Text = string.Empty;

            txtBOW.Text = string.Empty;
            txtSKEW.Text = string.Empty;
            txtFLEX_SCOTT.Text = string.Empty;
            txtBENDING.Text = string.Empty;

            EnabledControl(false);

            cmdEdit.IsEnabled = true;
            cmdSave.IsEnabled = false;

            cbItemCode.SelectedValue = null;
            cbItemCode.Focus();
        }

        #endregion

        #region ClearText
        private void ClearText()
        {
            cbREPORT_ID.SelectedValue = null;

            txtREVERSION.Text = string.Empty;
            txtYARNTYPE.Text = string.Empty;
            txtWEIGHT.Text = string.Empty;
            txtCOATWEIGHT.Text = string.Empty;
            txtNUMTHREADS.Text = string.Empty;
            txtUSEWIDTH.Text = string.Empty;
            txtTHICKNESS.Text = string.Empty;
            txtMAXFORCE.Text = string.Empty;
            txtELOGATION.Text = string.Empty;
            txtFLAMMABILITY.Text = string.Empty;
            txtEDGECOMB.Text = string.Empty;
            txtSTIFFNESS.Text = string.Empty;
            txtTEAR.Text = string.Empty;
            txtSTATIC_AIR.Text = string.Empty;
            txtDYNAMIC_AIR.Text = string.Empty;
            txtEXPONENT.Text = string.Empty;
            txtDIMENSCHANGE.Text = string.Empty;
            txtFLEXABRASION.Text = string.Empty;

            txtBOW.Text = string.Empty;
            txtSKEW.Text = string.Empty;
            txtFLEX_SCOTT.Text = string.Empty;
            txtBENDING.Text = string.Empty;
        }
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

        #region LoadCUS_GETLIST

        private void LoadCUS_GETLIST()
        {
            try
            {
                List<CUS_GETLISTData> cms = LuckyTex.Services.MasterDataService.Instance.GetCUS_GETLISTDataList();

                this.cbCUSTOMERID.ItemsSource = cms;
                this.cbCUSTOMERID.DisplayMemberPath = "CUSTOMERNAME";
                this.cbCUSTOMERID.SelectedValuePath = "CUSTOMERID";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadLAB_GETREPORTINFO

        private void LoadLAB_GETREPORTINFO()
        {
            try
            {
                List<LAB_GETREPORTIDLIST> reps = LabDataPDFDataService.Instance.LAB_GETREPORTIDLIST();

                this.cbREPORT_ID.ItemsSource = reps;
                this.cbREPORT_ID.DisplayMemberPath = "REPORT_NAME";
                this.cbREPORT_ID.SelectedValuePath = "REPORT_ID";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region EnabledControl
        private void EnabledControl(bool chkEna)
        {
            cbREPORT_ID.IsEnabled = chkEna;

            txtREVERSION.IsEnabled = chkEna;
            txtYARNTYPE.IsEnabled = chkEna;
            txtWEIGHT.IsEnabled = chkEna;
            txtCOATWEIGHT.IsEnabled = chkEna;
            txtNUMTHREADS.IsEnabled = chkEna;
            txtUSEWIDTH.IsEnabled = chkEna;
            txtTHICKNESS.IsEnabled = chkEna;
            txtMAXFORCE.IsEnabled = chkEna;
            txtELOGATION.IsEnabled = chkEna;
            txtFLAMMABILITY.IsEnabled = chkEna;
            txtEDGECOMB.IsEnabled = chkEna;
            txtSTIFFNESS.IsEnabled = chkEna;
            txtTEAR.IsEnabled = chkEna;
            txtSTATIC_AIR.IsEnabled = chkEna;
            txtDYNAMIC_AIR.IsEnabled = chkEna;
            txtEXPONENT.IsEnabled = chkEna;
            txtDIMENSCHANGE.IsEnabled = chkEna;
            txtFLEXABRASION.IsEnabled = chkEna;

            txtBOW.IsEnabled = chkEna;
            txtSKEW.IsEnabled = chkEna;
            txtFLEX_SCOTT.IsEnabled = chkEna;

            txtBENDING.IsEnabled = chkEna;
        }
        #endregion

        #region SearchData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <param name="P_CUSTOMERID"></param>
        private void SearchData(string P_ITMCODE, string P_CUSTOMERID)
        {
            List<LAB_GETREPORTINFO> dbResults = LabDataPDFDataService.Instance.LAB_GETREPORTINFO( P_ITMCODE, P_CUSTOMERID);

            if (null != dbResults)
            {
                if (dbResults.Count > 0)
                {
                    cbREPORT_ID.SelectedValue = dbResults[0].REPORT_ID;
                    txtREVERSION.Text = dbResults[0].REVESION;
                    txtYARNTYPE.Text = dbResults[0].YARNTYPE;
                    txtWEIGHT.Text = dbResults[0].WEIGHT;
                    txtCOATWEIGHT.Text = dbResults[0].COATWEIGHT;
                    txtNUMTHREADS.Text = dbResults[0].NUMTHREADS;
                    txtUSEWIDTH.Text = dbResults[0].USABLE_WIDTH;
                    txtTHICKNESS.Text = dbResults[0].THICKNESS;
                    txtMAXFORCE.Text = dbResults[0].MAXFORCE;
                    txtELOGATION.Text = dbResults[0].ELONGATIONFORCE;
                    txtFLAMMABILITY.Text = dbResults[0].FLAMMABILITY;
                    txtEDGECOMB.Text = dbResults[0].EDGECOMB;
                    txtSTIFFNESS.Text = dbResults[0].STIFFNESS;
                    txtTEAR.Text = dbResults[0].TEAR;
                    txtSTATIC_AIR.Text = dbResults[0].STATIC_AIR;
                    txtDYNAMIC_AIR.Text = dbResults[0].DYNAMIC_AIR;
                    txtEXPONENT.Text = dbResults[0].EXPONENT;
                    txtDIMENSCHANGE.Text = dbResults[0].DIMENSCHANGE;
                    txtFLEXABRASION.Text = dbResults[0].FLEXABRASION;

                    txtBOW.Text = dbResults[0].BOW;
                    txtSKEW.Text = dbResults[0].SKEW;
                    txtFLEX_SCOTT.Text = dbResults[0].FLEX_SCOTT;

                    txtBENDING.Text = dbResults[0].BENDING;
                }
                else
                {
                    if (cbREPORT_ID.IsEnabled == false)
                    {
                        string temp = "ไม่พบข้อมูล ตาม Item Code" + "\n\r" + "และ Customer ที่เลือก";
                        temp.ShowMessageBox();
                        ClearText();                      
                    }
                }
            }
            else
            {
                if (cbREPORT_ID.IsEnabled == false)
                {
                    "ไม่พบข้อมูล ตาม Item Code และ Customer ที่เลือก".ShowMessageBox();
                    ClearText();
                }
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
                string P_CUSTOMERID = string.Empty;
                string P_REPORTID = string.Empty;
                string P_REVERSION = string.Empty;
                string P_YARNTYPE = string.Empty;
                string P_WEIGHT = string.Empty;
                string P_COATWEIGHT = string.Empty;
                string P_NUMTHREADS = string.Empty;
                string P_USEWIDTH = string.Empty;
                string P_THICKNESS = string.Empty;
                string P_MAXFORCE = string.Empty;
                string P_ELOGATION = string.Empty;
                string P_FLAMMABILITY = string.Empty;
                string P_EDGECOMB = string.Empty;
                string P_STIFFNESS = string.Empty;
                string P_TEAR = string.Empty;
                string P_STATIC_AIR = string.Empty;
                string P_DYNAMIC_AIR = string.Empty;
                string P_EXPONENT = string.Empty;
                string P_DIMENSCHANGE = string.Empty;
                string P_FLEXABRASION = string.Empty;

                string P_BOW = string.Empty;
                string P_SKEW = string.Empty;
                string P_FLEX_SCOTT = string.Empty;
                string P_BENDING = string.Empty;

                #endregion

                if (cbItemCode.SelectedValue != null)
                    P_ITMCODE = cbItemCode.SelectedValue.ToString();

                if (cbCUSTOMERID.SelectedValue != null)
                    P_CUSTOMERID = cbCUSTOMERID.SelectedValue.ToString();

                if (cbREPORT_ID.SelectedValue != null)
                    P_REPORTID = cbREPORT_ID.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtREVERSION.Text))
                    P_REVERSION = txtREVERSION.Text;

                if (!string.IsNullOrEmpty(txtYARNTYPE.Text))
                    P_YARNTYPE = txtYARNTYPE.Text;

                if (!string.IsNullOrEmpty(txtWEIGHT.Text))
                    P_WEIGHT = txtWEIGHT.Text;

                if (!string.IsNullOrEmpty(txtCOATWEIGHT.Text))
                    P_COATWEIGHT = txtCOATWEIGHT.Text;

                if (!string.IsNullOrEmpty(txtNUMTHREADS.Text))
                    P_NUMTHREADS = txtNUMTHREADS.Text;

                if (!string.IsNullOrEmpty(txtUSEWIDTH.Text))
                    P_USEWIDTH = txtUSEWIDTH.Text;

                if (!string.IsNullOrEmpty(txtTHICKNESS.Text))
                    P_THICKNESS = txtTHICKNESS.Text;

                if (!string.IsNullOrEmpty(txtMAXFORCE.Text))
                    P_MAXFORCE = txtMAXFORCE.Text;

                if (!string.IsNullOrEmpty(txtELOGATION.Text))
                    P_ELOGATION = txtELOGATION.Text;

                if (!string.IsNullOrEmpty(txtFLAMMABILITY.Text))
                    P_FLAMMABILITY = txtFLAMMABILITY.Text;

                if (!string.IsNullOrEmpty(txtEDGECOMB.Text))
                    P_EDGECOMB = txtEDGECOMB.Text;

                if (!string.IsNullOrEmpty(txtSTIFFNESS.Text))
                    P_STIFFNESS = txtSTIFFNESS.Text;

                if (!string.IsNullOrEmpty(txtTEAR.Text))
                    P_TEAR = txtTEAR.Text;

                if (!string.IsNullOrEmpty(txtSTATIC_AIR.Text))
                    P_STATIC_AIR = txtSTATIC_AIR.Text;

                if (!string.IsNullOrEmpty(txtDYNAMIC_AIR.Text))
                    P_DYNAMIC_AIR = txtDYNAMIC_AIR.Text;

                if (!string.IsNullOrEmpty(txtEXPONENT.Text))
                    P_EXPONENT = txtEXPONENT.Text;

                if (!string.IsNullOrEmpty(txtDIMENSCHANGE.Text))
                    P_DIMENSCHANGE = txtDIMENSCHANGE.Text;

                if (!string.IsNullOrEmpty(txtFLEXABRASION.Text))
                    P_FLEXABRASION = txtFLEXABRASION.Text;

                if (!string.IsNullOrEmpty(txtBOW.Text))
                    P_BOW = txtBOW.Text;

                if (!string.IsNullOrEmpty(txtSKEW.Text))
                    P_SKEW = txtSKEW.Text;

                if (!string.IsNullOrEmpty(txtFLEX_SCOTT.Text))
                    P_FLEX_SCOTT = txtFLEX_SCOTT.Text;

                if (!string.IsNullOrEmpty(txtBENDING.Text))
                    P_BENDING = txtBENDING.Text;

                string insert = LabDataPDFDataService.Instance.LAB_INSERTUPDATEREPORTINFO(P_ITMCODE, P_CUSTOMERID, P_REPORTID, P_REVERSION, P_YARNTYPE, P_WEIGHT, P_COATWEIGHT,
                    P_NUMTHREADS, P_USEWIDTH, P_THICKNESS, P_MAXFORCE, P_ELOGATION, P_FLAMMABILITY, P_EDGECOMB, P_STIFFNESS, P_TEAR,
                    P_STATIC_AIR, P_DYNAMIC_AIR, P_EXPONENT, P_DIMENSCHANGE, P_FLEXABRASION, P_BOW, P_SKEW, P_FLEX_SCOTT, P_BENDING);

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

