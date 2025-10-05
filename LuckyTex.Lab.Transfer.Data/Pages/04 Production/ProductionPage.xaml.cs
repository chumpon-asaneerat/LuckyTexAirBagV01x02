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
    /// Interaction logic for ProductionPage.xaml
    /// </summary>
    public partial class ProductionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProductionPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemCode();
            LoadLoom();
            LoadFinishingProcess();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string positionLevel = string.Empty;

        string itemCode = string.Empty;
        string weavingLot = string.Empty;
        string finishingLot = string.Empty;
        DateTime? entryDate = null;
        string status = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;

            //if (positionLevel != "")
            //{
            //    if (positionLevel == "1" || positionLevel == "2")
            //    {
            //        cmdPrintReport.Visibility = System.Windows.Visibility.Visible;
            //    }
            //    else
            //    {
            //        cmdPrintReport.Visibility = System.Windows.Visibility.Collapsed;
            //    }
            //}
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

        #region cmdViewData_Click
        private void cmdViewData_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null && !string.IsNullOrEmpty(status))
            {
                LabDataEntryInfo labData = LabDataPDFDataService.Instance.ShowLabDataEntryWindow(opera, positionLevel, itemCode, weavingLot, finishingLot, entryDate, status);
                if (null != labData)
                {
                    if (labData.ChkApprove == true)
                    {
                        itemCode = string.Empty;
                        weavingLot = string.Empty;
                        finishingLot = string.Empty;
                        entryDate = null;
                        status = string.Empty;

                        SearchData();
                    }
                }
            }

            buttonEnabled(true);
        }
        #endregion

        #region cmdPrintReport_Click
        private void cmdPrintReport_Click(object sender, RoutedEventArgs e)
        {
            string P_ENTRYSTARTDATE = string.Empty;
            string P_ENTRYENDDATE = string.Empty;
            string P_LOOM = string.Empty;
            string P_FINISHPROCESS = string.Empty;

            if (cbMCNAME.SelectedValue != null)
            {
                if (cbMCNAME.SelectedValue.ToString() != "All")
                    P_LOOM = cbMCNAME.SelectedValue.ToString();
            }

            if (cbFinishingProcess.SelectedValue != null)
            {
                if (cbFinishingProcess.SelectedValue.ToString() != "All")
                    P_FINISHPROCESS = cbFinishingProcess.SelectedValue.ToString();
            }

            if (entryDate != null)
                P_ENTRYSTARTDATE = entryDate.Value.ToString("dd/MM/yyyy");

            if (entryDate != null)
                P_ENTRYENDDATE = entryDate.Value.ToString("dd/MM/yyyy");

            if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null)
            {
                Preview(itemCode, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS);
            }
            else
                "Please select Data".ShowMessageBox();
        }
        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdExportExcelforQDAS_Click
        private void cmdExportExcelforQDAS_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemCode.SelectedValue != null)
            {
                if (cbItemCode.SelectedValue.ToString() != "All")
                {
                    buttonEnabled(false);

                    //ExportCSVQDAS();
                    ExportExcelforQDAS();

                    buttonEnabled(true);
                }
                else
                {
                    "Please select Item Code".ShowMessageBox();
                }
            }
            else
            {
                "Please select Item Code".ShowMessageBox();
            }
        }
        #endregion
        
        #region cmdExportData_Click
        private void cmdExportData_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            //ExportCSV();
            ExportExcel();

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
                            if (((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE != null
                                && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).WEAVINGLOT != null
                                && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).FINISHINGLOT != null
                                && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).ENTRYDATE != null
                                && ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).STATUS != null)
                            {
                                itemCode = ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE;
                                weavingLot = ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).WEAVINGLOT;
                                finishingLot = ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).FINISHINGLOT;
                                entryDate = ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).ENTRYDATE;
                                status = ((LuckyTex.Models.LAB_SEARCHLABENTRYPRODUCTION)(gridLabEntryProduction.CurrentCell.Item)).STATUS;
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
            finishingLot = string.Empty;
            entryDate = null;
            status = string.Empty;

            //cbItemCode.Text = "";
            //cbItemCode.SelectedItem = null;
            //cbMCNAME.Text = "";
            //cbMCNAME.SelectedItem = null;
            //cbFinishingProcess.Text = "";
            //cbFinishingProcess.SelectedItem = null;

            if (cbItemCode.ItemsSource != null)
                cbItemCode.SelectedIndex = 0;

            if (cbMCNAME.ItemsSource != null)
                cbMCNAME.SelectedIndex = 0;

            if (cbFinishingProcess.ItemsSource != null)
                cbFinishingProcess.SelectedIndex = 0;


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

        #region LoadLoom

        private void LoadLoom()
        {
            try
            {
                List<MC_GETLOOMLIST> items = LabDataPDFDataService.Instance.MC_GETLOOMLIST();

                this.cbMCNAME.ItemsSource = items;
                this.cbMCNAME.DisplayMemberPath = "MCNAME";
                this.cbMCNAME.SelectedValuePath = "MCNAME";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadFinishingProcess

        private void LoadFinishingProcess()
        {
            try
            {
                List<LAB_FinishingProcess> items = LabDataPDFDataService.Instance.LAB_FinishingProcess();

                this.cbFinishingProcess.ItemsSource = items;
                this.cbFinishingProcess.DisplayMemberPath = "PROCESS";
                this.cbFinishingProcess.SelectedValuePath = "PROCESS";
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
            string P_LOOM = string.Empty;
            string P_FINISHPROCESS = string.Empty;

            if (cbItemCode.SelectedValue != null)
            {
                if (cbItemCode.SelectedValue.ToString() != "All")
                    P_ITMCODE = cbItemCode.SelectedValue.ToString();
            }

            if (cbMCNAME.SelectedValue != null)
            {
                if (cbMCNAME.SelectedValue.ToString() != "All")
                    P_LOOM = cbMCNAME.SelectedValue.ToString();
            }

            if (cbFinishingProcess.SelectedValue != null)
            {
                if (cbFinishingProcess.SelectedValue.ToString() != "All")
                    P_FINISHPROCESS = cbFinishingProcess.SelectedValue.ToString();
            }

            if (dteENTRYSTARTDATE.SelectedDate != null)
                P_ENTRYSTARTDATE = dteENTRYSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

            if (dteENTRYENDDATE.SelectedDate != null)
                P_ENTRYENDDATE = dteENTRYENDDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

            Lab_SearchData(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS);
        }
        #endregion

        #region Lab_SearchData
        private bool Lab_SearchData(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS)
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

                List<LAB_SEARCHLABENTRYPRODUCTION> results = LabDataPDFDataService.Instance.LAB_SEARCHLABENTRYPRODUCTION(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS);

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
                    XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("For Q-Das (Data Stack)");//Create a work table in the table
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
                    ISheet sheet = workbook.CreateSheet("For Q-Das (Data Stack)");//Create a work table in the table
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

                string P_ITMCODE = string.Empty;
                string P_ENTRYSTARTDATE = string.Empty;
                string P_ENTRYENDDATE = string.Empty;
                string P_LOOM = string.Empty;
                string P_FINISHPROCESS = string.Empty;

                if (cbItemCode.SelectedValue != null)
                {
                    if (cbItemCode.SelectedValue.ToString() != "All")
                        P_ITMCODE = cbItemCode.SelectedValue.ToString();
                }

                if (cbMCNAME.SelectedValue != null)
                {
                    if (cbMCNAME.SelectedValue.ToString() != "All")
                        P_LOOM = cbMCNAME.SelectedValue.ToString();
                }

                if (cbFinishingProcess.SelectedValue != null)
                {
                    if (cbFinishingProcess.SelectedValue.ToString() != "All")
                        P_FINISHPROCESS = cbFinishingProcess.SelectedValue.ToString();
                }

                if (dteENTRYSTARTDATE.SelectedDate != null)
                    P_ENTRYSTARTDATE = dteENTRYSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (dteENTRYENDDATE.SelectedDate != null)
                    P_ENTRYENDDATE = dteENTRYENDDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

                List<LAB_SEARCHLABENTRYPRODUCTION> results = LabDataPDFDataService.Instance.LAB_SEARCHLABENTRYPRODUCTION(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS);

                table.Columns.Add("ITM_CODE", typeof(string));
                table.Columns.Add("WEAVINGLOT", typeof(string));
                table.Columns.Add("FINISHINGLOT", typeof(string));
                table.Columns.Add("ENTRYDATE", typeof(DateTime));
                table.Columns.Add("ENTEYBY", typeof(string));
                table.Columns.Add("WIDTH", typeof(decimal));
                table.Columns.Add("USABLE_WIDTH1", typeof(decimal));
                table.Columns.Add("USABLE_WIDTH2", typeof(decimal));
                table.Columns.Add("USABLE_WIDTH3", typeof(decimal));
                table.Columns.Add("WIDTH_SILICONE1", typeof(decimal));
                table.Columns.Add("WIDTH_SILICONE2", typeof(decimal));
                table.Columns.Add("WIDTH_SILICONE3", typeof(decimal));
                table.Columns.Add("NUMTHREADS_W1", typeof(decimal));
                table.Columns.Add("NUMTHREADS_W2", typeof(decimal));
                table.Columns.Add("NUMTHREADS_W3", typeof(decimal));
                table.Columns.Add("NUMTHREADS_F1", typeof(decimal));
                table.Columns.Add("NUMTHREADS_F2", typeof(decimal));
                table.Columns.Add("NUMTHREADS_F3", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT1", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT2", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT3", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT4", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT5", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT6", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT1", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT2", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT3", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT4", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT5", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT6", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT1", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT2", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT3", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT4", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT5", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT6", typeof(decimal));
                table.Columns.Add("THICKNESS1", typeof(decimal));
                table.Columns.Add("THICKNESS2", typeof(decimal));
                table.Columns.Add("THICKNESS3", typeof(decimal));
                table.Columns.Add("MAXFORCE_W1", typeof(decimal));
                table.Columns.Add("MAXFORCE_W2", typeof(decimal));
                table.Columns.Add("MAXFORCE_W3", typeof(decimal));
                table.Columns.Add("MAXFORCE_F1", typeof(decimal));
                table.Columns.Add("MAXFORCE_F2", typeof(decimal));
                table.Columns.Add("MAXFORCE_F3", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_W1", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_W2", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_W3", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_F1", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_F2", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_F3", typeof(decimal));

                table.Columns.Add("FLAMMABILITY_W", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_W2", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_W3", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_W4", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_W5", typeof(decimal));

                table.Columns.Add("FLAMMABILITY_F", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_F2", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_F3", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_F4", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_F5", typeof(decimal));

                table.Columns.Add("EDGECOMB_W1", typeof(decimal));
                table.Columns.Add("EDGECOMB_W2", typeof(decimal));
                table.Columns.Add("EDGECOMB_W3", typeof(decimal));
                table.Columns.Add("EDGECOMB_F1", typeof(decimal));
                table.Columns.Add("EDGECOMB_F2", typeof(decimal));
                table.Columns.Add("EDGECOMB_F3", typeof(decimal));
                table.Columns.Add("STIFFNESS_W1", typeof(decimal));
                table.Columns.Add("STIFFNESS_W2", typeof(decimal));
                table.Columns.Add("STIFFNESS_W3", typeof(decimal));
                table.Columns.Add("STIFFNESS_F1", typeof(decimal));
                table.Columns.Add("STIFFNESS_F2", typeof(decimal));
                table.Columns.Add("STIFFNESS_F3", typeof(decimal));
                table.Columns.Add("TEAR_W1", typeof(decimal));
                table.Columns.Add("TEAR_W2", typeof(decimal));
                table.Columns.Add("TEAR_W3", typeof(decimal));
                table.Columns.Add("TEAR_F1", typeof(decimal));
                table.Columns.Add("TEAR_F2", typeof(decimal));
                table.Columns.Add("TEAR_F3", typeof(decimal));
                table.Columns.Add("STATIC_AIR1", typeof(decimal));
                table.Columns.Add("STATIC_AIR2", typeof(decimal));
                table.Columns.Add("STATIC_AIR3", typeof(decimal));

                table.Columns.Add("STATIC_AIR4", typeof(decimal));
                table.Columns.Add("STATIC_AIR5", typeof(decimal));
                table.Columns.Add("STATIC_AIR6", typeof(decimal));

                table.Columns.Add("DYNAMIC_AIR1", typeof(decimal));
                table.Columns.Add("DYNAMIC_AIR2", typeof(decimal));
                table.Columns.Add("DYNAMIC_AIR3", typeof(decimal));
                table.Columns.Add("EXPONENT1", typeof(decimal));
                table.Columns.Add("EXPONENT2", typeof(decimal));
                table.Columns.Add("EXPONENT3", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_W1", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_W2", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_W3", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_F1", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_F2", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_F3", typeof(decimal));
                table.Columns.Add("FLEXABRASION_W1", typeof(decimal));
                table.Columns.Add("FLEXABRASION_W2", typeof(decimal));
                table.Columns.Add("FLEXABRASION_W3", typeof(decimal));
                table.Columns.Add("FLEXABRASION_F1", typeof(decimal));
                table.Columns.Add("FLEXABRASION_F2", typeof(decimal));
                table.Columns.Add("FLEXABRASION_F3", typeof(decimal));

                //table.Columns.Add("BOW", typeof(decimal));
                //table.Columns.Add("SKEW", typeof(decimal));
                table.Columns.Add("STATUS", typeof(string));
                table.Columns.Add("REMARK", typeof(string));
                table.Columns.Add("APPROVEBY", typeof(string));
                table.Columns.Add("APPROVEDATE", typeof(DateTime));
                table.Columns.Add("CREATEDATE", typeof(DateTime));
                table.Columns.Add("FINISHLENGTH", typeof(decimal));
                table.Columns.Add("FINISHINGPROCESS", typeof(string));
                table.Columns.Add("LOOMNO", typeof(string));
                table.Columns.Add("FINISHINGMC", typeof(string));

                //update 06/07/18
                table.Columns.Add("BOW1", typeof(decimal));
                table.Columns.Add("BOW2", typeof(decimal));
                table.Columns.Add("BOW3", typeof(decimal));
                table.Columns.Add("SKEW1", typeof(decimal));
                table.Columns.Add("SKEW2", typeof(decimal));
                table.Columns.Add("SKEW3", typeof(decimal));
                table.Columns.Add("BENDING_W1", typeof(decimal));
                table.Columns.Add("BENDING_W2", typeof(decimal));
                table.Columns.Add("BENDING_W3", typeof(decimal));
                table.Columns.Add("BENDING_F1", typeof(decimal));
                table.Columns.Add("BENDING_F2", typeof(decimal));
                table.Columns.Add("BENDING_F3", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_W1", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_W2", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_W3", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_F1", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_F2", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_F3", typeof(decimal));
                table.Columns.Add("ITEMLOT", typeof(string));

                string ITM_CODE = string.Empty;
                string WEAVINGLOT = string.Empty;
                string FINISHINGLOT = string.Empty;
                DateTime? ENTRYDATE = null;
                string ENTEYBY = string.Empty;
                decimal? WIDTH = null;
                decimal? USABLE_WIDTH1 = null;
                decimal? USABLE_WIDTH2 = null;
                decimal? USABLE_WIDTH3 = null;
                decimal? WIDTH_SILICONE1 = null;
                decimal? WIDTH_SILICONE2 = null;
                decimal? WIDTH_SILICONE3 = null;
                decimal? NUMTHREADS_W1 = null;
                decimal? NUMTHREADS_W2 = null;
                decimal? NUMTHREADS_W3 = null;
                decimal? NUMTHREADS_F1 = null;
                decimal? NUMTHREADS_F2 = null;
                decimal? NUMTHREADS_F3 = null;
                decimal? TOTALWEIGHT1 = null;
                decimal? TOTALWEIGHT2 = null;
                decimal? TOTALWEIGHT3 = null;
                decimal? TOTALWEIGHT4 = null;
                decimal? TOTALWEIGHT5 = null;
                decimal? TOTALWEIGHT6 = null;
                decimal? UNCOATEDWEIGHT1 = null;
                decimal? UNCOATEDWEIGHT2 = null;
                decimal? UNCOATEDWEIGHT3 = null;
                decimal? UNCOATEDWEIGHT4 = null;
                decimal? UNCOATEDWEIGHT5 = null;
                decimal? UNCOATEDWEIGHT6 = null;
                decimal? COATINGWEIGHT1 = null;
                decimal? COATINGWEIGHT2 = null;
                decimal? COATINGWEIGHT3 = null;
                decimal? COATINGWEIGHT4 = null;
                decimal? COATINGWEIGHT5 = null;
                decimal? COATINGWEIGHT6 = null;
                decimal? THICKNESS1 = null;
                decimal? THICKNESS2 = null;
                decimal? THICKNESS3 = null;
                decimal? MAXFORCE_W1 = null;
                decimal? MAXFORCE_W2 = null;
                decimal? MAXFORCE_W3 = null;
                decimal? MAXFORCE_F1 = null;
                decimal? MAXFORCE_F2 = null;
                decimal? MAXFORCE_F3 = null;
                decimal? ELONGATIONFORCE_W1 = null;
                decimal? ELONGATIONFORCE_W2 = null;
                decimal? ELONGATIONFORCE_W3 = null;
                decimal? ELONGATIONFORCE_F1 = null;
                decimal? ELONGATIONFORCE_F2 = null;
                decimal? ELONGATIONFORCE_F3 = null;

                decimal? FLAMMABILITY_W = null;
                decimal? FLAMMABILITY_W2 = null;
                decimal? FLAMMABILITY_W3 = null;
                decimal? FLAMMABILITY_W4 = null;
                decimal? FLAMMABILITY_W5 = null;

                decimal? FLAMMABILITY_F = null;
                decimal? FLAMMABILITY_F2 = null;
                decimal? FLAMMABILITY_F3 = null;
                decimal? FLAMMABILITY_F4 = null;
                decimal? FLAMMABILITY_F5 = null;

                decimal? EDGECOMB_W1 = null;
                decimal? EDGECOMB_W2 = null;
                decimal? EDGECOMB_W3 = null;
                decimal? EDGECOMB_F1 = null;
                decimal? EDGECOMB_F2 = null;
                decimal? EDGECOMB_F3 = null;
                decimal? STIFFNESS_W1 = null;
                decimal? STIFFNESS_W2 = null;
                decimal? STIFFNESS_W3 = null;
                decimal? STIFFNESS_F1 = null;
                decimal? STIFFNESS_F2 = null;
                decimal? STIFFNESS_F3 = null;
                decimal? TEAR_W1 = null;
                decimal? TEAR_W2 = null;
                decimal? TEAR_W3 = null;
                decimal? TEAR_F1 = null;
                decimal? TEAR_F2 = null;
                decimal? TEAR_F3 = null;
                decimal? STATIC_AIR1 = null;
                decimal? STATIC_AIR2 = null;
                decimal? STATIC_AIR3 = null;

                decimal? STATIC_AIR4 = null;
                decimal? STATIC_AIR5 = null;
                decimal? STATIC_AIR6 = null;

                decimal? DYNAMIC_AIR1 = null;
                decimal? DYNAMIC_AIR2 = null;
                decimal? DYNAMIC_AIR3 = null;
                decimal? EXPONENT1 = null;
                decimal? EXPONENT2 = null;
                decimal? EXPONENT3 = null;
                decimal? DIMENSCHANGE_W1 = null;
                decimal? DIMENSCHANGE_W2 = null;
                decimal? DIMENSCHANGE_W3 = null;
                decimal? DIMENSCHANGE_F1 = null;
                decimal? DIMENSCHANGE_F2 = null;
                decimal? DIMENSCHANGE_F3 = null;
                decimal? FLEXABRASION_W1 = null;
                decimal? FLEXABRASION_W2 = null;
                decimal? FLEXABRASION_W3 = null;
                decimal? FLEXABRASION_F1 = null;
                decimal? FLEXABRASION_F2 = null;
                decimal? FLEXABRASION_F3 = null;
                string STATUS = string.Empty;
                string REMARK = string.Empty;
                string APPROVEBY = string.Empty;
                DateTime? APPROVEDATE = null;
                DateTime? CREATEDATE = null;
                decimal? FINISHLENGTH = null;
                string FINISHINGPROCESS = string.Empty;
                string LOOMNO = string.Empty;
                string FINISHINGMC = string.Empty;

                // Update 06/07/18
                decimal? BOW1 = null;
                decimal? BOW2 = null;
                decimal? BOW3 = null;
                decimal? SKEW1 = null;
                decimal? SKEW2 = null;
                decimal? SKEW3 = null;
                decimal? BENDING_W1 = null;
                decimal? BENDING_W2 = null;
                decimal? BENDING_W3 = null;
                decimal? BENDING_F1 = null;
                decimal? BENDING_F2 = null;
                decimal? BENDING_F3 = null;
                decimal? FLEX_SCOTT_W1 = null;
                decimal? FLEX_SCOTT_W2 = null;
                decimal? FLEX_SCOTT_W3 = null;
                decimal? FLEX_SCOTT_F1 = null;
                decimal? FLEX_SCOTT_F2 = null;
                decimal? FLEX_SCOTT_F3 = null;
                string ITEMLOT = string.Empty;

                #endregion

                #region Insert Data

                if (results != null)
                {
                    for (int i = 0; i <= results.Count - 1; i++)
                    {
                        if (results[i].ITM_CODE != null
                                        && results[i].WEAVINGLOT != null
                                        && results[i].FINISHINGLOT != null
                                        && results[i].ENTRYDATE != null
                                        && results[i].STATUS != null)
                        {
                            ITM_CODE = results[i].ITM_CODE;
                            WEAVINGLOT = results[i].WEAVINGLOT;
                            FINISHINGLOT = results[i].FINISHINGLOT;
                            ENTRYDATE = results[i].ENTRYDATE;
                            ENTEYBY = results[i].ENTEYBY;
                            WIDTH = results[i].WIDTH;
                            USABLE_WIDTH1 = results[i].USABLE_WIDTH1;
                            USABLE_WIDTH2 = results[i].USABLE_WIDTH2;
                            USABLE_WIDTH3 = results[i].USABLE_WIDTH3;
                            WIDTH_SILICONE1 = results[i].WIDTH_SILICONE1;
                            WIDTH_SILICONE2 = results[i].WIDTH_SILICONE2;
                            WIDTH_SILICONE3 = results[i].WIDTH_SILICONE3;
                            NUMTHREADS_W1 = results[i].NUMTHREADS_W1;
                            NUMTHREADS_W2 = results[i].NUMTHREADS_W2;
                            NUMTHREADS_W3 = results[i].NUMTHREADS_W3;
                            NUMTHREADS_F1 = results[i].NUMTHREADS_F1;
                            NUMTHREADS_F2 = results[i].NUMTHREADS_F2;
                            NUMTHREADS_F3 = results[i].NUMTHREADS_F3;
                            TOTALWEIGHT1 = results[i].TOTALWEIGHT1;
                            TOTALWEIGHT2 = results[i].TOTALWEIGHT2;
                            TOTALWEIGHT3 = results[i].TOTALWEIGHT3;
                            TOTALWEIGHT4 = results[i].TOTALWEIGHT4;
                            TOTALWEIGHT5 = results[i].TOTALWEIGHT5;
                            TOTALWEIGHT6 = results[i].TOTALWEIGHT6;
                            UNCOATEDWEIGHT1 = results[i].UNCOATEDWEIGHT1;
                            UNCOATEDWEIGHT2 = results[i].UNCOATEDWEIGHT2;
                            UNCOATEDWEIGHT3 = results[i].UNCOATEDWEIGHT3;
                            UNCOATEDWEIGHT4 = results[i].UNCOATEDWEIGHT4;
                            UNCOATEDWEIGHT5 = results[i].UNCOATEDWEIGHT5;
                            UNCOATEDWEIGHT6 = results[i].UNCOATEDWEIGHT6;
                            COATINGWEIGHT1 = results[i].COATINGWEIGHT1;
                            COATINGWEIGHT2 = results[i].COATINGWEIGHT2;
                            COATINGWEIGHT3 = results[i].COATINGWEIGHT3;
                            COATINGWEIGHT4 = results[i].COATINGWEIGHT4;
                            COATINGWEIGHT5 = results[i].COATINGWEIGHT5;
                            COATINGWEIGHT6 = results[i].COATINGWEIGHT6;
                            THICKNESS1 = results[i].THICKNESS1;
                            THICKNESS2 = results[i].THICKNESS2;
                            THICKNESS3 = results[i].THICKNESS3;
                            MAXFORCE_W1 = results[i].MAXFORCE_W1;
                            MAXFORCE_W2 = results[i].MAXFORCE_W2;
                            MAXFORCE_W3 = results[i].MAXFORCE_W3;
                            MAXFORCE_F1 = results[i].MAXFORCE_F1;
                            MAXFORCE_F2 = results[i].MAXFORCE_F2;
                            MAXFORCE_F3 = results[i].MAXFORCE_F3;
                            ELONGATIONFORCE_W1 = results[i].ELONGATIONFORCE_W1;
                            ELONGATIONFORCE_W2 = results[i].ELONGATIONFORCE_W2;
                            ELONGATIONFORCE_W3 = results[i].ELONGATIONFORCE_W3;
                            ELONGATIONFORCE_F1 = results[i].ELONGATIONFORCE_F1;
                            ELONGATIONFORCE_F2 = results[i].ELONGATIONFORCE_F2;
                            ELONGATIONFORCE_F3 = results[i].ELONGATIONFORCE_F3;

                            FLAMMABILITY_W = results[i].FLAMMABILITY_W;
                            FLAMMABILITY_W2 = results[i].FLAMMABILITY_W2;
                            FLAMMABILITY_W3 = results[i].FLAMMABILITY_W3;
                            FLAMMABILITY_W4 = results[i].FLAMMABILITY_W4;
                            FLAMMABILITY_W5 = results[i].FLAMMABILITY_W5;

                            FLAMMABILITY_F = results[i].FLAMMABILITY_F;
                            FLAMMABILITY_F2 = results[i].FLAMMABILITY_F2;
                            FLAMMABILITY_F3 = results[i].FLAMMABILITY_F3;
                            FLAMMABILITY_F4 = results[i].FLAMMABILITY_F4;
                            FLAMMABILITY_F5 = results[i].FLAMMABILITY_F5;

                            EDGECOMB_W1 = results[i].EDGECOMB_W1;
                            EDGECOMB_W2 = results[i].EDGECOMB_W2;
                            EDGECOMB_W3 = results[i].EDGECOMB_W3;
                            EDGECOMB_F1 = results[i].EDGECOMB_F1;
                            EDGECOMB_F2 = results[i].EDGECOMB_F2;
                            EDGECOMB_F3 = results[i].EDGECOMB_F3;
                            STIFFNESS_W1 = results[i].STIFFNESS_W1;
                            STIFFNESS_W2 = results[i].STIFFNESS_W2;
                            STIFFNESS_W3 = results[i].STIFFNESS_W3;
                            STIFFNESS_F1 = results[i].STIFFNESS_F1;
                            STIFFNESS_F2 = results[i].STIFFNESS_F2;
                            STIFFNESS_F3 = results[i].STIFFNESS_F3;
                            TEAR_W1 = results[i].TEAR_W1;
                            TEAR_W2 = results[i].TEAR_W2;
                            TEAR_W3 = results[i].TEAR_W3;
                            TEAR_F1 = results[i].TEAR_F1;
                            TEAR_F2 = results[i].TEAR_F2;
                            TEAR_F3 = results[i].TEAR_F3;
                            STATIC_AIR1 = results[i].STATIC_AIR1;
                            STATIC_AIR2 = results[i].STATIC_AIR2;
                            STATIC_AIR3 = results[i].STATIC_AIR3;
                            STATIC_AIR4 = results[i].STATIC_AIR4;
                            STATIC_AIR5 = results[i].STATIC_AIR5;
                            STATIC_AIR6 = results[i].STATIC_AIR6;

                            DYNAMIC_AIR1 = results[i].DYNAMIC_AIR1;
                            DYNAMIC_AIR2 = results[i].DYNAMIC_AIR2;
                            DYNAMIC_AIR3 = results[i].DYNAMIC_AIR3;
                            EXPONENT1 = results[i].EXPONENT1;
                            EXPONENT2 = results[i].EXPONENT2;
                            EXPONENT3 = results[i].EXPONENT3;
                            DIMENSCHANGE_W1 = results[i].DIMENSCHANGE_W1;
                            DIMENSCHANGE_W2 = results[i].DIMENSCHANGE_W2;
                            DIMENSCHANGE_W3 = results[i].DIMENSCHANGE_W3;
                            DIMENSCHANGE_F1 = results[i].DIMENSCHANGE_F1;
                            DIMENSCHANGE_F2 = results[i].DIMENSCHANGE_F2;
                            DIMENSCHANGE_F3 = results[i].DIMENSCHANGE_F3;
                            FLEXABRASION_W1 = results[i].FLEXABRASION_W1;
                            FLEXABRASION_W2 = results[i].FLEXABRASION_W2;
                            FLEXABRASION_W3 = results[i].FLEXABRASION_W3;
                            FLEXABRASION_F1 = results[i].FLEXABRASION_F1;
                            FLEXABRASION_F2 = results[i].FLEXABRASION_F2;
                            FLEXABRASION_F3 = results[i].FLEXABRASION_F3;

                            STATUS = results[i].STATUS;
                            REMARK = results[i].REMARK;
                            APPROVEBY = results[i].APPROVEBY;
                            APPROVEDATE = results[i].APPROVEDATE;
                            CREATEDATE = results[i].CREATEDATE;
                            FINISHLENGTH = results[i].FINISHLENGTH;
                            FINISHINGPROCESS = results[i].FINISHINGPROCESS;
                            LOOMNO = results[i].LOOMNO;
                            FINISHINGMC = results[i].FINISHINGMC;

                            // Update 06/07/18
                            BOW1 = results[i].BOW1;
                            BOW2 = results[i].BOW2;
                            BOW3 = results[i].BOW3;
                            SKEW1 = results[i].SKEW1;
                            SKEW2 = results[i].SKEW2;
                            SKEW3 = results[i].SKEW3;
                            BENDING_W1 = results[i].BENDING_W1;
                            BENDING_W2 = results[i].BENDING_W2;
                            BENDING_W3 = results[i].BENDING_W3;
                            BENDING_F1 = results[i].BENDING_F1;
                            BENDING_F2 = results[i].BENDING_F2;
                            BENDING_F3 = results[i].BENDING_F3;
                            FLEX_SCOTT_W1 = results[i].FLEX_SCOTT_W1;
                            FLEX_SCOTT_W2 = results[i].FLEX_SCOTT_W2;
                            FLEX_SCOTT_W3 = results[i].FLEX_SCOTT_W3;
                            FLEX_SCOTT_F1 = results[i].FLEX_SCOTT_F1;
                            FLEX_SCOTT_F2 = results[i].FLEX_SCOTT_F2;
                            FLEX_SCOTT_F3 = results[i].FLEX_SCOTT_F3;
                            ITEMLOT = results[i].ITEMLOT;

                            table.Rows.Add(ITM_CODE, WEAVINGLOT, FINISHINGLOT, ENTRYDATE, ENTEYBY, WIDTH,
                                USABLE_WIDTH1, USABLE_WIDTH2, USABLE_WIDTH3, WIDTH_SILICONE1, WIDTH_SILICONE2, WIDTH_SILICONE3,
                                NUMTHREADS_W1, NUMTHREADS_W2, NUMTHREADS_W3, NUMTHREADS_F1, NUMTHREADS_F2, NUMTHREADS_F3,
                                TOTALWEIGHT1, TOTALWEIGHT2, TOTALWEIGHT3, TOTALWEIGHT4, TOTALWEIGHT5, TOTALWEIGHT6,
                                UNCOATEDWEIGHT1, UNCOATEDWEIGHT2, UNCOATEDWEIGHT3, UNCOATEDWEIGHT4, UNCOATEDWEIGHT5, UNCOATEDWEIGHT6,
                                COATINGWEIGHT1, COATINGWEIGHT2, COATINGWEIGHT3, COATINGWEIGHT4, COATINGWEIGHT5, COATINGWEIGHT6,
                                THICKNESS1, THICKNESS2, THICKNESS3,
                                MAXFORCE_W1, MAXFORCE_W2, MAXFORCE_W3, MAXFORCE_F1, MAXFORCE_F2, MAXFORCE_F3,
                                ELONGATIONFORCE_W1, ELONGATIONFORCE_W2, ELONGATIONFORCE_W3, ELONGATIONFORCE_F1, ELONGATIONFORCE_F2, ELONGATIONFORCE_F3,
                                FLAMMABILITY_W, FLAMMABILITY_W2, FLAMMABILITY_W3, FLAMMABILITY_W4, FLAMMABILITY_W5,
                                FLAMMABILITY_F, FLAMMABILITY_F2, FLAMMABILITY_F3, FLAMMABILITY_F4, FLAMMABILITY_F5, 
                                EDGECOMB_W1, EDGECOMB_W2, EDGECOMB_W3, EDGECOMB_F1, EDGECOMB_F2, EDGECOMB_F3,
                                STIFFNESS_W1, STIFFNESS_W2, STIFFNESS_W3, STIFFNESS_F1, STIFFNESS_F2, STIFFNESS_F3,
                                TEAR_W1, TEAR_W2, TEAR_W3, TEAR_F1, TEAR_F2, TEAR_F3,
                                STATIC_AIR1, STATIC_AIR2, STATIC_AIR3, STATIC_AIR4, STATIC_AIR5, STATIC_AIR6,
                                DYNAMIC_AIR1, DYNAMIC_AIR2, DYNAMIC_AIR3,
                                EXPONENT1, EXPONENT2, EXPONENT3,
                                DIMENSCHANGE_W1, DIMENSCHANGE_W2, DIMENSCHANGE_W3, DIMENSCHANGE_F1, DIMENSCHANGE_F2, DIMENSCHANGE_F3,
                                FLEXABRASION_W1, FLEXABRASION_W2, FLEXABRASION_W3, FLEXABRASION_F1, FLEXABRASION_F2, FLEXABRASION_F3,
                                STATUS, REMARK, APPROVEBY, APPROVEDATE, CREATEDATE, FINISHLENGTH, FINISHINGPROCESS, LOOMNO, FINISHINGMC,
                                BOW1, BOW2, BOW3, SKEW1, SKEW2, SKEW3, BENDING_W1, BENDING_W2, BENDING_W3, BENDING_F1, BENDING_F2, BENDING_F3,
                                FLEX_SCOTT_W1, FLEX_SCOTT_W2, FLEX_SCOTT_W3, FLEX_SCOTT_F1, FLEX_SCOTT_F2, FLEX_SCOTT_F3, ITEMLOT);
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

        #region ExportExcelforQDAS
        private void ExportExcelforQDAS()
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

                        if (DataTable_To_ExcelQDAS(newFileName) == true)
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

        #region DataTable_To_ExcelQDAS
        private bool DataTable_To_ExcelQDAS(string fileName)
        {
            try
            {
                System.Data.DataTable dt = ExportToExcelforQDAS();

                DataToExcelQDAS(dt, fileName);

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.Err();

                return false;
            }
        }
        #endregion

        #region DataToExcelQDAS
        private void DataToExcelQDAS(DataTable dt, string fileName)
        {
            using (dt)
            {
                double value;
    
                if (fileName.Contains(".xlsx") == true)
                {
                    XSSFWorkbook workbook = new XSSFWorkbook();//Create an excel Workbook
                    XSSFSheet sheet = (XSSFSheet)workbook.CreateSheet("For Q-Das (Data Stack)");//Create a work table in the table
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

                    #region headerRow

                    headerRow.CreateCell(0).SetCellValue("Lot no.");
                    headerRow.CreateCell(1).SetCellValue("Batch no.");
                    headerRow.CreateCell(2).SetCellValue("Width");
                    headerRow.CreateCell(3).SetCellValue("Usable Width");
                    headerRow.CreateCell(4).SetCellValue("Width of Silicone");
                    headerRow.CreateCell(5).SetCellValue("Number of threads (fabric sett)");
                    headerRow.CreateCell(6).SetCellValue("");
                    headerRow.CreateCell(7).SetCellValue("Total Weight");
                    headerRow.CreateCell(8).SetCellValue("Uncoated weight");
                    headerRow.CreateCell(9).SetCellValue("Total coating weight");
                    headerRow.CreateCell(10).SetCellValue("Thickness");
                    headerRow.CreateCell(11).SetCellValue("Maximum Force");
                    headerRow.CreateCell(12).SetCellValue("");
                    headerRow.CreateCell(13).SetCellValue("Elongation at Maximum Force");
                    headerRow.CreateCell(14).SetCellValue("");
                    headerRow.CreateCell(15).SetCellValue("Flammability");
                    headerRow.CreateCell(16).SetCellValue("");
                    headerRow.CreateCell(17).SetCellValue("Edgecomb Resistance");
                    headerRow.CreateCell(18).SetCellValue("");
                    headerRow.CreateCell(19).SetCellValue("Stiffness");
                    headerRow.CreateCell(20).SetCellValue("");
                    headerRow.CreateCell(21).SetCellValue("Tear Force");
                    headerRow.CreateCell(22).SetCellValue("");
                    headerRow.CreateCell(23).SetCellValue("Static Air Permeability");
                    headerRow.CreateCell(24).SetCellValue("Dynamic Air Permeability");
                    headerRow.CreateCell(25).SetCellValue("Exponent");
                    headerRow.CreateCell(26).SetCellValue("Dimensional Change");
                    headerRow.CreateCell(27).SetCellValue("");
                    headerRow.CreateCell(28).SetCellValue("Flex Abrasion (scrub)");
                    headerRow.CreateCell(29).SetCellValue("");
                    headerRow.CreateCell(30).SetCellValue("Bow");
                    headerRow.CreateCell(31).SetCellValue("Skew");
                    headerRow.CreateCell(32).SetCellValue("Bending resistance");
                    headerRow.CreateCell(33).SetCellValue("");
                    headerRow.CreateCell(34).SetCellValue("Flex Abrasion (Scott scrub)");
                    headerRow.CreateCell(35).SetCellValue("");

                    headerRow.Cells[0].CellStyle = headerCellStyle;
                    headerRow.Cells[1].CellStyle = headerCellStyle;
                    headerRow.Cells[2].CellStyle = headerCellStyle;
                    headerRow.Cells[3].CellStyle = headerCellStyle;
                    headerRow.Cells[4].CellStyle = headerCellStyle;
                    headerRow.Cells[5].CellStyle = headerCellStyle;
                    headerRow.Cells[6].CellStyle = headerCellStyle;
                    headerRow.Cells[7].CellStyle = headerCellStyle;
                    headerRow.Cells[8].CellStyle = headerCellStyle;
                    headerRow.Cells[9].CellStyle = headerCellStyle;
                    headerRow.Cells[10].CellStyle = headerCellStyle;
                    headerRow.Cells[11].CellStyle = headerCellStyle;
                    headerRow.Cells[12].CellStyle = headerCellStyle;
                    headerRow.Cells[13].CellStyle = headerCellStyle;
                    headerRow.Cells[14].CellStyle = headerCellStyle;
                    headerRow.Cells[15].CellStyle = headerCellStyle;
                    headerRow.Cells[16].CellStyle = headerCellStyle;
                    headerRow.Cells[17].CellStyle = headerCellStyle;
                    headerRow.Cells[18].CellStyle = headerCellStyle;
                    headerRow.Cells[19].CellStyle = headerCellStyle;
                    headerRow.Cells[20].CellStyle = headerCellStyle;
                    headerRow.Cells[21].CellStyle = headerCellStyle;
                    headerRow.Cells[22].CellStyle = headerCellStyle;
                    headerRow.Cells[23].CellStyle = headerCellStyle;
                    headerRow.Cells[24].CellStyle = headerCellStyle;
                    headerRow.Cells[25].CellStyle = headerCellStyle;
                    headerRow.Cells[26].CellStyle = headerCellStyle;
                    headerRow.Cells[27].CellStyle = headerCellStyle;
                    headerRow.Cells[28].CellStyle = headerCellStyle;
                    headerRow.Cells[29].CellStyle = headerCellStyle;
                    headerRow.Cells[30].CellStyle = headerCellStyle;
                    headerRow.Cells[31].CellStyle = headerCellStyle;
                    headerRow.Cells[32].CellStyle = headerCellStyle;
                    headerRow.Cells[33].CellStyle = headerCellStyle;
                    headerRow.Cells[34].CellStyle = headerCellStyle;
                    headerRow.Cells[35].CellStyle = headerCellStyle;

                    headerRow.Cells[0].CellStyle.SetFont(font);

                    var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 5, 6);
                    var cra1 = new NPOI.SS.Util.CellRangeAddress(0, 0, 11, 12);
                    var cra2 = new NPOI.SS.Util.CellRangeAddress(0, 0, 13, 14);
                    var cra3 = new NPOI.SS.Util.CellRangeAddress(0, 0, 15, 16);

                    var cra4 = new NPOI.SS.Util.CellRangeAddress(0, 0, 17, 18);
                    var cra5 = new NPOI.SS.Util.CellRangeAddress(0, 0, 19, 20);
                    var cra6 = new NPOI.SS.Util.CellRangeAddress(0, 0, 21, 22);

                    var cra7 = new NPOI.SS.Util.CellRangeAddress(0, 0, 26, 27);
                    var cra8 = new NPOI.SS.Util.CellRangeAddress(0, 0, 28, 29);
                    var cra9 = new NPOI.SS.Util.CellRangeAddress(0, 0, 32, 33);
                    var cra10 = new NPOI.SS.Util.CellRangeAddress(0, 0, 34, 35);

                    sheet.AddMergedRegion(cra);
                    sheet.AddMergedRegion(cra1);
                    sheet.AddMergedRegion(cra2);
                    sheet.AddMergedRegion(cra3);
                    sheet.AddMergedRegion(cra4);
                    sheet.AddMergedRegion(cra5);
                    sheet.AddMergedRegion(cra6);
                    sheet.AddMergedRegion(cra7);
                    sheet.AddMergedRegion(cra8);
                    sheet.AddMergedRegion(cra9);
                    sheet.AddMergedRegion(cra10);

                    sheet.AutoSizeColumn(1);
                    sheet.AutoSizeColumn(2);
                    sheet.AutoSizeColumn(3);
                    sheet.AutoSizeColumn(4);
                    sheet.AutoSizeColumn(5);
                    sheet.AutoSizeColumn(6);
                    sheet.AutoSizeColumn(7);
                    sheet.AutoSizeColumn(8);
                    sheet.AutoSizeColumn(9);
                    sheet.AutoSizeColumn(10);
                    sheet.AutoSizeColumn(11);
                    sheet.AutoSizeColumn(12);
                    sheet.AutoSizeColumn(13);
                    sheet.AutoSizeColumn(14);
                    sheet.AutoSizeColumn(15);
                    sheet.AutoSizeColumn(16);
                    sheet.AutoSizeColumn(17);
                    sheet.AutoSizeColumn(18);
                    sheet.AutoSizeColumn(19);
                    sheet.AutoSizeColumn(20);
                    sheet.AutoSizeColumn(21);
                    sheet.AutoSizeColumn(22);
                    sheet.AutoSizeColumn(23);
                    sheet.AutoSizeColumn(24);
                    sheet.AutoSizeColumn(25);
                    sheet.AutoSizeColumn(26);
                    sheet.AutoSizeColumn(27);
                    sheet.AutoSizeColumn(28);
                    sheet.AutoSizeColumn(29);
                    sheet.AutoSizeColumn(30);
                    sheet.AutoSizeColumn(31);
                    sheet.AutoSizeColumn(32);
                    sheet.AutoSizeColumn(33);
                    sheet.AutoSizeColumn(34);
                    sheet.AutoSizeColumn(35);

                    #endregion

                    headerRow = (XSSFRow)sheet.CreateRow(1);

                    foreach (DataColumn column in dt.Columns)
                    {
                        //headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);

                        #region headerRow

                        if (column.Caption == "WEAVINGLOT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("");
                            headerRow.Cells[0].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "LOOMNO")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("");
                            headerRow.Cells[1].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "WIDTH")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[2].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "USABLE_WIDTH")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[3].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "WIDTH_SILICONE")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[4].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "NUMTHREADS_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[5].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "NUMTHREADS_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[6].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "TOTALWEIGHT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[7].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "UNCOATEDWEIGHT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[8].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "COATINGWEIGHT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[9].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "THICKNESS")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[10].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "MAXFORCE_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[11].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "MAXFORCE_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[12].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "ELONGATIONFORCE_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[13].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "ELONGATIONFORCE_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[14].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLAMMABILITY_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[15].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLAMMABILITY_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[16].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "EDGECOMB_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[17].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "EDGECOMB_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[18].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "STIFFNESS_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[19].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "STIFFNESS_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[20].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "TEAR_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[21].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "TEAR_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[22].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "STATIC_AIR")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[23].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "DYNAMIC_AIR")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[24].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "EXPONENT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[25].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "DIMENSCHANGE_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[26].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "DIMENSCHANGE_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[27].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEXABRASION_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[28].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEXABRASION_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[29].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "BOW")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[30].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "SKEW")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[31].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "BENDING_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[32].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "BENDING_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[33].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEX_SCOTT_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[34].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEX_SCOTT_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[35].CellStyle = headerCellStyle;
                        }

                        #endregion
                    }

                    var cra11 = new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 0);
                    var cra12 = new NPOI.SS.Util.CellRangeAddress(0, 1, 1, 1);
                    sheet.AddMergedRegion(cra11);
                    sheet.AddMergedRegion(cra12);

                    int rowIndex = 2;
                    foreach (DataRow row in dt.Rows)
                    {
                        XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in dt.Columns)
                        {
                            //dataRow.CreateCell(column.Ordinal, CellType.Numeric).SetCellValue(row[column].ToString());

                            #region dataRow

                            if (row[column] != null)
                            {
                                if (!string.IsNullOrEmpty(row[column].ToString()) && row[column].ToString().Trim() != "")
                                {
                                    if (double.TryParse(row[column].ToString(), out value))
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(double.Parse(row[column].ToString()));
                                    }
                                    else
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                    }
                                }
                                else
                                {
                                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                }

                                if (column.Caption == "WEAVINGLOT")
                                {
                                    dataRow.Cells[0].CellStyle = headerCellStyle;
                                    dataRow.Cells[0].CellStyle.SetFont(font);
                                }
                                else if (column.Caption == "LOOMNO")
                                {
                                    dataRow.Cells[1].CellStyle = headerCellStyle;
                                    dataRow.Cells[1].CellStyle.SetFont(font);
                                }
                                else if (column.Caption == "WIDTH")
                                {
                                    dataRow.Cells[2].CellStyle = detailCellStyle;
                                    dataRow.Cells[2].CellStyle.SetFont(font);
                                }
                                else if (column.Caption == "USABLE_WIDTH")
                                {
                                    dataRow.Cells[3].CellStyle = detailCellStyle;
                                    dataRow.Cells[3].CellStyle.SetFont(font);
                                }
                                else if (column.Caption == "WIDTH_SILICONE")
                                {
                                    dataRow.Cells[4].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "NUMTHREADS_W")
                                {
                                    dataRow.Cells[5].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "NUMTHREADS_F")
                                {
                                    dataRow.Cells[6].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "TOTALWEIGHT")
                                {
                                    dataRow.Cells[7].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "UNCOATEDWEIGHT")
                                {
                                    dataRow.Cells[8].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "COATINGWEIGHT")
                                {
                                    dataRow.Cells[9].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "THICKNESS")
                                {
                                    dataRow.Cells[10].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "MAXFORCE_W")
                                {
                                    dataRow.Cells[11].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "MAXFORCE_F")
                                {
                                    dataRow.Cells[12].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "ELONGATIONFORCE_W")
                                {
                                    dataRow.Cells[13].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "ELONGATIONFORCE_F")
                                {
                                    dataRow.Cells[14].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLAMMABILITY_W")
                                {
                                    dataRow.Cells[15].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLAMMABILITY_F")
                                {
                                    dataRow.Cells[16].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "EDGECOMB_W")
                                {
                                    dataRow.Cells[17].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "EDGECOMB_F")
                                {
                                    dataRow.Cells[18].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "STIFFNESS_W")
                                {
                                    dataRow.Cells[19].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "STIFFNESS_F")
                                {
                                    dataRow.Cells[20].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "TEAR_W")
                                {
                                    dataRow.Cells[21].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "TEAR_F")
                                {
                                    dataRow.Cells[22].CellStyle = detailCellStyle;
                                }

                                else if (column.Caption == "STATIC_AIR")
                                {
                                    dataRow.Cells[23].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "DYNAMIC_AIR")
                                {
                                    dataRow.Cells[24].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "EXPONENT")
                                {
                                    dataRow.Cells[25].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "DIMENSCHANGE_W")
                                {
                                    dataRow.Cells[26].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "DIMENSCHANGE_F")
                                {
                                    dataRow.Cells[27].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEXABRASION_W")
                                {
                                    dataRow.Cells[28].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEXABRASION_F")
                                {
                                    dataRow.Cells[29].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "BOW")
                                {
                                    dataRow.Cells[30].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "SKEW")
                                {
                                    dataRow.Cells[31].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "BENDING_W")
                                {
                                    dataRow.Cells[32].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "BENDING_F")
                                {
                                    dataRow.Cells[33].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEX_SCOTT_W")
                                {
                                    dataRow.Cells[34].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEX_SCOTT_F")
                                {
                                    dataRow.Cells[35].CellStyle = detailCellStyle;
                                }
                            }

                            #endregion
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
                    ISheet sheet = workbook.CreateSheet("For Q-Das (Data Stack)");//Create a work table in the table
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

                    #region headerRow

                    headerRow.CreateCell(0).SetCellValue("Lot no.");
                    headerRow.CreateCell(1).SetCellValue("Batch no.");
                    headerRow.CreateCell(2).SetCellValue("Width");
                    headerRow.CreateCell(3).SetCellValue("Usable Width");
                    headerRow.CreateCell(4).SetCellValue("Width of Silicone");
                    headerRow.CreateCell(5).SetCellValue("Number of threads (fabric sett)");
                    headerRow.CreateCell(6).SetCellValue("");
                    headerRow.CreateCell(7).SetCellValue("Total Weight");
                    headerRow.CreateCell(8).SetCellValue("Uncoated weight");
                    headerRow.CreateCell(9).SetCellValue("Total coating weight");
                    headerRow.CreateCell(10).SetCellValue("Thickness");
                    headerRow.CreateCell(11).SetCellValue("Maximum Force");
                    headerRow.CreateCell(12).SetCellValue("");
                    headerRow.CreateCell(13).SetCellValue("Elongation at Maximum Force");
                    headerRow.CreateCell(14).SetCellValue("");
                    headerRow.CreateCell(15).SetCellValue("Flammability");
                    headerRow.CreateCell(16).SetCellValue("");
                    headerRow.CreateCell(17).SetCellValue("Edgecomb Resistance");
                    headerRow.CreateCell(18).SetCellValue("");
                    headerRow.CreateCell(19).SetCellValue("Stiffness");
                    headerRow.CreateCell(20).SetCellValue("");
                    headerRow.CreateCell(21).SetCellValue("Tear Force");
                    headerRow.CreateCell(22).SetCellValue("");
                    headerRow.CreateCell(23).SetCellValue("Static Air Permeability");
                    headerRow.CreateCell(24).SetCellValue("Dynamic Air Permeability");
                    headerRow.CreateCell(25).SetCellValue("Exponent");
                    headerRow.CreateCell(26).SetCellValue("Dimensional Change");
                    headerRow.CreateCell(27).SetCellValue("");
                    headerRow.CreateCell(28).SetCellValue("Flex Abrasion (scrub)");
                    headerRow.CreateCell(29).SetCellValue("");
                    headerRow.CreateCell(30).SetCellValue("Bow");
                    headerRow.CreateCell(31).SetCellValue("Skew");
                    headerRow.CreateCell(32).SetCellValue("Bending resistance");
                    headerRow.CreateCell(33).SetCellValue("");
                    headerRow.CreateCell(34).SetCellValue("Flex Abrasion (Scott scrub)");
                    headerRow.CreateCell(35).SetCellValue("");

                    headerRow.Cells[0].CellStyle = headerCellStyle;
                    headerRow.Cells[1].CellStyle = headerCellStyle;
                    headerRow.Cells[2].CellStyle = headerCellStyle;
                    headerRow.Cells[3].CellStyle = headerCellStyle;
                    headerRow.Cells[4].CellStyle = headerCellStyle;
                    headerRow.Cells[5].CellStyle = headerCellStyle;
                    headerRow.Cells[6].CellStyle = headerCellStyle;
                    headerRow.Cells[7].CellStyle = headerCellStyle;
                    headerRow.Cells[8].CellStyle = headerCellStyle;
                    headerRow.Cells[9].CellStyle = headerCellStyle;
                    headerRow.Cells[10].CellStyle = headerCellStyle;
                    headerRow.Cells[11].CellStyle = headerCellStyle;
                    headerRow.Cells[12].CellStyle = headerCellStyle;
                    headerRow.Cells[13].CellStyle = headerCellStyle;
                    headerRow.Cells[14].CellStyle = headerCellStyle;
                    headerRow.Cells[15].CellStyle = headerCellStyle;
                    headerRow.Cells[16].CellStyle = headerCellStyle;
                    headerRow.Cells[17].CellStyle = headerCellStyle;
                    headerRow.Cells[18].CellStyle = headerCellStyle;
                    headerRow.Cells[19].CellStyle = headerCellStyle;
                    headerRow.Cells[20].CellStyle = headerCellStyle;
                    headerRow.Cells[21].CellStyle = headerCellStyle;
                    headerRow.Cells[22].CellStyle = headerCellStyle;
                    headerRow.Cells[23].CellStyle = headerCellStyle;
                    headerRow.Cells[24].CellStyle = headerCellStyle;
                    headerRow.Cells[25].CellStyle = headerCellStyle;
                    headerRow.Cells[26].CellStyle = headerCellStyle;
                    headerRow.Cells[27].CellStyle = headerCellStyle;
                    headerRow.Cells[28].CellStyle = headerCellStyle;
                    headerRow.Cells[29].CellStyle = headerCellStyle;
                    headerRow.Cells[30].CellStyle = headerCellStyle;
                    headerRow.Cells[31].CellStyle = headerCellStyle;
                    headerRow.Cells[32].CellStyle = headerCellStyle;
                    headerRow.Cells[33].CellStyle = headerCellStyle;
                    headerRow.Cells[34].CellStyle = headerCellStyle;
                    headerRow.Cells[35].CellStyle = headerCellStyle;

                    headerRow.Cells[0].CellStyle.SetFont(font);

                    var cra = new NPOI.SS.Util.CellRangeAddress(0, 0, 5, 6);
                    var cra1 = new NPOI.SS.Util.CellRangeAddress(0, 0, 11, 12);
                    var cra2 = new NPOI.SS.Util.CellRangeAddress(0, 0, 13, 14);
                    var cra3 = new NPOI.SS.Util.CellRangeAddress(0, 0, 15, 16);

                    var cra4 = new NPOI.SS.Util.CellRangeAddress(0, 0, 17, 18);
                    var cra5 = new NPOI.SS.Util.CellRangeAddress(0, 0, 19, 20);
                    var cra6 = new NPOI.SS.Util.CellRangeAddress(0, 0, 21, 22);

                    var cra7 = new NPOI.SS.Util.CellRangeAddress(0, 0, 26, 27);
                    var cra8 = new NPOI.SS.Util.CellRangeAddress(0, 0, 28, 29);
                    var cra9 = new NPOI.SS.Util.CellRangeAddress(0, 0, 32, 33);
                    var cra10 = new NPOI.SS.Util.CellRangeAddress(0, 0, 34, 35);

                    sheet.AddMergedRegion(cra);
                    sheet.AddMergedRegion(cra1);
                    sheet.AddMergedRegion(cra2);
                    sheet.AddMergedRegion(cra3);
                    sheet.AddMergedRegion(cra4);
                    sheet.AddMergedRegion(cra5);
                    sheet.AddMergedRegion(cra6);
                    sheet.AddMergedRegion(cra7);
                    sheet.AddMergedRegion(cra8);
                    sheet.AddMergedRegion(cra9);
                    sheet.AddMergedRegion(cra10);

                    sheet.AutoSizeColumn(1);
                    sheet.AutoSizeColumn(2);
                    sheet.AutoSizeColumn(3);
                    sheet.AutoSizeColumn(4);
                    sheet.AutoSizeColumn(5);
                    sheet.AutoSizeColumn(6);
                    sheet.AutoSizeColumn(7);
                    sheet.AutoSizeColumn(8);
                    sheet.AutoSizeColumn(9);
                    sheet.AutoSizeColumn(10);
                    sheet.AutoSizeColumn(11);
                    sheet.AutoSizeColumn(12);
                    sheet.AutoSizeColumn(13);
                    sheet.AutoSizeColumn(14);
                    sheet.AutoSizeColumn(15);
                    sheet.AutoSizeColumn(16);
                    sheet.AutoSizeColumn(17);
                    sheet.AutoSizeColumn(18);
                    sheet.AutoSizeColumn(19);
                    sheet.AutoSizeColumn(20);
                    sheet.AutoSizeColumn(21);
                    sheet.AutoSizeColumn(22);
                    sheet.AutoSizeColumn(23);
                    sheet.AutoSizeColumn(24);
                    sheet.AutoSizeColumn(25);
                    sheet.AutoSizeColumn(26);
                    sheet.AutoSizeColumn(27);
                    sheet.AutoSizeColumn(28);
                    sheet.AutoSizeColumn(29);
                    sheet.AutoSizeColumn(30);
                    sheet.AutoSizeColumn(31);
                    sheet.AutoSizeColumn(32);
                    sheet.AutoSizeColumn(33);
                    sheet.AutoSizeColumn(34);
                    sheet.AutoSizeColumn(35);

                    #endregion

                    headerRow = sheet.CreateRow(1); //To add a row in the table

                    foreach (DataColumn column in dt.Columns)
                    {
                        //headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);

                        #region headerRow

                        if (column.Caption == "WEAVINGLOT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("");
                            headerRow.Cells[0].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "LOOMNO")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("");
                            headerRow.Cells[1].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "WIDTH")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[2].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "USABLE_WIDTH")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[3].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "WIDTH_SILICONE")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[4].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "NUMTHREADS_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[5].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "NUMTHREADS_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[6].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "TOTALWEIGHT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[7].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "UNCOATEDWEIGHT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[8].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "COATINGWEIGHT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[9].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "THICKNESS")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[10].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "MAXFORCE_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[11].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "MAXFORCE_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[12].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "ELONGATIONFORCE_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[13].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "ELONGATIONFORCE_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[14].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLAMMABILITY_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[15].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLAMMABILITY_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[16].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "EDGECOMB_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[17].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "EDGECOMB_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[18].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "STIFFNESS_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[19].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "STIFFNESS_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[20].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "TEAR_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[21].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "TEAR_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[22].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "STATIC_AIR")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[23].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "DYNAMIC_AIR")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[24].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "EXPONENT")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[25].CellStyle = headerCellStyle;
                        }

                        else if (column.Caption == "DIMENSCHANGE_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[26].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "DIMENSCHANGE_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[27].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEXABRASION_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[28].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEXABRASION_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[29].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "BOW")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[30].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "SKEW")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("-");
                            headerRow.Cells[31].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "BENDING_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[32].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "BENDING_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[33].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEX_SCOTT_W")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Warp");
                            headerRow.Cells[34].CellStyle = headerCellStyle;
                        }
                        else if (column.Caption == "FLEX_SCOTT_F")
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue("Weft");
                            headerRow.Cells[35].CellStyle = headerCellStyle;
                        }

                        #endregion
                    }

                    var cra11 = new NPOI.SS.Util.CellRangeAddress(0, 1, 0, 0);
                    var cra12 = new NPOI.SS.Util.CellRangeAddress(0, 1, 1, 1);
                    sheet.AddMergedRegion(cra11);
                    sheet.AddMergedRegion(cra12);

                    int rowIndex = 2;
                    foreach (DataRow row in dt.Rows)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        foreach (DataColumn column in dt.Columns)
                        {
                            #region dataRow

                            if (row[column] != null)
                            {
                                if (!string.IsNullOrEmpty(row[column].ToString()) && row[column].ToString().Trim() != "")
                                {
                                    if (double.TryParse(row[column].ToString(), out value))
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(double.Parse(row[column].ToString()));
                                    }
                                    else
                                    {
                                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                    }
                                }
                                else
                                {
                                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                                }

                                if (column.Caption == "WEAVINGLOT")
                                {
                                    dataRow.Cells[0].CellStyle = headerCellStyle;
                                    dataRow.Cells[0].CellStyle.SetFont(font);
                                }
                                else if (column.Caption == "LOOMNO")
                                {
                                    dataRow.Cells[1].CellStyle = headerCellStyle;
                                }
                                else if (column.Caption == "WIDTH")
                                {
                                    dataRow.Cells[2].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "USABLE_WIDTH")
                                {
                                    dataRow.Cells[3].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "WIDTH_SILICONE")
                                {
                                    dataRow.Cells[4].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "NUMTHREADS_W")
                                {
                                    dataRow.Cells[5].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "NUMTHREADS_F")
                                {
                                    dataRow.Cells[6].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "TOTALWEIGHT")
                                {
                                    dataRow.Cells[7].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "UNCOATEDWEIGHT")
                                {
                                    dataRow.Cells[8].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "COATINGWEIGHT")
                                {
                                    dataRow.Cells[9].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "THICKNESS")
                                {
                                    dataRow.Cells[10].CellStyle = detailCellStyle;
                                }

                                else if (column.Caption == "MAXFORCE_W")
                                {
                                    dataRow.Cells[11].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "MAXFORCE_F")
                                {
                                    dataRow.Cells[12].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "ELONGATIONFORCE_W")
                                {
                                    dataRow.Cells[13].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "ELONGATIONFORCE_F")
                                {
                                    dataRow.Cells[14].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLAMMABILITY_W")
                                {
                                    dataRow.Cells[15].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLAMMABILITY_F")
                                {
                                    dataRow.Cells[16].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "EDGECOMB_W")
                                {
                                    dataRow.Cells[17].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "EDGECOMB_F")
                                {
                                    dataRow.Cells[18].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "STIFFNESS_W")
                                {
                                    dataRow.Cells[19].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "STIFFNESS_F")
                                {
                                    dataRow.Cells[20].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "TEAR_W")
                                {
                                    dataRow.Cells[21].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "TEAR_F")
                                {
                                    dataRow.Cells[22].CellStyle = detailCellStyle;
                                }

                                else if (column.Caption == "STATIC_AIR")
                                {
                                    dataRow.Cells[23].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "DYNAMIC_AIR")
                                {
                                    dataRow.Cells[24].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "EXPONENT")
                                {
                                    dataRow.Cells[25].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "DIMENSCHANGE_W")
                                {
                                    dataRow.Cells[26].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "DIMENSCHANGE_F")
                                {
                                    dataRow.Cells[27].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEXABRASION_W")
                                {
                                    dataRow.Cells[28].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEXABRASION_F")
                                {
                                    dataRow.Cells[29].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "BOW")
                                {
                                    dataRow.Cells[30].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "SKEW")
                                {
                                    dataRow.Cells[31].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "BENDING_W")
                                {
                                    dataRow.Cells[32].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "BENDING_F")
                                {
                                    dataRow.Cells[33].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEX_SCOTT_W")
                                {
                                    dataRow.Cells[34].CellStyle = detailCellStyle;
                                }
                                else if (column.Caption == "FLEX_SCOTT_F")
                                {
                                    dataRow.Cells[35].CellStyle = detailCellStyle;
                                }
                            }

                            #endregion
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

        #region LoadData
        private List<LAB_SEARCHLABENTRYPRODUCTION_QDAS> LoadDataQDAS()
        {
            try
            {
                string P_ITMCODE = string.Empty;
                string P_ENTRYSTARTDATE = string.Empty;
                string P_ENTRYENDDATE = string.Empty;
                string P_LOOM = string.Empty;
                string P_FINISHPROCESS = string.Empty;

                if (cbItemCode.SelectedValue != null)
                {
                    if (cbItemCode.SelectedValue.ToString() != "All")
                        P_ITMCODE = cbItemCode.SelectedValue.ToString();
                }

                if (cbMCNAME.SelectedValue != null)
                {
                    if (cbMCNAME.SelectedValue.ToString() != "All")
                        P_LOOM = cbMCNAME.SelectedValue.ToString();
                }

                if (cbFinishingProcess.SelectedValue != null)
                {
                    if (cbFinishingProcess.SelectedValue.ToString() != "All")
                        P_FINISHPROCESS = cbFinishingProcess.SelectedValue.ToString();
                }

                if (dteENTRYSTARTDATE.SelectedDate != null)
                    P_ENTRYSTARTDATE = dteENTRYSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

                if (dteENTRYENDDATE.SelectedDate != null)
                    P_ENTRYENDDATE = dteENTRYENDDATE.SelectedDate.Value.ToString("dd/MM/yyyy");

                // 1 Row
                List<LAB_SEARCHLABENTRYPRODUCTION> results = LabDataPDFDataService.Instance.LAB_SEARCHLABENTRYPRODUCTION(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS);
                List<LAB_SEARCHLABENTRYPRODUCTION_1ROW> results_1Row = new List<LAB_SEARCHLABENTRYPRODUCTION_1ROW>();
                List<LAB_WIDTH_1ROW> widths_1Row = new List<LAB_WIDTH_1ROW>();
                List<LAB_FLAMMABILITY_1ROW> flammabilitys_1Row = new List<LAB_FLAMMABILITY_1ROW>();
                List<LAB_REMARK_1ROW> remarks_1Row = new List<LAB_REMARK_1ROW>();
                List<LAB_APPROVE_1ROW> approves_1Row = new List<LAB_APPROVE_1ROW>();

                // 3 Row
                List<LAB_USABLE_WIDTH_3ROW> usable_widths_3Row = new List<LAB_USABLE_WIDTH_3ROW>();
                List<LAB_WIDTH_SILICONE_3ROW> width_silicones_3Row = new List<LAB_WIDTH_SILICONE_3ROW>();
                List<LAB_NUMTHREADS_3ROW> numthreadss_3Row = new List<LAB_NUMTHREADS_3ROW>();
                List<LAB_THICKNESS_3ROW> thicknesss_3Row = new List<LAB_THICKNESS_3ROW>();
                List<LAB_MAXFORCE_3ROW> maxforces_3Row = new List<LAB_MAXFORCE_3ROW>();
                List<LAB_ELONGATIONFORCE_3ROW> elongationforces_3Row = new List<LAB_ELONGATIONFORCE_3ROW>();
                List<LAB_EDGECOMB_3ROW> edgecombs_3Row = new List<LAB_EDGECOMB_3ROW>();
                List<LAB_STIFFNESS_3ROW> stiffnesss_3Row = new List<LAB_STIFFNESS_3ROW>();
                List<LAB_TEAR_3ROW> tears_3Row = new List<LAB_TEAR_3ROW>();
                
                List<LAB_DYNAMIC_AIR_3ROW> dynamic_airs_3Row = new List<LAB_DYNAMIC_AIR_3ROW>();
                List<LAB_EXPONENT_3ROW> exponents_3Row = new List<LAB_EXPONENT_3ROW>();
                List<LAB_DIMENSCHANGE_3ROW> dimenschanges_3Row = new List<LAB_DIMENSCHANGE_3ROW>();
                List<LAB_FLEXABRASION_3ROW> flexabrasions_3Row = new List<LAB_FLEXABRASION_3ROW>();
                List<LAB_BOW_3ROW> bows_3Row = new List<LAB_BOW_3ROW>();
                List<LAB_SKEW_3ROW> skews_3Row = new List<LAB_SKEW_3ROW>();
                List<LAB_BENDING_3ROW> bendings_3Row = new List<LAB_BENDING_3ROW>();
                List<LAB_FLEX_SCOTT_3ROW> flex_scotts_3Row = new List<LAB_FLEX_SCOTT_3ROW>();

                List<LAB_TOTALWEIGHT_6ROW> totalweights_6Row = new List<LAB_TOTALWEIGHT_6ROW>();
                List<LAB_UNCOATEDWEIGHT_6ROW> uncoatedweights_6Row = new List<LAB_UNCOATEDWEIGHT_6ROW>();
                List<LAB_COATINGWEIGHT_6ROW> coatingweights_6Row = new List<LAB_COATINGWEIGHT_6ROW>();
                List<LAB_STATIC_AIR_6ROW> static_airs_6Row = new List<LAB_STATIC_AIR_6ROW>();

                List<LAB_SEARCHLABENTRYPRODUCTION_QDAS> results_All = new List<LAB_SEARCHLABENTRYPRODUCTION_QDAS>();

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        int? Row1 = 0;
                        int? Row_width = 0;
                        int? Row_flammability = 0;
                        int? Row_remark = 0;
                        int? Row_approve = 0;

                        int? Row_usable_widt = 0;
                        int? Row_width_silicone = 0;
                        int? Row_numthreads = 0;
                        int? Row_thickness = 0;
                        int? Row_maxforce = 0;
                        int? Row_elongationforce = 0;
                        int? Row_edgecomb = 0;
                        int? Row_stiffness = 0;
                        int? Row_tear = 0;
                        int? Row_static_air = 0;
                        int? Row_dynamic_air = 0;
                        int? Row_exponent = 0;
                        int? Row_dimenschange = 0;
                        int? Row_flexabrasion = 0;
                        int? Row_bow = 0;
                        int? Row_skew = 0;
                        int? Row_bending = 0;
                        int? Row_flex_scott = 0;

                        int? Row_totalweight = 0;
                        int? Row_uncoatedweight = 0;
                        int? Row_coatingweight = 0;

                        for (int page = 0; page <= results.Count - 1; page++)
                        {
                            if (!string.IsNullOrEmpty(results[page].ITM_CODE))
                            {
                                LAB_SEARCHLABENTRYPRODUCTION_1ROW result_1Row = new LAB_SEARCHLABENTRYPRODUCTION_1ROW();
                                LAB_WIDTH_1ROW width_1Row = new LAB_WIDTH_1ROW();
                                LAB_FLAMMABILITY_1ROW flammability_1Row = new LAB_FLAMMABILITY_1ROW();
                                LAB_REMARK_1ROW remark_1Row = new LAB_REMARK_1ROW();
                                LAB_APPROVE_1ROW approve_1Row = new LAB_APPROVE_1ROW();

                                result_1Row.ITM_CODE = results[page].ITM_CODE;
                                result_1Row.WEAVINGLOT = results[page].WEAVINGLOT;
                                result_1Row.FINISHINGLOT = results[page].FINISHINGLOT;
                                result_1Row.ENTRYDATE = results[page].ENTRYDATE;
                                result_1Row.ENTEYBY = results[page].ENTEYBY;
                                result_1Row.STATUS = results[page].STATUS;
                                result_1Row.CREATEDATE = results[page].CREATEDATE;
                                result_1Row.FINISHLENGTH = results[page].FINISHLENGTH;
                                result_1Row.FINISHINGPROCESS = results[page].FINISHINGPROCESS;
                                result_1Row.LOOMNO = results[page].LOOMNO;
                                result_1Row.FINISHINGMC = results[page].FINISHINGMC;

                                // Update 06/07/18
                                result_1Row.ITEMLOT = results[page].ITEMLOT;

                                // Update 06/07/18
                                width_1Row.WIDTH = results[page].WIDTH;
                                flammability_1Row.FLAMMABILITY_W = results[page].FLAMMABILITY_W;
                                flammability_1Row.FLAMMABILITY_F = results[page].FLAMMABILITY_F;
                                remark_1Row.REMARK = results[page].REMARK;
                                approve_1Row.APPROVEBY = results[page].APPROVEBY;
                                approve_1Row.APPROVEDATE = results[page].APPROVEDATE;


                                for (int page_Row = 0; page_Row < 6; page_Row++)
                                {
                                    if (page_Row >= 0 && page_Row < 6)
                                    {

                                        LAB_TOTALWEIGHT_6ROW totalweight_6Row = new LAB_TOTALWEIGHT_6ROW();
                                        LAB_UNCOATEDWEIGHT_6ROW uncoatedweight_6Row = new LAB_UNCOATEDWEIGHT_6ROW();
                                        LAB_COATINGWEIGHT_6ROW coatingweight_6Row = new LAB_COATINGWEIGHT_6ROW();
                                        LAB_STATIC_AIR_6ROW static_air_6Row = new LAB_STATIC_AIR_6ROW();

                                        if (page_Row < 3)
                                        {
                                            LAB_USABLE_WIDTH_3ROW usable_width_3Row = new LAB_USABLE_WIDTH_3ROW();
                                            LAB_WIDTH_SILICONE_3ROW width_silicone_3Row = new LAB_WIDTH_SILICONE_3ROW();
                                            LAB_NUMTHREADS_3ROW numthreads_3Row = new LAB_NUMTHREADS_3ROW();
                                            LAB_THICKNESS_3ROW thickness_3Row = new LAB_THICKNESS_3ROW();
                                            LAB_MAXFORCE_3ROW maxforce_3Row = new LAB_MAXFORCE_3ROW();
                                            LAB_ELONGATIONFORCE_3ROW elongationforce_3Row = new LAB_ELONGATIONFORCE_3ROW();
                                            LAB_EDGECOMB_3ROW edgecomb_3Row = new LAB_EDGECOMB_3ROW();
                                            LAB_STIFFNESS_3ROW stiffness_3Row = new LAB_STIFFNESS_3ROW();
                                            LAB_TEAR_3ROW tear_3Row = new LAB_TEAR_3ROW();
                                            
                                            LAB_DYNAMIC_AIR_3ROW dynamic_air_3Row = new LAB_DYNAMIC_AIR_3ROW();
                                            LAB_EXPONENT_3ROW exponent_3Row = new LAB_EXPONENT_3ROW();
                                            LAB_DIMENSCHANGE_3ROW dimenschange_3Row = new LAB_DIMENSCHANGE_3ROW();
                                            LAB_FLEXABRASION_3ROW flexabrasion_3Row = new LAB_FLEXABRASION_3ROW();
                                            LAB_BOW_3ROW bow_3Row = new LAB_BOW_3ROW();
                                            LAB_SKEW_3ROW skew_3Row = new LAB_SKEW_3ROW();
                                            LAB_BENDING_3ROW bending_3Row = new LAB_BENDING_3ROW();
                                            LAB_FLEX_SCOTT_3ROW flex_scott_3Row = new LAB_FLEX_SCOTT_3ROW();

                                            if (page_Row == 0)
                                            {
                                                #region result_3Row

                                                if (results[page].USABLE_WIDTH1 != null)
                                                    usable_width_3Row.USABLE_WIDTH = results[page].USABLE_WIDTH1;

                                                if (results[page].WIDTH_SILICONE1 != null)
                                                    width_silicone_3Row.WIDTH_SILICONE = results[page].WIDTH_SILICONE1;

                                                if (results[page].NUMTHREADS_W1 != null)
                                                    numthreads_3Row.NUMTHREADS_W = results[page].NUMTHREADS_W1;

                                                if (results[page].NUMTHREADS_F1 != null)
                                                    numthreads_3Row.NUMTHREADS_F = results[page].NUMTHREADS_F1;

                                                if (results[page].THICKNESS1 != null)
                                                    thickness_3Row.THICKNESS = results[page].THICKNESS1;

                                                if (results[page].MAXFORCE_W1 != null)
                                                    maxforce_3Row.MAXFORCE_W = results[page].MAXFORCE_W1;

                                                if (results[page].MAXFORCE_F1 != null)
                                                    maxforce_3Row.MAXFORCE_F = results[page].MAXFORCE_F1;

                                                if (results[page].ELONGATIONFORCE_W1 != null)
                                                    elongationforce_3Row.ELONGATIONFORCE_W = results[page].ELONGATIONFORCE_W1;

                                                if (results[page].ELONGATIONFORCE_F1 != null)
                                                    elongationforce_3Row.ELONGATIONFORCE_F = results[page].ELONGATIONFORCE_F1;

                                                if (results[page].EDGECOMB_W1 != null)
                                                    edgecomb_3Row.EDGECOMB_W = results[page].EDGECOMB_W1;

                                                if (results[page].EDGECOMB_F1 != null)
                                                    edgecomb_3Row.EDGECOMB_F = results[page].EDGECOMB_F1;

                                                if (results[page].STIFFNESS_W1 != null)
                                                    stiffness_3Row.STIFFNESS_W = results[page].STIFFNESS_W1;

                                                if (results[page].STIFFNESS_F1 != null)
                                                    stiffness_3Row.STIFFNESS_F = results[page].STIFFNESS_F1;

                                                if (results[page].TEAR_W1 != null)
                                                    tear_3Row.TEAR_W = results[page].TEAR_W1;

                                                if (results[page].TEAR_F1 != null)
                                                    tear_3Row.TEAR_F = results[page].TEAR_F1;


                                                if (results[page].DYNAMIC_AIR1 != null)
                                                    dynamic_air_3Row.DYNAMIC_AIR = results[page].DYNAMIC_AIR1;

                                                if (results[page].EXPONENT1 != null)
                                                    exponent_3Row.EXPONENT = results[page].EXPONENT1;

                                                if (results[page].DIMENSCHANGE_W1 != null)
                                                    dimenschange_3Row.DIMENSCHANGE_W = results[page].DIMENSCHANGE_W1;

                                                if (results[page].DIMENSCHANGE_F1 != null)
                                                    dimenschange_3Row.DIMENSCHANGE_F = results[page].DIMENSCHANGE_F1;

                                                if (results[page].FLEXABRASION_W1 != null)
                                                    flexabrasion_3Row.FLEXABRASION_W = results[page].FLEXABRASION_W1;

                                                if (results[page].FLEXABRASION_F1 != null)
                                                    flexabrasion_3Row.FLEXABRASION_F = results[page].FLEXABRASION_F1;

                                                // Update 06/07/18
                                                if (results[page].BOW1 != null)
                                                    bow_3Row.BOW = results[page].BOW1;

                                                if (results[page].SKEW1 != null)
                                                    skew_3Row.SKEW = results[page].SKEW1;

                                                if (results[page].BENDING_W1 != null)
                                                    bending_3Row.BENDING_W = results[page].BENDING_W1;

                                                if (results[page].BENDING_F1 != null)
                                                    bending_3Row.BENDING_F = results[page].BENDING_F1;

                                                if (results[page].FLEX_SCOTT_W1 != null)
                                                    flex_scott_3Row.FLEX_SCOTT_W = results[page].FLEX_SCOTT_W1;

                                                if (results[page].FLEX_SCOTT_F1 != null)
                                                    flex_scott_3Row.FLEX_SCOTT_F = results[page].FLEX_SCOTT_F1;

                                                #endregion

                                                #region result_6Row

                                                if (results[page].TOTALWEIGHT1 != null)
                                                    totalweight_6Row.TOTALWEIGHT = results[page].TOTALWEIGHT1;

                                                if (results[page].UNCOATEDWEIGHT1 != null)
                                                    uncoatedweight_6Row.UNCOATEDWEIGHT = results[page].UNCOATEDWEIGHT1;

                                                if (results[page].COATINGWEIGHT1 != null)
                                                    coatingweight_6Row.COATINGWEIGHT = results[page].COATINGWEIGHT1;

                                                if (results[page].STATIC_AIR1 != null)
                                                    static_air_6Row.STATIC_AIR = results[page].STATIC_AIR1;

                                                #endregion

                                            }
                                            else if (page_Row == 1)
                                            {
                                                #region result_3Row

                                                if (results[page].USABLE_WIDTH2 != null)
                                                    usable_width_3Row.USABLE_WIDTH = results[page].USABLE_WIDTH2;

                                                if (results[page].WIDTH_SILICONE2 != null)
                                                    width_silicone_3Row.WIDTH_SILICONE = results[page].WIDTH_SILICONE2;

                                                if (results[page].NUMTHREADS_W2 != null)
                                                    numthreads_3Row.NUMTHREADS_W = results[page].NUMTHREADS_W2;

                                                if (results[page].NUMTHREADS_F2 != null)
                                                    numthreads_3Row.NUMTHREADS_F = results[page].NUMTHREADS_F2;

                                                if (results[page].THICKNESS2 != null)
                                                    thickness_3Row.THICKNESS = results[page].THICKNESS2;

                                                if (results[page].MAXFORCE_W2 != null)
                                                    maxforce_3Row.MAXFORCE_W = results[page].MAXFORCE_W2;

                                                if (results[page].MAXFORCE_F2 != null)
                                                    maxforce_3Row.MAXFORCE_F = results[page].MAXFORCE_F2;

                                                if (results[page].ELONGATIONFORCE_W2 != null)
                                                    elongationforce_3Row.ELONGATIONFORCE_W = results[page].ELONGATIONFORCE_W2;

                                                if (results[page].ELONGATIONFORCE_F2 != null)
                                                    elongationforce_3Row.ELONGATIONFORCE_F = results[page].ELONGATIONFORCE_F2;

                                                if (results[page].EDGECOMB_W2 != null)
                                                    edgecomb_3Row.EDGECOMB_W = results[page].EDGECOMB_W2;

                                                if (results[page].EDGECOMB_F2 != null)
                                                    edgecomb_3Row.EDGECOMB_F = results[page].EDGECOMB_F2;

                                                if (results[page].STIFFNESS_W2 != null)
                                                    stiffness_3Row.STIFFNESS_W = results[page].STIFFNESS_W2;

                                                if (results[page].STIFFNESS_F2 != null)
                                                    stiffness_3Row.STIFFNESS_F = results[page].STIFFNESS_F2;

                                                if (results[page].TEAR_W2 != null)
                                                    tear_3Row.TEAR_W = results[page].TEAR_W2;

                                                if (results[page].TEAR_F2 != null)
                                                    tear_3Row.TEAR_F = results[page].TEAR_F2;                                    

                                                if (results[page].DYNAMIC_AIR2 != null)
                                                    dynamic_air_3Row.DYNAMIC_AIR = results[page].DYNAMIC_AIR2;

                                                if (results[page].EXPONENT2 != null)
                                                    exponent_3Row.EXPONENT = results[page].EXPONENT2;

                                                if (results[page].DIMENSCHANGE_W2 != null)
                                                    dimenschange_3Row.DIMENSCHANGE_W = results[page].DIMENSCHANGE_W2;

                                                if (results[page].DIMENSCHANGE_F2 != null)
                                                    dimenschange_3Row.DIMENSCHANGE_F = results[page].DIMENSCHANGE_F2;

                                                if (results[page].FLEXABRASION_W2 != null)
                                                    flexabrasion_3Row.FLEXABRASION_W = results[page].FLEXABRASION_W2;

                                                if (results[page].FLEXABRASION_F2 != null)
                                                    flexabrasion_3Row.FLEXABRASION_F = results[page].FLEXABRASION_F2;

                                                // Update 06/07/18
                                                if (results[page].BOW2 != null)
                                                    bow_3Row.BOW = results[page].BOW2;

                                                if (results[page].SKEW2 != null)
                                                    skew_3Row.SKEW = results[page].SKEW2;

                                                if (results[page].BENDING_W2 != null)
                                                    bending_3Row.BENDING_W = results[page].BENDING_W2;

                                                if (results[page].BENDING_F2 != null)
                                                    bending_3Row.BENDING_F = results[page].BENDING_F2;

                                                if (results[page].FLEX_SCOTT_W2 != null)
                                                    flex_scott_3Row.FLEX_SCOTT_W = results[page].FLEX_SCOTT_W2;

                                                if (results[page].FLEX_SCOTT_F2 != null)
                                                    flex_scott_3Row.FLEX_SCOTT_F = results[page].FLEX_SCOTT_F2;


                                                #endregion

                                                #region result_6Row

                                                if (results[page].TOTALWEIGHT2 != null)
                                                    totalweight_6Row.TOTALWEIGHT = results[page].TOTALWEIGHT2;

                                                if (results[page].UNCOATEDWEIGHT2 != null)
                                                    uncoatedweight_6Row.UNCOATEDWEIGHT = results[page].UNCOATEDWEIGHT2;

                                                if (results[page].COATINGWEIGHT2 != null)
                                                    coatingweight_6Row.COATINGWEIGHT = results[page].COATINGWEIGHT2;

                                                if (results[page].STATIC_AIR2 != null)
                                                    static_air_6Row.STATIC_AIR = results[page].STATIC_AIR2;

                                                #endregion
                                            }
                                            else if (page_Row == 2)
                                            {
                                                #region result_3Row

                                                if (results[page].USABLE_WIDTH3 != null)
                                                    usable_width_3Row.USABLE_WIDTH = results[page].USABLE_WIDTH3;

                                                if (results[page].WIDTH_SILICONE3 != null)
                                                    width_silicone_3Row.WIDTH_SILICONE = results[page].WIDTH_SILICONE3;

                                                if (results[page].NUMTHREADS_W3 != null)
                                                    numthreads_3Row.NUMTHREADS_W = results[page].NUMTHREADS_W3;

                                                if (results[page].NUMTHREADS_F3 != null)
                                                    numthreads_3Row.NUMTHREADS_F = results[page].NUMTHREADS_F3;

                                                if (results[page].THICKNESS3 != null)
                                                    thickness_3Row.THICKNESS = results[page].THICKNESS3;

                                                if (results[page].MAXFORCE_W3 != null)
                                                    maxforce_3Row.MAXFORCE_W = results[page].MAXFORCE_W3;

                                                if (results[page].MAXFORCE_F3 != null)
                                                    maxforce_3Row.MAXFORCE_F = results[page].MAXFORCE_F3;

                                                if (results[page].ELONGATIONFORCE_W3 != null)
                                                    elongationforce_3Row.ELONGATIONFORCE_W = results[page].ELONGATIONFORCE_W3;

                                                if (results[page].ELONGATIONFORCE_F3 != null)
                                                    elongationforce_3Row.ELONGATIONFORCE_F = results[page].ELONGATIONFORCE_F3;

                                                if (results[page].EDGECOMB_W3 != null)
                                                    edgecomb_3Row.EDGECOMB_W = results[page].EDGECOMB_W3;

                                                if (results[page].EDGECOMB_F3 != null)
                                                    edgecomb_3Row.EDGECOMB_F = results[page].EDGECOMB_F3;

                                                if (results[page].STIFFNESS_W3 != null)
                                                    stiffness_3Row.STIFFNESS_W = results[page].STIFFNESS_W3;

                                                if (results[page].STIFFNESS_F3 != null)
                                                    stiffness_3Row.STIFFNESS_F = results[page].STIFFNESS_F3;

                                                if (results[page].TEAR_W3 != null)
                                                    tear_3Row.TEAR_W = results[page].TEAR_W3;

                                                if (results[page].TEAR_F3 != null)
                                                    tear_3Row.TEAR_F = results[page].TEAR_F3;

                                                if (results[page].DYNAMIC_AIR3 != null)
                                                    dynamic_air_3Row.DYNAMIC_AIR = results[page].DYNAMIC_AIR3;

                                                if (results[page].EXPONENT3 != null)
                                                    exponent_3Row.EXPONENT = results[page].EXPONENT3;

                                                if (results[page].DIMENSCHANGE_W3 != null)
                                                    dimenschange_3Row.DIMENSCHANGE_W = results[page].DIMENSCHANGE_W3;

                                                if (results[page].DIMENSCHANGE_F3 != null)
                                                    dimenschange_3Row.DIMENSCHANGE_F = results[page].DIMENSCHANGE_F3;

                                                if (results[page].FLEXABRASION_W3 != null)
                                                    flexabrasion_3Row.FLEXABRASION_W = results[page].FLEXABRASION_W3;

                                                if (results[page].FLEXABRASION_F3 != null)
                                                    flexabrasion_3Row.FLEXABRASION_F = results[page].FLEXABRASION_F3;

                                                // Update 06/07/18
                                                if (results[page].BOW3 != null)
                                                    bow_3Row.BOW = results[page].BOW3;

                                                if (results[page].SKEW3 != null)
                                                    skew_3Row.SKEW = results[page].SKEW3;

                                                if (results[page].BENDING_W3 != null)
                                                    bending_3Row.BENDING_W = results[page].BENDING_W3;

                                                if (results[page].BENDING_F3 != null)
                                                    bending_3Row.BENDING_F = results[page].BENDING_F3;

                                                if (results[page].FLEX_SCOTT_W3 != null)
                                                    flex_scott_3Row.FLEX_SCOTT_W = results[page].FLEX_SCOTT_W3;

                                                if (results[page].FLEX_SCOTT_F3 != null)
                                                    flex_scott_3Row.FLEX_SCOTT_F = results[page].FLEX_SCOTT_F3;

                                                #endregion

                                                #region result_6Row

                                                if (results[page].TOTALWEIGHT3 != null)
                                                    totalweight_6Row.TOTALWEIGHT = results[page].TOTALWEIGHT3;

                                                if (results[page].UNCOATEDWEIGHT3 != null)
                                                    uncoatedweight_6Row.UNCOATEDWEIGHT = results[page].UNCOATEDWEIGHT3;

                                                if (results[page].COATINGWEIGHT3 != null)
                                                    coatingweight_6Row.COATINGWEIGHT = results[page].COATINGWEIGHT3;

                                                if (results[page].STATIC_AIR3 != null)
                                                    static_air_6Row.STATIC_AIR = results[page].STATIC_AIR3;

                                                #endregion
                                            }

                                            #region 3 Row

                                            if (usable_width_3Row.USABLE_WIDTH != null)
                                            {
                                                usable_widths_3Row.Add(usable_width_3Row);
                                                Row_usable_widt++;
                                            }

                                            if (width_silicone_3Row.WIDTH_SILICONE != null)
                                            {
                                                width_silicones_3Row.Add(width_silicone_3Row);
                                                Row_width_silicone++;
                                            }

                                            if (numthreads_3Row.NUMTHREADS_W != null || numthreads_3Row.NUMTHREADS_F != null)
                                            {
                                                numthreadss_3Row.Add(numthreads_3Row);
                                                Row_numthreads++;
                                            }

                                            if (thickness_3Row.THICKNESS != null)
                                            {
                                                thicknesss_3Row.Add(thickness_3Row);
                                                Row_thickness++;
                                            }

                                            if (maxforce_3Row.MAXFORCE_W != null || maxforce_3Row.MAXFORCE_F != null)
                                            {
                                                maxforces_3Row.Add(maxforce_3Row);
                                                Row_maxforce++;
                                            }

                                            if (elongationforce_3Row.ELONGATIONFORCE_W != null || elongationforce_3Row.ELONGATIONFORCE_F != null)
                                            {
                                                elongationforces_3Row.Add(elongationforce_3Row);
                                                Row_elongationforce++;
                                            }

                                            if (edgecomb_3Row.EDGECOMB_W != null || edgecomb_3Row.EDGECOMB_F != null)
                                            {
                                                edgecombs_3Row.Add(edgecomb_3Row);
                                                Row_edgecomb++;
                                            }

                                            if (stiffness_3Row.STIFFNESS_W != null || stiffness_3Row.STIFFNESS_F != null)
                                            {
                                                stiffnesss_3Row.Add(stiffness_3Row);
                                                Row_stiffness++;
                                            }

                                            if (tear_3Row.TEAR_W != null || tear_3Row.TEAR_F != null)
                                            {
                                                tears_3Row.Add(tear_3Row);
                                                Row_tear++;
                                            }

                                            if (dynamic_air_3Row.DYNAMIC_AIR != null)
                                            {
                                                dynamic_airs_3Row.Add(dynamic_air_3Row);
                                                Row_dynamic_air++;
                                            }

                                            if (exponent_3Row.EXPONENT != null)
                                            {
                                                exponents_3Row.Add(exponent_3Row);
                                                Row_exponent++;
                                            }

                                            if (dimenschange_3Row.DIMENSCHANGE_W != null || dimenschange_3Row.DIMENSCHANGE_F != null)
                                            {
                                                dimenschanges_3Row.Add(dimenschange_3Row);
                                                Row_dimenschange++;
                                            }

                                            if (flexabrasion_3Row.FLEXABRASION_W != null || flexabrasion_3Row.FLEXABRASION_F != null)
                                            {
                                                flexabrasions_3Row.Add(flexabrasion_3Row);
                                                Row_flexabrasion++;
                                            }

                                            if (bow_3Row.BOW != null)
                                            {
                                                bows_3Row.Add(bow_3Row);
                                                Row_bow++;
                                            }

                                            if (skew_3Row.SKEW != null)
                                            {
                                                skews_3Row.Add(skew_3Row);
                                                Row_skew++;
                                            }

                                            if (bending_3Row.BENDING_W != null || bending_3Row.BENDING_F != null)
                                            {
                                                bendings_3Row.Add(bending_3Row);
                                                Row_bending++;
                                            }

                                            if (flex_scott_3Row.FLEX_SCOTT_W != null || flex_scott_3Row.FLEX_SCOTT_F != null)
                                            {
                                                flex_scotts_3Row.Add(flex_scott_3Row);
                                                Row_flex_scott++;
                                            }

                                            #endregion
                                        }

                                        if (page_Row == 3)
                                        {
                                            #region result_6Row

                                            if (results[page].TOTALWEIGHT4 != null)
                                                totalweight_6Row.TOTALWEIGHT = results[page].TOTALWEIGHT4;

                                            if (results[page].UNCOATEDWEIGHT4 != null)
                                                uncoatedweight_6Row.UNCOATEDWEIGHT = results[page].UNCOATEDWEIGHT4;

                                            if (results[page].COATINGWEIGHT4 != null)
                                                coatingweight_6Row.COATINGWEIGHT = results[page].COATINGWEIGHT4;

                                            if (results[page].STATIC_AIR4 != null)
                                                static_air_6Row.STATIC_AIR = results[page].STATIC_AIR4;

                                            #endregion
                                        }
                                        else if (page_Row == 4)
                                        {
                                            #region result_6Row

                                            if (results[page].TOTALWEIGHT5 != null)
                                                totalweight_6Row.TOTALWEIGHT = results[page].TOTALWEIGHT5;

                                            if (results[page].UNCOATEDWEIGHT5 != null)
                                                uncoatedweight_6Row.UNCOATEDWEIGHT = results[page].UNCOATEDWEIGHT5;

                                            if (results[page].COATINGWEIGHT5 != null)
                                                coatingweight_6Row.COATINGWEIGHT = results[page].COATINGWEIGHT5;

                                            if (results[page].STATIC_AIR5 != null)
                                                static_air_6Row.STATIC_AIR = results[page].STATIC_AIR5;

                                            #endregion
                                        }
                                        else if (page_Row == 5)
                                        {
                                            #region result_6Row

                                            if (results[page].TOTALWEIGHT6 != null)
                                                totalweight_6Row.TOTALWEIGHT = results[page].TOTALWEIGHT6;

                                            if (results[page].UNCOATEDWEIGHT6 != null)
                                                uncoatedweight_6Row.UNCOATEDWEIGHT = results[page].UNCOATEDWEIGHT6;

                                            if (results[page].COATINGWEIGHT6 != null)
                                                coatingweight_6Row.COATINGWEIGHT = results[page].COATINGWEIGHT6;

                                            if (results[page].STATIC_AIR6 != null)
                                                static_air_6Row.STATIC_AIR = results[page].STATIC_AIR6;

                                            #endregion
                                        }

                                        if (totalweight_6Row.TOTALWEIGHT != null)
                                        {
                                            totalweights_6Row.Add(totalweight_6Row);
                                            Row_totalweight++;
                                        }

                                        if (uncoatedweight_6Row.UNCOATEDWEIGHT != null)
                                        {
                                            uncoatedweights_6Row.Add(uncoatedweight_6Row);
                                            Row_uncoatedweight++;
                                        }

                                        if (coatingweight_6Row.COATINGWEIGHT != null)
                                        {
                                            coatingweights_6Row.Add(coatingweight_6Row);
                                            Row_coatingweight++;
                                        }

                                        if (static_air_6Row.STATIC_AIR != null)
                                        {
                                            static_airs_6Row.Add(static_air_6Row);
                                            Row_static_air++;
                                        }
                                    }

                                }

                                #region 1 Row

                                if (result_1Row.ITM_CODE != null || result_1Row.WEAVINGLOT != null || result_1Row.FINISHINGLOT != null)
                                {
                                    results_1Row.Add(result_1Row);
                                    Row1++;
                                }

                                if (width_1Row.WIDTH != null)
                                {
                                    widths_1Row.Add(width_1Row);
                                    Row_width++;
                                }

                                if (flammability_1Row.FLAMMABILITY_F != null && flammability_1Row.FLAMMABILITY_W != null)
                                {
                                    flammabilitys_1Row.Add(flammability_1Row);
                                    Row_flammability++;
                                }

                                if (remark_1Row.REMARK != null)
                                {
                                    remarks_1Row.Add(remark_1Row);
                                    Row_remark++;  
                                }

                                if (approve_1Row.APPROVEBY != null && approve_1Row.APPROVEDATE != null)
                                {
                                    approves_1Row.Add(approve_1Row);
                                    Row_approve++;
                                }

                                #endregion
                            }
                        }

                        int?[] numbers = { Row1, Row_width, Row_flammability, Row_remark, Row_approve 
                                             , Row_usable_widt,Row_width_silicone,Row_numthreads,Row_thickness, Row_maxforce, Row_elongationforce
                                             , Row_edgecomb,Row_stiffness, Row_tear,Row_static_air,Row_dynamic_air,Row_exponent, Row_dimenschange
                                             , Row_flexabrasion,Row_bow, Row_skew, Row_bending, Row_flex_scott 
                                             , Row_totalweight , Row_uncoatedweight , Row_coatingweight};
                        int? biggestNumber = numbers.Max();

                        for (int page = 0; page < biggestNumber; page++)
                        {
                            LAB_SEARCHLABENTRYPRODUCTION_QDAS result_All = new LAB_SEARCHLABENTRYPRODUCTION_QDAS();

                            if (page < Row1)
                            {
                                #region results_1Row

                                //result_All.ITM_CODE = results_1Row[page].ITM_CODE;
                                result_All.WEAVINGLOT = results_1Row[page].WEAVINGLOT;
                                result_All.LOOMNO = results_1Row[page].LOOMNO;
                                //result_All.ENTRYDATE = results_1Row[page].ENTRYDATE;
                                //result_All.ENTEYBY = results_1Row[page].ENTEYBY;
                                //result_All.ITEMLOT = results_1Row[page].ITEMLOT;
                                //result_All.STATUS = results_1Row[page].STATUS;
                                //result_All.CREATEDATE = results_1Row[page].CREATEDATE;
                                //result_All.FINISHLENGTH = results_1Row[page].FINISHLENGTH;
                                //result_All.FINISHINGPROCESS = results_1Row[page].FINISHINGPROCESS;
                                //result_All.LOOMNO = results_1Row[page].LOOMNO;
                                //result_All.FINISHINGMC = results_1Row[page].FINISHINGMC;

                                //// Update 06/07/18
                                //result_All.ITEMLOT = results_1Row[page].ITEMLOT;

                                #endregion
                            }

                            // Update 07/07/18
                            if (page < Row_width)
                            {
                                result_All.WIDTH = widths_1Row[page].WIDTH;
                            }

                            if (page < Row_flammability)
                            {
                                result_All.FLAMMABILITY_W = flammabilitys_1Row[page].FLAMMABILITY_W;
                                result_All.FLAMMABILITY_F = flammabilitys_1Row[page].FLAMMABILITY_F;
                            }

                            if (page < Row_remark)
                            {
                                //result_All.REMARK = remarks_1Row[page].REMARK;
                            }

                            if (page < Row_approve)
                            {
                                //result_All.APPROVEBY = approves_1Row[page].APPROVEBY;
                                //result_All.APPROVEDATE = approves_1Row[page].APPROVEDATE;
                            }

                            if (page < Row_usable_widt)
                            {
                                #region result_3Row

                                if (usable_widths_3Row[page].USABLE_WIDTH != null)
                                    result_All.USABLE_WIDTH = usable_widths_3Row[page].USABLE_WIDTH;

                                #endregion
                            }

                            if (page < Row_width_silicone)
                            {
                                #region result_3Row

                                if (width_silicones_3Row[page].WIDTH_SILICONE != null)
                                    result_All.WIDTH_SILICONE = width_silicones_3Row[page].WIDTH_SILICONE;

                                #endregion
                            }

                            if (page < Row_numthreads)
                            {
                                #region result_3Row

                                if (numthreadss_3Row[page].NUMTHREADS_W != null)
                                    result_All.NUMTHREADS_W = numthreadss_3Row[page].NUMTHREADS_W;

                                if (numthreadss_3Row[page].NUMTHREADS_F != null)
                                    result_All.NUMTHREADS_F = numthreadss_3Row[page].NUMTHREADS_F;

                                #endregion
                            }

                            if (page < Row_thickness)
                            {
                                #region result_3Row

                                if (thicknesss_3Row[page].THICKNESS != null)
                                    result_All.THICKNESS = thicknesss_3Row[page].THICKNESS;

                                #endregion
                            }

                            if (page < Row_maxforce)
                            {
                                #region result_3Row

                                if (maxforces_3Row[page].MAXFORCE_W != null)
                                    result_All.MAXFORCE_W = maxforces_3Row[page].MAXFORCE_W;

                                if (maxforces_3Row[page].MAXFORCE_F != null)
                                    result_All.MAXFORCE_F = maxforces_3Row[page].MAXFORCE_F;

                                #endregion
                            }

                            if (page < Row_elongationforce)
                            {
                                #region result_3Row

                                if (elongationforces_3Row[page].ELONGATIONFORCE_W != null)
                                    result_All.ELONGATIONFORCE_W = elongationforces_3Row[page].ELONGATIONFORCE_W;

                                if (elongationforces_3Row[page].ELONGATIONFORCE_F != null)
                                    result_All.ELONGATIONFORCE_F = elongationforces_3Row[page].ELONGATIONFORCE_F;

                                #endregion
                            }

                            if (page < Row_edgecomb)
                            {
                                #region result_3Row

                                if (edgecombs_3Row[page].EDGECOMB_W != null)
                                    result_All.EDGECOMB_W = edgecombs_3Row[page].EDGECOMB_W;

                                if (edgecombs_3Row[page].EDGECOMB_F != null)
                                    result_All.EDGECOMB_F = edgecombs_3Row[page].EDGECOMB_F;

                                #endregion
                            }

                            if (page < Row_stiffness)
                            {
                                #region result_3Row

                                if (stiffnesss_3Row[page].STIFFNESS_W != null)
                                    result_All.STIFFNESS_W = stiffnesss_3Row[page].STIFFNESS_W;

                                if (stiffnesss_3Row[page].STIFFNESS_F != null)
                                    result_All.STIFFNESS_F = stiffnesss_3Row[page].STIFFNESS_F;

                                #endregion
                            }

                            if (page < Row_tear)
                            {
                                #region result_3Row

                                if (tears_3Row[page].TEAR_W != null)
                                    result_All.TEAR_W = tears_3Row[page].TEAR_W;

                                if (tears_3Row[page].TEAR_F != null)
                                    result_All.TEAR_F = tears_3Row[page].TEAR_F;

                                #endregion
                            }

                            if (page < Row_dynamic_air)
                            {
                                #region result_3Row

                                if (dynamic_airs_3Row[page].DYNAMIC_AIR != null)
                                    result_All.DYNAMIC_AIR = dynamic_airs_3Row[page].DYNAMIC_AIR;

                                #endregion
                            }

                            if (page < Row_exponent)
                            {
                                #region result_3Row

                                if (exponents_3Row[page].EXPONENT != null)
                                    result_All.EXPONENT = exponents_3Row[page].EXPONENT;

                                #endregion
                            }

                            if (page < Row_dimenschange)
                            {
                                #region result_3Row

                                if (dimenschanges_3Row[page].DIMENSCHANGE_W != null)
                                    result_All.DIMENSCHANGE_W = dimenschanges_3Row[page].DIMENSCHANGE_W;

                                if (dimenschanges_3Row[page].DIMENSCHANGE_F != null)
                                    result_All.DIMENSCHANGE_F = dimenschanges_3Row[page].DIMENSCHANGE_F;

                                #endregion
                            }

                            if (page < Row_flexabrasion)
                            {
                                #region result_3Row

                                if (flexabrasions_3Row[page].FLEXABRASION_W != null)
                                    result_All.FLEXABRASION_W = flexabrasions_3Row[page].FLEXABRASION_W;

                                if (flexabrasions_3Row[page].FLEXABRASION_F != null)
                                    result_All.FLEXABRASION_F = flexabrasions_3Row[page].FLEXABRASION_F;

                                #endregion
                            }

                            if (page < Row_bow)
                            {
                                #region result_3Row

                                if (bows_3Row[page].BOW != null)
                                    result_All.BOW = bows_3Row[page].BOW;

                                #endregion
                            }

                            if (page < Row_skew)
                            {
                                #region result_3Row

                                if (skews_3Row[page].SKEW != null)
                                    result_All.SKEW = skews_3Row[page].SKEW;

                                #endregion
                            }

                            if (page < Row_bending)
                            {
                                #region result_3Row

                                if (bendings_3Row[page].BENDING_W != null)
                                    result_All.BENDING_W = bendings_3Row[page].BENDING_W;

                                if (bendings_3Row[page].BENDING_F != null)
                                    result_All.BENDING_F = bendings_3Row[page].BENDING_F;

                                #endregion
                            }

                            if (page < Row_flex_scott)
                            {
                                #region result_3Row

                                if (flex_scotts_3Row[page].FLEX_SCOTT_W != null)
                                    result_All.FLEX_SCOTT_W = flex_scotts_3Row[page].FLEX_SCOTT_W;

                                if (flex_scotts_3Row[page].FLEX_SCOTT_F != null)
                                    result_All.FLEX_SCOTT_F = flex_scotts_3Row[page].FLEX_SCOTT_F;

                                #endregion
                            }
                            if (page < Row_totalweight)
                            {
                                #region result_6Row

                                if (totalweights_6Row[page].TOTALWEIGHT != null)
                                    result_All.TOTALWEIGHT = totalweights_6Row[page].TOTALWEIGHT;

                                #endregion
                            }

                            if (page < Row_uncoatedweight)
                            {
                                #region result_6Row

                                if (uncoatedweights_6Row[page].UNCOATEDWEIGHT != null)
                                    result_All.UNCOATEDWEIGHT = uncoatedweights_6Row[page].UNCOATEDWEIGHT;

                                #endregion
                            }

                            if (page < Row_coatingweight)
                            {
                                #region result_6Row

                                if (coatingweights_6Row[page].COATINGWEIGHT != null)
                                    result_All.COATINGWEIGHT = coatingweights_6Row[page].COATINGWEIGHT;

                                #endregion
                            }

                            if (page < Row_static_air)
                            {
                                #region result_6Row

                                if (static_airs_6Row[page].STATIC_AIR != null)
                                    result_All.STATIC_AIR = static_airs_6Row[page].STATIC_AIR;

                                #endregion
                            }

                            if (result_All.USABLE_WIDTH != null || result_All.WIDTH_SILICONE != null || result_All.NUMTHREADS_W != null || result_All.NUMTHREADS_F != null
                                              || result_All.THICKNESS != null || result_All.MAXFORCE_W != null || result_All.MAXFORCE_F != null || result_All.ELONGATIONFORCE_W != null
                                              || result_All.ELONGATIONFORCE_F != null || result_All.EDGECOMB_W != null || result_All.EDGECOMB_F != null || result_All.STIFFNESS_W != null
                                              || result_All.STIFFNESS_F != null || result_All.TEAR_W != null || result_All.TEAR_F != null || result_All.STATIC_AIR != null || result_All.DYNAMIC_AIR != null
                                              || result_All.EXPONENT != null || result_All.DIMENSCHANGE_W != null || result_All.DIMENSCHANGE_F != null || result_All.FLEXABRASION_W != null || result_All.FLEXABRASION_F != null
                                              || result_All.TOTALWEIGHT != null || result_All.UNCOATEDWEIGHT != null || result_All.COATINGWEIGHT != null
                                              || result_All.BOW != null || result_All.SKEW != null || result_All.BENDING_W != null || result_All.BENDING_F != null || result_All.FLEX_SCOTT_W != null || result_All.FLEX_SCOTT_F != null)
                            {
                                results_All.Add(result_All);
                            }
                        }
                    }
                }

                return results_All;
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                return null;
            }
        }
        #endregion

        #region ExportToExcelforQAll
        public System.Data.DataTable ExportToExcelforQDAS()
        {
            try
            {
                #region Def

                System.Data.DataTable table = new System.Data.DataTable();
                List<LAB_SEARCHLABENTRYPRODUCTION_QDAS> results = LoadDataQDAS();


                //table.Columns.Add("ITM_CODE", typeof(string));
                table.Columns.Add("WEAVINGLOT", typeof(string));
                table.Columns.Add("LOOMNO", typeof(string));
                //table.Columns.Add("FINISHINGLOT", typeof(string));
                //table.Columns.Add("ENTRYDATE", typeof(DateTime));
                //table.Columns.Add("ENTEYBY", typeof(string));
                table.Columns.Add("WIDTH", typeof(decimal));
                table.Columns.Add("USABLE_WIDTH", typeof(decimal));
                table.Columns.Add("WIDTH_SILICONE", typeof(decimal));
                table.Columns.Add("NUMTHREADS_W", typeof(decimal));
                table.Columns.Add("NUMTHREADS_F", typeof(decimal));
                table.Columns.Add("TOTALWEIGHT", typeof(decimal));
                table.Columns.Add("UNCOATEDWEIGHT", typeof(decimal));
                table.Columns.Add("COATINGWEIGHT", typeof(decimal));
                table.Columns.Add("THICKNESS", typeof(decimal));
                table.Columns.Add("MAXFORCE_W", typeof(decimal));
                table.Columns.Add("MAXFORCE_F", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_W", typeof(decimal));
                table.Columns.Add("ELONGATIONFORCE_F", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_W", typeof(decimal));
                table.Columns.Add("FLAMMABILITY_F", typeof(decimal));
                table.Columns.Add("EDGECOMB_W", typeof(decimal));
                table.Columns.Add("EDGECOMB_F", typeof(decimal));
                table.Columns.Add("STIFFNESS_W", typeof(decimal));
                table.Columns.Add("STIFFNESS_F", typeof(decimal));
                table.Columns.Add("TEAR_W", typeof(decimal));
                table.Columns.Add("TEAR_F", typeof(decimal));
                table.Columns.Add("STATIC_AIR", typeof(decimal));
                table.Columns.Add("DYNAMIC_AIR", typeof(decimal));
                table.Columns.Add("EXPONENT", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_W", typeof(decimal));
                table.Columns.Add("DIMENSCHANGE_F", typeof(decimal));
                table.Columns.Add("FLEXABRASION_W", typeof(decimal));
                table.Columns.Add("FLEXABRASION_F", typeof(decimal));
                table.Columns.Add("BOW", typeof(decimal));
                table.Columns.Add("SKEW", typeof(decimal));
                //table.Columns.Add("STATUS", typeof(string));
                //table.Columns.Add("REMARK", typeof(string));
                //table.Columns.Add("APPROVEBY", typeof(string));
                //table.Columns.Add("APPROVEDATE", typeof(DateTime));
                //table.Columns.Add("CREATEDATE", typeof(DateTime));
                //table.Columns.Add("FINISHLENGTH", typeof(decimal));
                //table.Columns.Add("FINISHINGPROCESS", typeof(string));
               
                //table.Columns.Add("FINISHINGMC", typeof(string));

                //Update 06/07/18
                table.Columns.Add("BENDING_W", typeof(decimal));
                table.Columns.Add("BENDING_F", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_W", typeof(decimal));
                table.Columns.Add("FLEX_SCOTT_F", typeof(decimal));
                //table.Columns.Add("ITEMLOT", typeof(string));


                //string ITM_CODE = string.Empty;
                string WEAVINGLOT = string.Empty;
                string LOOMNO = string.Empty;
                //string FINISHINGLOT = string.Empty;
                //DateTime? ENTRYDATE = null;
                //string ENTEYBY = string.Empty;
                decimal? WIDTH = null;
                decimal? USABLE_WIDTH = null;
                decimal? WIDTH_SILICONE = null;
                decimal? NUMTHREADS_W = null;
                decimal? NUMTHREADS_F = null;
                decimal? TOTALWEIGHT = null;
                decimal? UNCOATEDWEIGHT = null;
                decimal? COATINGWEIGHT = null;
                decimal? THICKNESS = null;
                decimal? MAXFORCE_W = null;
                decimal? MAXFORCE_F = null;
                decimal? ELONGATIONFORCE_W = null;
                decimal? ELONGATIONFORCE_F = null;
                decimal? FLAMMABILITY_W = null;
                decimal? FLAMMABILITY_F = null;
                decimal? EDGECOMB_W = null;
                decimal? EDGECOMB_F = null;
                decimal? STIFFNESS_W = null;
                decimal? STIFFNESS_F = null;
                decimal? TEAR_W = null;
                decimal? TEAR_F = null;
                decimal? STATIC_AIR = null;
                decimal? DYNAMIC_AIR = null;
                decimal? EXPONENT = null;
                decimal? DIMENSCHANGE_W = null;
                decimal? DIMENSCHANGE_F = null;
                decimal? FLEXABRASION_W = null;
                decimal? FLEXABRASION_F = null;
                decimal? BOW = null;
                decimal? SKEW = null;
                //string STATUS = string.Empty;
                //string REMARK = string.Empty;
                //string APPROVEBY = string.Empty;
                //DateTime? APPROVEDATE = null;
                //DateTime? CREATEDATE = null;
                //decimal? FINISHLENGTH = null;
                //string FINISHINGPROCESS = string.Empty;
                
                //string FINISHINGMC = string.Empty;

                //Update 06/07/18
                decimal? BENDING_W = null;
                decimal? BENDING_F = null;
                decimal? FLEX_SCOTT_W = null;
                decimal? FLEX_SCOTT_F = null;
                //string ITEMLOT = string.Empty;

                #endregion

                #region Insert Data

                if (results != null)
                {
                    for (int i = 0; i <= results.Count - 1; i++)
                    {

                        //ITM_CODE = results[i].ITM_CODE;
                        WEAVINGLOT = results[i].WEAVINGLOT;
                        LOOMNO = results[i].LOOMNO;
                        //FINISHINGLOT = results[i].FINISHINGLOT;
                        //ENTRYDATE = results[i].ENTRYDATE;
                        //ENTEYBY = results[i].ENTEYBY;
                        WIDTH = results[i].WIDTH;
                        USABLE_WIDTH = results[i].USABLE_WIDTH;
                        WIDTH_SILICONE = results[i].WIDTH_SILICONE;
                        NUMTHREADS_W = results[i].NUMTHREADS_W;
                        NUMTHREADS_F = results[i].NUMTHREADS_F;
                        TOTALWEIGHT = results[i].TOTALWEIGHT;
                        UNCOATEDWEIGHT = results[i].UNCOATEDWEIGHT;
                        COATINGWEIGHT = results[i].COATINGWEIGHT;
                        THICKNESS = results[i].THICKNESS;
                        MAXFORCE_W = results[i].MAXFORCE_W;
                        MAXFORCE_F = results[i].MAXFORCE_F;
                        ELONGATIONFORCE_W = results[i].ELONGATIONFORCE_W;
                        ELONGATIONFORCE_F = results[i].ELONGATIONFORCE_F;
                        FLAMMABILITY_W = results[i].FLAMMABILITY_W;
                        FLAMMABILITY_F = results[i].FLAMMABILITY_F;
                        EDGECOMB_W = results[i].EDGECOMB_W;
                        EDGECOMB_F = results[i].EDGECOMB_F;
                        STIFFNESS_W = results[i].STIFFNESS_W;
                        STIFFNESS_F = results[i].STIFFNESS_F;
                        TEAR_W = results[i].TEAR_W;
                        TEAR_F = results[i].TEAR_F;
                        STATIC_AIR = results[i].STATIC_AIR;
                        DYNAMIC_AIR = results[i].DYNAMIC_AIR;
                        EXPONENT = results[i].EXPONENT;
                        DIMENSCHANGE_W = results[i].DIMENSCHANGE_W;
                        DIMENSCHANGE_F = results[i].DIMENSCHANGE_F;
                        FLEXABRASION_W = results[i].FLEXABRASION_W;
                        FLEXABRASION_F = results[i].FLEXABRASION_F;
                        BOW = results[i].BOW;
                        SKEW = results[i].SKEW;
                        //STATUS = results[i].STATUS;
                        //REMARK = results[i].REMARK;
                        //APPROVEBY = results[i].APPROVEBY;
                        //APPROVEDATE = results[i].APPROVEDATE;
                        //CREATEDATE = results[i].CREATEDATE;
                        //FINISHLENGTH = results[i].FINISHLENGTH;
                        //FINISHINGPROCESS = results[i].FINISHINGPROCESS;
                        
                        //FINISHINGMC = results[i].FINISHINGMC;

                        //Update 06/07/18
                        BENDING_W = results[i].BENDING_W;
                        BENDING_F = results[i].BENDING_F;
                        FLEX_SCOTT_W = results[i].FLEX_SCOTT_W;
                        FLEX_SCOTT_F = results[i].FLEX_SCOTT_F;
                        //ITEMLOT = results[i].ITEMLOT;

                        //table.Rows.Add(ITM_CODE, WEAVINGLOT, FINISHINGLOT, ENTRYDATE, ENTEYBY, WIDTH,
                        //    USABLE_WIDTH, WIDTH_SILICONE,
                        //    NUMTHREADS_W, NUMTHREADS_F,
                        //    TOTALWEIGHT,
                        //    UNCOATEDWEIGHT,
                        //    COATINGWEIGHT,
                        //    THICKNESS,
                        //    MAXFORCE_W, MAXFORCE_F,
                        //    ELONGATIONFORCE_W, ELONGATIONFORCE_F,
                        //    FLAMMABILITY_W, FLAMMABILITY_F, EDGECOMB_W, EDGECOMB_F,
                        //    STIFFNESS_W, STIFFNESS_F,
                        //    TEAR_W, TEAR_F,
                        //    STATIC_AIR, DYNAMIC_AIR,
                        //    EXPONENT,
                        //    DIMENSCHANGE_W, DIMENSCHANGE_F,
                        //    FLEXABRASION_W, FLEXABRASION_F,
                        //    BOW, SKEW, STATUS, REMARK, APPROVEBY, APPROVEDATE, CREATEDATE, FINISHLENGTH, FINISHINGPROCESS, LOOMNO,
                        //    FINISHINGMC, BENDING_W, BENDING_F, FLEX_SCOTT_W, FLEX_SCOTT_F, ITEMLOT);


                        table.Rows.Add(WEAVINGLOT, LOOMNO, WIDTH,
                            USABLE_WIDTH, WIDTH_SILICONE,
                            NUMTHREADS_W, NUMTHREADS_F,
                            TOTALWEIGHT,
                            UNCOATEDWEIGHT,
                            COATINGWEIGHT,
                            THICKNESS,
                            MAXFORCE_W, MAXFORCE_F,
                            ELONGATIONFORCE_W, ELONGATIONFORCE_F,
                            FLAMMABILITY_W, FLAMMABILITY_F, EDGECOMB_W, EDGECOMB_F,
                            STIFFNESS_W, STIFFNESS_F,
                            TEAR_W, TEAR_F,
                            STATIC_AIR, DYNAMIC_AIR,
                            EXPONENT,
                            DIMENSCHANGE_W, DIMENSCHANGE_F,
                            FLEXABRASION_W, FLEXABRASION_F,
                            BOW, SKEW, BENDING_W, BENDING_F, FLEX_SCOTT_W, FLEX_SCOTT_F);

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

        #region CSV

        #region ExportCSVQDAS
        private void ExportCSVQDAS()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string csv = string.Empty;
                csv = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";

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

                        if (CreateCSVQDAS(newFileName) == true)
                        {
                            MessageBox.Show("CSV : " + newFileName, "Save Complete", MessageBoxButton.OK);
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

        #region CreateCSVQDAS
        private bool CreateCSVQDAS(string newFileName)
        {
            try
            {
                System.Data.DataTable dt = ExportToExcelforQDAS();

                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                      string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(newFileName, sb.ToString());

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ex.Message.Err();
                return false;
            }
        }
        #endregion

        #region ExportCSV
        private void ExportCSV()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog1.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                string csv = string.Empty;
                csv = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";

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

                        if (CreateCSV(newFileName) == true)
                        {
                            MessageBox.Show("CSV : " + newFileName, "Save Complete", MessageBoxButton.OK);
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

        #region CreateCSV
        private bool CreateCSV(string newFileName)
        {
            try
            {
                System.Data.DataTable dt = ExportToExcel();

                StringBuilder sb = new StringBuilder();

                IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                                  Select(column => column.ColumnName);
                sb.AppendLine(string.Join(",", columnNames));

                foreach (DataRow row in dt.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                      string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                    sb.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(newFileName, sb.ToString());

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ex.Message.Err();
                return false;
            }
        }
        #endregion

        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdSearch.IsEnabled = enabled;
            cmdClear.IsEnabled = enabled;
            cmdViewData.IsEnabled = enabled;
            cmdExportExcelforQDAS.IsEnabled = enabled;
            cmdExportData.IsEnabled = enabled;
        }
        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS)
        {
            try
            {
                ConmonReportService.Instance.ITM_Code = P_ITMCODE;
                ConmonReportService.Instance.ENTRYSTARTDATE = P_ENTRYSTARTDATE;
                ConmonReportService.Instance.ENTRYENDDATE = P_ENTRYENDDATE;
                ConmonReportService.Instance.LOOM = P_LOOM;
                ConmonReportService.Instance.FINISHPROCESS = P_FINISHPROCESS;

                ConmonReportService.Instance.WEAVINGLOT = weavingLot;
                ConmonReportService.Instance.FinishingLot = finishingLot;
                ConmonReportService.Instance.ENTRYDATE = entryDate;

                ConmonReportService.Instance.ReportName = " LabTestingResult";

                if (!string.IsNullOrEmpty(ConmonReportService.Instance.ReportName))
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

        private void Preview(string P_ITMCODE, string P_ENTRYSTARTDATE, string P_ENTRYENDDATE, string P_LOOM, string P_FINISHPROCESS)
        {
            try
            {
                ConmonReportService.Instance.ITM_Code = P_ITMCODE;
                ConmonReportService.Instance.ENTRYSTARTDATE = P_ENTRYSTARTDATE;
                ConmonReportService.Instance.ENTRYENDDATE = P_ENTRYENDDATE;
                ConmonReportService.Instance.LOOM = P_LOOM;
                ConmonReportService.Instance.FINISHPROCESS = P_FINISHPROCESS;

                ConmonReportService.Instance.WEAVINGLOT = weavingLot;
                ConmonReportService.Instance.FinishingLot = finishingLot;
                ConmonReportService.Instance.ENTRYDATE = entryDate;

                ConmonReportService.Instance.ReportName = "Production";

                if (!string.IsNullOrEmpty(ConmonReportService.Instance.ReportName))
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

