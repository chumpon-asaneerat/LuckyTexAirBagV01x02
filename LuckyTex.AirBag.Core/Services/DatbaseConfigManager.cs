#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using NLib;
using NLib.Data;
using NLib.Logs;
using NLib.Xml;

using LuckyTex.Configs;

#endregion

namespace LuckyTex.Services
{
    #region Config Manager (Main)
    
    /// <summary>
    /// Config Manager. This class is used from get database connnection configuration from Xml file.
    /// </summary>
    public class ConfigManager
    {
        #region Singelton

        private static ConfigManager _instance = null;
        /// <summary>
        /// Singelton acccess instance.
        /// </summary>
        public static ConfigManager Instance
        {
            get
            {
                if (null == _instance)
                {
                    lock (typeof(ConfigManager))
                    {
                        _instance = new ConfigManager();
                    }

                }
                return _instance;
            }
        }

        #endregion

        #region Internal Variables

        private AirbagConfig _config = new AirbagConfig();

        private DefectFileConfig _defectConfig = new DefectFileConfig();

        private WeightConfig _weightConfig = new WeightConfig();

        private AS400Config _AS400Config = new AS400Config();

        private DefaultAS400Config _defaultAS400Config = new DefaultAS400Config();

        private MachineStatusConfig _MachineStatusConfig = new MachineStatusConfig();

        private BackupFilePDFConfig _backupFilePDFConfig = new BackupFilePDFConfig();

        private UploadReportFilePDFConfig _uploadReportFilePDFConfig = new UploadReportFilePDFConfig();

        //New 23/8/22
        private ConfirmLengthConfig _confirmLengthConfig = new ConfirmLengthConfig();


        private D365Config _d365config = new D365Config();

        #endregion

        #region Constructor and Destructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private ConfigManager() : base() { }
        /// <summary>
        /// Destructor.
        /// </summary>
        ~ConfigManager()
        {
        }

        #endregion

        #region Public Methods

        #region Configs

