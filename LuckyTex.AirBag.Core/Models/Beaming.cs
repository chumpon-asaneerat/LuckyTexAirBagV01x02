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

    #region ITM_GETITEMPREPARELIST 

    public class ITM_GETITEMPREPARELIST
    {
        public System.String ITM_PREPARE { get; set; }
        public System.String ITM_YARN { get; set; }
    }

    #endregion

    #region BEAM_GETSPECBYCHOPNO 

    public class BEAM_GETSPECBYCHOPNO
    {
        public System.String CHOPNO { get; set; }
        public System.Decimal? NOWARPBEAM { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.Decimal? TOTALKEBA { get; set; }
        public System.Decimal? BEAMLENGTH { get; set; }
        public System.Decimal? MAXHARDNESS { get; set; }
        public System.Decimal? MINHARDNESS { get; set; }
        public System.Decimal? MAXBEAMWIDTH { get; set; }
        public System.Decimal? MINBEAMWIDTH { get; set; }
        public System.Decimal? MAXSPEED { get; set; }
        public System.Decimal? MINSPEED { get; set; }
        public System.Decimal? MAXYARNTENSION { get; set; }
        public System.Decimal? MINYARNTENSION { get; set; }
        public System.Decimal? MAXWINDTENSION { get; set; }
        public System.Decimal? MINWINDTENSION { get; set; }
        public System.String COMBTYPE { get; set; }
        public System.Decimal? COMBPITCH { get; set; }
        public System.Decimal? TOTALBEAM { get; set; }
    }

    #endregion

    #region BEAM_GETWARPNOBYITEMPREPARE 

    public class BEAM_GETWARPNOBYITEMPREPARE
    {
        public System.Boolean IsSelect { get; set; }
        public System.String WARPHEADNO { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String WARPMC { get; set; }
        public System.Decimal? ACTUALCH { get; set; }
        public System.Decimal? TOTALBEAM { get; set; }
    }

    #endregion

    #region BEAM_GETWARPERLOT

    public class BEAM_GETWARPERLOT
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? OLDTOTALEND { get; set; }
        public System.Decimal? TOTALEND { get; set; }
        public System.Decimal? TAKEOUT { get; set; }
    }

    #endregion

    #region BEAM_GETSTOPREASONBYBEAMLOT 

    public class BEAM_GETSTOPREASONBYBEAMLOT
    {
        public System.String BEAMERNO { get; set; }
        public System.String BEAMLOT { get; set; }
        public System.String REASON { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String OPERATOR { get; set; }
        public System.String OTHERFLAG { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
    }

    #endregion

    #region BEAM_GETBEAMLOTBYBEAMERNO 

    public class BEAM_GETBEAMLOTBYBEAMERNO
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
        public System.Decimal? TENSION_ST1 { get; set; }
        public System.Decimal? TENSION_ST2 { get; set; }
        public System.Decimal? TENSION_ST3 { get; set; }
        public System.Decimal? TENSION_ST4 { get; set; }
        public System.Decimal? TENSION_ST5 { get; set; }
        public System.Decimal? TENSION_ST6 { get; set; }
        public System.Decimal? TENSION_ST7 { get; set; }
        public System.Decimal? TENSION_ST8 { get; set; }
        public System.Decimal? TENSION_ST9 { get; set; }
        public System.Decimal? TENSION_ST10 { get; set; }

        public System.String EDITBY { get; set; }
        public System.String OLDBEAMNO { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.Decimal? KEBA { get; set; }
        public System.Decimal? MISSYARN { get; set; }
        public System.Decimal? OTHER { get; set; }
    }

    #endregion

    #region BEAM_INSERTBEAMINGDETAIL 

    public class BEAM_INSERTBEAMINGDETAIL
    {
        public System.String R_BEAMLOT { get; set; }
        public System.String RESULT { get; set; }
    }

    #endregion

    #region BEAM_TRANFERSLIP

    public class BEAM_TRANFERSLIP
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
        public System.String ITM_PREPARE { get; set; }
    }

    #endregion

    #region BEAM_GETBEAMERMCSTATUS

    public class BEAM_GETBEAMERMCSTATUS
    {
        public System.String BEAMERNO { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.Decimal? TOTALKEBA { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String STATUS { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String MCNO { get; set; }
        public System.String WARPHEADNO { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? ADJUSTKEBA { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? NOWARPBEAM { get; set; }
        public System.Decimal? TOTALBEAM { get; set; }

        public System.String strSTATUS { get; set; }
    }

    #endregion

    #region BEAM_GETINPROCESSLOTBYBEAMNO

    public class BEAM_GETINPROCESSLOTBYBEAMNO
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
    }

    #endregion

    #region BEAM_SEARCHBEAMRECORD

    public class BEAM_SEARCHBEAMRECORD
    {
        public System.String BEAMERNO { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.Decimal? TOTALKEBA { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String STATUS { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String MCNO { get; set; }
        public System.String WARPHEADNO { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? ADJUSTKEBA { get; set; }
        public System.String REMARK { get; set; }
    }

    #endregion

    #region BEAM_GETWARPROLLBYBEAMERNO

    public class BEAM_GETWARPROLLBYBEAMERNO
    {
        public System.String BEAMERNO { get; set; }
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
    }

    #endregion

    //เพิ่ม 25/08/17
    #region BEAM_BEAMLIST

    public class BEAM_BEAMLIST
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
        public System.Decimal? TENSION_ST1 { get; set; }
        public System.Decimal? TENSION_ST2 { get; set; }
        public System.Decimal? TENSION_ST3 { get; set; }
        public System.Decimal? TENSION_ST4 { get; set; }
        public System.Decimal? TENSION_ST5 { get; set; }
        public System.Decimal? TENSION_ST6 { get; set; }
        public System.Decimal? TENSION_ST7 { get; set; }
        public System.Decimal? TENSION_ST8 { get; set; }
        public System.Decimal? TENSION_ST9 { get; set; }
        public System.Decimal? TENSION_ST10 { get; set; }
        public System.String EDITBY { get; set; }
        public System.String OLDBEAMNO { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String WARPHEADNO { get; set; }
        public System.Decimal? TOTALYARN { get; set; }
        public System.Decimal? TOTALKEBA { get; set; }
    }

    #endregion

    #region BeamingRecordHead

    public class BeamingRecordHead
    {
        public System.String BEAMERNO { get; set; }
        public System.String ITM_PREPARE { get; set; }

        public System.Decimal? TOTALYARN { get; set; }
        public System.Decimal? TOTALKEBA { get; set; }
        public System.Decimal? ADJUSTKEBA { get; set; }

        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }

        public System.String MCNO { get; set; }

        public System.String REMARK { get; set; }
    }

    #endregion

    #region BEAM_GETBEAMROLLDETAIL

    public class BEAM_GETBEAMROLLDETAIL
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
        public System.Decimal? TENSION_ST1 { get; set; }
        public System.Decimal? TENSION_ST2 { get; set; }
        public System.Decimal? TENSION_ST3 { get; set; }
        public System.Decimal? TENSION_ST4 { get; set; }
        public System.Decimal? TENSION_ST5 { get; set; }
        public System.Decimal? TENSION_ST6 { get; set; }
        public System.Decimal? TENSION_ST7 { get; set; }
        public System.Decimal? TENSION_ST8 { get; set; }
        public System.Decimal? TENSION_ST9 { get; set; }
        public System.Decimal? TENSION_ST10 { get; set; }
        public System.String EDITBY { get; set; }
        public System.String OLDBEAMNO { get; set; }
        public System.DateTime? EDITDATE { get; set; }
    }

    #endregion

    #region Beaming Session 

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class BeamingSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BeamingSession()
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

        #region Old
        //public List<WEAV_GETITEMWEAVINGLIST> GetItemCodeData()
        //{
        //    List<WEAV_GETITEMWEAVINGLIST> results = WeavingDataService.Instance
        //        .WEAV_GETITEMWEAVINGLIST();

        //    return results;
        //}
        #endregion

        public List<ITM_GETITEMPREPARELIST> GetItemCodeData()
        {
            List<ITM_GETITEMPREPARELIST> results = WarpingDataService.Instance
                .ITM_GETITEMPREPARELIST();

            return results;
        }

        public List<BeamingMCItem> GetBeamingMCData()
        {
            List<BeamingMCItem> results = BeamingDataService.Instance
                .GetMachinesData();

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
