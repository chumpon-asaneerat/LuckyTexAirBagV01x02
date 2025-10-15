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

using System.Diagnostics;
using System.Printing;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for PackingLabelPage.xaml
    /// </summary>
    public partial class PackingLabelPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PackingLabelPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private PackingSession _session = new PackingSession();
        string opera = string.Empty;
        string cmID = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();
            ConmonReportService.Instance.OperatorID = string.Empty;

            if (opera != "")
            {
                ConmonReportService.Instance.OperatorID = opera;
                txtOperator.Text = opera;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ConmonReportService.Instance.OperatorID = string.Empty;
            ConmonReportService.Instance.INSLOT = string.Empty;
        }

        #endregion

        #region Main Menu Button Handlers
        
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            string INSLOT = string.Empty;
            INSLOT = txtINSPECTIONLOT.Text;

            if (INSLOT != "")
            {
                DateTime? result = CHECKPRINTLABEL(INSLOT);
                if (result == null)
                {
                    if (!string.IsNullOrEmpty(txtITEMCODE.Text))
                    {
                        ConmonReportService.Instance.Printername = "";

                        if (cmID == "09")
                        {
                            Preview2D(INSLOT);
                        }
                        else if (cmID == "08")
                        {
                            PreviewCM08(INSLOT);
                        }
                        else
                        {
                            Preview(INSLOT);
                        }
                    }

                    ClearControl();
                }
                else
                {
                    string temp = "Can't print lable \r\n" + "because this label is printed.\r\n" + "(printed date :" + result.Value.ToString("dd/MM/yy") + ")";
                    temp.ShowMessageBox();

                    ClearControl();
                }
            }
            else
                "Inspection Lot Lot isn't Null".ShowMessageBox(false);
        }

        #endregion

        #region Controls Handlers

        private void txtINSPECTIONLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtINSPECTIONLOT.Text != "")
                {
                    cmdPrint.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtINSPECTIONLOT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtINSPECTIONLOT.Text != "")
            {
                LoadPACK_PRINTLABEL(txtINSPECTIONLOT.Text.Trim());
            }
        }
       
        #endregion

        #region private Methods

        private void ClearControl()
        {
            txtINSPECTIONLOT.Text = "";
            txtITEMCODE.Text = "";
            txtGRADE.Text = "";
            txtQUANTITY.Text = "";
            txtGROSSWEIGHT.Text = "";
            txtNETWEIGHT.Text = "";
            txtDESCRIPTION.Text = "";
            txtSUPPLIERCODE.Text = "";
            txtPDATE.Text = "";
            txtCUSTOMERPARTNO.Text = "";
            txtBATCHNO.Text = "";

            cmID = string.Empty;

            txtINSPECTIONLOT.SelectAll();
            txtINSPECTIONLOT.Focus();
        }

        private void LoadPACK_PRINTLABEL(string INSPECTIONLOT)
        {
            try
            {
                List<PACK_PRINTLABEL> lots = new List<PACK_PRINTLABEL>();

                lots = PackingDataService.Instance.PACK_PRINTLABEL(INSPECTIONLOT);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    txtITEMCODE.Text = lots[0].ITEMCODE;
                    txtGRADE.Text = lots[0].GRADE;
                    txtQUANTITY.Text = lots[0].QUANTITY.Value.ToString("#,##0.##");
                    txtGROSSWEIGHT.Text = lots[0].GROSSWEIGHT.Value.ToString("#,##0.##");
                    txtNETWEIGHT.Text = lots[0].NETWEIGHT.Value.ToString("#,##0.##");
                    txtDESCRIPTION.Text = lots[0].DESCRIPTION;
                    txtSUPPLIERCODE.Text = lots[0].SUPPLIERCODE;
                    txtPDATE.Text = lots[0].PDATE;
                    txtCUSTOMERPARTNO.Text = lots[0].CUSTOMERPARTNO;
                    txtBATCHNO.Text = lots[0].BATCHNO;

                    //New Check
                    cmID = lots[0].CUSTOMERID;
                }
                else
                {
                    cmID = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private DateTime? CHECKPRINTLABEL(string P_INSLOT)
        {
            try
            {
                DateTime? result = null;
                result = PackingDataService.Instance.PACK_CHECKPRINTLABEL(P_INSLOT);

                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return null;
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string INSLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PackingLabel2D";
                //ConmonReportService.Instance.ReportName = "PackingLabel";
                ConmonReportService.Instance.INSLOT = INSLOT;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();

                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region PrintBig

        private void PrintBig(string INSLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PackingLabel2DBig";
                ConmonReportService.Instance.INSLOT = INSLOT;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();

                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string INSLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PackingLabel";
                ConmonReportService.Instance.INSLOT = INSLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview2D

        private void Preview2D(string INSLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PackingLabel2D";
                ConmonReportService.Instance.INSLOT = INSLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region PreviewCM08

        private void PreviewCM08(string INSLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PackingLabelCM08";
                ConmonReportService.Instance.INSLOT = INSLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region PreviewBig

        private void PreviewBig(string INSLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "PackingLabel2DBig";
                ConmonReportService.Instance.INSLOT = INSLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user)
        {
            if (opera != null)
            {
                opera = user;
            }
        }

        #endregion
 
    }
}
