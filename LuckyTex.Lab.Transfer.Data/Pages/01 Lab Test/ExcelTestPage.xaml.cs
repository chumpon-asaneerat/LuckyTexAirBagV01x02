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
    /// Interaction logic for ExcelTestPage.xaml
    /// </summary>
    public partial class ExcelTestPage : UserControl
    {
        #region ExcelTestPage
        public ExcelTestPage()
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

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            OpenExcel();
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

        }

        #endregion

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

                                List<LAB_ImportExcel1> results  = ExcelToDataTable(newFileName);

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

                    for (int i = (startRow); i <= sheet.LastRowNum; i++)
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

                            results.Add(inst);
                        }
                    }

                    #endregion
                }

                #region Old
                //int cellCount = headerRow.LastCellNum;

                //for (int j = 0; j < cellCount; j++)
                //{
                //    XSSFCell cell = (XSSFCell)headerRow.GetCell(j);
                //    dt.Columns.Add(cell.ToString());
                //}

                //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                //{
                //    XSSFRow row = (XSSFRow)sheet.GetRow(i);
                //    DataRow dataRow = dt.NewRow();
                //    if (row == null)
                //    {
                //        break;
                //    }
                //    for (int j = row.FirstCellNum; j < cellCount; j++)
                //    {
                //        if (row.GetCell(j) != null)
                //            dataRow[j] = row.GetCell(j).ToString();
                //    }

                //    dt.Rows.Add(dataRow);
                //}
                #endregion

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
                //hssfworkbook.IsHidden = true;
                //ISheet sheet = hssfworkbook.GetSheet("");
                int s = hssfworkbook.ActiveSheetIndex;

                ISheet sheet = hssfworkbook.GetSheetAt(s);
                IRow row = sheet.GetRow(0);

                string itemcode = string.Empty;
                int startRow = 6;

                if (!string.IsNullOrEmpty(row.Cells[0].ToString()))
                    itemcode = row.Cells[0].ToString();
                else if (!string.IsNullOrEmpty(row.Cells[1].ToString()))
                    itemcode = row.Cells[1].ToString();

                //if (hssfworkbook is XSSFWorkbook)
                //{
                //    XSSFFormulaEvaluator.EvaluateAllFormulaCells(hssfworkbook);
                //}
                //else
                //{
                //    HSSFFormulaEvaluator.EvaluateAllFormulaCells(hssfworkbook);
                //}

                IFormulaEvaluator evaluator = hssfworkbook.GetCreationHelper().CreateFormulaEvaluator();
                //CellValue cellValue = evaluator.evaluate(cell);

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

                    #endregion

                    row = sheet.GetRow(startRow);

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

                    #region Add Detail

                    for (int i = (startRow); i <= sheet.LastRowNum; i++)
                    {
                        LAB_ImportExcel1 inst = new LAB_ImportExcel1();

                        row = sheet.GetRow(i);

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

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user, string level)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (positionLevel != null)
            {
                positionLevel = level;
            }
        }

        #endregion
    }

}
