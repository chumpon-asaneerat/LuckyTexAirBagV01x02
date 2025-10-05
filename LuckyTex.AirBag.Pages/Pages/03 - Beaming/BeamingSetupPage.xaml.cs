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

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for BeamingSetupPage.xaml
    /// </summary>
    public partial class BeamingSetupPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingSetupPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private BeamingSession _session = new BeamingSession();

        string opera = string.Empty;
        string mcNo = string.Empty;
        string mcName = string.Empty;
        string WARPHEADNO = string.Empty;

        decimal? ACTUALCH = null;

        int checkIsSelect = 0;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadItemGood();

            ClearControl();

            if (opera != "")
                txtOperator.Text = opera;

            if (mcName != "")
                txtMCNo.Text = mcName;
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

        #region cmdClear_Click

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            ClearGridBeam();
        }

        #endregion

        #region cmdStart_Click

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBeamerNo.Text))
            {
                if (txtBeamerNo.Text.Length <= 20)
                {
                    if (!string.IsNullOrEmpty(txtNOWARPBEAM.Text) && !string.IsNullOrEmpty(txtActualBeam.Text))
                    {

                        if (!string.IsNullOrEmpty(txtTOTALYARN.Text) && !string.IsNullOrEmpty(txtTotalEnd.Text) && !string.IsNullOrEmpty(txtADJUSTKEBA.Text))
                        {
                            decimal? total = null;
                            decimal? totalEnd = null;
                            decimal? adjKeba = null;
                            decimal? takeOut = null;
                            decimal? Keba = null;


                            #region total
                            try
                            {
                                total = decimal.Parse(txtTOTALYARN.Text);
                            }
                            catch
                            {
                                total = 0;
                            }
                            #endregion

                            #region totalEnd
                            try
                            {
                                totalEnd = decimal.Parse(txtTotalEnd.Text);
                            }
                            catch
                            {
                                totalEnd = 0;
                            }
                            #endregion

                            #region adjKeba
                            try
                            {
                                adjKeba = decimal.Parse(txtADJUSTKEBA.Text);
                            }
                            catch
                            {
                                adjKeba = 0;
                            }
                            #endregion

                            if (totalEnd == (total - adjKeba))
                            {
                                if (!string.IsNullOrEmpty(txtTakeOut.Text) && !string.IsNullOrEmpty(txtTOTALKEBA.Text))
                                {
                                    #region takeOut
                                    try
                                    {
                                        takeOut = decimal.Parse(txtTakeOut.Text);
                                    }
                                    catch
                                    {
                                        takeOut = 0;
                                    }
                                    #endregion

                                    #region Keba
                                    try
                                    {
                                        Keba = decimal.Parse(txtTOTALKEBA.Text);
                                    }
                                    catch
                                    {
                                        Keba = 0;
                                    }
                                    #endregion

                                    if ((takeOut) == (Keba + adjKeba))
                                    {
                                        if (adjKeba == 0)
                                        {
                                            Start();
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(txtRemark.Text))
                                            {
                                                Start();
                                            }
                                            else
                                            {
                                                "Remark isn't Null".ShowMessageBox(true);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        "Please Check Take Out Yarn and try to start again".ShowMessageBox(true);
                                    }
                                }
                            }
                            else
                            {
                                "Total Yarn is invalid".ShowMessageBox(true);
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(txtTOTALYARN.Text))
                            {
                                "Total Yarn isn't Null".ShowMessageBox(true);

                                txtTOTALYARN.Focus();
                                txtTOTALYARN.SelectAll();
                            }
                            else if (string.IsNullOrEmpty(txtTotalEnd.Text))
                            {
                                "Total Yarn isn't Null".ShowMessageBox(true);

                                txtTotalEnd.Focus();
                                txtTotalEnd.SelectAll();
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtNOWARPBEAM.Text))
                        {
                            "No Beam isn't Null".ShowMessageBox(true);

                            txtNOWARPBEAM.Focus();
                            txtNOWARPBEAM.SelectAll();
                        }
                        else if (string.IsNullOrEmpty(txtActualBeam.Text))
                        {
                            "Actual Beam isn't Null".ShowMessageBox(true);

                            txtActualBeam.Focus();
                            txtActualBeam.SelectAll();
                        }
                    }
                }
                else
                {
                    "Beamer Lot Length can't over 20".ShowMessageBox(true);
                }
            }
            else
            {
                "Beamer No isn't Null".ShowMessageBox(true);

                txtBeamerNo.Focus();
                txtBeamerNo.SelectAll();
            }
        }

        #endregion

        #endregion

        #region cbItemCode_LostFocus

        private void cbItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbItemCode.SelectedValue != null)
            {
                List<BEAM_GETSPECBYCHOPNO> results = BeamingDataService.Instance.BEAM_GETSPECBYCHOPNO(cbItemCode.SelectedValue.ToString());

                if (results.Count > 0)
                {
                    txtNOWARPBEAM.Text = results[0].NOWARPBEAM.Value.ToString("#,##0.##");
                    txtTOTALYARN.Text = results[0].TOTALYARN.Value.ToString("#,##0.##");
                    txtTOTALKEBA.Text = results[0].TOTALKEBA.Value.ToString("#,##0.##");
                    txtADJUSTKEBA.Text = "0";
                }
                else
                {
                    txtNOWARPBEAM.Text = "0";
                    txtTOTALYARN.Text = "0";
                    txtTOTALKEBA.Text = "0";
                    txtADJUSTKEBA.Text = "0";
                }

                BEAM_GETWARPNOBYITEMPREPARE(cbItemCode.SelectedValue.ToString());
            }

        }

        #endregion

        #region TextBox

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region txtBeamerNo_KeyDown

        private void txtBeamerNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWarperLot.Focus();
                txtWarperLot.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtADJUSTKEBA_KeyDown

        private void txtADJUSTKEBA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWarperLot.Focus();
                txtWarperLot.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtADJUSTKEBA_PreviewTextInput

        private void txtADJUSTKEBA_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion

        #region txtWarperLot_KeyDown

        private void txtWarperLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (!string.IsNullOrEmpty(txtWarperLot.Text) && !string.IsNullOrEmpty(txtWARPHEADNO.Text))
                {
                    string P_WARPHEADNO = txtWARPHEADNO.Text.Trim();
                    string WarperLot = txtWarperLot.Text.Trim();
                    decimal? NoCH = ACTUALCH;

                    if (ChkWarperLot(WarperLot) == true)
                    {
                        AddBeaming(P_WARPHEADNO, WarperLot, NoCH);
                    }
                    else
                    {
                        "Warper Roll had in Data".ShowMessageBox(false);

                        txtWarperLot.Text = string.Empty;
                        txtWarperLot.SelectAll();
                        txtWarperLot.Focus();
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(txtWarperLot.Text))
                    {
                        "Warper Lot isn't null".ShowMessageBox(true);
                    }
                    else if (string.IsNullOrEmpty(txtWARPHEADNO.Text))
                    {
                        "Warper No isn't null".ShowMessageBox(true);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region gridWarping_SelectedCellsChanged

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

        #region gridBeaming_SelectedCellsChanged

        private void gridBeaming_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridBeaming.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridBeaming);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)(gridBeaming.CurrentCell.Item)).IsSelect == true)
                            {
                                if (((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)(gridBeaming.CurrentCell.Item)).WARPHEADNO != null)
                                {
                                    WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)(gridBeaming.CurrentCell.Item)).WARPHEADNO;

                                    //if (string.IsNullOrEmpty(txtWARPHEADNO.Text))
                                    //{
                                        txtWARPHEADNO.Text = WARPHEADNO;
                                        //txtBeamerNo.Text = WARPHEADNO;
                                    //}
                                }
                                else
                                {
                                    WARPHEADNO = string.Empty;
                                    txtWARPHEADNO.Text = "";
                                    //txtBeamerNo.Text = "";
                                }

                                if (((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)(gridBeaming.CurrentCell.Item)).ACTUALCH != null)
                                {
                                    ACTUALCH = ((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)(gridBeaming.CurrentCell.Item)).ACTUALCH;
                                }
                                else
                                {
                                    ACTUALCH = null;
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

        #endregion

        #region Selected_Checked

        private void Selected_Checked(object sender, RoutedEventArgs e)
        {
            SelectedWARPHEADNO();
        }

        #endregion

        #region Selected_Unchecked

        private void Selected_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectedWARPHEADNO();
        }

        #endregion

        #endregion

        #region gridBeam_SelectedCellsChanged

        private void gridBeam_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridBeam.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridBeam);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.CurrentCell.Item)).WARPERLOT != null)
                            {

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

        private void TotalEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridBeam.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPHEADNO != ""
                        && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPERLOT != "")
                    {
                        if (((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).OLDTOTALEND != null
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TOTALEND != null
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TAKEOUT != null
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).LENGTH != null)
                        {
                            EditBeamTotalEnd(((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPHEADNO, ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPERLOT
                                , ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).LENGTH
                                , ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).OLDTOTALEND, ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TOTALEND
                                , ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TAKEOUT);

                            SumTotalGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void TakeOut_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridBeam.SelectedItems.Count > 0)
                {
                    if (((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPHEADNO != ""
                        && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPERLOT != "")
                    {
                        if (((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).OLDTOTALEND != null
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TOTALEND != null
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TAKEOUT != null
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).LENGTH != null)
                        {
                            EditBeamTakeOut(((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPHEADNO, ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).WARPERLOT
                                , ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).LENGTH
                                , ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).OLDTOTALEND, ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TOTALEND
                                , ((LuckyTex.Models.BEAM_GETWARPERLOT)(gridBeam.SelectedItem)).TAKEOUT);

                            SumTotalGrid();
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

        #region private Methods

        #region LoadItemGood

        private void LoadItemGood()
        {
            try
            {
                //List<WEAV_GETITEMWEAVINGLIST> items = _session.GetItemCodeData();

                //this.cbItemCode.ItemsSource = items;
                //this.cbItemCode.DisplayMemberPath = "ITM_WEAVING";
                //this.cbItemCode.SelectedValuePath = "ITM_WEAVING";

                List<ITM_GETITEMPREPARELIST> items = _session.GetItemCodeData();

                this.cbItemCode.ItemsSource = items;
                this.cbItemCode.DisplayMemberPath = "ITM_PREPARE";
                this.cbItemCode.SelectedValuePath = "ITM_PREPARE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            cbItemCode.SelectedValue = null;

            txtBeamerNo.Text = "";
            
            txtNOWARPBEAM.Text = "";
            txtTOTALYARN.Text = "";
            txtTOTALKEBA.Text = "";
            txtADJUSTKEBA.Text = "";

            rbMassProduction.IsChecked = true;
            rbTest.IsChecked = false;

            txtRemark.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridBeaming.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridBeaming.SelectedItems.Clear();
            else
                this.gridBeaming.SelectedItem = null;

            gridBeaming.ItemsSource = null;

            txtWARPHEADNO.Text = "";
            txtWarperLot.Text = "";

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridBeam.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridBeam.SelectedItems.Clear();
            else
                this.gridBeam.SelectedItem = null;

            gridBeam.ItemsSource = null;

            WARPHEADNO = string.Empty;

            SumTotalGrid();

            txtBeamerNo.SelectAll();
            txtBeamerNo.Focus();
        }

        #endregion

        #region BEAM_GETWARPNOBYITEMPREPARE

        private void BEAM_GETWARPNOBYITEMPREPARE(string P_ITMPREPARE)
        {
            List<BEAM_GETWARPNOBYITEMPREPARE> lots = new List<BEAM_GETWARPNOBYITEMPREPARE>();

            lots = BeamingDataService.Instance.BEAM_GETWARPNOBYITEMPREPARE(P_ITMPREPARE);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridBeaming.ItemsSource = lots;
            }
            else
            {
                gridBeaming.ItemsSource = null;
            }
        }

        #endregion

        #region AddBeaming

        private void AddBeaming(string P_WARPHEADNO, string WarperLot, decimal? NoCH)
        {
            try
            {
                #region AddWarpingDetail

                List<BEAM_GETWARPERLOT> results = BeamingDataService.Instance.CheckWarpheadNo_WarperLot(P_WARPHEADNO, WarperLot, NoCH);

                if (results != null && results.Count > 0)
                {
                    string WARPHEADNO = string.Empty;
                    string WARPERLOT = string.Empty;
                    decimal? LENGTH = null;
                    decimal? TOTALEND = null;
                    decimal? TAKEOUT = null;

                    int i = 0;

                    foreach (var row in results)
                    {
                        WARPHEADNO = string.Empty;
                        WARPERLOT = string.Empty;
                        LENGTH = null;
                        TOTALEND = null;
                        TAKEOUT = null;

                        WARPHEADNO = results[i].WARPHEADNO;
                        WARPERLOT = results[i].WARPERLOT;

                        LENGTH = results[i].LENGTH;
                        TOTALEND = results[i].TOTALEND;
                        TAKEOUT = results[i].TAKEOUT;

                        if (AddBeamingDetail(WARPHEADNO, WARPERLOT, LENGTH, TOTALEND, TAKEOUT) == false)
                        {

                            "This Warper Roll is invalid for this Warper Lot".ShowMessageBox(true);
                            break;
                        }

                        i++;
                    }

                    txtWarperLot.Text = "";
                    txtWarperLot.Focus();
                    txtWarperLot.SelectAll();
                }
                else
                {
                    "This Warper Roll is invalid for this Warper Lot".ShowMessageBox(true);

                    txtWarperLot.Text = "";
                    txtWarperLot.Focus();
                    txtWarperLot.SelectAll();
                }

                SumTotalGrid();

                #endregion
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region AddBeamingDetail

        private bool AddBeamingDetail(string WARPHEADNO, string WARPERLOT, decimal? LENGTH, decimal? TOTALEND, decimal? TAKEOUT)
        {
            try
            {
                List<LuckyTex.Models.BEAM_GETWARPERLOT> dataList = new List<LuckyTex.Models.BEAM_GETWARPERLOT>();
                int o = 0;
                foreach (var row in gridBeam.Items)
                {
                    LuckyTex.Models.BEAM_GETWARPERLOT dataItem = new LuckyTex.Models.BEAM_GETWARPERLOT();

                    dataItem.WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO;
                    dataItem.WARPERLOT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT;
                    dataItem.LENGTH = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).LENGTH;
                    dataItem.OLDTOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND;
                    dataItem.TOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TOTALEND;
                    dataItem.TAKEOUT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TAKEOUT;
                   
                    o++;

                    dataList.Add(dataItem);
                }

                LuckyTex.Models.BEAM_GETWARPERLOT dataItemNew = new LuckyTex.Models.BEAM_GETWARPERLOT();

                dataItemNew.WARPHEADNO = WARPHEADNO;
                dataItemNew.WARPERLOT = WARPERLOT;
                dataItemNew.LENGTH = LENGTH;
                dataItemNew.OLDTOTALEND = TOTALEND;
                dataItemNew.TOTALEND = TOTALEND;
                dataItemNew.TAKEOUT = TAKEOUT;

                dataList.Add(dataItemNew);

                this.gridBeam.ItemsSource = dataList;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region EditBeamTotalEnd
        private void EditBeamTotalEnd(string WARPHEADNO, string WARPERLOT, decimal? LENGTH, decimal? OLDTOTALEND, decimal? TOTALEND, decimal? TAKEOUT)
        {
            if (gridBeam.SelectedItems.Count > 0)
            {
                try
                {
                    List<LuckyTex.Models.BEAM_GETWARPERLOT> dataList = new List<LuckyTex.Models.BEAM_GETWARPERLOT>();

                    int o = 0;
                    foreach (var row in gridBeam.Items)
                    {
                        LuckyTex.Models.BEAM_GETWARPERLOT dataItem = new LuckyTex.Models.BEAM_GETWARPERLOT();

                        if (((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO == WARPHEADNO
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT == WARPERLOT)
                        {
                            dataItem.WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO;
                            dataItem.WARPERLOT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT;
                            dataItem.LENGTH = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).LENGTH;
                            dataItem.OLDTOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND;
                            dataItem.TOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TOTALEND;
                            dataItem.TAKEOUT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND - TOTALEND;

                            dataList.Add(dataItem);

                        }
                        else
                        {

                            dataItem.WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO;
                            dataItem.WARPERLOT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT;
                            dataItem.LENGTH = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).LENGTH;
                            dataItem.OLDTOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND;
                            dataItem.TOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TOTALEND;
                            dataItem.TAKEOUT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TAKEOUT;

                            dataList.Add(dataItem);
                        }
                        o++;
                    }

                    this.gridBeam.ItemsSource = dataList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region EditBeamTakeOut
        private void EditBeamTakeOut(string WARPHEADNO, string WARPERLOT, decimal? LENGTH, decimal? OLDTOTALEND, decimal? TOTALEND, decimal? TAKEOUT)
        {
            if (gridBeam.SelectedItems.Count > 0)
            {
                try
                {
                    List<LuckyTex.Models.BEAM_GETWARPERLOT> dataList = new List<LuckyTex.Models.BEAM_GETWARPERLOT>();

                    int o = 0;
                    foreach (var row in gridBeam.Items)
                    {
                        LuckyTex.Models.BEAM_GETWARPERLOT dataItem = new LuckyTex.Models.BEAM_GETWARPERLOT();

                        if (((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO == WARPHEADNO
                            && ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT == WARPERLOT)
                        {
                            dataItem.WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO;
                            dataItem.WARPERLOT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT;
                            dataItem.LENGTH = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).LENGTH;
                            dataItem.OLDTOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND;
                            dataItem.TOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND - TAKEOUT;
                            dataItem.TAKEOUT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TAKEOUT;

                            dataList.Add(dataItem);

                        }
                        else
                        {

                            dataItem.WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPHEADNO;
                            dataItem.WARPERLOT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT;
                            dataItem.LENGTH = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).LENGTH;
                            dataItem.OLDTOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).OLDTOTALEND;
                            dataItem.TOTALEND = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TOTALEND;
                            dataItem.TAKEOUT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TAKEOUT;

                            dataList.Add(dataItem);
                        }
                        o++;
                    }

                    this.gridBeam.ItemsSource = dataList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region SumTotalGrid

        private void SumTotalGrid()
        {
            if (gridBeam.Items != null && gridBeam.Items.Count > 0)
            {
                decimal? TOTALEND = 0;
                decimal? TAKEOUT = 0;

                int o = 0;
                foreach (var row in gridBeam.Items)
                {
                    TOTALEND += ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TOTALEND;
                    TAKEOUT += ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).TAKEOUT;
                    o++;
                }

                txtTotalEnd.Text = TOTALEND.Value.ToString("#,##0.##");
                txtTakeOut.Text = TAKEOUT.Value.ToString("#,##0.##");
                txtActualBeam.Text = o.ToString("#,##0");
            }
            else
            {
                txtTotalEnd.Text = "0";
                txtTakeOut.Text = "0";
                txtActualBeam.Text = "0";
            }
        }

        #endregion

        #region ChkWarperLot

        private bool ChkWarperLot(string WarperLot)
        {
            try
            {
                bool chkWL = true;

                int o = 0;
                foreach (var row in gridBeam.Items)
                {
                    if (((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[o])).WARPERLOT == WarperLot)
                    {
                        chkWL = false;
                        break;
                    }

                    o++;
                }

                return chkWL;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region SelectedWARPHEADNO

        private void SelectedWARPHEADNO()
        {
            if (gridBeaming.Items.Count > 0)
            {
                try
                {
                    string  warpHeadNo = string.Empty;
                    checkIsSelect = 0;

                    int i = 0;
                    foreach (var row in gridBeaming.Items)
                    {
                        if (((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)((gridBeaming.Items)[i])).IsSelect == true)
                        {

                            checkIsSelect = i;

                            if (((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)((gridBeaming.Items)[i])).WARPHEADNO != null)
                            {
                                WARPHEADNO = ((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)((gridBeaming.Items)[i])).WARPHEADNO;

                                //if (string.IsNullOrEmpty(txtWARPHEADNO.Text))
                                //{
                                    txtWARPHEADNO.Text = WARPHEADNO;
                                //}

                                if (!string.IsNullOrEmpty(warpHeadNo))
                                {
                                    warpHeadNo += ",";
                                }

                                warpHeadNo += ((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)((gridBeaming.Items)[i])).WARPHEADNO;
                            }
                            else
                            {
                                WARPHEADNO = string.Empty;
                                txtWARPHEADNO.Text = "";
                                txtBeamerNo.Text = "";
                            }

                            if (((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)((gridBeaming.Items)[i])).ACTUALCH != null)
                            {
                                ACTUALCH = ((LuckyTex.Models.BEAM_GETWARPNOBYITEMPREPARE)((gridBeaming.Items)[i])).ACTUALCH;
                            }
                            else
                            {
                                ACTUALCH = null;
                            }
                        }

                        i++;
                    }

                    //if (warpHeadNo.Length <= 20)
                    //{
                        txtBeamerNo.Text = warpHeadNo;
                    //}
                    //else
                    //{
                    //    "Beamer Lot Length can't over 20".ShowMessageBox(true);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                WARPHEADNO = string.Empty;
                txtWARPHEADNO.Text = "";
                txtBeamerNo.Text = "";
                ACTUALCH = null;
            }
        }

        #endregion

        #region ClearGridBeam

        private void ClearGridBeam()
        {
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridBeam.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridBeam.SelectedItems.Clear();
            else
                this.gridBeam.SelectedItem = null;

            gridBeam.ItemsSource = null;

            txtWARPHEADNO.Text = "";
            txtBeamerNo.Text = "";
            txtRemark.Text = "";

            SumTotalGrid();
        }

        #endregion

        #region Start
        private bool Start()
        {
            string result = string.Empty;
            bool chkStart = true;

            try
            {
                if (gridBeam.Items.Count > 0)
                {
                    //int i = 0;
                    string P_BEAMNO = string.Empty;
                    string P_WARPERHEADNO = string.Empty;

                    string warpHeadNo = string.Empty;
                    string WARPERLOT = string.Empty;

                    string P_ITMPREPARE = string.Empty;
                    string P_PRODUCTID = string.Empty;

                    decimal? P_TOTALYARN = null;
                    decimal? P_TOTALKEBA = null;
                    decimal? P_ADJUSTKEBA = null;
                    string P_REMARK= string.Empty;

                    if (!string.IsNullOrEmpty(txtBeamerNo.Text))
                        P_BEAMNO = txtBeamerNo.Text;

                    if (!string.IsNullOrEmpty(txtWARPHEADNO.Text))
                    {
                        if (checkIsSelect <= 1)
                        {
                            P_WARPERHEADNO = txtWARPHEADNO.Text;
                        }
                        else
                        {
                            P_WARPERHEADNO = null;
                        }
                    }

                    if (cbItemCode.SelectedValue != null)
                        P_ITMPREPARE = cbItemCode.SelectedValue.ToString();

                    #region P_TOTALYARN
                    try
                    {
                        if (!string.IsNullOrEmpty(txtTOTALYARN.Text))
                        {
                            P_TOTALYARN = decimal.Parse(txtTOTALYARN.Text);
                        }
                    }
                    catch
                    {
                        P_TOTALYARN = 0;
                    }
                    #endregion

                    #region P_TOTALKEBA
                    try
                    {
                        if (!string.IsNullOrEmpty(txtTOTALKEBA.Text))
                        {
                            P_TOTALKEBA = decimal.Parse(txtTOTALKEBA.Text);
                        }
                    }
                    catch
                    {
                        P_TOTALKEBA = 0;
                    }
                    #endregion

                    #region P_PRODUCTID

                    if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false)
                    {
                        P_PRODUCTID = "1";
                    }
                    else if (rbMassProduction.IsChecked == false && rbTest.IsChecked == true)
                    {
                        P_PRODUCTID = "2";
                    }

                    #endregion

                    #region P_ADJUSTKEBA
                    try
                    {
                        if (!string.IsNullOrEmpty(txtADJUSTKEBA.Text))
                        {
                            P_ADJUSTKEBA = decimal.Parse(txtADJUSTKEBA.Text);
                        }
                    }
                    catch
                    {
                        P_ADJUSTKEBA = 0;
                    }
                    #endregion

                    if (!string.IsNullOrEmpty(txtRemark.Text))
                        P_REMARK = txtRemark.Text;

                    if (!string.IsNullOrEmpty(P_BEAMNO) && !string.IsNullOrEmpty(P_ITMPREPARE))
                    {
                        result = BeamingDataService.Instance.BEAM_INSERTBEAMNO(P_BEAMNO, P_WARPERHEADNO, P_ITMPREPARE, P_PRODUCTID, mcNo, P_TOTALYARN, P_TOTALKEBA, opera
                            , P_ADJUSTKEBA, P_REMARK);

                        if (!string.IsNullOrEmpty(result))
                        {
                            result.ShowMessageBox(true);
                            chkStart = false;
                        }
                        else
                        {
                            //if (!string.IsNullOrEmpty(P_WARPERHEADNO))
                            //{
                            //    if (BeamingDataService.Instance.WARP_UPDATESETTINGHEADFLAG(P_WARPERHEADNO) == true)
                            //    {

                            #region BEAM_INSERTBEAMERROLLSETTING

                            int i = 0;
                            bool chkBeam = false;

                            foreach (var row in gridBeam.Items)
                            {
                                warpHeadNo = string.Empty;
                                WARPERLOT = string.Empty;

                                if (!string.IsNullOrEmpty(((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[i])).WARPHEADNO))
                                {
                                    warpHeadNo = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[i])).WARPHEADNO;
                                }

                                if (!string.IsNullOrEmpty(((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[i])).WARPERLOT))
                                {
                                    WARPERLOT = ((LuckyTex.Models.BEAM_GETWARPERLOT)((gridBeam.Items)[i])).WARPERLOT;
                                }

                                if (!string.IsNullOrEmpty(warpHeadNo) && !string.IsNullOrEmpty(WARPERLOT))
                                {
                                    if (BeamingDataService.Instance.BEAM_INSERTBEAMERROLLSETTING(P_BEAMNO, warpHeadNo, WARPERLOT) == false)
                                    {
                                        chkBeam = true;
                                        break;
                                    }
                                }

                                i++;
                            }

                            #endregion

                            if (chkBeam == false)
                            {
                                List<BEAM_GETBEAMERMCSTATUS> results = null;

                                results = BeamingDataService.Instance.BEAM_GETBEAMERMCSTATUS(mcNo);

                                BeamingProcessPage page = new BeamingProcessPage();
                                page.Setup(opera, mcName, mcNo, P_BEAMNO, P_ITMPREPARE, 0, results[0].TOTALBEAM, results[0].NOWARPBEAM);

                                PageManager.Instance.Current = page;
                            }
                            else
                            {
                                "Can't insert Beamer Roll setting".ShowMessageBox(true);
                            }

                            //    }
                            //    else
                            //    {
                            //        "Can't update Warp Setting".ShowMessageBox(true);
                            //    }
                            //}

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(P_BEAMNO))
                        {
                            "Beamer No isn't Null".ShowMessageBox(true);

                            txtBeamerNo.Focus();
                            txtBeamerNo.SelectAll();
                        }
                        //else if (string.IsNullOrEmpty(P_WARPERHEADNO))
                        //{
                        //    "Warper No isn't Null".ShowMessageBox(true);

                        //    txtWARPHEADNO.Focus();
                        //    txtWARPHEADNO.SelectAll();
                        //}
                        else if (string.IsNullOrEmpty(P_ITMPREPARE))
                        {
                            "Item Prepare isn't Null".ShowMessageBox(true);

                            cbItemCode.Focus();
                        }
                    }
                }

                return chkStart;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(string user, string DisplayName, string MCNo)
        {
            if (opera != null)
            {
                opera = user;
            }

            if (mcName != null)
            {
                mcName = DisplayName;
            }

            if (mcNo != null)
            {
                mcNo = MCNo;
            }
        }

        #endregion

    }
}

