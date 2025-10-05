#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

using NLib;
using LuckyTex.Services;
using System.Windows.Media;

namespace LuckyTex.Models
{

    #region OperatorData

    /// <summary>
    /// The Operator Lot Data class.
    /// </summary>
    [Serializable]
    public class Operator_SearchData
    {
        #region Public Propeties

        public System.String OPERATORID { get; set; }
        public System.String TITLE { get; set; }
        public System.String FNAME { get; set; }
        public System.String LNAME { get; set; }
        public System.String USERNAME { get; set; }
        public System.String PASSWORD { get; set; }
        public System.String DELETEFLAG { get; set; }
        public System.String POSITIONLEVEL { get; set; }
        public System.String PROCESSID { get; set; }
        public System.DateTime? CREATEDDATE { get; set; }
        public System.String CREATEDBY { get; set; }
        public System.String WEB { get; set; }
        public System.String WIP { get; set; }

        #endregion
    }

    #endregion

    #region Operator Session

    /// <summary>
    /// Operator Session.
    /// </summary>
    [Serializable]
    public class OperatorSession
    {
        #region Internal Variables

        private string _opID = string.Empty;
        private string _title = string.Empty;
        private string _fName = string.Empty;
        private string _lName = string.Empty;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _positionLevel = string.Empty;
        private string _processID = string.Empty;
        private string _deleteFlag = string.Empty;

        private string _createdBy = string.Empty;
        private DateTime? _createdDate = null;
        private string _web = string.Empty;
        private string _wip = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public OperatorSession()
            : base()
        {

        }

        #endregion

        #region Private Methods

        #region Event Raiser(s)

        private void RaiseStateChanged()
        {
            if (null != OnStateChanged)
            {
                OnStateChanged.Call(this, EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region New

        public void New()
        {
            _opID = string.Empty;
            _title = string.Empty;
            _fName = string.Empty;
            _lName = string.Empty;
            _userName = string.Empty;
            _password = string.Empty;
            _positionLevel = string.Empty;
            _processID = string.Empty;
            _deleteFlag = string.Empty;
            _web = string.Empty;
            _wip = string.Empty;
        }

        #endregion

        #region Save

        public string Save()
        {
            string chkSave = string.Empty;

            try
            {
                chkSave = MasterDataService.Instance.SaveOperator(_opID,
                _title, _fName, _lName, _userName, _password, _positionLevel, _processID, _deleteFlag, _createdBy, _web, _wip);
            }
            catch (Exception ex)
            {
                chkSave = ex.Message.ToString();
            }

            return chkSave;
        }

        #endregion

        #region Get Operator data

        /// <summary>
        /// Gets Operator Data.
        /// </summary>
        public Operator_SearchData GetOperatorData()
        {
            Operator_SearchData result = null;

            List<Operator_SearchData> results =
                MasterDataService.Instance.Operator_SearchDataList(_opID,"","","","","");
            if (null != results && results.Count > 0)
            {
                result = results[0];
            }

            return result;
        }

        #endregion

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets OperatorID.
        /// </summary>
        [XmlAttribute]
        public string OperatorID
        {
            get { return _opID; }
            set
            {
                if (_opID != value)
                {
                    _opID = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string FName
        {
            get { return _fName; }
            set
            {
                if (_fName != value)
                {
                    _fName = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string LName
        {
            get { return _lName; }
            set
            {
                if (_lName != value)
                {
                    _lName = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Password 
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string PositionLevel 
        {
            get { return _positionLevel; }
            set
            {
                if (_positionLevel != value)
                {
                    _positionLevel = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string ProcessID
        {
            get { return _processID; }
            set
            {
                if (_processID != value)
                {
                    _processID = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string DeleteFlag 
        {
            get { return _deleteFlag; }
            set
            {
                if (_deleteFlag != value)
                {
                    _deleteFlag = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                if (_createdBy != value)
                {
                    _createdBy = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set
            {
                if (_createdDate != value)
                {
                    _createdDate = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string Web
        {
            get { return _web; }
            set
            {
                if (_web != value)
                {
                    _web = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string WIP
        {
            get { return _wip; }
            set
            {
                if (_wip != value)
                {
                    _wip = value;
                }
            }
        }
        #endregion

        #region Public Events

        /// <summary>
        /// OnStateChanged event.
        /// </summary>
        public event EventHandler OnStateChanged;

        #endregion
    }

    #endregion

}
