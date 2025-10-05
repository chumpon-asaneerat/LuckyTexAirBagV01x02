#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

#endregion

using NLib;
using NLib.Xml;
using NLib.Devices.Modbus;
using LuckyTex.Models;
using LuckyTex.Services;

namespace TestModbusSlave
{
    /// <summary>
    /// Modbus Tcp Slave Main form.
    /// </summary>
    public partial class Form1 : Form
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private ModbusTcpIpSlave _slave = null;
        private ModbusTcpIpMaster _master = null;
        // Buffers
        private ModbusBuffer _readBuffer = null;
        private ModbusBuffer _writeBuffer = null;

        #endregion

        #region Private Methods

        private void StartListen()
        {
            if (null != _slave)
                return;
            _slave = new ModbusTcpIpSlave();
            _slave.IP = txtIP.Text;
            _slave.Port = Convert.ToInt32(txtPort.Text);
            _slave.SlaveId = Convert.ToByte(txtSlaveId.Text);
            _slave.Start();

            txtStatus.Text = (null != _slave) ? 
                "Slave is running." : "Cannot create slave.";
        }

        private void StopListen()
        {
            if (null != _slave)
            {
                _slave.Shutdown();
            }
            _slave = null;

            txtStatus.Text = "-";
        }

        private void Connect()
        {
            if (null != _master)
            {
                return;
            }
            _master = new ModbusTcpIpMaster();
            _master.IP = txtIP.Text;
            _master.Port = Convert.ToInt32(txtPort.Text);
            _master.Connect();
        }

        private void Disconnect()
        {
            if (null != _master)
            {
                _master.Disconnect();
            }
            _master = null;
        }

        private void WriteData(ushort startAddr, params ushort[] inputs)
        {
            if (null == _master || !_master.IsConnected)
                Connect();

            if (!_master.IsConnected)
            {
                MessageBox.Show("Cannot connect to remote IP.");
                return;
            }

            if (null != _master && _master.IsConnected && null != inputs && inputs.Length > 0)
            {
                ushort numInput = Convert.ToUInt16(inputs.Length);
                _master.WriteHoldingRegisters(startAddr, inputs);
            }
        }

        private ushort[] ReadData(ushort startAddr, ushort numInput)
        {
            ushort[] results = null;

            if (null == _master || !_master.IsConnected)
                Connect();

            if (!_master.IsConnected)
            {
                MessageBox.Show("Cannot connect to remote IP.");
                return results;
            }

            if (null != _master && _master.IsConnected && numInput > 0)
            {
                ushort[] inputs = _master.ReadHoldingRegisters(startAddr, numInput);
                if (null != inputs && inputs.Length == numInput)
                {
                    results = inputs;
                }
            }

            return results;
        }

        private void InitReadBuffer()
        {
            if (null != _readBuffer)
                return;

            _readBuffer = new ModbusBuffer();
        }

        private void FreeReadBuffer()
        {
            _readBuffer = null;
        }

        private void InitWriteBuffer()
        {
            if (null != _writeBuffer)
                return;

            _writeBuffer = new ModbusBuffer();
        }

        private void FreeWriteBuffer()
        {
            _writeBuffer = null;
        }

        private void ReadData(int startAddr, int noOfInput)
        {
            gvCurrHoldingRegisters.DataSource = null;

            if (null == _readBuffer)
                InitReadBuffer();

            if (_readBuffer.StartAddress != startAddr)
                _readBuffer.StartAddress = startAddr;
            if (_readBuffer.NoOfInputs != noOfInput)
                _readBuffer.NoOfInputs = noOfInput; // Allocate buffer size.

            ushort[] buffers = ReadData((ushort)startAddr, (ushort)noOfInput);
            _readBuffer.Update(buffers);
            //_readBuffer.BufferToModbusList();

            gvCurrHoldingRegisters.DataSource = _readBuffer.ModbusValues;
        }

        #endregion

        #region Form Load/Closing

        private void Form1_Load(object sender, EventArgs e)
        {
            // Init buffer
            InitWriteBuffer();
            InitReadBuffer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Free buffer
            FreeReadBuffer();
            FreeWriteBuffer();
            // Disconect and Stop
            Disconnect();
            StopListen();
        }

        #endregion

        #region Button Handlers
        
        private void cmdStart_Click(object sender, EventArgs e)
        {
            StartListen();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            StopListen();
        }

        private void cmdReadAddModbusValue_Click(object sender, EventArgs e)
        {
            gvCurrHoldingRegisters.DataSource = null;

            if (null == _readBuffer)
                InitReadBuffer();

            string name = txtReadName.Text;
            ushort addr;
            if (!ushort.TryParse(txtReadAddr.Text, out addr))
            {
                return;
            }
            bool swapFP = chkReadSwapFP.Checked;

            if (cbReadDataTypes.SelectedIndex == 0)
            {
                _readBuffer.Register(name, addr, (short)0);
            }
            else if (cbReadDataTypes.SelectedIndex == 1)
            {
                _readBuffer.Register(name, addr, (ushort)0);
            }
            else if (cbReadDataTypes.SelectedIndex == 2)
            {
                _readBuffer.Register(name, addr, (float)0, swapFP);
            }
            //buffer.Register("TEMP2", 570, (short)-88);
            //buffer.Register("W1", 573, (float)24.375399, true);
            //buffer.Register("W2", 593, (float)31.1, true);
            gvCurrHoldingRegisters.DataSource = _readBuffer.ModbusValues;
        }

