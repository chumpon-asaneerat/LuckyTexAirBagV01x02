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
    /// Interaction logic for LABPage.xaml
    /// </summary>
    public partial class LABPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LABPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            Status = new List<string> { "Pass", "Fail" };
        }

        #endregion

        #region Internal Variables

        private LABSession _session = new LABSession();
        string opera = string.Empty;
        string INSPECTIONLOT = string.Empty;

        // DataGrid List<String> Example
        public List<string> Status { get; set; }

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
            LoadLAB();
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (SaveLAB() == true)
            {
                LoadLAB();
            }
        }

        #endregion

        #endregion

        #region gridLAB_SelectedCellsChanged

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

        private void gridLAB_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridLAB.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridLAB);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.LAB_GETINSPECTIONLIST)(gridLAB.CurrentCell.Item)).INSPECTIONLOT != null)
                            {
                                INSPECTIONLOT = ((LuckyTex.Models.LAB_GETINSPECTIONLIST)(gridLAB.CurrentCell.Item)).INSPECTIONLOT;
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            dteInspectionDate.SelectedDate = null;
            dteInspectionDate.Text = "";
            txtInspectionLot.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLAB.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLAB.SelectedItems.Clear();
            else
                this.gridLAB.SelectedItem = null;

            gridLAB.ItemsSource = null;

            txtInspectionLot.SelectAll();
            txtInspectionLot.Focus();
        }

        #endregion

        #region LoadLAB
        private void LoadLAB()
        {
            string _InspectionLot = string.Empty;
            string _InspectionDate = string.Empty;

            if (txtInspectionLot.Text != "")
                _InspectionLot = txtInspectionLot.Text;

            if (dteInspectionDate.SelectedDate != null)
                _InspectionDate = dteInspectionDate.SelectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            List<LAB_GETINSPECTIONLIST> lots = new List<LAB_GETINSPECTIONLIST>();

            lots = LABDataService.Instance.LAB_GETINSPECTIONLIST(_InspectionLot, _InspectionDate);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridLAB.ItemsSource = lots;
            }
            else
            {
                gridLAB.ItemsSource = null;
            }
        }
        #endregion

        #region SaveLAB

        private bool SaveLAB()
        {
            try
            {
                if (gridLAB.SelectedItems.Count > 0)
                {

                    string INSLOT = string.Empty;
                    string OPERATOR = string.Empty;
                    DateTime? CHECKDATE = null;

                    OPERATOR = txtOperator.Text;
                    CHECKDATE = DateTime.Now;
                    string RESULT = string.Empty;

                    foreach (object obj in gridLAB.SelectedItems)
                    {
                        if (((LuckyTex.Models.LAB_GETINSPECTIONLIST)(obj)).INSPECTIONLOT != null)
                        {
                            INSLOT = ((LuckyTex.Models.LAB_GETINSPECTIONLIST)(obj)).INSPECTIONLOT;
                        }

                        if (((LuckyTex.Models.LAB_GETINSPECTIONLIST)(obj)).LABResult != null)
                        {
                            if (((LuckyTex.Models.LAB_GETINSPECTIONLIST)(obj)).LABResult == "Pass")
                                RESULT = "Y";
                            else if (((LuckyTex.Models.LAB_GETINSPECTIONLIST)(obj)).LABResult == "Fail")
                                RESULT = "N";
                        }

                        if (INSLOT != "" && RESULT != "")
                        {
                            LABDataService.Instance.LAB_SAVELABRESULT(INSLOT, RESULT);
                        }
                    }
                }

                return true;
            }
            catch
            {
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

    }
}

