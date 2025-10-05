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
using System.Runtime.InteropServices;
using System.Threading;

using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using DataControl.ClassData;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WeavingWeftYarnWindow.xaml
    /// </summary>
    public partial class WeavingWeftYarnWindow : Window
    {
        #region Constructror

        /// <summary>
        /// Constructror.
        /// </summary>
        public WeavingWeftYarnWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        string beamroll = string.Empty;
        decimal? DEFFNO = null;
        string WEFTYARN = string.Empty;
        string opera = string.Empty;
        string P_MCNo = string.Empty;

        string gridPALLETNO = string.Empty;
        string gridCHLOTNO = string.Empty;
        string gridUSETYPE = string.Empty;
        string gridREMARK = string.Empty;

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            txtBeamRoll.Text = beamroll;

            if (DEFFNO != null)
                txtDoffNo.Text = DEFFNO.Value.ToString();

            txtWeftYarn.Text = WEFTYARN;
            txtPALLETNO.Text = WEFTYARN;

            txtMCNO.Text = P_MCNo;

            if (!string.IsNullOrEmpty(beamroll) && DEFFNO != null)
            {
                LoadData(beamroll, DEFFNO);
            }

            txtCHLOTNO.SelectAll();
            txtCHLOTNO.Focus();
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gridPALLETNO))
            {
                if (!string.IsNullOrEmpty(gridCHLOTNO))
                {
                    if (MessageBox.Show("Do you want to delete this Pallet No : " + gridPALLETNO + " , Ch Lot No : " + gridCHLOTNO, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        string P_BEAMLOT = string.Empty;
                        decimal? P_DOFFNO = null;

                        P_BEAMLOT = txtBeamRoll.Text;

                        try
                        {
                            if (!string.IsNullOrEmpty(txtDoffNo.Text))
                                P_DOFFNO = decimal.Parse(txtDoffNo.Text);
                        }
                        catch
                        {
                            P_DOFFNO = null;
                        }

                        Delete(P_BEAMLOT, P_DOFFNO, gridPALLETNO, gridCHLOTNO);
                    }
                }
                else
                {
                    "CH Lot No isn't Null".ShowMessageBox(true);
                }
            }
            else
            {
                "Pallet No isn't Null".ShowMessageBox(true);
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            txtPALLETNO.Text = string.Empty;
            txtCHLOTNO.Text = string.Empty;
            txtREMARK.Text = string.Empty;

            rbFull.IsChecked = true;
            rbHalf.IsChecked = false;

            //gridPALLETNO = string.Empty;
            //gridCHLOTNO = string.Empty;
            //gridUSETYPE = string.Empty;
            //gridREMARK = string.Empty;

            txtPALLETNO.SelectAll();
            txtPALLETNO.Focus();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #region TextBox

        private void txtPALLETNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCHLOTNO.SelectAll();
                txtCHLOTNO.Focus();
                e.Handled = true;
            }
        }

        private void txtCHLOTNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (Save() == true)
                {
                    txtCHLOTNO.SelectAll();
                    txtCHLOTNO.Focus();
                    e.Handled = true;
                }
                else
                {
                    txtREMARK.SelectAll();
                    txtREMARK.Focus();
                    e.Handled = true;
                }
            }
        }

        private void txtREMARK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
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

        #region gridWeaving_SelectedCellsChanged

        private void gridWeaving_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridWeaving.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridWeaving);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).PALLETNO != null)
                            {
                                gridPALLETNO = ((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).PALLETNO;
                            }
                            else
                            {
                                gridPALLETNO = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).CHLOTNO != null)
                            {
                                gridCHLOTNO = ((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).CHLOTNO;
                            }
                            else
                            {
                                gridCHLOTNO = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).USETYPE != null)
                            {
                                gridUSETYPE = ((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).USETYPE;
                            }
                            else
                            {
                                gridUSETYPE = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).REMARK != null)
                            {
                                gridREMARK = ((LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).REMARK;
                            }
                            else
                            {
                                gridREMARK = string.Empty;
                            }


                        }
                    }
                }
                else
                {
                    gridPALLETNO = string.Empty;
                    gridCHLOTNO = string.Empty;
                    gridUSETYPE = string.Empty;
                    gridREMARK = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Properties

        #region ClearControl

        private void ClearControl()
        {
            //txtBeamRoll.Text = string.Empty;
            //txtDoffNo.Text = string.Empty;
            //txtWeftYarn.Text = string.Empty;
            //txtPALLETNO.Text = string.Empty;
            txtCHLOTNO.Text = string.Empty;
            txtREMARK.Text = string.Empty;

            rbFull.IsChecked = true;
            rbHalf.IsChecked = false;

            gridPALLETNO = string.Empty;
            gridCHLOTNO = string.Empty;
            gridUSETYPE = string.Empty;
            gridREMARK = string.Empty;

            if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWeaving.SelectedItems.Clear();
            else
                this.gridWeaving.SelectedItem = null;

            gridWeaving.ItemsSource = null;

            txtCHLOTNO.SelectAll();
            txtCHLOTNO.Focus();
        }

        #endregion

        #region Edit

        private void Edit()
        {
            txtPALLETNO.Text = gridPALLETNO;
            txtCHLOTNO.Text = gridCHLOTNO;
            txtREMARK.Text = gridREMARK;

            if (gridUSETYPE == "1")
            {
                rbFull.IsChecked = true;
                rbHalf.IsChecked = false;
            }
            else
            {
                rbFull.IsChecked = false;
                rbHalf.IsChecked = true;
            }

            txtPALLETNO.SelectAll();
            txtPALLETNO.Focus();
        }

        #endregion

        #region Delete

        private void Delete(string P_BEAMLOT, decimal? P_DOFFNO, string P_PALLETNO, string P_CHLOTNO)
        {
            try
            {
                if (WeavingDataService.Instance.WEAVE_DELETEWEFTYARN(P_BEAMLOT, P_DOFFNO, P_PALLETNO, P_CHLOTNO) == true)
                {
                    ClearControl();
                    LoadData(P_BEAMLOT, P_DOFFNO);
                }
                else
                {
                    "Can't Delete".ShowMessageBox(true);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadData

        private void LoadData(string P_BEAMROLL, decimal? P_DOFFNO)
        {
            List<WEAV_GETWEFTYARNLISTBYDOFFNO> results = null;
            results = WeavingDataService.Instance.WEAV_GETWEFTYARNLISTBYDOFFNO(P_BEAMROLL, P_DOFFNO);

            if (results != null && results.Count > 0)
            {
                List<LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO> dataList = new List<LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO dataItemNew = new LuckyTex.Models.WEAV_GETWEFTYARNLISTBYDOFFNO();

                    dataItemNew.BEAMLOT = results[i].BEAMLOT;
                    dataItemNew.DOFFNO = results[i].DOFFNO;
                    dataItemNew.PALLETNO = results[i].PALLETNO;
                    dataItemNew.CHLOTNO = results[i].CHLOTNO;
                    dataItemNew.ADDDATE = results[i].ADDDATE;
                    dataItemNew.ADDBY = results[i].ADDBY;
                    dataItemNew.USETYPE = results[i].USETYPE;
                    
                    if(!string.IsNullOrEmpty(dataItemNew.USETYPE))
                    {
                        if(dataItemNew.USETYPE == "1")
                        {
                            dataItemNew.USETYPENAME = "Full";
                        }
                        else if (dataItemNew.USETYPE == "2")
                        {
                            dataItemNew.USETYPENAME = "Half";
                        }
                    }

                    dataItemNew.REMARK = results[i].REMARK;

                    dataList.Add(dataItemNew);
                    i++;
                }

                this.gridWeaving.ItemsSource = dataList;
            }
            else
            {
                // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
                if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                    this.gridWeaving.SelectedItems.Clear();
                else
                    this.gridWeaving.SelectedItem = null;

                gridWeaving.ItemsSource = null;

            }
        }

        #endregion

        #region SaveWEAVE_INSERTUPDATEWEFTYARN

        private bool SaveWEAVE_INSERTUPDATEWEFTYARN(string P_BEAMLOT, decimal? P_DOFFNO, string P_PALLETNO,
            string P_CHLOTNO, DateTime? P_ADDDATE, string P_ADDBY, string P_USETYPE, string P_REMARK, string P_MCNO)
        {
            try
            {
                string result = string.Empty;

                result = WeavingDataService.Instance.WEAVE_INSERTUPDATEWEFTYARN(P_BEAMLOT, P_DOFFNO, P_PALLETNO
                    , P_CHLOTNO, P_ADDDATE, P_ADDBY, P_USETYPE, P_REMARK, P_MCNO);

                if (string.IsNullOrEmpty(result))
                {
                    return true;
                }
                else
                {
                    result.ShowMessageBox(true);

                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);

                return false;
            }
        }

        #endregion

        private bool Save()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPALLETNO.Text))
                {
                    if (!string.IsNullOrEmpty(txtCHLOTNO.Text))
                    {
                        string P_BEAMLOT = string.Empty;
                        decimal? P_DOFFNO = null;
                        string P_PALLETNO = string.Empty;
                        string P_CHLOTNO = string.Empty;
                        DateTime? P_ADDDATE = null;
                        string P_ADDBY = string.Empty;
                        string P_USETYPE = string.Empty;
                        string P_REMARK = string.Empty;
                        string P_MCNO = string.Empty;

                        P_BEAMLOT = txtBeamRoll.Text;

                        try
                        {
                            if (!string.IsNullOrEmpty(txtDoffNo.Text))
                                P_DOFFNO = decimal.Parse(txtDoffNo.Text);
                        }
                        catch
                        {
                            P_DOFFNO = null;
                        }

                        P_PALLETNO = txtPALLETNO.Text;
                        P_CHLOTNO = txtCHLOTNO.Text;

                        P_ADDDATE = DateTime.Now;
                        P_ADDBY = opera;

                        if (rbFull.IsChecked == true && rbHalf.IsChecked == false)
                        {
                            P_USETYPE = "1";
                        }
                        else if (rbFull.IsChecked == false && rbHalf.IsChecked == true)
                        {
                            P_USETYPE = "2";
                        }

                        P_REMARK = txtREMARK.Text;

                        P_MCNO = txtMCNO.Text;

                        if (SaveWEAVE_INSERTUPDATEWEFTYARN(P_BEAMLOT, P_DOFFNO, P_PALLETNO, P_CHLOTNO, P_ADDDATE, P_ADDBY, P_USETYPE, P_REMARK, P_MCNO) == true)
                        {
                            ClearControl();
                            LoadData(P_BEAMLOT, P_DOFFNO);

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        "CH Lot No isn't Null".ShowMessageBox(true);
                        return false;
                    }
                }
                else
                {
                    "Pallet No isn't Null".ShowMessageBox(true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        #region Public Properties

        public void Setup(string BeamerRoll, decimal? DeffNo, string WeftYarn, string user,string mcNo)
        {
            opera = user;
            beamroll = BeamerRoll;
            DEFFNO = DeffNo;
            WEFTYARN = WeftYarn;
            P_MCNo = mcNo;
        }

        #endregion

    }
}
