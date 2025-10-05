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
using System.Windows.Shapes;

using System.Windows.Navigation;
using LuckyTex.Services;
using System.Collections;

using NLib;
using LuckyTex.Models;

#endregion


namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for InspectionRecordWindow.xaml
    /// </summary>
    public partial class InspectionRecordWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public InspectionRecordWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        List<InspectionTestHistoryItem> results = new List<InspectionTestHistoryItem>();

        private List<Control> _testCtrls = new List<Control>();

        private string insLotNo = string.Empty;
        private string tesId = string.Empty;

        private string testRecordID = string.Empty;
        private string stdLength = string.Empty;
        private decimal? length = null;
        private decimal? actLength = null;

        private string densityW = string.Empty;
        private string densityF = string.Empty;

        private string widthAll = string.Empty;
        private string widthCoat = string.Empty;
        private string widthPin = string.Empty;

        private string trimL = string.Empty;
        private string trimR = string.Empty;

        private string floppyL = string.Empty;
        private string floppyR = string.Empty;

        private string unwinderSet = string.Empty;
        private string unwinderActual = string.Empty;

        private string winderSet = string.Empty;
        private string winderActual = string.Empty;

        private string hardnessL = string.Empty;
        private string hardnessC = string.Empty;
        private string hardnessR = string.Empty;

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ClearControl();

            if (cmdDelete.IsEnabled == true)
                cmdDelete.IsEnabled = false;

            if (cmdEdit.IsEnabled == true)
                cmdEdit.IsEnabled = false;
        }

        #endregion

        #region grid100MRecord_SelectedCellsChanged

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

        private void grid100MRecord_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (grid100MRecord.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(grid100MRecord);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).TESTRECORDID != "")
                            {
                                testRecordID = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).TESTRECORDID;
                                {
                                    stdLength = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).STDLength;

                                    if (!string.IsNullOrEmpty(stdLength))
                                        length = decimal.Parse(((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).STDLength);
                                }

                                if (((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).ActualLength != "")
                                    actLength = decimal.Parse(((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).ActualLength);

                                densityW = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).DensityW;
                                densityF = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).DensityF;

                                widthAll = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).WidthAll;
                                widthCoat = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).WidthCoat;
                                widthPin = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).WidthPin;

                                trimL = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).TrimL;
                                trimR = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).TrimR;

                                floppyL = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).FloppyL;
                                floppyR = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).FloppyR;

                                unwinderSet = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).UnwinderSet;
                                unwinderActual = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).UnwinderActual;

                                winderSet = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).WinderSet;
                                winderActual = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).WinderActual;

                                hardnessL = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).HardnessL;
                                hardnessC = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).HardnessC;
                                hardnessR = ((LuckyTex.Models.InspectionTestHistoryItem)(grid100MRecord.CurrentCell.Item)).HardnessR;

                                if (stdLength != null)
                                {
                                    if (cmdDelete.IsEnabled == false)
                                        cmdDelete.IsEnabled = true;

                                    if (cmdEdit.IsEnabled == false)
                                        cmdEdit.IsEnabled = true;
                                }
                            }
                            else
                            {
                                if (cmdDelete.IsEnabled == true)
                                    cmdDelete.IsEnabled = false;

                                if (cmdEdit.IsEnabled == true)
                                    cmdEdit.IsEnabled = false;
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

        #region Button Events

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            EnableTestControls();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (testRecordID != "")
            {
                ClearDeleteControl();
            }
            else
            {
                MessageBox.Show("Please Select Data");
            }
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            string userName = string.Empty;
            string pwd = string.Empty;
            string reason = string.Empty;

            userName = txtUserName.Text;
            pwd = txtPwd.Password;
            reason = txtReason.Text;

            if (userName != "" && pwd != "" && reason != "")
            {
                Delete100MRecord(userName, pwd, reason);
            }
            else
            {
                if (userName == "")
                {
                    "User Name isn't Null".ShowMessageBox(true);
                    txtUserName.Focus();
                }
                else if (pwd == "")
                {
                    "Password isn't Null".ShowMessageBox(true);
                    txtPwd.Focus();
                }
                else if (reason == "")
                {
                    "Reason isn't Null".ShowMessageBox(true);
                    txtReason.Focus();
                }
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cmdSaveTestRecord_Click(object sender, RoutedEventArgs e)
        {
            Edit100MRecord();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
        }

        #endregion

        #region TextBox Handlers

        private void txtUserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    txtPwd.SelectAll();
                    txtPwd.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtPwd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter || e.Key == Key.Return)
                {
                    txtReason.SelectAll();
                    txtReason.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().Err();
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        private void txtTotalLen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;
            }
        }

        private void testInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                FocusNextTestControls(sender);
                e.Handled = true;
            }
        }

        private void txtWdAll_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtWdAll.Text != "")
                {
                    decimal? nwidthAll = null;

                    nwidthAll = decimal.Parse(txtWdAll.Text);

                    if (nwidthAll > 220)
                    {
                        "Width Can't Over 220".ShowMessageBox(true);

                        txtWdAll.Text = "220";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtWdPin_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtWdPin.Text != "")
                {
                    decimal? nwidthPin = null;

                    nwidthPin = decimal.Parse(txtWdPin.Text);

                    if (nwidthPin > 220)
                    {
                        "Width Can't Over 220".ShowMessageBox(true);

                        txtWdPin.Text = "220";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        private void txtWdCoat_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtWdCoat.Text != "")
                {
                    decimal? nwidthCoat = null;

                    nwidthCoat = decimal.Parse(txtWdCoat.Text);

                    if (nwidthCoat > 220)
                    {
                        "Width Can't Over 220".ShowMessageBox(true);

                        txtWdCoat.Text = "220";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }
        }

        #endregion

        #region Enable Input Controls

        public void EnableTestControls()
        {
            // Density
            #region Density

            if (densityW != "")
                txtDensityW.IsEnabled = true;
            else
                txtDensityW.IsEnabled = false;

            if (densityF != "")
                txtDensityF.IsEnabled = true;
            else
                txtDensityF.IsEnabled = false;

            #endregion

            // Width
            #region Width

            if (widthAll != "")
                txtWdAll.IsEnabled = true;
            else
                txtWdAll.IsEnabled = false;

            if (widthPin != "")
                txtWdPin.IsEnabled = true;
            else
                txtWdPin.IsEnabled = false;

            if (widthCoat != "")
                txtWdCoat.IsEnabled = true;
            else
                txtWdCoat.IsEnabled = false;

            #endregion

            // Trim
            #region Trim

            if (trimL != "")
                txtTrimL.IsEnabled = true;
            else
                txtTrimL.IsEnabled = false;

            if (trimR != "")
                txtTrimR.IsEnabled = true;
            else
                txtTrimR.IsEnabled = false;

            #endregion

            // Floppy
            #region Floppy

            if (floppyL != null)
                txtFloppyL.IsEnabled = true;
            else
                txtFloppyL.IsEnabled = false;

            if (floppyR != null)
                txtFloppyR.IsEnabled = true;
            else
                txtFloppyR.IsEnabled = false;

            #endregion

            // Hardness
            #region Hardness
            if (hardnessL != null)
                txtHardnessL.IsEnabled = true;
            else
                txtHardnessL.IsEnabled = false;

            if (hardnessC != null)
                txtHardnessC.IsEnabled = true;
            else
                txtHardnessC.IsEnabled = false;

            if (hardnessR != null)
                txtHardnessR.IsEnabled = true;
            else
                txtHardnessR.IsEnabled = false;

            #endregion

            // Tension - Unwinder
            #region Unwinder

            if (unwinderSet != "")
                txtUSet.IsEnabled = true;
            else
                txtUSet.IsEnabled = false;

            if (unwinderActual != "")
                txtUActual.IsEnabled = true;
            else
                txtUActual.IsEnabled = false;

            #endregion

            // Tension - Winder
            #region Winder

            if (winderSet != "")
                txtWSet.IsEnabled = true;
            else
                txtWSet.IsEnabled = false;

            if (winderActual != "")
                txtWActual.IsEnabled = true;
            else
                txtWActual.IsEnabled = false;

            #endregion


            txtSTDLength.Text = length.ToString();

            txtActualLength.Text = actLength.ToString();

            txtDensityW.Text = densityW;
            txtDensityF.Text = densityF;
            txtWdAll.Text = widthAll;
            txtWdPin.Text = widthPin;
            txtWdCoat.Text = widthCoat;
            txtTrimL.Text = trimL;
            txtTrimR.Text = trimR;
            txtFloppyL.Text = floppyL;
            txtFloppyR.Text = floppyR;
            txtUSet.Text = unwinderSet;
            txtUActual.Text = unwinderActual;
            txtWSet.Text = winderSet;
            txtWActual.Text = winderActual;

            txtHardnessL.Text = hardnessL;
            txtHardnessC.Text = hardnessC;
            txtHardnessR.Text = hardnessR;

            // Buttons
            cmdSaveTestRecord.IsEnabled = true;

            txtDensityW.Focus();
            ShowContentEdit = true;

            ShowContent = false;
            ShowLogin = false;

            ShowDelete = false;
            ShowClose = true;
            ShowEdit = false;

            ShowCancel = false;
            ShowOK = false;
        }

        #endregion

        #region Clear Input Controls

        private void ClearTestInputControls()
        {
            txtSTDLength.Text = string.Empty;
            txtActualLength.Text = string.Empty;

            // Density
            txtDensityW.Text = string.Empty;
            txtDensityF.Text = string.Empty;
            // Width
            txtWdAll.Text = string.Empty;
            txtWdPin.Text = string.Empty;
            txtWdCoat.Text = string.Empty;
            // Trim
            txtTrimL.Text = string.Empty;
            txtTrimR.Text = string.Empty;
            // Floppy
            txtFloppyL.Text = string.Empty;
            txtFloppyR.Text = string.Empty;
            // Hardness
            txtHardnessL.Text = string.Empty;
            txtHardnessC.Text = string.Empty;
            txtHardnessR.Text = string.Empty;
            // Tension - Unwinder
            txtUSet.Text = string.Empty;
            txtUActual.Text = string.Empty;
            // Tension - Winder
            txtWSet.Text = string.Empty;
            txtWActual.Text = string.Empty;

            densityW = string.Empty;
            densityF = string.Empty;

            widthAll = string.Empty;
            widthCoat = string.Empty;
            widthPin = string.Empty;

            trimL = string.Empty;
            trimR = string.Empty;

            floppyL = string.Empty;
            floppyR = string.Empty;

            unwinderSet = string.Empty;
            unwinderActual = string.Empty;

            winderSet = string.Empty;
            winderActual = string.Empty;

            hardnessL = string.Empty;
            hardnessC = string.Empty;
            hardnessR = string.Empty;
        }

        #endregion

        #region Focus Controls

        private void FocusNextTestControls(object sender)
        {
            TextBox tb = null;
            if (sender.GetType() != typeof(TextBox) ||
                null == _testCtrls ||
                _testCtrls.Count <= 0)
                return;

            TextBox _textbox = null;
            int iCurr = 0;
            int iMax = _testCtrls.Count;
            for (int i = 0; i < iMax; ++i)
            {
                _textbox = _testCtrls[i] as TextBox;
                if (null != _textbox && _textbox == sender)
                {
                    iCurr = i;
                    break;
                }
            }

            for (int i = iCurr + 1; i < iMax; ++i)
            {
                _textbox = _testCtrls[i] as TextBox;
                if (null != _textbox && _textbox.IsEnabled)
                {
                    tb = _textbox; // control is enable so can focus.
                    break;
                }
            }

            if (null != tb)
            {
                tb.Focus();
                tb.SelectAll();
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
            ShowContent = true;
            ShowLogin = false;
            ShowContentEdit = false;

            ShowDelete = true;
            ShowClose = true;
            ShowEdit = true;

            ShowCancel = false;
            ShowOK = false;

            txtUserName.Text = "";
            txtPwd.Password = "";
            txtReason.Text = "";

            testRecordID = "";
            stdLength = "";
            length = null;
            actLength = null;

            ClearTestInputControls();

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.grid100MRecord.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.grid100MRecord.SelectedItems.Clear();
            else
                this.grid100MRecord.SelectedItem = null;

            if (cmdDelete.IsEnabled == true)
                cmdDelete.IsEnabled = false;

            if (cmdEdit.IsEnabled == true)
                cmdEdit.IsEnabled = false;
        }
        /// <summary>
        /// ClearDeleteControl
        /// </summary>
        private void ClearDeleteControl()
        {
            ShowContent = false;
            ShowLogin = true;
            ShowContentEdit = false;

            ShowDelete = false;
            ShowClose = false;
            ShowEdit = false;

            ShowCancel = true;
            ShowOK = true;

            txtUserName.Focus();
        }
        #endregion

        #region Delete100MRecord
        /// <summary>
        /// DeleteDefect
        /// </summary>
        private void Delete100MRecord(string userName, string pwd, string reason)
        {

            int processId = 8; // for inspection
            string operators = LuckyTex.Services.UserDataService.Instance
                .GetOperatorsDelete(userName, pwd, processId);

            if (null == operators || operators == "")
            {
                "This User is not Authorize.".ShowMessageBox(true);
                return;
            }
            else
            {

                if (testRecordID != "" )
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(stdLength))
                        {
                            // ปรับโดยส่งค่า length เข้าไปแทน
                            if (InspectionDataService.Instance.Ins_Delete100MRecord(testRecordID, length.ToString(), actLength, operators, reason) == true)
                            {
                                List<InspectionTestHistoryItem> defectItems = new List<InspectionTestHistoryItem>();

                                BindingOperations.ClearAllBindings(grid100MRecord);
                                grid100MRecord.ItemsSource = defectItems;

                                defectItems = InspectionDataService.Instance.GetTestHistoryList(insLotNo, tesId);

                                if (null != defectItems && defectItems.Count > 0 && null != defectItems[0])
                                {
                                    grid100MRecord.ItemsSource = defectItems;
                                }

                                ClearControl();
                            }
                            else
                            {
                                "Can't Delete Data".ShowMessageBox(false);
                            }
                        }
                        else
                        {
                            "Can't Delete Data".ShowMessageBox(false);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString().ShowMessageBox(true);
                    }
                }
                else
                {
                    "testRecordID is null".ShowMessageBox(false);
                }
            }
        }
        #endregion

        #region Edit100MRecord
        /// <summary>
        /// Edit100MRecord
        /// </summary>
        private void Edit100MRecord()
        {
            if (testRecordID != "")
            {
                try
                {
                    decimal? nactLength = actLength;

                    decimal? ndenw = null;
                    if (txtDensityW.Text != "")
                        ndenw = decimal.Parse(txtDensityW.Text);

                    decimal? ndenf = null;
                    if (txtDensityF.Text != "")
                        ndenf = decimal.Parse(txtDensityF.Text);

                    decimal? nwidthAll = null;
                    if (txtWdAll.Text != "")
                        nwidthAll = decimal.Parse(txtWdAll.Text);

                    decimal? nwidthPin = null;
                    if (txtWdPin.Text != "")
                        nwidthPin = decimal.Parse(txtWdPin.Text);

                    decimal? nwidthCoat = null;
                    if (txtWdCoat.Text != "")
                        nwidthCoat = decimal.Parse(txtWdCoat.Text);

                    decimal? ntrimL = null;
                    if (txtTrimL.Text != "")
                        ntrimL = decimal.Parse(txtTrimL.Text);

                    decimal? ntrimR = null;
                    if (txtTrimR.Text != "")
                        ntrimR = decimal.Parse(txtTrimR.Text);

                    //decimal? nfloppyR = null;
                    //if (txtFloppyR.Text != "")
                    //    nfloppyR = decimal.Parse(txtFloppyR.Text);

                    string nfloppyL = string.Empty;
                    if (txtFloppyL.Text != "")
                        nfloppyL = txtFloppyL.Text;

                    string nfloppyR = string.Empty;
                    if (txtFloppyR.Text != "")
                        nfloppyR = txtFloppyR.Text;

                    decimal? nunwinderSet = null;
                    if (txtUSet.Text != "")
                        nunwinderSet = decimal.Parse(txtUSet.Text);

                    decimal? nunwinderAct = null;
                    if (txtUActual.Text != "")
                        nunwinderAct = decimal.Parse(txtUActual.Text);

                    decimal? nwinderSet = null;
                    if (txtWSet.Text != "")
                        nwinderSet = decimal.Parse(txtWSet.Text);

                    decimal? nwinderAct = null;
                    if (txtWActual.Text != "")
                        nwinderAct = decimal.Parse(txtWActual.Text);

                    decimal? hardnessesL = null;
                    if (txtHardnessL.Text != "")
                        hardnessesL = decimal.Parse(txtHardnessL.Text);

                    decimal? hardnessesC = null;
                    if (txtHardnessC.Text != "")
                        hardnessesC = decimal.Parse(txtHardnessC.Text);

                    decimal? hardnessesR = null;
                    if (txtHardnessR.Text != "")
                        hardnessesR = decimal.Parse(txtHardnessR.Text);

                    if (!string.IsNullOrEmpty(stdLength))
                    {
                        // ปรับโดยส่งค่า length เข้าไปแทน
                        if (InspectionDataService.Instance.INS_EDIT100TESTRECORD(testRecordID, insLotNo, length.ToString(), actLength, nactLength, ndenw, ndenf
                            , nwidthAll, nwidthPin, nwidthCoat, ntrimL, ntrimR, nfloppyL, nfloppyR, nunwinderSet, nunwinderAct, nwinderSet, nwinderAct
                            , hardnessesL, hardnessesC, hardnessesR) == true)
                        {
                            List<InspectionTestHistoryItem> defectItems = new List<InspectionTestHistoryItem>();

                            BindingOperations.ClearAllBindings(grid100MRecord);
                            grid100MRecord.ItemsSource = defectItems;

                            defectItems = InspectionDataService.Instance.GetTestHistoryList(insLotNo, tesId);

                            if (null != defectItems && defectItems.Count > 0 && null != defectItems[0])
                            {
                                grid100MRecord.ItemsSource = defectItems;
                            }

                            ClearControl();
                        }
                        else
                        {
                            "Can't Edit Data".ShowMessageBox(false);
                        }
                    }
                    else
                    {
                        "Can't Edit Data".ShowMessageBox(false);
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
            else
            {
                "testRecordID is null".ShowMessageBox(false);
            }
        }
        #endregion

        #region Show Control

        /// <summary>
        /// Gets or sets is show Content panel.
        /// </summary>
        private bool ShowContent
        {
            get { return contentBorder.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    contentBorder.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    contentBorder.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Gets or sets is show Content panel.
        /// </summary>
        private bool ShowLogin
        {
            get { return deleteBorder.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    deleteBorder.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    deleteBorder.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Gets or sets is show Edit panel.
        /// </summary>
        private bool ShowContentEdit
        {
            get { return contentEdit.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    contentEdit.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    contentEdit.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Gets or sets is show Delete panel.
        /// </summary>
        private bool ShowDelete
        {
            get { return cmdDelete.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    cmdDelete.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    cmdDelete.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Gets or sets is show Close panel.
        /// </summary>
        private bool ShowClose
        {
            get { return cmdClose.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    cmdClose.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    cmdClose.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Gets or sets is show Edit panel.
        /// </summary>
        private bool ShowEdit
        {
            get { return cmdEdit.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    cmdEdit.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    cmdEdit.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Gets or sets is show OK panel.
        /// </summary>
        private bool ShowOK
        {
            get { return cmdOk.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    cmdOk.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    cmdOk.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        /// <summary>
        /// Gets or sets is show Cancel panel.
        /// </summary>
        private bool ShowCancel
        {
            get { return cmdCancel.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    cmdCancel.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    cmdCancel.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }
        
        #endregion

        #endregion

        #region Public Methods
        //Old
        //public void Setup(List<InspectionTestHistoryItem> items)
        //{
        //    // Binding.
        //    this.DataContext = items;
        //}

        /// <summary>
        /// Setup.
        /// </summary>
        public void Setup(List<InspectionTestHistoryItem> items ,string inspecionLotNo, string testId)
        {
            insLotNo = inspecionLotNo;
            tesId = testId;
            this.DataContext = items;
        }

        #endregion
    }
}

