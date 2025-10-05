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
    /// Interaction logic for ElongationPage.xaml
    /// </summary>
    public partial class ElongationPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElongationPage()
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

        List<DataControl.ClassData.PDFClassData.ListTENSILE> results = new List<DataControl.ClassData.PDFClassData.ListTENSILE>();
        bool chkEditTENSILE = false;

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

            if (gridTENS.ItemsSource != null)
            {
                if (ChkSave() == true)
                {
                    if (Save() == true)
                    {
                        "Upload Tensile and Elongation Data Complete".ShowMessageBox();
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

        private void TENSILE1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE1 = null;
            }
        }

        private void TENSILE2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE2 = null;
            }
        }

        private void TENSILE3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE3 = null;
            }
        }

        private void TENSILE4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE4 = null;
            }
        }

        private void TENSILE5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE5 = null;
            }
        }

        private void TENSILE6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE6 = null;
            }
        }

        private void ELONG1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG1 = null;
            }
        }

        private void ELONG2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG2 = null;
            }
        }

        private void ELONG3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG3 = null;
            }
        }

        private void ELONG4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG4 = null;
            }
        }

        private void ELONG5_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG5 = null;
            }
        }

        private void ELONG6_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((System.Windows.Controls.TextBox)(((System.Windows.RoutedEventArgs)(e)).OriginalSource)).Text == string.Empty)
            {
                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG6 = null;
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
                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).ITMCODE != null
                                && ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).WEAVINGLOG != null
                                && ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).FINISHINGLOT != null
                                && ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).YARN != null)
                            {
                                itemCode = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).ITMCODE;
                                weavingLot = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).WEAVINGLOG;
                                finishingLot = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).FINISHINGLOT;
                                yarnType = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.CurrentCell.Item)).YARN;
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

        #region gridTENS_LoadingRow
        private void gridTENS_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            try
            {
                #region Foreground

                if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ITMCODE) 
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).WEAVINGLOG)
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).FINISHINGLOT)
                    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).YARN))
                {
                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE1 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE2 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE3 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE4 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE5 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE6 >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).AVE_TENSILE >= 5000)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG1 >= 100)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG2 >= 100)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG3 >= 100)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG4 >= 100)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG5 >= 100)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG6 >= 100)
                    {
                        e.Row.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).AVE_ELONG >= 100)
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

                //int? checkTENSILE = 0;
                //int? checkELONG = 0;

                //if (!string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ITMCODE)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).WEAVINGLOG)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).FINISHINGLOT)
                //    && !string.IsNullOrEmpty(((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).YARN))
                //{
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE1 != null 
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE1 > 0)
                //    {
                //        checkTENSILE++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE2 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE2 > 0)
                //    {
                //        checkTENSILE++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE3 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE3 > 0)
                //    {
                //        checkTENSILE++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE4 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE4 > 0)
                //    {
                //        checkTENSILE++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE5 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE5 > 0)
                //    {
                //        checkTENSILE++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE6 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).TENSILE6 > 0)
                //    {
                //        checkTENSILE++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG1 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG1 > 0)
                //    {
                //        checkELONG++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG2 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG2 > 0)
                //    {
                //        checkELONG++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG3 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG3 > 0)
                //    {
                //        checkELONG++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG4 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG4 > 0)
                //    {
                //        checkELONG++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG5 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG5 > 0)
                //    {
                //        checkELONG++;
                //    }
                //    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG6 != null
                //        && ((DataControl.ClassData.PDFClassData.ListTENSILE)(e.Row.DataContext)).ELONG6 > 0)
                //    {
                //        checkELONG++;
                //    }

                //    if (checkTENSILE == 3 && checkELONG == 3)
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
            results = new List<DataControl.ClassData.PDFClassData.ListTENSILE>();

            itemCode = string.Empty;
            weavingLot = string.Empty;
            finishingLot = string.Empty;
            yarnType = string.Empty;

            chkEditTENSILE = false;

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
                if (chkEditTENSILE == true)
                {
                    if (gridTENS.ItemsSource != null)
                    {
                        results = new List<DataControl.ClassData.PDFClassData.ListTENSILE>();

                        for (int i = 0; i < gridTENS.Items.Count; i++)
                        {
                            DataControl.ClassData.PDFClassData.ListTENSILE inst = new DataControl.ClassData.PDFClassData.ListTENSILE();

                            inst.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ITMCODE;
                            inst.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).WEAVINGLOG;
                            inst.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).FINISHINGLOT;
                            inst.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).OPERATOR;
                            inst.TESTDATE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TESTDATE;
                            inst.TESTTIME = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TESTTIME;
                            inst.YARN = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).YARN;

                            inst.TENSILE1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1;
                            inst.TENSILE2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2;
                            inst.TENSILE3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3;
                            inst.TENSILE4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4;
                            inst.TENSILE5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5;
                            inst.TENSILE6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6;
                            inst.AVE_TENSILE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).AVE_TENSILE;

                            inst.ELONG1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1;
                            inst.ELONG2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2;
                            inst.ELONG3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3;
                            inst.ELONG4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4;
                            inst.ELONG5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5;
                            inst.ELONG6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6;
                            inst.AVE_ELONG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).AVE_ELONG;

                            inst.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).FILEPDF;

                            results.Add(inst);
                        }

                        chkEditTENSILE = false;
                    }
                }

                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if (file.Contains("is_tens"))
                        {
                            ReadPDFtoDataTableTENSILE(file);
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

        #region ReadPDFtoDataTableTENSILE
        private void ReadPDFtoDataTableTENSILE(string fileName)
        {
            int? count = results.Count;

            DataControl.ClassData.PDFClassData.ListTENSILE inst = new DataControl.ClassData.PDFClassData.ListTENSILE();

            inst = DataControl.ClassData.PDFClassData.Instance.LoadTENSILE(fileName);

            if (inst != null)
            {
                gridTENS.Items.Refresh();
                if (gridTENS.ItemsSource != null)
                {
                    bool chkData = true;

                    for (int i = 0; i < gridTENS.Items.Count; i++)
                    {
                        if (inst.ITMCODE == ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ITMCODE
                         && inst.WEAVINGLOG == ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).WEAVINGLOG
                                && inst.FINISHINGLOT == ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).FINISHINGLOT
                                && inst.YARN == ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).YARN)
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
                            if (gridTENS.ItemsSource != null)
                            {
                                gridTENS.ItemsSource = null;
                            }
                            gridTENS.Items.Refresh();
                            gridTENS.ItemsSource = results;
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
                if (gridTENS.SelectedItems.Count > 0)
                {
                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ITMCODE != null)
                    {
                        #region TENSILE

                        int? i = 0;
                        int? o = 0;

                        decimal? ave1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE1;
                        decimal? ave2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE2;
                        decimal? ave3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE3;
                        decimal? ave4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE4;
                        decimal? ave5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE5;
                        decimal? ave6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).TENSILE6;
                        decimal? Avg = 0;

                        decimal? old_ave = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).AVE_TENSILE;

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

                        #region ELONG

                        decimal? elong1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG1;
                        decimal? elong2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG2;
                        decimal? elong3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG3;
                        decimal? elong4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG4;
                        decimal? elong5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG5;
                        decimal? elong6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ELONG6;
                        decimal? Avgelong = 0;

                        decimal? old_elong = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).AVE_ELONG;

                        if (elong1 != null && elong1 != 0)
                            o++;
                        else
                            elong1 = 0;

                        if (elong2 != null && elong2 != 0)
                            o++;
                        else
                            elong2 = 0;

                        if (elong3 != null && elong3 != 0)
                            o++;
                        else
                            elong3 = 0;

                        if (elong4 != null && elong4 != 0)
                            o++;
                        else
                            elong4 = 0;

                        if (elong5 != null && elong5 != 0)
                            o++;
                        else
                            elong5 = 0;

                        if (elong6 != null && elong6 != 0)
                            o++;
                        else
                            elong6 = 0;

                        if (elong1 != 0 || elong2 != 0 || elong3 != 0 || elong4 != 0 || elong5 != 0 || elong6 != 0)
                            Avgelong = DataControl.ClassData.MathEx.Round(((elong1 + elong2 + elong3 + elong4 + elong5 + elong6) / o).Value, 2);
                        else
                            Avgelong = 0;

                        #endregion

                        if (old_ave != Avg || old_elong != Avgelong)
                        {
                            if (old_ave == Avg)
                                Avg = null;
                            if (old_elong != Avgelong)
                                Avgelong = null;

                            EditAvg(((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).ITMCODE
                                , ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).WEAVINGLOG
                                , ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).FINISHINGLOT
                                , ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS.SelectedItem)).YARN
                                , Avg, Avgelong);
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
        private void EditAvg(string ITMCODE, string WEAVINGLOG, string FINISHINGLOT, string YARN, decimal? avg, decimal? Avgelong)
        {
            if (gridTENS.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListTENSILE> dataList = new List<DataControl.ClassData.PDFClassData.ListTENSILE>();

                    int o = 0;
                    foreach (var row in gridTENS.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListTENSILE dataItem = new DataControl.ClassData.PDFClassData.ListTENSILE();

                        if (((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).ITMCODE == ITMCODE
                            && ((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).WEAVINGLOG == WEAVINGLOG
                            && ((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).FINISHINGLOT == FINISHINGLOT
                             && ((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).YARN == YARN)
                        {

                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).YARN;

                            dataItem.TENSILE1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE1;
                            dataItem.TENSILE2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE2;
                            dataItem.TENSILE3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE3;
                            dataItem.TENSILE4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE4;
                            dataItem.TENSILE5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE5;
                            dataItem.TENSILE6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE6;

                            if (avg != null)
                                dataItem.AVE_TENSILE = avg;
                            else
                                dataItem.AVE_TENSILE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).AVE_TENSILE;

                            dataItem.ELONG1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG1;
                            dataItem.ELONG2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG2;
                            dataItem.ELONG3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG3;
                            dataItem.ELONG4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG4;
                            dataItem.ELONG5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG5;
                            dataItem.ELONG6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG6;

                            if (Avgelong != null)
                                dataItem.AVE_ELONG = Avgelong;
                            else
                                dataItem.AVE_ELONG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).AVE_ELONG;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FILEPDF;

                            dataList.Add(dataItem);

                            chkEditTENSILE = true;
                        }
                        else
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).YARN;

                            dataItem.TENSILE1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE1;
                            dataItem.TENSILE2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE2;
                            dataItem.TENSILE3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE3;
                            dataItem.TENSILE4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE4;
                            dataItem.TENSILE5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE5;
                            dataItem.TENSILE6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE6;
                            dataItem.AVE_TENSILE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).AVE_TENSILE;

                            dataItem.ELONG1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG1;
                            dataItem.ELONG2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG2;
                            dataItem.ELONG3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG3;
                            dataItem.ELONG4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG4;
                            dataItem.ELONG5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG5;
                            dataItem.ELONG6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG6;
                            dataItem.AVE_ELONG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).AVE_ELONG;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FILEPDF;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridTENS.ItemsSource = dataList;

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

            if (gridTENS.SelectedItems.Count > 0)
            {
                try
                {
                    List<DataControl.ClassData.PDFClassData.ListTENSILE> dataList = new List<DataControl.ClassData.PDFClassData.ListTENSILE>();

                    int o = 0;
                    foreach (var row in gridTENS.Items)
                    {
                        DataControl.ClassData.PDFClassData.ListTENSILE dataItem = new DataControl.ClassData.PDFClassData.ListTENSILE();

                        if (((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).ITMCODE == ITMCODE
                            && ((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).WEAVINGLOG == WEAVINGLOG
                            && ((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).FINISHINGLOT == FINISHINGLOT
                             && ((DataControl.ClassData.PDFClassData.ListTENSILE)((gridTENS.Items)[o])).YARN == YARN)
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).YARN;

                            dataList.Remove(dataItem);

                            chkEditTENSILE = true;
                        }
                        else
                        {
                            dataItem.ITMCODE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ITMCODE;
                            dataItem.WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).WEAVINGLOG;
                            dataItem.FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FINISHINGLOT;
                            dataItem.OPERATOR = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).OPERATOR;
                            dataItem.TESTDATE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTDATE;
                            dataItem.TESTTIME = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TESTTIME;
                            dataItem.YARN = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).YARN;

                            dataItem.TENSILE1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE1;
                            dataItem.TENSILE2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE2;
                            dataItem.TENSILE3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE3;
                            dataItem.TENSILE4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE4;
                            dataItem.TENSILE5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE5;
                            dataItem.TENSILE6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).TENSILE6;
                            dataItem.AVE_TENSILE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).AVE_TENSILE;

                            dataItem.ELONG1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG1;
                            dataItem.ELONG2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG2;
                            dataItem.ELONG3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG3;
                            dataItem.ELONG4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG4;
                            dataItem.ELONG5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG5;
                            dataItem.ELONG6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).ELONG6;
                            dataItem.AVE_ELONG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).AVE_ELONG;

                            dataItem.FILEPDF = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[o]).FILEPDF;

                            dataList.Add(dataItem);
                        }

                        o++;
                    }

                    this.gridTENS.ItemsSource = dataList;

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

                if (gridTENS.ItemsSource != null)
                {
                    string P_ITMCODE = string.Empty;
                    string P_WEAVINGLOG = string.Empty;
                    string P_FINISHINGLOT = string.Empty;
                    string P_OPERATOR = string.Empty;
                    string P_TESTDATE = string.Empty; 
                    string P_TESTTIME  = string.Empty;
                    string P_YARN = string.Empty;

                    decimal? P_TENSILE1 = null;
                    decimal? P_TENSILE2  = null;
                    decimal? P_TENSILE3  = null;
                    decimal? P_TENSILE4 = null;
                    decimal? P_TENSILE5 = null;
                    decimal? P_TENSILE6 = null;

                    decimal? P_ELONG1  = null;
                    decimal? P_ELONG2  = null;
                    decimal? P_ELONG3  = null;
                    decimal? P_ELONG4 = null;
                    decimal? P_ELONG5 = null;
                    decimal? P_ELONG6 = null;

                    DateTime? P_UPLOADDATE = null;
                    P_UPLOADDATE = DateTime.Now;
                    string P_UPLOADBY = txtOperator.Text;

                    string P_FILEPDF = string.Empty;

                    for (int i = 0; i < gridTENS.Items.Count; i++)
                    {
                        if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ITMCODE != null
                                && ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).WEAVINGLOG != null
                                && ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).FINISHINGLOT != null
                                && ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).YARN != null)
                        {
                            P_ITMCODE = string.Empty;
                            P_WEAVINGLOG = string.Empty;
                            P_FINISHINGLOT = string.Empty;
                            P_OPERATOR = string.Empty;
                            P_TESTDATE = string.Empty;
                            P_TESTTIME = string.Empty;
                            P_YARN = string.Empty;

                            P_TENSILE1 = null;
                            P_TENSILE2 = null;
                            P_TENSILE3 = null;
                            P_TENSILE4 = null;
                            P_TENSILE5 = null;
                            P_TENSILE6 = null;

                            P_ELONG1 = null;
                            P_ELONG2 = null;
                            P_ELONG3 = null;
                            P_ELONG4 = null;
                            P_ELONG5 = null;
                            P_ELONG6 = null;

                            P_FILEPDF = string.Empty;

                            P_ITMCODE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ITMCODE;
                            P_WEAVINGLOG = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).WEAVINGLOG;
                            P_FINISHINGLOT = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).FINISHINGLOT;
                            P_OPERATOR = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).OPERATOR;
                            P_TESTDATE = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TESTDATE;
                            P_TESTTIME = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TESTTIME;
                            P_YARN = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).YARN;

                            P_FILEPDF = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).FILEPDF;

                            #region TENSILE

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1 != 0)
                                P_TENSILE1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2 != 0)
                                P_TENSILE2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3 != 0)
                                P_TENSILE3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4 != 0)
                                P_TENSILE4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5 != 0)
                                P_TENSILE5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6 != 0)
                                P_TENSILE6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6;

                            ArrayList ar = new ArrayList();

                            if (P_TENSILE1 != null)
                                ar.Add(P_TENSILE1);
                            if (P_TENSILE2 != null)
                                ar.Add(P_TENSILE2);
                            if (P_TENSILE3 != null)
                                ar.Add(P_TENSILE3);
                            if (P_TENSILE4 != null)
                                ar.Add(P_TENSILE4);
                            if (P_TENSILE5 != null)
                                ar.Add(P_TENSILE5);
                            if (P_TENSILE6 != null)
                                ar.Add(P_TENSILE6);

                            if (ar.Count == 3)
                            {
                                P_TENSILE1 = decimal.Parse(ar[0].ToString());
                                P_TENSILE2 = decimal.Parse(ar[1].ToString());
                                P_TENSILE3 = decimal.Parse(ar[2].ToString());
                            }

                            #endregion

                            #region ELONG

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1 != 0)
                                P_ELONG1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2 != 0)
                                P_ELONG2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3 != 0)
                                P_ELONG3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4 != 0)
                                P_ELONG4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5 != 0)
                                P_ELONG5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5;

                            if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6 != null &&
                                ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6 != 0)
                                P_ELONG6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6;

                            ArrayList arElong = new ArrayList();

                            if (P_ELONG1 != null)
                                arElong.Add(P_ELONG1);
                            if (P_ELONG2 != null)
                                arElong.Add(P_ELONG2);
                            if (P_ELONG3 != null)
                                arElong.Add(P_ELONG3);
                            if (P_ELONG4 != null)
                                arElong.Add(P_ELONG4);
                            if (P_ELONG5 != null)
                                arElong.Add(P_ELONG5);
                            if (P_ELONG6 != null)
                                arElong.Add(P_ELONG6);

                            if (arElong.Count == 3)
                            {
                                P_ELONG1 = decimal.Parse(arElong[0].ToString());
                                P_ELONG2 = decimal.Parse(arElong[1].ToString());
                                P_ELONG3 = decimal.Parse(arElong[2].ToString());
                            }

                            #endregion

                            if (LabDataPDFDataService.Instance.LAB_INSERTUPDATETENSILE(P_ITMCODE, P_WEAVINGLOG, P_FINISHINGLOT, P_OPERATOR, P_TESTDATE, P_TESTTIME, P_YARN
                                                                                     , P_TENSILE1, P_TENSILE2, P_TENSILE3, P_ELONG1, P_ELONG2, P_ELONG3, P_UPLOADDATE, P_UPLOADBY) == "0")
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

                decimal? P_TENSILE1 = null;
                decimal? P_TENSILE2 = null;
                decimal? P_TENSILE3 = null;
                decimal? P_TENSILE4 = null;
                decimal? P_TENSILE5 = null;
                decimal? P_TENSILE6 = null;

                decimal? P_ELONG1 = null;
                decimal? P_ELONG2 = null;
                decimal? P_ELONG3 = null;
                decimal? P_ELONG4 = null;
                decimal? P_ELONG5 = null;
                decimal? P_ELONG6 = null;

                for (int i = 0; i < gridTENS.Items.Count; i++)
                {
                    P_TENSILE1 = null;
                    P_TENSILE2 = null;
                    P_TENSILE3 = null;
                    P_TENSILE4 = null;
                    P_TENSILE5 = null;
                    P_TENSILE6 = null;

                    P_ELONG1 = null;
                    P_ELONG2 = null;
                    P_ELONG3 = null;
                    P_ELONG4 = null;
                    P_ELONG5 = null;
                    P_ELONG6 = null;

                    #region TENSILE

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1 != null &&
                               ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1 != 0)
                        P_TENSILE1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE1;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2 != 0)
                        P_TENSILE2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE2;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3 != 0)
                        P_TENSILE3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE3;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4 != 0)
                        P_TENSILE4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE4;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5 != 0)
                        P_TENSILE5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE5;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6 != 0)
                        P_TENSILE6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).TENSILE6;

                    ArrayList ar = new ArrayList();

                    if (P_TENSILE1 != null )
                        ar.Add(P_TENSILE1);
                    if (P_TENSILE2 != null)
                        ar.Add(P_TENSILE2);
                    if (P_TENSILE3 != null)
                        ar.Add(P_TENSILE3);
                    if (P_TENSILE4 != null)
                        ar.Add(P_TENSILE4);
                    if (P_TENSILE5 != null)
                        ar.Add(P_TENSILE5);
                    if (P_TENSILE6 != null)
                        ar.Add(P_TENSILE6);

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

                    #region ELONG

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1 != 0)
                        P_ELONG1 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG1;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2 != 0)
                        P_ELONG2 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG2;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3 != 0)
                        P_ELONG3 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG3;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4 != 0)
                        P_ELONG4 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG4;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5 != 0)
                        P_ELONG5 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG5;

                    if (((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6 != null &&
                        ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6 != 0)
                        P_ELONG6 = ((DataControl.ClassData.PDFClassData.ListTENSILE)(gridTENS).Items[i]).ELONG6;

                    ArrayList arElong = new ArrayList();

                    if (P_ELONG1 != null)
                        arElong.Add(P_ELONG1);
                    if (P_ELONG2 != null)
                        arElong.Add(P_ELONG2);
                    if (P_ELONG3 != null)
                        arElong.Add(P_ELONG3);
                    if (P_ELONG4 != null)
                        arElong.Add(P_ELONG4);
                    if (P_ELONG5 != null)
                        arElong.Add(P_ELONG5);
                    if (P_ELONG6 != null)
                        arElong.Add(P_ELONG6);

                    if (arElong.Count == 3)
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
                backupFolder = ConfigManager.Instance.BackupFilePDFConfig_Elongation;

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

