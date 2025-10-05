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

#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for WeavingSpecificMCStopWindow.xaml
    /// </summary>
    public partial class WeavingSpecificMCStopWindow : Window
    {
        #region Constructror
        
        /// <summary>
        /// Constructror.
        /// </summary>
        public WeavingSpecificMCStopWindow()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

        }

        #endregion

        #region Internal Variables

        private string BEAMLOT = string.Empty;
        private string DOFFNO = null;
        private string LOOMNO = string.Empty;
        private string WEAVELOT = string.Empty;

        private string OperatorText = string.Empty;
        private bool chkSave = false;
        private string ResultText = string.Empty;
        private WeavingSession _session = new WeavingSession();


        public string gridLOOMNO { get; set; }
        public decimal? gridDOFFNO { get; set; }
        public string gridBEAMROLL { get; set; }
        public string gridDEFECT { get; set; }
        public DateTime? gridDATE { get; set; }

        #endregion

        #region Window_Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDefect();

            if (BEAMLOT != "")
                txtBEAMLOT.Text = BEAMLOT;

            if (DOFFNO != "")
                txtDOFFNO.Text = DOFFNO;

            if (OperatorText != "")
                txtOperator.Text = OperatorText;

            ClearControl();

            if (!string.IsNullOrEmpty(BEAMLOT) && (!string.IsNullOrEmpty(DOFFNO)))
                LoadStopReason();

            txtLength.Focus();
            txtLength.SelectAll();
        }

        #endregion

        #region Button Events

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            chkSave = false;
            this.DialogResult = false;
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to Delete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Delete();
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

        private void txtLength_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtPosition.SelectAll();
                txtPosition.Focus();

                e.Handled = true;
            }
        }

        private void txtPosition_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);

            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtRemark.SelectAll();
                txtRemark.Focus();

                e.Handled = true;
            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                HrTxtTime.SelectAll();
                HrTxtTime.Focus();

                e.Handled = true;
            }
        }

        private void txtOperator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSave.Focus();
                e.Handled = true;
            }
        }

        private void HrTxtTime_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(HrTxtTime.Text))
                {
                    int hour = int.Parse(HrTxtTime.Text);

                    if (hour >= 24)
                        hour = 23;

                    if (hour.ToString().Length == 1)
                    {
                        HrTxtTime.Text = "0" + hour.ToString();
                    }
                    else
                    {
                        HrTxtTime.Text = hour.ToString();
                    }
                }
                else
                {
                    HrTxtTime.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                HrTxtTime.Text = "0";
            }
        }

        private void HrTxtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                MinTxtTime.SelectAll();
                MinTxtTime.Focus();
                e.Handled = true;
            }
        }

        private void MinTxtTime_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(MinTxtTime.Text))
                {
                    int min = int.Parse(MinTxtTime.Text);

                    if (min >= 60)
                        min = 59;

                    if (min.ToString().Length == 1)
                    {
                        MinTxtTime.Text = "0" + min.ToString();
                    }
                    else
                    {
                        MinTxtTime.Text = min.ToString();
                    }
                }
                else
                {
                    MinTxtTime.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                MinTxtTime.Text = "0";
            }
        }

        private void MinTxtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                SecTxtStart.SelectAll();
                SecTxtStart.Focus();
                e.Handled = true;
            }
        }

        private void SecTxtStart_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SecTxtStart.Text))
                {
                    int sec = int.Parse(SecTxtStart.Text);

                    if (sec >= 60)
                        sec = 59;

                    if (sec.ToString().Length == 1)
                    {
                        SecTxtStart.Text = "0" + sec.ToString();
                    }
                    else
                    {
                        SecTxtStart.Text = sec.ToString();
                    }
                }
                else
                {
                    SecTxtStart.Text = "0";
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
                SecTxtStart.Text = "0";
            }
        }

        private void SecTxtStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtOperator.SelectAll();
                txtOperator.Focus();
                e.Handled = true;
            }
        }

        #endregion

        #region gridWeaving_SelectedCellsChanged

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
                            gridLOOMNO = string.Empty;
                            gridDOFFNO = null;
                            gridBEAMROLL = string.Empty;
                            gridDEFECT = string.Empty;
                            gridDATE = null;

                            if (!string.IsNullOrEmpty(((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).LOOMNO))
                            {
                                gridLOOMNO = ((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).LOOMNO;
                            }
                            else
                            {
                                gridLOOMNO = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).DOFFNO != null)
                            {
                                gridDOFFNO = ((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).DOFFNO;
                            }
                            else
                            {
                                gridDOFFNO = null;
                            }

                            if (!string.IsNullOrEmpty(((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).BEAMERROLL))
                            {
                                gridBEAMROLL = ((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).BEAMERROLL;
                            }
                            else
                            {
                                gridBEAMROLL = string.Empty;
                            }

                            if (!string.IsNullOrEmpty(((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).DEFECTCODE))
                            {
                                gridDEFECT = ((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).DEFECTCODE;
                            }
                            else
                            {
                                gridDEFECT = string.Empty;
                            }

                            if (((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).STOPDATE != null)
                            {
                                gridDATE = ((LuckyTex.Models.WEAV_GETMCSTOPLISTBYDOFFNO)(gridWeaving.CurrentCell.Item)).STOPDATE;
                            }
                            else
                            {
                                gridDATE = null;
                            }
                        }
                    }
                }
                else
                {
                    gridLOOMNO = string.Empty;
                    gridDOFFNO = null;
                    gridBEAMROLL = string.Empty;
                    gridDEFECT = string.Empty;
                    gridDATE = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region LoadDefect

        private void LoadDefect()
        {
            try
            {
                List<WEAV_DEFECTLIST> items = _session.GetDefectData();

                this.cbDefect.ItemsSource = items;
                //this.cbDefect.DisplayMemberPath = "DESCRIPTION";
                this.cbDefect.DisplayMemberPath = "DefectCodeName";
                this.cbDefect.SelectedValuePath = "DEFECTCODE";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }

        #endregion

        #region LoadStopReason
        private void LoadStopReason()
        {
            if (!string.IsNullOrEmpty(LOOMNO) && !string.IsNullOrEmpty(DOFFNO) && !string.IsNullOrEmpty(BEAMLOT))
            {
                List<WEAV_GETMCSTOPLISTBYDOFFNO> lots = new List<WEAV_GETMCSTOPLISTBYDOFFNO>();

                #region doff

                decimal? doff = null;
                try
                {
                    doff = decimal.Parse(DOFFNO);
                }
                catch
                {
                    doff = null;
                }

                #endregion

                lots = WeavingDataService.Instance.WEAV_GETMCSTOPLISTBYDOFFNO(LOOMNO, doff, BEAMLOT, WEAVELOT);

                if (null != lots && lots.Count > 0 && null != lots[0])
                {
                    gridWeaving.ItemsSource = lots;
                }
                else
                {
                    gridWeaving.ItemsSource = null;
                }

                gridLOOMNO = string.Empty;
                gridDOFFNO = null;
                gridBEAMROLL = string.Empty;
                gridDEFECT = string.Empty;
                gridDATE = null;
            }
            else
            {
                gridWeaving.ItemsSource = null;
            }
        }
        #endregion

        #region ClearControl

        private void ClearControl()
        {
            txtLength.Text = string.Empty;
            txtPosition.Text = string.Empty;
            txtRemark.Text = string.Empty;
            cbDefect.SelectedValue = null;

            DateTime dt = DateTime.Now;

            dteSTARTDATE.SelectedDate = dt;

            HrTxtTime.Text = dt.ToString("HH");
            MinTxtTime.Text = dt.ToString("mm");
            SecTxtStart.Text = dt.ToString("ss");

            // ใช้สำหรับ clear ค่าที่เลือกใน DataGrid
            if (this.gridWeaving.SelectionMode != DataGridSelectionMode.Single) //if the Extended mode
                this.gridWeaving.SelectedItems.Clear();
            else
                this.gridWeaving.SelectedItem = null;

            gridWeaving.ItemsSource = null;

            gridLOOMNO = string.Empty;
            gridDOFFNO = null;
            gridBEAMROLL = string.Empty;
            gridDEFECT = string.Empty;
            gridDATE = null;

            txtLength.SelectAll();
            txtLength.Focus();
        }

        #endregion

        #region Save
        private void Save()
        {
            if (!string.IsNullOrEmpty(txtLength.Text) && !string.IsNullOrEmpty(txtOperator.Text))
            {

                string P_DEFECT = string.Empty;
                decimal? P_LENGTH = null;
                decimal? P_POSITION = null;

                string P_REMARK = string.Empty;
                string P_OPERATOR = string.Empty;
                string result = string.Empty;

                decimal? doff = null;
                DateTime? P_DATE = null;

                #region doff

                if (!string.IsNullOrEmpty(DOFFNO))
                {
                    try
                    {
                        doff = decimal.Parse(DOFFNO);
                    }
                    catch
                    {
                        doff = null;
                    }
                }

                #endregion

                #region P_DEFECT

                if (cbDefect.SelectedValue != null)
                    P_DEFECT = cbDefect.SelectedValue.ToString();

                #endregion

                if (!string.IsNullOrEmpty(P_DEFECT))
                {
                    #region P_LENGTH

                    if (!string.IsNullOrEmpty(txtLength.Text))
                    {
                        try
                        {
                            P_LENGTH = decimal.Parse(txtLength.Text);
                        }
                        catch
                        {
                            P_LENGTH = 0;
                        }
                    }

                    #endregion

                    #region P_POSITION

                    if (!string.IsNullOrEmpty(txtPosition.Text))
                    {
                        try
                        {
                            P_POSITION = decimal.Parse(txtPosition.Text);
                        }
                        catch
                        {
                            P_POSITION = 0;
                        }
                    }

                    #endregion

                    #region P_REMARK

                    if (!string.IsNullOrEmpty(txtRemark.Text))
                    {
                        P_REMARK = txtRemark.Text;
                    }

                    #endregion

                    #region P_OPERATOR

                    if (!string.IsNullOrEmpty(txtOperator.Text))
                        P_OPERATOR = txtOperator.Text;

                    #endregion

                    if (dteSTARTDATE.SelectedDate != null)
                    {
                        try
                        {
                            P_DATE = DateTime.Parse(dteSTARTDATE.SelectedDate.Value.ToString("dd/MM/yyyy") + " " + HrTxtTime.Text + " : " + MinTxtTime.Text + " : " + SecTxtStart.Text);
                        }
                        catch
                        {
                            P_DATE = DateTime.Now;
                        }
                    }

                    result = WeavingDataService.Instance.WEAV_INSERTMCSTOP(LOOMNO, doff, BEAMLOT, WEAVELOT,
                          P_DEFECT, P_LENGTH, P_POSITION, P_REMARK, P_OPERATOR, P_DATE);

                    if (string.IsNullOrEmpty(result))
                    {
                        ClearControl();

                        if (!string.IsNullOrEmpty(BEAMLOT) && doff != null)
                            LoadStopReason();

                        //chkSave = true;
                        //this.DialogResult = true;
                    }
                }
                else
                {
                    MessageBox.Show("Reason or Other isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtLength.Text))
                {
                    MessageBox.Show("Length isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    txtLength.SelectAll();
                    txtLength.Focus();
                    txtLength.Focus();
                }
                else if (string.IsNullOrEmpty(txtOperator.Text))
                {
                    MessageBox.Show("Operator isn't Null", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    txtOperator.SelectAll();
                    txtOperator.Focus();
                    txtOperator.Focus();
                }
            }
        }
        #endregion

        #region Delete
        private void Delete()
        {
            if (!string.IsNullOrEmpty(gridLOOMNO) && !string.IsNullOrEmpty(gridBEAMROLL))
            {
                if (WeavingDataService.Instance.WEAV_DELETEMCSTOP(gridLOOMNO, gridDOFFNO, gridBEAMROLL, gridDEFECT, gridDATE) == true)
                {
                    LoadStopReason();
                }
                else
                {
                    "Can't Delete".ShowMessageBox(true);
                }
            }
        }
        #endregion

        #region Public Properties

        public bool ChkStatus { get { return chkSave; } }
        public string Result { get { return ResultText; } }

        public void Setup(string P_LOOMNO, string P_DOFFNO, string P_BEAMROLL, string P_WEAVELOT, string operatorText)
        {
            LOOMNO = P_LOOMNO;
            DOFFNO = P_DOFFNO;
            BEAMLOT = P_BEAMROLL;
            WEAVELOT = P_WEAVELOT;

            OperatorText = operatorText;
        }

        #endregion 
       
    }
}
