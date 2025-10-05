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
    #region GETINSPECTIONLISTTESTBYITMCODE

    public class GETINSPECTIONLISTTESTBYITMCODE
    {
        public System.String ITM_CODE { get; set; }

        public System.String DENSITY_W { get; set; }
        public System.String DENSITY_F { get; set; }
        public System.String WIDTH_ALL { get; set; }
        public System.String WIDTH_PIN { get; set; }
        public System.String WIDTH_COAT { get; set; }
        public System.String WIDTH_SelvageL { get; set; }
        public System.String WIDTH_SelvageR { get; set; }

        public System.String TRIM_L { get; set; }
        public System.String TRIM_R { get; set; }
        public System.String FLOPPY_L { get; set; }
        public System.String FLOPPY_R { get; set; }
        public System.String HARDNESS_L { get; set; }
        public System.String HARDNESS_C { get; set; }
        public System.String HARDNESS_R { get; set; }
        public System.String UNWINDER { get; set; }
        public System.String WINDER { get; set; }


        public System.Boolean ChkDENSITY_W { get; set; }
        public System.Boolean ChkDENSITY_F { get; set; }
        public System.Boolean ChkWIDTH_ALL { get; set; }
        public System.Boolean ChkWIDTH_PIN { get; set; }
        public System.Boolean ChkWIDTH_COAT { get; set; }
        public System.Boolean ChkWIDTH_SelvageL { get; set; }
        public System.Boolean ChkWIDTH_SelvageR { get; set; }

        public System.Boolean ChkTRIM_L { get; set; }
        public System.Boolean ChkTRIM_R { get; set; }
        public System.Boolean ChkFLOPPY_L { get; set; }
        public System.Boolean ChkFLOPPY_R { get; set; }
        public System.Boolean ChkHARDNESS_L { get; set; }
        public System.Boolean ChkHARDNESS_C { get; set; }
        public System.Boolean ChkHARDNESS_R { get; set; }
        public System.Boolean ChkUNWINDER { get; set; }
        public System.Boolean ChkWINDER { get; set; }
    }

    #endregion

    #region  HundredMDataSession

    /// <summary>
    ///  HundredM Session.
    /// </summary>
    [Serializable]
    public class HundredMDataSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HundredMDataSession()
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
            List<ITM_GETITEMCODELIST> results = HundredMDataService.Instance
                .ITM_GETITEMCODELIST();

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
