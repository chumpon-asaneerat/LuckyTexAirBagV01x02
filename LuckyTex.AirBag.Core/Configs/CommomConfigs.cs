#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;

#endregion

#region Extra Usings

using System.IO;

using NLib;
//using NLib.Configs;
using NLib.Logs;
using NLib.Data;
using NLib.Xml;

#endregion

namespace LuckyTex.Configs
{
    /// <summary>
    /// Airbag common config class.
    /// </summary>
    [Serializable]
    public class AirbagConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AirbagConfig() : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.Database = new OracleConfig();
            this.Database.DataSource.HostName = "ORACLE11";
            this.Database.DataSource.ServiceName = "orcl";
            this.Database.Security.UserName = "AIRBAG2014";
            this.Database.Security.Password = "winnt123";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The database server connection config.
        /// </summary>
        [XmlElement]
        public OracleConfig Database { get; set; }

        #endregion
    }

    [Serializable]
    public class DefectFileConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DefectFileConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.SendDefectFile = @"\\172.20.10.18\D$\Inspection\DefectFile\";
        }

        #endregion

        //private string _sendDefectFile = @"\\172.20.10.18\D$\Inspection\DefectFile\";

        [XmlAttribute]
        public string SendDefectFile { get; set; }
        //public string SendDefectFile { get { return _sendDefectFile; } set { _sendDefectFile = value; } }
    }

    [Serializable]
    public class WeightConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WeightConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.WeightMachine = "W1";
        }

        #endregion

        [XmlAttribute]
        public string WeightMachine { get; set; }

    }

    [Serializable]
    public class AS400Config
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public AS400Config()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.Provider = "Provider=" + "IBMDA400" + ";";
            this.DataSource = "Data Source=" + "172.20.7.16" + ";";
            this.UserID = "User ID=" + "PLM" + ";";
            this.Password = "Password=" + "LUCKY" + ";";
            this.DefaultCollection = "Default Collection=" + "TESLUCDAT" + ";";
        }

        #endregion

        [XmlAttribute]
        public string Provider { get; set; }
        [XmlAttribute]
        public string DataSource { get; set; }
        [XmlAttribute]
        public string UserID { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
        [XmlAttribute]
        public string DefaultCollection { get; set; }
    }

    [Serializable]
    public class DefaultAS400Config
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DefaultAS400Config()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.FLAGS = "R";
            this.RECTY = "A";
            this.CDSTO = "3T";
            this.USRNM = "PGMR";
            this.CDUM0 = "MT";
        }

        #endregion

        [XmlAttribute]
        public string FLAGS { get; set; }

        [XmlAttribute]
        public string RECTY { get; set; }

        [XmlAttribute]
        public string CDSTO { get; set; }

        [XmlAttribute]
        public string USRNM { get; set; }

        [XmlAttribute]
        public string CDUM0 { get; set; }
    }

    [Serializable]
    public class MachineStatusConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public MachineStatusConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.Coating1Status = true;
            this.Coating2Status = true;
            this.Coating3Status = true;
            this.Scouring1Status = true;
            this.Scouring2Status = true;
            this.Coating3ScouringStatus = true;

            //เพิ่มใหม่ ScouringCoat2Status 05/01/17
            this.ScouringCoat2Status = true;

            //เพิ่มใหม่ ScouringCoat1Status 12/09/18
            this.ScouringCoat1Status = true;

            this.Coating1ScouringStatus = true;
            this.Scouring2ScouringDryStatus = true;
            this.ScouringLabStatus = true;
            this.AirStaticLabStatus = true;
        }

        #endregion

        [XmlAttribute]
        public bool Coating1Status { get; set; }
        [XmlAttribute]
        public bool Coating2Status { get; set; }
        [XmlAttribute]
        public bool Coating3Status { get; set; }
        [XmlAttribute]
        public bool Scouring1Status { get; set; }
        
        [XmlAttribute]
        public bool Scouring2Status { get; set; }
        [XmlAttribute]
        public bool Coating3ScouringStatus { get; set; }

        //เพิ่มใหม่ ScouringCoat2Status 05/01/17
        [XmlAttribute]
        public bool ScouringCoat2Status { get; set; }

        [XmlAttribute]
        public bool Coating1ScouringStatus { get; set; }

         [XmlAttribute]
        public bool Scouring2ScouringDryStatus { get; set; }

         //เพิ่มใหม่ ScouringCoat1Status 12/09/18
         [XmlAttribute]
         public bool ScouringCoat1Status { get; set; }

         [XmlAttribute]
         public bool ScouringLabStatus { get; set; }

         [XmlAttribute]
         public bool AirStaticLabStatus { get; set; }
    }

    //-- Lab --//
    [Serializable]
    public class BackupFilePDFConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BackupFilePDFConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.Elongation = @"D:\pdfbackup\tensile\";
            this.Edgecomb = @"D:\pdfbackup\edgcomb\";
            this.TearStrength = @"D:\pdfbackup\tear\";
        }

        #endregion

        [XmlAttribute]
        public string Elongation { get; set; }
        [XmlAttribute]
        public string Edgecomb { get; set; }
        [XmlAttribute]
        public string TearStrength { get; set; }

    }

    public class UploadReportFilePDFConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UploadReportFilePDFConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.UploadReport = @"D:\UploadReport\";
        }

        #endregion

        [XmlAttribute]
        public string UploadReport { get; set; }
    }

    //---------//


    /// <summary>
    /// Lab common config class.
    /// </summary>
    [Serializable]
    public class LabConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LabConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.Database = new OracleConfig();
            this.Database.DataSource.HostName = "ORACLE11";
            this.Database.DataSource.ServiceName = "orcl";
            this.Database.Security.UserName = "AIRBAG2014";
            this.Database.Security.Password = "winnt123";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The database server connection config.
        /// </summary>
        [XmlElement]
        public OracleConfig Database { get; set; }

        #endregion
    }

    //New 23/8/22
    [Serializable]
    public class ConfirmLengthConfig
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ConfirmLengthConfig()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.ConfirmLength = "10";
        }

        #endregion

        [XmlAttribute]
        public string ConfirmLength { get; set; }

    }

    [Serializable]
    public class D365Config
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public D365Config()
            : base()
        {
            InitDefault();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Init default config.
        /// </summary>
        public void InitDefault()
        {
            this.Database = new SqlServerConfig();
            this.Database.DataSource.ServerName = "(local)";
            this.Database.DataSource.Version = SqlServerVersion.SqlServer2012;
            this.Database.DataSource.DatabaseName = "TRACE";
            this.Database.Security.UserName = "sa";
            this.Database.Security.Password = "2Ttt@Dmin!365";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The database server connection config.
        /// </summary>
        [XmlElement]
        public SqlServerConfig Database { get; set; }

        #endregion
    }
}
