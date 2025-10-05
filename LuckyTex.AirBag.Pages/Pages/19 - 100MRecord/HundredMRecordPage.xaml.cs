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
    /// Interaction logic for HundredMRecordPage.xaml
    /// </summary>
    public partial class HundredMRecordPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public HundredMRecordPage()
        {
            InitializeComponent();
            LoadItemGood();
        }

        #endregion

        #region Internal Variables

        private HundredMDataSession _session = new HundredMDataSession();
        string opera = string.Empty;

        string gidITM_CODE = string.Empty;

        bool gidDENSITY_W = false;
        bool gidDENSITY_F = false;
        bool gidTRIM_L = false;
        bool gidTRIM_R = false;
        bool gidFLOPPY_L = false;
        bool gidFLOPPY_R = false;
        bool gidWIDTH_ALL = false;
        bool gidWIDTH_PIN = false;
        bool gidWIDTH_COAT = false;
        bool gidHARDNESS_L = false;
        bool gidHARDNESS_C = false;
        bool gidHARDNESS_R = false;
        bool gidUNWINDER = false;
        bool gidWINDER = false;

       bool  gidWIDTH_SelvageL = false;
       bool gidWIDTH_SelvageR = false;

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

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbItemGoods.SelectedValue != null)
            {
                if (chkDENSITY_W.IsEnabled == true)
                {
                    Save();
                }
                else
                {
                    "Please Add Or Edit Data".ShowMessageBox();
                }
            }
            else
            {
                "Please select Item Code".ShowMessageBox();
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

        #endregion

        #region gridItem_SelectedCellsChanged

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

        private void gridHundredM_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridHundredM.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridHundredM);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            gidITM_CODE = string.Empty;
                            gidDENSITY_W = false;
                            gidDENSITY_F = false;
                            gidTRIM_L = false;
                            gidTRIM_R = false;
                            gidFLOPPY_L = false;
                            gidFLOPPY_R = false;
                            gidWIDTH_ALL = false;
                            gidWIDTH_PIN = false;
                            gidWIDTH_COAT = false;

                            gidWIDTH_SelvageL = false;
                            gidWIDTH_SelvageR = false;

                            gidHARDNESS_L = false;
                            gidHARDNESS_C = false;
                            gidHARDNESS_R = false;
                            gidUNWINDER = false;
                            gidWINDER = false;

                            #region gidITM_CODE

                            if (((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ITM_CODE != null)
                            {
                                gidITM_CODE = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ITM_CODE;
                            }

                            #endregion

                            gidDENSITY_W = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkDENSITY_W;
                            gidDENSITY_F = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkDENSITY_F;
                            gidTRIM_L = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkTRIM_L;
                            gidTRIM_R = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkTRIM_R;
                            gidFLOPPY_L = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkFLOPPY_L;
                            gidFLOPPY_R = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkFLOPPY_R;
                            gidWIDTH_ALL = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkWIDTH_ALL;
                            gidWIDTH_PIN = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkWIDTH_PIN;
                            gidWIDTH_COAT = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkWIDTH_COAT;
                            gidHARDNESS_L = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkHARDNESS_L;
                            gidHARDNESS_C = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkHARDNESS_C;
                            gidHARDNESS_R = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkHARDNESS_R;
                            gidUNWINDER = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkUNWINDER;
                            gidWINDER = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkWINDER;


                            gidWIDTH_SelvageL = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkWIDTH_SelvageL;
                            gidWIDTH_SelvageR = ((LuckyTex.Models.GETINSPECTIONLISTTESTBYITMCODE)(gridHundredM.CurrentCell.Item)).ChkWIDTH_SelvageR;
                           

                            #region gidITM_CODE

                            if (!string.IsNullOrEmpty(gidITM_CODE))
                            {
                                cbItemGoods.SelectedValue = gidITM_CODE;
                            }

                            #endregion

                            #region gidDENSITY_W

                            if (gidDENSITY_W == true)
                                chkDENSITY_W.IsChecked = true;
                            else
                                chkDENSITY_W.IsChecked = false;

                            #endregion

                            #region gidDENSITY_F

                            if (gidDENSITY_F == true)
                                chkDENSITY_F.IsChecked = true;
                            else
                                chkDENSITY_F.IsChecked = false;

                            #endregion

                            #region gidTRIM_L

                            if (gidTRIM_L == true)
                                chkTRIM_L.IsChecked = true;
                            else
                                chkTRIM_L.IsChecked = false;

                            #endregion

                            #region gidTRIM_R

                            if (gidTRIM_R == true)
                                chkTRIM_R.IsChecked = true;
                            else
                                chkTRIM_R.IsChecked = false;

                            #endregion

                            #region gidFLOPPY_L

                            if (gidFLOPPY_L == true)
                                chkFLOPPY_L.IsChecked = true;
                            else
                                chkFLOPPY_L.IsChecked = false;

                            #endregion

                            #region gidFLOPPY_R

                            if (gidFLOPPY_R == true)
                                chkFLOPPY_R.IsChecked = true;
                            else
                                chkFLOPPY_R.IsChecked = false;

                            #endregion

                            #region gidWIDTH_ALL

                            if (gidWIDTH_ALL == true)
                                chkWIDTH_ALL.IsChecked = true;
                            else
                                chkWIDTH_ALL.IsChecked = false;

                            #endregion

                            #region gidWIDTH_PIN

                            if (gidWIDTH_PIN == true)
                                chkWIDTH_PIN.IsChecked = true;
                            else
                                chkWIDTH_PIN.IsChecked = false;

                            #endregion

                            #region gidWIDTH_COAT

                            if (gidWIDTH_COAT == true)
                                chkWIDTH_COAT.IsChecked = true;
                            else
                                chkWIDTH_COAT.IsChecked = false;

                            #endregion

                            #region gidHARDNESS_L

                            if (gidHARDNESS_L == true)
                                chkHARDNESS_L.IsChecked = true;
                            else
                                chkHARDNESS_L.IsChecked = false;

                            #endregion

                            #region gidHARDNESS_C

                            if (gidHARDNESS_C == true)
                                chkHARDNESS_C.IsChecked = true;
                            else
                                chkHARDNESS_C.IsChecked = false;

                            #endregion

                            #region gidHARDNESS_R

                            if (gidHARDNESS_R == true)
                                chkHARDNESS_R.IsChecked = true;
                            else
                                chkHARDNESS_R.IsChecked = false;

                            #endregion

                            #region gidUNWINDER

                            if (gidUNWINDER == true)
                                chkUNWINDER.IsChecked = true;
                            else
                                chkUNWINDER.IsChecked = false;

                            #endregion

                            #region gidWINDER

                            if (gidWINDER == true)
                                chkWINDER.IsChecked = true;
                            else
                                chkWINDER.IsChecked = false;

                            #endregion

                            #region gidWIDTH_SelvageL

                            if (gidWIDTH_SelvageL == true)
                                chkWIDTH_Selvage_L.IsChecked = true;
                            else
                                chkWIDTH_Selvage_L.IsChecked = false;

                            #endregion

                            #region gidWIDTH_SelvageR

                            if (gidWIDTH_SelvageR == true)
                                chkWIDTH_Selvage_R.IsChecked = true;
                            else
                                chkWIDTH_Selvage_R.IsChecked = false;

                            #endregion

                            EnabledCon(false);
                        }
                    }
                }
                else
                {
                    gidITM_CODE = string.Empty;
                    gidDENSITY_W = false;
                    gidDENSITY_F = false;
                    gidTRIM_L = false;
                    gidTRIM_R = false;
                    gidFLOPPY_L = false;
                    gidFLOPPY_R = false;
                    gidWIDTH_ALL = false;
                    gidWIDTH_PIN = false;
                    gidWIDTH_COAT = false;
                    gidHARDNESS_L = false;
                    gidHARDNESS_C = false;
                    gidHARDNESS_R = false;
                    gidUNWINDER = false;
                    gidWINDER = false;
                    gidWIDTH_SelvageL = false;
                    gidWIDTH_SelvageR = false;
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
                List<ITM_GETITEMCODELIST> items = _session.GetItemCodeData();

                this.cbItemGoods.ItemsSource = items;
                this.cbItemGoods.DisplayMemberPath = "ITM_CODE";
                this.cbItemGoods.SelectedValuePath = "ITM_CODE";
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
            cbItemGoods.SelectedValue = null;

            chkAll.IsChecked = false;

            chkDENSITY_W.IsChecked = false;
            chkDENSITY_F.IsChecked = false;
            chkTRIM_L.IsChecked = false;
            chkTRIM_R.IsChecked = false;
            chkFLOPPY_L.IsChecked = false;
            chkFLOPPY_R.IsChecked = false;
            chkWIDTH_ALL.IsChecked = false;
            chkWIDTH_PIN.IsChecked = false;
            chkWIDTH_COAT.IsChecked = false;
            chkWIDTH_Selvage_L.IsChecked = false;
            chkWIDTH_Selvage_R.IsChecked = false;

            chkHARDNESS_L.IsChecked = false;
            chkHARDNESS_C.IsChecked = false;
            chkHARDNESS_R.IsChecked = false;
            chkUNWINDER.IsChecked = false;
            chkWINDER.IsChecked = false;

            gidITM_CODE = string.Empty;
            gidDENSITY_W = false;
            gidDENSITY_F = false;
            gidTRIM_L = false;
            gidTRIM_R = false;
            gidFLOPPY_L = false;
            gidFLOPPY_R = false;
            gidWIDTH_ALL = false;
            gidWIDTH_PIN = false;
            gidWIDTH_COAT = false;
            gidHARDNESS_L = false;
            gidHARDNESS_C = false;
            gidHARDNESS_R = false;
            gidUNWINDER = false;
            gidWINDER = false;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridHundredM.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridHundredM.SelectedItems.Clear();
            else
                this.gridHundredM.SelectedItem = null;

            gridHundredM.ItemsSource = null;

          
            EnabledCon(true);
        }

        #endregion

        #region Search

        private void Search()
        {
            string P_ITEMCODE = string.Empty;
           
            #region Search

            if (chkAll.IsChecked == false)
            {
                if (cbItemGoods.SelectedValue != null)
                    P_ITEMCODE = cbItemGoods.SelectedValue.ToString();
            }

            #endregion

            List<GETINSPECTIONLISTTESTBYITMCODE> results = null;

            results = HundredMDataService.Instance.GETINSPECTIONLISTTESTBYITMCODE(P_ITEMCODE);

            if (results != null && results.Count > 0)
            {
                gridHundredM.ItemsSource = results;
            }
            else
            {
                gridHundredM.ItemsSource = null;
            }

            EnabledCon(false);

            chkDENSITY_W.IsChecked = false;
            chkDENSITY_F.IsChecked = false;
            chkTRIM_L.IsChecked = false;
            chkTRIM_R.IsChecked = false;
            chkFLOPPY_L.IsChecked = false;
            chkFLOPPY_R.IsChecked = false;
            chkWIDTH_ALL.IsChecked = false;
            chkWIDTH_PIN.IsChecked = false;
            chkWIDTH_COAT.IsChecked = false;
            chkWIDTH_Selvage_L.IsChecked = false;
            chkWIDTH_Selvage_R.IsChecked = false;

            chkHARDNESS_L.IsChecked = false;
            chkHARDNESS_C.IsChecked = false;
            chkHARDNESS_R.IsChecked = false;
            chkUNWINDER.IsChecked = false;
            chkWINDER.IsChecked = false;

            gidITM_CODE = string.Empty;
            gidDENSITY_W = false;
            gidDENSITY_F = false;
            gidTRIM_L = false;
            gidTRIM_R = false;
            gidFLOPPY_L = false;
            gidFLOPPY_R = false;
            gidWIDTH_ALL = false;
            gidWIDTH_PIN = false;
            gidWIDTH_COAT = false;
            gidHARDNESS_L = false;
            gidHARDNESS_C = false;
            gidHARDNESS_R = false;
            gidUNWINDER = false;
            gidWINDER = false;
        }

        #endregion

        #region Edit

        private void Edit()
        {
            EnabledCon(true);
        }

        #endregion

        #region EnabledCon

        private void EnabledCon(bool ena)
        {
            chkDENSITY_W.IsEnabled = ena;
            chkDENSITY_F.IsEnabled = ena;
            chkTRIM_L.IsEnabled = ena;
            chkTRIM_R.IsEnabled = ena;
            chkFLOPPY_L.IsEnabled = ena;
            chkFLOPPY_R.IsEnabled = ena;
            chkWIDTH_ALL.IsEnabled = ena;
            chkWIDTH_PIN.IsEnabled = ena;
            chkWIDTH_COAT.IsEnabled = ena;
            chkWIDTH_Selvage_L.IsEnabled = ena;
            chkWIDTH_Selvage_R.IsEnabled = ena;

            chkHARDNESS_L.IsEnabled = ena;
            chkHARDNESS_C.IsEnabled = ena;
            chkHARDNESS_R.IsEnabled = ena;
            chkUNWINDER.IsEnabled = ena;
            chkWINDER.IsEnabled = ena;
        }

        #endregion

        #region Save

        private void Save()
        {
            string P_ITEMCODE = string.Empty;
            string P_DENW = string.Empty;
            string P_DENF = string.Empty;
            string P_WIDTHALL = string.Empty;
            string P_WIDTHPIN = string.Empty;
            string P_WIDTHCOAT = string.Empty;

            string P_WIDTHSELVAGEL = string.Empty;
            string P_WIDTHSELVAGER = string.Empty;

            string P_TRIML = string.Empty;
            string P_TRIMR = string.Empty;
            string P_FLOPPYL = string.Empty;
            string P_FLOPPYR = string.Empty;
            string P_UNWINDER = string.Empty;
            string P_WINDER = string.Empty;
            string P_HARDNESSL = string.Empty;
            string P_HARDNESSC = string.Empty;
            string P_HARDNESSR = string.Empty;

            if (cbItemGoods.SelectedValue != null)
                P_ITEMCODE = cbItemGoods.SelectedValue.ToString();

            if (chkDENSITY_W.IsChecked == true)
                P_DENW = "1";

            if (chkDENSITY_F.IsChecked == true)
                P_DENF = "1";

            if (chkWIDTH_ALL.IsChecked == true)
                P_WIDTHALL = "1";

            if (chkWIDTH_PIN.IsChecked == true)
                P_WIDTHPIN = "1";

            if (chkWIDTH_COAT.IsChecked == true)
                P_WIDTHCOAT = "1";
            if (chkWIDTH_Selvage_L.IsChecked == true)
                P_WIDTHSELVAGEL = "1";
            if (chkWIDTH_Selvage_R.IsChecked == true)
                P_WIDTHSELVAGER = "1";

            if (chkTRIM_L.IsChecked == true)
                P_TRIML = "1";

            if (chkTRIM_R.IsChecked == true)
                P_TRIMR = "1";

            if (chkFLOPPY_L.IsChecked == true)
                P_FLOPPYL = "1";

            if (chkFLOPPY_R.IsChecked == true)
                P_FLOPPYR = "1";

            if (chkUNWINDER.IsChecked == true)
                P_UNWINDER = "1";

            if (chkWINDER.IsChecked == true)
                P_WINDER = "1";

            if (chkHARDNESS_L.IsChecked == true)
                P_HARDNESSL = "1";

            if (chkHARDNESS_C.IsChecked == true)
                P_HARDNESSC = "1";

            if (chkHARDNESS_R.IsChecked == true)
                P_HARDNESSR = "1";

            if (HundredMDataService.Instance.ITM_UPDATE100MRECORD(P_ITEMCODE,
        P_DENW, P_DENF, P_WIDTHALL, P_WIDTHPIN, P_WIDTHCOAT, P_WIDTHSELVAGEL, P_WIDTHSELVAGER, P_TRIML, P_TRIMR,
        P_FLOPPYL, P_FLOPPYR, P_UNWINDER, P_WINDER, P_HARDNESSL, P_HARDNESSC, P_HARDNESSR) == true)
            {
                "Save complete".ShowMessageBox();

                ClearControl();
            }
            else
            {
                "Can't Save".ShowMessageBox();
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
