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
    #region WEAV_GREYROLLDAILYREPORT

    public class WEAV_GREYROLLDAILYREPORT
    {
        public System.String WEAVINGLOT { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String LOOMNO { get; set; }
        public System.DateTime? WEAVINGDATE { get; set; }
        public System.String SHIFT { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.String PREPAREBY { get; set; }
        public System.String WEAVINGNO { get; set; }
    }

    #endregion

    #region WEAV_GETITEMWEAVINGLIST 

    public class WEAV_GETITEMWEAVINGLIST
    {
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? WIDTHWEAVING { get; set; }
        public System.String WEAVE_TYPE { get; set; }
        public System.String ITM_YARN { get; set; }
    }

    #endregion

    #region WEAV_GETMACHINEZONELIST

    public class WEAV_GETMACHINEZONELIST
    {
        public System.String ZONE { get; set; }
        public System.String TYPE { get; set; }
        public System.Decimal? TOTAL { get; set; }
    }

    #endregion

    #region WEAVE_CHECKITEMPREPARE

    public class WEAVE_CHECKITEMPREPARE
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
    }

    #endregion

    #region WEAVE_GETBEAMLOTDETAIL 

    public class WEAVE_GETBEAMLOTDETAIL
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
        public System.Decimal? LENGTH { get; set; }

        //เพิ่ม 01/09/17
        public System.String RESULT { get; set; }
        
    }

    #endregion

    #region WEAVE_INSERTPROCESSSETTING

    public class WEAVE_INSERTPROCESSSETTING
    {
        public System.String RESULT { get; set; }
    }

    #endregion

    #region WEAV_WEAVINGINPROCESSLIST 

    public class WEAV_WEAVINGINPROCESSLIST
    {
        public System.String BEAMLOT { get; set; }
        public System.String MC { get; set; }
        public System.String REEDNO2 { get; set; }
        public System.String WEFTYARN { get; set; }
        public System.String TEMPLETYPE { get; set; }
        public System.String BARNO { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? FINISHDATE { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String SETTINGBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? WIDTH { get; set; }
    }

    #endregion

    #region WEAV_WEAVINGMCSTATUS

    public class WEAV_WEAVINGMCSTATUS
    {
        public System.String BEAMLOT { get; set; }
        public System.String MC { get; set; }
        public System.String REEDNO2 { get; set; }
        public System.String WEFTYARN { get; set; }
        public System.String TEMPLETYPE { get; set; }
        public System.String BARNO { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? FINISHDATE { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String SETTINGBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? BEAMLENGTH { get; set; }
        public System.String REEDNO { get; set; }
        public System.String HEALDCOLOR { get; set; }

        public System.Decimal? SPEED { get; set; }
    }

    #endregion

    #region WEAV_GETWEFTYARNLISTBYDOFFNO

    public class WEAV_GETWEFTYARNLISTBYDOFFNO
    {
        public System.String BEAMLOT { get; set; }
        public System.Decimal? DOFFNO { get; set; }
        public System.String PALLETNO { get; set; }
        public System.String CHLOTNO { get; set; }
        public System.DateTime? ADDDATE { get; set; }
        public System.String ADDBY { get; set; }
        public System.String USETYPE { get; set; }
        public System.String USETYPENAME { get; set; }
        public System.String REMARK { get; set; }
    }

    #endregion

    #region WEAV_GETWEAVELISTBYBEAMROLL

    public class WEAV_GETWEAVELISTBYBEAMROLL
    {
        public System.Int32 RowNo { get; set; }
        public System.String WEAVINGLOT { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String LOOMNO { get; set; }
        public System.DateTime? WEAVINGDATE { get; set; }
        public System.String SHIFT { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.String PREPAREBY { get; set; }
        public System.String WEAVINGNO { get; set; }
        public System.String BEAMLOT { get; set; }
        public System.Decimal? DOFFNO { get; set; }
        
        public System.Decimal? TENSION { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String DOFFBY { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? WASTE { get; set; }

        // เพิ่มใหม่
        public System.Decimal? DENSITY_WARP { get; set; }
        public System.Decimal? DENSITY_WEFT { get; set; }

        public System.String DELETEFLAG { get; set; }
        public System.String DELETEBY { get; set; }
        public System.DateTime? DELETEDATE { get; set; }

        public System.String DeleteHistory { get; set; }
    }

    #endregion

    #region WEAV_GETMCSTOPBYLOT

    public class WEAV_GETMCSTOPBYLOT
    {
        public System.String WEAVINGLOT { get; set; }
        public System.String DEFECTCODE { get; set; }
        public System.Decimal? DEFECTPOSITION { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String REMARK { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String BEAMERROLL { get; set; }
        public System.Decimal? DOFFNO { get; set; }
        public System.Decimal? DEFECTLENGTH { get; set; }
        public System.String DESCRIPTION { get; set; }
        public System.DateTime? WEAVSTARTDATE { get; set; }
        public System.DateTime? WEAVFINISHDATE { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? LENGTH { get; set; }
    }

    #endregion

    #region WEAV_GETINPROCESSBYBEAMROLL

    public class WEAV_GETINPROCESSBYBEAMROLL
    {
        public System.String WEAVINGLOT { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? LENGTH { get; set; }
        public System.String LOOMNO { get; set; }
        public System.DateTime? WEAVINGDATE { get; set; }
        public System.String SHIFT { get; set; }
        public System.String REMARK { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.String PREPAREBY { get; set; }
        public System.String WEAVINGNO { get; set; }
        public System.String BEAMLOT { get; set; }
        public System.Decimal? DOFFNO { get; set; }
        //public System.String DENSITY { get; set; }
        public System.Decimal? TENSION { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.String DOFFBY { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? WASTE { get; set; }

        // เพิ่มใหม่
        public System.Decimal? DENSITY_WARP { get; set; }
        public System.Decimal? DENSITY_WEFT { get; set; }
    }

    #endregion

    #region WEAVE_CHECKWEAVINGMC

    public class WEAVE_CHECKWEAVINGMC
    {
        public System.String MACHINEID { get; set; }
        public System.String PROCESSID { get; set; }
        public System.String MCNAME { get; set; }
        public System.String ZONE { get; set; }
        public System.Decimal? NO { get; set; }
        public System.String TYPE { get; set; }
    }

    #endregion

    #region WEAV_GETALLITEMWEAVING

    public class WEAV_GETALLITEMWEAVING
    {
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? WIDTHWEAVING { get; set; }
    }

    #endregion

    #region WEAV_GETSAMPLINGDATA

    public class WEAV_GETSAMPLINGDATA
    {
        public System.String BEAMERROLL { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.DateTime? SETTINGDATE { get; set; }
        public System.String BARNO { get; set; }
        public System.Decimal? SPIRAL_L { get; set; }
        public System.Decimal? SPIRAL_R { get; set; }
        public System.Decimal? STSAMPLING { get; set; }
        public System.Decimal? RECUTSAMPLING { get; set; }
        public System.String STSAMPLINGBY { get; set; }
        public System.String RECUTBY { get; set; }
        public System.DateTime? STDATE { get; set; }
        public System.DateTime? RECUTDATE { get; set; }
        public System.String REMARK { get; set; }

        public System.String BEAMMC { get; set; }
        public System.String WARPMC { get; set; }
        public System.String BEAMERNO { get; set; }
    }

    #endregion

    #region WEAV_DEFECTLIST

    public class WEAV_DEFECTLIST
    {
        public System.String DEFECTCODE { get; set; }
        public System.String DEFECTTYPE { get; set; }
        public System.String DESCRIPTION { get; set; }
        public System.String YARN { get; set; }

        public System.String DefectCodeName { get; set; }
    }

    #endregion

    #region WEAV_GETMCSTOPLISTBYDOFFNO

    public class WEAV_GETMCSTOPLISTBYDOFFNO
    {
        public System.String WEAVINGLOT { get; set; }
        public System.String DEFECTCODE { get; set; }
        public System.Decimal? DEFECTPOSITION { get; set; }
        public System.String CREATEBY { get; set; }
        public System.DateTime? CREATEDATE { get; set; }
        public System.String REMARK { get; set; }
        public System.String LOOMNO { get; set; }
        public System.String BEAMERROLL { get; set; }
        public System.Decimal? DOFFNO { get; set; }
        public System.Decimal? DEFECTLENGTH { get; set; }
        public System.String DESCRIPTION { get; set; }
        public System.DateTime? STOPDATE { get; set; }
    }

    #endregion

    #region WEAV_SEARCHPRODUCTION

    public class WEAV_SEARCHPRODUCTION
    {
        public System.String BEAMLOT { get; set; }
        public System.String MC { get; set; }
        public System.String REEDNO2 { get; set; }
        public System.String WEFTYARN { get; set; }
        public System.String TEMPLETYPE { get; set; }
        public System.String BARNO { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? FINISHDATE { get; set; }
        public System.String FINISHFLAG { get; set; }
        public System.String SETTINGBY { get; set; }
        public System.DateTime? EDITDATE { get; set; }
        public System.String EDITBY { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.Decimal? WIDTH { get; set; }
        public System.Decimal? BEAMLENGTH { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.String BEAMERNO { get; set; }
    }

    #endregion

    #region WEAV_SHIPMENTREPORT

    public class WEAV_SHIPMENTREPORT
    {
        public System.Int32 No { get; set; }
        public System.String ITM_WEAVING { get; set; }
        public System.Decimal? PIECES { get; set; }
        public System.Decimal? METERS { get; set; }
    }

    #endregion

    #region Weaving Session

    /// <summary>
    /// Weaving Session.
    /// </summary>
    [Serializable]
    public class WeavingSession
    {
        #region Internal Variables

        private LogInResult _currUser = null;
        private string _operator = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public WeavingSession()
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
        public List<WEAV_GETITEMWEAVINGLIST> GetItemCodeData()
        {
            List<WEAV_GETITEMWEAVINGLIST> results = WeavingDataService.Instance
                .WEAV_GETITEMWEAVINGLIST();

            return results;
        }

        public List<WEAV_GETALLITEMWEAVING> Weav_getAllItemWeaving()
        {
            List<WEAV_GETALLITEMWEAVING> results = WeavingDataService.Instance
                    .WEAV_GETALLITEMWEAVING();

            return results;
        }

        public List<WEAV_GETITEMWEAVINGLIST> GetItemCodeData(string type)
        {
            List<WEAV_GETITEMWEAVINGLIST> results = WeavingDataService.Instance
                .WEAV_GETITEMWEAVINGLIST(type);

            return results;
        }

        public List<WEAV_GETITEMWEAVINGLIST> GetItemCodeData(string ITM_WEAVING, string type)
        {
            List<WEAV_GETITEMWEAVINGLIST> results = WeavingDataService.Instance
                .WEAV_GETITEMWEAVINGLIST(ITM_WEAVING,type);

            return results;
        }

        public List<WEAV_DEFECTLIST> GetDefectData()
        {
            List<WEAV_DEFECTLIST> results = WeavingDataService.Instance
                .WEAV_DEFECTLIST();

            return results;
        }

        #endregion

        #region GetWIDTHWEAVING

        public decimal? GetWIDTHWEAVING(string ITM_WEAVING)
        {
            decimal? WIDTHWEAVING = 0;

            WIDTHWEAVING = WeavingDataService.Instance.GetItemWidth(ITM_WEAVING);

            return WIDTHWEAVING;
        }

        #endregion

        #region GetWEAV_GETCNTCHINALOT

        public string GetWEAV_GETCNTCHINALOT(string LOT)
        {
            string CNT = string.Empty;

            CNT = WeavingDataService.Instance.WEAV_GETCNTCHINALOT(LOT);

            return CNT;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล WeavingingData
        #region Load Weavinging

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WEAVINGLOT"></param>
        /// <returns></returns>
        public List<GETWEAVINGINGDATA> GetWeavingingDataList(string WEAVINGLOT)
        {
            List<GETWEAVINGINGDATA> results = CoatingDataService.Instance
                .GetWeavingingDataList(WEAVINGLOT);

            return results;
        }

        #endregion

        #region GetWEAVE_GETBEAMLOTDETAIL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEAMLOT"></param>
        /// <returns></returns>
        public List<WEAVE_GETBEAMLOTDETAIL> GetWEAVE_GETBEAMLOTDETAIL(string P_BEAMLOT)
        {
            List<WEAVE_GETBEAMLOTDETAIL> results = WeavingDataService.Instance
                .WEAVE_GETBEAMLOTDETAIL(P_BEAMLOT);

            return results;
        }

        #endregion

        #region GetWEAV_SHIPMENTREPORT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_BEGINDATE"></param>
        /// <param name="P_ENDDATE"></param>
        /// <returns></returns>
        public List<WEAV_SHIPMENTREPORT> GetWEAV_SHIPMENTREPORT(string P_BEGINDATE, string P_ENDDATE)
        {
            List<WEAV_SHIPMENTREPORT> results = WeavingDataService.Instance
                .WEAV_SHIPMENTREPORT(P_BEGINDATE, P_ENDDATE);

            return results;
        }

        #endregion

        #region GetWEAVE_CHECKITEMPREPARE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMWEAVING"></param>
        /// <param name="P_ITMPREPARE"></param>
        /// <returns></returns>
        public List<WEAVE_CHECKITEMPREPARE> GetWEAVE_CHECKITEMPREPARE(string P_ITMWEAVING, string P_ITMPREPARE)
        {
            List<WEAVE_CHECKITEMPREPARE> results = WeavingDataService.Instance
                .WEAVE_CHECKITEMPREPARE(P_ITMWEAVING, P_ITMPREPARE);

            return results;
        }

        #endregion

        #region Load Total

        public List<WEAV_GETMACHINEZONELIST> GetWeav_getMachineZoneList(string zone)
        {
            List<WEAV_GETMACHINEZONELIST> results = WeavingDataService.Instance
                .WEAV_GETMACHINEZONELIST(zone);

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
