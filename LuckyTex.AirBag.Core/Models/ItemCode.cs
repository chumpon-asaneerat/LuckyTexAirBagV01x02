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
    #region ITM_SEARCHITEMCODE

    public class ITM_SEARCHITEMCODE
    {
        public System.String ITM_CODE { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.Decimal? ITM_WIDTH { get; set; }
        public System.String ITM_PROC1 { get; set; }
        public System.String ITM_PROC2 { get; set; }
        public System.String ITM_PROC3 { get; set; }
        public System.String ITM_PROC4 { get; set; }
        public System.String ITM_PROC5 { get; set; }
        public System.String ITM_PROC6 { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.Decimal? COREWEIGHT { get; set; }
        public System.Decimal? FULLWEIGHT { get; set; }
        public System.String ITM_GROUP { get; set; }
        public System.String YARNCODE { get; set; }
        public System.String WIDTHCODE { get; set; }
        public System.Decimal? WIDTHWEAVING { get; set; }
        public System.String LABFORM { get; set; }
        public System.String WEAVE_TYPE { get; set; }

        public System.Boolean PROC1 { get; set; }
        public System.Boolean PROC2 { get; set; }
        public System.Boolean PROC3 { get; set; }
        public System.Boolean PROC4 { get; set; }
        public System.Boolean PROC5 { get; set; }
        public System.Boolean PROC6 { get; set; }
    }

    #endregion

    #region ItemCode Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class ItemCodeSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ItemCodeSession()
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
