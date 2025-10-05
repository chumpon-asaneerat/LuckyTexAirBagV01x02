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
    /// Interaction logic for TearStrengthPage.xaml
    /// </summary>
    public partial class TearStrengthPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TearStrengthPage()
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

        List<DataControl.ClassData.PDFClassData.ListTEAR> results = new List<DataControl.ClassData.PDFClassData.ListTEAR>();
        bool chkEditTEAR = false;

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

            if (gridTEAR.ItemsSource != null)
            {
                if (ChkSave() == true)
                {
                    if (Save() == true)
                    {
                        "Upload Tear Strength Data Complete".ShowMessageBox();
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

        private void TEAR1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).TEAR1 = null;
            }
        }

        private void TEAR2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).TEAR2 = null;
            }
        }

        private void TEAR3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).TEAR3 = null;
            }
        }

        private void TEAR4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).TEAR4 = null;
            }
        }

        private void TEAR5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).TEAR5 = null;
            }
        }

        private void TEAR6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).TEAR6 = null;
            }
        }
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


        private void gridTEAR_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridTEAR.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridTEAR);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).ITMCODE != null
                                && ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).WEAVINGLOG != null
                                && ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).FINISHINGLOT != null
                                && ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).YARN != null)
                            {
                                itemCode = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).ITMCODE;
                                weavingLot = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).WEAVINGLOG;
                                finishingLot = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).FINISHINGLOT;
                                yarnType = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.CurrentCell.Item)).YARN;
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

        #region gridTEAR_LoadingRow
        private void gridTEAR_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                #region Foreground

                if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).ITMCODE) 
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).WEAVINGLOG)
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).FINISHINGLOT)
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).YARN))
                {
                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR1 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR2 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR3 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR4 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR5 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR6 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).AVE_TEAR >= 5000)
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
                //int? checkTEAR = 0;

                //if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).ITMCODE)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).WEAVINGLOG)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).FINISHINGLOT)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).YARN))
                //{
                //    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR1 != null 
                //        && ((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR1 > 0)
                //    {
                //        checkTEAR++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR2 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR2 > 0)
                //    {
                //        checkTEAR++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR3 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR3 > 0)
                //    {
                //        checkTEAR++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR4 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR4 > 0)
                //    {
                //        checkTEAR++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR5 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR5 > 0)
                //    {
                //        checkTEAR++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR6 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTEAR)(e.Row.DataContext)).TEAR6 > 0)
                //    {
                //        checkTEAR++;
                //    }

                //    if (checkTEAR == 3)
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
            results = new List<DataControl.ClassData.PDFClassData.ListTEAR>();

            itemCode = string.Empty;
            weavingLot = string.Empty;
            finishingLot = string.Empty;
            yarnType = string.Empty;

            chkEditTEAR = false;

            if (this.gridTEAR.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridTEAR.SelectedItems.Clear();
            else
                this.gridTEAR.SelectedItem = null;

            gridTEAR.ItemsSource = null;
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
                if (chkEditTEAR == true)
                {
                    if (gridTEAR.ItemsSource != null)
                    {
                        results = new List<DataControl.ClassData.PDFClassData.ListTEAR>();

                        for (int i = 0; i < gridTEAR.Items.Count; i++)
                        {
                            DataControl.ClassData.PDFClassData.ListTEAR inst = new DataControl.ClassData.PDFClassData.ListTEAR();

                            inst.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).ITMCODE;
                            inst.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).WEAVINGLOG;
                            inst.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).FINISHINGLOT;
                            inst.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).OPERATOR;
                            inst.YARN = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).YARN;

                            inst.TEAR1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1;
                            inst.TEAR2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2;
                            inst.TEAR3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3;
                            inst.TEAR4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4;
                            inst.TEAR5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5;
                            inst.TEAR6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6;
                            inst.AVE_TEAR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).AVE_TEAR;

                            inst.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).FILEPDF;

                            results.Add(inst);
                        }

                        chkEditTEAR = false;
                    }
                }

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if (file.Contains("is_ptf"))
                        {
                            ReadPDFtoDataTableTEAR(file);
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

        #region ReadPDFtoDataTableTEAR
        private void ReadPDFtoDataTableTEAR(string fileName)
        {
            int? count = results.Count;

            DataControl.ClassData.PDFClassData.ListTEAR inst = new DataControl.ClassData.PDFClassData.ListTEAR();

            inst = DataControl.ClassData.PDFClassData.Instance.LoadTEAR(fileName);

            if (inst != null)
            {
                gridTEAR.Items.Refresh();
                if (gridTEAR.ItemsSource != null)
                {
                    bool chkData = true;

                    for (int i = 0; i < gridTEAR.Items.Count; i++)
                    {
                        if (inst.ITMCODE == ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).ITMCODE
                         && inst.WEAVINGLOG == ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).WEAVINGLOG
                                && inst.FINISHINGLOT == ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).FINISHINGLOT
                                && inst.YARN == ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).YARN)
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
                            if (gridTEAR.ItemsSource != null)
                            {
                                gridTEAR.ItemsSource = null;
                            }
                            gridTEAR.Items.Refresh();
                            gridTEAR.ItemsSource = results;
                        }
                    }
                    else
                    {
                        gridTEAR.Items.Refresh();
                        gridTEAR.ItemsSource = null;
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
                if (gridTEAR.SelectedItems.Count > 0)
                {
                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).ITMCODE != null)
                    {
                        #region TEAR

                        int? i = 0;

                        decimal? ave1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).TEAR1;
                        decimal? ave2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).TEAR2;
                        decimal? ave3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).TEAR3;
                        decimal? ave4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).TEAR4;
                        decimal? ave5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).TEAR5;
                        decimal? ave6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).TEAR6;

                        decimal? Avg = 0;

                        decimal? old_ave = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).AVE_TEAR;

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

                            EditAvg(((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).ITMCODE
                                , ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).WEAVINGLOG
                                , ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).FINISHINGLOT
                                , ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR.SelectedItem)).YARN
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
            if (gridTEAR.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListTEAR> dataList = new List<DataControl.ClassData.PDFClassData.ListTEAR>();

                    int o = 0;
                    foreach (var row in gridTEAR.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListTEAR dataItem = new DataControl.ClassData.PDFClassData.ListTEAR();

                        if (((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).ITMCODE == ITMCODE
                            && ((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).WEAVINGLOG == WEAVINGLOG
                            && ((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).FINISHINGLOT == FINISHINGLOT
                             && ((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).YARN == YARN)
                        {

                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).OPERATOR;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).YARN;

                            dataItem.TEAR1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR1;
                            dataItem.TEAR2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR2;
                            dataItem.TEAR3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR3;
                            dataItem.TEAR4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR4;
                            dataItem.TEAR5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR5;
                            dataItem.TEAR6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR6;

                            if (avg != null)
                                dataItem.AVE_TEAR = avg;
                            else
                                dataItem.AVE_TEAR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).AVE_TEAR;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FILEPDF;

                            dataList.Add(dataItem);

                            chkEditTEAR = true;
                        }
                        else
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).OPERATOR;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).YARN;

                            dataItem.TEAR1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR1;
                            dataItem.TEAR2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR2;
                            dataItem.TEAR3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR3;
                            dataItem.TEAR4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR4;
                            dataItem.TEAR5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR5;
                            dataItem.TEAR6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR6;
                            dataItem.AVE_TEAR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).AVE_TEAR;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FILEPDF;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridTEAR.ItemsSource = dataList;

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

            if (gridTEAR.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListTEAR> dataList = new List<DataControl.ClassData.PDFClassData.ListTEAR>();

                    int o = 0;
                    foreach (var row in gridTEAR.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListTEAR dataItem = new DataControl.ClassData.PDFClassData.ListTEAR();

                        if (((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).ITMCODE == ITMCODE
                            && ((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).WEAVINGLOG == WEAVINGLOG
                            && ((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).FINISHINGLOT == FINISHINGLOT
                             && ((DataControl.ClassData.PDFClassData.ListTEAR)((gridTEAR.Items)[o])).YARN == YARN)
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).OPERATOR;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).YARN;

                            dataList.Remove(dataItem);

                            chkEditTEAR = true;
                        }
                        else
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).OPERATOR;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).YARN;

                            dataItem.TEAR1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR1;
                            dataItem.TEAR2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR2;
                            dataItem.TEAR3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR3;
                            dataItem.TEAR4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR4;
                            dataItem.TEAR5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR5;
                            dataItem.TEAR6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).TEAR6;
                            dataItem.AVE_TEAR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).AVE_TEAR;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[o]).FILEPDF;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridTEAR.ItemsSource = dataList;

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

                if (gridTEAR.ItemsSource != null)
                {
                    string P_ITMCODE = string.Empty;
                    string P_WEAVINGLOG = string.Empty;
                    string P_FINISHINGLOT = string.Empty;
                    string P_OPERATOR = string.Empty;
                    string P_TESTDATE = string.Empty; 
                    string P_TESTTIME  = string.Empty;
                    string P_YARN = string.Empty;

                    decimal? P_TEAR1 = null;
                    decimal? P_TEAR2  = null;
                    decimal? P_TEAR3  = null;
                    decimal? P_TEAR4 = null;
                    decimal? P_TEAR5 = null;
                    decimal? P_TEAR6 = null;

                    DateTime? P_UPLOADDATE = null;
                    P_UPLOADDATE = DateTime.Now;
                    string P_UPLOADBY = txtOperator.Text;

                    string P_FILEPDF = string.Empty;

                    for (int i = 0; i < gridTEAR.Items.Count; i++)
                    {
                        if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).ITMCODE != null
                                && ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).WEAVINGLOG != null
                                && ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).FINISHINGLOT != null
                                && ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).YARN != null)
                        {
                            P_ITMCODE = string.Empty;
                            P_WEAVINGLOG = string.Empty;
                            P_FINISHINGLOT = string.Empty;
                            P_OPERATOR = string.Empty;
                            P_TESTDATE = string.Empty;
                            P_TESTTIME = string.Empty;
                            P_YARN = string.Empty;

                            P_TEAR1 = null;
                            P_TEAR2 = null;
                            P_TEAR3 = null;
                            P_TEAR4 = null;
                            P_TEAR5 = null;
                            P_TEAR6 = null;

                            P_FILEPDF = string.Empty;

                            P_ITMCODE = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).ITMCODE;
                            P_WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).WEAVINGLOG;
                            P_FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).FINISHINGLOT;
                            P_OPERATOR = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).OPERATOR;
                            P_YARN = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).YARN;

                            P_FILEPDF = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).FILEPDF;

                            #region TEAR

                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1 != 0)
                                P_TEAR1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1;

                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2 != 0)
                                P_TEAR2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2;

                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3 != 0)
                                P_TEAR3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3;

                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4 != 0)
                                P_TEAR4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4;

                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5 != 0)
                                P_TEAR5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5;

                            if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6 != 0)
                                P_TEAR6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6;

                            ArrayList ar = new ArrayList();

                            if (P_TEAR1 != null)
                                ar.Add(P_TEAR1);
                            if (P_TEAR2 != null)
                                ar.Add(P_TEAR2);
                            if (P_TEAR3 != null)
                                ar.Add(P_TEAR3);
                            if (P_TEAR4 != null)
                                ar.Add(P_TEAR4);
                            if (P_TEAR5 != null)
                                ar.Add(P_TEAR5);
                            if (P_TEAR6 != null)
                                ar.Add(P_TEAR6);

                            if (ar.Count == 3)
                            {
                                P_TEAR1 = decimal.Parse(ar[0].ToString());
                                P_TEAR2 = decimal.Parse(ar[1].ToString());
                                P_TEAR3 = decimal.Parse(ar[2].ToString());
                            }

                            #endregion

                            if (LabDataPDFDataService.Instance.LAB_INSERTUPDATETEAR(P_ITMCODE, P_WEAVINGLOG, P_FINISHINGLOT, P_OPERATOR, P_YARN
                                                                                     , P_TEAR1, P_TEAR2, P_TEAR3, P_UPLOADDATE, P_UPLOADBY) == "0")
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

                decimal? P_TEAR1 = null;
                decimal? P_TEAR2 = null;
                decimal? P_TEAR3 = null;
                decimal? P_TEAR4 = null;
                decimal? P_TEAR5 = null;
                decimal? P_TEAR6 = null;

                for (int i = 0; i < gridTEAR.Items.Count; i++)
                {
                    P_TEAR1 = null;
                    P_TEAR2 = null;
                    P_TEAR3 = null;
                    P_TEAR4 = null;
                    P_TEAR5 = null;
                    P_TEAR6 = null;

                    #region TEAR

                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1 != null &&
                               ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1 != 0)
                        P_TEAR1 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR1;

                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2 != 0)
                        P_TEAR2 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR2;

                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3 != 0)
                        P_TEAR3 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR3;

                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4 != 0)
                        P_TEAR4 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR4;

                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5 != 0)
                        P_TEAR5 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR5;

                    if (((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6 != 0)
                        P_TEAR6 = ((DataControl.ClassData.PDFClassData.ListTEAR)(gridTEAR).Items[i]).TEAR6;

                    ArrayList ar = new ArrayList();

                    if (P_TEAR1 != null )
                        ar.Add(P_TEAR1);
                    if (P_TEAR2 != null)
                        ar.Add(P_TEAR2);
                    if (P_TEAR3 != null)
                        ar.Add(P_TEAR3);
                    if (P_TEAR4 != null)
                        ar.Add(P_TEAR4);
                    if (P_TEAR5 != null)
                        ar.Add(P_TEAR5);
                    if (P_TEAR6 != null)
                        ar.Add(P_TEAR6);

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
                backupFolder = ConfigManager.Instance.BackupFilePDFConfig_TearStrength;

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

