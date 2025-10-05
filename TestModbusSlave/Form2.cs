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
    /// Test Slave v2.
    /// </summary>
    public partial class Form2 : Form
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Form2()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private ModbusTcpIpSlaveService _inspWeightSlave = null;
        private ModbusTcpIpSlaveService _coating1Slave = null;
        private ModbusTcpIpSlaveService _coating2Slave = null;

        private ModbusTcpIpSlaveService _coating3Slave = null;

        private ModbusTcpIpSlaveService _scouring1Slave = null;
        private ModbusTcpIpSlaveService _scouring2Slave = null;

        private ModbusTcpIpSlaveService _scouringCounter1Slave = null;
        private ModbusTcpIpSlaveService _scouringCounter2Slave = null;
        private ModbusTcpIpSlaveService _coatingCounter1Slave = null;
        private ModbusTcpIpSlaveService _coatingCounter2Slave = null;
        private ModbusTcpIpSlaveService _scouringDryerCounterSlave = null;

        private ModbusTcpIpSlaveService _scouringCoat2CounterSlave = null;

        private ModbusTcpIpSlaveService _coating3ScouringSlave = null;
        private ModbusTcpIpSlaveService _coating3ScouringCounterSlave = null;

        // เพิ่มใหม่ scouring coat 2 05/01/17
        private ModbusTcpIpSlaveService _scouringCoat2Slave = null;

        // เพิ่มใหม่ 12/05/17
        private ModbusTcpIpSlaveService _coating1ScouringSlave = null;
        private ModbusTcpIpSlaveService _coatingCounter1ScouringSlave = null;

        // เพิ่มใหม่ 26/05/17
        private ModbusTcpIpSlaveService _scouring2ScouringDrySlave = null;
        private ModbusTcpIpSlaveService _scouring2ScouringDryCounterSlave = null;

        private ModbusTcpIpSlaveService _coating3TestSlave = null;
        private ModbusTcpIpSlaveService _scouring2TestSlave = null;

        private ModbusTcpIpSlaveService _scouringCoat1Slave = null;
        private ModbusTcpIpSlaveService _scouringCoat1CounterSlave = null;


        private ModbusTcpIpSlaveService _scouringLabSlave = null;
        private ModbusTcpIpSlaveService _airLabSlave = null;


        #endregion

        #region Private Methods

        private void InitModbusManagers()
        {
            // Inspection Weight
            InspectionWeightModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Start();
            // Coating 1
            Coating1ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Coating1>(Instance_ReadCompleted);
            Coating1ModbusManager.Instance.Start();
            // Coating 2
            Coating2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Coating2>(Instance_ReadCompleted);
            Coating2ModbusManager.Instance.Start();
            // Coating 3
            Coating3ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Coating3>(Instance_ReadCompleted);
            Coating3ModbusManager.Instance.Start();

            // Scouring 1
            Scouring1ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Scouring1>(Instance_ReadCompleted);
            Scouring1ModbusManager.Instance.Start();
            // Scouring 2
            Scouring2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Scouring2>(Instance_ReadCompleted);
            Scouring2ModbusManager.Instance.Start();
            // Scouring Counter 1
            ScouringCounter1ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(ScouringCounter1_ReadCompleted);
            ScouringCounter1ModbusManager.Instance.Start();
            // Scouring Counter 2
            ScouringCounter2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(ScouringCounter2_ReadCompleted);
            ScouringCounter2ModbusManager.Instance.Start();
            // Coating Counter 1
            CoatingCounter1ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(CoatingCounter1_ReadCompleted);
            CoatingCounter1ModbusManager.Instance.Start();
            // Coating Counter 2
            CoatingCounter2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(CoatingCounter2_ReadCompleted);
            CoatingCounter2ModbusManager.Instance.Start();
            // Scouring Dryer Counter
            ScouringDryerCounterModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(ScouringDryerCounter_ReadCompleted);
            ScouringDryerCounterModbusManager.Instance.Start();

            // Coating 3 Scouring
            Coating3ScouringModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Coating3Scouring>(Instance_ReadCompleted);
            Coating3ScouringModbusManager.Instance.Start();

            // Coating 3 Scouring Counter
            Coating3ScouringCounterModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(Coating3ScouringCounter_ReadCompleted);
            Coating3ScouringCounterModbusManager.Instance.Start();

            // ScouringCoat2
            ScouringCoat2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<ScouringCoat2>(Instance_ReadCompleted);
            ScouringCoat2ModbusManager.Instance.Start();

            // Scouring Coat2 Counter
            ScouringCoat2CounterModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(ScouringCoat2Counter_ReadCompleted);
            ScouringCoat2CounterModbusManager.Instance.Start();


            // Coating 1 Scouring
            Coating1ScouringModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Coating1Scouring>(Instance_ReadCompleted);
            Coating1ScouringModbusManager.Instance.Start();

            // Coating Counter 1 Scouring
            CoatingCounter1ScouringModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(CoatingCounter1Scouring_ReadCompleted);
            CoatingCounter1ScouringModbusManager.Instance.Start();

            // Scouring2ScouringDry
            Scouring2ScouringDryModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Scouring2ScouringDry>(Instance_ReadCompleted);
            Scouring2ScouringDryModbusManager.Instance.Start();

            // Scouring2ScouringDry Counter
            Scouring2ScouringDryCounterModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(Scouring2ScouringDryCounter_ReadCompleted);
            Scouring2ScouringDryCounterModbusManager.Instance.Start();

            // Coating 3
            Coating3ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Coating3>(Coating3_ReadCompleted);
            Coating3ModbusManager.Instance.Start();

            // Scouring 2
            Scouring2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<Scouring2>(Scouring2_ReadCompleted);
            Scouring2ModbusManager.Instance.Start();

            // ScouringCoat 1
            ScouringCoat1ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<ScouringCoat1>(Instance_ReadCompleted);
            ScouringCoat1ModbusManager.Instance.Start();

            // Coating Counter 1
            ScouringCoat1CounterModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<FinishingCounter>(ScouringCoat1Counter_ReadCompleted);
            ScouringCoat1CounterModbusManager.Instance.Start();

            // ScouringLab
            ScouringLabModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<ScouringLab>(ScouringLab_ReadCompleted);
            ScouringLabModbusManager.Instance.Start();

            // AirStaticLab
            AirStaticLabModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<AirStaticLab>(AirStaticLab_ReadCompleted);
            AirStaticLabModbusManager.Instance.Start();
        }

        private void ReleaseModbusManagers()
        {
            // Coating Counter 1
            CoatingCounter1ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(CoatingCounter1_ReadCompleted);
            CoatingCounter1ModbusManager.Instance.Shutdown();
            // Coating Counter 2
            CoatingCounter2ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(CoatingCounter2_ReadCompleted);
            CoatingCounter2ModbusManager.Instance.Shutdown();
            // Scouring Dryer Counter
            ScouringDryerCounterModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(ScouringDryerCounter_ReadCompleted);
            ScouringDryerCounterModbusManager.Instance.Shutdown();
           
            // Scouring Counter 1
            ScouringCounter1ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(ScouringCounter1_ReadCompleted);
            ScouringCounter1ModbusManager.Instance.Shutdown();
            // Scouring Counter 2
            ScouringCounter2ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(ScouringCounter2_ReadCompleted);
            ScouringCounter2ModbusManager.Instance.Shutdown();
            // Coating Counter 3
            //CoatingCounter3ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(CoatingCounter3_ReadCompleted);
            //CoatingCounter3ModbusManager.Instance.Shutdown();
           
            // Scouring 1
            Scouring1ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Scouring1>(Instance_ReadCompleted);
            Scouring1ModbusManager.Instance.Shutdown();
            // Scouring 2
            Scouring2ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Scouring2>(Instance_ReadCompleted);
            Scouring2ModbusManager.Instance.Shutdown();
            
            // Coating 1
            Coating1ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Coating1>(Instance_ReadCompleted);
            Coating1ModbusManager.Instance.Shutdown();
            // Coating 2
            Coating2ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Coating2>(Instance_ReadCompleted);
            Coating2ModbusManager.Instance.Shutdown();
            // Coating 3
            Coating3ModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Coating3>(Instance_ReadCompleted);
            Coating3ModbusManager.Instance.Shutdown();
           
            // Coating 3 Scouring
            Coating3ScouringModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Coating3Scouring>(Instance_ReadCompleted);
            Coating3ScouringModbusManager.Instance.Shutdown();

            Coating3ScouringCounterModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(Coating3ScouringCounter_ReadCompleted);
            Coating3ScouringCounterModbusManager.Instance.Shutdown();

            // Inspection Weight
            InspectionWeightModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<InspectionWeight>(Instance_ReadCompleted);
            InspectionWeightModbusManager.Instance.Shutdown();

            // ScouringCoat2
            ScouringCoat2ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<ScouringCoat2>(Instance_ReadCompleted);
            ScouringCoat2ModbusManager.Instance.Shutdown();

            // Scouring Coat2 Counter
            ScouringCoat2CounterModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(ScouringCoat2Counter_ReadCompleted);
            ScouringCoat2CounterModbusManager.Instance.Shutdown();

            // Coating 1 Scouring
            Coating1ScouringModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Coating1Scouring>(Instance_ReadCompleted);
            Coating1ScouringModbusManager.Instance.Shutdown();

            // Coating Counter 1 Scouring
            CoatingCounter1ScouringModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(CoatingCounter1Scouring_ReadCompleted);
            CoatingCounter1ScouringModbusManager.Instance.Shutdown();

            // Scouring2ScouringDry
            Scouring2ScouringDryModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<Scouring2ScouringDry>(Instance_ReadCompleted);
            Scouring2ScouringDryModbusManager.Instance.Shutdown();

            // Scouring2ScouringDry Counter 
            Scouring2ScouringDryCounterModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(Scouring2ScouringDryCounter_ReadCompleted);
            Scouring2ScouringDryCounterModbusManager.Instance.Shutdown();


            // ScouringCoat1
            ScouringCoat1ModbusManager.Instance.ReadCompleted += new ModbusReadEventHandler<ScouringCoat1>(Instance_ReadCompleted);
            ScouringCoat1ModbusManager.Instance.Shutdown();

            // ScouringCoat1
            ScouringCoat1CounterModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<FinishingCounter>(ScouringCoat1Counter_ReadCompleted);
            ScouringCoat1CounterModbusManager.Instance.Shutdown();


            // ScouringLab
            ScouringLabModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<ScouringLab>(ScouringLab_ReadCompleted);
            ScouringLabModbusManager.Instance.Shutdown();

            // AirStaticLab
            AirStaticLabModbusManager.Instance.ReadCompleted -= new ModbusReadEventHandler<AirStaticLab>(AirStaticLab_ReadCompleted);
            AirStaticLabModbusManager.Instance.Shutdown();
        }

        private void InitSlaves()
        {
            // Inspection Weight
            if (null == _inspWeightSlave)
                _inspWeightSlave = new ModbusTcpIpSlaveService();
            // Coating 1
            if (null == _coating1Slave)
                _coating1Slave = new ModbusTcpIpSlaveService();
            // Coating 2
            if (null == _coating2Slave)
                _coating2Slave = new ModbusTcpIpSlaveService();
            // Coating 3
            if (null == _coating3Slave)
                _coating3Slave = new ModbusTcpIpSlaveService();
            // Scouring 1
            if (null == _scouring1Slave)
                _scouring1Slave = new ModbusTcpIpSlaveService();
            // Scouring 2
            if (null == _scouring2Slave)
                _scouring2Slave = new ModbusTcpIpSlaveService();
            // Scouring Counter 1
            if (null == _scouringCounter1Slave)
                _scouringCounter1Slave = new ModbusTcpIpSlaveService();
            // Scouring Counter 2
            if (null == _scouringCounter2Slave)
                _scouringCounter2Slave = new ModbusTcpIpSlaveService();
            // Coating Counter 1
            if (null == _coatingCounter1Slave)
                _coatingCounter1Slave = new ModbusTcpIpSlaveService();
            // Coating Counter 2
            if (null == _coatingCounter2Slave)
                _coatingCounter2Slave = new ModbusTcpIpSlaveService();
            // Scouring Dryer Counter
            if (null == _scouringDryerCounterSlave)
                _scouringDryerCounterSlave = new ModbusTcpIpSlaveService();

            // Coating 3 Scouring
            if (null == _coating3ScouringSlave)
                _coating3ScouringSlave = new ModbusTcpIpSlaveService();

            // Coating 3 Scouring Counter
            if (null == _coating3ScouringCounterSlave)
                _coating3ScouringCounterSlave = new ModbusTcpIpSlaveService();

            // ScouringCoat2
            if (null == _scouringCoat2Slave)
                _scouringCoat2Slave = new ModbusTcpIpSlaveService();

            // Scouring Counter 1
            if (null == _scouringCoat2CounterSlave)
                _scouringCoat2CounterSlave = new ModbusTcpIpSlaveService();

            // Coating 1 Scouring
            if (null == _coating1ScouringSlave)
                _coating1ScouringSlave = new ModbusTcpIpSlaveService();

            // Coating Counter 1 Scouring
            if (null == _coatingCounter1ScouringSlave)
                _coatingCounter1ScouringSlave = new ModbusTcpIpSlaveService();

            // Scouring2ScouringDry
            if (null == _scouring2ScouringDrySlave)
                _scouring2ScouringDrySlave = new ModbusTcpIpSlaveService();

            // Scouring2ScouringDry Counter
            if (null == _scouring2ScouringDryCounterSlave)
                _scouring2ScouringDryCounterSlave = new ModbusTcpIpSlaveService();

            // Test Coating 3
            if (null == _coating3TestSlave)
                _coating3TestSlave = new ModbusTcpIpSlaveService();

            // Test Scouring 2
            if (null == _scouring2TestSlave)
                _scouring2TestSlave = new ModbusTcpIpSlaveService();

            // ScouringCoat1
            if (null == _scouringCoat1Slave)
                _scouringCoat1Slave = new ModbusTcpIpSlaveService();

            // Scouring Counter 1
            if (null == _scouringCoat1CounterSlave)
                _scouringCoat1CounterSlave = new ModbusTcpIpSlaveService();

            // ScouringLabSlave
            if (null == _scouringLabSlave)
                _scouringLabSlave = new ModbusTcpIpSlaveService();

            // ScouringLabSlave
            if (null == _airLabSlave)
                _airLabSlave = new ModbusTcpIpSlaveService();
        }

        private void ReleaseSlaves()
        {
            // Inpsection Weight
            if (null != _inspWeightSlave)
                _inspWeightSlave.Shutdown();
            _inspWeightSlave = null;
            // Coating 1
            if (null != _coating1Slave)
                _coating1Slave.Shutdown();
            _coating1Slave = null;
            // Coating 2
            if (null != _coating2Slave)
                _coating2Slave.Shutdown();
            _coating2Slave = null;
            // Coating 3
            if (null != _coating3Slave)
                _coating3Slave.Shutdown();
            _coating3Slave = null;

            // Scouring 1
            if (null != _scouring1Slave)
                _scouring1Slave.Shutdown();
            _scouring1Slave = null;
            // Scouring 2
            if (null != _scouring2Slave)
                _scouring2Slave.Shutdown();
            _scouring2Slave = null;

            // Scouring Counter 1
            if (null != _scouringCounter1Slave)
                _scouringCounter1Slave.Shutdown();
            _scouringCounter1Slave = null;
            // Scouring Counter 2
            if (null != _scouringCounter2Slave)
                _scouringCounter2Slave.Shutdown();
            _scouringCounter2Slave = null;

            // Coating Counter 1
            if (null != _coatingCounter1Slave)
                _coatingCounter1Slave.Shutdown();
            _coatingCounter1Slave = null;
            // Coating Counter 2
            if (null != _coatingCounter2Slave)
                _coatingCounter2Slave.Shutdown();
            _coatingCounter2Slave = null;

            // Scouring Dryer Counter
            if (null != _scouringDryerCounterSlave)
                _scouringDryerCounterSlave.Shutdown();
            _scouringDryerCounterSlave = null;

            // Coating 3 Scouring
            if (null != _coating3ScouringSlave)
                _coating3ScouringSlave.Shutdown();
            _coating3ScouringSlave = null;

            // Coating 3 Scouring Counter
            if (null != _coating3ScouringCounterSlave)
                _coating3ScouringCounterSlave.Shutdown();
            _coating3ScouringCounterSlave = null;

            // ScouringCoat2
            if (null != _scouringCoat2Slave)
                _scouringCoat2Slave.Shutdown();
            _scouringCoat2Slave = null;

            // Scouring Coat2 Counter 
            if (null != _scouringCoat2CounterSlave)
                _scouringCoat2CounterSlave.Shutdown();
            _scouringCoat2CounterSlave = null;

            // Coating 1 Scouring
            if (null != _coating1ScouringSlave)
                _coating1ScouringSlave.Shutdown();
            _coating1ScouringSlave = null;

            // Coating Counter 1 Scouring
            if (null != _coatingCounter1ScouringSlave)
                _coatingCounter1ScouringSlave.Shutdown();
            _coatingCounter1ScouringSlave = null;

            // Scouring2ScouringDry
            if (null != _scouring2ScouringDrySlave)
                _scouring2ScouringDrySlave.Shutdown();
            _scouring2ScouringDrySlave = null;

            // Scouring2ScouringDry Counter
            if (null != _scouring2ScouringDryCounterSlave)
                _scouring2ScouringDryCounterSlave.Shutdown();
            _scouring2ScouringDryCounterSlave = null;

            // Coating 3 Test
            if (null != _coating3TestSlave)
                _coating3TestSlave.Shutdown();
            _coating3TestSlave = null;

            // Scouring 2 Test
            if (null != _scouring2TestSlave)
                _scouring2TestSlave.Shutdown();
            _scouring2TestSlave = null;

            // ScouringCoat1
            if (null != _scouringCoat1Slave)
                _scouringCoat1Slave.Shutdown();
            _scouringCoat1Slave = null;

            // Scouring Coat1 Counter 
            if (null != _scouringCoat1CounterSlave)
                _scouringCoat1CounterSlave.Shutdown();
            _scouringCoat1CounterSlave = null;

            // Scouring Lab 
            if (null != _scouringLabSlave)
                _scouringLabSlave.Shutdown();
            _scouringLabSlave = null;

            // Scouring Lab 
            if (null != _airLabSlave)
                _airLabSlave.Shutdown();
            _airLabSlave = null;
        }

        private void ValidateSlaveControls()
        {
            ModbusTcpIpConfig cfg = null;

            #region Inspection Weight
            
            // Inspection Weight
            cfg = InspectionWeightModbusManager.Instance.Configs;
            
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtInspWeightIP.Text = cfg.Slave.IP;
                txtInspWeightPortNo.Text = cfg.Slave.Port.ToString();
                txtInspWeightSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdInspWeightStart.Enabled = !_inspWeightSlave.IsRunning;
                cmdInspWeightStop.Enabled = !cmdInspWeightStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMInspWeightIP.Text = cfg.Master.IP;
                    txtMInspWeightPortNo.Text = cfg.Master.Port.ToString();
                    txtMInspWeightSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMInspWeightIP.Text = string.Empty;
                    txtMInspWeightPortNo.Text = string.Empty;
                    txtMInspWeightSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpInspWeightWrite.Enabled = !cmdInspWeightStart.Enabled;
                grpInspWeightRead.Enabled = !cmdInspWeightStart.Enabled;
                // Property Grid
                pgInspWeightWrite.SelectedObject = new InspectionWeight();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtInspWeightIP.Text = string.Empty;
                txtInspWeightPortNo.Text = string.Empty;
                txtInspWeightSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMInspWeightIP.Text = string.Empty;
                txtMInspWeightPortNo.Text = string.Empty;
                txtMInspWeightSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdInspWeightStart.Enabled = false;
                cmdInspWeightStop.Enabled = false;
                // Write-Read GroupBox
                grpInspWeightWrite.Enabled = !cmdInspWeightStart.Enabled;
                grpInspWeightRead.Enabled = !cmdInspWeightStart.Enabled;
                // Property Grid
                pgInspWeightWrite.SelectedObject = null;
            }

            #endregion

            #region Coating 1
            
            // Coating 1
            cfg = Coating1ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating1IP.Text = cfg.Slave.IP;
                txtCoating1PortNo.Text = cfg.Slave.Port.ToString();
                txtCoating1SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating1Start.Enabled = !_coating1Slave.IsRunning;
                cmdCoating1Stop.Enabled = !cmdCoating1Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating1IP.Text = cfg.Master.IP;
                    txtMCoating1PortNo.Text = cfg.Master.Port.ToString();
                    txtMCoating1SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating1IP.Text = string.Empty;
                    txtMCoating1PortNo.Text = string.Empty;
                    txtMCoating1SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoating1Write.Enabled = !cmdCoating1Start.Enabled;
                grpCoating1Read.Enabled = !cmdCoating1Start.Enabled;
                // Property Grid
                pgCoating1Write.SelectedObject = new Coating1();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating1IP.Text = string.Empty;
                txtCoating1PortNo.Text = string.Empty;
                txtCoating1SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating1IP.Text = string.Empty;
                txtMCoating1PortNo.Text = string.Empty;
                txtMCoating1SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating1Start.Enabled = false;
                cmdCoating1Stop.Enabled = false;
                // Write-Read GroupBox
                grpCoating1Write.Enabled = !cmdCoating1Start.Enabled;
                grpCoating1Read.Enabled = !cmdCoating1Start.Enabled;
                // Property Grid
                pgCoating1Write.SelectedObject = null;
            }

            #endregion

            #region Coating 2
            
            // Coating 2
            cfg = Coating2ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating2IP.Text = cfg.Slave.IP;
                txtCoating2PortNo.Text = cfg.Slave.Port.ToString();
                txtCoating2SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating2Start.Enabled = !_coating2Slave.IsRunning;
                cmdCoating2Stop.Enabled = !cmdCoating2Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating2IP.Text = cfg.Master.IP;
                    txtMCoating2PortNo.Text = cfg.Master.Port.ToString();
                    txtMCoating2SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating2IP.Text = string.Empty;
                    txtMCoating2PortNo.Text = string.Empty;
                    txtMCoating2SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoating2Write.Enabled = !cmdCoating2Start.Enabled;
                grpCoating2Read.Enabled = !cmdCoating2Start.Enabled;
                // Property Grid
                pgCoating2Write.SelectedObject = new Coating2();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating2IP.Text = string.Empty;
                txtCoating2PortNo.Text = string.Empty;
                txtCoating2SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating2IP.Text = string.Empty;
                txtMCoating2PortNo.Text = string.Empty;
                txtMCoating2SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating2Start.Enabled = false;
                cmdCoating2Stop.Enabled = false;
                // Write-Read GroupBox
                grpCoating2Write.Enabled = !cmdCoating2Start.Enabled;
                grpCoating2Read.Enabled = !cmdCoating2Start.Enabled;
                // Property Grid
                pgCoating2Write.SelectedObject = null;
            }

            #endregion

            #region Coating 3

            // Coating 3
            cfg = Coating3ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3IP.Text = cfg.Slave.IP;
                txtCoating3PortNo.Text = cfg.Slave.Port.ToString();
                txtCoating3SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating3Start.Enabled = !_coating3Slave.IsRunning;
                cmdCoating3Stop.Enabled = !cmdCoating3Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating3IP.Text = cfg.Master.IP;
                    txtMCoating3PortNo.Text = cfg.Master.Port.ToString();
                    txtMCoating3SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating3IP.Text = string.Empty;
                    txtMCoating3PortNo.Text = string.Empty;
                    txtMCoating3SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoating3Write.Enabled = !cmdCoating3Start.Enabled;
                grpCoating3Read.Enabled = !cmdCoating3Start.Enabled;
                // Property Grid
                pgCoating3Write.SelectedObject = new Coating3();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3IP.Text = string.Empty;
                txtCoating3PortNo.Text = string.Empty;
                txtCoating3SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating3IP.Text = string.Empty;
                txtMCoating3PortNo.Text = string.Empty;
                txtMCoating3SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating3Start.Enabled = false;
                cmdCoating3Stop.Enabled = false;
                // Write-Read GroupBox
                grpCoating3Write.Enabled = !cmdCoating3Start.Enabled;
                grpCoating3Read.Enabled = !cmdCoating3Start.Enabled;
                // Property Grid
                pgCoating3Write.SelectedObject = null;
            }

            #endregion

            #region Scouring 1
            
            // Scouring 1
            cfg = Scouring1ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouring1IP.Text = cfg.Slave.IP;
                txtScouring1PortNo.Text = cfg.Slave.Port.ToString();
                txtScouring1SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouring1Start.Enabled = !_scouring1Slave.IsRunning;
                cmdScouring1Stop.Enabled = !cmdScouring1Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouring1IP.Text = cfg.Master.IP;
                    txtMScouring1PortNo.Text = cfg.Master.Port.ToString();
                    txtMScouring1SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouring1IP.Text = string.Empty;
                    txtMScouring1PortNo.Text = string.Empty;
                    txtMScouring1SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouring1Write.Enabled = !cmdScouring1Start.Enabled;
                grpScouring1Read.Enabled = !cmdScouring1Start.Enabled;
                // Property Grid
                pgScouring1Write.SelectedObject = new Scouring1();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouring1IP.Text = string.Empty;
                txtScouring1PortNo.Text = string.Empty;
                txtScouring1SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouring1IP.Text = string.Empty;
                txtMScouring1PortNo.Text = string.Empty;
                txtMScouring1SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouring1Start.Enabled = false;
                cmdScouring1Stop.Enabled = false;
                // Write-Read GroupBox
                grpScouring1Write.Enabled = !cmdScouring1Start.Enabled;
                grpScouring1Read.Enabled = !cmdScouring1Start.Enabled;
                // Property Grid
                pgScouring1Write.SelectedObject = null;
            }

            #endregion

            #region Scouring 2

            // Scouring 2
            cfg = Scouring2ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2IP.Text = cfg.Slave.IP;
                txtScouring2PortNo.Text = cfg.Slave.Port.ToString();
                txtScouring2SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouring2Start.Enabled = !_scouring2Slave.IsRunning;
                cmdScouring2Stop.Enabled = !cmdScouring2Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouring2IP.Text = cfg.Master.IP;
                    txtMScouring2PortNo.Text = cfg.Master.Port.ToString();
                    txtMScouring2SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouring2IP.Text = string.Empty;
                    txtMScouring2PortNo.Text = string.Empty;
                    txtMScouring2SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouring2Write.Enabled = !cmdScouring2Start.Enabled;
                grpScouring2Read.Enabled = !cmdScouring2Start.Enabled;
                // Property Grid
                pgScouring2Write.SelectedObject = new Scouring2();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2IP.Text = string.Empty;
                txtScouring2PortNo.Text = string.Empty;
                txtScouring2SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouring2IP.Text = string.Empty;
                txtMScouring2PortNo.Text = string.Empty;
                txtMScouring2SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouring2Start.Enabled = false;
                cmdScouring2Stop.Enabled = false;
                // Write-Read GroupBox
                grpScouring2Write.Enabled = !cmdScouring2Start.Enabled;
                grpScouring2Read.Enabled = !cmdScouring2Start.Enabled;
                // Property Grid
                pgScouring2Write.SelectedObject = null;
            }

            #endregion

            #region Scouring Counter 1

            // Scouring Counter 1
            cfg = ScouringCounter1ModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCounter1IP.Text = cfg.Slave.IP;
                txtScouringCounter1PortNo.Text = cfg.Slave.Port.ToString();
                txtScouringCounter1SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringCounter1Start.Enabled = !_scouringCounter1Slave.IsRunning;
                cmdScouringCounter1Stop.Enabled = !cmdScouringCounter1Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringCounter1IP.Text = cfg.Master.IP;
                    txtMScouringCounter1PortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringCounter1SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringCounter1IP.Text = string.Empty;
                    txtMScouringCounter1PortNo.Text = string.Empty;
                    txtMScouringCounter1SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringCounter1Write.Enabled = !cmdScouringCounter1Start.Enabled;
                grpScouringCounter1Read.Enabled = !cmdScouringCounter1Start.Enabled;
                // Property Grid
                pgScouringCounter1Write.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCounter1IP.Text = string.Empty;
                txtScouringCounter1PortNo.Text = string.Empty;
                txtScouringCounter1SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringCounter1IP.Text = string.Empty;
                txtMScouringCounter1PortNo.Text = string.Empty;
                txtMScouringCounter1SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringCounter1Start.Enabled = false;
                cmdScouringCounter1Stop.Enabled = false;
                // Write-Read GroupBox
                grpScouringCounter1Write.Enabled = !cmdScouringCounter1Start.Enabled;
                grpScouringCounter1Read.Enabled = !cmdScouringCounter1Start.Enabled;
                // Property Grid
                pgScouringCounter1Write.SelectedObject = null;
            }

            #endregion

            #region Scouring Counter 2

            // Scouring Counter 2
            cfg = ScouringCounter2ModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCounter2IP.Text = cfg.Slave.IP;
                txtScouringCounter2PortNo.Text = cfg.Slave.Port.ToString();
                txtScouringCounter2SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringCounter2Start.Enabled = !_scouringCounter2Slave.IsRunning;
                cmdScouringCounter2Stop.Enabled = !cmdScouringCounter2Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringCounter2IP.Text = cfg.Master.IP;
                    txtMScouringCounter2PortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringCounter2SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringCounter2IP.Text = string.Empty;
                    txtMScouringCounter2PortNo.Text = string.Empty;
                    txtMScouringCounter2SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringCounter2Write.Enabled = !cmdScouringCounter2Start.Enabled;
                grpScouringCounter2Read.Enabled = !cmdScouringCounter2Start.Enabled;
                // Property Grid
                pgScouringCounter2Write.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCounter2IP.Text = string.Empty;
                txtScouringCounter2PortNo.Text = string.Empty;
                txtScouringCounter2SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringCounter2IP.Text = string.Empty;
                txtMScouringCounter2PortNo.Text = string.Empty;
                txtMScouringCounter2SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringCounter2Start.Enabled = false;
                cmdScouringCounter2Stop.Enabled = false;
                // Write-Read GroupBox
                grpScouringCounter2Write.Enabled = !cmdScouringCounter2Start.Enabled;
                grpScouringCounter2Read.Enabled = !cmdScouringCounter2Start.Enabled;
                // Property Grid
                pgScouringCounter2Write.SelectedObject = null;
            }

            #endregion

            #region Coating Counter 1

            // Coating Counter 1
            cfg = CoatingCounter1ModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoatingCounter1IP.Text = cfg.Slave.IP;
                txtCoatingCounter1PortNo.Text = cfg.Slave.Port.ToString();
                txtCoatingCounter1SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoatingCounter1Start.Enabled = !_coatingCounter1Slave.IsRunning;
                cmdCoatingCounter1Stop.Enabled = !cmdCoatingCounter1Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoatingCounter1IP.Text = cfg.Master.IP;
                    txtMCoatingCounter1PortNo.Text = cfg.Master.Port.ToString();
                    txtMCoatingCounter1SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoatingCounter1IP.Text = string.Empty;
                    txtMCoatingCounter1PortNo.Text = string.Empty;
                    txtMCoatingCounter1SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoatingCounter1Write.Enabled = !cmdCoatingCounter1Start.Enabled;
                grpCoatingCounter1Read.Enabled = !cmdCoatingCounter1Start.Enabled;
                // Property Grid
                pgCoatingCounter1Write.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoatingCounter1IP.Text = string.Empty;
                txtCoatingCounter1PortNo.Text = string.Empty;
                txtCoatingCounter1SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoatingCounter1IP.Text = string.Empty;
                txtMCoatingCounter1PortNo.Text = string.Empty;
                txtMCoatingCounter1SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoatingCounter1Start.Enabled = false;
                cmdCoatingCounter1Stop.Enabled = false;
                // Write-Read GroupBox
                grpCoatingCounter1Write.Enabled = !cmdCoatingCounter1Start.Enabled;
                grpCoatingCounter1Read.Enabled = !cmdCoatingCounter1Start.Enabled;
                // Property Grid
                pgCoatingCounter1Write.SelectedObject = null;
            }

            #endregion

            #region Coating Counter 2

            // Coating Counter 2
            cfg = CoatingCounter2ModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoatingCounter2IP.Text = cfg.Slave.IP;
                txtCoatingCounter2PortNo.Text = cfg.Slave.Port.ToString();
                txtCoatingCounter2SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoatingCounter2Start.Enabled = !_coatingCounter2Slave.IsRunning;
                cmdCoatingCounter2Stop.Enabled = !cmdCoatingCounter2Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoatingCounter2IP.Text = cfg.Master.IP;
                    txtMCoatingCounter2PortNo.Text = cfg.Master.Port.ToString();
                    txtMCoatingCounter2SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoatingCounter2IP.Text = string.Empty;
                    txtMCoatingCounter2PortNo.Text = string.Empty;
                    txtMCoatingCounter2SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoatingCounter2Write.Enabled = !cmdCoatingCounter2Start.Enabled;
                grpCoatingCounter2Read.Enabled = !cmdCoatingCounter2Start.Enabled;
                // Property Grid
                pgCoatingCounter2Write.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoatingCounter2IP.Text = string.Empty;
                txtCoatingCounter2PortNo.Text = string.Empty;
                txtCoatingCounter2SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoatingCounter2IP.Text = string.Empty;
                txtMCoatingCounter2PortNo.Text = string.Empty;
                txtMCoatingCounter2SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoatingCounter2Start.Enabled = false;
                cmdCoatingCounter2Stop.Enabled = false;
                // Write-Read GroupBox
                grpCoatingCounter2Write.Enabled = !cmdCoatingCounter2Start.Enabled;
                grpCoatingCounter2Read.Enabled = !cmdCoatingCounter2Start.Enabled;
                // Property Grid
                pgCoatingCounter2Write.SelectedObject = null;
            }

            #endregion

            #region Scouring Dryer

            // Scouring Dryer Counter
            cfg = ScouringDryerCounterModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringDryerCounterIP.Text = cfg.Slave.IP;
                txtScouringDryerCounterPortNo.Text = cfg.Slave.Port.ToString();
                txtScouringDryerCounterSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringDryerCounterStart.Enabled = !_scouringDryerCounterSlave.IsRunning;
                cmdScouringDryerCounterStop.Enabled = !cmdScouringDryerCounterStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringDryerCounterIP.Text = cfg.Master.IP;
                    txtMScouringDryerCounterPortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringDryerCounterSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringDryerCounterIP.Text = string.Empty;
                    txtMScouringDryerCounterPortNo.Text = string.Empty;
                    txtMScouringDryerCounterSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringDryerCounterWrite.Enabled = !cmdScouringDryerCounterStart.Enabled;
                grpScouringDryerCounterRead.Enabled = !cmdScouringDryerCounterStart.Enabled;
                // Property Grid
                pgScouringDryerCounterWrite.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringDryerCounterIP.Text = string.Empty;
                txtScouringDryerCounterPortNo.Text = string.Empty;
                txtScouringDryerCounterSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringDryerCounterIP.Text = string.Empty;
                txtMScouringDryerCounterPortNo.Text = string.Empty;
                txtMScouringDryerCounterSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringDryerCounterStart.Enabled = false;
                cmdScouringDryerCounterStop.Enabled = false;
                // Write-Read GroupBox
                grpScouringDryerCounterWrite.Enabled = !cmdScouringDryerCounterStart.Enabled;
                grpScouringDryerCounterRead.Enabled = !cmdScouringDryerCounterStart.Enabled;
                // Property Grid
                pgScouringDryerCounterWrite.SelectedObject = null;
            }

            #endregion

            #region Coating 3 Scouring

            // Coating 3
            cfg = Coating3ScouringModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3ScouringIP.Text = cfg.Slave.IP;
                txtCoating3ScouringPortNo.Text = cfg.Slave.Port.ToString();
                txtCoating3ScouringSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating3ScouringStart.Enabled = !_coating3ScouringSlave.IsRunning;
                cmdCoating3ScouringStop.Enabled = !cmdCoating3ScouringStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating3ScouringIP.Text = cfg.Master.IP;
                    txtMCoating3ScouringPortNo.Text = cfg.Master.Port.ToString();
                    txtMCoating3ScouringSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating3ScouringIP.Text = string.Empty;
                    txtMCoating3ScouringPortNo.Text = string.Empty;
                    txtMCoating3ScouringSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoating3ScouringWrite.Enabled = !cmdCoating3ScouringStart.Enabled;
                grpCoating3ScouringRead.Enabled = !cmdCoating3ScouringStart.Enabled;
                // Property Grid
                pgCoating3ScouringWrite.SelectedObject = new Coating3Scouring();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3ScouringIP.Text = string.Empty;
                txtCoating3ScouringPortNo.Text = string.Empty;
                txtCoating3ScouringSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating3ScouringIP.Text = string.Empty;
                txtMCoating3ScouringPortNo.Text = string.Empty;
                txtMCoating3ScouringSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating3ScouringStart.Enabled = false;
                cmdCoating3ScouringStop.Enabled = false;
                // Write-Read GroupBox
                grpCoating3ScouringWrite.Enabled = !cmdCoating3ScouringStart.Enabled;
                grpCoating3ScouringRead.Enabled = !cmdCoating3ScouringStart.Enabled;
                // Property Grid
                pgCoating3ScouringWrite.SelectedObject = null;
            }

            #endregion

            #region Coating 3 Scouring Counter 

            // Coating 3 Scouring Counter
            cfg = Coating3ScouringCounterModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3ScouringCounterIP.Text = cfg.Slave.IP;
                txtCoating3ScouringCounterPortNo.Text = cfg.Slave.Port.ToString();
                txtCoating3ScouringCounterSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating3ScouringCounterStart.Enabled = !_coating3ScouringCounterSlave.IsRunning;
                cmdCoating3ScouringCounterStop.Enabled = !cmdCoating3ScouringCounterStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating3ScouringCounterIP.Text = cfg.Master.IP;
                    txtMCoating3ScouringCounterPortNo.Text = cfg.Master.Port.ToString();
                    txtMCoating3ScouringCounterSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating3ScouringCounterIP.Text = string.Empty;
                    txtMCoating3ScouringCounterPortNo.Text = string.Empty;
                    txtMCoating3ScouringCounterSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoating3ScouringCounterWrite.Enabled = !cmdCoating3ScouringCounterStart.Enabled;
                grpCoating3ScouringCounterRead.Enabled = !cmdCoating3ScouringCounterStart.Enabled;
                // Property Grid
                pgCoating3ScouringCounterWrite.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3ScouringCounterIP.Text = string.Empty;
                txtCoating3ScouringCounterPortNo.Text = string.Empty;
                txtCoating3ScouringCounterSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating3ScouringCounterIP.Text = string.Empty;
                txtMCoating3ScouringCounterPortNo.Text = string.Empty;
                txtMCoating3ScouringCounterSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating3ScouringCounterStart.Enabled = false;
                cmdCoating3ScouringCounterStop.Enabled = false;
                // Write-Read GroupBox
                grpCoating3ScouringCounterWrite.Enabled = !cmdCoating3ScouringCounterStart.Enabled;
                grpCoating3ScouringCounterRead.Enabled = !cmdCoating3ScouringCounterStart.Enabled;
                // Property Grid
                pgCoating3ScouringCounterWrite.SelectedObject = null;
            }

            #endregion

            #region Scouring Coat 2

            // Scouring Coat 2
            cfg = ScouringCoat2ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat2IP.Text = cfg.Slave.IP;
                txtScouringCoat2PortNo.Text = cfg.Slave.Port.ToString();
                txtScouringCoat2SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringCoat2Start.Enabled = !_scouringCoat2Slave.IsRunning;
                cmdScouringCoat2Stop.Enabled = !cmdScouringCoat2Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringCoat2IP.Text = cfg.Master.IP;
                    txtMScouringCoat2PortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringCoat2SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringCoat2IP.Text = string.Empty;
                    txtMScouringCoat2PortNo.Text = string.Empty;
                    txtMScouringCoat2SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringCoat2Write.Enabled = !cmdScouringCoat2Start.Enabled;
                grpScouringCoat2Read.Enabled = !cmdScouringCoat2Start.Enabled;
                // Property Grid
                pgScouringCoat2Write.SelectedObject = new ScouringCoat2();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat2IP.Text = string.Empty;
                txtScouringCoat2PortNo.Text = string.Empty;
                txtScouringCoat2SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringCoat2IP.Text = string.Empty;
                txtMScouringCoat2PortNo.Text = string.Empty;
                txtMScouringCoat2SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringCoat2Start.Enabled = false;
                cmdScouringCoat2Stop.Enabled = false;
                // Write-Read GroupBox
                grpScouringCoat2Write.Enabled = !cmdScouringCoat2Start.Enabled;
                grpScouringCoat2Read.Enabled = !cmdScouringCoat2Start.Enabled;
                // Property Grid
                pgScouringCoat2Write.SelectedObject = null;
            }

            #endregion

            #region Scouring Coat2 Counter

            // Scouring Coat2 Counter
            cfg = ScouringCoat2CounterModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat2CounterIP.Text = cfg.Slave.IP;
                txtScouringCoat2CounterPortNo.Text = cfg.Slave.Port.ToString();
                txtScouringCoat2CounterSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringCoat2CounterStart.Enabled = !_scouringCoat2CounterSlave.IsRunning;
                cmdScouringCoat2CounterStop.Enabled = !cmdScouringCoat2CounterStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringCoat2CounterIP.Text = cfg.Master.IP;
                    txtMScouringCoat2CounterPortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringCoat2CounterSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringCoat2CounterIP.Text = string.Empty;
                    txtMScouringCoat2CounterPortNo.Text = string.Empty;
                    txtMScouringCoat2CounterSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringCoat2CounterWrite.Enabled = !cmdScouringCoat2CounterStart.Enabled;
                grpScouringCoat2CounterRead.Enabled = !cmdScouringCoat2CounterStart.Enabled;
                // Property Grid
                pgScouringCoat2CounterWrite.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat2CounterIP.Text = string.Empty;
                txtScouringCoat2CounterPortNo.Text = string.Empty;
                txtScouringCoat2CounterSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringCoat2CounterIP.Text = string.Empty;
                txtMScouringCoat2CounterPortNo.Text = string.Empty;
                txtMScouringCoat2CounterSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringCoat2CounterStart.Enabled = false;
                cmdScouringCoat2CounterStop.Enabled = false;
                // Write-Read GroupBox
                grpScouringCoat2CounterWrite.Enabled = !cmdScouringCoat2CounterStart.Enabled;
                grpScouringCoat2CounterRead.Enabled = !cmdScouringCoat2CounterStart.Enabled;
                // Property Grid
                pgScouringCoat2CounterWrite.SelectedObject = null;
            }

            #endregion

            #region Coating 1 Scouring

            // Coating 1 Scouring
            cfg = Coating1ScouringModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating1ScouringIP.Text = cfg.Slave.IP;
                txtCoating1ScouringPortNo.Text = cfg.Slave.Port.ToString();
                txtCoating1ScouringSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating1ScouringStart.Enabled = !_coating1ScouringSlave.IsRunning;
                cmdCoating1ScouringStop.Enabled = !cmdCoating1ScouringStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating1ScouringIP.Text = cfg.Master.IP;
                    txtMCoating1ScouringPortNo.Text = cfg.Master.Port.ToString();
                    txtMCoating1ScouringSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating1ScouringIP.Text = string.Empty;
                    txtMCoating1ScouringPortNo.Text = string.Empty;
                    txtMCoating1ScouringSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoating1ScouringWrite.Enabled = !cmdCoating1ScouringStart.Enabled;
                grpCoating1ScouringRead.Enabled = !cmdCoating1ScouringStart.Enabled;
                // Property Grid
                pgCoating1ScouringWrite.SelectedObject = new Coating1Scouring();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating1ScouringIP.Text = string.Empty;
                txtCoating1ScouringPortNo.Text = string.Empty;
                txtCoating1ScouringSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating1ScouringIP.Text = string.Empty;
                txtMCoating1ScouringPortNo.Text = string.Empty;
                txtMCoating1ScouringSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating1ScouringStart.Enabled = false;
                cmdCoating1ScouringStop.Enabled = false;
                // Write-Read GroupBox
                grpCoating1ScouringWrite.Enabled = !cmdCoating1ScouringStart.Enabled;
                grpCoating1ScouringRead.Enabled = !cmdCoating1ScouringStart.Enabled;
                // Property Grid
                pgCoating1ScouringWrite.SelectedObject = null;
            }

            #endregion

            #region Coating Counter 1 Scouring

            // Coating Counter 1 Scouring
            cfg = CoatingCounter1ScouringModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoatingCounter1ScouringIP.Text = cfg.Slave.IP;
                txtCoatingCounter1ScouringPortNo.Text = cfg.Slave.Port.ToString();
                txtCoatingCounter1ScouringSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoatingCounter1ScouringStart.Enabled = !_coatingCounter1ScouringSlave.IsRunning;
                cmdCoatingCounter1ScouringStop.Enabled = !cmdCoatingCounter1ScouringStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoatingCounter1ScouringIP.Text = cfg.Master.IP;
                    txtMCoatingCounter1ScouringPortNo.Text = cfg.Master.Port.ToString();
                    txtMCoatingCounter1ScouringSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoatingCounter1ScouringIP.Text = string.Empty;
                    txtMCoatingCounter1ScouringPortNo.Text = string.Empty;
                    txtMCoatingCounter1ScouringSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpCoatingCounter1ScouringWrite.Enabled = !cmdCoatingCounter1ScouringStart.Enabled;
                grpCoatingCounter1ScouringRead.Enabled = !cmdCoatingCounter1ScouringStart.Enabled;
                // Property Grid
                pgCoatingCounter1ScouringWrite.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoatingCounter1ScouringIP.Text = string.Empty;
                txtCoatingCounter1ScouringPortNo.Text = string.Empty;
                txtCoatingCounter1ScouringSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoatingCounter1ScouringIP.Text = string.Empty;
                txtMCoatingCounter1ScouringPortNo.Text = string.Empty;
                txtMCoatingCounter1ScouringSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdCoatingCounter1ScouringStart.Enabled = false;
                cmdCoatingCounter1ScouringStop.Enabled = false;
                // Write-Read GroupBox
                grpCoatingCounter1ScouringWrite.Enabled = !cmdCoatingCounter1ScouringStart.Enabled;
                grpCoatingCounter1ScouringRead.Enabled = !cmdCoatingCounter1ScouringStart.Enabled;
                // Property Grid
                pgCoatingCounter1ScouringWrite.SelectedObject = null;
            }

            #endregion

            #region Scouring2ScouringDry

            // Scouring2ScouringDry 
            cfg = Scouring2ScouringDryModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2ScouringDryIP.Text = cfg.Slave.IP;
                txtScouring2ScouringDryPortNo.Text = cfg.Slave.Port.ToString();
                txtScouring2ScouringDrySlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouring2ScouringDryStart.Enabled = !_scouring2ScouringDrySlave.IsRunning;
                cmdScouring2ScouringDryStop.Enabled = !cmdScouring2ScouringDryStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouring2ScouringDryIP.Text = cfg.Master.IP;
                    txtMScouring2ScouringDryPortNo.Text = cfg.Master.Port.ToString();
                    txtMScouring2ScouringDrySlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouring2ScouringDryIP.Text = string.Empty;
                    txtMScouring2ScouringDryPortNo.Text = string.Empty;
                    txtMScouring2ScouringDrySlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouring2ScouringDryWrite.Enabled = !cmdScouring2ScouringDryStart.Enabled;
                grpScouring2ScouringDryRead.Enabled = !cmdScouring2ScouringDryStart.Enabled;
                // Property Grid
                pgScouring2ScouringDryWrite.SelectedObject = new Scouring2ScouringDry();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2ScouringDryIP.Text = string.Empty;
                txtScouring2ScouringDryPortNo.Text = string.Empty;
                txtScouring2ScouringDrySlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouring2ScouringDryIP.Text = string.Empty;
                txtMScouring2ScouringDryPortNo.Text = string.Empty;
                txtMScouring2ScouringDrySlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouring2ScouringDryStart.Enabled = false;
                cmdScouring2ScouringDryStop.Enabled = false;
                // Write-Read GroupBox
                grpScouring2ScouringDryWrite.Enabled = !cmdScouring2ScouringDryStart.Enabled;
                grpScouring2ScouringDryRead.Enabled = !cmdScouring2ScouringDryStart.Enabled;
                // Property Grid
                pgScouring2ScouringDryWrite.SelectedObject = null;
            }

            #endregion

            #region Scouring2ScouringDry Counter

            // Scouring2ScouringDry Counter 
            cfg = Scouring2ScouringDryCounterModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2ScouringDryCounterIP.Text = cfg.Slave.IP;
                txtScouring2ScouringDryCounterPortNo.Text = cfg.Slave.Port.ToString();
                txtScouring2ScouringDryCounterSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouring2ScouringDryCounterStart.Enabled = !_scouring2ScouringDryCounterSlave.IsRunning;
                cmdScouring2ScouringDryCounterStop.Enabled = !cmdScouring2ScouringDryCounterStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouring2ScouringDryCounterIP.Text = cfg.Master.IP;
                    txtMScouring2ScouringDryCounterPortNo.Text = cfg.Master.Port.ToString();
                    txtMScouring2ScouringDryCounterSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouring2ScouringDryCounterIP.Text = string.Empty;
                    txtMScouring2ScouringDryCounterPortNo.Text = string.Empty;
                    txtMScouring2ScouringDryCounterSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouring2ScouringDryCounterWrite.Enabled = !cmdScouring2ScouringDryCounterStart.Enabled;
                grpScouring2ScouringDryCounterRead.Enabled = !cmdScouring2ScouringDryCounterStart.Enabled;
                // Property Grid
                pgScouring2ScouringDryCounterWrite.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2ScouringDryCounterIP.Text = string.Empty;
                txtScouring2ScouringDryCounterPortNo.Text = string.Empty;
                txtScouring2ScouringDryCounterSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouring2ScouringDryCounterIP.Text = string.Empty;
                txtMScouring2ScouringDryCounterPortNo.Text = string.Empty;
                txtMScouring2ScouringDryCounterSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouring2ScouringDryCounterStart.Enabled = false;
                cmdScouring2ScouringDryCounterStop.Enabled = false;
                // Write-Read GroupBox
                grpScouring2ScouringDryCounterWrite.Enabled = !cmdScouring2ScouringDryCounterStart.Enabled;
                grpScouring2ScouringDryCounterRead.Enabled = !cmdScouring2ScouringDryCounterStart.Enabled;
                // Property Grid
                pgScouring2ScouringDryCounterWrite.SelectedObject = null;
            }

            #endregion

            #region Coating 3 Test

            // Coating 3
            cfg = Coating3ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3IPTest.Text = cfg.Slave.IP;
                txtCoating3PortNoTest.Text = cfg.Slave.Port.ToString();
                txtCoating3SlaveIdTest.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdCoating3StartTest.Enabled = !_coating3TestSlave.IsRunning;
                cmdCoating3StopTest.Enabled = !cmdCoating3StartTest.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMCoating3IPTest.Text = cfg.Master.IP;
                    txtMCoating3PortNoTest.Text = cfg.Master.Port.ToString();
                    txtMCoating3SlaveIdTest.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMCoating3IPTest.Text = string.Empty;
                    txtMCoating3PortNoTest.Text = string.Empty;
                    txtMCoating3SlaveIdTest.Text = string.Empty;
                }
                // Write-Read GroupBox
                gpCoating3Write.Enabled = !cmdCoating3StartTest.Enabled;
                gpCoating3Read.Enabled = !cmdCoating3StartTest.Enabled;
                // Property Grid
                pgCoating3WriteTest.SelectedObject = new Coating3();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtCoating3IPTest.Text = string.Empty;
                txtCoating3PortNoTest.Text = string.Empty;
                txtCoating3SlaveIdTest.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMCoating3IPTest.Text = string.Empty;
                txtMCoating3PortNoTest.Text = string.Empty;
                txtMCoating3SlaveIdTest.Text = string.Empty;
                // Start and Stop buttons
                cmdCoating3StartTest.Enabled = false;
                cmdCoating3StopTest.Enabled = false;
                // Write-Read GroupBox
                gpCoating3Write.Enabled = !cmdCoating3Start.Enabled;
                gpCoating3Read.Enabled = !cmdCoating3Start.Enabled;
                // Property Grid
                pgCoating3WriteTest.SelectedObject = null;
            }

            #endregion

            #region Scouring 2 Test

            // Scouring 2
            cfg = Scouring2ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2IPTest.Text = cfg.Slave.IP;
                txtScouring2PortNoTest.Text = cfg.Slave.Port.ToString();
                txtScouring2SlaveIdTest.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouring2StartTest.Enabled = !_scouring2TestSlave.IsRunning;
                cmdScouring2StopTest.Enabled = !cmdScouring2StartTest.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouring2IPTest.Text = cfg.Master.IP;
                    txtMScouring2PortNoTest.Text = cfg.Master.Port.ToString();
                    txtMScouring2SlaveIdTest.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouring2IPTest.Text = string.Empty;
                    txtMScouring2PortNoTest.Text = string.Empty;
                    txtMScouring2SlaveIdTest.Text = string.Empty;
                }
                // Write-Read GroupBox
                gpScouring2Write.Enabled = !cmdScouring2StartTest.Enabled;
                gpScouring2Read.Enabled = !cmdScouring2StartTest.Enabled;
                // Property Grid
                pgScouring2WriteTest.SelectedObject = new Scouring2();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouring2IPTest.Text = string.Empty;
                txtScouring2PortNoTest.Text = string.Empty;
                txtScouring2SlaveIdTest.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouring2IPTest.Text = string.Empty;
                txtMScouring2PortNoTest.Text = string.Empty;
                txtMScouring2SlaveIdTest.Text = string.Empty;
                // Start and Stop buttons
                cmdScouring2StartTest.Enabled = false;
                cmdScouring2StopTest.Enabled = false;
                // Write-Read GroupBox
                gpScouring2Write.Enabled = !cmdScouring2StartTest.Enabled;
                gpScouring2Read.Enabled = !cmdScouring2StartTest.Enabled;
                // Property Grid
                pgScouring2WriteTest.SelectedObject = null;
            }

            #endregion

            #region ScouringCoat 1

            // ScouringCoat 1
            cfg = ScouringCoat1ModbusManager.Instance.Configs;
            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat1IP.Text = cfg.Slave.IP;
                txtScouringCoat1PortNo.Text = cfg.Slave.Port.ToString();
                txtScouringCoat1SlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringCoat1Start.Enabled = !_scouringCoat1Slave.IsRunning;
                cmdScouringCoat1Stop.Enabled = !cmdScouringCoat1Start.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringCoat1IP.Text = cfg.Master.IP;
                    txtMScouringCoat1PortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringCoat1SlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringCoat1IP.Text = string.Empty;
                    txtMScouringCoat1PortNo.Text = string.Empty;
                    txtMScouringCoat1SlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringCoat1Write.Enabled = !cmdScouringCoat1Start.Enabled;
                grpScouringCoat1Read.Enabled = !cmdScouringCoat1Start.Enabled;
                // Property Grid
                pgScouringCoat1Write.SelectedObject = new ScouringCoat1();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat1IP.Text = string.Empty;
                txtScouringCoat1PortNo.Text = string.Empty;
                txtScouringCoat1SlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringCoat1IP.Text = string.Empty;
                txtMScouringCoat1PortNo.Text = string.Empty;
                txtMScouringCoat1SlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringCoat1Start.Enabled = false;
                cmdScouringCoat1Stop.Enabled = false;
                // Write-Read GroupBox
                grpScouringCoat1Write.Enabled = !cmdScouringCoat1Start.Enabled;
                grpScouringCoat1Read.Enabled = !cmdScouringCoat1Start.Enabled;
                // Property Grid
                pgScouringCoat1Write.SelectedObject = null;
            }

            #endregion

            #region ScouringCoat1 Counter

            // ScouringCoat1 Counter
            cfg = ScouringCoat1CounterModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat1CounterIP.Text = cfg.Slave.IP;
                txtScouringCoat1CounterPortNo.Text = cfg.Slave.Port.ToString();
                txtScouringCoat1CounterSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringCoat1CounterStart.Enabled = !_scouringCoat1CounterSlave.IsRunning;
                cmdScouringCoat1CounterStop.Enabled = !cmdScouringCoat1CounterStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringCoat1CounterIP.Text = cfg.Master.IP;
                    txtMScouringCoat1CounterPortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringCoat1CounterSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringCoat1CounterIP.Text = string.Empty;
                    txtMScouringCoat1CounterPortNo.Text = string.Empty;
                    txtMScouringCoat1CounterSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringCoat1CounterWrite.Enabled = !cmdScouringCoat1CounterStart.Enabled;
                grpScouringCoat1CounterRead.Enabled = !cmdScouringCoat1CounterStart.Enabled;
                // Property Grid
                pgScouringCoat1CounterWrite.SelectedObject = new FinishingCounter();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringCoat1CounterIP.Text = string.Empty;
                txtScouringCoat1CounterPortNo.Text = string.Empty;
                txtScouringCoat1CounterSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringCoat1CounterIP.Text = string.Empty;
                txtMScouringCoat1CounterPortNo.Text = string.Empty;
                txtMScouringCoat1CounterSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringCoat1CounterStart.Enabled = false;
                cmdScouringCoat1CounterStop.Enabled = false;
                // Write-Read GroupBox
                grpScouringCoat1CounterWrite.Enabled = !cmdScouringCoat1CounterStart.Enabled;
                grpScouringCoat1CounterRead.Enabled = !cmdScouringCoat1CounterStart.Enabled;
                // Property Grid
                pgScouringCoat1CounterWrite.SelectedObject = null;
            }

            #endregion

            #region ScouringLabModbusManager

            // Inspection Weight
            cfg = ScouringLabModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtScouringLabIP.Text = cfg.Slave.IP;
                txtScouringLabPortNo.Text = cfg.Slave.Port.ToString();
                txtScouringLabSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdScouringLabStart.Enabled = !_scouringLabSlave.IsRunning;
                cmdScouringLabStop.Enabled = !cmdScouringLabStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMScouringLabIP.Text = cfg.Master.IP;
                    txtMScouringLabPortNo.Text = cfg.Master.Port.ToString();
                    txtMScouringLabSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMScouringLabIP.Text = string.Empty;
                    txtMScouringLabPortNo.Text = string.Empty;
                    txtMScouringLabSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpScouringLabWrite.Enabled = !cmdScouringLabStart.Enabled;
                grpScouringLabRead.Enabled = !cmdScouringLabStart.Enabled;
                // Property Grid
                pgScouringLabWrite.SelectedObject = new ScouringLab();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtScouringLabIP.Text = string.Empty;
                txtScouringLabPortNo.Text = string.Empty;
                txtScouringLabSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMScouringLabIP.Text = string.Empty;
                txtMScouringLabPortNo.Text = string.Empty;
                txtMScouringLabSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdScouringLabStart.Enabled = false;
                cmdScouringLabStop.Enabled = false;
                // Write-Read GroupBox
                grpScouringLabWrite.Enabled = !cmdScouringLabStart.Enabled;
                grpScouringLabRead.Enabled = !cmdScouringLabStart.Enabled;
                // Property Grid
                pgScouringLabWrite.SelectedObject = null;
            }

            #endregion

            #region AirStaticLabModbusManager

            // Inspection Weight
            cfg = AirStaticLabModbusManager.Instance.Configs;

            if (null != cfg.Slave && cfg.Slave.Enabled)
            {
                // IP, Port, Slave Id (Slave)
                txtAirLabIP.Text = cfg.Slave.IP;
                txtAirLabPortNo.Text = cfg.Slave.Port.ToString();
                txtAirLabSlaveId.Text = cfg.Slave.SlaveId.ToString();
                // Start and Stop buttons
                cmdAirLabStart.Enabled = !_airLabSlave.IsRunning;
                cmdAirLabStop.Enabled = !cmdAirLabStart.Enabled;
                // IP, Port, Slave Id (Master)
                if (null != cfg.Master)
                {
                    txtMAirLabIP.Text = cfg.Master.IP;
                    txtMAirLabPortNo.Text = cfg.Master.Port.ToString();
                    txtMAirLabSlaveId.Text = cfg.Master.SlaveId.ToString();
                }
                else
                {
                    txtMAirLabIP.Text = string.Empty;
                    txtMAirLabPortNo.Text = string.Empty;
                    txtMAirLabSlaveId.Text = string.Empty;
                }
                // Write-Read GroupBox
                grpAirLabWrite.Enabled = !cmdAirLabStart.Enabled;
                grpAirLabRead.Enabled = !cmdAirLabStart.Enabled;
                // Property Grid
                pgAirLabWrite.SelectedObject = new AirStaticLab();
            }
            else
            {
                // IP, Port, Slave Id (Slave)
                txtAirLabIP.Text = string.Empty;
                txtAirLabPortNo.Text = string.Empty;
                txtAirLabSlaveId.Text = string.Empty;
                // IP, Port, Slave Id (Master)
                txtMAirLabIP.Text = string.Empty;
                txtMAirLabPortNo.Text = string.Empty;
                txtMAirLabSlaveId.Text = string.Empty;
                // Start and Stop buttons
                cmdAirLabStart.Enabled = false;
                cmdAirLabStop.Enabled = false;
                // Write-Read GroupBox
                grpAirLabWrite.Enabled = !cmdAirLabStart.Enabled;
                grpAirLabRead.Enabled = !cmdAirLabStart.Enabled;
                // Property Grid
                pgAirLabWrite.SelectedObject = null;
            }

            #endregion
        }

        #endregion

        #region Form Load/Closing

        private void Form2_Load(object sender, EventArgs e)
        {
            InitSlaves();
            InitModbusManagers();
            ValidateSlaveControls();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseModbusManagers();
            ReleaseSlaves();
        }

        #endregion

        #region Modbus Manager Handlers

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<InspectionWeight> e)
        {
            if (tabControl1.SelectedIndex != 0)
                return; // ignore if not shown.

            // Inspection Weight
            if (null == e.Value)
                return;

            lbInstWeightLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtW1.Text = e.Value.W1.ToString("n3");
            txtW2.Text = e.Value.W2.ToString("n3");
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Coating1> e)
        {
            if (tabControl1.SelectedIndex != 1)
                return; // ignore if not shown.

            // Coating1
            if (null == e.Value)
                return;

            lbCoating1LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtCoating1Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtCoating1Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtCoating1Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtCoating1Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtCoating1Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtCoating1Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtCoating1Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtCoating1Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtCoating1Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtCoating1Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtCoating1Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtCoating1Temp6SP.Text = e.Value.TEMP6SP.ToString();
            txtCoating1Temp7PV.Text = e.Value.TEMP7PV.ToString();
            txtCoating1Temp7SP.Text = e.Value.TEMP7SP.ToString();
            txtCoating1Temp8PV.Text = e.Value.TEMP8PV.ToString();
            txtCoating1Temp8SP.Text = e.Value.TEMP8SP.ToString();

            txtCoating1SATPV.Text = e.Value.SATPV.ToString();
            txtCoating1SATSP.Text = e.Value.SATSP.ToString();

            txtCoating1WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtCoating1WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtCoating1WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtCoating1WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtCoating1HOTPV.Text = e.Value.HOTPV.ToString();
            txtCoating1HOTSP.Text = e.Value.HOTSP.ToString();
            txtCoating1SPEED.Text = e.Value.SPEED.ToString();
            txtCoating1TENUP.Text = e.Value.TENUP.ToString();
            txtCoating1TENDOWN.Text = e.Value.TENDOWN.ToString();

            txtCoating1SEN1.Text = e.Value.SEN1.ToString();
            txtCoating1SEN2.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Coating2> e)
        {
            if (tabControl1.SelectedIndex != 2)
                return; // ignore if not shown.

            // Coating2
            if (null == e.Value)
                return;

            lbCoating2LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtCoating2Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtCoating2Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtCoating2Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtCoating2Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtCoating2Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtCoating2Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtCoating2Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtCoating2Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtCoating2Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtCoating2Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtCoating2Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtCoating2Temp6SP.Text = e.Value.TEMP6SP.ToString();
            txtCoating2Temp7PV.Text = e.Value.TEMP7PV.ToString();
            txtCoating2Temp7SP.Text = e.Value.TEMP7SP.ToString();
            txtCoating2Temp8PV.Text = e.Value.TEMP8PV.ToString();
            txtCoating2Temp8SP.Text = e.Value.TEMP8SP.ToString();
            txtCoating2Temp9PV.Text = e.Value.TEMP9PV.ToString();
            txtCoating2Temp9SP.Text = e.Value.TEMP9SP.ToString();
            txtCoating2Temp10PV.Text = e.Value.TEMP10PV.ToString();
            txtCoating2Temp10SP.Text = e.Value.TEMP10SP.ToString();

            txtCoating2SATPV.Text = e.Value.SATPV.ToString();
            txtCoating2SATSP.Text = e.Value.SATSP.ToString();

            txtCoating2WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtCoating2WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtCoating2WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtCoating2WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtCoating2HOTPV.Text = e.Value.HOTPV.ToString();
            txtCoating2HOTSP.Text = e.Value.HOTSP.ToString();
            txtCoating2SPEED.Text = e.Value.SPEED.ToString();
            txtCoating2TENUP.Text = e.Value.TENUP.ToString();
            txtCoating2TENDOWN.Text = e.Value.TENDOWN.ToString();

            txtCoating2SEN1.Text = e.Value.SEN1.ToString();
            txtCoating2SEN2.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Coating3> e)
        {
            if (tabControl1.SelectedIndex != 3)
                return; // ignore if not shown.

            // Coating3
            if (null == e.Value)
                return;

            lbCoating3LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtCoating3Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtCoating3Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtCoating3Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtCoating3Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtCoating3Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtCoating3Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtCoating3Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtCoating3Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtCoating3Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtCoating3Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtCoating3Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtCoating3Temp6SP.Text = e.Value.TEMP6SP.ToString();
            txtCoating3Temp7PV.Text = e.Value.TEMP7PV.ToString();
            txtCoating3Temp7SP.Text = e.Value.TEMP7SP.ToString();
            txtCoating3Temp8PV.Text = e.Value.TEMP8PV.ToString();
            txtCoating3Temp8SP.Text = e.Value.TEMP8SP.ToString();
            txtCoating3Temp9PV.Text = e.Value.TEMP9PV.ToString();
            txtCoating3Temp9SP.Text = e.Value.TEMP9SP.ToString();
            txtCoating3Temp10PV.Text = e.Value.TEMP10PV.ToString();
            txtCoating3Temp10SP.Text = e.Value.TEMP10SP.ToString();

            txtCoating3SATPV.Text = e.Value.SATPV.ToString();
            txtCoating3SATSP.Text = e.Value.SATSP.ToString();

            txtCoating3WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtCoating3WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtCoating3WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtCoating3WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtCoating3HOTPV.Text = e.Value.HOTPV.ToString();
            txtCoating3HOTSP.Text = e.Value.HOTSP.ToString();
            txtCoating3SPEED.Text = e.Value.SPEED.ToString();
            txtCoating3TENUP.Text = e.Value.TENUP.ToString();
            txtCoating3TENDOWN.Text = e.Value.TENDOWN.ToString();

            txtCoating3SEN1.Text = e.Value.SEN1.ToString();
            txtCoating3SEN2.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Scouring1> e)
        {
            if (tabControl1.SelectedIndex != 4)
                return; // ignore if not shown.

            // Scouring1
            if (null == e.Value)
                return;

            lbScouring1LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtScouring1Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtScouring1Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtScouring1Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtScouring1Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtScouring1Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtScouring1Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtScouring1Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtScouring1Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtScouring1Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtScouring1Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtScouring1Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtScouring1Temp6SP.Text = e.Value.TEMP6SP.ToString();


            txtScouring1SATPV.Text = e.Value.SATPV.ToString();
            txtScouring1SATPV.Text = e.Value.SATSP.ToString();

            txtScouring1WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtScouring1WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtScouring1WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtScouring1WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtScouring1HOTPV.Text = e.Value.HOTPV.ToString();
            txtScouring1HOTSP.Text = e.Value.HOTSP.ToString();
            txtScouring1SPEED.Text = e.Value.SPEED.ToString();

            txtScouring1SEN1.Text = e.Value.SEN1.ToString();
            txtScouring1SEN2.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Scouring2> e)
        {
            if (tabControl1.SelectedIndex != 5)
                return; // ignore if not shown.

            // Scouring2
            if (null == e.Value)
                return;

            lbScouring2LastUpdate.Text = "Last update : " + 
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtScouring2Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtScouring2Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtScouring2Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtScouring2Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtScouring2Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtScouring2Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtScouring2Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtScouring2Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtScouring2Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtScouring2Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtScouring2Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtScouring2Temp6SP.Text = e.Value.TEMP6SP.ToString();
            txtScouring2Temp7PV.Text = e.Value.TEMP7PV.ToString();
            txtScouring2Temp7SP.Text = e.Value.TEMP7SP.ToString();
            txtScouring2Temp8PV.Text = e.Value.TEMP8PV.ToString();
            txtScouring2Temp8SP.Text = e.Value.TEMP8SP.ToString();


            txtScouring2SATPV.Text = e.Value.SATPV.ToString();
            txtScouring2SATSP.Text = e.Value.SATSP.ToString();

            txtScouring2WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtScouring2WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtScouring2WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtScouring2WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtScouring2HOTPV.Text = e.Value.HOTPV.ToString();
            txtScouring2HOTSP.Text = e.Value.HOTSP.ToString();

            txtScouring2SPEED1.Text = e.Value.SPEED1.ToString();
            txtScouring2SPEED2.Text = e.Value.SPEED2.ToString();

            txtScouring2SEN1.Text = e.Value.SEN1.ToString();
            txtScouring2SEN2.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Coating3Scouring> e)
        {
            if (tabControl1.SelectedIndex != 11)
                return; // ignore if not shown.

            // Coating3Scouring
            if (null == e.Value)
                return;

            lbCoating3ScouringLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtCoating3ScouringTemp1PV.Text = e.Value.TEMP1PV.ToString();
            txtCoating3ScouringTemp1SP.Text = e.Value.TEMP1SP.ToString();
            txtCoating3ScouringTemp2PV.Text = e.Value.TEMP2PV.ToString();
            txtCoating3ScouringTemp2SP.Text = e.Value.TEMP2SP.ToString();
            txtCoating3ScouringTemp3PV.Text = e.Value.TEMP3PV.ToString();
            txtCoating3ScouringTemp3SP.Text = e.Value.TEMP3SP.ToString();
            txtCoating3ScouringTemp4PV.Text = e.Value.TEMP4PV.ToString();
            txtCoating3ScouringTemp4SP.Text = e.Value.TEMP4SP.ToString();
            txtCoating3ScouringTemp5PV.Text = e.Value.TEMP5PV.ToString();
            txtCoating3ScouringTemp5SP.Text = e.Value.TEMP5SP.ToString();
            txtCoating3ScouringTemp6PV.Text = e.Value.TEMP6PV.ToString();
            txtCoating3ScouringTemp6SP.Text = e.Value.TEMP6SP.ToString();
            txtCoating3ScouringTemp7PV.Text = e.Value.TEMP7PV.ToString();
            txtCoating3ScouringTemp7SP.Text = e.Value.TEMP7SP.ToString();
            txtCoating3ScouringTemp8PV.Text = e.Value.TEMP8PV.ToString();
            txtCoating3ScouringTemp8SP.Text = e.Value.TEMP8SP.ToString();
            txtCoating3ScouringTemp9PV.Text = e.Value.TEMP9PV.ToString();
            txtCoating3ScouringTemp9SP.Text = e.Value.TEMP9SP.ToString();
            txtCoating3ScouringTemp10PV.Text = e.Value.TEMP10PV.ToString();
            txtCoating3ScouringTemp10SP.Text = e.Value.TEMP10SP.ToString();

            txtCoating3ScouringSATPV.Text = e.Value.SATPV.ToString();
            txtCoating3ScouringSATSP.Text = e.Value.SATSP.ToString();

            txtCoating3ScouringWASH1PV.Text = e.Value.WASH1PV.ToString();
            txtCoating3ScouringWASH1SP.Text = e.Value.WASH1SP.ToString();
            txtCoating3ScouringWASH2PV.Text = e.Value.WASH2PV.ToString();
            txtCoating3ScouringWASH2SP.Text = e.Value.WASH2SP.ToString();

            txtCoating3ScouringHOTPV.Text = e.Value.HOTPV.ToString();
            txtCoating3ScouringHOTSP.Text = e.Value.HOTSP.ToString();
            txtCoating3ScouringSPEED.Text = e.Value.SPEED.ToString();
            txtCoating3ScouringTENUP.Text = e.Value.TENUP.ToString();
            txtCoating3ScouringTENDOWN.Text = e.Value.TENDOWN.ToString();

            txtCoating3ScouringSEN1.Text = e.Value.SEN1.ToString();
            txtCoating3ScouringSEN2.Text = e.Value.SEN2.ToString();
        }

        void ScouringCounter1_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 6)
                return; // ignore if not shown.

            // Scouring Counter 1
            if (null == e.Value)
                return;

            lbScouringCounter1LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryScouringCounter1.Update(e.Value);
        }

        void ScouringCounter2_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 7)
                return; // ignore if not shown.

            // Scouring Counter 2
            if (null == e.Value)
                return;

            lbScouringCounter2LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryScouringCounter2.Update(e.Value);
        }

        void CoatingCounter1_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 8)
                return; // ignore if not shown.

            // Coating Counter 1
            if (null == e.Value)
                return;

            lbCoatingCounter1LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryCoatingCounter1.Update(e.Value);
        }

        void CoatingCounter2_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 9)
                return; // ignore if not shown.

            // Coating Counter 2
            if (null == e.Value)
                return;

            lbCoatingCounter2LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryCoatingCounter2.Update(e.Value);
        }

        void ScouringDryerCounter_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 10)
                return; // ignore if not shown.

            // Scouring Dryer
            if (null == e.Value)
                return;

            lbScouringDryerCounterLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryScouringDryerCounter.Update(e.Value);
        }

        void Coating3ScouringCounter_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 12)
                return; // ignore if not shown.

            // Coating3 Scouring Counter
            if (null == e.Value)
                return;

            lbCoating3ScouringCounterLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryCoating3ScouringCounter.Update(e.Value);
        }

        void ScouringCoat2Counter_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 14)
                return; // ignore if not shown.

            // Scouring Counter 2
            if (null == e.Value)
                return;

            lbScouringCoat2CounterLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryScouringCoat2Counter.Update(e.Value);
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<ScouringCoat2> e)
        {
            if (tabControl1.SelectedIndex != 13)
                return; // ignore if not shown.

            // ScouringCoat2
            if (null == e.Value)
                return;

            lbScouringCoat2LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtScouringCoat2Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtScouringCoat2Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtScouringCoat2Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtScouringCoat2Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtScouringCoat2Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtScouringCoat2Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtScouringCoat2Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtScouringCoat2Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtScouringCoat2Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtScouringCoat2Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtScouringCoat2Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtScouringCoat2Temp6SP.Text = e.Value.TEMP6SP.ToString();
            txtScouringCoat2Temp7PV.Text = e.Value.TEMP7PV.ToString();
            txtScouringCoat2Temp7SP.Text = e.Value.TEMP7SP.ToString();
            txtScouringCoat2Temp8PV.Text = e.Value.TEMP8PV.ToString();
            txtScouringCoat2Temp8SP.Text = e.Value.TEMP8SP.ToString();
            txtScouringCoat2Temp9PV.Text = e.Value.TEMP9PV.ToString();
            txtScouringCoat2Temp9SP.Text = e.Value.TEMP9SP.ToString();
            txtScouringCoat2Temp10PV.Text = e.Value.TEMP10PV.ToString();
            txtScouringCoat2Temp10SP.Text = e.Value.TEMP10SP.ToString();

            txtScouringCoat2SATPV.Text = e.Value.SATPV.ToString();
            txtScouringCoat2SATSP.Text = e.Value.SATSP.ToString();

            txtScouringCoat2WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtScouringCoat2WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtScouringCoat2WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtScouringCoat2WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtScouringCoat2HOTPV.Text = e.Value.HOTPV.ToString();
            txtScouringCoat2HOTSP.Text = e.Value.HOTSP.ToString();
            txtScouringCoat2SPEED.Text = e.Value.SPEED.ToString();
            txtScouringCoat2TENUP.Text = e.Value.TENUP.ToString();
            txtScouringCoat2TENDOWN.Text = e.Value.TENDOWN.ToString();

            txtScouringCoat2SEN1.Text = e.Value.SEN1.ToString();
            txtScouringCoat2SEN2.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Coating1Scouring> e)
        {
            if (tabControl1.SelectedIndex != 15)
                return; // ignore if not shown.

            // Coating1
            if (null == e.Value)
                return;

            lbCoating1ScouringLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtCoating1ScouringTemp1PV.Text = e.Value.TEMP1PV.ToString();
            txtCoating1ScouringTemp1SP.Text = e.Value.TEMP1SP.ToString();
            txtCoating1ScouringTemp2PV.Text = e.Value.TEMP2PV.ToString();
            txtCoating1ScouringTemp2SP.Text = e.Value.TEMP2SP.ToString();
            txtCoating1ScouringTemp3PV.Text = e.Value.TEMP3PV.ToString();
            txtCoating1ScouringTemp3SP.Text = e.Value.TEMP3SP.ToString();
            txtCoating1ScouringTemp4PV.Text = e.Value.TEMP4PV.ToString();
            txtCoating1ScouringTemp4SP.Text = e.Value.TEMP4SP.ToString();
            txtCoating1ScouringTemp5PV.Text = e.Value.TEMP5PV.ToString();
            txtCoating1ScouringTemp5SP.Text = e.Value.TEMP5SP.ToString();
            txtCoating1ScouringTemp6PV.Text = e.Value.TEMP6PV.ToString();
            txtCoating1ScouringTemp6SP.Text = e.Value.TEMP6SP.ToString();
            txtCoating1ScouringTemp7PV.Text = e.Value.TEMP7PV.ToString();
            txtCoating1ScouringTemp7SP.Text = e.Value.TEMP7SP.ToString();
            txtCoating1ScouringTemp8PV.Text = e.Value.TEMP8PV.ToString();
            txtCoating1ScouringTemp8SP.Text = e.Value.TEMP8SP.ToString();

            txtCoating1ScouringSATPV.Text = e.Value.SATPV.ToString();
            txtCoating1ScouringSATSP.Text = e.Value.SATSP.ToString();

            txtCoating1ScouringWASH1PV.Text = e.Value.WASH1PV.ToString();
            txtCoating1ScouringWASH1SP.Text = e.Value.WASH1SP.ToString();
            txtCoating1ScouringWASH2PV.Text = e.Value.WASH2PV.ToString();
            txtCoating1ScouringWASH2SP.Text = e.Value.WASH2SP.ToString();

            txtCoating1ScouringHOTPV.Text = e.Value.HOTPV.ToString();
            txtCoating1ScouringHOTSP.Text = e.Value.HOTSP.ToString();
            txtCoating1ScouringSPEED.Text = e.Value.SPEED.ToString();
            txtCoating1ScouringTENUP.Text = e.Value.TENUP.ToString();
            txtCoating1ScouringTENDOWN.Text = e.Value.TENDOWN.ToString();

            txtCoating1ScouringSEN1.Text = e.Value.SEN1.ToString();
            txtCoating1ScouringSEN2.Text = e.Value.SEN2.ToString();
        }
       
        void CoatingCounter1Scouring_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 16)
                return; // ignore if not shown.

            // Coating Counter 1
            if (null == e.Value)
                return;

            lbCoatingCounter1ScouringLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryCoatingCounter1Scouring.Update(e.Value);
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<Scouring2ScouringDry> e)
        {
            if (tabControl1.SelectedIndex != 17)
                return; // ignore if not shown.

            // Scouring2ScouringDry
            if (null == e.Value)
                return;

            lbScouring2LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtScouring2ScouringDryTemp1PV.Text = e.Value.TEMP1PV.ToString();
            txtScouring2ScouringDryTemp1SP.Text = e.Value.TEMP1SP.ToString();
            txtScouring2ScouringDryTemp2PV.Text = e.Value.TEMP2PV.ToString();
            txtScouring2ScouringDryTemp2SP.Text = e.Value.TEMP2SP.ToString();
            txtScouring2ScouringDryTemp3PV.Text = e.Value.TEMP3PV.ToString();
            txtScouring2ScouringDryTemp3SP.Text = e.Value.TEMP3SP.ToString();
            txtScouring2ScouringDryTemp4PV.Text = e.Value.TEMP4PV.ToString();
            txtScouring2ScouringDryTemp4SP.Text = e.Value.TEMP4SP.ToString();
            txtScouring2ScouringDryTemp5PV.Text = e.Value.TEMP5PV.ToString();
            txtScouring2ScouringDryTemp5SP.Text = e.Value.TEMP5SP.ToString();
            txtScouring2ScouringDryTemp6PV.Text = e.Value.TEMP6PV.ToString();
            txtScouring2ScouringDryTemp6SP.Text = e.Value.TEMP6SP.ToString();
            txtScouring2ScouringDryTemp7PV.Text = e.Value.TEMP7PV.ToString();
            txtScouring2ScouringDryTemp7SP.Text = e.Value.TEMP7SP.ToString();
            txtScouring2ScouringDryTemp8PV.Text = e.Value.TEMP8PV.ToString();
            txtScouring2ScouringDryTemp8SP.Text = e.Value.TEMP8SP.ToString();


            txtScouring2ScouringDrySATPV.Text = e.Value.SATPV.ToString();
            txtScouring2ScouringDrySATSP.Text = e.Value.SATSP.ToString();

            txtScouring2ScouringDryWASH1PV.Text = e.Value.WASH1PV.ToString();
            txtScouring2ScouringDryWASH1SP.Text = e.Value.WASH1SP.ToString();
            txtScouring2ScouringDryWASH2PV.Text = e.Value.WASH2PV.ToString();
            txtScouring2ScouringDryWASH2SP.Text = e.Value.WASH2SP.ToString();

            txtScouring2ScouringDryHOTPV.Text = e.Value.HOTPV.ToString();
            txtScouring2ScouringDryHOTSP.Text = e.Value.HOTSP.ToString();

            txtScouring2ScouringDrySPEED1.Text = e.Value.SPEED1.ToString();
            txtScouring2ScouringDrySPEED2.Text = e.Value.SPEED2.ToString();

            txtScouring2ScouringDrySEN1.Text = e.Value.SEN1.ToString();
            txtScouring2ScouringDrySEN2.Text = e.Value.SEN2.ToString();
        }

        void Scouring2ScouringDryCounter_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 18)
                return; // ignore if not shown.

            // Scouring Counter 2
            if (null == e.Value)
                return;

            lbScouring2ScouringDryCounterLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryScouring2ScouringDryCounter.Update(e.Value);
        }

        void Coating3_ReadCompleted(object sender, ModbusReadEventArgs<Coating3> e)
        {
            if (tabControl1.SelectedIndex != 19)
                return; // ignore if not shown.

            // Coating3
            if (null == e.Value)
                return;

            lbCoating3TestLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtCoating3Temp1PVTest.Text = e.Value.TEMP1PV.ToString();
            txtCoating3Temp1SPTest.Text = e.Value.TEMP1SP.ToString();
            txtCoating3Temp2PVTest.Text = e.Value.TEMP2PV.ToString();
            txtCoating3Temp2SPTest.Text = e.Value.TEMP2SP.ToString();
            txtCoating3Temp3PVTest.Text = e.Value.TEMP3PV.ToString();
            txtCoating3Temp3SPTest.Text = e.Value.TEMP3SP.ToString();
            txtCoating3Temp4PVTest.Text = e.Value.TEMP4PV.ToString();
            txtCoating3Temp4SPTest.Text = e.Value.TEMP4SP.ToString();
            txtCoating3Temp5PVTest.Text = e.Value.TEMP5PV.ToString();
            txtCoating3Temp5SPTest.Text = e.Value.TEMP5SP.ToString();
            txtCoating3Temp6PVTest.Text = e.Value.TEMP6PV.ToString();
            txtCoating3Temp6SPTest.Text = e.Value.TEMP6SP.ToString();
            txtCoating3Temp7PVTest.Text = e.Value.TEMP7PV.ToString();
            txtCoating3Temp7SPTest.Text = e.Value.TEMP7SP.ToString();
            txtCoating3Temp8PVTest.Text = e.Value.TEMP8PV.ToString();
            txtCoating3Temp8SPTest.Text = e.Value.TEMP8SP.ToString();
            txtCoating3Temp9PVTest.Text = e.Value.TEMP9PV.ToString();
            txtCoating3Temp9SPTest.Text = e.Value.TEMP9SP.ToString();
            txtCoating3Temp10PVTest.Text = e.Value.TEMP10PV.ToString();
            txtCoating3Temp10SPTest.Text = e.Value.TEMP10SP.ToString();

            txtCoating3SATPVTest.Text = e.Value.SATPV.ToString();
            txtCoating3SATSPTest.Text = e.Value.SATSP.ToString();

            txtCoating3WASH1PVTest.Text = e.Value.WASH1PV.ToString();
            txtCoating3WASH1SPTest.Text = e.Value.WASH1SP.ToString();
            txtCoating3WASH2PVTest.Text = e.Value.WASH2PV.ToString();
            txtCoating3WASH2SPTest.Text = e.Value.WASH2SP.ToString();

            txtCoating3HOTPVTest.Text = e.Value.HOTPV.ToString();
            txtCoating3HOTSPTest.Text = e.Value.HOTSP.ToString();
            txtCoating3SPEEDTest.Text = e.Value.SPEED.ToString();
            txtCoating3TENUPTest.Text = e.Value.TENUP.ToString();
            txtCoating3TENDOWNTest.Text = e.Value.TENDOWN.ToString();

            txtCoating3SEN1Test.Text = e.Value.SEN1.ToString();
            txtCoating3SEN2Test.Text = e.Value.SEN2.ToString();
        }

        void Scouring2_ReadCompleted(object sender, ModbusReadEventArgs<Scouring2> e)
        {
            if (tabControl1.SelectedIndex != 20)
                return; // ignore if not shown.

            // Scouring2
            if (null == e.Value)
                return;

            lbScouring2TestLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtScouring2Temp1PVTest.Text = e.Value.TEMP1PV.ToString();
            txtScouring2Temp1SPTest.Text = e.Value.TEMP1SP.ToString();
            txtScouring2Temp2PVTest.Text = e.Value.TEMP2PV.ToString();
            txtScouring2Temp2SPTest.Text = e.Value.TEMP2SP.ToString();
            txtScouring2Temp3PVTest.Text = e.Value.TEMP3PV.ToString();
            txtScouring2Temp3SPTest.Text = e.Value.TEMP3SP.ToString();
            txtScouring2Temp4PVTest.Text = e.Value.TEMP4PV.ToString();
            txtScouring2Temp4SPTest.Text = e.Value.TEMP4SP.ToString();
            txtScouring2Temp5PVTest.Text = e.Value.TEMP5PV.ToString();
            txtScouring2Temp5SPTest.Text = e.Value.TEMP5SP.ToString();
            txtScouring2Temp6PVTest.Text = e.Value.TEMP6PV.ToString();
            txtScouring2Temp6SPTest.Text = e.Value.TEMP6SP.ToString();
            txtScouring2Temp7PVTest.Text = e.Value.TEMP7PV.ToString();
            txtScouring2Temp7SPTest.Text = e.Value.TEMP7SP.ToString();
            txtScouring2Temp8PVTest.Text = e.Value.TEMP8PV.ToString();
            txtScouring2Temp8SPTest.Text = e.Value.TEMP8SP.ToString();


            txtScouring2SATPVTest.Text = e.Value.SATPV.ToString();
            txtScouring2SATSPTest.Text = e.Value.SATSP.ToString();

            txtScouring2WASH1PVTest.Text = e.Value.WASH1PV.ToString();
            txtScouring2WASH1SPTest.Text = e.Value.WASH1SP.ToString();
            txtScouring2WASH2PVTest.Text = e.Value.WASH2PV.ToString();
            txtScouring2WASH2SPTest.Text = e.Value.WASH2SP.ToString();

            txtScouring2HOTPVTest.Text = e.Value.HOTPV.ToString();
            txtScouring2HOTSPTest.Text = e.Value.HOTSP.ToString();

            txtScouring2SPEED1Test.Text = e.Value.SPEED1.ToString();
            txtScouring2SPEED2Test.Text = e.Value.SPEED2.ToString();

            txtScouring2SEN1Test.Text = e.Value.SEN1.ToString();
            txtScouring2SEN2Test.Text = e.Value.SEN2.ToString();
        }

        void Instance_ReadCompleted(object sender, ModbusReadEventArgs<ScouringCoat1> e)
        {
            if (tabControl1.SelectedIndex != 21)
                return; // ignore if not shown.

            // Coating1
            if (null == e.Value)
                return;

            lbScouringCoat1LastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            txtScouringCoat1Temp1PV.Text = e.Value.TEMP1PV.ToString();
            txtScouringCoat1Temp1SP.Text = e.Value.TEMP1SP.ToString();
            txtScouringCoat1Temp2PV.Text = e.Value.TEMP2PV.ToString();
            txtScouringCoat1Temp2SP.Text = e.Value.TEMP2SP.ToString();
            txtScouringCoat1Temp3PV.Text = e.Value.TEMP3PV.ToString();
            txtScouringCoat1Temp3SP.Text = e.Value.TEMP3SP.ToString();
            txtScouringCoat1Temp4PV.Text = e.Value.TEMP4PV.ToString();
            txtScouringCoat1Temp4SP.Text = e.Value.TEMP4SP.ToString();
            txtScouringCoat1Temp5PV.Text = e.Value.TEMP5PV.ToString();
            txtScouringCoat1Temp5SP.Text = e.Value.TEMP5SP.ToString();
            txtScouringCoat1Temp6PV.Text = e.Value.TEMP6PV.ToString();
            txtScouringCoat1Temp6SP.Text = e.Value.TEMP6SP.ToString();
            txtScouringCoat1Temp7PV.Text = e.Value.TEMP7PV.ToString();
            txtScouringCoat1Temp7SP.Text = e.Value.TEMP7SP.ToString();
            txtScouringCoat1Temp8PV.Text = e.Value.TEMP8PV.ToString();
            txtScouringCoat1Temp8SP.Text = e.Value.TEMP8SP.ToString();

            txtCoating1SATPV.Text = e.Value.SATPV.ToString();
            txtCoating1SATSP.Text = e.Value.SATSP.ToString();

            txtCoating1WASH1PV.Text = e.Value.WASH1PV.ToString();
            txtCoating1WASH1SP.Text = e.Value.WASH1SP.ToString();
            txtCoating1WASH2PV.Text = e.Value.WASH2PV.ToString();
            txtCoating1WASH2SP.Text = e.Value.WASH2SP.ToString();

            txtCoating1HOTPV.Text = e.Value.HOTPV.ToString();
            txtCoating1HOTSP.Text = e.Value.HOTSP.ToString();
            txtCoating1SPEED.Text = e.Value.SPEED.ToString();
            txtCoating1TENUP.Text = e.Value.TENUP.ToString();
            txtCoating1TENDOWN.Text = e.Value.TENDOWN.ToString();

            txtCoating1SEN1.Text = e.Value.SEN1.ToString();
            txtCoating1SEN2.Text = e.Value.SEN2.ToString();
        }

        void ScouringCoat1Counter_ReadCompleted(object sender, ModbusReadEventArgs<FinishingCounter> e)
        {
            if (tabControl1.SelectedIndex != 22)
                return; // ignore if not shown.

            // Scouring Counter 1
            if (null == e.Value)
                return;

            lbScouringCoat1CounterLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            entryScouringCoat1Counter.Update(e.Value);
        }

        void ScouringLab_ReadCompleted(object sender, ModbusReadEventArgs<ScouringLab> e)
        {
            if (tabControl1.SelectedIndex != 23)
                return; // ignore if not shown.

            if (null == e.Value)
                return;

            lbScouringLabLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            if (e.Value.AIRFLAG != null)
            {
                if (e.Value.AIRFLAG == 1)
                {
                    if (e.Value.AIR != null)
                        txtAIR.Text = e.Value.AIR.ToString();
                    else
                        txtAIR.Text = "0";
                }
                else
                    txtAIR.Text = "0";
            }
            else
                txtAIR.Text = "0";

            if (e.Value.DYNAMICFLAG != null)
            {
                if (e.Value.DYNAMICFLAG == 1)
                {
                    if (e.Value.DYNAMIC != null)
                        txtDYNAMIC.Text = e.Value.DYNAMIC.ToString();
                    else
                        txtDYNAMIC.Text = "0";
                }
                else
                    txtDYNAMIC.Text = "0";
            }
            else
                txtDYNAMIC.Text = "0";

            if (e.Value.HIGHPRESSFLAG != null)
            {
                if (e.Value.HIGHPRESSFLAG == 1)
                {
                    if (e.Value.HIGHPRESS != null)
                        txtHIGHPRESS.Text = e.Value.HIGHPRESS.ToString();
                    else
                        txtHIGHPRESS.Text = "0";

                    if (e.Value.EXPONENT != null)
                        txtEXPONENT.Text = e.Value.EXPONENT.ToString();
                    else
                        txtEXPONENT.Text = "0";
                }
                else
                {
                    txtHIGHPRESS.Text = "0";
                    txtEXPONENT.Text = "0";
                }
            }
            else
            {
                txtHIGHPRESS.Text = "0";
                txtEXPONENT.Text = "0";
            }

            if (e.Value.WEIGHTFLAG != null)
            {
                if (e.Value.WEIGHTFLAG == 1)
                {
                    if (e.Value.WEIGHT != null)
                        txtWEIGHT.Text = e.Value.WEIGHT.ToString();
                    else
                        txtWEIGHT.Text = "0";
                }
                else
                    txtWEIGHT.Text = "0";
            }
            else
                txtWEIGHT.Text = "0";
        }

        void AirStaticLab_ReadCompleted(object sender, ModbusReadEventArgs<AirStaticLab> e)
        {
            if (tabControl1.SelectedIndex != 24)
                return; // ignore if not shown.

            if (null == e.Value)
                return;

            lbAirLabLastUpdate.Text = "Last update : " +
                DateTime.Now.ToString("HH:mm:ss.fff");

            if (e.Value.AIRFLAG != null)
            {
                if (e.Value.AIRFLAG == 1)
                {
                    if (e.Value.AIR != null)
                        txtAIRLab.Text = e.Value.AIR.ToString();
                    else
                        txtAIRLab.Text = "0";
                }
                else
                    txtAIRLab.Text = "0";
            }
            else
                txtAIRLab.Text = "0";

           
        }

        #endregion

        #region Tab 1 - Inspection Weight

        private void cmdInspWeightStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = InspectionWeightModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _inspWeightSlave && null != cfg.Slave)
                _inspWeightSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdInspWeightStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _inspWeightSlave)
                _inspWeightSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdInspWeightWrite_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Tab 2 - Coating 1

        private void cmdCoating1Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating1ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating1Slave && null != cfg.Slave)
                _coating1Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating1Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating1Slave)
                _coating1Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating1Write_Click(object sender, EventArgs e)
        {
            if (null == pgCoating1Write.SelectedObject &&
                !(pgCoating1Write.SelectedObject is Coating1))
                return;
            Coating1 inst = (pgCoating1Write.SelectedObject as Coating1);
            Coating1ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 3 - Coating 2

        private void cmdCoating2Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating2ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating2Slave && null != cfg.Slave)
                _coating2Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating2Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating2Slave)
                _coating2Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating2Write_Click(object sender, EventArgs e)
        {
            if (null == pgCoating2Write.SelectedObject &&
                !(pgCoating2Write.SelectedObject is Coating2))
                return;
            Coating2 inst = (pgCoating2Write.SelectedObject as Coating2);
            Coating2ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 11 - Coating 3

        private void cmdCoating3Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating3ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating3Slave && null != cfg.Slave)
                _coating3Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating3Slave)
                _coating3Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3Write_Click(object sender, EventArgs e)
        {
            if (null == pgCoating3Write.SelectedObject &&
                !(pgCoating3Write.SelectedObject is Coating3))
                return;
            Coating3 inst = (pgCoating3Write.SelectedObject as Coating3);
            Coating3ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 4 - Scouring 1

        private void cmdScouring1Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Scouring1ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouring1Slave && null != cfg.Slave)
                _scouring1Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring1Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouring1Slave)
                _scouring1Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring1Write_Click(object sender, EventArgs e)
        {
            if (null == pgScouring1Write.SelectedObject &&
                !(pgScouring1Write.SelectedObject is Scouring1))
                return;
            Scouring1 inst = (pgScouring1Write.SelectedObject as Scouring1);
            Scouring1ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 5 - Scouring 2

        private void cmdScouring2Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Scouring2ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouring2Slave && null != cfg.Slave)
                _scouring2Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouring2Slave)
                _scouring2Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2Write_Click(object sender, EventArgs e)
        {
            if (null == pgScouring2Write.SelectedObject &&
                !(pgScouring2Write.SelectedObject is Scouring2))
                return;
            Scouring2 inst = (pgScouring2Write.SelectedObject as Scouring2);
            Scouring2ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 6 - Scouring Counter 1

        private void cmdScouringCounter1Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringCounter1ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringCounter1Slave && null != cfg.Slave)
                _scouringCounter1Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCounter1Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringCounter1Slave)
                _scouringCounter1Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCounter1Write_Click(object sender, EventArgs e)
        {
            if (null == pgScouringCounter1Write.SelectedObject &&
                !(pgScouringCounter1Write.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgScouringCounter1Write.SelectedObject as FinishingCounter);
            ScouringCounter1ModbusManager.Instance.Write(inst);
        }

        private void cmdScouringCounter1Reset_Click(object sender, EventArgs e)
        {
            ScouringCounter1ModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 7 - Scouring Counter 2

        private void cmdScouringCounter2Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringCounter2ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringCounter2Slave && null != cfg.Slave)
                _scouringCounter2Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCounter2Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringCounter2Slave)
                _scouringCounter2Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCounter2Write_Click(object sender, EventArgs e)
        {
            if (null == pgScouringCounter2Write.SelectedObject &&
                !(pgScouringCounter2Write.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgScouringCounter2Write.SelectedObject as FinishingCounter);
            ScouringCounter2ModbusManager.Instance.Write(inst);
        }

        private void cmdScouringCounter2Reset_Click(object sender, EventArgs e)
        {
            ScouringCounter2ModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 8 - Coating Counter 1

        private void cmdCoatingCounter1Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = CoatingCounter1ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coatingCounter1Slave && null != cfg.Slave)
                _coatingCounter1Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoatingCounter1Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coatingCounter1Slave)
                _coatingCounter1Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoatingCounter1Write_Click(object sender, EventArgs e)
        {
            if (null == pgCoatingCounter1Write.SelectedObject &&
                !(pgCoatingCounter1Write.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst = 
                (pgCoatingCounter1Write.SelectedObject as FinishingCounter);
            CoatingCounter1ModbusManager.Instance.Write(inst);
        }

        private void cmdCoatingCounter1Reset_Click(object sender, EventArgs e)
        {
            CoatingCounter1ModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 9 - Coating Counter 2

        private void cmdCoatingCounter2Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = CoatingCounter2ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coatingCounter2Slave && null != cfg.Slave)
                _coatingCounter2Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoatingCounter2Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coatingCounter2Slave)
                _coatingCounter2Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoatingCounter2Write_Click(object sender, EventArgs e)
        {
            if (null == pgCoatingCounter2Write.SelectedObject &&
                !(pgCoatingCounter2Write.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst = 
                (pgCoatingCounter2Write.SelectedObject as FinishingCounter);
            CoatingCounter2ModbusManager.Instance.Write(inst);
        }

        private void cmdCoatingCounter2Reset_Click(object sender, EventArgs e)
        {
            CoatingCounter2ModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 10 - Scouring Dryer Counter

        private void cmdScouringDryerCounterStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringDryerCounterModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringDryerCounterSlave && null != cfg.Slave)
                _scouringDryerCounterSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringDryerCounterStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringDryerCounterSlave)
                _scouringDryerCounterSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringDryerCounterWrite_Click(object sender, EventArgs e)
        {
            if (null == pgScouringDryerCounterWrite.SelectedObject &&
                !(pgScouringDryerCounterWrite.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst = 
                (pgScouringDryerCounterWrite.SelectedObject as FinishingCounter);
            ScouringDryerCounterModbusManager.Instance.Write(inst);
        }

        private void cmdScouringDryerCounterReset_Click(object sender, EventArgs e)
        {
            ScouringDryerCounterModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 12 - Coating 3 Scouring

        private void cmdCoating3ScouringStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating3ScouringModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating3ScouringSlave && null != cfg.Slave)
                _coating3ScouringSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3ScouringStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating3ScouringSlave)
                _coating3ScouringSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3ScouringWrite_Click(object sender, EventArgs e)
        {
            if (null == pgCoating3ScouringWrite.SelectedObject &&
                !(pgCoating3ScouringWrite.SelectedObject is Coating3Scouring))
                return;
            Coating3Scouring inst = (pgCoating3ScouringWrite.SelectedObject as Coating3Scouring);
            Coating3ScouringModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 13 - Coating 3 Scouring Counter

        private void cmdCoating3ScouringCounterStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating3ScouringCounterModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating3ScouringCounterSlave && null != cfg.Slave)
                _coating3ScouringCounterSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3ScouringCounterStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating3ScouringCounterSlave)
                _coating3ScouringCounterSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3ScouringCounterReset_Click(object sender, EventArgs e)
        {
            Coating3ScouringCounterModbusManager.Instance.Reset();
        }

        private void cmdCoating3ScouringCounterWrite_Click(object sender, EventArgs e)
        {
            if (null == pgCoating3ScouringCounterWrite.SelectedObject &&
               !(pgCoating3ScouringCounterWrite.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgCoating3ScouringCounterWrite.SelectedObject as FinishingCounter);
            Coating3ScouringCounterModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 14 - ScouringCoat2

        private void cmdScouringCoat2Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringCoat2ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringCoat2Slave && null != cfg.Slave)
                _scouringCoat2Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat2Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringCoat2Slave)
                _scouringCoat2Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat2Write_Click(object sender, EventArgs e)
        {
            if (null == pgScouringCoat2Write.SelectedObject &&
                !(pgScouringCoat2Write.SelectedObject is ScouringCoat2))
                return;
            ScouringCoat2 inst = (pgScouringCoat2Write.SelectedObject as ScouringCoat2);
            ScouringCoat2ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 15 - Scouring Coat 2 Counter

        private void cmdScouringCoat2CounterStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringCoat2CounterModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringCounter2Slave && null != cfg.Slave)
                _scouringCoat2CounterSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat2CounterStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringCounter2Slave)
                _scouringCoat2CounterSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat2CounterWrite_Click(object sender, EventArgs e)
        {
            if (null == pgScouringCoat2CounterWrite.SelectedObject &&
                !(pgScouringCoat2CounterWrite.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgScouringCoat2CounterWrite.SelectedObject as FinishingCounter);
            ScouringCoat2CounterModbusManager.Instance.Write(inst);
        }

        private void cmdScouringCoat2CounterReset_Click(object sender, EventArgs e)
        {
            ScouringCoat2CounterModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 16 - Coating 1 Scouring

        private void cmdCoating1ScouringStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating1ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating1ScouringSlave && null != cfg.Slave)
                _coating1ScouringSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating1ScouringStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating1ScouringSlave)
                _coating1ScouringSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating1ScouringWrite_Click(object sender, EventArgs e)
        {
            if (null == pgCoating1ScouringWrite.SelectedObject &&
                !(pgCoating1ScouringWrite.SelectedObject is Coating1))
                return;
            Coating1Scouring inst = (pgCoating1ScouringWrite.SelectedObject as Coating1Scouring);
            Coating1ScouringModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 17 - Coating Counter 1 Scouring

        private void cmdCoatingCounter1ScouringStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = CoatingCounter1ScouringModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coatingCounter1ScouringSlave && null != cfg.Slave)
                _coatingCounter1ScouringSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoatingCounter1ScouringStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coatingCounter1ScouringSlave)
                _coatingCounter1ScouringSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoatingCounter1ScouringWrite_Click(object sender, EventArgs e)
        {
            if (null == pgCoatingCounter1ScouringWrite.SelectedObject &&
                !(pgCoatingCounter1ScouringWrite.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgCoatingCounter1ScouringWrite.SelectedObject as FinishingCounter);
            CoatingCounter1ScouringModbusManager.Instance.Write(inst);
        }

        private void cmdCoatingCounter1ScouringReset_Click(object sender, EventArgs e)
        {
            CoatingCounter1ScouringModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 18 - Scouring2ScouringDry

        private void cmdScouring2ScouringDryStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Scouring2ScouringDryModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouring2ScouringDrySlave && null != cfg.Slave)
                _scouring2ScouringDrySlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2ScouringDryStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouring2ScouringDrySlave)
                _scouring2ScouringDrySlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2ScouringDryWrite_Click(object sender, EventArgs e)
        {
            if (null == pgScouring2ScouringDryWrite.SelectedObject &&
                !(pgScouring2ScouringDryWrite.SelectedObject is Scouring2ScouringDry))
                return;
            Scouring2ScouringDry inst = (pgScouring2ScouringDryWrite.SelectedObject as Scouring2ScouringDry);
            Scouring2ScouringDryModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 19 - Scouring2ScouringDry Counter 

        private void cmdScouring2ScouringDryCounterStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Scouring2ScouringDryCounterModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouring2ScouringDryCounterSlave && null != cfg.Slave)
                _scouring2ScouringDryCounterSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2ScouringDryCounterStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouring2ScouringDryCounterSlave)
                _scouring2ScouringDryCounterSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2ScouringDryCounterWrite_Click(object sender, EventArgs e)
        {
            if (null == pgScouring2ScouringDryCounterWrite.SelectedObject &&
                !(pgScouring2ScouringDryCounterWrite.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgScouring2ScouringDryCounterWrite.SelectedObject as FinishingCounter);
            Scouring2ScouringDryCounterModbusManager.Instance.Write(inst);
        }

        private void cmdScouring2ScouringDryCounterReset_Click(object sender, EventArgs e)
        {
            Scouring2ScouringDryCounterModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 20 - Test Coating 3

        private void cmdCoating3StartTest_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Coating3ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _coating3TestSlave && null != cfg.Slave)
                _coating3TestSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3StopTest_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _coating3TestSlave)
                _coating3TestSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdCoating3WriteTest_Click(object sender, EventArgs e)
        {
            if (null == pgCoating3WriteTest.SelectedObject &&
                !(pgCoating3WriteTest.SelectedObject is Coating3))
                return;
            Coating3 inst = (pgCoating3WriteTest.SelectedObject as Coating3);
            Coating3ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 21 - Scouring 2

        private void cmdScouring2StartTest_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = Scouring2ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouring2TestSlave && null != cfg.Slave)
                _scouring2TestSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2StopTest_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouring2TestSlave)
                _scouring2TestSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouring2WriteTest_Click(object sender, EventArgs e)
        {
            if (null == pgScouring2WriteTest.SelectedObject &&
                !(pgScouring2WriteTest.SelectedObject is Scouring2))
                return;
            Scouring2 inst = (pgScouring2WriteTest.SelectedObject as Scouring2);
            Scouring2ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 22 - ScouringCoat1

        private void cmdScouringCoat1Start_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringCoat1ModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringCoat1Slave && null != cfg.Slave)
                _scouringCoat1Slave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat1Stop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringCoat1Slave)
                _scouringCoat1Slave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat1Write_Click(object sender, EventArgs e)
        {
            if (null == pgScouringCoat1Write.SelectedObject &&
                !(pgScouringCoat1Write.SelectedObject is Coating1))
                return;
            ScouringCoat1 inst = (pgScouringCoat1Write.SelectedObject as ScouringCoat1);
            ScouringCoat1ModbusManager.Instance.Write(inst);
        }

        #endregion

        #region Tab 23 - Scouring Coat 1 Counter

        private void cmdScouringCoat1CounterStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringCoat1CounterModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringCounter1Slave && null != cfg.Slave)
                _scouringCoat1CounterSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat1CounterStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringCoat1CounterSlave)
                _scouringCoat1CounterSlave.Shutdown();
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringCoat1CounterWrite_Click(object sender, EventArgs e)
        {
            if (null == pgScouringCoat1CounterWrite.SelectedObject &&
                !(pgScouringCoat1CounterWrite.SelectedObject is FinishingCounter))
                return;
            FinishingCounter inst =
                (pgScouringCoat1CounterWrite.SelectedObject as FinishingCounter);
            ScouringCoat1CounterModbusManager.Instance.Write(inst);
        }

        private void cmdScouringCoat1CounterReset_Click(object sender, EventArgs e)
        {
            ScouringCoat1CounterModbusManager.Instance.Reset();
        }

        #endregion

        #region Tab 24 - Scouring Lab Modbus

        private void cmdScouringLabStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringLabModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _scouringLabSlave && null != cfg.Slave)
                _scouringLabSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringLabStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _scouringLabSlave)
                _scouringLabSlave.Shutdown();


            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdScouringLabWrite_Click(object sender, EventArgs e)
        {
            if (null == pgScouringLabWrite.SelectedObject &&
                !(pgScouringLabWrite.SelectedObject is ScouringLab))
                return;
            ScouringLab inst =
                (pgScouringLabWrite.SelectedObject as ScouringLab);
            ScouringLabModbusManager.Instance.Write(inst);
        }


        #endregion

        #region Tab 25 - Air Lab Modbus

        private void cmdAirLabStart_Click(object sender, EventArgs e)
        {
            ModbusTcpIpConfig cfg = ScouringLabModbusManager.Instance.Configs;
            // Start Slave Listener
            if (null != _airLabSlave && null != cfg.Slave)
                _airLabSlave.Start(cfg.Slave.IP, cfg.Slave.Port, cfg.Slave.SlaveId);
            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdAirLabStop_Click(object sender, EventArgs e)
        {
            // Shutdown Slave Listener
            if (null != _airLabSlave)
                _airLabSlave.Shutdown();


            // Update Slave GUI.
            ValidateSlaveControls();
        }

        private void cmdAirLabWrite_Click(object sender, EventArgs e)
        {
            if (null == pgAirLabWrite.SelectedObject &&
                !(pgAirLabWrite.SelectedObject is AirStaticLab))
                return;
            AirStaticLab inst =
                (pgAirLabWrite.SelectedObject as AirStaticLab);
            AirStaticLabModbusManager.Instance.Write(inst);
        }


        #endregion
    }
}
