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
    #region WARP_RECEIVEPALLET 

    public class WARP_RECEIVEPALLET
    {
        public System.Int32 RowNo { get; set; }
        public System.String P_ITMYARN { get; set; }
        public System.DateTime? P_RECEIVEDATE { get; set; }
        public System.String P_PALLETNO { get; set; }
        public System.Decimal? P_WEIGHT { get; set; }
        public System.Decimal? P_CH { get; set; }
        public System.String P_VERIFY { get; set; }
        public System.String P_REJECTID { get; set; }
        public System.String P_OPERATOR { get; set; }
        public System.Boolean Receive { get; set; }
        public System.Boolean Reject { get; set; }
    }

    #endregion

    #region ITM_GETITEMYARNLIST 

    public class ITM_GETITEMYARNLIST
    {
        public System.String ITM_YARN { get; set; }
    }

    #endregion

    #region WARP_GETSPECBYCHOPNOANDMC 

    public class WARP_GETSPECBYCHOPNOANDMC
    {
        public System.String CHOPNO { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.Decimal? WARPERENDS { get; set; }
        public System.Decimal? MAXLENGTH { get; set; }
        public System.Decimal? MINLENGTH { get; set; }
        public System.String WAXING { get; set; }
        public System.String COMBTYPE { get; set; }
        public System.String COMBPITCH { get; set; }
        public System.Decimal? KEBAYARN { get; set; }
        public System.Decimal? NOWARPBEAM { get; set; }
        public System.Decimal? MAXHARDNESS { get; set; }
        public System.Decimal? MINHARDNESS { get; set; }
        public System.String MCNO { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SPEED_MARGIN { get; set; }
        public System.Decimal? YARN_TENSION { get; set; }
        public System.Decimal? YARN_TENSION_MARGIN { get; set; }
        public System.Decimal? WINDING_TENSION { get; set; }
        public System.Decimal? WINDING_TENSION_MARGIN { get; set; }
        public System.Decimal? NOCH { get; set; }
    }

    #endregion

    #region WARP_PALLETLISTBYITMYARN 

    public class WARP_PALLETLISTBYITMYARN
    {
        public System.Boolean IsSelect { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String PALLETNO { get; set; }
        public System.Decimal? RECEIVEWEIGHT { get; set; }
        public System.Decimal? RECEIVECH { get; set; }
        public System.Decimal? USEDWEIGHT { get; set; }
        public System.Decimal? USEDCH { get; set; }
        public System.String VERIFY { get; set; }
        public System.String REJECTID { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String RETURNFLAG { get; set; }
        public System.Decimal? REJECTCH { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }

        public System.String CLEARBY { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CLEARDATE { get; set; }

        public System.Decimal? NoCH { get; set; }
        public System.Decimal? Use { get; set; }
        public System.Decimal? Reject { get; set; }
        public System.Decimal? Remain { get; set; }
    }

    #endregion

    #region WARP_GETWARPERMCSTATUS 

    public class WARP_GETWARPERMCSTATUS
    {
        public System.String WARPHEADNO { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String WARPMC { get; set; }
        public System.String SIDE { get; set; }
        public System.Decimal? ACTUALCH { get; set; }
        public System.String WTYPE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CONDITIONSTART { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String STATUS { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String CONDITIONING { get; set; }
    }

    #endregion

    #region WARP_GETWARPERLOTBYHEADNO 

    public class WARP_GETWARPERLOTBYHEADNO
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.String BEAMNO { get; set; }
        public System.String SIDE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? HARDNESS_L { get; set; }
        public System.Decimal? HARDNESS_N { get; set; }
        public System.Decimal? HARDNESS_R { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.String STARTBY { get; set; }
        public System.String DOFFBY { get; set; }
        public System.String FLAG { get; set; }
        public System.String WARPMC { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? TENSION_IT { get; set; }
        public System.Decimal? TENSION_TAKEUP { get; set; }
        public System.Decimal? MC_COUNT_L { get; set; }
        public System.Decimal? MC_COUNT_S { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.Decimal? KEBA { get; set; }
        public System.Decimal? TIGHTEND { get; set; }
        public System.Decimal? MISSYARN { get; set; }
        public System.Decimal? OTHER { get; set; }
    }

    #endregion

    #region WARP_INSERTWARPINGPROCESS 

    public class WARP_INSERTWARPINGPROCESS
    {
        public System.String R_WRAPLOT { get; set; }
        public System.String RESULT { get; set; }
    }

    #endregion

    #region WARP_TRANFERSLIP

    public class WARP_TRANFERSLIP
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.String BEAMNO { get; set; }
        public System.String SIDE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? HARDNESS_L { get; set; }
        public System.Decimal? HARDNESS_N { get; set; }
        public System.Decimal? HARDNESS_R { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.String STARTBY { get; set; }
        public System.String DOFFBY { get; set; }
        public System.String FLAG { get; set; }
        public System.String WARPMC { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? TENSION_IT { get; set; }
        public System.Decimal? TENSION_TAKEUP { get; set; }
        public System.Decimal? MC_COUNT_L { get; set; }
        public System.Decimal? MC_COUNT_S { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String ITM_YARN { get; set; }


    }

    #endregion

    #region WARP_GETREMAINPALLET 

    public class WARP_GETREMAINPALLET
    {
        public System.String ITM_YARN { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String PALLETNO { get; set; }
        public System.Decimal? RECEIVEWEIGHT { get; set; }
        public System.Decimal? RECEIVECH { get; set; }
        public System.Decimal? USEDWEIGHT { get; set; }
        public System.Decimal? USEDCH { get; set; }
        public System.String VERIFY { get; set; }
        public System.String REJECTID { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String RETURNFLAG { get; set; }
        public System.Decimal? REJECTCH { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.String CLEARBY { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CLEARDATE { get; set; }
        public System.Decimal? REMAINCH { get; set; }
    }

    #endregion

    #region WARP_GETSTOPREASONBYWARPERLOT 

    public class WARP_GETSTOPREASONBYWARPERLOT
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.String REASON { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String OPERATOR { get; set; }
        public System.String OTHERFLAG { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
    }

    #endregion

    #region WARP_GETINPROCESSLOTBYHEADNO 

    public class WARP_GETINPROCESSLOTBYHEADNO
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.String BEAMNO { get; set; }
        public System.String SIDE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? HARDNESS_L { get; set; }
        public System.Decimal? HARDNESS_N { get; set; }
        public System.Decimal? HARDNESS_R { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.String STARTBY { get; set; }
        public System.String DOFFBY { get; set; }
        public System.String FLAG { get; set; }
        public System.String WARPMC { get; set; }
        public System.String REMARK { get; set; }
    }

    #endregion

    #region WARP_GETCREELSETUPSTATUS

    public class WARP_GETCREELSETUPSTATUS
    {
        public System.String WARPHEADNO { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String WARPMC { get; set; }
        public System.String SIDE { get; set; }
        public System.Decimal? ACTUALCH { get; set; }
        public System.String WTYPE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CONDITIONSTART { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String STATUS { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String REEDNO { get; set; }
        public System.String EDITBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }

        public System.String strSTATUS { get; set; }
    }

    #endregion

    #region WARP_GETCREELSETUPDETAIL

    public class WARP_GETCREELSETUPDETAIL
    {
        public System.Boolean IsSelect { get; set; }
        public System.String PALLETNO { get; set; }
        public System.Decimal? RECEIVECH { get; set; }
        public System.Decimal? USEDCH { get; set; }
        public System.Decimal? REJECTCH { get; set; }
        public System.Decimal? PREJECT { get; set; }
        public System.String ITM_YARN { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.Decimal? PUSED { get; set; }

        public System.Decimal? NoCH { get; set; }
        public System.Decimal? Use { get; set; }
        public System.Decimal? Reject { get; set; }
        public System.Decimal? Remain { get; set; }
    }

    #endregion

    #region WARP_SEARCHWARPRECORD

    public class WARP_SEARCHWARPRECORD
    {
        public System.String WARPHEADNO { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String WARPMC { get; set; }
        public System.String SIDE { get; set; }
        public System.Decimal? ACTUALCH { get; set; }
        public System.String WTYPE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CONDITIONSTART { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.String STATUS { get; set; }
        public System.String FINISHBY { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String REEDNO { get; set; }
        public System.String EDITBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }
    }

    #endregion

    //เพิ่มใหม่ 25/08/17
    #region WARP_WARPLIST

    public class WARP_WARPLIST
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.String BEAMNO { get; set; }
        public System.String SIDE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? HARDNESS_L { get; set; }
        public System.Decimal? HARDNESS_N { get; set; }
        public System.Decimal? HARDNESS_R { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.String STARTBY { get; set; }
        public System.String DOFFBY { get; set; }
        public System.String FLAG { get; set; }
        public System.String WARPMC { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? TENSION_IT { get; set; }
        public System.Decimal? TENSION_TAKEUP { get; set; }
        public System.Decimal? MC_COUNT_L { get; set; }
        public System.Decimal? MC_COUNT_S { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String ITM_PREPARE { get; set; }
        public System.String ITM_YARN { get; set; }
    }

    #endregion

    #region WarpingRecordHead

    public class WarpingRecordHead
    {
        public System.String WARPHEADNO { get; set; }
        public System.String SIDE { get; set; }
        public System.String WARPMC { get; set; }
        public System.String ITM_PREPARE { get; set; }

        public System.String WTYPE { get; set; }
        public System.String CONDITIONBY { get; set; }
        public System.String REEDNO { get; set; }
        public System.DateTime? CONDITIONSTART { get; set; }
    }

    #endregion

    #region WARP_GETWARPERROLLDETAIL

    public class WARP_GETWARPERROLLDETAIL
    {
        public System.String WARPHEADNO { get; set; }
        public System.String WARPERLOT { get; set; }
        public System.String BEAMNO { get; set; }
        public System.String SIDE { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? HARDNESS_L { get; set; }
        public System.Decimal? HARDNESS_N { get; set; }
        public System.Decimal? HARDNESS_R { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.String STARTBY { get; set; }
        public System.String DOFFBY { get; set; }
        public System.String FLAG { get; set; }
        public System.String WARPMC { get; set; }
        public System.String REMARK { get; set; }
        public System.Decimal? TENSION_IT { get; set; }
        public System.Decimal? TENSION_TAKEUP { get; set; }
        public System.Decimal? MC_COUNT_L { get; set; }
        public System.Decimal? MC_COUNT_S { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
    }

    #endregion

    #region WARP_CHECKPALLET

    public class WARP_CHECKPALLET
    {
        public System.String ITM_YARN { get; set; }
        public System.DateTime? RECEIVEDATE { get; set; }
        public System.String PALLETNO { get; set; }
        public System.Decimal? RECEIVEWEIGHT { get; set; }
        public System.Decimal? RECEIVECH { get; set; }
        public System.Decimal? USEDWEIGHT { get; set; }
        public System.Decimal? USEDCH { get; set; }
        public System.String VERIFY { get; set; }
        public System.String REJECTID { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String RETURNFLAG { get; set; }
        public System.Decimal? REJECTCH { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String CREATEBY { get; set; }
        public System.String CLEARBY { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CLEARDATE { get; set; }
        public System.Decimal? KGPERCH { get; set; }
    }

    #endregion

    #region Warping Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class WarpingSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WarpingSession()
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
        public List<ITM_GETITEMPREPARELIST> GetItemCodeData()
        {
            List<ITM_GETITEMPREPARELIST> results = WarpingDataService.Instance
                .ITM_GETITEMPREPARELIST();

            return results;
        }

        #endregion

        #region Load Combo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cusID"></param>
        /// <returns></returns>
        public List<ITM_GETITEMYARNLIST> GetItemCodeYarnData()
        {
            List<ITM_GETITEMYARNLIST> results = WarpingDataService.Instance
                .ITM_GETITEMYARNLIST();

            return results;
        }

        #endregion

        #region Load Combo Machines
        /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
        public List<WarpingMCItem> GetMachines()
        {
            List<WarpingMCItem> results = WarpingDataService.Instance
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
