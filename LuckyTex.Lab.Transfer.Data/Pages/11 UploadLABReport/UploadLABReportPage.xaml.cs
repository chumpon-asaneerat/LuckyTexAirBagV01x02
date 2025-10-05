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
    /// Interaction logic for UploadLABReportPage.xaml
    /// </summary>
    public partial class UploadLABReportPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UploadLABReportPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemCode();
            LoadLoom();
            LoadFinishingProcess();

            CreateDirectoryBackup();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string positionLevel = string.Empty;

        string itemCode = string.Empty;
        string weavingLot = string.Empty;
        string finishingLot = string.Empty;
        DateTime? entryDate = null;
        string fileName = string.Empty;
        DateTime? uploadDate = null;

        string backupFolder = string.Empty;

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

        #region cmdUpload_Click
        private void cmdUpload_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (!string.IsNullOrEmpty(itemCode) && !string.IsNullOrEmpty(weavingLot) && !string.IsNullOrEmpty(finishingLot) && entryDate != null)
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    UploadReportPDF();
                }
                else
                {
                    string tmp = "มี File Report ถูก Upload ไปแล้ว" + "\r\n" + "ต้องการ Upload ใหม่หรือไม่";

                    if (tmp.ShowMessageOKCancel() == true)
                    {
                        UploadReportPDF();
                    }
                }
            }
            else
            {
                "Please select Data".ShowMessageBox();
            }

            buttonEnabled(true);
        }
        #endregion

        #region cmdDownload_Click
        private void cmdDownload_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                if (CheckFile(fileName) == true)
                {
                    if (DownloadFile(fileName) == true)
                    {
                        "Save file complete".ShowMessageBox();
                    }
                }
                else
                {
                    "File Not Found or Recheck Config Path".ShowMessageBox();
                }
            }
            else
            {
                "Please select Data".ShowMessageBox();
            }
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
                            if (((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE != null
                               && ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).WEAVINGLOT != null
                               && ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).FINISHINGLOT != null
                               && ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).ENTRYDATE != null)
                            {
                                itemCode = ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).ITM_CODE;
                                weavingLot = ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).WEAVINGLOT;
                                finishingLot = ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).FINISHINGLOT;
                                entryDate = ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).ENTRYDATE;

                                cmdUpload.IsEnabled = true;
                            }
                            else
                            {
                                itemCode = string.Empty;
                                weavingLot = string.Empty;
                                finishingLot = string.Empty;
                                entryDate = null;
                                cmdUpload.IsEnabled = false;
                            }

                            if (((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).FILENAME != null
                                && ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).UPLOADDATE != null)
                            {
                                fileName = ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).FILENAME;
                                uploadDate = ((LuckyTex.Models.LAB_SEARCHAPPROVELAB)(gridLabEntryProduction.CurrentCell.Item)).UPLOADDATE;

                                if (!string.IsNullOrEmpty(fileName))
                                    cmdDownload.IsEnabled = true;
                                else
                                    cmdDownload.IsEnabled = false;
                            }
                            else
                            {
                                fileName = string.Empty;
                                uploadDate = null;
                                cmdDownload.IsEnabled = false;
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

            fileName = string.Empty;
            uploadDate = null;

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

            cmdUpload.IsEnabled = false;
            cmdDownload.IsEnabled = false;
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

            itemCode = string.Empty;
            weavingLot = string.Empty;
            finishingLot = string.Empty;
            entryDate = null;

            fileName = string.Empty;
            uploadDate = null;

            cmdUpload.IsEnabled = false;
            cmdDownload.IsEnabled = false;
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

                List<LAB_SEARCHAPPROVELAB> results = LabDataPDFDataService.Instance.LAB_SEARCHAPPROVELAB(P_ITMCODE, P_ENTRYSTARTDATE, P_ENTRYENDDATE, P_LOOM, P_FINISHPROCESS);

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

        #region Save
        private bool Save(string fileName)
        {
            try
            {
                bool chkSave = true;
                string P_FILENAME = string.Empty;
                DateTime? P_UPLOADDATE = DateTime.Now;

                if (!string.IsNullOrEmpty(fileName))
                    P_FILENAME = fileName;

                string insert = LabDataPDFDataService.Instance.LAB_UPLOADREPORT(itemCode, weavingLot, finishingLot, entryDate, P_FILENAME, P_UPLOADDATE, opera);

                if (insert == "1")
                {
                    "Save Data Complete".ShowMessageBox();
                    chkSave = true;
                }
                else
                {
                    insert.ShowMessageBox();
                    chkSave = false;
                }

                return chkSave;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region CreateDirectoryBackup
        private void CreateDirectoryBackup()
        {
            try
            {
                ConfigManager.Instance.LoadUploadReportPDFConfigs();
                backupFolder = ConfigManager.Instance.UploadReportConfig_UploadReport;

                if (backupFolder != string.Empty)
                {
                    string drive = backupFolder.Substring(0, 1).ToUpper();
                    System.IO.DriveInfo di = new System.IO.DriveInfo(drive);

                    if (di.IsReady == false)
                    {
                        bool folderExists = Directory.Exists(backupFolder);
                        if (!folderExists)
                        {
                            string msg = "Can't find drive " + drive + " Please check drive";
                            msg.ShowMessageBox();
                        }
                    }
                    else
                    {
                        bool folderExists = Directory.Exists(backupFolder);
                        if (!folderExists)
                            Directory.CreateDirectory(backupFolder);
                    }
                }
                else
                {
                    "Can't find file config BackupFilePDFConfig.xml".ShowMessageBox();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }
        #endregion

        #region MoveFilePDF
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectFile"></param>
        /// <returns></returns>
        private bool MoveFilePDF(string selectFile)
        {
            bool chkMove = true;

            try
            {
                if (backupFolder != string.Empty && selectFile != string.Empty)
                {
                    string test = string.Empty;

                    string[] lines = selectFile.Split(new[] { Environment.NewLine },
                                            StringSplitOptions.RemoveEmptyEntries);
                    foreach (string file in lines)
                    {
                        try
                        {
                            test = file.Substring(file.LastIndexOf(@"\") + 1);

                            if (file != string.Empty && test != string.Empty)
                                MoveFile(file, backupFolder + test);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString().ShowMessageBox(false);
                            chkMove = false;
                            break;
                        }
                    }
                }

                return chkMove;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return false;
            }
        }

        #endregion

        #region MoveFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        private void MoveFile(string sourceFile, string destinationFile)
        {
            try
            {
                if (!File.Exists(sourceFile))
                {
                    // This statement ensures that the file is created,
                    // but the handle is not kept.
                    using (FileStream fs = File.Create(sourceFile)) { }
                }

                // Ensure that the target does not exist.
                if (File.Exists(destinationFile))
                {
                    File.Delete(destinationFile);
                }

                // Move the file.
                File.Move(sourceFile, destinationFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        #endregion

        #region UploadReportPDF
        private void UploadReportPDF()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.Filter = "PDF files (*.pdf)|*.PDF";
                //openFileDialog1.InitialDirectory = backupFolder;

                System.Windows.Forms.DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    string fileN = string.Empty;
                    fileN = openFileDialog1.SafeFileName;
                    string tmp = "Confirm การ Upload Lab Report File" + "\r\n" + "สำหรับ Lot " + weavingLot + "\r\n" + "File Name " + fileN;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        if (fileN == fileName)
                        {
                            if (tmp.ShowMessageOKCancel() == true)
                            {
                                if (Save(fileN) == true)
                                {
                                    MoveFilePDF(openFileDialog1.FileName);
                                    SearchData();
                                }
                            }
                        }
                        else
                        {
                            if (CheckFileUp(fileN) == true)
                            {
                                if ("Duplicate data Do you want to Upload".ShowMessageOKCancel() == true)
                                {
                                    if (tmp.ShowMessageOKCancel() == true)
                                    {
                                        if (Save(fileN) == true)
                                        {
                                            MoveFilePDF(openFileDialog1.FileName);
                                            SearchData();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (tmp.ShowMessageOKCancel() == true)
                                {
                                    if (Save(fileN) == true)
                                    {
                                        MoveFilePDF(openFileDialog1.FileName);
                                        SearchData();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (CheckFileUp(fileN) == true)
                        {
                            if ("Duplicate data Do you want to Upload".ShowMessageOKCancel() == true)
                            {
                                if (tmp.ShowMessageOKCancel() == true)
                                {
                                    if (Save(fileN) == true)
                                    {
                                        MoveFilePDF(openFileDialog1.FileName);
                                        SearchData();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (tmp.ShowMessageOKCancel() == true)
                            {
                                if (Save(fileN) == true)
                                {
                                    MoveFilePDF(openFileDialog1.FileName);
                                    SearchData();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.Message.ShowMessageBox();
            }
        }
        #endregion

        private bool CheckFileUp(string fileN)
        {
            bool chkFile = false;
            try
            {

                string[] dirs = Directory.GetFiles(backupFolder, "*.PDF");

                foreach (string dir in dirs)
                {
                    if (fileN == dir.Substring(backupFolder.Length))
                    {
                        chkFile = true;
                        break;
                    }
                }

                return chkFile;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return chkFile;
            }
        }

        #region CheckFile
        private bool CheckFile(string fileName)
        {
            bool chk = false;

            try
            {
                string[] txtList = Directory.GetFiles(backupFolder, fileName);

                if (txtList != null && txtList.Length > 0)
                    chk = true;

                return chk; 
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return chk;
            }
        }
        #endregion

        #region DownloadFile
        private bool DownloadFile(string selectFile)
        {
            bool chkFile = true;

            try
            {
                if (backupFolder != string.Empty && selectFile != string.Empty)
                {
                    string test = string.Empty;

                    string[] lines = selectFile.Split(new[] { Environment.NewLine },
                                            StringSplitOptions.RemoveEmptyEntries);

                    foreach (string file in lines)
                    {
                        try
                        {
                            test = file.Substring(file.LastIndexOf(@"\") + 1);

                            if (file != string.Empty && test != string.Empty)
                            {
                                System.Windows.Forms.SaveFileDialog openFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                                openFileDialog1.Filter = "PDF files (*.pdf)|*.PDF";
                                //openFileDialog1.InitialDirectory = backupFolder;
                                openFileDialog1.FileName = file;

                                System.Windows.Forms.DialogResult dr = openFileDialog1.ShowDialog();
                                if (dr == System.Windows.Forms.DialogResult.OK)
                                {
                                    File.Copy(backupFolder + test, openFileDialog1.FileName, true);
                                }
                                else
                                {
                                    chkFile = false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString().ShowMessageBox(false);
                            chkFile = false;
                            break;
                        }
                    }
                }

                return chkFile;
            }
            catch (Exception e)
            {
                e.Message.ShowMessageBox();
                return false;
            }
        }
        #endregion

        #region CopyFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        private void CopyFile(string sourceFile ,string destinationFile)
        {
            try
            {
                System.Windows.Forms.SaveFileDialog openFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                openFileDialog1.Filter = "PDF files (*.pdf)|*.PDF";
                //openFileDialog1.InitialDirectory = backupFolder;
                openFileDialog1.FileName = sourceFile;

                System.Windows.Forms.DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    File.Copy(destinationFile, openFileDialog1.FileName, true);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdSearch.IsEnabled = enabled;
            cmdClear.IsEnabled = enabled;
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

