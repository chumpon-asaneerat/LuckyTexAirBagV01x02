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
    /// Interaction logic for ShipmentReportPage.xaml
    /// </summary>
    public partial class ShipmentReportPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShipmentReportPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private WeavingSession _session = new WeavingSession();

        string opera = string.Empty;
        string ITM_WEAVING = string.Empty;
        
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
            string Begin = string.Empty;
            string End = string.Empty;

            if (dteBeginDate.SelectedDate != null)
                Begin = dteBeginDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (dteEndDate.SelectedDate != null)
                End = dteEndDate.SelectedDate.Value.ToString("dd/MM/yyyy");


            WEAV_SHIPMENTREPORT(Begin, End);
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdPrintReport_Click

        private void cmdPrintReport_Click(object sender, RoutedEventArgs e)
        {
            string Begin = string.Empty;
            string End = string.Empty;

            if (dteBeginDate.SelectedDate != null)
                Begin = dteBeginDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (dteEndDate.SelectedDate != null)
                End = dteEndDate.SelectedDate.Value.ToString("dd/MM/yyyy");

            Preview(Begin, End);
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

        #endregion

        #region gridWeaving

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
                            #region BEAMERROLL
                            if (((LuckyTex.Models.WEAV_SHIPMENTREPORT)(gridWeaving.CurrentCell.Item)).ITM_WEAVING != null)
                            {
                                ITM_WEAVING = ((LuckyTex.Models.WEAV_SHIPMENTREPORT)(gridWeaving.CurrentCell.Item)).ITM_WEAVING;
                            }
                            else
                            {
                                ITM_WEAVING = string.Empty;
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    ITM_WEAVING = string.Empty;
                    txtTotal.Text = string.Empty;
                    txtTotalBeam.Text = string.Empty;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEGINDATE"></param>
        /// <param name="P_ENDDATE"></param>
        private void Print(string P_BEGINDATE, string P_ENDDATE)
        {
            try
            {
                ConmonReportService.Instance.ReportName = "WEAV_SHIPMENTREPORT";
                ConmonReportService.Instance.P_BEGINDATE = P_BEGINDATE;
                ConmonReportService.Instance.P_ENDDATE = P_ENDDATE;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEGINDATE"></param>
        /// <param name="P_ENDDATE"></param>
        private void Preview(string P_BEGINDATE, string P_ENDDATE)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "WEAV_SHIPMENTREPORT";
                ConmonReportService.Instance.P_BEGINDATE = P_BEGINDATE;
                ConmonReportService.Instance.P_ENDDATE = P_ENDDATE;

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

        #region WEAV_SHIPMENTREPORT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEGINDATE"></param>
        /// <param name="P_ENDDATE"></param>
        private void WEAV_SHIPMENTREPORT(string P_BEGINDATE, string P_ENDDATE)
        {
            try
            {
                List<WEAV_SHIPMENTREPORT> lots = new List<WEAV_SHIPMENTREPORT>();

                lots = _session.GetWEAV_SHIPMENTREPORT(P_BEGINDATE, P_ENDDATE);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridWeaving.ItemsSource = lots;

                    GetTotalLength();
                }
                else
                {
                    gridWeaving.ItemsSource = null;

                    ITM_WEAVING = string.Empty;
                    txtTotal.Text = string.Empty;
                    txtTotalBeam.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// 
        /// </summary>
        private void ClearControl()
        {
            ITM_WEAVING = string.Empty;

            dteBeginDate.SelectedDate = DateTime.Now;
            dteEndDate.SelectedDate = DateTime.Now;
            
            txtTotal.Text = string.Empty;
            txtTotalBeam.Text = string.Empty;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWeaving.SelectedItems.Clear();
            else
                this.gridWeaving.SelectedItem = null;

            gridWeaving.ItemsSource = null;

            dteBeginDate.Focus();
        }

        #endregion

        #region GetTotalLength
        /// <summary>
        /// 
        /// </summary>
        private void GetTotalLength()
        {
            try
            {
                if (gridWeaving.Items.Count > 0)
                {
                    decimal? totalPieces  = 0;
                    decimal? totalMeters = 0;

                    int o = 0;
                    foreach (var row in gridWeaving.Items)
                    {
                        if (((LuckyTex.Models.WEAV_SHIPMENTREPORT)((gridWeaving.Items)[o])).PIECES != null)
                            totalPieces += ((LuckyTex.Models.WEAV_SHIPMENTREPORT)((gridWeaving.Items)[o])).PIECES;

                        if (((LuckyTex.Models.WEAV_SHIPMENTREPORT)((gridWeaving.Items)[o])).METERS != null)
                            totalMeters += ((LuckyTex.Models.WEAV_SHIPMENTREPORT)((gridWeaving.Items)[o])).METERS;

                        o++;
                    }

                    txtTotal.Text = totalPieces.Value.ToString("#,##0.##");
                    txtTotalBeam.Text = totalMeters.Value.ToString("#,##0.##");
                }
                else
                {
                    txtTotal.Text = "0";
                    txtTotalBeam.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

