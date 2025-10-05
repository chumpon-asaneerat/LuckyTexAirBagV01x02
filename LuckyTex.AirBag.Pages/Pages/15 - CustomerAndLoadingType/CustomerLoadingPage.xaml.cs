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

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for CustomerLoadingPage.xaml
    /// </summary>
    public partial class CustomerLoadingPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CustomerLoadingPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        string opera = string.Empty;
        string CUSTOMERTYPE = string.Empty;
        string LOADINGTYPE = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            MASTER_CUSTOMERTYPELIST();

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

        #region cmdAddNew_Click

        private void cmdAddNew_Click(object sender, RoutedEventArgs e)
        {
            txtCUSTOMERTYPE.IsEnabled = true;
            txtLOADINGTYPE.IsEnabled = true;

            txtCUSTOMERTYPE.Text = "";
            txtLOADINGTYPE.Text = "";
        }

        #endregion

        #region cmdEdit_Click

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            txtCUSTOMERTYPE.Text = CUSTOMERTYPE;
            txtCUSTOMERTYPE.IsEnabled = true;
            txtLOADINGTYPE.IsEnabled = true;
        }

        #endregion

        #region cmdDeleteCustomerType_Click

        private void cmdDeleteCustomerType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CUSTOMERTYPE))
            {
                if (MessageBox.Show("Do you want to delete " + CUSTOMERTYPE, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DeleteCUSTYPE(CUSTOMERTYPE);
                }
            }
        }

        #endregion

        #region cmdDeleteLoadingType_Click

        private void cmdDeleteLoadingType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(LOADINGTYPE))
            {
                if (MessageBox.Show("Do you want to delete " + LOADINGTYPE, "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    DeleteLOADTYPE(LOADINGTYPE);
                }
            }
        }

        #endregion

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
            MASTER_CUSTOMERTYPELIST();
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCUSTOMERTYPE.Text))
            {
                if (!string.IsNullOrEmpty(txtLOADINGTYPE.Text))
                {
                    MASTER_EDITCUSLOADTYPE(txtCUSTOMERTYPE.Text, txtLOADINGTYPE.Text, txtOperator.Text);
                }
                else
                {
                    if (MessageBox.Show("Want to Save this Customer Type With No Type Loading ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        MASTER_EDITCUS(txtCUSTOMERTYPE.Text, txtOperator.Text);
                    }
                }
            }
            else
                "Customer Type isn't Null".ShowMessageBox(true);
        }

        #endregion

        #endregion

        #region txtCUSTOMERTYPE_KeyDown

        private void txtCUSTOMERTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLOADINGTYPE.Focus();
                txtLOADINGTYPE.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLOADINGTYPE_KeyDown

        private void txtLOADINGTYPE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdAddNew.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region chkAllLoadingType_Checked

        private void chkAllLoadingType_Checked(object sender, RoutedEventArgs e)
        {
            MASTER_GETLOADINGBYCUSTYPE("");

            cmdDeleteLoadingType.IsEnabled = true;
        }

        #endregion

        #region chkAllLoadingType_Unchecked

        private void chkAllLoadingType_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CUSTOMERTYPE))
                MASTER_GETLOADINGBYCUSTYPE(CUSTOMERTYPE);

            cmdDeleteLoadingType.IsEnabled = false;
        }

        #endregion

        #region GetDataGridRows

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

        #endregion

        #region gridMASTER_CUSTOMERTYPELIST_SelectedCellsChanged

        private void gridMASTER_CUSTOMERTYPELIST_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridMASTER_CUSTOMERTYPELIST.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridMASTER_CUSTOMERTYPELIST);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (chkAllLoadingType.IsChecked == false)
                            {
                                if (!string.IsNullOrEmpty(((LuckyTex.Models.MASTER_CUSTOMERTYPELIST)(gridMASTER_CUSTOMERTYPELIST.CurrentCell.Item)).CUSTOMERTYPE))
                                {
                                    CUSTOMERTYPE = ((LuckyTex.Models.MASTER_CUSTOMERTYPELIST)(gridMASTER_CUSTOMERTYPELIST.CurrentCell.Item)).CUSTOMERTYPE;
                                    MASTER_GETLOADINGBYCUSTYPE(CUSTOMERTYPE);
                                }
                                else
                                {
                                    CUSTOMERTYPE = string.Empty;

                                    if (this.gridMASTER_GETLOADINGBYCUSTYPE.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                                        this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItems.Clear();
                                    else
                                        this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItem = null;

                                    gridMASTER_GETLOADINGBYCUSTYPE.ItemsSource = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    CUSTOMERTYPE = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region gridMASTER_GETLOADINGBYCUSTYPE_SelectedCellsChanged

        private void gridMASTER_GETLOADINGBYCUSTYPE_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridMASTER_GETLOADINGBYCUSTYPE.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridMASTER_GETLOADINGBYCUSTYPE);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (chkAllLoadingType.IsChecked == true)
                            {
                                if (!string.IsNullOrEmpty(((LuckyTex.Models.MASTER_GETLOADINGBYCUSTYPE)(gridMASTER_GETLOADINGBYCUSTYPE.CurrentCell.Item)).LOADINGTYPE))
                                {
                                    LOADINGTYPE = ((LuckyTex.Models.MASTER_GETLOADINGBYCUSTYPE)(gridMASTER_GETLOADINGBYCUSTYPE.CurrentCell.Item)).LOADINGTYPE;
                                }
                                else
                                {
                                    LOADINGTYPE = string.Empty;
                                }
                            }
                            else
                            {
                                LOADINGTYPE = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    LOADINGTYPE = string.Empty;
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
            txtCUSTOMERTYPE.Text = "";
            txtLOADINGTYPE.Text = "";
            txtCUSTOMERTYPE.IsEnabled = false;
            txtLOADINGTYPE.IsEnabled = false;

            chkAllLoadingType.IsChecked = false;
            cmdDeleteLoadingType.IsEnabled = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridMASTER_CUSTOMERTYPELIST.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridMASTER_CUSTOMERTYPELIST.SelectedItems.Clear();
            else
                this.gridMASTER_CUSTOMERTYPELIST.SelectedItem = null;

            gridMASTER_CUSTOMERTYPELIST.ItemsSource = null;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridMASTER_GETLOADINGBYCUSTYPE.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItems.Clear();
            else
                this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItem = null;

            gridMASTER_GETLOADINGBYCUSTYPE.ItemsSource = null;

            CUSTOMERTYPE = string.Empty;
            LOADINGTYPE = string.Empty;
        }

        #endregion

        #region MASTER_CUSTOMERTYPELIST

        private void MASTER_CUSTOMERTYPELIST()
        {
            List<MASTER_CUSTOMERTYPELIST> result = new List<Models.MASTER_CUSTOMERTYPELIST>();
            result = CustomerAndLoadingTypeDataService.Instance.MASTER_CUSTOMERTYPELIST();

            if (result.Count > 0)
            {
               List<LuckyTex.Models.MASTER_CUSTOMERTYPELIST> dataList = new List<LuckyTex.Models.MASTER_CUSTOMERTYPELIST>();
                int i = 0;

                foreach (var row in result)
                {
                    LuckyTex.Models.MASTER_CUSTOMERTYPELIST dataItemNew = new LuckyTex.Models.MASTER_CUSTOMERTYPELIST();

                    dataItemNew.CUSTOMERTYPE = result[i].CUSTOMERTYPE;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridMASTER_CUSTOMERTYPELIST.ItemsSource = dataList;

            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridMASTER_CUSTOMERTYPELIST.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridMASTER_CUSTOMERTYPELIST.SelectedItems.Clear();
                else
                    this.gridMASTER_CUSTOMERTYPELIST.SelectedItem = null;

                gridMASTER_CUSTOMERTYPELIST.ItemsSource = null;

                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridMASTER_GETLOADINGBYCUSTYPE.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItems.Clear();
                else
                    this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItem = null;

                gridMASTER_GETLOADINGBYCUSTYPE.ItemsSource = null;
            }
        }

        #endregion

        #region MASTER_GETLOADINGBYCUSTYPE

        private void MASTER_GETLOADINGBYCUSTYPE(string P_CUSTYPE)
        {
            List<MASTER_GETLOADINGBYCUSTYPE> result = new List<Models.MASTER_GETLOADINGBYCUSTYPE>();
            result = CustomerAndLoadingTypeDataService.Instance.MASTER_GETLOADINGBYCUSTYPE(P_CUSTYPE);

            if (result.Count > 0)
            {
                List<LuckyTex.Models.MASTER_GETLOADINGBYCUSTYPE> dataList = new List<LuckyTex.Models.MASTER_GETLOADINGBYCUSTYPE>();
                int i = 0;

                foreach (var row in result)
                {
                    LuckyTex.Models.MASTER_GETLOADINGBYCUSTYPE dataItemNew = new LuckyTex.Models.MASTER_GETLOADINGBYCUSTYPE();

                    dataItemNew.LOADINGTYPE = result[i].LOADINGTYPE;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridMASTER_GETLOADINGBYCUSTYPE.ItemsSource = dataList;

            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridMASTER_GETLOADINGBYCUSTYPE.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItems.Clear();
                else
                    this.gridMASTER_GETLOADINGBYCUSTYPE.SelectedItem = null;

                gridMASTER_GETLOADINGBYCUSTYPE.ItemsSource = null;
            }
        }

        #endregion

        #region MASTER_EDITCUSLOADTYPE

        private void MASTER_EDITCUSLOADTYPE(string P_CUSTYPE, string P_LOADTYPE, string P_OPERATOR)
        { 
             string result = string.Empty;

             result = CustomerAndLoadingTypeDataService.Instance.MASTER_EDITCUSLOADTYPE(P_CUSTYPE, P_LOADTYPE, P_OPERATOR);

             if (string.IsNullOrEmpty(result))
             {
                 "Save Complete".ShowMessageBox(false);
                 ClearControl();
                 MASTER_CUSTOMERTYPELIST();
             }
             else
                 result.ShowMessageBox(true);
        }

        #endregion

        #region MASTER_EDITCUS

        private void MASTER_EDITCUS(string P_CUSTYPE, string P_OPERATOR)
        {
            string result = string.Empty;

            result = CustomerAndLoadingTypeDataService.Instance.MASTER_EDITCUS(P_CUSTYPE, P_OPERATOR);

            if (string.IsNullOrEmpty(result))
            {
                "Save Complete".ShowMessageBox(false);
                ClearControl();
                MASTER_CUSTOMERTYPELIST();
            }
            else
                result.ShowMessageBox(true);
        }

        #endregion

        #region DeleteCUSTYPE

        private void DeleteCUSTYPE(string P_CUSTYPE)
        {

            if (CustomerAndLoadingTypeDataService.Instance.MASTER_DELETECUS(P_CUSTYPE) == true)
            {
                "Delete Complete".ShowMessageBox(false);

                MASTER_CUSTOMERTYPELIST();
            }
            else
                "Can't Delete".ShowMessageBox(true);
        }

        #endregion

        #region DeleteLOADTYPE

        private void DeleteLOADTYPE(string P_LOADTYPE)
        {

            if (CustomerAndLoadingTypeDataService.Instance.MASTER_DELETELOADTYPE(P_LOADTYPE) == true)
            {
                "Delete Complete".ShowMessageBox(false);

                MASTER_GETLOADINGBYCUSTYPE("");
            }
            else
                "Can't Delete".ShowMessageBox(true);
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
