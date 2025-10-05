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
    /// Interaction logic for GreigePage.xaml
    /// </summary>
    public partial class GreigePage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public GreigePage()
        {
            InitializeComponent();
            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            LoadItemGood();
            LoadLabResult();

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

        private void txtBEAMROLL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtBEAMROLL.Text != "")
                {
                    txtLOOMNO.SelectAll();
                    txtLOOMNO.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtLOOMNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtLOOMNO.Text != "")
                {
                    dteSettingDate.Focus();
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
                            if (!string.IsNullOrEmpty(((LuckyTex.Models.LAB_SEARCHLABGREIGE)(gridLab.SelectedItem)).BEAMERROLL)
                                && string.IsNullOrEmpty(((LuckyTex.Models.LAB_SEARCHLABGREIGE)(gridLab.SelectedItem)).TESTRESULT))
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

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                List<WEAV_GETALLITEMWEAVING> items = _session.Weav_getAllItemWeaving();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_WEAVING";
                this.cbItemCode.SelectedValuePath = "ITM_WEAVING";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
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

        #region ClearControl

        private void ClearControl()
        {
            txtBEAMROLL.Text = "";
            txtLOOMNO.Text = "";
            cbItemCode.SelectedValue = null;
            cbLabResult.SelectedValue = null;

            dteSendDate.SelectedDate = null;
            dteSettingDate.SelectedDate = null;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridLab.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridLab.SelectedItems.Clear();
            else
                this.gridLab.SelectedItem = null;

            gridLab.ItemsSource = null;

          
            txtBEAMROLL.SelectAll();
            txtBEAMROLL.Focus();
        }

        #endregion

        #region Search

        private void Search()
        {
            string P_BEAMERROLL = string.Empty;
            string P_LOOM = string.Empty;
            string P_ITMWEAVE = string.Empty;
            string P_TESTRESULT = string.Empty;
            string P_SETTINGDATE = string.Empty;
            string P_SENDDATE = string.Empty;

            #region Search

            if (!string.IsNullOrEmpty(txtBEAMROLL.Text))
            {
                P_BEAMERROLL = txtBEAMROLL.Text;
            }
            else
            {
                P_BEAMERROLL = null;
            }

            if (!string.IsNullOrEmpty(txtLOOMNO.Text))
            {
                P_LOOM = txtLOOMNO.Text;
            }
            else
            {
                P_LOOM = null;
            }

            if (cbItemCode.SelectedValue != null)
            {
                P_ITMWEAVE = cbItemCode.SelectedValue.ToString();
            }
            else
            {
                P_ITMWEAVE = null;
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

            if (dteSettingDate.SelectedDate != null)
            {
                P_SETTINGDATE = dteSettingDate.SelectedDate.Value.ToString("dd/MM/yyyy");
            }
            else
            {
                P_SETTINGDATE = null;
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

            List<LAB_SEARCHLABGREIGE> results = null;

            results = LABDataService.Instance.LAB_SEARCHLABGREIGE(P_BEAMERROLL, P_LOOM, P_ITMWEAVE ,P_SETTINGDATE,P_SENDDATE, P_TESTRESULT);

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
                string P_BEAMERROLL = string.Empty;
                string P_LOOM = string.Empty;
                string P_ITMWEAVE = string.Empty;
                string P_TESTRESULT = string.Empty;
                string Old_TESTRESULT = string.Empty;
                decimal? P_TESTNO = null;

                string Judg = string.Empty;

                bool chkErr = true;
                string err = string.Empty;

                int i = 0;
                foreach (var row in gridLab.Items)
                {
                    P_BEAMERROLL = string.Empty;
                    P_LOOM = string.Empty;
                    P_ITMWEAVE = string.Empty;
                    P_TESTRESULT = string.Empty;
                    Old_TESTRESULT = string.Empty;
                    P_TESTNO = null;

                    Judg = string.Empty;

                    if (((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).BEAMERROLL != null)
                    {
                        P_BEAMERROLL = ((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).BEAMERROLL;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).LOOMNO != null)
                    {
                        P_LOOM = ((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).LOOMNO;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).ITM_WEAVING != null)
                    {
                        P_ITMWEAVE = ((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).ITM_WEAVING;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).Judgment != null)
                    {
                        Judg = ((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).Judgment;

                        if (Judg != "Null")
                            P_TESTRESULT = Judg;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).TESTRESULT != null)
                    {
                        Old_TESTRESULT = ((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).TESTRESULT;
                    }

                    if (((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).TESTNO != null)
                    {
                        P_TESTNO = ((LuckyTex.Models.LAB_SEARCHLABGREIGE)((gridLab.Items)[i])).TESTNO;
                    }

                    if (string.IsNullOrEmpty(Old_TESTRESULT) && !string.IsNullOrEmpty(P_TESTRESULT))
                    {
                        if (LABDataService.Instance.LAB_SAVELABGREIGERESULT(P_BEAMERROLL, P_LOOM, P_ITMWEAVE, P_TESTRESULT, P_TESTNO) == false)
                        {
                            chkErr = false;
                            err = "Can't save Beamer Roll = " + P_BEAMERROLL + " Please check data";
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
