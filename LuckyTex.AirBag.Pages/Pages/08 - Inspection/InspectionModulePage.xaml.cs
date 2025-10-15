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
using LuckyTex;
using LuckyTex.Services;
using LuckyTex.Models;

using DataControl.ClassData;
#endregion

namespace LuckyTex.Pages
{
    /// <summary>
    /// Interaction logic for InspectionModulePage.xaml
    /// </summary>
    public partial class InspectionModulePage : UserControl
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public InspectionModulePage()
        {
            InitializeComponent();

            LoadGroup();

            //New 23/8/22
            ConfigManager.Instance.LoadConfirmLengthConfigs();
            if (!string.IsNullOrEmpty(ConfigManager.Instance.ConfirmLengthConfig))
                confirmLength = decimal.Parse(ConfigManager.Instance.ConfirmLengthConfig);
            else
                confirmLength = 10;
        }

        #endregion

        #region InspectionTestPopupMonitor
        /// <summary>
        /// InspectionTestPopupMonitor internal class
        /// </summary>
        public class InspectionTestPopupMonitor
        {
            #region Internal Variables

            private decimal _currCounter = 0;
            private int _lastPopupDistance = 0;
            private int _nextPopupDistance = 0;

            private System.Timers.Timer _timer = null;
            private bool _onExecute = false;

            private bool _lock = false;

            #endregion

            #region Constructor and Destructor

            /// <summary>
            /// Constructor.
            /// </summary>
            public InspectionTestPopupMonitor() : base() { }
            /// <summary>
            /// Destructor.
            /// </summary>
            ~InspectionTestPopupMonitor()
            {
                Stop();
            }

            #endregion

            #region Private Methods

            #region Comment out

            private void CalcNextPopupDistance()
            {
                int nextPopup = Convert.ToInt32(
                    Math.Floor((_currCounter / (decimal)100)) * (int)100) + 100;
                if (_nextPopupDistance != nextPopup)
                {
                    lock (this)
                    {
                        _lastPopupDistance = _nextPopupDistance; // Keep last value.
                        _nextPopupDistance = nextPopup;
                    }
                }
            }

            #endregion

            void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
            {
                if (_onExecute)
                    return;
                _onExecute = true;
                try
                {
                    if (!_lock)
                    {
                        if (_nextPopupDistance > 0 && 
                            _currCounter >= _nextPopupDistance)
                        {
                            // Raise event
                            if (null != OnPopup)
                            {
                                // Raise event in UI Thread.
                                ApplicationManager.Instance.Invoke(OnPopup, this, EventArgs.Empty);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                _onExecute = false;
            }

            #endregion

            #region Public Methods

            /// <summary>
            /// Start monitoring.
            /// </summary>
            public void Start()
            {
                if (null != _timer)
                    return;
                _timer = new System.Timers.Timer();
                _timer.Interval = 300;
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                _timer.Start();
            }
            /// <summary>
            /// Stop monitoring.
            /// </summary>
            public void Stop()
            {
                if (null != _timer)
                {
                    _timer.Elapsed -= new System.Timers.ElapsedEventHandler(_timer_Elapsed);
                    _timer.Stop();
                    try { _timer.Dispose(); }
                    catch { }
                    finally { NGC.FreeGC(_timer); }
                }
                _timer = null;
            }
            /// <summary>
            /// Update Monitor value.
            /// </summary>
            /// <param name="counterValue">The current counter value.</param>
            /// <param name="session">The inspection session.</param>
            public void UpdateMonitor(decimal counterValue, InspectionSession session)
            {
                if (null == session || session.State != InspectionSessionState.Started)
                {
                    // Reset
                    _currCounter = decimal.Zero;
                    _nextPopupDistance = 0;
                    _lastPopupDistance = 0;
                    return;
                }
                // Update current counter value.
                _currCounter = counterValue;
                if (_nextPopupDistance == 0 ||
                    _nextPopupDistance < _lastPopupDistance)
                {
                    CalcNextPopupDistance();
                }
            }
            /// <summary>
            /// Reset Next Popup value. Call when start.
            /// </summary>
            public void Reset()
            {
                lock (this) 
                { 
                    _nextPopupDistance = 0;
                    _lastPopupDistance = 0;
                }
                CalcNextPopupDistance();
            }
            /// <summary>
            /// Advance next popup value by 100m.
            /// </summary>
            public void Next()
            {
                CalcNextPopupDistance();
            }

            #endregion

            #region Public Propeties

            /// <summary>
            /// Gets or sets lock.
            /// </summary>
            public bool Lock { get { return _lock; } set { _lock = value; } }

            #endregion

            #region Public Events

            /// <summary>
            /// OnPopup event.
            /// </summary>
            public event EventHandler OnPopup;

            #endregion
        }
        #endregion

        #region Internal Variables

        private List<Control> _generalCtrls = new List<Control>();
        private List<Control> _insOptionCtrls = new List<Control>();
        private List<Control> _defectCtrls = new List<Control>();
        private List<Control> _testCtrls = new List<Control>(); // Contains buttons only


        private InspectionSession __session = null;

        private InspectionSession _session
        {
            get { return __session; }
            set
            {
                __session = value;
                if (null == __session)
                {
                    Console.WriteLine("Null session assigned.");
                }
            }
        }

        private InspectionTestPopupMonitor _monitor = null;
        private bool _isTestPopupShown = false;

        private InspectionTestTempItem insTestTemp = new InspectionTestTempItem();

        //New 23/8/22
        decimal? confirmLength = null;


        bool chkConfirmstartLength = true;

        string P_FINISHINGLOT = string.Empty;
        string P_INSPECTIONLOT = string.Empty;
        DateTime? P_StartDate = null;

        long? PRODID = null;
        long? HEADERID = null;

        string P_LOTNO = string.Empty;
        string P_ITEMID = string.Empty;
        string P_LOADINGTYPE = string.Empty;

        #endregion

        #region Private Methods

        #region Inspection Session methods
        
        private void InitSession()
        {
            if (null != _session)
            {
                lbInsMC.Text = (null != _session.Machine) ?
                    _session.Machine.DisplayName : "-";
                _session.OnStateChanged += new EventHandler(_session_OnStateChanged);
            }
            else
            {
                lbInsMC.Text = "-";
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
                switch (_session.State)
                {
                    case InspectionSessionState.Idle:
                        EnableGeneralControls(true);
                        EnableInsOptionControls(false);
                        EnableDefectControls(false);
                        EnableTestControls(false);
                        break;
                    case InspectionSessionState.Started:
                        EnableGeneralControls(false);
                        EnableInsOptionControls(false);
                        EnableDefectControls(true);
                        EnableTestControls();
                        break;
                    case InspectionSessionState.LongDefect:
                        EnableGeneralControls(false);
                        EnableInsOptionControls(false);
                        EnableDefectControls(true);
                        EnableTestControls(false);
                        break;
                    default: // None
                        EnableGeneralControls(true);
                        EnableInsOptionControls(true);
                        EnableDefectControls(false);
                        EnableTestControls(false);
                        break;
                }
                // Re-Sync Inspection Type from Checked Box and Radio Buttons.
                UpdateInspectionTypeToSession();
                // Update controls.
                UpdateSessionToControls();
                // Validate Inspection Type
                ValidateInspectionType();
                // Enable process buttons
                EnableProcessButtons();
            }
        }

        #endregion

        #region Init/Release Input Controls

        private void InitGeneralControls()
        {
            if (null == _generalCtrls)
                _generalCtrls = new List<Control>();
            _generalCtrls.Add(txtItemLotNo);
            _generalCtrls.Add(txtInsLotNo);
            //_generalCtrls.Add(txtCustomerType);
        }

        private void ReleaseGeneralControls()
        {
            if (null != _generalCtrls)
            {
                _generalCtrls.Clear();
            }
            _generalCtrls = null;
        }

        private void InitInsOptionControls()
        {
            if (null == _insOptionCtrls)
                _insOptionCtrls = new List<Control>();

            _insOptionCtrls.Add(rbMass);
            _insOptionCtrls.Add(rbTest);

            _insOptionCtrls.Add(chkReAdjustSameLot);
            _insOptionCtrls.Add(rbReAdjustSameLotSameItem);
            _insOptionCtrls.Add(rbReAdjustSameLotDiffItem);
            _insOptionCtrls.Add(txtDiff);

            _insOptionCtrls.Add(chkReProcessChangeLot);
        }

        private void ReleaseInsOptionControls()
        {
            if (null != _insOptionCtrls)
            {
                _insOptionCtrls.Clear();
            }
            _insOptionCtrls = null;
        }

        private void InitDefectControls()
        {
            if (null == _defectCtrls)
                _defectCtrls = new List<Control>();

            _defectCtrls.Add(txtDefectPos);
            _defectCtrls.Add(txtLongDefectStart);
            _defectCtrls.Add(txtLongDefectEnd);

            //New Long
            _defectCtrls.Add(txtDefectPos_Long);
            _defectCtrls.Add(txtDefectLength);

            // Buttons
            _defectCtrls.Add(cmdLongDefectStart);
            _defectCtrls.Add(cmdLongDefectEnd);
            _defectCtrls.Add(cmdLongDefectCancel);
        }

        private void ReleaseDefectControls()
        {
            if (null != _defectCtrls)
            {
                _defectCtrls.Clear();
            }
            _defectCtrls = null;
        }

        private void InitTestControls()
        {
            if (null == _testCtrls)
                _testCtrls = new List<Control>();

            // Buttons
            _defectCtrls.Add(cmd100M);
            _defectCtrls.Add(cmdHistory);
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
            InitGeneralControls();
            InitInsOptionControls();
            InitDefectControls();
            InitTestControls();
        }

        private void ReleaseControls()
        {
            ReleaseGeneralControls();
            ReleaseInsOptionControls();
            ReleaseDefectControls();
            ReleaseTestControls();
        }

        #endregion

        #region Init/Release Pupup Monitor and Popup Handler

        private void InitMonitor()
        {
            if (null != _monitor)
                return;
            _monitor = new InspectionTestPopupMonitor();
            _monitor.OnPopup += new EventHandler(_monitor_OnPopup);
            _monitor.Start();
        }

        private void ReleaseMonitor()
        {
            if (null != _monitor)
            {
                _monitor.OnPopup -= new EventHandler(_monitor_OnPopup);
                _monitor.Stop();
            }
            _monitor = null;
        }

        void _monitor_OnPopup(object sender, EventArgs e)
        {
            ShowTestPopup();
        }

        #endregion

        #region Enable Input/Button Controls

        public void EnableGeneralControls(bool enabled)
        {
            if (null == _generalCtrls)
                return;
            foreach (Control ctrl in _generalCtrls)
            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).IsReadOnly = !enabled;
                }
                else ctrl.IsEnabled = enabled;
            }
        }

