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

using System.IO;
using NLib;
using NLib.Components.Devices.SerialPorts;

namespace InspectionPLCSim
{
    public partial class Form1 : Form
    {
        #region Constrctor
        
        /// <summary>
        /// Constrctor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Variables

        private decimal _totalLength = decimal.Zero;
        private decimal _currLength = decimal.Zero;
        private decimal _increment = decimal.Zero;
        private decimal _startLength = decimal.Zero;

        private DateTime _lastUpdate = DateTime.Now;
        private SerialPortAccess _comAccess = null;

        private string _command = string.Empty;
        private Queue<string> _commands = new Queue<string>();

        /// <summary>
        /// Gets Share config folder.
        /// </summary>
        private string ShareConfigFolder
        {
            get
            {
                return ApplicationManager.Instance.Environments.Company.Configs.FullName;
            }
        }

        private string comConfigFile 
        {
            get { return Path.Combine(this.ShareConfigFolder, "InsPLCSimCommConfig.xml"); }
        }

        #endregion

        #region Private Methods

        private void InitCom()
        {
            _commands.Clear();

            _comAccess = new SerialPortAccess();

            if (!System.IO.File.Exists(comConfigFile))
            {
                _comAccess.Settings.Common.AutoReopen = true;
                _comAccess.Settings.Common.Port = "COM6:";
                _comAccess.Settings.Common.BaudRate = 9600;
                _comAccess.Settings.Common.DataBits = 8;
                _comAccess.Settings.Common.Parity = NLib.Devices.SerialPorts.Parity.none;
                _comAccess.Settings.Common.StopBits = NLib.Devices.SerialPorts.StopBits.one;

                //_comAccess.Settings.Tx.TxWhenRxXoff = false;
                //_comAccess.Settings.Common.CheckAllSends = false;

                _comAccess.SaveSetting(comConfigFile);
            }
            else
            {
                _comAccess.LoadSetting(comConfigFile);
            }

            _comAccess.OnTerminalRx += new TerminalRxCharEventHandler(_comAccess_OnTerminalRx);
            try { _comAccess.Open(); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

            timer2.Start();
        }

        void _comAccess_OnTerminalRx(string ch, bool newline)
        {
            _command += ch;
            if (!string.IsNullOrWhiteSpace(_command) && _command.EndsWith("<CR>"))
            {
                if (!string.IsNullOrWhiteSpace(_command) && _command.Trim() != "<CR>")
                {
                    lock (this)
                    {
                        if (null != _commands && _commands.Count <= 0)
                        {
                            _commands.Enqueue(_command.Trim());
                        }
                    }
                }
                _command = string.Empty; // wait for new command
            }
            else if (!string.IsNullOrWhiteSpace(_command) && _command.StartsWith("l"))
            {
                lock (this)
                {
                    if (null != _commands && _commands.Count <= 0)
                    {
                        _commands.Enqueue(_command.Trim());
                    }
                }
                _command = string.Empty; // wait for new command
            }
        }

        private void ReleaseCom()
        {
            timer2.Stop();

            _commands.Clear();

            if (null != _comAccess)
            {
                _comAccess.OnTerminalRx -= new TerminalRxCharEventHandler(_comAccess_OnTerminalRx);
                try { _comAccess.Close(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            _comAccess = null;
        }

        private void EnableInput()
        {
            cmdStart.Enabled = true;
            cmdStop.Enabled = !cmdStart.Enabled;
            cmdSet.Enabled = cmdStart.Enabled;
            cmdClear.Enabled = cmdStart.Enabled;
            txtTotalLength.Enabled = cmdStart.Enabled;
            txtIncrement.Enabled = cmdStart.Enabled;
            txtStartLength.Enabled = cmdStart.Enabled;
        }

        private void DisableInput()
        {
            cmdStart.Enabled = false;
            cmdStop.Enabled = !cmdStart.Enabled;
            cmdSet.Enabled = cmdStart.Enabled;
            cmdClear.Enabled = cmdStart.Enabled;
            txtTotalLength.Enabled = cmdStart.Enabled;
            txtIncrement.Enabled = cmdStart.Enabled;
            txtStartLength.Enabled = cmdStart.Enabled;
        }

        private void InitLengths()
        {
            try
            {
                _totalLength = Convert.ToDecimal(txtTotalLength.Text);
                _startLength = Convert.ToDecimal(txtStartLength.Text);
                _currLength = _startLength;
                _increment = Convert.ToDecimal(txtIncrement.Text);

                lbActualLength.Text = _currLength.ToString("n2");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearLengths()
        {
            txtTotalLength.Text = "0";
            txtIncrement.Text = "1";
            txtStartLength.Text = "0";
            lbActualLength.Text = "0";
            // Update variables
            InitLengths();
        }

        #endregion

        #region Form Load/Closing

        private void Form1_Load(object sender, EventArgs e)
        {
            EnableInput();
            InitCom();
            InitLengths();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            EnableInput();
            ReleaseCom();
        }

        #endregion

        #region Timer Events

        private bool _onUpdating1 = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_onUpdating1)
                return;
            
            _onUpdating1 = true;

            TimeSpan ts = DateTime.Now - _lastUpdate;
            if (ts.TotalMilliseconds >= 150)
            {
                if (_currLength < _totalLength)
                {
                    _currLength += _increment;
                    lbActualLength.Text = _currLength.ToString("n2");
                }
                else
                {
                    // same length reach. Stop timer.
                    timer1.Stop();
                    EnableInput();
                }

                _lastUpdate = DateTime.Now;
            }
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();

            _onUpdating1 = false;
        }

        private bool _onUpdating2 = false;

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (_onUpdating2)
                return;

            _onUpdating2 = true;

            TimeSpan ts = DateTime.Now - _lastUpdate;
            if (ts.TotalMilliseconds >= 150)
            {
                if (null != _commands && _commands.Count > 0 &&
                    null != _comAccess && _comAccess.Online)
                {
                    string cmd = string.Empty;
                    lock (this)
                    {
                        cmd = _commands.Dequeue();
                    }

                    if (!string.IsNullOrWhiteSpace(cmd) && 
                        (cmd == "l<CR>" || cmd == "l"))
                    {
                        string result = "<LF>";
                        result += (_currLength >= 0) ? "+" : "-";
                        string aLen = _currLength.ToString("n2")
                            .Replace(",", string.Empty)
                            .Replace("-", string.Empty)
                            .Replace("+", string.Empty);
                        int index = aLen.IndexOf(".");
                        string aPart = aLen.Substring(0, index).Replace(".", string.Empty);
                        string bPart = aLen.Replace(aPart, string.Empty).Replace(".", string.Empty);
                        int iaPart = (!string.IsNullOrWhiteSpace(aPart)) ? 
                            Convert.ToInt32(aPart) : 0;
                        int ibPart = (!string.IsNullOrWhiteSpace(bPart)) ? 
                            Convert.ToInt32(bPart) : 0;
                        string sLen =
                            iaPart.ToString("D5") + "." +
                            ibPart.ToString("D2") + "m" + "   ";

                        result += sLen;
                        result += "<CR>";

                        _comAccess.Send(result, false);
                    }
                }
            }
            System.Threading.Thread.Sleep(50);
            Application.DoEvents();

            _onUpdating2 = false;
        }

        #endregion

        #region Button Events
        
        private void cmdStart_Click(object sender, EventArgs e)
        {
            DisableInput();
            timer1.Start();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            EnableInput();
        }

        private void cmdSet_Click(object sender, EventArgs e)
        {
            InitLengths();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            ClearLengths();
        }

        #endregion
    }
}