        private void cmdReadDeleteModbusValue_Click(object sender, EventArgs e)
        {
            DataGridView grid = gvCurrHoldingRegisters;
            if (null != grid.SelectedRows &&
                grid.SelectedRows.Count > 0)
            {
                int deleteIndex = grid.SelectedRows[0].Index;
                if (null != _readBuffer)
                {
                    try
                    {
                        grid.DataSource = null;
                        _readBuffer.Unregister(deleteIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        grid.DataSource = _readBuffer.ModbusValues;
                    }
                }
            }
        }

        private void cmdReadClearModbusValue_Click(object sender, EventArgs e)
        {
            DataGridView grid = gvCurrHoldingRegisters;
            if (null != _readBuffer)
            {
                try
                {
                    grid.DataSource = null;
                    _readBuffer.UnregisterAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    grid.DataSource = _readBuffer.ModbusValues;
                }
            }
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            int startAddr;
            int noOfInput;
            if (!int.TryParse(txtStartAddr.Text, out startAddr))
            {
                return;
            }
            if (!int.TryParse(txtNumInputs.Text, out noOfInput))
            {
                return;
            }

            if (startAddr >= 0 && noOfInput > 0)
            {
                ReadData(startAddr, noOfInput);
            }
            else
            {
                MessageBox.Show("Start Address and No of inputs should be positive value");
            }
        }

        private void cmdWriteAddModbusValue_Click(object sender, EventArgs e)
        {
            gvEditHoldingRegisters.DataSource = null;

            if (null == _writeBuffer)
                InitWriteBuffer();

            string name = txtWriteName.Text;
            ushort addr;
            if (!ushort.TryParse(txtWriteAddr.Text, out addr))
            {
                return;
            }
            bool swapFP = chkWriteSwapFP.Checked;

            if (cbWriteDataTypes.SelectedIndex == 0)
            {
                short val;
                if (!short.TryParse(txtWriteValue.Text.Trim(), out val))
                {
                    val = 0;
                }
                _writeBuffer.Register(name, addr, val);
            }
            else if (cbWriteDataTypes.SelectedIndex == 1)
            {
                ushort val;
                if (!ushort.TryParse(txtWriteValue.Text.Trim(), out val))
                {
                    val = 0;
                }
                _writeBuffer.Register(name, addr, val);
            }
            else if (cbWriteDataTypes.SelectedIndex == 2)
            {
                float val;
                if (!float.TryParse(txtWriteValue.Text.Trim(), out val))
                {
                    val = 0;
                }
                _writeBuffer.Register(name, addr, val, swapFP);
            }

            gvEditHoldingRegisters.DataSource = _writeBuffer.ModbusValues;
        }

        private void cmdWriteDeleteModbusValue_Click(object sender, EventArgs e)
        {
            DataGridView grid = gvEditHoldingRegisters;
            if (null != grid.SelectedRows &&
                grid.SelectedRows.Count > 0)
            {
                int deleteIndex = grid.SelectedRows[0].Index;
                if (null != _writeBuffer)
                {
                    try
                    {
                        grid.DataSource = null;
                        _writeBuffer.Unregister(deleteIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        grid.DataSource = _writeBuffer.ModbusValues;
                    }
                }
            }
        }

        private void cmdWriteClearModbusValue_Click(object sender, EventArgs e)
        {
            DataGridView grid = gvEditHoldingRegisters;
            if (null != _writeBuffer)
            {
                try
                {
                    grid.DataSource = null;
                    _writeBuffer.UnregisterAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    grid.DataSource = _writeBuffer.ModbusValues;
                }
            }
        }

        private void cmdWrite_Click(object sender, EventArgs e)
        {
            int startAddr;
            int noOfInput;
            if (!int.TryParse(txtStartAddr.Text, out startAddr))
            {
                return;
            }
            if (!int.TryParse(txtNumInputs.Text, out noOfInput))
            {
                return;
            }

            if (startAddr >= 0 && noOfInput > 0 && null != _writeBuffer)
            {
                if (_writeBuffer.StartAddress != startAddr)
                    _writeBuffer.StartAddress = startAddr;
                if (_writeBuffer.NoOfInputs != noOfInput)
                    _writeBuffer.NoOfInputs = noOfInput;
                //_writeBuffer.ModbusListToBuffer(); // Sync list to buffer.
                WriteData((ushort)startAddr, _writeBuffer.GetBuffers());
            }
            else
            {
                MessageBox.Show("Start Address and No of inputs should be positive value");
            }
        }

        #endregion
    }
}
