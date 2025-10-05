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
using System.IO;
using System.Collections;

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for OperatorPage.xaml
    /// </summary>
    public partial class OperatorPage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public OperatorPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables
        private OperatorSession _session = new OperatorSession();

        private List<ProcessItem> processList = null;
        private List<PositonItem> positonList = null;

        #endregion

        #region Private Methods

        #region Inspection Session methods

        private void InitSession()
        {
            if (null != _session)
            {
                _session.OnStateChanged += new EventHandler(_session_OnStateChanged);
            }
            else
            {

            }
        }

        private void ReleaseSession()
        {
            if (null != _session)
            {
                _session.OnStateChanged -= new EventHandler(_session_OnStateChanged);
            }
            _session = null;
        }

        void _session_OnStateChanged(object sender, EventArgs e)
        {
            if (null != _session)
            {

            }
        }

        #endregion

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadStatus();
            LoadTitle();
            LoadProcess();
            LoadPositon();

            ClearControl();
            EnabledControl(false);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ReleaseSession();
        }

        #endregion

        #region TextBox

        #region txtOperatorID_LostFocus

        private void txtOperatorID_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.OperatorID = txtOperatorID.Text;
                _session.UserName = _session.OperatorID;
            }
        }

        #endregion

        #region txtFname_LostFocus

        private void txtFname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.FName = txtFname.Text;
            }
        }

        #endregion

        #region txtLName_LostFocus

        private void txtLName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.LName = txtLName.Text;
            }
        }

        #endregion

        #region txtPassword_LostFocus

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.Password = txtPassword.Text;
            }
        }

        #endregion

        #region txtOperatorID_KeyDown

        private void txtOperatorID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cbStatus.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region txtFname_KeyDown

        private void txtFname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLName.Focus();
                txtLName.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region txtLName_KeyDown

        private void txtLName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cbProcess.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region txtPassword_KeyDown

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #endregion

        #region chkWeb_Checked

        private void chkWeb_Checked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (chkWeb.IsChecked == true)
                    _session.Web = "1";
            }
        }

        #endregion

        #region chkWeb_Unchecked

        private void chkWeb_Unchecked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (chkWeb.IsChecked == false)
                    _session.Web = "0";
            }
        }

        #endregion

        #region chkWIP_Checked

        private void chkWIP_Checked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (chkWIP.IsChecked == true)
                    _session.WIP = "1";
            }
        }

        #endregion

        #region chkWIP_Unchecked

        private void chkWIP_Unchecked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (chkWIP.IsChecked == false)
                    _session.WIP = "0";
            }
        }

        #endregion

        #region comboBox

        #region cbStatus_LostFocus

        private void cbStatus_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbStatus.Text != "")
                {
                    if (cbStatus.Text == "Active")
                        _session.DeleteFlag = "1";
                    else if (cbStatus.Text == "Delete")
                        _session.DeleteFlag = "0";
                }
                else
                {
                    _session.DeleteFlag = string.Empty;
                }
            }
        }

        #endregion

        #region cbTitle_LostFocus

        private void cbTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbTitle.Text != "")
                {
                    _session.Title = cbTitle.Text;
                }
                else
                {
                    _session.Title = string.Empty;
                }
            }
        }

        #endregion

        #region cbProcess_LostFocus

        private void cbProcess_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbProcess.Text != "")
                {
                    _session.ProcessID = cbProcess.SelectedValue.ToString();
                }
                else
                {
                    _session.ProcessID = string.Empty;
                }
            }
        }

        #endregion

        #region cbPositon_LostFocus

        private void cbPositon_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbPositon.Text != "")
                {
                    _session.PositionLevel = cbPositon.SelectedValue.ToString();
                }
                else
                {
                    _session.PositionLevel = string.Empty;
                }
            }
        }

        #endregion

        #endregion

        #region gridOperator_SelectedCellsChanged

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

        private void gridOperator_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (gridOperator.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(gridOperator);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {
                            EnabledControl(false);

                            #region OPERATORID

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).OPERATORID != null)
                            {
                                _session.OperatorID = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).OPERATORID;
                                _session.UserName = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).USERNAME;
                                
                                if (cmdEdit.IsEnabled == false)
                                    cmdEdit.IsEnabled = true;
                            }
                            else
                            {
                                if (cmdEdit.IsEnabled == true)
                                    cmdEdit.IsEnabled = false;

                                _session.OperatorID = string.Empty;
                                _session.UserName = string.Empty;
                            }

                            #endregion

                            #region TITLE

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).TITLE != null)
                            {
                                _session.Title = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).TITLE;                 
                            }
                            else
                            {
                                _session.Title = string.Empty;
                            }

                            #endregion

                            #region FNAME

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).FNAME != null)
                            {
                                _session.FName = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).FNAME;
                            }
                            else
                            {
                                _session.FName = string.Empty;
                            }

                            #endregion

                            #region LNAME

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).LNAME != null)
                            {
                                _session.LName = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).LNAME;
                            }
                            else
                            {
                                _session.LName = string.Empty;
                            }

                            #endregion

                            #region PROCESSID

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).PROCESSID != null)
                            {
                                _session.ProcessID = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).PROCESSID;
                            }
                            else
                            {
                                _session.ProcessID = string.Empty;
                            }

                            #endregion

                            #region POSITIONLEVEL

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).POSITIONLEVEL != null)
                            {
                                _session.PositionLevel = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).POSITIONLEVEL;
                            }
                            else
                            {
                                _session.PositionLevel = string.Empty;
                            }

                            #endregion

                            #region PASSWORD

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).PASSWORD != null)
                            {
                                _session.Password = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).PASSWORD;
                            }
                            else
                            {
                                _session.Password = string.Empty;
                            }

                            #endregion

                            #region DELETEFLAG

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).DELETEFLAG != null)
                            {
                                _session.DeleteFlag = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).DELETEFLAG;
                            }
                            else
                            {
                                _session.DeleteFlag = string.Empty;
                            }

                            #endregion

                            #region WEB

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).WEB != null)
                            {
                                _session.Web = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).WEB;
                            }
                            else
                            {
                                _session.Web = string.Empty;
                            }

                            #endregion

                            #region WIP

                            if (((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).WIP != null)
                            {
                                _session.WIP = ((LuckyTex.Models.Operator_SearchData)(gridOperator.CurrentCell.Item)).WIP;
                            }
                            else
                            {
                                _session.WIP = string.Empty;
                            }

                            #endregion

                            txtOperatorID.Text = _session.OperatorID;
                            cbTitle.Text = _session.Title;
                            txtFname.Text = _session.FName;
                            txtLName.Text = _session.LName;
                            cbProcess.SelectedValue = _session.ProcessID;
                            cbPositon.SelectedValue = _session.PositionLevel;
                            txtPassword.Text = _session.Password;

                            if (_session.DeleteFlag != "")
                            {
                                if (_session.DeleteFlag != "1")
                                    cbStatus.Text = "Delete";
                                else if (_session.DeleteFlag == "1")
                                    cbStatus.Text = "Active";
                            }

                            if (_session.Web != "")
                            {
                                if (_session.Web == "0")
                                    chkWeb.IsChecked = false;
                                else if (_session.Web == "1")
                                    chkWeb.IsChecked = true;
                            }

                            if (_session.WIP != "")
                            {
                                if (_session.WIP == "0")
                                    chkWIP.IsChecked = false;
                                else if (_session.WIP == "1")
                                    chkWIP.IsChecked = true;
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

        #region Button Handlers

        #region cmdSearch_Click

        private void cmdSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadOperatorData();
        }

        #endregion

        #region cmdNew_Click

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            ClearControl();
            EnabledControl(true);
        }

        #endregion

        #region cmdEdit_Click

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            EnabledControl(true);
            txtOperatorID.IsEnabled = false;
        }

        #endregion

        #region cmdSave_Click

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            SaveData();
        }

        #endregion

        #region cmdBack_Click

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }

        #endregion

        #endregion

        #region private Methods

        #region LoadStatus

        private void LoadStatus()
        {
            if (cbStatus.ItemsSource == null)
            {
                string[] str = new string[] { "Active", "Delete"};

                cbStatus.ItemsSource = str;
                cbStatus.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadTitle

        private void LoadTitle()
        {
            if (cbTitle.ItemsSource == null)
            {
                string[] str = new string[] { "Mr.", "Mrs.","Ms." };

                cbTitle.ItemsSource = str;
                cbTitle.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadProcess

        private void LoadProcess()
        {
            try
            {
                processList = InspectionDataService.Instance.GetProcessData();

                this.cbProcess.ItemsSource = processList;
                this.cbProcess.DisplayMemberPath = "PROCESSDESCRIPTION";
                this.cbProcess.SelectedValuePath = "PROCESSID";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadPositon

        private void LoadPositon()
        {
            try
            {
                positonList = InspectionDataService.Instance.GetPositonData();

                this.cbPositon.ItemsSource = positonList;
                this.cbPositon.DisplayMemberPath = "POSITIONNAME";
                this.cbPositon.SelectedValuePath = "POSITIONLEVEL";
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
            txtOperatorID.Text = "";
            txtFname.Text = "";
            txtLName.Text = "";
            txtPassword.Text = "";

            cbStatus.Text = "";
            cbTitle.Text = "";
            cbProcess.Text = "";
            cbPositon.Text = "";
            chkWeb.IsChecked = false;
            chkWIP.IsChecked = false;
            
            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridOperator.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridOperator.SelectedItems.Clear();
            else
                this.gridOperator.SelectedItem = null;

            gridOperator.ItemsSource = null;

            txtOperatorID.Focus();

            if (_session != null)
            {
                _session.New();
            }

            cmdEdit.IsEnabled = false;
        }

        #endregion

        #region EnabledControl

        private void EnabledControl(bool chkEnabled)
        {
            txtOperatorID.IsEnabled = chkEnabled;
            txtFname.IsEnabled = chkEnabled;
            txtLName.IsEnabled = chkEnabled;
            txtPassword.IsEnabled = chkEnabled;

            cbStatus.IsEnabled = chkEnabled;
            cbTitle.IsEnabled = chkEnabled;
            cbPositon.IsEnabled = chkEnabled;
            cbProcess.IsEnabled = chkEnabled;
        }

        #endregion

        #region LoadOperatorData

        private void LoadOperatorData()
        {
            string _operatorID = string.Empty;
            string _fName = string.Empty;
            string _lName = string.Empty;
            string _password = string.Empty;

            string _title = string.Empty;
            string _process = string.Empty;
            string _positon = string.Empty;
            string _status = string.Empty;
            //string _web = string.Empty;
            //string _wip = string.Empty;

            if (chkAll.IsChecked == false)
            {
                if (txtOperatorID.Text != "")
                {
                    _operatorID = "%" + txtOperatorID.Text + "%";
                }
                if (txtFname.Text != "")
                {
                    _fName = "%" + txtFname.Text + "%";
                }
                if (txtLName.Text != "")
                {
                    _lName = "%" + txtLName.Text + "%";
                }
                if (txtPassword.Text != "")
                {
                    _password = txtPassword.Text;
                }
                if (cbTitle.SelectedValue != null)
                {
                    _title = cbTitle.SelectedValue.ToString();
                }
                if (cbProcess.SelectedValue != null)
                {
                    _process = cbProcess.SelectedValue.ToString();
                }
                if (cbPositon.SelectedValue != null)
                {
                    _positon = cbPositon.SelectedValue.ToString();
                }
                if (cbStatus.SelectedValue != null)
                {
                    _status = cbStatus.SelectedValue.ToString();
                }
            }

            List<Operator_SearchData> lots = new List<Operator_SearchData>();

            lots = MasterDataService.Instance.Operator_SearchDataList(_operatorID, _title, _fName, _lName, _process, _positon);

            if (null != lots && lots.Count > 0 && null != lots[0])
            {
                gridOperator.ItemsSource = lots;
            }
            else
            {
                gridOperator.ItemsSource = null;
            }
        }

        #endregion

        #region SaveData

        private void SaveData()
        {
            if (_session != null)
            {
                if (_session.OperatorID == "")
                {
                    "Operator ID not null".ShowMessageBox(true);
                    return;
                }
                if (_session.FName == "")
                {
                    "Name not null".ShowMessageBox(true);
                    return;
                }
                if (_session.Password == "")
                {
                    "Password not null".ShowMessageBox(true);
                    return;
                }
                if (_session.ProcessID == "")
                {
                    "แผนก not null".ShowMessageBox(true);
                    return;
                }
                if (_session.PositionLevel == "")
                {
                    "Position not null".ShowMessageBox(true);
                    return;
                }

                string msgSave = _session.Save();

                if (msgSave == "Y")
                {
                    "Save Data Complete".ShowMessageBox(false);

                    LoadOperatorData();
                }
                else
                {
                    msgSave = "Error on Save Data please try again: " +msgSave;
                    msgSave.ShowMessageBox(true);
                }
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
            // Init Session
            InitSession();
            if(_session != null)
            {
                _session.CreatedBy = user;
            }
        }

        #endregion

    }
}
