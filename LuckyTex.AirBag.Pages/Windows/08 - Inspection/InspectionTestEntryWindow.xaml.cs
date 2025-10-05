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

using DataControl.ClassData;
#endregion

namespace LuckyTex.Windows
{
    /// <summary>
    /// Interaction logic for InspectionTestEntryWindow.xaml
    /// </summary>
    public partial class InspectionTestEntryWindow : Window
    {
        #region Constructor
        
        public InspectionTestEntryWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private List<Control> _testCtrls = new List<Control>();

        private InspectionSession _session = null;

        private DateTime _popupDateTime = DateTime.Now; // Keep popup datetime
        private decimal counter = 0;
        #endregion

        #region Private Methods

        #region Validate Input State

        private void ValidateInputState()
        {
            if (null != _session)
            {
                switch (_session.State)
                {
                    case InspectionSessionState.Idle:
                        EnableTestControls(false);
                        break;
                    case InspectionSessionState.Started:
                        EnableTestControls();
                        break;
                    case InspectionSessionState.LongDefect:
                        EnableTestControls(false);
                        break;
                    default: // None
                        EnableTestControls(false);
                        break;
                }
            }
        }

        #endregion

        #region Init/Release Input Controls

        private void InitTestControls()
        {
            if (null == _testCtrls)
                _testCtrls = new List<Control>();

            // Density
            _testCtrls.Add(txtDensityW);
            _testCtrls.Add(txtDensityF);
            // Width
            _testCtrls.Add(txtWdAll);
            _testCtrls.Add(txtWdPin);
            _testCtrls.Add(txtWdCoat);
            // Trim
            _testCtrls.Add(txtTrimL);
            _testCtrls.Add(txtTrimR);
            // Floppy
            _testCtrls.Add(txtFloppyL);
            _testCtrls.Add(txtFloppyR);
            // Hardness
            _testCtrls.Add(txtHardnessL);
            _testCtrls.Add(txtHardnessC);
            _testCtrls.Add(txtHardnessR);
            // Tension - Unwinder
            _testCtrls.Add(txtUSet);
            _testCtrls.Add(txtUActual);
            // Tension - Winder
            _testCtrls.Add(txtWSet);
            _testCtrls.Add(txtWActual);
        }

        private void ReleaseTestControls()
        {
            if (null != _testCtrls)
            {
                _testCtrls.Clear();
            }
            _testCtrls = null;
        }

        private void InitControls()
        {
            InitTestControls();
        }

        private void ReleaseControls()
        {
            ReleaseTestControls();
        }

        #endregion

        #region Enable Input Controls

        public void EnableTestControls(bool enabled)
        {
            if (null == _testCtrls)
                return;
            foreach (Control ctrl in _testCtrls)
            {
                ctrl.IsEnabled = enabled;
            }
            // Change background.
            ChangeTestControlBackground();
        }

        public void EnableTestControls()
        {
            if (null == _session || null == _session.InspectionTests)
            {
                EnableTestControls(false);
            }
            // Check null instance.
            _session.InspectionTests.CheckInstances();
            // Density
            txtDensityW.IsEnabled = _session.InspectionTests.Densities.W.Enabled;
            txtDensityF.IsEnabled = _session.InspectionTests.Densities.F.Enabled;
            // Width
            txtWdAll.IsEnabled = _session.InspectionTests.Widths.All.Enabled;
            txtWdPin.IsEnabled = _session.InspectionTests.Widths.Pin.Enabled;
            txtWdCoat.IsEnabled = _session.InspectionTests.Widths.Coat.Enabled;
            // Trim
            txtTrimL.IsEnabled = _session.InspectionTests.Trims.L.Enabled;
            txtTrimR.IsEnabled = _session.InspectionTests.Trims.R.Enabled;
            // Floppy
            txtFloppyL.IsEnabled = _session.InspectionTests.Floppies.L.Enabled;
            txtFloppyR.IsEnabled = _session.InspectionTests.Floppies.R.Enabled;

            // Hardness
            txtHardnessL.IsEnabled = _session.InspectionTests.Hardnesses.L.Enabled;
            txtHardnessC.IsEnabled = _session.InspectionTests.Hardnesses.C.Enabled;
            txtHardnessR.IsEnabled = _session.InspectionTests.Hardnesses.R.Enabled;

            // Tension - Unwinder
            txtUSet.IsEnabled = _session.InspectionTests.Unwinders.Set.Enabled;
            txtUActual.IsEnabled = _session.InspectionTests.Unwinders.Actual.Enabled;
            // Tension - Winder
            txtWSet.IsEnabled = _session.InspectionTests.Winders.Set.Enabled;
            txtWActual.IsEnabled = _session.InspectionTests.Winders.Actual.Enabled;

            if (_session.InspectionTests.Unwinders.Set.Enabled == true || _session.InspectionTests.Unwinders.Actual.Enabled == true)
            {
                if (ConmonReportService.Instance.CmdCountTestData != 0)
                {
                    CheckInspectionTestTempItem(_session.InspecionLotNo, ConmonReportService.Instance.CmdCountTestData);
                }
            }

            // Buttons
            cmdSaveTestRecord.IsEnabled = true;

            // Change background.
            ChangeTestControlBackground();
        }

