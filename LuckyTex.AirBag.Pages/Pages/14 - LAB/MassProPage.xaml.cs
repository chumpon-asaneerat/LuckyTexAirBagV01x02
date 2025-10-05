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

using System.Diagnostics;
using System.Printing;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for MassProPage.xaml
    /// </summary>
    public partial class MassProPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public MassProPage()
        {
            InitializeComponent();
            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadFabricType();
            LoadLabResult();
            LoadItemGood();

            Judgment = new List<string> { "Pass", "Fail", "Null" };
        }

        #endregion

        #region Internal Variables

        private LABSession _session = new LABSession();
        string opera = string.Empty;
      
        public List<string> Judgment { get; set; }

        #endregion

        #region Load/Unload

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

        #region Main Menu Button Handlers
        
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (Save() == true)
            {
                Search();
            }
        }

        #endregion

        #region Controls Handlers

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtWEAVINGLOT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtWEAVINGLOT.Text != "")
                {
                    cbItemCode.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        #endregion

        #region gridLab_SelectedCellsChanged

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

        private void gridLab_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridLab.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridLab);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (!string.IsNullOrEmpty(((LuckyTex.Models.LAB_SEARCHLABMASSPRO)(gridLab.SelectedItem)).WEAVINGLOT)
                                && string.IsNullOrEmpty(((LuckyTex.Models.LAB_SEARCHLABMASSPRO)(gridLab.SelectedItem)).TESTRESULT))
                            {
                                single_row.IsEnabled = true;
                            }
                            else
                            {
                                single_row.IsEnabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        #region LoadFabricType

        private void LoadFabricType()
        {
            if (cbFabricType.ItemsSource == null)
            {
                string[] str = new string[] { "All", "GRAY", "COATED" };

                cbFabricType.ItemsSource = str;
                cbFabricType.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadLabResult

        private void LoadLabResult()
        {
            if (cbLabResult.ItemsSource == null)
            {
                string[] str = new string[] { "All", "Pass", "Fail" };

                cbLabResult.ItemsSource = str;
                cbLabResult.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<ITM_GETITEMCODELIST> items = _session.GetItemCodeData();

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

        #region ClearControl

        private void ClearControl()
        {
            txtWEAVINGLOT.Text = "";

            cbItemCode.SelectedValue = null;
            cbLabResult.SelectedValue = null;
            cbFabricType.SelectedValue = null;
            dteSendDate.SelectedDate = null;
           
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLab.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLab.SelectedItems.Clear();
            else
                this.gridLab.SelectedItem = null;

            gridLab.ItemsSource = null;

            txtWEAVINGLOT.SelectAll();
            txtWEAVINGLOT.Focus();
        }

        #endregion

        #region Search

        private void Search()
        {
            string P_WEAVINGLOT = string.Empty;
            string P_ITMCODE = string.Empty;
            string P_FINISHINGLOT = string.Empty;
            string P_TESTRESULT = string.Empty;
            string P_SENDDATE = null;

            #region Search

            if (!string.IsNullOrEmpty(txtWEAVINGLOT.Text))
            {
                P_WEAVINGLOT = txtWEAVINGLOT.Text;
            }
            else
            {
                P_WEAVINGLOT = null;
            }

            if (cbItemCode.SelectedValue != null)
            {
                P_ITMCODE = cbItemCode.SelectedValue.ToString();
            }
            else
            {
                P_ITMCODE = null;
            }

            if (cbFabricType.SelectedValue != null)
            {
                string finish = cbFabricType.SelectedValue.ToString();
                if (finish != "All")
                {
                    P_FINISHINGLOT = cbFabricType.SelectedValue.ToString();
                }
                else
                {
                    P_FINISHINGLOT = null;
                }
            }
            else
            {
                P_FINISHINGLOT = null;
            }


            if (cbLabResult.SelectedValue != null)
            {
                string testresult = cbLabResult.SelectedValue.ToString();

                if (testresult != "All")
                {
                    P_TESTRESULT = cbLabResult.SelectedValue.ToString();
                }
                else
                {
                    P_TESTRESULT = null;
                }
            }
            else
            {
                P_TESTRESULT = null;
            }

            if (dteSendDate.SelectedDate != null)
            {
                P_SENDDATE = dteSendDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                P_SENDDATE = null;
            }

            #endregion

            List<LAB_SEARCHLABMASSPRO> results = null;

            results = LABDataService.Instance.LAB_SEARCHLABMASSPRO(P_WEAVINGLOT, P_ITMCODE, P_FINISHINGLOT, P_SENDDATE, P_TESTRESULT);

            if (results != null && results.Count > 0)
            {
                gridLab.ItemsSource = results;
            }
            else
            {
                gridLab.ItemsSource = null;
            }
        }

        #endregion

        #region Save

        private bool Save()
        {
            try
            {
                string P_WEAVELOT = string.Empty;
                string P_ITMCODE = string.Empty;
                string P_FINISHINGLOT = string.Empty;
                string P_TESTRESULT = string.Empty;
                string Old_TESTRESULT = string.Empty;

                string Judg = string.Empty;

                bool chkErr = true;
                string err = string.Empty;

                int i = 0;
                foreach (var row in gridLab.Items)
                {

                    P_WEAVELOT = string.Empty;
                    P_ITMCODE = string.Empty;
                    P_FINISHINGLOT = string.Empty;
                    P_TESTRESULT = string.Empty;
                    Old_TESTRESULT = string.Empty;
                    Judg = string.Empty;

                    if (((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).WEAVINGLOT != null)
                    {
                        P_WEAVELOT = ((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).WEAVINGLOT;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).ITM_CODE != null)
                    {
                        P_ITMCODE = ((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).ITM_CODE;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).FINISHINGLOT != null)
                    {
                        P_FINISHINGLOT = ((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).FINISHINGLOT;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).Judgment != null)
                    {
                        Judg = ((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).Judgment;

                        if (Judg != "Null")
                            P_TESTRESULT = Judg;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).TESTRESULT != null)
                    {
                        Old_TESTRESULT = ((LuckyTex.Models.LAB_SEARCHLABMASSPRO)((gridLab.Items)[i])).TESTRESULT;
                    }

                    if (string.IsNullOrEmpty(Old_TESTRESULT) && !string.IsNullOrEmpty(P_TESTRESULT))
                    {
                        if (LABDataService.Instance.LAB_SAVELABMASSPRORESULT(P_WEAVELOT, P_ITMCODE, P_FINISHINGLOT, P_TESTRESULT) == false)
                        {
                            chkErr = false;
                            err = "Can't save Weaving Lot = " + P_WEAVELOT + " Please check data";
                            err.ShowMessageBox(true);
                            break;
                        }
                    }

                    i++;
                }

                return chkErr;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
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
