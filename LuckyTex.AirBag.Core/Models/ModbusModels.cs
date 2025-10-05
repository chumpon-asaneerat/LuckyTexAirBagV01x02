using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LuckyTex.Models
{
    #region InspectionWeight Modbus Model

    /// <summary>
    /// The Inspection Weight  Modbus Model.
    /// </summary>
    public class InspectionWeight
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public InspectionWeight() : base()
        {

        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets Weight 1.
        /// </summary>
        public float W1 { get; set; }
        /// <summary>
        /// Gets or sets Weight 2.
        /// </summary>
        public float W2 { get; set; }

        #endregion
    }

    #endregion

    #region Coating1 Modbus Model

    /// <summary>
    /// Coating1 Modbus Model.
    /// </summary>
    public class Coating1
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating1()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP
        
        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }

        #endregion

        #region SAT
        
        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT
        
        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED
        
        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN
        
        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR
        
        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region Coating2 Modbus Model

    /// <summary>
    /// Coating2 Modbus Model.
    /// </summary>
    public class Coating2
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating2()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP9PV.
        /// </summary>
        public short TEMP9PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP9SP.
        /// </summary>
        public short TEMP9SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP10PV.
        /// </summary>
        public short TEMP10PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP10SP.
        /// </summary>
        public short TEMP10SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN

        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region Coating3 Modbus Model

    /// <summary>
    /// Coating2 Modbus Model.
    /// </summary>
    public class Coating3
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating3()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP9PV.
        /// </summary>
        public short TEMP9PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP9SP.
        /// </summary>
        public short TEMP9SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP10PV.
        /// </summary>
        public short TEMP10PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP10SP.
        /// </summary>
        public short TEMP10SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN

        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region Scouring1 Modbus Model

    /// <summary>
    /// Scouring1 Modbus Model.
    /// </summary>
    public class Scouring1
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Scouring1()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region Scouring2 Modbus Model

    /// <summary>
    /// Scouring2 Modbus Model.
    /// </summary>
    public class Scouring2
    {
        #region Constructor
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public Scouring2()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED1.
        /// </summary>
        public float? SPEED1 { get; set; }
        /// <summary>
        /// Gets or sets SPEED1.
        /// </summary>
        public float? SPEED2 { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region Coating3Scouring Modbus Model

    /// <summary>
    /// Coating2 Modbus Model.
    /// </summary>
    public class Coating3Scouring
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating3Scouring()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP9PV.
        /// </summary>
        public short TEMP9PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP9SP.
        /// </summary>
        public short TEMP9SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP10PV.
        /// </summary>
        public short TEMP10PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP10SP.
        /// </summary>
        public short TEMP10SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN

        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region FinishingCounter Modbus Model

    /// <summary>
    /// Finishing Counter Modbus Model.
    /// </summary>
    public class FinishingCounter
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FinishingCounter()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region Overall

        /// <summary>
        /// Gets or sets Overall Batch Counter.
        /// </summary>
        public float OverallBatchCounter { get; set; }
        /// <summary>
        /// Gets or sets Overall Total Counter.
        /// </summary>
        public float OverallTotalCounter { get; set; }
        /// <summary>
        /// Gets or sets Overall Batch Count (only finished).
        /// </summary>
        public short OverallBatchCount { get; set; }

        #endregion

        #region Flags

        /// <summary>
        /// Gets or sets Batch Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short BatchFlag { get; set; }
        /// <summary>
        /// Gets or sets Total Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short TotalFlag { get; set; }

        #endregion

        #region RealTime

        /// <summary>
        /// Gets or sets RealTime Batch Counter.
        /// </summary>
        public float RealTimeBatchCounter { get; set; }
        /// <summary>
        /// Gets or sets RealTime Total Counter.
        /// </summary>
        public float RealTimeTotalCounter { get; set; }

        #endregion

        #region Batch Counter 1-7

        /// <summary>
        /// Gets or sets Batch Counter 1.
        /// </summary>
        public float BatchCounter1 { get; set; }
        /// <summary>
        /// Gets or sets Batch Counter 2.
        /// </summary>
        public float BatchCounter2 { get; set; }
        /// <summary>
        /// Gets or sets Batch Counter 3.
        /// </summary>
        public float BatchCounter3 { get; set; }
        /// <summary>
        /// Gets or sets Batch Counter 4.
        /// </summary>
        public float BatchCounter4 { get; set; }
        /// <summary>
        /// Gets or sets Batch Counter 5.
        /// </summary>
        public float BatchCounter5 { get; set; }
        /// <summary>
        /// Gets or sets Batch Counter 6.
        /// </summary>
        public float BatchCounter6 { get; set; }
        /// <summary>
        /// Gets or sets Batch Counter 7.
        /// </summary>
        public float BatchCounter7 { get; set; }

        #endregion

        #region Total Counter 1-7

        /// <summary>
        /// Gets or sets Total Counter 1.
        /// </summary>
        public float TotalCounter1 { get; set; }
        /// <summary>
        /// Gets or sets Total Counter 2.
        /// </summary>
        public float TotalCounter2 { get; set; }
        /// <summary>
        /// Gets or sets Total Counter 3.
        /// </summary>
        public float TotalCounter3 { get; set; }
        /// <summary>
        /// Gets or sets Total Counter 4.
        /// </summary>
        public float TotalCounter4 { get; set; }
        /// <summary>
        /// Gets or sets Total Counter 5.
        /// </summary>
        public float TotalCounter5 { get; set; }
        /// <summary>
        /// Gets or sets Total Counter 6.
        /// </summary>
        public float TotalCounter6 { get; set; }
        /// <summary>
        /// Gets or sets Total Counter 7.
        /// </summary>
        public float TotalCounter7 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    //เพิ่มใหม่ Scouring Coat 2 05/01/17
    #region ScouringCoat2 Modbus Model

    /// <summary>
    /// ScouringCoat2 Modbus Model.
    /// </summary>
    public class ScouringCoat2
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScouringCoat2()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP9PV.
        /// </summary>
        public short TEMP9PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP9SP.
        /// </summary>
        public short TEMP9SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP10PV.
        /// </summary>
        public short TEMP10PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP10SP.
        /// </summary>
        public short TEMP10SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN

        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 12/05/17
    #region Coating1 Scouring Modbus Model

    /// <summary>
    /// Coating1 Modbus Model.
    /// </summary>
    public class Coating1Scouring
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Coating1Scouring()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN

        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 26/05/17
    #region Scouring2ScouringDry Modbus Model

    /// <summary>
    /// Scouring2 Modbus Model.
    /// </summary>
    public class Scouring2ScouringDry
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Scouring2ScouringDry()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED1.
        /// </summary>
        public float? SPEED1 { get; set; }
        /// <summary>
        /// Gets or sets SPEED1.
        /// </summary>
        public float? SPEED2 { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 12/09/18
    #region ScouringCoat1 Modbus Model

    /// <summary>
    /// Coating1 Modbus Model.
    /// </summary>
    public class ScouringCoat1
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScouringCoat1()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region TEMP

        /// <summary>
        /// Gets or sets TEMP1PV.
        /// </summary>
        public short TEMP1PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP1SP.
        /// </summary>
        public short TEMP1SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP2PV.
        /// </summary>
        public short TEMP2PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP2SP.
        /// </summary>
        public short TEMP2SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP3PV.
        /// </summary>
        public short TEMP3PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP3SP.
        /// </summary>
        public short TEMP3SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP4PV.
        /// </summary>
        public short TEMP4PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP4SP.
        /// </summary>
        public short TEMP4SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP5PV.
        /// </summary>
        public short TEMP5PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP5SP.
        /// </summary>
        public short TEMP5SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP6PV.
        /// </summary>
        public short TEMP6PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP6SP.
        /// </summary>
        public short TEMP6SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP7PV.
        /// </summary>
        public short TEMP7PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP7SP.
        /// </summary>
        public short TEMP7SP { get; set; }
        /// <summary>
        /// Gets or sets TEMP8PV.
        /// </summary>
        public short TEMP8PV { get; set; }
        /// <summary>
        /// Gets or sets TEMP8SP.
        /// </summary>
        public short TEMP8SP { get; set; }

        #endregion

        #region SAT

        /// <summary>
        /// Gets or sets SATPV.
        /// </summary>
        public short SATPV { get; set; }
        /// <summary>
        /// Gets or sets SATSP.
        /// </summary>
        public short SATSP { get; set; }

        #endregion

        #region WASH

        /// <summary>
        /// Gets or sets WASH1PV.
        /// </summary>
        public short WASH1PV { get; set; }
        /// <summary>
        /// Gets or sets WASH1SP.
        /// </summary>
        public short WASH1SP { get; set; }
        /// <summary>
        /// Gets or sets WASH2PV.
        /// </summary>
        public short WASH2PV { get; set; }
        /// <summary>
        /// Gets or sets WASH2SP.
        /// </summary>
        public short WASH2SP { get; set; }

        #endregion

        #region HOT

        /// <summary>
        /// Gets or sets HOTPV.
        /// </summary>
        public short HOTPV { get; set; }
        /// <summary>
        /// Gets or sets HOTSP.
        /// </summary>
        public short HOTSP { get; set; }

        #endregion

        #region SPEED

        /// <summary>
        /// Gets or sets SPEED.
        /// </summary>
        public float? SPEED { get; set; }

        #endregion

        #region TEN

        /// <summary>
        /// Gets or sets TENUP.
        /// </summary>
        public float TENUP { get; set; }
        /// <summary>
        /// Gets or sets TENDOWN.
        /// </summary>
        public float TENDOWN { get; set; }

        #endregion

        #region SENSOR

        /// <summary>
        /// Gets or sets SEN1.
        /// </summary>
        public short SEN1 { get; set; }
        /// <summary>
        /// Gets or sets SEN2.
        /// </summary>
        public short SEN2 { get; set; }

        #endregion

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 10/07/18
    #region ScouringLab Modbus Model

    /// <summary>
    /// ScouringLab Modbus Model.
    /// </summary>
    public class ScouringLab
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ScouringLab()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region Flags

        /// <summary>
        /// Gets or sets WEIGHT Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short WEIGHTFLAG { get; set; }
        /// <summary>
        /// Gets or sets High Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short HIGHPRESSFLAG { get; set; }
        /// <summary>
        /// Gets or sets Air Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short AIRFLAG { get; set; }

        //เพิ่ม 25/11/18
        /// <summary>
        /// Gets or sets DYNAMIC Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short DYNAMICFLAG { get; set; }

        public short STATICFLAG { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets Weight.
        /// </summary>
        public float? WEIGHT { get; set; }
        /// <summary>
        /// Gets or sets High press.
        /// </summary>
        public float? HIGHPRESS { get; set; }
        public float? HIGHPRESS2 { get; set; }
        /// <summary>
        /// Gets or sets Air press.
        /// </summary>
        public float? AIR { get; set; }

        //เพิ่ม 25/11/18
        /// <summary>
        /// Gets or sets DYNAMIC press.
        /// </summary>
        public float? DYNAMIC { get; set; }
        /// <summary>
        /// Gets or sets EXPONENT press.
        /// </summary>
        public float? EXPONENT { get; set; }


        public float? STATIC { get; set; }

        #endregion
    }

    #endregion

    //เพิ่มใหม่ 03/07/20
    #region AirStaticLab Modbus Model

    /// <summary>
    /// AirStaticLab Modbus Model.
    /// </summary>
    public class AirStaticLab
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AirStaticLab()
            : base()
        {

        }

        #endregion

        #region Public Properties

        #region Flags
        /// <summary>
        /// Gets or sets Air Flag. Value 0 = reset, 1 = finished.
        /// </summary>
        public short AIRFLAG { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets Air press.
        /// </summary>
        public float? AIR { get; set; }

        #endregion
    }

    #endregion
}
