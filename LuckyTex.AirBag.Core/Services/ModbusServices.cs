#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using NLib;
using NLib.Xml;
using NLib.Devices.Modbus;
using LuckyTex.Services;

#endregion

namespace LuckyTex.Services
{
    using LuckyTex.Models;

    #region InspectionWeightModbusManager
    
    /// <summary>
    /// Inspection Weight Modbus Manager.
    /// </summary>
    public class InspectionWeightModbusManager : ModbusManager<InspectionWeight>
    {
        #region Singelton

        private static InspectionWeightModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static InspectionWeightModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(InspectionWeightModbusManager))
                    {
                        _instance = new InspectionWeightModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private InspectionWeightModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "InspectionModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Inspection";
                // Slave
                this.Configs.Slave.IP = "172.20.10.20";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 569;
                this.Configs.Master.NoOfInputs = 30;
                ModbusValue[] values = new ModbusValue[]
                {
                    new ModbusValue() { Name = "W1", Address = 573, DataType = typeof(float), SwapFP = true },
                    new ModbusValue() { Name = "W2", Address = 593, DataType = typeof(float), SwapFP = true }
                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override InspectionWeight FormatReadInputs()
        {
            InspectionWeight result = new InspectionWeight();

            result.W1 = Convert.ToSingle(this["W1"]);
            result.W2 = Convert.ToSingle(this["W2"]);

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(InspectionWeight inst)
        {
            bool result = false;
            if (null == inst)
                return result;

            try
            {
                this["W1"] = inst.W1;
                this["W2"] = inst.W2;
                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Coating1ModbusManager
    
    /// <summary>
    /// Coating 1 Modbus Manager.
    /// </summary>
    public class Coating1ModbusManager : ModbusManager<Coating1>
    {
        #region Singelton

        private static Coating1ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Coating1ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Coating1ModbusManager))
                    {
                        _instance = new Coating1ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Coating1ModbusManager() : base() 
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Coating1ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Coating1";
                // Slave
                this.Configs.Slave.IP = "172.20.10.112";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    #region Old
                    //// TEMP 1-8  PV/SP
                    //new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP1SP", Address = 1, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2PV", Address = 10, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2SP", Address = 11, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3PV", Address = 20, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3SP", Address = 21, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4PV", Address = 30, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4SP", Address = 31, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5PV", Address = 40, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5SP", Address = 41, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6PV", Address = 50, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7PV", Address = 60, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7SP", Address = 61, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8PV", Address = 70, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    //// SAT PV/SP
                    //new ModbusValue() { Name = "SATPV", Address = 100, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SATSP", Address = 101, DataType = typeof(short), SwapFP = false },
                    //// WASH PV/SP
                    //new ModbusValue() { Name = "WASH1PV", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH1SP", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2PV", Address = 120, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2SP", Address = 121, DataType = typeof(short), SwapFP = false },
                    //// HOT PV/SP
                    //new ModbusValue() { Name = "HOTPV", Address = 130, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "HOTSP", Address = 131, DataType = typeof(short), SwapFP = false },
                    //// SPEED
                    //new ModbusValue() { Name = "SPEED", Address = 209, DataType = typeof(float), SwapFP = false },
                    //// TEN UP/DOWN
                    //new ModbusValue() { Name = "TENUP", Address = 229, DataType = typeof(float), SwapFP = false },
                    //new ModbusValue() { Name = "TENDOWN", Address = 249, DataType = typeof(float), SwapFP = false },
                    //// SENSOR
                    //new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                    #endregion

                     // New
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Coating1 FormatReadInputs()
        {
            Coating1 result = new Coating1();

            #region TEMP
            
            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Coating1 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Coating2ModbusManager

    /// <summary>
    /// Coating 2 Modbus Manager.
    /// </summary>
    public class Coating2ModbusManager : ModbusManager<Coating2>
    {
        #region Singelton

        private static Coating2ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Coating2ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Coating2ModbusManager))
                    {
                        _instance = new Coating2ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Coating2ModbusManager() : base() 
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Coating2ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Coating2";
                // Slave
                this.Configs.Slave.IP = "172.20.10.113";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    #region Old

                    //// TEMP 1-10  PV/SP
                    //new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP1SP", Address = 1, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2PV", Address = 10, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2SP", Address = 11, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3PV", Address = 20, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3SP", Address = 21, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4PV", Address = 30, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4SP", Address = 31, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5PV", Address = 40, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5SP", Address = 41, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6PV", Address = 50, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7PV", Address = 60, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7SP", Address = 61, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8PV", Address = 70, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP9PV", Address = 100, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP9SP", Address = 101, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP10PV", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP10SP", Address = 111, DataType = typeof(short), SwapFP = false },                    
                    //// SAT PV/SP
                    //new ModbusValue() { Name = "SATPV", Address = 150, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SATSP", Address = 151, DataType = typeof(short), SwapFP = false },
                    //// WASH PV/SP
                    //new ModbusValue() { Name = "WASH1PV", Address = 140, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH1SP", Address = 141, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2PV", Address = 130, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2SP", Address = 131, DataType = typeof(short), SwapFP = false },
                    //// HOT PV/SP
                    //new ModbusValue() { Name = "HOTPV", Address = 120, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "HOTSP", Address = 121, DataType = typeof(short), SwapFP = false },
                    //// SPEED
                    //new ModbusValue() { Name = "SPEED", Address = 209, DataType = typeof(float), SwapFP = false },
                    //// TEN UP/DOWN
                    //new ModbusValue() { Name = "TENUP", Address = 249, DataType = typeof(float), SwapFP = false },
                    //new ModbusValue() { Name = "TENDOWN", Address = 229, DataType = typeof(float), SwapFP = false },
                    //// SENSOR
                    //new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                    #endregion

                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9PV", Address = 8, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10PV", Address = 9, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9SP", Address = 58, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10SP", Address = 59, DataType = typeof(short), SwapFP = false }, 
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }
                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Coating2 FormatReadInputs()
        {
            Coating2 result = new Coating2();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);
            result.TEMP9PV = Convert.ToInt16(this["TEMP9PV"]);
            result.TEMP9SP = Convert.ToInt16(this["TEMP9SP"]);
            result.TEMP10PV = Convert.ToInt16(this["TEMP10PV"]);
            result.TEMP10SP = Convert.ToInt16(this["TEMP10SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Coating2 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;
                this["TEMP9PV"] = inst.TEMP9PV;
                this["TEMP9SP"] = inst.TEMP9SP;
                this["TEMP10PV"] = inst.TEMP10PV;
                this["TEMP10SP"] = inst.TEMP10SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Coating3ModbusManager

    /// <summary>
    /// Coating 3 Modbus Manager.
    /// </summary>
    public class Coating3ModbusManager : ModbusManager<Coating3>
    {
        #region Singelton

        private static Coating3ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Coating3ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Coating3ModbusManager))
                    {
                        _instance = new Coating3ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Coating3ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Coating3ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Coating3";
                // Slave
                this.Configs.Slave.IP = "172.20.10.113";
                this.Configs.Slave.Port = 507;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 507;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    #region Old

                    //// TEMP 1-10  PV/SP
                    //new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP1SP", Address = 1, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2PV", Address = 10, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2SP", Address = 11, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3PV", Address = 20, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3SP", Address = 21, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4PV", Address = 30, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4SP", Address = 31, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5PV", Address = 40, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5SP", Address = 41, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6PV", Address = 50, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7PV", Address = 60, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7SP", Address = 61, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8PV", Address = 70, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP9PV", Address = 100, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP9SP", Address = 101, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP10PV", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP10SP", Address = 111, DataType = typeof(short), SwapFP = false },                    
                    //// SAT PV/SP
                    //new ModbusValue() { Name = "SATPV", Address = 150, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SATSP", Address = 151, DataType = typeof(short), SwapFP = false },
                    //// WASH PV/SP
                    //new ModbusValue() { Name = "WASH1PV", Address = 140, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH1SP", Address = 141, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2PV", Address = 130, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2SP", Address = 131, DataType = typeof(short), SwapFP = false },
                    //// HOT PV/SP
                    //new ModbusValue() { Name = "HOTPV", Address = 120, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "HOTSP", Address = 121, DataType = typeof(short), SwapFP = false },
                    //// SPEED
                    //new ModbusValue() { Name = "SPEED", Address = 209, DataType = typeof(float), SwapFP = false },
                    //// TEN UP/DOWN
                    //new ModbusValue() { Name = "TENUP", Address = 249, DataType = typeof(float), SwapFP = false },
                    //new ModbusValue() { Name = "TENDOWN", Address = 229, DataType = typeof(float), SwapFP = false },
                    //// SENSOR
                    //new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                    #endregion

                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9PV", Address = 8, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10PV", Address = 9, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9SP", Address = 58, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10SP", Address = 59, DataType = typeof(short), SwapFP = false }, 
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Coating3 FormatReadInputs()
        {
            Coating3 result = new Coating3();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);
            result.TEMP9PV = Convert.ToInt16(this["TEMP9PV"]);
            result.TEMP9SP = Convert.ToInt16(this["TEMP9SP"]);
            result.TEMP10PV = Convert.ToInt16(this["TEMP10PV"]);
            result.TEMP10SP = Convert.ToInt16(this["TEMP10SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }

        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Coating3 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;
                this["TEMP9PV"] = inst.TEMP9PV;
                this["TEMP9SP"] = inst.TEMP9SP;
                this["TEMP10PV"] = inst.TEMP10PV;
                this["TEMP10SP"] = inst.TEMP10SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Coating3ScouringModbusManager

    /// <summary>
    /// Coating 3 Scouring Modbus Manager.
    /// </summary>
    public class Coating3ScouringModbusManager : ModbusManager<Coating3Scouring>
    {
        #region Singelton

        private static Coating3ScouringModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Coating3ScouringModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Coating3ScouringModbusManager))
                    {
                        _instance = new Coating3ScouringModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Coating3ScouringModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Coating3ScouringModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Coating3Scouring";
                // Slave
                this.Configs.Slave.IP = "172.20.10.113";
                this.Configs.Slave.Port = 507;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 507;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9PV", Address = 8, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10PV", Address = 9, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9SP", Address = 58, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10SP", Address = 59, DataType = typeof(short), SwapFP = false }, 
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }
                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Coating3Scouring FormatReadInputs()
        {
            Coating3Scouring result = new Coating3Scouring();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);
            result.TEMP9PV = Convert.ToInt16(this["TEMP9PV"]);
            result.TEMP9SP = Convert.ToInt16(this["TEMP9SP"]);
            result.TEMP10PV = Convert.ToInt16(this["TEMP10PV"]);
            result.TEMP10SP = Convert.ToInt16(this["TEMP10SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Coating3Scouring inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;
                this["TEMP9PV"] = inst.TEMP9PV;
                this["TEMP9SP"] = inst.TEMP9SP;
                this["TEMP10PV"] = inst.TEMP10PV;
                this["TEMP10SP"] = inst.TEMP10SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Scouring1ModbusManager
    
    /// <summary>
    /// Scouring 1 Modbus Manager.
    /// </summary>
    public class Scouring1ModbusManager : ModbusManager<Scouring1>
    {
        #region Singelton

        private static Scouring1ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Scouring1ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Scouring1ModbusManager))
                    {
                        _instance = new Scouring1ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Scouring1ModbusManager() : base() 
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Scouring1ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Scouring1";
                // Slave
                this.Configs.Slave.IP = "172.20.10.111";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    #region Old

                    // TEMP 1-6  PV/SP
                    //new ModbusValue() { Name = "TEMP1PV", Address = 10, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP1SP", Address = 11, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2PV", Address = 20, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2SP", Address = 21, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3PV", Address = 30, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3SP", Address = 31, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4PV", Address = 40, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4SP", Address = 41, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5PV", Address = 50, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6PV", Address = 60, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6SP", Address = 61, DataType = typeof(short), SwapFP = false },

                    // SAT PV/SP
                    //new ModbusValue() { Name = "SATPV", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SATSP", Address = 111, DataType = typeof(short), SwapFP = false },
                    // WASH PV/SP
                    //new ModbusValue() { Name = "WASH1PV", Address = 100, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH1SP", Address = 101, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2PV", Address = 70, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    // HOT PV/SP
                    //new ModbusValue() { Name = "HOTPV", Address = 0, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "HOTSP", Address = 1, DataType = typeof(short), SwapFP = false },
                    // SPEED
                    //new ModbusValue() { Name = "SPEED", Address = 209, DataType = typeof(float), SwapFP = false },
                    // SENSOR
                    //new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                    #endregion

                    // New
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },

                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Scouring1 FormatReadInputs()
        {
            Scouring1 result = new Scouring1();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Scouring1 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Scouring2ModbusManager
    
    /// <summary>
    /// Scouring 2 Modbus Manager.
    /// </summary>
    public class Scouring2ModbusManager : ModbusManager<Scouring2>
    {
        #region Singelton

        private static Scouring2ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Scouring2ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Scouring2ModbusManager))
                    {
                        _instance = new Scouring2ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Scouring2ModbusManager() : base() 
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Scouring2ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Scouring2";
                // Slave
                this.Configs.Slave.IP = "172.20.10.114";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    #region Old

                    //// TEMP 1-8  PV/SP
                    //new ModbusValue() { Name = "TEMP1PV", Address = 140, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP1SP", Address = 141, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2PV", Address = 10, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2SP", Address = 11, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3PV", Address = 20, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3SP", Address = 21, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4PV", Address = 30, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4SP", Address = 31, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5PV", Address = 40, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5SP", Address = 41, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6PV", Address = 50, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7PV", Address = 60, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7SP", Address = 61, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8PV", Address = 70, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    //// SAT PV/SP
                    //new ModbusValue() { Name = "SATPV", Address = 100, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SATSP", Address = 101, DataType = typeof(short), SwapFP = false },
                    //// WASH PV/SP
                    //new ModbusValue() { Name = "WASH1PV", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH1SP", Address = 111, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2PV", Address = 120, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2SP", Address = 121, DataType = typeof(short), SwapFP = false },
                    //// HOT PV/SP
                    //new ModbusValue() { Name = "HOTPV", Address = 130, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "HOTSP", Address = 131, DataType = typeof(short), SwapFP = false },
                    //// SPEED 1-2
                    //new ModbusValue() { Name = "SPEED1", Address = 209, DataType = typeof(float), SwapFP = false },
                    //new ModbusValue() { Name = "SPEED2", Address = 220, DataType = typeof(float), SwapFP = false },
                    //// SENSOR
                    //new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                    #endregion

                     // New
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED1", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "SPEED2", Address = 220, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },

                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Scouring2 FormatReadInputs()
        {
            Scouring2 result = new Scouring2();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED1 = Convert.ToSingle(this["SPEED1"]);
            result.SPEED2 = Convert.ToSingle(this["SPEED2"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Scouring2 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED1"] = inst.SPEED1;
                this["SPEED2"] = inst.SPEED2;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region FinishingCounterModbusManager (abstract)

    /// <summary>
    /// FinishingCounter Modbus Manager (abstract).
    /// </summary>
    public abstract class FinishingCounterModbusManager : ModbusManager<FinishingCounter>
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingCounterModbusManager() : base() { }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets Default Addresses.
        /// </summary>
        /// <returns></returns>
        protected ModbusValue[] GetDefaultAddresses()
        {
            ModbusValue[] values = new ModbusValue[]
            {
                // Overall
                new ModbusValue() { Name = "ALL_BATCH_COUNTER", Address = 199, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "ALL_TOTAL_COUNTER", Address = 201, DataType = typeof(float), SwapFP = false },
                // Flags
                new ModbusValue() { Name = "BATCH_FLAG", Address = 204, DataType = typeof(short), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_FLAG", Address = 205, DataType = typeof(short), SwapFP = false },
                // Batch Count.
                new ModbusValue() { Name = "BATCH_COUNT", Address = 206, DataType = typeof(short), SwapFP = false },
                // Real Time
                new ModbusValue() { Name = "RT_BATCH_COUNTER", Address = 207, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "RT_TOTAL_COUNTER", Address = 215, DataType = typeof(float), SwapFP = false },
                // Batch Counter 1 - 7
                new ModbusValue() { Name = "BATCH_COUNTER1", Address = 239, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "BATCH_COUNTER2", Address = 241, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "BATCH_COUNTER3", Address = 243, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "BATCH_COUNTER4", Address = 245, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "BATCH_COUNTER5", Address = 247, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "BATCH_COUNTER6", Address = 249, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "BATCH_COUNTER7", Address = 251, DataType = typeof(float), SwapFP = false },
                // Total Counter 1 - 7
                new ModbusValue() { Name = "TOTAL_COUNTER1", Address = 259, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_COUNTER2", Address = 261, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_COUNTER3", Address = 263, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_COUNTER4", Address = 265, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_COUNTER5", Address = 267, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_COUNTER6", Address = 269, DataType = typeof(float), SwapFP = false },
                new ModbusValue() { Name = "TOTAL_COUNTER7", Address = 271, DataType = typeof(float), SwapFP = false }
            };

            return values;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override FinishingCounter FormatReadInputs()
        {
            FinishingCounter result = new FinishingCounter();

            #region Overall

            result.OverallBatchCounter = Convert.ToSingle(this["ALL_BATCH_COUNTER"]);
            result.OverallTotalCounter = Convert.ToSingle(this["ALL_TOTAL_COUNTER"]);
            result.OverallBatchCount = Convert.ToInt16(this["BATCH_COUNT"]);

            #endregion

            #region Flags

            result.BatchFlag = Convert.ToInt16(this["BATCH_FLAG"]);
            result.TotalFlag = Convert.ToInt16(this["TOTAL_FLAG"]);

            #endregion

            #region RealTime

            result.RealTimeBatchCounter = Convert.ToSingle(this["RT_BATCH_COUNTER"]);
            result.RealTimeTotalCounter = Convert.ToSingle(this["RT_TOTAL_COUNTER"]);

            #endregion

            #region Batch Counter

            result.BatchCounter1 = Convert.ToSingle(this["BATCH_COUNTER1"]);
            result.BatchCounter2 = Convert.ToSingle(this["BATCH_COUNTER2"]);
            result.BatchCounter3 = Convert.ToSingle(this["BATCH_COUNTER3"]);
            result.BatchCounter4 = Convert.ToSingle(this["BATCH_COUNTER4"]);
            result.BatchCounter5 = Convert.ToSingle(this["BATCH_COUNTER5"]);
            result.BatchCounter6 = Convert.ToSingle(this["BATCH_COUNTER6"]);
            result.BatchCounter7 = Convert.ToSingle(this["BATCH_COUNTER7"]);

            #endregion

            #region Total Counter

            result.TotalCounter1 = Convert.ToSingle(this["TOTAL_COUNTER1"]);
            result.TotalCounter2 = Convert.ToSingle(this["TOTAL_COUNTER2"]);
            result.TotalCounter3 = Convert.ToSingle(this["TOTAL_COUNTER3"]);
            result.TotalCounter4 = Convert.ToSingle(this["TOTAL_COUNTER4"]);
            result.TotalCounter5 = Convert.ToSingle(this["TOTAL_COUNTER5"]);
            result.TotalCounter6 = Convert.ToSingle(this["TOTAL_COUNTER6"]);
            result.TotalCounter7 = Convert.ToSingle(this["TOTAL_COUNTER7"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(FinishingCounter inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region Overall

                this["ALL_BATCH_COUNTER"] = inst.OverallBatchCounter;
                this["ALL_TOTAL_COUNTER"] = inst.OverallTotalCounter;
                this["BATCH_COUNT"] = inst.OverallBatchCount;

                #endregion

                #region Flags

                this["BATCH_FLAG"] = inst.BatchFlag;
                this["TOTAL_FLAG"] = inst.TotalFlag;

                #endregion

                #region RealTime

                this["RT_BATCH_COUNTER"] = inst.RealTimeBatchCounter;
                this["RT_TOTAL_COUNTER"] = inst.RealTimeTotalCounter;

                #endregion

                #region Batch Counter

                this["BATCH_COUNTER1"] = inst.BatchCounter1;
                this["BATCH_COUNTER2"] = inst.BatchCounter2;
                this["BATCH_COUNTER3"] = inst.BatchCounter3;
                this["BATCH_COUNTER4"] = inst.BatchCounter4;
                this["BATCH_COUNTER5"] = inst.BatchCounter5;
                this["BATCH_COUNTER6"] = inst.BatchCounter6;
                this["BATCH_COUNTER7"] = inst.BatchCounter7;

                #endregion

                #region Total Counter

                this["TOTAL_COUNTER1"] = inst.TotalCounter1;
                this["TOTAL_COUNTER2"] = inst.TotalCounter2;
                this["TOTAL_COUNTER3"] = inst.TotalCounter3;
                this["TOTAL_COUNTER4"] = inst.TotalCounter4;
                this["TOTAL_COUNTER5"] = inst.TotalCounter5;
                this["TOTAL_COUNTER6"] = inst.TotalCounter6;
                this["TOTAL_COUNTER7"] = inst.TotalCounter7;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reset Flags
        /// </summary>
        /// <returns></returns>
        public bool Reset()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                // BATCH_FLAG - Address : 204
                master.WriteHoldingRegisters(204, 0);
                // TOTAL_FLAG- Address : 205
                master.WriteHoldingRegisters(205, 0);

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region ScouringCounter1ModbusManager

    /// <summary>
    /// Scouring Counter 1 Modbus Manager.
    /// </summary>
    public class ScouringCounter1ModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static ScouringCounter1ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringCounter1ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringCounter1ModbusManager))
                    {
                        _instance = new ScouringCounter1ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringCounter1ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringCounter1ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringCounter1";
                // Slave
                this.Configs.Slave.IP = "172.20.18.130";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    #region ScouringCounter2ModbusManager

    /// <summary>
    /// Scouring Counter 2 Modbus Manager.
    /// </summary>
    public class ScouringCounter2ModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static ScouringCounter2ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringCounter2ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringCounter2ModbusManager))
                    {
                        _instance = new ScouringCounter2ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringCounter2ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringCounter2ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringCounter2";
                // Slave
                this.Configs.Slave.IP = "172.20.18.136";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    #region CoatingCounter1ModbusManager

    /// <summary>
    /// Coating Counter 1 Modbus Manager.
    /// </summary>
    public class CoatingCounter1ModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static CoatingCounter1ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static CoatingCounter1ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CoatingCounter1ModbusManager))
                    {
                        _instance = new CoatingCounter1ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private CoatingCounter1ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "CoatingCounter1ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "CoatingCounter1";
                // Slave
                this.Configs.Slave.IP = "172.20.18.132";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    #region CoatingCounter2ModbusManager

    /// <summary>
    /// Coating Counter 2 Modbus Manager.
    /// </summary>
    public class CoatingCounter2ModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static CoatingCounter2ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static CoatingCounter2ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CoatingCounter2ModbusManager))
                    {
                        _instance = new CoatingCounter2ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private CoatingCounter2ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "CoatingCounter2ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "CoatingCounter2";
                // Slave
                this.Configs.Slave.IP = "172.20.18.134";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    #region CoatingCounter3ModbusManager

    /// <summary>
    /// Coating Counter 3 Modbus Manager.
    /// </summary>
    public class CoatingCounter3ModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static CoatingCounter3ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static CoatingCounter3ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CoatingCounter3ModbusManager))
                    {
                        _instance = new CoatingCounter3ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private CoatingCounter3ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "CoatingCounter3ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "CoatingCounter3";
                // Slave
                this.Configs.Slave.IP = "172.20.18.134";
                this.Configs.Slave.Port = 507;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 507;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    #region Coating3ScouringCounterModbusManager

    /// <summary>
    /// Coating Counter 3 Scouring Modbus Manager.
    /// </summary>
    public class Coating3ScouringCounterModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static Coating3ScouringCounterModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Coating3ScouringCounterModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Coating3ScouringCounterModbusManager))
                    {
                        _instance = new Coating3ScouringCounterModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Coating3ScouringCounterModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Coating3ScouringCounterModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Coating3ScouringCounter";
                // Slave
                this.Configs.Slave.IP = "172.20.18.134";
                this.Configs.Slave.Port = 507;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 507;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    #region ScouringDryerCounterModbusManager

    /// <summary>
    /// Scouring Dryer Counter Modbus Manager.
    /// </summary>
    public class ScouringDryerCounterModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static ScouringDryerCounterModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringDryerCounterModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringDryerCounterModbusManager))
                    {
                        _instance = new ScouringDryerCounterModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringDryerCounterModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringDryerCounterModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringDryerCounter";
                // Slave
                this.Configs.Slave.IP = "172.20.18.138";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    //เพิ่มใหม่ Scouring Coat 2 05/01/17
    #region ScouringCoat2ModbusManager

    /// <summary>
    /// ScouringCoat2 Modbus Manager.
    /// </summary>
    public class ScouringCoat2ModbusManager : ModbusManager<ScouringCoat2>
    {
        #region Singelton

        private static ScouringCoat2ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringCoat2ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringCoat2ModbusManager))
                    {
                        _instance = new ScouringCoat2ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringCoat2ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringCoat2ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringCoat2";
                // Slave
                this.Configs.Slave.IP = "172.20.10.113";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9PV", Address = 8, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10PV", Address = 9, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP9SP", Address = 58, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP10SP", Address = 59, DataType = typeof(short), SwapFP = false }, 
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }
                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override ScouringCoat2 FormatReadInputs()
        {
            ScouringCoat2 result = new ScouringCoat2();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);
            result.TEMP9PV = Convert.ToInt16(this["TEMP9PV"]);
            result.TEMP9SP = Convert.ToInt16(this["TEMP9SP"]);
            result.TEMP10PV = Convert.ToInt16(this["TEMP10PV"]);
            result.TEMP10SP = Convert.ToInt16(this["TEMP10SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(ScouringCoat2 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;
                this["TEMP9PV"] = inst.TEMP9PV;
                this["TEMP9SP"] = inst.TEMP9SP;
                this["TEMP10PV"] = inst.TEMP10PV;
                this["TEMP10SP"] = inst.TEMP10SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region ScouringCoat2CounterModbusManager

    /// <summary>
    /// Scouring Coat 2 Counter Modbus Manager.
    /// </summary>
    public class ScouringCoat2CounterModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static ScouringCoat2CounterModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringCoat2CounterModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringCoat2CounterModbusManager))
                    {
                        _instance = new ScouringCoat2CounterModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringCoat2CounterModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringCoat2CounterModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringCoat2Counter";
                // Slave
                this.Configs.Slave.IP = "172.20.18.136";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    //เพิ่ม 12/05/17
    #region Coating1ScouringModbusManager

    /// <summary>
    /// Coating 1 Scouring Modbus Manager.
    /// </summary>
    public class Coating1ScouringModbusManager : ModbusManager<Coating1Scouring>
    {
        #region Singelton

        private static Coating1ScouringModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Coating1ScouringModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Coating1ScouringModbusManager))
                    {
                        _instance = new Coating1ScouringModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Coating1ScouringModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Coating1ScouringModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Coating1Scouring";
                // Slave
                this.Configs.Slave.IP = "172.20.10.112";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                     // New
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Coating1Scouring FormatReadInputs()
        {
            Coating1Scouring result = new Coating1Scouring();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Coating1Scouring inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region CoatingCounter1ScouringModbusManager

    /// <summary>
    /// Coating Counter 1 Scouring Modbus Manager.
    /// </summary>
    public class CoatingCounter1ScouringModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static CoatingCounter1ScouringModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static CoatingCounter1ScouringModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(CoatingCounter1ScouringModbusManager))
                    {
                        _instance = new CoatingCounter1ScouringModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private CoatingCounter1ScouringModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "CoatingCounter1ScouringModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "CoatingCounter1Scouring";
                // Slave
                this.Configs.Slave.IP = "172.20.18.132";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 26/05/17
    #region Scouring2ScouringDryModbusManager

    /// <summary>
    /// Scouring2ScouringDry Modbus Manager.
    /// </summary>
    public class Scouring2ScouringDryModbusManager : ModbusManager<Scouring2ScouringDry>
    {
        #region Singelton

        private static Scouring2ScouringDryModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Scouring2ScouringDryModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Scouring2ScouringDryModbusManager))
                    {
                        _instance = new Scouring2ScouringDryModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Scouring2ScouringDryModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Scouring2ScouringDryModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Scouring2ScouringDry";
                // Slave
                this.Configs.Slave.IP = "172.20.10.114";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                     // New
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED1", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "SPEED2", Address = 220, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },

                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override Scouring2ScouringDry FormatReadInputs()
        {
            Scouring2ScouringDry result = new Scouring2ScouringDry();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED1 = Convert.ToSingle(this["SPEED1"]);
            result.SPEED2 = Convert.ToSingle(this["SPEED2"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(Scouring2ScouringDry inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED1"] = inst.SPEED1;
                this["SPEED2"] = inst.SPEED2;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region Scouring2ScouringDryCounterModbusManager

    /// <summary>
    /// Scouring2ScouringDry Counter Modbus Manager.
    /// </summary>
    public class Scouring2ScouringDryCounterModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static Scouring2ScouringDryCounterModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static Scouring2ScouringDryCounterModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(Scouring2ScouringDryCounterModbusManager))
                    {
                        _instance = new Scouring2ScouringDryCounterModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private Scouring2ScouringDryCounterModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "Scouring2ScouringDryCounterModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "Scouring2ScouringDryCounter";
                // Slave
                this.Configs.Slave.IP = "172.20.18.136";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 12/09/18
    #region ScouringCoat1ModbusManager

    /// <summary>
    /// Scouring Coat 1 Modbus Manager.
    /// </summary>
    public class ScouringCoat1ModbusManager : ModbusManager<ScouringCoat1>
    {
        #region Singelton

        private static ScouringCoat1ModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringCoat1ModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringCoat1ModbusManager))
                    {
                        _instance = new ScouringCoat1ModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringCoat1ModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringCoat1ModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringCoat1";
                // Slave
                this.Configs.Slave.IP = "172.20.10.112";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    #region Old
                    //// TEMP 1-8  PV/SP
                    //new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP1SP", Address = 1, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2PV", Address = 10, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP2SP", Address = 11, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3PV", Address = 20, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP3SP", Address = 21, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4PV", Address = 30, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP4SP", Address = 31, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5PV", Address = 40, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP5SP", Address = 41, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6PV", Address = 50, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP6SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7PV", Address = 60, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP7SP", Address = 61, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8PV", Address = 70, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "TEMP8SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    //// SAT PV/SP
                    //new ModbusValue() { Name = "SATPV", Address = 100, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SATSP", Address = 101, DataType = typeof(short), SwapFP = false },
                    //// WASH PV/SP
                    //new ModbusValue() { Name = "WASH1PV", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH1SP", Address = 110, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2PV", Address = 120, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "WASH2SP", Address = 121, DataType = typeof(short), SwapFP = false },
                    //// HOT PV/SP
                    //new ModbusValue() { Name = "HOTPV", Address = 130, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "HOTSP", Address = 131, DataType = typeof(short), SwapFP = false },
                    //// SPEED
                    //new ModbusValue() { Name = "SPEED", Address = 209, DataType = typeof(float), SwapFP = false },
                    //// TEN UP/DOWN
                    //new ModbusValue() { Name = "TENUP", Address = 229, DataType = typeof(float), SwapFP = false },
                    //new ModbusValue() { Name = "TENDOWN", Address = 249, DataType = typeof(float), SwapFP = false },
                    //// SENSOR
                    //new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    //new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                    #endregion

                     // New
                    new ModbusValue() { Name = "TEMP1PV", Address = 0, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2PV", Address = 1, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3PV", Address = 2, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4PV", Address = 3, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5PV", Address = 4, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6PV", Address = 5, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7PV", Address = 6, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8PV", Address = 7, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATPV", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1PV", Address = 21, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2PV", Address = 22, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTPV", Address = 23, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SPEED", Address = 29, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENUP", Address = 31, DataType = typeof(float), SwapFP = false },
                    new ModbusValue() { Name = "TENDOWN", Address = 33, DataType = typeof(float), SwapFP = false },
                    
                    new ModbusValue() { Name = "TEMP1SP", Address = 50, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP2SP", Address = 51, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP3SP", Address = 52, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP4SP", Address = 53, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP5SP", Address = 54, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP6SP", Address = 55, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP7SP", Address = 56, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "TEMP8SP", Address = 57, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SATSP", Address = 70, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH1SP", Address = 71, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "WASH2SP", Address = 72, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HOTSP", Address = 73, DataType = typeof(short), SwapFP = false },
 
                    new ModbusValue() { Name = "SEN1", Address = 500, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "SEN2", Address = 510, DataType = typeof(short), SwapFP = false }

                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override ScouringCoat1 FormatReadInputs()
        {
            ScouringCoat1 result = new ScouringCoat1();

            #region TEMP

            result.TEMP1PV = Convert.ToInt16(this["TEMP1PV"]);
            result.TEMP1SP = Convert.ToInt16(this["TEMP1SP"]);
            result.TEMP2PV = Convert.ToInt16(this["TEMP2PV"]);
            result.TEMP2SP = Convert.ToInt16(this["TEMP2SP"]);
            result.TEMP3PV = Convert.ToInt16(this["TEMP3PV"]);
            result.TEMP3SP = Convert.ToInt16(this["TEMP3SP"]);
            result.TEMP4PV = Convert.ToInt16(this["TEMP4PV"]);
            result.TEMP4SP = Convert.ToInt16(this["TEMP4SP"]);
            result.TEMP5PV = Convert.ToInt16(this["TEMP5PV"]);
            result.TEMP5SP = Convert.ToInt16(this["TEMP5SP"]);
            result.TEMP6PV = Convert.ToInt16(this["TEMP6PV"]);
            result.TEMP6SP = Convert.ToInt16(this["TEMP6SP"]);
            result.TEMP7PV = Convert.ToInt16(this["TEMP7PV"]);
            result.TEMP7SP = Convert.ToInt16(this["TEMP7SP"]);
            result.TEMP8PV = Convert.ToInt16(this["TEMP8PV"]);
            result.TEMP8SP = Convert.ToInt16(this["TEMP8SP"]);

            #endregion

            #region SAT

            result.SATPV = Convert.ToInt16(this["SATPV"]);
            result.SATSP = Convert.ToInt16(this["SATSP"]);

            #endregion

            #region WASH

            result.WASH1PV = Convert.ToInt16(this["WASH1PV"]);
            result.WASH1SP = Convert.ToInt16(this["WASH1SP"]);
            result.WASH2PV = Convert.ToInt16(this["WASH2PV"]);
            result.WASH2SP = Convert.ToInt16(this["WASH2SP"]);

            #endregion

            #region HOT

            result.HOTPV = Convert.ToInt16(this["HOTPV"]);
            result.HOTSP = Convert.ToInt16(this["HOTSP"]);

            #endregion

            #region SPEED

            result.SPEED = Convert.ToSingle(this["SPEED"]);

            #endregion

            #region TEN

            result.TENUP = Convert.ToSingle(this["TENUP"]);
            result.TENDOWN = Convert.ToSingle(this["TENDOWN"]);

            #endregion

            #region SENSOR

            result.SEN1 = Convert.ToInt16(this["SEN1"]);
            result.SEN2 = Convert.ToInt16(this["SEN2"]);

            #endregion

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(ScouringCoat1 inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                #region TEMP

                this["TEMP1PV"] = inst.TEMP1PV;
                this["TEMP1SP"] = inst.TEMP1SP;
                this["TEMP2PV"] = inst.TEMP2PV;
                this["TEMP2SP"] = inst.TEMP2SP;
                this["TEMP3PV"] = inst.TEMP3PV;
                this["TEMP3SP"] = inst.TEMP3SP;
                this["TEMP4PV"] = inst.TEMP4PV;
                this["TEMP4SP"] = inst.TEMP4SP;
                this["TEMP5PV"] = inst.TEMP5PV;
                this["TEMP5SP"] = inst.TEMP5SP;
                this["TEMP6PV"] = inst.TEMP6PV;
                this["TEMP6SP"] = inst.TEMP6SP;
                this["TEMP7PV"] = inst.TEMP7PV;
                this["TEMP7SP"] = inst.TEMP7SP;
                this["TEMP8PV"] = inst.TEMP8PV;
                this["TEMP8SP"] = inst.TEMP8SP;

                #endregion

                #region SAT

                this["SATPV"] = inst.SATPV;
                this["SATSP"] = inst.SATSP;

                #endregion

                #region WASH

                this["WASH1PV"] = inst.WASH1PV;
                this["WASH1SP"] = inst.WASH1SP;
                this["WASH2PV"] = inst.WASH2PV;
                this["WASH2SP"] = inst.WASH2SP;

                #endregion

                #region HOT

                this["HOTPV"] = inst.HOTPV;
                this["HOTSP"] = inst.HOTSP;

                #endregion

                #region SPEED

                this["SPEED"] = inst.SPEED;

                #endregion

                #region TEN

                this["TENUP"] = inst.TENUP;
                this["TENDOWN"] = inst.TENDOWN;

                #endregion

                #region SENSOR

                this["SEN1"] = inst.SEN1;
                this["SEN2"] = inst.SEN2;

                #endregion

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion
    }

    #endregion

    #region ScouringCoat1CounterModbusManager

    /// <summary>
    /// ScouringCoat1 Counter Modbus Manager.
    /// </summary>
    public class ScouringCoat1CounterModbusManager : FinishingCounterModbusManager
    {
        #region Singelton

        private static ScouringCoat1CounterModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringCoat1CounterModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringCoat1CounterModbusManager))
                    {
                        _instance = new ScouringCoat1CounterModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringCoat1CounterModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringCoat1CounterModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringCoat1Counter";
                // Slave
                this.Configs.Slave.IP = "172.20.18.132";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 199;
                this.Configs.Master.NoOfInputs = 100;
                ModbusValue[] values = GetDefaultAddresses();
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 10/07/18
    #region ScouringLabModbusManager

    /// <summary>
    /// ScouringLabModbusManager Modbus Manager.
    /// </summary>
    public class ScouringLabModbusManager : ModbusManager<ScouringLab>
    {
        #region Singelton

        private static ScouringLabModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static ScouringLabModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ScouringLabModbusManager))
                    {
                        _instance = new ScouringLabModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ScouringLabModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "ScouringLabModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "ScouringLab";
                // Slave
                this.Configs.Slave.IP = "172.20.78.113";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                     // New
                    new ModbusValue() { Name = "WEIGHT", Address = 9, DataType = typeof(float), SwapFP = true },
                    new ModbusValue() { Name = "HIGHPRESS", Address = 11, DataType = typeof(float), SwapFP = true },
                    new ModbusValue() { Name = "AIR", Address = 13, DataType = typeof(float), SwapFP = true },

                    //เพิ่ม 25/11/18
                    new ModbusValue() { Name = "DYNAMIC", Address = 15, DataType = typeof(float), SwapFP = true },
                    new ModbusValue() { Name = "EXPONENT", Address = 17, DataType = typeof(float), SwapFP = true },

                    //test
                    new ModbusValue() { Name = "STATIC", Address = 18, DataType = typeof(float), SwapFP = true },


                    new ModbusValue() { Name = "WEIGHTFLAG", Address = 19, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "HIGHPRESSFLAG", Address = 20, DataType = typeof(short), SwapFP = false },
                    new ModbusValue() { Name = "AIRFLAG", Address = 21, DataType = typeof(short), SwapFP = false },

                    //เพิ่ม 25/11/18
                    new ModbusValue() { Name = "DYNAMICFLAG", Address = 22, DataType = typeof(short), SwapFP = false },

                    //test
                    new ModbusValue() { Name = "STATICFLAG", Address = 23, DataType = typeof(short), SwapFP = false },
                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override ScouringLab FormatReadInputs()
        {
            ScouringLab result = new ScouringLab();

            result.WEIGHT = Convert.ToSingle(this["WEIGHT"]);
            result.HIGHPRESS = Convert.ToSingle(this["HIGHPRESS"]);
            result.AIR = Convert.ToSingle(this["AIR"]);

            //เพิ่ม 25/11/18
            result.DYNAMIC = Convert.ToSingle(this["DYNAMIC"]);
            result.EXPONENT = Convert.ToSingle(this["EXPONENT"]);

            result.WEIGHTFLAG = Convert.ToInt16(this["WEIGHTFLAG"]);
            result.HIGHPRESSFLAG = Convert.ToInt16(this["HIGHPRESSFLAG"]);
            result.AIRFLAG = Convert.ToInt16(this["AIRFLAG"]);

            //เพิ่ม 25/11/18
            result.DYNAMICFLAG = Convert.ToInt16(this["DYNAMICFLAG"]);

            result.STATICFLAG = Convert.ToInt16(this["STATICFLAG"]);

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(ScouringLab inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {

                this["WEIGHT"] = inst.WEIGHT;
                this["HIGHPRESS"] = inst.HIGHPRESS;
                this["AIR"] = inst.AIR;

                //เพิ่ม 25/11/18
                this["DYNAMIC"] = inst.DYNAMIC;
                this["EXPONENT"] = inst.EXPONENT;

                this["WEIGHTFLAG"] = inst.WEIGHTFLAG;
                this["HIGHPRESSFLAG"] = inst.HIGHPRESSFLAG;
                this["AIRFLAG"] = inst.AIRFLAG;

                //เพิ่ม 25/11/18
                this["DYNAMICFLAG"] = inst.DYNAMICFLAG;

                //test
                this["STATICFLAG"] = inst.STATICFLAG;

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reset Flags
        /// </summary>
        /// <returns></returns>
        public bool Reset_WEIGHT()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                if (master.IsConnected == true)
                {
                    //WEIGHT
                    master.WriteHoldingRegisters(29, 1);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }

        public bool Reset_HIGHPRESS()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                if (master.IsConnected == true)
                {
                    //HIGHPRESS
                    master.WriteHoldingRegisters(30, 1);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }

        public bool Reset_AIR()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                if (master.IsConnected == true)
                {
                    //AIR
                    master.WriteHoldingRegisters(31, 1);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }

        //เพิ่ม 25/11/18
        public bool Reset_DYNAMIC()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                if (master.IsConnected == true)
                {
                    //AIR
                    master.WriteHoldingRegisters(32, 1);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }

        public bool Reset_STATIC_AIR()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                if (master.IsConnected == true)
                {
                    //AIR
                    master.WriteHoldingRegisters(33, 1);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }


        #endregion
    }

    #endregion

    //เพิ่มใหม่ 09/06/20
    #region AirStaticLabModbusManager

    /// <summary>
    /// AirStaticLabModbusManager Modbus Manager.
    /// </summary>
    public class AirStaticLabModbusManager : ModbusManager<AirStaticLab>
    {
        #region Singelton

        private static AirStaticLabModbusManager _instance = null;
        /// <summary>
        /// Singelton Access Instance.
        /// </summary>
        public static AirStaticLabModbusManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(AirStaticLabModbusManager))
                    {
                        _instance = new AirStaticLabModbusManager();
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private AirStaticLabModbusManager()
            : base()
        {
            this.LoadConfig();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the config file name.
        /// </summary>
        /// <returns>Returns the config file name.</returns>
        protected override string GetFileName()
        {
            return "AirStaticLabModbusPLC.xml";
        }
        /// <summary>
        /// Init default config value.
        /// </summary>
        protected override void InitDefaultConfig()
        {
            #region Checks

            if (null == this.Configs)
            {
                "The Config instance is null.".Err();
                return;
            }

            #endregion

            try
            {
                this.Configs.Name = "AirStaticLab";
                // Slave
                this.Configs.Slave.IP = "172.20.78.119";
                this.Configs.Slave.Port = 502;
                this.Configs.Slave.SlaveId = 1;
                this.Configs.Slave.Enabled = true;
                // Master
                this.Configs.Master.IP = this.Configs.Slave.IP;
                this.Configs.Master.Port = 502;
                this.Configs.Master.SlaveId = 1;
                this.Configs.Master.Enabled = true;
                // Add address to read.
                this.Configs.Master.StartAddress = 0;
                this.Configs.Master.NoOfInputs = 550;
                ModbusValue[] values = new ModbusValue[]
                {
                    new ModbusValue() { Name = "AIR", Address = 15, DataType = typeof(float), SwapFP = true },
                    new ModbusValue() { Name = "AIRFLAG", Address = 22, DataType = typeof(short), SwapFP = false },
                };
                this.Configs.Master.Settings.AddRange(values);
            }
            catch (Exception ex)
            {
                ex.Err();
            }
        }
        /// <summary>
        /// Formatting read inputs after read data from modbus device.
        /// </summary>
        /// <returns>Returns instance of input model.</returns>
        protected override AirStaticLab FormatReadInputs()
        {
            AirStaticLab result = new AirStaticLab();

            result.AIR = Convert.ToSingle(this["AIR"]);
            result.AIRFLAG = Convert.ToInt16(this["AIRFLAG"]);

            return result;
        }
        /// <summary>
        /// Format data to write before send to modbus device.
        /// </summary>
        /// <param name="inst">The instance to write.</param>
        /// <returns>Returns true if format succcess.</returns>
        protected override bool FormatWriteInputs(AirStaticLab inst)
        {
            bool result = false;
            if (null == inst)
                return result;
            try
            {
                this["AIR"] = inst.AIR;
                this["AIRFLAG"] = inst.AIRFLAG;

                result = true;
            }
            catch (Exception ex)
            {
                ex.Err();
            }

            return result;
        }

        #endregion

        #region Public Methods

        public bool Reset_AIR()
        {
            bool result = false;

            ModbusTcpIpMaster master = null;

            try
            {
                master = new ModbusTcpIpMaster();
                master.IP = this.Configs.Master.IP;
                master.Port = this.Configs.Master.Port;
                master.SlaveId = this.Configs.Master.SlaveId;
                master.Connect();

                if (master.IsConnected == true)
                {
                    //AIR
                    master.WriteHoldingRegisters(32, 1);
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                ex.Err();
            }
            finally
            {
                if (null != master)
                {
                    try { master.Disconnect(); }
                    catch { }
                }
                master = null;
            }

            return result;
        }

        #endregion
    }

    #endregion
}