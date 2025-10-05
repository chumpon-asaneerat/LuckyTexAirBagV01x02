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
    /// Interaction logic for WeavingProcessPage.xaml
    /// </summary>
    public partial class WeavingProcessPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingProcessPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            cmdPrint.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Internal Variables

        private WeavingSession _session = new WeavingSession();
        string opera = string.Empty;
        string MCNo = string.Empty;

        string strType = string.Empty;
        string productTypeID = string.Empty;
        decimal? gridDOFFNO = null;
        string gridWEAVINGLOT = string.Empty;
        string weavLot = string.Empty;

        string gridSHIFT = string.Empty;
        decimal? gridDENSITY_WARP = null;
        decimal? gridDENSITY_WEFT = null;
        decimal? gridTENSION = null;
        decimal? gridWASTE = null;
        decimal? gridLENGTH = null;

        bool Doff = true;
        bool hideFinish = true;


        string P_BEAMLOT = string.Empty;
        string P_WEAVINGLOT = string.Empty;
        decimal? P_DOFFNO = null;
        string P_LOOMNO = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;
        decimal? P_TOTALRECORD = null;
        bool chkISHRow0 = false;
        bool chkMaxDoffNo = false;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTEMPLE();
            LoadShift();

            if (!string.IsNullOrEmpty(strType))
                LoadItemGood(strType);

            ClearInputs();

            if (opera != "")
            {
                txtOperator.Text = opera;
                txtSettingBy.Text = opera;
            }

            if (!string.IsNullOrEmpty(productTypeID))
            {
                if (productTypeID == "1")
                {
                    rbMassProduction.IsChecked = true;
                    rbTest.IsChecked = false;
                }
                else if (productTypeID == "2")
                {
                    rbMassProduction.IsChecked = false;
                    rbTest.IsChecked = true;
                }
            }

            if (!string.IsNullOrEmpty(MCNo))
            {
                txtMCNo.Text = MCNo;

                if (!string.IsNullOrEmpty(txtMCNo.Text))
                {
                    WEAV_WEAVINGMCSTATUS(txtMCNo.Text);
                }
            }

            if (hideFinish == true)
            {
                cmdFinishBeam.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                cmdFinishBeam.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button

        #region cmdSet_Click

        private void cmdSet_Click(object sender, RoutedEventArgs e)
        {
            string P_BEAMLOT = string.Empty;
            string P_MC = string.Empty;
            string P_ITMWEAVE = string.Empty;
            string P_REEDNO2 = string.Empty;
            string P_WEFTYARN = string.Empty;

            string P_TEMPLE = string.Empty;
            string P_BARNO = string.Empty;
            DateTime? P_STARTDATE = null;
            string P_SETTINGBY = string.Empty;
            string P_PRODUCTTYPE = string.Empty;
            decimal? P_WIDTH = null;
            decimal? P_BEAMLENGTH = null;
            decimal? P_SPEED = null;

            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                P_BEAMLOT = txtBEAMLOT.Text;
                P_MC = txtMCNo.Text;

                if (cbItemCode.SelectedValue != null)
                {
                    P_ITMWEAVE = cbItemCode.SelectedValue.ToString();
                    P_REEDNO2 = txtREEDNO2.Text;

                    if (!string.IsNullOrEmpty(txtWarpYarn.Text))
                    {
                        P_WEFTYARN = txtWarpYarn.Text;

                        if (cbTEMPLE.SelectedValue != null)
                        {
                            P_TEMPLE = cbTEMPLE.SelectedValue.ToString();
                            P_BARNO = txtBARNO.Text;
                        }

                        P_STARTDATE = DateTime.Now;
                        P_SETTINGBY = txtSettingBy.Text;

                        #region P_PRODUCTTYPE

                        if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false)
                        {
                            P_PRODUCTTYPE = "1";
                        }
                        else
                        {
                            P_PRODUCTTYPE = "2";
                        }

                        #endregion

                        if (!string.IsNullOrEmpty(txtWIDTH.Text))
                        {
                            P_WIDTH = decimal.Parse(txtWIDTH.Text);
                        }

                        if (!string.IsNullOrEmpty(txtBeamLength.Text))
                            P_BEAMLENGTH = decimal.Parse(txtBeamLength.Text);

                        if (!string.IsNullOrEmpty(txtSpeed.Text))
                            P_SPEED = decimal.Parse(txtSpeed.Text);

                        if (cbTEMPLE.SelectedValue != null)
                        {
                            if (!string.IsNullOrEmpty(txtWIDTH.Text))
                            {
                                if (!string.IsNullOrEmpty(txtSpeed.Text))
                                {
                                    #region Set

                                    if (P_TEMPLE == "Bar")
                                    {
                                        if (!string.IsNullOrEmpty(P_BARNO))
                                        {
                                            Set(P_BEAMLOT, P_MC, P_ITMWEAVE, P_REEDNO2,
                                                 P_WEFTYARN, P_TEMPLE,
                                                 P_BARNO, P_STARTDATE, P_SETTINGBY, P_PRODUCTTYPE, P_WIDTH, P_BEAMLENGTH, P_SPEED);
                                        }
                                        else
                                        {
                                            "Bar No isn't Null".ShowMessageBox(false);
                                        }
                                    }
                                    else
                                    {
                                        Set(P_BEAMLOT, P_MC, P_ITMWEAVE, P_REEDNO2,
                                               P_WEFTYARN, P_TEMPLE,
                                               P_BARNO, P_STARTDATE, P_SETTINGBY, P_PRODUCTTYPE, P_WIDTH, P_BEAMLENGTH, P_SPEED);
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Speed isn't Null".ShowMessageBox();
                                }
                            }
                            else
                            {
                                "Width isn't Null".ShowMessageBox();
                            }
                        }
                        else
                        {
                            "Temple Type isn't Null".ShowMessageBox();
                        }
                    }
                    else
                    {
                        "Warp Yarn isn't Null".ShowMessageBox(false);
                    }
                }
                else
                {
                    "Item Weaving isn't Null".ShowMessageBox(false);
                }
            }
            else
            {
                "Beam Lot isn't Null".ShowMessageBox(false);
                txtBEAMLOT.SelectAll();
                txtBEAMLOT.Focus();
            }
        }

        #endregion

        #region cmdEdit_Click

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            string P_BEAMLOT = string.Empty;
            string P_REEDNO2 = string.Empty;
            string P_TEMPLE = string.Empty;
            string P_BARNO = string.Empty;
            string P_PRODUCTTYPE = string.Empty;
            decimal? P_WIDTH = null;

            DateTime? P_FINISHDATE = null;
            string P_FLAG = null;

            DateTime? P_EDITDATE = DateTime.Now;
            string P_EDITBY = string.Empty;
            decimal? P_SPEED = null;

            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                P_BEAMLOT = txtBEAMLOT.Text;
                P_REEDNO2 = txtREEDNO2.Text;
                if (cbTEMPLE.SelectedValue != null)
                {
                    P_TEMPLE = cbTEMPLE.SelectedValue.ToString();
                    P_BARNO = txtBARNO.Text;
                }
                if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false)
                {
                    P_PRODUCTTYPE = "1";
                }
                else
                {
                    P_PRODUCTTYPE = "2";
                }
                if (!string.IsNullOrEmpty(txtWIDTH.Text))
                {
                    P_WIDTH = decimal.Parse(txtWIDTH.Text);
                }

                P_EDITBY = txtOperator.Text;

                if (!string.IsNullOrEmpty(txtSpeed.Text))
                    P_SPEED = decimal.Parse(txtSpeed.Text);

                Edit(P_BEAMLOT, P_REEDNO2, P_TEMPLE, P_BARNO, P_PRODUCTTYPE,
                    P_WIDTH, P_FINISHDATE, P_FLAG, P_EDITDATE, P_EDITBY, P_SPEED);
            }
            else
            {
                "Beam Lot isn't Null".ShowMessageBox(false);
                txtBEAMLOT.SelectAll();
                txtBEAMLOT.Focus();
            }
        }

        #endregion

        #region cmdStart_Click

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            string P_BEAMLOT = string.Empty;
            decimal? P_DOFFNO = null;
            string P_ITEMWEAVING = string.Empty;
            decimal? P_LENGHT = null;
            DateTime? P_WEAVINGDATE = null;
            string P_LOOM = string.Empty;
            string P_SHIFT = string.Empty;
            //string P_DENSITY = null;
            decimal? P_TENSION = null;
            string P_REMARK = null;
            string P_OPERATOR = string.Empty;

            decimal? P_SPEED = null;
            decimal? P_WASTE = null;

            decimal? P_DENSITYWARP = null;
            decimal? P_DENSITYWEFT = null;
            DateTime? P_STARTDATE = null;

            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                P_BEAMLOT = txtBEAMLOT.Text;

                if (cbItemCode.SelectedValue != null)
                {
                    P_ITEMWEAVING = cbItemCode.SelectedValue.ToString();
                }

                P_LOOM = txtMCNo.Text;

                if (cbShift.SelectedValue != null)
                {
                    P_SHIFT = cbShift.SelectedValue.ToString();
                }

                if (!string.IsNullOrEmpty(txtDensityWarp.Text))
                    P_DENSITYWARP = decimal.Parse(txtDensityWarp.Text);

                if (!string.IsNullOrEmpty(txtDensityWeft.Text))
                    P_DENSITYWEFT = decimal.Parse(txtDensityWeft.Text);

                txtSTARTBY.Text = txtOperator.Text;
                P_OPERATOR = txtSTARTBY.Text;

                if (!string.IsNullOrEmpty(txtSpeed.Text))
                    P_SPEED = decimal.Parse(txtSpeed.Text);

                if (dteSTARTDATE.SelectedDate != null)
                    P_STARTDATE = Convert.ToDateTime(dteSTARTDATE.SelectedDate.Value.ToString("dd/MM/yy") + " " + DateTime.Now.ToString("HH:mm"));
                else
                {
                    P_STARTDATE = DateTime.Now;
                    dteSTARTDATE.SelectedDate = P_STARTDATE;
                }

                Start(P_BEAMLOT, P_DOFFNO, P_ITEMWEAVING, P_LENGHT, P_WEAVINGDATE, P_LOOM, P_SHIFT,
                       P_DENSITYWARP, P_DENSITYWEFT, P_TENSION, P_SPEED, P_WASTE, P_REMARK, P_OPERATOR, P_STARTDATE);
            }
            else
            {
                "Beam Lot isn't Null".ShowMessageBox(false);
                txtBEAMLOT.SelectAll();
                txtBEAMLOT.Focus();
            }
        }

        #endregion

        #region cmdDoffing_Click

        private void cmdDoffing_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDensityWarp.Text))
            {
                if (!string.IsNullOrEmpty(txtDensityWeft.Text))
                {
                    if (!string.IsNullOrEmpty(txtTension.Text))
                    {
                        if (!string.IsNullOrEmpty(txtSpeed.Text))
                        {
                            if (!string.IsNullOrEmpty(txtLength.Text))
                            {
                                if (!string.IsNullOrEmpty(txtWaste.Text))
                                {
                                    #region Doffing

                                    //LogInInfo logInfo = this.ShowLogInBox();
                                    //if (logInfo != null)
                                    //{
                                    //    int processId = 5; // for inspection
                                    //    List<LogInResult> operators = UserDataService.Instance
                                    //        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                                    //    if (null == operators || operators.Count <= 0)
                                    //    {
                                    //        "This User can not be Use for This Menu".ShowMessageBox(true);
                                    //        return;
                                    //    }
                                    //    else
                                    //    {
                                    string _BEAMLOT = string.Empty;
                                    decimal? _DOFFNO = null;
                                    string P_ITEMWEAVING = string.Empty;
                                    decimal? P_LENGHT = null;
                                    DateTime? P_WEAVINGDATE = null;
                                    string _LOOM = string.Empty;
                                    string P_SHIFT = string.Empty;

                                    decimal? P_TENSION = null;
                                    decimal? P_SPEED = null;
                                    decimal? P_WASTE = null;
                                    string P_REMARK = null;
                                    string P_OPERATOR = string.Empty;

                                    decimal? P_DENSITYWARP = null;
                                    decimal? P_DENSITYWEFT = null;
                                    DateTime? P_STARTDATE = null;

                                    if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
                                    {
                                        _BEAMLOT = txtBEAMLOT.Text;

                                        if (!string.IsNullOrEmpty(txtDOFFNO.Text))
                                        {
                                            _DOFFNO = decimal.Parse(txtDOFFNO.Text);

                                            if (cbItemCode.SelectedValue != null)
                                            {
                                                P_ITEMWEAVING = cbItemCode.SelectedValue.ToString();
                                            }

                                            if (dteWeavingDate.SelectedDate != null)
                                            {
                                                P_WEAVINGDATE = Convert.ToDateTime(dteWeavingDate.SelectedDate.Value.ToString("dd/MM/yy") + " " + DateTime.Now.ToString("HH:mm"));
                                                //P_WEAVINGDATE = dteWeavingDate.SelectedDate;
                                            }
                                            else
                                            {
                                                P_WEAVINGDATE = DateTime.Now;
                                            }

                                            _LOOM = MCNo;

                                            if (cbShift.SelectedValue != null)
                                            {
                                                P_SHIFT = cbShift.SelectedValue.ToString();
                                            }

                                            if (!string.IsNullOrEmpty(txtDensityWarp.Text))
                                                P_DENSITYWARP = decimal.Parse(txtDensityWarp.Text);

                                            if (!string.IsNullOrEmpty(txtDensityWeft.Text))
                                                P_DENSITYWEFT = decimal.Parse(txtDensityWeft.Text);

                                            if (!string.IsNullOrEmpty(txtTension.Text))
                                            {
                                                P_TENSION = decimal.Parse(txtTension.Text);
                                            }

                                            if (!string.IsNullOrEmpty(txtSpeed.Text))
                                            {
                                                P_SPEED = decimal.Parse(txtSpeed.Text);
                                            }

                                            if (!string.IsNullOrEmpty(txtLength.Text))
                                            {
                                                P_LENGHT = decimal.Parse(txtLength.Text);
                                            }

                                            if (!string.IsNullOrEmpty(txtWaste.Text))
                                            {
                                                P_WASTE = decimal.Parse(txtWaste.Text);
                                            }

                                            //P_OPERATOR = operators[0].UserName;
                                            P_OPERATOR = opera;

                                            if (dteSTARTDATE.SelectedDate != null)
                                                P_STARTDATE = Convert.ToDateTime(dteSTARTDATE.SelectedDate.Value.ToString("dd/MM/yy") + " " + DateTime.Now.ToString("HH:mm"));
                                            else
                                            {
                                                P_STARTDATE = DateTime.Now;
                                                dteSTARTDATE.SelectedDate = P_STARTDATE;
                                            }

                                            Doffing(_BEAMLOT, _DOFFNO, P_ITEMWEAVING, P_LENGHT, P_WEAVINGDATE, _LOOM, P_SHIFT,
                                                 P_DENSITYWARP, P_DENSITYWEFT, P_TENSION, P_SPEED, P_WASTE, P_REMARK, P_OPERATOR, P_STARTDATE);
                                        }
                                    }
                                    else
                                    {
                                        "Beamer Roll isn't null".ShowMessageBox();
                                    }

                                    //}
                                    //}

                                    #endregion
                                }
                                else
                                {
                                    "Waste isn't null".ShowMessageBox();

                                    txtWaste.SelectAll();
                                    txtWaste.Focus();
                                }
                            }
                            else
                            {
                                "Length isn't null".ShowMessageBox();

                                txtLength.SelectAll();
                                txtLength.Focus();
                            }
                        }
                        else
                        {
                            "Speed isn't null".ShowMessageBox();

                            txtSpeed.SelectAll();
                            txtSpeed.Focus();
                        }
                    }
                    else
                    {
                        "Tension isn't null".ShowMessageBox();

                        txtTension.SelectAll();
                        txtTension.Focus();
                    }
                }
                else
                {
                    "Density Weft isn't null".ShowMessageBox();

                    txtDensityWeft.SelectAll();
                    txtDensityWeft.Focus();
                }
            }
            else
            {
                "Density Warp isn't null".ShowMessageBox();

                txtDensityWarp.SelectAll();
                txtDensityWarp.Focus();
            }
        }

        #endregion

        #region cmdWeftYarn_Click

        private void cmdWeftYarn_Click(object sender, RoutedEventArgs e)
        {
            WeavingWeftYarnWindow window = new WeavingWeftYarnWindow();

            string BeamerRoll = string.Empty;
            decimal? DeffNo = null;
            string WeftYarn = string.Empty;

            BeamerRoll = txtBEAMLOT.Text;

            try
            {
                if (!string.IsNullOrEmpty(txtDOFFNO.Text))
                    DeffNo = decimal.Parse(txtDOFFNO.Text);
            }
            catch
            {
                DeffNo = null;
            }

            WeftYarn = txtWeftYarn.Text;

            if (!string.IsNullOrEmpty(BeamerRoll) && DeffNo != null)
            {
                string operatorid = txtOperator.Text;

                window.Setup(BeamerRoll, DeffNo, WeftYarn, operatorid, MCNo);

                if (window.ShowDialog() == true)
                {

                }
            }
        }

        #endregion

        #region WeftYarn_Click

        private void WeftYarn_Click(object sender, RoutedEventArgs e)
        {
            WeavingWeftYarnWindow window = new WeavingWeftYarnWindow();

            string BeamerRoll = string.Empty;
            decimal? DeffNo = null;
            string WeftYarn = string.Empty;

            BeamerRoll = txtBEAMLOT.Text;

            DeffNo = gridDOFFNO;

            WeftYarn = txtWeftYarn.Text;

            if (!string.IsNullOrEmpty(BeamerRoll) && DeffNo != null)
            {
                string operatorid = txtOperator.Text;

                window.Setup(BeamerRoll, DeffNo, WeftYarn, operatorid, MCNo);

                if (window.ShowDialog() == true)
                {

                }
            }
        }

        #endregion

        #region MCStop_Click

        private void MCStop_Click(object sender, RoutedEventArgs e)
        {
            WeavingSpecificMCStopWindow window = new WeavingSpecificMCStopWindow();

            string loono = string.Empty;
            string beamLot = string.Empty;
            string doffNo = string.Empty;
            string weavingLot = string.Empty;
            string oper = string.Empty;

            loono = txtMCNo.Text;
            beamLot = txtBEAMLOT.Text;

            if (gridDOFFNO != null)
                doffNo = gridDOFFNO.ToString();

            if (!string.IsNullOrEmpty(gridWEAVINGLOT))
                weavingLot = gridWEAVINGLOT;

            oper = txtOperator.Text;

            if (!string.IsNullOrEmpty(loono) && !string.IsNullOrEmpty(doffNo) && !string.IsNullOrEmpty(beamLot) && !string.IsNullOrEmpty(weavingLot))
            {
                window.Setup(loono, doffNo, beamLot, weavingLot, oper);

                if (window.ShowDialog() == true)
                {

                }
            }
        }

        #endregion

        #region cmdPrint_Click
        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region cmdFinishBeam_Click

        private void cmdFinishBeam_Click(object sender, RoutedEventArgs e)
        {
            string P_BEAMLOT = string.Empty;
            string P_REEDNO2 = null;
            string P_TEMPLE = null;
            string P_BARNO = null;
            string P_PRODUCTTYPE = null;
            decimal? P_WIDTH = null;
            DateTime? P_FINISHDATE = null;
            string P_FLAG = string.Empty;
            DateTime? P_EDITDATE = null;
            string P_EDITBY = string.Empty;
            decimal? P_SPEED = null;

            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                P_BEAMLOT = txtBEAMLOT.Text;
                P_FINISHDATE = DateTime.Now;
                P_FLAG = "1";
                P_EDITBY = txtOperator.Text;

                Finish(P_BEAMLOT, P_REEDNO2, P_TEMPLE, P_BARNO, P_PRODUCTTYPE,
                        P_WIDTH, P_FINISHDATE, P_FLAG, P_EDITDATE, P_EDITBY, P_SPEED);
            }
            else
            {
                "Beam Lot isn't Null".ShowMessageBox(false);

                txtBEAMLOT.SelectAll();
                txtBEAMLOT.Focus();
            }
        }

        #endregion

        #region cmdBeamChange_Click

        private void cmdBeamChange_Click(object sender, RoutedEventArgs e)
        {
            string P_BEAMLOT = string.Empty;

            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
            {
                P_BEAMLOT = txtBEAMLOT.Text;

                if (BeamChange(P_BEAMLOT) == true)
                {
                    PageManager.Instance.Back();
                }
            }
        }

        #endregion

        #region cmdSampling_Click

        private void cmdSampling_Click(object sender, RoutedEventArgs e)
        {
            WeavingSamplingWindow window = new WeavingSamplingWindow();
            string BeamerRoll = string.Empty;
            string itm_Weaving = string.Empty;
            string barno = string.Empty;
            string oper = string.Empty;
            DateTime? stDate = null;

            BeamerRoll = txtBEAMLOT.Text;

            if (cbItemCode.SelectedValue != null)
                itm_Weaving = cbItemCode.SelectedValue.ToString();

            barno = txtBARNO.Text;
            oper = txtOperator.Text;

            if (dteSTARTDATE.SelectedDate != null)
                stDate = dteSTARTDATE.SelectedDate.Value;

            window.Setup(BeamerRoll, MCNo, itm_Weaving, barno, oper, stDate);

            if (window.ShowDialog() == true)
            {

            }

        }

        #endregion

        #region cmdSpecificMCStopReason_Click

        private void cmdSpecificMCStopReason_Click(object sender, RoutedEventArgs e)
        {
            WeavingSpecificMCStopWindow window = new WeavingSpecificMCStopWindow();
            string loono = string.Empty;
            string beamLot = string.Empty;
            string doffNo = string.Empty;
            string weavingLot = string.Empty;
            string oper = string.Empty;

            loono = txtMCNo.Text;
            beamLot = txtBEAMLOT.Text;

            if (!string.IsNullOrEmpty(txtDOFFNO.Text))
                doffNo = txtDOFFNO.Text;

            if (!string.IsNullOrEmpty(weavLot))
                weavingLot = weavLot;

            oper = txtOperator.Text;

            if (!string.IsNullOrEmpty(loono) && !string.IsNullOrEmpty(doffNo) && !string.IsNullOrEmpty(beamLot))
            {
                window.Setup(loono, doffNo, beamLot, weavingLot, oper);

                if (window.ShowDialog() == true)
                {

                }
            }

        }

        #endregion

        #region cmdDelete_Click

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gridWEAVINGLOT))
            {
                EditWeavingLotWindow window = new EditWeavingLotWindow();
                window.Setup(gridWEAVINGLOT, gridSHIFT, gridDENSITY_WARP, gridDENSITY_WEFT, gridTENSION, gridWASTE, gridLENGTH);

                if (window.ShowDialog() == true)
                {
                    WEAV_GETWEAVELISTBYBEAMROLL(txtBEAMLOT.Text, MCNo);
                }
            }
        }

        #endregion

        #endregion

        #region ComboBox

        private void cbItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(strType))
            {
                if (cbItemCode.SelectedValue != null)
                {
                    string ITM_WEAVING = cbItemCode.SelectedValue.ToString();

                    GetITM_YARN(ITM_WEAVING, strType);

                    GetBeamlotDetail();
                }
                else
                {
                    txtWeftYarn.Text = "";
                    txtWarpYarn.Text = "";
                }
            }
            else
            {
                txtWeftYarn.Text = "";
                txtWarpYarn.Text = "";
            }
        }

        #region cmbColors_LostFocus
        private void cmbColors_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbColors.Text == "Orange")
            {
                cmbColors.Background = Brushes.Orange;
                cmbColors.Foreground = Brushes.Orange;
            }
            else if (cmbColors.Text == "Green")
            {
                cmbColors.Background = Brushes.Green;
                cmbColors.Foreground = Brushes.Green;
            }
            else if (cmbColors.Text == "Blue")
            {
                cmbColors.Background = Brushes.Blue;
                cmbColors.Foreground = Brushes.Blue;
            }
            else if (cmbColors.Text == "Gray")
            {
                cmbColors.Background = Brushes.Gray;
                cmbColors.Foreground = Brushes.Gray;
            }
        }
        #endregion

        private void cbTEMPLE_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbTEMPLE.SelectedValue != null)
            {
                string temple = cbTEMPLE.SelectedValue.ToString();

                if (temple == "Bar")
                {
                    txtBARNO.IsEnabled = true;
                }
                else
                {
                    txtBARNO.IsEnabled = false;
                }
            }
            else
            {
                txtBARNO.IsEnabled = false;
            }
        }

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtBEAMLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
                {
                    txtREEDNO2.SelectAll();
                    txtREEDNO2.Focus();

                    e.Handled = true;
                }
            }
        }

        private void txtBEAMLOT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBEAMLOT.Text))
                GetBeamlotDetail();
        }

        private void txtBeamLength_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtREEDNO2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (cbTEMPLE.SelectedValue != null)
                {
                    string temple = cbTEMPLE.SelectedValue.ToString();

                    if (temple == "Bar")
                    {
                        txtBARNO.SelectAll();
                        txtBARNO.Focus();
                    }
                    else
                    {
                        txtWIDTH.SelectAll();
                        txtWIDTH.Focus();
                    }
                }
                else
                {
                    txtWIDTH.SelectAll();
                    txtWIDTH.Focus();
                }

                e.Handled = true;
            }
        }

        private void txtBARNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWIDTH.SelectAll();
                txtWIDTH.Focus();

                e.Handled = true;
            }
        }

        private void txtWIDTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtSpeed.SelectAll();
                txtSpeed.Focus();

                e.Handled = true;
            }
        }

        private void txtSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSet.Focus();

                e.Handled = true;
            }
        }

        private void txtDensityWarp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDensityWeft.SelectAll();
                txtDensityWeft.Focus();

                e.Handled = true;
            }
        }

        private void txtDensityWeft_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTension.SelectAll();
                txtTension.Focus();

                e.Handled = true;
            }
        }

        private void txtTension_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength.SelectAll();
                txtLength.Focus();

                e.Handled = true;
            }
        }

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWaste.SelectAll();
                txtWaste.Focus();

                e.Handled = true;
            }
        }

        private void txtWaste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdDoffing.Focus();

                e.Handled = true;
            }
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

        #region gridWeaving_SelectedCellsChanged

        private void gridWeaving_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWeaving.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWeaving);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            gridDOFFNO = null;
                            gridWEAVINGLOT = string.Empty;
                            gridSHIFT = string.Empty;
                            gridDENSITY_WARP = null;
                            gridDENSITY_WEFT = null;
                            gridTENSION = null;
                            gridWASTE = null;
                            gridLENGTH = null;

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).DOFFNO != null)
                            {
                                gridDOFFNO = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).DOFFNO;
                            }
                            else
                            {
                                gridDOFFNO = null;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                gridWEAVINGLOT = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).WEAVINGLOT;
                            }
                            else
                            {
                                gridWEAVINGLOT = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).SHIFT != null)
                            {
                                gridSHIFT = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).SHIFT;
                            }
                            else
                            {
                                gridSHIFT = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).DENSITY_WARP != null)
                            {
                                gridDENSITY_WARP = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).DENSITY_WARP;
                            }
                            else
                            {
                                gridDENSITY_WARP = null;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).DENSITY_WEFT != null)
                            {
                                gridDENSITY_WEFT = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).DENSITY_WEFT;
                            }
                            else
                            {
                                gridDENSITY_WEFT = null;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).TENSION != null)
                            {
                                gridTENSION = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).TENSION;
                            }
                            else
                            {
                                gridTENSION = null;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).WASTE != null)
                            {
                                gridWASTE = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).WASTE;
                            }
                            else
                            {
                                gridWASTE = null;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).LENGTH != null)
                            {
                                gridLENGTH = ((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(gridWeaving.CurrentCell.Item)).LENGTH;
                            }
                            else
                            {
                                gridLENGTH = null;
                            }
                        }
                    }
                }
                else
                {
                    gridDOFFNO = null;
                    gridWEAVINGLOT = string.Empty;
                    gridSHIFT = string.Empty;
                    gridDENSITY_WARP = null;
                    gridDENSITY_WEFT = null;
                    gridTENSION = null;
                    gridWASTE = null;
                    gridLENGTH = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region gridWeaving_LoadingRow
        private void gridWeaving_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(e.Row.DataContext)).DELETEFLAG))
                {
                    if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(e.Row.DataContext)).DELETEFLAG == "0")
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL)(e.Row.DataContext)).DELETEFLAG == "1")
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Black);
                    }
                    else 
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            } 
        }
        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string WEAVINGLOT, string ChinaFabric)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WeavingProcess";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.CHINA = ChinaFabric;


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

        private void Preview(string WEAVINGLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WeavingProcess";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;

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

        #region LoadItemGood

        private void LoadItemGood(string type)
        {
            try
            {
                List<WEAV_GETITEMWEAVINGLIST> items = _session.GetItemCodeData(type);

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_WEAVING";
                this.cbItemCode.SelectedValuePath = "ITM_WEAVING";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadTEMPLE

        private void LoadTEMPLE()
        {
            if (cbTEMPLE.ItemsSource == null)
            {
                string[] str = new string[] { "Bar", "Ring" };

                cbTEMPLE.ItemsSource = str;
                cbTEMPLE.SelectedIndex = 0;
            }
        }

        #endregion

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

        #region GetITM_YARN

        private void GetITM_YARN(string ITM_WEAVING, string type)
        {
            List<WEAV_GETITEMWEAVINGLIST> items = _session.GetItemCodeData(ITM_WEAVING, type);

            if (items != null && items.Count > 0)
            {
                txtWeftYarn.Text = items[0].ITM_YARN;
                txtWarpYarn.Text = items[0].ITM_YARN;
            }
            else
            {
                "Can not process this item Weaving on this MC".ShowMessageBox(true);

                txtWeftYarn.Text = "";
                txtWarpYarn.Text = "";

                cbItemCode.SelectedValue = null;
            }
        }

        #endregion

        #region GetBeamlotDetail

        private void GetBeamlotDetail()
        {
            if (!string.IsNullOrEmpty(txtBEAMLOT.Text) && cbItemCode.SelectedValue != null)
            {
                string P_ITMWEAVING = cbItemCode.SelectedValue.ToString();

                weave_getbeamlotdetail(txtBEAMLOT.Text, P_ITMWEAVING);
            }
        }

        #endregion

        #region weave_getbeamlotdetail

        private void weave_getbeamlotdetail(string P_BEAMLOT, string P_ITMWEAVING)
        {
            List<WEAVE_GETBEAMLOTDETAIL> items = _session.GetWEAVE_GETBEAMLOTDETAIL(P_BEAMLOT);

            if (items != null && items.Count > 0)
            {
                //เพิ่ม 01/09/17
                if (!string.IsNullOrEmpty(items[0].RESULT))
                {
                    items[0].RESULT.ToString().ShowMessageBox(true);

                    txtBeamLength.Text = string.Empty;
                    txtREEDNO.Text = string.Empty;
                    cmbColors.Text = "";
                    cmbColors.Background = null;
                    cmbColors.Foreground = null;

                    txtBEAMLOT.Text = "";
                    txtBEAMLOT.SelectAll();
                    txtBEAMLOT.Focus();
                }
                else
                {
                    string P_ITMPREPARE = items[0].ITM_PREPARE;
                    txtBeamLength.Text = items[0].LENGTH.Value.ToString("#,##0.##");
                    txtREEDNO.Text = items[0].REEDNO;

                    string StrColors = items[0].HEALDCOLOR;

                    if (StrColors == "Orange")
                    {
                        cmbColors.Text = "Orange";
                        cmbColors.Background = Brushes.Orange;
                        cmbColors.Foreground = Brushes.Orange;
                    }
                    else if (StrColors == "Green")
                    {
                        cmbColors.Text = "Green";
                        cmbColors.Background = Brushes.Green;
                        cmbColors.Foreground = Brushes.Green;
                    }
                    else if (StrColors == "Blue")
                    {
                        cmbColors.Text = "Blue";
                        cmbColors.Background = Brushes.Blue;
                        cmbColors.Foreground = Brushes.Blue;
                    }
                    else if (StrColors == "Gray")
                    {
                        cmbColors.Text = "Gray";
                        cmbColors.Background = Brushes.Gray;
                        cmbColors.Foreground = Brushes.Gray;
                    }
                    else
                    {
                        cmbColors.Text = "";
                        cmbColors.Background = null;
                        cmbColors.Foreground = null;
                    }

                    weave_checkItemprepare(P_ITMWEAVING, P_ITMPREPARE);
                }
            }
            else
            {
                "This Beam Lot hadn't in data".ShowMessageBox(true);

                txtBeamLength.Text = string.Empty;
                txtREEDNO.Text = string.Empty;
                cmbColors.Text = "";
                cmbColors.Background = null;
                cmbColors.Foreground = null;

                txtBEAMLOT.Text = "";
                txtBEAMLOT.SelectAll();
                txtBEAMLOT.Focus();
            }
        }

        #endregion

        #region weave_checkItemprepare

        private void weave_checkItemprepare(string P_ITMWEAVING, string P_ITMPREPARE)
        {
            List<WEAVE_CHECKITEMPREPARE> items = _session.GetWEAVE_CHECKITEMPREPARE(P_ITMWEAVING, P_ITMPREPARE);

            if (items != null && items.Count > 0)
            {

            }
            else
            {
                "This Beam Lot is not Map with Selected Item Weaving".ShowMessageBox(true);
                cbItemCode.SelectedValue = null;
                txtWeftYarn.Text = "";
                txtWarpYarn.Text = "";
            }
        }

        #endregion

        #region ClearInputs

        private void ClearInputs()
        {
            cbItemCode.SelectedValue = null;
            cbTEMPLE.Text = "";
            txtBARNO.IsEnabled = false;

            cmbColors.SelectedValue = null;
            cmbColors.Text = "";
            cmbColors.Background = null;
            cmbColors.Foreground = null;

            cbShift.SelectedIndex = 0;

            rbMassProduction.IsChecked = true;
            rbTest.IsChecked = false;

            txtBEAMLOT.Text = "";
            txtBeamLength.Text = "";

            txtWarpYarn.Text = "";
            txtWeftYarn.Text = "";
            txtREEDNO.Text = "";
            txtREEDNO2.Text = "";
            txtWIDTH.Text = "";
            txtLength.Text = "";
            txtBARNO.Text = "";
            dteSTARTDATE.SelectedDate = DateTime.Now;
            txtSTARTBY.Text = "";

            txtDensityWarp.Text = "";
            txtDensityWeft.Text = "";

            txtTension.Text = "";
            txtSpeed.Text = "";
            txtWaste.Text = "";

            dteWeavingDate.SelectedDate = DateTime.Now;

            cmdSet.IsEnabled = true;
            cmdEdit.IsEnabled = false;
            cmdSpecificMCStopReason.IsEnabled = false;

            cmdWeftYarn.IsEnabled = false;
            cmdStart.IsEnabled = false;
            cmdDoffing.IsEnabled = false;
            cmdPrint.IsEnabled = false;
            cmdFinishBeam.IsEnabled = false;
            cmdBeamChange.IsEnabled = false;

            gridDOFFNO = null;
            gridWEAVINGLOT = string.Empty;
            gridSHIFT = string.Empty;
            gridDENSITY_WARP = null;
            gridDENSITY_WEFT = null;
            gridTENSION = null;
            gridWASTE = null;
            gridLENGTH = null;

            weavLot = string.Empty;

            P_BEAMLOT = string.Empty;
            P_WEAVINGLOT = string.Empty;
            P_DOFFNO = null;
            P_LOOMNO = string.Empty;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
            P_TOTALRECORD = null;

            chkISHRow0 = false;
            chkMaxDoffNo = false;
        }

        #endregion

        #region WEAV_WEAVINGMCSTATUS

        private void WEAV_WEAVINGMCSTATUS(string P_MC)
        {
            WEAV_WEAVINGMCSTATUS results = null;

            results = WeavingDataService.Instance.WEAV_WEAVINGMCSTATUS(P_MC);

            if (results != null && !string.IsNullOrEmpty(results.BEAMLOT))
            {
                if (!string.IsNullOrEmpty(results.BEAMLOT))
                    txtBEAMLOT.Text = results.BEAMLOT;

                if (results.ITM_WEAVING != null)
                {
                    cbItemCode.SelectedValue = results.ITM_WEAVING;
                }

                txtREEDNO2.Text = results.REEDNO2;
                txtWarpYarn.Text = results.WEFTYARN;
                txtWeftYarn.Text = results.WEFTYARN;

                if (results.TEMPLETYPE != null)
                {
                    cbTEMPLE.SelectedValue = results.TEMPLETYPE;

                    if (results.TEMPLETYPE == "Bar")
                    {
                        txtBARNO.IsEnabled = true;
                        txtBARNO.Text = results.BARNO;
                    }
                }

                //if (results.STARTDATE != null)
                //    txtSTARTDATE.Text = results.STARTDATE.Value.ToString("dd/MM/yy");

                //results.FINISHDATE = results.FINISHDATE;
                //results.FINISHFLAG = results.FINISHFLAG;
                if (!string.IsNullOrEmpty(results.SETTINGBY))
                    txtSettingBy.Text = results.SETTINGBY;
                //results.EDITDATE = results.EDITDATE;
                //results.EDITBY = results.EDITBY;

                string P_PRODUCTTYPE = results.PRODUCTTYPEID;

                if (!string.IsNullOrEmpty(P_PRODUCTTYPE))
                {
                    if (P_PRODUCTTYPE == "1")
                    {
                        rbMassProduction.IsChecked = true;
                        rbTest.IsChecked = false;
                    }
                    else if (P_PRODUCTTYPE == "2")
                    {
                        rbMassProduction.IsChecked = false;
                        rbTest.IsChecked = true;
                    }
                }

                if (results.WIDTH != null)
                    txtWIDTH.Text = results.WIDTH.Value.ToString("#,##0.##");

                //เอาออกยังไม่ได้ใช้
                //GetBeamlotDetail();

                if (results.BEAMLENGTH != null)
                    txtBeamLength.Text = results.BEAMLENGTH.Value.ToString("#,##0.##");

                txtREEDNO.Text = results.REEDNO;

                string StrColors = results.HEALDCOLOR;

                if (StrColors == "Orange")
                {
                    cmbColors.Text = "Orange";
                    cmbColors.Background = Brushes.Orange;
                    cmbColors.Foreground = Brushes.Orange;
                }
                else if (StrColors == "Green")
                {
                    cmbColors.Text = "Green";
                    cmbColors.Background = Brushes.Green;
                    cmbColors.Foreground = Brushes.Green;
                }
                else if (StrColors == "Blue")
                {
                    cmbColors.Text = "Blue";
                    cmbColors.Background = Brushes.Blue;
                    cmbColors.Foreground = Brushes.Blue;
                }
                else if (StrColors == "Gray")
                {
                    cmbColors.Text = "Gray";
                    cmbColors.Background = Brushes.Gray;
                    cmbColors.Foreground = Brushes.Gray;
                }
                else
                {
                    cmbColors.Text = "";
                    cmbColors.Background = null;
                    cmbColors.Foreground = null;
                }

                if (results.SPEED != null)
                    txtSpeed.Text = results.SPEED.Value.ToString("#,##0.##");

                if (!string.IsNullOrEmpty(results.BEAMLOT))
                {
                    if (Doff == true)
                    {
                        cmdEdit.IsEnabled = true;
                    }
                    else
                    {
                        cmdEdit.IsEnabled = false;
                    }

                    cmdStart.IsEnabled = true;
                    cmdFinishBeam.IsEnabled = true;

                    cbItemCode.IsEnabled = false;
                    txtBEAMLOT.IsEnabled = false;
                    txtREEDNO.IsEnabled = false;

                    cmdSet.IsEnabled = false;
                    cmdDoffing.IsEnabled = false;
                    cmdWeftYarn.IsEnabled = false;

                    cmdBeamChange.IsEnabled = true;

                    WEAV_GETWEAVELISTBYBEAMROLL(results.BEAMLOT, MCNo);

                    WEAV_GETINPROCESSBYBEAMROLL(results.BEAMLOT, MCNo);
                }
            }
        }

        #endregion

        #region WEAV_GETINPROCESSBYBEAMROLL

        private void WEAV_GETINPROCESSBYBEAMROLL(string P_BEAMROLL, string P_LOOM)
        {
            WEAV_GETINPROCESSBYBEAMROLL results = null;

            results = WeavingDataService.Instance.WEAV_GETINPROCESSBYBEAMROLL(P_BEAMROLL, P_LOOM);

            if (results != null && results.DOFFNO != null)
            {
                if (results.DOFFNO != null)
                    txtDOFFNO.Text = results.DOFFNO.ToString();

                if (!string.IsNullOrEmpty(results.WEAVINGLOT))
                    weavLot = results.WEAVINGLOT;

                if (results.STARTDATE != null)
                    dteSTARTDATE.SelectedDate = results.STARTDATE;

                txtSTARTBY.Text = results.PREPAREBY;

                if (results.SHIFT != null)
                {
                    string shift = results.SHIFT;

                    if (shift == "A")
                    {
                        cbShift.SelectedIndex = 0;
                    }
                    else if (shift == "B")
                    {
                        cbShift.SelectedIndex = 1;
                    }
                    else if (shift == "C")
                    {
                        cbShift.SelectedIndex = 2;
                    }
                    else if (shift == "D")
                    {
                        cbShift.SelectedIndex = 3;
                    }
                }

                if (results.DENSITY_WARP != null)
                    txtDensityWarp.Text = results.DENSITY_WARP.Value.ToString("#,##0.##");

                if (results.DENSITY_WEFT != null)
                    txtDensityWeft.Text = results.DENSITY_WEFT.Value.ToString("#,##0.##");

                if (results.TENSION != null)
                    txtTension.Text = results.TENSION.Value.ToString("#,##0.##");

                if (results.WASTE != null)
                    txtWaste.Text = results.WASTE.Value.ToString("#,##0.##");

                if (results.SPEED != null)
                    txtSpeed.Text = results.SPEED.Value.ToString("#,##0.##");

                if (results.LENGTH != null)
                    txtLength.Text = results.LENGTH.Value.ToString("#,##0.##");


                cmdStart.IsEnabled = false;
                cmdSet.IsEnabled = false;
                cmdSpecificMCStopReason.IsEnabled = true;

                if (Doff == true)
                {
                    cmdEdit.IsEnabled = true;
                }
                else
                {
                    cmdEdit.IsEnabled = false;
                }

                cmdFinishBeam.IsEnabled = true;

                cmdDoffing.IsEnabled = true;
                cmdWeftYarn.IsEnabled = true;

                cmdBeamChange.IsEnabled = true;
            }
            else
            {
                cmdStart.IsEnabled = true;
                cmdSet.IsEnabled = false;
                cmdSpecificMCStopReason.IsEnabled = false;

                if (Doff == true)
                {
                    cmdEdit.IsEnabled = true;
                }
                else
                {
                    cmdEdit.IsEnabled = false;
                }

                cmdFinishBeam.IsEnabled = true;

                cmdDoffing.IsEnabled = false;
                cmdWeftYarn.IsEnabled = false;

                cmdBeamChange.IsEnabled = true;
            }
        }

        #endregion

        #region Load Data in Grid

        private void WEAV_GETWEAVELISTBYBEAMROLL(string P_BEAMROLL, string P_LOOM)
        {
            List<WEAV_GETWEAVELISTBYBEAMROLL> results = null;

            results = WeavingDataService.Instance.WEAV_GETWEAVELISTBYBEAMROLL(P_BEAMROLL, P_LOOM);

            if (results != null && results.Count > 0)
            {
                List<LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL> dataList = new List<LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL>();
                int i = 0;


                foreach (var row in results)
                {
                    LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL dataItemNew = new LuckyTex.Models.WEAV_GETWEAVELISTBYBEAMROLL();

                    //dataItemNew.RowNo = results[i].RowNo;
                    dataItemNew.WEAVINGLOT = results[i].WEAVINGLOT;
                    dataItemNew.ITM_WEAVING = results[i].ITM_WEAVING;
                    dataItemNew.LENGTH = results[i].LENGTH;
                    dataItemNew.LOOMNO = results[i].LOOMNO;
                    dataItemNew.WEAVINGDATE = results[i].WEAVINGDATE;
                    dataItemNew.SHIFT = results[i].SHIFT;
                    dataItemNew.REMARK = results[i].REMARK;
                    dataItemNew.CREATEDATE = results[i].CREATEDATE;
                    dataItemNew.WIDTH = results[i].WIDTH;
                    dataItemNew.PREPAREBY = results[i].PREPAREBY;
                    dataItemNew.WEAVINGNO = results[i].WEAVINGNO;
                    dataItemNew.BEAMLOT = results[i].BEAMLOT;
                    dataItemNew.DOFFNO = results[i].DOFFNO;

                    dataItemNew.DENSITY_WARP = results[i].DENSITY_WARP;
                    dataItemNew.DENSITY_WEFT = results[i].DENSITY_WEFT;

                    dataItemNew.TENSION = results[i].TENSION;
                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.DOFFBY = results[i].DOFFBY;

                    dataItemNew.SPEED = results[i].SPEED;
                    dataItemNew.WASTE = results[i].WASTE;

                    dataItemNew.DELETEFLAG = results[i].DELETEFLAG;
                    dataItemNew.DELETEBY = results[i].DELETEBY;
                    dataItemNew.DELETEDATE = results[i].DELETEDATE;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridWeaving.ItemsSource = dataList;

                if (Doff == true)
                {
                    cmdEdit.IsEnabled = true;
                }
                else
                {
                    cmdEdit.IsEnabled = false;
                }

                cmdStart.IsEnabled = true;
                cmdFinishBeam.IsEnabled = true;
                cmdSpecificMCStopReason.IsEnabled = false;

                cmdSet.IsEnabled = false;
                cmdDoffing.IsEnabled = false;
                cmdWeftYarn.IsEnabled = false;

                cmdBeamChange.IsEnabled = true;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWeaving.SelectedItems.Clear();
                else
                    this.gridWeaving.SelectedItem = null;

                gridWeaving.ItemsSource = null;

            }

            gridDOFFNO = null;
            gridWEAVINGLOT = string.Empty;
            gridSHIFT = string.Empty;
            gridDENSITY_WARP = null;
            gridDENSITY_WEFT = null;
            gridTENSION = null;
            gridWASTE = null;
            gridLENGTH = null;
        }

        #endregion

        #region Set

        private void Set(string P_BEAMLOT, string P_MC, string P_ITMWEAVE, string P_REEDNO2,
        string P_WEFTYARN, string P_TEMPLE,
        string P_BARNO, DateTime? P_STARTDATE, string P_SETTINGBY, string P_PRODUCTTYPE, decimal? P_WIDTH, decimal? P_BEAMLENGTH, decimal? P_SPEED)
        {

            WEAVE_INSERTPROCESSSETTING result = WeavingDataService.Instance.WEAVE_INSERTPROCESSSETTING(P_BEAMLOT, P_MC, P_ITMWEAVE, P_REEDNO2,
           P_WEFTYARN, P_TEMPLE,
           P_BARNO, P_STARTDATE, P_SETTINGBY, P_PRODUCTTYPE, P_WIDTH, P_BEAMLENGTH, P_SPEED);

            string msg = result.RESULT;

            if (string.IsNullOrEmpty(msg))
            {
                if (Doff == true)
                {
                    cmdEdit.IsEnabled = true;
                }
                else
                {
                    cmdEdit.IsEnabled = false;
                }

                cmdStart.IsEnabled = true;
                cmdFinishBeam.IsEnabled = true;
                cmdSpecificMCStopReason.IsEnabled = false;

                cbItemCode.IsEnabled = false;
                txtBEAMLOT.IsEnabled = false;
                txtREEDNO.IsEnabled = false;

                cmdSet.IsEnabled = false;
                cmdDoffing.IsEnabled = false;
                cmdWeftYarn.IsEnabled = false;

                cmdBeamChange.IsEnabled = true;

                dteSTARTDATE.SelectedDate = DateTime.Now;
            }
            else
            {
                msg.ShowMessageBox(false);
            }
        }

        #endregion

        #region edit

        private void Edit(string P_BEAMLOT, string P_REEDNO2, string P_TEMPLE, string P_BARNO, string P_PRODUCTTYPE,
        decimal? P_WIDTH, DateTime? P_FINISHDATE, string P_FLAG, DateTime? P_EDITDATE, string P_EDITBY, decimal? P_SPEED)
        {

            string result = WeavingDataService.Instance.WEAVE_UPDATEPROCESSSETTING(P_BEAMLOT, P_REEDNO2, P_TEMPLE, P_BARNO,
           P_PRODUCTTYPE, P_WIDTH, P_FINISHDATE, P_FLAG, P_EDITDATE, P_EDITBY, P_SPEED);

            if (!string.IsNullOrEmpty(result))
            {
                result.ShowMessageBox(false);
            }
            else
            {
                "Save complete".ShowMessageBox(false);
            }
        }

        #endregion

        #region Start

        private void Start(string P_BEAMLOT, decimal? P_DOFFNO, string P_ITEMWEAVING,
        decimal? P_LENGHT, DateTime? P_WEAVINGDATE, string P_LOOM, string P_SHIFT,
        decimal? P_DENSITYWARP, decimal? P_DENSITYWEFT, decimal? P_TENSION, decimal? P_SPEED, decimal? P_WASTE, string P_REMARK, string P_OPERATOR, DateTime? P_STARTDATE)
        {
            string DoffNo = WeavingDataService.Instance.WEAVE_WEAVINGPROCESS(P_BEAMLOT, P_DOFFNO, P_ITEMWEAVING,
                 P_LENGHT, P_WEAVINGDATE, P_LOOM, P_SHIFT,
                 P_DENSITYWARP, P_DENSITYWEFT, P_TENSION, P_SPEED, P_WASTE, P_REMARK, P_OPERATOR, P_STARTDATE);

            if (!string.IsNullOrEmpty(DoffNo))
            {
                PageManager.Instance.Back();

                //txtDOFFNO.Text = DoffNo;

                //cmdStart.IsEnabled = false;
                //cmdDoffing.IsEnabled = true;
                //cmdWeftYarn.IsEnabled = true;

                //cmdSpecificMCStopReason.IsEnabled = true;

                //dteWeavingDate.SelectedDate = DateTime.Now;

                //txtDensityWarp.SelectAll();
                //txtDensityWarp.Focus();
            }
        }

        #endregion

        #region Doffing
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <param name="P_DOFFNO"></param>
        /// <param name="P_ITEMWEAVING"></param>
        /// <param name="P_LENGHT"></param>
        /// <param name="P_WEAVINGDATE"></param>
        /// <param name="P_LOOM"></param>
        /// <param name="P_SHIFT"></param>
        /// <param name="P_DENSITYWARP"></param>
        /// <param name="P_DENSITYWEFT"></param>
        /// <param name="P_TENSION"></param>
        /// <param name="P_SPEED"></param>
        /// <param name="P_WASTE"></param>
        /// <param name="P_REMARK"></param>
        /// <param name="P_OPERATOR"></param>
        /// <param name="P_STARTDATE"></param>
        private void Doffing(string P_BEAMLOT, decimal? P_DOFFNO, string P_ITEMWEAVING,
        decimal? P_LENGHT, DateTime? P_WEAVINGDATE, string P_LOOM, string P_SHIFT,
        decimal? P_DENSITYWARP, decimal? P_DENSITYWEFT, decimal? P_TENSION, decimal? P_SPEED, decimal? P_WASTE, string P_REMARK, string P_OPERATOR, DateTime? P_STARTDATE)
        {
            string result = WeavingDataService.Instance.WEAVE_WEAVINGPROCESS(P_BEAMLOT, P_DOFFNO, P_ITEMWEAVING,
             P_LENGHT, P_WEAVINGDATE, P_LOOM, P_SHIFT, P_DENSITYWARP, P_DENSITYWEFT, P_TENSION, P_SPEED, P_WASTE, P_REMARK, P_OPERATOR, P_STARTDATE);

            if (!string.IsNullOrEmpty(result))
            {
                D365_GR(P_BEAMLOT, result, P_DOFFNO, P_LOOM,false);

                string msg = string.Empty;
                msg = "Weaving Lot : " + result + " has been created";
                msg.ShowMessageBox(false);

                WEAV_GETWEAVELISTBYBEAMROLL(P_BEAMLOT, MCNo);


                txtDOFFNO.Text = string.Empty;
                dteSTARTDATE.SelectedDate = null;
                txtSTARTBY.Text = string.Empty;
                cbShift.SelectedIndex = 0;
                txtDensityWarp.Text = string.Empty;
                txtDensityWeft.Text = string.Empty;
                txtTension.Text = string.Empty;

                //txtSpeed.Text = string.Empty;

                txtLength.Text = string.Empty;
                txtWaste.Text = string.Empty;

                dteWeavingDate.SelectedDate = null;

                if (Doff == true)
                {
                    cmdEdit.IsEnabled = true;
                }
                else
                {
                    cmdEdit.IsEnabled = false;
                }

                cmdStart.IsEnabled = true;
                cmdFinishBeam.IsEnabled = true;
                cmdSpecificMCStopReason.IsEnabled = false;

                cmdSet.IsEnabled = false;
                cmdDoffing.IsEnabled = false;
                cmdWeftYarn.IsEnabled = false;

                cmdBeamChange.IsEnabled = true;
            }
        }

        #endregion

        #region Finish

        private void Finish(string P_BEAMLOT, string P_REEDNO2, string P_TEMPLE, string P_BARNO, string P_PRODUCTTYPE,
        decimal? P_WIDTH, DateTime? P_FINISHDATE, string P_FLAG, DateTime? P_EDITDATE, string P_EDITBY, decimal? P_SPEED)
        {
            string result = WeavingDataService.Instance.WEAVE_UPDATEPROCESSSETTING(P_BEAMLOT, P_REEDNO2, P_TEMPLE, P_BARNO, P_PRODUCTTYPE,
         P_WIDTH, P_FINISHDATE, P_FLAG, P_EDITDATE, P_EDITBY, P_SPEED);

            if (!string.IsNullOrEmpty(result))
            {
                result.ShowMessageBox(false);
            }
            else
            {
                DRAW_UPDATEDRAWING(P_BEAMLOT);

                D365_GR_DoffNo(P_BEAMLOT, MCNo, true);
            }
        }

        #endregion

        #region DRAW_UPDATEDRAWING
        private void DRAW_UPDATEDRAWING(string P_BEAMLOT)
        {
            string P_DRAWINGTYPE = null;
            string P_REEDNO = null;
            string P_HEALDCOLOR = null;
            decimal? P_HEALDNO = null;
            string P_OPERATOR = null;
            string P_FLAG = "1";
            string P_GROUP = null;

            string result = WeavingDataService.Instance.DRAW_UPDATEDRAWING(P_BEAMLOT, P_DRAWINGTYPE, P_REEDNO, P_HEALDCOLOR,
           P_HEALDNO, P_OPERATOR, P_FLAG, P_GROUP);

            if (!string.IsNullOrEmpty(result))
            {
                result.ShowMessageBox(true);
            }
            else
            {
                PageManager.Instance.Back();
            }
        }
        #endregion

        #region BeamChange
        private bool BeamChange(string P_BEAMLOT)
        {
            DateTime? P_FINISHDATE = DateTime.Now;
            string P_FLAG = "1";
            string P_EDITBY = opera;

            string result = WeavingDataService.Instance.BeamChange(P_BEAMLOT, P_FINISHDATE, P_FLAG, P_EDITBY);

            if (!string.IsNullOrEmpty(result))
            {
                result.ShowMessageBox(true);

                return false;
            }
            else
            {
                D365_GR_DoffNo(P_BEAMLOT, MCNo, true);

                "Finish This Loom No".ShowMessageBox();

                return true;
            }
        }
        #endregion

        #region WEAV_UPDATEWEFTSTOCK
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <param name="P_WEAVINGLOT"></param>
        /// <param name="P_DOFFNO"></param>
        /// <param name="P_LOOMNO"></param>
        /// <param name="P_ITMYARN"></param>
        /// <returns></returns>
        private bool WEAV_UPDATEWEFTSTOCK(string P_BEAMLOT, string P_WEAVINGLOT, decimal? P_DOFFNO, string P_LOOMNO, string P_ITMYARN)
        {
            try
            {
                string result = WeavingDataService.Instance.WEAV_UPDATEWEFTSTOCK(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO, P_LOOMNO, P_ITMYARN);

                if (!string.IsNullOrEmpty(result))
                {
                    result.ShowMessageBox(false);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return false;
            }
        }

        #endregion

        #region D365_GR
        private bool D365_GR(string _BEAMLOT, string _WEAVINGLOT, decimal? _DOFFNO, string _LOOMNO, bool _MaxDoffNo)
        {
            bool chkGR = false;
            try
            {
                P_BEAMLOT = _BEAMLOT;
                P_WEAVINGLOT = _WEAVINGLOT;
                P_DOFFNO = _DOFFNO;
                P_LOOMNO = _LOOMNO;

                string P_ITMYARN = string.Empty;
                if(!string.IsNullOrEmpty(txtWeftYarn.Text))
                    P_ITMYARN = txtWeftYarn.Text;

                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;
                P_TOTALRECORD = null;

                chkISHRow0 = false;

                chkMaxDoffNo = _MaxDoffNo;
                
                if (!string.IsNullOrEmpty(P_BEAMLOT) && !string.IsNullOrEmpty(P_WEAVINGLOT))
                {
                    if (D365_GR_BPO() == true)
                    {
                        if (WEAV_UPDATEWEFTSTOCK(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO, P_LOOMNO, P_ITMYARN) == true)
                        {
                            if (PRODID != null)
                            {
                                if (D365_GR_ISH(PRODID) == true)
                                {
                                    if (chkISHRow0 == false)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (P_TOTALRECORD != 0)
                                            {
                                                #region D365_GR_ISL
                                                if (D365_GR_ISL(HEADERID) == true)
                                                {
                                                    if (D365_GR_OPH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_GR_OPL(HEADERID) == true)
                                                            {
                                                                if (D365_GR_OUH(PRODID) == true)
                                                                {
                                                                    if (HEADERID != null)
                                                                    {
                                                                        if (D365_GR_OUL(HEADERID) == true)
                                                                        {
                                                                            "Send D365 complete".Info();
                                                                            chkGR = true;
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
                                                #endregion
                                            }
                                            else
                                            {
                                                //ข้าม D365_GR_ISL
                                                #region D365_GR_OPH
                                                if (D365_GR_OPH(PRODID) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_GR_OPL(HEADERID) == true)
                                                        {
                                                            if (D365_GR_OUH(PRODID) == true)
                                                            {
                                                                if (HEADERID != null)
                                                                {
                                                                    if (D365_GR_OUL(HEADERID) == true)
                                                                    {
                                                                        "Send D365 complete".Info();
                                                                        chkGR = true;
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
                                                #endregion
                                            }
                                        }
                                        else
                                        {
                                            "HEADERID is null".Info();
                                        }
                                    }
                                    else
                                    {
                                        //ข้าม D365_GR_ISL
                                        #region D365_GR_OPH
                                        if (D365_GR_OPH(PRODID) == true)
                                        {
                                            if (HEADERID != null)
                                            {
                                                if (D365_GR_OPL(HEADERID) == true)
                                                {
                                                    if (D365_GR_OUH(PRODID) == true)
                                                    {
                                                        if (HEADERID != null)
                                                        {
                                                            if (D365_GR_OUL(HEADERID) == true)
                                                            {
                                                                "Send D365 complete".Info();
                                                                chkGR = true;
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
                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                "PRODID is null".Info();
                            }
                        }
                    }
                }
                else
                {
                    "Beamer Roll is null".Info();
                }

                return chkGR;
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return chkGR;
            }
        }
        #endregion

        #region D365_GR_DoffNo
        private void D365_GR_DoffNo(string _BEAMLOT, string MCNo , bool _MaxDoffNo)
        {
            try
            {
                List<WEAV_GETWEAVELISTBYBEAMROLL> results = null;

                results = WeavingDataService.Instance.WEAV_GETWEAVELISTBYBEAMROLL(_BEAMLOT, MCNo);

                if (results != null && results.Count > 0)
                {
                    P_BEAMLOT = _BEAMLOT;
                    P_LOOMNO = MCNo;
                    P_DOFFNO = results.Count;
                    chkMaxDoffNo = _MaxDoffNo;

                    PRODID = null;
                    HEADERID = null;

                    foreach (var row in results)
                    {
                        if (D365_GR_BPO() == true)
                        {
                            P_WEAVINGLOT = row.WEAVINGLOT;

                            if (D365_GR_OUH(PRODID) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_GR_OUL(HEADERID) == true)
                                    {
                                        "Send D365 complete".Info();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }
        #endregion

        #region D365_GR_BPO
        private bool D365_GR_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_BPOData> results = new List<ListD365_GR_BPOData>();

                results = D365DataService.Instance.D365_GR_BPO(P_BEAMLOT, P_LOOMNO, P_DOFFNO);

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
                      
                        if (PRODID != null && P_DOFFNO == 1)
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
                    "D365_GR_BPO Row = 0".Info();
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

        #region D365_GR_ISH
        private bool D365_GR_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_GR_ISHData> results = new List<D365_GR_ISHData>();

                results = D365DataService.Instance.D365_GR_ISH(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO);

                if (results.Count > 0)
                {
                    chkISHRow0 = false;

                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (results[i].TOTALRECORD != null)
                            P_TOTALRECORD = results[i].TOTALRECORD;
                        else
                            P_TOTALRECORD = null;

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
                    "D365_GR_ISH Row = 0".Info();
                    chkISHRow0 = true;
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

        #region D365_GR_ISL
        private bool D365_GR_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_ISLData> results = new List<ListD365_GR_ISLData>();

                results = D365DataService.Instance.D365_GR_ISL(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO, P_LOOMNO);

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
                    "D365_GR_ISL Row = 0".Info();
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

        #region D365_GR_OPH
        private bool D365_GR_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_GR_OPHData> results = new List<D365_GR_OPHData>();

                results = D365DataService.Instance.D365_GR_OPH(P_WEAVINGLOT);

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
                    "D365_GR_OPH Row = 0".Info();
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

        #region D365_GR_OPL
        private bool D365_GR_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_OPLData> results = new List<ListD365_GR_OPLData>();

                results = D365DataService.Instance.D365_GR_OPL(P_WEAVINGLOT);

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
                    "D365_GR_OPL Row = 0".Info();
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

        #region D365_GR_OUH
        private bool D365_GR_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_GR_OUHData> results = new List<D365_GR_OUHData>();

                results = D365DataService.Instance.D365_GR_OUH(P_WEAVINGLOT);

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
                            if (chkMaxDoffNo == false)
                                chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);
                            else
                                chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "U", 1, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

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
                    "D365_GR_OUH Row = 0".Info();
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

        #region D365_GR_OUL
        private bool D365_GR_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_GR_OULData> results = new List<ListD365_GR_OULData>();

                results = D365DataService.Instance.D365_GR_OUL(P_BEAMLOT, P_WEAVINGLOT, P_DOFFNO, P_LOOMNO);

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

                        if (chkMaxDoffNo == false)
                            chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                                , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);
                        else
                            chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "U", 1, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
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
                    "D365_GR_OUL Row = 0".Info();
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
        public void Setup(string user, string mcno, string type, string productType, bool hideFinishBeam)
        {
            if (opera != null)
            {
                opera = user;
            }

            MCNo = mcno;

            strType = type;
            productTypeID = productType;
            hideFinish = hideFinishBeam;
        }

        public void Setup(string user, string mcno, string type, string productType, bool getDoff, bool hideFinishBeam)
        {
            if (opera != null)
            {
                opera = user;
            }

            MCNo = mcno;

            strType = type;
            productTypeID = productType;

            Doff = getDoff;
            hideFinish = hideFinishBeam;
        }

        #endregion

    }
}
