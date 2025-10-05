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
    /// Interaction logic for LabFinishRecordPage.xaml
    /// </summary>
    public partial class LabFinishRecordPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LabFinishRecordPage()
        {
            InitializeComponent();
            LoadItemGood();
            LoadMachineList("6");

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private FinishingSession _session = new FinishingSession();

        string gridWEAVINGLOT = string.Empty;
      
        string opera = string.Empty;
       
        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();
            
            if (opera != "")
                txtOperator.Text = opera;
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

            string FinishingDate = string.Empty;
            string Process = string.Empty;
            string itemCode = string.Empty;

            if (dteFinishingDate.SelectedDate != null)
                FinishingDate = dteFinishingDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (cbProcess.SelectedValue != null)
                Process = cbProcess.SelectedValue.ToString();

            if (cbItemCode.SelectedValue != null)
                itemCode = cbItemCode.SelectedValue.ToString();

            FINISHING_SEARCHFINISHRECORD(FinishingDate, Process, itemCode);

        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdPrint_Click

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            string FinishingDate = string.Empty;
            string Process = string.Empty;
            string itemCode = string.Empty;

            if (dteFinishingDate.SelectedDate != null)
                FinishingDate = dteFinishingDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (cbProcess.SelectedValue != null)
                Process = cbProcess.SelectedValue.ToString();


            if (cbItemCode.SelectedValue != null)
                itemCode = cbItemCode.SelectedValue.ToString();

            Preview(FinishingDate, Process, itemCode);
        }

        #endregion

        #endregion

        #region gridMASSPROSTOCKSTATUS_SelectedCellsChanged

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

        private void gridFINISHRECORD_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridFINISHRECORD.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridFINISHRECORD);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                            gridWEAVINGLOT = string.Empty;

                            if (((LuckyTex.Models.FINISHING_SEARCHFINISHRECORD)(gridFINISHRECORD.CurrentCell.Item)).WEAVINGLOT != null)
                            {
                                gridWEAVINGLOT = ((LuckyTex.Models.FINISHING_SEARCHFINISHRECORD)(gridFINISHRECORD.CurrentCell.Item)).WEAVINGLOT;
                            }
                           
                        }
                    }
                }
                else
                {
                    gridWEAVINGLOT = string.Empty;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                gridWEAVINGLOT = string.Empty;
                   
            }
        }

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string P_DATE, string P_MCNO, string ITM_Code)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "LabFinishRecord";
                ConmonReportService.Instance.CmdStringDate = P_DATE;
                ConmonReportService.Instance.MCNO = P_MCNO;
                ConmonReportService.Instance.ITM_Code = ITM_Code;

                if (!string.IsNullOrEmpty(cbProcess.Text))
                    ConmonReportService.Instance.CmdString = cbProcess.Text;

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

        private void Preview(string P_DATE, string P_MCNO, string ITM_Code)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "LabFinishRecord";
                ConmonReportService.Instance.CmdStringDate = P_DATE;
                ConmonReportService.Instance.MCNO = P_MCNO;
                ConmonReportService.Instance.ITM_Code = ITM_Code;

                if (!string.IsNullOrEmpty(cbProcess.Text))
                ConmonReportService.Instance.CmdString = cbProcess.Text;

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

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMCODELIST> items = _session.GetItemCodeData();

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

        #region LoadMachineList

        private void LoadMachineList(string P_PROCESSID)
        {
            try
            {
                List<GETMACHINELISTBYPROCESSID> items = _session.GetGETMACHINELISTBYPROCESSID(P_PROCESSID);

                this.cbProcess.ItemsSource = items;
                this.cbProcess.DisplayMemberPath = "MCNAME";
                this.cbProcess.SelectedValuePath = "MACHINEID";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {

            dteFinishingDate.SelectedDate = null;
            dteFinishingDate.Text = "";

            cbProcess.SelectedValue = 252;

            cbItemCode.SelectedValue = null;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridFINISHRECORD.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridFINISHRECORD.SelectedItems.Clear();
            else
                this.gridFINISHRECORD.SelectedItem = null;

            gridFINISHRECORD.ItemsSource = null;

            gridWEAVINGLOT = string.Empty;
           
            dteFinishingDate.Focus();
        }

        #endregion

        #region FINISHING_SEARCHFINISHRECORD

        private void FINISHING_SEARCHFINISHRECORD(string P_DATE, string P_MCNO, string P_ITMCODE)
        {
            List<FINISHING_SEARCHFINISHRECORD> lots = new List<FINISHING_SEARCHFINISHRECORD>();

            lots = _session.GetFINISHING_SEARCHFINISHRECORD(P_DATE, P_MCNO, P_ITMCODE);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridFINISHRECORD.ItemsSource = lots;
            }
            else
            {
                gridFINISHRECORD.ItemsSource = null;
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

    }
}

