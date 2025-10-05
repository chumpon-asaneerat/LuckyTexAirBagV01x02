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
    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUT_GETFINISHINGDATA
    #region CUT_GETFINISHINGDATA

    /// <summary>
    /// The CUT_GETFINISHINGDATA class.
    /// </summary>
    public class CUT_GETFINISHINGDATA
    {
        #region Public Propeties

        public System.String ITEMCODE { get; set; }
        public System.String ITEMLOT { get; set; }
        public System.String BATCHNO { get; set; }
        public System.String FINISHINGLOT { get; set; }
        public System.String PRODUCTTYPEID { get; set; }

        //เพิ่มใหม่ 20/01/16
        public System.String FINISHINGPROCESS { get; set; }

        //เพิ่มใหม่ 27/06/16
        public System.String SND_BARCODE { get; set; }

        //เพิ่มใหม่ 10/12/16
        public System.String CUSTOMERID { get; set; }

        //เพิ่มใหม่ 04/10/17
        public System.Decimal? BEFORE_WIDTH { get; set; }

        #endregion
    }

    #endregion

    // เพิ่มขึ้นมาใหม่ เพื่อใช้งานกับ CUT SLIP Report
    #region CUT_GETSLIPReport

    /// <summary>
    /// The Inspection Report Data class.
    /// </summary>
    [Serializable]
    public class CUT_GETSLIPReport
    {
        #region Public Propeties

        public System.String ITEMLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? WIDTHBARCODE1 { get; set; }
        public System.Decimal? WIDTHBARCODE2 { get; set; }
        public System.Decimal? WIDTHBARCODE3 { get; set; }
        public System.Decimal? WIDTHBARCODE4 { get; set; }
        public System.Decimal? DISTANTBARCODE1 { get; set; }
        public System.Decimal? DISTANTBARCODE2 { get; set; }
        public System.Decimal? DISTANTBARCODE3 { get; set; }
        public System.Decimal? DISTANTBARCODE4 { get; set; }
        public System.Decimal? DISTANTLINE1 { get; set; }
        public System.Decimal? DISTANTLINE2 { get; set; }
        public System.Decimal? DISTANTLINE3 { get; set; }
        public System.Decimal? DENSITYWARP { get; set; }
        public System.Decimal? DENSITYWEFT { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? BEFORE_WIDTH { get; set; }
        public System.Decimal? AFTER_WIDTH { get; set; }
        public System.String BEGINROLL_LINE1 { get; set; }
        public System.String BEGINROLL_LINE2 { get; set; }
        public System.String BEGINROLL_LINE3 { get; set; }
        public System.String BEGINROLL_LINE4 { get; set; }
        public System.String ENDROLL_LINE1 { get; set; }
        public System.String ENDROLL_LINE2 { get; set; }
        public System.String ENDROLL_LINE3 { get; set; }
        public System.String ENDROLL_LINE4 { get; set; }
        public System.String OPERATORID { get; set; }
        public System.String SELVAGE_LEFT { get; set; }
        public System.String SELVAGE_RIGHT { get; set; }
        public System.String REMARK { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String MCNO { get; set; }
        public System.String FINISHLOT { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String ITEMLOT1 { get; set; }
        public System.String PARTNO { get; set; }
        public System.String MCNAME { get; set; }

        //เพิ่มใหม่ 20/01/16
        public System.String FINISHINGPROCESS { get; set; }

        //เพิ่มใหม่ 03/05/16
        public System.String STATUS { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? CLEARDATE { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String CLEARBY { get; set; }
        public System.Decimal? LENGTHPRINT { get; set; }
        public System.DateTime? SUSPENDSTARTDATE { get; set; }

        //เพิ่มใหม่ 06/05/16
        public System.String SHOWSELVAGE { get; set; }

        //เพิ่มใหม่ 27/06/16
        public System.String BEGINROLL2_LINE1 { get; set; }
        public System.String BEGINROLL2_LINE2 { get; set; }
        public System.String BEGINROLL2_LINE3 { get; set; }
        public System.String BEGINROLL2_LINE4 { get; set; }
        public System.String ENDROLL2_LINE1 { get; set; }
        public System.String ENDROLL2_LINE2 { get; set; }
        public System.String ENDROLL2_LINE3 { get; set; }
        public System.String ENDROLL2_LINE4 { get; set; }

        public System.String SND_BARCODE { get; set; }

        //เพิ่มใหม่ 10/12/16
        public System.String CUSTOMERID { get; set; }

        //เพิ่ม 28/06/17
        public System.Decimal? TENSION { get; set; }

        //เพิ่มใหม่ 13/07/17
        public System.String FINISHLENGTH { get; set; }

        //เพิ่มใหม่ 28/09/17
        public System.String LENGTHDETAIL { get; set; }

        public System.Decimal? AFTER_WIDTH_END { get; set; }

        #endregion
    }

    #endregion

    #region CUT_GETMCSUSPENDDATA

    public class CUT_GETMCSUSPENDDATA
    {
        public System.String ITEMLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? WIDTHBARCODE1 { get; set; }
        public System.Decimal? WIDTHBARCODE2 { get; set; }
        public System.Decimal? WIDTHBARCODE3 { get; set; }
        public System.Decimal? WIDTHBARCODE4 { get; set; }
        public System.Decimal? DISTANTBARCODE1 { get; set; }
        public System.Decimal? DISTANTBARCODE2 { get; set; }
        public System.Decimal? DISTANTBARCODE3 { get; set; }
        public System.Decimal? DISTANTBARCODE4 { get; set; }
        public System.Decimal? DISTANTLINE1 { get; set; }
        public System.Decimal? DISTANTLINE2 { get; set; }
        public System.Decimal? DISTANTLINE3 { get; set; }
        public System.Decimal? DENSITYWARP { get; set; }
        public System.Decimal? DENSITYWEFT { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? BEFORE_WIDTH { get; set; }
        public System.Decimal? AFTER_WIDTH { get; set; }
        public System.String BEGINROLL_LINE1 { get; set; }
        public System.String BEGINROLL_LINE2 { get; set; }
        public System.String BEGINROLL_LINE3 { get; set; }
        public System.String BEGINROLL_LINE4 { get; set; }
        public System.String ENDROLL_LINE1 { get; set; }
        public System.String ENDROLL_LINE2 { get; set; }
        public System.String ENDROLL_LINE3 { get; set; }
        public System.String ENDROLL_LINE4 { get; set; }
        public System.String OPERATORID { get; set; }
        public System.String SELVAGE_LEFT { get; set; }
        public System.String SELVAGE_RIGHT { get; set; }
        public System.String REMARK { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String MCNO { get; set; }
        public System.String STATUS { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? CLEARDATE { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String CLEARBY { get; set; }
        public System.Decimal? LENGTHPRINT { get; set; }
        public System.DateTime? SUSPENDSTARTDATE { get; set; }

        //เพิ่มใหม่ 27/06/16
        public System.String BEGINROLL2_LINE1 { get; set; }
        public System.String BEGINROLL2_LINE2 { get; set; }
        public System.String BEGINROLL2_LINE3 { get; set; }
        public System.String BEGINROLL2_LINE4 { get; set; }
        public System.String ENDROLL2_LINE1 { get; set; }
        public System.String ENDROLL2_LINE2 { get; set; }
        public System.String ENDROLL2_LINE3 { get; set; }
        public System.String ENDROLL2_LINE4 { get; set; }

        //เพิ่มใหม่ 28/06/17
        public System.Decimal? TENSION { get; set; }

        //เพิ่มใหม่ 25/08/17
        public System.String LENGTHDETAIL = null;

        //เพิ่มใหม่ 04/10/17
        public System.Decimal? AFTER_WIDTH_END { get; set; }
    }

    #endregion

    #region CUT_GETCONDITIONBYITEMCODE

    public class CUT_GETCONDITIONBYITEMCODE
    {
        public System.String ITM_CODE { get; set; }
        public System.Decimal? WIDTHBARCODE_MIN { get; set; }
        public System.Decimal? WIDTHBARCODE_MAX { get; set; }
        public System.Decimal? DISTANTBARCODE_MIN { get; set; }
        public System.Decimal? DISTANTBARCODE_MAX { get; set; }
        public System.Decimal? DISTANTLINE_MIN { get; set; }
        public System.Decimal? DISTANTLINE_MAX { get; set; }
        public System.Decimal? DENSITYWARP_MIN { get; set; }
        public System.Decimal? DENSITYWARP_MAX { get; set; }
        public System.Decimal? DENSITYWEFT_MIN { get; set; }
        public System.Decimal? DENSITYWEFT_MAX { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? SPEED_MARGIN { get; set; }
        public System.Decimal? AFTER_WIDTH { get; set; }

        public System.String SHOWSELVAGE { get; set; }

        public System.String strWIDTHBARCODE { get; set; }
        public System.String strDISTANTBARCODE { get; set; }
        public System.String strDISTANTLINE { get; set; }
        public System.String strDENSITYWARP { get; set; }
        public System.String strDENSITYWEFT { get; set; }
        public System.String strSPEED { get; set; }
        public System.String strAFTER { get; set; }

    }

    #endregion

    #region CUT_SERACHLIST

    public class CUT_SERACHLIST
    {
        public System.String ITEMLOT { get; set; }
        public System.DateTime? STARTDATE { get; set; }
        public System.DateTime? ENDDATE { get; set; }
        public System.Decimal? WIDTHBARCODE1 { get; set; }
        public System.Decimal? WIDTHBARCODE2 { get; set; }
        public System.Decimal? WIDTHBARCODE3 { get; set; }
        public System.Decimal? WIDTHBARCODE4 { get; set; }
        public System.Decimal? DISTANTBARCODE1 { get; set; }
        public System.Decimal? DISTANTBARCODE2 { get; set; }
        public System.Decimal? DISTANTBARCODE3 { get; set; }
        public System.Decimal? DISTANTBARCODE4 { get; set; }
        public System.Decimal? DISTANTLINE1 { get; set; }
        public System.Decimal? DISTANTLINE2 { get; set; }
        public System.Decimal? DISTANTLINE3 { get; set; }
        public System.Decimal? DENSITYWARP { get; set; }
        public System.Decimal? DENSITYWEFT { get; set; }
        public System.Decimal? SPEED { get; set; }
        public System.Decimal? BEFORE_WIDTH { get; set; }
        public System.Decimal? AFTER_WIDTH { get; set; }
        public System.String BEGINROLL_LINE1 { get; set; }
        public System.String BEGINROLL_LINE2 { get; set; }
        public System.String BEGINROLL_LINE3 { get; set; }
        public System.String BEGINROLL_LINE4 { get; set; }
        public System.String ENDROLL_LINE1 { get; set; }
        public System.String ENDROLL_LINE2 { get; set; }
        public System.String ENDROLL_LINE3 { get; set; }
        public System.String ENDROLL_LINE4 { get; set; }
        public System.String OPERATORID { get; set; }
        public System.String SELVAGE_LEFT { get; set; }
        public System.String SELVAGE_RIGHT { get; set; }
        public System.String REMARK { get; set; }
        public System.String PRODUCTTYPEID { get; set; }
        public System.String MCNO { get; set; }
        public System.String ITEMCODE { get; set; }
        public System.String PARTNO { get; set; }
        public System.String MCNAME { get; set; }

        //เพิ่มใหม่ 27/06/16
        public System.String BEGINROLL2_LINE1 { get; set; }
        public System.String BEGINROLL2_LINE2 { get; set; }
        public System.String BEGINROLL2_LINE3 { get; set; }
        public System.String BEGINROLL2_LINE4 { get; set; }
        public System.String ENDROLL2_LINE1 { get; set; }
        public System.String ENDROLL2_LINE2 { get; set; }
        public System.String ENDROLL2_LINE3 { get; set; }
        public System.String ENDROLL2_LINE4 { get; set; }

        public System.String SND_BARCODE { get; set; }

        //เพิ่มใหม่ 28/06/17
        public System.Decimal? TENSION { get; set; }
        public System.String FINISHLOT { get; set; }
        public System.Decimal? FINISHLENGTH { get; set; }

        public System.String STATUS { get; set; }
        public System.DateTime? SUSPENDDATE { get; set; }
        public System.String SUSPENDBY { get; set; }
        public System.DateTime? CLEARDATE { get; set; }
        public System.String CLEARREMARK { get; set; }
        public System.String CLEARBY { get; set; }
        public System.Decimal? LENGTHPRINT { get; set; }
        public System.DateTime? SUSPENDSTARTDATE { get; set; }

        //เพิ่มใหม่ 25/08/17
        public System.String LENGTHDETAIL { get; set; }
        public System.String FINISHLENGTH1 { get; set; }
    }

    #endregion

    #region CutPrint Session

    /// <summary>
    /// Finishing Session.
    /// </summary>
    [Serializable]
    public class CutPrintSession
    {
        #region Internal Variables

        private CutPrintMCItem _machine = null;
        private LogInResult _currUser = null;
        private string _operator = string.Empty;
        private string _MCNO = string.Empty;

        private string _ITEMCODE = string.Empty;
        private string _ITEMLOT = string.Empty;
        private string _BATCHNO = string.Empty;
        private string _FINISHINGLOT = string.Empty;
        private string _PRODUCTTYPEID = string.Empty;

        private DateTime? _STARTDATE = null;
        private DateTime? _ENDDATE = null;
        private string _REMARK = string.Empty;
        private decimal? _WIDTH1 = null;
        private decimal? _WIDTH2 = null;
        private decimal? _WIDTH3 = null;
        private decimal? _WIDTH4 = null;
        private decimal? _DISTANTBAR1 = null;
        private decimal? _DISTANTBAR2 = null;
        private decimal? _DISTANTBAR3 = null;
        private decimal? _DISTANTBAR4 = null;
        private decimal? _DISTANTLINE1 = null;
        private decimal? _DISTANTLINE2 = null;
        private decimal? _DISTANTLINE3 = null;
        private decimal? _DENWARP = null;
        private decimal? _DENWEFT = null;
        private decimal? _SPEED = null;
        private decimal? _WIDTHBE = null;
        private decimal? _WIDTHAF = null;

        //เพิ่มใหม่ 04/10/17
        private decimal? _WIDTHAF_END = null;

        private string _BEGINLINE1 = string.Empty;
        private string _BEGINLINE2 = string.Empty;
        private string _BEGINLINE3 = string.Empty;
        private string _BEGINLINE4 = string.Empty;
        private string _ENDLINE1 = string.Empty;
        private string _ENDLINE2 = string.Empty;
        private string _ENDLINE3 = string.Empty;
        private string _ENDLINE4 = string.Empty;
        private string _SELVAGELEFT = string.Empty;
        private string _SELVAGERIGHT = string.Empty;

        // เพิ่ม 20/01/16
        private string _FINISHINGPROCESS = string.Empty;

        // เพิ่ม 03/05/16
        private string _STATUS = string.Empty;
        private decimal? _LENGTHPRINT = null;
        private string _CLEARBY = string.Empty;
        private string _CLEARREMARK = string.Empty;
        private DateTime? _CLEARDATE = null;
        private DateTime? _SUSPENDDATE = null;

        // เพิ่ม 06/05/16
        private string _SHOWSELVAGE = string.Empty;

        // เพิ่ม 27/06/16
        private string _P_2BEGINLINE1 = string.Empty;
        private string _P_2BEGINLINE2 = string.Empty;
        private string _P_2BEGINLINE3 = string.Empty;
        private string _P_2BEGINLINE4 = string.Empty;

        private string _P_2ENDLINE1 = string.Empty;
        private string _P_2ENDLINE2 = string.Empty;
        private string _P_2ENDLINE3 = string.Empty;
        private string _P_2ENDLINE4 = string.Empty;

        //เพิ่มใหม่ 28/06/17
        private decimal? _TENSION = null;

        //เพิ่มใหม่ 25/08/17
        private string _LENGTHDETAIL = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CutPrintSession()
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
        /// Init current Session.
        /// </summary>
        /// <param name="machine">The selected machine.</param>
        /// <param name="currUser">The current user that operate machine.</param>
        public void Init(CutPrintMCItem machine, LogInResult currUser)
        {
            _machine = machine;
            _currUser = currUser;
        }

        #endregion

        // ใช้สำหรับ Load ข้อมูล CUT_GETFINISHINGDATA 
        #region Load CUT_GETFINISHINGDATA

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itm_code"></param>
        /// <returns></returns>
        public List<CUT_GETFINISHINGDATA> GetCUT_GETFINISHINGDATA(string ITEMLOT)
        {
            List<CUT_GETFINISHINGDATA> results = CutPrintDataService.Instance
                .GetCUT_GETFINISHINGDATAList(ITEMLOT);

            return results;
        }

        #endregion

        #region Load CUT_GETCONDITIONBYITEMCODE

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_ITMCODE"></param>
        /// <returns></returns>
        public List<CUT_GETCONDITIONBYITEMCODE> GetCUT_GETCONDITIONBYITEMCODE(string P_ITMCODE)
        {
            List<CUT_GETCONDITIONBYITEMCODE> results = CutPrintDataService.Instance
                .CUT_GETCONDITIONBYITEMCODE(P_ITMCODE);

            return results;
        }

        #endregion

        #region Load GetCutPrintMCItem

        public List<CutPrintMCItem> GetCutPrintMCItem()
        {
            List<CutPrintMCItem> results = CutPrintDataService.Instance
                .GetCutPrintMCItem();

            return results;
        }

        #endregion

        #region Load CUT_SERACHLIST

        /// <summary>
        /// 
        /// </summary>
        /// <param name="P_STARTDATE"></param>
        /// <param name="P_MC"></param>
        /// <returns></returns>
        public List<CUT_SERACHLIST> GetCUT_SERACHLIST(string P_STARTDATE, string P_MC)
        {
            List<CUT_SERACHLIST> results = CutPrintDataService.Instance
                .CUT_SERACHLIST(P_STARTDATE, P_MC);

            return results;
        }

        #endregion

        // ใช้สำหรับ CUT_INSERTDATA
        #region CUT_INSERTDATA

        public bool CUT_INSERTDATA()
        {
            bool chkSave = false;

            try
            {
                chkSave = CutPrintDataService.Instance.CUT_INSERTDATA(_ITEMLOT, _STARTDATE, _PRODUCTTYPEID, _operator,
                    _REMARK, _MCNO, _WIDTH1, _WIDTH2, _WIDTH3, _WIDTH4,
                    _DISTANTBAR1, _DISTANTBAR2, _DISTANTBAR3, _DISTANTBAR4,
                    _DISTANTLINE1, _DISTANTLINE2, _DISTANTLINE3, _DENWARP, _DENWEFT,
                    _SPEED, _WIDTHBE, _WIDTHAF,
                    _BEGINLINE1, _BEGINLINE2, _BEGINLINE3, _BEGINLINE4,
                    _ENDLINE1, _ENDLINE2, _ENDLINE3, _ENDLINE4,
                    _SELVAGELEFT, _SELVAGERIGHT, _FINISHINGPROCESS, _SUSPENDDATE,
                    _P_2BEGINLINE1, _P_2BEGINLINE2, _P_2BEGINLINE3, _P_2BEGINLINE4,
                    _P_2ENDLINE1, _P_2ENDLINE2, _P_2ENDLINE3, _P_2ENDLINE4, _TENSION, _LENGTHDETAIL, _WIDTHAF_END);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return chkSave;
        }

        #endregion

        // ใช้สำหรับ CUT_UPDATEDATA
        #region CUT_UPDATEDATA

        public bool CUT_UPDATEDATA()
        {
            bool chkSave = false;

            try
            {
                chkSave = CutPrintDataService.Instance.CUT_UPDATEDATA(_ITEMLOT, _ENDDATE,
                    _REMARK, _WIDTH1, _WIDTH2, _WIDTH3, _WIDTH4,
                    _DISTANTBAR1, _DISTANTBAR2, _DISTANTBAR3, _DISTANTBAR4,
                    _DISTANTLINE1, _DISTANTLINE2, _DISTANTLINE3, _DENWARP, _DENWEFT,
                    _SPEED, _WIDTHBE, _WIDTHAF,
                    _BEGINLINE1, _BEGINLINE2, _BEGINLINE3, _BEGINLINE4,
                    _ENDLINE1, _ENDLINE2, _ENDLINE3, _ENDLINE4,
                    _SELVAGELEFT, _SELVAGERIGHT, _FINISHINGPROCESS, _STATUS, _LENGTHPRINT,
                    _P_2BEGINLINE1, _P_2BEGINLINE2, _P_2BEGINLINE3, _P_2BEGINLINE4,
                    _P_2ENDLINE1, _P_2ENDLINE2, _P_2ENDLINE3, _P_2ENDLINE4, _TENSION, _LENGTHDETAIL, _WIDTHAF_END);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return chkSave;
        }

        #endregion

        #region Suspend

        public bool Suspend()
        {
            bool chkSave = false;

            try
            {
                chkSave = CutPrintDataService.Instance.CUT_UPDATEDATA(_ITEMLOT,
                    _REMARK, _WIDTH1, _WIDTH2, _WIDTH3, _WIDTH4,
                    _DISTANTBAR1, _DISTANTBAR2, _DISTANTBAR3, _DISTANTBAR4,
                    _DISTANTLINE1, _DISTANTLINE2, _DISTANTLINE3, _DENWARP, _DENWEFT,
                    _SPEED, _WIDTHBE, _WIDTHAF, _BEGINLINE1, _BEGINLINE2, _BEGINLINE3, _BEGINLINE4,
                    _ENDLINE1, _ENDLINE2, _ENDLINE3, _ENDLINE4, _SELVAGELEFT, _SELVAGERIGHT, _FINISHINGPROCESS,
                    _STATUS, _LENGTHPRINT, _CLEARBY, _CLEARREMARK, _CLEARDATE, _SUSPENDDATE,
                    _P_2BEGINLINE1, _P_2BEGINLINE2, _P_2BEGINLINE3, _P_2BEGINLINE4,
                    _P_2ENDLINE1, _P_2ENDLINE2, _P_2ENDLINE3, _P_2ENDLINE4, _TENSION, _LENGTHDETAIL, _WIDTHAF_END);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return chkSave;
        }

        #endregion

        #region GetAuthorizeByProcessID

        public bool GetAuthorizeByProcessID(string PROCESSID, string USER, string PASS)
        {
            bool chkSave = false;

            try
            {
                chkSave = CutPrintDataService.Instance.GetAuthorizeByProcessID(PROCESSID, USER, PASS);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return chkSave;
        }

        #endregion

        #region ClearCut

        public bool ClearCut()
        {
            bool chkSave = false;

            try
            {
                chkSave = CutPrintDataService.Instance.CUT_UPDATEDATA(_ITEMLOT
                    , _STATUS, _CLEARBY, _CLEARREMARK, _CLEARDATE);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return chkSave;
        }

        #endregion

        #region Cut_GetMCSuspendData

        public List<CUT_GETMCSUSPENDDATA> Cut_GetMCSuspendData()
        {
            List<CUT_GETMCSUSPENDDATA> getMC = null;

            try
            {
                getMC = CutPrintDataService.Instance.Cut_GetMCSuspendData(_MCNO);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return getMC;
        }

        #endregion

        #endregion

        #region Public Proeprties

        /// <summary>
        /// Gets or sets machine.
        /// </summary>
        [XmlElement]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CutPrintMCItem Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
            }
        }
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
        public string ITEMCODE
        {
            get { return _ITEMCODE; }
            set
            {
                if (_ITEMCODE != value)
                {
                    _ITEMCODE = value;
                }
            }
        }
        [XmlAttribute]
        public string ITEMLOT
        {
            get { return _ITEMLOT; }
            set
            {
                if (_ITEMLOT != value)
                {
                    _ITEMLOT = value;
                }
            }
        }
        [XmlAttribute]
        public string BATCHNO
        {
            get { return _BATCHNO; }
            set
            {
                if (_BATCHNO != value)
                {
                    _BATCHNO = value;
                }
            }
        }
        [XmlAttribute]
        public string FINISHINGLOT
        {
            get { return _FINISHINGLOT; }
            set
            {
                if (_FINISHINGLOT != value)
                {
                    _FINISHINGLOT = value;
                }
            }
        }
        [XmlAttribute]
        public string PRODUCTTYPEID
        {
            get { return _PRODUCTTYPEID; }
            set
            {
                if (_PRODUCTTYPEID != value)
                {
                    _PRODUCTTYPEID = value;
                }
            }
        }

        [XmlAttribute]
        public DateTime? STARTDATE
        {
            get { return _STARTDATE; }
            set
            {
                if (_STARTDATE != value)
                {
                    _STARTDATE = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? ENDDATE
        {
            get { return _ENDDATE; }
            set
            {
                if (_ENDDATE != value)
                {
                    _ENDDATE = value;
                }
            }
        }

        [XmlAttribute]
        public string REMARK
        {
            get { return _REMARK; }
            set
            {
                if (_REMARK != value)
                {
                    _REMARK = value;
                }
            }
        }
        [XmlAttribute]
        public string MCNO
        {
            get { return _MCNO; }
            set
            {
                if (_MCNO != value)
                {
                    _MCNO = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTH1
        {
            get { return _WIDTH1; }
            set
            {
                if (_WIDTH1 != value)
                {
                    _WIDTH1 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTH2
        {
            get { return _WIDTH2; }
            set
            {
                if (_WIDTH2 != value)
                {
                    _WIDTH2 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTH3
        {
            get { return _WIDTH3; }
            set
            {
                if (_WIDTH3 != value)
                {
                    _WIDTH3 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTH4
        {
            get { return _WIDTH4; }
            set
            {
                if (_WIDTH4 != value)
                {
                    _WIDTH4 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTBAR1
        {
            get { return _DISTANTBAR1; }
            set
            {
                if (_DISTANTBAR1 != value)
                {
                    _DISTANTBAR1 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTBAR2
        {
            get { return _DISTANTBAR2; }
            set
            {
                if (_DISTANTBAR2 != value)
                {
                    _DISTANTBAR2 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTBAR3
        {
            get { return _DISTANTBAR3; }
            set
            {
                if (_DISTANTBAR3 != value)
                {
                    _DISTANTBAR3 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTBAR4
        {
            get { return _DISTANTBAR4; }
            set
            {
                if (_DISTANTBAR4 != value)
                {
                    _DISTANTBAR4 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTLINE1
        {
            get { return _DISTANTLINE1; }
            set
            {
                if (_DISTANTLINE1 != value)
                {
                    _DISTANTLINE1 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTLINE2
        {
            get { return _DISTANTLINE2; }
            set
            {
                if (_DISTANTLINE2 != value)
                {
                    _DISTANTLINE2 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DISTANTLINE3
        {
            get { return _DISTANTLINE3; }
            set
            {
                if (_DISTANTLINE3 != value)
                {
                    _DISTANTLINE3 = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DENWARP
        {
            get { return _DENWARP; }
            set
            {
                if (_DENWARP != value)
                {
                    _DENWARP = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? DENWEFT
        {
            get { return _DENWEFT; }
            set
            {
                if (_DENWEFT != value)
                {
                    _DENWEFT = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? SPEED
        {
            get { return _SPEED; }
            set
            {
                if (_SPEED != value)
                {
                    _SPEED = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTHBE
        {
            get { return _WIDTHBE; }
            set
            {
                if (_WIDTHBE != value)
                {
                    _WIDTHBE = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? WIDTHAF
        {
            get { return _WIDTHAF; }
            set
            {
                if (_WIDTHAF != value)
                {
                    _WIDTHAF = value;
                }
            }
        }
        [XmlAttribute]
        public string BEGINLINE1
        {
            get { return _BEGINLINE1; }
            set
            {
                if (_BEGINLINE1 != value)
                {
                    _BEGINLINE1 = value;
                }
            }
        }
        [XmlAttribute]
        public string BEGINLINE2
        {
            get { return _BEGINLINE2; }
            set
            {
                if (_BEGINLINE2 != value)
                {
                    _BEGINLINE2 = value;
                }
            }
        }
        [XmlAttribute]
        public string BEGINLINE3
        {
            get { return _BEGINLINE3; }
            set
            {
                if (_BEGINLINE3 != value)
                {
                    _BEGINLINE3 = value;
                }
            }
        }
        [XmlAttribute]
        public string BEGINLINE4
        {
            get { return _BEGINLINE4; }
            set
            {
                if (_BEGINLINE4 != value)
                {
                    _BEGINLINE4 = value;
                }
            }
        }
        [XmlAttribute]
        public string ENDLINE1
        {
            get { return _ENDLINE1; }
            set
            {
                if (_ENDLINE1 != value)
                {
                    _ENDLINE1 = value;
                }
            }
        }
        [XmlAttribute]
        public string ENDLINE2
        {
            get { return _ENDLINE2; }
            set
            {
                if (_ENDLINE2 != value)
                {
                    _ENDLINE2 = value;
                }
            }
        }
        [XmlAttribute]
        public string ENDLINE3
        {
            get { return _ENDLINE3; }
            set
            {
                if (_ENDLINE3 != value)
                {
                    _ENDLINE3 = value;
                }
            }
        }
        [XmlAttribute]
        public string ENDLINE4
        {
            get { return _ENDLINE4; }
            set
            {
                if (_ENDLINE4 != value)
                {
                    _ENDLINE4 = value;
                }
            }
        }
        [XmlAttribute]
        public string SELVAGELEFT
        {
            get { return _SELVAGELEFT; }
            set
            {
                if (_SELVAGELEFT != value)
                {
                    _SELVAGELEFT = value;
                }
            }
        }
        [XmlAttribute]
        public string SELVAGERIGHT
        {
            get { return _SELVAGERIGHT; }
            set
            {
                if (_SELVAGERIGHT != value)
                {
                    _SELVAGERIGHT = value;
                }
            }
        }

        //เพิ่ม 20/01/16
        [XmlAttribute]
        public string FINISHINGPROCESS
        {
            get { return _FINISHINGPROCESS; }
            set
            {
                if (_FINISHINGPROCESS != value)
                {
                    _FINISHINGPROCESS = value;
                }
            }
        }

        //เพิ่ม 03/05/16
        [XmlAttribute]
        public string STATUS
        {
            get { return _STATUS; }
            set
            {
                if (_STATUS != value)
                {
                    _STATUS = value;
                }
            }
        }
        [XmlAttribute]
        public decimal? LENGTHPRINT
        {
            get { return _LENGTHPRINT; }
            set
            {
                if (_LENGTHPRINT != value)
                {
                    _LENGTHPRINT = value;
                }
            }
        }
        [XmlAttribute]
        public string CLEARBY
        {
            get { return _CLEARBY; }
            set
            {
                if (_CLEARBY != value)
                {
                    _CLEARBY = value;
                }
            }
        }
        [XmlAttribute]
        public string CLEARREMARK
        {
            get { return _CLEARREMARK; }
            set
            {
                if (_CLEARREMARK != value)
                {
                    _CLEARREMARK = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? CLEARDATE
        {
            get { return _CLEARDATE; }
            set
            {
                if (_CLEARDATE != value)
                {
                    _CLEARDATE = value;
                }
            }
        }
        [XmlAttribute]
        public DateTime? SUSPENDDATE
        {
            get { return _SUSPENDDATE; }
            set
            {
                if (_SUSPENDDATE != value)
                {
                    _SUSPENDDATE = value;
                }
            }
        }

        //เพิ่ม 06/05/16
        [XmlAttribute]
        public string SHOWSELVAGE
        {
            get { return _SHOWSELVAGE; }
            set
            {
                if (_SHOWSELVAGE != value)
                {
                    _SHOWSELVAGE = value;
                }
            }
        }

        //เพิ่ม 27/06/16
        [XmlAttribute]
        public string P_2BEGINLINE1
        {
            get { return _P_2BEGINLINE1; }
            set
            {
                if (_P_2BEGINLINE1 != value)
                {
                    _P_2BEGINLINE1 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2BEGINLINE2
        {
            get { return _P_2BEGINLINE2; }
            set
            {
                if (_P_2BEGINLINE2 != value)
                {
                    _P_2BEGINLINE2 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2BEGINLINE3
        {
            get { return _P_2BEGINLINE3; }
            set
            {
                if (_P_2BEGINLINE3 != value)
                {
                    _P_2BEGINLINE3 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2BEGINLINE4
        {
            get { return _P_2BEGINLINE4; }
            set
            {
                if (_P_2BEGINLINE4 != value)
                {
                    _P_2BEGINLINE4 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2ENDLINE1
        {
            get { return _P_2ENDLINE1; }
            set
            {
                if (_P_2ENDLINE1 != value)
                {
                    _P_2ENDLINE1 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2ENDLINE2
        {
            get { return _P_2ENDLINE2; }
            set
            {
                if (_P_2ENDLINE2 != value)
                {
                    _P_2ENDLINE2 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2ENDLINE3
        {
            get { return _P_2ENDLINE3; }
            set
            {
                if (_P_2ENDLINE3 != value)
                {
                    _P_2ENDLINE3 = value;
                }
            }
        }

        [XmlAttribute]
        public string P_2ENDLINE4
        {
            get { return _P_2ENDLINE4; }
            set
            {
                if (_P_2ENDLINE4 != value)
                {
                    _P_2ENDLINE4 = value;
                }
            }
        }

        //เพิ่ม 28/06/17
        [XmlAttribute]
        public decimal? P_TENSION
        {
            get { return _TENSION; }
            set
            {
                if (_TENSION != value)
                {
                    _TENSION = value;
                }
            }
        }

        //เพิ่ม 25/08/17
        [XmlAttribute]
        public string LENGTHDETAIL
        {
            get { return _LENGTHDETAIL; }
            set
            {
                if (_LENGTHDETAIL != value)
                {
                    _LENGTHDETAIL = value;
                }
            }
        }

        //เพิ่มใหม่ 04/10/17
        [XmlAttribute]
        public decimal? WIDTHAF_END
        {
            get { return _WIDTHAF_END; }
            set
            {
                if (_WIDTHAF_END != value)
                {
                    _WIDTHAF_END = value;
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
