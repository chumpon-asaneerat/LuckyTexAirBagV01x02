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
    /// Interaction logic for WeavingPage.xaml
    /// </summary>
    public partial class WeavingPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingPage()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            cmdDelete.Visibility = System.Windows.Visibility.Collapsed;
        }

        #endregion

        #region Internal Variables

        private WeavingSession _session = new WeavingSession();
        string opera = string.Empty;
        string P_WEAVINGLOTOLD = string.Empty;

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLoomNo();
            LoadShift();
            LoadItemGood();

            ClearInputs();

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

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Delete() == true)
            {
                ClearInputs();
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (Save() == true)
            {
                ClearInputs();
            }
        }

        private void cmdRePrint_Click(object sender, RoutedEventArgs e)
        {
            string WEAVINGLOT = string.Empty;
            WEAVINGLOT = txtWeavingLot.Text;

            if (WEAVINGLOT != "")
            {
                ConmonReportService.Instance.Printername = "";
                Preview(WEAVINGLOT);

                ClearInputs();
            }
            else
                "Weaving Lot isn't Null".ShowMessageBox(false);
        }

        private void cmdReport_Click(object sender, RoutedEventArgs e)
        {
            Report();
        }

        #endregion

        private void chkChinaFabric_Checked(object sender, RoutedEventArgs e)
        {
            if (chkChinaFabric.IsChecked == true)
                chkTKATFabric.IsChecked = false;
        }

        private void chkTKATFabric_Checked(object sender, RoutedEventArgs e)
        {
            if (chkTKATFabric.IsChecked == true)
                chkChinaFabric.IsChecked = false;
        }

        #region Controls Handlers

        private void cbLoomNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbLoomNo.SelectedValue != null)
            {
                if (cbLoomNo.SelectedValue.ToString() != "H" && cbLoomNo.SelectedValue.ToString() != "W" && cbLoomNo.SelectedValue.ToString() != "I")
                {
                    if (txtLoomNo.Text != "" && dteWeavingDate.SelectedDate != null)
                    {
                        GetWeavingLot();
                    }
                }
                else
                {
                    if (cbLoomNo.SelectedValue.ToString() == "H" || cbLoomNo.SelectedValue.ToString() == "W")
                    {
                        if (cbItemCode.ItemsSource != null)
                        {
                            if (cbItemCode.Text != "MN4750AGL")
                            {
                                cbItemCode.SelectedValue = "MN4750AGL";

                                if (cbItemCode.SelectedValue != null)
                                    GetWIDTHWEAVING(cbItemCode.SelectedValue.ToString());
                                else
                                    txtITM_WEAVING.Text = "0";
                            }
                        }
                    }
                    else if (cbLoomNo.SelectedValue.ToString() == "I")
                    {
                        if (cbItemCode.ItemsSource != null)
                        {
                            if (cbItemCode.Text != "47L1S00Z")
                            {
                                cbItemCode.SelectedValue = "47L1S00Z";

                                if (cbItemCode.SelectedValue != null)
                                    GetWIDTHWEAVING(cbItemCode.SelectedValue.ToString());
                                else
                                    txtITM_WEAVING.Text = "0";
                            }
                        }
                    }

                    if (dteWeavingDate.SelectedDate != null)
                    {
                        GetWeavingLot_HW();
                    }
                }
            }
        }

        private void cbItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbItemCode.SelectedValue != null)
                GetWIDTHWEAVING(cbItemCode.SelectedValue.ToString());
            else
                txtITM_WEAVING.Text = "0";
        }

        private void txtLoomNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtITM_WEAVING.Focus();
                txtITM_WEAVING.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLoomNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtLoomNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtLoomNo.Text != "" && dteWeavingDate.SelectedDate != null)
            {
                if (cbLoomNo.SelectedValue != null)
                {
                    if (cbLoomNo.SelectedValue.ToString() != "H" && cbLoomNo.SelectedValue.ToString() != "W" && cbLoomNo.SelectedValue.ToString() != "I")
                    {
                        GetWeavingLot();
                    }
                    else
                    {
                        GetWeavingLot_HW();
                    }
                }
            }
        }

        private void dteWeavingDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbLoomNo.SelectedValue != null)
            {
                if (cbLoomNo.SelectedValue.ToString() != "H" && cbLoomNo.SelectedValue.ToString() != "W" && cbLoomNo.SelectedValue.ToString() != "I")
                {
                    if (dteWeavingDate.SelectedDate != null && txtLoomNo.Text != "")
                        GetWeavingLot();
                }
                else
                {
                    if (cbLoomNo.SelectedValue.ToString() == "H" || cbLoomNo.SelectedValue.ToString() == "W")
                    {
                        if (cbItemCode.ItemsSource != null)
                        {
                            if (cbItemCode.Text != "MN4750AGL")
                            {
                                cbItemCode.SelectedValue = "MN4750AGL";

                                if (cbItemCode.SelectedValue != null)
                                    GetWIDTHWEAVING(cbItemCode.SelectedValue.ToString());
                                else
                                    txtITM_WEAVING.Text = "0";
                            }
                        }
                    }
                    else if (cbLoomNo.SelectedValue.ToString() == "I")
                    {
                        if (cbItemCode.ItemsSource != null)
                        {
                            if (cbItemCode.Text != "47L1S00Z")
                            {
                                cbItemCode.SelectedValue = "47L1S00Z";

                                if (cbItemCode.SelectedValue != null)
                                    GetWIDTHWEAVING(cbItemCode.SelectedValue.ToString());
                                else
                                    txtITM_WEAVING.Text = "0";
                            }
                        }
                    }

                    if (dteWeavingDate.SelectedDate != null)
                    {
                        GetWeavingLot_HW();
                    }
                }
            }
        }

        private void txtITM_WEAVING_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWeavingLot.Focus();
                txtWeavingLot.SelectAll();
                e.Handled = true;
            }
        }

        private void txtITM_WEAVING_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtWeavingLot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLength.Focus();
                txtLength.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWeavingLot_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtWeavingLot.Text != "")
            {
                GetWeavingingDataList(txtWeavingLot.Text);
            }
        }

        private void txtLength_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRemark.Focus();
                txtRemark.SelectAll();
                e.Handled = true;
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region private Methods

        #region LoadLoomNo

        private void LoadLoomNo()
        {
            if (cbLoomNo.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C", "D", "E", "F","I", "G", "H", "W" };

                cbLoomNo.ItemsSource = str;
                cbLoomNo.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadShift

        private void LoadShift()
        {
            if (cbShift.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C" ,"D" };

                cbShift.ItemsSource = str;
                cbShift.SelectedIndex = 0;
            }
        }

        #endregion

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

        #region GetWeavingingDataList

        private void GetWeavingingDataList(string WeavingLot)
        {
            try
            {
                List<GETWEAVINGINGDATA> getWeavinging = WeavingDataService.Instance.GetWeavingingDataList(WeavingLot);

                if (getWeavinging.Count > 0)
                {
                    if (getWeavinging[0].LOOMNO != "")
                    {
                        string loom = string.Empty;
                        string loomNo = string.Empty;

                        if (getWeavinging[0].LOOMNO.Length == 3)
                        {
                            loom = getWeavinging[0].LOOMNO.Substring(0, 1);
                            loomNo = getWeavinging[0].LOOMNO.Substring(1, 2);

                            cbLoomNo.SelectedValue = loom;
                            txtLoomNo.Text = loomNo;
                        }
                        else
                        {
                            //"Loom No is not true".ShowMessageBox(true);
                            loom = getWeavinging[0].LOOMNO.Substring(0, 1);
                            loomNo = getWeavinging[0].LOOMNO.Substring(1, getWeavinging[0].LOOMNO.Length - 1);

                            cbLoomNo.SelectedValue = loom;
                            txtLoomNo.Text = loomNo;
                        }
                    }

                    if (getWeavinging[0].WEAVINGDATE != null)
                        dteWeavingDate.SelectedDate = getWeavinging[0].WEAVINGDATE;

                    if (getWeavinging[0].SHIFT != null)
                        cbShift.SelectedValue = getWeavinging[0].SHIFT;
                    if (getWeavinging[0].ITM_WEAVING != null)
                        cbItemCode.SelectedValue = getWeavinging[0].ITM_WEAVING;

                    if (getWeavinging[0].WIDTH != null)
                        txtITM_WEAVING.Text = getWeavinging[0].WIDTH.Value.ToString("#,##0.##");
                    if (getWeavinging[0].LENGTH != null)
                        txtLength.Text = getWeavinging[0].LENGTH.Value.ToString("#,##0.##");

                    txtRemark.Text = getWeavinging[0].REMARK;

                    //เพิ่ม Load WEAVINGLOT
                    P_WEAVINGLOTOLD = getWeavinging[0].WEAVINGLOT;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(false);
            }
        }

        #endregion

        #region GetWeavingLot

        private void GetWeavingLot()
        {
            try
            {
                string P_LOOM = string.Empty;
                string P_LOOM2 = string.Empty;
                int Temp = 0;
                int TempM = 0;
                string LOOMNo = string.Empty;
                string WeavingLot = string.Empty;
                string WeavingDate = string.Empty;

                string yy = string.Empty;
                string mm = string.Empty;
                string m = string.Empty;
                string dd = string.Empty;

                if (cbLoomNo.SelectedValue.ToString() != "")
                    P_LOOM = cbLoomNo.SelectedValue.ToString();

                P_LOOM2 = txtLoomNo.Text;

                if (P_LOOM2 != "")
                {
                    try
                    {
                        Temp = int.Parse(P_LOOM2);

                        if (Temp <= 9)
                            LOOMNo = "0" + Temp.ToString();
                        else
                            LOOMNo = Temp.ToString();
                    }
                    catch
                    {
                        LOOMNo = "00";
                    }
                }

                if (dteWeavingDate.SelectedDate != null)
                {
                    yy = dteWeavingDate.SelectedDate.Value.ToString("yy");

                    mm = dteWeavingDate.SelectedDate.Value.ToString("MM");

                    try
                    {
                        TempM = int.Parse(mm);

                        if (TempM <= 9)
                            m = TempM.ToString();
                        else
                        {
                            if (TempM == 10)
                                m = "X";
                            else if (TempM == 11)
                                m = "Y";
                            else if (TempM == 12)
                                m = "Z";
                        }
                    }
                    catch
                    {
                        m = "0";
                    }

                    dd = dteWeavingDate.SelectedDate.Value.ToString("dd");
                }

                WeavingLot = P_LOOM + LOOMNo + yy + m + dd + "0";

                txtWeavingLot.Text = WeavingLot;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(false);
            }
        }

        #endregion

        #region GetWeavingLot_HW

        private void GetWeavingLot_HW()
        {
            try
            {
                string P_LOOM = string.Empty;
                int TempM = 0;
                string LOOMNo = string.Empty;
                int TempLOOMNo = 0;
                string WeavingLot = string.Empty;
                string WeavingDate = string.Empty;

                string yy = string.Empty;
                string mm = string.Empty;
                string m = string.Empty;
                string dd = string.Empty;

                if (cbLoomNo.SelectedValue.ToString() != "")
                    P_LOOM = cbLoomNo.SelectedValue.ToString();

                if (dteWeavingDate.SelectedDate != null)
                {
                    yy = dteWeavingDate.SelectedDate.Value.ToString("yy");

                    mm = dteWeavingDate.SelectedDate.Value.ToString("MM");

                    try
                    {
                        TempM = int.Parse(mm);

                        if (TempM <= 9)
                            m = TempM.ToString();
                        else
                        {
                            if (TempM == 10)
                                m = "X";
                            else if (TempM == 11)
                                m = "Y";
                            else if (TempM == 12)
                                m = "Z";
                        }
                    }
                    catch
                    {
                        m = "0";
                    }

                    dd = dteWeavingDate.SelectedDate.Value.ToString("dd");
                }

                try
                {
                    LOOMNo = _session.GetWEAV_GETCNTCHINALOT(P_LOOM + yy + m + dd);

                    if (LOOMNo != "")
                    {
                        TempLOOMNo = int.Parse(LOOMNo);

                        if (TempLOOMNo <= 9)
                            LOOMNo = "0" + TempLOOMNo.ToString();
                        else
                            LOOMNo = TempLOOMNo.ToString();
                    }
                }
                catch
                {
                    LOOMNo = "00";
                }

                txtLoomNo.Text = LOOMNo;

                WeavingLot = P_LOOM + yy + m + dd +LOOMNo+ "0";

                txtWeavingLot.Text = WeavingLot;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(false);
            }
        }

        #endregion

        #region GetWIDTHWEAVING

        private void GetWIDTHWEAVING(string ITM_WEAVING)
        {
            txtITM_WEAVING.Text = _session.GetWIDTHWEAVING(ITM_WEAVING).Value.ToString("#,##0.##");
        }

        #endregion

        #region ClearInputs

        private void ClearInputs()
        {
            cbLoomNo.SelectedIndex = 0;
            cbShift.SelectedIndex = 0;
            cbItemCode.SelectedValue = null;
            txtLoomNo.Text = string.Empty;
            txtITM_WEAVING.Text = string.Empty;
            dteWeavingDate.SelectedDate = null;

            txtWeavingLot.Text = string.Empty;
            txtLength.Text = string.Empty;
            txtRemark.Text = string.Empty;

            P_WEAVINGLOTOLD = string.Empty;

            chkChinaFabric.IsChecked = false;
            chkTKATFabric.IsChecked = false;

            txtLoomNo.SelectAll();
            txtLoomNo.Focus();
        }

        #endregion

        #region CheckWEAVINGLOT

        private bool CheckWEAVINGLOT()
        {
            try
            {
                if (txtWeavingLot.Text.Length == 9)
                {
                    string P_LOOM = string.Empty;
                    string P_LOOM2 = string.Empty;
                    int Temp = 0;
                    int TempM = 0;
                    string LOOMNo = string.Empty;
                    string WeavingLot = string.Empty;
                    string WeavingDate = string.Empty;

                    string yy = string.Empty;
                    string mm = string.Empty;
                    string m = string.Empty;
                    string dd = string.Empty;

                    if (cbLoomNo.SelectedValue.ToString() != "")
                        P_LOOM = cbLoomNo.SelectedValue.ToString();

                    P_LOOM2 = txtLoomNo.Text;

                    if (P_LOOM2 != "")
                    {
                        try
                        {
                            Temp = int.Parse(P_LOOM2);

                            if (Temp <= 9)
                                LOOMNo = "0" + Temp.ToString();
                            else
                                LOOMNo = Temp.ToString();
                        }
                        catch
                        {
                            LOOMNo = "00";
                        }
                    }

                    if (dteWeavingDate.SelectedDate != null)
                    {
                        yy = dteWeavingDate.SelectedDate.Value.ToString("yy");

                        mm = dteWeavingDate.SelectedDate.Value.ToString("MM");

                        try
                        {
                            TempM = int.Parse(mm);

                            if (TempM <= 9)
                                m = TempM.ToString();
                            else
                            {
                                if (TempM == 10)
                                    m = "X";
                                else if (TempM == 11)
                                    m = "Y";
                                else if (TempM == 12)
                                    m = "Z";
                            }
                        }
                        catch
                        {
                            m = "0";
                        }

                        dd = dteWeavingDate.SelectedDate.Value.ToString("dd");
                    }

                    if (cbLoomNo.SelectedValue.ToString() != "H" && cbLoomNo.SelectedValue.ToString() != "W" && cbLoomNo.SelectedValue.ToString() != "I")
                    {
                        WeavingLot = P_LOOM + LOOMNo + yy + m + dd;

                        if (WeavingLot == (txtWeavingLot.Text.Substring(0, txtWeavingLot.Text.Length - 1)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        WeavingLot = P_LOOM + yy + m + dd + LOOMNo;

                        if (WeavingLot == (txtWeavingLot.Text.Substring(0, txtWeavingLot.Text.Length - 1)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region CheckWEAVINGLOT_HW

        private bool CheckWEAVINGLOT_HW()
        {
            try
            {
                if (txtWeavingLot.Text.Length == 9)
                {
                    string P_LOOM = string.Empty;
                    int TempM = 0;
                    string LOOMNo = string.Empty;
                    int TempLOOMNo = 0;
                    string WeavingLot = string.Empty;
                    string WeavingDate = string.Empty;

                    string yy = string.Empty;
                    string mm = string.Empty;
                    string m = string.Empty;
                    string dd = string.Empty;

                    if (cbLoomNo.SelectedValue.ToString() != "")
                        P_LOOM = cbLoomNo.SelectedValue.ToString();

                    if (dteWeavingDate.SelectedDate != null)
                    {
                        yy = dteWeavingDate.SelectedDate.Value.ToString("yy");

                        mm = dteWeavingDate.SelectedDate.Value.ToString("MM");

                        try
                        {
                            TempM = int.Parse(mm);

                            if (TempM <= 9)
                                m = TempM.ToString();
                            else
                            {
                                if (TempM == 10)
                                    m = "X";
                                else if (TempM == 11)
                                    m = "Y";
                                else if (TempM == 12)
                                    m = "Z";
                            }
                        }
                        catch
                        {
                            m = "0";
                        }

                        dd = dteWeavingDate.SelectedDate.Value.ToString("dd");
                    }

                    if (txtLoomNo.Text != "")
                    {
                        try
                        {
                            LOOMNo = txtLoomNo.Text;

                            if (LOOMNo != "")
                            {
                                TempLOOMNo = int.Parse(LOOMNo);

                                if (TempLOOMNo <= 9)
                                    LOOMNo = "0" + TempLOOMNo.ToString();
                                else
                                    LOOMNo = TempLOOMNo.ToString();
                            }
                        }
                        catch
                        {
                            LOOMNo = "00";
                        }
                    }
                    else
                        LOOMNo = "00";

                    if (cbLoomNo.SelectedValue.ToString() != "H" && cbLoomNo.SelectedValue.ToString() != "W" && cbLoomNo.SelectedValue.ToString() != "I")
                    {
                        WeavingLot = P_LOOM + LOOMNo + yy + m + dd;

                        if (WeavingLot == (txtWeavingLot.Text.Substring(0, txtWeavingLot.Text.Length - 1)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        WeavingLot = P_LOOM + yy + m + dd + LOOMNo;

                        if (WeavingLot == (txtWeavingLot.Text.Substring(0, txtWeavingLot.Text.Length - 1)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Save

        private bool Save()
        {
            bool chkSave = true;

            try
            {
                if (txtWeavingLot.Text != "")
                {
                    if (cbLoomNo.SelectedValue != null)
                    {
                        if (cbLoomNo.SelectedValue.ToString() != "H" && cbLoomNo.SelectedValue.ToString() != "W" && cbLoomNo.SelectedValue.ToString() != "I")
                        {
                            if (CheckWEAVINGLOT() == true)
                            {
                                #region INSERTUPDATEWEAVINGDATA

                                string P_WEAVINGLOTNEW = string.Empty;
                                string P_ITEMWEAVING = string.Empty;
                                decimal? P_LENGHT = null;
                                DateTime? P_WEAVINGDATE = null;

                                string P_LOOM = string.Empty;
                                string P_LOOM2 = string.Empty;
                                int Temp = 0;
                                string LOOMNo = string.Empty;
                                string WeavingLot = string.Empty;

                                string P_SHIFT = string.Empty;
                                decimal? P_WIDTH = null;
                                string P_REMARK = string.Empty;
                                string P_OPERATOR = string.Empty;

                                if (cbLoomNo.SelectedValue.ToString() != "")
                                    P_LOOM = cbLoomNo.SelectedValue.ToString() + txtLoomNo.Text;

                                if (dteWeavingDate.SelectedDate != null)
                                {
                                    try
                                    {
                                        P_WEAVINGDATE = Convert.ToDateTime(dteWeavingDate.SelectedDate.Value.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                    }
                                    catch
                                    {
                                        P_WEAVINGDATE = dteWeavingDate.SelectedDate;
                                    }
                                }

                                P_SHIFT = cbShift.SelectedValue.ToString();

                                P_ITEMWEAVING = cbItemCode.SelectedValue.ToString();

                                if (txtITM_WEAVING.Text != "")
                                    P_WIDTH = decimal.Parse(txtITM_WEAVING.Text);

                                P_WEAVINGLOTNEW = txtWeavingLot.Text;

                                if (string.IsNullOrWhiteSpace(P_WEAVINGLOTOLD))
                                    P_WEAVINGLOTOLD = P_WEAVINGLOTNEW;

                                if (txtLength.Text != "")
                                    P_LENGHT = decimal.Parse(txtLength.Text);

                                P_REMARK = txtRemark.Text;

                                P_OPERATOR = txtOperator.Text;

                                if (P_LOOM != "")
                                {
                                    if (P_WEAVINGDATE != null)
                                    {
                                        if (P_LENGHT != null)
                                        {

                                            if (cbLoomNo.SelectedValue.ToString() != "")
                                                P_LOOM = cbLoomNo.SelectedValue.ToString();

                                            P_LOOM2 = txtLoomNo.Text;

                                            if (P_LOOM2 != "")
                                            {
                                                try
                                                {
                                                    Temp = int.Parse(P_LOOM2);

                                                    if (Temp <= 9)
                                                        LOOMNo = "0" + Temp.ToString();
                                                    else
                                                        LOOMNo = Temp.ToString();
                                                }
                                                catch
                                                {
                                                    LOOMNo = "00";
                                                }
                                            }

                                            if (txtWeavingLot.Text != "")
                                            {
                                                var s = txtWeavingLot.Text;
                                                WeavingLot = s.Substring(0, 3);
                                            }

                                            string pLoom = (P_LOOM + LOOMNo);

                                            if (pLoom == WeavingLot)
                                            {
                                                if (WeavingDataService.Instance.INSERTUPDATEWEAVINGDATA(P_WEAVINGLOTNEW, P_WEAVINGLOTOLD, P_ITEMWEAVING, P_LENGHT, P_WEAVINGDATE
                                                    , pLoom, P_SHIFT, P_WIDTH, P_REMARK, P_OPERATOR) == true)
                                                {
                                                    Preview(P_WEAVINGLOTNEW);
                                                    chkSave = true;
                                                }
                                                else
                                                    chkSave = false;
                                            }
                                            else
                                            {
                                                "Weaving Lot <> Loom No".ShowMessageBox(false);
                                                chkSave = false;
                                            }
                                        }
                                        else
                                        {
                                            "Length isn't null".ShowMessageBox(false);
                                            chkSave = false;
                                        }
                                    }
                                    else
                                    {
                                        "Weaving Date isn't null".ShowMessageBox(false);
                                        chkSave = false;
                                    }
                                }
                                else
                                {
                                    "Loom No isn't null".ShowMessageBox(false);
                                    chkSave = false;
                                }

                                #endregion
                            }
                            else
                            {
                                "Weaving Lot isn't true".ShowMessageBox(false);
                                chkSave = false;
                            }
                        }
                        else
                        {
                            if (txtRemark.Text != "")
                            {
                                if (CheckWEAVINGLOT_HW() == true)
                                {
                                    #region INSERTUPDATEWEAVINGDATA

                                    string P_WEAVINGLOTNEW = string.Empty;
                                    string P_ITEMWEAVING = string.Empty;
                                    decimal? P_LENGHT = null;
                                    DateTime? P_WEAVINGDATE = null;

                                    string P_LOOM = string.Empty;
                                    string P_LOOM2 = string.Empty;
                                    int Temp = 0;
                                    string LOOMNo = string.Empty;
                                    string WeavingLot = string.Empty;

                                    string P_SHIFT = string.Empty;
                                    decimal? P_WIDTH = null;
                                    string P_REMARK = string.Empty;
                                    string P_OPERATOR = string.Empty;

                                    if (cbLoomNo.SelectedValue.ToString() != "")
                                        P_LOOM = cbLoomNo.SelectedValue.ToString() + txtLoomNo.Text;

                                    if (dteWeavingDate.SelectedDate != null)
                                    {
                                        try
                                        {
                                            P_WEAVINGDATE = Convert.ToDateTime(dteWeavingDate.SelectedDate.Value.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                                        }
                                        catch
                                        {
                                            P_WEAVINGDATE = dteWeavingDate.SelectedDate;
                                        }
                                    }

                                    P_SHIFT = cbShift.SelectedValue.ToString();

                                    P_ITEMWEAVING = cbItemCode.SelectedValue.ToString();

                                    if (txtITM_WEAVING.Text != "")
                                        P_WIDTH = decimal.Parse(txtITM_WEAVING.Text);

                                    P_WEAVINGLOTNEW = txtWeavingLot.Text;

                                    if (string.IsNullOrWhiteSpace(P_WEAVINGLOTOLD))
                                        P_WEAVINGLOTOLD = P_WEAVINGLOTNEW;

                                    if (txtLength.Text != "")
                                        P_LENGHT = decimal.Parse(txtLength.Text);

                                    P_REMARK = txtRemark.Text;

                                    P_OPERATOR = txtOperator.Text;

                                    if (P_LOOM != "")
                                    {
                                        if (P_WEAVINGDATE != null)
                                        {
                                            if (P_LENGHT != null)
                                            {

                                                if (cbLoomNo.SelectedValue.ToString() != "")
                                                    P_LOOM = cbLoomNo.SelectedValue.ToString();

                                                P_LOOM2 = txtLoomNo.Text;

                                                if (P_LOOM2 != "")
                                                {
                                                    try
                                                    {
                                                        Temp = int.Parse(P_LOOM2);

                                                        if (Temp <= 9)
                                                            LOOMNo = "0" + Temp.ToString();
                                                        else
                                                            LOOMNo = Temp.ToString();
                                                    }
                                                    catch
                                                    {
                                                        LOOMNo = "00";
                                                    }
                                                }


                                                string pLoom = (P_LOOM + LOOMNo);

                                                if (WeavingDataService.Instance.INSERTUPDATEWEAVINGDATA(P_WEAVINGLOTNEW, P_WEAVINGLOTOLD, P_ITEMWEAVING, P_LENGHT, P_WEAVINGDATE
                                                    , pLoom, P_SHIFT, P_WIDTH, P_REMARK, P_OPERATOR) == true)
                                                {
                                                    Preview(P_WEAVINGLOTNEW);
                                                    chkSave = true;
                                                }
                                                else
                                                    chkSave = false;
                                            }
                                            else
                                            {
                                                "Length isn't null".ShowMessageBox(false);
                                                chkSave = false;
                                            }
                                        }
                                        else
                                        {
                                            "Weaving Date isn't null".ShowMessageBox(false);
                                            chkSave = false;
                                        }
                                    }
                                    else
                                    {
                                        "Loom No isn't null".ShowMessageBox(false);
                                        chkSave = false;
                                    }

                                    #endregion
                                }
                                else
                                {
                                    "Weaving Lot isn't true".ShowMessageBox(false);
                                    chkSave = false;
                                }
                            }
                            else
                            {
                                "Remark isn't null".ShowMessageBox(false);
                                chkSave = false;
                            }
                        }
                    }
                    else
                    {
                        "Loom No isn't null".ShowMessageBox(false);
                        chkSave = false;
                    }
                }
                else
                {
                    "Weaving Lot isn't null".ShowMessageBox(false);
                    chkSave = false;
                }

                return chkSave;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }

        #endregion

        #region Delete

        private bool Delete()
        {
            bool chkDelete = true;

            try
            {
                if (txtWeavingLot.Text != "")
                {
                    if (MessageBox.Show("Do you want to Delete " + txtWeavingLot.Text + " ? ", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        #region WEAV_DELETEWEAVINGLOT

                        string P_WEAVINGLOT = string.Empty;

                        P_WEAVINGLOT = txtWeavingLot.Text;

                        if (WeavingDataService.Instance.WEAV_DELETEWEAVINGLOT(P_WEAVINGLOT) == true)
                        {
                            chkDelete = true;
                        }
                        else
                            chkDelete = false;

                        #endregion
                    }
                }
                else
                {
                    "Weaving Lot isn't null".ShowMessageBox(false);
                    chkDelete = false;
                }

                return chkDelete;
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
                return false;
            }
        }

        #endregion

        #region Report

        private void Report()
        {
            try
            {
                 string P_WEAVINGDATE = string.Empty;

                 if (dteWeavingDate.SelectedDate != null)
                 {
                     IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
                     P_WEAVINGDATE = dteWeavingDate.SelectedDate.Value.ToString("dd/MM/yyyy", culture);

                     #region WEAV_DELETEWEAVINGLOT

                     try
                     {
                         string ChinaFabric = string.Empty;

                         if (chkChinaFabric.IsChecked == true)
                             ChinaFabric = "Y";
                         else if (chkTKATFabric.IsChecked == true)
                             ChinaFabric = "P";
                         else
                             ChinaFabric = "N";

                         if (!string.IsNullOrEmpty(P_WEAVINGDATE))
                         {
                             List<WEAV_GREYROLLDAILYREPORT> lots =
                                    WeavingDataService.Instance.WEAV_GREYROLLDAILYREPORT(P_WEAVINGDATE, ChinaFabric);

                             if (null != lots && lots.Count > 0 && null != lots[0])
                             {
                                 // ConmonReportService
                                 ConmonReportService.Instance.ReportName = "GreyRollDaily";
                                 ConmonReportService.Instance.WEAVINGDATE = P_WEAVINGDATE;
                                 ConmonReportService.Instance.CHINA = ChinaFabric;

                                 var newWindow = new RepMasterForm();
                                 newWindow.ShowDialog();
                             }
                             else
                             {
                                 string msg = "Weaving Date = " + P_WEAVINGDATE + " hadn't data";
                                 msg.ShowMessageBox(true);
                             }
                         }
                         else
                         {
                             "Weaving Date isn't Null".ShowMessageBox(true);
                         }
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
                     }

                     #endregion
                 }
                 else
                 {
                     "Weaving Date isn't null".ShowMessageBox(false);
                 }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string WEAVINGLOT, string ChinaFabric)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "Weaving";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;
                ConmonReportService.Instance.CHINA = ChinaFabric;


                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    ConmonReportService.Instance.Printername = dp.ToString().Trim();
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string WEAVINGLOT)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.ReportName = "Weaving";
                ConmonReportService.Instance.WEAVINGLOT = WEAVINGLOT;

                var newWindow = new RepMasterForm();
                newWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Please Try again later", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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
