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
    /// Interaction logic for DefectCodePage.xaml
    /// </summary>
    public partial class DefectCodePage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public DefectCodePage()
        {
            InitializeComponent();
            LoadProcessList();
        }

        #endregion

        #region Internal Variables

        private DefectCodeSession _session = new DefectCodeSession();
        string opera = string.Empty;

        string DEFECTCODE = string.Empty;
        string PROCESSID = string.Empty;
        string DESCRIPTION_TH = string.Empty;
        string DESCRIPTION_EN = string.Empty;
        decimal? POINT = null;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();
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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDEFECTID.Text))
            {
                string P_DEFECTID = string.Empty;
                string P_PROCESSID = string.Empty;
                string P_THAIDESC = string.Empty;
                string P_ENGDESC = string.Empty;
                decimal? P_POINT = null;

                P_DEFECTID = txtDEFECTID.Text;

                if (cbPROCESSID.SelectedValue != null)
                    P_PROCESSID = cbPROCESSID.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(txtTHAIDESC.Text))
                    P_THAIDESC = txtTHAIDESC.Text;

                if (!string.IsNullOrEmpty(txtENGDESC.Text))
                    P_ENGDESC = txtENGDESC.Text;

                if (chkPOINT.IsChecked == true)
                    P_POINT = 1;
                else
                    P_POINT = null;

                Save(P_DEFECTID, P_PROCESSID, P_THAIDESC, P_ENGDESC, P_POINT);
            }
        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DEFECTCODE))
            {
                Delete(DEFECTCODE);
            }
            else
            {
                "Please select Defect Code".ShowMessageBox();
            }
        }

        #endregion

        #region Controls Handlers

        private void txtDEFECTID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDEFECTID.Text != "")
                {
                    txtTHAIDESC.SelectAll();
                    txtTHAIDESC.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtTHAIDESC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtTHAIDESC.Text != "")
                {
                    txtENGDESC.SelectAll();
                    txtENGDESC.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        private void txtENGDESC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtENGDESC.Text != "")
                {
                    cmdSave.Focus();
                    e.Handled = true;
                }

                e.Handled = true;
            }
        }

        #endregion

        #region gridDefect_SelectedCellsChanged

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

        private void gridDefect_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridDefect.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridDefect);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            DEFECTCODE = string.Empty;
                            PROCESSID = string.Empty;
                            DESCRIPTION_TH = string.Empty;
                            DESCRIPTION_EN = string.Empty;
                            POINT = null;

                            if (((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).DEFECTCODE != null)
                            {
                                DEFECTCODE = ((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).DEFECTCODE;
                            }

                            if (((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).PROCESSID != null)
                            {
                                PROCESSID = ((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).PROCESSID;
                            }

                            if (((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).DESCRIPTION_TH != null)
                            {
                                DESCRIPTION_TH = ((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).DESCRIPTION_TH;
                            }

                            if (((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).DESCRIPTION_EN != null)
                            {
                                DESCRIPTION_EN = ((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).DESCRIPTION_EN;
                            }

                            if (((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).POINT != null)
                            {
                                POINT = ((LuckyTex.Models.DEFECT_SEARCH)(gridDefect.CurrentCell.Item)).POINT;
                            }
                        }
                    }
                }
                else
                {
                    DEFECTCODE = string.Empty;
                    PROCESSID = string.Empty;
                    DESCRIPTION_TH = string.Empty;
                    DESCRIPTION_EN = string.Empty;
                    POINT = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region private Methods

        private void ClearControl()
        {
            txtDEFECTID.Text = "";
            chkAll.IsChecked = false;
            cbPROCESSID.SelectedItem = null;
            txtTHAIDESC.Text = "";
            txtENGDESC.Text = "";
            chkPOINT.IsChecked = true;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridDefect.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridDefect.SelectedItems.Clear();
            else
                this.gridDefect.SelectedItem = null;

            gridDefect.ItemsSource = null;

            DEFECTCODE = string.Empty;
            PROCESSID = string.Empty;
            DESCRIPTION_TH = string.Empty;
            DESCRIPTION_EN = string.Empty;
            POINT = null;

            txtDEFECTID.IsEnabled = true;

            txtDEFECTID.SelectAll();
            txtDEFECTID.Focus();
        }

        private void Search()
        {
            string P_DEFECTID = string.Empty;
            string P_PROCESSID = string.Empty;
            string P_THAIDESC = string.Empty;
            string P_ENGDESC = string.Empty;

            #region Search

            if (chkAll.IsChecked == false)
            {
                if (!string.IsNullOrEmpty(txtDEFECTID.Text))
                {
                    P_DEFECTID = "%" + txtDEFECTID.Text + "%";
                }
                else
                {
                    P_DEFECTID = null;
                }

                if (!string.IsNullOrEmpty(txtTHAIDESC.Text))
                {
                    P_THAIDESC = "%" + txtTHAIDESC.Text + "%";
                }
                else
                {
                    P_THAIDESC = null;
                }

                if (!string.IsNullOrEmpty(txtENGDESC.Text))
                {
                    P_ENGDESC = "%" + txtENGDESC.Text + "%";
                }
                else
                {
                    P_ENGDESC = null;
                }

                if (cbPROCESSID.SelectedValue != null)
                {
                    P_PROCESSID = cbPROCESSID.SelectedValue.ToString();
                }
                else
                {
                    P_PROCESSID = null;
                }
            }
            else
            {
                P_DEFECTID = null;
                P_THAIDESC = null;
                P_ENGDESC = null;
                P_PROCESSID = null;
            }

            #endregion

            List<DEFECT_SEARCH> results = null;

            results = DefectCodeService.Instance.DEFECT_SEARCH(P_DEFECTID, P_PROCESSID, P_THAIDESC, P_ENGDESC);

            if (results != null && results.Count > 0)
            {
                gridDefect.ItemsSource = results;
            }
            else
            {
                gridDefect.ItemsSource = null;
            }
        }

        private void Edit()
        {
            if (!string.IsNullOrEmpty(DEFECTCODE))
            {
                txtDEFECTID.Text = DEFECTCODE;

                txtDEFECTID.IsEnabled = false;
            }

            if (!string.IsNullOrEmpty(PROCESSID))
            {
                cbPROCESSID.SelectedValue = PROCESSID;
            }

            if (!string.IsNullOrEmpty(DESCRIPTION_TH))
            {
                txtTHAIDESC.Text = DESCRIPTION_TH;
            }

            if (!string.IsNullOrEmpty(DESCRIPTION_EN))
            {
                txtENGDESC.Text = DESCRIPTION_EN;
            }

            if (POINT != null)
            {
                if (POINT == 1)
                {
                    chkPOINT.IsChecked = true;
                }
                else
                {
                    chkPOINT.IsChecked = false;
                }
            }
            else
            {
                chkPOINT.IsChecked = false;
            }
        }

        private void Delete(string DEFECTCODE)
        {
            if (gridDefect.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Do you want to Delete", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (DefectCodeService.Instance.DEFECT_DELETE(DEFECTCODE) == true)
                    {
                        ClearControl();
                    }
                }
            }
        }

        private void Save(string P_DEFECTID, string P_PROCESSID, string P_THAIDESC, string P_ENGDESC, decimal? P_POINT)
        {
            string R_RESULT = string.Empty;

            R_RESULT = DefectCodeService.Instance.DEFECT_INSERTUPDATE(P_DEFECTID, P_PROCESSID, P_THAIDESC, P_ENGDESC, P_POINT);

            if (!string.IsNullOrEmpty(R_RESULT))
            {
                if (R_RESULT == "Y")
                {
                    "Save complete".ShowMessageBox();

                    ClearControl();
                }
                else
                    R_RESULT.ShowMessageBox(true);
            }
        }

        #endregion

        #region Private Methods

        #region LoadProcessList

        private void LoadProcessList()
        {
            try
            {
                List<MASTER_AIRBAGPROCESSLIST> items = _session.GetProcessList();

                this.cbPROCESSID.ItemsSource = items;
                this.cbPROCESSID.DisplayMemberPath = "PROCESSDESCRIPTION";
                this.cbPROCESSID.SelectedValuePath = "PROCESSID";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
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
