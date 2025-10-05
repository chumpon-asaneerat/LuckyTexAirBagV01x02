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

using System.Text.RegularExpressions;

#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for EntrySampleTestData1Window.xaml
    /// </summary>
    public partial class EntrySampleTestData1Window : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntrySampleTestData1Window()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            ClearControl();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;

        string ITM_CODE = string.Empty;
        string WEAVINGLOT = string.Empty;
        string FINISHINGLOT = string.Empty;
        DateTime? ENTRYDATE = DateTime.Now;
        string METHOD = string.Empty;
        int? NO = 0;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtITMCODE.Text = ITM_CODE;
            txtWEAVINGLOG.Text = WEAVINGLOT;
            txtFINISHINGLOT.Text = FINISHINGLOT;
            dteEntryDate.SelectedDate = ENTRYDATE;
            txtMethod.Text = METHOD;

            LoadLabData(ITM_CODE, WEAVINGLOT, FINISHINGLOT, METHOD);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #endregion

        #region cmdSave_Click
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (Save() == true)
            {
                this.DialogResult = true;
            }
            //else
            //{
            //    this.DialogResult = false;
            //}
        }
        #endregion

        #endregion

        #region gridLab_SampleTestData

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

        private void gridLab_SampleTestData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridLab_SampleTestData.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridLab_SampleTestData);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_ITMCODE != null
                                && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_PRODUCTIONLOT != null
                                && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_FINISHINGLOT != null
                                && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_ENTRYDATE != null
                                && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_METHOD != null
                                && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_NO != null)
                            {
                                if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_VALUE == null)
                                {
                                    //((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData.CurrentCell.Item)).P_VALUE = 0;
                                }
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

        private void gridLab_SampleTestData_LostFocus(object sender, RoutedEventArgs e)
        {
            AveGrid();
        }

        #endregion

        #region private Methods

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            txtITMCODE.Text = string.Empty;
            txtWEAVINGLOG.Text = string.Empty;
            txtFINISHINGLOT.Text = string.Empty;
            dteEntryDate.SelectedDate = null;

            gridLab_SampleTestData.ItemsSource = null;
            gridLab_SampleTestData.Focus();
        }

        #endregion

        #region LoadLabData
        private bool LoadLabData(string P_ITMCODE, string P_PRODUCTIONLOT, string P_FINISHINGLOT, string P_METHOD)
        {
            try
            {
                if (NO > 0)
                {
                    List<LAB_GETSAMPLEDATABYMETHOD> results = LabDataPDFDataService.Instance.LAB_GETSAMPLEDATABYMETHOD(P_ITMCODE, P_PRODUCTIONLOT, P_FINISHINGLOT, P_METHOD);

                    List<LAB_INSERTSAMPLEDATA> labResults = new List<LAB_INSERTSAMPLEDATA>();

                    LAB_INSERTSAMPLEDATA labData = new LAB_INSERTSAMPLEDATA();

                    if (results != null)
                    {
                        if (results.Count > 0)
                        {
                            #region Get Data

                            for (int page = 0; page < NO ; page++)
                            {
                                if (page <= results.Count - 1)
                                {
                                    if (results[page].NO != null)
                                    {
                                        labData = new LAB_INSERTSAMPLEDATA();

                                        if (!string.IsNullOrEmpty(results[page].ITM_CODE))
                                            labData.P_ITMCODE = results[page].ITM_CODE;

                                        if (!string.IsNullOrEmpty(results[page].PRODUCTIONLOT))
                                            labData.P_PRODUCTIONLOT = results[page].PRODUCTIONLOT;

                                        if (!string.IsNullOrEmpty(results[page].FINISHINGLOT))
                                            labData.P_FINISHINGLOT = results[page].FINISHINGLOT;

                                        if (results[page].ENTRYDATE != null)
                                            labData.P_ENTRYDATE = results[page].ENTRYDATE;

                                        labData.P_ENTRYBY = opera;

                                        if (!string.IsNullOrEmpty(results[page].METHOD))
                                            labData.P_METHOD = results[page].METHOD;

                                        if (results[page].NO != null)
                                            labData.P_NO = results[page].NO;

                                        if (results[page].VALUE != null)
                                        {
                                            labData.P_VALUE = results[page].VALUE;
                                            labData.P_VALUE_OLD = results[page].VALUE;
                                        }

                                        labResults.Add(labData);
                                    }
                                }
                                else
                                {
                                    labData = new LAB_INSERTSAMPLEDATA();

                                    labData.P_ITMCODE = ITM_CODE;
                                    labData.P_PRODUCTIONLOT = WEAVINGLOT;
                                    labData.P_FINISHINGLOT = FINISHINGLOT;
                                    labData.P_ENTRYDATE = ENTRYDATE;
                                    labData.P_ENTRYBY = opera;
                                    labData.P_METHOD = METHOD;

                                    labData.P_NO = page+1;
                                    labData.P_VALUE = 0;
                                    labData.P_VALUE_OLD = 0;

                                    labResults.Add(labData);
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            #region Get Data

                            for (int page = 1; page <= NO; page++)
                            {
                                labData = new LAB_INSERTSAMPLEDATA();

                                labData.P_ITMCODE = ITM_CODE;
                                labData.P_PRODUCTIONLOT = WEAVINGLOT;
                                labData.P_FINISHINGLOT = FINISHINGLOT;
                                labData.P_ENTRYDATE = ENTRYDATE;
                                labData.P_ENTRYBY = opera;
                                labData.P_METHOD = METHOD;

                                labData.P_NO = page;
                                labData.P_VALUE = 0;
                                labData.P_VALUE_OLD = 0;

                                labResults.Add(labData);
                            }

                            #endregion
                        }
                    }
                    else
                    {
                        #region Get Data

                        for (int page = 1; page <= NO; page++)
                        {
                            labData = new LAB_INSERTSAMPLEDATA();

                            labData.P_ITMCODE = ITM_CODE;
                            labData.P_PRODUCTIONLOT = WEAVINGLOT;
                            labData.P_FINISHINGLOT = FINISHINGLOT;
                            labData.P_ENTRYDATE = ENTRYDATE;
                            labData.P_ENTRYBY = opera;
                            labData.P_METHOD = METHOD;

                            labData.P_NO = page;
                            labData.P_VALUE = 0;
                            labData.P_VALUE_OLD = 0;

                            labResults.Add(labData);
                        }

                        #endregion
                    }

                    if (labResults != null)
                    {
                        if (labResults.Count > 0)
                        {
                            gridLab_SampleTestData.ItemsSource = labResults;

                            AveGrid();
                        }
                        else
                        {
                            gridLab_SampleTestData.ItemsSource = null;
                            txtAverage.Text = "0";
                        }
                    }
                    else
                    {
                        gridLab_SampleTestData.ItemsSource = null;
                        txtAverage.Text = "0";
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }
        #endregion

        #region Save

        private bool Save()
        {
            try
            {
                #region ตัวแปร

                string insert = string.Empty;

                string P_ITMCODE = string.Empty;
                string P_PRODUCTIONLOT = string.Empty;
                string P_FINISHINGLOT = string.Empty;
                DateTime? P_ENTRYDATE = null;
                string P_ENTRYBY = string.Empty;
                string P_YARN = string.Empty;
                string P_METHOD = string.Empty;

                decimal? P_NO = null;
                decimal? P_VALUE = null;

                #endregion

                if (chkDataOnForm() == false)
                {
                    "Please Fill In All Test Result Data".ShowMessageBox();
                    save = false;
                }
                else
                {
                    if (gridLab_SampleTestData.ItemsSource != null)
                    {
                        for (int i = 0; i < gridLab_SampleTestData.Items.Count; i++)
                        {
                             P_ITMCODE = string.Empty;
                             P_PRODUCTIONLOT = string.Empty;
                             P_FINISHINGLOT = string.Empty;
                             P_ENTRYDATE = null;
                             P_ENTRYBY = string.Empty;
                             P_YARN = string.Empty;
                             P_METHOD = string.Empty;

                             P_NO = null;
                             P_VALUE = null;

                            if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_ITMCODE != null
                              && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_PRODUCTIONLOT != null
                              && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_FINISHINGLOT != null
                              && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_METHOD != null)
                            {
                                P_ITMCODE= ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_ITMCODE;
                                P_PRODUCTIONLOT = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_PRODUCTIONLOT;
                                P_FINISHINGLOT = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_FINISHINGLOT;
                                P_ENTRYDATE = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_ENTRYDATE;
                                P_ENTRYBY = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_ENTRYBY;
                                P_YARN = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_YARN;

                                P_METHOD = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_METHOD;
                                P_NO = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_NO;
                                P_VALUE = ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE;

                                if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE != ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE_OLD)
                                {
                                    insert = LabDataPDFDataService.Instance.LAB_INSERTSAMPLEDATA(P_ITMCODE, P_PRODUCTIONLOT, P_FINISHINGLOT, P_ENTRYDATE, P_ENTRYBY, P_YARN, P_METHOD, P_NO, P_VALUE);

                                    if (insert != "1")
                                    {
                                        //"Can't Save Please check data".ShowMessageBox();
                                        insert.ShowMessageBox();
                                        save = false;
                                        break;
                                    }
                                    else
                                        save = true;
                                }
                            }
                        }
                    }
                }

                return save;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region chkDataOnForm
        private bool chkDataOnForm()
        {
            try
            {
                bool chkSave = true;

                #region check data

                for (int i = 1; i == 1; i++)
                {
                    if (string.IsNullOrEmpty(txtITMCODE.Text))
                    {
                        chkSave = false;
                        break;
                    }
                    if (string.IsNullOrEmpty(txtWEAVINGLOG.Text))
                    {
                        chkSave = false;
                        break;
                    }
                    if (string.IsNullOrEmpty(txtFINISHINGLOT.Text))
                    {
                        chkSave = false;
                        break;
                    }

                    if (dteEntryDate.SelectedDate == null)
                    {
                        chkSave = false;
                        break;
                    }
                }
                #endregion

                if (gridLab_SampleTestData.ItemsSource != null)
                {
                    for (int i = 0; i < gridLab_SampleTestData.Items.Count; i++)
                    {
                        if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE == null)
                        {
                            chkSave = false;
                            break;
                        }
                        else if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE == 0)
                        {
                            chkSave = false;
                            break;
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

        #region AveGrid
        private void AveGrid()
        {
            try
            {
                if (gridLab_SampleTestData.ItemsSource != null)
                {
                    decimal? no = 0;
                    decimal? sum = 0;

                    for (int i = 0; i < gridLab_SampleTestData.Items.Count; i++)
                    {
                        if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_NO != null
                            && ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE != null)
                        {
                            if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE > 0)
                                no++;
                        }

                        if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE != null)
                        {
                            if (((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE > 0)
                                sum += ((LuckyTex.Models.LAB_INSERTSAMPLEDATA)(gridLab_SampleTestData).Items[i]).P_VALUE;
                        }
                    }

                    if (sum != 0 && no != 0)
                    {
                        decimal? avg = (sum / no);

                        txtAverage.Text = avg.Value.ToString("#,##0.##");
                    }
                    else
                    {
                        txtAverage.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #endregion

        #region Public Methods

        bool save = true;

        public bool ChkSave
        {
            get { return save; }
            set { save = value; }
        }

        #region Setup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="itemCode"></param>
        /// <param name="weavingLot"></param>
        /// <param name="finishingLot"></param>
        /// <param name="entryDate"></param>
        /// <param name="method"></param>
        /// <param name="No"></param>
        public void Setup(string user, string itemCode, string weavingLot, string finishingLot, DateTime? entryDate, string method,int? No)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (ITM_CODE != null)
            {
                ITM_CODE = itemCode;
            }

            if (WEAVINGLOT != null)
            {
                WEAVINGLOT = weavingLot;
            }

            if (FINISHINGLOT != null)
            {
                FINISHINGLOT = finishingLot;
            }

            if (ENTRYDATE != null)
            {
                ENTRYDATE = entryDate;
            }

            if (METHOD != null)
            {
                METHOD = method;
            }

            if (NO != null)
            {
                NO = No;
            }
        }
        #endregion

        #endregion

    }
}