        public void EnableInsOptionControls(bool enabled)
        {
            if (null == _insOptionCtrls)
                return;
            foreach (Control ctrl in _insOptionCtrls)
            {
                ctrl.IsEnabled = enabled;
                if (enabled)
                {
                    ctrl.Opacity = 1;
                }
                else
                {
                    ctrl.Opacity = 0.5;
                }
            }
        }

        public void EnableDefectControls(bool enabled)
        {
            if (null == _defectCtrls)
                return;
            foreach (Control ctrl in _defectCtrls)
            {
                if (ctrl is TextBox)
                {
                    (ctrl as TextBox).IsReadOnly = !enabled;
                }
                else ctrl.IsEnabled = enabled;
            }
        }

        public void EnableTestControls(bool enabled)
        {
            if (null == _testCtrls)
                return;
            foreach (Control ctrl in _testCtrls)
            {
                ctrl.IsEnabled = enabled;
            }
        }

        public void EnableTestControls()
        {
            if (null == _session || null == _session.InspectionTests)
            {
                EnableTestControls(false);
            }
            // Check null instance.
            _session.InspectionTests.CheckInstances();
           
            // Buttons
            cmd100M.IsEnabled = true;
        }

        private void EnableControls(bool enabled)
        {
            EnableGeneralControls(enabled);
            EnableInsOptionControls(enabled);
            EnableDefectControls(enabled);
            EnableTestControls(enabled);
        }

        private void EnableProcessButtons()
        {
            if (null != _session)
            {
                switch (_session.State)
                {
                    case InspectionSessionState.Idle:
                        if (_session.StartDate != DateTime.MinValue)
                        {
                            cmdStart.IsEnabled = false;
                        }
                        else
                        {
                            cmdStart.IsEnabled = true;
                        }

                        cmdStop.IsEnabled = false;
                        cmdPreview.IsEnabled = false;

                        if (_session.StartDate != DateTime.MinValue)
                        {
                            //if (_session.ReAdjustMode == ReAdjustMode.None &&
                            //    _session.ReProcessMode == ReProcessMode.None)
                                cmdNext.IsEnabled = true;
                            //else cmdNext.IsEnabled = false;
                        }
                        else cmdNext.IsEnabled = false;
                        
                        cmdClear.IsEnabled = false;
                        
                        cmdSuspend.IsEnabled = false;
                        cmdNew.IsEnabled = true;

                        if (cmdStop.IsEnabled == false && cmdNext.IsEnabled == true && cmdStart.IsEnabled == false
                            && _session.EndDate != DateTime.MinValue)
                            cmdWeight.IsEnabled = true;
                        else
                            cmdWeight.IsEnabled = false;

                        if (_session.StartDate != DateTime.MinValue)
                            cmdRemark.IsEnabled = true;
                        else cmdRemark.IsEnabled = false;

                        cmdShowDefectList.IsEnabled = true;
                        cmdHistory.IsEnabled = true;
                        break;
                    case InspectionSessionState.Started:
                        cmdStart.IsEnabled = false;
                        cmdWeight.IsEnabled = false;
                        cmdStop.IsEnabled = true;
                        cmdNext.IsEnabled = false;
                        cmdClear.IsEnabled = true;
                        cmdPreview.IsEnabled = true;
                        
                        if (_session.ReAdjustMode == ReAdjustMode.None &&
                            _session.ReProcessMode == ReProcessMode.None)
                            cmdSuspend.IsEnabled = true;
                        else cmdSuspend.IsEnabled = false;                        

                        cmdNew.IsEnabled = false;                        
                        cmdRemark.IsEnabled = true;
                        cmdShowDefectList.IsEnabled = true;
                        cmdHistory.IsEnabled = true;
                        break;
                    case InspectionSessionState.LongDefect:
                        cmdStart.IsEnabled = false;
                        cmdWeight.IsEnabled = false;
                        cmdStop.IsEnabled = false;
                        cmdNext.IsEnabled = false;
                        cmdClear.IsEnabled = false;
                        cmdSuspend.IsEnabled = false;
                        cmdNew.IsEnabled = false;
                        cmdRemark.IsEnabled = false;
                        cmdShowDefectList.IsEnabled = false;
                        cmdHistory.IsEnabled = false;
                        cmdPreview.IsEnabled = false;
                        break;
                    default: // None
                        cmdStart.IsEnabled = false;
                        cmdWeight.IsEnabled = false;
                        cmdStop.IsEnabled = false;
                        cmdNext.IsEnabled = false;
                        cmdClear.IsEnabled = false;
                        cmdSuspend.IsEnabled = false;
                        cmdNew.IsEnabled = false;
                        cmdRemark.IsEnabled = false;
                        cmdShowDefectList.IsEnabled = false;
                        cmdHistory.IsEnabled = false;
                        cmdPreview.IsEnabled = false;
                        break;
                }
            }
        }

