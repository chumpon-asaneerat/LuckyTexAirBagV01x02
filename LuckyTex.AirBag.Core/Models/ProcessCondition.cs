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
    #region ProcessCondition Session

    /// <summary>
    /// ProcessCondition Session.
    /// </summary>
    [Serializable]
    public class ProcessConditionSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public ProcessConditionSession()
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
        #region Load Combo GetItemCodeData
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<ITM_GETITEMCODELIST> GetItemCodeData()
        {
            List<ITM_GETITEMCODELIST> results = ProcessConditionDataService.Instance
                .ITM_GETITEMCODELIST();

            return results;
        }

        #endregion

        #region Load Combo ITM_GETITEMPREPARELIST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<ITM_GETITEMPREPARELIST> ITM_GETITEMPREPARELIST()
        {
            List<ITM_GETITEMPREPARELIST> results = ProcessConditionDataService.Instance
                .ITM_GETITEMPREPARELIST();

            return results;
        }

        #endregion

        #region Load Combo ITM_GETITEMYARNLIST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<ITM_GETITEMYARNLIST> ITM_GETITEMYARNLIST()
        {
            List<ITM_GETITEMYARNLIST> results = ProcessConditionDataService.Instance
                .ITM_GETITEMYARNLIST();

            return results;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล FINISHING SCOURINGCONDITION
        #region Load FINISHING SCOURINGCONDITION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <param name="ScouringNo"></param>
        /// <returns></returns>
        public List<FINISHING_GETSCOURINGCONDITIONData> GetFINISHING_GETSCOURINGCONDITION(string itm_code, string ScouringNo)
        {
            List<FINISHING_GETSCOURINGCONDITIONData> results = CoatingDataService.Instance
                .GetFINISHING_GETSCOURINGCONDITIONDataList(itm_code, ScouringNo);

            return results;
        }

        #endregion

        #region Load finishing COATINGCONDITION
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <param name="CoatNo"></param>
        /// <returns></returns>
        public List<FINISHING_GETCOATINGCONDITIONData> GetFINISHING_GETCOATINGCONDITION(string itm_code, string CoatNo)
        {
            List<FINISHING_GETCOATINGCONDITIONData> results = CoatingDataService.Instance
                .GetFINISHING_GETCOATINGCONDITIONDataList(itm_code, CoatNo);

            return results;
        }

        #endregion

        #region Load finishing

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <returns></returns>
        public List<FINISHING_GETDRYERCONDITIONData> GetFINISHING_GETDRYERCONDITION(string itm_code, string mcno)
        {
            List<FINISHING_GETDRYERCONDITIONData> results = CoatingDataService.Instance
                .GetFINISHING_GETDRYERCONDITIONDataList(itm_code, mcno);

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
