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
    /// Interaction logic for ManualInspectionPage.xaml
    /// </summary>
    public partial class ManualInspectionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ManualInspectionPage()
        {
            InitializeComponent();

            LoadGroup();

            CultureInfo ci = CultureInfo.CreateSpecificCulture("en-US");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
            Thread.CurrentThread.CurrentCulture = ci; 
            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);
        }

        #endregion

        #region Internal Variables

        private InspectionSession _session = new InspectionSession();
        string opera = string.Empty;
        //private InspectionTypes _inspectionType = InspectionTypes.Mass;

        private List<Control> _testCtrls = new List<Control>();

        private DateTime _popupDateTime = DateTime.Now; // Keep popup datetime
        private decimal counter = 0;

        private InspectionTestTempItem insTestTemp = new InspectionTestTempItem();

        InspectionMCItem mcItem = new InspectionMCItem();
        LogInResult loginResult = new LogInResult();

        string _customerID = string.Empty;


        string P_FINISHINGLOT = string.Empty;
        string P_INSPECTIONLOT = string.Empty;
        DateTime? P_StartDate = null;

        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Load

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMacines();

            ClearControl();
            EnabledControl(false);

            if (opera != "")
                txtOperator.Text = opera;

            if (_session != null)
            {
                _session.ReAdjustMode = Models.ReAdjustMode.None;
                _session.ReProcessMode = Models.ReProcessMode.None;

                if (true == rbMassProduction.IsChecked)
                    _session.InspectionType = InspectionTypes.Mass;
                else
                    _session.InspectionType = InspectionTypes.Test;

                _session.State = InspectionSessionState.Idle;

                _session.CurrentUser = new LogInResult();
                _session.Machine = new InspectionMCItem();
            }

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region TextBox Handlers

        #region Common

        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        private void txtItemLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (txtItemLotNo.Text != "")
                {
                    _session.FinishingLotNo = txtItemLotNo.Text;
                    UpdateSessionToControls();

                    if (!string.IsNullOrEmpty(_session.FinishingLotNo))
                    {
                        _customerID = InspectionDataService.Instance.GetCUSTOMERID(_session.FinishingLotNo);
                    }
                    else
                    {
                        _customerID = "";
                    }
                }
                else
                {
                    _session.FinishingLotNo = "";
                    _customerID = "";
                }

                if (txtItemLotNo.Text != "")
                {
                    if (_session.ErrorInspectionId != "")
                    {
                        _session.ErrorInspectionId.ShowMessageBox(false);

                        txtItemLotNo.Text = "";
                        txtItemLotNo.Focus();
                        txtItemLotNo.SelectAll();

                        _session.ErrorInspectionId = "";
                    }
                }
            }
        }

        private void txtInsLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (txtInsLotNo.Text != "")
                {
                    _session.InspecionLotNo = txtInsLotNo.Text;

                    CheckInspectionLot();
                    UpdateSessionToControls();  
                }
                else
                {
                    _session.InspecionLotNo = "";
                }
            }
        }

        private void txtCustomerType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomerType.Text))
            {
                if (_session != null)
                {
                    _session.CustomerType = txtCustomerType.Text;
                }
            }
        }

        private void txtItemCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItemCode.Text))
            {
                if (_session != null)
                {
                    _session.ItemCode = txtItemCode.Text;
                }
            }
        }

        private void txtLoadingType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLoadingType.Text))
            {
                if (_session != null)
                {
                    _session.LOADTYPE = txtLoadingType.Text;
                }
            }
        }

        private void txtItemLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtCustomerType.Focus();
                txtCustomerType.SelectAll();
                e.Handled = true;
            }
        }

        private void txtCustomerType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtItemCode.Focus();
                txtItemCode.SelectAll();
                e.Handled = true;
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLoadingType.Focus();
                txtLoadingType.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLoadingType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtInsLotNo.Focus();
                txtInsLotNo.SelectAll();
                e.Handled = true;
            }
        }

        private void txtInsLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtGLENGHT.Focus();
                txtGLENGHT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtGLENGHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNLENGTH.Focus();
                txtNLENGTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNLENGTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStartHH.Focus();
                txtStartHH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtStartHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtStartMM.Focus();
                txtStartMM.SelectAll();
                e.Handled = true;
            }
        }

        private void txtStartMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtGWEIGHT.Focus();
                txtGWEIGHT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtGWEIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtNWEIGHT.Focus();
                txtNWEIGHT.SelectAll();
                e.Handled = true;
            }
        }

        private void txtNWEIGHT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEndHH.Focus();
                txtEndHH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEndHH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtEndMM.Focus();
                txtEndMM.SelectAll();
                e.Handled = true;
            }
        }

        private void txtEndMM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtGRADE.Focus();
                txtGRADE.SelectAll();
                e.Handled = true;
            }
        }

        private void txtGRADE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtINSPECTEDBY.Focus();
                txtINSPECTEDBY.SelectAll();
                e.Handled = true;
            }
        }

        private void txtINSPECTEDBY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtREMARK.Focus();
                txtREMARK.SelectAll();
                e.Handled = true;
            }
        }

        private void txtREMARK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDefectPos_Long.Focus();
                txtDefectPos_Long.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDefectPos_Long_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLongDefectStart.Focus();
                txtLongDefectStart.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDefectPos_Long_KeyUp(object sender, KeyEventArgs e)
        {
            if (null != _session)
            {
                if (txtDefectPos_Long.Text != "")
                {
                    string text = txtDefectPos_Long.Text.Trim();
                    if (text.Length > 1)
                    {
                        //string defText = _session.CheckDefectCode(text);

                        string defText = CheckDefectCode(text);

                        if (defText != "-")
                        {
                            txtDefectInfo_Long.Text = defText;

                            if (text.Length > 2)
                            {
                                if (_session.CheckPosition(text) == false)
                                {
                                    "position invariant > 220".ShowMessageBox(true);
                                    txtDefectInfo_Long.Text = "-";
                                    txtDefectPos_Long.Text = "";
                                    txtDefectPos_Long.Focus();
                                    txtDefectPos_Long.SelectAll();
                                }
                            }
                        }
                        else
                        {
                            "Can't find Defect/Position".ShowMessageBox(true);
                            txtDefectInfo_Long.Text = "-";
                            txtDefectPos_Long.Text = "";
                            txtDefectPos_Long.Focus();
                            txtDefectPos_Long.SelectAll();
                        }
                    }
                    else
                    {
                        txtDefectInfo_Long.Text = "-";
                    }
                }
                else
                {
                    txtDefectInfo_Long.Text = "-";
                }
            }
        }

        private void txtLongDefectStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLongDefectEnd.Focus();
                txtLongDefectEnd.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLongDefectEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtLENGTH.Focus();
                txtLENGTH.SelectAll();
                e.Handled = true;
            }
        }

        private void txtLENGTH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDensityW.Focus();
                txtDensityW.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDensityW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtDensityF.Focus();
                txtDensityF.SelectAll();
                e.Handled = true;
            }
        }

        private void txtDensityF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWdAll.Focus();
                txtWdAll.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWdAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWdPin.Focus();
                txtWdPin.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWdPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWdCoat.Focus();
                txtWdCoat.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWdCoat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTrimL.Focus();
                txtTrimL.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTrimL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtTrimR.Focus();
                txtTrimR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTrimR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUSet.Focus();
                txtUSet.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWSet.Focus();
                txtWSet.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWSet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFloppyL.Focus();
                txtFloppyL.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFloppyL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtFloppyR.Focus();
                txtFloppyR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtFloppyR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtUActual.Focus();
                txtUActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtUActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtWActual.Focus();
                txtWActual.SelectAll();
                e.Handled = true;
            }
        }

        private void txtWActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHardnessL.Focus();
                txtHardnessL.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHardnessL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHardnessC.Focus();
                txtHardnessC.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHardnessC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtHardnessR.Focus();
                txtHardnessR.SelectAll();
                e.Handled = true;
            }
        }

        private void txtHardnessR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                cmdSaveTestRecord.Focus();
                e.Handled = true;
            }
        }

        private void txtStartHH_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartHH.Text))
            {
                txtStartHH.Text = "00";
            }
            else
            {
                try
                {
                    int hh = int.Parse(txtStartHH.Text);

                    if (hh > 23)
                        txtStartHH.Text = "23";
                }
                catch
                {
                    txtStartHH.Text = "00";
                }

                try
                {
                    if (!string.IsNullOrEmpty(txtStartMM.Text) && dteStartDate.SelectedDate != null)
                        _session.StartDate = DateTime.Parse(dteStartDate.Text + " " + txtStartHH.Text + " : " + txtStartMM.Text);
                }
                catch
                {
                    _session.StartDate = DateTime.Now;
                }
            }
        }

        private void txtStartMM_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartMM.Text))
            {
                txtStartMM.Text = "00";
            }
            else
            {
                try
                {
                    int hh = int.Parse(txtStartMM.Text);
                    if (hh > 59)
                        txtStartMM.Text = "00";
                }
                catch
                {
                    txtStartMM.Text = "00";
                }

                try
                {
                    if (!string.IsNullOrEmpty(txtStartHH.Text) && dteStartDate.SelectedDate != null)
                        _session.StartDate = DateTime.Parse(dteStartDate.Text + " " + txtStartHH.Text + " : " + txtStartMM.Text);
                }
                catch
                {
                    _session.StartDate = DateTime.Now;
                }
            }
        }

        private void txtEndHH_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEndHH.Text))
            {
                txtEndHH.Text = "00";
            }
            else
            {
                try
                {
                    int hh = int.Parse(txtEndHH.Text);
                    if (hh > 23)
                        txtEndHH.Text = "23";
                }
                catch
                {
                    txtEndHH.Text = "00";
                }

                try
                {
                    if (!string.IsNullOrEmpty(txtEndMM.Text) && dteEndDate.SelectedDate != null)
                        _session.EndDate = DateTime.Parse(dteEndDate.Text + " " + txtEndHH.Text + " : " + txtEndMM.Text);
                }
                catch
                {
                    _session.EndDate = DateTime.Now;
                }
            }
        }

        private void txtEndMM_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEndMM.Text))
            {
                txtEndMM.Text = "00";
            }
            else
            {
                try
                {
                    int hh = int.Parse(txtEndMM.Text);
                    if (hh > 59)
                        txtEndMM.Text = "00";
                }
                catch
                {
                    txtEndMM.Text = "00";
                }

                try
                {
                    if (!string.IsNullOrEmpty(txtEndHH.Text) && dteEndDate.SelectedDate != null)
                        _session.EndDate = DateTime.Parse(dteEndDate.Text + " " + txtEndHH.Text + " : " + txtEndMM.Text);
                }
                catch
                {
                    _session.EndDate = DateTime.Now;
                }
            }
        }

        #endregion

        private void cbInspectionMCItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbInspectionMCItem.SelectedValue != null)
            {
                mcItem.MCId = cbInspectionMCItem.SelectedValue.ToString();

                InspectionSession session = InspectionDataService.Instance.GetSession(
                      mcItem, loginResult);

                _session.Machine.MCId = mcItem.MCId;
            }
        }

        private void rbMassProduction_Checked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (true == rbMassProduction.IsChecked)
                    _session.InspectionType = InspectionTypes.Mass;
                else 
                    _session.InspectionType = InspectionTypes.Test;
            }
        }

        private void rbTest_Checked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (true == rbTest.IsChecked)
                    _session.InspectionType = InspectionTypes.Test;
                else
                    _session.InspectionType = InspectionTypes.Mass;
            }
        }

        private void cbGroup_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbGroup.SelectedValue != null)
                    _session.OPERATOR_GROUP = cbGroup.SelectedValue.ToString();
            }
        }

        #region DateTime

        private void dteStartDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dteStartDate.SelectedDate != null)
            {
                DateTime start = DateTime.Now;
                txtStartHH.Text = start.ToString("HH");
                txtStartMM.Text = start.ToString("mm");

                try
                {
                    _session.StartDate = DateTime.Parse(dteStartDate.Text + " " + start.ToString("HH:mm"));
                }
                catch
                {
                    _session.StartDate = DateTime.Now;
                }
            }
            else
            {
                txtStartHH.Text = "00";
                txtStartMM.Text = "00";
            }
        }

        private void dteEndDate_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dteEndDate.SelectedDate != null)
            {
                DateTime end = DateTime.Now;
                txtEndHH.Text = end.ToString("HH");
                txtEndMM.Text = end.ToString("mm");

                try
                {
                    _session.EndDate = DateTime.Parse(dteEndDate.Text + " " + end.ToString("HH:mm"));
                }
                catch
                {
                    _session.EndDate = DateTime.Now;
                }
            }
            else
            {
                txtEndHH.Text = "00";
                txtEndMM.Text = "00";
            }
        }

        #endregion

        #region Button Handlers

        #region cmdBack_Click
        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            PageManager.Instance.Back();
        }
        #endregion

        private void cmdSaveInspectionLot_Click(object sender, RoutedEventArgs e)
        {
            if (SaveInspectionLot() == true)
            {
                EnabledControl(true);

                UpdateInspectionTypeToSession();

                _session.State = InspectionSessionState.Started;
            }
            else
            {
                EnabledControl(false);
                _session.State = InspectionSessionState.None;
            }
        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDefectPos_Long.Text))
                SaveDefect();
            else
                "Defect/Position isn't Null".ShowMessageBox(true);
        }

        private void cmdShowDefectList_Click(object sender, RoutedEventArgs e)
        {
            List<InspectionDefectItem> items = _session.GetDefectList();
            this.ShowDefectListBox(items, _session.InspecionLotNo);
        }

        private void cmdSaveTestRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLENGTH.Text))
                Save100M();
            else
                "Length isn't null".ShowMessageBox(true);
        }

        private void cmd100M_Click(object sender, RoutedEventArgs e)
        {
            // Show tests history records
            if (null != _session)
            {
                List<InspectionTestHistoryItem> items = _session.GetTestHistoryList();
                //this.ShowInspectionRecordBox(items);

                this.ShowInspectionRecordBox(items, _session.InspecionLotNo.ToString(), _session.GetTestID());
            }
        }

        private void cmdFinish_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInsLotNo.Text) && dteStartDate.SelectedDate != null && !string.IsNullOrEmpty(txtItemLotNo.Text))
            {
                //if (!string.IsNullOrEmpty(txtItemLotNo.Text) && !string.IsNullOrEmpty(txtInsLotNo.Text))
                //{
                    //D365_IN_New();
                //}

                if (Finish() == true)
                {
                    "Start D365".Info();

                    D365_IN();

                    "End D365".Info();

                    cmdReport.IsEnabled = true;
                    cmdSaveInspectionLot.IsEnabled = true;
                }
                else
                {
                    "Can't Finish".ShowMessageBox(true);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtInsLotNo.Text))
                {
                    "Item Lot No isn't Null".ShowMessageBox(true);
                }
                else if (string.IsNullOrEmpty(txtItemLotNo.Text))
                {
                    "Lot No. isn't Null".ShowMessageBox(true);
                }
                else if (dteStartDate.SelectedDate == null)
                {
                    "Start Time isn't Null".ShowMessageBox(true);
                }
            }
        }

        private void cmdReport_Click(object sender, RoutedEventArgs e)
        {
            string insLotNo = txtInsLotNo.Text;

            if (!string.IsNullOrWhiteSpace(insLotNo))
            {
                Preview(insLotNo);
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
            cbInspectionMCItem.SelectedValue = null;

            txtItemLotNo.Text = string.Empty;
            txtCustomerType.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtLoadingType.Text = string.Empty;
            txtInsLotNo.Text = string.Empty;
            txtGLENGHT.Text = string.Empty;
            txtNLENGTH.Text = string.Empty;

            txtGWEIGHT.Text = string.Empty;
            txtNWEIGHT.Text = string.Empty;
            txtGRADE.Text = string.Empty;
            txtINSPECTEDBY.Text = string.Empty;
            txtREMARK.Text = string.Empty;
            txtDefectPos_Long.Text = string.Empty;

            txtLongDefectStart.Text = string.Empty;
            txtLongDefectEnd.Text = string.Empty;

            txtLENGTH.Text = string.Empty;

            rbMassProduction.IsChecked = true;
            rbTest.IsChecked = false;

            txtGLENGHT.Text = "";
            txtNLENGTH.Text = "";

            dteStartDate.SelectedDate = null;
            txtStartHH.Text = "";
            txtStartMM.Text = "";

            dteEndDate.SelectedDate = null;
            txtEndHH.Text = "";
            txtEndMM.Text = "";

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

            cmdReport.IsEnabled = false;

            cbGroup.SelectedIndex = 0;

            P_FINISHINGLOT = string.Empty;
            P_INSPECTIONLOT = string.Empty;
            P_StartDate = null;

            PRODID = null;
            HEADERID = null;

            P_LOTNO = string.Empty;
            P_ITEMID = string.Empty;
            P_LOADINGTYPE = string.Empty;
        }

        #endregion

        #region Clear Defect Control
        /// <summary>
        /// ClearControl
        /// </summary>
        private void ClearDefectControl()
        {
            txtDefectInfo_Long.Text = string.Empty;
            txtDefectPos_Long.Text = string.Empty;

            txtLongDefectStart.Text = string.Empty;
            txtLongDefectEnd.Text = string.Empty;
            //txtDefectLength.Text = string.Empty;
        }

        #endregion

        #region Clear Input Controls

        private void ClearTestInputControls()
        {
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
        }

        private void ClearInputControls()
        {
            ClearTestInputControls();
        }

        #endregion

        #region LoadGroup

        private void LoadGroup()
        {
            if (cbGroup.ItemsSource == null)
            {
                string[] str = new string[] { "A", "B", "C" };

                cbGroup.ItemsSource = str;
                cbGroup.SelectedIndex = 0;
            }
        }

        #endregion

        #region LoadMacines
        private void LoadMacines()
        {
            try
            {
                List<InspectionMCItem> instList = InspectionDataService.Instance.GetMachinesData();

                this.cbInspectionMCItem.ItemsSource = instList;
                this.cbInspectionMCItem.DisplayMemberPath = "DisplayName";
                this.cbInspectionMCItem.SelectedValuePath = "MCId";
            }
            catch
            {
                "Please Check Data".ShowMessageBox(true);
            }
        }
        #endregion

        private void UpdateInspectionTypeToSession()
        {
            if (null != _session)
            {
                if (true == rbMassProduction.IsChecked)
                    _session.InspectionType = InspectionTypes.Mass;
                else
                    _session.InspectionType = InspectionTypes.Test;

                _session.ReAdjustMode = ReAdjustMode.None;
                _session.ReProcessMode = ReProcessMode.None;
            }
        }

        private void CheckInspectionLot()
        {
            string insLot = txtInsLotNo.Text;
            if (null != _session)
            {
                if (_session.InspecionLotNo == insLot)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(insLot))
                {
                    _session.InspecionLotNo = insLot;
                }

                if (_session.ReAdjustMode == ReAdjustMode.None &&
                    _session.ReProcessMode == ReProcessMode.None)
                {
                    // No Checked Box selected
                    return;
                }

                if (_session.ReAdjustMode == ReAdjustMode.Diff)
                {
                    // Diff Item Mode
                }

                if (!string.IsNullOrWhiteSpace(insLot) &&
                    string.IsNullOrWhiteSpace(_session.FinishingLotNo))
                {
                    // Show error message
                    "No Inspection Data Found".ShowMessageBox(true);
                    txtInsLotNo.SelectAll();
                    txtInsLotNo.Focus();
                    return;
                }
            }
        }

        private void UpdateSessionToControls()
        {
            if (null == _session)
            {
                return;
            }
            // Update User.
            if (null != _session)
            {
                if (!string.IsNullOrEmpty(txtOperator.Text))
                    _session.CurrentUser.OperatorId = txtOperator.Text;
                else
                    txtOperator.Text = (null != _session.CurrentUser) ?
                        _session.CurrentUser.OperatorId : "-";
            }
            else
            {
                txtOperator.Text = "-";
            }

            if (_session.State == InspectionSessionState.None)
            {

            }
            txtItemLotNo.Text = _session.FinishingLotNo;
            txtItemCode.Text = _session.ItemCode;

            txtGLENGHT.Text = _session.OverallLength.ToString("n2")
                .Replace(",", string.Empty);

            txtNLENGTH.Text = _session.OverallLength.ToString("n2")
                .Replace(",", string.Empty);


            txtInsLotNo.Text = _session.InspecionLotNo;
            txtLoadingType.Text = _session.LOADTYPE;

            txtCustomerType.Text = _session.CustomerType;


            //if ((_session.State == InspectionSessionState.Idle &&
            //     _session.StartDate != DateTime.MinValue &&
            //     _session.StartDate != DateTime.MaxValue) ||
            //    _session.State == InspectionSessionState.Started ||
            //    _session.State == InspectionSessionState.LongDefect)
            //{
            //    try
            //    {
            //        if (!string.IsNullOrEmpty(_session.StartDateString))
            //        {
            //            DateTime sd = DateTime.Parse(_session.StartDateString);
            //            dteStartDate.SelectedDate = sd;
            //            txtStartHH.Text = sd.ToString("HH");
            //            txtStartMM.Text = sd.ToString("mm");
            //        }
            //    }
            //    catch
            //    {
            //        dteStartDate.SelectedDate = null;
            //        txtStartHH.Text = "";
            //        txtStartMM.Text = "";
            //    }
            //}
            //else
            //{
            //    dteStartDate.SelectedDate = null;
            //}

            //if ((_session.State == InspectionSessionState.Idle &&
            //     _session.EndDate != DateTime.MinValue &&
            //     _session.EndDate != DateTime.MaxValue))
            //{
            //    try
            //    {
            //        if (!string.IsNullOrEmpty(_session.StartDateString))
            //        {
            //            DateTime ed = DateTime.Parse(_session.StartDateString);
            //            dteEndDate.SelectedDate = ed;
            //            txtEndHH.Text = ed.ToString("HH");
            //            txtEndMM.Text = ed.ToString("mm");
            //        }
            //    }
            //    catch
            //    {
            //        dteEndDate.SelectedDate = null;
            //        txtEndHH.Text = "";
            //        txtEndMM.Text = "";
            //    }
            //}
            //else
            //{
            //    dteEndDate.SelectedDate = null;
            //    txtEndHH.Text = "";
            //    txtEndMM.Text = "";
            //}

            if (_session.State == InspectionSessionState.None ||
                _session.State == InspectionSessionState.Idle)
            {
                // Clear both data
                txtLongDefectStart.Text = string.Empty;
                txtLongDefectEnd.Text = string.Empty;

                //New Long
                txtDefectPos_Long.Text = string.Empty;
                txtDefectInfo_Long.Text = "-";
            }
        }

        public string CheckDefectCode(string value)
        {
            string result = "-";
            // Check defect.
            if (string.IsNullOrWhiteSpace(value))
            {
                return result;
            }
            string text = value.Trim();
            if (text.Length >= 2)
            {
                string code = text.Substring(0, 2).ToUpper();

                Dictionary<string, InspectionDefectCode> results = InspectionDataService.Instance.GetDefectCodes(code);

                if (null != results)
                {
                    result = results[code].DesciptionEN;
                }
            }
            else if (text.Length < 2)
            {
                // Do nothing
            }
            return result;
        }

        private void EnabledControl(bool status)
        {
            txtDefectPos_Long.IsEnabled = status;

            txtLongDefectStart.IsEnabled = status;
            txtLongDefectEnd.IsEnabled = status;

            txtDensityW.IsEnabled = status;
            txtDensityF.IsEnabled = status;
            txtWdAll.IsEnabled = status;
            txtWdPin.IsEnabled = status;
            txtWdCoat.IsEnabled = status;
            txtTrimL.IsEnabled = status;
            txtTrimR.IsEnabled = status;
            txtUSet.IsEnabled = status;
            txtWSet.IsEnabled = status;
            txtFloppyL.IsEnabled = status;
            txtFloppyR.IsEnabled = status;
            txtUActual.IsEnabled = status;
            txtWActual.IsEnabled = status;
            txtHardnessL.IsEnabled = status;
            txtHardnessC.IsEnabled = status;
            txtHardnessR.IsEnabled = status;
        }

        private bool SaveInspectionLot()
        {
            string result = string.Empty;

            try
            {
                string P_INSLOT = string.Empty;
                string P_ITMCODE = string.Empty;
                string P_FINISHLOT = string.Empty;
                DateTime? P_STARTDATE = null;
                DateTime? P_ENDDATE = null;
                string P_CUSTOMERID = string.Empty;
                string P_PRODUCTTYPEID = string.Empty;
                string P_INSPECTEDBY = string.Empty;
                string P_MCNO = string.Empty;
                string P_CUSTOMERTYPE = string.Empty;
                string P_LOADTYPE = string.Empty;
                Decimal? P_GLENGHT = null;
                Decimal? P_NLENGTH = null;
                string P_GRADE = string.Empty;
                Decimal? P_GWEIGHT = null;
                Decimal? P_NWEIGHT = null;
                string P_REMARK = string.Empty;
                string P_OPERATOR = string.Empty;

                string P_GROUP = string.Empty;

                if (cbInspectionMCItem.SelectedValue != null)
                P_MCNO = cbInspectionMCItem.SelectedValue.ToString();

                P_FINISHLOT = txtItemLotNo.Text;

                P_CUSTOMERTYPE = txtCustomerType.Text;
                P_ITMCODE = txtItemCode.Text;
                P_LOADTYPE = txtLoadingType.Text;
                P_INSLOT = txtInsLotNo.Text;

             //   string instType = (_inspectionType == InspectionTypes.Mass) ?
             //"1" : "2";

             //   P_PRODUCTTYPEID = instType;

                #region P_PRODUCTID

                if (rbMassProduction.IsChecked == true && rbTest.IsChecked == false)
                {
                    P_PRODUCTTYPEID = "1";
                }
                else if (rbMassProduction.IsChecked == false && rbTest.IsChecked == true)
                {
                    P_PRODUCTTYPEID = "2";
                }

                #endregion

                #region P_STARTDATE

                try
                {
                    if (dteStartDate.SelectedDate != null && !string.IsNullOrEmpty(txtStartHH.Text) && !string.IsNullOrEmpty(txtStartMM.Text))
                        P_STARTDATE = DateTime.Parse(dteStartDate.Text + " " + txtStartHH.Text + " : " + txtStartMM.Text);
                    else
                        P_STARTDATE = DateTime.Now;
                }
                catch
                {
                    P_STARTDATE = DateTime.Now;
                }

                _session.StartDate = P_STARTDATE.Value;

                #endregion

                #region P_ENDDATE

                try
                {
                    if (dteEndDate.SelectedDate != null && !string.IsNullOrEmpty(txtEndHH.Text) && !string.IsNullOrEmpty(txtEndMM.Text))
                        P_ENDDATE = DateTime.Parse(dteEndDate.Text + " " + txtEndHH.Text + " : " + txtEndMM.Text);
                    else
                        P_ENDDATE = DateTime.Now;
                }
                catch
                {
                    P_ENDDATE = DateTime.Now;
                }

                _session.EndDate = P_ENDDATE.Value;

                #endregion

                #region P_GLENGHT

                try
                {
                    if (!string.IsNullOrEmpty(txtGLENGHT.Text))
                        P_GLENGHT = decimal.Parse(txtGLENGHT.Text);
                    else
                        P_GLENGHT = 0;
                }
                catch
                {
                    P_GLENGHT = 0;
                }

                #endregion

                #region P_NLENGTH

                try
                {
                    if (!string.IsNullOrEmpty(txtNLENGTH.Text))
                        P_NLENGTH = decimal.Parse(txtNLENGTH.Text);
                    else
                        P_NLENGTH = 0;
                }
                catch
                {
                    P_NLENGTH = 0;
                }

                #endregion

                P_GRADE = txtGRADE.Text;

                #region P_GWEIGHT

                try
                {
                    if (!string.IsNullOrEmpty(txtGWEIGHT.Text))
                        P_GWEIGHT = decimal.Parse(txtGWEIGHT.Text);
                    else
                        P_GWEIGHT = 0;
                }
                catch
                {
                    P_GWEIGHT = 0;
                }

                #endregion

                #region P_NWEIGHT

                try
                {
                    if (!string.IsNullOrEmpty(txtNWEIGHT.Text))
                        P_NWEIGHT = decimal.Parse(txtNWEIGHT.Text);
                    else
                        P_NWEIGHT = 0;
                }
                catch
                {
                    P_NWEIGHT = 0;
                }

                #endregion

                P_REMARK = txtREMARK.Text;
                P_INSPECTEDBY = txtINSPECTEDBY.Text;
                P_OPERATOR = txtOperator.Text;

                P_CUSTOMERID = _customerID;

                if (cbGroup.SelectedValue != null)
                    P_GROUP = cbGroup.SelectedValue.ToString();

                if (!string.IsNullOrEmpty(P_MCNO))
                {
                    if (!string.IsNullOrEmpty(P_FINISHLOT))
                    {
                        if (!string.IsNullOrEmpty(P_ITMCODE))
                        {
                            if (!string.IsNullOrEmpty(P_GRADE))
                            {
                                result = InspectionDataService.Instance.INS_INSERTMANUALINSPECTDATA(P_INSLOT, P_ITMCODE, P_FINISHLOT,
                                           P_STARTDATE, P_ENDDATE, P_CUSTOMERID, P_PRODUCTTYPEID, P_INSPECTEDBY, P_MCNO, P_CUSTOMERTYPE, P_LOADTYPE,
                                           P_GLENGHT, P_NLENGTH, P_GRADE, P_GWEIGHT, P_NWEIGHT, P_REMARK, P_OPERATOR, P_GROUP);

                                if (!string.IsNullOrEmpty(result))
                                {
                                    result.ShowMessageBox(true);
                                    return false;
                                }
                                else
                                {
                                    cmdSaveInspectionLot.IsEnabled = false;
                                    return true;
                                }
                            }
                            else
                            {
                                "Grade isn't Null".ShowMessageBox(true);
                                return false;
                            }
                        }
                        else
                        {
                            "Item isn't Null".ShowMessageBox(true);
                            return false;
                        }
                    }
                    else
                    {
                        "Item Lot No isn't Null".ShowMessageBox(true);
                        return false;
                    }
                }
                else
                {
                    "INS MC No isn't Null".ShowMessageBox(true);
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private void SaveDefect()
        {
            string text = txtDefectPos_Long.Text.Trim();

            if (!string.IsNullOrEmpty(txtLongDefectStart.Text) && string.IsNullOrEmpty(txtLongDefectEnd.Text) )
            {
                if (null != _session)
                {

                    if (null != _session &&
                     _session.State == InspectionSessionState.Started)
                    {
                        if (txtDefectPos_Long.Text != "-" && text.Length > 1)
                        {
                            if (!string.IsNullOrEmpty(txtLongDefectStart.Text))
                            {
                                _session.AddDefectCode(text, txtLongDefectStart.Text);

                                // Update controls.
                                UpdateSessionToControls();

                                txtDefectPos_Long.Text = string.Empty; // Clear
                                txtDefectInfo_Long.Text = "-";

                                ClearDefectControl();
                            }
                        }
                    }
                }
            }
            else if (!string.IsNullOrEmpty(txtLongDefectStart.Text) && !string.IsNullOrEmpty(txtLongDefectEnd.Text) )
            {
                if (null != _session)
                {
                    if (string.IsNullOrWhiteSpace(text) || text.Length <= 2)
                    {
                        // No Defect Pos.
                        "Please Enter Defect Position.".ShowMessageBox(false);

                        // New 
                        txtDefectPos_Long.SelectAll();
                        txtDefectPos_Long.Focus();

                        return;
                    }

                    if (null != _session)
                    {
                        if (!string.IsNullOrEmpty(txtLongDefectStart.Text))
                        {
                            decimal counterVal = Convert.ToDecimal(txtLongDefectStart.Text);
                            _session.StartLongDefect(counterVal);
                        }
                    }

                    if (txtDefectInfo_Long.Text != "-")
                    {
                        if (!string.IsNullOrEmpty(txtLongDefectEnd.Text))
                        {
                            decimal counterVal = Convert.ToDecimal(txtLongDefectEnd.Text);
                            _session.EndLongDefect(counterVal, text);

                            txtDefectPos_Long.Text = string.Empty;
                            txtDefectInfo_Long.Text = "-";

                            ClearDefectControl();
                        }
                    }
                    else
                    {
                        "Can't find Defect/Position".ShowMessageBox(true);
                        txtDefectInfo_Long.Text = "-";
                        txtDefectPos_Long.Text = "";
                        txtDefectPos_Long.Focus();
                        txtDefectPos_Long.SelectAll();
                        return;
                    }
                }
            }
        }

        private void ReadTestControlValues()
        {
            if (null == _session || null == _session.InspectionTests)
                return;
            // Check null instance.
            _session.InspectionTests.CheckInstances();
            // Density
            if (txtDensityW.IsEnabled)
            {
                _session.InspectionTests.Densities.W.Value =
                    !string.IsNullOrWhiteSpace(txtDensityW.Text) ?
                    Convert.ToDecimal(txtDensityW.Text) : new decimal?();
            }
            if (txtDensityF.IsEnabled)
            {
                _session.InspectionTests.Densities.F.Value =
                    !string.IsNullOrWhiteSpace(txtDensityF.Text) ?
                    Convert.ToDecimal(txtDensityF.Text) : new decimal?();
            }
            // Width
            if (txtWdAll.IsEnabled)
            {
                _session.InspectionTests.Widths.All.Value =
                    !string.IsNullOrWhiteSpace(txtWdAll.Text) ?
                    Convert.ToDecimal(txtWdAll.Text) : new decimal?();
            }
            if (txtWdPin.IsEnabled)
            {
                _session.InspectionTests.Widths.Pin.Value =
                    !string.IsNullOrWhiteSpace(txtWdPin.Text) ?
                    Convert.ToDecimal(txtWdPin.Text) : new decimal?();
            }
            if (txtWdCoat.IsEnabled)
            {
                _session.InspectionTests.Widths.Coat.Value =
                    !string.IsNullOrWhiteSpace(txtWdCoat.Text) ?
                    Convert.ToDecimal(txtWdCoat.Text) : new decimal?();
            }
            // Trim
            if (txtTrimL.IsEnabled)
            {
                _session.InspectionTests.Trims.L.Value =
                    !string.IsNullOrWhiteSpace(txtTrimL.Text) ?
                    Convert.ToDecimal(txtTrimL.Text) : new decimal?();
            }
            if (txtTrimR.IsEnabled)
            {
                _session.InspectionTests.Trims.R.Value =
                    !string.IsNullOrWhiteSpace(txtTrimR.Text) ?
                    Convert.ToDecimal(txtTrimR.Text) : new decimal?();
            }
            // Floppy
            if (txtFloppyL.IsEnabled)
            {
                _session.InspectionTests.Floppies.L.strValue = txtFloppyL.Text;
            }
            if (txtFloppyR.IsEnabled)
            {
                _session.InspectionTests.Floppies.R.strValue = txtFloppyR.Text;
            }
            // Hardness
            if (txtHardnessL.IsEnabled)
            {
                _session.InspectionTests.Hardnesses.L.Value =
                    !string.IsNullOrWhiteSpace(txtHardnessL.Text) ?
                    Convert.ToDecimal(txtHardnessL.Text) : new decimal?();
            }
            if (txtHardnessC.IsEnabled)
            {
                _session.InspectionTests.Hardnesses.C.Value =
                    !string.IsNullOrWhiteSpace(txtHardnessC.Text) ?
                    Convert.ToDecimal(txtHardnessC.Text) : new decimal?();
            }
            if (txtHardnessR.IsEnabled)
            {
                _session.InspectionTests.Hardnesses.R.Value =
                    !string.IsNullOrWhiteSpace(txtHardnessR.Text) ?
                    Convert.ToDecimal(txtHardnessR.Text) : new decimal?();
            }
            // Tension - Unwinder
            if (txtUSet.IsEnabled)
            {
                _session.InspectionTests.Unwinders.Set.Value =
                    !string.IsNullOrWhiteSpace(txtUSet.Text) ?
                    Convert.ToDecimal(txtUSet.Text) : new decimal?();
            }
            if (txtUActual.IsEnabled)
            {
                _session.InspectionTests.Unwinders.Actual.Value =
                    !string.IsNullOrWhiteSpace(txtUActual.Text) ?
                    Convert.ToDecimal(txtUActual.Text) : new decimal?();
            }
            // Tension - Winder
            if (txtWSet.IsEnabled)
            {
                _session.InspectionTests.Winders.Set.Value =
                    !string.IsNullOrWhiteSpace(txtWSet.Text) ?
                    Convert.ToDecimal(txtWSet.Text) : new decimal?();
            }
            if (txtWActual.IsEnabled)
            {
                _session.InspectionTests.Winders.Actual.Value =
                    !string.IsNullOrWhiteSpace(txtWActual.Text) ?
                    Convert.ToDecimal(txtWActual.Text) : new decimal?();
            }
        }

        private void Save100M()
        {
            // Save tests
            if (null != _session)
            {
                if (null == _session.InspectionTests)
                {
                    // init instance if null.
                    _session.InspectionTests = new InspectionTests();
                }

                // read all test control value(s) if enabled or not read only.
                ReadTestControlValues();

                decimal counterVal = decimal.Zero;
                // Get Counter
                lock (this)
                {
                    counterVal = counter;
                }

                try
                {
                    if (!string.IsNullOrEmpty(txtLENGTH.Text))
                        counterVal = decimal.Parse(txtLENGTH.Text);
                }
                catch
                {
                    counterVal = counter;
                }

                //_popupDateTime = DateTime.Now;

                _session.AddInspectionTestData(_popupDateTime, counterVal);

                //_session.InspectionTests.Unwinders.Set.Enabled = true;
                //_session.InspectionTests.Unwinders.Actual.Enabled = true;

                //if (_session.InspectionTests.Unwinders.Set.Enabled == true || _session.InspectionTests.Unwinders.Actual.Enabled == true)
                //{
                ConmonReportService.Instance.CmdCountTestData = ConmonReportService.Instance.CmdCountTestData + 1;
                CheckInspectionTestTempItem(_session.InspecionLotNo, ConmonReportService.Instance.CmdCountTestData);
                //}

                // Clear all test data.
                ClearTestInputControls();
            }
        }

        private void CheckInspectionTestTempItem(string insLotNo, int chkCountNew)
        {
            List<InspectionTestTempItem> ins = InspectionDataService.Instance.AddInspectionTestTemptData(insLotNo, chkCountNew, _session.InspectionTests);

            if (ins.Count > 0)
            {
                if (_session.InspectionTests.Unwinders.Set.Enabled == true || _session.InspectionTests.Unwinders.Actual.Enabled == true)
                {
                    if (ins[0].OnlyAddUnwinder == true)
                    {
                        if (ins[0].UnwindersSet != null)
                            txtUSet.Text = ins[0].UnwindersSet.Value.ToString("#,##0.##");

                        if (ins[0].UnwindersActual != null)
                            txtUActual.Text = ins[0].UnwindersActual.Value.ToString("#,##0.##");
                    }
                }
            }
        }

        private bool Finish()
        {
            try
            {
                _session.FinishManual();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region D365

        private void D365_IN()
        {
            try
            {

                P_FINISHINGLOT = txtItemLotNo.Text;
                P_INSPECTIONLOT = txtInsLotNo.Text;

                #region P_STARTDATE

                try
                {
                    if (dteStartDate.SelectedDate != null && !string.IsNullOrEmpty(txtStartHH.Text) && !string.IsNullOrEmpty(txtStartMM.Text))
                        P_StartDate = DateTime.Parse(dteStartDate.Text + " " + txtStartHH.Text + " : " + txtStartMM.Text);
                    else
                        P_StartDate = DateTime.Now;
                }
                catch
                {
                    P_StartDate = DateTime.Now;
                }

                #endregion

                ("FINISHING LOT = " + P_FINISHINGLOT).Info();
                ("INSPECTION LOT = " + P_INSPECTIONLOT).Info();
                ("START DATE = " + P_StartDate.Value.ToString("dd/MM/yy HH:mm")).Info();

                if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                {
                    "Start D365_IN_BPO".Info();

                    if (D365_IN_BPO() == true)
                    {
                        "End D365_IN_BPO Row > 0".Info();
                        if (PRODID != null)
                        {
                            ("PRODID = " + PRODID.ToString()).Info();

                            if (PRODID != 0)
                            {
                                #region D365_IN_ISH
                                "Start D365_IN_ISH".Info();

                                if (D365_IN_ISH(PRODID) == true)
                                {
                                    "End D365_IN_ISH".Info();

                                    if (HEADERID != null)
                                    {
                                        ("HEADERID = " + HEADERID.ToString()).Info();

                                        "Start D365_IN_ISL".Info();
                                        if (D365_IN_ISL(HEADERID) == true)
                                        {
                                            "End D365_IN_ISL".Info();

                                            "Start D365_IN_OPH".Info();
                                            if (D365_IN_OPH(PRODID) == true)
                                            {
                                                "End D365_IN_OPH".Info();

                                                if (HEADERID != null)
                                                {
                                                    "Start D365_IN_OPL".Info();
                                                    if (D365_IN_OPL(HEADERID) == true)
                                                    {
                                                        "End D365_IN_OPL".Info();

                                                        "Start D365_IN_OUH".Info();
                                                        if (D365_IN_OUH(PRODID) == true)
                                                        {
                                                            "End D365_IN_OUH".Info();

                                                            if (HEADERID != null)
                                                            {
                                                                "Start D365_IN_OUL".Info();
                                                                if (D365_IN_OUL(HEADERID) == true)
                                                                {
                                                                    "End D365_IN_OUL".Info();

                                                                    "Send D365 complete".ShowMessageBox();
                                                                }
                                                                else
                                                                {
                                                                    "End D365_IN_OUL Row = 0".Info();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                "HEADERID is null".Info();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            "End D365_IN_OUH Row = 0".Info();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        "End D365_IN_OPL Row = 0".Info();
                                                    }
                                                }
                                                else
                                                {
                                                    "HEADERID is null".Info();
                                                }
                                            }
                                            else
                                            {
                                                "End D365_IN_OPH Row = 0".Info();
                                            }
                                        }
                                        else
                                        {
                                            "End D365_IN_ISL Row = 0".Info();
                                        }
                                    }
                                    else
                                    {
                                        "HEADERID is null".Info();
                                    }
                                }
                                else
                                {
                                    "End D365_IN_ISH Row = 0".Info();
                                }
                                #endregion
                            }
                            else
                            {
                                #region D365_IN_OPH
                                "Start D365_IN_OPH".Info();
                                if (D365_IN_OPH(PRODID) == true)
                                {
                                    "End D365_IN_OPH".Info();

                                    ("HEADERID = " + HEADERID.ToString()).Info();

                                    if (HEADERID != null)
                                    {
                                        "Start D365_IN_OPL".Info();

                                        if (D365_IN_OPL(HEADERID) == true)
                                        {
                                            "End D365_IN_OPL".Info();

                                            "Start D365_IN_OUH".Info();

                                            if (D365_IN_OUH(PRODID) == true)
                                            {
                                                "End D365_IN_OUH".Info();

                                                if (HEADERID != null)
                                                {
                                                    "Start D365_IN_OUL".Info();

                                                    if (D365_IN_OUL(HEADERID) == true)
                                                    {
                                                        "End D365_IN_OUL".Info();

                                                        "Send D365 complete".ShowMessageBox();
                                                    }
                                                    else
                                                    {
                                                        "End D365_IN_OUL Row = 0".Info();
                                                    }
                                                }
                                                else
                                                {
                                                    "HEADERID is null".Info();
                                                }
                                            }
                                            else
                                            {
                                                "End D365_IN_OUH Row = 0".Info();
                                            }
                                        }
                                        else
                                        {
                                            "End D365_IN_OPL Row = 0".Info();
                                        }
                                    }
                                    else
                                    {
                                        "HEADERID is null".Info();
                                    }
                                }
                                else
                                {
                                    "End D365_IN_OPH Row = 0".Info();
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }
                    else
                    {
                        "End D365_IN_BPO Row = 0".Info();
                    }
                }
                else
                {
                    "Inspection Lot is null".ShowMessageBox();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }

        #region D365_IN_BPO
        private bool D365_IN_BPO()
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_BPOData> results = new List<ListD365_IN_BPOData>();

                results = D365DataService.Instance.D365_IN_BPO(P_FINISHINGLOT, P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                            PRODID = Convert.ToInt64(results[i].PRODID);
                        else
                            PRODID = null;

                        if (!string.IsNullOrEmpty(results[i].LOTNO))
                            P_LOTNO = results[i].LOTNO;
                        else
                            P_LOTNO = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].ITEMID))
                            P_ITEMID = results[i].ITEMID;
                        else
                            P_ITEMID = string.Empty;

                        if (!string.IsNullOrEmpty(results[i].LOADINGTYPE))
                            P_LOADINGTYPE = results[i].LOADINGTYPE;
                        else
                            P_LOADINGTYPE = string.Empty;

                        if (PRODID != null)
                        {
                            if (PRODID != 0)
                            {
                                chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION);

                                if (!string.IsNullOrEmpty(chkError))
                                {
                                    chkError.Err();
                                    chkError.ShowMessageBox();
                                    chkD365 = false;
                                    break;
                                }
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_BPO Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_ISH
        private bool D365_IN_ISH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_ISHData> results = new List<D365_IN_ISHData>();

                results = D365DataService.Instance.D365_IN_ISH(P_FINISHINGLOT, P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_ISH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_ISL
        private bool D365_IN_ISL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_ISLData> results = new List<ListD365_IN_ISLData>();

                results = D365DataService.Instance.D365_IN_ISL(P_FINISHINGLOT, P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string issDate = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].ISSUEDATE != null)
                            issDate = results[i].ISSUEDATE.Value.ToString("yyyy-MM-dd");
                        else
                            issDate = string.Empty;

                        chkError = D365DataService.Instance.Insert_ABISL(HEADERID, results[i].LINENO, "N", 0, issDate, results[i].ITEMID, results[i].STYLEID, results[i].QTY, results[i].UNIT, results[i].SERIALID);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_ISL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OPH
        private bool D365_IN_OPH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_OPHData> results = new List<D365_IN_OPHData>();

                results = D365DataService.Instance.D365_IN_OPH(P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OPH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OPL
        private bool D365_IN_OPL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_OPLData> results = new List<ListD365_IN_OPLData>();

                results = D365DataService.Instance.D365_IN_OPL(P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        chkError = D365DataService.Instance.Insert_ABOPL(HEADERID, results[i].LINENO, "N", 0, results[i].PROCQTY, results[i].OPRNO, results[i].OPRID, results[i].MACHINENO, results[i].STARTDATETIME, results[i].ENDDATETIME);

                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OPL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OUH
        private bool D365_IN_OUH(long? PRODID)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_OUHData> results = new List<D365_IN_OUHData>();

                results = D365DataService.Instance.D365_IN_OUH(P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OUH Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #region D365_IN_OUL
        private bool D365_IN_OUL(long? HEADERID)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_OULData> results = new List<ListD365_IN_OULData>();

                results = D365DataService.Instance.D365_IN_OUL(P_FINISHINGLOT, P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string outputDate = string.Empty;
                    int? finish = null;

                    foreach (var row in results)
                    {
                        if (results[i].OUTPUTDATE != null)
                            outputDate = results[i].OUTPUTDATE.Value.ToString("yyyy-MM-dd");
                        else
                            outputDate = string.Empty;

                        if (results[i].FINISH != null)
                            finish = Convert.ToInt32(results[i].FINISH);
                        else
                            finish = 0;

                        chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);


                        if (!string.IsNullOrEmpty(chkError))
                        {
                            chkError.Err();
                            chkError.ShowMessageBox();
                            chkD365 = false;
                            break;
                        }

                        i++;
                    }
                }
                else
                {
                    "D365_IN_OUL Row = 0".Info();
                }

                return chkD365;
            }
            catch (Exception ex)
            {
                ex.Err();
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
        #endregion

        #endregion

        #region GetDefaultPrinter

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int size);

        #endregion

        #region Print

        private void Print(string insLotNo)
        {
            try
            {
                Windows.ShiftRemarkWindow window = new Windows.ShiftRemarkWindow();
                window.Setup(insLotNo, _session.StartDate);

                if (window.ShowDialog() == true)
                {
                    if (window.useRemark == true)
                    {
                        ConmonReportService.Instance.ReportName = "InspectionRemark";

                        if (window.useShiftRemark == true)
                        {
                            ConmonReportService.Instance.UseShiftRemark = true;
                        }
                        else
                        {
                            ConmonReportService.Instance.UseShiftRemark = false;
                        }
                    }
                    else
                    {
                        ConmonReportService.Instance.ReportName = "Inspection";
                    }
                }
                else
                {
                    ConmonReportService.Instance.ReportName = "Inspection";
                }

                //if (MessageBox.Show("Show Remark on report?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    ConmonReportService.Instance.ReportName = "InspectionRemark";
                //}
                //else
                //{
                //    ConmonReportService.Instance.ReportName = "Inspection";
                //}

                string CmdString = string.Empty;
                ConmonReportService.Instance.CmdString = insLotNo;

                StringBuilder dp = new StringBuilder(256);
                int size = dp.Capacity;
                if (GetDefaultPrinter(dp, ref size))
                {
                    DataControl.ClassData.Report rep = new DataControl.ClassData.Report();
                    rep.PrintByPrinter(dp.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Preview

        private void Preview(string insLotNo)
        {
            try
            {
                Windows.ShiftRemarkWindow window = new Windows.ShiftRemarkWindow();
                window.Setup(insLotNo, _session.StartDate);

                if (window.ShowDialog() == true)
                {
                    if (window.useRemark == true)
                    {
                        ConmonReportService.Instance.ReportName = "InspectionRemark";

                        if (window.useShiftRemark == true)
                        {
                            ConmonReportService.Instance.UseShiftRemark = true;
                        }
                        else
                        {
                            ConmonReportService.Instance.UseShiftRemark = false;
                        }
                    }
                    else
                    {
                        ConmonReportService.Instance.ReportName = "Inspection";
                    }
                }
                else
                {
                    ConmonReportService.Instance.ReportName = "Inspection";
                }

                //if (MessageBox.Show("Show Remark on report?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                //{
                //    ConmonReportService.Instance.ReportName = "InspectionRemark";
                //}
                //else
                //{
                //    ConmonReportService.Instance.ReportName = "Inspection";
                //}

                // ConmonReportService
                ConmonReportService.Instance.CmdString = insLotNo;

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

