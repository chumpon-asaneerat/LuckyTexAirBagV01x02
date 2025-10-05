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
    /// Interaction logic for DefectListWindow.xaml
    /// </summary>
    public partial class DefectListWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public DefectListWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables
        InspectionSession _session = new InspectionSession();
        private Dictionary<string, InspectionDefectCode> _defectCodes = InspectionDataService.Instance.GetDefectCodes(null);
                   
        private string inspecionLotNo = string.Empty;
        private string defectID = string.Empty;
        private string defectCode = string.Empty;
        private string length = string.Empty;
        private string length2 = string.Empty;
        private string position = string.Empty;

        #endregion

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadReason();
            ClearControl();

            if (cmdDelete.IsEnabled == true)
                cmdDelete.IsEnabled = false;

            if (cmdEdit.IsEnabled == true)
                cmdEdit.IsEnabled = false;
        }

        #endregion

        #region Button Events

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (defectID != "")
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

            if (rbReasonList.IsChecked == true)
                reason = cbReason.Text;
            else if (rbReasonText.IsChecked == true)
                reason = txtReason.Text;

            if (userName != "" && pwd != "" && reason != "")
            {
                DeleteDefect(userName, pwd, reason);
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

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            txtDefectCode.Text = defectCode;
            txtLength1.Text = length;
            txtLength2.Text = length2;

            txtPosition.Text = position;

            txtDefectCode.IsReadOnly = false;
            txtLength1.IsReadOnly = false;
            txtLength2.IsReadOnly = false;
            txtPosition.IsReadOnly = false;
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (defectID != "")
            {
                if (!string.IsNullOrEmpty(txtDefectCode.Text))
                {
                    if (!string.IsNullOrEmpty(length) && !string.IsNullOrEmpty(position) && !string.IsNullOrEmpty(txtLength1.Text))
                    {
                        if (MessageBox.Show("Do you want to Edit This Data?", "Edit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {

                            bool chkEdit = false;

                            if (defectCode != txtDefectCode.Text)
                            {
                                chkEdit = true;
                            }

                            if (length != txtLength1.Text)
                            {
                                chkEdit = true;
                            }

                            if (length2 != txtLength2.Text)
                            {
                                chkEdit = true;
                            }

                            if (position != txtPosition.Text)
                            {
                                chkEdit = true;
                            }

                            if (chkEdit == false)
                            {
                                "ข้อมูลไม่ได้ถูกแก้ไข กรุณา แก้ไข ข้อมูล ตามต้องการแล้ว กด Save".ShowMessageBox(true);
                            }
                            else
                            {
                                string ndefectCode = txtDefectCode.Text;

                                decimal? len = null;
                                decimal? len2 = null;
                                decimal? pos = null;
                                decimal? nlength = null;
                                decimal? nlength2 = null;
                                decimal? nposition = null;

                                if (!string.IsNullOrEmpty(length))
                                    len = decimal.Parse(length);

                                if (!string.IsNullOrEmpty(length2))
                                    len2 = decimal.Parse(length2);

                                if (!string.IsNullOrEmpty(position))
                                    pos = decimal.Parse(position);

                                if (!string.IsNullOrEmpty(txtLength1.Text))
                                    nlength = decimal.Parse(txtLength1.Text);

                                if (!string.IsNullOrEmpty(txtLength2.Text))
                                    nlength2 = decimal.Parse(txtLength2.Text);

                                if (!string.IsNullOrEmpty(txtPosition.Text))
                                    nposition = decimal.Parse(txtPosition.Text);

                                try
                                {
                                    EditDefect(defectID, inspecionLotNo, defectCode, len, len2, pos, ndefectCode, nlength, nlength2, nposition);
                                }
                                catch (Exception ex)
                                {
                                    ex.Message.ToString().ShowMessageBox(true);
                                }

                            }
                        }
                    }
                    else
                    {
                        "length and position".ShowMessageBox(true);
                    }
                }
                else
                {
                    "Please select data and click Edit button".ShowMessageBox(true);
                }
            }
            else
            {
                "Please select data".ShowMessageBox(true);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtDefectCode.Text = "";
            txtLength1.Text = "";
            txtLength2.Text = "";
            txtPosition.Text = "";

            txtDefectCode.IsReadOnly = true;
            txtLength1.IsReadOnly = true;
            txtLength2.IsReadOnly = true;
            txtPosition.IsReadOnly = true;
        }

        #endregion

        #region TextBox Handlers

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtDefectCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDefectCode.Text != "")
            {
                string text = txtDefectCode.Text.Trim();

                if (text.Length > 1)
                { 
                    if (null != _defectCodes && _defectCodes.ContainsKey(text))
                    {
                        string defText = _defectCodes[text].DesciptionEN;

                        if (string.IsNullOrEmpty(defText))
                        {
                            "Can't find Defect".ShowMessageBox(true);

                            txtDefectCode.Text = "";
                            txtDefectCode.Focus();
                            txtDefectCode.SelectAll();
                        }
                    }
                    else
                    {
                        "Can't find Defect".ShowMessageBox(true);

                        txtDefectCode.Text = "";
                        txtDefectCode.Focus();
                        txtDefectCode.SelectAll();
                    }
                }
            }
        }

        private void txtDefectCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtDefectCode.Text != "")
            {
                string text = txtDefectCode.Text.Trim();

                if (text.Length == 1)
                {
                    "Can't find Defect".ShowMessageBox(true);

                    txtDefectCode.Text = "";
                    txtDefectCode.Focus();
                    txtDefectCode.SelectAll();
                }
            }
        }

        private void txtPosition_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPosition.Text != "")
            {
                decimal? position = null;

                try
                {
                    position = decimal.Parse(txtPosition.Text);
                }
                catch
                {
                    position = 0;
                }

                if (position > 220)
                {
                    "position invariant > 220".ShowMessageBox(true);

                    txtPosition.Text = "";
                    txtPosition.Focus();
                    txtPosition.SelectAll();
                }
            }
        }

        private void txtUserName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPwd.Focus();
               
            }
        }

        private void txtPwd_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                rbReasonList.Focus();
            }
        }

        #endregion

        #region RadioButton

        private void rbReasonList_Checked(object sender, RoutedEventArgs e)
        {
            cbReason.IsEnabled = true;
        }

        private void rbReasonList_Unchecked(object sender, RoutedEventArgs e)
        {
            cbReason.IsEnabled = false;
        }

        private void rbReasonText_Checked(object sender, RoutedEventArgs e)
        {
            txtReason.IsEnabled = true;
        }

        private void rbReasonText_Unchecked(object sender, RoutedEventArgs e)
        {
            txtReason.IsEnabled = false;
            txtReason.Text = "";
        }

        #endregion

        #region gridInsLots_SelectedCellsChanged

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
        private void gridDefectList_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridDefectList.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridDefectList);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            if (((LuckyTex.Models.InspectionDefectItem)(gridDefectList.CurrentCell.Item)).DefectID != null)
                            {
                                defectID = ((LuckyTex.Models.InspectionDefectItem)(gridDefectList.CurrentCell.Item)).DefectID;
                                defectCode = ((LuckyTex.Models.InspectionDefectItem)(gridDefectList.CurrentCell.Item)).DefectCode;
                                length = ((LuckyTex.Models.InspectionDefectItem)(gridDefectList.CurrentCell.Item)).Length;
                                length2 = ((LuckyTex.Models.InspectionDefectItem)(gridDefectList.CurrentCell.Item)).Length2;
                                position = ((LuckyTex.Models.InspectionDefectItem)(gridDefectList.CurrentCell.Item)).Position;

                                if (defectID != "")
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

        #region private Methods

        #region LoadReason

        private void LoadReason()
        {
            if (cbReason.ItemsSource == null)
            {
                string[] str = new string[] { "Re-judge By Shift Leader", "Found Long Defect" };

                cbReason.ItemsSource = str;
                cbReason.SelectedIndex = 0;
            }
        }

        #endregion

        #region Clear Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearControl()
        {
            ShowContent = true;
            ShowTotal = true;
            ShowLogin = false;

            ShowDelete = true;
            ShowClose = true;
            ShowEdit = true;

            ShowCancel = false;
            ShowOK = false;

            txtUserName.Text = "";
            txtPwd.Password = "";
            txtReason.Text = "";
            rbReasonList.IsChecked = true;
            rbReasonText.IsChecked = false;
            txtReason.IsEnabled = false;

            defectID = "";
            defectCode = "";
            length = "";
            length2 = "";
            position = "";

            txtDefectCode.Text = "";
            txtLength1.Text = "";
            txtLength2.Text = "";
            txtPosition.Text = "";

            txtDefectCode.IsReadOnly = true;
            txtLength1.IsReadOnly = true;
            txtLength2.IsReadOnly = true;
            txtPosition.IsReadOnly = true;

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridDefectList.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridDefectList.SelectedItems.Clear();
            else
                this.gridDefectList.SelectedItem = null;

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
            ShowTotal = false;
            ShowLogin = true;

            ShowDelete = false;
            ShowClose = false;
            ShowEdit = false;

            ShowCancel = true;
            ShowOK = true;

            txtUserName.Focus();

        }
        #endregion

        #region DeleteDefect
        /// <summary>
        /// DeleteDefect
        /// </summary>
        private void DeleteDefect(string userName, string pwd, string reason)
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

                if (inspecionLotNo != "")
                {
                    try
                    {
                        #region Old
                        //decimal? leng = 0;

                        //if (length != "")
                        //{
                        //    leng = decimal.Parse(length);
                        //}

                        //if (InspectionDataService.Instance.DeleteInspectionLotDefect(defectID, defectCode, leng, operators, reason) == true)
                        //{
                        //    List<InspectionDefectItem> defectItems = new List<InspectionDefectItem>();

                        //    BindingOperations.ClearAllBindings(gridDefectList);
                        //    gridDefectList.ItemsSource = defectItems;

                        //    defectItems = InspectionDataService.Instance.GetDefectList(inspecionLotNo, defectID);

                        //    int total = 0;
                        //    if (null != defectItems)
                        //    {
                        //        total = defectItems.Count;
                        //        // Binding.
                        //        if (null != defectItems && defectItems.Count > 0 && null != defectItems[0])
                        //        {
                        //            gridDefectList.ItemsSource = defectItems;
                        //        }
                        //    }

                        //    txtTotal.Text = total.ToString("n0");

                        //    ClearControl();
                        //}
                        //else
                        //{
                        //    "Can't Delete Data".ShowMessageBox(false);
                        //}
                        #endregion

                        DeleteMultiDefect(operators, reason);
                    }
                    catch (Exception ex)
                    {
                        ex.Message.ToString().ShowMessageBox(true);
                    }
                }
                else
                {
                    "defectID is null".ShowMessageBox(false);
                }
            }
        }
        #endregion

        #region DeleteMultiDefect

        private void DeleteMultiDefect(string operators, string reason)
        {
            if (gridDefectList.SelectedItems.Count > 0)
            {
                try
                {
                    decimal? leng = 0;
                    string errorDel = string.Empty;

                    #region gridDefectList

                    foreach (object obj in gridDefectList.SelectedItems)
                    {
                        if (((LuckyTex.Models.InspectionDefectItem)(obj)).DefectID != null)
                        {
                            defectID = ((LuckyTex.Models.InspectionDefectItem)(obj)).DefectID;
                            defectCode = ((LuckyTex.Models.InspectionDefectItem)(obj)).DefectCode;
                            length = ((LuckyTex.Models.InspectionDefectItem)(obj)).Length;

                            if (length != "")
                            {
                                leng = decimal.Parse(length);
                            }

                            if (InspectionDataService.Instance.DeleteInspectionLotDefect(defectID, defectCode, leng, operators, reason) == false)
                            {
                                errorDel += "Defect Code = " + defectCode +"Can't Delete Data"+ "\r\n";
                            }
                        }
                    }

                    if(errorDel != "")
                        errorDel.ShowMessageBox(false);

                    List<InspectionDefectItem> defectItems = new List<InspectionDefectItem>();

                    BindingOperations.ClearAllBindings(gridDefectList);
                    gridDefectList.ItemsSource = defectItems;

                    defectItems = InspectionDataService.Instance.GetDefectList(inspecionLotNo, defectID);

                    int total = 0;
                    if (null != defectItems)
                    {
                        total = defectItems.Count;
                        // Binding.
                        if (null != defectItems && defectItems.Count > 0 && null != defectItems[0])
                        {
                            gridDefectList.ItemsSource = defectItems;
                        }
                    }

                    txtTotal.Text = total.ToString("n0");

                    ClearControl();

                    #endregion
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
        }

        #endregion

        #region EditDefect
        /// <summary>
        /// EditDefect
        /// </summary>
        private void EditDefect(string defectID, string insLot,
            string defectCode, decimal? len1, decimal? len2, decimal? position, string ndefectCode
            , decimal? nlen, decimal? nlen2, decimal? nposition)
        {

            if (defectID != "")
            {
                try
                {

                    if (InspectionDataService.Instance.EditInspectionLotDefect( defectID,  insLot,defectCode, len1,len2, position,
                        ndefectCode, nlen, nlen2, nposition) == true)
                    {
                        List<InspectionDefectItem> defectItems = new List<InspectionDefectItem>();

                        BindingOperations.ClearAllBindings(gridDefectList);
                        gridDefectList.ItemsSource = defectItems;

                        defectItems = InspectionDataService.Instance.GetDefectList(inspecionLotNo, defectID);

                        int total = 0;
                        if (null != defectItems)
                        {
                            total = defectItems.Count;
                            // Binding.
                            if (null != defectItems && defectItems.Count > 0 && null != defectItems[0])
                            {
                                gridDefectList.ItemsSource = defectItems;
                            }
                        }

                        txtTotal.Text = total.ToString("n0");

                        ClearControl();
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
                "defectID is null".ShowMessageBox(false);
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
        /// Gets or sets is show Total panel.
        /// </summary>
        private bool ShowTotal
        {
            get { return totalBorder.Visibility == System.Windows.Visibility.Visible; }
            set
            {
                if (value)
                {
                    totalBorder.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    totalBorder.Visibility = System.Windows.Visibility.Collapsed;
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

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="items">The item source.</param>
        public void Setup(List<InspectionDefectItem> items,string _inspecionLotNo)
        {            
            int total = 0;
            if (null != items)
            {
                total = items.Count;
            }
            txtTotal.Text = total.ToString("n0");
            // Binding.
            this.gridDefectList.ItemsSource = items;

            inspecionLotNo = _inspecionLotNo;
        }

        #endregion

    }
}