        /// <summary>
        /// Load Configs.
        /// </summary>
        public void LoadConfigs()
        {
            if (!System.IO.File.Exists(this.ConfigurationFileName))
            {
                UpdateConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationFileName))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationFileName);
                msg.Err();
                return;
            }
            _config = XmlManager.LoadFromFile<AirbagConfig>(this.ConfigurationFileName);
            if (null == _config)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationFileName);
                msg.Err();
            }
        }

        /// <summary>
        /// Update Config.
        /// </summary>
        public void UpdateConfigs()
        {
            if (null == _config)
                _config = new AirbagConfig();
            bool success = 
                XmlManager.SaveToFile<AirbagConfig>(this.ConfigurationFileName, _config);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationFileName);
                msg.Err();
            }
        }


        public void LoadConfigs_D365()
        {
            if (!System.IO.File.Exists(this.ConfigurationD365))
            {
                UpdateConfigs_D365();
            }
            if (!System.IO.File.Exists(this.ConfigurationD365))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationD365);
                msg.Err();
                return;
            }

            _d365config = XmlManager.LoadFromFile<D365Config>(this.ConfigurationD365);
            if (null == _d365config)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationD365);
                msg.Err();
            }
        }

        public void UpdateConfigs_D365()
        {
            if (null == _d365config)
                _d365config = new D365Config();
            bool success =
                XmlManager.SaveToFile<D365Config>(this.ConfigurationD365, _d365config);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationD365);
                msg.Err();
            }
        }

        #endregion

        #region Defect
        /// <summary>
        /// Load Defect Configs.
        /// </summary>
        public void LoadDefectConfigs()
        {
            if (!System.IO.File.Exists(this.ConfigurationDefectFileName))
            {
                UpdateDefectConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationDefectFileName))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationDefectFileName);
                msg.Err();
                return;
            }
            _defectConfig = XmlManager.LoadFromFile<DefectFileConfig>(this.ConfigurationDefectFileName);
            if (null == _defectConfig)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationDefectFileName);
                msg.Err();
            }
        }
        /// <summary>
        /// Update Defect Config.
        /// </summary>
        public void UpdateDefectConfigs()
        {
            if (null == _defectConfig)
                _defectConfig = new DefectFileConfig();
            bool success =
                XmlManager.SaveToFile<DefectFileConfig>(this.ConfigurationDefectFileName, _defectConfig);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationDefectFileName);
                msg.Err();
            }
        }
        #endregion

        #region Weight
        /// <summary>
        /// Load Weight Configs.
        /// </summary>
        public void LoadWeightConfigs()
        {
            if (!System.IO.File.Exists(this.ConfigurationWeight))
            {
                UpdateWeightConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationWeight))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationWeight);
                msg.Err();
                return;
            }
            _weightConfig = XmlManager.LoadFromFile<WeightConfig>(this.ConfigurationWeight);
            if (null == _weightConfig)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationWeight);
                msg.Err();
            }
        }
        /// <summary>
        /// Update Weight Config.
        /// </summary>
        public void UpdateWeightConfigs()
        {
            if (null == _weightConfig)
                _weightConfig = new WeightConfig();
            bool success =
                XmlManager.SaveToFile<WeightConfig>(this.ConfigurationWeight, _weightConfig);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationWeight);
                msg.Err();
            }
        }
        #endregion

        #region AS400
        /// <summary>
        /// Load AS400 Configs.
        /// </summary>
        public void LoadAS400Configs()
        {
            if (!System.IO.File.Exists(this.ConfigurationAS400))
            {
                UpdateAS400Configs();
            }
            if (!System.IO.File.Exists(this.ConfigurationAS400))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationAS400);
                msg.Err();
                return;
            }
            _AS400Config = XmlManager.LoadFromFile<AS400Config>(this.ConfigurationAS400);
            if (null == _AS400Config)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationAS400);
                msg.Err();
            }
        }
        /// <summary>
        /// Update AS400 Config.
        /// </summary>
        public void UpdateAS400Configs()
        {
            if (null == _AS400Config)
                _AS400Config = new AS400Config();
            bool success =
                XmlManager.SaveToFile<AS400Config>(this.ConfigurationAS400, _AS400Config);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationAS400);
                msg.Err();
            }
        }
        #endregion

        #region DefaultAS400
        /// <summary>
        /// Load Default AS400 Configs.
        /// </summary>
        public void LoadDefaultAS400Configs()
        {
            if (!System.IO.File.Exists(this.ConfigurationDefaultAS400))
            {
                UpdateDefaultAS400Configs();
            }
            if (!System.IO.File.Exists(this.ConfigurationDefaultAS400))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationDefaultAS400);
                msg.Err();
                return;
            }
            _defaultAS400Config = XmlManager.LoadFromFile<DefaultAS400Config>(this.ConfigurationDefaultAS400);
            if (null == _defaultAS400Config)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationDefaultAS400);
                msg.Err();
            }
        }
        /// <summary>
        /// Update DefaultAS400 Config.
        /// </summary>
        public void UpdateDefaultAS400Configs()
        {
            if (null == _defaultAS400Config)
                _defaultAS400Config = new DefaultAS400Config();
            bool success =
                XmlManager.SaveToFile<DefaultAS400Config>(this.ConfigurationDefaultAS400, _defaultAS400Config);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationDefaultAS400);
                msg.Err();
            }
        }

        #endregion

        #region MachineStatus
        /// <summary>
        /// Load MachineStatus Configs.
        /// </summary>
        public void LoadMachineStatusConfig()
        {
            if (!System.IO.File.Exists(this.ConfigurationMachineStatus))
            {
                UpdateMachineStatusConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationMachineStatus))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationMachineStatus);
                msg.Err();
                return;
            }
            _MachineStatusConfig = XmlManager.LoadFromFile<MachineStatusConfig>(this.ConfigurationMachineStatus);
            if (null == _MachineStatusConfig)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationMachineStatus);
                msg.Err();
            }
        }
        /// <summary>
        /// Update MachineStatus Config.
        /// </summary>
        public void UpdateMachineStatusConfigs()
        {
            if (null == _MachineStatusConfig)
                _MachineStatusConfig = new MachineStatusConfig();
            bool success =
                XmlManager.SaveToFile<MachineStatusConfig>(this.ConfigurationMachineStatus, _MachineStatusConfig);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationMachineStatus);
                msg.Err();
            }
        }
        #endregion

        #region BackupFilePDF
        /// <summary>
        /// Load BackupFilePDF Configs.
        /// </summary>
        public void LoadBackupFilePDFConfigs()
        {
            if (!System.IO.File.Exists(this.ConfigurationBackupFilePDF))
            {
                UpdateBackupFilePDFConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationBackupFilePDF))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationBackupFilePDF);
                msg.Err();
                return;
            }
            _backupFilePDFConfig = XmlManager.LoadFromFile<BackupFilePDFConfig>(this.ConfigurationBackupFilePDF);
            if (null == _backupFilePDFConfig)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationBackupFilePDF);
                msg.Err();
            }
        }
        /// <summary>
        /// Update BackupFilePDF Config.
        /// </summary>
        public void UpdateBackupFilePDFConfigs()
        {
            if (null == _backupFilePDFConfig)
                _backupFilePDFConfig = new BackupFilePDFConfig();
            bool success =
                XmlManager.SaveToFile<BackupFilePDFConfig>(this.ConfigurationBackupFilePDF, _backupFilePDFConfig);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationBackupFilePDF);
                msg.Err();
            }
        }
        #endregion

        #region UploadReportFilePDF
        /// <summary>
        /// Load UploadReportFilePDF Configs.
        /// </summary>
        public void LoadUploadReportPDFConfigs()
        {
            if (!System.IO.File.Exists(this.ConfigurationUploadReportFilePDF))
            {
                UpdateUploadReportFilePDFConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationUploadReportFilePDF))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationUploadReportFilePDF);
                msg.Err();
                return;
            }
            _uploadReportFilePDFConfig = XmlManager.LoadFromFile<UploadReportFilePDFConfig>(this.ConfigurationUploadReportFilePDF);
            if (null == _uploadReportFilePDFConfig)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationUploadReportFilePDF);
                msg.Err();
            }
        }
        /// <summary>
        /// Update UploadReportFilePDF Config.
        /// </summary>
        public void UpdateUploadReportFilePDFConfigs()
        {
            if (null == _uploadReportFilePDFConfig)
                _uploadReportFilePDFConfig = new UploadReportFilePDFConfig();
            bool success =
                XmlManager.SaveToFile<UploadReportFilePDFConfig>(this.ConfigurationUploadReportFilePDF, _uploadReportFilePDFConfig);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationUploadReportFilePDF);
                msg.Err();
            }
        }
        #endregion

        //New 23/8/22
        #region ConfirmLength
        /// <summary>
        /// Load ConfirmLength Configs.
        /// </summary>
        public void LoadConfirmLengthConfigs()
        {
            if (!System.IO.File.Exists(this.ConfigurationConfirmLength))
            {
                UpdateConfirmLengthConfigs();
            }
            if (!System.IO.File.Exists(this.ConfigurationConfirmLength))
            {
                string msg = string.Format(
                    "Cannnot create file: {0}", this.ConfigurationConfirmLength);
                msg.Err();
                return;
            }
            _confirmLengthConfig = XmlManager.LoadFromFile<ConfirmLengthConfig>(this.ConfigurationConfirmLength);
            if (null == _confirmLengthConfig)
            {
                string msg = string.Format(
                    "Cannnot load configuration file: {0}", this.ConfigurationConfirmLength);
                msg.Err();
            }
        }
        /// <summary>
        /// Update ConfirmLength Config.
        /// </summary>
        public void UpdateConfirmLengthConfigs()
        {
            if (null == _confirmLengthConfig)
                _confirmLengthConfig = new ConfirmLengthConfig();
            bool success =
                XmlManager.SaveToFile<ConfirmLengthConfig>(this.ConfigurationConfirmLength, _confirmLengthConfig);
            if (!success)
            {
                string msg = string.Format(
                    "Cannnot save configuration file: {0}", this.ConfigurationConfirmLength);
                msg.Err();
            }
        }
        #endregion

        #endregion

        #region Public Properties

        #region ConfigurationFileName
        /// <summary>
        /// Gets Configuration File Name.
        /// </summary>
        public string ConfigurationFileName
        {
            get
            {
                string result = 
                    AirBagConsts.GetShareConfigFullName("AirBagConfig.xml");
                return result;
            }
        }
        /// <summary>
        /// Gets Database config.
        /// </summary>
        public OracleConfig DatabaseConfig
        {
            get
            {
                if (null == _config)
                    _config = new AirbagConfig();
                return _config.Database;
            }
        }
        #endregion

        #region ConfigurationDefectFileName

        // เพิ่มขึ้นมาเพื่อใช้ส่งค่า String ของที่อยู่ DefectFile
        /// <summary>
        /// Gets Configuration Defect File Name.
        /// </summary>
        public string ConfigurationDefectFileName
        {
            get
            {
                string result =
                    DefectFileConsts.GetShareConfigFullName("DefectFileConfig.xml");
                return result;
            }
        }
        /// <summary>
        /// DefectFileConfig
        /// </summary>
        public string DefectFileConfig
        {
            get
            {
                if (null == _defectConfig)
                    _defectConfig = new DefectFileConfig();
                return _defectConfig.SendDefectFile;
            }
        }

        #endregion

        #region ConfigurationWeight

        // เพิ่มขึ้นมาเพื่อระบุว่าใช้เครื่องจักรอะไรในการหา Weight
        /// <summary>
        /// Gets Configuration Weight machine.
        /// </summary>
        public string ConfigurationWeight
        {
            get
            {
                string result =
                    WeightConsts.GetShareConfigFullName("WeightConfig.xml");
                return result;
            }
        }
        /// <summary>
        /// Weight machine Config
        /// </summary>
        public string WeightConfig
        {
            get
            {
                if (null == _weightConfig)
                    _weightConfig = new WeightConfig();
                return _weightConfig.WeightMachine;
            }
        }

        #endregion

        #region ConfigurationAS400

        // เพิ่มขึ้นมาเพื่อระบุว่าต่อกับ AS400
        /// <summary>
        /// Gets Configuration AS400.
        /// </summary>
        public string ConfigurationAS400
        {
            get
            {
                string result =
                    AS400Consts.GetShareConfigFullName("AS400Config.xml");
                return result;
            }
        }
        /// <summary>
        /// AS400 Config
        /// </summary>
        public string AS400Config
        {
            get
            {
                if (null == _AS400Config)
                    _AS400Config = new AS400Config();
                return _AS400Config.Provider+
                    _AS400Config.DataSource + 
                    _AS400Config.UserID +
                    _AS400Config.Password +
                    _AS400Config.DefaultCollection;
            }
        }

        #endregion

        #region ConfigurationDefaultAS400

        // เพิ่มขึ้นมาเพื่อระบุว่าใช้ Default AS400
        /// <summary>
        /// Gets Configuration DefaultAS400 machine.
        /// </summary>
        public string ConfigurationDefaultAS400
        {
            get
            {
                string result =
                    DefaultAS400Consts.GetShareConfigFullName("DefaultAS400Config.xml");
                return result;
            }
        }

        /// <summary>
        /// DefaultAS400 Config --> FLAGS
        /// </summary>
        public string DefaultAS400Config_FLAGS
        {
            get
            {
                if (null == _defaultAS400Config)
                    _defaultAS400Config = new DefaultAS400Config();
                return _defaultAS400Config.FLAGS;
            }
        }

        /// <summary>
        /// DefaultAS400 Config --> RECTY
        /// </summary>
        public string DefaultAS400Config_RECTY
        {
            get
            {
                if (null == _defaultAS400Config)
                    _defaultAS400Config = new DefaultAS400Config();
                return _defaultAS400Config.RECTY;
            }
        }

        /// <summary>
        /// DefaultAS400 Config --> CDSTO
        /// </summary>
        public string DefaultAS400Config_CDSTO
        {
            get
            {
                if (null == _defaultAS400Config)
                    _defaultAS400Config = new DefaultAS400Config();
                return _defaultAS400Config.CDSTO;
            }
        }

        /// <summary>
        /// DefaultAS400 Config --> USRNM
        /// </summary>
        public string DefaultAS400Config_USRNM
        {
            get
            {
                if (null == _defaultAS400Config)
                    _defaultAS400Config = new DefaultAS400Config();
                return _defaultAS400Config.USRNM;
            }
        }

        /// <summary>
        /// DefaultAS400 Config --> CDUM0
        /// </summary>
        public string DefaultAS400Config_CDUM0
        {
            get
            {
                if (null == _defaultAS400Config)
                    _defaultAS400Config = new DefaultAS400Config();
                return _defaultAS400Config.CDUM0;
            }
        }

        #endregion

        #region ConfigurationMachineStatus

        // เพิ่มขึ้นมาเพื่อระบุว่าต่อกับ MachineStatus
        /// <summary>
        /// Gets Configuration MachineStatus.
        /// </summary>
        public string ConfigurationMachineStatus
        {
            get
            {
                string result =
                    MachineStatusConsts.GetShareConfigFullName("MachineStatusConfig.xml");
                return result;
            }
        }
        
        public bool Coating1MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Coating1Status;
            }
        }

        public bool Coating2MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Coating2Status ;
            }
        }

        public bool Coating3MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Coating3Status;
            }
        }

        public bool Coating3ScouringMachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Coating3ScouringStatus;
            }
        }

        public bool Scouring1MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Scouring1Status;
            }
        }

        public bool Scouring2MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Scouring2Status;
            }
        }

        //เพิ่มใหม่ Scouring Coat 2 05/01/17
        public bool ScouringCoat2MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.ScouringCoat2Status;
            }
        }

        public bool Coating1ScouringMachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Coating1ScouringStatus;
            }
        }

        public bool Scouring2ScouringDryMachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.Scouring2ScouringDryStatus;
            }
        }

        //เพิ่มใหม่ 12/09/18
        public bool ScouringCoat1MachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.ScouringCoat1Status;
            }
        }

        //Update 10/07/18
        public bool ScouringLabMachineConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.ScouringLabStatus;
            }
        }

        //Update 09/06/20
        public bool AirStaticLabConfig
        {
            get
            {
                if (null == _MachineStatusConfig)
                    _MachineStatusConfig = new MachineStatusConfig();
                return _MachineStatusConfig.AirStaticLabStatus;
            }
        }


        #endregion

        #region ConfigurationBackupFilePDF

        // เพิ่มขึ้นมาเพื่อระบุว่าใช้ Backup File PDF
        /// <summary>
        /// Gets Configuration BackupFilePDF.
        /// </summary>
        public string ConfigurationBackupFilePDF
        {
            get
            {
                string result =
                    BackupFilePDFConsts.GetShareConfigFullName("BackupFilePDFConfig.xml");
                return result;
            }
        }
        
        public string BackupFilePDFConfig_Elongation
        {
            get
            {
                if (null == _backupFilePDFConfig)
                    _backupFilePDFConfig = new BackupFilePDFConfig();
                return _backupFilePDFConfig.Elongation;
            }
        }

        public string BackupFilePDFConfig_Edgecomb
        {
            get
            {
                if (null == _backupFilePDFConfig)
                    _backupFilePDFConfig = new BackupFilePDFConfig();
                return _backupFilePDFConfig.Edgecomb;
            }
        }

        public string BackupFilePDFConfig_TearStrength
        {
            get
            {
                if (null == _backupFilePDFConfig)
                    _backupFilePDFConfig = new BackupFilePDFConfig();
                return _backupFilePDFConfig.TearStrength;
            }
        }

        #endregion

        #region ConfigurationUploadReportFilePDF
      
        /// <summary>
        /// 
        /// </summary>
        public string ConfigurationUploadReportFilePDF
        {
            get
            {
                string result =
                    UploadReportFilePDFConsts.GetShareConfigFullName("UploadReportFilePDFConfig.xml");
                return result;
            }
        }

        public string UploadReportConfig_UploadReport
        {
            get
            {
                if (null == _uploadReportFilePDFConfig)
                    _uploadReportFilePDFConfig = new UploadReportFilePDFConfig();
                return _uploadReportFilePDFConfig.UploadReport;
            }
        }

        #endregion

        //New 23/8/22
        #region ConfigurationConfirmLength

        // เพิ่มขึ้นมาเพื่อระบุในการหา ConfigurationConfirmLength
        /// <summary>
        /// ConfigurationConfirmLength
        /// </summary>
        public string ConfigurationConfirmLength
        {
            get
            {
                string result =
                    ConfirmLengthConsts.GetShareConfigFullName("ConfirmLength.xml");
                return result;
            }
        }
        /// <summary>
        /// ConfirmLength 
        /// </summary>
        public string ConfirmLengthConfig
        {
            get
            {
                if (null == _confirmLengthConfig)
                    _confirmLengthConfig = new ConfirmLengthConfig();
                return _confirmLengthConfig.ConfirmLength;
            }
        }

        #endregion

        #region ConfigurationD365
        /// <summary>
        /// Gets Configuration File Name.
        /// </summary>
        public string ConfigurationD365
        {
            get
            {
                string result =
                    D365Consts.GetShareConfigFullName("D365Config.xml");
                return result;
            }
        }
        /// <summary>
        /// Gets Database config.
        /// </summary>
        public SqlServerConfig DatabaseConfigD365
        {
            get
            {
                if (null == _d365config)
                    _d365config = new D365Config();
                return _d365config.Database;
            }
        }
        #endregion

        #endregion
    }

    #endregion
}
