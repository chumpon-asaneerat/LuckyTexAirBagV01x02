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
    /// Interaction logic for BeamingProcessPage.xaml
    /// </summary>
    public partial class BeamingProcessPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingProcessPage()
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

        string BeamerNo = string.Empty;
        string ITM_PREPARE = string.Empty;

        string BEAMLOT = string.Empty;
        int? STATUS = 0;
        decimal? TOTALBEAM = null;
        decimal? NOWARPBEAM = null;

        string gridBEAMNO = string.Empty;

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

            if (BeamerNo != "")
                txtBeamerNo.Text = BeamerNo;

            if (ITM_PREPARE != "")
            {
                txtITM_PREPARE.Text = ITM_PREPARE;
                BEAM_GETSPECBYCHOPNO(ITM_PREPARE);
                //GetMCSpeed(ITM_PREPARE);
            }

            if (opera != "")
                txtOperator.Text = opera;

            if (STATUS == 1)
                GetBeamLot(BeamerNo);

            if (!string.IsNullOrEmpty(BeamerNo))
                BEAM_GETINPROCESSLOTBYBEAMNO(BeamerNo);

            txtTENSION_ST1.IsEnabled = false;
            txtTENSION_ST2.IsEnabled = false;
            txtTENSION_ST3.IsEnabled = false;
            txtTENSION_ST4.IsEnabled = false;
            txtTENSION_ST5.IsEnabled = false;
            txtTENSION_ST6.IsEnabled = false;
            txtTENSION_ST7.IsEnabled = false;
            txtTENSION_ST8.IsEnabled = false;
            txtTENSION_ST9.IsEnabled = false;
            txtTENSION_ST10.IsEnabled = false;

            if (TOTALBEAM != null)
            {
                txtTOTALBEAM.Text = TOTALBEAM.Value.ToString("#,##0.##");
            }

            if (NOWARPBEAM != null)
            {
                EnabledTension(NOWARPBEAM);
            }
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
                GetBeamLot(txtBeamerNo.Text, MCno, txtBeamNo.Text, DateTime.Now, opera);

                txtSTARTDATE.Text = DateTime.Now.ToString("dd/MM/yy HH:mm");

                cmdStart.IsEnabled = false;
                cmdDoffing.IsEnabled = true;

                cmdSpecific.IsEnabled = true;

                txtSTANDTENSION.Focus();
                txtSTANDTENSION.SelectAll();
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
            if (!string.IsNullOrEmpty(txtSTANDTENSION.Text)
                && !string.IsNullOrEmpty(txtHARDNESS_L.Text) && !string.IsNullOrEmpty(txtHARDNESS_M.Text) && !string.IsNullOrEmpty(txtHARDNESS_R.Text)
                && !string.IsNullOrEmpty(txtWINDTENSION.Text) && !string.IsNullOrEmpty(txtLENGTH.Text) 
                && !string.IsNullOrEmpty(txtINSIDE.Text) && !string.IsNullOrEmpty(txtOUTSIDE.Text) && !string.IsNullOrEmpty(txtFULL.Text))
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
                        string BeamLot = txtBeamLot.Text;
                        decimal? length = 0;
                        decimal? speed = 0;
                        decimal? hardnessL = 0;
                        decimal? hardnessM = 0;
                        decimal? hardnessR = 0;
                        decimal? STANDTENSION = 0;
                        decimal? WINDTENSION = 0;
                        decimal? INSIDE = 0;
                        decimal? OUTSIDE = 0;
                        decimal? FULL = 0;

                        decimal? tension_ST1 = null;
                        decimal? tension_ST2 = null;
                        decimal? tension_ST3 = null;
                        decimal? tension_ST4 = null;
                        decimal? tension_ST5 = null;
                        decimal? tension_ST6 = null;
                        decimal? tension_ST7 = null;
                        decimal? tension_ST8 = null;
                        decimal? tension_ST9 = null;
                        decimal? tension_ST10 = null;

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
                            if (!string.IsNullOrEmpty(txtSPEED.Text))
                                speed = decimal.Parse(txtSPEED.Text);
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

                        #region STANDTENSION

                        try
                        {
                            if (!string.IsNullOrEmpty(txtSTANDTENSION.Text))
                                STANDTENSION = decimal.Parse(txtSTANDTENSION.Text);
                        }
                        catch
                        {
                            STANDTENSION = 0;
                        }

                        #endregion

                        #region WINDTENSION

                        try
                        {
                            if (!string.IsNullOrEmpty(txtWINDTENSION.Text))
                                WINDTENSION = decimal.Parse(txtWINDTENSION.Text);
                        }
                        catch
                        {
                            WINDTENSION = 0;
                        }

                        #endregion

                        #region INSIDE

                        try
                        {
                            if (!string.IsNullOrEmpty(txtINSIDE.Text))
                                INSIDE = decimal.Parse(txtINSIDE.Text);
                        }
                        catch
                        {
                            INSIDE = 0;
                        }

                        #endregion

                        #region OUTSIDE

                        try
                        {
                            if (!string.IsNullOrEmpty(txtOUTSIDE.Text))
                                OUTSIDE = decimal.Parse(txtOUTSIDE.Text);
                        }
                        catch
                        {
                            OUTSIDE = 0;
                        }

                        #endregion

                        #region FULL

                        try
                        {
                            if (!string.IsNullOrEmpty(txtFULL.Text))
                                FULL = decimal.Parse(txtFULL.Text);
                        }
                        catch
                        {
                            FULL = 0;
                        }

                        #endregion

                        #region tension_ST1

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST1.Text))
                                tension_ST1 = decimal.Parse(txtTENSION_ST1.Text);
                        }
                        catch
                        {
                            tension_ST1 = 0;
                        }

                        #endregion

                        #region tension_ST2

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST2.Text))
                                tension_ST2 = decimal.Parse(txtTENSION_ST2.Text);
                        }
                        catch
                        {
                            tension_ST2 = 0;
                        }

                        #endregion

                        #region tension_ST3

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST3.Text))
                                tension_ST3 = decimal.Parse(txtTENSION_ST3.Text);
                        }
                        catch
                        {
                            tension_ST3 = 0;
                        }

                        #endregion

                        #region tension_ST4

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST4.Text))
                                tension_ST4 = decimal.Parse(txtTENSION_ST4.Text);
                        }
                        catch
                        {
                            tension_ST4 = 0;
                        }

                        #endregion

                        #region tension_ST5

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST5.Text))
                                tension_ST5 = decimal.Parse(txtTENSION_ST5.Text);
                        }
                        catch
                        {
                            tension_ST5 = 0;
                        }

                        #endregion

                        #region tension_ST6

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST6.Text))
                                tension_ST6 = decimal.Parse(txtTENSION_ST6.Text);
                        }
                        catch
                        {
                            tension_ST6 = 0;
                        }

                        #endregion

                        #region tension_ST7

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST7.Text))
                                tension_ST7 = decimal.Parse(txtTENSION_ST7.Text);
                        }
                        catch
                        {
                            tension_ST7 = 0;
                        }

                        #endregion

                        #region tension_ST8

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST8.Text))
                                tension_ST8 = decimal.Parse(txtTENSION_ST8.Text);
                        }
                        catch
                        {
                            tension_ST8 = 0;
                        }

                        #endregion

                        #region tension_ST9

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST9.Text))
                                tension_ST9 = decimal.Parse(txtTENSION_ST9.Text);
                        }
                        catch
                        {
                            tension_ST9 = 0;
                        }

                        #endregion

                        #region tension_ST10

                        try
                        {
                            if (!string.IsNullOrEmpty(txtTENSION_ST10.Text))
                                tension_ST10 = decimal.Parse(txtTENSION_ST10.Text);
                        }
                        catch
                        {
                            tension_ST10 = 0;
                        }

                        #endregion

                        opera = logInfo.UserName;

                        PrintProcess(BeamerNo, BeamLot, length, DateTime.Now, speed, hardnessL, hardnessM, hardnessR
                            , STANDTENSION, WINDTENSION, INSIDE, OUTSIDE, FULL, opera
                            , tension_ST1, tension_ST2, tension_ST3, tension_ST4, tension_ST5
                            , tension_ST6, tension_ST7, tension_ST8, tension_ST9, tension_ST10, opera);

                        txtOperator.Text = opera;

                        txtLENGTH.Text = string.Empty;
                        txtSTARTDATE.Text = string.Empty;

                        //txtSPEED.Text = string.Empty;

                        cmdSpecific.IsEnabled = false;

                        txtBeamNo.Text = string.Empty;
                        txtBeamNo.Focus();
                        txtBeamNo.SelectAll();
                        
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtSTANDTENSION.Text))
                {
                    "Tension Beam Stand can't Null".ShowMessageBox(false);
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
                else if (string.IsNullOrEmpty(txtWINDTENSION.Text))
                {
                    "Winding can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtLENGTH.Text))
                {
                    "Length can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtINSIDE.Text))
                {
                    "Inside can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtOUTSIDE.Text))
                {
                    "OutSide can't Null".ShowMessageBox(false);
                }
                else if (string.IsNullOrEmpty(txtFULL.Text))
                {
                    "Full Beam can't Null".ShowMessageBox(false);
                }
            }
        }

        #endregion

        #region cmdSpecific_Click

        private void cmdSpecific_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBeamLot.Text))
            {
                string BeamLot = txtBeamLot.Text;

                BeamingSpecificMCStop sMC = this.ShowBeamingSpecificMCStopBox(BeamerNo, BeamLot, opera);

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
                     if (!string.IsNullOrEmpty(BeamerNo))
                     {
                         string Chkfinish = "Do you want to finish" + "\r\n" + BeamerNo;

                         if (Chkfinish.ShowMessageOKCancel() == true)
                         {
                             if (Finish(BeamerNo, "F") == true)
                             {
                                 D365_BM();

                                 string finish = "Beam " + BeamerNo + " finish";
                                 finish.ShowMessageBox(false);

                                 PageManager.Instance.Back2();
                             }
                         }
                     }
                 }
             }
        }
        #endregion

        #region cmdEditBeamNo_Click

        private void cmdEditBeamNo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBeamLot.Text) && string.IsNullOrEmpty(BEAMLOT))
            {
                #region EditBeamNoWindow

                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 13; // for inspection
                    string R_OUT = UserDataService.Instance
                          .GetOperatorsDelete(logInfo.UserName, logInfo.Password, processId);

                    if (string.IsNullOrEmpty(R_OUT))
                    {
                        "No Authorize for Edit Length".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        EditBeamNoWindow window = new EditBeamNoWindow();

                        string P_BEAMROLL = string.Empty;
                        string P_BeamerNo = string.Empty;

                        if (!string.IsNullOrEmpty(BEAMLOT) && !string.IsNullOrEmpty(gridBEAMNO))
                        {
                            P_BEAMROLL = BEAMLOT;
                            P_BeamerNo = gridBEAMNO;
                        }
                        else
                        {
                            P_BEAMROLL = txtBeamLot.Text;
                            P_BeamerNo = txtBeamNo.Text;
                        }

                        string operatorid = txtOperator.Text;

                        window.Setup(P_BEAMROLL, P_BeamerNo, operatorid);

                        if (window.ShowDialog() == true)
                        {
                            string NewBeamerNo = window.GetBeamerNo();

                            if (!string.IsNullOrEmpty(NewBeamerNo))
                            {
                                if (NewBeamerNo != P_BeamerNo)
                                {
                                    txtBeamNo.Text = NewBeamerNo;

                                    BEAM_GETINPROCESSLOTBYBEAMNO(txtBeamerNo.Text);

                                    GetBeamLot(BeamerNo);
                                }
                                else
                                {
                                    GetBeamLot(BeamerNo);
                                }
                            }
                            else
                            {
                                GetBeamLot(BeamerNo);
                            }

                            BEAMLOT = string.Empty;
                            gridBEAMNO = string.Empty;
                        }
                    }
                }

                #endregion
            }
            else if (string.IsNullOrEmpty(txtBeamLot.Text) || !string.IsNullOrEmpty(BEAMLOT))
            {
                #region EditBeamNoWindow

                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 13; // for inspection
                    string R_OUT = UserDataService.Instance
                          .GetOperatorsDelete(logInfo.UserName, logInfo.Password, processId);

                    if (string.IsNullOrEmpty(R_OUT))
                    {
                        "No Authorize for Edit Length".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        EditBeamNoWindow window = new EditBeamNoWindow();

                        string P_BEAMROLL = string.Empty;
                        string P_BeamerNo = string.Empty;

                        if (!string.IsNullOrEmpty(BEAMLOT) && !string.IsNullOrEmpty(gridBEAMNO))
                        {
                            P_BEAMROLL = BEAMLOT;
                            P_BeamerNo = gridBEAMNO;
                        }
                        else
                        {
                            P_BEAMROLL = txtBeamLot.Text;
                            P_BeamerNo = txtBeamNo.Text;
                        }

                        string operatorid = txtOperator.Text;

                        window.Setup(P_BEAMROLL, P_BeamerNo, operatorid);

                        if (window.ShowDialog() == true)
                        {
                            GetBeamLot(BeamerNo);

                            BEAMLOT = string.Empty;
                            gridBEAMNO = string.Empty;
                        }
                    }
                }

                #endregion
            }
        }

        #endregion

        #region cmdEdit_Click
        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BEAMLOT))
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
                        BeamingEditWindow window = new BeamingEditWindow();
                        window.Setup(gridBEAMNO, BEAMLOT, logInfo.UserName);

                        if (window.ShowDialog() == true)
                        {
                            GetBeamLot(BeamerNo);
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

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BEAMLOT))
            {
                Print(BEAMLOT);
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
            if (!string.IsNullOrEmpty(BEAMLOT))
            {
                if (this.ShowBeamingMCSTOPReasonBox(BeamerNo, BEAMLOT) == true)
                {

                }
            }
        }

        #endregion

        #region Remark_Click

        private void Remark_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BEAMLOT))
            {
                string remark = BeamingDataService.Instance.BEAM_GETBEAMERROLLREMARK(BEAMLOT);
                if (null == remark)
                    remark = string.Empty;


                RemarkInfo remarkInfo = this.ShowRemarkBox(remark);

                if (null != remarkInfo)
                {
                    BeamingDataService.Instance.BEAM_UPDATEBEAMDETAIL_REMARK(BeamerNo, BEAMLOT, remarkInfo.Remark);
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

        #region txtBeamLot_KeyDown

        private void txtBeamLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSPEED.Focus();
                txtSPEED.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtSPEED_KeyDown

        private void txtSPEED_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSTANDTENSION.Focus();
                txtSTANDTENSION.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtSTANDTENSION_KeyDown
        private void txtSTANDTENSION_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWINDTENSION.Focus();
                txtWINDTENSION.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtWINDTENSION_KeyDown
        private void txtWINDTENSION_KeyDown(object sender, KeyEventArgs e)
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
                txtINSIDE.Focus();
                txtINSIDE.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtINSIDE_KeyDown
        private void txtINSIDE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtOUTSIDE.Focus();
                txtOUTSIDE.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtOUTSIDE_KeyDown
        private void txtOUTSIDE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFULL.Focus();
                txtFULL.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtFULL_KeyDown
        private void txtFULL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTENSION_ST1.Focus();
                txtTENSION_ST1.SelectAll();
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST1_KeyDown
        private void txtTENSION_ST1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST2.IsEnabled == true)
                {
                    txtTENSION_ST2.Focus();
                    txtTENSION_ST2.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST2_KeyDown
        private void txtTENSION_ST2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST3.IsEnabled == true)
                {
                    txtTENSION_ST3.Focus();
                    txtTENSION_ST3.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST3_KeyDown
        private void txtTENSION_ST3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST4.IsEnabled == true)
                {
                    txtTENSION_ST4.Focus();
                    txtTENSION_ST4.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST4_KeyDown
        private void txtTENSION_ST4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST5.IsEnabled == true)
                {
                    txtTENSION_ST5.Focus();
                    txtTENSION_ST5.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST5_KeyDown
        private void txtTENSION_ST5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST6.IsEnabled == true)
                {
                    txtTENSION_ST6.Focus();
                    txtTENSION_ST6.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST6_KeyDown
        private void txtTENSION_ST6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST7.IsEnabled == true)
                {
                    txtTENSION_ST7.Focus();
                    txtTENSION_ST7.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST7_KeyDown
        private void txtTENSION_ST7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST8.IsEnabled == true)
                {
                    txtTENSION_ST8.Focus();
                    txtTENSION_ST8.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST8_KeyDown
        private void txtTENSION_ST8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST9.IsEnabled == true)
                {
                    txtTENSION_ST9.Focus();
                    txtTENSION_ST9.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST9_KeyDown
        private void txtTENSION_ST9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTENSION_ST10.IsEnabled == true)
                {
                    txtTENSION_ST10.Focus();
                    txtTENSION_ST10.SelectAll();
                }
                else
                {
                    txtLENGTH.Focus();
                    txtLENGTH.SelectAll();
                }
                e.Handled = true;
            }
        }
        #endregion

        #region txtTENSION_ST10_KeyDown
        private void txtTENSION_ST10_KeyDown(object sender, KeyEventArgs e)
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

        #region gridBeamer_SelectedCellsChanged

        private void gridBeamer_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridBeamer.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridBeamer);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMLOT != null)
                            {
                                BEAMLOT = ((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMLOT;
                            }
                            else
                            {
                                BEAMLOT = string.Empty;
                            }

                            if (((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMNO != null)
                            {
                                gridBEAMNO = ((LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO)(gridBeamer.CurrentCell.Item)).BEAMNO;
                            }
                            else
                            {
                                gridBEAMNO = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    BEAMLOT = string.Empty;
                    gridBEAMNO = string.Empty;
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

        private void Print(string BEAMLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "BeamTransferSlip";
                ConmonReportService.Instance.BEAMLOT = BEAMLOT;

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

        private void Preview(string BEAMLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "BeamTransferSlip";
                ConmonReportService.Instance.BEAMLOT = BEAMLOT;

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
            txtBeamerNo.Text = string.Empty;
            txtITM_PREPARE.Text = string.Empty;
            txtReedNo.Text = string.Empty;
            txtBeamNo.Text = string.Empty;
            txtSPEED.Text = string.Empty;

            cmdStart.IsEnabled = true;
            cmdDoffing.IsEnabled = false;

            cmdSpecific.IsEnabled = false;

            txtBeamLot.Text = string.Empty;
            txtSTANDTENSION.Text = string.Empty;
            txtHARDNESS_L.Text = string.Empty;
            txtHARDNESS_M.Text = string.Empty;
            txtHARDNESS_R.Text = string.Empty;
            txtWINDTENSION.Text = string.Empty;
            txtLENGTH.Text = string.Empty;
            txtINSIDE.Text = string.Empty;
            txtOUTSIDE.Text = string.Empty;
            txtFULL.Text = string.Empty;

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

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridBeamer.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridBeamer.SelectedItems.Clear();
            else
                this.gridBeamer.SelectedItem = null;

            gridBeamer.ItemsSource = null;

            BEAMLOT = string.Empty;
            gridBEAMNO = string.Empty;

            chkManual.IsChecked = false;

            cmdFinish.IsEnabled = false;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
 
            txtBeamNo.Focus();
            txtBeamNo.SelectAll();
        }

        #endregion

        private void BEAM_GETSPECBYCHOPNO(string ItemCode)
        {
            List<BEAM_GETSPECBYCHOPNO> results = BeamingDataService.Instance.BEAM_GETSPECBYCHOPNO(ItemCode);

            if (results.Count > 0)
            {
                txtLength.Text = results[0].BEAMLENGTH.Value.ToString("#,##0.##");
                txtMCSPEED.Text = "(" + results[0].MINSPEED.Value.ToString("#,##0.##") + " - " + results[0].MAXSPEED.Value.ToString("#,##0.##") + ")";
                txtTensionBeamStand.Text = "(" + results[0].MINYARNTENSION.Value.ToString("#,##0.##") + " - " + results[0].MAXYARNTENSION.Value.ToString("#,##0.##") + ")";
                txtTensionWinding.Text = "(" + results[0].MINWINDTENSION.Value.ToString("#,##0.##") + " - " + results[0].MAXWINDTENSION.Value.ToString("#,##0.##") + ")";
                txtHardness.Text = "(" + results[0].MINHARDNESS.Value.ToString("#,##0.##") + " - " + results[0].MAXHARDNESS.Value.ToString("#,##0.##") + ")";
                txtReedNo.Text = results[0].COMBPITCH.Value.ToString("#,##0.##");
            }
            else
            {
                txtLength.Text = "0";
                txtMCSPEED.Text = "0";
                txtTensionBeamStand.Text = "0";
                txtTensionWinding.Text = "0";
                txtHardness.Text = "0";
                txtReedNo.Text = "0";
            }
        }

        private void EnabledTension(decimal? noWarpbeam)
        {
            if (noWarpbeam == 1)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = false;
                txtTENSION_ST3.IsEnabled = false;
                txtTENSION_ST4.IsEnabled = false;
                txtTENSION_ST5.IsEnabled = false;
                txtTENSION_ST6.IsEnabled = false;
                txtTENSION_ST7.IsEnabled = false;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 2)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = false;
                txtTENSION_ST4.IsEnabled = false;
                txtTENSION_ST5.IsEnabled = false;
                txtTENSION_ST6.IsEnabled = false;
                txtTENSION_ST7.IsEnabled = false;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 3)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = false;
                txtTENSION_ST5.IsEnabled = false;
                txtTENSION_ST6.IsEnabled = false;
                txtTENSION_ST7.IsEnabled = false;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 4)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = false;
                txtTENSION_ST6.IsEnabled = false;
                txtTENSION_ST7.IsEnabled = false;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 5)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = true;
                txtTENSION_ST6.IsEnabled = false;
                txtTENSION_ST7.IsEnabled = false;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 6)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = true;
                txtTENSION_ST6.IsEnabled = true;
                txtTENSION_ST7.IsEnabled = false;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 7)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = true;
                txtTENSION_ST6.IsEnabled = true;
                txtTENSION_ST7.IsEnabled = true;
                txtTENSION_ST8.IsEnabled = false;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 8)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = true;
                txtTENSION_ST6.IsEnabled = true;
                txtTENSION_ST7.IsEnabled = true;
                txtTENSION_ST8.IsEnabled = true;
                txtTENSION_ST9.IsEnabled = false;
                txtTENSION_ST10.IsEnabled = false;
            }
            else if (noWarpbeam == 9)
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = true;
                txtTENSION_ST6.IsEnabled = true;
                txtTENSION_ST7.IsEnabled = true;
                txtTENSION_ST8.IsEnabled = true;
                txtTENSION_ST9.IsEnabled = true;
                txtTENSION_ST10.IsEnabled = false;
            }
            else
            {
                txtTENSION_ST1.IsEnabled = true;
                txtTENSION_ST2.IsEnabled = true;
                txtTENSION_ST3.IsEnabled = true;
                txtTENSION_ST4.IsEnabled = true;
                txtTENSION_ST5.IsEnabled = true;
                txtTENSION_ST6.IsEnabled = true;
                txtTENSION_ST7.IsEnabled = true;
                txtTENSION_ST8.IsEnabled = true;
                txtTENSION_ST9.IsEnabled = true;
                txtTENSION_ST10.IsEnabled = true;
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
                if (TOTALBEAM != null || gridBeamer.Items != null)
                {
                    if (gridBeamer.Items.Count > 0)
                    {
                        decimal? count = gridBeamer.Items.Count;

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

        #region Not Use
        //private void GetMCSpeed(string ITM_PREPARE)
        //{
        //    List<WARP_GETSPECBYCHOPNOANDMC> results = new List<WARP_GETSPECBYCHOPNOANDMC>();
        //    results = WarpingDataService.Instance.WARP_GETSPECBYCHOPNOANDMC(ITM_PREPARE, MCno);

        //    if (results.Count > 0)
        //    {
        //        txtSPEED.Text = results[0].SPEED.Value.ToString("#,##0.##");
        //    }
        //    else
        //    {
        //        txtSPEED.Text = "0";
        //    }
        //}
        #endregion

        private bool GetBeamLot(string P_BEAMERNO, string P_MCNO, string P_BEAMNO, DateTime? P_STARTDATE, string P_STARTBY)
        {
            bool chkBeamLot = true;
            DateTime P_ENDDATE = DateTime.Now;

            if (string.IsNullOrEmpty(BeamingDataService.Instance.BEAM_UPDATEBEAMNO(P_BEAMERNO, P_ENDDATE, "P")))
            {
                string R_BEAMLOT = string.Empty;
                BEAM_INSERTBEAMINGDETAIL result = BeamingDataService.Instance.BEAM_INSERTBEAMINGDETAIL(P_BEAMERNO, P_MCNO, P_BEAMNO, P_STARTDATE, P_STARTBY);

                if (result != null)
                {
                    if (result.RESULT == "Y")
                    {
                        R_BEAMLOT = result.R_BEAMLOT;
                        txtBeamLot.Text = R_BEAMLOT;

                        chkBeamLot = true;
                    }
                    else if (result.RESULT == "N")
                    {
                        result.R_BEAMLOT.ShowMessageBox(true);

                        txtBeamNo.Text = "";
                        txtBeamNo.SelectAll();
                        txtBeamNo.Focus();

                        chkBeamLot = false;
                    }
                }
                else
                {
                    chkBeamLot = false;
                }
            }
            else
            {
                "Beam No Can't Update".ShowMessageBox(true);
                chkBeamLot = false;
            }

            return chkBeamLot;
        }

        private void PrintProcess(string P_BEAMERNO, string P_BEAMLOT, decimal? P_LENGTH, DateTime? P_ENDDATE, decimal? P_SPEED,
            decimal? P_HARDL, decimal? P_HARDN, decimal? P_HARDR, decimal? P_STANDTENSION ,
            decimal? P_WINDTENSION, decimal? P_INSIDE,decimal? P_OUTSIDE , decimal? P_FULL ,
            string P_DOFFBY, decimal? P_TENSION_ST1, decimal? P_TENSION_ST2, decimal? P_TENSION_ST3, decimal? P_TENSION_ST4, decimal? P_TENSION_ST5
            , decimal? P_TENSION_ST6, decimal? P_TENSION_ST7, decimal? P_TENSION_ST8, decimal? P_TENSION_ST9, decimal? P_TENSION_ST10, string P_OPERATOR)
        {
            if (BeamingDataService.Instance.BEAM_UPDATEBEAMDETAIL(P_BEAMERNO, P_BEAMLOT, P_LENGTH, P_ENDDATE, P_SPEED,
             P_HARDL, P_HARDN, P_HARDR, P_STANDTENSION ,
             P_WINDTENSION,  P_INSIDE, P_OUTSIDE ,  P_FULL ,
             P_DOFFBY
             ,  P_TENSION_ST1,  P_TENSION_ST2,  P_TENSION_ST3,  P_TENSION_ST4,  P_TENSION_ST5
            ,  P_TENSION_ST6,  P_TENSION_ST7,  P_TENSION_ST8,  P_TENSION_ST9,  P_TENSION_ST10, P_OPERATOR) == true)
            {
                GetBeamLot(P_BEAMERNO);

                //Print Report รอสรุป
                Print(P_BEAMLOT);

                txtBeamLot.Text = string.Empty;
                txtHARDNESS_L.Text = string.Empty;
                txtHARDNESS_M.Text = string.Empty;
                txtHARDNESS_R.Text = string.Empty;

                txtSTANDTENSION.Text = string.Empty;
                txtWINDTENSION.Text = string.Empty;
                txtINSIDE.Text = string.Empty;
                txtOUTSIDE.Text = string.Empty;
                txtFULL.Text = string.Empty;

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

                cmdStart.IsEnabled = true;
                cmdDoffing.IsEnabled = false;
            }
        }

        private void GetBeamLot(string P_BEAMERNO)
        {
            List<BEAM_GETBEAMLOTBYBEAMERNO> results = new List<BEAM_GETBEAMLOTBYBEAMERNO>();

            results = BeamingDataService.Instance.BEAM_GETBEAMLOTBYBEAMERNO(P_BEAMERNO);

            if (results.Count > 0)
            {
                List<LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO> dataList = new List<LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO dataItemNew = new LuckyTex.Models.BEAM_GETBEAMLOTBYBEAMERNO();

                    dataItemNew.BEAMERNO = results[i].BEAMERNO;
                    dataItemNew.BEAMLOT = results[i].BEAMLOT;
                    dataItemNew.BEAMNO = results[i].BEAMNO;

                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.ENDDATE = results[i].ENDDATE;
                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.SPEED = results[i].SPEED;
                    dataItemNew.HARDNESS_L = results[i].HARDNESS_L;
                    dataItemNew.HARDNESS_N = results[i].HARDNESS_N;
                    dataItemNew.HARDNESS_R = results[i].HARDNESS_R;
                    dataItemNew.BEAMSTANDTENSION = results[i].BEAMSTANDTENSION;

                    dataItemNew.WINDINGTENSION = results[i].WINDINGTENSION;
                    dataItemNew.INSIDE_WIDTH = results[i].INSIDE_WIDTH;
                    dataItemNew.OUTSIDE_WIDTH = results[i].OUTSIDE_WIDTH;
                    dataItemNew.FULL_WIDTH = results[i].FULL_WIDTH;
                    dataItemNew.STARTBY = results[i].STARTBY;
                    dataItemNew.DOFFBY = results[i].DOFFBY;
                    dataItemNew.FLAG = results[i].FLAG;
                    dataItemNew.BEAMMC = results[i].BEAMMC;

                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.REMARK = results[i].REMARK;
                    dataItemNew.TENSION_ST1 = results[i].TENSION_ST1;
                    dataItemNew.TENSION_ST2 = results[i].TENSION_ST2;
                    dataItemNew.TENSION_ST3 = results[i].TENSION_ST3;
                    dataItemNew.TENSION_ST4 = results[i].TENSION_ST4;
                    dataItemNew.TENSION_ST5 = results[i].TENSION_ST5;
                    dataItemNew.TENSION_ST6 = results[i].TENSION_ST6;
                    dataItemNew.TENSION_ST7 = results[i].TENSION_ST7;
                    dataItemNew.TENSION_ST8 = results[i].TENSION_ST8;
                    dataItemNew.TENSION_ST9 = results[i].TENSION_ST9;
                    dataItemNew.TENSION_ST10 = results[i].TENSION_ST10;

                    dataItemNew.EDITDATE = results[i].EDITDATE;
                    dataItemNew.EDITBY = results[i].EDITBY;
                    dataItemNew.OLDBEAMNO = results[i].OLDBEAMNO;
                    dataItemNew.KEBA = results[i].KEBA;
                    dataItemNew.MISSYARN = results[i].MISSYARN;
                    dataItemNew.OTHER = results[i].OTHER;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridBeamer.ItemsSource = dataList;

                #region cmdFinish.IsEnabled

                if (TOTALBEAM != null || gridBeamer.Items != null)
                {
                    if (gridBeamer.Items.Count > 0)
                    {
                        decimal? count = gridBeamer.Items.Count;

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
                if (this.gridBeamer.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridBeamer.SelectedItems.Clear();
                else
                    this.gridBeamer.SelectedItem = null;

                gridBeamer.ItemsSource = null;

                cmdFinish.IsEnabled = false;
            }
        }

        private void BEAM_GETINPROCESSLOTBYBEAMNO(string P_BEAMERNO)
        {
            List<BEAM_GETINPROCESSLOTBYBEAMNO> results = new List<BEAM_GETINPROCESSLOTBYBEAMNO>();

            results = BeamingDataService.Instance.BEAM_GETINPROCESSLOTBYBEAMNO(P_BEAMERNO);

            if (results.Count > 0)
            {

                txtBeamLot.Text = results[0].BEAMLOT;
                txtBeamNo.Text = results[0].BEAMNO;

                if (results[0].STARTDATE != null)
                    txtSTARTDATE.Text = results[0].STARTDATE.Value.ToString("dd/MM/yy HH:mm");

                if (results[0].SPEED != null)
                    txtSPEED.Text = results[0].SPEED.Value.ToString("#,##0.##");

                if (results[0].HARDNESS_L != null)
                    txtHARDNESS_L.Text = results[0].HARDNESS_L.Value.ToString("#,##0.##");

                if (results[0].HARDNESS_N != null)
                    txtHARDNESS_M.Text = results[0].HARDNESS_N.Value.ToString("#,##0.##");

                if (results[0].HARDNESS_R != null)
                    txtHARDNESS_R.Text = results[0].HARDNESS_R.Value.ToString("#,##0.##");

                if (results[0].BEAMSTANDTENSION != null)
                    txtSTANDTENSION.Text = results[0].BEAMSTANDTENSION.Value.ToString("#,##0.##");

                if (results[0].WINDINGTENSION != null)
                    txtWINDTENSION.Text = results[0].WINDINGTENSION.Value.ToString("#,##0.##");

                if (results[0].INSIDE_WIDTH != null)
                    txtINSIDE.Text = results[0].INSIDE_WIDTH.Value.ToString("#,##0.##");

                if (results[0].OUTSIDE_WIDTH != null)
                    txtOUTSIDE.Text = results[0].OUTSIDE_WIDTH.Value.ToString("#,##0.##");

                if (results[0].FULL_WIDTH != null)
                    txtFULL.Text = results[0].FULL_WIDTH.Value.ToString("#,##0.##");

                if (results[0].LENGTH != null)
                    txtLENGTH.Text = results[0].LENGTH.Value.ToString("#,##0.##");

                cmdStart.IsEnabled = false;
                cmdDoffing.IsEnabled = true;

                cmdSpecific.IsEnabled = true;

                txtSPEED.Focus();
                txtSPEED.SelectAll();
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid

                cmdStart.IsEnabled = true;
                cmdDoffing.IsEnabled = false;

                cmdSpecific.IsEnabled = false;
            }
        }

        private bool Finish(string P_BEAMNO, string P_STATUS)
        {
            DateTime P_ENDDATE = DateTime.Now;

            if (string.IsNullOrEmpty(BeamingDataService.Instance.BEAM_UPDATEBEAMNO(P_BEAMNO,P_ENDDATE, P_STATUS)) )
            {
                return true;
            }
            else
                return false;
        }

        #region D365_BM
        private void D365_BM()
        {
            try
            {
                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;

                if (!string.IsNullOrEmpty(BeamerNo))
                {
                    if (D365_BM_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            if (D365_BM_ISH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_BM_ISL(HEADERID) == true)
                                    {
                                        if (D365_BM_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_BM_OPL(HEADERID) == true)
                                                {
                                                    if (D365_BM_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_BM_OUL(HEADERID) == true)
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
                    "Beamer Lot is null".Info();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }
        #endregion

        #region D365_BM_BPO
        private bool D365_BM_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_BPOData> results = new List<ListD365_BM_BPOData>();

                results = D365DataService.Instance.D365_BM_BPO(BeamerNo);

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
                    "D365_BM_BPO Row = 0".Info();
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

        #region D365_BM_ISH
        private bool D365_BM_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_BM_ISHData> results = new List<D365_BM_ISHData>();

                results = D365DataService.Instance.D365_BM_ISH(BeamerNo);

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
                    "D365_BM_ISH Row = 0".Info();
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

        #region D365_BM_ISL
        private bool D365_BM_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_ISLData> results = new List<ListD365_BM_ISLData>();

                results = D365DataService.Instance.D365_BM_ISL(BeamerNo);

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
                    "D365_BM_ISL Row = 0".Info();
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

        #region D365_BM_OPH
        private bool D365_BM_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_BM_OPHData> results = new List<D365_BM_OPHData>();

                results = D365DataService.Instance.D365_BM_OPH(BeamerNo);

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
                    "D365_BM_OPH Row = 0".Info();
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

        #region D365_BM_OPL
        private bool D365_BM_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_OPLData> results = new List<ListD365_BM_OPLData>();

                results = D365DataService.Instance.D365_BM_OPL(BeamerNo);

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
                    "D365_BM_OPL Row = 0".Info();
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

        #region D365_BM_OUH
        private bool D365_BM_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_BM_OUHData> results = new List<D365_BM_OUHData>();

                results = D365DataService.Instance.D365_BM_OUH(BeamerNo);

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
                    "D365_BM_OUH Row = 0".Info();
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

        #region D365_BM_OUL
        private bool D365_BM_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_BM_OULData> results = new List<ListD365_BM_OULData>();

                results = D365DataService.Instance.D365_BM_OUL(BeamerNo);

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
                    "D365_BM_OUL Row = 0".Info();
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
            , string beamerNo, string itm_Prepare, int? status, decimal? totalBeam, decimal? noWarpBeam)
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

            if (BeamerNo != null)
            {
                BeamerNo = beamerNo;
            }

            if (ITM_PREPARE != null)
            {
                ITM_PREPARE = itm_Prepare;
            }

            NOWARPBEAM = noWarpBeam;
            TOTALBEAM = totalBeam;

            STATUS = status;
        }

        #endregion

    }
}
