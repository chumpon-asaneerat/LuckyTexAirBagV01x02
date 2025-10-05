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
    #region MASTER_AIRBAGPROCESSLIST

    public class MASTER_AIRBAGPROCESSLIST
    {
        public System.String PROCESSID { get; set; }
        public System.String PROCESSDESCRIPTION { get; set; }
    }

    #endregion

    #region DEFECT_SEARCH

    public class DEFECT_SEARCH
    {
        public System.String DEFECTCODE { get; set; }
        public System.String DESCRIPTION_TH { get; set; }
        public System.String DESCRIPTION_EN { get; set; }
        public System.String PROCESSID { get; set; }
        public System.String DEFECTPROCESSCODE { get; set; }
        public System.Decimal? POINT { get; set; }
        public System.String PROCESSDESCRIPTION { get; set; }
    }

    #endregion

    #region DefectCode Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class DefectCodeSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        private string _DEFECTID = string.Empty;
        private string _PROCESSID = string.Empty;
        private string _THAIDESC = string.Empty;
        private string _ENGDESC = string.Empty;
        private decimal? _POINT = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DefectCodeSession()
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

        // ใช้สำหรับ Load ข้อมูลใส่ ComboBox
        #region Load Combo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<MASTER_AIRBAGPROCESSLIST> GetProcessList()
        {
            List<MASTER_AIRBAGPROCESSLIST> results = DefectCodeService.Instance
                .GetProcessList();

            return results;
        }

        #endregion

        #region Public Methods

        #region Init

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currUser"></param>
        public void Init(LogInResult currUser)
        {
            _currUser = currUser;
        }

        #endregion 

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets current user.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public LogInResult CurrentUser
        {
            get { return _currUser; }
            set
            {
                _currUser = value;
            }
        }
        /// <summary>
        /// Gets or sets Operator (or Operator).
        /// </summary>
        [XmlAttribute]
        public string Operator
        {
            get { return _operator; }
            set
            {
                if (_operator != value)
                {
                    _operator = value;
                }
            }
        }
      
        [XmlAttribute]
        public string DefectID
        {
            get { return _DEFECTID; }
            set
            {
                if (_DEFECTID != value)
                {
                    _DEFECTID = value;
                }
            }
        }

        [XmlAttribute]
        public string ProcessID
        {
            get { return _PROCESSID; }
            set
            {
                if (_PROCESSID != value)
                {
                    _PROCESSID = value;
                }
            }
        }

        [XmlAttribute]
        public string ThaiDesc
        {
            get { return _THAIDESC; }
            set
            {
                if (_THAIDESC != value)
                {
                    _THAIDESC = value;
                }
            }
        }

        [XmlAttribute]
        public string EngDesc
        {
            get { return _ENGDESC; }
            set
            {
                if (_ENGDESC != value)
                {
                    _ENGDESC = value;
                }
            }
        }

        [XmlAttribute]
        public decimal? Point
        {
            get { return _POINT; }
            set
            {
                if (_POINT != value)
                {
                    _POINT = value;
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
