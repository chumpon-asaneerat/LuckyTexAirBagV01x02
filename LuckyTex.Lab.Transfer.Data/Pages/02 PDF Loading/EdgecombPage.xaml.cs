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

using System.Security.AccessControl;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for EdgecombPage.xaml
    /// </summary>
    public partial class EdgecombPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EdgecombPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            CreateDirectoryBackup();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string strFileName = string.Empty;
        string backupFolder = string.Empty;

        List<DataControl.ClassData.PDFClassData.ListEDGECOMB> results = new List<DataControl.ClassData.PDFClassData.ListEDGECOMB>();
        bool chkEditEDGECOMB = false;

        string itemCode = string.Empty;
        string weavingLot = string.Empty;
        string finishingLot = string.Empty;
        string yarnType = string.Empty;

        private void NumberValidationTextBox(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
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

        #region cmdSelectFile_Click

        private void cmdSelectFile_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);
            LoadLAB();
            buttonEnabled(true);
        }

        #endregion

        #region cmdClearAll_Click

        private void cmdClearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region cmdDelete_Click

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (itemCode != string.Empty && weavingLot != string.Empty
                    && finishingLot != string.Empty && yarnType != string.Empty)
            {
                string Chkfinish = "Do you want to Delete" + "\r\n"
                    + "Item Code : " + itemCode + "\r\n"
                    + "Weaving Lot : " + weavingLot + "\r\n"
                    + "Finishing Lot : " + finishingLot + "\r\n"
                    + "Yarn Type : " + yarnType;

                if (Chkfinish.ShowMessageOKCancel() == true)
                {
                    if (DeleteRow(itemCode, weavingLot, finishingLot, yarnType) == true)
                    {
                        itemCode = string.Empty;
                        weavingLot = string.Empty;
                        finishingLot = string.Empty;
                        yarnType = string.Empty;
                    }
                }
            }
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            buttonEnabled(false);

            if (gridEDGECOMB.ItemsSource != null)
            {
                if (ChkSave() == true)
                {
                    if (Save() == true)
                    {
                        "Upload Edgecomb Resistance Data Complete".ShowMessageBox();
                        ClearControl();
                    }
                }
                else
                {
                    "Samples Data must have 3 Values Please Check again".ShowMessageBox();
                }
            }
            else
                "Please check data \n\r Data is null".ShowMessageBox();

            buttonEnabled(true);
        }

        #endregion

        #endregion

        #region TextChanged

        private void EDGECOMB1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB1 = null;
            }
        }

        private void EDGECOMB2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB2 = null;
            }
        }

        private void EDGECOMB3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB3 = null;
            }
        }

        private void EDGECOMB4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB4 = null;
            }
        }

        private void EDGECOMB5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB5 = null;
            }
        }

        private void EDGECOMB6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB6 = null;
            }
        }

        #endregion

        #region gridEDGECOMB_SelectedCellsChanged

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


        private void gridEDGECOMB_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridEDGECOMB.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridEDGECOMB);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).ITMCODE != null
                                && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).WEAVINGLOG != null
                                && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).FINISHINGLOT != null
                                && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).YARN != null)
                            {
                                itemCode = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).ITMCODE;
                                weavingLot = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).WEAVINGLOG;
                                finishingLot = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).FINISHINGLOT;
                                yarnType = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.CurrentCell.Item)).YARN;
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

        #region gridEDGECOMB_LoadingRow
        private void gridEDGECOMB_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                #region Foreground

                if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).ITMCODE) 
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).WEAVINGLOG)
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).FINISHINGLOT)
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).YARN))
                {
                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB1 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB2 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB3 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB4 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB5 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB6 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).AVE_EDGECOMB >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }

                #endregion

                #region ไม่ได้ใช้
                //int? checkEDGECOMB = 0;

                //if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).ITMCODE)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).WEAVINGLOG)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).FINISHINGLOT)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).YARN))
                //{
                //    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB1 != null 
                //        && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB1 > 0)
                //    {
                //        checkEDGECOMB++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB2 != null
                //        && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB2 > 0)
                //    {
                //        checkEDGECOMB++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB3 != null
                //        && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB3 > 0)
                //    {
                //        checkEDGECOMB++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB4 != null
                //        && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB4 > 0)
                //    {
                //        checkEDGECOMB++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB5 != null
                //        && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB5 > 0)
                //    {
                //        checkEDGECOMB++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB6 != null
                //        && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(e.Row.DataContext)).EDGECOMB6 > 0)
                //    {
                //        checkEDGECOMB++;
                //    }

                //    if (checkEDGECOMB == 3)
                //    {
                //        e.Row.IsEnabled = false;
                //    }
                //    else
                //    {
                //        e.Row.IsEnabled = true;
                //    }
                //}
                #endregion
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
            results = new List<DataControl.ClassData.PDFClassData.ListEDGECOMB>();

            itemCode = string.Empty;
            weavingLot = string.Empty;
            finishingLot = string.Empty;
            yarnType = string.Empty;

            chkEditEDGECOMB = false;

            if (this.gridEDGECOMB.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridEDGECOMB.SelectedItems.Clear();
            else
                this.gridEDGECOMB.SelectedItem = null;

            gridEDGECOMB.ItemsSource = null;
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
                if (chkEditEDGECOMB == true)
                {
                    if (gridEDGECOMB.ItemsSource != null)
                    {
                        results = new List<DataControl.ClassData.PDFClassData.ListEDGECOMB>();

                        for (int i = 0; i < gridEDGECOMB.Items.Count; i++)
                        {
                            DataControl.ClassData.PDFClassData.ListEDGECOMB inst = new DataControl.ClassData.PDFClassData.ListEDGECOMB();

                            inst.ITMCODE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).ITMCODE;
                            inst.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).WEAVINGLOG;
                            inst.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).FINISHINGLOT;
                            inst.OPERATOR = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).OPERATOR;
                            inst.TESTDATE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).TESTDATE;
                            inst.TESTTIME = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).TESTTIME;
                            inst.YARN = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).YARN;

                            inst.EDGECOMB1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1;
                            inst.EDGECOMB2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2;
                            inst.EDGECOMB3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3;
                            inst.EDGECOMB4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4;
                            inst.EDGECOMB5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5;
                            inst.EDGECOMB6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6;
                            inst.AVE_EDGECOMB = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).AVE_EDGECOMB;

                            inst.FILEPDF = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).FILEPDF;

                            results.Add(inst);
                        }

                        chkEditEDGECOMB = false;
                    }
                }

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if (file.Contains("is_tens"))
                        {
                            ReadPDFtoDataTableEDGECOMB(file);
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

        #region ReadPDFtoDataTableEDGECOMB
        private void ReadPDFtoDataTableEDGECOMB(string fileName)
        {
            int? count = results.Count;

            DataControl.ClassData.PDFClassData.ListEDGECOMB inst = new DataControl.ClassData.PDFClassData.ListEDGECOMB();

            inst = DataControl.ClassData.PDFClassData.Instance.LoadEDGECOMB(fileName);

            if (inst != null)
            {
                gridEDGECOMB.Items.Refresh();
                if (gridEDGECOMB.ItemsSource != null)
                {
                    bool chkData = true;

                    for (int i = 0; i < gridEDGECOMB.Items.Count; i++)
                    {
                        if (inst.ITMCODE == ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).ITMCODE
                         && inst.WEAVINGLOG == ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).WEAVINGLOG
                                && inst.FINISHINGLOT == ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).FINISHINGLOT
                                && inst.YARN == ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).YARN)
                        {
                            chkData = false;
                            break;
                        }
                    }

                    if (chkData == true)
                    {
                        if (inst.ITMCODE != string.Empty && inst.WEAVINGLOG != string.Empty && inst.FINISHINGLOT != string.Empty && inst.YARN != string.Empty)
                            results.Add(inst);
                    }
                }
                else
                {
                    if (inst.ITMCODE != string.Empty && inst.WEAVINGLOG != string.Empty && inst.FINISHINGLOT != string.Empty && inst.YARN != string.Empty)
                        results.Add(inst);
                }

                try
                {

                    if (null != results && results.Count > 0 && null != results[0])
                    {
                        if (count != results.Count)
                        {
                            if (gridEDGECOMB.ItemsSource != null)
                            {
                                gridEDGECOMB.ItemsSource = null;
                            }
                            gridEDGECOMB.Items.Refresh();
                            gridEDGECOMB.ItemsSource = results;
                        }
                    }
                    else
                    {
                        gridEDGECOMB.Items.Refresh();
                        gridEDGECOMB.ItemsSource = null;
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
                if (gridEDGECOMB.SelectedItems.Count > 0)
                {
                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).ITMCODE != null)
                    {
                        #region EDGECOMB

                        int? i = 0;

                        decimal? ave1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB1;
                        decimal? ave2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB2;
                        decimal? ave3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB3;
                        decimal? ave4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB4;
                        decimal? ave5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB5;
                        decimal? ave6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).EDGECOMB6;
                        decimal? Avg = 0;

                        decimal? old_ave = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).AVE_EDGECOMB;

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

                        if (ave1 != 0 || ave2 != 0 || ave3 != 0 || ave4 != 0 || ave5 != 0 || ave6 != 0)
                            Avg = DataControl.ClassData.MathEx.Round(((ave1 + ave2 + ave3 + ave4 + ave5 + ave6) / i).Value, 2);
                        else
                            Avg = 0;

                        #endregion

                        if (old_ave != Avg )
                        {
                            if (old_ave == Avg)
                                Avg = null;

                            EditAvg(((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).ITMCODE
                                , ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).WEAVINGLOG
                                , ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).FINISHINGLOT
                                , ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB.SelectedItem)).YARN
                                , Avg);
                        }
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
        private void EditAvg(string ITMCODE, string WEAVINGLOG, string FINISHINGLOT, string YARN, decimal? avg)
        {
            if (gridEDGECOMB.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListEDGECOMB> dataList = new List<DataControl.ClassData.PDFClassData.ListEDGECOMB>();

                    int o = 0;
                    foreach (var row in gridEDGECOMB.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListEDGECOMB dataItem = new DataControl.ClassData.PDFClassData.ListEDGECOMB();

                        if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).ITMCODE == ITMCODE
                            && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).WEAVINGLOG == WEAVINGLOG
                            && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).FINISHINGLOT == FINISHINGLOT
                             && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).YARN == YARN)
                        {

                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).YARN;

                            dataItem.EDGECOMB1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB1;
                            dataItem.EDGECOMB2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB2;
                            dataItem.EDGECOMB3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB3;
                            dataItem.EDGECOMB4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB4;
                            dataItem.EDGECOMB5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB5;
                            dataItem.EDGECOMB6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB6;

                            if (avg != null)
                                dataItem.AVE_EDGECOMB = avg;
                            else
                                dataItem.AVE_EDGECOMB = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).AVE_EDGECOMB;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FILEPDF;

                            dataList.Add(dataItem);

                            chkEditEDGECOMB = true;
                        }
                        else
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).YARN;

                            dataItem.EDGECOMB1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB1;
                            dataItem.EDGECOMB2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB2;
                            dataItem.EDGECOMB3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB3;
                            dataItem.EDGECOMB4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB4;
                            dataItem.EDGECOMB5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB5;
                            dataItem.EDGECOMB6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB6;
                            dataItem.AVE_EDGECOMB = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).AVE_EDGECOMB;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FILEPDF;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridEDGECOMB.ItemsSource = dataList;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region DeleteRow
        private bool DeleteRow(string ITMCODE, string WEAVINGLOG, string FINISHINGLOT, string YARN)
        {
            bool chkErr = true;

            if (gridEDGECOMB.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListEDGECOMB> dataList = new List<DataControl.ClassData.PDFClassData.ListEDGECOMB>();

                    int o = 0;
                    foreach (var row in gridEDGECOMB.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListEDGECOMB dataItem = new DataControl.ClassData.PDFClassData.ListEDGECOMB();

                        if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).ITMCODE == ITMCODE
                            && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).WEAVINGLOG == WEAVINGLOG
                            && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).FINISHINGLOT == FINISHINGLOT
                             && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)((gridEDGECOMB.Items)[o])).YARN == YARN)
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).YARN;

                            dataList.Remove(dataItem);

                            chkEditEDGECOMB = true;
                        }
                        else
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).YARN;

                            dataItem.EDGECOMB1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB1;
                            dataItem.EDGECOMB2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB2;
                            dataItem.EDGECOMB3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB3;
                            dataItem.EDGECOMB4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB4;
                            dataItem.EDGECOMB5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB5;
                            dataItem.EDGECOMB6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).EDGECOMB6;
                            dataItem.AVE_EDGECOMB = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).AVE_EDGECOMB;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[o]).FILEPDF;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridEDGECOMB.ItemsSource = dataList;

                    chkErr = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    chkErr = false;
                }
            }

            return chkErr;
        }
        #endregion

        #region Save

        private bool Save()
        {
            try
            {
                bool chkSave = true;

                if (gridEDGECOMB.ItemsSource != null)
                {
                    string P_ITMCODE = string.Empty;
                    string P_WEAVINGLOG = string.Empty;
                    string P_FINISHINGLOT = string.Empty;
                    string P_OPERATOR = string.Empty;
                    string P_TESTDATE = string.Empty; 
                    string P_TESTTIME  = string.Empty;
                    string P_YARN = string.Empty;

                    decimal? P_EDGECOMB1 = null;
                    decimal? P_EDGECOMB2  = null;
                    decimal? P_EDGECOMB3  = null;
                    decimal? P_EDGECOMB4 = null;
                    decimal? P_EDGECOMB5 = null;
                    decimal? P_EDGECOMB6 = null;

                    DateTime? P_UPLOADDATE = null;
                    P_UPLOADDATE = DateTime.Now;
                    string P_UPLOADBY = txtOperator.Text;

                    string P_FILEPDF = string.Empty;

                    for (int i = 0; i < gridEDGECOMB.Items.Count; i++)
                    {
                        if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).ITMCODE != null
                                && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).WEAVINGLOG != null
                                && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).FINISHINGLOT != null
                                && ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).YARN != null)
                        {
                            P_ITMCODE = string.Empty;
                            P_WEAVINGLOG = string.Empty;
                            P_FINISHINGLOT = string.Empty;
                            P_OPERATOR = string.Empty;
                            P_TESTDATE = string.Empty;
                            P_TESTTIME = string.Empty;
                            P_YARN = string.Empty;

                            P_EDGECOMB1 = null;
                            P_EDGECOMB2 = null;
                            P_EDGECOMB3 = null;
                            P_EDGECOMB4 = null;
                            P_EDGECOMB5 = null;
                            P_EDGECOMB6 = null;
                            P_FILEPDF = string.Empty;

                            P_ITMCODE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).ITMCODE;
                            P_WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).WEAVINGLOG;
                            P_FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).FINISHINGLOT;
                            P_OPERATOR = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).OPERATOR;
                            P_TESTDATE = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).TESTDATE;
                            P_TESTTIME = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).TESTTIME;
                            P_YARN = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).YARN;

                            P_FILEPDF = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).FILEPDF;

                            #region EDGECOMB

                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1 != null &&
                                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1 != 0)
                                P_EDGECOMB1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1;

                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2 != null &&
                                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2 != 0)
                                P_EDGECOMB2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2;

                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3 != null &&
                                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3 != 0)
                                P_EDGECOMB3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3;

                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4 != null &&
                                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4 != 0)
                                P_EDGECOMB4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4;

                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5 != null &&
                                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5 != 0)
                                P_EDGECOMB5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5;

                            if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6 != null &&
                                ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6 != 0)
                                P_EDGECOMB6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6;

                            ArrayList ar = new ArrayList();

                            if (P_EDGECOMB1 != null)
                                ar.Add(P_EDGECOMB1);
                            if (P_EDGECOMB2 != null)
                                ar.Add(P_EDGECOMB2);
                            if (P_EDGECOMB3 != null)
                                ar.Add(P_EDGECOMB3);
                            if (P_EDGECOMB4 != null)
                                ar.Add(P_EDGECOMB4);
                            if (P_EDGECOMB5 != null)
                                ar.Add(P_EDGECOMB5);
                            if (P_EDGECOMB6 != null)
                                ar.Add(P_EDGECOMB6);

                            if (ar.Count == 3)
                            {
                                P_EDGECOMB1 = decimal.Parse(ar[0].ToString());
                                P_EDGECOMB2 = decimal.Parse(ar[1].ToString());
                                P_EDGECOMB3 = decimal.Parse(ar[2].ToString());
                            }

                            #endregion

                            if (LabDataPDFDataService.Instance.LAB_INSERTUPDATEEDGECOMB(P_ITMCODE, P_WEAVINGLOG, P_FINISHINGLOT, P_OPERATOR, P_TESTDATE, P_TESTTIME, P_YARN
                                                                                     , P_EDGECOMB1, P_EDGECOMB2, P_EDGECOMB3, P_UPLOADDATE, P_UPLOADBY) == "0")
                            {
                                "Can't Save Please check data".ShowMessageBox();
                                chkSave = false;
                                break;
                            }
                            else
                            {
                                if (MoveFilePDF(P_FILEPDF) == false)
                                {
                                    "Can't move file Please check data".ShowMessageBox();
                                    chkSave = false;
                                    break;
                                }
                            }
                        }
                    }
                }

                return chkSave;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region ChkSave
        private bool ChkSave()
        {
            try
            {
                bool chkTen = true;

                decimal? P_EDGECOMB1 = null;
                decimal? P_EDGECOMB2 = null;
                decimal? P_EDGECOMB3 = null;
                decimal? P_EDGECOMB4 = null;
                decimal? P_EDGECOMB5 = null;
                decimal? P_EDGECOMB6 = null;

                for (int i = 0; i < gridEDGECOMB.Items.Count; i++)
                {
                    P_EDGECOMB1 = null;
                    P_EDGECOMB2 = null;
                    P_EDGECOMB3 = null;
                    P_EDGECOMB4 = null;
                    P_EDGECOMB5 = null;
                    P_EDGECOMB6 = null;

                    #region EDGECOMB

                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1 != null &&
                               ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1 != 0)
                        P_EDGECOMB1 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB1;

                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2 != null &&
                        ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2 != 0)
                        P_EDGECOMB2 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB2;

                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3 != null &&
                        ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3 != 0)
                        P_EDGECOMB3 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB3;

                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4 != null &&
                        ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4 != 0)
                        P_EDGECOMB4 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB4;

                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5 != null &&
                        ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5 != 0)
                        P_EDGECOMB5 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB5;

                    if (((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6 != null &&
                        ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6 != 0)
                        P_EDGECOMB6 = ((DataControl.ClassData.PDFClassData.ListEDGECOMB)(gridEDGECOMB).Items[i]).EDGECOMB6;

                    ArrayList ar = new ArrayList();

                    if (P_EDGECOMB1 != null )
                        ar.Add(P_EDGECOMB1);
                    if (P_EDGECOMB2 != null)
                        ar.Add(P_EDGECOMB2);
                    if (P_EDGECOMB3 != null)
                        ar.Add(P_EDGECOMB3);
                    if (P_EDGECOMB4 != null)
                        ar.Add(P_EDGECOMB4);
                    if (P_EDGECOMB5 != null)
                        ar.Add(P_EDGECOMB5);
                    if (P_EDGECOMB6 != null)
                        ar.Add(P_EDGECOMB6);

                    int c = ar.Count;

                    if (ar.Count == 3)
                    {
                        chkTen = true;
                    }
                    else
                    {
                        chkTen = false;
                        break;
                    }

                    #endregion

                }

                return chkTen;
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
               

                ConfigManager.Instance.LoadBackupFilePDFConfigs();
                backupFolder = ConfigManager.Instance.BackupFilePDFConfig_Edgecomb;

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
                        {
                            Directory.CreateDirectory(backupFolder);
                        }
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

        #region buttonEnabled
        private void buttonEnabled(bool enabled)
        {
            cmdSelectFile.IsEnabled = enabled;
            cmdClearAll.IsEnabled = enabled;
            cmdDelete.IsEnabled = enabled;
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

