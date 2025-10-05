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

    #region DRAW_GETSPECBYCHOPNO

    public class DRAW_GETSPECBYCHOPNO
    {
        public System.String CHOPNO { get; set; }
        public System.Decimal? NOYARN { get; set; }
        public System.Decimal? REEDTYPE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? NODENT { get; set; }
        public System.Decimal? PITCH { get; set; }
        public System.Decimal? AIRSPACE { get; set; }
    }

    #endregion

    #region DRAW_GETBEAMLOTDETAIL 

    public class DRAW_GETBEAMLOTDETAIL
    {
        public System.String BEAMERNO { get; set; }
        public System.String BEAMLOT { get; set; }
        public System.String BEAMNO { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? BEAMSTANDTENSION { get; set; }
        public System.Decimal? WINDINGTENSION { get; set; }
        public System.Decimal? HARDNESS_L { get; set; }
        public System.Decimal? HARDNESS_N { get; set; }
        public System.Decimal? HARDNESS_R { get; set; }
        public System.Decimal? INSIDE_WIDTH { get; set; }
        public System.Decimal? OUTSIDE_WIDTH { get; set; }
        public System.Decimal? FULL_WIDTH { get; set; }
        public System.String STARTBY { get; set; }
        public System.String DOFFBY { get; set; }
        public System.String BEAMMC { get; set; }
        public System.String FLAG { get; set; }
        public System.String REMARK { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.Decimal? TOTALYARN { get; set; }

        public System.String StrMsg { get; set; }
    }

    #endregion

    #region DRAW_GETDRAWINGLISTBYITEM 

    public class DRAW_GETDRAWINGLISTBYITEM
    {
        public System.String BEAMLOT { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String DRAWINGTYPE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDATE { get; set; }
        public System.String REEDNO { get; set; }
        public System.String HEALDCOLOR { get; set; }
        public System.String STARTBY { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String USEFLAG { get; set; }
        public System.Decimal? HEALDNO { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? REEDTYPE { get; set; }
        public System.Decimal? HEALDNO1 { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.String BEAMNO { get; set; }
    }

    #endregion

    // เพิ่มใหม่ 21/01/17
    #region DRAW_DAILYREPORT

    public class DRAW_DAILYREPORT
    {
        public System.Int32 No { get; set; }
        public System.String BEAMLOT { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String DRAWINGTYPE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDATE { get; set; }
        public System.String REEDNO { get; set; }
        public System.String HEALDCOLOR { get; set; }
        public System.String STARTBY { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String USEFLAG { get; set; }
        public System.Decimal? HEALDNO { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.String BEAMNO { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String BEAMERNO { get; set; }
    }

    #endregion
    
    // เพิ่มใหม่ 21/01/17
    #region DRAW_TRANSFERSLIP

    public class DRAW_TRANSFERSLIP
    {
        public System.String BEAMLOT { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String DRAWINGTYPE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDATE { get; set; }
        public System.String REEDNO { get; set; }
        public System.String HEALDCOLOR { get; set; }
        public System.String STARTBY { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String USEFLAG { get; set; }
        public System.Decimal? HEALDNO { get; set; }
        public System.String OPERATOR_GROUP { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.String BEAMNO { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String BEAMERNO { get; set; }
    }

    #endregion

    #region Drawing Session

    /// <summary>
    /// Drawing Session.
    /// </summary>
    [Serializable]
    public class DrawingSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DrawingSession()
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

        #region Load Combo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<ITM_GETITEMPREPARELIST> GetItemCodeData()
        {
            List<ITM_GETITEMPREPARELIST> results = DrawingDataService.Instance
                .ITM_GETITEMPREPARELIST();

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
