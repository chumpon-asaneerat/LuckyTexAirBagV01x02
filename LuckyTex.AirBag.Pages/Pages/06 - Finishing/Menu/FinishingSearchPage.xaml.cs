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
    /// Interaction logic for FinishingSearchPage.xaml
    /// </summary>
    public partial class FinishingSearchPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingSearchPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        string WEAVINGLOT = string.Empty;
        string ITM_Code = string.Empty;
        string MC = string.Empty;
        string PROCESS = string.Empty;
        string opera = string.Empty;
        string FinishingLot = string.Empty;

        string FINISHINGCUSTOMER = string.Empty;
        string PRODUCTTYPEID = string.Empty;

        decimal? SAMPLING_WIDTH = null;
        decimal? SAMPLING_LENGTH = null;
        string REMARK = string.Empty;

        decimal? LENGTH1 = null;
        decimal? LENGTH2 = null;
        decimal? LENGTH3 = null;
        decimal? LENGTH4 = null;
        decimal? LENGTH5 = null;
        decimal? LENGTH6 = null;
        decimal? LENGTH7 = null;
        decimal? TOTALLENGTH = null;

        string P_FINISHINGLOT = string.Empty;
        string P_WEAVINGLOT = string.Empty;
        string P_PROCESS = string.Empty;
        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProcess();

            ClearControl();
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

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            WEAVINGLOT = string.Empty;
            ITM_Code = string.Empty;
            MC = string.Empty;
            PROCESS = string.Empty;
            FinishingLot = string.Empty;

            FINISHINGCUSTOMER = string.Empty;
            PRODUCTTYPEID = string.Empty;

            SAMPLING_WIDTH = null;
            SAMPLING_LENGTH = null;
            REMARK = string.Empty;

            LENGTH1 = null;
            LENGTH2 = null;
            LENGTH3 = null;
            LENGTH4 = null;
            LENGTH5 = null;
            LENGTH6 = null;
            LENGTH7 = null;
            TOTALLENGTH = null;

            LoadFinishing();
        }

        #endregion

        #region cmPrint_Click

        private void cmPrint_Click(object sender, RoutedEventArgs e)
        {
            if (WEAVINGLOT != "" &&
                ITM_Code != "" &&
                FinishingLot != "" &&
                MC != "")
            {
                //Print();
                Preview();
            }
        }

        #endregion

        #region cmdSampling_Click

        private void cmdSampling_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(WEAVINGLOT))
            {

                if (!string.IsNullOrEmpty(PROCESS))
                {
                    if (PROCESS == "Coating")
                    {
                        SamplingCoatingWindow window = new SamplingCoatingWindow();

                        window.SetupFinishingSearch(WEAVINGLOT, FinishingLot, ITM_Code, FINISHINGCUSTOMER, PRODUCTTYPEID, opera);

                        if (window.ShowDialog() == true)
                        {

                        }
                    }
                    else
                    {
                        SamplingWindow window = new SamplingWindow();

                        window.SetupFinishingSearch(WEAVINGLOT, FinishingLot, ITM_Code, FINISHINGCUSTOMER, PRODUCTTYPEID, opera, SAMPLING_WIDTH, SAMPLING_LENGTH, REMARK);

                        if (window.ShowDialog() == true)
                        {

                        }
                    }

                }
            }
        }

        #endregion

        #region cmdEditLength_Click

        private void cmdEditLength_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(WEAVINGLOT))
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 6; // for inspection
                    string R_OUT = UserDataService.Instance
                        .GetOperatorsDelete(logInfo.UserName, logInfo.Password, processId);

                    if (string.IsNullOrEmpty(R_OUT))
                    {
                        "No Authorize for Edit Length".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        FinishingEditLengthWindow window = new FinishingEditLengthWindow();

                        window.Setup(WEAVINGLOT, FinishingLot, PROCESS, opera, LENGTH1, LENGTH2, LENGTH3, LENGTH4, LENGTH5, LENGTH6, LENGTH7, TOTALLENGTH);

                        if (window.ShowDialog() == true)
                        {
                            WEAVINGLOT = string.Empty;
                            ITM_Code = string.Empty;
                            MC = string.Empty;
                            PROCESS = string.Empty;
                            FinishingLot = string.Empty;

                            FINISHINGCUSTOMER = string.Empty;
                            PRODUCTTYPEID = string.Empty;

                            SAMPLING_WIDTH = null;
                            SAMPLING_LENGTH = null;
                            REMARK = string.Empty;

                             LENGTH1 = null;
                             LENGTH2 = null;
                             LENGTH3 = null;
                             LENGTH4 = null;
                             LENGTH5 = null;
                             LENGTH6 = null;
                             LENGTH7 = null;
                             TOTALLENGTH = null;

                            LoadFinishing();
                        }
                    }
                }
            }
        }

        #endregion

        #region cmdSendD365_Click
        private void cmdSendD365_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(P_FINISHINGLOT) && !string.IsNullOrEmpty(P_WEAVINGLOT) && !string.IsNullOrEmpty(P_PROCESS))
            {
                if (D365_FN_BPO() == true)
                {
                    if (PRODID != null)
                    {
                        if (D365_FN_ISH(PRODID) == true)
                        {
                            if (HEADERID != null)
                            {
                                if (D365_FN_ISL(HEADERID) == true)
                                {
                                    if (D365_FN_OPH(PRODID) == true)
                                    {
                                        if (HEADERID != null)
                                        {
                                            if (D365_FN_OPL(HEADERID) == true)
                                            {
                                                if (D365_FN_OUH(PRODID) == true)
                                                {
                                                    if (HEADERID != null)
                                                    {
                                                        if (D365_FN_OUL(HEADERID) == true)
                                                        {
                                                            "Send D365 complete".ShowMessageBox();
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
                if (!string.IsNullOrEmpty(P_FINISHINGLOT))
                {
                    "Finishing Lot is null".ShowMessageBox();
                }
                else if (!string.IsNullOrEmpty(P_WEAVINGLOT))
                {
                    "Weaving Lot is null".ShowMessageBox();
                }
                else if (!string.IsNullOrEmpty(P_PROCESS))
                {
                    "Process is null".ShowMessageBox();
                }
            }
        }
        #endregion

        #endregion

        #region gridOperator_SelectedCellsChanged

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

        private void gridProcess_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridProcess.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridProcess);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            // Old
                            #region ScouringNo

                            //if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).FINISHINGLOT != null)
                            //{
                            //    ScouringNo = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).FINISHINGLOT;
                            //}
                            //else
                            //{
                            //    ScouringNo = "";
                            //}

                            #endregion

                            #region WEAVINGLOT

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                WEAVINGLOT = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).WEAVINGLOT;

                                if (!string.IsNullOrEmpty(WEAVINGLOT))
                                {
                                    P_WEAVINGLOT = WEAVINGLOT;
                                }
                                else
                                {
                                    WEAVINGLOT = string.Empty;
                                    P_WEAVINGLOT = string.Empty;
                                }
                            }
                            else
                            {
                                WEAVINGLOT = string.Empty;
                                P_WEAVINGLOT = string.Empty;
                            }

                            #endregion

                            #region ITM_Code

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).ITEMCODE != null)
                            {
                                ITM_Code = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).ITEMCODE;
                            }
                            else
                            {
                                ITM_Code = "";
                            }

                            #endregion

                            #region MC

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).MC != null)
                            {
                                MC = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).MC;
                            }
                            else
                            {
                                MC = "";
                            }

                            #endregion

                            #region PROCESS

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).PROCESS != null)
                            {
                                PROCESS = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).PROCESS;

                                if (!string.IsNullOrEmpty(WEAVINGLOT))
                                {
                                    P_PROCESS = PROCESS;
                                }
                                else
                                {
                                    PROCESS = string.Empty;
                                    P_PROCESS = string.Empty;
                                }
                            }
                            else
                            {
                                PROCESS = string.Empty;
                                P_PROCESS = string.Empty;
                            }

                            #endregion

                            #region FinishingLot

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).FINISHINGLOT != null)
                            {
                                FinishingLot = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).FINISHINGLOT;

                                if (!string.IsNullOrEmpty(WEAVINGLOT))
                                {
                                    P_FINISHINGLOT = FinishingLot;
                                }
                                else
                                {
                                    FinishingLot = string.Empty;
                                    P_FINISHINGLOT = string.Empty;
                                }
                            }
                            else
                            {
                                FinishingLot = string.Empty;
                                P_FINISHINGLOT = string.Empty;
                            }

                            #endregion

                            #region FINISHINGCUSTOMER

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).FINISHINGCUSTOMER != null)
                            {
                                FINISHINGCUSTOMER = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).FINISHINGCUSTOMER;
                            }
                            else
                            {
                                FINISHINGCUSTOMER = "";
                            }

                            #endregion

                            #region PRODUCTTYPEID

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).PRODUCTTYPEID != null)
                            {
                                PRODUCTTYPEID = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).PRODUCTTYPEID;
                            }
                            else
                            {
                                PRODUCTTYPEID = "";
                            }

                            #endregion

                            #region SAMPLING_WIDTH

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).SAMPLING_WIDTH != null)
                            {
                                SAMPLING_WIDTH = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).SAMPLING_WIDTH;
                            }
                            else
                            {
                                SAMPLING_WIDTH = null;
                            }

                            #endregion

                            #region SAMPLING_LENGTH

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).SAMPLING_LENGTH != null)
                            {
                                SAMPLING_LENGTH = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).SAMPLING_LENGTH;
                            }
                            else
                            {
                                SAMPLING_LENGTH = null;
                            }

                            #endregion

                            #region REMARK

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).REMARK != null)
                            {
                                REMARK = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).REMARK;
                            }
                            else
                            {
                                REMARK = "";
                            }

                            #endregion

                            #region LENGTH1

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH1 != null)
                            {
                                LENGTH1 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH1;
                            }
                            else
                            {
                                LENGTH1 = null;
                            }

                            #endregion

                            #region LENGTH2

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH2 != null)
                            {
                                LENGTH2 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH2;
                            }
                            else
                            {
                                LENGTH2 = null;
                            }

                            #endregion

                            #region LENGTH3

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH3 != null)
                            {
                                LENGTH3 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH3;
                            }
                            else
                            {
                                LENGTH3 = null;
                            }

                            #endregion

                            #region LENGTH4

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH4 != null)
                            {
                                LENGTH4 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH4;
                            }
                            else
                            {
                                LENGTH4 = null;
                            }

                            #endregion

                            #region LENGTH5

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH5 != null)
                            {
                                LENGTH5 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH5;
                            }
                            else
                            {
                                LENGTH5 = null;
                            }

                            #endregion

                            #region LENGTH6

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH6 != null)
                            {
                                LENGTH6 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH6;
                            }
                            else
                            {
                                LENGTH6 = null;
                            }

                            #endregion

                            #region LENGTH7

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH7 != null)
                            {
                                LENGTH7 = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).LENGTH7;
                            }
                            else
                            {
                                LENGTH7 = null;
                            }

                            #endregion

                            #region TOTALLENGTH

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).TOTALLENGTH != null)
                            {
                                TOTALLENGTH = ((LuckyTex.Models.FINISHING_SEARCHFINISHDATA)(gridProcess.CurrentCell.Item)).TOTALLENGTH;
                            }
                            else
                            {
                                TOTALLENGTH = null;
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    WEAVINGLOT = string.Empty;
                    ITM_Code = string.Empty;
                    MC = string.Empty;
                    PROCESS = string.Empty;
                    FinishingLot = string.Empty;
                    FINISHINGCUSTOMER = string.Empty;
                    PRODUCTTYPEID = string.Empty;

                    SAMPLING_WIDTH = null;
                    SAMPLING_LENGTH = null;
                    REMARK = string.Empty;

                    LENGTH1 = null;
                    LENGTH2 = null;
                    LENGTH3 = null;
                    LENGTH4 = null;
                    LENGTH5 = null;
                    LENGTH6 = null;
                    LENGTH7 = null;
                    TOTALLENGTH = null;

                    P_FINISHINGLOT = string.Empty;
                    P_WEAVINGLOT = string.Empty;
                    P_PROCESS = string.Empty;

                    PRODID = null;
                    HEADERID = null;

                    P_LOTNO = string.Empty;
                    P_ITEMID = string.Empty;
                    P_LOADINGTYPE = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadProcess

        private void LoadProcess()
        {
            if (cbProcess.ItemsSource == null)
            {
                string[] str = new string[] { "Coating", "Scouring", "Dryer" };

                cbProcess.ItemsSource = str;
                cbProcess.SelectedIndex = 0;
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            //dteFinishingDate.SelectedDate = null;
            //dteFinishingDate.Text = "";

            P_FINISHINGLOT = string.Empty;
            P_WEAVINGLOT = string.Empty;
            P_PROCESS = string.Empty;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;

            dteFinishingDate.SelectedDate = DateTime.Now;

            cbProcess.Text = "";
            chkAll.IsChecked = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridProcess.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridProcess.SelectedItems.Clear();
            else
                this.gridProcess.SelectedItem = null;

            gridProcess.ItemsSource = null;
            
        }

        #endregion

        #region LoadFinishing
        private void LoadFinishing()
        {
            string _FinishingDate = string.Empty;
            string _Process = string.Empty;

            P_FINISHINGLOT = string.Empty;
            P_WEAVINGLOT = string.Empty;
            P_PROCESS = string.Empty;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;

            if (chkAll.IsChecked == false)
            {
                if (dteFinishingDate.SelectedDate != null)
                    _FinishingDate = dteFinishingDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

                if (cbProcess.SelectedValue != null)
                    _Process = cbProcess.SelectedValue.ToString();
            }

            List<FINISHING_SEARCHFINISHDATA> lots = new List<FINISHING_SEARCHFINISHDATA>();

            lots = InspectionDataService.Instance.Finishing_SearchDataList(_FinishingDate, _Process);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridProcess.ItemsSource = lots;
            }
            else
            {
                gridProcess.ItemsSource = null;
            }
        }
        #endregion

        #region D365_FN_BPO
        private bool D365_FN_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_BPOData> results = new List<ListD365_FN_BPOData>();

                results = D365DataService.Instance.D365_FN_BPO(P_FINISHINGLOT,  P_WEAVINGLOT,  P_PROCESS);

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
                    "D365_FN_BPO Row = 0".Info();
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

        #region D365_FN_ISH
        private bool D365_FN_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_FN_ISHData> results = new List<D365_FN_ISHData>();

                results = D365DataService.Instance.D365_FN_ISH(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_ISH Row = 0".Info();
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

        #region D365_FN_ISL
        private bool D365_FN_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_ISLData> results = new List<ListD365_FN_ISLData>();

                results = D365DataService.Instance.D365_FN_ISL(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_ISL Row = 0".Info();
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

        #region D365_FN_OPH
        private bool D365_FN_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_FN_OPHData> results = new List<D365_FN_OPHData>();

                results = D365DataService.Instance.D365_FN_OPH(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OPH Row = 0".Info();
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

        #region D365_FN_OPL
        private bool D365_FN_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_OPLData> results = new List<ListD365_FN_OPLData>();

                results = D365DataService.Instance.D365_FN_OPL(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OPL Row = 0".Info();
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

        #region D365_FN_OUH
        private bool D365_FN_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_FN_OUHData> results = new List<D365_FN_OUHData>();

                results = D365DataService.Instance.D365_FN_OUH(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OUH Row = 0".Info();
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

        #region D365_FN_OUL
        private bool D365_FN_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_FN_OULData> results = new List<ListD365_FN_OULData>();

                results = D365DataService.Instance.D365_FN_OUL(P_FINISHINGLOT, P_WEAVINGLOT, P_PROCESS);

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
                    "D365_FN_OUL Row = 0".Info();
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
        public void Setup(string user)
        {
            if (opera != null)
            {
                opera = user;
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print()
        {
            try
            {
                //ConmonReportService.Instance.ScouringNo = ScouringNo;
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = ITM_Code;
                ConmonReportService.Instance.FinishingLot = FinishingLot;

                if (MC != "")
                {
                    if (MC == "Scouring1 Dryer" || MC == "Scouring2 Scouring Dry" || MC == "Coating1 Dryer" || MC == "Coat #1 Scouring-Dry")
                    {
                        if (WEAVINGLOT != "" && ITM_Code != "")
                        {
                            ConmonReportService.Instance.ScouringNo = CoatingDataService.Instance.FINISHING_GETDryerMCNO(WEAVINGLOT, FinishingLot);
                        }
                    }
                    else if (MC == "Scouring1" || MC == "Scouring2" || MC == "Scouring5" || MC == "Scouring Coat2" || MC == "Scouring Coat3")
                    {
                        if (WEAVINGLOT != "" && ITM_Code != "")
                        {
                            ConmonReportService.Instance.ScouringNo = CoatingDataService.Instance.FINISHING_GETScouringMCNO(WEAVINGLOT, FinishingLot);
                        }
                    }
                    else if (MC == "Coating1" || MC == "Coating2" || MC == "Coating3" || MC == "Coating1 Coat")
                    {
                        if (WEAVINGLOT != "" && ITM_Code != "")
                        {
                            ConmonReportService.Instance.ScouringNo = CoatingDataService.Instance.FINISHING_GETCoatingMCNO(WEAVINGLOT, FinishingLot);
                        }
                    }
                    else
                    {
                        ConmonReportService.Instance.ScouringNo = "";
                    }
                }


                if (MC != "")
                {
                    if (MC == "Scouring1 Dryer")
                    {
                        ConmonReportService.Instance.ReportName = "ScouringDryer";
                    }
                    else if (MC == "Scouring2 Scouring Dry")
                    {
                        ConmonReportService.Instance.ReportName = "Scouring2ScouringDry";
                    }
                    else if (MC == "Coating1 Dryer")
                    {
                        ConmonReportService.Instance.ReportName = "Coating1Dryer";
                    }
                    else if (MC == "Coat #1 Scouring-Dry")
                    {
                        ConmonReportService.Instance.ReportName = "Coating1Scouring";
                    }
                    else if (MC == "Scouring1")
                    {
                        ConmonReportService.Instance.ReportName = "Scouring1";
                    }
                    else if (MC == "Scouring2")
                    {
                        ConmonReportService.Instance.ReportName = "Scouring2";
                    }
                    else if (MC == "Scouring5")
                    {
                        ConmonReportService.Instance.ReportName = "ScouringCoat1";
                    }
                    else if (MC == "Scouring Coat2")
                    {
                        ConmonReportService.Instance.ReportName = "ScouringCoat2";
                    }
                    else if (MC == "Scouring Coat3")
                    {
                        ConmonReportService.Instance.ReportName = "Coating3Scouring";
                    }                
                    else if (MC == "Coating1")
                    {
                        ConmonReportService.Instance.ReportName = "Coating1";
                    }
                    else if (MC == "Coating2")
                    {
                        ConmonReportService.Instance.ReportName = "Coating2";
                    }
                    else if (MC == "Coating3")
                    {
                        ConmonReportService.Instance.ReportName = "Coating3";
                    }
                    else if (MC == "Coating1 Coat")
                    {
                        ConmonReportService.Instance.ReportName = "Coating12Step";
                    }
                    else
                    {
                        ConmonReportService.Instance.ReportName = MC;
                    }
                }

                if (!string.IsNullOrEmpty(ConmonReportService.Instance.ScouringNo) && !string.IsNullOrEmpty(ConmonReportService.Instance.ReportName))
                {
                    StringBuilder dp = new StringBuilder(256);
                    int size = dp.Capacity;
                    if (GetDefaultPrinter(dp, ref size))
                    {
                        DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                        rep.PrintByPrinter(dp.ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview()
        {
            try
            {
                //Old
                //ConmonReportService.Instance.ScouringNo = MC;

                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.ITM_Code = ITM_Code;
                ConmonReportService.Instance.FinishingLot = FinishingLot;

                
                #region MC No
                if (MC != "")
                {
                    if (MC == "Scouring1 Dryer" || MC == "Scouring2 Scouring Dry" || MC == "Coating1 Dryer" || MC == "Coat #1 Scouring-Dry")
                    {
                        if (WEAVINGLOT != "" && ITM_Code != "")
                        {
                            ConmonReportService.Instance.ScouringNo = CoatingDataService.Instance.FINISHING_GETDryerMCNO(WEAVINGLOT, FinishingLot);
                        }
                    }
                    else if (MC == "Scouring1" || MC == "Scouring2" || MC == "Scouring5" || MC == "Scouring Coat2" || MC == "Scouring Coat3")
                    {
                        if (WEAVINGLOT != "" && ITM_Code != "")
                        {
                            ConmonReportService.Instance.ScouringNo = CoatingDataService.Instance.FINISHING_GETScouringMCNO(WEAVINGLOT, FinishingLot);
                        }
                    }
                    else if (MC == "Coating1" || MC == "Coating2" || MC == "Coating3" || MC == "Coating1 Coat")
                    {
                        if (WEAVINGLOT != "" && ITM_Code != "")
                        {
                            ConmonReportService.Instance.ScouringNo = CoatingDataService.Instance.FINISHING_GETCoatingMCNO(WEAVINGLOT, FinishingLot);
                        }
                    }
                    else
                    {
                        ConmonReportService.Instance.ScouringNo = "";
                    }
                }
                else
                {
                    MessageBox.Show("MC is null", "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                #endregion

                if (MC != "")
                {
                    if (MC == "Scouring1 Dryer")
                    {
                        ConmonReportService.Instance.ReportName = "ScouringDryer";
                    }
                    else if (MC == "Scouring2 Scouring Dry")
                    {
                        ConmonReportService.Instance.ReportName = "Scouring2ScouringDry";
                    }
                    else if (MC == "Coating1 Dryer")
                    {
                        ConmonReportService.Instance.ReportName = "Coating1Dryer";
                    }
                    else if (MC == "Coat #1 Scouring-Dry")
                    {
                        ConmonReportService.Instance.ReportName = "Coating1Scouring";
                    }
                    else if (MC == "Scouring1")
                    {
                        ConmonReportService.Instance.ReportName = "Scouring1";
                    }
                    else if (MC == "Scouring2")
                    {
                        ConmonReportService.Instance.ReportName = "Scouring2";
                    }
                    else if (MC == "Scouring5")
                    {
                        ConmonReportService.Instance.ReportName = "ScouringCoat1";
                    }
                    else if (MC == "Scouring Coat2")
                    {
                        ConmonReportService.Instance.ReportName = "ScouringCoat2";
                    }
                    else if (MC == "Scouring Coat3")
                    {
                        ConmonReportService.Instance.ReportName = "Coating3Scouring";
                    }
                    else if (MC == "Coating1")
                    {
                        ConmonReportService.Instance.ReportName = "Coating1";
                    }
                    else if (MC == "Coating2")
                    {
                        ConmonReportService.Instance.ReportName = "Coating2";
                    }
                    else if (MC == "Coating3")
                    {
                        ConmonReportService.Instance.ReportName = "Coating3";
                    }
                    else if (MC == "Coating1 Coat")
                    {
                        ConmonReportService.Instance.ReportName = "Coating12Step";
                    }
                    else
                    {
                        ConmonReportService.Instance.ReportName = MC;
                    }
                }

                if (!string.IsNullOrEmpty(ConmonReportService.Instance.ScouringNo) && !string.IsNullOrEmpty(ConmonReportService.Instance.ReportName))
                {
                    var newWindow = new RepMasterForm();
                    newWindow.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

    }
}
