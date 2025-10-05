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
    /// Interaction logic for FinishingMCMenu.xaml
    /// </summary>
    public partial class FinishingMCMenu : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingMCMenu()
        {
            InitializeComponent();

            ConfigManager.Instance.LoadMachineStatusConfig();
            Coating1Status = ConfigManager.Instance.Coating1MachineConfig;
            Coating2Status = ConfigManager.Instance.Coating2MachineConfig;
            Coating3Status = ConfigManager.Instance.Coating3MachineConfig;
            Scouring1Status = ConfigManager.Instance.Scouring1MachineConfig;
            Scouring2Status = ConfigManager.Instance.Scouring2MachineConfig;
            Coating3ScouringStatus = ConfigManager.Instance.Coating3ScouringMachineConfig;

            Coating1ScouringStatus = ConfigManager.Instance.Coating1ScouringMachineConfig;
            Scouring2ScouringDryStatus = ConfigManager.Instance.Scouring2ScouringDryMachineConfig;

            ScouringCoat1Status = ConfigManager.Instance.ScouringCoat1MachineConfig;
            ScouringCoat2Status = ConfigManager.Instance.ScouringCoat2MachineConfig;
        }

        #endregion

        #region Internal Variables

        private List<FinishingMCItem> instList = null;

        private string MCNAME = string.Empty;
        private string FINISHINGLOT = string.Empty;
        private string WEAVINGLOT = string.Empty;
        private string ITM_CODE = string.Empty;
        private string STATUS = string.Empty;
        private string FINISHINGCUSTOMER = string.Empty;
        private string STARTBY = string.Empty;
        private string CONDITIONBY = string.Empty;
        string OPERATOR_GROUP = string.Empty;

        private bool Coating1Status = true;
        private bool Coating2Status = true;
        private bool Coating3Status = true;
        private bool Scouring1Status = true;
        private bool Scouring2Status = true;
        private bool Coating3ScouringStatus = true;
        private bool ScouringCoat2Status = true;
        
        private bool Coating1ScouringStatus = true;
        private bool Scouring2ScouringDryStatus = true;

        private bool ScouringCoat1Status = true;
       
        #endregion

        #region Private Methods
        
        private void LoadMacines()
        {
            instList = FinishingDataService.Instance.GetMachines();
            lstInstMC.ItemsSource = instList;
        }

        private void LoadFINISHING_INPROCESSLIST()
        {
            MCNAME = string.Empty;
            FINISHINGLOT = string.Empty;
            WEAVINGLOT = string.Empty;
            ITM_CODE = string.Empty;
            STATUS = string.Empty;
            FINISHINGCUSTOMER = string.Empty;
            STARTBY = string.Empty;
            OPERATOR_GROUP = string.Empty;
            CONDITIONBY = string.Empty;

            List<FINISHING_INPROCESSLIST> lots = new List<FINISHING_INPROCESSLIST>();
            lots = CoatingDataService.Instance.FINISHING_INPROCESSLIST();

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridFinishing.ItemsSource = lots;
            }
            else
            {
                gridFinishing.ItemsSource = null;
            }
        }

        private void FINISHING_CANCEL(string WEAVLOT, string FINISHLOT, string PROCESS, string OPERATOR)
        {
            if (CoatingDataService.Instance.FINISHING_CANCEL(WEAVLOT, FINISHLOT, PROCESS, OPERATOR) == true)
            {
                LoadFINISHING_INPROCESSLIST();
            }
            else
            {
                "Can't Cancel Finishing".ShowMessageBox(true);
            }
        }

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMacines();
            LoadFINISHING_INPROCESSLIST();
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

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadFINISHING_INPROCESSLIST();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(FINISHINGLOT))
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 6; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "This User can not be Use for This Menu".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        FINISHING_CANCEL(WEAVINGLOT, FINISHINGLOT, MCNAME, logInfo.UserName);
                    }
                }
            }
        }

        #endregion

        #region ListBox Handler

        private void lstInstMC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstInstMC.SelectedIndex <= -1 && null != instList)
            {
                return;
            }

            if (lstInstMC.SelectedIndex < instList.Count - 2)
            {
                #region Page

                string mc = instList[lstInstMC.SelectedIndex].MCId;
                bool chkStatus = true;

                if (!string.IsNullOrEmpty(mc))
                {
                    #region chkStatus

                    if (mc == "252")
                    {
                        if (Scouring1Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "253")
                    {
                        if (Scouring2Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "254")
                    {
                        if (Coating1Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "255")
                    {
                        if (Coating2Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "272")
                    {
                        if (Scouring1Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "273")
                    {
                        if (Coating1Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "274")
                    {
                        if (Coating1Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "370")
                    {
                        if (Coating3Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    else if (mc == "371")
                    {
                        if (Coating3ScouringStatus == false)
                        {
                            chkStatus = false;
                        }
                    }

                    //เพิ่มใหม่ ScouringCoat2Status 05/01/17
                    else if (mc == "372")
                    {
                        if (ScouringCoat2Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    //เพิ่มใหม่ 12/05/17
                    else if (mc == "373")
                    {
                        if (Coating1ScouringStatus == false)
                        {
                            chkStatus = false;
                        }
                    }
                    //เพิ่มใหม่ 26/05/17
                    else if (mc == "374")
                    {
                        if (Scouring2ScouringDryStatus == false)
                        {
                            chkStatus = false;
                        }
                    }
                    //เพิ่มใหม่ 26/05/17
                    else if (mc == "394")
                    {
                        if (ScouringCoat1Status == false)
                        {
                            chkStatus = false;
                        }
                    }
                    #endregion

                    if (chkStatus == true)
                    {
                        #region LogInInfo
                        LogInInfo logInfo = new LogInInfo();

                        //if (mc == "252" || mc == "253" || mc == "254" || mc == "255" || mc == "274" || mc == "272" || mc == "273")
                        //{
                            //logInfo = this.ShowLogInSelectPositionBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);
                        //}
                        //else
                        //{
                            logInfo = this.ShowOldLogInSelectPositionBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);
                        //}

                        if (logInfo != null)
                        {
                            string position = logInfo.Position;

                            int processId = 6; // for inspection
                            List<LogInResult> operators = UserDataService.Instance
                                .GetOperators(logInfo.UserName, logInfo.Password, processId);

                            if (null == operators || operators.Count <= 0)
                            {
                                "User Not found or Not Authorized for this Menu".ShowMessageBox(true);
                                lstInstMC.SelectedIndex = -1; // reset index.
                                return;
                            }

                            List<Domains.INS_GETMCSUSPENDDATAResult> results =
                                FinishingDataService.Instance.GetSuspendInspectionProcess(
                                instList[lstInstMC.SelectedIndex].MCId);

                            Domains.INS_GETMCSUSPENDDATAResult result = null;

                            FinishingSession session = FinishingDataService.Instance.GetSession(
                                instList[lstInstMC.SelectedIndex], operators[0]);

                            if (null != results && results.Count > 0)
                            {
                                result = results[0];
                            }

                            if (mc == "252")
                            {
                                #region Scouring 1

                                //if (Scouring1Status == true)
                                //{
                                //    #region Scouring 1

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Scouring1PreparingPage page = new Scouring1PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Scouring1FinishingPage page = new Scouring1FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Scouring 1

                                if (Scouring1Status == true)
                                {
                                    #region Scouring 1

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldScouring1PreparingPage page = new OldScouring1PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Scouring1ProcessingPage page = new Scouring1ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldScouring1FinishingPage page = new OldScouring1FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "253")
                            {
                                #region Scouring 2

                                //if (Scouring2Status == true)
                                //{
                                //    #region Scouring 2

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Scouring2PreparingPage page = new Scouring2PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Scouring2FinishingPage page = new Scouring2FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Scouring 2

                                if (Scouring2Status == true)
                                {
                                    #region Scouring 2

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldScouring2PreparingPage page = new OldScouring2PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Scouring2ProcessingPage page = new Scouring2ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldScouring2FinishingPage page = new OldScouring2FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "254")
                            {
                                #region Coating 1

                                //if (Coating1Status == true)
                                //{
                                //    #region Coating 1

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Coating1PreparingPage page = new Coating1PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Coating1FinishingPage page = new Coating1FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Coating 1

                                if (Coating1Status == true)
                                {
                                    #region Coating 1

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating1PreparingPage page = new OldCoating1PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating1ProcessingPage page = new Coating1ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating1FinishingPage page = new OldCoating1FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "255")
                            {
                                #region Coating 2

                                //if (Coating2Status == true)
                                //{
                                //    #region Coating 2

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Coating2PreparingPage page = new Coating2PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Coating2FinishingPage page = new Coating2FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Coating 2

                                if (Coating2Status == true)
                                {
                                    #region Coating 2

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating2PreparingPage page = new OldCoating2PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating2ProcessingPage page = new Coating2ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating2FinishingPage page = new OldCoating2FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "272")
                            {
                                #region Scouring Dryer

                                //if (Scouring1Status == true)
                                //{
                                //    #region Scouring Dryer

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            ScouringDryerPreparingPage page = new ScouringDryerPreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            ScouringDryerFinishingPage page = new ScouringDryerFinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Scouring Dryer

                                if (Scouring1Status == true)
                                {
                                    #region Scouring Dryer

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldScouringDryerPreparingPage page = new OldScouringDryerPreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            ScouringDryerProcessingPage page = new ScouringDryerProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldScouringDryerFinishingPage page = new OldScouringDryerFinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "273")
                            {
                                #region Coating1 Dryer

                                //if (Coating1Status == true)
                                //{
                                //    #region Coating1 Dryer

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Coating1DryerPreparingPage page = new Coating1DryerPreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Coating1DryerFinishingPage page = new Coating1DryerFinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Coating1 Dryer

                                if (Coating1Status == true)
                                {
                                    #region Coating1 Dryer

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating1DryerPreparingPage page = new OldCoating1DryerPreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating1DryerProcessingPage page = new Coating1DryerProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating1DryerFinishingPage page = new OldCoating1DryerFinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "274")
                            {
                                #region Coating1 2Step

                                //if (Coating1Status == true)
                                //{
                                //    #region Coating1 2Step

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Coating12StepPreparingPage page = new Coating12StepPreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Coating12StepFinishingPage page = new Coating12StepFinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Coating1 2Step

                                if (Coating1Status == true)
                                {
                                    #region Coating1 2Step

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating12StepPreparingPage page = new OldCoating12StepPreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating12StepProcessingPage page = new Coating12StepProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating12StepFinishingPage page = new OldCoating12StepFinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "370")
                            {
                                #region Coating 3

                                //if (Coating3Status == true)
                                //{
                                //    #region Coating 3

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            Coating3PreparingPage page = new Coating3PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            Coating3FinishingPage page = new Coating3FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old Coating 3

                                if (Coating3Status == true)
                                {
                                    #region Coating 3

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating3PreparingPage page = new OldCoating3PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating3ProcessingPage page = new Coating3ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating3FinishingPage page = new OldCoating3FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }
                            else if (mc == "371")
                            {
                                #region Old Coating 3

                                if (Coating3ScouringStatus == true)
                                {
                                    #region Coating 3 Scouring

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating3ScouringPreparingPage page = new OldCoating3ScouringPreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating3ScouringProcessingPage page = new Coating3ScouringProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating3ScouringFinishingPage page = new OldCoating3ScouringFinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }

                            //เพิ่มใหม่ ScouringCoat2Status 05/01/17
                            else if (mc == "372")
                            {
                                #region ScouringCoat2

                                //if (ScouringCoat2Status == true)
                                //{
                                //    #region Scouring 2

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            ScouringCoat2PreparingPage page = new ScouringCoat2PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            ScouringCoat2FinishingPage page = new ScouringCoat2FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion

                                #region Old ScouringCoat2

                                if (ScouringCoat2Status == true)
                                {
                                    #region ScouringCoat2Status

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldScouringCoat2PreparingPage page = new OldScouringCoat2PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            ScouringCoat2ProcessingPage page = new ScouringCoat2ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldScouringCoat2FinishingPage page = new OldScouringCoat2FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }

                            //เพิ่มใหม่ 12/05/17
                            else if (mc == "373")
                            {
                                #region Old Coating1Scouring

                                if (Coating1ScouringStatus == true)
                                {
                                    #region Coating1ScouringStatus

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldCoating1ScouringPreparingPage page = new OldCoating1ScouringPreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Coating1ScouringProcessingPage page = new Coating1ScouringProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldCoating1ScouringFinishingPage page = new OldCoating1ScouringFinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }

                             //เพิ่มใหม่ 26/05/17
                            else if (mc == "374")
                            {
                                #region Old Scouring2 Scouring Dry

                                if (Scouring2ScouringDryStatus == true)
                                {
                                    #region Scouring2ScouringDryStatus

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            OldScouring2ScouringDryPreparingPage page = new OldScouring2ScouringDryPreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            Scouring2ScouringDryProcessingPage page = new Scouring2ScouringDryProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            OldScouring2ScouringDryFinishingPage page = new OldScouring2ScouringDryFinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion
                            }

                            else if (mc == "394")
                            {
                                #region  Scouring Coat1

                                if (ScouringCoat1Status == true)
                                {
                                    #region ScouringCoat1

                                    if (position != "")
                                    {
                                        if (position == "Preparing")
                                        {
                                            ScouringCoat1PreparingPage page = new ScouringCoat1PreparingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Processing")
                                        {
                                            ScouringCoat1ProcessingPage page = new ScouringCoat1ProcessingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                        else if (position == "Finishing")
                                        {
                                            ScouringCoat1FinishingPage page = new ScouringCoat1FinishingPage();
                                            page.Setup(session, result);
                                            PageManager.Instance.Current = page;
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Machine Status = false Please check config".ShowMessageBox();
                                }

                                #endregion

                                #region Old ScouringCoat1

                                //if (ScouringCoat1Status == true)
                                //{
                                //    #region ScouringCoat1Status

                                //    if (position != "")
                                //    {
                                //        if (position == "Preparing")
                                //        {
                                //            OldScouringCoat1PreparingPage page = new OldScouringCoat1PreparingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Processing")
                                //        {
                                //            ScouringCoat1ProcessingPage page = new ScouringCoat1ProcessingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //        else if (position == "Finishing")
                                //        {
                                //            OldScouringCoat1FinishingPage page = new OldScouringCoat1FinishingPage();
                                //            page.Setup(session, result);
                                //            PageManager.Instance.Current = page;
                                //        }
                                //    }

                                //    #endregion
                                //}
                                //else
                                //{
                                //    "Machine Status = false Please check config".ShowMessageBox();
                                //}

                                #endregion
                            }
                        }
                        else
                        {
                            //lstInstMC.SelectedIndex = 0;
                            this.lstInstMC.SelectedItem = null;
                        }

                        #endregion
                    }
                    else
                    {
                        "Machine Status = false Please check config".ShowMessageBox();
                    }
                }

                #endregion
            }
            else if (lstInstMC.SelectedIndex == instList.Count - 2)
            {
                // Finishing List
                #region Finishing List

                LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                if (logInfo != null)
                {
                    string position = logInfo.Position;

                    int processId = 6; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "User Not found or Not Authorized for this Menu".ShowMessageBox(true);
                        lstInstMC.SelectedIndex = -1; // reset index.
                        return;
                    }

                    FinishingSearchPage page = new FinishingSearchPage();
                    page.Setup(logInfo.UserName);
                    PageManager.Instance.Current = page;
                }
                else
                {
                    this.lstInstMC.SelectedItem = null;
                }

                #endregion
            }
            else if (lstInstMC.SelectedIndex == instList.Count - 1)
            {
                // Finish Record
                #region Finish Record

                LogInInfo logInfo = this.ShowLogInBox(((LuckyTex.Models.MachineBase)(this.lstInstMC.SelectedItem)).DisplayName);

                if (logInfo != null)
                {
                    string position = logInfo.Position;

                    int processId = 6; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "User Not found or Not Authorized for this Menu".ShowMessageBox(true);
                        lstInstMC.SelectedIndex = -1; // reset index.
                        return;
                    }

                    FinishRecordPage page = new FinishRecordPage();
                    page.Setup(logInfo.UserName);
                    PageManager.Instance.Current = page;
                }
                else
                {
                    this.lstInstMC.SelectedItem = null;
                }

                #endregion
            }
        }

        #endregion

        #region gridFinishing_SelectedCellsChanged

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

        private void gridFinishing_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridFinishing.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridFinishing);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            MCNAME = string.Empty;
                            FINISHINGLOT = string.Empty;
                            WEAVINGLOT = string.Empty;
                            ITM_CODE = string.Empty;
                            STATUS = string.Empty;
                            FINISHINGCUSTOMER = string.Empty;
                            STARTBY = string.Empty;
                            CONDITIONBY = string.Empty;
                            OPERATOR_GROUP = string.Empty;

                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).MCNAME != null)
                            {
                                MCNAME = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).MCNAME;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).FINISHINGLOT != null)
                            {
                                FINISHINGLOT = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).FINISHINGLOT;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                WEAVINGLOT = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).WEAVINGLOT;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).ITM_CODE != null)
                            {
                                ITM_CODE = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).ITM_CODE;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).STATUS != null)
                            {
                                STATUS = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).STATUS;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).FINISHINGCUSTOMER != null)
                            {
                                FINISHINGCUSTOMER = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).FINISHINGCUSTOMER;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).STARTBY != null)
                            {
                                STARTBY = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).STARTBY;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).CONDITIONBY != null)
                            {
                                CONDITIONBY = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).CONDITIONBY;
                            }
                            if (((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).OPERATOR_GROUP != null)
                            {
                                OPERATOR_GROUP = ((LuckyTex.Models.FINISHING_INPROCESSLIST)(gridFinishing.CurrentCell.Item)).OPERATOR_GROUP;
                            }
                            
                        }
                    }
                }
                else
                {
                    MCNAME = string.Empty;
                    FINISHINGLOT = string.Empty;
                    WEAVINGLOT = string.Empty;
                    ITM_CODE = string.Empty;
                    STATUS = string.Empty;
                    FINISHINGCUSTOMER = string.Empty;
                    STARTBY = string.Empty;
                    CONDITIONBY = string.Empty;
                    OPERATOR_GROUP = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MCNAME = string.Empty;
                FINISHINGLOT = string.Empty;
                WEAVINGLOT = string.Empty;
                ITM_CODE = string.Empty;
                STATUS = string.Empty;
                FINISHINGCUSTOMER = string.Empty;
                STARTBY = string.Empty;
                CONDITIONBY = string.Empty;
                OPERATOR_GROUP = string.Empty;

                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
