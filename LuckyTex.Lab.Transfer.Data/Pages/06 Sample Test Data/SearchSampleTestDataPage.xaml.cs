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
//using Microsoft.Office.Interop.Excel;
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
    /// Interaction logic for SearchSampleTestDataPage.xaml
    /// </summary>
    public partial class SearchSampleTestDataPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SearchSampleTestDataPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemCode();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;

        string itemCode = string.Empty;
        string weavingLot = string.Empty;
        DateTime? entryDate = null;
        
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
            buttonEnabled(false);

            SearchData();

            buttonEnabled(true);
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion
        
        #region cmdExportData_Click
        private void cmdExportData_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot))
                ExportExcel();
            else
                "Please select Data".ShowMessageBox();

            buttonEnabled(true);
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
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.LAB_SEARCHLABSAMPLEDATA)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE != null)
                            {
                                itemCode = ((LuckyTex.Models.LAB_SEARCHLABSAMPLEDATA)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE;
                                weavingLot = ((LuckyTex.Models.LAB_SEARCHLABSAMPLEDATA)(gridLabEntryProduction.CurrentCell.Item)).PRODUCTIONLOT;
                                entryDate = ((LuckyTex.Models.LAB_SEARCHLABSAMPLEDATA)(gridLabEntryProduction.CurrentCell.Item)).ENTRYDATE;
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

            itemCode = string.Empty;
            weavingLot = string.Empty;
            entryDate = null;

            if (cbItemCode.ItemsSource != null)
                cbItemCode.SelectedIndex = 0;

            dteENTRYSTARTDATE.SelectedDate = null;
            dteENTRYSTARTDATE.Text = "";
            dteENTRYENDDATE.SelectedDate = null;
            dteENTRYENDDATE.Text = "";

            if (this.gridLabEntryProduction.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLabEntryProduction.SelectedItems.Clear();
            else
                this.gridLabEntryProduction.SelectedItem = null;

            gridLabEntryProduction.ItemsSource = null;
        }

        #endregion

        #region LoadItemCode

        private void LoadItemCode()
        {
            try
            {
                List<ITM_GETITEMCODELIST> items = LabDataPDFDataService.Instance.ITM_GETITEMCODELIST();

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

        #region SearchData
        private void SearchData()
        {
            string P_ITMCODE = string.Empty;
            string P_ENTRYSTARTDATE = string.Empty;
            string P_ENTRYENDDATE = string.Empty;

            if (cbItemCode.SelectedValue != null)
            {
                if (cbItemCode.SelectedValue.ToString() != "All")
                    P_ITMCODE = cbItemCode.SelectedValue.ToString();
            }


            if (dteENTRYSTARTDATE.SelectedDate != null)
                P_ENTRYSTARTDATE = dteENTRYSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (dteENTRYENDDATE.SelectedDate != null)
                P_ENTRYENDDATE = dteENTRYENDDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

            Lab_SearchData(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE);
        }
        #endregion

        #region Lab_SearchData
        private bool Lab_SearchData(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE)
        {
            bool chkLoad = true;

            try
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridLabEntryProduction.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridLabEntryProduction.SelectedItems.Clear();
                else
                    this.gridLabEntryProduction.SelectedItem = null;

                gridLabEntryProduction.ItemsSource = null;

                List<LAB_SEARCHLABSAMPLEDATA> results = LabDataPDFDataService.Instance.LAB_SEARCHLABSAMPLEDATA(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        gridLabEntryProduction.ItemsSource = results;
                    }
                    else
                    {
                        chkLoad = false;
                    }
                }
                else
                {
                    chkLoad = false;
                }

                return chkLoad;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region Export Excel

        #region ExportExcel
        private void ExportExcel()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string csv = string.Empty;
                csv = "Excel 97-2003 file (*.xls)|*.xls|Excel file (*.xlsx)|*.xlsx";

                saveFileDialog1.Filter = csv;
                saveFileDialog1.FilterIndex = 1;

                Nullable<bool> result = saveFileDialog1.ShowDialog();

                if (result == true)
                {
                    string newFileName = saveFileDialog1.FileName;

                    if (!string.IsNullOrEmpty(newFileName))
                    {
                        try
                        {
                            if (File.Exists(newFileName))
                            {
                                FileInfo fileCheck = new FileInfo(newFileName);
                                fileCheck.Delete();
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.Err();
                        }

                        if (DataTable_To_Excel(newFileName) == true)
                        {
                            MessageBox.Show("Excel : " + newFileName, "Save Complete", MessageBoxButton.OK);
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

        #region DataTable_To_Excel
        private bool DataTable_To_Excel(string fileName)
        {
            try
            {
                System.Data.DataTable dt = ExportToExcel();

                DataToExcel(dt, fileName);

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.Err();

                return false;
            }
        }
        #endregion

        #region DataToExcel
        private void DataToExcel(DataTable dt, string fileName)
        {
            using (dt)
            {
                double value;

                if (fileName.Contains(".xlsx") == true)
                {
                    XSSFWorkbook workbook = new XSSFWorkbook();//Create an excel Workbook
                    XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("Search Sample Test Data");//Create a work table in the table
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(0); //To add a row in the table

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 14;
                    font.FontName = "Cordia New";

                    // Create Header Style
                    XSSFCellStyle headerCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                    headerCellStyle.BorderBottom = BorderStyle.Thin;
                    headerCellStyle.BorderTop = BorderStyle.Thin;
                    headerCellStyle.BorderLeft = BorderStyle.Thin;
                    headerCellStyle.BorderRight = BorderStyle.Thin;
                    headerCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;


                    // Create Header Style
                    XSSFCellStyle detailCellStyle = (XSSFCellStyle)workbook.CreateCellStyle();
                    detailCellStyle.BorderBottom = BorderStyle.Thin;
                    detailCellStyle.BorderTop = BorderStyle.Thin;
                    detailCellStyle.BorderLeft = BorderStyle.Thin;
                    detailCellStyle.BorderRight = BorderStyle.Thin;
                    detailCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;

                    int n = 0;
                    foreach (DataColumn column in dt.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        headerRow.Cells[n].CellStyle = headerCellStyle;
                        headerRow.Cells[n].CellStyle.SetFont(font);

                        n++;
                    }

                    int rowIndex = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);

                        int i = 0;

                        foreach (DataColumn column in dt.Columns)
                        {
                            //dataRow.CreateCell(column.Ordinal, CellType.Numeric).SetCellValue(row[column].ToString());

                            if (row[column] != null)
                            {
                                if (!string.IsNullOrEmpty(row[column].ToString()) && row[column].ToString().Trim() != "")
                                {
                                    if (double.TryParse(row[column].ToString(), out value))
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(double.Parse(row[column].ToString()));
                                        dataRow.Cells[i].CellStyle = detailCellStyle;
                                        dataRow.Cells[i].CellStyle.SetFont(font);
                                    }
                                    else
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                        dataRow.Cells[i].CellStyle = headerCellStyle;
                                        dataRow.Cells[i].CellStyle.SetFont(font);
                                    }
                                }
                                else
                                {
                                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                    dataRow.Cells[i].CellStyle = headerCellStyle;
                                    dataRow.Cells[i].CellStyle.SetFont(font);
                                }

                                i++;
                            }
                        }
                        rowIndex++;
                    }

                    using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        workbook.Write(fs);
                    }
                }
                else
                {
                    IWorkbook workbook = new HSSFWorkbook();//Create an excel Workbook
                    ISheet sheet = workbook.CreateSheet("Search Sample Test Data");//Create a work table in the table
                    IRow headerRow = sheet.CreateRow(0);

                    var font = workbook.CreateFont();
                    font.FontHeightInPoints = 14;
                    font.FontName = "Cordia New";

                    // Create Header Style
                    ICellStyle headerCellStyle = workbook.CreateCellStyle();
                    headerCellStyle.BorderBottom = BorderStyle.Thin;
                    headerCellStyle.BorderTop = BorderStyle.Thin;
                    headerCellStyle.BorderLeft = BorderStyle.Thin;
                    headerCellStyle.BorderRight = BorderStyle.Thin;
                    headerCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;


                    // Create Header Style
                    ICellStyle detailCellStyle = workbook.CreateCellStyle();
                    detailCellStyle.BorderBottom = BorderStyle.Thin;
                    detailCellStyle.BorderTop = BorderStyle.Thin;
                    detailCellStyle.BorderLeft = BorderStyle.Thin;
                    detailCellStyle.BorderRight = BorderStyle.Thin;
                    detailCellStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;

                    int n = 0;
                    foreach (DataColumn column in dt.Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);
                        headerRow.Cells[n].CellStyle = headerCellStyle;
                        headerRow.Cells[n].CellStyle.SetFont(font);

                        n++;
                    }

                    int rowIndex = 1;

                    foreach (DataRow row in dt.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);

                        int i = 0;

                        foreach (DataColumn column in dt.Columns)
                        {
                            if (row[column] != null)
                            {
                                if (!string.IsNullOrEmpty(row[column].ToString()) && row[column].ToString().Trim() != "")
                                {
                                    if (double.TryParse(row[column].ToString(), out value))
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(double.Parse(row[column].ToString()));
                                        dataRow.Cells[i].CellStyle = detailCellStyle;
                                        dataRow.Cells[i].CellStyle.SetFont(font);
                                    }
                                    else
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                        dataRow.Cells[i].CellStyle = headerCellStyle;
                                        dataRow.Cells[i].CellStyle.SetFont(font);
                                    }
                                }
                                else
                                {
                                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                    dataRow.Cells[i].CellStyle = headerCellStyle;
                                    dataRow.Cells[i].CellStyle.SetFont(font);
                                }

                                i++;
                            }
                        }
                        rowIndex++;
                    }

                    using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        workbook.Write(fs);
                    }
                }
            }
        }
        #endregion

        #region ExportToExcel
        public System.Data.DataTable ExportToExcel()
        {
            try
            {
                #region Def

                System.Data.DataTable table = new System.Data.DataTable();


                List<LAB_GETSAMPLEDATABYMETHOD> results = LabDataPDFDataService.Instance.LAB_GETSAMPLEDATABYMETHOD(itemCode, weavingLot, null, null);

                table.Columns.Add("Item Code", typeof(string));
                table.Columns.Add("Production Lot", typeof(string));
                table.Columns.Add("Finishing Lot", typeof(string));
                table.Columns.Add("Entry Date", typeof(DateTime));
                table.Columns.Add("Entry By", typeof(string));
                table.Columns.Add("Method", typeof(string));
                table.Columns.Add("Yarn", typeof(string));
                table.Columns.Add("No", typeof(decimal));
                table.Columns.Add("Value", typeof(decimal));
                table.Columns.Add("Created Date", typeof(DateTime));

                string ITM_CODE = string.Empty;
                string PRODUCTIONLOT = string.Empty;
                string FINISHINGLOT = string.Empty;
                DateTime? ENTRYDATE = null;
                string ENTRYBY = string.Empty;
                string METHOD = string.Empty;
                string YARN = string.Empty;
                decimal? NO = null;
                decimal? VALUE = null;
                DateTime? CREATEDDATE = null;

                #endregion

                #region Insert Data

                if (results != null)
                {
                    for (int i = 0; i <= results.Count - 1; i++)
                    {
                        if (results[i].ITM_CODE != null
                                        && results[i].PRODUCTIONLOT != null
                                        && results[i].ENTRYDATE != null)
                        {
                            ITM_CODE = results[i].ITM_CODE;
                            PRODUCTIONLOT = results[i].PRODUCTIONLOT;
                            FINISHINGLOT = results[i].FINISHINGLOT;
                            ENTRYDATE = results[i].ENTRYDATE;
                            ENTRYBY = results[i].ENTRYBY;
                            METHOD = results[i].METHOD;
                            YARN = results[i].YARN;
                            NO = results[i].NO;
                            VALUE = results[i].VALUE;
                            CREATEDDATE = results[i].CREATEDDATE;

                            table.Rows.Add(ITM_CODE, PRODUCTIONLOT, FINISHINGLOT, ENTRYDATE, ENTRYBY, METHOD, YARN, NO, VALUE,CREATEDDATE);
                        }
                    }
                }

                #endregion

                return table;

            }
            catch (Exception ex)
            {
                ex.Message.Err();

                return null;
            }
        }
        #endregion

        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdSearch.IsEnabled = enabled;
            cmdClear.IsEnabled = enabled;
            cmdExportData.IsEnabled = enabled;
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