        private void ChangeTestControlBackground()
        {
            if (null == _testCtrls)
                return;
            foreach (Control ctrl in _testCtrls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (ctrl as TextBox);
                    if (null != tb)
                    {
                        if (tb.IsEnabled)
                        {
                            tb.Background = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            tb.Background = new SolidColorBrush(Colors.DimGray);
                        }
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

        private void EnableControls(bool enabled)
        {
            EnableTestControls(enabled);
        }

        public bool CheckEnableControls()
        {
            bool chkEnCon = true;

            if (_session.InspectionTests.Densities.W.Enabled == true && chkEnCon == true)
            {
                if (txtDensityW.Text == "")
                {
                    "Density W isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Densities.F.Enabled == true && chkEnCon == true)
            {
                if (txtDensityF.Text == "")
                {
                    "Density F isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Widths.All.Enabled == true && chkEnCon == true)
            {
                if (txtWdAll.Text == "")
                {
                    "Width All isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Widths.Pin.Enabled == true && chkEnCon == true)
            {
                if (txtWdPin.Text == "")
                {
                    "Width Pin isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Widths.Coat.Enabled == true && chkEnCon == true)
            {
                if (txtWdCoat.Text == "")
                {
                    "Width Coat isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Trims.L.Enabled == true && chkEnCon == true)
            {
                if (txtTrimL.Text == "")
                {
                    "Trim L isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Trims.R.Enabled == true && chkEnCon == true)
            {
                if (txtTrimR.Text == "")
                {
                    "Trim R isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Floppies.L.Enabled == true && chkEnCon == true)
            {
                if (txtFloppyL.Text == "")
                {
                    "Floppy L isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Floppies.R.Enabled == true && chkEnCon == true)
            {
                if (txtFloppyR.Text == "")
                {
                    "Floppy R isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Hardnesses.L.Enabled == true && chkEnCon == true)
            {
                if (txtHardnessL.Text == "")
                {
                    "Hardness L isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Hardnesses.C.Enabled == true && chkEnCon == true)
            {
                if (txtHardnessC.Text == "")
                {
                    "Hardness C isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Hardnesses.R.Enabled == true && chkEnCon == true)
            {
                if (txtHardnessR.Text == "")
                {
                    "Hardness R isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Unwinders.Set.Enabled == true && chkEnCon == true)
            {
                if (txtUSet.Text == "")
                {
                    "Tension Unwinders Set isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Unwinders.Actual.Enabled == true && chkEnCon == true)
            {
                if (txtUActual.Text == "")
                {
                    "Tension Unwinders Actual isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Winders.Set.Enabled == true && chkEnCon == true)
            {
                if (txtWSet.Text == "")
                {
                    "Tension Winders Set isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            if (_session.InspectionTests.Winders.Actual.Enabled == true && chkEnCon == true)
            {
                if (txtWActual.Text == "")
                {
                    "Tension Winders Actual isn't null".ShowMessageBox(true);
                    chkEnCon = false;
                }
            }

            return chkEnCon;
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

        #endregion

        #region Load/Unload

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Init
            InitControls();
            // Clear all.
            ClearInputControls();
            // Validate Input by session state.
            ValidateInputState();

            InspectionPLCService.Instance.OnDataArrived += new PLCDataArrivedEventHandler(Instance_OnDataArrived);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            // PLC Service
            InspectionPLCService.Instance.OnDataArrived += new PLCDataArrivedEventHandler(Instance_OnDataArrived);
            // Free
            ReleaseControls();
        }

        #endregion

        #region PLC Handlers

        void Instance_OnDataArrived(object sender, PLCDataArrivedEventArgs e)
        {
            lock (this)
            {
                counter = e.Value;
            }
        }

        #endregion

        #region Control Handlers

        #region TextBox Handlers

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

        #region Button Handlers
        
        private void cmdSaveTestRecord_Click(object sender, RoutedEventArgs e)
        {
            // Save tests
            if (null != _session)
            {
                if (null == _session.InspectionTests)
                {
                    // init instance if null.
                    _session.InspectionTests = new InspectionTests();
                }

                if (CheckEnableControls() == true)
                {
                    // read all test control value(s) if enabled or not read only.
                    ReadTestControlValues();

                    decimal counterVal = decimal.Zero;
                    // Get Counter
                    lock (this)
                    {
                        counterVal = counter;
                    }
                    // Save data.
                    _session.AddInspectionTestData(_popupDateTime, counterVal);

                    if (_session.InspectionTests.Unwinders.Set.Enabled == true || _session.InspectionTests.Unwinders.Actual.Enabled == true)
                    {
                        ConmonReportService.Instance.CmdCountTestData = ConmonReportService.Instance.CmdCountTestData + 1;
                        CheckInspectionTestTempItem(_session.InspecionLotNo, ConmonReportService.Instance.CmdCountTestData);
                    }
                    // Clear all test data.
                    ClearTestInputControls();

                    this.DialogResult = false;
                }
            }
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        #endregion

        #region Setup

        /// <summary>
        /// Setup.
        /// </summary>
        /// <param name="session">The inspection session.</param>
        public void Setup(InspectionSession session)
        {
            _session = session;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="insLotNo"></param>
        /// <param name="chkNew"></param>
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

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets Popup DateTime.
        /// </summary>
        public DateTime PopupDateTime { get { return _popupDateTime; } }

        #endregion

    }
}
