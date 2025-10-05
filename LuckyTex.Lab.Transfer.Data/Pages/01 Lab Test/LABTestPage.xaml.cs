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
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for LABTestPage.xaml
    /// </summary>
    public partial class LABTestPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LABTestPage()
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
        string strFileName = string.Empty;
        List<DataControl.ClassData.PDFClassData.ListID_PTF> results = new List<DataControl.ClassData.PDFClassData.ListID_PTF>();
        List<DataControl.ClassData.PDFClassData.ListID_TENS> results_TENS = new List<DataControl.ClassData.PDFClassData.ListID_TENS>();
        bool chkEditPTF = false;

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            string onlyNumeric = @"^([0-9]+(.[0-9]+)?)$";
            Regex regex = new Regex(onlyNumeric);
            e.Handled = !regex.IsMatch(e.Text);
        }

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
            LoadLAB();
            buttonEnabled(true);
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdBackup_Click
        private void cmdBackup_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            #region Backup
            string selectFile = string.Empty;

            //System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Multiselect = true;

            System.Windows.Forms.DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if (selectFile != string.Empty)
                        {
                            selectFile += Environment.NewLine;
                            selectFile += file;
                        }
                        else
                        {
                            selectFile = file;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);

                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(selectFile))
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string backup = string.Empty;

                    if (dialog.SelectedPath.ToString() != string.Empty)
                        backup = @dialog.SelectedPath.ToString() + "\\";

                    if (backup != string.Empty && selectFile != string.Empty)
                        BackupZip(backup, selectFile);
                }
            }
            #endregion

            buttonEnabled(true);
        }
        #endregion

        #region cmdMove_Click
        private void cmdMove_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            #region Move
            string selectFile = string.Empty;

            //System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Multiselect = true;

            System.Windows.Forms.DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if (selectFile != string.Empty)
                        {
                            selectFile += Environment.NewLine;
                            selectFile += file;
                        }
                        else
                        {
                            selectFile = file;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);

                        break;
                    }
                }
            }

            if (!string.IsNullOrEmpty(selectFile))
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string move = string.Empty;

                    if (dialog.SelectedPath.ToString() != string.Empty)
                        move = @dialog.SelectedPath.ToString() + "\\";

                    if (move != string.Empty && selectFile != string.Empty)
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
                                    MoveFile(file, move + test);
                            }
                            catch (Exception ex)
                            {
                                ex.Message.ToString().ShowMessageBox(false);
                                break;
                            }
                        }
                    }

                    "Move complete ".ShowMessageBox();
                }
            }
            #endregion

            buttonEnabled(true);
        }
        #endregion

        #region cmdExpExcel_Click
        private void cmdExpExcel_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (gridPTF.ItemsSource != null || gridTENS.ItemsSource != null)
                ExportExcel();
            else
                "Please check data \n\r Data is null".ShowMessageBox();

            buttonEnabled(true);
        }
        #endregion 

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (gridPTF.ItemsSource != null || gridTENS.ItemsSource != null)
            {
                if (SavePTF() == true)
                {
                    "Save complete".ShowMessageBox();
                    ClearControl();
                }
            }
            else
                "Please check data \n\r Data is null".ShowMessageBox();

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

        private void gridPTF_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridPTF.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridPTF);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.CurrentCell.Item)).specimenLabel != null)
                            {
                              
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

        private void gridTENS_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridTENS.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridTENS);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            //if (((LuckyTex.Models.LAB_GETINSPECTIONLIST)(gridLAB.CurrentCell.Item)).INSPECTIONLOT != null)
                            //{
                            //    INSPECTIONLOT = ((LuckyTex.Models.LAB_GETINSPECTIONLIST)(gridLAB.CurrentCell.Item)).INSPECTIONLOT;
                            //}
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

        #region gridPTF_LoadingRow
        private void gridPTF_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).specimenLabel))
                {
                    if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).avg >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).ave1 >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).ave2 >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).ave3 >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).ave4 >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).ave5 >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListID_PTF)(e.Row.DataContext)).ave6 >= 1000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }

               
            }
            catch (Exception ex)
            {
                ex.Err();
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
            results = new List<DataControl.ClassData.PDFClassData.ListID_PTF>();
            results_TENS = new List<DataControl.ClassData.PDFClassData.ListID_TENS>();

            chkEditPTF = false;

            txtBrowse.Text = string.Empty;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridPTF.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridPTF.SelectedItems.Clear();
            else
                this.gridPTF.SelectedItem = null;

            gridPTF.ItemsSource = null;

            if (this.gridTENS.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridTENS.SelectedItems.Clear();
            else
                this.gridTENS.SelectedItem = null;

            gridTENS.ItemsSource = null;
        }

        #endregion

        #region LoadLAB
        private void LoadLAB()
        {
            //System.IO.Stream myStream = null;
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.Filter = "PDF files (*.pdf)|*.PDF";

            openFileDialog1.Multiselect = true;

            System.Windows.Forms.DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                txtBrowse.Text = string.Empty;

                if (chkEditPTF == true)
                {
                    if (gridPTF.ItemsSource != null)
                    {
                        results = new List<DataControl.ClassData.PDFClassData.ListID_PTF>();

                        for (int i = 0; i < gridPTF.Items.Count; i++)
                        {
                            DataControl.ClassData.PDFClassData.ListID_PTF inst = new DataControl.ClassData.PDFClassData.ListID_PTF();

                            inst.specimenLabel = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).specimenLabel;
                            inst.specimenRep = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).specimenRep;
                            inst.weavingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).weavingLot;
                            inst.weavingType = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).weavingType;
                            inst.finishingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).finishingLot;

                            inst.ave1 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).ave1;
                            inst.ave2 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).ave2;
                            inst.ave3 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).ave3;
                            inst.ave4 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).ave4;
                            inst.ave5 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).ave5;
                            inst.ave6 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).ave6;
                            inst.avg = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).avg;

                            results.Add(inst);
                        }

                        chkEditPTF = false;
                    }
                }

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if (txtBrowse.Text != string.Empty)
                        {
                            txtBrowse.Text += Environment.NewLine;
                            txtBrowse.Text += file;

                            if (file.Contains("is_ptf"))
                            {
                                ReadPDFtoDataTablePTF(file);
                            }
                            else if (file.Contains("is_tens"))
                            {
                                ReadPDFtoDataTableTENS(file);
                            }
                        }
                        else
                        {
                            txtBrowse.Text = file;

                            if (file.Contains("is_ptf"))
                            {
                                ReadPDFtoDataTablePTF(file);
                            }
                            else if (file.Contains("is_tens"))
                            {
                                ReadPDFtoDataTableTENS(file);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        System.Windows.Forms.MessageBox.Show("Cannot display the : " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);

                        break;
                    }
                }
            }
        }
        #endregion

        #region ReadPDFtoDataTablePTF
        private void ReadPDFtoDataTablePTF(string fileName)
        {
            int? count = results.Count;

            DataControl.ClassData.PDFClassData.ListID_PTF inst = new DataControl.ClassData.PDFClassData.ListID_PTF();

            inst = DataControl.ClassData.PDFClassData.Instance.LoadID_PTF(fileName);

            if (inst != null)
            {
                gridPTF.Items.Refresh();
                if (gridPTF.ItemsSource != null)
                {
                    bool chkData = true;

                    for (int i = 0; i < gridPTF.Items.Count; i++)
                    {
                        if (inst.specimenLabel == ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).specimenLabel
                         && inst.specimenRep == ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).specimenRep
                                && inst.weavingLot == ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).weavingLot
                                && inst.weavingType == ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).weavingType
                                && inst.finishingLot == ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF).Items[i]).finishingLot)
                        {
                            chkData = false;
                            break;
                        }
                    }

                    if (chkData == true)
                    {
                        if (inst.specimenLabel != string.Empty)
                            results.Add(inst);
                    }
                }
                else
                {
                    if (inst.specimenLabel != string.Empty)
                        results.Add(inst);
                }

                try
                {

                    if (null != results && results.Count > 0 && null != results[0])
                    {
                        if (count != results.Count)
                        {
                            if (gridPTF.ItemsSource != null)
                            {
                                gridPTF.ItemsSource = null;
                            }
                            gridPTF.Items.Refresh();
                            gridPTF.ItemsSource = results;
                        }
                    }
                    else
                    {
                        gridPTF.Items.Refresh();
                        gridPTF.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(false);
                }
            }

        }

        #endregion

        #region ReadPDFtoDataTableTENS
        private void ReadPDFtoDataTableTENS(string fileName)
        {
            int? count = results.Count;

            DataControl.ClassData.PDFClassData.ListID_TENS inst = new DataControl.ClassData.PDFClassData.ListID_TENS();

            inst = DataControl.ClassData.PDFClassData.Instance.LoadID_TENS(fileName);

            if (inst != null)
            {
                gridTENS.Items.Refresh();
                if (gridTENS.ItemsSource != null)
                {
                    bool chkData = true;

                    for (int i = 0; i < gridTENS.Items.Count; i++)
                    {
                        if (inst.specimenLabel == ((DataControl.ClassData.PDFClassData.ListID_TENS)(gridTENS).Items[i]).specimenLabel
                         && inst.specimenRep == ((DataControl.ClassData.PDFClassData.ListID_TENS)(gridTENS).Items[i]).specimenRep
                                && inst.weavingLot == ((DataControl.ClassData.PDFClassData.ListID_TENS)(gridTENS).Items[i]).weavingLot
                                && inst.weavingType == ((DataControl.ClassData.PDFClassData.ListID_TENS)(gridTENS).Items[i]).weavingType
                                && inst.scouringLot == ((DataControl.ClassData.PDFClassData.ListID_TENS)(gridTENS).Items[i]).scouringLot
                                && inst.methodType == ((DataControl.ClassData.PDFClassData.ListID_TENS)(gridTENS).Items[i]).methodType)
                        {
                            chkData = false;
                            break;
                        }
                    }

                    if (chkData == true)
                    {
                        if (inst.specimenLabel != string.Empty && inst.weavingLot != string.Empty && inst.scouringLot != string.Empty)
                            results_TENS.Add(inst);
                    }
                }
                else
                {
                    if (inst.specimenLabel != string.Empty && inst.weavingLot != string.Empty && inst.scouringLot != string.Empty)
                        results_TENS.Add(inst);
                }

                try
                {

                    if (null != results_TENS && results_TENS.Count > 0 && null != results_TENS[0])
                    {
                        if (count != results_TENS.Count)
                        {
                            if (gridTENS.ItemsSource != null)
                            {
                                gridTENS.ItemsSource = null;
                            }
                            gridTENS.Items.Refresh();
                            gridTENS.ItemsSource = results_TENS;
                        }
                    }
                    else
                    {
                        gridTENS.Items.Refresh();
                        gridTENS.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(false);
                }
            }
        }
        #endregion

        #region AVE_LostFocus

        private void AVE_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridPTF.SelectedItems.Count > 0)
                {
                    if (((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).specimenLabel != null)
                    {
                        int? i = 0;

                        decimal? ave1 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).ave1;
                        decimal? ave2 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).ave2;
                        decimal? ave3 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).ave3;
                        decimal? ave4 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).ave4;
                        decimal? ave5 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).ave5;
                        decimal? ave6 = ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).ave6;
                        decimal? Avg = 0;

                        #region Old
                        //if (ave1 != null && ave1 != 0)
                        //    i++;
                        //else
                        //    ave1 = 0;

                        //if (ave2 != null && ave2 != 0)
                        //    i++;
                        //else
                        //    ave2 = 0;

                        //if (ave3 != null && ave3 != 0)
                        //    i++;
                        //else
                        //    ave3 = 0;

                        //if (ave4 != null && ave4 != 0)
                        //    i++;
                        //else
                        //    ave4 = 0;

                        //if (ave5 != null && ave5 != 0)
                        //    i++;
                        //else
                        //    ave5 = 0;

                        //if (ave6 != null && ave6 != 0)
                        //    i++;
                        //else
                        //    ave6 = 0;
                        #endregion

                        #region New

                        if (ave1 == null)
                            ave1 = 0;
                        else
                            i++;

                        if (ave2 == null)
                            ave2 = 0;
                        else
                            i++;

                        if (ave3 == null)
                            ave3 = 0;
                        else
                            i++;

                        if (ave4 == null)
                            ave4 = 0;
                        else
                            i++;

                        if (ave5 == null)
                            ave5 = 0;
                        else
                            i++;

                        if (ave6 == null)
                            ave6 = 0;
                        else
                            i++;

                        #endregion

                        Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);

                        EditAvg(((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).specimenLabel
                            , ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).specimenRep
                            , ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).weavingLot
                            , ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).weavingType
                            , ((DataControl.ClassData.PDFClassData.ListID_PTF)(gridPTF.SelectedItem)).finishingLot
                            , Avg);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region EditAvg
        private void EditAvg(string specimenLabel, string specimenRep, string weavingLot, string weavingType, string finishingLot, decimal? avg)
        {
            if (gridPTF.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListID_PTF> dataList = new List<DataControl.ClassData.PDFClassData.ListID_PTF>();

                    int o = 0;
                    foreach (var row in gridPTF.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListID_PTF dataItem = new DataControl.ClassData.PDFClassData.ListID_PTF();

                        if (((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).specimenLabel == specimenLabel
                            && ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).specimenRep == specimenRep
                            && ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).weavingLot == weavingLot
                             && ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).weavingType == weavingType
                            && ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).finishingLot == finishingLot
                            )
                        {
                            dataItem.specimenLabel = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).specimenLabel;
                            dataItem.specimenRep = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).specimenRep;
                            dataItem.weavingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).weavingLot;
                            dataItem.weavingType = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).weavingType;
                            dataItem.finishingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).finishingLot;
                            dataItem.ave1 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave1;
                            dataItem.ave2 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave2;
                            dataItem.ave3 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave3;
                            dataItem.ave4 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave4;
                            dataItem.ave5 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave5;
                            dataItem.ave6 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave6;
                            dataItem.avg = avg;

                            dataList.Add(dataItem);

                            chkEditPTF = true;
                        }
                        else
                        {
                            dataItem.specimenLabel = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).specimenLabel;
                            dataItem.specimenRep = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).specimenRep;
                            dataItem.weavingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).weavingLot;
                            dataItem.weavingType = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).weavingType;
                            dataItem.finishingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).finishingLot;
                            dataItem.ave1 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave1;
                            dataItem.ave2 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave2;
                            dataItem.ave3 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave3;
                            dataItem.ave4 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave4;
                            dataItem.ave5 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave5;
                            dataItem.ave6 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).ave6;
                            dataItem.avg = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[o])).avg;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridPTF.ItemsSource = dataList;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region ExportExcel
        private void ExportExcel()
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string folder = string.Empty;

                    if (dialog.SelectedPath.ToString() != string.Empty)
                        folder = @dialog.SelectedPath.ToString() + "\\";

                    if (folder != string.Empty)
                    {
                        string tens = folder + "TENS_" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + ".xlsx";
                        string ptf = folder + "PTF_" + DateTime.Now.ToString("yy") + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd") + ".xlsx";

                        if (gridTENS.ItemsSource != null)
                        {
                            if (CreateSheet_TENS(tens) == true)
                            {
                                MessageBox.Show("Excel : " + tens, "Save Complete", MessageBoxButton.OK);
                            }
                        }

                        if (gridPTF.ItemsSource != null)
                        {
                            if (CreateSheet_PTF(ptf) == true)
                            {
                                MessageBox.Show("Excel : " + ptf, "Save Complete", MessageBoxButton.OK);
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

        #region ExportToExcel
        public System.Data.DataTable ExportToExcel()
        {
            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("specimenLabel", typeof(string));
            table.Columns.Add("specimenRep", typeof(string));
            table.Columns.Add("operatorID", typeof(string));
            table.Columns.Add("weavingLot", typeof(string));
            table.Columns.Add("weavingType", typeof(string));
            table.Columns.Add("finishingLot", typeof(string));

            table.Columns.Add("ave1", typeof(decimal));
            table.Columns.Add("ave2", typeof(decimal));
            table.Columns.Add("ave3", typeof(decimal));
            table.Columns.Add("ave4", typeof(decimal));
            table.Columns.Add("ave5", typeof(decimal));
            table.Columns.Add("ave6", typeof(decimal));
            table.Columns.Add("avg", typeof(decimal));
           

            string specimenLabel = string.Empty;
            string specimenRep = string.Empty;
            string operatorID = string.Empty;
            string weavingLot = string.Empty;
            string weavingType = string.Empty;
            string finishingLot = string.Empty;

            decimal? ave1 = null;
            decimal? ave2 = null;
            decimal? ave3 = null;
            decimal? ave4 = null;
            decimal? ave5 = null;
            decimal? ave6 = null;
            decimal? avg = null;

            int i = 0;
            foreach (var row in gridPTF.Items)
            {
                if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).specimenLabel))
                {
                    specimenLabel = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).specimenLabel;
                    specimenRep = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).specimenRep;
                    operatorID = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).operatorID;
                    weavingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).weavingLot;
                    weavingType = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).weavingType;
                    finishingLot = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).finishingLot;

                    ave1 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).ave1;
                    ave2 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).ave2;
                    ave3 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).ave3;
                    ave4 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).ave4;
                    ave5 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).ave5;
                    ave6 = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).ave6;
                    avg = ((DataControl.ClassData.PDFClassData.ListID_PTF)((gridPTF.Items)[i])).avg;

                    table.Rows.Add(specimenLabel, specimenRep, operatorID, weavingLot, weavingType, finishingLot, ave1, ave2, ave3, ave4, ave5, ave6, avg);
                }

                i++;
            }

            return table;
        }
        #endregion

        #region ExportToExcelTENS
        public System.Data.DataTable ExportToExcelTENS()
        {
            System.Data.DataTable table = new System.Data.DataTable();

            table.Columns.Add("specimenLabel", typeof(string));
            table.Columns.Add("specimenRep", typeof(string));
            table.Columns.Add("operatorID", typeof(string));
            table.Columns.Add("weavingLot", typeof(string));
            table.Columns.Add("weavingType", typeof(string));
            table.Columns.Add("scouringLot", typeof(string));

            table.Columns.Add("methodType", typeof(string));

            table.Columns.Add("lastTestDate", typeof(string));
            table.Columns.Add("lastTestTime", typeof(string));

            table.Columns.Add("max1", typeof(decimal));
            table.Columns.Add("maxStrain1", typeof(decimal));
            table.Columns.Add("max2", typeof(decimal));
            table.Columns.Add("maxStrain2", typeof(decimal));
            table.Columns.Add("max3", typeof(decimal));
            table.Columns.Add("maxStrain3", typeof(decimal));
            table.Columns.Add("max4", typeof(decimal));
            table.Columns.Add("maxStrain4", typeof(decimal));
            table.Columns.Add("max5", typeof(decimal));
            table.Columns.Add("maxStrain5", typeof(decimal));
            table.Columns.Add("max6", typeof(decimal));
            table.Columns.Add("maxStrain6", typeof(decimal));
           

            string specimenLabel = string.Empty;
            string specimenRep = string.Empty;
            string operatorID = string.Empty;
            string weavingLot = string.Empty;
            string weavingType = string.Empty;
            string scouringLot = string.Empty;

            string methodType = string.Empty;

            string lastTestDate = string.Empty;
            string lastTestTime = string.Empty;

            decimal? max1 = null;
            decimal? maxStrain1 = null;
            decimal? max2 = null;
            decimal? maxStrain2 = null;
            decimal? max3 = null;
            decimal? maxStrain3 = null;
            decimal? max4 = null;
            decimal? maxStrain4 = null;
            decimal? max5 = null;
            decimal? maxStrain5 = null;
            decimal? max6 = null;
            decimal? maxStrain6 = null;

            int i = 0;
            foreach (var row in gridTENS.Items)
            {
                if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).specimenLabel))
                {
                    specimenLabel = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).specimenLabel;
                    specimenRep = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).specimenRep;
                    operatorID = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).operatorID;
                    weavingLot = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).weavingLot;
                    weavingType = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).weavingType;
                    scouringLot = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).scouringLot;

                    methodType = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).methodType;

                    lastTestDate = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).lastTestDate;
                    lastTestTime = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).lastTestTime;

                    max1 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).max1;
                    maxStrain1 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).maxStrain1;
                    max2 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).max2;
                    maxStrain2 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).maxStrain2;
                    max3 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).max3;
                    maxStrain3 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).maxStrain3;
                    max4 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).max4;
                    maxStrain4 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).maxStrain4;
                    max5 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).max5;
                    maxStrain5 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).maxStrain5;
                    max6 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).max6;
                    maxStrain6 = ((DataControl.ClassData.PDFClassData.ListID_TENS)((gridTENS.Items)[i])).maxStrain6;

                    table.Rows.Add(specimenLabel, specimenRep, operatorID, weavingLot, weavingType, scouringLot, methodType, lastTestDate, lastTestTime
                        , max1, maxStrain1, max2, maxStrain2, max3, maxStrain3, max4, maxStrain4, max5, maxStrain5, max6, maxStrain6);
                }

                i++;
            }

            return table;
        }
        #endregion

        #region CreateSheet_PTF
        private bool CreateSheet_PTF(string newFileName)
        {

            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook worKbooK;
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            Microsoft.Office.Interop.Excel.Range celLrangE;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                worKbooK = excel.Workbooks.Add(Type.Missing);

                worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                worKsheeT.Name = "ID_PTF";

                int rowcount = 1;
                System.Data.DataTable export = ExportToExcel();

                foreach (DataRow datarow in export.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= export.Columns.Count; i++)
                    {
                        if (rowcount == 2)
                        {
                            worKsheeT.Cells[1, i] = export.Columns[i - 1].ColumnName;
                            worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;
                        }

                        worKsheeT.Cells[rowcount, i] = datarow[i - 1].ToString();

                        if (rowcount > 2)
                        {
                            if (i == export.Columns.Count)
                            {
                                if (rowcount % 1 == 0)
                                {
                                    celLrangE = worKsheeT.Range[worKsheeT.Cells[rowcount, 1], worKsheeT.Cells[rowcount, export.Columns.Count]];
                                }

                            }
                        }
                    }
                }

                celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[rowcount, export.Columns.Count]];
                celLrangE.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = celLrangE.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border.Weight = 2d;

                celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[2, export.Columns.Count]];
                worKbooK.SaveAs(newFileName);
                worKbooK.Close();
                excel.Quit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ex.Message.Err();
                return false;
            }
            finally
            {
                worKsheeT = null;
                celLrangE = null;
                worKbooK = null;
            }

        }
        #endregion

        #region CreateSheet_TENS
        private bool CreateSheet_TENS(string newFileName)
        {

            Microsoft.Office.Interop.Excel.Application excel;
            Microsoft.Office.Interop.Excel.Workbook worKbooK;
            Microsoft.Office.Interop.Excel.Worksheet worKsheeT;
            Microsoft.Office.Interop.Excel.Range celLrangE;

            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;

                worKbooK = excel.Workbooks.Add(Type.Missing);

                worKsheeT = (Microsoft.Office.Interop.Excel.Worksheet)worKbooK.ActiveSheet;
                worKsheeT.Name = "ID_TENS";

                System.Data.DataTable export = ExportToExcelTENS();
                int rowcount = 1;

                foreach (DataRow datarow in export.Rows)
                {
                    rowcount += 1;
                    for (int i = 1; i <= export.Columns.Count; i++)
                    {
                        if (rowcount == 2)
                        {
                            worKsheeT.Cells[1, i] = export.Columns[i - 1].ColumnName;
                            worKsheeT.Cells.Font.Color = System.Drawing.Color.Black;
                        }

                        worKsheeT.Cells[rowcount, i] = datarow[i - 1].ToString();

                        if (rowcount > 2)
                        {
                            if (i == export.Columns.Count)
                            {
                                if (rowcount % 1 == 0)
                                {
                                    celLrangE = worKsheeT.Range[worKsheeT.Cells[rowcount, 1], worKsheeT.Cells[rowcount, export.Columns.Count]];
                                }

                            }
                        }
                    }
                }

                celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[rowcount, export.Columns.Count]];
                celLrangE.EntireColumn.AutoFit();
                Microsoft.Office.Interop.Excel.Borders border = celLrangE.Borders;
                border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                celLrangE = worKsheeT.Range[worKsheeT.Cells[1, 1], worKsheeT.Cells[2, export.Columns.Count]];

                worKbooK.SaveAs(newFileName); ;
                worKbooK.Close();
                excel.Quit();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ex.Message.Err();
                return false;
            }
            finally
            {
                worKsheeT = null;
                celLrangE = null;
                worKbooK = null;
            }

        }
        #endregion

        #region SavePTF

        private bool SavePTF()
        {
            try
            {
                if (gridPTF.SelectedItems.Count > 0)
                {

                    string specimenLabel = string.Empty;

                    foreach (object obj in gridPTF.SelectedItems)
                    {
                        if(((DataControl.ClassData.PDFClassData.ListID_PTF)(obj)).specimenLabel != null)
                        {
                            specimenLabel = ((DataControl.ClassData.PDFClassData.ListID_PTF)(obj)).specimenLabel;
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

        #region BackupZip
        private void BackupZip(string backup, string selectFile)
        {
            try
            {
                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(Encoding.UTF8))
                {
                    string[] lines = selectFile.Split(new[] { Environment.NewLine },
                                     StringSplitOptions.RemoveEmptyEntries);
                    foreach (string file in lines)
                    {
                        zip.AddFile(file);
                    }

                    string n = backup + "backup_" + DateTime.Now.ToString("yyMMdd") + ".zip";
                    zip.Save(n);

                    "Zip complete".ShowMessageBox();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error", ex.Message);
            }
        }
        #endregion

        #region MoveFile
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
                    File.Delete(destinationFile);

                // Move the file.
                File.Move(sourceFile, destinationFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        #endregion

        #region ShowApprove
        void ShowApprove(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Approve", "Approve Or UnApprove" ,MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            LuckyTex.Windows.ApproveWindow win2 = new LuckyTex.Windows.ApproveWindow();
            win2.Show();
            win2.Topmost = true;
            win2.Activate();
        }
        #endregion

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdSearch.IsEnabled = enabled;
            cmdClear.IsEnabled = enabled;
            cmdBackup.IsEnabled = enabled;
            cmdMove.IsEnabled = enabled;
            cmdExpExcel.IsEnabled = enabled;
            cmdSave.IsEnabled = enabled;
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