        private void CheckInspectionLot()
        {
            string insLot = txtInsLotNo.Text;
            if (null != _session)
            {
                ValidateInspectionType(); // Check Type of Inspection.

                if (_session.InspecionLotNo == insLot)
                {
                    return;
                }

                _session.InspecionLotNo = insLot;

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

        // New calculate Defect Length
        private void CalDefectLength()
        {
            try
            {
                if (txtLongDefectStart.Text != "" && txtLongDefectEnd.Text != "")
                {
                    decimal start = decimal.Zero;
                    decimal end = decimal.Zero;

                    if (!string.IsNullOrWhiteSpace(txtLongDefectStart.Text))
                    {
                        start = Convert.ToDecimal(txtLongDefectStart.Text);
                    }

                    if (!string.IsNullOrWhiteSpace(txtLongDefectEnd.Text))
                    {
                        end = Convert.ToDecimal(txtLongDefectEnd.Text);
                    }

                    if (start > 0 && end > 0)
                    {
                        if (end > start)
                            txtDefectLength.Text = (end - start).ToString("#,##0.##");
                    }
                    else
                        txtDefectLength.Text = "0";
                    
                }
            }
            catch (Exception ex)
            {
                ex.Message.ShowMessageBox(false);
            }
        }

        #endregion

        #region Clear Input Controls

        private void ClearGeneralInputControls()
        {
            txtItemLotNo.Text = string.Empty;
            txtCustomerType.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtTotalLen.Text = "0.00";
            txtStartDT.Text = string.Empty;
            txtLoadingType.Text = string.Empty;
            txtInsLotNo.Text = string.Empty;

            txtCurrLen.Text = "0.00";
            txtRemainLen.Text = "0.00";

            txtCounter.Text = "00000.00";

            // เพิ่มเพื่อใช้ในการหาค่า Def Start 100
            chkStart0.IsChecked = true;

            cbGroup.SelectedIndex = 0;
        }

        private void ClearInsOptionControls()
        {
            rbMass.IsChecked = true;
            rbTest.IsChecked = false;
            chkReAdjustSameLot.IsChecked = false;
            chkReProcessChangeLot.IsChecked = false;
            txtDiff.Text = string.Empty;
        }

        private void ClearDefectInputControls()
        {
            txtDefectPos.Text = string.Empty;
            txtDefectInfo.Text = "-";

            txtLongDefectStart.Text = string.Empty;
            txtLongDefectEnd.Text = string.Empty;

            // New
            txtDefectPos_Long.Text = string.Empty;
            txtDefectInfo_Long.Text = "-";
            txtDefectLength.Text = string.Empty;

            txtTotalDefect.Text = "0";
            txtGrade.Text = string.Empty;
            ChangeGradeBoxColors();
        }

        private void ClearInputControls()
        {
            ClearGeneralInputControls();
            ClearInsOptionControls();
            ClearDefectInputControls();
        }

        #endregion

        #region Counter Controls

        private void UpdateCounterValue(decimal value)
        {
            string sVal = value.ToString("n2").Replace(",", string.Empty);
            // Current Value From Counter.
            txtCurrLen.Text = sVal.Trim();
            // Overall Length (Length from Finishing Process).
            decimal overallLen = (null != _session) ? _session.OverallLength : 0;
            txtTotalLen.Text = overallLen.ToString("n2").Replace(",", string.Empty);
            // Actual Length
            decimal actualLen = (null != _session) ? _session.ActualLength : overallLen;

            // Remain
            decimal remain = actualLen - value;
            txtRemainLen.Text = remain.ToString("n2").Replace(",", string.Empty);

            // Update Counter Box Text
            bool isMinus = (value < 0);
            if (isMinus)
            {
                sVal = sVal.Remove(0, 1); // remove minus sign.
            }
            // Fill zero(s) in front of text.
            while (sVal.Length < 8)
            {
                sVal = "0" + sVal;
            }
            if (isMinus)
            {
                sVal = "-" + sVal; // Add minus sign in front of value.
            }

            txtCounter.Text = sVal.Trim();

            UpdateGrade(false); // Update every 2 second.
        }

        #endregion

        #region Validate/Update Inspection Type

        private void ValidateInspectionType()
        {
            if (null != _session)
            {
                if (_session.ReAdjustMode == ReAdjustMode.None &&
                    _session.ReProcessMode == ReProcessMode.None)
                {
                    // Enable and Allow to edit Item Lot Number (Finishing Lot)
                    txtItemLotNo.IsEnabled = true;
                    txtItemLotNo.IsReadOnly = false;                    
                    // Disable and Not allow Inspection Lot Number
                    txtInsLotNo.IsEnabled = false;
                    txtInsLotNo.IsReadOnly = true;
                }
                else if (_session.ReAdjustMode == ReAdjustMode.Same &&
                    _session.ReProcessMode == ReProcessMode.None)
                {
                    // Enable and Allow to edit Item Lot Number (Finishing Lot)
                    txtItemLotNo.IsEnabled = false;
                    txtItemLotNo.IsReadOnly = true;
                    // Disable and Not allow Inspection Lot Number
                    txtInsLotNo.IsEnabled = true;
                    txtInsLotNo.IsReadOnly = false;
                }
                else if (_session.ReAdjustMode == ReAdjustMode.Diff &&
                    _session.ReProcessMode == ReProcessMode.None)
                {
                    // Enable and Allow to edit Item Lot Number (Finishing Lot)
                    txtItemLotNo.IsEnabled = false;
                    txtItemLotNo.IsReadOnly = true;
                    // Disable and Not allow Inspection Lot Number
                    txtInsLotNo.IsEnabled = true;
                    txtInsLotNo.IsReadOnly = false;
                }
                else if (_session.ReProcessMode == ReProcessMode.ReProcess)
                {
                    // Enable and Allow to edit Item Lot Number (Finishing Lot)
                    txtItemLotNo.IsEnabled = false;
                    txtItemLotNo.IsReadOnly = true;
                    // Disable and Not allow Inspection Lot Number
                    txtInsLotNo.IsEnabled = true;
                    txtInsLotNo.IsReadOnly = false;
                }
                else
                {
                    // Enable and Allow to edit Item Lot Number (Finishing Lot)
                    txtItemLotNo.IsEnabled = true;
                    txtItemLotNo.IsReadOnly = false;
                    // Disable and Not allow Inspection Lot Number
                    txtInsLotNo.IsEnabled = false;
                    txtInsLotNo.IsReadOnly = true;
                }
            }
            else
            {
                // Enable and Allow to edit Item Lot Number (Finishing Lot)
                txtItemLotNo.IsEnabled = true;
                txtItemLotNo.IsReadOnly = false;
                // Disable and Not allow Inspection Lot Number
                txtInsLotNo.IsEnabled = false;
                txtInsLotNo.IsReadOnly = true;
            }
        }

        private void UpdateInspectionTypeToSession()
        {
            if (null != _session)
            {
                if (true == rbMass.IsChecked)
                    _session.InspectionType = InspectionTypes.Mass;
                else
                    _session.InspectionType = InspectionTypes.Test;

                if (chkReAdjustSameLot.IsChecked == true)
                {
                    if (rbReAdjustSameLotSameItem.IsChecked == true)
                        _session.ReAdjustMode = ReAdjustMode.Same;
                    else if (rbReAdjustSameLotDiffItem.IsChecked == true)
                        _session.ReAdjustMode = ReAdjustMode.Diff;
                    else _session.ReAdjustMode = ReAdjustMode.Same; // default is same
                }

                if (chkReProcessChangeLot.IsChecked == true)
                {
                    _session.ReProcessMode = ReProcessMode.ReProcess;
                }
                else _session.ReProcessMode = ReProcessMode.None;
            }
        }

        #endregion

        #region Update Session to Controls

        private bool _lockUpdateGrade = false;
        private DateTime _lastUpdateGrade = DateTime.Now;

        private void UpdateGrade(bool force)
        {
            if (_lockUpdateGrade || null == _session)
                return;

            if (_session.State == InspectionSessionState.None ||
                _session.State == InspectionSessionState.Idle)
            {
                return;
            }

            _lockUpdateGrade = true;

            TimeSpan ts = DateTime.Now - _lastUpdateGrade;

            if (force || ts.TotalSeconds > 30)
            {
                decimal counterVal = Convert.ToDecimal(txtCounter.Text);
                txtGrade.Text = _session.GetGrade(counterVal);
                ChangeGradeBoxColors();

                _lastUpdateGrade = DateTime.Now;
            }

            _lockUpdateGrade = false;
        }

        private void ChangeGradeBoxColors()
        {
            // Change Grade Bg Color.
            if (string.IsNullOrWhiteSpace(txtGrade.Text))
            {
                brdGrade.Background = InspectionConsts.NBackground;
                txtGrade.Foreground = InspectionConsts.BlackBlush;
            }
            else if (txtGrade.Text == "A")
            {
                brdGrade.Background = InspectionConsts.ABackground;
                txtGrade.Foreground = InspectionConsts.BlackBlush;
            }
            else if (txtGrade.Text == "B")
            {
                brdGrade.Background = InspectionConsts.BBackground;
                txtGrade.Foreground = InspectionConsts.BlackBlush;
            }
            else
            {
                brdGrade.Background = InspectionConsts.CBackground;
                txtGrade.Foreground = InspectionConsts.WhiteBlush;
            }
        }

        private void UpdateSessionToControls()
        {
            if (null == _session)
            {
                this.EnableControls(false);
                return;
            }
            // Update User.
            if (null != _session)
            {
                txtOperator.Text = (null != _session.CurrentUser) ?
                    _session.CurrentUser.OperatorId : "-";
            }
            else
            {
                txtOperator.Text = "-";
            }

            if (_session.State == InspectionSessionState.None)
            {
                EnableGeneralControls(true);
                EnableDefectControls(false);
                EnableTestControls(false);
            }
            txtItemLotNo.Text = _session.FinishingLotNo;
            txtItemCode.Text = _session.ItemCode;
            txtTotalLen.Text = _session.OverallLength.ToString("n2")
                .Replace(",", string.Empty);
            txtInsLotNo.Text = _session.InspecionLotNo;
            txtLoadingType.Text = _session.LOADTYPE;

            txtCustomerType.Text = _session.CustomerType;

            if ((_session.State == InspectionSessionState.Idle &&
                 _session.StartDate != DateTime.MinValue &&
                 _session.StartDate != DateTime.MaxValue) ||
                _session.State == InspectionSessionState.Started ||
                _session.State == InspectionSessionState.LongDefect)
            {
                txtStartDT.Text = _session.StartDateString;
            }
            else
            {
                txtStartDT.Text = string.Empty;
            }

            if ((_session.State == InspectionSessionState.Idle &&
                 _session.EndDate != DateTime.MinValue &&
                 _session.EndDate != DateTime.MaxValue))
            {
                txtEndDT.Text = _session.EndDateString;
            }
            else
            {
                txtEndDT.Text = string.Empty;
            }

            txtLongDefectStart.IsReadOnly = true;
            txtLongDefectEnd.IsReadOnly = true;
            //New Long
            txtDefectLength.IsReadOnly = true;

            if (_session.State == InspectionSessionState.None ||
                _session.State == InspectionSessionState.Idle)
            {
                // Clear both data
                txtLongDefectStart.Text = string.Empty;
                txtLongDefectEnd.Text = string.Empty;

                //New Long
                txtDefectPos_Long.Text = string.Empty;
                txtDefectInfo_Long.Text = "-";
                txtDefectLength.Text = string.Empty;
            }

            if (_session.State != InspectionSessionState.None)
            {
                int total = _session.GetDefectCount();
                txtTotalDefect.Text = total.ToString("n0");
                CalDefect();
            }
            else txtTotalDefect.Text = "0";

            if (_session.State == InspectionSessionState.Started ||
                _session.State == InspectionSessionState.LongDefect)
            {
                UpdateGrade(true);
            }
            else
            {
                if (_session.EndDate == DateTime.MinValue)
                {
                    txtGrade.Text = string.Empty;
                }
                ChangeGradeBoxColors();
            }
        }

        private void CheckSuspend()
        { 
            if (null != _session)
            {
                if (_session.InspecionLotNo != "")
                {
                    cmdNew.IsEnabled = false;
                }
            }
        }

        #endregion

        #region 100M Test Popup

        private void ShowTestPopup()
        {
            if (_isTestPopupShown)
                return;
            if (null != _session)
            {
                _isTestPopupShown = true;
                if (null != _monitor)
                {
                    _monitor.Lock = true;
                }
                try
                {
                    Windows.InspectionTestEntryWindow window = new Windows.InspectionTestEntryWindow();
                    window.Setup(_session);

                    if (window.ShowDialog() == true)
                    {

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                _isTestPopupShown = false;

                if (null != _monitor)
                {
                    _monitor.Next();
                    _monitor.Lock = false;
                }

                UpdateGrade(true);
            }
        }

        #endregion

        #region เพิ่มการคำนวณหาค่า 100 M
        #region chkStart0

        private void chkStart0_Checked(object sender, RoutedEventArgs e)
        {
            if (chkStart0.IsChecked == true)
            {
                txtDefect100.Text = "0";
                txtDefect100.IsEnabled = false;

                CalDefect();
            }
        }

        private void chkStart0_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkStart0.IsChecked == false)
            {
                txtDefect100.Text = txtCurrLen.Text;
                txtDefect100.IsEnabled = true;

                CalDefect();
            }
        }

        #endregion

        #region CalDefect

        private void CalDefect()
        {
            try
            {
                #region currLen

                decimal currLen = decimal.Zero;

                if (txtCurrLen.Text != "")
                {
                    try
                    {
                        currLen = Convert.ToDecimal(txtCurrLen.Text);
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                    return;

                #endregion

                #region remainLen

                decimal remainLen = decimal.Zero;
                
                if (txtRemainLen.Text != "")
                {
                    try
                    {
                        remainLen = Convert.ToDecimal(txtRemainLen.Text);
                    }
                    catch
                    {
                        return;
                    }
                }
                else
                    return;

                #endregion

                #region defect100

                decimal defect100 = decimal.Zero;
                
                if (txtDefect100.Text != "")
                {
                    try
                    {
                        defect100 = Convert.ToDecimal(txtDefect100.Text);
                    }
                    catch
                    {
                        return;
                    }
                }

                #endregion

                decimal totalLen = decimal.Zero;

                //totalLen = (remainLen + currLen);
                totalLen = _session.ActualLength;

                if (totalLen != 0)
                {
                    decimal n = 0;
                    int total = 0;
                    string lotno = _session.InspecionLotNo;

                    if (lotno != "")
                    {
                        #region chkDef

                        int chkDef = 0;

                        try
                        {
                            if (txtTotalDefect.Text != "")
                                chkDef = Convert.ToInt32(txtTotalDefect.Text);
                        }
                        catch
                        {
                            return;
                        }

                        #endregion

                        if (chkDef > 0)
                        {
                            #region Defect100

                            decimal i = decimal.Zero;

                            for (i = defect100; i <= (totalLen + 100); )
                            {
                                n = i + 100;
                                if (i <= currLen && n >= currLen)
                                {
                                    total = _session.GetDefect100MCount(i, n);
                                    break;
                                }

                                i = i + 100;
                            }

                            if (total > 0)
                            {
                                txtTotalDefect100M.Text = total.ToString("n0");

                                if (total >= 10)
                                    brdDefect100.Background = Brushes.Red;
                                else if (total == 9)
                                    brdDefect100.Background = Brushes.Orange;
                                else
                                    brdDefect100.Background = Brushes.Yellow;
                            }
                            else
                            {
                                txtTotalDefect100M.Text = "0";
                                brdDefect100.Background = Brushes.Yellow;
                            }

                            #endregion
                        }
                        else
                        {
                            txtTotalDefect100M.Text = "0";
                            brdDefect100.Background = Brushes.Yellow;
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

        #endregion

        #region เพิ่ม ins_getNetLength 
        private decimal GetNetLength(decimal? length, string grade)
        {
            decimal? netLength = null;

            try
            {
                //string cusid = string.Empty;

                //if (_session.GetNetLength(cusid, length, grade) != null)
                //{
                //    netLength = _session.GetNetLength(cusid, length, grade).Value;
                //}

                netLength = _session.GetNetLength(length, grade);

                if (netLength != null)
                {
                    return netLength.Value;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString().ShowMessageBox(true);
            }

            return 0;
        }
        #endregion

        #region ShowConfirmstartLengthPopup

        private void ShowConfirmstartLengthPopup(string ConfirmstartLength)
        {
            if (null != _session)
            {
                try
                {
                    if (!string.IsNullOrEmpty(_session.InspecionLotNo))
                    {
                        Windows.ConfirmstartLengthWindow window = new Windows.ConfirmstartLengthWindow();

                        window.Setup(ConfirmstartLength);

                        if (window.ShowDialog() == true)
                        {
                            decimal? _CONFIRMSTART = null;

                            //เลขตามที่ config
                            _CONFIRMSTART = decimal.Parse(ConfirmstartLength);

                            // เลขจริงที่หน้าจอ
                            //_CONFIRMSTART = decimal.Parse(txtCounter.Text);

                            _session.INS_INSERTCONFIRMSTARTING(_CONFIRMSTART);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        #endregion

        #endregion

        #region Load/Unload

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            labCustomerType.Visibility = System.Windows.Visibility.Collapsed;
            txtCustomerType.Visibility = System.Windows.Visibility.Collapsed;

            // Init
            InitControls();
            // Clear all.
            ClearInputControls();
            // PLC Service
            InspectionPLCService.Instance.OnDataArrived += new PLCDataArrivedEventHandler(Instance_OnDataArrived);
            // Prepare
            UpdateCounterValue(decimal.Zero);
            // Load session data and bind to controls
            UpdateSessionToControls();
            // Check Type state
            ValidateInspectionType();
            // Enable process buttons
            EnableProcessButtons();
            // Init Popup Monitor.
            InitMonitor();

            CheckSuspend();

            string msg = string.Empty;
            msg += "============================================" + Environment.NewLine;
            msg += "| " + "Start Inspection Module " + Environment.NewLine;
            msg += "| " + "Operator = " + txtOperator.Text + Environment.NewLine;
            msg += "| " + "MC No = " + lbInsMC.Text + " " + Environment.NewLine;
            msg += "============================================" + Environment.NewLine;
            msg.Info(); 

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // PLC Service
            InspectionPLCService.Instance.OnDataArrived += new PLCDataArrivedEventHandler(Instance_OnDataArrived);
            // Release Monitor
            ReleaseMonitor();
            // Free
            ReleaseControls();
            // Free Session
            ReleaseSession();
        }

        #endregion

        #region Main Menu Button Handlers

        private void cmdPreview_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_session.InspecionLotNo))
            {
                try
                {
                    decimal counterVal = 0;

                    if (txtCounter.Text != string.Empty)
                        counterVal = Convert.ToDecimal(txtCounter.Text);
                    else
                        txtCounter.Text = "0";

                    string grade = txtGrade.Text;

                    decimal netLength = GetNetLength(counterVal, grade);

                    Preview(_session.InspecionLotNo, grade, netLength, counterVal);
                }
                catch (Exception ex)
                {
                    ex.Message.ToString().ShowMessageBox(true);
                }
            }
            else
            {
                "Can't Preview Item Lot No. is null".ShowMessageBox(false);
            }
        }

        private void cmdWeight_Click(object sender, RoutedEventArgs e)
        {
            // New 15/01/24
            // Show WeightMeasurement
            if (null != _session)
            {
                if (_session.InspecionLotNo != "" && txtEndDT.Text != "")
                {
                    Windows.WeightMeasurementWindow window = new Windows.WeightMeasurementWindow();
                    window.Setup(_session.InspecionLotNo);

                    if (window.ShowDialog() == true)
                    {
                    }
                }
                else
                {
                    if (_session.InspecionLotNo == "")
                        "Can't find Lot No.".ShowMessageBox(false);
                    else if(txtEndDT.Text == "")
                        ("Lot No. = "+_session.InspecionLotNo +" is no stop").ShowMessageBox(false);
                }
            }
        }

        private void cmdBack_Click(object sender, RoutedEventArgs e)
        {
            string msg = string.Empty;
            msg += "| " + "Close Inspection Module " + Environment.NewLine;
            msg.Info(); 
            PageManager.Instance.Back();
        }

        #endregion

        #region PLC Handlers
        
        void Instance_OnDataArrived(object sender, PLCDataArrivedEventArgs e)
        {
            // Sync value to monitor.
            if (null != _monitor)
            {
                _monitor.UpdateMonitor(e.Value, _session);
            }
            // Update display.
            UpdateCounterValue(e.Value);


        }

        #endregion

        #region Control Handlers

        #region Buttons

        #region Main (New/Start/Stop/Clear)
        
        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                // Reset Inspection Type
                chkReAdjustSameLot.IsChecked = false;
                chkReProcessChangeLot.IsChecked = false;

                if (null != _monitor)
                {
                    _monitor.Reset(); // Reset next popup value.
                }
                // Reset.
                _session.New();

                ConmonReportService.Instance.CmdCountTestData = 0;
                insTestTemp = null;


                //New 30/08/22
                chkConfirmstartLength = true;

                if (!string.IsNullOrEmpty(txtItemLotNo.Text) 
                    && !string.IsNullOrEmpty(txtInsLotNo.Text))
                {
                    D365_IN_New();
                }
            }
        }

        private void cmdStart_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                // Check for Diff Item input
                if (rbReAdjustSameLotDiffItem.IsChecked == true)
                {
                    if (string.IsNullOrWhiteSpace(txtDiff.Text))
                    {
                        "Please enter item code.".ShowMessageBox(true);

                        rbReAdjustSameLotDiffItem.Opacity = 1;
                        txtDiff.IsEnabled = true;
                        txtDiff.IsReadOnly = false;
                        txtDiff.Opacity = 1;

                        txtDiff.SelectAll();
                        txtDiff.Focus();
                        return;
                    }
                    string msg = "Do you want to Re-Check Lot : " + 
                        _session.InspecionLotNo;
                    if (!msg.ShowConfirmWindow())
                    {
                        // User cancel.
                        return;
                    }
                    // Assign new item code.
                    _session.DiffItemCode = txtDiff.Text;
                    // Disable Diff Item textbox
                    rbReAdjustSameLotDiffItem.Opacity = 0.5;
                    txtDiff.IsEnabled = false;
                    txtDiff.IsReadOnly = true;
                    txtDiff.Opacity = 0.5;
                }
                else if (chkReProcessChangeLot.IsChecked == true)
                {
                    string msg = "Do you want to Re-Check and Change Lot : " +
                        _session.PEInsLotNo;
                    if (!msg.ShowConfirmWindow())
                    {
                        // User cancel.
                        return;
                    }
                }

                UpdateInspectionTypeToSession();

                //นำออกไปก่อนยังไม่ได้ใช้
                //if (_session.INS_GETRESETSTARTLENGTH() != null)
                //{
                //    ("ค่า Reset Start Length ของ Item นี้ = " + _session.RESETSTARTLENGTH.ToString()).ShowMessageBox();
                //}

                _session.Start();

                ConmonReportService.Instance.CmdCountTestData = 0;
                insTestTemp = null;

                cmdBack.IsEnabled = false;

                //เพิ่มใหม่กันไม่ให้ key Defect
                txtDefectPos.IsEnabled = true;
                txtDefectPos_Long.IsEnabled = true;
                cmdLongDefectStart.IsEnabled = true;
                cmdLongDefectEnd.IsEnabled = true;

                // New 30/08/22
                decimal? cl = decimal.Parse(txtCounter.Text);
                if (chkConfirmstartLength == true)
                {
                    //Check confirmLength
                    if (cl >= confirmLength)
                    {
                        chkConfirmstartLength = false;
                        //Open confirmLength windows
                        ShowConfirmstartLengthPopup(confirmLength.ToString());
                    }
                }
                else if (chkConfirmstartLength == false)
                {
                    if (cl >= 0 && cl < (confirmLength - 1))
                    {
                        chkConfirmstartLength = true;
                    }
                }
            }
        }

        private void cmdStop_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                UpdateInspectionTypeToSession();

                //if (chkReAdjustSameLot.IsChecked == true || chkReProcessChangeLot.IsChecked == true)
                if (chkReProcessChangeLot.IsChecked == true)
                {
                    string remark = _session.GetRemark();
                    if (null != remark)
                    {
                        #region End

                        decimal counterVal = Convert.ToDecimal(txtCounter.Text);
                        string grade = txtGrade.Text;
                        if (string.IsNullOrWhiteSpace(grade))
                        {
                            "Cannot Stop current process because Grade is not calculated."
                                .ShowMessageBox(true);
                            return;
                        }

                        // Show Confirm grade.
                        ConfirmGradeInfo confirmResult =
                            this.ShowConfirmGradeBox(grade, _session);

                        if (null != confirmResult)
                        {
                            if (confirmResult.IsConfirm)
                            {
                                // Update Grade
                                txtGrade.Text = confirmResult.Grade;
                                ChangeGradeBoxColors();

                                // เพิ่ม ins_getNetLength หลังจาก ConfirmGrade
                                decimal netLength = GetNetLength(counterVal, confirmResult.Grade);

                                // Success Confirm. End current session.
                                // เพิ่มส่งค่า netlength
                                _session.End(counterVal, netLength, confirmResult.Grade,txtLoadingType.Text,"W");

                                if (!string.IsNullOrEmpty(_session.FinishingLotNo) 
                                    && !string.IsNullOrEmpty(_session.InspecionLotNo))
                                {
                                    D365_IN(); 
                                }

                                cmdBack.IsEnabled = true;

                                //เพิ่มใหม่กันไม่ให้ key Defect
                                txtDefectPos.IsEnabled = false;
                                //txtDefectPos.Text = string.Empty;
                                txtDefectPos_Long.IsEnabled = false;
                                //txtDefectPos_Long.Text = string.Empty;
                                cmdLongDefectStart.IsEnabled = false;
                                cmdLongDefectEnd.IsEnabled = false;
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(confirmResult.ErrMessage))
                                {
                                    confirmResult.ErrMessage.ShowMessageBox(true);
                                }
                                else "Grade is not confirm please comfirm grade to stop current process.".ShowMessageBox(true);
                            }
                        }
                        else
                        {
                            "Confirm Grade Error. No Result Returns.".ShowMessageBox(true);
                        }

                        #endregion
                    }
                    else
                    {
                        "Please insert remark".ShowMessageBox(true);
                    }
                }
                else
                {
                    #region End

                    decimal counterVal = Convert.ToDecimal(txtCounter.Text);
                    string grade = txtGrade.Text;
                    if (string.IsNullOrWhiteSpace(grade))
                    {
                        "Cannot Stop current process because Grade is not calculated."
                            .ShowMessageBox(true);
                        return;
                    }

                    // Show Confirm grade.
                    ConfirmGradeInfo confirmResult =
                        this.ShowConfirmGradeBox(grade, _session);

                    if (null != confirmResult)
                    {
                        if (confirmResult.IsConfirm)
                        {
                            // Update Grade
                            txtGrade.Text = confirmResult.Grade;
                            ChangeGradeBoxColors();

                            // เพิ่ม ins_getNetLength หลังจาก ConfirmGrade
                            decimal netLength = GetNetLength(counterVal, confirmResult.Grade);

                            // Success Confirm. End current session.
                            // เพิ่มส่งค่า netlength
                            _session.End(counterVal, netLength, confirmResult.Grade,txtLoadingType.Text,"Y");

                            if (!string.IsNullOrEmpty(_session.FinishingLotNo)
                                  && !string.IsNullOrEmpty(_session.InspecionLotNo))
                            {
                                D365_IN();
                            }

                            cmdBack.IsEnabled = true;

                            //เพิ่มใหม่กันไม่ให้ key Defect
                            txtDefectPos.IsEnabled = false;
                           // txtDefectPos.Text = string.Empty;
                            txtDefectPos_Long.IsEnabled = false;
                            //txtDefectPos_Long.Text = string.Empty;
                            cmdLongDefectStart.IsEnabled = false;
                            cmdLongDefectEnd.IsEnabled = false;
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(confirmResult.ErrMessage))
                            {
                                confirmResult.ErrMessage.ShowMessageBox(true);
                            }
                            else "Grade is not confirm please comfirm grade to stop current process.".ShowMessageBox(true);
                        }
                    }
                    else
                    {
                        "Confirm Grade Error. No Result Returns.".ShowMessageBox(true);
                    }

                    #endregion
                }
            }
        }

        private void cmdNext_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                UpdateInspectionTypeToSession();

                decimal counterVal = Convert.ToDecimal(txtCounter.Text);
                string grade = txtGrade.Text;
                if (string.IsNullOrWhiteSpace(grade))
                {
                    grade = "A";
                }
                // End current session.
                _session.Next();

                ConmonReportService.Instance.CmdCountTestData = 0;
                insTestTemp = null;
            }
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            LogInInfo userToClear = this.ShowLogInBox(lbInsMC.Text, true);
            if (null != _session && null != userToClear)
            {
                // Reset Inspection Type
                chkReAdjustSameLot.IsChecked = false;
                chkReProcessChangeLot.IsChecked = false;
                //UpdateInspectionTypeToSession();
                _session.Clear(userToClear);

                cmdBack.IsEnabled = true;
                txtDefectPos.IsEnabled = false;

                P_FINISHINGLOT = string.Empty;
                P_INSPECTIONLOT = string.Empty;
                P_StartDate = null;

                PRODID = null;
                HEADERID = null;

                P_LOTNO = string.Empty;
                P_ITEMID = string.Empty;
                P_LOADINGTYPE = string.Empty;
            }
        }

        private void cmdSuspend_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                string msg = "Suspend for Inspection Lot : " + _session.InspecionLotNo + "";
                if (msg.ShowConfirmWindow())
                {
                    UpdateInspectionTypeToSession();

                    _session.Suspend();
                    // Back to Machine Selection Page.
                    PageManager.Instance.Back();
                }
            }
        }

