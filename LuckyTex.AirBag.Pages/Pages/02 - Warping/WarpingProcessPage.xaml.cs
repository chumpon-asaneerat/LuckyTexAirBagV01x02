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

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WarpingProcessPage.xaml
    /// </summary>
    public partial class WarpingProcessPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingProcessPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string MCName = string.Empty;
        string MCno = string.Empty;
        
        string WARPHEADNO = string.Empty;
        string gridWARPHEADNO = string.Empty;

        string ITM_PREPARE = string.Empty;
        string SIDE = string.Empty;
        int? STATUS = 0;
        string WARPERLOT = string.Empty;

        decimal? TOTALBEAM = null;



        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (MCName != "")
                txtMCName.Text = MCName;

            if (WARPHEADNO != "")
                txtWARPHEADNO.Text = WARPHEADNO;

            if (ITM_PREPARE != "")
            {
                txtITM_PREPARE.Text = ITM_PREPARE;
                GetMCSpeed(ITM_PREPARE);
            }

            if (SIDE != "")
                txtSide.Text = SIDE;

            if (opera != "")
                txtOperator.Text = opera;

            if (STATUS == 1)
                GetWarperLot(WARPHEADNO);

            if (!string.IsNullOrEmpty(WARPHEADNO))
                WARP_GETINPROCESSLOTBYHEADNO(WARPHEADNO);
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

        #region cmdStart_Click

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBeamNo.Text))
            {
                if (GetWrapLot(WARPHEADNO, MCno, txtBeamNo.Text, SIDE, DateTime.Now, opera) == true)
                {

                    txtSTARTDATE.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");

                    cmdStart.IsEnabled = false;
                    cmdDoffing.IsEnabled = true;

                    cmdSpecific.IsEnabled = true;

                    txtActual.Focus();
                    txtActual.SelectAll();
                }
                else
                {
                    cmdStart.IsEnabled = true;
                    cmdDoffing.IsEnabled = false;
                }
            }
            else
            {
                "Beam No can't Null".ShowMessageBox(false);

                cmdStart.IsEnabled = true;
                cmdDoffing.IsEnabled = false;
            }
        }

        #endregion

        #region cmdDoffing_Click

        private void cmdDoffing_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtActual.Text)
                && !string.IsNullOrEmpty(txtHARDNESS_L.Text) && !string.IsNullOrEmpty(txtHARDNESS_M.Text) && !string.IsNullOrEmpty(txtHARDNESS_R.Text)
                && !string.IsNullOrEmpty(txtTENSION.Text) && !string.IsNullOrEmpty(txtLENGTH.Text))
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 13; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        string WarperLot = txtWarperLot.Text;
                        decimal? length = 0;
                        decimal? speed = 0;
                        decimal? hardnessL = 0;
                        decimal? hardnessM = 0;
                        decimal? hardnessR = 0;
                        decimal? tension = 0;
                        decimal? tensionIT = 0;
                        decimal? tensionTake = 0;
                        decimal? mcl = 0;
                        decimal? mcs = 0;

                        #region length
                        try
                        {
                            if (!string.IsNullOrEmpty(txtLENGTH.Text))
                                length = decimal.Parse(txtLENGTH.Text);
                        }
                        catch
                        {
                            length = 0;
                        }
                        #endregion

                        #region speed
                        try
                        {
                            if (!string.IsNullOrEmpty(txtActual.Text))
                                speed = decimal.Parse(txtActual.Text);
                        }
                        catch
                        {
                            speed = 0;
                        }
                        #endregion

                        #region hardnessL

                        try
                        {
                            if (!string.IsNullOrEmpty(txtHARDNESS_L.Text))
                                hardnessL = decimal.Parse(txtHARDNESS_L.Text);
                        }
                        catch
                        {
                            hardnessL = 0;
                        }

                        #endregion

                        #region hardnessM

                        try
                        {
                            if (!string.IsNullOrEmpty(txtHARDNESS_M.Text))
                                hardnessM = decimal.Parse(txtHARDNESS_M.Text);
                        }
                        catch
                        {
                            hardnessM = 0;
                        }

                        #endregion

                        #region hardnessR

                        try
                        {
                            if (!string.IsNullOrEmpty(txtHARDNESS_R.Text))
                                hardnessR = decimal.Parse(txtHARDNESS_R.Text);
                        }
                        catch
                        {
                            hardnessR = 0;
                        }

                        #endregion

                        #region tension

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION.Text))
                                tension = decimal.Parse(txtTENSION.Text);
                        }
                        catch
                        {
                            tension = 0;
                        }

                        #endregion

                        #region tensionIT

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_IT.Text))
                                tensionIT = decimal.Parse(txtTENSION_IT.Text);
                        }
                        catch
                        {
                            tensionIT = 0;
                        }

                        #endregion

                        #region tensionTake

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_TAKE.Text))
                                tensionTake = decimal.Parse(txtTENSION_TAKE.Text);
                        }
                        catch
                        {
                            tensionTake = 0;
                        }

                        #endregion

                        #region mcl

                        try
                        {
                            if (!string.IsNullOrEmpty(txtMCL.Text))
                                mcl = decimal.Parse(txtMCL.Text);
                        }
                        catch
                        {
                            mcl = 0;
                        }

                        #endregion

                        #region mcs

                        try
                        {
                            if (!string.IsNullOrEmpty(txtMCS.Text))
                                mcs = decimal.Parse(txtMCS.Text);
                        }
                        catch
                        {
                            mcs = 0;
                        }

                        #endregion

                        opera = logInfo.UserName;

                        PrintProcess(WARPHEADNO, WarperLot, length, DateTime.Now, speed, hardnessL, hardnessM, hardnessR, tension, opera, tensionIT, tensionTake, mcl, mcs);

                        txtOperator.Text = opera;

                        txtLENGTH.Text = string.Empty;
                        txtSTARTDATE.Text = string.Empty;

                        txtMCL.Text = string.Empty;
                        txtMCS.Text = string.Empty;
                        txtTENSION_IT.Text = string.Empty;
                        txtTENSION_TAKE.Text = string.Empty;

                        cmdSpecific.IsEnabled = false;

                        txtBeamNo.Text = string.Empty;
                        txtBeamNo.Focus();
                        txtBeamNo.SelectAll();
                        
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtActual.Text))
                {
                    "Actual can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtHARDNESS_L.Text))
                {
                    "Hardness L can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtHARDNESS_M.Text))
                {
                    "Hardness M can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtHARDNESS_R.Text))
                {
                    "Hardness R can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtTENSION.Text))
                {
                    "Tension can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtLENGTH.Text))
                {
                    "Length can't Null".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #region cmdSpecific_Click

        private void cmdSpecific_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWarperLot.Text))
            {
                string WarperLot = txtWarperLot.Text;

                SpecificMCStop sMC = this.ShowSpecificMCStopBox(WARPHEADNO,WarperLot, opera);

                if (sMC != null)
                {
                    if (sMC.ChkStatus == true)
                    { 
                    
                    }
                }
            }
        }

        #endregion

        #region cmdFinish_Click
        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
             LogInInfo logInfo = this.ShowLogInBox();
             if (logInfo != null)
             {
                 int processId = 13; // for inspection
                 List<LogInResult> operators = UserDataService.Instance
                     .GetOperators(logInfo.UserName, logInfo.Password, processId);

                 if (null == operators || operators.Count <= 0)
                 {
                     "This User can not be Use for This Menu".ShowMessageBox(true);
                     return;
                 }
                 else
                 {

                     if (!string.IsNullOrEmpty(WARPHEADNO))
                     {
                         string Chkfinish = "Do you want to finish" + "\r\n" + WARPHEADNO;

                         if (Chkfinish.ShowMessageOKCancel() == true)
                         {
                             if (Finish(WARPHEADNO, DateTime.Now, "F", opera) == true)
                             {
                                 D365_WP();

                                 string finish = "Warper " + WARPHEADNO + " finish";
                                 finish.ShowMessageBox(false);

                                 PageManager.Instance.Back3();
                             }
                         }

                         #region Old

                         //if (MessageBox.Show("Do you want to finish" + "\r\n" + WARPHEADNO, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                         //{
                         //    if (Finish(WARPHEADNO, DateTime.Now, "F", opera) == true)
                         //    {
                         //        "Finish".ShowMessageBox(false);

                         //        PageManager.Instance.Back3();
                         //        //PageManager.Instance.Current = new WarpingMCMenu();
                         //    }
                         //}

                         #endregion
                     }
                 }
             }
        }
        #endregion

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gridWARPHEADNO) && !string.IsNullOrEmpty(WARPERLOT))
            {
                PrintWarp_tranferSlip(gridWARPHEADNO, WARPERLOT);
            }
            else
            {
                "Please select data in grid".ShowMessageBox();
            }
        }
        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(WARPERLOT))
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 13; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        WarpingEditWindow window = new WarpingEditWindow();
                        window.Setup(gridWARPHEADNO, WARPERLOT, logInfo.UserName);

                        if (window.ShowDialog() == true)
                        {
                            GetWarperLot(WARPHEADNO);
                        }
                    }
                }
            }
            else
            {
                "Please select data in grid".ShowMessageBox();
            }
        }
        #endregion

        #region STOPReason_Click

        private void STOPReason_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(WARPERLOT))
            {
                if (this.ShowMCSTOPReasonBox(WARPHEADNO,WARPERLOT) == true)
                {

                }
            }
        }

        #endregion

        #region Remark_Click

        private void Remark_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(WARPERLOT))
            {
                string remark = WarpingDataService.Instance.WARP_GETWARPERROLLREMARK(WARPERLOT);
                if (null == remark)
                    remark = string.Empty;


                RemarkInfo remarkInfo = this.ShowRemarkBox(remark);

                if (null != remarkInfo)
                {
                   WarpingDataService.Instance.WARP_UPDATEWARPINGPROCESS_REMARK(WARPHEADNO,WARPERLOT, remarkInfo.Remark);
                }
            }
        }

        #endregion

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtBeamNo_KeyDown
        private void txtBeamNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdStart.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWarperLot_KeyDown

        private void txtWarperLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtActual.Focus();
                txtActual.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtActual_KeyDown
        private void txtActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMCL.Focus();
                txtMCL.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtMCL_KeyDown
        private void txtMCL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtMCS.Focus();
                txtMCS.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtMCS_KeyDown
        private void txtMCS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_IT.Focus();
                txtTENSION_IT.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_IT_KeyDown
        private void txtTENSION_IT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_TAKE.Focus();
                txtTENSION_TAKE.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_TAKE_KeyDown
        private void txtTENSION_TAKE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION.Focus();
                txtTENSION.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_KeyDown
        private void txtTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHARDNESS_L.Focus();
                txtHARDNESS_L.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtHARDNESS_L_KeyDown
        private void txtHARDNESS_L_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHARDNESS_M.Focus();
                txtHARDNESS_M.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtHARDNESS_M_KeyDown
        private void txtHARDNESS_M_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHARDNESS_R.Focus();
                txtHARDNESS_R.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtHARDNESS_R_KeyDown
        private void txtHARDNESS_R_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH.Focus();
                txtLENGTH.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtLENGTH_KeyDown
        private void txtLENGTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdDoffing.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region chkManual

        private void chkManual_Checked(object sender, RoutedEventArgs e)
        {
            LogInInfo logInfo = this.ShowLogInBox();
            if (logInfo != null)
            {
                int processId = 13; // for inspection
                List<LogInResult> operators = UserDataService.Instance
                    .GetOperators(logInfo.UserName, logInfo.Password, processId);

                if (null == operators || operators.Count <= 0)
                {
                    "This User can not be Use for This Menu".ShowMessageBox(true);
                    chkManual.IsChecked = false;
                    return;
                }
                else
                {
                    chkMFinish();
                }
            }
        }

        private void chkManual_Unchecked(object sender, RoutedEventArgs e)
        {
            chkMFinish();
        }

        #endregion

        #region GetDataGridRows

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        #endregion

        #region gridWarp_getwarperlotbyheadno_SelectedCellsChanged

        private void gridWarp_getwarperlotbyheadno_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWarp_getwarperlotbyheadno.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWarp_getwarperlotbyheadno);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPHEADNO != null)
                            {
                                gridWARPHEADNO = ((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPHEADNO;
                            }
                            else
                            {
                                gridWARPHEADNO = string.Empty;
                            }

                            if (((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPERLOT != null)
                            {
                                WARPERLOT = ((LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO)(gridWarp_getwarperlotbyheadno.CurrentCell.Item)).WARPERLOT;
                            }
                            else
                            {
                                WARPERLOT = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    gridWARPHEADNO = string.Empty;
                    WARPERLOT = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string WARPHEADNO, string WARPLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "TransferSlip";
                ConmonReportService.Instance.WARPHEADNO = WARPHEADNO;
                ConmonReportService.Instance.WARPLOT = WARPLOT;

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

        #region PrintWarp_tranferSlip

        private void PrintWarp_tranferSlip(string WARPHEADNO , string WARPLOT)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "TransferSlip";
                ConmonReportService.Instance.WARPHEADNO = WARPHEADNO;
                ConmonReportService.Instance.WARPLOT = WARPLOT;

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

        private void Preview(string WARPHEADNO, string WARPLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "TransferSlip";
                ConmonReportService.Instance.WARPHEADNO = WARPHEADNO;
                ConmonReportService.Instance.WARPLOT = WARPLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            txtWARPHEADNO.Text = string.Empty;
            txtITM_PREPARE.Text = string.Empty;
            txtSide.Text = string.Empty;
            txtBeamNo.Text = string.Empty;
            txtSPEED.Text = string.Empty;
            txtDefualtLength.Text = string.Empty;

            cmdStart.IsEnabled = true;
            cmdDoffing.IsEnabled = false;

            cmdSpecific.IsEnabled = false;

            txtWarperLot.Text = string.Empty;
            txtActual.Text = string.Empty;
            txtHARDNESS_L.Text = string.Empty;
            txtHARDNESS_M.Text = string.Empty;
            txtHARDNESS_R.Text = string.Empty;
            txtTENSION.Text = string.Empty;
            txtLENGTH.Text = string.Empty;

            txtMCL.Text = string.Empty;
            txtMCS.Text = string.Empty;
            txtTENSION_IT.Text = string.Empty;
            txtTENSION_TAKE.Text = string.Empty;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWarp_getwarperlotbyheadno.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWarp_getwarperlotbyheadno.SelectedItems.Clear();
            else
                this.gridWarp_getwarperlotbyheadno.SelectedItem = null;

            gridWarp_getwarperlotbyheadno.ItemsSource = null;

            gridWARPHEADNO = string.Empty;
            WARPERLOT = string.Empty;


            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;

            txtBeamNo.Focus();
            txtBeamNo.SelectAll();
        }

        #endregion

        private void GetMCSpeed(string ITM_PREPARE)
        {
            List<WARP_GETSPECBYCHOPNOANDMC> results = new List<WARP_GETSPECBYCHOPNOANDMC>();
            results = WarpingDataService.Instance.WARP_GETSPECBYCHOPNOANDMC(ITM_PREPARE, MCno);

            if (results.Count > 0)
            {
                txtSPEED.Text = results[0].SPEED.Value.ToString("#,##0.##");

                txtDefualtLength.Text = "(" + results[0].MINLENGTH.Value.ToString("#,##0.##") + " - " + results[0].MAXLENGTH.Value.ToString("#,##0.##") + ")";

                TOTALBEAM = results[0].NOWARPBEAM;
            }
            else
            {
                txtSPEED.Text = "0";
                txtDefualtLength.Text = "";
                TOTALBEAM = null;
            }
        }

        private bool GetWrapLot(string P_WARPHEADNO, string P_WARPMC, string P_BEAMNO, string P_SIDE, DateTime? P_STARTDATE, string P_STARTBY)
        {
            bool chkWrapLot = true;

            string R_WRAPLOT = string.Empty;
            WARP_INSERTWARPINGPROCESS result  = WarpingDataService.Instance.WARP_INSERTWARPINGPROCESS(P_WARPHEADNO, P_WARPMC, P_BEAMNO, P_SIDE, P_STARTDATE, P_STARTBY);

            if (result != null)
            {
                if (result.RESULT == "Y")
                {
                    R_WRAPLOT = result.R_WRAPLOT;
                    txtWarperLot.Text = R_WRAPLOT;

                    chkWrapLot = true;
                }
                else if (result.RESULT == "N")
                {
                    result.R_WRAPLOT.ShowMessageBox(true);

                    txtBeamNo.Text = "";
                    txtBeamNo.SelectAll();
                    txtBeamNo.Focus();

                    chkWrapLot = false;
                }
            }
            else
            {
                chkWrapLot = false;
            }

            return chkWrapLot;
        }

        private void PrintProcess(string P_WARPHEADNO, string P_WARPLOT, decimal? P_LENGTH, DateTime? P_ENDDATE, decimal? P_SPEED,
            decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR, decimal? P_TENSION,
            string P_DOFFBY, decimal? P_TENSION_IT, decimal? P_TENSION_TAKE, decimal? P_MCL, decimal? P_MCS)
        {
            if (WarpingDataService.Instance.WARP_UPDATEWARPINGPROCESS(P_WARPHEADNO, P_WARPLOT, P_LENGTH, P_ENDDATE, P_SPEED,
             P_HARDL, P_HARDN, P_HARDR, P_TENSION, P_DOFFBY, P_TENSION_IT,  P_TENSION_TAKE,  P_MCL,  P_MCS) == true)
            {
                GetWarperLot(P_WARPHEADNO);

                //Print Report
                Print(P_WARPHEADNO, P_WARPLOT);

                txtWarperLot.Text = string.Empty;
                txtHARDNESS_L.Text = string.Empty;
                txtHARDNESS_M.Text = string.Empty;
                txtHARDNESS_R.Text = string.Empty;

                txtTENSION.Text = string.Empty;
                txtActual.Text = string.Empty;

                cmdStart.IsEnabled = true;
                cmdDoffing.IsEnabled = false;
            }
        }

        private void chkMFinish()
        {
            if (chkManual.IsChecked == true)
            {
                cmdFinish.IsEnabled = true;
                cmdStart.IsEnabled = true;
            }
            else
            {
                if (TOTALBEAM != null || gridWarp_getwarperlotbyheadno.Items != null)
                {
                    if (gridWarp_getwarperlotbyheadno.Items.Count > 0)
                    {
                        decimal? count = gridWarp_getwarperlotbyheadno.Items.Count;

                        if (count < TOTALBEAM)
                        {
                            cmdFinish.IsEnabled = false;
                            cmdStart.IsEnabled = true;
                        }
                        else
                        {
                            cmdFinish.IsEnabled = true;
                            cmdStart.IsEnabled = false;
                        }
                    }
                    else
                    {
                        cmdFinish.IsEnabled = false;
                        cmdStart.IsEnabled = true;
                    }
                }
                else
                {
                    cmdFinish.IsEnabled = false;
                    cmdStart.IsEnabled = true;
                }
            }
        }

        private void GetWarperLot(string P_WARPHEADNO)
        {
            List<WARP_GETWARPERLOTBYHEADNO> results = new List<WARP_GETWARPERLOTBYHEADNO>();

            results = WarpingDataService.Instance.WARP_GETWARPERLOTBYHEADNO(P_WARPHEADNO);

            if (results.Count > 0)
            {
                List<LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO> dataList = new List<LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO dataItemNew = new LuckyTex.Models.WARP_GETWARPERLOTBYHEADNO();

                    dataItemNew.WARPHEADNO = results[i].WARPHEADNO;
                    dataItemNew.WARPERLOT = results[i].WARPERLOT;
                    dataItemNew.BEAMNO = results[i].BEAMNO;
                    dataItemNew.SIDE = results[i].SIDE;
                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.ENDDATE = results[i].ENDDATE;
                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.SPEED = results[i].SPEED;
                    dataItemNew.HARDNESS_L = results[i].HARDNESS_L;
                    dataItemNew.HARDNESS_N = results[i].HARDNESS_N;
                    dataItemNew.HARDNESS_R = results[i].HARDNESS_R;
                    dataItemNew.TENSION = results[i].TENSION;
                    dataItemNew.STARTBY = results[i].STARTBY;
                    dataItemNew.DOFFBY = results[i].DOFFBY;
                    dataItemNew.FLAG = results[i].FLAG;
                    dataItemNew.WARPMC = results[i].WARPMC;

                    dataItemNew.REMARK = results[i].REMARK;
                    dataItemNew.TENSION_IT = results[i].TENSION_IT;
                    dataItemNew.TENSION_TAKEUP = results[i].TENSION_TAKEUP;
                    dataItemNew.MC_COUNT_L = results[i].MC_COUNT_L;
                    dataItemNew.MC_COUNT_S = results[i].MC_COUNT_S;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridWarp_getwarperlotbyheadno.ItemsSource = dataList;

                #region cmdFinish.IsEnabled

                if (TOTALBEAM != null || gridWarp_getwarperlotbyheadno.Items != null)
                {
                    if (gridWarp_getwarperlotbyheadno.Items.Count > 0)
                    {
                        decimal? count = gridWarp_getwarperlotbyheadno.Items.Count;

                        if (count < TOTALBEAM)
                        {
                            cmdFinish.IsEnabled = false;
                            cmdStart.IsEnabled = true;
                        }
                        else
                        {
                            cmdFinish.IsEnabled = true;
                            cmdStart.IsEnabled = false;
                        }
                    }
                    else
                    {
                        cmdFinish.IsEnabled = false;
                        cmdStart.IsEnabled = true;
                    }
                }
                else
                {
                    cmdFinish.IsEnabled = false;
                    cmdStart.IsEnabled = true;
                }

                #endregion
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWarp_getwarperlotbyheadno.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWarp_getwarperlotbyheadno.SelectedItems.Clear();
                else
                    this.gridWarp_getwarperlotbyheadno.SelectedItem = null;

                gridWarp_getwarperlotbyheadno.ItemsSource = null;
            }
        }

        private bool Finish(string P_WARPHEADNO,  DateTime? P_ENDDATE, string P_STATUS, string P_FINISHBY)
        {
            if (WarpingDataService.Instance.WARP_UPDATESETTINGHEAD(P_WARPHEADNO, P_ENDDATE , P_STATUS, P_FINISHBY) == true)
            {
                return true;
            }
            else
                return false;
        }

        private void WARP_GETINPROCESSLOTBYHEADNO(string P_WARPHEADNO)
        {
            List<WARP_GETINPROCESSLOTBYHEADNO> results = new List<WARP_GETINPROCESSLOTBYHEADNO>();

            results = WarpingDataService.Instance.WARP_GETINPROCESSLOTBYHEADNO(P_WARPHEADNO);

            if (results.Count > 0)
            {
                //List<LuckyTex.Models.WARP_GETINPROCESSLOTBYHEADNO> dataList = new List<LuckyTex.Models.WARP_GETINPROCESSLOTBYHEADNO>();
                //int i = 0;

                //foreach (var row in results)
                //{
                //    LuckyTex.Models.WARP_GETINPROCESSLOTBYHEADNO dataItemNew = new LuckyTex.Models.WARP_GETINPROCESSLOTBYHEADNO();

                //dataItemNew.WARPHEADNO = results[0].WARPHEADNO;
                txtWarperLot.Text = results[0].WARPERLOT;
                txtBeamNo.Text = results[0].BEAMNO;
                //txtSide.Text = results[0].SIDE;

                if (results[0].STARTDATE != null)
                    txtSTARTDATE.Text = results[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");
                //dataItemNew.ENDDATE = results[i].ENDDATE;
                if (results[0].LENGTH != null)
                    txtLENGTH.Text = results[0].LENGTH.Value.ToString("#,##0.##");

                if (results[0].SPEED != null)
                    txtSPEED.Text = results[0].SPEED.Value.ToString("#,##0.##");

                if (results[0].HARDNESS_L != null)
                    txtHARDNESS_L.Text = results[0].HARDNESS_L.Value.ToString("#,##0.##");

                if (results[0].HARDNESS_N != null)
                    txtHARDNESS_M.Text = results[0].HARDNESS_N.Value.ToString("#,##0.##");

                if (results[0].HARDNESS_R != null)
                    txtHARDNESS_R.Text = results[0].HARDNESS_R.Value.ToString("#,##0.##");

                if (results[0].TENSION != null)
                    txtTENSION.Text = results[0].TENSION.Value.ToString("#,##0.##");
                //dataItemNew.STARTBY = results[i].STARTBY;
                //dataItemNew.DOFFBY = results[i].DOFFBY;
                //dataItemNew.FLAG = results[i].FLAG;
                //dataItemNew.WARPMC = results[i].WARPMC;

                //dataItemNew.REMARK = results[i].REMARK;

                //    dataList.Add(dataItemNew);


                //    i++;
                //}

                cmdStart.IsEnabled = false;
                cmdDoffing.IsEnabled = true;

                cmdSpecific.IsEnabled = true;

                txtActual.Focus();
                txtActual.SelectAll();
            }
            else
            {
                cmdStart.IsEnabled = true;
                cmdDoffing.IsEnabled = false;

                cmdSpecific.IsEnabled = false;
            }
        }

        #region D365_WP
        private void D365_WP()
        {
            try
            {
                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;

                if (!string.IsNullOrEmpty(WARPHEADNO))
                {
                    if (D365_WP_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            if (D365_WP_ISH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_WP_ISL(HEADERID) == true)
                                    {
                                        if (D365_WP_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_WP_OPL(HEADERID) == true)
                                                {
                                                    if (D365_WP_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_WP_OUL(HEADERID) == true)
                                                            {
                                                                "Send D365 complete".Info();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            "HEADERID is null".Info();
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                "HEADERID is null".Info();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }
                }
                else
                {
                    "Warper Lot is null".Info();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        
        }
        #endregion

        #region D365_WP_BPO
        private bool D365_WP_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_BPOData> results = new List<ListD365_WP_BPOData>();

                results = D365DataService.Instance.D365_WP_BPO(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                            PRODID = Convert.ToInt64(results[i].PRODID);
                        else
                            PRODID = null;

                        if (!string.IsNullOrEmpty(results[i].LOTNO))
                            P_LOTNO = results[i].LOTNO;
                        else
                            P_LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            P_ITEMID = results[i].ITEMID;
                        else
                            P_ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

                        if (PRODID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_BPO Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_WP_ISH
        private bool D365_WP_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_WP_ISHData> results = new List<D365_WP_ISHData>();

                results = D365DataService.Instance.D365_WP_ISH(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_ISH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_WP_ISL
        private bool D365_WP_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_ISLData> results = new List<ListD365_WP_ISLData>();

                results = D365DataService.Instance.D365_WP_ISL(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string issDate = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].ISSUEDATE != null)
                            issDate = results[i].ISSUEDATE.Value.ToString("yyyy-MM-dd");
                        else
                            issDate = string.Empty;

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, "N", 0, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_ISL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_WP_OPH
        private bool D365_WP_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_WP_OPHData> results = new List<D365_WP_OPHData>();

                results = D365DataService.Instance.D365_WP_OPH(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_OPH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_WP_OPL
        private bool D365_WP_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_OPLData> results = new List<ListD365_WP_OPLData>();

                results = D365DataService.Instance.D365_WP_OPL(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_OPL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_WP_OUH
        private bool D365_WP_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_WP_OUHData> results = new List<D365_WP_OUHData>();

                results = D365DataService.Instance.D365_WP_OUH(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_OUH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_WP_OUL
        private bool D365_WP_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_WP_OULData> results = new List<ListD365_WP_OULData>();

                results = D365DataService.Instance.D365_WP_OUL(WARPHEADNO);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string outputDate = string.Empty;
                    int? finish = null;

                    foreach (var row in results)
                    {
                        if (results[i].OUTPUTDATE != null)
                            outputDate = results[i].OUTPUTDATE.Value.ToString("yyyy-MM-dd");
                        else
                            outputDate = string.Empty;

                        if (results[i].FINISH != null)
                            finish = Convert.ToInt32(results[i].FINISH);
                        else
                            finish = 0;

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);


                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_WP_OUL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user, string mcName, string mcNo
            , string warpheadNo, string itm_Prepare, string side, int? status)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (MCName != null)
            {
                MCName = mcName;
            }

            if (MCno != null)
            {
                MCno = mcNo;
            }

            if (WARPHEADNO != null)
            {
                WARPHEADNO = warpheadNo;
            }

            if (ITM_PREPARE != null)
            {
                ITM_PREPARE = itm_Prepare;
            }

            if (SIDE != null)
            {
                SIDE = side;
            }

            STATUS = status;
        }

        #endregion

    }
}
