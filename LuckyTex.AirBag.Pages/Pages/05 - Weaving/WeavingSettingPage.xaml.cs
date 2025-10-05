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

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WeavingSettingPage.xaml
    /// </summary>
    public partial class WeavingSettingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingSettingPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private WeavingSession _session = new WeavingSession();
        string strType = string.Empty;
        decimal? totalZone = null;

        string MC = string.Empty;
        string beamlot = string.Empty;

        #endregion

        #region UserControl_Loaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ClearInputs();

            string P_BEAMROLL = string.Empty;
            decimal? P_DOFFNO = null;

            WEAV_WEAVINGINPROCESSLIST(P_BEAMROLL, P_DOFFNO);
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            string mc = string.Empty;
            string zone = string.Empty;
            string PRODUCTTYPEID = string.Empty;

            if (!string.IsNullOrEmpty(txtTotal.Text))
            {
                string m = string.Empty;
                int intmc = 0;
                intmc = int.Parse(txtTotal.Text);
                if (intmc <= 9)
                {
                    m = "0" + intmc.ToString();
                }
                else
                {
                    m = txtTotal.Text;
                }

                if (rbA.IsChecked == true)
                    zone = "A";
                else if (rbB.IsChecked == true)
                    zone = "B";
                else if (rbC.IsChecked == true)
                    zone = "C";
                else if (rbD.IsChecked == true)
                    zone = "D";
                else if (rbE.IsChecked == true)
                    zone = "E";
                else if (rbF.IsChecked == true)
                    zone = "F";
                else if (rbG.IsChecked == true)
                    zone = "G";

                mc = zone + m;

            }
            else
            {
                if (!string.IsNullOrEmpty(MC))
                {
                    mc = MC;
                }
            }

            if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false)
            {
                PRODUCTTYPEID = "1";
            }
            else
            {
                PRODUCTTYPEID = "2";
            }

            if (!string.IsNullOrEmpty(mc))
            {
                if (Weave_CheckWeavingMC(mc) == true)
                {
                    LogInInfo logInfo = this.ShowLogInBox();
                    if (logInfo != null)
                    {
                        int processId = 5; // for inspection
                        List<LogInResult> operators = UserDataService.Instance
                            .GetOperators(logInfo.UserName, logInfo.Password, processId);

                        if (null == operators || operators.Count <= 0)
                        {
                            "This User can not be Use for This Menu".ShowMessageBox(true);
                            return;
                        }
                        else
                        {
                            WeavingProcessPage page = new WeavingProcessPage();
                            page.Setup(logInfo.UserName, mc, strType, PRODUCTTYPEID,true);

                            PageManager.Instance.Current = page;
                        }
                    }
                }
                else
                {
                    txtTotal.Text = "";
                    txtTotal.SelectAll();
                    txtTotal.Focus();
                }
            }
            else
            {
                "Please select MC".ShowMessageBox(true);
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        private void cmdRefresh_Click(object sender, RoutedEventArgs e)
        {
            string P_BEAMROLL = string.Empty;
            decimal? P_DOFFNO = null;
            MC = string.Empty;
            beamlot = string.Empty;

            WEAV_WEAVINGINPROCESSLIST(P_BEAMROLL, P_DOFFNO);
        }

        private void cmdCancelLoomSetting_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MC) && !string.IsNullOrEmpty(beamlot))
            {
                LogInInfo logInfo = this.ShowLogInBox();
                if (logInfo != null)
                {
                    int processId = 5; // for inspection
                    List<LogInResult> operators = UserDataService.Instance
                        .GetOperators(logInfo.UserName, logInfo.Password, processId);

                    if (null == operators || operators.Count <= 0)
                    {
                        "No Authorize for Edit Length".ShowMessageBox(true);
                        return;
                    }
                    else
                    {
                        if (MessageBox.Show("Do you want to Cancel this Loom Setting?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            string RESULT = string.Empty;

                            RESULT = WeavingDataService.Instance.WEAVE_CANCELLOOMSETUP(beamlot, MC, logInfo.UserName);

                            if (string.IsNullOrEmpty(RESULT))
                            {
                                string P_BEAMROLL = string.Empty;
                                decimal? P_DOFFNO = null;
                                MC = string.Empty;
                                beamlot = string.Empty;

                                WEAV_WEAVINGINPROCESSLIST(P_BEAMROLL, P_DOFFNO);
                            }
                            else
                            {
                                RESULT.ShowMessageBox(false);
                            }

                        }
                    }
                }
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

        #region txtTotal_KeyDown

        private void txtTotal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (string.IsNullOrEmpty(txtTotal.Text))
                {
                    txtTotal.SelectAll();
                    txtTotal.Focus();
                }
                else
                {
                    cmdOK.Focus();
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region txtTotal_TextChanged

        private void txtTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTotal.Text))
            {
                try
                {
                    string z = string.Empty;

                    decimal t = decimal.Parse(txtTotal.Text);

                    if (rbA.IsChecked == true)
                        z = "A";
                    else if (rbB.IsChecked == true)
                        z = "B";
                    else if (rbC.IsChecked == true)
                        z = "C";
                    else if (rbD.IsChecked == true)
                        z = "D";
                    else if (rbE.IsChecked == true)
                        z = "E";
                    else if (rbF.IsChecked == true)
                        z = "F";
                    else if (rbG.IsChecked == true)
                        z = "G";

                    if (t > totalZone)
                    {
                        string msg = "No MC is Not Available for this Zone " + z;
                        msg.ShowMessageBox(true);

                        txtTotal.Text = totalZone.Value.ToString("#,##0.##");
                    }
                }
                catch
                {
                    txtTotal.Text = totalZone.Value.ToString("#,##0.##");
                }
            }
        }

        #endregion

        #endregion

        #region RadioButton

        private void rbA_Checked(object sender, RoutedEventArgs e)
        {
            if (rbA.IsChecked == true)
                GetTotal("A");
        }

        private void rbB_Checked(object sender, RoutedEventArgs e)
        {
            if (rbB.IsChecked == true)
                GetTotal("B");
        }

        private void rbC_Checked(object sender, RoutedEventArgs e)
        {
            if (rbC.IsChecked == true)
                GetTotal("C");
        }

        private void rbD_Checked(object sender, RoutedEventArgs e)
        {
            if (rbD.IsChecked == true)
                GetTotal("D");
        }

        private void rbE_Checked(object sender, RoutedEventArgs e)
        {
            if (rbE.IsChecked == true)
                GetTotal("E");
        }

        private void rbF_Checked(object sender, RoutedEventArgs e)
        {
            if (rbF.IsChecked == true)
                GetTotal("F");
        }

        private void rbG_Checked(object sender, RoutedEventArgs e)
        {
            if (rbG.IsChecked == true)
                GetTotal("G");
        }
        #endregion

        #region gridWeaving_SelectedCellsChanged

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
                            if (((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).BEAMLOT != null)
                            {
                                beamlot = ((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).BEAMLOT;
                            }
                            else
                            {
                                beamlot = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).MC != null)
                            {
                                MC = ((LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST)(gridWeaving.CurrentCell.Item)).MC;
                            }
                            else
                            {
                                MC = string.Empty;
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

        #region private Methods

        #region ClearInputs

        private void ClearInputs()
        {
            strType = string.Empty;
            totalZone = null;

            rbA.IsChecked = true;
            rbB.IsChecked = false;
            rbC.IsChecked = false;
            rbD.IsChecked = false;
            rbE.IsChecked = false;
            rbF.IsChecked = false;
            rbG.IsChecked = false;

            rbMassProduction.IsChecked = true;
            rbTest.IsChecked = false;

            txtTotal.Text = string.Empty;

            txtTotal.SelectAll();
            txtTotal.Focus();

            if (rbA.IsChecked == true)
                GetTotal("A");

            MC = string.Empty;

            if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWeaving.SelectedItems.Clear();
            else
                this.gridWeaving.SelectedItem = null;

            gridWeaving.ItemsSource = null;
        }

        #endregion

        #region GetTotal

        private void GetTotal(string zone)
        {
            try
            {
                List<WEAV_GETMACHINEZONELIST> results = _session.GetWeav_getMachineZoneList(zone);

                if (results != null)
                {
                    if (results.Count > 0)
                    {
                        strType = results[0].TYPE;

                        decimal? total = null;
                        total = results[0].TOTAL;

                        if (total != null)
                        {
                            totalZone = total;
                        }
                        else
                        {
                            totalZone = 0;
                        }
                    }
                    else
                    {
                        totalZone = 0;
                    }
                }
                else
                {
                    totalZone = 0;
                }
            }
            catch(Exception ex)
            {
                ex.Message.ToString().ShowMessageBox();
            }
        }

        #endregion

        #region WEAV_WEAVINGINPROCESSLIST

        private void WEAV_WEAVINGINPROCESSLIST(string P_BEAMROLL, decimal? P_DOFFNO)
        {
            List<WEAV_WEAVINGINPROCESSLIST> results = WeavingDataService.Instance.WEAV_WEAVINGINPROCESSLIST(P_BEAMROLL, P_DOFFNO);

            if (results != null && results.Count > 0)
            {
                List<LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST> dataList = new List<LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST>();
                int i = 0;

                foreach (var row in results)
                {
                    LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST dataItemNew = new LuckyTex.Models.WEAV_WEAVINGINPROCESSLIST();

                    dataItemNew.BEAMLOT = results[i].BEAMLOT;
                    dataItemNew.MC = results[i].MC;
                    dataItemNew.BARNO = results[i].BARNO;

                    dataItemNew.REEDNO2 = results[i].REEDNO2;
                    dataItemNew.WEFTYARN = results[i].WEFTYARN;
                    dataItemNew.TEMPLETYPE = results[i].TEMPLETYPE;
                   
                    dataItemNew.STARTDATE = results[i].STARTDATE;
                    dataItemNew.FINISHDATE = results[i].FINISHDATE;
                    dataItemNew.FINISHFLAG = results[i].FINISHFLAG;
                    dataItemNew.SETTINGBY = results[i].SETTINGBY;
                    dataItemNew.EDITDATE = results[i].EDITDATE;
                    dataItemNew.EDITBY = results[i].EDITBY;
                    dataItemNew.ITM_WEAVING = results[i].ITM_WEAVING;
                    dataItemNew.PRODUCTTYPEID = results[i].PRODUCTTYPEID;
                    dataItemNew.WIDTH = results[i].WIDTH;

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

        private bool Weave_CheckWeavingMC(string LOOMNO)
        {
            List<WEAVE_CHECKWEAVINGMC> results = WeavingDataService.Instance.WEAVE_CHECKWEAVINGMC(LOOMNO);

            if (results != null)
            {
                if (results.Count > 0)
                {
                    if (!string.IsNullOrEmpty(results[0].MACHINEID))
                    {
                        return true;
                    }
                    else
                    {
                        "Invalid Loom No, Please Select Again".ShowMessageBox();
                        return false;
                    }
                }
                else
                {
                    "Invalid Loom No, Please Select Again".ShowMessageBox();
                    return false;
                }
            }
            else
            {
                "Invalid Loom No, Please Select Again".ShowMessageBox();
                return false;
            }
        }
       
        #endregion

    }
}
