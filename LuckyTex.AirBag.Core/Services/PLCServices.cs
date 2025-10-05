#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion

using System.Threading;
using NLib;
using NLib.Devices.SerialPorts;
using NLib.Components.Devices.SerialPorts;

namespace LuckyTex.Services
{
    #region PLC event hander and event args

    /// <summary>
    /// The PLCDataArrivedEventHandler delegate.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    public delegate void PLCDataArrivedEventHandler(object sender, PLCDataArrivedEventArgs e);
    /// <summary>
    /// The PLCDataArrivedEventArgs class.
    /// </summary>
    public class PLCDataArrivedEventArgs
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public PLCDataArrivedEventArgs() : this(decimal.Zero) { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="val">The value.</param>
        public PLCDataArrivedEventArgs(decimal val)
        {
            this.Value = val;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets value.
        /// </summary>
        public decimal Value { get; set; }

        #endregion
    }

    #endregion

    #region Inspection PLC Service

    /// <summary>
    /// The PLC service for Inspection process.
    /// </summary>
    public class InspectionPLCService
    {
        #region Singelton

        private static InspectionPLCService _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static InspectionPLCService Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(InspectionPLCService))
                    {
                        _instance = new InspectionPLCService();
                    }

                }
                return _instance;
            }
        }

        #endregion

        #region Enums
        
        /// <summary>
        /// CommPortStatus.
        /// </summary>
        private enum CommPortStatus
        {
            /// <summary>
            /// No setting.
            /// </summary>
            None,
            /// <summary>
            /// Port Name not avaliable.
            /// </summary>
            NotAvaliable,
            /// <summary>
            /// Port Name is exists.
            /// </summary>
            Avaliable
        }

        #endregion

        #region Internal Variables

        private SerialPortAccess _comAccess = null;
        private System.IO.Ports.SerialPort comPort = null; // For Reset port.
        private SerialPortSettings _settings = null;

        private string _rxData = string.Empty;
        private Queue<string> _rxQueues = new Queue<string>();

        private bool _isStart = false;
        private CommPortStatus _portStatus = CommPortStatus.None;

        private Thread _thCheckPortAvaliable = null;
        private DateTime _lastCheckPortAvaliableTime = DateTime.Now;
        private int _CheckPortInterVal = 450;

        private bool bfirst = true;
        private Thread _thRequestData = null;
        private DateTime _lastRequestDataTime = DateTime.Now;
        private int _RequestDataInterVal = 250;

        private Thread _thProcessRxData = null;
        private DateTime _lastProcessRxDataTime = DateTime.Now;
        private int _RxDataInterVal = 150;

        private string ConfigFileName
        {
            get
            {
                return AirBagConsts.GetShareConfigFullName("InsPLCConfig.xml");
            }
        }

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private InspectionPLCService() : base()
        {

        }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~InspectionPLCService()
        {
            this.Stop();
        }

        #endregion

        #region Private Methods

        #region Init/Release Comm

        private void InitComm()
        {
            bfirst = true;

            _comAccess = new SerialPortAccess();

            if (!System.IO.File.Exists(this.ConfigFileName))
            {
                _comAccess.Settings.Common.AutoReopen = true;
                _comAccess.Settings.Common.Port = "COM3:";
                _comAccess.Settings.Common.BaudRate = 9600;
                _comAccess.Settings.Common.DataBits = 8;
                _comAccess.Settings.Common.Parity = NLib.Devices.SerialPorts.Parity.none;
                _comAccess.Settings.Common.StopBits = NLib.Devices.SerialPorts.StopBits.one;
                //_comAccess.Settings.Common.CheckAllSends = true;

                //_comAccess.Settings.Tx.TxWhenRxXoff = true;
                //_comAccess.Settings.Rx.RxFlowX = false;

                _comAccess.SaveSetting(this.ConfigFileName);
            }
            else
            {
                _comAccess.LoadSetting(this.ConfigFileName);
            }
            //_comAccess.Settings.Common.Port = "COM3:";
            _comAccess.Immediate = false;
            _comAccess.OnTerminalRx += new TerminalRxCharEventHandler(_comAccess_OnTerminalRx);

            // keep settings
            _settings = _comAccess.Settings;
        }

        private void ReleaseComm()
        {
            if (null != _comAccess)
            {
                _comAccess.OnTerminalRx -= new TerminalRxCharEventHandler(_comAccess_OnTerminalRx);
                if (_comAccess.Online)
                {
                    try 
                    {
                        _comAccess.Close(); 
                    }
                    catch (Exception ex) { ex.Err(); }
                }

                try 
                { 
                    _comAccess.Dispose(); 
                }
                catch (Exception ex) { ex.Err(); }
            }
            _comAccess = null;
        }

        #endregion

        #region OnTerminalRx Handler

        void _comAccess_OnTerminalRx(string ch, bool newline)
        {
            _rxData += ch;
            if (!string.IsNullOrWhiteSpace(_rxData) && _rxData.EndsWith("<CR>"))
            {
                if (!string.IsNullOrWhiteSpace(_rxData) && _rxData.Trim() != "<CR>")
                {
                    lock (this)
                    {                        
                        if (null != _rxQueues && _rxQueues.Count <= 0)
                        {
                            _rxQueues.Enqueue(_rxData.Trim());
                        }
                    }
                }
                _rxData = string.Empty; // wait for new command
            }
        }

        #endregion

        #region Open/Close Comm

        private void Open()
        {
            try
            {
                if (null == _comAccess)
                {
                    InitComm();
                }
            }
            catch (Exception ex) { ex.Err(); }

            if (null != _comAccess)
            {
                try
                {
                    if (!_comAccess.Online)
                    {
                        _comAccess.Open();
                    }
                }
                catch (Exception ex) { ex.Err(); }
            }
        }

        private void Close()
        {
            try
            {
                if (null != _comAccess)
                {
                    if (_comAccess.Online)
                    {
                        _comAccess.Close();
                    }
                }
                ReleaseComm();
            }
            catch (Exception ex) { ex.Err();  }
        }

        #endregion

        #region Check Port Avaliable related methods

        /// <summary>
        /// Gets current port avaliable status.
        /// </summary>
        private CommPortStatus PortStatus { get { return _portStatus; } }

        private bool _onChcekPort = false;

        private void CheckAvaliablePorts()
        {
            if (_onChcekPort)
                return;

            _onChcekPort = true;

            #region Check setting

            if (null == _settings || null == _settings.Common)
            {
                // Read setting.
                InitComm();
                ReleaseComm();
            }

            if (null == _settings || null == _settings.Common)
            {
                _portStatus = CommPortStatus.None;

                _onChcekPort = false;

                return;
            }

            #endregion

            #region Check avaliable port

            try
            {
                string[] portNames = System.IO.Ports.SerialPort.GetPortNames();
                if (null != portNames && portNames.Length > 0)
                {
                    string targetPort = _settings.Common.Port.Replace(":", string.Empty);
                    _portStatus = CommPortStatus.NotAvaliable;
                    foreach (string portName in portNames)
                    {
                        if (portName == targetPort)
                        {
                            _portStatus = CommPortStatus.Avaliable;
                            break;
                        }
                    }
                }
                else _portStatus = CommPortStatus.NotAvaliable;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            #endregion

            #region Auto Open/Close port
            
            if (PortStatus == CommPortStatus.None ||
                PortStatus == CommPortStatus.NotAvaliable)
            {
                if (_isStart)
                {
                    try
                    {

                        if (null != _comAccess || _comAccess.Online)
                        {
                            this.Close(); // Close port.
                        }
                    }
                    catch (Exception ex) { ex.Err(); }
                }
            }
            else
            {
                if (_isStart)
                {
                    try
                    {
                        if (null == _comAccess || !_comAccess.Online)
                        {
                            this.Open(); // Open port (automatically).
                        }
                    }
                    catch (Exception ex) { ex.Err(); }
                }
            }

            #endregion

            _onChcekPort = false;
        }

        #endregion

        #region Request Data method(s)

        private bool _onRequestData = false;

        private void RequestData()
        {
            if (_onRequestData)
                return;

            _onRequestData = true;

            if (_onProcesssRx)
            {
                _onRequestData = false;
                return; // If in Process Rx wait until finished.
            }

            if (null != _comAccess && _comAccess.Online)
            {
                try
                {
                    if (bfirst)
                    {
                        _comAccess.Send(new byte[] { 0x6c, 0x0d }, false);
                        bfirst = false;
                    }
                    else
                    {
                        // Old ถ้าไม่สามารถใช้งานได้ให้เอา Old กลับมาใช้
                        //_comAccess.Send(new byte[] { 0x6c }, false);
                        // New
                        _comAccess.Send(new byte[] { 0x6c, 0x0d }, false);
                    }
                }
                catch (Exception ex) { ex.Err(); }
            }

            _onRequestData = false;
        }

        #endregion

        #region Process Rx Data method(s)
        
        private void DoDataArrived(string val)
        {
            if (!string.IsNullOrWhiteSpace(val))
            {
                decimal mcounter = decimal.Zero;
                try
                {
                    mcounter = Convert.ToDecimal(val);
                    if (null != OnDataArrived)
                    {
                        OnDataArrived.Call(this, new PLCDataArrivedEventArgs(mcounter));
                        ApplicationManager.Instance.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    ex.Err();
                    mcounter = decimal.Zero;
                }
            }
        }

        private bool _onProcesssRx = false;

        private void ProcessRxData()
        {
            if (_onProcesssRx)
                return;

            _onProcesssRx = true;

            if (null != _rxQueues && _rxQueues.Count > 0)
            {
                string cmd = string.Empty;
                lock (this)
                {
                    cmd = _rxQueues.Dequeue();
                }

                if (string.IsNullOrWhiteSpace(cmd))
                {
                    _onProcesssRx = false;
                    return;
                }

                if (cmd.Trim().StartsWith("<LF>"))
                {
                    string val = cmd.Trim()
                        .Replace("<LF>", string.Empty)
                        .Replace("<CR>", string.Empty)
                        .Replace("+", string.Empty)
                        .Replace("m", string.Empty)
                        .Replace("y", string.Empty)
                        .Trim();
                    // Check data and raise event.
                    DoDataArrived(val);
                }
            }

            _onProcesssRx = false;
        }

        #endregion

        #region Check port avaliable (thread)

        private void InitCheckPortThread()
        {
            if (null != _thCheckPortAvaliable)
                return;
            if (null == _thCheckPortAvaliable)
            {
                _thCheckPortAvaliable = new Thread(this.CheckPortProcessing);
                _thCheckPortAvaliable.Priority = ThreadPriority.BelowNormal;
                _thCheckPortAvaliable.IsBackground = true;

                _thCheckPortAvaliable.Start();
            }
        }

        private void ReleaseCheckPortThread()
        {
            if (null != _thCheckPortAvaliable)
            {
                try { _thCheckPortAvaliable.Abort(); }
                catch (ThreadAbortException) { Thread.ResetAbort(); }
            }
            _thCheckPortAvaliable = null;
        }

        private void CheckPortProcessing()
        {
            while (null != _thCheckPortAvaliable && 
                !ApplicationManager.Instance.IsExit)
            {
                TimeSpan ts = DateTime.Now - _lastCheckPortAvaliableTime;
                if (ts.TotalMilliseconds > _CheckPortInterVal)
                {
                    try
                    {
                        CheckAvaliablePorts();
                    }
                    catch (Exception ex) { ex.Err(); }

                    ApplicationManager.Instance.DoEvents();
                    _lastCheckPortAvaliableTime = DateTime.Now;
                }
                Thread.Sleep(50);
                ApplicationManager.Instance.DoEvents();
            }
        }

        #endregion

        #region Request Data (thread)

        private void InitRequestDataThread()
        {
            if (null != _thRequestData)
                return;
            if (null == _thRequestData)
            {
                _thRequestData = new Thread(this.RequestDataProcessing);
                _thRequestData.Priority = ThreadPriority.BelowNormal;
                _thRequestData.IsBackground = true;
                
                _thRequestData.Start();
            }
        }

        private void ReleaseRequestDataThread()
        {
            if (null != _thRequestData)
            {
                try { _thRequestData.Abort(); }
                catch (ThreadAbortException) { Thread.ResetAbort(); }
            }
            _thRequestData = null;
        }

        private void RequestDataProcessing()
        {
            while (null != _thRequestData && 
                !ApplicationManager.Instance.IsExit)
            {
                TimeSpan ts = DateTime.Now - _lastRequestDataTime;
                if (ts.TotalMilliseconds > _RequestDataInterVal)
                {                    
                    try
                    {
                        RequestData();
                    }
                    catch (Exception ex) { ex.Err(); }

                    ApplicationManager.Instance.DoEvents();
                    _lastRequestDataTime = DateTime.Now;
                }
                Thread.Sleep(50);
                ApplicationManager.Instance.DoEvents();
            }
        }

        #endregion

        #region Process Rx Data (thread)

        private void InitProcessRxDataThread()
        {
            if (null != _thProcessRxData)
                return;
            if (null == _thProcessRxData)
            {
                _thProcessRxData = new Thread(this.RxDataProcessing);
                _thProcessRxData.Priority = ThreadPriority.BelowNormal;
                _thProcessRxData.IsBackground = true;

                _thProcessRxData.Start();
            }
        }

        private void ReleaseProcessRxDataThread()
        {
            if (null != _thProcessRxData)
            {
                try { _thProcessRxData.Abort(); }
                catch (ThreadAbortException) { Thread.ResetAbort(); }
            }
            _thProcessRxData = null;
        }

        private void RxDataProcessing()
        {
            while (null != _thProcessRxData &&
                !ApplicationManager.Instance.IsExit)
            {
                TimeSpan ts = DateTime.Now - _lastProcessRxDataTime;
                if (ts.TotalMilliseconds > _RxDataInterVal)
                {
                    try
                    {
                        ProcessRxData();
                    }
                    catch (Exception ex) { ex.Err(); }

                    ApplicationManager.Instance.DoEvents();
                    _lastProcessRxDataTime = DateTime.Now;
                }
                Thread.Sleep(50);
                ApplicationManager.Instance.DoEvents();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        /// <summary>
        /// Start listening com port.
        /// </summary>
        public void Start()
        {
            if (_isStart)
                return;
            _isStart = true;

            InitCheckPortThread();
            InitRequestDataThread();
            InitProcessRxDataThread();
        }
        /// <summary>
        /// Stop listening com port.
        /// </summary>
        public void Stop()
        {
            _isStart = false;

            ReleaseProcessRxDataThread();
            ReleaseRequestDataThread();
            ReleaseCheckPortThread();

            this.Close();
        }

        #region Testing and Reset
        
        /// <summary>
        /// Send Garbage. For testing purpose only.
        /// </summary>
        public void SendGarbage()
        {
            try
            {
                if (null != _comAccess && _comAccess.Online)
                {
                    _comAccess.Send(new byte[] { 0x69, 0xAA }, false);
                }
            }
            catch (Exception ex) { ex.Err(); }
        }
        /// <summary>
        /// Reset Port.
        /// </summary>
        public void ResetPort()
        {
            string portName = string.Empty;
            if (null != _settings && null != _settings.Common)
            {
                portName = _settings.Common.Port.Replace(":", string.Empty);
            }

            this.Stop();

            if (!string.IsNullOrWhiteSpace(portName) &&
                null != _settings && null != _settings.Common)
            {
                #region Clone settings

                try
                {
                    comPort = new System.IO.Ports.SerialPort();
                    comPort.PortName = portName;
                    comPort.BaudRate = _settings.Common.BaudRate;
                    comPort.DataBits = (int)_settings.Common.DataBits;

                    if (_settings.Common.StopBits == StopBits.one)
                        comPort.StopBits = System.IO.Ports.StopBits.One;
                    else if (_settings.Common.StopBits == StopBits.two)
                        comPort.StopBits = System.IO.Ports.StopBits.Two;
                    else if (_settings.Common.StopBits == StopBits.onePointFive)
                        comPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                    else comPort.StopBits = System.IO.Ports.StopBits.None;

                    if (_settings.Common.Parity == Parity.even)
                        comPort.Parity = System.IO.Ports.Parity.Even;
                    else if (_settings.Common.Parity == Parity.mark)
                        comPort.Parity = System.IO.Ports.Parity.Mark;
                    else if (_settings.Common.Parity == Parity.odd)
                        comPort.Parity = System.IO.Ports.Parity.Odd;
                    else if (_settings.Common.Parity == Parity.space)
                        comPort.Parity = System.IO.Ports.Parity.Space;
                    else comPort.Parity = System.IO.Ports.Parity.None;
                }
                catch (Exception ex) { ex.Err(); }

                #endregion

                #region Open, test and close

                try
                {
                    comPort.Open();
                    // Send request data
                    comPort.Write(new byte[] { 0x6c, 0x0d }, 0, 2);

                    comPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(comPort_DataReceived);
                }
                catch (Exception ex)
                {
                    ex.Err();
                }

                ApplicationManager.Instance.Wait(1000); // wait 1000 ms.

                if (null != comPort)
                {
                    try
                    {
                        comPort.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(comPort_DataReceived);
                        comPort.Close();
                        comPort.Dispose();
                    }
                    catch (Exception ex)
                    {
                        ex.Err();
                    }
                }

                comPort = null;

                #endregion
            }

            this.Start();
        }

        void comPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string InputData = comPort.ReadExisting();
                if (!string.IsNullOrWhiteSpace(InputData))
                {
                    Console.WriteLine("{0}", InputData);
                }
                else
                {
                    Console.WriteLine("No data returns.");
                }
            }
            catch (Exception ex) { ex.Err(); }
        }

        #endregion

        #endregion

        #region Public Properties

        /// <summary>
        /// Checks is processing thread is running.
        /// </summary>
        public bool IsRunning 
        { 
            get 
            { 
                return (null != _thRequestData && null != _thProcessRxData); } 
            }
        /// <summary>
        /// Checks is comm opened.
        /// </summary>
        public bool IsOnline 
        { 
            get 
            { 
                return (null != _comAccess && _comAccess.Online); 
            } 
        }

        #endregion

        #region Public Events

        /// <summary>
        /// OnDataArrived event.
        /// </summary>
        public event PLCDataArrivedEventHandler OnDataArrived;

        #endregion
    }

    #endregion
}