        private void cmdRemark_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                UpdateInspectionTypeToSession();

                string remark = _session.GetRemark();
                if (null == remark)
                    remark = string.Empty;
                RemarkInfo remarkInfo = this.ShowRemarkBox(remark);
                if (null != remarkInfo)
                {
                    _session.AddRemark(remarkInfo.Remark);
                }
            }
        }

        #endregion

        #region Defect
        
        private void cmdLongDefectStart_Click(object sender, RoutedEventArgs e)
        {
            // Clear data when start.
            txtLongDefectStart.Text = string.Empty;
            txtLongDefectEnd.Text = string.Empty;

            //New Long
            txtDefectPos_Long.Text = string.Empty;
            txtDefectInfo_Long.Text = "-";
            txtDefectLength.Text = string.Empty;

            if (null != _session)
            {
                txtLongDefectStart.Text = txtCounter.Text;
                decimal counterVal = Convert.ToDecimal(txtCounter.Text);
                _session.StartLongDefect(counterVal);
            }
        }

        private void cmdLongDefectEnd_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                // ของเดิมดึงค่าจาก txtDefectPos.Text.Trim()
                //string text = txtDefectPos.Text.Trim();

                string text = txtDefectPos_Long.Text.Trim();

                if (string.IsNullOrWhiteSpace(text) || text.Length <= 2)
                {
                    // No Defect Pos.
                    "Please Enter Defect Position.".ShowMessageBox(false);
                    // Old
                    //txtDefectPos.SelectAll();
                    //txtDefectPos.Focus();

                    // New 
                    txtDefectPos_Long.SelectAll();
                    txtDefectPos_Long.Focus();

                    return;
                }

                if (txtDefectInfo_Long.Text != "-")
                {
                    txtLongDefectEnd.Text = txtCounter.Text;
                    decimal counterVal = Convert.ToDecimal(txtCounter.Text);
                    _session.EndLongDefect(counterVal, text);

                    //New 24/8/22 
                    if (_session.INS_GET100MDEFECTPOINTLongDefect(counterVal) > 10)
                    {
                        "Defect in 100 m Over 10 Point".ShowMessageBox(true);
                    }

                    //Old
                    //txtDefectPos.Text = string.Empty; // Clear
                    //txtDefectInfo.Text = string.Empty; // Clear

                    txtDefectPos_Long.Text = string.Empty;
                    //txtDefectInfo_Long.Text = string.Empty;
                    txtDefectInfo_Long.Text = "-";

                    CalDefectLength();
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

        private void cmdLongDefectCancel_Click(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.CancelLongDefect();
                // Clear data when cancel.
                txtLongDefectStart.Text = string.Empty;
                txtLongDefectEnd.Text = string.Empty;

                // New 
                txtDefectPos_Long.Text = string.Empty;
                txtDefectInfo_Long.Text = "-";
                txtDefectLength.Text = string.Empty;
            }
        }

        private void cmdShowDefectList_Click(object sender, RoutedEventArgs e)
        {
            // Show Defect List (Historical).
            if (null != _session)
            {
                if (chkReAdjustSameLot.IsChecked == true)
                {
                    if (txtTotalDefect.Text != "0" && !string.IsNullOrEmpty(txtTotalDefect.Text))
                    {
                        List<InspectionDefectItem> items = _session.GetDefectList();
                        this.ShowDefectListBox(items, _session.InspecionLotNo);

                        // เพิ่มเข้ามาเพื่อใช้สำหรับเมื่อมีการ ลบ Defect
                        UpdateSessionToControls();
                    }
                }
                else
                {
                    List<InspectionDefectItem> items = _session.GetDefectList();
                    this.ShowDefectListBox(items, _session.InspecionLotNo);

                    // เพิ่มเข้ามาเพื่อใช้สำหรับเมื่อมีการ ลบ Defect
                    UpdateSessionToControls();
                }
            }
        }

        #endregion

        #region Tests
        
        private void cmdHistory_Click(object sender, RoutedEventArgs e)
        {
            // Show tests history records
            if (null != _session)
            {
                List<InspectionTestHistoryItem> items = _session.GetTestHistoryList();
                //this.ShowInspectionRecordBox(items);

                this.ShowInspectionRecordBox(items, _session.InspecionLotNo.ToString(), _session.GetTestID());
            }
        }

        private void cmd100M_Click(object sender, RoutedEventArgs e)
        {
            ShowTestPopup();
        }

        #endregion

        #endregion

        #region TextBox Handlers

        #region Common
        
        private void Common_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = !this.IsNumericInput(e);
        }

        #endregion

        #region General

        private void txtItemLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                //txtCustomerType.Focus();
                //txtCustomerType.SelectAll();

                txtLoadingType.Focus();
                txtLoadingType.SelectAll();
                e.Handled = true;
            }
        }

        private void txtItemLotNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If Item/Inspection Lot No is not empty so lock type changed
            if (!string.IsNullOrWhiteSpace(txtItemLotNo.Text) ||
                !string.IsNullOrWhiteSpace(txtInsLotNo.Text))
            {
                EnableInsOptionControls(false);
            }
            else EnableInsOptionControls(true);
        }

        private void txtItemLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (txtItemLotNo.Text != "")
                {
                    _session.FinishingLotNo = txtItemLotNo.Text;
                }
                else
                {
                    _session.FinishingLotNo = "";
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

        private void txtCustomerType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (null != _session)
                {
                    _session.CustomerType = txtCustomerType.Text;
                }
                e.Handled = true;
            }
        }

        private void txtCustomerType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.CustomerType = txtCustomerType.Text;
            }
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                //txtTotalLen.Focus();
                //txtTotalLen.SelectAll();
                e.Handled = true;
            }
        }

        private void txtTotalLen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                e.Handled = true;
            }
        }

        private void txtInsLotNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            // If Item/Inspection Lot No is not empty so lock type changed
            if (!string.IsNullOrWhiteSpace(txtItemLotNo.Text) ||
                !string.IsNullOrWhiteSpace(txtInsLotNo.Text))
            {
                EnableInsOptionControls(false);
            }
            else EnableInsOptionControls(true);
        }

        private void txtInsLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                CheckInspectionLot();
                e.Handled = true;
            }
        }

        private void txtInsLotNo_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckInspectionLot();
            UpdateSessionToControls(); // Re update controls
        }

        private void txtLoadingType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                txtInsLotNo.SelectAll();
                txtInsLotNo.Focus();
                e.Handled = true;
            }
        }

        private void txtLoadingType_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.LOADTYPE = txtLoadingType.Text;
            }
        }

        #endregion

        #region Defect
        
        private void txtDefectPos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (null != _session &&
                    _session.State == InspectionSessionState.Started)
                {
                    string text = txtDefectPos.Text.Trim();

                    if (txtDefectInfo.Text != "-" && text.Length > 1)
                    {
                        _session.AddDefectCode(text, txtCounter.Text);

                        //New 24/8/22 
                        if (_session.INS_GET100MDEFECTPOINT(txtCounter.Text) > 10)
                        {
                            "Defect in 100 m Over 10 Point".ShowMessageBox(true);
                        }

                        // Update controls.
                        UpdateSessionToControls();

                        txtDefectPos.Text = string.Empty; // Clear
                        txtDefectInfo.Text = "-";
                    }
                }

                #region Original Code
                /*
                // Insert defect
                string text = txtDefectPos.Text.Trim();
                if (text.Length > 2)
                {
                    string insLot = txtInsLotNo.Text.Trim();
                    string code = text.Substring(0, 2).ToUpper();
                    string pos = text.Substring(2, text.Length - 2).ToUpper();
                    decimal len1 = Convert.ToDecimal(txtCounter.Text);
                    decimal? len2 = new decimal?();
                    decimal point = (decimal)1;
                    decimal? position = Convert.ToDecimal(pos);

                    string defId = InspectionDataService.Instance.InstInspectionLotDefect(
                        code, point, position, len1, len2, insLot);

                    if (!string.IsNullOrWhiteSpace(defId))
                    {
                        _defectIds.Add(defId); // Add to list
                        ++_defectCount; // increase counter
                    }

                    string itemCode = txtItemCode.Text.Trim();
                    string grade = InspectionDataService.Instance.GetGrade(
                        insLot, itemCode, len1, null);
                    if (string.IsNullOrWhiteSpace(grade))
                    {
                        grade = "A";
                    }
                    else
                    {
                        if (_defectCount > 10)
                            grade = "C";
                        else grade = "A";
                    }

                    txtGrade.Text = grade;
                    txtTotalDeflect.Text = _defectCount.ToString();
                }
                */
                #endregion

                e.Handled = true;
            }
        }

        private void txtDefectPos_KeyUp(object sender, KeyEventArgs e)
        {
            if (null != _session)
            {
                if (txtDefectPos.Text != "")
                {
                    string text = txtDefectPos.Text.Trim();

                    if (text.Length > 1)
                    {
                        string defText = _session.CheckDefectCode(text);

                        if (defText != "-")
                        {
                            txtDefectInfo.Text = defText;

                            if (text.Length > 2)
                            {
                                if (_session.CheckPosition(text) == false)
                                {
                                    "position invariant > 220".ShowMessageBox(true);
                                    txtDefectInfo.Text = "-";
                                    txtDefectPos.Text = "";
                                    txtDefectPos.Focus();
                                    txtDefectPos.SelectAll();
                                }
                            }
                        }
                        else
                        {
                            "Can't find Defect/Position".ShowMessageBox(true);
                            txtDefectInfo.Text = "-";
                            txtDefectPos.Text = "";
                            txtDefectPos.Focus();
                            txtDefectPos.SelectAll();
                        }
                    }
                    else
                    {
                        txtDefectInfo.Text = "-";
                    }
                }
                else
                {
                    txtDefectInfo.Text = "-";
                }
            }
        }

        private void txtDefectPos_Long_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (null != _session &&
                    _session.State == InspectionSessionState.Started)
                {
                    string text = txtDefectPos_Long.Text.Trim();

                    if (txtDefectInfo_Long.Text != "-" && text.Length > 1)
                    {
                        _session.AddDefectCode(text, txtCounter.Text);

                        // Update controls.
                        UpdateSessionToControls();

                        txtDefectPos_Long.Text = string.Empty; // Clear
                        txtDefectInfo_Long.Text = "-";
                    }
                }

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
                        string defText = _session.CheckDefectCode(text);
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
                e.Handled = true;
            }
        }

        // เพิ่มมาใหม่ Defect100
        private void txtDefect100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                if (txtDefect100.Text == "")
                {
                    txtDefect100.Text = "0";
                }

                CalDefect();

                txtDefectPos.Focus();
                txtDefectPos.SelectAll();
                e.Handled = true;
            }
        }

        #endregion

        #region Count

        decimal? countLength = null;

        private void txtCounter_LayoutUpdated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCounter.Text))
            {
                countLength = decimal.Parse(txtCounter.Text);

                if (chkConfirmstartLength == true)
                {
                    //Check confirmLength
                    if (countLength == confirmLength)
                    {
                        chkConfirmstartLength = false;
                        //Open confirmLength windows
                        ShowConfirmstartLengthPopup(confirmLength.ToString());
                    }
                }
                else if (chkConfirmstartLength == false)
                {
                    if (countLength >= 0 && countLength < (confirmLength - 1))
                    {
                        chkConfirmstartLength = true;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region RadioButton and CheckedBox

        private void rbMass_Checked(object sender, RoutedEventArgs e)
        {
            ValidateInspectionType();
        }

        private void rbMass_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rbTest_Checked(object sender, RoutedEventArgs e)
        {
            ValidateInspectionType();
        }

        private void rbTest_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void chkReAdjustSameLot_Checked(object sender, RoutedEventArgs e)
        {
            // Re Adjust (Same Lot) is checked.
            if (chkReProcessChangeLot.IsChecked.HasValue &&
                chkReProcessChangeLot.IsChecked != false)
            {
                // Checked Box cannot allow both checked
                chkReProcessChangeLot.IsChecked = false;
            }
            // Enable sub Radio Buttons
            rbReAdjustSameLotSameItem.IsEnabled = true;
            rbReAdjustSameLotSameItem.Opacity = 1;
            rbReAdjustSameLotDiffItem.IsEnabled = true;
            rbReAdjustSameLotDiffItem.Opacity = 1;
            // Set Checked
            rbReAdjustSameLotSameItem.IsChecked = true;
            rbReAdjustSameLotDiffItem.IsChecked = false;

            ValidateInspectionType();
        }

        private void chkReAdjustSameLot_Unchecked(object sender, RoutedEventArgs e)
        {
            // Re Adjust (Same Lot) is unchecked.
            rbReAdjustSameLotSameItem.IsChecked = false;
            rbReAdjustSameLotDiffItem.IsChecked = false;
            // Disable sub Radio Buttons
            rbReAdjustSameLotSameItem.IsEnabled = false;
            rbReAdjustSameLotSameItem.Opacity = 0.5;
            rbReAdjustSameLotDiffItem.IsEnabled = false;
            rbReAdjustSameLotDiffItem.Opacity = 0.5;
            // Set Checked
            rbReAdjustSameLotSameItem.IsChecked = false;
            rbReAdjustSameLotDiffItem.IsChecked = false;

            if (null != _session)
            {
                _session.ReAdjustMode = ReAdjustMode.None;
            }

            ValidateInspectionType();

            if (chkReAdjustSameLot.IsChecked == false &&
                chkReProcessChangeLot.IsChecked == false)
            {
                // Focus control
                txtItemLotNo.SelectAll();
                txtItemLotNo.Focus();
            }
        }

        private void rbReAdjustSameLotSameItem_Checked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.ReAdjustMode = ReAdjustMode.Same;
            }
            ValidateInspectionType();
            // Focus control
            txtInsLotNo.SelectAll();
            txtInsLotNo.Focus();
        }

        private void rbReAdjustSameLotSameItem_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void rbReAdjustSameLotDiffItem_Checked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.ReAdjustMode = ReAdjustMode.Diff;
            }

            txtDiff.IsEnabled = true;

            ValidateInspectionType();
            // Focus control
            txtInsLotNo.SelectAll();
            txtInsLotNo.Focus();
        }

        private void rbReAdjustSameLotDiffItem_Unchecked(object sender, RoutedEventArgs e)
        {
            txtDiff.IsEnabled = false;
            txtDiff.Text = string.Empty;
        }

        private void chkReProcessChangeLot_Checked(object sender, RoutedEventArgs e)
        {
            // Re Process (Change Lot) is checked.
            if (chkReAdjustSameLot.IsChecked.HasValue && 
                chkReAdjustSameLot.IsChecked != false)
            {
                // Checked Box cannot allow both checked
                chkReAdjustSameLot.IsChecked = false;
            }
            if (null != _session)
            {
                _session.ReProcessMode = ReProcessMode.ReProcess;
            }

            ValidateInspectionType();
            // Focus control
            txtInsLotNo.SelectAll();
            txtInsLotNo.Focus();
        }

        private void chkReProcessChangeLot_Unchecked(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                _session.ReProcessMode = ReProcessMode.None;
            }

            ValidateInspectionType();

            if (chkReAdjustSameLot.IsChecked == false &&
                chkReProcessChangeLot.IsChecked == false)
            {
                // Focus control
                txtItemLotNo.SelectAll();
                txtItemLotNo.Focus();
            }
        }

        #endregion

        #region cbGroup_LostFocus

        private void cbGroup_LostFocus(object sender, RoutedEventArgs e)
        {
            if (null != _session)
            {
                if (cbGroup.SelectedValue != null)
                    _session.OPERATOR_GROUP = cbGroup.SelectedValue.ToString();
            }
        }

        #endregion

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

        #region D365

        private void D365_IN()
        {
            try
            {
                P_FINISHINGLOT = _session.FinishingLotNo;
                P_INSPECTIONLOT = _session.InspecionLotNo;
                P_StartDate = _session.StartDate;

                string fin = string.Empty;
                string ins = string.Empty;
                string proid = string.Empty;


                if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                {
                    fin = "Finishing Lot = " + P_FINISHINGLOT;
                    fin.Info();
                }
                else
                {
                    "Finishing Lot is null".Info();
                }


                if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                {
                    ins = "Inspection Lot = " + P_INSPECTIONLOT;
                    ins.Info();

                    if (D365_IN_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            proid = "PRODID = " + PRODID.ToString();
                            proid.Info();

                            if (PRODID != 0)
                            {
                                #region D365_IN_ISH
                                if (D365_IN_ISH(PRODID) == true)
                                {
                                    if (HEADERID != null)
                                    {
                                        if (D365_IN_ISL(HEADERID) == true)
                                        {
                                            if (D365_IN_OPH(PRODID) == true)
                                            {
                                                if (HEADERID != null)
                                                {
                                                    if (D365_IN_OPL(HEADERID) == true)
                                                    {
                                                        if (D365_IN_OUH(PRODID, false) == true)
                                                        {
                                                            if (HEADERID != null)
                                                            {
                                                                if (D365_IN_OUL(HEADERID, 0, false) == true)
                                                                {
                                                                    "Send D365 complete".Info();
                                                                }
                                                            }
                                                            else
                                                            {
                                                                "HEADERID is null".Info();
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    "HEADERID is null".Info();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        "HEADERID is null".Info();
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region D365_IN_OPH
                                if (D365_IN_OPH(PRODID) == true)
                                {
                                    if (HEADERID != null)
                                    {
                                        if (D365_IN_OPL(HEADERID) == true)
                                        {
                                            if (D365_IN_OUH(PRODID, false) == true)
                                            {
                                                if (HEADERID != null)
                                                {
                                                    if (D365_IN_OUL(HEADERID, 0, false) == true)
                                                    {
                                                        "Send D365 complete".Info();
                                                    }
                                                }
                                                else
                                                {
                                                    "HEADERID is null".Info();
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        "HEADERID is null".Info();
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }
                }
                else
                {
                    "Inspection Lot is null".Info();
                }
            }
            catch (Exception ex)
            {
                ex.Message.Err();
            }
        }

        private void D365_IN_New()
        {
            try
            {
                P_FINISHINGLOT = txtItemLotNo.Text;
                P_INSPECTIONLOT = txtInsLotNo.Text;

                string fin = string.Empty;
                string ins = string.Empty;
                string proid = string.Empty;

                if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                {
                    fin = "Finishing Lot = " + P_FINISHINGLOT;
                    fin.Info();
                }
                else
                {
                    "Finishing Lot is null".Info();
                }

                if (!string.IsNullOrEmpty(P_INSPECTIONLOT))
                {
                    ins = "Inspection Lot = " + P_INSPECTIONLOT;
                    ins.Info();

                    if (D365_IN_BPO() == true)
                    {
                        if (PRODID != null)
                        {
                            proid = "PRODID = " + PRODID.ToString();
                            proid.Info();

                            if (D365_IN_OUH(PRODID, true) == true)
                            {
                                if (HEADERID != null)
                                {
                                    if (D365_IN_OUL(HEADERID, 1, true) == true)
                                    {
                                        "Send D365 complete".Info();
                                    }
                                }
                                else
                                {
                                    "HEADERID is null".Info();
                                }
                            }
                        }
                        else
                        {
                            "PRODID is null".Info();
                        }
                    }
                }
                else
                {
                    "Inspection Lot is null".Info();
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
                "D365_IN_BPO".Info();
                results = D365DataService.Instance.D365_IN_BPO(P_FINISHINGLOT, P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string pid = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].PRODID != null)
                        {
                            PRODID = Convert.ToInt64(results[i].PRODID);
                            pid = "PRODID = " + PRODID.ToString();
                            pid.Info();
                        }
                        else
                        {
                            PRODID = null;
                            "PRODID is null".Info();
                        }

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
                                "Insert_ABBPO".Info();

                                chkError = D365DataService.Instance.Insert_ABBPO(PRODID, results[i].LOTNO, results[i].ITEMID, results[i].LOADINGTYPE, 0, "N", results[i].QTY, results[i].UNIT, results[i].OPERATION, results[i].MACHINESTART);

                                if (!string.IsNullOrEmpty(chkError))
                                {
                                    chkError.Err();
                                    chkError.ShowMessageBox();
                                    chkD365 = false;
                                    break;
                                }
                            }
                            else
                            {
                                "PRODID = 0".Info();
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

                "D365_IN_ISH".Info();
                results = D365DataService.Instance.D365_IN_ISH(P_FINISHINGLOT, P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string hid = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            hid = "HEADERID = " + HEADERID.ToString();
                            hid.Info();

                            "Insert_ABISH".Info();

                            chkError = D365DataService.Instance.Insert_ABISH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }
                        else
                        {
                            "HEADERID is null".Info();
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
                string hid = string.Empty;

                if (HEADERID != null)
                {
                    hid = "HEADERID = " + HEADERID.ToString();
                    hid.Info();
                }
                else
                {
                    "HEADERID is null".Info();
                }

                List<ListD365_IN_ISLData> results = new List<ListD365_IN_ISLData>();

                "D365_IN_ISL".Info();
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

                        "Insert_ABISL".Info();
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

                "D365_IN_OPH".Info();

                results = D365DataService.Instance.D365_IN_OPH(P_INSPECTIONLOT);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;
                    string hid = string.Empty;

                    foreach (var row in results)
                    {
                        if (results[i].HEADERID != null)
                            HEADERID = Convert.ToInt64(results[i].HEADERID);
                        else
                            HEADERID = null;

                        if (HEADERID != null)
                        {
                            hid = "HEADERID = "+HEADERID.ToString();
                            hid.Info();

                            "Insert_ABOPH".Info();

                            chkError = D365DataService.Instance.Insert_ABOPH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);

                            if (!string.IsNullOrEmpty(chkError))
                            {
                                chkError.Err();
                                chkError.ShowMessageBox();
                                chkD365 = false;
                                break;
                            }
                        }
                        else
                        {
                            "HEADERID is null".Info();
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

                "D365_IN_OPL".Info();

                results = D365DataService.Instance.D365_IN_OPL(P_INSPECTIONLOT, P_StartDate);

                if (results.Count > 0)
                {
                    int i = 0;
                    string chkError = string.Empty;

                    foreach (var row in results)
                    {
                        "Insert_ABOPL".Info();

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
        private bool D365_IN_OUH(long? PRODID,bool chkNew)
        {
            bool chkD365 = true;

            try
            {
                List<D365_IN_OUHData> results = new List<D365_IN_OUHData>();

                "D365_IN_OUH".Info();

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
                            if (chkNew == false)
                            {
                                "Insert_ABOUH action = N".Info();
                                chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "N", 0, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);
                            }
                            else
                            {
                                "Insert_ABOUH action = U".Info();
                                chkError = D365DataService.Instance.Insert_ABOUH(HEADERID, PRODID, "U", 1, results[i].TOTALRECORD, P_LOTNO, P_ITEMID, P_LOADINGTYPE, results[i].MACHINESTART);
                            }

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
        private bool D365_IN_OUL(long? HEADERID, decimal? P_FINISH, bool chkNew)
        {
            bool chkD365 = true;

            try
            {
                List<ListD365_IN_OUL_AUTOData> results = new List<ListD365_IN_OUL_AUTOData>();

                "D365_IN_OUL_AUTO".Info();

                results = D365DataService.Instance.D365_IN_OUL_AUTO(P_FINISHINGLOT, P_INSPECTIONLOT, P_FINISH);

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

                        if (chkNew == false)
                        {
                            "Insert_ABOUL action = N".Info();

                            chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "N", 0, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                                , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);
                        }
                        else
                        {
                            "Insert_ABOUL action = U".Info();

                            chkError = D365DataService.Instance.Insert_ABOUL(HEADERID, results[i].LINENO, "U", 1, outputDate, results[i].ITEMID, results[i].QTY, results[i].UNIT, results[i].GROSSLENGTH, results[i].NETLENGTH
                            , results[i].GROSSWEIGHT, results[i].NETWEIGHT, results[i].PALLETNO, results[i].GRADE, results[i].SERIALID, results[i].LOADINGTYPE, finish, results[i].MOVEMENTTRANS, results[i].WAREHOUSE, results[i].LOCATION);
                        }

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

        #region Preview

        private void Preview(string insLotNo, string grade, decimal? netLenge, decimal? grossLength)
        {
            try
            {
                // ConmonReportService
                ConmonReportService.Instance.CmdString = insLotNo;
                ConmonReportService.Instance.GRADE = grade;
                ConmonReportService.Instance.NetLenge = netLenge;
                ConmonReportService.Instance.GrossLength = grossLength;

                ConmonReportService.Instance.ReportName = "TestInspection";

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
        /// <param name="session">The inspection session.</param>
        /// <param name="suspendData">The suspend data.</param>
        public void Setup(InspectionSession session, 
            Domains.INS_GETMCSUSPENDDATAResult suspendData)
        {
            _session = session;
            // Init Session
            InitSession();
            if (null != suspendData && null != _session)
            {
                session.Resume(suspendData.FINISHINGLOT,
                    suspendData.ITEMCODE, suspendData.INSPECTIONLOT,
                    suspendData.CUSTOMERTYPE,
                    suspendData.INSPECTIONID, suspendData.DEFECTID, suspendData.TESTRECORDID,
                    suspendData.NETLENGTH, suspendData.GROSSLENGTH);

            }
        }

        #endregion

    }
}
