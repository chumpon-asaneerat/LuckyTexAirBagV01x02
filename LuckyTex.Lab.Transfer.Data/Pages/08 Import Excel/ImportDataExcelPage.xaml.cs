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
using System.Text.RegularExpressions;

using NPOI.HSSF.UserModel;
using NPOI.HSSF.Model;
using NPOI.SS.UserModel;

using NPOI.XSSF.UserModel;
using NPOI.XSSF.Model;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for ImportDataExcelPage.xaml
    /// </summary>
    public partial class ImportDataExcelPage : UserControl
    {
        #region ExcelTestPage
        public ImportDataExcelPage()
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
        string positionLevel = string.Empty;

        string strFileName = string.Empty;

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

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);
            OpenExcel();
            buttonEnabled(true);
        }

        #endregion

        #region cmdSave2_Click

        private void cmdSave2_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);
            OpenExcel2();
            buttonEnabled(true);
        }

        #endregion

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);
            OpenExcel_LoadTest();
            buttonEnabled(true);
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #endregion

        #region gridPTF_SelectedCellsChanged

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

        private void gridLabEntryProduction_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridLabEntryProduction.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridLabEntryProduction);
                    foreach (DataGridRow single_row in row_list)
                    {
                        //if (single_row.IsSelected == true)
                        //{
                        //    if (((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE != null
                        //        && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).WEAVINGLOT != null
                        //        && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).FINISHINGLOT != null
                        //        && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).ENTRYDATE != null
                        //        && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).STATUS != null)
                        //    {

                        //    }
                        //}
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

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdClear.IsEnabled = enabled;
            cmdSearch.IsEnabled = enabled;
            cmdSave.IsEnabled = enabled;
            cmdSave2.IsEnabled = enabled;
        }
        #endregion

        #region Load Test
        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLabEntryProduction.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLabEntryProduction.SelectedItems.Clear();
            else
                this.gridLabEntryProduction.SelectedItem = null;

            gridLabEntryProduction.ItemsSource = null;

            listView.ItemsSource = null;
            listView.Items.Clear();
        }

        #endregion

        #region OpenExcel_LoadTest
        private void OpenExcel_LoadTest()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
                openFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string csv = string.Empty;
                csv = "Excel 97-2003 file (*.xls)|*.xls|Excel file (*.xlsx)|*.xlsx";

                openFileDialog1.Filter = csv;
                openFileDialog1.FilterIndex = 1;

                Nullable<bool> result = openFileDialog1.ShowDialog();

                if (result == true)
                {
                    string newFileName = openFileDialog1.FileName;

                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        try
                        {
                            if (newFileName.Contains(".xlsx") == true)
                            {
                                if (this.gridLabEntryProduction.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                    this.gridLabEntryProduction.SelectedItems.Clear();
                                else
                                    this.gridLabEntryProduction.SelectedItem = null;

                                gridLabEntryProduction.ItemsSource = null;

                                List<LAB_ImportExcel1> results = ExcelToDataTable(newFileName);

                                if (results != null)
                                {
                                    if (results.Count > 0)
                                    {
                                        gridLabEntryProduction.ItemsSource = results;
                                    }
                                }
                            }
                            else
                            {
                                if (this.gridLabEntryProduction.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                    this.gridLabEntryProduction.SelectedItems.Clear();
                                else
                                    this.gridLabEntryProduction.SelectedItem = null;

                                gridLabEntryProduction.ItemsSource = null;

                                List<LAB_ImportExcel1> results = ExcelToDataTable97(newFileName);

                                if (results != null)
                                {
                                    if (results.Count > 0)
                                    {
                                        gridLabEntryProduction.ItemsSource = results;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.Err();
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

        #region ExcelToDataTable
        public static List<LAB_ImportExcel1> ExcelToDataTable(string filePath)
        {
            List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();
            try
            {
                decimal value;

                XSSFWorkbook hssfworkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                int s = hssfworkbook.ActiveSheetIndex;

                XSSFSheet sheet = (XSSFSheet)hssfworkbook.GetSheetAt(s);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                XSSFRow headerRow = (XSSFRow)sheet.GetRow(0);
                string itemcode = string.Empty;
                int startRow = 6;

                if (!string.IsNullOrEmpty(headerRow.Cells[0].ToString()))
                    itemcode = headerRow.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(headerRow.Cells[1].ToString()))
                    itemcode = headerRow.Cells[1].ToString();

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();

                if (!string.IsNullOrEmpty(itemcode))
                {
                    #region Def

                    int p_weavinglog = 0;
                    int p_finishinglot = 0;
                    int p_entrydate = 0;
                    int p_width = 0;
                    int p_usewidth1 = 0;
                    int p_usewidth2 = 0;
                    int p_usewidth3 = 0;
                    int p_totalweight1 = 0;
                    int p_totalweight2 = 0;
                    int p_totalweight3 = 0;
                    int p_thickness1 = 0;
                    int p_thickness2 = 0;
                    int p_thickness3 = 0;
                    int p_numthreads_w1 = 0;
                    int p_numthreads_w2 = 0;
                    int p_numthreads_w3 = 0;
                    int p_numthreads_f1 = 0;
                    int p_numthreads_f2 = 0;
                    int p_numthreads_f3 = 0;
                    int p_maxforce_w1 = 0;
                    int p_maxforce_w2 = 0;
                    int p_maxforce_w3 = 0;
                    int p_elogation_w1 = 0;
                    int p_elogation_w2 = 0;
                    int p_elogation_w3 = 0;
                    int p_maxforce_f1 = 0;
                    int p_maxforce_f2 = 0;
                    int p_maxforce_f3 = 0;
                    int p_elogation_f1 = 0;
                    int p_elogation_f2 = 0;
                    int p_elogation_f3 = 0;
                    int p_edgecomb_w1 = 0;
                    int p_edgecomb_w2 = 0;
                    int p_edgecomb_w3 = 0;
                    int p_edgecomb_f1 = 0;
                    int p_edgecomb_f2 = 0;
                    int p_edgecomb_f3 = 0;
                    int p_stiffness_w1 = 0;
                    int p_stiffness_w2 = 0;
                    int p_stiffness_w3 = 0;
                    int p_stiffness_f1 = 0;
                    int p_stiffness_f2 = 0;
                    int p_stiffness_f3 = 0;
                    int p_tear_w1 = 0;
                    int p_tear_w2 = 0;
                    int p_tear_w3 = 0;
                    int p_tear_f1 = 0;
                    int p_tear_f2 = 0;
                    int p_tear_f3 = 0;
                    int p_static_air1 = 0;
                    int p_static_air2 = 0;
                    int p_static_air3 = 0;
                    int p_dynamic_air1 = 0;
                    int p_dynamic_air2 = 0;
                    int p_dynamic_air3 = 0;

                    int p_exponent1 = 0;
                    int p_exponent2 = 0;
                    int p_exponent3 = 0;

                    int p_flammability_w = 0;
                    //int p_flammability_w2 = 0;
                    //int p_flammability_w3 = 0;
                    //int p_flammability_w4 = 0;
                    //int p_flammability_w5 = 0;

                    int p_flammability_f = 0;
                    //int p_flammability_f2 = 0;
                    //int p_flammability_f3 = 0;
                    //int p_flammability_f4 = 0;
                    //int p_flammability_f5 = 0;

                    int p_dimenschange_w1 = 0;
                    int p_dimenschange_w2 = 0;
                    int p_dimenschange_w3 = 0;
                    int p_dimenschange_f1 = 0;
                    int p_dimenschange_f2 = 0;
                    int p_dimenschange_f3 = 0;

                    int p_bow1 = 0;
                    int p_bow2 = 0;
                    int p_bow3 = 0;
                    int p_skew1 = 0;
                    int p_skew2 = 0;
                    int p_skew3 = 0;

                    int p_entryby = 0;
                    int p_approveby = 0;

                    int chkCoulmn = 0;
                    int chknull = 0;

                    #endregion

                    XSSFRow row = (XSSFRow)sheet.GetRow(startRow);

                    // Set Head
                    #region Set Head

                    for (int j = 0; j < row.Cells.Count + 1; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType != CellType.Blank)
                            {
                                if (row.GetCell(j).ToString() != "")
                                {
                                    if (chkCoulmn == 1)
                                        p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 3)
                                        p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 4)
                                        p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 5)
                                        p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 6)
                                        p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 7)
                                        p_usewidth2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 8)
                                        p_usewidth3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 9)
                                        p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 10)
                                        p_totalweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 11)
                                        p_totalweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 12)
                                        p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 13)
                                        p_thickness2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 14)
                                        p_thickness3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 15)
                                        p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 16)
                                        p_numthreads_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 17)
                                        p_numthreads_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 18)
                                        p_numthreads_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 19)
                                        p_numthreads_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 20)
                                        p_numthreads_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 21)
                                        p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 22)
                                        p_maxforce_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 23)
                                        p_maxforce_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 24)
                                        p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 25)
                                        p_elogation_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 26)
                                        p_elogation_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 27)
                                        p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 28)
                                        p_maxforce_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 29)
                                        p_maxforce_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 30)
                                        p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 31)
                                        p_elogation_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 32)
                                        p_elogation_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 33)
                                        p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 34)
                                        p_edgecomb_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 35)
                                        p_edgecomb_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 36)
                                        p_edgecomb_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 37)
                                        p_edgecomb_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 38)
                                        p_edgecomb_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 39)
                                        p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 40)
                                        p_stiffness_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 41)
                                        p_stiffness_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 42)
                                        p_stiffness_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 43)
                                        p_stiffness_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 44)
                                        p_stiffness_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 45)
                                        p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 46)
                                        p_tear_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 47)
                                        p_tear_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 48)
                                        p_tear_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 49)
                                        p_tear_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 50)
                                        p_tear_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 51)
                                        p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 52)
                                        p_static_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 53)
                                        p_static_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 60)
                                        p_dynamic_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 61)
                                        p_dynamic_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 62)
                                        p_dynamic_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 63)
                                        p_exponent1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 64)
                                        p_exponent2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 65)
                                        p_exponent3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 66)
                                        p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 67)
                                        p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 68)
                                        p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 69)
                                        p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 70)
                                        p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 71)
                                        p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 72)
                                        p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 73)
                                        p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 74)
                                        p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 75)
                                        p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 83)
                                        p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 84)
                                        p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 85)
                                        p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 86)
                                        p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 87)
                                        p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 88)
                                        p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());


                                    #region Old

                                    //if (chkCoulmn == 66)
                                    //    p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 67)
                                    //    p_flammability_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 68)
                                    //    p_flammability_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 69)
                                    //    p_flammability_w4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 70)
                                    //    p_flammability_w5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 71)
                                    //    p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 72)
                                    //    p_flammability_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 73)
                                    //    p_flammability_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 74)
                                    //    p_flammability_f4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 75)
                                    //    p_flammability_f5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 76)
                                    //    p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 77)
                                    //    p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 78)
                                    //    p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 79)
                                    //    p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 80)
                                    //    p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 81)
                                    //    p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 82)
                                    //    p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 83)
                                    //    p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 91)
                                    //    p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 92)
                                    //    p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 93)
                                    //    p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 94)
                                    //    p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 95)
                                    //    p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 96)
                                    //    p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    #endregion

                                    chkCoulmn++;
                                }
                            }

                            if (p_entryby != 0)
                            {
                                if (row.GetCell(j).CellType == CellType.Blank)
                                {
                                    if (chknull >= 0 && chknull <= 3)
                                    {
                                        chknull++;
                                    }
                                    else
                                        break;
                                }
                            }
                        }
                    }

                    #endregion

                    #region Add Detail

                    for (int i = (startRow); i <= startRow+3; i++)
                    {
                        LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                        row = (XSSFRow)sheet.GetRow(i);

                        if (row.Cells.Count() >= chkCoulmn)
                        {
                            inst.ITM_CODE = itemcode;

                            if (row.Cells[p_weavinglog] == null)
                            {
                                break;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                {
                                    break;
                                }
                                else
                                {
                                    if (p_weavinglog != 0)
                                        inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();

                                    if (inst.WEAVINGLOT.Count() < 9)
                                        break;
                                }
                            }


                            if (p_finishinglot != 0)
                                inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                            if (p_entrydate != 0)
                            {
                                if (row.Cells[p_entrydate] != null)
                                    inst.ENTRYDATE = DateTime.Parse(row.Cells[p_entrydate].ToString());
                            }

                            #region WIDTH
                            if (p_width != 0)
                            {
                                if (row.Cells[p_width] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_width].ToString(), out value))
                                    {
                                        inst.WIDTH = decimal.Parse(row.Cells[p_width].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region USABLE_WIDTH
                            if (p_usewidth1 != 0)
                            {
                                if (row.Cells[p_usewidth1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_usewidth1].ToString(), out value))
                                    {
                                        inst.USABLE_WIDTH1 = decimal.Parse(row.Cells[p_usewidth1].ToString());
                                    }
                                }
                                if (row.Cells[p_usewidth2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_usewidth2].ToString(), out value))
                                    {
                                        inst.USABLE_WIDTH2 = decimal.Parse(row.Cells[p_usewidth2].ToString());
                                    }
                                }
                                if (row.Cells[p_usewidth3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_usewidth3].ToString(), out value))
                                    {
                                        inst.USABLE_WIDTH3 = decimal.Parse(row.Cells[p_usewidth3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region TOTALWEIGHT
                            if (p_totalweight1 != 0)
                            {
                                if (row.Cells[p_totalweight1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_totalweight1].ToString(), out value))
                                    {
                                        inst.TOTALWEIGHT1 = decimal.Parse(row.Cells[p_totalweight1].ToString());
                                    }
                                }
                                if (row.Cells[p_totalweight2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_totalweight2].ToString(), out value))
                                    {
                                        inst.TOTALWEIGHT2 = decimal.Parse(row.Cells[p_totalweight2].ToString());
                                    }
                                }
                                if (row.Cells[p_totalweight3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_totalweight3].ToString(), out value))
                                    {
                                        inst.TOTALWEIGHT3 = decimal.Parse(row.Cells[p_totalweight3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region THICKNESS
                            if (p_thickness1 != 0)
                            {
                                if (row.Cells[p_thickness1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_thickness1].ToString(), out value))
                                    {
                                        inst.THICKNESS1 = decimal.Parse(row.Cells[p_thickness1].ToString());
                                    }
                                }
                                if (row.Cells[p_thickness2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_thickness2].ToString(), out value))
                                    {
                                        inst.THICKNESS2 = decimal.Parse(row.Cells[p_thickness2].ToString());
                                    }
                                }
                                if (row.Cells[p_thickness3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_thickness3].ToString(), out value))
                                    {
                                        inst.THICKNESS3 = decimal.Parse(row.Cells[p_thickness3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region NUMTHREADS
                            if (p_numthreads_w1 != 0)
                            {
                                if (row.Cells[p_numthreads_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_w1].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_W1 = decimal.Parse(row.Cells[p_numthreads_w1].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_w2].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_W2 = decimal.Parse(row.Cells[p_numthreads_w2].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_w3].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_W3 = decimal.Parse(row.Cells[p_numthreads_w3].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_f1].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_F1 = decimal.Parse(row.Cells[p_numthreads_f1].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_f2].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_F2 = decimal.Parse(row.Cells[p_numthreads_f2].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_f3].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_F3 = decimal.Parse(row.Cells[p_numthreads_f3].ToString()) * (10);
                                    }
                                }
                            }
                            #endregion

                            #region MAXFORCE
                            if (p_maxforce_w1 != 0)
                            {
                                if (row.Cells[p_maxforce_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_w1].ToString(), out value))
                                    {
                                        inst.MAXFORCE_W1 = decimal.Parse(row.Cells[p_maxforce_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_w2].ToString(), out value))
                                    {
                                        inst.MAXFORCE_W2 = decimal.Parse(row.Cells[p_maxforce_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_w3].ToString(), out value))
                                    {
                                        inst.MAXFORCE_W3 = decimal.Parse(row.Cells[p_maxforce_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_f1].ToString(), out value))
                                    {
                                        inst.MAXFORCE_F1 = decimal.Parse(row.Cells[p_maxforce_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_f2].ToString(), out value))
                                    {
                                        inst.MAXFORCE_F2 = decimal.Parse(row.Cells[p_maxforce_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_f3].ToString(), out value))
                                    {
                                        inst.MAXFORCE_F3 = decimal.Parse(row.Cells[p_maxforce_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region ELONGATIONFORCE
                            if (p_elogation_w1 != 0)
                            {
                                if (row.Cells[p_elogation_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_w1].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_W1 = decimal.Parse(row.Cells[p_elogation_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_w2].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_W2 = decimal.Parse(row.Cells[p_elogation_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_w3].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_W3 = decimal.Parse(row.Cells[p_elogation_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_f1].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_F1 = decimal.Parse(row.Cells[p_elogation_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_f2].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_F2 = decimal.Parse(row.Cells[p_elogation_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_f3].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_F3 = decimal.Parse(row.Cells[p_elogation_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region EDGECOMB
                            if (p_edgecomb_w1 != 0)
                            {
                                if (row.Cells[p_edgecomb_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_w1].ToString(), out value))
                                    {
                                        inst.EDGECOMB_W1 = decimal.Parse(row.Cells[p_edgecomb_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_w2].ToString(), out value))
                                    {
                                        inst.EDGECOMB_W2 = decimal.Parse(row.Cells[p_edgecomb_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_w3].ToString(), out value))
                                    {
                                        inst.EDGECOMB_W3 = decimal.Parse(row.Cells[p_edgecomb_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_f1].ToString(), out value))
                                    {
                                        inst.EDGECOMB_F1 = decimal.Parse(row.Cells[p_edgecomb_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_f2].ToString(), out value))
                                    {
                                        inst.EDGECOMB_F2 = decimal.Parse(row.Cells[p_edgecomb_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_f3].ToString(), out value))
                                    {
                                        inst.EDGECOMB_F3 = decimal.Parse(row.Cells[p_edgecomb_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region STIFFNESS
                            if (p_stiffness_w1 != 0)
                            {
                                if (row.Cells[p_stiffness_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_w1].ToString(), out value))
                                    {
                                        inst.STIFFNESS_W1 = decimal.Parse(row.Cells[p_stiffness_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_w2].ToString(), out value))
                                    {
                                        inst.STIFFNESS_W2 = decimal.Parse(row.Cells[p_stiffness_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_w3].ToString(), out value))
                                    {
                                        inst.STIFFNESS_W3 = decimal.Parse(row.Cells[p_stiffness_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_f1].ToString(), out value))
                                    {
                                        inst.STIFFNESS_F1 = decimal.Parse(row.Cells[p_stiffness_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_f2].ToString(), out value))
                                    {
                                        inst.STIFFNESS_F2 = decimal.Parse(row.Cells[p_stiffness_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_f3].ToString(), out value))
                                    {
                                        inst.STIFFNESS_F3 = decimal.Parse(row.Cells[p_stiffness_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region TEAR
                            if (p_tear_w1 != 0)
                            {
                                if (row.Cells[p_tear_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_w1].ToString(), out value))
                                    {
                                        inst.TEAR_W1 = decimal.Parse(row.Cells[p_tear_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_w2].ToString(), out value))
                                    {
                                        inst.TEAR_W2 = decimal.Parse(row.Cells[p_tear_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_w3].ToString(), out value))
                                    {
                                        inst.TEAR_W3 = decimal.Parse(row.Cells[p_tear_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_f1].ToString(), out value))
                                    {
                                        inst.TEAR_F1 = decimal.Parse(row.Cells[p_tear_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_f2].ToString(), out value))
                                    {
                                        inst.TEAR_F2 = decimal.Parse(row.Cells[p_tear_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_f3].ToString(), out value))
                                    {
                                        inst.TEAR_F3 = decimal.Parse(row.Cells[p_tear_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region STATIC_AIR
                            if (p_static_air1 != 0)
                            {
                                if (row.Cells[p_static_air1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_static_air1].ToString(), out value))
                                    {
                                        inst.STATIC_AIR1 = decimal.Parse(row.Cells[p_static_air1].ToString());
                                    }
                                    else
                                    {
                                        if (evaluator.Evaluate(row.Cells[p_static_air1]) != null)
                                            inst.STATIC_AIR1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air1]).NumberValue.ToString());
                                    }

                                }
                                if (row.Cells[p_static_air2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_static_air2].ToString(), out value))
                                    {
                                        inst.STATIC_AIR2 = decimal.Parse(row.Cells[p_static_air2].ToString());
                                    }
                                    else
                                    {
                                        if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                            inst.STATIC_AIR2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air2]).NumberValue.ToString());
                                    }
                                }
                                if (row.Cells[p_static_air3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_static_air3].ToString(), out value))
                                    {
                                        inst.STATIC_AIR3 = decimal.Parse(row.Cells[p_static_air3].ToString());
                                    }
                                    else
                                    {
                                        if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                            inst.STATIC_AIR3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air3]).NumberValue.ToString());
                                    }
                                }
                            }
                            #endregion

                            #region DYNAMIC_AIR
                            if (p_dynamic_air1 != 0)
                            {
                                if (row.Cells[p_dynamic_air1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dynamic_air1].ToString(), out value))
                                    {
                                        inst.DYNAMIC_AIR1 = decimal.Parse(row.Cells[p_dynamic_air1].ToString());
                                    }
                                }
                                if (row.Cells[p_dynamic_air2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dynamic_air2].ToString(), out value))
                                    {
                                        inst.DYNAMIC_AIR2 = decimal.Parse(row.Cells[p_dynamic_air2].ToString());
                                    }
                                }
                                if (row.Cells[p_dynamic_air3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dynamic_air3].ToString(), out value))
                                    {
                                        inst.DYNAMIC_AIR3 = decimal.Parse(row.Cells[p_dynamic_air3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region EXPONENT
                            if (p_exponent1 != 0)
                            {
                                if (row.Cells[p_exponent1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_exponent1].ToString(), out value))
                                    {
                                        inst.EXPONENT1 = decimal.Parse(row.Cells[p_exponent1].ToString());
                                    }
                                }
                                if (row.Cells[p_exponent2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_exponent2].ToString(), out value))
                                    {
                                        inst.EXPONENT2 = decimal.Parse(row.Cells[p_exponent2].ToString());
                                    }
                                }
                                if (row.Cells[p_exponent3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_exponent3].ToString(), out value))
                                    {
                                        inst.EXPONENT3 = decimal.Parse(row.Cells[p_exponent3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region FLAMMABILITY
                            if (p_flammability_w != 0)
                            {
                                if (row.Cells[p_flammability_w] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_flammability_w].ToString(), out value))
                                    {
                                        inst.FLAMMABILITY_W = decimal.Parse(row.Cells[p_flammability_w].ToString());
                                    }
                                }
                                //if (row.Cells[p_flammability_w2] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w2].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W2 = decimal.Parse(row.Cells[p_flammability_w2].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_w3] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w3].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W3 = decimal.Parse(row.Cells[p_flammability_w3].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_w4] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w4].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W4 = decimal.Parse(row.Cells[p_flammability_w4].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_w5] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w5].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W5 = decimal.Parse(row.Cells[p_flammability_w5].ToString());
                                //    }
                                //}

                                if (row.Cells[p_flammability_f] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_flammability_f].ToString(), out value))
                                    {
                                        inst.FLAMMABILITY_F = decimal.Parse(row.Cells[p_flammability_f].ToString());
                                    }
                                }
                                //if (row.Cells[p_flammability_f2] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_f2].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_F2 = decimal.Parse(row.Cells[p_flammability_f2].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_f3] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_f3].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_F3 = decimal.Parse(row.Cells[p_flammability_f3].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_f4] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_f4].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_F4 = decimal.Parse(row.Cells[p_flammability_f4].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_f5] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_f5].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_F5 = decimal.Parse(row.Cells[p_flammability_f5].ToString());
                                //    }
                                //}
                            }
                            #endregion

                            #region DIMENSCHANGE
                            if (p_dimenschange_w1 != 0)
                            {
                                if (row.Cells[p_dimenschange_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_w1].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_W1 = decimal.Parse(row.Cells[p_dimenschange_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_w2].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_W2 = decimal.Parse(row.Cells[p_dimenschange_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_w3].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_W3 = decimal.Parse(row.Cells[p_dimenschange_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_f1].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_F1 = decimal.Parse(row.Cells[p_dimenschange_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_f2].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_F2 = decimal.Parse(row.Cells[p_dimenschange_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_f3].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_F3 = decimal.Parse(row.Cells[p_dimenschange_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region BOW & SKEW

                            if (p_bow1 != 0)
                            {
                                if (row.Cells[p_bow1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_bow1].ToString(), out value))
                                    {
                                        inst.BOW1 = decimal.Parse(row.Cells[p_bow1].ToString());
                                    }
                                }
                            }

                            if (p_skew1 != 0)
                            {
                                if (row.Cells[p_skew1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_skew1].ToString(), out value))
                                    {
                                        inst.SKEW1 = decimal.Parse(row.Cells[p_skew1].ToString());
                                    }
                                }
                            }

                            if (p_entryby != 0)
                            {
                                inst.ENTEYBY = row.Cells[p_entryby].ToString();
                            }

                            if (p_approveby != 0)
                            {
                                inst.APPROVEBY = row.Cells[p_approveby].ToString();
                            }

                            if (p_bow2 != 0)
                            {
                                if (row.Cells[p_bow2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_bow2].ToString(), out value))
                                    {
                                        inst.BOW2 = decimal.Parse(row.Cells[p_bow2].ToString());
                                    }
                                }
                            }

                            if (p_skew2 != 0)
                            {
                                if (row.Cells[p_skew2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_skew2].ToString(), out value))
                                    {
                                        inst.SKEW2 = decimal.Parse(row.Cells[p_skew2].ToString());
                                    }
                                }
                            }

                            if (p_bow3 != 0)
                            {
                                if (row.Cells[p_bow3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_bow3].ToString(), out value))
                                    {
                                        inst.BOW3 = decimal.Parse(row.Cells[p_bow3].ToString());
                                    }
                                }
                            }

                            if (p_skew3 != 0)
                            {
                                if (row.Cells[p_skew3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_skew3].ToString(), out value))
                                    {
                                        inst.SKEW3 = decimal.Parse(row.Cells[p_skew3].ToString());
                                    }
                                }
                            }


                            #endregion

                            results.Add(inst);
                        }
                    }

                    #endregion
                }

                return results;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();

                return results;
            }
        }

        #endregion

        #region ExcelToDataTable97
        public static List<LAB_ImportExcel1> ExcelToDataTable97(string filePath)
        {
            List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

            try
            {
                decimal value;

                HSSFWorkbook hssfworkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }

                int s = hssfworkbook.ActiveSheetIndex;

                ISheet sheet = hssfworkbook.GetSheetAt(s);
                IRow row = sheet.GetRow(0);

                string itemcode = string.Empty;
                int startRow = 6;
                int cellCount = 0;

                if (!string.IsNullOrEmpty(row.Cells[0].ToString()))
                    itemcode = row.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(row.Cells[1].ToString()))
                    itemcode = row.Cells[1].ToString();

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();

                if (!string.IsNullOrEmpty(itemcode))
                {
                    #region Def

                    int p_weavinglog = 0;
                    int p_finishinglot = 0;
                    int p_entrydate = 0;
                    int p_width = 0;
                    int p_usewidth1 = 0;
                    int p_usewidth2 = 0;
                    int p_usewidth3 = 0;
                    int p_totalweight1 = 0;
                    int p_totalweight2 = 0;
                    int p_totalweight3 = 0;
                    int p_thickness1 = 0;
                    int p_thickness2 = 0;
                    int p_thickness3 = 0;
                    int p_numthreads_w1 = 0;
                    int p_numthreads_w2 = 0;
                    int p_numthreads_w3 = 0;
                    int p_numthreads_f1 = 0;
                    int p_numthreads_f2 = 0;
                    int p_numthreads_f3 = 0;
                    int p_maxforce_w1 = 0;
                    int p_maxforce_w2 = 0;
                    int p_maxforce_w3 = 0;
                    int p_elogation_w1 = 0;
                    int p_elogation_w2 = 0;
                    int p_elogation_w3 = 0;
                    int p_maxforce_f1 = 0;
                    int p_maxforce_f2 = 0;
                    int p_maxforce_f3 = 0;
                    int p_elogation_f1 = 0;
                    int p_elogation_f2 = 0;
                    int p_elogation_f3 = 0;
                    int p_edgecomb_w1 = 0;
                    int p_edgecomb_w2 = 0;
                    int p_edgecomb_w3 = 0;
                    int p_edgecomb_f1 = 0;
                    int p_edgecomb_f2 = 0;
                    int p_edgecomb_f3 = 0;
                    int p_stiffness_w1 = 0;
                    int p_stiffness_w2 = 0;
                    int p_stiffness_w3 = 0;
                    int p_stiffness_f1 = 0;
                    int p_stiffness_f2 = 0;
                    int p_stiffness_f3 = 0;
                    int p_tear_w1 = 0;
                    int p_tear_w2 = 0;
                    int p_tear_w3 = 0;
                    int p_tear_f1 = 0;
                    int p_tear_f2 = 0;
                    int p_tear_f3 = 0;
                    int p_static_air1 = 0;
                    int p_static_air2 = 0;
                    int p_static_air3 = 0;
                    int p_dynamic_air1 = 0;
                    int p_dynamic_air2 = 0;
                    int p_dynamic_air3 = 0;

                    int p_exponent1 = 0;
                    int p_exponent2 = 0;
                    int p_exponent3 = 0;

                    int p_flammability_w = 0;
                    //int p_flammability_w2 = 0;
                    //int p_flammability_w3 = 0;
                    //int p_flammability_w4 = 0;
                    //int p_flammability_w5 = 0;

                    int p_flammability_f = 0;
                    //int p_flammability_f2 = 0;
                    //int p_flammability_f3 = 0;
                    //int p_flammability_f4 = 0;
                    //int p_flammability_f5 = 0;

                    int p_dimenschange_w1 = 0;
                    int p_dimenschange_w2 = 0;
                    int p_dimenschange_w3 = 0;
                    int p_dimenschange_f1 = 0;
                    int p_dimenschange_f2 = 0;
                    int p_dimenschange_f3 = 0;

                    int p_bow1 = 0;
                    int p_bow2 = 0;
                    int p_bow3 = 0;
                    int p_skew1 = 0;
                    int p_skew2 = 0;
                    int p_skew3 = 0;

                    int p_entryby = 0;
                    int p_approveby = 0;

                    int chkCoulmn = 0;
                    int chknull = 0;

                    #endregion

                    row = sheet.GetRow(startRow);
                    cellCount = row.Cells.Count;

                    // Set Head
                    #region Set Head

                    for (int j = 0; j < cellCount + 1; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (row.GetCell(j).CellType != CellType.Blank)
                            {
                                if (row.GetCell(j).ToString() != "")
                                {
                                    if (chkCoulmn == 1)
                                        p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 3)
                                        p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 4)
                                        p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 5)
                                        p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 6)
                                        p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 7)
                                        p_usewidth2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 8)
                                        p_usewidth3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 9)
                                        p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 10)
                                        p_totalweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 11)
                                        p_totalweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 12)
                                        p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 13)
                                        p_thickness2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 14)
                                        p_thickness3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 15)
                                        p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 16)
                                        p_numthreads_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 17)
                                        p_numthreads_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 18)
                                        p_numthreads_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 19)
                                        p_numthreads_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 20)
                                        p_numthreads_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 21)
                                        p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 22)
                                        p_maxforce_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 23)
                                        p_maxforce_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 24)
                                        p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 25)
                                        p_elogation_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 26)
                                        p_elogation_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 27)
                                        p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 28)
                                        p_maxforce_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 29)
                                        p_maxforce_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 30)
                                        p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 31)
                                        p_elogation_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 32)
                                        p_elogation_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 33)
                                        p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 34)
                                        p_edgecomb_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 35)
                                        p_edgecomb_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 36)
                                        p_edgecomb_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 37)
                                        p_edgecomb_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 38)
                                        p_edgecomb_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 39)
                                        p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 40)
                                        p_stiffness_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 41)
                                        p_stiffness_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 42)
                                        p_stiffness_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 43)
                                        p_stiffness_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 44)
                                        p_stiffness_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 45)
                                        p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 46)
                                        p_tear_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 47)
                                        p_tear_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 48)
                                        p_tear_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 49)
                                        p_tear_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 50)
                                        p_tear_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 51)
                                        p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 52)
                                        p_static_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 53)
                                        p_static_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 60)
                                        p_dynamic_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 61)
                                        p_dynamic_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 62)
                                        p_dynamic_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 63)
                                        p_exponent1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 64)
                                        p_exponent2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 65)
                                        p_exponent3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 66)
                                        p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 67)
                                        p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 68)
                                        p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 69)
                                        p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 70)
                                        p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 71)
                                        p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 72)
                                        p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 73)
                                        p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 74)
                                        p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 75)
                                        p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 83)
                                        p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 84)
                                        p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    if (chkCoulmn == 85)
                                        p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 86)
                                        p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 87)
                                        p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    if (chkCoulmn == 88)
                                        p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    #region Old

                                    //if (chkCoulmn == 66)
                                    //    p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 67)
                                    //    p_flammability_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 68)
                                    //    p_flammability_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 69)
                                    //    p_flammability_w4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 70)
                                    //    p_flammability_w5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 71)
                                    //    p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 72)
                                    //    p_flammability_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 73)
                                    //    p_flammability_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 74)
                                    //    p_flammability_f4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 75)
                                    //    p_flammability_f5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 76)
                                    //    p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 77)
                                    //    p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 78)
                                    //    p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 79)
                                    //    p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 80)
                                    //    p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 81)
                                    //    p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 82)
                                    //    p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 83)
                                    //    p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 91)
                                    //    p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 92)
                                    //    p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    //if (chkCoulmn == 93)
                                    //    p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 94)
                                    //    p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 95)
                                    //    p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                    //if (chkCoulmn == 96)
                                    //    p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                    #endregion

                                    chkCoulmn++;
                                }
                            }

                            if (p_entryby != 0)
                            {
                                if (row.GetCell(j).CellType == CellType.Blank)
                                {
                                    if (chknull >= 0 && chknull <= 3)
                                    {
                                        chknull++;
                                    }
                                    else
                                        break;
                                }
                            }

                            #region Old
                            //if (row.GetCell(j).ToString().Contains("Lot No."))
                            //{
                            //    p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //}

                            //if (row.GetCell(j).ToString().Contains("Lot no."))
                            //{
                            //    if (p_weavinglog != 0)
                            //        p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //}

                            //if (row.GetCell(j).ToString().Contains("Testing Date"))
                            //{
                            //    p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //}
                            //if (row.GetCell(j).ToString().Contains("Total Width"))
                            //{
                            //    p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //}

                            //if (row.GetCell(j).ToString().Contains("Use width"))
                            //{
                            //    p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_usewidth2 = p_usewidth1 + 1;
                            //    p_usewidth3 = p_usewidth2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Total Weight") || row.GetCell(j).ToString().Contains("Weight"))
                            //{
                            //    p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_totalweight2 = p_totalweight1 + 1;
                            //    p_totalweight3 = p_totalweight2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Thickness"))
                            //{
                            //    p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_thickness2 = p_thickness1 + 1;
                            //    p_thickness3 = p_thickness2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Density(Unit"))
                            //{
                            //    p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_numthreads_w2 = p_numthreads_w1 + 1;
                            //    p_numthreads_w3 = p_numthreads_w2 + 1;
                            //    p_numthreads_f1 = p_numthreads_w3 + 1;
                            //    p_numthreads_f2 = p_numthreads_f1 + 1;
                            //    p_numthreads_f3 = p_numthreads_f2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Tensile Strength"))
                            //{
                            //    if (chkmaxforce == 0)
                            //    {
                            //        p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //        p_maxforce_w2 = p_maxforce_w1 + 1;
                            //        p_maxforce_w3 = p_maxforce_w2 + 1;

                            //        chkmaxforce++;
                            //    }
                            //    else
                            //    {
                            //        p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //        p_maxforce_f2 = p_maxforce_f1 + 1;
                            //        p_maxforce_f3 = p_maxforce_f2 + 1;
                            //    }
                            //}
                            //if (row.GetCell(j).ToString().Contains("Elongation"))
                            //{
                            //    if (chkelogation == 0)
                            //    {
                            //        p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //        p_elogation_w2 = p_elogation_w1 + 1;
                            //        p_elogation_w3 = p_elogation_w2 + 1;

                            //        chkelogation++;
                            //    }
                            //    else
                            //    {
                            //        p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //        p_elogation_f2 = p_elogation_f1 + 1;
                            //        p_elogation_f3 = p_elogation_f2 + 1;
                            //    }
                            //}
                            //if (row.GetCell(j).ToString().Contains("Edgecom resistance"))
                            //{
                            //    p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_edgecomb_w2 = p_edgecomb_w1 + 1;
                            //    p_edgecomb_w3 = p_edgecomb_w2 + 1;
                            //    p_edgecomb_f1 = p_edgecomb_w3 + 1;
                            //    p_edgecomb_f2 = p_edgecomb_f1 + 1;
                            //    p_edgecomb_f3 = p_edgecomb_f2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Stiffness"))
                            //{
                            //    if (chkStiffness == false)
                            //    {
                            //        p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //        p_stiffness_w2 = p_stiffness_w1 + 1;
                            //        p_stiffness_w3 = p_stiffness_w2 + 1;
                            //        p_stiffness_f1 = p_stiffness_w3 + 1;
                            //        p_stiffness_f2 = p_stiffness_f1 + 1;
                            //        p_stiffness_f3 = p_stiffness_f2 + 1;

                            //        chkStiffness = true;
                            //    }
                            //}
                            //if (row.GetCell(j).ToString().Contains("Tear Strength"))
                            //{
                            //    p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_tear_w2 = p_tear_w1 + 1;
                            //    p_tear_w3 = p_tear_w2 + 1;
                            //    p_tear_f1 = p_tear_w3 + 1;
                            //    p_tear_f2 = p_tear_f1 + 1;
                            //    p_tear_f3 = p_tear_f2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Static Air Permeability"))
                            //{
                            //    p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_static_air2 = p_static_air1 + 1;
                            //    p_static_air3 = p_static_air2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("DynamicAir Permeability") || row.GetCell(j).ToString().Contains("Dynamic Air Permeability"))
                            //{
                            //    p_dynamic_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_dynamic_air2 = p_dynamic_air1 + 1;
                            //    p_dynamic_air3 = p_dynamic_air2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Eponent") || row.GetCell(j).ToString().Contains("Exponent"))
                            //{
                            //    p_exponent1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_exponent2 = p_exponent1 + 1;
                            //    p_exponent3 = p_exponent2 + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Flammability"))
                            //{
                            //    p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_flammability_f = p_flammability_w + 1;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Shrinkage"))
                            //{
                            //    p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    p_dimenschange_w2 = p_dimenschange_w1 + 1;
                            //    p_dimenschange_w3 = p_dimenschange_w2 + 1;
                            //    p_dimenschange_f1 = p_dimenschange_w3 + 1;
                            //    p_dimenschange_f2 = p_dimenschange_f1 + 1;
                            //    p_dimenschange_f3 = p_dimenschange_f2 + 1;
                            //}

                            //if (row.GetCell(j).ToString().Contains("Bow"))
                            //{
                            //    if (chkbow == 0)
                            //    {
                            //        p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    }
                            //    else if (chkbow == 1)
                            //    {
                            //        p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    }
                            //    else if (chkbow == 2)
                            //    {
                            //        p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    }

                            //    chkbow++;
                            //}
                            //if (row.GetCell(j).ToString().Contains("Bias"))
                            //{
                            //    if (chkskew == 0)
                            //    {
                            //        p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    }
                            //    else if (chkskew == 1)
                            //    {
                            //        p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    }
                            //    else if (chkskew == 2)
                            //    {
                            //        p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                            //    }

                            //    chkskew++;
                            //}

                            //if (row.GetCell(j).ToString().Contains("Tested") || row.GetCell(j).ToString().Contains("Tester") || row.GetCell(j).ToString().Contains("LAB"))
                            //{
                            //    p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString()) + 1;
                            //    p_approveby = p_entryby + 1;
                            //}

                            //if (p_entryby != 0)
                            //{
                            //    if (row.GetCell(j).ToString().Contains(""))
                            //    {
                            //        if (chknull >= 0 && chknull <= 6)
                            //        {
                            //            chknull++;
                            //        }
                            //        else
                            //            break;
                            //    }
                            //}

                            #endregion
                        }
                    }

                    #endregion

                    #region Add Detail

                    for (int i = (startRow); i <= startRow + 3; i++)
                    {
                        LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                        row = sheet.GetRow(i);

                        if (row.Cells.Count() >= chkCoulmn)
                        {
                            inst.ITM_CODE = itemcode;

                            #region WEAVINGLOT
                            if (row.Cells[p_weavinglog] == null)
                            {
                                break;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                {
                                    break;
                                }
                                else
                                {
                                    if (p_weavinglog != 0)
                                        inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();

                                    if (inst.WEAVINGLOT.Count() < 9)
                                        break;
                                }
                            }
                            #endregion

                            if (p_finishinglot != 0)
                                inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                            if (p_entrydate != 0)
                            {
                                if (row.Cells[p_entrydate] != null)
                                    inst.ENTRYDATE = DateTime.Parse(row.Cells[p_entrydate].ToString());
                            }

                            #region WIDTH
                            if (p_width != 0)
                            {
                                if (row.Cells[p_width] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_width].ToString(), out value))
                                    {
                                        inst.WIDTH = decimal.Parse(row.Cells[p_width].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region USABLE_WIDTH
                            if (p_usewidth1 != 0)
                            {
                                if (row.Cells[p_usewidth1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_usewidth1].ToString(), out value))
                                    {
                                        inst.USABLE_WIDTH1 = decimal.Parse(row.Cells[p_usewidth1].ToString());
                                    }
                                }
                                if (row.Cells[p_usewidth2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_usewidth2].ToString(), out value))
                                    {
                                        inst.USABLE_WIDTH2 = decimal.Parse(row.Cells[p_usewidth2].ToString());
                                    }
                                }
                                if (row.Cells[p_usewidth3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_usewidth3].ToString(), out value))
                                    {
                                        inst.USABLE_WIDTH3 = decimal.Parse(row.Cells[p_usewidth3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region TOTALWEIGHT
                            if (p_totalweight1 != 0)
                            {
                                if (row.Cells[p_totalweight1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_totalweight1].ToString(), out value))
                                    {
                                        inst.TOTALWEIGHT1 = decimal.Parse(row.Cells[p_totalweight1].ToString());
                                    }
                                }
                                if (row.Cells[p_totalweight2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_totalweight2].ToString(), out value))
                                    {
                                        inst.TOTALWEIGHT2 = decimal.Parse(row.Cells[p_totalweight2].ToString());
                                    }
                                }
                                if (row.Cells[p_totalweight3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_totalweight3].ToString(), out value))
                                    {
                                        inst.TOTALWEIGHT3 = decimal.Parse(row.Cells[p_totalweight3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region THICKNESS
                            if (p_thickness1 != 0)
                            {
                                if (row.Cells[p_thickness1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_thickness1].ToString(), out value))
                                    {
                                        inst.THICKNESS1 = decimal.Parse(row.Cells[p_thickness1].ToString());
                                    }
                                }
                                if (row.Cells[p_thickness2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_thickness2].ToString(), out value))
                                    {
                                        inst.THICKNESS2 = decimal.Parse(row.Cells[p_thickness2].ToString());
                                    }
                                }
                                if (row.Cells[p_thickness3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_thickness3].ToString(), out value))
                                    {
                                        inst.THICKNESS3 = decimal.Parse(row.Cells[p_thickness3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region NUMTHREADS
                            if (p_numthreads_w1 != 0)
                            {
                                if (row.Cells[p_numthreads_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_w1].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_W1 = decimal.Parse(row.Cells[p_numthreads_w1].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_w2].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_W2 = decimal.Parse(row.Cells[p_numthreads_w2].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_w3].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_W3 = decimal.Parse(row.Cells[p_numthreads_w3].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_f1].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_F1 = decimal.Parse(row.Cells[p_numthreads_f1].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_f2].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_F2 = decimal.Parse(row.Cells[p_numthreads_f2].ToString()) * (10);
                                    }
                                }
                                if (row.Cells[p_numthreads_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_numthreads_f3].ToString(), out value))
                                    {
                                        inst.NUMTHREADS_F3 = decimal.Parse(row.Cells[p_numthreads_f3].ToString()) * (10);
                                    }
                                }
                            }
                            #endregion

                            #region MAXFORCE
                            if (p_maxforce_w1 != 0)
                            {
                                if (row.Cells[p_maxforce_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_w1].ToString(), out value))
                                    {
                                        inst.MAXFORCE_W1 = decimal.Parse(row.Cells[p_maxforce_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_w2].ToString(), out value))
                                    {
                                        inst.MAXFORCE_W2 = decimal.Parse(row.Cells[p_maxforce_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_w3].ToString(), out value))
                                    {
                                        inst.MAXFORCE_W3 = decimal.Parse(row.Cells[p_maxforce_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_f1].ToString(), out value))
                                    {
                                        inst.MAXFORCE_F1 = decimal.Parse(row.Cells[p_maxforce_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_f2].ToString(), out value))
                                    {
                                        inst.MAXFORCE_F2 = decimal.Parse(row.Cells[p_maxforce_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_maxforce_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_maxforce_f3].ToString(), out value))
                                    {
                                        inst.MAXFORCE_F3 = decimal.Parse(row.Cells[p_maxforce_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region ELONGATIONFORCE
                            if (p_elogation_w1 != 0)
                            {
                                if (row.Cells[p_elogation_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_w1].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_W1 = decimal.Parse(row.Cells[p_elogation_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_w2].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_W2 = decimal.Parse(row.Cells[p_elogation_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_w3].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_W3 = decimal.Parse(row.Cells[p_elogation_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_f1].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_F1 = decimal.Parse(row.Cells[p_elogation_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_f2].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_F2 = decimal.Parse(row.Cells[p_elogation_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_elogation_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_elogation_f3].ToString(), out value))
                                    {
                                        inst.ELONGATIONFORCE_F3 = decimal.Parse(row.Cells[p_elogation_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region EDGECOMB
                            if (p_edgecomb_w1 != 0)
                            {
                                if (row.Cells[p_edgecomb_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_w1].ToString(), out value))
                                    {
                                        inst.EDGECOMB_W1 = decimal.Parse(row.Cells[p_edgecomb_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_w2].ToString(), out value))
                                    {
                                        inst.EDGECOMB_W2 = decimal.Parse(row.Cells[p_edgecomb_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_w3].ToString(), out value))
                                    {
                                        inst.EDGECOMB_W3 = decimal.Parse(row.Cells[p_edgecomb_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_f1].ToString(), out value))
                                    {
                                        inst.EDGECOMB_F1 = decimal.Parse(row.Cells[p_edgecomb_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_f2].ToString(), out value))
                                    {
                                        inst.EDGECOMB_F2 = decimal.Parse(row.Cells[p_edgecomb_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_edgecomb_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_edgecomb_f3].ToString(), out value))
                                    {
                                        inst.EDGECOMB_F3 = decimal.Parse(row.Cells[p_edgecomb_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region STIFFNESS
                            if (p_stiffness_w1 != 0)
                            {
                                if (row.Cells[p_stiffness_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_w1].ToString(), out value))
                                    {
                                        inst.STIFFNESS_W1 = decimal.Parse(row.Cells[p_stiffness_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_w2].ToString(), out value))
                                    {
                                        inst.STIFFNESS_W2 = decimal.Parse(row.Cells[p_stiffness_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_w3].ToString(), out value))
                                    {
                                        inst.STIFFNESS_W3 = decimal.Parse(row.Cells[p_stiffness_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_f1].ToString(), out value))
                                    {
                                        inst.STIFFNESS_F1 = decimal.Parse(row.Cells[p_stiffness_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_f2].ToString(), out value))
                                    {
                                        inst.STIFFNESS_F2 = decimal.Parse(row.Cells[p_stiffness_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_stiffness_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_stiffness_f3].ToString(), out value))
                                    {
                                        inst.STIFFNESS_F3 = decimal.Parse(row.Cells[p_stiffness_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region TEAR
                            if (p_tear_w1 != 0)
                            {
                                if (row.Cells[p_tear_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_w1].ToString(), out value))
                                    {
                                        inst.TEAR_W1 = decimal.Parse(row.Cells[p_tear_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_w2].ToString(), out value))
                                    {
                                        inst.TEAR_W2 = decimal.Parse(row.Cells[p_tear_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_w3].ToString(), out value))
                                    {
                                        inst.TEAR_W3 = decimal.Parse(row.Cells[p_tear_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_f1].ToString(), out value))
                                    {
                                        inst.TEAR_F1 = decimal.Parse(row.Cells[p_tear_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_f2].ToString(), out value))
                                    {
                                        inst.TEAR_F2 = decimal.Parse(row.Cells[p_tear_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_tear_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_tear_f3].ToString(), out value))
                                    {
                                        inst.TEAR_F3 = decimal.Parse(row.Cells[p_tear_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region STATIC_AIR
                            if (p_static_air1 != 0)
                            {
                                if (row.Cells[p_static_air1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_static_air1].ToString(), out value))
                                    {
                                        inst.STATIC_AIR1 = decimal.Parse(row.Cells[p_static_air1].ToString());
                                    }
                                    else
                                    {
                                        if (evaluator.Evaluate(row.Cells[p_static_air1]) != null)
                                            inst.STATIC_AIR1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air1]).NumberValue.ToString());
                                    }

                                }
                                if (row.Cells[p_static_air2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_static_air2].ToString(), out value))
                                    {
                                        inst.STATIC_AIR2 = decimal.Parse(row.Cells[p_static_air2].ToString());
                                    }
                                    else
                                    {
                                        if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                            inst.STATIC_AIR2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air2]).NumberValue.ToString());
                                    }
                                }
                                if (row.Cells[p_static_air3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_static_air3].ToString(), out value))
                                    {
                                        inst.STATIC_AIR3 = decimal.Parse(row.Cells[p_static_air3].ToString());
                                    }
                                    else
                                    {
                                        if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                            inst.STATIC_AIR3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air3]).NumberValue.ToString());
                                    }
                                }
                            }
                            #endregion

                            #region DYNAMIC_AIR
                            if (p_dynamic_air1 != 0)
                            {
                                if (row.Cells[p_dynamic_air1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dynamic_air1].ToString(), out value))
                                    {
                                        inst.DYNAMIC_AIR1 = decimal.Parse(row.Cells[p_dynamic_air1].ToString());
                                    }
                                }
                                if (row.Cells[p_dynamic_air2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dynamic_air2].ToString(), out value))
                                    {
                                        inst.DYNAMIC_AIR2 = decimal.Parse(row.Cells[p_dynamic_air2].ToString());
                                    }
                                }
                                if (row.Cells[p_dynamic_air3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dynamic_air3].ToString(), out value))
                                    {
                                        inst.DYNAMIC_AIR3 = decimal.Parse(row.Cells[p_dynamic_air3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region EXPONENT
                            if (p_exponent1 != 0)
                            {
                                if (row.Cells[p_exponent1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_exponent1].ToString(), out value))
                                    {
                                        inst.EXPONENT1 = decimal.Parse(row.Cells[p_exponent1].ToString());
                                    }
                                }
                                if (row.Cells[p_exponent2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_exponent2].ToString(), out value))
                                    {
                                        inst.EXPONENT2 = decimal.Parse(row.Cells[p_exponent2].ToString());
                                    }
                                }
                                if (row.Cells[p_exponent3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_exponent3].ToString(), out value))
                                    {
                                        inst.EXPONENT3 = decimal.Parse(row.Cells[p_exponent3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region FLAMMABILITY
                            if (p_flammability_w != 0)
                            {
                                if (row.Cells[p_flammability_w] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_flammability_w].ToString(), out value))
                                    {
                                        inst.FLAMMABILITY_W = decimal.Parse(row.Cells[p_flammability_w].ToString());
                                    }
                                }
                                //if (row.Cells[p_flammability_w2] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w2].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W2 = decimal.Parse(row.Cells[p_flammability_w2].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_w3] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w3].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W3 = decimal.Parse(row.Cells[p_flammability_w3].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_w4] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w4].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W4 = decimal.Parse(row.Cells[p_flammability_w4].ToString());
                                //    }
                                //}
                                //if (row.Cells[p_flammability_w5] != null)
                                //{
                                //    if (Decimal.TryParse(row.Cells[p_flammability_w5].ToString(), out value))
                                //    {
                                //        inst.FLAMMABILITY_W5 = decimal.Parse(row.Cells[p_flammability_w5].ToString());
                                //    }
                                //}

                                if (row.Cells[p_flammability_f] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_flammability_f].ToString(), out value))
                                    {
                                        inst.FLAMMABILITY_F = decimal.Parse(row.Cells[p_flammability_f].ToString());
                                    }
                                }
                            //    if (row.Cells[p_flammability_f2] != null)
                            //    {
                            //        if (Decimal.TryParse(row.Cells[p_flammability_f2].ToString(), out value))
                            //        {
                            //            inst.FLAMMABILITY_F2 = decimal.Parse(row.Cells[p_flammability_f2].ToString());
                            //        }
                            //    }
                            //    if (row.Cells[p_flammability_f3] != null)
                            //    {
                            //        if (Decimal.TryParse(row.Cells[p_flammability_f3].ToString(), out value))
                            //        {
                            //            inst.FLAMMABILITY_F3 = decimal.Parse(row.Cells[p_flammability_f3].ToString());
                            //        }
                            //    }
                            //    if (row.Cells[p_flammability_f4] != null)
                            //    {
                            //        if (Decimal.TryParse(row.Cells[p_flammability_f4].ToString(), out value))
                            //        {
                            //            inst.FLAMMABILITY_F4 = decimal.Parse(row.Cells[p_flammability_f4].ToString());
                            //        }
                            //    }
                            //    if (row.Cells[p_flammability_f5] != null)
                            //    {
                            //        if (Decimal.TryParse(row.Cells[p_flammability_f5].ToString(), out value))
                            //        {
                            //            inst.FLAMMABILITY_F5 = decimal.Parse(row.Cells[p_flammability_f5].ToString());
                            //        }
                            //    }
                            }
                            #endregion

                            #region DIMENSCHANGE
                            if (p_dimenschange_w1 != 0)
                            {
                                if (row.Cells[p_dimenschange_w1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_w1].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_W1 = decimal.Parse(row.Cells[p_dimenschange_w1].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_w2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_w2].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_W2 = decimal.Parse(row.Cells[p_dimenschange_w2].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_w3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_w3].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_W3 = decimal.Parse(row.Cells[p_dimenschange_w3].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_f1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_f1].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_F1 = decimal.Parse(row.Cells[p_dimenschange_f1].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_f2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_f2].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_F2 = decimal.Parse(row.Cells[p_dimenschange_f2].ToString());
                                    }
                                }
                                if (row.Cells[p_dimenschange_f3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_dimenschange_f3].ToString(), out value))
                                    {
                                        inst.DIMENSCHANGE_F3 = decimal.Parse(row.Cells[p_dimenschange_f3].ToString());
                                    }
                                }
                            }
                            #endregion

                            #region BOW & SKEW

                            if (p_bow1 != 0)
                            {
                                if (row.Cells[p_bow1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_bow1].ToString(), out value))
                                    {
                                        inst.BOW1 = decimal.Parse(row.Cells[p_bow1].ToString());
                                    }
                                }
                            }

                            if (p_skew1 != 0)
                            {
                                if (row.Cells[p_skew1] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_skew1].ToString(), out value))
                                    {
                                        inst.SKEW1 = decimal.Parse(row.Cells[p_skew1].ToString());
                                    }
                                }
                            }

                            if (p_entryby != 0)
                            {
                                inst.ENTEYBY = row.Cells[p_entryby].ToString();
                            }

                            if (p_approveby != 0)
                            {
                                inst.APPROVEBY = row.Cells[p_approveby].ToString();
                            }

                            if (p_bow2 != 0)
                            {
                                if (row.Cells[p_bow2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_bow2].ToString(), out value))
                                    {
                                        inst.BOW2 = decimal.Parse(row.Cells[p_bow2].ToString());
                                    }
                                }
                            }

                            if (p_skew2 != 0)
                            {
                                if (row.Cells[p_skew2] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_skew2].ToString(), out value))
                                    {
                                        inst.SKEW2 = decimal.Parse(row.Cells[p_skew2].ToString());
                                    }
                                }
                            }

                            if (p_bow3 != 0)
                            {
                                if (row.Cells[p_bow3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_bow3].ToString(), out value))
                                    {
                                        inst.BOW3 = decimal.Parse(row.Cells[p_bow3].ToString());
                                    }
                                }
                            }

                            if (p_skew3 != 0)
                            {
                                if (row.Cells[p_skew3] != null)
                                {
                                    if (Decimal.TryParse(row.Cells[p_skew3].ToString(), out value))
                                    {
                                        inst.SKEW3 = decimal.Parse(row.Cells[p_skew3].ToString());
                                    }
                                }
                            }


                            #endregion

                            results.Add(inst);
                        }
                    }

                    #endregion
                }

                return results;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();

                return results;
            }

        }
        #endregion
        #endregion

        #region Save

        #region OpenExcel
        private void OpenExcel()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
                openFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string csv = string.Empty;
                csv = "Excel 97-2003 file (*.xls)|*.xls|Excel file (*.xlsx)|*.xlsx";

                openFileDialog1.Filter = csv;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Multiselect = true;

                Nullable<bool> result = openFileDialog1.ShowDialog();

                if (result == true)
                {
                    List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

                    listView.ItemsSource = null;
                    listView.Items.Clear();

                    string newFileName = string.Empty;
                    //int RowNo = 1;

                    foreach (String file in openFileDialog1.FileNames)
                    {
                        newFileName = file;

                        if (!string.IsNullOrEmpty(newFileName))
                        {
                            try
                            {
                                results = new List<LAB_ImportExcel1>();

                                if (newFileName.Contains(".xlsx") == true)
                                {
                                    results = ExcelToLAB_ImportExcel1(newFileName);
                                }
                                else
                                {
                                    results = ExcelToLAB_ImportExcel197(newFileName);
                                }

                                #region Approve

                                if (results != null)
                                {
                                    if (results.Count > 0)
                                    {
                                        int RowNo2 = 0;
                                        int RowNoErr = 0;
                                        string Err = string.Empty;
                                        bool chkApprove = true;

                                        #region ตัวแปร

                                        string P_ITMCODE = string.Empty;
                                        string P_WEAVINGLOG = string.Empty;
                                        string P_FINISHINGLOT = string.Empty;
                                        DateTime? P_ENTRYDATE = null;
                                        string P_ENTRYBY = string.Empty;
                                        decimal? P_WIDTH = null;
                                        decimal? P_USEWIDTH1 = null;
                                        decimal? P_USEWIDTH2 = null;
                                        decimal? P_USEWIDTH3 = null;
                                        decimal? P_WIDTHSILICONE1 = null;
                                        decimal? P_WIDTHSILICONE2 = null;
                                        decimal? P_WIDTHSILICONE3 = null;
                                        decimal? P_NUMTHREADS_W1 = null;
                                        decimal? P_NUMTHREADS_W2 = null;
                                        decimal? P_NUMTHREADS_W3 = null;
                                        decimal? P_NUMTHREADS_F1 = null;
                                        decimal? P_NUMTHREADS_F2 = null;
                                        decimal? P_NUMTHREADS_F3 = null;
                                        decimal? P_TOTALWEIGHT1 = null;
                                        decimal? P_TOTALWEIGHT2 = null;
                                        decimal? P_TOTALWEIGHT3 = null;
                                        decimal? P_TOTALWEIGHT4 = null;
                                        decimal? P_TOTALWEIGHT5 = null;
                                        decimal? P_TOTALWEIGHT6 = null;
                                        decimal? P_UNCOATEDWEIGHT1 = null;
                                        decimal? P_UNCOATEDWEIGHT2 = null;
                                        decimal? P_UNCOATEDWEIGHT3 = null;
                                        decimal? P_UNCOATEDWEIGHT4 = null;
                                        decimal? P_UNCOATEDWEIGHT5 = null;
                                        decimal? P_UNCOATEDWEIGHT6 = null;
                                        decimal? P_COATWEIGHT1 = null;
                                        decimal? P_COATWEIGHT2 = null;
                                        decimal? P_COATWEIGHT3 = null;
                                        decimal? P_COATWEIGHT4 = null;
                                        decimal? P_COATWEIGHT5 = null;
                                        decimal? P_COATWEIGHT6 = null;
                                        decimal? P_THICKNESS1 = null;
                                        decimal? P_THICKNESS2 = null;
                                        decimal? P_THICKNESS3 = null;

                                        decimal? P_MAXFORCE_W1 = null;
                                        decimal? P_MAXFORCE_W2 = null;
                                        decimal? P_MAXFORCE_W3 = null;

                                        decimal? P_MAXFORCE_W4 = null;
                                        decimal? P_MAXFORCE_W5 = null;
                                        decimal? P_MAXFORCE_W6 = null;

                                        decimal? P_MAXFORCE_F1 = null;
                                        decimal? P_MAXFORCE_F2 = null;
                                        decimal? P_MAXFORCE_F3 = null;

                                        decimal? P_MAXFORCE_F4 = null;
                                        decimal? P_MAXFORCE_F5 = null;
                                        decimal? P_MAXFORCE_F6 = null;

                                        decimal? P_ELOGATION_W1 = null;
                                        decimal? P_ELOGATION_W2 = null;
                                        decimal? P_ELOGATION_W3 = null;

                                        decimal? P_ELOGATION_W4 = null;
                                        decimal? P_ELOGATION_W5 = null;
                                        decimal? P_ELOGATION_W6 = null;

                                        decimal? P_ELOGATION_F1 = null;
                                        decimal? P_ELOGATION_F2 = null;
                                        decimal? P_ELOGATION_F3 = null;

                                        decimal? P_ELOGATION_F4 = null;
                                        decimal? P_ELOGATION_F5 = null;
                                        decimal? P_ELOGATION_F6 = null;

                                        decimal? P_FLAMMABILITY_W = null;
                                        decimal? P_FLAMMABILITY_W2 = null;
                                        decimal? P_FLAMMABILITY_W3 = null;
                                        decimal? P_FLAMMABILITY_W4 = null;
                                        decimal? P_FLAMMABILITY_W5 = null;

                                        decimal? P_FLAMMABILITY_F = null;
                                        decimal? P_FLAMMABILITY_F2 = null;
                                        decimal? P_FLAMMABILITY_F3 = null;
                                        decimal? P_FLAMMABILITY_F4 = null;
                                        decimal? P_FLAMMABILITY_F5 = null;

                                        decimal? P_EDGECOMB_W1 = null;
                                        decimal? P_EDGECOMB_W2 = null;
                                        decimal? P_EDGECOMB_W3 = null;
                                        decimal? P_EDGECOMB_F1 = null;
                                        decimal? P_EDGECOMB_F2 = null;
                                        decimal? P_EDGECOMB_F3 = null;
                                        decimal? P_STIFFNESS_W1 = null;
                                        decimal? P_STIFFNESS_W2 = null;
                                        decimal? P_STIFFNESS_W3 = null;
                                        decimal? P_STIFFNESS_F1 = null;
                                        decimal? P_STIFFNESS_F2 = null;
                                        decimal? P_STIFFNESS_F3 = null;
                                        decimal? P_TEAR_W1 = null;
                                        decimal? P_TEAR_W2 = null;
                                        decimal? P_TEAR_W3 = null;
                                        decimal? P_TEAR_F1 = null;
                                        decimal? P_TEAR_F2 = null;
                                        decimal? P_TEAR_F3 = null;
                                        decimal? P_STATIC_AIR1 = null;
                                        decimal? P_STATIC_AIR2 = null;
                                        decimal? P_STATIC_AIR3 = null;

                                        decimal? P_STATIC_AIR4 = null;
                                        decimal? P_STATIC_AIR5 = null;
                                        decimal? P_STATIC_AIR6 = null;

                                        decimal? P_DYNAMIC_AIR1 = null;
                                        decimal? P_DYNAMIC_AIR2 = null;
                                        decimal? P_DYNAMIC_AIR3 = null;
                                        decimal? P_EXPONENT1 = null;
                                        decimal? P_EXPONENT2 = null;
                                        decimal? P_EXPONENT3 = null;
                                        decimal? P_DIMENSCHANGE_W1 = null;
                                        decimal? P_DIMENSCHANGE_W2 = null;
                                        decimal? P_DIMENSCHANGE_W3 = null;
                                        decimal? P_DIMENSCHANGE_F1 = null;
                                        decimal? P_DIMENSCHANGE_F2 = null;
                                        decimal? P_DIMENSCHANGE_F3 = null;
                                        decimal? P_FLEXABRASION_W1 = null;
                                        decimal? P_FLEXABRASION_W2 = null;
                                        decimal? P_FLEXABRASION_W3 = null;
                                        decimal? P_FLEXABRASION_F1 = null;
                                        decimal? P_FLEXABRASION_F2 = null;
                                        decimal? P_FLEXABRASION_F3 = null;

                                        string P_STATUS = string.Empty;
                                        string P_REMARK = string.Empty;
                                        string P_APPROVEBY = string.Empty;
                                        DateTime? P_APPROVEDATE = null;

                                        decimal? P_BOW1 = null;
                                        decimal? P_BOW2 = null;
                                        decimal? P_BOW3 = null;
                                        decimal? P_SKEW1 = null;
                                        decimal? P_SKEW2 = null;
                                        decimal? P_SKEW3 = null;
                                        decimal? P_BENDING_W1 = null;
                                        decimal? P_BENDING_W2 = null;
                                        decimal? P_BENDING_W3 = null;
                                        decimal? P_BENDING_F1 = null;
                                        decimal? P_BENDING_F2 = null;
                                        decimal? P_BENDING_F3 = null;
                                        decimal? P_FLEX_SCOTT_W1 = null;
                                        decimal? P_FLEX_SCOTT_W2 = null;
                                        decimal? P_FLEX_SCOTT_W3 = null;
                                        decimal? P_FLEX_SCOTT_F1 = null;
                                        decimal? P_FLEX_SCOTT_F2 = null;
                                        decimal? P_FLEX_SCOTT_F3 = null;

                                        #endregion

                                        //if (!string.IsNullOrEmpty(txtOperator.Text))
                                        //{
                                        //    P_APPROVEBY = txtOperator.Text;
                                        //}

                                        P_STATUS = "Approve";
                                        P_APPROVEDATE = DateTime.Now;

                                        for (int i = 0; i <= results.Count - 1; i++)
                                        {
                                            chkApprove = true;

                                            #region ตัวแปร

                                            P_ITMCODE = string.Empty;
                                            P_WEAVINGLOG = string.Empty;
                                            P_FINISHINGLOT = string.Empty;
                                            P_ENTRYDATE = null;
                                            P_ENTRYBY = string.Empty;
                                            P_WIDTH = null;
                                            P_USEWIDTH1 = null;
                                            P_USEWIDTH2 = null;
                                            P_USEWIDTH3 = null;
                                            P_WIDTHSILICONE1 = null;
                                            P_WIDTHSILICONE2 = null;
                                            P_WIDTHSILICONE3 = null;
                                            P_NUMTHREADS_W1 = null;
                                            P_NUMTHREADS_W2 = null;
                                            P_NUMTHREADS_W3 = null;
                                            P_NUMTHREADS_F1 = null;
                                            P_NUMTHREADS_F2 = null;
                                            P_NUMTHREADS_F3 = null;
                                            P_TOTALWEIGHT1 = null;
                                            P_TOTALWEIGHT2 = null;
                                            P_TOTALWEIGHT3 = null;
                                            P_TOTALWEIGHT4 = null;
                                            P_TOTALWEIGHT5 = null;
                                            P_TOTALWEIGHT6 = null;
                                            P_UNCOATEDWEIGHT1 = null;
                                            P_UNCOATEDWEIGHT2 = null;
                                            P_UNCOATEDWEIGHT3 = null;
                                            P_UNCOATEDWEIGHT4 = null;
                                            P_UNCOATEDWEIGHT5 = null;
                                            P_UNCOATEDWEIGHT6 = null;
                                            P_COATWEIGHT1 = null;
                                            P_COATWEIGHT2 = null;
                                            P_COATWEIGHT3 = null;
                                            P_COATWEIGHT4 = null;
                                            P_COATWEIGHT5 = null;
                                            P_COATWEIGHT6 = null;
                                            P_THICKNESS1 = null;
                                            P_THICKNESS2 = null;
                                            P_THICKNESS3 = null;
                                            P_MAXFORCE_W1 = null;
                                            P_MAXFORCE_W2 = null;
                                            P_MAXFORCE_W3 = null;
                                            P_MAXFORCE_F1 = null;
                                            P_MAXFORCE_F2 = null;
                                            P_MAXFORCE_F3 = null;
                                            P_ELOGATION_W1 = null;
                                            P_ELOGATION_W2 = null;
                                            P_ELOGATION_W3 = null;
                                            P_ELOGATION_F1 = null;
                                            P_ELOGATION_F2 = null;
                                            P_ELOGATION_F3 = null;

                                            P_FLAMMABILITY_W = null;
                                            P_FLAMMABILITY_W2 = null;
                                            P_FLAMMABILITY_W3 = null;
                                            P_FLAMMABILITY_W4 = null;
                                            P_FLAMMABILITY_W5 = null;

                                            P_FLAMMABILITY_F = null;
                                            P_FLAMMABILITY_F2 = null;
                                            P_FLAMMABILITY_F3 = null;
                                            P_FLAMMABILITY_F4 = null;
                                            P_FLAMMABILITY_F5 = null;

                                            P_EDGECOMB_W1 = null;
                                            P_EDGECOMB_W2 = null;
                                            P_EDGECOMB_W3 = null;
                                            P_EDGECOMB_F1 = null;
                                            P_EDGECOMB_F2 = null;
                                            P_EDGECOMB_F3 = null;
                                            P_STIFFNESS_W1 = null;
                                            P_STIFFNESS_W2 = null;
                                            P_STIFFNESS_W3 = null;
                                            P_STIFFNESS_F1 = null;
                                            P_STIFFNESS_F2 = null;
                                            P_STIFFNESS_F3 = null;
                                            P_TEAR_W1 = null;
                                            P_TEAR_W2 = null;
                                            P_TEAR_W3 = null;
                                            P_TEAR_F1 = null;
                                            P_TEAR_F2 = null;
                                            P_TEAR_F3 = null;
                                            P_STATIC_AIR1 = null;
                                            P_STATIC_AIR2 = null;
                                            P_STATIC_AIR3 = null;

                                            P_STATIC_AIR4 = null;
                                            P_STATIC_AIR5 = null;
                                            P_STATIC_AIR6 = null;

                                            P_DYNAMIC_AIR1 = null;
                                            P_DYNAMIC_AIR2 = null;
                                            P_DYNAMIC_AIR3 = null;
                                            P_EXPONENT1 = null;
                                            P_EXPONENT2 = null;
                                            P_EXPONENT3 = null;
                                            P_DIMENSCHANGE_W1 = null;
                                            P_DIMENSCHANGE_W2 = null;
                                            P_DIMENSCHANGE_W3 = null;
                                            P_DIMENSCHANGE_F1 = null;
                                            P_DIMENSCHANGE_F2 = null;
                                            P_DIMENSCHANGE_F3 = null;
                                            P_FLEXABRASION_W1 = null;
                                            P_FLEXABRASION_W2 = null;
                                            P_FLEXABRASION_W3 = null;
                                            P_FLEXABRASION_F1 = null;
                                            P_FLEXABRASION_F2 = null;
                                            P_FLEXABRASION_F3 = null;

                                            P_REMARK = string.Empty;
                                            P_APPROVEBY = string.Empty;

                                            P_BOW1 = null;
                                            P_BOW2 = null;
                                            P_BOW3 = null;
                                            P_SKEW1 = null;
                                            P_SKEW2 = null;
                                            P_SKEW3 = null;
                                            P_BENDING_W1 = null;
                                            P_BENDING_W2 = null;
                                            P_BENDING_W3 = null;
                                            P_BENDING_F1 = null;
                                            P_BENDING_F2 = null;
                                            P_BENDING_F3 = null;
                                            P_FLEX_SCOTT_W1 = null;
                                            P_FLEX_SCOTT_W2 = null;
                                            P_FLEX_SCOTT_W3 = null;
                                            P_FLEX_SCOTT_F1 = null;
                                            P_FLEX_SCOTT_F2 = null;
                                            P_FLEX_SCOTT_F3 = null;

                                            #endregion

                                            P_ITMCODE = results[i].ITM_CODE;
                                            P_WEAVINGLOG = results[i].WEAVINGLOT;
                                            P_FINISHINGLOT = results[i].FINISHINGLOT;

                                            #region MAXFORCE
                                            P_MAXFORCE_W1 = results[i].MAXFORCE_W1;
                                            P_MAXFORCE_W2 = results[i].MAXFORCE_W2;
                                            P_MAXFORCE_W3 = results[i].MAXFORCE_W3;

                                            P_MAXFORCE_W4 = results[i].MAXFORCE_W4;
                                            P_MAXFORCE_W5 = results[i].MAXFORCE_W5;
                                            P_MAXFORCE_W6 = results[i].MAXFORCE_W6;

                                            P_MAXFORCE_F1 = results[i].MAXFORCE_F1;
                                            P_MAXFORCE_F2 = results[i].MAXFORCE_F2;
                                            P_MAXFORCE_F3 = results[i].MAXFORCE_F3;

                                            P_MAXFORCE_F4 = results[i].MAXFORCE_F4;
                                            P_MAXFORCE_F5 = results[i].MAXFORCE_F5;
                                            P_MAXFORCE_F6 = results[i].MAXFORCE_F6;
                                            #endregion

                                            #region ELOGATION
                                            P_ELOGATION_W1 = results[i].ELONGATIONFORCE_W1;
                                            P_ELOGATION_W2 = results[i].ELONGATIONFORCE_W2;
                                            P_ELOGATION_W3 = results[i].ELONGATIONFORCE_W3;

                                            P_ELOGATION_W4 = results[i].ELONGATIONFORCE_W4;
                                            P_ELOGATION_W5 = results[i].ELONGATIONFORCE_W5;
                                            P_ELOGATION_W6 = results[i].ELONGATIONFORCE_W6;

                                            P_ELOGATION_F1 = results[i].ELONGATIONFORCE_F1;
                                            P_ELOGATION_F2 = results[i].ELONGATIONFORCE_F2;
                                            P_ELOGATION_F3 = results[i].ELONGATIONFORCE_F3;

                                            P_ELOGATION_F4 = results[i].ELONGATIONFORCE_F4;
                                            P_ELOGATION_F5 = results[i].ELONGATIONFORCE_F5;
                                            P_ELOGATION_F6 = results[i].ELONGATIONFORCE_F6;

                                            #endregion

                                            #region EDGECOMB
                                            P_EDGECOMB_W1 = results[i].EDGECOMB_W1;
                                            P_EDGECOMB_W2 = results[i].EDGECOMB_W2;
                                            P_EDGECOMB_W3 = results[i].EDGECOMB_W3;
                                            P_EDGECOMB_F1 = results[i].EDGECOMB_F1;
                                            P_EDGECOMB_F2 = results[i].EDGECOMB_F2;
                                            P_EDGECOMB_F3 = results[i].EDGECOMB_F3;
                                            #endregion

                                            #region TEAR
                                            P_TEAR_W1 = results[i].TEAR_W1;
                                            P_TEAR_W2 = results[i].TEAR_W2;
                                            P_TEAR_W3 = results[i].TEAR_W3;
                                            P_TEAR_F1 = results[i].TEAR_F1;
                                            P_TEAR_F2 = results[i].TEAR_F2;
                                            P_TEAR_F3 = results[i].TEAR_F3;
                                            #endregion

                                            P_WIDTH = results[i].WIDTH;

                                            #region USEWIDTH
                                            P_USEWIDTH1 = results[i].USABLE_WIDTH1;
                                            P_USEWIDTH2 = results[i].USABLE_WIDTH2;
                                            P_USEWIDTH3 = results[i].USABLE_WIDTH3;
                                            #endregion

                                            #region WIDTHSILICONE
                                            //P_WIDTHSILICONE1 = results[i].WIDTHSILICONE1;
                                            //P_WIDTHSILICONE2 = results[i].WIDTHSILICONE2;
                                            //P_WIDTHSILICONE3 = results[i].WIDTHSILICONE3;
                                            #endregion

                                            #region NUMTHREADS
                                            P_NUMTHREADS_W1 = results[i].NUMTHREADS_W1;
                                            P_NUMTHREADS_W2 = results[i].NUMTHREADS_W2;
                                            P_NUMTHREADS_W3 = results[i].NUMTHREADS_W3;
                                            P_NUMTHREADS_F1 = results[i].NUMTHREADS_F1;
                                            P_NUMTHREADS_F2 = results[i].NUMTHREADS_F2;
                                            P_NUMTHREADS_F3 = results[i].NUMTHREADS_F3;
                                            #endregion

                                            #region TOTALWEIGHT
                                            P_TOTALWEIGHT1 = results[i].TOTALWEIGHT1;
                                            P_TOTALWEIGHT2 = results[i].TOTALWEIGHT2;
                                            P_TOTALWEIGHT3 = results[i].TOTALWEIGHT3;
                                            //P_TOTALWEIGHT4 = results[i].TOTALWEIGHT4;
                                            //P_TOTALWEIGHT5 = results[i].TOTALWEIGHT5;
                                            //P_TOTALWEIGHT6 = results[i].TOTALWEIGHT6;
                                            #endregion

                                            #region UNCOATEDWEIGHT
                                            //P_UNCOATEDWEIGHT1 = results[i].UNCOATEDWEIGHT1;
                                            //P_UNCOATEDWEIGHT2 = results[i].UNCOATEDWEIGHT2;
                                            //P_UNCOATEDWEIGHT3 = results[i].UNCOATEDWEIGHT3;
                                            //P_UNCOATEDWEIGHT4 = results[i].UNCOATEDWEIGHT4;
                                            //P_UNCOATEDWEIGHT5 = results[i].UNCOATEDWEIGHT5;
                                            //P_UNCOATEDWEIGHT6 = results[i].UNCOATEDWEIGHT6;
                                            #endregion

                                            #region COATWEIGHT
                                            //P_COATWEIGHT1 = results[i].COATWEIGHT1;
                                            //P_COATWEIGHT2 = results[i].COATWEIGHT2;
                                            //P_COATWEIGHT3 = results[i].COATWEIGHT3;
                                            //P_COATWEIGHT4 = results[i].COATWEIGHT4;
                                            //P_COATWEIGHT5 = results[i].COATWEIGHT5;
                                            //P_COATWEIGHT6 = results[i].COATWEIGHT6;
                                            #endregion

                                            #region THICKNESS
                                            P_THICKNESS1 = results[i].THICKNESS1;
                                            P_THICKNESS2 = results[i].THICKNESS2;
                                            P_THICKNESS3 = results[i].THICKNESS3;
                                            #endregion

                                            P_FLAMMABILITY_W = results[i].FLAMMABILITY_W;
                                            P_FLAMMABILITY_W2 = results[i].FLAMMABILITY_W2;
                                            P_FLAMMABILITY_W3 = results[i].FLAMMABILITY_W3;
                                            P_FLAMMABILITY_W4 = results[i].FLAMMABILITY_W4;
                                            P_FLAMMABILITY_W5 = results[i].FLAMMABILITY_W5;

                                            P_FLAMMABILITY_F = results[i].FLAMMABILITY_F;
                                            P_FLAMMABILITY_F2 = results[i].FLAMMABILITY_F2;
                                            P_FLAMMABILITY_F3 = results[i].FLAMMABILITY_F3;
                                            P_FLAMMABILITY_F4 = results[i].FLAMMABILITY_F4;
                                            P_FLAMMABILITY_F5 = results[i].FLAMMABILITY_F5;

                                            #region STIFFNESS
                                            P_STIFFNESS_W1 = results[i].STIFFNESS_W1;
                                            P_STIFFNESS_W2 = results[i].STIFFNESS_W2;
                                            P_STIFFNESS_W3 = results[i].STIFFNESS_W3;
                                            P_STIFFNESS_F1 = results[i].STIFFNESS_F1;
                                            P_STIFFNESS_F2 = results[i].STIFFNESS_F2;
                                            P_STIFFNESS_F3 = results[i].STIFFNESS_F3;
                                            #endregion

                                            #region STATIC_AIR
                                            P_STATIC_AIR1 = results[i].STATIC_AIR1;
                                            P_STATIC_AIR2 = results[i].STATIC_AIR2;
                                            P_STATIC_AIR3 = results[i].STATIC_AIR3;

                                            //P_STATIC_AIR4 = results[i].STATIC_AIR4;
                                            //P_STATIC_AIR5 = results[i].STATIC_AIR5;
                                            //P_STATIC_AIR6 = results[i].STATIC_AIR6;

                                            #endregion

                                            #region DYNAMIC_AIR
                                            P_DYNAMIC_AIR1 = results[i].DYNAMIC_AIR1;
                                            P_DYNAMIC_AIR2 = results[i].DYNAMIC_AIR2;
                                            P_DYNAMIC_AIR3 = results[i].DYNAMIC_AIR3;
                                            #endregion

                                            #region EXPONENT
                                            P_EXPONENT1 = results[i].EXPONENT1;
                                            P_EXPONENT2 = results[i].EXPONENT2;
                                            P_EXPONENT3 = results[i].EXPONENT3;
                                            #endregion

                                            #region DIMENSCHANGE
                                            P_DIMENSCHANGE_W1 = results[i].DIMENSCHANGE_W1;
                                            P_DIMENSCHANGE_W2 = results[i].DIMENSCHANGE_W2;
                                            P_DIMENSCHANGE_W3 = results[i].DIMENSCHANGE_W3;
                                            P_DIMENSCHANGE_F1 = results[i].DIMENSCHANGE_F1;
                                            P_DIMENSCHANGE_F2 = results[i].DIMENSCHANGE_F2;
                                            P_DIMENSCHANGE_F3 = results[i].DIMENSCHANGE_F3;
                                            #endregion

                                            #region FLEXABRASION
                                            //P_FLEXABRASION_W1 = results[i].FLEXABRASION_W1;
                                            //P_FLEXABRASION_W2 = results[i].FLEXABRASION_W2;
                                            //P_FLEXABRASION_W3 = results[i].FLEXABRASION_W3;
                                            //P_FLEXABRASION_F1 = results[i].FLEXABRASION_F1;
                                            //P_FLEXABRASION_F2 = results[i].FLEXABRASION_F2;
                                            //P_FLEXABRASION_F3 = results[i].FLEXABRASION_F3;
                                            #endregion

                                            #region BOW
                                            P_BOW1 = results[i].BOW1;
                                            P_BOW2 = results[i].BOW2;
                                            P_BOW3 = results[i].BOW3;
                                            #endregion

                                            #region SKEW
                                            P_SKEW1 = results[i].SKEW1;
                                            P_SKEW2 = results[i].SKEW2;
                                            P_SKEW3 = results[i].SKEW3;
                                            #endregion

                                            #region BENDING
                                            //P_BENDING_W1 = results[i].BENDING_W1;
                                            //P_BENDING_W2 = results[i].BENDING_W2;
                                            //P_BENDING_W3 = results[i].BENDING_W3;
                                            //P_BENDING_F1 = results[i].BENDING_F1;
                                            //P_BENDING_F2 = results[i].BENDING_F2;
                                            //P_BENDING_F3 = results[i].BENDING_F3;
                                            #endregion

                                            #region FLEX_SCOTT
                                            //P_FLEX_SCOTT_W1 = results[i].FLEX_SCOTT_W1;
                                            //P_FLEX_SCOTT_W2 = results[i].FLEX_SCOTT_W2;
                                            //P_FLEX_SCOTT_W3 = results[i].FLEX_SCOTT_W3;
                                            //P_FLEX_SCOTT_F1 = results[i].FLEX_SCOTT_F1;
                                            //P_FLEX_SCOTT_F2 = results[i].FLEX_SCOTT_F2;
                                            //P_FLEX_SCOTT_F3 = results[i].FLEX_SCOTT_F3;
                                            #endregion

                                            P_APPROVEBY = results[i].APPROVEBY;
                                            P_ENTRYDATE = results[i].ENTRYDATE;
                                            P_ENTRYBY = results[i].ENTEYBY;

                                            if (!string.IsNullOrEmpty(P_ITMCODE) && !string.IsNullOrEmpty(P_WEAVINGLOG) && !string.IsNullOrEmpty(P_FINISHINGLOT))
                                            {
                                                string insert = LabDataPDFDataService.Instance.LAB_INSERTPRODUCTIONP(P_ITMCODE, P_WEAVINGLOG, P_FINISHINGLOT, P_ENTRYDATE, P_ENTRYBY,
                                                  P_WIDTH, P_USEWIDTH1, P_USEWIDTH2, P_USEWIDTH3, P_WIDTHSILICONE1, P_WIDTHSILICONE2, P_WIDTHSILICONE3,
                                                  P_NUMTHREADS_W1, P_NUMTHREADS_W2, P_NUMTHREADS_W3, P_NUMTHREADS_F1, P_NUMTHREADS_F2, P_NUMTHREADS_F3,
                                                  P_TOTALWEIGHT1, P_TOTALWEIGHT2, P_TOTALWEIGHT3, P_TOTALWEIGHT4, P_TOTALWEIGHT5, P_TOTALWEIGHT6,
                                                  P_UNCOATEDWEIGHT1, P_UNCOATEDWEIGHT2, P_UNCOATEDWEIGHT3, P_UNCOATEDWEIGHT4, P_UNCOATEDWEIGHT5, P_UNCOATEDWEIGHT6,
                                                  P_COATWEIGHT1, P_COATWEIGHT2, P_COATWEIGHT3, P_COATWEIGHT4, P_COATWEIGHT5, P_COATWEIGHT6,
                                                  P_THICKNESS1, P_THICKNESS2, P_THICKNESS3, 
                                                  P_MAXFORCE_W1, P_MAXFORCE_W2, P_MAXFORCE_W3,
                                                  P_MAXFORCE_W4, P_MAXFORCE_W5, P_MAXFORCE_W6,
                                                  P_MAXFORCE_F1, P_MAXFORCE_F2, P_MAXFORCE_F3,
                                                  P_MAXFORCE_F4, P_MAXFORCE_F5, P_MAXFORCE_F6, 
                                                  P_ELOGATION_W1, P_ELOGATION_W2, P_ELOGATION_W3,
                                                  P_ELOGATION_W4, P_ELOGATION_W5, P_ELOGATION_W6,
                                                  P_ELOGATION_F1, P_ELOGATION_F2, P_ELOGATION_F3,
                                                  P_ELOGATION_F4, P_ELOGATION_F5, P_ELOGATION_F6,
                                                  P_FLAMMABILITY_W, P_FLAMMABILITY_W2, P_FLAMMABILITY_W3, P_FLAMMABILITY_W4, P_FLAMMABILITY_W5,
                                                  P_FLAMMABILITY_F, P_FLAMMABILITY_F2, P_FLAMMABILITY_F3, P_FLAMMABILITY_F4, P_FLAMMABILITY_F5,
                                                  P_EDGECOMB_W1, P_EDGECOMB_W2, P_EDGECOMB_W3, P_EDGECOMB_F1, P_EDGECOMB_F2, P_EDGECOMB_F3,
                                                  P_STIFFNESS_W1, P_STIFFNESS_W2, P_STIFFNESS_W3, P_STIFFNESS_F1, P_STIFFNESS_F2, P_STIFFNESS_F3,
                                                  P_TEAR_W1, P_TEAR_W2, P_TEAR_W3, P_TEAR_F1, P_TEAR_F2, P_TEAR_F3,
                                                  P_STATIC_AIR1, P_STATIC_AIR2, P_STATIC_AIR3, P_STATIC_AIR4, P_STATIC_AIR5, P_STATIC_AIR6,
                                                  P_DYNAMIC_AIR1, P_DYNAMIC_AIR2, P_DYNAMIC_AIR3,
                                                  P_EXPONENT1, P_EXPONENT2, P_EXPONENT3, P_DIMENSCHANGE_W1, P_DIMENSCHANGE_W2, P_DIMENSCHANGE_W3,
                                                  P_DIMENSCHANGE_F1, P_DIMENSCHANGE_F2, P_DIMENSCHANGE_F3, P_FLEXABRASION_W1, P_FLEXABRASION_W2, P_FLEXABRASION_W3,
                                                  P_FLEXABRASION_F1, P_FLEXABRASION_F2, P_FLEXABRASION_F3, P_STATUS, P_REMARK, P_APPROVEBY, P_APPROVEDATE,
                                                  P_BOW1, P_BOW2, P_BOW3, P_SKEW1, P_SKEW2, P_SKEW3,
                                                  P_BENDING_W1, P_BENDING_W2, P_BENDING_W3, P_BENDING_F1, P_BENDING_F2, P_BENDING_F3,
                                                  P_FLEX_SCOTT_W1, P_FLEX_SCOTT_W2, P_FLEX_SCOTT_W3, P_FLEX_SCOTT_F1, P_FLEX_SCOTT_F2, P_FLEX_SCOTT_F3);

                                                if (insert == "1")
                                                {
                                                    RowNo2++;
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(Err))
                                                        Err += " , " + insert;
                                                    else
                                                        Err += insert;

                                                    RowNoErr++;
                                                }
                                            }
                                            else
                                                chkApprove = false;

                                            //if (insert == "1")
                                            //{
                                            //    this.listView.Items.Add(new CheckList { NO = RowNo ,ITMCODE = P_ITMCODE, WEAVINGLOG = P_WEAVINGLOG,FINISHINGLOT = P_FINISHINGLOT, STATUS = "Complete", ERROR = ""});
                                            //}
                                            //else
                                            //{
                                            //    this.listView.Items.Add(new CheckList { NO = RowNo ,ITMCODE = P_ITMCODE, WEAVINGLOG = P_WEAVINGLOG, FINISHINGLOT = P_FINISHINGLOT, STATUS = "Error", ERROR = insert});
                                            //}

                                            //RowNo++;

                                        }

                                        if (chkApprove == true)
                                            this.listView.Items.Add(new CheckList { FILENAME = newFileName, SUMROW = RowNo2, SUMERR = RowNoErr, ERROR = Err });
                                    }
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                ex.Message.Err();
                            }
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

        #region ExcelToLAB_ImportExcel1
        private List<LAB_ImportExcel1> ExcelToLAB_ImportExcel1(string filePath)
        {
            List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

            try
            {
                decimal value;

                XSSFWorkbook hssfworkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                int s = hssfworkbook.ActiveSheetIndex;

                XSSFSheet sheet = (XSSFSheet)hssfworkbook.GetSheetAt(s);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                XSSFRow headerRow = (XSSFRow)sheet.GetRow(0);
                string itemcode = string.Empty;
                int startRow = 6;
                int cellCount = 0;

                if (!string.IsNullOrEmpty(headerRow.Cells[0].ToString()))
                    itemcode = headerRow.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(headerRow.Cells[1].ToString()))
                    itemcode = headerRow.Cells[1].ToString();

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();

                if (!string.IsNullOrEmpty(itemcode))
                {
                    #region Def

                    int p_weavinglog = 0;
                    int p_finishinglot = 0;
                    int p_entrydate = 0;
                    int p_width = 0;
                    int p_usewidth1 = 0;
                    int p_usewidth2 = 0;
                    int p_usewidth3 = 0;
                    int p_totalweight1 = 0;
                    int p_totalweight2 = 0;
                    int p_totalweight3 = 0;
                    int p_thickness1 = 0;
                    int p_thickness2 = 0;
                    int p_thickness3 = 0;
                    int p_numthreads_w1 = 0;
                    int p_numthreads_w2 = 0;
                    int p_numthreads_w3 = 0;
                    int p_numthreads_f1 = 0;
                    int p_numthreads_f2 = 0;
                    int p_numthreads_f3 = 0;
                    int p_maxforce_w1 = 0;
                    int p_maxforce_w2 = 0;
                    int p_maxforce_w3 = 0;
                    int p_elogation_w1 = 0;
                    int p_elogation_w2 = 0;
                    int p_elogation_w3 = 0;
                    int p_maxforce_f1 = 0;
                    int p_maxforce_f2 = 0;
                    int p_maxforce_f3 = 0;
                    int p_elogation_f1 = 0;
                    int p_elogation_f2 = 0;
                    int p_elogation_f3 = 0;
                    int p_edgecomb_w1 = 0;
                    int p_edgecomb_w2 = 0;
                    int p_edgecomb_w3 = 0;
                    int p_edgecomb_f1 = 0;
                    int p_edgecomb_f2 = 0;
                    int p_edgecomb_f3 = 0;
                    int p_stiffness_w1 = 0;
                    int p_stiffness_w2 = 0;
                    int p_stiffness_w3 = 0;
                    int p_stiffness_f1 = 0;
                    int p_stiffness_f2 = 0;
                    int p_stiffness_f3 = 0;
                    int p_tear_w1 = 0;
                    int p_tear_w2 = 0;
                    int p_tear_w3 = 0;
                    int p_tear_f1 = 0;
                    int p_tear_f2 = 0;
                    int p_tear_f3 = 0;
                    int p_static_air1 = 0;
                    int p_static_air2 = 0;
                    int p_static_air3 = 0;
                    int p_dynamic_air1 = 0;
                    int p_dynamic_air2 = 0;
                    int p_dynamic_air3 = 0;

                    int p_exponent1 = 0;
                    int p_exponent2 = 0;
                    int p_exponent3 = 0;

                    int p_flammability_w = 0;
                    int p_flammability_f = 0;

                    int p_dimenschange_w1 = 0;
                    int p_dimenschange_w2 = 0;
                    int p_dimenschange_w3 = 0;
                    int p_dimenschange_f1 = 0;
                    int p_dimenschange_f2 = 0;
                    int p_dimenschange_f3 = 0;

                    int p_bow1 = 0;
                    int p_bow2 = 0;
                    int p_bow3 = 0;
                    int p_skew1 = 0;
                    int p_skew2 = 0;
                    int p_skew3 = 0;

                    int p_entryby = 0;
                    int p_approveby = 0;

                    int chkCoulmn = 0;
                    int chknull = 0;
                    int chkWeavinglog = 0;

                    #endregion

                    XSSFRow row = (XSSFRow)sheet.GetRow(startRow);
                    cellCount = row.Cells.Count;

                    if (row.Cells.Count > 89)
                    {
                        // Set Head
                        #region Set Head

                        for (int j = 0; j < row.Cells.Count + 1; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (row.GetCell(j).CellType != CellType.Blank)
                                {
                                    if (row.GetCell(j).ToString() != "")
                                    {
                                        if (chkCoulmn == 1)
                                            p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 3)
                                            p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 4)
                                            p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 5)
                                            p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 6)
                                            p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 7)
                                            p_usewidth2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 8)
                                            p_usewidth3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 9)
                                            p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 10)
                                            p_totalweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 11)
                                            p_totalweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 12)
                                            p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 13)
                                            p_thickness2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 14)
                                            p_thickness3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 15)
                                            p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 16)
                                            p_numthreads_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 17)
                                            p_numthreads_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 18)
                                            p_numthreads_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 19)
                                            p_numthreads_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 20)
                                            p_numthreads_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 21)
                                            p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 22)
                                            p_maxforce_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 23)
                                            p_maxforce_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 24)
                                            p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 25)
                                            p_elogation_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 26)
                                            p_elogation_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 27)
                                            p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 28)
                                            p_maxforce_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 29)
                                            p_maxforce_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 30)
                                            p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 31)
                                            p_elogation_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 32)
                                            p_elogation_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 33)
                                            p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 34)
                                            p_edgecomb_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 35)
                                            p_edgecomb_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 36)
                                            p_edgecomb_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 37)
                                            p_edgecomb_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 38)
                                            p_edgecomb_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 39)
                                            p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 40)
                                            p_stiffness_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 41)
                                            p_stiffness_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 42)
                                            p_stiffness_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 43)
                                            p_stiffness_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 44)
                                            p_stiffness_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 45)
                                            p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 46)
                                            p_tear_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 47)
                                            p_tear_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 48)
                                            p_tear_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 49)
                                            p_tear_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 50)
                                            p_tear_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 51)
                                            p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 52)
                                            p_static_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 53)
                                            p_static_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 60)
                                            p_dynamic_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 61)
                                            p_dynamic_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 62)
                                            p_dynamic_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 63)
                                            p_exponent1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 64)
                                            p_exponent2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 65)
                                            p_exponent3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 66)
                                            p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 67)
                                            p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 68)
                                            p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 69)
                                            p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 70)
                                            p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 71)
                                            p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 72)
                                            p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 73)
                                            p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 74)
                                            p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 75)
                                            p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 83)
                                            p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 84)
                                            p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 85)
                                            p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 86)
                                            p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 87)
                                            p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 88)
                                            p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        chkCoulmn++;
                                    }
                                }

                                if (p_entryby != 0)
                                {
                                    if (row.GetCell(j).CellType == CellType.Blank)
                                    {
                                        if (chknull >= 0 && chknull <= 3)
                                        {
                                            chknull++;
                                        }
                                        else
                                            break;
                                    }
                                }
                            }
                        }

                        #endregion

                        try
                        {
                            #region Add Detail

                            for (int i = (startRow); i <= sheet.LastRowNum; i++)
                            {
                                LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                                row = (XSSFRow)sheet.GetRow(i);

                                if (row.Cells.Count() >= chkCoulmn && cellCount <= row.Cells.Count())
                                {
                                    inst.ITM_CODE = itemcode;

                                    #region WEAVINGLOT
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                                break;

                                            if (inst.WEAVINGLOT.Count() < 9)
                                            {
                                                chkWeavinglog++;
                                            }

                                            if (chkWeavinglog >= 3)
                                            {
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FINISHINGLOT
                                    if (row.Cells[p_finishinglot] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_finishinglot].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_finishinglot != 0)
                                                inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                                            if (string.IsNullOrEmpty(inst.FINISHINGLOT))
                                                break;
                                        }
                                    }
                                    #endregion

                                    //if (p_finishinglot != 0)
                                    //    inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                                    if (p_entrydate != 0)
                                    {
                                        if (row.Cells[p_entrydate] != null)
                                            inst.ENTRYDATE = DateTime.Parse(row.Cells[p_entrydate].ToString());
                                    }

                                    #region WIDTH
                                    if (p_width != 0)
                                    {
                                        if (row.Cells[p_width] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_width].ToString(), out value))
                                            {
                                                inst.WIDTH = decimal.Parse(row.Cells[p_width].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region USABLE_WIDTH
                                    if (p_usewidth1 != 0)
                                    {
                                        if (row.Cells[p_usewidth1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth1].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH1 = decimal.Parse(row.Cells[p_usewidth1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth2].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH2 = decimal.Parse(row.Cells[p_usewidth2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth3].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH3 = decimal.Parse(row.Cells[p_usewidth3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TOTALWEIGHT
                                    if (p_totalweight1 != 0)
                                    {
                                        if (row.Cells[p_totalweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight1].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT1 = decimal.Parse(row.Cells[p_totalweight1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight2].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT2 = decimal.Parse(row.Cells[p_totalweight2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight3].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT3 = decimal.Parse(row.Cells[p_totalweight3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region THICKNESS
                                    if (p_thickness1 != 0)
                                    {
                                        if (row.Cells[p_thickness1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness1].ToString(), out value))
                                            {
                                                inst.THICKNESS1 = decimal.Parse(row.Cells[p_thickness1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness2].ToString(), out value))
                                            {
                                                inst.THICKNESS2 = decimal.Parse(row.Cells[p_thickness2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness3].ToString(), out value))
                                            {
                                                inst.THICKNESS3 = decimal.Parse(row.Cells[p_thickness3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region NUMTHREADS
                                    if (p_numthreads_w1 != 0)
                                    {
                                        if (row.Cells[p_numthreads_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W1 = decimal.Parse(row.Cells[p_numthreads_w1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W2 = decimal.Parse(row.Cells[p_numthreads_w2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W3 = decimal.Parse(row.Cells[p_numthreads_w3].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F1 = decimal.Parse(row.Cells[p_numthreads_f1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F2 = decimal.Parse(row.Cells[p_numthreads_f2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F3 = decimal.Parse(row.Cells[p_numthreads_f3].ToString()) * (10);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region MAXFORCE
                                    if (p_maxforce_w1 != 0)
                                    {
                                        if (row.Cells[p_maxforce_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W1 = decimal.Parse(row.Cells[p_maxforce_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W2 = decimal.Parse(row.Cells[p_maxforce_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W3 = decimal.Parse(row.Cells[p_maxforce_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F1 = decimal.Parse(row.Cells[p_maxforce_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F2 = decimal.Parse(row.Cells[p_maxforce_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F3 = decimal.Parse(row.Cells[p_maxforce_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region ELONGATIONFORCE
                                    if (p_elogation_w1 != 0)
                                    {
                                        if (row.Cells[p_elogation_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W1 = decimal.Parse(row.Cells[p_elogation_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W2 = decimal.Parse(row.Cells[p_elogation_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W3 = decimal.Parse(row.Cells[p_elogation_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F1 = decimal.Parse(row.Cells[p_elogation_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F2 = decimal.Parse(row.Cells[p_elogation_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F3 = decimal.Parse(row.Cells[p_elogation_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region EDGECOMB
                                    if (p_edgecomb_w1 != 0)
                                    {
                                        if (row.Cells[p_edgecomb_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W1 = decimal.Parse(row.Cells[p_edgecomb_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W2 = decimal.Parse(row.Cells[p_edgecomb_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W3 = decimal.Parse(row.Cells[p_edgecomb_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F1 = decimal.Parse(row.Cells[p_edgecomb_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F2 = decimal.Parse(row.Cells[p_edgecomb_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F3 = decimal.Parse(row.Cells[p_edgecomb_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STIFFNESS
                                    if (p_stiffness_w1 != 0)
                                    {
                                        if (row.Cells[p_stiffness_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W1 = decimal.Parse(row.Cells[p_stiffness_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W2 = decimal.Parse(row.Cells[p_stiffness_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W3 = decimal.Parse(row.Cells[p_stiffness_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F1 = decimal.Parse(row.Cells[p_stiffness_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F2 = decimal.Parse(row.Cells[p_stiffness_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F3 = decimal.Parse(row.Cells[p_stiffness_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TEAR
                                    if (p_tear_w1 != 0)
                                    {
                                        if (row.Cells[p_tear_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w1].ToString(), out value))
                                            {
                                                inst.TEAR_W1 = decimal.Parse(row.Cells[p_tear_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w2].ToString(), out value))
                                            {
                                                inst.TEAR_W2 = decimal.Parse(row.Cells[p_tear_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w3].ToString(), out value))
                                            {
                                                inst.TEAR_W3 = decimal.Parse(row.Cells[p_tear_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f1].ToString(), out value))
                                            {
                                                inst.TEAR_F1 = decimal.Parse(row.Cells[p_tear_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f2].ToString(), out value))
                                            {
                                                inst.TEAR_F2 = decimal.Parse(row.Cells[p_tear_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f3].ToString(), out value))
                                            {
                                                inst.TEAR_F3 = decimal.Parse(row.Cells[p_tear_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STATIC_AIR
                                    if (p_static_air1 != 0)
                                    {
                                        if (row.Cells[p_static_air1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air1].ToString(), out value))
                                            {
                                                inst.STATIC_AIR1 = decimal.Parse(row.Cells[p_static_air1].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air1]) != null)
                                                    inst.STATIC_AIR1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air1]).NumberValue.ToString());
                                            }

                                        }
                                        if (row.Cells[p_static_air2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air2].ToString(), out value))
                                            {
                                                inst.STATIC_AIR2 = decimal.Parse(row.Cells[p_static_air2].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air2]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_static_air3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air3].ToString(), out value))
                                            {
                                                inst.STATIC_AIR3 = decimal.Parse(row.Cells[p_static_air3].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air3]).NumberValue.ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region DYNAMIC_AIR
                                    if (p_dynamic_air1 != 0)
                                    {
                                        if (row.Cells[p_dynamic_air1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dynamic_air1].ToString(), out value))
                                            {
                                                inst.DYNAMIC_AIR1 = decimal.Parse(row.Cells[p_dynamic_air1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dynamic_air2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dynamic_air2].ToString(), out value))
                                            {
                                                inst.DYNAMIC_AIR2 = decimal.Parse(row.Cells[p_dynamic_air2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dynamic_air3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dynamic_air3].ToString(), out value))
                                            {
                                                inst.DYNAMIC_AIR3 = decimal.Parse(row.Cells[p_dynamic_air3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region EXPONENT
                                    if (p_exponent1 != 0)
                                    {
                                        if (row.Cells[p_exponent1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_exponent1].ToString(), out value))
                                            {
                                                inst.EXPONENT1 = decimal.Parse(row.Cells[p_exponent1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_exponent2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_exponent2].ToString(), out value))
                                            {
                                                inst.EXPONENT2 = decimal.Parse(row.Cells[p_exponent2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_exponent3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_exponent3].ToString(), out value))
                                            {
                                                inst.EXPONENT3 = decimal.Parse(row.Cells[p_exponent3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FLAMMABILITY
                                    if (p_flammability_w != 0)
                                    {
                                        if (row.Cells[p_flammability_w] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_w].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_W = decimal.Parse(row.Cells[p_flammability_w].ToString());
                                            }
                                        }
                                        if (row.Cells[p_flammability_f] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_f].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_F = decimal.Parse(row.Cells[p_flammability_f].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region DIMENSCHANGE
                                    if (p_dimenschange_w1 != 0)
                                    {
                                        if (row.Cells[p_dimenschange_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W1 = decimal.Parse(row.Cells[p_dimenschange_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W2 = decimal.Parse(row.Cells[p_dimenschange_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W3 = decimal.Parse(row.Cells[p_dimenschange_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F1 = decimal.Parse(row.Cells[p_dimenschange_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F2 = decimal.Parse(row.Cells[p_dimenschange_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F3 = decimal.Parse(row.Cells[p_dimenschange_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region BOW & SKEW

                                    if (p_bow1 != 0)
                                    {
                                        if (row.Cells[p_bow1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow1].ToString(), out value))
                                            {
                                                inst.BOW1 = decimal.Parse(row.Cells[p_bow1].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew1 != 0)
                                    {
                                        if (row.Cells[p_skew1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew1].ToString(), out value))
                                            {
                                                inst.SKEW1 = decimal.Parse(row.Cells[p_skew1].ToString());
                                            }
                                        }
                                    }

                                    if (p_entryby != 0)
                                    {
                                        inst.ENTEYBY = row.Cells[p_entryby].ToString();
                                    }

                                    if (p_approveby != 0)
                                    {
                                        inst.APPROVEBY = row.Cells[p_approveby].ToString();
                                    }

                                    if (p_bow2 != 0)
                                    {
                                        if (row.Cells[p_bow2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow2].ToString(), out value))
                                            {
                                                inst.BOW2 = decimal.Parse(row.Cells[p_bow2].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew2 != 0)
                                    {
                                        if (row.Cells[p_skew2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew2].ToString(), out value))
                                            {
                                                inst.SKEW2 = decimal.Parse(row.Cells[p_skew2].ToString());
                                            }
                                        }
                                    }

                                    if (p_bow3 != 0)
                                    {
                                        if (row.Cells[p_bow3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow3].ToString(), out value))
                                            {
                                                inst.BOW3 = decimal.Parse(row.Cells[p_bow3].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew3 != 0)
                                    {
                                        if (row.Cells[p_skew3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew3].ToString(), out value))
                                            {
                                                inst.SKEW3 = decimal.Parse(row.Cells[p_skew3].ToString());
                                            }
                                        }
                                    }


                                    #endregion

                                    if (inst.WEAVINGLOT.Count() >= 9)
                                    {
                                        results.Add(inst);
                                    }
                                    else
                                    {
                                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                    }
                                }
                                else
                                {
                                    #region List
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                            {
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel Lot No. = " + inst.WEAVINGLOT });
                                            }

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                            {
                                                break;
                                            }

                                            if (inst.WEAVINGLOT.Count() < 7)
                                            {
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel" });
                                                break;
                                            }
                                        }
                                    }
                                    #endregion
                                }
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = ex.Message.ToString() });
                        }
                    }
                    else
                    {
                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Excel Head Cell < 88 Please check file excel" });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();

                return results;
            }
        }

        #endregion

        #region ExcelToLAB_ImportExcel197
        private List<LAB_ImportExcel1> ExcelToLAB_ImportExcel197(string filePath)
        {
            List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

            try
            {

                decimal value;

                HSSFWorkbook hssfworkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }

                int s = hssfworkbook.ActiveSheetIndex;

                ISheet sheet = hssfworkbook.GetSheetAt(s);
                IRow row = sheet.GetRow(0);

                string itemcode = string.Empty;
                int startRow = 6;
                int cellCount = 0;

                if (!string.IsNullOrEmpty(row.Cells[0].ToString()))
                    itemcode = row.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(row.Cells[1].ToString()))
                    itemcode = row.Cells[1].ToString();

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();

                if (!string.IsNullOrEmpty(itemcode))
                {
                    #region Def

                    int p_weavinglog = 0;
                    int p_finishinglot = 0;
                    int p_entrydate = 0;
                    int p_width = 0;
                    int p_usewidth1 = 0;
                    int p_usewidth2 = 0;
                    int p_usewidth3 = 0;
                    int p_totalweight1 = 0;
                    int p_totalweight2 = 0;
                    int p_totalweight3 = 0;
                    int p_thickness1 = 0;
                    int p_thickness2 = 0;
                    int p_thickness3 = 0;
                    int p_numthreads_w1 = 0;
                    int p_numthreads_w2 = 0;
                    int p_numthreads_w3 = 0;
                    int p_numthreads_f1 = 0;
                    int p_numthreads_f2 = 0;
                    int p_numthreads_f3 = 0;
                    int p_maxforce_w1 = 0;
                    int p_maxforce_w2 = 0;
                    int p_maxforce_w3 = 0;
                    int p_elogation_w1 = 0;
                    int p_elogation_w2 = 0;
                    int p_elogation_w3 = 0;
                    int p_maxforce_f1 = 0;
                    int p_maxforce_f2 = 0;
                    int p_maxforce_f3 = 0;
                    int p_elogation_f1 = 0;
                    int p_elogation_f2 = 0;
                    int p_elogation_f3 = 0;
                    int p_edgecomb_w1 = 0;
                    int p_edgecomb_w2 = 0;
                    int p_edgecomb_w3 = 0;
                    int p_edgecomb_f1 = 0;
                    int p_edgecomb_f2 = 0;
                    int p_edgecomb_f3 = 0;
                    int p_stiffness_w1 = 0;
                    int p_stiffness_w2 = 0;
                    int p_stiffness_w3 = 0;
                    int p_stiffness_f1 = 0;
                    int p_stiffness_f2 = 0;
                    int p_stiffness_f3 = 0;
                    int p_tear_w1 = 0;
                    int p_tear_w2 = 0;
                    int p_tear_w3 = 0;
                    int p_tear_f1 = 0;
                    int p_tear_f2 = 0;
                    int p_tear_f3 = 0;
                    int p_static_air1 = 0;
                    int p_static_air2 = 0;
                    int p_static_air3 = 0;
                    int p_dynamic_air1 = 0;
                    int p_dynamic_air2 = 0;
                    int p_dynamic_air3 = 0;

                    int p_exponent1 = 0;
                    int p_exponent2 = 0;
                    int p_exponent3 = 0;

                    int p_flammability_w = 0;
                    int p_flammability_f = 0;

                    int p_dimenschange_w1 = 0;
                    int p_dimenschange_w2 = 0;
                    int p_dimenschange_w3 = 0;
                    int p_dimenschange_f1 = 0;
                    int p_dimenschange_f2 = 0;
                    int p_dimenschange_f3 = 0;

                    int p_bow1 = 0;
                    int p_bow2 = 0;
                    int p_bow3 = 0;
                    int p_skew1 = 0;
                    int p_skew2 = 0;
                    int p_skew3 = 0;

                    int p_entryby = 0;
                    int p_approveby = 0;

                    int chkCoulmn = 0;
                    int chknull = 0;
                    int chkWeavinglog = 0;

                    #endregion

                    row = sheet.GetRow(startRow);
                    cellCount = row.Cells.Count;

                    if (row.Cells.Count > 89)
                    {
                        // Set Head
                        #region Set Head

                        for (int j = 0; j < row.Cells.Count + 1; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (row.GetCell(j).CellType != CellType.Blank)
                                {
                                    if (row.GetCell(j).ToString() != "")
                                    {
                                        if (chkCoulmn == 1)
                                            p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 3)
                                            p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 4)
                                            p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 5)
                                            p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 6)
                                            p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 7)
                                            p_usewidth2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 8)
                                            p_usewidth3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 9)
                                            p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 10)
                                            p_totalweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 11)
                                            p_totalweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 12)
                                            p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 13)
                                            p_thickness2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 14)
                                            p_thickness3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 15)
                                            p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 16)
                                            p_numthreads_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 17)
                                            p_numthreads_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 18)
                                            p_numthreads_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 19)
                                            p_numthreads_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 20)
                                            p_numthreads_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 21)
                                            p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 22)
                                            p_maxforce_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 23)
                                            p_maxforce_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 24)
                                            p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 25)
                                            p_elogation_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 26)
                                            p_elogation_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 27)
                                            p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 28)
                                            p_maxforce_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 29)
                                            p_maxforce_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 30)
                                            p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 31)
                                            p_elogation_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 32)
                                            p_elogation_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 33)
                                            p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 34)
                                            p_edgecomb_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 35)
                                            p_edgecomb_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 36)
                                            p_edgecomb_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 37)
                                            p_edgecomb_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 38)
                                            p_edgecomb_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 39)
                                            p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 40)
                                            p_stiffness_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 41)
                                            p_stiffness_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 42)
                                            p_stiffness_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 43)
                                            p_stiffness_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 44)
                                            p_stiffness_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 45)
                                            p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 46)
                                            p_tear_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 47)
                                            p_tear_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 48)
                                            p_tear_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 49)
                                            p_tear_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 50)
                                            p_tear_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 51)
                                            p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 52)
                                            p_static_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 53)
                                            p_static_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 60)
                                            p_dynamic_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 61)
                                            p_dynamic_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 62)
                                            p_dynamic_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 63)
                                            p_exponent1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 64)
                                            p_exponent2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 65)
                                            p_exponent3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 66)
                                            p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 67)
                                            p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 68)
                                            p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 69)
                                            p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 70)
                                            p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 71)
                                            p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 72)
                                            p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 73)
                                            p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 74)
                                            p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 75)
                                            p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 83)
                                            p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 84)
                                            p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        if (chkCoulmn == 85)
                                            p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 86)
                                            p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 87)
                                            p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                        if (chkCoulmn == 88)
                                            p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                        chkCoulmn++;
                                    }
                                }

                                if (p_entryby != 0)
                                {
                                    if (row.GetCell(j).CellType == CellType.Blank)
                                    {
                                        if (chknull >= 0 && chknull <= 3)
                                        {
                                            chknull++;
                                        }
                                        else
                                            break;
                                    }
                                }

                                #region Old
                                //if (row.GetCell(j).ToString().Contains("Lot No."))
                                //{
                                //    p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //}

                                //if (row.GetCell(j).ToString().Contains("Lot no."))
                                //{
                                //    if (p_weavinglog != 0)
                                //        p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //}

                                //if (row.GetCell(j).ToString().Contains("Testing Date"))
                                //{
                                //    p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //}
                                //if (row.GetCell(j).ToString().Contains("Total Width"))
                                //{
                                //    p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //}

                                //if (row.GetCell(j).ToString().Contains("Use width"))
                                //{
                                //    p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_usewidth2 = p_usewidth1 + 1;
                                //    p_usewidth3 = p_usewidth2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Total Weight") || row.GetCell(j).ToString().Contains("Weight"))
                                //{
                                //    p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_totalweight2 = p_totalweight1 + 1;
                                //    p_totalweight3 = p_totalweight2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Thickness"))
                                //{
                                //    p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_thickness2 = p_thickness1 + 1;
                                //    p_thickness3 = p_thickness2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Density(Unit"))
                                //{
                                //    p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_numthreads_w2 = p_numthreads_w1 + 1;
                                //    p_numthreads_w3 = p_numthreads_w2 + 1;
                                //    p_numthreads_f1 = p_numthreads_w3 + 1;
                                //    p_numthreads_f2 = p_numthreads_f1 + 1;
                                //    p_numthreads_f3 = p_numthreads_f2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Tensile Strength"))
                                //{
                                //    if (chkmaxforce == 0)
                                //    {
                                //        p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //        p_maxforce_w2 = p_maxforce_w1 + 1;
                                //        p_maxforce_w3 = p_maxforce_w2 + 1;

                                //        chkmaxforce++;
                                //    }
                                //    else
                                //    {
                                //        p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //        p_maxforce_f2 = p_maxforce_f1 + 1;
                                //        p_maxforce_f3 = p_maxforce_f2 + 1;
                                //    }
                                //}
                                //if (row.GetCell(j).ToString().Contains("Elongation"))
                                //{
                                //    if (chkelogation == 0)
                                //    {
                                //        p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //        p_elogation_w2 = p_elogation_w1 + 1;
                                //        p_elogation_w3 = p_elogation_w2 + 1;

                                //        chkelogation++;
                                //    }
                                //    else
                                //    {
                                //        p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //        p_elogation_f2 = p_elogation_f1 + 1;
                                //        p_elogation_f3 = p_elogation_f2 + 1;
                                //    }
                                //}
                                //if (row.GetCell(j).ToString().Contains("Edgecom resistance"))
                                //{
                                //    p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_edgecomb_w2 = p_edgecomb_w1 + 1;
                                //    p_edgecomb_w3 = p_edgecomb_w2 + 1;
                                //    p_edgecomb_f1 = p_edgecomb_w3 + 1;
                                //    p_edgecomb_f2 = p_edgecomb_f1 + 1;
                                //    p_edgecomb_f3 = p_edgecomb_f2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Stiffness"))
                                //{
                                //    if (chkStiffness == false)
                                //    {
                                //        p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //        p_stiffness_w2 = p_stiffness_w1 + 1;
                                //        p_stiffness_w3 = p_stiffness_w2 + 1;
                                //        p_stiffness_f1 = p_stiffness_w3 + 1;
                                //        p_stiffness_f2 = p_stiffness_f1 + 1;
                                //        p_stiffness_f3 = p_stiffness_f2 + 1;

                                //        chkStiffness = true;
                                //    }
                                //}
                                //if (row.GetCell(j).ToString().Contains("Tear Strength"))
                                //{
                                //    p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_tear_w2 = p_tear_w1 + 1;
                                //    p_tear_w3 = p_tear_w2 + 1;
                                //    p_tear_f1 = p_tear_w3 + 1;
                                //    p_tear_f2 = p_tear_f1 + 1;
                                //    p_tear_f3 = p_tear_f2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Static Air Permeability"))
                                //{
                                //    p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_static_air2 = p_static_air1 + 1;
                                //    p_static_air3 = p_static_air2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("DynamicAir Permeability") || row.GetCell(j).ToString().Contains("Dynamic Air Permeability"))
                                //{
                                //    p_dynamic_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_dynamic_air2 = p_dynamic_air1 + 1;
                                //    p_dynamic_air3 = p_dynamic_air2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Eponent") || row.GetCell(j).ToString().Contains("Exponent"))
                                //{
                                //    p_exponent1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_exponent2 = p_exponent1 + 1;
                                //    p_exponent3 = p_exponent2 + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Flammability"))
                                //{
                                //    p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_flammability_f = p_flammability_w + 1;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Shrinkage"))
                                //{
                                //    p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    p_dimenschange_w2 = p_dimenschange_w1 + 1;
                                //    p_dimenschange_w3 = p_dimenschange_w2 + 1;
                                //    p_dimenschange_f1 = p_dimenschange_w3 + 1;
                                //    p_dimenschange_f2 = p_dimenschange_f1 + 1;
                                //    p_dimenschange_f3 = p_dimenschange_f2 + 1;
                                //}

                                //if (row.GetCell(j).ToString().Contains("Bow"))
                                //{
                                //    if (chkbow == 0)
                                //    {
                                //        p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    }
                                //    else if (chkbow == 1)
                                //    {
                                //        p_bow2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    }
                                //    else if (chkbow == 2)
                                //    {
                                //        p_bow3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    }

                                //    chkbow++;
                                //}
                                //if (row.GetCell(j).ToString().Contains("Bias"))
                                //{
                                //    if (chkskew == 0)
                                //    {
                                //        p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    }
                                //    else if (chkskew == 1)
                                //    {
                                //        p_skew2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    }
                                //    else if (chkskew == 2)
                                //    {
                                //        p_skew3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                //    }

                                //    chkskew++;
                                //}

                                //if (row.GetCell(j).ToString().Contains("Tested") || row.GetCell(j).ToString().Contains("Tester") || row.GetCell(j).ToString().Contains("LAB"))
                                //{
                                //    p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString()) + 1;
                                //    p_approveby = p_entryby + 1;
                                //}

                                //if (p_entryby != 0)
                                //{
                                //    if (row.GetCell(j).ToString().Contains(""))
                                //    {
                                //        if (chknull >= 0 && chknull <= 6)
                                //        {
                                //            chknull++;
                                //        }
                                //        else
                                //            break;
                                //    }
                                //}

                                #endregion
                            }
                        }

                        #endregion

                        try
                        {
                            #region Add Detail

                            for (int i = (startRow); i <= sheet.LastRowNum; i++)
                            {
                                LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                                row = sheet.GetRow(i);

                                if (row.Cells.Count() >= chkCoulmn && cellCount <= row.Cells.Count())
                                {

                                    inst.ITM_CODE = itemcode;

                                    #region WEAVINGLOT
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                                break;

                                            if (inst.WEAVINGLOT.Count() < 9)
                                            {
                                                chkWeavinglog++;
                                            }

                                            if (chkWeavinglog >= 3)
                                            {
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FINISHINGLOT
                                    if (row.Cells[p_finishinglot] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_finishinglot].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_finishinglot != 0)
                                                inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                                            if (string.IsNullOrEmpty(inst.FINISHINGLOT))
                                                break;
                                        }
                                    }
                                    #endregion

                                    //if (p_finishinglot != 0)
                                    //    inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                                    if (p_entrydate != 0)
                                    {
                                        if (row.Cells[p_entrydate] != null)
                                            inst.ENTRYDATE = DateTime.Parse(row.Cells[p_entrydate].ToString());
                                    }

                                    #region WIDTH
                                    if (p_width != 0)
                                    {
                                        if (row.Cells[p_width] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_width].ToString(), out value))
                                            {
                                                inst.WIDTH = decimal.Parse(row.Cells[p_width].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region USABLE_WIDTH
                                    if (p_usewidth1 != 0)
                                    {
                                        if (row.Cells[p_usewidth1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth1].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH1 = decimal.Parse(row.Cells[p_usewidth1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth2].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH2 = decimal.Parse(row.Cells[p_usewidth2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth3].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH3 = decimal.Parse(row.Cells[p_usewidth3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TOTALWEIGHT
                                    if (p_totalweight1 != 0)
                                    {
                                        if (row.Cells[p_totalweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight1].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT1 = decimal.Parse(row.Cells[p_totalweight1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight2].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT2 = decimal.Parse(row.Cells[p_totalweight2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight3].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT3 = decimal.Parse(row.Cells[p_totalweight3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region THICKNESS
                                    if (p_thickness1 != 0)
                                    {
                                        if (row.Cells[p_thickness1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness1].ToString(), out value))
                                            {
                                                inst.THICKNESS1 = decimal.Parse(row.Cells[p_thickness1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness2].ToString(), out value))
                                            {
                                                inst.THICKNESS2 = decimal.Parse(row.Cells[p_thickness2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness3].ToString(), out value))
                                            {
                                                inst.THICKNESS3 = decimal.Parse(row.Cells[p_thickness3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region NUMTHREADS
                                    if (p_numthreads_w1 != 0)
                                    {
                                        if (row.Cells[p_numthreads_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W1 = decimal.Parse(row.Cells[p_numthreads_w1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W2 = decimal.Parse(row.Cells[p_numthreads_w2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W3 = decimal.Parse(row.Cells[p_numthreads_w3].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F1 = decimal.Parse(row.Cells[p_numthreads_f1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F2 = decimal.Parse(row.Cells[p_numthreads_f2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F3 = decimal.Parse(row.Cells[p_numthreads_f3].ToString()) * (10);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region MAXFORCE
                                    if (p_maxforce_w1 != 0)
                                    {
                                        if (row.Cells[p_maxforce_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W1 = decimal.Parse(row.Cells[p_maxforce_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W2 = decimal.Parse(row.Cells[p_maxforce_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W3 = decimal.Parse(row.Cells[p_maxforce_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F1 = decimal.Parse(row.Cells[p_maxforce_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F2 = decimal.Parse(row.Cells[p_maxforce_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F3 = decimal.Parse(row.Cells[p_maxforce_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region ELONGATIONFORCE
                                    if (p_elogation_w1 != 0)
                                    {
                                        if (row.Cells[p_elogation_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W1 = decimal.Parse(row.Cells[p_elogation_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W2 = decimal.Parse(row.Cells[p_elogation_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W3 = decimal.Parse(row.Cells[p_elogation_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F1 = decimal.Parse(row.Cells[p_elogation_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F2 = decimal.Parse(row.Cells[p_elogation_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F3 = decimal.Parse(row.Cells[p_elogation_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region EDGECOMB
                                    if (p_edgecomb_w1 != 0)
                                    {
                                        if (row.Cells[p_edgecomb_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W1 = decimal.Parse(row.Cells[p_edgecomb_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W2 = decimal.Parse(row.Cells[p_edgecomb_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W3 = decimal.Parse(row.Cells[p_edgecomb_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F1 = decimal.Parse(row.Cells[p_edgecomb_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F2 = decimal.Parse(row.Cells[p_edgecomb_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F3 = decimal.Parse(row.Cells[p_edgecomb_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STIFFNESS
                                    if (p_stiffness_w1 != 0)
                                    {
                                        if (row.Cells[p_stiffness_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W1 = decimal.Parse(row.Cells[p_stiffness_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W2 = decimal.Parse(row.Cells[p_stiffness_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W3 = decimal.Parse(row.Cells[p_stiffness_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F1 = decimal.Parse(row.Cells[p_stiffness_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F2 = decimal.Parse(row.Cells[p_stiffness_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F3 = decimal.Parse(row.Cells[p_stiffness_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TEAR
                                    if (p_tear_w1 != 0)
                                    {
                                        if (row.Cells[p_tear_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w1].ToString(), out value))
                                            {
                                                inst.TEAR_W1 = decimal.Parse(row.Cells[p_tear_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w2].ToString(), out value))
                                            {
                                                inst.TEAR_W2 = decimal.Parse(row.Cells[p_tear_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w3].ToString(), out value))
                                            {
                                                inst.TEAR_W3 = decimal.Parse(row.Cells[p_tear_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f1].ToString(), out value))
                                            {
                                                inst.TEAR_F1 = decimal.Parse(row.Cells[p_tear_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f2].ToString(), out value))
                                            {
                                                inst.TEAR_F2 = decimal.Parse(row.Cells[p_tear_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f3].ToString(), out value))
                                            {
                                                inst.TEAR_F3 = decimal.Parse(row.Cells[p_tear_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STATIC_AIR
                                    if (p_static_air1 != 0)
                                    {
                                        if (row.Cells[p_static_air1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air1].ToString(), out value))
                                            {
                                                inst.STATIC_AIR1 = decimal.Parse(row.Cells[p_static_air1].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air1]) != null)
                                                    inst.STATIC_AIR1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air1]).NumberValue.ToString());
                                            }

                                        }
                                        if (row.Cells[p_static_air2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air2].ToString(), out value))
                                            {
                                                inst.STATIC_AIR2 = decimal.Parse(row.Cells[p_static_air2].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air2]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_static_air3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air3].ToString(), out value))
                                            {
                                                inst.STATIC_AIR3 = decimal.Parse(row.Cells[p_static_air3].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air3]).NumberValue.ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region DYNAMIC_AIR
                                    if (p_dynamic_air1 != 0)
                                    {
                                        if (row.Cells[p_dynamic_air1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dynamic_air1].ToString(), out value))
                                            {
                                                inst.DYNAMIC_AIR1 = decimal.Parse(row.Cells[p_dynamic_air1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dynamic_air2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dynamic_air2].ToString(), out value))
                                            {
                                                inst.DYNAMIC_AIR2 = decimal.Parse(row.Cells[p_dynamic_air2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dynamic_air3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dynamic_air3].ToString(), out value))
                                            {
                                                inst.DYNAMIC_AIR3 = decimal.Parse(row.Cells[p_dynamic_air3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region EXPONENT
                                    if (p_exponent1 != 0)
                                    {
                                        if (row.Cells[p_exponent1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_exponent1].ToString(), out value))
                                            {
                                                inst.EXPONENT1 = decimal.Parse(row.Cells[p_exponent1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_exponent2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_exponent2].ToString(), out value))
                                            {
                                                inst.EXPONENT2 = decimal.Parse(row.Cells[p_exponent2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_exponent3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_exponent3].ToString(), out value))
                                            {
                                                inst.EXPONENT3 = decimal.Parse(row.Cells[p_exponent3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FLAMMABILITY
                                    if (p_flammability_w != 0)
                                    {
                                        if (row.Cells[p_flammability_w] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_w].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_W = decimal.Parse(row.Cells[p_flammability_w].ToString());
                                            }
                                        }
                                        if (row.Cells[p_flammability_f] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_f].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_F = decimal.Parse(row.Cells[p_flammability_f].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region DIMENSCHANGE
                                    if (p_dimenschange_w1 != 0)
                                    {
                                        if (row.Cells[p_dimenschange_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W1 = decimal.Parse(row.Cells[p_dimenschange_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W2 = decimal.Parse(row.Cells[p_dimenschange_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W3 = decimal.Parse(row.Cells[p_dimenschange_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F1 = decimal.Parse(row.Cells[p_dimenschange_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F2 = decimal.Parse(row.Cells[p_dimenschange_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F3 = decimal.Parse(row.Cells[p_dimenschange_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region BOW & SKEW

                                    if (p_bow1 != 0)
                                    {
                                        if (row.Cells[p_bow1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow1].ToString(), out value))
                                            {
                                                inst.BOW1 = decimal.Parse(row.Cells[p_bow1].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew1 != 0)
                                    {
                                        if (row.Cells[p_skew1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew1].ToString(), out value))
                                            {
                                                inst.SKEW1 = decimal.Parse(row.Cells[p_skew1].ToString());
                                            }
                                        }
                                    }

                                    if (p_entryby != 0)
                                    {
                                        inst.ENTEYBY = row.Cells[p_entryby].ToString();
                                    }

                                    if (p_approveby != 0)
                                    {
                                        inst.APPROVEBY = row.Cells[p_approveby].ToString();
                                    }

                                    if (p_bow2 != 0)
                                    {
                                        if (row.Cells[p_bow2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow2].ToString(), out value))
                                            {
                                                inst.BOW2 = decimal.Parse(row.Cells[p_bow2].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew2 != 0)
                                    {
                                        if (row.Cells[p_skew2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew2].ToString(), out value))
                                            {
                                                inst.SKEW2 = decimal.Parse(row.Cells[p_skew2].ToString());
                                            }
                                        }
                                    }

                                    if (p_bow3 != 0)
                                    {
                                        if (row.Cells[p_bow3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow3].ToString(), out value))
                                            {
                                                inst.BOW3 = decimal.Parse(row.Cells[p_bow3].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew3 != 0)
                                    {
                                        if (row.Cells[p_skew3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew3].ToString(), out value))
                                            {
                                                inst.SKEW3 = decimal.Parse(row.Cells[p_skew3].ToString());
                                            }
                                        }
                                    }


                                    #endregion

                                    if (inst.WEAVINGLOT.Count() >= 9)
                                    {
                                        results.Add(inst);
                                    }
                                    else
                                    {
                                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                    }
                                }
                                else
                                {
                                    #region List
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                            {
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel Lot No. = " + inst.WEAVINGLOT });
                                            }

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                            {
                                                break;
                                            }

                                            if (inst.WEAVINGLOT.Count() < 7)
                                            {
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel" });
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    //break;
                                }
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = ex.Message.ToString() });
                        }
                    }
                    else
                    {
                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Excel Head Cell < 88 Please check file excel" });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();

                return results;
            }

        }
        #endregion

        #endregion

        #region Save 2

        #region OpenExcel2
        private void OpenExcel2()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
                openFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string csv = string.Empty;
                csv = "Excel 97-2003 file (*.xls)|*.xls|Excel file (*.xlsx)|*.xlsx";

                openFileDialog1.Filter = csv;
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Multiselect = true;

                Nullable<bool> result = openFileDialog1.ShowDialog();

                if (result == true)
                {
                    List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

                    listView.ItemsSource = null;
                    listView.Items.Clear();

                    string newFileName = string.Empty;
                    //int RowNo = 1;

                    foreach (String file in openFileDialog1.FileNames)
                    {
                        newFileName = file;

                        if (!string.IsNullOrEmpty(newFileName))
                        {
                            try
                            {
                                results = new List<LAB_ImportExcel1>();

                                if (newFileName.Contains(".xlsx") == true)
                                {
                                    results = ExcelToLAB_ImportExcel2(newFileName);
                                }
                                else
                                {
                                    results = ExcelToLAB_ImportExcel297(newFileName);
                                }

                                #region Approve

                                if (results != null)
                                {
                                    if (results.Count > 0)
                                    {
                                        int RowNo2 = 0;
                                        int RowNoErr = 0;
                                        string Err = string.Empty;
                                        bool chkApprove = true;

                                        #region ตัวแปร

                                        string P_ITMCODE = string.Empty;
                                        string P_WEAVINGLOG = string.Empty;
                                        string P_FINISHINGLOT = string.Empty;
                                        DateTime? P_ENTRYDATE = null;
                                        string P_ENTRYBY = string.Empty;
                                        decimal? P_WIDTH = null;
                                        decimal? P_USEWIDTH1 = null;
                                        decimal? P_USEWIDTH2 = null;
                                        decimal? P_USEWIDTH3 = null;
                                        decimal? P_WIDTHSILICONE1 = null;
                                        decimal? P_WIDTHSILICONE2 = null;
                                        decimal? P_WIDTHSILICONE3 = null;
                                        decimal? P_NUMTHREADS_W1 = null;
                                        decimal? P_NUMTHREADS_W2 = null;
                                        decimal? P_NUMTHREADS_W3 = null;
                                        decimal? P_NUMTHREADS_F1 = null;
                                        decimal? P_NUMTHREADS_F2 = null;
                                        decimal? P_NUMTHREADS_F3 = null;
                                        decimal? P_TOTALWEIGHT1 = null;
                                        decimal? P_TOTALWEIGHT2 = null;
                                        decimal? P_TOTALWEIGHT3 = null;
                                        decimal? P_TOTALWEIGHT4 = null;
                                        decimal? P_TOTALWEIGHT5 = null;
                                        decimal? P_TOTALWEIGHT6 = null;
                                        decimal? P_UNCOATEDWEIGHT1 = null;
                                        decimal? P_UNCOATEDWEIGHT2 = null;
                                        decimal? P_UNCOATEDWEIGHT3 = null;
                                        decimal? P_UNCOATEDWEIGHT4 = null;
                                        decimal? P_UNCOATEDWEIGHT5 = null;
                                        decimal? P_UNCOATEDWEIGHT6 = null;
                                        decimal? P_COATWEIGHT1 = null;
                                        decimal? P_COATWEIGHT2 = null;
                                        decimal? P_COATWEIGHT3 = null;
                                        decimal? P_COATWEIGHT4 = null;
                                        decimal? P_COATWEIGHT5 = null;
                                        decimal? P_COATWEIGHT6 = null;
                                        decimal? P_THICKNESS1 = null;
                                        decimal? P_THICKNESS2 = null;
                                        decimal? P_THICKNESS3 = null;
                                        decimal? P_MAXFORCE_W1 = null;
                                        decimal? P_MAXFORCE_W2 = null;
                                        decimal? P_MAXFORCE_W3 = null;

                                        decimal? P_MAXFORCE_W4 = null;
                                        decimal? P_MAXFORCE_W5 = null;
                                        decimal? P_MAXFORCE_W6 = null;

                                        decimal? P_MAXFORCE_F1 = null;
                                        decimal? P_MAXFORCE_F2 = null;
                                        decimal? P_MAXFORCE_F3 = null;

                                        decimal? P_MAXFORCE_F4 = null;
                                        decimal? P_MAXFORCE_F5 = null;
                                        decimal? P_MAXFORCE_F6 = null;

                                        decimal? P_ELOGATION_W1 = null;
                                        decimal? P_ELOGATION_W2 = null;
                                        decimal? P_ELOGATION_W3 = null;

                                        decimal? P_ELOGATION_W4 = null;
                                        decimal? P_ELOGATION_W5 = null;
                                        decimal? P_ELOGATION_W6 = null;

                                        decimal? P_ELOGATION_F1 = null;
                                        decimal? P_ELOGATION_F2 = null;
                                        decimal? P_ELOGATION_F3 = null;

                                        decimal? P_ELOGATION_F4 = null;
                                        decimal? P_ELOGATION_F5 = null;
                                        decimal? P_ELOGATION_F6 = null;

                                        decimal? P_FLAMMABILITY_W = null;
                                        decimal? P_FLAMMABILITY_W2 = null;
                                        decimal? P_FLAMMABILITY_W3 = null;
                                        decimal? P_FLAMMABILITY_W4 = null;
                                        decimal? P_FLAMMABILITY_W5 = null;

                                        decimal? P_FLAMMABILITY_F = null;
                                        decimal? P_FLAMMABILITY_F2 = null;
                                        decimal? P_FLAMMABILITY_F3 = null;
                                        decimal? P_FLAMMABILITY_F4 = null;
                                        decimal? P_FLAMMABILITY_F5 = null;

                                        decimal? P_EDGECOMB_W1 = null;
                                        decimal? P_EDGECOMB_W2 = null;
                                        decimal? P_EDGECOMB_W3 = null;
                                        decimal? P_EDGECOMB_F1 = null;
                                        decimal? P_EDGECOMB_F2 = null;
                                        decimal? P_EDGECOMB_F3 = null;
                                        decimal? P_STIFFNESS_W1 = null;
                                        decimal? P_STIFFNESS_W2 = null;
                                        decimal? P_STIFFNESS_W3 = null;
                                        decimal? P_STIFFNESS_F1 = null;
                                        decimal? P_STIFFNESS_F2 = null;
                                        decimal? P_STIFFNESS_F3 = null;
                                        decimal? P_TEAR_W1 = null;
                                        decimal? P_TEAR_W2 = null;
                                        decimal? P_TEAR_W3 = null;
                                        decimal? P_TEAR_F1 = null;
                                        decimal? P_TEAR_F2 = null;
                                        decimal? P_TEAR_F3 = null;
                                        decimal? P_STATIC_AIR1 = null;
                                        decimal? P_STATIC_AIR2 = null;
                                        decimal? P_STATIC_AIR3 = null;

                                        decimal? P_STATIC_AIR4 = null;
                                        decimal? P_STATIC_AIR5 = null;
                                        decimal? P_STATIC_AIR6 = null;

                                        decimal? P_DYNAMIC_AIR1 = null;
                                        decimal? P_DYNAMIC_AIR2 = null;
                                        decimal? P_DYNAMIC_AIR3 = null;
                                        decimal? P_EXPONENT1 = null;
                                        decimal? P_EXPONENT2 = null;
                                        decimal? P_EXPONENT3 = null;
                                        decimal? P_DIMENSCHANGE_W1 = null;
                                        decimal? P_DIMENSCHANGE_W2 = null;
                                        decimal? P_DIMENSCHANGE_W3 = null;
                                        decimal? P_DIMENSCHANGE_F1 = null;
                                        decimal? P_DIMENSCHANGE_F2 = null;
                                        decimal? P_DIMENSCHANGE_F3 = null;
                                        decimal? P_FLEXABRASION_W1 = null;
                                        decimal? P_FLEXABRASION_W2 = null;
                                        decimal? P_FLEXABRASION_W3 = null;
                                        decimal? P_FLEXABRASION_F1 = null;
                                        decimal? P_FLEXABRASION_F2 = null;
                                        decimal? P_FLEXABRASION_F3 = null;

                                        string P_STATUS = string.Empty;
                                        string P_REMARK = string.Empty;
                                        string P_APPROVEBY = string.Empty;
                                        DateTime? P_APPROVEDATE = null;

                                        decimal? P_BOW1 = null;
                                        decimal? P_BOW2 = null;
                                        decimal? P_BOW3 = null;
                                        decimal? P_SKEW1 = null;
                                        decimal? P_SKEW2 = null;
                                        decimal? P_SKEW3 = null;
                                        decimal? P_BENDING_W1 = null;
                                        decimal? P_BENDING_W2 = null;
                                        decimal? P_BENDING_W3 = null;
                                        decimal? P_BENDING_F1 = null;
                                        decimal? P_BENDING_F2 = null;
                                        decimal? P_BENDING_F3 = null;
                                        decimal? P_FLEX_SCOTT_W1 = null;
                                        decimal? P_FLEX_SCOTT_W2 = null;
                                        decimal? P_FLEX_SCOTT_W3 = null;
                                        decimal? P_FLEX_SCOTT_F1 = null;
                                        decimal? P_FLEX_SCOTT_F2 = null;
                                        decimal? P_FLEX_SCOTT_F3 = null;

                                        #endregion

                                        P_STATUS = "Approve";
                                        P_APPROVEDATE = DateTime.Now;

                                        for (int i = 0; i <= results.Count - 1; i++)
                                        {
                                            chkApprove = true;

                                            #region ตัวแปร

                                            P_ITMCODE = string.Empty;
                                            P_WEAVINGLOG = string.Empty;
                                            P_FINISHINGLOT = string.Empty;
                                            P_ENTRYDATE = null;
                                            P_ENTRYBY = string.Empty;
                                            P_WIDTH = null;
                                            P_USEWIDTH1 = null;
                                            P_USEWIDTH2 = null;
                                            P_USEWIDTH3 = null;
                                            P_WIDTHSILICONE1 = null;
                                            P_WIDTHSILICONE2 = null;
                                            P_WIDTHSILICONE3 = null;
                                            P_NUMTHREADS_W1 = null;
                                            P_NUMTHREADS_W2 = null;
                                            P_NUMTHREADS_W3 = null;
                                            P_NUMTHREADS_F1 = null;
                                            P_NUMTHREADS_F2 = null;
                                            P_NUMTHREADS_F3 = null;
                                            P_TOTALWEIGHT1 = null;
                                            P_TOTALWEIGHT2 = null;
                                            P_TOTALWEIGHT3 = null;
                                            P_TOTALWEIGHT4 = null;
                                            P_TOTALWEIGHT5 = null;
                                            P_TOTALWEIGHT6 = null;
                                            P_UNCOATEDWEIGHT1 = null;
                                            P_UNCOATEDWEIGHT2 = null;
                                            P_UNCOATEDWEIGHT3 = null;
                                            P_UNCOATEDWEIGHT4 = null;
                                            P_UNCOATEDWEIGHT5 = null;
                                            P_UNCOATEDWEIGHT6 = null;
                                            P_COATWEIGHT1 = null;
                                            P_COATWEIGHT2 = null;
                                            P_COATWEIGHT3 = null;
                                            P_COATWEIGHT4 = null;
                                            P_COATWEIGHT5 = null;
                                            P_COATWEIGHT6 = null;
                                            P_THICKNESS1 = null;
                                            P_THICKNESS2 = null;
                                            P_THICKNESS3 = null;
                                            P_MAXFORCE_W1 = null;
                                            P_MAXFORCE_W2 = null;
                                            P_MAXFORCE_W3 = null;
                                            P_MAXFORCE_F1 = null;
                                            P_MAXFORCE_F2 = null;
                                            P_MAXFORCE_F3 = null;
                                            P_ELOGATION_W1 = null;
                                            P_ELOGATION_W2 = null;
                                            P_ELOGATION_W3 = null;
                                            P_ELOGATION_F1 = null;
                                            P_ELOGATION_F2 = null;
                                            P_ELOGATION_F3 = null;

                                            P_FLAMMABILITY_W = null;
                                            P_FLAMMABILITY_W2 = null;
                                            P_FLAMMABILITY_W3 = null;
                                            P_FLAMMABILITY_W4 = null;
                                            P_FLAMMABILITY_W5 = null;

                                            P_FLAMMABILITY_F = null;
                                            P_FLAMMABILITY_F2 = null;
                                            P_FLAMMABILITY_F3 = null;
                                            P_FLAMMABILITY_F4 = null;
                                            P_FLAMMABILITY_F5 = null;

                                            P_EDGECOMB_W1 = null;
                                            P_EDGECOMB_W2 = null;
                                            P_EDGECOMB_W3 = null;
                                            P_EDGECOMB_F1 = null;
                                            P_EDGECOMB_F2 = null;
                                            P_EDGECOMB_F3 = null;
                                            P_STIFFNESS_W1 = null;
                                            P_STIFFNESS_W2 = null;
                                            P_STIFFNESS_W3 = null;
                                            P_STIFFNESS_F1 = null;
                                            P_STIFFNESS_F2 = null;
                                            P_STIFFNESS_F3 = null;
                                            P_TEAR_W1 = null;
                                            P_TEAR_W2 = null;
                                            P_TEAR_W3 = null;
                                            P_TEAR_F1 = null;
                                            P_TEAR_F2 = null;
                                            P_TEAR_F3 = null;
                                            P_STATIC_AIR1 = null;
                                            P_STATIC_AIR2 = null;
                                            P_STATIC_AIR3 = null;

                                            P_STATIC_AIR4 = null;
                                            P_STATIC_AIR5 = null;
                                            P_STATIC_AIR6 = null;

                                            P_DYNAMIC_AIR1 = null;
                                            P_DYNAMIC_AIR2 = null;
                                            P_DYNAMIC_AIR3 = null;
                                            P_EXPONENT1 = null;
                                            P_EXPONENT2 = null;
                                            P_EXPONENT3 = null;
                                            P_DIMENSCHANGE_W1 = null;
                                            P_DIMENSCHANGE_W2 = null;
                                            P_DIMENSCHANGE_W3 = null;
                                            P_DIMENSCHANGE_F1 = null;
                                            P_DIMENSCHANGE_F2 = null;
                                            P_DIMENSCHANGE_F3 = null;
                                            P_FLEXABRASION_W1 = null;
                                            P_FLEXABRASION_W2 = null;
                                            P_FLEXABRASION_W3 = null;
                                            P_FLEXABRASION_F1 = null;
                                            P_FLEXABRASION_F2 = null;
                                            P_FLEXABRASION_F3 = null;

                                            P_REMARK = string.Empty;
                                            P_APPROVEBY = string.Empty;

                                            P_BOW1 = null;
                                            P_BOW2 = null;
                                            P_BOW3 = null;
                                            P_SKEW1 = null;
                                            P_SKEW2 = null;
                                            P_SKEW3 = null;
                                            P_BENDING_W1 = null;
                                            P_BENDING_W2 = null;
                                            P_BENDING_W3 = null;
                                            P_BENDING_F1 = null;
                                            P_BENDING_F2 = null;
                                            P_BENDING_F3 = null;
                                            P_FLEX_SCOTT_W1 = null;
                                            P_FLEX_SCOTT_W2 = null;
                                            P_FLEX_SCOTT_W3 = null;
                                            P_FLEX_SCOTT_F1 = null;
                                            P_FLEX_SCOTT_F2 = null;
                                            P_FLEX_SCOTT_F3 = null;

                                            #endregion

                                            P_ITMCODE = results[i].ITM_CODE;
                                            P_WEAVINGLOG = results[i].WEAVINGLOT;
                                            P_FINISHINGLOT = results[i].FINISHINGLOT;

                                            #region MAXFORCE
                                            P_MAXFORCE_W1 = results[i].MAXFORCE_W1;
                                            P_MAXFORCE_W2 = results[i].MAXFORCE_W2;
                                            P_MAXFORCE_W3 = results[i].MAXFORCE_W3;

                                            P_MAXFORCE_F1 = results[i].MAXFORCE_F1;
                                            P_MAXFORCE_F2 = results[i].MAXFORCE_F2;
                                            P_MAXFORCE_F3 = results[i].MAXFORCE_F3;

                                            #endregion

                                            #region ELOGATION
                                            P_ELOGATION_W1 = results[i].ELONGATIONFORCE_W1;
                                            P_ELOGATION_W2 = results[i].ELONGATIONFORCE_W2;
                                            P_ELOGATION_W3 = results[i].ELONGATIONFORCE_W3;

                                            P_ELOGATION_F1 = results[i].ELONGATIONFORCE_F1;
                                            P_ELOGATION_F2 = results[i].ELONGATIONFORCE_F2;
                                            P_ELOGATION_F3 = results[i].ELONGATIONFORCE_F3;

                                            #endregion

                                            #region EDGECOMB
                                            P_EDGECOMB_W1 = results[i].EDGECOMB_W1;
                                            P_EDGECOMB_W2 = results[i].EDGECOMB_W2;
                                            P_EDGECOMB_W3 = results[i].EDGECOMB_W3;
                                            P_EDGECOMB_F1 = results[i].EDGECOMB_F1;
                                            P_EDGECOMB_F2 = results[i].EDGECOMB_F2;
                                            P_EDGECOMB_F3 = results[i].EDGECOMB_F3;
                                            #endregion

                                            #region TEAR
                                            P_TEAR_W1 = results[i].TEAR_W1;
                                            P_TEAR_W2 = results[i].TEAR_W2;
                                            P_TEAR_W3 = results[i].TEAR_W3;
                                            P_TEAR_F1 = results[i].TEAR_F1;
                                            P_TEAR_F2 = results[i].TEAR_F2;
                                            P_TEAR_F3 = results[i].TEAR_F3;
                                            #endregion

                                            P_WIDTH = results[i].WIDTH;

                                            #region USEWIDTH
                                            P_USEWIDTH1 = results[i].USABLE_WIDTH1;
                                            P_USEWIDTH2 = results[i].USABLE_WIDTH2;
                                            P_USEWIDTH3 = results[i].USABLE_WIDTH3;
                                            #endregion

                                            #region WIDTHSILICONE
                                            //P_WIDTHSILICONE1 = results[i].WIDTHSILICONE1;
                                            //P_WIDTHSILICONE2 = results[i].WIDTHSILICONE2;
                                            //P_WIDTHSILICONE3 = results[i].WIDTHSILICONE3;
                                            #endregion

                                            #region NUMTHREADS
                                            P_NUMTHREADS_W1 = results[i].NUMTHREADS_W1;
                                            P_NUMTHREADS_W2 = results[i].NUMTHREADS_W2;
                                            P_NUMTHREADS_W3 = results[i].NUMTHREADS_W3;
                                            P_NUMTHREADS_F1 = results[i].NUMTHREADS_F1;
                                            P_NUMTHREADS_F2 = results[i].NUMTHREADS_F2;
                                            P_NUMTHREADS_F3 = results[i].NUMTHREADS_F3;
                                            #endregion

                                            #region TOTALWEIGHT
                                            P_TOTALWEIGHT1 = results[i].TOTALWEIGHT1;
                                            P_TOTALWEIGHT2 = results[i].TOTALWEIGHT2;
                                            P_TOTALWEIGHT3 = results[i].TOTALWEIGHT3;
                                            P_TOTALWEIGHT4 = results[i].TOTALWEIGHT4;
                                            P_TOTALWEIGHT5 = results[i].TOTALWEIGHT5;
                                            P_TOTALWEIGHT6 = results[i].TOTALWEIGHT6;
                                            #endregion

                                            #region UNCOATEDWEIGHT
                                            P_UNCOATEDWEIGHT1 = results[i].UNCOATEDWEIGHT1;
                                            P_UNCOATEDWEIGHT2 = results[i].UNCOATEDWEIGHT2;
                                            P_UNCOATEDWEIGHT3 = results[i].UNCOATEDWEIGHT3;
                                            P_UNCOATEDWEIGHT4 = results[i].UNCOATEDWEIGHT4;
                                            P_UNCOATEDWEIGHT5 = results[i].UNCOATEDWEIGHT5;
                                            P_UNCOATEDWEIGHT6 = results[i].UNCOATEDWEIGHT6;
                                            #endregion

                                            #region COATWEIGHT
                                            P_COATWEIGHT1 = results[i].COATWEIGHT1;
                                            P_COATWEIGHT2 = results[i].COATWEIGHT2;
                                            P_COATWEIGHT3 = results[i].COATWEIGHT3;
                                            P_COATWEIGHT4 = results[i].COATWEIGHT4;
                                            P_COATWEIGHT5 = results[i].COATWEIGHT5;
                                            P_COATWEIGHT6 = results[i].COATWEIGHT6;
                                            #endregion

                                            #region THICKNESS
                                            P_THICKNESS1 = results[i].THICKNESS1;
                                            P_THICKNESS2 = results[i].THICKNESS2;
                                            P_THICKNESS3 = results[i].THICKNESS3;
                                            #endregion

                                            P_FLAMMABILITY_W = results[i].FLAMMABILITY_W;
                                            P_FLAMMABILITY_W2 = results[i].FLAMMABILITY_W2;
                                            P_FLAMMABILITY_W3 = results[i].FLAMMABILITY_W3;
                                            P_FLAMMABILITY_W4 = results[i].FLAMMABILITY_W4;
                                            P_FLAMMABILITY_W5 = results[i].FLAMMABILITY_W5;

                                            P_FLAMMABILITY_F = results[i].FLAMMABILITY_F;
                                            P_FLAMMABILITY_F2 = results[i].FLAMMABILITY_F2;
                                            P_FLAMMABILITY_F3 = results[i].FLAMMABILITY_F3;
                                            P_FLAMMABILITY_F4 = results[i].FLAMMABILITY_F4;
                                            P_FLAMMABILITY_F5 = results[i].FLAMMABILITY_F5;

                                            #region STIFFNESS
                                            P_STIFFNESS_W1 = results[i].STIFFNESS_W1;
                                            P_STIFFNESS_W2 = results[i].STIFFNESS_W2;
                                            P_STIFFNESS_W3 = results[i].STIFFNESS_W3;
                                            P_STIFFNESS_F1 = results[i].STIFFNESS_F1;
                                            P_STIFFNESS_F2 = results[i].STIFFNESS_F2;
                                            P_STIFFNESS_F3 = results[i].STIFFNESS_F3;
                                            #endregion

                                            #region STATIC_AIR
                                            P_STATIC_AIR1 = results[i].STATIC_AIR1;
                                            P_STATIC_AIR2 = results[i].STATIC_AIR2;
                                            P_STATIC_AIR3 = results[i].STATIC_AIR3;

                                            //P_STATIC_AIR4 = results[i].STATIC_AIR4;
                                            //P_STATIC_AIR5 = results[i].STATIC_AIR5;
                                            //P_STATIC_AIR6 = results[i].STATIC_AIR6;

                                            #endregion

                                            #region DYNAMIC_AIR
                                            //P_DYNAMIC_AIR1 = results[i].DYNAMIC_AIR1;
                                            //P_DYNAMIC_AIR2 = results[i].DYNAMIC_AIR2;
                                            //P_DYNAMIC_AIR3 = results[i].DYNAMIC_AIR3;
                                            #endregion

                                            #region EXPONENT
                                            //P_EXPONENT1 = results[i].EXPONENT1;
                                            //P_EXPONENT2 = results[i].EXPONENT2;
                                            //P_EXPONENT3 = results[i].EXPONENT3;
                                            #endregion

                                            #region DIMENSCHANGE
                                            P_DIMENSCHANGE_W1 = results[i].DIMENSCHANGE_W1;
                                            P_DIMENSCHANGE_W2 = results[i].DIMENSCHANGE_W2;
                                            P_DIMENSCHANGE_W3 = results[i].DIMENSCHANGE_W3;
                                            P_DIMENSCHANGE_F1 = results[i].DIMENSCHANGE_F1;
                                            P_DIMENSCHANGE_F2 = results[i].DIMENSCHANGE_F2;
                                            P_DIMENSCHANGE_F3 = results[i].DIMENSCHANGE_F3;
                                            #endregion

                                            #region FLEXABRASION
                                            P_FLEXABRASION_W1 = results[i].FLEXABRASION_W1;
                                            //P_FLEXABRASION_W2 = results[i].FLEXABRASION_W2;
                                            //P_FLEXABRASION_W3 = results[i].FLEXABRASION_W3;
                                            P_FLEXABRASION_F1 = results[i].FLEXABRASION_F1;
                                            //P_FLEXABRASION_F2 = results[i].FLEXABRASION_F2;
                                            //P_FLEXABRASION_F3 = results[i].FLEXABRASION_F3;
                                            #endregion

                                            #region BOW
                                            P_BOW1 = results[i].BOW1;
                                            //P_BOW2 = results[i].BOW2;
                                            //P_BOW3 = results[i].BOW3;
                                            #endregion

                                            #region SKEW
                                            P_SKEW1 = results[i].SKEW1;
                                            //P_SKEW2 = results[i].SKEW2;
                                            //P_SKEW3 = results[i].SKEW3;
                                            #endregion

                                            #region BENDING
                                            //P_BENDING_W1 = results[i].BENDING_W1;
                                            //P_BENDING_W2 = results[i].BENDING_W2;
                                            //P_BENDING_W3 = results[i].BENDING_W3;
                                            //P_BENDING_F1 = results[i].BENDING_F1;
                                            //P_BENDING_F2 = results[i].BENDING_F2;
                                            //P_BENDING_F3 = results[i].BENDING_F3;
                                            #endregion

                                            #region FLEX_SCOTT
                                            //P_FLEX_SCOTT_W1 = results[i].FLEX_SCOTT_W1;
                                            //P_FLEX_SCOTT_W2 = results[i].FLEX_SCOTT_W2;
                                            //P_FLEX_SCOTT_W3 = results[i].FLEX_SCOTT_W3;
                                            //P_FLEX_SCOTT_F1 = results[i].FLEX_SCOTT_F1;
                                            //P_FLEX_SCOTT_F2 = results[i].FLEX_SCOTT_F2;
                                            //P_FLEX_SCOTT_F3 = results[i].FLEX_SCOTT_F3;
                                            #endregion

                                            P_APPROVEBY = results[i].APPROVEBY;
                                            P_ENTRYDATE = results[i].ENTRYDATE;
                                            P_ENTRYBY = results[i].ENTEYBY;

                                            if (!string.IsNullOrEmpty(P_ITMCODE) && !string.IsNullOrEmpty(P_WEAVINGLOG) && !string.IsNullOrEmpty(P_FINISHINGLOT))
                                            {
                                                string insert = LabDataPDFDataService.Instance.LAB_INSERTPRODUCTIONP(P_ITMCODE, P_WEAVINGLOG, P_FINISHINGLOT, P_ENTRYDATE, P_ENTRYBY,
                                                  P_WIDTH, P_USEWIDTH1, P_USEWIDTH2, P_USEWIDTH3, P_WIDTHSILICONE1, P_WIDTHSILICONE2, P_WIDTHSILICONE3,
                                                  P_NUMTHREADS_W1, P_NUMTHREADS_W2, P_NUMTHREADS_W3, P_NUMTHREADS_F1, P_NUMTHREADS_F2, P_NUMTHREADS_F3,
                                                  P_TOTALWEIGHT1, P_TOTALWEIGHT2, P_TOTALWEIGHT3, P_TOTALWEIGHT4, P_TOTALWEIGHT5, P_TOTALWEIGHT6,
                                                  P_UNCOATEDWEIGHT1, P_UNCOATEDWEIGHT2, P_UNCOATEDWEIGHT3, P_UNCOATEDWEIGHT4, P_UNCOATEDWEIGHT5, P_UNCOATEDWEIGHT6,
                                                  P_COATWEIGHT1, P_COATWEIGHT2, P_COATWEIGHT3, P_COATWEIGHT4, P_COATWEIGHT5, P_COATWEIGHT6,
                                                  P_THICKNESS1, P_THICKNESS2, P_THICKNESS3, 
                                                  P_MAXFORCE_W1, P_MAXFORCE_W2, P_MAXFORCE_W3,
                                                  P_MAXFORCE_W4, P_MAXFORCE_W5, P_MAXFORCE_W6,
                                                  P_MAXFORCE_F1, P_MAXFORCE_F2, P_MAXFORCE_F3,
                                                  P_MAXFORCE_F4, P_MAXFORCE_F5, P_MAXFORCE_F6,
                                                  P_ELOGATION_W1, P_ELOGATION_W2, P_ELOGATION_W3,
                                                  P_ELOGATION_W4, P_ELOGATION_W5, P_ELOGATION_W6,
                                                  P_ELOGATION_F1, P_ELOGATION_F2, P_ELOGATION_F3,
                                                  P_ELOGATION_F4, P_ELOGATION_F5, P_ELOGATION_F6,
                                                  P_FLAMMABILITY_W, P_FLAMMABILITY_W2, P_FLAMMABILITY_W3, P_FLAMMABILITY_W4, P_FLAMMABILITY_W5,
                                                  P_FLAMMABILITY_F, P_FLAMMABILITY_F2, P_FLAMMABILITY_F3, P_FLAMMABILITY_F4, P_FLAMMABILITY_F5,
                                                  P_EDGECOMB_W1, P_EDGECOMB_W2, P_EDGECOMB_W3, P_EDGECOMB_F1, P_EDGECOMB_F2, P_EDGECOMB_F3,
                                                  P_STIFFNESS_W1, P_STIFFNESS_W2, P_STIFFNESS_W3, P_STIFFNESS_F1, P_STIFFNESS_F2, P_STIFFNESS_F3,
                                                  P_TEAR_W1, P_TEAR_W2, P_TEAR_W3, P_TEAR_F1, P_TEAR_F2, P_TEAR_F3,
                                                  P_STATIC_AIR1, P_STATIC_AIR2, P_STATIC_AIR3, P_STATIC_AIR4, P_STATIC_AIR5, P_STATIC_AIR6,
                                                  P_DYNAMIC_AIR1, P_DYNAMIC_AIR2, P_DYNAMIC_AIR3,
                                                  P_EXPONENT1, P_EXPONENT2, P_EXPONENT3, P_DIMENSCHANGE_W1, P_DIMENSCHANGE_W2, P_DIMENSCHANGE_W3,
                                                  P_DIMENSCHANGE_F1, P_DIMENSCHANGE_F2, P_DIMENSCHANGE_F3, P_FLEXABRASION_W1, P_FLEXABRASION_W2, P_FLEXABRASION_W3,
                                                  P_FLEXABRASION_F1, P_FLEXABRASION_F2, P_FLEXABRASION_F3, P_STATUS, P_REMARK, P_APPROVEBY, P_APPROVEDATE,
                                                  P_BOW1, P_BOW2, P_BOW3, P_SKEW1, P_SKEW2, P_SKEW3,
                                                  P_BENDING_W1, P_BENDING_W2, P_BENDING_W3, P_BENDING_F1, P_BENDING_F2, P_BENDING_F3,
                                                  P_FLEX_SCOTT_W1, P_FLEX_SCOTT_W2, P_FLEX_SCOTT_W3, P_FLEX_SCOTT_F1, P_FLEX_SCOTT_F2, P_FLEX_SCOTT_F3);

                                                if (insert == "1")
                                                {
                                                    RowNo2++;
                                                }
                                                else
                                                {
                                                    if (!string.IsNullOrEmpty(Err))
                                                        Err += " , " + insert;
                                                    else
                                                        Err += insert;

                                                    RowNoErr++;
                                                }
                                            }
                                            else
                                                chkApprove = false;

                                        }

                                        if (chkApprove == true)
                                            this.listView.Items.Add(new CheckList { FILENAME = newFileName, SUMROW = RowNo2, SUMERR = RowNoErr, ERROR = Err });
                                    }
                                }

                                #endregion
                            }
                            catch (Exception ex)
                            {
                                ex.Message.Err();
                            }
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

        #region ExcelToLAB_ImportExcel2
        private List<LAB_ImportExcel1> ExcelToLAB_ImportExcel2(string filePath)
        {
            List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

            try
            {
                decimal value;

                XSSFWorkbook hssfworkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new XSSFWorkbook(file);
                }
                int s = hssfworkbook.ActiveSheetIndex;

                XSSFSheet sheet = (XSSFSheet)hssfworkbook.GetSheetAt(s);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                XSSFRow headerRow = (XSSFRow)sheet.GetRow(0);
                string itemcode = string.Empty;
                int startRow = 6;

                if (!string.IsNullOrEmpty(headerRow.Cells[0].ToString()))
                    itemcode = headerRow.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(headerRow.Cells[1].ToString()))
                    itemcode = headerRow.Cells[1].ToString();

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();

                if (!string.IsNullOrEmpty(itemcode))
                {
                    #region Def

                    int p_weavinglog = 0;
                    int p_finishinglot = 0;
                    int p_entrydate = 0;
                    int p_width = 0;
                    int p_usewidth1 = 0;
                    int p_usewidth2 = 0;
                    int p_usewidth3 = 0;
                    int p_totalweight1 = 0;
                    int p_totalweight2 = 0;
                    int p_totalweight3 = 0;
                    int p_totalweight4 = 0;
                    int p_totalweight5 = 0;
                    int p_totalweight6 = 0;
                    int p_uncoatedweight1 = 0;
                    int p_uncoatedweight2 = 0;
                    int p_uncoatedweight3 = 0;
                    int p_uncoatedweight4 = 0;
                    int p_uncoatedweight5 = 0;
                    int p_uncoatedweight6 = 0;
                    int p_coatweight1 = 0;
                    int p_coatweight2 = 0;
                    int p_coatweight3 = 0;
                    int p_coatweight4 = 0;
                    int p_coatweight5 = 0;
                    int p_coatweight6 = 0;
                    int p_thickness1 = 0;
                    int p_thickness2 = 0;
                    int p_thickness3 = 0;
                    int p_numthreads_w1 = 0;
                    int p_numthreads_w2 = 0;
                    int p_numthreads_w3 = 0;
                    int p_numthreads_f1 = 0;
                    int p_numthreads_f2 = 0;
                    int p_numthreads_f3 = 0;
                    int p_maxforce_w1 = 0;
                    int p_maxforce_w2 = 0;
                    int p_maxforce_w3 = 0;
                    int p_elogation_w1 = 0;
                    int p_elogation_w2 = 0;
                    int p_elogation_w3 = 0;
                    int p_maxforce_f1 = 0;
                    int p_maxforce_f2 = 0;
                    int p_maxforce_f3 = 0;
                    int p_elogation_f1 = 0;
                    int p_elogation_f2 = 0;
                    int p_elogation_f3 = 0;
                    int p_edgecomb_w1 = 0;
                    int p_edgecomb_w2 = 0;
                    int p_edgecomb_w3 = 0;
                    int p_edgecomb_f1 = 0;
                    int p_edgecomb_f2 = 0;
                    int p_edgecomb_f3 = 0;
                    int p_stiffness_w1 = 0;
                    int p_stiffness_w2 = 0;
                    int p_stiffness_w3 = 0;
                    int p_stiffness_f1 = 0;
                    int p_stiffness_f2 = 0;
                    int p_stiffness_f3 = 0;
                    int p_tear_w1 = 0;
                    int p_tear_w2 = 0;
                    int p_tear_w3 = 0;
                    int p_tear_f1 = 0;
                    int p_tear_f2 = 0;
                    int p_tear_f3 = 0;
                    int p_static_air1 = 0;
                    int p_static_air2 = 0;
                    int p_static_air3 = 0;
                  
                    int p_flammability_w = 0;
                    int p_flammability_f = 0;

                    int p_dimenschange_w1 = 0;
                    int p_dimenschange_w2 = 0;
                    int p_dimenschange_w3 = 0;
                    int p_dimenschange_f1 = 0;
                    int p_dimenschange_f2 = 0;
                    int p_dimenschange_f3 = 0;

                    int p_flexabrasion_w1 = 0;
                    int p_flexabrasion_f1 = 0;

                    int p_bow1 = 0;
                    int p_skew1 = 0;
                    
                    int p_entryby = 0;
                    int p_approveby = 0;

                    int chkCoulmn = 0;
                    int chknull = 0;
                    int chkWeavinglog = 0;

                    #endregion

                    XSSFRow row = (XSSFRow)sheet.GetRow(startRow);

                    if (row.Cells.Count >= 102)
                    {
                        // Set Head
                        #region Set Head

                        for (int j = 0; j < row.Cells.Count + 1; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (chkCoulmn == 1)
                                    p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 3)
                                    p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 5)
                                    p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 6)
                                    p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 7)
                                    p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 8)
                                    p_usewidth2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 9)
                                    p_usewidth3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 10)
                                    p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 11)
                                    p_totalweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 12)
                                    p_totalweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 13)
                                    p_uncoatedweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 14)
                                    p_uncoatedweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 15)
                                    p_uncoatedweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 16)
                                    p_coatweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 17)
                                    p_coatweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 18)
                                    p_coatweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 19)
                                    p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 20)
                                    p_thickness2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 21)
                                    p_thickness3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 28)
                                    p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 29)
                                    p_numthreads_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 30)
                                    p_numthreads_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 31)
                                    p_numthreads_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 32)
                                    p_numthreads_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 33)
                                    p_numthreads_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 34)
                                    p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 35)
                                    p_maxforce_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 36)
                                    p_maxforce_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 37)
                                    p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 38)
                                    p_elogation_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 39)
                                    p_elogation_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 40)
                                    p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 41)
                                    p_maxforce_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 42)
                                    p_maxforce_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 43)
                                    p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 44)
                                    p_elogation_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 45)
                                    p_elogation_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 46)
                                    p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 47)
                                    p_edgecomb_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 48)
                                    p_edgecomb_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 49)
                                    p_edgecomb_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 50)
                                    p_edgecomb_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 51)
                                    p_edgecomb_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 52)
                                    p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 53)
                                    p_stiffness_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 54)
                                    p_stiffness_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 55)
                                    p_stiffness_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 56)
                                    p_stiffness_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 57)
                                    p_stiffness_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 58)
                                    p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 59)
                                    p_tear_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 60)
                                    p_tear_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 61)
                                    p_tear_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 62)
                                    p_tear_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 63)
                                    p_tear_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 64)
                                    p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 65)
                                    p_static_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 66)
                                    p_static_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 73)
                                    p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 74)
                                    p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 75)
                                    p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 76)
                                    p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 77)
                                    p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 78)
                                    p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 79)
                                    p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 80)
                                    p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 81)
                                    p_totalweight4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 82)
                                    p_totalweight5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 83)
                                    p_totalweight6 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 84)
                                    p_uncoatedweight4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 85)
                                    p_uncoatedweight5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 86)
                                    p_uncoatedweight6 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 87)
                                    p_coatweight4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 88)
                                    p_coatweight5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 89)
                                    p_coatweight6 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 90)
                                    p_flexabrasion_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 91)
                                    p_flexabrasion_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 92)
                                    p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 93)
                                    p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 101)
                                    p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 102)
                                    p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                chkCoulmn++;

                                if (p_approveby != 0)
                                {
                                    if (chknull >= 0 && chknull <= 2)
                                    {
                                        chknull++;
                                    }
                                    else
                                        break;
                                }
                            }
                        }

                        #endregion

                        try
                        {
                            #region Add Detail

                            for (int i = (startRow); i <= sheet.LastRowNum; i++)
                            {
                                LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                                row = (XSSFRow)sheet.GetRow(i);

                                if (row.Cells.Count() >= 102)
                                {
                                    inst.ITM_CODE = itemcode;

                                    #region WEAVINGLOT
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                                break;

                                            if (inst.WEAVINGLOT.Count() < 9)
                                            {
                                                chkWeavinglog++;
                                            }

                                            if (chkWeavinglog >= 3)
                                            {
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FINISHINGLOT
                                    if (row.Cells[p_finishinglot] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_finishinglot].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_finishinglot != 0)
                                                inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                                            if (string.IsNullOrEmpty(inst.FINISHINGLOT))
                                                break;
                                        }
                                    }
                                    #endregion

                                    if (p_entrydate != 0)
                                    {
                                        if (row.Cells[p_entrydate] != null)
                                            inst.ENTRYDATE = DateTime.Parse(row.Cells[p_entrydate].ToString());
                                    }

                                    #region WIDTH
                                    if (p_width != 0)
                                    {
                                        if (row.Cells[p_width] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_width].ToString(), out value))
                                            {
                                                inst.WIDTH = decimal.Parse(row.Cells[p_width].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region USABLE_WIDTH
                                    if (p_usewidth1 != 0)
                                    {
                                        if (row.Cells[p_usewidth1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth1].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH1 = decimal.Parse(row.Cells[p_usewidth1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth2].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH2 = decimal.Parse(row.Cells[p_usewidth2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth3].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH3 = decimal.Parse(row.Cells[p_usewidth3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TOTALWEIGHT
                                    if (p_totalweight1 != 0)
                                    {
                                        if (row.Cells[p_totalweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight1].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT1 = decimal.Parse(row.Cells[p_totalweight1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight2].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT2 = decimal.Parse(row.Cells[p_totalweight2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight3].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT3 = decimal.Parse(row.Cells[p_totalweight3].ToString());
                                            }
                                        }

                                        if (row.Cells[p_totalweight4] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight4].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT4 = decimal.Parse(row.Cells[p_totalweight4].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight5] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight5].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT5 = decimal.Parse(row.Cells[p_totalweight5].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight6] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight6].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT6 = decimal.Parse(row.Cells[p_totalweight6].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region UNCOATEDWEIGHT
                                    if (p_uncoatedweight1 != 0)
                                    {
                                        if (row.Cells[p_uncoatedweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight1].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT1 = decimal.Parse(row.Cells[p_uncoatedweight1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight2].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT2 = decimal.Parse(row.Cells[p_uncoatedweight2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight3].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT3 = decimal.Parse(row.Cells[p_uncoatedweight3].ToString());
                                            }
                                        }

                                        if (row.Cells[p_uncoatedweight4] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight4].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT4 = decimal.Parse(row.Cells[p_uncoatedweight4].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight5] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight5].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT5 = decimal.Parse(row.Cells[p_uncoatedweight5].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight6] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight6].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT6 = decimal.Parse(row.Cells[p_uncoatedweight6].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region COATWEIGHT
                                    if (p_coatweight1 != 0)
                                    {
                                        if (row.Cells[p_coatweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight1].ToString(), out value))
                                            {
                                                inst.COATWEIGHT1 = decimal.Parse(row.Cells[p_coatweight1].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight1]) != null)
                                                    inst.COATWEIGHT1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight1]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight2].ToString(), out value))
                                            {
                                                inst.COATWEIGHT2 = decimal.Parse(row.Cells[p_coatweight2].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight2]) != null)
                                                    inst.COATWEIGHT2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight2]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight3].ToString(), out value))
                                            {
                                                inst.COATWEIGHT3 = decimal.Parse(row.Cells[p_coatweight3].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight3]) != null)
                                                    inst.COATWEIGHT3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight3]).NumberValue.ToString());
                                            }
                                        }

                                        if (row.Cells[p_coatweight4] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight4].ToString(), out value))
                                            {
                                                inst.COATWEIGHT4 = decimal.Parse(row.Cells[p_coatweight4].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight4]) != null)
                                                    inst.COATWEIGHT4 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight4]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight5] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight5].ToString(), out value))
                                            {
                                                inst.COATWEIGHT5 = decimal.Parse(row.Cells[p_coatweight5].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight5]) != null)
                                                    inst.COATWEIGHT5 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight5]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight6] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight6].ToString(), out value))
                                            {
                                                inst.COATWEIGHT6 = decimal.Parse(row.Cells[p_coatweight6].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight6]) != null)
                                                    inst.COATWEIGHT6 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight6]).NumberValue.ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region THICKNESS
                                    if (p_thickness1 != 0)
                                    {
                                        if (row.Cells[p_thickness1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness1].ToString(), out value))
                                            {
                                                inst.THICKNESS1 = decimal.Parse(row.Cells[p_thickness1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness2].ToString(), out value))
                                            {
                                                inst.THICKNESS2 = decimal.Parse(row.Cells[p_thickness2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness3].ToString(), out value))
                                            {
                                                inst.THICKNESS3 = decimal.Parse(row.Cells[p_thickness3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region NUMTHREADS
                                    if (p_numthreads_w1 != 0)
                                    {
                                        if (row.Cells[p_numthreads_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W1 = decimal.Parse(row.Cells[p_numthreads_w1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W2 = decimal.Parse(row.Cells[p_numthreads_w2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W3 = decimal.Parse(row.Cells[p_numthreads_w3].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F1 = decimal.Parse(row.Cells[p_numthreads_f1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F2 = decimal.Parse(row.Cells[p_numthreads_f2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F3 = decimal.Parse(row.Cells[p_numthreads_f3].ToString()) * (10);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region MAXFORCE
                                    if (p_maxforce_w1 != 0)
                                    {
                                        if (row.Cells[p_maxforce_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W1 = decimal.Parse(row.Cells[p_maxforce_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W2 = decimal.Parse(row.Cells[p_maxforce_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W3 = decimal.Parse(row.Cells[p_maxforce_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F1 = decimal.Parse(row.Cells[p_maxforce_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F2 = decimal.Parse(row.Cells[p_maxforce_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F3 = decimal.Parse(row.Cells[p_maxforce_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region ELONGATIONFORCE
                                    if (p_elogation_w1 != 0)
                                    {
                                        if (row.Cells[p_elogation_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W1 = decimal.Parse(row.Cells[p_elogation_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W2 = decimal.Parse(row.Cells[p_elogation_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W3 = decimal.Parse(row.Cells[p_elogation_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F1 = decimal.Parse(row.Cells[p_elogation_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F2 = decimal.Parse(row.Cells[p_elogation_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F3 = decimal.Parse(row.Cells[p_elogation_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region EDGECOMB
                                    if (p_edgecomb_w1 != 0)
                                    {
                                        if (row.Cells[p_edgecomb_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W1 = decimal.Parse(row.Cells[p_edgecomb_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W2 = decimal.Parse(row.Cells[p_edgecomb_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W3 = decimal.Parse(row.Cells[p_edgecomb_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F1 = decimal.Parse(row.Cells[p_edgecomb_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F2 = decimal.Parse(row.Cells[p_edgecomb_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F3 = decimal.Parse(row.Cells[p_edgecomb_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STIFFNESS
                                    if (p_stiffness_w1 != 0)
                                    {
                                        if (row.Cells[p_stiffness_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W1 = decimal.Parse(row.Cells[p_stiffness_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W2 = decimal.Parse(row.Cells[p_stiffness_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W3 = decimal.Parse(row.Cells[p_stiffness_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F1 = decimal.Parse(row.Cells[p_stiffness_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F2 = decimal.Parse(row.Cells[p_stiffness_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F3 = decimal.Parse(row.Cells[p_stiffness_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TEAR
                                    if (p_tear_w1 != 0)
                                    {
                                        if (row.Cells[p_tear_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w1].ToString(), out value))
                                            {
                                                inst.TEAR_W1 = decimal.Parse(row.Cells[p_tear_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w2].ToString(), out value))
                                            {
                                                inst.TEAR_W2 = decimal.Parse(row.Cells[p_tear_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w3].ToString(), out value))
                                            {
                                                inst.TEAR_W3 = decimal.Parse(row.Cells[p_tear_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f1].ToString(), out value))
                                            {
                                                inst.TEAR_F1 = decimal.Parse(row.Cells[p_tear_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f2].ToString(), out value))
                                            {
                                                inst.TEAR_F2 = decimal.Parse(row.Cells[p_tear_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f3].ToString(), out value))
                                            {
                                                inst.TEAR_F3 = decimal.Parse(row.Cells[p_tear_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STATIC_AIR
                                    if (p_static_air1 != 0)
                                    {
                                        if (row.Cells[p_static_air1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air1].ToString(), out value))
                                            {
                                                inst.STATIC_AIR1 = decimal.Parse(row.Cells[p_static_air1].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air1]) != null)
                                                    inst.STATIC_AIR1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air1]).NumberValue.ToString());
                                            }

                                        }
                                        if (row.Cells[p_static_air2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air2].ToString(), out value))
                                            {
                                                inst.STATIC_AIR2 = decimal.Parse(row.Cells[p_static_air2].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air2]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_static_air3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air3].ToString(), out value))
                                            {
                                                inst.STATIC_AIR3 = decimal.Parse(row.Cells[p_static_air3].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air3]).NumberValue.ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FLAMMABILITY
                                    if (p_flammability_w != 0)
                                    {
                                        if (row.Cells[p_flammability_w] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_w].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_W = decimal.Parse(row.Cells[p_flammability_w].ToString());
                                            }
                                        }
                                        if (row.Cells[p_flammability_f] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_f].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_F = decimal.Parse(row.Cells[p_flammability_f].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region DIMENSCHANGE
                                    if (p_dimenschange_w1 != 0)
                                    {
                                        if (row.Cells[p_dimenschange_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W1 = decimal.Parse(row.Cells[p_dimenschange_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W2 = decimal.Parse(row.Cells[p_dimenschange_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W3 = decimal.Parse(row.Cells[p_dimenschange_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F1 = decimal.Parse(row.Cells[p_dimenschange_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F2 = decimal.Parse(row.Cells[p_dimenschange_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F3 = decimal.Parse(row.Cells[p_dimenschange_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FLAMMABILITY
                                    if (p_flexabrasion_w1 != 0)
                                    {
                                        if (row.Cells[p_flexabrasion_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flexabrasion_w1].ToString(), out value))
                                            {
                                                inst.FLEXABRASION_W1 = decimal.Parse(row.Cells[p_flexabrasion_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_flexabrasion_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flexabrasion_f1].ToString(), out value))
                                            {
                                                inst.FLEXABRASION_F1 = decimal.Parse(row.Cells[p_flexabrasion_f1].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region BOW & SKEW

                                    if (p_bow1 != 0)
                                    {
                                        if (row.Cells[p_bow1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow1].ToString(), out value))
                                            {
                                                inst.BOW1 = decimal.Parse(row.Cells[p_bow1].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew1 != 0)
                                    {
                                        if (row.Cells[p_skew1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew1].ToString(), out value))
                                            {
                                                inst.SKEW1 = decimal.Parse(row.Cells[p_skew1].ToString());
                                            }
                                        }
                                    }

                                    if (p_entryby != 0)
                                    {
                                        inst.ENTEYBY = row.Cells[p_entryby].ToString();
                                    }

                                    if (p_approveby != 0)
                                    {
                                        inst.APPROVEBY = row.Cells[p_approveby].ToString();
                                    }

                                    #endregion

                                    if (inst.WEAVINGLOT.Count() >= 9)
                                    {
                                        results.Add(inst);
                                    }
                                    else
                                    {
                                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                    }
                                }
                                else
                                {
                                    #region List
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                            {
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel Lot No. = " + inst.WEAVINGLOT });
                                            }

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                            {
                                                break;
                                            }

                                            //if (inst.WEAVINGLOT.Count() < 7)
                                            //{
                                            //    this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel" });
                                            //    break;
                                            //}
                                        }
                                    }
                                    #endregion
                                }
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = ex.Message.ToString() });
                        }
                    }
                    else
                    {
                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Excel Head Cell < 102 Please check file excel" });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();

                return results;
            }
        }

        #endregion

        #region ExcelToLAB_ImportExcel297
        private List<LAB_ImportExcel1> ExcelToLAB_ImportExcel297(string filePath)
        {
            List<LAB_ImportExcel1> results = new List<LAB_ImportExcel1>();

            try
            {

                decimal value;

                HSSFWorkbook hssfworkbook;
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }

                int s = hssfworkbook.ActiveSheetIndex;

                ISheet sheet = hssfworkbook.GetSheetAt(s);
                IRow row = sheet.GetRow(0);

                string itemcode = string.Empty;
                int startRow = 6;

                if (!string.IsNullOrEmpty(row.Cells[0].ToString()))
                    itemcode = row.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(row.Cells[1].ToString()))
                    itemcode = row.Cells[1].ToString();

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();

                if (!string.IsNullOrEmpty(itemcode))
                {
                    #region Def

                    int p_weavinglog = 0;
                    int p_finishinglot = 0;
                    int p_entrydate = 0;
                    int p_width = 0;
                    int p_usewidth1 = 0;
                    int p_usewidth2 = 0;
                    int p_usewidth3 = 0;
                    int p_totalweight1 = 0;
                    int p_totalweight2 = 0;
                    int p_totalweight3 = 0;
                    int p_totalweight4 = 0;
                    int p_totalweight5 = 0;
                    int p_totalweight6 = 0;
                    int p_uncoatedweight1 = 0;
                    int p_uncoatedweight2 = 0;
                    int p_uncoatedweight3 = 0;
                    int p_uncoatedweight4 = 0;
                    int p_uncoatedweight5 = 0;
                    int p_uncoatedweight6 = 0;
                    int p_coatweight1 = 0;
                    int p_coatweight2 = 0;
                    int p_coatweight3 = 0;
                    int p_coatweight4 = 0;
                    int p_coatweight5 = 0;
                    int p_coatweight6 = 0;
                    int p_thickness1 = 0;
                    int p_thickness2 = 0;
                    int p_thickness3 = 0;
                    int p_numthreads_w1 = 0;
                    int p_numthreads_w2 = 0;
                    int p_numthreads_w3 = 0;
                    int p_numthreads_f1 = 0;
                    int p_numthreads_f2 = 0;
                    int p_numthreads_f3 = 0;
                    int p_maxforce_w1 = 0;
                    int p_maxforce_w2 = 0;
                    int p_maxforce_w3 = 0;
                    int p_elogation_w1 = 0;
                    int p_elogation_w2 = 0;
                    int p_elogation_w3 = 0;
                    int p_maxforce_f1 = 0;
                    int p_maxforce_f2 = 0;
                    int p_maxforce_f3 = 0;
                    int p_elogation_f1 = 0;
                    int p_elogation_f2 = 0;
                    int p_elogation_f3 = 0;
                    int p_edgecomb_w1 = 0;
                    int p_edgecomb_w2 = 0;
                    int p_edgecomb_w3 = 0;
                    int p_edgecomb_f1 = 0;
                    int p_edgecomb_f2 = 0;
                    int p_edgecomb_f3 = 0;
                    int p_stiffness_w1 = 0;
                    int p_stiffness_w2 = 0;
                    int p_stiffness_w3 = 0;
                    int p_stiffness_f1 = 0;
                    int p_stiffness_f2 = 0;
                    int p_stiffness_f3 = 0;
                    int p_tear_w1 = 0;
                    int p_tear_w2 = 0;
                    int p_tear_w3 = 0;
                    int p_tear_f1 = 0;
                    int p_tear_f2 = 0;
                    int p_tear_f3 = 0;
                    int p_static_air1 = 0;
                    int p_static_air2 = 0;
                    int p_static_air3 = 0;

                    int p_flammability_w = 0;
                    int p_flammability_f = 0;

                    int p_dimenschange_w1 = 0;
                    int p_dimenschange_w2 = 0;
                    int p_dimenschange_w3 = 0;
                    int p_dimenschange_f1 = 0;
                    int p_dimenschange_f2 = 0;
                    int p_dimenschange_f3 = 0;

                    int p_flexabrasion_w1 = 0;
                    int p_flexabrasion_f1 = 0;

                    int p_bow1 = 0;
                    int p_skew1 = 0;

                    int p_entryby = 0;
                    int p_approveby = 0;

                    int chkCoulmn = 0;
                    int chknull = 0;
                    int chkWeavinglog = 0;

                    #endregion

                    row = sheet.GetRow(startRow);

                    if (row.Cells.Count >= 102)
                    {
                        // Set Head
                        #region Set Head

                        for (int j = 0; j < row.Cells.Count + 1; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (chkCoulmn == 1)
                                    p_weavinglog = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 3)
                                    p_finishinglot = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 5)
                                    p_entrydate = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 6)
                                    p_width = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 7)
                                    p_usewidth1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 8)
                                    p_usewidth2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 9)
                                    p_usewidth3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 10)
                                    p_totalweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 11)
                                    p_totalweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 12)
                                    p_totalweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 13)
                                    p_uncoatedweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 14)
                                    p_uncoatedweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 15)
                                    p_uncoatedweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 16)
                                    p_coatweight1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 17)
                                    p_coatweight2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 18)
                                    p_coatweight3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 19)
                                    p_thickness1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 20)
                                    p_thickness2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 21)
                                    p_thickness3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 28)
                                    p_numthreads_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 29)
                                    p_numthreads_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 30)
                                    p_numthreads_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 31)
                                    p_numthreads_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 32)
                                    p_numthreads_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 33)
                                    p_numthreads_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 34)
                                    p_maxforce_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 35)
                                    p_maxforce_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 36)
                                    p_maxforce_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 37)
                                    p_elogation_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 38)
                                    p_elogation_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 39)
                                    p_elogation_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 40)
                                    p_maxforce_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 41)
                                    p_maxforce_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 42)
                                    p_maxforce_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 43)
                                    p_elogation_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 44)
                                    p_elogation_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 45)
                                    p_elogation_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 46)
                                    p_edgecomb_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 47)
                                    p_edgecomb_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 48)
                                    p_edgecomb_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 49)
                                    p_edgecomb_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 50)
                                    p_edgecomb_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 51)
                                    p_edgecomb_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 52)
                                    p_stiffness_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 53)
                                    p_stiffness_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 54)
                                    p_stiffness_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 55)
                                    p_stiffness_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 56)
                                    p_stiffness_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 57)
                                    p_stiffness_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 58)
                                    p_tear_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 59)
                                    p_tear_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 60)
                                    p_tear_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 61)
                                    p_tear_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 62)
                                    p_tear_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 63)
                                    p_tear_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 64)
                                    p_static_air1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 65)
                                    p_static_air2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 66)
                                    p_static_air3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 73)
                                    p_flammability_w = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 74)
                                    p_flammability_f = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 75)
                                    p_dimenschange_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 76)
                                    p_dimenschange_w2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 77)
                                    p_dimenschange_w3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 78)
                                    p_dimenschange_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 79)
                                    p_dimenschange_f2 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 80)
                                    p_dimenschange_f3 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 81)
                                    p_totalweight4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 82)
                                    p_totalweight5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 83)
                                    p_totalweight6 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 84)
                                    p_uncoatedweight4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 85)
                                    p_uncoatedweight5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 86)
                                    p_uncoatedweight6 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 87)
                                    p_coatweight4 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 88)
                                    p_coatweight5 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 89)
                                    p_coatweight6 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 90)
                                    p_flexabrasion_w1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 91)
                                    p_flexabrasion_f1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 92)
                                    p_bow1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 93)
                                    p_skew1 = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                if (chkCoulmn == 101)
                                    p_entryby = int.Parse(row.GetCell(j).ColumnIndex.ToString());
                                if (chkCoulmn == 102)
                                    p_approveby = int.Parse(row.GetCell(j).ColumnIndex.ToString());

                                chkCoulmn++;

                                if (p_approveby != 0)
                                {
                                    if (chknull >= 0 && chknull <= 2)
                                    {
                                        chknull++;
                                    }
                                    else
                                        break;
                                }
                            }
                        }

                        #endregion

                        try
                        {
                            #region Add Detail

                            for (int i = (startRow); i <= sheet.LastRowNum; i++)
                            {
                                LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                                row = sheet.GetRow(i);

                                if (row.Cells.Count() >= 102)
                                {
                                    inst.ITM_CODE = itemcode;

                                    #region WEAVINGLOT
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                                break;

                                            if (inst.WEAVINGLOT.Count() < 9)
                                            {
                                                chkWeavinglog++;
                                            }

                                            if (chkWeavinglog >= 3)
                                            {
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                                break;
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FINISHINGLOT
                                    if (row.Cells[p_finishinglot] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_finishinglot].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_finishinglot != 0)
                                                inst.FINISHINGLOT = row.Cells[p_finishinglot].ToString();

                                            if (string.IsNullOrEmpty(inst.FINISHINGLOT))
                                                break;
                                        }
                                    }
                                    #endregion

                                    if (p_entrydate != 0)
                                    {
                                        if (row.Cells[p_entrydate] != null)
                                            inst.ENTRYDATE = DateTime.Parse(row.Cells[p_entrydate].ToString());
                                    }

                                    #region WIDTH
                                    if (p_width != 0)
                                    {
                                        if (row.Cells[p_width] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_width].ToString(), out value))
                                            {
                                                inst.WIDTH = decimal.Parse(row.Cells[p_width].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region USABLE_WIDTH
                                    if (p_usewidth1 != 0)
                                    {
                                        if (row.Cells[p_usewidth1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth1].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH1 = decimal.Parse(row.Cells[p_usewidth1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth2].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH2 = decimal.Parse(row.Cells[p_usewidth2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_usewidth3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_usewidth3].ToString(), out value))
                                            {
                                                inst.USABLE_WIDTH3 = decimal.Parse(row.Cells[p_usewidth3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TOTALWEIGHT
                                    if (p_totalweight1 != 0)
                                    {
                                        if (row.Cells[p_totalweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight1].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT1 = decimal.Parse(row.Cells[p_totalweight1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight2].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT2 = decimal.Parse(row.Cells[p_totalweight2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight3].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT3 = decimal.Parse(row.Cells[p_totalweight3].ToString());
                                            }
                                        }

                                        if (row.Cells[p_totalweight4] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight4].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT4 = decimal.Parse(row.Cells[p_totalweight4].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight5] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight5].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT5 = decimal.Parse(row.Cells[p_totalweight5].ToString());
                                            }
                                        }
                                        if (row.Cells[p_totalweight6] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_totalweight6].ToString(), out value))
                                            {
                                                inst.TOTALWEIGHT6 = decimal.Parse(row.Cells[p_totalweight6].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region UNCOATEDWEIGHT
                                    if (p_uncoatedweight1 != 0)
                                    {
                                        if (row.Cells[p_uncoatedweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight1].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT1 = decimal.Parse(row.Cells[p_uncoatedweight1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight2].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT2 = decimal.Parse(row.Cells[p_uncoatedweight2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight3].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT3 = decimal.Parse(row.Cells[p_uncoatedweight3].ToString());
                                            }
                                        }

                                        if (row.Cells[p_uncoatedweight4] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight4].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT4 = decimal.Parse(row.Cells[p_uncoatedweight4].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight5] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight5].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT5 = decimal.Parse(row.Cells[p_uncoatedweight5].ToString());
                                            }
                                        }
                                        if (row.Cells[p_uncoatedweight6] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_uncoatedweight6].ToString(), out value))
                                            {
                                                inst.UNCOATEDWEIGHT6 = decimal.Parse(row.Cells[p_uncoatedweight6].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region COATWEIGHT
                                    if (p_coatweight1 != 0)
                                    {
                                        if (row.Cells[p_coatweight1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight1].ToString(), out value))
                                            {
                                                inst.COATWEIGHT1 = decimal.Parse(row.Cells[p_coatweight1].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight1]) != null)
                                                    inst.COATWEIGHT1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight1]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight2].ToString(), out value))
                                            {
                                                inst.COATWEIGHT2 = decimal.Parse(row.Cells[p_coatweight2].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight2]) != null)
                                                    inst.COATWEIGHT2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight2]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight3].ToString(), out value))
                                            {
                                                inst.COATWEIGHT3 = decimal.Parse(row.Cells[p_coatweight3].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight3]) != null)
                                                    inst.COATWEIGHT3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight3]).NumberValue.ToString());
                                            }
                                        }

                                        if (row.Cells[p_coatweight4] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight4].ToString(), out value))
                                            {
                                                inst.COATWEIGHT4 = decimal.Parse(row.Cells[p_coatweight4].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight4]) != null)
                                                    inst.COATWEIGHT4 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight4]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight5] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight5].ToString(), out value))
                                            {
                                                inst.COATWEIGHT5 = decimal.Parse(row.Cells[p_coatweight5].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight5]) != null)
                                                    inst.COATWEIGHT5 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight5]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_coatweight6] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_coatweight6].ToString(), out value))
                                            {
                                                inst.COATWEIGHT6 = decimal.Parse(row.Cells[p_coatweight6].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_coatweight6]) != null)
                                                    inst.COATWEIGHT6 = decimal.Parse(evaluator.Evaluate(row.Cells[p_coatweight6]).NumberValue.ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region THICKNESS
                                    if (p_thickness1 != 0)
                                    {
                                        if (row.Cells[p_thickness1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness1].ToString(), out value))
                                            {
                                                inst.THICKNESS1 = decimal.Parse(row.Cells[p_thickness1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness2].ToString(), out value))
                                            {
                                                inst.THICKNESS2 = decimal.Parse(row.Cells[p_thickness2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_thickness3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_thickness3].ToString(), out value))
                                            {
                                                inst.THICKNESS3 = decimal.Parse(row.Cells[p_thickness3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region NUMTHREADS
                                    if (p_numthreads_w1 != 0)
                                    {
                                        if (row.Cells[p_numthreads_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W1 = decimal.Parse(row.Cells[p_numthreads_w1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W2 = decimal.Parse(row.Cells[p_numthreads_w2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_w3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_W3 = decimal.Parse(row.Cells[p_numthreads_w3].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f1].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F1 = decimal.Parse(row.Cells[p_numthreads_f1].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f2].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F2 = decimal.Parse(row.Cells[p_numthreads_f2].ToString()) * (10);
                                            }
                                        }
                                        if (row.Cells[p_numthreads_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_numthreads_f3].ToString(), out value))
                                            {
                                                inst.NUMTHREADS_F3 = decimal.Parse(row.Cells[p_numthreads_f3].ToString()) * (10);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region MAXFORCE
                                    if (p_maxforce_w1 != 0)
                                    {
                                        if (row.Cells[p_maxforce_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W1 = decimal.Parse(row.Cells[p_maxforce_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W2 = decimal.Parse(row.Cells[p_maxforce_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_w3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_W3 = decimal.Parse(row.Cells[p_maxforce_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f1].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F1 = decimal.Parse(row.Cells[p_maxforce_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f2].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F2 = decimal.Parse(row.Cells[p_maxforce_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_maxforce_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_maxforce_f3].ToString(), out value))
                                            {
                                                inst.MAXFORCE_F3 = decimal.Parse(row.Cells[p_maxforce_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region ELONGATIONFORCE
                                    if (p_elogation_w1 != 0)
                                    {
                                        if (row.Cells[p_elogation_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W1 = decimal.Parse(row.Cells[p_elogation_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W2 = decimal.Parse(row.Cells[p_elogation_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_w3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_W3 = decimal.Parse(row.Cells[p_elogation_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f1].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F1 = decimal.Parse(row.Cells[p_elogation_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f2].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F2 = decimal.Parse(row.Cells[p_elogation_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_elogation_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_elogation_f3].ToString(), out value))
                                            {
                                                inst.ELONGATIONFORCE_F3 = decimal.Parse(row.Cells[p_elogation_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region EDGECOMB
                                    if (p_edgecomb_w1 != 0)
                                    {
                                        if (row.Cells[p_edgecomb_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W1 = decimal.Parse(row.Cells[p_edgecomb_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W2 = decimal.Parse(row.Cells[p_edgecomb_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_w3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_W3 = decimal.Parse(row.Cells[p_edgecomb_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f1].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F1 = decimal.Parse(row.Cells[p_edgecomb_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f2].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F2 = decimal.Parse(row.Cells[p_edgecomb_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_edgecomb_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_edgecomb_f3].ToString(), out value))
                                            {
                                                inst.EDGECOMB_F3 = decimal.Parse(row.Cells[p_edgecomb_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STIFFNESS
                                    if (p_stiffness_w1 != 0)
                                    {
                                        if (row.Cells[p_stiffness_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W1 = decimal.Parse(row.Cells[p_stiffness_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W2 = decimal.Parse(row.Cells[p_stiffness_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_w3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_W3 = decimal.Parse(row.Cells[p_stiffness_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f1].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F1 = decimal.Parse(row.Cells[p_stiffness_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f2].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F2 = decimal.Parse(row.Cells[p_stiffness_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_stiffness_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_stiffness_f3].ToString(), out value))
                                            {
                                                inst.STIFFNESS_F3 = decimal.Parse(row.Cells[p_stiffness_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region TEAR
                                    if (p_tear_w1 != 0)
                                    {
                                        if (row.Cells[p_tear_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w1].ToString(), out value))
                                            {
                                                inst.TEAR_W1 = decimal.Parse(row.Cells[p_tear_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w2].ToString(), out value))
                                            {
                                                inst.TEAR_W2 = decimal.Parse(row.Cells[p_tear_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_w3].ToString(), out value))
                                            {
                                                inst.TEAR_W3 = decimal.Parse(row.Cells[p_tear_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f1].ToString(), out value))
                                            {
                                                inst.TEAR_F1 = decimal.Parse(row.Cells[p_tear_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f2].ToString(), out value))
                                            {
                                                inst.TEAR_F2 = decimal.Parse(row.Cells[p_tear_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_tear_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_tear_f3].ToString(), out value))
                                            {
                                                inst.TEAR_F3 = decimal.Parse(row.Cells[p_tear_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region STATIC_AIR
                                    if (p_static_air1 != 0)
                                    {
                                        if (row.Cells[p_static_air1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air1].ToString(), out value))
                                            {
                                                inst.STATIC_AIR1 = decimal.Parse(row.Cells[p_static_air1].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air1]) != null)
                                                    inst.STATIC_AIR1 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air1]).NumberValue.ToString());
                                            }

                                        }
                                        if (row.Cells[p_static_air2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air2].ToString(), out value))
                                            {
                                                inst.STATIC_AIR2 = decimal.Parse(row.Cells[p_static_air2].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR2 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air2]).NumberValue.ToString());
                                            }
                                        }
                                        if (row.Cells[p_static_air3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_static_air3].ToString(), out value))
                                            {
                                                inst.STATIC_AIR3 = decimal.Parse(row.Cells[p_static_air3].ToString());
                                            }
                                            else
                                            {
                                                if (evaluator.Evaluate(row.Cells[p_static_air2]) != null)
                                                    inst.STATIC_AIR3 = decimal.Parse(evaluator.Evaluate(row.Cells[p_static_air3]).NumberValue.ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FLAMMABILITY
                                    if (p_flammability_w != 0)
                                    {
                                        if (row.Cells[p_flammability_w] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_w].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_W = decimal.Parse(row.Cells[p_flammability_w].ToString());
                                            }
                                        }
                                        if (row.Cells[p_flammability_f] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flammability_f].ToString(), out value))
                                            {
                                                inst.FLAMMABILITY_F = decimal.Parse(row.Cells[p_flammability_f].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region DIMENSCHANGE
                                    if (p_dimenschange_w1 != 0)
                                    {
                                        if (row.Cells[p_dimenschange_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W1 = decimal.Parse(row.Cells[p_dimenschange_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W2 = decimal.Parse(row.Cells[p_dimenschange_w2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_w3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_w3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_W3 = decimal.Parse(row.Cells[p_dimenschange_w3].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f1].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F1 = decimal.Parse(row.Cells[p_dimenschange_f1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f2] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f2].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F2 = decimal.Parse(row.Cells[p_dimenschange_f2].ToString());
                                            }
                                        }
                                        if (row.Cells[p_dimenschange_f3] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_dimenschange_f3].ToString(), out value))
                                            {
                                                inst.DIMENSCHANGE_F3 = decimal.Parse(row.Cells[p_dimenschange_f3].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region FLAMMABILITY
                                    if (p_flexabrasion_w1 != 0)
                                    {
                                        if (row.Cells[p_flexabrasion_w1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flexabrasion_w1].ToString(), out value))
                                            {
                                                inst.FLEXABRASION_W1 = decimal.Parse(row.Cells[p_flexabrasion_w1].ToString());
                                            }
                                        }
                                        if (row.Cells[p_flexabrasion_f1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_flexabrasion_f1].ToString(), out value))
                                            {
                                                inst.FLEXABRASION_F1 = decimal.Parse(row.Cells[p_flexabrasion_f1].ToString());
                                            }
                                        }
                                    }
                                    #endregion

                                    #region BOW & SKEW

                                    if (p_bow1 != 0)
                                    {
                                        if (row.Cells[p_bow1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_bow1].ToString(), out value))
                                            {
                                                inst.BOW1 = decimal.Parse(row.Cells[p_bow1].ToString());
                                            }
                                        }
                                    }

                                    if (p_skew1 != 0)
                                    {
                                        if (row.Cells[p_skew1] != null)
                                        {
                                            if (Decimal.TryParse(row.Cells[p_skew1].ToString(), out value))
                                            {
                                                inst.SKEW1 = decimal.Parse(row.Cells[p_skew1].ToString());
                                            }
                                        }
                                    }

                                    if (p_entryby != 0)
                                    {
                                        inst.ENTEYBY = row.Cells[p_entryby].ToString();
                                    }

                                    if (p_approveby != 0)
                                    {
                                        inst.APPROVEBY = row.Cells[p_approveby].ToString();
                                    }

                                    #endregion

                                    if (inst.WEAVINGLOT.Count() >= 9)
                                    {
                                        results.Add(inst);
                                    }
                                    else
                                    {
                                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Lot No. = " + inst.WEAVINGLOT });
                                    }
                                }
                                else
                                {
                                    #region List
                                    if (row.Cells[p_weavinglog] == null)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(row.Cells[p_weavinglog].ToString()))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (p_weavinglog != 0)
                                            {
                                                inst.WEAVINGLOT = row.Cells[p_weavinglog].ToString();
                                                this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel Lot No. = " + inst.WEAVINGLOT });
                                            }

                                            if (string.IsNullOrEmpty(inst.WEAVINGLOT))
                                            {
                                                break;
                                            }

                                            //if (inst.WEAVINGLOT.Count() < 7)
                                            //{
                                            //    this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Head Cell != Detail Cell Please check file excel" });
                                            //    break;
                                            //}
                                        }
                                    }
                                    #endregion
                                }
                            }

                            #endregion
                        }
                        catch (Exception ex)
                        {
                            this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = ex.Message.ToString() });
                        }
                    }
                    else
                    {
                        this.listView.Items.Add(new CheckList { FILENAME = filePath, SUMROW = 0, SUMERR = 1, ERROR = "Excel Head Cell < 102 Please check file excel" });
                    }
                }

                return results;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();

                return results;
            }

        }
        #endregion

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
